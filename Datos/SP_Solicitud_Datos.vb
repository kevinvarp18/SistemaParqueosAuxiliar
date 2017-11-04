Imports System.Data.SqlClient
Imports Entidad
Public Class SP_Solicitud_Datos
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.Acceso_a_Datos.
    'DESCRIPCIÓN:
    ' Esta clase maneja las coneciones de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Dylan Zamora
    '
    'FECHA DE CREACIÓN                               05-Octubre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  04-Noviembre-2017
    '******************************************************************
    'Declaracion de Varaiables.
    Public gstrconnString As String

    'Declaracion de constrcutor.
    Public Sub New(gstrconnString As String)
        Me.gstrconnString = gstrconnString
    End Sub

    Public Function insertarSolicitud(correo As String, solicitud As Solicitud) As Solicitud

        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_RegistrarSolicitud"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure

        cmdInsert.Parameters.Add(New SqlParameter("@correo", correo))
        cmdInsert.Parameters.Add(New SqlParameter("@hora_i", solicitud.GstrHoraISG))
        cmdInsert.Parameters.Add(New SqlParameter("@hora_f", solicitud.GstrHoraFSG))
        cmdInsert.Parameters.Add(New SqlParameter("@placa", solicitud.GstrPlacaSG))
        cmdInsert.Parameters.Add(New SqlParameter("@modelo", solicitud.GstrModeloSG))
        cmdInsert.Parameters.Add(New SqlParameter("@marca", solicitud.GstrMarcaSG))
        cmdInsert.Parameters.Add(New SqlParameter("@fecha_i", solicitud.GstrFechaISG))
        cmdInsert.Parameters.Add(New SqlParameter("@fecha_f", solicitud.GstrFechaFSG))

        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        cmdInsert.Connection.Close()

        Return solicitud
    End Function

    Public Function obtenerSolicitud() As LinkedList(Of Solicitud)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_VerSolicitud;" 'hay q hacer otro procedimiento

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Solicitud")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Solicitud").Rows
        Dim solicitud As New LinkedList(Of Solicitud)()

        For Each currentRow As DataRow In dataRowCollection
            Dim solicitudActual As New Solicitud()
            solicitudActual.GintIdSolicutudSG = Integer.Parse(currentRow("TN_Idsolicitud_TSP_Solicitud").ToString())
            solicitudActual.GintIdVisitanteSG = Integer.Parse(currentRow("TN_Idvisitante_TSP_Solicitud").ToString())
            solicitudActual.GintIdParqueoSG = Integer.Parse(currentRow("TN_Idparqueo_TSP_Solicitud").ToString())
            solicitudActual.GstrHoraISG = currentRow("TF_Horai_TSP_Solicitud").ToString()
            solicitudActual.GstrHoraFSG = currentRow("TF_Horaf_TSP_Solicitud").ToString()
            solicitudActual.GstrPlacaSG = currentRow("TC_Placa_TSP_Solicitud").ToString()
            solicitudActual.GstrModeloSG = currentRow("TC_Modelo_TSP_Solicitud").ToString()
            solicitudActual.GstrMarcaSG = currentRow("TC_Marca_TSP_Solicitud").ToString()
            solicitudActual.GstrFechaISG = currentRow("TF_Fechai_TSP_Solicitud").ToString()
            solicitudActual.GstrFechaFSG = currentRow("TF_Fechaf_TSP_Solicitud").ToString()
            solicitud.AddLast(solicitudActual)

        Next
        Return solicitud
    End Function
    Public Function obtenerAdSolicitud() As LinkedList(Of Solicitud)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_VerSolicitud;"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Solicitud")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Solicitud").Rows
        Dim solicitud As New LinkedList(Of Solicitud)()

        'nombre,placa,fecha_e,hora_e,fecha_s,hora_s

        For Each currentRow As DataRow In dataRowCollection
            Dim solicitudActual As New Solicitud()
            solicitudActual.GstrHoraISG = currentRow("hora_e").ToString()
            solicitudActual.GstrHoraFSG = currentRow("hora_s").ToString()
            solicitudActual.GstrPlacaSG = currentRow("placa").ToString()
            solicitudActual.GstrMarcaSG = currentRow("nombre").ToString() 'voy a usar este para el nombre
            solicitudActual.GstrFechaISG = currentRow("fecha_e").ToString()
            solicitudActual.GstrFechaFSG = currentRow("fecha_s").ToString()
            solicitud.AddLast(solicitudActual)
        Next
        Return solicitud
    End Function
    Public Function obtenerNumeroSolicitudes() As String
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As String = "PA_CantSolicitudNotif"
        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "SP.TSP_Usuario")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("SP.TSP_Usuario").Rows
        Dim cantidad As String
        cantidad = ""
        For Each currentRow As DataRow In dataRowCollection
            Dim usuarioActual As New Usuario()
            cantidad = currentRow("Cantidad").ToString()
        Next
        Return cantidad
    End Function
    Public Function obtenerReporte(fecha_i As String, fecha_f As String) As LinkedList(Of Solicitud)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_ReporteFechas"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@fecha_i", fecha_i))
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@fecha_f", fecha_f))

        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Solicitud")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Solicitud").Rows
        Dim solicitud As New LinkedList(Of Solicitud)()

        For Each currentRow As DataRow In dataRowCollection
            Dim solicitudActual As New Solicitud()
            solicitudActual.GintIdParqueoSG = Integer.Parse(currentRow("num_parqueo").ToString())
            solicitudActual.GstrHoraISG = currentRow("hora_e").ToString()
            solicitudActual.GstrHoraFSG = currentRow("hora_s").ToString()
            solicitudActual.GstrPlacaSG = currentRow("placa").ToString()
            solicitudActual.GstrMarcaSG = currentRow("nombre").ToString()
            solicitudActual.GstrFechaISG = currentRow("fecha_e").ToString()
            solicitudActual.GstrFechaFSG = currentRow("fecha_s").ToString()
            solicitud.AddLast(solicitudActual)

        Next
        Return solicitud
    End Function
    Public Sub decidirSolicitud(marca As String, placa As String, horaI As String, horaF As String, fechaI As String, fechaF As String, idParqueo As String, accion As String)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_DecidirSolicitud"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure

        cmdInsert.Parameters.Add(New SqlParameter("@marca", marca))
        cmdInsert.Parameters.Add(New SqlParameter("@placa", placa))
        cmdInsert.Parameters.Add(New SqlParameter("@horaEntrada", horaI))
        cmdInsert.Parameters.Add(New SqlParameter("@horaSalida", horaF))
        cmdInsert.Parameters.Add(New SqlParameter("@fechaEntrada", fechaI))
        cmdInsert.Parameters.Add(New SqlParameter("@fechaSalida", fechaF))
        cmdInsert.Parameters.Add(New SqlParameter("@idParqueo", Integer.Parse(idParqueo)))
        cmdInsert.Parameters.Add(New SqlParameter("@accion", Integer.Parse(accion)))

        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        cmdInsert.Connection.Close()
    End Sub
    Public Function obtenerSolicitudesHoy() As LinkedList(Of Solicitud)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_VerSolicitudesHoy"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure

        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Solicitud")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Solicitud").Rows
        Dim solicitudes As New LinkedList(Of Solicitud)()

        For Each currentRow As DataRow In dataRowCollection
            Dim solicitudActual As New Solicitud()
            solicitudActual.GstrFechaISG = currentRow("TC_Nombre_TSP_Usuario").ToString() 'Se usa este para el nombre
            solicitudActual.GstrMarcaSG = currentRow("TC_Marca_TSP_Solicitud").ToString()
            solicitudActual.GstrPlacaSG = currentRow("TC_Placa_TSP_Solicitud").ToString()
            solicitudActual.GstrModeloSG = currentRow("TC_Modelo_TSP_Solicitud").ToString()
            solicitudActual.GintIdParqueoSG = Integer.Parse(currentRow("TN_Idparqueo_TSP_Solicitud").ToString())
            solicitudActual.GstrHoraISG = currentRow("TF_Horai_TSP_Solicitud").ToString()
            solicitudActual.GstrHoraFSG = currentRow("TF_Horaf_TSP_Solicitud").ToString()
            solicitudes.AddLast(solicitudActual)
        Next
        Return solicitudes
    End Function

End Class
