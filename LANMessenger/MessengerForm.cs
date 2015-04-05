using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LANMessenger {
	public partial class MessengerForm : Form {

		TcpClient client;
		NetworkStream clientStream;
		private Thread listenThread;

		private delegate void writerDelegate(String s);
		private delegate void AddSubItemCallback(ListView lv, String name, String ip);

		public MessengerForm(NetworkStream ClientStream) {
			InitializeComponent();
			clientStream = ClientStream;
			listenThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
			listenThread.Start(ClientStream);
		}

		private void sendButton_Click(object sender, EventArgs e) {
			ASCIIEncoding encoder = new ASCIIEncoding();
			byte[] buffer = encoder.GetBytes(messageBox.Text);

			write(Environment.UserName + ": " + messageBox.Text);
			clientStream.Write(buffer, 0, buffer.Length);
			clientStream.Flush();

			messageBox.Text = "";
		}

		private void HandleClientComm(object client) {
			//TcpClient tcpClientA = (TcpClient)client;
			NetworkStream clientStreamA = (NetworkStream)client;

			byte[] messageA = new byte[4096];
			int bytesReadA;

			while (true) {
				bytesReadA = 0;

				try {
					//blocks until a client sends a message
					bytesReadA = clientStreamA.Read(messageA, 0, 4096);

				} catch {
					//a socket error has occured
					break;
				}

				if (bytesReadA == 0) {
					//the client has disconnected from the server
					break;
				}

				//message has successfully been received
				ASCIIEncoding encoder = new ASCIIEncoding();
				write(encoder.GetString(messageA, 0, bytesReadA));
			}

			clientStreamA.Close();
		}

		public void write(String s) {
			if (InvokeRequired) {
				writerDelegate writeDel = new writerDelegate(write);
				Invoke(writeDel, s);
				return;
			}
			conversationBox.Text = conversationBox.Text + Environment.NewLine + s;
		}

		public void AddSubItem(ListView lv, String name, String ip) {
			if (InvokeRequired) {
				AddSubItemCallback asic = new AddSubItemCallback(AddSubItem);
				Invoke(asic, lv, name, ip);
				return;
			}
			ListViewItem lvi = new ListViewItem(name);
			lvi.SubItems.Add(ip);
			lv.Items.Add(lvi);
		}
	}
}