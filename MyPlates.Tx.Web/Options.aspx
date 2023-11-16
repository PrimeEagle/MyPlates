<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Options.aspx.cs" Inherits="MyPlates.Tx.Web.Options" %>

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
document.write('<scr'+'ipt src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17111&amp;rnd=' + ebRand + '"></scr' + 'ipt>');
//]]>
</script>
<noscript>
<img width="1" height="1" style="border:0" src="HTTPS://bs.serving-sys.com/BurstingPipe/ActivityServer.bs?cn=as&amp;ActivityID=17111" />
</noscript>

</head>

<body class="section-2 page-2 options">
       
<form runat="server">
<div id="banner">
<div id="page">
<mpuc:Header id="PageHeader" runat="server" />
<!--#include virtual="/inc/how-secnav.htm" -->
<div id="main">

<h2>Options &amp; Pricing</h2>
<p>Not only are we offering many new ways to express yourself on your bumper, but we’re also offering different pricing choices to suit your needs. You can choose from three different series, each allowing different designs and customization options. And now, new to Texas, you can choose how long you’d like to keep your plate. The longer, the better. If you have further questions, please call us toll free at 877-7MY-PLAT(ES.)</p>
<h4>My Plates are good for Texas</h4>
<p>A portion of each purchase goes into the state’s general revenue fund, which provides services for all Texans. Together we’re working toward building a better Texas.</p>
<h4 class="table">Passenger Vehicle Plates: Plates from less than $40 a year when purchased for a 10-year term.*</h4>
<table cellspacing="0" summary="Passenger vehicle plates options & pricing">
<tr class="thead">
<th scope="col">Series &amp; Length</th>
<th scope="col">1 Year</th>
<th scope="col">5 Year</th>
<th scope="col">10 Year</th>
</tr>
<tr class="first">
<th scope="row" class="row first"><div><strong>Custom Series</strong><br />
AAA ##<br />
Or<br />
## AAA</div></th>
<td>$95<br />
    <asp:LoginView ID="LoginView3" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/plate_custom_1.png" alt="Custom passenger vehicle plate - 1 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/plate_custom_1.png" alt="Custom passenger vehicle plate - 1 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td>$295<br />
    <asp:LoginView ID="LoginView4" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/plate_custom_2.png" alt="Custom passenger vehicle plate - 5 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/plate_custom_2.png" alt="Custom passenger vehicle plate - 5 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td class="last">$395<br />
    <asp:LoginView ID="LoginView5" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/plate_custom_3.png" alt="Custom passenger vehicle plate - 10 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/plate_custom_3.png" alt="Custom passenger vehicle plate - 10 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
</tr>
<tr>
<th scope="row" class="row"><div><strong>Premium</strong><br />
T + AAAAA Or<br />
### AAA or AAA ###<br />
(must include A,E,O or U in the letter set)</div></th>
<td>$195<br />
    <asp:LoginView ID="LoginView6" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/plate_premium_1.png" alt="Premium passenger vehicle plate - 1 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/plate_premium_1.png" alt="Premium passenger vehicle plate - 1 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td>$495<br />
    <asp:LoginView ID="LoginView7" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/plate_premium_2.png" alt="Premium passenger vehicle plate - 5 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/plate_premium_2.png" alt="Premium passenger vehicle plate - 5 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td class="last">$595<br />
    <asp:LoginView ID="LoginView8" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/plate_premium_3.png" alt="Premium passenger vehicle plate - 10 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/plate_premium_3.png" alt="Premium passenger vehicle plate - 10 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
</tr>
<tr>
<th scope="row" class="row"><div><strong>Luxury</strong><br />
Up to any combination of 6 letters and numbers<br />
(must include 1 letter)</div></th>
<td>$395<br />
    <asp:LoginView ID="LoginView9" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/plate_luxury_1.png" alt="Luxury passenger vehicle plate - 1 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/plate_luxury_1.png" alt="Luxury passenger vehicle plate - 1 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td>$695<br />
    <asp:LoginView ID="LoginView10" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/plate_luxury_2.png" alt="Luxury passenger vehicle plate - 5 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/plate_luxury_2.png" alt="Luxury passenger vehicle plate - 5 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td class="last">$795<br />
    <asp:LoginView ID="LoginView11" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/plate_luxury_3.png" alt="Luxury passenger vehicle plate - 10 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/plate_luxury_3.png" alt="Luxury passenger vehicle plate - 10 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
