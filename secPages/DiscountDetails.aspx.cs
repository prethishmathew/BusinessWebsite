using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration ;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;


public partial class secPages_DiscountDetails : System.Web.UI.Page
{
    object discountID = null;
    decimal orgValue = 0.0M;
    decimal discountValue = 0.0M;
    string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //discountID = 1238;
        if (Request.QueryString["id"] != null)
        {
            discountID = Request.QueryString["id"];
        }
        else
        {
            Response.Write("Invalid discount Id...");   
            Response.End( ); 
        }
        if (!Page.IsPostBack)
        {
            Data_Load();
        }
        Page.Title = bizDiscountHeader.Text ;
    }

    protected void Data_Load()
    {         if (!Page.IsPostBack)
        {
        
            using (SqlConnection Conn = new SqlConnection(connStr))
            {

                using (SqlCommand cmd = new SqlCommand("DiscountSelectOnDiscountID"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DiscountID", discountID));
                    Conn.Open();
                    cmd.Connection = Conn;
                    buyLink.Visible = false;
                    using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        
                        while (rdr.Read())
                        {
                            buyLink.Visible = true;
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountHeader")))
                            {
                                Page.Title = rdr.GetString(rdr.GetOrdinal("bizDiscountHeader"));
                                bizDiscountHeader.Text  = rdr.GetString(rdr.GetOrdinal("bizDiscountHeader"));
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountDesc")))
                            {
                                string bizDiscountStr = rdr.GetString(rdr.GetOrdinal("bizDiscountDesc"));
                                if (bizDiscountStr.Length > 400)
                                {
                                    bizDiscountDesc.Text = bizDiscountStr.Substring(0, 400);
                                    bizDiscountDesc.Text +=
                                    "<span id='dtlsMore' style='display:none'   >" + bizDiscountStr.Substring(400) +
                                    "</span><a id='showDtls' href='#'  > Show More..</a>";
                                }
                                else
                                {
                                    bizDiscountDesc.Text = rdr.GetString(rdr.GetOrdinal("bizDiscountDesc"));
                                }
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountException1")))
                            {
                                discoutexceptions.Text  = rdr.GetString(rdr.GetOrdinal("bizDiscountException1"));
                            }

                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountStartdate")))
                            {
                                bizDiscountStartdate.Text  = rdr.GetDateTime(rdr.GetOrdinal("bizDiscountStartdate")).ToString();
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountExpirydate")))
                            {
                                bizDiscountExpirydate.Text  = rdr.GetDateTime(rdr.GetOrdinal("bizDiscountExpirydate")).ToString();
                            }

                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountMaxprint")))
                            {
                                int bizDiscountMaxprint = rdr.GetInt32(rdr.GetOrdinal("bizDiscountMaxprint"));
                                {
                                    discountSold.Text = "Only " + bizDiscountMaxprint.ToString() + " Left!!";

                                }
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountCpnsGen")))
                            {
                                int bizDiscountCpnsGen = rdr.GetInt32(rdr.GetOrdinal("bizDiscountCpnsGen"));
                                if (bizDiscountCpnsGen > 7)
                                {
                                    discountSold.Text = bizDiscountCpnsGen.ToString() + " Sold!!";
                                }
                                

                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountOrgPrice")))
                            {
                                orgValue = rdr.GetDecimal(rdr.GetOrdinal("bizDiscountOrgPrice"));
                                ActualValue.Text   = orgValue.ToString () ;
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountCpnPrice")))
                            {
                                discountValue = rdr.GetDecimal(rdr.GetOrdinal("bizDiscountCpnPrice"));
                                CpnValue.Text = discountValue.ToString() ;
                            }

                            if (discountValue != 0.0M & orgValue != 0.0M)
                            { 
                                decimal  percent =  Math.Round(   ((orgValue - discountValue)/ orgValue )*100);
                                percentOff.Text = percent.ToString(); 
                            }

                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizName")))
                            {
                                bizName.Text  = rdr.GetString(rdr.GetOrdinal("bizName"));
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("profileImgPath")))
                            {
                                profImg.ImageUrl  = rdr.GetString(rdr.GetOrdinal("profileImgPath"));
                            }
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountStopDate")))
                            {
                                //bizDiscountStopDate = rdr.GetDateTime (rdr.GetOrdinal("bizDiscountStopDate")).ToString();
                                DateTime bizDiscountStopDate = rdr.GetDateTime(rdr.GetOrdinal("bizDiscountStopDate"));
                                stopYear.Value = bizDiscountStopDate.Year.ToString();
                                stopMonth.Value = bizDiscountStopDate.Month.ToString();
                                stopDay.Value = bizDiscountStopDate.Day.ToString();
                                stopHrs.Value = bizDiscountStopDate.Hour.ToString();
                                stopMins.Value = bizDiscountStopDate.Minute.ToString();
                            }


                            //buildwebPage();
                        }
                        rdr.Close ();
                    }
                }
            }
        }
    }

    protected void Ondisbtn_Click(object sender, EventArgs e)
   
    {
        patRatesFunctions.paypalItems paySrc = new patRatesFunctions.paypalItems();
         
        using (SqlConnection Conn = new SqlConnection(connStr))
        {

            using (SqlCommand cmd = new SqlCommand("DiscountSelectOnDiscountID"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DiscountID", discountID));
                Conn.Open();
                cmd.Connection = Conn;
                buyLink.Visible = false;
                bizDiscountHeader.Text = "Sorry the Deal's sold out!!";
                bizDiscountHeader.ForeColor = System.Drawing.Color.Red    ;
                
                using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    decimal amt = 0.00M;
                    while (rdr.Read())
                    {
                        buyLink.Visible = true;
                        if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountHeader")))
                        {

                            Page.Title = rdr.GetString(rdr.GetOrdinal("bizDiscountHeader"));
                            bizDiscountHeader.Text = rdr.GetString(rdr.GetOrdinal("bizDiscountHeader"));
                            bizDiscountHeader.ForeColor = System.Drawing.Color.Blue;
                            paySrc.item_name = bizDiscountHeader.Text;

                        }

                        if (!rdr.IsDBNull(rdr.GetOrdinal("bizDiscountCpnPrice")))
                        {
                            amt = rdr.GetDecimal(rdr.GetOrdinal("bizDiscountCpnPrice"));
                            paySrc.amount = amt; 
                        }


                        paySrc.cancel_return = "http://myrattles.com/secPages/DiscountDetails.aspx?id=" + discountID.ToString ();
                        paySrc.returnUrl = "http://myrattles.com/secPages/Cpnthankyou.aspx"; 
                        paySrc.discountID  = Convert.ToInt32(   discountID);
                        int rtn = paySrc.createCouponEntry();
                        if (rtn == 0)
                        {
                            string redir = paySrc.getUrl();
                            Response.Redirect(redir);
                        }

                        
                        
                    }
                }
            }


        }
    }
    
}