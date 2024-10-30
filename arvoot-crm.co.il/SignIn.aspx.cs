//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using ControlPanel.HelpersFunctions;
using System.Net.Mail;

namespace ControlPanel
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.FindControl("AfterSignIn1").Visible = false;
            this.Master.FindControl("AfterSignIn2").Visible = false;
            this.Master.FindControl("AfterSignIn3").Visible = false;
            this.Master.FindControl("Div1").Visible = false;
        }

        protected void SignInBU_Click(object sender, EventArgs e)
        {

            int ErrorCount = 0;
            ErrorDiv.Visible = false;

            E_UserName_lable.Visible = false;
            E_Password_lable.Visible = false;
            if (string.IsNullOrWhiteSpace(Email.Value) || Email.Value == "אימייל")
            {
                E_UserName_lable.Visible = true;
                E_UserName_lable.Text = "יש להזין אימייל";
                ErrorCount++;
            }


            if (string.IsNullOrWhiteSpace(Password.Value) || Password.Value == "סיסמא")
            {
                E_Password_lable.Visible = true;
                E_Password_lable.Text = "יש להזין סיסמא";
                ErrorCount++;
            }

            if (ErrorCount == 0)
            {

                SqlCommand cmd = new SqlCommand("Select TOP 1 * From ArvootManagers where Email = @Email And Password = @Password");
                cmd.Parameters.AddWithValue("@Password",  Md5.GetMd5Hash(Md5.CreateMd5Hash(), "Pass755" + Password.Value));
                cmd.Parameters.AddWithValue("@Email", Email.Value);
                DataTable dataTable = DbProvider.GetDataTable(cmd);
                if (dataTable.Rows.Count > 0)
                {
                    
                    HttpContext.Current.Session["SignIn"] = true;
                    HttpContext.Current.Session["AgentID"] = long.Parse(dataTable.Rows[0]["ID"].ToString());
                    HttpContext.Current.Session["AgentName"] = dataTable.Rows[0]["FullName"].ToString();
                    HttpContext.Current.Session["AgentLevel"] = dataTable.Rows[0]["Type"].ToString();

                    System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentID"] = long.Parse(dataTable.Rows[0]["ID"].ToString()).ToString();
                    string AgentIDToken = Md5.GetMd5Hash(Md5.CreateMd5Hash(), ConfigurationManager.AppSettings["SecretKey"] + long.Parse(dataTable.Rows[0]["ID"].ToString()).ToString());
                    System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentIDToken"] = AgentIDToken;
                    System.Web.HttpContext.Current.Response.Cookies["AgentCookie"].Expires = (DateTime.Now.AddDays(1));


                    System.Web.HttpContext.Current.Response.Redirect("Default.aspx");

                }
                else
                {
                    HttpContext.Current.Session["SignIn"] = false;
                    HttpContext.Current.Session["AgentID"] = "";
                    HttpContext.Current.Session["AgentName"] = "";

                    System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentID"] = "";
                    System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentIDToken"] = "";
                    System.Web.HttpContext.Current.Response.Cookies["AgentCookie"].Expires = (DateTime.Now.AddMinutes(1));

                    ErrorDiv.InnerHtml = "שגיאה, אנא נסה שוב.";
                    ErrorDiv.Visible = true;
                }
            }

        }

        protected void ClosePopUp_Click(object sender, ImageClickEventArgs e)
        {
            ForgetPasswordpopUp.Visible = false;
        }






        protected void ForgetPassword_Click(object sender, EventArgs e)
        {
            E_Password_lable.Text = "";
            E_Password_lable.Visible = false;
            ForgetPasswordpopUp.Visible = true;
            ErrorLable.Visible = false;
            E_UserName_lable.Text = "";
            E_UserName_lable.Visible = false;
            UserEmailPopUp.Value = "";
            //UserEmailPopUp.Visible = false;
            ErrorDiv.Visible = false;
        }

        protected void BtnOkForgetPassword_Click(object sender, EventArgs e)
        {
            ErrorLable.Visible = false;
            if (UserEmailPopUp.Value == "" || UserEmailPopUp.Value == "אימייל")
            {
                ErrorLable.Visible = true;
                ErrorLable.Text = "הכנס אימייל";
            }
            else
            {
                string userEmail = UserEmailPopUp.Value;
                SqlCommand cmd = new SqlCommand("Select Top 1 * from ArvootManagers where Email = @Email ");
                cmd.Parameters.AddWithValue("@Email", userEmail);
                DataTable dataTable = DbProvider.GetDataTable(cmd);

                if (dataTable.Rows.Count > 0)
                {
                    string PasswordTempCode = Helpers.RandomStrings(30);
                    cmd = new SqlCommand("Update ArvootManagers set PasswordTempCode = @PasswordTempCode where ID = @AgentID");
                    cmd.Parameters.AddWithValue("@AgentID", dataTable.Rows[0]["ID"]);
                    cmd.Parameters.AddWithValue("@PasswordTempCode", PasswordTempCode);

                    if (DbProvider.ExecuteCommand(cmd) > 0)
                    {
                        // שליחת אימייל עם הקוד החדש
                        string Str = "<div align=\"center\"style=\"width:100%;text-align:center;\" dir=\"rtl\"><div align=\"right\" style=\"width:500px;\"><font size=\"3\" color=\"000000\" face=\"arial\"><img src=\"" + ConfigurationManager.AppSettings["EmailHeaderLogo"] + "\"><br /><br />";
                        Str = Str + "<br><br>הי " + dataTable.Rows[0]["FirstName"] + "!<br><br><br>";
                        Str = Str + "ביקשת לשנות את סיסמת " + ConfigurationManager.AppSettings["AppName"] + " שלך. אנא לחץ/י למטה כדי להשלים בקשה זו.<br><br><br><br>";
                        Str = Str + "אם לא ביקשת לאפס את הסיסמה, אנא התעלם/י ממייל זה ללא חשש.<br><br><br><u>לחץ/י על הקישור למטה כדי לאפס את הסיסמה שלך:</u><br><br><br>";


                        string url = "arvoot-crm.co.il/ResetPassword.aspx?Email=" + dataTable.Rows[0]["Email"] + "&TempCode=" + PasswordTempCode + "&jck=" + dataTable.Rows[0]["ID"] + "&tempiduser=" + Helpers.RandomStrings(32);
                        Str = Str + "<a href=\"" + url + "\">" + url + "</a><br><br><br><br><br>";
                        Str = Str + "</div></div><br><br>";

                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("contact@platinum-crm.co.il", ConfigurationManager.AppSettings["AppName"]);
                        message.To.Add(new MailAddress(userEmail));
                        message.Subject = "Reset Password - " + ConfigurationManager.AppSettings["AppName"];
                        message.Body = Str;
                        message.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient();
                        try
                        {
                            client.Send(message);
                            E_UserName_lable.Visible = true;
                            E_UserName_lable.Text = "נשלח אימייל לאיפוס סיסמא";
                            ForgetPasswordpopUp.Visible = false;
                        }
                        catch (Exception ex)
                        {

                        }


                    }
                }
                else
                {
                    ErrorLable.Visible = true;
                    ErrorLable.Text = "האימייל לא נמצא";
                }
            }
        }

    }
}