Imports System.Data.SqlClient
Imports Entidad

Public Class SP_Parqueo_Datos
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.Acceso_a_Datos.
    'DESCRIPCIÓN:
    ' Esta clase maneja las coneciones de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Dylan Zamora
    '
    'FECHA DE CREACIÓN                               05-Octubre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  03-Noviembre-2017
    '******************************************************************
    'Declaracion de Varaiables. 
    Public gstrconnString As String

    'Declaracion de constructor.
    Public Sub New(gstrconnString As String)
        Me.gstrconnString = gstrconnString
    End Sub
    Public Function insertarParqueo(parqueo As Parqueo) As Parqueo

        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_InsertaParqueo"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure

        cmdInsert.Parameters.Add(New SqlParameter("@Estado", parqueo.GintDisponibleSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Tipo", parqueo.GstrTipoSG))

        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        cmdInsert.Connection.Close()

        Return parqueo
    End Function
    Public Function actualizarParqueo(parqueo As Parqueo) As Parqueo

        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_ActualizaParqueo"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure

        cmdInsert.Parameters.Add(New SqlParameter("@NumeroParqueo", parqueo.GintIdentificadorSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Estado", parqueo.GintDisponibleSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Tipo", parqueo.GstrTipoSG))

        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        cmdInsert.Connection.Close()

        Return parqueo
    End Function

    Public Function eliminarParqueo(parqueo As Parqueo) As Parqueo

        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_EliminaParqueo"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure

        cmdInsert.Parameters.Add(New SqlParameter("@NumeroParqueo", parqueo.GintIdentificadorSG))

        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        cmdInsert.Connection.Close()

        Return parqueo
    End Function

    Public Function obtenerParqueo() As LinkedList(Of Parqueo)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_VerParqueo;"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Parqueo")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Parqueo").Rows
        Dim parqueo As New LinkedList(Of Parqueo)()

        For Each currentRow As DataRow In dataRowCollection
            Dim parqueoActual As New Parqueo()
            parqueoActual.GintIdentificadorSG = Long.Parse(currentRow("TN_Identificador_TSP_Parqueo").ToString())
            parqueoActual.GintDisponibleSG = Boolean.Parse(currentRow("TB_Disponible_TSP_Parqueo").ToString())
            parqueoActual.GstrTipoSG = currentRow("TC_Tipo_TSP_Parqueo").ToString()

            parqueo.AddLast(parqueoActual)
        Next
        Return Parqueo
    End Function
    Public Function obtenerParqueoHabilitado() As LinkedList(Of Parqueo)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_VerParqueosDisponibles;"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Parqueo")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Parqueo").Rows
        Dim parqueo As New LinkedList(Of Parqueo)()

        For Each currentRow As DataRow In dataRowCollection
            Dim parqueoActual As New Parqueo()
            parqueoActual.GintIdentificadorSG = Long.Parse(currentRow("TN_Identificador_TSP_Parqueo").ToString())
            parqueoActual.GstrTipoSG = currentRow("TC_Tipo_TSP_Parqueo").ToString()
            parqueo.AddLast(parqueoActual)
        Next
        Return parqueo
    End Function

    Public Function obtenerParqueoOcupado(strFecha As String, strHorai As String, strHoraf As String) As LinkedList(Of Parqueo)

        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_VerParqueosOcupados"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@fechaInicio", strFecha))
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@horaInicio", strHorai))
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@horaFinal", strHoraf))

        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Disponibilidad_Parqueo_X_TSP_Parqueo")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Disponibilidad_Parqueo_X_TSP_Parqueo").Rows
        Dim parqueo As New LinkedList(Of Parqueo)()

        For Each currentRow As DataRow In dataRowCollection
            Dim parqueoActual As New Parqueo()
            parqueoActual.GintIdentificadorSG = Long.Parse(currentRow("TN_Idparqueo_TSP_Disponibilidad_Parqueo_X_TSP_Parqueo").ToString())
            parqueo.AddLast(parqueoActual)
        Next
        Return parqueo
    End Function

    Public Function cantidadTiposParqueo() As LinkedList(Of String)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_VerTiposParqueo;"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Parqueo")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Parqueo").Rows

        Dim parqueo As New LinkedList(Of String)()
        For Each currentRow As DataRow In dataRowCollection
            Dim tipoParqueo As String
            tipoParqueo = currentRow("TC_Tipo_TSP_Parqueo").ToString()
            parqueo.AddLast(tipoParqueo)
        Next
        Return parqueo
    End Function

End Class
