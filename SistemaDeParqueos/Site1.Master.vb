Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class Site1
    Inherits System.Web.UI.MasterPage

    Dim connectionString As String
    Dim solicitudNegocios As SP_Solicitud_Parqueo_Negocios
    Dim url As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If (Session("Usuario") Is Nothing) Then
                Session("Usuario") = "N"
            ElseIf (String.Equals(Session("Usuario"), "a")) Then
                Me.connectionString = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
                Me.solicitudNegocios = New SP_Solicitud_Parqueo_Negocios(connectionString)

                HypLnkSolicitudes.Text = HypLnkSolicitudes.Text + Me.solicitudNegocios.obtenerNumeroSolicitudes()
                HypLinkVisitantes.Text = HypLinkVisitantes.Text + Me.solicitudNegocios.obtenerNumeroVisitantesAtrasados()

                url = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
                HypLnkSolicitudes.NavigateUrl = url + "/view/frm_AdministrarSolicitudes.aspx"
                HypLinkVisitantes.NavigateUrl = url + "/view/frm_SolicitudesAtrasadas.aspx"
            End If 'Si la session es nula, lo inicia en N.
        End If
    End Sub

    Protected Sub cerrarSesion(sender As Object, e As EventArgs)
        Session.RemoveAll()
        Response.BufferOutput = True
        Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
    End Sub

End Class