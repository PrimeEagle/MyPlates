<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUIGetCountyInfo.aspx.cs" Inherits="MyPlates.Tx.Web.TestUIGetCountyInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <asp:Label id="TitleLabel" runat="server" Font-Bold="True" Font-Size="X-Large">Get County Info</asp:Label>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="county" runat="server">
        </asp:DropDownList>        
        <br /><br />
        <asp:Button ID="GetInfo" runat="server" Text="Get Info" 
            onclick="GetInfo_Click" PostBackUrl="/UIGetCountyInfo.aspx" />    
    </div>
    </form>
</body>
</html>
