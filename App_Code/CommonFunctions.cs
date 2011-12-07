using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient ;
using System.Text;
using System.Collections.Generic;   
using System.Text.RegularExpressions;
using System.Net;   

/// <summary>
/// Summary description for CommonFunctions
/// </summary>
public class CommonFunctions
{
    public CommonFunctions()
    {

        //
        // TODO: Add constructor logic here
        //
    }
    public bool IsEmailAddress(string Email)
    {

        if (Email.Contains("@") & Email.Contains("."))
        { return true; }
        else
            return false;
    }

    public TextBox createDynumTextBox(string textBoxID)
    {
        TextBox textBox = new TextBox();
        textBox.ID = textBoxID;
        return textBox;
    }

    public static string CvtNullSpaces(object passValue)
    {
        if (passValue == System.DBNull.Value || passValue.ToString().Trim().Length == 0)
        {
            passValue = "NAS";
        }

        return passValue.ToString();
    }

    public static string isDbReaderNull(SqlDataReader readerNull, int ordinalNum)
    {

        if (readerNull.IsDBNull(ordinalNum))
        {
            return "Not Defined";

        }
        else
            return readerNull.GetString(ordinalNum);
    }


    public static bool isValidDate(string dateParse)
    {

        bool isValidDate = true;

        try
        {
            DateTime.Parse(dateParse);
        }
        catch (FormatException fe)
        {
            isValidDate = false;

        }

        return isValidDate;


    }
    public static bool IsImage(HttpPostedFile file)
    {
        
        if (file != null && Regex.IsMatch(file.ContentType, "image/\\S+") &&
            file.ContentLength > 0)
        {
            return true;
        }

        return false;
    }

    public static bool IsValidDoc(HttpPostedFile file)
    {

        int MaxSize = 900 * 1024;
        List<string> fileMimeType = new List<string>();

        fileMimeType.Add("application/msword");
        fileMimeType.Add("application/doc");
        fileMimeType.Add("appl/text");
        fileMimeType.Add("application/vnd.msword");
        fileMimeType.Add("application/vnd.ms-word");
        fileMimeType.Add("application/winword");
        fileMimeType.Add("application/word");
        fileMimeType.Add("application/x-msw6");
        fileMimeType.Add("application/x-msword");
        fileMimeType.Add("application/pdf");
        fileMimeType.Add("application/vnd.openxmlformats-officedocument.wordprocessingml.document");


        if (file != null && fileMimeType.Contains(file.ContentType) &&
            file.ContentLength > 0 && file.ContentLength < MaxSize)
        {
            return true;
        }

        return false;
    }

    public static string getUserLocation(string userName)
    {
        string ZippyCode = " ";
        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
        using (SqlConnection con =
                     new SqlConnection(connStr))
        {
            string sqlstatement = "SELECT zipCodeLocation FROM " +
                " userLocationTable WHERE ID = '" +
                userName + "'";
            //System.Windows.Forms.MessageBox.Show(sqlstatement);     
            SqlCommand cmd = new SqlCommand(sqlstatement, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                ZippyCode = CommonFunctions.isDbReaderNull(reader, reader.GetOrdinal("zipCodeLocation")).ToString();
            }

            reader.Close();
            con.Close();
        }

        return ZippyCode;
    }

