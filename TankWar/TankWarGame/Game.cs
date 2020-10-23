using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using TankWarGame.Screen;
using TankWarLib.Dtos.Messages;
using TankWarLib.Objects;
using TankWarLib.Socket;
using Message = TankWarLib.Dtos.Messages.Message;

namespace TankWarGame
{
    public partial class Game : Form
    {
        private readonly SocketConnection _connection;
        private readonly IPEndPoint _serverEndPoint;
        private readonly BufferedScreenController _screenController;

        private readonly Timer _updater = new Timer
        {
            Interval = 50,
        };

        // Messages
        private static readonly Message JoinMessage = new Message(MessageId.GameJoined);
        private static readonly Message ShootMessage = new Message(MessageId.Shoot);

        public Game(IPAddress serverAddress, int port, int ownPort)
        {
            // Ready
            _connection = new SocketConnection(50001);
            _serverEndPoint = new IPEndPoint(serverAddress, port);
            _updater.Tick += QueryKeys;

            // Set
            InitializeComponent();
            _screenController = new BufferedScreenController(pnlGame, Color.White);
            KeyPreview = true;
            KeyDown += KeyStatus.KeyDownHandler;
            KeyUp += KeyStatus.KeyUpHander;
            _connection.OnMessageReceived += MessageHandler;

            // Go
            _connection.Send(JoinMessage, _serverEndPoint);
            _updater.Start();
        }

        private void QueryKeys(object sender, EventArgs e)
        {
            var move = 0;
            var turn = 0;
            var turret = 0;

            if (KeyStatus.IsPressed(38))
                move++;

            if (KeyStatus.IsPressed(40))
                move--;

            if (KeyStatus.IsPressed(37))
                turn--;

            if (KeyStatus.IsPressed(39))
                turn++;

            if (KeyStatus.IsPressed(68))
                turret++;

            if (KeyStatus.IsPressed(65))
                turret--;

            var movement = new Movement(move, turn, turret);
            var message = new Message(MessageId.Movement, JsonConvert.SerializeObject(movement));
            _connection.Send(message, _serverEndPoint);

            if (KeyStatus.IsPressed(32))
                _connection.Send(ShootMessage, _serverEndPoint);

            Size = _screenController.Map.Size;
        }

        public void MessageHandler(object sender, SocketEventArgs s)
        {
            if (s.Message.Id.Equals(MessageId.MapData))
            {
                var map = JsonConvert.DeserializeObject<Map>(s.Message.Content);
                _screenController.Map = map;
            }

            if (s.Message.Id.Equals(MessageId.Positions))
            {
                var positions = JsonConvert.DeserializeObject<Positions>(s.Message.Content);
                _screenController._players = positions.Players;
                _screenController._bullets = positions.Bullets;
                _screenController._explosions = positions.Explosions;
            }
        }
    }
}
