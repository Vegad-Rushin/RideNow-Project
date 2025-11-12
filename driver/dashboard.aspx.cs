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
                LoadDriverStats(userId);
                LoadRideRequests(userId);
            }
        }

        protected void rptRideRequests_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                string status = drv["booking_status"].ToString();

                LinkButton btnAccept = (LinkButton)e.Item.FindControl("btnAccept");
                LinkButton btnDecline = (LinkButton)e.Item.FindControl("btnDecline");
                LinkButton btnComplete = (LinkButton)e.Item.FindControl("btnComplete");

                if (status == "Pending")
                {
                    btnAccept.Visible = true;
                    btnDecline.Visible = true;
                    btnComplete.Visible = false;
                }
                else if (status == "Accepted")
                {
                    btnAccept.Visible = false;
                    btnDecline.Visible = false;
                    btnComplete.Visible = true;
                }
            }
        }

        protected void HandleRideRequest(object sender, CommandEventArgs e)
        {
            string bookingId = e.CommandArgument.ToString(); // Use string for concatenation
            string driverId = Session["UserID"].ToString();
            string query = "";

            if (e.CommandName == "Accept")
            {
                // INSECURE: String concatenation
                query = "UPDATE Bookings SET booking_status = 'Accepted', driver_id = " + driverId + " WHERE booking_id = " + bookingId + " AND booking_status = 'Pending'";
                Session["DriverMessage"] = "<div class='alert alert-success'>Ride accepted! The user has been notified.</div>";
            }
            else if (e.CommandName == "Decline")
            {
                // INSECURE: String concatenation
                query = "UPDATE Bookings SET booking_status = 'Pending', driver_id = NULL WHERE booking_id = " + bookingId + " AND driver_id = " + driverId;
                Session["DriverMessage"] = "<div class='alert alert-info'>Ride has been declined.</div>";
            }
            else if (e.CommandName == "Complete")
            {
                // INSECURE: String concatenation
                query = "UPDATE Bookings SET booking_status = 'Completed' WHERE booking_id = " + bookingId + " AND driver_id = " + driverId;
                Session["DriverMessage"] = "<div class='alert alert-success'>Ride completed! Your earnings have been updated.</div>";
            }

            if (!string.IsNullOrEmpty(query))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // No parameters used
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            Response.Redirect("dashboard.aspx");
        }

        void LoadRideRequests(string driverId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // INSECURE: String concatenation
                string query = @"
                    SELECT 
                        b.booking_id, 
                        b.pickup_address, 
                        b.dropoff_address, 
                        b.distance_km,  
                        b.booking_status,
                        u.full_name 
                    FROM Bookings b 
                    JOIN Users u ON b.user_id = u.user_id 
                    WHERE b.driver_id = " + driverId + @" 
                      AND (b.booking_status = 'Pending' OR b.booking_status = 'Accepted')
                    ORDER BY b.created_at DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                // No parameters used

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rptRideRequests.DataSource = dt;
                    rptRideRequests.DataBind();
                    litRequestCount.Text = $"<span class='request-count'>{dt.Rows.Count} Active</span>";
                }
                else
                {
                    rptRideRequests.Visible = false;
                    litNoRequests.Visible = true;
                    litNoRequests.Text = "<div class='text-center p-4'>You have no active or pending rides.</div>";
                    litRequestCount.Text = "";
                }
            }
        }

        void LoadDriverStats(string driverId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // INSECURE: String concatenation (driverId is used 3 times)
                string query = @"
                    SELECT 
                        (SELECT COUNT(*) FROM Bookings WHERE driver_id = " + driverId + @" AND booking_status = 'Completed') AS TotalRides,
                        (SELECT ISNULL(SUM(total_fare), 0) FROM Bookings WHERE driver_id = " + driverId + @" AND booking_status = 'Completed') AS TotalEarnings,
                        (SELECT ISNULL(SUM(total_fare), 0) FROM Bookings WHERE driver_id = " + driverId + @" AND booking_status = 'Completed' AND CAST(pickup_time AS DATE) = CAST(GETDATE() AS DATE)) AS TodaysEarnings
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // No parameters used
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        litTotalCompletedRides.Text = reader["TotalRides"].ToString();
                        litTotalEarnings.Text = Convert.ToDecimal(reader["TotalEarnings"]).ToString("F2");
                        litTotalEarnings2.Text = Convert.ToDecimal(reader["TotalEarnings"]).ToString("F2");
                        //litTodaysEarnings.Text = "₹ " + Convert.ToDecimal(reader["TodaysEarnings"]).ToString("F2");
                    }
                }
            }
        }

        void LoadDashboardData(string userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // INSECURE: String concatenation
                string query = "SELECT U.full_name, V.make_model, V.year, V.license_plate FROM dbo.Users AS U LEFT JOIN dbo.Vehicles AS V ON U.user_id = V.driver_id WHERE U.user_id = " + userId;

                SqlCommand cmd = new SqlCommand(query, con);
                // No parameters used

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