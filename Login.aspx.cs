using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;

namespace CourierServiceManagement
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string email = txtEmail.Text.Trim();
            string password = txtPass.Text.Trim();

            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT UserID, Role FROM [User] WHERE Email=@Email AND Password=@Password";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Session["UserID"] = dr["UserID"].ToString();
                    Session["UserEmail"] = email;
                    Session["Role"] = dr["Role"].ToString();

                    string role = dr["Role"].ToString().Trim();

                    if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        ShowPopup("Login Success", "Welcome Admin!", "success", "AdminAdd.aspx");
                    }
                    else if (role.Equals("DeliveryBoy", StringComparison.OrdinalIgnoreCase))
                    {
                        ShowPopup("Login Success", "Welcome Delivery Boy!", "success", "DeliveryBoyDashboard.aspx");
                    }
                    else
                    {
                        ShowPopup("Login Success", "Welcome User!", "success", "CourierBooking.aspx");
                    }
                }
            }

            // ================= POPUP FUNCTION =================
            void ShowPopup(string title, string message, string icon, string redirectUrl = "")
            {
                string script;

                if (redirectUrl == "")
                {
                    script = $"Swal.fire('{title}','{message}','{icon}')";
                }
                else
                {
                    script = $"Swal.fire('{title}','{message}','{icon}').then(()=>{{window.location='{redirectUrl}';}})";
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", script, true);
            }
        }
    }
}
