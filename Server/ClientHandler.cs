using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
//using Client;
using System.IO;
using Server;
using Newtonsoft.Json.Linq;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IClientHandler" />
    public class ClientHandler : IClientHandler
    {
        //MEMBERS:
        /// <summary>
        /// The controller
        /// </summary>
        private IController controller;

        //CONSTRUCTOR:
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public ClientHandler(IController controller)
        {
            this.controller = controller;
        }

        /************
        HandleClient - Handles the client.
        ************/
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient(TcpClient client)
        {
            Task task;
            task = new Task(() =>
            {
              //  Console.WriteLine("starting to preform task");
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = " ";
                    while (!(commandLine.Equals("close")))
                    {
                        //reading a command from the client:
                      //  Console.WriteLine("before reading command");
                        commandLine = reader.ReadLine();
                      //  Console.WriteLine("after reading command");
                     //   Console.WriteLine("Got command: {0}", commandLine);
                        //send reply to client - the result from the command executed by the controller;
                        string result = controller.ExecuteCommand(commandLine, client);
                        result += '\n'; // after receiving result string we add end of line.
                        result += '@'; // we add character '@' to mark end of stream.
                        writer.WriteLine(result);
                        writer.Flush();
                    }
                }
                client.Close();
            });
            task.Start();
            //task.Wait();            
        }
    }
}


