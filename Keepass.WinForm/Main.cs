namespace Keepass.WinForm;

public partial class Main : Form
{
    private List<ListViewItem> allEntries;

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
        
        allEntries = new List<ListViewItem>();
    }

    private void LoadSampleData()
    {
        var sampleEntries = new[]
        {
            new { Title = "Gmail", Username = "user@gmail.com", URL = "https://gmail.com", Modified = "2024-01-15" },
            new { Title = "Facebook", Username = "myusername", URL = "https://facebook.com", Modified = "2024-01-10" },
            new { Title = "GitHub", Username = "developer", URL = "https://github.com", Modified = "2024-01-20" },
            new { Title = "Microsoft", Username = "work@company.com", URL = "https://microsoft.com", Modified = "2024-01-08" },
            new { Title = "Banking", Username = "account123", URL = "https://mybank.com", Modified = "2024-01-12" },
            new { Title = "Amazon", Username = "shopper", URL = "https://amazon.com", Modified = "2024-01-18" }
        };

        foreach (var entry in sampleEntries)
        {
            var item = new ListViewItem(new[] { entry.Title, entry.Username, entry.URL, entry.Modified });
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
                entriesListView.Items.Add((ListViewItem)item.Clone());
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
                entriesListView.Items.Add((ListViewItem)item.Clone());
            }
        }
    }
}