using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class masterShowBizmaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /* 
         * This Line is being added as the JavaScript Resolve code block was changed from
         *  <%= to <%# 
         
         */

        Page.Header.DataBind();
        Page.DataBind();
        // Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 60 + 10) ));
        /**********************************************************************************/
    }


    protected void Login2_Authenticate(object sender, AuthenticateEventArgs e)
    {
        bool IsValidUser = false;
        CommonFunctions h = new CommonFunctions();

        //   MessageBox.Show("in Here"); 
        if (h.IsEmailAddress(Login2.UserName))
        {
            string Username = Membership.GetUserNameByEmail(Login2.UserName);
            //     MessageBox.Show(Username); 
            IsValidUser = Membership.ValidateUser(Username, Login2.Password);
            if (IsValidUser)
            {
                Login2.UserName = Username;
            }

        }
        else
        {
            //   MessageBox.Show(Login2.UserName);  
            IsValidUser = Membership.ValidateUser(Login2.UserName, Login2.Password);
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
