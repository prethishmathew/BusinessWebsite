using System;
using System.Collections;
using System.Configuration;
using System.Web.Configuration;  
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;

//using System.Windows.Forms;
 
public partial class BizUsers_bizinfo : System.Web.UI.Page
{
    String UserID = Membership.GetUser().ProviderUserKey.ToString();
   // discountUtility disCnt = new discountUtility ();
   

    public void invHeader_Update(Object sender, EventArgs e)
    {
       // System.Windows.Forms.MessageBox.Show(Request.Params.Get("__EVENTTARGET"));     
    }
    /*
    public void GridView1_Bind()
    {
        //GridView1.DataSourceID  = "ObjectDataSource1";
        try
        {
            //DataTable dtDis = disCnt.SelectDiscount(UserID);
  
        }
        catch (Exception err)
        {

            System.Windows.Forms.MessageBox.Show(err.InnerException.Message  );   
        }
    }
    
    public void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)

    {        
        int index = e.RowIndex ;        
        GridViewRow row = GridView1.Rows[index];
        //Console.WriteLine(row.Cells[0].Text);
        //TextBox2.Text = row.Cells[1].Text;
        
        string discountText = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblDisCntID")).Text;
        //System.Windows.Forms.MessageBox.Show(discountText);    
        Int64 discountID = Convert.ToInt64(discountText);
        ObjectDataSource1.DeleteParameters.Clear();
        ObjectDataSource1.DeleteParameters.Add("bizDiscountID", TypeCode.Int64, discountID.ToString());
        ObjectDataSource1.DeleteParameters.Add("bizID",TypeCode.String , UserID.ToString() );
        
   //   ObjectDataSource1.DeleteParameters["bizDiscountID"].DefaultValue = "1234";
   //   ObjectDataSource1.DeleteParameters["bizID"].DefaultValue = UserID;

        ObjectDataSource1.Delete();
        GridView1_Bind();

    }

    public void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs  e)
    
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
            System.Windows.Forms.MessageBox.Show(Header + "<br>" + Desc + "<br>" + Excep + "<br>" + discountText);
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
                System.Windows.Forms.MessageBox.Show(Header + "<br>" + Desc + "<br>" + Excep + "<br>" + discountText);
                ObjectDataSource1.Update();
                //    GridView1_Bind();
            
            }
        }
        catch (Exception err)
        {

            System.Windows.Forms.MessageBox.Show(err.InnerException.Message  );    
        }
    }

    public void GridView1_RowEditing(Object sender, GridViewEditEventArgs e) {

        GridView1.EditIndex = e.NewEditIndex;
        GridView1.DataBind(); 
    
    }*/

    protected void Create_InvList(string listName, SqlXml sqlXMLValue, PlaceHolder placeHolderName,int listNumber)
   
