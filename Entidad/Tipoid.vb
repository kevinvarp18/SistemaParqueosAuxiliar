Public Class Tipoid
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
    Private gintdusuarioTipoUsuario As Integer
    Private gstrTipo As String
    Private gstrIdentificacion As String

    'Declaracion de constructor Default.
    Public Sub New()
        Me.gintdusuarioTipoUsuario = 0
        Me.gstrTipo = ""
        Me.gstrIdentificacion = ""
    End Sub

    'Declaracion de constructor sobrecargado.
    Public Sub New(gintdusuarioTipoUsuario As Integer, gstrTipo As String, gstrIdentificacion As String)
        Me.gintdusuarioTipoUsuario = gintdusuarioTipoUsuario
        Me.gstrTipo = gstrTipo
        Me.gstrIdentificacion = gstrIdentificacion
    End Sub

    'Declaracion de set y get.
    Public Property GintdusuarioTipoUsuarioSG As Integer
        Get
            Return gintdusuarioTipoUsuario
        End Get
        Set(value As Integer)
            gintdusuarioTipoUsuario = value
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

    Public Property GstrIdentificacionSG As String
        Get
            Return gstrIdentificacion
        End Get
        Set(value As String)
            gstrIdentificacion = value
        End Set
    End Property
End Class
