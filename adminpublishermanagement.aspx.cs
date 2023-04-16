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
    public partial class adminpublishermanagement : System.Web.UI.Page
    {

        string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        // add button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkifpublisherExist())
            {
                Response.Write("<script>alert('publisher  with this ID already exist.you cannot add another ID');</script>");
            }
            else
            {
                add();
            }
        }

        // update button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkifpublisherExist())
            {
                update();

            }
            else
            {
                Response.Write("<script>alert('publisher  does not exit ');</script>");
            }
        }

        // Delete button
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkifpublisherExist())
            {

                delete();

            }
            else
            {
                Response.Write("<script>alert('publisher doesn't exist');</script>");
            }
        }

        void add()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO publisher_master_tbl(publisher_id,publisher_name)values" +
                    "(@publisher_id,@publisher_name)", con);
                cmd.Parameters.AddWithValue("@publisher_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());

                // for firing our data to database
                cmd.ExecuteNonQuery();
                // once done then close itself
                con.Close();
                Response.Write("<script>alert('Publisher Added Successfully');</script>");
                clearForm();
                // we need to see the real time data from database and give us new fresh data so to make it we need databind to perfom that 
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void update()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE publisher_master_tbl SET publisher_name=@publisher_name where publisher_id='" + TextBox1.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());

                // for firing our data to database
                cmd.ExecuteNonQuery();
                // once done then close itself
                con.Close();
                Response.Write("<script>alert('Publisher updated Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void delete()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE from publisher_master_tbl where publisher_id='" + TextBox1.Text.Trim() + "'", con);

                // from delete we don't need any parameter

                // for firing our data to database
                cmd.ExecuteNonQuery();
                // once done then close itself
                con.Close();
                Response.Write("<script>alert('Publisher Deleted Successfully');</script>");
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

        bool checkifpublisherExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                // checking if database connection is open or not and on this condition if it closed then what we is we want to open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select*from publisher_master_tbl where publisher_id ='" + TextBox1.Text.Trim() + "';", con);
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
                SqlCommand cmd = new SqlCommand("select*from publisher_master_tbl where publisher_id ='" + TextBox1.Text.Trim() + "';", con);
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
                    Response.Write("<script>alert('Invalid publishers ID');</script>");
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // go button 
        protected void Button1_Click(object sender, EventArgs e)
        {
            getAuthorByID();
        }
    }
}
