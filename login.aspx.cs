using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["ReturnUrl"].Substring(Request.QueryString["ReturnUrl"].LastIndexOf(@"/") + 1).ToString() == "disCounts.aspx")
            {
                Login1.CreateUserText = null;
                Login1.CreateUserUrl = null;
                Login1.PasswordRecoveryText = null;
                Login1.PasswordRecoveryUrl = null;

            }
        }

        catch (NullReferenceException ex)
        {
            Login1.DestinationPageUrl = "default.aspx";   
        }
        
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {

        bool IsValidUser = false;
        CommonFunctions h = new CommonFunctions();
        

        if (h.IsEmailAddress(Login1.UserName))
        {
            string Username = Membership.GetUserNameByEmail(Login1.UserName);
            IsValidUser = Membership.ValidateUser(Username, Login1.Password);
            if (IsValidUser)
            {
                Login1.UserName = Username;
            }
        }
        else
        {
            IsValidUser = Membership.ValidateUser(Login1.UserName, Login1.Password);
        }

        if (IsValidUser)
        {
            e.Authenticated = true; 
        }
        else
        {
            e.Authenticated = false;
        }
    }


}