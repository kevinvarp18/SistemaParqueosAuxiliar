Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class VerParqueo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.Equals(Session("Usuario"), "a") Then
        Else
            Response.BufferOutput = True
            Response.Redirect("http://localhost:52086/view/frm_index.aspx")
        End If
    End Sub

    Protected Sub btnBuscarP_Click(sender As Object, e As EventArgs) Handles btnBuscarP.Click
        Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
        Dim parqueoNegocios As New SP_Parqueo_Negocios(strconnectionString)
        Dim sn As New SP_Solicitud_Parqueo_Negocios(strconnectionString)
        If tbFechaI.Text <> "" AndAlso tbHoraI.Text <> "" AndAlso tbHoraF.Text <> "" Then

            Dim parqueosOcupados As LinkedList(Of Parqueo) = parqueoNegocios.obtenerParqueoOcupado(tbFechaI.Text, tbHoraI.Text, tbHoraF.Text)
            Dim parqueosTotales As LinkedList(Of Parqueo) = parqueoNegocios.obtenerParqueoHabilitado()
            Dim cantidadTiposParqueo As LinkedList(Of String) = parqueoNegocios.cantidadTiposParqueo()

            Dim rowCnt As Integer

            rowCnt = 1

            Dim tableHeaderRow As New TableHeaderRow()
            For Each tipos As String In cantidadTiposParqueo
                Dim tableHeaderCell As New TableHeaderCell()
                tableHeaderCell.Text = tipos
                tableHeaderCell.ID = tipos
                tableHeaderRow.Cells.Add(tableHeaderCell)
            Next 'Agrega los tipos de parqueos a la primera fila.
            table.Rows.Add(tableHeaderRow)

            For Each parqueoActual As Parqueo In parqueosTotales
                Dim tableRow As New TableRow()
                For rowCtr = 0 To cantidadTiposParqueo.Count - 1
                    Dim tableCell As New TableCell()
                    Dim tipoParqueo As String
                    tipoParqueo = table.Rows.Item(0).Cells.Item(rowCtr).ID
                    If parqueoActual.GstrTipoSG.Equals(tipoParqueo) Then
                        Dim hyperLink As New HyperLink()
                        Dim ocu = False
                        For Each parqueoOcupado As Parqueo In parqueosOcupados
                            If parqueoActual.GintIdentificadorSG = parqueoOcupado.GintIdentificadorSG Then
                                ocu = True
                            End If
                        Next 'Busca en todos los parqueos ocupados, para ver si el parqueo actual está ocupado.
                        hyperLink.Text = "Espacio " + parqueoActual.GintIdentificadorSG.ToString()
                        hyperLink.NavigateUrl = ""
                        If ocu = True Then
                            hyperLink.Style("color") = "#ff0000"
                        Else
                            hyperLink.Style("color") = "#00fe00"
                        End If
                        tableCell.Controls.Add(hyperLink)
                    End If
                    tableRow.Cells.Add(tableCell)
                Next 'For rowCtr = 0 To rowCnt
                table.Rows.Add(tableRow)
            Next 'For Each parqueosAct As Parqueo In parqueosTotales
        End If
    End Sub

End Class