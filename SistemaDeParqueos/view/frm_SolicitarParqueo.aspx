<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_SolicitarParqueo.aspx.vb" Inherits="SistemaDeParqueos.frm_SolicitarParqueo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <h3>Solicitar Parqueo</h3>
                    <asp:Label ID="lblSolicitante" runat="server" Text="Nombre Solicitante:"></asp:Label>
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario" style="margin-left: 1%; height: 5%;"></asp:Label><br /><br />
                    <asp:Label ID="lblFechaE" runat="server" Text="Fecha Entrada:"></asp:Label>
                    <asp:TextBox ID="tbFechaE" type="date" required="" runat="server" style="margin-left:4.3%; width:27%;"></asp:TextBox><br />
                    <asp:Label ID="lblHoraE" runat="server" Text="Hora Entrada:"></asp:Label>
                    <asp:TextBox ID="tbHoraE" type="time" required="" runat="server" style="margin-left:4.9%; width:27%;"></asp:TextBox><br />
                    <asp:Label ID="lblFechaS" runat="server" Text="Fecha Salida:"></asp:Label>
                    <asp:TextBox ID="tbFechaS" type="date" required="" runat="server" style="margin-left:5.5%; width:27%;"></asp:TextBox><br />
                    <asp:Label ID="lblHoraS" runat="server" Text="Hora Salida:"></asp:Label>
                    <asp:TextBox ID="tbHoraS" type="time" required="" runat="server" style="margin-left:6.1%; width:27%;"></asp:TextBox><br />
                    <asp:Label ID="lblPlaca" runat="server" Text="Placa:"></asp:Label>
                    <asp:TextBox ID="tbPlaca" type="text" runat="server" style="margin-left:9.8%; width:27%;"></asp:TextBox><br />
                    <asp:Label ID="lblMarca" runat="server" Text="Marca:"></asp:Label>
                    <asp:TextBox ID="tbMarca" type="text" runat="server" style="margin-left:9.2%; width:27%;"></asp:TextBox><br />
                    <asp:Label ID="lblModelo" runat="server" Text="Modelo:"></asp:Label>
                    <asp:TextBox ID="tbModelo" type="text" runat="server" style="margin-left:8.5%; width:27%;"></asp:TextBox><br />
                    <asp:Button ID="btnSolicitar" runat="server" CssClass="singleTextBox" OnClick="btnSolicitar_Click" Text="Solicitar" />
                    <br />
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
