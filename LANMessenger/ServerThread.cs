using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

using System.Threading;
using System.Collections;


namespace LANMessenger {
	class ServerThread {
		private Thread listenThreadA;
		private Thread listenThreadB;
		ServerClient AA;
		ServerClient BB;

		Action<String> write;

		public ServerThread(Action<String> wr, ServerClient A, ServerClient B) {
			AA = A;
			BB = B;
			write = wr;
			listenThreadA = new Thread(new ParameterizedThreadStart(HandleClientComm));
			ArrayList clientList = new ArrayList();
			clientList.Add(AA);
			clientList.Add(BB);
			listenThreadA.Start(clientList);

			listenThreadB = new Thread(new ParameterizedThreadStart(HandleClientComm));
			clientList = new ArrayList();
			clientList.Add(BB);
			clientList.Add(AA);
			listenThreadB.Start(clientList);

		}

		private void HandleClientComm(object clientList) {
			NetworkStream clientStreamA = ((ServerClient)((ArrayList)clientList)[0]).clientStream;
			NetworkStream clientStreamB = ((ServerClient)((ArrayList)clientList)[1]).clientStream;
			ServerClient CA = (ServerClient)((ArrayList)clientList)[0];
			ServerClient CB = (ServerClient)((ArrayList)clientList)[1];
			byte[] messageA = new byte[4096];
			String msgA;
			int bytesReadA;

			while (true) {
				bytesReadA = 0;

				try {
					bytesReadA = clientStreamA.Read(messageA, 0, 4096);
				} catch {
					 break;
				}

				ASCIIEncoding encoder = new ASCIIEncoding();
				byte[] buffer;
				msgA = encoder.GetString(messageA, 0, bytesReadA);


				if (bytesReadA != 0) {
					write(CA.name + ": " + msgA);
					buffer = encoder.GetBytes(CA.name + ": " + msgA);
					clientStreamB.Write(buffer, 0, buffer.Length);
				}
			}
		}

	}
}
