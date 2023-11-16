<%@ Register TagPrefix="mpuc" TagName="HeaderNoBanner" Src="/inc/HeaderNoBanner.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAPlate.aspx.cs" Inherits="MyPlates.Tx.Web.CreateAPlate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">
<head>
<title>MyPlates Create a Plate</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="description" content="" />
<meta name="keywords" content="" />
<link type="text/css" rel="stylesheet" href="/css/styles.css" />
<script type="text/javascript" src="/js/jquery-1.2.3.pack.js"></script>
<script type="text/javascript" src="/js/swfobject.js"></script>
<script type="text/javascript" src="/js/scripts.js"></script>
<!--[if IE 7]><link href="/css/ie7.css" rel="stylesheet" type="text/css" /><![endif]-->
<!--[if lte IE 6]><link href="/css/ie6.css" rel="stylesheet" type="text/css" /><![endif]-->
		<script type="text/javascript">
			var flashvars = {
				loadXMLURL: "/UIPopulate.aspx",
				checkAvailabilityURL: "/UICheckAvailability.aspx",
				addToCartURL: "/UIAddPlateToCart.aspx",
				redirectURL: "/Cart.aspx",
				holdInterval: "840000",
				updateHoldURL: "/UIRenewPlateHold.aspx",
				jpgURL: "/UIAddPlateImage.aspx"
			};
			var params = {
				menu: "false"
			};
			var attributes = {};
			swfobject.embedSWF("/flash/createaplate.swf", "createaplate", "900", "530", "9.0.0", "/flash/expressInstall.swf", flashvars, params, attributes);
		</script>
		
<script type="text/javascript">
var ebRand = Math.random()+ ' ';
ebRand = ebRand * 1000000;
//<![CDATA[
document.write('<scr'+'ipt src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17074&amp;rnd=' + ebRand + '"></scr' + 'ipt>');
//]]>
</script>
<noscript>
<img width="1" height="1" style="border:0" src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17074" />
</noscript>		
</head>

<body class="section-1">
    
<div>
<div id="page">

<mpuc:HeaderNoBanner id="PageHeader" runat="server" />

<div id="createaplate">

<div id="main">

<h2>Create A Plate</h2>
<p>We’re sorry you can’t access this page. This site primarily features graphics to display and communicate products. Please <a href="http://www.adobe.com/products/flashplayer/" target="_blank">click here</a> to access this page by installing the correct software or call 877-7MY-PLAT(ES) if you’re having difficulty. Our hours are 8 am to 6 pm CST on weekdays.</p>

</div><!--/main-->

</div><!--/createaplate-->

<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>
