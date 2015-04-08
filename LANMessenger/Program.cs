using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LANMessenger {
	static class Program {

		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			DialogResult type = MessageBox.Show("Run as server? (No to run as client)", "Instance Type", MessageBoxButtons.YesNoCancel);
			if (type == DialogResult.Yes) {
				Server localServer = new Server();
				Application.Run(localServer.getForm());
			} else if (type == DialogResult.No) {
				ConnectForm connector = new ConnectForm();
				connector.ShowDialog();
				if (connector.clientStream != null) {
					Client localClient = new Client(connector.clientStream);
					Application.Run(localClient.getForm());
				}
			}
		}
	}
}
