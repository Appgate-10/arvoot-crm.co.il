<%@ Page Title="" Language="C#" MasterPageFile="~/DesignDisplay.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ControlPanel._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<% 
    string Title = "asdasd";
%>
    <meta name="Description" content='<% = Title%>'>
    <meta name="keywords" content='<% = Title%>'>
    <meta name="abstract" content='<% = Title%>'>
    <meta http-equiv="title" content="<% = Title%>">
    <title><% = Title %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">MarkMenuCss('Default');</script>
</asp:Content>
