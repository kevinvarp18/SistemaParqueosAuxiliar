<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_AdministrarParqueo.aspx.vb" Inherits="SistemaDeParqueos.administrarParqueo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <h3>Administrar Parqueo</h3>
                    <asp:Label ID="lblNumEspacio" runat="server" Text="Número Espacio:"></asp:Label>
                    <asp:DropDownList ID="DwnEspacio" runat="server" style="margin-left: 1%;" AutoPostBack="false"></asp:DropDownList><br />
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo:"></asp:Label>
                    <asp:DropDownList ID="DwnLstTipos" runat="server" style="margin-left: 8.4%;" AutoPostBack="false"></asp:DropDownList><br />
                    <asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label>
                    <asp:DropDownList ID="DwnLstEstado" runat="server" style="margin-left: 7%;" AutoPostBack="false"></asp:DropDownList> <br />
                    <asp:Button ID="btnCrear" runat="server" CssClass="multipleTextBox" OnClick="btnCrear_Click" Text="Crear" />
                    <asp:Button ID="btnActualizar" runat="server" CssClass="multipleTextBox" OnClick="btnActualizar_Click" Text="Actualizar" />
                    <asp:Button ID="btnEliminar" runat="server" CssClass="multipleTextBox" OnClick="btnEliminar_Click" Text="Eliminar" />
                    <br />
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
