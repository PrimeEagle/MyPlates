<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlatesByDateRange.aspx.cs" Inherits="MyPlates.Tx.Web.CSR.PlatesByDateRange" %>

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


<h2>View Plates</h2>

<div id="content">
    <table>
    <tr>
        <td>From Date:
            <asp:CheckBox ID="FromDateCheck" runat="server" AutoPostBack="True" oncheckedchanged="FromDateCheck_CheckedChanged" />
         </td>
        <td>To Date:
            <asp:CheckBox ID="ToDateCheck" runat="server" AutoPostBack="True" Enabled="False" oncheckedchanged="ToDateCheck_CheckedChanged" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Calendar ID="FromDate" runat="server" BackColor="White" 
                BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                Width="200px" Visible="False">
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <WeekendDayStyle BackColor="#FFFFCC" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <NextPrevStyle VerticalAlign="Bottom" />
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            </asp:Calendar>
        </td>
        <td>
            <asp:Calendar ID="ToDate" runat="server" BackColor="White" 
                BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                Width="200px" Visible="False">
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <WeekendDayStyle BackColor="#FFFFCC" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <NextPrevStyle VerticalAlign="Bottom" />
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            </asp:Calendar>
        </td>
    </tr>
    </table>
    <br />
    <asp:Button ID="Search" runat="server" Text="Search" onclick="Search_Click" />
    <hr />
    
    <asp:DataList ID="PlateImages" runat="server" RepeatLayout="Flow" RepeatColumns="2" 
                RepeatDirection="Horizontal">
    <ItemTemplate>
        <a href="/CSR/OrderDetail.aspx?orderid=<%# Eval("Order_ID") %>">
            <img src='/UIDisplayPlateImage.aspx?PlateID=<%# Eval("PlateGuid") %>' width="<%# Eval("ImageSizeX") %>" height="<%# Eval("ImageSizeY") %>" alt='Plate Combination is: <%# Eval("MfgText").ToString().Replace("%", "") %>' />
        </a>
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