Public Class Solicitud_Visitante
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.BizLayer.
    'DESCRIPCIÓN:
    ' Esta clase de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Dylan Zamora
    '
    'FECHA DE CREACIÓN                               05-Octubre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  05-Octubre-2017
    '******************************************************************

    'Declaracion de variables.
    Private gintIdUsuario As Integer
    Private gintIdSolicitud As Integer
    Private gintEstado As Integer

    'Declaracion de constructor Default.
    Public Sub New()
        Me.gintIdUsuario = 0
        Me.gintIdSolicitud = 0
        Me.gintEstado = 0
    End Sub

    'Declaracion de constructor Sobrecargado.
    Public Sub New(gintIdUsuario As Integer, gintIdSolicitud As Integer, gintEstado As Integer)
        Me.gintIdUsuario = gintIdUsuario
        Me.gintIdSolicitud = gintIdSolicitud
        Me.gintEstado = gintEstado
    End Sub

    'Declaracion de set y get.
    Public Property GintIdUsuarioSG As Integer
        Get
            Return gintIdUsuario
        End Get
        Set(value As Integer)
            gintIdUsuario = value
        End Set
    End Property

    Public Property GintIdSolicitudSG As Integer
        Get
            Return gintIdSolicitud
        End Get
        Set(value As Integer)
            gintIdSolicitud = value
        End Set
    End Property

    Public Property GintEstadoSG As Integer
        Get
            Return gintEstado
        End Get
        Set(value As Integer)
            gintEstado = value
        End Set
    End Property
End Class
