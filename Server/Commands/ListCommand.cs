using System;
using System.Collections.Generic;
using System.Net.Sockets;
using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICommand" />
    internal class ListCommand : ICommand
    {
        //MEMBERS:
        /// <summary>
        /// The model
        /// </summary>
        private Model model;

        //CONSTRUCTOR:
        /// <summary>
        /// Initializes a new instance of the <see cref="ListCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ListCommand(Model model)
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
        public string Execute(string[] args, TcpClient client = null)
        {
            // the avaliable game lists lists
            JArray GamesList = model.getLists();
            // cool shortcut to serialize the list to json format not working dont delte!!
          //  string str = JsonConvert.SerializeObject(GamesList);
          //  return str;

            return GamesList.ToString() + '\n';
        }

        }
    
}