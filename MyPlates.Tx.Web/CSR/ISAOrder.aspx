<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ISAOrder.aspx.cs" Inherits="MyPlates.Tx.Web.ISAOrder" %>

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
<form id="Form2" runat="server">
<asp:Label runat="server" ID="CurrentUser" Font-Italic="False" Font-Bold="True">Current User: </asp:Label><%= User.Identity.Name %> (<asp:LoginStatus ID="LoginStatus1" runat="server" />)
<!--#include virtual="/CSR/csr-secnav.htm" --> 
<div id="main">


<h2>ISA Plate Order</h2>

<div id="content">
    <div style="height: 800px">
        <asp:Label ID="LabelCategory" runat="server" Text="Category:" />
        <asp:DropDownList ID="CategoriesDropdown" runat="server" 
            onselectedindexchanged="CategoriesDropdown_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>

        <br />
        <br />
        
        <asp:Label ID="Label1" runat="server" Text="Plate:" />
        <asp:DropDownList ID="PlateCodesDropdown" runat="server" AutoPostBack="True">
        </asp:DropDownList>

        <br />
        <br />
        
        <asp:Label ID="Label2" runat="server" Text="Plate Text:" />        
        <asp:DropDownList ID="PlateTextDropDown" runat="server" 
            AppendDataBoundItems="true" 
            onselectedindexchanged="PlateTextDropDown_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Text="(Select a Reserved Plate)" Value="" />
        </asp:DropDownList>
        <asp:TextBox ID="PlateText" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label3" runat="server" Text="Renewal Period" />        
        <asp:DropDownList ID="RenewalDropdown" runat="server">
        </asp:DropDownList>

        <br />
        <br />
        
        <asp:Button ID="CheckAvailabilityButton" runat="server" Text="Check Availability" 
            onclick="CheckAvailabilityButton_Click" />
        <asp:Label ID="ResponseLabel" runat="server" Font-Bold="True" 
            ForeColor="#CC0000"></asp:Label>
        <br />
        <br />
        
        OWNER INFO
        
        <br />
        <br />

        <asp:Label ID="Label4" runat="server" Text="First Name:" />        
        <asp:TextBox ID="FirstNameBox" runat="server"></asp:TextBox>

        <br />
        <br />


        <asp:Label ID="Label5" runat="server" Text="Last Name:" />        
        <asp:TextBox ID="LastNameBox" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label6" runat="server" Text="Street 1:" />        
        <asp:TextBox ID="Street1Box" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label7" runat="server" Text="Street 2:" />        
        <asp:TextBox ID="Street2Box" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label8" runat="server" Text="City:" />        
        <asp:TextBox ID="CityBox" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label9" runat="server" Text="County:" />        
        <asp:DropDownList ID="CountyDropdown" runat="server" AppendDataBoundItems="true">
            <asp:ListItem Text="Select County" Value="" />
        </asp:DropDownList>        

        <br />
        <br />

        <asp:Label ID="Label10" runat="server" Text="State:" />        
        <asp:Label ID="Label12" runat="server" Text="TX" />        

        <br />
        <br />

        <asp:Label ID="Label11" runat="server" Text="ZIP:" />        
        <asp:TextBox ID="ZIPBox" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label13" runat="server" Text="Phone:" />        
        <asp:TextBox ID="PhoneBox" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label14" runat="server" Text="Email:" />        
        <asp:TextBox ID="EmailBox" runat="server"></asp:TextBox>

        <br />
        <br />                
                
        <br /> 
        <asp:Button ID="bAdd" runat="server" Text="Add Plate to Cart" onclick="bAdd_Click" />
        <asp:Label ID="AddLabel" runat="server" Font-Bold="True" ForeColor="#CC0000" />  

        <br />
        <br />                

    </div>

</div><!--/content-->

</div><!--/main-->
</form>
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>