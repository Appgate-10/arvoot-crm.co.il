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
    public partial class _serviceRequestEdit : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "Search";
        public string StrSrc { get { return strSrc; } }
        public string ListPageUrl = "OfferEdit.aspx";


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {
                Pageinit.CheckManagerPermissions();

                SqlCommand cmdServiceRequestPurpose = new SqlCommand("SELECT * FROM ServiceRequestPurpose");
                DataSet dsSourceLoanOrInsurance = DbProvider.GetDataSet(cmdServiceRequestPurpose);
                SelectPurpose.DataSource = dsSourceLoanOrInsurance;
                SelectPurpose.DataTextField = "purpose";
                SelectPurpose.DataValueField = "ID";
                SelectPurpose.DataBind();
                SelectPurpose.Items.Insert(0, new ListItem("ללא", ""));

                SqlCommand cmdtMethodsPayment = new SqlCommand("SELECT * FROM MethodsPayment");
                DataSet dsMethodsPayment = DbProvider.GetDataSet(cmdtMethodsPayment);
                SelectMethodsPayment.DataSource = dsMethodsPayment;
                SelectMethodsPayment.DataTextField = "Text";
                SelectMethodsPayment.DataValueField = "ID";
                SelectMethodsPayment.DataBind();
                SelectMethodsPayment.Items.Insert(0, new ListItem("בחר אמצעי תשלום", ""));

                loadData();
            }
        }


      
        public void loadData()
        {

            string sql = @"select * from ServiceRequest where ID = @ID  ";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@ID", Request.QueryString["ServiceRequestID"]);
            DataTable ds = DbProvider.GetDataTable(cmd);
            if(ds.Rows.Count > 0)
            {
                OfferID.Value = ds.Rows[0]["OfferID"].ToString();
                Invoice.Value = ds.Rows[0]["Invoice"].ToString();
                AllSum.Value = ds.Rows[0]["Sum"].ToString();
                Policy.Value = ds.Rows[0]["Policy"].ToString();
                Balance.Value = ds.Rows[0]["Balance"].ToString();
                SelectPurpose.Value = ds.Rows[0]["PurposeID"].ToString();
                Note.Value = ds.Rows[0]["Note"].ToString();

                Sum1.Value = ds.Rows[0]["SumPayment1"].ToString();
                if (!string.IsNullOrEmpty(ds.Rows[0]["DatePayment1"].ToString()))
                {
                    DateTime Date1 = Convert.ToDateTime(ds.Rows[0]["DatePayment1"]);
                    DatePayment1.Value = (Date1).ToString("yyyy-MM-dd");
                }
                Num1.Value = ds.Rows[0]["NumPayment1"].ToString();
                ReferencePayment1.Value = ds.Rows[0]["ReferencePayment1"].ToString();
                IsApprove1.Checked = ds.Rows[0]["IsApprovedPayment1"].ToString().Equals("1");

                Sum2.Value = ds.Rows[0]["SumPayment2"].ToString();
                if (!string.IsNullOrEmpty(ds.Rows[0]["DatePayment2"].ToString()))
                {
                    DateTime Date2 = Convert.ToDateTime(ds.Rows[0]["DatePayment2"]);
                    DatePayment2.Value = (Date2).ToString("yyyy-MM-dd");
                }
                Num2.Value = ds.Rows[0]["NumPayment2"].ToString();
                ReferencePayment2.Value = ds.Rows[0]["ReferencePayment2"].ToString();
                IsApprove2.Checked = ds.Rows[0]["IsApprovedPayment2"].ToString().Equals("1");  
                
                Sum3.Value = ds.Rows[0]["SumPayment3"].ToString();
                if (!string.IsNullOrEmpty(ds.Rows[0]["DatePayment3"].ToString()))
                {
                    DateTime Date3 = Convert.ToDateTime(ds.Rows[0]["DatePayment3"]);
                    DatePayment3.Value = (Date3).ToString("yyyy-MM-dd");
                }
                Num3.Value = ds.Rows[0]["NumPayment3"].ToString();
                ReferencePayment3.Value = ds.Rows[0]["ReferencePayment3"].ToString();
                IsApprove3.Checked = ds.Rows[0]["IsApprovedPayment3"].ToString().Equals("1");

                Sum4.Value = ds.Rows[0]["SumCreditOrDenial"].ToString();
                if (!string.IsNullOrEmpty(ds.Rows[0]["DateCreditOrDenial"].ToString()))
                {
                    DateTime Date4 = Convert.ToDateTime(ds.Rows[0]["DateCreditOrDenial"]);
                    DateCreditOrDenial.Value = (Date4).ToString("yyyy-MM-dd");
                }
                Num4.Value = ds.Rows[0]["NumCreditOrDenial"].ToString();
                ReferenceCreditOrDenial.Value = ds.Rows[0]["ReferenceCreditOrDenial"].ToString(); 
                NoteCreditOrDenial.Value = ds.Rows[0]["NoteCreditOrDenial"].ToString();
                IsApprove4.Checked = ds.Rows[0]["IsApprovedCreditOrDenial"].ToString().Equals("1");
                SelectMethodsPayment.Value= ds.Rows[0]["PaymentMethodID"].ToString();
                BankName.Value = ds.Rows[0]["BankName"].ToString(); 
                Branch.Value = ds.Rows[0]["Branch"].ToString(); 
                AccountNumber.Value = ds.Rows[0]["AccountNumber"].ToString();    
                CreditNumber.Value = ds.Rows[0]["CreditNumber"].ToString(); 
                CreditValidity.Value = ds.Rows[0]["CreditValidity"].ToString(); 
                CardholdersID.Value = ds.Rows[0]["CardholdersID"].ToString();
            }
        }
        protected void RadioButttonMethodsPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            DivRBMethodsPayment.Visible = false;
            //BtnMethodsPayment.Text = RadioButttonMethodsPayment.SelectedItem.Text;
        }
   
        public bool funcSave(object sender, EventArgs e)
        {
            //return false;

            int ErrorCount = 0;
            FormError_lable.Visible = false;
            //שם פרטי שם משפחה תאריך לידה תז טלפון אימייל סטטוס ראשי

            if (Invoice.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין חשבון";
                return false;
            }
            if (AllSum.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין סכום כולל לגבייה";
                return false;
            }
            if (Policy.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין פוליסה";
                return false;
            }
            if (Balance.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין יתרת הגבייה";
                return false;
            }
            if (SelectPurpose.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מטרת הגבייה";
                return false;
            }
            if (Sum1.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין סכום לתשלום ראשון";
                return false;
            }
            if (DatePayment1.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין תאריך תשלום ראשון";
                return false;
            }
            //if (Num1.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_lable.Visible = true;
            //    FormError_lable.Text = "יש להזין מספר תשלומים לתשלום ראשון";
            //    return false;
            //}
            if (Num1.Value == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מספר תשלומים לתשלום ראשון";
                return false;
            }




            if (ErrorCount == 0)
            {


                string sql = @" Update ServiceRequest set Invoice = @Invoice, Sum=@Sum, Note=@Note, Policy=@Policy, Balance=@Balance, PurposeID=@PurposeID, SumPayment1=@SumPayment1,
                                DatePayment1=@DatePayment1, NumPayment1=@NumPayment1, ReferencePayment1=@ReferencePayment1, IsApprovedPayment1=@IsApprovedPayment1, SumPayment2=@SumPayment2, 
                                DatePayment2=@DatePayment2, NumPayment2=@NumPayment2, ReferencePayment2=@ReferencePayment2, IsApprovedPayment2=@IsApprovedPayment2, SumPayment3=@SumPayment3,
                                DatePayment3=@DatePayment3, NumPayment3=@NumPayment3, ReferencePayment3=@ReferencePayment3, IsApprovedPayment3=@IsApprovedPayment3, SumCreditOrDenial=@SumCreditOrDenial,
                                DateCreditOrDenial=@DateCreditOrDenial, NumCreditOrDenial=@NumCreditOrDenial, ReferenceCreditOrDenial=@ReferenceCreditOrDenial, NoteCreditOrDenial=@NoteCreditOrDenial,
                                IsApprovedCreditOrDenial=@IsApprovedCreditOrDenial, PaymentMethodID=@PaymentMethodID, BankName=@BankName, Branch=@Branch, AccountNumber=@AccountNumber, CreditNumber=@CreditNumber,
                                CreditValidity=@CreditValidity, CardholdersID=@CardholdersID  where ID = @ID";
                              
                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@ID", Request.QueryString["ServiceRequestID"]);
                cmd.Parameters.AddWithValue("@Invoice", Invoice.Value);
                cmd.Parameters.AddWithValue("@Sum", AllSum.Value);
                cmd.Parameters.AddWithValue("@Note", Note.Value);
                cmd.Parameters.AddWithValue("@Policy", Policy.Value);
                cmd.Parameters.AddWithValue("@Balance", Balance.Value);
                cmd.Parameters.AddWithValue("@PurposeID", SelectPurpose.Value);
                cmd.Parameters.AddWithValue("@SumPayment1", Sum1.Value);
                cmd.Parameters.AddWithValue("@DatePayment1", DatePayment1.Value);
                cmd.Parameters.AddWithValue("@NumPayment1", Num1.Value);
                cmd.Parameters.AddWithValue("@ReferencePayment1", string.IsNullOrEmpty(ReferencePayment1.Value) ? (object)DBNull.Value : ReferencePayment1.Value);
                cmd.Parameters.AddWithValue("@IsApprovedPayment1", IsApprove1.Checked ? "1" : "0");
                cmd.Parameters.AddWithValue("@SumPayment2", string.IsNullOrEmpty(Sum2.Value) ? (object)DBNull.Value : Sum2.Value);
                cmd.Parameters.AddWithValue("@DatePayment2", string.IsNullOrEmpty(DatePayment2.Value) ? (object)DBNull.Value : DatePayment2.Value);
                cmd.Parameters.AddWithValue("@NumPayment2", string.IsNullOrEmpty(Num2.Value) ? (object)DBNull.Value : Num2.Value);
                cmd.Parameters.AddWithValue("@ReferencePayment2", string.IsNullOrEmpty(ReferencePayment2.Value) ? (object)DBNull.Value : ReferencePayment2.Value);
                cmd.Parameters.AddWithValue("@IsApprovedPayment2", IsApprove2.Checked ? "1" : "0");
                cmd.Parameters.AddWithValue("@SumPayment3", string.IsNullOrEmpty(Sum3.Value) ? (object)DBNull.Value : Sum3.Value);
                cmd.Parameters.AddWithValue("@DatePayment3", string.IsNullOrEmpty(DatePayment3.Value) ? (object)DBNull.Value : DatePayment3.Value);
                cmd.Parameters.AddWithValue("@NumPayment3", string.IsNullOrEmpty(Num3.Value) ? (object)DBNull.Value : Num3.Value);
                cmd.Parameters.AddWithValue("@ReferencePayment3", string.IsNullOrEmpty(ReferencePayment3.Value) ? (object)DBNull.Value : ReferencePayment3.Value);
                cmd.Parameters.AddWithValue("@IsApprovedPayment3", IsApprove3.Checked ? "1" : "0");
                cmd.Parameters.AddWithValue("@SumCreditOrDenial", string.IsNullOrEmpty(Sum4.Value) ? (object)DBNull.Value : Sum4.Value);
                cmd.Parameters.AddWithValue("@DateCreditOrDenial", string.IsNullOrEmpty(DateCreditOrDenial.Value) ? (object)DBNull.Value : DateCreditOrDenial.Value);
                cmd.Parameters.AddWithValue("@NumCreditOrDenial", string.IsNullOrEmpty(Num4.Value) ? (object)DBNull.Value : Num4.Value);
                cmd.Parameters.AddWithValue("@ReferenceCreditOrDenial", string.IsNullOrEmpty(ReferenceCreditOrDenial.Value) ? (object)DBNull.Value : ReferenceCreditOrDenial.Value);
                cmd.Parameters.AddWithValue("@NoteCreditOrDenial", string.IsNullOrEmpty(NoteCreditOrDenial.Value) ? (object)DBNull.Value : NoteCreditOrDenial.Value);
                cmd.Parameters.AddWithValue("@IsApprovedCreditOrDenial", IsApprove4.Checked ? "1" : "0");
                cmd.Parameters.AddWithValue("@PaymentMethodID", string.IsNullOrEmpty(SelectMethodsPayment.Value) ? (object)DBNull.Value : SelectMethodsPayment.Value);
                cmd.Parameters.AddWithValue("@BankName", string.IsNullOrEmpty(BankName.Value) ? (object)DBNull.Value : BankName.Value);
                cmd.Parameters.AddWithValue("@Branch", string.IsNullOrEmpty(Branch.Value) ? (object)DBNull.Value : Branch.Value);
                cmd.Parameters.AddWithValue("@AccountNumber", string.IsNullOrEmpty(AccountNumber.Value) ? (object)DBNull.Value : AccountNumber.Value);
                cmd.Parameters.AddWithValue("@CreditNumber", string.IsNullOrEmpty(CreditNumber.Value) ? (object)DBNull.Value : CreditNumber.Value);
                cmd.Parameters.AddWithValue("@CreditValidity", string.IsNullOrEmpty(CreditValidity.Value) ? (object)DBNull.Value : CreditValidity.Value);
                cmd.Parameters.AddWithValue("@CardholdersID", string.IsNullOrEmpty(CardholdersID.Value) ? (object)DBNull.Value : CardholdersID.Value);



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

        protected void btn_save_Click(object sender, EventArgs e)
        {
            bool success = funcSave(sender, e);
            if (!success)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect(ListPageUrl + "?OfferID=" + OfferID.Value);
            }
        }
        protected void DeleteService_Click(object sender, ImageClickEventArgs e)
        {


            SqlCommand cmdDelServices = new SqlCommand("delete from ServiceRequest where ID = @ID");
            cmdDelServices.Parameters.AddWithValue("@ID", Request.QueryString["ServiceRequestID"]);
            if (DbProvider.ExecuteCommand(cmdDelServices) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                FormError_lable.Text = "* התרחשה שגיאה";
                FormError_lable.Visible = true;
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect(ListPageUrl + "?OfferID=" + OfferID.Value);

            }

        }
        protected void BtnMethodsPayment_Click(object sender, EventArgs e)
        {
            bool isOpen = DivRBMethodsPayment.Visible;
            if (isOpen == false)
            {
                DivRBMethodsPayment.Visible = true;
            }
            else
            {
                DivRBMethodsPayment.Visible = false;
            }

        }
     

    }
}