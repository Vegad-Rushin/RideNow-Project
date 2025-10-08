using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RideNow
{
    public partial class register1 : System.Web.UI.Page
    {
        string connect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        void getcon()
        {
            con = new SqlConnection(connect);
            con.Open();
        }

        void clear()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkAgree.Checked = false;
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter all fields correctly.');", true);
                return;
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Passwords do not match. Please try again.');", true);
                return;
            }
            else if (!chkAgree.Checked)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You must agree to the Terms and Privacy policy.');", true);
                return;
            }

            getcon();

            SqlCommand checkEmail = new SqlCommand("select count(*) from Users where email = '" + txtEmail.Text.Trim() + "'", con);
            int eCount = (int)checkEmail.ExecuteScalar();
            SqlCommand checkPhone = new SqlCommand("select count(*) from Users where phone_number = '" + txtPhone.Text.Trim() + "'", con);
            int pCount = (int)checkPhone.ExecuteScalar();

            if (eCount > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('A user with this email address already exists.');", true);
                con.Close();
                return;
            }
            else if (pCount > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('A user with this phone number already exists.');", true);
                con.Close();
                return;
            }

            string insertQuery = "insert into Users (full_name, email, password, phone_number) values ('" + txtName.Text.Trim() + "', '" + txtEmail.Text.Trim() + "', '" + txtPassword.Text + "', '" + txtPhone.Text.Trim() + "'); select SCOPE_IDENTITY();";
            cmd = new SqlCommand(insertQuery, con);

            object newUserId = cmd.ExecuteScalar();
            con.Close();

            if (newUserId != null)
            {
                Session["UserID"] = Convert.ToInt32(newUserId);

                Response.Redirect("login.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred while creating the account. Please try again.');", true);
            }

            clear();
        }

        protected void btnDriverAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("driver/driverRegister.aspx");
        }

    }
}