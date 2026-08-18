using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CourierServiceManagement
{

        public partial class CourierBooking : Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {
                if (Session["UserID"] == null || Session["UserEmail"] == null)
                    Response.Redirect("Login.aspx");

                if (!IsPostBack)
                    lblAmount.Text = "₹ 0";
            }

            protected void btnBook_Click(object sender, EventArgs e)
            {
                lblMessage.Text = "";
                phReceipt.Controls.Clear();

                // Required fields
                if (txtSender.Text == "" || txtSenderAddress.Text == "" ||
                    txtReceiver.Text == "" || txtReceiverAddress.Text == "" ||
                    txtCity.Text == "")
                {
                    ShowPopup("Error", "Please fill all fields", "error");
                    return;
                }

                // Phone validation
                if (!Regex.IsMatch(txtSenderPhone.Text.Trim(), @"^[6-9]\d{9}$"))
                {
                    ShowPopup("Invalid Phone", "Enter valid Sender Phone Number", "error");
                    return;
                }

                if (!Regex.IsMatch(txtReceiverPhone.Text.Trim(), @"^[6-9]\d{9}$"))
                {
                    ShowPopup("Invalid Phone", "Enter valid Receiver Phone Number", "error");
                    return;
                }

                // Pin
                if (!int.TryParse(txtPincode.Text.Trim(), out int pin))
                {
                    ShowPopup("Error", "Invalid Pincode", "error");
                    return;
                }

                // Weight
                if (!double.TryParse(txtWeight.Text.Trim(), out double weight) || weight <= 0)
                {
                    ShowPopup("Error", "Enter valid weight", "error");
                    return;
                }

                // KM
                if (!double.TryParse(txtKM.Text.Trim(), out double km) || km <= 0)
                {
                    ShowPopup("Error", "Enter valid distance", "error");
                    return;
                }

                string service = ddlService.SelectedValue;
                if (service == "")
                {
                    ShowPopup("Error", "Select Service Type", "error");
                    return;
                }

                string payment = ddlPayment.SelectedValue;
                if (payment == "")
                {
                    ShowPopup("Payment Required", "Please select payment method", "warning");
                    return;
                }

                double total = (weight * 100) + (km * 3);
                if (service == "Fast") total += 200;

                string tracking = "TRK" + DateTime.Now.Ticks.ToString().Substring(10);
                string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();

                        string query = @"INSERT INTO Courier
                     (UserID,TrackingID,SenderName,SenderPhone,SenderAddress,
                     ReceiverName,ReceiverPhone,ReceiverAddress,
                     City,Pincode,Weight,DistanceKM,ServiceType,PaymentMethod,TotalAmount,Status,BookingDate)
                     VALUES
                     (@UserID,@Track,@Sender,@SenderPhone,@SenderAddress,
                     @Receiver,@ReceiverPhone,@ReceiverAddress,
                     @City,@Pincode,@Weight,@KM,@Service,@Payment,@Total,'Booked',GETDATE())";

                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserID"]));
                        cmd.Parameters.AddWithValue("@Track", tracking);
                        cmd.Parameters.AddWithValue("@Sender", txtSender.Text.Trim());
                        cmd.Parameters.AddWithValue("@SenderPhone", txtSenderPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@SenderAddress", txtSenderAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Receiver", txtReceiver.Text.Trim());
                        cmd.Parameters.AddWithValue("@ReceiverPhone", txtReceiverPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@ReceiverAddress", txtReceiverAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@City", txtCity.Text.Trim());
                        cmd.Parameters.AddWithValue("@Pincode", pin);
                        cmd.Parameters.AddWithValue("@Weight", weight);
                        cmd.Parameters.AddWithValue("@KM", km);
                        cmd.Parameters.AddWithValue("@Service", service);
                        cmd.Parameters.AddWithValue("@Payment", payment);
                        cmd.Parameters.AddWithValue("@Total", total);

                        cmd.ExecuteNonQuery();
                    }

                    lblMessage.Text = "Courier booked successfully! Tracking ID: " + tracking;
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                    HyperLink view = new HyperLink();
                    view.Text = "View Receipt";
                    view.NavigateUrl = "Receipt.aspx?track=" + tracking;
                    view.CssClass = "btn btn-success";
                    view.Target = "_blank";

                    HyperLink download = new HyperLink();
                    download.Text = "Download Receipt";
                    download.NavigateUrl = "Receipt.aspx?track=" + tracking + "&download=1";
                    download.CssClass = "btn btn-info";
                    download.Target = "_blank";

                    phReceipt.Controls.Add(view);
                    phReceipt.Controls.Add(new Literal { Text = " " });
                    phReceipt.Controls.Add(download);

                    ClearForm();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }

            protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
            {
                pnlCOD.Visible = false;
                pnlUPI.Visible = false;

                if (ddlPayment.SelectedValue == "COD")
                    pnlCOD.Visible = true;
                else if (ddlPayment.SelectedValue == "UPI")
                    pnlUPI.Visible = true;
            }

            protected void btnShowQR_Click(object sender, EventArgs e)
            {
                pnlQR.Visible = true;
            }

            void ShowPopup(string title, string msg, string type)
            {
                string script = $"Swal.fire('{title}','{msg}','{type}');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", script, true);
            }

            void ClearForm()
            {
                txtSender.Text = "";
                txtSenderPhone.Text = "";
                txtSenderAddress.Text = "";
                txtReceiver.Text = "";
                txtReceiverPhone.Text = "";
                txtReceiverAddress.Text = "";
                txtCity.Text = "";
                txtPincode.Text = "";
                txtWeight.Text = "";
                txtKM.Text = "";
                ddlService.SelectedIndex = 0;
                ddlPayment.SelectedIndex = 0;
                pnlCOD.Visible = false;
                pnlUPI.Visible = false;
                pnlQR.Visible = false;
                lblAmount.Text = "₹ 0";
            }
        }
    }