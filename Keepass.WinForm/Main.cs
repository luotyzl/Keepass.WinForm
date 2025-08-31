using System.ComponentModel;

namespace Keepass.WinForm;

public partial class Main : Form
{
    private List<ListViewItem> allEntries = new List<ListViewItem>();
    private ImageList entryIcons = new ImageList();
    private KeePassLib.PwDatabase? database;

    public Main()
    {
        InitializeComponent();
        InitializeIcons();
        InitializeListView();
        InitializeTreeView(); // Initialize with sample data
    }

    public Main(KeePassLib.PwDatabase pwDatabase)
    {
        InitializeComponent();
        InitializeIcons();
        InitializeListView();
        database = pwDatabase;
        LoadDatabaseGroups(); // Load actual groups from database
        LoadDatabaseEntries(); // Load all entries initially
    }

    private void InitializeTreeView()
    {
        groupsTreeView.BeginUpdate();
        
        TreeNode rootNode = new TreeNode("Database");
        TreeNode generalNode = new TreeNode("General");
        TreeNode workNode = new TreeNode("Work");
        TreeNode personalNode = new TreeNode("Personal");
        
        rootNode.Nodes.Add(generalNode);
        rootNode.Nodes.Add(workNode);
        rootNode.Nodes.Add(personalNode);
        
        groupsTreeView.Nodes.Add(rootNode);
        rootNode.Expand();
        
        groupsTreeView.EndUpdate();
    }

    private void InitializeIcons()
    {
        entryIcons = new ImageList();
        entryIcons.ImageSize = new Size(16, 16);
        entryIcons.ColorDepth = ColorDepth.Depth32Bit;
        
        entriesListView.SmallImageList = entryIcons;
    }

    private void InitializeListView()
    {
        entriesListView.FullRowSelect = true;
        
        // Configure for Details view with 3 columns
        entriesListView.View = View.Details;
        entriesListView.GridLines = true;
        
        // Add columns: Icon, Name, Username
        entriesListView.Columns.Add("Icon", 60);
        entriesListView.Columns.Add("Name", 200);
        entriesListView.Columns.Add("Username", 150);
    }
    
    private void searchTextBox_TextChanged(object sender, EventArgs e)
    {
        FilterEntries(searchTextBox.Text);
    }

    private void FilterEntries(string searchText)
    {
        entriesListView.Items.Clear();
        
        if (string.IsNullOrWhiteSpace(searchText))
        {
            foreach (var item in allEntries)
            {
                var clonedItem = (ListViewItem)item.Clone();
                clonedItem.Tag = item.Tag;
                entriesListView.Items.Add(clonedItem);
            }
        }
        else
        {
            var filteredEntries = allEntries.Where(item =>
                item.SubItems[1].Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                item.SubItems[2].Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
            );

            foreach (var item in filteredEntries)
            {
                var clonedItem = (ListViewItem)item.Clone();
                clonedItem.Tag = item.Tag;
                entriesListView.Items.Add(clonedItem);
            }
        }
    }

