using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for zipCodeUtil
/// </summary>
public class zipCodeUtil
{
    private const double kEarthRadiusMiles = 3956.0;
    
    private double bottomLatLine;
    public double BottomLatLine
    {
        get { return bottomLatLine; }
        set { bottomLatLine = value; }

    }
    
    private double topLatLine;
    public double TopLatLine
    {
        get { return topLatLine; }
        set { topLatLine = value; }

    }
    
    private double leftLongLine;
    public double LeftLongLine
    {
        get { return leftLongLine; }
        set { leftLongLine = value; }

    }
    
    private double rightLongLine;
    public double RightLongLine
    {
        get { return rightLongLine; }
        set { rightLongLine = value; }
    }
    
    private double _dRadius;
			
    private int rtnCode;
    public int RtnCode
    {
        get { return rtnCode; }
        set { rtnCode = value; }

    }

    private string   rtnmessage;
    public string  Rtnmessage
    {
        get { return rtnmessage; }
        set { rtnmessage = value; }

    }

    private string zipCode;
    public string ZipCode
    {
        get { return zipCode; }
        set { zipCode = value; }

    }

    private string city;
    public string City
    {
        get { return city; }
        set { city = value; }

    }

    private string stateCode;
    public string StateCode
    {
        get { return stateCode; }
        set { stateCode = value; }

    }

    private double lat;
    public double Lat
    {
        get { return lat; }
        set { lat = value; }

    }

    private double lon;
    public double Lon
    {
        get { return lon; }
        set { lon = value; }

    }

    private double distance;
    public double Distance
    {
        get { return distance; }
        set { distance = value; }

    }

    private int[] radiusBoxZips;
    public int[] RadiusBoxZips
    {
        get { return radiusBoxZips; }
        set { radiusBoxZips = value; }

    }
    
	public zipCodeUtil(string zipOrCityState,int distance)
	{
        RtnCode = 0;
        if (CommonFunctions.isNumeric(zipOrCityState))
        {
            getLocfromZip(zipOrCityState, distance);
        }

        else
        {
            if (CommonFunctions.validCityStateFormat(zipOrCityState))
            {
                getLocfromCityState(zipOrCityState, distance);
            }
            else
            {
                errorRoutine("Invalid Zipcode or City,State not in correct format", 400);
            }
            
        }
    }
       
    private void getLocfromZip(string zip,int distance)
    {
        try
        {
            XDocument xmlDoc = CommonFunctions.generateXdoc("xmlCityStateconvertBooks", "XmlData/Convertbooks.xml");

            if (xmlDoc != null)
            {
                var apps = (from book in xmlDoc.Descendants("zip")
                            where book.Attribute("code").Value.Equals(zip)
                            select new
                            {
                                lat1 = book.Element("lat").Value,
                                lon1 = book.Element("long").Value,
                                city1 = book.Element("city").Value,
                                state1 = book.Element("state").Value
                            });



                //System.Windows.Forms.MessageBox.Show("Latitude - for " + zip + " is " + apps.First().lat1);     
                if (null != apps.First().lat1 && null != apps.First().lon1)
                {
                    this.zipCode = zip;
                    this.lat = Convert.ToDouble(apps.First().lat1.ToString());
                    this.lon = Convert.ToDouble(apps.First().lon1);
                    this.city = apps.First().city1;
                    this.stateCode = apps.First().state1;
                    this.distance = distance;
                    this.getBoxCorordinates();

                }
               
            }
            else
                errorRoutine("Server Error: Unable to process record", 910);
        }
        catch (Exception err)
        {
            //System.Windows.Forms.MessageBox.Show("Error " + err.Message );
           errorRoutine(err.Message,410)  ;
        }
           
    }
    
