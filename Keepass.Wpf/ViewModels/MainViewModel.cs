using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using KeePassLib;
using Keepass.Wpf.Models;
using Keepass.Wpf.Services;

namespace Keepass.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;
        private readonly IconService _iconService;
        private readonly ClipboardService _clipboardService;
        private readonly PwDatabase _database;

        private string _searchText = string.Empty;
        private GroupModel? _selectedGroup;
        private EntryModel? _selectedEntry;
        private ObservableCollection<EntryModel> _allEntries = new();

        public MainViewModel(DatabaseService databaseService, PwDatabase database)
        {
            _databaseService = databaseService;
            _database = database;
            _iconService = new IconService();
            _clipboardService = new ClipboardService();

            Groups = new ObservableCollection<GroupModel>();
            Entries = new ObservableCollection<EntryModel>();

            CopyUsernameCommand = new RelayCommand(() => CopyToClipboard(SelectedEntry?.Username), 
                () => !string.IsNullOrEmpty(SelectedEntry?.Username));
            CopyPasswordCommand = new RelayCommand(() => CopyToClipboard(SelectedEntry?.Password), 
                () => !string.IsNullOrEmpty(SelectedEntry?.Password));
            AddEntryCommand = new RelayCommand(AddEntry);

            LoadDatabaseData();
        }

        public ObservableCollection<GroupModel> Groups { get; }
        public ObservableCollection<EntryModel> Entries { get; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterEntries();
                }
            }
        }

        public GroupModel? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                if (SetProperty(ref _selectedGroup, value))
                {
                    LoadEntriesForGroup(value);
                }
            }
        }

        public EntryModel? SelectedEntry
        {
            get => _selectedEntry;
            set
            {
                SetProperty(ref _selectedEntry, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand CopyUsernameCommand { get; }
        public ICommand CopyPasswordCommand { get; }
        public ICommand AddEntryCommand { get; }

        private void LoadDatabaseData()
        {
            LoadGroups();
            LoadAllEntries();
            
            // Auto-select "All Items" if available
            var allItemsGroup = Groups.FirstOrDefault(g => g.IsSpecialGroup && g.SpecialGroupType == "ALL_ITEMS");
            if (allItemsGroup != null)
            {
                SelectedGroup = allItemsGroup;
            }
        }

        private void LoadGroups()
        {
            Groups.Clear();

            // Add special "All Items" group first
            int totalCount = GetTotalNonRecycledEntryCount();
            var allItemsGroup = new GroupModel
            {
                Name = $"All Items ({totalCount})",
                IsSpecialGroup = true,
                SpecialGroupType = "ALL_ITEMS",
                EntryCount = totalCount
            };
            Groups.Add(allItemsGroup);

            // Add actual groups from database
            if (_database.RootGroup != null)
            {
                AddGroupsToCollection(_database.RootGroup, Groups, false);
            }

            // Add recycle bin at the bottom if it exists
            var recycleBin = GetRecycleBinGroup();
            if (recycleBin != null)
            {
                int recycleBinCount = GetGroupEntryCount(recycleBin, true);
                var recycleBinGroup = new GroupModel
                {
                    Name = $"🗑️ Recycle Bin ({recycleBinCount})",
                    OriginalGroup = recycleBin,
                    EntryCount = recycleBinCount,
                    IsSpecialGroup = true,
                    SpecialGroupType = "RECYCLE_BIN"
                };
                Groups.Add(recycleBinGroup);
            }
        }

        private void AddGroupsToCollection(PwGroup group, ObservableCollection<GroupModel> collection, bool includeRecycleBin = false)
        {
            if (group == null) return;

            if (group != _database.RootGroup && (includeRecycleBin || !IsRecycleBin(group)))
            {
                int groupCount = GetGroupEntryCount(group, includeRecycleBin);
                var groupModel = new GroupModel
                {
                    Name = $"{group.Name} ({groupCount})",
                    OriginalGroup = group,
                    EntryCount = groupCount
                };

                collection.Add(groupModel);

                foreach (PwGroup childGroup in group.Groups)
                {
                    AddGroupsToCollection(childGroup, groupModel.Children, includeRecycleBin);
                }
            }
            else if (group == _database.RootGroup)
            {
                foreach (PwGroup childGroup in group.Groups)
                {
                    AddGroupsToCollection(childGroup, collection, includeRecycleBin);
                }
            }
        }

        private void LoadAllEntries()
        {
            _allEntries.Clear();

            if (_database.RootGroup == null) return;

            foreach (PwGroup group in _database.RootGroup.GetFlatGroupList())
            {
                if (IsRecycleBin(group)) continue;
                LoadEntriesFromGroup(group, _allEntries);
            }
        }

        private void LoadEntriesForGroup(GroupModel? groupModel)
        {
            Entries.Clear();

            if (groupModel == null) return;

            if (groupModel.IsSpecialGroup && groupModel.SpecialGroupType == "ALL_ITEMS")
            {
                foreach (var entry in _allEntries)
                {
                    Entries.Add(entry);
                }
            }
            else if (groupModel.OriginalGroup != null)
            {
                LoadEntriesFromGroup(groupModel.OriginalGroup, Entries);
            }

            FilterEntries();
            
            if (Entries.Count > 0)
            {
                SelectedEntry = Entries[0];
            }
        }

        private void LoadEntriesFromGroup(PwGroup group, ObservableCollection<EntryModel> targetCollection)
        {
            if (group?.Entries == null) return;

            foreach (PwEntry entry in group.Entries)
            {
                if (entry.Expires && entry.ExpiryTime <= DateTime.Now) continue;

                var entryModel = new EntryModel
                {
                    Name = entry.Strings.ReadSafe(PwDefs.TitleField),
                    Username = entry.Strings.ReadSafe(PwDefs.UserNameField),
                    Password = entry.Strings.ReadSafe(PwDefs.PasswordField),
                    Url = entry.Strings.ReadSafe(PwDefs.UrlField),
                    Notes = entry.Strings.ReadSafe(PwDefs.NotesField),
                    IconSource = _iconService.GetEntryIcon(entry, _database),
                    OriginalEntry = entry
                };

                targetCollection.Add(entryModel);
            }
        }

        private void FilterEntries()
        {
            if (SelectedGroup == null) return;

            var allGroupEntries = new ObservableCollection<EntryModel>();
            
            if (SelectedGroup.IsSpecialGroup && SelectedGroup.SpecialGroupType == "ALL_ITEMS")
            {
                foreach (var entry in _allEntries)
                {
                    allGroupEntries.Add(entry);
                }
            }
            else if (SelectedGroup.OriginalGroup != null)
            {
                LoadEntriesFromGroup(SelectedGroup.OriginalGroup, allGroupEntries);
            }

            Entries.Clear();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                foreach (var entry in allGroupEntries)
                {
                    Entries.Add(entry);
                }
            }
            else
            {
                var filtered = allGroupEntries.Where(entry =>
                    entry.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    entry.Username.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);

                foreach (var entry in filtered)
                {
                    Entries.Add(entry);
                }
            }

            if (Entries.Count > 0)
            {
                SelectedEntry = Entries[0];
            }
        }

        private void CopyToClipboard(string? content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                _clipboardService.CopyToClipboardWithTimer(content);
            }
        }

        private void AddEntry()
        {
            // Placeholder for add entry functionality
            System.Windows.MessageBox.Show("Add new entry functionality will be implemented here.", 
                "Add Entry", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private int GetTotalNonRecycledEntryCount()
        {
            if (_database.RootGroup == null) return 0;
            
            int count = 0;
            foreach (PwGroup group in _database.RootGroup.GetFlatGroupList())
            {
                if (IsRecycleBin(group)) continue;
                count += GetActiveEntriesCount(group);
            }
            return count;
        }
        
        private int GetGroupEntryCount(PwGroup group, bool includeSubgroups = false)
        {
            if (group?.Entries == null) return 0;
            
            int count = GetActiveEntriesCount(group);
            
            if (includeSubgroups)
            {
                foreach (PwGroup subGroup in group.Groups)
                {
                    count += GetGroupEntryCount(subGroup, true);
                }
            }
            
            return count;
        }
        
        private int GetActiveEntriesCount(PwGroup group)
        {
            if (group?.Entries == null) return 0;
            
            return group.Entries.Count(entry => 
                !entry.Expires || entry.ExpiryTime > DateTime.Now);
        }

        private bool IsRecycleBin(PwGroup group)
        {
            return group.Uuid.Equals(_database.RecycleBinUuid);
        }

        private PwGroup? GetRecycleBinGroup()
        {
            if (_database.RootGroup == null) return null;
            return _database.RootGroup.FindGroup(_database.RecycleBinUuid, true);
        }
    }
}