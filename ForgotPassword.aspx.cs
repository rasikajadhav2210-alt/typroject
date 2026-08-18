using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace CourierServiceManagement
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Nothing on load
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                ShowMsg("Enter your email first", Color.Red);
                return;
            }

            string token = Guid.NewGuid().ToString(); // Unique reset token

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                // Check if email exists
                string check = "SELECT COUNT(*) FROM [User] WHERE Email=@Email";
                SqlCommand cmd = new SqlCommand(check, con);
                cmd.Parameters.AddWithValue("@Email", email);
                int exists = (int)cmd.ExecuteScalar();

                if (exists == 0)
                {
                    ShowMsg("Email not registered", Color.Red);
                    return;
                }

                // Update ResetToken and TokenExpiry
                string update = @"UPDATE [User]
                                  SET ResetToken=@Token,
                                      TokenExpiry=DATEADD(MINUTE,15,GETDATE())
                                  WHERE Email=@Email";

                SqlCommand cmd2 = new SqlCommand(update, con);
                cmd2.Parameters.AddWithValue("@Token", token);
                cmd2.Parameters.AddWithValue("@Email", email);
                cmd2.ExecuteNonQuery();
            }

            // Generate reset link
            string link = Request.Url.GetLeftPart(UriPartial.Authority)
                        + "/ResetPassword.aspx?token=" + token;

            // Display link on page for demo
            lblMsg.Text = "Reset Link:<br/><a href='" + link + "'>" + link + "</a>";
            lblMsg.ForeColor = Color.Green;
        }

        void ShowMsg(string msg, Color color)
        {
            lblMsg.Text = msg;
            lblMsg.ForeColor = color;
        }
    }
}