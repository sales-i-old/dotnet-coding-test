using System;
using System.Collections.Generic;
using ToDo.Entity;

namespace ToDo.DA.Mapper
{
    public interface IToDoItemMapper
    {
        IList<IToDoItem> GetToDoItems(string idFilter = "");

        string Insert(IToDoItem item);

        bool Update(IToDoItem item);
    }
}
