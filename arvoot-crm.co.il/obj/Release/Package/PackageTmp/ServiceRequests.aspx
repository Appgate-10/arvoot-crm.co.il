<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="ServiceRequests.aspx.cs" Inherits="ControlPanel._serviceRequests" %>

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
    <div class="NewOfferDiv row">
        <label class="NewOfferLable" style="margin-inline-end: 3%;">בקשות שירות</label>
        <div class="row DivSearchBox">
            <a href="javascript:window.location.href = 'Contacts.aspx?Q=' + document.getElementById('Q').value;">
                <img src="images/icons/Search_Contact_User_Button.png" class="ImgSearch" /></a>
            <input type="text" class="InputTextSearch" style="text-align: right;" value="<% = StrSrc%>" name="Q" id="Q" onblur="javascript:if(this.value==''){this.value='חיפוש'};" onfocus="javascript:if(this.value=='חיפוש'){this.value='';}" onkeypress="javascript:runSearch(event, 'ServiceRequests.aspx');" />
        </div>
       
    </div>
<%--    <div class="ListDivParamsHead ListDivParamsHeadS">
        <div style="width: 5%; text-align: right;"></div>
        <div style="width: 10%; text-align: right;">ת.הקמה</div>
        <div style="width: 10%; text-align: right;">שם פרטי</div>
        <div style="width: 10%; text-align: right;">שם משפחה</div>
        <div style="width: 10%; text-align: right;">ת.ז.</div>
        <div style="width: 10%; text-align: right;">טלפון</div>
        <div style="width: 10%; text-align: right;">תאריך לידה</div>
        <div style="width: 30%; text-align: right;">בעלים</div>
        <div style="width: 5%; text-align: center;"></div>

    </div>--%>
        <asp:ImageButton  ID="ExcelExport"  ImageUrl="~/images/icons/Excel_Icon.png" Style="width: 62px;height: 58px;position: fixed;left:250px;top: 27px;" runat="server" OnClick="ExcelExport_Click" />

    <div class="ListDivParamsHead DivParamsHeadMargin">

                    <div style="width: 17%; text-align: right; padding-right: 4%">תאריך</div>
                    <div style="width: 15%; text-align: right;">חשבון</div>
                    <div style="width: 15%; text-align: right;">סכום כולל לגבייה</div>
                    <div style="width: 15%; text-align: right;">יתרת גבייה</div>
                    <div style="width: 17%; text-align: right;">מטרת הגבייה</div>
                    <div style="width: 16%; text-align: right;">
                           <asp:DropDownList style="width:30%; color: #6F798E;" runat="server" ID="AgentList" OnSelectedIndexChanged="AgentList_SelectedIndexChanged" CssClass="StatusClaims" ToolTip="בעלים" AutoPostBack="true"></asp:DropDownList>

                    </div>
                    <div style="width: 5%; text-align: center;"></div>

                </div>


    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div runat="server" id="div2">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <asp:LinkButton ID="BtnDetailsServiceReq" runat="server"  CommandArgument='<%#Eval("ID") %>' OnCommand="BtnDetailsServiceReq_Command" CssClass="ButtonDiv" >

                               <%-- <div class='ListDivParams'>

                                    <div style="width: 5%; text-align: center">
                                        <asp:Image ID="BtnDetailsContact" Style="vertical-align: middle;" runat="server" ImageUrl="~/images/icons/Arrow_Left_1.png" />
                                    </div>
                                    <div style="width: 30%; text-align: right"><%#Eval("FullNameAgent") %></div>
                                    <div style="width: 10%; text-align: right"><%#Eval("DateBirth") %></div>
                                    <div style="width: 10%; text-align: right;"><%#Eval("Phone1") %></div>
                                    <div style="width: 10%; text-align: right;"><%#Eval("Tz") %></div>
                                    <div style="width: 10%; text-align: right;"><%#Eval("LastName") %></div>
                                    <div style="width: 10%; text-align: right"><%#Eval("FirstName") %></div>
                                    <div style="width: 10%; text-align: right"><%#Eval("createDate") %></div>
                                    <div style="width: 5%; text-align: center">
                                         <asp:CheckBox ID="chk" runat="server" />
                                    </div>
                                </div>--%>
                            <div class='ListDivParams' style="position: relative;padding-left: 18px;">
                                    <div style="width: 5%; text-align: center">
                                    <asp:Image  ID="BtnServiceRequest" Style="vertical-align: middle;" ImageUrl="~/images/icons/Arrow_Left_1.png" runat="server" />
                                    </div>
                                      <div style="width: 16%; text-align: right"><%#Eval("AgentName") %></div>
                                      <div style="width: 17%; text-align: right"><%#Eval("PurposeName") %></div>
                                <div style="width: 15%; text-align: right;">
                                    <%#(double.Parse(Eval("Sum").ToString()) - (!string.IsNullOrWhiteSpace(Eval("paid").ToString()) ?double.Parse(Eval("paid").ToString()) : 0) + (Eval("IsApprovedCreditOrDenial").ToString() == "1" && !string.IsNullOrWhiteSpace(Eval("SumCreditOrDenial").ToString()) ? double.Parse(Eval("SumCreditOrDenial").ToString()) : 0)).ToString() %></div>
                                <div style="width: 15%; text-align: right;"><%#Eval("Sum") %></div>
                                <div style="width: 15%; text-align: right"><%#Eval("Invoice") %></div>
                                <div style="width: 17%; text-align: right; padding-right: 4%"><%#Eval("CreateDate") %></div>
                            </div>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
                <div id="PageingDiv" class="PageingDiv" runat="server"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--    </div>--%>


    <%-- </div>--%>

    <br />
    <br />


    <%--    <script type="text/javascript">MarkMenuCss('Users');</script>--%>
</asp:Content>

