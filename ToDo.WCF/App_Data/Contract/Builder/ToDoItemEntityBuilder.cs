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
            IToDoItem toDoItem = new ToDoItem
            {
                Id = toDoItemContract.Id,
                Title = toDoItemContract.Title,
                Description = toDoItemContract.Description,
                Complete = toDoItemContract.Complete,
                DependentOnId = toDoItemContract.DependentOnId
            };

            return toDoItem;
        }
    }
}