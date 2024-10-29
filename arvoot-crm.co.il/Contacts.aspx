<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="ControlPanel._contacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% 
        string Title = "Contacts";
    %>
    <meta name="Description" content='<% = Title%>' />
    <meta name="keywords" content='<% = Title%>' />
    <meta name="abstract" content='<% = Title%>' />
    <meta http-equiv="title" content="<% = Title%>" />
    <title><% = Title %></title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanelButtons" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <asp:Button  ID="MoveTo" Class="NewLid"  Visible="false" Text="העבר איש קשר" Style="width: 130px; height: 35px; left:220px;" runat="server" OnClick="MoveTo_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="NewOfferDiv row">
        <label class="NewOfferLable" style="margin-inline-end: 3%;">אנשי קשר </label>
        <%--  <div class=" HeaderBoxSearch" >--%>
        <div class="row DivSearchBox">
            <a href="javascript:window.location.href = 'Contacts.aspx?Q=' + document.getElementById('Q').value;">
                <img src="images/icons/Search_Contact_User_Button.png" class="ImgSearch" /></a>
            <input type="text" class="InputTextSearch" style="text-align: right;" value="<% = StrSrc%>" name="Q" id="Q" onblur="javascript:if(this.value==''){this.value='חפש איש קשר'};" onfocus="javascript:if(this.value=='חפש איש קשר'){this.value='';}" onkeypress="javascript:runSearch(event, 'Contacts.aspx');" />
        </div>
        <%-- </div>--%>
    </div>
    <div class="ListDivParamsHead ListDivParamsHeadS">
        <div style="width: 5%; text-align: right;"></div>
        <%--<div style="width: 10%; text-align: right;"></div>--%>
        <div style="width: 10%; text-align: right;">ת.הקמה</div>
        <div style="width: 10%; text-align: right;">שם פרטי</div>
        <div style="width: 10%; text-align: right;">שם משפחה</div>
        <div style="width: 10%; text-align: right;">ת.ז.</div>
        <div style="width: 10%; text-align: right;">טלפון</div>
        <div style="width: 10%; text-align: right;">תאריך לידה</div>
        <div style="width: 30%; text-align: right;">בעלים</div>
        <div style="width: 5%; text-align: center;">

        </div>

    </div>


    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div runat="server" id="div2">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <%--<asp:LinkButton ID="ButtonDiv" runat="server"  CommandArgument='<%#Eval("ID") %>' OnCommand="BtnDetailsContact_Command" CssClass="ButtonDiv" >--%>

                            <div class='ListDivParams' style="position:relative;">

                                <asp:HiddenField ID="LeadID" Value='<%#Eval("ID") %>' runat="server"/>
                                <div style="width: 5%; text-align: center">
                                    <asp:Image ID="BtnDetailsContact" Style="vertical-align: middle;" runat="server" ImageUrl="~/images/icons/Arrow_Left_1.png"  />
                                </div>
                                <div id="AgentName" runat="server" style="width: 30%; text-align: right"><%#Eval("FullNameAgent") %></div>
                                <div style="width: 10%; text-align: right"><%#Eval("DateBirth") %></div>
                                <div style="width: 10%; text-align: right;"><%#Eval("Phone1") %></div>
                                <div style="width: 10%; text-align: right;"><%#Eval("Tz") %></div>
                                <div id="ContactLastName" runat="server" style="width: 10%; text-align: right;"><%#Eval("LastName") %></div>
                                <div id="ContactFirstName" runat="server" style="width: 10%; text-align: right"><%#Eval("FirstName") %></div>
                                <div style="width: 10%; text-align: right"><%#Eval("createDate") %></div>
                               <%-- <div style="width: 10%; text-align: right">
                                </div>--%>
                                <div style="width: 5%; text-align: center">
                                     <asp:CheckBox ID="chk" runat="server" ClientIDMode="Static" />
                                </div>
                                <asp:Button ID="ButtonDiv" runat="server"  CommandArgument='<%#Eval("ID") %>' OnCommand="BtnDetailsContact_Command" CssClass="repeaterButton" Style="width:95%;"/>
                         </div>
                        <%--</asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:Repeater>
                <div id="PageingDiv" class="PageingDiv" runat="server"></div>
            </div>
        

           
        </ContentTemplate>
    </asp:UpdatePanel>

     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="MoveContactPopUp" class="popUpOut MainDivDocuments " visible="false" runat="server">
                <div id="Div3" class="popUpIn" style="width: 57%; height: 600px; margin-top: 160px; margin-bottom: 160px; direction: rtl; text-align: center; border-width: 2px;" runat="server">
                    <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" CssClass="ImgX" ID="CloseMovePopUp" OnClick="CloseMovePopUp_Click" />
                    <div class="col MainDivPopup " style="width: 43%; margin-bottom: 30px;">
                        <label class="HeaderPopup">העבר איש קשר לסוכן חדש</label>
                        <label class="SecondaryHeaderPopup">בחר סוכן מתוך רשימת הסוכנים במערכת וסמן את אנשי הקשר להעביר לסוכן חדש</label>
                    </div>
                    <div class="col" style="padding: 0px 5%; align-items: center;">
                        <div class="row DivAgentVAll">
                            <asp:DropDownList runat="server" ID="AgentList" class="DropDownList FontWeightBold" ToolTip="סוכן"></asp:DropDownList>
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="images/icons/Forward_Leads_To_Agent_Mark_All_Button.png" style="display:none;" OnClientClick="checkAllBoxes(); return false;" />
                        </div>
                       
                        <asp:ImageButton ID="ForwardContactsToAgent" runat="server" OnClick="ForwardContactsToAgent_Click" Style="max-width:95%;" ImageUrl="~/images/icons/Forward_Leads_To_Agent_Forward_Button.png"  />
                         <asp:Label ID="FormError_lable" runat="server" Text="" CssClass="ErrorLable2" Visible="false" Style="float: left;"></asp:Label>

                        <%-- </div>--%>
                    </div>

                </div>
            </div>

            </ContentTemplate>
         </asp:UpdatePanel>
    <%--    </div>--%>


    <%-- </div>--%>

    <br />
    <br />


        <script type="text/javascript">
            function checkAllBoxes() {
                var checkboxes = document.querySelectorAll('span.itemCheckbox input[type="checkbox"]');
                for (var i = 0; i < checkboxes.length; i++) {

                    checkboxes[i].checked = true;
                }

            }
        </script>
</asp:Content>

