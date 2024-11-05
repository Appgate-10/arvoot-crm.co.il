<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="ChatList.aspx.cs" Inherits="ControlPanel._chatList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% 
        string Title = "Users";
    %>
    <meta name="Description" content='<% = Title%>' />
    <meta name="keywords" content='<% = Title%>' />
    <meta name="abstract" content='<% = Title%>' />
    <meta http-equiv="title" content="<% = Title%>" />
    <title><% = Title %></title>
    <style>
        a {
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="col">
                <div class="row">
                    <div style="width: 61%; margin-inline-end: 2%; padding-top: 10px;" class="SecondaryDiv">
                        <div class="row">
                            <img src="images/icons/Topbar_Logo_Message_Icon_Off.png" style="margin-inline-end: 10px;" class="ImgBell" />
                            <label class="LblLatestNotifications">נתון חודשי</label>
                        </div>
                    </div>
                    <div style="width: 37%;" class="SecondaryDiv">
                        <div class="row DivLatestNotifications">
                            <div class="row DivSecLatestN">
                                <img src="images/icons/Topbar_Logo_Message_Icon_Off.png" style="margin-inline-end: 10px;" class="ImgBell" />
                                <label class="LblLatestNotifications">התראות אחרונות</label>
                            </div>
                            <div class="row DivSecLatestN">
                                <img src="images/icons/Refresh_Last_Message_Button.png" style="margin-inline-end: 10px;" />
                                <label class="Blue" style="margin-inline-end: 5px;">עודכן ב:</label>
                                <label class="Blue">21:02</label>
                            </div>
                        </div>
                        <div class="MainDivDocuments" style="height: 266px; margin-bottom: 40px;">
                            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col" style="background-color: #f6f6f6; padding: 2% 1%; border-radius: 4px;">
                                        <div class="row">
                                            <label class="LblDate FontWeightBold">
                                                <%--                                                <%#Eval("AlertsDate") %>--%>
                                            </label>
                                        </div>
                                        <div class="row">
                                            <%--<%#Eval("AlertText") %>--%>
                                            <%#Eval("userFirstName") %>
                                        </div>
                                    </div>
                                    <div style="background-color: white; height: 15px;"></div>
                                </ItemTemplate>


                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 40px;">
                    <div class="col SecondaryDiv" style="width: 47%; margin-inline-end: 2%;">
                        <div class="row DivTasks">
                            <label class="LableBlue">משימות פתוחות</label>
                        </div>
                        <div class="MainDivDocuments DivRepeaterTasks">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="row" id="Row" runat="server">
                                        <%--                                    <div style="height: 20px;"></div>--%>
                                        <div style="width: 8%; text-align: left;">
                                            <img src="images/icons/Open_Mession_Blue_Point_2.png" runat="server" id="ImgBlue" />
                                            <%--<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/icons/Open_Mession_Blue_Point_2.png" OnClick="DeleteLid_Click" />--%>
                                        </div>
                                        <div class="row DivBackgroundTasks" runat="server">
                                            <div runat="server" style="width: 10%; text-align: center;" class="ColorLable ">
                                                <%-- <%#Eval("TasksDate") %>--%>
                                            </div>
                                            <div class="DivGrayTasks" style="margin-right: 3%; margin-left: 3%;">
                                                <%--                                            <div style="height: 80%; width: 1px; background-color: #606b86; margin: auto;"></div>--%>
                                            </div>
                                            <div runat="server" style="width: 13%; text-align: right;" class="ColorLable ">
                                                <%-- <%#Eval("TasksTime") %>--%>
                                            </div>
                                            <div runat="server" style="width: 40%; text-align: right;" class="ColorLable ">
                                                <%--<%#Eval("TaskText") %>--%><%#Eval("userFirstName") %>
                                            </div>
                                            <div runat="server" style="width: 17%; text-align: right;" class="ColorLable row AlignItemsCenter">
                                                <%--                                    <%#Eval("File") %>--%>
                                                <img src="images/icons/Open_Mession_Waiting_Flug.png" runat="server" id="Img1" />
                                                <label style="color: #10b049;">בדיקה</label>
                                            </div>
                                            <div style="width: 7%; text-align: center;">
                                                <%--<asp:ImageButton ID="ImageButton5" runat="server" CommandArgument='<%#Eval("File") %>' OnCommand="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icon/Upload_Button.png" />--%>
                                                <asp:ImageButton ID="UploadFile" runat="server" OnClick="UploadFile_Command" Style="vertical-align: middle; position: relative" ImageUrl="~/images/icons/Open_Mession_Delete_Button.png" />
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

                    <div class="col SecondaryDiv" style="width: 51%;">
                        <div style="margin-bottom: 30px;">
                            <asp:Calendar CssClass="myCalendar myCalendarMonth" DayHeaderStyle-Font-Bold="false" TitleStyle-CssClass="calendar-title calendar-title-month" BorderStyle="None" OnSelectionChanged="activitiesCal_SelectionChanged" OnDayRender="activitiesCal_DayRender" Width="100%" runat="server" ID="activitiesCal">
                                <SelectedDayStyle BackColor="LightGray"
                                    ForeColor="Red"></SelectedDayStyle>
                            </asp:Calendar>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

