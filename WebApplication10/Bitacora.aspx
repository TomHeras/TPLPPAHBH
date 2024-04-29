<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="Bitacora.aspx.cs" Inherits="WebApplication10.Bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Bitacora
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .form-control {}
.table{border-collapse:collapse!important}.table-bordered{border:1px solid #dee2e6}.table{width:100%;margin-bottom:1rem;color:#212529}table{border-collapse:collapse}*{text-shadow:none!important;box-shadow:none!important}*{box-sizing:border-box}thead{display:table-header-group}tr{page-break-inside:avoid}.table-bordered thead th{border-bottom-width:2px}.table thead th{vertical-align:bottom;border-bottom:2px solid #dee2e6}.table-bordered th{border:1px solid #dee2e6!important}.table th{background-color:#fff!important}.table-bordered th{border:1px solid #dee2e6}.table th{padding:.75rem;vertical-align:top;border-top:1px solid #dee2e6}th{text-align:inherit}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server"  >
    
    <div class="panel panel-default panel-transparente">
                        <div class="panel-heading">
                             Bitácora
                             <br />
                             <br />
                             <div class="table-responsive">
                                 <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Bitacora"></asp:Label>
                                 <br />
                                 <br />
                                 <asp:Label ID="Label2" runat="server" Text="Usuario"></asp:Label>
                                 <br />
                                 <asp:DropDownList ID="ddlusuarios" runat="server" Width="225px" OnSelectedIndexChanged="ddlusuarios_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <br />
                                 <br />
                                 <asp:Label ID="Label3" runat="server" Text="Criticidad"></asp:Label>
                                 <br />
                                 <asp:DropDownList ID="ddlcriticidad" runat="server" Height="16px" Width="224px" OnSelectedIndexChanged="ddlcriticidad_SelectedIndexChanged">
                                     <asp:ListItem>Alto</asp:ListItem>
                                     <asp:ListItem>Media</asp:ListItem>
                                     <asp:ListItem>Leve</asp:ListItem>
                                 </asp:DropDownList>
                                 <br />
                                 <br />
                                 <asp:Label ID="Label4" runat="server" Text="Fecha Inicio"></asp:Label>
                                 <br />
                                 <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" EnableTheming="True" TextMode="Date" Width="215px"></asp:TextBox>
                                 <br />
                                 <br />
                                 <asp:Label ID="Label5" runat="server" Text="Fecja Fin"></asp:Label>
                                 <br />
                                 <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date" Width="213px"></asp:TextBox>
                                 <br />
                                 <br />
                                 <asp:Button ID="Btnfiltrar" runat="server" BorderColor="#0033CC" BorderStyle="Groove" Text="Filtrar" Width="72px" OnClick="Btnfiltrar_Click" />
                                 <br />
                                 <table border="1" class="table table-bordered" style="border: 1px solid #009999; font-family: Arial, Helvetica, sans-serif;">
                                     <thead>
                                         <tr>
                                             <th>LoginName</th>
                                             <th>Actividad</th>
                                             <th>Criticidad</th>
                                             <th>Fecha</th>
                                             <th>Hora</th>
                                         </tr>
                                     </thead>
                                     <asp:Repeater ID="rptBitacora" runat="server">
                                         <ItemTemplate>
                                             <tr>
                                                 <td><%# Eval("LoginName") %></td>
                                                 <td><%# Eval("Actividad") %></td>
                                                 <td><%# Eval("Criticidad") %></td>
                                                 <td><%# Eval("Fecha") %></td>
                                                 <td><%# Eval("Hora") %></td>
                                             </tr>
                                         </ItemTemplate>
                                     </asp:Repeater>
                                     <asp:Button ID="Button1" runat="server" Text="Button" />
                                 </table>
                                 <br />
                                 <asp:Button ID="btnanterior" runat="server" Text="Anterior" OnClick="btnanterior_Click" />
                                 <br />
                                 <asp:Button ID="btnsiguiente" runat="server" Text="Siguiente" OnClick="btnsiguiente_Click" />
                                 <br />
                                 <br />
                                 <asp:Label ID="Label6" runat="server" Text="Num Pagina:"></asp:Label>
                                 <asp:Label ID="lblpagina" runat="server" ForeColor="#000066"></asp:Label>
                             </div>
                             <div class="table-responsive">
                                 <br />
                                 <br />
                             </div>
                        </div>
        </div> 
</asp:Content>
