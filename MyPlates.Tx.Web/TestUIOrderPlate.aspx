<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUIOrderPlate.aspx.cs" Inherits="MyPlates.Tx.Web.TestUIOrderPlate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>MyPlates ISA Order</title>
</head>
<body>
    <asp:Label id="TitleLabel" runat="server" Font-Bold="True" Font-Size="X-Large">Order Plate</asp:Label>
    <form id="form1" runat="server">
    <br /><br />
    <div style="height: 800px">
        <asp:Label ID="LabelCategory" runat="server" Text="Category:" />
        <asp:DropDownList ID="CategoriesDropdown" runat="server" 
            onselectedindexchanged="CategoriesDropdown_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>

        <br />
        <br />
        
        <asp:Label ID="Label1" runat="server" Text="Plate:" />
        <asp:DropDownList ID="PlateCodesDropdown" runat="server" AutoPostBack="True">
        </asp:DropDownList>

        <br />
        <br />
        
        <asp:Label ID="Label2" runat="server" Text="Plate Text:" />        
        <asp:TextBox ID="PlateText" runat="server"></asp:TextBox>
        
        <br />
        <br />
        <asp:Label ID="Label15" runat="server" Text="Trace Number:" />
        <asp:TextBox ID="TraceNumber" runat="server"></asp:TextBox>
        <asp:Button ID="GenerateTraceNumber" runat="server" Text="Generate Trace Number" onclick="GenerateTraceNumber_Click" />

        <br />
        <br />

        <asp:Label ID="Label3" runat="server" Text="Renewal Period" />        
        <asp:DropDownList ID="RenewalDropdown" runat="server">
        </asp:DropDownList>

        <br />
        <br />
        
        <asp:Button ID="CheckAvailabilityButton" runat="server" Text="Check Availability" 
            onclick="CheckAvailabilityButton_Click" />
        <asp:Label ID="ResponseLabel" runat="server" Font-Bold="True" 
            ForeColor="#CC0000"></asp:Label>
        <br />
        <br />
        
        OWNER INFO
        
        <br />
        <br />

        <asp:Label ID="Label4" runat="server" Text="First Name:" />        
        <asp:TextBox ID="FirstNameBox" runat="server"></asp:TextBox>

        <br />
        <br />


        <asp:Label ID="Label5" runat="server" Text="Last Name:" />        
        <asp:TextBox ID="LastNameBox" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label6" runat="server" Text="Street 1:" />        
        <asp:TextBox ID="Street1Box" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label7" runat="server" Text="Street 2:" />        
        <asp:TextBox ID="Street2Box" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label8" runat="server" Text="City:" />        
        <asp:TextBox ID="CityBox" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label9" runat="server" Text="County:" />        
        <asp:DropDownList ID="CountyDropdown" runat="server">
        </asp:DropDownList>        

        <br />
        <br />

        <asp:Label ID="Label10" runat="server" Text="State:" />        
        <asp:Label ID="Label12" runat="server" Text="TX" />        

        <br />
        <br />

        <asp:Label ID="Label11" runat="server" Text="ZIP:" />        
        <asp:TextBox ID="ZIPBox" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label13" runat="server" Text="Phone:" />        
        <asp:TextBox ID="PhoneBox" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label14" runat="server" Text="Email:" />        
        <asp:TextBox ID="EmailBox" runat="server"></asp:TextBox>

        <br />
        <br />                
                
        <br /> 
        <asp:Button ID="bOrder" runat="server" Text="Order Plate" onclick="bOrder_Click" />
        <asp:Label ID="OrderLabel" runat="server" Font-Bold="True" ForeColor="#CC0000" />  

        <br />
        <br />                

    </div>
    </form>
   
</body>
</html>
