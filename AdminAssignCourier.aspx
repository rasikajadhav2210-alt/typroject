<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="AdminAssignCourier.aspx.cs" Inherits="CourierServiceManagement.AdminAssignCourier" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Assign Courier to Delivery Boy</h2>

    <!-- Courier ID -->
    <div class="form-group">
        <label>Courier ID:</label><br />
        <asp:TextBox ID="txtCourierID" runat="server" CssClass="input" />
    </div>
    <br />

    <!-- Delivery Boy Dropdown -->
    <div class="form-group">
        <label>Delivery Boy:</label><br />
        <asp:DropDownList ID="ddlDeliveryBoy" runat="server" CssClass="input" />
    </div>
    <br />

    <!-- Assign Button -->
    <div class="form-group">
        <asp:Button ID="btnAssign" runat="server" Text="Assign Courier" CssClass="btn-book" OnClick="btnAssign_Click" />
    </div>
    <br />

    <!-- Message Label -->
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Font-Bold="true" />

</asp:Content>