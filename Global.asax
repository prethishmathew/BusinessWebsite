<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="apiMyrattles" %>
<%@ Import Namespace="System.Threading" %>


<script runat="server">


    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup        
        RegisterRoutes(RouteTable.Routes);
        
        Thread t = new Thread(new ThreadStart(this.openAppPoolonwebService));
        t.Start();  
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        
        // Code that runs when an unhandled error occurs
        //string lastException = Context.Error.Message + "###" + Context.Error.InnerException;
        //Context.ClearError(); 
        //Response.Redirect("customError.aspx?Error=" + lastException);

        //System.Windows.Forms.MessageBox.Show(Context.Error.Source + Context.CurrentNotification.ToString () + Context.Request.FilePath       );        
        //Response.Redirect("GenericErrorPage.htm");  
        
        
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    void RegisterRoutes(RouteCollection routes)
        
    {
        //routes.Ignore("*.aspx");
        routes.Ignore("{resource}.axd/{*pathInfo}");
        routes.MapPageRoute("", "{user}", "~/showBiz.aspx");

    }

    void openAppPoolonwebService()
    {
        // Dummy call to http://api.myrattles.com/service.svc to start appPool. 
        try
        {
            ServiceClient client = new ServiceClient();
            client.Open();
            string patrates = client.wakeUpPool("fromPatrates.com");
            client.Close();
        }
        catch (Exception ERR)
        {
         //   System.Windows.Forms.MessageBox.Show(ERR.Message + ERR.InnerException );     
        }
        
    }
       
</script>
