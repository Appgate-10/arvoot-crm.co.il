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

namespace ControlPanel
{
    public partial class DesignDisplay : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);
                if (pageName.ToLower() != "signin")
                {
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
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{

        //}
        protected void TaskListOpen_Click(object sender, EventArgs e)
        {
            AllTasksList.Visible = true;
            SqlCommand cmdSelectTasks = new SqlCommand(@"select t.ID, Text,ts.Status,CONVERT(varchar,PerformDate, 104) as PerformDate, Agent.FullName from Tasks t 
                                                         left join TaskStatuses ts on t.Status = ts.ID 
                                                         left join Offer on Offer.ID = t.OfferID
                                                         left join Lead on Lead.ID = Offer.LeadID
                                                         left join Agent on Agent.ID = Lead.AgentID");
            DataSet ds = DbProvider.GetDataSet(cmdSelectTasks);
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
        }  
        protected void CloseTasksListPopUp_Click(object sender, EventArgs e)
        {
            loadCountAlert();
            AllTasksList.Visible = false;
            
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

            if (HttpContext.Current.Session["popUp"] != null&& HttpContext.Current.Session["popUp"].ToString()!=popUpId) 
            {
                var prevPopUpIp = (HtmlGenericControl)FindControl(HttpContext.Current.Session["popUp"].ToString());
                var prevBtnStr = HttpContext.Current.Session["popUp"].ToString().Replace("PopUp", "BtnBarItem");
                var prevBtn = (Button)FindControl(prevBtnStr);
                OpenCloseBtnPopUp(true, prevBtn, prevPopUpIp);
            }
            HttpContext.Current.Session["popUp"] = popUpId;
            bool isOpen = popUp.Visible;
            OpenCloseBtnPopUp(isOpen,btn,popUp);
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

        }
        public void loadData()
        {
            string sql = "select ImageFile from Agent where ID = @ID";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
            string imageFileUrl = DbProvider.GetOneParamValueString(cmd);
            if(!string.IsNullOrEmpty(imageFileUrl))
                ProfileAgent.ImageUrl = ConfigurationManager.AppSettings["FilesUrl"] + "Agent/" + imageFileUrl;
            AgentName.Text = HttpContext.Current.Session["AgentName"].ToString();
            loadCountAlert();
            loadActivityHistory(DateTime.Today);

        }     
        
        public void loadCountAlert()
        {
            string sql = "select COUNT(*) from Tasks ";
            SqlCommand cmd = new SqlCommand(sql);
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


            HttpContext.Current.Session["SignIn"] = false;
            HttpContext.Current.Session["AgentID"] = "";
            HttpContext.Current.Session["AgentName"] = "";
            HttpContext.Current.Session["AgentLevel"] = "";
            System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentID"] = "";
            System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentIDToken"] = "";
            System.Web.HttpContext.Current.Response.Cookies["AgentCookie"].Expires = (DateTime.Now.AddMinutes(1));

            System.Web.HttpContext.Current.Response.Redirect("SignIn.aspx");
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
    }
}