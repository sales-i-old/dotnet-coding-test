using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        [System.Web.Services.WebMethod]
        public static string GetAnyDependentTasks(string id)
        {

            string result = "";

            // get the todo list items
            ToDoService.ToDoServiceClient client = new ToDoService.ToDoServiceClient();

            try
            {
                List<ToDoService.ToDoItemContract> toDoItems = client.GetToDoItems("").ToList();

                // get specific record
                ToDoService.ToDoItemContract singleRecord = toDoItems.Where(tdi => tdi.Id == id).FirstOrDefault();

                // get records the have the same parentid as the id passed in and order by orderid asc
                // only those whose orderid < than singleRecord.OrderId
                foreach (var item in toDoItems.Where(tdi => tdi.Complete == false && tdi.Id == singleRecord.ParentId && tdi.OrderId < singleRecord.OrderId).OrderBy(tdi => tdi.OrderId).ToList())
                {

                    result = string.Format("{0}<li>{1}</li>", result, item.Title);
                }

                // if result is not empty, add enclosing ul tag
                if (!string.IsNullOrWhiteSpace(result))
                {
                    result = string.Format("<ul>{0}</ul>", result);
                }

                client.Close();
            }
            catch (Exception ex)
            {
                // TODO: Log error
                client.Abort();
            }

            return result;
        }

        [System.Web.Services.WebMethod]
        public static ToDoService.ToDoItemContract[] GetDependentTaskOptions(string id)
        {
            List<ToDoService.ToDoItemContract> matches = new List<ToDoService.ToDoItemContract>();

            // get the todo list items
            ToDoService.ToDoServiceClient client = new ToDoService.ToDoServiceClient();

            try
            {
                List<ToDoService.ToDoItemContract> toDoItems = client.GetToDoItems("").ToList();
                client.Close();
                // get specific record
                ToDoService.ToDoItemContract singleRecord = toDoItems.Where(tdi => tdi.Id == id).FirstOrDefault();
                // get all dependents with lower order ID not including current task
                matches = toDoItems.Where(tdi => tdi.Id != id && tdi.OrderId < singleRecord.OrderId).ToList();
                                
            }
            catch (Exception ex)
            {
                // TODO: Log error
                client.Abort();
            }

            return matches.ToArray();
        }

        [System.Web.Services.WebMethod]
        public static void UpdateDependentTasks(string id, string parentid)
        {
            ToDoService.ToDoItemContract toDoItem = new ToDoService.ToDoItemContract();
            toDoItem.Id = id;
            toDoItem.ParentId = parentid;

            UpdateDependentTasks(toDoItem);
            
        }

        private static void UpdateDependentTasks(ToDoService.ToDoItemContract toDoItemContract)
        {
            ToDoService.ToDoServiceClient client = new ToDoService.ToDoServiceClient();

            try
            {
                // update the task
                client.UpdateDependentTasks(toDoItemContract);
            }
            catch (Exception ex)
            {
                client.Abort();
                // TODO: Client side save error message
                return;
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

                ddlDependentTasks.DataSource = toDoItems;
                ddlDependentTasks.DataTextField = "Title";
                ddlDependentTasks.DataValueField = "Id";
                ddlDependentTasks.DataBind();
                ddlDependentTasks.Items.Insert(0, new ListItem("No Dependency",""));

                client.Close();
            }
            catch (Exception ex)
            {
                // TODO: Log error
                client.Abort();
            }
        }

        protected void btn_AddTask_Click(object sender, EventArgs e)
        {
            ToDoService.ToDoItemContract toDoItem = new ToDoService.ToDoItemContract();
            toDoItem.Title = txtTask.Text;
            toDoItem.Description = txtDescription.Text;
            toDoItem.ParentId = ddlDependentTasks.SelectedValue;

            Save(toDoItem);

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
            ToDoService.ToDoItemContract toDoItem = new ToDoService.ToDoItemContract();
            toDoItem.Id = e.CommandArgument.ToString();
            toDoItem.Title = (e.Item.FindControl("txtUpdateTitle") as TextBox).Text;
            toDoItem.Description = (e.Item.FindControl("txtUpdateDescription") as TextBox).Text;
            toDoItem.Complete = (e.Item.FindControl("chkComplete") as CheckBox).Checked;
            
            Save(toDoItem);

            // take the list out of edit mode
            dlTasks.EditItemIndex = -1;

            // update the UI
            LoadTasks();
        }

        protected void dlTasks_CancelCommand(object source, DataListCommandEventArgs e)
        {
            dlTasks.EditItemIndex = -1;
            LoadTasks();
        }

        //protected void dlTasks_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item)
        //    {
        //        List<string> items = new List<string>() { "a", "b", "c" };

        //        DropDownList ddlTasks = ((DropDownList)e.Item.FindControl("ddlDependentTasks"));
        //        ddlTasks.DataSource = items;
        //        ddlTasks.DataBind();
        //    }
        //}
    }
}