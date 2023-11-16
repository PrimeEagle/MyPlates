<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sitemap.aspx.cs" Inherits="MyPlates.Tx.Web.Sitemap" %>

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

<body class="sitemap">
<div id="banner">
<div id="page">
<mpuc:Header id="PageHeader" runat="server" />
<div id="main">

<h2>Site Map</h2>
<p>Can't find what you're looking for? Feel free to call us toll free at 877-7MY-PLAT(ES) or <a href="mailto:Customerervice@myplates.com">click here to send us an e-mail</a>.</p>
<div class="column-1">
<ul>
<li><a href="/">Home</a></li>
<li><asp:HyperLink id="CreateAPlateLink" runat="server">Create a Plate</asp:HyperLink><ul><li><a href="/Cart.aspx">Cart / Checkout</a></li></ul>
</li>
<li><a href="/How.aspx">How It Works</a><ul><li><a href="/Options.aspx">Options & Pricing</a></li><li><a href="/Faq.aspx">Frequently Asked Questions</a></li></ul>
</li>
<li><a href="http://rts.texasonline.state.tx.us/NASApp/txdotrts/SpecialPlateOrderServlet" target="_blank">Charity Plates</a></li>
</ul>
</div><!--/column-1-->
<div class="column-2">
<ul>
<li><a href="/About.aspx">About Us</a><ul><li><a href="/News.aspx">News & Press</a></li>
<!-- <li><a href="/Media.aspx">Media Center</a></li><li><a href="/Press.aspx">Press Materials</a></li> -->
</ul>
</li>
<li><a href="/Contact.aspx">Contact Us</a></li>
<li><a href="/Espanol.aspx">Español</a></li>
<li><a href="/Terms.aspx">Terms and Conditons</a></li>
<li><a href="/Privacy.aspx">Privacy Policy</a></li>
</ul>
</div><!--/column-2-->

</div><!--/main-->
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>
