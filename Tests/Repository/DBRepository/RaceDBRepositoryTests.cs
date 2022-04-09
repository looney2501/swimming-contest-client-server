using System.Collections.Generic;
using MPP_lab_project.Domain;
using MPP_lab_project.Repository.Interfaces;
using MPP_lab_project.Repository.DBRepository;
using MPP_lab_project.Utils;
using NUnit.Framework;

namespace Tests.tests.Repository.DBRepository
{
    [TestFixture]
    public class RaceDBRepositoryTests
    {
        [Test]
        public void FindByIDTest()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project_test.db");

            IRaceRepository raceRepository = new RaceDBRepository(properties);
            Assert.IsNull(raceRepository.FindById(30));
            
            Race race = new Race(SwimmingDistances._50m, SwimmingStyles.Mixed, 0);
            Assert.AreEqual(race, raceRepository.FindById(1));
        }

        [Test]
        public void FindByDistanceAndStyle()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project_test.db");

            IRaceRepository raceRepository = new RaceDBRepository(properties);

            Race race = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistances._50m, SwimmingStyles.Freestyle);
            Assert.AreEqual(SwimmingDistances._50m, race.Distance);
            Assert.AreEqual(SwimmingStyles.Freestyle, race.Style);
        }

        [Test]
        public void FindAllRaces()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project_test.db");
            
            IRaceRepository raceRepository = new RaceDBRepository(properties);
            List<Race> races = raceRepository.FindAllRaces();
            
            Assert.AreEqual(16, races.Count);
        }
    }
}