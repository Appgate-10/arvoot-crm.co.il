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
    public partial class _contact : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "חפש קובץ";
        public string StrSrc { get { return strSrc; } }
        public string ListPageUrl = "Contacts.aspx";
        //באיש קשר התעודה זהות  ייחודית todo

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {
                Pageinit.CheckManagerPermissions();



                SqlCommand cmdSourceLead = new SqlCommand("SELECT * FROM SourceLead");
                DataSet dsSourceLead = DbProvider.GetDataSet(cmdSourceLead);
                SelectSourceLead.DataSource = dsSourceLead;
                SelectSourceLead.DataTextField = "Text";
                SelectSourceLead.DataValueField = "ID";
                SelectSourceLead.DataBind();
                SelectSourceLead.Items.Insert(0, new ListItem("בחר", ""));


                SqlCommand cmdEmploymentStatus = new SqlCommand("SELECT * FROM EmploymentStatus");
                DataSet dsEmploymentStatus = DbProvider.GetDataSet(cmdEmploymentStatus);
                SelectBusinessEmploymentStatus.DataSource = dsEmploymentStatus;
                SelectBusinessEmploymentStatus.DataTextField = "Name";
                SelectBusinessEmploymentStatus.DataValueField = "ID";
                SelectBusinessEmploymentStatus.DataBind();
                SelectBusinessEmploymentStatus.Items.Insert(0, new ListItem("בחר", ""));

                SelectPartnerEmploymentStatus.DataSource = dsEmploymentStatus;
                SelectPartnerEmploymentStatus.DataTextField = "Name";
                SelectPartnerEmploymentStatus.DataValueField = "ID";
                SelectPartnerEmploymentStatus.DataBind();
                SelectPartnerEmploymentStatus.Items.Insert(0, new ListItem("בחר", ""));

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

                //SqlCommand cmdTaskStatuses = new SqlCommand("SELECT * FROM TaskStatuses");
                //DataSet dsTaskStatuses = DbProvider.GetDataSet(cmdTaskStatuses);
                //SelectStatusTask.DataSource = dsTaskStatuses;
                //SelectStatusTask.DataTextField = "Status";
                //SelectStatusTask.DataValueField = "ID";
                //SelectStatusTask.DataBind();

                SqlCommand cmd = new SqlCommand("SELECT * FROM StatusContact");
                DataSet ds = DbProvider.GetDataSet(cmd);
                SelectContactStatus.DataSource = ds;
                SelectContactStatus.DataTextField = "Status";
                SelectContactStatus.DataValueField = "ID";
                SelectContactStatus.DataBind();

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
                           ,Lead.PartnerLineBusiness
                           ,PartnerName,PartnerGrossSalary,PartnerAge,PartnerSeniority
                           ,HaveAsset,Lead.AssetValue,Lead.AssetType,Lead.AssetAddress,HaveMortgageOnAsset
                           ,MortgageAmount,MonthlyRepaymentAmount,LendingBank,PurposeTest,RequestedLoanAmount
                           ,PurposeLoan,MortgageBalance
						   ,Agent.FullName as FullNameAgent,Agent.Phone PhoneAgent,Agent.Email as EmailAgent,StatusContact
                           from Lead
						   left join Agent on Lead.AgentID=Agent.ID
                           inner join FirstStatusLead on Lead.FirstStatusLeadID=FirstStatusLead.ID
                           left join SecondStatusLead on Lead.SecondStatusLeadID=SecondStatusLead.ID
                           where Lead.ID=@LeadID";
            SqlCommand cmdLead = new SqlCommand(sqlLead);

            cmdLead.Parameters.AddWithValue("@LeadID", Request.QueryString["ContactID"]);

            DataTable dtLead = DbProvider.GetDataTable(cmdLead);
            if (dtLead.Rows.Count > 0)
            {

                if (!string.IsNullOrEmpty(dtLead.Rows[0]["DateBirth"].ToString()))
                {
                    DateTime dateOfBirth = Convert.ToDateTime(dtLead.Rows[0]["DateBirth"]);
                    DateTime currentDate = DateTime.Now;
                    int age = currentDate.Year - dateOfBirth.Year;
                    Age.InnerText = age.ToString();
                    DateBirth.Value = (dateOfBirth).ToString("yyyy-MM-dd");
                }
                FullName.InnerText= dtLead.Rows[0]["FirstName"].ToString()+" "+dtLead.Rows[0]["LastName"].ToString();
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
                IssuanceDateTz.Value = string.IsNullOrWhiteSpace(dtLead.Rows[0]["IssuanceDateTz"].ToString()) ? "" : Convert.ToDateTime(dtLead.Rows[0]["IssuanceDateTz"]).ToString("yyyy-MM-dd");
                //IsValidIssuanceDateTz.Checked = Convert.ToBoolean(int.Parse(dtLead.Rows[0]["IsValidIssuanceDateTz"].ToString()));
                BdiValidity.SelectedIndex =int.Parse(dtLead.Rows[0]["IsValidBdi"].ToString());
                InvalidBdiReason.Value = dtLead.Rows[0]["InvalidBdiReason"].ToString();
                Phone1.Value = dtLead.Rows[0]["Phone1"].ToString();
                Phone2.Value = dtLead.Rows[0]["Phone2"].ToString();
                Email.Value = dtLead.Rows[0]["Email"].ToString();

                SelectSourceLead.Value = dtLead.Rows[0]["SourceLeadID"].ToString();
                InterestedIn.Value = dtLead.Rows[0]["InterestedIn"].ToString();
                TrackingTime.Value = string.IsNullOrWhiteSpace(dtLead.Rows[0]["TrackingTime"].ToString()) ? "" : Convert.ToDateTime(dtLead.Rows[0]["TrackingTime"]).ToString("yyyy-MM-ddTHH:mm:ss");
                Note.Value = dtLead.Rows[0]["Note"].ToString();
                BusinessName.Value = dtLead.Rows[0]["BusinessName"].ToString();
                BusinessSeniority.Value = dtLead.Rows[0]["BusinessSeniority"].ToString();
                PrevBusinessSeniority.Value = dtLead.Rows[0]["PrevBusinessSeniority"].ToString();
                BusinessProfession.Value = dtLead.Rows[0]["BusinessProfession"].ToString();
                BusinessCity.Value = dtLead.Rows[0]["BusinessCity"].ToString();
                //BusinessEmail.Value = dtLead.Rows[0]["BusinessEmail"].ToString();

                BusinessPhone.Value = dtLead.Rows[0]["BusinessPhone"].ToString();
                BusinessGrossSalary.Value = dtLead.Rows[0]["BusinessGrossSalary"].ToString();
                SelectBusinessEmploymentStatus.Value = dtLead.Rows[0]["BusinessLineBusiness"].ToString();

                SelectPartnerEmploymentStatus.Value = dtLead.Rows[0]["PartnerLineBusiness"].ToString();
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
                SelectContactStatus.Value = dtLead.Rows[0]["StatusContact"].ToString();

              //  FullNameAgent.InnerText = dtLead.Rows[0]["FullNameAgent"].ToString();
               

            }
            string sql = @"select  Offer.ID, Offer.CreateDate, OfferType.Name as OfferType, Agent.FullName as FullNameAgent ,StatusOffer.Status as StatusOffer
                           from Offer
                           left join OfferType on OfferType.ID = Offer.OfferTypeID
                           left join StatusOffer on StatusOffer.ID = Offer.StatusOfferID 
                           left join Lead on Lead.ID = Offer.LeadID
                           left join Agent on Lead.AgentID=Agent.ID where LeadID = @LeadID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@LeadID", Request.QueryString["ContactID"]);
            DataSet ds = DbProvider.GetDataSet(cmd);

            Repeater2.DataSource = ds;
            Repeater2.DataBind();

            string sqlServiceRequest = @"select s.ID, Invoice,Sum,CONVERT(varchar, s.CreateDate, 104)  CreateDate, p.purpose as PurposeName,
(select sum(SumPayment) from ServiceRequestPayment where ServiceRequestID = s.ID and IsApprovedPayment = 1) as paid, SumCreditOrDenial, IsApprovedCreditOrDenial
from ServiceRequest s 
left join ServiceRequestPurpose p on s.PurposeID = p.ID 
inner join Offer on Offer.ID = s.OfferID
inner join Lead on Lead.ID = Offer.LeadID
where Lead.ID = @LeadID";
            SqlCommand cmdServiceRequest = new SqlCommand(sqlServiceRequest);
            cmdServiceRequest.Parameters.AddWithValue("@LeadID", Request.QueryString["ContactID"]);
            DataTable dtServiceRequest = DbProvider.GetDataTable(cmdServiceRequest);
            Repeater3.DataSource = dtServiceRequest;
            Repeater3.DataBind();




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

        }

        //protected void OpenTask_Click(object sender, ImageClickEventArgs e)
        //{
        //    TaskDiv.Visible = true;

        //}
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
        //        cmd.Parameters.AddWithValue("@LeadID", Request.QueryString["ContactID"]);
        //        cmd.Parameters.AddWithValue("@PerformDate", Date.Value);

        //        if (DbProvider.ExecuteCommand(cmd) > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
        //            FormError_lable.Text = "* התרחשה שגיאה";
        //            FormError_lable.Visible = true;
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
        //protected void DeleteTask_Command(object sender, CommandEventArgs e)
        //{
        //    string strDel = "delete from Tasks where ID = @ID ";
        //    SqlCommand cmdDel = new SqlCommand(strDel);
        //    cmdDel.Parameters.AddWithValue("@ID", e.CommandArgument);
        //    if (DbProvider.ExecuteCommand(cmdDel) <= 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
        //        FormError_lable.Text = "* התרחשה שגיאה";
        //        FormError_lable.Visible = true;
        //    }
        //    PopUpTasksList_Click(sender, null);
        //}
        //protected void CloseTaskPopUp_Click(object sender, EventArgs e)
        //{
        //    TaskDiv.Visible = false;
        //}

        //protected void PopUpTasksList_Click(object sender, EventArgs e)
        //{
        //    OpenTasksList.Visible = true;
        //    SqlCommand cmdSelectTasks = new SqlCommand("select t.ID, Text,ts.Status,CONVERT(varchar,PerformDate, 104) as PerformDate from Tasks t left join TaskStatuses ts on t.Status = ts.ID where LeadID = @ID");
        //    cmdSelectTasks.Parameters.AddWithValue("@ID", Request.QueryString["ContactID"]);
        //    DataSet ds = DbProvider.GetDataSet(cmdSelectTasks);
        //    Repeater3.DataSource = ds;
        //    Repeater3.DataBind();
        //}
        //protected void CloseTasksListPopUp_Click(object sender, EventArgs e)
        //{
        //    OpenTasksList.Visible = false;
        //}
        protected void BtnDetailsOffer_Command(object sender, CommandEventArgs e)
        {
            System.Web.HttpContext.Current.Response.Redirect("OfferEdit.aspx?OfferID=" + e.CommandArgument.ToString());

        }
        protected void Status_Click(object sender, EventArgs e)
        { }


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
        protected void DeleteContact_Click(object sender, ImageClickEventArgs e)
        {
            SqlCommand cmdSelect =  new SqlCommand("select ID from Offer where LeadID = @ID ");
            cmdSelect.Parameters.AddWithValue("@ID", Request.QueryString["ContactID"]);
            DataTable dt =  DbProvider.GetDataTable(cmdSelect);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmdDelServices = new SqlCommand("delete from ServiceRequest where OfferID = @ID");
                    cmdDelServices.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"]);
                    DbProvider.ExecuteCommand(cmdDelServices);
                    SqlCommand cmdDelDocuments = new SqlCommand("delete from OfferDocuments where OfferID = @ID");
                    cmdDelDocuments.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"]);
                    DbProvider.ExecuteCommand(cmdDelDocuments);
                    SqlCommand cmdDelTasks = new SqlCommand("delete from Tasks where OfferID = @ID");
                    cmdDelTasks.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"]);
                    DbProvider.ExecuteCommand(cmdDelTasks);
                }
                SqlCommand cmdDelOffer = new SqlCommand("delete from Offer where LeadID = @ID");
                cmdDelOffer.Parameters.AddWithValue("@ID", Request.QueryString["ContactID"]);
                DbProvider.ExecuteCommand(cmdDelOffer);
            }
           
            
            SqlCommand cmdDelLead = new SqlCommand("delete from Lead where ID = @ID");
            cmdDelLead.Parameters.AddWithValue("@ID", Request.QueryString["ContactID"]);
            if (DbProvider.ExecuteCommand(cmdDelLead) <= 0)
            {
                ExportNewContact_lable.Text = "* התרחשה שגיאה";
                ExportNewContact_lable.Visible = true;
                return;
            }
            else
            {
                Response.Redirect(ListPageUrl);

            }
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
                System.Web.HttpContext.Current.Response.Redirect("Contacts.aspx");
            }
        }
        public bool funcSave(object sender, EventArgs e)
        {
            int ErrorCount = 0;
            FormError_lable.Visible = false;
            if (FirstName.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין שם פרטי";
            }
            else if (LastName.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין שם משפחה";
            }
            else if (SelectGender.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין מין";
            }
            else if (DateBirth.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין תאריך לידה";
            }
            else if (DateTime.Parse(DateBirth.Value) > DateTime.Now)
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין תאריך לידה תקין";
            }
            else if (Tz.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין ת.ז";
            }
            else if (Tz.Value.Length != 9)
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין ת.ז תקינה";
            }
            else if (Helpers.insuredTzExist(Tz.Value, -1) == "true")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "ת.ז קיימת במערכת";
                return false;
            }
            else if (Address.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין כתובת";
            }
            else if (SelectHaveAsset.SelectedIndex == 0)
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש לבחור האם קיים נכס בבעלות הלקוח";
            }
            else if (AssetAddress.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין כתובת עסק";//Gila נראה שאמור להיות כתוב כתובת נכס
            }
          
            /*   else if (SelectFamilyStatus.SelectedIndex == 0)
               {
                   ErrorCount++;
                   ExportNewContact_lable.Visible = true;
                   ExportNewContact_lable.Text = "יש לבחור מצב תעסוקתי";
               }*/
            else if (BdiValidity.SelectedIndex == 0)
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "תקין/ לא תקין BDI יש לבחור";
            }
            else if (BdiValidity.SelectedIndex == 2 && InvalidBdiReason.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין סיבה לאי תקינות";
            }
            else if (BusinessName.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין שם העסק";
            }
            else if (BusinessSeniority.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין ותק במקום העבודה הנוכחי";
            }
            else if (PrevBusinessSeniority.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין ותק במקום העבודה הקודם";
            }
            else if (PrevBusinessSeniority.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין ותק במקום העבודה הקודם";
            }
            else if (BusinessCity.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין עיר בה ממוקם העסק";
            }
            //else if (SelectBusinessLineBusiness.SelectedIndex == 0)
            //{
            //    ErrorCount++;
            //    ExportNewContact_lable.Visible = true;
            //    ExportNewContact_lable.Text = "יש לבחור מצב תעסוקתי";
            //}            
            else if (BusinessProfession.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש לבחור מקצוע";
            }
            else if (BusinessGrossSalary.Value == "")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "יש להזין שכר ברוטו";
            }
            else if (Phone1.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מספר טלפון";
                return false;
            }
            else if (Phone1.Value.Length < 9 || Phone1.Value.Substring(0, 1) != "0")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מספר טלפון תקין";
                return false;
            }
            else if (Helpers.insuredPhoneExist(Phone1.Value, long.Parse(Request.QueryString["LeadID"])) == "true")
            {
                ErrorCount++;
                ExportNewContact_lable.Visible = true;
                ExportNewContact_lable.Text = "מספר הטלפון קיים במערכת";
                return false;
            }
            if (Email.Value != "" && !Email.Value.Contains("@"))
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין אימייל תקין";
                return false;
            }
          
            if (Phone2.Value != "" && (Phone2.Value.Length < 9 || Phone2.Value.Substring(0, 1) != "0"))
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מספר טלפון תקין";
                return false;
            }
            //if (BusinessEmail.Value != "" && !BusinessEmail.Value.Contains("@"))
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין אימייל תקין";
            //    return false;

            //}

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
            if (SelectHaveAsset.SelectedIndex == 0)
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
            else if(SelectHaveAsset.SelectedIndex == 1)
            {
                if (AssetAddress.Value == "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך להזין כתובת נכס";
                    return false;
                }
            }
            if (SelectHaveMortgageOnAsset.SelectedIndex == 0)
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
            else if(SelectHaveMortgageOnAsset.SelectedIndex == 1)
            {
                if (MortgageAmount.Value == "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "עליך להזין גובה משכנתא";
                    return false;
                }
            }
