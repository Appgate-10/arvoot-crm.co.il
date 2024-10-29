<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="Leads.aspx.cs" Inherits="ControlPanel._leads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% 
        string Title = "Users";
    %>
    <meta name="Description" content='<% = Title%>' />
    <meta name="keywords" content='<% = Title%>' />
    <meta name="abstract" content='<% = Title%>' />
    <meta http-equiv="title" content="<% = Title%>" />
    <title><% = Title %></title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanelButtons" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <asp:Button  ID="NewLidBtn" Class="NewLid" Text="ליד חדש" Style="width: 110px; height: 35px;" runat="server" OnClick="NewLidBtn_Click" />
    <asp:Button  ID="MoveTo" Class="NewLid"  Visible="false" Text="העבר ליד" Style="width: 110px; height: 35px; left:22%" runat="server" OnClick="MoveTo_Click" />
    <asp:Button  ID="SetStatus" Class="NewLid"  Visible="false" Text="ערוך סטטוס" Style="width: 110px; height: 35px; left:30%" runat="server" OnClick="SetStatus_Click" />
    <asp:Button  ID="CreateEmployee" Class="NewLid"  Visible="false" Text="צור עובד/סוכן" Style="width: 110px; height: 35px; left:38%" runat="server" OnClick="CreateEmployee_Click" />

        </ContentTemplate>
    </asp:UpdatePanel>
   
    <div class="ListDivParamsHeadFlex">
        <div style="width: 5%; text-align: right;"></div>
        <div style="width: 7%; text-align: right;">ת.הקמה</div>
        <div style="width: 7%; text-align: right;">שם פרטי</div>
        <div style="width: 7%; text-align: right;">שם משפחה</div>
        <div style="width: 7%; text-align: right;">ת.ז.</div>
        <div style="width: 8%; text-align: right;">טלפון</div>
        <div style="width: 12%; text-align: right;">          
             <asp:DropDownList style="width:60%" runat="server" ID="MainStatusList" OnSelectedIndexChanged="StatusList_SelectedIndexChanged" CssClass="StatusClaims" ToolTip="סטטוס ראשי" AutoPostBack="true"></asp:DropDownList>

        </div>
        <div style="width: 15%; text-align: right;">
             <asp:DropDownList style="width:60%" runat="server" ID="SubStatusList" OnSelectedIndexChanged="SubStatusList_SelectedIndexChanged" CssClass="StatusClaims" ToolTip="סטטוס משני" AutoPostBack="true"></asp:DropDownList>

           </div>
        <div style="width: 9%; text-align: right;">
            <asp:DropDownList  style="width:60%" runat="server" ID="AgentsList" OnSelectedIndexChanged="AgentsList_SelectedIndexChanged" CssClass="StatusClaims" ToolTip="סוכן" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div style="width: 8%; text-align: right;">זמן מעקב</div>
        <div class="row" style="width: 14%; text-align: right; align-items: center; justify-content: space-between;">
            <label style="margin-left: 2%;">הערות</label>
            <div class="row">
<%--                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/Upload_Button.png" OnClick="CopyLid_Click" />--%>
                <asp:ImageButton ID="ImageButton2" Style="margin-right: 10px;" runat="server" ImageUrl="~/images/icons/Print_Button.png" OnClick="CopyLid_Click" />
            </div>
        </div>
        <div style="width: 1%; text-align: center;">
