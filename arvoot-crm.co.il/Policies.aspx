﻿<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="Policies.aspx.cs" Inherits="ControlPanel._policies" %>

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
        <label class="NewOfferLable" style="margin-inline-end: 3%;">פוליסות </label>
        <%--  <div class=" HeaderBoxSearch" >--%>
              <div class="row DivSearchBox">
            <a href="javascript:window.location.href = 'Policies.aspx?Q=' + document.getElementById('Q').value;">
                <img src="images/icons/Search_Contact_User_Button.png" class="ImgSearch" /></a>
            <input type="text" class="InputTextSearch" style="text-align: right;" value="<% = StrSrc%>" name="Q" id="Q" onblur="javascript:if(this.value==''){this.value='חיפוש'};" onfocus="javascript:if(this.value=='חפש איש קשר'){this.value='';}" onkeypress="javascript:runSearch(event, 'Policies.aspx');" />
        </div>
        <div class="row" style="padding-top:2px;  ">
            
                <div id="SortButton" style="margin-right: 60px; margin-top:4px; color:#273283">מ</div>

                <asp:TextBox runat="server" type="text" class="inputSortDate" ID="FromDate" />
                <div id="SortButton" style="margin-right: 20px; margin-top:4px; color:#273283">עד</div>


                <asp:TextBox runat="server" type="text" class="inputSortDate" ID="ToDate" />
             <div style="display: inline-flex; flex-direction: column; width: 160px;  margin-right: 30px;">
                <asp:Button ID="SortBtn" OnCommand="SortBtn_Click" Text="סנן תאריך"  runat="server" CssClass="BtnSort" />
            </div>
        </div>

        <%-- </div>--%>
    </div>
    <div class="ListDivParamsHead ListDivParamsHeadS">


           <div style="width: 20%; text-align: right; padding-right: 2%">תאריך</div>
                    <div style="width: 15%; text-align: right;">שם מבוטח</div>
                    <div style="width: 10%; text-align: right;">ת.ז</div>
                    <div style="width: 15%; text-align: right;">הצעה</div>
                    <div style="width: 15%; text-align: right;">
                         <asp:DropDownList style="width:30%" runat="server" ID="AgentList" OnSelectedIndexChanged="AgentList_SelectedIndexChanged" CssClass="StatusClaims" ToolTip="בעלים" AutoPostBack="true"></asp:DropDownList>

                    </div>
                    <div id="status" runat="server"  style="width: 20%; text-align: right;">
                                     <asp:DropDownList style="width:60%" runat="server" ID="StatusList" OnSelectedIndexChanged="StatusList_SelectedIndexChanged" CssClass="StatusClaims" ToolTip="סטטוס" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div id="operatoring" runat="server"  style="width: 10%; text-align: right;display:none">
                          <asp:DropDownList  style="width:100%" runat="server" ID="OperatorsList" OnSelectedIndexChanged="OperatorsList_SelectedIndexChanged" CssClass="StatusClaims" ToolTip="מתפעלת" AutoPostBack="true"></asp:DropDownList>

                    </div>
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
                                    <div  id="operatorVal" runat="server" style="width: 10%; text-align: right;display:none"><%#Eval("OperatorName") %></div>
                                    <div  id="statusVal" runat="server"  style="width: 20%; text-align: right"><%#Eval("StatusOffer") %></div>
                                    <div style="width: 15%; text-align: right;"><%#Eval("FullNameAgent") %></div>
                                    <div style="width: 15%; text-align: right"><%#Eval("OfferType") %></div>
                                    <div style="width: 10%; text-align: right"><%#Eval("Tz") %></div>
                                    <div style="width: 15%; text-align: right"><%#Eval("FullName") %></div>
                                    <div style="width: 20%; text-align: right; padding-right: 2%"><%#Eval("CreateDate") %></div>


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

