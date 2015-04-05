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
		Client AA;
		Client BB;

		Action<String> write;

		public ServerThread(Action<String> wr, Client A, Client B) {
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

		private void HandleClientComm() {//object clientList) {
			ArrayList clientList = new ArrayList();
			TcpClient tcpClientA = (TcpClient)(((ArrayList)clientList)[0]);
			TcpClient tcpClientB = (TcpClient)(((ArrayList)clientList)[1]);
			NetworkStream clientStreamA = tcpClientA.GetStream();
			NetworkStream clientStreamB = tcpClientB.GetStream();

			byte[] messageA = new byte[4096];
			String msgA;
			int bytesReadA;
			byte[] messageB = new byte[4096];
			String msgB;
			int bytesReadB;

			while (true) {
				bytesReadA = 0;
				bytesReadB = 0;

				try {
					//blocks until a client sends a message
					bytesReadA = clientStreamA.Read(messageA, 0, 4096);
					bytesReadB = clientStreamB.Read(messageB, 0, 4096);
				} catch {
					//a socket error has occured
					// break;
				}

				ASCIIEncoding encoder = new ASCIIEncoding();
				byte[] buffer;
				msgA = encoder.GetString(messageA, 0, bytesReadA);
				msgB = encoder.GetString(messageB, 0, bytesReadB);

				if (bytesReadA != 0) {

					write(AA.name + ": " + msgA);
					buffer = encoder.GetBytes(BB.name + ": " + msgB);
					AA.clientStream.Write(buffer, 0, buffer.Length);
				}
				if (bytesReadB != 0) {

					write(BB.name + ": " + msgB);
					buffer = encoder.GetBytes(AA.name + ": " + msgA);
					BB.clientStream.Write(buffer, 0, buffer.Length);
				}

			}

			tcpClientA.Close();
		}

		private void HandleClientComm(object clientList) {
			NetworkStream clientStreamA = ((Client)((ArrayList)clientList)[0]).clientStream;
			NetworkStream clientStreamB = ((Client)((ArrayList)clientList)[1]).clientStream;
			Client CA = (Client)((ArrayList)clientList)[0];
			Client CB = (Client)((ArrayList)clientList)[1];
			byte[] messageA = new byte[4096];
			String msgA;
			int bytesReadA;

			while (true) {
				bytesReadA = 0;

				try {
					//blocks until a client sends a message
					bytesReadA = clientStreamA.Read(messageA, 0, 4096);
				} catch {
					//a socket error has occured
					// break;
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
