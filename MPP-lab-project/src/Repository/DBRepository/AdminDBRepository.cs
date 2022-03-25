using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using log4net.Config;
using log4net.Util;
using MPP_lab_project.Domain;
using MPP_lab_project.Repository.Interfaces;
using MPP_lab_project.Utils;

namespace MPP_lab_project.Repository.DBRepository;

public class AdminDBRepository : IAdminRepository
{
    private static readonly ILog Logger = LogManager.GetLogger("AdminDBRepository");
    IDictionary<String, String> properties;

    public AdminDBRepository(IDictionary<String, String> properties)
    {
        Logger.InfoFormat("Initialising AdminDBRepository...");
        this.properties = properties;
    }

    public int Add(Admin elem)
    {
        throw new System.NotImplementedException();
    }

    public void Delete(Admin elem)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Admin elem, int id)
    {
        throw new System.NotImplementedException();
    }

    public Admin FindById(int id)
    {
        throw new System.NotImplementedException();
    }

    public Admin FindByUsernameAndPassword(String username, String password)
    {
        Logger.InfoFormat("FindByUsernameAndPassword(username = {0}, password = {1})", username, password);
        Admin admin = null;

        IDbConnection connection = DbUtils.GetConnection(properties);
        using (IDbCommand comm = connection.CreateCommand())
        {
            comm.CommandText = "select id from Admins where username = @username and password = @password;";
        
            IDbDataParameter paramUsername = comm.CreateParameter();
            paramUsername.ParameterName = "@username";
            paramUsername.Value = username;
            comm.Parameters.Add(paramUsername);
        
            IDbDataParameter paramPassword = comm.CreateParameter();
            paramPassword.ParameterName = "@password";
            paramPassword.Value = password;
            comm.Parameters.Add(paramPassword);

            using (IDataReader dataReader = comm.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    Int32 id = dataReader.GetInt32(0);
                    admin = new Admin(id, username, password);
                }
            }
        }
        Logger.InfoFormat("Result: admin = {0}", admin);
        
        return admin;
    }
}