<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="AdminAdd.aspx.cs" Inherits="CourierServiceManagement.AdminAdd" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Admin – Update Courier Status</h2>

    Tracking ID<br />
    <asp:TextBox ID="txtTrackingId" runat="server" /><br /><br />

    Status<br />
    <asp:TextBox ID="txtStatus" runat="server" /><br /><br />

    Location<br />
    <asp:TextBox ID="txtLocation" runat="server" /><br /><br />

    <asp:Button ID="btnUpdate"
    runat="server"
    Text="Update Status"
    OnClick="btnUpdate_Click" />


    <br /><br />

    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />

</asp:Content>
