using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Configuration;

namespace Client
{
    public class Client
    {
        //MEMBERS
        private int port;
        
        IPEndPoint ep;

        private TcpClient client;

     //   private bool endOfCommunication;

        //CONSTRUCTOR
        public  Client(){
            port = int.Parse(ConfigurationManager.AppSettings["PortNum"]);
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("Client connected");
            //this.endOfCommunication = false;
        } 

        /****************
        start - creates a connection with the server
        and sends messages saying what command it wants
        to be executed by the sever.
        ****************/
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void start()
        {
            //int Port =  int.Parse(Console.ReadLine());

            
            bool isMultiplayer = false;
            bool isClose = false;
            bool isNewSPCommand = false;
         //   bool IsMPReaderClosed = false;

            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            {
                string command = null;
                bool isNewCommand = false;
                Task commandLineReader = new Task(() =>
                {
                    while (true)
                    {
                        Console.WriteLine("Please enter a command: ");
                        command = Console.ReadLine();
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
                        while (true)
                        {
                            string feedback = reader.ReadLine();
                            if (reader.Peek() == '@' && !command.Contains("play"))
                            {
                                feedback.TrimEnd('\n');
                                break;
                            }
                            if (reader.Peek() == '@' && command.Contains("play"))
                            {
                                break;
                            }
                            Console.WriteLine("{0}", feedback);
                        }
                        reader.ReadLine();
                        isNewSPCommand = false;
                    }


                    if (isMultiplayer)
                    {
                        Task multiplayerReader = new Task(() =>
                        {
                            while (isClose == false)
                            {
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
                    client.Close();

                    stream.Dispose();
                    reader.Dispose();
                    writer.Dispose();
                }             
            } 
            //client.Close();
        } 
    }
}
