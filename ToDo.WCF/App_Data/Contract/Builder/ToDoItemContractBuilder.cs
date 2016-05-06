using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo.Entity;

namespace ToDo.WCF.Contract.Builder
{
    public class ToDoItemContractBuilder
    {
        public ToDoItemContractBuilder()
        { }

        public ToDoItemContract Build(IToDoItem toDoItemEntity)
        {
            ToDoItemContract toDoItemContract = new ToDoItemContract
            {
                Id = toDoItemEntity.Id,
                Title = toDoItemEntity.Title,
                Description = toDoItemEntity.Description,
                Complete = toDoItemEntity.Complete,
                DependentOnId = toDoItemEntity.DependentOnId
            };

            return toDoItemContract;
        }
    }
}