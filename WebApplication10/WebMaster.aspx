<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master"  CodeBehind="WebMaster.aspx.cs" Inherits="WebApplication10.WebMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    WebMaster
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server" CssClass="contenido-transparente">


    <h1>Bienvenido WebMaster</h1>
    <h2>Elija alguna opción del menú de la izquierda<asp:Image ID="Image2" runat="server" ImageUrl="~/fondowebmaster.jpg" />
    </h2>
    <p>&nbsp;</p>
</asp:Content>
