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
        entryIcons.ImageSize = new Size(32, 32);
        entryIcons.ColorDepth = ColorDepth.Depth32Bit;
        
        // Create basic icons for different entry types
        entryIcons.Images.Add("default", CreateDefaultIcon());
        entryIcons.Images.Add("email", CreateEmailIcon());
        entryIcons.Images.Add("web", CreateWebIcon());
        entryIcons.Images.Add("social", CreateSocialIcon());
        entryIcons.Images.Add("bank", CreateBankIcon());
        entryIcons.Images.Add("shopping", CreateShoppingIcon());
        
        entriesListView.LargeImageList = entryIcons;
        entriesListView.SmallImageList = entryIcons;
    }

    private void InitializeListView()
    {
        entriesListView.FullRowSelect = true;
        
        // Configure for tile view with two-line display
        entriesListView.TileSize = new Size(350, 48);
        entriesListView.View = View.Tile;
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
                item.SubItems[0].Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
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
        titleTextBox.Text = item.SubItems[0].Text;
        usernameTextBox.Text = item.SubItems[1].Text;
        urlTextBox.Text = item.SubItems[2].Text;

        if (item.Tag != null)
        {
            dynamic tagData = item.Tag;
            passwordTextBox.Text = tagData.Password;
            notesTextBox.Text = tagData.Notes;
        }
        else
        {
            passwordTextBox.Text = "";
            notesTextBox.Text = "";
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

    private string GetIconKeyForEntry(string title, string url)
    {
        string lowerTitle = title.ToLower();
        string lowerUrl = url.ToLower();
        
        if (lowerTitle.Contains("gmail") || lowerTitle.Contains("email") || lowerTitle.Contains("mail"))
            return "email";
        else if (lowerTitle.Contains("facebook") || lowerTitle.Contains("twitter") || lowerTitle.Contains("instagram"))
            return "social";
        else if (lowerTitle.Contains("bank") || lowerTitle.Contains("credit") || lowerTitle.Contains("finance"))
            return "bank";
        else if (lowerTitle.Contains("amazon") || lowerTitle.Contains("shop") || lowerTitle.Contains("store"))
            return "shopping";
        else if (lowerUrl.Contains("http") || lowerTitle.Contains("web") || lowerTitle.Contains("github"))
            return "web";
        else
            return "default";
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

    private Bitmap CreateEmailIcon()
    {
        var bitmap = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            
            // Draw envelope
            using (var brush = new SolidBrush(Color.LightBlue))
            using (var pen = new Pen(Color.Blue, 2))
            {
                Rectangle rect = new Rectangle(4, 8, 24, 16);
                g.FillRectangle(brush, rect);
                g.DrawRectangle(pen, rect);
                
                // Draw letter fold
                Point[] points = { new Point(4, 8), new Point(16, 16), new Point(28, 8) };
                g.DrawLines(pen, points);
            }
        }
        return bitmap;
    }

    private Bitmap CreateWebIcon()
    {
        var bitmap = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            
            // Draw globe
            using (var brush = new SolidBrush(Color.LightGreen))
            using (var pen = new Pen(Color.Green, 2))
            {
                g.FillEllipse(brush, 4, 4, 24, 24);
                g.DrawEllipse(pen, 4, 4, 24, 24);
                
                // Draw grid lines
                g.DrawLine(pen, 16, 4, 16, 28);
                g.DrawLine(pen, 4, 16, 28, 16);
                g.DrawArc(pen, 8, 4, 16, 24, 0, 180);
                g.DrawArc(pen, 8, 4, 16, 24, 180, 180);
            }
        }
        return bitmap;
    }

    private Bitmap CreateSocialIcon()
    {
        var bitmap = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            
            // Draw people icons
            using (var brush = new SolidBrush(Color.Orange))
            using (var pen = new Pen(Color.DarkOrange, 2))
            {
                // Person 1
                g.FillEllipse(brush, 6, 6, 8, 8);
                g.FillEllipse(brush, 4, 16, 12, 12);
                
                // Person 2
                g.FillEllipse(brush, 18, 8, 6, 6);
                g.FillEllipse(brush, 16, 16, 10, 10);
            }
        }
        return bitmap;
    }

    private Bitmap CreateBankIcon()
    {
        var bitmap = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            
            // Draw bank building
            using (var brush = new SolidBrush(Color.Gold))
            using (var pen = new Pen(Color.DarkGoldenrod, 2))
            {
                // Base
                g.FillRectangle(brush, 4, 22, 24, 6);
                g.DrawRectangle(pen, 4, 22, 24, 6);
                
                // Columns
                g.FillRectangle(brush, 8, 10, 3, 12);
                g.FillRectangle(brush, 14, 10, 3, 12);
                g.FillRectangle(brush, 20, 10, 3, 12);
                
                // Top triangle
                Point[] roof = { new Point(16, 4), new Point(6, 10), new Point(26, 10) };
                g.FillPolygon(brush, roof);
                g.DrawPolygon(pen, roof);
            }
        }
        return bitmap;
    }

    private Bitmap CreateShoppingIcon()
    {
        var bitmap = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            
            // Draw shopping cart
            using (var brush = new SolidBrush(Color.Purple))
            using (var pen = new Pen(Color.DarkMagenta, 2))
            {
                // Cart body
                g.DrawRectangle(pen, 8, 12, 16, 10);
                g.DrawLine(pen, 6, 8, 10, 12);
                g.DrawLine(pen, 6, 8, 4, 8);
                
                // Wheels
                g.FillEllipse(brush, 10, 24, 4, 4);
                g.FillEllipse(brush, 18, 24, 4, 4);
                
                // Handle
                g.DrawLine(pen, 24, 10, 26, 8);
                g.DrawLine(pen, 26, 8, 28, 10);
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

            var item = new ListViewItem(title);
            item.SubItems.Add(username);
            item.SubItems.Add(url);
            item.SubItems.Add(entry.LastModificationTime.ToString("yyyy-MM-dd"));
            item.Tag = new { Password = password, Notes = notes, Entry = entry };

            // Set icon based on entry
            string iconKey = GetIconKeyForEntry(title, url);
            item.ImageKey = iconKey;

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