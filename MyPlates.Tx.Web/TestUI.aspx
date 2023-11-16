<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUI.aspx.cs" Inherits="MyPlates.Tx.Web.TestUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Test UI</title>
</head>
<body>
    <asp:Label id="TitleLabel" runat="server" Font-Bold="True" Font-Size="X-Large">Test Plate</asp:Label>
    <form id="form1" runat="server">
    <div>
        <a href="/TestUIGetCountyInfo.aspx">Get County Info</a><br />
        <a href="/TestUICheckAvailability.aspx">Check Availability</a><br />
        <a href="/TestUIAddPlateToCart.aspx">Add Plate to Cart</a><br />
        <a href="/TestUIRenewPlateHold.aspx">Renew Plate Hold</a><br />
        <a href="/TestUISendPayment.aspx">Send Payment to ePay</a><br />
        <a href="/TestUIOrderPlate.aspx">Order Plate</a> (TxDOT Order only - no payment processing)<br />
        <a href="/TestUICancelPlateHold.aspx">Cancel Plate Hold</a><br />
        <a href="/TestUIViewOrderReceipt.aspx">View Order Receipt</a><br />
    </div>
    </form>
</body>
</html>
