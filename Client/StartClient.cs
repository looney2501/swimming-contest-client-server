using System;
using System.Windows.Forms;
using Client.Forms;
using log4net.Config;
using Server.Repository;
using Server.Repository.DBRepository;
using Server.Services;
using Server.Utils;

namespace Client
{
    static class Program
    {
        
        static Services LoadServices()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project.db");
            IAdminRepository adminRepository = new AdminDBRepository(properties);
            IRaceRepository raceRepository = new RaceDBRepository(properties);
            ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
            ISwimmerRaceRepository swimmerRaceRepository =
                new SwimmerRaceDBRepository(swimmerRepository, raceRepository, properties);
            Services services = new Services(adminRepository, swimmerRepository, raceRepository,
                swimmerRaceRepository);
            return services;
        }
        
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure();
            Services services = LoadServices();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm loginForm = new LoginForm();
            loginForm.Services = services;
            Application.Run(loginForm);
        }
    }
}