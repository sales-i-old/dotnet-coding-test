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
        public IDbConnection GetConnection()
        {

            var dbConfigSection = ConfigurationManager.AppSettings["DatabaseConnection"];

            if (dbConfigSection == null)
                throw new ApplicationException("No DatabaseConnection app setting defined");

            var dbConnection = ConfigurationManager.ConnectionStrings[dbConfigSection];
            
            if (dbConnection == null)
                throw new ApplicationException("No database configuration defined in web.config");
           



            // TODO: Get connection string from the config. A valid connection string already exists in the relevant config
            return new SqlConnection(dbConnection.ConnectionString);
        }
    }
}
