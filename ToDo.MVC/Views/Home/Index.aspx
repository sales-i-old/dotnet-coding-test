<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>

    <% using (Html.BeginForm("Create", "Task"))
       { %>

       <div>
        <%= Html.TextBox("TaskName") %>
        <br />
        <%= Html.TextBox("TaskDescription") %>
        <br />
        <input type="submit" value="Save" />
       </div>



    <%} %>

    <div>
    <ul>
    <% foreach (var item in ViewData["Tasks"] as List<ToDo.MVC.ToDoService.ToDoItemContract>) {%>

    <li>
    <span style="font-weight: bold;"><%=Html.ActionLink(item.Title, "Details", "Task", null, null, "", new { id = item.Id }, null)%></span>
    <br />
    <%=item.Description %>
    <br />
    <%=Html.CheckBox("complete", item.Complete, new { disabled = true })%>
    </li>
    
    <%} %>
    </ul>
    </div>
    
</asp:Content>
