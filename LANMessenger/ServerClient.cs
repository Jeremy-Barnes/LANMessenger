
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
using System.Runtime.Serialization;

namespace LANMessenger {
	[Serializable]
	public class ServerClient {
		public IPAddress thisIP;
		public String thisIPString;
		public String name;
		public TcpClient client;
		public NetworkStream clientStream;


		public ServerClient(String nam, String IPString, IPAddress IP, TcpClient clientAddress) {
			thisIP = IP;
			thisIPString = IPString;
			name = nam;
			client = clientAddress;
			if(client!=null)
				clientStream = client.GetStream();
		}
	}
}