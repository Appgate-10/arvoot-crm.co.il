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
    public partial class _contacts : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "חפש איש קשר";
        public string StrSrc { get { return strSrc; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {
                Pageinit.CheckManagerPermissions();

                if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) < 4)
                {
                    MoveTo.Visible = true;
                }

                    loadUsers(1, false);
                //loadData();
            }
        }



        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");

            if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) < 4)
            {
                chk.Visible = true;
            }
            else
            {
                chk.Visible = false;
            }

            //var divShowStatus = (HtmlGenericControl)e.Item.FindControl("ShowStatus");
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
        public void loadUsers(int page, bool shouldUpdate)
        {

            int PageNumber = page;
            if (Request.QueryString["Page"] != null)
            {
                PageNumber = int.Parse(Request.QueryString["Page"]);
            }
            int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            int CurrentRow = (PageNumber == 1) ? 0 : (PageSize * (PageNumber - 1));
            long ItemCount = 0;
            string sqlJoin = "";
            string sqlWhere = "";
            SqlCommand cmd = new SqlCommand();
            if (Request.QueryString["Q"] != null)
            {
                if (Request.QueryString["Q"].ToString().Length > 0)
                {
                    sqlWhere = " and( Lead.FirstName like @SrcParam OR Lead.LastName like @SrcParam Or Lead.tz like @SrcParam Or Lead.Phone1 like @SrcParam )";
                }
                strSrc = Request.QueryString["Q"].ToString();
            }
            if (Request.QueryString["filter"] != null)
            {
                if (Request.QueryString["filter"].ToString().Length > 0)
                {
                    if(Request.QueryString["filter"].ToString().Equals("new"))
                        sqlWhere += " and DATEDIFF(DAY,Lead.CreateDate,getdate())<=7 ";
                    else if (Request.QueryString["filter"].ToString().Equals("birthday"))
                        sqlWhere += @" and 
                                    (
                                        MONTH(Lead.DateBirth) = MONTH(GETDATE()) AND 
                                        DAY(Lead.DateBirth) BETWEEN DAY(GETDATE()) AND DAY(DATEADD(DAY, 7, GETDATE()))
                                    )
                                    OR 
                                    (
                                        MONTH(Lead.DateBirth) = MONTH(DATEADD(DAY, 7, GETDATE())) AND
                                        DAY(Lead.DateBirth) BETWEEN 1 AND DAY(DATEADD(DAY, 7, GETDATE()))
                                    ) ";
                                               
                }
            }

            if (HttpContext.Current.Session["AgentLevel"] != null)
            {
                switch (int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()))
                {
                    case 1:
                        sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID ";
                        break;
                    case 2:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID inner join ArvootManagers C on C.ID = B.ParentID ";
                        sqlWhere = " and C.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 3:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type  in (3,6) inner join ArvootManagers B on B.ID = A.ParentID  ";
                        sqlWhere = " and (B.ID = @ID OR A.ID = @ID)";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 6:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type  in (3,6) ";
                        sqlWhere = " and A.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break; 
                    case 4:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type  in (3,6) inner join ArvootManagers B on B.ParentID = A.ParentID  ";
                        sqlWhere = " and B.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;    
                    case 5:
                        sqlJoin = " inner join Offer on Offer.LeadID = Lead.ID and OperatorID = @ID left join ArvootManagers A on A.ID = Lead.AgentID ";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    default:
                        sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID ";
                        break;

                }
            }
            string sql = @" select Lead.ID,Lead.Phone1,Lead.Tz,
                        CONVERT(varchar, Lead.CreateDate, 104) as CreateDate---תאריך המרה לאיש קשר
                            ,Lead.LastName,Lead.FirstName,
							A.FullName as FullNameAgent,
                            CONVERT(varchar, DateBirth, 104) as DateBirth 
                            from Lead "+ sqlJoin  +
                            @"where Lead.IsContact=1 ";

            cmd.CommandText =  sql + sqlWhere;

            string sqlCnt = @"select count(*) from Lead "+ sqlJoin +" where Lead.IsContact=1";
            SqlCommand cmdCount = new SqlCommand(sqlCnt + sqlWhere);
            cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);

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

            if (shouldUpdate == true)
            {
                AddForm.Update();
            }

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

        protected void BtnDetailsContact_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("Contact.aspx?ContactID=" + e.CommandArgument.ToString());
        }

        protected void MoveTo_Click(object sender, EventArgs e)
        {
            FormError_lable.Visible = false;
            MoveContactPopUp.Visible = true;

            SqlCommand cmdAgents = new SqlCommand("SELECT  FullName as AgentName,ID FROM ArvootManagers where Type =6");
            DataSet dsAgents = DbProvider.GetDataSet(cmdAgents);
            AgentList.DataSource = dsAgents;
            AgentList.DataTextField = "AgentName";
            AgentList.DataValueField = "ID";
            AgentList.DataBind();
            AgentList.Items.Insert(0, new ListItem("חפש סוכן", ""));

            UpdatePanel2.Update();

        }

        protected void CloseMovePopUp_Click(object sender, ImageClickEventArgs e)
        {
            MoveContactPopUp.Visible = false;
        }

        protected void ForwardContactsToAgent_Click(object sender, ImageClickEventArgs e)
        {
            if (AgentList.SelectedIndex == 0)
            {
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש לבחור סוכן";
                return;
            };
            string IdsLeads = "";
            List<string> agentsNames = new List<string>();
            List<string> ContactsNames = new List<string>();

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("chk")).Checked)
                {
                    IdsLeads += ((HiddenField)Repeater1.Items[i].FindControl("LeadID")).Value;
                    IdsLeads += ",";
                    agentsNames.Add(((HtmlGenericControl)Repeater1.Items[i].FindControl("AgentName")).InnerText);
                    ContactsNames.Add(((HtmlGenericControl)Repeater1.Items[i].FindControl("ContactFirstName")).InnerText + " " + ((HtmlGenericControl)Repeater1.Items[i].FindControl("ContactLastName")).InnerText);

                }
            }

            if (string.IsNullOrEmpty(IdsLeads))
            {
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש לסמן איש קשר";
                return;
            }

            SqlCommand cmd = new SqlCommand("update Lead set AgentID = @AgentID where ID in(" + IdsLeads.Remove(IdsLeads.Length - 1, 1) + ")");
            cmd.Parameters.AddWithValue("@AgentID", AgentList.SelectedValue);
            if (DbProvider.ExecuteCommand(cmd) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('An error occurred');", true);

            }
            else
            {
                for (int i = 0; i < ContactsNames.Count; i++)
                {
                    SqlCommand cmdHistory = new SqlCommand("INSERT INTO ActivityHistory (AgentID, Details, CreateDate, Show) VALUES (@agentID, @details, GETDATE(), 1)");
                    cmdHistory.Parameters.AddWithValue("@agentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                    cmdHistory.Parameters.AddWithValue("@details", ("איש קשר " + ContactsNames[i] + " הועבר מהסוכן " + agentsNames[i]));
                    DbProvider.ExecuteCommand(cmdHistory);
                }

                Helpers.loadActivityHistoryOnAdd(Page);
            }

            
            MoveContactPopUp.Visible = false;
            loadUsers(1, true);

        }
    }
}