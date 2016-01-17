<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDo.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ACME To Do List</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="http://code.jquery.com/ui/1.8.24/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://code.jquery.com/ui/1.8.24/themes/blitzer/jquery-ui.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="Scripts/JQueryScript.js"></script>
    <link href="Style/Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="draggableArea" class="container body-content">
            <div id="logo">
                <img src="Images/logo.png" alt="Sales i logo" /></div>
            <div id="todoItemInput">
                <asp:TextBox runat="server" ID="txtTask" OnTextChanged="txtTask_TextChanged"></asp:TextBox>
                <br />
                <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
                <asp:Button runat="server" ID="btnAddTask" Text="Add Task" OnClick="btn_AddTask_Click" />
            </div>
            <asp:DataList runat="server" ID="dlTasks" OnEditCommand="dlTasks_EditCommand" OnUpdateCommand="dlTasks_UpdateCommand" DataKeyField="Id" OnItemDataBound="dlTasks_ItemDataBound">
                <HeaderTemplate>
                    <div id="todoItems">
                </HeaderTemplate>

                <ItemTemplate>
                    <div class="todoItem">
                        <%# DataBinder.Eval(Container.DataItem, "Title") %>
                        <asp:CheckBox runat="server" ID="chkComplete" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' />
                        <%# DataBinder.Eval(Container.DataItem, "ParentTaskTitle") %>
                        <br />
                        <asp:LinkButton runat="server" ID="lbEdit" Text="Edit" CommandName="Edit"></asp:LinkButton>
                    </div>
                </ItemTemplate>

                <EditItemTemplate>
                    <div class="todoItem">
                        <asp:TextBox runat="server" ID="txtUpdateTitle" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></asp:TextBox>
                        <br />
                        <asp:TextBox runat="server" ID="txtUpdateDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:TextBox>
                        <br />
                        <asp:CheckBox runat="server" ID="chkComplete" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' />
                        <br />
                        <asp:CheckBox runat="server" ID="CheckBox1" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' />
                        <asp:DropDownList ID="cboParentTask" runat="server" DataValueField="ID" DataTextField="Title"></asp:DropDownList>
                        <br />
                        <asp:Button runat="server" ID="btnUpdate" Text="Save" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                    </div>
                </EditItemTemplate>

                <FooterTemplate>
                    </div>    
                </FooterTemplate>
            </asp:DataList>
            <div id="droppableArea">Drop here</div>
        </div>
    </form>
</body>
</html>
