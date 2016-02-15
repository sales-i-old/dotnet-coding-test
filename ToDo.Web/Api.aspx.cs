using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToDo.Web
{
    public partial  class Api : System.Web.UI.Page
    {
        [WebMethod]
        public static string AddTask(string title, string description, string relatedId)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
                return "<b>Error:</b> Both the title and description are required";

            ToDoService.ToDoItemContract toDoItem = new ToDoService.ToDoItemContract();

            toDoItem.Title = title;
            toDoItem.Description = description;
            toDoItem.RelatedId = relatedId;

            return ShowResult(Save(toDoItem));
        }

        private static string Save(ToDoService.ToDoItemContract toDoItemContract)
        {
            // get the todo list items
            ToDoService.ToDoServiceClient client = new ToDoService.ToDoServiceClient();

            try
            {
                // save the new task
                return client.SaveToDoItem(toDoItemContract);
            }
            catch (Exception ex)
            {
                client.Abort();
                return ex.Message;
            }
        }

        private static string ShowResult(string result)
        {
            Guid newId;

            //show success if guid returned
            if (Guid.TryParse(result, out newId))
            {
                return "<b>Success:</b> Task saved.";
            }
            else
            {
                return string.Format("<b>Error:</b> {0} ", result);
            }
        }
    }
}