using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Win32;
using System.Drawing;

namespace TimeEntryPrompter
{
    public partial class TimeEntryForm : Form
    {
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private DateTime startTime;
        private DateTime endTime;
        private int intervalMinutes;

        private const string RegistryPath = @"Software\TimeEntryPrompter";
        private const string PostUrlKey = "POST_URL";

        private NotifyIcon trayIcon = null!;
        private ContextMenuStrip trayMenu = null!;

        private DateTime? lastEntryTime = null;


        public TimeEntryForm()
        {
            InitializeComponent();
            intervalMinutes = 15;
            InitializeTimer();
            InitializeTrayIcon();

            InitializeStartupCheckbox(); // Initialize the startup checkbox state

            Timer_Tick(null, EventArgs.Empty);
            
            StartTimeTextBox_Leave(null, EventArgs.Empty);
            EndTimeTextBox_Leave(null, EventArgs.Empty);
            
            SetStartup();

            this.FormClosing += TimeEntryForm_FormClosing;
            this.Resize += TimeEntryForm_Resize;
        }

        private void InitializeTimer()
        {
            timer.Interval = intervalMinutes * 60 * 1000; // Convert minutes to milliseconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializeTrayIcon()
        {
            // Create a context menu for the tray icon
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Add Entry", null, OnAddEntry); // New "Add Entry" option
            trayMenu.Items.Add("Open", null, OnOpen);
            trayMenu.Items.Add("Exit", null, OnExit);

            trayIcon = new NotifyIcon
            {
                Text = "Time Entry Prompter",
                Icon = new Icon(SystemIcons.Application, 40, 40),
                ContextMenuStrip = trayMenu,
                Visible = true // Ensure the tray icon is visible on startup
            };

            trayIcon.DoubleClick += TrayIcon_DoubleClick;
        }

                private void InitializeStartupCheckbox()
        {
            try
            {
        using (RegistryKey? rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", writable: false))
                {
                    if (rk != null)
                    {
                        string? value = rk.GetValue("TimeEntryPrompter") as string;
                        startupCheckbox.Checked = !string.IsNullOrEmpty(value);
                    }
                    else
                    {
                        startupCheckbox.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accessing registry to determine startup setting: {ex.Message}");
                startupCheckbox.Checked = false;
            }
        }

        private void StartupCheckbox_CheckedChanged(object? sender, EventArgs e)
        {
            if (startupCheckbox.Checked)
            {
                SetStartup();
            }
            else
            {
                RemoveStartup();
            }
        }

        private void SetStartup()
        {
            try
            {
                using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run"))
                {
                    if (rk != null)
                    {
                        string exePath = Application.ExecutablePath;
                        rk.SetValue("TimeEntryPrompter", exePath);
                    }
                    else
                    {
                        MessageBox.Show("Failed to create or open the registry key for setting startup.");
                        startupCheckbox.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to set startup: {ex.Message}");
                startupCheckbox.Checked = false;
            }
        }

        private void RemoveStartup()
        {
            try
            {
                using (RegistryKey? rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", writable: true))
                {
                    if (rk != null)
                    {
                        rk.DeleteValue("TimeEntryPrompter", false);
                    }
                    else
                    {
                        MessageBox.Show("Failed to open the registry key for removing startup.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to remove startup: {ex.Message}");
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (now.TimeOfDay >= TimeSpan.FromHours(8) && now.TimeOfDay < TimeSpan.FromHours(17))
            {
                endTimeTextBox.Text = now.ToString("HHmm");
                startTimeTextBox.Text = now.AddMinutes(-intervalMinutes).ToString("HHmm");

                if (!this.Visible)
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    trayIcon.Visible = false;
                }
            }
        }

        /// <summary>
        /// Attempts to parse the input time string into a DateTime object.
        /// Automatically prepends a '0' if the input is 3 digits.
        /// </summary>
        /// <param name="input">The time string input by the user.</param>
        /// <param name="time">The parsed DateTime object.</param>
        /// <returns>True if parsing is successful; otherwise, false.</returns>
        private bool TryParseTime(string input, out DateTime time)
        {
            time = DateTime.MinValue;
            string normalizedInput = input.Trim();

            // Prepend '0' if input is 3 digits
            if (normalizedInput.Length == 3 && normalizedInput.All(char.IsDigit))
            {
                normalizedInput = "0" + normalizedInput;
            }

            // Define possible formats
            string[] formats = { "HHmm", "Hmm", "hhmm tt", "hmm tt", "h:mm tt", "hh:mm tt" };

            return DateTime.TryParseExact(
                normalizedInput, 
                formats, 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.AssumeLocal | DateTimeStyles.None, 
                out time);
        }

        private void TimeEntryForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                trayIcon.Visible = true;
                ShowBalloonTip("Time Entry Prompter minimized to tray.");
            }
        }

        private void TimeEntryForm_Resize(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                trayIcon.Visible = true;
                ShowBalloonTip("Time Entry Prompter minimized to tray.");
            }
        }

        /// <summary>
        /// Event handler for the Set End Time button click.
        /// Sets the End Time textbox to the current time in HHmm format.
        /// </summary>
        private void SetEndTimeButton_Click(object? sender, EventArgs e)
        {
            endTimeTextBox.Text = DateTime.Now.ToString("HHmm");
        }

        private void ShowBalloonTip(string message)
        {
            trayIcon.BalloonTipTitle = "Time Entry Prompter";
            trayIcon.BalloonTipText = message;
            trayIcon.ShowBalloonTip(3000);
        }

        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            DateTime today = DateTime.Today;

            // Parse Start Time
            if (!TryParseTime(startTimeTextBox.Text, out startTime))
            {
                MessageBox.Show("Invalid Start Time. Please enter time in HHmm format (e.g., 0830 or 830).");
                return;
            }

            // Parse End Time
            if (!TryParseTime(endTimeTextBox.Text, out endTime))
            {
                MessageBox.Show("Invalid End Time. Please enter time in HHmm format (e.g., 1545 or 545).");
                return;
            }

            if (endTime <= startTime)
            {
                MessageBox.Show("End Time must be after Start Time.");
                return;
            }

                // Convert Start Time and End Time to UTC
            DateTime utcStartTime = startTime.ToUniversalTime();
            DateTime utcEndTime = endTime.ToUniversalTime();

            // Retrieve Ticket Number or Work Type
            string ticketOrWorkType = ticketTextBox.Text.Trim();
            // No longer required to be non-empty
            // Optional: Add validation if specific formats or work types are required

            /*
             * Example Validation (Optional):
             * If you have specific work types, you can validate against them.
             * Uncomment and modify as needed.
             *
             * string[] validWorkTypes = { "Admin", "Internal", "Meeting" };
             * if (!string.IsNullOrEmpty(ticketOrWorkType) && !validWorkTypes.Contains(ticketOrWorkType, StringComparer.OrdinalIgnoreCase))
             * {
             *     MessageBox.Show("Invalid Work Type. Please enter a valid work type or leave the field empty.");
             *     return;
             * }
             */
    
            // Calculate time difference if this is an ad hoc entry
            string? timeDifference = null;
            if (lastEntryTime.HasValue)
            {
                TimeSpan difference = startTime - lastEntryTime.Value;
                if (difference.TotalMinutes < intervalMinutes)
                {
                    timeDifference = difference.ToString(@"hh\:mm\:ss");
                }
            }

            var data = new
            {
                StartTime = utcStartTime.ToString("o"), // ISO 8601 format in UTC
                EndTime = utcEndTime.ToString("o"),   
                Username = usernameTextBox?.Text ?? string.Empty,
                TicketOrWorkType = string.IsNullOrEmpty(ticketOrWorkType) ? null : ticketOrWorkType,
                Notes = notesTextBox?.Text ?? string.Empty,
                TimeZone = TimeZoneInfo.Local.Id,
                UtcOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).ToString("hh\\:mm"),
                TimeDifference = timeDifference
            };
            string json = System.Text.Json.JsonSerializer.Serialize(data);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
            
                    string? postUrl = GetPostUrlFromRegistry();
                    if (string.IsNullOrEmpty(postUrl))
                    {
                        MessageBox.Show("POST_URL is not set in the registry.");
                        PromptForPostUrl();
                        return;
                    }

                    var response = await client.PostAsync(postUrl, content);
                    response.EnsureSuccessStatusCode();
                }

                MessageBox.Show("Time entry submitted!");
                notesTextBox?.Clear();
                ticketTextBox?.Clear(); // Clear Ticket Number / Work Type field after submission
                this.Hide();
                trayIcon.Visible = true; // Ensure tray icon is visible after saving
                ShowBalloonTip("Time Entry Prompter minimized to tray.");

                lastEntryTime = endTime; // Update the last entry time to the end time of the current entry
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to submit time entry: {ex.Message}");
            }
        }

        private void StartTimeTextBox_Leave(object? sender, EventArgs e)
        {
            if (TryParseTime(startTimeTextBox.Text, out DateTime time))
            {
                startTimeTextBox.Text = time.ToString("h:mm tt", CultureInfo.InvariantCulture);
            }
            else
            {
                MessageBox.Show("Invalid Start Time format. Please enter time in HHmm format (e.g., 0830 or 830).");
                startTimeTextBox.Focus();
            }
        }

        private void StartTimeTextBox_Enter(object? sender, EventArgs e)
        {
            if (DateTime.TryParseExact(startTimeTextBox.Text, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time))
            {
                startTimeTextBox.Text = time.ToString("HHmm");
            }
        }

        private void EndTimeTextBox_Leave(object? sender, EventArgs e)
        {
            if (TryParseTime(endTimeTextBox.Text, out DateTime time))
            {
                endTimeTextBox.Text = time.ToString("h:mm tt", CultureInfo.InvariantCulture);
            }
            else
            {
                MessageBox.Show("Invalid End Time format. Please enter time in HHmm format (e.g., 1545 or 545).");
                endTimeTextBox.Focus();
            }
        }

        private void EndTimeTextBox_Enter(object? sender, EventArgs e)
        {
            if (DateTime.TryParseExact(endTimeTextBox.Text, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time))
            {
                endTimeTextBox.Text = time.ToString("HHmm");
            }
        }

        private void TimeTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private string? GetPostUrlFromRegistry()
        {
            try
            {
                using (RegistryKey? rk = Registry.CurrentUser.OpenSubKey(RegistryPath, writable: false))
                {
                    if (rk != null)
                    {
                        return rk.GetValue(PostUrlKey) as string;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading POST_URL from registry: {ex.Message}");
            }
            return null;
        }

        private void SetPostUrlInRegistry(string postUrl)
        {
            try
            {
                using (RegistryKey? rk = Registry.CurrentUser.CreateSubKey(RegistryPath))
                {
                    if (rk != null)
                    {
                        rk.SetValue(PostUrlKey, postUrl, RegistryValueKind.String);
                    }
                }
                MessageBox.Show("POST_URL has been set successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing POST_URL to registry: {ex.Message}");
            }
        }

        private void PromptForPostUrl()
        {
            using (PostUrlForm prompt = new PostUrlForm())
            {
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    string postUrl = prompt.PostUrl.Trim();
                    if (IsValidUrl(postUrl))
                    {
                        SetPostUrlInRegistry(postUrl);
                    }
                    else
                    {
                        MessageBox.Show("Invalid URL format. Please enter a valid URL.");
                        PromptForPostUrl();
                    }
                }
                else
                {
                    MessageBox.Show("POST_URL is required for the application to function.");
                    Application.Exit();
                }
            }
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? temp);
        }

        private void OnAddEntry(object? sender, EventArgs e) // Method for ad hoc entry
        {
            // Set Start Time to the End Time of the last entry, if available
            if (lastEntryTime.HasValue)
            {
                startTimeTextBox.Text = lastEntryTime.Value.ToString("HHmm");
            }
            else
            {
                // If there's no previous entry, default to the current time minus intervalMinutes
                startTimeTextBox.Text = DateTime.Now.AddMinutes(-intervalMinutes).ToString("HHmm");
            }

            // Set End Time to the current time
            endTimeTextBox.Text = DateTime.Now.ToString("HHmm");

            // Show the form and hide the tray icon
            this.Show();
            this.WindowState = FormWindowState.Normal;
            trayIcon.Visible = false;
        }

        
        private void OnOpen(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            trayIcon.Visible = false;
        }

        private void TrayIcon_DoubleClick(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            trayIcon.Visible = false;
        }

        private void OnExit(object? sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            trayIcon.Dispose();
            trayMenu.Dispose();
        }
    }
} 