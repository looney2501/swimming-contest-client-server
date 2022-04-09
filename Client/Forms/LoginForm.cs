using System;
using System.Windows.Forms;
using Server.Services;

namespace Client.Forms
{
    public partial class LoginForm : GenericForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            if (username.Length == 0)
            {
                MessageBox.Show(@"Introduceti un nume de utilizator!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (password.Length == 0)
            {
                MessageBox.Show(@"Introduceti o parola!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Services.IsExistingUser(username, password))
            {
                MainForm mainForm = new MainForm();
                mainForm.Services = Services;
                mainForm.LoginForm = this;
                Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show(@"Username sau parola sunt gresite!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}