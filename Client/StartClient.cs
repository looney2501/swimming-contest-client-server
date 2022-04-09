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
        
        static SwimmingRaceServicesServer LoadServices()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project.db");
            IAdminRepository adminRepository = new AdminDBRepository(properties);
            IRaceRepository raceRepository = new RaceDBRepository(properties);
            ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
            ISwimmerRaceRepository swimmerRaceRepository =
                new SwimmerRaceDBRepository(swimmerRepository, raceRepository, properties);
            SwimmingRaceServicesServer swimmingRaceServicesServer = new SwimmingRaceServicesServer(adminRepository, swimmerRepository, raceRepository,
                swimmerRaceRepository);
            return swimmingRaceServicesServer;
        }
        
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure();
            SwimmingRaceServicesServer swimmingRaceServicesServer = LoadServices();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm loginForm = new LoginForm();
            loginForm.SwimmingRaceServicesServer = swimmingRaceServicesServer;
            Application.Run(loginForm);
        }
    }
}