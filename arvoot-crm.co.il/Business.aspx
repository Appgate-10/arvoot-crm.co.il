<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="Business.aspx.cs" Inherits="ControlPanel._business" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% 
        string Title = "Customers";
    %>
    <meta name="Description" content='<% = Title%>' />
    <meta name="keywords" content='<% = Title%>' />
    <meta name="abstract" content='<% = Title%>' />
    <meta http-equiv="title" content="<% = Title%>" />
    <title><% = Title %></title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="DivLidTop">
                <asp:ImageButton ID="CopyLid" runat="server" ImageUrl="~/images/icons/Copy_Lid_Button.png" OnClick="CopyLid_Click" />
                <asp:ImageButton ID="ShereLid" runat="server" ImageUrl="~/images/icons/Shere_Lid_Button.png" OnClick="ShereLid_Click" />
                <asp:ImageButton ID="DeleteLid" runat="server" ImageUrl="~/images/icons/Delete_Lid_Button.png" OnClick="DeleteLid_Click" />
            </div>
            <div class="NewOfferDiv">
                <label class="NewOfferLable">בקשת שירות חדשה </label>
            </div>

            <div class="row MarginDiv">
                <div class="col DivDetails" style="margin-inline-end: 3%;">
                    <div class="row">
                        <div style="width: 20%; text-align: center; padding-top: 44px;">
                            <img src="images/icons/Agent_Avatar_Icon.png" runat="server" />
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">סוכנות</label>
                            <label class="PaddingAgentCus ColorLable">Lisanee</label>
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">בעלים</label>
                            <label class="PaddingAgentCus ColorLable">Lisanee</label>
                        </div>
                        <div class="col" style="width: 30%;">
                            <label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus ColorLable">Lisanee</label>
                        </div>

                    </div>
                    <div class="DivGray"></div>
                </div>
                <div class="col DivDetails">
                    <div class="row">
                        <div style="width: 20%; text-align: center; padding-top: 44px;">
                            <img src="images/icons/Customer_Avatar.png" runat="server" />
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">סוכנות</label>
                            <label class="PaddingAgentCus ColorLable">Lisanee</label>
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">בעלים</label>
                            <label class="PaddingAgentCus ColorLable">Lisanee</label>
                        </div>
                        <div class="col" style="width: 30%;">
                            <label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus ColorLable">Lisanee</label>
                        </div>

                    </div>
                    <div class="DivGray"></div>
                </div>
            </div>

            <div class="col MarginDiv SecondaryDiv ">
                <div class="row MarginRow ServiceRequestDiv">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">פירוט הצעה</label>
                        </div>
                    </div>
                    <div class="row">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/In_Treatment_Status_Button.png" OnClick="CopyLid_Click" />
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div class="row col1">
                        <lable class="InputLable">שם:</lable>
                        <lable class="ColorLable">#123456</lable>
                    </div>
                    <div class="row col2">
                        <label class="InputLable">שם סוכנות:</label>
                        <lable class="ColorLable">רונית לוינסון</lable>
                    </div>
                    <div class="row col3">
                        <label class="InputLable">פרמיה:</label>
                        <lable class="ColorLable">098098</lable>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div class="row col1">
                        <lable class="InputLable">הצעה מקור:</lable>
                        <lable class="ColorLable">מגדל חברה לביטוח</lable>
                    </div>
                    <div class="row col2">
                        <label class="InputLable">סוג רשומה:</label>
                        <lable class="ColorLable">פוליסת חיים</lable>
                    </div>
                    <div class="row col3">
                        <label class="InputLable">עלות חודשים</label>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div class="row col1">
                        <lable class="InputLable">מספר פוליסה:</lable>
                        <lable class="ColorLable">#098980</lable>
                    </div>
                    <div class="row col2">
                        <label class="InputLable">בעלים:</label>
                        <lable class="ColorLable">נועם אביטל</lable>
                    </div>
                    <div class="row col3">
                        <label class="InputLable">הוטל עיקולים:</label>
                        <lable class="LableBlue">לא</lable>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div class="row col1">
                        <lable class="InputLable">משק בית:</lable>
                        <lable class="ColorLable">משפחת אביטל</lable>
                    </div>
                    <div class="row col2">
                        <label class="InputLable">סטטוס:</label>
                        <lable class="LableBlue">פעיל</lable>
                    </div>
                    <div class="row col3">
                        <label class="InputLable">הוטל שיעבוד:</label>
                        <lable class="LableBlue">לא</lable>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div class="row col1 AlignItemsCenter" >
                        <img src="images/Chack_Box_1_On.png" class="ImgV" style="margin-left: 10px;" runat="server" />
                        <lable class="ColorLable">מיידי</lable>
                    </div>
                    <div class="row col2">
                        <label class="InputLable">תיאור:</label>
                        <lable class="ColorLable">משפחת אביטל #1234567</lable>
                    </div>
                    <div class="row col3 AlignItemsCenter">
                        <img src="images/Chack_Box_1_On.png" class="ImgV" style="margin-left: 10px;" runat="server" />
                        <label class="InputLable">פוליסה מנוהלת בסוכנות אחרת</label>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div class="row col1">
                        <lable class="InputLable">סטטוס גביה ראשון:</lable>
                        <lable class="LableBlue">פעיל</lable>
                    </div>
                    <div class="row col2">
                        <label class="InputLable">מועד כניסה לתוקף:</label>
                        <lable class="ColorLable">1.1.23</lable>
                    </div>
                    <div class="row col3">
                        <label class="InputLable">תאריך מילות</label>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div class="row col1">
                        <lable class="InputLable">בתאריך צפוי לגביה ראשונה:</lable>
                        <lable class="ColorLable">משפחת אביטל</lable>
                    </div>
                    <div class="row col2">
                        <label class="InputLable">תאריך סיום הביטוח:</label>
                        <lable class="ColorLable">1.1.23</lable>
                    </div>
                    <div class="row col3 AlignItemsCenter" >
                        <img src="images/Chack_Box_1_On.png"  class="ImgV" style="margin-left: 10px;" runat="server" />
                        <label class="LableBlue">הלקוח מעל גיל 60</label>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div class="row col1">
                        <lable class="InputLable">אחוז הנחה:</lable>
                        <lable class="ColorLable" style="margin-left: 10px;">12%</lable>
                        <lable class="InputLable">תאריך סיום הנחה:</lable>
                        <lable class="ColorLable">1.3.23</lable>
                    </div>
                    <div class="row col2">
                        <label class="InputLable">פרמיה:</label>
                        <lable class="ColorLable">098908</lable>
                    </div>

                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div class="row col1">
                        <lable class="InputLable">אחוז הנחה2:</lable>
                        <lable class="ColorLable" style="margin-left: 10px;">16%</lable>
                        <lable class="InputLable">תאריך סיום הנחה:</lable>
                        <lable class="ColorLable">1.3.23</lable>
                    </div>

                </div>
            </div>




            <div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row" style="margin-inline-end: 60px;">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">מסמכים מצורפים</label>
                        </div>
                    </div>
                    <div class="row HeaderBoxD" style="justify-content: flex-end;">
                        <div class="HeaderSearchBox">
                            <input type="text" class="InputTextSearch" value="<% = StrSrc%>" name="Q" id="Q" onblur="javascript:if(this.value==''){this.value='חפש קובץ'};" onfocus="javascript:if(this.value=='חפש קובץ'){this.value='';}" onkeypress="javascript:runSearch(event, 'Enrollment.aspx');" />
                            <a href="javascript:window.location.href = 'Enrollment.aspx?Q=' + document.getElementById('Q').value;">
                                <img src="images/icons/Search_Pdf_Button.png" class="SearchIcon" /></a>

                        </div>

                    </div>
                </div>
                <div class="row PaddingRow MarginRow" style="width: 100%;">
                    <asp:ImageButton ID="ImageButton2" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Mark_For_Archives_Button.png" OnClick="CopyLid_Click" />
                    <asp:ImageButton ID="ImageButton3" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Send_Mail_Button.png" OnClick="ShereLid_Click" />
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Send_Sms_Button.png" OnClick="DeleteLid_Click" />
                </div>

                <div class="MainDivDocuments" style="height: 102px;">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <div class="row SecondaryDivDocuments" runat="server">
                                <div style="width: 5%;" class="RowDocuments">
                                    <%--              <asp:CheckBox runat="server" ID="IsPromoted" />--%>
                                </div>
                                <div style="width: 2%; text-align: left;" class="RowDocuments">
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Pdf_Icon.png" OnClick="DeleteLid_Click" />
                                </div>
                                <div class="row DivBackground DivBackgroundRep">
                                    <div runat="server" style="width: 86%; text-align: right;" class="RowDocuments DivNameFile">
                                        <%--                                    <%#Eval("File") %>--%>
                                    vhh
                                    </div>
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="UploadFile" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Downlaod_Button.png" />
                                    </div>
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="ImageButton5" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Shere_Button.png" />
                                    </div>
                                </div>



                            </div>

                            <div style="height: 20px;"></div>
                        </ItemTemplate>


                    </asp:Repeater>
                </div>


            </div>


            <div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">מבוטחים/מוטבים</label>
                        </div>
                    </div>
                    <div>
                        <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/images/icons/New_Insured_Button.png" OnClick="CopyLid_Click" />
                    </div>
                </div>
                <div class="row PaddingRow MarginRow" style="width: 100%;">
                    <asp:ImageButton ID="ImageButton6" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Edit_Button_2.png" OnClick="CopyLid_Click" />
                    <asp:ImageButton ID="ImageButton7" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Delete_Button_2.png" OnClick="ShereLid_Click" />
                    <asp:ImageButton ID="ImageButton10" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Copy_Button_2.png" OnClick="ShereLid_Click" />
                    <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/images/icons/Copy_Button_2.png" OnClick="DeleteLid_Click" />
                </div>

                <div>
                    <div class="ListDivParamsHead ListDivParamsHeadS" style="padding-left: 18px;">
                        <div style="width: 5%; text-align: right;"></div>
                        <div style="width: 6%; text-align: right;"></div>
                        <div style="width: 12%; text-align: right;">ת.הקמה</div>
                        <div style="width: 12%; text-align: right;">מבוטח</div>
                        <div style="width: 12%; text-align: right;">מתוך רשומה</div>
                        <div style="width: 48%; text-align: right;">לקוח</div>
                        <div style="width: 5%; text-align: center;"></div>

                    </div>



                    <div style="height: 229px;" class="MainDivDocuments">
                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                            <ItemTemplate>
                                <div class='ListDivParams' style="position: relative;">
                                    <div style="width: 5%; text-align: center">
                                        <asp:ImageButton ID="CopyLid" runat="server" ImageUrl="~/images/icons/Arrow_Left_1.png" OnClick="CopyLid_Click" />
                                    </div>
                                    <div style="width: 48%; text-align: right;">366968541</div>
                                    <div style="width: 12%; text-align: right;">קדישמן</div>
                                    <div style="width: 12%; text-align: right">רונית</div>
                                    <div style="width: 12%; text-align: right">3.2.23</div>
                                    <div style="width: 6%; text-align: right">
                                        <%# 
                            Eval("ImageFile") + "" == "" ? 
                                "<img src=\"images/icons/User_Image_Avatar.png\" class=\"ListDivParamsImg\" style=\"border:none;\" />"
                            : //Else
                                "<img src=\"" + ConfigurationManager.AppSettings["DomainUrl"] + "Delivery/" +   Eval("ImageFile") + "\" class=\"ListDivParamsImg\" style=\"border:none;\" />"
                                        %>
                                    </div>
                                    <div style="width: 5%; text-align: right"></div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div id="PageingDiv" class="PageingDiv" runat="server"></div>
                    </div>

                </div>


            </div>

            <div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="justify-content: center; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row" style="width: 90%;">
                        <div class="row" style="margin-inline-end: 60px;">
                            <div>
                                <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                            </div>
                            <div>
                                <label class="LableBlue">היסטוריית פוליסות</label>
                            </div>
                        </div>
                        <div class="row HeaderBoxD" style="justify-content: flex-end;">
                            <div class="HeaderSearchBox">
                                <input type="text" class="InputTextSearch" value="<% = StrSrc%>" name="Q" id="Q" onblur="javascript:if(this.value==''){this.value='חפש קובץ'};" onfocus="javascript:if(this.value=='חפש קובץ'){this.value='';}" onkeypress="javascript:runSearch(event, 'Enrollment.aspx');" />
                                <a href="javascript:window.location.href = 'Enrollment.aspx?Q=' + document.getElementById('Q').value;">
                                    <img src="images/icons/Search_Pdf_Button.png" class="SearchIcon" /></a>

                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/images/icons/New_Polica_Button.png" OnClick="CopyLid_Click" />

                    </div>
                </div>
                <div class="row PaddingRow MarginRow" style="width: 100%;">
                    <asp:ImageButton ID="ImageButton11" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Mark_For_Archives_Button.png" OnClick="CopyLid_Click" />
                    <asp:ImageButton ID="ImageButton12" Style="margin-inline-end: 10px" runat="server" ImageUrl="~/images/icons/Send_Mail_Button.png" OnClick="ShereLid_Click" />
                    <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/images/icons/Send_Sms_Button.png" OnClick="DeleteLid_Click" />
                </div>

                <div class="MainDivDocuments" style="height: 102px;">
                    <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                        <ItemTemplate>
                            <div class="row SecondaryDivDocuments" runat="server">
                                <div style="width: 5%;" class="RowDocuments">
                                    <%--              <asp:CheckBox runat="server" ID="IsPromoted" />--%>
                                </div>
                                <div style="width: 2%; text-align: left;" class="RowDocuments">
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Pdf_Icon.png" OnClick="DeleteLid_Click" />
                                </div>
                                <div class="row DivBackground DivBackgroundRep">
                                    <div runat="server" style="width: 86%; text-align: right;" class="RowDocuments DivNameFile">
                                        <%--                                    <%#Eval("File") %>--%>
                                    vhh
                                    </div>
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="UploadFile" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Downlaod_Button.png" />
                                    </div>
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="ImageButton5" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Shere_Button.png" />
                                    </div>
                                </div>



                            </div>

                            <div style="height: 20px;"></div>
                        </ItemTemplate>


                    </asp:Repeater>
                </div>


            </div>

            <div class="col MarginDiv SecondaryDiv ">
                <div class="row MarginRow ServiceRequestDiv">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">מידע לצורך לקיחת הלוואה</label>
                        </div>
                    </div>

                </div>



                <div class="row MarginRow " style="width: 100%;">
                    <div class="row AlignItemsCenter" style="width: 31%; margin-left: 24%;">
                        <img src="images/Chack_Box_1_On.png" class="ImgV" style="margin-left: 10px;" runat="server" />
                        <label class="InputLable">האם קיים נכס בבעלות הלקוח</label>
                    </div>
                    <div style="width: 20%; margin-left: 2%;" class="row">
                        <label class="InputLable">בעלים:</label>
                        <lable class="ColorLable">אורית</lable>
                    </div>
                    <div class="row AlignItemsCenter" style="width: 23%;">
                        <img src="images/Chack_Box_1_On.png" class="ImgV" style="margin-left: 10px;" runat="server" />
                        <label class="InputLable">העבר לתור תפעול</label>
                    </div>    
              

                </div>


                <div class="row MarginRow" style="width: 100%;">
                    <div style="width: 20%;" class="row">
                        <lable class="InputLable">שווי הנכס:</lable>
                        <lable class="ColorLable">4 מליון</lable>
                    </div>
                    <div style="width: 35%;" class="row">
                        <lable class="InputLable">סוג הנכס:</lable>
                        <lable class="ColorLable">בית מגורים</lable>
                    </div>
                    <div style="width: 20%; margin-left: 2%;" class="row">
                        <label class="InputLable">סוג רשומה:</label>
                        <lable class="ColorLable">הצעת הלוואה</lable>
                    </div>
                    <div style="width: 23%;" class="row">
                        <label class="InputLable">סיבת אי תקינות bdi:</label>
                        <lable class="ColorLable">135256</lable>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 20%;" class="row">
                        <lable class="InputLable">כתובת הנכס:</lable>
                        <lable class="ColorLable">הבית 123</lable>
                    </div>
                    <div style="width: 35%;" class="row">
                        <label class="InputLable">נחלה:</label>
                        <lable class="ColorLable">123456</lable>
                    </div>
                </div>

               
                <div class="row MarginRow PaddingRow" style="width: 100%;">
                    <div class="row AlignItemsCenter" style="width: 31%; margin-left: 24%;">
                        <img src="images/Chack_Box_1_On.png" class="ImgV" style="margin-left: 10px;" runat="server" />
                        <label class="InputLable">האם קיימת משכנתא על נכס</label>
                    </div>
                </div>
                
   
                <div class="row MarginRow" style="width: 100%;">
                    <div style="width: 20%;" class="row">
                        <lable class="InputLable">גובה משכנתא:</lable>
                        <lable class="ColorLable">11750000</lable>
                    </div>
                    <div style="width: 35%;" class="row">
                        <lable class="InputLable">גובה החזר חודשי:</lable>
                        <lable class="ColorLable">9000</lable>
                    </div>
                    <div style="width: 43%; margin-left: 2%;" class="row">
                        <label class="InputLable">סהכ פרמיה במוצרי פרט מהר הביטוח:</label>
                        <lable class="ColorLable">1750000</lable>
                    </div>

                </div>
                <div class="row MarginRow" style="width: 100%;">
                    <div style="width: 20%;" class="row">
                        <lable class="InputLable">בנק מלווה:</lable>
                        <lable class="ColorLable">לאומי</lable>
                    </div>
                    <div style="width: 35%;" class="row">
                        <lable class="InputLable">מטרת הבדיקה:</lable>
                        <lable class="ColorLable">123456</lable>
                    </div>
                    <div style="width: 43%; margin-left: 2%;" class="row">
                        <label class="InputLable">פרמיה חודשים במוצרי פרט:</label>
                        <lable class="ColorLable">1750000</lable>
                    </div>

                </div>
                <div class="row MarginRow" style="width: 100%;">
                    <div style="width: 20%;" class="row">
                        <lable class="InputLable">גובה הלוואה מבוקש:</lable>
                        <lable class="ColorLable">354444</lable>
                    </div>
                    <div style="width: 35%;" class="row">
                        <lable class="InputLable">מטרת ההלוואה:</lable>
                        <lable class="ColorLable">לאומי</lable>
                    </div>
                    <div style="width: 43%; margin-left: 2%;" class="row">
                        <label class="InputLable">סהכ פרמיה במוצרי פרט מהר הביטוח:</label>
                        <lable class="ColorLable">1750000</lable>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <br />
    <br />


    <%--    <script type="text/javascript">MarkMenuCss('Users');</script>--%>
</asp:Content>

