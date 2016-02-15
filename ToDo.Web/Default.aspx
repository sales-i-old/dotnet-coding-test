<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDo.Web.Default" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="content/bootstrap.min.css" rel="stylesheet">
    <link href="content/todo.css" rel="stylesheet">
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

    <title>ACME To Do List</title>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Title bar -->
        <!--
        TODO: Place these parts in a MasterPage if the number of pages increases
    -->
        <nav class="navbar navbar-default navbar-static-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">ACME To Do List</a>
                </div>
                <div id="navbar" class="navbar-collapse collapse">
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="#" class="btn btn-primary" id="lnkNewTask" title="Create new task"> New task </a></li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </nav>

        <div class="container">

            <div class="row">

                <div class="alert alert-info" role="alert" id="dvResult" style="display:none;"></div>
                <div id="dvNewTask" style="display:none;" >
                <div class="panel panel-default">
  <div class="panel-heading">Add task</div>
  <div class="panel-body">
   
                
                    <div class="form-horizontal col-lg-offset-2">
                        <div class="form-group">
                            <label class="control-label col-lg-2" for="txtTask">Title</label>
                            <div class="col-lg-6">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtTask" required></asp:TextBox>
                        </div></div>
                        <div class="form-group">
                            <label class="control-label col-lg-2" for="txtDescription" >Description</label>
                            <div class="col-lg-6">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDescription" required></asp:TextBox>
                        </div></div>
                        <div class="form-group">
                            <label class="control-label col-lg-2" for="ddlRelatedTask">Related task</label>
                            <div class="col-lg-6">
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRelatedTask" DataTextField="Title" DataValueField="Id"></asp:DropDownList>
                        </div></div>
                        <div class="form-group">
                            <div class="col-lg-offset-2">
                                <a href="#" id="lnkCancel" class="btn btn-default">Cancel</a>
                                <a href="#" id="lnkSaveTask" class="btn btn-primary">Save</a>
                            <!-- <asp:Button runat="server" ID="btnAddTask" CssClass="btn btn-primary" Text="Add Task" OnClick="btn_AddTask_Click" />-->
                        </div></div>
                    </div>
            </div>

       </div>
</div>

        </div>

        <div class="row">

            

            <div class="col-lg-4">

                <section class="tasks">
                    <header class="tasks-header">
                        <h2 class="tasks-title">My tasks</h2>
                        <!--<a href="index.html" class="tasks-lists">Lists</a>-->
                    </header>
                    <fieldset class="tasks-list">
                        <asp:Repeater ID="rptTasks" runat="server">
                            <ItemTemplate>
                        <label class="tasks-list-item">
                            <input type="checkbox" name="task_1" value='<%# Eval("Id") %>' class="tasks-list-cb" <%# (bool)Eval("Complete") ? "checked" : "" %>>
                            <span class="tasks-list-mark"></span>
                            <span class="tasks-list-desc"><%# Eval("Title") %></span>
                        </label>
                            </ItemTemplate>
                        </asp:Repeater> 
                        <label class="tasks-list-item">
                            <input type="checkbox" name="task_1" value="1" class="tasks-list-cb" checked>
                            <span class="tasks-list-mark"></span>
                            <span class="tasks-list-desc">Check This</span>
                        </label>
                        <label class="tasks-list-item">
                            <input type="checkbox" name="task_2" value="1" class="tasks-list-cb" checked>
                            <span class="tasks-list-mark"></span>
                            <span class="tasks-list-desc">Then This</span>
                        </label>
                        <label class="tasks-list-item">
                            <input type="checkbox" name="task_3" value="1" class="tasks-list-cb">
                            <span class="tasks-list-mark"></span>
                            <span class="tasks-list-desc">And Go!</span>
                        </label>
                    </fieldset>
                </section>

            </div>
            <div class="col-lg-8">

                <asp:Literal ID="ltMessage" Text="" runat="server"></asp:Literal>

                <asp:DataList runat="server" ID="dlTasks" RepeatLayout="Flow" RepeatDirection="Horizontal" RepeatColumns="3" OnEditCommand="dlTasks_EditCommand" OnUpdateCommand="dlTasks_UpdateCommand" OnItemDataBound="dlTasks_ItemDataBound" DataKeyField="Id">
                    <HeaderTemplate>
                        <div id="todoItems">
                            <ul id="notes">
                    </HeaderTemplate>

                    <ItemTemplate>
                        <li>
                            <div class="tape"></div>
                            <p>
                            <%# DataBinder.Eval(Container.DataItem, "Title") %> (Related to <%# DataBinder.Eval(Container.DataItem, "RelatedId") == null ? "none" : Eval("RelatedTaskTitle") %>)
                            <br />
                            <asp:CheckBox runat="server" ID="chkComplete" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' Text=" Complete?" />
                            <br />
                            <asp:LinkButton runat="server" ID="lbEdit" Text="Edit" CommandName="Edit"></asp:LinkButton>
                            </p>
                        </li>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <li>
                            <div class="tape"></div>
                            <p>Title
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtUpdateTitle" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' required></asp:TextBox>
                            <br />Description
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtUpdateDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' required></asp:TextBox>
                            <br />Is Complete?
                            <asp:CheckBox runat="server" ID="chkComplete" Text="Complete ?" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' />
                            <br />Related Task
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRelatedTask" DataTextField="Title" DataValueField="Id" />
                            <br />
                            
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnUpdate" Text="Save" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                       </p>
                                 </li>
                    </EditItemTemplate>

                    <FooterTemplate>
                        </ul>
                </div>    
                    </FooterTemplate>
                </asp:DataList>
            </div>
        

        </div>
    </form>

    <script src="scripts/jquery-1.9.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#lnkNewTask").click(function () {
                $("#dvNewTask").slideDown();
            });

            $("#lnkCancel").click(function () {
                $("#dvNewTask").slideUp();
            });

            $("#lnkSaveTask").click(function (e) {
                e.preventDefault();
                $.ajax({
                    method: "POST",
                    url: "Api.aspx/AddTask",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "title":"' + $("#txtTask").val() + '", "description":"' + $("#txtDescription").val() + '", "relatedId":"' + $("#ddlRelatedTask").val() + '"}'
                })
                  .done(function (msg) {
                      $("#dvResult").html(msg.d).show().delay(3000).fadeOut();
                  });
            });
        });
    </script>
</body>
</html>