<%--            <asp:ImageButton ID="CopyLid"  runat="server" ImageUrl="~/images/icons/Help_Button_2.png" OnClick="CopyLid_Click" />--%>
        </div>

    </div>


    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div runat="server" id="div2">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                      <%-- <asp:LinkButton ID="ButtonDiv" runat="server"  CommandArgument='<%#Eval("ID") %>' OnCommand="BtnDetailsLead_Command" CssClass="ButtonDiv" >--%>

                            <div class="ListDivParamsBig" style="position:relative;" >
                                <div style="width: 5%; text-align: center">
                                    <asp:Image ID="BtnDetailsLead"   style="vertical-align: middle;" runat="server" ImageUrl="~/images/icons/Arrow_Left_1.png"  />
                                </div>

                                <asp:HiddenField ID="LeadID" Value='<%#Eval("ID") %>' runat="server"/>
                                <div style="width: 10%; text-align: right"><%#Eval("Note") %></div>
                                <div style="width: 8%; text-align: right;"><%#Eval("TrackingTime") %></div>
                                <div id="AgentName" runat="server" style="width: 9%; text-align: right;"><%#Eval("AgentName") %></div>
                                <div style="width: 15%; text-align: right;"><%--<%#Eval("SecondStatus") %>--%>
                                      <asp:TextBox  ID="SubStatus"   Text='<%#Eval("SecondStatus ") %>'  style="vertical-align: middle;width: 90%;height: 40%;font-weight: bold;color: #35508C;background-repeat: no-repeat;
                                                    background-size: 100% 100%;text-align: center;border: none; background-image: url(../images/icons/Secondary_Status_6.png);" runat="server"   />

                                </div>
                                <div style="width: 12%; text-align: right;"><%--<%#Eval("FirstStatus ") %>--%>
                                       <asp:Image  ID="MainStatus" style="vertical-align: middle;width:auto;height:40%;" runat="server" ImageUrl="~/images/icons/Status_1.png"  />

                                </div>
                                <div style="width: 8%; text-align: right"><%#Eval("Phone1") %></div>
                                <div style="width: 7%; text-align: right"><%#Eval("Tz") %></div>
                                <div id="LeadFirstName" runat="server" style="width: 7%; text-align: right"><%#Eval("LastName") %></div>
                                <div id="LeadLastName" runat="server" style="width: 7%; text-align: right"><%#Eval("FirstName") %></div>
                                <div style="width: 7%; text-align: right"><%#Eval("CreateDate") %></div>
                                <div style="width: 5%; text-align: center">
                                    <asp:CheckBox ID="chk" runat="server" ClientIDMode="Static" />
                                </div>
                                <asp:Button ID="ButtonDiv" runat="server"  CommandArgument='<%#Eval("ID") %>' OnCommand="BtnDetailsLead_Command" CssClass="repeaterButton" Style="width:95%;"/>
                            </div>
                       <%--</asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:Repeater>
                <div id="PageingDiv" class="PageingDiv" runat="server"></div>
            </div>

           
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <div id="MoveLeadPopUp" class="popUpOut MainDivDocuments " visible="false" runat="server">
                <div id="Div3" class="popUpIn" style="width: 57%; height: 600px; margin-top: 160px; margin-bottom: 160px; direction: rtl; text-align: center; border-width: 2px;" runat="server">
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="CloseMoveLeadPopUp" OnClick="CloseMoveLeadPopUp_Click" />
                    <div class="col MainDivPopup " style="width: 43%; margin-bottom: 30px;">
                        <label class="HeaderPopup">העבר לידים לסוכן חדש</label>
                        <label class="SecondaryHeaderPopup">בחר סוכן מתוך רשימת הסוכנים במערכת וסמן את הלידים להעביר לסוכן חדש</label>
                    </div>
                    <div class="col" style="padding: 0px 5%; align-items: center;">
                        <div class="row DivAgentVAll">
                            <asp:DropDownList runat="server" ID="AgentList" class="DropDownList FontWeightBold" ToolTip="סוכן"></asp:DropDownList>
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="images/icons/Forward_Leads_To_Agent_Mark_All_Button.png" style="display:none;" OnClientClick="checkAllBoxes(); return false;" />
                        </div>
                        <div class="MainDivDocuments DivRepeaterMoveLead" style="display:none;">
                            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="row RepeaterMoveLead " runat="server">
                                        <div style="width: 7%; text-align: center;">
                                            <asp:CheckBox CssClass="itemCheckbox" runat="server"  ID="ChkBox" ClientIDMode="Static" />
                                        </div>
                                        <div runat="server" style="width: 5%; text-align: center;" class=" ColorLable FontWeightBold">
                                            <asp:HiddenField ID="LeadID" Value='<%#Eval("ID") %>' runat="server"/>
                                        </div>
                                        <div runat="server" style="width: 20%; text-align: right;" class=" ColorLable FontWeightBold">
                                         <%#Eval("LeadName") %> 
                                  
                                        </div>
                                        <div style="width: 20%; text-align: right;" class=" LableBlue">
                                             <%#Eval("Phone1") %> 
