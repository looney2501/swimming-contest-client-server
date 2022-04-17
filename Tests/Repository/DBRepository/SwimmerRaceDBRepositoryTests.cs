using System.Collections.Generic;
using System.Data;
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
        IDictionary<string, string> properties = DbUtils.GetDBPropertiesByName("mpp_lab_project_test.db");
        
        [Test]
        public void Add()
        {
            ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
            IRaceRepository raceRepository = new RaceDBRepository(properties);
            ISwimmerRaceRepository swimmerRaceRepository =
                new SwimmerRaceDBRepository(swimmerRepository, raceRepository, properties);

            Swimmer swimmer = new Swimmer("Gigi", "Ursu", 32);
            int id = swimmerRepository.Add(swimmer);
            swimmer.ID = id;

            Race race = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._200m, SwimmingStyle.Backstroke);

            SwimmerRace swimmerRace = new SwimmerRace(swimmer, race);
            swimmerRaceRepository.Add(swimmerRace);

            IDbConnection connection = DbUtils.GetConnection(properties);

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select id_swimmer, id_race from SwimmersRaces;";

                using (IDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        int id_swimmer = dataReader.GetInt32(0);
                        int id_race = dataReader.GetInt32(1);
                        Assert.AreEqual(swimmer.ID, id_swimmer);
                        Assert.AreEqual(race.ID, id_race);
                    }
                }
            }

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "delete from SwimmersRaces;";
                command.ExecuteNonQuery();
            }
            
            using (IDbCommand command = connection.CreateCommand())
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
            
            Swimmer swimmer = new Swimmer("Gigi", "Ursu", 32);
            int id = swimmerRepository.Add(swimmer);
            swimmer.ID = id;

            Race race = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._200m, SwimmingStyle.Backstroke);

            Assert.AreEqual(0, swimmerRaceRepository.GetNumberOfSwimmersForRace(race));
            
            SwimmerRace swimmerRace = new SwimmerRace(swimmer, race);
            swimmerRaceRepository.Add(swimmerRace);
            
            Assert.AreEqual(1, swimmerRaceRepository.GetNumberOfSwimmersForRace(race));

            IDbConnection connection = DbUtils.GetConnection(properties);
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "delete from SwimmersRaces;";
                command.ExecuteNonQuery();
            }
            
            using (IDbCommand command = connection.CreateCommand())
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
            
            Swimmer swimmer1 = new Swimmer("Gigi", "Ursu", 32);
            int id1 = swimmerRepository.Add(swimmer1);
            swimmer1.ID = id1;
            
            Swimmer swimmer2 = new Swimmer("Gigi2", "Ursu2", 22);
            int id2 = swimmerRepository.Add(swimmer2);
            swimmer2.ID = id2;
            
            Race race = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._200m, SwimmingStyle.Backstroke);

            SwimmerRace swimmerRace1 = new SwimmerRace(swimmer1, race);
            swimmerRaceRepository.Add(swimmerRace1);
            Assert.AreEqual(1, swimmerRaceRepository.FindAllSwimmersForRace(race).Count);

            SwimmerRace swimmerRace2 = new SwimmerRace(swimmer2, race);
            swimmerRaceRepository.Add(swimmerRace2);
            Assert.AreEqual(2, swimmerRaceRepository.FindAllSwimmersForRace(race).Count);
            
            IDbConnection connection = DbUtils.GetConnection(properties);
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "delete from SwimmersRaces;";
                command.ExecuteNonQuery();
            }
            
            using (IDbCommand command = connection.CreateCommand())
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
            
            Swimmer swimmer = new Swimmer("Gigi", "Ursu", 32);
            int id = swimmerRepository.Add(swimmer);
            swimmer.ID = id;
            
            Race race1 = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._200m, SwimmingStyle.Backstroke);
            Race race2 = raceRepository.FindRaceByDistanceAndStyle(SwimmingDistance._50m, SwimmingStyle.Backstroke);
            
            SwimmerRace swimmerRace1 = new SwimmerRace(swimmer, race1);
            swimmerRaceRepository.Add(swimmerRace1);
            Assert.AreEqual(1, swimmerRaceRepository.FindAllRacesForSwimmer(swimmer).Count);
            
            SwimmerRace swimmerRace2 = new SwimmerRace(swimmer, race2);
            swimmerRaceRepository.Add(swimmerRace2);
            Assert.AreEqual(2, swimmerRaceRepository.FindAllRacesForSwimmer(swimmer).Count);
            
            IDbConnection connection = DbUtils.GetConnection(properties);
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "delete from SwimmersRaces;";
                command.ExecuteNonQuery();
            }
            
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "delete from Swimmers;";
                command.ExecuteNonQuery();
            }
        }
    }
}