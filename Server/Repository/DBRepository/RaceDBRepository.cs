using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using Model.Domain.Entities;
using Model.Domain.Enums;
using Server.Utils;

namespace Server.Repository.DBRepository;

public class RaceDBRepository : IRaceRepository
{
    private static readonly ILog Logger = LogManager.GetLogger("RaceDbRepository");
    IDictionary<String, String> properties;

    public RaceDBRepository(IDictionary<string, string> properties)
    {
        Logger.InfoFormat("Initialising RaceDBRepository...");
        this.properties = properties;
    }

    public int Add(Race elem)
    {
        throw new System.NotImplementedException();
    }

    public void Delete(Race elem)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Race elem, int id)
    {
        throw new System.NotImplementedException();
    }

    public Race FindById(int id)
    {
        Logger.InfoFormat("FindById(id = {0})", id);
        Race race = null;

        IDbConnection connection = DbUtils.GetConnection(properties);
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "select distance, style, swimmersNumber from Races where id = @id;";

            IDbDataParameter paramID = command.CreateParameter();
            paramID.ParameterName = "id";
            paramID.Value = id;
            command.Parameters.Add(paramID);


            using (IDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    SwimmingDistances swimmingDistances =
                        SwimmingDistancesMethods.DistanceFromInteger(dataReader.GetInt32(0));
                    SwimmingStyles swimmingStyles = SwimmingStylesMethods.StyleFromInteger(dataReader.GetInt32(1));
                    Int32 swimmersNumber = dataReader.GetInt32(2);
                    race = new Race(id, swimmingDistances, swimmingStyles, swimmersNumber);
                }
            }
        }

        Logger.InfoFormat("Result: race = {0}", race);
        
        return race;
    }

    public Race FindRaceByDistanceAndStyle(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle)
    {
        Logger.InfoFormat("FindRaceByDistanceAndStyle(distance = {0}, style = {1})", swimmingDistance, swimmingStyle);
        Race race = null;
        
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "select id, swimmersNumber from Races where distance = @distance and style = @style;";

            IDbDataParameter paramDistance = command.CreateParameter();
            paramDistance.ParameterName = "distance";
            paramDistance.Value = SwimmingDistancesMethods.IntegerFromDistance(swimmingDistance);
            command.Parameters.Add(paramDistance);

            IDbDataParameter paramStyle = command.CreateParameter();
            paramStyle.ParameterName = "style";
            paramStyle.Value = SwimmingStylesMethods.IntegerFromStyle(swimmingStyle);
            command.Parameters.Add(paramStyle);

            using (IDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    Int32 id = dataReader.GetInt32(0);
                    Int32 swimmersNumber = dataReader.GetInt32(1);
                    race = new Race(id, swimmingDistance, swimmingStyle, swimmersNumber);
                }
            }
        }

        Logger.InfoFormat("Result: race = {0}", race);
        return race;
    }

    public List<Race> FindAllRaces()
    {
        Logger.InfoFormat("FindAllRaces()");
        List<Race> races = new List<Race>();
        
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "select id, distance, style, swimmersNumber from Races;";

            using (IDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    Int32 id = dataReader.GetInt32(0);
                    SwimmingDistances distance = SwimmingDistancesMethods.DistanceFromInteger(dataReader.GetInt32(1));
                    SwimmingStyles style = SwimmingStylesMethods.StyleFromInteger(dataReader.GetInt32(2));
                    Int32 swimmersNumber = dataReader.GetInt32(3);
                    races.Add(new Race(id, distance, style, swimmersNumber));
                }
            }
        }

        Logger.InfoFormat("Result: races = {0}", races);
        return races;
    }
}