//,FirstStatusLeadID = @FirstStatusLeadID
//,SecondStatusLeadID = @SecondStatusLeadID
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
,PurposeLoan=@PurposeLoan
,MortgageBalance=@PurposeLoan
,StatusContact=@SelectContactStatus";


             
                sql += " where ID = @LeadID";
                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@FirstName", FirstName.Value);
                cmd.Parameters.AddWithValue("@LastName", LastName.Value);
                cmd.Parameters.AddWithValue("@GenderID", string.IsNullOrEmpty(SelectGender.Value) ? (object)DBNull.Value : SelectGender.Value);
                cmd.Parameters.AddWithValue("@DateBirth", DateBirth.Value);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(Address.Value) ? (object)DBNull.Value : Address.Value);
                cmd.Parameters.AddWithValue("@FamilyStatusID", string.IsNullOrEmpty(SelectFamilyStatus.Value) ? (object)DBNull.Value : SelectFamilyStatus.Value);
                cmd.Parameters.AddWithValue("@Tz", Tz.Value);
                cmd.Parameters.AddWithValue("@IssuanceDateTz", string.IsNullOrEmpty(IssuanceDateTz.Value) ? (object)DBNull.Value : DateTime.Parse(IssuanceDateTz.Value));
                //cmd.Parameters.AddWithValue("@IsValidIssuanceDateTz", IsValidIssuanceDateTz.Checked == true ? 1 : 0);
                cmd.Parameters.AddWithValue("@IsValidBdi",/* IsValidBdi.Checked == true ? 1 : 0*/ BdiValidity.SelectedIndex);
                cmd.Parameters.AddWithValue("@InvalidBdiReason", InvalidBdiReason.Value);
                cmd.Parameters.AddWithValue("@Phone1", Phone1.Value);
                cmd.Parameters.AddWithValue("@Phone2", string.IsNullOrEmpty(Phone2.Value) ? (object)DBNull.Value : Phone2.Value);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email.Value) ? (object)DBNull.Value : (Email.Value));
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
                cmd.Parameters.AddWithValue("@PrevBusinessSeniority", string.IsNullOrEmpty(PrevBusinessSeniority.Value) ? (object)DBNull.Value : PrevBusinessSeniority.Value);
                cmd.Parameters.AddWithValue("@BusinessProfession", string.IsNullOrEmpty(BusinessProfession.Value) ? (object)DBNull.Value : BusinessProfession.Value);
                cmd.Parameters.AddWithValue("@BusinessCity", string.IsNullOrEmpty(BusinessCity.Value) ? (object)DBNull.Value : BusinessCity.Value);
                //cmd.Parameters.AddWithValue("@BusinessEmail", string.IsNullOrEmpty(BusinessEmail.Value) ? (object)DBNull.Value : BusinessEmail.Value);
                cmd.Parameters.AddWithValue("@BusinessPhone", string.IsNullOrEmpty(BusinessPhone.Value) ? (object)DBNull.Value : BusinessPhone.Value);
                cmd.Parameters.AddWithValue("@BusinessGrossSalary", string.IsNullOrEmpty(BusinessGrossSalary.Value) ? (object)DBNull.Value : BusinessGrossSalary.Value);
                cmd.Parameters.AddWithValue("@BusinessLineBusiness", string.IsNullOrEmpty(SelectBusinessEmploymentStatus.Value) ? (object)DBNull.Value : int.Parse(SelectBusinessEmploymentStatus.Value));
                cmd.Parameters.AddWithValue("@PartnerLineBusiness", string.IsNullOrEmpty(SelectPartnerEmploymentStatus.Value) ? (object)DBNull.Value : int.Parse(SelectPartnerEmploymentStatus.Value));
                cmd.Parameters.AddWithValue("@PartnerName", string.IsNullOrEmpty(PartnerName.Value) ? (object)DBNull.Value : PartnerName.Value);
                cmd.Parameters.AddWithValue("@PartnerGrossSalary", string.IsNullOrEmpty(PartnerGrossSalary.Value) ? (object)DBNull.Value : PartnerGrossSalary.Value);
                cmd.Parameters.AddWithValue("@PartnerAge", string.IsNullOrEmpty(PartnerAge.Value) ? (object)DBNull.Value : PartnerAge.Value);
                cmd.Parameters.AddWithValue("@PartnerSeniority", string.IsNullOrEmpty(PartnerSeniority.Value) ? (object)DBNull.Value : PartnerSeniority.Value);

                cmd.Parameters.AddWithValue("@HaveAsset", SelectHaveAsset.SelectedIndex );
                cmd.Parameters.AddWithValue("@AssetValue", string.IsNullOrEmpty(AssetValue.Value) ? (object)DBNull.Value : int.Parse(AssetValue.Value));
                cmd.Parameters.AddWithValue("@AssetType", string.IsNullOrEmpty(AssetType.Value) ? (object)DBNull.Value : AssetType.Value);
                cmd.Parameters.AddWithValue("@AssetAddress", string.IsNullOrEmpty(AssetAddress.Value) ? (object)DBNull.Value : AssetAddress.Value);

                cmd.Parameters.AddWithValue("@HaveMortgageOnAsset", SelectHaveMortgageOnAsset);
                cmd.Parameters.AddWithValue("@MortgageAmount", string.IsNullOrEmpty(MortgageAmount.Value) ? (object)DBNull.Value : long.Parse(MortgageAmount.Value));
                cmd.Parameters.AddWithValue("@MonthlyRepaymentAmount", string.IsNullOrEmpty(MonthlyRepaymentAmount.Value) ? (object)DBNull.Value : int.Parse(MonthlyRepaymentAmount.Value));
                cmd.Parameters.AddWithValue("@LendingBank", string.IsNullOrEmpty(LendingBank.Value) ? (object)DBNull.Value : LendingBank.Value);
                cmd.Parameters.AddWithValue("@PurposeTest", string.IsNullOrEmpty(PurposeTest.Value) ? (object)DBNull.Value : PurposeTest.Value);
                cmd.Parameters.AddWithValue("@RequestedLoanAmount", string.IsNullOrEmpty(RequestedLoanAmount.Value) ? (object)DBNull.Value : long.Parse(RequestedLoanAmount.Value));
                cmd.Parameters.AddWithValue("@PurposeLoan", string.IsNullOrEmpty(PurposeLoan.Value) ? (object)DBNull.Value : PurposeLoan.Value);
                cmd.Parameters.AddWithValue("@MortgageBalance", string.IsNullOrEmpty(MortgageBalance.Value) ? (object)DBNull.Value : long.Parse(MortgageBalance.Value));
                cmd.Parameters.AddWithValue("@SelectContactStatus", string.IsNullOrEmpty(SelectContactStatus.Value) ? (object)DBNull.Value : long.Parse(SelectContactStatus.Value));


                cmd.Parameters.AddWithValue("@LeadID", Request.QueryString["ContactID"]);

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

        protected void OfferAdd_Click(object sender, ImageClickEventArgs e)
        {
            System.Web.HttpContext.Current.Response.Redirect("OfferAdd.aspx?ContactID=" + Request.QueryString["ContactID"]);

        }

        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void BtnServiceRequest_Command(object sender, CommandEventArgs e)
        {
            System.Web.HttpContext.Current.Response.Redirect("ServiceRequestEdit.aspx?ServiceRequestID=" + e.CommandArgument.ToString());
        }
    }
}