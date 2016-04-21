<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDo.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ACME To Do List</title>
    <style type="text/css">
        li {
            min-width:200px;
            height:auto;
            border:1px solid #ccc;
            box-sizing:border-box;
            margin-bottom:10px;
            padding:10px;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });

        function LoadDependentTaskOptions(id) {
            
            // pass id to WebMethod in back end to get any dependent tasks
            $.ajax({
                url: 'Default.aspx/GetDependentTaskOptions',
                data: JSON.stringify({ 'id': id }),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var options = $("#ddlDependentTaskOptions");
                    // only show if data is not empty
                    if (data.d != "") {

                        debugger;
                        // only show if data is not empty
                        if (data.d != "") {
                            
                            var innerhtml = '<option value="">No dependant task</option>';
                            for (var i = 0; i < data.d.length; i++) {
                                 innerhtml += '<option value="' + data.d[i].Id + '">' + data.d[i].Title + '</option>';
                            }
                            innerhtml 
                            options.html(innerhtml);
                        }
                    }

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    // notify user of any errors
                    $('#' + id).html(thrownError);
                }
            });
        }

        function LoadDependentTasks(id) {

            // pass id to WebMethod in back end to get any dependent tasks
            $.ajax({
                url: 'Default.aspx/GetAnyDependentTasks',
                data: JSON.stringify({ 'id': id }),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    // only show if data is not empty
                    if (data.d != "") {

                        // append header title
                        $('#' + id).append("<p>Dependant Tasks:</p>");

                        // append dependant tasks
                        $('#' + id).append(data.d);
                        debugger;
                        // set the selected dropdown list value
                        var taskOptions = $("#ddlDependentTaskOptions");
                        var task = data.d.slice(8, -10);
                        $("#ddlDependentTaskOptions option:contains(" + task + ")").attr('selected', true);
                       
                        // disable save button
                        $('.' + id).attr("disabled", "disabled");
                    }

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    // notify user of any errors
                    $('#' + id).html(thrownError);
                }
            });
        }

        function UpdateDependentTasks(id, parentid) {
            debugger;
            // pass id to WebMethod in back end to get any dependent tasks
            $.ajax({
                url: 'Default.aspx/UpdateDependentTasks',
                data: JSON.stringify({ 'id': id, 'parentid': parentid }),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#' + id).html("");
                    LoadDependentTaskOptions(id);
                    LoadDependentTasks(id);                
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    // notify user of any errors
                    $('#' + id).html(thrownError);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>ACME To Do List</h1>
        <div>
            <asp:TextBox runat="server" ID="txtTask"></asp:TextBox>
            <br />
            <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
            <br />
            <asp:DropDownList ID="ddlDependentTasks" runat="server"></asp:DropDownList>
            <br />
            <asp:Button runat="server" ID="btnAddTask" Text="Add Task" OnClick="btn_AddTask_Click" />

        </div>
        <asp:DataList runat="server" ID="dlTasks" OnCancelCommand="dlTasks_CancelCommand" OnEditCommand="dlTasks_EditCommand" OnUpdateCommand="dlTasks_UpdateCommand" DataKeyField="Id" >
            <HeaderTemplate>
                <div id="todoItems">
                    <ol>
            </HeaderTemplate>

            <ItemTemplate>
                        <li>
                            <%# DataBinder.Eval(Container.DataItem, "Title") %> <asp:CheckBox runat="server" ID="chkComplete" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' />
                            <br />
                            <asp:LinkButton runat="server" ID="lbEdit" Text="Edit" CommandName="Edit"></asp:LinkButton>
                        </li>
            </ItemTemplate>

            <EditItemTemplate>
                        <li>
                            <asp:TextBox runat="server" ID="txtUpdateTitle" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></asp:TextBox>
                            <br />
                            <asp:TextBox runat="server" ID="txtUpdateDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:TextBox>
                            <br />
                            
                            <asp:CheckBox runat="server" ID="chkComplete" Text="Complete" Checked='<%# DataBinder.Eval(Container.DataItem, "Complete") %>' />
                            <br />
                            <asp:Button runat="server" ID="btnUpdate" Text="Save" CssClass='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                            <asp:Button runat="server" ID="btnCancel" Text="Close"  CommandName="Cancel" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                            <br />
                            <select id="ddlDependentTaskOptions" onchange="UpdateDependentTasks('<%# DataBinder.Eval(Container.DataItem, "Id") %>', this.value);"></select>
                            <div id='<%# DataBinder.Eval(Container.DataItem, "Id") %>'>

                            </div>
                            <script type="text/javascript">
                                LoadDependentTaskOptions('<%# DataBinder.Eval(Container.DataItem, "Id") %>');
                                LoadDependentTasks('<%# DataBinder.Eval(Container.DataItem, "Id") %>');

                            </script>
                        </li>
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
