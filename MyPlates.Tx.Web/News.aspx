<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="MyPlates.Tx.Web.News" %>

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

<body class="section-4 page-2">
<div id="banner">
<div id="page">
<mpuc:Header id="PageHeader" runat="server" />
<!--#include virtual="/inc/about-secnav.htm" -->
<div id="main">

<h2>News &amp; Press</h2>
<p>If you would like to learn more about My Plates, <a href="Press.aspx">click here to access our press materials</a>. Or call Kim Miller Drummond at 512-610-4250 or e-mail her at <a href="mailto:kim@myplates.com">kim@myplates.com</a>.</p>
<div class="spine-left">
<h4>A classic look for Texas <span class="small">– August 1, 2008</span></h4><br />
<p>New Vintage black and white plates offer Texans a real classic and authentic look for their cars. Whether you drive a car from yesteryear or one from the modern era, these plates will add the perfect finishing touch to your vehicle.</p>
</div><!--/spine-left-->
<div class="spine-right">
<h4>New colors for Texas <span class="small">– August 1, 2008</span></h4><br />
<p>New Lone Star color plates offer Texans a choice of 5 exciting colors including Red, Blue, Orange, Maroon and even a vibrant Pink. </p>
</div><!--/spine-right-->

</div><!--/main-->
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>