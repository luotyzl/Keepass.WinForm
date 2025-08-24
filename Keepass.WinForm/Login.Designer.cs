using System.Drawing;
using static System.Drawing.FontStyle;

namespace Keepass.WinForm;

partial class Login
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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

    private System.Windows.Forms.Label passwordLabel;
    private System.Windows.Forms.TextBox passwordTextBox;
    private System.Windows.Forms.Button loginButton;
    private System.Windows.Forms.Button settingsButton;
    private System.Windows.Forms.Label errorMessageLabel;
    private System.Windows.Forms.Label databaseFileLabel;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        passwordLabel = new System.Windows.Forms.Label();
        passwordTextBox = new System.Windows.Forms.TextBox();
        loginButton = new System.Windows.Forms.Button();
        settingsButton = new System.Windows.Forms.Button();
        errorMessageLabel = new System.Windows.Forms.Label();
        databaseFileLabel = new System.Windows.Forms.Label();
        SuspendLayout();
        databaseFileLabel.AutoSize = true;
        databaseFileLabel.ForeColor = System.Drawing.Color.Gray;
        databaseFileLabel.Location = new System.Drawing.Point(103, 10);
        databaseFileLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        databaseFileLabel.Name = "databaseFileLabel";
        databaseFileLabel.Size = new System.Drawing.Size(120, 20);
        databaseFileLabel.TabIndex = 5;
        databaseFileLabel.Text = "No database file";
        passwordLabel.AutoSize = true;
        passwordLabel.Location = new System.Drawing.Point(13, 36);
        passwordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        passwordLabel.Name = "passwordLabel";
        passwordLabel.Size = new System.Drawing.Size(82, 20);
        passwordLabel.TabIndex = 0;
        passwordLabel.Text = "Password:";
        passwordTextBox.Location = new System.Drawing.Point(103, 33);
        passwordTextBox.Margin = new System.Windows.Forms.Padding(4);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.PasswordChar = '*';
        passwordTextBox.PlaceholderText = "master password";
        passwordTextBox.Size = new System.Drawing.Size(256, 27);
        passwordTextBox.TabIndex = 1;
        passwordTextBox.UseSystemPasswordChar = true;
        passwordTextBox.TextChanged += passwordTextBox_TextChanged;
        passwordTextBox.KeyDown += passwordTextBox_KeyDown;
        loginButton.Location = new System.Drawing.Point(367, 33);
        loginButton.Margin = new System.Windows.Forms.Padding(4);
        loginButton.Name = "loginButton";
        loginButton.Size = new System.Drawing.Size(74, 27);
        loginButton.TabIndex = 2;
        loginButton.Text = "↩";
        loginButton.UseVisualStyleBackColor = true;
        loginButton.Click += loginButton_Click;
        settingsButton.Location = new System.Drawing.Point(13, 85);
        settingsButton.Margin = new System.Windows.Forms.Padding(4);
        settingsButton.Name = "settingsButton";
        settingsButton.Size = new System.Drawing.Size(35, 35);
        settingsButton.TabIndex = 3;
        settingsButton.Text = "⚙";
        settingsButton.UseVisualStyleBackColor = true;
        settingsButton.Click += settingsButton_Click;
        errorMessageLabel.AutoSize = true;
        errorMessageLabel.ForeColor = System.Drawing.Color.Red;
        errorMessageLabel.Location = new System.Drawing.Point(154, 100);
        errorMessageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        errorMessageLabel.Name = "errorMessageLabel";
        errorMessageLabel.Size = new System.Drawing.Size(0, 20);
        errorMessageLabel.TabIndex = 4;
        errorMessageLabel.Visible = false;
        AcceptButton = loginButton;
        AutoScaleDimensions = new System.Drawing.SizeF(9, 20);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(464, 137);
        Controls.Add(databaseFileLabel);
        Controls.Add(errorMessageLabel);
        Controls.Add(settingsButton);
        Controls.Add(loginButton);
        Controls.Add(passwordTextBox);
        Controls.Add(passwordLabel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        Margin = new System.Windows.Forms.Padding(4);
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KeePass Login";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}