using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CourierServiceManagement
{
    public partial class AdminAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Block if not logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            // Block if not Admin
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Unauthorized.aspx"); // or Home.aspx
                return;
            }

            // Prevent Back Button Access After Logout
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.MinValue);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string tracking = txtTrackingId.Text.Trim();
            string status = txtStatus.Text.Trim();
            string location = txtLocation.Text.Trim();

            if (string.IsNullOrEmpty(tracking) || string.IsNullOrEmpty(status) || string.IsNullOrEmpty(location))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please fill all fields!";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 1️⃣ Insert into TrackingHistory
                string insertQuery = @"INSERT INTO TrackingHistory (TrackingID, Location, Status)
                                       VALUES (@TrackingID, @Location, @Status)";
                SqlCommand cmdInsert = new SqlCommand(insertQuery, conn);
                cmdInsert.Parameters.AddWithValue("@TrackingID", tracking);
                cmdInsert.Parameters.AddWithValue("@Location", location);
                cmdInsert.Parameters.AddWithValue("@Status", status);
                cmdInsert.ExecuteNonQuery();

                // 2️⃣ Update Courier table's current Status and DeliveredOn if delivered
                string updateCourier = @"UPDATE Courier 
                                         SET Status = @Status, 
                                             DeliveredOn = CASE WHEN @Status='Delivered' THEN GETDATE() ELSE DeliveredOn END
                                         WHERE TrackingID = @TrackingID";
                SqlCommand cmdUpdate = new SqlCommand(updateCourier, conn);
                cmdUpdate.Parameters.AddWithValue("@TrackingID", tracking);
                cmdUpdate.Parameters.AddWithValue("@Status", status);
                cmdUpdate.ExecuteNonQuery();

                // 3️⃣ Send email notification to user
                try
                {
                    string getEmailQuery = "SELECT U.Email FROM Courier C JOIN [User] U ON C.UserID = U.UserID WHERE C.TrackingID = @TrackingID";
                    SqlCommand cmdEmail = new SqlCommand(getEmailQuery, conn);
                    cmdEmail.Parameters.AddWithValue("@TrackingID", tracking);

                    object result = cmdEmail.ExecuteScalar();
                    if (result != null)
                    {
                        string userEmail = result.ToString();

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("yourgmail@gmail.com"); // Replace with your Gmail
                        mail.To.Add(userEmail);
                        mail.Subject = $"Courier Status Update - {tracking}";
                        mail.Body = $"Hello,\n\nYour courier with Tracking ID: {tracking} has been updated.\n" +
                                    $"Current Status: {status}\n" +
                                    $"Location: {location}\n" +
                                    (status == "Delivered" ? $"\nDelivered On: {DateTime.Now}" : "") +
                                    "\n\nThank you for using our service.";

                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Port = 587;
                        smtp.Credentials = new System.Net.NetworkCredential("yourgmail@gmail.com", "your_app_password"); // Use App Password
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text += "<br/>Warning: Unable to send email. " + ex.Message;
                }
            }

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text += "<br/>Tracking updated successfully!";

            // Clear fields
            txtTrackingId.Text = "";
            txtStatus.Text = "";
            txtLocation.Text = "";
        }
    }
}