using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace ToDo.WCF
{
    [ServiceContract]
    public interface IToDoService
    {
        // list all to do items
        [OperationContract]
        IEnumerable<Contract.ToDoItemContract> GetToDoItems(string idFilter);

        [OperationContract]
        string SaveToDoItem(Contract.ToDoItemContract toDoItem);

        [OperationContract]
       List<string> GetAllDependantTasks(string id);

      

    }

}
