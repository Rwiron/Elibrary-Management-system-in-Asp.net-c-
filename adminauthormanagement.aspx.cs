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
    public partial class adminauthormanagement : System.Web.UI.Page
    {
        //  string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        // admin add button 
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkifAuthorExist())
            {
                Response.Write("<script>alert('author with this ID already exist.you cannot add another ID');</script>");

            }
            else
            {
                addNewAuthor();
            }
        }

        //updating button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkifAuthorExist())
            {
                userUpdate();

            }
            else
            {
                Response.Write("<script>alert('author does not exit ');</script>");
            }

        }


        // user defined function on adding new author
        void addNewAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl(author_id,author_name)values" +
                    "(@author_id,@author_name)", con);
                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                // for firing our data to database
                cmd.ExecuteNonQuery();
                // once done then close itself
                con.Close();
                Response.Write("<script>alert('Author Added Successfully');</script>");
                clearForm();
                // we need to see the real time data from database and give us new fresh data so to make it we need databind to perfom that 
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        // user definded function on update 
        void userUpdate()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name=@author_name where author_id='" + TextBox1.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                // for firing our data to database
                cmd.ExecuteNonQuery();
                // once done then close itself
                con.Close();
                Response.Write("<script>alert('Author updated Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        // user defined function on checking the author id if exist or not 
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
                SqlCommand cmd = new SqlCommand("select*from author_master_tbl where author_id ='" + TextBox1.Text.Trim() + "';", con);
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

        // user defined function in delete 

        void deleteUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE from author_master_tbl where author_id='" + TextBox1.Text.Trim() + "'", con);

                // from delete we don't need any parameter

                // for firing our data to database
                cmd.ExecuteNonQuery();
                // once done then close itself
                con.Close();
                Response.Write("<script>alert('Author Deleted Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";

        }

        void getAuthorByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select*from author_master_tbl where author_id ='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                // adapting object with corresponding law
                adapter.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();


                }
                else
                {
                    Response.Write("<script>alert('Invalid Author ID');</script>");
                }

               
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                
            }
        }
        // button delete 
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkifAuthorExist())
            {
                deleteUser();
            }
            else
            {
                Response.Write("<script>alert('Author doesn't exist');</script>");
            }
        }

        // Go button
        protected void Button1_Click(object sender, EventArgs e)
        {
            getAuthorByID();
        }
    }
}