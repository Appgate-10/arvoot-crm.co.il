using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using MySql.Data.MySqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using System.Xml;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.UI;
using System.Data.SqlClient;
using System.Globalization;

namespace ControlPanel.HelpersFunctions
{
    class Helpers
    {
        // Dictionary to store irregular ordinals (1-10)
        private static readonly Dictionary<int, string> IrregularOrdinals = new Dictionary<int, string>
    {
        {1, "ראשון"},
        {2, "שני"},
        {3, "שלישי"},
        {4, "רביעי"},
        {5, "חמישי"},
        {6, "שישי"},
        {7, "שביעי"},
        {8, "שמיני"},
        {9, "תשיעי"},
        {10, "עשירי"}
    };
        public static async void SendPushNotification(string vMessage, string DeviceToken, int OsType, string NotificationType)
        {

            string AppName = "arvoot-crm";
            string Message = vMessage;
            string Sound = "";

            try
            {
                //-- שליחת ההתראה עצמה
                using (var client = new HttpClient())
                {
                    string sendUrl = "http://dev.appgate.co.il/AppGatePushNotifications/apns.php";
                    if (OsType == 1)
                    {
                        //-- android user
                        Message = "{ \"PushType\": \"" + NotificationType + "\", \"alert\": \"" + vMessage + "\" }";
                        sendUrl = "http://dev.appgate.co.il/AppGatePushNotifications/GCM.php";
                    }


                    var values = new Dictionary<string, string>
                    {
                        { "GoogleAppKey", ConfigurationManager.AppSettings["GoogleAppKey"] },
                        { "DT", DeviceToken },
                        { "PushType", NotificationType },
                        { "alert", Message },
                        { "message", Message },
                        { "App", AppName},
                        { "sound", Sound }
                    };

                    var content = new FormUrlEncodedContent(values);

                    var response = await client.PostAsync(sendUrl, content);

                    var responseString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex) { }

        }

        public static /*async*/ void SendMultiplePushNotifications(Control control, string vMessage, List<string> players_ids, string NotificationType)
        {

            string AppName = "אדר בצקים";
            string Message = vMessage;
            //string Sound = "";

            try
            {
                //-- שליחת ההתראה עצמה
                using (var client = new HttpClient())
                {
                    string sendUrl = "https://onesignal.com/api/v1/notifications";
                    //if (OsType == 1)
                    //{
                    //    //-- android user
                    //    Message = "{ \"PushType\": \"" + NotificationType + "\", \"alert\": \"" + vMessage + "\" }";
                    //    sendUrl = "https://dev.appgate.co.il/AppGatePushNotifications/GCM.php";
                    //}

                    //var values = new Dictionary<string, string>
                    //{
                    //    { "GoogleAppKey", ConfigurationManager.AppSettings["GoogleAppKey"] },
                    //    { "DT", DeviceToken },
                    //    { "PushType", NotificationType },
                    //    { "alert", Message },
                    //    { "message", Message },
                    //    { "App", AppName},
                    //    { "sound", Sound }
                    //};

                    //var content = new FormUrlEncodedContent(values);
                    //var response = await client.PostAsync(sendUrl, content);
                    //var responseString = await response.Content.ReadAsStringAsync();

                    
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var request = WebRequest.Create(sendUrl) as HttpWebRequest;

                    request.KeepAlive = true;
                    request.Method = "POST";
                    request.ContentType = "application/json; charset=utf-8";

                    string[] player_ids = players_ids.ToArray();

                    var obj = new
                    {
                        app_id = "5e1727cb-f0a2-46c5-b8af-87362f70eebf",
                        contents = new { en = Message, he = Message },
                        include_player_ids = player_ids,
                        headings = new { en = AppName, he = AppName }
                    };


                    var param = JsonConvert.SerializeObject(obj);
                    byte[] byteArray = Encoding.UTF8.GetBytes(param);

                    string responseContent = null;

                    try
                    {
                        using (var writer = request.GetRequestStream())
                        {
                            writer.Write(byteArray, 0, byteArray.Length);
                        }

                        using (var response1 = request.GetResponse() as HttpWebResponse)
                        {
                            using (var reader = new StreamReader(response1.GetResponseStream()))
                            {
                                responseContent = reader.ReadToEnd();
                                OneSignalResponse jsonObj = JsonConvert.DeserializeObject<OneSignalResponse>(responseContent);
                                if (jsonObj.recipients > 0)
                                {
                                    SqlCommand cmdPush = new SqlCommand("Insert Into Notifications (MessageText, CreationDate) Values (@message, GETUTCDATE())");
                                    cmdPush.Parameters.AddWithValue("@message", vMessage);
                                    if (DbProvider.ExecuteCommand(cmdPush) > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(control, control.GetType(), "showalert", "alert('ההודעה נשלחה בהצלחה ל" + jsonObj.recipients.ToString() + " לקוחות');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(control, control.GetType(), "showalert", "alert('התרחשה שגיאה בעת השליחה, נסה שוב');", true);
                                }
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        string s = ex.Message;
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());

                        ScriptManager.RegisterStartupScript(control, control.GetType(), "showalert", "alert('התרחשה שגיאה בעת השליחה, נסה שוב');", true);
                    }

                    System.Diagnostics.Debug.WriteLine(responseContent);
                }
            }
            catch (Exception ex) { }

        }

        public static string AgentEmailExist(string Email, long IDAgent)
        {

            ////-- בדיקה שלא חסרים פרמטרים נדרשים
            //if (string.IsNullOrWhiteSpace(Email))
            //{
            //    return "missing parameter";
            //}
            string sql = "Select Top 1 Email from ArvootManagers where Email = @Email  ";

            if (IDAgent != -1)
            {
                sql += " and ID<>" + IDAgent;
            }
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Email", Email);

            var res = DbProvider.GetOneParamValueString(cmd);
            if (res == null)
            {
                return "false";
            }
            else
            {
                return "true";
            }

        }

        public static string AgentPhoneExist(string Phone, long IDAgent)
        {

            string sql = "Select Top 1 Phone from ArvootManagers where Phone = @Phone  ";

            if (IDAgent != -1)
            {
                sql += " and ID<>" + IDAgent;
            }
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Phone", Phone);

            var res = DbProvider.GetOneParamValueString(cmd);
            if (res == null)
            {
                return "false";
            }
            else
            {
                return "true";
            }

        }

        public static string AgentTzExist(string Tz, long IDAgent)
        {

            //-- בדיקה שלא חסרים פרמטרים נדרשים
            if (string.IsNullOrWhiteSpace(Tz))
            {
                return "missing parameter";
            }
            string sql = "Select Top 1 Tz from ArvootManagers where Tz = @Tz ";

            if (IDAgent != -1)
            {
                sql += " and ID<>" + IDAgent;
            }
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Tz", Tz);

            var res = DbProvider.GetOneParamValueString(cmd);
            if (res == null)
            {
                return "false";
            }
            else
            {
                return "true";
            }

        }
        public static string AgentTzExist(string Tz)
        {

            //-- בדיקה שלא חסרים פרמטרים נדרשים
            if (string.IsNullOrWhiteSpace(Tz))
            {
                return "missing parameter";
            }
            string sql = "Select Top 1 Tz from ArvootManagers where Tz = @Tz ";


            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Tz", Tz);

            var res = DbProvider.GetOneParamValueString(cmd);
            if (res == null)
            {
                return "false";
            }
            else
            {
                return "true";
            }

        }

        public static async System.Threading.Tasks.Task<bool> SendSmsAsync(string Phone, string MSG)
        {
            if (Phone[0] == '0') { Phone = Phone.Substring(1); }

            var str1 = "{\"details\":{\"name\":\"arvoot\",\"from_name\":\"2Sign\",\"sms_sending_profile_id\":5,\"content\":\"" + MSG + "\"},\"scheduling\":{\"send_now\":true},\"mobiles\":[{\"phone_number\":\"+972" + Phone + "\",\"unsubscribe_text\":\"unsubscribe arvoot\"}]}";

            string url1 = "http://webapi.mymarketing.co.il/api/smscampaign/OperationalMessage";


            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url1);

            httpWebRequest.Headers.Add("Authorization", "0XFE6FF0CBC2532FE8E9FCB9EC3B2142296CC43E9443A9DB46A1BD3C158483712A149B639BEE18288D90CBE7AD9ACA5C86");

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                streamWriter.Write(str1);
                streamWriter.Flush();
                streamWriter.Close();

            }
            string gg;
            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                gg = ex.Message.ToString();
            }

            return true;
        }

        public static string CreateFileName(string ImageFileName)
        {
            return DateTime.UtcNow.ToString().Replace(" ", "").Replace("/", "").Replace(":", "") + Path.GetExtension(ImageFileName).ToString();

        }

        public static IEnumerable<string> RandomStrings(int minLength, int maxLength, int count, Random rng)
        {
            const string allowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            char[] chars = new char[maxLength];
            int setLength = allowedChars.Length;

            while (count-- > 0)
            {
                int length = rng.Next(minLength, maxLength + 1);

                for (int i = 0; i < length; ++i)
                {
                    chars[i] = allowedChars[rng.Next(setLength)];
                }

                yield return new string(chars, 0, length);
            }

        }

        public static string RandomStrings(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);

            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string RandomDigits(int size)
        {
            Random rnd = new Random();
            string ch = "";
            for (int i = 0; i < size; i++)
            {
                ch = ch + rnd.Next(9);
            }
            return ch;
        }

        public static string RandomDigitsSF(int type)
        {
            Random random = new Random();
            string ch = "";

            if (type == 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    ch = ch + random.Next(7);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    ch = ch + random.Next(9);
                }
            }

            return ch;
        }
        public void Pluggify()
        {

        }


    
    
