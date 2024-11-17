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
    public partial class _LeadEdit : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "חפש קובץ";
        public string StrSrc { get { return strSrc; } }
        public string ListPageUrl = "LeadAdd.aspx";


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
                DataSet dsGendrer = DbProvider.GetDataSet(cmdGender);
                SelectGender.DataSource = dsGendrer;
                SelectGender.DataTextField = "Name";
                SelectGender.DataValueField = "ID";
                SelectGender.DataBind();
                SelectGender.Items.Insert(0, new ListItem("בחר", ""));

                 

                HttpContext.Current.Session["FirstStatusLeadID"] = null;

                loadData();
            }

        }
        public void loadData()
        {
            string sqlLead = @"
                          select lead.FirstName,lead.LastName,GenderID,Year(GetDate())-Year(DateBirth) Age,CONVERT(varchar,DateBirth, 104) DateBirth,Address,lead.FamilyStatusID,lead.Tz, CONVERT(varchar,IssuanceDateTz, 104) IssuanceDateTz
                           ,IsValidBdi,InvalidBdiReason,lead.Phone1,lead.Phone2,lead.Email,SourceLeadID,InterestedIn,TrackingTime,Note
                           ,FirstStatusLead.Status FirstStatus,SecondStatusLead.Status SecondStatus
                           ,Lead.FirstStatusLeadID,Lead.SecondStatusLeadID,DateChangeFirstStatus
                           ,BusinessName,BusinessSeniority,PrevBusinessSeniority,BusinessProfession,BusinessCity,BusinessPhone,BusinessGrossSalary,BusinessLineBusiness
                           ,PartnerLineBusiness
                           ,PartnerName,PartnerGrossSalary,PartnerAge,PartnerSeniority
                           ,HaveAsset,Lead.AssetValue,Lead.AssetType,Lead.AssetAddress,HaveMortgageOnAsset
                           ,MortgageAmount,MonthlyRepaymentAmount,LendingBank,PurposeTest,RequestedLoanAmount
                           ,PurposeLoan,MortgageBalance
						   ,A.FullName as FullNameAgent,A.Phone PhoneAgent,A.Email as EmailAgent, A.ID as AgentID
                           ,CONVERT(varchar,Lead.CreateDate, 104) CreateDate
                           from Lead
						   left join ArvootManagers A on Lead.AgentID=A.ID and A.Type  in (3,6)
                           inner join FirstStatusLead on Lead.FirstStatusLeadID=FirstStatusLead.ID
                           left join SecondStatusLead on Lead.SecondStatusLeadID=SecondStatusLead.ID
                           where Lead.ID=@LeadID";
            SqlCommand cmdLead = new SqlCommand(sqlLead);

            cmdLead.Parameters.AddWithValue("@LeadID", Request.QueryString["LeadID"]);

            DataTable dtLead = DbProvider.GetDataTable(cmdLead);
            if (dtLead.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtLead.Rows[0]["DateBirth"].ToString()))
                {
                    DateTime dateOfBirth = DateTime.ParseExact(dtLead.Rows[0]["DateBirth"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    DateTime currentDate = DateTime.Now;
                    int age = currentDate.Year - dateOfBirth.Year;
                    Age.InnerText = age.ToString();
                    DateBirth.Value = (dateOfBirth).ToString("yyyy-MM-dd");
                }

                HiddenAgentID.Value = dtLead.Rows[0]["AgentID"].ToString();

                FirstName.Value = dtLead.Rows[0]["FirstName"].ToString();
                LastName.Value = dtLead.Rows[0]["LastName"].ToString();
                SelectGender.Value = dtLead.Rows[0]["GenderID"].ToString();
                //if (dtLead.Rows[0]["Gender"].ToString() == "other") { BtnGender.Text = "אחר"; }
                //else if (dtLead.Rows[0]["Gender"].ToString() == "male") { BtnGender.Text = "זכר"; }
                //else if (dtLead.Rows[0]["Gender"].ToString() == "female") { BtnGender.Text = "נקבה"; }
               
                //DateBirth.Value =DateTime.Parse(dtLead.Rows[0]["DateBirth"].ToString()).ToString("dd/mm/yyyy");
                Address.Value = dtLead.Rows[0]["Address"].ToString();
                SelectFamilyStatus.Value = dtLead.Rows[0]["FamilyStatusID"].ToString();
                Tz.Value = dtLead.Rows[0]["Tz"].ToString();
                IssuanceDateTz.Value = string.IsNullOrWhiteSpace(dtLead.Rows[0]["IssuanceDateTz"].ToString()) ? "" : DateTime.ParseExact(dtLead.Rows[0]["IssuanceDateTz"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
               /* IsValidIssuanceDateTz.Checked = Convert.ToBoolean(int.Parse(dtLead.Rows[0]["IsValidIssuanceDateTz"].ToString()));
                IsValidBdi.Checked = Convert.ToBoolean(int.Parse(dtLead.Rows[0]["IsValidBdi"].ToString()));*/
                BdiValidity.SelectedIndex = int.Parse(dtLead.Rows[0]["IsValidBdi"].ToString());
                InvalidBdiReason.Value = dtLead.Rows[0]["InvalidBdiReason"].ToString();
                Phone1.Value = dtLead.Rows[0]["Phone1"].ToString();
                Phone2.Value = dtLead.Rows[0]["Phone2"].ToString();
                Email.Value = dtLead.Rows[0]["Email"].ToString();
                SelectFirstStatus.Value = dtLead.Rows[0]["FirstStatusLeadID"].ToString();
                SelectSecondStatus.Value = dtLead.Rows[0]["SecondStatusLeadID"].ToString();
                SelectSourceLead.Value = dtLead.Rows[0]["SourceLeadID"].ToString();
                InterestedIn.Value = dtLead.Rows[0]["InterestedIn"].ToString();
                TrackingTime.Value = string.IsNullOrWhiteSpace(dtLead.Rows[0]["TrackingTime"].ToString()) ? "" : DateTime.Parse(dtLead.Rows[0]["TrackingTime"].ToString()).ToString("yyyy-MM-ddTHH:mm:ss");
                Note.Value = dtLead.Rows[0]["Note"].ToString();
                BusinessName.Value = dtLead.Rows[0]["BusinessName"].ToString();
                BusinessSeniority.Value = dtLead.Rows[0]["BusinessSeniority"].ToString();
                PrevBusinessSeniority.Value = dtLead.Rows[0]["PrevBusinessSeniority"].ToString();
                BusinessProfession.Value = dtLead.Rows[0]["BusinessProfession"].ToString();
                BusinessCity.Value = dtLead.Rows[0]["BusinessCity"].ToString();
                //BusinessEmail.Value = dtLead.Rows[0]["BusinessEmail"].ToString();

                BusinessPhone.Value = dtLead.Rows[0]["BusinessPhone"].ToString();
                BusinessGrossSalary.Value = dtLead.Rows[0]["BusinessGrossSalary"].ToString();
                SelectBusinessLineBusiness.Value = dtLead.Rows[0]["BusinessLineBusiness"].ToString();

                SelectPartnerLineBusiness.Value = dtLead.Rows[0]["PartnerLineBusiness"].ToString();
                PartnerName.Value = dtLead.Rows[0]["PartnerName"].ToString();
                PartnerGrossSalary.Value = dtLead.Rows[0]["PartnerGrossSalary"].ToString();
                PartnerAge.Value = dtLead.Rows[0]["PartnerAge"].ToString();
                PartnerSeniority.Value = dtLead.Rows[0]["PartnerSeniority"].ToString();
                //IsOpen24H.Checked = Convert.ToBoolean(int.Parse(dt.Rows[0]["IsOpen24H"].ToString()));
                SelectHaveAsset.SelectedIndex = int.Parse(dtLead.Rows[0]["HaveAsset"].ToString());
                AssetValue.Value = dtLead.Rows[0]["AssetValue"].ToString();
                AssetType.Value = dtLead.Rows[0]["AssetType"].ToString();
                AssetAddress.Value = dtLead.Rows[0]["AssetAddress"].ToString();
                SelectHaveMortgageOnAsset.SelectedIndex = int.Parse(dtLead.Rows[0]["HaveMortgageOnAsset"].ToString());

                MortgageAmount.Value = dtLead.Rows[0]["MortgageAmount"].ToString();
                MonthlyRepaymentAmount.Value = dtLead.Rows[0]["MonthlyRepaymentAmount"].ToString();
                LendingBank.Value = dtLead.Rows[0]["LendingBank"].ToString();
                PurposeTest.Value = dtLead.Rows[0]["PurposeTest"].ToString();
                RequestedLoanAmount.Value = dtLead.Rows[0]["RequestedLoanAmount"].ToString();
                PurposeLoan.Value = dtLead.Rows[0]["PurposeLoan"].ToString();
                MortgageBalance.Value = dtLead.Rows[0]["MortgageBalance"].ToString();

                DateChangeFirstStatus.Value = string.IsNullOrWhiteSpace(dtLead.Rows[0]["DateChangeFirstStatus"].ToString()) ? "" : DateTime.Parse(dtLead.Rows[0]["DateChangeFirstStatus"].ToString()).ToString("yyyy-MM-dd");

                HttpContext.Current.Session["FirstStatusLeadID"] = dtLead.Rows[0]["FirstStatusLeadID"].ToString();

                FullNameAgent.InnerText = dtLead.Rows[0]["FullNameAgent"].ToString();
                CreateDate.InnerText = dtLead.Rows[0]["CreateDate"].ToString();
                PhoneAgent.InnerText = dtLead.Rows[0]["PhoneAgent"].ToString();
                EmailAgent.InnerText = dtLead.Rows[0]["EmailAgent"].ToString();

            }


            //string sql = @"   select Text,TaskStatuses.Status, convert(VARCHAR(5), CreationDate, 108) As Time ,
            //                          CONVERT(varchar, CreationDate, 104) AS  Date 
            //                          from Tasks
            //                          inner join TaskStatuses on Tasks.Status=TaskStatuses.ID
            //                          inner join Agent on Agent.ID=Tasks.AgentID
            //                          where Tasks.AgentID=@AgentID";

            //SqlCommand cmd = new SqlCommand(sql);
            //cmd.Parameters.AddWithValue("@AgentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));

            //DataSet ds = DbProvider.GetDataSet(cmd);

            //Repeater1.DataSource = ds;
            //Repeater1.DataBind();

            //string sql2 = @"select  * from Lead";

            //SqlCommand cmd2 = new SqlCommand(sql2);

            //DataSet ds2 = DbProvider.GetDataSet(cmd2);

            //Repeater2.DataSource = ds2;
            //Repeater2.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        protected void CopyLid_Click(object sender, ImageClickEventArgs e)
        {
            var x = 1;
        }    
        
        //protected void DeleteTask_Command(object sender, CommandEventArgs e)
        //{
        //    string strDel = "delete from Tasks where ID = @ID ";
        //    SqlCommand cmdDel = new SqlCommand(strDel);
        //    cmdDel.Parameters.AddWithValue("@ID", e.CommandArgument);
        //    if (DbProvider.ExecuteCommand(cmdDel) <= 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
        //        FormError_label.Text = "* התרחשה שגיאה";
        //        FormError_label.Visible = true;
        //    }
        //    PopUpTasksList_Click(sender,null);
        //}
        //protected void PopUpTasksList_Click(object sender, EventArgs e)
        //{
        //    OpenTasksList.Visible = true;
        //    SqlCommand cmdSelectTasks = new SqlCommand("select t.ID, Text,ts.Status,CONVERT(varchar,PerformDate, 104) as PerformDate from Tasks t left join TaskStatuses ts on t.Status = ts.ID where LeadID = @ID");
        //    cmdSelectTasks.Parameters.AddWithValue("@ID", Request.QueryString["LeadID"]);
        //    DataSet ds = DbProvider.GetDataSet(cmdSelectTasks);
        //    Repeater2.DataSource = ds;
        //    Repeater2.DataBind();
        //}

        //protected void FirstStatus_Click(object sender, EventArgs e)
        //{
        //    bool isOpen = DivRadioButtonFirstStatus.Visible;
        //    if (isOpen == false)
        //    {
        //        DivRadioButtonFirstStatus.Visible = true;
        //        DivRadioButtonSecondStatus.Visible = false;


        //        //SpanLeadManagement.Style.Add("color", "#2da9fd");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Blue_Button.png";
        //    }
        //    else
        //    {
        //        DivRadioButtonFirstStatus.Visible = false;
        //        //SpanLeadManagement.Style.Add("color", "#0f325e");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Slide_Button.png";
        //    }

        //}
        //protected void SecondStatus_Click(object sender, EventArgs e)
        //{
        //    bool isOpen = DivRadioButtonSecondStatus.Visible;
        //    if (isOpen == false)
        //    {
        //        DivRadioButtonSecondStatus.Visible = true;
        //        DivRadioButtonFirstStatus.Visible = false;

        //        //SpanLeadManagement.Style.Add("color", "#2da9fd");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Blue_Button.png";
        //    }
        //    else
        //    {
        //        DivRadioButtonSecondStatus.Visible = false;
        //        //SpanLeadManagement.Style.Add("color", "#0f325e");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Slide_Button.png";
        //    }

        //}
        //protected void Gender_Click(object sender, EventArgs e)
        //{
        //    bool isOpen = DivRadioButtonGender.Visible;
        //    if (isOpen == false)
        //    {
        //        DivRadioButtonGender.Visible = true;

        //        //SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
        //        //DataSet ds = DbProvider.GetDataSet(cmd);
        //        //RadioButttonGender.DataSource = ds;
        //        //RadioButttonGender.DataTextField = "userFirstName";
        //        //RadioButttonGender.DataValueField = "IDUser";
        //        //RadioButttonGender.DataBind();
        //        //SpanLeadManagement.Style.Add("color", "#2da9fd");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Blue_Button.png";
        //    }
        //    else
        //    {
        //        DivRadioButtonGender.Visible = false;
        //        //SpanLeadManagement.Style.Add("color", "#0f325e");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Slide_Button.png";
        //    }

        //}


        //protected void LineBusiness_Click(object sender, EventArgs e)
        //{
        //    bool isOpen = DivRBLineBusiness.Visible;
        //    if (isOpen == false)
        //    {
        //        DivRBLineBusiness.Visible = true;

        //        //SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
        //        //DataSet ds = DbProvider.GetDataSet(cmd);
        //        //RadioButtonLineBusiness.DataSource = ds;
        //        //RadioButtonLineBusiness.DataTextField = "userFirstName";
        //        //RadioButtonLineBusiness.DataValueField = "IDUser";
        //        //RadioButtonLineBusiness.DataBind();
        //        //SpanLeadManagement.Style.Add("color", "#2da9fd");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Blue_Button.png";
        //    }
        //    else
        //    {
        //        DivRBLineBusiness.Visible = false;
        //        //SpanLeadManagement.Style.Add("color", "#0f325e");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Slide_Button.png";
        //    }

        //}
        //protected void PartnerFamilyStatus_Click(object sender, EventArgs e)
        //{
        //    bool isOpen =DivRBPartnerFamilyStatus.Visible;
        //    if (isOpen == false)
        //    {
        //        DivRBPartnerFamilyStatus.Visible = true;

        //        //SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
        //        //DataSet ds = DbProvider.GetDataSet(cmd);
        //        //RadioButtonLineBusiness.DataSource = ds;
        //        //RadioButtonLineBusiness.DataTextField = "userFirstName";
        //        //RadioButtonLineBusiness.DataValueField = "IDUser";
        //        //RadioButtonLineBusiness.DataBind();
        //        //SpanLeadManagement.Style.Add("color", "#2da9fd");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Blue_Button.png";
        //    }
        //    else
        //    {
        //        DivRBPartnerFamilyStatus.Visible = false;
        //        //SpanLeadManagement.Style.Add("color", "#0f325e");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Slide_Button.png";
        //    }

        //}

        //protected void PartnerLineBusiness_Click(object sender, EventArgs e)
        //{
        //    bool isOpen = DivRBPartnerLineBusiness.Visible;
        //    if (isOpen == false)
        //    {
        //        DivRBPartnerLineBusiness.Visible = true;

        //        //SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
        //        //DataSet ds = DbProvider.GetDataSet(cmd);
        //        //RadioButtonLineBusiness.DataSource = ds;
        //        //RadioButtonLineBusiness.DataTextField = "userFirstName";
        //        //RadioButtonLineBusiness.DataValueField = "IDUser";
        //        //RadioButtonLineBusiness.DataBind();
        //        //SpanLeadManagement.Style.Add("color", "#2da9fd");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Blue_Button.png";
        //    }
        //    else
        //    {
        //        DivRBPartnerLineBusiness.Visible = false;
        //        //SpanLeadManagement.Style.Add("color", "#0f325e");
        //        //ImgLeadManagement.Src = "images/icons/Arrow_Slide_Button.png";
        //    }

        //}
        //protected void BtnSourceLead_Click(object sender, EventArgs e)
        //{
        //    bool isOpen = DivRBSourceLead.Visible;
        //    if (isOpen == false)
        //    {
        //        DivRBSourceLead.Visible = true;

        //        //SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
        //        //DataSet ds = DbProvider.GetDataSet(cmd);
        //        //RadioButttonSourceLead.DataSource = ds;
        //        //RadioButttonSourceLead.DataTextField = "userFirstName";
        //        //RadioButttonSourceLead.DataValueField = "IDUser";
        //        //RadioButttonSourceLead.DataBind();

        //    }
        //    else
        //    {
        //        DivRBSourceLead.Visible = false;

        //    }

        //}
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
            SqlCommand cmd = new SqlCommand("delete from Lead where ID = @ID");
            cmd.Parameters.AddWithValue("@ID", Request.QueryString["LeadID"]);
            if (DbProvider.ExecuteCommand(cmd) <= 0)
            {
                FormError_label.Text = "* התרחשה שגיאה";
                FormError_label.Visible = true;
                FormErrorBottom_label.Text = "* התרחשה שגיאה";
                FormErrorBottom_label.Visible = true;
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect("Leads.aspx");
            }

        }

        protected void ExportNewContact_Click(object sender, ImageClickEventArgs e)
        {
            bool success = funcSaveNewContact();
            if (!success)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                Response.Redirect("Contact.aspx?ContactID=" + Request.QueryString["LeadID"]);
            }
        }

        //public bool funcSaveNewContact()
        //{
        //    int ErrorCount = 0;
        //    FormError_label.Visible = false;
        //    //שדות חובה כשעודים המרה לאיש קשר שם מלא מין תאריך לידה ת.זץ פרטים אישיים עיר האם קיים נכס בבעלות הלקוח כתובת הנכס 
        //   //- האם קיים נכס בבעלות הלקוח חייב להיות מסומן עם ויtodo
        //    if (FirstName.Value == "")
        //    {
        //        ErrorCount++;
        //        FormError_label.Visible = true;
        //        FormError_label.Text = "יש להזין שם פרטי";
        //    }
        //    else if (LastName.Value == "")
        //    {
        //        ErrorCount++;
        //        FormError_label.Visible = true;
        //        FormError_label.Text = "יש להזין שם משפחה";
        //    }
        //    else if (SelectGender.Value == "")
        //    {
        //        ErrorCount++;
        //        FormError_label.Visible = true;
        //        FormError_label.Text = "יש להזין מין";
        //    }
        //    else if (DateBirth.Value == "")
        //    {
        //        ErrorCount++;
        //        FormError_label.Visible = true;
        //        FormError_label.Text = "יש להזין תאריך לידה";
        //    }
        //    else if (DateTime.Parse(DateBirth.Value) > DateTime.Now)
        //    {
        //        ErrorCount++;
        //        FormError_label.Visible = true;
        //        FormError_label.Text = "יש להזין תאריך לידה תקין";
        //    }
        //    else if (Tz.Value == "")
        //    {
        //        ErrorCount++;
        //        FormError_label.Visible = true;
        //        FormError_label.Text = "יש להזין ת.ז";
        //    }
        //    else if (Tz.Value.Length != 9)
        //    {
        //        ErrorCount++;
        //        FormError_label.Visible = true;
        //        FormError_label.Text = "יש להזין ת.ז תקינה";
        //    }

        //    if (ErrorCount == 0)
        //    {
        //        string sql = @"  
        //                         insert into  Contact  select FirstName,LastName,DateBirth,Gender,tz,IssuanceDateTz,BusinessLineBusiness,BusinessGrossSalary,Phone1
        //                                         ,Email,Note,Address,AgentID,GETDATE()
        //                                               from Lead
        //	                    where ID=@LeadID";



        //        SqlCommand cmd = new SqlCommand(sql);
        //        cmd.Parameters.AddWithValue("@LeadID", Request.QueryString["LeadID"]);

        //        if (DbProvider.ExecuteCommand(cmd) > 0)
        //        {
        //            //מחיקה של ליד
        //            SqlCommand cmdDelete = new SqlCommand("delete from Lead where id=@LeadID");
        //            cmdDelete.Parameters.AddWithValue("@LeadID", Request.QueryString["LeadID"]);
        //            if (DbProvider.ExecuteCommand(cmdDelete) <= 0)
        //            {
        //                FormError_label.Text = "* התרחשה שגיאה";
        //                FormError_label.Visible = true;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            FormError_label.Text = "התרחשה שגיאה";
        //            FormError_label.Visible = true;
        //        }
        //    }
        //    return false;
        //}


        public bool funcSaveNewContact()
        {
            int ErrorCount = 0;
            FormError_label.Visible = false;
            FormErrorBottom_label.Visible = false;

            //שדות חובה כשעושים המרה לאיש קשר שם מלא מין תאריך לידה ת.ז. פרטים אישיים עיר האם קיים נכס בבעלות הלקוח כתובת הנכס 
            //- האם קיים נכס בבעלות הלקוח חייב להיות מסומן עם ויtodo
            if (FirstName.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין שם פרטי";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין שם פרטי";
            }
            else if (LastName.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין שם משפחה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין שם משפחה";
            }
            else if (SelectGender.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מין";
            }
            else if (DateBirth.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין תאריך לידה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין תאריך לידה";
            }
            //else if (DateTime.ParseExact(DateBirth.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture) > DateTime.Now)
                else if (DateTime.Parse(DateBirth.Value) > DateTime.Now)
                    {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין תאריך לידה תקין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין תאריך לידה תקין";
            }
            else if (Tz.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין ת.ז";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין ת.ז";
            }
            else if (Tz.Value.Length != 9)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין ת.ז תקינה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין ת.ז תקינה";
            }
            else if(Helpers.insuredTzExist(Tz.Value, long.Parse(Request.QueryString["LeadID"])) == "true")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "ת.ז קיימת במערכת";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "ת.ז קיימת במערכת";
                return false;
            }
            else if (Address.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין כתובת";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין כתובת";
            }
          
            else if (SelectFamilyStatus.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש לבחור מצב משפחתי";
            }
            else if (BdiValidity.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "תקין/ לא תקין BDI יש לבחור";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "תקין/ לא תקין BDI יש לבחור";
            }
            else if (BdiValidity.SelectedIndex == 2 && InvalidBdiReason.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין סיבה לאי תקינות";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין סיבה לאי תקינות";
            }
            else if (Phone1.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מספר טלפון";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מספר טלפון";
                return false;
            }
            else if (Phone1.Value.Length < 9 || Phone1.Value.Substring(0, 1) != "0")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מספר טלפון תקין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מספר טלפון תקין";
                return false;
            }
            else if (Helpers.insuredPhoneExist(Phone1.Value, long.Parse(Request.QueryString["LeadID"])) == "true")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "מספר הטלפון קיים במערכת";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "מספר הטלפון קיים במערכת";
                return false;
            }
            else if (SelectSourceLead.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש לבחור את מקור הליד";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש לבחור את מקור הליד";
            }
            else if (BusinessName.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין שם העסק";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין שם העסק";
            }
            else if (BusinessSeniority.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין ותק במקום העבודה הנוכחי";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין ותק במקום העבודה הנוכחי";
            }
            //else if (PrevBusinessSeniority.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_label.Visible = true;
            //    FormError_label.Text = "יש להזין ותק במקום העבודה הקודם";
            //    FormErrorBottom_label.Visible = true;
            //    FormErrorBottom_label.Text = "יש להזין ותק במקום העבודה הקודם";
            //}
            else if (SelectBusinessLineBusiness.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש לבחור מצב תעסוקתי";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "ייש לבחור מצב תעסוקתי";
            }
            else if (BusinessProfession.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש לבחור מקצוע";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש לבחור מקצוע";
            }
            else if (BusinessGrossSalary.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין שכר ברוטו";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין שכר ברוטו";
            }
            //else if (BusinessCity.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_label.Visible = true;
            //    FormError_label.Text = "יש להזין עיר בה ממוקם העסק";
            //    FormErrorBottom_label.Visible = true;
            //    FormErrorBottom_label.Text = "יש להזין עיר בה ממוקם העסק";
            //}
            //else if (SelectBusinessLineBusiness.SelectedIndex == 0)
            //{
            //    ErrorCount++;
            //    FormError_label.Visible = true;
            //    FormError_label.Text = "יש לבחור מצב תעסוקתי";
            //}            

           
  
            else if (SelectHaveAsset.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש לבחור האם קיים נכס בבעלות הלקוח";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש לבחור האם קיים נכס בבעלות הלקוח";
            }
            else if (SelectHaveAsset.SelectedIndex == 1 && SelectHaveMortgageOnAsset.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש לבחור האם קיים משכנתא על הנכס";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש לבחור האם קיים משכנתא על הנכס";
            }
            if (ErrorCount == 0)
            {

                string sql = @" Update Lead set 
                    FirstName=@FirstName
,LastName=@LastName
,GenderID=@GenderID
,DateBirth=@DateBirth
,Address=@Address
,FamilyStatusID=@FamilyStatusID
,Tz=@Tz
,IssuanceDateTz=@IssuanceDateTz
,IsValidBdi=@IsValidBdi
,InvalidBdiReason=@InvalidBdiReason
,Phone1=@Phone1
,Phone2=@Phone2
,Email=@Email
,FirstStatusLeadID=@FirstStatusLeadID
,SecondStatusLeadID=@SecondStatusLeadID
,SourceLeadID=@SourceLeadID
,InterestedIn=@InterestedIn
,TrackingTime=@TrackingTime
,Note=@Note
,AgentID=@AgentID
,BusinessName=@BusinessName
,BusinessSeniority=@BusinessSeniority
,PrevBusinessSeniority=@PrevBusinessSeniority
,BusinessProfession=@BusinessProfession
,BusinessCity=@BusinessCity
,BusinessPhone=@BusinessPhone
,BusinessGrossSalary=@BusinessGrossSalary
,BusinessLineBusiness=@BusinessLineBusiness
,PartnerLineBusiness=@PartnerLineBusiness
,PartnerName=@PartnerName
,PartnerGrossSalary=@PartnerGrossSalary
,PartnerAge=@PartnerAge
,PartnerSeniority=@PartnerSeniority
,HaveAsset=@HaveAsset
,AssetValue=@AssetValue
,AssetType=@AssetType
,AssetAddress=@AssetAddress
,HaveMortgageOnAsset=@HaveMortgageOnAsset
,MortgageAmount=@MortgageAmount
,MonthlyRepaymentAmount=MonthlyRepaymentAmount
,LendingBank=@LendingBank
,PurposeTest=@PurposeTest
,RequestedLoanAmount=@RequestedLoanAmount
,PurposeLoan =@PurposeLoan
,MortgageBalance=@MortgageBalance
,IsContact=1
,ConvertContactDate=getdate()
";


                if (HttpContext.Current.Session["FirstStatusLeadID"].ToString() != SelectFirstStatus.Value)
                {
                    sql += " ,DateChangeFirstStatus = GETDATE()";
                }
                sql += " where ID = @LeadID";
                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@FirstName", FirstName.Value);
                cmd.Parameters.AddWithValue("@LastName", LastName.Value);
                cmd.Parameters.AddWithValue("@GenderID", string.IsNullOrEmpty(SelectGender.Value) ? (object)DBNull.Value : SelectGender.Value);
                cmd.Parameters.AddWithValue("@DateBirth", string.IsNullOrEmpty(DateBirth.Value) ? (object)DBNull.Value : DateBirth.Value);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(Address.Value) ? (object)DBNull.Value : Address.Value);
                cmd.Parameters.AddWithValue("@FamilyStatusID", string.IsNullOrEmpty(SelectFamilyStatus.Value) ? (object)DBNull.Value : SelectFamilyStatus.Value);
                cmd.Parameters.AddWithValue("@Tz", string.IsNullOrEmpty(Tz.Value) ? (object)DBNull.Value : Tz.Value);
                cmd.Parameters.AddWithValue("@IssuanceDateTz", string.IsNullOrEmpty(IssuanceDateTz.Value) ? (object)DBNull.Value : IssuanceDateTz.Value );
                //cmd.Parameters.AddWithValue("@IsValidIssuanceDateTz", IsValidIssuanceDateTz.Checked == true ? 1 : 0);
                cmd.Parameters.AddWithValue("@IsValidBdi", /*IsValidBdi.Checked == true*/ BdiValidity.SelectedIndex);
                cmd.Parameters.AddWithValue("@InvalidBdiReason", string.IsNullOrEmpty(InvalidBdiReason.Value) ? (object)DBNull.Value : InvalidBdiReason.Value);
                cmd.Parameters.AddWithValue("@Phone1", Phone1.Value);
                cmd.Parameters.AddWithValue("@Phone2", string.IsNullOrEmpty(Phone2.Value) ? (object)DBNull.Value : Phone2.Value);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email.Value) ? (object)DBNull.Value : Email.Value);
                cmd.Parameters.AddWithValue("@FirstStatusLeadID", SelectFirstStatus.Value);
                //DateChangeFirstStatus

                cmd.Parameters.AddWithValue("@SecondStatusLeadID", string.IsNullOrEmpty(SelectSecondStatus.Value) ? (object)DBNull.Value : int.Parse(SelectSecondStatus.Value));
                cmd.Parameters.AddWithValue("@SourceLeadID", string.IsNullOrEmpty(SelectSourceLead.Value) ? (object)DBNull.Value : int.Parse(SelectSourceLead.Value));
                cmd.Parameters.AddWithValue("@InterestedIn", string.IsNullOrEmpty(InterestedIn.Value) ? (object)DBNull.Value : InterestedIn.Value);
                cmd.Parameters.AddWithValue("@TrackingTime", string.IsNullOrEmpty(TrackingTime.Value) ? (object)DBNull.Value : DateTime.Parse(TrackingTime.Value ));

                cmd.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note.Value) ? (object)DBNull.Value : Note.Value);



                try
                {
                    Pageinit.CheckManagerPermissions();
                    cmd.Parameters.AddWithValue("@AgentID", long.Parse(HiddenAgentID.Value/*HttpContext.Current.Session["AgentID"].ToString()*/));
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
                //    cmd.Parameters.AddWithValue("@BusinessEmail", string.IsNullOrEmpty(BusinessEmail.Value) ? (object)DBNull.Value : BusinessEmail.Value);
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


               
                cmd.Parameters.AddWithValue("@LeadID", Request.QueryString["LeadID"]);

                if (DbProvider.ExecuteCommand(cmd) > 0)
                {
                    SqlCommand cmdHistory = new SqlCommand("INSERT INTO ActivityHistory (AgentID, Details, CreateDate, Show) VALUES (@agentID, @details, GETDATE(), 1)");
                    cmdHistory.Parameters.AddWithValue("@agentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                    cmdHistory.Parameters.AddWithValue("@details", ("המרה לאיש קשר " + FirstName.Value + " " + LastName.Value));
                    DbProvider.ExecuteCommand(cmdHistory);
                    return true;
                }
                else
                {
                    FormError_label.Text = "התרחשה שגיאה";
                    FormError_label.Visible = true;
                    FormErrorBottom_label.Text = "התרחשה שגיאה";
                    FormErrorBottom_label.Visible = true;
                }
            }
            return false;
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
            //StatusPopUp.Style.("", "");
        }

        //protected void RadioButttonFirstStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DivRadioButtonFirstStatus.Visible = false;
        //    BtnFirstStatus.Text = RadioButttonFirstStatus.SelectedItem.Text;
        //}
        //protected void RadioButttonSecondStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DivRadioButtonSecondStatus.Visible = false;
        //    BtnSecondStatus.Text = RadioButttonSecondStatus.SelectedItem.Text;
        //}

        //protected void RadioButttonGender_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DivRadioButtonGender.Visible = false;
        //    BtnGender.Text = RadioButttonGender.SelectedItem.Text;
        //}
        //protected void RadioButttonLineBusiness_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DivRBLineBusiness.Visible = false;
        //    BtnLineBusiness.Text = RadioButtonLineBusiness.SelectedItem.Text;
        //}
        //protected void RadioButttonPartnerLineBusiness_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DivRBPartnerLineBusiness.Visible = false;
        //    PartnerLineBusiness.Text = RadioButtonPartnerLineBusiness.SelectedItem.Text;
        //}
        //protected void RadioButttonFamilyStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DivRBPartnerFamilyStatus.Visible = false;
        //    PartnerFamilyStatus.Text = RadioButttonPartnerFamilyStatus.SelectedItem.Text;
        //}
        //protected void RadioButttonFamilyStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DivRBFamilyStatus.Visible = false;
        //    BtnFamilyStatus.Text = RadioButttonFamilyStatus.SelectedItem.Text;
        //}
        //protected void RadioButttonSourceLead_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DivRBSourceLead.Visible = false;
        //    BtnSourceLead.Text = RadioButttonSourceLead.SelectedItem.Text;
        //}

        protected void btn_save_Click(object sender, EventArgs e)
        {
            bool success = funcSave(sender, e);
            if (!success)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                //System.Web.HttpContext.Current.Response.Redirect("Leads.aspx");

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                loadData();
            }
        }
        public bool funcSave(object sender, EventArgs e)
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
                return false;
            }
            if (LastName.Value == "")
            { 
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין שם משפחה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין שם משפחה";
                return false;
            }            
            if (Phone1.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מספר טלפון";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מספר טלפון";
                return false;
            }
            if (Phone1.Value.Length < 9 || Phone1.Value.Substring(0, 1) != "0")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מספר טלפון תקין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מספר טלפון תקין";
                return false;
            }
            if (Helpers.insuredPhoneExist(Phone1.Value, long.Parse(Request.QueryString["LeadID"])) == "true")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "מספר הטלפון קיים במערכת";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "מספר הטלפון קיים במערכת";
                return false;
            }
            if (Phone2.Value != "" && (Phone2.Value.Length < 9 || Phone2.Value.Substring(0, 1) != "0"))
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מספר טלפון תקין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מספר טלפון תקין";
                return false;
            }
            if (Email.Value != "" && !Email.Value.Contains("@"))
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין אימייל תקין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין אימייל תקין";
                return false;
            }
             if (DateBirth.Value != "" && DateTime.Parse(DateBirth.Value) > DateTime.Now)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין תאריך לידה תקין";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין תאריך לידה תקין";
            }
            if (Tz.Value != "" && Tz.Value.Length != 9)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין ת.ז תקינה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין ת.ז תקינה";
            }
            if (Tz.Value != "" && Helpers.insuredTzExist(Tz.Value, long.Parse(Request.QueryString["LeadID"])) == "true")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "ת.ז קיימת במערכת";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "ת.ז קיימת במערכת";
                return false;
            }
            //סטטוס מעקב לחייב למלא תאריך
            if(SelectFirstStatus.SelectedIndex == 8 && TrackingTime.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין זמן מעקב";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין זמן מעקב";
                return false;
            }
            //סטטוס לא רלוונטי לחייב למלא סטטוס משני
            if (SelectFirstStatus.SelectedIndex == 7 && SelectSecondStatus.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין סטטוס משני";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין סטטוס משני";
                return false;
            }
            if (BdiValidity.SelectedIndex == 2 && InvalidBdiReason.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין סיבה לאי תקינות";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין סיבה לאי תקינות";
            }
            if (ErrorCount == 0)
            {
                string sql = @" Update Lead set 
                    FirstName=@FirstName
,LastName=@LastName
,GenderID=@GenderID
,DateBirth=@DateBirth
,Address=@Address
,FamilyStatusID=@FamilyStatusID
,Tz=@Tz
,IssuanceDateTz=@IssuanceDateTz
,IsValidBdi=@IsValidBdi
,InvalidBdiReason=@InvalidBdiReason
,Phone1=@Phone1
,Phone2=@Phone2
,Email=@Email
,FirstStatusLeadID=@FirstStatusLeadID
,SecondStatusLeadID=@SecondStatusLeadID
,SourceLeadID=@SourceLeadID
,InterestedIn=@InterestedIn
,TrackingTime=@TrackingTime
,Note=@Note
,AgentID=@AgentID
,BusinessName=@BusinessName
,BusinessSeniority=@BusinessSeniority
,PrevBusinessSeniority=@PrevBusinessSeniority
,BusinessProfession=@BusinessProfession
,BusinessCity=@BusinessCity
,BusinessPhone=@BusinessPhone
,BusinessGrossSalary=@BusinessGrossSalary
,BusinessLineBusiness=@BusinessLineBusiness
,PartnerLineBusiness=@PartnerLineBusiness
,PartnerName=@PartnerName
,PartnerGrossSalary=@PartnerGrossSalary
,PartnerAge=@PartnerAge
,PartnerSeniority=@PartnerSeniority
,HaveAsset=@HaveAsset
,AssetValue=@AssetValue
,AssetType=@AssetType
,AssetAddress=@AssetAddress
,HaveMortgageOnAsset=@HaveMortgageOnAsset
,MortgageAmount=@MortgageAmount
,MonthlyRepaymentAmount=MonthlyRepaymentAmount
,LendingBank=@LendingBank
,PurposeTest=@PurposeTest
,RequestedLoanAmount=@RequestedLoanAmount
,PurposeLoan =@PurposeLoan
,MortgageBalance=@MortgageBalance
";

                if (HttpContext.Current.Session["FirstStatusLeadID"].ToString() != SelectFirstStatus.Value)
                {
                    sql += " ,DateChangeFirstStatus = GETDATE()";
                }
                sql += " where ID = @LeadID";
                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@FirstName", FirstName.Value);
                cmd.Parameters.AddWithValue("@LastName", LastName.Value);
                cmd.Parameters.AddWithValue("@GenderID", string.IsNullOrEmpty(SelectGender.Value) ? (object)DBNull.Value : SelectGender.Value);
                cmd.Parameters.AddWithValue("@DateBirth", string.IsNullOrEmpty(DateBirth.Value) ? (object)DBNull.Value : DateBirth.Value);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(Address.Value) ? (object)DBNull.Value : Address.Value);
                cmd.Parameters.AddWithValue("@FamilyStatusID", string.IsNullOrEmpty(SelectFamilyStatus.Value) ? (object)DBNull.Value : SelectFamilyStatus.Value);
                cmd.Parameters.AddWithValue("@Tz", string.IsNullOrEmpty(Tz.Value) ? (object)DBNull.Value : Tz.Value);
                cmd.Parameters.AddWithValue("@IssuanceDateTz", string.IsNullOrEmpty(IssuanceDateTz.Value) ? (object)DBNull.Value : DateTime.Parse(IssuanceDateTz.Value));
                //cmd.Parameters.AddWithValue("@IsValidIssuanceDateTz", IsValidIssuanceDateTz.Checked == true ? 1 : 0);
                cmd.Parameters.AddWithValue("@IsValidBdi", /*IsValidBdi.Checked == true*/ BdiValidity.SelectedIndex);
                cmd.Parameters.AddWithValue("@InvalidBdiReason", string.IsNullOrEmpty(InvalidBdiReason.Value) ? (object)DBNull.Value : InvalidBdiReason.Value);
                cmd.Parameters.AddWithValue("@Phone1", Phone1.Value);
                cmd.Parameters.AddWithValue("@Phone2", string.IsNullOrEmpty(Phone2.Value) ? (object)DBNull.Value : Phone2.Value);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email.Value) ? (object)DBNull.Value : Email.Value);
                cmd.Parameters.AddWithValue("@FirstStatusLeadID", SelectFirstStatus.Value);
                //DateChangeFirstStatus

                cmd.Parameters.AddWithValue("@SecondStatusLeadID", string.IsNullOrEmpty(SelectSecondStatus.Value) ? (object)DBNull.Value : int.Parse(SelectSecondStatus.Value));
                cmd.Parameters.AddWithValue("@SourceLeadID", string.IsNullOrEmpty(SelectSourceLead.Value) ? (object)DBNull.Value : int.Parse(SelectSourceLead.Value));
                cmd.Parameters.AddWithValue("@InterestedIn", string.IsNullOrEmpty(InterestedIn.Value) ? (object)DBNull.Value : InterestedIn.Value);
                cmd.Parameters.AddWithValue("@TrackingTime", string.IsNullOrEmpty(TrackingTime.Value) ? (object)DBNull.Value : DateTime.Parse(TrackingTime.Value));

                cmd.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(Note.Value) ? (object)DBNull.Value : Note.Value);



                try
                {
                    Pageinit.CheckManagerPermissions();
                    //heni - 13.10.24 - אין מענה פעם שלישית להעביר ליד למנהל

                    SqlCommand cmdBranchManager = new SqlCommand("SELECT CASE WHEN a.Type = 3 THEN a.ID ELSE a.ParentID END as ManagerID FROM ArvootManagers a WHERE a.ID = @AgentID");
                    cmdBranchManager.Parameters.AddWithValue("@AgentID", HiddenAgentID.Value);
                    string branchAgent = DbProvider.GetOneParamValueString(cmdBranchManager);
                    cmd.Parameters.AddWithValue("@AgentID", int.Parse(SelectFirstStatus.Value) == 5 ? /*(object)DBNull.Value*/ long.Parse(branchAgent) : long.Parse(HiddenAgentID.Value/*HttpContext.Current.Session["AgentID"].ToString()*/));
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
            //    cmd.Parameters.AddWithValue("@BusinessEmail", string.IsNullOrEmpty(BusinessEmail.Value) ? (object)DBNull.Value : BusinessEmail.Value);
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


                cmd.Parameters.AddWithValue("@LeadID", long.Parse(Request.QueryString["LeadID"]));

              //  long LeadID = DbProvider.GetOneParamValueLong(cmd);
                if (DbProvider.ExecuteCommand(cmd) > 0)
                { 
                    if (HttpContext.Current.Session["FirstStatusLeadID"].ToString() != SelectFirstStatus.Value)
                    {
                        SqlCommand cmdHistory = new SqlCommand("INSERT INTO ActivityHistory (AgentID, Details, CreateDate, Show) VALUES (@agentID, @details, GETDATE(), 1)");
                        cmdHistory.Parameters.AddWithValue("@agentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                        cmdHistory.Parameters.AddWithValue("@details", ("שינוי סטטוס ליד " + FirstName.Value + " " + LastName.Value + " - " + SelectFirstStatus.Items[SelectFirstStatus.SelectedIndex].Text));
                        DbProvider.ExecuteCommand(cmdHistory);
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
                            cmdTasks.Parameters.AddWithValue("@LeadID", long.Parse(Request.QueryString["LeadID"]));
                            cmdTasks.Parameters.AddWithValue("@PerformDate", DateTime.Parse(TrackingTime.Value));

                            DbProvider.ExecuteCommand(cmdTasks);

                            string sqlAlert = "INSERT INTO Alerts (AgentID, Text, CreationDate, DisplayDate) Values (@AgentID, @Text, GETDATE(), @DisplayDate)";
                            SqlCommand cmdAlert = new SqlCommand(sqlAlert);
                            cmdAlert.Parameters.AddWithValue("@AgentID", HiddenAgentID.Value);
                            cmdAlert.Parameters.AddWithValue("@Text", "מעקב ליד " + FirstName.Value + " " + LastName.Value + " " + Phone1.Value);
                            cmdAlert.Parameters.AddWithValue("@DisplayDate", DateTime.Parse(TrackingTime.Value));
                            DbProvider.ExecuteCommand(cmdAlert);

                        }
                        return true;
                    }

                    return true;
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

            return false;
        }
        //public bool funcSaveTask(object sender, EventArgs e)
        //{
        //    //שם פרטי שם משפחה תאריך לידה תז טלפון אימייל סטטוס ראשי
        //    int ErrorCount = 0;
        //    FormErrorTask_lable.Visible = false;
        //    if (TextTask.Value == "")
        //    {
        //        ErrorCount++;
        //        FormErrorTask_lable.Visible = true;
        //        FormErrorTask_lable.Text = "יש להזין תוכן";
        //        return false;
        //    }
        //    if (Date.Value == "")
        //    {
        //        ErrorCount++;
        //        FormErrorTask_lable.Visible = true;
        //        FormErrorTask_lable.Text = "יש להזין תאריך";
        //        return false;
        //    }
        //    if (SelectStatusTask.Value == "")
        //    {
        //        ErrorCount++;
        //        FormErrorTask_lable.Visible = true;
        //        FormErrorTask_lable.Text = "יש להזין סטטוס";
        //        return false;
        //    }




        //    if (ErrorCount == 0)
        //    {
        //        string sql = @" INSERT INTO [Tasks]( Text
        //        ,Status
        //        ,LeadID
        //        ,PerformDate)
        //         VALUES (
	       //       @Text
        //         ,@Status
        //         ,@LeadID
        //         ,@PerformDate)";

        //        SqlCommand cmd = new SqlCommand(sql);

        //        cmd.Parameters.AddWithValue("@Text", TextTask.Value);
        //        cmd.Parameters.AddWithValue("@Status", SelectStatusTask.Value);
        //        cmd.Parameters.AddWithValue("@LeadID", Request.QueryString["LeadID"]);
        //        cmd.Parameters.AddWithValue("@PerformDate", Date.Value);

        //        if (DbProvider.ExecuteCommand(cmd) > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
        //            FormError_label.Text = "* התרחשה שגיאה";
        //            FormError_label.Visible = true;
        //        }

        //    }

        //    return false;
        //}

        //protected void OpenNewTask_Click(object sender, EventArgs e)
        //{

        //    bool success = funcSaveTask(sender, e);
        //    if (!success)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
        //    }
        //    else
        //    {
        //        TaskDiv.Visible = false;
        //    }
        //}
      
    }
}