<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="MyPlates.Tx.Web.CSR.CustomerSearch" %>



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
<div id="widepage">
<mpuc:Header id="PageHeader" runat="server" />
<form id="Form2" runat="server">
<asp:Label runat="server" ID="CurrentUser" Font-Italic="False" Font-Bold="True">Current User: </asp:Label><%= User.Identity.Name %> (<asp:LoginStatus ID="LoginStatus1" runat="server" />)
<!--#include virtual="/CSR/csr-secnav.htm" --> 
<div id="main">

<div id="content">
    <asp:Button ID="Search" Text="Search" runat="server" onclick="Search_Click" /><br />
    
    <h2>Customer Search</h2>
    <table>
        <tr>
            <td>First Name:</td>
            <td><asp:TextBox ID="FirstName" runat="server" Width="250px" /></td>
            <td>Last Name:</td>
            <td><asp:TextBox ID="LastName" runat="server" Width="250px" /></td>
        </tr>
        <tr>
            <td>Phone:</td>
            <td><asp:TextBox ID="Phone" runat="server" /></td>
            <td>ZIP:</td>
            <td><asp:TextBox ID="ZIP" runat="server" /></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><asp:TextBox ID="Email" runat="server" Width="250px" /></td>
            <td>County:</td>
            <td>
                <asp:DropDownList ID="county" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="<All Counties>" Value=""></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    
    <hr />
    <h2>Plate Search</h2>
    <table>
        <tr>
            <td>Plate Text:</td>
            <td><asp:TextBox ID="PlateText" runat="server" Width="100px" /></td>
        </tr>
    </table>
    
    <hr />
    <h2>Order Search</h2>
    
    <table>
    <tr>
        <td>Order ID:</td>
        <td><asp:TextBox ID="OrderID" runat="server" Width="100px" /><br /></td>
        <td>Username:</td>
        <td><asp:DropDownList runat="server" ID="Username" AppendDataBoundItems="true">
                <asp:ListItem Text="(None Selected)" Value="" />
            </asp:DropDownList>
        </td>
    </tr>    
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
       
    <hr /> 
    
    <asp:GridView ID="ResultsGrid" runat="server" CellPadding="4" ForeColor="Black" 
        GridLines="Vertical" AllowPaging="True" 
            onpageindexchanging="ResultsGrid_PageIndexChanging" 
            AllowSorting="True" onsorting="ResultsGrid_Sorting" PageSize="25" 
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                        BorderStyle="None" BorderWidth="1px">
        <FooterStyle BackColor="#CCCC99" />
        <RowStyle BackColor="#F7F7DE" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="Black" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
            <asp:BoundField DataField="Street1" HeaderText="Street 1" SortExpression="Street1" />
            <asp:BoundField DataField="Street2" HeaderText="Street 2" SortExpression="Street2" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="County" HeaderText="County" SortExpression="County" />
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
            <asp:BoundField DataField="ZIP" HeaderText="ZIP" SortExpression="ZIP" />
            <asp:BoundField DataField="PlateText" HeaderText="PlateText" SortExpression="PlateText" />
            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
            <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
            <asp:BoundField DataField="OrderTimestamp" HeaderText="Order Time" SortExpression="OrderTimestamp" />
            <asp:HyperLinkField HeaderText="Order ID" SortExpression="Order_ID" DataNavigateUrlFields="Order_ID" DataNavigateUrlFormatString="/CSR/OrderDetail.aspx?orderid={0}" DataTextField="Order_ID" NavigateUrl="/CSR/OrderDetail.aspx" />
        </Columns>
    </asp:GridView>

</div><!--/content-->

</div><!--/main-->
</form>
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>