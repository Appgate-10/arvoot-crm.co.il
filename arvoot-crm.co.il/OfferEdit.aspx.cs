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



        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

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
                FormError_lable.Text = "* התרחשה שגיאה";
                FormError_lable.Visible = true;
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
                Response.Redirect("DownloadFile.ashx?fileName=" + parameters[0]);
            else ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('עליך לשמור את הקובץ לפני הורדה');", true);



        }
        public void loadData()
        {
           

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

            string sqlOffer = @"select LeadID, CONVERT(varchar,Offer.CreateDate, 104) as CreateDate, 
                                DATEDIFF(DAY,Offer.CreateDate,getdate()) as sla, SourceLoanOrInsuranceID, OfferTypeID,TurnOfferID,
                                ReasonLackSuccess,CONVERT(varchar,Offer.ReturnDateToCustomer, 104) ReturnDateToCustomer,
                                Note,DateSentToInsuranceCompany, StatusOfferID from Offer where ID = @OfferID";
            SqlCommand cmdOffer = new SqlCommand(sqlOffer);
            cmdOffer.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
            DataTable dtOffer = DbProvider.GetDataTable(cmdOffer);
            if (dtOffer.Rows.Count > 0)
            {
                ContactID.Value = dtOffer.Rows[0]["LeadID"].ToString(); 
                EffectiveDate.InnerText = dtOffer.Rows[0]["CreateDate"].ToString();
                sla.InnerText = dtOffer.Rows[0]["sla"].ToString();
                    SelectSourceLoanOrInsurance.Value = dtOffer.Rows[0]["SourceLoanOrInsuranceID"].ToString();
                SelectOfferType.Value = dtOffer.Rows[0]["OfferTypeID"].ToString();
                SelectTurnOffer.Value = dtOffer.Rows[0]["TurnOfferID"].ToString();
                ReasonLackSuccess.Value = dtOffer.Rows[0]["ReasonLackSuccess"].ToString();
                ReturnDateToCustomer.Value = Convert.ToDateTime(dtOffer.Rows[0]["ReturnDateToCustomer"]).ToString("yyyy-MM-dd");
                Note.Value = dtOffer.Rows[0]["Note"].ToString();
                DateSentToInsuranceCompany.Value = Convert.ToDateTime(dtOffer.Rows[0]["DateSentToInsuranceCompany"]).ToString("yyyy-MM-dd"); 
                SelectStatusOffer.Value = dtOffer.Rows[0]["StatusOfferID"].ToString();

                string sqlOfferDocument = @"select * from OfferDocuments where OfferID = @OfferID";
                SqlCommand cmdOfferDocument = new SqlCommand(sqlOfferDocument);
                cmdOfferDocument.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                dtOfferDocument = DbProvider.GetDataTable(cmdOfferDocument);
                Repeater1.DataSource = dtOfferDocument;
                Repeater1.DataBind();
                
                string sqlServiceRequest = @"select s.ID, Invoice,Sum,CONVERT(varchar, CreateDate, 104)  CreateDate ,Balance, p.purpose as PurposeName from ServiceRequest s left join ServiceRequestPurpose p on s.PurposeID = p.ID where s.OfferID = @OfferID";
                SqlCommand cmdServiceRequest = new SqlCommand(sqlServiceRequest);
                cmdServiceRequest.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                dtServiceRequest = DbProvider.GetDataTable(cmdServiceRequest);
                Repeater2.DataSource = dtServiceRequest;
                Repeater2.DataBind();
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

            foreach (var file in (List<FileDetail>)Session["UploadedFiles"])
            {
                DataRow row = dtUploadedFiles.NewRow();
                row["FileName"] = file.FileName;
                row["ID"] = 0;
                dtUploadedFiles.Rows.Add(row);
            }

            // Combine both DataTables
            string sqlOfferDocument = @"select ID,FileName from OfferDocuments where OfferID = @OfferID";
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
                        if (uploadedFiles == null) uploadedFiles = new List<FileDetail>();
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
                    string filePath = String.Format("{0}/OfferDocuments/{1}", ConfigurationManager.AppSettings["MapPath1"], lblFilePath.Text) ;
                    
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
                    string filePath = String.Format("{0}/OfferDocuments/{1}", ConfigurationManager.AppSettings["MapPath1"], lblFilePath.Text);

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
                SqlCommand cmdInsert = new SqlCommand(@"update Offer set SourceLoanOrInsuranceID=@SourceLoanOrInsuranceID,OfferTypeID=@OfferTypeID,ReasonLackSuccess=@ReasonLackSuccess,
                                                        ReturnDateToCustomer=@ReturnDateToCustomer,DateSentToInsuranceCompany=@DateSentToInsuranceCompany,Note=@Note,StatusOfferID=@StatusOfferID,TurnOfferID=@TurnOfferID 
                                                        where ID = @OfferID");

                cmdInsert.Parameters.AddWithValue("@SourceLoanOrInsuranceID", SelectSourceLoanOrInsurance.Value);
                cmdInsert.Parameters.AddWithValue("@OfferTypeID", SelectOfferType.Value);
                cmdInsert.Parameters.AddWithValue("@ReasonLackSuccess", ReasonLackSuccess.Value);
                cmdInsert.Parameters.AddWithValue("@ReturnDateToCustomer", DateTime.Parse(ReturnDateToCustomer.Value));
                cmdInsert.Parameters.AddWithValue("@DateSentToInsuranceCompany", DateTime.Parse(DateSentToInsuranceCompany.Value));
                cmdInsert.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note.Value) ? (object)DBNull.Value : Note.Value);
                cmdInsert.Parameters.AddWithValue("@StatusOfferID", SelectStatusOffer.Value);
                cmdInsert.Parameters.AddWithValue("@TurnOfferID", SelectTurnOffer.Value);
                cmdInsert.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);

                if (DbProvider.ExecuteCommand(cmdInsert) > 0)
                {
                    List<FileDetail> myFile = (List<FileDetail>)Session["UploadedFiles"];
                    if (myFile != null)
                    {
                        for (int i = 0; i < myFile.Count; i++)
                        {
                            try
                            {
                                string FilePath1 = String.Format("{0}/OfferDocuments/", ConfigurationManager.AppSettings["MapPath1"]);
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

                        return true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                        FormError_lable.Text = "* התרחשה שגיאה";
                        FormError_lable.Visible = true;
                    }
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
                Response.Redirect("Contact.aspx?ContactID=" + ContactID.Value);
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
            public int FileSize { get; set; }
            public HttpPostedFile PostedFile { get; set; }
        }
    }
}