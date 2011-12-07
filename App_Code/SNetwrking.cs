using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.Security;
using System.Web.Configuration;
using System.Data.SqlClient;
namespace newPlace
{
    /// <summary>
    /// Summary description for SNetwrking
    /// </summary>
    public class SNetwrking
    {
        private string id;
        public string ID
        {
            get { return id; }
            // set { id = value; } - To make ID read only

        }
        /*
        private  XmlDocument [] myshops;
        public XmlDocument[] myShops
        {
            get { return myshops ; }
            set { myshops  = value; }
    
        }

        private XmlDocument[] myfans;
        public XmlDocument[] myFans
        {
            get { return myfans; }
            set { myfans = value; }

        }
        */
        private DataTable dtmyshopgrps;
        private DataTable DtmyShopGrps
        {
            get { return dtmyshopgrps; }
            set { dtmyshopgrps = value; }

        }
        private DataTable dtmyfangrps;
        private DataTable DtmyFanGrps
        {
            get { return dtmyfangrps; }
            set { dtmyfangrps = value; }

        }
        private DataTable dtmyshops;
        private DataTable DtmyShops
        {
            get { return dtmyshops; }
            set { dtmyshops = value; }

        }
        private DataTable dtmyfans;
        private DataTable DtmyFans
        {
            get { return dtmyfans; }
            set { dtmyfans = value; }

        }
        private int totalfans;
        public int TotalFans
        {
            get { return totalfans; }
            //set { totalfans  = value; }

        }
        private int totalshops;
        public int TotalShops
        {
            get { return totalshops; }
            //set { totalshops = value; }

        }
        private int rtnCode;
        public int RtnCode
        {
            get { return rtnCode; }
            //  set { rtnCode = value; }

        }
        private string maxgrpid;
        public string maxGrpID
        {
            get { return maxgrpid; }
        }
        private string rtnmessage;
        public string Rtnmessage
        {
            get { return rtnmessage; }
            // set { rtnmessage = value; }

        }
        private string cacheFans;
        private string cacheShops;
        private string cacheShopGrp;
        private string cacheFanGrp;
        private List<List<string>> fangrps;
        private List<List<string>> fanGrps
        {
            get { return fangrps; }
            set { fangrps = value; }

        }
        private List<List<string>> shopgrps;
        private List<List<string>> ShopGrps
        {
            get { return shopgrps; }
            set { shopgrps = value; }
        }
        private List<List<string>> myshops;
        private List<List<string>> MyShops
        {
            get { return myshops; }
            set { myshops = value; }

        }
        private List<List<string>> myfans;
        private List<List<string>> MyFans
        {
            get { return myfans; }
            set { myfans = value; }

        }
        public SNetwrking()
        {
            //string cacheSnetFans =  "SnetCaheFans" + id;
            //string cacheSnetShops = "SnetCaheShops" + id;
            //dtmyfans = CommonFunctions.getFansOrShopsDtXml(cacheSnetFans, "SNSelectMyFans", "myFan");
            //dtmyshops = CommonFunctions.getFansOrShopsDtXml(cacheSnetShops, "SNSelectMyShops", "myShop");  
            this.id = Membership.GetUser().ProviderUserKey.ToString();
            homeInitFunction();
            //
            // TODO: Add constructor logic here
            //
        }
        public SNetwrking(string IndvID)
        {
            if (Roles.IsUserInRole("Admin"))
            {
                this.id = IndvID;
                homeInitFunction();
            }
            else
            {
                this.rtnCode = 911;
                this.rtnmessage = "User not authorized for this Action";


            }
        }
        public List<List<string>> getMyFans()
        {
            if (this.myfans == null)
            {
                this.generateArrays(ref this.myfans, this.dtmyfans, ref this.totalfans, "fans");
            }

            return this.myfans;
        }
        public List<List<string>> getMyShops()
        {
            //System.Windows.Forms.MessageBox.Show("here1");     
            try
            {
                if (this.myshops == null)
                {
                    this.generateArrays(ref this.myshops, this.dtmyshops, ref this.totalshops, "shops");
                }

            }
            catch (Exception Err)
            {
                //System.Windows.Forms.MessageBox.Show(Err.Message);

            }

            return this.myshops;

        }
        public List<List<string>> getFanGroups()
        {

            if (this.fangrps == null)
            {
                this.generateArraysForGrps(ref this.fangrps, ref this.dtmyfangrps);
            }
            return this.fangrps;
        }
        public List<List<string>> getShopGroups()
        {
            //this.shopgrps = null;
            if (this.shopgrps == null)
            {

                this.generateArraysForGrps(ref this.shopgrps, ref this.dtmyshopgrps);
            }
            return this.shopgrps;
        }
        public bool isValidGrp(string groupName, string typ)
        {
            try
            {

                DataTable mytab = new DataTable();
                switch (typ)
                {
                    case "myShops":
                        if (this.dtmyshopgrps == null)
                        {
                            this.buildSnDataTab();

                        }
                        mytab = this.dtmyshopgrps;
                        break;
                    case "myFans":
                        if (this.dtmyfangrps == null)
                        {
                            this.buildSnDataTab();

                        }
                        mytab = this.dtmyfangrps;
                        break;
                }

                if (mytab == null) { return false; }

                if (mytab.Rows.Count == 0) { return false; }

                string strExpr = "grpName LIKE '" + groupName + "'";
                DataRow[] dtRow = mytab.Select(strExpr);

                if (dtRow.Length == 0) { return false; }
                else { return true; }
            }
            catch (Exception err)
            {
                //System.Windows.Forms.MessageBox.Show("IsGrp" + err.Message);
                return true;
            }
        }

