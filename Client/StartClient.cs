using System;
using System.Windows.Forms;
using Client.Forms;
using Client.Services;
using log4net.Config;
using Model.Services;

namespace Client;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        XmlConfigurator.Configure();

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        ISwimmingRaceServices services = new SwimmingRaceServicesProxy("127.0.0.1", 55556);
        var loginForm = new LoginForm();
        loginForm.SwimmingRaceServicesServer = services;
        Application.Run(loginForm);
    }
}