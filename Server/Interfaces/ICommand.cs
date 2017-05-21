using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/*****************************************************************************************
this interface is for the command pattern/
we want to wrap every command as an object/
we have an array of args of some command
we pass an object that represent the client for the option to return 
an answer later(we will want to hold the connection open until other client will arrive)
args- will contain the name of the maze and the size of the maze (3 arguments)//
*****************************************************************************************/

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommand
    {
        /*******************************
        Execute - executes the command.
        *******************************/
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        string Execute(string[] args, TcpClient client = null);
    }
}
