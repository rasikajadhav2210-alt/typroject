<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeFile="CourierBooking.aspx.cs" Inherits="CourierServiceManagement.CourierBooking" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="form-card">

<h2 class="title">Book Courier</h2>

<asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
<br /><br />

<asp:PlaceHolder ID="phReceipt" runat="server"></asp:PlaceHolder>

<!-- Sender -->
<div class="form-group">
<label>Sender Name</label>
<asp:TextBox ID="txtSender" runat="server" CssClass="input"/>
</div>

<div class="form-group">
<label>Sender Phone</label>
<asp:TextBox ID="txtSenderPhone" runat="server" CssClass="input"/>
</div>

<div class="form-group">
<label>Sender Address</label>
<asp:TextBox ID="txtSenderAddress" runat="server" CssClass="input"/>
</div>

<!-- Receiver -->
<div class="form-group">
<label>Receiver Name</label>
<asp:TextBox ID="txtReceiver" runat="server" CssClass="input"/>
</div>

<div class="form-group">
<label>Receiver Phone</label>
<asp:TextBox ID="txtReceiverPhone" runat="server" CssClass="input"/>
</div>

<div class="form-group">
<label>Receiver Address</label>
<asp:TextBox ID="txtReceiverAddress" runat="server" CssClass="input"/>
</div>

<!-- Location -->
<div class="form-group">
<label>City</label>
<asp:TextBox ID="txtCity" runat="server" CssClass="input"/>
</div>

<div class="form-group">
<label>Pincode</label>
<asp:TextBox ID="txtPincode" runat="server" CssClass="input-small"/>
</div>

<!-- Weight + KM -->
<div class="form-group">
<label>Weight (kg)</label>
<asp:TextBox ID="txtWeight" runat="server" CssClass="input-small" OnKeyUp="calculateTotal()" />
</div>

<div class="form-group">
<label>Distance (KM)</label>
<asp:TextBox ID="txtKM" runat="server" CssClass="input-small" OnKeyUp="calculateTotal()" />
</div>

<!-- Service -->
<div class="form-group">
<label>Service Type</label>
<asp:DropDownList ID="ddlService" runat="server" CssClass="input" onchange="calculateTotal()">
    <asp:ListItem Text="Select Service" Value="" />
    <asp:ListItem Text="Normal Delivery (6-10 days)" Value="Normal" />
    <asp:ListItem Text="Fast Delivery (2-4 days)" Value="Fast" />
</asp:DropDownList>
</div>

<!-- Payment -->
<div class="form-group">
<label>Payment Method</label>

<asp:DropDownList ID="ddlPayment" runat="server" CssClass="input"
    AutoPostBack="true"
    OnSelectedIndexChanged="ddlPayment_SelectedIndexChanged">

    <asp:ListItem Text="Select Payment" Value="" />
    <asp:ListItem Text="Cash On Delivery" Value="COD" />
    <asp:ListItem Text="UPI" Value="UPI" />
</asp:DropDownList>

<!-- COD -->
<asp:Panel ID="pnlCOD" runat="server" Visible="false">
<p style="color:green;font-weight:bold">
Pay cash when courier is delivered.
</p>
</asp:Panel>

<!-- UPI -->
<asp:Panel ID="pnlUPI" runat="server" Visible="false">

<p><b>Pay using UPI</b></p>

<asp:Button ID="btnShowQR" runat="server"
Text="Show Scanner"
CssClass="btn btn-primary"
OnClick="btnShowQR_Click" />

<asp:Panel ID="pnlQR" runat="server" Visible="false">
<br />
<img src="Image/Scanner.jpeg" width="170" />
<p><b>UPI ID:</b> Courier@upi</p>
</asp:Panel>

</asp:Panel>
</div>

<!-- Amount -->
<div class="form-group">
<label>Total Amount</label>
<asp:Label ID="lblAmount" runat="server" Font-Bold="true" ForeColor="Green">₹ 0</asp:Label>
</div>

<!-- Submit -->
<div class="center">
<asp:Button ID="btnBook" runat="server"
Text="Book Courier"
CssClass="btn-book"
OnClick="btnBook_Click"/>
</div>

</div>

<script>
    function calculateTotal() {

        var weight = parseFloat(document.getElementById('<%= txtWeight.ClientID %>').value) || 0;
    var km = parseFloat(document.getElementById('<%= txtKM.ClientID %>').value) || 0;
var service = document.getElementById('<%= ddlService.ClientID %>').value;

var total = (weight * 100) + (km * 3);
if (service === "Fast") total += 200;

document.getElementById('<%= lblAmount.ClientID %>').innerText = "₹ " + total;
    }
</script>

</asp:Content>
