using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using MPP_lab_project.Domain;
using MPP_lab_project.Repository.Interfaces;
using MPP_lab_project.Utils;

namespace MPP_lab_project.Repository.DBRepository;

public class SwimmerRaceDBRepository : ISwimmerRaceRepository
{
    private static readonly ILog Logger = LogManager.GetLogger("RaceDbRepository");
    IDictionary<String, String> properties;
    
    public SwimmerDBRepository SwimmerDbRepository { get; set; }
    public RaceDBRepository RaceDbRepository { get; set; }

    public SwimmerRaceDBRepository(SwimmerDBRepository swimmerDbRepository, RaceDBRepository raceDbRepository, IDictionary<string, string> properties)
    {
        Logger.InfoFormat("Initialising RaceDBRepository with properties {0}", properties);
        this.properties = properties;
    }

    public int Add(SwimmerRace elem)
    {
        Logger.InfoFormat("Add(swimmer = {0})", elem);
        
        int id;
        
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (IDbCommand comm = connection.CreateCommand())
        {
            comm.CommandText = "insert into SwimmersRaces (id_swimmer, id_race) values (@idSwimmer, @idRace);";
            
            IDbDataParameter paramIDSwimmer = comm.CreateParameter();
            paramIDSwimmer.ParameterName = "@idSwimmer";
            paramIDSwimmer.Value = elem.Swimmer.ID;
            comm.Parameters.Add(paramIDSwimmer);
            
            IDbDataParameter paramIDRace = comm.CreateParameter();
            paramIDRace.ParameterName = "@idRace";
            paramIDRace.Value = elem.Race.ID;
            comm.Parameters.Add(paramIDRace);

            id = (int) comm.ExecuteScalar();
        }
        
        Logger.InfoFormat("Result: id = {0}", id);
        return id;
    }

    public void Delete(SwimmerRace elem)
    {
        throw new System.NotImplementedException();
    }

    public void Update(SwimmerRace elem, int id)
    {
        throw new System.NotImplementedException();
    }

    public SwimmerRace FindById(int id)
    {
        throw new System.NotImplementedException();
    }

    public int GetNumberOfSwimmersForRace(Race race)
    {
        Logger.InfoFormat("GetNumberOfSwimmersForRace(race = {0})", race);

        int noSwimmers = 0;
        
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "select count(*) from SwimmersRaces where id_race = @idRace;";

            IDbDataParameter paramIDRace = command.CreateParameter();
            paramIDRace.ParameterName = "idRace";
            paramIDRace.Value = race.ID;
            command.Parameters.Add(paramIDRace);
            
            using (IDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    noSwimmers = dataReader.GetInt32(1);
                }
            }
        }
        
        Logger.InfoFormat("Result: numberOfSwimmers = {0}", noSwimmers);
        return noSwimmers;
    }

    public List<Swimmer> FindAllSwimmersForRace(Race race)
    {
        Logger.InfoFormat("FindAllSwimmersForRace(race = {0})", race);

        List<Swimmer> swimmers = new List<Swimmer>();
        
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText =
                "select S.id, S.firstName, S.lastName, S.age from Swimmers S inner join SwimmersRaces SR on S.id = SR.id_swimmer where SR.id_race = ?;";

            IDbDataParameter paramIDRace = command.CreateParameter();
            paramIDRace.ParameterName = "idRace";
            paramIDRace.Value = race.ID;
            command.Parameters.Add(paramIDRace);
            
            using (IDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int id = dataReader.GetInt32(1);
                    String firstName = dataReader.GetString(2);
                    String lastName = dataReader.GetString(3);
                    int age = dataReader.GetInt32(4);
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

        List<Race> allRaces = new List<Race>();
        
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText =
                "select R.id, R.distance, R.style, R.swimmersNumber from main.Races R inner join SwimmersRaces SR on R.id = SR.id_race where SR.id_swimmer = @idSwimmer;";

            IDbDataParameter paramIDSwimmer = command.CreateParameter();
            paramIDSwimmer.ParameterName = "idSwimmer";
            paramIDSwimmer.Value = swimmer.ID;
            command.Parameters.Add(paramIDSwimmer);
            
            using (IDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int id = dataReader.GetInt32(1);
                    SwimmingDistances swimmingDistances =
                        SwimmingDistancesMethods.DistanceFromInteger(dataReader.GetInt32(1));
                    SwimmingStyles swimmingStyles = SwimmingStylesMethods.StyleFromInteger(dataReader.GetInt32(2));
                    int swimmersNumber = dataReader.GetInt32(4);
                    allRaces.Add(new Race(id, swimmingDistances, swimmingStyles, swimmersNumber));
                }
            }
        }
        
        Logger.InfoFormat("Result: allRacesForSwimmer = {0}", allRaces);
        return allRaces;
    }
}