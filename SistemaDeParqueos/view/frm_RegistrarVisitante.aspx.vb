﻿Imports System.Web.Configuration
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

                DwnLstDept.Items.Add("Seleccione una opción")
                DwnLstDept.Items.Add("Jefatura")
                DwnLstDept.Items.Add("PIP")
                DwnLstDept.Items.Add("UPRO")
                DwnLstDept.Items.Add("OPO")
                DwnLstDept.Items.Add("SERT")
                DwnLstDept.Items.Add("UPROV")
                DwnLstDept.Items.Add("UVISE")
                DwnLstDept.Items.Add("Visitas")
            Else
                Dim contentPlaceHolder As ContentPlaceHolder
                Dim updatePanel2, updatePanel3 As UpdatePanel
                contentPlaceHolder = DirectCast(Page.Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)
                updatePanel2 = DirectCast(contentPlaceHolder.FindControl("UpdatePanel2"), UpdatePanel)
                updatePanel3 = DirectCast(contentPlaceHolder.FindControl("UpdatePanel3"), UpdatePanel)

                updatePanel2.Visible = False
                updatePanel3.Visible = False

                If (DwnLstProcedencia.SelectedItem.ToString().Equals("Seleccione una opción")) Then
                ElseIf (DwnLstProcedencia.SelectedItem.ToString().Equals("Interno")) Then
                    updatePanel3.Visible = True
                Else
                    updatePanel2.Visible = True
                    End If
                End If
                Else
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
            Response.BufferOutput = True
            Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
        End If
    End Sub

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Dim titulo, mensaje, tipo, tipoVisitante As String
        titulo = "ERROR"
        tipo = "error"

        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")

        If (tbIdentificacion.Text.Equals("") Or tbNombre.Text.Equals("") Or tbApellidos.Text.Equals("") Or
            tbTelefono.Text.Equals("") Or tbEmail.Text.Equals("") Or tbContrasena.Text.Equals("") Or
            tbUbicacion.Text.Equals("") Or (tbInstitucion.Text.Equals("") And Not DwnLstDept.SelectedItem.ToString().Equals("Seleccione una opcion")) Or
            (DwnLstDept.SelectedItem.ToString().Equals("Seleccione una opcion") And Not tbInstitucion.Text.Equals("")) Or
            DwnLstTipoIdentificacion.SelectedItem.ToString().Equals("Seleccione una opción") Or
            DwnLstProcedencia.SelectedItem.ToString().Equals("Seleccione una opción")) Then

            mensaje = "Debe completar todos los campos"
        ElseIf (Not email.IsMatch(tbEmail.Text)) Then
            mensaje = "Ingrese una dirección de correo válida"
        Else
            If (DwnLstProcedencia.SelectedItem.ToString().Equals("Externo")) Then
                tipoVisitante = "Externo"
            Else
                tipoVisitante = "Interno"
            End If

            Dim resultado As Integer = Me.usuarioNegocios.insertarVisitante(New Visitante(tbIdentificacion.Text, tbNombre.Text, tbApellidos.Text, tbEmail.Text,
            tbContrasena.Text, DwnLstTipoIdentificacion.SelectedItem.ToString(), "v",
            Integer.Parse(tbTelefono.Text), tbUbicacion.Text, tipoVisitante, tbInstitucion.Text))

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
                tbInstitucion.Text = ""
                DwnLstProcedencia.SelectedIndex = 0
                DwnLstTipoIdentificacion.SelectedIndex = 0
                DwnLstDept.SelectedIndex = 0
            Else
                mensaje = "Ese correo ya existe en el sistema"
            End If
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
    End Sub

End Class