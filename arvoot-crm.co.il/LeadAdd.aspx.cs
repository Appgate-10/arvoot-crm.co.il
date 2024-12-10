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
    public partial class _leadAdd : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "חפש קובץ";
        public string StrSrc { get { return strSrc; } }
        public string ListPageUrl = "Leads.aspx";

        //בליד התעודה זהות לא ייחודית

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {
                Pageinit.CheckManagerPermissions();

                SqlCommand cmd = new SqlCommand("SELECT * FROM FirstStatusLead where ID != 10");
                DataSet ds = DbProvider.GetDataSet(cmd);
                SelectFirstStatus.DataSource = ds;
                SelectFirstStatus.DataTextField = "Status";
                SelectFirstStatus.DataValueField = "ID";
                SelectFirstStatus.DataBind();
                SelectFirstStatus.SelectedIndex = 1;

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
                                                     from ArvootManagers where ID = @AgentID");
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
        protected void Yes_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select ID,IsContact from Lead where Tz = @Tz");
            cmd.Parameters.AddWithValue("Tz" , Tz.Value);
            DataTable dt = DbProvider.GetDataTable(cmd);
            if(dt.Rows.Count > 0)
            {
                if (int.Parse(dt.Rows[0]["IsContact"].ToString()) == 0)
                {
                    System.Web.HttpContext.Current.Response.Redirect("LeadEdit.aspx?LeadID=" + dt.Rows[0]["ID"].ToString());
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Redirect("Contact.aspx?ContactID=" + dt.Rows[0]["ID"].ToString());
                }
            }
            
            Div1.Visible = false;
        } 
        protected void No_Click(object sender, EventArgs e)
        {
            Div1.Visible = false;

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
        protected void CloseTzPopUp_Click(object sender, ImageClickEventArgs e)
        {
            Div1.Visible = false;
        }
      
        protected void btn_save_Click(object sender, EventArgs e)
        {
            long success = funcSave(sender, e);
            if (success == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                //System.Web.HttpContext.Current.Response.Redirect(ListPageUrl);
                Response.Redirect("LeadEdit.aspx?LeadID=" + success.ToString());
            }
        }
        public long funcSave(object sender, EventArgs e)
        {
            //שם פרטי שם משפחה תאריך לידה תז טלפון אימייל סטטוס ראשי
            int ErrorCount = 0;
            FormError_label.Visible = false;
            FormErrorBottom_label.Visible = false;
            if (FirstName.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין שם פרטי";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין שם פרטי";
                return 0;
            }
            if (LastName.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין שם משפחה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין שם משפחה";
                return 0;
            }
      
            if (Phone1.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מספר טלפון";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מספר טלפון";
                return 0;
            }
            if (Phone1.Value.Length < 9 || Phone1.Value.Substring(0, 1) != "0")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מספר טלפון תקין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מספר טלפון תקין";
                return 0;
            } 
            if (Phone2.Value != "" &&( Phone2.Value.Length < 9 || Phone2.Value.Substring(0, 1) != "0"))
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מספר טלפון תקין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מספר טלפון תקין";
                return 0;
            }
            if (Helpers.insuredPhoneExist(Phone1.Value, -1 ) == "true")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "מספר הטלפון קיים במערכת";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "מספר הטלפון קיים במערכת";
                return 0;
            }
            if (Email.Value != "" && !Email.Value.Contains("@"))
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין אימייל תקין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין אימייל תקין";
                return 0;
            }
             if (DateBirth.Value != "" && DateTime.Parse(DateBirth.Value) > DateTime.Now)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין תאריך לידה תקין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין תאריך לידה תקין";
                return 0;
            }
            if (Tz.Value != "" && Tz.Value.Length != 9)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין ת.ז תקינה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין ת.ז תקינה";
                return 0;
            }
            if (Tz.Value != "" && Helpers.insuredTzExist(Tz.Value, -1) == "true")
            {
                Div1.Visible = true;
                return 0;
   
            }
            //סטטוס מעקב לחייב למלא תאריך
            if (SelectFirstStatus.SelectedIndex == 8 && TrackingTime.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין זמן מעקב";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין זמן מעקב";
                return 0;
            }
            //סטטוס לא רלוונטי לחייב למלא סטטוס משני
            if (SelectFirstStatus.SelectedIndex == 7 && SelectSecondStatus.SelectedIndex ==0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין סטטוס משני";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין סטטוס משני";
                return 0;
            }
            if (BdiValidity.SelectedIndex == 2 && InvalidBdiReason.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין סיבה לאי תקינות";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין סיבה לאי תקינות";
                return 0;
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
      ,PrevBusinessSeniority
      ,BusinessProfession
      ,BusinessCity
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
      ,CreateDate) output INSERTED.ID
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
      ,@PrevBusinessSeniority
      ,@BusinessProfession
      ,@BusinessCity
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
                cmd.Parameters.AddWithValue("@DateBirth", string.IsNullOrEmpty(DateBirth.Value) ? (object)DBNull.Value : DateBirth.Value);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(Address.Value) ? (object)DBNull.Value : Address.Value);
                cmd.Parameters.AddWithValue("@FamilyStatusID", string.IsNullOrEmpty(SelectFamilyStatus.Value) ? (object)DBNull.Value : SelectFamilyStatus.Value);
                cmd.Parameters.AddWithValue("@Tz", string.IsNullOrEmpty(Tz.Value)? (object)DBNull.Value : Tz.Value);
                cmd.Parameters.AddWithValue("@IssuanceDateTz", string.IsNullOrEmpty(IssuanceDateTz.Value) ? (object)DBNull.Value : DateTime.Parse(IssuanceDateTz.Value));
                //Gila
                //cmd.Parameters.AddWithValue("@IsValidIssuanceDateTz", /*CBIsValidIssuanceDateTz.Checked == true ? 1 : 0*/0);
                cmd.Parameters.AddWithValue("@IsValidBdi", /*IsValidBdi.Checked == true*/ BdiValidity.SelectedIndex);
                cmd.Parameters.AddWithValue("@InvalidBdiReason", string.IsNullOrEmpty(InvalidBdiReason.Value) ? (object)DBNull.Value : InvalidBdiReason.Value );
                cmd.Parameters.AddWithValue("@Phone1", Phone1.Value);
                cmd.Parameters.AddWithValue("@Phone2", string.IsNullOrEmpty(Phone2.Value) ? (object)DBNull.Value : Phone2.Value);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email.Value) ? (object)DBNull.Value : Address.Value);
                cmd.Parameters.AddWithValue("@FirstStatusLeadID", SelectFirstStatus.Value);
                cmd.Parameters.AddWithValue("@SecondStatusLeadID", string.IsNullOrEmpty(SelectSecondStatus.Value) ? (object)DBNull.Value : int.Parse(SelectSecondStatus.Value));

                cmd.Parameters.AddWithValue("@SourceLeadID", string.IsNullOrEmpty(SelectSourceLead.Value) ? (object)DBNull.Value : int.Parse(SelectSourceLead.Value));
                cmd.Parameters.AddWithValue("@InterestedIn", string.IsNullOrEmpty(InterestedIn.Value) ? (object)DBNull.Value : InterestedIn.Value);
                cmd.Parameters.AddWithValue("@TrackingTime", string.IsNullOrEmpty(TrackingTime.Value) ? (object)DBNull.Value : DateTime.Parse(TrackingTime.Value));
                cmd.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note.Value) ? (object)DBNull.Value : Note.Value);



                try
                {
                    Pageinit.CheckManagerPermissions();
                    //heni - 13.10.24 - אין מענה פעם שלישית להעביר ליד למנהל
                    cmd.Parameters.AddWithValue("@AgentID", int.Parse(SelectFirstStatus.Value) == 5 ? (object)DBNull.Value : long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                }
                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Redirect("SignIn.aspx");
                }


                cmd.Parameters.AddWithValue("@BusinessName", string.IsNullOrEmpty(BusinessName.Value) ? (object)DBNull.Value : BusinessName.Value);
                cmd.Parameters.AddWithValue("@BusinessSeniority", string.IsNullOrEmpty(BusinessSeniority.Value) ? (object)DBNull.Value : BusinessSeniority.Value);
                cmd.Parameters.AddWithValue("@PrevBusinessSeniority", string.IsNullOrEmpty(PrevBusinessSeniority.Value) ? (object)DBNull.Value : PrevBusinessSeniority.Value);
                cmd.Parameters.AddWithValue("@BusinessProfession", string.IsNullOrEmpty(BusinessProfession.Value) ? (object)DBNull.Value : BusinessProfession.Value);
                cmd.Parameters.AddWithValue("@BusinessCity", string.IsNullOrEmpty(BusinessCity.Value) ? (object)DBNull.Value : BusinessCity.Value);
              //  cmd.Parameters.AddWithValue("@BusinessEmail", string.IsNullOrEmpty(BusinessEmail.Value) ? (object)DBNull.Value : BusinessEmail.Value);
                cmd.Parameters.AddWithValue("@BusinessPhone", string.IsNullOrEmpty(BusinessPhone.Value) ? (object)DBNull.Value : BusinessPhone.Value);
                cmd.Parameters.AddWithValue("@BusinessGrossSalary", string.IsNullOrEmpty(BusinessGrossSalary.Value) ? (object)DBNull.Value : BusinessGrossSalary.Value);
                cmd.Parameters.AddWithValue("@BusinessLineBusiness", string.IsNullOrEmpty(SelectBusinessLineBusiness.Value) ? (object)DBNull.Value : int.Parse(SelectBusinessLineBusiness.Value));
                cmd.Parameters.AddWithValue("@PartnerLineBusiness", string.IsNullOrEmpty(SelectPartnerLineBusiness.Value) ? (object)DBNull.Value : int.Parse(SelectPartnerLineBusiness.Value));
                cmd.Parameters.AddWithValue("@PartnerName", string.IsNullOrEmpty(PartnerName.Value) ? (object)DBNull.Value : PartnerName.Value);
                cmd.Parameters.AddWithValue("@PartnerGrossSalary", string.IsNullOrEmpty(PartnerGrossSalary.Value) ? (object)DBNull.Value : PartnerGrossSalary.Value);
                cmd.Parameters.AddWithValue("@PartnerAge", string.IsNullOrEmpty(PartnerAge.Value) ? (object)DBNull.Value : PartnerAge.Value);
                cmd.Parameters.AddWithValue("@PartnerSeniority", string.IsNullOrEmpty(PartnerSeniority.Value) ? (object)DBNull.Value : PartnerSeniority.Value);

                cmd.Parameters.AddWithValue("@HaveAsset", SelectHaveAsset.SelectedIndex);
                cmd.Parameters.AddWithValue("@AssetValue", string.IsNullOrEmpty(AssetValue.Value) ? (object)DBNull.Value : int.Parse(AssetValue.Value));
                cmd.Parameters.AddWithValue("@AssetType", string.IsNullOrEmpty(AssetType.Value) ? (object)DBNull.Value : AssetType.Value);
                cmd.Parameters.AddWithValue("@AssetAddress", string.IsNullOrEmpty(AssetAddress.Value) ? (object)DBNull.Value : AssetAddress.Value);



                cmd.Parameters.AddWithValue("@HaveMortgageOnAsset", SelectHaveMortgageOnAsset.SelectedIndex);

                cmd.Parameters.AddWithValue("@MortgageAmount", string.IsNullOrEmpty(MortgageAmount.Value) ? (object)DBNull.Value : long.Parse(MortgageAmount.Value));
                cmd.Parameters.AddWithValue("@MonthlyRepaymentAmount", string.IsNullOrEmpty(MonthlyRepaymentAmount.Value) ? (object)DBNull.Value : int.Parse(MonthlyRepaymentAmount.Value));
                cmd.Parameters.AddWithValue("@LendingBank", string.IsNullOrEmpty(LendingBank.Value) ? (object)DBNull.Value : LendingBank.Value);
                cmd.Parameters.AddWithValue("@PurposeTest", string.IsNullOrEmpty(PurposeTest.Value) ? (object)DBNull.Value : PurposeTest.Value);
                cmd.Parameters.AddWithValue("@RequestedLoanAmount", string.IsNullOrEmpty(RequestedLoanAmount.Value) ? (object)DBNull.Value : long.Parse(RequestedLoanAmount.Value));
                cmd.Parameters.AddWithValue("@PurposeLoan", string.IsNullOrEmpty(PurposeLoan.Value) ? (object)DBNull.Value : PurposeLoan.Value);
                cmd.Parameters.AddWithValue("@MortgageBalance", string.IsNullOrEmpty(MortgageBalance.Value) ? (object)DBNull.Value : long.Parse(MortgageBalance.Value));

                long LeadID = DbProvider.GetOneParamValueLong(cmd);
                if (LeadID > 0)
                {
                    SqlCommand cmdHistory = new SqlCommand("INSERT INTO ActivityHistory (AgentID, Details, CreateDate, Show) VALUES (@agentID, @details, GETDATE(), 1)");
                    cmdHistory.Parameters.AddWithValue("@agentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                    cmdHistory.Parameters.AddWithValue("@details", ("הוספת ליד חדש " + FirstName.Value + " " + LastName.Value));
                    DbProvider.ExecuteCommand(cmdHistory);
                    Helpers.loadActivityHistoryOnAdd(Page);
                    if (!string.IsNullOrEmpty(TrackingTime.Value))
                    {
                        string sqlTasks = @" INSERT INTO [Tasks]( Text
                                        ,Status
                                        ,LeadID
                                        ,PerformDate)
                                         VALUES (
	                                      @Text
                                         ,@Status
                                         ,@LeadID
                                         ,@PerformDate)";

                        SqlCommand cmdTasks = new SqlCommand(sqlTasks);

                        cmdTasks.Parameters.AddWithValue("@Text", "מעקב ליד " + Phone1.Value);
                        cmdTasks.Parameters.AddWithValue("@Status", 3); 
                        cmdTasks.Parameters.AddWithValue("@LeadID", LeadID);
                        cmdTasks.Parameters.AddWithValue("@PerformDate", DateTime.Parse(TrackingTime.Value));

                        DbProvider.ExecuteCommand(cmdTasks);

                        string sqlAlert = "INSERT INTO Alerts (AgentID, Text, CreationDate, DisplayDate) Values (@AgentID, @Text, GETDATE(), @DisplayDate)";
                        SqlCommand cmdAlert = new SqlCommand(sqlAlert);
                        cmdAlert.Parameters.AddWithValue("@AgentID", HttpContext.Current.Session["AgentID"]);
                        cmdAlert.Parameters.AddWithValue("@Text", "מעקב ליד " + FirstName.Value + " " + LastName.Value + " " + Phone1.Value);
                        cmdAlert.Parameters.AddWithValue("@DisplayDate", DateTime.Parse(TrackingTime.Value));
                        DbProvider.ExecuteCommand(cmdAlert);

                    }
                    return LeadID;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                    FormError_label.Text = "* התרחשה שגיאה";
                    FormError_label.Visible = true;
                    FormErrorBottom_label.Text = "* התרחשה שגיאה";
                    FormErrorBottom_label.Visible = true;
                }

            }

            return 0;
        }
        


    }
}