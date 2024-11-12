#nullable enable

namespace TimeEntryPrompter
{
    partial class TimeEntryForm
    {
        #pragma warning disable CS0414
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;
        #pragma warning restore CS0414

        private System.Windows.Forms.TextBox startTimeTextBox = null!;
        private System.Windows.Forms.TextBox endTimeTextBox = null!;
        private System.Windows.Forms.TextBox usernameTextBox = null!;
        private System.Windows.Forms.TextBox notesTextBox = null!;
        private System.Windows.Forms.Button saveButton = null!;
        private System.Windows.Forms.CheckBox startupCheckbox = null!;

        // Label declarations
        private System.Windows.Forms.Label startTimeLabel = null!;
        private System.Windows.Forms.Label endTimeLabel = null!;
        private System.Windows.Forms.Label usernameLabel = null!;
        private System.Windows.Forms.Label notesLabel = null!;
        private System.Windows.Forms.Label ticketLabel = null!;
        private System.Windows.Forms.TextBox ticketTextBox = null!;
        private System.Windows.Forms.Button setEndTimeButton = null!;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code


        private void InitializeComponent()
        {
            this.startTimeTextBox = new System.Windows.Forms.TextBox();
            this.endTimeTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.startupCheckbox = new System.Windows.Forms.CheckBox();

            this.startTimeLabel = new System.Windows.Forms.Label();
            this.endTimeLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.notesLabel = new System.Windows.Forms.Label();
            this.ticketLabel = new System.Windows.Forms.Label();
            this.ticketTextBox = new System.Windows.Forms.TextBox();

            this.SuspendLayout();

            // 
            // startTimeLabel
            // 
            this.startTimeLabel.Location = new System.Drawing.Point(12, 15);
            this.startTimeLabel.Name = "startTimeLabel";
            this.startTimeLabel.Size = new System.Drawing.Size(100, 20);
            this.startTimeLabel.Text = "Start Time:";

            // 
            // startTimeTextBox
            // 
            this.startTimeTextBox.Location = new System.Drawing.Point(12, 38);
            this.startTimeTextBox.Name = "startTimeTextBox";
            this.startTimeTextBox.Size = new System.Drawing.Size(200, 22);
            this.startTimeTextBox.TabIndex = 0;
            this.startTimeTextBox.PlaceholderText = "Enter Start Time (e.g., 1428)";

            // 
            // endTimeLabel
            // 
            this.endTimeLabel.Location = new System.Drawing.Point(12, 70);
            this.endTimeLabel.Name = "endTimeLabel";
            this.endTimeLabel.Size = new System.Drawing.Size(100, 20);
            this.endTimeLabel.Text = "End Time:";

            // 
            // endTimeTextBox
            // 
            this.endTimeTextBox.Location = new System.Drawing.Point(12, 93);
            this.endTimeTextBox.Name = "endTimeTextBox";
            this.endTimeTextBox.Size = new System.Drawing.Size(200, 22);
            this.endTimeTextBox.TabIndex = 1;
            this.endTimeTextBox.PlaceholderText = "Enter End Time (e.g., 1545)";

            // 
            // setEndTimeButton
            // 
            this.setEndTimeButton = new System.Windows.Forms.Button();
            this.setEndTimeButton.Location = new System.Drawing.Point(220, 93);
            this.setEndTimeButton.Name = "setEndTimeButton";
            this.setEndTimeButton.Size = new System.Drawing.Size(75, 23);
            this.setEndTimeButton.TabIndex = 7;
            this.setEndTimeButton.Text = "Set Now";
            this.setEndTimeButton.UseVisualStyleBackColor = true;
            this.setEndTimeButton.Click += new System.EventHandler(this.SetEndTimeButton_Click);

            // 
            // usernameLabel
            // 
            this.usernameLabel.Location = new System.Drawing.Point(12, 125);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(100, 20);
            this.usernameLabel.Text = "Username:";

            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(12, 148);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(200, 22);
            this.usernameTextBox.TabIndex = 2;
            this.usernameTextBox.PlaceholderText = "Enter your username";

            // 
            // notesLabel
            // 
            this.notesLabel.Location = new System.Drawing.Point(12, 180);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(100, 20);
            this.notesLabel.Text = "Notes:";

            // 
            // notesTextBox
            // 
            this.notesTextBox.Location = new System.Drawing.Point(12, 203);
            this.notesTextBox.Multiline = true;
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.Size = new System.Drawing.Size(200, 50);
            this.notesTextBox.TabIndex = 3;
            this.notesTextBox.PlaceholderText = "Enter notes";

            // 
            // ticketLabel
            // 
            this.ticketLabel.Location = new System.Drawing.Point(12, 265);
            this.ticketLabel.Name = "ticketLabel";
            this.ticketLabel.Size = new System.Drawing.Size(150, 20);
            this.ticketLabel.Text = "Ticket # / Work Type:";

            // 
            // ticketTextBox
            // 
            this.ticketTextBox.Location = new System.Drawing.Point(12, 288);
            this.ticketTextBox.Name = "ticketTextBox";
            this.ticketTextBox.Size = new System.Drawing.Size(200, 22);
            this.ticketTextBox.TabIndex = 4;
            this.ticketTextBox.PlaceholderText = "Enter Ticket Number or Work Type";

            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 326);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(200, 30);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save Entry";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);

            // 
            // startupCheckbox
            // 
            this.startupCheckbox.Location = new System.Drawing.Point(12, 362);
            this.startupCheckbox.Name = "startupCheckbox";
            this.startupCheckbox.Size = new System.Drawing.Size(200, 24);
            this.startupCheckbox.TabIndex = 6;
            this.startupCheckbox.Text = "Run at startup";
            this.startupCheckbox.UseVisualStyleBackColor = true;
            this.startupCheckbox.CheckedChanged += new System.EventHandler(this.StartupCheckbox_CheckedChanged);

            // 
            // TimeEntryForm
            // 
            this.ClientSize = new System.Drawing.Size(320, 400);
            this.Controls.Add(this.startTimeLabel);
            this.Controls.Add(this.startTimeTextBox);
            this.Controls.Add(this.endTimeLabel);
            this.Controls.Add(this.endTimeTextBox);
            this.Controls.Add(this.setEndTimeButton);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.notesLabel);
            this.Controls.Add(this.notesTextBox);
            this.Controls.Add(this.ticketLabel);
            this.Controls.Add(this.ticketTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.startupCheckbox);
            this.Name = "TimeEntryForm";
            this.Text = "Time Entry Prompter";
            this.ResumeLayout(false);
            this.PerformLayout();

            // Attach event handlers with updated nullable signatures
            this.startTimeTextBox.Leave += new System.EventHandler(this.StartTimeTextBox_Leave);
            this.startTimeTextBox.Enter += new System.EventHandler(this.StartTimeTextBox_Enter);
            this.startTimeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TimeTextBox_KeyPress);

            // Attach event handlers for EndTimeTextBox
            this.endTimeTextBox.Leave += new System.EventHandler(this.EndTimeTextBox_Leave);
            this.endTimeTextBox.Enter += new System.EventHandler(this.EndTimeTextBox_Enter);
            this.endTimeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TimeTextBox_KeyPress);
        }

        #endregion
    }
} 