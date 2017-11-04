Imports System.Web.Configuration
Imports Entidad
Imports Negocios

Public Class administrarParqueo
    Inherits System.Web.UI.Page

    Public gintParqueoIdentificador As Long
    Public gstrParqueoSelecion As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.Equals(Session("Usuario"), "a") Then
            Dim connectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "frm_AdministrarParqueo", ResolveUrl("~") + "public/js/" + "script.js")

            Dim parqueoNegocios As New SP_Parqueo_Negocios(connectionString)
            If IsPostBack Then
                Dim parqueo As LinkedList(Of Parqueo) = parqueoNegocios.obtenerParqueo()
                Me.gstrParqueoSelecion = DwnEspacio.SelectedItem.ToString()
                For Each parqueoActual As Parqueo In parqueo
                    If Me.gstrParqueoSelecion.Equals("Numero Parqueo: " + parqueoActual.GintIdentificadorSG.ToString()) Then
                        Me.gintParqueoIdentificador = parqueoActual.GintIdentificadorSG
                    End If
                Next
            Else
                DwnEspacio.Items.Clear()
                DwnEspacio.Items.Add("Sin Seleccionar")
                Dim parqueo As LinkedList(Of Parqueo) = parqueoNegocios.obtenerParqueo()
                For Each item As Parqueo In parqueo
                    DwnEspacio.Items.Add("Numero Parqueo: " + item.GintIdentificadorSG.ToString)
                Next
                DwnLstTipos.Items.Clear()
                DwnLstEstado.Items.Clear()
                DwnLstTipos.Items.Add("Seleccione una opción")
                DwnLstTipos.Items.Add("Jefatura")
                DwnLstTipos.Items.Add("PIP")
                DwnLstTipos.Items.Add("UPRO")
                DwnLstTipos.Items.Add("OPO")
                DwnLstTipos.Items.Add("SERT")
                DwnLstTipos.Items.Add("UPROV")
                DwnLstTipos.Items.Add("UVISE")
                DwnLstTipos.Items.Add("Visitas")
                DwnLstEstado.Items.Add("Seleccione una opción")
                DwnLstEstado.Items.Add("Habilitado")
                DwnLstEstado.Items.Add("Deshabilitado")
                Me.gstrParqueoSelecion = DwnEspacio.SelectedItem.ToString()
                For Each parqueoActual As Parqueo In parqueo
                    If Me.gstrParqueoSelecion.Equals("Numero Parqueo: " + parqueoActual.GintIdentificadorSG.ToString()) Then
                        Me.gintParqueoIdentificador = parqueoActual.GintIdentificadorSG
                    End If
                Next
            End If
        Else
            Response.BufferOutput = True
            Response.Redirect("http://localhost:52086/view/frm_index.aspx")
        End If
    End Sub

    Protected Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        Dim titulo, mensaje, tipo As String

        If (DwnEspacio.SelectedItem.ToString().Equals("Sin Seleccionar") Or DwnLstTipos.SelectedItem.ToString.Equals("Seleccione una opción") Or
            DwnLstEstado.SelectedItem.ToString.Equals("Seleccione una opción")) Then

            titulo = "ERROR"
            mensaje = "Debe completar todos los campos"
            tipo = "error"

        Else
            Dim Blnestado As Byte = 0
            Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Dim parqueoNegocios As New SP_Parqueo_Negocios(strconnectionString)
            If (DwnLstEstado.Text.Equals("Habilitado")) Then
                Blnestado = 1
            End If
            parqueoNegocios.insertarParqueo(New Parqueo(0, Blnestado, DwnLstTipos.Text))

            titulo = "Correcto"
            mensaje = "Se ha creado el parqueo exitosamente"
            tipo = "success"
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim titulo, mensaje, tipo As String

        If (DwnEspacio.SelectedItem.ToString().Equals("Sin Seleccionar") Or DwnLstTipos.SelectedItem.ToString.Equals("Seleccione una opción") Or
            DwnLstEstado.SelectedItem.ToString.Equals("Seleccione una opción")) Then

            titulo = "ERROR"
            mensaje = "Debe completar todos los campos"
            tipo = "error"

        Else

            Dim Blnestado As Byte = 0
            Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Dim parqueoNegocios As New SP_Parqueo_Negocios(strconnectionString)
            If (DwnLstEstado.Text.Equals("Habilitado")) Then
                Blnestado = 1
            End If
            parqueoNegocios.actualizarParqueo(New Parqueo(Me.gintParqueoIdentificador, Blnestado, DwnLstTipos.Text))

            titulo = "Correcto"
            mensaje = "Se ha actualizado el parqueo exitosamente"
            tipo = "success"
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim titulo, mensaje, tipo As String

        If (DwnEspacio.SelectedItem.ToString().Equals("Sin Seleccionar") Or DwnLstTipos.SelectedItem.ToString.Equals("Seleccione una opción") Or
            DwnLstEstado.SelectedItem.ToString.Equals("Seleccione una opción")) Then

            titulo = "ERROR"
            mensaje = "Debe completar todos los campos"
            tipo = "error"

        Else

            Dim Blnestado As Byte = 0
            Dim strconnectionString As String = WebConfigurationManager.ConnectionStrings("DBOIJ").ToString()
            Dim parqueoNegocios As New SP_Parqueo_Negocios(strconnectionString)
            parqueoNegocios.eliminarParqueo(New Parqueo(Me.gintParqueoIdentificador, 0, ""))

            titulo = "Correcto"
            mensaje = "Se ha eliminado el parqueo exitosamente"
            tipo = "success"
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ScriptManager2", "muestraMensaje(""" + titulo + """,""" + mensaje + """,""" + tipo + """);", True)
    End Sub
End Class