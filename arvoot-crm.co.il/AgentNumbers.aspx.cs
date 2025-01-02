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
    public partial class _agentNumbers: System.Web.UI.Page
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
            int PageSize = 4;//int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            int CurrentRow = (PageNumber == 1) ? 0 : (PageSize * (PageNumber - 1));
            long ItemCount = 0;
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdCount = new SqlCommand();

            string sqlJoin = "", sqlWhere = "";
            string sql = @"select Am.CompanyName,S.Text SourceName,An.AgentNumber from AgentNumbers An inner join ArvootManagers Am on An.CompanyManagerId = Am.ID
                            inner join SourceLoanOrInsurance S on An.SourceID = S.ID";

            if (HttpContext.Current.Session["AgentLevel"] != null)
            {
                switch (int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()))
                {
                    /*inner join ArvootManagers A on A.ID = An.CompanyManagerId
							inner join ArvootManagers B on B.ParentID = A.ID
							inner join ArvootManagers C on C.ParentID = B.ID
							where C.ID = 6*/
                    case 1:
                        break;
                    case 2:
                        sqlJoin = " inner join ArvootManagers A on A.ID = An.CompanyManagerId ";
                        sqlWhere = "Where A.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 7:
                        sqlJoin = " inner join ArvootManagers A on A.ParentID = An.CompanyManagerId ";
                        sqlWhere = "Where A.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 3:
                        sqlJoin = " inner join ArvootManagers A on A.ID = An.CompanyManagerId inner join ArvootManagers B on B.ParentID = A.ID ";
                        sqlWhere = "Where (B.ID = @ID OR A.ID = @ID) ";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    default:
                        sqlJoin = " inner join ArvootManagers A on A.ID = An.CompanyManagerId inner join ArvootManagers B on B.ParentID = A.ID inner join ArvootManagers C on C.ParentID = B.ID ";
                        sqlWhere = "Where C.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;

                }
            }

            cmd.CommandText = sql + sqlJoin + sqlWhere;
            DataTable dtServiceRequest = DbProvider.GetDataTable(cmd);

            string sqlCnt = @"select count(*) from AgentNumbers An inner join ArvootManagers Am on An.CompanyManagerId = Am.ID
                            inner join SourceLoanOrInsurance S on An.SourceID = S.ID" ;
            cmdCount.CommandText = sqlCnt + sqlJoin + sqlWhere;
            ItemCount = DbProvider.GetDataTable(cmdCount).Rows.Count;

            if (ItemCount > PageSize)
            {
                string str = "";
                if (PageNumber > 1) { str = str + "<a href=\"AgentNumbers.aspx?Page=" + (PageNumber - 1).ToString() + "\"\" title=\"Back\">&laquo;</a>"; }

                int iRunFrom = ((PageNumber - 4) < 1) ? 1 : (PageNumber - 4);
                int iRunUntil = (int)Math.Ceiling((double)ItemCount / (double)PageSize);
                Session["Page"] = PageNumber;
                int iRun;
                for (iRun = iRunFrom; iRun <= iRunUntil && iRun < (iRunFrom + 10); iRun++)
                {
                    str = str + "<a href=\"AgentNumbers.aspx?Page=" + iRun.ToString() + "\">" + iRun.ToString() + "</a>";
                }
                str = str.Replace(">" + PageNumber.ToString() + "</a>", " class=\"active\">" + PageNumber.ToString() + "</a>");

                if (PageNumber < (iRun - 1)) { str = str + "<a href=\"AgentNumbers.aspx?Page=" + (PageNumber + 1).ToString() + "\"\" title=\"Next\">&raquo;</a>"; }

                PageingDiv.InnerHtml = str;
            };

            Repeater1.DataSource = dtServiceRequest;
            Repeater1.DataBind();

        }


    }
}