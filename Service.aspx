<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile= "~/Site.Master" CodeBehind="~/Service.aspx.cs" Inherits="CourierServiceManagement.Service" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h1 class="title">Our Courier Services</h1>

<div class="service-wrapper">

<!-- NORMAL SERVICE -->

<div class="service-card">
<h2>Normal Delivery</h2>

<p class="time">Delivery Time: 6 – 10 Days</p>

<ul>
<li>Weight Charge = ₹100 per KG</li>
<li>Distance Charge = ₹3 per KM</li>
<li>Safe Handling</li>
<li>Budget Friendly</li>
</ul>

<div class="price-box">
Total Cost = (Weight × 100) + (Distance × 3)
</div>

</div>

<!-- FAST SERVICE -->

<div class="service-card fast">

<h2>Fast Delivery</h2>

<p class="time">Delivery Time: 2 – 4 Days</p>

<ul>
<li>Weight Charge = ₹100 per KG</li>
<li>Distance Charge = ₹3 per KM</li>
<li>Express Priority</li>
<li>Instant Dispatch</li>
</ul>

<div class="price-box">
Total Cost = (Weight × 100) + (Distance × 3) + ₹200
</div>

</div>

</div>

<!-- INFO SECTION -->

<div class="info">

<h2>How Charges Are Calculated?</h2>

<p>
Our pricing system is simple and transparent.
Charges are calculated based on parcel weight and delivery distance.
Fast delivery includes an additional express service charge.
</p>

<table class="price-table">
<tr>
<th>Type</th>
<th>Rate</th>
</tr>

<tr>
<td>Weight Charge</td>
<td>₹100 per KG</td>
</tr>

<tr>
<td>Distance Charge</td>
<td>₹3 per KM</td>
</tr>

<tr>
<td>Fast Service Charge</td>
<td>₹200 Extra</td>
</tr>

</table>

</div>

</asp:Content>
