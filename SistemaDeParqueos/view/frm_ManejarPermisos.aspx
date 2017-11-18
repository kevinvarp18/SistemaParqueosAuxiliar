<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_ManejarPermisos.aspx.vb" Inherits="SistemaDeParqueos.frm_ManejarPermisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <h3>Permisos y Roles</h3>
                    <div class="bs-example " data-example-id="simple-table">
                        <asp:Table ID="tabla" runat="server" CssClass="table">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell>Permiso</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Rol</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </div>
                    <br /><br />
                    <h3>Agregar o Eliminar Permisos a un Rol</h3>
                    <br />
                    <asp:Label ID="lblPermiso" runat="server" Text="Permiso:"></asp:Label>
                    <asp:DropDownList ID="DwnLstPermisos" runat="server" AutoPostBack="false"></asp:DropDownList><br />
                    <asp:Label ID="lblUsuario" runat="server" Text="Rol:"></asp:Label>
                    <asp:DropDownList ID="DwnLstRoles" runat="server" AutoPostBack="false"></asp:DropDownList><br />
                    <asp:Button ID="btnAgregar" runat="server" CssClass="multipleButton" OnClick="btnAgregar_Click" Text="Agregar" />
                    <asp:Button ID="btnEliminar" runat="server" CssClass="multipleButton" OnClick="btnEliminar_Click" Text="Eliminar" /><br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
