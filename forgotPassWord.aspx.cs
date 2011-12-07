using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forgotPassWord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void PasswordRecovery1_OnVerifyingUser(object sender, LoginCancelEventArgs e)
    {
        try
        {
            CommonFunctions h = new CommonFunctions();
            if (h.IsEmailAddress(PasswordRecoveryAg.UserName))
            {
                PasswordRecoveryAg.UserName = Membership.GetUserNameByEmail(PasswordRecoveryAg.UserName);

            }
            
        }
        catch (Exception err)
        { 
        
        }
    }
}
