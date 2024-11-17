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
    public partial class _policies : System.Web.UI.Page
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


                loadUsers(1);
                //loadData();
            }
        }



        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
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
        public void loadUsers(int page)
        {

            int PageNumber = page;
            if (Request.QueryString["Page"] != null)
            {
                PageNumber = int.Parse(Request.QueryString["Page"]);
            }
            int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            int CurrentRow = (PageNumber == 1) ? 0 : (PageSize * (PageNumber - 1));
            long ItemCount = 0;
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdCount = new SqlCommand();

           string sqlWhere = "", sql2 = "", sqlJoin = "";
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
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID inner join ArvootManagers C on C.ID = B.ParentID ";
                        sqlWhere = " and C.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 3:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID  ";
                        sqlWhere = " and (B.ID = @ID OR A.ID = @ID) ";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);

                        break;
                    case 6:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) ";
                        sqlWhere = " and A.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 4:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ParentID = A.ParentID  ";
                        sqlWhere = " and B.ID = @ID and IsInOperatingQueue = 1";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);

                        break;
                    case 5:
                        sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID ";
                        sqlWhere = " and OperatorID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);

                        break;
                    default:
                        sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6)";
                        break;

                }
            }

            //Heni 28.10.24 -  where  OfferType.ID  in(1,2,3,12) -הסטטוסים מסוג ביטוח מוצגים בפוליסות 

            string sql = @"SELECT Offer.ID, Offer.CreateDate, OfferType.Name as OfferType, A.FullName as FullNameAgent ,StatusOffer.Status as StatusOffer
                           from Offer
                           left join OfferType on OfferType.ID = Offer.OfferTypeID
                           left join StatusOffer on StatusOffer.ID = Offer.StatusOfferID 
                           left join Lead on Lead.ID = Offer.LeadID" 
                            + sqlJoin + " where  OfferType.ID in(1,2,3,13)";



            cmd.CommandText =sql + sqlWhere;

            string sqlCnt = @"select count(*) from Offer left join Lead on Lead.ID = Offer.LeadID " + sqlJoin + " where  OfferTypeID in(1,2,3,13)";
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


    }
}