<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_AdministrarSolicitudes.aspx.vb" Inherits="SistemaDeParqueos.frm_AdministrarSolicitudes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <h3>Administrar Solicitudes</h3>
                    </div>
                    <asp:HyperLink id="lblParqueoModal" class="new-gri" data-toggle="modal" style="text-decoration:underline; color:#000000; margin-left:84%; font-size: 1.5em; font-family: 'Cabin', sans-serif;" data-target="#myModal1" NavigateUrl="#" Text="Ver Parqueo" runat="server"/>
                    <div class="container">
                        <div class="page w3-4">
                            <div class="bs-example " data-example-id="simple-table">
                                <asp:Table ID="tabla" runat="server" CssClass="table">
                                    <asp:TableHeaderRow>
                                        <asp:TableHeaderCell>Nombre</asp:TableHeaderCell>                                        
                                        <asp:TableHeaderCell>Placa</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Fecha Entrada</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Hora Entrada</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Fecha Salida</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Hora Salida</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Espacio</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Acción</asp:TableHeaderCell>
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
