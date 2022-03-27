using System;
using System.Windows.Forms;

namespace MPP_lab_project.Forms;

public partial class MainForm : GenericForm
{
    private Service.Service _service;
    public MainForm(Service.Service service) : base(service)
    {
        _service = service;
        InitializeComponent();
    }

    private void exitButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void logoutButton_Click(object sender, EventArgs e)
    {
        Close();
        new LoginForm(service).Show();
    }
}