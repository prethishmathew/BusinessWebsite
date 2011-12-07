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
/// Summary description for discountDetailsClass
/// </summary>
public class discountDetailsClass
{
    private Int64  discountID;
    public Int64 DisCountID 
    { 
        get {return discountID ;}
        set { discountID = value; }
    
    }

    private string bizID;
    public string BizID
    {
        get { return bizID; }
        set { bizID = value; }

    }

    private string header;
    public string Header
    {
        get { return header; }
        set { header = value; }
    
    }

    private string description;
    public string Description
    {
        get { return description; }
        set { description = value; }

    }
    
    private string exceptions;
    public string Exceptions
    {
        get { return exceptions; }
        set { exceptions = value; }

    }
    
    public DateTime  startDate;
    public DateTime StartDate
    {
        get { return startDate; }
        set { startDate = value; }

    }

    public DateTime expiryDate;
    public DateTime ExpiryDate
    {
        get { return expiryDate; }
        set { expiryDate = value; }

    }
    
    private int maxNumberofPrints;    
    public int MaxNumberofPrints
    {
        get { return maxNumberofPrints; }
        set { maxNumberofPrints = value; }

    }

    private int currentPrints;
    public int CurrentPrints
    {
        get { return currentPrints; }
        set { currentPrints = value; }

    }

    private Decimal orgPrice;
    public Decimal OrgPrice
    {
        get { return orgPrice; }
        set { orgPrice = value; }

    }

    private Decimal cpnPrice;
    public Decimal CpnPrice
    {
        get { return cpnPrice; }
        set { cpnPrice = value; }

    }

    //public discountDetailsClass() 
    
    //{ 
    
    //Add Construtor Logic here
    
    //}

    public discountDetailsClass(Int64 discountID, string bizID, string header,string description,
        string exceptions, DateTime startDate, DateTime expiryDate, int maxNumberofPrints,
        int currentPrints, Decimal orgPrice, Decimal cpnPrice)
	{
		//
		// TODO: Add constructor logic here
		//
        DisCountID = discountID;
        BizID = bizID;
        Header = header;
        Description = description;
        Exceptions = exceptions;
        StartDate = startDate;
        ExpiryDate = expiryDate;
        MaxNumberofPrints = maxNumberofPrints;
        CurrentPrints = currentPrints;
        OrgPrice = orgPrice;
        CpnPrice = cpnPrice;
	}

  
}
