namespace LANMessenger {
	partial class ServerForm {
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
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.outputBox = new System.Windows.Forms.TextBox();
			this.ipLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// connectedPartiesListview
			// 
			this.connectedPartiesListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.connectedPartiesListview.Location = new System.Drawing.Point(674, 40);
			this.connectedPartiesListview.Name = "connectedPartiesListview";
			this.connectedPartiesListview.Size = new System.Drawing.Size(217, 329);
			this.connectedPartiesListview.TabIndex = 6;
			this.connectedPartiesListview.UseCompatibleStateImageBehavior = false;
			this.connectedPartiesListview.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Users";
			this.columnHeader1.Width = 99;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "IP";
			this.columnHeader2.Width = 112;
			// 
			// outputBox
			// 
			this.outputBox.Location = new System.Drawing.Point(12, 40);
			this.outputBox.Multiline = true;
			this.outputBox.Name = "outputBox";
			this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.outputBox.Size = new System.Drawing.Size(656, 329);
			this.outputBox.TabIndex = 5;
			// 
			// ipLabel
			// 
			this.ipLabel.AutoSize = true;
			this.ipLabel.Location = new System.Drawing.Point(12, 12);
			this.ipLabel.Name = "ipLabel";
			this.ipLabel.Size = new System.Drawing.Size(61, 13);
			this.ipLabel.TabIndex = 4;
			this.ipLabel.Text = "IP Address:";
			// 
			// ServerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(896, 398);
			this.Controls.Add(this.connectedPartiesListview);
			this.Controls.Add(this.outputBox);
			this.Controls.Add(this.ipLabel);
			this.Name = "ServerForm";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.TextBox outputBox;
		private System.Windows.Forms.Label ipLabel;
		private System.Windows.Forms.ListView connectedPartiesListview;
	}
}