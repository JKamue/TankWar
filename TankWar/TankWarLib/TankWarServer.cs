using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TankWarLib.Dtos.Messages;
using TankWarLib.Objects;
using TankWarLib.Socket;

namespace TankWarLib
{
    class TankWarServer
    {
        private readonly GameController _gameController;
        private List<SocketClient> _clients = new List<SocketClient>();
        private readonly SocketConnection _connection;
        private readonly Timer _serverTick;

        public TankWarServer(Map map, int port, bool debug = false)
        {
            _connection = new SocketConnection(port, debug);
            _connection.OnMessageReceived += MessageHandler;
            _serverTick = new Timer(CalculateTick, new AutoResetEvent(false), 0, 10);
        }

        public void CalculateTick(Object stateInfo)
        {
            // Remove inactive Clients
            var inactiveClients = _clients.Where(c => !c.Alive()).ToList();
            inactiveClients.ForEach(c => _gameController.RemovePlayer(c.Id));

            // Let Game Controller Calculate Players
            _gameController.Tick();

            // Broadcast Player Information to each Player
            var positions = JsonConvert.SerializeObject(_gameController.Players);
            var message = new Message(MessageId.Positions, positions);
            _clients.ForEach(c => _connection.Send(message, c.Endpoint));
        }

        public void MessageHandler(object sender, SocketEventArgs s)
        {
            var client = _clients.First(c => c.Endpoint.Equals(s.Client));
            var envelope = new Envelope(client.Id, s.Message);

            if (s.Message.Id == MessageId.GameJoined)
                ClientJoined(s.Client); return;

            if (s.Message.Id == MessageId.KeepAlive)
                KeepAlive(s.Client); return;
        }

        private void ClientJoined(IPEndPoint client)
        {
            // Store Client
            var socketClient = new SocketClient(client);
            _clients.Add(socketClient);
            _gameController.AddNewPlayer(socketClient.Id);

            // Send Map
            var message = new Message(MessageId.MapData, JsonConvert.SerializeObject(_gameController.Map));
            _connection.Send(message, client);
        }

        private void KeepAlive(IPEndPoint client) => _clients.First(c => c.Endpoint.Equals(client)).KeepAliveReceived();
    }
}
