using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LANMessenger {
	public partial class ServerForm : Form {
        private Dictionary<String, Client> activeUsers;

        private IPAddress thisIP;
        private String thisIPString;

        private TcpListener tcpListener;
        private Thread listenThread;

        private delegate void writerDelegate(String s);

        private delegate void AddSubItemCallback(ListView lv, String name, String ip);

        public ServerForm()
        {
            InitializeComponent();
            IPAddress[] ipswitch = Dns.GetHostAddresses(Dns.GetHostName());
            writerDelegate writeDel = new writerDelegate(write);

            activeUsers = new Dictionary<String,Client>();

            foreach (IPAddress ip in ipswitch)
            {


                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    thisIPString = ip.ToString();
                    thisIP = ip;
                }
            }
            
            this.tcpListener = new TcpListener(thisIP, 3000);
            
            thisIPString = Dns.GetHostAddresses(Dns.GetHostName())[1].ToString();
            ipLabel.Text = thisIPString;
            this.Update();
            write("Server established with IP " + thisIPString);

           
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
            write("Listening for clients...");
        }

        private void ListenForClients()
        {
            tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();
                NetworkStream clientStream = client.GetStream();
                String name = "";
                byte[] message = new byte[4096];
                int bytesRead = clientStream.Read(message, 0, 4096);

                if(bytesRead == 0) //the client failed to handshake
                    continue;
                
                //handshake has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
				name = encoder.GetString(message, 0, bytesRead); //+ (activeUsers.Count + 1);

                //create a thread to handle communication with connected client
              //  Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
              //  clientThread.Start(client);
                write("Client (" + name +") found!");

                Client currClient = new Client(name, ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), ((IPEndPoint)client.Client.RemoteEndPoint).Address, client);
                
                activeUsers.Add(name, currClient);
                AddSubItem(connectedPartiesListview, name, ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());

                if(activeUsers.Count == 2) {
                    ServerThread sth = new ServerThread(write, activeUsers[activeUsers.Keys.ToArray()[0]], activeUsers[activeUsers.Keys.ToArray()[1]]);
                }

            }
        }

        //private void HandleClientComm(object client)
        //{
        //    TcpClient tcpClient = (TcpClient)client;
        //    NetworkStream clientStream = tcpClient.GetStream();
            
        //    byte[] message = new byte[4096];
        //    int bytesRead;

        //    while (true)
        //    {
        //        bytesRead = 0;

        //        try
        //        {
        //            //blocks until a client sends a message
        //            bytesRead = clientStream.Read(message, 0, 4096);
        //           // write(bytesRead.ToString());
        //        }
        //        catch
        //        {
        //            //a socket error has occured
        //            break;
        //        }

        //        if (bytesRead == 0)
        //        {
        //            //the client has disconnected from the server
        //            break;
        //        }

        //        //message has successfully been received
        //        ASCIIEncoding encoder = new ASCIIEncoding();
        //        write(encoder.GetString(message, 0, bytesRead));
        //    }

        //    tcpClient.Close();
        //}

        public void write(String s)
        {
            if (InvokeRequired)
            {
                writerDelegate writeDel = new writerDelegate(write);
                Invoke(writeDel, s);
                return;
            }
            this.outputBox.Text = outputBox.Text + Environment.NewLine + s;
        }

        public void AddSubItem(ListView lv, String name, String ip) {
            if(InvokeRequired) {
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
