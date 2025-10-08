using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RideNow
{
    public partial class about1 : System.Web.UI.Page
    {
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
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("index.aspx");
        }

    }
}