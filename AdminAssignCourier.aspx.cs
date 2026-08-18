using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace CourierServiceManagement
{
    public partial class AdminAssignCourier : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Admin authentication
            if (Session["UserID"] == null || Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Unauthorized.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindDeliveryBoys();
            }
        }

        // Populate delivery boys dropdown
        void BindDeliveryBoys()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT UserID, Name FROM [User] WHERE Role='DeliveryBoy'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlDeliveryBoy.DataSource = dt;
                ddlDeliveryBoy.DataTextField = "Name";  // Display name
                ddlDeliveryBoy.DataValueField = "UserID"; // Value is ID
                ddlDeliveryBoy.DataBind();

                ddlDeliveryBoy.Items.Insert(0, new ListItem("Select Delivery Boy", "")); // Default
            }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            int courierId;
            int deliveryBoyId;

            if (!int.TryParse(txtCourierID.Text.Trim(), out courierId))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Enter a valid Courier ID!";
                return;
            }

            if (!int.TryParse(ddlDeliveryBoy.SelectedValue, out deliveryBoyId))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Select a Delivery Boy!";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string update = @"UPDATE Courier
                                  SET DeliveryBoyID = @DeliveryBoyID,
                                      Status = 'Out for Delivery'
                                  WHERE CourierID = @CourierID";

                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@DeliveryBoyID", deliveryBoyId);
                cmd.Parameters.AddWithValue("@CourierID", courierId);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Courier assigned successfully!";
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Courier ID not found!";
                }
            }

            // Clear form
            txtCourierID.Text = "";
            ddlDeliveryBoy.SelectedIndex = 0;
        }
    }
}