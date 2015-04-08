namespace LANMessenger {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.connectedPartiesListview = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.idLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// connectedPartiesListview
			// 
			this.connectedPartiesListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.connectedPartiesListview.Location = new System.Drawing.Point(12, 61);
			this.connectedPartiesListview.Name = "connectedPartiesListview";
			this.connectedPartiesListview.Size = new System.Drawing.Size(182, 357);
			this.connectedPartiesListview.TabIndex = 13;
			this.connectedPartiesListview.UseCompatibleStateImageBehavior = false;
			this.connectedPartiesListview.View = System.Windows.Forms.View.Details;
			this.connectedPartiesListview.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.connectedPartiesListview_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Users";
			this.columnHeader1.Width = 170;
			// 
			// idLabel
			// 
			this.idLabel.AutoSize = true;
			this.idLabel.Location = new System.Drawing.Point(9, 21);
			this.idLabel.Name = "idLabel";
			this.idLabel.Size = new System.Drawing.Size(116, 13);
			this.idLabel.TabIndex = 14;
			this.idLabel.Text = "Connected with name: ";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(210, 430);
			this.Controls.Add(this.idLabel);
			this.Controls.Add(this.connectedPartiesListview);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView connectedPartiesListview;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Label idLabel;
	}
}