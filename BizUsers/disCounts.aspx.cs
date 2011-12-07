using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BizUsers_disCounts : System.Web.UI.Page
{
    string UserID = Membership.GetUser().ProviderUserKey.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.DataSourceID = ObjectDataSource1.ID;
        dynamicLoad_metaData();
        if (!IsPostBack)
        {
            ObjectDataSource1.SelectParameters["bizID"].DefaultValue = UserID;
            //GridView1.DataSource = ObjectDataSource1;  
            GridView1.DataBind();  
        }


    }


    protected void dynamicLoad_metaData()
    {

        try
        {
            String csname = "CommonScript";
            String csurl = "~/Scripts/bizinfo.js";
            Type cstype = this.GetType();
            // System.Windows.Forms.MessageBox.Show("er1");
            // Get a ClientScriptManager reference from the Page class.

            ClientScriptManager cs = Page.ClientScript;

            // Check to see if the include script exists already.
            if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
            {
                cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
                //   System.Windows.Forms.MessageBox.Show("er2");
            }

            /*******************************************************************************
             * 
             * CODE TO DYNAMICALLY LOAD CSS FILES TO WEBPAGE
             * 
             * CODE starts Here --
             */
            
        }
        catch (Exception err)
        {


            System.Windows.Forms.MessageBox.Show(err.Message);
        }
    }
    public void GridView1_Bind()
    {
        //GridView1.DataSourceID  = "ObjectDataSource1";
        try
        {
            //DataTable dtDis = disCnt.SelectDiscount(UserID);

        }
        catch (Exception err)
        {

            //System.Windows.Forms.MessageBox.Show((err.InnerException.Message);
        }
    }

    public void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        GridViewRow row = GridView1.Rows[index];
        //Console.WriteLine(row.Cells[0].Text);
        //TextBox2.Text = row.Cells[1].Text;

        string discountText = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblDisCntID")).Text;
        //System.Windows.Forms.MessageBox.Show(discountText);    
        Int64 discountID = Convert.ToInt64(discountText);
        ObjectDataSource1.DeleteParameters.Clear();
        ObjectDataSource1.DeleteParameters.Add("bizDiscountID", TypeCode.Int64, discountID.ToString());
        ObjectDataSource1.DeleteParameters.Add("bizID", TypeCode.String, UserID.ToString());

        //   ObjectDataSource1.DeleteParameters["bizDiscountID"].DefaultValue = "1234";
        //   ObjectDataSource1.DeleteParameters["bizID"].DefaultValue = UserID;

        //   ObjectDataSource1.Delete();
        //   GridView1_Bind();

    }

    public void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            
            Int64 discountID;
            DateTime startDate;
            DateTime expiryDate;
            Int32 maxPrints;
            Decimal  oPrice;
            Decimal cPrice;
            int index = e.RowIndex;
            GridViewRow row = GridView1.Rows[index];
            gridErrorMessage.Text = " ";
            
            string discountText = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblDisCntID")).Text ;

            if (!Int64.TryParse(discountText, out discountID))
            {
                gridErrorMessage.Text = "Invalid Discount Id";
                e.Cancel = true;
            }

            
            string Header = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtHeader")).Text ;
            string Desc = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtDescription")).Text;
            string Excep = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtExceptions")).Text;
            Label errLabel = ((Label)GridView1.Rows[e.RowIndex].FindControl("errorTxt")); 

            if (string.IsNullOrEmpty(Header))
            {
                errLabel.Text = "Header cannot be empty";
                e.Cancel = true;
            }
            if (!DateTime.TryParse(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtStartDate")).Text,
                out startDate))
            {
                errLabel.Text = "Unknown format for start date.  Reenter date in mm/dd/yyyy format. ";
                e.Cancel = true;
            }

            
            if (!DateTime.TryParse(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtExpiryDate")).Text,
                out expiryDate))
            {
                errLabel.Text = "Unknown format for Expiry date. Reenter date in mm/dd/yyyy format. ";
                e.Cancel = true;
            }

            if (startDate > expiryDate)
            {
                errLabel.Text = "Start date cannot be lesser than end date";
                e.Cancel = true;
            }


            if (!Int32.TryParse(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtMaxNumberofPrints")).Text,
                out maxPrints))
            {
                errLabel.Text = "Maximum Prints should be a numeric number.";
                e.Cancel = true;
            }
            if (!Decimal.TryParse(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtOrgPrice")).Text,
                out oPrice ))
            {
                errLabel.Text = "Original Price should be a numeric number.";
                e.Cancel = true;
            }
            if (!Decimal.TryParse(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCpnPrice")).Text,
                out cPrice ))
            {
                errLabel.Text = "Coupon Should be a numeric number";
                e.Cancel = true;
            }


            if (!e.Cancel)
            {
                ObjectDataSource1.UpdateParameters.Clear();
                ObjectDataSource1.UpdateParameters.Add("bizDiscountID", TypeCode.Int64, discountID.ToString());
                ObjectDataSource1.UpdateParameters.Add("bizID", TypeCode.String, UserID.ToString());
                ObjectDataSource1.UpdateParameters.Add("header", TypeCode.String, Header.ToString());
                ObjectDataSource1.UpdateParameters.Add("description", TypeCode.String, Desc.ToString());
                ObjectDataSource1.UpdateParameters.Add("exceptions", TypeCode.String, Excep.ToString());
                ObjectDataSource1.UpdateParameters.Add("startDate", TypeCode.String, startDate.ToString());
                ObjectDataSource1.UpdateParameters.Add("expiryDate", TypeCode.String, expiryDate.ToString());
                ObjectDataSource1.UpdateParameters.Add("maxNumberofPrints", TypeCode.String, maxPrints.ToString());
                ObjectDataSource1.UpdateParameters.Add("orgPrice", TypeCode.String , oPrice.ToString());
                ObjectDataSource1.UpdateParameters.Add("cpnPrice", TypeCode.String, cPrice.ToString());
            
            }
            
        }
        catch (Exception err)
        {

            System.Windows.Forms.MessageBox.Show(err.InnerException.Message);
        }
    }

    public void GridView1_RowEditing(Object sender, GridViewEditEventArgs e)
    {

        GridView1.EditIndex = e.NewEditIndex;
        GridView1.DataBind(); 


    }

}
