﻿//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using RestSharp;
using System.Xml;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Configuration;
using ControlPanel.HelpersFunctions;
using System.Text;
using Newtonsoft.Json;

namespace ControlPanel
{
    public partial class DesignDisplay : System.Web.UI.MasterPage
    {
        string FileName1;
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageFile_1_display.Attributes.Add("onclick", "document.getElementById('" + ImageFile_1_FileUpload.ClientID + "').click();");

            if (!Page.IsPostBack)
            {             

                string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);
                
                if (pageName.ToLower() != "signin")
                {
                    if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) > 4)
                    {
                        BtnAgents.Visible = false;
                    }

                    MainContentDiv.Attributes.Add("class", "MainContentDiv");
                    ContentDiv.Attributes.Add("class", "ContentDiv");
                    DivContentPlaceHolder.Style.Add("width", "100%");
                    loadData();

                }
                else
                {
                    DivContentPlaceHolder.Style.Add("width", "100%");
                    MainContentDiv.Style.Add("width", "100%");
                }
            }

            else
            {
                string eventTarget = Request["__EVENTTARGET"];
                string eventArgument = Request["__EVENTARGUMENT"];

                if (eventTarget == "EditAgent" && !string.IsNullOrEmpty(eventArgument))
                {
                    int agentId = int.Parse(eventArgument);
                    EditAgent(agentId);
                }
                if (eventTarget == "DeleteAgent" && !string.IsNullOrEmpty(eventArgument))
                {
                    string[] args = eventArgument.Split('|');
                    if (args.Length == 2)
                    {
                        int agentId = int.Parse(args[0].ToString());
                        int show = int.Parse(args[1].ToString());

                        // Now you can use these parameters
                       DeleteAgent(agentId, show);
                    }
                    //       DeleteAgent(data.agentId, data.show);
                }
            }

        }
    
        //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{

        //}

        //changed to alerts
        protected void TaskListOpen_Click(object sender, EventArgs e)
        {
            AllTasksList.Visible = true;
            //SqlCommand cmdSelectTasks = new SqlCommand(@"select t.ID, Text,ts.Status,CONVERT(varchar,PerformDate, 104) as PerformDate, A.FullName from Tasks t 
            //                                             left join TaskStatuses ts on t.Status = ts.ID 
            //                                             left join Offer on Offer.ID = t.OfferID
            //                                             left join Lead on Lead.ID = Offer.LeadID
            //                                             left join ArvootManagers A on A.ID = Lead.AgentID");
            SqlCommand cmdAlerts = new SqlCommand("SELECT * From Alerts WHERE AgentID = @AgentID AND Show = 1");
            cmdAlerts.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
            DataSet ds = DbProvider.GetDataSet(cmdAlerts);
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
        }
        protected void CloseTasksListPopUp_Click(object sender, EventArgs e)
        {
            loadCountAlert();
            AllTasksList.Visible = false;

        }
        protected void navigateToOtherPage(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Response.Redirect("HomePage.aspx");


        }
        protected void DeleteTask_Command(object sender, CommandEventArgs e)
        {
            string strDel = "delete from Tasks where ID = @ID ";
            SqlCommand cmdDel = new SqlCommand(strDel);
            cmdDel.Parameters.AddWithValue("@ID", e.CommandArgument);
            if (DbProvider.ExecuteCommand(cmdDel) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);

            }
            TaskListOpen_Click(sender, null);
        }
        //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        //{

        //}
        //protected void OrderReciveTime_Command(object sender, CommandEventArgs e)
        //{
        //    var btn = (Button)sender;
        //    switch (btn.Text)
        //    {
        //        case "כל הלידים":
        //            {
        //                SpanLeadManagement.InnerText = btn.Text;
        //                Response.Redirect("Users.aspx");
        //                break;
        //            }
        //        case "אין מענה":
        //            {
        //                Response.Redirect("Business.aspx");
        //                SpanLeadManagement.InnerText = btn.Text;
        //                break;
        //            }
        //    }
        //}
        protected void BtnBarItem_Command(object sender, CommandEventArgs e)
        {
            Button btn = (Button)sender;
            var popUpId = btn.ID.Replace("BtnBarItem", "PopUp");
            var popUp = (HtmlGenericControl)FindControl(popUpId);

            if (HttpContext.Current.Session["popUp"] != null && HttpContext.Current.Session["popUp"].ToString() != popUpId)
            {
                var prevPopUpIp = (HtmlGenericControl)FindControl(HttpContext.Current.Session["popUp"].ToString());
                var prevBtnStr = HttpContext.Current.Session["popUp"].ToString().Replace("PopUp", "BtnBarItem");
                var prevBtn = (Button)FindControl(prevBtnStr);
                OpenCloseBtnPopUp(true, prevBtn, prevPopUpIp);
            }
            HttpContext.Current.Session["popUp"] = popUpId;
            bool isOpen = popUp.Visible;
            OpenCloseBtnPopUp(isOpen, btn, popUp);
        }
        public void OpenCloseBtnPopUp(bool isOpen, Button btn, HtmlGenericControl popUp)
        {
            if (isOpen == false)
            {
                popUp.Visible = true;
                btn.Style.Add("color", "#2da9fd");
                btn.Style["background-image"] = "url('../images/icons/Arrow_Blue_Button.png')";
            }
            else
            {
                popUp.Visible = false;
                btn.Style.Add("color", "#0f325e");
                btn.Style["background-image"] = "url('../images/icons/Arrow_Slide_Button.png')";
            }
        }
        protected void activitiesCal_SelectionChanged(object sender, EventArgs e)
        {
            DateTime seldate = activitiesCal.SelectedDate;
            loadActivityHistory(seldate);
        }
        protected void activitiesCal_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth) // בדיקה האם היום אינו בחודש הנוכחי
            {
                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#e9f0fe"); // שינוי צבע רקע ליום שאינו בחודש הנוכחי ל-e9f0fe
            }
            if (e.Cell.Width != 27)
            {
                e.Cell.Height = 29;
                e.Cell.BorderStyle = BorderStyle.Solid;
                //e.Cell.Attributes.Add("onclick", "window.location='ActivitiesCalendar.aspx?Date=" + e.Day.Date.ToString("yyyy-MM-dd") + "'");
            }
        }
        protected void UploadFile_Command(object sender, CommandEventArgs e)
        {
            loadData();
        }
        public void loadData()
        {
            string sql = "select ImageFile from ArvootManagers where ID = @ID";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
            string imageFileUrl = DbProvider.GetOneParamValueString(cmd);
            if (!string.IsNullOrEmpty(imageFileUrl))
                ProfileAgent.ImageUrl = ConfigurationManager.AppSettings["FilesUrl"] + "Agent/" + imageFileUrl;
            AgentName.Text = HttpContext.Current.Session["AgentName"].ToString();
            currentTime.InnerText = DateTime.Now.ToShortTimeString();

            loadCountAlert();
            loadActivityHistory(DateTime.Today);

        }

        public void loadCountAlert()
        {
            //string sql = "select COUNT(*) from Tasks ";
            string sql = "select COUNT(*) from Alerts Where AgentID = @AgentID and Show = 1 ";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
            long countAlert = DbProvider.GetOneParamValueLong(cmd);
            if (countAlert > 0)
            {
                CMessagesNumber.Visible = true;
                CMessagesNumber.Text = countAlert.ToString();
            }
            else CMessagesNumber.Visible = false;



        }

        public void loadActivityHistory(DateTime selectedDate)
        {
            string sqlHistory = "SELECT * FROM ActivityHistory WHERE AgentID = @agentID AND Show = 1 ";
            sqlHistory += "AND DATEADD(dd, 0, DATEDIFF(dd, 0, @theDate)) = DATEADD(dd, 0, DATEDIFF(dd, 0, CreateDate)) ";

            SqlCommand cmdHistory = new SqlCommand(sqlHistory);
            cmdHistory.Parameters.AddWithValue("@agentID", HttpContext.Current.Session["AgentID"]);
            cmdHistory.Parameters.AddWithValue("@theDate", selectedDate);

            DataSet dsHistory = DbProvider.GetDataSet(cmdHistory);

            Repeater2.DataSource = dsHistory;
            Repeater2.DataBind();
        }
        protected void LogOutBU_Click(object sender, EventArgs e)
        {


            //HttpContext.Current.Session["SignIn"] = false;
            //HttpContext.Current.Session["AgentID"] = "";
            //HttpContext.Current.Session["AgentName"] = "";
            //HttpContext.Current.Session["AgentLevel"] = "";
            //System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentID"] = "";
            //System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentIDToken"] = "";
            //System.Web.HttpContext.Current.Response.Cookies["AgentCookie"].Expires = (DateTime.Now.AddMinutes(1));

            //System.Web.HttpContext.Current.Response.Redirect("SignIn.aspx");


            setProfileEditPopUp(int.Parse(HttpContext.Current.Session["AgentID"].ToString()), false);
            /*
            SqlCommand cmdAgent = new SqlCommand("Select * From ArvootManagers Where ID = @AgentID");
            cmdAgent.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
            DataTable dtAgent = DbProvider.GetDataTable(cmdAgent);
            if (dtAgent.Rows.Count > 0)
            {
                SetAgentPopUp.Visible = true;
                BtnAgents.Visible = true;
                Name.Value = dtAgent.Rows[0]["FullName"].ToString();
                EmailA.Value = dtAgent.Rows[0]["Email"].ToString();
                Tz.Value = dtAgent.Rows[0]["Tz"].ToString();
                Phone.Value = dtAgent.Rows[0]["Phone"].ToString();
                PasswordAgent.Value = "";//dtAgent.Rows[0]["Password"].ToString();
                if (!string.IsNullOrWhiteSpace(dtAgent.Rows[0]["ImageFile"].ToString()))
                {
                    ImageFile_1_display.Src = ConfigurationManager.AppSettings["FilesUrl"] + "Agent/" + dtAgent.Rows[0]["ImageFile"].ToString();
                }
                else
                {
                    ImageFile_1_display.Src = "images/icons/User_Image_Avatar.png";
                }


                switch (HttpContext.Current.Session["AgentLevel"].ToString())
                {
                    case "2":
                        {
                            Address.Visible = true;
                            NameOrAddress.Visible = true;
                            NameOrAddress.InnerText = "שם החברה";
                            Address.Attributes["placeholder"] = "שם החברה";
                            Address.Value = dtAgent.Rows[0]["CompanyName"].ToString();
                            numbersAgentTitle.Visible = true;
                            numbersAgent.Visible = true;
                            Div1.Style.Add("height", "100%");
                            Div1.Style.Add("overflow-x", "scroll");
                            SqlCommand sql = new SqlCommand("select * from SourceLoanOrInsurance where ID<4");
                            DataTable dt = DbProvider.GetDataTable(sql);
                            company1.InnerText = dt.Rows[0]["Text"].ToString();
                            CompanyID1.Value = dt.Rows[0]["ID"].ToString();
                            company2.InnerText = dt.Rows[1]["Text"].ToString();
                            CompanyID2.Value = dt.Rows[1]["ID"].ToString();
                            company3.InnerText = dt.Rows[2]["Text"].ToString();
                            CompanyID3.Value = dt.Rows[2]["ID"].ToString();

                            SqlCommand cmdAgentNumbers = new SqlCommand("SELECT * FROM AgentNumbers Where CompanyManagerId = @AgentID Order By SourceId");
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
                            DataTable dtAgentsNumbers = DbProvider.GetDataTable(cmdAgentNumbers);
                            if (dtAgentsNumbers.Rows.Count > 0)
                            {
                                try
                                {
                                    AgentNumber1.Value = dtAgentsNumbers.Rows[0]["AgentNumber"].ToString();
                                    AgentNumber2.Value = dtAgentsNumbers.Rows[1]["AgentNumber"].ToString();
                                    AgentNumber3.Value = dtAgentsNumbers.Rows[2]["AgentNumber"].ToString();
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            break;
                        }
                    case "3":
                        {
                            Address.Visible = true;
                            NameOrAddress.Visible = true;
                            NameOrAddress.InnerText = "שם הסניף";
                            Address.Attributes["placeholder"] = "כתובת";
                            Address.Value = dtAgent.Rows[0]["BranchName"].ToString();
                            break;
                        }
                    case "4":
                    case "5":
                    case "6":
                        {
                            Address.Visible = false;
                            NameOrAddress.Visible = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
            */




        }


        protected void Logo_Click(object sender, EventArgs e)
        {
            //System.Web.HttpContext.Current.Response.Redirect("default.aspx");
        }

        protected void BtnDeleteHistory_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            SqlCommand cmdDelHistory = new SqlCommand("UPDATE TOP (1) ActivityHistory SET Show = 0 WHERE ID = @historyId and AgentID = @agentID");
            cmdDelHistory.Parameters.AddWithValue("@historyId", btn.CommandArgument);
            cmdDelHistory.Parameters.AddWithValue("@agentID", HttpContext.Current.Session["AgentID"]);
            if (DbProvider.ExecuteCommand(cmdDelHistory) > 0)
            {
                DateTime seldate = activitiesCal.SelectedDate;
                if (seldate == DateTime.MinValue)
                {
                    loadActivityHistory(DateTime.Today);
                }
                else
                {
                    loadActivityHistory(seldate);
                }
            }

        }
        protected void BtnDeleteAllHistory_Click(object sender, ImageClickEventArgs e)
        {
            DateTime seldate = activitiesCal.SelectedDate;
            ImageButton btn = sender as ImageButton;
            SqlCommand cmdDelHistory = new SqlCommand("UPDATE ActivityHistory SET Show = 0 WHERE AgentID = @agentID AND DATEADD(dd, 0, DATEDIFF(dd, 0, @theDate)) = DATEADD(dd, 0, DATEDIFF(dd, 0, CreateDate))");
            cmdDelHistory.Parameters.AddWithValue("@historyId", btn.CommandArgument);
            cmdDelHistory.Parameters.AddWithValue("@agentID", HttpContext.Current.Session["AgentID"]);
            cmdDelHistory.Parameters.AddWithValue("@theDate", seldate == DateTime.MinValue ? DateTime.Today : seldate);
            if (DbProvider.ExecuteCommand(cmdDelHistory) > 0)
            {

                if (seldate == DateTime.MinValue)
                {
                    loadActivityHistory(DateTime.Today);
                }
                else
                {
                    loadActivityHistory(seldate);
                }
            }
        }

        protected void DeleteAlert_Command(object sender, CommandEventArgs e)
        {
            string strDel = "Update Top (1) Alerts Set Show = 0 where ID = @ID ";
            SqlCommand cmdDel = new SqlCommand(strDel);
            cmdDel.Parameters.AddWithValue("@ID", e.CommandArgument);
            if (DbProvider.ExecuteCommand(cmdDel) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);

            }
            TaskListOpen_Click(sender, null);
        }

        protected void CloseAlertPopUp_Click(object sender, ImageClickEventArgs e)
        {
            NewAlertPopUp.Visible = false;
        }
        protected void BtnDetails_Command(object sender, CommandEventArgs e)
        {
            NewAlertPopUp.Visible = false;
            AllTasksList.Visible = false;
            string[] arg = e.CommandArgument.ToString().Split(',');

            if (!string.IsNullOrEmpty(arg[0]) && !arg[0].Equals("0"))
            {
                SqlCommand cmd = new SqlCommand("select IsContact from Lead where ID = @ID");
                cmd.Parameters.AddWithValue("@ID", arg[0]);
                DataTable dt = DbProvider.GetDataTable(cmd);
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0]["IsContact"].ToString()) == 0)
                    {
                        System.Web.HttpContext.Current.Response.Redirect("LeadEdit.aspx?LeadID=" + arg[0]);
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Response.Redirect("Contact.aspx?ContactID=" + arg[0]);
                    }
                }

            }
            else if (!string.IsNullOrEmpty(arg[1]) && !arg[1].Equals("0"))
            {
                System.Web.HttpContext.Current.Response.Redirect("OfferEdit.aspx?OfferID=" + arg[1]);

            }
        }

        protected void TimerAlerts_Tick(object sender, EventArgs e)
        {
            //if (NewAlertPopUp.Visible == false)
            //{
            SqlCommand cmdAlerts = new SqlCommand("SELECT * From Alerts WHERE AgentID = @AgentID AND Show = 1 AND DisplayDate <= getdate() AND IsRead = 0");
            cmdAlerts.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
            DataSet ds = DbProvider.GetDataSet(cmdAlerts);
            if (ds.Tables[0].Rows.Count > 0)
            {
                NewAlertPopUp.Visible = true;
                RepeaterNewAlerts.DataSource = ds;
                RepeaterNewAlerts.DataBind();
               
                SqlCommand cmdUpdate = new SqlCommand("UPDATE Alerts SET IsRead = 1 WHERE AgentID = @AgentID AND Show = 1 AND DisplayDate <= getdate() AND IsRead = 0");
                cmdUpdate.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
                DbProvider.ExecuteCommand(cmdUpdate);

            }
            //}


        }

        protected void ClosePopUpSetAgent_Click(object sender, ImageClickEventArgs e)
        {
            SetAgentPopUp.Visible = false;
        }

        protected void SaveAgent_Click(object sender, EventArgs e)
        {
            bool success = funcSave(sender, e);
            if (!success)
            {
                HiddenAgentID.Value = "";
                HiddenAgentLevel.Value = "";
                ScriptManager.RegisterStartupScript(UpdatePanelAlerts, UpdatePanelAlerts.GetType(), "showalert", "HideLoadingDiv();", true);
            }
            else
            {
                //Response.Redirect(ListPageUrl);
                ScriptManager.RegisterStartupScript(UpdatePanelAlerts, UpdatePanelAlerts.GetType(), "showalert", "HideLoadingDiv();", true);
                if (HiddenAgentID.Value == HttpContext.Current.Session["AgentID"].ToString())
                {
                    HiddenAgentID.Value = "";
                    HiddenAgentLevel.Value = "";
                    SetAgentPopUp.Visible = false;
                    loadData();
                }
                else
                {
                    HiddenAgentID.Value = "";
                    HiddenAgentLevel.Value = "";
                    setProfileEditPopUp(int.Parse(HttpContext.Current.Session["AgentID"].ToString()), false);
                }
                

            }
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
            if (Helpers.AgentEmailExist(EmailA.Value, long.Parse(/*HttpContext.Current.Session["AgentID"].ToString()*/HiddenAgentID.Value)) == "true")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "אימייל זה כבר קיים במערכת";
                return false;
            }
            if (Address.Visible == true && string.IsNullOrWhiteSpace(Address.Value) && /*!HttpContext.Current.Session["AgentLevel"].ToString()*/!HiddenAgentLevel.Value.Equals("1"))
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
            if (Helpers.AgentPhoneExist(Phone.Value, long.Parse(/*HttpContext.Current.Session["AgentID"].ToString()*/HiddenAgentID.Value)) == "true")
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
            if (Helpers.AgentTzExist(Tz.Value, long.Parse(/*HttpContext.Current.Session["AgentID"].ToString()*/HiddenAgentID.Value)) == "true")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "ת.ז קיימת במערכת";
                return false;

            }
            //if (PasswordAgent.Value == "")
            //{
            //    ErrorCount++;
            //    FormErrorAgent_lable.Visible = true;
            //    FormErrorAgent_lable.Text = "יש להזין סיסמא ";
            //    return false;
            //}
            if (PasswordAgent.Value != "" && PasswordAgent.Value.Length < 6)
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין סיסמא תקינה ";
                return false;
            }

            if (ErrorCount == 0)
            {

                string sql = @"UPDATE ArvootManagers SET Email = @Email, FullName = @FullName, Tz = @Tz, 
                            Phone = @Phone, ImageFile = @ImageFile, CompanyName = @CompanyName, BranchName = @BranchName ";
                if (PasswordAgent.Value != "")
                {
                    sql += ", Password = @Password ";
                }
                sql += " WHERE ID = @AgentID ";

                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@AgentID", /*HttpContext.Current.Session["AgentID"].ToString()*/HiddenAgentID.Value);
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


                switch (/*HttpContext.Current.Session["AgentLevel"].ToString()*/HiddenAgentLevel.Value)
                {
                    case "7":
                    case "2":
                        {
                            cmd.Parameters.AddWithValue("@CompanyName", Address.Value);
                            cmd.Parameters.AddWithValue("@BranchName", DBNull.Value);

                            break;
                        }
                    case "3":
                        {
                            cmd.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                            cmd.Parameters.AddWithValue("@BranchName", Address.Value);
                            break;
                        }
                    case "4":
                    case "5":
                    case "6":
                        {
                            cmd.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                            cmd.Parameters.AddWithValue("@BranchName", DBNull.Value);
                            break;
                        }
                    default:
                        {
                            cmd.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                            cmd.Parameters.AddWithValue("@BranchName", DBNull.Value);
                            break;
                        }

                }
                if (DbProvider.ExecuteCommand(cmd) > 0)
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
                    if (/*HttpContext.Current.Session["AgentLevel"].ToString()*/HiddenAgentLevel.Equals("2"))
                    {
                        string sqlAgentNumbers = "Update AgentNumbers Set AgentNumber = @AgentNumber Where CompanyManagerId = @CompanyManagerId And SourceId = @SourceId";

                        if (!string.IsNullOrEmpty(AgentNumber1.Value))
                        {
                            SqlCommand cmdAgentNumbers = new SqlCommand(sqlAgentNumbers);
                            cmdAgentNumbers.Parameters.AddWithValue("@CompanyManagerId", /*HttpContext.Current.Session["AgentID"]*/HiddenAgentID.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@SourceId", CompanyID1.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentNumber", AgentNumber1.Value);
                            DbProvider.ExecuteCommand(cmdAgentNumbers);

                        }
                        if (!string.IsNullOrEmpty(AgentNumber2.Value))
                        {
                            SqlCommand cmdAgentNumbers = new SqlCommand(sqlAgentNumbers);
                            cmdAgentNumbers.Parameters.AddWithValue("@CompanyManagerId", /*HttpContext.Current.Session["AgentID"]*/HiddenAgentID.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@SourceId", CompanyID2.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentNumber", AgentNumber2.Value);
                            DbProvider.ExecuteCommand(cmdAgentNumbers);


                        }
                        if (!string.IsNullOrEmpty(AgentNumber3.Value))
                        {
                            SqlCommand cmdAgentNumbers = new SqlCommand(sqlAgentNumbers);
                            cmdAgentNumbers.Parameters.AddWithValue("@CompanyManagerId", /*HttpContext.Current.Session["AgentID"]*/HiddenAgentID.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@SourceId", CompanyID3.Value);
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentNumber", AgentNumber3.Value);
                            DbProvider.ExecuteCommand(cmdAgentNumbers);


                        }
                    }

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

        protected void LogOutBtn_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session["SignIn"] = false;
            HttpContext.Current.Session["AgentID"] = "";
            HttpContext.Current.Session["AgentName"] = "";
            HttpContext.Current.Session["AgentLevel"] = "";
            System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentID"] = "";
            System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentIDToken"] = "";
            System.Web.HttpContext.Current.Response.Cookies["AgentCookie"].Expires = (DateTime.Now.AddMinutes(1));

            System.Web.HttpContext.Current.Response.Redirect("SignIn.aspx");
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

        private List<Agent> GetChildrenOfConnectedAgent(string AgentID)
        {
            List<Agent> agents = new List<Agent>();
            string sqlAgents = "Select A.*, T.Type as TypeName From ArvootManagers A Inner Join EmployeeType T On T.ID = A.Type ";
            if (AgentID == HttpContext.Current.Session["AgentID"].ToString() && HttpContext.Current.Session["AgentLevel"].ToString() == "1")
            {
                sqlAgents += "Where ParentID Is Null And A.ID <> @AgentID ";
            }
            else
            {
                sqlAgents += "Where ParentID = @AgentID ";
            }
            SqlCommand cmdAgents = new SqlCommand(sqlAgents);
            cmdAgents.Parameters.AddWithValue("@AgentID", AgentID);
            DataTable dtAgents = DbProvider.GetDataTable(cmdAgents);
            foreach (DataRow row in dtAgents.Rows)
            {
                List<Agent> childrenAgent = GetChildrenOfConnectedAgent(row["ID"].ToString());
                Agent ag = new Agent
                {
                    Id = int.Parse(row["ID"].ToString()),
                    Name = row["FullName"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Level = int.Parse(row["Type"].ToString()),
                    LevelName = row["TypeName"].ToString(),
                    Show = int.Parse(row["Show"].ToString()),
                    ParentId = !string.IsNullOrWhiteSpace(row["ParentID"].ToString()) ? int.Parse(row["ParentID"].ToString()) : 0,
                    Children = childrenAgent
                };
                agents.Add(ag);
            }

            return agents;
        }

        private string RenderAgentHierarchy(List<Agent> agents)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var agent in agents)
            {
                sb.Append(RenderAgentItem(agent));
            }
            return sb.ToString();
        }

        private string RenderAgentItem(Agent agent)
        {
            string show1 = "", show2 = "display: none";
            if (agent.Show == 1)
            {
                show1 = "display:none";
                show2 = "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append($@"<div class='agent-item'>
<div class='ListDivParams agent-info' onclick='toggleChildren({agent.Id})'>
<div style='width: 10%; text-align: right; background-color: transparent' id='expandIcon_{agent.Id}' class='expand-icon'>▶</div>
<div style='width: 20%; text-align: right; background-color: transparent'>{agent.Name}</div>
<div style='width: 20%; text-align: right; background-color: transparent;'>{agent.Phone}</div>
<div style='width: 20%; text-align: right; background-color: transparent'>{agent.Email}</div>
<div style='width: 20%; text-align: right; background-color: transparent; padding-right: 4%' >{agent.LevelName}</div>
<div style='width: 10%; text-align: right; background-color: transparent'>
<asp:ImageButton runat='server' ID='EditButton' ImageUrl='/images/icons/Open_Mession_Edit_Button.png' OnClick='EditButton_Click' CommandArgument='{agent.Id}' />
<input type='image' src='/images/icons/Open_Mession_Edit_Button.png' alt='Edit' style='position:relative; cursor:pointer;' onclick='editAgent({agent.Id}); return false;' />
</div>
<div style='width: 10%; text-align: right; background-color: transparent' onclick='event.stopPropagation()'>
<input type='image' src='/images/icons/Non_Active_Icon.png' alt='Active' style='position:relative; cursor:pointer;width:15px;" + show2 + $@"' onclick='deleteAgent({agent.Id},0); return false;' />
<input type='image' src='/images/icons/Active_Icon.png' alt='Delete' style='position:relative; cursor:pointer;width:15px;" + show1 + $@"' onclick='deleteAgent({agent.Id},1); return false;' />
</div>
</div>
<div id='children_{agent.Id}' class='agent-children' style='display: none;'>");



            if (agent.Children != null && agent.Children.Count > 0)
            {
                sb.Append(RenderAgentHierarchy(agent.Children));
            }

            sb.Append("</div></div>");
            return sb.ToString();
        }

        protected void BtnAgents_Click(object sender, EventArgs e)
        {
            AgentsListPopUp.Visible = true;
            //UpdatePanelAlerts.Update();
            List<Agent> topLevelAgents = GetChildrenOfConnectedAgent(HttpContext.Current.Session["AgentID"].ToString());
            AgentHierarchyLiteral.Text = RenderAgentHierarchy(topLevelAgents);
        }

        protected void CloseAgentsPopUp_Click(object sender, ImageClickEventArgs e)
        {
            AgentsListPopUp.Visible = false;
        }       
        protected void OnSearch(object sender, EventArgs e)
        {
            resultSearch.Text = "";
            if (!String.IsNullOrWhiteSpace(TxtInsuredSearch.Value))
            {
                string sql = @"select L.ID, L.IsContact,L.FirstName,L.LastName,A.FullName, A.Type, isnull(A.CompanyName,isnull(B.CompanyName,C.CompanyName)) as CompanyName
                            from Lead L left join ArvootManagers A on A.ID = L.AgentID 
                             LEFT JOIN ArvootManagers B ON a.ParentID = b.ID
                             LEFT JOIN ArvootManagers C ON b.ParentID = c.ID
                             where Phone1 = '" + TxtInsuredSearch.Value + "' or L.Tz = '" + TxtInsuredSearch.Value + "' ";
                SqlCommand cmd = new SqlCommand(sql);
                DataTable dt = DbProvider.GetDataTable(cmd);
                if (dt.Rows.Count > 0)
                {
                    resultSearch.Visible = true;
                    resultSearch.Text = "שם " + (dt.Rows[0]["IsContact"].ToString().Equals("1") ? "איש קשר: " : "ליד: ") + dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString() + "\n נציג מטפל: " + dt.Rows[0]["FullName"].ToString() + " חברה: " + dt.Rows[0]["CompanyName"].ToString();
                    resultSearch.Style.Add("white-space", "pre");
                }
            }


        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
        }

        private void EditAgent(int agentId)
        {
            setProfileEditPopUp(agentId, true);
        } 
        
        private void DeleteAgent(int agentId,int Show)
        {
            SqlCommand cmdUpdate = new SqlCommand("update ArvootManagers set Show = @Show Where ID = @AgentID");
            cmdUpdate.Parameters.AddWithValue("@AgentID", agentId);
            cmdUpdate.Parameters.AddWithValue("@Show", Show);
            DbProvider.ExecuteCommand(cmdUpdate);
            BtnAgents_Click(null, null);

        }

        public void setProfileEditPopUp(int agentID, bool isChild)
        {
            SqlCommand cmdAgent = new SqlCommand("Select * From ArvootManagers Where ID = @AgentID");
            cmdAgent.Parameters.AddWithValue("@AgentID", agentID);
            DataTable dtAgent = DbProvider.GetDataTable(cmdAgent);
            if (dtAgent.Rows.Count > 0)
            {
                HiddenAgentID.Value = agentID.ToString();
                HiddenAgentLevel.Value = dtAgent.Rows[0]["Type"].ToString();
                SetAgentPopUp.Visible = true;
                if (isChild == true || (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) > 4))
                {
                    BtnAgents.Visible = false;
                }
                else
                {
                    BtnAgents.Visible = true;
                }
                
                AgentsListPopUp.Visible = false;
                Name.Value = dtAgent.Rows[0]["FullName"].ToString();
                EmailA.Value = dtAgent.Rows[0]["Email"].ToString();
                Tz.Value = dtAgent.Rows[0]["Tz"].ToString();
                Phone.Value = dtAgent.Rows[0]["Phone"].ToString();
                PasswordAgent.Value = "";//dtAgent.Rows[0]["Password"].ToString();
                if (!string.IsNullOrWhiteSpace(dtAgent.Rows[0]["ImageFile"].ToString()))
                {
                    ImageFile_1_display.Src = ConfigurationManager.AppSettings["FilesUrl"] + "Agent/" + dtAgent.Rows[0]["ImageFile"].ToString();
                }
                else
                {
                    ImageFile_1_display.Src = "images/icons/User_Image_Avatar.png";
                }


                switch (dtAgent.Rows[0]["Type"].ToString())
                {
                    case "1":
                        {
                            Address.Visible = false;
                            NameOrAddress.Visible = false;
                            numbersAgentTitle.Visible = false;
                            numbersAgent.Visible = false;
                            break;
                        }
                    case "2":
                        {
                            Address.Visible = true;
                            NameOrAddress.Visible = true;
                            NameOrAddress.InnerText = "שם החברה";
                            Address.Attributes["placeholder"] = "שם החברה";
                            Address.Value = dtAgent.Rows[0]["CompanyName"].ToString();
                            numbersAgentTitle.Visible = true;
                            numbersAgent.Visible = true;
                            Div1.Style.Add("height", "100%");
                            Div1.Style.Add("overflow-x", "scroll");
                            SqlCommand sql = new SqlCommand("select * from SourceLoanOrInsurance where ID<4");
                            DataTable dt = DbProvider.GetDataTable(sql);
                            company1.InnerText = dt.Rows[0]["Text"].ToString();
                            CompanyID1.Value = dt.Rows[0]["ID"].ToString();
                            company2.InnerText = dt.Rows[1]["Text"].ToString();
                            CompanyID2.Value = dt.Rows[1]["ID"].ToString();
                            company3.InnerText = dt.Rows[2]["Text"].ToString();
                            CompanyID3.Value = dt.Rows[2]["ID"].ToString();

                            SqlCommand cmdAgentNumbers = new SqlCommand("SELECT * FROM AgentNumbers Where CompanyManagerId = @AgentID Order By SourceId");
                            cmdAgentNumbers.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
                            DataTable dtAgentsNumbers = DbProvider.GetDataTable(cmdAgentNumbers);
                            if (dtAgentsNumbers.Rows.Count > 0)
                            {
                                try
                                {
                                    AgentNumber1.Value = dtAgentsNumbers.Rows[0]["AgentNumber"].ToString();
                                    AgentNumber2.Value = dtAgentsNumbers.Rows[1]["AgentNumber"].ToString();
                                    AgentNumber3.Value = dtAgentsNumbers.Rows[2]["AgentNumber"].ToString();
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            break;
                        }
                    case "3":
                        {
                            numbersAgentTitle.Visible = false;
                            numbersAgent.Visible = false;
                            Address.Visible = true;
                            NameOrAddress.Visible = true;
                            NameOrAddress.InnerText = "שם הסניף";
                            Address.Attributes["placeholder"] = "כתובת";
                            Address.Value = dtAgent.Rows[0]["BranchName"].ToString();
                            break;
                        }
                    case "4":
                    case "5":
                    case "6":
                        {
                            numbersAgentTitle.Visible = false;
                            numbersAgent.Visible = false;
                            Address.Visible = false;
                            NameOrAddress.Visible = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
        }
    }
}

public class Agent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int Level { get; set; }
    public string LevelName { get; set; }
    public int? ParentId { get; set; }
    public int Show { get; set; }
    public List<Agent> Children { get; set; } = new List<Agent>();
}