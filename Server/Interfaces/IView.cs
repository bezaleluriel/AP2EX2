using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Starts the server.
        /// </summary>
        void startServer();
        /// <summary>
        /// Stops the server.
        /// </summary>
        void stopServer();
    }
}
