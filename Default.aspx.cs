using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Configuration ;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //searchBtn_Click(sender,e );
         //   loadCSSNScript();
        
        
    }

    protected void loadCSSNScript()
    {

        try
        {

            String csname = "CommonScript";
            String csurl = "~/Scripts/Default.js";
            Type cstype = this.GetType();
           // System.Windows.Forms.MessageBox.Show("er1");
            // Get a ClientScriptManager reference from the Page class.
            
            ClientScriptManager cs = Page.ClientScript;

            // Check to see if the include script exists already.
            if (!cs.IsClientScriptIncludeRegistered(cstype, csname))
            {
                cs.RegisterClientScriptInclude(cstype, csname, ResolveClientUrl(csurl));
             //   System.Windows.Forms.MessageBox.Show("er2");
            }

            
        }
        catch (Exception err)
        {


            //System.Windows.Forms.MessageBox.Show(err.Message);
        }
    }

    
    
    
    protected void searchBtn_Click(object sender, EventArgs e)
    {

        
        /*
        SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource1.SelectCommand = "bizDenormalizedDtls_Select";
        SqlDataSource1.SelectParameters.Clear();
        SqlDataSource1.SelectParameters.Add("ID", TypeCode.String, membershipID );
        SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
        GridView1.DataSource = SqlDataSource1.Select(DataSourceSelectArguments.Empty);  
        */
        /*
       String  ConnStr = string.Empty;
        ConnStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

        using (SqlConnection connection =
                new SqlConnection(ConnStr))
        {
            SqlCommand command = new SqlCommand("bizDenormalizedDtls_SelectMultiple", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Clear();  
            command.Parameters.Add(new SqlParameter("@zipCodeList", SqlDbType.VarChar, 5000));
            command.Parameters["@zipCodeList"].Value = "11554,56431";
            command.Parameters.Add(new SqlParameter("@centreZip", SqlDbType.VarChar, 10));
            command.Parameters["@centreZip"].Value = "11554";

            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            
            DataSet ds = new DataSet ();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();  
        }

        //SqlDataSource1.DataBind();
        //GridView1.DataBind();  
          */     
    
    }
    
}
