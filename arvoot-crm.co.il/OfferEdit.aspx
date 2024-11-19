<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="OfferEdit.aspx.cs" Inherits="ControlPanel._offerEdit" %>

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
       
        .lblAns {
            float: right;
            text-align: right;
            /*   color: #636e88;
            font-weight: 700;*/
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="DivLidTop">
      
                <asp:Button runat="server" ID="btnMoveToOperatingQueqe" Text="העבר לתור תפעול" OnClick="btnMoveToOperatingQueqe_Click" CssClass="BtnMove" OnClientClick="reload(LoadingDiv);" />
                <asp:Button runat="server" ID="btnMoveToOperator" Text="העבר למתפעלת" OnClick="btnMoveToOperator_Click" CssClass="BtnMove" OnClientClick="reload(LoadingDiv);" />
                <asp:ImageButton ID="DeleteLid" runat="server" OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק את ההצעה');" ImageUrl="~/images/icons/Delete_Lid_Button.png" OnClick="DeleteOffer_Click" />
                <asp:Button runat="server" ID="btn_save" Text="שמור" OnClick="btn_save_Click" CssClass="BtnSave" Style="width: 110px; height: 35px;" OnClientClick="reload(LoadingDiv);" />
            </div>
            <div>
                <asp:Label ID="FormError_label" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
            </div>
            <div class="NewOfferDiv">
                 <input placeholder="עריכת הצעה" id="NameOffer" style="font-size: 20pt;" runat="server" class="NewOfferLable"/> 

            </div>
            <asp:HiddenField ID="CurrentStatusOfferID" runat="server"/>
            <div class="row MarginDiv">
                <div class="col DivDetails" style="margin-inline-end: 3%;">
                    <div class="row">
                        <div style="width: 20%; text-align: center; padding-top: 44px;">
                            <img src="images/icons/Agent_Avatar_Icon.png" runat="server" />
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">סוכנות</label>
                            <label class="PaddingAgentCus ColorLable"></label>
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">בעלים</label>
                            <label id="lblOwner" runat="server" class="PaddingAgentCus ColorLable"></label>
                        </div>
                        <div class="col" style="width: 30%;">
                            <label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus ColorLable"></label>
                        </div>

                    </div>
                    <div class="DivGray " style="height: 60px; line-height: 60px; padding-right: 20%;">
                            <label class="LableDetails">נוצר על ידי:</label>
                        <label id="AgentName" runat="server" class=" ColorLable"></label>
                        
                        <label id="movedToOperating" runat="server" class="LableBlue" style="margin-right:10px;">הועבר לתור תפעול</label>

                    </div>
                </div>
                <div class="col DivDetails">
                    <div class="row" style="justify-content: space-between; align-items: center; padding: 0px 2%;">
                         <asp:Button ID="Button2" runat="server" class="buttonWithOneImages listOpenTasks buttonWithImages LableBlue" OnClick="PopUpTasksList_Click" Text="לרשימת המשימות הפתוחות" />

                        <asp:ImageButton ID="ImageButton2" Style="margin: 17px 0px 17px 7px; width: 24%;" runat="server" ImageUrl="~/images/icons/Open_Mession_Button.png" OnClick="OpenTask_Click" />

                    </div>
                    <div class="DivGray" style="height: 60px;"></div>
                </div>
            </div>

            <div class="col MarginDiv SecondaryDiv ">
                <div class="row MarginRow ServiceRequestDiv">
                    <div class="row" style="align-items: center;">
                        <div class="div-arrows-img">
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">פירוט הצעה</label>
                        </div>
                        <div>
                            <asp:HiddenField  id="ContactID" runat="server"></asp:HiddenField>
                        </div>
                    </div>
                    <div style="min-width:180px;">
                        <select runat="server" id="SelectStatusOffer" class="selectGlobal"></select>

                        <%--                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/In_Treatment_Status_Button.png" OnClick="CopyLid_Click" />--%>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">מבוטח ראשי:</lable>
                        <lable class="ColorLable" id="FullName" runat="server"></lable>
                        
                 
                    </div>
                    <%--<div style="width: 20%; margin-left: 2%;" class="row">
                        <label class="InputLable">בעלים:</label>
                        <lable class="ColorLable" id="FullNameAgent" runat="server"></lable>
                    </div>--%>
                   <%-- <div style="width: 23%;" class="row">--%>
                         <%-- <asp:CheckBox runat="server" ID="IsPromoted" />
                        <asp:Label ID="lblIsPromoted" AssociatedControlID="IsPromoted" runat="server" CssClass="lblAns ColorLable" Text=" העבר לתור תפעול"></asp:Label>--%>
                       <%-- <label class="InputLable">תור:</label>
                        <select id="SelectTurnOffer" runat="server" class="selectGlobal"></select>--%>
                    <%--</div>--%>
                     <div style="width: 20%; margin-left: 2%;" class="row">
                        <label class="InputLable">סוג הצעה:</label>
                        <select runat="server" id="SelectOfferType" class="selectGlobal"></select>
                    </div>
                </div>
                <div class="row GrayLine" style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">ת.ז. של מבוטח ראשי:</lable>
                        <lable class="ColorLable" id="Tz" runat="server"></lable>

                    </div>
                   
                </div>
                <div  class="row MarginRow "  style="width: 100%;">
                    <div  style="width: 80%;">
                                       <div class="row MarginRow PaddingRow" style="width: 100%;">
                    <div style="width: 26%; margin-left: 24%" class="row">
                        <lable class="InputLable">מועד כניסה לתוקף:</lable>
                        <lable class="ColorLable" id="EffectiveDate" runat="server"></lable>
                    </div>
                    <div style="width: 47%; margin-left: 2%;" class="row">
                        <label class="InputLable">סיבה לחוסר הצלחה:</label>
                        <input id="ReasonLackSuccess" name="ReasonLackSuccess" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 18%;" class="row">
                        <label style="width:100%;text-align: left;" class="InputLable">הערות:</label>
<%--                        <textarea id="Note" name="FirstName" type="text" runat="server" style="width: 100%;" class="InputAdd" />--%>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 21%; margin-left: 24%" class="row">
                        <%--       todo-לחשב כמה ימים עברו מפתיחת ההצעה
                        מועד כניסה לתוקף-לחשב כמה ימים עברו--%>
                        <lable class="InputLable">sla מפתיחת ההצעה:</lable>
                        <lable id="sla" runat="server" class="ColorLable">0</lable>
                    </div>
                    <div style="width: 47%; margin-left: 2%;" class="row">
                        <label class="InputLable">מועד חזרה ללקוח :</label>
                        <input id="ReturnDateToCustomer" name="FirstName" type="date" runat="server" style="width: 20%;" class="InputAdd" />
                    </div>
                </div>
                <%-- <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">ימים לפני/אחרי תאריך יעד של sla:</lable>
                        <lable class="ColorLable">3.6.2023</lable>
                    </div>
                    <div style="width: 20%; margin-left: 2%;" class="row">
                        <label class="InputLable">תאריך יעד sla :</label>
                        <lable class="ColorLable">2.6.23</lable>
                    </div>
                </div>--%>
                <div class="row MarginRow " style="width: 100%;">
                    <%--    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">מספר בקשות שירות גביה:</lable>
                        <lable class="ColorLable">3.6.2023</lable>
                    </div>--%>
                    <div style="width: 31%; margin-left: 14%" class="row">
                        <lable class="InputLable">מקור ההלוואה/ביטוח:</lable>
                        <%--                        <lable class="ColorLable">לאומי</lable>--%>
                        <select runat="server" id="SelectSourceLoanOrInsurance" class="selectGlobal"></select>
                    </div>
                    <div style="width: 20%; margin-left: 2%;" class="row">
                        <label class="InputLable">תאריך שליחה לחברת הביטוח :</label>
                        <input id="DateSentToInsuranceCompany" name="FirstName" type="date" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                </div>
                <%--         <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">מקור ההלוואה:</lable>
                        <lable class="ColorLable">לאומי</lable>
                    </div>
                </div>--%>

                    </div>
                    <div  style="width: 20%;">
                         <textarea id="Note" name="FirstName" type="text" runat="server" style="width: 100%;height: 164px;border: 1px solid rgb(0, 152, 255);
                                    border-radius: 12px;margin-top: 20px;resize: none;overflow-y: scroll;scrollbar-width: none;" class="InputAdd" />

                    </div>
                </div>
 
            </div>

            <div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row" style="align-items: center;">
                        <div class="div-arrows-img">
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">מסמכים </label>
                        </div>
                    </div>
                    <div class="row HeaderBoxD" style="justify-content: flex-end;">
                         <div>
                              <asp:Label ID="ImageFile_lable" runat="server" Text="* Please Upload Image" CssClass="ErrorLable2" Visible="false"></asp:Label>                         
                              <asp:ImageButton Style="margin-left:20px"  ID="UploadDocument" runat="server" ImageUrl="~/images/icons/Uplaod_File_Button.png" OnClick="DeleteLid_Click" />
              
                        </div>
                        <%--<div class="HeaderSearchBox">
                            <input type="text" class="InputTextSearch" value="<% = StrSrc%>" name="Q" id="Q" onblur="javascript:if(this.value==''){this.value='חפש קובץ'};" onfocus="javascript:if(this.value=='חפש קובץ'){this.value='';}" onkeypress="javascript:runSearch(event, 'Enrollment.aspx');" />
                            <a href="javascript:window.location.href = 'Enrollment.aspx?Q=' + document.getElementById('Q').value;">
                                <img src="images/icons/Search_Pdf_Button.png" class="SearchIcon" /></a>
                        </div>--%>
                    </div>
                </div>
                <div class="row PaddingRow MarginRow" style="width: 100%;">
<%--                    <asp:ImageButton ID="ImageButton2" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Mark_For_Archives_Button.png" OnClick="CopyLid_Click" />--%>
                    <asp:ImageButton ID="ImageButton3" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Send_Mail_Button.png" OnClick="SendMail_Click" />
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Send_Sms_Button.png" OnClick="SendSms_Click" />
                </div>

                <div class="MainDivDocuments" style="height: 290px;">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <div class="row SecondaryDivDocuments" runat="server">
                                <div style="width: 2%;" class="RowDocuments">
                                    <asp:CheckBox runat="server" Style="vertical-align:middle" AutoPostBack="true" ID="IsPromoted" />
                                </div>
                                <div style="width: 2%; text-align: left;" class="RowDocuments">
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Pdf_Icon.png" OnClick="DeleteLid_Click" />
                                </div>
                                <div class="row DivBackground DivBackgroundRep">
                                    <div runat="server" style="width: 93%; text-align: right;" class="RowDocuments DivNameFile">
                                        <%--                                    <%#Eval("File") %>--%>
                                   <asp:Label ID="FileName" Text='<%# Eval("FileName") %>' runat="server"/> 
                                    </div>
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="UploadFile" runat="server" OnCommand="UploadFile_Command" CommandArgument='<%# Eval("FileName") + "," + Eval("ID") %>' Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Downlaod_Button.png" />
                                    </div>
                                <%--    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />
                                        <asp:ImageButton ID="ImageButton5" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Shere_Button.png" />
                                    </div>--%>
                                </div>



                            </div>

                            <div style="height: 20px;"></div>
                        </ItemTemplate>


                    </asp:Repeater>
                </div>

             
                   <div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                   <div class="row" style="align-items: center;">
                        <div class="div-arrows-img">
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">בקשות שירות</label>
                        </div>
                    </div>
                    <div class="row HeaderBoxD" style="justify-content: flex-end;">

                        <asp:ImageButton ID="ImageButton1" Style="width:250px;height:auto" runat="server" ImageUrl="~/images/icons/Choosing_Service_New_Service_Button.png" OnClick="ServiceRequestAdd_Click" />

                </div>
                </div>

       <div class="ListDivParamsHead DivParamsHeadMargin">

                    <div style="width: 17%; text-align: right; padding-right: 4%">תאריך</div>
                    <div style="width: 15%; text-align: right;">חשבון</div>
                    <div style="width: 15%; text-align: right;">סכום כולל לגבייה</div>
                    <%--                    הלקוח אמר שאין צורך ב סוג רשומה--%>
                    <%--                    <div style="width: 15%; text-align: right;">סוג רשומה</div>--%>
                    <div style="width: 15%; text-align: right;">יתרת גבייה</div>
                    <div style="width: 33%; text-align: right;">מטרת הגבייה</div>
                    <div style="width: 5%; text-align: center;"></div>

                </div>
                <div runat="server" id="divRepeat" style="height: 463px; overflow-x: auto; margin-bottom: 20px">
                    <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <asp:LinkButton ID="ButtonDiv" runat="server"  CommandArgument='<%#Eval("ID") %>' OnCommand="BtnServiceRequest_Command" CssClass="ButtonDiv" >

                                <div class='ListDivParams' style="position: relative;padding-left: 18px;">
                                    <div style="width: 5%; text-align: center">
                                    <asp:Image  ID="BtnServiceRequest" Style="vertical-align: middle;" ImageUrl="~/images/icons/Arrow_Left_1.png" runat="server" />
                                    </div>
                                      <div style="width: 33%; text-align: right"><%#Eval("PurposeName") %></div>
                                <div style="width: 15%; text-align: right;">
                                    <%#(double.Parse(Eval("Sum").ToString()) - (!string.IsNullOrWhiteSpace(Eval("paid").ToString()) ?double.Parse(Eval("paid").ToString()) : 0) + (Eval("IsApprovedCreditOrDenial").ToString() == "1" && !string.IsNullOrWhiteSpace(Eval("SumCreditOrDenial").ToString()) ? double.Parse(Eval("SumCreditOrDenial").ToString()) : 0)).ToString() %></div>
                                <div style="width: 15%; text-align: right;"><%#Eval("Sum") %></div>
                                <div style="width: 15%; text-align: right"><%#Eval("Invoice") %></div>
                                <div style="width: 17%; text-align: right; padding-right: 4%"><%#Eval("CreateDate") %></div>
                            </div>
                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>



            </div>

            </div>
            <div class="DivLidTop" style="height:36px;">
      
               <asp:Button runat="server" ID="BtnSaveBottom" Text="שמור" OnClick="btn_save_Click" CssClass="BtnSave" OnClientClick="reload(LoadingDiv);" />
            </div>
            <div>
                <asp:Label ID="FormErrorBottom_label" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
            </div>

                        <div id="TaskDiv" class="popUpOut MainDivDocuments" visible="false" runat="server">
                <div id="TaskDiv2" class="popUpIn" style="width: 35%; height: 592px; margin-top: 147px; margin-bottom: 100px; direction: rtl; text-align: center; border-width: 2px;" runat="server">
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="ImageButton5" OnClick="CloseTaskPopUp_Click" />
                    <div class="col MainDivPopup2" style="padding-top: 45px;">
                        <label class="HeaderPopupPurple">משימה חדשה</label>
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
                        <asp:Label ID="Label4" runat="server" Text="" CssClass="ErrorLable2" Visible="false"></asp:Label>

                        <asp:Button ID="AddNewTask" Name="AddNewTask" OnClick="OpenNewTask_Click" runat="server"  Text="פתח" class="AddAgentButton" />
                          <asp:Label ID="FormErrorTask_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
                   </div>
                    </div>



                </div>
            </div>

             <div id="OpenTasksList" class="popUpOut MainDivDocuments" visible="false" runat="server">
                <div class="popUpIn" style="width: 35%; height: 700px; margin-top: 100px; margin-bottom: 100px; direction: rtl; text-align: center; border-width: 2px;" runat="server">  
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="ImageButton6" OnClick="CloseTasksListPopUp_Click" />
                    <div class="DivDownPopUp" >
                        <label style="font-size:15pt" class="LableBlue">רשימת משימות פתוחות</label>
                        <div class="MainDivDocuments DivRepeaterPopUp">
                         <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                             <ItemTemplate>
                                 <div class="row DivRpwRepeaterPopUp">
                                     <div style="width: 50%; text-align: right;" class="ColorLable"><%#Eval("Text") %></div>
                                     <div style="width: 22.5%; text-align: right;" class="ColorLable"><%#Eval("PerformDate") %></div>
                                     <div style="width: 22.5%; text-align: right;" class="ColorLable"><%#Eval("Status") %></div>
                                        <div style="width: 5%; text-align: right;" class="ColorLable">
                                              <asp:ImageButton ID="DeleteTask" runat="server" OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק את המשימה?');" CommandArgument='<%#Eval("ID") %>' OnCommand="DeleteTask_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Open_Mession_Delete_Button.png" />

                                        </div>
                                    
                                 </div>
                                 <div class="RowWhitePopUp"></div>
                             </ItemTemplate>


                         </asp:Repeater>
                     </div>
                    </div>
                 </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="MoveToOperatorPopUp" class="popUpOut MainDivDocuments " visible="false" runat="server">
                <div id="Div3" class="popUpIn" style="width: 32%; height: 600px; margin-top: 160px; margin-bottom: 160px; direction: rtl; text-align: center; border-width: 2px;" runat="server">
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="CloseMovePopUp" OnClick="CloseMovePopUp_Click" />
                    <div class="col MainDivPopup " style="width: 43%; margin-bottom: 30px;">
                        <label class="HeaderPopup" style="font-size:18pt">שיוך הצעה למתעפלת</label>
                    </div>
                    <div class="col" style="padding: 0px 5%; align-items: center;">
                        <div class="row DivAgentVAll">
                            <asp:DropDownList runat="server" ID="OperatorsList" class="DropDownList FontWeightBold" ToolTip="מתפעלות"></asp:DropDownList>
                        </div>
                        <asp:Button ID="MoveToOperator" Name="AddNewTask" OnClick="MoveToOperator_Save" runat="server"  Text="שמור" class="SaveOperetorButton" />

                         <asp:Label ID="Label1" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>

                        <%-- </div>--%>
                    </div>

                </div>
            </div>

         </ContentTemplate>
     </asp:UpdatePanel>
     <input type="text" runat="server" name="ImageFile" id="ImageFile" style="display: none" />
      <asp:FileUpload ID="ImageFile_FileUpload" runat="server" onchange="ImageFile_UploadFile(this)" AllowMultiple="true" Style="display: none" />
      <asp:Button ID="ImageFile_btnUpload" Text="2" runat="server" OnClick="ImageFile_btnUpload_Click" Style="display: none" />


    <br />
    <br />


   
       <script type="text/javascript">

           function ImageFile_UploadFile(fileUpload) {
               
               var x = fileUpload.id;
               x = x.replace("FileUpload", "btnUpload")

               if (fileUpload.value != '') {

                   document.getElementById(x).click();

               }
           }
       </script>
</asp:Content>

