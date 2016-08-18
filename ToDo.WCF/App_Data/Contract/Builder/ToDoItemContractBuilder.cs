﻿using System;
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
            ToDoItemContract toDoItemContract = new ToDoItemContract();
            toDoItemContract.Id = toDoItemEntity.Id;
            toDoItemContract.Title = toDoItemEntity.Title;
            toDoItemContract.Description = toDoItemEntity.Description;
            toDoItemContract.Complete = toDoItemEntity.Complete;
            // ANDREI: mapped the parent task properties
            toDoItemContract.ParentTaskId = toDoItemEntity.ParentTaskId;
            toDoItemContract.ParentTaskTitle = toDoItemEntity.ParentTaskTitle;

            return toDoItemContract;
        }
    }
}