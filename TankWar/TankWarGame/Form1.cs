using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
    public partial class Form1 : Form
    {
        private IPEndPoint ServerEndPoint;
        private Timer KeepAlive = new Timer();
        private SocketConnection Connection;
        private BufferedScreenController screenController;

        public Form1()
        {
            InitializeComponent();
            KeepAlive.Interval = 50;
            KeepAlive.Tick += QueryKeys;
            screenController = new BufferedScreenController(pnlGame, Color.White);
            KeyPreview = true;
            KeyDown += KeyStatus.KeyDownHandler;
            KeyUp += KeyStatus.KeyUpHander;
        }

        public void MessageHandler(object sender, SocketEventArgs s)
        {
            if (s.Message.Id.Equals(MessageId.MapData))
            {
                var map = JsonConvert.DeserializeObject<Map>(s.Message.Content);
                screenController.Map = map;
            }

            if (s.Message.Id.Equals(MessageId.Positions))
            {
                var positions = JsonConvert.DeserializeObject<Positions>(s.Message.Content);
                screenController._players = positions.Players;
                screenController._bullets = positions.Bullets;
            }


            Console.WriteLine("Received:" + s.Message.Id);
            Console.WriteLine(s.Message.Content);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            Connection = new SocketConnection((int) nbxOwnPort.Value, true);
            var joinMessage = new Message(MessageId.GameJoined);
            ServerEndPoint = new IPEndPoint(IPAddress.Parse(tbxIpAdress.Text), (int) nbxServerPort.Value);
            Connection.Send(joinMessage, ServerEndPoint);
            Connection.OnMessageReceived += MessageHandler;
            KeepAlive.Start();
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
            Connection.Send(message, ServerEndPoint);

            if (KeyStatus.IsPressed(32))
            {
                var shoot = new Message(MessageId.Shoot);
                Connection.Send(shoot, ServerEndPoint);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            var quitTheGame = new Message(MessageId.GameQuit);
            screenController._players = new List<Player>();
            screenController.Map = new Map(new List<Line>());
            Connection.Send(quitTheGame, ServerEndPoint);
            KeepAlive.Stop();
            btnConnect.Enabled = true;
            Connection.Close();
            Connection = null;
        }
    }
}
