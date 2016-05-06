using System;
using System.Collections.Generic;
using ToDo.Entity;

namespace ToDo.Core.Service
{
    public interface IToDoItemService
    {
        IList<IToDoItem> GetTodoItems(string idFilter = "");

        string Save(IToDoItem toDoItem);
    }
}
