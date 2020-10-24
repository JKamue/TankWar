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

            var port = decimal.ToInt32(nbxServerPort.Value);

            // https://stackoverflow.com/a/5879681/14181760
            var startingAtPort = 50100;
            var maxNumberOfPortsToCheck = 500;
            var range = Enumerable.Range(startingAtPort, maxNumberOfPortsToCheck);
            var portsInUse =
                from p in range
                join used in System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveUdpListeners()
                    on p equals used.Port
                select p;

            var FirstFreeUDPPortInRange = range.Except(portsInUse).FirstOrDefault();

            if (FirstFreeUDPPortInRange > 0)
            {
                var form = new Game(serverIp, port, FirstFreeUDPPortInRange);
                form.Show();
            }
        }
    }
}
