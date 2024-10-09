<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="LeadEdite.aspx.cs" Inherits="ControlPanel._LeadEdite" %>

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
        /*  .list label {
            display: inline-block;
            padding: 10px;
        }

        .list input[type="radio"] {
            display: none;
        }

            .list input[type="radio"] + label {
                cursor: pointer;
            }

            .list input[type="radio"]:checked + label {
                color: #0098ff;
            }
*/



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
        }

        .lblAnsFont {
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
            zoom: 75%;
            height: 0px;
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
                <asp:ImageButton ID="ExportNewContact" runat="server" ImageUrl="~/images/icons/Export_New_Contact_Button.png" OnClick="ExportNewContact_Click" OnClientClick="reload(LoadingDiv);" />
<%--                <asp:ImageButton ID="ShereLid" runat="server" ImageUrl="~/images/icons/Shere_Lid_Button.png" OnClick="ShereLid_Click" />--%>
                <asp:ImageButton ID="DeleteLid" runat="server"  OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק את הליד?');" ImageUrl="~/images/icons/Delete_Lid_Button.png"  OnClick="DeleteLid_Click" />
                <asp:Button runat="server" ID="btn_save" Text="שמור" OnClick="btn_save_Click" CssClass="BtnSave" OnClientClick="reload(LoadingDiv);" />
            </div>
            <div>
                <asp:Label ID="FormError_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
                <asp:Label ID="ExportNewContact_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="true" Style="float: left;  margin-left: 15%;"></asp:Label>
            </div>
            <div class="NewOfferDiv">
                <label class="NewOfferLable">ניהול ליד</label>
            </div>

            <div class="row MarginDiv">
                <div class="col DivDetails" style="width:100%">
                    <div class="row">
                        <div style="width: 10%; text-align: center; padding-top: 44px;">
                            <img src="images/icons/Agent_Avatar_Icon.png" id="ImageFileAgent" runat="server" />
                        </div>
                        <div class="col" style="width: 10%;">
                            <label class="LableDetails">סוכן</label>
                            <label class="PaddingAgentCus ColorLable" id="FullNameAgent" runat="server"></label>
                        </div>
                        <div class="col" style="width: 10%;">
                            <%--                            תאריך יצירת הליד--%>
                            <label class="LableDetails">תאריך</label>
                            <label class="PaddingAgentCus ColorLable" id="CreateDate" runat="server"></label>
                        </div>
                        <div class="col" style="width: 10%;">
                            <label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus ColorLable" id="PhoneAgent" runat="server"></label>
                        </div>
                        <div class="col" style="width: 10%;">
                            <label class="LableDetails">מייל</label>
                            <label class="PaddingAgentCus ColorLable" id="EmailAgent" runat="server"></label>
                        </div>
                         <div class="col" style="width: 50%;"> </div>
                    </div>
                    <div class="DivGray"></div>
                </div>
 
            </div>

            <div class="row" style="position: relative;">
                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="margin-inline-end: 2%;">
                    <div class="row MarginRow ServiceRequestDiv">
                        <label class="InputLable">פרטי הליד</label>
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
                            <label class="InputLable">מין:</label>
                            <%--<select id="SelectGender" runat="server" class="selectGlobal">
                                <option value="">בחר</option>
                                <option value="other">אחר</option>
                                <option value="male">זכר</option>
                                <option value="female">נקבה</option>
                            </select>--%>
                            <select runat="server" id="SelectGender" class="selectGlobal"></select>

                            <%--                            <asp:Button ID="BtnGender" runat="server" class="BtnGender " Text="בחר" OnCommand="Gender_Click" />--%>
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">גיל:</label>
                            <label id="Age" name="Age" type="text" runat="server" style="width: 100%;" class="InputAdd"></label>
                        </div>

                    </div>
                    <%-- <div class="row  MarginRow" style="width: 100%;">
                        <div class="row ColUpLid">
                            <div style="width: 16%; margin-right: 2%; position: absolute; background-color: white;" class=" ListSelect" id="DivRadioButtonGender" runat="server" visible="false">
                                <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonGender_SelectedIndexChanged" ID="RadioButttonGender" runat="server" CssClass="radioButtonListSmall">
                                    <asp:ListItem Text="אחר" Value="other"></asp:ListItem>
                                    <asp:ListItem Text="זכר" Value="male"></asp:ListItem>
                                    <asp:ListItem Text="נקבה" Value="female"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>--%>
                    <%--   <div class="row MarginRow GrayLine" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">תאריך לידה:</label>
                            <input id="DateBirth" name="DateBirth" type="date" onchange="CalculationAge(this)" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>--%>
                    <div class="row MarginRow" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">תאריך לידה:</label>
                            <input id="DateBirth" name="DateBirth" type="date" onchange="CalculationAge(this)" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">כתובת:</label>
                            <input id="Address" name="Address" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow GrayLine" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">מצב משפחתי:</label>
                            <%--      <select id="SelectFamilyStatus" runat="server" class="selectGlobal">
                                <option value="">בחר</option>
                                <option value="1">רווק/ה</option>
                                <option value="2">גרוש/ה</option>
                                <option value="3">נשוי/ה</option>
                                <option value="4">אלמנ/ה</option>
                            </select>--%>
                            <select runat="server" id="SelectFamilyStatus" class="selectGlobal"></select>

                        </div>

                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">תעודת זהות:</label>
                            <input id="Tz" name="Tz" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">תאריך הנפקה ת.ז.:</label>
                            <input id="IssuanceDateTz" onchange="checkDate()" name="IssuanceDateTz" type="date" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <%--  <div class="row MarginRow AlignItemsCenter" style="width: 100%;">
                        <asp:CheckBox runat="server" ID="IsValidIssuanceDateTz" />
                        <asp:Label ID="Label1" AssociatedControlID="IsValidIssuanceDateTz" runat="server" CssClass="lblAns InputLable" Text="תאריך תעודת זהות לא תקין"></asp:Label>
                    </div>
                    <div class="row MarginRow AlignItemsCenter" style="width: 100%;">
                        <asp:CheckBox runat="server" ID="IsValidBdi" />
                        <asp:Label ID="LblIsValidBdi" AssociatedControlID="IsValidBdi" runat="server" CssClass="lblAns InputLable" Text="תקין BDI"></asp:Label>
                    </div>--%>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                             <label class="InputLable">תקינות BDI</label>

                            <select runat="server" id="BdiValidity" class="selectGlobal">
                                <option value="בחר"></option>
                                <option value="BDI תקין"></option>
                                <option value="BDI לא תקין"></option>
                            </select>
                           <%-- <asp:CheckBox runat="server" ID="IsValidIssuanceDateTz" />
                            <asp:Label ID="Label1" AssociatedControlID="IsValidIssuanceDateTz" runat="server" CssClass="lblAns InputLable" Text="תאריך תעודת זהות לא תקין"></asp:Label>--%>
                            
                        </div>
                        <div class="row ColUpLid">
                             <label class="InputLable">סיבה לאי תקינות הBDI</label>
                            <input id="InvalidBdiReason" name="InvalidBdiReason" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                           <%-- <asp:CheckBox runat="server" ID="IsValidBdi" />
                            <asp:Label ID="LblIsValidBdi" AssociatedControlID="IsValidBdi" runat="server" CssClass="lblAns InputLable" Text="תקין BDI"></asp:Label>--%>
                        </div>
                    </div>
                </div>
                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="position: relative;">
                    <div class="row ServiceRequestDiv" style="margin-bottom: 12px;">
                        <lable class="InputLable">פרטי התקשרות</lable>
                        <lable class="InputLable" style="width: 30%;">סטטוס</lable>
                    </div>
                    <div class="row  MarginRow" style="width: 100%; align-items: flex-end;">
                        <div style="width: 31%; margin-inline-end: 2%;" class="row">
                            <label class="InputLable">טלפון 1:</label>
                            <input id="Phone1" name="Phone1" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row" style="width: 30%; margin-inline-end: 7%;">
                            <label class="InputLable">אימייל:</label>
                            <input id="Email" name="Email" type="text" runat="server" style="width: 100%;" class="InputAdd" />

                        </div>
                        <div style="width: 30%;" class="row ContactDetailsStatus ">
                            <select runat="server" id="SelectFirstStatus" class="selectGlobal"></select>

                            <%--                            <asp:Button ID="BtnFirstStatus" runat="server" class="buttonWithTwoImages buttonWithImages LableBlue width100" Text="בחר" OnClick="FirstStatus_Click" />--%>
                        </div>
                    </div>
                    <%--        <div class="row MarginRow" style="justify-content: end;">
                        <div style="width: 27%; position: absolute; text-align: right; height: 178px; background-color: white;" class="MainContentDiv ListSelect" id="DivRadioButtonFirstStatus" runat="server" visible="false">
                            <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonFirstStatus_SelectedIndexChanged" ID="RadioButttonFirstStatus" runat="server" CssClass="radioButtonListSmall"></asp:RadioButtonList>
                        </div>
                    </div>--%>
                    <div class="row MarginRow" style="justify-content: space-between;">
                        <div style="width: 31%; margin-inline-end: 2%;" class="row">
                            <label class="InputLable">טלפון 2:</label>
                            <input id="Phone2" name="Phone2" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>

                        <div style="width: 30%; justify-content: flex-end;" class="row">
                            <%--                            <asp:Button ID="BtnSecondStatus" runat="server" class="buttonWithOneImages buttonWithImages secondStatus LableBlue width100" OnClick="SecondStatus_Click" Text="סטטוס משני" />--%>
                            <select runat="server" id="SelectSecondStatus" class="selectGlobal"></select>
                        </div>
                    </div>
                    <div class="row  ">
                        <div style="width: 25%;" class="row">
                            <lable class="InputLable">מקור הליד</lable>
                        </div>
                        <div class="row" style="width: 20%;">
                            <lable class="InputLable">מתעניין ב</lable>
                        </div>
                        <div style="width: 21%; margin-inline-end: 4%;" class="row">
                            <lable class="InputLable">זמן מעקב</lable>
                        </div>
                        <%--    <div style="width: 30%; justify-content: flex-end;" class="row">
                            <asp:Button ID="BtnSecondStatus" runat="server" class="buttonWithOneImages buttonWithImages secondStatus LableBlue width100" OnClick="SecondStatus_Click" Text="סטטוס משני" />
                        </div>--%>
                    </div>
                    <%--   <div class="row GrayLine " style="justify-content: end;">
                        <div style="width: 27%; position: absolute; height: 201px; text-align: right; background-color: white;" class="MainContentDiv ListSelect" id="DivRadioButtonSecondStatus" runat="server" visible="false">
                            <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonSecondStatus_SelectedIndexChanged" ID="RadioButttonSecondStatus" runat="server" CssClass="radioButtonListSmall"></asp:RadioButtonList>
                        </div>
                    </div>--%>
                    <div class="row PaddingRow MarginRow">
                        <div style="width: 22%; margin-inline-end: 3%;" class="row">
                            <%--                            <asp:Button ID="BtnSourceLead" runat="server" class="BtnGender " Text="בחר" OnCommand="BtnSourceLead_Click" />--%>
                            <select runat="server" id="SelectSourceLead" class="selectGlobal"></select>
                        </div>
                        <div class="row" style="width: 16%; margin-inline-end: 4%;">
                            <input id="InterestedIn" name="InterestedIn" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div style="width: 22%; margin-inline-end: 1%;" class="row">
                            <input id="TrackingTime" name="TrackingTime" type="datetime-local" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <%--              <div class="row MarginRow">
                        <div style="width: 20%; z-index: 2; position: absolute; background-color: white;" class="ListSelect" id="DivRBSourceLead" runat="server" visible="false">
                            <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonSourceLead_SelectedIndexChanged" ID="RadioButttonSourceLead" runat="server" CssClass="radioButtonListSmall">
                                <asp:ListItem Text="ללא" Value="0"></asp:ListItem>
                                <asp:ListItem Text="פייסבוק" Value="1"></asp:ListItem>
                                <asp:ListItem Text="קמפיין גוגל" Value="2"></asp:ListItem>
                                <asp:ListItem Text="אתר" Value="3"></asp:ListItem>
                                <asp:ListItem Text="התקשר למשרד" Value="4"></asp:ListItem>
                                <asp:ListItem Text="דובי" Value="5"></asp:ListItem>
                                <asp:ListItem Text="פיננסי" Value="6"></asp:ListItem>
                                <asp:ListItem Text="קרן השתלמות" Value="7"></asp:ListItem>

                            </asp:RadioButtonList>
                    <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonSourceLead_SelectedIndexChanged" ID="RadioButttonSourceLead" runat="server" CssClass="radioButtonListSmall"></asp:RadioButtonList>

                </div>
            </div>
                    --%>
                    <div class="row">
                        <lable class="InputLable">הערות</lable>
                    </div>
                    <div class="row MarginRow">
                        <input id="Note" name="Note" type="text" runat="server" style="width: 66%;" class="InputAdd" />
                    </div>
                </div>

            </div>


            <div class="row" style="position: relative;">
                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="margin-inline-end: 2%;">
                    <div class="row MarginRow ServiceRequestDiv">
                        <label class="InputLable">פרטי העסקה</label>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">שם העסק:</label>
                            <input id="BusinessName" name="BusinessName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">ותק במקום העבודה הנוכחי:</label>
                            <input id="BusinessSeniority" name="BusinessSeniority" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">מקצוע:</label>
                            <input id="BusinessProfession" name="BusinessProfession" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">עיר בה ממקום העסק:</label>
                            <input id="BusinessCity" name="BusinessCity" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>

                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">אימייל:</label>
                            <input id="BusinessEmail" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">טלפון:</label>
                            <input id="BusinessPhone" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">שכר ברוטו:</label>
                            <input id="BusinessGrossSalary" name="BusinessGrossSalary" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">מצב תעסוקתי:</label>
                            <%--                            <asp:Button ID="BtnLineBusiness" Text="בחר" runat="server" class="BtnGender " OnCommand="LineBusiness_Click" />--%>
                            <%--     <select id="SelectLineBusiness" runat="server" class="selectGlobal">
                                <option value="">בחר</option>
                                <option value="1">מובטל/ת</option>
                                <option value="2">שכיר/ה</option>
                                <option value="3">עצמאי/ת</option>
                                <option value="4">פנסיונר/ית</option>
                            </select>--%>
                            <select runat="server" id="SelectBusinessLineBusiness" class="selectGlobal"></select>

                        </div>
                    </div>
                    <%--   <div class="row  MarginRow" style="width: 100%; justify-content: end;">
                        <div class="row ColUpLid">
                            <div style="width: 12%; margin-right: 6%; position: absolute; background-color: white;" class="ListSelect" id="DivRBLineBusiness" runat="server" visible="false">
                                <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonLineBusiness_SelectedIndexChanged" ID="RadioButtonLineBusiness" runat="server" CssClass="radioButtonListSmall">
                                    <asp:ListItem Text="מובטל/ת" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="שכיר/ה" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="עצמאי/ת" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="פנסיונר/ית" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="position: relative">
                    <div class="row MarginRow ServiceRequestDiv">
                        <label class="InputLable">פרטים אישיים מורחב</label>
                    </div>

                    <%--                    <div class="row  MarginRow" style="width: 100%;">

                        <div class="row ColUpLid">
                            <div style="width: 23.5%; margin-right: 13.5%; position: absolute; background-color: white;" class=" ListSelect" id="DivRBPartnerFamilyStatus" runat="server" visible="false">
                                <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonFamilyStatus_SelectedIndexChanged" ID="RadioButttonPartnerFamilyStatus" runat="server" CssClass="radioButtonListSmall">
                                    <asp:ListItem Text="רווק/ה" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="גרוש/ה" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="נשוי/אה" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="אלמנ/ה" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row ColUpLid">
                            <div style="width: 25%; margin-right: 12%; position: absolute; background-color: white;" class="ListSelect" id="DivRBPartnerLineBusiness" runat="server" visible="false">
                                <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonPartnerLineBusiness_SelectedIndexChanged" ID="RadioButtonPartnerLineBusiness" runat="server" CssClass="radioButtonListSmall">
                                    <asp:ListItem Text="מובטל/ת" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="שכיר/ה" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="עצמאי/ת" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="פנסיונר/ית" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>--%>

                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">שם בן/ת הזוג:</label>
                            <input id="PartnerName" name="PartnerName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">שכר ברוטו:</label>
                            <input id="PartnerGrossSalary" name="PartnerGrossSalary" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">גיל בן/ת הזוג:</label>
                            <input id="PartnerAge" name="PartnerAge" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">ותק במקום העבודה:</label>
                            <input id="PartnerSeniority" name="PartnerSeniority" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">מצב תעסוקתי:</label>
                            <%--                            <asp:Button ID="PartnerLineBusiness" Text="בחר" runat="server" class="BtnGender " OnCommand="PartnerLineBusiness_Click" />--%>
                            <%--      <select id="SelectPartnerLineBusiness" runat="server" class="selectGlobal">
                                <option value="">בחר</option>
                                <option value="1">מובטל/ת</option>
                                <option value="2">שכיר/ה</option>
                                <option value="3">עצמאי/ת</option>
                                <option value="4">פנסיונר/ית</option>
                            </select>--%>
                            <select runat="server" id="SelectPartnerLineBusiness" class="selectGlobal"></select>
                        </div>
                    </div>
                </div>

                <%--                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth">
                    <div class="row MarginRow ServiceRequestDiv">
                        <label class="InputLable">פרטים אישיים מורחב</label>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">אימייל:</label>
                            <input id="Text35" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">טלפון:</label>
                            <input id="Text36" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">עיר מגורים:</label>
                            <input id="Text37" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">רחוב מגורים:</label>
                            <input id="Text38" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>

                    </div>
                    <div class="row MarginRow GrayLine" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">בניין,קומה,דירה:</label>
                            <input id="Text39" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">מספר תא דואר:</label>
                            <input id="Text40" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">מצב משפחתי:</label>
                            <asp:Button ID="BtnFamilyStatus" runat="server" class="BtnGender " Text="בחר" OnCommand="BtnFamilyStatus_Click" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">ילדים מתחת ל-18:</label>
                            <input id="Text42" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row  MarginRow" style="width: 100%;">
                        <div class="row ColUpLid">
                            <div style="width: 11.5%; margin-right: 6.5%; position: absolute; background-color: white;" class=" ListSelect" id="DivRBFamilyStatus" runat="server" visible="false">
                                <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonFamilyStatus_SelectedIndexChanged" ID="RadioButttonFamilyStatus" runat="server" CssClass="radioButtonListSmall">
                                    <asp:ListItem Text="רווק/ה" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="גרוש/ה" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="נשוי/אה" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="אלמנ/ה" Value="3"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">שם בן/ת הזוג:</label>
                            <input id="Text43" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">גיל בן/ת הזוג:</label>
                            <input id="Text44" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                </div>--%>
            </div>
            <div class="BackgroundColorWhite MarginDiv">
                <div class="row MarginRow ServiceRequestDiv" style="padding: 0px 2%;">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">פרטי הנכס</label>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="margin-inline-end: 2%;">
                        <div class="row AlignItemsCenter MarginRow" style="width: 31%; margin-left: 24%;">
                            <asp:CheckBox runat="server" ID="CBHaveAsset" />
                            <asp:Label ID="Label2" AssociatedControlID="CBHaveAsset" runat="server" CssClass="lblAns InputLable" Text="האם קיים נכס בבעלות הלקוח"></asp:Label>
                        </div>
                        <div class="row MarginRow " style="width: 100%;">
                            <div class="row ColUpLid">
                                <label class="InputLable">שווי הנכס:</label>
                                <input id="AssetValue" name="AssetValue" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>
                            <div class="row ColUpLid">
                                <label class="InputLable">סוג הנכס:</label>
                                <input id="AssetType" name="AssetType" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>
                        </div>
                        <div class="row AlignItemsCenter MarginRow" style="width: 31%; margin-left: 24%;">
                            <asp:CheckBox runat="server" ID="CBHaveMortgageOnAsset" />
                            <asp:Label ID="Label3" AssociatedControlID="CBHaveMortgageOnAsset" runat="server" CssClass="lblAns InputLable" Text="האם קיימת משכנתא על נכס"></asp:Label>
                        </div>
                        <div class="row MarginRow" style="width: 100%;">
                            <div class="row ColUpLid">
                                <label class="InputLable">גובה משכנתא:</label>
                                <input id="MortgageAmount" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>
                            <div class="row ColUpLid">
                                <label class="InputLable">גובה החזר חודשי:</label>
                                <input id="MonthlyRepaymentAmount" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>
                        </div>
                        <div class="row MarginRow" style="width: 100%;">
                            <div class="row ColUpLid">
                                <label class="InputLable">גובה הלוואה מבוקש:</label>
                                <input id="RequestedLoanAmount" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>
                            <div class="row ColUpLid">
                                <label class="InputLable">מטרת ההלוואה:</label>
                                <input id="PurposeLoan" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>
                        </div>

                    </div>
                    <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth">
                        <div class="row MarginRow " style="width: 100%; margin-top: 50px;">
                            <div class="row ColUpLid">
                                <label class="InputLable">כתובת הנכס:</label>
                                <input id="AssetAddress" name="AssetAddress" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>

                        </div>
                        <div class="row MarginRow" style="width: 100%; margin-top: 50px;">
                            <div class="row ColUpLid">
                                <label class="InputLable">בנק מלווה:</label>
                                <input id="LendingBank" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>
                            <div class="row ColUpLid">
                                <label class="InputLable">מטרת הבדיקה:</label>
                                <input id="PurposeTest" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>
                        </div>
                        <div class="row MarginRow" style="width: 100%;">
                            <div class="row ColUpLid">
                                <label class="InputLable">יתרת משכנתא:</label>
                                <input id="MortgageBalance" type="number" runat="server" style="width: 100%;" class="InputAdd" />
                            </div>

                        </div>
                    </div>
                </div>

            </div>

            <%--   <div class="col MarginDiv SecondaryDiv">
                <div class="row" style="justify-content: space-between; width: 100%; border-bottom: 1px solid #dddddd; height: 75px; align-items: center;">
                    <div class="row">
                        <div>
                            <img src="images/icons/Duble_Arrow_Button_Blue.png" runat="server" />
                        </div>
                        <div>
                            <label class="LableBlue">מידע לצורך לקיחת הלוואה</label>
                        </div>
                    </div>
                </div>
                <div class="row PaddingRow MarginRow" style="width: 100%;">
                    <div style="width: 52%; direction: rtl; float: right;">
                        <asp:CheckBox runat="server" ID="HaveAsset" />
                        <asp:Label ID="LblHaveAsset" AssociatedControlID="HaveAsset" runat="server" CssClass="lblAns lblAnsFont" Text="האם קיים נכס בבעלות הלקוח"></asp:Label>
                    </div>
                   
                </div>
                <div class="row  MarginRow " style="width: 100%;">
                    <div style="width: 52%; direction: rtl; float: right;">
                        <asp:CheckBox runat="server" ID="HaveMortgageOnAsset" />
                        <asp:Label ID="LblHaveMortgageOnAsset" AssociatedControlID="HaveMortgageOnAsset" runat="server" CssClass="lblAns lblAnsFont" Text="האם קיימת משכנתא על נכס"></asp:Label>
                    </div>
                 
                </div>

            </div>--%>

            <div class="row MarginDiv SecondaryDiv" style="justify-content: space-between; align-items: center; height: 85px;">
                <div class="row" style="width: 20%;">
                    <label class="InputLable">תאריך שינוי סטטוס:</label>
                    <input id="DateChangeFirstStatus" name="DateChangeFirstStatus" type="date" runat="server" style="width: 100%;" class="InputAdd" disabled="disabled" />
                </div>
            </div>

            <%--<div class="row">
                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="margin-inline-end: 2%;">
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
                        <div class="row ColUpLid">
                            <label class="InputLable">תאריך שינוי סטטוס:</label>
                            <input id="Text1" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">מנורה מבטחים ליד:</label>
                            <input id="Text2" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">is active in bsl:</label>
                            <input id="Text3" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">מקור הליד:</label>
                            <input id="Text4" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>

                    </div>
                    <div class="row MarginRow GrayLine" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">תאריך טעינת ליד אחרון:</label>
                            <input id="Text5" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">סוכנות קודמת:</label>
                            <input id="Text10" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">סוג רשומה:</label>
                            <input id="Text11" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">נוצר על ידי:</label>
                            <input id="Text12" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">שם בעלים:</label>
                            <input id="Text13" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">מזהה טלפון:</label>
                            <input id="Text14" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">Ipadssn:</label>
                            <input id="Text15" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                </div>
                <div class="col MarginDiv SecondaryDiv DivDetailsCusWidth" style="position: relative">
                    <div class="row MarginRow ServiceRequestDiv">
                        <label class="InputLable">פרטים אישיים מורחב</label>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">אימייל:</label>
                            <input id="Text16" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">טלפון:</label>
                            <input id="Text17" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">עיר מגורים:</label>
                            <input id="Text18" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">רחוב מגורים:</label>
                            <input id="Text19" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>

                    </div>
                    <div class="row MarginRow GrayLine" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">בניין,קומה,דירה:</label>
                            <input id="Text20" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">מספר תא דואר:</label>
                            <input id="Text27" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row" style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">מצב משפחתי:</label>
                            <input id="Text21" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">תחום עיסוק:</label>
                            <asp:Button ID="BtnLineBusiness2" Text="בחר" runat="server" class="BtnGender " OnCommand="LineBusiness2_Click" />
                        </div>
                    </div>
                    <div class="row  MarginRow" style="width: 100%; justify-content: end;">
                        <div class="row ColUpLid">
                            <div style="width: 25%; margin-right: 12%; position: absolute; background-color: white;" class="ListSelect" id="DivRBLineBusiness2" runat="server" visible="false">
                                <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="RadioButttonLineBusiness2_SelectedIndexChanged" ID="RadioButtonLineBusiness2" runat="server" CssClass="radioButtonListSmall">
                                    <asp:ListItem Text="מובטל/ת" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="שכיר/ה" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="עצמאי/ת" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="פנסיונר/ית" Value="3"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">שם בן/ת הזוג:</label>
                            <input id="Text23" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">שכר ברוטו:</label>
                            <input id="Text24" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                    <div class="row MarginRow " style="width: 100%;">
                        <div class="row ColUpLid">
                            <label class="InputLable">גיל בן/ת הזוג:</label>
                            <input id="Text25" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                        <div class="row ColUpLid">
                            <label class="InputLable">ותק במקום העבודה:</label>
                            <input id="Text26" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                        </div>
                    </div>
                </div>

            </div>--%>

            <%--   <div class="row MarginDiv SecondaryDiv" style="justify-content: space-between; align-items: center; height: 85px;">
                <label class="InputLable">הוסף מסמכים/קבצים:</label>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/icons/Uplaod_File_Button.png" OnClick="CopyLid_Click" />
            </div>--%>


            <div id="StatusPopUp" class="popUpOut MainDivDocuments" style="display: none;" runat="server">
                <div id="Div3" class="popUpIn" style="width: 57%; height: 940px; margin-top: 160px; margin-bottom: 160px; direction: rtl; text-align: center; border-width: 2px;" runat="server">
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="CloseAddTime" OnClick="CloseAddTime_Click" />
                    <div class="col MainDivPopup" style="padding-bottom: 55px;">
                        <label class="HeaderPopup">בחירת סוג רשומת הצעה</label>
                        <label class="SecondaryHeaderPopup">בחר סוג רשומה עבור ההצעה השירות החדשה. תוכל להוסיף את הרשמה בהגדרות האשיות שלך</label>
                        <img src="images/icons/Choosing_Service_Choose_Button.png" runat="server" style="height: auto; width: 45%; margin-top: 48px;" />
                        <div class="MainDivDocuments SecondaryDivPopup ListSelect" style="height: 142px;">
                            <%--<asp:
                                List ID="ListTimes" runat="server" RepeatLayout="Table" RepeatColumns="1" Style="text-align: center; margin: auto; font-size: 16pt;">
                                <asp:ListItem Value="5"><span>אין מענה 1</span></asp:ListItem>
                                <asp:ListItem Value="10"><span>אין מענה2</span><span style="font-size:11pt;"> דק'</span></asp:ListItem>
                                <asp:ListItem Value="15"><span>אין מענה 3</span><span style="font-size:11pt;"> דק'</span></asp:ListItem>
                            </asp:RadioButtonList>--%>
                            <asp:RadioButtonList ID="rbList2" runat="server" CssClass="radioButtonListSmall">
                            </asp:RadioButtonList>
                        </div>
                        <div>
                            <asp:ImageButton ID="ImageButton5" runat="server" class="imgOkCancelPopUp" ImageUrl="images/icons/Choosing_Service_Request_Cancel_Button.png" Style="box-shadow: 0px 3px 9px 0px rgba(0, 0, 0, 0.5); border-radius: 40px;" OnClick="AddTimeOK_Click" />
                            <asp:ImageButton ID="ImageButton6" runat="server" class="imgOkCancelPopUp" ImageUrl="images/icons/Choosing_Service_Request_Ok_Button.png" OnClick="AddTimeOK_Click" />
                        </div>

                    </div>
                    <div class="DivDownPopUp">
                        <label class="LableBlue">בחירת סוגי רשומה זמינין שח בקשות שירות </label>
                        <div class="MainDivDocuments DivRepeaterPopUp">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="row DivRpwRepeaterPopUp">
                                        <div style="width: 90%; text-align: right;" class="InputLable">בדיקת תיק קטן</div>
                                        <div style="width: 10%; text-align: left;">
                                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="images/icons/DownLaod_Pdf_Button.png" OnClick="AddTimeOK_Click" />
                                        </div>
                                    </div>
                                    <div class="RowWhitePopUp"></div>
                                </ItemTemplate>


                            </asp:Repeater>
                        </div>
                    </div>


                </div>
            </div>

            <div id="Div1" class="popUpOut MainDivDocuments" visible="false" runat="server">
                <div id="Div2" class="popUpIn" style="width: 57%; height: 900px; margin-top: 160px; margin-bottom: 160px; direction: rtl; text-align: center; border-width: 2px;" runat="server">
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="ImageButton7" OnClick="CloseAddTime_Click" />
                    <div class="col MainDivPopup">
                        <label class="HeaderPopup">הגדר סטטוס להצעה</label>
                        <label class="SecondaryHeaderPopup">בחר סוג רשומה עבור ההצעה החדשה. תוכל להוסיף את הרשמה בהגדרות האשיות שלך</label>
                        <img src="images/icons/In_Treatment_Status_Button.png" runat="server" style="height: auto; width: 45%; margin-top: 48px;" />
                        <div class="MainDivDocuments SecondaryDivPopup ListSelect" style="height: 350px;">
                            <asp:RadioButtonList ID="rbList" runat="server" CssClass="radioButtonListBig"></asp:RadioButtonList>
                        </div>
                        <div>
                            <asp:ImageButton ID="ImageButton8" runat="server" class="imgOkCancelPopUp" ImageUrl="images/icons/Choosing_Service_Request_Cancel_Button.png" Style="box-shadow: 0px 3px 9px 0px rgba(0, 0, 0, 0.5); border-radius: 40px;" OnClick="AddTimeOK_Click" />
                            <asp:ImageButton ID="ImageButton9" runat="server" class="imgOkCancelPopUp" ImageUrl="images/icons/Choosing_Service_Request_Ok_Button.png" OnClick="AddTimeOK_Click" />
                        </div>
                    </div>



                </div>
            </div>
    
          

        </ContentTemplate>
    </asp:UpdatePanel>


    <br />
    <br />

    <script type="text/javascript">
        function displayPopup() {
            var popup = document.getElementById('<%=StatusPopUp.ClientID %>');
            if (popup.style.display == "none") {
                popup.style.display = "block";
            } else {
                popup.style.display = "none";
            }
        }
        function CalculationAge(dateBirth) {
            var birthDate = new Date(dateBirth.value);
            var today = new Date();
            var age = today.getFullYear() - birthDate.getFullYear();
            document.getElementById('<% = Age.ClientID%>').innerText = age;
        }
        function checkDate() {
            var issuanceDate = document.getElementById('<% =IssuanceDateTz.ClientID%>').value;
<%--            var checkBox = document.getElementById('<% =IsValidIssuanceDateTz.ClientID%>');--%>
            //var vOn = document.getElementById("VOn");
            //var vOff = document.getElementById("VOff");
            if (issuanceDate == '') {
                checkBox.checked = true; // התאריך אינו ריק
            } else {
                checkBox.checked = false; // התאריך ריק
            }
            //if (issuanceDate == '') {
            //    vOff.visible = true;// התאריך אינו ריק
            //    vOn.visible = false;
            //} else {
            //    vOff.visible = false;
            //    vOn.visible = true;
            //}
        }

    </script>
</asp:Content>


