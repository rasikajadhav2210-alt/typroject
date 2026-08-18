using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CourierServiceManagement
{
    public partial class DeliveryBoyDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "DeliveryBoy")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadCouriers();
            }
        }

        protected void gvCouriers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MarkDelivered")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string courierId = gvCouriers.DataKeys[index].Value.ToString();

                SqlConnection con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["db"].ConnectionString);

                string query = "UPDATE Courier SET Status='Delivered', DeliveredOn=GETDATE() WHERE CourierID=@id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", courierId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                LoadCouriers();
            }
        }

        private void LoadCouriers()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["db"].ConnectionString);

            string query = "SELECT * FROM Courier WHERE DeliveryBoyID=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Session["UserID"]);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            gvCouriers.DataSource = reader;
            gvCouriers.DataBind();

            con.Close();
        }
    }
}