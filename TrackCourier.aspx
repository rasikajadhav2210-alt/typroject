<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TrackCourier.aspx.cs" Inherits="CourierServiceManagement.TrackCourier" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- REQUIRED FOR POPUPS -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <h2>Track Courier</h2>

    Enter Tracking ID:
    <br /><br />

    <asp:TextBox ID="txtTracking" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Button ID="btnTrack" runat="server" Text="Track Now" OnClick="btnTrack_Click" />
    <br /><br />

    <asp:Label ID="lblResult" runat="server"></asp:Label>

    <!-- ================= TRACK STATUS BAR ADDED ================= -->
    <br /><br />

    <div class="tracker">

        <div id="stepBooked" runat="server" class="step">Booked</div>
        <div id="stepShipped" runat="server" class="step">Shipped</div>
        <div id="stepOut" runat="server" class="step">Out for Delivery</div>
        <div id="stepDelivered" runat="server" class="step">Delivered</div>

    </div>
    <!-- ========================================================== -->

</asp:Content>


   
