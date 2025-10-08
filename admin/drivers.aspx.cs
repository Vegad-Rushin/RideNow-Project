using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace RideNow.admin
{
    public partial class drivers1 : System.Web.UI.Page
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
                LoadDriverStats();
                LoadDriversList();
            }
        }

        private void LoadDriverStats()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE role = 'Driver'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    int driverCount = (int)cmd.ExecuteScalar();

                    // Set the text for the stat cards
                    litTotalDrivers.Text = driverCount.ToString();
                    litActiveDrivers.Text = driverCount.ToString(); // As requested, same as total
                    litPending.Text = "0";
                    litSuspended.Text = "0";
                }
            }
        }

        private void LoadDriversList()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // This query joins Users and Vehicles, and includes a subquery to count rides for each driver.
                string query = @"
                    SELECT 
                        U.user_id,
                        U.full_name,
                        U.email,
                        U.phone_number,
                        U.created_at,
                        ISNULL(V.make_model, 'N/A') AS make_model,
                        (SELECT COUNT(*) FROM Bookings WHERE driver_id = U.user_id) AS TotalRides
                    FROM Users AS U
                    LEFT JOIN Vehicles AS V ON U.user_id = V.driver_id
                    WHERE U.role = 'Driver'
                    ORDER BY U.created_at DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rptDrivers.DataSource = dt;
                    rptDrivers.DataBind();
                }
                else
                {
                    rptDrivers.Visible = false;
                    litNoDrivers.Visible = true;
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
