<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyPlates.Tx.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">
<head>
<title>My Plates</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="description" content="" />
<meta name="keywords" content="" />
<link type="text/css" rel="stylesheet" href="/css/styles.css" />
<script type="text/javascript" src="/js/jquery-1.2.3.pack.js"></script>
<script type="text/javascript" src="/js/scripts.js"></script>
<!--[if IE 7]><link href="/css/ie7.css" rel="stylesheet" type="text/css" /><![endif]-->
<!--[if lte IE 6]><link href="/css/ie6.css" rel="stylesheet" type="text/css" /><![endif]-->

<script type="text/javascript">
var ebRand = Math.random()+ ' ';
ebRand = ebRand * 1000000;
//<![CDATA[
document.write('<scr'+'ipt src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17073&amp;rnd=' + ebRand + '"></scr' + 'ipt>');
//]]>
</script>
<noscript>
<img width="1" height="1" style="border:0" src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17073" />
</noscript>
</head>

<body class="home">
    <asp:LoginView ID="LoginView2" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx" style="position: absolute; top: 209px; left: 50%; margin-left: -172px;"><img src="/img/banner_big_overlay.png" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx" style="position: absolute; top: 209px; left: 50%; margin-left: -172px;"><img src="/img/banner_big_overlay.png" /></a>
        </LoggedInTemplate>
    </asp:LoginView>

<div id="banner">
<div id="page">
<mpuc:Header id="PageHeader" runat="server" />
<div id="main">

<div id="welcome" class="box">
<h3>Welcome to My Plates</h3>
<div class="container">
<p>In Texas, there are now more ways than ever before to express yourself. Because now, there’s My Plates.  With new designs and color options combined with your choice of customization, it’s now possible for your car to say even more about you.</p>
<p>In the “Create-a-plate” section, you’ll be able to create your own plate combination and check its availability. So, what are you waiting for? 
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx">Create your plate now</a>!    
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx">Create your plate now</a>!
        </LoggedInTemplate>
    </asp:LoginView>
</p>
</div><!--/container-->
</div><!--/welcome-->
<!--#include virtual="/CurrentNews.htm" -->

</div><!--/main-->
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>