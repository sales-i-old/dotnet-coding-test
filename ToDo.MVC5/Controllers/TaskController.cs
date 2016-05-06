using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.MVC5.Models;
using ToDo.MVC5.TodoService;
using ToDo.WCF.Contract;

namespace ToDo.MVC.Controllers
{
    public class TaskController : Controller
    {
        private ToDoServiceClient _toDoServiceClient;

        public TaskController()
        {
            _toDoServiceClient = new ToDoServiceClient();
        }

        /// <summary>
        /// Index - lists all tasks
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<ToDoItemContract> tasks = new List<ToDoItemContract>();
            tasks = _toDoServiceClient.GetToDoItems("").ToList();

            var model = new List<TaskViewModel>();

            //Map service response to view model. Could use automapper for this. Could create mapping helper to separate out this logic.
            //Todo: TJH Mapping helper
            foreach (var task in tasks)
            {
                var taskVM = new TaskViewModel
                {
                    Id = Guid.Parse(task.Id),
                    Complete = task.Complete,
                    Description = task.Description,
                    Title = task.Title
                };

                //Add dependent task data if applicable
                if (task.DependentOnId != null)
                {
                    ToDoItemContract dependentOntask = _toDoServiceClient.GetToDoItems("").Where(x => x.Id == task.DependentOnId).FirstOrDefault();

                    if (dependentOntask != null)
                    {
                        taskVM.DependentOnTaskId = Guid.Parse(dependentOntask.Id);
                        taskVM.DependentOnTaskTitle = dependentOntask.Title;
                    }
                }

                model.Add(taskVM);
            }

            return View(model);
        }

        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new CreateOrEditTaskViewModel();

            model.Dependencies = BuildDependencyList(null, null);

            return View(model);
        } 

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(CreateOrEditTaskViewModel model)
        {
            try
            {
                ToDoItemContract task = new ToDoItemContract
                {
                    Description = model.Description,
                    Complete = model.Complete,
                    Title = model.Title,
                    DependentOnId = model.DependencyId.ToString()
                };

                _toDoServiceClient.SaveToDoItem(task);

                return RedirectToAction("Index", "Task");
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
            //Get the task details
            ToDoServiceClient service = new ToDoServiceClient();

            ToDoItemContract task = service.GetToDoItems(id).Where(x => x.Id == id).FirstOrDefault();

            Guid? dependencyId = null;

            if (!string.IsNullOrEmpty(task.DependentOnId))
            {
                dependencyId = Guid.Parse(task.DependentOnId);
            }

            var model = new CreateOrEditTaskViewModel
            {
                Title = task.Title,
                Complete = task.Complete,
                Description = task.Description,
                DependencyId = dependencyId
            };

            model.Dependencies = BuildDependencyList(id, task.DependentOnId);

            return View(model);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateOrEditTaskViewModel model)
        {
            try
            {
                ToDoItemContract task = new ToDoItemContract();
                task.Id = model.Id;
                task.Title = model.Title;
                task.Description = model.Description;

                task.Complete = model.Complete;

                if (model.DependencyId != null)
                {
                    task.DependentOnId = model.DependencyId.ToString();
                }

                _toDoServiceClient.SaveToDoItem(task);
 
                return RedirectToAction("Index", "Task");
            }
            catch
            {
                return View("Index", "Home");
            }
        }


        /// <summary>
        /// Creates the Select list for the dependency DDL
        /// </summary>
        /// <returns></returns>
        private SelectList BuildDependencyList(string taskId = null, string dependentOnId = null)
        {
            //get a list of all tasks that we could link this task to.
            List<ToDoItemContract> tasks = new List<ToDoItemContract>();

            tasks = _toDoServiceClient.GetToDoItems("").ToList();

            //We don't want to be able to make a task dependent on it's self, so we remove it from the list.
            if (!String.IsNullOrEmpty(taskId))
            {
                var matchedTask = tasks.Where(x => x.Id == taskId).First();
                tasks.Remove(matchedTask);
            }

            var model = new CreateOrEditTaskViewModel();

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var task in tasks)
            {
                var dependencyItem = new SelectListItem
                {
                    Value = task.Id,
                    Text = task.Title,
                    Selected = false
                };

                items.Add(dependencyItem);
            }

            //Add a default option for No dependency
            var item = new SelectListItem
            {
                Value = null,
                Text = "None",
                Selected = false
            };

            items.Add(item);

            SelectListItem selectedTask;

            if (!String.IsNullOrEmpty(dependentOnId))
            {
                //Set the dependent task as the default item in the list
                selectedTask = items.Where(x => x.Value == dependentOnId).First();
            } else
            {
                //Make no dependency the default
                selectedTask = items.Where(x => x.Value == null).First();
            }

            var list = new SelectList(items, "Value", "Text", selectedTask);

            return list;
        }
    }
}
