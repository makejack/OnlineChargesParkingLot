using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Runtime.Remoting.Messaging;
using System.Configuration;

namespace DAL
{
    public partial class DbConnectionFactory
    {
        public  static IDbConnection Create()
        {
            IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SqliteConnStr"].ConnectionString);
            return connection;
        }
    }
}
