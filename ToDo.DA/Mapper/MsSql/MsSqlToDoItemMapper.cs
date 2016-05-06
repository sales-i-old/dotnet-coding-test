using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ToDo.Entity;

namespace ToDo.DA.Mapper.MsSql
{
    public class MsSqlToDoItemMapper : MsSqlMapper, IToDoItemMapper
    {
        public MsSqlToDoItemMapper()
        { 
            
        }

        public IList<IToDoItem> GetToDoItems(string idFilter = "")
        {
            // sql to execute
            string sql = "select * from ToDoItems";

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
                    sql += " where id = '@id'";
                    command.Parameters.Add(new SqlParameter("@id", idFilter));
                }

                try
                {
                    conn.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IToDoItem item = new ToDoItem
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("id")).ToString(),
                                Title = reader.GetString(reader.GetOrdinal("title")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Complete = reader.GetBoolean(reader.GetOrdinal("complete"))
                            };

                            int dependentFieldIndex = reader.GetOrdinal("dependentOnId");
                            //check for Db null before mapping nullable uniqueidentifier
                            if (!reader.IsDBNull(dependentFieldIndex))
                            {
                                item.DependentOnId = reader.GetGuid(dependentFieldIndex).ToString();
                            }

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
            string sql = "INSERT INTO ToDoItems (id, title, description, complete, dependentOnId) OUTPUT INSERTED.id VALUES (NEWID(), @title, @description, 0, @dependentOnId)";

            // access the database and retrieve data
            using (IDbConnection conn = GetConnection())
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = sql;

                IDbDataParameter title = new SqlParameter("@title", toDoItem.Title);
                IDbDataParameter description = new SqlParameter("@description", toDoItem.Description);
                IDbDataParameter dependentOnId;

                if (!String.IsNullOrEmpty(toDoItem.DependentOnId))
                {
                    dependentOnId = new SqlParameter("@dependentOnId", toDoItem.DependentOnId);
                }
                else
                {
                    dependentOnId = new SqlParameter("@dependentOnId", DBNull.Value);
                }

                command.Parameters.Add(title);
                command.Parameters.Add(description);
                command.Parameters.Add(dependentOnId);                

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
            //would use sprocs here instead of hardcoded sql commands in code
            //or code-first EF and query with linq for smaller apps like this.
            string sql;

            string completeBit = "0";

            if(toDoItem.Complete)
            {
                completeBit = "1";
            }

            if (String.IsNullOrEmpty(toDoItem.DependentOnId))
            {
                sql = String.Format("UPDATE [todo_test].[dbo].[ToDoItems] set description = '{0}', Title = '{1}', Complete = '{2}', DependentOnId = NULL WHERE ID = '{3}'", toDoItem.Description, toDoItem.Title, completeBit, toDoItem.Id);
            }
            else
            {
                sql = String.Format("UPDATE [todo_test].[dbo].[ToDoItems] set description = '{0}', Title = '{1}', Complete = '{2}', DependentOnId = '{3}' WHERE ID = '{4}'", toDoItem.Description, toDoItem.Title, completeBit, toDoItem.DependentOnId, toDoItem.Id);
            }

            // access the database and retrieve data
            using (IDbConnection conn = GetConnection())
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = sql;

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
