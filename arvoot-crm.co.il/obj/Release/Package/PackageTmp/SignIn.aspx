<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="ControlPanel.SignIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% 
        string Title = "התחברות";
    %>
    <meta name="Description" content='<% = Title%>' />
    <meta name="keywords" content='<% = Title%>' />
    <meta name="abstract" content='<% = Title%>' />
    <meta http-equiv="title" content="<% = Title%>" />
    <title><% = Title %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="height: 100vh;">
        <div style="position: relative; width: 100%; height: 100%; background-size: 100% auto; background-repeat: no-repeat; background-image: url('../images/icons/Signin_BG.png')">
            <div class="row" style="height: 80px; background-color: white; margin-bottom: 60px; justify-content: space-between; padding: 10% 0px; justify-content: space-between; padding: 0px 20%; align-items: center;">
                <img src="images/icons/Topbar_Logo_1.png " />
                <div>
                    <span style="margin-inline-end: 10px; color: #9da4b1;">אין לך חשבון?</span>
                    <a id="EnrollmentId" runat="server" href="Users.aspx" style="color: #273283;">פנה לשירות הצטרפות</a>
                </div>
            </div>
            <div style="position: relative; min-width: 600px; margin-right: 22%; width: 32%; height: 53%; border-radius: 24px; background-color: white;">

                <%--                <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
                <asp:UpdatePanel ID="UpdateTopPanelLogin" style="position: absolute; top: 12%; width: 100%; height: 88%; text-align: center;" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>

                        <div runat="server" id="ErrorDiv" visible="false" style="margin: 0 auto; width: 64.3%; background-color: #ffeaea; border-bottom: solid 1px red; font-size: 14pt; border-radius: 6px; padding: 1%;"></div>

                        <span class="SpanConnection">ברוך שובך:</span>
                        <input type="text" id="Email" runat="server" placeholder="אימייל" class="SignInBoxInput" />
                        <asp:Label ID="E_UserName_lable" runat="server" Text="יש להזין אימייל" CssClass="ErrorLable" Style="margin-top: 0px;" Visible="false"></asp:Label>

                        <input style="position: relative;" type="password" placeholder="סיסמא" id="Password" name="Password" class="SignInBoxInput" runat="server" />
                        <asp:ImageButton style="position: absolute;top: 37%;left: 103px;" ID="ShowPasswordBtn" runat="server" ImageUrl="~/images/icons/Signin_Detele_Hide_Password_On.png" OnClientClick="showPassword(); return false;" />
                        <asp:Label ID="E_Password_lable" runat="server" Text="יש להזין סיסמא" CssClass="ErrorLable" Style="margin-top: 0px;" Visible="false"></asp:Label>
                        <div style="width: 84%; text-align: left; margin-top: 2%;">
                            <asp:Button ID="BusinessChat" EnableViewState="false" runat="server" Text="שחזר סיסמא" CssClass="PasswordButton" Style="cursor: pointer;" OnClick="ForgetPassword_Click" />
                        </div>
                        <div style="width: 84%; text-align: left;">
                            <asp:ImageButton Style="margin-top: 19%;" ID="SignInBU" Name="SignInBU" runat="server" ImageUrl="~/images/icons/Signin_Detele_Next_Button.png" OnClick="SignInBU_Click" />
                            <%--<asp:Button ID="SignInBU" Name="SignInBU" runat="server" Text="התחברות" OnClick="SignInBU_Click" class="FormInputBu" Style="width: 66.3%; margin-top: 6%;" />--%>
                        </div>
                        <div id="ForgetPasswordpopUp" class="popUpOut" style="direction: ltr; text-align: center;" visible="false" runat="server">

                            <div class="popUpIn" style="position: relative; min-width: 600px; background-position: center; margin-top: 161px; vertical-align: middle; width: 30%; height: 46%; direction: rtl;" runat="server">



                                <asp:ImageButton runat="server" ImageUrl="images/icons/Popup_Close_Button.png" style="margin-top: 15px; float: left; margin-left: 15px;" CssClass="imgX" ID="ClosePopUp" OnClick="ClosePopUp_Click" />


                                <div class="content-wrapper">

                                    <div class="form-header form-header-password">הכנס אימייל על מנת לקבל סיסמא</div>
                                    <input type="text" id="UserEmailPopUp" runat="server" style="height:50px; width:50%;margin-bottom:7%" class="SignInBoxInput" placeholder="אימייל" />

                                    <asp:Label ID="UserEmail_lable" runat="server" Text="" CssClass="ErrorLable" Style="display: block; text-align: center" Visible="true"></asp:Label>
                                    <asp:Button ID="BtnOkForgetPassword" OnClick="BtnOkForgetPassword_Click" runat="server" Text="אישור" class="BtnPopUpPay" />
                                    <asp:Label ID="ErrorLable" runat="server" Text="התרחשה שגיאה" CssClass="ErrorLable2" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
    <script type="text/javascript">
        function showPassword() {
            var passwordTextBox = document.getElementById('<%= Password.ClientID %>');
            var showPasswordBtn = document.getElementById('<%= ShowPasswordBtn.ClientID %>');

            if (passwordTextBox.type == "password") {
                passwordTextBox.type = "text";
                showPasswordBtn.src = "images/icons/Signin_Detele_Hide_Password_Off.png";
            } else {
                passwordTextBox.type = "password";
                showPasswordBtn.src = "images/icons/Signin_Detele_Hide_Password_On.png";
            }
        }
    </script>
</asp:Content>
