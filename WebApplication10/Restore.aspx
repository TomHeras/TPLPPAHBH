<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="master.Master" CodeBehind="Restore.aspx.cs" Inherits="WebApplication10.Restore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Restore
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    <h1>
        Restore
    </h1>
    <br />
    <br />
    <asp:FileUpload ID="FileUpload1" runat="server" />  
    <br />
    <asp:Button ID="btnrestore" runat="server" CssClass="btn btn-primary" Text="Realizar restore" OnClick="btnrestore_Click" />
</asp:Content>