    public static async Task<string> GetCoordinatesForAddress(string address)
        {
            string strReq1 = "https://maps.googleapis.com/maps/api/geocode/json?address=";
            strReq1=strReq1+address+"&key="+ConfigurationManager.AppSettings["GoogleMapsApiKey"] ;

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://maps.googleapis.com/maps/api/") };
            HttpResponseMessage httpResult = await httpClient.GetAsync(strReq1); // In my Pluggify() method, I replace spaces with + and then lowercase it all
            httpResult.EnsureSuccessStatusCode();
            var result =  httpResult.Content.ReadAsStringAsync().Result;
            //return await Task.Run(() =&gt; json)
            dynamic r = JsonConvert.DeserializeObject(result);
            // 
            JObject json = JObject.Parse(result);
            JToken x = json.GetValue("results");

            if (x.First != null)
            {
                var latString = ((JValue)x.First["geometry"]["location"]["lat"]).Value;
                var longString = ((JValue)x.First["geometry"]["location"]["lng"]).Value;
                var formattedAddress = ((JValue)x.First["formatted_address"]).Value;
                return string.Format("{0};{1};{2}", latString, longString,formattedAddress);
            }
            else
            {
                return string.Format("", "");
            }
            

            //return string.Format("{0}", location);
            
            ////Create a WebRequest
            //string strReq1="https://maps.googleapis.com/maps/api/geocode/json?address=";
            //strReq1=strReq1+ipaddress+"&key="+ConfigurationManager.AppSettings["GoogleMapsApiKey"] ;
            //WebRequest rssReq = WebRequest.Create(strReq1);
            ////Create a Proxy
            //WebProxy px = new WebProxy(strReq1, true);
            ////Assign the proxy to the WebRequest
            //rssReq.Proxy = px;
            ////Set the timeout in Seconds for the WebRequest
            //rssReq.Timeout = 2000;
            //try
            //{
            //    //Get the WebResponse
            //    WebResponse rep = rssReq.GetResponse();
            //    //Read the Response in a XMLTextReader
            //    XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());   
            //    StreamReader reader = new StreamReader(rep.GetResponseStream(), Encoding.UTF8);
            //    //Create a new DataSet
            //    DataSet ds = new DataSet();
            //    //Read the Response into the DataSet
            //    ds.ReadXml(reader);
            //    return ds.Tables[0];
            //}
            //catch
            //{
            //    return null;
            //}

        }

