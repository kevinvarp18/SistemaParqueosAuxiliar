Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class frm_ListaVisitantes
    Inherits System.Web.UI.Page

    Dim strConnectionString As String
    Dim solicitudNegocios As SP_Solicitud_Parqueo_Negocios

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.strConnectionString = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
        Me.solicitudNegocios = New SP_Solicitud_Parqueo_Negocios(Me.strConnectionString)

        If String.Equals(Session("Usuario"), "o") Then
            llenarTabla()
        Else
            Response.BufferOutput = True
            Response.Redirect("http://localhost:52086/view/frm_index.aspx")
        End If
    End Sub

    Public Sub llenarTabla()
        Dim rowCnt As Integer
        Dim rowCtr As Integer
        Dim contador As Integer
        Dim solicitudes As LinkedList(Of Solicitud) = solicitudNegocios.obtenerSolicitudesHoy()
        rowCnt = 1
        contador = 1

        For Each solicitudAct As Solicitud In solicitudes
            For rowCtr = 1 To rowCnt
                Dim filaTabla As New TableRow()
                Dim columnaNombre As New TableCell()
                Dim columnaMarca As New TableCell()
                Dim columnaPlaca As New TableCell()
                Dim columnaModelo As New TableCell()
                Dim columnaEspacio As New TableCell()
                Dim columnaHoraE As New TableCell()
                Dim columnaHoraS As New TableCell()
                Dim columnaHypLnk As New TableCell()

                columnaNombre.Text = solicitudAct.GstrFechaISG
                columnaMarca.Text = solicitudAct.GstrMarcaSG
                columnaPlaca.Text = solicitudAct.GstrPlacaSG
                columnaModelo.Text = solicitudAct.GstrModeloSG
                columnaEspacio.Text = solicitudAct.GintIdParqueoSG
                columnaHoraE.Text = solicitudAct.GstrHoraISG
                columnaHoraS.Text = solicitudAct.GstrHoraFSG

                Dim literalControl As New LiteralControl()
                literalControl.Text = ""
                columnaHypLnk.Controls.Add(literalControl)

                Dim lnkMarcarEntrada As New HyperLink()
                lnkMarcarEntrada.Text = "(Marcar Entrada)"
                lnkMarcarEntrada.NavigateUrl = "http://localhost:52086/view/frm_ListaVisitantes.aspx?id=1;" + contador.ToString() + ";" + solicitudAct.GstrMarcaSG + ";" + solicitudAct.GstrPlacaSG + ";" + solicitudAct.GstrModeloSG + ";" + solicitudAct.GstrHoraISG + ";" + solicitudAct.GstrHoraFSG + ";1"
                lnkMarcarEntrada.Style("font-weight") = "bold"
                lnkMarcarEntrada.Style("color") = "#0000ff"

                Dim lnkMarcarSalida As New HyperLink()
                lnkMarcarSalida.Text = "(Marcar Salida)"
                lnkMarcarSalida.NavigateUrl = "http://localhost:52086/view/frm_ListaVisitantes.aspx?id=1;" + contador.ToString() + ";" + solicitudAct.GstrMarcaSG + ";" + solicitudAct.GstrPlacaSG + ";" + solicitudAct.GstrModeloSG + ";" + solicitudAct.GstrHoraISG + ";" + solicitudAct.GstrHoraFSG + ";0"
                lnkMarcarSalida.Style("font-weight") = "bold"
                lnkMarcarSalida.Style("color") = "#0000ff"

                columnaHypLnk.Controls.Add(lnkMarcarEntrada)
                columnaHypLnk.Controls.Add(lnkMarcarSalida)

                filaTabla.Cells.Add(columnaNombre)
                filaTabla.Cells.Add(columnaMarca)
                filaTabla.Cells.Add(columnaPlaca)
                filaTabla.Cells.Add(columnaModelo)
                filaTabla.Cells.Add(columnaEspacio)
                filaTabla.Cells.Add(columnaHoraE)
                filaTabla.Cells.Add(columnaHoraS)
                filaTabla.Cells.Add(columnaHypLnk)
                tabla.Rows.Add(filaTabla)

                contador = contador + 1
            Next
        Next
    End Sub

End Class