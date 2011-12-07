using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class akgment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString.Count == 0)
        {

            Response.End();

        }
        else
        {
            string pageName = Request.QueryString["pageName"];
            try
            {
                LiteralControl htmlData = new LiteralControl();
                switch (pageName)
                {
                        
                    case "Acknowledgement":
                         htmlData = new LiteralControl(
                            "We may have never met or spoken to anyone of these tech gurus but their guidance and posting over the internet has helped us immensely become what we are today. " +
                            "<ul>" +
                            "<li><a href='http://jquery.com/'>Jquery Team @ jquery.com</a></li>" +
                            "<li><a href='http://www.malsup.com/jquery/'>Jquery Cycle Lite</a></li>" +
                            "<li><a href='http://www.addictedtocoffee.de/'>Oliver Twardowski</a></li>" +
                            "<li><a href='http://www.asp.net'>Asp.net</a></li>" +
                            "<li><a href='http://www.scottgu.com/'>Scott Guthrie</a></li>" +
                            "<li><a href='fancybox.net'>Fancy Box</a></li>" +
                            "<li><a href='http://jquery.com/'>Jonathan chaffer and Karl Swedberg</a></li>" +
                           "<li><a href='http://www.favicon.cc'>Favicon</a></li></ul>" +
                           "If we have missed out anyone please email us at support@patRates.com and we would be glad to add you to the list. ")
                           ;
                        dataHolder1.Controls.Add(htmlData);
                        break;
                    case "InvestorRelations":
                        htmlData = new LiteralControl(
                            "</br><br><h2>Interested in Investing with us?  </h2>" +
                            "<u>Investors mail us at:</u></br></br>" +
                            "<b> New Investor Relations </b></br>" +
                            "34930 363 Place, </br>Aitkin, MN - 56431"
                            );
                        dataHolder1.Controls.Add(htmlData);
                        break;
                    case "contactUs":
                        htmlData = new LiteralControl(
                            "</br></br>Mail us at:</br></br>" +
                            "<b> myRattles Inc </b></br>" +
                            "34930 363 Place, </br>Aitkin, MN - 56431"+
                            "</br><h4>For ongoing information about myRattles you can read our <a href='http://www.patrates.blogspot.com/' target='_blank' >Corporate Blog</a></h4>" 
                            
                            );
                        dataHolder1.Controls.Add(htmlData);
                        break;
                    case "aboutUs":
                        htmlData = new LiteralControl(
                            "</br></br>myRattles was built with idea to bring local business closer to their customers.Started in 2010 in the nothern Minnesottan town of Aitkin," +
                            "myRattles is the way for people to effectively know more about their local businesses and events in an orderly manner. We provide a platform where local businesses could instantly communicate with their customers through e-mail, web or phone.");
                        dataHolder1.Controls.Add(htmlData);
                            break;
                   
                    default:
                        break;

                }
            }
            catch (Exception err)
            {

                //System.Windows.Forms.MessageBox.Show(err.Message);     
            }
        }
    }
}
