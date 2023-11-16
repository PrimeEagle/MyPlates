<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="MyPlates.Tx.Web.Cart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>MyPlates Shopping Cart</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="description" content="" />
<meta name="keywords" content="" />
<link type="text/css" rel="stylesheet" href="/css/styles.css" />
<script type="text/javascript" src="/js/jquery-1.2.3.pack.js"></script>
<script type="text/javascript" src="/js/jquery.simplemodal.js"></script>
<script type="text/javascript" src="/js/scripts.js"></script>
<!-- <script type="text/javascript" src="/js/swfobject.js"></script> -->
<script type="text/javascript" src="/js/renewholdforplates.js"></script>
<!-- <% =CreateFlashPlateViewScript() %> -->
<!--[if IE 7]><link href="/css/ie7.css" rel="stylesheet" type="text/css" /><![endif]-->
<!--[if lte IE 6]><link href="/css/ie6.css" rel="stylesheet" type="text/css" /><![endif]-->

</head>

<body class="section-1 cart" onclick="BodyClicked();">
<form id="form1" runat="server" submitdisabledcontrols="true">
<div id="banner">
<div id="page">

<mpuc:Header id="PageHeader" runat="server" />

<div id="main">
<h2>Create a Plate</h2>
<h2 id="progress_checkout">Check Out</h2>
<p style="margin-bottom:15px;">Now that you&#39;ve finished creating your plate you can 
    simply fill out the checkout form below to complete your purchase, or you can 
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <a href="/CreateAPlate.aspx">continue shopping</a>    
        </AnonymousTemplate>
        <LoggedInTemplate>
            <a href="/CSR/Default.aspx">continue shopping</a>
        </LoggedInTemplate>
    </asp:LoginView>    
    .</p>
		
<div id="cart" class="column-1 box">
<h3>Your Cart</h3>

<div class="container">

<asp:DataList ID="CartList" runat="server" onitemcommand="CartList_ItemCommand" 
        onitemdatabound="CartList_ItemDataBound" RepeatLayout="Flow">
    <ItemTemplate>
        <% =DisplayFirstItemOpeningDiv()%>
        <div class="img">
            <img src='/UIDisplayPlateImage.aspx?PlateID=<%# Eval("ItemID") %>' width="<%# Eval("ImageSizeX") %>" height="<%# Eval("ImageSizeY") %>" alt="Plate Combination is: <%# Eval("MfgText").ToString().Replace("%", "") %>" />
        </div>
        <div class="desc">
        <h4><asp:Label ID="CategoryName2" runat="server" Text='<%# Eval("CategoryName") %>' /></h4>
        <p><asp:Label ID="TypeName" runat="server" Text='<%# Eval("TypeName") %>' /><br />
        <strong><asp:Label ID="CostTotal" runat="server" 
            Text='<%# string.Format("{0:C}", Eval("TotalCost")) %>' /></strong> (<asp:Label ID="RenewalPeriod" runat="server" 
            Text='<%# Eval("RenewalPeriod") %>' />-year plate)</p>
        <ul class="inline">
	        <li><asp:LinkButton ID="RemoveFromCart" runat="server" CommandName="delete" 
                    CommandArgument='<%# Eval("ItemID") %>'>Remove from cart</asp:LinkButton></li>
        </ul>
        </div>
        <asp:Label ID="Disclaimer" runat="server" Text='<%# GetNaturalTexasDisclaimer(Eval("PlateCode").ToString()) %>' />
        <% =DisplayFirstItemClosingDiv()%>
    </ItemTemplate>
    </asp:DataList>
<div class="divider center">
<p class="big"><strong>Order Total: <span class="big" style="padding-left:5px;">
    <asp:Label ID="OrderTotal" runat="server"></asp:Label></span></strong></p>
    <asp:LoginView ID="LoginView2" runat="server">
        <AnonymousTemplate>
            <asp:HyperLink ID="ContinueShoppingButton" NavigateUrl="/CreateAPlate.aspx" runat="server" CssClass="button continue_shop" style="margin:0 auto;">Continue Shopping</asp:HyperLink>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <asp:HyperLink ID="ContinueShoppingButton" NavigateUrl="/CSR/Default.aspx" runat="server" CssClass="button continue_shop" style="margin:0 auto;">Continue Shopping</asp:HyperLink>
        </LoggedInTemplate>
    </asp:LoginView>      
    
