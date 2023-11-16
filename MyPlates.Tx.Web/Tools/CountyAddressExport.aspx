<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CountyAddressExport.aspx.cs" Inherits="MyPlates.Tx.Web.CSR.CountyAddressExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Export" runat="server" Text="Export to Excel" 
            onclick="Export_Click" />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# Eval("Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="Street1">
                    <ItemTemplate>
                        <%# Eval("MailingAddress.Street1") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="Street2">
                    <ItemTemplate>
                        <%# Eval("MailingAddress.Street2")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="City">
                    <ItemTemplate>
                        <%# Eval("MailingAddress.City")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="State">
                    <ItemTemplate>
                        <%# Eval("MailingAddress.State")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="ZIP">
                    <ItemTemplate>
                        <%# Eval("MailingAddress.ZIP")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="ZIP4">
                    <ItemTemplate>
                        <%# Eval("MailingAddress.ZIP4")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="Phone">
                    <ItemTemplate>
                        <%# MyPlates.Tx.Configuration.PlateConfiguration.FormatPhoneNumber(Eval("Phone").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <%# Eval("Email") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>                                                                                    
            <Columns>
                <asp:TemplateField HeaderText="TACName">
                    <ItemTemplate>
                        <%# Eval("TACName") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>   
        </asp:GridView>
    </div>
    </form>
</body>
</html>
