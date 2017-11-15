Imports Datos
Imports Entidad

Public Class SP_Solicitud_Parqueo_Negocios
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.Servicios.
    'DESCRIPCIÓN:
    ' Esta clase maneja la logica de negocios de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Dylan Zamora
    '
    'FECHA DE CREACIÓN                               05-Octubre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  13-Noviembre-2017
    '******************************************************************
    'Declaracion de Varaiables.
    Public solicitud_Acceso_a_Datos As SP_Solicitud_Datos

    'Declaracion de constrcutor.
    Public Sub New(gstrconnString As String)
        Me.solicitud_Acceso_a_Datos = New SP_Solicitud_Datos(gstrconnString)
    End Sub
    Public Function insertarSolicitud(correo As String, solicitud As Solicitud) As Solicitud
        Return Me.solicitud_Acceso_a_Datos.insertarSolicitud(correo, solicitud)
    End Function
    Public Function obtenerSolicitudes() As LinkedList(Of Solicitud)
        Return Me.solicitud_Acceso_a_Datos.obtenerSolicitud()
    End Function
    Public Function obtenerAdSolicitud() As LinkedList(Of Solicitud)
        Return Me.solicitud_Acceso_a_Datos.obtenerAdSolicitud()
    End Function
    Public Function obtenerNumeroSolicitudes() As String
        Return Me.solicitud_Acceso_a_Datos.obtenerNumeroSolicitudes()
    End Function
    Public Function obtenerReporte(fecha_i As String, fecha_f As String) As LinkedList(Of Solicitud)
        Return Me.solicitud_Acceso_a_Datos.obtenerReporte(fecha_i, fecha_f)
    End Function

    Public Function obtenerReportePlaca(placa As String) As LinkedList(Of Solicitud)
        Return Me.solicitud_Acceso_a_Datos.obtenerReportePlaca(placa)
    End Function
    Public Function obtenerReporteCorreo(correo As String) As LinkedList(Of Solicitud)
        Return Me.solicitud_Acceso_a_Datos.obtenerReporteCorreo(correo)
    End Function

    Public Function decidirSolicitud(marca As String, placa As String, horaI As String, horaF As String, fechaI As String, fechaF As String, idParqueo As String, accion As String) As Integer
        Return Me.solicitud_Acceso_a_Datos.decidirSolicitud(marca, placa, horaI, horaF, fechaI, fechaF, idParqueo, accion)
    End Function
    Public Function obtenerSolicitudesHoy() As LinkedList(Of Solicitud)
        Return Me.solicitud_Acceso_a_Datos.obtenerSolicitudesHoy()
    End Function
    Public Sub marcarEntrada_Salida(marca As String, placa As String, modelo As String, espacioParqueo As Integer, horaEntrada As String, horaSalida As String, accion As Integer)
        Me.solicitud_Acceso_a_Datos.marcarEntrada_Salida(marca, placa, modelo, espacioParqueo, horaEntrada, horaSalida, accion)
    End Sub
    Public Function obtenerNumeroVisitantesAtrasados() As String
        Return Me.solicitud_Acceso_a_Datos.obtenerNumeroVisitantesAtrasados()
    End Function
    Public Function obtenerVisitantesAtrasados() As LinkedList(Of Solicitud)
        Return Me.solicitud_Acceso_a_Datos.obtenerVisitantesAtrasados()
    End Function
    Public Function decidirSolicitudAtrasada(marca As String, placa As String, modelo As String, idParqueo As Integer, horaI As String, horaF As String, horaNueva As String, accion As String) As Integer
        Return Me.solicitud_Acceso_a_Datos.decidirSolicitudAtrasada(marca, placa, modelo, idParqueo, horaI, horaF, horaNueva, accion)
    End Function

    Public Function obtenerReporteCedula(cedula As String) As LinkedList(Of Solicitud)
        Return Me.solicitud_Acceso_a_Datos.obtenerReporteCedula(cedula)
    End Function

    Public Function ObtenerCedulasYNombres() As LinkedList(Of Usuario)
        Return Me.solicitud_Acceso_a_Datos.ObtenerCedulasYNombres()
    End Function

    Public Function ObtenerDepartamentos() As LinkedList(Of String)
        Return Me.solicitud_Acceso_a_Datos.ObtenerDepartamentos()
    End Function

    Public Function ObtenerInstituciones() As LinkedList(Of String)
        Return Me.solicitud_Acceso_a_Datos.ObtenerInstituciones()
    End Function

    Public Function obtenerReporteInstitucion(institucion As String) As LinkedList(Of Solicitud)
        Return Me.solicitud_Acceso_a_Datos.obtenerReporteInstitucion(institucion)
    End Function

    Public Function obtenerReporteDepartamento(departamento As String) As LinkedList(Of Solicitud)
        Return Me.solicitud_Acceso_a_Datos.obtenerReporteDepartamento(departamento)
    End Function

End Class
