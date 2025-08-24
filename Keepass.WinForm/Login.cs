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
    private UserConfig? m_userConfig;
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
                m_userConfig = JsonSerializer.Deserialize<UserConfig>(jsonContent);
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage($"Error loading config: {ex.Message}");
        }
        
        UpdateDatabaseFileDisplay();
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
        HideErrorMessage();
        
        string password = passwordTextBox.Text;
        if (m_userConfig == null)
        {
            ShowErrorMessage("No configuration found. Please check settings.");
            return;
        }
        if (string.IsNullOrEmpty(password))
        {
            ShowErrorMessage("Please enter a password.");
            return;
        }

        var canOpenDb = false;
        if (!string.IsNullOrEmpty(m_userConfig.LocalDatabasePath))
            canOpenDb = LoadKdbxFromLocalPath(password);
        else if (m_userConfig.WebDavConfiguration != null)
            canOpenDb = await LoadKdbxFromWebDav(password);

        if (canOpenDb && LoadedDatabase != null)
        {
            // Hide login form and show main form with loaded database
            this.Hide();
            Main mainForm = new Main(LoadedDatabase);
            mainForm.Show();
        }

    }

    private bool LoadKdbxFromLocalPath(string password)
    {
        try
        {
            if (string.IsNullOrEmpty(m_userConfig?.LocalDatabasePath))
            {
                ShowErrorMessage("Local database path is not configured.");
                return false;
            }

            if (!File.Exists(m_userConfig.LocalDatabasePath))
            {
                ShowErrorMessage($"Database file not found: {m_userConfig.LocalDatabasePath}");
                return false;
            }

            // Load the KeePass database from local file
            LoadedDatabase = new PwDatabase();
            var key = new CompositeKey();
            key.AddUserKey(new KcpPassword(password));

            var ioConnectionInfo = new IOConnectionInfo()
            {
                Path = m_userConfig.LocalDatabasePath,
                CredSaveMode = IOCredSaveMode.NoSave
            };

            LoadedDatabase.Open(ioConnectionInfo, key, null);

            return true;
        }
        catch (Exception ex)
        {
            ShowErrorMessage($"Error loading database: {ex.Message}");
            return false;
        }
    }

    private async Task<bool> LoadKdbxFromWebDav(string password)
    {
        try
        {
            var webDavConfig = m_userConfig?.WebDavConfiguration;
            if (webDavConfig?.Url == null || webDavConfig?.DatabasePath == null)
            {
                ShowErrorMessage("WebDAV configuration is incomplete.");
                return false;
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
            var kdbxFile = new KdbxFile(LoadedDatabase);
            kdbxFile.Load(memoryStream, KdbxFormat.Default, null);
            
            return true;
        }
        catch (Exception ex)
        {
            ShowErrorMessage($"Error loading database from WebDAV: {ex.Message}");
            return false;
        }
    }

    private void settingsButton_Click(object sender, EventArgs e)
    {
        var settingsForm = new SettingsForm(m_userConfig);
        if (settingsForm.ShowDialog() == DialogResult.OK)
        {
            m_userConfig = settingsForm.UserConfig;
            SaveUserConfig();
            UpdateDatabaseFileDisplay();
        }
    }

    private void SaveUserConfig()
    {
        try
        {
            string configPath = @"C:\temp\config.json";
            string jsonContent = JsonSerializer.Serialize(m_userConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, jsonContent);
        }
        catch (Exception ex)
        {
            ShowErrorMessage($"Error saving config: {ex.Message}");
        }
    }

    private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = true;
            loginButton_Click(sender, e);
        }
    }

    private void passwordTextBox_TextChanged(object sender, EventArgs e)
    {
        HideErrorMessage();
    }

    private void ShowErrorMessage(string message)
    {
        errorMessageLabel.Text = message;
        errorMessageLabel.Visible = true;
    }

    private void HideErrorMessage()
    {
        errorMessageLabel.Visible = false;
        errorMessageLabel.Text = string.Empty;
    }

    private void UpdateDatabaseFileDisplay()
    {
        if (m_userConfig == null)
        {
            databaseFileLabel.Text = @"No database configured";
            return;
        }

        if (!string.IsNullOrEmpty(m_userConfig.LocalDatabasePath))
        {
            string fileName = Path.GetFileName(m_userConfig.LocalDatabasePath);
            databaseFileLabel.Text = $@"Local: {fileName}";
        }
        else if (m_userConfig.WebDavConfiguration?.DatabasePath != null)
        {
            string fileName = Path.GetFileName(m_userConfig.WebDavConfiguration.DatabasePath);
            string serverName = ExtractServerName(m_userConfig.WebDavConfiguration.Url);
            databaseFileLabel.Text = $@"WebDAV ({serverName}): {fileName}";
        }
        else
        {
            databaseFileLabel.Text = @"No database file configured";
        }
    }

    private string ExtractServerName(string? url)
    {
        if (string.IsNullOrEmpty(url))
            return "Unknown";

        try
        {
            var uri = new Uri(url);
            return uri.Host;
        }
        catch
        {
            return "Unknown";
        }
    }
}