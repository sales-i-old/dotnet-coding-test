using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.MVC.Models;

namespace ToDo.MVC.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            // Set the model to list of task id's so they can be added to a drop down box

            return View();
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(string id)
        {
            ToDoService.ToDoServiceClient service = new ToDoService.ToDoServiceClient();

            var items = service.GetToDoItems("").ToList();
            var currentItem = items.FirstOrDefault(x => x.Id == id);

            var vm = new TaskViewModel(currentItem, items);

            return View(vm);
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
                task.ParentId = collection.Get("SelectedParent");

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
                task.ParentId = collection.Get("SelectedParent");
                task.Title = collection.Get("TodoItem.Title");
                task.Description = collection.Get("TodoItem.Description");

                // MVC sends a checkbox value grouped with a hidden field, take the first result
                string complete = collection.Get("TodoItem.Complete").Split(',')[0];

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
