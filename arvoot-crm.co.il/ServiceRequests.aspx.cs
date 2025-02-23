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
    public partial class _serviceRequests : System.Web.UI.Page
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

            string sqlWhere = " where 1=1 ", sql2 = "", sqlJoin = "";
            if (HttpContext.Current.Session["AgentLevel"] != null)
            {
                switch (int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()))
                {

                  
                    case 2:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID left join ArvootManagers C on C.ID = B.ParentID ";
                        sqlWhere += " and (C.ID = @ID OR B.ID = @ID)";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 7:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID left join ArvootManagers C on C.ID = B.ParentID ";
                        sqlWhere += " and (C.ID = (select ParentID from ArvootManagers where ID = @ID) OR B.ID = (select ParentID from ArvootManagers where ID = @ID))";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 3:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID  ";
                        sqlWhere += " and (B.ID = @ID OR A.ID = @ID) ";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);

                        break;
                    case 6:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) ";
                        sqlWhere += " and A.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 4:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ParentID = A.ParentID  ";
                        sqlWhere += " and B.ID = @ID and IsInOperatingQueue = 1";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);

                        break;
                    case 5:
                        sqlWhere += " and OperatorID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        cmdCount.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                       break;
                   

                }
            }

            string sqlServiceRequest = @"select s.ID, Lead.FirstName + ' ' + Lead.LastName as Invoice,Sum,CONVERT(varchar, s.CreateDate, 104)  CreateDate, p.purpose as PurposeName,
                                        (select sum(SumPayment) from ServiceRequestPayment where ServiceRequestID = s.ID and IsApprovedPayment = 1) as paid, 
                                        SumCreditOrDenial, IsApprovedCreditOrDenial
                                        from ServiceRequest s 
                                        left join ServiceRequestPurpose p on s.PurposeID = p.ID 
                                        inner join Offer on Offer.ID = s.OfferID
                                        inner join Lead on Lead.ID = Offer.LeadID" + sqlJoin + sqlWhere;
            cmd.CommandText = sqlServiceRequest;
            DataTable dtServiceRequest = DbProvider.GetDataTable(cmd);

            string sqlCnt = @"select count(*) from ServiceRequest s inner join Offer on Offer.ID = s.OfferID inner join Lead on Lead.ID = Offer.LeadID " + sqlJoin + sqlWhere;
            cmdCount.CommandText = sqlCnt ;
            ItemCount = DbProvider.GetDataTable(cmdCount).Rows.Count;

            if (ItemCount > PageSize)
            {
                string str = "";
                if (PageNumber > 1) { str = str + "<a href=\"ServiceRequests.aspx?Page=" + (PageNumber - 1).ToString() + "\"\" title=\"Back\">&laquo;</a>"; }

                int iRunFrom = ((PageNumber - 4) < 1) ? 1 : (PageNumber - 4);
                int iRunUntil = (int)Math.Ceiling((double)ItemCount / (double)PageSize);
                Session["Page"] = PageNumber;
                int iRun;
                for (iRun = iRunFrom; iRun <= iRunUntil && iRun < (iRunFrom + 10); iRun++)
                {
                    str = str + "<a href=\"ServiceRequests.aspx?Page=" + iRun.ToString() + "\">" + iRun.ToString() + "</a>";
                }
                str = str.Replace(">" + PageNumber.ToString() + "</a>", " class=\"active\">" + PageNumber.ToString() + "</a>");

                if (PageNumber < (iRun - 1)) { str = str + "<a href=\"ServiceRequests.aspx?Page=" + (PageNumber + 1).ToString() + "\"\" title=\"Next\">&raquo;</a>"; }

                PageingDiv.InnerHtml = str;
            };

            Repeater1.DataSource = dtServiceRequest;
            Repeater1.DataBind();

        }
        protected void ExcelExport_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            string sqlWhere = " where 1=1 ", sql2 = "", sqlJoin = " left join ArvootManagers A on A.ID = Lead.AgentID";
            if (HttpContext.Current.Session["AgentLevel"] != null)
            {
                switch (int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()))
                {
                    case 2:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID left join ArvootManagers C on C.ID = B.ParentID ";
                        sqlWhere += " and (C.ID = @ID OR B.ID = @ID)";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 7:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID left join ArvootManagers C on C.ID = B.ParentID ";
                        sqlWhere += " and (C.ID = (select ParentID from ArvootManagers where ID = @ID) OR B.ID = (select ParentID from ArvootManagers where ID = @ID))";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 3:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ID = A.ParentID  ";
                        sqlWhere += " and (B.ID = @ID OR A.ID = @ID) ";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 6:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) ";
                        sqlWhere += " and A.ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 4:
                        sqlJoin = " inner join ArvootManagers A on A.ID = Lead.AgentID and A.Type in (3,6) inner join ArvootManagers B on B.ParentID = A.ParentID  ";
                        sqlWhere += " and B.ID = @ID and IsInOperatingQueue = 1";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                    case 5:
                        sqlWhere += " and OperatorID = @ID";
                        cmd.Parameters.AddWithValue("@ID", HttpContext.Current.Session["AgentID"]);
                        break;
                }
            }

            string sqlServiceRequest = @"select CONVERT(varchar, s.CreateDate, 104) CreateDate, Lead.FirstName + ' ' + Lead.LastName as Invoice,Lead.tz,A.FullName, convert(varchar,Sum) as Sum,
                                         convert(varchar,(iif(payment.IsApprovedPayment = 1,payment.SumPayment,0) + 
                                         iif(IsApprovedCreditOrDenial = 1 and SumCreditOrDenial is not null and SumCreditOrDenial != '',SumCreditOrDenial, 0 ) )) 										 
                                         as ApprovedSum, convert(varchar, payment.DatePayment,104) as PaymentDate,
                                         convert(varchar,Sum - (isnull((select sum(SumPayment) from ServiceRequestPayment where ServiceRequestID = s.ID and IsApprovedPayment = 1),0) + 
										 convert(varchar,iif(IsApprovedCreditOrDenial = 1 and SumCreditOrDenial is not null and SumCreditOrDenial != '',SumCreditOrDenial, 0 ) ))) as paid, 
                                         p.purpose as PurposeName
                                         from ServiceRequest s  left join ServiceRequestPayment payment on payment.ServiceRequestID = s.ID
                                         left join ServiceRequestPurpose p on s.PurposeID = p.ID 
                                         inner join Offer on Offer.ID = s.OfferID
                                         inner join Lead on Lead.ID = Offer.LeadID" + sqlJoin + sqlWhere;
            cmd.CommandText = sqlServiceRequest;
            
            try
            {
                cmd.Parameters.AddWithValue("@SrcParam", "%" + Request.QueryString["Q"].ToString() + "%");
            }
            catch (Exception) { }

            DataSet ds = DbProvider.GetDataSet(cmd);
            DataRow dataRow = ds.Tables[0].NewRow();
            dataRow[0] = "תאריך הקמה";
            dataRow[1] = "שם לקוח";
            dataRow[2] = "ת.ז.";
            dataRow[3] = "בעלים";
            dataRow[4] = "סכום גבייה";
            dataRow[5] = "סכום שאושר";
            dataRow[6] = "תאריך תשלום";
            dataRow[7] = "יתרת גבייה";
            dataRow[8] = "מטרת הגבייה";

            ds.Tables[0].Rows.InsertAt(dataRow, 0);
            bool didSuccess = CreateSimpleExcelFile.CreateExcelDocument(ds, "ServiceRequests.xlsx", Response);
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + didSuccess + " ');", true);


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

        protected void BtnDetailsServiceReq_Command(object sender, CommandEventArgs e)
        {
           Response.Redirect("ServiceRequestEdit.aspx?ServiceRequestID=" + e.CommandArgument.ToString());
        }
    }
}