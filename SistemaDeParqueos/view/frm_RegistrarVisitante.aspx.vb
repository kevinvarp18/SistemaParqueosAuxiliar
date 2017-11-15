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
                DwnLstTipoIdentificacion.Items.Add("Numero de Cedula")
                DwnLstTipoIdentificacion.Items.Add("Pasaporte")
                DwnLstTipoIdentificacion.Items.Add("Licencia de conducir")
            Else
                Dim contentPlaceHolder As ContentPlaceHolder
                Dim updatePanel As UpdatePanel
                contentPlaceHolder = DirectCast(Page.Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)
                updatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel2"), UpdatePanel)

                If (DwnLstProcedencia.SelectedItem.ToString().Equals("Seleccione una opción")) Then
                    updatePanel.Visible = False
                ElseIf (DwnLstProcedencia.SelectedItem.ToString().Equals("Interno")) Then
                    updatePanel.Visible = True
                    lblProcedenciatb.Text = "Nombre Dept:"
                    tbProcedencia.Style("margin-left") = "3.9%"
                Else
                    updatePanel.Visible = True
                    lblProcedenciatb.Text = "Institución:"
                    tbProcedencia.Style("margin-left") = "5.7%"
                End If
            End If
        Else
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
            Response.BufferOutput = True
            Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
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

            Dim resultado As Integer = Me.usuarioNegocios.insertarVisitante(New Visitante(tbIdentificacion.Text, tbNombre.Text, tbApellidos.Text, tbEmail.Text,
            tbContrasena.Text, DwnLstTipoIdentificacion.SelectedItem.ToString(), "v",
            Integer.Parse(tbTelefono.Text), tbUbicacion.Text, tipoVisitante, tbProcedencia.Text))

            If (resultado = 1) Then
                titulo = "Correcto"
                mensaje = "Se ha registrado el visitante exitosamente"
                tipo = "success"

                tbNombre.Text = ""
                tbApellidos.Text = ""
                tbIdentificacion.Text = ""
                tbTelefono.Text = ""
                tbEmail.Text = ""
                tbContrasena.Text = ""
                tbUbicacion.Text = ""
                tbProcedencia.Text = ""
                DwnLstProcedencia.SelectedIndex = 0
                DwnLstTipoIdentificacion.SelectedIndex = 0
            Else
                titulo = "Error"
                mensaje = "Ese correo ya existe en el sistema"
                tipo = "error"
            End If
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
    End Sub

End Class