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
using System.Net.Mail;
using System.Text;
using System.Globalization;

namespace ControlPanel
{
    public partial class _offerEdit : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "חפש קובץ";
        public string StrSrc { get { return strSrc; } }
        DataTable dtOfferDocument;
        DataTable dtServiceRequest;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {

                Pageinit.CheckManagerPermissions();
                UploadDocument.Attributes.Add("onclick", "document.getElementById('" + ImageFile_FileUpload.ClientID + "').click();");

                
                if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) > 3)
                {
                    DeleteLid.Visible = false;
                }

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
                SelectOfferType.Items.Insert(0, new ListItem("בחר", ""));


                //SqlCommand cmdTurnOffer = new SqlCommand("SELECT * FROM TurnOffer");
                //DataSet dsTurnOffer = DbProvider.GetDataSet(cmdTurnOffer);
                //SelectTurnOffer.DataSource = dsTurnOffer;
                //SelectTurnOffer.DataTextField = "Name";
                //SelectTurnOffer.DataValueField = "ID";
                //SelectTurnOffer.DataBind();
                //SelectTurnOffer.Items.Insert(0, new ListItem("בחר", ""));

                SqlCommand cmdTaskStatuses = new SqlCommand("SELECT * FROM TaskStatuses");
                DataSet dsTaskStatuses = DbProvider.GetDataSet(cmdTaskStatuses);
                SelectStatusTask.DataSource = dsTaskStatuses;
                SelectStatusTask.DataTextField = "Status";
                SelectStatusTask.DataValueField = "ID";
                SelectStatusTask.DataBind();
                Session["UploadedFiles"] = null;
                //loadUsers(1);
                if (HttpContext.Current.Session["AgentLevel"].ToString() == "7")
                {
                    btn_save.Enabled = false;
                    DeleteLid.Enabled = false;
                    btnMoveToOperatingQueqe.Enabled = false;
                    ImageButton1.Enabled = false;
                    ImageButton2.Enabled = false;
                    BtnSaveBottom.Enabled = false;
                }
                if (int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) < 5)
                {
                    divHistory.Visible = true;
                }
                loadData();
              
            }
           
        }
        protected void CloseTaskPopUp_Click(object sender, EventArgs e)
        {
            TaskDiv.Visible = false;
        }

        public bool funcSaveTask(object sender, EventArgs e)
        {
            //שם פרטי שם משפחה תאריך לידה תז טלפון אימייל סטטוס ראשי
            int ErrorCount = 0;
            FormErrorTask_lable.Visible = false;
            if (TextTask.Value == "")
            {
                ErrorCount++;
                FormErrorTask_lable.Visible = true;
                FormErrorTask_lable.Text = "יש להזין תוכן";
                return false;
            }
            if (Date.Value == "")
            {
                ErrorCount++;
                FormErrorTask_lable.Visible = true;
                FormErrorTask_lable.Text = "יש להזין תאריך";
                return false;
            }
            if (SelectStatusTask.Value == "")
            {
                ErrorCount++;
                FormErrorTask_lable.Visible = true;
                FormErrorTask_lable.Text = "יש להזין סטטוס";
                return false;
            }




            if (ErrorCount == 0)
            {
                string sql = @" INSERT INTO [Tasks]( Text
                ,Status
                ,OfferID
                ,PerformDate)
                 VALUES (
	              @Text
                 ,@Status
                 ,@OfferID
                 ,@PerformDate)";

                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@Text", TextTask.Value);
                cmd.Parameters.AddWithValue("@Status", SelectStatusTask.Value);
                cmd.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                cmd.Parameters.AddWithValue("@PerformDate", Date.Value);

                if (DbProvider.ExecuteCommand(cmd) > 0)
                {
                    return true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                    FormError_label.Text = "* התרחשה שגיאה";
                    FormError_label.Visible = true;
                    FormErrorBottom_label.Text = "* התרחשה שגיאה";
                    FormErrorBottom_label.Visible = true;
                }

            }

            return false;
        }

        protected void OpenNewTask_Click(object sender, EventArgs e)
        {

            bool success = funcSaveTask(sender, e);
            if (!success)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                TaskDiv.Visible = false;
            }
        }

        protected void OpenTask_Click(object sender, ImageClickEventArgs e)
        {
            TaskDiv.Visible = true;

        } 
        
        protected void OpenImage_Click(object sender, CommandEventArgs e)
        {
            popupImg.Visible = true;
            UpdatePanel3.Update();
            string[] parameters = e.CommandArgument.ToString().Split(',');
            if (string.IsNullOrWhiteSpace(parameters[1]))
                 fileImg.ImageUrl = String.Format("{0}/OfferDocuments/", ConfigurationManager.AppSettings["FilesUrl"]) + parameters[0];
            else  fileImg.ImageUrl = "data:image/png;base64," + parameters[1];
            


        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string filename = DataBinder.Eval(e.Item.DataItem, "FileName").ToString();
            string Base64String = DataBinder.Eval(e.Item.DataItem, "Base64String").ToString();
            string extension = System.IO.Path.GetExtension(filename).ToLower();
            ImageButton fileImg = (ImageButton)e.Item.FindControl("fileImg");
            Image filePdf = (Image)e.Item.FindControl("filePdf");
            // Now extension will be ".jpg" and you can check:
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                    if(string.IsNullOrEmpty(Base64String))
                        fileImg.ImageUrl = String.Format("{0}/OfferDocuments/", ConfigurationManager.AppSettings["FilesUrl"]) + filename;
                    else fileImg.ImageUrl = "data:image/png;base64," + Base64String;
                    break;
                case ".pdf":
                    fileImg.Visible = false;
                    filePdf.Visible = true;
                    break;
                case ".doc":
                case ".docx":
                  //  fileImg.ImageUrl = "~/images/icons/pdf.png";

                    break;
                   
            }
        }
        protected void CopyLid_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void ShereLid_Click(object sender, ImageClickEventArgs e)
        {

        }  
        protected void DeleteLid_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void DeleteOffer_Click(object sender, ImageClickEventArgs e)
        {

            SqlCommand cmdDelServices = new SqlCommand("delete from ServiceRequest where OfferID = @ID");
            cmdDelServices.Parameters.AddWithValue("@ID", Request.QueryString["OfferID"]);
            DbProvider.ExecuteCommand(cmdDelServices);

            SqlCommand cmdDelDocuments = new SqlCommand("delete from OfferDocuments where OfferID = @ID");
            cmdDelDocuments.Parameters.AddWithValue("@ID", Request.QueryString["OfferID"]);
            DbProvider.ExecuteCommand(cmdDelDocuments);

            SqlCommand cmd = new SqlCommand("delete from Offer where ID = @ID");
            cmd.Parameters.AddWithValue("@ID", Request.QueryString["OfferID"]);
            if (DbProvider.ExecuteCommand(cmd) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                FormError_label.Text = "* התרחשה שגיאה";
                FormError_label.Visible = true;
                FormErrorBottom_label.Text = "* התרחשה שגיאה";
                FormErrorBottom_label.Visible = true;
            }
            else
            {
                Response.Redirect("Contact.aspx?ContactID=" + ContactID.Value);
            }

        }
        protected void UploadFile_Command(object sender, CommandEventArgs e)
        {
            string[] parameters = e.CommandArgument.ToString().Split(',');
            if (!parameters[1].ToString().Equals("0"))
                Response.Redirect("DownloadFile.ashx?fileName=" + parameters[0] + "&OfferID=" + Request.QueryString["OfferID"]);
            else ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('עליך לשמור את הקובץ לפני הורדה');", true);



        }    
        
        protected void RemoveFile_Command(object sender, CommandEventArgs e)
        {
            string[] parameters = e.CommandArgument.ToString().Split(',');
            if (!parameters[1].ToString().Equals("0"))
            {
                SqlCommand sqlCommand = new SqlCommand("delete from OfferDocuments where ID = @ID");
                sqlCommand.Parameters.AddWithValue("@ID", parameters[1]);
                DbProvider.ExecuteCommand(sqlCommand);
            }
            else
            {
                int curIndex = int.Parse(parameters[0]);
                SqlCommand sqlCount = new SqlCommand("select count(*) from OfferDocuments where OfferID = @OfferID");
                sqlCount.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                long countDs = DbProvider.GetOneParamValueLong(sqlCount);
                List<FileDetail> list = (List<FileDetail>)Session["UploadedFiles"];
                int index = (int)(curIndex - countDs);
                list.RemoveAt(index);
                Session["UploadedFiles"] = list;
            }

            BindFileRepeater();




        }




        protected void CloseMovePopUp_Click(object sender, ImageClickEventArgs e)
        {
            MoveToOperatorPopUp.Visible = false;
        }     
        protected void CloseImagePopUp_Click(object sender, ImageClickEventArgs e)
        {
            popupImg.Visible = false;
            UpdatePanel3.Update();
        }  
        protected void MoveToOperator_Save(object sender, EventArgs e)
        {
            MoveToOperatorPopUp.Visible = false;
            string sql = "update Offer set OperatorID = @OperatorID, IsInOperatingQueue = 0, DateSentToOperator = getdate() where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql);
            sqlCommand.Parameters.AddWithValue("@OperatorID", OperatorsList.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@ID", Request.QueryString["OfferID"]);
            DbProvider.ExecuteCommand(sqlCommand);

            string sqlAlert = "INSERT INTO Alerts (AgentID, Text, CreationDate, DisplayDate, OfferID) Values (@AgentID, @Text, GETDATE(), GETDATE(), @OfferID)";
            SqlCommand cmdAlert = new SqlCommand(sqlAlert);
            cmdAlert.Parameters.AddWithValue("@AgentID", OperatorsList.SelectedValue);
            cmdAlert.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
            cmdAlert.Parameters.AddWithValue("@Text", "ההצעה " + NameOffer.Value + " הועברה אליך לתפעול");
            DbProvider.ExecuteCommand(cmdAlert);

            Response.Redirect("Offers.aspx");


        }
        protected void SelectStatusOffer_Change(object sender, EventArgs e)
        {
            if(SelectStatusOffer.SelectedValue == "4" )
            {
                DateSentToInsuranceCompany.InnerText = DateTime.Now.ToString("dd.MM.yyyy");
            }    
    
            
        }
        public void loadData()
        {
           

            string sql = @"select Lead.Tz,Lead.FirstName+' '+Lead.LastName as FullName,A.FullName as FullNameAgent, Lead.AgentID, parent2.CompanyName from Lead
                           inner join ArvootManagers A on Lead.AgentID=A.ID inner join Offer on Offer.LeadID = Lead.ID 
                           left join ArvootManagers parent on parent.ID = A.ParentID
                           left join ArvootManagers parent2 on parent2.ID = parent.ParentID
                           where Offer.ID = @OfferID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);

            DataTable dt = DbProvider.GetDataTable(cmd);
            if (dt.Rows.Count > 0)
            {
                FullName.Text = dt.Rows[0]["FullName"].ToString();
                //FullNameAgent.InnerText = dt.Rows[0]["FullNameAgent"].ToString();
                lblOwner.InnerText = dt.Rows[0]["FullNameAgent"].ToString();
                AgentName.InnerText = dt.Rows[0]["FullNameAgent"].ToString();
                Tz.InnerText = dt.Rows[0]["Tz"].ToString();
                EffectiveDate.InnerText = DateTime.Now.ToString("dd.MM.yyyy");
                lblAgency.InnerText = dt.Rows[0]["CompanyName"].ToString();
            }

            string sqlOffer = @"select LeadID, CONVERT(varchar,Offer.CreateDate, 104) as CreateDate, CONVERT(varchar,Offer.DateSentToOperator, 104) as DateSentToOperator, IsInOperatingQueue, OperatorID,
                                 DATEDIFF(DAY,iif(Offer.OperatingQueueDate is null, Offer.CreateDate , Offer.OperatingQueueDate )  ,
                                iif(CompletedDate is null, getdate(),CompletedDate)) as sla, SourceLoanOrInsuranceID, OfferTypeID,TurnOfferID,
                                ReasonLackSuccess,CONVERT(varchar,Offer.ReturnDateToCustomer, 104) ReturnDateToCustomer,
                                Note,CONVERT(varchar,Offer.DateSentToInsuranceCompany, 104) as DateSentToInsuranceCompany, StatusOfferID,NameOffer from Offer where ID = @OfferID";
            SqlCommand cmdOffer = new SqlCommand(sqlOffer);
            cmdOffer.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
            DataTable dtOffer = DbProvider.GetDataTable(cmdOffer);
            if (dtOffer.Rows.Count > 0)
            {
                DataRow rowOffer = dtOffer.Rows[0];
                ContactID.Value = rowOffer["LeadID"].ToString(); 
                EffectiveDate.InnerText = rowOffer["CreateDate"].ToString();
                DateSentToOperator.InnerText = rowOffer["DateSentToOperator"].ToString();
                sla.InnerText = rowOffer["sla"].ToString();
                    SelectSourceLoanOrInsurance.Value = rowOffer["SourceLoanOrInsuranceID"].ToString();
                SelectOfferType.Value = rowOffer["OfferTypeID"].ToString();
                //SelectTurnOffer.Value = rowOffer["TurnOfferID"].ToString();
                ReasonLackSuccess.Value = rowOffer["ReasonLackSuccess"].ToString();
                ReturnDateToCustomer.Value = string.IsNullOrWhiteSpace(rowOffer["ReturnDateToCustomer"].ToString()) ? "":DateTime.ParseExact(rowOffer["ReturnDateToCustomer"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                Note.Value = rowOffer["Note"].ToString();
                DateSentToInsuranceCompany.InnerText = string.IsNullOrWhiteSpace(rowOffer["DateSentToInsuranceCompany"].ToString()) ? "" : rowOffer["DateSentToInsuranceCompany"].ToString(); 
                SelectStatusOffer.SelectedValue = rowOffer["StatusOfferID"].ToString();
                CurrentStatusOfferID.Value = rowOffer["StatusOfferID"].ToString();
                NameOffer.Value = rowOffer["NameOffer"].ToString();

                bool isInOperatingManager = false;
                if (!string.IsNullOrWhiteSpace(rowOffer["OperatorID"].ToString()))
                {
                    string sqlOwner = @"select ArvootManagers.FullName as FullNameAgent from ArvootManagers WHERE ID = @OperatorID and (Type = 5 or Type = 4)";
                    SqlCommand cmdOwner = new SqlCommand(sqlOwner);
                    cmdOwner.Parameters.AddWithValue("@OperatorID", rowOffer["OperatorID"]);
                    DataTable dtOwner = DbProvider.GetDataTable(cmdOwner);
                    if (dtOwner.Rows.Count > 0)
                    {
                        lblOwner.InnerText = dtOwner.Rows[0]["FullNameAgent"].ToString();
                        if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) == 4)
                            btnMoveToOperator2.Style["display"] = "block";
                    }

                    movedToOperating.Visible = false;
                    isInOperatingManager = true;
                    if (HttpContext.Current.Session["AgentLevel"] != null && (int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) == 4 || int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) == 3))
                        btnMoveToAgent.Visible = true;
                }
                if (rowOffer["IsInOperatingQueue"].ToString() == "1")
                {
                    movedToOperating.Visible = true;
                    isInOperatingManager = true;
                   
                }

                if (isInOperatingManager == true)
                {
                    btnMoveToOperatingQueqe.Visible = false;

                    //if (dt.Rows.Count > 0 && HttpContext.Current.Session != null)
                    //{
                    //    if (dt.Rows[0]["AgentID"].ToString() == HttpContext.Current.Session["AgentID"].ToString())
                    //    {
                    //        //אם הגיעו מהסוכן - בעל ההצעה יש לחסום אפשרות לעריכה
                    //        DeleteLid.Enabled = false;
                    //        btn_save.Enabled = false;
                    //        SelectStatusOffer.Disabled = true;
                    //        SelectOfferType.Disabled = true;
                    //        ImageButton2.Enabled = false;
                    //        ReasonLackSuccess.Disabled = true;
                    //        Note.Disabled = true;
                    //        ReturnDateToCustomer.Disabled = true;
                    //        DateSentToInsuranceCompany.Disabled = true;
                    //        SelectSourceLoanOrInsurance.Disabled = true;
                    //        UploadDocument.Enabled = false;
                    //        ImageButton3.Enabled = false;
                    //        ImageButton4.Enabled = false;
                    //        ImageButton1.Enabled = false;
                    //    }
                    //}

                        
                    
                }
                else
                {
                    btnMoveToOperatingQueqe.Visible = true;
                    movedToOperating.Visible = false;
                }
                string sqlOfferDocument = @"select *,'' as Base64String from OfferDocuments where OfferID = @OfferID";
                SqlCommand cmdOfferDocument = new SqlCommand(sqlOfferDocument);
                cmdOfferDocument.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                dtOfferDocument = DbProvider.GetDataTable(cmdOfferDocument);
                Repeater1.DataSource = dtOfferDocument;
                Repeater1.DataBind();
                
                string sqlServiceRequest = @"select s.ID, Lead.FirstName + ' ' + Lead.LastName as Invoice ,Sum,CONVERT(varchar, s.CreateDate, 104)  CreateDate, p.purpose as PurposeName,
                            (select sum(SumPayment) from ServiceRequestPayment where ServiceRequestID = s.ID and IsApprovedPayment = 1) as paid, SumCreditOrDenial, IsApprovedCreditOrDenial
                            from ServiceRequest s 
                            left join ServiceRequestPurpose p on s.PurposeID = p.ID 
                            left join Offer on Offer.ID = s.OfferID 
                            left join Lead on Lead.ID = Offer.LeadID
                            where s.OfferID = @OfferID";
                SqlCommand cmdServiceRequest = new SqlCommand(sqlServiceRequest);
                cmdServiceRequest.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                dtServiceRequest = DbProvider.GetDataTable(cmdServiceRequest);
                Repeater2.DataSource = dtServiceRequest;
                Repeater2.DataBind();


                string sqlHistory = @"select  O.CreateDate, isnull(S.Status,O.Message) as Status, A.FullName as Agent from OfferHistory O
                                      left join StatusOffer S on O.StatusID = S.ID
                                      left join ArvootManagers A on A.ID = O.AgentID
                                      where O.OfferID = @OfferID order by O.CreateDate desc";
                SqlCommand cmdHistory = new SqlCommand(sqlHistory);
                cmdHistory.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                DataTable dtHistory = DbProvider.GetDataTable(cmdHistory);
                Repeater4.DataSource = dtHistory;
                Repeater4.DataBind();


                if ((HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) != 4)|| rowOffer["IsInOperatingQueue"].ToString() != "1")
                    btnMoveToOperator.Visible = false;
              

            }

       }


        protected void BtnServiceRequest_Command(object sender, CommandEventArgs e)
        {
            System.Web.HttpContext.Current.Response.Redirect("ServiceRequestEdit.aspx?ServiceRequestID=" + e.CommandArgument.ToString());

        }
        private void BindFileRepeater()
        {
            DataTable dtUploadedFiles = new DataTable();          
            dtUploadedFiles.Columns.Add("ID", typeof(int));
            dtUploadedFiles.Columns.Add("FileName", typeof(string));
            dtUploadedFiles.Columns.Add("Base64String", typeof(string));
            if (Session["UploadedFiles"] != null)
            {
                try
                {
                    foreach (var file in (List<FileDetail>)Session["UploadedFiles"])
                    {
                        DataRow row = dtUploadedFiles.NewRow();
                        row["FileName"] = file.FileName;
                        row["ID"] = 0;
                        row["Base64String"] = file.FileBase64String;
                        dtUploadedFiles.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            // Combine both DataTables
            string sqlOfferDocument = @"select ID,FileName,'' as Base64String from OfferDocuments where OfferID = @OfferID";
            SqlCommand cmdOfferDocument = new SqlCommand(sqlOfferDocument);
            cmdOfferDocument.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
            dtOfferDocument = DbProvider.GetDataTable(cmdOfferDocument);
            DataTable combinedDataTable = dtOfferDocument.Copy();
            combinedDataTable.Merge(dtUploadedFiles);

            // Bind combined DataTable to Repeater
            Repeater1.DataSource = combinedDataTable;
            Repeater1.DataBind();
        }

        protected void ImageFile_btnUpload_Click(object sender, EventArgs e)
        {
            if (ImageFile_FileUpload != null && ImageFile_FileUpload.HasFiles)
            {
                try
                {
                    var uploadedFiles = new List<FileDetail>();
                    try
                    {
                        uploadedFiles = (List<FileDetail>)Session["UploadedFiles"] ?? new List<FileDetail>();
                    }
                    catch
                    {
                        uploadedFiles = new List<FileDetail>();
                    }

                    // Track successful and failed uploads
                    int successfulUploads = 0;
                    int failedUploads = 0;

                    foreach (HttpPostedFile postedFile in ImageFile_FileUpload.PostedFiles)
                    {
                        string ext = Path.GetExtension(postedFile.FileName).ToLower();
                        if (ext == ".pdf" || ext == ".jpeg" || ext == ".png" || ext == ".jpg")
                        {
                            string base64String = "";
                            if (ext == ".jpeg" || ext == ".png" || ext == ".jpg") {
                                System.IO.Stream fs = postedFile.InputStream;
                                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            }
                            var fileDetail = new FileDetail
                            {
                                FileName = Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + Helpers.CreateFileName(postedFile.FileName),
                                FileSize = postedFile.ContentLength,
                                PostedFile = postedFile,
                                FileBase64String = base64String
                            };

                            uploadedFiles.Add(fileDetail);
                            successfulUploads++;
                        }
                        else
                        {
                            failedUploads++;
                        }
                    }

                    // Update session with uploaded files
                    Session["UploadedFiles"] = uploadedFiles;

                    // Provide user feedback
                    if (successfulUploads > 0)
                    {
                        BindFileRepeater();
                        ImageFile_lable.Text = $"{successfulUploads} קבצים הועלו בהצלחה";
                        ImageFile_lable.Visible = true;
                    }

                    if (failedUploads > 0)
                    {
                        ImageFile_lable.Text += $"\n{failedUploads} קבצים לא הועלו בשל סיומת לא תקינה";
                        ImageFile_lable.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    ImageFile_lable.Text = "* בבקשה נסה שוב";
                    ImageFile_lable.Visible = true;
                }
            }
        }
        protected void ButtonDiv_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

        }
        protected void SendMail_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("heniappgate@gmail.com");
            mail.To.Add("heniappgate@gmail.com");
            mail.Subject = "פיננסים";
            mail.Body = "";
            mail.IsBodyHtml = false;
            if(Repeater1.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('אין קבצים לשליחה');", true);
                return;
            }
            // Iterate through Repeater items
            foreach (RepeaterItem item in Repeater1.Items)
            {
                CheckBox chkAttach = (CheckBox)item.FindControl("IsPromoted");
                Label lblFilePath = (Label)item.FindControl("FileName");

                if (chkAttach.Checked)
                {
                    string filePath = String.Format("{0}/OfferDocuments/{1}", ConfigurationManager.AppSettings["MapPath"], lblFilePath.Text) ;
                    
                    if (File.Exists(filePath))
                    {
                        mail.Attachments.Add(new Attachment(filePath));
                    }
                    
                }
            }
            if(mail.Attachments.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('עליך לסמן קבצים לשליחה');", true);
                return;
            }
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
               
            }
         

        }
        protected void CloseTasksListPopUp_Click(object sender, EventArgs e)
        {
            OpenTasksList.Visible = false;
        }
        protected void DeleteTask_Command(object sender, CommandEventArgs e)
        {
            string strDel = "delete from Tasks where ID = @ID ";
            SqlCommand cmdDel = new SqlCommand(strDel);
            cmdDel.Parameters.AddWithValue("@ID", e.CommandArgument);
            if (DbProvider.ExecuteCommand(cmdDel) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                FormError_label.Text = "* התרחשה שגיאה";
                FormError_label.Visible = true;
                FormErrorBottom_label.Text = "* התרחשה שגיאה";
                FormErrorBottom_label.Visible = true;
            }
            PopUpTasksList_Click(sender, null);
        }
        protected void PopUpTasksList_Click(object sender, EventArgs e)
        {
            OpenTasksList.Visible = true;
            UpdatePanel2.Update();
            SqlCommand cmdSelectTasks = new SqlCommand("select t.ID, Text, ts.Status, CONVERT(varchar,PerformDate, 104) as PerformDate from Tasks t left join TaskStatuses ts on t.Status = ts.ID where OfferID = @ID");
            cmdSelectTasks.Parameters.AddWithValue("@ID", Request.QueryString["OfferID"]);
            DataSet ds = DbProvider.GetDataSet(cmdSelectTasks);
          Repeater3.DataSource = ds;
            Repeater3.DataBind();

        }

        protected void SendSms_Click(object sender, EventArgs e)
        {
            StringBuilder msg1 = new StringBuilder();

            msg1.AppendLine("מצורפים מסמכים");
            msg1.AppendLine();
            msg1.AppendLine("בברכה,");
            msg1.AppendLine("פלטינום רפואה משלימה בעמ");
            msg1.AppendLine("*3779");
            msg1.AppendLine("");
            if (Repeater1.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('אין קבצים לשליחה');", true);
                return;
            }
            foreach (RepeaterItem item in Repeater1.Items)
            {
                CheckBox chkAttach = (CheckBox)item.FindControl("IsPromoted");
                Label lblFilePath = (Label)item.FindControl("FileName");

                if (chkAttach.Checked)
                {
                    string filePath = String.Format("{0}/OfferDocuments/{1}", ConfigurationManager.AppSettings["MapPath"], lblFilePath.Text);

                    msg1.AppendLine(filePath);

                }
            }
            if (!msg1.ToString().Contains("OfferDocuments"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('עליך לסמן קבצים לשליחה');", true);
                return;
            }
            //msg1.AppendLine(ConfigurationManager.AppSettings["FilesUrl"] + "SalesFile/" + dt.Rows[0]["SalesFile"].ToString());

            //msg1.AppendLine(strPath);

            var res = Helpers.SendSmsAsync("0505122922", msg1.ToString());

            try
            {
                if (res.Result == true)
                {
                                    
                }

            }
            catch (AggregateException ex)
            {
                
            }

        }

        public bool funcSave(object sender, EventArgs e)
        {

            int ErrorCount = 0;
            FormError_label.Visible = false;
            if (string.IsNullOrEmpty(NameOffer.Value))
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין שם ההצעה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין שם ההצעה";
                return false;
            }
            if (SelectSourceLoanOrInsurance.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מקור ההלוואה/ביטוח";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מקור ההלוואה/ביטוח";
                return false;
            }
            if (SelectOfferType.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין סוג הצעה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין סוג הצעה";
                return false;
            }

            //if (string.IsNullOrEmpty(ReasonLackSuccess.Value))
            //{
            //    ErrorCount++;
            //    FormError_label.Visible = true;
            //    FormError_label.Text = "יש להזין סיבה לחוסר הצלחה";
            //    FormErrorBottom_label.Visible = true;
            //    FormErrorBottom_label.Text = "יש להזין סיבה לחוסר הצלחה";
            //    return false;
            //}
            if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) == 5)
            {
                if (SelectStatusOffer.SelectedIndex == 10 && string.IsNullOrEmpty(ReturnDateToCustomer.Value))
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין מועד חזרה ללקוח";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין מועד חזרה ללקוח";
                    return false;
                }
              /*  if (SelectStatusOffer.SelectedIndex == 3 && string.IsNullOrEmpty(DateSentToInsuranceCompany.Value))
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין תאריך שליחה לחברת הביטוח";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין תאריך שליחה לחברת הביטוח";
                    return false;
                }*/
                if ((SelectStatusOffer.SelectedIndex == 9 || SelectStatusOffer.SelectedIndex == 5 || SelectStatusOffer.SelectedIndex == 4) && string.IsNullOrEmpty(ReasonLackSuccess.Value))
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין סיבה לחוסר הצלחה";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין סיבה לחוסר הצלחה";
                    return false;
                }
            }
            //if (SelectStatusOffer.SelectedIndex == 0)
            //{
            //    ErrorCount++;
            //    FormError_label.Visible = true;
            //    FormError_label.Text = "יש להזין סטטוס הצעה";
            //    return false;
            //}
            //if (SelectTurnOffer.SelectedIndex == 0)
            //{
            //    ErrorCount++;
            //    FormError_label.Visible = true;
            //    FormError_label.Text = "יש להזין תור";
            //    return false;
            //}
            if (ErrorCount == 0)
            {
                
                SqlCommand cmdInsert = new SqlCommand(@"update Offer set SourceLoanOrInsuranceID=@SourceLoanOrInsuranceID,OfferTypeID=@OfferTypeID,ReasonLackSuccess=@ReasonLackSuccess,
                                                        ReturnDateToCustomer=@ReturnDateToCustomer,DateSentToInsuranceCompany=@DateSentToInsuranceCompany,Note=@Note,StatusOfferID=@StatusOfferID
                                                        ,NameOffer=@NameOffer, CompletedDate=@CompletedDate 
                                                        where ID = @OfferID");
                //,TurnOfferID=@TurnOfferID
                cmdInsert.Parameters.AddWithValue("@SourceLoanOrInsuranceID", SelectSourceLoanOrInsurance.Value);
                cmdInsert.Parameters.AddWithValue("@OfferTypeID", SelectOfferType.Value);
                cmdInsert.Parameters.AddWithValue("@ReasonLackSuccess", ReasonLackSuccess.Value);
                cmdInsert.Parameters.AddWithValue("@ReturnDateToCustomer", string.IsNullOrEmpty(ReturnDateToCustomer.Value) ? (object)DBNull.Value :  DateTime.Parse(ReturnDateToCustomer.Value));
                cmdInsert.Parameters.AddWithValue("@DateSentToInsuranceCompany", string.IsNullOrEmpty(DateSentToInsuranceCompany.InnerText) ? (object)DBNull.Value : DateTime.ParseExact(DateSentToInsuranceCompany.InnerText, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                cmdInsert.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note.Value) ? (object)DBNull.Value : Note.Value);
                cmdInsert.Parameters.AddWithValue("@StatusOfferID", SelectStatusOffer.SelectedValue);
                //cmdInsert.Parameters.AddWithValue("@TurnOfferID", SelectTurnOffer.Value);
                cmdInsert.Parameters.AddWithValue("@NameOffer", NameOffer.Value);
                cmdInsert.Parameters.AddWithValue("@CompletedDate", (SelectStatusOffer.SelectedValue == "9" && SelectStatusOffer.SelectedValue != CurrentStatusOfferID.Value)?DateTime.Now.ToString(): (object)DBNull.Value);
                cmdInsert.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);

                if (DbProvider.ExecuteCommand(cmdInsert) > 0)
                {
                    if(SelectStatusOffer.SelectedValue != CurrentStatusOfferID.Value)
                    {
                        SqlCommand cmdOfferHistory = new SqlCommand("insert into OfferHistory(OfferID, StatusID, AgentID) values(@OfferID, @StatusID, @AgentID)");
                        cmdOfferHistory.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                        cmdOfferHistory.Parameters.AddWithValue("@StatusID", SelectStatusOffer.SelectedValue);
                        cmdOfferHistory.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"].ToString());
                        DbProvider.ExecuteCommand(cmdOfferHistory);
                    }

                    if ((SelectStatusOffer.SelectedValue == "9" || SelectStatusOffer.SelectedValue == "2") && SelectStatusOffer.SelectedValue != CurrentStatusOfferID.Value)
                    {
                        string text = "ממתין להשלמת חוסרים בהצעה: ";
                        if (SelectStatusOffer.SelectedValue.Equals("9"))
                        {
                            text = "סיום טיפול בהצעה: ";
                        }
                       
                        string sqlBranchManager = @"SELECT a.ParentID, a.ID from Offer
                                                    INNER JOIN [Lead] l On l.ID = Offer.LeadID
                                                    INNER JOIN ArvootManagers a on a.ID = l.AgentID WHERE Offer.ID = @OfferID";
                        SqlCommand cmdBranchManager = new SqlCommand(sqlBranchManager);
                        cmdBranchManager.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                        DataTable dt = DbProvider.GetDataTable(cmdBranchManager);

                        if (dt.Rows.Count > 0 )
                        {
                            string sqlAlert = "INSERT INTO Alerts (AgentID, Text, CreationDate, DisplayDate, OfferID) Values (@AgentID, @Text, GETDATE(), GETDATE(), @OfferID)";
                            SqlCommand cmdAlert = new SqlCommand(sqlAlert);
                            cmdAlert.Parameters.AddWithValue("@AgentID", dt.Rows[0]["ParentID"].ToString());
                            cmdAlert.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                            cmdAlert.Parameters.AddWithValue("@Text", text + NameOffer.Value + " על ידי: " + lblOwner.InnerText);
                            DbProvider.ExecuteCommand(cmdAlert); 
                            string sqlAlert2 = "INSERT INTO Alerts (AgentID, Text, CreationDate, DisplayDate, OfferID) Values (@AgentID, @Text, GETDATE(), GETDATE(), @OfferID)";
                            SqlCommand cmdAlert2 = new SqlCommand(sqlAlert2);
                            cmdAlert2.Parameters.AddWithValue("@AgentID", dt.Rows[0]["ID"].ToString());
                            cmdAlert2.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                            cmdAlert2.Parameters.AddWithValue("@Text", text + NameOffer.Value + " על ידי: " + lblOwner.InnerText);
                            DbProvider.ExecuteCommand(cmdAlert2);
                        }
                        
                    }
                    try
                    {
                        List<FileDetail> myFile = (List<FileDetail>)Session["UploadedFiles"];
                        if (myFile != null)
                        {
                            for (int i = 0; i < myFile.Count; i++)
                            {
                                try
                                {
                                    string FilePath1 = String.Format("{0}/OfferDocuments/", ConfigurationManager.AppSettings["MapPath"]);
                                    string FileName1 = myFile[i].FileName;

                                    myFile[i].PostedFile.SaveAs(Path.Combine(FilePath1, FileName1));
                                    SqlCommand cmdInsertDoc = new SqlCommand(@"insert into OfferDocuments (FileName,OfferID) 
                                                     values(@FileName,@OfferID)");
                                    cmdInsertDoc.Parameters.AddWithValue("@FileName", FileName1);
                                    cmdInsertDoc.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                                    DbProvider.ExecuteCommand(cmdInsertDoc);
                                }
                                catch (Exception) { }
                            }
                            Session["UploadedFiles"] = null;
                            return true;
                        }
                        /* else
                         {
                             ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                             FormError_label.Text = "* התרחשה שגיאה";
                             FormError_label.Visible = true;
                             FormErrorBottom_label.Text = "* התרחשה שגיאה";
                             FormErrorBottom_label.Visible = true;
                         }*/
                    }catch(Exception ex) { }
                    return true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                    FormError_label.Text = "* התרחשה שגיאה";
                    FormError_label.Visible = true;
                    FormErrorBottom_label.Text = "* התרחשה שגיאה";
                    FormErrorBottom_label.Visible = true;
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
                //Response.Redirect("Contact.aspx?ContactID=" + ContactID.Value);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                loadData();
                // System.Web.HttpContext.Current.Response.Redirect(ListPageUrl);

            }
           
        }
        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        protected void ServiceRequestAdd_Click(object sender, ImageClickEventArgs e)
        {
            
            System.Web.HttpContext.Current.Response.Redirect("ServiceRequestAdd.aspx?OfferID=" + Request.QueryString["OfferID"]);

        }
        public class FileDetail
        {
            public string FileName { get; set; }
            public string FileBase64String { get; set; }
            public int FileSize { get; set; }
         
            public HttpPostedFile PostedFile { get; set; }
        }

        protected void btnMoveToOperatingQueqe_Click(object sender, EventArgs e)
        {
            SqlCommand cmdMove = new SqlCommand("Update Offer SET IsInOperatingQueue = 1, OperatingQueueDate = getdate() WHERE ID = @offerID");
            cmdMove.Parameters.AddWithValue("@offerID", Request.QueryString["OfferID"]);
            if (DbProvider.ExecuteCommand(cmdMove) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                SqlCommand cmdOfferHistory = new SqlCommand("insert into OfferHistory(OfferID, AgentID, Message) values(@OfferID, @AgentID, @Message)");
                cmdOfferHistory.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                cmdOfferHistory.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"].ToString());
                cmdOfferHistory.Parameters.AddWithValue("@Message", "הועבר לתור תפעול");
                DbProvider.ExecuteCommand(cmdOfferHistory);
                Response.Redirect("Contact.aspx?ContactID=" + ContactID.Value);
            }
        }
        protected void OpenContact_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Response.Redirect("Contact.aspx?ContactID=" + ContactID.Value);

        }

        protected void btnMoveToOperator_Click(object sender, EventArgs e) {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);

            SqlCommand cmdOperators = new SqlCommand(@"select FullName as OperatorName,ID from ArvootManagers where Show = 1 and ( Type = 5 and ParentID in(
                                                       select ID from ArvootManagers where Type = 3 and ParentID = (
                                                       select ParentID from ArvootManagers where ID = (select ParentID from ArvootManagers where ID = @ID )))) or ( Type = 4 and ParentID in(
                                                       select ID from ArvootManagers where Type = 3 and ParentID = (
                                                       select ParentID from ArvootManagers where ID = (select ParentID from ArvootManagers where ID = @ID ))))");

            cmdOperators.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"].ToString());
            DataSet dsOperators = DbProvider.GetDataSet(cmdOperators);
            OperatorsList.DataSource = dsOperators;
            OperatorsList.DataTextField = "OperatorName";
            OperatorsList.DataValueField = "ID";
            OperatorsList.DataBind();
            OperatorsList.Items.Insert(0, new ListItem("חפש מתפעלת", ""));
            MoveToOperatorPopUp.Visible = true;
            UpdatePanel2.Update();

        }
        protected void btnMoveToAgent_Click(object sender, EventArgs e) {
            string sql = "update Offer set OperatorID = @OperatorID where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql);
            sqlCommand.Parameters.AddWithValue("@OperatorID", DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ID", Request.QueryString["OfferID"]);
            DbProvider.ExecuteCommand(sqlCommand);

            Response.Redirect("Offers.aspx");

        }


    }
}