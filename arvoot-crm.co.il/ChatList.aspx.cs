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
    public partial class _chatList : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "Search";
        public string StrSrc { get { return strSrc; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {
                Pageinit.CheckManagerPermissions();


                //loadUsers(1);
                loadData();
            }
        }



        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FuncRepeater(sender, e);
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        public void FuncRepeater(object sender, RepeaterItemEventArgs e)
        {
            HtmlImage imgBlue = (HtmlImage)e.Item.FindControl("ImgBlue");
            HtmlGenericControl row = (HtmlGenericControl)e.Item.FindControl("Row");
            if (e.Item.ItemIndex == 0)
            {
                imgBlue.Src = "images/icons/Open_Mession_Blue_Point_1.png";
                row.Style.Add("align-items", "center");
                row.Style.Add("height", "40px");
            }
            else
            {
                imgBlue.Src = "images/icons/Open_Mession_Blue_Point_2.png";
                row.Style.Add("align-items", "end");
                row.Style.Add("height", "62px");
            }
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
        //public void loadData()
        //{
        //    string sql = @"  select  ID,TaskText,CONVERT(varchar,CreationDate, 104) AS TasksDate,
        //                             CONVERT(VARCHAR(5),CreationDate,108) As TasksTime
        //                          from Tasks";

        //    SqlCommand cmd = new SqlCommand(sql);

        //    DataSet ds = DbProvider.GetDataSet(cmd);

        //    Repeater1.DataSource = ds;
        //    Repeater1.DataBind();

        //    string sql2 = @"
        //                      select  ID,AlertText,CONVERT(varchar,CreationDate, 104) AS AlertsDate
        //                             from Alerts";

        //    SqlCommand cmd2 = new SqlCommand(sql2);

        //    DataSet ds2 = DbProvider.GetDataSet(cmd2);

        //    Repeater2.DataSource = ds2;
        //    Repeater2.DataBind();
        //}
        public void loadData()
        {
            //string sql = @"  select * from users";

            //SqlCommand cmd = new SqlCommand(sql);

            //DataSet ds = DbProvider.GetDataSet(cmd);

            //Repeater1.DataSource = ds;
            //Repeater1.DataBind();

            //string sql2 = @" select * from users";

            //SqlCommand cmd2 = new SqlCommand(sql2);

            //DataSet ds2 = DbProvider.GetDataSet(cmd2);

            //Repeater2.DataSource = ds2;
            //Repeater2.DataBind();
        }
        protected void SuspensionBU_Click(object sender, CommandEventArgs e)
        {
            SqlCommand cmdUpdate = new SqlCommand("Update Users Set Users.Show = 0 Where IDUser = @ID");
            cmdUpdate.Parameters.AddWithValue("@ID", long.Parse(e.CommandArgument.ToString()));
            if (DbProvider.ExecuteCommand(cmdUpdate) > 0)
            {
                var btn = (ImageButton)sender;
                var item = (RepeaterItem)btn.NamingContainer;
                var btnSuspensionBU = (ImageButton)item.FindControl("SuspensionBU");
                var btnActivatingBU = (ImageButton)item.FindControl("ActivatingBU");
                var divShowStatus = (HtmlGenericControl)item.FindControl("ShowStatus");
                if (btnSuspensionBU != null)
                {
                    divShowStatus.Attributes.Add("class", "ListDivShowStatusRed");
                    btnSuspensionBU.Visible = false;
                    btnActivatingBU.Visible = true;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('An error occurred');", true);
            }
        }
        protected void CopyLid_Click(object sender, ImageClickEventArgs e)
        {
        }
        protected void ActivatingBU_Click(object sender, CommandEventArgs e)
        {
            SqlCommand cmdUpdate = new SqlCommand("Update Users Set Users.Show = 1 Where IDUser = @ID");
            cmdUpdate.Parameters.AddWithValue("@ID", long.Parse(e.CommandArgument.ToString()));
            if (DbProvider.ExecuteCommand(cmdUpdate) > 0)
            {
                var btn = (ImageButton)sender;
                var item = (RepeaterItem)btn.NamingContainer;
                var btnSuspensionBU = (ImageButton)item.FindControl("SuspensionBU");
                var btnActivatingBU = (ImageButton)item.FindControl("ActivatingBU");
                var divShowStatus = (HtmlGenericControl)item.FindControl("ShowStatus");
                if (btnSuspensionBU != null)
                {
                    divShowStatus.Attributes.Add("class", "ListDivShowStatusGreen");
                    btnSuspensionBU.Visible = true;
                    btnActivatingBU.Visible = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('An error occurred');", true);
            }
        }


        protected void ButtonDiv_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

        }
        protected void btnWithVolunteers_Click(object sender, EventArgs e)
        {
            //string myDate = hiddenDate.Value;

            //if (myDate != null && !string.IsNullOrWhiteSpace(myDate))
            //{
            //    DateTime seldate = DateTime.Parse(myDate);//activitiesCal.SelectedDate;

            //    activitiesCal.SelectedDate = seldate;
            //    activitiesCal.VisibleDate = activitiesCal.SelectedDate;

            //    string sqlActivities = "Select activities.*, COALESCE(NULLIF(activities.name,''), activities.name_en) as name_by_lng, ";
            //    sqlActivities = sqlActivities + "(Select count(id) from activity_user Where activity_id = activities.id And activity_user.deleted_at1 Is Null And join_status = 'joined') as num_volunteers, ";
            //    sqlActivities = sqlActivities + "(select name From areas where id = activities.area_id) as area_name, ";
            //    sqlActivities = sqlActivities + "users.full_name, users.profile_image from activities ";
            //    sqlActivities = sqlActivities + "Inner join users On activities.user_id = users.id ";
            //    sqlActivities = sqlActivities + "Where date(start_date) <= date(@start_date) And date(end_date) >= date(@start_date) ";
            //    if (ShowDeleted.Checked == false)
            //    {
            //        sqlActivities = sqlActivities + "And activities.deleted_at IS Null ";
            //    }
            //    sqlActivities = sqlActivities + "And activities.id in (select activity_id from activity_user Where activity_user.deleted_at1 Is Null And join_status = 'joined' group by activity_id) ";

            //    //if (areas.SelectedIndex > 0)
            //    //{
            //    //    sqlActivities = sqlActivities + "And activities.country_id = 1 And activities.area_id = " + areas.SelectedIndex;
            //    //}
            //    //if (Session["orderByArea"] != null && int.Parse(Session["orderByArea"].ToString()) == 1)
            //    //{
            //    //    sqlActivities = sqlActivities + "Order By activities.area_id desc ";
            //    //}
            //    //else if (Session["orderByArea"] != null && int.Parse(Session["orderByArea"].ToString()) == 2)
            //    //{
            //    //    sqlActivities = sqlActivities + "Order By activities.area_id asc ";
            //    //}

            //    MySqlCommand cmdActivities = new MySqlCommand(sqlActivities);
            //    cmdActivities.Parameters.AddWithValue("@start_date", seldate.ToString("yyyy-MM-dd"));
            //    DataSet ds = MySqlDbProvider.GetDataSet(cmdActivities);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        activitiesList.Visible = true;
            //        VolunteersList.Visible = false;
            //        Repeater1.DataSource = ds;
            //        Repeater1.DataBind();
            //        TotalActivities.InnerText = "Total: " + ds.Tables[0].Rows.Count;
            //    }
            //    else
            //    {
            //        activitiesList.Visible = false;
            //    }
            //}
        }

        protected void activitiesCal_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth) // בדיקה האם היום אינו בחודש הנוכחי
            {
                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#e9f0fe"); // שינוי צבע רקע ליום שאינו בחודש הנוכחי ל-e9f0fe
            }
            if (e.Cell.Width != 96)
            {
                e.Cell.Height =69;
                e.Cell.BorderStyle = BorderStyle.Solid;
                //e.Cell.Attributes.Add("onclick", "window.location='ActivitiesCalendar.aspx?Date=" + e.Day.Date.ToString("yyyy-MM-dd") + "'");
            }
        }
        //protected void activitiesCal_DayRender(object sender, DayRenderEventArgs e)
        //{

        //if (e.Cell.Width != 100)
        //{
        //    e.Cell.Height = 90;
        //    e.Cell.BorderStyle = BorderStyle.Solid;

        //    e.Cell.Attributes.Add("onclick", "window.location='ActivitiesCalendar.aspx?Date=" + e.Day.Date.ToString("yyyy-MM-dd") + "'");

        //    if (dt.Rows.Count <= 0)
        //    {
        //        Session["index"] = 0;
        //        string sql = "Select start_date, id, name ";
        //        sql = sql + "from activities aa Where date(start_date) >= date(@my_date) ";
        //        if (ShowDeleted.Checked == false)
        //        {
        //            sql = sql + "And deleted_at IS Null ";
        //        }
        //        sql = sql + "group by date(start_date) order by start_date ";

        //        MySqlCommand cmd = new MySqlCommand(sql);
        //        cmd.Parameters.AddWithValue("@my_date", e.Day.Date);
        //        dt = MySqlDbProvider.GetDataTable(cmd);

        //    }


        //    if (Session["index"] == null)
        //    {
        //        Session["index"] = 0;
        //    }

        //    int myIndex = int.Parse(Session["index"].ToString());
        //    if (myIndex + 1 >= dt.Rows.Count)
        //    {
        //        //שליפת מספר הפעילויות הפתוחות והפעילות ביום זה
        //        //string sql1 = "Select sum(id in (select distinct activity_id from activity_user Where activity_user.deleted_at1 Is Null And date(start_date) <= date(@my_date) And date(end_date) >= date(@my_date) group by activity_id, user_id)) as active_activities, ";
        //        string sql1 = "Select ";
        //        sql1 = sql1 + "sum(1) as open_activities ";
        //        sql1 = sql1 + "From activities ";
        //        sql1 = sql1 + "Where date(start_date) <= date(@my_date) And date(end_date) >= date(@my_date) ";
        //        if (ShowDeleted.Checked == false)
        //        {
        //            sql1 = sql1 + "And deleted_at IS Null ";
        //        }
        //        MySqlCommand cmdOpenActivities = new MySqlCommand(sql1);
        //        cmdOpenActivities.Parameters.AddWithValue("@my_date", e.Day.Date);
        //        DataTable dt1 = MySqlDbProvider.GetDataTable(cmdOpenActivities);

        //        if (dt1.Rows.Count > 0)
        //        {
        //            Literal l = new Literal();
        //            l.Visible = true;
        //            l.Text = "<div class='calNumbers' >";
        //            l.Text += "<span title='opened activities' class='openedActivites' onclick='fromYellow()'>" + dt1.Rows[0]["open_activities"].ToString() + "</span>";
        //            //l.Text += "<span title='activities with volunteers' class='withVolunteers' onclick='fromGreen()'>" + dt1.Rows[0]["active_activities"].ToString() + "</span>";
        //            l.Text += "<span title='activities with volunteers' class='withVolunteers' onclick='fromGreen()'>" + "^" + "</span>";
        //            l.Text += "<img onclick='fromOrange()' src='images/Group_95@2x.png' class='VolunteersBtn'/>";
        //            l.Text += "<img onclick='fromBlue()' src='images/Group_94@2x.png' class='FarmersBtn'/>";
        //            l.Text += "<label style='display:none;'>" + e.Day.Date.ToShortDateString() + "</label></div>";
        //            e.Cell.Controls.Add(l);
        //        }
        //    }
        //    else
        //    {
        //        //for (; myIndex < dt.Rows.Count; myIndex++)
        //        //{
        //        Session["index"] = myIndex;
        //        //הרשימה ממוינת לפי תאריכים
        //        DateTime date1 = DateTime.Parse(dt.Rows[myIndex][0].ToString());
        //        if (date1 >= e.Day.Date)
        //        {//אם התאריך גדול או שווה, ז"א- נמצא היום המבוקש או שהוא איננו ברשימה

        //            //אם היום שווה ליום המבוקש - סימון יום זה כיום התחלת פעילות
        //            if (date1.ToShortDateString() == e.Day.Date.ToShortDateString())
        //            {
        //                Literal l = new Literal();
        //                l.Visible = true;
        //                l.Text = "<div class='ActivityStartDiv' >";
        //                //style='position: absolute; bottom: 1px; line-height: 15px; text-align: center; width: 100%;'
        //                l.Text += "<span class='ActivityStart' id='" + dt.Rows[myIndex][1].ToString() + "' >התחלת פעילות</span></div>";
        //                //style='color:Red;font-size:11pt;font-weight:normal;margin-right:2px;margin-left:2px;'
        //                e.Cell.Controls.Add(l);
        //                activitiesCal.SelectedDates.Add(e.Day.Date);

        //                myIndex++;
        //                Session["index"] = myIndex;
        //            }

        //            //var t = Task.Run(() =>
        //            //{
        //            //    //string sql1 = "Select sum(id in (select activity_id from activity_user Where activity_user.deleted_at1 Is Null And date(start_date) <= date(@my_date) And date(end_date) >= date(@my_date) group by activity_id, user_id)) as active_activities, ";
        //            //    string sql1 = "Select sum(1) as active_activities, ";
        //            //    sql1 = sql1 + " sum(1) as open_activities ";
        //            //    sql1 = sql1 + "From activities ";
        //            //    sql1 = sql1 + "Where date(start_date) <= date(@my_date) And date(end_date) >= date(@my_date) ";
        //            //    if (ShowDeleted.Checked == false)
        //            //    {
        //            //        sql1 = sql1 + "And deleted_at IS Null ";
        //            //    }

        //            //    using (MySqlCommand cmdOpenActivities = new MySqlCommand(sql1))
        //            //    {
        //            //        try
        //            //        {
        //            //            cmdOpenActivities.Parameters.AddWithValue("@my_date", e.Day.Date);
        //            //            DataTable dt1 = MySqlDbProvider.GetDataTable1(cmdOpenActivities);

        //            //            if (dt1.Rows.Count > 0)
        //            //            {
        //            //                Literal ll = new Literal();
        //            //                ll.Visible = true;
        //            //                ll.Text = "<div class='calNumbers'>";
        //            //                ll.Text += "<span title='opened activities' class='openedActivites' onclick='fromYellow()'>" + dt1.Rows[0]["open_activities"].ToString() + "</span>";
        //            //                ll.Text += "<span title='activities with volunteers' class='withVolunteers' onclick='fromGreen()'>" + dt1.Rows[0]["active_activities"].ToString() + "</span>";
        //            //                //ll.Text += "<img onclick='fromOrange()' src='images/Group_95@2x.png' style='width:21px; height:21px; background-color: #5e9336;'/>";
        //            //                //ll.Text += "<img onclick='fromBlue()' src='images/Group_94@2x.png' style='width:21px; height:21px;  background-color: #ffc93d;'/>";
        //            //                ll.Text += "<label style='display:none;'>" + e.Day.Date.ToShortDateString() + "</label></div>";

        //            //                e.Cell.Controls.Add(ll);
        //            //            }
        //            //        }
        //            //        catch (Exception ex)
        //            //        {
        //            //            string ss = ex.Message;
        //            //            //throw;
        //            //        }
        //            //    }
        //            //});

        //            //שליפת מספר הפעילויות הפתוחות והפעילות ביום זה
        //            //string sql1 = "Select sum(id in (select distinct activity_id from activity_user Where activity_user.deleted_at1 Is Null And date(start_date) <= date(@my_date) And date(end_date) >= date(@my_date) group by activity_id, user_id)) as active_activities, ";
        //            string sql1 = "Select  ";
        //            sql1 = sql1 + "sum(1) as open_activities ";
        //            sql1 = sql1 + "From activities ";
        //            sql1 = sql1 + "Where date(start_date) <= date(@my_date) And date(end_date) >= date(@my_date) ";
        //            if (ShowDeleted.Checked == false)
        //            {
        //                sql1 = sql1 + "And deleted_at IS Null ";
        //            }
        //            MySqlCommand cmdOpenActivities = new MySqlCommand(sql1);
        //            cmdOpenActivities.Parameters.AddWithValue("@my_date", e.Day.Date);
        //            DataTable dt1 = MySqlDbProvider.GetDataTable(cmdOpenActivities);

        //            if (dt1.Rows.Count > 0)
        //            {
        //                Literal ll = new Literal();
        //                ll.Visible = true;
        //                ll.Text = "<div class='calNumbers'>";
        //                ll.Text += "<span title='opened activities' class='openedActivites' onclick='fromYellow()'>" + dt1.Rows[0]["open_activities"].ToString() + "</span>";
        //                //ll.Text += "<span title='activities with volunteers' class='withVolunteers' onclick='fromGreen()'>" + dt1.Rows[0]["active_activities"].ToString() + "</span>";
        //                ll.Text += "<span title='activities with volunteers' class='withVolunteers' onclick='fromGreen()'>" + "^" + "</span>";
        //                ll.Text += "<img onclick='fromOrange()' src='images/Group_95@2x.png' class='VolunteersBtn'/>";
        //                ll.Text += "<img onclick='fromBlue()' src='images/Group_94@2x.png' class='FarmersBtn'/>";
        //                ll.Text += "<label style='display:none;'>" + e.Day.Date.ToShortDateString() + "</label></div>";

        //                e.Cell.Controls.Add(ll);
        //            }

        //            //break;
        //            //}
        //        }
        //    }


        //}

        //}
        protected void btnActive_Click(object sender, EventArgs e)
        {

            //string myDate = hiddenDate.Value;

            //if (myDate != null && !string.IsNullOrWhiteSpace(myDate))
            //{
            //    DateTime seldate = DateTime.Parse(myDate);//activitiesCal.SelectedDate;

            //    activitiesCal.SelectedDate = seldate;
            //    activitiesCal.VisibleDate = activitiesCal.SelectedDate;

            //    string sqlActivities = "Select activities.*, COALESCE(NULLIF(activities.name,''), activities.name_en) as name_by_lng, ";
            //    sqlActivities = sqlActivities + "(Select count(id) from activity_user Where activity_id = activities.id And activity_user.deleted_at1 Is Null And join_status = 'joined') as num_volunteers, ";
            //    sqlActivities = sqlActivities + "(select name From areas where id = activities.area_id) as area_name, ";
            //    sqlActivities = sqlActivities + "users.full_name, users.profile_image from activities ";
            //    sqlActivities = sqlActivities + "Inner join users On activities.user_id = users.id ";
            //    sqlActivities = sqlActivities + "Where date(start_date) <= date(@start_date) And date(end_date) >= date(@start_date) ";
            //    if (ShowDeleted.Checked == false)
            //    {
            //        sqlActivities = sqlActivities + "And activities.deleted_at IS Null ";
            //    }

            //    //if (areas.SelectedIndex > 0)
            //    //{
            //    //    sqlActivities = sqlActivities + "And activities.country_id = 1 And activities.area_id = " + areas.SelectedIndex;
            //    //}
            //    //if (Session["orderByArea"] != null && int.Parse(Session["orderByArea"].ToString()) == 1)
            //    //{
            //    //    sqlActivities = sqlActivities + "Order By activities.area_id desc ";
            //    //}
            //    //else if (Session["orderByArea"] != null && int.Parse(Session["orderByArea"].ToString()) == 2)
            //    //{
            //    //    sqlActivities = sqlActivities + "Order By activities.area_id asc ";
            //    //}
            //    //sqlActivities = sqlActivities + "And (public = 1 Or (public = 0 And activities.id in (Select activity_id from activity_user group by activity_id)))";

            //    MySqlCommand cmdActivities = new MySqlCommand(sqlActivities);
            //    cmdActivities.Parameters.AddWithValue("@start_date", seldate.ToString("yyyy-MM-dd"));
            //    DataSet ds = MySqlDbProvider.GetDataSet(cmdActivities);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        activitiesList.Visible = true;
            //        VolunteersList.Visible = false;
            //        Repeater1.DataSource = ds;
            //        Repeater1.DataBind();
            //        TotalActivities.InnerText = "";
            //    }
            //    else
            //    {
            //        activitiesList.Visible = false;
            //    }
            //}

        }
        protected void btnVolunteers_Click(object sender, EventArgs e)
        {
            //string myDate = hiddenDate.Value;

            //if (myDate != null && !string.IsNullOrWhiteSpace(myDate))
            //{
            //    DateTime seldate = DateTime.Parse(myDate);//activitiesCal.SelectedDate;

            //    activitiesCal.SelectedDate = seldate;
            //    activitiesCal.VisibleDate = activitiesCal.SelectedDate;

            //    /*select count(id) from activity_user where activity_id = activities.id ";
            //        sql1 = sql1 + "And date(activity_user.start_date) <= date(@my_date) And date(activity_user.end_date) >= date(@my_date)*/

            //    string sqlParticipants = "Select activity_user.user_id as volunteer_id, activity_user.id as activity_user_id, ";
            //    sqlParticipants = sqlParticipants + "date_format(COALESCE(activity_user.start_date, activities.start_date),'%d/%m/%Y') as start_date, ";
            //    sqlParticipants = sqlParticipants + "date_format(COALESCE(activity_user.end_date, activities.end_date),'%d/%m/%Y') as end_date, ";
            //    sqlParticipants = sqlParticipants + "users.full_name, users.profile_image, users.model_type, farmer.full_name as farmer_name, activities.name, activities.id as activity_id ";
            //    sqlParticipants = sqlParticipants + " ,count(activity_user.user_id) as friends_num ";//כמה חברים הצטרפו יחד איתי
            //    sqlParticipants = sqlParticipants + "from activity_user ";
            //    sqlParticipants = sqlParticipants + "Inner join activities ON activity_user.activity_id = activities.id ";
            //    sqlParticipants = sqlParticipants + "Inner join users on activity_user.user_id = users.id ";
            //    sqlParticipants = sqlParticipants + "Inner join users farmer on activities.user_id = farmer.id ";
            //    //sqlParticipants = sqlParticipants + "Where activity_user.activity_id = @activity_id ";
            //    sqlParticipants = sqlParticipants + "Where activity_user.deleted_at1 Is Null And join_status = 'joined' And date(COALESCE(activity_user.start_date, activities.start_date)) <= date(@my_date) And date(COALESCE(activity_user.end_date, activities.end_date)) >= date(@my_date) ";
            //    if (ShowDeleted.Checked == false)
            //    {
            //        sqlParticipants = sqlParticipants + "And activities.deleted_at IS Null ";
            //    }
            //    sqlParticipants = sqlParticipants + "Group By activity_user.user_id, COALESCE(activity_user.start_date, activities.start_date) ";
            //    sqlParticipants = sqlParticipants + "Order By activity_user.activity_id desc ";

            //    MySqlCommand cmdParticipants = new MySqlCommand(sqlParticipants);
            //    cmdParticipants.Parameters.AddWithValue("@activity_id", Request.QueryString["ID"]);
            //    cmdParticipants.Parameters.AddWithValue("@my_date", seldate);

            //    DataSet ds = MySqlDbProvider.GetDataSet(cmdParticipants);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        FarmerName.InnerText = "Farmer Name";
            //        VolunteersList.Visible = true;
            //        activitiesList.Visible = false;
            //        Repeater2.DataSource = ds;
            //        Repeater2.DataBind();

            //        int numVolunteers = 0;
            //        foreach (DataRow row in ds.Tables[0].Rows)
            //        {
            //            numVolunteers += int.Parse(row["friends_num"].ToString());
            //        }
            //        TotalNum.InnerText = "Total volunteers " + numVolunteers.ToString();
            //    }
            //    else
            //    {
            //        VolunteersList.Visible = false;

            //    }
            //}
        }
        protected void btnFarmers_Click(object sender, EventArgs e)
        {

            //            /*select users.full_name, activities.start_date, activities.end_date, activities.name from sundo_new_db.users
            //inner join sundo_new_db.activities On activities.user_id = users.id
            //where activities.id in (select activity_id from sundo_new_db.activity_user where 
            // date(COALESCE(activity_user.start_date, activities.start_date)) <= date(utc_timestamp())
            //and date(COALESCE(activity_user.end_date, activities.end_date)) >= date(utc_timestamp()) and activities.deleted_at is null)
            //-- group by users.id*/

            //            string myDate = hiddenDate.Value;

            //            if (myDate != null && !string.IsNullOrWhiteSpace(myDate))
            //            {
            //                DateTime seldate = DateTime.Parse(myDate);//activitiesCal.SelectedDate;

            //                activitiesCal.SelectedDate = seldate;
            //                activitiesCal.VisibleDate = activitiesCal.SelectedDate;

            //                /*select users.full_name, activities.start_date, activities.end_date, activities.name from sundo_new_db.users
            //inner join sundo_new_db.activities On activities.user_id = users.id
            //where activities.id in (select activity_id from sundo_new_db.activity_user where 
            // date(COALESCE(activity_user.start_date, activities.start_date)) <= date(utc_timestamp())
            //and date(COALESCE(activity_user.end_date, activities.end_date)) >= date(utc_timestamp()) and activities.deleted_at is null)
            //-- group by users.id*/

            //                string sqlParticipants = "Select users.id as farmer_id, ";//count(users.id) as activities_num,
            //                sqlParticipants = sqlParticipants + "date_format(activities.start_date,'%d/%m/%Y') as start_date, ";
            //                sqlParticipants = sqlParticipants + "date_format(activities.end_date,'%d/%m/%Y') as end_date, ";
            //                sqlParticipants = sqlParticipants + "users.full_name, users.profile_image, users.model_type, group_concat(activities.name ) as name, activities.id as activity_id, ";
            //                sqlParticipants = sqlParticipants + "(Select count(a_user.id) as confirm From activity_user a_user ";
            //                sqlParticipants = sqlParticipants + "Inner Join activities act on a_user.activity_id = act.id ";
            //                sqlParticipants = sqlParticipants + "Where a_user.deleted_at1 Is Null And a_user.join_status = 'joined' And date(COALESCE(a_user.start_date, act.start_date)) <= date(@my_date) ";
            //                sqlParticipants = sqlParticipants + "And date(COALESCE(a_user.end_date, act.end_date)) >= date(@my_date) And act.deleted_at IS Null ";
            //                sqlParticipants = sqlParticipants + "and (did_confirm = 1 or confirm_day_before = 1) and act.user_id = activities.user_id) as ConfirmedSum ";
            //                sqlParticipants = sqlParticipants + "from users ";
            //                sqlParticipants = sqlParticipants + "Inner join activities ON users.id = activities.user_id ";
            //                sqlParticipants = sqlParticipants + "Where activities.id in (select activity_id from activity_user ";
            //                //sqlParticipants = sqlParticipants + "Where activity_user.activity_id = @activity_id ";
            //                sqlParticipants = sqlParticipants + "Where activity_user.deleted_at1 Is Null And activity_user.join_status = 'joined' And date(COALESCE(activity_user.start_date, activities.start_date)) <= date(@my_date) And date(COALESCE(activity_user.end_date, activities.end_date)) >= date(@my_date) ";
            //                if (ShowDeleted.Checked == false)
            //                {
            //                    sqlParticipants = sqlParticipants + "And activities.deleted_at IS Null ";
            //                }
            //                sqlParticipants = sqlParticipants + ") Group by users.id ";

            //                MySqlCommand cmdParticipants = new MySqlCommand(sqlParticipants);
            //                //cmdParticipants.Parameters.AddWithValue("@activity_id", Request.QueryString["ID"]);
            //                cmdParticipants.Parameters.AddWithValue("@my_date", seldate);

            //                DataSet ds = MySqlDbProvider.GetDataSet(cmdParticipants);
            //                if (ds.Tables[0].Rows.Count > 0)
            //                {
            //                    int confirmedUsers = 0;
            //                    foreach (DataRow row in ds.Tables[0].Rows)
            //                    {
            //                        confirmedUsers += int.Parse(row["ConfirmedSum"].ToString());
            //                    }
            //                    FarmerName.InnerText = "Confirmed: " + confirmedUsers.ToString();
            //                    VolunteersList.Visible = true;
            //                    activitiesList.Visible = false;
            //                    Repeater2.DataSource = ds;
            //                    Repeater2.DataBind();

            //                    TotalNum.InnerText = "Total farmers: " + ds.Tables[0].Rows.Count.ToString();
            //                }
            //                else
            //                {
            //                    VolunteersList.Visible = false;

            //                }
            //            }
        }

        protected void activitiesCal_SelectionChanged(object sender, EventArgs e)
        {
            //DateTime seldate = activitiesCal.SelectedDate;

            //string sqlActivities = "Select activities.*, COALESCE(NULLIF(activities.name,''), activities.name_en) as name_by_lng, ";
            //sqlActivities = sqlActivities + "(Select count(id) from activity_user Where activity_id = activities.id And activity_user.deleted_at1 Is Null And join_status = 'joined') as num_volunteers, ";
            //sqlActivities = sqlActivities + "(select name From areas where id = activities.area_id) as area_name, ";
            //sqlActivities = sqlActivities + "users.full_name, users.profile_image from activities ";
            //sqlActivities = sqlActivities + "Inner join users On activities.user_id = users.id ";
            //sqlActivities = sqlActivities + "Where date(start_date) = @start_date ";
            //if (ShowDeleted.Checked == false)
            //{
            //    sqlActivities = sqlActivities + "And activities.deleted_at IS Null ";
            //}

            ////if (areas.SelectedIndex > 0)
            ////{
            ////    sqlActivities = sqlActivities + "And activities.country_id = 1 And activities.area_id = " + areas.SelectedIndex;
            ////}
            ////if (Session["orderByArea"] != null && int.Parse(Session["orderByArea"].ToString()) == 1)
            ////{
            ////    sqlActivities = sqlActivities + "Order By activities.area_id desc ";
            ////}
            ////else if (Session["orderByArea"] != null && int.Parse(Session["orderByArea"].ToString()) == 2)
            ////{
            ////    sqlActivities = sqlActivities + "Order By activities.area_id asc ";
            ////}
            ////sqlActivities = sqlActivities + "And (public = 1 Or (public = 0 And activities.id in (Select activity_id from activity_user group by activity_id)))";
            //MySqlCommand cmdActivities = new MySqlCommand(sqlActivities);
            //cmdActivities.Parameters.AddWithValue("@start_date", seldate.ToString("yyyy-MM-dd"));
            //DataSet ds = MySqlDbProvider.GetDataSet(cmdActivities);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    activitiesList.Visible = true;
            //    VolunteersList.Visible = false;
            //    Repeater1.DataSource = ds;
            //    Repeater1.DataBind();

            //    TotalActivities.InnerText = "";
            //}
            //else
            //{
            //    activitiesList.Visible = false;
            //}
        }



    }
}