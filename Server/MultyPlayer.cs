using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;


namespace Client
{
    /// <summary>
    /// 
    /// </summary>
    public class MultyPlayer
    {
        /// <summary>
        /// Gets the TCP client.
        /// </summary>
        /// <value>
        /// The TCP client.
        /// </value>
        public TcpClient tcpClient { get;  }
        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        private List<Direction> direction { get; }
        /// <summary>
        /// To wait
        /// </summary>
        private bool toWait;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultyPlayer" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public MultyPlayer(TcpClient client)
        {
            tcpClient = client;
            toWait = false;
            direction = new List<Direction>();
        }

        /// <summary>
        /// Waits this instance.
        /// </summary>
        public void Wait()
        {
            toWait = true;
            while (toWait == true)
            {
                System.Threading.Thread.Sleep(1);
                
            }
        }

        /// <summary>
        /// Stops the wait.
        /// </summary>
        public void StopWait()
        {
            toWait = false;
        }

        /**
        public override bool Equals(object obj)
        {
            return tcpClient.Equals(obj);
        }
        **/

    }
}
