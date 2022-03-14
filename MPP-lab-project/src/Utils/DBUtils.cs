using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using log4net.Repository.Hierarchy;

namespace MPP_lab_project.Utils;

public static class DBUtils
{
    private static IDbConnection _instance = null;
    private static ILog _logger = LogManager.GetLogger("DBUtils");
    
    public static IDbConnection GetConnection(IDictionary<string,string> props)
    {
        if (_instance == null || _instance.State == System.Data.ConnectionState.Closed)
        {
            String url = props["url"];
            String user = props["user"];
            String password = props["password"];
            
            _logger.InfoFormat("trying to connect to database ... {0}", url);
            _logger.InfoFormat("user: {0}", user);
            _logger.InfoFormat("password: {0}", password);
            
            _instance = GetNewConnection(props);
            _instance.Open();
            if (_instance.State != ConnectionState.Open)
            {
                _logger.Error("Connection could not be established");
            }
        }
        return _instance;
    }

    private static IDbConnection GetNewConnection(IDictionary<string,string> props)
    {
        return ConnectionUtils.ConnectionFactory.GetInstance().CreateConnection(props);
    }
}