using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for datTabJSON
/// </summary>
/// 
[Serializable] 
public class datTabJSON
{
/*
 * Error Types : 904    : User Not Authenticated;
 *               999    : Unhandled Error;
 *               000    : Success;
 *               100    : No Data Found;
 */

	public datTabJSON()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string sEcho { get; set; }     
    public int iTotalRecords { get; set; }     
    public int iTotalDisplayRecords { get; set; }     
    //public string[,] aaData { get; set; }
    public List<List<string>> aaData { get; set; }
    public List<List<string>> iDataGrp { get; set; }
    public string sColumns { get; set; }
    public int sError { get; set; }

}
