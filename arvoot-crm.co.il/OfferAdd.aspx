﻿<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="OfferAdd.aspx.cs" Inherits="ControlPanel._offerAdd" %>

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
                <%--   <asp:ImageButton ID="CopyLid" runat="server" ImageUrl="~/images/icons/Copy_Lid_Button.png" OnClick="CopyLid_Click" />
                <asp:ImageButton ID="ShereLid" runat="server" ImageUrl="~/images/icons/Shere_Lid_Button.png" OnClick="ShereLid_Click" />
                <asp:ImageButton ID="DeleteLid" runat="server" ImageUrl="~/images/icons/Delete_Lid_Button.png" OnClick="DeleteLid_Click" />
                --%>
                <asp:Button runat="server" ID="btn_save" Text="שמור" OnClick="btn_save_Click" Style="padding-top: 7px; padding-bottom: 7px" CssClass="BtnSave" OnClientClick="reload(LoadingDiv);" />
            </div>
            <div>
                <asp:Label ID="FormError_label" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
            </div>
            <div class="NewOfferDiv">
                <input placeholder="הצעה חדשה" id="NameOffer" style="font-size: 20pt;" runat="server" class="NewOfferLable" />
            </div>

            <div class="row MarginDiv">
                <div class="col DivDetails" style="width: 100%;">
                    <div class="row">
                        <div style="width: 12%; text-align: center; padding-top: 44px;">
                            <img src="images/icons/Agent_Avatar_Icon.png" runat="server" />
                        </div>
                        <div class="col" style="width: 12%;">
                            <label class="LableDetails">סוכנות</label>
                            <label id="lblAgency" runat="server" class="PaddingAgentCus ColorLable"></label>
                        </div>
                        <div class="col" style="width: 12%;">
                            <label class="LableDetails">בעלים</label>
                            <label id="lblOwner" runat="server" class="PaddingAgentCus ColorLable"></label>
                        </div>
                        <div class="col" style="width: 14%;">
                           <%-- <label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus ColorLable"></label>--%>
                        </div>
                        <div style="width: 50%"></div>
                    </div>
                    <div class="DivGray " style="height: 60px; line-height: 60px; padding-right: 10%;">
                        <label class="LableDetails">נוצר על ידי:</label>
                        <label class=" ColorLable"></label>
                    </div>
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
                    </div>
                    <div class="row">
                        <label class="InputLable">סטטוס:</label>

                        <select runat="server" id="SelectStatusOffer" class="selectGlobal"></select>

                        <%--                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/In_Treatment_Status_Button.png" OnClick="CopyLid_Click" />--%>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">מבוטח ראשי:</lable>
                        <lable class="ColorLable" id="FullName" runat="server"></lable>
                        <img src="images/icons/copy.png"  onclick="CopyToClipboard()" style="font-size:24px;background:none;border:none; width: 20px;margin-right: 10px;height:20px"></img>

                    </div>
                    <%--      <div style="width: 20%; margin-left: 2%;" class="row">
                        <label class="InputLable">בעלים:</label>
                        <lable class="ColorLable" id="FullNameAgent" runat="server"></lable>
                    </div>
                    <div style="width: 23%;" class="row">
                        <%--  <asp:CheckBox runat="server" ID="IsPromoted" />
                        <asp:Label ID="lblIsPromoted" AssociatedControlID="IsPromoted" runat="server" CssClass="lblAns ColorLable" Text=" העבר לתור תפעול"></asp:Label>
                        <label class="InputLable">תור:</label>
                        <select id="SelectTurnOffer" runat="server" class="selectGlobal"></select>
                    </div>--%>
                    <div style="width: 20%; margin-left: 2%;" class="row">
                        <label class="InputLable">סוג הצעה:</label>
                        <select runat="server" id="SelectOfferType" class="selectGlobal"></select>
                    </div>
                </div>
                <div class="row MarginRow" style="width: 100%;">
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
                <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 55%;">
                        <div class="row MarginRow PaddingRow" style="width: 100%;">
                            <div style="width: 10%; margin-left: 24%" class="row">
                                <lable class="InputLable">מועד כניסה לתוקף:</lable>
                                <lable class="ColorLable" id="EffectiveDate" runat="server"></lable>
                            </div>
                            <div style="width: 47%; margin-left: 2%;" class="row">
                                <label class="InputLable">סיבה לחוסר הצלחה:</label>
                                <input id="ReasonLackSuccess" name="ReasonLackSuccess" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>
                            <div style="width: 18%;" class="row">
                                <label style="width: 100%; text-align: left;" class="InputLable">הערות:</label>
                                <%--                        <textarea id="Note" name="FirstName" type="text" runat="server" style="width: 100%;" class="InputAdd" />--%>
                            </div>
                        </div>
                        <div class="row MarginRow " style="width: 100%;">
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
                        <div class="row MarginRow " style="width: 100%;">
                            <%--    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">מספר בקשות שירות גביה:</lable>
                        <lable class="ColorLable">3.6.2023</lable>
                    </div>--%>
                            <div style="width: 10%; margin-left: 24%" class="row">
                                  <lable class="InputLable">תאריך שליחה למתפעלת:</lable>
                                     <%--                        <lable class="ColorLable">לאומי</lable>--%>
                                  <lable class="ColorLable" id="DateSentToOperator" runat="server"></lable>
                            </div>
                            <div style="width: 47%; margin-left: 2%;" class="row">
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
                    <div style="width: 45%;">
                        <textarea id="Note" name="FirstName" type="text" runat="server" style="width: 100%; height: 164px; border: 1px solid rgb(0, 152, 255); border-radius: 12px; margin-top: 20px; resize: none; overflow-y: hidden; scrollbar-width: none;"
                            class="InputAdd" />

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
                            <asp:ImageButton Style="margin-left: 20px" ID="UploadDocument" runat="server" ImageUrl="~/images/icons/Uplaod_File_Button.png" OnClick="DeleteLid_Click" />

                        </div>

                        <%--  <div class="HeaderSearchBox">
                            <input type="text" class="InputTextSearch" value="<% = StrSrc%>" name="Q" id="Q" onblur="javascript:if(this.value==''){this.value='חפש קובץ'};" onfocus="javascript:if(this.value=='חפש קובץ'){this.value='';}" onkeypress="javascript:runSearch(event, 'Enrollment.aspx');" />
                            <a href="javascript:window.location.href = 'Enrollment.aspx?Q=' + document.getElementById('Q').value;">
                                <img src="images/icons/Search_Pdf_Button.png" class="SearchIcon" /></a>
                        </div>--%>
                    </div>
                </div>
                <%--  <div class="row PaddingRow MarginRow" style="width: 100%;">
                    <asp:ImageButton ID="ImageButton2" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Mark_For_Archives_Button.png" OnClick="CopyLid_Click" />
                    <asp:ImageButton ID="ImageButton3" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Send_Mail_Button.png" OnClick="ShereDoc_Click" />
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Send_Sms_Button.png" OnClick="DeleteLid_Click" />
                    
                </div>--%>

                <div class="MainDivDocuments" style="height: 290px;">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <div class="row SecondaryDivDocumentsWithImg" runat="server">
                                <div style="width: 2%;" class="RowDocuments">
                                    <%--                                     <asp:CheckBox runat="server" Style="vertical-align:middle" AutoPostBack="true" ID="IsPromoted" />--%>
                                </div>
                                <div style="width: 2%; text-align: left;" class="RowDocuments">
                                    <asp:ImageButton ID="ImageButton4" Style="vertical-align: middle" runat="server" ImageUrl="~/images/icons/Pdf_Icon.png" />
                                </div>
                                <div class="row DivBackground DivBackgroundRep">
                                    <div runat="server" style="width: 79%; text-align: right;" class="RowDocuments DivNameFile">
                                        <%--                                    <%#Eval("File") %>--%>
                                        <%--<asp:Label ID="FileNameLabel" runat="server" />--%>
                                        <%# Eval("FileName") %>
                                    </div>
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <asp:ImageButton ID="fileImg" OnCommand="OpenImage_Click" CommandArgument='<%# Eval("FileName") + "," + Eval("FileBase64String") %>'  Visible="true" runat="server" style="width: auto; height: 80%; margin-top: 6%; border-radius: 3px;" />
                                        <asp:Image ID="filePdf" Visible="false" ImageUrl="~/images/icons/pdf.png" runat="server" style="width: auto; height: 50%; margin-top: 15%; border-radius: 3px;" />
                                     
                                    </div> 
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="DownloadFile" runat="server" OnCommand="Download_Click" CommandArgument='<%# Container.ItemIndex %>' Style="vertical-align: middle; position: relative; margin-top: 16%; margin-bottom: 12%;" ImageUrl="~/images/icons/Choosing_New_Service_Request_Downlaod_Button.png" />
                                    </div>
                                       <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="RemoveFile" runat="server" OnCommand="RemoveFile_Command" CommandArgument='<%# +  Container.ItemIndex    %>' Style="vertical-align: middle; position: relative; height:46%; margin-top: 16%;" ImageUrl="~/images/icons/Delete_Lid_Button.png" />
                                    </div>
                                    <%--   <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />
                                        <asp:ImageButton ID="ImageButton5" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Shere_Button.png" />
                                    </div>--%>
                                </div>



                            </div>

                            <div style="height: 20px;"></div>
                        </ItemTemplate>


                    </asp:Repeater>
                </div>
                <div style="width: 20%; text-align: left; padding-top: 14px;">

                    <%--                      <asp:ImageButton ID="ImageButton1" Style="width:250px;height:auto" runat="server" ImageUrl="~/images/icons/Choosing_Service_New_Service_Button.png" OnClick="ServiceRequestAdd_Click" />--%>
                </div>


            </div>
            <div class="DivLidTop">
                <asp:Button runat="server" ID="BtnSaveBottom" Text="שמור" OnClick="btn_save_Click" Style="padding-top: 7px; padding-bottom: 7px" CssClass="BtnSave" OnClientClick="reload(LoadingDiv);" />
            </div>
            <div>
                <asp:Label ID="FormErrorBottom_label" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
           <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="popupImg" class="popUpOut MainDivDocuments " visible="false" runat="server">
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

        function CopyToClipboard() {

            var textToCopy = document.getElementById('<%=FullName.ClientID %>');

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
         document.addEventListener("DOMContentLoaded", function() {
        var textarea = document.getElementById("ContentPlaceHolder1_Note");

        // Initial adjustment
        adjustTextareaHeight(textarea);

        // Add event listeners for input changes
        textarea.addEventListener("input", function() {
                adjustTextareaHeight(this);
        });

        // Also adjust on window resize
        window.addEventListener("resize", function() {
                adjustTextareaHeight(textarea);
        });
         });

    </script>
    <%--    <script type="text/javascript">MarkMenuCss('Users');</script>--%>
</asp:Content>

