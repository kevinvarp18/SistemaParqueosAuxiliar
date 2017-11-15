Imports Datos
Imports Entidad

Public Class SP_Parqueo_Negocios
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.Servicios.
    'DESCRIPCIÓN:
    ' Esta clase maneja la logica de negocios de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Dylan Zamora
    '
    'FECHA DE CREACIÓN                               05-Octubre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  05-Octubre-2017
    '******************************************************************
    'Declaracion de Varaiables.
    Public parqueo_Acceso_a_Datos As SP_Parqueo_Datos

    'Declaracion de constrcutor.
    Public Sub New(gstrconnString As String)
        Me.parqueo_Acceso_a_Datos = New SP_Parqueo_Datos(gstrconnString)
    End Sub
    Public Function insertarParqueo(parqueo As Parqueo) As Parqueo
        Return Me.parqueo_Acceso_a_Datos.insertarParqueo(parqueo)
    End Function

    Public Function actualizarParqueo(parqueo As Parqueo) As Parqueo
        Return Me.parqueo_Acceso_a_Datos.actualizarParqueo(parqueo)
    End Function

    Public Function eliminarParqueo(parqueo As Parqueo) As Parqueo
        Return Me.parqueo_Acceso_a_Datos.eliminarParqueo(parqueo)
    End Function

    Public Function obtenerParqueo() As LinkedList(Of Parqueo)
        Return Me.parqueo_Acceso_a_Datos.obtenerParqueo()
    End Function

    Public Function obtenerParqueoHabilitado() As LinkedList(Of Parqueo)
        Return Me.parqueo_Acceso_a_Datos.obtenerParqueoHabilitado()
    End Function

    Public Function obtenerParqueoOcupado(strfecha As String, strHorai As String, strHoraf As String) As LinkedList(Of Parqueo)
        If strHorai.Length < 6 Then
            strHorai = strHorai + ":00"
        End If
        If strHoraf.Length < 6 Then
            strHoraf = strHoraf + ":00"
        End If
        Return Me.parqueo_Acceso_a_Datos.obtenerParqueoOcupado(strfecha, strHorai, strHoraf)
    End Function

    Public Function cantidadTiposParqueo() As LinkedList(Of String)
        Return Me.parqueo_Acceso_a_Datos.cantidadTiposParqueo()
    End Function

End Class
