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
            string sql = "select tdo1.*, isnull(tdo2.Title,'') RelatedTaskTitle from ToDoItems tdo1 left join ToDoItems tdo2 on tdo1.relatedid = tdo2.id ";

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
                            IToDoItem item = new ToDoItem();
                            item.Id = reader.GetGuid(reader.GetOrdinal("id")).ToString();
                            item.Title = reader.GetString(reader.GetOrdinal("title"));
                            item.Description = reader.GetString(reader.GetOrdinal("description"));
                            item.Complete = reader.GetBoolean(reader.GetOrdinal("complete"));
                            if (!reader.IsDBNull(reader.GetOrdinal("relatedid")))
                                item.RelatedId = reader.GetGuid(reader.GetOrdinal("relatedid")).ToString();
                            item.RelatedTaskTitle = reader.GetString(reader.GetOrdinal("RelatedTaskTitle")).ToString();

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
            string sql = "INSERT INTO ToDoItems (id, title, description, complete, relatedid) OUTPUT INSERTED.id VALUES (NEWID(), @title, @description, 0, @relatedid)";

            // access the database and retrieve data
            using (IDbConnection conn = GetConnection())
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = sql;

                IDbDataParameter title = new SqlParameter("@title", toDoItem.Title);
                IDbDataParameter description = new SqlParameter("@description", toDoItem.Description);
                IDbDataParameter relatedid;

                if (string.IsNullOrEmpty(toDoItem.RelatedId))
                    relatedid = new SqlParameter("@relatedid", DBNull.Value);
                else
                    relatedid = new SqlParameter("@relatedid", toDoItem.RelatedId); 

                command.Parameters.Add(title);
                command.Parameters.Add(description);
                command.Parameters.Add(relatedid);

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
                            , relatedid = @relatedId
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
                IDbDataParameter relatedid;

                if (string.IsNullOrEmpty(toDoItem.RelatedId))
                    relatedid = new SqlParameter("@relatedid", DBNull.Value); 
                else
                    relatedid = new SqlParameter("@relatedid", toDoItem.RelatedId); 

                command.Parameters.Add(title);
                command.Parameters.Add(description);
                command.Parameters.Add(complete);
                command.Parameters.Add(id);
                command.Parameters.Add(relatedid);

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
