<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUISendPayment.aspx.cs" Inherits="MyPlates.Tx.Web.TestUISendPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <asp:Label id="TitleLabel" runat="server" Font-Bold="True" Font-Size="X-Large">Send Payment</asp:Label>
    <form id="form1" runat="server">
    <div>
        CREDIT CARD INFO<br /><br />
        <asp:Label ID="Label2" runat="server" Text="Amount: $" />
        <asp:TextBox ID="AmountBox" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label18" runat="server" Text="Card Type: " />
        <asp:DropDownList ID="CCTypeDropdown" runat="server" AppendDataBoundItems="true">
            <asp:ListItem Text="Select a card" Value=""></asp:ListItem>
        </asp:DropDownList>
        <br />    
        <asp:Label ID="Label10" runat="server" Text="CC Name: " />
        <asp:TextBox ID="CCNameBox" runat="server"></asp:TextBox>
        <br />    
        <asp:Label ID="Label16" runat="server" Text="CC Number: " />
        <asp:TextBox ID="CCNumberBox" runat="server"></asp:TextBox>
        <br />            
        <asp:Label ID="Label17" runat="server" Text="CVV: " />
        <asp:TextBox ID="CVVBox" runat="server"></asp:TextBox>
        <br />                    
        <asp:Label ID="Label12" runat="server" Text="CC Exp Month: " />
        <asp:DropDownList ID="CCExpMonthDropdown" runat="server" AppendDataBoundItems="true">
            <asp:ListItem Text="Month" Value=""></asp:ListItem>
        </asp:DropDownList>
        <br />            
        <asp:Label ID="Label15" runat="server" Text="CC Exp Year: " />
        <asp:DropDownList ID="CCExpYearDropdown" runat="server" AppendDataBoundItems="true">
            <asp:ListItem Text="Year" Value=""></asp:ListItem>
        </asp:DropDownList>
        <br />   
        <br /><br /> 
        ePAY INFO<br /><br />
        <asp:Label ID="Label19" runat="server" Text="Plate Text: " />        
        <asp:TextBox ID="PlateTextBox" runat="server"></asp:TextBox> (only used in generating Trace Number)
        <br />
        <asp:Label ID="Label3" runat="server" Text="Trace Number: " />        
        <asp:TextBox ID="TraceNumberBox" runat="server"></asp:TextBox> &nbsp; 
        <asp:Button ID="GenerateTraceNumber" runat="server" 
            Text="Generate Trace Number" onclick="GenerateTraceNumber_Click" />
        <br />    
        <asp:Label ID="Label9" runat="server" Text="Unique Transaction ID: " />        
        <asp:TextBox ID="UniqueTransactionIDBox" runat="server" Width="250px"></asp:TextBox> &nbsp; 
        <asp:Button ID="GenerateUniqueTransactionId" runat="server" 
            Text="Generate Unique Transaction ID" 
            onclick="GenerateUniqueTransactionId_Click" />
        <br />      
        <br /><br />  
        BILLING INFO
        
        <br />
        <br />

        <asp:Label ID="Label4" runat="server" Text="First Name:" />        
        <asp:TextBox ID="FirstNameBox" runat="server" Width="241px"></asp:TextBox>

        <br />
        <br />


        <asp:Label ID="Label5" runat="server" Text="Last Name:" />        
        <asp:TextBox ID="LastNameBox" runat="server" Width="243px"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label6" runat="server" Text="Street 1:" />        
        <asp:TextBox ID="Street1Box" runat="server" Width="261px"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label7" runat="server" Text="Street 2:" />        
        <asp:TextBox ID="Street2Box" runat="server" Width="261px"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label8" runat="server" Text="City:" />        
        <asp:TextBox ID="CityBox" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Label ID="Label1" runat="server" Text="State:" />        
        <asp:TextBox ID="StateBox" runat="server" MaxLength="2"></asp:TextBox>
        
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
        <asp:TextBox ID="EmailBox" runat="server" Width="261px"></asp:TextBox>

        <br />
        <br />                
                
        <br /> 
        <asp:Button ID="bPay" runat="server" Text="Send Payment" onclick="bPay_Click" />
        <asp:Label ID="PaymentLabel" runat="server" Font-Bold="True" ForeColor="#CC0000" />  

        <br />
        <br />      
    </div>
    </form>
</body>
</html>
