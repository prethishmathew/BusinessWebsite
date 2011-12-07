using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration; 
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;   

/// <summary>
/// Summary description for discountUtility
/// </summary>
public class discountUtility
{
    private string connectionstr;

    public discountUtility()
	{
        connectionstr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;  

		//
		// TODO: Add constructor logic here
		//
	}
    
    public discountUtility( string connectionstr){
    
    this.connectionstr = connectionstr ;
    
    }

    public int InsertDiscount(discountDetailsClass DisCnt)
    {

        SqlConnection con = new SqlConnection(connectionstr)  ;
        SqlCommand cmd = new SqlCommand("DiscountInsert", con);
        cmd.CommandType = CommandType.StoredProcedure;
       
        cmd.Parameters.Add(new SqlParameter("@bizID",SqlDbType.VarChar,50));
        cmd.Parameters.Add(new SqlParameter("@header", SqlDbType.VarChar, 100));
        cmd.Parameters.Add(new SqlParameter("@descrition",SqlDbType.VarChar ));
        cmd.Parameters.Add(new SqlParameter("@exceptions",SqlDbType.VarChar ));
        cmd.Parameters.Add(new SqlParameter ("@startDate",SqlDbType.Date));
        cmd.Parameters.Add(new SqlParameter ("@expirydate",SqlDbType.Date));
        cmd.Parameters.Add(new SqlParameter("@maxnumberofCoupons", SqlDbType.Int ,5));
        cmd.Parameters.Add(new SqlParameter("@currentCuponsGen", SqlDbType.Int, 5));
        cmd.Parameters.Add(new SqlParameter("@orgPrice", SqlDbType.Int, 5));
        cmd.Parameters.Add(new SqlParameter("@cpnPrice", SqlDbType.Int, 5));
        cmd.Parameters["@bizID"].Value = DisCnt.BizID;
        cmd.Parameters["@header"].Value = DisCnt.Header ;
        cmd.Parameters["@descrition"].Value = DisCnt.Description;
        cmd.Parameters["@exceptions"].Value = DisCnt.Exceptions;
        cmd.Parameters["@startDate"].Value = DisCnt.StartDate ;
        cmd.Parameters["@expirydate"].Value = DisCnt.ExpiryDate ;
        cmd.Parameters["@maxnumberofCoupons"].Value = DisCnt.MaxNumberofPrints;
        cmd.Parameters["@orgPrice"].Value = DisCnt.OrgPrice;
        cmd.Parameters["@cpnPrice"].Value = DisCnt.CpnPrice ;
       // cmd.Parameters["@currentCuponsGen"].Value = DisCnt.CurrentPrints;
        cmd.Parameters.Add(new SqlParameter("@discountID",SqlDbType.BigInt));
        cmd.Parameters["@discountID"].Direction = ParameterDirection.Output ;
        
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            return 1;
        }
        catch (SqlException Err)
        {
            throw new ApplicationException(Err.ErrorCode + " - Error in Insert Discount");

        }
        finally
        {
            con.Close();
        }
    }

    public List<discountDetailsClass> SelectDiscount(string bizID)
    
    {
        SqlConnection con = new SqlConnection(connectionstr);
        SqlCommand cmd = new SqlCommand("DiscountSelect", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter ("@bizID",SqlDbType.VarChar,50));
        cmd.Parameters["@bizID"].Value = bizID;

        List <discountDetailsClass> disCountsAvailable = new List<discountDetailsClass>();

        try
        {

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                discountDetailsClass disCntDtls = new discountDetailsClass(
                    (Int64)reader["bizDisCountID"], (string)reader["bizID"], (string)reader["bizDiscountHeader"], (string)reader["bizDiscountDesc"],
                    (string)reader["bizDiscountException1"], (DateTime)reader["bizDiscountStartdate"],
                    (DateTime)reader["bizDiscountExpirydate"], (int)reader["bizDiscountMaxprint"],
                    (int)reader["bizDiscountCpnsGen"], (Decimal)reader["bizDiscountOrgPrice"], (Decimal)reader["bizDiscountCpnPrice"]);
                
                disCountsAvailable.Add(disCntDtls);
                DataSet dtTab = new DataSet();
                DataTable datTab = new DataTable();
                dtTab.ReadXml(reader.GetSqlXml(reader.GetOrdinal("bizDiscountBuyers")).CreateReader());                        
                datTab =  dtTab.Tables["buyer"];       
                if (datTab  != null)
                            {
                            string discountCache = "Cachediscountbuyerslist" + reader["bizDisCountID"].ToString ();
                            HttpRuntime.Cache.Insert(discountCache,
                                    datTab, 
                                    null,
                                    System.DateTime.MaxValue,
                                    TimeSpan.Zero,
                                    System.Web.Caching.CacheItemPriority.Default,
                                    null);
                            }           
                        
            
            }

            reader.Close();
            return disCountsAvailable;
        }
        catch (SqlException err)
        {

            throw new ApplicationException("Select Error Discount : " + err.Message );
        }
     
        finally 
        
        {

            con.Close();
        }
 
    }

    public Int64 UpdateDiscount(Int64 bizDiscountID, string bizID, string header,string description,
        string exceptions, DateTime startDate, DateTime expiryDate, int maxNumberofPrints,Decimal  orgPrice,Decimal  cpnPrice)            
    {
        
        SqlConnection con = new SqlConnection(connectionstr);
        SqlCommand cmd = new SqlCommand("DiscountUpdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        
        cmd.Parameters.Add(new SqlParameter("@discountID", SqlDbType.BigInt));
        cmd.Parameters.Add(new SqlParameter("@bizID", SqlDbType.VarChar, 50));
        cmd.Parameters.Add(new SqlParameter("@header", SqlDbType.VarChar, 100));
        cmd.Parameters.Add(new SqlParameter("@descrition", SqlDbType.VarChar));
        cmd.Parameters.Add(new SqlParameter("@exceptions", SqlDbType.VarChar));
        cmd.Parameters.Add(new SqlParameter("@startDate", SqlDbType.DateTime ));
        cmd.Parameters.Add(new SqlParameter("@expirydate", SqlDbType.DateTime));
        cmd.Parameters.Add(new SqlParameter("@maxnumberofCoupons", SqlDbType.Int, 5));
        cmd.Parameters.Add(new SqlParameter("@orgPrice", SqlDbType.Decimal));
        cmd.Parameters.Add(new SqlParameter("@cpnPrice", SqlDbType.Decimal));
        
        //cmd.Parameters.Add(new SqlParameter("@currentCuponsGen", SqlDbType.Int, 5));
        cmd.Parameters["@discountID"].Value = bizDiscountID;
        cmd.Parameters["@bizID"].Value = bizID;
        cmd.Parameters["@header"].Value = header ;
        cmd.Parameters["@descrition"].Value = description;
        cmd.Parameters["@exceptions"].Value = exceptions;
        cmd.Parameters["@startDate"].Value = startDate;
        cmd.Parameters["@expirydate"].Value = expiryDate;
        cmd.Parameters["@maxnumberofCoupons"].Value = maxNumberofPrints;
        cmd.Parameters["@orgPrice"].Value = orgPrice;
        cmd.Parameters["@cpnPrice"].Value = cpnPrice;
        System.Windows.Forms.MessageBox.Show(cmd.Parameters["@cpnPrice"].Value.ToString ());
        //cmd.Parameters["@currentCuponsGen"].Value = currentPrints;
        //cmd.Parameters.Add(new SqlParameter("@discountID", SqlDbType.BigInt));
        //cmd.Parameters["@discountID"].Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            return bizDiscountID;
        }
        catch (SqlException Err)
        {
        //    System.Windows.Forms.MessageBox.Show(Err.Message);
            throw new ApplicationException(Err.ErrorCode + " - Error in Update Discount" + Err.Message);

        }
        catch (Exception err)
        {
          //  System.Windows.Forms.MessageBox.Show(err.Message);
            throw new ApplicationException(err.Message + " - Error in Update Discount" + err.Message);

        }
        finally
        {
            con.Close();
        }
    }

    public Int64 DeleteDiscount(Int64 bizDiscountID,string bizID)
    {
        SqlConnection con = new SqlConnection(connectionstr);
        SqlCommand cmd = new SqlCommand("DiscountDelete", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@discountID", SqlDbType.VarChar, 50));
        cmd.Parameters["@discountID"].Value = bizDiscountID;
        cmd.Parameters.Add(new SqlParameter("@bizID", SqlDbType.VarChar, 50));
        cmd.Parameters["@bizID"].Value = bizID;

        try

        {

            con.Open();
            cmd.ExecuteNonQuery();
            return bizDiscountID;        
        }
        catch (SqlException err)
        {

            throw new ApplicationException("Delete Error Discount : " + err.ErrorCode);
        }

        finally
        {

            con.Close();
        }

    }


}
