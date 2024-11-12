namespace TimeEntryPrompter
{
    partial class PostUrlForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label instructionLabel;
        private System.Windows.Forms.TextBox postUrlTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.instructionLabel = new System.Windows.Forms.Label();
            this.postUrlTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // instructionLabel
            // 
            this.instructionLabel.AutoSize = true;
            this.instructionLabel.Location = new System.Drawing.Point(12, 15);
            this.instructionLabel.Name = "instructionLabel";
            this.instructionLabel.Size = new System.Drawing.Size(248, 17);
            this.instructionLabel.TabIndex = 0;
            this.instructionLabel.Text = "Please enter the POST URL for submissions:";
            // 
            // postUrlTextBox
            // 
            this.postUrlTextBox.Location = new System.Drawing.Point(15, 35);
            this.postUrlTextBox.Name = "postUrlTextBox";
            this.postUrlTextBox.Size = new System.Drawing.Size(400, 22);
            this.postUrlTextBox.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(259, 70);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 30);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(340, 70);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 30);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // PostUrlForm
            // 
            this.ClientSize = new System.Drawing.Size(430, 112);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.postUrlTextBox);
            this.Controls.Add(this.instructionLabel);
            this.Name = "PostUrlForm";
            this.Text = "Set POST URL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
} 