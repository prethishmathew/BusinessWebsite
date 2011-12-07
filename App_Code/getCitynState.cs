using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

/// <summary>
/// Summary description for getCitynState
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class getCitynState : System.Web.Services.WebService {

    [WebMethod]

    public string citySuggestion () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        string results = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<select size=6 id=ajaxSelect  width=400px onkeyup=" + "\"" + "moveKeyBacktoOrgin('ajaxSelect','txt1',event))";
        results = "<option value=One>1</option>";
        results = results + "<option value=Two>2</option>";
        results = results + "<option value=Three>3</option>";
        return results;
    }

    
    public string HelloWorld() {
        return "Hello World";
    }
    
}


