<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ToDo.MVC.Models.TaskViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Task
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("Edit", "Task", new { id = Model.TodoItem.Id}))
       { %>
    <p>
    <%=Html.TextBoxFor(x => Model.TodoItem.Title)%>
    </p>

    <p>
        <%=Html.TextBoxFor(x => Model.TodoItem.Description)%>
    </p>


    <p>
            <%= Html.DropDownListFor( x=> x.SelectedParent, Model.AvailableParents, "") %>
    </p>

    <p>
        <%=Html.CheckBoxFor(x => Model.TodoItem.Complete)%>
    </p>
    <p>
        <input type="submit" value="Save" />
    </p>
    <%} %>
</asp:Content>
