using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Keepass.Wpf.Models;

namespace Keepass.Wpf.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _localDatabasePath = string.Empty;
        private string _webDavUrl = string.Empty;
        private string _webDavUsername = string.Empty;
        private string _webDavPassword = string.Empty;
        private string _webDavDatabasePath = string.Empty;

        public SettingsViewModel(UserConfig? userConfig)
        {
            BrowseCommand = new RelayCommand(BrowseForDatabase);
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            
            LoadConfigToViewModel(userConfig);
        }

        public string LocalDatabasePath
        {
            get => _localDatabasePath;
            set => SetProperty(ref _localDatabasePath, value);
        }

        public string WebDavUrl
        {
            get => _webDavUrl;
            set => SetProperty(ref _webDavUrl, value);
        }

        public string WebDavUsername
        {
            get => _webDavUsername;
            set => SetProperty(ref _webDavUsername, value);
        }

        public string WebDavPassword
        {
            get => _webDavPassword;
            set => SetProperty(ref _webDavPassword, value);
        }

        public string WebDavDatabasePath
        {
            get => _webDavDatabasePath;
            set => SetProperty(ref _webDavDatabasePath, value);
        }

        public ICommand BrowseCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private void LoadConfigToViewModel(UserConfig? userConfig)
        {
            if (userConfig == null) return;

            LocalDatabasePath = userConfig.LocalDatabasePath ?? string.Empty;

            if (userConfig.WebDavConfiguration != null)
            {
                WebDavUrl = userConfig.WebDavConfiguration.Url ?? string.Empty;
                WebDavUsername = userConfig.WebDavConfiguration.Username ?? string.Empty;
                WebDavPassword = userConfig.WebDavConfiguration.Password ?? string.Empty;
                WebDavDatabasePath = userConfig.WebDavConfiguration.DatabasePath ?? string.Empty;
            }
        }

        public UserConfig GetUserConfig()
        {
            var config = new UserConfig();

            config.LocalDatabasePath = string.IsNullOrWhiteSpace(LocalDatabasePath) 
                ? null : LocalDatabasePath.Trim();

            config.WebDavConfiguration = new WebDavConfig
            {
                Url = string.IsNullOrWhiteSpace(WebDavUrl) ? null : WebDavUrl.Trim(),
                Username = string.IsNullOrWhiteSpace(WebDavUsername) ? null : WebDavUsername.Trim(),
                Password = string.IsNullOrWhiteSpace(WebDavPassword) ? null : WebDavPassword.Trim(),
                DatabasePath = string.IsNullOrWhiteSpace(WebDavDatabasePath) ? null : WebDavDatabasePath.Trim()
            };

            return config;
        }

        private void BrowseForDatabase()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "KeePass Database Files (*.kdbx)|*.kdbx|All Files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                LocalDatabasePath = openFileDialog.FileName;
            }
        }

        private void Save()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.DialogResult = true;
                    break;
                }
            }
        }

        private void Cancel()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.DialogResult = false;
                    break;
                }
            }
        }
    }
}