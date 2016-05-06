using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDo.DA;
using ToDo.Core.Entity;

namespace ToDo.Service
{
    public class ToDoItemService
    {
        private ToDoConnection _toDoConnection;

        public ToDoItemService(ToDoConnection toDoConnection)
        {
            _toDoConnection = toDoConnection;
        }

        public IList<IToDoItem> GetTodoItems()
        {
            return null;
        }
    }
}
