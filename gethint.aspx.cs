using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.Design; 
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class gethint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     Response.Expires = -1;
     Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<select size=6 id=ajaxSelect  width=400px onkeyup=" + "\"" + "moveKeyBacktoOrgin('ajaxSelect','txt1',event)" + "\">");
     Response.Write("<option value=One>One</option>");
     Response.Write("<option value=two>Two</option>");
     Response.Write("<option value=three>three</option>");
     
     Response.Write(" </select>");  
    }
}
