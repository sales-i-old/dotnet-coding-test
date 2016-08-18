using System;
namespace ToDo.Entity
{
    public interface IToDoItem
    {
        string Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        bool Complete { get; set; }
        // ANDREI: added parent task id property
        string ParentTaskId { get; set; }
        string ParentTaskTitle { get; set; }
    }
}
