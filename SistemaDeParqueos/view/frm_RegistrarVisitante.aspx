<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_RegistrarVisitante.aspx.vb" Inherits="SistemaDeParqueos.registrarVisitante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <h3>Registrarse</h3>
                    <asp:Label ID="lblTipoIdentificacion" runat="server" Text="Tipo Identificación:"></asp:Label>
                    <asp:DropDownList ID="DwnLstTipoIdentificacion" runat="server" AutoPostBack="false" Style="width: 27%;"></asp:DropDownList><br />
                    <asp:Label ID="lblIdentificacion" runat="server" Text="# Identificación:"></asp:Label>
                    <asp:TextBox ID="tbIdentificacion" type="text" runat="server" Style="width: 27%; margin-left: 3%;"></asp:TextBox><br />
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                    <asp:TextBox ID="tbNombre" type="text" runat="server" Style="margin-left: 7.3%; width: 27%;"></asp:TextBox><br />
                    <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label>
                    <asp:TextBox ID="tbApellidos" type="text" Style="margin-left: 6.8%; width: 27%;" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:"></asp:Label>
                    <asp:TextBox ID="tbTelefono" type="number" runat="server" Style="margin-left: 7.2%; width: 27%;"></asp:TextBox><br />
                    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    <asp:TextBox ID="tbEmail" type="text" runat="server" Style="margin-left: 9.1%; width: 27%;"></asp:TextBox><br />
                    <asp:Label ID="lblContrasena" runat="server" Text="Contraseña:"></asp:Label>
                    <asp:TextBox ID="tbContrasena" type="password" runat="server" Style="margin-left: 5.4%; width: 27%;"></asp:TextBox><br />
                    <asp:Label ID="lblUbicacion" runat="server" Text="Ubicación:"></asp:Label>
                    <asp:TextBox ID="tbUbicacion" type="text" runat="server" Style="margin-left: 6.4%; width: 27%;"></asp:TextBox><br />
                    <asp:Label ID="lblProcedenciaDwnLst" runat="server" Text="Procedencia:"></asp:Label>
                    <asp:DropDownList ID="DwnLstProcedencia" runat="server" Style="margin-left: 4.8%; width: 27%;" AutoPostBack="true"></asp:DropDownList><br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="false">
                        <ContentTemplate>
                            <asp:Label ID="lblProcedenciatb" runat="server" Text="Nombre Dept:"></asp:Label>
                            <asp:TextBox ID="tbProcedencia" type="text" runat="server" Style="margin-left: 4%; width: 27%;"></asp:TextBox><br />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:Button ID="btnRegistrar" runat="server" CssClass="singleTextBox" OnClick="btnRegistrar_Click" Text="Registrarse" /><br />
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
