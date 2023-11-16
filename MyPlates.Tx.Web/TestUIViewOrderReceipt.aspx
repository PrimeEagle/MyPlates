<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUIViewOrderReceipt.aspx.cs" Inherits="MyPlates.Tx.Web.TestUIViewOrderReceipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <asp:Label id="TitleLabel" runat="server" Font-Bold="True" Font-Size="X-Large">View Order Receipt</asp:Label>
    <form id="form1" runat="server">
    <div>
        Order ID: <asp:TextBox ID="OrderID" runat="server" /><br /><br />
        <asp:Button ID="ViewReceipt" runat="server" Text="View Receipt" 
            onclick="ViewReceipt_Click" />
    </div>
    </form>
</body>
</html>
