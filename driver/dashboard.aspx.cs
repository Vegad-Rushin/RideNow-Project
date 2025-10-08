using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RideNow.driver
{
    public partial class dashboard1 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserRole"]?.ToString() != "Driver")
            {
                Response.Redirect("../login.aspx");
                return;
            }

            if (Session["DriverMessage"] != null)
            {
                litMessage.Text = Session["DriverMessage"].ToString();
                Session["DriverMessage"] = null;
            }

            if (!IsPostBack)
            {
                string userId = Session["UserID"].ToString();
                LoadDashboardData(userId);
                LoadRideRequests(userId);
            }
        }

        protected void HandleRideRequest(object sender, CommandEventArgs e)
        {
            int bookingId = Convert.ToInt32(e.CommandArgument);
            string driverId = Session["UserID"].ToString();

            if (e.CommandName == "Accept")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Bookings SET booking_status = 'Accepted', driver_id = " + driverId + " WHERE booking_id = " + bookingId;
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                Session["DriverMessage"] = "<div class='alert alert-success'>Ride accepted! The user has been notified.</div>";
            }
            else if (e.CommandName == "Decline")
            {
                Session["DriverMessage"] = "<div class='alert alert-info'>Ride has been declined.</div>";
            }

            Response.Redirect("dashboard.aspx");
        }

        // --- CORRECTED METHOD ---
        void LoadRideRequests(string driverId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // THE FIX: This query now looks for rides where the booking's driver_id
                // matches the ID of the driver who is currently logged in.
                string query = @"
            SELECT TOP 4 
                b.booking_id, 
                b.pickup_address, 
                b.dropoff_address, 
                u.full_name 
            FROM Bookings b 
            JOIN Users u ON b.user_id = u.user_id 
            WHERE b.booking_status = 'Pending' 
              AND b.driver_id = " + driverId + @"
            ORDER BY b.created_at DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rptRideRequests.DataSource = dt;
                    rptRideRequests.DataBind();
                    litRequestCount.Text = $"<span class='request-count'>{dt.Rows.Count} New</span>";
                }
                else
                {
                    rptRideRequests.Visible = false;
                    litNoRequests.Visible = true;
                    // The message from your screenshot is better, so we'll keep it.
                    litNoRequests.Text = "<div class='text-center p-4'>No new ride requests for your vehicle type at the moment.</div>";
                    litRequestCount.Text = "";
                }
            }
        }

        void LoadDashboardData(string userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT U.full_name, V.make_model, V.year, V.license_plate FROM dbo.Users AS U LEFT JOIN dbo.Vehicles AS V ON U.user_id = V.driver_id WHERE U.user_id = " + userId;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string driverFullName = reader["full_name"].ToString();
                    litDriverName.Text = driverFullName.Split(' ')[0];
                    litProfileName.Text = driverFullName;
                    if (reader["make_model"] != DBNull.Value)
                    {
                        litVehicleInfo.Text = $"{reader["make_model"]} ({reader["year"]})";
                        litLicensePlate.Text = reader["license_plate"].ToString();
                    }
                    else
                    {
                        litVehicleInfo.Text = "No vehicle assigned";
                        litLicensePlate.Text = "N/A";
                    }
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../index.aspx");
        }
    }
}