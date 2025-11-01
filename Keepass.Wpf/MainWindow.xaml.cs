using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;
using Keepass.Wpf.ViewModels;
using Keepass.Wpf.Models;

namespace Keepass.Wpf;

public partial class MainWindow : Window
{
    private TaskbarIcon? _notifyIcon;

    public MainWindow()
    {
        InitializeComponent();
        InitializeNotifyIcon();
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel && sender is PasswordBox passwordBox)
        {
            // Set password display from selected entry
            if (viewModel.SelectedEntry != null && passwordBox.Password != viewModel.SelectedEntry.Password)
            {
                passwordBox.Password = viewModel.SelectedEntry.Password;
            }
        }
    }

    private void MainWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.PropertyChanged += (s, args) =>
            {
                if (args.PropertyName == nameof(MainViewModel.SelectedEntry))
                {
                    UpdatePasswordDisplay();
                }
            };
        }
    }

    private void UpdatePasswordDisplay()
    {
        if (DataContext is MainViewModel viewModel && viewModel.SelectedEntry != null)
        {
            // Find the password box and update it
            var passwordBox = FindName("EntryPasswordBox") as PasswordBox;
            if (passwordBox != null)
            {
                passwordBox.Password = viewModel.SelectedEntry.Password;
            }
        }
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (DataContext is MainViewModel viewModel && e.NewValue is GroupModel selectedGroup)
        {
            viewModel.SelectedGroup = selectedGroup;
        }
    }

    private void InitializeNotifyIcon()
    {
        _notifyIcon = new TaskbarIcon
        {
            Icon = new System.Drawing.Icon("favicon.ico"),
            ToolTipText = "KeeZ - Password Manager"
        };
        
        _notifyIcon.TrayLeftMouseDown += (s, e) => RestoreFromTray();
        _notifyIcon.TrayMouseDoubleClick += (s, e) => RestoreFromTray();
    }

    private void MainWindow_StateChanged(object sender, EventArgs e)
    {
        if (WindowState == WindowState.Minimized)
        {
            Hide();
            _notifyIcon?.ShowBalloonTip("KeeZ", "Application minimized to tray", BalloonIcon.Info);
        }
    }

    private void MainWindow_Closing(object sender, CancelEventArgs e)
    {
        base.Close();
        // e.Cancel = true;
        // Hide();
        // _notifyIcon?.ShowBalloonTip("KeeZ", "Application minimized to tray", BalloonIcon.Info);
    }

    private void RestoreFromTray()
    {
        Show();
        WindowState = WindowState.Normal;
        Activate();
        Focus();
    }

    protected override void OnClosed(EventArgs e)
    {
        _notifyIcon?.Dispose();
        base.OnClosed(e);
    }
}