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
using System.Text;
using System.Security.Cryptography;
/// <summary>
/// Summary description for S3Helper
/// </summary>
public class S3Helper
{
    public S3Helper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string FormatTimeStamp(DateTime myTime)
    {
        DateTime myUniversalTime = myTime.ToUniversalTime();
        return myUniversalTime.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", System.Globalization.CultureInfo.InvariantCulture);
    }

    public static DateTime GetTimeStamp(DateTime myTime)
    {
        DateTime myUniversalTime = myTime.ToUniversalTime();
        DateTime myNewTime = new DateTime(myUniversalTime.Year, myUniversalTime.Month, myUniversalTime.Day,
                                 myUniversalTime.Hour, myUniversalTime.Minute, myUniversalTime.Second,
                                 myUniversalTime.Millisecond);

        return myNewTime;

    }

    public static string GetSignature(string mySecretAccessKeyId, string strOperation, DateTime myTime)
    {
        Encoding myEncoding = new UTF8Encoding();

        // Create the source string which is used to create the digest
        string mySource = "AmazonS3" + strOperation + FormatTimeStamp(myTime);

        // Create a new Cryptography class using the 
        // Secret Access Key as the key
        HMACSHA1 myCrypto = new HMACSHA1(myEncoding.GetBytes(mySecretAccessKeyId));

        // Convert the source string to an array of bytes
        char[] mySourceArray = mySource.ToCharArray();

        // Convert the source to a UTF8 encoded array of bytes
        byte[] myUTF8Bytes = myEncoding.GetBytes(mySourceArray);

        // Calculate the digest 
        byte[] strDigest = myCrypto.ComputeHash(myUTF8Bytes);

        return Convert.ToBase64String(strDigest);
    }


}
