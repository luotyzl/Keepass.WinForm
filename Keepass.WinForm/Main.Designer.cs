namespace Keepass.WinForm;

partial class Main
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private System.Windows.Forms.TreeView groupsTreeView;
    private System.Windows.Forms.ListView entriesListView;
    private System.Windows.Forms.Panel detailsPanel;
    private System.Windows.Forms.Panel centerPanel;
    private System.Windows.Forms.Panel searchPanel;
    private System.Windows.Forms.PictureBox searchIcon;
    private System.Windows.Forms.TextBox searchTextBox;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Splitter splitter2;
    private System.Windows.Forms.Label detailsTitleLabel;
    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.TextBox titleTextBox;
    private System.Windows.Forms.Label usernameLabel;
    private System.Windows.Forms.TextBox usernameTextBox;
    private System.Windows.Forms.Label passwordLabel;
    private System.Windows.Forms.TextBox passwordTextBox;
    private System.Windows.Forms.Label urlLabel;
    private System.Windows.Forms.TextBox urlTextBox;
    private System.Windows.Forms.Label notesLabel;
    private System.Windows.Forms.TextBox notesTextBox;
    private System.Windows.Forms.NotifyIcon notifyIcon;
    private System.Windows.Forms.ContextMenuStrip trayContextMenu;
    private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
        trayContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
        showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        groupsTreeView = new System.Windows.Forms.TreeView();
        notifyIcon = new System.Windows.Forms.NotifyIcon(components);
        entriesListView = new System.Windows.Forms.ListView();
        detailsPanel = new System.Windows.Forms.Panel();
        notesTextBox = new System.Windows.Forms.TextBox();
        notesLabel = new System.Windows.Forms.Label();
        urlTextBox = new System.Windows.Forms.TextBox();
        urlLabel = new System.Windows.Forms.Label();
        passwordTextBox = new System.Windows.Forms.TextBox();
        passwordLabel = new System.Windows.Forms.Label();
        usernameTextBox = new System.Windows.Forms.TextBox();
        usernameLabel = new System.Windows.Forms.Label();
        titleTextBox = new System.Windows.Forms.TextBox();
        titleLabel = new System.Windows.Forms.Label();
        detailsTitleLabel = new System.Windows.Forms.Label();
        centerPanel = new System.Windows.Forms.Panel();
        searchPanel = new System.Windows.Forms.Panel();
        searchTextBox = new System.Windows.Forms.TextBox();
        searchIcon = new System.Windows.Forms.PictureBox();
        splitter1 = new System.Windows.Forms.Splitter();
        splitter2 = new System.Windows.Forms.Splitter();
        trayContextMenu.SuspendLayout();
        detailsPanel.SuspendLayout();
        centerPanel.SuspendLayout();
        searchPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)searchIcon).BeginInit();
        SuspendLayout();
        // 
        // trayContextMenu
        // 
        trayContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
        trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            showToolStripMenuItem, exitToolStripMenuItem
        });
        trayContextMenu.Name = "trayContextMenu";
        trayContextMenu.Size = new System.Drawing.Size(119, 52);
        // 
        // showToolStripMenuItem
        // 
        showToolStripMenuItem.Name = "showToolStripMenuItem";
        showToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
        showToolStripMenuItem.Text = "Show";
        showToolStripMenuItem.Click += showToolStripMenuItem_Click;
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
        exitToolStripMenuItem.Text = "Exit";
        exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
        // 
        // groupsTreeView
        // 
        groupsTreeView.Dock = System.Windows.Forms.DockStyle.Left;
        groupsTreeView.Location = new System.Drawing.Point(0, 0);
        groupsTreeView.Margin = new System.Windows.Forms.Padding(4);
        groupsTreeView.Name = "groupsTreeView";
        groupsTreeView.Size = new System.Drawing.Size(256, 800);
        groupsTreeView.TabIndex = 0;
        groupsTreeView.AfterSelect += groupsTreeView_AfterSelect;
        // 
        // notifyIcon
        // 
        notifyIcon.ContextMenuStrip = trayContextMenu;
        notifyIcon.Text = "KeeZ - Password Manager";
        notifyIcon.Visible = true;
        notifyIcon.DoubleClick += notifyIcon_DoubleClick;
        notifyIcon.MouseClick += notifyIcon_MouseClick;
        // 
        // entriesListView
        // 
        entriesListView.Dock = System.Windows.Forms.DockStyle.Fill;
        entriesListView.Location = new System.Drawing.Point(0, 40);
        entriesListView.Margin = new System.Windows.Forms.Padding(4);
        entriesListView.Name = "entriesListView";
        entriesListView.Size = new System.Drawing.Size(387, 760);
        entriesListView.TabIndex = 1;
        entriesListView.TileSize = new System.Drawing.Size(350, 48);
        entriesListView.UseCompatibleStateImageBehavior = false;
        entriesListView.View = System.Windows.Forms.View.Tile;
        entriesListView.SelectedIndexChanged += entriesListView_SelectedIndexChanged;
        // 
        // detailsPanel
        // 
        detailsPanel.Controls.Add(notesTextBox);
        detailsPanel.Controls.Add(notesLabel);
        detailsPanel.Controls.Add(urlTextBox);
        detailsPanel.Controls.Add(urlLabel);
        detailsPanel.Controls.Add(passwordTextBox);
        detailsPanel.Controls.Add(passwordLabel);
        detailsPanel.Controls.Add(usernameTextBox);
        detailsPanel.Controls.Add(usernameLabel);
        detailsPanel.Controls.Add(titleTextBox);
        detailsPanel.Controls.Add(titleLabel);
        detailsPanel.Controls.Add(detailsTitleLabel);
        detailsPanel.Dock = System.Windows.Forms.DockStyle.Right;
        detailsPanel.Location = new System.Drawing.Point(651, 0);
        detailsPanel.Margin = new System.Windows.Forms.Padding(4);
        detailsPanel.Name = "detailsPanel";
        detailsPanel.Padding = new System.Windows.Forms.Padding(13);
        detailsPanel.Size = new System.Drawing.Size(378, 800);
        detailsPanel.TabIndex = 4;
        // 
        // notesTextBox
        // 
        notesTextBox.Location = new System.Drawing.Point(13, 413);
        notesTextBox.Margin = new System.Windows.Forms.Padding(4);
        notesTextBox.Multiline = true;
        notesTextBox.Name = "notesTextBox";
        notesTextBox.ReadOnly = true;
        notesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        notesTextBox.Size = new System.Drawing.Size(346, 132);
        notesTextBox.TabIndex = 10;
        // 
        // notesLabel
        // 
        notesLabel.AutoSize = true;
        notesLabel.Location = new System.Drawing.Point(13, 387);
        notesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        notesLabel.Name = "notesLabel";
        notesLabel.Size = new System.Drawing.Size(57, 20);
        notesLabel.TabIndex = 9;
        notesLabel.Text = "Notes:";
        // 
        // urlTextBox
        // 
        urlTextBox.Location = new System.Drawing.Point(13, 333);
        urlTextBox.Margin = new System.Windows.Forms.Padding(4);
        urlTextBox.Name = "urlTextBox";
        urlTextBox.ReadOnly = true;
        urlTextBox.Size = new System.Drawing.Size(346, 27);
        urlTextBox.TabIndex = 8;
        // 
        // urlLabel
        // 
        urlLabel.AutoSize = true;
        urlLabel.Location = new System.Drawing.Point(13, 307);
        urlLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        urlLabel.Name = "urlLabel";
        urlLabel.Size = new System.Drawing.Size(42, 20);
        urlLabel.TabIndex = 7;
        urlLabel.Text = "URL:";
        // 
        // passwordTextBox
        // 
        passwordTextBox.Location = new System.Drawing.Point(13, 253);
        passwordTextBox.Margin = new System.Windows.Forms.Padding(4);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.PasswordChar = '*';
        passwordTextBox.ReadOnly = true;
        passwordTextBox.Size = new System.Drawing.Size(346, 27);
        passwordTextBox.TabIndex = 6;
        // 
        // passwordLabel
        // 
        passwordLabel.AutoSize = true;
        passwordLabel.Location = new System.Drawing.Point(13, 227);
        passwordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        passwordLabel.Name = "passwordLabel";
        passwordLabel.Size = new System.Drawing.Size(82, 20);
        passwordLabel.TabIndex = 5;
        passwordLabel.Text = "Password:";
        // 
        // usernameTextBox
        // 
        usernameTextBox.Location = new System.Drawing.Point(13, 173);
        usernameTextBox.Margin = new System.Windows.Forms.Padding(4);
        usernameTextBox.Name = "usernameTextBox";
        usernameTextBox.ReadOnly = true;
        usernameTextBox.Size = new System.Drawing.Size(346, 27);
        usernameTextBox.TabIndex = 4;
        // 
        // usernameLabel
        // 
        usernameLabel.AutoSize = true;
        usernameLabel.Location = new System.Drawing.Point(13, 147);
        usernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        usernameLabel.Name = "usernameLabel";
        usernameLabel.Size = new System.Drawing.Size(86, 20);
        usernameLabel.TabIndex = 3;
        usernameLabel.Text = "Username:";
        // 
        // titleTextBox
        // 
        titleTextBox.Location = new System.Drawing.Point(13, 93);
        titleTextBox.Margin = new System.Windows.Forms.Padding(4);
        titleTextBox.Name = "titleTextBox";
        titleTextBox.ReadOnly = true;
        titleTextBox.Size = new System.Drawing.Size(346, 27);
        titleTextBox.TabIndex = 2;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new System.Drawing.Point(13, 67);
        titleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new System.Drawing.Size(45, 20);
        titleLabel.TabIndex = 1;
        titleLabel.Text = "Title:";
        // 
        // detailsTitleLabel
        // 
        detailsTitleLabel.AutoSize = true;
        detailsTitleLabel.Location = new System.Drawing.Point(13, 13);
        detailsTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        detailsTitleLabel.Name = "detailsTitleLabel";
        detailsTitleLabel.Size = new System.Drawing.Size(99, 20);
        detailsTitleLabel.TabIndex = 0;
        detailsTitleLabel.Text = "Entry Details";
        // 
        // centerPanel
        // 
        centerPanel.Controls.Add(entriesListView);
        centerPanel.Controls.Add(searchPanel);
        centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        centerPanel.Location = new System.Drawing.Point(260, 0);
        centerPanel.Margin = new System.Windows.Forms.Padding(4);
        centerPanel.Name = "centerPanel";
        centerPanel.Size = new System.Drawing.Size(387, 800);
        centerPanel.TabIndex = 5;
        // 
        // searchPanel
        // 
        searchPanel.Controls.Add(searchTextBox);
        searchPanel.Controls.Add(searchIcon);
        searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
        searchPanel.Location = new System.Drawing.Point(0, 0);
        searchPanel.Margin = new System.Windows.Forms.Padding(4);
        searchPanel.Name = "searchPanel";
        searchPanel.Size = new System.Drawing.Size(387, 40);
        searchPanel.TabIndex = 0;
        // 
        // searchTextBox
        // 
        searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        searchTextBox.Location = new System.Drawing.Point(39, 5);
        searchTextBox.Margin = new System.Windows.Forms.Padding(4);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.PlaceholderText = "Search entries...";
        searchTextBox.Size = new System.Drawing.Size(341, 27);
        searchTextBox.TabIndex = 0;
        searchTextBox.TextChanged += searchTextBox_TextChanged;
        // 
        // searchIcon
        // 
        searchIcon.BackColor = System.Drawing.Color.Transparent;
        searchIcon.Location = new System.Drawing.Point(6, 7);
        searchIcon.Margin = new System.Windows.Forms.Padding(4);
        searchIcon.Name = "searchIcon";
        searchIcon.Size = new System.Drawing.Size(26, 27);
        searchIcon.TabIndex = 1;
        searchIcon.TabStop = false;
        searchIcon.Click += searchIcon_Click;
        searchIcon.Paint += searchIcon_Paint;
        // 
        // splitter1
        // 
        splitter1.Location = new System.Drawing.Point(256, 0);
        splitter1.Margin = new System.Windows.Forms.Padding(4);
        splitter1.Name = "splitter1";
        splitter1.Size = new System.Drawing.Size(4, 800);
        splitter1.TabIndex = 1;
        splitter1.TabStop = false;
        // 
        // splitter2
        // 
        splitter2.Dock = System.Windows.Forms.DockStyle.Right;
        splitter2.Location = new System.Drawing.Point(647, 0);
        splitter2.Margin = new System.Windows.Forms.Padding(4);
        splitter2.Name = "splitter2";
        splitter2.Size = new System.Drawing.Size(4, 800);
        splitter2.TabIndex = 3;
        splitter2.TabStop = false;
        // 
        // Main
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1029, 800);
        Controls.Add(centerPanel);
        Controls.Add(splitter2);
        Controls.Add(detailsPanel);
        Controls.Add(splitter1);
        Controls.Add(groupsTreeView);
        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
        Margin = new System.Windows.Forms.Padding(4);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KeeZ";
        FormClosing += Main_FormClosing;
        Resize += Main_Resize;
        trayContextMenu.ResumeLayout(false);
        detailsPanel.ResumeLayout(false);
        detailsPanel.PerformLayout();
        centerPanel.ResumeLayout(false);
        searchPanel.ResumeLayout(false);
        searchPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)searchIcon).EndInit();
        ResumeLayout(false);
    }

    #endregion
}