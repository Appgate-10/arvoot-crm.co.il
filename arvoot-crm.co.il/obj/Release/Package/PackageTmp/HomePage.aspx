﻿<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ControlPanel._homePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% 
        string Title = "Customers";
    %>
    <meta name="Description" content='<% = Title%>' />
    <meta name="keywords" content='<% = Title%>' />
    <meta name="abstract" content='<% = Title%>' />
    <meta http-equiv="title" content="<% = Title%>" />
    <title><% = Title %></title>

    <style>
        .progress-ring {
            position: relative;
            width: 120px;
            height: 120px;
            margin: auto;
        }

        .svg-circle {
            width: 120px;
            height: 120px;
        }

        .progress-ring__circle {
            stroke: #F1F1F1;
            fill: transparent;
            stroke-width: 8;
        }

        .progress-ring__path {
            fill: transparent;
            stroke-width: 8;
            stroke-linecap: round;
            transition: stroke-dashoffset 0.35s;
            transform: rotate(-90deg);
            transform-origin: 50% 50%;
        }

        .progress-ring__text {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            font-size: 24px;
            font-weight: bold;
        }

        .progress-ring__background {
            fill: #F1F1F1;
        }

        .text-yellow {
            color: #F5C065;
            font-weight: bold;
        }

        .text-green {
            color: #10B049;
            font-weight: bold;
        }

        .text-purple {
            color: #800080;
            font-weight: bold;
        }

        .text-blue {
            color: #0098FF;
            font-weight: bold;
        }

        .text-red {
            color: #F44336;
            font-weight: bold;
        }

        .text-gray {
            padding: 7px;
            color: #8990A4;
            font-weight: 500;
            font-size: 14px;
        }

        
