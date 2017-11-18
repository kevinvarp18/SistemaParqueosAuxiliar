Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class frm_RegistrarUsuario
    Inherits System.Web.UI.Page

    Dim connectionString As String
    Dim usuarioNegocios As SP_Usuario_Negocios

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim permitido As Boolean = False

        For Each variableSesion As String In Session.Keys
            If (String.Equals(variableSesion, "frm_RegistrarUsuario")) Then
                permitido = True
            End If
        Next

        If (permitido) Then
            Me.connectionString = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Me.usuarioNegocios = New SP_Usuario_Negocios(connectionString)
            ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "frm_RegistrarUsuario", ResolveUrl("~") + "public/js/" + "script.js")

            If IsPostBack Then
                Dim contentPlaceHolder As ContentPlaceHolder
                Dim updatePanel2, updatePanel3, updatePanel4 As UpdatePanel
                contentPlaceHolder = DirectCast(Page.Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)
                updatePanel2 = DirectCast(contentPlaceHolder.FindControl("UpdatePanel2"), UpdatePanel)
                updatePanel3 = DirectCast(contentPlaceHolder.FindControl("UpdatePanel3"), UpdatePanel)
                updatePanel4 = DirectCast(contentPlaceHolder.FindControl("UpdatePanel4"), UpdatePanel)

                If (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Visitante")) Then
                    updatePanel2.Visible = True
                Else
                    updatePanel2.Visible = False
                End If

                If (DwnLstProcedencia.SelectedItem.ToString().Equals("Seleccione una opción")) Then
                    updatePanel3.Visible = False
                    updatePanel4.Visible = False
                ElseIf (DwnLstProcedencia.SelectedItem.ToString().Equals("Interno")) Then
                    updatePanel3.Visible = True
                    updatePanel4.Visible = False
                Else
                    updatePanel3.Visible = False
                    updatePanel4.Visible = True
                End If
            Else
                DwnLstTipoUsuario.Items.Add("Seleccione una opción")
                DwnLstTipoUsuario.Items.Add("Visitante")
                DwnLstTipoUsuario.Items.Add("Administrador")
                DwnLstTipoUsuario.Items.Add("Oficial de Seguridad")

                DwnLstTipoIdentificacion.Items.Add("Seleccione una opción")
                DwnLstTipoIdentificacion.Items.Add("Numero de Cedula")
                DwnLstTipoIdentificacion.Items.Add("Pasaporte")
                DwnLstTipoIdentificacion.Items.Add("Licencia")

                DwnLstProcedencia.Items.Add("Seleccione una opción")
                DwnLstProcedencia.Items.Add("Externo")
                DwnLstProcedencia.Items.Add("Interno")

                DwnLstDepartamento.Items.Add("Seleccione una opción")
                DwnLstDepartamento.Items.Add("Jefatura")
                DwnLstDepartamento.Items.Add("PIP")
                DwnLstDepartamento.Items.Add("UPRO")
                DwnLstDepartamento.Items.Add("OPO")
                DwnLstDepartamento.Items.Add("SERT")
                DwnLstDepartamento.Items.Add("UPROV")
                DwnLstDepartamento.Items.Add("UVISE")
            End If
        Else
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
            Response.BufferOutput = True
            Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
        End If
    End Sub

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Dim titulo As String = "ERROR"
        Dim mensaje As String
        Dim tipo As String = "error"
        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")

        If ((tbIdentificacion.Text.Equals("") Or tbNombre.Text.Equals("") Or tbApellidos.Text.Equals("") Or tbEmail.Text.Equals("") Or tbContrasena.Text.Equals("") Or DwnLstTipoIdentificacion.SelectedItem.ToString().Equals("Seleccione una opción") Or DwnLstTipoUsuario.SelectedItem.ToString().Equals("Seleccione una opción")) Or
            (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Visitante") And (tbUbicacion.Text.Equals("") Or tbTelefono.Text.Equals("") Or DwnLstProcedencia.SelectedItem.ToString().Equals("Seleccione una opción"))) Or
            (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Visitante") And Not DwnLstProcedencia.SelectedItem.ToString().Equals("Seleccione una opción") And tbInstitucion.Text.Equals("") And DwnLstDepartamento.SelectedItem.ToString().Equals("Seleccione una opción"))) Then
            mensaje = "Debe completar todos los campos"
        ElseIf (Not email.IsMatch(tbEmail.Text)) Then
            mensaje = "Ingrese una dirección de correo válida"
        Else
            Dim resultado As Boolean = True

            If (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Administrador")) Then
                resultado = Me.usuarioNegocios.insertarAdministrador(New Administrador(tbIdentificacion.Text, tbNombre.Text, tbApellidos.Text, tbEmail.Text, tbContrasena.Text, DwnLstTipoIdentificacion.SelectedItem.ToString(), "a"))
            ElseIf (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Oficial de Seguridad")) Then
                resultado = Me.usuarioNegocios.insertarOficial(New Oficial(tbIdentificacion.Text, tbNombre.Text, tbApellidos.Text, tbEmail.Text, tbContrasena.Text, DwnLstTipoIdentificacion.SelectedItem.ToString(), "o"))
            ElseIf (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Visitante")) Then
                If (DwnLstProcedencia.SelectedItem.ToString().Equals("Interno")) Then
                    resultado = Me.usuarioNegocios.insertarVisitante(New Visitante(tbIdentificacion.Text, tbNombre.Text, tbApellidos.Text, tbEmail.Text, tbContrasena.Text, DwnLstTipoIdentificacion.SelectedItem.ToString(), "v", tbTelefono.Text, tbUbicacion.Text, DwnLstProcedencia.SelectedItem.ToString(), tbInstitucion.Text))
                Else
                    resultado = Me.usuarioNegocios.insertarVisitante(New Visitante(tbIdentificacion.Text, tbNombre.Text, tbApellidos.Text, tbEmail.Text, tbContrasena.Text, DwnLstTipoIdentificacion.SelectedItem.ToString(), "v", tbTelefono.Text, tbUbicacion.Text, DwnLstProcedencia.SelectedItem.ToString(), DwnLstDepartamento.SelectedItem.Text))
                End If
            End If

            If resultado Then
                titulo = "Correcto"
                mensaje = "Se ha registrado el usuario exitosamente"
                tipo = "success"

                tbIdentificacion.Text = ""
                tbNombre.Text = ""
                tbApellidos.Text = ""
                tbEmail.Text = ""
                tbContrasena.Text = ""
                tbUbicacion.Text = ""
                tbTelefono.Text = ""
                tbInstitucion.Text = ""
                DwnLstDepartamento.SelectedIndex = 0
            Else
                mensaje = "Ya existe un usuario registrado con ese correo"
            End If
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)

    End Sub

    Protected Sub DwnLstTipoUsuario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DwnLstTipoUsuario.SelectedIndexChanged

    End Sub

    Protected Sub DwnLstProcedencia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DwnLstProcedencia.SelectedIndexChanged

    End Sub
End Class