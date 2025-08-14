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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.groupsTreeView = new System.Windows.Forms.TreeView();
        this.entriesListView = new System.Windows.Forms.ListView();
        this.detailsPanel = new System.Windows.Forms.Panel();
        this.centerPanel = new System.Windows.Forms.Panel();
        this.searchTextBox = new System.Windows.Forms.TextBox();
        this.splitter1 = new System.Windows.Forms.Splitter();
        this.splitter2 = new System.Windows.Forms.Splitter();
        this.detailsTitleLabel = new System.Windows.Forms.Label();
        this.titleLabel = new System.Windows.Forms.Label();
        this.titleTextBox = new System.Windows.Forms.TextBox();
        this.usernameLabel = new System.Windows.Forms.Label();
        this.usernameTextBox = new System.Windows.Forms.TextBox();
        this.passwordLabel = new System.Windows.Forms.Label();
        this.passwordTextBox = new System.Windows.Forms.TextBox();
        this.urlLabel = new System.Windows.Forms.Label();
        this.urlTextBox = new System.Windows.Forms.TextBox();
        this.notesLabel = new System.Windows.Forms.Label();
        this.notesTextBox = new System.Windows.Forms.TextBox();
        this.centerPanel.SuspendLayout();
        this.detailsPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupsTreeView
        // 
        this.groupsTreeView.Dock = System.Windows.Forms.DockStyle.Left;
        this.groupsTreeView.Location = new System.Drawing.Point(0, 0);
        this.groupsTreeView.Name = "groupsTreeView";
        this.groupsTreeView.Size = new System.Drawing.Size(200, 600);
        this.groupsTreeView.TabIndex = 0;
        // 
        // splitter1
        // 
        this.splitter1.Location = new System.Drawing.Point(200, 0);
        this.splitter1.Name = "splitter1";
        this.splitter1.Size = new System.Drawing.Size(3, 600);
        this.splitter1.TabIndex = 1;
        this.splitter1.TabStop = false;
        // 
        // centerPanel
        // 
        this.centerPanel.Controls.Add(this.entriesListView);
        this.centerPanel.Controls.Add(this.searchTextBox);
        this.centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.centerPanel.Location = new System.Drawing.Point(203, 0);
        this.centerPanel.Name = "centerPanel";
        this.centerPanel.Size = new System.Drawing.Size(300, 600);
        this.centerPanel.TabIndex = 5;
        // 
        // searchTextBox
        // 
        this.searchTextBox.Dock = System.Windows.Forms.DockStyle.Top;
        this.searchTextBox.Location = new System.Drawing.Point(0, 0);
        this.searchTextBox.Name = "searchTextBox";
        this.searchTextBox.PlaceholderText = "Search entries...";
        this.searchTextBox.Size = new System.Drawing.Size(300, 23);
        this.searchTextBox.TabIndex = 0;
        this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
        // 
        // entriesListView
        // 
        this.entriesListView.Dock = System.Windows.Forms.DockStyle.Fill;
        this.entriesListView.Location = new System.Drawing.Point(0, 23);
        this.entriesListView.Name = "entriesListView";
        this.entriesListView.Size = new System.Drawing.Size(300, 577);
        this.entriesListView.TabIndex = 1;
        this.entriesListView.UseCompatibleStateImageBehavior = false;
        this.entriesListView.View = System.Windows.Forms.View.Details;
        this.entriesListView.SelectedIndexChanged += new System.EventHandler(this.entriesListView_SelectedIndexChanged);
        // 
        // splitter2
        // 
        this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
        this.splitter2.Location = new System.Drawing.Point(503, 0);
        this.splitter2.Name = "splitter2";
        this.splitter2.Size = new System.Drawing.Size(3, 600);
        this.splitter2.TabIndex = 3;
        this.splitter2.TabStop = false;
        // 
        // detailsPanel
        // 
        this.detailsPanel.Controls.Add(this.notesTextBox);
        this.detailsPanel.Controls.Add(this.notesLabel);
        this.detailsPanel.Controls.Add(this.urlTextBox);
        this.detailsPanel.Controls.Add(this.urlLabel);
        this.detailsPanel.Controls.Add(this.passwordTextBox);
        this.detailsPanel.Controls.Add(this.passwordLabel);
        this.detailsPanel.Controls.Add(this.usernameTextBox);
        this.detailsPanel.Controls.Add(this.usernameLabel);
        this.detailsPanel.Controls.Add(this.titleTextBox);
        this.detailsPanel.Controls.Add(this.titleLabel);
        this.detailsPanel.Controls.Add(this.detailsTitleLabel);
        this.detailsPanel.Dock = System.Windows.Forms.DockStyle.Right;
        this.detailsPanel.Location = new System.Drawing.Point(506, 0);
        this.detailsPanel.Name = "detailsPanel";
        this.detailsPanel.Padding = new System.Windows.Forms.Padding(10);
        this.detailsPanel.Size = new System.Drawing.Size(294, 600);
        this.detailsPanel.TabIndex = 4;
        // 
        // detailsTitleLabel
        // 
        this.detailsTitleLabel.AutoSize = true;
        this.detailsTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.detailsTitleLabel.Location = new System.Drawing.Point(10, 10);
        this.detailsTitleLabel.Name = "detailsTitleLabel";
        this.detailsTitleLabel.Size = new System.Drawing.Size(103, 20);
        this.detailsTitleLabel.TabIndex = 0;
        this.detailsTitleLabel.Text = "Entry Details";
        // 
        // titleLabel
        // 
        this.titleLabel.AutoSize = true;
        this.titleLabel.Location = new System.Drawing.Point(10, 50);
        this.titleLabel.Name = "titleLabel";
        this.titleLabel.Size = new System.Drawing.Size(32, 15);
        this.titleLabel.TabIndex = 1;
        this.titleLabel.Text = "Title:";
        // 
        // titleTextBox
        // 
        this.titleTextBox.Location = new System.Drawing.Point(10, 70);
        this.titleTextBox.Name = "titleTextBox";
        this.titleTextBox.ReadOnly = true;
        this.titleTextBox.Size = new System.Drawing.Size(270, 23);
        this.titleTextBox.TabIndex = 2;
        // 
        // usernameLabel
        // 
        this.usernameLabel.AutoSize = true;
        this.usernameLabel.Location = new System.Drawing.Point(10, 110);
        this.usernameLabel.Name = "usernameLabel";
        this.usernameLabel.Size = new System.Drawing.Size(63, 15);
        this.usernameLabel.TabIndex = 3;
        this.usernameLabel.Text = "Username:";
        // 
        // usernameTextBox
        // 
        this.usernameTextBox.Location = new System.Drawing.Point(10, 130);
        this.usernameTextBox.Name = "usernameTextBox";
        this.usernameTextBox.ReadOnly = true;
        this.usernameTextBox.Size = new System.Drawing.Size(270, 23);
        this.usernameTextBox.TabIndex = 4;
        // 
        // passwordLabel
        // 
        this.passwordLabel.AutoSize = true;
        this.passwordLabel.Location = new System.Drawing.Point(10, 170);
        this.passwordLabel.Name = "passwordLabel";
        this.passwordLabel.Size = new System.Drawing.Size(60, 15);
        this.passwordLabel.TabIndex = 5;
        this.passwordLabel.Text = "Password:";
        // 
        // passwordTextBox
        // 
        this.passwordTextBox.Location = new System.Drawing.Point(10, 190);
        this.passwordTextBox.Name = "passwordTextBox";
        this.passwordTextBox.PasswordChar = '*';
        this.passwordTextBox.ReadOnly = true;
        this.passwordTextBox.Size = new System.Drawing.Size(270, 23);
        this.passwordTextBox.TabIndex = 6;
        // 
        // urlLabel
        // 
        this.urlLabel.AutoSize = true;
        this.urlLabel.Location = new System.Drawing.Point(10, 230);
        this.urlLabel.Name = "urlLabel";
        this.urlLabel.Size = new System.Drawing.Size(31, 15);
        this.urlLabel.TabIndex = 7;
        this.urlLabel.Text = "URL:";
        // 
        // urlTextBox
        // 
        this.urlTextBox.Location = new System.Drawing.Point(10, 250);
        this.urlTextBox.Name = "urlTextBox";
        this.urlTextBox.ReadOnly = true;
        this.urlTextBox.Size = new System.Drawing.Size(270, 23);
        this.urlTextBox.TabIndex = 8;
        // 
        // notesLabel
        // 
        this.notesLabel.AutoSize = true;
        this.notesLabel.Location = new System.Drawing.Point(10, 290);
        this.notesLabel.Name = "notesLabel";
        this.notesLabel.Size = new System.Drawing.Size(41, 15);
        this.notesLabel.TabIndex = 9;
        this.notesLabel.Text = "Notes:";
        // 
        // notesTextBox
        // 
        this.notesTextBox.Location = new System.Drawing.Point(10, 310);
        this.notesTextBox.Multiline = true;
        this.notesTextBox.Name = "notesTextBox";
        this.notesTextBox.ReadOnly = true;
        this.notesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.notesTextBox.Size = new System.Drawing.Size(270, 100);
        this.notesTextBox.TabIndex = 10;
        // 
        // Main
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 600);
        this.Controls.Add(this.centerPanel);
        this.Controls.Add(this.splitter2);
        this.Controls.Add(this.detailsPanel);
        this.Controls.Add(this.splitter1);
        this.Controls.Add(this.groupsTreeView);
        this.Name = "Main";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Main";
        this.centerPanel.ResumeLayout(false);
        this.centerPanel.PerformLayout();
        this.detailsPanel.ResumeLayout(false);
        this.detailsPanel.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion
}