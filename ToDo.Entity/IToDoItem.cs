﻿using System;
namespace ToDo.Entity
{
    public interface IToDoItem
    {
        bool Complete { get; set; }
        string Description { get; set; }
        string Id { get; set; }
        string Title { get; set; }
        string Pid { get; set; }
    }
}
