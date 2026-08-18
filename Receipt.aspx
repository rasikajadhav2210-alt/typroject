<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Receipt.aspx.cs" Inherits="CourierServiceManagement.Receipt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="receipt-card">
    <h2 class="title">Courier Receipt</h2>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

    <asp:Panel ID="pnlReceipt" runat="server" Visible="false">
        <table class="receipt-table">
            <tr>
                <th>Tracking ID</th>
                <td><asp:Label ID="lblTracking" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Sender Name</th>
                <td><asp:Label ID="lblSender" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Sender Address</th>
                <td><asp:Label ID="lblSenderAddress" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Receiver Name</th>
                <td><asp:Label ID="lblReceiver" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Receiver Address</th>
                <td><asp:Label ID="lblReceiverAddress" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>City</th>
                <td><asp:Label ID="lblCity" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Pincode</th>
                <td><asp:Label ID="lblPincode" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Weight (kg)</th>
                <td><asp:Label ID="lblWeight" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Distance (KM)</th>
                <td><asp:Label ID="lblKM" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Service Type</th>
                <td><asp:Label ID="lblService" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Payment Method</th>
                <td><asp:Label ID="lblPayment" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Total Amount</th>
                <td><asp:Label ID="lblTotal" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <th>Status</th>
                <td><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>Booking Date</th>
                <td><asp:Label ID="lblDate" runat="server"></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>
</div>

</asp:Content>
