using System;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using ListItem = System.Web.UI.WebControls.ListItem;
using Image = System.Web.UI.WebControls.Image;

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
        protected void CloseCreateDoc_Click(object sender, EventArgs e)
        {
            CreateDocPopup.Visible = false;
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
                string sql="";
                SqlCommand cmd = new SqlCommand();
                if (String.IsNullOrEmpty(ID.Value))
                {

                    sql = @" INSERT INTO [Tasks]( Text
                        ,Status
                        ,OfferID
                        ,PerformDate)
                         VALUES (
	                      @Text
                         ,@Status
                         ,@OfferID
                         ,@PerformDate)";
                    cmd.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);

                }
                else
                {
                     sql = "Update [Tasks] set Text = @Text ,Status = @Status ,PerformDate = @PerformDate where ID = @ID";
                     cmd.Parameters.AddWithValue("@ID",ID.Value);
                }

                cmd.CommandText =sql;

                cmd.Parameters.AddWithValue("@Text", TextTask.Value);
                cmd.Parameters.AddWithValue("@Status", SelectStatusTask.Value);
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
                if (!String.IsNullOrEmpty(ID.Value)) PopUpTasksList_Click(sender, null);
            }
        }

        protected void OpenTask_Click(object sender, ImageClickEventArgs e)
        {
            titleTask.InnerText = "משימה חדשה";
            AddNewTask.Text = "פתח משימה";
            TextTask.InnerText ="";
            Date.Value = "";
            ID.Value = "";
            SelectStatusTask.Value = "";
            ID.Value = "";
            TaskDiv.Visible = true;
            UpdatePanel2.Update();


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
           

            string sql = @"select Lead.Tz, Lead.FirstName+' '+Lead.LastName as FullName, A.FullName as FullNameAgent, Lead.AgentID, parent2.CompanyName from Lead
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
                                Note,CONVERT(varchar,Offer.DateSentToInsuranceCompany, 104) as DateSentToInsuranceCompany, StatusOfferID,NameOffer, DateSignPdfFile from Offer where ID = @OfferID";
            SqlCommand cmdOffer = new SqlCommand(sqlOffer);
            cmdOffer.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
            DataTable dtOffer = DbProvider.GetDataTable(cmdOffer);
            if (dtOffer.Rows.Count > 0)
            {
                DataRow rowOffer = dtOffer.Rows[0];
                ContactID.Value = rowOffer["LeadID"].ToString();
          //      PdfFile.Value = rowOffer["PdfFile"].ToString(); 
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


                SqlCommand sqlCmd = new SqlCommand(@"select ID from( SELECT  a.ID, a.ParentID
                                                                     FROM ArvootManagers a
                                                                     LEFT JOIN ArvootManagers b ON a.ParentID = b.ID
                                                                     LEFT JOIN ArvootManagers c ON b.ParentID = c.ID
                                                                     WHERE a.ID = 18   OR a.ParentID = 18                
                                                                     OR (b.ID IS NOT NULL AND b.ParentID = 18) 
                                                                     OR (c.ID IS NOT NULL AND c.ParentID = 18)
                                                                    ) as myAgents where ID = @ID");
                sqlCmd.Parameters.AddWithValue("@ID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                if (DbProvider.GetOneParamValueLong(sqlCmd) > 0)
                {
                    if (String.IsNullOrEmpty(rowOffer["DateSignPdfFile"].ToString()))
                        btnCreateDoc.Visible = true;
                    else btnDownloadDoc.Visible = true;
                }

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
        protected void btnDownloadDoc_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("select PdfFile from Offer where ID = @ID");
            cmd.Parameters.AddWithValue("@ID", Request.QueryString["OfferID"]);
            string PdfFile = DbProvider.GetOneParamValueString(cmd);
            Response.Redirect("DownloadFile.ashx?fileName=" + PdfFile + "&OfferID=" + Request.QueryString["OfferID"] + "&PdfFile=1");

        }

        protected void btnCreateDoc_Click(object sender, EventArgs e)
        {
            string[] options = new string[] {
                    "ניתוח תיק פנסיוני מקיף–מסלקה פנסיונית",
                    "הגשת בקשה להלוואה כנגד קופות (פנסיה, גמל, השתלמות, ביטוח מנהלים)",
                    "הגשת בקשה לפדיון כספים מחברות הביטוח",
                    "הלוואה כנגד הנכס(בנקאי/חוץ בנקאי)",
                     "הגשת בקשה להלוואה (בנקאי/חוץ בנקאי)"
            };

            string[] months = {
                     "01", "02", "03", "04", "05", "06",
                     "07", "08", "09", "10", "11", "12"
                };
            SelectMonth.DataSource = months;
            SelectMonth.DataBind();
            List<string> years = new List<string>();
            for (int i = DateTime.Now.Year; i <= DateTime.Now.Year + 30; i++)
            {
                years.Add(i.ToString());
            }

            SelectYear.DataSource = years.ToArray();
            SelectYear.DataBind();

            Repeater5.DataSource = options;
            Repeater5.DataBind();
            CreateDocPopup.Visible = true;
            UpdatePanel2.Update();
        } 
        protected void btn_sendDoc_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(CreditNumber.Value.ToString()) || String.IsNullOrEmpty(Cvv.Value.ToString()) || String.IsNullOrEmpty(CreditHolderName.Value.ToString()) || String.IsNullOrEmpty(CardholdersID.Value.ToString()))
            {
                error.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);

                return;
            }
            else error.Visible = false;

            CreateDocPopup.Visible = false;
            string fileName = exportPDF();
            SqlCommand cmd = new SqlCommand("update Offer set IsSignPdf = 1,PdfFile = @PdfFile where ID = @ID ");
            cmd.Parameters.AddWithValue("@PdfFile", fileName);
            cmd.Parameters.AddWithValue("@ID", Request.QueryString["OfferID"]);
            DbProvider.ExecuteCommand(cmd);
            SqlCommand cmdPhone = new SqlCommand(@"select top 1 Phone1 from Lead inner join Offer on Offer.LeadID = Lead.ID where Offer.ID=@ID ");
            cmdPhone.Parameters.AddWithValue("@ID", Request.QueryString["OfferID"]);
            DataTable dt = DbProvider.GetDataTable(cmdPhone);

            if (dt.Rows.Count > 0)
            {

                StringBuilder msg1 = new StringBuilder();

                msg1.AppendLine("לקוח יקר!");
                msg1.AppendLine("מצורף טופס הסכם שירות לחתימתך");
                msg1.AppendLine();
                msg1.AppendLine("בברכה,");
                msg1.AppendLine("פיננסים בעמ");
                msg1.AppendLine("");
                msg1.AppendLine("https://sign.arvoot-crm.co.il/PdfFile.aspx?ID=" + Request.QueryString["OfferID"]);
/*                		dt.Rows[0]["Phone1"].ToString()	
*/

                var res = Helpers.SendSmsAsync(dt.Rows[0]["Phone1"].ToString(), msg1.ToString());
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);

                try
                {
                    if (res.Result == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('הטופס נשלח ללקוח לחתימה');", true);
                        System.Web.HttpContext.Current.Response.Redirect("OfferEdit.aspx?OfferID=" + Request.QueryString["OfferID"]);

                    }

                }
                catch (Exception ex)
                {
                }

            }


        }
        protected void Mession_Edit(object sender, CommandEventArgs e)
        {
            SqlCommand cmdTaskStatuses = new SqlCommand("SELECT * FROM TaskStatuses");
            DataSet dsTaskStatuses = DbProvider.GetDataSet(cmdTaskStatuses);
            SelectStatusTask.DataSource = dsTaskStatuses;
            SelectStatusTask.DataTextField = "Status";
            SelectStatusTask.DataValueField = "ID";
            SelectStatusTask.DataBind();

            string strTasks = @"select * from Tasks  where ID = @ID ";
            SqlCommand cmdTasks = new SqlCommand(strTasks);
            cmdTasks.Parameters.AddWithValue("@ID", e.CommandArgument);
            DataTable dtTasks = DbProvider.GetDataTable(cmdTasks);
            if (dtTasks.Rows.Count > 0)
            {
                titleTask.InnerText = "עדכון משימה";
                AddNewTask.Text = "שמור";
                TextTask.InnerText = dtTasks.Rows[0]["Text"].ToString();
                //  DateTime date = DateTime.ParseExact(dtTasks.Rows[0]["PerformDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dateTime = Convert.ToDateTime(dtTasks.Rows[0]["PerformDate"]);
                Date.Value = dateTime.ToString("yyyy-MM-dd");
                ID.Value = dtTasks.Rows[0]["ID"].ToString();
                SelectStatusTask.Value = dtTasks.Rows[0]["Status"].ToString();
                TaskDiv.Visible = true;
                UpdatePanel2.Update();
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
        public string exportPDF()
        {
            DateTime theDate = DateTime.Now;
            string FileName = "פיננסים - " + DateTime.Now;
            string FileName1 = Md5.GetMd5Hash(Md5.CreateMd5Hash(), "1" + Helpers.CreateFileName(FileName)) + ".pdf";
            //string FileName1 = Md5.GetMd5Hash(Md5.CreateMd5Hash(), "1" + Helpers.CreateFileName(FileName));
            //-1בדיקה האם נשמר 2האם לשמור בדטה בייס ואחרכ בשרת?? שמירה בשרת 
            string strPath = String.Format("{0}/pdf/{1}", ConfigurationManager.AppSettings["MapPath"], FileName1);
            string sql = @"select Lead.Tz, Lead.FirstName, Lead.LastName, Lead.Phone1, Lead.Address, Lead.Email from Lead
                           inner join Offer on Offer.LeadID = Lead.ID                        
                           where Offer.ID = @OfferID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
            DataTable dt = DbProvider.GetDataTable(cmd);
            try
            {
                using (FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate))
                {


                   


                    Document document = new Document(PageSize.A4, 50, 50, 50, 50);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.Open();

                    /*    string fontPath = "C:/Users/AppGate/source/repos/arvoot-crm.co.il/arvoot-crm.co.il/css/opensanshebrew/OpenSansHebrew-Regular.ttf";
                        string boldFontPath = "C:/Users/AppGate/source/repos/arvoot-crm.co.il/arvoot-crm.co.il/css/opensanshebrew/OpenSansHebrew-Bold.ttf";*/

                    string fontPath = "C:/inetpub/vhosts/arvoot-crm.co.il/httpdocs/css/opensanshebrew/OpenSansHebrew-Regular.ttf";
                    string boldFontPath = "C:/inetpub/vhosts/arvoot-crm.co.il/httpdocs/css/opensanshebrew/OpenSansHebrew-Bold.ttf";
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    BaseFont boldBaseFont = BaseFont.CreateFont(boldFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font font = new Font(baseFont, 10);
                    Font boldFont = new Font(boldBaseFont, 10);
                    Font underlineFontTitle = new Font(boldBaseFont, 12, Font.UNDERLINE | Font.BOLD);
                    Font underlineFont = new Font(baseFont, 10, Font.UNDERLINE | Font.BOLD);

                    Font underlineFontBold = new Font(boldBaseFont, 10, Font.UNDERLINE | Font.BOLD);

                    // כותרת מרכזית
                    PdfPTable tableTitle = new PdfPTable(1); // טבלה עם עמודה אחת
                    tableTitle.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    tableTitle.WidthPercentage = 100;
                    tableTitle.SpacingAfter = 10;


                    PdfPCell cell = new PdfPCell(new Phrase("הסכם שירות – הגשת בקשה לטיפול מקיף בגין הלוואה/פדיון", underlineFontTitle));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Border = PdfPCell.NO_BORDER;

                    tableTitle.AddCell(cell);
                    document.Add(tableTitle);


                    document.Add(new Paragraph("\n")); // שורת רווח

                    // PdfPTable table1 = new PdfPTable(4);
                    // table1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    // table1.HorizontalAlignment = Element.ALIGN_LEFT;
                    // table1.WidthPercentage = 100;
                    // table1.SetWidths(new float[] { 2, 2, 2, 2 });

                    //// הוספת שורות לטבלה(כל שורה מכילה שני שדות – ימין ושמאל)
                    // AddTableRowWithLines(table1, "שם משפחה:", "אורית", "שם פרטי:", "", font);
                    // AddTableRowWithLines(table1, "טלפון נייד:", "", "ת.ז:", "", font);
                    // AddTableRowWithLines(table1, "כתובת:", "", "", "", font);
                    // //הוספת הטבלה למסמך
                    // document.Add(table1);
                    /////////////////////////////////////////////
                    PdfPTable detailsTable = new PdfPTable(5);
                    detailsTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTable.WidthPercentage = 100;
                    detailsTable.SpacingAfter = 10;
                    //float[] widths2 = new float[] { 15f, 10f, 20f, 15f, 20f, 20f };
                    float[] widths2 = new float[] { 21f, 25f, 10f, 25f, 8f };

                    detailsTable.SetWidths(widths2);

                    PdfPCell firstNameCell = new PdfPCell(new Phrase("שם פרטי:", font));
                    firstNameCell.BorderWidth = 0;
                    //firstNameCell.BorderWidthBottom = 1;
                    firstNameCell.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שני - ריק עם קו
                    PdfPCell emptyCell1 = new PdfPCell(new Phrase(dt.Rows[0]["FirstName"].ToString(), font));
                    emptyCell1.BorderWidth = 0;
                    emptyCell1.BorderWidthBottom = 1;

                    // תא ראשון - ת.ז
                    PdfPCell idCell = new PdfPCell(new Phrase("שם משפחה:", font));
                    idCell.BorderWidth = 0;
                    //idCell.BorderWidthBottom = 1;
                    idCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    /**/
                    // תא שישי - ריק עם קו
                    PdfPCell emptyCell3 = new PdfPCell(new Phrase(dt.Rows[0]["LastName"].ToString(), font));
                    emptyCell3.BorderWidth = 0;
                    emptyCell3.BorderWidthBottom = 1;

                    PdfPCell emptyCell44 = new PdfPCell(new Phrase(""));
                    emptyCell44.BorderWidth = 0;

                    // הוספת התאים לטבלה
                    detailsTable.AddCell(firstNameCell);
                    detailsTable.AddCell(emptyCell1);
                    detailsTable.AddCell(idCell);
                    detailsTable.AddCell(emptyCell3);
                    detailsTable.AddCell(emptyCell44);

                    document.Add(detailsTable);

                    /////////////////////////////////////
                    PdfPTable detailsTable1 = new PdfPTable(5);
                    detailsTable1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTable1.WidthPercentage = 100;
                    detailsTable1.SpacingAfter = 10;
                    //float[] widths2 = new float[] { 15f, 10f, 20f, 15f, 20f, 20f };
                    float[] widths3 = new float[] { 21f, 27f, 8f, 29f, 4f };

                    detailsTable1.SetWidths(widths3);

                    PdfPCell firstNameCell1 = new PdfPCell(new Phrase("ת.ז:", font));
                    firstNameCell1.BorderWidth = 0;
                    //firstNameCell.BorderWidthBottom = 1;
                    firstNameCell1.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שני - ריק עם קו
                    PdfPCell emptyCell2 = new PdfPCell(new Phrase(dt.Rows[0]["Tz"].ToString(), font));
                    emptyCell2.BorderWidth = 0;
                    emptyCell2.BorderWidthBottom = 1;

                    // תא ראשון - ת.ז
                    PdfPCell idCell2 = new PdfPCell(new Phrase("טלפון נייד:", font));
                    idCell2.BorderWidth = 0;
                    //idCell.BorderWidthBottom = 1;
                    idCell2.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שישי - ריק עם קו
                    PdfPCell emptyCell4 = new PdfPCell(new Phrase(dt.Rows[0]["Phone1"].ToString()));
                    emptyCell4.BorderWidth = 0;
                    emptyCell4.BorderWidthBottom = 1;

                    PdfPCell emptyCell5 = new PdfPCell(new Phrase(""));
                    emptyCell5.BorderWidth = 0;

                    // הוספת התאים לטבלה
                    detailsTable1.AddCell(firstNameCell1);
                    detailsTable1.AddCell(emptyCell2);
                    detailsTable1.AddCell(idCell2);
                    detailsTable1.AddCell(emptyCell4);
                    detailsTable1.AddCell(emptyCell5);

                    document.Add(detailsTable1);

                    ///////////////////////////////////
                    PdfPTable detailsTable9 = new PdfPTable(3);
                    detailsTable9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTable9.WidthPercentage = 100;
                    detailsTable9.SpacingAfter = 10;
                    //float[] widths2 = new float[] { 15f, 10f, 20f, 15f, 20f, 20f };
                    float[] widths9 = new float[] { 56f, 27f, 6f };

                    detailsTable9.SetWidths(widths9);

                    PdfPCell firstNameCell9 = new PdfPCell(new Phrase("כתובת:", font));
                    firstNameCell9.BorderWidth = 0;
                    //firstNameCell.BorderWidthBottom = 1;
                    firstNameCell9.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שני - ריק עם קו
                    PdfPCell emptyCell9 = new PdfPCell(new Phrase(dt.Rows[0]["Address"].ToString(), font));
                    emptyCell9.BorderWidth = 0;
                    emptyCell9.BorderWidthBottom = 1;


                    PdfPCell emptyCell99 = new PdfPCell(new Phrase(""));
                    emptyCell99.BorderWidth = 0;

                    // הוספת התאים לטבלה
                    detailsTable9.AddCell(firstNameCell9);
                    detailsTable9.AddCell(emptyCell9);
                    detailsTable9.AddCell(emptyCell99);


                    document.Add(detailsTable9);
                    ///////////////////////////////////
                    PdfPTable detailsTable5 = new PdfPTable(5);
                    detailsTable5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTable5.WidthPercentage = 100;
                    detailsTable5.SpacingAfter = 10;
                    //float[] widths2 = new float[] { 15f, 10f, 20f, 15f, 20f, 20f };
                    float[] widths5 = new float[] { 21f, 26f, 9f, 26.9f, 6f };

                    detailsTable5.SetWidths(widths5);

                    PdfPCell firstNameCell5 = new PdfPCell(new Phrase("תאריך:", font));
                    firstNameCell5.BorderWidth = 0;
                    //firstNameCell.BorderWidthBottom = 1;
                    firstNameCell5.HorizontalAlignment = Element.ALIGN_LEFT;
                    // תא שני - ריק עם קו
                    PdfPCell emptyCell55 = new PdfPCell(new Phrase(DateTime.Now.ToShortDateString(), font));
                    emptyCell55.BorderWidth = 0;
                    emptyCell55.BorderWidthBottom = 1;

                    // תא ראשון - ת.ז
                    PdfPCell idCell5 = new PdfPCell(new Phrase("נציג מטפל:", font));
                    idCell5.BorderWidth = 0;
                    //idCell.BorderWidthBottom = 1;
                    idCell5.HorizontalAlignment = Element.ALIGN_LEFT;
                    /**/
                    // תא שישי - ריק עם קו
                    PdfPCell emptyCell555 = new PdfPCell(new Phrase(HttpContext.Current.Session["AgentName"].ToString(), font));
                    emptyCell555.BorderWidth = 0;
                    emptyCell555.BorderWidthBottom = 1;

                    PdfPCell emptyCell54 = new PdfPCell(new Phrase(""));
                    emptyCell54.BorderWidth = 0;

                    // הוספת התאים לטבלה
                    detailsTable5.AddCell(firstNameCell5);
                    detailsTable5.AddCell(emptyCell55);
                    detailsTable5.AddCell(idCell5);
                    detailsTable5.AddCell(emptyCell555);
                    detailsTable5.AddCell(emptyCell54);

                    document.Add(detailsTable5);

                    ///////////////////////////////////
                    // כותרת מרכזית
                    PdfPTable tableTitle2 = new PdfPTable(1); // טבלה עם עמודה אחת
                    tableTitle2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    tableTitle2.WidthPercentage = 100;
                    tableTitle2.SpacingAfter = 10;


                    PdfPCell cell5 = new PdfPCell(new Phrase("על הלקוח לצרף את המסמכים הבאים:", underlineFontBold));
                    cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell5.Border = PdfPCell.NO_BORDER;

                    tableTitle2.AddCell(cell5);
                    document.Add(tableTitle2);
                    /////////////////////////////////////////////
                    PdfPTable table4 = new PdfPTable(1);
                    table4.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // כיוון מימין לשמאל
                    table4.WidthPercentage = 100; // רוחב מלא

                    string[] rows = { "צילום תעודת זיהוי כולל ספח (צילום של שני הצדדים)", "אישור ניהול חשבון / שיק מבוטל", "אמצעי תשלום של הלקוח.", "תלושי שכר ועוש במידת הצורך" };

                    for (int i = 0; i < rows.Length; i++)
                    {
                        PdfPCell cell4 = new PdfPCell(new Phrase($"{i + 1}. {rows[i]}", font));
                        cell4.HorizontalAlignment = Element.ALIGN_LEFT; // יישור לימין
                        cell4.Padding = 5;
                        cell4.Border = PdfPCell.NO_BORDER;
                        table4.AddCell(cell4);
                    }

                    // הוספת הטבלה למסמך
                    document.Add(table4);

                    ////////////////////////////////////////////
                    // כותרת מרכזית
                    PdfPTable tableTitle7 = new PdfPTable(1); // טבלה עם עמודה אחת
                    tableTitle7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    tableTitle7.WidthPercentage = 100;
                    tableTitle7.SpacingAfter = 10;


                    PdfPCell cell7 = new PdfPCell(new Phrase("הטיפול:", underlineFontBold));
                    cell7.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell7.Border = PdfPCell.NO_BORDER;


                    tableTitle7.AddCell(cell7);
                    document.Add(tableTitle7);
                    ////////////////////////////////////////////
                    // יצירת הטבלה הראשית
                    PdfPTable radioTable = new PdfPTable(2);
                    radioTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    radioTable.HorizontalAlignment = Element.ALIGN_LEFT;
                    radioTable.WidthPercentage = 100;
                    float[] colWidths = new float[] { 90f, 10f };
                    radioTable.SetWidths(colWidths);



                    // Define options text
                    string[] options = new string[] {
    "ניתוח תיק פנסיוני מקיף–מסלקה פנסיונית",
    "הגשת בקשה להלוואה כנגד קופות (פנסיה, גמל, השתלמות, ביטוח מנהלים)",
    "הגשת בקשה לפדיון כספים מחברות הביטוח",
    "הלוואה כנגד הנכס(בנקאי/חוץ בנקאי)",
     "הגשת בקשה להלוואה (בנקאי/חוץ בנקאי)"
};


                    // קביעת האפשרות הנבחרת
                    int selectedOption = 0;
                    for (int i = 0; i < options.Length; i++)
                    {
                        /**/
                        RepeaterItem r = Repeater5.Items[i];


                        string checkmark = (bool)((r.FindControl("CheckBox") as CheckBox)?.Checked) ? "V" : "";
                        PdfPTable optionTable = new PdfPTable(2);
                        optionTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        optionTable.WidthPercentage = 100;
                        //optionTable.SpacingAfter =5;
                        float[] widthsPage2 = new float[] { 140f, 5f };
                        optionTable.SetWidths(widthsPage2);

                        // תיבת הסימון
                        PdfPCell checkboxCell = new PdfPCell(new Phrase(checkmark, font));
                        checkboxCell.BorderWidth = 1;
                        checkboxCell.FixedHeight = 12f;
                        checkboxCell.BorderColor = BaseColor.BLACK;
                        checkboxCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        checkboxCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                        // תא טקסט האפשרות
                        PdfPCell optionCell = new PdfPCell(new Phrase(options[i], font));
                        optionCell.BorderWidth = 0;
                        optionCell.HorizontalAlignment = Element.ALIGN_LEFT;

                        optionTable.AddCell(checkboxCell);
                        optionTable.AddCell(optionCell);

                        document.Add(optionTable);
                        document.Add(new Paragraph(" ")); // רווח בין האפשרויות
                    }

                    // הוספת הטבלה למסמך
                    document.Add(radioTable);
                    ////////////////////////////////////////////

                    PdfPTable table = new PdfPTable(7); // 7 עמודות (6 ריקות + 1 לכותרות)
                    table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.WidthPercentage = 100;
                    //table.SpacingBefore = 8;
                    table.SpacingAfter = 20;
                    // הגדרת רוחב יחסי לעמודות - עמודת הכותרות רחבה יותר
                    table.SetWidths(new float[] { 1, 1, 1, 1, 1, 1, 1 });

                    // יצירת שורות עם כותרות בצד ימין ותאים ריקים משמאל
                    // שורה 1
                    PdfPCell headerCell1 = new PdfPCell(new Phrase("סוג קופה", underlineFontBold));
                    headerCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(headerCell1);
                    
                    PdfPCell emptyCell01 = new PdfPCell(new Phrase(PosType1.Text, font));
                    emptyCell01.MinimumHeight = 25f;
                    table.AddCell(emptyCell01);  
                    PdfPCell emptyCell02 = new PdfPCell(new Phrase(PosType2.Text, font));
                    emptyCell02.MinimumHeight = 25f;
                    table.AddCell(emptyCell02); 
                    PdfPCell emptyCell03 = new PdfPCell(new Phrase(PosType3.Text, font));
                    emptyCell03.MinimumHeight = 25f;
                    table.AddCell(emptyCell03); 
                    PdfPCell emptyCell04 = new PdfPCell(new Phrase(PosType4.Text, font));
                    emptyCell04.MinimumHeight = 25f;
                    table.AddCell(emptyCell04); 
                    PdfPCell emptyCell05 = new PdfPCell(new Phrase(PosType5.Text, font));
                    emptyCell5.MinimumHeight = 25f;
                    table.AddCell(emptyCell05);  
                    PdfPCell emptyCell6 = new PdfPCell(new Phrase(PosType6.Text, font));
                    emptyCell6.MinimumHeight = 25f;
                    table.AddCell(emptyCell6);
                    

                    // שורה 2
                    PdfPCell headerCell2 = new PdfPCell(new Phrase("סוג בקשה", underlineFontBold));
                    headerCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(headerCell2);

                    // הוספת תאים ריקים לשורה השנייה
                   
                   emptyCell01 = new PdfPCell(new Phrase(RequestType1.Text, font));
                    emptyCell01.MinimumHeight = 25f;
                   table.AddCell(emptyCell01);
                    emptyCell01 = new PdfPCell(new Phrase(RequestType2.Text, font));
                    emptyCell01.MinimumHeight = 25f;
                   table.AddCell(emptyCell01);
                    emptyCell01 = new PdfPCell(new Phrase(RequestType3.Text, font));
                    emptyCell01.MinimumHeight = 25f;
                   table.AddCell(emptyCell01);
                    emptyCell01 = new PdfPCell(new Phrase(RequestType4.Text, font));
                    emptyCell01.MinimumHeight = 25f;
                   table.AddCell(emptyCell01);
                    emptyCell01 = new PdfPCell(new Phrase(RequestType5.Text, font));
                    emptyCell01.MinimumHeight = 25f;
                   table.AddCell(emptyCell01);
                    emptyCell01 = new PdfPCell(new Phrase(RequestType6.Text, font));
                    emptyCell01.MinimumHeight = 25f;
                   table.AddCell(emptyCell01);
                   

                    // שורה 3
                    PdfPCell headerCell3 = new PdfPCell(new Phrase("חברת ביטוח", underlineFontBold));
                    headerCell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(headerCell3);

                    // הוספת תאים ריקים לשורה השלישית
                    PdfPCell emptyCell = new PdfPCell(new Phrase(InsuranceCompany1.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    emptyCell = new PdfPCell(new Phrase(InsuranceCompany2.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    emptyCell = new PdfPCell(new Phrase(InsuranceCompany3.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    emptyCell = new PdfPCell(new Phrase(InsuranceCompany4.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    emptyCell = new PdfPCell(new Phrase(InsuranceCompany5.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    emptyCell = new PdfPCell(new Phrase(InsuranceCompany6.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    

                    // שורה 4
                    PdfPCell headerCell4 = new PdfPCell(new Phrase("מספר קופה", underlineFontBold));
                    headerCell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(headerCell4);

                    // הוספת תאים ריקים לשורה הרביעית
                   
                    emptyCell = new PdfPCell(new Phrase(PosNumber1.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    emptyCell = new PdfPCell(new Phrase(PosNumber2.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    emptyCell = new PdfPCell(new Phrase(PosNumber3.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    emptyCell = new PdfPCell(new Phrase(PosNumber4.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    emptyCell = new PdfPCell(new Phrase(PosNumber5.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    emptyCell = new PdfPCell(new Phrase(PosNumber6.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    

                    // שורה 5
                    PdfPCell headerCell5 = new PdfPCell(new Phrase("סכום בקשה", underlineFontBold));
                    headerCell5.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(headerCell5);

                    // הוספת תאים ריקים לשורה החמישית
                    emptyCell = new PdfPCell(new Phrase(RequestSum1.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);  
                    emptyCell = new PdfPCell(new Phrase(RequestSum2.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);  
                    emptyCell = new PdfPCell(new Phrase(RequestSum3.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);  
                    emptyCell = new PdfPCell(new Phrase(RequestSum4.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);  
                    emptyCell = new PdfPCell(new Phrase(RequestSum5.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);  
                    emptyCell = new PdfPCell(new Phrase(RequestSum6.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    

                    // שורה 6
                    PdfPCell headerCell6 = new PdfPCell(new Phrase("הלוואה קיימת", underlineFontBold));
                    headerCell6.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(headerCell6);

                    // הוספת תאים ריקים לשורה השישית
                    emptyCell = new PdfPCell(new Phrase(ExistingLoan1.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell); 
                    emptyCell = new PdfPCell(new Phrase(ExistingLoan2.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell); 
                    emptyCell = new PdfPCell(new Phrase(ExistingLoan3.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell); 
                    emptyCell = new PdfPCell(new Phrase(ExistingLoan4.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell); 
                    emptyCell = new PdfPCell(new Phrase(ExistingLoan5.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell); 
                    emptyCell = new PdfPCell(new Phrase(ExistingLoan6.Text, font));
                    emptyCell.MinimumHeight = 25f;
                    table.AddCell(emptyCell);
                    

                    // הוספת הטבלה למסמך
                    document.Add(table);



                    PdfPTable T6 = new PdfPTable(1);
                    T6.DefaultCell.BorderWidth = 0;
                    T6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    T6.WidthPercentage = 100;
                    T6.SpacingAfter = 10;
                    T6.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    T6.AddCell(new Phrase("פרטי כרטיס אשראי:", underlineFontBold));
                    document.Add(T6);


                    PdfPTable detailsTable2 = new PdfPTable(5);
                    detailsTable2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTable2.WidthPercentage = 100;
                    detailsTable2.SpacingAfter = 10;
                    //float[] widths2 = new float[] { 15f, 10f, 20f, 15f, 20f, 20f };
                    float[] widths7 = new float[] { 21f, 25f, 17f, 25f, 12f };

                    detailsTable2.SetWidths(widths7);

                    PdfPCell firstNameCell22 = new PdfPCell(new Phrase("בעל הכרטיס:", font));
                    firstNameCell22.BorderWidth = 0;
                    //firstNameCell.BorderWidthBottom = 1;
                    firstNameCell22.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שני - ריק עם קו
                    PdfPCell emptyCell32 = new PdfPCell(new Phrase(CreditHolderName.Value, font));
                    emptyCell32.BorderWidth = 0;
                    emptyCell32.BorderWidthBottom = 1;

                    // תא ראשון - ת.ז
                    PdfPCell idCell88 = new PdfPCell(new Phrase("ת.ז של בעל הכרטיס:", font));
                    idCell88.BorderWidth = 0;
                    //idCell.BorderWidthBottom = 1;
                    idCell88.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שישי - ריק עם קו
                    PdfPCell emptyCell77 = new PdfPCell(new Phrase(CardholdersID.Value, font));
                    emptyCell77.BorderWidth = 0;
                    emptyCell77.BorderWidthBottom = 1;

                    PdfPCell emptyCell777 = new PdfPCell(new Phrase(""));
                    emptyCell777.BorderWidth = 0;

                    // הוספת התאים לטבלה
                    detailsTable2.AddCell(firstNameCell22);
                    detailsTable2.AddCell(emptyCell32);
                    detailsTable2.AddCell(idCell88);
                    detailsTable2.AddCell(emptyCell77);
                    detailsTable2.AddCell(emptyCell777);

                    document.Add(detailsTable2);



                    PdfPTable detailsTable8 = new PdfPTable(6);
                    detailsTable8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTable8.WidthPercentage = 100;
                    detailsTable8.SpacingAfter = 10;
                    //float[] widths2 = new float[] { 15f, 10f, 20f, 15f, 20f, 20f };
                    float[] widths8 = new float[] { 27f, 6f, 25f, 6f, 25f, 12f };

                    detailsTable8.SetWidths(widths8);

                    PdfPCell numCell = new PdfPCell(new Phrase("מספר כרטיס:", font));
                    numCell.BorderWidth = 0;
                    //firstNameCell.BorderWidthBottom = 1;
                    numCell.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שני - ריק עם קו
                    PdfPCell emptyCellNum = new PdfPCell(new Phrase(CreditNumber.Value, font));
                    emptyCellNum.BorderWidth = 0;
                    emptyCellNum.BorderWidthBottom = 1;

                    // תא ראשון - ת.ז
                    PdfPCell validityCell = new PdfPCell(new Phrase("תוקף:", font));
                    validityCell.BorderWidth = 0;
                    validityCell.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שישי - ריק עם קו
                    PdfPCell emptyCellvValidity = new PdfPCell(new Phrase(SelectMonth.Value + "/" +SelectYear.Value, font));
                    emptyCellvValidity.BorderWidth = 0;
                    emptyCellvValidity.BorderWidthBottom = 1;

                    PdfPCell cvvCell = new PdfPCell(new Phrase("CVV:", font));
                    cvvCell.BorderWidth = 0;
                    cvvCell.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שישי - ריק עם קו
                    PdfPCell emptyCellvCvv = new PdfPCell(new Phrase(Cvv.Value, font));
                    emptyCellvCvv.BorderWidth = 0;
                    emptyCellvCvv.BorderWidthBottom = 1;

                    // הוספת התאים לטבלה
                    detailsTable8.AddCell(numCell);
                    detailsTable8.AddCell(emptyCellNum);
                    detailsTable8.AddCell(validityCell);
                    detailsTable8.AddCell(emptyCellvValidity);
                    detailsTable8.AddCell(cvvCell);
                    detailsTable8.AddCell(emptyCellvCvv);

                    document.Add(detailsTable8);


                    PdfPTable detailsTable6 = new PdfPTable(3);
                    detailsTable6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTable6.WidthPercentage = 100;
                    detailsTable6.SpacingAfter = 10;
                    //float[] widths2 = new float[] { 15f, 10f, 20f, 15f, 20f, 20f };
                    float[] widths6 = new float[] { 64f, 25f, 12f };

                    detailsTable6.SetWidths(widths6);

                    PdfPCell costTreatedCell = new PdfPCell(new Phrase("עלות טיפול:", font));
                    costTreatedCell.BorderWidth = 0;
                    costTreatedCell.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שני - ריק עם קו
                    PdfPCell emptyCellCostTreated = new PdfPCell(new Phrase(Text4.Value, font));
                    emptyCellCostTreated.BorderWidth = 0;
                    emptyCellCostTreated.BorderWidthBottom = 1;

                    // תא שני - ריק עם קו
                    PdfPCell emptyCell21 = new PdfPCell(new Phrase("", font));
                    emptyCell21.BorderWidth = 0;

                    // הוספת התאים לטבלה
                    detailsTable6.AddCell(costTreatedCell);
                    detailsTable6.AddCell(emptyCellCostTreated);
                    detailsTable6.AddCell(emptyCell21);


                    document.Add(detailsTable6);

                    //////////////////////
                    PdfPTable detailsTableNote = new PdfPTable(2);
                    detailsTableNote.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTableNote.WidthPercentage = 100;
                    detailsTableNote.SpacingAfter = 10;
                    //float[] widths2 = new float[] { 15f, 10f, 20f, 15f, 20f, 20f };
                    float[] widthsNote = new float[] { 91f, 12f };

                    detailsTableNote.SetWidths(widthsNote);

                    PdfPCell noteCell = new PdfPCell(new Phrase("הערות:", font));
                    noteCell.BorderWidth = 0;
                    noteCell.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שני - ריק עם קו
                    PdfPCell emptyCellNote = new PdfPCell(new Phrase(Text1.Value, font));
                    emptyCellNote.BorderWidth = 0;
                    emptyCellNote.BorderWidthBottom = 1;

                    // תא שני - ריק עם קו
                    //PdfPCell emptyCellNote2 = new PdfPCell(new Phrase("", font));
                    //emptyCellNote2.BorderWidth = 0;

                    // הוספת התאים לטבלה
                    detailsTableNote.AddCell(noteCell);
                    detailsTableNote.AddCell(emptyCellNote);
                    //detailsTableNote.AddCell(emptyCellNote2);


                    document.Add(detailsTableNote);

                    //////////////////

                    document.NewPage();

                    PdfPTable T8 = new PdfPTable(1);
                    T8.DefaultCell.BorderWidth = 0;
                    T8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    T8.WidthPercentage = 100;
                    T8.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    T8.AddCell(new Phrase("הלקוח מצהיר ומבין כי:", underlineFontBold));
                    document.Add(T8);


                    // יצירת טבלה עם עמודה אחת
                    PdfPTable table14 = new PdfPTable(1);
                    table14.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // יישור מימין לשמאל
                    table14.WidthPercentage = 100; // רוחב מלא של העמוד
                    table14.DefaultCell.Border = Rectangle.NO_BORDER; // ללא גבולות
                    table14.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT; // יישור לימין

                    // הגדרת גופן שתומך בעברית
                    float lineSpacing = 8f;

                    // רשימת סעיפים
                    List<string> clauses = new List<string>
{
    "החברה הינה נותנת השירות האדמיניסטרטיבי ומקבלת התשלום, לרבות תשלום עבור מסלקה פנסיונית הנה חברת יהב – אור נ.א פיננסים בעמ ח.פ 516861820.",
    "מתן השירות האדמיניסטרטיבי הכולל את הזמנת המסלקה הפנסיונית, יינתן בהתאם לדין, מובהר בזאת כי החברה איננה תאגיד בנקאי וכי הסכם השירות הוא איננו הסכם פדיון כספים ו/או הסכם הלוואה מול החברה.",
    "תשלום עמלת הטיפול הינו בגין הגשת מסמכים לחברות ביטוח ו/או אחרות, וטיפול בכל הנדרש לצורך מימוש הפדיון ו/או ההלוואה בהתאם לבקשתו של הלקוח.",
    "הלקוח מצהיר ומבין כי יכל לבצע את ההליך באופן עצמאי או באמצעות איש מקצוע אחר או  חברה אחרת ובכל זאת בחר בחברה לצורך הטיפול האמור.",
    "במקרה של פדיון כספים מקופת פנסיה ו/או ביטוח מנהלים ו/או קופת גמל, הלקוח מצהיר ומבין כי הדבר עלול לפגוע בקצבה החודשית המגיעה לו ביום פרישה.",
    "הלקוח מבין ומצהיר כי הוא עלול לאבד את הוותק הביטוחי שלו בקופה וייתכן וייאלץ לפתוח קופה חדשה, דבר שיצריך תקופת אכשרה חדשה, הלקוח מאשר כי עם חתימה על הסכם זה האחריות בגין האמור הוא על דעתו ובאחריותו הבלעדית ואין לו כל טענה כלפי החברה.",
    "במקרה של פדיון כספים מקופת פנסיה ו/או ביטוח מנהלים ו/או קופת גמל, אשר בגינו יבוצע ניכוי מס ממרכיב התגמולים של עד 30% מס הכנסה ו/או ממרכיב הפיצויים של עד 47% מס הכנסה ו/או בפדיון של קרן השתלמות לא נזילה אשר בגינו יבוצע ניכוי מס ממרכיב התגמולים של עד 47% מס הכנסה, הלקוח מתחייב כי הוא מבין את המשמעות וכי הוא נושא במלוא האחריות וכי אין לו ולא תהיה לו כל תלונה ו/או תביעה ו/או דרישה מצידו כלפי החברה בנוגע לשיעור המס ששילם ככל ויידרש לכך. ",
    "מובא לידיעת הלקוח כי בהמשך לאמור, ייתכן ויהיה זכאי להחזר מס בסוף שנת המס בעקבות הפדיון, מובהר כי עניין זה אינו באחריותה של החברה.",
    "תשלום עמלת הגשת הבקשה לפדיון ו/או הלוואה ישולם על ידי הלקוח לחברה ישירות.",
    "הלקוח מתחייב לשתף פעולה עם החברה ולדווח לנציגיה בזמן אמת על קבלת הכספים לחשבונו, במידה והלקוח לא ידווח בזמן הדבר עלול להביא את החברה לפעול כנגדו כך שהסכומים המחויבים יגררו חיובים נוספים לרבות ריבית פיגורים, הוצאות משפטיות והליכי הוצאה לפועל.",
    "הלקוח מבין ומאשר שאין לראות בכל האמור כיעוץ פנסיוני או המלצה והוא בחר באופן עצמאי ובלעדי לבצע כל פעולה מול החברה. ",
    " הלקוח מתחייב לשלם את עמלת הגשת הבקשה לפדיון ו/או הלוואה )עלות הטיפול( בהתאם לחתום בהסכם זה ולא יאוחר מ46 שעות לאחר סיום התהליך מולו. "
};

                    // מספור סעיפים
                    int clauseNumber = 1;
                    foreach (string clause in clauses)
                    {
                        // יצירת פסקה עם רווחים של 15 בין השורות
                        Paragraph paragraph = new Paragraph($"{clauseNumber}. {clause}", font);
                        paragraph.SetLeading(lineSpacing, 1f); // ריווח של 15 פיקסלים בין השורות בתוך סעיף

                        PdfPCell cell14 = new PdfPCell();
                        cell14.Border = Rectangle.NO_BORDER;
                        cell14.AddElement(paragraph);
                        cell14.PaddingBottom = lineSpacing;

                        table14.AddCell(cell14);

                        clauseNumber++;
                    }

                    // הוספת הטבלה למסמך
                    document.Add(table14);

                    //PdfPTable T = new PdfPTable(4);
                    ////TDetailsBank.TotalWidth = 30f;
                    //T.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    //T.SpacingBefore = 8;
                    //T.SpacingAfter = 8;
                    ////T.DefaultCell.FixedHeight = 15f;
                    //T.DefaultCell.BorderWidth = 0;
                    //T.DefaultCell.VerticalAlignment = Element.ALIGN_LEFT;



                    ////SqlCommand cmdInsurance = new SqlCommand(@" select InsuredSignature,CONVERT(varchar, DateSignature, 104) AS DateSignature
                    ////                                         from Insurance  
                    ////                    where Insurance.ID=@IDInsurance");
                    ////cmdInsurance.Parameters.AddWithValue("@IDInsurance", Request.QueryString["InsuranceID1"]);
                    ////DataTable dtInsurance = DbProvider.GetDataTable(cmdInsurance);
                    ////string strpath = String.Format("{0}/Signature/{1}", ConfigurationManager.AppSettings["MapPath"], dtInsurance.Rows[0]["InsuredSignature"].ToString());
                    ////var image = iTextSharp.text.Image.GetInstance(strpath);
                    ////image.ScaleToFit(50f, 50f);  
                    ////var imageCell2 = (new PdfPCell(image) { Border = 0 });
                    ////T.AddCell(new Phrase("      תאריך:" + dtInsurance.Rows[0]["DateSignature"].ToString(), font));
                    //T.AddCell(new Phrase("      תאריך:" + "עעהעהע", font));

                    //T.AddCell(new Phrase(" ", font));
                    //T.AddCell(new PdfPCell(new Phrase("חתימה:", font)) { Border = 0 });
                    ////T.AddCell(imageCell2);
                    //T.AddCell("");

                    //document.Add(T);

                    document.NewPage();

                    // יצירת כותרות במסמך
                    PdfPTable headerTable = new PdfPTable(1);
                    headerTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    headerTable.HorizontalAlignment = Element.ALIGN_LEFT;
                    headerTable.WidthPercentage = 100;

                    // הוספת כותרות
                    headerTable.AddCell(CreateCell("לכל מאן דבעי,", font, 30, Element.ALIGN_LEFT));
                    headerTable.AddCell(CreateCell("הרשאה חד פעמית למסירת/ייעוץ פנסיוני לקבלת מידע (נספח א)", font, 10, Element.ALIGN_LEFT));
                    headerTable.AddCell(CreateCell("מייפה הכוח (הלקוח):", font, 10, Element.ALIGN_LEFT));
                    document.Add(headerTable);

                    // יצירת טבלת פרטים
                    PdfPTable detailsTable33 = new PdfPTable(4);
                    detailsTable33.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTable33.WidthPercentage = 100;
                    detailsTable33.SpacingAfter = 10;
                    detailsTable33.SetWidths(new float[] { 42, 10f, 43.5f, 4.5f });

                    // הוספת שורת שם ומספר זיהוי
                    AddLabelAndLine(detailsTable33, "שם:", font, dt.Rows[0]["FirstName"].ToString()+" " + dt.Rows[0]["LastName"].ToString());
                    AddLabelAndLine(detailsTable33, "מספר זיהוי:", font, dt.Rows[0]["Tz"].ToString());
                    document.Add(detailsTable33);

                    // יצירת טבלת כתובת
                    PdfPTable detailsTablAddress = new PdfPTable(2);
                    detailsTablAddress.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTablAddress.WidthPercentage = 100;
                    detailsTablAddress.SpacingAfter = 10;
                    float[] widthsAaddress = new float[] { 83f, 6f };
                    detailsTablAddress.SetWidths(widthsAaddress);

                    PdfPCell addressCell = new PdfPCell(new Phrase("כתובת:", font));
                    addressCell.BorderWidth = 0;
                    addressCell.HorizontalAlignment = Element.ALIGN_LEFT;

                    // תא שני - ריק עם קו
                    PdfPCell emptyCellAddress = new PdfPCell(new Phrase(dt.Rows[0]["Address"].ToString(), font));
                    emptyCellAddress.BorderWidth = 0;
                    emptyCellAddress.BorderWidthBottom = 1;

                    PdfPCell emptyCellAddress2 = new PdfPCell(new Phrase(""));
                    emptyCellAddress2.BorderWidth = 0;

                    // הוספת התאים לטבלה
                    detailsTablAddress.AddCell(addressCell);
                    detailsTablAddress.AddCell(emptyCellAddress);
                    detailsTablAddress.AddCell(emptyCellAddress2);
                    document.Add(detailsTablAddress);

                    // טבלה למיופה הכוח
                    PdfPTable T3 = new PdfPTable(1);
                    T3.DefaultCell.BorderWidth = 0;
                    T3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    T3.WidthPercentage = 100;
                    T3.SpacingAfter = 10;
                    T3.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    T3.AddCell(new Phrase("מיופה הכוח (סוכן/יועץ פנסיוני, במקרה של סוכן/יועץ פנסיוני שהוא תאגיד מיופה הכוח הינו התאגיד):", font));
                    document.Add(T3);

                    // יצירת טבלת פרטי מיופה הכוח
                    PdfPTable detailsTable12 = new PdfPTable(4);
                    detailsTable12.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTable12.WidthPercentage = 100;
                    detailsTable12.SpacingAfter = 10;
                    detailsTable12.SetWidths(new float[] { 41.5f, 9.5f, 35f, 15f });

                    // הוספת שורת שם ומספר זיהוי
                    AddLabelAndLine(detailsTable12, "שם (יחיד/תאגיד):", font, Text3.Value);
                    AddLabelAndLine(detailsTable12, "רישיון מס:'", font,Text2.Value);
                    document.Add(detailsTable12);

                    PdfPTable table6 = new PdfPTable(1);
                    table6.WidthPercentage = 100;
                    table6.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // הגדרת כיוון ריצה לטבלה
                    table6.HorizontalAlignment = Element.ALIGN_LEFT;
                    table6.SpacingAfter = 10;

                    // יצירת פסקה בעברית עם טקסט רגיל ומודגש
                    Paragraph paragraph6 = new Paragraph();
                    paragraph6.Add(new Chunk("אשר הינו: ", font));
                    paragraph6.Add(new Chunk("1) יועץ פנסיוני ;", boldFont));
                    //paragraph6.Add(new Chunk(" ; ", font));
                    paragraph6.Add(new Chunk("  2) סוכן ביטוח פנסיוני ;", font));
                    //paragraph6.Add(new Chunk(" ; ", font));
                    paragraph6.Add(new Chunk("  3) סוכן שיווק פנסיוני", font));

                    // עטיפת הפסקה בתא בטבלה כדי לשלוט על כיוון הטקסט
                    PdfPCell cell6 = new PdfPCell(paragraph6);
                    cell6.Border = PdfPCell.NO_BORDER;
                    cell6.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // כיוון ריצה בעברית
                    cell6.HorizontalAlignment = Element.ALIGN_LEFT; // יישור לימין

                    // הוספת התא לטבלה
                    table6.AddCell(cell6);

                    // הוספת הטבלה למסמך
                    document.Add(table6);

                    // טבלה למיופה הכוח
                    PdfPTable T5 = new PdfPTable(1);
                    T5.DefaultCell.BorderWidth = 0;
                    T5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    T5.WidthPercentage = 100;
                    T5.SpacingAfter = 10;
                    T5.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    T5.AddCell(new Phrase("סמן את האפשרות המתאימה.", font));
                    document.Add(T5);

                    // יצירת טבלה להוספת טלפון ומייל
                    PdfPTable detailsTable3 = new PdfPTable(4);
                    detailsTable3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    detailsTable3.WidthPercentage = 100;
                    detailsTable3.SpacingAfter = 10;
                    detailsTable3.SetWidths(new float[] { 45, 5f, 44f, 6f });

                    // הוספת שורת טלפון ומייל
                    AddLabelAndLine(detailsTable3, "טלפון:", font, dt.Rows[0]["Phone1"].ToString());
                    AddLabelAndLine(detailsTable3, "מייל:", font, dt.Rows[0]["Email"].ToString());
                    document.Add(detailsTable3);

                    // יצירת טבלת הצהרה
                    PdfPTable disclaimerTable = new PdfPTable(1);
                    disclaimerTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL; // הגדרת כיוון הטבלה
                    disclaimerTable.WidthPercentage = 100;
                    disclaimerTable.SpacingBefore = 20;
                    disclaimerTable.SpacingAfter = 20;

                    // הטקסט המלא
                    string disclaimerText = "אני, הח\"מ, מייפה את כוחו של הסוכן/ היועץ הפנסיוני, או מי מטעמו, לפנות בשמי, לכל גוף מוסדי לשם " +
                                            "קבלת מידע אודות מוצרים פנסיוניים או שיווק פנסיוני או שיווק פנסיוני בגינם תכניות ביטוח לשם מתן ייעוץ פנסיוני או שיווק פנסיוני באופן חד-" +
                                            "פעמי או לשם מתן סיוע בהליכי שיווק פנסיוני לראשונה, כהכנה למתן ייעוץ פנסיוני או שיווק פנסיוני " +
                                            "מתמשך. העברת מידע אודותיי, כאמור לעיל, יכול שתיעשה באמצעות מערכת סליקה פנסיונית. " +
                                            "ייפוי כוח זה מתייחס לכל המוצרים הפנסיוניים המנוהלים עבורי בגוף מוסדי כלשהו נכון למועד חתימת " +
                                            "הרשאה זו, מלבד המוצרים המנויים בטופס המצורף להרשאה זו (עבור כל גוף מוסדי בנפרד). " +
                                            "שים לב! אם לא יצוינו מוצרים פנסיוניים בטופס המצ\"ב, ההרשאה תתייחס לכל המוצרים הפנסיוניים " +
                                            "ותכניות הביטוח שברשותך.";

                    // יצירת תא עם טקסט
                    PdfPCell disclaimerCell = CreateCell("", font, 10);

                    // יצירת פסקה עם רווחים בין השורות
                    Paragraph disclaimerParagraph = CreateParagraphWithLineSpacing(disclaimerText, font);

                    // הוספת הפסקה לתא
                    disclaimerCell.AddElement(disclaimerParagraph);
                    disclaimerTable.AddCell(disclaimerCell);

                    // הוספת טבלה למסמך
                    document.Add(disclaimerTable);

                    // יצירת טבלת טקסט עם הודעת סיום
                    PdfPTable textTable = new PdfPTable(1);
                    textTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    textTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    textTable.WidthPercentage = 100;
                    textTable.SpacingAfter = 5;

                    // הוספת הכותרות
                    textTable.AddCell(CreateCell("***הרשאה זו תעמוד בתוקפה במשך 3 חודשים מיום חתימתה***", boldFont, 5, Element.ALIGN_CENTER));
                    textTable.AddCell(CreateCell("ולראיה באתי על החתום:", boldFont, 30, Element.ALIGN_CENTER));
                    document.Add(textTable);
                    document.Close();
                }

            }
            catch (Exception ex) { }
            return FileName1;
        }
        void AddLabelAndLine(PdfPTable table, string label, Font font, string value)
        {

            PdfPCell labelCell = CreateCell(label, font, 0, Element.ALIGN_LEFT);
            PdfPCell lineCell = new PdfPCell(new Phrase(value, font));
            lineCell.BorderWidth = 0;
            lineCell.BorderWidthBottom = 1;

            table.AddCell(labelCell);
            table.AddCell(lineCell);
        }

        private PdfPCell CreateCell(string text, Font font, int paddingBottom = 0, int horizontalAlignment = Element.ALIGN_RIGHT, int verticalAlignment = Element.ALIGN_MIDDLE, bool addBorder = false)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                Border = addBorder ? Rectangle.BOX : Rectangle.NO_BORDER,
                PaddingBottom = paddingBottom,
                HorizontalAlignment = horizontalAlignment,
                VerticalAlignment = verticalAlignment
            };
            return cell;
        }

        // פונקציה ליצירת פסקה עם רווחים בין השורות
        private Paragraph CreateParagraphWithLineSpacing(string text, Font font)
        {
            Paragraph paragraph = new Paragraph(text, font);
            paragraph.Alignment = Element.ALIGN_JUSTIFIED; // יישור הטקסט
            paragraph.SetLeading(0f, 1.5f); // מרווח בין השורות (מרווח של 1.5 בין שורה לשורה)
            paragraph.IndentationLeft = 10; // הגדרת רווח בצד שמאל
            paragraph.IndentationRight = 10; // הגדרת רווח בצד ימין
            return paragraph;
        }
        void AddTableRowWithLines(PdfPTable table, string labelRight, string valueRight, string labelLeft, string valueLeft, Font labelFont)
        {
            PdfPCell cellRightLabel = new PdfPCell(new Phrase(labelRight, labelFont));
            cellRightLabel.Border = PdfPCell.NO_BORDER;
            cellRightLabel.HorizontalAlignment = Element.ALIGN_LEFT;
            cellRightLabel.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            PdfPCell cellRightValue = new PdfPCell(new Phrase(valueRight, labelFont));
            cellRightValue.Border = PdfPCell.BOTTOM_BORDER;
            cellRightValue.PaddingBottom = 2;
            cellRightValue.VerticalAlignment = Element.ALIGN_BOTTOM;
            cellRightValue.FixedHeight = 15f;
            cellRightValue.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cellRightValue.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell cellLeftLabel = new PdfPCell(new Phrase(labelLeft, labelFont));
            cellLeftLabel.Border = PdfPCell.NO_BORDER;
            cellLeftLabel.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellLeftLabel.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            PdfPCell cellLeftValue = new PdfPCell(new Phrase(valueLeft, labelFont));
            cellLeftValue.Border = PdfPCell.BOTTOM_BORDER;
            cellLeftValue.PaddingBottom = 2;
            cellLeftValue.VerticalAlignment = Element.ALIGN_BOTTOM;
            cellLeftValue.FixedHeight = 15f;
            cellLeftValue.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cellLeftValue.HorizontalAlignment = Element.ALIGN_LEFT;

            table.AddCell(cellRightLabel);
            table.AddCell(cellRightValue);
            table.AddCell(cellLeftLabel);
            table.AddCell(cellLeftValue);
        }


    }
}