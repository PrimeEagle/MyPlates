<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUIRenewPlateHold.aspx.cs" Inherits="MyPlates.Tx.Web.TestUIRenewPlateHold" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
    
    
    </script>
</head>
<body>
    <asp:Label id="TitleLabel" runat="server" Font-Bold="True" Font-Size="X-Large">Renew Plate Hold</asp:Label>
    <form id="form1" runat="server">
    PlateText: <input type="text" />   <br />
    PlateCode: <asp:DropDownList ID="PlateCode" runat="server"></asp:DropDownList>   <br />
                    
    <asp:Button ID="Button1" runat="server" PostBackUrl="/UIRenewPlateHold.aspx" Text="Post" />
<div>
    
    </div>
    </form>
</body>
</html>
