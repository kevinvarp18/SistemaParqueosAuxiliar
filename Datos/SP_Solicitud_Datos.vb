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
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  13-Noviembre-2017
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

        For Each currentRow As DataRow In dataRowCollection
            Dim solicitudActual As New Solicitud()
            solicitudActual.GstrHoraISG = currentRow("hora_e").ToString()
            solicitudActual.GstrHoraFSG = currentRow("hora_s").ToString()
            solicitudActual.GstrPlacaSG = currentRow("placa").ToString()
            solicitudActual.GstrMarcaSG = currentRow("nombre").ToString()
            solicitudActual.GstrModeloSG = currentRow("correo").ToString()
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
    Public Function obtenerNumeroVisitantesAtrasados() As String
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As String = "PA_CantVisitantesAtrasados"
        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "SP.TSP_Entradas_Parqueo_X_TSP_Solicitud")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("SP.TSP_Entradas_Parqueo_X_TSP_Solicitud").Rows
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
            solicitudActual.GstrModeloSG = currentRow("tipo_visistante").ToString()
            solicitud.AddLast(solicitudActual)

        Next
        Return solicitud
    End Function
    Public Function obtenerReportePlaca(placa As String) As LinkedList(Of Solicitud)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_ReportePlaca"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@placa_", placa))

        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Solicitud")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Solicitud").Rows
        Dim solicitudes As New LinkedList(Of Solicitud)()

        For Each currentRow As DataRow In dataRowCollection
            Dim solicitudActual As New Solicitud()
            solicitudActual.GintIdParqueoSG = Integer.Parse(currentRow("num_parqueo").ToString())
            solicitudActual.GstrHoraISG = currentRow("hora_e").ToString()
            solicitudActual.GstrHoraFSG = currentRow("hora_s").ToString()
            solicitudActual.GstrPlacaSG = currentRow("placa").ToString()
            solicitudActual.GstrMarcaSG = currentRow("nombre").ToString()
            solicitudActual.GstrFechaISG = currentRow("fecha_e").ToString()
            solicitudActual.GstrFechaFSG = currentRow("fecha_s").ToString()
            solicitudActual.GstrModeloSG = currentRow("tipo_visistante").ToString()
            solicitudes.AddLast(solicitudActual)

        Next
        Return solicitudes
    End Function
    Public Function obtenerReporteCorreo(correo As String) As LinkedList(Of Solicitud)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_ReporteCorreo"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@correo_", correo))

        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Solicitud")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Solicitud").Rows
        Dim solicitudes As New LinkedList(Of Solicitud)()

        For Each currentRow As DataRow In dataRowCollection
            Dim solicitudActual As New Solicitud()
            solicitudActual.GintIdParqueoSG = Integer.Parse(currentRow("num_parqueo").ToString())
            solicitudActual.GstrHoraISG = currentRow("hora_e").ToString()
            solicitudActual.GstrHoraFSG = currentRow("hora_s").ToString()
            solicitudActual.GstrPlacaSG = currentRow("placa").ToString()
            solicitudActual.GstrMarcaSG = currentRow("nombre").ToString()
            solicitudActual.GstrFechaISG = currentRow("fecha_e").ToString()
            solicitudActual.GstrFechaFSG = currentRow("fecha_s").ToString()
            solicitudActual.GstrModeloSG = currentRow("tipo_visistante").ToString()
            solicitudes.AddLast(solicitudActual)

        Next
        Return solicitudes
    End Function

    Public Function obtenerReporteCedula(cedula As String) As LinkedList(Of Solicitud)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_ReporteCedula"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@cedula_", cedula))

        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Solicitud")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Solicitud").Rows
        Dim solicitudes As New LinkedList(Of Solicitud)()

        For Each currentRow As DataRow In dataRowCollection
            Dim solicitudActual As New Solicitud()
            solicitudActual.GintIdParqueoSG = Integer.Parse(currentRow("num_parqueo").ToString())
            solicitudActual.GstrHoraISG = currentRow("hora_e").ToString()
            solicitudActual.GstrHoraFSG = currentRow("hora_s").ToString()
            solicitudActual.GstrPlacaSG = currentRow("placa").ToString()
            solicitudActual.GstrMarcaSG = currentRow("nombre").ToString()
            solicitudActual.GstrFechaISG = currentRow("fecha_e").ToString()
            solicitudActual.GstrFechaFSG = currentRow("fecha_s").ToString()
            solicitudActual.GstrModeloSG = currentRow("tipo_visistante").ToString()
            solicitudes.AddLast(solicitudActual)

        Next
        Return solicitudes
    End Function

    Public Function obtenerReporteInstitucion(institucion As String) As LinkedList(Of Solicitud)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_ReporteInstitucion"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@institucion_", institucion))

        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Solicitud")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Solicitud").Rows
        Dim solicitudes As New LinkedList(Of Solicitud)()

        For Each currentRow As DataRow In dataRowCollection
            Dim solicitudActual As New Solicitud()
            solicitudActual.GintIdParqueoSG = Integer.Parse(currentRow("num_parqueo").ToString())
            solicitudActual.GstrHoraISG = currentRow("hora_e").ToString()
            solicitudActual.GstrHoraFSG = currentRow("hora_s").ToString()
            solicitudActual.GstrPlacaSG = currentRow("placa").ToString()
            solicitudActual.GstrMarcaSG = currentRow("nombre").ToString()
            solicitudActual.GstrFechaISG = currentRow("fecha_e").ToString()
            solicitudActual.GstrFechaFSG = currentRow("fecha_s").ToString()
            solicitudActual.GstrModeloSG = currentRow("tipo_visistante").ToString()
            solicitudes.AddLast(solicitudActual)

        Next
        Return solicitudes
    End Function

    Public Function obtenerReporteDepartamento(departamento As String) As LinkedList(Of Solicitud)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_ReporteDepartamento"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@departamento_", departamento))

        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Solicitud")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Solicitud").Rows
        Dim solicitudes As New LinkedList(Of Solicitud)()

        For Each currentRow As DataRow In dataRowCollection
            Dim solicitudActual As New Solicitud()
            solicitudActual.GintIdParqueoSG = Integer.Parse(currentRow("num_parqueo").ToString())
            solicitudActual.GstrHoraISG = currentRow("hora_e").ToString()
            solicitudActual.GstrHoraFSG = currentRow("hora_s").ToString()
            solicitudActual.GstrPlacaSG = currentRow("placa").ToString()
            solicitudActual.GstrMarcaSG = currentRow("nombre").ToString()
            solicitudActual.GstrFechaISG = currentRow("fecha_e").ToString()
            solicitudActual.GstrFechaFSG = currentRow("fecha_s").ToString()
            solicitudActual.GstrModeloSG = currentRow("tipo_visistante").ToString()
            solicitudes.AddLast(solicitudActual)

        Next
        Return solicitudes
    End Function

    Public Function ObtenerCedulasYNombres() As LinkedList(Of Usuario)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As String = "PA_VerCedulasYNombres"
        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "SP.TSP_Usuario")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("SP.TSP_Usuario").Rows
        Dim usuarios As New LinkedList(Of Usuario)()

        For Each currentRow As DataRow In dataRowCollection
            Dim usuario As New Usuario()
            usuario.GstrNombreSG = currentRow("TC_Nombre_TSP_Usuario")
            usuario.GstrApellidoSG = currentRow("TC_Apellido_TSP_Usuario")
            usuario.GstrIdSG = currentRow("TC_Identificacion_TSP_Tipoid")
            usuarios.AddLast(usuario)
        Next
        Return usuarios
    End Function

    Public Function ObtenerDepartamentos() As LinkedList(Of String)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As String = "PA_VerDepartamentos"
        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "SP.TSP_Usuario")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("SP.TSP_Usuario").Rows
        Dim departamentos As New LinkedList(Of String)()

        For Each currentRow As DataRow In dataRowCollection
            Dim departamento As String = ""
            departamento = currentRow("TC_Departamento_TSP_Vinterno")
            departamentos.AddLast(departamento)
        Next
        Return departamentos
    End Function

    Public Function ObtenerInstituciones() As LinkedList(Of String)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As String = "PA_VerInstituciones"
        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "SP.TSP_Usuario")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("SP.TSP_Usuario").Rows
        Dim instituciones As New LinkedList(Of String)()

        For Each currentRow As DataRow In dataRowCollection
            Dim institucion As String = ""
            institucion = currentRow("TC_Institucion_TSP_Vexterno")
            instituciones.AddLast(institucion)
        Next
        Return instituciones
    End Function

    Public Function decidirSolicitud(marca As String, placa As String, horaI As String, horaF As String, fechaI As String, fechaF As String, idParqueo As String, accion As String) As Integer
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_DecidirSolicitud"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure
        Dim resultado As Integer

        cmdInsert.Parameters.Add(New SqlParameter("@marca", marca))
        cmdInsert.Parameters.Add(New SqlParameter("@placa", placa))
        cmdInsert.Parameters.Add(New SqlParameter("@horaEntrada", horaI))
        cmdInsert.Parameters.Add(New SqlParameter("@horaSalida", horaF))
        cmdInsert.Parameters.Add(New SqlParameter("@fechaEntrada", fechaI))
        cmdInsert.Parameters.Add(New SqlParameter("@fechaSalida", fechaF))
        cmdInsert.Parameters.Add(New SqlParameter("@idParqueo", Integer.Parse(idParqueo)))
        cmdInsert.Parameters.Add(New SqlParameter("@accion", Integer.Parse(accion)))
        cmdInsert.Parameters.Add("@valorRetorno", SqlDbType.Int).Direction = ParameterDirection.Output
        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        resultado = Convert.ToInt32(cmdInsert.Parameters("@valorRetorno").Value)
        cmdInsert.Connection.Close()

        Return resultado
    End Function
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
            solicitudActual.GintIdSolicutudSG = Integer.Parse(currentRow("TN_MarcaE_TSP_Entradas_Parqueo_X_TSP_Solicitud"))
            solicitudActual.GintIdVisitanteSG = Integer.Parse(currentRow("TN_MarcaS_TSP_Entradas_Parqueo_X_TSP_Solicitud"))
            solicitudes.AddLast(solicitudActual)
        Next
        Return solicitudes
    End Function
    Public Sub marcarEntrada_Salida(marca As String, placa As String, modelo As String, espacioParqueo As Integer, horaEntrada As String, horaSalida As String, accion As Integer)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_MarcarEntrada_Salida"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure

        cmdInsert.Parameters.Add(New SqlParameter("@marca", marca))
        cmdInsert.Parameters.Add(New SqlParameter("@placa", placa))
        cmdInsert.Parameters.Add(New SqlParameter("@modelo", modelo))
        cmdInsert.Parameters.Add(New SqlParameter("@espacioParqueo", espacioParqueo))
        cmdInsert.Parameters.Add(New SqlParameter("@horaEntrada", horaEntrada))
        cmdInsert.Parameters.Add(New SqlParameter("@horaSalida", horaSalida))
        cmdInsert.Parameters.Add(New SqlParameter("@accion", Integer.Parse(accion)))

        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        cmdInsert.Connection.Close()
    End Sub
    Public Function obtenerVisitantesAtrasados() As LinkedList(Of Solicitud)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_ObtenerVisitantesAtrasados"

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
            solicitudActual.GstrFechaFSG = currentRow("TC_Correo_TSP_Usuario").ToString()
            solicitudActual.GstrMarcaSG = currentRow("TC_Marca_TSP_Solicitud").ToString()
            solicitudActual.GstrPlacaSG = currentRow("TC_Placa_TSP_Solicitud").ToString()
            solicitudActual.GstrModeloSG = currentRow("TC_Modelo_TSP_Solicitud").ToString()
            solicitudActual.GintIdParqueoSG = Integer.Parse(currentRow("TN_Idparqueo_TSP_Solicitud").ToString())
            solicitudActual.GstrHoraISG = currentRow("TF_Horai_TSP_Entradas_Parqueo_X_TSP_Solicitud").ToString()
            solicitudActual.GstrHoraFSG = currentRow("TF_Horaf_TSP_Entradas_Parqueo_X_TSP_Solicitud").ToString()
            solicitudes.AddLast(solicitudActual)
        Next
        Return solicitudes
    End Function
    Public Function decidirSolicitudAtrasada(marca As String, placa As String, modelo As String, idParqueo As Integer, horaI As String, horaF As String, horaNueva As String, accion As String) As Integer
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_DecidirSolicitudAtrasada"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure
        Dim resultado As Integer

        cmdInsert.Parameters.Add(New SqlParameter("@marca", marca))
        cmdInsert.Parameters.Add(New SqlParameter("@placa", placa))
        cmdInsert.Parameters.Add(New SqlParameter("@modelo", modelo))
        cmdInsert.Parameters.Add(New SqlParameter("@idParqueo", idParqueo))
        cmdInsert.Parameters.Add(New SqlParameter("@horaEntrada", horaI))
        cmdInsert.Parameters.Add(New SqlParameter("@horaSalida", horaF))
        cmdInsert.Parameters.Add(New SqlParameter("@nuevaHora", horaNueva))
        cmdInsert.Parameters.Add(New SqlParameter("@accion", Integer.Parse(accion)))
        cmdInsert.Parameters.Add("@valorRetorno", SqlDbType.Int).Direction = ParameterDirection.Output
        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        resultado = Convert.ToInt32(cmdInsert.Parameters("@valorRetorno").Value)
        cmdInsert.Connection.Close()

        Return resultado
    End Function
End Class
