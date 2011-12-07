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
        if (!Page.IsPostBack)
        {
            ObjectDataSource1.SelectParameters["bizID"].DefaultValue = UserID; 
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

        ObjectDataSource1.Delete();
        GridView1_Bind();

    }

    public void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Int64 discountID;
            DateTime startDate;
            DateTime expiryDate;
            Int32 maxPrints;
            int index = e.RowIndex;
            GridViewRow row = GridView1.Rows[index];
            gridErrorMessage.Text = " ";
            //System.Windows.Forms.MessageBox.Show(e.NewValues[1].ToString());  
            //discountID = Convert.ToInt64(row.Cells[1].Text);
            string discountText = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblDisCntID")).Text;
            if (!Int64.TryParse(discountText, out discountID))
            {
                gridErrorMessage.Text = "Invalid Discount Id";
                e.Cancel = true;
            }

            //TextBox tb = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0];

            string Header = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtHeader")).Text;
            string Desc = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtDescription")).Text;
            string Excep = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtExceptions")).Text;


            if (string.IsNullOrEmpty(Header))
            {
                gridErrorMessage.Text = "Header cannot be empty";
                e.Cancel = true;
            }
            //        startDate = Convert.ToDateTime(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtStartDate")).Text);        
            if (!DateTime.TryParse(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtStartDate")).Text,
                out startDate))
            {
                gridErrorMessage.Text = "Unknown format for start date.  Reenter date in mm/dd/yyyy format. ";
                e.Cancel = true;
            }

            //expiryDate = Convert.ToDateTime(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtExpiryDate")).Text);

            if (!DateTime.TryParse(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtExpiryDate")).Text,
                out expiryDate))
            {
                gridErrorMessage.Text = "Unknown format for Expiry date. Reenter date in mm/dd/yyyy format. ";
                e.Cancel = true;
            }
            //maxPrints = Convert.ToInt32(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtMaxNumberofPrints")).Text);

            if (!Int32.TryParse(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtMaxNumberofPrints")).Text,
                out maxPrints))
            {
                gridErrorMessage.Text = "Maximum Prints should be a numeric number.";
                e.Cancel = true;
            }
            //Int32 curPrints = Convert.ToInt32(((Label)GridView1.Rows[e.RowIndex].FindControl("txtMaxNumberofPrints")).Text);
            //System.Windows.Forms.MessageBox.Show((Header + "<br>" + Desc + "<br>" + Excep + "<br>" + discountText);
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
                //ObjectDataSource1.UpdateParameters.Add("currentPrints", TypeCode.String, curPrints.ToString());
                //System.Windows.Forms.MessageBox.Show(Header + "<br>" + Desc + "<br>" + Excep + "<br>" + discountText);
                ObjectDataSource1.Update();
                //    GridView1_Bind();

            }
        }
        catch (Exception err)
        {

            //System.Windows.Forms.MessageBox.Show(err.InnerException.Message);
        }
    }

    public void GridView1_RowEditing(Object sender, GridViewEditEventArgs e)
    {

        GridView1.EditIndex = e.NewEditIndex;
        GridView1.DataBind();

    }

}
