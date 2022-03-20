using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Config;
using MPP_lab_project.Domain;
using MPP_lab_project.Repository.DBRepository;
using MPP_lab_project.Utils;

namespace MPP_lab_project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            XmlConfigurator.Configure();
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project.db");
            AdminDBRepository adminDbRepository = new AdminDBRepository(properties);
            // Debug.Assert(adminDbRepository.FindByUsernameAndPassword("admin", "admin").Equals(new Admin("admin", "admin")));

            // RaceDBRepository raceDbRepository = new RaceDBRepository(properties);
            // SwimmerDBRepository swimmerDbRepository = new SwimmerDBRepository(properties);
            // SwimmerRaceDBRepository swimmerRaceDbRepository =
            //     new SwimmerRaceDBRepository(swimmerDbRepository, raceDbRepository, properties);
            //
            // Debug.Assert(raceDbRepository.FindById(2).Equals(new Race(SwimmingDistances._50, SwimmingStyles.Freestyle, 0)));
        }
    }
}