using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CourierServiceManagement
{
    public partial class TrackCourier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }
            protected void btnTrack_Click(object sender, EventArgs e)
            {
                string track = txtTracking.Text.Trim();

                if (track == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "popup",
                    "Swal.fire('Error','Enter Tracking ID','error');", true);
                    return;
                }

                string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT Status FROM Courier WHERE TrackingID=@Track";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Track", track);

                    conn.Open();

                    object status = cmd.ExecuteScalar();

                    if (status != null)
                    {
                        string stat = status.ToString();

                        // popup message
                        ScriptManager.RegisterStartupScript(this, GetType(), "popup",
                        "Swal.fire('Status Found','Current Status: " + stat + "','success');", true);

                        // show progress bar update
                        SetProgress(stat);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "popup",
                        "Swal.fire('Invalid','Tracking ID not found!','error');", true);
                    }
                }
            }

            // ================= PROGRESS TRACK BAR =================
            void SetProgress(string status)
            {
                stepBooked.Attributes["class"] = "step";
                stepShipped.Attributes["class"] = "step";
                stepOut.Attributes["class"] = "step";
                stepDelivered.Attributes["class"] = "step";

                if (status == "Booked")
                {
                    stepBooked.Attributes["class"] += " active";
                }
                else if (status == "Shipped")
                {
                    stepBooked.Attributes["class"] += " done";
                    stepShipped.Attributes["class"] += " active";
                }
                else if (status == "Out for Delivery")
                {
                    stepBooked.Attributes["class"] += " done";
                    stepShipped.Attributes["class"] += " done";
                    stepOut.Attributes["class"] += " active";
                }
                else if (status == "Delivered")
                {
                    stepBooked.Attributes["class"] += " done";
                    stepShipped.Attributes["class"] += " done";
                    stepOut.Attributes["class"] += " done";
                    stepDelivered.Attributes["class"] += " active";
                }
            }
        }
    }