    private void getLocfromCityState(string CityState, int distance)
    {
     //   System.Windows.Forms.MessageBox.Show(" In city " + CityState);
        string[] cityInfo = CityState.Split(',');
        City =  CommonFunctions.standardizeCity(cityInfo[0]);
        StateCode = cityInfo[1].ToUpper() ;
        
        XDocument xmlDoc = CommonFunctions.generateXdoc("xmlCityStateconvertBooks", "XmlData/Convertbooks.xml");
        
        if (xmlDoc != null)
        {
            try
            {

                var apps = (from book in xmlDoc.Descendants("zip")
                            where book.Element("city").Value.Equals(City)
                            && book.Element("state").Value.Equals(StateCode)
                            select new
                            {
                                lat1 = book.Element("lat").Value,
                                lon1 = book.Element("long").Value,
                                zip = book.Attribute("code").Value,
                                city1 = book.Element("city").Value,
                                state1 = book.Element("state").Value
                            });


                if (null != apps.First().lat1 && null != apps.First().lon1)
                {
                    this.zipCode = apps.First().zip;
                    this.lat = Convert.ToDouble(apps.First().lat1);
                    this.lon = Convert.ToDouble(apps.First().lon1);
                    this.city = apps.First().city1;
                    this.stateCode = apps.First().state1;
                    this.distance = distance;
                    this.getBoxCorordinates();

                }
                
            }
            catch (Exception )
            {
                errorRoutine("No Cities found",450) ;
            }
        }
        else {
            errorRoutine("Server Error: Unable to process record", 905);
        }

    }   
       
    private void getBoxCorordinates() {

        double dlat;
        double dlon;
        double dLatInRads = this.lat * (Math.PI / 180.0); 
        double dLongInRads = this.lon * (Math.PI / 180.0);
        double dDistInRad = this.distance / kEarthRadiusMiles;
        
        this.TopLatLine   = dLatInRads + dDistInRad;
        this.TopLatLine *= (180.0 / Math.PI);

        this.BottomLatLine = dLatInRads - dDistInRad;
        this.BottomLatLine *= (180.0 / Math.PI);

        dlat = Math.Asin(Math.Sin(dLatInRads) * Math.Cos(dDistInRad));
        dlon = Math.Atan2(Math.Sin(Math.PI / 2.0) * Math.Sin(dDistInRad) * Math.Cos(dLatInRads), Math.Cos(dDistInRad) - Math.Sin(dLatInRads) * Math.Sin(dlat));
        this.RightLongLine = ((dLongInRads + dlon + Math.PI) % (2.0 * Math.PI)) - Math.PI;
        this.RightLongLine *= (180.0 / Math.PI);

        dlon = Math.Atan2(Math.Sin(3.0 * Math.PI / 2.0) * Math.Sin(dDistInRad) * Math.Cos(dLatInRads), Math.Cos(dDistInRad) - Math.Sin(dLatInRads) * Math.Sin(dlat));
        this.LeftLongLine = ((dLongInRads + dlon + Math.PI) % (2.0 * Math.PI)) - Math.PI;
        this.LeftLongLine *= (180.0 / Math.PI);
 
    }

    private void nullValuesinData(string strMessage)
    {

        this.rtnCode = 100;

    }

    private void errorRoutine(string errMessage, int errCode)
    {

        Rtnmessage = errMessage;
        RtnCode = errCode;
    
    }

    public int[] getZipCodesInRange(int distance)
    
    {

        this.distance = distance;
        getBoxCorordinates();
        int i = 0;
        try
        {
            XDocument xmlDoc = CommonFunctions.generateXdoc("xmlCityStateconvertBooks", "XmlData/Convertbooks.xml");   
            if (xmlDoc != null)
            {

                var apps = (from book in xmlDoc.Descendants("zip")
                            where (double)book.Element("lat") <= this.TopLatLine // Northern Latt Line 
                            && (double)book.Element("lat") >= this.BottomLatLine  // Southern Latt Line
                            && (double)book.Element("long") <= this.RightLongLine // Eastern Long Line
                            && (double)book.Element("long") >= this.leftLongLine  // western Long Line                       
                            /*select new
                            {
                               zippy = book.Attribute("code").Value */
                            select new { tot = (int)book.Attribute("code") }
                            );
               //system.windows.forms.messagebox.Show("Apps Count " + apps.Count().ToString());

                if (apps.Count() > 0)
                {

                    RadiusBoxZips = new int[apps.Count()];
                }
                foreach (var Inuem in apps)
                {
                    i++;
                    //System.Windows.Forms.MessageBox.Show("Zippy " + Inuem.tot.ToString());       
                    RadiusBoxZips.SetValue(Inuem.tot, i - 1);
                    
                }
            }
            else
                errorRoutine("Server error: xdoc not found", 912);
        }
        
        catch (Exception err) {
            errorRoutine("Error Processing Zipcodes", 465);
        }

        return this.radiusBoxZips;
    
    }

    
}
