using System.Windows;

namespace Keepass.Wpf.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void WebDavPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.SettingsViewModel viewModel)
            {
                viewModel.WebDavPassword = WebDavPasswordBox.Password;
            }
        }
    }
}