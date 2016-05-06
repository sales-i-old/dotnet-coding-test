using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToDo.MVC5.Models
{
    public class CreateOrEditTaskViewModel
    {
        public string Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public bool Complete { get; set; }

        //For creating task dependency
        public Guid? DependencyId { get; set; }
        public System.Web.Mvc.SelectList Dependencies { get; set; }
    }
}