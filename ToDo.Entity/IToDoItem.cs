using System;
namespace ToDo.Entity
{
    public interface IToDoItem
    {
        bool Complete { get; set; }
        string Description { get; set; }
        string Id { get; set; }
        string ParentId { get; set; }
        string Title { get; set; }
    }
}
