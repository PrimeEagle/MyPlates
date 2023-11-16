<%@ Register TagPrefix="mpuc" TagName="Header" Src="/inc/Header.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Terms.aspx.cs" Inherits="MyPlates.Tx.Web.Terms" %>

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

<body class="terms">
<div id="banner">
<div id="page">
<mpuc:Header id="PageHeader" runat="server" />
<div id="main">

<!--#include virtual="/iframe_termsandconditions.html" -->

</div><!--/column-2-->
</div>
<!--/main-->
<!--#include virtual="/inc/footer.htm" -->
</div><!--/page-->
</div><!--/banner-->
</body>

</html>
