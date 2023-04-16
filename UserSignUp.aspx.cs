using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class UserSignUp : System.Web.UI.Page
    {
        // creating the variable which will hold connection string we should note on that before

        string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // when sign up button clicked on the event 
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkmemberExist())
            {
                Response.Write("<script>alert('Member Already Exist With This Member ID,Try another ID Please');</script>");
            }
            else
            {
                signUpNewUser();
            }
           
        }

        // best pratical instead of adding any code in click event we have to create method then we call in click event
        // not returning any value or any parameter
        // user defined method 

        void signUpNewUser()
        {
            // giving user pop message
            // Response.Write("<script>alert('Testing mode');</script>"); 
            // whenever we try to connect to database it better to use try and catch in order to handle those expcetion 
            // conn will hold connection string configuration.
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tbl(full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status)values" +
                    "(@full_name,@dob,@contact_no,@email,@state,@city,@pincode,@full_address,@member_id,@password,@account_status)", con);
                cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                //cmd.Parameters.AddWithValue("@state", TextBox5.Text.Trim()); it dropdown
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "pending");

                // for firing our data to database
                cmd.ExecuteNonQuery();
                // once done then close itself
                con.Close();
                Response.Write("<script>alert('Sign Up Successfully Go to User Login to Login');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkmemberExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select*from member_master_tbl where member_id ='" + TextBox8.Text.Trim()+"';", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    // if we pass the correct id it will equal to 1 which mean it exist the entry in database
                    return true;

                }
                else
                {
                    return false;
                }
                    

                // for firing our data to database
                cmd.ExecuteNonQuery();
                // once done then close itself
                con.Close();
                Response.Write("<script>alert('Sign Up Successfully Go to User Login to Login');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
           
        }
    }
}