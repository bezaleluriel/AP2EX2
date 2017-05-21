
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Wpf
{
    class ModelMultiPlayerMenu : INotifyPropertyChanged
    {
        public ModelMultiPlayerMenu()
        {
            port = int.Parse(ConfigurationManager.AppSettings["PortNum"]);
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("Client connected");
            //this.endOfCommunication = false;
        }

        private int port;

        IPEndPoint ep;

        private TcpClient client;

       // private bool endOfCommunication;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private ObservableCollection<string> gamesList;
        public ObservableCollection<string> GamesList
        {
            get { return gamesList; }
            set
            {
                gamesList = value;
                NotifyPropertyChanged("GamesList");
            }
        }

        private string mazeName;
        public string MazeName
        {
            get { return mazeName; }
            set
            {
                mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }

        private int mazeRows;
        public int MazeRows
        {
            get { return mazeRows; }
            set
            {
                mazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        private int mazeCols;
        public int MazeCols
        {
            get { return mazeCols; }
            set
            {
                mazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public void start(string command)
        {
            Console.WriteLine("in start func");
            Console.WriteLine("command string is:");
            Console.Write(command);

            //int Port =  int.Parse(Console.ReadLine());


            bool isMultiplayer = false;
            bool isClose = false;
            bool isSinglePlayerClosed = false;
            bool isNewSPCommand = false;
          //  bool IsMPReaderClosed = false;

            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            {
                //string command = null;
                bool isNewCommand = false;
                Task commandLineReader = new Task(() =>
                {
                    while (isSinglePlayerClosed == false)
                    {

                        isNewCommand = true;

                        if (!client.Connected)
                        {
                            client = new TcpClient();
                            client.Connect(ep);

                            stream = client.GetStream();
                            reader = new StreamReader(stream);
                            writer = new StreamWriter(stream);
                        }

                        if (command.Contains("start") || command.Contains("join"))
                        {
                            isMultiplayer = true;
                        }
                        if (command.Contains("close"))
                        {
                            isClose = true;
                        }
                        if ((command.Contains("list")))
                        {
                            isSinglePlayerClosed = true;                            
                        }
                    }

                });
                commandLineReader.Start();
                while (true)
                {
                    isMultiplayer = false;
                    isClose = false;
                    isNewSPCommand = false;
                    //IsMPReaderClosed = false;


                    if (isNewCommand)
                    {
                        System.Threading.Thread.Sleep(1);
                        writer.WriteLine(command);
                        writer.Flush();
                        isNewCommand = false;
                        isNewSPCommand = true;
                    }

                    //reading a reply from server
                    if (isNewSPCommand)
                    {
                        string builder = "";
                        while (true)
                        {
                            string feedback = reader.ReadLine();
                            builder += feedback;
                            if (reader.Peek() == '@' && !command.Contains("play"))
                            {
                                feedback.TrimEnd('\n');
                                builder.TrimEnd('\n');
                                break;
                            }
                            if (reader.Peek() == '@' && command.Contains("play"))
                            {
                                break;
                            }
                        }
                        reader.ReadLine();
                        isNewSPCommand = false;
                        /*                        
                        if (command.Contains("list"))
                        {
                            Console.Write("list string received is: ");
                            Console.WriteLine(builder);
                            //GamesList = Maze.FromJSON(builder);
                            //Console.WriteLine(builder);
                            break;
                        }
                        */
                 
                        if (command.Contains("list"))
                        {
                            //JArray j = new JArray();
                            //GamesList = j.ToObject<ObservableCollection<string>>();
                            List<string> games = new List<string>();
                            foreach (var token in JArray.Parse(builder))
                            {
                                games.Add(token.ToString());
                            }
                            GamesList = new ObservableCollection<string>(games);
                            //var myjson = JsonConvert.DeserializeObject<ObservableCollection<string>>(builder);                            
                            break;
                        }
                        break;
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (isMultiplayer)
                    {
                        Task multiplayerReader = new Task(() =>
                        {
                            while (isClose == false)
                            {
                                isClose = true;
                                Console.WriteLine("waiting to read a multiplayer command: ");
                                while (true)
                                {
                                    string feedback = reader.ReadLine();
                                    if (reader.Peek() == '@')
                                    {
                                        feedback.TrimEnd('\n');
                                        break;
                                    }
                                    Console.WriteLine("{0}", feedback);
                                    if (feedback.Contains("close"))
                                    {
                                        Console.WriteLine("updating is close to true ");
                                        isClose = true;
                                        break;
                                    }
                                }
                                reader.ReadLine();
                            }
                            //IsMPReaderClosed = true;
                            isMultiplayer = false;
                            Console.WriteLine("exiting multiplayerReader");

                        });




                        Task multiplayerWriter = new Task(() =>
                        {
                            while (isClose == false)
                            {
                                //Console.Write("Please enter a multiplayer command: ");
                                string multiplayerCommand = null;
                                if (isNewCommand)
                                {

                                    multiplayerCommand = command;
                                    isNewCommand = false;
                                    writer.WriteLine(multiplayerCommand);
                                    writer.Flush();
                                    Console.WriteLine("{0}", multiplayerCommand);
                                    if (multiplayerCommand.Contains("close"))
                                    {
                                        Console.WriteLine("the command that was received is closed! ");
                                        isClose = true;
                                        //while(IsMPReaderClosed == false) { }
                                        break;
                                    }
                                }
                            }
                            Console.WriteLine("exiting multiplayerWriter");
                            isMultiplayer = false;

                        });

                        multiplayerReader.Start();
                        multiplayerWriter.Start();

                        multiplayerReader.Wait();
                        multiplayerWriter.Wait();

                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    client.Close();
                    if (stream != null)
                    {
                        stream.Dispose();

                    }
                    if (stream != null)
                    {
                        reader.Dispose();

                    }
                    if (stream != null)
                    {
                        writer.Dispose();

                    }
                }
            }
            //client.Close();
        }


    }
}
