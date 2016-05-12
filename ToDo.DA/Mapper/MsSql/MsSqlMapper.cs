using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ToDo.DA.Mapper.MsSql
{
    public abstract class MsSqlMapper
    {
        private MsSqlConnection connection;

        public MsSqlMapper()
        {
            connection = new MsSqlConnection();
        }

        public IDbConnection GetConnection()
        {
            // Get connection string from the config. A valid connection string already exists in the relevant config
            return connection.GetConnection();
        }
    }
}
