using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using KeePassLib;
using KeePassLib.Keys;
using KeePassLib.Serialization;
using WebDav;
using Keepass.Wpf.Models;

namespace Keepass.Wpf.Services
{
    public class DatabaseService
    {
        public PwDatabase? LoadedDatabase { get; private set; }

        public bool LoadFromLocalPath(string databasePath, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(databasePath))
                    throw new ArgumentException("Database path cannot be empty");

                if (!File.Exists(databasePath))
                    throw new FileNotFoundException($"Database file not found: {databasePath}");

                LoadedDatabase = new PwDatabase();
                var key = new CompositeKey();
                key.AddUserKey(new KcpPassword(password));

                var ioConnectionInfo = new IOConnectionInfo()
                {
                    Path = databasePath,
                    CredSaveMode = IOCredSaveMode.NoSave
                };

                LoadedDatabase.Open(ioConnectionInfo, key, null);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error loading database: {ex.Message}", ex);
            }
        }

        public async Task<bool> LoadFromWebDav(WebDavConfig webDavConfig, string password)
        {
            try
            {
                if (webDavConfig?.Url == null || webDavConfig?.DatabasePath == null)
                    throw new ArgumentException("WebDAV configuration is incomplete");

                var clientParams = new WebDavClientParams
                {
                    BaseAddress = new Uri(webDavConfig.Url),
                    Credentials = new NetworkCredential(webDavConfig.Username, webDavConfig.Password)
                };
                var client = new WebDavClient(clientParams);

                var response = await client.GetRawFile(webDavConfig.DatabasePath);
                using var stream = response.Stream;
                
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                LoadedDatabase = new PwDatabase();
                var key = new CompositeKey();
                key.AddUserKey(new KcpPassword(password));

                var ioConnectionInfo = new IOConnectionInfo()
                {
                    Path = webDavConfig.DatabasePath,
                    CredSaveMode = IOCredSaveMode.NoSave
                };
                
                var kdbxFile = new KdbxFile(LoadedDatabase);
                kdbxFile.Load(memoryStream, KdbxFormat.Default, null);
                
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error loading database from WebDAV: {ex.Message}", ex);
            }
        }

        public void CloseDatabase()
        {
            LoadedDatabase?.Close();
            LoadedDatabase = null;
        }
    }
}