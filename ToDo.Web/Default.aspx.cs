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
        private List<ToDoService.ToDoItemContract> _toDoItems;
        private ToDoService.ToDoServiceClient _client;

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
            _client = new ToDoService.ToDoServiceClient();

            try
            {
                _toDoItems = _client.GetToDoItems("").ToList();
                dlTasks.DataSource = _toDoItems;
                dlTasks.DataBind();

                _client.Close();
            }
            catch (Exception ex)
            {
                // TODO: Log error
                _client.Abort();
            }
        }

        protected void btn_AddTask_Click(object sender, EventArgs e)
        {
            // ANDREI: added a session mechanism to prevent the insert of the same task and/or insert on the page reload
            if (!IsPostBack) return;
            if (Session["commentAdded"] != null) return;
            var toDoItem = new ToDoService.ToDoItemContract
            {
                Title = txtTask.Text,
                Description = txtDescription.Text
            };

            Save(toDoItem);

            // update the UI
            LoadTasks();
            Session.Add("commentAdded", true);
        }

        private string Save(ToDoService.ToDoItemContract toDoItemContract)
        {
            // get the todo list items
            _client = new ToDoService.ToDoServiceClient();
            
            try
            {
                // save the new task
                return _client.SaveToDoItem(toDoItemContract);
            }
            catch (Exception ex)
            {
                _client.Abort();
                // TODO: Client side save error message
                return "";
            }
        }

        protected void dlTasks_EditCommand(Object sender, DataListCommandEventArgs e)
        {
            dlTasks.EditItemIndex = e.Item.ItemIndex;
            LoadTasks();
        }

        protected void dlTasks_UpdateCommand(Object sender, DataListCommandEventArgs e)
        {
            var toDoItem = new ToDoService.ToDoItemContract
            {
                Id = e.CommandArgument.ToString(),
                Title = (e.Item.FindControl("txtUpdateTitle") as TextBox).Text,
                Description = (e.Item.FindControl("txtUpdateDescription") as TextBox).Text,
                Complete = (e.Item.FindControl("chkComplete") as CheckBox).Checked,
                // ANDREI: set a parent task id property
                ParentTaskId = (e.Item.FindControl("cboParentTask") as DropDownList).SelectedValue
            };

            Save(toDoItem);

            // take the list out of edit mode
            dlTasks.EditItemIndex = -1;

            // update the UI
            LoadTasks();
        }


        protected void dlTasks_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            // ANDREI : populated a parent task dropdown list in edit mode
            if (e.Item.ItemType != ListItemType.EditItem) return;

            var ddl = e.Item.FindControl("cboParentTask") as DropDownList;
            if (ddl == null) return;

            try
            {
                ddl.DataSource = _toDoItems;
                ddl.DataBind();

                _client.Close();
            }
            catch (Exception ex)
            {
                // TODO: Log error
                _client.Abort();
            }
        }

        protected void txtTask_TextChanged(object sender, EventArgs e)
        {
            Session.Remove("commentAdded");
        }
    }
}