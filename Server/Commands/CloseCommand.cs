using System;
using System.IO;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICommand" />
    internal class CloseCommand : ICommand
    {
        //MEMBERS:
        /// <summary>
        /// The model
        /// </summary>
        private Model model;

        //CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseCommand(Model model)
        {
            this.model = model;
        }

        /***************************************
        Execute - execute the command.
        ***************************************/
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            //Tuple<TcpClient, string> tup = model.c(client);
            TcpClient otherPlayer = model.Close(client);
            //TcpClient otherPlayer = tup.Item1;
            string closeMessage = "close";
            NetworkStream stream = otherPlayer.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            closeMessage += '\n'; // after receiving result string we add end of line.
            writer.WriteLine(closeMessage);
            writer.Flush();

            string rv = "close";
            return rv;
        }
    }
}