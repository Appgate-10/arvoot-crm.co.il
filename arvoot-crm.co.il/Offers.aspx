<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="Offers.aspx.cs" Inherits="ControlPanel._offers" %>

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
<%--               <asp:ImageButton  ID="ExcelExport"  ImageUrl="~/images/icons/Excel_Icon.png" Style="width: 62px;height: 58px;position: fixed;left: 250px;top: 27px;" runat="server"  />--%>

            </ContentTemplate>
    </asp:UpdatePanel>
              <asp:ImageButton  ID="ExcelExport"  ImageUrl="~/images/icons/Excel_Icon.png" Style="width: 62px;height: 58px;position: fixed;left:250px;top: 27px;" runat="server" OnClick="ExcelExport_Click" />

    <div class="NewOfferDiv row">
        <label class="NewOfferLable" style="margin-inline-end: 3%;">הצעות </label>
        <%--  <div class=" HeaderBoxSearch" >--%>
        <div class="row DivSearchBox">
            <a href="javascript:window.location.href = 'Offers.aspx?Q=' + document.getElementById('Q').value;">
                <img src="images/icons/Search_Contact_User_Button.png" class="ImgSearch" /></a>
            <input type="text" class="InputTextSearch" style="text-align: right;" value="<% = StrSrc%>" name="Q" id="Q" onblur="javascript:if(this.value==''){this.value='חיפוש'};" onfocus="javascript:if(this.value=='חיפוש'){this.value='';}" onkeypress="javascript:runSearch(event, 'Offers.aspx');" />
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

                    <div style="width: 15%; text-align: right; padding-right: 2%">
                        <asp:Button OnClick="DateSort" style="background: none;border: none; font-size: 17px; color: #6F798E; font-family: 'Open Sans Hebrew';" ID="date" runat="server" Text="תאריך"/>
                    </div>
                    <div style="width: 15%; text-align: right; color: #6F798E;">שם מבוטח</div>
                    <div style="width: 10%; text-align: right; color: #6F798E;">ת.ז</div>
                    <div style="width: 10%; text-align: right; color: #6F798E;">הצעה</div>
                    <div style="width: 10%; text-align: right; color: #6F798E;">
                         <asp:DropDownList style="width:30%; color: #6F798E;" runat="server" ID="AgentList" OnSelectedIndexChanged="AgentList_SelectedIndexChanged" CssClass="StatusClaims" ToolTip="בעלים" AutoPostBack="true"></asp:DropDownList>

                    </div>
                    <div id="status" runat="server"  style="width: 20%; text-align: right;">
                                     <asp:DropDownList style="width:60%; color: #6F798E;" runat="server" ID="StatusList" OnSelectedIndexChanged="StatusList_SelectedIndexChanged" CssClass="StatusClaims" ToolTip="סטטוס" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div id="operatoring" runat="server"  style="width: 10%; text-align: right;display:none">
                          <asp:DropDownList  style="width:60%; color: #6F798E;" runat="server" ID="OperatorsList" OnSelectedIndexChanged="OperatorsList_SelectedIndexChanged" CssClass="StatusClaims" ToolTip="מתפעלת" AutoPostBack="true"></asp:DropDownList>

                    </div>
                    <div style="width: 15%; text-align: right;">
                         <asp:Button OnClick="DateSentSort" style="background: none;border: none; font-size: 17px; color: #6F798E; font-family: 'Open Sans Hebrew';" ID="Button1" runat="server" Text=" תאריך שליחה לחברת ביטוח"/>

                      </div>
                    <div style="width: 5%; text-align: center;"></div>
    </div>

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
                                    <div  runat="server" style="width: 15%; text-align: right"><%#Eval("DateSentToInsuranceCompany") %></div>
                                    <div  id="operatorVal" runat="server" style="width: 10%; text-align: right;display:none"><%#Eval("OperatorName") %></div>
                                    <div  id="statusVal" runat="server"  style="width: 20%; text-align: right"><%#Eval("StatusOffer") %></div>
                                    <div style="width: 10%; text-align: right;"><%#Eval("FullNameAgent") %></div>
                                    <div style="width: 10%; text-align: right"><%#Eval("OfferType") %></div>
                                    <div style="width: 10%; text-align: right"><%#Eval("Tz") %></div>
                                    <div style="width: 15%; text-align: right"><%#Eval("FullName") %></div>
                                    <div style="width: 15%; text-align: right; padding-right: 2%"><%#Eval("CreateDate") %></div>



                           <%--     <div style="width: 5%; text-align: center">
                                    <asp:Image ID="BtnDetailsContact" Style="vertical-align: middle;" runat="server" ImageUrl="~/images/icons/Arrow_Left_1.png"  />
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
                                </div>--%>
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

    <script>

        $(document).ready(function () {
            $("[id$=FromDate]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                showButtonPanel: false,
                dateFormat: 'dd/mm/yy',
                onClose: function (selectedDate) {
                    $("#ToDate").datepicker("option", "minDate", selectedDate);
                }
            });

            $("[id$=ToDate]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeMonth: true,
                numberOfMonths: 1,
                showButtonPanel: false,
                dateFormat: 'dd/mm/yy',
                onClose: function (selectedDate) {
                    $("#FromDate").datepicker("option", "maxDate", selectedDate);
                }

            });
        });

    </script>

    <%--    <script type="text/javascript">MarkMenuCss('Users');</script>--%>
</asp:Content>

