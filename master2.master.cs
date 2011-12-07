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

  
public partial class master2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*string postBackStr = Page.ClientScript.GetPostBackEventReference(this.Page, "Mydata");
        */
        /* This Line is being added as the JavaScript Resolve code block was changed from
         *  <%= to <%# */
        try
        {
            Page.Header.DataBind();
            Page.DataBind();
            
            Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 60 + 10)));
            /*
             * This Code is to get the current page name and then accordingly place the 
             * dynamic CSS in the page 
             *
             * 
             * 
             * 
             * 
             * */
            //Button lnk = (Button)LoginView1.FindControl("pageLinkBizOwner");
            string currentPage = System.IO.Path.GetFileName(Request.Path);
            if (currentPage == "Default.aspx" | currentPage == "default.aspx")
            {
                LoginViewMidAds.Visible = true;

            }

            //if (currentPage == "login.aspx" && Request.QueryString["ReturnUrl"] !=null)
            //{
              //      int ind = Request.QueryString["ReturnUrl"].LastIndexOf(@"/");
            /*if (currentPage == "login.aspx" && Request.QueryString["ReturnUrl"].Substring(ind + 1).ToString() == "disCounts.aspx")
            {
                LoginView1.Visible = false;
                LoginViewLeftAds.Visible = false;

            }*/
            //}
            //{
                //HtmlControl iframeRattles = (HtmlControl) LoginViewLeftAds.FindControl("Iframed");
                     
                //System.Windows.Forms.MessageBox.Show(iframeRattles.ClientID);     
               
                //HtmlControl iframeRattles = (HtmlControl)Master.FindControl("Iframed");
                //System.Windows.Forms.MessageBox.Show(iframeRattles.ID);     
                //iframeRattles.Attributes["src"] = ResolveUrl("~/BizUsers/bizPicsShow.aspx");   
            //}
        }
        catch (Exception err)
        {

            //System.Windows.Forms.MessageBox.Show("in mast" + err.Message );
        }
        }

    protected void showBiz_dynamicLoad() {

        /*THIS CODE HAS BEEN MOVED TO SHOWBIZ onPageLoad()
         * Register JavaScript. Also Add Dynamic CSS File over Here
         * 
        try
        {
            String csname = "dynumScript";
            String csurl = "~/Scripts/showBiz.js";
            Type cstype = this.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;

            // Check to see if the include script exists already.
            if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
            {
                cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
            }
 
          //  Page.RegisterClientScriptBlock("dynamicJScript",
          // "<script type='text/javascript' src='/BusinessWebsite/Scripts/showBiz.js'>");
        }
        catch (Exception err) {

            System.Windows.Forms.MessageBox.Show(err.Message);    
        }*/
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
