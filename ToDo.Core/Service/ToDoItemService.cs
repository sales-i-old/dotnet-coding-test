using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ToDo.DA.Mapper;
using ToDo.Entity;

namespace ToDo.Core.Service
{
    public class ToDoItemService : IToDoItemService
    {
        private IToDoItemMapper _toDoMapper;

        public ToDoItemService(IToDoItemMapper toDoMapper)
        {
            _toDoMapper = toDoMapper;
        }

        public IList<IToDoItem> GetTodoItems(string idFilter = "")
        {
            // TODO: Need to sanitise the idFilter string for injection attempts
            return _toDoMapper.GetToDoItems(idFilter);
        }

        public string Save(IToDoItem toDoItem)
        {
            if (!string.IsNullOrEmpty(toDoItem.Id))
            {
                // TODO implement update
                //TODO: TJH Make this a sproc
                //string sqlCommand = String.Format("UPDATE [todo_test].[dbo].[ToDoItems] set description = '{0}', Title = '{1}' WHERE ID = '{2}'", toDoItem.Description, toDoItem.Title, toDoItem.Id);

                //prefer not to do a one-liner to make it easier to read and debug.
                var result = Update(toDoItem);
                return result;
            }
            else
            {
                return Insert(toDoItem);
            }
        }

        private string Update(IToDoItem toDoItem)
        {
            try
            {
                if (_toDoMapper.Update(toDoItem))
                    return toDoItem.Id;
                else
                    throw new Exception("No todo items updated");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Insert(IToDoItem toDoItem)
        {
            try
            {
                return _toDoMapper.Insert(toDoItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
