using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Keepass.Wpf.Models;
using Keepass.Wpf.Services;
using Keepass.Wpf.Views;

namespace Keepass.Wpf.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly ConfigurationService _configService;
        private readonly DatabaseService _databaseService;
        private UserConfig? _userConfig;
        private string _password = string.Empty;
        private string _databaseFileDisplay = "No database configured";
        private string _errorMessage = string.Empty;
        private bool _isErrorVisible;
        private bool _showOpenHint = true;

        public LoginViewModel()
        {
            _configService = new ConfigurationService();
            _databaseService = new DatabaseService();

            LoginCommand = new RelayCommand(async () => await LoginAsync(), () => !string.IsNullOrEmpty(Password));
            SettingsCommand = new RelayCommand(ShowSettings);
            OpenDatabaseCommand = new RelayCommand(OpenDatabase);

            LoadUserConfig();
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    IsErrorVisible = false;
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string DatabaseFileDisplay
        {
            get => _databaseFileDisplay;
            set => SetProperty(ref _databaseFileDisplay, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set => SetProperty(ref _isErrorVisible, value);
        }

        public bool ShowOpenHint
        {
            get => _showOpenHint;
            set => SetProperty(ref _showOpenHint, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand OpenDatabaseCommand { get; }

        private void LoadUserConfig()
        {
            try
            {
                _userConfig = _configService.LoadConfig();
                UpdateDatabaseFileDisplay();
            }
            catch (Exception ex)
            {
                ShowError($"Error loading config: {ex.Message}");
            }
        }

        private async Task LoginAsync()
        {
            try
            {
                HideError();
                
                if (_userConfig == null)
                {
                    ShowError("No configuration found. Please check settings.");
                    return;
                }

                bool success = false;
                
                if (!string.IsNullOrEmpty(_userConfig.LocalDatabasePath))
                {
                    success = _databaseService.LoadFromLocalPath(_userConfig.LocalDatabasePath, Password);
                }
                else if (_userConfig.WebDavConfiguration != null)
                {
                    success = await _databaseService.LoadFromWebDav(_userConfig.WebDavConfiguration, Password);
                }

                if (success && _databaseService.LoadedDatabase != null)
                {
                    var mainWindow = new MainWindow();
                    var mainViewModel = new MainViewModel(_databaseService, _databaseService.LoadedDatabase);
                    mainWindow.DataContext = mainViewModel;
                    
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();
                    
                    // Close login window
                    if (Application.Current.Windows.Count > 1)
                    {
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window is LoginWindow)
                            {
                                window.Close();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ShowSettings()
        {
            var settingsWindow = new SettingsWindow();
            var settingsViewModel = new SettingsViewModel(_userConfig);
            settingsWindow.DataContext = settingsViewModel;
            
            if (settingsWindow.ShowDialog() == true)
            {
                _userConfig = settingsViewModel.GetUserConfig();
                SaveUserConfig();
                UpdateDatabaseFileDisplay();
            }
        }

        private void SaveUserConfig()
        {
            try
            {
                if (_userConfig != null)
                {
                    _configService.SaveConfig(_userConfig);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error saving config: {ex.Message}");
            }
        }

        private void OpenDatabase()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "KeePass Database Files (*.kdbx)|*.kdbx|All Files (*.*)|*.*",
                Title = "Select KeePass Database"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Update config with selected file
                if (_userConfig == null)
                {
                    _userConfig = new UserConfig();
                }

                _userConfig.LocalDatabasePath = openFileDialog.FileName;
                SaveUserConfig();
                UpdateDatabaseFileDisplay();
                ShowOpenHint = false;
            }
        }

        private void UpdateDatabaseFileDisplay()
        {
            if (_userConfig == null)
            {
                DatabaseFileDisplay = "No database configured";
                ShowOpenHint = true;
                return;
            }

            if (!string.IsNullOrEmpty(_userConfig.LocalDatabasePath))
            {
                string fileName = System.IO.Path.GetFileName(_userConfig.LocalDatabasePath);
                DatabaseFileDisplay = $"📁 {fileName}";
                ShowOpenHint = false;
            }
            else if (_userConfig.WebDavConfiguration?.DatabasePath != null)
            {
                string fileName = System.IO.Path.GetFileName(_userConfig.WebDavConfiguration.DatabasePath);
                string serverName = ExtractServerName(_userConfig.WebDavConfiguration.Url);
                DatabaseFileDisplay = $"☁ {serverName}: {fileName}";
                ShowOpenHint = false;
            }
            else
            {
                DatabaseFileDisplay = "No database file configured";
                ShowOpenHint = true;
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

        private void ShowError(string message)
        {
            ErrorMessage = message;
            IsErrorVisible = true;
        }

        private void HideError()
        {
            IsErrorVisible = false;
            ErrorMessage = string.Empty;
        }
    }
}