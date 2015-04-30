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

namespace LANMessenger {
	public partial class ConnectForm : Form {
		TcpClient client;
        public NetworkStream clientStream;
        public ConnectForm()
        {
            InitializeComponent();   
        }

		private void connectButton_Click(object sender, EventArgs e) {
			client = new TcpClient();
			
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ipBox.Text), 3000);
			client.Connect(serverEndPoint);
			if (client.Connected) {
				clientStream = client.GetStream();

				ASCIIEncoding encoder = new ASCIIEncoding();
				byte[] buffer = encoder.GetBytes(Environment.UserName);

				clientStream.Write(buffer, 0, buffer.Length);
				clientStream.Flush();
				this.Close();
			} else {
				ipBox.Text = "Invalid IP";
			}
		}
	}
}
