using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LANMessenger {
	public partial class MainForm : Form {

		private delegate void AddSubItemCallback(ServerClient client);
		private delegate void RemoveSubItemCallback(ServerClient client);
		private Action<ServerClient> selectPartner;
		private Action closeApp;

		public MainForm(Action<ServerClient> partnerSelect, Action closeApplication) {
			InitializeComponent();
			selectPartner = partnerSelect;
			closeApp = closeApplication;
		}

		private void connectedPartiesListview_MouseDoubleClick(object sender, MouseEventArgs e) {
			if(connectedPartiesListview.SelectedItems.Count == 1){
				selectPartner((ServerClient)connectedPartiesListview.SelectedItems[0].Tag);
			}
		}

		public void AddSubItem(ServerClient client) {
			if (InvokeRequired) {
				AddSubItemCallback asic = new AddSubItemCallback(AddSubItem);
				Invoke(asic, client);
				return;
			}

			ListViewItem lvi = new ListViewItem(client.name);
			lvi.Tag = client;
			connectedPartiesListview.Items.Add(lvi);
		}

		public void RemoveSubItem(ServerClient client) {
			if (InvokeRequired) {
				RemoveSubItemCallback asic = new RemoveSubItemCallback(RemoveSubItem);
				Invoke(asic, client);
				return;
			}

			for (int i = 0; i < connectedPartiesListview.Items.Count; i++) {
				if (connectedPartiesListview.Items[i].Text == client.name) {
					connectedPartiesListview.Items.RemoveAt(i);
					return;
				}
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			closeApp();
		}
	}
}
