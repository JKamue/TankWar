using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TankWarLib.Dtos.Messages;

namespace TankWarLib.Socket
{
    public class SocketEventArgs
    {
        public IPEndPoint Client;
        public Message Message;

        public SocketEventArgs(IPEndPoint client, Message message)
        {
            Client = client;
            Message = message;
        }
    }
}
