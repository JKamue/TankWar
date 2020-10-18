using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankWarLib.Dtos.Messages;
using TankWarLib.Socket;
using Message = TankWarLib.Dtos.Messages.Message;

namespace TankWarGame
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var connection = new SocketConnection(50001, true);
            var joinMessage = new Message(MessageId.GameJoined, "");
            var quitMessage = new Message(MessageId.GameQuit, "");
            var partner = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 50000);
            connection.Send(joinMessage, partner);
            connection.OnMessageReceived += MessageHandler;
            connection.Send(quitMessage, partner);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void MessageHandler(object sender, SocketEventArgs s)
        {
            Console.WriteLine("Received:" + s.Message.Id);
            Console.WriteLine(s.Message.Content);
        }
    }
}
