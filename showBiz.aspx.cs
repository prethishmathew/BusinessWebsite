using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data.SqlTypes;
using System.Xml;
 

public partial class showBiz : System.Web.UI.Page
{
   public string imgPath = System.Web.VirtualPathUtility.ToAbsolute("~/profileImages/emblem-generic.png");
//    public string imgPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            object user = Page.RouteData.Values["user"];
            //System.Windows.Forms.MessageBox.Show("1" + user.ToString ());
            
            //System.Windows.Forms.MessageBox.Show(user + "1");
            string membershipID = null;

            //Page.Form.Attributes["style"] = string.Format("background: url({0});", "~/Images/myRattlesblueV1.png");
                   
            if (user == null)
            {
                if (Request.QueryString.Count == 0)
                {
                Response.Redirect ( "default.aspx",false  );
                }
                else if (Request.QueryString["id"] != null)
                {

                membershipID = Request.QueryString.GetValues("ID")[0];
                }
                else if (Request.QueryString["user"] != null)
                {

                membershipID =
                    Membership.GetUser(Request.QueryString.GetValues("user")[0]).ProviderUserKey.ToString();

               
                }
            }
        
            else
            {
                if (user != null)
                {
                    membershipID =
                        Membership.GetUser(user.ToString()).ProviderUserKey.ToString();
                }
            }
            //System.Windows.Forms.MessageBox.Show(Page.RouteData.Values["user"].ToString());
            /*= "ab3598eb-3bbe-4c37-a5b8-10334a714b67";*/
            
            //System.Windows.Forms.MessageBox.Show(membershipID);

            if (membershipID == null)
            {
                Response.End();
            }
            if (!IsPostBack)
            {

                dynamicLoad_metaData();
            }


