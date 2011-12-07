using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;  
using System.Web.Configuration ;
using System.Data;
using System.Data.SqlClient ;
using System.Data.Sql ;
using System.Data.SqlTypes ;
using System.Runtime.Serialization.Json;   
using patRatesFunctions; 

/// <summary>
/// Summary description for myNetworkSrv
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class myNetworkSrv : System.Web.Services.WebService
{

    public myNetworkSrv()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public datTabJSON getMyShops(string sEcho)
    {
        //Get all Shops And ShopGrps in Array and pass it as JSON

        datTabJSON JSON = new datTabJSON();
        try
        {
            if (User.Identity.IsAuthenticated)
            {

                SNetwrking myShops = new SNetwrking();
                JSON.aaData = myShops.getMyShops();
                JSON.iDataGrp = myShops.getShopGroups();
                JSON.iTotalRecords = myShops.TotalShops;
                JSON.sColumns = "6"; // changed from 4 to 6 05.14.2011
                JSON.sEcho = sEcho;
                JSON.iTotalDisplayRecords = 10;
                if (JSON.iTotalRecords == 0)
                {
                    JSON.sError = 100;
                }
                else
                {
                    JSON.sError = 0;
                }
            }
            else
            {
                JSON.sError = 904;
            }
        }
        catch (Exception err)
        {
            //System.Windows.Forms.MessageBox.Show (err.Message );
            JSON.sError = 999;
        }
        return JSON;
    }

    [WebMethod]

    public datTabJSON getMyFans(string sEcho)
    {
        //Get all Fans And FanGrps in Array and pass it as JSON - cloned from getMyShops

        datTabJSON JSON = new datTabJSON();
        try
        {
            if (User.Identity.IsAuthenticated)
            {

                SNetwrking myFans = new SNetwrking();
                JSON.aaData = myFans.getMyFans();
                JSON.iDataGrp = myFans.getFanGroups();
                JSON.iTotalRecords = myFans.TotalFans;
                JSON.sColumns = "4";
                JSON.sEcho = sEcho;
                JSON.iTotalDisplayRecords = 10;
                if (JSON.iTotalRecords == 0)
                {
                    JSON.sError = 100;
                }
                else
                {
                    JSON.sError = 0;
                }
            }
            else
            {
                JSON.sError = 904;
            }
        }
        catch (Exception err)
        {
            //System.Windows.Forms.MessageBox.Show (err.Message );
            JSON.sError = 999;
        }
        return JSON;
    }

    [WebMethod]
    public string deleteMyShop(string id)
    {
        if (!User.Identity.IsAuthenticated)
        {

            return "904";

        }
        try
        {
            string myID = Membership.GetUser().ProviderUserKey.ToString();
            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SNShopsDelete", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@myID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@shopID", SqlDbType.VarChar, 50));

                cmd.Parameters["@myID"].Value = myID;
                cmd.Parameters["@shopID"].Value = id;    /*id;*/
                try
                {
                    con.Open();
                    int rtncode = cmd.ExecuteNonQuery();
                    con.Close();
                    CommonFunctions.cacheRemove(myID + "SNetwrkMyShopsDataTab");
                    {
                        return "0";

                    }


                }
                catch (SqlException)
                {
                    return "911";
                }
                finally
                {
                    con.Close();
                }
            }

        }
        catch (Exception)
        {

            return "912";
        }



    }

    [WebMethod]
    public string deleteMyFan(string id)
    {

        if (!User.Identity.IsAuthenticated)
        {

            return "904";

        }
        try
        {
            string myID = Membership.GetUser().ProviderUserKey.ToString();
            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SNFansDelete", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@myID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@fanID", SqlDbType.VarChar, 50));

                cmd.Parameters["@myID"].Value = myID;
                cmd.Parameters["@fanID"].Value = id;
                try
                {
                    con.Open();
                    int rtncode = cmd.ExecuteNonQuery();
                    con.Close();
                    CommonFunctions.cacheRemove(myID + "SNetwrkMyFansDataTab");
                    //Execute Non Query will return the number of Rows affected ;
                    return "0";

                }
                catch (SqlException err)
                {
                    //System.Windows.Forms.MessageBox.Show(err.Message);    
                    return "911";
                }
                finally
                {
                    con.Close();
                }
            }

        }
        catch (Exception)
        {

            return "912";
        }

    }

    [WebMethod]
    public string MyShopChgGrp(string id, string toGrp)
    {
        if (!User.Identity.IsAuthenticated)
        {

            return "904";

        }



        try
        {
            string myID = Membership.GetUser().ProviderUserKey.ToString();
            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SNShopsGrpChg", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@myID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@shopID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@newGrp", SqlDbType.VarChar, 100));

                cmd.Parameters["@myID"].Value = myID;
                cmd.Parameters["@shopID"].Value = id;
                cmd.Parameters["@newGrp"].Value = toGrp;
                try
                {
                    con.Open();
                    int rtncode = cmd.ExecuteNonQuery();
                    con.Close();
                    CommonFunctions.cacheRemove(myID + "SNetwrkShopGrpDataTab");
                    //Execute Non Query will return the number of Rows affected ;
                    //if (rtncode == 1)
                    {
                        return "0";

                    }


                }
                catch (SqlException)
                {
                    return "911";
                }
                finally
                {
                    con.Close();
                }
            }

        }
        catch (Exception)
        {

            return "912";
        }

    }
    [WebMethod]
    public string MyFanChgGrp(string id, string toGrp)
    {
        if (!User.Identity.IsAuthenticated)
        {

            return "904";

        }



        try
        {
            string myID = Membership.GetUser().ProviderUserKey.ToString();
            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SNFansGrpChg", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@myID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@fanID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@newGrp", SqlDbType.VarChar, 100));

                cmd.Parameters["@myID"].Value = myID;
                cmd.Parameters["@fanID"].Value = id;
                cmd.Parameters["@newGrp"].Value = toGrp;
                try
                {
                    con.Open();
                    int rtncode = cmd.ExecuteNonQuery();
                    con.Close();
                    CommonFunctions.cacheRemove(myID + "SNetwrkFanGrpDataTab");
                    //Execute Non Query will return the number of Rows affected ;
                    //if (rtncode == 1)
                    {
                        return "0";

                    }


                }
                catch (SqlException)
                {
                    return "911";
                }
                finally
                {
                    con.Close();
                }
            }

        }
        catch (Exception)
        {

            return "912";
        }

    }

    [WebMethod]
    public string addMyShop(string id, string grp)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return "904";
        }
        
        SNetwrking myNetwork = new SNetwrking();
        string myProfileImg = myNetwork.getmyProfilePic();
        //myProfileImg = myProfileImg.Substring(myProfileImg.LastIndexOf("profileImg") + 11).Trim();            
        
        if (myNetwork.isValidShop(id))
        {
            return "811";/*Group Exists no further processing Needed*/
        }

        try
        {
            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SNShopsAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@myID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@shopID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@myProfilePic", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@groupName", SqlDbType.VarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@myName", SqlDbType.VarChar, 100));
                
                
                cmd.Parameters["@myID"].Value = myNetwork.ID;
                if (string.IsNullOrWhiteSpace(myProfileImg))
                {
                    cmd.Parameters["@myProfilePic"].Value = "NULL"   ;
                }
                else
                {
                    cmd.Parameters["@myProfilePic"].Value = myProfileImg;
                
                }
                
                cmd.Parameters["@shopID"].Value = id;
                if (string.IsNullOrEmpty(grp) || grp == "null")
                {
                    cmd.Parameters["@groupName"].Value = "Group Not Assigned";
                }
                else
                {
                    cmd.Parameters["@groupName"].Value = grp;
                }
                cmd.Parameters["@myName"].Value = User.Identity.Name;
                try
                {
                    con.Open();
                    int rtncode = cmd.ExecuteNonQuery();
                    con.Close();
                    CommonFunctions.cacheRemove(myNetwork.ID + "SNetwrkMyShopsDataTab");
                    return "0";
                }
                catch (SqlException sqlErr)
                {
                    System.Windows.Forms.MessageBox.Show(sqlErr.Message);
                    return "911";
                }
                catch (Exception err)
                {
                    return "804";
                }
                finally
                {
                    con.Close();
                }
            }

        }
        catch (Exception)
        {

            return "912";
        }
    }

    //[WebMethod] ---- NOT IMPLEMENTED DUE TO PERFORMANCE ISSUE.
    private string deleteGrp(string grpID, string typ)//NOT IMPLEMENTED DUE TO PERFORMANCE ISSUE.
    {
        /*
         * Delete grp from the grplist. coomon for Shops and Fans. Function parameter 
         * typ pass the switch string. 
         * 
         * Imp Note : This Function was not Implemented because the overhead that would be needed
         * to update all the Shops Or Fans that are in the deleted groups. 
         *              NOT IMPLEMENTED DUE TO PERFORMANCE ISSUE.
         * 
         */

        if (!User.Identity.IsAuthenticated)
        {

            return "904";

        }
        string storedProc = "";
        string myID = Membership.GetUser().ProviderUserKey.ToString();
        string cacheName = myID + "SNetwrkShopGrpDataTab";
        switch (typ)
        {
            case "shops":
                {
                    storedProc = "SNGrpShopDelete";
                    cacheName = myID + "SNetwrkShopGrpDataTab";
                    break;
                }
            case "fans":
                {
                    storedProc = "SNGrpFansDelete";
                    cacheName = myID + "SNetwrkFanGrpDataTab";
                    break;
                }
        }
        try
        {
            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(storedProc, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@grpID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@myID", SqlDbType.VarChar, 50));
                cmd.Parameters["@myID"].Value = myID;
                cmd.Parameters["@grpID"].Value = grpID;
                try
                {
                    con.Open();
                    int rtncode = cmd.ExecuteNonQuery();
                    con.Close();
                    CommonFunctions.cacheRemove(cacheName);
                    return "0";

                }
                catch (SqlException sqlErr)
                {
                    //System.Windows.Forms.MessageBox.Show(sqlErr.Message);
                    return "911";
                }
                catch (Exception err)
                {
                    return "804";
                }
                finally
                {
                    con.Close();
                }
            }

        }
        catch (Exception)
        {

            return "912";
        }

    }
    [WebMethod]
    public string addNewGrp(string grpName, string typ)
    {

        if (!User.Identity.IsAuthenticated)
        {
            return "904";
        }

        if (string.IsNullOrEmpty(grpName))
        {
            return "101";
        }

        SNetwrking myNetwork = new SNetwrking();

        try
        {
            if (myNetwork.isValidGrp(grpName, typ))
            {
                return "811";
                /*Group Exists no further processing Needed*/
            }
        }
        catch (NullReferenceException err)
        {
            Server.ClearError();
        }

        //System.Windows.Forms.MessageBox.Show("1" + typ);     
        string storedProc = "";
        string cacheName = "";
        string myID = myNetwork.ID;
        //System.Windows.Forms.MessageBox.Show("2" + typ);     
        switch (typ)
        {

            case "myShops":
                storedProc = "SNGrpShopAdd";
                cacheName = myID + "SNetwrkShopGrpDataTab";
                break;
            case "myFans":
                storedProc = "SNGrpFanAdd";
                cacheName = myID + "SNetwrkFanGrpDataTab";
                break;
            case "default":
                return "111";
        }
        //System.Windows.Forms.MessageBox.Show("3" + typ);     
        try
        {
            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(storedProc, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@grpName", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@myID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@grpID", SqlDbType.VarChar, 50));
                cmd.Parameters["@myID"].Value = myID;
                cmd.Parameters["@grpName"].Value = grpName;
                cmd.Parameters["@grpID"].Value = myNetwork.getMaxGrpID(typ).ToString();
                //System.Windows.Forms.MessageBox.Show("4" + typ);     
                try
                {
                    con.Open();
                    int rtncode = cmd.ExecuteNonQuery();
                    con.Close();
                    CommonFunctions.cacheRemove(cacheName);
                    return "0";
                }
                catch (SqlException sqlErr)
                {
                    //System.Windows.Forms.MessageBox.Show(sqlErr.Message);
                    return "911";
                }
                catch (Exception err)
                {
                    //System.Windows.Forms.MessageBox.Show(err.Message);
                    return "804";
                }
                finally
                {
                    con.Close();
                }
            }

        }
        catch (SqlException sqlErr)
        {
            //  System.Windows.Forms.MessageBox.Show(sqlErr.Message);
            return "911";
        }

        catch (Exception err)
        {
            //System.Windows.Forms.MessageBox.Show(err.Message);
            return "912";
        }


    }

    [WebMethod]
    public bool isMyShop(string id)
    {

        try
        {
            if (!User.Identity.IsAuthenticated)
            {

                return false;

            }

            if (string.IsNullOrEmpty(id) || id == "null")
            {
                return false;
            }

            SNetwrking myNet = new SNetwrking();
            return myNet.isValidShop(id);
        }

        catch (Exception)
        {
            return false;
        }
    }
    [WebMethod]
    public datTabJSON updateSMS(string id, char flag)
    {
        datTabJSON rtnCls = new datTabJSON();
        try
        {
            if (!User.Identity.IsAuthenticated)
            {
                rtnCls.sError = 904;
                return rtnCls;
            }
            string myID = Membership.GetUser().ProviderUserKey.ToString();
            if (string.IsNullOrEmpty(id))
            {
                rtnCls.sError = 913;
                return rtnCls;

            }

            if (char.IsWhiteSpace(flag))
            {
                rtnCls.sError = 914;
                return rtnCls;
            }

            if (flag.ToString() == "Y" && flag.ToString() == "N")
            {
                rtnCls.sError = 915;
                return rtnCls;

            }

            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            string storedProc = "SNsmsNotificationChg";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(storedProc, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@myID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@shopID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@sms", SqlDbType.Char, 1));
                cmd.Parameters["@myID"].Value = myID;
                cmd.Parameters["@shopID"].Value = id;
                cmd.Parameters["@sms"].Value = flag;
                //System.Windows.Forms.MessageBox.Show("4" + typ);     
                try
                {
                    con.Open();
                    int rtncode = cmd.ExecuteNonQuery();
                    //System.Windows.Forms.MessageBox.Show("5" + typ);     
                    con.Close();
                    //System.Windows.Forms.MessageBox.Show("6" + typ);   

                    string cacheFans = myID + "SNetwrkMyFansDataTab";
                    CommonFunctions.cacheRemove(cacheFans);
                    string HTMLstr;
                    if (flag.ToString() == "Y")
                    { // This means that the Flag is now set to Yes. So we need to set it to "N" on the page
                        HTMLstr = "<a href='#' onclick='updateMsgFlag(" + '"' + myID + '"' + "," + '"' + "N" + '"' + "," + '"' + "SMS" + '"' + ",this)'; Title='Stop Email Notification'> <img style='outline-style:none;border-style:none;' alt='Stop SMS Notification' src='../Images/phone-delete.png' /> </a>";
                    }
                    else
                    {
                        HTMLstr = "<a href='#' onclick='updateMsgFlag(" + '"' + myID + '"' + "," + '"' + "Y" + '"' + "," + '"' + "SMS" + '"' + ",this);'Title='Get SMS updates'> <img style='outline-style:none;border-style:none;' alt='Get SMS updates' src='../Images/phone-add.png' /> </a>";
                    }

                    rtnCls.sError = 0;
                    rtnCls.sEcho = (HTMLstr);

                }
                catch (SqlException sqlErr)
                {
                    //System.Windows.Forms.MessageBox.Show(sqlErr.Message);
                    rtnCls.sError = 911;

                }

                return rtnCls;
            }
        }
        catch (Exception err)
        {
            rtnCls.sError = 912;
            return rtnCls;
        }

    }

    [WebMethod]
    public datTabJSON updateEmail(string id, char flag)
    {
        datTabJSON rtnCls = new datTabJSON();
        try
        {
            if (!User.Identity.IsAuthenticated)
            {

                rtnCls.sError = 904;
                return rtnCls;

            }
            string myID = Membership.GetUser().ProviderUserKey.ToString();
            if (string.IsNullOrEmpty(id))
            {
                rtnCls.sError = 913;
                return rtnCls;

            }

            if (char.IsWhiteSpace(flag))
            {
                rtnCls.sError = 914;
                return rtnCls;
            }


            if (flag.ToString() == "Y" || flag.ToString() == "N")
            {

            }
            else
            {
                rtnCls.sError = 915;
                return rtnCls;
            }


            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            string storedProc = "SNEmailNotificationChg";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(storedProc, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@myID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@shopID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.Char, 1));
                cmd.Parameters["@myID"].Value = myID;
                cmd.Parameters["@shopID"].Value = id;
                cmd.Parameters["@email"].Value = flag;
                //System.Windows.Forms.MessageBox.Show("4" + typ);     
                try
                {
                    con.Open();
                    int rtncode = cmd.ExecuteNonQuery();
                    //System.Windows.Forms.MessageBox.Show("5" + typ);     
                    con.Close();
                    //System.Windows.Forms.MessageBox.Show("6" + typ);   

                    string cacheFans = myID + "SNetwrkMyFansDataTab";
                    CommonFunctions.cacheRemove(cacheFans);
                    string HTMLstr;
                    if (flag.ToString() == "Y")
                    { // This means that the Flag is now set to Yes. So we need to set it to "N" on the page
                        HTMLstr = "<a href='#' onclick='updateMsgFlag(" + '"' + myID + '"' + "," + '"' + "N" + '"' + "," + '"' + "Email" + '"' + ",this)'; Title='Stop Email Notification'> <img style='outline-style:none;border-style:none;' alt='Stop Email Notification' src='../Images/mail-delete.png' /> </a>";
                    }
                    else
                    {
                        HTMLstr = "<a href='#' onclick='updateMsgFlag(" + '"' + myID + '"' + "," + '"' + "Y" + '"' + "," + '"' + "Email" + '"' + ",this)';Title='Get Email Notification'> <img style='outline-style:none;border-style:none;' alt='Get Email Notification' src='../Images/mail-add.png' /></a>";
                    }
                    rtnCls.sError = 0;
                    rtnCls.sEcho = HTMLstr;


                }
                catch (SqlException sqlErr)
                {
                    //System.Windows.Forms.MessageBox.Show(sqlErr.Message);
                    rtnCls.sError = 911;

                }

                return rtnCls;
            }
        }
        catch (Exception err)
        {
            rtnCls.sError = 912;
            return rtnCls;
        }

    }
    [WebMethod]
    public datTabJSON getMyRattles(int pageNum)
    {
        datTabJSON dtTab = new datTabJSON();
        try
        {
            if (!User.Identity.IsAuthenticated)
            {

                dtTab.sError = 904;
                return dtTab;

            }


            SNetwrking myNetWork = new SNetwrking();
            dtTab.iDataGrp = myNetWork.generateMyRattles(pageNum);
            if (dtTab.iDataGrp != null)
            {
                dtTab.iTotalRecords = dtTab.iDataGrp.Count;
                dtTab.sError = 0;
            }
            else
            {
                dtTab.sError = 100;
            }
            return dtTab;
        }

        catch (Exception err)
        {
            //System.Windows.Forms.MessageBox.Show(err.Message  );    
            dtTab.sError = 911;
            return dtTab;

        }
    }
    [WebMethod]
    public datTabJSON createNewRattle(string rattle)
    {
        datTabJSON myJSON = new datTabJSON();

        try
        {
            if (!User.Identity.IsAuthenticated)
            {
                myJSON.sEcho = "User not logged in";
                myJSON.sError = 904;
                return myJSON;
            }
            rattle.Trim();
            if (rattle.Length > 120)
            {
                myJSON.sEcho = "Rattle length is greater than 120 characters";
                myJSON.sError = 555;
                return myJSON;
            }
            if (rattle.Length == 0)
            {
                myJSON.sEcho = "Your rattles cannot be empty";
                myJSON.sError = 556;
                return myJSON;
            }

            string connStr = WebConfigurationManager.ConnectionStrings["rattleDB"].ConnectionString;
            string myID = Membership.GetUser().ProviderUserKey.ToString();
            string storedProc = "rattleInsert";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(storedProc, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@rattleMsg", SqlDbType.VarChar, 120));
                cmd.Parameters.Add(new SqlParameter("@RattleID", SqlDbType.Int, 120));

                cmd.Parameters["@ID"].Value = myID;
                cmd.Parameters["@rattleMsg"].Value = rattle;
                cmd.Parameters["@RattleID"].Direction = ParameterDirection.Output;
                //System.Windows.Forms.MessageBox.Show("4" + typ);     
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    string rattleID = cmd.Parameters["@RattleID"].Value.ToString();
                    con.Close();
                    string cacheFans = myID + "rattlesIcreatedXml";
                    myJSON.sEcho = patRatesFunctions.CommonFunctions.createRattleHTML(rattleID, DateTime.Now.ToShortTimeString(), rattle);
                    myJSON.sColumns = "#R" + rattleID;
                    patRatesFunctions.CommonFunctions.cacheRemove(cacheFans);
                    myJSON.sError = 000;
                    return myJSON;
                }
                catch (SqlException sqlErr)
                {
                    myJSON.sEcho = sqlErr.Message;
                    myJSON.sError = 557;
                    return myJSON;
                }
            }
        }
        catch (Exception err)
        {
            
            myJSON.sEcho = err.Message;
            myJSON.sError = 558;
            return myJSON;

        }

    }
    [WebMethod]
    public datTabJSON DeleteRattle(int rattleID)
    {
        datTabJSON myJSON = new datTabJSON();
        if (!User.Identity.IsAuthenticated)
        {

            myJSON.sError = 904;
            return myJSON;
        }

        string connStr = WebConfigurationManager.ConnectionStrings["rattleDB"].ConnectionString;
        string myID = Membership.GetUser().ProviderUserKey.ToString();
        string storedProc = "rattleDelete";
        using (SqlConnection con = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand(storedProc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar, 50));
            cmd.Parameters.Add(new SqlParameter("@RattleID", SqlDbType.Int, 120));
            cmd.Parameters["@ID"].Value = myID;
            cmd.Parameters["@RattleID"].Value = rattleID;
            //System.Windows.Forms.MessageBox.Show("4" + typ);     
            try
            {
                con.Open();
                int rtnCode = cmd.ExecuteNonQuery();
                con.Close();
                string cacheFans = myID + "rattlesIcreatedXml"; ;
                CommonFunctions.cacheRemove(cacheFans);
                myJSON.sError = 0;
                return myJSON;
            }
            catch (SqlException sqlErr)
            {
                //System.Windows.Forms.MessageBox.Show(sqlErr.Message);
                myJSON.sError = 557;
                return myJSON;
            }
        }


    }
    [WebMethod]
    public datTabJSON getRattlesICreated(int PageNum)
    {
        //System.Windows.Forms.MessageBox.Show("in service");
        datTabJSON rtnJSON = new datTabJSON();
        try
        {
            bizInformation myBizInfo = new bizInformation();

            rtnJSON.sColumns = myBizInfo.getMyRattlesHtml(PageNum);
            if (rtnJSON.sColumns != null)
            {
                rtnJSON.iTotalDisplayRecords = PageNum + 1;
                rtnJSON.sError = 0;
            }
            else
            {
                rtnJSON.iTotalDisplayRecords = 0;
                rtnJSON.sError = 100;
            }

        }
        catch (Exception err)
        {

            rtnJSON.sError = 550;
            return null;
        }
        return rtnJSON;
    }
    [WebMethod]
    public datTabJSON getShopsRattle(string shopID)
    {

        datTabJSON rtnJSON = new datTabJSON();
        try
        {

            bizInformation myBizInfo = new bizInformation(shopID);
            rtnJSON.sColumns = myBizInfo.getMyRattlesHtml(1);
            if (rtnJSON.sColumns != null)
            {
                rtnJSON.iTotalDisplayRecords = 1 + 1;
                rtnJSON.iTotalRecords = myBizInfo.CurrCycleRattleCnt;
                rtnJSON.sError = 0;
            }
            else
            {
                rtnJSON.iTotalDisplayRecords = 0;
                rtnJSON.sError = 100;
            }


        }
        catch (Exception err)
        {
            rtnJSON.sError = 911;
        }
        
        return rtnJSON;
    }

    [WebMethod]
    public datTabJSON getmyCoupons(int PageNum) {

        datTabJSON rtnJSON = new datTabJSON();
        if (!User.Identity.IsAuthenticated)
        {

            rtnJSON.sError = 904;
            return rtnJSON;
        }

        if (!patRatesFunctions.CommonFunctions.isNumeric(PageNum.ToString()))
        
            {
                rtnJSON.sEcho  = "Invalid Pagenumber";
                rtnJSON.sError = 908;
                return rtnJSON;
            }

        try
        {
            SNetwrking disMy = new SNetwrking();
            string myHtml = disMy.getMydiscounts(PageNum);
            if (myHtml == null)            
                {
                    rtnJSON.sEcho = "<div>No More records to fetch.</div>";
                    rtnJSON.sError = 100;
                    return rtnJSON;
                }
            rtnJSON.sEcho = myHtml;
            rtnJSON.sError = 0; 
            }
        catch (Exception ex)
        {
            
            rtnJSON.sError = 656;
            rtnJSON.sEcho = ex.Message; 
            return rtnJSON;
        
        }

        return rtnJSON;
    
    }
    /*
    [WebMethod]
    public datTabJSON grabDiscount(string DiscountID, string myName, string myZip)
    {
       datTabJSON rtnJSON = new datTabJSON();


        if (!User.Identity.IsAuthenticated)
        {
            rtnJSON.sError =  904;
            return rtnJSON ;
        }

        if (string.IsNullOrEmpty(DiscountID ))
        {
            rtnJSON.sError =  101;
            return rtnJSON ;
        }
        if (string.IsNullOrEmpty(myName ))
        {
            rtnJSON.sError =  102;
            return rtnJSON ;
        }
        if (string.IsNullOrEmpty(myZip ))
        {
            rtnJSON.sError =  103;
            return rtnJSON ;
        }
        
        try
        {
            
            string cpnNumber = getDiscountDetails(DiscountID );
            switch (cpnNumber) { 
                case "MAXEDOUT":
                    rtnJSON.sError = 100;
                    rtnJSON.sEcho = "Max Number Reached";
                    return rtnJSON ;

                case "ERROR":
                    rtnJSON.sError = 811;
                    rtnJSON.sEcho = "Unknown Error";
                    return rtnJSON ;

                default:
                    cpnNumber = DiscountID + "-" + cpnNumber;
                    rtnJSON.sError = 0;
                    break;
            }

            string myID = Membership.GetUser().ProviderUserKey.ToString();
            string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("BuyDiscount", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@discountID", SqlDbType.VarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@myID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@myName", SqlDbType.VarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@buyerZip", SqlDbType.VarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@cpnNumber", SqlDbType.VarChar, 100));
                cmd.Parameters["@discountID"].Value = DiscountID ;
                cmd.Parameters["@myID"].Value = myID;
                cmd.Parameters["@myName"].Value = myName ;
                cmd.Parameters["@buyerZip"].Value = myZip ;
                cmd.Parameters["@cpnNumber"].Value = cpnNumber ;
                try
                {
                    con.Open();
                    int rtncode = cmd.ExecuteNonQuery();
                    con.Close();
                    string discountCache = "Cachediscountbuyerslist" + DiscountID; 
                    CommonFunctions.cacheRemove(discountCache );
                    //sendEmail;
                }
                catch (SqlException)
                {
                    rtnJSON.sError = 911;
                    return rtnJSON;
                }
                finally
                {
                    con.Close();
                }
            }

        }
        catch (Exception err)
        {
            rtnJSON.sError = 911;
        }
        return rtnJSON;
    }

    private string getDiscountDetails(string discountId)
    {
        DataSet dtTab = new DataSet();
        DataTable datTab = new DataTable();
        string couponcnt;

        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand("DiscountSelectOnDiscountID", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DiscountID", SqlDbType.VarChar, 50));

            cmd.Parameters["@DiscountID"].Value = discountId;

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int maxCpn = Convert.ToInt32(reader["bizDiscountMaxprint"]);
                int genCpn = Convert.ToInt32(reader["bizDiscountCpnsGen"]);

                if (genCpn < maxCpn)
                {
                    string discountCache = "Cachediscountbuyerslist" + discountId ; 
                    dtTab.ReadXml(reader.GetSqlXml(reader.GetOrdinal("bizDiscountBuyers")).CreateReader());
                    datTab = dtTab.Tables["buyer"];
                    if (datTab != null)
                    {
                        HttpRuntime.Cache.Insert(discountCache,
                                datTab,
                                null,
                                System.DateTime.MaxValue,
                                TimeSpan.Zero,
                                System.Web.Caching.CacheItemPriority.Default,
                                null);


                    }
                    couponcnt = (datTab.DefaultView.Count + 1).ToString();

                }
                else
                {
                    couponcnt = "MAXEDOUT";
                }
                con.Close();
                return couponcnt;

            }
            catch (SqlException err)
            {

                return "Error";
            }
        }
    }*/
//WebService brackets
}