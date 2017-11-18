Imports System.Web.Configuration
Imports Entidad
Imports Negocios
Public Class frm_AdministrarSolicitudes
    Inherits System.Web.UI.Page

    Dim strConnectionString As String
    Dim parqueoNegocios As SP_Parqueo_Negocios
    Dim solicitudNegocios As SP_Solicitud_Parqueo_Negocios
    Dim usuarioNegocios As SP_Usuario_Negocios
    Shared marca, placa, horaI, horaF, fechaI, fechaF, espacioParqueo, correo, accion As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim permitido As Boolean = False

        For Each variableSesion As String In Session.Keys
            If (String.Equals(variableSesion, "frm_AdministrarSolicitudes")) Then
                permitido = True
            End If
        Next

        If (permitido) Then
            tablaParqueos.Rows.Clear()
            llenarTablaParqueos(DateTime.Now.ToString("dd/MM/yyyy"), "07:00", "21:00")
            Me.strConnectionString = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Me.parqueoNegocios = New SP_Parqueo_Negocios(Me.strConnectionString)
            Me.solicitudNegocios = New SP_Solicitud_Parqueo_Negocios(Me.strConnectionString)
            Me.usuarioNegocios = New SP_Usuario_Negocios(Me.strConnectionString)
            llenarTablaSolicitudes()
        Else
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
            Response.BufferOutput = True
            Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
        End If
    End Sub
    Public Sub llenarTablaSolicitudes()
        Dim rowCnt As Integer
        Dim rowCtr As Integer
        Dim contador As Integer
        Dim solicitudes As LinkedList(Of Solicitud) = Me.solicitudNegocios.obtenerAdSolicitud()
        Dim listaDwnLstParqueos As New LinkedList(Of DropDownList)
        rowCnt = 1
        contador = 1

        For Each solicitudAct As Solicitud In solicitudes
            For rowCtr = 1 To rowCnt
                Dim filaTabla As New TableRow()
                Dim celdaMarca As New TableCell()
                Dim celdaPlaca As New TableCell()
                Dim celdaFechaI As New TableCell()
                Dim celdaHoraI As New TableCell()
                Dim celdaFechaS As New TableCell()
                Dim celdaHoraS As New TableCell()
                Dim celdaEspaciosParqueo As New TableCell()
                Dim celdaBotones As New TableCell()

                celdaMarca.Text = solicitudAct.GstrMarcaSG
                celdaPlaca.Text = solicitudAct.GstrPlacaSG
                celdaFechaI.Text = solicitudAct.GstrFechaISG.Substring(0, 10)
                celdaHoraI.Text = solicitudAct.GstrHoraISG
                celdaFechaS.Text = solicitudAct.GstrFechaFSG.Substring(0, 10)
                celdaHoraS.Text = solicitudAct.GstrHoraFSG
                filaTabla.ID = "filaTabla" + contador.ToString()
                celdaEspaciosParqueo.ID = "celdaParqueo" + contador.ToString()

                Dim DwnLstParqueos As New DropDownList()
                DwnLstParqueos.CssClass = "parqueosModal"
                DwnLstParqueos.AutoPostBack = False
                DwnLstParqueos.ID = "DwnLstParqueo" + contador.ToString()
                If IsPostBack Then
                Else
                    DwnLstParqueos.Items.Clear()
                End If

                Dim parqueo As LinkedList(Of Parqueo) = Me.parqueoNegocios.obtenerParqueo()
                For Each item As Parqueo In parqueo
                    If item.GintDisponibleSG <> 0 Then
                        DwnLstParqueos.Items.Add(item.GintIdentificadorSG.ToString)
                    End If
                Next

                Dim literalControl As New LiteralControl()
                literalControl.Text = ""
                celdaEspaciosParqueo.Controls.Add(literalControl)
                celdaBotones.Controls.Add(literalControl)

                Dim btnRechazar As Button = New Button
                btnRechazar.CssClass = contador.ToString() + ";" + solicitudAct.GstrMarcaSG + ";" + solicitudAct.GstrPlacaSG + ";" + solicitudAct.GstrHoraISG + ";" + solicitudAct.GstrHoraFSG + ";" + celdaFechaI.Text + ";" + celdaFechaS.Text + ";" + solicitudAct.GstrModeloSG + ";0"
                btnRechazar.Text = "(Rechazar)"
                btnRechazar.Width = 90%
                btnRechazar.Style("color") = "#ff0000"
                btnRechazar.Style("font-size") = "90%"
                AddHandler btnRechazar.Click, AddressOf Me.button_Click

                Dim btnAceptar As Button = New Button
                btnAceptar.CssClass = contador.ToString() + ";" + solicitudAct.GstrMarcaSG + ";" + solicitudAct.GstrPlacaSG + ";" + solicitudAct.GstrHoraISG + ";" + solicitudAct.GstrHoraFSG + ";" + celdaFechaI.Text + ";" + celdaFechaS.Text + ";" + solicitudAct.GstrModeloSG + ";1"
                btnAceptar.Text = "(Aceptar)"
                btnAceptar.Width = 90%
                btnAceptar.Style("color") = "#00fe00"
                btnAceptar.Style("font-size") = "90%"
                AddHandler btnAceptar.Click, AddressOf Me.button_Click

                celdaEspaciosParqueo.Controls.Add(DwnLstParqueos)
                listaDwnLstParqueos.AddLast(DwnLstParqueos)
                celdaBotones.Controls.Add(btnRechazar)
                celdaBotones.Controls.Add(btnAceptar)

                filaTabla.Cells.Add(celdaMarca)
                filaTabla.Cells.Add(celdaPlaca)
                filaTabla.Cells.Add(celdaFechaI)
                filaTabla.Cells.Add(celdaHoraI)
                filaTabla.Cells.Add(celdaFechaS)
                filaTabla.Cells.Add(celdaHoraS)
                filaTabla.Cells.Add(celdaEspaciosParqueo)
                filaTabla.Cells.Add(celdaBotones)
                tablaSolicitudes.Rows.Add(filaTabla)

                contador = contador + 1
            Next
        Next
    End Sub
    Protected Sub btnBuscarP_Click(sender As Object, e As EventArgs) Handles btnBuscarP.Click
        Dim fechai As DateTime = Convert.ToDateTime(tbFechaI.Text)
        If tbFechaI.Text <> "" AndAlso tbHoraI.Text <> "" AndAlso tbHoraF.Text <> "" Then
            tablaParqueos.Rows.Clear()
            llenarTablaParqueos(fechai, tbHoraI.Text, tbHoraF.Text)
        Else
            Dim titulo As String = "ERROR"
            Dim mensaje As String = "Debe completar todos los campos"
            Dim tipo As String = "error"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
        End If
    End Sub
    Protected Sub llenarTablaParqueos(fechai As DateTime, horaI As String, horaF As String)
        Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
        Dim parqueoNegocios As New SP_Parqueo_Negocios(strconnectionString)
        Dim solicitudNegocios As New SP_Solicitud_Parqueo_Negocios(strconnectionString)
        Dim parqueosOcupados As LinkedList(Of Parqueo) = parqueoNegocios.obtenerParqueoOcupado(fechai.ToString("dd/MM/yyyy"), horaI, horaF)
        Dim parqueosTotales As LinkedList(Of Parqueo) = parqueoNegocios.obtenerParqueo()
        Dim cantidadTiposParqueo As LinkedList(Of String) = parqueoNegocios.cantidadTiposParqueo()

        Dim rowCnt As Integer

        rowCnt = 1

        Dim tableHeaderRow As New TableHeaderRow()
        For Each tipos As String In cantidadTiposParqueo
            Dim tableHeaderCell As New TableHeaderCell()
            tableHeaderCell.Text = tipos
            tableHeaderCell.ID = tipos
            tableHeaderRow.Cells.Add(tableHeaderCell)
        Next
        tablaParqueos.Rows.Add(tableHeaderRow)
        Dim filaTabla As New TableRow()
        For rowCtr = 0 To cantidadTiposParqueo.Count - 1
            Dim celdaTabla As New TableCell()
            For Each parqueoActual As Parqueo In parqueosTotales
                Dim tipoParqueo As String
                tipoParqueo = tablaParqueos.Rows.Item(0).Cells.Item(rowCtr).ID
                Dim hyperLink As New HyperLink()
                If parqueoActual.GstrTipoSG.Equals(tipoParqueo) Then
                    Dim ocu = False
                    For Each parqueoOcupado As Parqueo In parqueosOcupados
                        If parqueoActual.GintIdentificadorSG = parqueoOcupado.GintIdentificadorSG Then
                            ocu = True
                        End If
                    Next
                    hyperLink.Text = String.Concat("Espacio ", parqueoActual.GintIdentificadorSG.ToString(), "<br/>", " ")
                    hyperLink.NavigateUrl = "frm_AdministrarParqueo.aspx?id=0;" + parqueoActual.GintIdentificadorSG.ToString() + ";" + parqueoActual.GintDisponibleSG.ToString() + ";" + parqueoActual.GstrTipoSG.ToString()
                    If parqueoActual.GintDisponibleSG = 0 Then
                        ocu = True
                    End If
                    If ocu = True Then
                        hyperLink.Style("color") = "#a30404"
                    Else
                        hyperLink.Style("color") = "#03ba03"
                    End If
                    celdaTabla.Controls.Add(hyperLink)
                End If
            Next
            filaTabla.Cells.Add(celdaTabla)
        Next
        tablaParqueos.Rows.Add(filaTabla)
    End Sub
    Protected Sub button_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim botonSeleccionado As Button = CType(sender, Button)
        Dim datosSolicitud As String() = botonSeleccionado.CssClass.Split(New String() {";"}, StringSplitOptions.None)

        Dim contentPlaceHolder As ContentPlaceHolder
        Dim updatePanel As UpdatePanel
        Dim tabla As Table
        Dim fila As TableRow
        Dim columnaEspacioD As TableCell
        Dim dwnLstParqueo As DropDownList
        Dim idParqueo As String

        contentPlaceHolder = DirectCast(Page.Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)
        updatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel1"), UpdatePanel)
        tabla = DirectCast(updatePanel.FindControl("tablaSolicitudes"), Table)
        fila = tablaSolicitudes.Rows.Item(Integer.Parse(datosSolicitud(0)))
        columnaEspacioD = DirectCast(fila.FindControl("celdaParqueo" + datosSolicitud(0)), TableCell)
        dwnLstParqueo = DirectCast(columnaEspacioD.FindControl("DwnLstParqueo" + datosSolicitud(0)), DropDownList)
        idParqueo = dwnLstParqueo.SelectedItem.Value

        marca = datosSolicitud(1)
        placa = datosSolicitud(2)
        horaI = datosSolicitud(3)
        horaF = datosSolicitud(4)
        fechaI = datosSolicitud(5)
        fechaF = datosSolicitud(6)
        espacioParqueo = idParqueo
        correo = datosSolicitud(7)
        accion = datosSolicitud(8)
        Session("fila") = datosSolicitud(0)

        If (accion.Equals("0")) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "abrirModal();", True)
        Else
            decidirSolicitud()
        End If
    End Sub
    Public Sub decidirSolicitud()
        Dim titulo As String = "ERROR"
        Dim mensaje, asuntoCorreo, mensajeCorreo As String
        Dim tipo As String = "error"
        Dim resultadoAccion As Integer

        If (accion.Equals("0")) Then
            mensaje = "La solicitud se ha rechazado exitosamente"
        Else
            mensaje = "La solicitud se ha aceptado exitosamente"

        End If

        If (accion.Equals("1")) Then
            asuntoCorreo = "Solicitud aceptada"
            mensajeCorreo = " Nos da gusto informarle que su solicitud ha sido aceptada para ingresar al parqueo "
            If (fechaI.Equals(fechaF)) Then
                mensajeCorreo = mensajeCorreo + "el día " + fechaI + " de las " + horaI + " a las " + horaF + " en el espacio " + espacioParqueo + " del parqueo."
            Else
                mensajeCorreo = mensajeCorreo + "para los días del " + fechaI + " al " + fechaF + "de " + horaI + "a " + horaF + " en el espacio " + espacioParqueo + " del parqueo."
            End If
        Else
            asuntoCorreo = "Solicitud rechazada"
            mensajeCorreo = " Lamentamos informarle que su solicitud ha sido rechazada por el siguiente motivo: " + tbRetroalimentacion.Text
        End If

        If (tbRetroalimentacion.Text.Equals("") And accion.Equals("0")) Then
            mensaje = "Debe completar todos los campos"
        Else
            resultadoAccion = Me.solicitudNegocios.decidirSolicitud(marca, placa, horaI, horaF, fechaI, fechaF, espacioParqueo, accion)

            If (resultadoAccion = 1 Or accion.Equals("0")) Then
                titulo = "Correcto"
                tipo = "success"
                Me.usuarioNegocios.envioCorreoSolicitud(asuntoCorreo, correo, mensajeCorreo)
                tablaSolicitudes.Rows.RemoveAt(Integer.Parse(Session("fila")))
            Else
                mensaje = "No se pudo aceptar la solicitud porque ese espacio ya está reservado"
            End If
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
        tbRetroalimentacion.Text = ""
    End Sub

End Class