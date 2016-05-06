using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace ToDo.DA
{
    public class MsSqlConnection
    {
        public IDataReader ExecuteQueryReader(IDbCommand command)
        {
            using (SqlConnection conn = GetConnection() as SqlConnection)
            {
                command.Connection = conn;
                return command.ExecuteReader();
            }
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["ToDoDatabase"].ToString());
        }

        public IDbCommand CreateCommand(string commandText)
        {
            throw new NotImplementedException();
        }
    }
}
