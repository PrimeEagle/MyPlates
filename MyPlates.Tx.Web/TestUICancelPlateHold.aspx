<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUICancelPlateHold.aspx.cs" Inherits="MyPlates.Tx.Web.TestUICancelPlateHold" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
    
    
    </script>
</head>
<body>
    <asp:Label id="TitleLabel" runat="server" Font-Bold="True" Font-Size="X-Large">Cancel Plate Hold</asp:Label>
    <form id="form1" runat="server">
    PlateText: <asp:TextBox ID="PlateText" runat="server"/> (manufacturing text)<br />
    PlateCode: <asp:DropDownList ID="PlateCode" runat="server"></asp:DropDownList><br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Post" onclick="Button1_Click" />
    
<div>
    
    </div>
    </form>
</body>
</html>
