Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class frm_BrindarAcceso
    Inherits System.Web.UI.Page

    Public gstrUsuarioSelecion As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim permitido As Boolean = False

        For Each variableSesion As String In Session.Keys
            If (String.Equals(variableSesion, "frm_BrindarParqueo")) Then
                permitido = True
            End If
        Next

        If (permitido) Then
            Dim connectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "frm_BrindarAcceso", ResolveUrl("~") + "public/js/" + "script.js")

            Dim usuariosNegocios As New SP_Usuario_Negocios(connectionString)
            If IsPostBack Then
                Dim correos As LinkedList(Of Usuario) = usuariosNegocios.obtenerCorreoUsuariosVisitantes()
                Me.gstrUsuarioSelecion = DwnLstSolicitante.SelectedItem.ToString()
                For Each usuarioActual As Usuario In correos
                    If Me.gstrUsuarioSelecion.Equals("Correo Usuario: " + usuarioActual.gstrCorreo.ToString()) Then
                        Me.gstrUsuarioSelecion = usuarioActual.gstrCorreo
                    End If
                Next
            Else
                DwnLstSolicitante.Items.Clear()
                DwnLstSolicitante.Items.Add("Seleccione una opción")
                Dim correos As LinkedList(Of Usuario) = usuariosNegocios.obtenerCorreoUsuariosVisitantes()
                For Each usuarioActual As Usuario In correos
                    DwnLstSolicitante.Items.Add("Correo Usuario: " + usuarioActual.gstrCorreo.ToString())
                Next
            End If
        Else
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
            Response.BufferOutput = True
            Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
        End If

    End Sub

    Protected Sub btnSolicitar_Click(sender As Object, e As EventArgs) Handles btnSolicitar.Click
        Dim titulo As String = "ERROR"
        Dim tipo As String = "error"
        Dim mensaje As String = "Debe completar todos los campos"

        If (DwnLstSolicitante.SelectedItem.ToString().Equals("") Or tbPlaca.Text.Equals("") Or tbMarca.Text.Equals("") Or tbModelo.Text.Equals("")) Then
        Else
            Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Dim solicitudNegocios As New SP_Solicitud_Parqueo_Negocios(strconnectionString)
            Dim fechai As DateTime = Convert.ToDateTime(tbFechaE.Text)
            Dim fechaf As DateTime = Convert.ToDateTime(tbFechaS.Text)

            If tbFechaE.Text <> "" AndAlso tbHoraE.Text <> "" Then
                solicitudNegocios.insertarSolicitud(gstrUsuarioSelecion, New Solicitud(0, 0, 0, tbHoraE.Text, tbHoraS.Text, tbPlaca.Text, tbModelo.Text, tbMarca.Text, fechai.ToString("dd/MM/yyyy"), fechaf.ToString("dd/MM/yyyy")))
                titulo = "Correcto"
                mensaje = "Acceso brindado exitosamente"
                tipo = "success"
            End If

            tbHoraE.Text = ""
            tbHoraS.Text = ""
            tbPlaca.Text = ""
            tbModelo.Text = ""
            tbFechaE.Text = ""
            tbFechaS.Text = ""
            tbMarca.Text = ""
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
    End Sub
End Class