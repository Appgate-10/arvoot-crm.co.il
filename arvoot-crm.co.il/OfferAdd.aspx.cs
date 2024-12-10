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
                SelectOfferType.Items.Insert(0, new ListItem("בחר", ""));


                //SqlCommand cmdTurnOffer = new SqlCommand("SELECT * FROM TurnOffer");
                //DataSet dsTurnOffer = DbProvider.GetDataSet(cmdTurnOffer);
                //SelectTurnOffer.DataSource = dsTurnOffer;
                //SelectTurnOffer.DataTextField = "Name";
                //SelectTurnOffer.DataValueField = "ID";
                //SelectTurnOffer.DataBind();
                //SelectTurnOffer.Items.Insert(0, new ListItem("בחר", ""));
                //loadUsers(1);
                loadData();
            }
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
                            var fileDetail = new FileDetail
                            {
                                FileName = postedFile.FileName,
                                FileSize = postedFile.ContentLength,
                                PostedFile = postedFile,

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

            string sql = @"select Lead.Tz, Lead.FirstName+' '+Lead.LastName as FullName, A.FullName as FullNameAgent, parent2.CompanyName from Lead
                           inner join ArvootManagers A on Lead.AgentID=A.ID 
                           left join ArvootManagers parent on parent.ID = A.ParentID
                           left join ArvootManagers parent2 on parent2.ID = parent.ParentID
                           where Lead.ID = @LeadID ";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@LeadID", Request.QueryString["ContactID"]);

            DataTable dt = DbProvider.GetDataTable(cmd);
            if (dt.Rows.Count > 0)
            {
                FullName.InnerText = dt.Rows[0]["FullName"].ToString();
             //FullNameAgent.InnerText = dt.Rows[0]["FullNameAgent"].ToString();
                lblOwner.InnerText = dt.Rows[0]["FullNameAgent"].ToString();
                lblAgency.InnerText = dt.Rows[0]["CompanyName"].ToString();
                Tz.InnerText = dt.Rows[0]["Tz"].ToString();
                EffectiveDate.InnerText = DateTime.Now.ToString("dd.MM.yyyy");
            }
        }

        protected void ButtonDiv_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

        }

        public long funcSave(object sender, EventArgs e)
        {

            int ErrorCount = 0;
            FormError_label.Visible = false;
            FormErrorBottom_label.Visible = false;

            if (string.IsNullOrEmpty(NameOffer.Value))
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין שם ההצעה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין שם ההצעה";
                return 0;
            }
            if (SelectSourceLoanOrInsurance.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מקור ההלוואה/ביטוח";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מקור ההלוואה/ביטוח";
                return 0;
            }
            if (SelectOfferType.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין סוג הצעה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין סוג הצעה";
                return 0;
            }

           
            if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) == 5) {
                if (SelectStatusOffer.SelectedIndex== 11 && string.IsNullOrEmpty(ReturnDateToCustomer.Value))
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין מועד חזרה ללקוח";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין מועד חזרה ללקוח";
                    return 0;
                }
                if (SelectStatusOffer.SelectedIndex == 4 && string.IsNullOrEmpty(DateSentToInsuranceCompany.Value))
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין תאריך שליחה לחברת הביטוח";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין תאריך שליחה לחברת הביטוח";
                    return 0;
                }
                if ((SelectStatusOffer.SelectedIndex == 10 ||SelectStatusOffer.SelectedIndex == 6 || SelectStatusOffer.SelectedIndex == 5) && string.IsNullOrEmpty(ReasonLackSuccess.Value))
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין סיבה לחוסר הצלחה";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין סיבה לחוסר הצלחה";
                    return 0;
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
                SqlCommand cmdInsert = new SqlCommand(@"insert into Offer (LeadID,SourceLoanOrInsuranceID,OfferTypeID,ReasonLackSuccess,ReturnDateToCustomer,DateSentToInsuranceCompany,Note,StatusOfferID,NameOffer) output INSERTED.ID  
                                                 values(@LeadID,@SourceLoanOrInsuranceID,@OfferTypeID,@ReasonLackSuccess,@ReturnDateToCustomer,@DateSentToInsuranceCompany,@Note,@StatusOfferID,@NameOffer)");

                //,TurnOfferID=@TurnOfferID
                cmdInsert.Parameters.AddWithValue("@LeadID", Request.QueryString["ContactID"]);
                cmdInsert.Parameters.AddWithValue("@SourceLoanOrInsuranceID", SelectSourceLoanOrInsurance.Value);
                cmdInsert.Parameters.AddWithValue("@OfferTypeID", SelectOfferType.Value);
                cmdInsert.Parameters.AddWithValue("@ReasonLackSuccess", ReasonLackSuccess.Value);
                cmdInsert.Parameters.AddWithValue("@ReturnDateToCustomer", string.IsNullOrEmpty(ReturnDateToCustomer.Value) ? (object)DBNull.Value : DateTime.Parse(ReturnDateToCustomer.Value));
                cmdInsert.Parameters.AddWithValue("@DateSentToInsuranceCompany", string.IsNullOrEmpty(DateSentToInsuranceCompany.Value) ? (object)DBNull.Value : DateTime.Parse(DateSentToInsuranceCompany.Value));
                cmdInsert.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note.Value) ? (object)DBNull.Value : Note.Value);
                cmdInsert.Parameters.AddWithValue("@NameOffer", string.IsNullOrEmpty(NameOffer.Value) ? (object)DBNull.Value : NameOffer.Value);
                cmdInsert.Parameters.AddWithValue("@StatusOfferID", SelectStatusOffer.Value);
                //cmdInsert.Parameters.AddWithValue("@TurnOfferID", SelectTurnOffer.Value);

                long offerID = DbProvider.GetOneParamValueLong(cmdInsert);
                List<FileDetail> myFile = (List<FileDetail>)Session["UploadedFiles"];
                if (offerID > 0 && myFile != null)
                {
                    for (int i = 0; i < myFile.Count; i++) {
                        try
                        {
                            string FilePath1 = String.Format("{0}/OfferDocuments/", ConfigurationManager.AppSettings["MapPath"]);                          
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

                    if (SelectStatusOffer.Value == "9")
                    {
                        string sqlBranchManager = @"SELECT a.ParentID from Offer
                                                    INNER JOIN [Lead] l On l.ID = Offer.LeadID
                                                    INNER JOIN ArvootManagers a on a.ID = l.AgentID WHERE Offer.ID = @OfferID";
                        SqlCommand cmdBranchManager = new SqlCommand(sqlBranchManager);
                        cmdBranchManager.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                        string parentID = DbProvider.GetOneParamValueString(cmdBranchManager);

                        if (parentID != null)
                        {
                            string sqlAlert = "INSERT INTO Alerts (AgentID, Text, CreationDate, DisplayDate) Values (@AgentID, @Text, GETDATE(), GETDATE())";
                            SqlCommand cmdAlert = new SqlCommand(sqlAlert);
                            cmdAlert.Parameters.AddWithValue("@AgentID", parentID);
                            cmdAlert.Parameters.AddWithValue("@Text", "סיום טיפול בהצעה: " + NameOffer.Value + " על ידי: " + lblOwner.InnerText);
                            DbProvider.ExecuteCommand(cmdAlert);
                        }

                    }

                    return offerID;
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



            return 0;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {


            long success = funcSave(sender, e);
            if (success == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                Response.Redirect("OfferEdit.aspx?OfferID=" + success.ToString());
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