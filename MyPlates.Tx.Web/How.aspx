<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="How.aspx.cs" Inherits="MyPlates.Tx.Web.How" %>

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

<script type="text/javascript">

var ebRand = Math.random()+ ' ';
ebRand = ebRand * 1000000;
//<![CDATA[
document.write('<scr'+'ipt src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17110&amp;rnd=' + ebRand + '"></scr' + 'ipt>');
//]]>
</script>
<noscript>
<img width="1" height="1" style="border:0" src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17110" />
</noscript>

</head>

<body class="section-2 page-1">
    
<div id="banner">
<div id="page">
<mpuc:Header id="PageHeader" runat="server" />
<!--#include virtual="/inc/how-secnav.htm" -->
<div id="main">

<h2>How It Works</h2>
<div id="content">
<p>You’re just four steps away from your perfect personalized plates:</p>
<p><strong>Step 1:</strong> Choose a plate category, either Custom, Premium or Luxury.<br />Each series offers different design options and levels of customization.</p>
<p><strong>Step 2:</strong> Then choose a design and create your message. You’ll then be able to check if your preferred combination is available for purchase.</p>
<p><strong>Step 3:</strong> Once you’ve decided to purchase a plate, you’ll then need to complete all the purchase and payment information. </p>
<p><strong>Step 4:</strong> Pick up your plates! About three weeks after you ordered your plate, you can pick them up  at your local county office, then put them on your car and enjoy!</p>
<p>Click here to <a href="Options.aspx">view our options and pricing</a> or 
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx">create a plate</a>    
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx">create a plate</a>
        </LoggedInTemplate>
    </asp:LoginView>
    .</p>
</div><!--/content-->
<div id="aside" class="box">
<h3 class="top_faq">Top Plate FAQs</h3>
<div class="container">
<h4>1. Can I order a 7 character plate?</h4>
<p>My Plates will be looking at a first and <br />
limited release of 7 character PLP’s in <br />
early to mid 2009. A full release of 7 <br />
character PLP’s has not yet been <br />
announced.</p>
<h4 class="divider">2. How do I find a charity plate?</h4>
<p>Click on the Charity Plate link at the top <br />
of the page or you can visit the TxDOT <br />
site where they also offer a link to these <br />
plates.</p>
<p><a href="/Faq.aspx">More FAQs&hellip;</a></p>
</div>
<!--/container-->
</div><!--/aside-->

</div><!--/main-->
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>