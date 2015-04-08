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

        private delegate void AddSubItemCallback(String name, String ip);
		private delegate void writerDelegate(String s);


        public ServerForm(String thisIPString)
        {
            InitializeComponent();
			writerDelegate writeDel = new writerDelegate(write);
			ipLabel.Text += thisIPString;
        }

		public void AddSubItem(String name, String ip) {
			if (InvokeRequired) {
				AddSubItemCallback asic = new AddSubItemCallback(AddSubItem);
				Invoke(asic, name, ip);
				return;
			}
			ListViewItem lvi = new ListViewItem(name);
			lvi.SubItems.Add(ip);
			connectedPartiesListview.Items.Add(lvi);
		}

		public void write(String s) {
			if (InvokeRequired) {
				writerDelegate writeDel = new writerDelegate(write);
				Invoke(writeDel, s);
				return;
			}
			this.outputBox.Text = outputBox.Text + Environment.NewLine + s;
		}


    }
}
