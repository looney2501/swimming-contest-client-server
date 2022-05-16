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
    public class SwimmerRaceDBRepositoryTests
    {
        private readonly IDictionary<string, string> properties =
            DbUtils.GetDBPropertiesByName("mpp_lab_project_test.db");

        [Test]
        public void Add()
        {
            ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
            IRaceRepository raceRepository = new RaceDBRepository(properties);
            ISwimmerRaceRepository swimmerRaceRepository =
                new SwimmerRaceDBRepository(swimmerRepository, raceRepository, properties);

            var swimmer = new Swimmer("Gigi", "Ursu", 32);
            var id = swimmerRepository.Add(swimmer);
            swimmer.id = id;

            var race = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._200m, SwimmingStyle.Backstroke);

            var swimmerRace = new SwimmerRace(swimmer, race);
            swimmerRaceRepository.Add(swimmerRace);

            var connection = DbUtils.GetConnection(properties);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select id_swimmer, id_race from SwimmersRaces;";

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        var id_swimmer = dataReader.GetInt32(0);
                        var id_race = dataReader.GetInt32(1);
                        Assert.AreEqual(swimmer.id, id_swimmer);
                        Assert.AreEqual(race.id, id_race);
                    }
                }
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from SwimmersRaces;";
                command.ExecuteNonQuery();
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from Swimmers;";
                command.ExecuteNonQuery();
            }
        }

        [Test]
        public void GetNumberOfSwimmersForRace()
        {
            ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
            IRaceRepository raceRepository = new RaceDBRepository(properties);
            ISwimmerRaceRepository swimmerRaceRepository =
                new SwimmerRaceDBRepository(swimmerRepository, raceRepository, properties);

            var swimmer = new Swimmer("Gigi", "Ursu", 32);
            var id = swimmerRepository.Add(swimmer);
            swimmer.id = id;

            var race = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._200m, SwimmingStyle.Backstroke);

            Assert.AreEqual(0, swimmerRaceRepository.GetNumberOfSwimmersForRace(race));

            var swimmerRace = new SwimmerRace(swimmer, race);
            swimmerRaceRepository.Add(swimmerRace);

            Assert.AreEqual(1, swimmerRaceRepository.GetNumberOfSwimmersForRace(race));

            var connection = DbUtils.GetConnection(properties);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from SwimmersRaces;";
                command.ExecuteNonQuery();
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from Swimmers;";
                command.ExecuteNonQuery();
            }
        }

        [Test]
        public void FindAllSwimmersForRace()
        {
            ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
            IRaceRepository raceRepository = new RaceDBRepository(properties);
            ISwimmerRaceRepository swimmerRaceRepository =
                new SwimmerRaceDBRepository(swimmerRepository, raceRepository, properties);

            var swimmer1 = new Swimmer("Gigi", "Ursu", 32);
            var id1 = swimmerRepository.Add(swimmer1);
            swimmer1.id = id1;

            var swimmer2 = new Swimmer("Gigi2", "Ursu2", 22);
            var id2 = swimmerRepository.Add(swimmer2);
            swimmer2.id = id2;

            var race = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._200m, SwimmingStyle.Backstroke);

            var swimmerRace1 = new SwimmerRace(swimmer1, race);
            swimmerRaceRepository.Add(swimmerRace1);
            Assert.AreEqual(1, swimmerRaceRepository.FindAllSwimmersForRace(race).Count);

            var swimmerRace2 = new SwimmerRace(swimmer2, race);
            swimmerRaceRepository.Add(swimmerRace2);
            Assert.AreEqual(2, swimmerRaceRepository.FindAllSwimmersForRace(race).Count);

            var connection = DbUtils.GetConnection(properties);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from SwimmersRaces;";
                command.ExecuteNonQuery();
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from Swimmers;";
                command.ExecuteNonQuery();
            }
        }

        [Test]
        public void FindAllRacesForSwimmer()
        {
            ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
            IRaceRepository raceRepository = new RaceDBRepository(properties);
            ISwimmerRaceRepository swimmerRaceRepository =
                new SwimmerRaceDBRepository(swimmerRepository, raceRepository, properties);

            var swimmer = new Swimmer("Gigi", "Ursu", 32);
            var id = swimmerRepository.Add(swimmer);
            swimmer.id = id;

            var race1 = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._200m, SwimmingStyle.Backstroke);
            var race2 = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._50m, SwimmingStyle.Backstroke);

            var swimmerRace1 = new SwimmerRace(swimmer, race1);
            swimmerRaceRepository.Add(swimmerRace1);
            Assert.AreEqual(1, swimmerRaceRepository.FindAllRacesForSwimmer(swimmer).Count);

            var swimmerRace2 = new SwimmerRace(swimmer, race2);
            swimmerRaceRepository.Add(swimmerRace2);
            Assert.AreEqual(2, swimmerRaceRepository.FindAllRacesForSwimmer(swimmer).Count);

            var connection = DbUtils.GetConnection(properties);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from SwimmersRaces;";
                command.ExecuteNonQuery();
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from Swimmers;";
                command.ExecuteNonQuery();
            }
        }
    }
}