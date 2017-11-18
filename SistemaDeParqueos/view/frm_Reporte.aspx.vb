Imports System.Web.Configuration
Imports Entidad
Imports Negocios
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports System.IO

Public Class frm_Reporte
    Inherits System.Web.UI.Page

    Dim lista As ArrayList = New ArrayList
    Dim str As String = ""
    Dim listaDatos As New LinkedList(Of String)
    Dim listaUsuarios As New LinkedList(Of Usuario)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim permitido As Boolean = False

        For Each variableSesion As String In Session.Keys
            If (String.Equals(variableSesion, "frm_Reporte")) Then
                permitido = True
            End If
        Next

        If (permitido) Then
            ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "frm_Reporte", ResolveUrl("~") + "public/js/" + "script.js")
            Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Dim usuarioNegocios As New SP_Usuario_Negocios(strconnectionString)
            Dim solicitudNegocios As New SP_Solicitud_Parqueo_Negocios(strconnectionString)

            If IsPostBack Then
                Me.lista = Me.lista
                Me.str = Me.str

                Dim contentPlaceHolder As ContentPlaceHolder = DirectCast(Page.Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)
                Dim updatePanelTipo As UpdatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel2"), UpdatePanel)
                Dim updatePanelFechas As UpdatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel3"), UpdatePanel)
                updatePanelFechas.Visible = False
                updatePanelTipo.Visible = True
                DwnLstDatos.Items.Clear()
                DwnLstDatos.Items.Add("Seleccione una opción")

                If (DwnLstTipoReporte.SelectedItem.ToString().Equals("Placa")) Then
                    lblTipoEscogido.Text = "Placa:"
                    listaDatos = usuarioNegocios.obtenerPlacas()
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Correo")) Then
                    lblTipoEscogido.Text = "Correo:"
                    listaUsuarios = usuarioNegocios.obtenerCorreoUsuariosVisitantes()
                    For Each datosUsuario As Usuario In listaUsuarios
                        DwnLstDatos.Items.Add(datosUsuario.GstrCorreoSG)
                    Next
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Nombre")) Then
                    lblTipoEscogido.Text = "Nombre"
                    listaUsuarios = solicitudNegocios.ObtenerCedulasYNombres()
                    For Each datosUsuario As Usuario In listaUsuarios
                        DwnLstDatos.Items.Add(datosUsuario.GstrIdSG + " - " + datosUsuario.GstrNombreSG + " " + datosUsuario.GstrApellidoSG)
                    Next
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Fecha")) Then
                    updatePanelTipo.Visible = False
                    updatePanelFechas.Visible = True
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Institucion")) Then
                    lblTipoEscogido.Text = "Institución:"
                    listaDatos = solicitudNegocios.ObtenerInstituciones()
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Departamento")) Then
                    lblTipoEscogido.Text = "Departamento:"
                    listaDatos = solicitudNegocios.ObtenerDepartamentos()
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Seleccione una opción")) Then
                    updatePanelTipo.Visible = False
                    updatePanelFechas.Visible = False
                End If


                For Each datos As String In listaDatos
                    DwnLstDatos.Items.Add(datos)
                Next
            Else
                Me.lista = New ArrayList
                Me.str = ""

                DwnLstTipoReporte.Items.Add("Seleccione una opción")
                DwnLstTipoReporte.Items.Add("Placa")
                DwnLstTipoReporte.Items.Add("Nombre")
                DwnLstTipoReporte.Items.Add("Correo")
                DwnLstTipoReporte.Items.Add("Institucion")
                DwnLstTipoReporte.Items.Add("Departamento")
                DwnLstTipoReporte.Items.Add("Fecha")
            End If
        Else
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
            Response.BufferOutput = True
            Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        selecciones("reporte")
    End Sub
    Public Sub construyeTabla(solicitudes As LinkedList(Of Solicitud))
        Dim rowCnt As Integer
        Dim rowCtr As Integer
        rowCnt = 1

        For Each solicitudAct As Solicitud In solicitudes
            For rowCtr = 1 To rowCnt
                Dim filaTabla As New TableRow()
                Dim celdaMarca As New TableCell()
                Dim celdaModelo As New TableCell()
                Dim celdaPlaca As New TableCell()
                Dim celdaFechaI As New TableCell()
                Dim celdaHoraI As New TableCell()
                Dim celdaFechaF As New TableCell()
                Dim celdaHoraF As New TableCell()
                Dim celdaEspacioParqueo As New TableCell()

                celdaMarca.Text = solicitudAct.GstrMarcaSG
                celdaModelo.Text = solicitudAct.GstrModeloSG
                celdaPlaca.Text = solicitudAct.GstrPlacaSG
                celdaFechaI.Text = solicitudAct.GstrFechaISG.Substring(0, 10)
                celdaHoraI.Text = solicitudAct.GstrHoraISG
                celdaFechaF.Text = solicitudAct.GstrFechaFSG.Substring(0, 10)
                celdaHoraF.Text = solicitudAct.GstrHoraFSG
                celdaEspacioParqueo.Text = solicitudAct.GintIdParqueoSG

                filaTabla.Cells.Add(celdaMarca)
                filaTabla.Cells.Add(celdaModelo)
                filaTabla.Cells.Add(celdaPlaca)
                filaTabla.Cells.Add(celdaFechaI)
                filaTabla.Cells.Add(celdaHoraI)
                filaTabla.Cells.Add(celdaFechaF)
                filaTabla.Cells.Add(celdaHoraF)
                filaTabla.Cells.Add(celdaEspacioParqueo)
                table.Rows.Add(filaTabla)
            Next
        Next
    End Sub
    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        selecciones("pdf")
        Try
            Dim doc As New Document(PageSize.A4.Rotate(), 10, 10, 10, 10)
            Dim filename As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\Reporte de parqueo.pdf"
            Dim file As New FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
            PdfWriter.GetInstance(doc, file)
            doc.Open()
            ExportarDatosPDF(doc, Me.str, selecciones("pdf"))
            doc.Close()
            Process.Start(filename)
        Catch ex As Exception
        End Try
    End Sub
    Public Function selecciones(accion As String) As LinkedList(Of Solicitud)
        'Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
        'Dim parqueoNegocios As New SP_Parqueo_Negocios(strconnectionString)
        'Dim titulo As String = "ERROR"
        'Dim mensaje As String = "Ha ocurrido un error"
        'Dim tipo As String = "error"
        'Dim solicitudNegocios As New SP_Solicitud_Parqueo_Negocios(strconnectionString)
        'Dim solicitudes As LinkedList(Of Solicitud)
        'Dim faltanDatos As Boolean = True

        'If DwnLstTipoReporte.SelectedItem.ToString().Equals("Seleccione una opción") Then
        '    titulo = "Incompleto"
        '    mensaje = "Debe seleccionar un tipo de reporte"
        '    tipo = "warning"
        '    faltanDatos = True
        'ElseIf tbFechaI.Text <> "" AndAlso tbFechaF.Text <> "" AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Fecha") Then
        '    If (tbFechaI.Text <= tbFechaF.Text) Then
        '        solicitudes = solicitudNegocios.obtenerReporte(tbFechaI.Text, tbFechaF.Text)

        '        If solicitudes.Count.Equals(0) Then
        '            titulo = "Vacio"
        '            mensaje = "No se encontraron datos para las fecha seleccionadas"
        '            tipo = "info"
        '            faltanDatos = True
        '        Else
        '            If accion.Equals("reporte") Then
        '                Me.construyeTabla(solicitudes)
        '            End If
        '            faltanDatos = False
        '        End If
        '    Else
        '        titulo = "ERROR"
        '        mensaje = "La fecha de salida debe ser mayor a la fecha de entrada"
        '        tipo = "error"
        '        faltanDatos = True
        '    End If
        'ElseIf (Not DwnLstDatos.SelectedItem.ToString().Equals("Seleccione una opción")) AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Placa") Then
        '    solicitudes = solicitudNegocios.obtenerReportePlaca(DwnLstDatos.SelectedItem.ToString())
        '    If solicitudes.Count.Equals(0) Then
        '        titulo = "Vacio"
        '        mensaje = "No se encontraron datos para la placa seleccionada"
        '        tipo = "info"
        '        faltanDatos = True
        '    Else
        '        If accion.Equals("reporte") Then
        '            Me.construyeTabla(solicitudes)
        '        End If
        '        faltanDatos = False
        '    End If
        'ElseIf (Not DwnLstNombre.SelectedItem.ToString().Equals("Seleccione una opción")) AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Nombre") Then
        '    solicitudes = solicitudNegocios.obtenerReporteCedula(DwnLstNombre.SelectedItem.ToString())
        '    If solicitudes.Count.Equals(0) Then
        '        titulo = "Vacio"
        '        mensaje = "No se encontraron datos para el usuario seleccionado"
        '        tipo = "info"
        '        faltanDatos = True
        '    Else
        '        If accion.Equals("reporte") Then
        '            Me.construyeTabla(solicitudes)
        '        End If
        '        faltanDatos = False
        '    End If
        'ElseIf (Not DwnLstInstitucion.SelectedItem.ToString().Equals("Seleccione una opción")) AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Institucion") Then
        '    solicitudes = solicitudNegocios.obtenerReporteInstitucion(DwnLstInstitucion.SelectedItem.ToString())
        '    If solicitudes.Count.Equals(0) Then
        '        titulo = "Vacio"
        '        mensaje = "No se encontraron datos para el usuario seleccionado"
        '        tipo = "info"
        '        faltanDatos = True
        '    Else
        '        If accion.Equals("reporte") Then
        '            Me.construyeTabla(solicitudes)
        '        End If
        '        faltanDatos = False
        '    End If
        'ElseIf (Not DwnLstDepartamento.SelectedItem.ToString().Equals("Seleccione una opción")) AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Departamento") Then
        '    solicitudes = solicitudNegocios.obtenerReporteDepartamento(DwnLstDepartamento.SelectedItem.ToString())
        '    If solicitudes.Count.Equals(0) Then
        '        titulo = "Vacio"
        '        mensaje = "No se encontraron datos para el usuario seleccionado"
        '        tipo = "info"
        '        faltanDatos = True
        '    Else
        '        If accion.Equals("reporte") Then
        '            Me.construyeTabla(solicitudes)
        '        End If
        '        faltanDatos = False
        '    End If
        'ElseIf (Not DwnLstCorreo.SelectedItem.ToString().Equals("Seleccione una opción")) AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Correo") Then
        '    solicitudes = solicitudNegocios.obtenerReporteCorreo(DwnLstCorreo.SelectedItem.ToString())
        '    If solicitudes.Count.Equals(0) Then
        '        titulo = "Vacio"
        '        mensaje = "No se encontraron entradas para el correo seleccionado"
        '        tipo = "info"
        '        faltanDatos = True
        '    Else
        '        If accion.Equals("reporte") Then
        '            Me.construyeTabla(solicitudes)
        '        End If
        '        faltanDatos = False
        '    End If
        'Else
        '    titulo = "Incompleto"
        '    mensaje = "Debe completar todos los campos"
        '    tipo = "warning"
        '    faltanDatos = True
        'End If

        'If faltanDatos Then
        '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
        'End If


        'Label1.Text = String.Join("", Me.lista.ToArray()).ToString

        'Return solicitudes
    End Function
    Public Sub ExportarDatosPDF(ByVal document As Document, ByVal str As String, solicitudes As LinkedList(Of Solicitud))
        Dim fuente As iTextSharp.text.pdf.BaseFont
        fuente = FontFactory.GetFont(FontFactory.HELVETICA, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL).BaseFont
        Dim strContent As String = str
        Dim parsedHtmlElements As List(Of IElement)
        Dim llenar As String = "
        <div><h1> Reporte de Parqueo</h1></div>
                   <table BORDER ='1' >
        '           <tr>
        '               <th><strong>Nombre</strong></th>
        '               <th><strong>Instituci&oacute;n</strong></th>
        '               <th><strong>Placa</strong></th>
        '               <th><strong>Fecha Entrada</strong></th>
        '               <th><strong>Hora Entrada</strong></th>
        '               <th><strong>Fecha Salida</strong></th>
        '               <th><strong>Hora Salida</strong></th>
        '               <th><strong>Espacio</strong></th>
        '           </tr>"

        For Each solicitudAct As Solicitud In solicitudes
            llenar += "<tr>" + "<td>" + solicitudAct.GstrMarcaSG + "</td>" + "<td>" + " " + "</td>" + "<td>" + solicitudAct.GstrPlacaSG +
            "</td>" + "<td>" + solicitudAct.GstrFechaISG.Substring(0, 10) + "</td>" + "<td>" + solicitudAct.GstrHoraISG + "</td>" + "<td>" + solicitudAct.GstrFechaFSG.Substring(0, 10) + "</td>" + "<td>" + solicitudAct.GstrHoraFSG + "</td>" + "<td>" + solicitudAct.GintIdParqueoSG.ToString() + "</td>" + "</tr>"
        Next

        llenar += "</table>" + "<br>"
        strContent = llenar
        parsedHtmlElements = HTMLWorker.ParseToList(New StringReader(strContent), Nothing)
        For Each htmlElement As IElement In parsedHtmlElements
            document.Add(htmlElement)
        Next
    End Sub

End Class