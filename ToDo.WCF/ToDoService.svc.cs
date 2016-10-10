using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ToDo.Core.Service.Factory;
using ToDo.Core.Service;
using ToDo.Entity;
using ToDo.WCF.Contract.Builder;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;



namespace ToDo.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [DataContract]
    public class ToDoService : IToDoService
    {
        private IToDoItemService ToDoItemService;

        public ToDoService()
        {
            ToDoItemServiceFactory toDoServiceFactory = new ToDoItemServiceFactory();

            ToDoItemService = toDoServiceFactory.CreateInstance();
        }


       


        public IEnumerable<Contract.ToDoItemContract> GetToDoItems(string idFilter)
        {
            ToDoItemContractBuilder builder = new ToDoItemContractBuilder();

            // array to return
            IList<Contract.ToDoItemContract> results = new List<Contract.ToDoItemContract>();

            IList<IToDoItem> items = ToDoItemService.GetTodoItems(idFilter);

            foreach (IToDoItem item in items)
            {
                Contract.ToDoItemContract toDoItemContract = builder.Build(item);

                results.Add(toDoItemContract);
                
            }

            return results;
        }

        [Obsolete("Datatable not serializable; use GetToDoItems!",true)]
        public List<string> GetAllDependantTasks(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                using (SqlCommand com = new SqlCommand())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ToDoDatabase"].ConnectionString;
                    com.Connection = conn;
                   
                    com.CommandText = "usp_GetAllDependantTaskInfoByID";
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandTimeout = 0;
                   
                    if (string.IsNullOrEmpty(id))
                    {
                        com.Parameters.Add(new SqlParameter("@id", "00000000-0000-0000-0000-000000000000"));
                    }
                  
                    conn.Open();
                    SqlDataReader returnvalue = com.ExecuteReader();

                    if (returnvalue.HasRows)
                    {
                        List<string> items = new List<string>();
                        dt.Load(returnvalue);

                       
                        foreach (DataRow row in dt.Rows)
                        {
                            foreach(var item in row.ItemArray)
                            {
                               
                                items.Add(Convert.ToString(item));
                            }
                           
                        }
                        
                        return items;
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
                return new List<string>();
            }
        }


        public string SaveToDoItem(Contract.ToDoItemContract toDoItemContract)
        {
            try
            {
                ToDoItemEntityBuilder builder = new ToDoItemEntityBuilder();

                return ToDoItemService.Save(builder.Build(toDoItemContract));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
