<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ControlPanel._homePage" %>

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
        .progress-ring {
            position: relative;
            width: 120px;
            height: 120px;
            margin: auto;
        }

        .progress-ring__circle {
            stroke: #F1F1F1;
            fill: transparent;
            stroke-width: 8;
        }

        .progress-ring__path {
            fill: transparent;
            stroke-width: 8;
            stroke-linecap: round;
            transition: stroke-dashoffset 0.35s;
            transform: rotate(-90deg);
            transform-origin: 50% 50%;
        }

        .progress-ring__text {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            font-size: 24px;
            font-weight: bold;
        }

        .progress-ring__background {
            fill: #F1F1F1;
        }

        .text-yellow {
            color: #F5C065;
            font-weight: bold;
        }

        .text-green {
            color: #10B049;
            font-weight: bold;
        }

        .text-purple {
            color: #800080;
            font-weight: bold;
        }

        .text-blue {
            color: #0098FF;
            font-weight: bold;
        }

        .text-green-paid {
            color: #4CAF50;
            font-weight: bold;
        }

        .text-red {
            color: #F44336;
            font-weight: bold;
        }

        .text-gray {
            padding: 7px;
            color: #8990A4;
            font-weight: 500;
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="AddForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>


            <div class="rowHome">
                <div class="colHome" style="width: 70%; font-size:15px">
                    <%--                 <asp:Label ID="Label5" Text="נתון כללי" class="text-blue" style="font-size:11pt"  runat="server" />--%>
                    <div class="rowHome" style="padding: 25px 25px 0px 25px; height: auto;">
                        <asp:Button ID="Label6" OnClick="Month_General_Click" Text="נתון כללי" class="text-blue" Style="font-size: 11pt; color: #05025f; margin-right: 3%; background: none; border: none; font-family: 'Open Sans Hebrew', sans-serif; /*Gila*/" runat="server" />
                    </div>
                    <div class="rowHome" style="height: 80%;">

                        <div style="width: 5%"></div>
                        <div style="width: 15%; margin: auto">
                            <div class="progress-ring">
                                <svg width="120" height="120">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath" class="progress-ring__path" style="stroke: #F5C065;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text">
                                    <asp:Label ID="PercentageText" class="text-yellow" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label1" Text="חוסרים" class="text-yellow" Style="font-size: 11pt" runat="server" />

                        </div>
                        <div style="width: 15%; margin: auto">
                            <div class="progress-ring">
                                <svg width="120" height="120">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath1" class="progress-ring__path" style="stroke: #10B049;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text">
                                    <asp:Label ID="PercentageText1" class="text-green" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label2" Text="בטיפול" class="text-green" Style="font-size: 11pt" runat="server" />

                        </div>
                        <div style="width: 15%; margin: auto">
                            <div class="progress-ring">
                                <svg width="120" height="120">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath2" class="progress-ring__path" style="stroke: #800080;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text">
                                    <asp:Label ID="PercentageText2" class="text-purple" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label3" Text="בוטל" class="text-purple" Style="font-size: 11pt" runat="server" />

                        </div>
                        <div style="width: 15%; margin: auto">
                            <div class="progress-ring">
                                <svg width="120" height="120">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath3" class="progress-ring__path" style="stroke: #0098FF;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text">
                                    <asp:Label ID="PercentageText3" class="text-blue" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label4" Text="הושלם" class="text-blue" Style="font-size: 11pt" runat="server" />

                        </div>

                        <div style="width: 15%; margin: auto">
                            <div class="progress-ring">
                                <svg width="120" height="120">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath4" class="progress-ring__path" style="stroke: #4CAF50;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text" style="font-size:18px;">
                                    <asp:Label ID="PercentageText4" class="text-green-paid" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label11" Text="שולם" class="text-green-paid" Style="font-size: 11pt" runat="server" />

                        </div>

                        <div style="width: 15%; margin: auto">
                            <div class="progress-ring">
                                <svg width="120" height="120">
                                    <circle class="progress-ring__background" cx="60" cy="60" r="45"></circle>
                                    <circle class="progress-ring__circle" cx="60" cy="60" r="54"></circle>
                                    <circle id="progressPath5" class="progress-ring__path" style="stroke: #F44336;" cx="60" cy="60" r="54"></circle>
                                </svg>
                                <span class="progress-ring__text" style="font-size:18px;">
                                    <asp:Label ID="PercentageText5" class="text-red" runat="server" />
                                </span>
                            </div>
                            <asp:Label ID="Label13" Text="יתרת תשלום" class="text-red" Style="font-size: 11pt" runat="server" />

                        </div>
                        <div style="width: 5%"></div>

                    </div>
                </div>
                <div class="colHome" style="width: 30%">
                    <div class="rowHome" style="padding: 25px 25px 0px 25px; height: auto;">
                        <img src="images/icons/Last_Message_Icon.png" runat="server" style="width: 17px; height: 18px;" />

                        <asp:Label ID="Label5" Text="התראות אחרונות" class="text-blue" Style="font-size: 11pt; padding-right: 10px" runat="server" />
                    </div>
                    <div style="padding-bottom: 12%; padding-right: 7%; margin: 10px 10px 10px 30px;">
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <div style="background-color: whitesmoke; border-radius: 12px; margin: 10px 0px 10px 10px; padding-right: 16px; padding-bottom: 8px;">
                                    <div class="LblDate FontWeightBold" style="margin-top: 10px; padding-top: 6px;">3.8.24</div>

                                    <div style="text-align: left; padding-left: 10px; text-align: right; margin-top: 6px; font-size: 12px">
                                        לורם אבח חכלגדח מצ לדגיכ לךדגי עחגדלי חעלי עחלד ךכיגחל 
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    <%-- Gila --%>
        <asp:UpdatePanel ID="AddForm2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="rowHome with-last-child">
                <div class="colHome" style="width: 50%; height: 400px;">
                    <%-- Gila --%>
                    <div class="rowHome" style="padding: 25px 25px 14px 25px; height: auto; border-bottom: 1px #EEF2F4 solid; width:90%;">
                        <asp:Label ID="Label7" Text="משימות פתוחות" class="text-blue" Style="font-size: 11pt; padding-right: 10px" runat="server" />
                    </div>
                    <div style="padding-bottom: 12%; margin: 10px 10px 10px 30px;">
                        <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                            <ItemTemplate>
                                <div style="display: flex; border-radius: 12px; margin-left: 10px; padding-right: 16px; padding-bottom: 8px;">
                                    <img src="images/icons/Open_Mession_Blue_Point_1.png" runat="server" style="width: 39px; height: 40px;" />

                                    <div style="text-align: left; display: flex; width: 100%; background-color: whitesmoke; padding-left: 10px; text-align: right; margin-top: 6px; font-size: 12px">

                                        <div id="Label7" class="text-gray" style="width: 10%"><%#Eval("dateTask") %> </div>
                                        <div id="Label8" class="text-gray" style="width: 3%">| </div>
                                        <div id="Label9" class="text-gray" style="width: 10%"><%#Eval("timeTask") %> </div>
                                        <div id="Label6" class="text-gray" style="width: 52%"><%#Eval("Text") %> </div>
                                        <div style="width: 5%" class="text-gray">
                                            <img src="images/icons/Open_Mession_Waiting_Flug.png" runat="server" style="width: 18px; height: auto;" /></div>
                                        <div id="Status" class="text-gray" style="width: 15%"><%#Eval("Status") %></div>
                                        <div class="text-gray" style="width: 5%">
                                            <img src="images/icons/Open_Mession_Delete_Button.png" runat="server" style="width: 18px; height: auto;" /></div>
                                        <div class="text-gray" style="width: 5%">
                                            <img src="images/icons/Open_Mession_Edit_Button.png" runat="server" style="width: 18px; height: auto;" /></div>


                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </div>
                <div class="colHome" style="width: 50%; height: 400px;">
                    <%-- Gila --%>
                    <asp:Calendar ID="TasksCalendar" CssClass="tasks-calendar" runat="server" SelectedDate="<%# DateTime.Today %>"
                        OnDayRender="TasksCalendar_DayRender"
                        OnSelectionChanged="TasksCalendar_SelectionChanged"
                        OnVisibleMonthChanged="TasksCalendar_VisibleMonthChanged" DayNameFormat="Shortest"
                        Width="86%" Height="94%" Style="margin-right: auto; margin-left: auto; margin-top: 2%; border: none; border-collapse: separate; border-spacing: 6px;">
                        <TitleStyle BackColor="Transparent" Height="40" ForeColor="#0098ff" Font-Bold="true" HorizontalAlign="Center" CssClass="tasks-calendar-title" />
                        <DayHeaderStyle Height="20px" />
                        <DayStyle CssClass="calendar-day" BorderColor="#DEDFE0" BorderStyle="Solid" BorderWidth="1px" />
                        <SelectedDayStyle BackColor="Transparent" BorderColor="#669EFF" ForeColor="Black" Font-Bold="true"></SelectedDayStyle>
                    </asp:Calendar>




                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">



</script>
</asp:Content>


