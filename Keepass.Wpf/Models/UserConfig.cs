namespace Keepass.Wpf.Models
{
    public class UserConfig
    {
        public string? LocalDatabasePath { get; set; }
        public WebDavConfig? WebDavConfiguration { get; set; }
    }

    public class WebDavConfig
    {
        public string? Url { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? DatabasePath { get; set; }
    }
}