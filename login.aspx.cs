using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RideNow
{
    public partial class login1 : System.Web.UI.Page
    {
        string connect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        void getcon()
        {
            con = new SqlConnection(connect);
            con.Open();
        }

        void clear()
        {
            txtIdentifier.Text = "";
            txtPassword.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdentifier.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter both email/phone and password.');", true);
                return;
            }

            getcon();

            string query = "select user_id, full_name, role from Users where (email = '" + txtIdentifier.Text.Trim() + "' or phone_number = '" + txtIdentifier.Text.Trim() + "') and password = '" + txtPassword.Text + "'";
            cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Session["UserID"] = Convert.ToInt32(reader["user_id"]);
                string userRole = reader["role"].ToString();
                Session["UserRole"] = userRole;

                
                clear();

                if (userRole == "Driver")
                {
                    Response.Redirect("driver/dashboard.aspx");
                }
                else if(userRole == "Admin")
                {
                    Response.Redirect("admin/index.aspx");
                }
                else
                {
                    Response.Redirect("user-dashboard.aspx");
                }
            }
            else
            {
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid credentials. Please try again.');", true);
            }
        }
    }
}