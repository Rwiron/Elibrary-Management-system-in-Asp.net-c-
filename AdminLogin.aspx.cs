using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            login();
        }
        void login()
        {
            try
            {
                // sql connection is class which prompt use to give use connection object to con and the we pass connection string to conn in (in constructor)
                SqlConnection con = new SqlConnection(conn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                // in this code below we  modified code, we replaced the hard-coded values in the query with parameter placeholders (@username and @password). Then, we used the AddWithValue method to add the actual values from the text boxes as parameters. This way, even if the user enters SQL injection code in the text boxes, it will be treated as a parameter value rather than a part of the query.
                //SqlCommand cmd = new SqlCommand("select * from admin_login_tbl where username= '" + TextBox1.Text.Trim() + "'AND password ='" + TextBox2.Text.Trim() + "'", con);
                SqlCommand cmd = new SqlCommand("SELECT * FROM admin_login_tbl WHERE username=@username AND password=@password", con);
                cmd.Parameters.AddWithValue("@username", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim());
                SqlDataReader wr = cmd.ExecuteReader();
                if (wr.HasRows)
                {
                    while (wr.Read())
                    {
                        // Response.Write("<script>alert('"+wr.GetValue(8).ToString()+"');</script>");
                        // Response.Write("<script>alert('" + wr.GetValue(0).ToString() + "');</script>");
                        Session["username"] = wr.GetValue(0).ToString();
                        Session["fullname"] = wr.GetValue(2).ToString();
                        Session["role"] = "admin";
                        
                    }
                    Response.Redirect("homepage.aspx");
                }
                else
                {

                    Response.Write("<script>alert('Invalid Credentials');</script>");

                }
            }
            catch
            (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}