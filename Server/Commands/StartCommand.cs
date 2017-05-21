using System;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICommand" />
    internal class StartCommand : ICommand
    {
        //MEMBERS:
        /// <summary>
        /// The model
        /// </summary>
        private Model model;




        //CONSTRUCTOR:
        /// <summary>
        /// Initializes a new instance of the <see cref="StartCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public StartCommand(Model model)
        {
            this.model = model;
        }

        /******************************
        Execute - executes the command.
        ******************************/
        // the null is give you an option not to pass a client
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.start(name, rows, cols, client);
            return maze.ToJSON()+'\n';


        }
    }
}