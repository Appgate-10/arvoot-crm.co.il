using ControlPanel.HelpersFunctions;
using System;
using System.Configuration;
//using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace ControlPanel
{
    public partial class _offers : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "חפש איש קשר";
        public string StrSrc { get { return strSrc; } }
        private string strDate1 = "";
        public string StrDate1 { get { return strDate1; } }
        private string strDate2 = "";
        public string StrDate2 { get { return strDate2; } }
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {
                Pageinit.CheckManagerPermissions();
                SqlCommand cmdOperators = new SqlCommand(@"select FullName as OperatorName,ID from ArvootManagers where Show = 1 and (Type = 5 and ParentID in(
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
                OperatorsList.Items.Insert(0, new ListItem("מתפעלת", ""));

                SqlCommand cmdStatus = new SqlCommand("SELECT * FROM StatusOffer where ID != 9 and ID != 10 ");
                DataSet dsStatus = DbProvider.GetDataSet(cmdStatus);
                StatusList.DataSource = dsStatus;
                StatusList.DataTextField = "Status";
                StatusList.DataValueField = "ID";
                StatusList.DataBind();
                StatusList.Items.Insert(0, new ListItem("סטטוס", ""));


                SqlCommand cmdAgents = new SqlCommand(" SELECT  FullName as AgentName,ID FROM ArvootManagers where ParentID = (select ParentID FROM ArvootManagers where ID = @ID) and Type in (3,6)");
                cmdAgents.Parameters.AddWithValue("@ID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                DataSet dsAgents = DbProvider.GetDataSet(cmdAgents);
                AgentList.DataSource = dsAgents;
                AgentList.DataTextField = "AgentName";
                AgentList.DataValueField = "ID";
                AgentList.DataBind();
                AgentList.Items.Insert(0, new ListItem("בעלים", ""));

                loadUsers(1);
                //loadData();
            }
        }
        protected void OperatorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selectedOperator"] = OperatorsList.SelectedValue.ToString();

            loadUsers(1);
        } 
        
        protected void StatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selectedStatus"] = StatusList.SelectedValue.ToString();

            loadUsers(1);
        }     
        protected void AgentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selectedAgent"] = AgentList.SelectedValue.ToString();

            loadUsers(1);
        }



        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var divStatus = (HtmlGenericControl)e.Item.FindControl("statusVal");
            var divOperator = (HtmlGenericControl)e.Item.FindControl("operatorVal");

            if (int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) == 4)
            {
                divStatus.Style.Add("width", "10%");
                divOperator.Style.Remove("display");
            }

            //ImageButton btnSuspensionBU = (ImageButton)e.Item.FindControl("SuspensionBU");
            //ImageButton btnActivatingBU = (ImageButton)e.Item.FindControl("ActivatingBU");

            //if (int.Parse(DataBinder.Eval(e.Item.DataItem, "Show").ToString()) == 1)
            //{
            //    divShowStatus.Attributes.Add("class", "ListDivShowStatusGreen");
            //    btnSuspensionBU.Visible = true;
            //    btnActivatingBU.Visible = false;
            //}
            //else
            //{
            //    divShowStatus.Attributes.Add("class", "ListDivShowStatusRed");
            //    btnSuspensionBU.Visible = false;
            //    btnActivatingBU.Visible = true;
            //}
        }
        //public void loadUsers(int page)
        //{
        //    int PageNumber = 1;
        //    int PageNumber = page;
        //    if (Request.QueryString["Page"] != null)
        //    {
        //        PageNumber = int.Parse(Request.QueryString["Page"]);
        //    }
        //    int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
        //    int CurrentRow = (PageNumber == 1) ? 0 : (PageSize * (PageNumber - 1));
        //    long ItemCount = 0;

        //    string sql = @"SELECT IDUser, Users.ImageFile, CONCAT(Users.userFirstName, ' ', Users.userLastName) AS userFullName, userEmail, userPhone,CONVERT(varchar, JoinDate, 104)  JoinDate, Users.Show, 
        //        (SELECT count(*) FROM tbl_order WHERE status = 2 AND BusinessStatus = 6 AND UserID = Users.IDUser) AS PurchasesNum FROM Users ";


        //    if (Request.QueryString["Q"] != null)
        //    {
        //        Session["search"] = Request.QueryString["Q"];
        //        if (Request.QueryString["Q"].ToString().Length > 0)
        //        {
        //            sql = sql + "Where userFirstName like @SrcParam Or userLastName like @SrcParam Or userEmail like @SrcParam OR userPhone like @SrcParam ";
        //        }
        //        strSrc = Request.QueryString["Q"].ToString();
        //    }
        //    string sqlOrder = " Order by Users.Show Desc, IDUser Asc OFFSET  " + CurrentRow.ToString() + "  ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY ";

        //    //-- ניהול Paging
        //    string sqlCnt = "Select Count(IDUser) FROM Users ";
        //    if (Request.QueryString["Q"] != null)
        //    {
        //        Session["search"] = Request.QueryString["Q"];
        //        if (Request.QueryString["Q"].ToString().Length > 0)
        //        {
        //            sqlCnt = sqlCnt + "Where userFirstName like @SrcParam Or userLastName like @SrcParam Or userEmail like @SrcParam OR userPhone like @SrcParam ";
        //        }
        //    }
        //    SqlCommand cmdCount = new SqlCommand(sqlCnt);
        //    try { cmdCount.Parameters.AddWithValue("@SrcParam", "%" + Request.QueryString["Q"].ToString() + "%"); }
        //    catch (Exception) { }
        //    ItemCount = DbProvider.GetOneParamValueLong(cmdCount);
        //    if (ItemCount > PageSize)
        //    {
        //        string str = "", str1 = "";
        //        if (PageNumber > 1) { str = str + "<a href=\"Users.aspx?Page=" + (PageNumber - 1).ToString() + str1 + "\"\" title=\"Back\">&laquo;</a>"; }

        //        int iRunFrom = ((PageNumber - 4) < 1) ? 1 : (PageNumber - 4);
        //        int iRunUntil = (int)Math.Ceiling((double)ItemCount / (double)PageSize);
        //        Session["Page"] = PageNumber;
        //        int iRun;
        //        for (iRun = iRunFrom; iRun <= iRunUntil && iRun < (iRunFrom + 10); iRun++)
        //        {
        //            str = str + "<a href=\"Users.aspx?Page=" + iRun.ToString() + str1 + "\">" + iRun.ToString() + "</a>";
        //        }
        //        str = str.Replace(">" + PageNumber.ToString() + "</a>", " class=\"active\">" + PageNumber.ToString() + "</a>");

        //        if (PageNumber < (iRun - 1)) { str = str + "<a href=\"Users.aspx?Page=" + (PageNumber + 1).ToString() + str1 + "\"\" title=\"Next\">&raquo;</a>"; }

        //        PageingDiv.InnerHtml = str;
        //    }
        //    else
        //    {
        //        PageingDiv.InnerHtml = "";
        //    }

        //    SqlCommand cmd = new SqlCommand(sql + sqlOrder);

        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@SrcParam", "%" + Request.QueryString["Q"].ToString() + "%");
        //    }
        //    catch (Exception) { }
        //    DataSet ds = DbProvider.GetDataSet(cmd);

        //    Repeater1.DataSource = ds;
        //    Repeater1.DataBind();
        //}
        public void loadUsers(int page)
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdCount = new SqlCommand();
            string sqlWhere = "", sql2 = "", sqlJoin = "";




            int PageNumber = page;
            if (Request.QueryString["Page"] != null)
            {
                PageNumber = int.Parse(Request.QueryString["Page"]);
            }
            int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            int CurrentRow = (PageNumber == 1) ? 0 : (PageSize * (PageNumber - 1));
            long ItemCount = 0;
           

            if (Request.QueryString["Q"] != null)
            {
                if (Request.QueryString["Q"].ToString().Length > 0)
                {
                    sqlWhere = " and( Lead.FirstName like @SrcParam OR Lead.LastName like @SrcParam Or Lead.tz like @SrcParam Or Lead.Phone1 like @SrcParam )";
                }
                strSrc = Request.QueryString["Q"].ToString();
            }
            if (HttpContext.Current.Session["AgentLevel"] != null)
            {
                switch (int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()))
                {

                    case 1:
                        sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) ";
                        break;
                    case 2:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID left join ArvootManagers C on C.ID = B.ParentID ";
                        sqlWhere = " and (C.ID = @ID OR B.ID = @ID)";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 3:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID  ";
                        sqlWhere = " and (B.ID = @ID OR A.ID = @ID) and StatusOffer.ID != 9 and  StatusOffer.ID != 10 ";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);

                        break;
                    case 6:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) ";
                        sqlWhere = " and A.ID = @ID and Offer.IsInOperatingQueue = 0 and Offer.OperatorID is null and StatusOffer.ID != 9 and  StatusOffer.ID != 10";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 4:

                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID inner join ArvootManagers C on B.ParentID = C.ID  ";
                        sqlWhere = " and C.ID = ( select ParentID from ArvootManagers where ID = (select ParentID  from ArvootManagers where ID = @ID )) and (IsInOperatingQueue = 1 or OperatorID is not null) and StatusOffer.ID != 9 and  StatusOffer.ID != 10";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        status.Style.Add("width", "10%");
                        operatoring.Style.Remove("display");
                        break;
                    case 5:
                        sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID ";
                        sqlWhere = " and OperatorID = @ID and StatusOffer.ID != 9 and  StatusOffer.ID != 10";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);

                        break;
                    default:
                        sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6)";
                        break;

                }
            }

            string sql = @"SELECT Offer.ID, Offer.CreateDate, OfferType.Name as OfferType, A.FullName as FullNameAgent ,StatusOffer.Status as StatusOffer, operators.FullName as OperatorName
                           , Lead.FirstName + ' ' + Lead.LastName as FullName, Lead.Tz
                           from Offer
                           left join OfferType on OfferType.ID = Offer.OfferTypeID
                           left join StatusOffer on StatusOffer.ID = Offer.StatusOfferID 
                           left join Lead on Lead.ID = Offer.LeadID     
                           left join ArvootManagers operators on operators.ID = Offer.OperatorID" +
                           sqlJoin + " where  OfferType.ID not in(1,2,3,13)";


            try
            {
                if (int.Parse(Session["selectedOperator"].ToString()) > 1)
                {

                    OperatorsList.SelectedValue = Session["selectedOperator"].ToString();

                    sqlWhere += " and Offer.OperatorID = @operatorID";
                    cmd.Parameters.AddWithValue("@operatorID", Session["selectedOperator"].ToString());
                    cmdCount.Parameters.AddWithValue("@operatorID", Session["selectedOperator"].ToString());
                }
            }
            catch (Exception) { }   
            
            try
            {
                if (int.Parse(Session["selectedAgent"].ToString()) > 1)
                {

                    AgentList.SelectedValue = Session["selectedAgent"].ToString();

                    sqlWhere += " and Lead.AgentID = @agentID";
                    cmd.Parameters.AddWithValue("@agentID", Session["selectedAgent"].ToString());
                    cmdCount.Parameters.AddWithValue("@agentID", Session["selectedAgent"].ToString());
                }
            }
            catch (Exception) { }    
            
            try
            {
                if (int.Parse(Session["selectedStatus"].ToString()) > 0)
                {

                    StatusList.SelectedValue = Session["selectedStatus"].ToString();

                    sqlWhere += " and Offer.StatusOfferID  = @StatusOfferID";
                    cmd.Parameters.AddWithValue("@StatusOfferID", Session["selectedStatus"].ToString());
                    cmdCount.Parameters.AddWithValue("@StatusOfferID", Session["selectedStatus"].ToString());
                }
            }
            catch (Exception) { }

            if (!string.IsNullOrEmpty(Request.QueryString["FromDate"]))
            {
                fromDate = ConvertToDateTime(Request.QueryString["FromDate"]);

                strDate1 = Request.QueryString["FromDate"].ToString();
                FromDate.Text = strDate1;
                sqlWhere = sqlWhere + " and Offer.CreateDate >= @FromDate ";
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmdCount.Parameters.AddWithValue("@FromDate", fromDate);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["ToDate"]))
            {
                toDate = ConvertToDateTime(Request.QueryString["ToDate"]);


                strDate2 = Request.QueryString["ToDate"].ToString();
                ToDate.Text = strDate2;
                sqlWhere = sqlWhere + " and Offer.CreateDate <= @ToDate ";
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                cmdCount.Parameters.AddWithValue("@ToDate", toDate);
            }

            cmd.CommandText = sql + sqlWhere;

            string sqlCnt = @"select count(*)  from Offer  left join Lead on Lead.ID = Offer.LeadID left join StatusOffer on StatusOffer.ID = Offer.StatusOfferID " + sqlJoin + " where OfferTypeID not in(1,2,3,13)";
            cmdCount.CommandText = sqlCnt + sqlWhere;

            try
            {
                cmdCount.Parameters.AddWithValue("@SrcParam", "%" + Request.QueryString["Q"].ToString() + "%");
            }
            catch (Exception) { }

            ItemCount = DbProvider.GetDataTable(cmdCount).Rows.Count;

            if (ItemCount > PageSize)
            {
                string str = "";
                if (PageNumber > 1) { str = str + "<a href=\"Contacts.aspx?Page=" + (PageNumber - 1).ToString() + "\"\" title=\"Back\">&laquo;</a>"; }

                int iRunFrom = ((PageNumber - 4) < 1) ? 1 : (PageNumber - 4);
                int iRunUntil = (int)Math.Ceiling((double)ItemCount / (double)PageSize);
                Session["Page"] = PageNumber;
                int iRun;
                for (iRun = iRunFrom; iRun <= iRunUntil && iRun < (iRunFrom + 10); iRun++)
                {
                    str = str + "<a href=\"Contacts.aspx?Page=" + iRun.ToString() + "\">" + iRun.ToString() + "</a>";
                }
                str = str.Replace(">" + PageNumber.ToString() + "</a>", " class=\"active\">" + PageNumber.ToString() + "</a>");

                if (PageNumber < (iRun - 1)) { str = str + "<a href=\"Contacts.aspx?Page=" + (PageNumber + 1).ToString() + "\"\" title=\"Next\">&raquo;</a>"; }

                PageingDiv.InnerHtml = str;
            };

            try
            {
                cmd.Parameters.AddWithValue("@SrcParam", "%" + Request.QueryString["Q"].ToString() + "%");
            }
            catch (Exception) { }

            DataSet ds = DbProvider.GetDataSet(cmd);

            Repeater1.DataSource = ds;
            Repeater1.DataBind();

        }
        protected void SortBtn_Click(object sender, EventArgs e)
        {
            ////??
            //System.Web.HttpContext.Current.Response.Redirect("AdvertisementConfirm.aspx?FromDate=" + FromDate.Text + "&ToDate=" + ToDate.Text);
            // אם חפשתי שם של עסק (קיו בחיפוש ואז מוסיף קיו ליורל)ורק אחרכ אני מסננת לפי תאריך אז אני צריכה לחפש גם לפי שם וגם לפי תאריך
            //אבל אם חפשתי לפי תאריך וגם לפי שם אז אני מחפשת רק לפי תאריך

            System.Web.HttpContext.Current.Response.Redirect("Offers.aspx?FromDate=" + FromDate.Text + "&ToDate=" + ToDate.Text);



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

        //protected void BtnDetailsContact_Command(object sender, CommandEventArgs e)
        //{
        //    Response.Redirect("Contact.aspx?ContactID=" + e.CommandArgument.ToString());
        //}

        protected void BtnDetailsOffer_Command(object sender, CommandEventArgs e)
        {
            System.Web.HttpContext.Current.Response.Redirect("OfferEdit.aspx?OfferID=" + e.CommandArgument.ToString());

        }

        public DateTime? ConvertToDateTime(string str)
        {
            try
            {
                if (str.Length == 10)
                    return DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}