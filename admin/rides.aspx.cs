using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace RideNow.admin
{
    public partial class rides1 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("../login.aspx");
            }

            if (!IsPostBack)
            {
                LoadRideStats();
                LoadRidesList();
            }
        }

        private void LoadRideStats()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Total Rides
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Bookings", con))
                {
                    litTotalRides.Text = ((int)cmd.ExecuteScalar()).ToString("N0");
                }

                // Active Rides (In Progress or Accepted)
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Bookings WHERE booking_status = 'Accepted'", con))
                {
                    litActiveRides.Text = ((int)cmd.ExecuteScalar()).ToString("N0");
                }

                // Completed Today
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Bookings WHERE booking_status = 'Completed' AND CAST(created_at AS DATE) = CAST(GETDATE() AS DATE)", con))
                {
                    litCompletedToday.Text = ((int)cmd.ExecuteScalar()).ToString("N0");
                }

                // Total Revenue
                using (SqlCommand cmd = new SqlCommand("SELECT SUM(total_fare) FROM Bookings WHERE booking_status = 'Completed'", con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        litTotalRevenue.Text = Convert.ToDecimal(result).ToString("C");
                    }
                }
            }
        }

        private void LoadRidesList()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // This query joins Bookings with the Users table twice to get both rider and driver names
                string query = @"
                    SELECT 
                        B.booking_id,
                        B.pickup_address,
                        B.dropoff_address,
                        ISNULL(Rider.full_name, 'N/A') AS rider_name,
                        ISNULL(Driver.full_name, 'Not Assigned') AS driver_name,
                        B.booking_status,
                        B.total_fare,
                        B.created_at
                    FROM Bookings AS B
                    LEFT JOIN Users AS Rider ON B.user_id = Rider.user_id
                    LEFT JOIN Users AS Driver ON B.driver_id = Driver.user_id
                    ORDER BY B.created_at DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rptRides.DataSource = dt;
                    rptRides.DataBind();
                }
                else
                {
                    rptRides.Visible = false;
                    litNoRides.Visible = true;
                }
            }
        }

        public string GetInitials(object fullNameObj)
        {
            if (fullNameObj == null || fullNameObj == DBNull.Value) return "";
            string fullName = fullNameObj.ToString();
            string[] names = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string initials = "";
            if (names.Length > 0)
            {
                initials += names[0][0];
                if (names.Length > 1)
                {
                    initials += names[names.Length - 1][0];
                }
            }
            return initials.ToUpper();
        }

        public string GetStatusClass(object statusObj)
        {
            if (statusObj == null) return "";
            string status = statusObj.ToString().ToLower();
            switch (status)
            {
                case "completed": return "status-completed";
                case "accepted": return "status-accepted";
                case "cancelled": return "status-cancelled";
                case "pending": return "status-pending";
                default: return "status-scheduled";
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
