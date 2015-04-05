using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;

namespace LANMessenger {
	class Client {
		public IPAddress thisIP;
		public String thisIPString;
		public String name;
		public TcpClient client;
		public NetworkStream clientStream;


		public Client(String nam, String IPString, IPAddress IP, TcpClient clientAddress) {
			thisIP = IP;
			thisIPString = IPString;
			name = nam;
			client = clientAddress;
			clientStream = client.GetStream();
		}
	}
}
