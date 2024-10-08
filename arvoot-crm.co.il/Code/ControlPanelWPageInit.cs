using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using System.Web.SessionState;


namespace ControlPanel.HelpersFunctions
{
    public class ControlPanelInit
    {
        public ControlPanelInit()
        {
        }

        public void CheckManagerPermissions()
        {
            //-- להסיר את הערה של שני השורות הבאות במידה ורוצים לנתק את המשתמש
            HttpContext.Current.Session["SignIn"] = "";
            //System.Web.HttpContext.Current.Request.Cookies["ManagerCookie"]["ManagerID"] = "";

            bool SignInStatus = false;
            long AgentID = 0;
            try
            {
                SignInStatus = bool.Parse(HttpContext.Current.Session["SignIn"].ToString());
            }
            catch (Exception ex)
            { 
                HttpContext.Current.Session["SignIn"] = "false";
                SignInStatus = false;
            }

            if (SignInStatus != true)
            {
                //-- לא מחובר / - בדיקה באמצעות קוקי
                if (System.Web.HttpContext.Current.Request.Cookies["AgentCookie"] != null)
                {
                    try
                    {
                        AgentID = long.Parse(System.Web.HttpContext.Current.Request.Cookies["AgentCookie"]["AgentID"].ToString());
                    }catch (Exception ex){ AgentID = 0;}

                    if (AgentID > 0)
                    {
                        //-- בדיקה שה ID מתאים להצפנה של ה MD5 TODO
                        try
                        {
                            if (!Md5.VerifyMd5Hash(Md5.CreateMd5Hash(), (ConfigurationManager.AppSettings["SecretKey"] + AgentID.ToString()), System.Web.HttpContext.Current.Request.Cookies["AgentCookie"]["AgentIDToken"].ToString()))
                            {
                                System.Web.HttpContext.Current.Response.Redirect("SignIn.aspx");
                            }
                        }
                        catch (Exception ex) { System.Web.HttpContext.Current.Response.Redirect("SignIn.aspx"); }

                        //-- עבר בהצלחה את הבדיקה - עדכון נתוני המשתמש
                        SqlCommand cmd = new SqlCommand("Select Top 1 * From Agent where ID = @ID ");
                        cmd.Parameters.AddWithValue("@ID", AgentID);
                        DataTable dataTable = DbProvider.GetDataTable(cmd);
                        if (dataTable.Rows.Count > 0)
                        {
                            SignInStatus = true;
                            HttpContext.Current.Session["SignIn"] = true;
                            HttpContext.Current.Session["AgentID"] = long.Parse(dataTable.Rows[0]["ID"].ToString());
                            HttpContext.Current.Session["AgentName"] = dataTable.Rows[0]["FullName"].ToString();
                            HttpContext.Current.Session["AgentLevel"] = int.Parse(dataTable.Rows[0]["Level"].ToString());


                            System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentID"] = long.Parse(dataTable.Rows[0]["ID"].ToString()).ToString();
                            string AgentIDToken = Md5.GetMd5Hash(Md5.CreateMd5Hash(), ConfigurationManager.AppSettings["SecretKey"] + long.Parse(dataTable.Rows[0]["ID"].ToString()).ToString());
                            System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentIDToken"] = AgentIDToken;
                            System.Web.HttpContext.Current.Response.Cookies["AgentCookie"].Expires = (DateTime.Now.AddDays(1));
                        }

                    }
                }

            }
            else
            {
                SqlCommand cmd = new SqlCommand("Select Top 1 * From Agent where ID = @ID ");
                cmd.Parameters.AddWithValue("@ID", long.Parse(System.Web.HttpContext.Current.Request.Cookies["AgentCookie"]["AgentID"].ToString()));
                DataTable dataTable = DbProvider.GetDataTable(cmd);
                if (dataTable.Rows.Count > 0)
                {
                    HttpContext.Current.Session["SignIn"] = true;
                    HttpContext.Current.Session["AgentID"] = long.Parse(dataTable.Rows[0]["ID"].ToString());
                    HttpContext.Current.Session["AgentName"] = dataTable.Rows[0]["FullName"].ToString();

                    System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentID"] = long.Parse(dataTable.Rows[0]["ID"].ToString()).ToString();
                    string AgentIDToken = Md5.GetMd5Hash(Md5.CreateMd5Hash(), ConfigurationManager.AppSettings["SecretKey"] + long.Parse(dataTable.Rows[0]["ID"].ToString()).ToString());
                    System.Web.HttpContext.Current.Response.Cookies["AgentCookie"]["AgentIDToken"] = AgentIDToken;
                    System.Web.HttpContext.Current.Response.Cookies["AgentCookie"].Expires = (DateTime.Now.AddDays(1));
                }
            }


            if (SignInStatus != true)
            {
                System.Web.HttpContext.Current.Response.Redirect("SignIn.aspx");
            }

        }




