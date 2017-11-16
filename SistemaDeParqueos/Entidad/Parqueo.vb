Public Class Parqueo
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
    Private gintIdentificador As Long
    Private gintDisponible As Boolean
    Private gstrTipo As String

    'Declaracion de constructor default.
    Public Sub New()
        Me.gintIdentificador = 0
        Me.gintDisponible = 0
        Me.gstrTipo = ""
    End Sub

    'Declaracion de constructor sobrecargado.
    Public Sub New(gintIdentificador As Long, gintDisponible As Boolean, gstrTipo As String)
        Me.gintIdentificador = gintIdentificador
        Me.gintDisponible = gintDisponible
        Me.gstrTipo = gstrTipo
    End Sub

    'Declaracion de set y Get.
    Public Property GintIdentificadorSG As Long
        Get
            Return gintIdentificador
        End Get
        Set(value As Long)
            gintIdentificador = value
        End Set
    End Property

    Public Property GintDisponibleSG As Boolean
        Get
            Return gintDisponible
        End Get
        Set(value As Boolean)
            gintDisponible = value
        End Set
    End Property

    Public Property GstrTipoSG As String
        Get
            Return gstrTipo
        End Get
        Set(value As String)
            gstrTipo = value
        End Set
    End Property
End Class
