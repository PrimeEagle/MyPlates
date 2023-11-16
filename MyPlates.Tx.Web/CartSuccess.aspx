<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CartSuccess.aspx.cs" Inherits="MyPlates.Tx.Web.CartSuccess" %>

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
<!-- <script type="text/javascript" src="/js/swfobject.js"></script> -->
<!-- <% =CreateFlashPlateViewScript() %> -->
<!--[if IE 7]><link href="/css/ie7.css" rel="stylesheet" type="text/css" /><![endif]-->
<!--[if lte IE 6]><link href="/css/ie6.css" rel="stylesheet" type="text/css" /><![endif]-->

<script type="text/javascript">

var ebRev = '<%= GetOrderRevenue() %>';
var ebOrderID = '<%= GetOrderID() %>';
//var ebProductID = '[ProductID]';
//var ebProductInfo = '[ProductInfo]';
var ebRand = Math.random()+'';
ebRand = ebRand * 1000000;
//<![CDATA[
document.write('<scr'+'ipt src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17078&amp;rnd=' + ebRand + '&amp;Value='+ebRev+'&amp;OrderID='+ebOrderID+'"></scr' + 'ipt>');
//]]>
</script>
<noscript>
<img width="1" height="1" style="border:0" src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17078&amp;Value=[Revenue]&amp;OrderID=[OrderID]&amp;ProductID=[ProductID]&amp;ProductInfo=[ProductInfo]"/>
</noscript>

</head>

<body class="section-1 cart_success">
<div id="banner">
<div id="page">

<mpuc:Header id="PageHeader" runat="server" />

<div id="main">
<h2>Create a Plate</h2>

<div class="column-1">
<h3>Congratulations, your order was successful!</h3>
<h3>Your Order ID is <asp:Label ID="OrderID" runat="server" /></h3>
<p>We recommend you print this page for your records. However we have sent an order confirmation to the following email address: <asp:Label ID="CustomerEmail" runat="server"></asp:Label>
Please allow <strong>3-4 weeks</strong> for your plates to be manufactured and shipped to the county of your choice.</p>
<p>We also encourage you to call your local county office to confirm that your plates have been delivered for you to collect them.</p>
<p>All personalized plate selections are subject to additional review and approval by TxDOT. If denied, one of our customer service representatives will contact you and provide you the opportunity to make another selection.</p>
<p><strong>Your plates will be delivered to:</strong></p>
<p><asp:Label ID="CountyAddress" runat="server" /></p>
<p><em>Specialty plate fees will not be refunded once application is submitted.</em></p>
<p><em>Actual plate shown is for illustrative purposes only. The color as presented on your screen may vary to what is actually represented on the manufactured plate.</em></p>

</div><!--/column-1-->

<div class="column-2 box stack">
<h3 class="summary">Order Summary</h3>
<div id="cart" class="container">
<asp:DataList ID="CartList" runat="server" onitemdatabound="CartList_ItemDataBound" RepeatLayout="Flow">
    <ItemTemplate>
        <% =DisplayFirstItemOpeningDiv()%>
        <div class="img">
            <img src='/UIDisplayPlateImage.aspx?PlateID=<%# Eval("ItemID") %>' width="127" height="64" alt='<%# "Plate Combination is " + Eval("MfgText").ToString().Replace("%", "") %>' />
        </div>
        <div class="desc">
        <h4><asp:Label ID="CategoryName2" runat="server" Text='<%# Eval("CategoryName") %>' ></asp:Label></h4>
        <p><asp:Label ID="TypeName" runat="server" Text='<%# Eval("TypeName") %>' /><br />
        <strong><asp:Label ID="CostTotal" runat="server" 
            Text='<%# string.Format("{0:C}", Eval("TotalCost")) %>' /></strong> (<asp:Label ID="RenewalPeriod" runat="server" 
            Text='<%# Eval("RenewalPeriod") %>' />-year plate)</p>
        </div>
        <% =DisplayFirstItemClosingDiv()%>
    </ItemTemplate>
    </asp:DataList>
<p class="big center"><strong>Order Total: <span style="padding-left:5px;"><asp:Label ID="OrderTotal" runat="server"></asp:Label></span></strong></p>
</div><!--/container-->

<h3 class="billing">Billing Information</h3>
<div class="container">
<p><strong><asp:Label ID="CustomerName" runat="server"/></strong><br /><asp:Label ID="CustomerInfo" runat="server"/></p>
</div>
<!--/container-->

<h3 class="payment">Payment Information</h3>
<div class="container last">
<p><strong><asp:Label ID="CardTypeNumber" runat="server"/></strong></p>
</div>
<!--/container-->
</div><!--/column-2-->

</div><!--/main-->

<!--#include virtual="/inc/footer.htm" -->

</div><!--/page-->
</div><!--/banner-->
</body>
</html>