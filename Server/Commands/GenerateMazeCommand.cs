using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICommand" />
    class GenerateMazeCommand : ICommand
    {
        //MEMBERS:
        /// <summary>
        /// The model
        /// </summary>
        private Model model;

        //CONSTRUCTOR:
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateMazeCommand(Model model)
        {
            this.model = model;

        }

        /**************************************************
        Execute - execute the command with specified args.
        **************************************************/
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.GenerateMaze(name, rows, cols);
            return maze.ToJSON() + '\n';
        }
    }
}
