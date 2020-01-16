using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GERALD.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection OpenConnection();
    }

    public static class ConnectionExtensions
    {

        public static IDbCommand CreateCommand(this IDbConnection connection, string commandText)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }

    }
}
