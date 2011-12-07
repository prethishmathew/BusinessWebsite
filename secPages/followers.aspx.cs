using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient ;

public partial class secPages_followers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dynamicLoad_metaData();
        loadMyShopsDataset();
       
    }


    protected void dynamicLoad_metaData()
    {

        try
        {

            HtmlLink link = new HtmlLink();
            link.Href = "~/Styles/eggPlantModified.css";
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            this.Header.Controls.Add(link);

            String csname = "dynumScript";
            String csurl = "~/Scripts/showBiz.js";
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;
            csname = "dynumScript";
            csurl = "~/Scripts/followers.js";
            if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
            {
                cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
            }
            
        }
        catch (Exception err)
        {

            //System.Windows.Forms.MessageBox.Show(err.Message);
        }
    }

    protected void loadMyShopsDataset()
    {
        try
        {
           CommonFunctions.getFansOrShopsDtXml("someCache", "SNSelectMyFans", "myShop");
        }
        catch (SqlException EX)
        {
            System.Windows.Forms.MessageBox.Show(EX.Message);
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show (ex.Message); 
        }
        }
}
