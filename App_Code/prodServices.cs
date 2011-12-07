using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.Configuration;
using System.Web.Security;
/// <summary>
/// Summary description for prodServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class prodServices : System.Web.Services.WebService {

    public prodServices () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string getSrchResults(string zipOrCity,string distance, string bizDetails)
    {
        
        string ZipSqlStr =" ",returnHtmlTag = " ";
        
        if (string.IsNullOrEmpty(zipOrCity))
        {
        return "Error : Zip Code or City Cannot be empty";
        
        }

        if (string.IsNullOrEmpty(distance) | (!CommonFunctions.isNumeric(distance)))
        {
            return "Error : Invalid Value Entered for Distance";

        }

        
        zipCodeUtil zipper = new zipCodeUtil(zipOrCity,Convert.ToInt16(distance ))  ;

        

        if (zipper.RtnCode > 0)
        
        {
            return "Error : " + zipper.Rtnmessage  ;
        }
             
        int[] zipCodeList = zipper.getZipCodesInRange(Convert.ToInt16(distance));
        
        if (zipper.RtnCode > 0)
        {
            return "Error : " + zipper.Rtnmessage  ;
        }
        
        //ZipSqlStr = "'" + zipper.RadiusBoxZips[0].ToString() + "',";

        for (int i = 0; i < zipper.RadiusBoxZips.Count()-1; i++)
        {            
            ZipSqlStr += zipper.RadiusBoxZips[i].ToString() + ",";
            

        }
        
        ZipSqlStr += zipper.RadiusBoxZips[zipper.RadiusBoxZips.Count() - 1].ToString() ;

        //system.windows.forms.messagebox.Show(ZipSqlStr);     
        /* Check if ZipCode range is available on the Cache */

        string CacheName = "sqlDataset#" + zipper.ZipCode + "#" + zipper.Distance + "#" + bizDetails ;
        DataTable listTable = CommonFunctions.generateCacheDataTab(CacheName, ZipSqlStr, zipper.ZipCode, bizDetails);
        //system.windows.forms.messagebox.Show("Count is " + listTable.DefaultView.Count);
          

        if (listTable.DefaultView.Count > 0)
        {
            returnHtmlTag = generateListingPageHtml(listTable, 1);
            returnHtmlTag +=
            "<input type='hidden' id='bConDSTempName' value='" + CacheName + "' />";
            
        }
        else
        {
            returnHtmlTag = "No data found";
            
        }
             
        return returnHtmlTag;
    }

    private string generateListingPageHtml(DataTable dtTab, int PageCount)
    {
        
            
        int PageRecMin = (PageCount - 1) * 20;
        int PageRecMax = (PageCount * 20) ;
        string expression = "RowID >= " + PageRecMin + " AND " + "RowID < " + PageRecMax ;
        String rtndata;
            
        DataRow[] foundRows = dtTab.Select(expression);
        if (PageCount == 1)
        {
            rtndata = genTabNInitPage(foundRows, dtTab.DefaultView.Count);
        }
        else
        {
            rtndata = generateTabdataListing(foundRows);
        
        }
 
        return rtndata;
    }

    private string genTabNInitPage(DataRow[] tabRows,int recCount)
    {

        string Htmldata;
        int MaxPage;
        try 
        {
            if   (tabRows.Length == 0)        
            {
            Htmldata = "No Biz Available in the given search criteria ";
            return Htmldata ;       
            }             
            try         
            {
                MaxPage = (recCount / 20) + 1;        
            }        
            catch (System.OverflowException )        
            {            
                MaxPage = 100;
        
            }
        // Start Buliding the Frame and the 1st page        
        /*
         * 
         * <div id="taber">
         * 	<ul>
         *      <li><a href="#page1">1</a> </li>
         *      <li><a href="#page2">2</a> </li>
         *      <li><a href="#page3">3</a> </li>
         *      <li><a href="#page4">4</a> </li>
         *  </ul>
         *  <div id="page1" class="tabs-botton-content" >CONTENT </div>
         *  <div id="page2" class="tabs-botton-content" >CONTENT </div>
         *  <div id="page3" class="tabs-botton-content" >CONTENT </div>
         *  <div id="page4" class="tabs-botton-content" >CONTENT </div>         
         * </div>
         * 
         */

            Htmldata = "<div id='taber' class='tabs-bottom'><ul id='tabListing'>";        
            for (int i = 1; i <= MaxPage; i++)
            {
            Htmldata += "<li><a href='#page" + i + "' >" + i + "</a> </li>";        
            }        
            Htmldata += "</ul>";
            Htmldata += "<div id='page1' class='tabs-botton-content' > ";
            Htmldata += generateTabdataListing(tabRows );
            Htmldata += "</div>";

            for (int i = 2; i <= MaxPage; i++)
            {
                Htmldata += "<div id='page" + i + "' class='tabs-botton-content' > " +
                    "Loading Data <img src='Images/loading.gif' />" +
                    "</div>";
                     
                
            }
            return Htmldata ;
        
        }
        
        catch(Exception )
        {                
            return " No records found";
        }

    }

    private string generateTabdataListing(DataRow[] tabdatarow) {

        string tabHTML = "<table class='tblsSrch'><thead></thead><tfoot></tfoot><tbody>";

        for (int i = 0; i < tabdatarow.Length; i++)
        {
            
            string ID =  tabdatarow[i]["ID"].ToString ();
            
            string imgPath      = tabdatarow[i]["profileImgPathThumbNail"].ToString();
            string bizName      = tabdatarow[i]["bizName"].ToString();
            string address1     = tabdatarow[i]["address1"].ToString();
            string address2     = tabdatarow[i]["address2"].ToString();
            string City         = tabdatarow[i]["City"].ToString();
            string state        = tabdatarow[i]["state"].ToString();
            string zipCode      = tabdatarow[i]["zipCode"].ToString();
            string disXml       = tabdatarow[i]["bizDiscountXML"].ToString();
            if (string.IsNullOrWhiteSpace(City))
            {

                City = patRatesFunctions.CommonFunctions.getCityNState(zipCode);
                string[] arr = City.Split(',');
                City = arr[0];
                state = arr[1];
            }
            tabHTML += "<tr>";
            tabHTML += "<td> <a href='http://www.myRattles.com/showBiz.aspx?id=" + ID + "'><img alt='' src='" + tabdatarow[i]["profileImgPathThumbNail"].ToString() + "' /> </a>   </td>";
            tabHTML += "<td> <a href='http://www.myRattles.com/showBiz.aspx?id=" + ID + "'>" + bizName + " </a> <br/>" + address1 + "," + address2 + "<br/>" + City + "," + state + "," + zipCode + " </td>";
            tabHTML += generateDisCountListing(disXml, ID);
            tabHTML += "</tr>";
            
        }
        tabHTML += "</tbody></table>";
        return tabHTML;
    
    }

    private string generateDisCountListing(string xmlDiscount,string ID)
    {
        string returnElement = "<td><div class='ScrolldisCnt'> ";
        try
        {

            //System.Windows.Forms.MessageBox.Show("In Here1" + xmlDiscount);
            //XmlReader reader = XmlReader.Create(xmlDiscount);
            XmlTextReader reader = new XmlTextReader(new System.IO.StringReader(xmlDiscount )); 
            //System.Windows.Forms.MessageBox.Show("In Here2");
            while (reader.Read())
            {
                if (reader.MoveToContent() == XmlNodeType.Element && reader.Name == "discount")
                {
                    //System.Windows.Forms.MessageBox.Show("In Here3");
                    string disCountDetails = reader.ReadString();
                    if (disCountDetails.Length > 72)
                    {
                         disCountDetails = disCountDetails.Substring(0,72) ;
                    }
                    

                    returnElement += "<div > " + disCountDetails +
                                        "<br> <a href='http://www.myRattles.com/showBiz.aspx?id=" + ID + "'> More..</a></div>";


                }

            }
            
            
        }
        catch(Exception err)
        {
            System.Windows.Forms.MessageBox.Show(err.Message);     
        
        }
        returnElement += "</div></td>";
        return returnElement;
    }
    [WebMethod]
    public string getAdditionalPage(string tempTabs, string pageNum)
    {
        try
        {

            DataTable listTable = (DataTable)HttpRuntime.Cache.Get(tempTabs);

            if (listTable == null)
            {
                string[] zipdetails = tempTabs.Split('#');
                string zCode = zipdetails[1];
                string zDistance = zipdetails[2];
                string zbizType = zipdetails[3];

                if (string.IsNullOrEmpty(zCode))
                {
                    return "Error : Invalid Input data or unsafe data detected - EGAP19001";

                }

                if (string.IsNullOrEmpty(zDistance) | (!CommonFunctions.isNumeric(zDistance)))
                {
                    return "Error : Invalid Input data or unsafe data detected - EGAP19002";

                }


                zipCodeUtil zipper = new zipCodeUtil(zCode, Convert.ToInt16(zDistance));



                if (zipper.RtnCode > 0)
                {
                    return "Error : Invalid Input data or unsafe data detected - EGAP19003";
                }

                int[] zipCodeList = zipper.getZipCodesInRange(Convert.ToInt16(zDistance));

                if (zipper.RtnCode > 0)
                {
                    return "Error : Invalid Input data or unsafe data detected - EGAP19004";
                }

                //ZipSqlStr = "'" + zipper.RadiusBoxZips[0].ToString() + "',";
                string ZipSqlStr = " ";
                for (int i = 0; i < zipper.RadiusBoxZips.Count() - 1; i++)
                {
                    ZipSqlStr += zipper.RadiusBoxZips[i].ToString() + ",";


                }

                ZipSqlStr += zipper.RadiusBoxZips[zipper.RadiusBoxZips.Count() - 1].ToString();

                //system.windows.forms.messagebox.Show(ZipSqlStr);     
                /* Check if ZipCode range is available on the Cache */

                string CacheName = "sqlDataset#" + zipper.ZipCode + "#" + zipper.Distance + "#" + zbizType;
                listTable = CommonFunctions.generateCacheDataTab(CacheName, ZipSqlStr, zipper.ZipCode, zbizType);

            }

            return generateListingPageHtml(listTable, Convert.ToInt16(pageNum));

        }
        catch (Exception err) {

            return err.Message;

        }
    }

    [WebMethod]
    public string deleteInvdoc(string zipOrCity)
    {
          
        String ConnStr = string.Empty;
        ConnStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;  
        //ConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
        string sqlStatement = "bizInv_DelInvDoc";
        SqlConnection dbConnection = new SqlConnection(ConnStr);
        dbConnection.Open();
        SqlCommand dbcommand = new SqlCommand(sqlStatement, dbConnection);
        dbcommand.CommandType = CommandType.StoredProcedure;
        dbcommand.Parameters.Add("@ID", SqlDbType.VarChar, 50);
        dbcommand.Parameters["@ID"].Value = Membership.GetUser().ProviderUserKey.ToString();  
        dbcommand.ExecuteNonQuery();
        dbConnection.Close();
        return "Success";
    
    }
}

