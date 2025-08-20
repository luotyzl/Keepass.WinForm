namespace Keepass.WinForm;

public partial class SettingsForm : Form
{
    public Login.UserConfig? UserConfig { get; private set; }

    public SettingsForm(Login.UserConfig? userConfig)
    {
        InitializeComponent();
        UserConfig = userConfig ?? new Login.UserConfig();
        LoadConfigToForm();
    }

    private void LoadConfigToForm()
    {
        if (UserConfig == null) return;

        localDatabasePathTextBox.Text = UserConfig.LocalDatabasePath ?? string.Empty;

        if (UserConfig.WebDavConfiguration != null)
        {
            webDavUrlTextBox.Text = UserConfig.WebDavConfiguration.Url ?? string.Empty;
            webDavUsernameTextBox.Text = UserConfig.WebDavConfiguration.Username ?? string.Empty;
            webDavPasswordTextBox.Text = UserConfig.WebDavConfiguration.Password ?? string.Empty;
            webDavDatabasePathTextBox.Text = UserConfig.WebDavConfiguration.DatabasePath ?? string.Empty;
        }
    }

    private void SaveConfigFromForm()
    {
        UserConfig ??= new Login.UserConfig();

        UserConfig.LocalDatabasePath = string.IsNullOrWhiteSpace(localDatabasePathTextBox.Text) 
            ? null : localDatabasePathTextBox.Text.Trim();

        UserConfig.WebDavConfiguration ??= new Login.UserConfig.WebDavConfig();
        UserConfig.WebDavConfiguration.Url = string.IsNullOrWhiteSpace(webDavUrlTextBox.Text) 
            ? null : webDavUrlTextBox.Text.Trim();
        UserConfig.WebDavConfiguration.Username = string.IsNullOrWhiteSpace(webDavUsernameTextBox.Text) 
            ? null : webDavUsernameTextBox.Text.Trim();
        UserConfig.WebDavConfiguration.Password = string.IsNullOrWhiteSpace(webDavPasswordTextBox.Text) 
            ? null : webDavPasswordTextBox.Text.Trim();
        UserConfig.WebDavConfiguration.DatabasePath = string.IsNullOrWhiteSpace(webDavDatabasePathTextBox.Text) 
            ? null : webDavDatabasePathTextBox.Text.Trim();
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
        SaveConfigFromForm();
        DialogResult = DialogResult.OK;
        Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void browseButton_Click(object sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Filter = "KeePass Database Files (*.kdbx)|*.kdbx|All Files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            localDatabasePathTextBox.Text = openFileDialog.FileName;
        }
    }
}