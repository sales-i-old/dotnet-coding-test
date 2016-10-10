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

namespace ToDo.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
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

        public string SaveToDoItem(Contract.ToDoItemContract toDoItemContract)
        {
            try
            {
                ToDoItemEntityBuilder builder = new ToDoItemEntityBuilder();
                
                return ToDoItemService.Save(builder.Build(toDoItemContract));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
