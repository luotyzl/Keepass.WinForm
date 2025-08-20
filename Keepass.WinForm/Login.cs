using System.Text.Json;
using System.Net;
using KeePassLib;
using KeePassLib.Keys;
using KeePassLib.Serialization;
using KeePassLib.Security;
using WebDav;

namespace Keepass.WinForm;

public partial class Login : Form
{
    private UserConfig? _userConfig;
    public PwDatabase? LoadedDatabase { get; private set; }

    public Login()
    {
        InitializeComponent();
        LoadUserConfig();
    }

    private void LoadUserConfig()
    {
        try
        {
            string configPath = @"C:\temp\config.json";
            if (File.Exists(configPath))
            {
                string jsonContent = File.ReadAllText(configPath);
                _userConfig = JsonSerializer.Deserialize<UserConfig>(jsonContent);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading config: {ex.Message}", "Configuration Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    public class UserConfig
    {
        public string? LocalDatabasePath { get; set; }
        public WebDavConfig? WebDavConfiguration { get; set; }
        
        public class WebDavConfig
        {
            public string? Url { get; set; }
            public string? Username { get; set; }
            public string? Password { get; set; }
            public string? DatabasePath { get; set; }
        }
    }

    private async void loginButton_Click(object sender, EventArgs e)
    {
        string password = passwordTextBox.Text;
        
        if (string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter a password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_userConfig != null)
        {
            if (!string.IsNullOrEmpty(_userConfig.LocalDatabasePath))
            {
                await LoadKdbxFromLocalPath(password);
            }
            else if (_userConfig.WebDavConfiguration != null)
            {
                await LoadKdbxFromWebDav(password);
            }
            else
            {
                MessageBox.Show("No database configuration found.", "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        else
        {
            MessageBox.Show("No configuration found.", "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Hide login form and show main form
        this.Hide();
        Main mainForm = new Main();
        mainForm.Show();
    }

    private async Task LoadKdbxFromLocalPath(string password)
    {
        try
        {
            if (string.IsNullOrEmpty(_userConfig?.LocalDatabasePath))
            {
                MessageBox.Show("Local database path is not configured.", "Configuration Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(_userConfig.LocalDatabasePath))
            {
                MessageBox.Show($"Database file not found: {_userConfig.LocalDatabasePath}", "File Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Load the KeePass database from local file
            LoadedDatabase = new PwDatabase();
            var key = new CompositeKey();
            key.AddUserKey(new KcpPassword(password));

            var ioConnectionInfo = new IOConnectionInfo()
            {
                Path = _userConfig.LocalDatabasePath,
                CredSaveMode = IOCredSaveMode.NoSave
            };

            await Task.Run(() =>
            {
                LoadedDatabase.Open(ioConnectionInfo, key, null);
            });

            MessageBox.Show("Database loaded successfully from local path!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading database from local path: {ex.Message}", "Load Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadKdbxFromWebDav(string password)
    {
        try
        {
            var webDavConfig = _userConfig?.WebDavConfiguration;
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

    private void settingsButton_Click(object sender, EventArgs e)
    {
        var settingsForm = new SettingsForm(_userConfig);
        if (settingsForm.ShowDialog() == DialogResult.OK)
        {
            _userConfig = settingsForm.UserConfig;
            SaveUserConfig();
        }
    }

    private void SaveUserConfig()
    {
        try
        {
            string configPath = @"C:\temp\config.json";
            string jsonContent = JsonSerializer.Serialize(_userConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, jsonContent);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving config: {ex.Message}", "Configuration Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}