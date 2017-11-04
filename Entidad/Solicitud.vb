Public Class Solicitud
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
    Private gintIdSolicutud As Integer
    Private gintIdVisitante As Integer
    Private gintIdParqueo As Integer
    Private gstrHoraI As String
    Private gstrHoraF As String
    Private gstrPlaca As String
    Private gstrModelo As String
    Private gstrMarca As String
    Private gstrFechaI As String
    Private gstrFechaF As String

    'Declaracion constructor default.
    Public Sub New()
        Me.gintIdSolicutud = 0
        Me.gintIdVisitante = 0
        Me.gintIdParqueo = 0
        Me.gstrHoraI = ""
        Me.gstrHoraF = ""
        Me.gstrPlaca = ""
        Me.gstrModelo = ""
        Me.gstrMarca = ""
        Me.gstrFechaI = ""
        Me.gstrFechaF = ""
    End Sub

    'Declaracion de constructor sobrecargado.
    Public Sub New(gintIdSolicutud As Integer, gintIdVisitante As Integer, gintIdParqueo As Integer, gstrHoraI As String, gstrHoraF As String, gstrPlaca As String, gstrModelo As String, gstrMarca As String, gstrFechaI As String, gstrFechaF As String)
        Me.gintIdSolicutud = gintIdSolicutud
        Me.gintIdVisitante = gintIdVisitante
        Me.gintIdParqueo = gintIdParqueo
        Me.gstrHoraI = gstrHoraI
        Me.gstrHoraF = gstrHoraF
        Me.gstrPlaca = gstrPlaca
        Me.gstrModelo = gstrModelo
        Me.gstrMarca = gstrMarca
        Me.gstrFechaI = gstrFechaI
        Me.gstrFechaF = gstrFechaF
    End Sub

    'Declaracion de set y Get.
    Public Property GintIdSolicutudSG As Integer
        Get
            Return gintIdSolicutud
        End Get
        Set(value As Integer)
            gintIdSolicutud = value
        End Set
    End Property

    Public Property GintIdVisitanteSG As Integer
        Get
            Return gintIdVisitante
        End Get
        Set(value As Integer)
            gintIdVisitante = value
        End Set
    End Property

    Public Property GintIdParqueoSG As Integer
        Get
            Return gintIdParqueo
        End Get
        Set(value As Integer)
            gintIdParqueo = value
        End Set
    End Property

    Public Property GstrHoraISG As String
        Get
            Return gstrHoraI
        End Get
        Set(value As String)
            gstrHoraI = value
        End Set
    End Property

    Public Property GstrHoraFSG As String
        Get
            Return gstrHoraF
        End Get
        Set(value As String)
            gstrHoraF = value
        End Set
    End Property

    Public Property GstrPlacaSG As String
        Get
            Return gstrPlaca
        End Get
        Set(value As String)
            gstrPlaca = value
        End Set
    End Property

    Public Property GstrModeloSG As String
        Get
            Return gstrModelo
        End Get
        Set(value As String)
            gstrModelo = value
        End Set
    End Property

    Public Property GstrMarcaSG As String
        Get
            Return gstrMarca
        End Get
        Set(value As String)
            gstrMarca = value
        End Set
    End Property

    Public Property GstrFechaISG As String
        Get
            Return gstrFechaI
        End Get
        Set(value As String)
            gstrFechaI = value
        End Set
    End Property

    Public Property GstrFechaFSG As String
        Get
            Return gstrFechaF
        End Get
        Set(value As String)
            gstrFechaF = value
        End Set
    End Property
End Class
