
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    class Server
    {
        //MEMBERS:
        /// <summary>
        /// The port
        /// </summary>
        private int port;
        /// <summary>
        /// The listener
        /// </summary>
        private TcpListener listener;
        /// <summary>
        /// The ch
        /// </summary>
        private IClientHandler ch;

        //CONSTRUCTOR:
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="ch">The ch.</param>
        public Server(IClientHandler ch)
        {
            port = int.Parse(ConfigurationManager.AppSettings["PortNum"]);
            this.ch = ch;
        }

        /******************
        start - listens to the socket waiting for a new client,
        when one arrives it uses clientHandler's method called
        handleClient to communicate with the client.
        ******************/
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
         //   Console.WriteLine("starting task ");
            task.Start();
            task.Wait();
        }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
    }
}