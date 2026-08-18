<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile ="Login.aspx.cs"  Inherits="CourierServiceManagement.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2 style="margin-left:40px">Login</h2>

<table class="form-table">

<tr>
<td>Email</td>
<td>
    <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email" CssClass="input"/>
    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
        ControlToValidate="txtEmail"
        ErrorMessage="Email is required"
        ForeColor="Red" />
    <asp:RegularExpressionValidator ID="revEmail" runat="server"
        ControlToValidate="txtEmail"
        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
        ErrorMessage="Enter a valid email"
        ForeColor="Red" />
</td>
</tr>

<tr>
<td>Password</td>
<td>
    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Placeholder="Password" CssClass="input"/>
    <asp:RequiredFieldValidator ID="rfvPass" runat="server"
        ControlToValidate="txtPass"
        ErrorMessage="Password is required"
        ForeColor="Red" />
</td>
</tr>

<tr>
<td></td>
<td>
    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click"/>
    <asp:HyperLink ID="lnkForgot" runat="server"
        NavigateUrl="ForgotPassword.aspx"
        Text="Forgot Password?"
        ForeColor="Blue"/>
</td>
</tr>

<tr>
<td colspan="2">
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
</td>
</tr>

</table>

</asp:Content>
