using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class secPages_updatePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void updateBtn_click(object sender, EventArgs e)
    {
        
       
        
        try
        {
            if (string.IsNullOrEmpty(oldPassword.Text))
            {
                labelMessage.InnerHtml  = "Currrent Password is required";
                 
                
            }
            else if (string.IsNullOrEmpty(password1.Text))
            {
                labelMessage.InnerHtml = "New Password is required";
                


            }
            else if (string.IsNullOrEmpty(password2.Text))
            {
                labelMessage.InnerHtml = "Confirm Password is required";
                
            }
            else if (password1.Text == password2.Text)
            {
                MembershipUser  user = Membership.GetUser(); 
                Boolean pwdChg =
                user.ChangePassword(oldPassword.Text, password1.Text);
                Membership.UpdateUser(user);
                if (pwdChg)
                {
                    labelMessage.InnerHtml = "Password updated";
                    labelMessage.Attributes.Add("color", "green");   
                }
                else
                {
                    labelMessage.InnerHtml = "Password update failed";
                    labelMessage.Attributes.Add("color", "red");
                        
   
                }
            }
            else
            {
                labelMessage.InnerHtml = "New passwords have to be same.";

            }
        }
        catch (Exception err)
        {
            labelMessage.InnerHtml = "Server error generated. Please try later.";
        }

        passwrdUpdatePanel.DataBind ();   
    }
}