/*@media screen and (max-width: 1300px) {
    .progress-ring {
            position: relative;
            width: 100px;
            height: 100px;
            margin: auto;
        }

        .svg-circle {
            width: 100px;
            height: 100px;
        }
}*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel_Button" runat="server">
        <ContentTemplate>
             <asp:Button  ID="CreateEmployee" Class="NewLid"  Visible="false" Text="צור עובד/סוכן" Style="width: 133px; height: 41px; left: 15%;" runat="server" OnClick="CreateEmployee_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>

               
            <div class="rowHome">
                <div class="colHome" style="width: 60%;">
                    <%--                 <asp:Label ID="Label5" Text="נתון כללי" class="text-blue" style="font-size:11pt"  runat="server" />--%>
                    <div class="rowHome" style="padding: 25px 25px 0px 25px; height: auto;">
                        <asp:Button ID="Label6" OnClick="Month_General_Click" Text="נתון כללי" class="text-blue" Style="font-size: 11pt; color: #05025f; margin-right: 3%; background: none; border: none; font-family: 'Open Sans Hebrew', sans-serif;" runat="server" />
                    </div>
                    <div class="rowHome" style="height: 80%;">

                        <div style="width: 5%"></div>
                        <div style="width: 18%; margin: auto">
                            <div class="progress-ring">
                                <svg class="svg-circle">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath" class="progress-ring__path" style="stroke: #F44336;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text">
                                    <asp:Label ID="PercentageText" class="text-red" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label1" Text="חוסרים" class="text-red" Style="font-size: 11pt" runat="server" />

                        </div>
                        <div style="width: 18%; margin: auto">
                            <div class="progress-ring">
                                <svg class="svg-circle">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath1" class="progress-ring__path" style="stroke: #F5C065;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text">
                                    <asp:Label ID="PercentageText1" class="text-yellow" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label2" Text="בטיפול" class="text-yellow" Style="font-size: 11pt" runat="server" />

                        </div>
                        <%--<div style="width: 15%; margin: auto">
                            <div class="progress-ring">
                                <svg class="svg-circle">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath2" class="progress-ring__path" style="stroke: #800080;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text">
                                    <asp:Label ID="PercentageText2" class="text-purple" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label3" Text="בוטל" class="text-purple" Style="font-size: 11pt" runat="server" />

                        </div>--%>
                        <div style="width: 18%; margin: auto">
                            <div class="progress-ring">
                                <svg class="svg-circle">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath3" class="progress-ring__path" style="stroke: #0098FF;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text">
                                    <asp:Label ID="PercentageText3" class="text-blue" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label4" Text="הושלם" class="text-blue" Style="font-size: 11pt" runat="server" />

                        </div>

                        <div style="width: 18%; margin: auto">
                            <div class="progress-ring">
                                <svg class="svg-circle">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath4" class="progress-ring__path" style="stroke: #10B049;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text" style="font-size:18px;">
                                    <asp:Label ID="PercentageText4" class="text-green" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label11" Text="שולם" class="text-green" Style="font-size: 11pt" runat="server" />

                        </div>

                        <div style="width: 18%; margin: auto">
                            <div class="progress-ring">
                                <svg class="svg-circle">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath5" class="progress-ring__path" style="stroke: #800080;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text" style="font-size:18px;">
                                    <asp:Label ID="PercentageText5" class="text-purple" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label13" Text="יתרת תשלום" class="text-purple" Style="font-size: 11pt" runat="server" />

                        </div>
                        <div style="width: 5%"></div>

                    </div>
                </div>
                <div class="colHome" style="width: 40%">
                    <div class="rowHome" style="padding: 25px 25px 0px 25px; height: auto;">
                        <img src="images/icons/Last_Message_Icon.png" runat="server" style="width: 17px; height: 18px;" />

                        <asp:Label ID="Label5" Text="התראות אחרונות" class="text-blue" Style="font-size: 11pt; padding-right: 10px" runat="server" />
                    </div>
                    <div style="padding-bottom: 12%; padding-right: 7%; margin: 10px; max-height: 200px; overflow-y: auto;">
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <div style="background-color: whitesmoke; border-radius: 12px; margin: 10px 0px 10px 10px; padding-right: 16px; padding-bottom: 8px;">
                                    <div class="LblDate FontWeightBold" style="margin-top: 10px; padding-top: 6px;"><%#Eval("CreationDate") %></div>
                                    <%# Eval("Text") %>
                                    <div style="text-align: left; padding-left: 10px; text-align: right; margin-top: 6px; font-size: 12px">
                                       
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

               
            </ContentTemplate>
    </asp:UpdatePanel>
    <%-- Gila --%>
        <asp:UpdatePanel ID="AddForm2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="rowHome with-last-child">
                <div class="colHome" style="width: 50%; height: 400px;">
                    <%-- Gila --%>
                    <div class="rowHome tasks-div">
                        <asp:Label ID="Label7" Text="משימות פתוחות" class="text-blue" Style="font-size: 11pt; padding-right: 10px" runat="server" />
                        <asp:Button ID="BtnFutureTasks" CssClass="btnBlue" runat="server" Text="משימות עתידיות" OnClick="BtnFutureTasks_Click"/>
                    </div>
                    <div style="height: 75%; overflow-y: auto; margin: 10px 10px 10px 30px;">
                        <asp:Repeater ID="Repeater3"  runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                            <ItemTemplate>

                                <div  style="display: flex; border-radius: 12px; margin-left: 10px; padding-right: 16px; padding-bottom: 8px;">
                                    <img src="images/icons/Open_Mession_Blue_Point_1.png" runat="server" style="width: 39px; height: 40px;" />

                                    <div style="text-align: left; display: flex; width: 100%; background-color: whitesmoke; padding-left: 10px; text-align: right; margin-top: 6px; font-size: 12px">
                                      <a ID="ButtonDiv"  style="width:100%" 
                                           href='<%# GetRedirectUrl(Eval("LeadID"), Eval("OfferID")) %>' class="ButtonDiv">
                                           <div style="display: flex; border-radius: 12px; margin-left: 10px; padding-right: 16px; padding-bottom: 8px;">
                                                <div id="Label7" class="text-gray" style="width: 10%"><%#Eval("dateTask") %> </div>
                                                <div id="Label8" class="text-gray" style="width: 3%">| </div>
                                                <div id="Label9" class="text-gray" style="width: 10%"><%#Eval("timeTask") %> </div>
                                                <div id="Label6" class="text-gray" style="width: 52%"><%#Eval("Text") %> </div>
                                                <div style="width: 5%" class="text-gray">
                                                    <img src="images/icons/Open_Mession_Waiting_Flug.png" runat="server" style="width: 18px; height: auto;" /></div>
                                                <div id="Status" class="text-gray" style="width: 15%"><%#Eval("Status") %></div>
                                            </div>
                                        </a>

                                        <div class="text-gray" style="width: 5%">
                                             <asp:ImageButton OnCommand="Mession_Delete" OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק את המשימה?');"  CommandArgument='<%#Eval("ID") %>'  ImageUrl="images/icons/Open_Mession_Delete_Button.png" runat="server" style="width: 18px; height: auto;" />

                                        </div>
                                        <div class="text-gray" style="width: 5%">
<%--                                            <img src="images/icons/Open_Mession_Edit_Button.png" runat="server" style="width: 18px; height: auto;" />--%>
                                            <asp:ImageButton OnCommand="Mession_Edit"  CommandArgument='<%#Eval("ID") %>' runat="server" style="width: 18px; height: auto;" ImageUrl="images/icons/Open_Mession_Edit_Button.png"  />
                                        </div>


                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </div>
                <div class="colHome" style="width: 50%; height: 400px;">
                    <%-- Gila --%>
                    <asp:Calendar ID="TasksCalendar" CssClass="tasks-calendar" runat="server" SelectedDate="<%# DateTime.Today %>"
                        OnDayRender="TasksCalendar_DayRender"
                        OnSelectionChanged="TasksCalendar_SelectionChanged"
                        OnVisibleMonthChanged="TasksCalendar_VisibleMonthChanged" DayNameFormat="Shortest"
                        Width="86%" Height="94%" Style="margin-right: auto; margin-left: auto; margin-top: 2%; border: none; border-collapse: separate; border-spacing: 6px;">
                        <TitleStyle BackColor="Transparent" Height="40" ForeColor="#0098ff" Font-Bold="true" HorizontalAlign="Center" CssClass="tasks-calendar-title" />
                        <DayHeaderStyle Height="20px" />
                        <DayStyle CssClass="calendar-day" BorderColor="#DEDFE0" BorderStyle="Solid" BorderWidth="1px" />
                        <SelectedDayStyle BackColor="Transparent" BorderColor="#669EFF" ForeColor="Black" Font-Bold="true"></SelectedDayStyle>
                    </asp:Calendar>




                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanelPopUps" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
                            <asp:Button ID="SupervisorPermission" type="button" runat="server"  OnClick="SupervisorPermission_Click"   class="Permissions" Text="מתפעלת" />
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
                                        <span class="form-span-wrapper">סיסמא</span>
                                        <input runat="server" type="text" name="PasswordAgent" id="PasswordAgent" placeholder="סיסמא" autocomplete="off" />
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
                                        <span id="NameOrAddress" runat="server" class="form-span-wrapper">שם סניף</span>
                                        <input type="text" runat="server" name="Address" id="Address" placeholder="כתובת" />
                                    </div>
                       

                                </div>

                   </div>

                   <div visible="false" id="numbersAgentTitle" runat="server" style="margin-bottom: 2%; text-align: right;">
                        <span style="color: #932B90; margin-bottom: 1%;">מספרי סוכנים:
                        </span>
                   </div>
                   <div style="height:29%" id="numbersAgent" visible="false" runat="server" >
                       <div     class="row" style="justify-content: center; width: 100%; height:33%; ">

                            <div class="col  form-group-wrapper  form-input-wrapper" id="Div2" runat="server" style="width: 30%; margin-left:32px">
                                <span id="company1" runat="server" class="form-span-wrapper">מגדל</span>
                                <asp:HiddenField  ID="CompanyID1" runat="server"/>
                                <input type="text" runat="server" name="EmailA" id="AgentNumber1" style="height:100%" placeholder="מספר סוכן" />                        
                            </div>
         
                            <div class="col  form-group-wrapper form-input-wrapper" style="width: 30%;">
                                 <span id="company2" runat="server" class="form-span-wrapper">כלל</span>
                                 <asp:HiddenField  ID="CompanyID2" runat="server"/>
                                <input type="text" runat="server" name="EmailA" id="AgentNumber2" style="height:100%" placeholder="מספר סוכן" />                        
                            </div>

                            <div class="col  form-group-wrapper  form-input-wrapper" style="width: 30%; margin-right:32px" runat="server">
                                <span  id="company3" runat="server" class="form-span-wrapper">איילון</span>
                                <asp:HiddenField  ID="CompanyID3" runat="server"/>
                                <input type="text" runat="server" name="EmailA" id="AgentNumber3" style="height:100%" placeholder="מספר סוכן" />                        
                            </div>
                        </div> 
                       <div  class="row" style="justify-content: center; width: 100%; height:33%; ">

                            <div class="col  form-group-wrapper  form-input-wrapper" runat="server" style="width: 30%; margin-left:32px">
                                <span id="company4" runat="server" class="form-span-wrapper">מגדל</span>
                                <asp:HiddenField  ID="CompanyID4" runat="server"/>
                                <input type="text" runat="server" name="EmailA" id="AgentNumber4" style="height:100%" placeholder="מספר סוכן" />                        
                            </div>
         
                            <div class="col  form-group-wrapper form-input-wrapper" style="width: 30%;">
                                 <span id="company5" runat="server" class="form-span-wrapper">כלל</span>
                                 <asp:HiddenField  ID="CompanyID5" runat="server"/>
                                <input type="text" runat="server" name="EmailA" id="AgentNumber5" style="height:100%" placeholder="מספר סוכן" />                        
                            </div>

                            <div class="col  form-group-wrapper  form-input-wrapper" style="width: 30%; margin-right:32px" id="Div3" runat="server">
                                <span  id="company6" runat="server" class="form-span-wrapper">איילון</span>
                                <asp:HiddenField  ID="CompanyID6" runat="server"/>
                                <input type="text" runat="server" name="EmailA" id="AgentNumber6" style="height:100%" placeholder="מספר סוכן" />                        
                            </div>
                        </div>

                         <div  class="row" style="justify-content: right; width: 100%; height:33%; margin-right: 11px; ">

                            <div class="col  form-group-wrapper  form-input-wrapper" runat="server" style="width: 30%; margin-left:32px">
                                <span id="company7" runat="server" class="form-span-wrapper">מגדל</span>
                                <asp:HiddenField  ID="CompanyID7" runat="server"/>
                                <input type="text" runat="server" name="EmailA" id="Text1" style="height:100%" placeholder="מספר סוכן" />                        
                            </div>
         
                        </div>
                   </div>
                   <div class="RowGrayPopUpPay" style="margin-bottom: 6%;"></div>
                   <div class="col">
                        <asp:Label ID="Label3" runat="server" Text="" CssClass="ErrorLable2" Visible="false"></asp:Label>

                        <asp:Button ID="AddNewAgent" Name="AddNewAgent" style="margin-bottom:40px" runat="server" OnClick="SaveNewAgent_Click" Text="צרף עובד חברה/סוכן חדש" class="AddAgentButton" OnClientClick="reload(LoadingDiv)" />
                          <asp:Label ID="FormErrorAgent_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
                   </div>
         


                </div>
                     
         </div>


     </div>
              <div id="TaskDiv" class="popUpOut MainDivDocuments" visible="false" runat="server">
                <div id="TaskDiv2" class="popUpIn" style="width: 35%; height: 592px; margin-top: 147px; margin-bottom: 100px; direction: rtl; text-align: center; border-width: 2px;" runat="server">
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="ImageButton5" OnClick="CloseTaskPopUp_Click" />
                    <div class="col MainDivPopup2" style="padding-top: 45px;">
                        <label class="HeaderPopupPurple">עידכון משימה</label>
                    <div class="row" style="align-items: center; justify-content: center; margin-top: 2%; height: 31%; margin-bottom: 2%;width: 100%;">
                    
                        <div class="col form-group-wrapper" style="margin-left: 15px; height: 100%;width:100%">
                                    <div class="col form-input-wrapper">
                                        <span class="form-span-wrapper-right">תוכן</span>
                                        <textarea type="text" runat="server" name="Name" style="margin: 0px 5%; text-align: right;padding-right: 10px;height: 147px; border: solid 1px #a3a3a4;
                                                    border-radius: 7px;width: 599px; resize:none" id="TextTask" placeholder="תוכן" />
                                       
                                    </div>
                            <div style="display:flex; padding: 20px 0px;">
                                    <div class="col form-input-wrapper" style="width:50%;margin-right: 2.5%;">
                                        <span class="form-span-wrapper-right">תאריך</span>
                                        <input type="date" runat="server" name="Name" style="margin: 0px 5%;border-radius: 7px; width: 90%;height: 37px;" id="Date" placeholder="תאריך" />
                                       
                                    </div>
                              <div class="col form-input-wrapper" style="width:50%; margin-left: 1.5%;">
                                        <span class="form-span-wrapper-right">סטטוס</span>
                                         <select id="SelectStatusTask" runat="server" class="selectGlobal" style="width: 90%; border: solid 1px #a3a3a4; border-radius: 7px;
                                                 height: 37px; margin: 0px 5%; color: #767676; font-size: 12pt; padding-right: 20px;"></select>
                                         
                                    </div>
                                </div>
                                </div>
                 

                   </div>
    
                   <div class="RowGrayPopUpPay" style="margin-bottom: 6%;"></div>
                   <div class="col" style="width:220px">
                        <asp:Label ID="Label10" runat="server" Text="" CssClass="ErrorLable2" Visible="false"></asp:Label>
                       <asp:HiddenField runat="server" ID="ID" />
                        <asp:Button ID="AddNewTask" Name="AddNewTask" OnClick="OpenNewTask_Click" runat="server"  Text="שמור" class="AddAgentButton" />
                          <asp:Label ID="FormErrorTask_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
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

        function redirectBasedOnLead(leadID, offerID) {
            if (leadID == 0) {
                window.open("OfferEdit.aspx?OfferID=" + offerID);
            } else {
                window.open("LeadEdit.aspx?LeadID=" + leadID);
            }
        }
    </script>
</asp:Content>


