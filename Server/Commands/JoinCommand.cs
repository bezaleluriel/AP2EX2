using System;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICommand" />
    internal class JoinCommand : ICommand
    {
        //MEMBERS:
        /// <summary>
        /// The model
        /// </summary>
        private Model model;

        //CONSTRUCTOR:
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinCommand"/> class.
        /// </summary>
        /// <param name="m">The m.</param>
        public JoinCommand(Model m)
        {
            this.model = m;
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public Model Model
        {
            get { return model; }
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
        public string Execute(string[] args, TcpClient client = null)
        {
            string maze = Model.join(args[0], client).ToJSON()+'\n';
            return maze;
        }
    }
}