using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using log4net;

namespace Server.Utils;

public static class DbUtils
{
    private static IDbConnection _instance = null;
    private static readonly ILog Logger = LogManager.GetLogger("DBUtils");
    
    public static IDbConnection GetConnection(IDictionary<string,string> props)
    {
        if (_instance == null || _instance.State == ConnectionState.Closed)
        {
            String connectionString = props["ConnectionString"];
            
            Logger.InfoFormat("trying to connect to database ... {0}", connectionString);
            
            _instance = GetNewConnection(props);
            _instance.Open();
            if (_instance.State != ConnectionState.Open)
            {
                Logger.Error("Connection could not be established");
            }
        }
        return _instance;
    }

    private static IDbConnection GetNewConnection(IDictionary<string,string> props)
    {
        return ConnectionUtils.ConnectionFactory.GetInstance().CreateConnection(props);
    }
    
    private static string GetConnectionStringByName(string name)
    {
        // Assume failure.
        string returnValue = null;

        // Look for the name in the connectionStrings section.
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
        
        // If found, return the connection string.
        if (settings != null)
            returnValue = settings.ConnectionString;

        return returnValue;
    }

    public static IDictionary<String, String> GetDBPropertiesByName(string name)
    {
        IDictionary<String, string> props = new SortedList<String, String>();
        props.Add("ConnectionString", GetConnectionStringByName(name));
        return props;
    }
}