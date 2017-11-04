Imports System.Web.Configuration
Imports Entidad
Imports Negocios
Public Class frm_Reporte
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.Equals(Session("Usuario"), "a") Then
            ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "frm_Reporte", ResolveUrl("~") + "public/js/" + "script.js")
        Else
            Response.BufferOutput = True
            Response.Redirect("http://localhost:52086/view/frm_index.aspx")
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
        Dim parqueoNegocios As New SP_Parqueo_Negocios(strconnectionString)
        Dim titulo, mensaje, tipo As String
        Dim sn As New SP_Solicitud_Parqueo_Negocios(strconnectionString)

        If tbFechaI.Text <> "" AndAlso tbFechaF.Text <> "" Then
            Dim solicitudes As LinkedList(Of Solicitud) = sn.obtenerReporte(tbFechaI.Text, tbFechaF.Text)
            Dim rowCnt As Integer
            Dim rowCtr As Integer
            Dim cellCnt As Integer

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
                    tCell2.Text = " "
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
        Else
            titulo = "ERROR"
            mensaje = "Debe completar todos los campos"
            tipo = "error"

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
        End If
    End Sub
End Class