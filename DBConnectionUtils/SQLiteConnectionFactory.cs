using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace ConnectionUtils
{
    public class SQLiteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection CreateConnection(IDictionary<string, string> props)
        {
            var connectionString = props["ConnectionString"];
            return new SQLiteConnection(connectionString);
        }
    }
}