<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDo.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ACME To Do List</title>
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css"/>
    <!--JQuery -->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="http://code.jquery.com/ui/1.8.24/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://code.jquery.com/ui/1.8.24/themes/blitzer/jquery-ui.css" rel="stylesheet"
        type="text/css" />
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .draggable
        {
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .dropped
        {
            position: static !important;
        }
        #dvSource, #dvDest
        {
            border: 5px solid #ccc;
            padding: 5px;
            min-height: 100px;
            width: 430px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#dlTasks table tr").draggable({
                revert: "true",
                refreshPositions: true,
                drag: function (event, ui) {
                    ui.helper.addClass("draggable");
                }
            });
            $("#dvDest").droppable({
                drop: function (event, ui) {
                    if ($("#dvDest table").length == 0) {
                        $("#dvDest").html("");
                    }
                    ui.draggable.addClass("dropped");
                    $("#dvDest").append(ui.draggable);
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>ACME To Do List</h1>
        <div>
            <asp:TextBox runat="server" ID="txtTask" OnTextChanged="txtTask_TextChanged"></asp:TextBox>
            <br />
            <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
            <asp:Button runat="server" ID="btnAddTask" Text="Add Task" OnClick="btn_AddTask_Click"/>
        </div>
        <asp:DataList runat="server" ID="dlTasks" OnEditCommand="dlTasks_EditCommand" OnUpdateCommand="dlTasks_UpdateCommand" DataKeyField="Id" OnItemDataBound="dlTasks_ItemDataBound" >
            <HeaderTemplate>
                <div id="todoItems">
                    <table>
            </HeaderTemplate>

            <ItemTemplate>
                        <tr><td>
                            <%# DataBinder.Eval(Container.DataItem, "Title") %> <asp:CheckBox runat="server" ID="chkComplete" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' />
                            <%--ANDREI: binded the parent task text--%>
                            <%# DataBinder.Eval(Container.DataItem, "ParentTaskTitle") %>
                            <br />
                            <asp:LinkButton runat="server" ID="lbEdit" Text="Edit" CommandName="Edit"></asp:LinkButton>
                        </td></tr>
            </ItemTemplate>

            <EditItemTemplate>
                        <tr><td>
                            <asp:TextBox runat="server" ID="txtUpdateTitle" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></asp:TextBox>
                            <br />
                            <asp:TextBox runat="server" ID="txtUpdateDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:TextBox>
                            <br />
                            <asp:CheckBox runat="server" ID="chkComplete" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' />
                            <br />
                            <%--ANDREI: added the parent task drop down--%>
                            <asp:DropDownList ID="cboParentTask" runat="server" DataTextField="Title" DataValueField="ID"/>
                            <br />
                            <asp:Button runat="server" ID="btnUpdate" Text="Save" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                        </td></tr>
            </EditItemTemplate>

            <FooterTemplate>
                    </table>
                </div>    
            </FooterTemplate>
        </asp:DataList>
        <hr />
        <div id="dvDest">
            Drop here
        </div>
    </div>
    </form>
</body>
</html>
