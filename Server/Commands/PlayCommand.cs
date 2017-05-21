using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICommand" />
    internal class PlayCommand : ICommand
    {
        //MEMBERS:
        /// <summary>
        /// The model
        /// </summary>
        private Model model;

        //CONSTRUCTOR:
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public PlayCommand(Model model)
        {
            this.model = model;
        }

        /******************************
        Execute - executes the command.
        ******************************/
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {
            string direction = args[0];
            Tuple<TcpClient, string> tup = model.getOtherPlayer(client);
            TcpClient otherPlayer = tup.Item1;
            string name = tup.Item2;
            string playCommandMessage = toJason(direction, name);
            NetworkStream stream = otherPlayer.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            
           // playCommandMessage += '\n'; // after receiving result string we add end of line.
           // playCommandMessage += '@'; // we add character '@' to mark end of stream.
            writer.WriteLine(playCommandMessage);
            writer.Flush();
            
            string rv = "play command message successfully sent :)";
            return rv + '\n';
        }


        /******************************
        toJason - creates a json obj.
        ******************************/
        /// <summary>
        /// To the jason.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string toJason(string direction, string name)
        {
            JObject solutionObj = new JObject();
            solutionObj["Name"] = name;
            solutionObj["Direction"] = direction;
            return solutionObj.ToString();
        }
    }
}