<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="ToDo.Web.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
</head>
<body>
     <h1>Error</h1>
    <form id="form1" runat="server">
    <div> 
        <asp:Literal ID="ltErrorMessage" Text="An error has occurred. Please try again or contact our support team if the problem persists." runat="server" />
    </div>
    <div>  
        <br /><br /><b>Detailed information</b><br />   
        <asp:Literal ID="ltException" Text="" runat="server"></asp:Literal> 
    </div> 
    </form>
</body>
</html>
