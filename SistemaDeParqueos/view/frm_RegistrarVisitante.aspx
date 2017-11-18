<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="frm_RegistrarVisitante.aspx.vb" Inherits="SistemaDeParqueos.registrarVisitante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newsletter wow fadeInUp animated animated" data-wow-delay=".5s" style="visibility: visible; animation-delay: 0.5s; animation-name: fadeInUp;">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
                <ContentTemplate>
                    <h3>Registrarse</h3>
                    <asp:Label ID="lblTipoIdentificacion" runat="server" Text="Tipo ID:"></asp:Label>
                    <asp:DropDownList ID="DwnLstTipoIdentificacion" runat="server" AutoPostBack="false"></asp:DropDownList><br />
                    <asp:Label ID="lblIdentificacion" runat="server" Text="Identificación:" onkeypress="return justNumbers(event)"></asp:Label>
                    <asp:TextBox ID="tbIdentificacion" type="text" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" onkeypress="return validar(event)"></asp:Label>
                    <asp:TextBox ID="tbNombre" type="text" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:" onkeypress="return validar(event)"></asp:Label>
                    <asp:TextBox ID="tbApellidos" type="text" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:"></asp:Label>
                    <asp:TextBox ID="tbTelefono" type="text" runat="server" onkeypress="return justNumbers(event)"></asp:TextBox><br />
                    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    <asp:TextBox ID="tbEmail" type="text" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblContrasena" runat="server" Text="Contraseña:"></asp:Label>
                    <asp:TextBox ID="tbContrasena" type="password" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblUbicacion" runat="server" Text="Ubicación:"></asp:Label>
                    <asp:TextBox ID="tbUbicacion" type="text" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblProcedenciaDwnLst" runat="server" Text="Procedencia:"></asp:Label>
                    <asp:DropDownList ID="DwnLstProcedencia" runat="server" AutoPostBack="true"></asp:DropDownList><br />
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" Visible="false">
                        <ContentTemplate>
                            <asp:Label ID="lblDepartamento" runat="server" Text="Nombre Dept:"></asp:Label>
                            <asp:DropDownList ID="DwnLstDepartamento" type="text" runat="server"></asp:DropDownList><br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" Visible="false">
                        <ContentTemplate>
                            <asp:Label ID="lblInstitucion" runat="server" Text="Institucion:"></asp:Label>
                            <asp:TextBox ID="tbInstitucion" type="text" runat="server" onkeypress="return validar(event)"></asp:TextBox><br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnRegistrar" runat="server" CssClass="singleButton" OnClick="btnRegistrar_Click" Text="Registrarse" /><br />
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
