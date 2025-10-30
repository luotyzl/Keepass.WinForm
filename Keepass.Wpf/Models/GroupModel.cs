using System.Collections.ObjectModel;
using KeePassLib;

namespace Keepass.Wpf.Models
{
    public class GroupModel
    {
        public string Name { get; set; } = string.Empty;
        public int EntryCount { get; set; }
        public PwGroup? OriginalGroup { get; set; }
        public ObservableCollection<GroupModel> Children { get; set; } = new();
        public bool IsSpecialGroup { get; set; }
        public string? SpecialGroupType { get; set; }
    }
}