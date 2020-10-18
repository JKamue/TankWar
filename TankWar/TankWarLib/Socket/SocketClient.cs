using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Socket
{
    public class SocketClient
    {
        private DateTime _lastKeepAlive;
        public readonly IPEndPoint Endpoint;
        public readonly string Id = Guid.NewGuid().ToString("D");

        public SocketClient(IPEndPoint endpoint)
        {
            _lastKeepAlive = DateTime.Now;
            Endpoint = endpoint;
        }

        public void KeepAliveReceived()
        {
            _lastKeepAlive = DateTime.Now;
        }

        public bool Alive()
        {
            return DateTime.Now.Subtract(new TimeSpan(0, 0, 1)) < _lastKeepAlive;
        }
    }
}
