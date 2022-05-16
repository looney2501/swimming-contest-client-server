using System;
using System.Collections.Generic;
using log4net;
using Model.Domain.Entities;
using Model.Domain.Enums;
using Server.Utils;

namespace Server.Repository.DBRepository;

public class SwimmerRaceDBRepository : ISwimmerRaceRepository
{
    private static readonly ILog Logger = LogManager.GetLogger("RaceDbRepository");
    private readonly IDictionary<string, string> properties;

    public SwimmerRaceDBRepository(ISwimmerRepository swimmerRepository, IRaceRepository raceRepository,
        IDictionary<string, string> properties)
    {
        Logger.InfoFormat("Initialising RaceDBRepository...");
        this.properties = properties;
        SwimmerRepository = swimmerRepository;
        RaceRepository = raceRepository;
    }

    public ISwimmerRepository SwimmerRepository { get; set; }
    public IRaceRepository RaceRepository { get; set; }

    public int Add(SwimmerRace elem)
    {
        Logger.InfoFormat("Add(swimmer = {0})", elem);

        var id = -1;

        var connection = DbUtils.GetConnection(properties);
        using (var comm = connection.CreateCommand())
        {
            comm.CommandText =
                "insert into SwimmersRaces (id_swimmer, id_race) values (@idSwimmer, @idRace) returning id;";

            var paramIDSwimmer = comm.CreateParameter();
            paramIDSwimmer.ParameterName = "@idSwimmer";
            paramIDSwimmer.Value = elem.Swimmer.id;
            comm.Parameters.Add(paramIDSwimmer);

            var paramIDRace = comm.CreateParameter();
            paramIDRace.ParameterName = "@idRace";
            paramIDRace.Value = elem.Race.id;
            comm.Parameters.Add(paramIDRace);

            id = Convert.ToInt32(comm.ExecuteScalar());
        }

        Logger.InfoFormat("Result: id = {0}", id);
        return id;
    }

    public void Delete(SwimmerRace elem)
    {
        throw new NotImplementedException();
    }

    public void Update(SwimmerRace elem, int id)
    {
        throw new NotImplementedException();
    }

    public SwimmerRace FindById(int id)
    {
        throw new NotImplementedException();
    }

    public int GetNumberOfSwimmersForRace(Race race)
    {
        Logger.InfoFormat("GetNumberOfSwimmersForRace(race = {0})", race);

        var noSwimmers = 0;

        var connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select count(*) from SwimmersRaces where id_race = @idRace;";

            var paramIDRace = command.CreateParameter();
            paramIDRace.ParameterName = "idRace";
            paramIDRace.Value = race.id;
            command.Parameters.Add(paramIDRace);

            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read()) noSwimmers = dataReader.GetInt32(0);
            }
        }

        Logger.InfoFormat("Result: numberOfSwimmers = {0}", noSwimmers);
        return noSwimmers;
    }

    public List<Swimmer> FindAllSwimmersForRace(Race race)
    {
        Logger.InfoFormat("FindAllSwimmersForRace(race = {0})", race);

        var swimmers = new List<Swimmer>();

        var connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText =
                "select S.id, S.firstName, S.lastName, S.age from Swimmers S inner join SwimmersRaces SR on S.id = SR.id_swimmer where SR.id_race = ?;";

            var paramIDRace = command.CreateParameter();
            paramIDRace.ParameterName = "idRace";
            paramIDRace.Value = race.id;
            command.Parameters.Add(paramIDRace);

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var id = dataReader.GetInt32(0);
                    var firstName = dataReader.GetString(1);
                    var lastName = dataReader.GetString(2);
                    var age = dataReader.GetInt32(3);
                    swimmers.Add(new Swimmer(id, firstName, lastName, age));
                }
            }
        }

        Logger.InfoFormat("Result: allSwimmersForRace = {0}", swimmers);
        return swimmers;
    }

    public List<Race> FindAllRacesForSwimmer(Swimmer swimmer)
    {
        Logger.InfoFormat("FindAllRacesForSwimmer(swimmer = {0})", swimmer);

        var allRaces = new List<Race>();

        var connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText =
                "select R.id, R.distance, R.style, R.swimmersNumber from main.Races R inner join SwimmersRaces SR on R.id = SR.id_race where SR.id_swimmer = @idSwimmer;";

            var paramIDSwimmer = command.CreateParameter();
            paramIDSwimmer.ParameterName = "idSwimmer";
            paramIDSwimmer.Value = swimmer.id;
            command.Parameters.Add(paramIDSwimmer);

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var id = dataReader.GetInt32(0);
                    var swimmingDistance =
                        SwimmingDistancesMethods.DistanceFromInteger(dataReader.GetInt32(1));
                    var swimmingStyle = SwimmingStylesMethods.StyleFromInteger(dataReader.GetInt32(2));
                    var swimmersNumber = dataReader.GetInt32(3);
                    allRaces.Add(new Race(id, swimmingDistance, swimmingStyle, swimmersNumber));
                }
            }
        }

        Logger.InfoFormat("Result: allRacesForSwimmer = {0}", allRaces);
        return allRaces;
    }
}