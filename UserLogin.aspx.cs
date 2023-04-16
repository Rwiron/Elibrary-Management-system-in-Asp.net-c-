using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class UserLogin : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //user login
        protected void Button1_Click(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('Sign Up Successfully ')</script>");
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
                // to prevent sql injection attact i Use parameterized queries instead of string concatenation to avoid SQL injection attacks.
                //SqlCommand cmd = new SqlCommand("select * from member_master_tbl where member_id= '" + TextBox1.Text.Trim() + "'AND password ='" + TextBox2.Text.Trim() + "'", con) ;
                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id=@member_id AND password=@password", con);
                cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim());
                SqlDataReader wr = cmd.ExecuteReader();  
                if(wr.HasRows)
                {
                    while(wr.Read())
                    {
                        //Response.Write("<script>alert('"+wr.GetValue(8).ToString()+"');</script>");
                        Response.Write("<script>alert('Login Successfully');</script>");
                        // that value will coming from database and be stored in session variable 
                        Session["username"]= wr.GetValue(8).ToString();
                        Session["Fullname"]= wr.GetValue(0).ToString();
                        Session["role"] = "user";
                        // for this session will show if user active or inactive
                        Session["status"] = wr.GetValue(10).ToString();

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
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
    }
}


