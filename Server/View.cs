using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;
//using Ex1.Interfaces;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Server.Interfaces.IView" />
    class View : IView
    {
        //MEMBERS:
        /// <summary>
        /// The view port
        /// </summary>
       // private int viewPort;
        /// <summary>
        /// The view client handler
        /// </summary>
        private IClientHandler viewClientHandler;
        /// <summary>
        /// The server
        /// </summary>
        private Server server;


        //CONSTRUCTOR:
        /// <summary>
        /// Initializes a new instance of the <see cref="View"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public View(Controller controller)
        {
            viewClientHandler = new ClientHandler(controller);
             server = new Server(viewClientHandler);
        }

        /*****************************
        startServer - opens the server.
        *****************************/
        /// <summary>
        /// Starts the server.
        /// </summary>
        public void startServer()
        {
            this.server.Start();
        }

        /*******************************
        stopServer - closes the server.
        *******************************/
        /// <summary>
        /// Stops the server.
        /// </summary>
        public void stopServer()
        {
            this.server.Stop();
        }
    }
}
