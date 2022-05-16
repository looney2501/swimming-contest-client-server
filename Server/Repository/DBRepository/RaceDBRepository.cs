using System;
using System.Collections.Generic;
using log4net;
using Model.Domain.Entities;
using Model.Domain.Enums;
using Server.Utils;

namespace Server.Repository.DBRepository;

public class RaceDBRepository : IRaceRepository
{
    private static readonly ILog Logger = LogManager.GetLogger("RaceDbRepository");
    private readonly IDictionary<string, string> properties;

    public RaceDBRepository(IDictionary<string, string> properties)
    {
        Logger.InfoFormat("Initialising RaceDBRepository...");
        this.properties = properties;
    }

    public int Add(Race elem)
    {
        throw new NotImplementedException();
    }

    public void Delete(Race elem)
    {
        throw new NotImplementedException();
    }

    public void Update(Race elem, int id)
    {
        throw new NotImplementedException();
    }

    public Race FindById(int id)
    {
        Logger.InfoFormat("FindById(id = {0})", id);
        Race race = null;

        var connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select distance, style, swimmersNumber from Races where id = @id;";

            var paramID = command.CreateParameter();
            paramID.ParameterName = "id";
            paramID.Value = id;
            command.Parameters.Add(paramID);


            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    var swimmingDistance =
                        SwimmingDistancesMethods.DistanceFromInteger(dataReader.GetInt32(0));
                    var swimmingStyle = SwimmingStylesMethods.StyleFromInteger(dataReader.GetInt32(1));
                    var swimmersNumber = dataReader.GetInt32(2);
                    race = new Race(id, swimmingDistance, swimmingStyle, swimmersNumber);
                }
            }
        }

        Logger.InfoFormat("Result: race = {0}", race);

        return race;
    }

    public Race FindRaceByDistanceAndStyle(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle)
    {
        Logger.InfoFormat("FindRaceByDistanceAndStyle(distance = {0}, style = {1})", swimmingDistance, swimmingStyle);
        Race race = null;

        var connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select id, swimmersNumber from Races where distance = @distance and style = @style;";

            var paramDistance = command.CreateParameter();
            paramDistance.ParameterName = "distance";
            paramDistance.Value = SwimmingDistancesMethods.IntegerFromDistance(swimmingDistance);
            command.Parameters.Add(paramDistance);

            var paramStyle = command.CreateParameter();
            paramStyle.ParameterName = "style";
            paramStyle.Value = SwimmingStylesMethods.IntegerFromStyle(swimmingStyle);
            command.Parameters.Add(paramStyle);

            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    var id = dataReader.GetInt32(0);
                    var swimmersNumber = dataReader.GetInt32(1);
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
        var races = new List<Race>();

        var connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select id, distance, style, swimmersNumber from Races;";

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var id = dataReader.GetInt32(0);
                    var distance = SwimmingDistancesMethods.DistanceFromInteger(dataReader.GetInt32(1));
                    var style = SwimmingStylesMethods.StyleFromInteger(dataReader.GetInt32(2));
                    var swimmersNumber = dataReader.GetInt32(3);
                    races.Add(new Race(id, distance, style, swimmersNumber));
                }
            }
        }

        Logger.InfoFormat("Result: races = {0}", races);
        return races;
    }
}