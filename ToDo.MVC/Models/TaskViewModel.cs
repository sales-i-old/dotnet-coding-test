using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo.MVC.ToDoService;
using System.Web.Mvc;

namespace ToDo.MVC.Models
{
    public class TaskViewModel
    {
        public ToDoItemContract TodoItem { get; private set;}
        public SelectListItem SelectedParent { get; set; }
        public IEnumerable<SelectListItem> AvailableParents { get; private set; }

        public TaskViewModel(ToDoItemContract todoItem, IList<ToDoItemContract> todoItems)
        {
            this.TodoItem = todoItem;
            this.AvailableParents =  todoItems.Where(x => x.Id != todoItem.Id)
                .Select(x => new SelectListItem { 
                    Text = String.Format("Title:{0} Description:{1}", x.Title, x.Description), 
                    Value = x.Id}).ToList();

            this.SelectedParent = this.AvailableParents.FirstOrDefault(x => x.Value == this.TodoItem.ParentId);
        }
    }
}