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
using System.Text;

namespace ControlPanel
{
    public partial class _serviceRequestAdd : System.Web.UI.Page
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
                SelectMethodsPayment.Items.Insert(0, new ListItem("בחר אמצעי תשלום", ""));
                string[] months = {
                     "01", "02", "03", "04", "05", "06",
                     "07", "08", "09", "10", "11", "12"
                };
                SelectMonth.DataSource = months;
                SelectMonth.DataBind();
                List<string> years = new List<string>();
                for (int i = DateTime.Now.Year; i <= DateTime.Now.Year + 30; i++)
                {
                    years.Add(i.ToString());
                }

                SelectYear.DataSource = years.ToArray();
                SelectYear.DataBind();
                loadData();
            }
        }


        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        public void loadData()
        {
            string sql = @"select Lead.FirstName + ' ' + Lead.LastName as FullName from Lead 
                            left join Offer o on o.LeadID =Lead.ID  where o.ID = @ID ";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@ID", Request.QueryString["OfferID"]);

            DataTable dt = DbProvider.GetDataTable(cmd);
            if (dt.Rows.Count > 0)
            {
                FullName.Text = dt.Rows[0]["FullName"].ToString();

            }

            List<serviceRequestPayment> payments = new List<serviceRequestPayment>();
            serviceRequestPayment payment = new serviceRequestPayment
            {
                ID = 0,
                ServiceRequestID = 0,
                DatePayment = "",
                NumPayment = 0,
                SumPayment = 0,
                ReferencePayment = "",
                IsApprovedPayment = false
            };
            serviceRequestPayment payment2 = new serviceRequestPayment
            {
                ID = 0,
                ServiceRequestID = 0,
                DatePayment = "",
                NumPayment = 0,
                SumPayment = 0,
                ReferencePayment = "",
                IsApprovedPayment = false
            };
            payments.Add(payment);
            payments.Add(payment2);
            Session["payments"] = payments;
            RepeaterPayments.DataSource = payments;
            RepeaterPayments.DataBind();
        }
        //protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    var divShowStatus = (HtmlGenericControl)e.Item.FindControl("ShowStatus");
        //    ImageButton btnSuspensionBU = (ImageButton)e.Item.FindControl("SuspensionBU");
        //    ImageButton btnActivatingBU = (ImageButton)e.Item.FindControl("ActivatingBU");

        //    if (int.Parse(DataBinder.Eval(e.Item.DataItem, "Show").ToString()) == 1)
        //    {
        //        divShowStatus.Attributes.Add("class", "ListDivShowStatusGreen");
        //        btnSuspensionBU.Visible = true;
        //        btnActivatingBU.Visible = false;
        //    }
        //    else
        //    {
        //        divShowStatus.Attributes.Add("class", "ListDivShowStatusRed");
        //        btnSuspensionBU.Visible = false;
        //        btnActivatingBU.Visible = true;
        //    }
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
        //public void loadUsers(int page)
        //{
        //    //int PageNumber = 1;
        //    int PageNumber = page;
        //    if (Request.QueryString["Page"] != null)
        //    {
        //        PageNumber = int.Parse(Request.QueryString["Page"]);
        //    }
        //    int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
        //    int CurrentRow = (PageNumber == 1) ? 0 : (PageSize * (PageNumber - 1));
        //    long ItemCount = 0;

        //    string sql = @"SELECT IDUser, Users.ImageFile, CONCAT(Users.userFirstName, ' ', Users.userLastName) AS userFullName, userEmail, userPhone, JoinDate, Users.Show, 
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

        public bool funcSave(object sender, EventArgs e)
        {
            //return false;
            List<serviceRequestPayment> payments = new List<serviceRequestPayment>();
            foreach (RepeaterItem item in RepeaterPayments.Items)
            {
                HtmlInputControl Sum = (HtmlInputControl)item.FindControl("Sum1");
                HtmlInputControl DatePayment = (HtmlInputControl)item.FindControl("DatePayment1");
                HtmlInputControl Num = (HtmlInputControl)item.FindControl("Num1");
                HtmlInputControl ReferencePayment = (HtmlInputControl)item.FindControl("ReferencePayment1");
                CheckBox IsApprove = (CheckBox)item.FindControl("IsApprove1");

                serviceRequestPayment payment = new serviceRequestPayment
                {
                    ID = 0,
                    ServiceRequestID = 0,
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
           if(SelectPurpose.SelectedIndex == 0)
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מטרת הגבייה";
                return false;
            }

            if (payments.Count == 0)
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין פרטי תשלום ראשון";
                return false;
            }
            if (payments[0].SumPayment == 0)
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין סכום לתשלום ראשון";
                return false;
            }
            if (payments[0].DatePayment == "")
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין תאריך תשלום ראשון";
                return false;
            }
            if (payments[0].NumPayment == 0)
            {
                ErrorCount++;
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש להזין מספר תשלומים לתשלום ראשון";
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
                            FormError_lable.Visible = true;
                            FormError_lable.Text = "יש להזין סכום לתשלום " + Helpers.NumberToHebrewOrdinal(i + 1);
                            return false;
                        }
                        if (payments[i].DatePayment == "")
                        {
                            FormError_lable.Visible = true;
                            FormError_lable.Text = "יש להזין תאריך תשלום " + Helpers.NumberToHebrewOrdinal(i + 1); ;
                            return false;
                        }
                        if (payments[i].NumPayment == 0)
                        {
                            FormError_lable.Visible = true;
                            FormError_lable.Text = "יש להזין מספר תשלומים לתשלום " + Helpers.NumberToHebrewOrdinal(i + 1); ;
                            return false;
                        }
                    }
                }
                
            }

            if(SelectMethodsPayment.SelectedIndex == 1)
            {
                if (BankName.Value == "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "יש להזין שם בנק";
                    return false;
                } 
                if (Branch.Value == "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "יש להזין סניף";
                    return false;
                }
                if (AccountNumber.Value == "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "יש להזין מספר חשבון";
                    return false;
                }
            }
            else if(SelectMethodsPayment.SelectedIndex == 2)
            {
                if (CreditNumber.Value == "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "יש להזין מספר כרטיס";
                    return false;
                }
                //if (CreditValidity.Value == "")
                //{
                //    ErrorCount++;
                //    FormError_lable.Visible = true;
                //    FormError_lable.Text = "יש להזין תוקף";
                //    return false;
                //}
                if (CardholdersID.Value == "")
                {
                    ErrorCount++;
                    FormError_lable.Visible = true;
                    FormError_lable.Text = "יש להזין ת.ז. בעל הכרטיס";
                    return false;
                }
            }


            if (ErrorCount == 0)
            {
                string sql = @" INSERT INTO ServiceRequest (OfferID,Invoice,Sum,Note,PurposeID,
                                SumCreditOrDenial,DateCreditOrDenial,NumCreditOrDenial,ReferenceCreditOrDenial,NoteCreditOrDenial,
                                IsApprovedCreditOrDenial,PaymentMethodID,BankName,Branch,AccountNumber,CreditNumber,CreditValidity,CardholdersID)
                                OUTPUT Inserted.ID 
                                VALUES (@OfferID,@Invoice,@Sum,@Note,@PurposeID,@SumCreditOrDenial,@DateCreditOrDenial,@NumCreditOrDenial,@ReferenceCreditOrDenial,@NoteCreditOrDenial,
                                @IsApprovedCreditOrDenial,@PaymentMethodID,@BankName,@Branch,@AccountNumber,@CreditNumber,@CreditValidity,@CardholdersID )";

                //Balance,@Balance

                /*SumPayment1,DatePayment1,NumPayment1,ReferencePayment1,
                                IsApprovedPayment1,SumPayment2,DatePayment2,NumPayment2,ReferencePayment2,IsApprovedPayment2,SumPayment3,DatePayment3,NumPayment3,
                                ReferencePayment3,IsApprovedPayment3,
                
                 ,@SumPayment1,@DatePayment1,@NumPayment1,@ReferencePayment1,
                                @IsApprovedPayment1,@SumPayment2,@DatePayment2,@NumPayment2,@ReferencePayment2,@IsApprovedPayment2,@SumPayment3,@DatePayment3,@NumPayment3,
                                @ReferencePayment3,@IsApprovedPayment3
                 */

                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@OfferID", Request.QueryString["OfferID"]);
                cmd.Parameters.AddWithValue("@Invoice", Invoice.Value);
                cmd.Parameters.AddWithValue("@Sum", AllSum.Value);
                cmd.Parameters.AddWithValue("@Note", Note.Value);
                //cmd.Parameters.AddWithValue("@Balance", Balance.Value);
                cmd.Parameters.AddWithValue("@PurposeID", SelectPurpose.Value);
                //cmd.Parameters.AddWithValue("@SumPayment1", Sum1.Value);
                //cmd.Parameters.AddWithValue("@DatePayment1", DatePayment1.Value);
                //cmd.Parameters.AddWithValue("@NumPayment1", Num1.Value);
                //cmd.Parameters.AddWithValue("@ReferencePayment1", string.IsNullOrEmpty(ReferencePayment1.Value) ? (object)DBNull.Value : ReferencePayment1.Value);
                //cmd.Parameters.AddWithValue("@IsApprovedPayment1", IsApprove1.Checked?"1":"0");
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
                cmd.Parameters.AddWithValue("@CreditNumber", string.IsNullOrEmpty(CreditNumber.Value) ? (object)DBNull.Value : "XXXX-XXXX-XXXX-" + CreditNumber.Value.Substring(12));
                cmd.Parameters.AddWithValue("@CreditValidity", string.IsNullOrEmpty(SelectMonth.Value) || string.IsNullOrEmpty(SelectYear.Value) ? (object)DBNull.Value : SelectMonth.Value +"/"+ SelectYear.Value);
                cmd.Parameters.AddWithValue("@CardholdersID", string.IsNullOrEmpty(CardholdersID.Value) ? (object)DBNull.Value : CardholdersID.Value);

                int serviceRequestID = DbProvider.ExecuteIntScalar(cmd);

                if (serviceRequestID > 0)
                {
                    foreach (serviceRequestPayment payment in payments)
                    {
                        if (payment.SumPayment > 0)
                        {
                            string sqlPayment = @"INSERT INTO ServiceRequestPayment 
                                            (ServiceRequestID,SumPayment,DatePayment,NumPayment,ReferencePayment,IsApprovedPayment)
                                            VALUES (@serviceID, @sumP, @dateP, @numP, @referenceP, @isApproved)";
                            SqlCommand cmdPayment = new SqlCommand(sqlPayment);
                            cmdPayment.Parameters.AddWithValue("@serviceID", serviceRequestID);
                            cmdPayment.Parameters.AddWithValue("@sumP", payment.SumPayment);
                            cmdPayment.Parameters.AddWithValue("@dateP", payment.DatePayment);
                            cmdPayment.Parameters.AddWithValue("@numP", payment.NumPayment);
                            cmdPayment.Parameters.AddWithValue("@referenceP", string.IsNullOrEmpty(payment.ReferencePayment) ? (object)DBNull.Value : payment.ReferencePayment);
                            cmdPayment.Parameters.AddWithValue("@isApproved", payment.IsApprovedPayment == true ? 1 : 0);
                            DbProvider.ExecuteCommand(cmdPayment);
                        }
                            
                    }
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
                Session["payments"] = null;
                System.Web.HttpContext.Current.Response.Redirect(ListPageUrl + "?OfferID=" + Request.QueryString["OfferID"]);
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


        protected void BtnFamilyStatus_Click(object sender, EventArgs e)
        {
            //bool isOpen = DivRBFamilyStatus.Visible;
            //if (isOpen == false)
            //{
            //    DivRBFamilyStatus.Visible = true;
            //}
            //else
            //{
            //    DivRBFamilyStatus.Visible = false;
            //}

        }
        protected void RadioButttonFamilyStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DivRBFamilyStatus.Visible = false;
            //BtnFamilyStatus.Text = SelectPurpose.Value;
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
        protected void RadioButttonMethodsPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            DivRBMethodsPayment.Visible = false;
            //BtnMethodsPayment.Text = RadioButttonMethodsPayment.SelectedItem.Text;
        }

        protected void RepeaterPayments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl paymentTitle = (HtmlGenericControl)e.Item.FindControl("paymentTitle");
                HtmlGenericControl sumTitle = (HtmlGenericControl)e.Item.FindControl("sumTitle");
                
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

                serviceRequestPayment paymentFromRepeater = new serviceRequestPayment
                {
                    ID = 0,
                    ServiceRequestID = 0,
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
                ServiceRequestID = 0,
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

                serviceRequestPayment payment = new serviceRequestPayment
                {
                    ID = 0,
                    ServiceRequestID = 0,
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
        }

        protected void btnReloadBalance_ServerClick(object sender, EventArgs e)
        {
            updateBalanceValue();
        }
    }

    public class serviceRequestPayment
    {
        public int ID { get; set; }
        public int ServiceRequestID { get; set; }
        public double SumPayment { get; set; }
        public string DatePayment { get; set; }
        public int NumPayment { get; set; }
        public string ReferencePayment { get; set; }
        public bool IsApprovedPayment { get; set; }

    }
}

