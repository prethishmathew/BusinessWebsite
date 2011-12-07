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
using System.Windows.Forms; 

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.IsAuthenticated)       { 
       // Response.Redirect("BizUsers/inti1.aspx");  
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return default(string[]);
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
