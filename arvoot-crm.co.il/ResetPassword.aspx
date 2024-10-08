<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplayFailed.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="ControlPanel._ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% 
        string Title = "Coupons";
    %>
    <meta name="Description" content='<% = Title%>' />
    <meta name="keywords" content='<% = Title%>' />
    <meta name="abstract" content='<% = Title%>' />
    <meta http-equiv="title" content="<% = Title%>" />
    <title><% = Title %></title>
              <style>
            #MainDiv {
                width: 500px;
                position: relative;
                margin-top: 4%;
            }

            @media screen and (max-width: 1030px) {
                #MainDiv {
                    width: 100%;
                    position: relative;
                    margin-top: 4%;
                    font-family: 'Open Sans Hebrew', sans-serif;
                }
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="MainContentDiv" style="display: block;text-align: -webkit-center;">

         <div style="width: 100%; height: 1%; background-color: #027aff; position: relative; text-align: center; "></div>
        <div style="width: 100%; height: 6%; background-color: #027aff; position: relative; text-align: center; ">
            <img  src="images/icon/Topbar_Icon.png" style="height:100%;" />
        </div>
        <div style="width: 100%; height: 1%; background-color: #027aff; position: relative; text-align: center; "></div>
        <div id="MainDiv">
            <div style="width:90%;position:relative;text-align:right;">
                <span style="font-size: xx-large; font-family: Arial; color: #027aff;">שינוי סיסמא</span>
            </div>

            <div id="LinkErrorDiv" runat="server" visible="false" style=" width: 90%; background-color: #fed1d1; border: solid 1px #ff0000; font-size: medium; color: #027aff; font-family: arial; text-align: right; line-height: 35px; margin-top: 6%; padding: 2%; ">
                <!--Sent Phone link for reset your password, missing or damaged.<br>Make a new request through the application-->
                לינק זה אינו תקין<br />הגש בקשה חדשה דרך האפליקציה
            </div>

            <div id="ChangedSuccessfullyDiv" runat="server" visible="false" style="width:90%;background-color:#dff0d8;border:solid 1px #d8ebcc;height:50px;font-size:medium;color:#4f844f;font-family:arial;text-align:center;line-height:50px;margin-top:6%;">
                הסיסמא שונתה בהצלחה.
            </div>
            <div id="FormDiv" runat="server">
               <%-- <form action="" runat="server" method="post" name="Formadd" id="Formadd">--%>
                    <div style="width: 90%; position: relative; text-align: right;">
                        <br /><br /><br />
                        <span style="font-size: medium; font-family: Arial; color: #027aff;">סיסמא חדשה</span><br />
                        <input type="password" id="PasswordaAgent" runat="server"  name="PasswordAgent" style="width:100%;border:solid 1px #bfbfbf;font-size:x-large;height:45px;" />
                        <br /><br /><br />
                        <span style="font-size: medium; font-family: Arial; color: #027aff; ">אימות סיסמא</span><br />
                        <input type="password" id="RePasswordAgent" runat="server" name="RePasswordAgent" style="width:100%;border:solid 1px #bfbfbf;font-size:x-large;height:45px;" />
                        <br /><br /><br />
                        <asp:Button ID="ChangePassword" runat="server" OnClick="ChangePassword_Click" type="button" style="width: 100%; background-color: #027aff; border: solid 1px #FFFFFF; height: 50px; font-size: large; color: #FFFFFF; font-family: Arial; text-align: center; line-height: 50px; -webkit-a ppearance: none; -webkit-border-radius: 0;" text="שנה סיסמא" />

<%--                        <input type="button" onclick="javascript:CheckAndSubmit();" style="width: 100%; background-color: #6d1f99; border: solid 1px #FFFFFF; height: 50px; font-size: large; color: #FFFFFF; font-family: Arial; text-align: center; line-height: 50px; -webkit-a ppearance: none; -webkit-border-radius: 0;" value="שנה סיסמא" />--%>
                        <br /><br /><br />
                    </div>
              <%--  </form>--%>
            </div>
        </div>

    </div>
   


 


</asp:Content>