        public bool isValidShop(string shopID)
        {
            if (this.dtmyshops == null) { this.buildSnDataTab(); }
            if (this.dtmyshops == null) { return false; }
            string strExpr = "id LIKE '" + shopID + "'";
            DataRow[] dtRow = this.dtmyshops.Select(strExpr);
            if (dtRow.Length == 0) { return false; }
            else { return true; }
        }
        public int getMaxGrpID(string typ)
        {

            DataTable mytab = new DataTable();
            switch (typ)
            {
                case "myShops":
                    if (this.dtmyshopgrps == null)
                    {
                        this.buildSnDataTab();
                    }
                    mytab = this.dtmyshopgrps;
                    break;
                case "myFans":
                    if (this.dtmyfangrps == null)
                    {
                        this.buildSnDataTab();
                    }
                    mytab = this.dtmyfangrps;
                    break;
            }

            if (mytab == null) { return 1; }
            if (mytab.Rows.Count == 0) { return 1; }
            object sumObject;
            sumObject = mytab.Compute("MAX(id)", "");

            if (sumObject != null) { return Convert.ToInt32(sumObject) + 1; }
            else { return 1; }


        }
        private void homeInitFunction()
        {

            this.rtnCode = 0;
            this.rtnmessage = null;
            this.cacheFans = this.id + "SNetwrkMyFansDataTab";
            this.cacheShops = this.id + "SNetwrkMyShopsDataTab";
            this.cacheShopGrp = this.id + "SNetwrkShopGrpDataTab";
            this.cacheFanGrp = this.id + "SNetwrkFanGrpDataTab";
            buildSnDataTab();

        }
        private void generateArrays(ref  List<List<string>> dataArray, DataTable myTab, ref int totRec, string fanShops)
        /*
         * This Routine Generates the Arrays for Groups and the real data of fans and shops
         * 
         * Imp Note : If iterating only for Set number of rows then a new groups section need to be added.
         * */
        {
            //System.Windows.Forms.MessageBox.Show("here - a");
            string bizName = "";
            string Localid = "";
            string varImg = "";
            string grpName = "";

            if (myTab == null) { buildSnDataTab(); }
            //System.Windows.Forms.MessageBox.Show("here - b");

            int i = 0;
            int j = 0;

            if (dataArray == null || dataArray.Count == 0)
            {
                //object LastValue = null;            
                string FieldName = "grpName";
                dataArray = new List<List<string>>();
                //grpArray = new List<List<string>>();

                try
                {
                    if (myTab != null)
                    {
                        foreach (DataRow dr in myTab.Select("", FieldName))
                        {

                            /*        if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                                    {
                                        if (LastValue != null)
                                        {
                                            var itemDistinct = new List<string>();
                                            itemDistinct.Add(LastValue.ToString());
                                            itemDistinct.Add(j.ToString());
                                            grpArray.Add(itemDistinct);
                                            j = 0;
                                        }
                                        LastValue = dr[FieldName];
                                    }*/

                            //    var item = new List<string>();
                            bizName = dr["bizName"].ToString();
                            Localid = dr["id"].ToString();
                            varImg = "<img alt='" + bizName +
                            "' src='http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/"
                            + Localid + "/profileImg.jpg' />";
                            grpName = dr["grpName"].ToString();
                            //testing
                            //varImg = "<img alt='provider' src='http://patrates.com/Images/login.png' />";
                            //testing edn

                            //string varBtn = "<input  type='button' value='Remove'  class='btn'  onclick='delSnet(" + dr["id"].ToString() + ")'  /> " +
                            //                "<br />" +
                            //                "<input  type='button' value='Move'  class='btn'  onclick='moveGrpSnet(" + dr["id"].ToString() + ")'  />";
                            string varBtn;
                            if (fanShops == "shops")
                            {
                                //varBtn = "<div class='menuDrop'><ul ><li style='outline-style:none;'><a href='#'> <img alt='test' src='../Images/arrow-right-double-2.ico' /></a><ul ><li><a href='#' onclick='changeMyGrp(" + '"' + Localid + '"' + "," + '"' + "myShops" + '"' + ");'>Change Group</a></li><li><a href='#' onclick='removeMyShop(" + '"' + Localid + '"' + ");'>Delete</a></li></ul></li></ul></div>";
                                varBtn = "<div class='menuDrop'><ul ><li style='outline-style:none;'><a href='#'> <img alt='test' src='../Images/arrow-right-double-2.ico' /></a><ul ><li><a href='#' onclick='fnChggrpMethods.fnChgGrpScr(" + '"' + Localid + '"' + "," + '"' + "myShops" + '"' + ");'>Change Group</a></li><li><a href='#' onclick='confirmRemoveShop(" + '"' + Localid + '"' + "," + '"' + "myShops" + '"' + ");'>Delete</a></li></ul></li></ul></div>";
                            }
                            else
                            {
                                varBtn = "<div class='menuDrop'><ul ><li style='outline-style:none;'><a href='#'> <img alt='test' src='../Images/arrow-right-double-2.ico' /></a><ul ><li><a href='#' onclick='fnChggrpMethods.fnChgGrpScr(" + '"' + Localid + '"' + "," + '"' + "myFans" + '"' + ");'>Change Group</a></li><li><a href='#' onclick='confirmRemoveShop(" + '"' + Localid + '"' + "," + '"' + "myFans" + '"' + ");'>Delete</a></li></ul></li></ul></div>";
                            }

                            //dataArray[i, 0] = dr["id"].ToString();
                            var item = new List<string>();
                            item.Add(varImg);
                            item.Add(bizName);
                            item.Add(grpName);
                            item.Add(varBtn);
                            dataArray.Add(item);
                            i++;
                            j++;

                        }

                        totRec = i;
                        /*if (j > 0)
                        {
                            var itemDistinct = new List<string>();     
                            itemDistinct.Add(grpName);
                            itemDistinct.Add(j.ToString());
                            grpArray.Add(itemDistinct);
                        }*/

                    }
                }
                catch (Exception err)
                {
                    //System.Windows.Forms.MessageBox.Show("Err" + err.Message );     

                }
            }

        }
        private void generateArraysForGrps(ref List<List<string>> grparray, ref DataTable myTab)
        {
            if (myTab == null) { buildSnDataTab(); }

            if (grparray == null || grparray.Count == 0)
            {
                string grpName = "";
                string Localid = "";
                string FieldName = "grpName";
                grparray = new List<List<string>>();
                try
                {
                    if (myTab != null)
                    {

                        foreach (DataRow dr in myTab.Select("", FieldName))
                        {
                            grpName = dr["grpName"].ToString();
                            Localid = dr["id"].ToString().Trim();
                            var item = new List<string>();
                            item.Add(grpName);
                            item.Add(Localid);
                            grparray.Add(item);
                        }
                    }
                }
                catch (Exception err)
                { //System.Windows.Forms.MessageBox.Show(err.Message + " - err in generateArraysForGrps"); 
                }
            }

        }
        private bool ColumnEqual(object A, object B)
        {

            // Compares two values to see if they are equal. Also compares DBNULL.Value.
            // Note: If your DataTable contains object fields, then you must extend this
            // function to handle them in a meaningful way if you intend to group on them.

            if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value
                return true;
            if (A == DBNull.Value || B == DBNull.Value) //  only one is DBNull.Value
                return false;
            return (A.Equals(B));  // value type standard comparison
        }
        private void buildSnDataTab()
        {
            this.dtmyfans = (DataTable)HttpRuntime.Cache.Get(this.cacheFans);
            this.dtmyshops = (DataTable)HttpRuntime.Cache.Get(this.cacheShops);
            this.dtmyfangrps = (DataTable)HttpRuntime.Cache.Get(this.cacheFanGrp);
            this.dtmyshopgrps = (DataTable)HttpRuntime.Cache.Get(this.cacheShopGrp);
            if (this.dtmyfans == null || this.dtmyshops == null || this.dtmyfangrps == null ||
                         this.dtmyshopgrps == null)
            {
                createDataSNTable();
            }

        }
        private void createDataSNTable()
        {
            try
            {

                CommonFunctions.cacheRemove(this.cacheShops);
                CommonFunctions.cacheRemove(this.cacheShopGrp);
                CommonFunctions.cacheRemove(this.cacheFans);
                CommonFunctions.cacheRemove(this.cacheFanGrp);

                string ConnStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
                DataSet dsmyShops = new DataSet();
                DataSet dsmyFans = new DataSet();
                DataSet dsmyGrpShop = new DataSet();
                DataSet dsmyGrpFan = new DataSet();

                using (SqlConnection connection =
                    new SqlConnection(ConnStr))
                {

                    SqlCommand command = new SqlCommand("SNSelectFansNShops", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("ID", SqlDbType.VarChar, 5000));
                    command.Parameters["ID"].Value = this.id;

                    /*testdata
                    command.Parameters["ID"].Value = "111";*/

                    connection.Open();

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (!dr.IsDBNull(dr.GetOrdinal("bizMyShops")))
                        {

                            dsmyShops.ReadXml(dr.GetSqlXml(dr.GetOrdinal("bizMyShops")).CreateReader());
                            this.dtmyshops = dsmyShops.Tables["myShop"];
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("bizMyFans")))
                        {

                            dsmyFans.ReadXml(dr.GetSqlXml(dr.GetOrdinal("bizMyFans")).CreateReader());
                            this.dtmyfans = dsmyFans.Tables["myFan"];
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("bizShopGrp")))
                        {

                            dsmyGrpShop.ReadXml(dr.GetSqlXml(dr.GetOrdinal("bizShopGrp")).CreateReader());
                            this.dtmyshopgrps = dsmyGrpShop.Tables["myShopGrp"];
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("bizFanGrp")))
                        {
                            dsmyGrpFan.ReadXml(dr.GetSqlXml(dr.GetOrdinal("bizFanGrp")).CreateReader());
                            this.dtmyfangrps = dsmyGrpFan.Tables["myFanGrp"];
                        }
                    }
                    connection.Close();
                }

                if (this.dtmyfans != null)
                {
                    CachedataTabInsert(this.cacheFans, ref this.dtmyfans);
                }
                if (this.dtmyshops != null)
                {
                    CachedataTabInsert(this.cacheShops, ref this.dtmyshops);
                }
                if (this.dtmyshopgrps != null)
                {
                    CachedataTabInsert(this.cacheShopGrp, ref this.dtmyshopgrps);
                }
                if (this.dtmyfangrps != null)
                {
                    CachedataTabInsert(this.cacheFanGrp, ref this.dtmyfangrps);
                }
            }
            catch (SqlException sqlerr)
            {
                //System.Windows.Forms.MessageBox.Show(sqlerr.Message);
            }
            catch (Exception err)
            {

                //System.Windows.Forms.MessageBox.Show(err.Message );
            }
        }
        private void CachedataTabInsert(string cacheName, ref DataTable datTabName)
        {
            HttpRuntime.Cache.Insert(cacheName,
                                datTabName,
                                null,
                                System.DateTime.MaxValue,
                                TimeSpan.Zero,
                                System.Web.Caching.CacheItemPriority.Default,
                                null);
        }
        /* Now Moved to CommonFunctions
           private void cacheRemove(string cacheName)
           {
               HttpRuntime.Cache.Remove(cacheName );
   
           }
            */
    }
}