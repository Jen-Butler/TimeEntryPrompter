using System;
using System.Windows.Forms;

namespace TimeEntryPrompter
{
    public partial class PostUrlForm : Form
    {
        public string PostUrl { get; private set; } = string.Empty;

        public PostUrlForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            PostUrl = postUrlTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
} 