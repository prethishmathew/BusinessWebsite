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


/// <summary>
/// Summary description for ProductionServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class ProductionServices : System.Web.Services.WebService {

    public ProductionServices () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]

    public string getAllDiscounts(string membershipID)// returns HTML Str
    {
        System.Text.StringBuilder str = new System.Text.StringBuilder();
        try
        {
            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(connStr))
            {

                using (SqlCommand cmd = new SqlCommand("bizDiscountDtls_Select"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", membershipID));
                    Conn.Open();
                    cmd.Connection = Conn;
                    string disID = null;
                    using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        str.Append("<div class='schemer'>");
                        
                        while (rdr.Read())
                        {

                            str.Append("<div class='postion1'>");
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDisCountID")))
                            {
                                disID = rdr.GetInt64(rdr.GetOrdinal("bizDisCountID")).ToString ();

                            }

                            /* PR-000029 - SI*/
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountHeader")))
                            {
                                str.Append("<div class='header'>" + rdr.GetString(rdr.GetOrdinal("bizDiscountHeader")).ToString() + "</div></br>");

                            }
                            /* PR-000029 - EI)*/

                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountDesc")))
                            {
                                string desc = rdr.GetString(rdr.GetOrdinal("bizDiscountDesc")).ToString();
                                if (desc.Length > 200)
                                {
                                    str.Append("<div class='desc'>" + desc.Substring(0,200) + "<a href='secPages/DiscountDetails.aspx?id=" + disID + "'  title='myRattles.com'  > View More </a>" + "</div></br>");
                                }
                                else
                                {
                                    str.Append("<div class='desc'>" + desc + "</div></br>");
                                }
                            }
                            /*
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountException1")))
                            {
                                xmlData = xmlData +
                                    "<DiscountExp1><![CDATA[" +
                                    rdr.GetString(rdr.GetOrdinal("bizDiscountException1")) +
                                    "]]></DiscountExp1>";
                            }
                        
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountStartdate")))
                            {
                                xmlData = xmlData +
                                    "<DiscountStartdate><![CDATA[" +
                                     Convert.ToString(rdr.GetDateTime(rdr.GetOrdinal("bizDiscountStartdate"))) +
                                    "]]></DiscountStartdate>";
                            }*/
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountExpirydate")))
                            {
                                str.Append("<div class='desc'> <b>Expiry date:" + rdr.GetDateTime(rdr.GetOrdinal("bizDiscountStartdate")).ToShortDateString() + "</b></div></br>");
                            }
                            str.Append("<a href='secPages/DiscountDetails.aspx?id=" + disID + "'  title='myRattles.com'  > View Details and Print </a> </br></br></div>");
                        }
                        str.Append("</div>");
                    }
                }

            }
            return str.ToString();
        }
        catch (Exception err)
        { return err.Message ; }

        
    }
    public string  getAllDiscountsXML(string membershipID)//returns xml 
    {
        try
        {

            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            string xmlData = "<?xml version='1.0'?> <xmlDiscountInfo>";
            using (SqlConnection Conn = new SqlConnection(connStr))
            {

                using (SqlCommand cmd = new SqlCommand("bizDiscountDtls_Select"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", membershipID));
                    Conn.Open();
                    cmd.Connection = Conn;
                    using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {

                        while (rdr.Read())
                        {
                            xmlData = xmlData + "<xmlDiscountDtls>";
                            
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDisCountID")))
                            {
                                xmlData = xmlData +
                                    "<DiscountID> <![CDATA[" +
                                Convert.ToString(rdr.GetInt64(rdr.GetOrdinal("bizDisCountID"))) + 
                                    "]]></DiscountID>";
                                
                            }

                            /* PR-000029 - SI*/
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountHeader")))
                            {
                                xmlData = xmlData +
                                    "<DiscountHeader> <![CDATA[" +
                                rdr.GetString(rdr.GetOrdinal("bizDiscountHeader")) +
                                    "]]></DiscountHeader>";

                            }
                            /* PR-000029 - EI)*/

                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountDesc")))
                            {
                                xmlData = xmlData +
                                    "<DiscountDesc><![CDATA[" +
                                    rdr.GetString(rdr.GetOrdinal("bizDiscountDesc")) +
                                    "]]></DiscountDesc>";
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountException1")))
                            {
                                xmlData = xmlData +
                                    "<DiscountExp1><![CDATA[" +
                                    rdr.GetString(rdr.GetOrdinal("bizDiscountException1")) +
                                    "]]></DiscountExp1>";
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountStartdate")))
                            {
                                xmlData = xmlData +
                                    "<DiscountStartdate><![CDATA[" +
                                     Convert.ToString(rdr.GetDateTime(rdr.GetOrdinal("bizDiscountStartdate"))) +
                                    "]]></DiscountStartdate>";
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountExpirydate")))
                            {
                                xmlData = xmlData +
                                    "<DiscountExpirydate><![CDATA[" +
                                    Convert.ToString(rdr.GetDateTime (rdr.GetOrdinal("bizDiscountExpirydate"))) +
                                    "]]></DiscountExpirydate>";
                            }
                            xmlData = xmlData + "</xmlDiscountDtls>";
                        }
                    }
                }

            }

            xmlData = xmlData + "</xmlDiscountInfo>";
            return xmlData;
        }
        catch ( Exception ex )
            {
                return ex.Message;
            
            }
    }

}

