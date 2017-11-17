Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class frm_SolicitudesAtrasadas
    Inherits System.Web.UI.Page

    Dim strConnectionString As String
    Dim parqueoNegocios As SP_Parqueo_Negocios
    Dim solicitudNegocios As SP_Solicitud_Parqueo_Negocios
    Dim usuarioNegocios As SP_Usuario_Negocios

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim permitido As Boolean

        For Each variableSesion As String In Session.Keys
            If (String.Equals(variableSesion, "frm_SolicitudesAtrasadas")) Then
                permitido = True
            End If
        Next

        If (permitido) Then
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
        Dim solicitudes As LinkedList(Of Solicitud) = Me.solicitudNegocios.obtenerVisitantesAtrasados()
        rowCnt = 1
        contador = 1

        For Each solicitudAct As Solicitud In solicitudes
            For rowCtr = 1 To rowCnt
                Dim filaTabla As New TableRow()
                Dim columnaNombre As New TableCell()
                Dim columnaPlaca As New TableCell()
                Dim columnaMarca As New TableCell()
                Dim columnaModelo As New TableCell()
                Dim columnaEspacioParqueo As New TableCell()
                Dim columnaHoraI As New TableCell()
                Dim columnaHoraS As New TableCell()
                Dim columnaExtension As New TableCell()
                Dim columnaBotones As New TableCell()

                filaTabla.ID = "filaTabla" + contador.ToString()
                columnaNombre.Text = solicitudAct.GstrFechaISG
                columnaPlaca.Text = solicitudAct.GstrPlacaSG
                columnaMarca.Text = solicitudAct.GstrMarcaSG
                columnaModelo.Text = solicitudAct.GstrModeloSG
                columnaEspacioParqueo.Text = solicitudAct.GintIdParqueoSG.ToString()
                columnaHoraI.Text = solicitudAct.GstrHoraISG
                columnaHoraS.Text = solicitudAct.GstrHoraFSG
                columnaExtension.ID = "columnaExtension" + contador.ToString()
                columnaBotones.ID = "columnaBotones" + contador.ToString()

                Dim tbHora As New TextBox()
                tbHora.TextMode = TextBoxMode.Time
                tbHora.Width = 80%
                tbHora.ID = "tbHora" + contador.ToString()
                tbHora.Style("padding") = "0px 0px"
                tbHora.ID = "tbHora" + contador.ToString()

                Dim literalControl As New LiteralControl()
                literalControl.Text = ""
                columnaExtension.Controls.Add(literalControl)
                columnaBotones.Controls.Add(literalControl)

                Dim btnAceptar As Button = New Button
                btnAceptar.CssClass = contador.ToString() + ";" + solicitudAct.GstrFechaFSG + ";" + solicitudAct.GstrMarcaSG + ";" + solicitudAct.GstrPlacaSG + ";" + solicitudAct.GstrModeloSG + ";" + columnaEspacioParqueo.Text + ";" + solicitudAct.GstrHoraISG + ";" + solicitudAct.GstrHoraFSG + ";1"
                btnAceptar.Text = "(Extender)"
                btnAceptar.Width = 90%
                btnAceptar.Style("color") = "#00fe00"
                AddHandler btnAceptar.Click, AddressOf Me.button_Click

                Dim btnRechazar As Button = New Button
                btnRechazar.CssClass = contador.ToString() + ";" + solicitudAct.GstrFechaFSG + ";" + solicitudAct.GstrMarcaSG + ";" + solicitudAct.GstrPlacaSG + ";" + solicitudAct.GstrModeloSG + ";" + columnaEspacioParqueo.Text + ";" + solicitudAct.GstrHoraISG + ";" + solicitudAct.GstrHoraFSG + ";0"
                btnRechazar.Text = "(Cancelar)"
                btnRechazar.Width = 90%
                btnRechazar.Style("color") = "#ff0000"
                AddHandler btnRechazar.Click, AddressOf Me.button_Click

                columnaExtension.Controls.Add(tbHora)
                columnaBotones.Controls.Add(btnAceptar)
                columnaBotones.Controls.Add(btnRechazar)

                filaTabla.Cells.Add(columnaNombre)
                filaTabla.Cells.Add(columnaPlaca)
                filaTabla.Cells.Add(columnaMarca)
                filaTabla.Cells.Add(columnaModelo)
                filaTabla.Cells.Add(columnaEspacioParqueo)
                filaTabla.Cells.Add(columnaHoraI)
                filaTabla.Cells.Add(columnaHoraS)
                filaTabla.Cells.Add(columnaExtension)
                filaTabla.Cells.Add(columnaBotones)
                tabla.Rows.Add(filaTabla)

                contador = contador + 1
            Next
        Next
    End Sub
    Protected Sub button_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim botonSeleccionado As Button = CType(sender, Button)
        Dim datosSolicitud As String() = botonSeleccionado.CssClass.Split(New String() {";"}, StringSplitOptions.None)

        Dim contentPlaceHolder As ContentPlaceHolder
        Dim updatePanel As UpdatePanel
        Dim tabla As Table
        Dim fila As TableRow
        Dim columnaExtension As TableCell
        Dim tbHora As TextBox
        Dim nuevaHora, titulo, tipo, mensaje, asuntoCorreo, mensajeCorreo As String

        contentPlaceHolder = DirectCast(Page.Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)
        updatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel1"), UpdatePanel)
        tabla = DirectCast(updatePanel.FindControl("tabla"), Table)
        fila = tabla.Rows.Item(Integer.Parse(datosSolicitud(0)))
        columnaExtension = DirectCast(fila.FindControl("columnaExtension" + datosSolicitud(0)), TableCell)
        tbHora = DirectCast(columnaExtension.FindControl("tbHora" + datosSolicitud(0)), TextBox)
        nuevaHora = tbHora.Text

        Dim resultadoAccion As Integer = Me.solicitudNegocios.decidirSolicitudAtrasada(datosSolicitud(2), datosSolicitud(3), datosSolicitud(4), datosSolicitud(5), datosSolicitud(6), datosSolicitud(7), nuevaHora, datosSolicitud(8))
        titulo = "Error"
        tipo = "error"

        If (resultadoAccion = 1 Or datosSolicitud(8).Equals("0")) Then
            titulo = "Correcto"
            tipo = "success"
            tabla.Rows.RemoveAt(Integer.Parse(datosSolicitud(0)))
            If resultadoAccion = 1 Then
                asuntoCorreo = "Extensión de tiempo de entrada"
                mensajeCorreo = " Debido a su llegada tardía, se ha modificado la hora de entrada al parqueo para las " + nuevaHora + " horas."
                mensaje = "Se ha extendido la hora de la solicitud para las " + nuevaHora + " horas."
            Else
                asuntoCorreo = "Visita cancelada"
                mensajeCorreo = " Debido a su llegada tardía, lamentamos informarle que se ha tenido que cancelar su visita para el día de hoy, por lo que no podrá ingresar al parqueo."
                mensaje = "Se canceló el ingreso al parqueo el dia de hoy para este visitante."
            End If
            Me.usuarioNegocios.envioCorreoSolicitud(asuntoCorreo, datosSolicitud(1), mensajeCorreo)
        ElseIf (resultadoAccion = 2) Then
            mensaje = "La nueva hora de entrada no puede ser después de la hora de salida."
        Else
            mensaje = "La nueva hora de entrada no puede ser antes de la actual."
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
    End Sub

End Class