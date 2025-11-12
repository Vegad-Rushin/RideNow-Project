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

        // --- CORRECTIONS ARE HERE ---
        protected HtmlInputText firstName;
        protected HtmlInputGenericControl email; // <-- WAS HtmlInputText
        protected HtmlInputGenericControl phone; // <-- WAS HtmlInputText
        protected HtmlGenericControl displayPhone;
        protected HtmlGenericControl displayEmail;
        // --- END CORRECTIONS ---


        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. Check if user is logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("login.aspx");
                return; // Stop processing the page
            }

            if (!IsPostBack)
            {
                // 2. If it's the first time loading, load the user's data
                string userId = Session["UserID"].ToString();
                LoadUserProfile(userId);
            }
            else
            {
                // 3. If it IS a postback, run the update logic

                // Get values from the runat="server" controls
                string fullName = firstName.Value;
                string emailValue = email.Value;
                string phoneValue = phone.Value;
                string userId = Session["UserID"].ToString();

                // 4. Create the SQL UPDATE query string (INSECURE, as requested)
                string query = "UPDATE Users SET full_name = '" + fullName + "', email = '" + emailValue + "', phone_number = '" + phoneValue + "' WHERE user_id = " + userId;

                // 5. Connect to the database and execute the query
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // This runs the UPDATE command
                con.Close();

                // 6. Show a success or warning message
                if (rowsAffected > 0)
                {
                    // Success!
                    string script = "showMessage('Profile updated successfully!', 'success');";
                    ltMessage.Text = "<script>" + script + "</script>";

                    // Also update the "Contact Information" section to show the new values
                    displayPhone.InnerText = phoneValue;
                    displayEmail.InnerText = emailValue;
                }
                else
                {
                    // Failed
                    string script = "showMessage('Could not update profile. Please try again.', 'warning');";
                    ltMessage.Text = "<script>" + script + "</script>";
                }
            }
        }

        // This method fetches the user's data and fills the form
        protected void LoadUserProfile(string userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // INSECURE query as requested
                string query = "SELECT full_name, email, phone_number FROM Users WHERE user_id = " + userId;

                SqlCommand cmd = new SqlCommand(query, con);
                // No parameters used

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string dbFullName = reader["full_name"].ToString();
                    string dbEmail = reader["email"].ToString();
                    string dbPhone = reader["phone_number"].ToString();

                    // 1. Populate the form textboxes
                    firstName.Value = dbFullName;
                    email.Value = dbEmail;
                    phone.Value = dbPhone;

                    // 2. Populate the "Contact Information" section
                    displayPhone.InnerText = dbPhone;
                    displayEmail.InnerText = dbEmail;
                }
                reader.Close();
            }
        }
    }
}