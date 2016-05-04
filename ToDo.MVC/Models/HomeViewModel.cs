using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo.MVC.ToDoService;
using System.Web.Mvc;

namespace ToDo.MVC.Models
{
    public class HomeViewModel
    {
        public string Message { get; private set; }
        public IList<ToDoItemContract> TodoItems { get; private set;}
        public SelectListItem SelectedParent { get; set; }
        public IEnumerable<SelectListItem> AvailableParents { get; private set; }

        public HomeViewModel(string message, IList<ToDoItemContract> todoItems)
        {
            this.Message = message;
            this.TodoItems = todoItems;
            this.AvailableParents =  this.TodoItems
                .Select(x => new SelectListItem { 
                    Text = String.Format("Title:{0} Description:{1}", x.Title, x.Description), 
                    Value = x.Id}).ToList();
        }

        
    }
}