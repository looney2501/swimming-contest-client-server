using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ConnectionUtils;
using log4net;

namespace Server.Utils;

public static class DbUtils
{
    private static IDbConnection _instance;
    private static readonly ILog Logger = LogManager.GetLogger("DBUtils");

    public static IDbConnection GetConnection(IDictionary<string, string> props)
    {
        if (_instance == null || _instance.State == ConnectionState.Closed)
        {
            var connectionString = props["ConnectionString"];

            Logger.InfoFormat("trying to connect to database ... {0}", connectionString);

            _instance = GetNewConnection(props);
            _instance.Open();
            if (_instance.State != ConnectionState.Open) Logger.Error("Connection could not be established");
        }

        return _instance;
    }

    private static IDbConnection GetNewConnection(IDictionary<string, string> props)
    {
        return ConnectionFactory.GetInstance().CreateConnection(props);
    }

    public static string GetConnectionStringByName(string name)
    {
        // Assume failure.
        string returnValue = null;

        // Look for the name in the connectionStrings section.
        var settings = ConfigurationManager.ConnectionStrings[name];

        // If found, return the connection string.
        if (settings != null)
            returnValue = settings.ConnectionString;

        return returnValue;
    }

    public static IDictionary<string, string> GetDBPropertiesByName(string name)
    {
        IDictionary<string, string> props = new SortedList<string, string>();
        props.Add("ConnectionString", GetConnectionStringByName(name));
        return props;
    }
}