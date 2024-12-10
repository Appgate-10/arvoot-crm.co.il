<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="ServiceRequestEdit.aspx.cs" Inherits="ControlPanel._serviceRequestEdit" %>

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
            text-align: right;
            color: #636e88;
            font-weight: 700;
        }
      /*  input[type=checkbox] + label::before {
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
            }*/
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
                <asp:ImageButton ID="DeleteLid" runat="server" OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק את הבקשה?');" ImageUrl="~/images/icons/Delete_Lid_Button.png" OnClick="DeleteService_Click" />

                <asp:Button runat="server" ID="btn_save" Text="שמור" OnClick="btn_save_Click" Style="width: 110px; height: 35px;" CssClass="BtnSave" OnClientClick="reload(LoadingDiv);" />

            </div>
            <div>
                <asp:Label ID="FormError_label" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
            </div>
            <div class="NewOfferDiv">
                <label class="NewOfferLable">בקשת שירות חדשה</label>
            </div>

            <div class="row MarginDiv">
                <div class="col DivDetails" style="width:100%;">
                    <div class="row">
                        <div style="width: 12%; text-align: center; padding-top: 44px;">
                            <img src="images/icons/Agent_Avatar_Icon.png" runat="server" />
                        </div>
                        <div class="col" style="width: 12%;">
                            <label class="LableDetails">סוכנות</label>
                            <label id="lblAgency" runat="server" class="PaddingAgentCus"></label>
                        </div>
                        <div class="col" style="width: 12%;">
                            <label class="LableDetails">בעלים</label>
                            <label id="lblOwner" runat="server" class="PaddingAgentCus"></label>
                        </div>
                        <div class="col" style="width: 14%;">
                        <%--    <label class="LableDetails">טלפון</label>
                            <label class="PaddingAgentCus"></label>--%>
                        </div>
                        <div  style="width: 50%;">

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
                              <asp:HiddenField  id="OfferID" runat="server"></asp:HiddenField>
                              <asp:HiddenField  id="ContactID" runat="server"></asp:HiddenField>

<%--                            <label class="LableBlue">עריכת בקשת שירות</label>--%>
                        </div>
                    </div>
                    <div class="row">
                      
                    </div>
                </div>
                <div class="row " style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <div style="width: 132px;">
                            <lable class="FontWeightBold">שם איש קשר:</lable>
                        </div>
                        <div style="width: 100%;">
                            <asp:Button OnClick="OpenContact_Click"  ID="FullName" runat="server" name="FullName" type="text" 
                                style="width: 100%; border-bottom: 0px;background: none;text-align: right;font-size: medium;" class="InputAdd" />
                        </div>
                    </div>
                    <div style="width: 17%; margin-left: 10%;" class="row">
                        <label class="InputLable">הצעה:</label>
                        <asp:Button id="OfferName"  OnClick="OpenOffer_Click"  name="FullName" type="text" 
                            runat="server" style="width: 100%; border-bottom: 0px;background: none;text-align: right;font-size: medium;" class="InputAdd" />
                    </div>
                    <div style="width: 18%">
                    </div>
                </div>
          <%--      <div class="row GrayLine" style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%;" class="row">
                        <div style="width: 132px;">
                            <lable class="FontWeightBold">חשבון:</lable>
                        </div>
                        <div style="width: 100%;">
                            <input id="Invoice" name="FullName" type="text" style="width: 100%;" runat="server" class="InputAdd" />
                        </div>
                    </div>

                </div>--%>
                <div class="row PaddingRow" style="width: 100%;">
                    <div style="width: 31%; margin-left: 24%" class="row">
                        <div style="width: 132px;">
                            <lable class="FontWeightBold">
                            מטרת הגבייה:</span>
                        </div>
                        <div style="width: 100%;">
                            <select  runat="server" OnSelectedIndexChanged="RadioButttonFamilyStatus_SelectedIndexChanged" ID="SelectPurpose" class="selectGlobal"></select>                       

                        </div>
                    </div>
                    <div style="width: 17%; margin-left: 3%;" class="row">
                        <label class="InputLable">סכום כולל לגבייה:</label>
                             <span class="input-symbol-shekel">
                        <input id="AllSum" name="FullName" type="number" min="0" runat="server" style="width: 85%;" class="InputAdd" />
                                 </span>
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
                         <div style="width: 100%;" class="row">
                        <label class="InputLable">הערות:</label>
                        <input id="Note" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    </div>
                    <div style="width: 20%; margin-left: 3%;" class="row">
                        <label class="InputLable">יתרת הגבייה:</label>
