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
    public partial class _business : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "חפש קובץ";
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

        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        protected void CopyLid_Click(object sender, ImageClickEventArgs e)
        {
        }
        protected void ShereLid_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void DeleteLid_Click(object sender, ImageClickEventArgs e)
        {
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
        public void loadData()
        {
            //int PageNumber = 1;

            //string sql = @"SELECT IDUser, Users.ImageFile, CONCAT(Users.userFirstName, ' ', Users.userLastName) AS userFullName, userEmail, userPhone, JoinDate, Users.Show, 
            //    (SELECT count(*) FROM tbl_order WHERE status = 2 AND BusinessStatus = 6 AND UserID = Users.IDUser) AS PurchasesNum FROM Users ";

            //SqlCommand cmd = new SqlCommand(sql);

            //DataSet ds = DbProvider.GetDataSet(cmd);

            //Repeater1.DataSource = ds;
            //Repeater1.DataBind();


            //string sql2 = @"SELECT IDUser, Users.ImageFile, CONCAT(Users.userFirstName, ' ', Users.userLastName) AS userFullName, userEmail, userPhone, JoinDate, Users.Show, 
            //    (SELECT count(*) FROM tbl_order WHERE status = 2 AND BusinessStatus = 6 AND UserID = Users.IDUser) AS PurchasesNum FROM Users ";

            //SqlCommand cmd2 = new SqlCommand(sql2);

            //DataSet ds2 = DbProvider.GetDataSet(cmd2);

            //Repeater2.DataSource = ds2;
            //Repeater2.DataBind();


            //string sql3 = @"SELECT IDUser, Users.ImageFile, CONCAT(Users.userFirstName, ' ', Users.userLastName) AS userFullName, userEmail, userPhone, JoinDate, Users.Show, 
            //    (SELECT count(*) FROM tbl_order WHERE status = 2 AND BusinessStatus = 6 AND UserID = Users.IDUser) AS PurchasesNum FROM Users ";

            //SqlCommand cmd3 = new SqlCommand(sql3);

            //DataSet ds3 = DbProvider.GetDataSet(cmd3);

            //Repeater3.DataSource = ds3;
            //Repeater3.DataBind();
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





    }
}