<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="Backup.aspx.cs" Inherits="WebApplication10.Backup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Backup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    <h1>Backup</h1>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Crear Backup" OnClick="Button1_Click" />
</asp:Content>
