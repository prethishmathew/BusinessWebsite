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

public partial class registerNew : System.Web.UI.Page
{

    protected void CreateUserWizard1_CreateUserError(object sender, CreateUserErrorEventArgs e)
    {
        //Response.Redirect("customError.aspx?Error=" + e.CreateUserError + "### CreateUser");

    }

    protected void CreateUserWizard1_sendEmailError(object sender, SendMailErrorEventArgs e
)
    {
        //System.Windows.Forms.MessageBox.Show("mail--" + e.Exception.Message); 
        Response.Redirect("customError.aspx?Error=" + e.Exception.Message + "### SendEmail");

    }
    protected void Page_Load(object sender, EventArgs e)
    {
/*        if (!IsPostBack)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("XmlData/xmlSecurityWizard.xml"));
            DropDownList Question = (DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Question");
            Question.DataSource = ds;
            Question.DataTextField = "securityQuestion";
            Question.DataValueField = "securityQuestion";
            Question.SelectedValue = "Please Select a Question";
            Question.DataBind();
        }*/
    }
    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        try
        {

            CheckBox isBizUserChkBox = (CheckBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("isBizUserChkBox");
            Label LabelCompleteMessage = (Label)CompleteStepMessage.ContentTemplateContainer.FindControl("LabelCompleteMessage");
            Label LabelCompleteUserName = (Label)CompleteStepMessage.ContentTemplateContainer.FindControl("LabelCompleteUserName");
            Label LabelCompleteEmail = (Label)CompleteStepMessage.ContentTemplateContainer.FindControl("LabelCompleteEmail");
            TextBox Location = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("location");
            if (isBizUserChkBox.Checked)
            {
                Roles.AddUserToRole(CreateUserWizard1.UserName, "bizOwner");
                string zipCodeLoc = CommonFunctions.getZipcode(Location.Text);
                int rtnCode = CommonFunctions.initDatabaseForNewRegistry(CreateUserWizard1.UserName, zipCodeLoc);

                if (rtnCode > 90)
                {
                    Membership.DeleteUser(CreateUserWizard1.UserName);
                    LabelCompleteMessage.Text = "Error creating User ID. Please start again!!!";
                    LabelCompleteUserName.Text = " ";
                    LabelCompleteEmail.Text = " ";

                }
                else
                {
                    LabelCompleteMessage.Text = "A new Business Owner User ID has been created for you!!!";
                    LabelCompleteUserName.Text = CreateUserWizard1.UserName;
                    LabelCompleteEmail.Text = CreateUserWizard1.Email;
                }
            }
            else
            {
                Roles.AddUserToRole(CreateUserWizard1.UserName, "surfer");
                string zipCodeLoc = CommonFunctions.getZipcode(Location.Text);
                int rtnCode = CommonFunctions.initDatabaseForNewSurfer(CreateUserWizard1.UserName, zipCodeLoc);
                LabelCompleteMessage.Text = "Your User ID has been created !!!!";
                LabelCompleteUserName.Text = CreateUserWizard1.UserName;
                LabelCompleteEmail.Text = CreateUserWizard1.Email;

            }
        }
        catch (Exception err)
        {
            //Response.Redirect("customError.aspx?Error=" + err.Message + "####CreateUserMAin");
            //System.Windows.Forms.MessageBox.Show (err.Message);    
        }
    }
    

    protected void loadDefaultPage(object sender, EventArgs e)
    {
       
        
        if (User.IsInRole("surfer"))
        {
        Response.Redirect("Default.aspx");
        }

        if (User.IsInRole("bizOwner"))
        {
            Response.Redirect("BizUsers/bizinfo.aspx");
        }
    }

}
