namespace LANMessenger {
	partial class MessengerForm {
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
			this.conversationBox = new System.Windows.Forms.TextBox();
			this.sendButton = new System.Windows.Forms.Button();
			this.messageBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// conversationBox
			// 
			this.conversationBox.Enabled = false;
			this.conversationBox.Location = new System.Drawing.Point(12, 44);
			this.conversationBox.Multiline = true;
			this.conversationBox.Name = "conversationBox";
			this.conversationBox.Size = new System.Drawing.Size(628, 180);
			this.conversationBox.TabIndex = 11;
			// 
			// sendButton
			// 
			this.sendButton.Location = new System.Drawing.Point(579, 242);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(61, 54);
			this.sendButton.TabIndex = 10;
			this.sendButton.Text = "Send";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
			// 
			// messageBox
			// 
			this.messageBox.Location = new System.Drawing.Point(12, 242);
			this.messageBox.Multiline = true;
			this.messageBox.Name = "messageBox";
			this.messageBox.Size = new System.Drawing.Size(561, 54);
			this.messageBox.TabIndex = 9;
			// 
			// MessengerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(649, 308);
			this.Controls.Add(this.conversationBox);
			this.Controls.Add(this.sendButton);
			this.Controls.Add(this.messageBox);
			this.Name = "MessengerForm";
			this.Text = "MessengerForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox conversationBox;
		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.TextBox messageBox;
	}
}