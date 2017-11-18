<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_VerParqueo.aspx.vb" Inherits="SistemaDeParqueos.VerParqueo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <h3>Ver Parqueo</h3>
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
                    <asp:TextBox ID="tbFechaI" type="date" required="" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblDesde" runat="server" Text="Desde:"></asp:Label>
                    <asp:TextBox ID="tbHoraI" TextMode="Time" required="" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblHasta" runat="server" Text="Hasta:"></asp:Label>
                    <asp:TextBox ID="tbHoraF" TextMode="Time" required="" runat="server"></asp:TextBox><br />
                    <asp:Button ID="btnBuscarP" runat="server" CssClass="singleButton" Text="Ver"/>
                    <div class="page w3-4">
                        <div class="bs-example " data-example-id="simple-table">
                            <asp:Table ID="table" runat="server" CssClass="table">
                            </asp:Table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
