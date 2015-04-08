using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LANMessenger {
	class Server {

		private ServerForm myForm;

		private Dictionary<String, ServerClient> activeUsers;

		private IPAddress thisIP;
		private String thisIPString;

		private TcpListener tcpListener;
		private Thread listenThread;

		public Server() {

			IPAddress[] ipswitch = Dns.GetHostAddresses(Dns.GetHostName());
			activeUsers = new Dictionary<String, ServerClient>();

			foreach (IPAddress ip in ipswitch) {//find my local IP
				if (ip.AddressFamily == AddressFamily.InterNetwork) {
					thisIPString = ip.ToString();
					thisIP = ip;
					break;
				}
			}

			tcpListener = new TcpListener(thisIP, 3000);
			myForm = new ServerForm(thisIPString);
			myForm.Show();
			myForm.write("Server established with IP " + thisIPString);
			listenThread = new Thread(new ThreadStart(ListenForClients));
			listenThread.Start();
			myForm.write("Listening for clients...");
			myForm.Update();

		}

		private void ListenForClients() {
			tcpListener.Start();

			while (true) {
				TcpClient client = this.tcpListener.AcceptTcpClient();
				NetworkStream clientStream = client.GetStream();
				String name = "";
				byte[] message = new byte[4096];
				int bytesRead = clientStream.Read(message, 0, 4096);

				if (bytesRead == 0) //the client failed to handshake
					continue;

				//handshake has successfully been received
				ASCIIEncoding encoder = new ASCIIEncoding();
				name = encoder.GetString(message, 0, bytesRead) + DateTime.Now.Millisecond.ToString();

				myForm.write("Client (" + name + ") found!");

				//update all users as to who's online
				ServerClient currClient = new ServerClient(name, ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), ((IPEndPoint)client.Client.RemoteEndPoint).Address, client);
				foreach(ServerClient sc in activeUsers.Values){
					byte[] buffer = encoder.GetBytes(currClient.name +";" + currClient.thisIPString);
					sc.clientStream.Write(buffer, 0, buffer.Length);
					buffer = encoder.GetBytes(sc.name + ";" + sc.thisIPString);
					currClient.clientStream.Write(buffer, 0, buffer.Length);
				}

				
				activeUsers.Add(name, currClient);
				
				myForm.AddSubItem(name, ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
			}
		}

		public ServerForm getForm() {
			return myForm;
		}
	}
}
