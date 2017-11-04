Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class frm_RegistrarUsuario
    Inherits System.Web.UI.Page

    Dim connectionString As String
    Dim usuarioNegocios As SP_Usuario_Negocios

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.Equals(Session("Usuario"), "a") Then
            Me.connectionString = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Me.usuarioNegocios = New SP_Usuario_Negocios(connectionString)
            ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "frm_RegistrarUsuario", ResolveUrl("~") + "public/js/" + "script.js")

            If IsPostBack Then
                Dim contentPlaceHolder As ContentPlaceHolder
                Dim updatePanel As UpdatePanel
                contentPlaceHolder = DirectCast(Page.Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)
                updatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel2"), UpdatePanel)

                If (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Visitante")) Then
                    updatePanel.Visible = True
                Else
                    updatePanel.Visible = False
                End If

                If (DwnLstProcedencia.SelectedItem.ToString().Equals("Seleccione una opción")) Then
                    lblProcedencia2.Visible = False
                    tbProcedencia.Visible = False
                ElseIf (DwnLstProcedencia.SelectedItem.ToString().Equals("Interno")) Then
                    lblProcedencia2.Visible = True
                    tbProcedencia.Visible = True
                    lblProcedencia2.Text = "Nombre Dept:"
                    tbProcedencia.Style("margin-left") = "3.4%"
                Else
                    lblProcedencia2.Visible = True
                    tbProcedencia.Visible = True
                    lblProcedencia2.Text = "Institución:"
                    tbProcedencia.Style("margin-left") = "5.3%"
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
            End If
        Else
            Response.BufferOutput = True
            Response.Redirect("http://localhost:52086/view/frm_index.aspx")
        End If
    End Sub

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Dim titulo, mensaje, tipo As String
        Dim tbIdentificacion_ As String = Trim(tbIdentificacion.Text)
        Dim tbNombre_ As String = Trim(tbNombre.Text)
        Dim tbApellidos_ As String = Trim(tbApellidos.Text)
        Dim tbEmail_ As String = Trim(tbEmail.Text)
        Dim tbContrasena_ As String = Trim(tbContrasena.Text)
        Dim tbUbicacion_ As String = Trim(tbUbicacion.Text)
        Dim tbTelefono_ As String = Trim(tbTelefono.Text)
        Dim tbProcedencia_ As String = Trim(tbProcedencia.Text)
        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")

        If ((tbIdentificacion_.Equals("") Or tbNombre_.Equals("") Or tbApellidos_.Equals("") Or tbEmail_.Equals("") Or
            tbContrasena_.Equals("") Or DwnLstTipoIdentificacion.SelectedItem.ToString().Equals("Seleccione una opción") Or
            DwnLstTipoUsuario.SelectedItem.ToString().Equals("Seleccione una opción")) Or
            (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Visitante") And tbUbicacion_.Equals("")) Or
            (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Visitante") And tbTelefono_.Equals("")) Or
            (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Visitante") And
            DwnLstProcedencia.SelectedItem.ToString().Equals("Seleccione una opción")) Or
            (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Visitante") And
            Not DwnLstProcedencia.SelectedItem.ToString().Equals("Seleccione una opción") And tbProcedencia_.Equals(""))) Then

            titulo = "ERROR"
            mensaje = "Debe completar todos los campos"
            tipo = "error"
        ElseIf (Not email.IsMatch(tbEmail.Text)) Then
            titulo = "ERROR"
            mensaje = "Ingrese una dirección de correo válida"
            tipo = "error"
        Else
            Dim resultado As Boolean = True

            If (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Administrador")) Then
                resultado = Me.usuarioNegocios.insertarAdministrador(New Administrador(tbIdentificacion_, tbNombre_, tbApellidos_, tbEmail_, tbContrasena_, DwnLstTipoIdentificacion.SelectedItem.ToString(), "a"))
            ElseIf (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Oficial de Seguridad")) Then
                resultado = Me.usuarioNegocios.insertarOficial(New Oficial(tbIdentificacion_, tbNombre_, tbApellidos_, tbEmail_,
                tbContrasena_, DwnLstTipoIdentificacion.SelectedItem.ToString(), "o"))
            ElseIf (DwnLstTipoUsuario.SelectedItem.ToString().Equals("Visitante")) Then
                resultado = Me.usuarioNegocios.insertarVisitante(New Visitante(tbIdentificacion_, tbNombre_, tbApellidos_, tbEmail_,
                tbContrasena_, DwnLstTipoIdentificacion.SelectedItem.ToString(), "v", tbTelefono_, tbUbicacion_, DwnLstProcedencia.SelectedItem.ToString(),
                tbProcedencia_))
            End If

            If resultado Then
                titulo = "Correcto"
                mensaje = "Se ha registrado el usuario exitosamente"
                tipo = "success"
            Else
                titulo = "ERROR"
                mensaje = "No se ha podido registrar el usuario"
                tipo = "error"
            End If

            tbIdentificacion.Text = ""
            tbNombre.Text = ""
            tbApellidos.Text = ""
            tbEmail.Text = ""
            tbContrasena.Text = ""
            tbUbicacion.Text = ""
            tbProcedencia.Text = ""
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)

    End Sub

    Protected Sub DwnLstTipoUsuario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DwnLstTipoUsuario.SelectedIndexChanged

    End Sub

    Protected Sub DwnLstProcedencia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DwnLstProcedencia.SelectedIndexChanged

    End Sub
End Class