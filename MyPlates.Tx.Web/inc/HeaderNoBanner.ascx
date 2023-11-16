<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderNoBanner.ascx.cs" Inherits="MyPlates.Tx.Web.inc.HeaderNoBanner" %>

<div id="header_nobanner">
<h1><a href="/">My Plates</a></h1>
<ul id="nav">
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <li id="nav-1"><a href="/CreateAPlate.aspx">Create a plate</a></li>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <li id="nav-1"><a href="/CSR/Default.aspx">Create a plate</a></li>
        </LoggedInTemplate>
    </asp:LoginView>
	<li id="nav-2"><a href="/How.aspx">How it Works</a></li>
	<li id="nav-3"><a href="http://rts.texasonline.state.tx.us/NASApp/txdotrts/SpecialPlateOrderServlet" target="_blank">Charity Plates</a></li>
	<li id="nav-4"><a href="/About.aspx">About Us</a></li>
	<li id="nav-5"><a href="/Contact.aspx">Contact Us</a></li>
</ul>
<p>877-7-MY PLATES (877-769-7528) 8am to 6pm Weekdays</p>
<ul id="topnav">
	<li id="topnav-1"><a href="/Espanol.aspx">Español</a></li>
	<li id="topnav-2"><a href="/Disabled.aspx">Disabled</a></li>
	<li id="topnav-3"><a href="/Cart.aspx">Cart/Checkout</a></li>
</ul>
<a class="txdot" href="http://www.dot.state.tx.us/" title="Texas Department of Transportation" rel="external"><img src="/img/txdot_logo.png" alt="Texas Department of Transportation" /></a>
</div>
<!--/header-->