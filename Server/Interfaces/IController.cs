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
    public interface IController
    {
        /*************************************************************
        ExecuteCommand - receiving a command creating a command object
        according to the command and calling it's execute method.
        **************************************************************/
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        string ExecuteCommand(string commandLine, TcpClient client);

        /*************************************
        setView2 - sets the view as a member.
        *************************************/
        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="view">The view.</param>
        void setView(IView view);
    }
}
