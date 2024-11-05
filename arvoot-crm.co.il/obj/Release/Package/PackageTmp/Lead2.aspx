<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="Lead2.aspx.cs" Inherits="ControlPanel._lead2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% 
        string Title = "Lead";
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
            margin-left: 10px;
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
            /*   color: #636e88;
            font-weight: 700;*/
        }

        input[type=checkbox] {
            visibility: hidden;
        }

        input[type=checkbox] {
            display: inline-block;
            padding: 0 0 0 0px;
            content: url('images/Chack_Box_1_Off.png');
            /*            float: right;
*/
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
                <div class="col">
                    <asp:ImageButton ID="ExportNewContact" runat="server" ImageUrl="~/images/icons/Export_New_Contact_Button.png" OnClick="ExportNewContact_Click" OnClientClick="reload(LoadingDiv);" />
                                            <asp:Label ID="ExportNewContact_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="true"></asp:Label>
                    </div>
                <asp:ImageButton ID="ShereLid" runat="server" ImageUrl="~/images/icons/Shere_Lid_Button.png" OnClick="ShereLid_Click" />
                <asp:ImageButton ID="DeleteLid" runat="server" ImageUrl="~/images/icons/Delete_Lid_Button.png" OnClick="DeleteLid_Click" />
            </div>
            <div class="NewOfferDiv">
                <label class="NewOfferLable">ניהול ליד</label>
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
                    <div class="row" style="justify-content: space-between; align-items: center; padding: 0px 2%;">
                        <%--   <div class="row" style="align-items: end;">
                            <asp:Button runat="server" class="BtnTasksOpen LableBlue" Text="לרשימת המשימות הפתוחות" OnClick="btnDateFilter_Click" />
                            <img src="images/icons/Arrow_Blue_Button.png" />
                        </div>--%>
                        <div style="position: relative;">
                            <span class="LableBlue" style="border-bottom: 1px solid;">לרשימת המשימות הפתוחות</span>
                            <img src="images/icons/Arrow_Blue_Button.png" />
                            <asp:Button runat="server" Style="width: 100%; height: 100%; cursor: pointer; border: none; background-color: transparent; top: 0px; position: absolute; right: 0px;" OnClick="btnDateFilter_Click" />
                        </div>
                        <asp:ImageButton ID="ImageButton1" Style="margin: 17px 0px;" runat="server" ImageUrl="~/images/icons/Open_Mession_Button.png" OnClick="CopyLid_Click" />

                    </div>
                    <div class="DivGray"></div>
                </div>
            </div>

            <div class="row" style="position: relative;">
                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="margin-inline-end: 2%;">
                    <div class="row MarginRow ServiceRequestDiv">
                        <lable class="InputLable">פרטי הליד</lable>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">שם פרטי:</lable>
                            <input id="FirstName" name="FirstName" type="text" runat="server" class="ColorLable BorderNone" />

                            <%--                            <asp:Label class="ColorLable" ID="FirstName" runat="server"></asp:Label>--%>
                        </div>
                        <div class="row DivDetails">
                            <label class="InputLable">שם משפחה:</label>
                            <input id="LastName" name="LastName" type="text" runat="server" class="ColorLable BorderNone" />
                        </div>
                    </div>
                    <div class="row  " style="width: 100%;">
                        <div class="row ColUpLid">
                            <%-- <lable class="InputLable">מין:</lable>
                            <lable class="ColorLable" id="Gender" runat="server"></lable>--%>


                            <label class="InputLable">מין:</label>
                            <asp:Button ID="BtnGender" runat="server" class="BtnGender " Text="בחר" OnCommand="Gender_Click" />
                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">גיל:</label>
                            <lable class="ColorLable" id="Age" runat="server"></lable>

                            <%--                            <input id="Age" name="Age" type="number" runat="server" class="ColorLable BorderNone" />--%>
                        </div>
                    </div>
                    <div class="row  MarginRow" style="width: 100%;">
                        <div class="row ColUpLid">
                            <div style="width: 16%; margin-right: 2%; position: absolute; background-color: white;" class=" ListSelect" id="DivRadioButtonGender" runat="server" visible="false">
                                <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonGender_SelectedIndexChanged" ID="RadioButttonGender" runat="server" CssClass="radioButtonListSmall">
                                    <asp:ListItem Text="אחר" Value="other"></asp:ListItem>
                                    <asp:ListItem Text="זכר" Value="male"></asp:ListItem>
                                    <asp:ListItem Text="נקבה" Value="female"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row MarginRow GrayLine" style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">תאריך לידה:</lable>
                            <input id="DateBirth" name="DateBirth" type="date" runat="server" class="ColorLable BorderNone" />
                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">כתובת:</label>
                            <input id="Address" name="Address" type="text" runat="server" class="ColorLable BorderNone" />
                        </div>
                    </div>
                    <div class="row MarginRow" style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">תעודה זהות:</lable>
                            <input id="Tz" name="Tz" type="text" runat="server" class="ColorLable BorderNone" />
                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">תאריך הנפקה ת.ז.:</label>
                            <input id="IssuanceDateTz" name="IssuanceDateTz" type="date" runat="server" class="ColorLable BorderNone" />
                        </div>
                    </div>
                    <div class="row MarginRow AlignItemsCenter" style="width: 100%;">
                        <img src="images/Chack_Box_1_On.png" class="ImgV" style="margin-left: 10px;" runat="server" />
                        <label class="LableBlue">תאריך תעודת זהות לא תקין</label>
                    </div>
                    <div class="row MarginRow AlignItemsCenter" style="width: 100%;">
                        <img src="images/Chack_Box_1_On.png" class="ImgV" style="margin-left: 10px;" runat="server" />
                        <label class="LableBlue">תקין BDI</label>
                    </div>
                </div>

                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth">
                    <div class="row MarginRow ServiceRequestDiv">
                        <lable class="InputLable">פרטי התקשרות</lable>
                        <lable class="InputLable" style="padding-left: 145px;">סטטוס</lable>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div style="width: 30%;" class="row">
                            <input id="Phone" name="Phone" type="text" runat="server" class="InputLable ContactDetails BorderNone" />
                            <%--                            <asp:Label class="InputLable ContactDetails" ID="Phone" runat="server"></asp:Label>--%>
                        </div>
                        <div class="row" style="width: 40%;">
                            <input id="Email" name="Email" type="text" runat="server" class="InputLable ContactDetails BorderNone" />
                            <%--                            <asp:Label class="InputLable ContactDetails" ID="Email" runat="server"></asp:Label>--%>
                        </div>
                        <div style="width: 30%;" class="row ContactDetailsStatus ">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/icons/In_Treatment_Status_Button.png" OnClick="CopyLid_Click" />
                        </div>
                    </div>
                    <div class="row MarginRow GrayLine">
                        <div style="width: 30%;" class="row">
                            <lable class="InputLable">מקור הליד</lable>
                        </div>
                        <div class="row" style="width: 22%;">
                            <lable class="InputLable">מתעניין ב</lable>
                        </div>
                        <div style="width: 48%;" class="row">
                            <lable class="InputLable">זמן מעקב</lable>
                        </div>
                    </div>
                    <div class="row MarginRow">
                        <div style="width: 30%;" class="row">
                            <asp:Label class="LableBlue" ID="SourceLead" runat="server"></asp:Label>
                        </div>
                        <div class="row" style="width: 22%;">
                            <%--                            <asp:Label class="LableBlue" ID="InterestedIn" runat="server"></asp:Label>--%>
                            <input id="InterestedIn" name="InterestedIn" type="text" runat="server" style="width: 100%;" class="LableBlue BorderNone" />

                        </div>
                        <div style="width: 22%;" class="row">
                            <%--                            <asp:Label class="LableBlue" ID="TrackingTime" runat="server"></asp:Label>--%>
                            <input id="TrackingTime" name="TrackingTime" type="datetime-local" runat="server" style="width: 100%;" class="LableBlue BorderNone" />

                        </div>
                    </div>
                    <div class="row MarginRow GrayLine">
                        <lable class="InputLable">הערות</lable>
                    </div>
                    <div class="row MarginRow ">
                        <%--                        <asp:Label class="ColorLable" ID="Note" runat="server"></asp:Label>--%>
                        <input id="Note" name="Note" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                    </div>
                </div>

            </div>


            <div class="col MarginDiv SecondaryDiv ">
                <div class="row MarginRow ServiceRequestDiv">
                    <label class="InputLable">פרטי העסקה</label>
                </div>
                <div class="row MarginRow" style="width: 100%;">
                    <div style="width: 23%;" class="row">
                        <lable class="InputLable">שם העסק:</lable>
                        <%--                        <asp:Label class="ColorLable" ID="BusinessName" runat="server"></asp:Label>--%>
                        <input id="BusinessName" name="BusinessName" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                    </div>
                    <div style="width: 20%;" class="row">
                        <lable class="InputLable">ותק במקום העבודה:</lable>
                        <%--                        <asp:Label class="ColorLable" ID="BusinessSeniority" runat="server"></asp:Label>--%>
                        <input id="BusinessSeniority" name="BusinessSeniority" type="number" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                    </div>
                    <div style="width: 20%; margin-left: 2%;" class="row">
                        <label class="InputLable">אימייל:</label>
                        <%--                        <asp:Label class="ColorLable" ID="BusinessEmail" runat="server"></asp:Label>--%>
                        <input id="BusinessEmail" name="BusinessEmail" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                    </div>
                    <div style="width: 35%;" class="row">
                        <label class="InputLable">טלפון:</label>
                        <%--                        <asp:Label class="ColorLable" ID="BusinessPhone" runat="server"></asp:Label>--%>
                        <input id="BusinessPhone" name="BusinessPhone" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                    </div>
                </div>
                <div class="row MarginRow" style="width: 100%;">
                    <div style="width: 23%;" class="row">
                        <lable class="InputLable">מקצוע:</lable>
                        <asp:Label class="ColorLable" ID="BusinessProfession" runat="server"></asp:Label>
                    </div>
                    <div style="width: 20%;" class="row">
                        <lable class="InputLable">עיר בה ממוקם:</lable>
                        <%--                        <asp:Label class="ColorLable" ID="BusinessCity" runat="server"></asp:Label>--%>
                        <input id="BusinessCity" name="BusinessCity" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />
                    </div>
                    <div style="width: 20%; margin-left: 2%;" class="row">
                        <label class="InputLable">שכר ברוטו:</label>
                        <input id="BusinessGrossSalary" name="BusinessGrossSalary" type="number" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                        <%--                        <asp:Label class="ColorLable" ID="BusinessGrossSalary" runat="server"></asp:Label>--%>
                    </div>
                    <div style="width: 35%;" class="row">
                        <label class="InputLable">תחום עיסוק:</label>
                        <asp:Label class="ColorLable" ID="BusinessLineBusiness" runat="server"></asp:Label>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="margin-inline-end: 2%;">
                    <div class="row MarginRow ServiceRequestDiv">
                        <%--<div style="width: 35%;" class="row">--%>
                        <lable class="InputLable">פרטים אישיים מורחב</lable>
                        <%--</div>--%>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">אימייל:</lable>
                            <%--                            <asp:Label class="ColorLable" ID="PartnerEmail" runat="server"></asp:Label>--%>
                            <input id="PartnerEmail" name="PartnerEmail" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">טלפון:</label>
                            <%--                            <asp:Label class="ColorLable" ID="PartnerPhone" runat="server"></asp:Label>--%>
                            <input id="PartnerPhone" name="PartnerPhone" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">עיר מגורים:</lable>
                            <%--                            <asp:Label class="ColorLable" ID="PartnerCity" runat="server"></asp:Label>--%>
                            <input id="PartnerCity" name="PartnerCity" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">רחוב מגורים:</label>
                            <%--                            <asp:Label class="ColorLable" ID="PartnerStreet" runat="server"></asp:Label>--%>
                            <input id="PartnerStreet" name="PartnerStreet" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">בניין,קומה,דירה:</lable>
                            <%--                            <asp:Label class="ColorLable" ID="PartnerBuildingFloorApartment" runat="server"></asp:Label>--%>
                            <input id="PartnerBuildingFloorApartment" name="PartnerBuildingFloorApartment" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">מספר ת דואר:</label>
                            <%--                            <asp:Label class="ColorLable" ID="PartnerNumMailbox" runat="server"></asp:Label>--%>
                            <input id="PartnerNumMailbox" name="PartnerNumMailbox" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />

                        </div>
                    </div>


                </div>

                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth">
                    <div class="row MarginRow ServiceRequestDiv">
                        <%--<div style="width: 35%;" class="row">--%>
                        <lable class="InputLable">פרטים אישיים מורחב</lable>
                        <%--</div>--%>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">תחום עיסוק:</lable>
                            <asp:Label class="ColorLable" ID="PartnerLineBusiness" runat="server"></asp:Label>
                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">מצב משפחתי:</label>
                            <asp:Label class="ColorLable" ID="PartnerFamilyStatus" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">שם בן/ת הזוג:</lable>
                            <asp:Label class="ColorLable" ID="PartnerName" runat="server"></asp:Label>
                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">שכר ברוטו:</label>
                            <asp:Label class="ColorLable" ID="PartnerGrossSalary" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">גיל בן/ת הזוג:</lable>
                            <asp:Label class="ColorLable" ID="PartnerAge" runat="server"></asp:Label>
                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">ותק במקום העבודה:</label>
                            <asp:Label class="ColorLable" ID="PartnerSeniority" runat="server"></asp:Label>
                        </div>
                    </div>


                </div>

            </div>



            <div class="row MarginRow " style="width: 100%; background-color: white; height: 100px; padding: 0px 2%; align-items: center;">
                <div style="width: 23%;" class="row">
                    <lable class="InputLable">איש קשר מקושר:</lable>
                    <lable class="ColorLable"></lable>
                </div>
                <div style="width: 20%;" class="row">
                    <label class="InputLable">שם איש קשר:</label>
                    <lable class="ColorLable"></lable>
                </div>
                <div style="width: 55%; margin-left: 2%;" class="row">
                    <label class="InputLable">שם משק בית:</label>
                    <lable class="ColorLable"></lable>
                </div>
            </div>

            <div class="row">
                <%--    <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="margin-inline-end: 2%;">
                    <div class="row MarginRow ServiceRequestDiv">
                        <div class="row">
                            <div>
                                <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                            </div>
                            <div>
                                <label class="LableBlue">פרטי מערכת</label>
                            </div>
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">תאריך שינוי סטטוס:</lable>
                            <lable class="ColorLable">26.5.23</lable>
                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">מנורה מבטחים ליד:</label>
                            <lable class="ColorLable">135465</lable>
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">is active in bsl:</lable>
                            <lable class="ColorLable">26.5.23</lable>
                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">מקור הליד:</label>
                            <lable class="ColorLable">קמפיין גוגל</lable>
                        </div>
                    </div>
                    <div class="row MarginRow GrayLine" style="width: 100%;">
                        <lable class="InputLable">תאריך טעינת ליד אחרון:</lable>
                        <lable class="ColorLable">06.5.23</lable>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">סוכנות קודמת:</lable>
                            <lable class="ColorLable">נאמנות</lable>
                        </div>
                        <div class="row DivDetails ">
                            <label class="InputLable">סוג רשומה:</label>
                            <lable class="ColorLable">פרטי</lable>
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">נוצר על ידי:</lable>
                            <lable class="ColorLable">נועם אביטל 12:53 25.3.23</lable>
                        </div>
                        <div class="row DivDetails">
                            <label class="InputLable">שם בעלים:</label>
                            <lable class="ColorLable">נצי הלוואות בכיר</lable>
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">מזהה טלפון:</lable>
                            <lable class="ColorLable">050415560</lable>
                        </div>
                        <div class="row DivDetails">
                            <label class="InputLable">שם Ipadssn:</label>
                            <lable class="ColorLable">0000098098</lable>
                        </div>
                    </div>
                </div>--%>

                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="margin-inline-end: 2%;">
                    <div class="row ServiceRequestDiv">
                        <label class="InputLable">היסטוריה</label>
                    </div>
                    <div class="MainDivDocuments MarginDiv" style="height: 160px; margin-top: 40px;">
                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                            <ItemTemplate>
                                <div class="row" id="Row" runat="server">
                                    <%--                                    <div style="height: 20px;"></div>--%>
                                    <div style="width: 5%; text-align: left;">
                                        <img src="images/icons/Open_Mession_Blue_Point_2.png" runat="server" id="ImgBlue" />
                                        <%--<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Open_Mession_Blue_Point_2.png" OnClick="DeleteLid_Click" />--%>
                                    </div>
                                    <div runat="server" style="width: 7%; text-align: left;" class="ColorLable ">
                                        <%--                                    <%#Eval("File") %>--%>
                                   03.8.23
                                    </div>
                                    <div class="DivGrayTasks" style="margin-right: 1%; margin-left: 1%;">
                                        <%--                                            <div style="height: 80%; width: 1px; background-color: #606b86; margin: auto;"></div>--%>
                                    </div>
                                    <div runat="server" style="width: 17%; text-align: right;" class="ColorLable ">
                                        <%--                                    <%#Eval("File") %>--%>
                                   10:36
                                    </div>
                                    <div runat="server" style="width: 69%; text-align: right;" class="ColorLable ">
                                        <img src="images/icons/Blue_Vi.png" runat="server" />
                                        <%--                                    <%#Eval("File") %>--%>
                                   נכנס ליד כפול לניד
                                    </div>
                                </div>

                            </ItemTemplate>


                        </asp:Repeater>

                    </div>

                </div>

                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth">
                    <div class="row  ServiceRequestDiv">
                        <div class="row">
                            <div>
                                <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                            </div>
                            <div>
                                <label class="LableBlue">משימות פתוחות</label>
                            </div>
                        </div>
                    </div>
                    <div class="MainDivDocuments" style="height: 226px; margin-top: 40px;">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <ItemTemplate>
                                <div class="row" id="Row" runat="server">
                                    <%--                                    <div style="height: 20px;"></div>--%>
                                    <div style="width: 8%; text-align: left;">
                                        <img src="images/icons/Open_Mession_Blue_Point_2.png" runat="server" id="ImgBlue" />
                                        <%--<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Open_Mession_Blue_Point_2.png" OnClick="DeleteLid_Click" />--%>
                                    </div>
                                    <div class="row DivBackgroundTasks" runat="server">
                                        <div runat="server" style="width: 13%; text-align: center;" class="ColorLable ">
                                            <%#Eval("Date") %>
                                        </div>
                                        <div class="DivGrayTasks" style="margin-right: 3%; margin-left: 3%;">
                                            <%--                                            <div style="height: 80%; width: 1px; background-color: #606b86; margin: auto;"></div>--%>
                                        </div>
                                        <div runat="server" style="width: 13%; text-align: right;" class="ColorLable ">
                                            <%#Eval("Time") %>
                                        </div>
                                        <div runat="server" style="width: 40%; text-align: right;" class="ColorLable ">
                                            <%#Eval("Text") %>
                                        </div>
                                        <div runat="server" style="width: 14%; text-align: right;" class="ColorLable row AlignItemsCenter">
                                            <%--                                    <%#Eval("File") %>--%>
                                            <img src="images/icons/Open_Mession_Waiting_Flug.png" runat="server" id="Img1" />
                                            <label style="color: #10b049;"><%#Eval("Status") %></label>
                                        </div>
                                        <div style="width: 7%; text-align: center;">
                                            <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                            <asp:ImageButton ID="UploadFile" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Open_Mession_Delete_Button.png" />
                                        </div>
                                        <div style="width: 7%; text-align: center;">
                                            <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                            <asp:ImageButton ID="ImageButton5" runat="server" OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Open_Mession_Edit_Button.png" />
                                        </div>
                                    </div>
                                </div>

                            </ItemTemplate>


                        </asp:Repeater>
                    </div>

                </div>

            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


    <br />
    <br />


    <%--    <script type="text/javascript">MarkMenuCss('Users');</script>--%>
</asp:Content>

