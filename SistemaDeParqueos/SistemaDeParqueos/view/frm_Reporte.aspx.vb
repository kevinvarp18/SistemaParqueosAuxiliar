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



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim permitido As Boolean = False

        For Each variableSesion As String In Session.Keys
            If (String.Equals(variableSesion, "frm_Reporte")) Then
                permitido = True
            End If
        Next

        'If (permitido) Then
        ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "frm_Reporte", ResolveUrl("~") + "public/js/" + "script.js")
            Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Dim sn As New SP_Usuario_Negocios(strconnectionString)
            Dim snSol As New SP_Solicitud_Parqueo_Negocios(strconnectionString)


            If IsPostBack Then

                Me.lista = Me.lista
                Me.str = Me.str

                Dim contentPlaceHolder As ContentPlaceHolder = DirectCast(Page.Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)
                Dim updatePanelPlaca As UpdatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel2"), UpdatePanel)
                Dim updatePanelCorreo As UpdatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel3"), UpdatePanel)
                Dim updatePanelNombre As UpdatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel4"), UpdatePanel)
                Dim updatePanelFecha As UpdatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel5"), UpdatePanel)
                Dim updatePanelTabla As UpdatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel6"), UpdatePanel)
                Dim updatePanelInstitucion As UpdatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel9"), UpdatePanel)
                Dim updatePanelDepartamento As UpdatePanel = DirectCast(contentPlaceHolder.FindControl("UpdatePanel10"), UpdatePanel)



                If (DwnLstTipoReporte.SelectedItem.ToString().Equals("Placa")) Then
                    updatePanelPlaca.Visible = True
                    updatePanelCorreo.Visible = False
                    updatePanelFecha.Visible = False
                    updatePanelNombre.Visible = False
                    updatePanelInstitucion.Visible = False
                    updatePanelDepartamento.Visible = False
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Correo")) Then
                    updatePanelPlaca.Visible = False
                    updatePanelCorreo.Visible = True
                    updatePanelFecha.Visible = False
                    updatePanelNombre.Visible = False
                    updatePanelInstitucion.Visible = False
                    updatePanelDepartamento.Visible = False
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Nombre")) Then
                    updatePanelPlaca.Visible = False
                    updatePanelCorreo.Visible = False
                    updatePanelFecha.Visible = False
                    updatePanelNombre.Visible = True
                    updatePanelInstitucion.Visible = False
                    updatePanelDepartamento.Visible = False
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Fecha")) Then
                    updatePanelPlaca.Visible = False
                    updatePanelCorreo.Visible = False
                    updatePanelFecha.Visible = True
                    updatePanelNombre.Visible = False
                    updatePanelInstitucion.Visible = False
                    updatePanelDepartamento.Visible = False
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Institucion")) Then
                    updatePanelPlaca.Visible = False
                    updatePanelCorreo.Visible = False
                    updatePanelFecha.Visible = False
                    updatePanelNombre.Visible = False
                    updatePanelInstitucion.Visible = True
                    updatePanelDepartamento.Visible = False
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Departamento")) Then
                    updatePanelPlaca.Visible = False
                    updatePanelCorreo.Visible = False
                    updatePanelFecha.Visible = False
                    updatePanelNombre.Visible = False
                    updatePanelInstitucion.Visible = False
                    updatePanelDepartamento.Visible = True
                ElseIf (DwnLstTipoReporte.SelectedItem.ToString().Equals("Seleccione una opción")) Then
                    updatePanelPlaca.Visible = False
                    updatePanelCorreo.Visible = False
                    updatePanelFecha.Visible = False
                    updatePanelNombre.Visible = False
                    updatePanelInstitucion.Visible = False
                    updatePanelDepartamento.Visible = False
                End If
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

                DwnLstPlaca.Items.Add("Seleccione una opción")
                Dim placas As LinkedList(Of String) = sn.obtenerPlacas()
                For Each placa As String In placas
                    DwnLstPlaca.Items.Add(placa)
                Next

                DwnLstCorreo.Items.Add("Seleccione una opción")
                Dim usuariosCorreo As LinkedList(Of Usuario) = sn.obtenerCorreoUsuariosVisitantes()
                For Each usuarioCorreo As Usuario In usuariosCorreo
                    DwnLstCorreo.Items.Add(usuarioCorreo.GstrCorreoSG)
                Next

                DwnLstNombre.Items.Add("Seleccione una opción")
                Dim usuariosCedulas As LinkedList(Of Usuario) = snSol.ObtenerCedulasYNombres()
                For Each uc As Usuario In usuariosCedulas
                    DwnLstNombre.Items.Add(uc.GstrIdSG + " - " + uc.GstrNombreSG + " " + uc.GstrApellidoSG)
                Next

                DwnLstDepartamento.Items.Add("Seleccione una opción")
                Dim departamentos As LinkedList(Of String) = snSol.ObtenerDepartamentos()
                For Each departamento As String In departamentos
                    DwnLstDepartamento.Items.Add(departamento)
                Next

                DwnLstInstitucion.Items.Add("Seleccione una opción")
                Dim instituciones As LinkedList(Of String) = snSol.ObtenerInstituciones()
                For Each institucion As String In instituciones
                    DwnLstInstitucion.Items.Add(institucion)
                Next

            End If
        'Else
        '    Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
        '    Response.BufferOutput = True
        '    Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
        'End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        selecciones("reporte")


    End Sub


    Public Function construyeTabla(solicitudes As LinkedList(Of Solicitud))
        Dim rowCnt As Integer
        Dim rowCtr As Integer
        Dim cellCnt As Integer

        Dim llenar As String = ""

        rowCnt = 1
        cellCnt = 7
        Dim tERow As New TableRow()
        Dim nom As New TableHeaderCell()
        Dim inst As New TableHeaderCell()
        Dim pla As New TableHeaderCell()
        Dim fecha_e As New TableHeaderCell()
        Dim hora_e As New TableHeaderCell()
        Dim fecha_s As New TableHeaderCell()
        Dim hora_s As New TableHeaderCell()
        Dim num_p As New TableHeaderCell()
        nom.Text = "Nombre"
        inst.Text = "Institución"
        pla.Text = "Placa"
        fecha_e.Text = "Fecha Entrada"
        hora_e.Text = "Hora Entrada"
        fecha_s.Text = "Fecha Salida"
        hora_s.Text = "Hora Salida"
        num_p.Text = "Espacio"

        tERow.Cells.Add(nom)
        tERow.Cells.Add(inst)
        tERow.Cells.Add(pla)
        tERow.Cells.Add(fecha_e)
        tERow.Cells.Add(hora_e)
        tERow.Cells.Add(fecha_s)
        tERow.Cells.Add(hora_s)
        tERow.Cells.Add(num_p)
        table.Rows.Add(tERow)

        For Each solicitudAct As Solicitud In solicitudes


            For rowCtr = 1 To rowCnt
                Dim tRow As New TableRow()
                Dim tCell As New TableCell()
                Dim tCell2 As New TableCell()
                Dim tCell3 As New TableCell()
                Dim tCell4 As New TableCell()
                Dim tCell5 As New TableCell()
                Dim tCell6 As New TableCell()
                Dim tCell7 As New TableCell()
                Dim tCell8 As New TableCell()

                tCell.Text = solicitudAct.GstrMarcaSG
                tCell2.Text = solicitudAct.GstrModeloSG
                tCell3.Text = solicitudAct.GstrPlacaSG
                tCell4.Text = solicitudAct.GstrFechaISG.Substring(0, 10)
                tCell5.Text = solicitudAct.GstrHoraISG
                tCell6.Text = solicitudAct.GstrFechaFSG.Substring(0, 10)
                tCell7.Text = solicitudAct.GstrHoraFSG
                tCell8.Text = solicitudAct.GintIdParqueoSG


                tRow.Cells.Add(tCell)
                tRow.Cells.Add(tCell2)
                tRow.Cells.Add(tCell3)
                tRow.Cells.Add(tCell4)
                tRow.Cells.Add(tCell5)
                tRow.Cells.Add(tCell6)
                tRow.Cells.Add(tCell7)
                tRow.Cells.Add(tCell8)
                table.Rows.Add(tRow)
            Next

        Next

    End Function



    Protected Sub btnBuscar_Click2(sender As Object, e As EventArgs) Handles btnExportar.Click
        selecciones("pdf")
        'empieza  a generar el documento

        Try
            ' generar el documento.
            Dim doc As New Document(PageSize.A4.Rotate(), 10, 10, 10, 10)
            'path para guardar en escritorio
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
        Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
        Dim parqueoNegocios As New SP_Parqueo_Negocios(strconnectionString)
        Dim titulo As String = "ERROR"
        Dim mensaje As String = "Ha ocurrido un error"
        Dim tipo As String = "error"
        Dim sn As New SP_Solicitud_Parqueo_Negocios(strconnectionString)
        Dim solicitudes As LinkedList(Of Solicitud)
        Dim faltanDatos As Boolean = True

        If DwnLstTipoReporte.SelectedItem.ToString().Equals("Seleccione una opción") Then
            titulo = "Incompleto"
            mensaje = "Debe seleccionar un tipo de reporte"
            tipo = "warning"
            faltanDatos = True
        ElseIf tbFechaI.Text <> "" AndAlso tbFechaF.Text <> "" AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Fecha") Then

            If (tbFechaI.Text <= tbFechaF.Text) Then
                solicitudes = sn.obtenerReporte(tbFechaI.Text, tbFechaF.Text)

                If solicitudes.Count.Equals(0) Then
                    titulo = "Vacio"
                    mensaje = "No se encontraron datos para las fecha seleccionadas"
                    tipo = "info"
                    faltanDatos = True
                Else
                    If accion.Equals("reporte") Then
                        Me.construyeTabla(solicitudes)
                    End If
                    faltanDatos = False
                End If
            Else
                titulo = "ERROR"
                mensaje = "La fecha de salida debe ser mayor a la fecha de ingreso"
                tipo = "error"
                faltanDatos = True
            End If
        ElseIf (Not DwnLstPlaca.SelectedItem.ToString().Equals("Seleccione una opción")) AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Placa") Then
            solicitudes = sn.obtenerReportePlaca(DwnLstPlaca.SelectedItem.ToString())
            If solicitudes.Count.Equals(0) Then
                titulo = "Vacio"
                mensaje = "No se encontraron datos para la placa seleccionada"
                tipo = "info"
                faltanDatos = True
            Else
                If accion.Equals("reporte") Then
                    Me.construyeTabla(solicitudes)
                End If
                faltanDatos = False
            End If
        ElseIf (Not DwnLstNombre.SelectedItem.ToString().Equals("Seleccione una opción")) AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Nombre") Then
            solicitudes = sn.obtenerReporteCedula(DwnLstNombre.SelectedItem.ToString())
            If solicitudes.Count.Equals(0) Then
                titulo = "Vacio"
                mensaje = "No se encontraron datos para el usuario seleccionado"
                tipo = "info"
                faltanDatos = True
            Else
                If accion.Equals("reporte") Then
                    Me.construyeTabla(solicitudes)
                End If
                faltanDatos = False
            End If
        ElseIf (Not DwnLstInstitucion.SelectedItem.ToString().Equals("Seleccione una opción")) AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Institucion") Then
            solicitudes = sn.obtenerReporteInstitucion(DwnLstInstitucion.SelectedItem.ToString())
            If solicitudes.Count.Equals(0) Then
                titulo = "Vacio"
                mensaje = "No se encontraron datos para el usuario seleccionado"
                tipo = "info"
                faltanDatos = True
            Else
                If accion.Equals("reporte") Then
                    Me.construyeTabla(solicitudes)
                End If
                faltanDatos = False
            End If
        ElseIf (Not DwnLstDepartamento.SelectedItem.ToString().Equals("Seleccione una opción")) AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Departamento") Then
            solicitudes = sn.obtenerReporteDepartamento(DwnLstDepartamento.SelectedItem.ToString())
            If solicitudes.Count.Equals(0) Then
                titulo = "Vacio"
                mensaje = "No se encontraron datos para el usuario seleccionado"
                tipo = "info"
                faltanDatos = True
            Else
                If accion.Equals("reporte") Then
                    Me.construyeTabla(solicitudes)
                End If
                faltanDatos = False
            End If
        ElseIf (Not DwnLstCorreo.SelectedItem.ToString().Equals("Seleccione una opción")) AndAlso DwnLstTipoReporte.SelectedItem.ToString().Equals("Correo") Then
            solicitudes = sn.obtenerReporteCorreo(DwnLstCorreo.SelectedItem.ToString())
            If solicitudes.Count.Equals(0) Then
                titulo = "Vacio"
                mensaje = "No se encontraron entradas para el correo seleccionado"
                tipo = "info"
                faltanDatos = True
            Else
                If accion.Equals("reporte") Then
                    Me.construyeTabla(solicitudes)
                End If
                faltanDatos = False
            End If
        Else
            titulo = "Incompleto"
            mensaje = "Debe completar todos los campos"
            tipo = "warning"
            faltanDatos = True
        End If

        If faltanDatos Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
        End If


        Label1.Text = String.Join("", Me.lista.ToArray()).ToString

        Return solicitudes


    End Function




    Public Function ExportarDatosPDF(ByVal document As Document, ByVal str As String, solicitudes As LinkedList(Of Solicitud))

        Dim fuente As iTextSharp.text.pdf.BaseFont
        fuente = FontFactory.GetFont(FontFactory.HELVETICA, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL).BaseFont

        'Se agrega el PDFTable al documento.
        Dim strContent As String = str
        Dim parsedHtmlElements As List(Of IElement)

        'selecciones contruye la tabla

        Dim cellCnt As Integer

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

            'tCell.Text = solicitudAct.GstrMarcaSG
            'tCell2.Text = " "
            'tCell3.Text = solicitudAct.GstrPlacaSG
            'tCell4.Text = solicitudAct.GstrFechaISG.Substring(0, 10)
            'tCell5.Text = solicitudAct.GstrHoraISG
            'tCell6.Text = solicitudAct.GstrFechaFSG.Substring(0, 10)
            'tCell7.Text = solicitudAct.GstrHoraFSG
            'tCell8.Text = solicitudAct.GintIdParqueoSG



            llenar += "<tr>" + "<td>" + solicitudAct.GstrMarcaSG + "</td>" + "<td>" + " " + "</td>" + "<td>" + solicitudAct.GstrPlacaSG +
            "</td>" + "<td>" + solicitudAct.GstrFechaISG.Substring(0, 10) + "</td>" + "<td>" + solicitudAct.GstrHoraISG + "</td>" + "<td>" + solicitudAct.GstrFechaFSG.Substring(0, 10) + "</td>" + "<td>" + solicitudAct.GstrHoraFSG + "</td>" + "<td>" + solicitudAct.GintIdParqueoSG.ToString() + "</td>" + "</tr>"
        Next

        llenar += "</table>" + "<br>"
        strContent = llenar



        'lee el string  y cnviente los elementos a la lista
        parsedHtmlElements = HTMLWorker.ParseToList(New StringReader(strContent), Nothing)

        'toma cada uno de los valores parseados y los agrega al documento pdf

        For Each htmlElement As IElement In parsedHtmlElements
            document.Add(htmlElement)
        Next


    End Function

End Class