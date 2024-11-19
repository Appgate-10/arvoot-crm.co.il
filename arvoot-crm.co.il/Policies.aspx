<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="Policies.aspx.cs" Inherits="ControlPanel._policies" %>

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
<%--    <div class="NewOfferDiv row">
        <label class="NewOfferLable" style="margin-inline-end: 3%;">אנשי קשר </label>
        <div class="row DivSearchBox">
            <a href="javascript:window.location.href = 'Contacts.aspx?Q=' + document.getElementById('Q').value;">
                <img src="images/icons/Search_Contact_User_Button.png" class="ImgSearch" /></a>
            <input type="text" class="InputTextSearch" style="text-align: right;" value="<% = StrSrc%>" name="Q" id="Q" onblur="javascript:if(this.value==''){this.value='חפש איש קשר'};" onfocus="javascript:if(this.value=='חפש איש קשר'){this.value='';}" onkeypress="javascript:runSearch(event, 'Contacts.aspx');" />
        </div>
       
    </div>--%>
    <div class="ListDivParamsHead ListDivParamsHeadS">
<%--        <div style="width: 5%; text-align: right;"></div>
        <div style="width: 10%; text-align: right;">ת.הקמה</div>
        <div style="width: 10%; text-align: right;">שם פרטי</div>
        <div style="width: 10%; text-align: right;">שם משפחה</div>
        <div style="width: 10%; text-align: right;">ת.ז.</div>
        <div style="width: 10%; text-align: right;">טלפון</div>
        <div style="width: 10%; text-align: right;">תאריך לידה</div>
        <div style="width: 30%; text-align: right;">בעלים</div>
        <div style="width: 5%; text-align: center;"></div>
--%>

                <div style="width: 17%; text-align: right; padding-right: 4%">תאריך</div>
                    <div style="width: 15%; text-align: right;">הצעה</div>
                    <div style="width: 20%; text-align: right;">בעלים</div>
                    <div id="status" runat="server"  style="width: 43%; text-align: right;">סטטוס</div>
                    <div id="operatoring" runat="server"  style="width: 20%; text-align: right;display:none">מתפעלת</div>
                    <div style="width: 5%; text-align: center;"></div>



    </div>


    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div runat="server" id="div2">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <asp:LinkButton ID="ButtonDiv" runat="server"  CommandArgument='<%#Eval("ID") %>' OnCommand="BtnDetailsOffer_Command" CssClass="ButtonDiv" >

                            <div class='ListDivParams'>

                                  <div style="width: 5%; text-align: center">
                                    <asp:Image ID="BtnDetailsOffer" style="vertical-align: middle;" runat="server" ImageUrl="~/images/icons/Arrow_Left_1.png"  />
                                    </div>
                                    <div  id="operatorVal" runat="server" style="width: 20%; text-align: right;display:none"><%#Eval("OperatorName") %></div>
                                    <div  id="statusVal" runat="server"  style="width: 43%; text-align: right"><%#Eval("StatusOffer") %></div>
                                    <div style="width: 20%; text-align: right;"><%#Eval("FullNameAgent") %></div>
                                    <div style="width: 15%; text-align: right"><%#Eval("OfferType") %></div>
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