        public static string GetLocation(string ipaddress)
        {
            string strReq1 = "https://maps.googleapis.com/maps/api/geocode/json?address=";
            strReq1 = strReq1 + ipaddress + "&key=" + ConfigurationManager.AppSettings["GoogleMapsApiKey"];
            HttpWebRequest request = WebRequest.Create(strReq1) as HttpWebRequest;
            //optional
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();

            return null;
            ////Create a WebRequest

            //WebRequest rssReq = WebRequest.Create("http://freegeoip.appspot.com/xml/"

            //    + ipaddress);



            ////Create a Proxy

            //WebProxy px = new WebProxy("http://freegeoip.appspot.com/xml/"

            //    + ipaddress, true);



            ////Assign the proxy to the WebRequest

            //rssReq.Proxy = px;



            ////Set the timeout in Seconds for the WebRequest

            //rssReq.Timeout = 2000;

            //try
            //{

            //    //Get the WebResponse

            //    WebResponse rep = rssReq.GetResponse();



            //    //Read the Response in a XMLTextReader

            //    XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());



            //    //Create a new DataSet

            //    DataSet ds = new DataSet();



            //    //Read the Response into the DataSet

            //    ds.ReadXml(xtr);

            //    return ds.Tables[0];

            //}

            //catch
            //{

            //    return null;

            //}

        }


