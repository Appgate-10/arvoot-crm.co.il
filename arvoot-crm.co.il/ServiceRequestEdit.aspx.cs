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
        public string ListPageUrl = "ServiceRequestEdit.aspx";


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
                string[] months = {
                     "01", "02", "03", "04", "05", "06",
                     "07", "08", "09", "10", "11", "12"
                };
                SelectMonth.DataSource = months;
                SelectMonth.DataBind();
                List<string> years = new List<string>();
                for (int i = DateTime.Now.Year; i <= DateTime.Now.Year + 30; i++){
                    years.Add(i.ToString());
                }
               
                SelectYear.DataSource = years.ToArray();
                SelectYear.DataBind();
                loadData();
                if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) > 3)
                {
                    DeleteLid.Visible = false;
                    IsApprove4.Enabled = false;
                    if (IsApprove4.Checked)
                    {
                        Num4.Disabled = true;
                        ReferenceCreditOrDenial.Disabled = true;
                        DateCreditOrDenial.Disabled = true;
                        Sum4.Disabled = true;
                        NoteCreditOrDenial.Disabled = true;
                    }
                }
            }
        }


      
        public void loadData()
        {
            if (Request.QueryString["ServiceRequestID"] != null)
            {
                string sql = @"select s.*, o.NameOffer from ServiceRequest s  left join Offer o on o.ID = s.OfferID where s.ID = @ID  ";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@ID", Request.QueryString["ServiceRequestID"]);
                DataTable ds = DbProvider.GetDataTable(cmd);
                if (ds.Rows.Count > 0)
                {
                    OfferID.Value = ds.Rows[0]["OfferID"].ToString();
                    Invoice.Value = ds.Rows[0]["Invoice"].ToString();
                    AllSum.Value = ds.Rows[0]["Sum"].ToString();
                    //Balance.value = ds.Rows[0]["Balance"].ToString();
                    SelectPurpose.Value = ds.Rows[0]["PurposeID"].ToString();
                    Note.Value = ds.Rows[0]["Note"].ToString();
                    OfferName.Value = ds.Rows[0]["NameOffer"].ToString();

                    //Sum1.Value = ds.Rows[0]["SumPayment1"].ToString();
                    //if (!string.IsNullOrEmpty(ds.Rows[0]["DatePayment1"].ToString()))
                    //{
                    //    DateTime Date1 = Convert.ToDateTime(ds.Rows[0]["DatePayment1"]);
                    //    DatePayment1.Value = (Date1).ToString("yyyy-MM-dd");
                    //}
                    //Num1.Value = ds.Rows[0]["NumPayment1"].ToString();
                    //ReferencePayment1.Value = ds.Rows[0]["ReferencePayment1"].ToString();
                    //IsApprove1.Checked = ds.Rows[0]["IsApprovedPayment1"].ToString().Equals("1");

                    //Sum2.Value = ds.Rows[0]["SumPayment2"].ToString();
                    //if (!string.IsNullOrEmpty(ds.Rows[0]["DatePayment2"].ToString()))
                    //{
                    //    DateTime Date2 = Convert.ToDateTime(ds.Rows[0]["DatePayment2"]);
                    //    DatePayment2.Value = (Date2).ToString("yyyy-MM-dd");
                    //}
                    //Num2.Value = ds.Rows[0]["NumPayment2"].ToString();
                    //ReferencePayment2.Value = ds.Rows[0]["ReferencePayment2"].ToString();
                    //IsApprove2.Checked = ds.Rows[0]["IsApprovedPayment2"].ToString().Equals("1");  

                    //Sum3.Value = ds.Rows[0]["SumPayment3"].ToString();
                    //if (!string.IsNullOrEmpty(ds.Rows[0]["DatePayment3"].ToString()))
                    //{
                    //    DateTime Date3 = Convert.ToDateTime(ds.Rows[0]["DatePayment3"]);
                    //    DatePayment3.Value = (Date3).ToString("yyyy-MM-dd");
                    //}
                    //Num3.Value = ds.Rows[0]["NumPayment3"].ToString();
                    //ReferencePayment3.Value = ds.Rows[0]["ReferencePayment3"].ToString();
                    //IsApprove3.Checked = ds.Rows[0]["IsApprovedPayment3"].ToString().Equals("1");

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
                    SelectMethodsPayment.Value = ds.Rows[0]["PaymentMethodID"].ToString();
                    BankName.Value = ds.Rows[0]["BankName"].ToString();
                    Branch.Value = ds.Rows[0]["Branch"].ToString();
                    AccountNumber.Value = ds.Rows[0]["AccountNumber"].ToString();
                    CreditNumber.Value = ds.Rows[0]["CreditNumber"].ToString();
                    string[] validity = ds.Rows[0]["CreditValidity"].ToString().Split('/');
                    //CreditValidity.Value = ds.Rows[0]["CreditValidity"].ToString(); 
                    if (validity.Length == 2 && !string.IsNullOrEmpty(validity[0]) && !string.IsNullOrEmpty(validity[1]))
                    {
                        SelectMonth.Value = validity[0];
                        SelectYear.Value = validity[1];
                    }
                    CardholdersID.Value = ds.Rows[0]["CardholdersID"].ToString();

                    SqlCommand cmdPayments = new SqlCommand("SELECT * FROM ServiceRequestPayment WHERE ServiceRequestID = @serviceReqID");
                    cmdPayments.Parameters.AddWithValue("@serviceReqID", Request.QueryString["ServiceRequestID"]);
                    DataTable dtPayments = DbProvider.GetDataTable(cmdPayments);

                    double SumPaid = 0;
                    List<serviceRequestPayment> payments = new List<serviceRequestPayment>();

                    foreach (DataRow row in dtPayments.Rows)
                    {
                        DateTime DateP = Convert.ToDateTime(row["DatePayment"]);
                        serviceRequestPayment payment = new serviceRequestPayment
                        {
                            ID = int.Parse(row["ID"].ToString()),
                            ServiceRequestID = int.Parse(row["ServiceRequestID"].ToString()),
                            DatePayment = DateP.ToString("yyyy-MM-dd"),
                            NumPayment = int.Parse(row["NumPayment"].ToString()),
                            SumPayment = double.Parse(row["SumPayment"].ToString()),
                            ReferencePayment = row["ReferencePayment"].ToString(),
                            IsApprovedPayment = row["IsApprovedPayment"].ToString() == "0" ? false : true
                        };
                        if (payment.IsApprovedPayment == true)
                        {
                            SumPaid += payment.SumPayment;
                        }
                        payments.Add(payment);
                    }

                    AllSum.Value = ds.Rows[0]["Sum"].ToString();

                    double AllSumVal;
                    if (!string.IsNullOrWhiteSpace(AllSum.Value) && double.TryParse(AllSum.Value, out AllSumVal) == true)
                    {
                        double balanceToPay = AllSumVal - SumPaid;
                        double sum4Val;
                        if (IsApprove4.Checked == true && double.TryParse(Sum4.Value, out sum4Val) == true)
                        {
                            balanceToPay += sum4Val;
                        }
                        Balance.InnerText = balanceToPay.ToString();
                    }

                    if (payments.Count == 0)
                    {
                        serviceRequestPayment payment2 = new serviceRequestPayment
                        {
                            ID = 0,
                            ServiceRequestID = int.Parse(Request.QueryString["ServiceRequestID"].ToString()),
                            DatePayment = "",
                            NumPayment = 0,
                            SumPayment = 0,
                            ReferencePayment = "",
                            IsApprovedPayment = false
                        };
                        payments.Add(payment2);
                    }
                    Session["payments"] = payments;
                    RepeaterPayments.DataSource = payments;
                    RepeaterPayments.DataBind();
                }
            }
            
        }
        protected void RadioButttonMethodsPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            DivRBMethodsPayment.Visible = false;
            //BtnMethodsPayment.Text = RadioButttonMethodsPayment.SelectedItem.Text;
        }
   
        public bool funcSave(object sender, EventArgs e)
        {
            List<serviceRequestPayment> payments = new List<serviceRequestPayment>();
            foreach (RepeaterItem item in RepeaterPayments.Items)
            {
                HtmlInputControl Sum = (HtmlInputControl)item.FindControl("Sum1");
                HtmlInputControl DatePayment = (HtmlInputControl)item.FindControl("DatePayment1");
                HtmlInputControl Num = (HtmlInputControl)item.FindControl("Num1");
                HtmlInputControl ReferencePayment = (HtmlInputControl)item.FindControl("ReferencePayment1");
                CheckBox IsApprove = (CheckBox)item.FindControl("IsApprove1");
                HiddenField hiddenPaymentID = (HiddenField)item.FindControl("hiddenPaymentID");

                serviceRequestPayment payment = new serviceRequestPayment
                {
                    ID = int.Parse(hiddenPaymentID.Value),
                    ServiceRequestID = int.Parse(Request.QueryString["ServiceRequestID"].ToString()),
                    SumPayment = (!string.IsNullOrWhiteSpace(Sum.Value) ? double.Parse(Sum.Value) : 0),
                    NumPayment = (!string.IsNullOrWhiteSpace(Num.Value) ? int.Parse(Num.Value) : 0),
                    DatePayment = DatePayment.Value,
                    ReferencePayment = ReferencePayment.Value,
                    IsApprovedPayment = IsApprove.Checked
                };

                payments.Add(payment);
            }
            Session["payments"] = payments;

            int ErrorCount = 0;
            FormError_label.Visible = false;
            FormErrorBottom_label.Visible = false;
            //שם פרטי שם משפחה תאריך לידה תז טלפון אימייל סטטוס ראשי

            if (Invoice.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין חשבון";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין חשבון";
                return false;
            }
            if (AllSum.Value == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין סכום כולל לגבייה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין סכום כולל לגבייה";
                return false;
            }
          
            if (SelectPurpose.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מטרת הגבייה";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מטרת הגבייה";
                return false;
            }
            //if (Sum1.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_label.Visible = true;
            //    FormError_label.Text = "יש להזין סכום לתשלום ראשון";
            //    return false;
            //}
            //if (DatePayment1.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_label.Visible = true;
            //    FormError_label.Text = "יש להזין תאריך תשלום ראשון";
            //    return false;
            //}
            //if (Num1.Value == "")
            //{
            //    ErrorCount++;
            //    FormError_label.Visible = true;
            //    FormError_label.Text = "יש להזין מספר תשלומים לתשלום ראשון";
            //    return false;
            //}


            if (payments.Count == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין פרטי תשלום ראשון";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין פרטי תשלום ראשון";
                return false;
            }
            if (payments[0].SumPayment == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין סכום לתשלום ראשון";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין סכום לתשלום ראשון";
                return false;
            }
            if (payments[0].DatePayment == "")
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין תאריך תשלום ראשון";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין תאריך תשלום ראשון";
                return false;
            }
            if (payments[0].NumPayment == 0)
            {
                ErrorCount++;
                FormError_label.Visible = true;
                FormError_label.Text = "יש להזין מספר תשלומים לתשלום ראשון";
                FormErrorBottom_label.Visible = true;
                FormErrorBottom_label.Text = "יש להזין מספר תשלומים לתשלום ראשון";
                return false;
            }

            if (payments.Count > 1)
            {
                for (int i = 1; i < payments.Count; i++)
                {
                    if (payments[i].SumPayment > 0 || payments[i].DatePayment != "" || payments[i].NumPayment > 0)
                    {
                        if (payments[i].SumPayment == 0)
                        {
                            FormError_label.Visible = true;
                            FormError_label.Text = "יש להזין סכום לתשלום " + Helpers.NumberToHebrewOrdinal(i + 1);
                            FormErrorBottom_label.Visible = true;
                            FormErrorBottom_label.Text = "יש להזין סכום לתשלום " + Helpers.NumberToHebrewOrdinal(i + 1);
                            return false;
                        }
                        if (payments[i].DatePayment == "")
                        {
                            FormError_label.Visible = true;
                            FormError_label.Text = "יש להזין תאריך תשלום " + Helpers.NumberToHebrewOrdinal(i + 1); ;
                            FormErrorBottom_label.Visible = true;
                            FormErrorBottom_label.Text = "יש להזין תאריך תשלום " + Helpers.NumberToHebrewOrdinal(i + 1); ;
                            return false;
                        }
                        if (payments[i].NumPayment == 0)
                        {
                            FormError_label.Visible = true;
                            FormError_label.Text = "יש להזין מספר תשלומים לתשלום " + Helpers.NumberToHebrewOrdinal(i + 1); ;
                            FormErrorBottom_label.Visible = true;
                            FormErrorBottom_label.Text = "יש להזין מספר תשלומים לתשלום " + Helpers.NumberToHebrewOrdinal(i + 1); ;
                            return false;
                        }
                    }
                }

            }
            if (SelectMethodsPayment.SelectedIndex == 1)
            {
                if (BankName.Value == "")
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין שם בנק";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין שם בנק";
                    return false;
                }
                if (Branch.Value == "")
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין סניף";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין סניף";
                    return false;
                }
                if (AccountNumber.Value == "")
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין מספר חשבון";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין מספר חשבון";
                    return false;
                }
            }
            else if (SelectMethodsPayment.SelectedIndex == 2)
            {
                if (CreditNumber.Value == "")
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין מספר כרטיס";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין מספר כרטיס";
                    return false;
                }
                //if (CreditValidity.Value == "")
                //{
                //    ErrorCount++;
                //    FormError_label.Visible = true;
                //    FormError_label.Text = "יש להזין תוקף";
                //    return false;
                //}
                if (CardholdersID.Value == "")
                {
                    ErrorCount++;
                    FormError_label.Visible = true;
                    FormError_label.Text = "יש להזין ת.ז. בעל הכרטיס";
                    FormErrorBottom_label.Visible = true;
                    FormErrorBottom_label.Text = "יש להזין ת.ז. בעל הכרטיס";
                    return false;
                }
            }

            if (ErrorCount == 0)
            {

                string sql = @" Update ServiceRequest set Invoice = @Invoice, Sum=@Sum, Note=@Note, PurposeID=@PurposeID, 
                                SumCreditOrDenial=@SumCreditOrDenial,DateCreditOrDenial=@DateCreditOrDenial, NumCreditOrDenial=@NumCreditOrDenial, ReferenceCreditOrDenial=@ReferenceCreditOrDenial, NoteCreditOrDenial=@NoteCreditOrDenial,
                                IsApprovedCreditOrDenial=@IsApprovedCreditOrDenial, PaymentMethodID=@PaymentMethodID, BankName=@BankName, Branch=@Branch, AccountNumber=@AccountNumber, CreditNumber=@CreditNumber,
                                CreditValidity=@CreditValidity, CardholdersID=@CardholdersID  where ID = @ID";

                //, Balance=@Balance
                /*SumPayment1=@SumPayment1,DatePayment1=@DatePayment1, NumPayment1=@NumPayment1, ReferencePayment1=@ReferencePayment1, IsApprovedPayment1=@IsApprovedPayment1, 
SumPayment2=@SumPayment2,DatePayment2=@DatePayment2, NumPayment2=@NumPayment2, ReferencePayment2=@ReferencePayment2, IsApprovedPayment2=@IsApprovedPayment2, 
SumPayment3=@SumPayment3,DatePayment3=@DatePayment3, NumPayment3=@NumPayment3, ReferencePayment3=@ReferencePayment3, IsApprovedPayment3=@IsApprovedPayment3, */

                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@ID", Request.QueryString["ServiceRequestID"]);
                cmd.Parameters.AddWithValue("@Invoice", Invoice.Value);
                cmd.Parameters.AddWithValue("@Sum", AllSum.Value);
                cmd.Parameters.AddWithValue("@Note", Note.Value);
                //cmd.Parameters.AddWithValue("@Balance", Balance.Value);
                cmd.Parameters.AddWithValue("@PurposeID", SelectPurpose.Value);
                //cmd.Parameters.AddWithValue("@SumPayment1", Sum1.Value);
                //cmd.Parameters.AddWithValue("@DatePayment1", DatePayment1.Value);
                //cmd.Parameters.AddWithValue("@NumPayment1", Num1.Value);
                //cmd.Parameters.AddWithValue("@ReferencePayment1", string.IsNullOrEmpty(ReferencePayment1.Value) ? (object)DBNull.Value : ReferencePayment1.Value);
                //cmd.Parameters.AddWithValue("@IsApprovedPayment1", IsApprove1.Checked ? "1" : "0");
                //cmd.Parameters.AddWithValue("@SumPayment2", string.IsNullOrEmpty(Sum2.Value) ? (object)DBNull.Value : Sum2.Value);
                //cmd.Parameters.AddWithValue("@DatePayment2", string.IsNullOrEmpty(DatePayment2.Value) ? (object)DBNull.Value : DatePayment2.Value);
                //cmd.Parameters.AddWithValue("@NumPayment2", string.IsNullOrEmpty(Num2.Value) ? (object)DBNull.Value : Num2.Value);
                //cmd.Parameters.AddWithValue("@ReferencePayment2", string.IsNullOrEmpty(ReferencePayment2.Value) ? (object)DBNull.Value : ReferencePayment2.Value);
                //cmd.Parameters.AddWithValue("@IsApprovedPayment2", IsApprove2.Checked ? "1" : "0");
                //cmd.Parameters.AddWithValue("@SumPayment3", string.IsNullOrEmpty(Sum3.Value) ? (object)DBNull.Value : Sum3.Value);
                //cmd.Parameters.AddWithValue("@DatePayment3", string.IsNullOrEmpty(DatePayment3.Value) ? (object)DBNull.Value : DatePayment3.Value);
                //cmd.Parameters.AddWithValue("@NumPayment3", string.IsNullOrEmpty(Num3.Value) ? (object)DBNull.Value : Num3.Value);
                //cmd.Parameters.AddWithValue("@ReferencePayment3", string.IsNullOrEmpty(ReferencePayment3.Value) ? (object)DBNull.Value : ReferencePayment3.Value);
                //cmd.Parameters.AddWithValue("@IsApprovedPayment3", IsApprove3.Checked ? "1" : "0");
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
                cmd.Parameters.AddWithValue("@CreditValidity", string.IsNullOrEmpty(SelectMonth.Value) || string.IsNullOrEmpty(SelectYear.Value) ? (object)DBNull.Value : SelectMonth.Value + "/" + SelectYear.Value);
                cmd.Parameters.AddWithValue("@CardholdersID", string.IsNullOrEmpty(CardholdersID.Value) ? (object)DBNull.Value : CardholdersID.Value);



                if (DbProvider.ExecuteCommand(cmd) > 0)
                {

                    foreach (serviceRequestPayment payment in payments)
                    {
                        if (payment.SumPayment > 0)
                        {
                            if (payment.ID > 0)
                            {
                                string sqlEditPayment = @"UPDATE ServiceRequestPayment SET SumPayment = @sumP, DatePayment = @dateP,
                                                        NumPayment = @numP, ReferencePayment = @referenceP, IsApprovedPayment = @isApproved
                                                        WHERE ID = @paymentID";
                                SqlCommand cmdEditPayment = new SqlCommand(sqlEditPayment);
                                cmdEditPayment.Parameters.AddWithValue("@paymentID", payment.ID);
                                cmdEditPayment.Parameters.AddWithValue("@sumP", payment.SumPayment);
                                cmdEditPayment.Parameters.AddWithValue("@dateP", payment.DatePayment);
                                cmdEditPayment.Parameters.AddWithValue("@numP", payment.NumPayment);
                                cmdEditPayment.Parameters.AddWithValue("@referenceP", string.IsNullOrEmpty(payment.ReferencePayment) ? (object)DBNull.Value : payment.ReferencePayment);
                                cmdEditPayment.Parameters.AddWithValue("@isApproved", payment.IsApprovedPayment == true ? 1 : 0);
                                DbProvider.ExecuteCommand(cmdEditPayment);
                            }
                            else
                            {
                                string sqlNewPayment = @"INSERT INTO ServiceRequestPayment 
                                            (ServiceRequestID,SumPayment,DatePayment,NumPayment,ReferencePayment,IsApprovedPayment)
                                            VALUES (@serviceID, @sumP, @dateP, @numP, @referenceP, @isApproved)";
                                SqlCommand cmdPayment = new SqlCommand(sqlNewPayment);
                                cmdPayment.Parameters.AddWithValue("@serviceID", Request.QueryString["ServiceRequestID"]);
                                cmdPayment.Parameters.AddWithValue("@sumP", payment.SumPayment);
                                cmdPayment.Parameters.AddWithValue("@dateP", payment.DatePayment);
                                cmdPayment.Parameters.AddWithValue("@numP", payment.NumPayment);
                                cmdPayment.Parameters.AddWithValue("@referenceP", string.IsNullOrEmpty(payment.ReferencePayment) ? (object)DBNull.Value : payment.ReferencePayment);
                                cmdPayment.Parameters.AddWithValue("@isApproved", payment.IsApprovedPayment == true ? 1 : 0);
                                DbProvider.ExecuteCommand(cmdPayment);
                            }
                            
                        }
                        else if (payment.ID > 0)
                        {
                            SqlCommand cmdDelPayment = new SqlCommand("DELETE TOP (1) FROM ServiceRequestPayment WHERE ID = @paymentID");
                            cmdDelPayment.Parameters.AddWithValue("@paymentID", payment.ID);
                            DbProvider.ExecuteCommand(cmdDelPayment);
                        }

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

        protected void btn_save_Click(object sender, EventArgs e)
        {
            bool success = funcSave(sender, e);
            if (!success)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                Session["payments"] = null;
                //System.Web.HttpContext.Current.Response.Redirect(ListPageUrl + "?OfferID=" + OfferID.Value);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                loadData();
            }
        }
        protected void DeleteService_Click(object sender, ImageClickEventArgs e)
        {


            SqlCommand cmdDelServices = new SqlCommand("delete from ServiceRequest where ID = @ID");
            cmdDelServices.Parameters.AddWithValue("@ID", Request.QueryString["ServiceRequestID"]);
            if (DbProvider.ExecuteCommand(cmdDelServices) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
                FormError_label.Text = "* התרחשה שגיאה";
                FormError_label.Visible = true;
                FormErrorBottom_label.Text = "* התרחשה שגיאה";
                FormErrorBottom_label.Visible = true;
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

        protected void RepeaterPayments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl paymentTitle = (HtmlGenericControl)e.Item.FindControl("paymentTitle");
                HtmlGenericControl sumTitle = (HtmlGenericControl)e.Item.FindControl("sumTitle");
                CheckBox IsApprove1 = (CheckBox)e.Item.FindControl("IsApprove1");
                HtmlInputControl Sum = (HtmlInputControl)e.Item.FindControl("Sum1");
                HtmlInputControl DatePayment = (HtmlInputControl)e.Item.FindControl("DatePayment1");
                HtmlInputControl Num = (HtmlInputControl)e.Item.FindControl("Num1");
                HtmlInputControl ReferencePayment = (HtmlInputControl)e.Item.FindControl("ReferencePayment1");

                if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) < 4)
                {
                    IsApprove1.Enabled = true;
                }
                else
                {
                    IsApprove1.Enabled = false;
                    if (IsApprove1.Checked)
                    {
                        Sum.Disabled = true;
                        DatePayment.Disabled = true;
                        Num.Disabled = true;
                        ReferencePayment.Disabled = true;
                    }
                }
                paymentTitle.InnerText = "פירוט תשלום " + Helpers.NumberToHebrewOrdinal(e.Item.ItemIndex + 1);
                sumTitle.InnerText = "סכום לתשלום " + Helpers.NumberToHebrewOrdinal(e.Item.ItemIndex + 1) + ":";

            }
        }

        protected void AddPayment_Click(object sender, EventArgs e)
        {
            //List<serviceRequestPayment> payments = new List<serviceRequestPayment>();
            //if (Session["payments"] != null)
            //{
            //    payments = Session["payments"] as List<serviceRequestPayment>;
            //}

            List<serviceRequestPayment> payments = new List<serviceRequestPayment>();
            double SumPaid = 0;
            foreach (RepeaterItem item in RepeaterPayments.Items)
            {
                HtmlInputControl Sum = (HtmlInputControl)item.FindControl("Sum1");
                HtmlInputControl DatePayment = (HtmlInputControl)item.FindControl("DatePayment1");
                HtmlInputControl Num = (HtmlInputControl)item.FindControl("Num1");
                HtmlInputControl ReferencePayment = (HtmlInputControl)item.FindControl("ReferencePayment1");
                CheckBox IsApprove = (CheckBox)item.FindControl("IsApprove1");
                HiddenField hiddenPaymentID = (HiddenField)item.FindControl("hiddenPaymentID");

                serviceRequestPayment paymentFromRepeater = new serviceRequestPayment
                {
                    ID = int.Parse(hiddenPaymentID.Value),
                    ServiceRequestID = int.Parse(Request.QueryString["ServiceRequestID"].ToString()),
                    SumPayment = (!string.IsNullOrWhiteSpace(Sum.Value) ? double.Parse(Sum.Value) : 0),
                    NumPayment = (!string.IsNullOrWhiteSpace(Num.Value) ? int.Parse(Num.Value) : 0),
                    DatePayment = DatePayment.Value,
                    ReferencePayment = ReferencePayment.Value,
                    IsApprovedPayment = IsApprove.Checked

                };

                if (paymentFromRepeater.IsApprovedPayment == true)
                {
                    SumPaid += paymentFromRepeater.SumPayment;
                }
                payments.Add(paymentFromRepeater);
            }


            double AllSumVal;
            if (!string.IsNullOrWhiteSpace(AllSum.Value) && double.TryParse(AllSum.Value, out AllSumVal) == true)
            {
                double balanceToPay = AllSumVal - SumPaid;
                double sum4Val;
                if (IsApprove4.Checked == true && double.TryParse(Sum4.Value, out sum4Val) == true)
                {
                    balanceToPay += sum4Val;
                }
                Balance.InnerText = balanceToPay.ToString();
            }


            serviceRequestPayment payment = new serviceRequestPayment
            {
                ID = 0,
                ServiceRequestID = int.Parse(Request.QueryString["ServiceRequestID"].ToString()),
                DatePayment = "",
                NumPayment = 0,
                SumPayment = 0,
                ReferencePayment = "",
                IsApprovedPayment = false
            };

            payments.Add(payment);
            Session["payments"] = payments;
            RepeaterPayments.DataSource = payments;
            RepeaterPayments.DataBind();
        }

        protected void IsApprove1_CheckedChanged(object sender, EventArgs e)
        {

            updateBalanceValue();
            var btn = (CheckBox)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            HtmlInputControl Sum = (HtmlInputControl)item.FindControl("Sum1");
            HtmlInputControl DatePayment = (HtmlInputControl)item.FindControl("DatePayment1");
            HtmlInputControl Num = (HtmlInputControl)item.FindControl("Num1");
            HtmlInputControl ReferencePayment = (HtmlInputControl)item.FindControl("ReferencePayment1");
            Label ErrorCheckBox = (Label)item.FindControl("ErrorCheckBox");
            CheckBox IsApprove = (CheckBox)item.FindControl("IsApprove1");

            if (IsApprove.Checked)
            {

                if (string.IsNullOrEmpty(Sum.Value))
                {
                    ErrorCheckBox.Text = "יש למלא סכום כדי לאשר";
                    IsApprove.Checked = false;
                }
                else
                if (string.IsNullOrEmpty(DatePayment.Value))
                {
                    ErrorCheckBox.Text = "יש למלא תאריך כדי לאשר";
                    IsApprove.Checked = false;
                }
                else
                if (string.IsNullOrEmpty(ReferencePayment.Value))
                {
                    ErrorCheckBox.Text = "יש למלא אסמכתא כדי לאשר";
                    IsApprove.Checked = false;
                }
                else ErrorCheckBox.Text = "";

            }
        }

        public void updateBalanceValue()
        {
            List<serviceRequestPayment> payments = new List<serviceRequestPayment>();
            double SumPaid = 0;
            foreach (RepeaterItem item in RepeaterPayments.Items)
            {
                HtmlInputControl Sum = (HtmlInputControl)item.FindControl("Sum1");
                HtmlInputControl DatePayment = (HtmlInputControl)item.FindControl("DatePayment1");
                HtmlInputControl Num = (HtmlInputControl)item.FindControl("Num1");
                HtmlInputControl ReferencePayment = (HtmlInputControl)item.FindControl("ReferencePayment1");
                CheckBox IsApprove = (CheckBox)item.FindControl("IsApprove1");
                HiddenField hiddenPaymentID = (HiddenField)item.FindControl("hiddenPaymentID");

                serviceRequestPayment payment = new serviceRequestPayment
                {
                    ID = int.Parse(hiddenPaymentID.Value),
                    ServiceRequestID = int.Parse(Request.QueryString["ServiceRequestID"].ToString()),
                    SumPayment = (!string.IsNullOrWhiteSpace(Sum.Value) ? double.Parse(Sum.Value) : 0),
                    NumPayment = (!string.IsNullOrWhiteSpace(Num.Value) ? int.Parse(Num.Value) : 0),
                    DatePayment = DatePayment.Value,
                    ReferencePayment = ReferencePayment.Value,
                    IsApprovedPayment = IsApprove.Checked
                };

                if (payment.IsApprovedPayment == true)
                {
                    SumPaid += payment.SumPayment;
                }
                payments.Add(payment);
            }


            double AllSumVal;
            if (!string.IsNullOrWhiteSpace(AllSum.Value) && double.TryParse(AllSum.Value, out AllSumVal) == true)
            {
                double balanceToPay = AllSumVal - SumPaid;
                double sum4Val;
                if (IsApprove4.Checked == true && double.TryParse(Sum4.Value, out sum4Val) == true)
                {
                    balanceToPay += sum4Val;
                }
                Balance.InnerText = balanceToPay.ToString();
            }
            Session["payments"] = payments;
        }

        protected void IsApprove4_CheckedChanged(object sender, EventArgs e)
        {
            updateBalanceValue();

            if (IsApprove4.Checked)
            {

                if (string.IsNullOrEmpty(Sum4.Value))
                {
                    ErrorCheckBox.Text = "יש למלא סכום כדי לאשר";
                    IsApprove4.Checked = false;
                }
                else
                if (string.IsNullOrEmpty(DateCreditOrDenial.Value))
                {
                    ErrorCheckBox.Text = "יש למלא תאריך כדי לאשר";
                    IsApprove4.Checked = false;
                }
                else
                if (string.IsNullOrEmpty(ReferenceCreditOrDenial.Value))
                {
                    ErrorCheckBox.Text = "יש למלא אסמכתא כדי לאשר";
                    IsApprove4.Checked = false;
                }
                else ErrorCheckBox.Text = "";

            }
        }

        protected void btnReloadBalance_ServerClick(object sender, EventArgs e)
        {
            updateBalanceValue();
        }
    }
}