            memIDHide.Value = membershipID;
            db2GenerateDataBse(membershipID);
        }
        catch (Exception err)
        {

            //System.Windows.Forms.MessageBox.Show(err.Message);    
        }
    }

    protected void db2GenerateDataBse(string membershipID)
    {

        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

        using (SqlConnection Conn = new SqlConnection(connStr))
        {

            using (SqlCommand cmd = new SqlCommand("bizDenormalizedDtls_Select"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", membershipID));
                Conn.Open();
                cmd.Connection = Conn;
                using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (rdr.Read())
                    {/*
                     
                        System.Windows.Forms.MessageBox.Show(rdr.GetString(rdr.GetOrdinal("Img2Path")));     
                    */
                        // Build Header for the Web Page
     
                        
                        if (!rdr.IsDBNull(rdr.GetOrdinal("bizName")))
                        {
                            Page.Title = rdr.GetValue(rdr.GetOrdinal("bizName")).ToString() ;
                            genHTML_Graphics(placeHolderHeader, rdr.GetString(rdr.GetOrdinal("bizName")), null, "h2");
                        }

                        if (!rdr.IsDBNull(rdr.GetOrdinal("bizCaption")))
                        {
                            genHTML_Graphics(placeHolderHeader, rdr.GetString(rdr.GetOrdinal("bizCaption")), null, "h3");
                        }

                        if (!rdr.IsDBNull(rdr.GetOrdinal("bizDesc")))
                        {
                            genHTML_Graphics(placeHolderMainMenuHome, rdr.GetString(rdr.GetOrdinal("bizDesc")), null, "div");
                        }


                        buildContactInfo_Graphics(rdr);
                        buildTimingsInfo_Graphics(rdr);
                        buildInventoryMain_Graphics(rdr);
                        buildGallery_Graphics(rdr);
                        
                        if (!rdr.IsDBNull(rdr.GetOrdinal("xmlProductDtls1")))
                        {
                            SqlXml sqlXmlValueMultiple1 = rdr.GetSqlXml(rdr.GetOrdinal("xmlProductDtls1")); 



                        }

                        /*NR0023 */
                        
                        if (!rdr.IsDBNull(rdr.GetOrdinal("bizWebsite")))
                        {
                            if (CommonFunctions.getURLResponse(rdr.GetValue(rdr.GetOrdinal("bizWebsite")).ToString()))
                            { 
                                LiteralControl defListStart = new LiteralControl ( "<li><a id='showDiscounts' href='#ShowBizMainMenuextWeb'>Our Website</a></li>");
                                PlaceHoldershowBizMain.Controls.Add(defListStart);
                                string xmlFile = "<div id='ShowBizMainMenuextWeb'>" +
                   "<iframe id='Iframeext' runat='server' src='" + rdr.GetValue(rdr.GetOrdinal("bizWebsite")).ToString () + "' frameborder='0' scrolling= 'auto' class='iframeTypeA'" +
                   "</div>";
                                LiteralControl iframeBor = new LiteralControl(xmlFile);
                                extWebPlaceholder.Controls.Add(iframeBor );  
                            }
                        }
                        /*NR0023 */

                        /*BackGround Image Changes bizBgImagePath*/

                        if (!rdr.IsDBNull(rdr.GetOrdinal("bizBgImagePath")))
                        {
                            string bgImgLink = rdr.GetValue(rdr.GetOrdinal("bizBgImagePath")).ToString();
                            HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("PageBody");
                            body.Style.Add("background-image", bgImgLink);
                            if (!rdr.IsDBNull(rdr.GetOrdinal("bizBgImageRepeat")))
                            {
                                string repBol = rdr.GetValue(rdr.GetOrdinal("bizBgImageRepeat")).ToString();
                                if (repBol.ToLower () == "yes")
                                {
                                    body.Style.Add("background-repeat","repeat");
                                }
                                else {
                                    body.Style.Add("background-repeat", "no-repeat");
                                }
                            }
                        
                        }

                        if (!rdr.IsDBNull(rdr.GetOrdinal("bizbackGroundColor"))) {
                            string bgImgLink = rdr.GetValue(rdr.GetOrdinal("bizbackGroundColor")).ToString();
                            HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("PageBody");
                            body.Style.Add("background-color", bgImgLink);
                        
                        }

                        if (!rdr.IsDBNull(rdr.GetOrdinal("bizfontColor")))
                        {
                            string bgImgLink = rdr.GetValue(rdr.GetOrdinal("bizfontColor")).ToString();
                            HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("PageBody");
                            body.Style.Add("color", bgImgLink);

                        }

                        if (!rdr.IsDBNull(rdr.GetOrdinal("bizTheme"))) {
                            
                            HtmlLink link = new HtmlLink();
                            link.Href = "~/Styles/" + rdr.GetValue(rdr.GetOrdinal("bizTheme")).ToString();
                            link.Attributes.Add("type", "text/css");
                            link.Attributes.Add("rel", "stylesheet");
                            this.Header.Controls.Add(link);
                            }

                        if (!rdr.IsDBNull(rdr.GetOrdinal("invHeader")))
                        {
                            showInventory.InnerText = rdr.GetValue(rdr.GetOrdinal("invHeader")).ToString();
                        }
                    }
                        rdr.Close();
                }
            }
        }
    }

    protected void buildGallery_Graphics(SqlDataReader reader)
    {

        string ImgCaption, ImgCaptionDB, ImgPath, ImgPathThumbNail, ImgPathGallery,imgLit;
        LiteralControl imgLiteralControl;

        if (!reader.IsDBNull(reader.GetOrdinal("profileImgPath")) && !reader.IsDBNull(reader.GetOrdinal("profileImgPathThumbNail"))
         && !reader.IsDBNull(reader.GetOrdinal("profileImgPathgallery"))
         )
        {
            ImgCaption = "Profile Image";
            if (!reader.IsDBNull(reader.GetOrdinal("profileImgCaption")))
            {
                ImgCaption = reader.GetString(reader.GetOrdinal("profileImgCaption"));
            }

            Image imgProf = new Image();
            //imgLit = "<a  rel='gallery_group' href='" + reader.GetString(reader.GetOrdinal("profileImgPathgallery")) + "' title='" + ImgCaption + "'><img src='" + reader.GetString(reader.GetOrdinal("profileImgPathThumbNail")) + "' alt=''/></a>";
            imgLit = "<a  rel='gallery_group' href='" + reader.GetString(reader.GetOrdinal("profileImgPathgallery")) + "' title='" + ImgCaption + "'>";
            imgProf.ImageUrl = reader.GetString(reader.GetOrdinal("profileImgPathgallery"));
            imgProf.Height = 150;
            imgProf.Width = 150;
            imgProf.BorderWidth = 1;
            imgProf.BorderStyle = BorderStyle.Solid;
            imgProf.BorderColor =  System.Drawing.Color.Gray ;      
            imgLiteralControl = new LiteralControl(imgLit);
            imgLit = "</a>";
            LiteralControl  imgLiteralControlFooter = new LiteralControl(imgLit);
            placeHolderGallery.Controls.Add(imgLiteralControl);
            placeHolderGallery.Controls.Add(imgProf);
            placeHolderGallery.Controls.Add(imgLiteralControlFooter);

        }


        for (int i = 1; i <= 5; i++)
        {
            ImgCaption = "Image " + i;
            ImgCaptionDB = "Img" + i + "Caption"; 
            ImgPath = "Img" + i + "Path";
            ImgPathThumbNail = "Img" + i + "PathThumbNail";
            ImgPathGallery = "Img" + i + "Pathgallery";

            if (!reader.IsDBNull(reader.GetOrdinal(ImgPath)) && !reader.IsDBNull(reader.GetOrdinal(ImgPathThumbNail))
            && !reader.IsDBNull(reader.GetOrdinal(ImgPathGallery))
            )
            {
                if (!reader.IsDBNull(reader.GetOrdinal(ImgCaptionDB)))
                {
                    ImgCaption = reader.GetString(reader.GetOrdinal(ImgCaptionDB));
                }

                //imgLit = "<a  rel='gallery_group' href='" + reader.GetString(reader.GetOrdinal(ImgPathGallery)) + "' title='" + ImgCaption + "'><img src='" + reader.GetString(reader.GetOrdinal(ImgPathThumbNail)) + "' alt=''/></a>";
                imgLit = "<a  rel='gallery_group' href='" + reader.GetString(reader.GetOrdinal(ImgPathGallery)) + "' title='" + ImgCaption + "'>";
                Image imgRef = new Image();
                imgRef.ImageUrl = reader.GetString(reader.GetOrdinal(ImgPathGallery));
                imgRef.Height = 150;
                imgRef.Width = 150;
                imgRef.BorderWidth = 1;
                imgRef.BorderStyle = BorderStyle.Solid;
                imgRef.BorderColor = System.Drawing.Color.Gray;      
            
                imgLiteralControl = new LiteralControl(imgLit);
                imgLit = "</a>";
                LiteralControl imgLiteralControlFooter = new LiteralControl(imgLit);
             
                
                placeHolderGallery.Controls.Add(imgLiteralControl);
                placeHolderGallery.Controls.Add(imgRef);
                placeHolderGallery.Controls.Add(imgLiteralControlFooter);

            }
        }
    
    
    }
   
    protected void buildInventoryMain_Graphics(SqlDataReader reader)
    
    {
        
           
            if (!reader.IsDBNull(reader.GetOrdinal("xmlProductDtls1")))
            {
                SqlXml sqlXmlValue = reader.GetSqlXml(reader.GetOrdinal("xmlProductDtls1"));
                buildInventoryDetails_Graphics(CommonFunctions.isDbReaderNull(reader, reader.GetOrdinal("listName1")), sqlXmlValue, placeHolderA1, 1);
            }
            if (!reader.IsDBNull(reader.GetOrdinal("xmlProductDtls2")))
            {
                SqlXml sqlXmlValue = reader.GetSqlXml(reader.GetOrdinal("xmlProductDtls2"));
                buildInventoryDetails_Graphics(CommonFunctions.isDbReaderNull(reader, reader.GetOrdinal("listName2")), sqlXmlValue, placeHolderB1, 2);
            }

            if (!reader.IsDBNull(reader.GetOrdinal("xmlProductDtls3")))
            {
                SqlXml sqlXmlValue = reader.GetSqlXml(reader.GetOrdinal("xmlProductDtls3"));
                buildInventoryDetails_Graphics(CommonFunctions.isDbReaderNull(reader, reader.GetOrdinal("listName3")), sqlXmlValue, placeHolderC1, 3);
            }
            if (!reader.IsDBNull(reader.GetOrdinal("xmlProductDtls4")))
            {
                SqlXml sqlXmlValue = reader.GetSqlXml(reader.GetOrdinal("xmlProductDtls4"));
                buildInventoryDetails_Graphics(CommonFunctions.isDbReaderNull(reader, reader.GetOrdinal("listName4")), sqlXmlValue, placeHolderD1, 4);
            }
            if (!reader.IsDBNull(reader.GetOrdinal("xmlProductDtls5")))
            {
                SqlXml sqlXmlValue = reader.GetSqlXml(reader.GetOrdinal("xmlProductDtls5"));
                buildInventoryDetails_Graphics(CommonFunctions.isDbReaderNull(reader, reader.GetOrdinal("listName5")), sqlXmlValue, placeHolderD1, 5);
            }

            if (!reader.IsDBNull(reader.GetOrdinal("docPath")))
            {

                Literal invtabLI = new Literal();
                invtabLI.Text = "<li><a href='#UpdatePanelDocs'><span>Downloads</span></a></li> ";
                placeHolderInvContainer.Controls.Add(invtabLI);

                Literal invtabDiv = new Literal();
                invtabDiv.Text = "<div style='width:400px;height:200px;text-align:center;background-color:white;font-size:small; font-weight:bold; color:#669;'><a href='" + reader.GetValue(reader.GetOrdinal("docPath")) + "'>Click to view inventory document</a></div>";
                placeHolderDocs.Controls.Add(invtabDiv);


            }
    }

    protected void buildInventoryDetails_Graphicsold(string listName, SqlXml sqlXMLValue, PlaceHolder placeHolderName,int listNumber) 
    
    {

       
        Literal invtabLI = new Literal();
        invtabLI.Text = "<li><a href='#UpdatePanel" + listNumber + "'><span>" + CommonFunctions.CvtNullSpaces(listName) + "</span></a></li> ";
        placeHolderInvContainer.Controls.Add(invtabLI);  

        int i = 0;
        int row = 0;

        if (!sqlXMLValue.IsNull)
        {
            XmlReader xmlreader = sqlXMLValue.CreateReader();
            LiteralControl defListStart = new LiteralControl ( "<dl>");
            placeHolderName.Controls.Add(defListStart);
            while (xmlreader.Read())
            {
                if (xmlreader.MoveToContent() == XmlNodeType.Element && xmlreader.Name == "item")
                {
                    LiteralControl itemLitrealControl = new LiteralControl 
                        ("<dt>" + xmlreader.ReadString() + "</dt>");
                    placeHolderName.Controls.Add(itemLitrealControl);
                    i += 1;
                    row += 1;
                }
                LiteralControl externalstaLitrealControl = new LiteralControl("<dd>");
                placeHolderName.Controls.Add(externalstaLitrealControl);
                if (xmlreader.MoveToContent() == XmlNodeType.Element && xmlreader.Name == "desc")
                {
                    i += 1;
                    LiteralControl itemLitrealControl = new LiteralControl
                        (xmlreader.ReadString() );
                    placeHolderName.Controls.Add(itemLitrealControl);
                }
                if (xmlreader.MoveToContent() == XmlNodeType.Element && xmlreader.Name == "cost")
                {
                    i += 1;
                    string xmlcost = xmlreader.ReadString();
                    if (string.IsNullOrEmpty(xmlcost))
                    {
                        LiteralControl itemLitrealControl = new LiteralControl
                            (xmlcost);
                        placeHolderName.Controls.Add(itemLitrealControl);
                    }
                }
                LiteralControl externalEndLitrealControl = new LiteralControl("</dd>");
                placeHolderName.Controls.Add(externalEndLitrealControl);
            }

            xmlreader.Close();
            LiteralControl defListend = new LiteralControl("</dl>");
            placeHolderName.Controls.Add(defListend);

        }


    }
    
    protected void buildInventoryDetails_Graphics(string listName, SqlXml sqlXMLValue, PlaceHolder placeHolderName, int listNumber)
    {

        
        Literal invtabLI = new Literal();
        invtabLI.Text = "<li><a href='#UpdatePanel" + listNumber + "'><span>" + CommonFunctions.CvtNullSpaces(listName) + "</span></a></li> ";
        placeHolderInvContainer.Controls.Add(invtabLI);


        string HtmlCode = "<table class='invtab'> <tr> <th style='height: 20px ; width:75% ;text-align:middle; '>" + listName + "</th>  ";
        //HtmlCode += "<th style='height: 20px ; width:75%  >'</th></tr>";
        bool xmlswitch = false;
        
        if (!sqlXMLValue.IsNull)
        {
            XmlReader xmlreader = sqlXMLValue.CreateReader();
            while (xmlreader.Read())
            {
                if (xmlreader.MoveToContent() == XmlNodeType.Element && xmlreader.Name == "item")
                {
                    xmlswitch = false;
                    string itemContent = xmlreader.ReadString();
                    if (string.IsNullOrEmpty(itemContent))
                    {
                        continue;
                    }
                    else
                    {
                        xmlswitch = true;
                        HtmlCode += "<tr><td>";
                        HtmlCode +=
                            ("<dt>" + itemContent + "</dt>");
                    }
                }
                
                if (xmlreader.MoveToContent() == XmlNodeType.Element && xmlreader.Name == "desc" && xmlswitch )
                {
                    HtmlCode +="<div>" +
                        (xmlreader.ReadString()) + "</div></td>";
                }

                if (xmlreader.MoveToContent() == XmlNodeType.Element && xmlreader.Name == "cost" && xmlswitch)
                {
                    string xmlcost = xmlreader.ReadString();
                    HtmlCode += "<td><div >";
                    if (!string.IsNullOrEmpty(xmlcost))
                    {
                        HtmlCode += "$" + xmlcost;
                        
                    }
                    HtmlCode += "</div></td></tr>";
                }
                
                
            }

            xmlreader.Close();
            HtmlCode += "</table>";
            LiteralControl defListend = new LiteralControl(HtmlCode);
            placeHolderName.Controls.Add(defListend);
            

        }


    }

    protected void dynamicLoad_metaData() {

        try
        {
            String csname = "dynumScript";
            String csurl = "~/Scripts/showBiz.js";
            Type cstype = this.GetType();


            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;
            /*
            // Check to see if the include script exists already.
            if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
            {
                cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
            }
            */
            /*******************************************************************************
             * 
             * CODE TO DYNAMICALLY LOAD CSS FILES TO WEBPAGE
             * 
             * CODE starts Here --
             */ 
            
                HtmlLink link = new HtmlLink();
                link.Href = "~/Styles/customThemeRed.css";
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("rel", "stylesheet");
                this.Header.Controls.Add(link); 
        
             /* Code ends Here 
             */


            /*
             * Add fancy Box Settings here
             * 
             */

                

                csname = "jquery4fancyBox";
                csurl = "~/fancybox/jquery.fancybox-1.3.1.js";

                if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
                {
                    cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
                }

                csname = "jqueryeasying4fancyBox";
                csurl = "~/fancybox/jquery.easing-1.3.pack.js";

                if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
                {
                    cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
                }
            /*
                csname = "jquerypack4fancyBox";
                csurl = "~/fancybox/jquery.fancybox-1.3.1.pack.js";

                if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
                {
                    cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
                }
            */
                csname = "mouseWheel4fancyBox";
                csurl = "~/fancybox/jquery.mousewheel-3.0.2.pack.js";

                if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
                {
                    cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
                }
            /*
                csname = "myrattles";
                csurl = "~/Scripts/myRattles.js";
                if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
                {
                    cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
                }
            */
                csname = "dynumScript";
                csurl = "~/Scripts/showBiz.js";
                if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
                {
                    cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
                }

                
                link = new HtmlLink();
                link.Href = "~/fancybox/jquery.fancybox-1.3.1.css";
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("rel", "stylesheet");
                this.Header.Controls.Add(link);

                /*
                 *  HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("PageBody");
                    body.Style.Add("background-image", "profileImages/face-angel-3.png");
                    body.Style.Add("background-repeat","no-repeat");
                */
                
            
        }
        catch (Exception err)
        {

            //System.Windows.Forms.MessageBox.Show(err.Message);
        }
    }

    protected void buildTimingsInfo_Graphics(SqlDataReader reader)
    {

        if (!reader.IsDBNull(reader.GetOrdinal("workhrsstart")) && !reader.IsDBNull(reader.GetOrdinal("workhrsclose")))
        {
            string regTime = " Open Hours : From  " + reader.GetString(reader.GetOrdinal("workhrsstart")) + " to " + reader.GetString(reader.GetOrdinal("workhrsclose"));
            genHTML_Graphics(placeHolderAboutUsBizHrs, regTime, null, "div");
        }
        
        if (!reader.IsDBNull(reader.GetOrdinal("lunchhrstart")) && !reader.IsDBNull(reader.GetOrdinal("lunchhrend")))
        {
            string lunchStart = reader.GetString(reader.GetOrdinal("lunchhrstart"));
            string lunchend = reader.GetString(reader.GetOrdinal("lunchhrend"));
            if (!string.IsNullOrEmpty(lunchStart) && !string.IsNullOrEmpty(lunchend))
            {
                genHTML_Graphics(placeHolderAboutUsBizHrs, "We are closed for lunch between " + reader.GetString(reader.GetOrdinal("lunchhrstart")) + " and "
                + reader.GetString(reader.GetOrdinal("lunchhrend")) + "<br><br>", null, "i");
            }
        }

        if (!reader.IsDBNull(reader.GetOrdinal("splDay1")) && !reader.IsDBNull(reader.GetOrdinal("splTime1From")) && !reader.IsDBNull(reader.GetOrdinal("splTime1To"))) 
        {
            
            string spl1Msg = null  ;

            switch (reader.GetString(reader.GetOrdinal("splDay1")))
            { 
                case "Mon":
                    spl1Msg = "On Mondays' we are open from " + reader.GetString(reader.GetOrdinal("splTime1From")) + " to " + reader.GetString(reader.GetOrdinal("splTime1To"));
                    break;
                case "Tue":
                    spl1Msg = "On Tuesdays' we are open from " + reader.GetString(reader.GetOrdinal("splTime1From")) + " to " + reader.GetString(reader.GetOrdinal("splTime1To")); 
                    break;
                case "Wed":
                    spl1Msg = "On Wednesdays' we are open from " + reader.GetString(reader.GetOrdinal("splTime1From")) + " to " + reader.GetString(reader.GetOrdinal("splTime1To"));
                    break;
                case "Thur":
                    spl1Msg = "On Thursdays' we are open from " + reader.GetString(reader.GetOrdinal("splTime1From")) + " to " + reader.GetString(reader.GetOrdinal("splTime1To"));
                    break;
                case "Fri":
                    spl1Msg = "On Fridays' we are open from " + reader.GetString(reader.GetOrdinal("splTime1From")) + " to " + reader.GetString(reader.GetOrdinal("splTime1To"));
                    break;
                case "Sat":
                    spl1Msg = "On Saturdays' we are open from " + reader.GetString(reader.GetOrdinal("splTime1From")) + " to " + reader.GetString(reader.GetOrdinal("splTime1To"));
                    break;
                case "Sun":
                    spl1Msg = "On Sundays' we are open from " + reader.GetString(reader.GetOrdinal("splTime1From")) + " to " + reader.GetString(reader.GetOrdinal("splTime1To"));
                    break;
            }

            

            if (!string.IsNullOrEmpty(spl1Msg))
            {
                genHTML_Graphics(placeHolderAboutUsBizHrs, spl1Msg, null, "div");
            
            }
        }
        if (!reader.IsDBNull(reader.GetOrdinal("splDay2")) && !reader.IsDBNull(reader.GetOrdinal("splTime2From")) && !reader.IsDBNull(reader.GetOrdinal("splTime2To")))
        {

            string splMsg = null;

            switch (reader.GetString(reader.GetOrdinal("splDay2")))
            {
                case "Mon":
                    splMsg = "On Mondays' we are open from " + reader.GetString(reader.GetOrdinal("splTime2From")) + " to " + reader.GetString(reader.GetOrdinal("splTime2To"));
                    break;
                case "Tue":
                    splMsg = "On Tuesdays' we are open from " + reader.GetString(reader.GetOrdinal("splTime2From")) + " to " + reader.GetString(reader.GetOrdinal("splTime2To"));
                    break;
                case "Wed":
                    splMsg = "On Wednesdays' we are open from " + reader.GetString(reader.GetOrdinal("splTime2From")) + " to " + reader.GetString(reader.GetOrdinal("splTime2To"));
                    break;
                case "Thur":
                    splMsg = "On Thursdays' we are open from " + reader.GetString(reader.GetOrdinal("splTime2From")) + " to " + reader.GetString(reader.GetOrdinal("splTime2To"));
                    break;
                case "Fri":
                    splMsg = "On Fridays' we are open from " + reader.GetString(reader.GetOrdinal("splTime2From")) + " to " + reader.GetString(reader.GetOrdinal("splTime2To"));
                    break;
                case "Sat":
                    splMsg = "On Saturdays' we are open from " + reader.GetString(reader.GetOrdinal("splTime2From")) + " to " + reader.GetString(reader.GetOrdinal("splTime2To"));
                    break;
                case "Sun":
                    splMsg = "On Sundays' we are open from " + reader.GetString(reader.GetOrdinal("splTime2From")) + " to " + reader.GetString(reader.GetOrdinal("splTime2To"));
                    break;
            }
            if (!string.IsNullOrEmpty(splMsg))
            {
                genHTML_Graphics(placeHolderAboutUsBizHrs, splMsg, null, "div");

            }
        }

        if (!reader.IsDBNull(reader.GetOrdinal("closedays")))
        {
            string ClosedDayMsg = null;
            string[] Closeddays = reader.GetString(reader.GetOrdinal("closedays")).Split(';');
            //Use arrayCount in for statement 'cause the .Count is evoked each time the loop is executed.

            int arrayCount = Closeddays.Count();

            for (int j = 0; j < arrayCount; j++)
            {
                switch (Closeddays[j])
                {
                    case "Sun":
                        ClosedDayMsg = ClosedDayMsg + ",Sunday";
                        break;
                    case "Mon":
                        ClosedDayMsg = ClosedDayMsg + ",Monday";
                        break;
                    case "Tue":
                        ClosedDayMsg = ClosedDayMsg + ",Tuesday";
                        break;
                    case "Wed":
                        ClosedDayMsg = ClosedDayMsg + ",Wednesday";
                        break;
                    case "Thur":
                        ClosedDayMsg = ClosedDayMsg + ",Thursday";
                        break;
                    case "Fri":
                        ClosedDayMsg = ClosedDayMsg + ",friday";
                        break;
                    case "Sat":
                        ClosedDayMsg = ClosedDayMsg + ",Saturday";
                        break;
                }
            }
            if (Closeddays.Count() > 0)
            {
                genHTML_Graphics(placeHolderAboutUsBizHrs, "We are closed on : " + ClosedDayMsg.TrimStart(','), null, "p");
            }
        }

    }
    
    protected void buildContactInfo_Graphics(SqlDataReader reader)
    {
        string CityStateZipCode = "";
        string phoneNumber = "<b>Phone Number : </b>";
        string emailadd    = "<b>Email Address : </b>";
        genHTML_Graphics(placeHolderAboutUsContactInfo, "Contact Address:", null, "dt");

        if (!reader.IsDBNull(reader.GetOrdinal("address1")))
        {
        genHTML_Graphics(placeHolderAboutUsContactInfo, reader.GetString(reader.GetOrdinal("address1"))  , null, "dd");
        }
        if (!reader.IsDBNull(reader.GetOrdinal("address2")))
        {
            genHTML_Graphics(placeHolderAboutUsContactInfo, reader.GetString(reader.GetOrdinal("address2")), null, "dd");
        }
        if (!reader.IsDBNull(reader.GetOrdinal("City")))
        {
            CityStateZipCode = reader.GetString(reader.GetOrdinal("City"));
        
        }
        if (!reader.IsDBNull(reader.GetOrdinal("state")))
        {
            CityStateZipCode = CityStateZipCode + "," +reader.GetString(reader.GetOrdinal("state"));

        }

        if (!reader.IsDBNull(reader.GetOrdinal("zipCode")))
        {
            CityStateZipCode = CityStateZipCode + " - " + reader.GetString(reader.GetOrdinal("zipCode"));

        }

        genHTML_Graphics(placeHolderAboutUsContactInfo, CityStateZipCode, null, "dd");


        if (!reader.IsDBNull(reader.GetOrdinal("contactPhone")))
        {
            phoneNumber = phoneNumber + reader.GetString(reader.GetOrdinal("contactPhone"));
            genHTML_Graphics(placeHolderAboutUsContactInfo, phoneNumber, null, "p");
        }

        if (!reader.IsDBNull(reader.GetOrdinal("contactEmail")))
        {
            emailadd = emailadd + reader.GetString(reader.GetOrdinal("contactEmail"));
            genHTML_Graphics(placeHolderAboutUsContactInfo, emailadd , null, "p");
        }
    }

    protected void genHTML_Graphics (PlaceHolder showBizPlaceHolder, string htmlStr, string htmlClass,string wrap)

    {
        LiteralControl literalHtml;
        if (htmlClass == null) {
            literalHtml = new LiteralControl("<" + wrap + ">" + htmlStr + "</" + wrap + ">");
        }
        else
       
        {
        
            literalHtml = new LiteralControl("<" + wrap + " class=" + htmlClass + ">" + htmlStr + "</" + wrap + ">");
        }

        showBizPlaceHolder.Controls.Add(literalHtml); 
    } 

}

