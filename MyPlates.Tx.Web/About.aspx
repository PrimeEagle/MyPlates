<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="MyPlates.Tx.Web.About" %>

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

<body class="section-4 page-1">
<div id="banner">
<div id="page">
<mpuc:Header id="PageHeader" runat="server" />
<!--#include virtual="/inc/about-secnav.htm" -->
<div id="main">

<h2>About Us</h2>
<div id="content">
<p>My Plates is a Texas-based company that was awarded a contract by the Texas Department of Transportation (TxDOT) to design, market and sell new Specialty license plates in the State of Texas. </p>
<p>We’ve created a whole new range of plate designs including new Texas themes and colors. In fact, you’re probably starting to see a lot of our new plates on the road right now.</p>
<p>When you call our toll-free number, you reach the friendly folks in our Austin office, so you talk with people who are well-versed in the specialty plate process. They also speak Texan and Plate Speak, so they can help you come up with fun combinations to get the plate you want!</p>
<p>In addition to great service and a whole lot of fun, there's another part that’s great for Texans. Because My Plates partners with TxDOT, a portion of each plate purchase goes into the state general fund, which provides services for all Texans.</p>
<p>What are you waiting for? 
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx">Create your plate right now</a>    
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx">Create your plate right now</a>
        </LoggedInTemplate>
    </asp:LoginView>
    !</p>
</div><!--/content-->
<div id="aside" class="box">
<!--#include virtual="/CurrentNews.htm" -->
</div><!--/aside-->

</div><!--/main-->
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>
