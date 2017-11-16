Imports System.Web.Configuration
Imports Entidad
Imports Negocios
Public Class frm_SolicitarParqueo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim permitido As Boolean = False

        For Each variableSesion As String In Session.Keys
            If (String.Equals(variableSesion, "frm_SolicitarParqueo")) Then
                permitido = True
            End If
        Next

        If (permitido) Then
            lblUsuario.Text = Session("Correo")
            ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "frm_SolicitarParqueo", ResolveUrl("~") + "public/js/" + "script.js")
        Else
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
            Response.BufferOutput = True
            Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
        End If
    End Sub

    Protected Sub btnSolicitar_Click(sender As Object, e As EventArgs) Handles btnSolicitar.Click
        Dim titulo, mensaje, tipo As String
        Dim tbPlaca_ As String = Trim(tbPlaca.Text)
        Dim tbMarca_ As String = Trim(tbMarca.Text)
        Dim tbModelo_ As String = Trim(tbModelo.Text)

        If (tbPlaca_.Equals("") Or tbMarca_.Equals("") Or tbModelo_.Equals("")) Then
            titulo = "ERROR"
            mensaje = "Debe completar todos los campos"
            tipo = "error"
        Else
            Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Dim solicitudNegocios As New SP_Solicitud_Parqueo_Negocios(strconnectionString)
            Dim fechai As DateTime = Convert.ToDateTime(tbFechaE.Text)
            Dim fechaf As DateTime = Convert.ToDateTime(tbFechaS.Text)

            If tbFechaE.Text <> "" AndAlso tbHoraE.Text <> "" AndAlso tbHoraS.Text <> "" AndAlso tbFechaS.Text <> "" AndAlso tbPlaca.Text <> "" AndAlso tbPlaca.Text <> "" AndAlso tbMarca.Text <> "" AndAlso tbModelo.Text <> "" Then
                solicitudNegocios.insertarSolicitud(Session("Correo"), New Solicitud(0, 0, 0, tbHoraE.Text, tbHoraS.Text, tbPlaca.Text, tbModelo.Text, tbMarca.Text, fechai.ToString("dd/MM/yyyy"), fechaf.ToString("dd/MM/yyyy")))
                titulo = "Correcto"
                mensaje = "La solicitud se ha enviado exitosamente"
                tipo = "success"
            Else
                titulo = "ERROR"
                mensaje = "Debe completar todos los campos"
                tipo = "error"
            End If

            tbFechaE.Text = ""
            tbFechaS.Text = ""
            tbHoraE.Text = ""
            tbHoraS.Text = ""
            tbPlaca.Text = ""
            tbModelo.Text = ""
            tbMarca.Text = ""
            tbModelo.Text = ""
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)

    End Sub
End Class