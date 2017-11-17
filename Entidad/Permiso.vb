Public Class Permiso
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.
    'DESCRIPCIÓN:
    ' Esta clase de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Prisiclla Mena
    '
    'FECHA DE CREACIÓN                               10-Noviembre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  10-Noviembre-2017
    '******************************************************************
    'Declaracion Declare variables

    Private gintIdentificador As Integer
    Private gstrTipo As String

    'Declaracion Declare constructor sobrecargado
    Public Sub New(gintIdentificador As Integer, gstrTipo As String)
        Me.GintIdentificador1 = gintIdentificador
        Me.GstrTipo1 = gstrTipo
    End Sub

    Public Sub New()
        Me.GintIdentificador1 = 0
        Me.GstrTipo1 = ""
    End Sub

    'Metodos accesores
    Public Property GintIdentificador1 As Integer
        Get
            Return gintIdentificador
        End Get
        Set(value As Integer)
            gintIdentificador = value
        End Set
    End Property

    Public Property GstrTipo1 As String
        Get
            Return gstrTipo
        End Get
        Set(value As String)
            gstrTipo = value
        End Set
    End Property







End Class
