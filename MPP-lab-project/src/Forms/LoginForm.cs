using System;
using System.Windows.Forms;

namespace MPP_lab_project.Forms
{
    public partial class LoginForm : GenericForm
    {
        public LoginForm(Service.Service service) : base(service)
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
            else if (service.IsExistingUser(username, password))
            {
                Close();
                new MainForm(service).Show();
            }
            else
            {
                MessageBox.Show(@"Username sau parola sunt gresite!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}