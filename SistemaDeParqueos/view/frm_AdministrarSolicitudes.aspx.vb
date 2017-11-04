Imports System.Web.Configuration
Imports Entidad
Imports Negocios
Public Class frm_AdministrarSolicitudes
    Inherits System.Web.UI.Page

    Dim strConnectionString As String
    Dim parqueoNegocios As SP_Parqueo_Negocios
    Dim solicitudNegocios As SP_Solicitud_Parqueo_Negocios

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then
        Else
            Me.strConnectionString = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Me.parqueoNegocios = New SP_Parqueo_Negocios(Me.strConnectionString)
            Me.solicitudNegocios = New SP_Solicitud_Parqueo_Negocios(Me.strConnectionString)

            If String.Equals(Session("Usuario"), "a") Then
                llenarTabla()
                Dim idPagina As String
                idPagina = Request.QueryString("id")
                Dim datosSolicitud As String() = idPagina.Split(New String() {";"}, StringSplitOptions.None)
                idPagina = datosSolicitud(0)

                If (idPagina.Equals("1")) Then
                    decidirSolicitud(datosSolicitud(1), datosSolicitud(2), datosSolicitud(3), datosSolicitud(4), datosSolicitud(5), datosSolicitud(6), datosSolicitud(7), datosSolicitud(8))
                End If
            Else
                Response.BufferOutput = True
                Response.Redirect("http://localhost:52086/view/frm_index.aspx")
            End If
        End If

    End Sub

    Public Sub llenarTabla()
        Dim rowCnt As Integer
        Dim rowCtr As Integer
        Dim contador As Integer
        Dim solicitudes As LinkedList(Of Solicitud) = solicitudNegocios.obtenerAdSolicitud()
        rowCnt = 1
        contador = 1

        For Each solicitudAct As Solicitud In solicitudes
            For rowCtr = 1 To rowCnt
                Dim filaTabla As New TableRow()
                Dim columnaMarca As New TableCell()
                Dim columnaPlaca As New TableCell()
                Dim columnaFechaI As New TableCell()
                Dim columnaHoraI As New TableCell()
                Dim columnaFechaS As New TableCell()
                Dim columnaHoraS As New TableCell()
                Dim columnaEspaciosD As New TableCell()
                Dim columnaHypLnk As New TableCell()

                columnaMarca.Text = solicitudAct.GstrMarcaSG
                columnaPlaca.Text = solicitudAct.GstrPlacaSG
                columnaFechaI.Text = solicitudAct.GstrFechaISG.Substring(0, 10)
                columnaHoraI.Text = solicitudAct.GstrHoraISG
                columnaFechaS.Text = solicitudAct.GstrFechaFSG.Substring(0, 10)
                columnaHoraS.Text = solicitudAct.GstrHoraFSG
                filaTabla.ID = "filaTabla" + contador.ToString()
                columnaEspaciosD.ID = "columnaParqueo" + contador.ToString()

                Dim DwnLstParqueos As New DropDownList()
                DwnLstParqueos.Width = 75%
                DwnLstParqueos.AutoPostBack = False
                DwnLstParqueos.ID = "DwnLstParqueo" + contador.ToString()
                If IsPostBack Then
                    Dim parqueo As LinkedList(Of Parqueo) = Me.parqueoNegocios.obtenerParqueoHabilitado()
                    For Each item As Parqueo In parqueo
                        DwnLstParqueos.Items.Add(item.GintIdentificadorSG.ToString)
                    Next
                Else
                    DwnLstParqueos.Items.Clear()
                    Dim parqueo As LinkedList(Of Parqueo) = Me.parqueoNegocios.obtenerParqueo()
                    For Each item As Parqueo In parqueo
                        DwnLstParqueos.Items.Add(item.GintIdentificadorSG.ToString)
                    Next
                End If
                'la llamada y el fill de este drop hay q hacerlo aqui

                Dim literalControl As New LiteralControl()
                literalControl.Text = ""
                columnaEspaciosD.Controls.Add(literalControl)
                columnaHypLnk.Controls.Add(literalControl)

                Dim lnkBtnRechazar As New HyperLink()
                lnkBtnRechazar.Text = "(Rechazar)"
                lnkBtnRechazar.NavigateUrl = "http://localhost:52086/view/frm_AdministrarSolicitudes.aspx?id=1;" + contador.ToString() + ";" + solicitudAct.GstrMarcaSG + ";" + solicitudAct.GstrPlacaSG + ";" + solicitudAct.GstrHoraISG + ";" + solicitudAct.GstrHoraFSG + ";" + columnaFechaI.Text + ";" + columnaFechaS.Text + ";0"
                lnkBtnRechazar.Style("color") = "#ff0000"

                Dim lnkBtnAceptar As New HyperLink()
                lnkBtnAceptar.Text = "(Aceptar)"
                lnkBtnAceptar.NavigateUrl = "http://localhost:52086/view/frm_AdministrarSolicitudes.aspx?id=1;" + contador.ToString() + ";" + solicitudAct.GstrMarcaSG + ";" + solicitudAct.GstrPlacaSG + ";" + solicitudAct.GstrHoraISG + ";" + solicitudAct.GstrHoraFSG + ";" + columnaFechaI.Text + ";" + columnaFechaS.Text + ";1"
                lnkBtnAceptar.Style("color") = "#00fe00"

                columnaEspaciosD.Controls.Add(DwnLstParqueos)
                columnaHypLnk.Controls.Add(lnkBtnRechazar)
                columnaHypLnk.Controls.Add(lnkBtnAceptar)

                filaTabla.Cells.Add(columnaMarca)
                filaTabla.Cells.Add(columnaPlaca)
                filaTabla.Cells.Add(columnaFechaI)
                filaTabla.Cells.Add(columnaHoraI)
                filaTabla.Cells.Add(columnaFechaS)
                filaTabla.Cells.Add(columnaHoraS)
                filaTabla.Cells.Add(columnaEspaciosD)
                filaTabla.Cells.Add(columnaHypLnk)
                tabla.Rows.Add(filaTabla)

                contador = contador + 1
            Next
        Next
    End Sub

    Public Sub decidirSolicitud(idControl As String, marca As String, placa As String, horaI As String, horaF As String, fechaI As String, fechaF As String, accion As String)
        Dim contentPlaceHolder As ContentPlaceHolder
        contentPlaceHolder = DirectCast(Page.Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)
        Dim updatePanel As UpdatePanel
        updatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel1"), UpdatePanel)
        Dim tabla As Table
        tabla = DirectCast(updatePanel.FindControl("tabla"), Table)
        Dim fila As TableRow
        fila = tabla.Rows.Item(Integer.Parse(idControl))
        Dim columnaEspacioD As TableCell
        columnaEspacioD = DirectCast(fila.FindControl("columnaParqueo" + idControl), TableCell)
        Dim dwnLstParqueo As DropDownList
        dwnLstParqueo = DirectCast(columnaEspacioD.FindControl("DwnLstParqueo" + idControl), DropDownList)
        Dim idParqueo As String
        idParqueo = dwnLstParqueo.SelectedItem.Value
        Me.solicitudNegocios.decidirSolicitud(marca, placa, horaI, horaF, fechaI, fechaF, idParqueo, accion)
        tabla.Rows.Remove(fila)
    End Sub

End Class