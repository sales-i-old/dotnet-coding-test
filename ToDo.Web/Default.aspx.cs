using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToDo.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTasks();                
            }
        }

        private void LoadTasks()
        {
            // get the todo list items
            ToDoService.ToDoServiceClient client = new ToDoService.ToDoServiceClient();

            try
            {
                List<ToDoService.ToDoItemContract> toDoItems = client.GetToDoItems("").ToList();
                dlTasks.DataSource = toDoItems;
                dlTasks.DataBind();
                ddlRelatedTask.DataSource = toDoItems;
                ddlRelatedTask.DataBind();

                client.Close();
            }
            catch (Exception ex)
            {
                // TODO: Log error
                Server.Transfer("Error.aspx", true);
                client.Abort();
            }
        }

        protected void btn_AddTask_Click(object sender, EventArgs e)
        {
            ToDoService.ToDoItemContract toDoItem = new ToDoService.ToDoItemContract();
            toDoItem.Title = txtTask.Text;
            toDoItem.Description = txtDescription.Text;
            toDoItem.RelatedId = ddlRelatedTask.SelectedValue;
            
            ShowResult(Save(toDoItem));

            // update the UI
            LoadTasks();
        }

        private string Save(ToDoService.ToDoItemContract toDoItemContract)
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

        protected void dlTasks_EditCommand(Object sender, DataListCommandEventArgs e)
        {
            dlTasks.EditItemIndex = e.Item.ItemIndex;
            LoadTasks();
        }

        protected void dlTasks_UpdateCommand(Object sender, DataListCommandEventArgs e)
        {
            ToDoService.ToDoItemContract toDoItem = new ToDoService.ToDoItemContract();
            toDoItem.Id = e.CommandArgument.ToString();
            toDoItem.Title = (e.Item.FindControl("txtUpdateTitle") as TextBox).Text;
            toDoItem.Description = (e.Item.FindControl("txtUpdateDescription") as TextBox).Text;
            toDoItem.Complete = (e.Item.FindControl("chkComplete") as CheckBox).Checked;
            toDoItem.RelatedId = (e.Item.FindControl("ddlRelatedTask") as DropDownList).SelectedValue;
         
            ShowResult(Save(toDoItem));

            // take the list out of edit mode
            dlTasks.EditItemIndex = -1;

            // update the UI
            LoadTasks();
        }

        private void ShowResult(string result)
        {
            Guid newId;

            //show success if guid returned
            if (Guid.TryParse(result, out newId))
            {
                ltMessage.Text = "Task saved.<br />";
            }
            else
            {
                ltMessage.Text = string.Format("Error: {0} <br />", result);
            }
        }


    }
}