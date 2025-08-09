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

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.passwordLabel = new System.Windows.Forms.Label();
        this.passwordTextBox = new System.Windows.Forms.TextBox();
        this.loginButton = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // passwordLabel
        // 
        this.passwordLabel.AutoSize = true;
        this.passwordLabel.Location = new System.Drawing.Point(50, 50);
        this.passwordLabel.Name = "passwordLabel";
        this.passwordLabel.Size = new System.Drawing.Size(57, 15);
        this.passwordLabel.TabIndex = 0;
        this.passwordLabel.Text = "Password:";
        // 
        // passwordTextBox
        // 
        this.passwordTextBox.Location = new System.Drawing.Point(120, 47);
        this.passwordTextBox.Name = "passwordTextBox";
        this.passwordTextBox.PasswordChar = '*';
        this.passwordTextBox.Size = new System.Drawing.Size(200, 23);
        this.passwordTextBox.TabIndex = 1;
        this.passwordTextBox.UseSystemPasswordChar = true;
        // 
        // loginButton
        // 
        this.loginButton.Location = new System.Drawing.Point(245, 90);
        this.loginButton.Name = "loginButton";
        this.loginButton.Size = new System.Drawing.Size(75, 23);
        this.loginButton.TabIndex = 2;
        this.loginButton.Text = "Login";
        this.loginButton.UseVisualStyleBackColor = true;
        this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
        // 
        // Login
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 200);
        this.Controls.Add(this.loginButton);
        this.Controls.Add(this.passwordTextBox);
        this.Controls.Add(this.passwordLabel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "Login";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "KeePass Login";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}