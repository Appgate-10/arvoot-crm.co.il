<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="ContactNo.aspx.cs" Inherits="ControlPanel._Contact" %>

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
        }

        input[type=checkbox]:checked + label::before {
            content: url('images/Chack_Box_1_On.png');
            display: inline-block;
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
        }

            input[type=checkbox]:checked {
                content: url('images/Chack_Box_1_On.png');
                display: inline-block;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="DivLidTop">
                <asp:ImageButton ID="CopyLid" runat="server" ImageUrl="~/images/icons/Copy_Lid_Button.png" OnClick="CopyLid_Click" />
                <asp:ImageButton ID="ShereLid" runat="server" ImageUrl="~/images/icons/Shere_Lid_Button.png" OnClick="ShereLid_Click" />
                <asp:ImageButton ID="DeleteLid" runat="server" ImageUrl="~/images/icons/Delete_Lid_Button.png" OnClick="DeleteLid_Click" />
                <asp:Button runat="server" ID="btn_save" Text="שמור" OnClick="btn_save_Click" CssClass="BtnSave" OnClientClick="reload(LoadingDiv);" />
            </div>
            <div>
                <asp:Label ID="FormError_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
            </div>
            <div class="NewOfferDiv" style="border-bottom: 2px solid #0f2951;">
                <label class="NewOfferLable">ניהול איש קשר</label>
            </div>

            <div class="row MarginDiv">
                <div class="col DivDetails" style="margin-inline-end: 3%;">
                    <div class="row" style="height: 113px; align-items: center;">
                        <div style="width: 20%; text-align: center;">
                            <img src="~/images/icons/User_Image_Avatar.png" style="border: 3px solid; border-radius: 40px; color: #0098ff;" runat="server" id="ImageFile" name="ImageFile" />
                        </div>
                        <div class="col" style="width: 25%;">
                            <label style="font-weight: 700; font-size: 18pt;" id="FullName" runat="server"></label>
                        </div>
                    </div>
                    <div class="DivGray"></div>
                </div>
                <div class="col DivDetails">
                    <div class="row">
                        <div style="width: 20%; text-align: center; padding-top: 44px;">
                            <img src="images/icons/Agent_Profile_Image.png" runat="server" />
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails" style="padding-bottom: 20px;">בעלים</label>
                            <label class=" ColorLable" style="font-size: 9pt;">שם סוכן</label>
                            <label class="PaddingAgentCus ColorLable" runat="server" id="FullNameAgent"></label>
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails" style="padding-bottom: 37px;">משק בית</label>
                            <label class="PaddingAgentCus ColorLable">?</label>
                        </div>
                        <div class="col" style="width: 30%;">
                            <label class="LableDetails" style="padding-bottom: 37px;">ליד מקשר</label>
                            <label class="PaddingAgentCus ColorLable">?</label>
                        </div>

                    </div>
                    <div class="DivGray"></div>
                </div>
            </div>

            <div class="col MarginDiv SecondaryDiv">
                <div class="row MarginRow ServiceRequestDiv">
                    <div style="width: 35%;" class="row">
                        <lable class="InputLable">פרטי התקשרות</lable>
                    </div>
                    <div style="width: 65%;" class="row">
                        <label class="InputLable">הערות</label>
                    </div>
                    <%--                    אין סטטוס באיש קשר --%>
                    <%--<div style="width: 19%; padding-left: 145px;" class="row ContactDetailsStatus ">
                        <label class="InputLable">סטטוס</label>
                    </div>--%>
                </div>
                <div class="row" style="width: 100%;">
                    <div class="row MarginRow " style="width: 100%;">
                        <div style="width: 35%;" class="row">
                            <img src="images/icons/Phone_Icon.png" class="ContactDetailsPM" runat="server" />
                            <lable class="InputLable ContactDetails" style="margin-left: 50px" id="PhoneH" runat="server"></lable>
                            <lable class="InputLable ContactDetails">?</lable>
                        </div>
                        <div style="width: 65%;" class="ContactDetailsNote row">
                            <%--                            <input id="Note" name="Note" type="text" runat="server" style="width: 100%;" class="ColorLable BorderNone" />--%>
                            <lable class="ColorLable" id="Note" runat="server"></lable>

                        </div>
                        <%--                    אין סטטוס באיש קשר --%>
                        <%--   <div style="width: 19%;" class="row ContactDetailsStatus ">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/In_Treatment_Status_Button.png" OnClick="CopyLid_Click" />
                        </div>--%>
                    </div>
                </div>
                <div class="row" style="width: 100%;">
                    <div class="row MarginRow " style="width: 100%;">
                        <div style="width: 100%;" class="row">
                            <img src="images/icons/Mail_Icon.png" class="ContactDetailsPM" runat="server" />
                            <lable class="InputLable ContactDetails" id="EmailH" runat="server"></lable>
                        </div>
                    </div>
                </div>


            </div>


            <div class="row" style="position: relative;">
                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="margin-inline-end: 2%;">
                    <div class="row MarginRow ServiceRequestDiv">
                        <%--<div style="width: 35%;" class="row">--%>
                        <lable class="InputLable">פרטי איש קשר</lable>
                        <%--</div>--%>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">שם פרטי:</label>
                            <input id="FirstName" name="FirstName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">שם משפחה:</label>
                            <input id="LastName" name="LastName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>

                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">תאריך לידה:</label>
                            <input id="DateBirth" name="DateBirth" type="date" onchange="CalculationAge(this)" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">גיל:</label>
                            <label id="Age" name="Age" type="text" runat="server" style="width: 100%;" class="InputAdd"></label>
                        </div>
                    </div>
                    <%--     <div class="row MarginRow GrayLine" style="width: 100%;">
                        <div class="row DivDetails ">
                            <lable class="InputLable">מין:</lable>
                            <lable class="ColorLable">זכר</lable>
                        </div>
                        <lable class="InputLable">מצב משפחתי:</lable>
                        <lable class="ColorLable">נשוי</lable>
                    </div>--%>
                    <div class="row" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">מין:</label>
                            <asp:Button ID="BtnGender" runat="server" class="BtnGender " Text="בחר" OnCommand="Gender_Click" />

                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">מצב משפחתי:</label>
                            <asp:Button ID="PartnerFamilyStatus" Text="בחר" runat="server" class="BtnGender " OnCommand="PartnerFamilyStatus_Click" />
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
                        <div class="row ColUpLid">
                            <div style="width: 12%; margin-right: 6%; position: absolute; background-color: white;" class=" ListSelect" id="DivRBPartnerFamilyStatus" runat="server" visible="false">
                                <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonFamilyStatus_SelectedIndexChanged" ID="RadioButttonPartnerFamilyStatus" runat="server" CssClass="radioButtonListSmall">
                                    <asp:ListItem Text="רווק/ה" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="גרוש/ה" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="נשוי/אה" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="אלמנ/ה" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row MarginRow GrayLine" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">תעודת זהות:</label>
                            <input id="Tz" name="Tz" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">תאריך הנפקה ת.ז.:</label>
                            <input id="IssuanceDateTz" onchange="checkDate()" name="IssuanceDateTz" type="date" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row  " style="width: 100%;">
                        <div class="row ColUpLid">
                            <lable class="InputLable">תחום עיסוק:</lable>
                            <asp:Button ID="BtnLineBusiness" Text="בחר" runat="server" class="BtnGender " OnCommand="LineBusiness_Click" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">שכר ברוטו:</label>
                            <input id="GrossSalary" name="GrossSalary" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow" style="width: 100%;">
                        <div class="row ColUpLid">
                            <div style="width: 12.5%; margin-right: 5.5%; position: absolute; background-color: white;" class="ListSelect" id="DivRBLineBusiness" runat="server" visible="false">
                                <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonLineBusiness_SelectedIndexChanged" ID="RadioButtonLineBusiness" runat="server" CssClass="radioButtonListSmall">
                                    <asp:ListItem Text="מובטל/ת" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="שכיר/ה" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="עצמאי/ת" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="פנסיונר/ית" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth">
                    <div class="row MarginRow ServiceRequestDiv">
                        <%--<div style="width: 35%;" class="row">--%>
                        <lable class="InputLable">פרטי התקשרות</lable>
                        <%--</div>--%>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <lable class="InputLable">טלפון:</lable>
                            <%--                            <lable class="ColorLable">0504156603</lable>--%>
                            <input id="Phone" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">טלפון נייד:</label>
                            <input id="dfsf" value="?" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow  GrayLine " style="width: 100%;">
                        <div class="row ColUpLid">
                            <lable class="InputLable">אימייל:</lable>
                            <input id="Email" name="Email" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">אימייל נוסף:</label>
                            <input id="Text2" value="?" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <%--<div class="row DivDetailsCus ">
                            <lable class="InputLable">עיר:</lable>
                            <lable class="ColorLable">תל אביב</lable>
                        </div>
                        <div class="row DivDetailsCus ">
                            <label class="InputLable">רחוב:</label>
                            <lable class="ColorLable">אבן גבירול </lable>
                        </div>--%>
                        <div class="row ColUpLid">
                            <label class="InputLable">כתובת:</label>
                            <input id="Address" name="Address" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <%--<div class="row DivDetailsCus ">
                            <lable class="InputLable">מספר בית:</lable>
                            <lable class="ColorLable">78</lable>
                        </div>
                        <div class="row DivDetailsCus">
                            <label class="InputLable">כניסה/קומה:</label>
                            <lable class="ColorLable">3</lable>
                        </div>--%>
                    </div>
                </div>
            </div>






            <div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row MarginRow">
                    <div class="row " style="width: 80%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">הצעות</label>
                        </div>
                    </div>
                    <div style="width: 20%; text-align: left; padding-top: 14px;">

                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/icons/New_Offer_Button.png" OnClick="CopyLid_Click" />

                    </div>
                </div>


                <%--   <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label style="color: #0098ff; font-weight: 700;">הצעות</label>
                        </div>
                    </div>
                </div>--%>

                <%--                <div id="ListDiv" runat="server">--%>
                <div class="ListDivParamsHead DivParamsHeadMargin">

                    <div style="width: 17%; text-align: right; padding-right: 4%">תאריך</div>
                    <div style="width: 15%; text-align: right;">הצעה</div>
                    <div style="width: 15%; text-align: right;">סוג רשומה</div>
                    <div style="width: 15%; text-align: right;">בעלים</div>
                    <div style="width: 33%; text-align: right;">סטטוס</div>
                    <div style="width: 5%; text-align: center;"></div>

                </div>
                <div runat="server" id="divRepeat" style="height: 463px; overflow-x: auto; margin-bottom: 20px">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <div class='ListDivParams' style="position: relative;">
                                <div style="width: 5%; text-align: center">
                                    <asp:ImageButton ID="CopyLid" runat="server" ImageUrl="~/images/icons/Arrow_Left_1.png" OnClick="CopyLid_Click" />
                                </div>
                                <div style="width: 33%; text-align: right"><%#Eval("userFullName") %></div>
                                <div style="width: 15%; text-align: right;"></div>
                                <div style="width: 15%; text-align: right;"></div>
                                <div style="width: 15%; text-align: right"></div>
                                <div style="width: 17%; text-align: right; padding-right: 4%"><%#Eval("userFullName") %></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>



                <%-- </div>--%>
            </div>


            <div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">בקשות שירות</label>
                        </div>
                    </div>
                </div>

                <div class="ListDivParamsHead DivParamsHeadMargin">

                    <div style="width: 17%; text-align: right; padding-right: 4%">תאריך</div>
                    <div style="width: 15%; text-align: right;">מספר בקשה</div>
                    <div style="width: 15%; text-align: right;">סוג רשומה</div>
                    <div style="width: 15%; text-align: right;">סכום כולל גבייה</div>
                    <div style="width: 15%; text-align: right;">יתרת גבייה</div>
                    <div style="width: 18%; text-align: right;">סטטוס</div>
                    <div style="width: 5%; text-align: center;"></div>

                </div>
                <div runat="server" id="div2" style="height: 463px; overflow-x: auto; margin-bottom: 20px">
                    <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                        <ItemTemplate>
                            <div class='ListDivParams' style="position: relative;">
                                <div style="width: 5%; text-align: center">
                                    <asp:ImageButton ID="CopyLid" runat="server" ImageUrl="~/images/icons/Arrow_Left_1.png" OnClick="CopyLid_Click" />
                                </div>
                                <div style="width: 18%; text-align: right"><%#Eval("userFullName") %></div>
                                <div style="width: 15%; text-align: right;"></div>
                                <div style="width: 15%; text-align: right;"></div>
                                <div style="width: 15%; text-align: right;"></div>
                                <div style="width: 15%; text-align: right"></div>
                                <div style="width: 17%; text-align: right; padding-right: 4%"><%#Eval("userFullName") %></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>




            </div>

            <%--<div class="col MarginDiv SecondaryDiv PaddingDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">פוליסות</label>
                        </div>
                    </div>
                </div>

                <div class="ListDivParamsHead DivParamsHeadMargin">

                    <div style="width: 30%; text-align: right; padding-right: 4%">תאריך</div>
                    <div style="width: 30%; text-align: right;">שם הפוליסה</div>
                    <div style="width: 35%; text-align: right;">תאריך נזילות</div>
                    <div style="width: 5%; text-align: center;"></div>

                </div>
                <div runat="server" id="div4" style="height: 157px; overflow-x: auto; margin-bottom: 20px">
                    <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                        <ItemTemplate>
                            <div class='ListDivParams' style="position: relative;">
                                <div style="width: 5%; text-align: center">
                                    <asp:ImageButton ID="CopyLid" runat="server" ImageUrl="~/images/icons/Arrow_Left_1.png" OnClick="CopyLid_Click" />
                                </div>
                                <div style="width: 35%; text-align: right"><%#Eval("userFullName") %></div>
                                <div style="width: 30%; text-align: right;"></div>
                                <div style="width: 30%; text-align: right;"></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>



            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>


    <br />
    <br />


    <%--    <script type="text/javascript">MarkMenuCss('Users');</script>--%>
</asp:Content>

