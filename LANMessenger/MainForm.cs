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
		private Action<ServerClient> selectPartner;


		public MainForm(Action<ServerClient> partnerSelect) {
			InitializeComponent();
			selectPartner = partnerSelect;
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

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			
		}
	}
}
