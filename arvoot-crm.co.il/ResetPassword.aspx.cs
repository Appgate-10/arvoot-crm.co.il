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
    public partial class _ResetPassword : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "Search";
        public string StrSrc { get { return strSrc; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {
                resetPasswordValidityWeb(Request.QueryString["Email"].ToString(), Request.QueryString["TempCode"].ToString(), long.Parse(Request.QueryString["jck"].ToString()));
            }
        }

       

        public void resetPasswordValidityWeb(string userEmail, string userTempCode, long UserID)
        {

            //-- בדיקה שלא חסרים פרמטרים נדרשים
            if (string.IsNullOrWhiteSpace(userEmail) || string.IsNullOrWhiteSpace(userTempCode) || string.IsNullOrWhiteSpace(UserID.ToString()))
            {
                //return "missing parameter";
            }
            SqlCommand cmd = new SqlCommand("Select Top 1 ID from Agent where Email = @Email And PasswordTempCode = @userTempCode And ID = @UserID ");
            cmd.Parameters.AddWithValue("@Email", userEmail);
            cmd.Parameters.AddWithValue("@userTempCode", userTempCode);
            cmd.Parameters.AddWithValue("@UserID", UserID);

            var res = DbProvider.GetOneParamValueString(cmd);
            if (res == null)
            {
                LinkErrorDiv.Visible = true;
                FormDiv.Visible = false;
            }
            //else
            //{
            //    FormDiv.Visible = true;
            //}

        }
        public bool resetPasswordWeb(string userPassword, string userTempCode, long UserID)
        {
            //-- בדיקה שלא חסרים פרמטרים נדרשים
            if (string.IsNullOrWhiteSpace(userPassword) || string.IsNullOrWhiteSpace(userTempCode) || string.IsNullOrWhiteSpace(UserID.ToString()))
            {
                //return "missing parameter";
            }
            SqlCommand cmd = new SqlCommand("Select Top 1 ID from Agent where PasswordTempCode = @TempCode And ID = @UserID ");
            cmd.Parameters.AddWithValue("@TempCode", userTempCode);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            var res = DbProvider.GetOneParamValueString(cmd);
            if (res == null)
            {
                return false;
            }
            else
            {

                SqlCommand cmdUpdate = new SqlCommand("Update Agent set Password = @userPassword, PasswordTempCode = @userPasswordTempCode where ID = @UserID");
                cmdUpdate.Parameters.AddWithValue("@UserID", res);
                cmdUpdate.Parameters.AddWithValue("@userPasswordTempCode", Helpers.RandomStrings(30));
                //string userPasswordHash = Md5.GetMd5Hash(Md5.CreateMd5Hash(), ConfigurationManager.AppSettings["SecretKey"] + userPassword);
                //userPasswordHash = Md5.GetMd5Hash(Md5.CreateMd5Hash(), "Pass755" + userPasswordHash);
                //cmdUpdate.Parameters.AddWithValue("@userPassword", userPasswordHash);


                cmdUpdate.Parameters.AddWithValue("@userPassword", userPassword);
                if (DbProvider.ExecuteCommand(cmdUpdate) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {

            if (PasswordaAgent.Value == "" || PasswordaAgent.Value == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('הכנס סיסמה חדשה');", true);
            }
            else if (PasswordaAgent.Value.Length < 8)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' סיסמה קצרה מדי, הכנס סיסמה של לפחות 8 ספרות');", true);
            }
            else if (RePasswordAgent.Value != PasswordaAgent.Value)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('הסיסמאות לא זהות');", true);
            }
            else
            {
                bool answer = resetPasswordWeb(PasswordaAgent.Value, Request.QueryString["TempCode"].ToString(), long.Parse(Request.QueryString["jck"].ToString()));

                if (answer == true)
                {
                    ChangedSuccessfullyDiv.Visible = true;
                    LinkErrorDiv.Visible = false;
                    FormDiv.Visible = false;
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('התרחשה שגיאה');", true);
                }


            }
        }
    }
}