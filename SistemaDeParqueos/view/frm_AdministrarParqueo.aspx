<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_AdministrarParqueo.aspx.vb" Inherits="SistemaDeParqueos.administrarParqueo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <h3>Administrar Parqueo</h3>
                    <div>
                            <asp:Label ID="lblTipo" runat="server" Text="Tipo:"></asp:Label>
                            <asp:DropDownList ID="DwnLstTipos" runat="server" style="margin-left: 8.4%;" AutoPostBack="false"></asp:DropDownList><br />
                            <asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label>
                            <asp:DropDownList ID="DwnLstEstado" runat="server" style="margin-left: 7%;" AutoPostBack="false"></asp:DropDownList> <br />                        
                    </div>                                  
                    <br />
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
             <asp:updatepanel id="UpdatePanel2" runat="server" visible="false">
                <ContentTemplate>
                       <asp:Button ID="btnActualizar" runat="server" style="margin-left: 28%;" CssClass="multipleTextBox" OnClick="btnActualizar_Click" Text="Actualizar" />
                        <asp:Button ID="btnEliminar"  runat="server"  CssClass="multipleTextBox" OnClick="btnEliminar_Click" Text="Eliminar" />
                </ContentTemplate>
            </asp:updatepanel>
            <asp:updatepanel id="UpdatePanel3" runat="server" visible="false">
                <ContentTemplate>
                       <asp:Button ID="btnCrear"  runat="server" CssClass="multipleTextBox" style="margin-top: -50%;margin-left: 40%;" OnClick="btnCrear_Click" Text="Crear" />
                </ContentTemplate>
            </asp:updatepanel>
        </div>
    </div>
</asp:Content>
