using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GERALD.Data
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection OpenConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
