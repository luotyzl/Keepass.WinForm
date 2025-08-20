namespace Keepass.WinForm;

partial class SettingsForm
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

    private System.Windows.Forms.GroupBox localGroupBox;
    private System.Windows.Forms.Label localDatabasePathLabel;
    private System.Windows.Forms.TextBox localDatabasePathTextBox;
    private System.Windows.Forms.Button browseButton;
    private System.Windows.Forms.GroupBox webDavGroupBox;
    private System.Windows.Forms.Label webDavUrlLabel;
    private System.Windows.Forms.TextBox webDavUrlTextBox;
    private System.Windows.Forms.Label webDavUsernameLabel;
    private System.Windows.Forms.TextBox webDavUsernameTextBox;
    private System.Windows.Forms.Label webDavPasswordLabel;
    private System.Windows.Forms.TextBox webDavPasswordTextBox;
    private System.Windows.Forms.Label webDavDatabasePathLabel;
    private System.Windows.Forms.TextBox webDavDatabasePathTextBox;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.Button cancelButton;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        localGroupBox = new System.Windows.Forms.GroupBox();
        localDatabasePathLabel = new System.Windows.Forms.Label();
        localDatabasePathTextBox = new System.Windows.Forms.TextBox();
        browseButton = new System.Windows.Forms.Button();
        webDavGroupBox = new System.Windows.Forms.GroupBox();
        webDavUrlLabel = new System.Windows.Forms.Label();
        webDavUrlTextBox = new System.Windows.Forms.TextBox();
        webDavUsernameLabel = new System.Windows.Forms.Label();
        webDavUsernameTextBox = new System.Windows.Forms.TextBox();
        webDavPasswordLabel = new System.Windows.Forms.Label();
        webDavPasswordTextBox = new System.Windows.Forms.TextBox();
        webDavDatabasePathLabel = new System.Windows.Forms.Label();
        webDavDatabasePathTextBox = new System.Windows.Forms.TextBox();
        saveButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        localGroupBox.SuspendLayout();
        webDavGroupBox.SuspendLayout();
        SuspendLayout();
        // 
        // localGroupBox
        // 
        localGroupBox.Controls.Add(browseButton);
        localGroupBox.Controls.Add(localDatabasePathTextBox);
        localGroupBox.Controls.Add(localDatabasePathLabel);
        localGroupBox.Location = new System.Drawing.Point(12, 12);
        localGroupBox.Name = "localGroupBox";
        localGroupBox.Size = new System.Drawing.Size(560, 80);
        localGroupBox.TabIndex = 0;
        localGroupBox.TabStop = false;
        localGroupBox.Text = "Local Database";
        // 
        // localDatabasePathLabel
        // 
        localDatabasePathLabel.AutoSize = true;
        localDatabasePathLabel.Location = new System.Drawing.Point(16, 32);
        localDatabasePathLabel.Name = "localDatabasePathLabel";
        localDatabasePathLabel.Size = new System.Drawing.Size(99, 20);
        localDatabasePathLabel.TabIndex = 0;
        localDatabasePathLabel.Text = "Database Path:";
        // 
        // localDatabasePathTextBox
        // 
        localDatabasePathTextBox.Location = new System.Drawing.Point(130, 28);
        localDatabasePathTextBox.Name = "localDatabasePathTextBox";
        localDatabasePathTextBox.Size = new System.Drawing.Size(320, 27);
        localDatabasePathTextBox.TabIndex = 1;
        // 
        // browseButton
        // 
        browseButton.Location = new System.Drawing.Point(460, 27);
        browseButton.Name = "browseButton";
        browseButton.Size = new System.Drawing.Size(75, 29);
        browseButton.TabIndex = 2;
        browseButton.Text = "Browse";
        browseButton.UseVisualStyleBackColor = true;
        browseButton.Click += browseButton_Click;
        // 
        // webDavGroupBox
        // 
        webDavGroupBox.Controls.Add(webDavDatabasePathTextBox);
        webDavGroupBox.Controls.Add(webDavDatabasePathLabel);
        webDavGroupBox.Controls.Add(webDavPasswordTextBox);
        webDavGroupBox.Controls.Add(webDavPasswordLabel);
        webDavGroupBox.Controls.Add(webDavUsernameTextBox);
        webDavGroupBox.Controls.Add(webDavUsernameLabel);
        webDavGroupBox.Controls.Add(webDavUrlTextBox);
        webDavGroupBox.Controls.Add(webDavUrlLabel);
        webDavGroupBox.Location = new System.Drawing.Point(12, 110);
        webDavGroupBox.Name = "webDavGroupBox";
        webDavGroupBox.Size = new System.Drawing.Size(560, 180);
        webDavGroupBox.TabIndex = 1;
        webDavGroupBox.TabStop = false;
        webDavGroupBox.Text = "WebDAV Configuration";
        // 
        // webDavUrlLabel
        // 
        webDavUrlLabel.AutoSize = true;
        webDavUrlLabel.Location = new System.Drawing.Point(16, 32);
        webDavUrlLabel.Name = "webDavUrlLabel";
        webDavUrlLabel.Size = new System.Drawing.Size(34, 20);
        webDavUrlLabel.TabIndex = 0;
        webDavUrlLabel.Text = "URL:";
        // 
        // webDavUrlTextBox
        // 
        webDavUrlTextBox.Location = new System.Drawing.Point(130, 28);
        webDavUrlTextBox.Name = "webDavUrlTextBox";
        webDavUrlTextBox.Size = new System.Drawing.Size(405, 27);
        webDavUrlTextBox.TabIndex = 1;
        // 
        // webDavUsernameLabel
        // 
        webDavUsernameLabel.AutoSize = true;
        webDavUsernameLabel.Location = new System.Drawing.Point(16, 70);
        webDavUsernameLabel.Name = "webDavUsernameLabel";
        webDavUsernameLabel.Size = new System.Drawing.Size(78, 20);
        webDavUsernameLabel.TabIndex = 2;
        webDavUsernameLabel.Text = "Username:";
        // 
        // webDavUsernameTextBox
        // 
        webDavUsernameTextBox.Location = new System.Drawing.Point(130, 66);
        webDavUsernameTextBox.Name = "webDavUsernameTextBox";
        webDavUsernameTextBox.Size = new System.Drawing.Size(200, 27);
        webDavUsernameTextBox.TabIndex = 3;
        // 
        // webDavPasswordLabel
        // 
        webDavPasswordLabel.AutoSize = true;
        webDavPasswordLabel.Location = new System.Drawing.Point(16, 108);
        webDavPasswordLabel.Name = "webDavPasswordLabel";
        webDavPasswordLabel.Size = new System.Drawing.Size(73, 20);
        webDavPasswordLabel.TabIndex = 4;
        webDavPasswordLabel.Text = "Password:";
        // 
        // webDavPasswordTextBox
        // 
        webDavPasswordTextBox.Location = new System.Drawing.Point(130, 104);
        webDavPasswordTextBox.Name = "webDavPasswordTextBox";
        webDavPasswordTextBox.PasswordChar = '*';
        webDavPasswordTextBox.Size = new System.Drawing.Size(200, 27);
        webDavPasswordTextBox.TabIndex = 5;
        webDavPasswordTextBox.UseSystemPasswordChar = true;
        // 
        // webDavDatabasePathLabel
        // 
        webDavDatabasePathLabel.AutoSize = true;
        webDavDatabasePathLabel.Location = new System.Drawing.Point(16, 146);
        webDavDatabasePathLabel.Name = "webDavDatabasePathLabel";
        webDavDatabasePathLabel.Size = new System.Drawing.Size(99, 20);
        webDavDatabasePathLabel.TabIndex = 6;
        webDavDatabasePathLabel.Text = "Database Path:";
        // 
        // webDavDatabasePathTextBox
        // 
        webDavDatabasePathTextBox.Location = new System.Drawing.Point(130, 142);
        webDavDatabasePathTextBox.Name = "webDavDatabasePathTextBox";
        webDavDatabasePathTextBox.Size = new System.Drawing.Size(405, 27);
        webDavDatabasePathTextBox.TabIndex = 7;
        // 
        // saveButton
        // 
        saveButton.Location = new System.Drawing.Point(416, 310);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(75, 35);
        saveButton.TabIndex = 2;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += saveButton_Click;
        // 
        // cancelButton
        // 
        cancelButton.Location = new System.Drawing.Point(497, 310);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(75, 35);
        cancelButton.TabIndex = 3;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += cancelButton_Click;
        // 
        // SettingsForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(584, 361);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        Controls.Add(webDavGroupBox);
        Controls.Add(localGroupBox);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        Icon = new System.Drawing.Icon("favicon.ico");
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SettingsForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Settings";
        localGroupBox.ResumeLayout(false);
        localGroupBox.PerformLayout();
        webDavGroupBox.ResumeLayout(false);
        webDavGroupBox.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
}