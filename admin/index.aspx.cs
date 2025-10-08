using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RideNow.admin
{
    public partial class index : System.Web.UI.Page
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
                LoadDashboardStats();
                LoadRecentActivities(); // This will load our new dynamic activities
            }
        }

        private void LoadDashboardStats()
        {
            // This method remains the same as before
            int driverCount = 0, riderCount = 0, rideCount = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE role = 'Driver'", con))
                {
                    driverCount = (int)cmd.ExecuteScalar();
                }
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE role = 'Rider'", con))
                {
                    riderCount = (int)cmd.ExecuteScalar();
                }
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Bookings", con))
                {
                    rideCount = (int)cmd.ExecuteScalar();
                }
            }
            litTotalDrivers.Text = driverCount.ToString("N0");
            litTotalRiders.Text = riderCount.ToString("N0");
            litTotalRides.Text = rideCount.ToString("N0");
        }

        // --- NEW METHOD TO LOAD DYNAMIC ACTIVITIES ---
        private void LoadRecentActivities()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // This simple query gets the last 5 registered users (Drivers or Riders).
                string query = @"
                    SELECT TOP 5 full_name, role, created_at 
                    FROM Users 
                    ORDER BY created_at DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rptRecentActivities.DataSource = dt;
                    rptRecentActivities.DataBind();
                }
                else
                {
                    rptRecentActivities.Visible = false;
                    litNoActivities.Visible = true;
                }
            }
        }

        // --- NEW EVENT HANDLER TO SET THE CORRECT TEXT AND ICONS ---
        protected void rptRecentActivities_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                string role = drv["role"].ToString();
                string fullName = drv["full_name"].ToString();

                var divIcon = (HtmlGenericControl)e.Item.FindControl("divActivityIcon");
                var iIcon = (HtmlGenericControl)e.Item.FindControl("iActivityIcon");
                var litActivityType = (Literal)e.Item.FindControl("litActivityType");
                var litActivityDetail = (Literal)e.Item.FindControl("litActivityDetail");

                if (role == "Driver")
                {
                    litActivityType.Text = "New Driver Registered";
                    litActivityDetail.Text = fullName + " joined the platform";
                    divIcon.Attributes["class"] = "activity-icon new-driver";
                    iIcon.Attributes["class"] = "fa fa-user-plus";
                }
                else // Assume any other role is a Rider
                {
                    litActivityType.Text = "New Rider Registered";
                    litActivityDetail.Text = fullName + " created an account";
                    divIcon.Attributes["class"] = "activity-icon new-rider";
                    iIcon.Attributes["class"] = "fa fa-user";
                }
            }
        }

        // --- NEW HELPER FUNCTION TO DISPLAY TIME LIKE "2 hours ago" ---
        public string FormatTimeAgo(object activityDateObj)
        {
            DateTime activityDate = Convert.ToDateTime(activityDateObj);
            TimeSpan timeSince = DateTime.Now.Subtract(activityDate);

            if (timeSince.TotalMinutes < 1) return "just now";
            if (timeSince.TotalMinutes < 60) return $"{Convert.ToInt32(timeSince.TotalMinutes)} min ago";
            if (timeSince.TotalHours < 24) return $"{Convert.ToInt32(timeSince.TotalHours)} hours ago";
            return $"{Convert.ToInt32(timeSince.TotalDays)} days ago";
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../index.aspx");
        }
    }
}