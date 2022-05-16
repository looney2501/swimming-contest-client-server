using System;
using System.Windows.Forms;
using Model.Services;

namespace Client.Forms;

public partial class LoginForm : GenericForm
{
    public LoginForm()
    {
        InitializeComponent();
        Text = @"Login";
    }

    private void loginButton_Click(object sender, EventArgs e)
    {
        var username = usernameTextBox.Text;
        var password = passwordTextBox.Text;
        if (username.Length == 0)
            MessageBox.Show(@"Introduceti un nume de utilizator!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        else if (password.Length == 0)
            MessageBox.Show(@"Introduceti o parola!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        else
            try
            {
                var mainForm = new MainForm();
                mainForm.SwimmingRaceServicesServer = SwimmingRaceServicesServer;
                mainForm.LoginForm = this;
                mainForm.LoggedUsername = username;

                SwimmingRaceServicesServer.Login(username, password, mainForm);
                mainForm.Show();

                ResetTextBoxes();
                Hide();
            }
            catch (ServicesException ex)
            {
                MessageBox.Show(ex.Message);
            }
    }

    private void ResetTextBoxes()
    {
        usernameTextBox.Clear();
        passwordTextBox.Clear();
    }
}