using System;
using System.IO;
using System.Text.Json;
using Keepass.Wpf.Models;

namespace Keepass.Wpf.Services
{
    public class ConfigurationService
    {
        private readonly string _configPath = @"C:\temp\config.json";

        public UserConfig? LoadConfig()
        {
            try
            {
                if (File.Exists(_configPath))
                {
                    string jsonContent = File.ReadAllText(_configPath);
                    return JsonSerializer.Deserialize<UserConfig>(jsonContent);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error loading config: {ex.Message}", ex);
            }
        }

        public void SaveConfig(UserConfig config)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_configPath)!);
                string jsonContent = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_configPath, jsonContent);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving config: {ex.Message}", ex);
            }
        }
    }
}