</div>
</div><!--/container-->
</div><!--/column-1-->

<div id="checkout" class="column-2 box stack">
<h3 id="checkout-1">Billing Information</h3>
<div class="container">
<p>Enter your billing information or choose from an address already provided:</p>

<asp:DataList ID="OwnerList" runat="server" onitemdatabound="OwnerList_ItemDataBound" 
                            RepeatLayout="Flow">
    <ItemTemplate>
        <p class="prev_address"><strong><namefirst><%# Eval("OwnerInfo.NameFirst") %></namefirst>&nbsp;<namelast><%# Eval("OwnerInfo.NameLast") %></namelast></strong><br />
            <street1><%# Eval("OwnerInfo.Street1") %></street1>, <street2><%# Eval("OwnerInfo.Street2") %></street2><br />
            <city><%# Eval("OwnerInfo.City") %></city>, <state><%# Eval("OwnerInfo.State") %></state> <zip><%# MyPlates.Tx.Configuration.PlateConfiguration.FormatZIPCode(Eval("OwnerInfo.ZIP").ToString(), Eval("OwnerInfo.ZIP4").ToString()) %></zip><br />
            <phone><%# MyPlates.Tx.Configuration.PlateConfiguration.FormatPhoneNumber(Eval("OwnerInfo.Phone").ToString()) %></phone><br />
            <email><%# Eval("OwnerInfo.Email") %></email></p>
    </ItemTemplate>
</asp:DataList>    

<br class="clear" />

<div id="checkout-1a">
<label for="first_name">First Name<span class="required">*</span></label>
  <asp:TextBox id="first_name" name="first_name" runat="server" CssClass="text required" MaxLength="30" CausesValidation="True" />
</div>
<div id="checkout-1b">
  <label for="last_name">Last Name<span class="required">*</span></label>
<asp:TextBox id="last_name" name="last_name" runat="server" CssClass="text required" MaxLength="30" CausesValidation="True" />
</div>
<div id="checkout-1c">
  <label for="address_1">Street Line 1<span class="required">*</span></label>
<asp:TextBox id="address_1" name="address_1" runat="server" CssClass="text required" MaxLength="30" CausesValidation="True" />
</div>
<div id="checkout-1d">
<label for="address_2">Street Line 2</label>
<asp:TextBox id="address_2" name="address_2" runat="server" CssClass="text" MaxLength="30" CausesValidation="True" />
</div>
<div id="checkout-1e">
  <label for="city">City<span class="required">*</span></label>
<asp:TextBox id="city" name="city" runat="server" CssClass="text required" MaxLength="19" CausesValidation="True" />
</div>
<div id="checkout-1f">
  <label for="state">State<span class="required">*</span></label>
<asp:DropDownList ID="state" runat="server" AppendDataBoundItems="true" CssClass="required" CausesValidation="True">
    <asp:ListItem Text="(Select a State)" Value=""></asp:ListItem>
</asp:DropDownList>
</div>
<div id="checkout-1g">
  <label for="zip">Zip Code<span class="required">*</span></label>
<asp:TextBox id="zip" name="zip" runat="server" CssClass="text required" MaxLength="10" CausesValidation="True" />
</div>
<div id="checkout-1h">
<label for="phone">Phone<span class="required">*</span></label>
<asp:TextBox id="phone" name="phone" runat="server" CssClass="text required" MaxLength="15" CausesValidation="True" />
</div>
<div id="checkout-1i">
<label for="email">Email<span class="required">*</span></label>
<asp:TextBox id="email" name="email" runat="server" CssClass="text required" MaxLength="50" CausesValidation="True" />
</div>
<p><span class="required">*</span> Denotes required fields</p>
</div><!--/container-->


