using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDo.MVC.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            ToDoService.ToDoServiceClient service = new ToDoService.ToDoServiceClient();

            List<ToDoService.ToDoItemContract> tasks = new List<ToDoService.ToDoItemContract>();
            tasks = service.GetToDoItems("").ToList();

            ViewData["Tasks"] = tasks;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
