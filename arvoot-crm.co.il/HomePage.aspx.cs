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
using System.Globalization;

namespace ControlPanel
{
    public partial class _homePage : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        string FileName1;
        public string ListPageUrl = "HomePage.aspx";
        private List<DateTime> _datesWithTasks;


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ImageFile_1_display.Attributes.Add("onclick", "document.getElementById('" + ImageFile_1_FileUpload.ClientID + "').click();");

            if (!Page.IsPostBack)
            {

                if (HttpContext.Current.Session["AgentID"] == null)
                {
                    HttpContext.Current.Response.Redirect("SignIn.aspx");
                }
                else
                {
                    //string str = "select * from Lead where IsContact = 1";
                    //SqlCommand cmd2 = new SqlCommand(str);
                    //DataTable dt2 = DbProvider.GetDataTable(cmd2);
                    //Repeater2.DataSource = dt2;
                    //Repeater2.DataBind();

                    //Gila
                    string strTasks = @"select Tasks.ID, FORMAT(Tasks.PerformDate, 'dd.MM.yy') as dateTask , 
                                    CONVERT(varchar(5), Tasks.PerformDate, 108) as timeTask, Tasks.Text, ts.Status ,isnull(Tasks.OfferID,0) as OfferID , isnull(Tasks.LeadID ,0) as LeadID
                                    from Tasks left join Offer on Offer.ID = Tasks.OfferID  left join Lead on Lead.ID = Offer.LeadID 
                                    left join TaskStatuses ts on ts.ID = Tasks.Status left join Lead lead2 on Tasks.LeadID = lead2.ID
                                    where isnull(isnull(Offer.OperatorID, Lead.AgentID),lead2.AgentID) = @AgentID
                                    AND PerformDate = CAST(@selectedDate AS DATE)";
                    SqlCommand cmdTasks = new SqlCommand(strTasks);

                    //Gila
                    cmdTasks.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
                    cmdTasks.Parameters.AddWithValue("@selectedDate", DateTime.Today);
                    DataTable dtTasks = DbProvider.GetDataTable(cmdTasks);
                    Repeater3.DataSource = dtTasks;
                    Repeater3.DataBind();

                    string sqlAlerts = @"SELECT TOP (10) Text, CONVERT(varchar, CreationDate, 104) AS CreationDate FROM Alerts WHERE AgentID = @AgentID ORDER BY ID desc";
                    SqlCommand cmdAlerts = new SqlCommand(sqlAlerts);
                    cmdAlerts.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
                    DataTable dtAlerts = DbProvider.GetDataTable(cmdAlerts);
                    Repeater2.DataSource = dtAlerts;
                    Repeater2.DataBind();


                    //Gila
                    TasksCalendar.SelectedDate = DateTime.Today;
                    LoadTaskDates();
                }

                if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) < 4)
                {
                    CreateEmployee.Visible = true;
                }

                switch (HttpContext.Current.Session["AgentLevel"].ToString())
                {
                    case "1":
                        {
                            ManagementPermission.Text = "מנהל חברה";
                            AgentPermission.Visible = false;
                            SupervisorPermission.Visible = false;
                            Address.Visible = true;
                            NameOrAddress.Visible = true;
                            NameOrAddress.InnerText = "שם החברה";
                            Address.Attributes["placeholder"] = "שם החברה";
                            numbersAgentTitle.Visible = true;
                            numbersAgent.Visible = true;
                            Div1.Style.Add("height", "100%");
                            Div1.Style.Add("overflow-x", "scroll");
                            SqlCommand sql = new SqlCommand("select * from SourceLoanOrInsurance where ID<7 or ID =11");
                            DataTable dt = DbProvider.GetDataTable(sql);
                            company1.InnerText = dt.Rows[0]["Text"].ToString();
                            CompanyID1.Value = dt.Rows[0]["ID"].ToString(); 
                            company2.InnerText = dt.Rows[1]["Text"].ToString();
                            CompanyID2.Value = dt.Rows[1]["ID"].ToString(); 
                            company3.InnerText = dt.Rows[2]["Text"].ToString();
                            CompanyID3.Value = dt.Rows[2]["ID"].ToString();
                            company4.InnerText = dt.Rows[3]["Text"].ToString();
                            CompanyID4.Value = dt.Rows[3]["ID"].ToString();
                            company5.InnerText = dt.Rows[4]["Text"].ToString();
                            CompanyID5.Value = dt.Rows[4]["ID"].ToString();
                            company6.InnerText = dt.Rows[5]["Text"].ToString();
                            CompanyID6.Value = dt.Rows[5]["ID"].ToString(); 
                            company7.InnerText = dt.Rows[6]["Text"].ToString();
                            CompanyID7.Value = dt.Rows[6]["ID"].ToString();
                     
                            break;
                        }
                    case "2":
                        {
                            ManagementPermission.Text = "מנהל סניף";
                            AgentPermission.Text = "מזכירה";
                           // AgentPermission.Visible = false;
                            SupervisorPermission.Visible = false;
                            Address.Visible = true;
                            NameOrAddress.Visible = true;                           
                            NameOrAddress.InnerText = "שם הסניף";
                            Address.Attributes["placeholder"] = "כתובת";
                            break;
                        }
                    case "3":
                        {
                            ManagementPermission.Text = "מנהלת תפעול";
                            AgentPermission.Visible = true;
                            SupervisorPermission.Visible = true;
                            Address.Visible = false;
                            NameOrAddress.Visible = false;
                            break;
                        }
                    //case "4":
                    //case "5":
                    //case "6":
                    //    {
                    //        ManagementPermission.Text = "מנהל חברה";
                    //        AgentPermission.Visible = false;
                    //        SupervisorPermission.Visible = false;
                    //        break;
                    //    }
                    default:
                        {
                            ManagementPermission.Text = "מנהל";
                            break;
                        }
                }
                ManagementPermission_Click(this, EventArgs.Empty);

            }
            else
            {
                if (_datesWithTasks == null)
                {
                    LoadTaskDates();
                }
            }

            //Gila
            loadGraf();

        }
        public bool funcSave(object sender, EventArgs e)
        {
            int ErrorCount = 0;

            ImageFile_1_lable_2.Visible = false;
            FormErrorAgent_lable.Visible = false;
            ImageFile_1_lable.Visible = false;

            if (string.IsNullOrWhiteSpace(Name.Value))
            {
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין שם מלא";
                ErrorCount++;
                return false;
            }
            if (Name.Value != "")
            {
                var fullNames = Name.Value.Split(' ');
                if (fullNames.Length >= 2)
                {
                    foreach (var str in fullNames)
                    {
                        if (str.Length < 2)
                        {
                            ErrorCount++;
                            FormErrorAgent_lable.Visible = true;
                            FormErrorAgent_lable.Text = "יש להזין שם מלא תקין";
                            return false;
                        }
                    }
                }
                else
                {
                    ErrorCount++;
                    FormErrorAgent_lable.Visible = true;
                    FormErrorAgent_lable.Text = "יש להזין שם מלא תקין";
                    return false;
                }
            }
            if (EmailA.Value == "")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין אימייל ";
                return false;
            }
            if (!EmailA.Value.Contains("@"))
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין אימייל תקין";
                return false;
            }
            if (Helpers.AgentEmailExist(EmailA.Value, -1) == "true")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "אימייל זה כבר קיים במערכת";
                return false;
            }
            if (Address.Visible == true && string.IsNullOrWhiteSpace(Address.Value))
            {
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין " + NameOrAddress.InnerText;
                ErrorCount++;
                return false;
            }
            if (Phone.Value == "" || Phone.Value.Length < 9)
            {
                ErrorCount++;
                FormErrorAgent_lable.Text = "יש להזין מספר טלפון  ";
                FormErrorAgent_lable.Visible = true;
                return false;
            }
            if (Helpers.AgentPhoneExist(Phone.Value, -1) == "true")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "מספר טלפון זה כבר קיים במערכת";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Tz.Value))
            {
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין ת.ז ";
                ErrorCount++;
                return false;
            }
            if (Tz.Value.Length != 9)
            {
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "  יש להזין ת.ז תקינה ";
                ErrorCount++;
                return false;
            }
            if (Helpers.AgentTzExist(Tz.Value, -1) == "true")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "ת.ז קיימת במערכת";
                return false;

            }
            if (PasswordAgent.Value == "")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין סיסמא ";
                return false;
            }
            if (PasswordAgent.Value.Length < 6)
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין סיסמא תקינה ";
                return false;
            }

            if (ErrorCount == 0)
            {

                //string sql = @"Insert INTO Agent (FullName,Tz,Email,Phone,ImageFile,Password,Level,Show)
                //                     values(@Name,@Tz,@Email,@Phone,@ImageFile,@Password,@Level,1)";

                string sql = @"INSERT INTO ArvootManagers(Email, Password, FullName, Type, CreateDate, Tz, Phone, ImageFile, ParentID, CompanyName, BranchName)  output INSERTED.ID
                                    VALUES(@Email, @Password, @FullName, @Type, GETDATE(), @Tz, @Phone, @ImageFile, @ParentID, @CompanyName, @BranchName)";

                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@Email", EmailA.Value);
                cmd.Parameters.AddWithValue("@Password", Md5.GetMd5Hash(Md5.CreateMd5Hash(), "Pass755" + PasswordAgent.Value));
                cmd.Parameters.AddWithValue("@FullName", Name.Value);
                cmd.Parameters.AddWithValue("@Tz", Tz.Value);
                cmd.Parameters.AddWithValue("@Phone", Phone.Value);

                try
                {
                    FileUpload fileU = (FileUpload)Session["imgFileUpload1"];
                    string ext = System.IO.Path.GetExtension(fileU.FileName).ToLower();
                    FileName1 = Md5.GetMd5Hash(Md5.CreateMd5Hash(), "1" + Helpers.CreateFileName(fileU.FileName)) + ext;
                    cmd.Parameters.AddWithValue("@ImageFile", FileName1);
                }
                catch (Exception) { cmd.Parameters.AddWithValue("@ImageFile", ""); }


                switch (HttpContext.Current.Session["AgentLevel"].ToString())
                {
                    case "1":
                        {
                            cmd.Parameters.AddWithValue("@ParentID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Type", 2);
                            cmd.Parameters.AddWithValue("@CompanyName", Address.Value);
                            cmd.Parameters.AddWithValue("@BranchName", DBNull.Value);

                            break;
                        }
                    case "2":
                        {
                            switch (HttpContext.Current.Session["Add_Level"].ToString())
                            {
                                case "1":
                                    {
                                        cmd.Parameters.AddWithValue("@Type", 3);
                                        cmd.Parameters.AddWithValue("@BranchName", Address.Value);

                                        break;
                                    }  
                                case "2":
                                    {
                                        cmd.Parameters.AddWithValue("@Type", 7);
                                        cmd.Parameters.AddWithValue("@BranchName", DBNull.Value);

                                        break;
                                    }
                            }
                            cmd.Parameters.AddWithValue("@ParentID", HttpContext.Current.Session["AgentID"]);
                        //    cmd.Parameters.AddWithValue("@Type", 3);
                            cmd.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                            break;
                        }
                    case "3":
                        {
                            cmd.Parameters.AddWithValue("@ParentID", HttpContext.Current.Session["AgentID"]);
                            cmd.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                            cmd.Parameters.AddWithValue("@BranchName", DBNull.Value);

                            switch (HttpContext.Current.Session["Add_Level"].ToString())
                            {
                                case "1":
                                    {
                                        cmd.Parameters.AddWithValue("@Type", 4);
                                        break;
                                    }
                                case "2":
                                    {
                                        cmd.Parameters.AddWithValue("@Type", 6);
                                        break;
                                    }
                                case "3":
                                    {
                                        cmd.Parameters.AddWithValue("@Type", 5);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                       
                }
                long CompanyMaanagerID = DbProvider.GetOneParamValueLong(cmd);
                if (CompanyMaanagerID > 0)
                {
                    if (Session["imgFileUpload1"] != null && ((FileUpload)Session["imgFileUpload1"]).HasFile)
                    {
                        //todo -לשמור בתקיה בשרת
                        string FilePath = String.Format("{0}/Agent/", ConfigurationManager.AppSettings["MapPath"]);
                        try { ((FileUpload)Session["imgFileUpload1"]).PostedFile.SaveAs(Path.Combine(FilePath, FileName1)); }
                        catch (Exception ex)
                        {

                        }


                    }
                    if (HttpContext.Current.Session["AgentLevel"].ToString().Equals("1"))
                    {
                        string sqlAgentNumbers = "insert into AgentNumbers (SourceId, AgentNumber, CompanyManagerId) values(@SourceId, @AgentNumber, @CompanyManagerId)";

                        if (!string.IsNullOrEmpty(AgentNumber1.Value))
                        {
                            SqlCommand cmdAgentNumbers = new SqlCommand(sqlAgentNumbers);
                            cmdAgentNumbers.Parameters.AddWithValue("@CompanyManagerId", CompanyMaanagerID);
                            cmdAgentNumbers.Parameters.AddWithValue("@SourceId", CompanyID1.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentNumber", AgentNumber1.Value);
                            DbProvider.ExecuteCommand(cmdAgentNumbers);
                        }
                        if (!string.IsNullOrEmpty(AgentNumber2.Value))
                        {
                            SqlCommand cmdAgentNumbers = new SqlCommand(sqlAgentNumbers);
                            cmdAgentNumbers.Parameters.AddWithValue("@CompanyManagerId", CompanyMaanagerID);
                            cmdAgentNumbers.Parameters.AddWithValue("@SourceId", CompanyID2.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentNumber", AgentNumber2.Value);
                            DbProvider.ExecuteCommand(cmdAgentNumbers);
                        }
                        if (!string.IsNullOrEmpty(AgentNumber3.Value))
                        {
                            SqlCommand cmdAgentNumbers = new SqlCommand(sqlAgentNumbers);
                            cmdAgentNumbers.Parameters.AddWithValue("@CompanyManagerId", CompanyMaanagerID);
                            cmdAgentNumbers.Parameters.AddWithValue("@SourceId", CompanyID3.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentNumber", AgentNumber3.Value);
                            DbProvider.ExecuteCommand(cmdAgentNumbers);
                        }
                        if (!string.IsNullOrEmpty(AgentNumber4.Value))
                        {
                            SqlCommand cmdAgentNumbers = new SqlCommand(sqlAgentNumbers);
                            cmdAgentNumbers.Parameters.AddWithValue("@CompanyManagerId", CompanyMaanagerID);
                            cmdAgentNumbers.Parameters.AddWithValue("@SourceId", CompanyID4.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentNumber", AgentNumber4.Value);
                            DbProvider.ExecuteCommand(cmdAgentNumbers);
                        } 
                        if (!string.IsNullOrEmpty(AgentNumber5.Value))
                        {
                            SqlCommand cmdAgentNumbers = new SqlCommand(sqlAgentNumbers);
                            cmdAgentNumbers.Parameters.AddWithValue("@CompanyManagerId", CompanyMaanagerID);
                            cmdAgentNumbers.Parameters.AddWithValue("@SourceId", CompanyID5.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentNumber", AgentNumber5.Value);
                            DbProvider.ExecuteCommand(cmdAgentNumbers);
                        }
                        if (!string.IsNullOrEmpty(AgentNumber6.Value))
                        {
                            SqlCommand cmdAgentNumbers = new SqlCommand(sqlAgentNumbers);
                            cmdAgentNumbers.Parameters.AddWithValue("@CompanyManagerId", CompanyMaanagerID);
                            cmdAgentNumbers.Parameters.AddWithValue("@SourceId", CompanyID6.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentNumber", AgentNumber6.Value);
                            DbProvider.ExecuteCommand(cmdAgentNumbers);
                        }
                    }
                    AddAgentPopUp.Visible = false;
                    return true;
                }
                else
                {
                    FormErrorAgent_lable.Text = "* התרחשה שגיאה";
                    FormErrorAgent_lable.Visible = true;
                }
            }
            return false;
        }

        protected void SaveNewAgent_Click(object sender, EventArgs e)
        {
            //  AddAgentPopUp.Visible = false;
            bool success = funcSave(sender, e);
            if (!success)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                //Response.Redirect(ListPageUrl);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                
            }
        }        
        
        protected void Mession_Delete(object sender, CommandEventArgs e)
        {
            string strDel = "delete from Tasks where ID = @ID ";
            SqlCommand cmdDel = new SqlCommand(strDel);
            cmdDel.Parameters.AddWithValue("@ID", e.CommandArgument);
            if (DbProvider.ExecuteCommand(cmdDel) > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                Response.Redirect(ListPageUrl);
            }
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
                string sql = "Update [Tasks] set Text = @Text ,Status = @Status ,PerformDate = @PerformDate where ID = @ID";

                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@Text", TextTask.Value);
                cmd.Parameters.AddWithValue("@Status", SelectStatusTask.Value);
                cmd.Parameters.AddWithValue("@PerformDate", Date.Value);
                cmd.Parameters.AddWithValue("@ID", ID.Value);

                if (DbProvider.ExecuteCommand(cmd) > 0)
                {
                    return true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
           
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
                Response.Redirect(ListPageUrl);
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
                TextTask.InnerText = dtTasks.Rows[0]["Text"].ToString();
              //  DateTime date = DateTime.ParseExact(dtTasks.Rows[0]["PerformDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
              DateTime dateTime = Convert.ToDateTime(dtTasks.Rows[0]["PerformDate"]);
                Date.Value = dateTime.ToString("yyyy-MM-dd");
                ID.Value = dtTasks.Rows[0]["ID"].ToString();
                SelectStatusTask.Value = dtTasks.Rows[0]["Status"].ToString();
                TaskDiv.Visible = true;
                UpdatePanelPopUps.Update();
            }
        }
        protected void CloseTaskPopUp_Click(object sender, EventArgs e)
        {
            TaskDiv.Visible = false;
        }

        protected void AgentPermission_Click(object sender, EventArgs e)
        {
            AgentPermission.Attributes.Add("class", "PermissionsChoose");
            ManagementPermission.Attributes.Add("class", "Permissions");
            SupervisorPermission.Attributes.Add("class", "Permissions");
            HttpContext.Current.Session["Add_Level"] = 2;

            if (HttpContext.Current.Session["AgentLevel"].ToString() == "2")
            {
                NameOrAddress.Visible = false;
                Address.Visible = false;
            }
        }

        protected void SupervisorPermission_Click(object sender, EventArgs e)
        {
            SupervisorPermission.Attributes.Add("class", "PermissionsChoose");
            ManagementPermission.Attributes.Add("class", "Permissions");
            AgentPermission.Attributes.Add("class", "Permissions");
            HttpContext.Current.Session["Add_Level"] = 3;
        }

        protected void ManagementPermission_Click(object sender, EventArgs e)
        {
            ManagementPermission.Attributes.Add("class", "PermissionsChoose");
            AgentPermission.Attributes.Add("class", "Permissions");
            SupervisorPermission.Attributes.Add("class", "Permissions");
            HttpContext.Current.Session["Add_Level"] = 1;
            if (HttpContext.Current.Session["AgentLevel"].ToString() == "2")
            {
                NameOrAddress.Visible = true;
                Address.Visible = true;
            }
        }

        protected void ImageFile_1_btnUpload_Click(object sender, EventArgs e)
        {
            int maxFileSize = 262144;
            ImageFile_1_lable.Visible = false;
            ImageFile_1_lable_2.Visible = false;
            if (ImageFile_1_FileUpload.HasFile && ImageFile_1_FileUpload.PostedFile.ContentLength < maxFileSize)
            {
                try
                {
                    string ext = System.IO.Path.GetExtension(ImageFile_1_FileUpload.FileName).ToLower();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
                    {
                        System.IO.Stream fs = ImageFile_1_FileUpload.PostedFile.InputStream;
                        System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        ImageFile_1_display.Src = "data:image/png;base64," + base64String;
                        Session["imgFileUpload1"] = ImageFile_1_FileUpload;
                    }
                    else
                    {
                        ImageFile_1_lable_2.Text = "* הסיומת לא תקינה";
                        ImageFile_1_lable_2.Visible = true;
                    }
                }
                catch
                {
                    ImageFile_1_lable_2.Text = "* בבקשה נסה שוב";
                    ImageFile_1_lable_2.Visible = true;
                }
            }
            else
            {
                ImageFile_1_lable_2.Text = "* התמונה גדולה מ250 קב,בבקשה הכנס תמונה חדשה";

                ImageFile_1_lable_2.Visible = true;
            }
        }
        
        protected void ClosePopUpAddAgent_Click(object sender, ImageClickEventArgs e)
        {
            AddAgentPopUp.Visible = false;
        }
        protected void AddNewAgent_Click(object sender, EventArgs e)
        {
            AddAgentPopUp.Visible = true;
            
        }

        protected void CreateEmployee_Click(object sender, EventArgs e)
        {
            AddAgentPopUp.Visible = true;
            UpdatePanelPopUps.Update();
            ImageFile_1_display.Src = "images/icons/User_Image_Avatar.png";
            Name.Value = "";
            Phone.Value = "";
            EmailA.Value = "";
            Tz.Value = "";
            PasswordAgent.Value = "";
            Address.Value = "";
            AgentNumber1.Value = "";
            AgentNumber2.Value = "";
            AgentNumber3.Value = "";
        }
        private void loadGraf()
        {
            string select = @"select
								sum(case when StatusOfferID = 2 THEN 1 ELSE 0 END) * 100 / count(*) as Percentage1,
								sum(case when StatusOfferID = 3 THEN 1 ELSE 0 END) * 100 / count(*) as Percentage2,
								sum(case when StatusOfferID = 9 THEN 1 ELSE 0 END) * 100 / count(*) as Percentage4
								FROM Offer
								left join Lead on Lead.ID = Offer.LeadID ";


            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdPayments = new SqlCommand();
            string sqlWhere = "", sqlJoin = "";
            if (HttpContext.Current.Session["AgentLevel"] != null)
            {
                switch (int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()))
                {

                    case 1:
                        sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID and A.Type  in (3,6) ";
                        break;
                    case 2:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type  in (3,6) inner join ArvootManagers B on B.ID = A.ParentID left join ArvootManagers C on C.ID = B.ParentID ";
                        sqlWhere = " Where C.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdPayments.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break; 
                    case 7:
                        sqlJoin = " inner join ArvootManagers A on A.ParentID = Lead.AgentID and A.Type  in (3,6) inner join ArvootManagers B on B.ID = A.ParentID left join ArvootManagers C on C.ID = B.ParentID ";
                        sqlWhere = " Where C.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdPayments.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 3:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type  in (3,6) inner join ArvootManagers B on B.ID = A.ParentID  ";
                        sqlWhere = " Where (B.ID = @ID OR A.ID = @ID) ";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdPayments.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 6:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type  in (3,6) ";
                        sqlWhere = " Where A.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdPayments.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 4:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type  in (3,6) inner join ArvootManagers B on B.ParentID = A.ParentID  ";
                        sqlWhere = " Where B.ID = @ID and IsInOperatingQueue = 1";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdPayments.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 5:
                        sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID ";
                        sqlWhere = " Where OperatorID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdPayments.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);

                        break;
                    default:
                        sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID and A.Type  in (3,6)";
                        break;

                }
            }

            //change details from offers, not leads
            //string select = @"SELECT CAST(
            //                        (SELECT COUNT(*) FROM Lead WHERE IsContact = 1 AND StatusContact = 2) * 100.0
            //                        /
            //                        (SELECT COUNT(*) FROM Lead WHERE IsContact = 1)
            //                        AS INT
            //                    ) AS Percentage1,
            //                    CAST(
            //                        (SELECT COUNT(*) FROM Lead WHERE IsContact = 1 AND StatusContact = 3) *100.0
            //                        /
            //                        (SELECT COUNT(*) FROM Lead WHERE IsContact = 1)
            //                        AS INT
            //                    ) AS Percentage2,

            //                    CAST(
            //                        (SELECT COUNT(*) FROM Lead WHERE IsContact = 1 AND StatusContact = 5) *100.0
            //                        /
            //                        (SELECT COUNT(*) FROM Lead WHERE IsContact = 1)
            //                        AS INT
            //                    ) AS Percentage4";

            /* סטטוס "בוטל" כרגע לא להציג
             * CAST(
                                    (SELECT COUNT(*) FROM Lead WHERE IsContact = 1 AND StatusContact = 4) *100.0
                                    /
                                    (SELECT COUNT(*) FROM Lead WHERE IsContact = 1)
                                    AS INT
                                ) AS Percentage3,*/

            /*to get paid and balance in the same query
             (SELECT SUM(sumPayment) FROM ServiceRequestPayment payments
								INNER JOIN ServiceRequest ON payments.ServiceRequestID = ServiceRequest.ID 
								WHERE IsApprovedPayment = 1) as paid,
								(SELECT Sum(ServiceRequest.[Sum]) - (SELECT SUM(sumPayment) FROM ServiceRequestPayment payments
								INNER JOIN ServiceRequest ON payments.ServiceRequestID = ServiceRequest.ID 
								WHERE IsApprovedPayment = 1) 
								+ Sum(CASE WHEN IsApprovedCreditOrDenial = 1 THEN SumCreditOrDenial ELSE 0 END) 
								from ServiceRequest  ) as balance1*/


            //SqlCommand cmd = new SqlCommand(select);
            cmd.CommandText = select + sqlJoin + sqlWhere;
            DataTable dt = DbProvider.GetDataTable(cmd);

            string sqlPayments = @"WITH ApprovedPayments AS (
    SELECT ServiceRequestID, SUM(sumPayment) AS TotalPayment
    FROM ServiceRequestPayment
    WHERE IsApprovedPayment = 1
    GROUP BY ServiceRequestID)
SELECT SUM(sr.[Sum]) 
    - COALESCE(SUM(ap.TotalPayment), 0) 
    + SUM(CASE WHEN sr.IsApprovedCreditOrDenial = 1 THEN sr.SumCreditOrDenial ELSE 0 END) AS balanceToPay,
	COALESCE(SUM(ap.TotalPayment), 0) - SUM(CASE WHEN sr.IsApprovedCreditOrDenial = 1 THEN sr.SumCreditOrDenial ELSE 0 END) as paid
FROM  ServiceRequest sr
LEFT JOIN ApprovedPayments ap ON sr.ID = ap.ServiceRequestID
inner join Offer On sr.OfferID = Offer.ID
left join Lead on Lead.ID = Offer.LeadID ";
            cmdPayments.CommandText = sqlPayments + sqlJoin + sqlWhere;
            DataTable dtPayments = DbProvider.GetDataTable(cmdPayments);


            Pageinit.CheckManagerPermissions();

            int percentage = !string.IsNullOrWhiteSpace(dt.Rows[0]["Percentage1"].ToString()) ? int.Parse(dt.Rows[0]["Percentage1"].ToString()) : 0;
            int percentage1 = !string.IsNullOrWhiteSpace(dt.Rows[0]["Percentage2"].ToString()) ? int.Parse(dt.Rows[0]["Percentage2"].ToString()) : 0;
            //int percentage2 = int.Parse(dt.Rows[0]["Percentage3"].ToString());
            int percentage3 = !string.IsNullOrWhiteSpace(dt.Rows[0]["Percentage4"].ToString()) ? int.Parse(dt.Rows[0]["Percentage4"].ToString()) : 0;
            double paidServices = !string.IsNullOrWhiteSpace(dtPayments.Rows[0]["paid"].ToString()) ? double.Parse(dtPayments.Rows[0]["paid"].ToString()) : 0;
            double serviceBalance = !string.IsNullOrWhiteSpace(dtPayments.Rows[0]["balanceToPay"].ToString()) ? double.Parse(dtPayments.Rows[0]["balanceToPay"].ToString()) : 0;

            PercentageText.Text = percentage + "%";
            PercentageText1.Text = percentage1 + "%";
            //PercentageText2.Text = percentage2 + "%";
            PercentageText3.Text = percentage3 + "%";

            if (dtPayments.Rows.Count > 0)
            {
                PercentageText4.Text = paidServices.ToString("N0") + "₪";
                PercentageText5.Text = serviceBalance.ToString("N0") + "₪";
            }


            double circumference = 2 * Math.PI * 54; // 2πr
            double offset = circumference - (percentage / 100.0 * circumference);
            double offset1 = circumference - (percentage1 / 100.0 * circumference);
            //double offset2 = circumference - (percentage2 / 100.0 * circumference);
            double offset3 = circumference - (percentage3 / 100.0 * circumference);
            double offset4 = circumference - (paidServices / (paidServices + serviceBalance) * circumference);
            double offset5 = circumference - (serviceBalance / (paidServices + serviceBalance) * circumference);
            if (paidServices + serviceBalance == 0)
            {
                offset4 = circumference;
                offset5 = circumference;
            }

            ClientScript.RegisterStartupScript(this.GetType(), "SetProgress",
                $"document.getElementById('progressPath1').style.strokeDasharray = '{circumference} {circumference}';" +
            $"document.getElementById('progressPath1').style.strokeDashoffset = '{offset1}';", true);

            ClientScript.RegisterStartupScript(this.GetType(), "SetProgress1",
                $"document.getElementById('progressPath').style.strokeDasharray = '{circumference} {circumference}';" +
            $"document.getElementById('progressPath').style.strokeDashoffset = '{offset}';", true);

            //ClientScript.RegisterStartupScript(this.GetType(), "SetProgress2",
            //    $"document.getElementById('progressPath2').style.strokeDasharray = '{circumference} {circumference}';" +
            //$"document.getElementById('progressPath2').style.strokeDashoffset = '{offset2}';", true);

            ClientScript.RegisterStartupScript(this.GetType(), "SetProgress3",
                $"document.getElementById('progressPath3').style.strokeDasharray = '{circumference} {circumference}';" +
            $"document.getElementById('progressPath3').style.strokeDashoffset = '{offset3}';", true);

            ClientScript.RegisterStartupScript(this.GetType(), "SetProgress4",
                $"document.getElementById('progressPath4').style.strokeDasharray = '{circumference} {circumference}';" +
            $"document.getElementById('progressPath4').style.strokeDashoffset = '{offset4}';", true);

            ClientScript.RegisterStartupScript(this.GetType(), "SetProgress5",
                $"document.getElementById('progressPath5').style.strokeDasharray = '{circumference} {circumference}';" +
            $"document.getElementById('progressPath5').style.strokeDashoffset = '{offset5}';", true);


        }
        protected void Month_General_Click(object sender, EventArgs e)
        {

            var btn = (Button)sender;
            if (btn.Text.Equals("נתון כללי"))
            {
                btn.Text = "נתון חודשי";
            }
            else
            {
                btn.Text = "נתון כללי";

            }
            loadGraf();
        }
        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("Status");


        }

        private void LoadTaskDates()
        {
            if (HttpContext.Current.Session["AgentID"] != null)
            {
                _datesWithTasks = new List<DateTime>();
                // Get the visible date range of the calendar
                //DateTime startDate = GetFirstVisibleDate(TasksCalendar);
                //DateTime endDate = GetLastVisibleDate(TasksCalendar);

                // Get the first day of the month
                DateTime firstDayOfMonth = TasksCalendar.VisibleDate;
                if (firstDayOfMonth == DateTime.MinValue)
                {
                    firstDayOfMonth = DateTime.Today;
                }

                DateTime startDate = GetFirstDisplayedDate(firstDayOfMonth);
                DateTime endDate = startDate.AddDays(41);

                string sqlDates = @"SELECT DISTINCT CAST(PerformDate AS DATE) AS TaskDate FROM Tasks
                                Left join Offer on Offer.ID = Tasks.OfferID 
                                Left JOIN Lead on Lead.ID = Offer.LeadID 
                                Left join Lead lead2 on Tasks.LeadID = lead2.ID
                                WHERE PerformDate BETWEEN @startDate AND @endDate AND isnull(isnull(Offer.OperatorID, Lead.AgentID),lead2.AgentID) = @AgentID";
                SqlCommand cmdDates = new SqlCommand(sqlDates);
                cmdDates.Parameters.AddWithValue("@startDate", startDate);
                cmdDates.Parameters.AddWithValue("@endDate", endDate);
                cmdDates.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);

                DataTable dtDates = DbProvider.GetDataTable(cmdDates);
                foreach (DataRow row in dtDates.Rows)
                {
                    _datesWithTasks.Add(DateTime.Parse(row["TaskDate"].ToString()));
                }

                // Query the database for dates with tasks
                // _datesWithTasks = GetDatesWithTasks(startDate, endDate);
            }

        }

        public static DateTime GetFirstDateOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime GetFirstDisplayedDate(DateTime date)
        {
            date = GetFirstDateOfMonth(date);
            return date.DayOfWeek == DayOfWeek.Sunday ? date.AddDays(-7) : date.AddDays((int)date.DayOfWeek * -1);
        }

        protected void TasksCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            e.Cell.Attributes.Add("OnClick", e.SelectUrl);
            if (e.Day.IsOtherMonth) // בדיקה האם היום אינו בחודש הנוכחי
            {
                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#e9f0fe"); // שינוי צבע רקע ליום שאינו בחודש הנוכחי ל-e9f0fe
            }

            //if (e.Day.Date == specificDate)
            if (_datesWithTasks != null && _datesWithTasks.Contains(e.Day.Date))
            {
                // Create a small dot and add it to the cell
                Literal dot = new Literal();
                dot.Text = "<div class='indicator'></div>";
                e.Cell.Controls.Add(dot);
                e.Cell.BorderColor = System.Drawing.ColorTranslator.FromHtml("#669EFF");

            }
            if (e.Day.Date == TasksCalendar.SelectedDate.Date)
            {
                e.Day.IsSelectable = false;
            }


        }

        protected void TasksCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            // This method will be called when the user navigates to a different month
            LoadTaskDates();
        }

        protected void TasksCalendar_SelectionChanged(object sender, EventArgs e)
        {
            string strTasks = @"select Tasks.ID, FORMAT(Tasks.PerformDate, 'dd.MM.yy') as dateTask , 
                                    CONVERT(varchar(5), Tasks.PerformDate, 108) as timeTask, Tasks.Text, ts.Status ,isnull(Tasks.OfferID,0) as OfferID , isnull(Tasks.LeadID ,0) as LeadID
                                    from Tasks left join Offer on Offer.ID = Tasks.OfferID left join Lead on Lead.ID = Offer.LeadID 
                                    left join TaskStatuses ts on ts.ID = Tasks.Status left join Lead lead2 on Tasks.LeadID = lead2.ID
                                    where isnull(isnull(Offer.OperatorID, Lead.AgentID),lead2.AgentID) = @AgentID
                                    AND CAST(PerformDate  AS DATE) = CAST(@selectedDate AS DATE)";

            SqlCommand cmdTasks = new SqlCommand(strTasks);
            cmdTasks.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
            cmdTasks.Parameters.AddWithValue("@selectedDate", TasksCalendar.SelectedDate);
            DataSet dsTasks = DbProvider.GetDataSet(cmdTasks);
            Repeater3.DataSource = dsTasks;
            Repeater3.DataBind();

            LoadTaskDates();
        }

        protected void BtnTask_Command(object sender, CommandEventArgs e)
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            if (arg[0].ToString().Equals("0")){
                System.Web.HttpContext.Current.Response.Redirect("OfferEdit.aspx?OfferID=" + arg[1].ToString());

            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect("LeadEdit.aspx?LeadID=" + arg[0].ToString());

            }
        }
        protected void BtnFutureTasks_Click(object sender, EventArgs e)
        {
            string strTasks = @"select Tasks.ID, FORMAT(Tasks.PerformDate, 'dd.MM.yy') as dateTask , 
                                    CONVERT(varchar(5), Tasks.PerformDate, 108) as timeTask, Tasks.Text, ts.Status ,isnull(Tasks.OfferID,0) as OfferID , isnull(Tasks.LeadID ,0) as LeadID
                                    from Tasks 
                                left join Offer on Offer.ID = Tasks.OfferID 
                                left JOIN Lead on Lead.ID = Offer.LeadID 
                                    left join TaskStatuses ts on ts.ID = Tasks.Status 
                                    left join Lead lead2 on Tasks.LeadID = lead2.ID
                                    where isnull(Lead.AgentID,lead2.AgentID) = @AgentID
                                    AND PerformDate between CAST(GETDATE() AS DATE) AND DATEADD(MONTH, 2, CAST(GETDATE() AS DATE))";
            SqlCommand cmdTasks = new SqlCommand(strTasks);
            cmdTasks.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
            DataTable dtTasks = DbProvider.GetDataTable(cmdTasks);
            Repeater3.DataSource = dtTasks;
            Repeater3.DataBind();

            LoadTaskDates();
            TasksCalendar.SelectedDates.Clear();
        }
    }
}