using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls; // Make sure this is here

namespace RideNow
{
    public partial class editProfile1 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected HtmlInputText firstName;
        protected HtmlInputGenericControl email; 
        protected HtmlInputGenericControl phone; 
        protected HtmlGenericControl displayPhone;
        protected HtmlGenericControl displayEmail;


        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UserID"] == null)
            {
                Response.Redirect("login.aspx");
                return; 
            }

            if (!IsPostBack)
            {
                string userId = Session["UserID"].ToString();
                LoadUserProfile(userId);
            }
            else
            {

                string fullName = firstName.Value;
                string emailValue = email.Value;
                string phoneValue = phone.Value;
                string userId = Session["UserID"].ToString();

                string query = "UPDATE Users SET full_name = '" + fullName + "', email = '" + emailValue + "', phone_number = '" + phoneValue + "' WHERE user_id = " + userId;

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); 
                con.Close();

                if (rowsAffected > 0)
                {
                    string script = "showMessage('Profile updated successfully!', 'success');";
                    ltMessage.Text = "<script>" + script + "</script>";

                    displayPhone.InnerText = phoneValue;
                    displayEmail.InnerText = emailValue;
                }
                else
                {
                    string script = "showMessage('Could not update profile. Please try again.', 'warning');";
                    ltMessage.Text = "<script>" + script + "</script>";
                }
            }
        }

        protected void LoadUserProfile(string userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT full_name, email, phone_number FROM Users WHERE user_id = " + userId;

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string dbFullName = reader["full_name"].ToString();
                    string dbEmail = reader["email"].ToString();
                    string dbPhone = reader["phone_number"].ToString();

                    firstName.Value = dbFullName;
                    email.Value = dbEmail;
                    phone.Value = dbPhone;

                    displayPhone.InnerText = dbPhone;
                    displayEmail.InnerText = dbEmail;
                }
                reader.Close();
            }
        }
    }
}