namespace Keepass.WinForm;

public partial class Main : Form
{
    private List<ListViewItem> allEntries = new List<ListViewItem>();

    public Main()
    {
        InitializeComponent();
        InitializeTreeView();
        InitializeListView();
        LoadSampleData();
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

    private void InitializeListView()
    {
        entriesListView.Columns.Add("Title", 150);
        entriesListView.Columns.Add("Username", 120);
        entriesListView.Columns.Add("URL", 200);
        entriesListView.Columns.Add("Modified", 100);
        
        entriesListView.FullRowSelect = true;
        entriesListView.GridLines = true;
    }

    private void LoadSampleData()
    {
        var sampleEntries = new[]
        {
            new { Title = "Gmail", Username = "user@gmail.com", URL = "https://gmail.com", Modified = "2024-01-15", Password = "mySecretPass123", Notes = "Personal email account for daily communication." },
            new { Title = "Facebook", Username = "myusername", URL = "https://facebook.com", Modified = "2024-01-10", Password = "fb_secure456", Notes = "Social media account. Enable two-factor authentication." },
            new { Title = "GitHub", Username = "developer", URL = "https://github.com", Modified = "2024-01-20", Password = "codeLife789", Notes = "Development platform for code repositories and collaboration." },
            new { Title = "Microsoft", Username = "work@company.com", URL = "https://microsoft.com", Modified = "2024-01-08", Password = "workPass321", Notes = "Corporate Microsoft account for Office 365 and Azure services." },
            new { Title = "Banking", Username = "account123", URL = "https://mybank.com", Modified = "2024-01-12", Password = "bank$ecure999", Notes = "Online banking account. Never share credentials. Use secure connection only." },
            new { Title = "Amazon", Username = "shopper", URL = "https://amazon.com", Modified = "2024-01-18", Password = "shop2024!", Notes = "E-commerce account for online shopping. Saved payment methods available." }
        };

        foreach (var entry in sampleEntries)
        {
            var item = new ListViewItem(new[] { entry.Title, entry.Username, entry.URL, entry.Modified });
            item.Tag = new { entry.Password, entry.Notes };
            allEntries.Add(item);
            entriesListView.Items.Add(item);
        }
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
}