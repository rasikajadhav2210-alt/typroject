using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CourierServiceManagement
{
        public partial class Register : System.Web.UI.Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {
            }

            protected void btnReg_Click(object sender, EventArgs e)
            {
                if (!Page.IsValid) return;

                string cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM [User] WHERE Email=@Email";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        ShowPopup("Error", "Email already registered!", "error");
                        return;
                    }

                    string insertQuery = @"INSERT INTO [User]
                   (Role, Name, Email, Password, Mobile, Gender, City, Pincode)
                   VALUES
                   (@Role,@Name,@Email,@Password,@Mobile,@Gender,@City,@Pincode)";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);

                    cmd.Parameters.AddWithValue("@Role", ddlRole.SelectedValue);
                    cmd.Parameters.AddWithValue("@Name", txtF.Text + " " + txtL.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPass.Text);
                    cmd.Parameters.AddWithValue("@Mobile", txtM.Text);
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@Pincode", txtPin.Text);

                    cmd.ExecuteNonQuery();

                    ShowPopup("Success", "Registration Successful!", "success", "Login.aspx");

                    ClearForm(this);
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

            // ================= CLEAR FORM FUNCTION =================
            void ClearForm(Control parent)
            {
                foreach (Control c in parent.Controls)
                {
                    if (c is TextBox)
                        ((TextBox)c).Text = "";

                    if (c.HasControls())
                        ClearForm(c);
                }
            }
        }
    }
