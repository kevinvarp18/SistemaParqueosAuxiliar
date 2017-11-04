<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_Reporte.aspx.vb" Inherits="SistemaDeParqueos.frm_Reporte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <h3>Reporte</h3>
                    <asp:Label ID="lblDesde" runat="server" style="margin-left: 10%;" Text="Desde:"></asp:Label>
                    <asp:TextBox ID="tbFechaI" type="date" runat="server" style="margin-left: 2%; width: 13%;"></asp:TextBox>
                    <asp:Label ID="lblHasta" runat="server" style="margin-left: 10%;" Text="Hasta:"></asp:Label>
                    <asp:TextBox ID="tbFechaF" type="date" runat="server" style="margin-left: 2%; width: 13%;"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" style="width: 15%; margin-left: 10%;" OnClick="btnBuscar_Click" Text="Buscar" />
                    
                    </div>
                    </div>
                    <div class="container">
                        <div class="page w3-4">
                            <div class="bs-example " data-example-id="simple-table">
                                <asp:Table ID="table" runat="server" CssClass="table">
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
</asp:Content>
