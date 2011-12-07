using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Configuration;
using System.Net;
using System.IO;
using System.Data;
public partial class secPages_Cpnthankyou : System.Web.UI.Page
{
    string authToken, txToken, query;
    string strResponse;
    decimal GrossTotal;
    int    InvoiceNumber;
    string PaymentStatus; 
    string PayerFirstName;
    double PaymentFee ;
    string BusinessEmail; 
    string PayerEmail;
    string TxToken;
    string PayerLastName ;
    string ReceiverEmail ;
    string ItemName; 
    string ItemNumber; 
    string Currency; 
    string TransactionId; 
    string SubscriberId;
    string Custom;
    string paymentDate;
    string paymentType;
    string pendingReason;
    string txType;
    string rcptID;
    string receiverID;
    string payerbizName;
    //int paymentStatus;
    bool isError = true;
    int errorNumber = 0;
    string errMsg = "";     
           
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            //read in txn token from querystring
            txToken = Request.QueryString.Get("tx");
            authToken = WebConfigurationManager.AppSettings["paypalPDTAuthKey"];

            query = string.Format("cmd=_notify-synch&tx={0}&at={1}",
                                  txToken, authToken);
            string url = WebConfigurationManager.AppSettings["PayPalSubmitUrl"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            // Write the request back IPN strings
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();

            // Do the request to PayPal and get the response
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();

            // sanity check
            //Label2.Text = strResponse;

            // If response was SUCCESS, parse response string and output details
            // System.Windows.Forms.MessageBox.Show(strResponse);    
            if (strResponse.StartsWith("SUCCESS"))
            {
                String[] StringArray = strResponse.Split('\n');

                int i;
                for (i = 1; i < StringArray.Length - 1; i++)
                {
                    String[] StringArray1 = StringArray[i].Split('=');

                    string sKey = StringArray1[0];
                    string sValue = HttpUtility.UrlDecode(StringArray1[1]);

                    // set string vars to hold variable names using a switch
                    switch (sKey)
                    {
                        case "mc_gross":
                            if (!decimal.TryParse(sValue, out GrossTotal )) { GrossTotal  = 0.0M; }
                            break;

                        case "invoice":
                            if (!Int32.TryParse(sValue, out InvoiceNumber)) { InvoiceNumber = 0; }
                            break;

                        case "payment_status":
                            PaymentStatus = Convert.ToString(sValue);
                            break;

                        case "first_name":
                            PayerFirstName = Convert.ToString(sValue);
                            break;

                        case "mc_fee":
                            PaymentFee = Convert.ToDouble(sValue);
                            if (!Double.TryParse(sValue, out PaymentFee )) { PaymentFee  = 0.0; }
                            break;

                        case "business":
                            BusinessEmail = Convert.ToString(sValue);
                            break;

                        case "payer_email":
                            PayerEmail = Convert.ToString(sValue);
                            break;

                        case "Tx Token":
                            TxToken = Convert.ToString(sValue);
                            break;

                        case "last_name":
                            PayerLastName = Convert.ToString(sValue);
                            break;

                        case "receiver_email":
                            ReceiverEmail = Convert.ToString(sValue);
                            break;

                        case "item_name":
                            ItemName = Convert.ToString(sValue);
                            break;

                        case "item_number":
                            ItemNumber = Convert.ToString(sValue);
                            break;

                        case "mc_currency":
                            Currency = Convert.ToString(sValue);
                            break;

                        case "txn_id":
                            TransactionId = Convert.ToString(sValue);
                            break;

                        case "custom":
                            Custom = Convert.ToString(sValue);
                            break;

                        case "subscr_id":
                            SubscriberId = Convert.ToString(sValue);
                            break;

                        case "payment_date":
                            paymentDate = Convert.ToString(sValue);
                            break;

                        case "payment_type":
                            paymentType = Convert.ToString(sValue);
                            break;

                        case "pending_reason":
                            pendingReason = Convert.ToString(sValue);
                            break; 

                        case "txn_type":
                            txType = Convert.ToString(sValue);
                            break;  
                        case "receipt_id":
                            rcptID = Convert.ToString(sValue);
                            break; 
                        case "receiver_id":
                            receiverID = Convert.ToString(sValue);
                            break;   
                   
                         case "payer_business_name ":
                            payerbizName = Convert.ToString(sValue);
                            break;   
                   
                    }
                }


                DataTable dt = patRatesFunctions.paypalItems.getCouponInfo( InvoiceNumber,ItemNumber );
                
                if (dt != null && dt.Rows.Count > 0 )
                {
                    DataRow dr = dt.Rows[0];
                    isError = false ;
                    if (BusinessEmail != "pradis_1316191084_biz@gmail.com")
                    {
                        isError = true;
                        errMsg = "External error:Authentication Failure -ER2345";
                        errorNumber = 8;
                    
                    }

                    if (Convert.ToInt32(dr["couponNumber"]) != InvoiceNumber)
                    {
                        isError = true;
                        errMsg = "External error:Authentication Failure -ER2346";
                        errorNumber = 7;
                    }

                    if (dr["couponIdentifier"].ToString () != ItemNumber )
                    {
                        isError = true;
                        errMsg = "External error:Authentication Failure -ER2347";
                        errorNumber = 7;
                    }

                    if (Convert.ToDecimal  (dr["totalAmount"]) != GrossTotal )
                    {
                        isError = true;
                        errMsg = "External error:Authentication Failure -ER2348";
                        errorNumber = 5;
                    }
                    

                    if (Currency.ToUpper()  != "USD") {
                        
                        isError = true;
                        errMsg = "External error:Authentication Failure -ER2349";
                        errorNumber = 5;
                    }
                    
                    
                    }
                thanks.InnerText = string.Format("Thank you {0} {1} [{2}] for your payment of {3} {4}! Your payment status is {5} for coupon number {6} and invoice is {7}",
                                PayerFirstName, PayerLastName, PayerEmail, GrossTotal, Currency, PaymentStatus,ItemNumber,InvoiceNumber  );
            }
            else
            {
                isError = true;
                thanks.InnerText = "There seems to be some error in your transaction, Please wait while we investigate. Please check your email for any futher information ...";
            }

            if (isError)
            {

                header.InnerHtml = "Oops... An Error !!!";
                thanks.InnerHtml = "There was an error processing your request. If you think there was any problem please send a email to support for help with your transaction number </br>";
                thanks.InnerHtml += "Transaction Number " + TransactionId + "<br> Error : " + errMsg;

            }
            else
            {
                switch (PaymentStatus)
                {
                    case "Processed":
                        errorNumber = 2;
                        break;
                    case "Completed":
                        errorNumber = 2;
                        break;
                    case "Pending":
                        errorNumber = 3;
                        break;
                    default :
                        errorNumber = 9;
                        isError = true;
                        break;
                }
                patRatesFunctions.paypalItems.updateCouponStatus(InvoiceNumber, ItemNumber, txType, BusinessEmail, rcptID, receiverID, txToken,
                    PayerFirstName, PayerLastName, payerbizName, PayerEmail, ItemName, ItemNumber,  GrossTotal   , paymentDate, PaymentStatus, paymentType,
                    pendingReason, errorNumber, 0);

                if (!isError)
                {
                    header.InnerHtml = "Thank You !! Check your email for your new coupon";
                    thanks.InnerText = string.Format("Thank you {0} {1} [{2}] for your payment of {3} {4}! Your payment status is {5} for coupon number {6}. Please check your email {7} for coupon. Make sure to have myrattles.com and patrates.com removed from your spam list and added to your list of trusted websites. ",
                                PayerFirstName, PayerLastName, PayerEmail, GrossTotal, Currency, PaymentStatus, ItemNumber, InvoiceNumber, PayerEmail);

                }
                else
                {
                    patRatesFunctions.paypalItems.bumpCouponCount(InvoiceNumber);  
                    header.InnerHtml = "Oops... An Error !!!";
                    thanks.InnerHtml = "There was an error processing your request. If you think there was any problem please send a email to support for help with your transaction number </br>";
                    thanks.InnerHtml += "Transaction Number " + TransactionId + "<br> Error : " + errMsg;

                }
            }
                
                
                

            }
        
        }

    }