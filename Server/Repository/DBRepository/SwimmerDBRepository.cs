using System;
using System.Collections.Generic;
using log4net;
using Model.Domain.Entities;
using Server.Utils;

namespace Server.Repository.DBRepository;

public class SwimmerDBRepository : ISwimmerRepository
{
    private static readonly ILog Logger = LogManager.GetLogger("RaceDbRepository");
    private readonly IDictionary<string, string> properties;

    public SwimmerDBRepository(IDictionary<string, string> properties)
    {
        Logger.InfoFormat("Initialising SwimmerDBRepository...");
        this.properties = properties;
    }

    public int Add(Swimmer elem)
    {
        Logger.InfoFormat("Add(swimmer = {0})", elem);

        var id = -1;

        var connection = DbUtils.GetConnection(properties);
        using (var comm = connection.CreateCommand())
        {
            comm.CommandText =
                "insert into Swimmers (firstName, lastName, age) values (@firstName, @lastName, @age) returning id;";

            var paramFirstName = comm.CreateParameter();
            paramFirstName.ParameterName = "@firstName";
            paramFirstName.Value = elem.FirstName;
            comm.Parameters.Add(paramFirstName);

            var paramLastName = comm.CreateParameter();
            paramLastName.ParameterName = "@lastName";
            paramLastName.Value = elem.LastName;
            comm.Parameters.Add(paramLastName);

            var paramAge = comm.CreateParameter();
            paramAge.ParameterName = "@age";
            paramAge.Value = elem.Age;
            comm.Parameters.Add(paramAge);

            id = Convert.ToInt32(comm.ExecuteScalar());
        }

        Logger.InfoFormat("Result: id = {0}", id);
        return id;
    }

    public void Delete(Swimmer elem)
    {
        throw new NotImplementedException();
    }

    public void Update(Swimmer elem, int id)
    {
        throw new NotImplementedException();
    }

    public Swimmer FindById(int id)
    {
        Logger.InfoFormat("FindById(id = {0})", id);
        Swimmer swimmer = null;

        var connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select firstName, lastName, age from main.Swimmers where id = @id;";

            var paramID = command.CreateParameter();
            paramID.ParameterName = "id";
            paramID.Value = id;
            command.Parameters.Add(paramID);

            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    var firstName = dataReader.GetString(0);
                    var lastName = dataReader.GetString(1);
                    var age = dataReader.GetInt32(2);
                    swimmer = new Swimmer(id, firstName, lastName, age);
                }
            }
        }

        Logger.InfoFormat("Result: swimmer = {0}", swimmer);

        return swimmer;
    }
}