<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="ControlPanel._Notifications" %>

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
        input[type=checkbox] + label::before {
            display: inline-block;
            content: url('images/Chack_Box_1_Off.png');
            position: relative;
            vertical-align: middle;
            zoom: 75%;
        }

        input[type=checkbox]:checked + label::before {
            content: url('images/Chack_Box_1_On.png');
            display: inline-block;
            zoom: 75%;
        }

        .lblAns {
            float: right;
            text-align: right;
            /*            color: #636e88;*/
            font-weight: 700;
        }

        input[type=checkbox] {
            visibility: hidden;
        }

        input[type=checkbox] {
            display: inline-block;
            padding: 0 0 0 0px;
            content: url('images/Chack_Box_1_Off.png');
            float: right;
            zoom: 75%;
        }

            input[type=checkbox]:checked {
                content: url('images/Chack_Box_1_On.png');
                display: inline-block;
            }
    </style>
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
                <label class="NewOfferLable">בקשת שירות מספר </label>
                <label class="NewOfferLable">123456789</label>
            </div>

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
                            <label class="PaddingAgentCus ColorLable"></label>
                        </div>
                        <div class="col" style="width: 30%;">
                            <label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus ColorLable"></label>
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
                            <label class="PaddingAgentCus ColorLable"></label>
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">בעלים</label>
                            <label class="PaddingAgentCus ColorLable"></label>
                        </div>
                        <div class="col" style="width: 30%;">
                            <label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus ColorLable"></label>
                        </div>

                    </div>
                    <div class="DivGray"></div>
                </div>
            </div>

            <div class="col MarginDiv SecondaryDiv BorderDiv">
                <div class="row MarginRow ServiceRequestDiv">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">פירוט בקשת השירות</label>
                        </div>
                    </div>
                    <div class="row">
                        <div style="padding-top: 5px; margin-inline-end: 16px;">
                            <label style="font-weight: 700;">סטטוס:</label>
                        </div>
                        <div>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/In_Treatment_Status_Button.png" OnClick="CopyLid_Click" />
                        </div>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">מבוטח'מוטב:</lable>
                        <lable class="ColorLable">אורית</lable>
                    </div>
                    <div style="width: 25%; margin-left: 2%;" class="row">
                        <label class="InputLable">הצעה:</label>
                        <lable class="ColorLable">הלוואה ממנורה מבטחים</lable>
                    </div>
                    <div style="width: 16%; margin-left: 2%;" class="row">
                        <label class="InputLable">סטטוס משני:</label>
                        <lable class="ColorLable">מתלבט</lable>
                    </div>
                </div>
                <div class="row GrayLine" style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">חשבון:</lable>
                        <lable class="ColorLable">123456789</lable>
                    </div>

                </div>
                <div class="row MarginRow PaddingRow" style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <lable class="InputLable">מטרת הגבייה:</lable>
                        <lable class="ColorLable">שכר טרחה לטיפול בלוואה</lable>
                    </div>
                    <div style="width: 17%; margin-left: 3%;" class="row">
                        <label class="InputLable">סכום כולל לגבייה:</label>
                        <lable class="ColorLable">5000</lable>
                    </div>
                    <div style="width: 25%;" class="row">
                        <label class="InputLable">הערות:</label>
                        <lable class="ColorLable">זהו מקום להערות על הלקוח.ואיך רצוי להתקשר אליו</lable>
                    </div>
                </div>
                <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <label class="InputLable">פוליסה:</label>
                        <lable class="ColorLable">שם הפוליסה הוא</lable>
                    </div>
                    <div style="width: 17%; margin-left: 3%;" class="row">
                        <label class="InputLable">יתרת הגבייה:</label>
                        <lable class="ColorLable">2500</lable>
                    </div>
                </div>
            </div>
            <%-- </div>--%>




            <div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">פירוט תשלום ראשון</label>
                        </div>
                    </div>
                </div>
                <div class="row PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">סכום לתשלום ראשון:</label>
                        <lable class="ColorLable">2500</lable>
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">תאריך תשלום:</label>
                        <lable class="ColorLable">23.6.2023</lable>
                    </div>
                    <div style="width: 28%; direction: rtl; float: right;">
                        <asp:CheckBox runat="server" ID="IsPromoted" />
                        <asp:Label ID="lblIsPromoted" AssociatedControlID="IsPromoted" runat="server" CssClass="lblAns" Text=" נבדק ואושר לביצוע"></asp:Label>
                    </div>
                </div>
                <div class="row MarginRow PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">מספר תשלומים:</label>
                        <lable class="ColorLable">1</lable>
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">אסמכתא:</label>
                        <lable class="ColorLable">5646465456454</lable>
                    </div>

                </div>

            </div>

            <div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">פירוט תשלום שני</label>
                        </div>
                    </div>
                </div>
                <div class="row PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">סכום לתשלום ראשון:</label>
                        <lable class="ColorLable">2500</lable>
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">תאריך תשלום:</label>
                        <lable class="ColorLable">23.6.2023</lable>
                    </div>
                    <div style="width: 28%; direction: rtl; float: right;">
                        <asp:CheckBox runat="server" ID="CheckBox1" />
                        <asp:Label ID="Label1" AssociatedControlID="IsPromoted" runat="server" CssClass="lblAns" Text=" נבדק ואושר לביצוע"></asp:Label>
                    </div>
                </div>
                <div class="row MarginRow PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">מספר תשלומים:</label>
                        <lable class="ColorLable">1</lable>
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">אסמכתא:</label>
                        <lable class="ColorLable">5646465456454</lable>
                    </div>

                </div>

            </div>

            <div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row">
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

                <div class="MainDivDocuments" style="height: 290px;">
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
                                    <div runat="server" style="width: 90%; text-align: right;" class="RowDocuments DivNameFile">
                                        <%--                                    <%#Eval("File") %>--%>
                                    vhh
                                    </div>
                                <%--    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <asp:ImageButton ID="ImageButton5" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Shere_Button.png" />
                                    </div>--%>
                                    <div style="width: 10%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="UploadFile" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Downlaod_Button.png" />
                                    </div>
                                </div>



                            </div>

                            <div style="height: 20px;"></div>
                        </ItemTemplate>


                    </asp:Repeater>
                </div>


            </div>



        </ContentTemplate>
    </asp:UpdatePanel>


    <br />
    <br />


    <%--    <script type="text/javascript">MarkMenuCss('Users');</script>--%>
</asp:Content>

