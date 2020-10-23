using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankWarGame
{
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPAddress serverIp;
            if (!IPAddress.TryParse(tbxServerIp.Text, out serverIp))
            {
                MessageBox.Show("Invalid Ip adress given!");
                return;
            }

            var port = Decimal.ToInt32(nbxServerPort.Value);

            var ownPort = 50101;
            while (!PortAvailable(ownPort))
            {
                ownPort += 1;
            }
            
            var form = new Game(serverIp, port, ownPort);
            form.Show();
        }

        private bool PortAvailable(int port)
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == port)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
