using System.Collections.Generic;
using Model.Domain.Entities;
using Model.Domain.Enums;
using NUnit.Framework;
using Server.Repository;
using Server.Repository.DBRepository;
using Server.Utils;

namespace Tests.Repository.DBRepository
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
            
            Race race = new Race(SwimmingDistance._50m, SwimmingStyle.Mixed, 0);
            Assert.AreEqual(race, raceRepository.FindById(1));
        }

        [Test]
        public void FindByDistanceAndStyle()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project_test.db");

            IRaceRepository raceRepository = new RaceDBRepository(properties);

            Race race = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._50m, SwimmingStyle.Freestyle);
            Assert.AreEqual(SwimmingDistance._50m, race.Distance);
            Assert.AreEqual(SwimmingStyle.Freestyle, race.Style);
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