<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_Reporte.aspx.vb" Inherits="SistemaDeParqueos.frm_Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <h3>Reporte</h3>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <asp:Label ID="lblTipoReporte" runat="server" Text="Tipo Reporte:"></asp:Label>
                    <asp:DropDownList ID="DwnLstTipoReporte" runat="server" AutoPostBack="true"></asp:DropDownList><br />

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="false">
                        <ContentTemplate>
                            <asp:Label ID="lblTipoEscogido" runat="server" Text=""></asp:Label>
                            <asp:DropDownList ID="DwnLstDatos" runat="server" AutoPostBack="false"></asp:DropDownList><br />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" Visible="false">
                        <ContentTemplate>
                            <asp:Label ID="lblDesde" runat="server" Text="Desde:"></asp:Label>
                            <asp:TextBox ID="tbFechaI" type="date" runat="server"></asp:TextBox><br />
                            <asp:Label ID="lblHasta" runat="server" Text="Hasta:"></asp:Label>
                            <asp:TextBox ID="tbFechaF" type="date" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:Button ID="btnBuscar" runat="server" CssClass="multipleButton" OnClick="btnBuscar_Click" Text="Buscar" />
                    <asp:Button ID="btnExportar" runat="server" CssClass="multipleButton" OnClick="btnExportar_Click" Text="Exportar" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel6" runat="server" Visible="true">
                <ContentTemplate>
                    <div class="container">
                        <div class="page w3-4">
                            <div class="bs-example " data-example-id="simple-table">
                                <asp:Table ID="table" runat="server" CssClass="table">
                                    <asp:TableHeaderRow>
                                            <asp:TableHeaderCell>Nombre</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Institución</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Placa</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Fecha Entrada</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Hora Entrada</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Fecha Salida</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Hora Salida</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Espacio</asp:TableHeaderCell>
                                        </asp:TableHeaderRow>
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
