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
    public partial class _Contact : System.Web.UI.Page
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

                //if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ImageFile_Close"].ToString()))
                //    ImageFile_3_display.Src = ConfigurationManager.AppSettings["FilesUrl"] + dt.Rows[0]["ImageFile_Close"].ToString();//FilesUrl
                //ImageFile.Src = "";

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
        //protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{

        //}
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
            string sqlContact = @"
              select  Contact.ID,
                                  Contact.FirstName
                                 ,Contact.LastName
                                 ,Contact.DateBirth
                                 ,Contact.Gender
                                 ,Contact.Tz
                                 ,Contact.IssuanceDateTz
                                 ,Contact.GrossSalary
                                 ,Contact.Phone
                                 ,Contact.Email
                                 ,Contact.Note
                                 ,Contact.Address
                                 ,Contact.IDAgent
                                 ,Contact.CreateDate
								 ,Agent.FullName as FullNameAgent
	                             from Contact
								 inner join Agent on Contact.IDAgent=Agent.id
                                 where Contact.ID=@ContactID";
            SqlCommand cmdContact = new SqlCommand(sqlContact);

            cmdContact.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"]);

            DataTable dtContact = DbProvider.GetDataTable(cmdContact);
            if (dtContact.Rows.Count > 0)
            {
                PhoneH.InnerText = dtContact.Rows[0]["Phone"].ToString();
                EmailH.InnerText = dtContact.Rows[0]["Email"].ToString();

                DateTime dateOfBirth = Convert.ToDateTime(dtContact.Rows[0]["DateBirth"]);
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - dateOfBirth.Year;
                FullName.InnerText= dtContact.Rows[0]["FirstName"].ToString()+ " " +dtContact.Rows[0]["LastName"].ToString();
                FirstName.Value = dtContact.Rows[0]["FirstName"].ToString();
                LastName.Value = dtContact.Rows[0]["LastName"].ToString();
                //RadioButttonGender.SelectedValue = dtLead.Rows[0]["Gender"].ToString();
                //if (dtLead.Rows[0]["Gender"].ToString() == "other") { BtnGender.Text = "אחר"; }
                //else if (dtLead.Rows[0]["Gender"].ToString() == "male") { BtnGender.Text = "זכר"; }
                //else if (dtLead.Rows[0]["Gender"].ToString() == "female") { BtnGender.Text = "נקבה"; }
                Age.InnerText = age.ToString();
                RadioButttonGender.SelectedValue= dtContact.Rows[0]["Gender"].ToString();
                if (dtContact.Rows[0]["Gender"].ToString() == "other") { BtnGender.Text = "אחר"; }
                else if (dtContact.Rows[0]["Gender"].ToString() == "male") { BtnGender.Text = "זכר"; }
                else if (dtContact.Rows[0]["Gender"].ToString() == "female") { BtnGender.Text = "נקבה"; }
                DateBirth.Value = (dateOfBirth).ToString("yyyy-MM-dd");
                //DateBirth.Value =DateTime.Parse(dtLead.Rows[0]["DateBirth"].ToString()).ToString("dd/mm/yyyy");
                //Address.Value = dtLead.Rows[0]["Address"].ToString();

                Tz.Value = dtContact.Rows[0]["Tz"].ToString();
                IssuanceDateTz.Value = (Convert.ToDateTime(dtContact.Rows[0]["IssuanceDateTz"])).ToString("yyyy-MM-dd");
                Phone.Value = dtContact.Rows[0]["Phone"].ToString();
                Email.Value = dtContact.Rows[0]["Email"].ToString();
                GrossSalary.Value = dtContact.Rows[0]["GrossSalary"].ToString();
                Note.InnerText = dtContact.Rows[0]["Note"].ToString();
                //    BusinessLineBusiness.Text = dtLead.Rows[0]["BusinessLineBusiness"].ToString();
                //    PartnerEmail.Value = dtLead.Rows[0]["PartnerEmail"].ToString();
                //    PartnerPhone.Value = dtLead.Rows[0]["PartnerPhone"].ToString();
                //    PartnerCity.Value = dtLead.Rows[0]["PartnerCity"].ToString();
                //    PartnerStreet.Value = dtLead.Rows[0]["PartnerStreet"].ToString();
                //    PartnerBuildingFloorApartment.Value = dtLead.Rows[0]["PartnerBuildingFloorApartment"].ToString();
                //    PartnerNumMailbox.Value = dtLead.Rows[0]["PartnerNumMailbox"].ToString();
                //    //PartnerLineBusiness.Text = dtLead.Rows[0]["PartnerLineBusiness"].ToString();
                //    PartnerFamilyStatus.Text = dtLead.Rows[0]["PartnerFamilyStatus"].ToString();
                //    PartnerName.Text = dtLead.Rows[0]["PartnerName"].ToString();
                //    PartnerAge.Text = dtLead.Rows[0]["PartnerAge"].ToString();
                //    PartnerSeniority.Text = dtLead.Rows[0]["PartnerSeniority"].ToString();
                Address.Value = dtContact.Rows[0]["Address"].ToString();
                FullNameAgent.InnerText= dtContact.Rows[0]["FullNameAgent"].ToString();

            }
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

        protected void btn_save_Click(object sender, EventArgs e)
        {
            bool success = funcSave(sender, e);
            if (!success)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect("Contacts.aspx");
            }
        }
        public bool funcSave(object sender, EventArgs e)
        {
            int ErrorCount = 0;
            FormError_lable.Visible = false;
            //if (FirstName.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין שם פרטי";
            //}
            //else if (LastName.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין שם משפחה";
            //}
            //else if (DateBirth.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין תאריך לידה";
            //}
            //else if (DateTime.Parse(DateBirth.Value) > DateTime.Now)
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין תאריך לידה תקין";
            //}
            //else if (Tz.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין ת.ז";
            //}
            //else if (Tz.Value.Length != 9)
            //{
            //    ErrorCount++;
            //    FormError_lable.Text = "יש להזין ת.ז תקינה";
            //    FormError_lable.Visible = true;
            //}
            //else if (Phone.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין מספר טלפון";
            //}
            //else if (Phone.Value.Length < 9 || Phone.Value.Substring(0, 1) != "0")
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין מספר טלפון תקין";
            //}
            //else if (Email.Value != "" && !Email.Value.Contains("@"))
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין אימייל תקין";

            //}
            //else if (RadioButttonFirstStatus.SelectedValue == "")
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין סטטוס ראשי";

            //}
            //else if (BusinessEmail.Value != "" && !BusinessEmail.Value.Contains("@"))
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין אימייל תקין";

            //}
            //else if (BusinessPhone.Value != "" && (BusinessPhone.Value.Length < 9 || Phone.Value.Substring(0, 1) != "0"))
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין מספר טלפון תקין";
            //}
            //else if (PartnerEmail.Value != "" && !PartnerEmail.Value.Contains("@"))
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין אימייל תקין";

            //}
            //else if (PartnerPhone.Value != "" && (BusinessPhone.Value.Length < 9 || Phone.Value.Substring(0, 1) != "0"))
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין מספר טלפון תקין";
            //}

            if (ErrorCount == 0)
            {
                string sql = @" Update Contact set 
                                       FirstName=@FirstName
                                       ,LastName=@LastName
                                       ,DateBirth=@DateBirth
                                       --,Gender=@Gender
                                       ,Tz=@Tz
                                       ,IssuanceDateTz=@IssuanceDateTz
                                       ,Phone=@Phone
                                       ,Email=@Email
                                       ,Address=@Address
                                        where ID = @ContactID";
                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@FirstName", FirstName.Value);
                cmd.Parameters.AddWithValue("@LastName", LastName.Value);
                cmd.Parameters.AddWithValue("@DateBirth", DateBirth.Value);
                //cmd.Parameters.AddWithValue("@Gender", RadioButttonGender.SelectedValue == "" ? (object)DBNull.Value : RadioButttonGender.SelectedValue);
                cmd.Parameters.AddWithValue("@Tz", string.IsNullOrEmpty(Tz.Value) ? (object)DBNull.Value : Tz.Value);
                cmd.Parameters.AddWithValue("@IssuanceDateTz", string.IsNullOrEmpty(IssuanceDateTz.Value) ? (object)DBNull.Value : IssuanceDateTz.Value);
                cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(Phone.Value) ? (object)DBNull.Value : Phone.Value);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email.Value) ? (object)DBNull.Value : Email.Value);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(Address.Value) ? (object)DBNull.Value : Address.Value);
               


               
                cmd.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"]);

                if (DbProvider.ExecuteCommand(cmd) > 0)
                {
                    return true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                    FormError_lable.Text = "* התרחשה שגיאה";
                    FormError_lable.Visible = true;
                }

            }

            return false;
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
        protected void RadioButttonFamilyStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DivRBPartnerFamilyStatus.Visible = false;
            PartnerFamilyStatus.Text = RadioButttonPartnerFamilyStatus.SelectedItem.Text;
        }
        protected void PartnerFamilyStatus_Click(object sender, EventArgs e)
        {
            bool isOpen = DivRBPartnerFamilyStatus.Visible;
            if (isOpen == false)
            {
                DivRBPartnerFamilyStatus.Visible = true;

                //SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
                //DataSet ds = DbProvider.GetDataSet(cmd);
                //RadioButtonLineBusiness.DataSource = ds;
                //RadioButtonLineBusiness.DataTextField = "userFirstName";
                //RadioButtonLineBusiness.DataValueField = "IDUser";
                //RadioButtonLineBusiness.DataBind();
                //SpanLeadManagement.Style.Add("color", "#2da9fd");
                //ImgLeadManagement.Src = "images/icons/Arrow_Blue_Button.png";
            }
            else
            {
                DivRBPartnerFamilyStatus.Visible = false;
                //SpanLeadManagement.Style.Add("color", "#0f325e");
                //ImgLeadManagement.Src = "images/icons/Arrow_Slide_Button.png";
            }

        }
        protected void RadioButttonLineBusiness_SelectedIndexChanged(object sender, EventArgs e)
        {
            DivRBLineBusiness.Visible = false;
            BtnLineBusiness.Text = RadioButtonLineBusiness.SelectedItem.Text;
        }
        protected void LineBusiness_Click(object sender, EventArgs e)
        {
            bool isOpen = DivRBLineBusiness.Visible;
            if (isOpen == false)
            {
                DivRBLineBusiness.Visible = true;

                //SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
                //DataSet ds = DbProvider.GetDataSet(cmd);
                //RadioButtonLineBusiness.DataSource = ds;
                //RadioButtonLineBusiness.DataTextField = "userFirstName";
                //RadioButtonLineBusiness.DataValueField = "IDUser";
                //RadioButtonLineBusiness.DataBind();
                //SpanLeadManagement.Style.Add("color", "#2da9fd");
                //ImgLeadManagement.Src = "images/icons/Arrow_Blue_Button.png";
            }
            else
            {
                DivRBLineBusiness.Visible = false;
                //SpanLeadManagement.Style.Add("color", "#0f325e");
                //ImgLeadManagement.Src = "images/icons/Arrow_Slide_Button.png";
            }

        }

    }
}