<%--                                              <asp:ImageButton ID="UploadFile" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Downlaod_Button.png" />--%>
                                        </div>
                                        <div style="width: 55%; text-align: right;" class=" LableBlue">
                                            35598548
                                        </div>
                                    </div>
                                </ItemTemplate>


                            </asp:Repeater>
                        </div>
                    
                        <asp:ImageButton ID="ImageButton3" runat="server" OnClick="ForwardLeadsToAgent_Click" Style="max-width:95%;" ImageUrl="~/images/icons/Forward_Leads_To_Agent_Forward_Button.png"  />
                         <asp:Label ID="FormError_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>

                        <%-- </div>--%>
                    </div>

                </div>
            </div>

             <div id="SetStatusPopUp" class="popUpOut MainDivDocuments " visible="false" runat="server">
                <div class="popUpIn" style="width: 57%; height: 600px; margin-top: 160px; margin-bottom: 160px; direction: rtl; text-align: center; border-width: 2px;" >
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="CloseStatusPopUp" OnClick="CloseStatusPopUp_Click" />
                    <div class="col MainDivPopup " style="width: 43%; margin-bottom: 30px;">
                        <label class="HeaderPopup">ערוך סטטוס ליד</label>
                        <label class="SecondaryHeaderPopup">בחר סטטוס ראשי:</label>
                    </div>
                    <div class="col" style="padding: 0px 5%; align-items: center;">
                        <div class="row DivAgentVAll">
                            <asp:DropDownList runat="server" ID="StatusEditList" class="DropDownList FontWeightBold" ToolTip="סטטוס"></asp:DropDownList>
                        </div>
                       
                        <asp:Button ID="btnSetStatusNow" runat="server" OnClick="btnSetStatusNow_Click" Text="ערוך סטטוס" CssClass="setStatusBtn"  />
                         <asp:Label ID="StatusError_label" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>

                        <%-- </div>--%>
                    </div>

                </div>
            </div>

    <div id="AddAgentPopUp" class="popUpOut" visible="false" runat="server">
               
            <div id="Div1" class="popUpIn" style="position: relative; min-width: 600px;  width: 60%;  height: 94%; direction: rtl;margin-top:20px" runat="server">
                                  <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="ClosePopUpAddAgent" OnClick="ClosePopUpAddAgent_Click" />


              <div class="content-wrapper-add-agent">
                   <div class="RowGrayPopUpPay"></div>
                   <div class="form-header-agent">צור עובד / סוכן</div>
                   <div style="margin-bottom: 2%; text-align: right;">
                        <span style="color: #932B90; margin-bottom: 1%;">עובד מסוג:
                        </span>
                   </div>
                   
                   <div class="row" style="justify-content: center; width: 100%; margin-bottom: 4%;">
                        <div class="col ColAgentPermissions" id="FullPermissionDiv" runat="server" style="width: 30%;">
                            <asp:Button ID="ManagementPermission"  OnClick="ManagementPermission_Click"  type="button" runat="server" class="Permissions" Text="מנהל" />
<%--                            <span class="SpanPermissions">(עריכת תוכן,הוספת הסרת מבוטחים,עריכת ומחיקה של מידע)</span>--%>
                        </div>
         
                        <div class="col ColAgentPermissions" style="width: 30%;">
                            <asp:Button ID="AgentPermission" type="button" runat="server"  OnClick="AgentPermission_Click"   class="Permissions" Text="סוכן" />
<%--                            <span class="SpanPermissions">(יכול לצפות במידע על המבוטחים שלו בלבד)</span>--%>
                        </div>
                        <div class="col" style="width: 30%;" id="AgentSupervisorDiv" runat="server">
                            <asp:Button ID="SupervisorPermission" type="button" runat="server"  OnClick="SupervisorPermission_Click"   class="Permissions" Text="מפקחת" />
