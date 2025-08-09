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
        this.centerPanel.SuspendLayout();
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
        this.detailsPanel.Dock = System.Windows.Forms.DockStyle.Right;
        this.detailsPanel.Location = new System.Drawing.Point(506, 0);
        this.detailsPanel.Name = "detailsPanel";
        this.detailsPanel.Size = new System.Drawing.Size(294, 600);
        this.detailsPanel.TabIndex = 4;
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
        this.ResumeLayout(false);
    }

    #endregion
}