    {
                 
                Button button = new Button();
                button.ID = "btnSaveList" + listNumber ;

                /* 
                 *
                 * No Need of Hidden Feild to store user id. This is handled in the webservice        
                 * Start Comment
                
                       HiddenField userIdHidden = new HiddenField() ;
                        userIdHidden.ID = "whoIsUser";
                        userIdHidden.Value  = Membership.GetUser().ProviderUserKey.ToString ();
                        updateInventoryList(listName)
                        //"javascript:return WebForm_OnSubmit();" 
        
                 * 
                 * End Comment
                 * 
                 */


                button.OnClientClick = "javascript:return updateInventoryList(" + listNumber + ");";
                button.Text = "Save List";
                
          /*      LinkButton link = new LinkButton ();
                link.ID = "linkCreateNewList";
                link.Text = "Create New List";
                link.OnClientClick = "javascript:return updateInventoryList(" + listNumber + ");";
          */

        /* Delete Inventory List Button */
                Button delButton = new Button();
                delButton.ID = "btnDelList" + listNumber;
                delButton.OnClientClick = "javascript:return delInventoryList(" + listNumber + ");";
                delButton.Text = "Delete List";

        /*                              */

        /* Menu Button for Inventory Lists  */
        /*  Button Cocept replaced by Jqquery Tabs 
                Button invContainerBtn = new Button();
                invContainerBtn.ID = "btnInvContainer" + listNumber;
                invContainerBtn.OnClientClick = "javascript:return animateInventoryMenu(" + listNumber + ");";
                invContainerBtn.CssClass = "btnActive";        

                if (CommonFunctions.CvtNullSpaces(listName )=="NAS")
                {
                    invContainerBtn.Text = "List" + listNumber ;
                }
                    else
                {
                    invContainerBtn.Text = CommonFunctions.CvtNullSpaces(listName);
                }
        */
        
        /*     Create Tab Headers for Jquery Tab UI */

               Literal invtabLI = new Literal() ;
               invtabLI.Text = "<li><a href='#UpdatePanel" + listNumber + "'><span>" + CommonFunctions.CvtNullSpaces(listName) + "</span></a></li> ";
        
        /*                                  */
                TextBox textBoxList = new TextBox();
                textBoxList.ID = "txtBoxListName" + listNumber; 
                textBoxList.Text = CommonFunctions.CvtNullSpaces(listName );
                textBoxList.Width = 200;                   
                LiteralControl literalBreakList = new LiteralControl("<br /><br /> items List:");
                LiteralControl literalBreakList1 = new LiteralControl("<br /><br /> ");
          /*      placeHolderName.Controls.Add(link);*/
                literalBreakList.Text = "Heading for this section:";

                
                placeHolderName.Controls.Add(literalBreakList);
                placeHolderInvContainer.Controls.Add(invtabLI);
                placeHolderName.Controls.Add(textBoxList);
                placeHolderName.Controls.Add(button);
                placeHolderName.Controls.Add(delButton);

                placeHolderName.Controls.Add(literalBreakList1);

                Label labelItem = new Label() ;
                labelItem.Text = "Product Name";
                labelItem.Width  = 255;
                labelItem.Style.Add("text-align", "center");
                labelItem.Style.Add("font-weight", "bolder");    
                placeHolderName.Controls.Add(labelItem);

                Label labelDesc = new Label();
                labelDesc.Text = "Description";
                
                labelDesc.Width = 300;
                labelDesc.Style.Add("text-align", "center");
                labelDesc.Style.Add("font-weight", "bolder");    
                placeHolderName.Controls.Add(labelDesc);

                Label labelCost = new Label();
                labelCost.Text = "Price/Cost";
                labelCost.Width = 50;
                labelCost.Style.Add ("text-align", "center");
                labelCost.Style.Add("font-weight", "bolder");    
                placeHolderName.Controls.Add(labelCost);

                /*literalBreakList1.Text = "<br /><br /> &nbsp;&nbsp;&nbsp;&nbsp;"
                    + "Item Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    + "Description&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cost/Price";*/

                
                
        

                int i = 0;
                int row = 0;


                if (!sqlXMLValue.IsNull)
                {
                    XmlReader xmlreader = sqlXMLValue.CreateReader();
                
                while (xmlreader.Read())
                {
                    if (xmlreader.MoveToContent() == XmlNodeType.Element && xmlreader.Name == "item")
                    {
                        i += 1;
                        row += 1;
                         
                        TextBox textBox = new TextBox();
                        textBox.ID = "invTextItem" + listNumber + i.ToString();
                        textBox.TextMode = TextBoxMode.MultiLine;
                        textBox.Rows = 2;
                        textBox.Text = xmlreader.ReadString();
                        textBox.CssClass = "bizInvItem";  
                        LiteralControl literalBreak = new LiteralControl("<br />");
                        placeHolderName.Controls.Add(literalBreak);
                        placeHolderName.Controls.Add(textBox); 
                    }
                    if (xmlreader.MoveToContent() == XmlNodeType.Element && xmlreader.Name == "desc")
                    {
                        i += 1;
                        TextBox textBox = new TextBox();
                        textBox.ID = "invTextItem" + listNumber + i.ToString();
                        textBox.TextMode = TextBoxMode.MultiLine;
                        textBox.Rows = 2; 
                        textBox.Text = xmlreader.ReadString();
                        textBox.CssClass = "bizInvDesc";
                        placeHolderName.Controls.Add(textBox);
                    }
                    if (xmlreader.MoveToContent() == XmlNodeType.Element && xmlreader.Name == "cost")
                    {
                        i += 1;
                        TextBox textBox = new TextBox();
                        textBox.ID = "invTextItem" + listNumber + i.ToString();
                        textBox.CssClass = "bizInvCost";
                        textBox.Rows = 2;
                        textBox.Text = xmlreader.ReadString();
                        textBox.TextMode = TextBoxMode.MultiLine;
                        placeHolderName.Controls.Add(textBox);
                    }
                    
                    }
                xmlreader.Close();  
                }
                row += 1;
                i += 1;
                if (row  < 21)
                {
                    for (int cnter = row ; cnter < 21; cnter++)
                    {
                        
                        LiteralControl literalBreak = new LiteralControl("<br />");
                        placeHolderName.Controls.Add(literalBreak);
                        TextBox textBox = new TextBox();
                        textBox.ID = "invTextItem" + listNumber + i.ToString();
                        textBox.TextMode = TextBoxMode.MultiLine;
                        textBox.Rows = 2;
                        textBox.CssClass = "bizInvItem";
                        placeHolderName.Controls.Add(literalBreak);
                        
                        placeHolderName.Controls.Add(textBox);
                        i += 1;
                        TextBox textBox1 = new TextBox();
                        textBox1.ID = "invTextItem" + listNumber + i.ToString();
                        textBox1.TextMode = TextBoxMode.MultiLine;
                        textBox1.Rows = 2;                        
                        textBox1.CssClass = "bizInvDesc";
                        placeHolderName.Controls.Add(textBox1);
                        i += 1;
                        TextBox textBox2 = new TextBox();
                        textBox2.ID = "invTextItem" + listNumber + i.ToString();
                        textBox2.CssClass = "bizInvCost";
                        textBox2.Rows = 2;
                        textBox2.TextMode = TextBoxMode.MultiLine;
                        placeHolderName.Controls.Add(textBox2);
                        i += 1;

                    }
                }
    }

