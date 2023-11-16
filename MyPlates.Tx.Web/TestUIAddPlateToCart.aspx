<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUIAddPlateToCart.aspx.cs" Inherits="MyPlates.Tx.Web.TestUIAddPlateToCart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
    
    
    </script>
</head>
<body>
    <asp:Label id="TitleLabel" runat="server" Font-Bold="True" Font-Size="X-Large">Add Plate to Cart</asp:Label>
    <form id="form1" runat="server">
    PlateText: <input type="text" name="PlateText" />   <br />
    PlateCode: <asp:DropDownList ID="PlateCode" runat="server"></asp:DropDownList>   <br />
    RenewalPeriod: <input type="text" name="RenewalPeriod" />    <br />
    FirstName: <input type="text" name="FirstName" />   <br />
    LastName: <input type="text" name="LastName" />   <br />
    Street1: <input type="text" name="Street1" />   <br />
    Street2: <input type="text" name="Street2" />   <br />
    City: <input type="text" name="City" />   <br />
    County: <input type="text" name="County" />   <br />
    ZipCode: <input type="text" name="ZipCode" />   <br />
    Phone: <input type="text" name="Phone" />   <br />
    Email: <input type="text" name="Email" />   <br />
                    
    <asp:Button ID="Button1" runat="server" PostBackUrl="/UIAddPlateToCart.aspx" Text="Post" />
<div>
    
    </div>
    </form>
</body>
</html>
