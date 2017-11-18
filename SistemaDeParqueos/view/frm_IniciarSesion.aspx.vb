Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class loginView
    Inherits System.Web.UI.Page

    Dim connectionString, titulo, mensaje, tipo As String
    Dim usuarioNegocios As SP_Usuario_Negocios
    Dim email As Regex
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.Equals(Session("Usuario"), "N") Then
            Me.connectionString = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Me.usuarioNegocios = New SP_Usuario_Negocios(connectionString)
            Response.BufferOutput = True
        Else
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
            Response.BufferOutput = True
            Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
        End If

        email = New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")

    End Sub

    Protected Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        titulo = "ERROR"
        tipo = "error"
        If (tbUsuario.Text.Equals("") Or tbContrasena.Text.Equals("")) Then
            mensaje = "Debe completar todos los campos"
        ElseIf (Not email.IsMatch(tbUsuario.Text)) Then
            mensaje = "Ingrese una dirección de correo válida"
        Else
            Dim usuarios As LinkedList(Of Usuario) = usuarioNegocios.obtenerUsuarios(tbUsuario.Text)
            If (usuarios.Count > 0) Then
                For Each usuarioActual As Usuario In usuarios
                    If (usuarioActual.GstrContrasennaSG.Equals(tbContrasena.Text)) Then
                        Session("Correo") = usuarioActual.gstrCorreo
                        Session("Usuario") = usuarioActual.gstrTipoUsuario

                        Dim listaPermisos As New LinkedList(Of Permiso)()
                        listaPermisos = usuarioNegocios.obtenerPermisosPorRol(usuarioActual.gstrTipoUsuario.ToString)

                        If listaPermisos.Count > 0 Then
                            For Each permiso As Permiso In listaPermisos
                                Session(permiso.GstrTipo1.ToString) = permiso.GstrTipo1.ToString
                            Next
                        End If

                        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
                        Response.BufferOutput = True
                        Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
                    Else
                        mensaje = "Contraseña incorrecta"
                    End If
                Next
            Else
                mensaje = "No hay ningún usuario registrado con ese correo electrónico"
            End If 'Verifica si el usuario existe o no.
        End If 'Verifica si los datos se insertaron o no y que sean correctos.
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
    End Sub
    Protected Sub enviarCorreo()
        titulo = "ERROR"
        tipo = "error"
        If (tbUsuario.Text.Equals("")) Then
            mensaje = "Debe completar todos los campos"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
        ElseIf (Not email.IsMatch(tbUsuario.Text)) Then
            mensaje = "Ingrese una dirección de correo válida"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
        Else
            Dim strCorreo = tbUsuario.Text
            Dim connectionString = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Dim usuario_Negocios = New SP_Usuario_Negocios(Me.connectionString)
            Dim blnRespuesta = usuarioNegocios.recuperacionContrasenaMail(strCorreo)
            If (blnRespuesta) Then
                titulo = "Correcto"
                mensaje = "Se ha recuperado su contraseña y enviado a su dirección de correo electrónico"
                tipo = "success"
            Else
                mensaje = "Los datos del usuario no existen"
            End If
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
        End If
    End Sub
End Class