using System;
using System.Collections;
using System.Configuration;
using System.Web.Configuration ;  
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Sql; 
using System.Data.SqlClient;
using System.Data.SqlTypes;


public partial class BizUsers_bizPicsShow : System.Web.UI.Page
{

    string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.Cache.SetExpires(DateTime.Parse(DateTime.Now.ToString()));
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        if (!Page.IsPostBack)
        {
            BtnDelProfPic.Visible = true;
            BtnUploadImg1.Visible = true;
            BtnUploadImg2.Visible = true;
            BtnUploadImg3.Visible = true;
            BtnUploadImg4.Visible = true;
            BtnUploadImg5.Visible = true; 
            
            BtnDelProfPic.Visible = false;
            BtnDelImg1.Visible = false;
            BtnDelImg2.Visible = false;
            BtnDelImg3.Visible = false;
            BtnDelImg4.Visible = false;
            BtnDelImg5.Visible = false;
            
            loadAllItemsonPage();
        
        }
    }

    protected void loadAllItemsonPage()

    {
        string SQLStatement = "SELECT profileImgPath,profileImgCaption,Img1Path,Img1Caption," +
           "Img2Path,Img2Caption,Img3Path,Img3Caption,Img4Path,Img4Caption,Img5Path,Img5Caption " +
           " FROM bizPicDtls WHERE ID = '"
           + Membership.GetUser().ProviderUserKey.ToString() + "'";
       // System.Windows.Forms.MessageBox.Show(SQLStatement);   

       using (SqlConnection connection =
                            new SqlConnection(connStr))
       {
           SqlCommand command = new SqlCommand(SQLStatement, connection);
           connection.Open();
           SqlDataReader reader = command.ExecuteReader();
           if (reader.HasRows)
           {
               reader.Read();
               if (!reader.IsDBNull(0))
               {
                   ProfileImage.ImageUrl = reader.GetString(0);
                   ProfileImage.Visible = true;
                   BtnUploadProfPic.Visible = false;
                   BtnDelProfPic.Visible = true; 
               }
         //      if (!reader.IsDBNull(1))
         //      {
         //          txtCaptionProfImg.Text = reader.GetString(1);

         //      }
               if (!reader.IsDBNull(2))
               {
                   Img1Image.ImageUrl = reader.GetString(2);
                   Img1Image.Visible = true;
                   BtnUploadImg1.Visible = false;
                   BtnDelImg1.Visible = true; 
               }
               //      if (!reader.IsDBNull(3))
               //{
               //    txtCaptionImg1.Text = reader.GetString(3);
               //
               // }
               if (!reader.IsDBNull(4))
               {
                   Img2Image.ImageUrl = reader.GetString(4);
                   Img2Image.Visible = true;
                   BtnUploadImg2.Visible = false;
                   BtnDelImg2.Visible = true;
               }
               //if (!reader.IsDBNull(5))
               //{
               //    txtCaptionImg2.Text = reader.GetString(5);

               // }
               if (!reader.IsDBNull(6))
               {
                   Img3Image.ImageUrl = reader.GetString(6);
                   Img3Image.Visible = true;
                   BtnUploadImg3.Visible = false;
                   BtnDelImg3.Visible = true;
               }
               // if (!reader.IsDBNull(7))
              // {
              //     txtCaptionImg3.Text = reader.GetString(7);
//
               //             }
               if (!reader.IsDBNull(8))
               {
                   Img4Image.ImageUrl = reader.GetString(8);
                   Img4Image.Visible = true;
                   BtnUploadImg4.Visible = false;
                   BtnDelImg4.Visible = true;
               }
               // if (!reader.IsDBNull(9))
              // {
               //    txtCaptionImg4.Text = reader.GetString(9);
//
               //             }
               if (!reader.IsDBNull(10))
               {
                   Img5Image.ImageUrl = reader.GetString(10);
                   Img5Image.Visible = true;
                   BtnUploadImg5.Visible = false;
                   BtnDelImg5.Visible = true;
               }
               // if (!reader.IsDBNull(11))
              // {
              //     txtCaptionImg5.Text = reader.GetString(11);
//
               //             }
           }
           reader.Close();
           connection.Close();
       }

    }

    protected void DelImgBtn_Click(object sender, EventArgs e)
    {

        
        if (Page.IsPostBack)
        {

            int serv;
            Button btndeleter = (Button)sender;
             
            switch (btndeleter.ID)
            {
                case "BtnDelProfPic":
                    if (ProfileImage.ImageUrl.Length > 0)
                    
                    {
                        serv = amazonServices.deleteImgFromCloud("profileImg");                                        
                        EvaluateAmz_ReturnCode(serv, ProfileImage, "profileImg", BtnUploadProfPic,BtnDelProfPic  );
                    }
                    break;

                case "BtnDelImg1":
                    if(Img1Image.ImageUrl.Length > 0)
                    {
                        serv = amazonServices.deleteImgFromCloud("Img1"); 
                        EvaluateAmz_ReturnCode(serv, Img1Image, "Img1", BtnUploadImg1,BtnDelImg1  );
                       // System.Windows.Forms.MessageBox.Show(serv.ToString());    
                    }
                    break;

                case "BtnDelImg2":
                    if (Img2Image.ImageUrl.Length > 0)
                    {
                        serv = amazonServices.deleteImgFromCloud("Img2");
                        EvaluateAmz_ReturnCode(serv, Img2Image, "Img2",  BtnUploadImg2, BtnDelImg2);

                    }
                    break;

                case "BtnDelImg3":
                    if (Img3Image.ImageUrl.Length > 0)
                    {
                        serv = amazonServices.deleteImgFromCloud("Img3");
                        EvaluateAmz_ReturnCode(serv, Img3Image, "Img3",  BtnUploadImg3, BtnDelImg3);

                    }
                    break;


                case "BtnDelImg4":
                    if (Img4Image.ImageUrl.Length > 0)
                    {
                        serv = amazonServices.deleteImgFromCloud("Img4");
                        EvaluateAmz_ReturnCode(serv, Img4Image, "Img4",  BtnUploadImg4, BtnDelImg4);

                    }
                    break;

                case "BtnDelImg5":
                    if (Img5Image.ImageUrl.Length > 0)
                    {
                        serv = amazonServices.deleteImgFromCloud("Img5");
                        EvaluateAmz_ReturnCode(serv, Img5Image, "Img5", BtnUploadImg5, BtnDelImg5);

                    }
                    break;

                
                default:
                
                    break;
            }
        }
    }



    protected void UploadBtn_Click(object sender, EventArgs e) 
    {
        int serv=0;
        if (Page.IsPostBack )
        {
        Button btnUploader = (Button)sender;
       // btnUploader.Text = "Hello";


        switch (btnUploader.ID)
        {

            case "BtnUploadProfPic":
                serv = amazonServices.sendOrgImgToCloud(FileUploadProfImg, "profileImg", DBNull.Value.ToString());
                System.Windows.Forms.MessageBox.Show(serv.ToString());      
                EvaluateAmz_ReturnCode(serv, ProfileImage, "profileImg", BtnDelProfPic, BtnUploadProfPic );
                break;

            case "BtnUploadImg1":
                serv = amazonServices.sendOrgImgToCloud(FileUploadImg1, "Img1", DBNull.Value.ToString());
                EvaluateAmz_ReturnCode(serv, Img1Image, "Img1", BtnDelImg1,BtnUploadImg1  );
                break;

            case "BtnUploadImg2":
                serv = amazonServices.sendOrgImgToCloud(FileUploadImg2, "Img2", DBNull.Value.ToString());
                EvaluateAmz_ReturnCode(serv, Img2Image, "Img2",  BtnDelImg2, BtnUploadImg2);
                break;

            case "BtnUploadImg3":
                serv = amazonServices.sendOrgImgToCloud(FileUploadImg3, "Img3", DBNull.Value.ToString());
                EvaluateAmz_ReturnCode(serv, Img3Image, "Img3",  BtnDelImg3, BtnUploadImg3);
                break;

            case "BtnUploadImg4":
                serv = amazonServices.sendOrgImgToCloud(FileUploadImg4, "Img4", DBNull.Value.ToString());
                EvaluateAmz_ReturnCode(serv, Img4Image, "Img4",  BtnDelImg4, BtnUploadImg4);
                break;

            case "BtnUploadImg5":
                serv = amazonServices.sendOrgImgToCloud(FileUploadImg5, "Img5", DBNull.Value.ToString());
                EvaluateAmz_ReturnCode(serv, Img5Image, "Img5", BtnDelImg5, BtnUploadImg5);
                break;

            default:
                break;

        }
        System.Windows.Forms.MessageBox.Show(serv.ToString());     
          
        }
    }

    protected void EvaluateAmz_ReturnCode(int rtnCode, Image toLoadImage, string Imgtype,Button showBtn, Button hideBtn)
    {
        
        switch (rtnCode)
        {

            case 0:
                {
                    toLoadImage.Visible = false;
                    /*aptionText.Text = " ";*/
                    LoadPic_OnRecord(toLoadImage, Imgtype);
                    showBtn.Visible = true;
                    hideBtn.Visible = false ;
                    break;
                }

            default:
                {
                    break;
                }
        }
    }

    protected void LoadPic_OnRecord(Image toLoadImage, string ImgType)
   
    {
        /************************************************************************************
         * This step refreshed the Iframe box with the latest Uploaded Image. If this step  *
         * is not used then the Cache image would be displayed. This step makes the image   *
         * visible and also updates the caption.                                            *
         *                                                                                  *
         ************************************************************************************ 
         ************************************************************************************/
        string SQLStatement = "SELECT " + ImgType + "Path, " 
        + ImgType + "Caption from bizPicDtls WHERE "
        + "ID = '" + Membership.GetUser().ProviderUserKey.ToString() + "'";

        

        using (SqlConnection connection =
                new SqlConnection(connStr))
        {
            SqlCommand command = new SqlCommand(SQLStatement, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (!reader.IsDBNull(0))
                {
                    toLoadImage.ImageUrl = reader.GetString(0);
                    toLoadImage.Visible = true; 

                }
/*               
 * Remove All Caption Related Code for Images
 * 
 *              if (!reader.IsDBNull(1))
                {
                    captionText.Text  = reader.GetString(1);

                }*/
            }
            reader.Close();
            connection.Close(); 
        }
    }

}
