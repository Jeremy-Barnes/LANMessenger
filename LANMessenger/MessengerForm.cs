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

		private delegate void writerDelegate(String s);
		private delegate void AddSubItemCallback(ListView lv, String name, String ip);
		Action<byte[], NetworkStream> sendMessage;
		ServerClient partner;

		public MessengerForm(Action<byte[], NetworkStream> sendMsg, ServerClient convPartner) {
			InitializeComponent();
			sendMessage = sendMsg;
			partner = convPartner;
		}

		private void sendButton_Click(object sender, EventArgs e) {
			ASCIIEncoding encoder = new ASCIIEncoding();
			byte[] buffer = encoder.GetBytes(messageBox.Text);
			sendMessage(buffer, partner.clientStream);
			write(Environment.UserName + ": " + messageBox.Text);
			messageBox.Text = "";
		}

		public void write(String s) {
			if (InvokeRequired) {
				writerDelegate writeDel = new writerDelegate(write);
				Invoke(writeDel, s);
				return;
			}
			conversationBox.Text = conversationBox.Text + Environment.NewLine + s;
		}

		private void MessengerForm_FormClosing(object sender, FormClosingEventArgs e) {
			partner.clientStream.Close();
		}
	}
}