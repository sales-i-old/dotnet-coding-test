using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.MVC.Models;

namespace ToDo.MVC.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ToDoService.ToDoServiceClient service = new ToDoService.ToDoServiceClient();

            List<ToDoService.ToDoItemContract> tasks = new List<ToDoService.ToDoItemContract>();
            tasks = service.GetToDoItems("").ToList();

            //ViewData["Tasks"] = tasks;

            var vm = new HomeViewModel ("Welcome to ACME todo list", tasks);


            return View(vm);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
