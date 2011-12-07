using System;
using System.Collections;
using System.Configuration;
using System.Web.Configuration;
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

public partial class BizUsers_bizBgImg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            LoadPic_OnRecord();
        }
    }

    protected void UploadBtn_Click(object sender, EventArgs e) {
        if (!string.IsNullOrWhiteSpace(FileUploadImg1.FileName))
        {
            int serv = amazonServices.sendOrgImgToCloud(FileUploadImg1, "bgImg", repeatImgDropList.SelectedValue);
            EvaluateAmz_ReturnCode(serv, Img1Image, "bgImg", BtnDelImg1, BtnUploadImg1);

        }
        else
        {
            updateRepeater();
        
        }    
    }
    protected void DelImgBtn_Click(object sender, EventArgs e)
    {
        deleteImagefromTable();
        LoadPic_OnRecord();

    }
    protected void EvaluateAmz_ReturnCode(int rtnCode, Image toLoadImage, string Imgtype, Button showBtn, Button hideBtn)
    {

        switch (rtnCode)
        {

            case 0:
                {
                    toLoadImage.Visible = false;
                    /*aptionText.Text = " ";*/
                    LoadPic_OnRecord();
     //               showBtn.Visible = true;
     //               hideBtn.Visible = false;
                    break;
                }

            default:
                {
                    break;
                }
        }
    }
    
    protected void LoadPic_OnRecord()
    {
        /************************************************************************************
         * This step refreshed the Iframe box with the latest Uploaded Image. If this step  *
         * is not used then the Cache image would be displayed. This step makes the image   *
         * visible and also updates the caption.                                            *
         *                                                                                  *
         ************************************************************************************ 
         ************************************************************************************/
        /*string SQLStatement = "SELECT bizBgImagePath,bizBgImageRepeat "
         + " from bizGraphics WHERE "
        + "bizID = '" + Membership.GetUser().ProviderUserKey.ToString() + "'";
        */
        string SQLStatement = "bizGraphics_BgImg_Select";
        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

        using (SqlConnection connection =
                new SqlConnection(connStr))
        {
            SqlCommand command = new SqlCommand(SQLStatement, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@userID", SqlDbType.VarChar, 50);
            command.Parameters["@userID"].Value = Membership.GetUser().ProviderUserKey.ToString();
                
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (!reader.IsDBNull(0))
                {
                    Img1Image.ImageUrl = reader.GetString(0);
                    Img1Image.Visible = true;
                    repeatImgDropList.SelectedValue = reader.GetString(1);
                    BtnDelImg1.Visible = true;
                    BtnUploadImg1.Visible = true;
                    BtnUploadImg1.Text = "Save ";
                }
                else
                {
                    BtnDelImg1.Visible = false;
                    BtnUploadImg1.Text = "Upload";
                    BtnUploadImg1.Visible = true ;
                    Img1Image.ImageUrl = "~/profileImages/applications-system-4.ico";
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

    protected int deleteImagefromTable()
    {
        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;
        string storedProc = "bizPicDtls_Update_bgImg" ;

        SqlConnection con = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand(storedProc, con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@imgPath", SqlDbType.VarChar, 1000));
        cmd.Parameters.Add(new SqlParameter("@imgCaption", SqlDbType.VarChar, 100));
        cmd.Parameters.Add(new SqlParameter("@imgID", SqlDbType.VarChar, 50));
        cmd.Parameters.Add(new SqlParameter("@imgThumbNailPath", SqlDbType.VarChar, 1000));
        cmd.Parameters.Add(new SqlParameter("@imggalleryPath", SqlDbType.VarChar, 1000));


        cmd.Parameters["@imgPath"].Value = DBNull.Value;
        cmd.Parameters["@imgThumbNailPath"].Value = DBNull.Value;
        cmd.Parameters["@imggalleryPath"].Value = DBNull.Value;
        cmd.Parameters["@imgCaption"].Value = DBNull.Value; ;
        cmd.Parameters["@imgID"].Value = Membership.GetUser().ProviderUserKey.ToString();


        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            return 0;

        }
        catch (SqlException)
        {
            return 12;
        }
        finally
        {
            con.Close();
        }
    
    
    
    }

    protected int updateRepeater()
    {
        string sqlStatement = "  UPDATE bizGraphics SET bizBgImageRepeat = '" + repeatImgDropList.SelectedValue +
                         "' WHERE bizID = '" + Membership.GetUser().ProviderUserKey.ToString() + "'";
     
        string connStr = WebConfigurationManager.ConnectionStrings["eastManDB"].ConnectionString;

        try
        {
            using (SqlConnection connection =
                    new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return 0;
            }
        }
        catch (SqlException err)
        {
            System.Windows.Forms.MessageBox.Show(err.Message);    
            return 12;

        }
    }
}