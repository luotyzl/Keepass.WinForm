using System.Text.Json;

namespace Keepass.WinForm;

public partial class Login : Form
{
    private WebDavConfig? webDavConfig;

    public Login()
    {
        InitializeComponent();
        LoadWebDavConfig();
    }

    private void LoadWebDavConfig()
    {
        try
        {
            string configPath = @"C:\temp\config.json";
            if (File.Exists(configPath))
            {
                string jsonContent = File.ReadAllText(configPath);
                webDavConfig = JsonSerializer.Deserialize<WebDavConfig>(jsonContent);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading WebDAV config: {ex.Message}", "Configuration Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    public class WebDavConfig
    {
        public string? Url { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? DatabasePath { get; set; }
    }

    private void loginButton_Click(object sender, EventArgs e)
    {
        string password = passwordTextBox.Text;
        
        if (string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter a password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (webDavConfig != null)
        {
            
        }
        else
        {
            MessageBox.Show("No WebDAV configuration found.", "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Hide login form and show main form
        this.Hide();
        Main mainForm = new Main();
        mainForm.Show();
    }
}