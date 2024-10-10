<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="ServiceRequestAdd.aspx.cs" Inherits="ControlPanel._serviceRequestAdd" %>

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
            color: #636e88;
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
                zoom: 75%;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>


            <div class="DivLidTop">
                <%--          <asp:ImageButton ID="CopyLid" runat="server" ImageUrl="~/images/icons/Copy_Lid_Button.png" OnClick="CopyLid_Click" />
                <asp:ImageButton ID="ShereLid" runat="server" ImageUrl="~/images/icons/Shere_Lid_Button.png" OnClick="ShereLid_Click" />
                <asp:ImageButton ID="DeleteLid" runat="server" ImageUrl="~/images/icons/Delete_Lid_Button.png" OnClick="DeleteLid_Click" />--%>
                <asp:Button runat="server" ID="btn_save" Text="שמור" OnClick="btn_save_Click" Style="width: 110px; height: 35px;" CssClass="BtnSave" OnClientClick="reload(LoadingDiv);" />

            </div>
            <div>
                <asp:Label ID="FormError_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
            </div>
            <div class="NewOfferDiv">
                <label class="NewOfferLable">בקשת שירות חדשה</label>
            </div>

            <div class="row MarginDiv">
                <div class="col DivDetails" style="margin-inline-end: 3%;">
                    <div class="row">
                        <div style="width: 20%; text-align: center; padding-top: 44px;">
                            <img src="images/icons/Agent_Avatar_Icon.png" runat="server" />
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">סוכנות</label>
                            <label class="PaddingAgentCus">Lisanee</label>
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">בעלים</label>
                            <label class="PaddingAgentCus">Lisanee</label>
                        </div>
                        <div class="col" style="width: 30%;">
                            <label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus">Lisanee</label>
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
                            <label class="PaddingAgentCus">Lisanee</label>
                        </div>
                        <div class="col" style="width: 25%;">
                            <label class="LableDetails">בעלים</label>
                            <label class="PaddingAgentCus">Lisanee</label>
                        </div>
                        <div class="col" style="width: 30%;">
                            <label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus">Lisanee</label>
                        </div>

                    </div>
                    <div class="DivGray"></div>
                </div>
            </div>

            <div class="col MarginDiv SecondaryDiv" style="position: relative;">
                <div class="row MarginRow ServiceRequestDiv">
                    <div class="row" style="align-items: center;">
                        <div class="div-arrows-img">
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <%--                            <label class="LableBlue">עריכת בקשת שירות</label>--%>
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
                        <div style="width: 132px;">
                            <lable class="FontWeightBold">מבוטח'מוטב:</lable>
                        </div>
                        <div style="width: 100%;">
                            <asp:Label ID="FullName" runat="server" name="FullName" type="text" Style="width: 100%; border-bottom: 0px;" runat="server" class="InputAdd" />
                        </div>
                    </div>
                    <div style="width: 17%; margin-left: 10%;" class="row">
                        <label class="InputLable">הצעה:</label>
                        <input id="Text2" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 18%">
                        <label style="font-weight: 700;">סטטוס משני:</label>
                    </div>
                </div>
                <div class="row GrayLine" style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%;" class="row">
                        <div style="width: 132px;">
                            <lable class="FontWeightBold">חשבון:</lable>
                        </div>
                        <div style="width: 100%;">
                            <input id="Invoice" name="FullName" type="text" style="width: 100%;" runat="server" class="InputAdd" />
                        </div>
                    </div>

                </div>
                <div class="row PaddingRow" style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <div style="width: 132px;">
                            <lable class="FontWeightBold">
                            מטרת הגבייה:</span>
                        </div>
                        <div style="width: 100%;">
                            <select runat="server" onselectedindexchanged="RadioButttonFamilyStatus_SelectedIndexChanged" id="SelectPurpose" class="selectGlobal"></select>

                        </div>
                    </div>
                    <div style="width: 17%; margin-left: 3%;" class="row">
                        <label class="InputLable">סכום כולל לגבייה:</label>
                        <input id="AllSum" name="FullName" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 25%;" class="row">
                        <label class="InputLable">הערות:</label>
                        <input id="Note" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                </div>

                <div class="row  MarginRow" style="width: 100%;">
                    <div class="row ColUpLid">

                        <%--
                        <div style="width: 23.3%; margin-right: 6.5%; height: 50px; position: absolute; background-color: white;" class="MainDivDocuments ListSelect" id="DivRBFamilyStatus" runat="server" visible="false">
                          <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonFamilyStatus_SelectedIndexChanged" ID="RadioButttonFamilyStatus" runat="server" CssClass="radioButtonListSmall">
                                <asp:ListItem Text="הלוואה" Value="0"></asp:ListItem>
                                <asp:ListItem Text="פדיון" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>

                        </div>--%>
                    </div>
                </div>

                <div class="row MarginRow " style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <div style="width: 132px;">
                            <lable class="FontWeightBold">פוליסה:</lable>
                        </div>
                        <div style="width: 100%;">
                            <input id="Policy" name="FullName" type="text" style="width: 100%;" runat="server" class="InputAdd" />
                        </div>
                    </div>
                    <div style="width: 20%; margin-left: 3%;" class="row">
                        <label class="InputLable">יתרת הגבייה:</label>
                        <label id="Balance" name="Balance" runat="server" style="width: 40%;" class="InputAdd"></label>
                        <button id="btnReloadBalance" runat="server" onserverclick="btnReloadBalance_ServerClick" style="margin-right: 5px; width: 23px; background-color: transparent; border: 1px solid black; border-radius: 4px;">
                            <i class="fa-solid fa-rotate-right"></i>
                        </button>

                        <%--<input id="Balance" name="FullName" type="number" runat="server" style="width: 100%;" class="InputAdd" />--%>
                    </div>
                </div>
            </div>
            <%-- </div>--%>



            <asp:Repeater ID="RepeaterPayments" runat="server" OnItemDataBound="RepeaterPayments_ItemDataBound">
                <ItemTemplate>
                    <div class="col MarginDiv SecondaryDiv">
                        <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                            <div class="row" style="align-items: center;">
                                <div class="div-arrows-img">
                                    <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                                </div>
                                <div>
                                    <label id="paymentTitle" runat="server" class="LableBlue">פירוט תשלום ראשון</label>
                                </div>
                            </div>
                        </div>

                        <div class="row PaddingRow" style="width: 100%;">
                            <div style="width: 18%; margin-left: 3%;" class="row">
                                <label id="sumTitle" runat="server" class="InputLable">סכום לתשלום ראשון:</label>
                                <input id="Sum1" name="FullName" type="number" runat="server" style="width: 100%;" class="InputAdd" value='<%# Eval("SumPayment").ToString() == "0" ? "" : Eval("SumPayment").ToString() %>' />
                            </div>
                            <div style="width: 15%; margin-left: 35%;" class="row">
                                <label class="InputLable">תאריך תשלום:</label>
                                <input id="DatePayment1" name="DatePayment1" type="date" runat="server" style="width: 100%;" class="InputAdd" value='<%# Eval("DatePayment").ToString() %>' />
                            </div>
                            <div style="width: 28%; direction: rtl; float: right;">
                                <asp:CheckBox runat="server" ID="IsApprove1" Checked='<%# Eval("IsApprovedPayment").ToString() == "1" ? true : false %>' AutoPostBack="true" OnCheckedChanged="IsApprove1_CheckedChanged" />
                                <asp:Label ID="lblIsApprove1" AssociatedControlID="IsApprove1" runat="server" CssClass="lblAns" Text=" נבדק ואושר לביצוע"></asp:Label>
                            </div>
                        </div>
                        <div class="row MarginRow PaddingRow" style="width: 100%;">
                            <div style="width: 18%; margin-left: 3%;" class="row">
                                <label class="InputLable">מספר תשלומים:</label>
                                <input id="Num1" name="FullName" type="number" runat="server" style="width: 100%;" class="InputAdd" value='<%#Eval("NumPayment").ToString() == "0" ? "" : Eval("NumPayment").ToString() %>' />
                            </div>
                            <div style="width: 15%; margin-left: 35%;" class="row">
                                <label class="InputLable">אסמכתא:</label>
                                <input id="ReferencePayment1" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" value='<%# Eval("ReferencePayment").ToString() %>' />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Button ID="AddPayment" runat="server" CssClass="btnBlue" Style="float: right; margin-bottom: 38px;" OnClick="AddPayment_Click" Text="+ הוספת תשלום" />
            <div class="col SecondaryDiv MarginDiv" style="width: 100%;">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row" style="align-items: center;">
                        <div class="div-arrows-img">
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">זיכוי/הכחשת עסקה</label>
                        </div>
                    </div>
                </div>
                <div class="row PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">סכום:</label>
                        <input id="Sum4" name="FullName" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">תאריך תשלום:</label>
                        <input id="DateCreditOrDenial" name="DateCreditOrDenial" type="date" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 28%; direction: rtl; float: right;">
                        <asp:CheckBox runat="server" ID="IsApprove4" AutoPostBack="true" OnCheckedChanged="IsApprove4_CheckedChanged" />
                        <asp:Label ID="lblIsApprove4" AssociatedControlID="IsApprove4" runat="server" CssClass="lblAns" Text=" נבדק ואושר לביצוע"></asp:Label>
                    </div>
                </div>
                <div class="row PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">מספר תשלומים:</label>
                        <input id="Num4" name="FullName" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">אסמכתא:</label>
                        <input id="ReferenceCreditOrDenial" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>

                </div>
                <div class="row MarginRow PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">סיבה/הערות:</label>
                        <input id="NoteCreditOrDenial" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                </div>
            </div>

            <%--            <div class="col SecondaryDiv MarginDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">פירוט זיכוי/הכחשת עסקה</label>
                        </div>
                    </div>
                </div>
                <div class="row PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">סכום:</label>
                        <input id="Text16" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">תאריך:</label>
                        <input id="Text17" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 28%; direction: rtl; float: right;">
                        <asp:CheckBox runat="server" ID="CheckBox2" />
                        <asp:Label ID="Label2" AssociatedControlID="IsPromoted" runat="server" CssClass="lblAns" Text=" נבדק ואושר לביצוע"></asp:Label>
                    </div>
                </div>
                <div class="row MarginRow PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">סיבה/הערות:</label>
                        <input id="Text18" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                </div>

            </div>--%>

            <div class="col SecondaryDiv ">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row" style="align-items: center;">
                        <div class="div-arrows-img">
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">אמצעי תשלום</label>
                        </div>
                    </div>
                </div>
                <div class="row PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <select runat="server" style="height: 100%;" id="SelectMethodsPayment" class="selectGlobal"></select>
                        <%--                       OnSelectedIndexChanged="RadioButttonFamilyStatus_SelectedIndexChanged"
                        <asp:Button ID="BtnMethodsPayment" runat="server" Style="height: 100%;" class="BtnGender " Text="בחר אמצעי תשלום" OnCommand="BtnMethodsPayment_Click" />--%>
                    </div>
                    <div style="width: 20%; margin-left: 5%;" class="row">
                        <label class="InputLable">שם בנק:</label>
                        <input id="BankName" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 20%; margin-left: 34%;" class="row">
                        <label class="InputLable">מספר אשראי:</label>
                        <input id="CreditNumber" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                </div>

                <div class="row  MarginRow" style="width: 100%;">
                    <div class="row ColUpLid">
                        <div style="width: 23.3%; margin-right: 6.5%; height: 50px; position: absolute; background-color: white;" class="MainDivDocuments ListSelect" id="DivRBMethodsPayment" runat="server" visible="true">
                            <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonMethodsPayment_SelectedIndexChanged" ID="RadioButttonMethodsPayment" runat="server" CssClass="radioButtonListSmall">
                                <asp:ListItem Text="צ'ק" Value="0"></asp:ListItem>
                                <asp:ListItem Text="אשראי" Value="1"></asp:ListItem>
                                <asp:ListItem Text="העברה בנקאית" Value="2"></asp:ListItem>
                                <asp:ListItem Text="שולם מזומן" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
                <div class="row" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                    </div>
                    <div style="width: 20%; margin-left: 5%;" class="row">
                        <label class="InputLable">סניף:</label>
                        <input id="Branch" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 20%; margin-left: 34%;" class="row">
                        <label class="InputLable">תוקף:</label>
                        <input id="CreditValidity" name="FullName" type="date" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                </div>
                <div class="row PaddingRow MarginRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                    </div>
                    <div style="width: 20%; margin-left: 5%;" class="row">
                        <label class="InputLable">מספר חשבון:</label>
                        <input id="AccountNumber" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 20%; margin-left: 34%;" class="row">
                        <label class="InputLable">ת.ז. בעל הכרטיס:</label>
                        <input id="CardholdersID" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                </div>
            </div>

            <%--            <asp:ImageButton ID="ImageButton2" runat="server" Style="margin-bottom: 50px; float: right; margin-top: 38px;" ImageUrl="~/images/icons/Choosing_Service_New_Service_Button.png" OnClick="CopyLid_Click" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>


    <br />
    <br />


    <script type="text/javascript">
            //MarkMenuCss('Users');


    </script>
</asp:Content>

