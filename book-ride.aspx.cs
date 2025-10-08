using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;

namespace RideNow
{
    public partial class book_ride1 : System.Web.UI.Page
    {
        string connect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user's session exists
            if (Session["UserID"] != null)
            {
                if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Driver")
                {
                    // If the role is "Driver", show the DRIVER nav-options
                    liLogin.Visible = false;
                    liGetStarted.Visible = false; // Hide the "Get Started" button

                    liDriverDashboard.Visible = true;
                    liDriverProfile.Visible = true;
                    liLogout.Visible = true;      // Show the "Logout" button
                    liDashboard.Visible = false;

                }
                else if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Rider")
                {
                    // If the role is "Rider", show the Rider nav-options
                    liLogin.Visible = false;
                    liGetStarted.Visible = false; // Hide the "Get Started" button
                    liDriverDashboard.Visible = false;
                    liDriverProfile.Visible = false;

                    liDashboard.Visible = true;
                    liLogout.Visible = true;      // Show the "Logout" button

                    if (!IsPostBack)
                    {
                        readUserDetails();
                        //txtFare.Text = "50.00";
                    }

                }

            }
            else
            {
                // User is NOT logged in (is a guest)
                liLogin.Visible = true;
                liGetStarted.Visible = true;

                liDriverDashboard.Visible = false;
                liDriverProfile.Visible = false;
                liDashboard.Visible = false;
                liLogout.Visible = false;

                Response.Redirect("login.aspx");
                return;

            }


        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("index.aspx");
        }

        void getcon()
        {
            con = new SqlConnection(connect);
            con.Open();
        }

        void readUserDetails()
        {
            string userId = Session["UserID"].ToString();
            getcon();

            cmd = new SqlCommand("select full_name, phone_number from users where user_id = '" + userId + "'", con);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtName.Text = reader["full_name"].ToString();
                txtPhone.Text = reader["phone_number"].ToString();

            }
            reader.Close();
            con.Close();
        }

        void clearForm()
        {
            ddlVehicleType.SelectedIndex = 0;
            txtPickupTime.Text = "";
            txtDistance.Text = "";
            txtPickupAddress.Text = "";
            txtDropoffAddress.Text = "";
        }

        protected void btnBookRide_Click(object sender, EventArgs e)
        {
            // Basic form validation for all fields
            if (string.IsNullOrWhiteSpace(ddlVehicleType.SelectedValue) ||
                string.IsNullOrWhiteSpace(txtDistance.Text) ||
                string.IsNullOrWhiteSpace(txtPickupTime.Text) ||
                string.IsNullOrWhiteSpace(txtPickupAddress.Text) ||
                string.IsNullOrWhiteSpace(txtDropoffAddress.Text))
            {
                Response.Write("<script>alert('Please fill out all required fields.');</script>");
                return;
            }

            // --- 1. FIND AN AVAILABLE DRIVER ---
            string selectedVehicleType = ddlVehicleType.SelectedValue;
            int assignedDriverId = 0; // Default to 0 (no driver)

            getcon();
            // This query finds the ID of the first driver who has the selected vehicle type
            string findDriverQuery = "SELECT TOP 1 driver_id FROM Vehicles WHERE vehicle_type = '" + selectedVehicleType + "'";
            cmd = new SqlCommand(findDriverQuery, con);
            object result = cmd.ExecuteScalar(); // Use ExecuteScalar for getting a single value

            if (result != null && result != DBNull.Value)
            {
                assignedDriverId = Convert.ToInt32(result);
            }
            con.Close();

            // If no driver is found for that vehicle type, show an error and stop
            if (assignedDriverId == 0)
            {
                Response.Write("<script>alert('Sorry, no drivers are available for the selected vehicle type at the moment.');</script>");
                return;
            }

            // --- 2. CALCULATE FARE AND SAVE THE BOOKING ---
            decimal distance = decimal.Parse(txtDistance.Text);
            decimal ratePerKm = 15.0m; // Your rate per KM
            decimal totalFare = distance * ratePerKm;

            string pickupDateTime = DateTime.Parse(txtPickupTime.Text).ToString("yyyy-MM-dd HH:mm:ss");
            string userId = Session["UserID"].ToString();

            getcon();
            // This is the new INSERT query with the driver_id, distance, calculated fare, and status
            string insertQuery = "INSERT INTO Bookings (user_id, driver_id, full_name, phone_number, vehicle_type, total_fare, distance_km, pickup_time, pickup_address, dropoff_address, booking_status) VALUES ('" + userId + "', " + assignedDriverId + ", '" + txtName.Text.Trim() + "', '" + txtPhone.Text.Trim() + "', '" + selectedVehicleType + "', " + totalFare + ", " + distance + ", '" + pickupDateTime + "', '" + txtPickupAddress.Text.Trim() + "', '" + txtDropoffAddress.Text.Trim() + "', 'Pending')";

            cmd = new SqlCommand(insertQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();

            // --- 3. SHOW SUCCESS MESSAGE AND REDIRECT ---
            Response.Write("<script>alert('Your ride has been booked successfully and sent to a driver!');</script>");

            clearForm(); // Clear the form fields for the next booking

            // Redirect to the user dashboard after a 2-second delay so they can read the alert
            Response.AddHeader("REFRESH", "2;URL=user-dashboard.aspx");
        }
    }
}

