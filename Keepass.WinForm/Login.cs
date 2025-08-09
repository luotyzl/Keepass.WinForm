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

        // Hide login form and show main form
        this.Hide();
        Main mainForm = new Main();
        mainForm.Show();
    }
}