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

        Dim permitido As Boolean = False

        For Each variableSesion As String In Session.Keys
            If (String.Equals(variableSesion, "frm_ListaVisitantes")) Then
                permitido = True
            End If
        Next

        If (permitido) Then

            llenarTabla()
        Else
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
            Response.BufferOutput = True
            Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
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

                Dim btnMarcarEntrada As New Button()
                If (solicitudAct.GintIdSolicutudSG = 0) Then
                    btnMarcarEntrada.Text = "Marcar Entrada"
                    btnMarcarEntrada.Style("font-size") = "0.70em"
                Else
                    btnMarcarEntrada.Text = "Desmarcar Entrada"
                    btnMarcarEntrada.Style("font-size") = "0.60em"
                End If
                btnMarcarEntrada.Width = 100%
                btnMarcarEntrada.CssClass = solicitudAct.GstrMarcaSG + ";" + solicitudAct.GstrPlacaSG + ";" + solicitudAct.GstrModeloSG + ";" + solicitudAct.GintIdParqueoSG.ToString() + ";" + solicitudAct.GstrHoraISG + ";" + solicitudAct.GstrHoraFSG + ";1"
                btnMarcarEntrada.Style("color") = "#ff0000"

                AddHandler btnMarcarEntrada.Click, AddressOf Me.button_Click

                Dim btnMarcarSalida As New Button()
                If (solicitudAct.GintIdVisitanteSG = 0) Then
                    btnMarcarSalida.Text = "Marcar Salida"
                    btnMarcarSalida.Style("font-size") = "0.70em"
                Else
                    btnMarcarSalida.Text = "Desmarcar Salida"
                    btnMarcarSalida.Style("font-size") = "0.60em"
                End If
                btnMarcarSalida.Width = 100%
                btnMarcarSalida.CssClass = solicitudAct.GstrMarcaSG + ";" + solicitudAct.GstrPlacaSG + ";" + solicitudAct.GstrModeloSG + ";" + solicitudAct.GintIdParqueoSG.ToString() + ";" + solicitudAct.GstrHoraISG + ";" + solicitudAct.GstrHoraFSG + ";0"
                btnMarcarSalida.Style("color") = "#00fe00"
                btnMarcarSalida.Style("font-size") = "0.70em"
                AddHandler btnMarcarSalida.Click, AddressOf Me.button_Click

                columnaHypLnk.Controls.Add(btnMarcarEntrada)
                columnaHypLnk.Controls.Add(btnMarcarSalida)

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

    Protected Sub button_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim botonSeleccionado As Button = CType(sender, Button)
        Dim datosSolictud As String() = botonSeleccionado.CssClass.Split(New String() {";"}, StringSplitOptions.None)
        solicitudNegocios.marcarEntrada_Salida(datosSolictud(0), datosSolictud(1), datosSolictud(2), Integer.Parse(datosSolictud(3)), datosSolictud(4), datosSolictud(5), Integer.Parse(datosSolictud(6)))

        If (botonSeleccionado.Text.Equals("Marcar Entrada")) Then
            botonSeleccionado.Text = "Desmarcar Entrada"
            botonSeleccionado.Style("font-size") = "0.60em"
        ElseIf (botonSeleccionado.Text.Equals("Desmarcar Entrada")) Then
            botonSeleccionado.Text = "Marcar Entrada"
            botonSeleccionado.Style("font-size") = "0.70em"
        ElseIf (botonSeleccionado.Text.Equals("Marcar Salida")) Then
            botonSeleccionado.Text = "Desmarcar Salida"
            botonSeleccionado.Style("font-size") = "0.60em"
        ElseIf (botonSeleccionado.Text.Equals("Desmarcar Salida")) Then
            botonSeleccionado.Style("font-size") = "0.70em"
            botonSeleccionado.Text = "Marcar Salida"
        End If
    End Sub

End Class