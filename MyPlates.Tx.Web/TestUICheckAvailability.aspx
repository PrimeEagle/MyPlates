<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUICheckAvailability.aspx.cs" Inherits="MyPlates.Tx.Web.TestUICheckAvailability" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
    
    
    </script>
</head>
<body>
    <asp:Label id="TitleLabel" runat="server" Font-Bold="True" Font-Size="X-Large">Check Plate Availability</asp:Label>
    <form id="form1" runat="server">
    PlateText: <input type="text" name="PlateText" />    <br />
    PlateCode: <asp:DropDownList ID="PlateCode" runat="server"></asp:DropDownList>

    
    <asp:Button ID="Button1" runat="server" PostBackUrl="/UICheckAvailability.aspx" Text="Post" />
<div>
    
    </div>
    </form>
</body>
</html>