    public static int initDatabaseForNewSurfer(string userName, string zipcdeLoc)
    {

        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
        using (SqlConnection con =
                     new SqlConnection(connStr))
        {

            SqlCommand cmd = new SqlCommand("InitDBSetup4Surfer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@bizID", SqlDbType.VarChar, 50));
            cmd.Parameters.Add(new SqlParameter("@zipCde", SqlDbType.Char, 5));
            cmd.Parameters["@bizID"].Value = Membership.GetUser(userName).ProviderUserKey.ToString();
            cmd.Parameters["@zipCde"].Value = zipcdeLoc.Substring(0, 5);
            try
            {
                con.Open();
                int rtncode = cmd.ExecuteNonQuery();
                con.Close();
                return rtncode;
            }
            catch (SqlException sqlEx)
            {

                con.Close();
                return 99;
            }
            catch (Exception ex)
            {

                con.Close();
                return 98;
            }
            finally
            {

                con.Close();

            }
        }

    }


    public static int initDatabaseForNewRegistry(string userName, string zipcdeLoc)
    {

        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

        using (SqlConnection con =
                new SqlConnection(connStr))
        {

            SqlCommand cmd = new SqlCommand("InitDBSetup", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@bizID", SqlDbType.VarChar, 50));
            cmd.Parameters.Add(new SqlParameter("@zipCde", SqlDbType.Char, 5));
            cmd.Parameters["@bizID"].Value = Membership.GetUser(userName).ProviderUserKey.ToString();
            if (string.IsNullOrEmpty(zipcdeLoc))
            {
                cmd.Parameters["@zipCde"].Value = null;
            }
            else
            {
                cmd.Parameters["@zipCde"].Value = zipcdeLoc.Substring(0, 5);
            }
            try
            {
                con.Open();
                int rtncode = cmd.ExecuteNonQuery();
                con.Close();
                return rtncode;
            }
            catch (SqlException sqlEx)
            {

                con.Close();
                return 99;
            }
            catch (Exception ex)
            {

                con.Close();
                return 98;
            }
            finally
            {

                con.Close();

            }
        }
    }

    public static string getZipcode(string cityState)
    {

        try
        {
            string[] cityStateArray = cityState.Split(',');
            XDocument convertBooks = (XDocument)HttpContext.Current.Cache.Get("xmlCityStateconvertBooks");

            if (convertBooks == null)
            {

                string xmlLocation = HttpContext.Current.Server.MapPath("~/XmlData/Convertbooks.xml");
                //xmlLocation = VirtualPathUtility.ToAbsolute("~/XmlData/Convertbooks.xml");   
                convertBooks = XDocument.Load(xmlLocation);
                HttpContext.Current.Cache.Insert("xmlCityStateconvertBooks",
                    convertBooks,
                    new System.Web.Caching.CacheDependency(xmlLocation),
                    System.DateTime.MaxValue,
                    TimeSpan.Zero,
                    System.Web.Caching.CacheItemPriority.Default,
                    null);
            }

            var apps = (from book in convertBooks.Descendants("zip")
                        where book.Element("city").Value.Equals(cityStateArray[0])
                        && book.Element("state").Value.Equals(cityStateArray[1])
                        orderby book.Element("city").Value
                        select new
                        {
                            cityZip = book.Attribute("code").Value

                        }).Distinct();



            if (apps.First().cityZip.Length > 0)
            {
                return apps.First().cityZip.ToString();
            }
            else
            {
                return "Invalid";
            }

        }
        catch (HttpException htpex)
        {
            return "htpex";
        }
        catch (Exception excp)
        {
            return excp.Message;
        }
    }


    public static bool isNumeric(string strTextEntry)
    {

        if (null == strTextEntry)
            return false;


        Regex objNotWholePattern = new Regex("[^0-9]");


        return !objNotWholePattern.IsMatch(strTextEntry)
         && (strTextEntry != "");


    }

    public static bool validCityStateFormat(string inpStr)
    {

        Regex objNotWholePattern = new Regex(@"[a-zA-Z]{1,}\s*[a-zA-Z]*,[a-zA-Z]{2}");

        return objNotWholePattern.IsMatch(inpStr)
         && (inpStr != "");


    }

    public static string standardizeCity(string inpCity)
    {
        inpCity = inpCity.ToLower();
        StringBuilder SB = new StringBuilder(inpCity);
        SB[0] = Char.ToUpper(SB[0]);

        /* Remove spaces at the beginning of the String */

        for (int a = 0; a < SB.Length; a++)
        {
            if (Char.IsWhiteSpace(SB[a]) == true)
            {
                SB[a + 1] = Char.ToUpper(SB[a + 1]);
            }
        }
        SB[0] = Char.ToUpper(SB[0]);
        inpCity = SB.ToString();

        return inpCity;


    }

    public static XDocument generateXdoc(string cacheName, string docPath)
    {
        try
        {
            XDocument xmlDoc = (XDocument)HttpRuntime.Cache.Get(cacheName);
            if (xmlDoc == null)
            {
                string xmlLocation =
                    HttpContext.Current.Server.MapPath(docPath);
                xmlDoc = XDocument.Load(xmlLocation);
                HttpRuntime.Cache.Insert(cacheName,
                    xmlDoc,
                    new System.Web.Caching.CacheDependency(xmlLocation),
                    System.DateTime.MaxValue,
                    TimeSpan.Zero,
                    System.Web.Caching.CacheItemPriority.Default,
                    null);
            }

            return xmlDoc;
        }
        catch (Exception)
        {
            //System.Windows.Forms.MessageBox.Show("cache error " + err.Message);     
            return null;
        }
    }

    public static DataTable generateCacheDataTab(string cacheName, string zipSqlStr, string zipCode, string bizName)
    {

        DataTable dtTable = (DataTable)HttpRuntime.Cache.Get(cacheName);

        if (dtTable == null)
        {

            String ConnStr = string.Empty;
            ConnStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection connection =
                new SqlConnection(ConnStr))
            {
                SqlCommand command = new SqlCommand("bizDenormalizedDtls_SelectMultiple", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@zipCodeList", SqlDbType.VarChar, 5000));
                command.Parameters["@zipCodeList"].Value = zipSqlStr;
                command.Parameters.Add(new SqlParameter("@centreZip", SqlDbType.VarChar, 10));
                command.Parameters["@centreZip"].Value = zipCode;
                if (!string.IsNullOrEmpty(bizName))
                {
                    command.Parameters.Add(new SqlParameter("@bizzName", SqlDbType.VarChar, 500));
                    command.Parameters["@bizzName"].Value = bizName;

                }

                /*connection.Open();
                */
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;

                DataSet ds = new DataSet();
                da.Fill(ds, "srchDetls");
                //DataView dtt = ds.Tables["srchDetls"].DefaultView;
                dtTable = ds.Tables["srchDetls"];

                //Add Primary Key to the datatable;

                UniqueConstraint pkey = new UniqueConstraint("PK_RowNum", dtTable.Columns["RowID"], true);
                dtTable.Constraints.Add(pkey);

                HttpRuntime.Cache.Insert(cacheName,
                    dtTable,
                    null,
                    System.DateTime.MaxValue,
                    TimeSpan.Zero,
                    System.Web.Caching.CacheItemPriority.Default,
                    null);

            }
        }
        return dtTable;

    }

    public static string charCleanUp(string strRem)
    {

        strRem = strRem.Replace("'", "");
        strRem = strRem.Replace("\"", "");
        strRem = strRem.Replace(";", "");
        return strRem;
    }

    public static DataSet getDSfromXML(string cacheName, string XMLFilePath)
    {

        DataSet ds = (DataSet)HttpRuntime.Cache.Get(cacheName);

        if (ds == null)
        {
            ds = new DataSet();
            ds.ReadXml(HttpContext.Current.Server.MapPath(XMLFilePath));
            HttpRuntime.Cache.Insert(cacheName,
                    ds,
                    null,
                    System.DateTime.MaxValue,
                    TimeSpan.Zero,
                    System.Web.Caching.CacheItemPriority.Default,
                    null);

        }

        return ds;

    }

    public static bool getURLResponse(string urlPath)
    {

        HttpWebRequest URLReq;
        HttpWebResponse URLRes;
        try
        {
            URLReq = (HttpWebRequest)WebRequest.Create(urlPath);
            URLReq.Timeout = 5000;
            URLRes = (HttpWebResponse)URLReq.GetResponse();
            if (URLRes.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }



    }

    public static DataTable getFansOrShopsDtXml(string cacheName, string storedProcedure,string tableType)
    {
        
        DataTable dataTab = (DataTable)HttpRuntime.Cache.Get(cacheName);

        if (dataTab == null)
        {
            DataSet ds = new DataSet() ;
            dataTab = new DataTable() ;
            String ConnStr = string.Empty;
            ConnStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection connection =
                new SqlConnection(ConnStr))
            {
                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("ID", SqlDbType.VarChar, 5000));
                command.Parameters["ID"].Value = Membership.GetUser().ProviderUserKey.ToString() ;

                /*testdata
                command.Parameters["ID"].Value = "111";*/

                connection.Open(); 
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    //System.Windows.Forms.MessageBox.Show("Test - 2");
                    dr.Read();
                    ds.ReadXml(dr.GetSqlXml(0).CreateReader() );
                    dataTab = ds.Tables[tableType]; 
                }
                connection.Close(); 
            }

            if (dataTab.Rows.Count > 0)
            {
                HttpRuntime.Cache.Insert(cacheName,
                        dataTab,
                        null,
                        System.DateTime.MaxValue,
                        TimeSpan.Zero,
                        System.Web.Caching.CacheItemPriority.Default,
                        null);
            }

            /*foreach (DataRow row in dataTab.Rows)
            {
                System.Windows.Forms.MessageBox.Show(row[0].ToString() + "''" + row[1].ToString() + "''" + row[2].ToString());
            }*/

             
        }
        return dataTab;
    }

    public static void cacheRemove(string cacheName)
   {
       HttpRuntime.Cache.Remove(cacheName );
   
   }
/*bracket for the Class Do NOt delete */
}


