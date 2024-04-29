<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Seguridad.master" CodeBehind="Permisos_usuarios.aspx.cs" Inherits="WebApplication10.Permisos_usuarios" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Usuarios
</asp:Content>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    <h1>
        Gestión De Permisos De Usuarios
    </h1>
    <br />
    <br />
        
    <br />
    <asp:Label ID="Label1" runat="server" Text="Usuarios"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlusuarios" runat="server"  Width="130px">
    </asp:DropDownList>
    <br />
    <asp:Button ID="btnconfigusuarios" runat="server" Text="Configurar" />
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Familias"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlfamilias" runat="server">
    </asp:DropDownList>
    <br />
    <asp:Button ID="btnconfigflias" runat="server" Text="Agregar" OnClick="btnconfigflias_Click" />
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Patentes"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlpatentes" runat="server">
    </asp:DropDownList>
    <br />
    <asp:Button ID="btnconfigpatentes" runat="server" Text="Agregar" OnClick="btnconfigpatentes_Click" />
    <br />
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="Configuracion"></asp:Label>
    <br />
    <asp:TreeView ID="TreeView1" runat="server">
    </asp:TreeView>
    <asp:Button ID="btnguardarcambios" runat="server" Text="Guardar Cambios" OnClick="btnguardarcambios_Click" />
</asp:Content>
