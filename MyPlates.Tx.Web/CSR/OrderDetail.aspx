<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="MyPlates.Tx.Web.CSR.OrderDetail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="description" content="" />
<meta name="keywords" content="" />
<link type="text/css" rel="stylesheet" href="/css/styles.css" />
<script type="text/javascript" src="/js/jquery-1.2.3.pack.js"></script>
<script type="text/javascript" src="/js/scripts.js"></script>
<!--[if IE 7]><link href="/css/ie7.css" rel="stylesheet" type="text/css" /><![endif]-->
<!--[if lte IE 6]><link href="/css/ie6.css" rel="stylesheet" type="text/css" /><![endif]-->
</head>

<body>
<div id="banner">
<div id="page">
<mpuc:Header id="PageHeader" runat="server" />
<form id="Form1" runat="server">
<asp:Label runat="server" ID="CurrentUser" Font-Italic="False" Font-Bold="True">Current User: </asp:Label><%= User.Identity.Name %> (<asp:LoginStatus ID="LoginStatus1" runat="server" />)
<!--#include virtual="/CSR/csr-secnav.htm" --> 
<div id="main">


<h2>Order Details</h2>

<div id="content">
    <asp:Label ID="OrderID" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
    <asp:HyperLink runat="server" ID="ViewReceiptLink">View Receipt</asp:HyperLink><br /><br />
    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Large">Customer Info</asp:Label><br />
    <asp:DataList ID="CustomerInfo" runat="server">
        <ItemTemplate>
            <asp:Label ID="CategoryName" runat="server" Text='<%# Eval("FirstName") %>' /> <asp:Label ID="Label1" runat="server" Text='<%# Eval("LastName") %>' /><br />
            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Street1") %>' /><br />
            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Street2") %>' visible='<%# CheckEmptyData(Eval("Street2").ToString()) %>' /><%# DisplayLineBreak(Eval("Street2").ToString()) %>
            <asp:Label ID="Label4" runat="server" Text='<%# Eval("City") %>' />, <asp:Label ID="Label5" runat="server" Text='<%# Eval("State") %>' /> <asp:Label ID="Label6" runat="server" Text='<%# Eval("ZIP") %>' /><br /><br />
            <asp:Label ID="Label8" runat="server" Text='<%# MyPlates.Tx.Configuration.PlateConfiguration.FormatPhoneNumber(Eval("Phone").ToString()) %>' /><br />
            <asp:Label ID="Label14" runat="server" Text='<%# Eval("Email") %>' /><br /><br />
        </ItemTemplate>
        </asp:DataList>
    <br /><br />
    <asp:DataList ID="PlateInfo" runat="server" RepeatColumns="2" 
            RepeatDirection="Horizontal" Width="600px">
        <ItemTemplate>
            <img src='/UIDisplayPlateImage.aspx?PlateID=<%# Eval("PlateGuid") %>' alt='Plate Combination is: <%# Eval("MfgText").ToString().Replace("%", "") %>' width="127" height="64" /> <br />       
            <table>
                <tr>
                    <td><strong>Plate Text:</strong></td>
                    <td>"<asp:Label ID="Label9" runat="server" Text='<%# Eval("PlateText") %>' />"</td>
                </tr>
                <tr>
                    <td><strong>Plate Mfg Text:</strong></td>
                    <td>"<asp:Label ID="Label10" runat="server" Text='<%# Eval("MfgText") %>' />"</td>
                </tr>
                <tr>
                    <td><strong>Plate Name:</strong></td>
                    <td>"<asp:Label ID="Label13" runat="server" Text='<%# Eval("Name") %>' />"</td>
                </tr>
                <tr>
                    <td><strong>Renewal Period:</strong></td>
                    <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("RenewalPeriod") %>' /> years</td>
                </tr>
                <tr>
                    <td><strong>Total Paid:</strong></td>
                    <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("TotalPaid", "{0:C}") %>' /></td>
                </tr>                                                
            </table>
            <br />
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large">Owner Info</asp:Label><br />
            <asp:Label ID="CategoryName" runat="server" Text='<%# Eval("FirstName") %>' /> <asp:Label ID="Label2" runat="server" Text='<%# Eval("LastName") %>' /><br />
            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Street1") %>' /><br />
            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Street2") %>' visible='<%# CheckEmptyData(Eval("Street2").ToString()) %>' /><%# DisplayLineBreak(Eval("Street2").ToString()) %>
            <asp:Label ID="Label15" runat="server" Text='<%# MyPlates.Tx.Configuration.PlateConfiguration.ToTitleCase(Eval("County").ToString()) %>' /> County<br />
            <asp:Label ID="Label5" runat="server" Text='<%# Eval("City") %>' />, <asp:Label ID="Label6" runat="server" Text='<%# Eval("State") %>' /> <asp:Label ID="Label7" runat="server" Text='<%# Eval("ZIP") %>' /><br /><br />
            <asp:Label ID="Label14" runat="server" Text='<%# Eval("Email") %>' /><br />
            <asp:Label ID="Label8" runat="server" Text='<%# MyPlates.Tx.Configuration.PlateConfiguration.FormatPhoneNumber(Eval("Phone").ToString()) %>' /><br /><br />
        </ItemTemplate>    
    </asp:DataList>

</div><!--/content-->

</div><!--/main-->
</form>
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>