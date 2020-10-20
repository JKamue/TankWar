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
    public class TankWarServer
    {
        private readonly GameController _gameController;
        private List<SocketClient> _clients = new List<SocketClient>();
        private Dictionary<IPEndPoint, string> _clientToId = new Dictionary<IPEndPoint, string>();
        private readonly SocketConnection _connection;
        private readonly Timer _serverTick;
        private readonly bool _debug;
        private int ticks = 0;

        public TankWarServer(Map map, int port, bool debug = false)
        {
            _debug = debug;
            _connection = new SocketConnection(port, debug);
            _connection.OnMessageReceived += MessageHandler;
            _serverTick = new Timer(CalculateTick, new AutoResetEvent(false), 0, 5);
            _gameController = new GameController(map);
        }

        public void CalculateTick(Object stateInfo)
        {
            ticks++;
            // Remove inactive Clients
            var inactiveClients = _clients.Where(c => !c.Alive()).ToList();
            _clients.RemoveAll(c => !c.Alive());
            inactiveClients.ForEach(c => _gameController.RemovePlayer(c.Id));

            if(_debug)
                inactiveClients.ForEach(c => LogMessage(c.Id + " timed out"));


            // Let Game Controller Calculate Players
            _gameController.Tick();

            // Broadcast Player Information to each Player
            var positions = new Positions(_gameController.Bullets, _gameController.Explosions, _gameController.Players);
            var positionsSerialized = JsonConvert.SerializeObject(positions);
            var message = new Message(MessageId.Positions, positionsSerialized);
            _clients.ForEach(c => _connection.Send(message, c.Endpoint));
        }

        public void MessageHandler(object sender, SocketEventArgs s)
        {

            if (s.Message.Id.Equals(MessageId.GameJoined))
                ClientJoined(s.Client);
            
            if (s.Message.Id.Equals(MessageId.GameQuit))
                RemoveClient(s.Client);

            if (s.Message.Id.Equals(MessageId.Movement))
            {
                SetClientMovement(s.Client, s.Message);
                KeepAlive(s.Client);
            }
        }

        private void SetClientMovement(IPEndPoint client, Message message)
        {
            var movement = JsonConvert.DeserializeObject<Movement>(message.Content);
            var id = _clientToId[client];
            var player = _gameController.Players.Where(p => p.Id == id).ToList();

            if (player.Count == 0)
                return;

            player[0].SetMovement(movement);
        }

        private void ClientJoined(IPEndPoint client)
        {
            // Store Client
            var socketClient = new SocketClient(client);
            _clientToId[client] = socketClient.Id;
            _clients.Add(socketClient);
            _gameController.AddNewPlayer(socketClient.Id);
            LogMessage(socketClient.Id + " joined");

            // Send Map
            var message = new Message(MessageId.MapData, JsonConvert.SerializeObject(_gameController.Map));
            _connection.Send(message, client);
        }

        private void RemoveClient(IPEndPoint client)
        {
            var clientId = _clientToId[client];
            _clientToId.Remove(client);
            _clients.RemoveAll(c => c.Endpoint.Equals(client));
            _gameController.RemovePlayer(clientId);
            LogMessage(clientId + " quit");
        }

        private void KeepAlive(IPEndPoint client)
        {
            var test = _clients.Where(c => c.Endpoint.Equals(client)).ToList();
            if (test.Count == 1)
                test[0].KeepAliveReceived();
        }

        private void LogMessage(string mes)
        {
            if (_debug)
                Console.WriteLine("Tick " + ticks + ": \t\t" + mes);
        }
    }
}
