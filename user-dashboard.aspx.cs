using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

namespace RideNow
{
    public partial class user_dashboard1 : System.Web.UI.Page
    {
        string connect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    string userId = Session["UserID"].ToString();
                    displayUser(userId);
                    DisplayRecentRides(userId);
                }
            }
        }

        protected void displayUser(string userId)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                
                string query = @"
                    SELECT 
                        full_name, 
                        email, 
                        phone_number,
                        (SELECT COUNT(*) FROM Bookings WHERE user_id = " + userId + @" AND booking_status = 'Completed') AS TotalRides,
                        (SELECT ISNULL(SUM(total_fare), 0) FROM Bookings WHERE user_id = " + userId + @" AND booking_status = 'Completed') AS TotalSpent
                    FROM Users 
                    WHERE user_id = " + userId;

                SqlCommand cmd = new SqlCommand(query, con);
                

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string userName = reader["full_name"].ToString();
                    litUserNameWelcome.Text = userName;
                    litUserNameProfile.Text = userName;
                    litUserEmail.Text = reader["email"].ToString();
                    litUserPhone.Text = reader["phone_number"].ToString();

                    litTotalRides.Text = reader["TotalRides"].ToString();
                    litTotalSpent.Text = "₹ " + Convert.ToDecimal(reader["TotalSpent"]).ToString("F2");
                }
            }
        }

        protected void DisplayRecentRides(string userId)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                
                string query = "SELECT TOP 3 pickup_address, dropoff_address, pickup_time, total_fare, booking_status FROM Bookings WHERE user_id = " + userId + " ORDER BY created_at DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rptRecentRides.DataSource = ds;
                    rptRecentRides.DataBind();
                }
                else
                {
                    rptRecentRides.Visible = false;
                    litNoRides.Visible = true;
                    litNoRides.Text = "<div style='text-align:center; padding: 20px;'>You have no recent rides.</div>";
                }
            }
        }

        [WebMethod]
        public static object GetRideStatusUpdate()
        {
            if (HttpContext.Current.Session["UserID"] == null)
            {
                return null;
            }

            string userId = HttpContext.Current.Session["UserID"].ToString();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                
                string query = @"
                    SELECT TOP 1 
                        b.booking_id, 
                        d.full_name AS driver_name, 
                        v.make_model, 
                        v.license_plate
                    FROM Bookings b
                    JOIN Users d ON b.driver_id = d.user_id
                    JOIN Vehicles v ON b.driver_id = v.driver_id
                    WHERE b.user_id = " + userId + @" 
                      AND b.booking_status = 'Accepted' 
                      AND b.notified_user = 0";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        var rideData = new
                        {
                            bookingId = reader["booking_id"].ToString(),
                            driverName = reader["driver_name"].ToString(),
                            vehicle = reader["make_model"].ToString(),
                            license = reader["license_plate"].ToString()
                        };
                        reader.Close();

                        
                        string updateQuery = "UPDATE Bookings SET notified_user = 1 WHERE booking_id = " + rideData.bookingId;
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                        {
                            
                            updateCmd.ExecuteNonQuery();
                        }
                        return rideData;
                    }
                }
            }
            return null;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("index.aspx");
        }
    }
}