        public string TempPassword()
        {
            return Membership.GeneratePassword(8, 1);
        }

        public string EmailTempPassword()
        {
            string newPassword = Membership.GeneratePassword(10, 0);
            newPassword = Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => "9");

            return newPassword.ToString();
        }

        public string FormatText(string strTemp)
        {

            //strTemp = Regex.Replace(strTemp, chr(13), "");
            //strTemp = Regex.Replace(strTemp, chr(10), "<br>");
            //strTemp = Regex.Replace(strTemp, chr(34), "'");

            strTemp = Regex.Replace(strTemp, "<BR>", "<br>");
            strTemp = Regex.Replace(strTemp, "</li><br><li>", "</li><li>");
            strTemp = Regex.Replace(strTemp, "</li><br><br><li>", "</li><li>");
            strTemp = Regex.Replace(strTemp, "<br></li>", "</li>");
            strTemp = Regex.Replace(strTemp, "<br><br><br>", "<br><br>");
            strTemp = Regex.Replace(strTemp, "<br><br><br><br>", "<br><br>");
            strTemp = Regex.Replace(strTemp, "<br><br><br><br><br>", "<br><br>");
            strTemp = Regex.Replace(strTemp, "<br><br><br><br><br><br>", "<br><br>");
            strTemp = Regex.Replace(strTemp, "<br><br><br><br><br><br><br>", "<br><br>");
            strTemp = Regex.Replace(strTemp, "<br><br></p>", "</p><br>");
            strTemp = Regex.Replace(strTemp, "<br><br><p>", "<p><br>");
            strTemp = Regex.Replace(strTemp, "<br></P><br><br><P>", "</P><br><P>");
            strTemp = Regex.Replace(strTemp, "</P><br><br><P>", "</P><br><P>");
            strTemp = Regex.Replace(strTemp, "</P><br><br><br><P>", "</P><br><P>");
            strTemp = Regex.Replace(strTemp, "<P>&nbsp;</P><BR>", "<br>");

            string StrFlash = "<OBJECT classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 codebase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0	 WIDTH=710 HEIGHT=290 ID=\"Object2\" VIEWASTEXT><param name=\"wmode\" value=\"transparent\"><PARAM NAME=movie VALUE=\"images/HowToBegin.swf\"><PARAM NAME=quality VALUE=high><EMBED wmode=\"transparent\" src=\"images/HowToBegin.swf\" quality=high WIDTH=710 HEIGHT=290 TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\"></EMBED></OBJECT>";

            strTemp = Regex.Replace(strTemp, "###HowToBegin###", StrFlash);

            //strTemp = Regex.Replace(strTemp, "(", "[");
            //strTemp = Regex.Replace(strTemp, ")", "]");

            return strTemp;
        }

        public bool IsValidEmail(string email)
        {
            string pattern = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.(com|co.il|net.il|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$";

            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            bool valid = false;

            if (string.IsNullOrEmpty(email))
            {
                valid = false;
            }
            else
            {
                valid = check.IsMatch(email);
            }
            return valid;
        }

        public bool IsInt(string strTemp)
        {
            string pattern = @"^\d*$";
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            if (string.IsNullOrEmpty(strTemp))
            {
                return false;
            }
            else
            {
                return check.IsMatch(strTemp);
            }
        }

        public bool IsValidHebrew(string strTemp)
        {
            string pattern = @"[a-z0-9]";

            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            bool valid = true;

            if (string.IsNullOrEmpty(strTemp))
            {
                valid = true;
            }
            else
            {
                valid = check.IsMatch(strTemp);
            }
            return valid;
        }

        public bool IsValidHebrewAndNumbers(string strTemp)
        {
            string pattern = @"[a-z]";

            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            bool valid = true;

            if (string.IsNullOrEmpty(strTemp))
            {
                valid = true;
            }
            else
            {
                valid = check.IsMatch(strTemp);
            }
            return valid;
        }

    }
}