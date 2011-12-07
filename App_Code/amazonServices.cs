using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.amazonaws.s3;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security;

/// <summary>
/// Summary description for amazonServices
/// </summary>
public class amazonServices
{

    public const int MaxWidth = 300;
    public const int MaxHeight = 300;
    public const int MinSize = 5 * 1024;
    public const int MaxSize = 900 * 1024;
    public const int ThumbnailWidth = 150;
    public const int ThumbnailHeight = 150;

    
    private const string myAWSAccessKeyId = "AKIAIVCCY65FIOCJWQ3Q";
    private const string mySecretAccessKeyId = "Rk6tI1QlV/HN/KBijpxo4dBBXeTImBJp2LmLGhpq";
    private const string awsDirName = "testprofilepicture";
    
	public amazonServices()
	{
    	//
		// TODO: Add constructor logic here
		//
	}

    public static int deleteImgFromCloud(string sqlVar)
    {
        /*
         * Function : Delete Image (Profile and Gallery from Cloud)
         *          : 1. Get File ext of the file to be deleted
         *          : 2. Nullify the Image Path and Caption
         *          : 3. Delete the Image from the server.
         * 
        */
        string fileName;
        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
        try
        {
            string sqlStr = "SELECT " + sqlVar + "Path," + sqlVar + "PathThumbNail," +
                sqlVar + "Pathgallery " +
               "FROM bizPicDtls WHERE ID = '" + Membership.GetUser().ProviderUserKey.ToString() + "'";

            //System.Windows.Forms.MessageBox.Show(sqlStr);
            
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(sqlStr, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {

                        fileName = reader.GetString(0).Substring(reader.GetString(0).LastIndexOf("."), (reader.GetString(0).Length - reader.GetString(0).LastIndexOf(".")));
                        fileName = sqlVar + fileName;
                        string uploadFileName = @"Images/orginal/"
                        + Membership.GetUser().ProviderUserKey.ToString()
                         + @"/" + fileName;
                        uploadFileName = reader.GetString(0); 
                        connection.Close();
                        string storedProc = "bizPicDtls_Update_" + sqlVar;

                        SqlConnection con = new SqlConnection(connStr);
                        SqlCommand cmd = new SqlCommand(storedProc, con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@imgPath", SqlDbType.VarChar, 1000));
                        cmd.Parameters.Add(new SqlParameter("@imgCaption", SqlDbType.VarChar, 100));
                        cmd.Parameters.Add(new SqlParameter("@imgID", SqlDbType.VarChar, 50));
                        cmd.Parameters.Add(new SqlParameter("@imgThumbNailPath", SqlDbType.VarChar, 1000));
                        cmd.Parameters.Add(new SqlParameter("@imggalleryPath", SqlDbType.VarChar, 1000));

                        if (sqlVar == "profileImg")
                        {
                            cmd.Parameters.Add(new SqlParameter("@IsABiz", SqlDbType.Bit));
                            if (HttpContext.Current.User.IsInRole("bizOwner"))
                            {

                                cmd.Parameters["@IsABiz"].Value = 1;
                            }
                            else
                            {
                                cmd.Parameters["@IsABiz"].Value = 0;

                            }
                        }
                    

                        cmd.Parameters["@imgPath"].Value            = DBNull.Value ;
                        cmd.Parameters["@imgThumbNailPath"].Value   = DBNull.Value;
                        cmd.Parameters["@imggalleryPath"].Value     = DBNull.Value;
                        cmd.Parameters["@imgCaption"].Value         = DBNull.Value;
                        cmd.Parameters["@imgID"].Value              = Membership.GetUser().ProviderUserKey.ToString();
                        

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();

                        }
                        catch (SqlException)
                        {
                            return 12;
                        }
                        finally
                        {
                            con.Close();
                        }
                    

                        /* If Stored Procedure execution sucessfull then delete file from 
                         * Amazon S3
                         **/
                         
                        AmazonS3 myS3 = new AmazonS3();
                        DateTime myTime = DateTime.Now;
                        try
                        {
                            string strMySignature = S3Helper.GetSignature(
                                                    mySecretAccessKeyId,
                                                    "DeleteObject",
                                                    myTime);

                            Status myResults = myS3.DeleteObject(
                                                awsDirName,
                                                uploadFileName,
                                                myAWSAccessKeyId,
                                                S3Helper.GetTimeStamp(myTime),
                                                true,
                                                strMySignature,
                                                null);
                            if (myResults.Code == 0 || myResults.Code == 204)
                            {
                                //System.Windows.Forms.MessageBox.Show(myResults.Code.ToString());
                                return 0;
                            }
                            else
                            {
                                //System.Windows.Forms.MessageBox.Show(myResults.Code.ToString());
                                return 9;
                            }
                           
                           
                        }
                        catch (Exception)
                        {
                            return 8;
                        }
                        finally
                        {
                            connection.Close();
                        }



                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 4;
                }
            }
        }
        catch (SqlException)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message );     
            return 3;
        }
       

    
    }
   
    public static int sendOrgImgToCloud(FileUpload htmlFilePost, string sqlVar, string picCaption)
    {

        /*
         * Orginal file Path    :   Images/orginal/UserID/filename.fileextension
         * Thumb Nail           :   Images/thumbsNails/UserID/filename.fileextension
         * GalleryFile          :   Images/gallery/UserID/filename.fileextension
         * 
         * 
         * 
         * 
         * 
         */ 
        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
        string thumbNailFileName;
        string galleryFileName;
        try
        {
            if (CommonFunctions.IsImage(htmlFilePost.PostedFile))
            {
                
                if(htmlFilePost.PostedFile.ContentLength < MaxSize )
                {
                    AmazonS3 myS3 = new com.amazonaws.s3.AmazonS3();
                    DateTime myTime = DateTime.Now;
                    string strMySignature = S3Helper.GetSignature(
                    mySecretAccessKeyId,
                        "PutObjectInline",
                        myTime);

                    Grant myGrant = new com.amazonaws.s3.Grant();
                    Grant[] myGrants = new Grant[1];
                    com.amazonaws.s3.Group myGroup = new com.amazonaws.s3.Group();
                    myGroup.URI = "http://acs.amazonaws.com/groups/global/AllUsers";
                    myGrant.Grantee = myGroup;
                    myGrant.Permission = Permission.READ;
                    myGrants[0] = myGrant;
                   
                    MetadataEntry myContentType = new MetadataEntry();
                    myContentType.Name = "ContentType";
                    myContentType.Value = htmlFilePost.PostedFile.ContentType;

                    MetadataEntry[] myMetaData = new MetadataEntry[1];
                    myMetaData[0] = myContentType;
                    myContentType.Value = htmlFilePost.PostedFile.ContentType;

                    string uploadFileName = @"Images/orginal/"
                        + Membership.GetUser().ProviderUserKey.ToString()
                         + @"/" +
                         sqlVar + System.IO.Path.GetExtension(htmlFilePost.PostedFile.FileName);
                    
                    thumbNailFileName = @"Images/thumbNails/"
                        + Membership.GetUser().ProviderUserKey.ToString()
                         + @"/" + sqlVar + System.IO.Path.GetExtension(htmlFilePost.PostedFile.FileName);

                    galleryFileName = @"Images/gallery/"
                        + Membership.GetUser().ProviderUserKey.ToString()
                         + @"/" + sqlVar + System.IO.Path.GetExtension(htmlFilePost.PostedFile.FileName);

                    PutObjectResult myResult =  
                        myS3.PutObjectInline(
                      awsDirName,
                      uploadFileName,
                      myMetaData,
                      htmlFilePost.FileBytes,
                      htmlFilePost.FileBytes.Length,
                      myGrants,
                      StorageClass.STANDARD,
                      true,
                      myAWSAccessKeyId,
                      S3Helper.GetTimeStamp(myTime),
                      true,
                      strMySignature, null
                      );
                
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(htmlFilePost.FileContent   ))
                    using (Bitmap bitmap = new Bitmap(image, 75, 75))
                    {
                        byte[] bytes = (byte[])System.ComponentModel.TypeDescriptor.GetConverter(bitmap).ConvertTo(bitmap, typeof(byte[]));

                        PutObjectResult thumbNailFile =
                        myS3.PutObjectInline(
                        awsDirName,
                        thumbNailFileName,
                        myMetaData,
                        bytes,
                        bytes.Length,
                        myGrants,
                        StorageClass.STANDARD,
                        true,
                        myAWSAccessKeyId,
                        S3Helper.GetTimeStamp(myTime),
                        true,
                        strMySignature, null
                      );
                    }
                    //    byte[] bytes = (byte[])System.ComponentModel.TypeDescriptor.GetConverter(bitmap).ConvertTo(bitmap, typeof(byte[]));
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(htmlFilePost.FileContent))  
                    using (Bitmap bitmapGallery = new Bitmap(image, 300, 300))
                    {
                        byte[] bytesGallery = (byte[])System.ComponentModel.TypeDescriptor.GetConverter(bitmapGallery).ConvertTo(bitmapGallery, typeof(byte[]));
                        
                        PutObjectResult galleryFile =
                        myS3.PutObjectInline(
                        awsDirName,
                        galleryFileName ,
                        myMetaData,
                        bytesGallery,
                        bytesGallery.Length,
                        myGrants,
                        StorageClass.STANDARD ,
                        true,
                        myAWSAccessKeyId,
                        S3Helper.GetTimeStamp(myTime),
                        true,
                        strMySignature, null
                      );
  
                    }
                    
                    string storedProc = "bizPicDtls_Update_" + sqlVar;
                    SqlConnection con = new SqlConnection(connStr);
                    SqlCommand cmd = new SqlCommand(storedProc, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@imgPath", SqlDbType.VarChar, 1000));
                    cmd.Parameters.Add(new SqlParameter("@imgThumbNailPath", SqlDbType.VarChar, 1000));
                    cmd.Parameters.Add(new SqlParameter("@imggalleryPath", SqlDbType.VarChar, 1000));
                    cmd.Parameters.Add(new SqlParameter("@imgCaption", SqlDbType.VarChar, 100));
                    cmd.Parameters.Add(new SqlParameter("@imgID", SqlDbType.VarChar, 50));

                    if (sqlVar == "profileImg")
                    {
                        cmd.Parameters.Add(new SqlParameter("@IsABiz", SqlDbType.Bit));
                        if (HttpContext.Current.User.IsInRole("bizOwner"))
                        {

                            cmd.Parameters["@IsABiz"].Value = 1;
                        }
                        else
                        {
                            cmd.Parameters["@IsABiz"].Value = 0;

                        }
                    }
                    

                    cmd.Parameters["@imgPath"].Value            = @"http://" + awsDirName + @".s3.amazonaws.com" + @"/" + uploadFileName;
                    cmd.Parameters["@imgThumbNailPath"].Value   = @"http://" + awsDirName + @".s3.amazonaws.com" + @"/" + thumbNailFileName;
                    cmd.Parameters["@imggalleryPath"].Value     = @"http://" + awsDirName + @".s3.amazonaws.com" + @"/" + galleryFileName ;
                    if (!string.IsNullOrEmpty(picCaption)){ 
                    cmd.Parameters["@imgCaption"].Value         = picCaption ;
                    }
                    else
                    {
                        cmd.Parameters["@imgCaption"].Value = DBNull.Value ;
                    }
                    cmd.Parameters["@imgID"].Value              = Membership.GetUser().ProviderUserKey.ToString();

                         

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        
                    }

                    catch (SqlException err)
                    {
                        System.Windows.Forms.MessageBox.Show(err.Message );     
                        return 12;
                    }
                    finally
                    {
                        con.Close();
                    }
                    
                    
                    
                } 
                else
                    return 2;

            }
            else
                return 1;
        }
        catch (Exception err)
        {
           // System.Windows.Forms.MessageBox.Show(err.Message);     
            return 10;
        
        }
        finally
        {
            
        }
        return 0;
    }

    public static string sendInvDoc2Cloud(FileUpload htmlFilePost,string InventoryHeader)
    {
       
        string docPath;
        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

        if (string.IsNullOrEmpty(htmlFilePost.FileName ) && string.IsNullOrEmpty(InventoryHeader )   )
        {
            return "Error - empty data passed";
        }
       

        if (CommonFunctions.IsValidDoc(htmlFilePost.PostedFile))
        {

            try
            {
                AmazonS3 myS3 = new com.amazonaws.s3.AmazonS3();
                DateTime myTime = DateTime.Now;
                string strMySignature = S3Helper.GetSignature(
                mySecretAccessKeyId,
                    "PutObjectInline",
                    myTime);

                Grant myGrant = new com.amazonaws.s3.Grant();
                Grant[] myGrants = new Grant[1];
                com.amazonaws.s3.Group myGroup = new com.amazonaws.s3.Group();
                myGroup.URI = "http://acs.amazonaws.com/groups/global/AllUsers";
                myGrant.Grantee = myGroup;
                myGrant.Permission = Permission.READ;
                myGrants[0] = myGrant;


                MetadataEntry myContentType = new MetadataEntry();
                myContentType.Name = "ContentType";
                myContentType.Value = htmlFilePost.PostedFile.ContentType;

                MetadataEntry[] myMetaData = new MetadataEntry[1];
                myMetaData[0] = myContentType;
                myContentType.Value = htmlFilePost.PostedFile.ContentType;

                string uploadFileName = @"InvDocs/orginal/"
                    + Membership.GetUser().ProviderUserKey.ToString()
                     + @"/" + "invDetails" +
                       System.IO.Path.GetExtension(htmlFilePost.PostedFile.FileName);

                PutObjectResult myResult =
                          myS3.PutObjectInline(
                        awsDirName,
                        uploadFileName,
                        myMetaData,
                        htmlFilePost.FileBytes,
                        htmlFilePost.FileBytes.Length,
                        myGrants,
                        StorageClass.STANDARD,
                        true,
                        myAWSAccessKeyId,
                        S3Helper.GetTimeStamp(myTime),
                        true,
                        strMySignature, null
                        );
                docPath = uploadFileName;
                    
            }
            catch (Exception )
            {

                return "Error database error";
            }

        }

        else
            docPath = null;

        if (string.IsNullOrEmpty(InventoryHeader ))
        
        {
            InventoryHeader = null;
        
        }


        using (SqlConnection connection = new SqlConnection(connStr))
        {
            try
            {
                string storedProc = "bizInventory_updateHeader";
                SqlCommand cmd = new SqlCommand(storedProc, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@docPath", SqlDbType.VarChar, 1000));
                cmd.Parameters.Add(new SqlParameter("@inventoryHeader", SqlDbType.VarChar, 100));

                cmd.Parameters["@ID"].Value = Membership.GetUser().ProviderUserKey.ToString();

                
                if (!string.IsNullOrEmpty(docPath))
                {
                    cmd.Parameters["@docPath"].Value = @"http://" + awsDirName + @".s3.amazonaws.com" + @"/" + docPath;
                    docPath = @"http://" + awsDirName + @".s3.amazonaws.com" + @"/" + docPath;
                }
                else
                {
                    cmd.Parameters["@docPath"].Value = docPath;
                    docPath = "Error - Null";
                }

                //System.Windows.Forms.MessageBox.Show("Header is " + InventoryHeader);     
                cmd.Parameters["@inventoryHeader"].Value = InventoryHeader;
                connection.Open();
                int rtn = cmd.ExecuteNonQuery();
                
                
                   
            }
            catch (SqlException err)
            {
                return "Error 6";
            }
            finally {

                connection.Close(); 
            }
        }
        
        // Update the bizInventoryTablewith the details; create 2 new columns on the table
        return docPath;
    
    }

}
