using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace RideNow.driver
{
    public partial class driverRegister1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegisterDriver_Click(object sender, EventArgs e)
        {
            // --- Server-side validation for password match ---
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                litMessage.Text = "<div style='color: #e74c3c; background: #fdd; border: 1px solid #e74c3c; padding: 15px; border-radius: 12px; margin-bottom: 20px;'>Passwords do not match. Please try again.</div>";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // --- Step 1: Insert into the main Users table and get the new user_id ---
                string userInsertQuery = "INSERT INTO Users (full_name, email, password, phone_number, role) VALUES ('" + txtFullName.Text.Trim() + "', '" + txtEmail.Text.Trim() + "', '" + txtPassword.Text + "', '" + txtPhone.Text.Trim() + "', 'Driver'); SELECT SCOPE_IDENTITY();";

                SqlCommand cmdUser = new SqlCommand(userInsertQuery, con);
                int newUserId = Convert.ToInt32(cmdUser.ExecuteScalar());

                // --- Step 2: Insert into the DriverDetails table using the new user_id ---
                string driverInsertQuery = "INSERT INTO DriverDetails (driver_id, full_name, email, phone, date_of_birth, license_number, license_expiry, password) VALUES (" + newUserId + ", '" + txtFullName.Text.Trim() + "', '" + txtEmail.Text.Trim() + "', '" + txtPhone.Text.Trim() + "', '" + txtDateOfBirth.Text + "', '" + txtLicenseNumber.Text.Trim() + "', '" + txtLicenseExpiry.Text + "', '" + txtPassword.Text + "')";

                SqlCommand cmdDriver = new SqlCommand(driverInsertQuery, con);
                cmdDriver.ExecuteNonQuery();

                // --- Step 3: Insert into the Vehicles table ---
                string vehicleInsertQuery = "INSERT INTO Vehicles (driver_id, make_model, year, license_plate, vehicle_type) VALUES (" + newUserId + ", '" + txtMakeModel.Text.Trim() + "', " + txtYear.Text.Trim() + ", '" + txtLicensePlate.Text.Trim() + "', '" + ddlVehicleType.SelectedValue + "')";

                SqlCommand cmdVehicle = new SqlCommand(vehicleInsertQuery, con);
                cmdVehicle.ExecuteNonQuery();

                // --- If all commands succeed, redirect ---
                Session["RegistrationStatus"] = "Success";
                Response.Redirect("../login.aspx");
            }
        }
    }
}