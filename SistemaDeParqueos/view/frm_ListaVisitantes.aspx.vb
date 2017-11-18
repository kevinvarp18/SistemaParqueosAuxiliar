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
                Dim celdaNombre As New TableCell()
                Dim celdaMarca As New TableCell()
                Dim celdaPlaca As New TableCell()
                Dim celdaModelo As New TableCell()
                Dim celdaEspacioParqueo As New TableCell()
                Dim celdaHoraE As New TableCell()
                Dim celdaHoraS As New TableCell()
                Dim celdaBotones As New TableCell()

                celdaNombre.Text = solicitudAct.GstrFechaISG
                celdaMarca.Text = solicitudAct.GstrMarcaSG
                celdaPlaca.Text = solicitudAct.GstrPlacaSG
                celdaModelo.Text = solicitudAct.GstrModeloSG
                celdaEspacioParqueo.Text = solicitudAct.GintIdParqueoSG
                celdaHoraE.Text = solicitudAct.GstrHoraISG
                celdaHoraS.Text = solicitudAct.GstrHoraFSG

                Dim literalControl As New LiteralControl()
                literalControl.Text = ""
                celdaBotones.Controls.Add(literalControl)

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

                celdaBotones.Controls.Add(btnMarcarEntrada)
                celdaBotones.Controls.Add(btnMarcarSalida)

                filaTabla.Cells.Add(celdaNombre)
                filaTabla.Cells.Add(celdaMarca)
                filaTabla.Cells.Add(celdaPlaca)
                filaTabla.Cells.Add(celdaModelo)
                filaTabla.Cells.Add(celdaEspacioParqueo)
                filaTabla.Cells.Add(celdaHoraE)
                filaTabla.Cells.Add(celdaHoraS)
                filaTabla.Cells.Add(celdaBotones)
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