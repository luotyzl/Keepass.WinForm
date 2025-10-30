using System;
using System.Windows;
using Serilog;

namespace Keepass.Wpf;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(@"C:\Temp\KeeZ-WPF.log",
                rollingInterval: RollingInterval.Infinite,
                fileSizeLimitBytes: 10 * 1024 * 1024,
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: 1)
            .CreateLogger();

        try
        {
            Log.Information("WPF Application starting up");
            base.OnStartup(e);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Log.CloseAndFlush();
        base.OnExit(e);
    }
}

