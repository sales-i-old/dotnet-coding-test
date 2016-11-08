<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDo.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ACME To Do List</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>ACME To Do List</h1>
        <div>
            Task Name: <asp:TextBox runat="server" ID="txtTask"></asp:TextBox>
            <br />
            <br />
            Task Description:<asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
            <br />
            <br />
            Parent Task: <asp:DropDownList ID="txtDropDownList" runat="server" Height="20" Width="250"
                DataTextField="Title" DataValueField="Id" >
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button runat="server" ID="btnAddTask" Text="Add Task" OnClick="btn_AddTask_Click" />
        </div>

        <br />
        <br />
        <h1>Here is the Task List</h1>
        <asp:DataList runat="server" ID="dlTasks" OnEditCommand="dlTasks_EditCommand" OnUpdateCommand="dlTasks_UpdateCommand" DataKeyField="Id" >
            <HeaderTemplate>
                
                <div id="todoItems">
                           
                    <ol>
            </HeaderTemplate>
            <ItemTemplate>
                        <li>
                            <%--//TODO: Just to display Parent Task modified the below code--%>
                            <%# DataBinder.Eval(Container.DataItem, "Title") %> => <%# DataBinder.Eval(Container.DataItem, "ParentId") %> <asp:CheckBox runat="server" ID="chkComplete" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' />
                            &nbsp;<asp:LinkButton runat="server" ID="lbEdit" Text="Edit" CommandName="Edit"></asp:LinkButton>
                        </li>
            </ItemTemplate>

            <EditItemTemplate>
                <br />
                        <li>
                            TaskName :<asp:TextBox runat="server" ID="txtUpdateTitle" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></asp:TextBox>
                            <br />
                            Task Description: <asp:TextBox runat="server" ID="txtUpdateDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:TextBox>
                            <br />
                            <asp:CheckBox runat="server" ID="chkComplete" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' />
                            <br />
                            <asp:Button runat="server" ID="btnUpdate" Text="Save" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                        </li>
                <br />
            </EditItemTemplate>

            <FooterTemplate>
                    </ol>
                </div>    
            </FooterTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
