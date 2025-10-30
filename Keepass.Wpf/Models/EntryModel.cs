using System.Windows.Media;
using KeePassLib;

namespace Keepass.Wpf.Models
{
    public class EntryModel
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public ImageSource? IconSource { get; set; }
        public PwEntry? OriginalEntry { get; set; }
    }
}