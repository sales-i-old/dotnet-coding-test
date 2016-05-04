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
                        var ordId = reader.GetOrdinal("id");
                        var ordParentId = reader.GetOrdinal("idParent");
                        var ordTitle = reader.GetOrdinal("title");
                        var ordDesc = reader.GetOrdinal("description");
                        var ordComplete = reader.GetOrdinal("complete");

                        while (reader.Read())
                        {
                            IToDoItem item = new ToDoItem();
                            item.Id = reader.GetGuid(ordId).ToString();
                            item.ParentId =  reader.IsDBNull(ordParentId) ? null : reader.GetGuid(ordParentId).ToString();
                            item.Title = reader.GetString(ordTitle);
                            item.Description = reader.GetString(ordDesc);
                            item.Complete = reader.GetBoolean(ordComplete);

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
            string sql = "INSERT INTO ToDoItems (id, idParent, title, description, complete) OUTPUT INSERTED.id VALUES (NEWID(), @idParent, @title, @description, 0)";

            // access the database and retrieve data
            using (IDbConnection conn = GetConnection())
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = sql;

                IDbDataParameter title = new SqlParameter("@title", toDoItem.Title);
                IDbDataParameter description = new SqlParameter("@description", toDoItem.Description);

                IDbDataParameter idParent = new SqlParameter("@idParent", String.IsNullOrEmpty(toDoItem.ParentId) ? DBNull.Value : (object)new Guid(toDoItem.ParentId));

                command.Parameters.Add(title);
                command.Parameters.Add(description);
                command.Parameters.Add(idParent);

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
                            SET idParent = @idParent
                            , title = @title
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
                IDbDataParameter idParent = new SqlParameter("@idParent", String.IsNullOrEmpty(toDoItem.ParentId) ? DBNull.Value : (object)new Guid(toDoItem.ParentId));

                command.Parameters.Add(title);
                command.Parameters.Add(description);
                command.Parameters.Add(complete);
                command.Parameters.Add(id);
                command.Parameters.Add(idParent);

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
