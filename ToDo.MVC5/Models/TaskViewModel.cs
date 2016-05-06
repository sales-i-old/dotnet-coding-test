using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToDo.MVC5.Models
{
    public class TaskViewModel
    {
        //The task
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public bool Complete { get; set; }

        //Dependencies
        public Guid? DependentOnTaskId { get; set; }

        [Display(Name = "Dependent On")]
        public String DependentOnTaskTitle { get; set; }
    }
}