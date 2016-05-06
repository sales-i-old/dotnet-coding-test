using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToDo.MVC5.Models
{
    public class TaskDependencyViewModel
    {
        public Guid? DependentOnTaskId { get; set; }
        public String DependentOnTaskTitle { get; set; }
    }
}