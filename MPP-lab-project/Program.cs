using System;
using System.Windows.Forms;
using log4net.Config;
using MPP_lab_project.Forms;
using MPP_lab_project.Repository.DBRepository;
using MPP_lab_project.Repository.Interfaces;
using MPP_lab_project.Utils;

namespace MPP_lab_project
{
    static class Program
    {
        static Service.Service GetService()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project.db");
            IAdminRepository adminRepository = new AdminDBRepository(properties);
            IRaceRepository raceRepository = new RaceDBRepository(properties);
            ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
            ISwimmerRaceRepository swimmerRaceRepository =
                new SwimmerRaceDBRepository(swimmerRepository, raceRepository, properties);
            Service.Service service = new Service.Service(adminRepository, swimmerRepository, raceRepository,
                swimmerRaceRepository);
            return service;
        }
        
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure();
            Service.Service service = GetService();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new LoginForm(service).Show();
            Application.Run();
        }
    }
}