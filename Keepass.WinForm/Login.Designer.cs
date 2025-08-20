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
        SuspendLayout();
        // 
        // passwordLabel
        // 
        passwordLabel.AutoSize = true;
        passwordLabel.Location = new System.Drawing.Point(64, 67);
        passwordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        passwordLabel.Name = "passwordLabel";
        passwordLabel.Size = new System.Drawing.Size(82, 20);
        passwordLabel.TabIndex = 0;
        passwordLabel.Text = "Password:";
        // 
        // passwordTextBox
        // 
        passwordTextBox.Location = new System.Drawing.Point(154, 63);
        passwordTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.PasswordChar = '*';
        passwordTextBox.Size = new System.Drawing.Size(256, 27);
        passwordTextBox.TabIndex = 1;
        passwordTextBox.UseSystemPasswordChar = true;
        // 
        // loginButton
        // 
        loginButton.Location = new System.Drawing.Point(315, 120);
        loginButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        loginButton.Name = "loginButton";
        loginButton.Size = new System.Drawing.Size(96, 31);
        loginButton.TabIndex = 2;
        loginButton.Text = "Login";
        loginButton.UseVisualStyleBackColor = true;
        loginButton.Click += loginButton_Click;
        // 
        // settingsButton
        // 
        settingsButton.Location = new System.Drawing.Point(12, 12);
        settingsButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        settingsButton.Name = "settingsButton";
        settingsButton.Size = new System.Drawing.Size(35, 35);
        settingsButton.TabIndex = 3;
        settingsButton.Text = "⚙";
        settingsButton.UseVisualStyleBackColor = true;
        settingsButton.Click += settingsButton_Click;
        // 
        // Login
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(514, 205);
        Controls.Add(settingsButton);
        Controls.Add(loginButton);
        Controls.Add(passwordTextBox);
        Controls.Add(passwordLabel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        Icon = new System.Drawing.Icon("favicon.ico");
        Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "KeePass Login";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}