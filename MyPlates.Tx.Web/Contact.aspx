<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MyPlates.Tx.Web.Contact" %>

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
document.write('<scr'+'ipt src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17112&amp;rnd=' + ebRand + '"></scr' + 'ipt>');
//]]>
</script>
<noscript>
<img width="1" height="1" style="border:0" src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17112" />
</noscript>
</head>

<body class="section-5 page-1 contact">
    
<div id="banner">
<div id="page">
<mpuc:Header id="PageHeader" runat="server" />

<div id="main">

<h2>Contact Us</h2>
<div class="column-1">
<div class="column-1">
<h4>My Plates</h4>
<p>11149 Research Blvd.<br />Suite 300<br />Austin, TX 78759</p>
<p>E-mail: <a href="mailto:Customerservice@myplates.com">Customerservice@myplates.com</a><br />Toll Free: 877-7MY-PLATES<br />Main: (512) 610-4242<br />Fax: (512) 610-4243<br />
</p>
</div><!--/column-1-->
<div class="column-2">
<h4>Media Contact</h4>
<p>Kim Miller Drummond<br />E-mail: <a href="mailto:kim@myplates.com">kim@myplates.com</a><br />Phone: (512) 610-4250</p>
<p><a href="/Press.aspx">Click here to access our press materials</a>.<br />
</p>
</div><!--/column-2-->
<p class="clear divider">If you have looked through our <a href="/Faq.aspx">FAQ page</a> and still have an unanswered question, please fill out the form below, and we will get back to you shortly.</p>
<form action="/Contact_Success.aspx" method="post" runat="server">
    <div id="contact-a">
        <label for="name">Name</label>
        <asp:TextBox ID="Name" runat="server" CssClass="text required" Width="210px" />
    </div>
    <div id="contact-b">
        <label for="email">Email</label>
        <asp:TextBox ID="Email" runat="server" CssClass="text required" Width="210px" />
    </div>
    <div id="contact-c">
        <label for="message">Message</label>
        <asp:TextBox ID="Message" runat="server" CssClass="text required" TextMode="MultiLine" Height="70px" Width="324px" />
    </div>
    <asp:Button ID="SendMail" runat="server" Text="Send Message" onclick="SendMail_Click" />
</form>
</div><!--/column-1-->
<div class="column-2">
<img src="/img/box_headquarters.png" alt="Map of My Plates Headquarters" class="block" />
<h4>From I-35 or Loop 1:</h4>
<p>Exit 183 North. Take 183 North and exit at Braker Lane/Balcones Woods Drive. Continue North on the frontage road, then turn right into the Iron Stone Bank building immediately before the Balcones Woods Drive traffic light. My Plates is located through the East entrance on the 3rd floor. </p>
<h4>From Hwy 183 heading south:</h4>
<p>Exit Braker Lane/Great Hills Trail onto Research Boulevard. Make a U-Turn at Braker Lane and continue North on Research Boulevard. Turn right into the Iron Stone Bank building immediately before the Balcones Woods Drive traffic light. My Plates is located through the East entrance on the 3rd floor.</p>
</div><!--/column-2-->

</div><!--/main-->
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>