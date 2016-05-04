<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ToDo.MVC.Models.HomeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: @Model.Message %></h2>

    <% using (Html.BeginForm("Create", "Task"))
       { %>

       <div>
        <%= Html.TextBox("TaskName") %>
        <br />
        <%= Html.TextBox("TaskDescription") %>
        <br />
        <%= Html.DropDownListFor( x=> x.SelectedParent, Model.AvailableParents, "Please select optional parent task") %>
        <br />
        <input type="submit" value="Save" />
       </div>



    <%} %>

    <div>
    <ul>
    <% foreach (var item in @Model.TodoItems) {%>

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