</tr>
</table>
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <asp:ImageButton runat="server" ID="CreateAPlateButton" CssClass="button create" ImageUrl="/img/btn_create.png" PostBackUrl="/CreateAPlate.aspx" />
            <!-- <a href="/CreateAPlate.aspx">Create A Plate</a>    -->
        </AnonymousTemplate>
        <LoggedInTemplate>
            <asp:ImageButton runat="server" ID="CreateAPlateButton" CssClass="button create" ImageUrl="/img/btn_create.png" PostBackUrl="/CSR/Default.aspx" />
            <!-- <a href="/CSR/Default.aspx">Create A Plate</a> -->
        </LoggedInTemplate>
    </asp:LoginView><p class="small">PLEASE NOTE: These plates are available only for passenger vehicles 6,000 pounds or less and trucks one ton or less. The plate images presented here are for illustrative purposes only. Actual manufactured license plates may vary in color and spacing of graphics and letters.</p>
<p class="small">*Pricing does not include vehicle registration fees.</p>
<h4 class="table">Motorcycle Plates</h4>
<table cellspacing="0" summary="Passenger vehicle plates options & pricing" id="mcycle">
<tr class="thead">
<th scope="col">Series &amp; Length</th>
<th scope="col">1 Year</th>
<th scope="col">5 Year</th>
<th scope="col">10 Year</th>
</tr>
<tr class="first">
<th scope="row" class="row first"><div><strong>Custom Series</strong><br />
AAA ##<br />
Or<br />
## AAA</div></th>
<td>$95<br />
    <asp:LoginView ID="LoginView12" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/mplate_custom_1.png" alt="Custom passenger vehicle plate - 1 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/mplate_custom_1.png" alt="Custom passenger vehicle plate - 1 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td>$295<br />
    <asp:LoginView ID="LoginView13" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/mplate_custom_2.png" alt="Custom passenger vehicle plate - 5 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/mplate_custom_2.png" alt="Custom passenger vehicle plate - 5 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td class="last">$395<br />
    <asp:LoginView ID="LoginView14" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/mplate_custom_1.png" alt="Custom passenger vehicle plate - 10 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/mplate_custom_1.png" alt="Custom passenger vehicle plate - 10 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
</tr>
<tr>
<th scope="row" class="row"><div><strong>Premium</strong><br />
T + AAAAA</div></th>
<td>$195<br />
    <asp:LoginView ID="LoginView15" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/mplate_premium_1.png" alt="Premium passenger vehicle plate - 1 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/mplate_premium_1.png" alt="Premium passenger vehicle plate - 1 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td>$495<br />
    <asp:LoginView ID="LoginView16" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/mplate_premium_1.png" alt="Premium passenger vehicle plate - 5 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/mplate_premium_1.png" alt="Premium passenger vehicle plate - 5 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td class="last">$595<br />
    <asp:LoginView ID="LoginView17" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/mplate_premium_1.png" alt="Premium passenger vehicle plate - 10 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/mplate_premium_1.png" alt="Premium passenger vehicle plate - 10 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
</tr>
<tr>
<th scope="row" class="row"><div><strong>Luxury</strong><br />
Up to any combination of 5 letters and numbers<br />
(must include 1 letter)</div></th>
<td>$395<br />
    <asp:LoginView ID="LoginView18" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/mplate_luxury_1.png" alt="Luxury passenger vehicle plate - 1 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/mplate_luxury_1.png" alt="Luxury passenger vehicle plate - 1 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td>$695<br />
    <asp:LoginView ID="LoginView19" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/mplate_luxury_2.png" alt="Luxury passenger vehicle plate - 5 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/mplate_luxury_2.png" alt="Luxury passenger vehicle plate - 5 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
<td class="last">$795<br />
    <asp:LoginView ID="LoginView20" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx"><img src="/img/mplate_luxury_1.png" alt="Luxury passenger vehicle plate - 10 year" /></a>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx"><img src="/img/mplate_luxury_1.png" alt="Luxury passenger vehicle plate - 10 year" /></a>
        </LoggedInTemplate>
    </asp:LoginView>
</td>
</tr>
</table>
    <asp:LoginView ID="LoginView2" runat="server">
        <AnonymousTemplate>
            <asp:ImageButton runat="server" ID="CreateAPlateButton" CssClass="button create" ImageUrl="/img/btn_create.png" PostBackUrl="/CreateAPlate.aspx" />
            <!-- <a href="/CreateAPlate.aspx">Create A Plate</a>    -->
        </AnonymousTemplate>
        <LoggedInTemplate>
            <asp:ImageButton runat="server" ID="CreateAPlateButton" CssClass="button create" ImageUrl="/img/btn_create.png" PostBackUrl="/CSR/Default.aspx" />
            <!-- <a href="/CSR/Default.aspx">Create A Plate</a> -->
        </LoggedInTemplate>
    </asp:LoginView>
<p class="small">PLEASE NOTE: These plates are available only for motorcycles The plate images presented here are for illustrative purposes only. Actual manufactured license plates may vary in color and spacing of graphics and letters.</p>
<p class="small">*Pricing does not include vehicle registration fees.</p>
</div><!--/main-->
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</form>
</body>

</html>
