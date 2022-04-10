using System;
using System.Windows.Forms;
using Client.Forms;
using Client.Services;
using log4net.Config;
using Model.Services;
using Server.Repository;
using Server.Repository.DBRepository;
using Server.Services;
using Server.Utils;

namespace Client
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ISwimmingRaceServices services = new SwimmingRaceServicesProxy("127.0.0.1", 55556);
            LoginForm loginForm = new LoginForm();
            loginForm.SwimmingRaceServicesServer = services;
            Application.Run(loginForm);
        }
    }
}