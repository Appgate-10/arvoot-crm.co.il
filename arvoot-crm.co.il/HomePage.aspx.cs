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
namespace ControlPanel
{
    public partial class _homePage : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();

        public string ListPageUrl = "LeadAdd.aspx";
        private List<DateTime> _datesWithTasks;


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Session["AgentID"] == null)
                {
                   HttpContext.Current.Response.Redirect("SignIn.aspx");
                }
                else
                {
                    string str = "select * from Lead where IsContact = 1";
                    SqlCommand cmd2 = new SqlCommand(str);
                    DataTable dt2 = DbProvider.GetDataTable(cmd2);
                    Repeater2.DataSource = dt2;
                    Repeater2.DataBind();

                    //Gila
                    string strTasks = @"select Tasks.ID, FORMAT(Tasks.PerformDate, 'dd.MM.yy') as dateTask , 
                                    CONVERT(varchar(5), Tasks.PerformDate, 108) as timeTask, Tasks.Text, ts.Status 
                                    from Tasks left join Offer on Offer.ID = Tasks.OfferID  left join Lead on Lead.ID = Offer.LeadID 
                                    left join TaskStatuses ts on ts.ID = Tasks.Status left join Lead lead2 on Tasks.LeadID = lead2.ID
                                    where isnull(Lead.AgentID,lead2.AgentID) = @AgentID
                                    AND PerformDate = CAST(@selectedDate AS DATE)";
                    SqlCommand cmdTasks = new SqlCommand(strTasks);

                    //Gila
                    cmdTasks.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
                    cmdTasks.Parameters.AddWithValue("@selectedDate", DateTime.Today);
                    DataTable dtTasks = DbProvider.GetDataTable(cmdTasks);
                    Repeater3.DataSource = dtTasks;
                    Repeater3.DataBind();

                    //Gila
                    TasksCalendar.SelectedDate = DateTime.Today;
                    LoadTaskDates();
                }
                

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
        private void loadGraf()
        {
            string select = @"SELECT CAST(
                                    (SELECT COUNT(*) FROM Lead WHERE IsContact = 1 AND StatusContact = 2) * 100.0
                                    /
                                    (SELECT COUNT(*) FROM Lead WHERE IsContact = 1)
                                    AS INT
                                ) AS Percentage1,
                                CAST(
                                    (SELECT COUNT(*) FROM Lead WHERE IsContact = 1 AND StatusContact = 3) *100.0
                                    /
                                    (SELECT COUNT(*) FROM Lead WHERE IsContact = 1)
                                    AS INT
                                ) AS Percentage2,
                                
                                CAST(
                                    (SELECT COUNT(*) FROM Lead WHERE IsContact = 1 AND StatusContact = 5) *100.0
                                    /
                                    (SELECT COUNT(*) FROM Lead WHERE IsContact = 1)
                                    AS INT
                                ) AS Percentage4";

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

            SqlCommand cmd = new SqlCommand(select);
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
LEFT JOIN ApprovedPayments ap ON sr.ID = ap.ServiceRequestID";
            SqlCommand cmdPayments = new SqlCommand(sqlPayments);
            DataTable dtPayments = DbProvider.GetDataTable(cmdPayments);


            Pageinit.CheckManagerPermissions();
            int percentage = int.Parse(dt.Rows[0]["Percentage1"].ToString());
            int percentage1 = int.Parse(dt.Rows[0]["Percentage2"].ToString());
            //int percentage2 = int.Parse(dt.Rows[0]["Percentage3"].ToString());
            int percentage3 = int.Parse(dt.Rows[0]["Percentage4"].ToString());
            double paidServices = double.Parse(dtPayments.Rows[0]["paid"].ToString());
            Double serviceBalance = double.Parse(dtPayments.Rows[0]["balanceToPay"].ToString());

            PercentageText.Text = percentage + "%";
            PercentageText1.Text = percentage1 + "%";
            //PercentageText2.Text = percentage2 + "%";
            PercentageText3.Text = percentage3 + "%";

            if (dtPayments.Rows.Count > 0)
            {
                PercentageText4.Text = paidServices.ToString() + "₪";
                PercentageText5.Text = serviceBalance.ToString() + "₪";
            }
           

            double circumference = 2 * Math.PI * 54; // 2πr
            double offset = circumference - (percentage / 100.0 * circumference);
            double offset1 = circumference - (percentage1 / 100.0 * circumference);
            //double offset2 = circumference - (percentage2 / 100.0 * circumference);
            double offset3 = circumference - (percentage3 / 100.0 * circumference);
            double offset4 = circumference - (paidServices / (paidServices + serviceBalance) * circumference);
            double offset5 = circumference - (serviceBalance / (paidServices + serviceBalance) * circumference);

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
            } else
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

            //Gila
            //2do - צריך לבדוק לפי הדרגה - אם מנהל, לא צריך את הסינון לפי סוכן
            string sqlDates = @"SELECT DISTINCT CAST(PerformDate AS DATE) AS TaskDate FROM Tasks
                                inner join Offer on Offer.ID = Tasks.OfferID 
                                INNER JOIN Lead on Lead.ID = Offer.LeadID 
                                left join Lead lead2 on Tasks.LeadID = lead2.ID
                                WHERE PerformDate BETWEEN @startDate AND @endDate AND isnull(Lead.AgentID,lead2.AgentID) = @AgentID";
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
                                    CONVERT(varchar(5), Tasks.PerformDate, 108) as timeTask, Tasks.Text, ts.Status 
                                    from Tasks left join Offer on Offer.ID = Tasks.OfferID left join Lead on Lead.ID = Offer.LeadID 
                                    left join TaskStatuses ts on ts.ID = Tasks.Status left join Lead lead2 on Tasks.LeadID = lead2.ID
                                    where isnull(Lead.AgentID,lead2.AgentID) = @AgentID
                                    AND CAST(PerformDate  AS DATE) = CAST(@selectedDate AS DATE)";

            SqlCommand cmdTasks = new SqlCommand(strTasks);
            cmdTasks.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
            cmdTasks.Parameters.AddWithValue("@selectedDate", TasksCalendar.SelectedDate);
            DataSet dsTasks = DbProvider.GetDataSet(cmdTasks);
            Repeater3.DataSource = dsTasks;
            Repeater3.DataBind();

            LoadTaskDates();
        }

        protected void BtnFutureTasks_Click(object sender, EventArgs e)
        {
            string strTasks = @"select Tasks.ID, FORMAT(Tasks.PerformDate, 'dd.MM.yy') as dateTask , 
                                    CONVERT(varchar(5), Tasks.PerformDate, 108) as timeTask, Tasks.Text, ts.Status 
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