<%--                        <input id="Balance" name="FullName" type="number" runat="server" style="width: 100%;" class="InputAdd" />--%>
                        <span class="Lable-symbol-shekel"  style="width: 40%;">
                             <label id="Balance" name="Balance" runat="server" style="width: 73%; display:flex" class="InputAdd"></label>
                        </span>

                        <button id="btnReloadBalance" runat="server" onserverclick="btnReloadBalance_ServerClick" style="margin-right: 5px; width: 23px; background-color: transparent; border: 1px solid black; border-radius: 4px;">
                            <i class="fa-solid fa-rotate-right"></i>
                        </button>
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
                             <span class="input-symbol-shekel">
                        <input id="Sum1" name="FullName" type="number" min="0" runat="server" style="width: 85%;" class="InputAdd" value='<%# Eval("SumPayment").ToString() == "0" ? "" : Eval("SumPayment").ToString() %>'/>
                                 </span>
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">תאריך תשלום:</label>
                        <input id="DatePayment1" name="DatePayment1" type="date" runat="server" style="width: 100%;" class="InputAdd" value='<%# Eval("DatePayment").ToString() %>' />
                    </div>
                    <div id="divIsApproved" runat="server"  style="width: 28%; direction: rtl; float: right;">
                        <asp:CheckBox runat="server" ID="IsApprove1" Checked='<%# Boolean.Parse(Eval("IsApprovedPayment").ToString()) %>' AutoPostBack="true" OnCheckedChanged="IsApprove1_CheckedChanged" />
                        <asp:Label ID="lblIsApprove1" AssociatedControlID="IsApprove1" runat="server" CssClass="lblAns" Text="נבדק ואושר לביצוע"></asp:Label>
                    </div>
                </div>
                <div class="row MarginRow PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">מספר תשלומים:</label>
                        <input id="Num1" name="FullName" type="number" min="0" runat="server" style="width: 100%;" class="InputAdd" value='<%#Eval("NumPayment").ToString() == "0" ? "" : Eval("NumPayment").ToString() %>' />
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">אסמכתא:</label>
                        <input id="ReferencePayment1" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" value='<%# Eval("ReferencePayment").ToString() %>'/>
                    </div> 
                    <div style="width: 28%;" class="row">
                         <asp:Label ID="ErrorCheckBox" Style="color:red;font-weight:400" runat="server" CssClass="InputLable" Text=""></asp:Label>

                    </div>
                </div>
                        <asp:HiddenField ID="hiddenPaymentID" runat="server" Value='<%# Eval("ID").ToString() %>'/>
            </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Button ID="AddPayment" runat="server" CssClass="btnBlue" Style="float: right; margin-bottom: 38px;" OnClick="AddPayment_Click" Text="+ הוספת תשלום"/>


            <div class="col SecondaryDiv MarginDiv" style="width:100%;">
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
                             <span class="input-symbol-shekel">
                        <input id="Sum4" name="FullName" type="number" min="0" runat="server" style="width: 90%;" class="InputAdd" />
                                 </span>
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">תאריך תשלום:</label>
                        <input id="DateCreditOrDenial" name="DateCreditOrDenial" type="date" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div id="divIsApproved2" runat="server" style="width: 28%; direction: rtl; float: right;">
                        <asp:CheckBox runat="server" ID="IsApprove4" AutoPostBack="true" OnCheckedChanged="IsApprove4_CheckedChanged" />
                        <asp:Label ID="lblIsApprove4" AssociatedControlID="IsApprove4" runat="server" CssClass="lblAns" Text=" נבדק ואושר לביצוע"></asp:Label>
                        
                    </div>
                </div>
                <div class="row PaddingRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                        <label class="InputLable">מספר תשלומים:</label>
                        <input id="Num4" name="FullName" type="number" min="0" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 15%; margin-left: 35%;" class="row">
                        <label class="InputLable">אסמכתא:</label>
                        <input id="ReferenceCreditOrDenial" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                      <div id="div1" runat="server" style="width: 28%; direction: rtl; float: right;">
                        <asp:Label ID="ErrorCheckBox" Style="color:red;font-weight:400" runat="server" CssClass="InputLable" Text=""></asp:Label>
                        
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
                        <select  runat="server"  Style="height: 100%;"  ID="SelectMethodsPayment" class="selectGlobal"></select>                       
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
                        <select  runat="server"  ID="SelectYear"  class="selectGlobal"></select>                       
                        <select  runat="server"  ID="SelectMonth" style="margin-right:25px" class="selectGlobal"></select>                       

