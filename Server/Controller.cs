using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
//using Client;
//using Ex1.Interfaces;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IController" />
    class Controller : IController
    {
        //MEMBERS:
        /// <summary>
        /// The command
        /// </summary>
        private Dictionary<string, ICommand> command;
        /// <summary>
        /// The model
        /// </summary>
        private Model model;
        /// <summary>
        /// The view
        /// </summary>
        private IView view;

        /**********************************************************        
        CONSTRUCTOR - creates all of the commands in the dictionary
        and gives them the model as parameter.        
        **********************************************************/
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller" /> class.
        /// </summary>
        public Controller()
        {
            command = new Dictionary<string, ICommand>();
        }

        /****************************************
        ExecuteCommand - splits the command
        string received and Executes the command.
        ****************************************/
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' '); // split the command line to individual words to an array
            string commandKey = arr[0]; // the first word of the string
            if (!this.command.ContainsKey(commandKey))
            {
                return "Command not found";
            }
            string[] args = arr.Skip(1).ToArray();
            ICommand command2 = command[commandKey]; // creation of Command object with the first word we want.. 
            return command2.Execute(args, client); // do the execute by the model..
        }

        /**************************
        setView1 - Sets the view1.
        **************************/
        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="view">The view.</param>
        public void setView(IView view)
        {
            this.view = view;
        }

        /*************************
        setModel - sets the model 
        **************************/
        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void setModel(Model model)
        {
            this.model = model;
            command.Add("generate", new GenerateMazeCommand(model));
            command.Add("solve", new SolveCommand(model));
            command.Add("start", new StartCommand(model));
            command.Add("list", new ListCommand(model));
            command.Add("join", new JoinCommand(model));
            command.Add("close", new CloseCommand(model));
            command.Add("play", new PlayCommand(model));
        }
    }
}
