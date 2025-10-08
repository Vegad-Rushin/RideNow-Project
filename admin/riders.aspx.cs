using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace RideNow.admin
{
    public partial class riders1 : System.Web.UI.Page
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
                LoadRiderStats();
                LoadRidersList();
            }
        }

        private void LoadRiderStats()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Query for total riders
                string totalQuery = "SELECT COUNT(*) FROM Users WHERE role = 'Rider'";
                using (SqlCommand cmd = new SqlCommand(totalQuery, con))
                {
                    int totalRiders = (int)cmd.ExecuteScalar();
                    litTotalRiders.Text = totalRiders.ToString("N0");
                    litActiveRiders.Text = totalRiders.ToString("N0"); // As requested
                }

                // Query for riders new this month
                string newThisMonthQuery = "SELECT COUNT(*) FROM Users WHERE role = 'Rider' AND MONTH(created_at) = MONTH(GETDATE()) AND YEAR(created_at) = YEAR(GETDATE())";
                using (SqlCommand cmd = new SqlCommand(newThisMonthQuery, con))
                {
                    litNewThisMonth.Text = ((int)cmd.ExecuteScalar()).ToString();
                }

                // Set suspended to static 0 as requested
                litSuspended.Text = "0";
            }
        }

        private void LoadRidersList()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // This query joins Users with an aggregated subquery from Bookings to get ride counts and total spent
                string query = @"
                    SELECT 
                        U.user_id,
                        U.full_name,
                        U.email,
                        U.phone_number,
                        U.created_at,
                        ISNULL(B.TotalRides, 0) AS TotalRides,
                        ISNULL(B.TotalSpent, 0) AS TotalSpent
                    FROM Users AS U
                    LEFT JOIN (
                        SELECT 
                            user_id, 
                            COUNT(booking_id) AS TotalRides, 
                            SUM(total_fare) AS TotalSpent 
                        FROM Bookings 
                        GROUP BY user_id
                    ) AS B ON U.user_id = B.user_id
                    WHERE U.role = 'Rider'
                    ORDER BY U.created_at DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rptRiders.DataSource = dt;
                    rptRiders.DataBind();
                }
                else
                {
                    rptRiders.Visible = false;
                    litNoRiders.Visible = true;
                }
            }
        }

        // Helper function to get initials from a full name for the avatar
        public string GetInitials(object fullNameObj)
        {
            if (fullNameObj == null) return "";
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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../index.aspx");
        }
    }
}
