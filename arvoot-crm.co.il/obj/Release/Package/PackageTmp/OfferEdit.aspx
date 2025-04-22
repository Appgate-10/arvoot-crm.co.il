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
        .Btn {
            background-color: #993796;
            color: white;
            padding: 0px 2%;
            border: none;
            border-radius: 5px;
            font-size: medium;
            font-weight: bold;
            margin-inline-start: 5px;
            cursor: pointer;
            font-family: 'Open Sans Hebrew', sans-serif; 
        }
        .InputAdd2 {
            border: none;
            border-bottom: 1px solid black;
            /*   width: 50%;*/
        }
       .PaddingRow2 {
            padding-top: 12px;
        }
        .lblAns {
            float: right;
            text-align: right;
            /*   color: #636e88;
            font-weight: 700;*/
            margin-right: 10px;
               font-weight: 700;
               font-size:14px;
        }
        .InputLable {
            font-weight: 700;
            margin-left: 10px;
            white-space: nowrap;
            font-size:14px;

        }
        .MarginRow2 {
            margin-bottom: 15px;
        }       
        .TextInnerTable{
            width:100%;
            background:none;
            border:none;
            font-size: 14px !important;
            text-align:center
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="DivLidTop">
      <%-- OnClientClick="reload(LoadingDiv);" --%>
                <asp:Button runat="server" ID="btnCreateDoc" Text="צור מסמך" Visible="false" OnClick="btnCreateDoc_Click" CssClass="BtnMove"  />
                <asp:Button runat="server" ID="btnDownloadDoc" Text="הורד מסמך" Visible="false" OnClick="btnDownloadDoc_Click" CssClass="BtnMove"  />
                <asp:Button runat="server" ID="btnMoveToOperatingQueqe" Text="העבר לתור תפעול" OnClick="btnMoveToOperatingQueqe_Click" CssClass="BtnMove" OnClientClick="reload(LoadingDiv);" />
                <asp:Button runat="server"  ID="btnMoveToOperator" Text="העבר למתפעלת" OnClick="btnMoveToOperator_Click" CssClass="BtnMove" OnClientClick="reload(LoadingDiv);" />
                <asp:Button runat="server" Visible="false" ID="btnMoveToAgent" Text="החזר לסוכן" OnClick="btnMoveToAgent_Click" CssClass="BtnMove" OnClientClick="reload(LoadingDiv);" />
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
                            <label  id="lblAgency" runat="server" class="PaddingAgentCus ColorLable"></label>
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">בעלים</label>
                            <div style="position: relative;">
                                <label id="lblOwner" runat="server" class="PaddingAgentCus ColorLable"></label>
                                <asp:Button OnClick="btnMoveToOperator_Click" 
                                            ID="btnMoveToOperator2" 
                                            runat="server" 
                                            OnClientClick="reload(LoadingDiv);"
                                            BackColor="Transparent" 
                                            BorderStyle="None" 
                                            Style="position: absolute; top: 0; left: 0; cursor:pointer; display:none; width:100%" />
                            </div>
                        </div>
                        <div class="col" style="width: 30%;">
                            <%--<label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus ColorLable"></label>--%>
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
                <div class="row MarginRow2 ServiceRequestDiv">
                    <div class="row" style="align-items: center;">
                        <div class="div-arrows-img">
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">פירוט הצעה</label>
                        </div>
                        <div>
                            <asp:HiddenField  id="ContactID" runat="server"></asp:HiddenField>
                            <asp:HiddenField  id="PdfFile" runat="server"></asp:HiddenField>
                        </div>
                    </div>
                    <div style="min-width:180px;"  class="row">
                        <label class="InputLable">סטטוס:</label>

                        <asp:DropDownList  runat="server" AutoPostBack="true" ID="SelectStatusOffer" OnSelectedIndexChanged="SelectStatusOffer_Change"   class="selectGlobal"></asp:DropDownList>

                        <%--                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/In_Treatment_Status_Button.png" OnClick="CopyLid_Click" />--%>
                    </div>
                </div>
                <div class="row MarginRow2 " style="width: 100%;">
                    <div style="width: 15%; margin-left: 40%" class="row">
                        <lable class="InputLable">מבוטח ראשי:</lable>
<%--                        <lable class="ColorLable" id="FullName" runat="server"></lable>--%>
                            <asp:Button OnClick="OpenContact_Click"  ID="FullName" runat="server" name="FullName" type="text" 
                                style="width: 100%; border-bottom: 0px;background: none;text-align: right;font-size: medium;" class="InputAdd" />
                                         <img src="images/icons/copy.png"  onclick="CopyToClipboard()" style="font-size:24px;background:none;border:none; width: 20px;margin-right: 10px;height:20px"></img>

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
                <div class="row MarginRow2" style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">ת.ז. של מבוטח ראשי:</lable>
                        <lable class="ColorLable" id="Tz" runat="server"></lable>

                    </div>
                     <div style="width: 31%; margin-left: 14%" class="row">
                        <lable class="InputLable">מקור ההלוואה/ביטוח:</lable>
                        <%--                        <lable class="ColorLable">לאומי</lable>--%>
                        <select runat="server" id="SelectSourceLoanOrInsurance" class="selectGlobal"></select>
                    </div>
                </div>
                <div class="row GrayLine" style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">טלפון:</lable>
                        <lable class="ColorLable" id="Phone" runat="server"></lable>
                         <img src="images/icons/copy.png"  onclick="CopyPhoneToClipboard()" style="font-size:24px;background:none;border:none; width: 20px;margin-right: 10px;height:20px"></img>

                    </div>
                     <div style="width: 31%; margin-left: 14%" class="row">
                     
                    </div>
                </div>
                <div  class="row MarginRow2 "  style="width: 100%;">
                    <div  style="width: 55%;">
                                       <div class="row MarginRow2 PaddingRow" style="width: 100%;">
                    <div style="width: 10%; margin-left: 24%" class="row">
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
                <div class="row MarginRow2 " style="width: 100%;">
                    <div style="width: 10%; margin-left: 24%" class="row">
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
                <div class="row MarginRow2 " style="width: 100%;">
                    <%--    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">מספר בקשות שירות גביה:</lable>
                        <lable class="ColorLable">3.6.2023</lable>
                    </div>--%>
                    <div style="width: 10%; margin-left: 24%" class="row">
                        <lable class="InputLable">תאריך שליחה למתפעלת:</lable>
                        <%--                        <lable class="ColorLable">לאומי</lable>--%>
                       <lable class="ColorLable" id="DateSentToOperator" runat="server"></lable>

                    </div>
                    <div style="width: 45%; margin-left: 2%;" class="row">
                        <label class="InputLable">תאריך שליחה לחברת הביטוח :</label>
                        <lable id="DateSentToInsuranceCompany" runat="server" style="width: 100%;" class="ColorLable" />
                    </div>
                </div>
                <%--         <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">מקור ההלוואה:</lable>
                        <lable class="ColorLable">לאומי</lable>
                    </div>
                </div>--%>

                    </div>
                    <div  style="width: 45%;">
                         <textarea id="Note" name="FirstName" type="text" runat="server" style="width: 100%;height: 164px;border: 1px solid rgb(0, 152, 255);
                                    border-radius: 12px;margin-top: 20px;resize: none;overflow-y: hidden;scrollbar-width: none; padding:5px" class="InputAdd" />

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
                <div class="row PaddingRow MarginRow2" style="width: 100%;">
<%--                    <asp:ImageButton ID="ImageButton2" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Mark_For_Archives_Button.png" OnClick="CopyLid_Click" />--%>
                    <asp:ImageButton ID="ImageButton3" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Send_Mail_Button.png" OnClick="SendMail_Click" />
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Send_Sms_Button.png" OnClick="SendSms_Click" />
                </div>

                <div class="MainDivDocuments" style="height: 290px;">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <div class="row SecondaryDivDocumentsWithImg" runat="server">
                                <div style="width: 2%;" class="RowDocuments">
                                    <asp:CheckBox runat="server" Style="vertical-align:middle" AutoPostBack="true" ID="IsPromoted" />
                                </div>
                                <div style="width: 2%; text-align: left;" class="RowDocuments">
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Pdf_Icon.png" OnClick="DeleteLid_Click" />
                                </div>
                                <div class="row DivBackground DivBackgroundRep">
                                    <div runat="server" style="width: 79%; text-align: right;" class="RowDocuments DivNameFile">
                                        
                                             <asp:Label ID="FileName" Text='<%# Eval("FileName") %>' runat="server"/> 
                                 
                                    </div>
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <asp:ImageButton ID="fileImg" OnCommand="OpenImage_Click" CommandArgument='<%# Eval("FileName") + "," + Eval("Base64String") %>'  Visible="true" runat="server" style="width: auto; height: 80%; margin-top: 6%; border-radius: 3px;" />
                                        <asp:Image ID="filePdf" Visible="false" ImageUrl="~/images/icons/pdf.png" runat="server" style="width: auto; height: 50%; margin-top: 15%; border-radius: 3px;" />
                                     
                                    </div> 
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="UploadFile" runat="server" OnCommand="UploadFile_Command" CommandArgument='<%# Eval("FileName") + "," + Eval("ID") %>' Style="vertical-align: middle; position: relative; margin-top: 16%; margin-bottom: 12%;" ImageUrl="~/images/icons/Choosing_New_Service_Request_Downlaod_Button.png" />
                                    </div>       
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="RemoveFile" runat="server" OnCommand="RemoveFile_Command" CommandArgument='<%# +  Container.ItemIndex + "," + Eval("ID")   %>' Style="vertical-align: middle; position: relative; height: 46%;margin-top: 16%;" ImageUrl="~/images/icons/Delete_Lid_Button.png" />
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



                <div runat="server" id="divRepeat" style="height: 380px; overflow-x: auto; margin-bottom: 20px">
                    <asp:Repeater ID="Repeater2" runat="server" >
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

              <div  id="divHistory" runat="server" visible="false"  class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                   <div class="row" style="align-items: center;">
                        <div class="div-arrows-img">
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">היסטוריית שינויים</label>
                        </div>
                   </div>                  
                </div>
                <div class="ListDivParamsHead DivParamsHeadMargin">
                     <div style="width: 20%; text-align: right; padding-right: 1%">תאריך</div>
                     <div style="width: 20%; text-align: right;">סטטוס</div>
                     <div style="width: 10%; text-align: right;">סוכן</div>                   
                     <div style="width: 50%; text-align: right;"></div>                   
                </div>
                <div runat="server" id="div1" style="height: 200px; overflow-x: auto; margin-bottom: 20px">
                    <asp:Repeater ID="Repeater4" runat="server" >
                        <ItemTemplate>
                            <div class='ListDivParams' style="position: relative;padding-left: 18px;">
                                 <div style="width: 50%; text-align: right"></div>
                                 <div style="width: 10%; text-align: right;"><%#Eval("Agent") %></div>                                  
                                 <div style="width: 20%; text-align: right"><%#Eval("Status") %></div>
                                 <div style="width: 20%; text-align: right; padding-right: 1%"><%#Eval("CreateDate") %></div>                         
                            </div>
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

              <div id="OpenTasksList" class="popUpOut MainDivDocuments" visible="false" runat="server">
                <div class="popUpIn" style="width: 35%; height: 700px; margin-top: 100px; margin-bottom: 100px; direction: rtl; text-align: center; border-width: 2px;" runat="server">  
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="ImageButton6" OnClick="CloseTasksListPopUp_Click" />
                    <div class="DivDownPopUp">
                        <label style="font-size:15pt" class="LableBlue">רשימת משימות פתוחות</label>
                        <div class="MainDivDocuments DivRepeaterPopUp">
                         <asp:Repeater ID="Repeater3" runat="server">
                             <ItemTemplate>
                                 <div class="row DivRpwRepeaterPopUp">
                                    <div style="width: 50%; text-align: right;" class="ColorLable"><%#Eval("Text") %></div>
                                    <div style="width: 22.5%; text-align: right;" class="ColorLable"><%#Eval("PerformDate") %></div>
                                    <div style="width: 22.5%; text-align: right;" class="ColorLable"><%#Eval("Status") %></div>
                                    <div style="width: 5%; text-align: right;" class="ColorLable">
                                          <asp:ImageButton ID="DeleteTask" runat="server" OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק את המשימה?');"  CommandArgument='<%#Eval("ID") %>' OnCommand="DeleteTask_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Open_Mession_Delete_Button.png" />
                                    </div> 
                                        <div class="text-gray" style="width: 5%">
<%--                                            <img src="images/icons/Open_Mession_Edit_Button.png" runat="server" style="width: 18px; height: auto;" />--%>
                                            <asp:ImageButton OnCommand="Mession_Edit"  CommandArgument='<%#Eval("ID") %>' runat="server" style="width: 18px; height: auto; vertical-align: middle;" ImageUrl="images/icons/Open_Mession_Edit_Button.png"  />
                                        </div>
                                 </div>
                                 <div class="RowWhitePopUp"></div>
                             </ItemTemplate>


                         </asp:Repeater>
                     </div>
                    </div>
                 </div>
            </div>


              <div id="CreateDocPopup" class="popUpOut MainDivDocuments" visible="false" runat="server">
              <div class="popUpIn" style="width: 60%; height: 850px;   margin-top: 30px; margin-bottom: 30px; direction: rtl; text-align: center; border-width: 2px;" runat="server">
                     <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="ImageButton8" OnClick="CloseCreateDoc_Click" />
                      <div class="col MainDivPopup2" style="padding-top: 15px;">
                          <label class="HeaderPopupPurple">הסכם שירות</label>
                                  <div class="row" style="display: flex; flex-direction: column; width: 70%;">
                                   <asp:Repeater ID="Repeater5" runat="server" >
                                        <ItemTemplate>
                                                   <div style="width: 70%; ">
                                                       <div class="row MarginRow2" style="width: 100%;">
                                                            <div style="width: 100%;"class="row">
                                                                 <asp:CheckBox runat="server" ClientIDMode="Static" ID="CheckBox"  AutoPostBack="true" />
                                                                 <asp:Label ID="lblIsApprove1" AssociatedControlID="CheckBox" runat="server" CssClass="lblAns" Text='<%# Container.DataItem %>'></asp:Label>                  
                                                            </div>
                                         
                                       
                                                         </div>
                <%--                                       <div class="row MarginRow2 " style="width: 100%;">
                                                            <div style="width: 60%;" class="row">
                                                               <asp:CheckBox runat="server" ID="CheckBox1"  AutoPostBack="true" />
                                                               <asp:Label ID="Label2" AssociatedControlID="CheckBox1" runat="server" CssClass="lblAns" Text="הגשת בקשה להלוואה כנגד קופות (פנסיה, גמל, השתלמות, ביטוח מנהלים)"></asp:Label>
                                                            </div>
                                           
                                                        </div>

                                                       <div class="row MarginRow2 " style="width: 100%;">
    
                                                            <div style="width: 60%;"  class="row">
                                                                  <asp:CheckBox runat="server" ID="CheckBox2"  AutoPostBack="true" />
                                                                  <asp:Label ID="Label3" AssociatedControlID="CheckBox2" runat="server" CssClass="lblAns" Text="הגשת בקשה לפדיון כספים מחברות הביטוח"></asp:Label>
                                         
                                                            </div>
                                      
                                                       </div>          
                                                      <div class="row MarginRow2 " style="width: 100%;">
    
                                                            <div style="width: 60%;" class="row">
                                                                  <asp:CheckBox runat="server" ID="CheckBox3"  AutoPostBack="true" />
                                                                  <asp:Label ID="Label5" AssociatedControlID="CheckBox3" runat="server" CssClass="lblAns" Text="הלוואה כנגד הנכס (בנקאי/חוץ בנקאי)"></asp:Label>
                                         
                                                            </div>
                                      
                                                       </div>   
                                                      <div class="row MarginRow2 " style="width: 100%;">
    
                                                            <div style="width: 60%" class="row">
                                                                  <asp:CheckBox runat="server" ID="CheckBox4"  AutoPostBack="true" />
                                                                  <asp:Label ID="Label6" AssociatedControlID="CheckBox4" runat="server" CssClass="lblAns" Text="הגשת בקשה להלוואה (בנקאי/חוץ בנקאי)"></asp:Label>
                                         
                                                            </div>
                                     
                                                       </div>--%>
        
                                                  </div>
                                            </ItemTemplate>
                            </asp:Repeater>
                            </div>
                       
                            <table style="border-collapse: collapse; width: 70%; margin-top:20px">
                                <tr>
                                    <td  style="border: 1px solid black; padding: 8px; text-align: center; font-weight: bold;width:22%;font-size:14px">סוג קופה</td>
                                    <td style="border: 1px solid black; padding: 8px; width:13%">
                                        <asp:TextBox ID="PosType1" runat="server" CssClass="TextInnerTable"/>
                                    </td>
                                    <td style="border: 1px solid black; padding: 8px; width:13%">
                                         <asp:TextBox ID="PosType2" runat="server" CssClass="TextInnerTable"/>

                                    </td>
                                    <td style="border: 1px solid black; padding: 8px; width:13%">
                                         <asp:TextBox ID="PosType3" runat="server" CssClass="TextInnerTable"/>
                                    </td>
                                    <td style="border: 1px solid black; padding: 8px; width:13%">
                                          <asp:TextBox ID="PosType4" runat="server" CssClass="TextInnerTable"/>
                                    </td>
                                    <td style="border: 1px solid black; padding: 8px; width:13%">
                                          <asp:TextBox  ID="PosType5" runat="server" CssClass="TextInnerTable"/>

                                    </td>
                                    <td style="border: 1px solid black; padding: 8px; width:13%">
                                          <asp:TextBox  ID="PosType6" runat="server" CssClass="TextInnerTable"/>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid black; padding: 8px; text-align: center; font-weight: bold;font-size:14px">סוג בקשה</td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                        <asp:TextBox ID="RequestType1" runat="server" CssClass="TextInnerTable"/>
                                    </td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                        <asp:TextBox ID="RequestType2" runat="server" CssClass="TextInnerTable"/>

                                    </td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                        <asp:TextBox ID="RequestType3" runat="server" CssClass="TextInnerTable"/>

                                    </td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                         <asp:TextBox ID="RequestType4" runat="server" CssClass="TextInnerTable"/>

                                    </td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                          <asp:TextBox ID="RequestType5" runat="server" CssClass="TextInnerTable"/>

                                    </td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                          <asp:TextBox ID="RequestType6" runat="server" CssClass="TextInnerTable"/>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid black; padding: 8px; text-align: center; font-weight: bold;font-size:14px">חברת ביטוח</td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                          <asp:TextBox ID="InsuranceCompany1" runat="server" CssClass="TextInnerTable"/>

                                    </td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                        <asp:TextBox ID="InsuranceCompany2" runat="server" CssClass="TextInnerTable"/>
                                    </td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                        <asp:TextBox ID="InsuranceCompany3" runat="server" CssClass="TextInnerTable"/>
                                    </td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                        <asp:TextBox ID="InsuranceCompany4" runat="server" CssClass="TextInnerTable"/>
                                    </td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                        <asp:TextBox  ID="InsuranceCompany5" runat="server" CssClass="TextInnerTable"/>
                                    </td>
                                    <td style="border: 1px solid black; padding: 8px;">
                                        <asp:TextBox ID="InsuranceCompany6" runat="server" CssClass="TextInnerTable"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid black; padding: 8px; text-align: center; font-weight: bold;font-size:14px">מס' קופה</td>
                                    <td style="border: 1px solid black; padding: 8px;"><asp:TextBox ID="PosNumber1" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"><asp:TextBox ID="PosNumber2" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"><asp:TextBox ID="PosNumber3" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"><asp:TextBox ID="PosNumber4" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"><asp:TextBox ID="PosNumber5" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"><asp:TextBox ID="PosNumber6" runat="server" CssClass="TextInnerTable"/></td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid black; padding: 8px; text-align: center; font-weight: bold;font-size:14px">סכום בקשה</td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="RequestSum1" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="RequestSum2" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="RequestSum3" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="RequestSum4" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="RequestSum5" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="RequestSum6" runat="server" CssClass="TextInnerTable"/></td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid black; padding: 8px; text-align: center; font-weight: bold;font-size:14px">הלוואה קיימת</td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="ExistingLoan1" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="ExistingLoan2" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="ExistingLoan3" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="ExistingLoan4" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="ExistingLoan5" runat="server" CssClass="TextInnerTable"/></td>
                                    <td style="border: 1px solid black; padding: 8px;"> <asp:TextBox ID="ExistingLoan6" runat="server" CssClass="TextInnerTable"/></td>
                                </tr>
                            </table>

                            <div class="row PaddingRow2" style="width: 70%; margin-top:15px">
                
                                <div style="width: 50%; margin-left: 14%;" class="row">
                                    <label class="InputLable">מספר אשראי:</label>
                                    <input id="CreditNumber" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd2" />
                                </div>
                                <div style="width: 50%;" class="row">
                                    <label class="InputLable">עלות הטיפול:</label>
                                    <input id="Text4" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd2" />
                                </div>
                            </div>
                
                           
                            <div class="row PaddingRow2" style="width: 70%;">
                                <div style="width: 50%; margin-left: 14%;" class="row">
                                    <label class="InputLable">תוקף:</label>
                                    <select  runat="server"  style="border-bottom: 1px solid black;  background: url(../images/icons/Arrow_Slide_Button.png) no-repeat left 4px center;" ID="SelectYear"  class="selectGlobal"></select>                       
                                    <select  runat="server"   ID="SelectMonth" style="margin-right:25px;border-bottom: 1px solid black;    background: url(../images/icons/Arrow_Slide_Button.png) no-repeat left 4px center;" class="selectGlobal"></select>                       

            <%--                        <input id="CreditValidity" name="FullName" type="date" runat="server" style="width: 100%;" class="InputAdd" />--%>
                                </div>
                               <div style="width: 50%;" class="row">
                                    <label class="InputLable">הערות:</label>
                                </div>
                            </div>
                            <div class="row PaddingRow2 " style="width: 70%;">
               
                                <div style="width: 50%; margin-left: 14%;" class="row">
                                    <label class="InputLable">Cvv:</label>
                                    <input id="Cvv" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd2" />
                                </div>
                                  <div style="width: 50%;" class="row">
                                    <input id="Text1" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd2" />
                                </div>
                            </div>
                            <div class="row PaddingRow2" style="width: 70%;">
                
                                <div style="width: 50%; margin-left: 14%;" class="row">
                                    <label class="InputLable">ת.ז. בעל הכרטיס:</label>
                                    <input id="CardholdersID" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd2" />
                                </div>
                                 <div style="width: 50%;" class="row">
                                    <label class="InputLable">שם מיופה הכח (יחיד/תאגיד):</label>
                                    <input id="Text3" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd2" />
                                </div>
                            </div>        
                          <div class="row PaddingRow2 MarginRow" style="width: 70%;">
                
                                <div style="width: 50%; margin-left: 14%;" class="row">
                                    <label class="InputLable">שם בעל הכרטיס:</label>
                                    <input id="CreditHolderName" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd2" />
                                </div>
                                <div style="width: 50%;" class="row">
                                    <label class="InputLable">רשיון מס':</label>
                                    <input id="Text2" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd2" />
                                </div>
                            </div>
                           <div class="DivLidTop">
                               <label class="InputLable" id="error" style="color:red" runat="server" visible="false">עליך למלא את כל הפרטי אשראי</label>
                            </div> 
                          <div class="DivLidTop PaddingRow2">
                                <asp:Button runat="server" ID="Button1" Text="שלח טופס" OnClick="btn_sendDoc_Click" Style="width: 165px; height: 53px;" CssClass="Btn" OnClientClick="reload(LoadingDiv);" />
                            </div>

                       </div>
                   </div>
                </div>
           <div id="TaskDiv" class="popUpOut MainDivDocuments" visible="false" runat="server">
                <div id="TaskDiv2" class="popUpIn" style="width: 35%; height: 592px; margin-top: 147px; margin-bottom: 100px; direction: rtl; text-align: center; border-width: 2px;" runat="server">
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="ImageButton5" OnClick="CloseTaskPopUp_Click" />
                    <div class="col MainDivPopup2" style="padding-top: 45px;">
                        <label runat="server" id="titleTask" class="HeaderPopupPurple">משימה חדשה</label>
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
                                              <asp:HiddenField runat="server" ID="ID" />


                        <asp:Button ID="AddNewTask" Name="AddNewTask" OnClick="OpenNewTask_Click" runat="server"  Text="פתח" class="AddAgentButton" />
                          <asp:Label ID="FormErrorTask_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
                   </div>
                    </div>



                </div>
            </div>

         </ContentTemplate>
     </asp:UpdatePanel>

       <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="popupImg" class="popUpOut MainDivDocuments" visible="false" runat="server">
                <div id="Div2" class="popUpIn" style="    width: 50%; height: 90%;margin-top: 40px; direction: rtl;  text-align: center; border-width: 2px;" runat="server">
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="ImageButton7" OnClick="CloseImagePopUp_Click" />
                   
                    <div class="col" style="padding: 0px 5%; align-items: center;">
                         <asp:Image ID="fileImg"  runat="server" style="width: auto;height: 721px; margin-top: 6%; border-radius: 3px;" />

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
           function CopyToClipboard() {
               var textToCopy = document.getElementById('<%=FullName.ClientID %>');

               navigator.clipboard.writeText(textToCopy.value)
                   .then(() => {
                       alert("הועתק בהצלחה!");
                   })
                   .catch((err) => {
                       console.error("Failed to copy text: ", err);
                   });
           }

           function CopyPhoneToClipboard() {

               var textToCopy = document.getElementById('<%=Phone.ClientID %>');

               navigator.clipboard.writeText(textToCopy.innerText)
                   .then(() => {
                       alert("הועתק בהצלחה!");
                   })
                   .catch((err) => {
                       console.error("Failed to copy text: ", err);
                   });
           }

           function adjustTextareaHeight(textarea) {
               // Reset height to auto to get the correct scrollHeight measurement
               textarea.style.height = "164px"; // Default height

               if (textarea.value.trim() === "") {
                   // If textarea is empty, set to default height
                   textarea.style.height = "164px";
               } else {
                   // Set height based on content
                   textarea.style.height = textarea.scrollHeight + "px";
               }
           }

           // Run when the document is loaded
           document.addEventListener("DOMContentLoaded", function () {
               var textarea = document.getElementById("ContentPlaceHolder1_Note");

               // Initial adjustment
               adjustTextareaHeight(textarea);

               // Add event listeners for input changes
               textarea.addEventListener("input", function () {
                   adjustTextareaHeight(this);
               });

               // Also adjust on window resize
               window.addEventListener("resize", function () {
                   adjustTextareaHeight(textarea);
               });
           });
       </script>
</asp:Content>

