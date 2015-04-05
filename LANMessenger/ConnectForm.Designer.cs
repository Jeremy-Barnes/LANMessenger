namespace LANMessenger {
	partial class ConnectForm {
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
			this.IPLabel = new System.Windows.Forms.Label();
			this.ipBox = new System.Windows.Forms.TextBox();
			this.connectButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// IPLabel
			// 
			this.IPLabel.AutoSize = true;
			this.IPLabel.Location = new System.Drawing.Point(21, 9);
			this.IPLabel.Name = "IPLabel";
			this.IPLabel.Size = new System.Drawing.Size(120, 13);
			this.IPLabel.TabIndex = 0;
			this.IPLabel.Text = "Enter Server IP Address";
			// 
			// ipBox
			// 
			this.ipBox.Location = new System.Drawing.Point(24, 37);
			this.ipBox.Name = "ipBox";
			this.ipBox.Size = new System.Drawing.Size(231, 20);
			this.ipBox.TabIndex = 1;
			// 
			// connectButton
			// 
			this.connectButton.Location = new System.Drawing.Point(261, 35);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(75, 23);
			this.connectButton.TabIndex = 4;
			this.connectButton.Text = "Connect";
			this.connectButton.UseVisualStyleBackColor = true;
			this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// ConnectForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(385, 71);
			this.Controls.Add(this.connectButton);
			this.Controls.Add(this.ipBox);
			this.Controls.Add(this.IPLabel);
			this.Name = "ConnectForm";
			this.Text = "ConnectForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label IPLabel;
		private System.Windows.Forms.TextBox ipBox;
		private System.Windows.Forms.Button connectButton;
	}
}