        public static string insuredTzExist(string Tz,long IDLead)
        {

            //-- בדיקה שלא חסרים פרמטרים נדרשים
            if (string.IsNullOrWhiteSpace(Tz))
            {
                return "missing parameter";
            }
            string sql = "Select Top 1 Tz from Lead where Tz = @Tz ";
            if (IDLead != -1)
            {
                sql += " and ID<>" + IDLead;
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Tz", Tz);

            var res = DbProvider.GetOneParamValueString(cmd);
            if (res == null)
            {
                return "false";
            }
            else
            {
                return "true";
            }

        }      
        public static string insuredPhoneExist(string Phone, long IDLead)
        {

            //-- בדיקה שלא חסרים פרמטרים נדרשים
            if (string.IsNullOrWhiteSpace(Phone))
            {
                return "missing parameter";
            }
            string sql = "Select Top 1 Phone1 from Lead where Phone1 = @Phone1";
            if (IDLead != -1)
            {
                sql += " and ID<>" + IDLead;
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Phone1", Phone);

            var res = DbProvider.GetOneParamValueString(cmd);
            if (res == null)
            {
                return "false";
            }
            else
            {
                return "true";
            }

        }

        public static string NumberToHebrewOrdinal(int number)
        {
            // Check for invalid input
            if (number < 1)
            {
                return "Invalid input";
            }

            if (IrregularOrdinals.TryGetValue(number, out string irregularOrdinal))
            {
                return irregularOrdinal;
            }

            return number.ToString();
        }  


        public static void loadActivityHistoryOnAdd(Page page)
        {
            DateTime seldate = ((System.Web.UI.WebControls.Calendar)(page.Master.FindControl("activitiesCal"))).SelectedDate;
            if (seldate == DateTime.MinValue)
            {
                ((DesignDisplay)page.Master).loadActivityHistory(DateTime.Today);
            }
            else
            {
                ((DesignDisplay)page.Master).loadActivityHistory(seldate);
            }

            ((UpdatePanel)page.Master.FindControl("AddForm2")).Update();
        }

    }
}

public class OneSignalResponse
{
    public string id { get; set; }
    public int recipients { get; set; }
    public string external_id { get; set; }
    public Dictionary<string, object> errros { get; set; }
}//		responseContent	"{\"id\":\"d6fa1364-ea16-4936-9460-1cdc4a0ca47f\",\"recipients\":1,\"external_id\":null,\"errors\":{\"invalid_player_ids\":[\"f6ad85c4-9176-4141-ab1f-c574fd324238\"]}}"	string
