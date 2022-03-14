using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using MPP_lab_project.Domain;
using MPP_lab_project.Repository.Interfaces;
using MPP_lab_project.Utils;

namespace MPP_lab_project.Repository.DBRepository;

public class SwimmerDBRepository : ISwimmerRepository
{
    private static readonly ILog Logger = LogManager.GetLogger("RaceDbRepository");
    IDictionary<String, String> properties;

    public SwimmerDBRepository(IDictionary<string, string> properties)
    {
        Logger.InfoFormat("Initialising SwimmerDBRepository with properties {0}", properties);
        this.properties = properties;
    }

    public int Add(Swimmer elem)
    {
        Logger.InfoFormat("Add(swimmer = {0})", elem);

        int id;
        
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (IDbCommand comm = connection.CreateCommand())
        {
            comm.CommandText = "insert into Swimmers (firstName, lastName, age) values (@firstName, @lastName, @age);";
            
            IDbDataParameter paramFirstName = comm.CreateParameter();
            paramFirstName.ParameterName = "@firstName";
            paramFirstName.Value = elem.FirstName;
            comm.Parameters.Add(paramFirstName);
            
            IDbDataParameter paramLastName = comm.CreateParameter();
            paramLastName.ParameterName = "@lastName";
            paramLastName.Value = elem.LastName;
            comm.Parameters.Add(paramLastName);
            
            IDbDataParameter paramAge = comm.CreateParameter();
            paramAge.ParameterName = "@age";
            paramAge.Value = elem.Age;
            comm.Parameters.Add(paramAge);

            id = (int) comm.ExecuteScalar();
        }
        
        Logger.InfoFormat("Result: id = {0}", id);
        return id;
    }

    public void Delete(Swimmer elem)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Swimmer elem, int id)
    {
        throw new System.NotImplementedException();
    }

    public Swimmer FindById(int id)
    {
        Logger.InfoFormat("FindById(id = {0})", id);
        Swimmer swimmer = null;
        
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "select firstName, lastName, age from main.Swimmers where id = @id;";

            IDbDataParameter paramID = command.CreateParameter();
            paramID.ParameterName = "id";
            paramID.Value = id;
            command.Parameters.Add(paramID);
            
            using (IDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    String firstName = dataReader.GetString(1);
                    String lastName = dataReader.GetString(2);
                    Int32 age = dataReader.GetInt32(3);
                    swimmer = new Swimmer(id, firstName, lastName, age);
                }
            }
        }
        
        Logger.InfoFormat("Result: swimmer = {0}", swimmer);
        
        return swimmer;
    }
}