using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ToDo.Entity;
using System.Configuration;

namespace ToDo.DA.Mapper.MsSql
{
    public class MsSqlToDoItemMapper : MsSqlMapper, IToDoItemMapper
    {
        public MsSqlToDoItemMapper()
        { 
            
        }

        public DataTable GetAllDependantTasks()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                using (SqlCommand com = new SqlCommand())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ToDoDatabase"].ConnectionString;
                    com.Connection = conn;
                    com.CommandText = "EXEC usp_GetAllDependantTaskInfoByID";
                    conn.Open();
                    SqlDataReader returnvalue = com.ExecuteReader();

                    if (returnvalue.HasRows)
                    {
                        dt.Load(returnvalue);
                        return dt;
                    }
                    else
                    {
                        throw new RowNotInTableException();
                    }

                }
            }
            catch (Exception ex)
            {
                //TODO: Write error to log
                return new DataTable();
            }
        }



        public IList<IToDoItem> GetToDoItems(string idFilter = "")
        {
            // sql to execute
            //string sql = "select * from ToDoItems";
            string sql = "exec usp_GetAllDependantTaskInfoByID";

            // instantiate list to populate
            List<IToDoItem> items = new List<IToDoItem>();

            // access the database and retrieve data
            using (IDbConnection conn = GetConnection())
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = sql;

                // do we have an id filter?
                if (!string.IsNullOrEmpty(idFilter))
                {
                   command.Parameters.Add(new SqlParameter("@id",null));
                }

                try
                {
                    conn.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            IToDoItem item = new ToDoItem();
                            item.Id = reader.GetGuid(reader.GetOrdinal("id")).ToString();
                            item.Title = reader.GetString(reader.GetOrdinal("title"));
                            item.Description = reader.GetString(reader.GetOrdinal("description"));
                            item.Complete = reader.GetBoolean(reader.GetOrdinal("complete"));
                            if (!reader.IsDBNull(5))
                            {
                               // item.DependantTaskID = reader.GetGuid(reader.GetOrdinal("DependsOnTask")).ToString();
                                item.DependantTaskTitle = reader.GetString(reader.GetOrdinal("DependantTaskTitle"));
                            }
                            else
                            {
                                item.DependantTaskID = String.Empty;
                                item.DependantTaskTitle = String.Empty;
                            }
                           

                            // item.DependantTaskID = (item.DependantTaskID!=null) ? reader.GetString(reader.GetOrdinal("DependsOnTask")) : String.Empty;
                            // item.DependantTaskTitle =(item.DependantTaskTitle!=null) ? reader.GetString(reader.GetOrdinal("DependantTaskTitle")) : String.Empty;


                            items.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }

            return items;
        }

        public string Insert(IToDoItem toDoItem)
        {
            string sql = "INSERT INTO ToDoItems (id, title, description, complete, DependantTaskTitle) OUTPUT INSERTED.id VALUES (NEWID(), @title, @description, 0, @DependantTaskTitle)";

            // access the database and retrieve data
            using (IDbConnection conn = GetConnection())
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = sql;

                IDbDataParameter title = new SqlParameter("@title", toDoItem.Title);
                IDbDataParameter description = new SqlParameter("@description", toDoItem.Description);
                IDbDataParameter DependantTaskTitle = new SqlParameter("@DependantTaskTitle", toDoItem.DependantTaskTitle);

                

                command.Parameters.Add(title);
                command.Parameters.Add(description);
                command.Parameters.Add(DependantTaskTitle);

                try
                {
                    conn.Open();

                    object result = command.ExecuteScalar();

                    if (result != null)
                        return result.ToString();
                    else
                        throw new NoNullAllowedException("No ID returned when inserting a to do item");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool Update(IToDoItem toDoItem)
        {
            string sql = @" UPDATE ToDoItems 
                            SET title = @title
                            , description = @description
                            , complete = @complete
                            WHERE id = @id";

            // access the database and retrieve data
            using (IDbConnection conn = GetConnection())
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = sql;

                IDbDataParameter title = new SqlParameter("@title", toDoItem.Title);
                IDbDataParameter description = new SqlParameter("@description", toDoItem.Description);
                IDbDataParameter complete = new SqlParameter("@complete", toDoItem.Complete);
                IDbDataParameter id = new SqlParameter("@id", toDoItem.Id);

                command.Parameters.Add(title);
                command.Parameters.Add(description);
                command.Parameters.Add(complete);
                command.Parameters.Add(id);

                try
                {
                    conn.Open();

                    int result = command.ExecuteNonQuery();
                    return (result > 0);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
