using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDo.MVC.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(string id)
        {
            ToDoService.ToDoServiceClient service = new ToDoService.ToDoServiceClient();

            List<ToDoService.ToDoItemContract> items = service.GetToDoItems("").ToList();

            return View(items.Where(x => x.Id == id).FirstOrDefault());
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ToDoService.ToDoServiceClient service = new ToDoService.ToDoServiceClient();

                ToDoService.ToDoItemContract task = new ToDoService.ToDoItemContract();
                task.Title = collection.Get("TaskName");
                task.Description = collection.Get("TaskDescription");

                service.SaveToDoItem(task);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Task/Edit/5
 
        public ActionResult Edit(string id)
        {
            return View();
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                ToDoService.ToDoServiceClient service = new ToDoService.ToDoServiceClient();

                ToDoService.ToDoItemContract task = new ToDoService.ToDoItemContract();
                task.Id = id;
                task.Title = collection.Get("Title");
                task.Description = collection.Get("Description");

                // MVC sends a checkbox value grouped with a hidden field, take the first result
                string complete = collection.Get("Complete").Split(',')[0];

                task.Complete = Convert.ToBoolean(complete);

                service.SaveToDoItem(task);
 
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Index", "Home");
            }
        }

        //
        // GET: /Task/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Task/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
