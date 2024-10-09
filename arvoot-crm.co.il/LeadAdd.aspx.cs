﻿using System;
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
    public partial class _leadAdd : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "חפש קובץ";
        public string StrSrc { get { return strSrc; } }
        public string ListPageUrl = "LeadAdd.aspx";

        //בליד התעודה זהות לא ייחודית

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {
                Pageinit.CheckManagerPermissions();

                SqlCommand cmd = new SqlCommand("SELECT * FROM FirstStatusLead");
                DataSet ds = DbProvider.GetDataSet(cmd);
                SelectFirstStatus.DataSource = ds;
                SelectFirstStatus.DataTextField = "Status";
                SelectFirstStatus.DataValueField = "ID";
                SelectFirstStatus.DataBind();

                SqlCommand cmdSecondStatus = new SqlCommand("SELECT * FROM SecondStatusLead");
                DataSet dsSecondStatus = DbProvider.GetDataSet(cmdSecondStatus);
                SelectSecondStatus.DataSource = dsSecondStatus;
                SelectSecondStatus.DataTextField = "Status";
                SelectSecondStatus.DataValueField = "ID";
                SelectSecondStatus.DataBind();

                SqlCommand cmdSourceLead = new SqlCommand("SELECT * FROM SourceLead");
                DataSet dsSourceLead = DbProvider.GetDataSet(cmdSourceLead);
                SelectSourceLead.DataSource = dsSourceLead;
                SelectSourceLead.DataTextField = "Text";
                SelectSourceLead.DataValueField = "ID";
                SelectSourceLead.DataBind();
                SelectSourceLead.Items.Insert(0, new ListItem("בחר", ""));


                SqlCommand cmdLineBusiness = new SqlCommand("SELECT * FROM EmploymentStatus");
                DataSet dsLineBusiness = DbProvider.GetDataSet(cmdLineBusiness);
                SelectBusinessLineBusiness.DataSource = dsLineBusiness;
                SelectBusinessLineBusiness.DataTextField = "Name";
                SelectBusinessLineBusiness.DataValueField = "ID";
                SelectBusinessLineBusiness.DataBind();
                SelectBusinessLineBusiness.Items.Insert(0, new ListItem("בחר", ""));

                SelectPartnerLineBusiness.DataSource = dsLineBusiness;
                SelectPartnerLineBusiness.DataTextField = "Name";
                SelectPartnerLineBusiness.DataValueField = "ID";
                SelectPartnerLineBusiness.DataBind();
                SelectPartnerLineBusiness.Items.Insert(0, new ListItem("בחר", ""));

                SqlCommand cmdFamilyStatus = new SqlCommand("SELECT * FROM FamilyStatus");
                DataSet dsFamilyStatus = DbProvider.GetDataSet(cmdFamilyStatus);
                SelectFamilyStatus.DataSource = dsFamilyStatus;
                SelectFamilyStatus.DataTextField = "Name";
                SelectFamilyStatus.DataValueField = "ID";
                SelectFamilyStatus.DataBind();
                SelectFamilyStatus.Items.Insert(0, new ListItem("בחר", ""));


                SqlCommand cmdGender = new SqlCommand("SELECT * FROM Gender");
                DataSet dsGender = DbProvider.GetDataSet(cmdGender);
                SelectGender.DataSource = dsGender;
                SelectGender.DataTextField = "Name";
                SelectGender.DataValueField = "ID";
                SelectGender.DataBind();
                SelectGender.Items.Insert(0, new ListItem("בחר", ""));

                //RadioButttonSecondStatus.Items.Insert(0, new ListItem("בחר", "0"));
                //CBIsValidIssuanceDateTz.Checked = true;



                loadData();
            }

        }
        public void loadData()
        {
            SqlCommand cmdAgent = new SqlCommand(@"  select ID, FullName, Tz,Email, Phone, 
                                                      CONVERT(varchar,CreateDate, 104) as CreateDate,ImageFile
                                                     from Agent where ID = @AgentID");
            try
            {
                Pageinit.CheckManagerPermissions();
                cmdAgent.Parameters.AddWithValue("@AgentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Redirect("SignIn.aspx");
            }

            DataTable dt = DbProvider.GetDataTable(cmdAgent);
            if (dt.Rows.Count > 0)
            {
                TodayDate.InnerText= DateTime.Now.ToString("dd.MM.yyyy");
                FullNameAgent.InnerText = dt.Rows[0]["FullName"].ToString();
                PhoneAgent.InnerText = dt.Rows[0]["Phone"].ToString();
                EmailAgent.InnerText = dt.Rows[0]["Email"].ToString();
                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ImageFile"].ToString()))
                {
                    ImageFileAgent.Src = ConfigurationManager.AppSettings["DomainUrl"] + "/Agent/" + dt.Rows[0]["ImageFile"].ToString();
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
       
        protected void Status_Click(object sender, EventArgs e)
        { }

       
      
        protected void btnDateFilter_Click(object sender, EventArgs e)
        {

        }
        protected void ShereLid_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void DeleteLid_Click(object sender, ImageClickEventArgs e)
        {


        }





        protected void ButtonDiv_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

        }


        protected void AddTimeOK_Click(object sender, ImageClickEventArgs e)
        {

        }


        
        protected void CloseAddTime_Click(object sender, ImageClickEventArgs e)
        {
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
                System.Web.HttpContext.Current.Response.Redirect(ListPageUrl);
            }
        }
        public bool funcSave(object sender, EventArgs e)
        {
            //שם פרטי שם משפחה תאריך לידה תז טלפון אימייל סטטוס ראשי
            int ErrorCount = 0;
            FormError_lable.Visible = false;
            if (FirstName.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין שם פרטי";
                return false;
            }
            if (LastName.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין שם משפחה";
                return false;
            }
            if (DateBirth.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין תאריך לידה";
                return false;
            }
            if (DateTime.Parse(DateBirth.Value) > DateTime.Now)
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין תאריך לידה תקין";
                return false;
            }
            if (Tz.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין ת.ז";
                return false;
            }
            if (Tz.Value.Length != 9)
            {
                ErrorCount++;
                FormError_lable.Text = "יש להזין ת.ז תקינה";
                FormError_lable.Visible = true;
                return false;
            }
            if (Phone1.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מספר טלפון";
                return false;
            }
            if (Phone1.Value.Length < 9 || Phone1.Value.Substring(0, 1) != "0")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מספר טלפון תקין";
                return false;
            }
            if (Email.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין אימייל";
                return false;
            }
            if (Email.Value != "" && !Email.Value.Contains("@"))
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין אימייל תקין";
                return false;
            }
            if (SelectFirstStatus.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין סטטוס ראשי";
                return false;
            }
            if (Phone2.Value != "" && (Phone2.Value.Length < 9 || Phone2.Value.Substring(0, 1) != "0"))
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מספר טלפון תקין";
                return false;
            }
            if (BusinessEmail.Value != "" && !BusinessEmail.Value.Contains("@"))
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין אימייל תקין";
                return false;

            }

            if (BusinessPhone.Value != "" && (BusinessPhone.Value.Length < 9 || BusinessPhone.Value.Substring(0, 1) != "0"))
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מספר טלפון תקין";
                return false;
            }

            //if (CBHaveAsset.Checked == true)
            //{
            //    if (AssetValue.Value == "")
            //    {
            //        ErrorCount++;
            //        FormError_lable.Visible = true;
            //        FormError_lable.Text = "יש להזין שווי הנכס";
            //        return false;
            //    }
            //    else if (AssetType.Value == "")
            //    {
            //        ErrorCount++;
            //        FormError_lable.Visible = true;
            //        FormError_lable.Text = "יש להזין סוג הנכס ";
            //        return false;
            //    }
            //    else if (AssetAddress.Value == "")
            //    {
            //        ErrorCount++;
            //        FormError_lable.Visible = true;
            //        FormError_lable.Text = "יש להזין כתובת הנכס ";
            //        return false;
            //    }

            //}
            if (CBHaveAsset.Checked == false)
            {
                if (AssetValue.Value != "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך לסמן האם קיים נכס בבעלות הלקוח";
                    return false;
                }
                else if (AssetType.Value != "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך לסמן האם קיים נכס בבעלות הלקוח";
                    return false;
                }
                else if (AssetAddress.Value != "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך לסמן האם קיים נכס בבעלות הלקוח";
                    return false;
                }

            }
            if (CBHaveMortgageOnAsset.Checked == false)
            {
                if (MortgageAmount.Value != "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך לסמן האם קיימת משכנתא על נכס";
                    return false;
                }
                else if (MonthlyRepaymentAmount.Value != "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך לסמן האם קיימת משכנתא על נכס";
                    return false;
                }
                else if (LendingBank.Value != "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך לסמן האם קיימת משכנתא על נכס";
                    return false;
                }
                else if (PurposeTest.Value != "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך לסמן האם קיימת משכנתא על נכס";
                    return false;
                }
                else if (RequestedLoanAmount.Value != "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך לסמן האם קיימת משכנתא על נכס";
                    return false;
                }
                else if (PurposeLoan.Value != "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך לסמן האם קיימת משכנתא על נכס";
                    return false;
                }
                else if (MortgageBalance.Value != "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך לסמן האם קיימת משכנתא על נכס";
                    return false;
                }

            }
            if (ErrorCount == 0)
            {
                string sql = @" INSERT INTO [Lead]( FirstName
      ,LastName
      ,GenderID
      ,DateBirth
      ,Address
      ,FamilyStatusID
      ,Tz
      ,IssuanceDateTz
      ,IsValidBdi
      ,InvalidBdiReason
      ,Phone1
      ,Phone2
      ,Email
      ,FirstStatusLeadID
      ,DateChangeFirstStatus
      ,SecondStatusLeadID
      ,SourceLeadID
      ,InterestedIn
      ,TrackingTime
      ,Note
      ,AgentID
      ,BusinessName
      ,BusinessSeniority
      ,BusinessProfession
      ,BusinessCity
      ,BusinessEmail
      ,BusinessPhone
      ,BusinessGrossSalary
      ,BusinessLineBusiness
      ,PartnerLineBusiness
      ,PartnerName
      ,PartnerGrossSalary
      ,PartnerAge
      ,PartnerSeniority
      ,HaveAsset
      ,AssetValue
      ,AssetType
      ,AssetAddress
      ,HaveMortgageOnAsset
      ,MortgageAmount
      ,MonthlyRepaymentAmount
      ,LendingBank
      ,PurposeTest
      ,RequestedLoanAmount
      ,PurposeLoan
      ,MortgageBalance
      ,CreateDate)
      VALUES (
	   @FirstName
      ,@LastName
      ,@GenderID
      ,@DateBirth
      ,@Address
      ,@FamilyStatusID
      ,@Tz
      ,@IssuanceDateTz
      ,@IsValidBdi
      ,@InvalidBdiReason
      ,@Phone1
      ,@Phone2
      ,@Email
      ,@FirstStatusLeadID
      ,GETDATE()
      ,@SecondStatusLeadID
      ,@SourceLeadID
      ,@InterestedIn
      ,@TrackingTime
      ,@Note
      ,@AgentID
      ,@BusinessName
      ,@BusinessSeniority
      ,@BusinessProfession
      ,@BusinessCity
      ,@BusinessEmail
      ,@BusinessPhone
      ,@BusinessGrossSalary
      ,@BusinessLineBusiness
      ,@PartnerLineBusiness
      ,@PartnerName
      ,@PartnerGrossSalary
      ,@PartnerAge
      ,@PartnerSeniority
      ,@HaveAsset
      ,@AssetValue
      ,@AssetType
      ,@AssetAddress
      ,@HaveMortgageOnAsset
      ,@MortgageAmount
      ,@MonthlyRepaymentAmount
      ,@LendingBank
      ,@PurposeTest
      ,@RequestedLoanAmount
      ,@PurposeLoan
      ,@MortgageBalance
      ,GETDATE())
  ";



                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@FirstName", FirstName.Value);
                cmd.Parameters.AddWithValue("@LastName", LastName.Value);
                cmd.Parameters.AddWithValue("@GenderID", string.IsNullOrEmpty(SelectGender.Value) ? (object)DBNull.Value : SelectGender.Value);
                cmd.Parameters.AddWithValue("@DateBirth", DateBirth.Value);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(Address.Value) ? (object)DBNull.Value : Address.Value);
                cmd.Parameters.AddWithValue("@FamilyStatusID", string.IsNullOrEmpty(SelectFamilyStatus.Value) ? (object)DBNull.Value : SelectFamilyStatus.Value);
                cmd.Parameters.AddWithValue("@Tz", Tz.Value);
                cmd.Parameters.AddWithValue("@IssuanceDateTz", string.IsNullOrEmpty(IssuanceDateTz.Value) ? (object)DBNull.Value : DateTime.Parse(IssuanceDateTz.Value));
                //Gila
                //cmd.Parameters.AddWithValue("@IsValidIssuanceDateTz", /*CBIsValidIssuanceDateTz.Checked == true ? 1 : 0*/0);
                cmd.Parameters.AddWithValue("@IsValidBdi", /*IsValidBdi.Checked == true*/ BdiValidity.SelectedIndex == 0 ? 1 : 0);
                cmd.Parameters.AddWithValue("@InvalidBdiReason", string.IsNullOrEmpty(InvalidBdiReason.Value) ? (object)DBNull.Value : InvalidBdiReason.Value );
                cmd.Parameters.AddWithValue("@Phone1", Phone1.Value);
                cmd.Parameters.AddWithValue("@Phone2", string.IsNullOrEmpty(Phone2.Value) ? (object)DBNull.Value : Phone2.Value);
                cmd.Parameters.AddWithValue("@Email", Email.Value);
                cmd.Parameters.AddWithValue("@FirstStatusLeadID", SelectFirstStatus.Value);
                cmd.Parameters.AddWithValue("@SecondStatusLeadID", string.IsNullOrEmpty(SelectSecondStatus.Value) ? (object)DBNull.Value : int.Parse(SelectSecondStatus.Value));

                cmd.Parameters.AddWithValue("@SourceLeadID", string.IsNullOrEmpty(SelectSourceLead.Value) ? (object)DBNull.Value : int.Parse(SelectSourceLead.Value));
                cmd.Parameters.AddWithValue("@InterestedIn", string.IsNullOrEmpty(InterestedIn.Value) ? (object)DBNull.Value : InterestedIn.Value);
                cmd.Parameters.AddWithValue("@TrackingTime", string.IsNullOrEmpty(TrackingTime.Value) ? (object)DBNull.Value : DateTime.Parse(TrackingTime.Value));
                cmd.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note.Value) ? (object)DBNull.Value : Note.Value);



                try
                {
                    Pageinit.CheckManagerPermissions();
                    cmd.Parameters.AddWithValue("@AgentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                }
                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Redirect("SignIn.aspx");
                }


                cmd.Parameters.AddWithValue("@BusinessName", string.IsNullOrEmpty(BusinessName.Value) ? (object)DBNull.Value : BusinessName.Value);
                cmd.Parameters.AddWithValue("@BusinessSeniority", string.IsNullOrEmpty(BusinessSeniority.Value) ? (object)DBNull.Value : BusinessSeniority.Value);
                cmd.Parameters.AddWithValue("@BusinessProfession", string.IsNullOrEmpty(BusinessProfession.Value) ? (object)DBNull.Value : BusinessProfession.Value);
                cmd.Parameters.AddWithValue("@BusinessCity", string.IsNullOrEmpty(BusinessCity.Value) ? (object)DBNull.Value : BusinessCity.Value);
                cmd.Parameters.AddWithValue("@BusinessEmail", string.IsNullOrEmpty(BusinessEmail.Value) ? (object)DBNull.Value : BusinessEmail.Value);
                cmd.Parameters.AddWithValue("@BusinessPhone", string.IsNullOrEmpty(BusinessPhone.Value) ? (object)DBNull.Value : BusinessPhone.Value);
                cmd.Parameters.AddWithValue("@BusinessGrossSalary", string.IsNullOrEmpty(BusinessGrossSalary.Value) ? (object)DBNull.Value : BusinessGrossSalary.Value);
                cmd.Parameters.AddWithValue("@BusinessLineBusiness", string.IsNullOrEmpty(SelectBusinessLineBusiness.Value) ? (object)DBNull.Value : int.Parse(SelectBusinessLineBusiness.Value));
                cmd.Parameters.AddWithValue("@PartnerLineBusiness", string.IsNullOrEmpty(SelectPartnerLineBusiness.Value) ? (object)DBNull.Value : int.Parse(SelectPartnerLineBusiness.Value));
                cmd.Parameters.AddWithValue("@PartnerName", string.IsNullOrEmpty(PartnerName.Value) ? (object)DBNull.Value : PartnerName.Value);
                cmd.Parameters.AddWithValue("@PartnerGrossSalary", string.IsNullOrEmpty(PartnerGrossSalary.Value) ? (object)DBNull.Value : PartnerGrossSalary.Value);
                cmd.Parameters.AddWithValue("@PartnerAge", string.IsNullOrEmpty(PartnerAge.Value) ? (object)DBNull.Value : PartnerAge.Value);
                cmd.Parameters.AddWithValue("@PartnerSeniority", string.IsNullOrEmpty(PartnerSeniority.Value) ? (object)DBNull.Value : PartnerSeniority.Value);

                cmd.Parameters.AddWithValue("@HaveAsset", CBHaveAsset.Checked == true ? 1 : 0);
                cmd.Parameters.AddWithValue("@AssetValue", string.IsNullOrEmpty(AssetValue.Value) ? (object)DBNull.Value : int.Parse(AssetValue.Value));
                cmd.Parameters.AddWithValue("@AssetType", string.IsNullOrEmpty(AssetType.Value) ? (object)DBNull.Value : AssetType.Value);
                cmd.Parameters.AddWithValue("@AssetAddress", string.IsNullOrEmpty(AssetAddress.Value) ? (object)DBNull.Value : AssetAddress.Value);



                cmd.Parameters.AddWithValue("@HaveMortgageOnAsset", CBHaveMortgageOnAsset.Checked == true ? 1 : 0);

                cmd.Parameters.AddWithValue("@MortgageAmount", string.IsNullOrEmpty(MortgageAmount.Value) ? (object)DBNull.Value : long.Parse(MortgageAmount.Value));
                cmd.Parameters.AddWithValue("@MonthlyRepaymentAmount", string.IsNullOrEmpty(MonthlyRepaymentAmount.Value) ? (object)DBNull.Value : int.Parse(MonthlyRepaymentAmount.Value));
                cmd.Parameters.AddWithValue("@LendingBank", string.IsNullOrEmpty(LendingBank.Value) ? (object)DBNull.Value : LendingBank.Value);
                cmd.Parameters.AddWithValue("@PurposeTest", string.IsNullOrEmpty(PurposeTest.Value) ? (object)DBNull.Value : PurposeTest.Value);
                cmd.Parameters.AddWithValue("@RequestedLoanAmount", string.IsNullOrEmpty(RequestedLoanAmount.Value) ? (object)DBNull.Value : long.Parse(RequestedLoanAmount.Value));
                cmd.Parameters.AddWithValue("@PurposeLoan", string.IsNullOrEmpty(PurposeLoan.Value) ? (object)DBNull.Value : PurposeLoan.Value);
                cmd.Parameters.AddWithValue("@MortgageBalance", string.IsNullOrEmpty(MortgageBalance.Value) ? (object)DBNull.Value : long.Parse(MortgageBalance.Value));


                if (DbProvider.ExecuteCommand(cmd) > 0)
                {
                    SqlCommand cmdHistory = new SqlCommand("INSERT INTO ActivityHistory (AgentID, Details, CreateDate, Show) VALUES (@agentID, @details, GETDATE(), 1)");
                    cmdHistory.Parameters.AddWithValue("@agentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                    cmdHistory.Parameters.AddWithValue("@details", ("הוספת ליד חדש " + FirstName.Value + " " + LastName.Value));
                    DbProvider.ExecuteCommand(cmdHistory);
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
        


    }
}