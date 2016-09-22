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
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            ToDoService.ToDoServiceClient service = new ToDoService.ToDoServiceClient();

            List<ToDoService.ToDoItemContract> tasks = new List<ToDoService.ToDoItemContract>();
            tasks = service.GetToDoItems("").ToList();

            ViewData["Tasks"] = tasks;

            var task = new TaskModel();

            return View(task);
        }

        public ActionResult SaveTask(string taskName, string taskDescription)
        {
            return View("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
