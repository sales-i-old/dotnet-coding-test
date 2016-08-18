<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDo.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ACME To Do List</title>
    <%--ANDREI: Bootstrap--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <%--ANDREI: JQuery--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="http://code.jquery.com/ui/1.8.24/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://code.jquery.com/ui/1.8.24/themes/blitzer/jquery-ui.css" rel="stylesheet"
        type="text/css" />
    <%--ANDREI: JQuery drap and drop--%>
    <script type="text/javascript" src="Scripts/JQueryScript.js"></script>
    <%--ANDREI: CSS--%>
    <link href="Style/Style.css" rel="stylesheet" />
</head>
<body class="container body-content">
    <form id="form1" runat="server">
        <div id="draggableArea">
            <div id="logo">
                <img src="Images/logo.png" alt="Sales i logo" class="img-responsive"/></div>
            <div id="todoItemInput">
                <div class="form-group">
					  <label class="control-label">Title:</label>
                      <asp:TextBox runat="server" ID="txtTask" OnTextChanged="txtTask_TextChanged" class="form-control required" MaxLength="255"></asp:TextBox>
			    </div>
                <div class="form-group">
					  <label class="control-label">Description:</label>
                      <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" class="form-control required" MaxLength="4000"></asp:TextBox>
			    </div>
                <asp:Button runat="server" ID="btnAddTask" Text="Add Task" OnClick="btn_AddTask_Click" class="btn btn-danger" />
            </div>
            <asp:DataList runat="server" ID="dlTasks" OnEditCommand="dlTasks_EditCommand" OnUpdateCommand="dlTasks_UpdateCommand" DataKeyField="Id" OnItemDataBound="dlTasks_ItemDataBound" RepeatDirection="Vertical">
                <HeaderTemplate>
                    <div id="todoItems">
                </HeaderTemplate>

                <ItemTemplate>
                    <div class="todoItem">
                        <div>
					    <label class="control-label">Title:</label>
                        <label class="control-label"><%# DataBinder.Eval(Container.DataItem, "Title") %></label>
			            </div>
                        <div>
					    <label class="control-label">Complete:</label>
                        <asp:CheckBox runat="server" ID="chkComplete" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>'/>
			            </div>
                        <%--Andrei: dispayed the parent task title--%>
                        <div>
					    <label class="control-label">Parent Task Title:</label>
                        <label class="control-label"><%# DataBinder.Eval(Container.DataItem, "ParentTaskTitle") %></label>
			            </div>
                        <div id="hideButton"><asp:LinkButton runat="server" ID="lbEdit" Text="Edit" CommandName="Edit" class="btn btn-danger"></asp:LinkButton></div>
                    </div>
                </ItemTemplate>

                <EditItemTemplate>
                    <div class="todoItem">
                        <div class="editItem">
					    <label class="control-label">Title:</label>
                        <asp:TextBox runat="server" ID="txtUpdateTitle" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' class="form-control"></asp:TextBox>
			            </div>
                        <div class="editItem">
					    <label class="control-label">Description:</label>
                        <asp:TextBox runat="server" ID="txtUpdateDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' class="form-control"></asp:TextBox>
			            </div>
                        <div class="editItem">
					    <label class="control-label">Complete:</label>
                        <asp:CheckBox runat="server" ID="chkComplete" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>'/>
			            </div>
                        <div class="editItem">
					    <label class="control-label">Parent Task Title:</label>
                        <%--ANDREI: populated the dropdown list--%>
                        <asp:DropDownList ID="cboParentTask" runat="server" DataValueField="ID" DataTextField="Title" class="form-control"></asp:DropDownList>
			            </div>
                        <div class="editItem">
                        <asp:Button runat="server" ID="btnUpdate" Text="Save" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' class="btn btn-danger"/>
                        </div>
                    </div>
                </EditItemTemplate>

                <FooterTemplate>
                    </div>    
                </FooterTemplate>
            </asp:DataList>
            <%--ANDREI: droppable area--%>
            <div id="droppableArea">Drop here</div>
        </div>
    </form>
</body>
</html>
