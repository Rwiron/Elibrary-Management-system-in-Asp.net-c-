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
    public partial class adminmember : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            // binding data from database
            GridView1.DataBind();
        }
        // Go button 
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            getMemberById();
        }
        // active button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // status that have to be passed should be active
            ActiveUser("active");
        }
        // pending button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ActiveUser("pending");
        }
        //Deactivate button 
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            ActiveUser("deactivated");
        }
        // delete button
        protected void Button2_Click(object sender, EventArgs e)
        {
            deleteUser();
        }
        void getMemberById()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from member_master_tbl where member_id='" + TextBox1.Text.Trim() + "'", con);
                SqlDataReader dt = cmd.ExecuteReader();

                if (dt.HasRows)
                {
                    while(dt.Read())
                    {
                        TextBox2.Text = dt.GetValue(0).ToString();
                        TextBox7.Text = dt.GetValue(10).ToString();
                        TextBox8.Text = dt.GetValue(1).ToString();
                        TextBox3.Text = dt.GetValue(2).ToString();
                        TextBox4.Text = dt.GetValue(3).ToString();
                        TextBox9.Text = dt.GetValue(4).ToString();
                        TextBox10.Text = dt.GetValue(5).ToString();
                        TextBox11.Text = dt.GetValue(6).ToString();
                        TextBox6.Text = dt.GetValue(7).ToString();
                    }
                   
                }
                else
                {
                    Response.Write("<script>alert('Invalid Credentials');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
                
            }
        }
        void ActiveUser(string status) // update member function
        {
            if (checkifAuthorExist())
            {
                try
                {
                    SqlConnection con = new SqlConnection(conn);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("update member_master_tbl set account_status ='" + status + "' where member_id='" + TextBox1.Text.Trim() + "'", con);
                    // for update
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Member Status Updated');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");

                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID')</script>");
            }
        }
        void deleteUser()
        {
            if (checkifAuthorExist())
            {
                try
                {
                    SqlConnection con = new SqlConnection(conn);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("DELETE from member_master_tbl WHERE member_id='" + TextBox1.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Member Deleted Successfully');</script>");
                    clear();
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
               
            }
            else
            {
                Response.Write("<script>alert('Error in Deleting Member ID Kindly Please Check If Member ID is valid')</script>"); 
            }
            
            
        }
        void clear()
        {
            TextBox2.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox6.Text = "";
        }

        bool checkifAuthorExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select*from member_master_tbl where member_id ='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                // adapting object with corresponding law
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