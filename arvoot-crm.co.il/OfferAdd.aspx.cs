﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlPanel.HelpersFunctions;
//using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;

namespace ControlPanel
{
    public partial class _offerAdd : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "חפש קובץ";
        public string StrSrc { get { return strSrc; } }

        string FileName1;
        protected void Page_Load(object sender, EventArgs e)
        {
       
            //  UploadDocument.Attributes.Add("onclick", "document.getElementById('" + ImageFile_FileUpload.ClientID + "').click();");
            
            if (!Page.IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                Pageinit.CheckManagerPermissions();
                UploadDocument.Attributes.Add("onclick", "document.getElementById('" + ImageFile_FileUpload.ClientID + "').click();");
                Session["UploadedFiles"] = new List<FileDetail>();
                BindFileRepeater();
                SqlCommand cmdSourceLoanOrInsurance = new SqlCommand("SELECT * FROM SourceLoanOrInsurance");
                DataSet dsSourceLoanOrInsurance = DbProvider.GetDataSet(cmdSourceLoanOrInsurance);
                SelectSourceLoanOrInsurance.DataSource = dsSourceLoanOrInsurance;
                SelectSourceLoanOrInsurance.DataTextField = "Text";
                SelectSourceLoanOrInsurance.DataValueField = "ID";
                SelectSourceLoanOrInsurance.DataBind();
                SelectSourceLoanOrInsurance.Items.Insert(0, new ListItem("בחר", ""));

                SqlCommand cmdSecondStatus = new SqlCommand("SELECT * FROM StatusOffer");
                DataSet dsSecondStatus = DbProvider.GetDataSet(cmdSecondStatus);
                SelectStatusOffer.DataSource = dsSecondStatus;
                SelectStatusOffer.DataTextField = "Status";
                SelectStatusOffer.DataValueField = "ID";
                SelectStatusOffer.DataBind();

                SqlCommand cmdOfferType = new SqlCommand("SELECT * FROM OfferType");
                DataSet dsOfferType = DbProvider.GetDataSet(cmdOfferType);
                SelectOfferType.DataSource = dsOfferType;
                SelectOfferType.DataTextField = "Name";
                SelectOfferType.DataValueField = "ID";
                SelectOfferType.DataBind(); 
                
                SqlCommand cmdTurnOffer = new SqlCommand("SELECT * FROM TurnOffer");
                DataSet dsTurnOffer = DbProvider.GetDataSet(cmdTurnOffer);
                SelectTurnOffer.DataSource = dsTurnOffer;
                SelectTurnOffer.DataTextField = "Name";
                SelectTurnOffer.DataValueField = "ID";
                SelectTurnOffer.DataBind();
                SelectTurnOffer.Items.Insert(0, new ListItem("בחר", ""));
                //loadUsers(1);
                loadData();
            }
        }



        protected void ImageFile_btnUpload_Click(object sender, EventArgs e)
        {
            if (ImageFile_FileUpload.HasFile)
            {
                try
                {
                    string ext = Path.GetExtension(ImageFile_FileUpload.FileName).ToLower();
                    if (ext == ".pdf")
                    {
                        var uploadedFiles = (List<FileDetail>)Session["UploadedFiles"];

                        var fileDetail = new FileDetail
                        {
                            FileName = ImageFile_FileUpload.FileName,
                            FileSize = ImageFile_FileUpload.PostedFile.ContentLength,
                            PostedFile = ImageFile_FileUpload.PostedFile
                        };

                        // Optionally save the file to server
                        // FileUploadControl.SaveAs(Server.MapPath("~/Uploads/") + FileUploadControl.FileName);

                        uploadedFiles.Add(fileDetail);
                        Session["UploadedFiles"] = uploadedFiles;

                        BindFileRepeater();

                    }
                    else
                    {
                        ImageFile_lable.Text = "* הסיומת לא תקינה";
                        ImageFile_lable.Visible = true;
                    }
                }
                catch
                {
                    ImageFile_lable.Text = "* בבקשה נסה שוב";
                    ImageFile_lable.Visible = true;
                }
            }
        }