<%--                            <span class="SpanPermissions">(יכול לצפות במידע על המבוטחים שלו בלבד)</span>--%>
                        </div>
                    </div>
                   <div class="RowGrayPopUpPay" style="margin-bottom: 4%;"></div>

                   <div class="row" style="margin: 0 15px; height: 7%;">
                        <div class="col colImgAgentSettings">

                            <img src="images/icons/User_Image_Avatar.png" runat="server" id="ImageFile_1_display" name="ImageFile_1_display" style="width: 71%; cursor: pointer;" class="ImageFile" />
                            <asp:Label ID="ImageFile_1_lable" runat="server" Text="* העלה לוגו של העסק" CssClass="ErrorLable" Style="width: 100%;" Visible="false"></asp:Label>
                            <asp:Label ID="ImageFile_1_lable_2" runat="server" Text="* בבקשה נסה שוב" CssClass="ErrorLable" Visible="false" Style="margin-top: unset; width: 100%;"></asp:Label>

                        </div>

                   </div>                         
                   <div class="row" style="align-items: center; justify-content: center; margin-top: 6%; height: 31%; margin-bottom: 2%;">
                    
                        <div class="col form-group-wrapper" style="margin-left: 15px; height: 100%;">
                                    <div class="col form-input-wrapper">
                                        <span class="form-span-wrapper">שם מלא</span>
                                        <input type="text" runat="server" name="Name" style="margin-bottom: 0px;" id="Name" placeholder="שם מלא" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="ErrorLable" Style="width: 100%" runat="server" ErrorMessage="יש להזין שם מלא תקין"
                                            ControlToValidate="Name"
                                            ValidationExpression="[A-Zא-תa-z' ]*">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col form-input-wrapper">
                                        <span class="form-span-wrapper">אימייל</span>
                                        <input type="text" runat="server" name="EmailA" id="EmailA" placeholder="אימייל" />
                                    </div>
                                    <div class="col form-input-wrapper">
                                        <span class="form-span-wrapper">שם סניף</span>
                                        <input type="text" runat="server" name="Address" id="Address" placeholder="כתובת" />
                                    </div>

                                </div>
                        <div class="col form-group-wrapper" style="margin-right: 15px; height: 100%;">
                                    <div class="col form-input-wrapper">
                                        <span class="form-span-wrapper">טלפון</span>
                                        <input type="text" runat="server" name="Phone" style="margin-bottom: 0px;" id="Phone" placeholder="טלפון" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="ErrorLable" Style="width: 100%" runat="server" ErrorMessage="יש להזין טלפון תקין"
                                            ControlToValidate="Phone"
                                            ValidationExpression="^([0-9\(\)\/\+ \-\.]*)$"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col form-input-wrapper">
                                        <span class="form-span-wrapper">ת.ז</span>
                                        <input type="text" runat="server" name="Tz" id="Tz" placeholder="ת.ז" />
                                    </div>
                                    <div class="col form-input-wrapper">
                                        <span class="form-span-wrapper">סיסמא</span>
                                        <input runat="server" type="text" name="PasswordAgent" id="PasswordAgent" placeholder="סיסמא" />
                                    </div>


                                </div>

                   </div>
         <%--          <div class="col form-group-wrapper" style="width: 100%; height: 10.5%;">
                        <div class="col form-input-wrapper">
                            <span class="form-span-wrapper">בחר עמלה באחוזים עבור מבוטחים חדשים של סוכן זה</span>
                            <input type="number" runat="server" name="PercentCommission" id="PercentCommission" placeholder="הזן סכום עמלה..." />
                        </div>
                   </div>--%>
                   <div class="RowGrayPopUpPay" style="margin-bottom: 6%;"></div>
                   <div class="col">
                        <asp:Label ID="Label1" runat="server" Text="" CssClass="ErrorLable2" Visible="false"></asp:Label>

                        <asp:Button ID="AddNewAgent" Name="AddNewAgent" runat="server" OnClick="SaveNewAgent_Click" Text="צרף עובד חברה/סוכן חדש" class="AddAgentButton" />
                          <asp:Label ID="FormErrorAgent_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
                   </div>


                </div>

         </div>


     </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   

   <input type="text" runat="server" name="ImageFile_1" id="ImageFile_1" style="display: none" />
    <asp:FileUpload ID="ImageFile_1_FileUpload" runat="server" onchange="ImageFile_UploadFile(this)" Style="display: none" />
    <asp:Button ID="ImageFile_1_btnUpload" Text="1" runat="server" OnClick="ImageFile_1_btnUpload_Click" Style="display: none" />
    <script type="text/javascript">
        function ImageFile_UploadFile(fileUpload) {

            var x = fileUpload.id;
            x = x.replace("FileUpload", "btnUpload")

            if (fileUpload.value != '') {

                document.getElementById(x).click();

            }
        }
        function checkAllBoxes() {
            var checkboxes = document.querySelectorAll('span.itemCheckbox input[type="checkbox"]');
           // var checkboxes = document.getElementsByClassName('itemCheckbox');
            //var checkboxes = document.querySelectorAll('input[type="checkbox"][id*="chkItem"]');
            for (var i = 0; i < checkboxes.length; i++) {
            
                checkboxes[i].checked = true;
            }

        }

       
    </script>

</asp:Content>

