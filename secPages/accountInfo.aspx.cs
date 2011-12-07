using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient ;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Configuration ;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class secPages_accountInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        errorMsg.Text = " ";

        

        if (!IsPostBack)
        {
            userNameTxt.Text = Membership.GetUser().UserName;
            EmailTextBox.Text = Membership.GetUser().Email;
            LocationBox.Text = CommonFunctions.getUserLocation(Membership.GetUser().ProviderUserKey.ToString());

            if (this.User.IsInRole("surfer"))
            {
                LinkUpGradeAccount.Enabled  = true;
            }
            else if (this.User.IsInRole("bizOwner"))
            {

                LinkUpDisableAccount.Enabled  = true; 
            }
 

        }
        errorMsg.Text = " ";  
    }

    protected void emailText_changed(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            
            try
            {
                MembershipUser user = Membership.GetUser();
                user.Email = EmailTextBox.Text;
                Membership.UpdateUser(user);
                errorMsg.Text = "Email Updated";
                //UpdatePanel1.Update();
            }
            
            catch (System.Configuration.Provider.ProviderException err)
            {
                errorMsg.Text = "Error updating email - " + err.Message;
            }
            catch (Exception err)
            {
                errorMsg.Text = "Error updating email - " + err.Message;
            }
        }
    }

    protected void LocationText_changed(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

            string zipCode = LocationBox.Text.Trim();
            if (CommonFunctions.isNumeric(zipCode))
            {

                
            }
            else if (CommonFunctions.validCityStateFormat(zipCode))
            {
                zipCode = CommonFunctions.getZipcode(zipCode);
            }
            else
            {
                errorMsg.Text = " Invalid Zipcode or City,State Format";
                return;
            }
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
                using (SqlConnection con =
                         new SqlConnection(connStr))
                {
                    string sqlstatement = "UPDATE userLocationTable " +
                        " SET zipCodeLocation =" + zipCode +
                        "  WHERE ID = '" +
                        Membership.GetUser().ProviderUserKey.ToString() + "'";
                    //System.Windows.Forms.MessageBox.Show(sqlstatement);     
                    SqlCommand cmd = new SqlCommand(sqlstatement, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    errorMsg.Text = "Zipcode Updated" + cmd.ExecuteNonQuery().ToString (); 
                    con.Close();
                    
                }
            }
            catch (SqlException ex)
            {
                errorMsg.Text = " Error Updating Changes. Try later."; 

            }
        }
    }

    protected void LinkUpGradeAccount_Click(object sender, EventArgs e)
    {
        System.Windows.Forms.MessageBox.Show("in Duty");     
    
    }

    protected void acceptButton_Click(object sender, EventArgs e)
    {

        Roles.RemoveUserFromRole(Membership.GetUser().UserName, "surfer");
        Roles.AddUserToRole(Membership.GetUser().UserName, "bizOwner");
        int rtnCode = CommonFunctions.initDatabaseForNewRegistry(Membership.GetUser().UserName, null);
        // The following line is addded to Refresh the whole page. Needed as the Masterfile links were getting messed up !.
        Response.Redirect(Request.Path.ToString());
   
    }

    protected void disAbleBtn_Click(object sender, EventArgs e)
    {
        Roles.RemoveUserFromRole(Membership.GetUser().UserName , "bizOwner");
        Roles.AddUserToRole(Membership.GetUser().UserName, "surfer");
        // The following line is addded to Refresh the whole page. Needed as the Masterfile links were getting messed up !.
        Response.Redirect(Request.Path.ToString());    
    }
}