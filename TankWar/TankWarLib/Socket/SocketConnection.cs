using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TankWarLib.Dtos.Messages;

namespace TankWarLib.Socket
{
    public class SocketConnection
    {
        private readonly UdpClient _udpClient;
        private readonly bool _debug;

        public delegate void MessageReceptionHandler(object sender, SocketEventArgs e);
        public event MessageReceptionHandler OnMessageReceived;

        public SocketConnection(int myPort, bool debug = false)
        {
            _udpClient = new UdpClient(myPort);
            _debug = debug;
            _udpClient.BeginReceive(DataReceived, _udpClient);
        }

        public void Send(Message message, IPEndPoint partner)
        {
            var content = message.Serialize();
            _udpClient.Send(Encoding.ASCII.GetBytes(content), content.Length, partner);
        }

        private void DataReceived(IAsyncResult ar)
        {
            IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                var receivedBytes = _udpClient.EndReceive(ar, ref receivedIpEndPoint);


                var receivedText = Encoding.ASCII.GetString(receivedBytes);
                var message = Message.Deserialize(receivedText);

                if (_debug)
                    Console.WriteLine(receivedIpEndPoint + ": \tPacket id " + message.Id.Content);

                OnMessageReceived?.Invoke(this, new SocketEventArgs(receivedIpEndPoint, message));

                // Restart listening for udp data packages
                _udpClient.BeginReceive(DataReceived, _udpClient);
            }
            catch (SocketException e)
            {
            }
            catch (ObjectDisposedException e)
            {
            }

        }

        public void Close()
        {
            _udpClient.Client.Shutdown(SocketShutdown.Receive);
            _udpClient.Client.Close();
            _udpClient.Close();
        }
    }
}
