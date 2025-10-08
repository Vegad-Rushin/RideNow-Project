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
    public partial class editProfile1 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // 1. Check if user is logged in
                if (Session["UserID"] == null)
                {
                    Response.Redirect("login.aspx");
                    return;
                }

                // 2. Get the updated values from the form inputs
                string fullName = Request.Form["firstName"]; // The input is "firstName" but maps to "full_name"
                string email = Request.Form["email"];
                string phone = Request.Form["phone"];
                string userId = Session["UserID"].ToString();

                // 3. Create the SQL UPDATE query string (insecure method)
                string query = "UPDATE Users SET full_name = '" + fullName + "', email = '" + email + "', phone_number = '" + phone + "' WHERE user_id = " + userId;

                // 4. Connect to the database and execute the query
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // This runs the UPDATE command
                con.Close();

                // 5. Show a success or warning message to the user
                if (rowsAffected > 0)
                {
                    // Success!
                    string script = "showMessage('Profile updated successfully!', 'success');";
                    ltMessage.Text = "<script>" + script + "</script>";
                }
                else
                {
                    // Failed (user not found, or data was the same)
                    string script = "showMessage('Could not update profile. Please try again.', 'warning');";
                    ltMessage.Text = "<script>" + script + "</script>";
                }
            }
        }
    }
}