    protected void Page_Load(object sender, EventArgs e)

    {
         
        Response.Cache.SetExpires(DateTime.Parse(DateTime.Now.ToString()));
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        /*if (!IsPostBack)*/
         
            
        dynamicLoad_metaData();
        
        if (!IsPostBack)
        {
            
            Profile_Preload();
        }
        
        String ConnStr = string.Empty;
        ConnStr = ConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
      //  String UserID = Membership.GetUser().ProviderUserKey.ToString();

        
      /*  String SqlStatement = "SELECT docPath,listName1,xmlProductDtls1, "
                + " listName2,xmlProductDtls2,listName3,xmlProductDtls3, "
                + " listName4,xmlProductDtls4,listName5,xmlProductDtls5 "
                + " FROM bizInventoryDtls WHERE "
                + " ID = '"
             //   + Membership.GetUser().ProviderUserKey.ToString() + "'";    
               + UserID + "'";*/
        string SqlStatement = "bizInventoryDtls_Select";        
        using (SqlConnection connection =
                new SqlConnection(ConnStr))
        {
            SqlCommand command = new SqlCommand(SqlStatement, connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar, 50));
            command.Parameters["@ID"].Value = UserID;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                reader.Read();
                //TextBox1.Text = Membership.GetUser().ProviderUserKey.ToString();
                // 1st Listdata point
                if (!reader.IsDBNull(2))
                {
                    SqlXml sqlXmlValue = reader.GetSqlXml(2);
                    Create_InvList(CommonFunctions.isDbReaderNull(reader, 1), sqlXmlValue, placeHolderA1, 1);
                }

                // 2nd datalist point
                if (!reader.IsDBNull(4))
                {
                    SqlXml sqlXmlValueMultiple = reader.GetSqlXml(4);
                    Create_InvList(CommonFunctions.isDbReaderNull(reader, 3), sqlXmlValueMultiple, placeHolderB1, 2);
                }

                // 3rd datalist point
                if (!reader.IsDBNull(6))
                {
                    SqlXml sqlXmlValueMultiple1 = reader.GetSqlXml(6);
                    Create_InvList(CommonFunctions.isDbReaderNull(reader, 5), sqlXmlValueMultiple1, placeHolderC1, 3);
                }

                // 4th datalist point
                if (!reader.IsDBNull(8))
                {
                    SqlXml sqlXmlValueMultiple2 = reader.GetSqlXml(8);
                    Create_InvList(CommonFunctions.isDbReaderNull(reader, 7), sqlXmlValueMultiple2, placeHolderD1, 4);
                }

                // 5th datalist point
                if (!reader.IsDBNull(10))
                {
                    SqlXml sqlXmlValueMultiple3 = reader.GetSqlXml(10);
                    Create_InvList(CommonFunctions.isDbReaderNull(reader, 9), sqlXmlValueMultiple3, placeHolderE1, 5);
                }

                if (!Page.IsPostBack)
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("invHeader")))
                    {

                        invHeaderTxtBox.Text = reader.GetValue(reader.GetOrdinal("invHeader")).ToString();
                    }
                }
                if (!reader.IsDBNull(reader.GetOrdinal("docPath")))
                    
                {
                        inventoryFile.Text = "<a href='" + reader.GetValue(reader.GetOrdinal("docPath")).ToString() + "'>Inventory Document</a>";
                        BtnDeleteInvDoc.Visible = true;   
                }
                else
                {
                    inventoryFile.Text = " ";
                    BtnDeleteInvDoc.Visible = false;
                }
            }
           // else
           // {
                //TextBox1.Text = "No Rows";
           // }
                      connection.Close();
                       
        }
        
    // For Discount Operations -- Thanks    
        //ObjectDataSource1.SelectParameters.Clear();  
        //ObjectDataSource1.SelectParameters.Add("bizID", DbType.String, UserID);
        //ObjectDataSource1.Select();
    }

    public void bizDisDynum_load(object sender, EventArgs e)
    
    {
          
           if (IsPostBack &&  
              Request.Params.Get("__EVENTTARGET") =="ctl00_ContentPlaceHolder1_bizDisDynum")  
          {
              /*GridView1.DataBind();*/
            
          }
        
    }

    protected void Profile_Preload() {

        /* LOADS the profile data , Graphics Information 
         * Read from table bizProfile and update the profile information section of the 
         * bizinfo.aspx page.
         * 
         * 
         */

       // Build theme data

        //DataSet ds = CommonFunctions.getDSfromXML("webThemes", "XmlData/webThemes.xml");
        DataSet ds = CommonFunctions.getDSfromXML("webTheme", "../XmlData/webThemes.xml");
        if (ds == null)
        {
            ds = new DataSet();
            ds.ReadXml(Server.MapPath("../XmlData/webThemes.xml"));  
        }
        webTheme.DataSource = ds;
        webTheme.DataTextField = "themeName";
        webTheme.DataValueField = "themePath";

        webTheme.SelectedValue = "jquery-ui-blue-custom.css";
        webTheme.DataBind();
 

        String ConnStr = string.Empty;
        ConnStr = ConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

        string SQLStr = "bizInfo_SelectProfileNGraphics";
        using (SqlConnection connection =
                new SqlConnection(ConnStr))
        {
            try
            {
                SqlCommand command = new SqlCommand(SQLStr, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID", SqlDbType.VarChar, 50);
                command.Parameters["@ID"].Value = UserID;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    reader.Read();
                    if (!reader.IsDBNull(reader.GetOrdinal("fName")))
                    {

                        fNameTxt.Text = reader.GetValue(reader.GetOrdinal("fName")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("lName")))
                    {

                        lNameTxt.Text = reader.GetValue(reader.GetOrdinal("lName")).ToString();
                    }


                    if (!reader.IsDBNull(reader.GetOrdinal("bizName")))
                    {

                        bizNameTxt.Text = reader.GetValue(reader.GetOrdinal("bizName")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("bizCat")))
                    {

                        bizCatgryTxt.Text = reader.GetValue(reader.GetOrdinal("bizCat")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("bizSubCat")))
                    {

                        bizSubCatgryTxt.Text = reader.GetValue(reader.GetOrdinal("bizSubCat")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("address1")))
                    {

                        add1Txt.Text = reader.GetValue(reader.GetOrdinal("address1")).ToString();
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("address2")))
                    {

                        add2Txt.Text = reader.GetValue(reader.GetOrdinal("address2")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("zipCode")))
                    {

                        zipTxt.Text = reader.GetValue(reader.GetOrdinal("zipCode")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("bizWebsite")))
                    {
                        extWebSiteTxt.Text = reader.GetValue(reader.GetOrdinal("bizWebsite")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("bizCaption")))
                    {

                        capTxt.Text = reader.GetValue(reader.GetOrdinal("bizCaption")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("bizDesc")))
                    {

                        bizDesTxt.Text = reader.GetValue(reader.GetOrdinal("bizDesc")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("contactPhone")))
                    {

                        phoneTxt.Text = reader.GetValue(reader.GetOrdinal("contactPhone")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("contactEmail")))
                    {

                        emailTxt.Text = reader.GetValue(reader.GetOrdinal("contactEmail")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("workhrsstart")))
                    {

                        regOpenTime.Text = reader.GetValue(reader.GetOrdinal("workhrsstart")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("workhrsclose")))
                    {

                        regCloseTime.Text = reader.GetValue(reader.GetOrdinal("workhrsclose")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("lunchhrstart")))
                    {

                        lunFromTime.Text = reader.GetValue(reader.GetOrdinal("lunchhrstart")).ToString();
                    }


                    if (!reader.IsDBNull(reader.GetOrdinal("lunchhrend")))
                    {

                        lunToTime.Text = reader.GetValue(reader.GetOrdinal("lunchhrend")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("splDay1")))
                    {

                        DropDownListSplDay1.SelectedItem.Value = reader.GetValue(reader.GetOrdinal("splDay1")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("splTime1From")))
                    {

                        splTime1From.Text = reader.GetValue(reader.GetOrdinal("splTime1From")).ToString();
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("splTime1To")))
                    {

                        splTime1To.Text = reader.GetValue(reader.GetOrdinal("splTime1To")).ToString();
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("splDay2")))
                    {

                        DropDownListSplDay2.SelectedItem.Value = reader.GetValue(reader.GetOrdinal("splDay2")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("splTime2From")))
                    {
                        splTime2From.Text = reader.GetValue(reader.GetOrdinal("splTime2From")).ToString();
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("splTime2To")))
                    {
                        splTime2To.Text = reader.GetValue(reader.GetOrdinal("splTime2To")).ToString();
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("closedays")))
                    {

                        string[] cloDays = reader.GetValue(reader.GetOrdinal("closedays")).ToString().Split(';');
                        DropDownListClosedDays1.SelectedItem.Value = cloDays[0];
                        DropDownListClosedDays2.SelectedItem.Value = cloDays[1];
                        DropDownListClosedDays3.SelectedItem.Value = cloDays[2];
                        DropDownListClosedDays4.SelectedItem.Value = cloDays[3];

                    }
                    char[] charTotrim = { '#' };
                    if (!reader.IsDBNull(reader.GetOrdinal("bizbackGroundColor")))
                    {
                        bgColorText.Text = reader.GetValue(reader.GetOrdinal("bizbackGroundColor")).ToString().Trim(charTotrim);
                    }


                    if (!reader.IsDBNull(reader.GetOrdinal("bizfontColor")))
                    {
                        fgColorText.Text = reader.GetValue(reader.GetOrdinal("bizfontColor")).ToString().Trim(charTotrim);
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("bizTheme")))
                    {
                        webTheme.SelectedValue = reader.GetValue(reader.GetOrdinal("bizTheme")).ToString();
                    }

                    /* Changes for Uploading Background Image Changes */ 

                /*    if (!reader.IsDBNull(reader.GetOrdinal("bizBgImagePath"))) ---Moved Changes to a new page.
                    {
                        Img1Image.ImageUrl = reader.GetValue(reader.GetOrdinal("bizBgImagePath")).ToString();
                        Img1Image.Visible = true;
                        repeatImgDropList.SelectedValue = reader.GetValue(reader.GetOrdinal("bizBgImageRepeat")).ToString();
                        BtnUploadImg1.Visible = false;
                        BtnDelImg1.Visible = true;

                    }

                    else
                    {
                        BtnUploadImg1.Visible = true;
                        BtnDelImg1.Visible = false;
                    }*/   
                }
                reader.Close();
                connection.Close();
            }
            catch (SqlException ex)
            {
                     //   System.Windows.Forms.MessageBox.Show("1" + ex.Message );    
                connection.Close();
            }
            catch (Exception exp)
            {
                     //System.Windows.Forms.MessageBox.Show("2" + exp.Message);    
                connection.Close();
            }
            finally
            {
                connection.Close(); 
            }
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
            HtmlLink link = new HtmlLink();
            link.Href = "~/Styles/jquery-ui-.custom.css";
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            this.Header.Controls.Add(link);
            
            HtmlLink masterLink = (HtmlLink)(Master.FindControl("dynamicCss"));
            masterLink.Href = "~/Styles/jquery-ui-blue-custom.css";
            /* Code ends Here 
            */
        }
        catch (Exception err)
        {


            System.Windows.Forms.MessageBox.Show(err.Message);
        }
    }

    protected void Page_PreInit(object sender, EventArgs e) {
        /*try
        {
            if (IsPostBack)
            {

                if (UpdatePanel1.IsInPartialRendering   )
                {
                    UpdatePanel1.Update();
                }

                if (UpdatePanel2.IsInPartialRendering)
                {
                    UpdatePanel2.Update();
                }

                if (UpdatePanel3.IsInPartialRendering)
                {
                    UpdatePanel3.Update();
                }
                if (UpdatePanel4.IsInPartialRendering)
                {
                    UpdatePanel4.Update();
                }
            }
        }
        catch (Exception err)
        {
            TextBox1.Text = err.Message;
        }
        /*
        { 

            UpdatePanel2.Update();
            UpdatePanel3.Update();
            UpdatePanel4.Update();
            UpdatePanel1.DataBind();
            UpdatePanel2.DataBind();
            UpdatePanel3.DataBind();
            UpdatePanel4.DataBind();
        } */

    }

    protected void invHeadingSave_Click(object sender, EventArgs e)
    {
             
        if (Page.IsPostBack)
        {
            string rtn = amazonServices.sendInvDoc2Cloud(InvDocUpload, invHeaderTxtBox.Text);
            if (!rtn.StartsWith("Error"))
            {
                inventoryFile.Text = "<a href='" + rtn +"'>Click to view Inventory Doc..</a>";
                BtnDeleteInvDoc.Visible = true;  
            }
        }

    }
    
    
    
}

