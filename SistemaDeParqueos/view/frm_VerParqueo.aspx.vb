Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class VerParqueo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim permitido As Boolean = False

        For Each variableSesion As String In Session.Keys
            If (String.Equals(variableSesion, "frm_VerParqueo")) Then
                permitido = True
            End If
        Next

        If (permitido) Then
            ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "frm_VerParqueo", ResolveUrl("~") + "public/js/" + "script.js")
            If Not IsPostBack Then
                llenarTablaParqueos(DateTime.Now.ToString("dd/MM/yyyy"), "07:00", "21:00")
            End If
        Else
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "")
            Response.BufferOutput = True
            Response.Redirect(url & Convert.ToString("/view/frm_index.aspx"))
        End If
    End Sub

    Protected Sub btnBuscarP_Click(sender As Object, e As EventArgs) Handles btnBuscarP.Click
        Dim fechai As DateTime = Convert.ToDateTime(tbFechaI.Text)
        If tbFechaI.Text <> "" AndAlso tbHoraI.Text <> "" AndAlso tbHoraF.Text <> "" Then
            llenarTablaParqueos(fechai, tbHoraI.Text, tbHoraF.Text)
        Else
            Dim titulo As String = "ERROR"
            Dim mensaje As String = "Debe completar todos los campos"
            Dim tipo As String = "error"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
        End If
    End Sub

    Private Sub llenarTablaParqueos(fechai As DateTime, horaI As String, horaF As String)
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
        table.Rows.Add(tableHeaderRow)
        Dim filaTabla As New TableRow()
        For rowCtr = 0 To cantidadTiposParqueo.Count - 1
            Dim celdaTabla As New TableCell()
            For Each parqueoActual As Parqueo In parqueosTotales
                Dim tipoParqueo As String
                tipoParqueo = table.Rows.Item(0).Cells.Item(rowCtr).ID
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
        table.Rows.Add(filaTabla)
    End Sub

End Class