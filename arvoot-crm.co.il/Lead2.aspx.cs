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
using System.Globalization;

namespace ControlPanel
{
    public partial class _lead2 : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "חפש קובץ";
        public string StrSrc { get { return strSrc; } }
        public string ListPageUrl = "Leads.aspx";


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

        protected void CopyLid_Click(object sender, ImageClickEventArgs e)
        {
        }
        protected void ExportNewContact_Click(object sender, ImageClickEventArgs e)
        {
            bool success = funcSave(sender, e);
            if (!success)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
               

            }
            else
            {
                Response.Redirect(ListPageUrl);
            }
        }

        public bool funcSave(object sender, EventArgs e)
        {
            int ErrorCount = 0;
            ExportNewContact_lable.Visible = false;

            //if (BusinessName.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין שם עסק";
            //}
            //else if (ListTypes.SelectedIndex == 0)
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש לבחור את סוג העסק";
            //}

            if (ErrorCount == 0)
            {
                string sql = @"  
                                 insert into  Contact  select FirstName,DateBirth,Gender,tz,IssuanceDateTz,BusinessGrossSalary,Phone
		                                               ,Email,Note,Address,AgentID,GETDATE()
                                                       from Lead
									                    where ID=@LeadID";



                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@LeadID", Request.QueryString["LeadID"]);

                if (DbProvider.ExecuteCommand(cmd) > 0)
                {
                    //מחיקה של ליד
                    SqlCommand cmdDelete = new SqlCommand("delete from Lead where id=@LeadID");
                    cmdDelete.Parameters.AddWithValue("@LeadID", Request.QueryString["LeadID"]);
                    if (DbProvider.ExecuteCommand(cmdDelete) <= 0)
                    {
                        ExportNewContact_lable.Text = "* התרחשה שגיאה";
                        ExportNewContact_lable.Visible = true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    ExportNewContact_lable.Text = "התרחשה שגיאה";
                    ExportNewContact_lable.Visible = true;
                }
            }
            return false;
        }



        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FuncRepeater(sender, e);
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FuncRepeater(sender, e);
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
        protected void btnDateFilter_Click(object sender, EventArgs e)
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
            string sqlLead = @"
                           select firstname,LastName,Gender,Year(GetDate())-Year(DateBirth) Age,CONVERT(varchar,DateBirth, 104) DateBirth,Address,Tz, CONVERT(varchar,IssuanceDateTz, 104) IssuanceDateTz,IsValidIssuanceDateTz
                           ,IsValidBdi,Phone,Email,SourceLead,InterestedIn,TrackingTime,Note
                           ,FirstStatusLead.Status FirstStatus,SecondStatusLead.Status SecondStatus
                           ,BusinessName,BusinessSeniority,BusinessProfession,BusinessCity,BusinessEmail,BusinessPhone,BusinessGrossSalary,BusinessLineBusiness
                           ,PartnerEmail,PartnerPhone,PartnerCity,PartnerStreet,PartnerBuildingFloorApartment,PartnerNumMailbox,PartnerFamilyStatus,PartnerLineBusiness
                           PartnerName,PartnerGrossSalary,PartnerAge,PartnerSeniority
                           from Lead
                           inner join FirstStatusLead on Lead.FirstStatusLeadID=FirstStatusesLead.ID
                           left join SecondStatusLead on Lead.SecondStatusLeadID=SecondStatusesLead.ID
                           where Lead.ID=@LeadID";
            SqlCommand cmdLead = new SqlCommand(sqlLead);

            cmdLead.Parameters.AddWithValue("@LeadID", Request.QueryString["LeadID"]);

            DataTable dtLead = DbProvider.GetDataTable(cmdLead);
            if (dtLead.Rows.Count > 0)
            {
                DateTime dateOfBirth = Convert.ToDateTime(dtLead.Rows[0]["DateBirth"]);
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - dateOfBirth.Year;

                FirstName.Value = dtLead.Rows[0]["FirstName"].ToString();
                LastName.Value = dtLead.Rows[0]["LastName"].ToString();
                RadioButttonGender.SelectedValue = dtLead.Rows[0]["Gender"].ToString();
                if (dtLead.Rows[0]["Gender"].ToString() == "other") { BtnGender.Text = "אחר"; }
                else if (dtLead.Rows[0]["Gender"].ToString() == "male") { BtnGender.Text = "זכר"; }
                else if (dtLead.Rows[0]["Gender"].ToString() == "female") { BtnGender.Text = "נקבה"; }
                Age.InnerText = age.ToString();
                DateBirth.Value = (dateOfBirth).ToString("yyyy-MM-dd");
                //DateBirth.Value =DateTime.Parse(dtLead.Rows[0]["DateBirth"].ToString()).ToString("dd/mm/yyyy");
                Address.Value = dtLead.Rows[0]["Address"].ToString();

                Tz.Value = dtLead.Rows[0]["Tz"].ToString();
                IssuanceDateTz.Value = (Convert.ToDateTime(dtLead.Rows[0]["IssuanceDateTz"])).ToString("yyyy-MM-dd");
                Phone.Value = dtLead.Rows[0]["Phone"].ToString();
                Email.Value = dtLead.Rows[0]["Email"].ToString();
                //Phone.Text = dtLead.Rows[0]["Phone"].ToString();
                //Email.Text = dtLead.Rows[0]["Email"].ToString();
                SourceLead.Text = dtLead.Rows[0]["SourceLead"].ToString();
                InterestedIn.Value = dtLead.Rows[0]["InterestedIn"].ToString();
                TrackingTime.Value = Convert.ToDateTime(dtLead.Rows[0]["TrackingTime"]).ToString("yyyy-MM-ddTHH:mm:ss");
                Note.Value = dtLead.Rows[0]["Note"].ToString();
                BusinessName.Value = dtLead.Rows[0]["BusinessName"].ToString();
                BusinessSeniority.Value = dtLead.Rows[0]["BusinessSeniority"].ToString();
                BusinessEmail.Value = dtLead.Rows[0]["BusinessEmail"].ToString();
                BusinessPhone.Value = dtLead.Rows[0]["BusinessPhone"].ToString();
                BusinessProfession.Text = dtLead.Rows[0]["BusinessProfession"].ToString();
                BusinessCity.Value = dtLead.Rows[0]["BusinessCity"].ToString();
                BusinessGrossSalary.Value = dtLead.Rows[0]["BusinessGrossSalary"].ToString();
                BusinessLineBusiness.Text = dtLead.Rows[0]["BusinessLineBusiness"].ToString();
                PartnerEmail.Value = dtLead.Rows[0]["PartnerEmail"].ToString();
                PartnerPhone.Value = dtLead.Rows[0]["PartnerPhone"].ToString();
                PartnerCity.Value = dtLead.Rows[0]["PartnerCity"].ToString();
                PartnerStreet.Value = dtLead.Rows[0]["PartnerStreet"].ToString();
                PartnerBuildingFloorApartment.Value = dtLead.Rows[0]["PartnerBuildingFloorApartment"].ToString();
                PartnerNumMailbox.Value = dtLead.Rows[0]["PartnerNumMailbox"].ToString();
                //PartnerLineBusiness.Text = dtLead.Rows[0]["PartnerLineBusiness"].ToString();
                PartnerFamilyStatus.Text = dtLead.Rows[0]["PartnerFamilyStatus"].ToString();
                PartnerName.Text = dtLead.Rows[0]["PartnerName"].ToString();
                PartnerAge.Text = dtLead.Rows[0]["PartnerAge"].ToString();
                PartnerSeniority.Text = dtLead.Rows[0]["PartnerSeniority"].ToString();
            }


            string sql = @"   select Text,TaskStatuses.Status, convert(VARCHAR(5), CreationDate, 108) As Time ,
                                      CONVERT(varchar, CreationDate, 104) AS  Date 
                                      from Tasks
                                      inner join TaskStatuses on Tasks.Status=TaskStatuses.ID
                                      inner join Agent on Agent.ID=Tasks.AgentID
                                      where Tasks.AgentID=@AgentID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@AgentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));

            DataSet ds = DbProvider.GetDataSet(cmd);

            Repeater1.DataSource = ds;
            Repeater1.DataBind();

            string sql2 = @"select  * from Lead";

            SqlCommand cmd2 = new SqlCommand(sql2);

            DataSet ds2 = DbProvider.GetDataSet(cmd2);

            Repeater2.DataSource = ds2;
            Repeater2.DataBind();
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
        protected void RadioButttonGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            DivRadioButtonGender.Visible = false;
            BtnGender.Text = RadioButttonGender.SelectedItem.Text;
        }
        protected void Gender_Click(object sender, EventArgs e)
        {
            bool isOpen = DivRadioButtonGender.Visible;
            if (isOpen == false)
            {
                DivRadioButtonGender.Visible = true;

                //SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
                //DataSet ds = DbProvider.GetDataSet(cmd);
                //RadioButttonGender.DataSource = ds;
                //RadioButttonGender.DataTextField = "userFirstName";
                //RadioButttonGender.DataValueField = "IDUser";
                //RadioButttonGender.DataBind();
                //SpanLeadManagement.Style.Add("color", "#2da9fd");
                //ImgLeadManagement.Src = "images/icons/Arrow_Blue_Button.png";
            }
            else
            {
                DivRadioButtonGender.Visible = false;
                //SpanLeadManagement.Style.Add("color", "#0f325e");
                //ImgLeadManagement.Src = "images/icons/Arrow_Slide_Button.png";
            }

        }


    }
}