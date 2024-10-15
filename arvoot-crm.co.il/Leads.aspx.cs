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
using System.IO;

namespace ControlPanel
{
    /// <summary>
    //Heni
    /// </summary>
    public partial class _leads : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();
        private string strSrc = "Search";
        public string StrSrc { get { return strSrc; } }
        string FileName1;
        public string ListPageUrl = "Leads.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ImageFile_1_display.Attributes.Add("onclick", "document.getElementById('" + ImageFile_1_FileUpload.ClientID + "').click();");
            if (!Page.IsPostBack)
            {
                Pageinit.CheckManagerPermissions();
                if (HttpContext.Current.Session["AgentLevel"] != null && int.Parse(HttpContext.Current.Session["AgentLevel"].ToString()) == 1)
                {
                    MoveTo.Visible = true;
                    CreateEmployee.Visible = true;
                    SetStatus.Visible = true;
                }
                ManagementPermission_Click(this, EventArgs.Empty);

                loadUsers(1,false);
            }
        }

        protected void ImageFile_1_btnUpload_Click(object sender, EventArgs e)
        {
            int maxFileSize = 262144;
            ImageFile_1_lable.Visible = false;
            ImageFile_1_lable_2.Visible = false;
            if (ImageFile_1_FileUpload.HasFile && ImageFile_1_FileUpload.PostedFile.ContentLength < maxFileSize)
            {
                try
                {
                    string ext = System.IO.Path.GetExtension(ImageFile_1_FileUpload.FileName).ToLower();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
                    {
                        System.IO.Stream fs = ImageFile_1_FileUpload.PostedFile.InputStream;
                        System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        ImageFile_1_display.Src = "data:image/png;base64," + base64String;
                        Session["imgFileUpload1"] = ImageFile_1_FileUpload;
                    }
                    else
                    {
                        ImageFile_1_lable_2.Text = "* הסיומת לא תקינה";
                        ImageFile_1_lable_2.Visible = true;
                    }
                }
                catch
                {
                    ImageFile_1_lable_2.Text = "* בבקשה נסה שוב";
                    ImageFile_1_lable_2.Visible = true;
                }
            }
            else
            {
                ImageFile_1_lable_2.Text = "* התמונה גדולה מ250קב,בבקשה הכנס תמונה חדשה";

                ImageFile_1_lable_2.Visible = true;
            }
        }
        protected void ManagementPermission_Click(object sender, EventArgs e)
        {
            ManagementPermission.Attributes.Add("class", "PermissionsChoose");
            AgentPermission.Attributes.Add("class", "Permissions");
            SupervisorPermission.Attributes.Add("class", "Permissions");
            HttpContext.Current.Session["Add_Level"] = 1;
        }

        protected void AgentPermission_Click(object sender, EventArgs e)
        {
            ManagementPermission.Attributes.Add("class", "Permissions");
            AgentPermission.Attributes.Add("class", "PermissionsChoose");
            SupervisorPermission.Attributes.Add("class", "Permissions");
            HttpContext.Current.Session["Add_Level"] = 2;

        }

        protected void SupervisorPermission_Click(object sender, EventArgs e)
        {
            ManagementPermission.Attributes.Add("class", "Permissions");
            AgentPermission.Attributes.Add("class", "Permissions");
            SupervisorPermission.Attributes.Add("class", "PermissionsChoose");      
            HttpContext.Current.Session["M_Level"] = 3;
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //CheckBox cb = (CheckBox)e.Item.FindControl("chk");
                //cb.ID = "CheckBox_" + DataBinder.Eval(e.Item.DataItem, "ID").ToString();

                Image image = (Image)e.Item.FindControl("MainStatus");
                if (int.Parse(DataBinder.Eval(e.Item.DataItem, "FirstStatus").ToString()) == 2)
                {
                    image.ImageUrl = "~/images/icons/Status_1.png";

                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "FirstStatus").ToString()) == 3)
                {
                    image.ImageUrl = "~/images/icons/Status_3_1.png";

                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "FirstStatus").ToString()) == 4)
                {

                    image.ImageUrl = "~/images/icons/Status_3_2.png";
                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "FirstStatus").ToString()) == 5)
                {
                    image.ImageUrl = "~/images/icons/Status_3_3.png";

                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "FirstStatus").ToString()) == 6)
                {
                    image.ImageUrl = "~/images/icons/Status_5.png";

                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "FirstStatus").ToString()) == 7)
                {
                    image.ImageUrl = "~/images/icons/Status_6.png";

                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "FirstStatus").ToString()) == 8)
                {
                    image.ImageUrl = "~/images/icons/Status_2.png";

                } 
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "FirstStatus").ToString()) == 9)
                {
                    image.ImageUrl = "~/images/icons/Not_Relevant_Button.png";

                }
                else image.ImageUrl = "";


            TextBox image2 = (TextBox)e.Item.FindControl("SubStatus");
            if (int.Parse(DataBinder.Eval(e.Item.DataItem, "SecondStatusLeadID").ToString()) == 1)
            {
                image2.Visible = false;
                //image2.Style["background-image"] = "url('../images/icons/Secondary_Status_1.png')";
                //image2.Style["color"] = "#CD9CCA";
            }
            else
          
                if (int.Parse(DataBinder.Eval(e.Item.DataItem, "SecondStatusLeadID").ToString()) == 2)
                {

                    image2.Style["background-image"] = "url('../images/icons/Secondary_Status_1.png')";
                    image2.Style["color"] = "#CD9CCA";
                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "SecondStatusLeadID").ToString()) == 3)
                {

                    image2.Style["background-image"] = "url('../images/icons/Secondary_Status_2.png')";
                    image2.Style["color"] = "#77C9FF";

                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "SecondStatusLeadID").ToString()) == 4)
                {
                    image2.Style["color"] = "#FFC253";
                    image2.Style["background-image"] = "url('../images/icons/Secondary_Status_3.png')";
                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "SecondStatusLeadID").ToString()) == 5)
                {
                    image2.Style["color"] = "#8DD980";
                    image2.Style["background-image"] = "url('../images/icons/Secondary_Status_4.png')";
                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "SecondStatusLeadID").ToString()) == 6)
                {
                    image2.Style["color"] = "#A3A3A3";
                    image2.Style["background-image"] = "url('../images/icons/Secondary_Status_5.png')";
                }
                else if (int.Parse(DataBinder.Eval(e.Item.DataItem, "SecondStatusLeadID").ToString()) == 7)
                {
                    image2.Style["color"] = "#35508C";
                    image2.Style["background-image"] = "url('../images/icons/Secondary_Status_6.png')";
                }
                else
                {
                    image2.Style["color"] = "#35508C";
                    image2.Style["background-image"] = "url('../images/icons/Secondary_Status_6.png')";
                }
            }
        }
        public void loadUsers(int page, bool shouldUpdate = false)
        {
            //int PageNumber = 1;
            int PageNumber = page;
            if (Request.QueryString["Page"] != null)
            {
                PageNumber = int.Parse(Request.QueryString["Page"]);
            }
            int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            int CurrentRow = (PageNumber == 1) ? 0 : (PageSize * (PageNumber - 1));
            long ItemCount = 0;
            string sqlWhere = "";
            SqlCommand cmd = new SqlCommand();
            string sql = @"select   Lead.ID ,CONVERT(varchar, Lead.CreateDate, 104) AS  CreateDate,FirstName,LastName,Lead.Tz,Phone1,FirstStatusLeadID FirstStatus,SecondStatusLead.Status SecondStatus, SecondStatusLeadID
                                 ,CONCAT(CONVERT(varchar, TrackingTime, 104), ' ', CONVERT(VARCHAR(5), TrackingTime, 108)) AS TrackingTime,Note,Agent.FullName as AgentName
                                  from Lead
                                  left join SecondStatusLead on Lead.SecondStatusLeadID=SecondStatusLead.ID
                                  left join Agent on Agent.ID = Lead.AgentID
                                  where Lead.IsContact=0
                                  ";
            

            if (Request.QueryString["Q"] != null)
            {
                Session["search"] = Request.QueryString["Q"];
                if (Request.QueryString["Q"].ToString().Length > 0)
                {
                    sqlWhere =  " and ( userFirstName like @SrcParam Or userLastName like @SrcParam Or userEmail like @SrcParam OR userPhone like @SrcParam )";
                }
                strSrc = Request.QueryString["Q"].ToString();
            }
            try
            {
                if (int.Parse(Session["subStatus"].ToString()) > 1)
                {
                    
                    SubStatusList.SelectedValue = Session["subStatus"].ToString();
                    
                    sqlWhere += " and  SecondStatusLeadID = @subStatus";
                    cmd.Parameters.AddWithValue("@subStatus", Session["subStatus"].ToString());
                }
            }
            catch(Exception) { }

            try
            {
                if (int.Parse(Session["selectedAgent"].ToString()) > 1)
                {

                    AgentsList.SelectedValue = Session["selectedAgent"].ToString();

                    sqlWhere += " and agent.id = @agentID";
                    cmd.Parameters.AddWithValue("@agentID", Session["selectedAgent"].ToString());
                }
            }
            catch (Exception) { }

            try
            {
                if (int.Parse(Session["mainStatus"].ToString()) > 1)
                {
                    
                    MainStatusList.SelectedValue = Session["mainStatus"].ToString();
                    
                    sqlWhere += " and FirstStatusLeadID = @mainStatus";
                    cmd.Parameters.AddWithValue("@mainStatus", Session["mainStatus"].ToString());
                }
            }
            catch(Exception) { }

            //להציג את הלידים מהישן לחדש
            string sqlOrder = " Order by Lead.CreateDate desc  OFFSET  " + CurrentRow.ToString() + "  ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY ";

            //-- ניהול Paging
            string sqlCnt = "Select Count(ID) FROM Lead where Lead.IsContact=0";
           
            SqlCommand cmdCount = new SqlCommand(sqlCnt+sqlWhere);
            try { cmdCount.Parameters.AddWithValue("@SrcParam", "%" + Request.QueryString["Q"].ToString() + "%"); }
            catch (Exception) { }
            ItemCount = DbProvider.GetOneParamValueLong(cmdCount);
            if (ItemCount > PageSize)
            {
                string str = "", str1 = "";
                if (PageNumber > 1) { str = str + "<a href=\"Leads.aspx?Page=" + (PageNumber - 1).ToString() + str1 + "\"\" title=\"Back\">&laquo;</a>"; }

                int iRunFrom = ((PageNumber - 4) < 1) ? 1 : (PageNumber - 4);
                int iRunUntil = (int)Math.Ceiling((double)ItemCount / (double)PageSize);
                Session["Page"] = PageNumber;
                int iRun;
                for (iRun = iRunFrom; iRun <= iRunUntil && iRun < (iRunFrom + 10); iRun++)
                {
                    str = str + "<a href=\"Leads.aspx?Page=" + iRun.ToString() + str1 + "\">" + iRun.ToString() + "</a>";
                }
                str = str.Replace(">" + PageNumber.ToString() + "</a>", " class=\"active\">" + PageNumber.ToString() + "</a>");

                if (PageNumber < (iRun - 1)) { str = str + "<a href=\"Leads.aspx?Page=" + (PageNumber + 1).ToString() + str1 + "\"\" title=\"Next\">&raquo;</a>"; }

                PageingDiv.InnerHtml = str;
            }
            else
            {
                PageingDiv.InnerHtml = "";
            }

            cmd.CommandText = sql +sqlWhere+ sqlOrder;

            try
            {
                cmd.Parameters.AddWithValue("@SrcParam", "%" + Request.QueryString["Q"].ToString() + "%");
            }
            catch (Exception) { }
            DataSet ds = DbProvider.GetDataSet(cmd);

            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            SqlCommand cmdStatus = new SqlCommand("SELECT * FROM FirstStatusLead");
            DataSet dsStatus = DbProvider.GetDataSet(cmdStatus);
            MainStatusList.DataSource = dsStatus;
            MainStatusList.DataTextField = "Status";
            MainStatusList.DataValueField = "ID";
            MainStatusList.DataBind();
            MainStatusList.Items.Insert(0, new ListItem("סטטוס ראשי", ""));
            
            SqlCommand cmdSubStatus = new SqlCommand("SELECT * FROM SecondStatusLead");
            DataSet dsSubStatus = DbProvider.GetDataSet(cmdSubStatus);
            SubStatusList.DataSource = dsSubStatus;
            SubStatusList.DataTextField = "Status";
            SubStatusList.DataValueField = "ID";
            SubStatusList.DataBind();
            SubStatusList.Items.Insert(0, new ListItem("סטטוס משני", ""));

            SqlCommand cmdAgents = new SqlCommand("SELECT  FullName as AgentName,ID FROM Agent");
            DataSet dsAgents = DbProvider.GetDataSet(cmdAgents);
            AgentsList.DataSource = dsAgents;
            AgentsList.DataTextField = "AgentName";
            AgentsList.DataValueField = "ID";
            AgentsList.DataBind();
            AgentsList.Items.Insert(0, new ListItem("סוכן", ""));

            if (shouldUpdate == true)
            {
                AddForm.Update();
            }
        }

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
        protected void CopyLid_Click(object sender, ImageClickEventArgs e)
        {
        }
        protected void ClosePopUpAddAgent_Click(object sender, ImageClickEventArgs e)
        {
            AddAgentPopUp.Visible = false;
        } 
        protected void AddNewAgent_Click(object sender, EventArgs e)
        {
            AddAgentPopUp.Visible = true;
        }

        public bool funcSave(object sender, EventArgs e)
        {
            int ErrorCount = 0;

            ImageFile_1_lable_2.Visible = false;
            FormErrorAgent_lable.Visible = false;
            ImageFile_1_lable.Visible = false;

            if (string.IsNullOrWhiteSpace(Name.Value))
            {
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין שם מלא";
                ErrorCount++;
                return false;
            }
            if (Name.Value != "")
            {
                var fullNames = Name.Value.Split(' ');
                if (fullNames.Length >= 2)
                {
                    foreach (var str in fullNames)
                    {
                        if (str.Length < 2)
                        {
                            ErrorCount++;
                            FormErrorAgent_lable.Visible = true;
                            FormErrorAgent_lable.Text = "יש להזין שם מלא תקין";
                            return false;
                        }
                    }
                }
                else
                {
                    ErrorCount++;
                    FormErrorAgent_lable.Visible = true;
                    FormErrorAgent_lable.Text = "יש להזין שם מלא תקין";
                    return false;
                }
            }
            if (EmailA.Value == "")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין אימייל ";
                return false;
            }
            if (!EmailA.Value.Contains("@"))
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין אימייל תקין";
                return false;
            }
            if (Helpers.AgentEmailExist(EmailA.Value, -1) == "true")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "אימייל זה כבר קיים במערכת";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Address.Value))
            {
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין כתובת";
                ErrorCount++;
                return false;
            }
            if (Phone.Value == "" || Phone.Value.Length < 9)
            {
                ErrorCount++;
                FormErrorAgent_lable.Text = "יש להזין מספר טלפון  ";
                FormErrorAgent_lable.Visible = true;
                return false;
            }


            if (string.IsNullOrWhiteSpace(Tz.Value))
            {
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין ת.ז ";
                ErrorCount++;
                return false;
            }
            if (Tz.Value.Length != 9)
            {
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "  יש להזין ת.ז תקינה ";
                ErrorCount++;
                return false;
            }
            if (Helpers.AgentTzExist(Tz.Value, -1) == "true")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "ת.ז קיימת במערכת";
                return false;

            }
            if (PasswordAgent.Value == "")
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין סיסמא ";
                return false;
            }
            if (PasswordAgent.Value.Length < 6)
            {
                ErrorCount++;
                FormErrorAgent_lable.Visible = true;
                FormErrorAgent_lable.Text = "יש להזין סיסמא תקינה ";
                return false;
            }

           /* if (string.IsNullOrWhiteSpace(PercentCommission.Value) || int.Parse(PercentCommission.Value) < 0 || int.Parse(PercentCommission.Value) >= 100)
            {
                FormError_lable.Visible = true;
                FormError_lable.Text = " יש להזין סכום עמלה תקין";
                ErrorCount++;
                return false;
            }*/
            if (ErrorCount == 0)
            {
                string sql = @"  Insert INTO Agent (FullName,Tz,Email,Phone,ImageFile,Password,Level,Show)
                                     values(@Name,@Tz,@Email,@Phone,@ImageFile,@Password,@Level,1)";
                SqlCommand cmd = new SqlCommand(sql);


                cmd.Parameters.AddWithValue("@Name", Name.Value);

                cmd.Parameters.AddWithValue("@Tz", Tz.Value);

                cmd.Parameters.AddWithValue("@Email", EmailA.Value);

                cmd.Parameters.AddWithValue("@Phone", Phone.Value);


/*                cmd.Parameters.AddWithValue("@PercentCommission", PercentCommission.Value);
*/
                cmd.Parameters.AddWithValue("@Password", PasswordAgent.Value);

                //try
                //{
                //    Pageinit.CheckManagerPermissions(this, this.Master);
                //    cmd.Parameters.AddWithValue("@ParentID", long.Parse(HttpContext.Current.Session["AgentID"].ToString()));
                //}
                //catch (Exception ex)
                //{
                //    System.Web.HttpContext.Current.Response.Redirect("SignIn.aspx");
                //}

                try
                {
                    FileUpload fileU = (FileUpload)Session["imgFileUpload1"];
                    string ext = System.IO.Path.GetExtension(fileU.FileName).ToLower();
                    FileName1 = Md5.GetMd5Hash(Md5.CreateMd5Hash(), "1" + Helpers.CreateFileName(fileU.FileName)) + ext;
                    cmd.Parameters.AddWithValue("@ImageFile", FileName1);
                }
                catch (Exception) { cmd.Parameters.AddWithValue("@ImageFile", ""); }

                cmd.Parameters.AddWithValue("@Level", HttpContext.Current.Session["Add_Level"]);

                if (DbProvider.ExecuteCommand(cmd) > 0)
                {
                    if (Session["imgFileUpload1"] != null && ((FileUpload)Session["imgFileUpload1"]).HasFile)
                    {
                        //todo -לשמור בתקיה בשרת
                        string FilePath = String.Format("{0}/Agent/", ConfigurationManager.AppSettings["MapPath1"]);
                        try { ((FileUpload)Session["imgFileUpload1"]).PostedFile.SaveAs(Path.Combine(FilePath, FileName1)); }
                        catch (Exception ex)
                        {

                        }

                     
                    }

                    return true;
                }
                else
                {
                    FormErrorAgent_lable.Text = "* התרחשה שגיאה";
                    FormErrorAgent_lable.Visible = true;
                }

            }


            return false;
        }

        protected void SaveNewAgent_Click(object sender, EventArgs e)
        {
          //  AddAgentPopUp.Visible = false;
            bool success = funcSave(sender, e);
            if (!success)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "setTimeout(HideLoadingDiv, 0);", true);
            }
            else
            {
                Response.Redirect(ListPageUrl);
            }
        }
        protected void ForwardLeadsToAgent_Click(object sender, ImageClickEventArgs e)
        {
            if (AgentList.SelectedIndex == 0)
            {
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש לבחור סוכן";
                return;
            };
            string IdsLeads ="";



            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("chk")).Checked)
                {
                    IdsLeads += ((HiddenField)Repeater1.Items[i].FindControl("LeadID")).Value;
                    IdsLeads += ",";
                }

            }
            
            //for (int i =0; i< Repeater2.Items.Count; i++)
            //{
            //    if (((CheckBox)Repeater2.Items[i].FindControl("ChkBox")).Checked)
            //    {
            //        IdsLeads += ((HiddenField)Repeater2.Items[i].FindControl("LeadID")).Value;
            //        IdsLeads += ",";
            //    }

            //}
            if (string.IsNullOrEmpty(IdsLeads))
            {
                FormError_lable.Visible = true;
                FormError_lable.Text = "יש לסמן ליד";
                return;
            }

            SqlCommand cmd = new SqlCommand("update Lead set AgentID = @AgentID where ID in(" + IdsLeads.Remove(IdsLeads.Length - 1, 1) + ")");
            cmd.Parameters.AddWithValue("@AgentID", AgentList.SelectedValue);
            //cmd.Parameters.AddWithValue("@IdsLeads", IdsLeads.Remove(IdsLeads.Length-1,1));
            if (DbProvider.ExecuteCommand(cmd) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('An error occurred');", true);

            }
            MoveLeadPopUp.Visible = false;
            loadUsers(1,true);


        } 

        protected void StatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["mainStatus"] = MainStatusList.SelectedValue.ToString();

            loadUsers(1,false);
        } 
        
        protected void SubStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["subStatus"] = SubStatusList.SelectedValue.ToString();

            loadUsers(1,false);
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

       
        protected void NewLidBtn_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Response.Redirect("LeadAdd.aspx");
        }        
        protected void MoveTo_Click(object sender, EventArgs e)
        {
            FormError_lable.Visible = false;
            MoveLeadPopUp.Visible = true;
           
            SqlCommand cmdAgents = new SqlCommand("SELECT  FullName as AgentName,ID FROM Agent where Level =3");
            DataSet dsAgents = DbProvider.GetDataSet(cmdAgents);
            AgentList.DataSource = dsAgents;
            AgentList.DataTextField = "AgentName";
            AgentList.DataValueField = "ID";
            AgentList.DataBind();
            AgentList.Items.Insert(0, new ListItem("חפש סוכן", "")); 
            
            SqlCommand cmdLeads = new SqlCommand("Select ID, FirstName + '' + LastName as LeadName, Phone1 From Lead where AgentID is null");
            DataSet dsLeads = DbProvider.GetDataSet(cmdLeads);
            Repeater2.DataSource = dsLeads;
            Repeater2.DataBind();

            UpdatePanel2.Update();

        }    
        protected void CreateEmployee_Click(object sender, EventArgs e)
        {
            AddAgentPopUp.Visible = true;
            UpdatePanel2.Update();
        }
        protected void AddTimeOK_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void CloseMoveLeadPopUp_Click(object sender, ImageClickEventArgs e)
        {
            MoveLeadPopUp.Visible = false;
        }

        protected void BtnDetailsLead_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("LeadEdite.aspx?LeadID=" + e.CommandArgument.ToString());
        }

        protected void AgentsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selectedAgent"] = AgentsList.SelectedValue.ToString();

            loadUsers(1,false);
        }

        protected void SetStatus_Click(object sender, EventArgs e)
        {
            StatusError_label.Visible = false;
            SetStatusPopUp.Visible = true;

            SqlCommand cmdStatus = new SqlCommand("SELECT * FROM FirstStatusLead");
            DataSet dsStatus = DbProvider.GetDataSet(cmdStatus);
            StatusEditList.DataSource = dsStatus;
            StatusEditList.DataTextField = "Status";
            StatusEditList.DataValueField = "ID";
            StatusEditList.DataBind();
            StatusEditList.Items.Insert(0, new ListItem("סטטוס ראשי", ""));

            UpdatePanel2.Update();
        }

        protected void CloseStatusPopUp_Click(object sender, ImageClickEventArgs e)
        {
            SetStatusPopUp.Visible = false;
        }

        protected void btnSetStatusNow_Click(object sender, EventArgs e)
        {
            if (StatusEditList.SelectedIndex == 0)
            {
                StatusError_label.Visible = true;
                StatusError_label.Text = "יש לבחור סטטוס ראשי";
                return;
            };
            string IdsLeads = "";



            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("chk")).Checked)
                {
                    IdsLeads += ((HiddenField)Repeater1.Items[i].FindControl("LeadID")).Value;
                    IdsLeads += ",";
                }

            }

            
            if (string.IsNullOrEmpty(IdsLeads))
            {
                StatusError_label.Visible = true;
                StatusError_label.Text = "יש לסמן ליד";
                return;
            }

            SqlCommand cmd = new SqlCommand("update Lead set FirstStatusLeadID = @FirstStatusLeadID where ID in(" + IdsLeads.Remove(IdsLeads.Length - 1, 1) + ")");
            cmd.Parameters.AddWithValue("@FirstStatusLeadID", StatusEditList.SelectedValue);
            //cmd.Parameters.AddWithValue("@IdsLeads", IdsLeads.Remove(IdsLeads.Length-1,1));
            if (DbProvider.ExecuteCommand(cmd) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('An error occurred');", true);

            }
            SetStatusPopUp.Visible = false;
            loadUsers(1,true);

           
        }
    }
}