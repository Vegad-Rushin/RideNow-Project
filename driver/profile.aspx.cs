using System;
using System.Configuration;
using System.Data.SqlClient;

namespace RideNow.driver
{
    public partial class profile1 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection con;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("../login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadProfileData();
            }
        }

        // Helper method to open the connection
        void OpenConnection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        // Method to load existing data into textboxes
        void LoadProfileData()
        {
            string userId = Session["UserID"].ToString();
            OpenConnection();

            // Insecure query to fetch all data for the profile page from three tables
            string query = "SELECT U.full_name, U.email, U.phone_number, D.date_of_birth, D.license_number, V.make_model, V.year, V.license_plate, V.vehicle_type FROM dbo.Users AS U LEFT JOIN dbo.DriverDetails AS D ON U.user_id = D.driver_id LEFT JOIN dbo.Vehicles AS V ON U.user_id = V.driver_id WHERE U.user_id = '" + userId + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                // Populate Personal Information
                txtFullName.Text = reader["full_name"].ToString();
                txtEmail.Text = reader["email"].ToString();
                txtPhone.Text = reader["phone_number"].ToString();

                // Populate new fields from DriverDetails, checking for nulls to prevent errors
                if (reader["date_of_birth"] != DBNull.Value)
                {
                    txtDateOfBirth.Text = ((DateTime)reader["date_of_birth"]).ToString("yyyy-MM-dd");
                }
                if (reader["license_number"] != DBNull.Value)
                {
                    txtLicenseNumber.Text = reader["license_number"].ToString();
                }

                // Populate Vehicle Information, checking for nulls
                if (reader["make_model"] != DBNull.Value)
                {
                    txtMakeModel.Text = reader["make_model"].ToString();
                    txtVehicleType.Text = reader["vehicle_type"].ToString();
                    txtLicensePlate.Text = reader["license_plate"].ToString();
                    txtYear.Text = reader["year"].ToString();
                }
            }

            con.Close();
        }

        // Event handler for the Save Changes button
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string userId = Session["UserID"].ToString();

            // --- Step 1: Update the Users Table ---
            OpenConnection();
            string updateUserQuery = "UPDATE Users SET full_name = '" + txtFullName.Text + "', email = '" + txtEmail.Text + "', phone_number = '" + txtPhone.Text + "' WHERE user_id = '" + userId + "'";
            SqlCommand cmdUser = new SqlCommand(updateUserQuery, con);
            cmdUser.ExecuteNonQuery();
            con.Close();

            // --- Step 2: Update the DriverDetails Table ---
            OpenConnection();
            string updateDetailsQuery = "UPDATE DriverDetails SET date_of_birth = '" + txtDateOfBirth.Text + "', license_number = '" + txtLicenseNumber.Text + "' WHERE driver_id = '" + userId + "'";
            SqlCommand cmdDetails = new SqlCommand(updateDetailsQuery, con);
            cmdDetails.ExecuteNonQuery();
            con.Close();

            // --- Step 3: Update the Vehicles Table ---
            OpenConnection();
            // Note: Year is a number, so it does not have single quotes around it.
            string updateVehicleQuery = "UPDATE Vehicles SET make_model = '" + txtMakeModel.Text + "', year = " + txtYear.Text + ", license_plate = '" + txtLicensePlate.Text + "', vehicle_type = '" + txtVehicleType.Text + "' WHERE driver_id = '" + userId + "'";
            SqlCommand cmdVehicle = new SqlCommand(updateVehicleQuery, con);
            cmdVehicle.ExecuteNonQuery();
            con.Close();

            // --- Step 4: Show Success Message ---
            lblMessage.Text = "Your profile has been updated successfully!";
            lblMessage.Visible = true;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("../index.aspx");
        }
    }
}