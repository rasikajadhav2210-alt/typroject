<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DeliveryBoyDashboard.aspx.cs" Inherits="CourierServiceManagement.DeliveryBoyDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Assigned Couriers</h2>

    <asp:GridView ID="gvCouriers" runat="server" AutoGenerateColumns="false" CssClass="table"
        DataKeyNames="CourierID" OnRowCommand="gvCouriers_RowCommand">
        <Columns>
            <asp:BoundField DataField="TrackingID" HeaderText="Tracking ID" />
            <asp:BoundField DataField="ReceiverName" HeaderText="Receiver" />
            <asp:BoundField DataField="City" HeaderText="City" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="DeliveredOn" HeaderText="Delivered On" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
            <asp:ButtonField Text="Mark Delivered" CommandName="MarkDelivered" ButtonType="Button" />
        </Columns>
    </asp:GridView>

</asp:Content>