    private void entriesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (entriesListView.SelectedItems.Count > 0)
        {
            var selectedItem = entriesListView.SelectedItems[0];
            ShowEntryDetails(selectedItem);
        }
        else
        {
            ClearEntryDetails();
        }
    }

    private void ShowEntryDetails(ListViewItem item)
    {
        titleTextBox.Text = item.SubItems[1].Text;
        usernameTextBox.Text = item.SubItems[2].Text;
        
        if (item.Tag != null)
        {
            dynamic tagData = item.Tag;
            passwordTextBox.Text = tagData.Password;
            notesTextBox.Text = tagData.Notes;
            urlTextBox.Text = tagData.Url;
        }
        else
        {
            passwordTextBox.Text = "";
            notesTextBox.Text = "";
            urlTextBox.Text = "";
        }
    }

    private void ClearEntryDetails()
    {
        titleTextBox.Text = "";
        usernameTextBox.Text = "";
        passwordTextBox.Text = "";
        urlTextBox.Text = "";
        notesTextBox.Text = "";
    }

    private void searchIcon_Paint(object sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        
        // Draw search magnifying glass
        using (var pen = new Pen(Color.Gray, 2))
        {
            // Draw circle (magnifying glass)
            g.DrawEllipse(pen, 2, 2, 10, 10);
            // Draw handle
            g.DrawLine(pen, 10, 10, 16, 16);
        }
    }

    private void searchIcon_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            Hide();
            notifyIcon.ShowBalloonTip(2000, "KeeZ", "Application minimized to tray", ToolTipIcon.Info);
        }
    }

    private void Main_Resize(object sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Minimized)
        {
            Hide();
            notifyIcon.ShowBalloonTip(2000, "KeeZ", "Application minimized to tray", ToolTipIcon.Info);
        }
    }

    private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            RestoreFromTray();
        }
    }

    private void notifyIcon_DoubleClick(object sender, EventArgs e)
    {
        RestoreFromTray();
    }

    private void showToolStripMenuItem_Click(object sender, EventArgs e)
    {
        RestoreFromTray();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        notifyIcon.Visible = false;
        Application.Exit();
    }

    private void RestoreFromTray()
    {
        Show();
        WindowState = FormWindowState.Normal;
        BringToFront();
        Focus();
    }

    protected override void SetVisibleCore(bool value)
    {
        base.SetVisibleCore(value);
        if (!value && !IsHandleCreated)
        {
            CreateHandle();
        }
    }

    private void SetEntryIcon(ListViewItem item, KeePassLib.PwEntry entry)
    {
        try
        {
            // Check if entry has a custom icon
            if (entry.CustomIconUuid != KeePassLib.PwUuid.Zero && database != null)
            {
                var customIcon = database.CustomIcons.FirstOrDefault(ci => ci.Uuid.Equals(entry.CustomIconUuid));
                if (customIcon?.ImageDataPng != null)
                {
                    using (var stream = new MemoryStream(customIcon.ImageDataPng))
                    {
                        var iconImage = Image.FromStream(stream);
                        var iconKey = entry.CustomIconUuid.ToHexString();
                        
                        if (!entryIcons.Images.ContainsKey(iconKey))
                        {
                            entryIcons.Images.Add(iconKey, new Bitmap(iconImage, 16, 16));
                        }
                        item.ImageKey = iconKey;
                        return;
                    }
                }
            }
            
            // Use standard icon from KeePass
            var standardIconId = (int)entry.IconId;
            var standardIconKey = $"std_{standardIconId}";
            
            if (!entryIcons.Images.ContainsKey(standardIconKey))
            {
                var standardIcon = GetStandardIcon(entry.IconId);
                entryIcons.Images.Add(standardIconKey, standardIcon);
            }
            item.ImageKey = standardIconKey;
        }
        catch
        {
            // Fallback to default icon if anything goes wrong
            item.ImageKey = "";
        }
    }

    private Bitmap GetStandardIcon(KeePassLib.PwIcon iconId)
    {
        // Create a simple representation of standard KeePass icons
        var bitmap = new Bitmap(16, 16);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            
            Color iconColor = GetStandardIconColor(iconId);
            using (var brush = new SolidBrush(iconColor))
            {
                g.FillEllipse(brush, 2, 2, 12, 12);
            }
        }
        return bitmap;
    }
    
    private Color GetStandardIconColor(KeePassLib.PwIcon iconId)
    {
        return iconId switch
        {
            KeePassLib.PwIcon.Key => Color.Gray,
            KeePassLib.PwIcon.World => Color.Blue,
            KeePassLib.PwIcon.Warning => Color.Orange,
            KeePassLib.PwIcon.NetworkServer => Color.Green,
            KeePassLib.PwIcon.MarkedDirectory => Color.Yellow,
            KeePassLib.PwIcon.UserCommunication => Color.Purple,
            KeePassLib.PwIcon.Parts => Color.Brown,
            KeePassLib.PwIcon.Notepad => Color.LightBlue,
            KeePassLib.PwIcon.WorldSocket => Color.DarkBlue,
            KeePassLib.PwIcon.Identity => Color.Pink,
            KeePassLib.PwIcon.PaperReady => Color.White,
            KeePassLib.PwIcon.Digicam => Color.Red,
            KeePassLib.PwIcon.IRCommunication => Color.Cyan,
            KeePassLib.PwIcon.MultiKeys => Color.DarkGray,
            KeePassLib.PwIcon.Energy => Color.Yellow,
            KeePassLib.PwIcon.Scanner => Color.Magenta,
            KeePassLib.PwIcon.WorldStar => Color.Gold,
            KeePassLib.PwIcon.CDRom => Color.Silver,
            KeePassLib.PwIcon.Monitor => Color.Black,
            KeePassLib.PwIcon.EMail => Color.LightGreen,
            KeePassLib.PwIcon.Configuration => Color.DarkRed,
            KeePassLib.PwIcon.ClipboardReady => Color.Beige,
            KeePassLib.PwIcon.PaperNew => Color.WhiteSmoke,
            KeePassLib.PwIcon.Screen => Color.Navy,
            KeePassLib.PwIcon.EnergyCareful => Color.Olive,
            KeePassLib.PwIcon.EMailBox => Color.Lime,
            KeePassLib.PwIcon.Disk => Color.Maroon,
            KeePassLib.PwIcon.Drive => Color.Teal,
            KeePassLib.PwIcon.PaperQ => Color.Aqua,
            KeePassLib.PwIcon.TerminalEncrypted => Color.Fuchsia,
            KeePassLib.PwIcon.Console => Color.DarkCyan,
            KeePassLib.PwIcon.Printer => Color.DarkMagenta,
            KeePassLib.PwIcon.ProgramIcons => Color.DarkOliveGreen,
            KeePassLib.PwIcon.Run => Color.DarkOrchid,
            KeePassLib.PwIcon.Settings => Color.DarkSalmon,
            KeePassLib.PwIcon.WorldComputer => Color.DarkSeaGreen,
            KeePassLib.PwIcon.Archive => Color.DarkSlateBlue,
            KeePassLib.PwIcon.Homebanking => Color.DarkTurquoise,
            KeePassLib.PwIcon.DriveWindows => Color.DarkViolet,
            KeePassLib.PwIcon.Clock => Color.DeepPink,
            KeePassLib.PwIcon.EMailSearch => Color.DeepSkyBlue,
            KeePassLib.PwIcon.PaperFlag => Color.DimGray,
            KeePassLib.PwIcon.Memory => Color.DodgerBlue,
            KeePassLib.PwIcon.TrashBin => Color.Firebrick,
            KeePassLib.PwIcon.Note => Color.ForestGreen,
            KeePassLib.PwIcon.Expired => Color.Gainsboro,
            KeePassLib.PwIcon.Info => Color.GhostWhite,
            KeePassLib.PwIcon.Package => Color.Goldenrod,
            KeePassLib.PwIcon.Folder => Color.HotPink,
            KeePassLib.PwIcon.FolderOpen => Color.IndianRed,
            KeePassLib.PwIcon.FolderPackage => Color.Indigo,
            KeePassLib.PwIcon.LockOpen => Color.Ivory,
            KeePassLib.PwIcon.PaperLocked => Color.Khaki,
            KeePassLib.PwIcon.Checked => Color.Lavender,
            KeePassLib.PwIcon.Pen => Color.LawnGreen,
            KeePassLib.PwIcon.Thumbnail => Color.LemonChiffon,
            KeePassLib.PwIcon.Book => Color.LightCoral,
            KeePassLib.PwIcon.List => Color.LightGoldenrodYellow,
            KeePassLib.PwIcon.UserKey => Color.LightGray,
            KeePassLib.PwIcon.Tool => Color.LightPink,
            KeePassLib.PwIcon.Home => Color.LightSalmon,
            KeePassLib.PwIcon.Star => Color.LightSkyBlue,
            KeePassLib.PwIcon.Tux => Color.LightSlateGray,
            KeePassLib.PwIcon.Feather => Color.LightSteelBlue,
            KeePassLib.PwIcon.Apple => Color.LightYellow,
            KeePassLib.PwIcon.Wiki => Color.LimeGreen,
            KeePassLib.PwIcon.Money => Color.Linen,
            KeePassLib.PwIcon.Certificate => Color.MediumAquamarine,
            KeePassLib.PwIcon.BlackBerry => Color.MediumBlue,
            _ => Color.Gray
        };
    }

    private Bitmap CreateDefaultIcon()
    {
        var bitmap = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            
            // Draw key icon
            using (var brush = new SolidBrush(Color.Gray))
            using (var pen = new Pen(Color.DarkGray, 2))
            {
                g.FillEllipse(brush, 4, 8, 8, 8);
                g.DrawEllipse(pen, 4, 8, 8, 8);
                g.DrawLine(pen, 12, 12, 24, 12);
                g.DrawLine(pen, 20, 8, 20, 12);
                g.DrawLine(pen, 24, 10, 24, 14);
            }
        }
        return bitmap;
    }

    private void LoadDatabaseGroups()
    {
        if (database == null) return;

        groupsTreeView.BeginUpdate();
        groupsTreeView.Nodes.Clear();

        // Add special "All Items" group first
        TreeNode allItemsNode = new TreeNode("All Items");
        allItemsNode.Tag = "ALL_ITEMS";
        groupsTreeView.Nodes.Add(allItemsNode);

        // Add actual groups from database (excluding recycle bin for now)
        if (database.RootGroup != null)
        {
            AddGroupsToTreeView(database.RootGroup, null, false); // false = exclude recycle bin
        }

        // Add recycle bin at the bottom if it exists
        var recycleBin = GetRecycleBinGroup();
        if (recycleBin != null)
        {
            TreeNode recycleBinNode = new TreeNode("🗑️ Recycle Bin");
            recycleBinNode.Tag = recycleBin;
            groupsTreeView.Nodes.Add(recycleBinNode);
            
            // Add any subgroups within recycle bin
            foreach (KeePassLib.PwGroup childGroup in recycleBin.Groups)
            {
                AddGroupsToTreeView(childGroup, recycleBinNode, true); // true = include all groups
            }
        }

        // Expand the first level
        foreach (TreeNode node in groupsTreeView.Nodes)
        {
            node.Expand();
        }

        // Select "All Items" by default
        groupsTreeView.SelectedNode = allItemsNode;

        groupsTreeView.EndUpdate();
    }

    private void AddGroupsToTreeView(KeePassLib.PwGroup group, TreeNode? parentNode, bool includeRecycleBin = false)
    {
        if (group == null) return;

        // Skip the root group itself (use its children) and conditionally skip recycle bin
        if (group != database?.RootGroup && (includeRecycleBin || !IsRecycleBin(group)))
        {
            TreeNode groupNode = new TreeNode(group.Name);
            groupNode.Tag = group;

            if (parentNode == null)
                groupsTreeView.Nodes.Add(groupNode);
            else
                parentNode.Nodes.Add(groupNode);

            // Add child groups
            foreach (KeePassLib.PwGroup childGroup in group.Groups)
            {
                AddGroupsToTreeView(childGroup, groupNode, includeRecycleBin);
            }
        }
        else if (group == database?.RootGroup)
        {
            // For root group, just add its children
            foreach (KeePassLib.PwGroup childGroup in group.Groups)
            {
                AddGroupsToTreeView(childGroup, parentNode, includeRecycleBin);
            }
        }
    }

    private bool IsRecycleBin(KeePassLib.PwGroup group)
    {
        if (database == null) return false;
        return group.Uuid.Equals(database.RecycleBinUuid);
    }

    private KeePassLib.PwGroup? GetRecycleBinGroup()
    {
        if (database?.RootGroup == null) return null;
        return database.RootGroup.FindGroup(database.RecycleBinUuid, true);
    }

    private void groupsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag != null)
        {
            LoadEntriesForGroup(e.Node.Tag);
        }
    }

    private void LoadEntriesForGroup(object groupTag)
    {
        if (database == null) return;

        allEntries.Clear();
        entriesListView.Items.Clear();

        if (groupTag is string && (string)groupTag == "ALL_ITEMS")
        {
            // Load all entries except from recycle bin
            LoadAllNonRecycledEntries();
        }
        else if (groupTag is KeePassLib.PwGroup group)
        {
            // Load entries from specific group
            LoadEntriesFromGroup(group);
        }

        // Apply current search filter if any
        if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
        {
            FilterEntries(searchTextBox.Text);
        }
    }

    private void LoadAllNonRecycledEntries()
    {
        if (database?.RootGroup == null) return;

        foreach (KeePassLib.PwGroup group in database.RootGroup.GetFlatGroupList())
        {
            if (IsRecycleBin(group)) continue;

            LoadEntriesFromGroup(group);
        }
    }

    private void LoadEntriesFromGroup(KeePassLib.PwGroup group)
    {
        if (group?.Entries == null) return;

        foreach (KeePassLib.PwEntry entry in group.Entries)
        {
            // Skip expired entries
            if (entry.Expires && entry.ExpiryTime <= DateTime.Now) continue;

            string title = entry.Strings.ReadSafe(KeePassLib.PwDefs.TitleField);
            string username = entry.Strings.ReadSafe(KeePassLib.PwDefs.UserNameField);
            string password = entry.Strings.ReadSafe(KeePassLib.PwDefs.PasswordField);
            string url = entry.Strings.ReadSafe(KeePassLib.PwDefs.UrlField);
            string notes = entry.Strings.ReadSafe(KeePassLib.PwDefs.NotesField);

            var item = new ListViewItem("");
            item.SubItems.Add(title);
            item.SubItems.Add(username);
            item.Tag = new { Password = password, Notes = notes, Entry = entry, Url = url };

            // Set icon from KDBX entry
            SetEntryIcon(item, entry);

            allEntries.Add(item);
            entriesListView.Items.Add(item);
        }
    }

    private void LoadDatabaseEntries()
    {
        // This method is now replaced by LoadEntriesForGroup
        // but kept for compatibility with sample data loading
        LoadAllNonRecycledEntries();
    }
}