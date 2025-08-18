using System.Text.Json;
using System.Net;
using KeePassLib;
using KeePassLib.Keys;
using KeePassLib.Serialization;
using WebDav;

namespace Keepass.WinForm;

public partial class Login : Form
{
    private WebDavConfig? webDavConfig;
    public PwDatabase? LoadedDatabase { get; private set; }

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

    private async void loginButton_Click(object sender, EventArgs e)
    {
        string password = passwordTextBox.Text;
        
        if (string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter a password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (webDavConfig != null)
        {
            await LoadKdbxFromWebDav(password);
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

    private async Task LoadKdbxFromWebDav(string password)
    {
        try
        {
            if (webDavConfig?.Url == null || webDavConfig?.DatabasePath == null)
            {
                MessageBox.Show("WebDAV configuration is incomplete.", "Configuration Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create WebDAV client
            var clientParams = new WebDavClientParams
            {
                BaseAddress = new Uri(webDavConfig.Url),
                Credentials = new NetworkCredential(webDavConfig.Username, webDavConfig.Password)
            };
            var client = new WebDavClient(clientParams);

            // Download the KDBX file from WebDAV
            var response = await client.GetRawFile(webDavConfig.DatabasePath);
            using var stream = response.Stream;
            
            // Create a memory stream to load the database
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            // Load the KeePass database
            LoadedDatabase = new PwDatabase();
            var key = new CompositeKey();
            key.AddUserKey(new KcpPassword(password));

            var ioConnectionInfo = new IOConnectionInfo()
            {
                Path = webDavConfig.DatabasePath,
                CredSaveMode = IOCredSaveMode.NoSave
            };
            
            // Use KdbxFile to load from stream
            var kdbxFile = new KeePassLib.Serialization.KdbxFile(LoadedDatabase);
            kdbxFile.Load(memoryStream, KeePassLib.Serialization.KdbxFormat.Default, null);

            MessageBox.Show("Database loaded successfully from WebDAV!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading database from WebDAV: {ex.Message}", "WebDAV Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}