<%--                        <input id="CreditValidity" name="FullName" type="date" runat="server" style="width: 100%;" class="InputAdd" />--%>
                    </div>
                </div>
                <div class="row PaddingRow " style="width: 100%;">
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
                    <div class="row PaddingRow MarginRow" style="width: 100%;">
                    <div style="width: 18%; margin-left: 3%;" class="row">
                    </div>
                    <div style="width: 20%; margin-left: 5%;" class="row">
                        <label class="InputLable">בעל החשבון:</label>
                        <input id="AccountHolder" name="FullName" type="text" runat="server" style="width: 100%;" class="InputAdd" />
                    </div>
                    <div style="width: 20%; margin-left: 34%;" class="row">
                       
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
                            <div class="row SecondaryDivDocuments" runat="server">
                                <div style="width: 2%;" class="RowDocuments">
                                    <asp:CheckBox runat="server" Style="vertical-align:middle" AutoPostBack="true" ID="IsPromoted" />
                                </div>
                                <div style="width: 2%; text-align: left;" class="RowDocuments">
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Pdf_Icon.png" OnClick="DeleteLid_Click" />
                                </div>
                                <div class="row DivBackground DivBackgroundRep">
                                    <div runat="server" style="width: 86%; text-align: right;" class="RowDocuments DivNameFile">
                                        
                                             <asp:Label ID="FileName" Text='<%# Eval("FileName") %>' runat="server"/> 
                                 
                                    </div>
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="UploadFile" runat="server" OnCommand="UploadFile_Command" CommandArgument='<%# Eval("FileName") + "," + Eval("ID") %>' Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Choosing_New_Service_Request_Downlaod_Button.png" />
                                    </div>       
                                    <div style="width: 7%; text-align: center;" class="RowDocuments">
                                        <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                        <asp:ImageButton ID="RemoveFile" runat="server" OnCommand="RemoveFile_Command" CommandArgument='<%# +  Container.ItemIndex + "," + Eval("ID")   %>' Style="vertical-align: middle; position: relative; height:70%" ImageUrl="~/images/icons/Delete_Lid_Button.png" />
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
                <div style="width: 20%; text-align: left; padding-top: 14px;">

                    <%--                      <asp:ImageButton ID="ImageButton1" Style="width:250px;height:auto" runat="server" ImageUrl="~/images/icons/Choosing_Service_New_Service_Button.png" OnClick="ServiceRequestAdd_Click" />--%>
                </div>


            </div>
            <div class="DivLidTop">
  

                <asp:Button runat="server" ID="BtnSaveBottom" Text="שמור" OnClick="btn_save_Click" Style="width: 110px; height: 35px;" CssClass="BtnSave" OnClientClick="reload(LoadingDiv);" />

            </div>
            <div>
                <asp:Label ID="FormErrorBottom_label" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>
            </div>

<%--            <asp:ImageButton ID="ImageButton2" runat="server" Style="margin-bottom: 50px; float: right; margin-top: 38px;" ImageUrl="~/images/icons/Choosing_Service_New_Service_Button.png" OnClick="CopyLid_Click" />--%>


      




        </ContentTemplate>
    </asp:UpdatePanel>
         <input type="text" runat="server" name="ImageFile" id="ImageFile" style="display: none" />
      <asp:FileUpload ID="ImageFile_FileUpload" runat="server" onchange="ImageFile_UploadFile(this)" AllowMultiple="true" Style="display: none" />
      <asp:Button ID="ImageFile_btnUpload" Text="2" runat="server" OnClick="ImageFile_btnUpload_Click" Style="display: none" />

    <br />
    <br />


    <%--    <script type="text/javascript">MarkMenuCss('Users');</script>--%>
</asp:Content>