        private void BindFileRepeater()
        {
            var uploadedFiles = (List<FileDetail>)Session["UploadedFiles"];
            Repeater1.DataSource = uploadedFiles;
            Repeater1.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //FileUpload fileUpload = (FileUpload)e.Item.DataItem;
                //Label fileNameLabel = (Label)e.Item.FindControl("FileNameLabel");

                //if (fileUpload != null && fileNameLabel != null)
                //{
                //    fileNameLabel.Text = fileUpload.FileName;
                //}
            }

        }
        protected void Download_Click(object sender, CommandEventArgs e)
        {
         
                //int index = Convert.ToInt32(e.CommandArgument);
                //List<FileUpload> myFiles = (List<FileUpload>)Session["MyFiles"];

                //if (myFiles != null && index < myFiles.Count)
                //{
                //        string fileName = myFiles[index].FileName;
                //        string filePath = Server.MapPath(Path.Combine(UPLOAD_DIRECTORY, fileName));


                //    if (File.Exists(filePath))
                //    {
                //        Response.Clear();
                //        Response.ContentType = "application/octet-stream";
                //        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                //        Response.TransmitFile(filePath);
                //        Response.Flush();
                //        Response.End();
                //    }
                //}
            
        }
        protected void CopyLid_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ShereDoc_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('עליך לשמור את ההצעה');", true);

        }
        protected void DeleteLid_Click(object sender, ImageClickEventArgs e)
        {
        }
        protected void UploadFile_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "")
            {
                //Error.Visible = true;
                //Error.Text = "הקובץ לא קיים";
            }
            else
            {
                Response.Redirect("DownloadFile.ashx?fileName=" + e.CommandArgument.ToString() + "&dirName=InsuredFiles");
            }


        }
        public void loadData()
        {
            //int PageNumber = 1;

            string sql = @"select  Lead.Tz,Lead.FirstName+' '+Lead.LastName as FullName,Agent.FullName as FullNameAgent from Lead
                           inner join Agent on Lead.AgentID=Agent.ID ";

            SqlCommand cmd = new SqlCommand(sql);


            DataTable dt = DbProvider.GetDataTable(cmd);
            if (dt.Rows.Count > 0)
            {
                FullName.InnerText = dt.Rows[0]["FullName"].ToString();
                FullNameAgent.InnerText = dt.Rows[0]["FullNameAgent"].ToString();
                Tz.InnerText = dt.Rows[0]["Tz"].ToString();
                EffectiveDate.InnerText = DateTime.Now.ToString("dd.MM.yyyy");
            }
        }

        protected void ButtonDiv_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

        }

        public bool funcSave(object sender, EventArgs e)
        {

            int ErrorCount = 0;
            FormError_lable.Visible = false;
            if (SelectSourceLoanOrInsurance.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מקור ההלוואה/ביטוח";
                return false;
            }  
           /* if (SelectOfferType.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין סוג הצעה";
                return false;
            } */
        
            if (string.IsNullOrEmpty(ReasonLackSuccess.Value))
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין סיבה לחוסר הצלחה";
                return false;
            }
            if (string.IsNullOrEmpty(ReturnDateToCustomer.Value))
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מועד חזרה ללקוח";
                return false;
            }
            if (string.IsNullOrEmpty(DateSentToInsuranceCompany.Value))
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין תאריך שליחה לחברת הביטוח";
                return false;
            }
            //if (SelectStatusOffer.SelectedIndex == 0)
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין סטטוס הצעה";
            //    return false;
            //}
            if (SelectTurnOffer.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין תור";
                return false;
            }
            if (ErrorCount == 0)
            {
                SqlCommand cmdInsert = new SqlCommand(@"insert into Offer (LeadID,SourceLoanOrInsuranceID,OfferTypeID,ReasonLackSuccess,ReturnDateToCustomer,DateSentToInsuranceCompany,Note,StatusOfferID,TurnOfferID) output INSERTED.ID  
                                                 values(@LeadID,@SourceLoanOrInsuranceID,@OfferTypeID,@ReasonLackSuccess,@ReturnDateToCustomer,@DateSentToInsuranceCompany,@Note,@StatusOfferID,@TurnOfferID)");

                cmdInsert.Parameters.AddWithValue("@LeadID", Request.QueryString["ContactID"]);
                cmdInsert.Parameters.AddWithValue("@SourceLoanOrInsuranceID", SelectSourceLoanOrInsurance.Value);
                cmdInsert.Parameters.AddWithValue("@OfferTypeID", SelectOfferType.Value);
                cmdInsert.Parameters.AddWithValue("@ReasonLackSuccess", ReasonLackSuccess.Value);
                cmdInsert.Parameters.AddWithValue("@ReturnDateToCustomer", DateTime.Parse(ReturnDateToCustomer.Value));
                cmdInsert.Parameters.AddWithValue("@DateSentToInsuranceCompany", DateTime.Parse(DateSentToInsuranceCompany.Value));
                cmdInsert.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note.Value) ? (object)DBNull.Value : Note.Value);
                cmdInsert.Parameters.AddWithValue("@StatusOfferID", SelectStatusOffer.Value);
                cmdInsert.Parameters.AddWithValue("@TurnOfferID", SelectTurnOffer.Value);

                long offerID = DbProvider.GetOneParamValueLong(cmdInsert);
                List<FileDetail> myFile = (List<FileDetail>)Session["UploadedFiles"];
                if (offerID > 0 && myFile != null)
                {
                    for (int i = 0; i < myFile.Count; i++) {
                        try
                        {
                            string FilePath1 = String.Format("{0}/OfferDocuments/", ConfigurationManager.AppSettings["MapPath1"]);                          
                            FileName1 = myFile[i].FileName;

                            myFile[i].PostedFile.SaveAs(Path.Combine(FilePath1, FileName1)); 
                            SqlCommand cmdInsertDoc = new SqlCommand(@"insert into OfferDocuments (FileName,OfferID) 
                                                     values(@FileName,@OfferID)");
                            cmdInsertDoc.Parameters.AddWithValue("@FileName", FileName1);
                            cmdInsertDoc.Parameters.AddWithValue("@OfferID", offerID);
                            DbProvider.ExecuteCommand(cmdInsertDoc);
                        }
                        catch (Exception) { }
                    }

                    SqlCommand cmdHistory = new SqlCommand("INSERT INTO ActivityHistory (AgentID, Details, CreateDate, Show) VALUES (@agentID, @details, GETDATE(), 1)");
                    cmdHistory.Parameters.AddWithValue("@agentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                    cmdHistory.Parameters.AddWithValue("@details", ("הוספת הצעה חדשה עבור " + FullName.InnerText));
                    DbProvider.ExecuteCommand(cmdHistory);

                    return true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                    FormError_lable.Text = "* התרחשה שגיאה";
                    FormError_lable.Visible = true;
                }
            }



            return false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {


            bool success = funcSave(sender, e);
            if (!success)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                Response.Redirect("Contact.aspx?ContactID=" + Request.QueryString["ContactID"].ToString());
               // System.Web.HttpContext.Current.Response.Redirect(ListPageUrl);
            }
           
        }

        protected void ServiceRequestAdd_Click(object sender, ImageClickEventArgs e)
        {
           // System.Web.HttpContext.Current.Response.Redirect("ServiceRequestAdd.aspx?LeadID" + Request.QueryString["ContactID"]);

        }

        public class FileDetail
        {
            public string FileName { get; set; }
            public int FileSize { get; set; }
            public HttpPostedFile PostedFile { get; set; }
        }
    }
}