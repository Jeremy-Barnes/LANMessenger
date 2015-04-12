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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LANMessenger {
	class Client {

		private NetworkStream mainServerStream;
		MainForm myForm;
		private TcpListener tcpListener;
		IPAddress thisIP;
		string thisIPString;


		private Dictionary<ServerClient, MessengerForm> activeConversations;

		private List<ServerClient> otherParties;

		public Client(NetworkStream ClientStream) {
			mainServerStream = ClientStream;
			myForm = new MainForm(selectPartner);
			activeConversations = new Dictionary<ServerClient, MessengerForm>();
			otherParties = new List<ServerClient>();

			IPAddress[] ipswitch = Dns.GetHostAddresses(Dns.GetHostName());
			foreach (IPAddress ip in ipswitch) {//find my local IP
				if (ip.AddressFamily == AddressFamily.InterNetwork) {
					thisIPString = ip.ToString();
					thisIP = ip;
					break;
				}
			}
			tcpListener = new TcpListener(thisIP, 3900);

			Thread contactThread = new Thread(new ParameterizedThreadStart(ListenForContactsList));
			contactThread.Start(mainServerStream);
			Thread conversationThread = new Thread(ListenForNewConversations);
			conversationThread.Start();	
		}

		private void ListenForNewConversations() {
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
				name = encoder.GetString(message, 0, bytesRead);

				ServerClient currClient = new ServerClient(name, ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), ((IPEndPoint)client.Client.RemoteEndPoint).Address, client);
				myForm.BeginInvoke((Action)delegate {
					MessengerForm mf = new MessengerForm(sendMessageToPartner, currClient);
					mf.Show();
					activeConversations.Add(currClient, mf);
					Thread listenThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
					listenThread.Start(currClient);
				});
				
			}
		} 

		private void ListenForContactsList(object server) {
			NetworkStream serverStream = (NetworkStream)server;
			byte[] message = new byte[4096];
			int bytesRead;

			while (true) {
				bytesRead = 0;

				try {
					bytesRead = serverStream.Read(message, 0, 4096);
				} catch {
				}
				if (bytesRead != 0) {
					ASCIIEncoding encoder = new ASCIIEncoding();
					string[] msg = Encoding.ASCII.GetString(message, 0, bytesRead).Split(';');
					string clname = msg[0];
					string clip = msg[1].Trim();
					ServerClient cl = new ServerClient(clname, clip, IPAddress.Parse(clip), null);
					if (cl.name != Environment.UserName) {
						if (otherParties.Contains(cl)) {
							otherParties.Remove(cl);
						} else {
							otherParties.Add(cl);
						}
						myForm.AddSubItem(cl);
					}
				}
			}
		}

		private void HandleClientComm(object client) {
			ServerClient partner = (ServerClient)client;
			MessengerForm myMessengerForm = activeConversations[partner];
			byte[] messageA = new byte[4096];
			int bytesReadA;

			while (true) {
				bytesReadA = 0;

				try {
					bytesReadA = partner.clientStream.Read(messageA, 0, 4096);
				} catch {
					break;
				}

				if (bytesReadA == 0) {
					break;
				}

				//message has successfully been received
				ASCIIEncoding encoder = new ASCIIEncoding();
				myMessengerForm.write(encoder.GetString(messageA, 0, bytesReadA));
			}
			partner.clientStream.Close();
			myMessengerForm.Close();
		}

		private void sendMessageToPartner(byte[] msgBuffer, NetworkStream partnerStream) {
			partnerStream.Write(msgBuffer, 0, msgBuffer.Length);
			partnerStream.Flush();
		}

		public MainForm getForm() {
			return myForm;
		}

		private void selectPartner(ServerClient c) {
			if (!activeConversations.Keys.Contains(c)) {
				TcpClient tclient = new TcpClient();
				IPEndPoint end = new IPEndPoint(c.thisIP, 3900);
				tclient.Connect(end);
				if (tclient.Connected) {
					c.client = tclient;
					c.clientStream = tclient.GetStream();
					ASCIIEncoding encoder = new ASCIIEncoding();
					byte[] buffer = encoder.GetBytes(Environment.UserName);
					c.clientStream.Write(buffer, 0, buffer.Length);
					c.clientStream.Flush();
					MessengerForm mf = new MessengerForm(sendMessageToPartner, c);
					mf.Show();
					activeConversations.Add(c, mf);
					Thread listenThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
					listenThread.Start(c);
				}
			}
		}
	}
}
