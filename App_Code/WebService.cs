using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security; 
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Configuration;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using System.Data;
using System.Data.Sql; 
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld(string tryne) {
        string results;
        results = "Hello World" + tryne;
        results = Membership.GetUser().ProviderUserKey.ToString();    
        return results;
    }
    [WebMethod]
    public string[] citySuggestion(string prefixText, int count) 
    {
        
 
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        string [] results = new string[5]  ;
        
        results.SetValue("Sagar", 0); 
        results.SetValue("Hold",1);
        results.SetValue("Name", 2);
        results.SetValue("None", 3);
        results.SetValue("Prads", 4);
        results[0] = prefixText; 
        
        return results ; 
    }

    [WebMethod]
    public string[] cityAutoSuggestion(string prefixText, int count)
    {
        XPathNavigator nav;
        XPathDocument docNav;
        XPathNodeIterator NodeIter;
        string Strexpression;
        int i=0;
        string[] results = new string[count];
        docNav = new XPathDocument(@"c:\books.xml");
        nav = docNav.CreateNavigator();

        Strexpression =  "/bookstore/book/title[../price>10.00]";
        Strexpression = "//author";
        NodeIter = nav.Select(Strexpression);

        do 
        {
            i++;
            NodeIter.MoveNext();
            //Console.WriteLine("Book Title: {0}", NodeIter.Current.Value);
            results.SetValue(NodeIter.Current.Value, i - 1); 
        } while( i < count) ;
        return results;
 
 
    }

    [WebMethod]
  
    public string[] cityStateDrop1(string prefixText, int count) {

        
        /*Fore runner to the cityStatedrop function. Not in use as this does not have the 
         Cache Implemented for the XMl files.
         * 
         */
        string[] results = new string[15] { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ",
                                                " ", " ", " ", " ", " " };
        
        int i = 0;
        
        string prefixCharU;
        prefixCharU = prefixText.ToLower();
        StringBuilder SB = new StringBuilder(prefixCharU);
        SB[0] = Char.ToUpper(SB[0])  ;

        for (int a = 0; a < SB.Length;a++ )
        {
            if (Char.IsWhiteSpace(SB[a]) == true) {
                SB[a + 1] = Char.ToUpper(SB[a + 1]);
            }
        }
        SB[0] = Char.ToUpper(SB[0]);
        prefixCharU = SB.ToString();
        

       // XDocument xmlDoc = XDocument.Load(@"C:\Users\SweetyPradish\Documents\Business Plan\Convertbooks.xml");
        string xmlLocation = Server.MapPath("XmlData/Convertbooks.xml");
        XDocument xmlDoc = XDocument.Load(xmlLocation);
        var apps = (from book in xmlDoc.Descendants("zip")
                    where book.Element("city").Value.StartsWith(prefixCharU)
                    orderby book.Element("city").Value
                    select new
                    {
                        city = book.Element("city").Value,
                        state = book.Element("state").Value
                    }).Distinct();
        
        
        foreach (var book in apps)
        {
            i++;
            results.SetValue((book.city + "," + book.state), i - 1);
            if (i > 14) {
                break;
            } 
        }
        
        
        return results;
    }


    [WebMethod]

    public string[] cityStateDrop(string prefixText, int count)
    {

        string[] results = new string[15] { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ",
                                                " ", " ", " ", " ", " " };

        int i = 0;
        /*
        string prefixCharU;
        
        prefixCharU = prefixText.ToLower();
        StringBuilder SB = new StringBuilder(prefixCharU);
        SB[0] = Char.ToUpper(SB[0]);

        

        for (int a = 0; a < SB.Length; a++)
        {
            if (Char.IsWhiteSpace(SB[a]) == true)
            {
                SB[a + 1] = Char.ToUpper(SB[a + 1]);
            }
        }
        SB[0] = Char.ToUpper(SB[0]);
        prefixCharU = SB.ToString();*/

        string prefixCharU = CommonFunctions.standardizeCity(prefixText);   

        // XDocument xmlDoc = XDocument.Load(@"C:\Users\SweetyPradish\Documents\Business Plan\Convertbooks.xml");
        //string xmlLocation = Server.MapPath("XmlData/Convertbooks.xml");
        //XDocument xmlDoc = XDocument.Load(xmlLocation);
        
        //XDocument xmlDoc = (XDocument)HttpContext.Current.Cache.Get("xmlCityStateconvertBooks"); //Removed HTTPCONTEXT
        XDocument xmlDoc = CommonFunctions.generateXdoc("xmlCityStateconvertBooks", "XmlData/Convertbooks.xml");
        /*XDocument xmlDoc = (XDocument)HttpRuntime.Cache.Get("xmlCityStateconvertBooks");
        
        if (xmlDoc == null)
        {
            string xmlLocation =  
                HttpContext.Current.Server.MapPath("XmlData/Convertbooks.xml");
            xmlDoc = XDocument.Load(xmlLocation);
            HttpRuntime.Cache.Insert("xmlCityStateconvertBooks",    
            //HttpContext.Current.Cache.Insert("xmlCityStateconvertBooks",
                xmlDoc,
                new System.Web.Caching.CacheDependency(xmlLocation),
                System.DateTime.MaxValue,
                TimeSpan.Zero,
                System.Web.Caching.CacheItemPriority.Default,
                null);

        }
        */
        if (xmlDoc != null)
        {
            var apps = (from book in xmlDoc.Descendants("zip")
                        where book.Element("city").Value.StartsWith(prefixCharU)
                        orderby book.Element("city").Value
                        select new
                        {
                            city = book.Element("city").Value,
                            state = book.Element("state").Value
                        }).Distinct();


            foreach (var book in apps)
            {
                i++;
                results.SetValue((book.city + "," + book.state), i - 1);
                if (i > 14)
                {
                    break;
                }
            }

            if (i > 0)
            {
                return results;
            }
            else
                return null;
        }
        else
            return null;
    }


    
    
    [WebMethod]

    public string updateInventoryList(string xmlData, int listNum,string listName)
    {
        try
        {
        string userID = Membership.GetUser().ProviderUserKey.ToString();
        
        string sqlStatement = " Update [bizInventoryDtls] Set " +
            "xmlProductDtls" + listNum + "='" + xmlData +
            "', listName" + listNum + "='" + listName +
            "' Where ID = '" + userID + "'" ;
        
        String ConnStr = string.Empty;
        ConnStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
        
        SqlConnection dbConnection = new SqlConnection(ConnStr);
        dbConnection.Open(); 
        SqlCommand dbcommand = new SqlCommand(sqlStatement, dbConnection);  
        return dbcommand.ExecuteNonQuery().ToString() ;
         
        }
        catch (System.Web.HttpUnhandledException      Err )
        {return Err.Message    ;
        }
    }
    

    [WebMethod]

    public int deleteInventoryList(int listNum)
    {

        string userID = Membership.GetUser().ProviderUserKey.ToString();

        string sqlStatement = " Update [bizInventoryDtls] Set " +
            "xmlProductDtls" + listNum + "=NULL" +
            ", listName" + listNum + "=NULL"  +
            " Where ID = '" + userID + "'";

        String ConnStr = string.Empty;
        ConnStr = ConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

        SqlConnection dbConnection = new SqlConnection(ConnStr);
        dbConnection.Open();
        SqlCommand dbcommand = new SqlCommand(sqlStatement, dbConnection);

        dbcommand.ExecuteNonQuery();
        return listNum;

    }


    [WebMethod]

    public string getListNumber()
    {
        

        string sqlStatement = "SELECT docPath,listName1,xmlProductDtls1, "
                + " listName2,xmlProductDtls2,listName3,xmlProductDtls3, "
                + " listName4,xmlProductDtls4,listName5,xmlProductDtls5 "
                + " FROM bizInventoryDtls WHERE "
                + " ID = '"
                + Membership.GetUser().ProviderUserKey.ToString() + "'";

        String  ConnStr = string.Empty;
        ConnStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
        
        using (SqlConnection connection =
                new SqlConnection(ConnStr))
        {
            SqlCommand command = new SqlCommand(sqlStatement, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                if (reader.IsDBNull(2))
                {
                    return ("1");
                }

                else if (reader.IsDBNull(4))
                {
                    return ("2");
                }
                else if (reader.IsDBNull(6))
                {
                    return ("3");
                }
                else if (reader.IsDBNull(8))
                {
                    return ("4");
                }
                else
                {
                    return ("5");
                }
            }
            else return ("6");
        }        
    }

    [WebMethod]

    public int updateGraphicsData(string bgColor, string fgcolor, string webTheme)
    {
        
        try
        {
        
            Regex colorChk = new Regex(@"^#?([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$" );

            if (!colorChk.IsMatch(bgColor))
            {
                return 1;
            }
  
            if (!colorChk.IsMatch(fgcolor))
            {
                return 2;
            }

            char[] toTrim = { '#' };

            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("bizGraphics_Update", con);
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@colorBg", SqlDbType.VarChar,50  ));
            cmd.Parameters.Add(new SqlParameter("@colorFg", SqlDbType.VarChar, 50));
            cmd.Parameters.Add(new SqlParameter("@webTheme", SqlDbType.VarChar, 50));
            cmd.Parameters.Add(new SqlParameter("@userID", SqlDbType.VarChar, 50));

            cmd.Parameters["@colorBg"].Value = "#" + bgColor.Trim(toTrim)   ;
            cmd.Parameters["@colorFg"].Value = "#" + fgcolor.Trim(toTrim);
            cmd.Parameters["@webTheme"].Value = webTheme;
            cmd.Parameters["@userID"].Value = Membership.GetUser().ProviderUserKey.ToString();
            try
            {
                con.Open();
              int rtncode =  cmd.ExecuteNonQuery();
              con.Close();

                //Execute Non Query will return the number of Rows affected ;
              if (rtncode == 1)
              {
                  return 0;

              }
              else
              {
                  return 3; 
              }

            }
            catch (SqlException)
            {
                return 3;
            }
            finally
            {
                con.Close();
            }
                    
        
        }
        catch(Exception )
        {
        
        return 4;
        }
  
        
        return 0;
    }

    [WebMethod]

    public string updateProfileData(string xmlInput)

    /*
     *updateProfileData: 
     * 
     * This Para will update the Profile data on the bizProfile table using Sql statements.
     * Input : XML String that contains the data
     *         XMl Schema is 
     *         <?xml version='1.0'?> 
     *         <profileCatalog> 
     *         <data1></data1>  
     *         <data2></data2>  
     *         </profileCatalog>  
     * 
     *Logic : The SQL is Built based on the input XML string and then the update command is  
     *      execute using execute NonQuery. SQLUpper is the Dynamic Part and SQlWhere has 
     *      WHERE clause.
     */

    {
        
        try
        {

            String SQLUpper = "Update [bizProfile] Set";
            String SQlWhere = " lastUpdateTime = '" + DateTime.Now.ToString ()   +
                                "' WHERE ID = '" + Membership.GetUser().ProviderUserKey.ToString() + "'";
            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            
            doc.LoadXml(xmlInput);
            XmlNodeList nodeList = doc.DocumentElement.ChildNodes;

            
            foreach (XmlNode node in nodeList)
            {
                
                if (node.NodeType == XmlNodeType.Element)
                {
                    switch (node.Name)
                    {
                        case "Name1":
                            SQLUpper = SQLUpper + " fName = '" + CommonFunctions.charCleanUp(node.InnerText)  + "',";
                            break;
                        case "Name2":
                            SQLUpper = SQLUpper + " lName = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "copName":
                            SQLUpper = SQLUpper + " bizName = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "copCat":
                            SQLUpper = SQLUpper + " bizCat = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "copSubCat":
                            SQLUpper = SQLUpper + " bizSubCat = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "copDesc":
                            SQLUpper = SQLUpper + " bizDesc = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "copCaption":
                            SQLUpper = SQLUpper + " bizCaption = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        // NR0023 - SA
                        case "copWeb":
                            SQLUpper = SQLUpper + " bizWebsite = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        //NR0023 - EA
                        case "add1":
                            SQLUpper = SQLUpper + " address1 = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "add2":
                            SQLUpper = SQLUpper + " address2 = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "zipCode":
                            SQLUpper = SQLUpper + " zipCode = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "Phone":
                            SQLUpper = SQLUpper + " contactPhone = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "Email":
                            SQLUpper = SQLUpper + " contactEmail = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "workhrsstart":
                            SQLUpper = SQLUpper + " workhrsstart = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "workhrsclose":
                            SQLUpper = SQLUpper + " workhrsclose = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "lunchhrstart":
                            SQLUpper = SQLUpper + " lunchhrstart = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "lunchhrend":
                            SQLUpper = SQLUpper + " lunchhrend = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "splDay1":
                            SQLUpper = SQLUpper + " splDay1 = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "splTime1From":
                            SQLUpper = SQLUpper + " splTime1From = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "splTime1To":
                            SQLUpper = SQLUpper + " splTime1To = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "splDay2":
                            SQLUpper = SQLUpper + " splDay2 = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "splTime2From":
                            SQLUpper = SQLUpper + " splTime2From = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                        case "splTime2To":
                            SQLUpper = SQLUpper + " splTime2To = '" + CommonFunctions.charCleanUp(node.InnerText) + "',";
                            break;
                            // No Char Clean Up for Close days because its delimiter is ';'.
                        case "closedays":
                            SQLUpper = SQLUpper + " closedays = '" + node.InnerText + "',";
                            break;
                    }
                }
            }

            
            String sqlStatement = SQLUpper + SQlWhere;
            String ConnStr = string.Empty;

            ConnStr = ConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

            SqlConnection dbConnection = new SqlConnection(ConnStr);
            dbConnection.Open();
            SqlCommand dbcommand = new SqlCommand(sqlStatement, dbConnection);

            dbcommand.ExecuteNonQuery();
             
            dbConnection.Close();  

        }
        catch (XmlException xmlEx)
        {

            return "WM97008";
        }

        catch (SqlException sqlEx)
        {
//            System.Windows.Forms.MessageBox.Show   (sqlEx.Message);      
            return "WM970012";
            
        }

        catch (Exception ex)
        {
            return "WM970016";
        }
        
        return "0";
    }

    [WebMethod]

    public string[] getBizCategory(string prefixText, int count)
    {
        string[] results = new string[15] { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ",
                                                " ", " ", " ", " ", " " };
        
        int i = 0;
        string prefixCharU;
        prefixCharU = prefixText.ToLower();
        StringBuilder SB = new StringBuilder(prefixCharU);
        SB[0] = Char.ToUpper(SB[0]);

        /* Remove Leading spaces */

        for (int a = 0; a < SB.Length; a++)
        {
            if (Char.IsWhiteSpace(SB[a]) == true)
            {
                SB[a + 1] = Char.ToUpper(SB[a + 1]);
            }
        }
        
        /* Convert to lower Case */

        prefixCharU = SB.ToString().ToLower() ;
        XDocument xmlDoc = CommonFunctions.generateXdoc("xmlbizCatInfo", "XmlData/bizCatInfo.xml");
        
        /*XDocument xmlDoc = (XDocument)HttpRuntime.Cache.Get("xmlbizCatInfo");*/
        /*if (xmlDoc == null)
        {
            string xmlLocation =
                HttpContext.Current.Server.MapPath("XmlData/bizCatInfo.xml");
            xmlDoc = XDocument.Load(xmlLocation);
            HttpRuntime.Cache.Insert("xmlbizCatInfo",
                //HttpContext.Current.Cache.Insert("xmlCityStateconvertBooks",
                xmlDoc,
                new System.Web.Caching.CacheDependency(xmlLocation),
                System.DateTime.MaxValue,
                TimeSpan.Zero,
                System.Web.Caching.CacheItemPriority.Default,
                null);

        }
        */
        if (xmlDoc != null)
        {
            var apps = (from book in xmlDoc.Descendants("bizdlts")
                        where book.Element("bizcat").Value.Contains(prefixCharU)
                        orderby book.Element("bizcat").Value
                        select new
                        {
                            bizCategory = book.Element("bizcat").Value

                        }).Distinct();


            foreach (var book in apps)
            {
                i++;
                results.SetValue((book.bizCategory), i - 1);
                if (i > 14)
                {
                    break;
                }
            }


            if (i > 0)
            {
                return results;
            }
            else
                return null;

            
        }
        else
            return null;
    
    }


  
} 