<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ToDo.MVC.ToDoService.ToDoItemContract>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Task
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("Edit", "Task", new { id = Model.Id}))
       { %>
    <p>
    <%=Html.TextBoxFor(x => Model.Title)%>
    </p>

    <p>
        <%=Html.TextBoxFor(x => Model.Description)%>
    </p>

    <p>
        <%=Html.CheckBoxFor(x => Model.Complete) %>
    </p>

    <p>
        <input type="submit" value="Save" />
    </p>
    <%} %>
</asp:Content>
