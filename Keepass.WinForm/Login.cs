namespace Keepass.WinForm;

public partial class Login : Form
{
    public Login()
    {
        InitializeComponent();
    }

    private void loginButton_Click(object sender, EventArgs e)
    {
        string password = passwordTextBox.Text;
        
        if (string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter a password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // TODO: Add actual password validation logic here
        MessageBox.Show($"Password entered: {password.Length} characters", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}