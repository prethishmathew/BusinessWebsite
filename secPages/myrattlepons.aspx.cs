using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class secPages_myrattlepons : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dynamicLoad_metaData();
    }

    protected void dynamicLoad_metaData()
    {

        try
        {

            
            String csname = "dynumScript";
            String csurl = "~/Scripts/showBiz.js";
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;
            csname = "dynumScript";
            csurl = "~/Scripts/myrattlepons.js";
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
}