<h3 id="checkout-2">Payment Information</h3>
<div class="container">
<p>Please enter your payment information below.</p>
<h5>Credit Card Information:</h5>
<div id="checkout-2a">
<label for="card_num">Card Number<span class="required">*</span></label>
<asp:TextBox id="card_num" name="card_num" runat="server" CssClass="text required" CausesValidation="True" />
</div>
<div id="checkout-2c">
<label for="card_type">Card Type<span class="required">*</span></label>
<asp:DropDownList ID="card_type" runat="server" AppendDataBoundItems="true" CssClass="required" CausesValidation="True">
    <asp:ListItem Text="Select a card" Value=""></asp:ListItem>
</asp:DropDownList>
</div>
<div id="checkout-2d">
<label for="card_name">Name as it appears on card<span class="required">*</span></label>
<asp:TextBox id="card_name" name="card_name" runat="server" CssClass="text required" CausesValidation="True" />
</div>
<div id="checkout-2e">
<label for="card_exp_month">Expiration Date<span class="required">*</span></label>
<asp:DropDownList ID="card_exp_month" runat="server" CssClass="required" AppendDataBoundItems="true" CausesValidation="True">
    <asp:ListItem Text="Month" Value=""></asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="card_exp_year" runat="server" CssClass="required" AppendDataBoundItems="true" CausesValidation="True">
    <asp:ListItem Text="Year" Value=""></asp:ListItem>
</asp:DropDownList>
</div>
<div id="checkout-2f">
<label for="card_code">Security Code<span class="required">*</span></label>
<asp:TextBox id="card_code" name="card_code" runat="server" CssClass="text required" CausesValidation="True" />

<a href="#cvv" rel="pop">What is this?</a></div>
<p style="margin-bottom:5px;"><a href="#safeguard" rel="pop">View Credit Card Safeguard Information</a></p>
<p><span class="required">*</span> Denotes required fields</p>
</div><!--/container-->

<h3 id="checkout-3">Order Confirmation</h3>
<div class="container last">
<div id="checkout-3a">
<p></p><iframe src="iframe_termsandconditions.html" width="380" height="104" frameborder="1" style="border: #cccccc 1px solid"></iframe></p>
<asp:CheckBox ID="order_confirm" Checked="false" runat="server" CausesValidation="True" />
<label for="order_confirm">I have read and agree to the <a href="/Terms.aspx" target="_blank">terms and conditions</a> of this transaction. <strong>I understand that specialty plate fees will not be refunded once this order is submitted.</strong></label>
</div><br /><br />
<div id="checkout-3b">
<p><strong>Verify your order information above then click:</strong></p>
<asp:ImageButton name="PlaceOrder" CssClass="place_order" runat="server" 
        Text="Place Order" ID="PlaceOrder" onclick="PlaceOrder_Click" ImageUrl="/img/btn_place_order.png" Width="108px" />
</div>
</div><!--/container-->
<asp:Label ID="ResponseLabel" runat="server" Font-Bold="False" ForeColor="#CC0000" Font-Size="Large" />
</div><!--/column-2-->

<div id="cvv" class="box pop">
<h3>Security Code</h3>
<div class="container">
<p>The CVV Security Code is either a 3-Digit number located on the back of your card or a 4-Digit number located on the front of your card. See card images below for reference:</p>
<img src="/img/cvv_visa.png" alt="CVV Security Code location on back of Visa, MasterCard and Discover" />
<img src="/img/cvv_ae.png" alt="CVV Security Code location on back of American Express" />
</div><!--/container-->
</div><!--/cvv-->
<div id="safeguard" class="box pop">
<h3>Credit Card Safequard Information</h3>
<div class="container">
<p>All transactions through the My Plates website are protected with an extremely high level of encryption. All purchase pages are secure and contain a padlock symbol that can be clicked upon to verify the security of the page.</p>
<p>All sensitive information, including personal details as well as credit card information, is kept confidential through the use of our secure server software (SSL.) The padlock symbol shows which pages are covered by this security system, thus ensuring all information exchanged between you and My Plates will be secure during transmission.</p>
<p>If for any reason you cannot access the secure server or cannot find the padlock symbol, please feel free to place your order with us by telephone by calling toll free 877-7MY-PLAT(ES) between 8 am and 6 pm CST on weekdays.</p>
</div><!--/container-->
</div><!--/safeguard-->

</div><!--/main-->

<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</form>
</body>

</html>
