using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // checking if the user is admin or member in the loader page
            // in order to not let our site crash we handle it by try and catch to handle those exception it a good pratice

            try
            {
                //if (Session["role"].Equals("")) for giving first pratical in order to see if they guess
                // this is the best pratice to chekc if no one log in those link will remain false visibel
                // that why we mention role = null 
                if (Session["role"] == null)
                {
                    // making user link visible 
                    LinkButton2.Visible = true; // user login link button
                    LinkButton3.Visible = true; // sign up link button
                    // since no one login logout na hello user zigomba kuguma ari false
                    LinkButton4.Visible = false; // logout up link button
                    LinkButton5.Visible = false;  // hello user  up link button
                    LinkButton6.Visible = true;
                    LinkButton7.Visible = false;
                    LinkButton8.Visible = false;
                    LinkButton9.Visible = false;
                    LinkButton10.Visible = false;
                    LinkButton11.Visible = false;
                }
                else if (Session["role"].Equals("user"))
                    // when user login below are thier link to be viewed
                {
                    LinkButton2.Visible = false; // user login link button
                    LinkButton3.Visible = false; // sign up link button
                    // since no one login logout na hello user zigomba kuguma ari false
                    LinkButton4.Visible = true; // logout up link button
                    LinkButton5.Visible = true;  // hello user  up link button
                    LinkButton5.Text = "Hello " + Session["username"].ToString();

                    LinkButton6.Visible = true;
                    LinkButton7.Visible = false;
                    LinkButton8.Visible = false;
                    LinkButton9.Visible = false;
                    LinkButton10.Visible = false;
                    LinkButton11.Visible = false;
                }

                else if (Session["role"].Equals("admin"))
                    // when admin login below are link to be logged in 
                {
                    LinkButton2.Visible = false; // user login link button
                    LinkButton3.Visible = false; // sign up link button
                    // since no one login logout na hello user zigomba kuguma ari false
                    LinkButton4.Visible = true; // logout up link button
                    LinkButton5.Visible = true;  // hello user  up link button
                    LinkButton5.Text = "Hello Admin";

                    LinkButton6.Visible = false; //admin link button
                    LinkButton7.Visible = true; //author management link button
                    LinkButton8.Visible = true; //publisher mgmt link button
                    LinkButton9.Visible = true; //book inventory link button
                    LinkButton10.Visible = true; //book issuing link button
                    LinkButton11.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }

        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            //to navigate the link page where you want to link them 
            Response.Redirect("adminlogin.aspx");
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminauthormanagement.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminpublishermanagement.aspx");
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminbookinventory.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminbookissuing.aspx");
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminmembermanagement.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewbooks.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("userlogin.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("usersignup.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["fullname"] = "";
            Session["role"] = "";
            Session["status"] = "";

            // Default for when users make logout  
            LinkButton2.Visible = true; // user login link button
            LinkButton3.Visible = true; // sign up link button
                                        // since no one login logout na hello user zigomba kuguma ari false
            LinkButton4.Visible = false; // logout up link button
            LinkButton5.Visible = false;  // hello user  up link button
            LinkButton6.Visible = true;
            LinkButton7.Visible = false;
            LinkButton8.Visible = false;
            LinkButton9.Visible = false;
            LinkButton10.Visible = false;
            LinkButton11.Visible = false;
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("userprofile.aspx");
        }
    }
}