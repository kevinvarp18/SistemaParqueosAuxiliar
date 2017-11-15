<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_SolicitudesAtrasadas.aspx.vb" Inherits="SistemaDeParqueos.frm_SolicitudesAtrasadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <h3>Visitantes Atrasados</h3>
                    <div class="container">
                        <div class="page w3-4">
                            <div class="bs-example " data-example-id="simple-table">
                                <div class="botonTabla">
                                    <asp:Table ID="tabla" runat="server" CssClass="table">
                                        <asp:TableHeaderRow>
                                            <asp:TableHeaderCell>Nombre</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Marca</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Placa</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Modelo</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Espacio</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Hora Entrada</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Hora Salida</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Extensión</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Accion</asp:TableHeaderCell>
                                        </asp:TableHeaderRow>
                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <script>
                        function muestraMensaje(titulo, mensaje, tipo) {
                            swal(
                              titulo,
                              mensaje,
                              tipo
                            )
                        }
                    </script>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

