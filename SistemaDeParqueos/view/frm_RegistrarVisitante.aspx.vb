Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class registrarVisitante
    Inherits System.Web.UI.Page

    Dim connectionString As String
    Dim usuarioNegocios As SP_Usuario_Negocios

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.Equals(Session("Usuario"), "N") Then
            Me.connectionString = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()

            ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "frm_RegistrarVisitante", ResolveUrl("~") + "public/js/" + "script.js")

            Me.usuarioNegocios = New SP_Usuario_Negocios(connectionString)

            If Not IsPostBack Then

                DwnLstProcedencia.Items.Add("Seleccione una opción")
                DwnLstProcedencia.Items.Add("Externo")
                DwnLstProcedencia.Items.Add("Interno")

                DwnLstTipoIdentificacion.Items.Add("Seleccione una opción")
                DwnLstTipoIdentificacion.Items.Add("Cédula")
                DwnLstTipoIdentificacion.Items.Add("Pasaporte")
                DwnLstTipoIdentificacion.Items.Add("Licencia de conducir")
            End If

        Else
            Response.BufferOutput = True
            Response.Redirect("http://localhost:52086/view/frm_index.aspx")
        End If
    End Sub

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Dim titulo, mensaje, tipo As String
        Dim tipoVisitante As String
        Dim tbIdentificacion_ As String = Trim(tbIdentificacion.Text)
        Dim tbNombre_ As String = Trim(tbNombre.Text)
        Dim tbApellidos_ As String = Trim(tbApellidos.Text)
        Dim tbTelefono_ As String = Trim(tbTelefono.Text)
        Dim tbEmail_ As String = Trim(tbEmail.Text)
        Dim tbContrasena_ As String = Trim(tbContrasena.Text)
        Dim tbUbicacion_ As String = Trim(tbUbicacion.Text)
        Dim tbProcedencia_ As String = Trim(tbProcedencia.Text)
        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")

        If (tbIdentificacion_.Equals("") Or tbNombre_.Equals("") Or tbApellidos_.Equals("") Or tbTelefono_.Equals("") Or tbEmail_.Equals("") Or
            tbContrasena_.Equals("") Or tbUbicacion_.Equals("") Or tbProcedencia_.Equals("") Or
            DwnLstTipoIdentificacion.SelectedItem.ToString().Equals("Seleccione una opción") Or
            DwnLstProcedencia.SelectedItem.ToString().Equals("Seleccione una opción")) Then

            titulo = "ERROR"
            mensaje = "Debe completar todos los campos"
            tipo = "error"

        ElseIf (Not email.IsMatch(tbEmail_)) Then
            titulo = "ERROR"
            mensaje = "Ingrese una dirección de correo válida"
            tipo = "error"
        Else
            If (DwnLstProcedencia.SelectedItem.ToString().Equals("Externo")) Then
                tipoVisitante = "Externo"
            Else
                tipoVisitante = "Interno"
            End If

            Me.usuarioNegocios.insertarVisitante(New Visitante(tbIdentificacion.Text, tbNombre.Text, tbApellidos.Text, tbEmail.Text,
            tbContrasena.Text, DwnLstTipoIdentificacion.SelectedItem.ToString(), "v",
            Integer.Parse(tbTelefono.Text), tbUbicacion.Text, tipoVisitante, tbProcedencia.Text))

            titulo = "Correcto"
            mensaje = "Se ha registrado el visitante exitosamente"
            tipo = "success"
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
    End Sub

End Class