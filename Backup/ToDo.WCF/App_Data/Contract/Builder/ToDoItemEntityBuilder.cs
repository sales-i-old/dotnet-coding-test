using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo.Entity;

namespace ToDo.WCF.Contract.Builder
{
    public class ToDoItemEntityBuilder
    {
        public IToDoItem Build(ToDoItemContract toDoItemContract)
        {
            IToDoItem toDoItem = new ToDoItem();
            toDoItem.Id = toDoItemContract.Id;
            toDoItem.Title = toDoItemContract.Title;
            toDoItem.Description = toDoItemContract.Description;
            toDoItem.Complete = toDoItemContract.Complete;

            return toDoItem;
        }
    }
}