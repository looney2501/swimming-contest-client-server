using System;
using System.Collections.Generic;
using log4net;
using Model.Domain.Entities;
using Server.Utils;

namespace Server.Repository.DBRepository;

public class AdminDBRepository : IAdminRepository
{
    private static readonly ILog Logger = LogManager.GetLogger("AdminDBRepository");
    private readonly IDictionary<string, string> properties;

    public AdminDBRepository(IDictionary<string, string> properties)
    {
        Logger.InfoFormat("Initialising AdminDBRepository...");
        this.properties = properties;
    }

    public int Add(Admin elem)
    {
        throw new NotImplementedException();
    }

    public void Delete(Admin elem)
    {
        throw new NotImplementedException();
    }

    public void Update(Admin elem, int id)
    {
        throw new NotImplementedException();
    }

    public Admin FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Admin FindByUsernameAndPassword(string username, string password)
    {
        Logger.InfoFormat("FindByUsernameAndPassword(username = {0}, password = {1})", username, password);
        Admin admin = null;

        var connection = DbUtils.GetConnection(properties);
        using (var comm = connection.CreateCommand())
        {
            comm.CommandText = "select id from Admins where username = @username and password = @password;";

            var paramUsername = comm.CreateParameter();
            paramUsername.ParameterName = "@username";
            paramUsername.Value = username;
            comm.Parameters.Add(paramUsername);

            var paramPassword = comm.CreateParameter();
            paramPassword.ParameterName = "@password";
            paramPassword.Value = password;
            comm.Parameters.Add(paramPassword);

            using (var dataReader = comm.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    var id = dataReader.GetInt32(0);
                    admin = new Admin(id, username, password);
                }
            }
        }

        Logger.InfoFormat("Result: admin = {0}", admin);

        return admin;
    }
}