using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;
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



/// <summary>
/// Summary description for disCntApps
/// </summary>
[WebService(Namespace = "http://www.contoso.com/")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 

[System.Web.Script.Services.ScriptService]

public class disCntApps : System.Web.Services.WebService {

    public disCntApps () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public int InsertDiscountForApps(string header,string description, string exceptions, string startDate, string expiryDate, string maxNumberofPrints,string orgPrice,string cpnPrice)
    {
        Int32 maxNumberOfPrintsInt;
        DateTime startDateDateTime;
        DateTime expiryDateDateTime;
        Decimal orgPriceInt;
        Decimal cpnPriceInt;

        
        string bizID = Membership.GetUser().ProviderUserKey.ToString();
        string sqlDatabase = "eastManDB";

        if (!DateTime.TryParse(startDate, out startDateDateTime))
        {

            return 1;
        }

        if (!DateTime.TryParse(expiryDate, out expiryDateDateTime))
        {
            return 2;
        }
        /*Change the return code */
        if (startDateDateTime > expiryDateDateTime)
        {
            return 2;
        }

        if (!Int32.TryParse(maxNumberofPrints, out maxNumberOfPrintsInt))
        {

            return 3;
        }

        if (string.IsNullOrEmpty(header))
        {
            return 4;
        }
        if (string.IsNullOrEmpty(description))
        {
            return 5;
        }
        if (!Decimal.TryParse(orgPrice, out orgPriceInt))
        {

            return 6;
        }

        if (!Decimal.TryParse(cpnPrice, out cpnPriceInt))
        {

            return 7;
        }

        String ConnStr = string.Empty;
        ConnStr = ConfigurationManager.ConnectionStrings[sqlDatabase].ConnectionString;

        SqlConnection con = new SqlConnection(ConnStr);
        SqlCommand cmd = new SqlCommand("DiscountInsert", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@bizID", SqlDbType.VarChar, 50));
        cmd.Parameters.Add(new SqlParameter("@header", SqlDbType.VarChar, 100));
        cmd.Parameters.Add(new SqlParameter("@descrition", SqlDbType.Text));
        cmd.Parameters.Add(new SqlParameter("@exceptions", SqlDbType.Text));
        cmd.Parameters.Add(new SqlParameter("@startDate", SqlDbType.DateTime));
        cmd.Parameters.Add(new SqlParameter("@expirydate", SqlDbType.DateTime));
        cmd.Parameters.Add(new SqlParameter("@maxnumberofCoupons", SqlDbType.Int, 5));
        cmd.Parameters.Add(new SqlParameter("@orgPrice", SqlDbType.Decimal));
        cmd.Parameters.Add(new SqlParameter("@cpnPrice", SqlDbType.Decimal ));
        
        //cmd.Parameters.Add(new SqlParameter("@currentCuponsGen", SqlDbType.Int, 5));
        cmd.Parameters["@bizID"].Value = bizID;
        cmd.Parameters["@header"].Value = header;
        cmd.Parameters["@descrition"].Value = description;
        cmd.Parameters["@exceptions"].Value = exceptions;
        cmd.Parameters["@startDate"].Value = startDateDateTime;
        cmd.Parameters["@expirydate"].Value = expiryDateDateTime;
        cmd.Parameters["@maxnumberofCoupons"].Value = maxNumberOfPrintsInt;
        cmd.Parameters["@orgPrice"].Value = orgPrice;
        cmd.Parameters["@cpnPrice"].Value = cpnPrice;
        // cmd.Parameters["@currentCuponsGen"].Value = DisCnt.CurrentPrints;
        cmd.Parameters.Add(new SqlParameter("@discountID", SqlDbType.BigInt));
        cmd.Parameters["@discountID"].Direction = ParameterDirection.Output;

        try
        {
            int returnvalue;
            con.Open();
            returnvalue = Convert.ToInt32(cmd.ExecuteScalar());
            return 0;
        }

        catch (SqlException Err)
        {
            throw new ApplicationException(Err.ErrorCode + " - Error in Insert Discount");
            //    return 4;

        }

        finally
        {
            con.Close();
        }


    }

    [WebMethod]
    

    public int getDiscountCounter()
    
    {
        int currentActiveDiscount = -1;
        string bizID = Membership.GetUser().ProviderUserKey.ToString();
        
        string SqlStr = " Select count(bizDisCountID) FROM bizDiscountDtls WHERE "
            + " bizID = '" + bizID + "' ";

        string sqlDatabase = "eastManDB";
        
        String ConnStr = string.Empty;
        
        
        ConnStr = ConfigurationManager.ConnectionStrings[sqlDatabase].ConnectionString;
        
        
        SqlConnection dbConnection = new SqlConnection(ConnStr);
        
        try
        
        {
            dbConnection.Open();
            SqlCommand dbcommand = new SqlCommand(SqlStr, dbConnection);
            currentActiveDiscount = Convert.ToInt32(dbcommand.ExecuteScalar());
         // ADD PROFILE CHKS TO THE IF STATEMENT FOR FUTURE DEVELOPMENT - CHANGE TO SWITCH STATEMENT
            
            if (currentActiveDiscount < 11)
            {
                return currentActiveDiscount;

            }
            else
            {
                return -1;
            }
        }
        
        catch (SqlException)
        {
            return -6;  
        }
     
        finally
        {
            dbConnection.Close(); 
        }
        
    }


}

