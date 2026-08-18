<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="ForgotPassword.aspx.cs" Inherits="CourierServiceManagement.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 
<h2>Forgot Password</h2>

Enter Email:<br />
<asp:TextBox ID="txtEmail" runat="server" Width="250" /><br /><br />

<asp:Button ID="btnReset" runat="server" Text="Send Reset Link"
    CssClass="btn btn-primary"
    OnClick="btnReset_Click" />

<br /><br />

<asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label>

</asp:Content>
