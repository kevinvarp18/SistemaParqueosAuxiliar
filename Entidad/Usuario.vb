Public Class Usuario
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.BizLayer.
    'DESCRIPCIÓN:
    ' Esta clase de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Dylan Zamora
    '
    'FECHA DE CREACIÓN                               05-Octubre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  21-Octubre-2017
    '******************************************************************

    'Declaracionde Variables.
    Public gstrId As String
    Public gstrNombre As String
    Public gstrApellido As String
    Public gstrCorreo As String
    Public gstrContrasenna As String
    Public gstrTipoId As String
    Public gstrTipoUsuario As String

    'Declarion de constructor default.
    Public Sub New()
        Me.gstrId = ""
        Me.gstrNombre = ""
        Me.gstrApellido = ""
        Me.gstrCorreo = ""
        Me.gstrContrasenna = ""
        Me.gstrTipoId = ""
        Me.gstrTipoUsuario = ""
    End Sub

    'Declaraccion Constructor Sobrecargado.
    Public Sub New(gstrId As String, gstrNombre As String, gstrApellido As String, gstrCorreo As String, gstrContrasenna As String, gstrTipoId As String, gstrTipoUsuario As String)
        Me.gstrId = gstrId
        Me.gstrNombre = gstrNombre
        Me.gstrApellido = gstrApellido
        Me.gstrCorreo = gstrCorreo
        Me.gstrContrasenna = gstrContrasenna
        Me.gstrTipoId = gstrTipoId
        Me.gstrTipoUsuario = gstrTipoUsuario
    End Sub

    'Set y Get.
    Public Property GstrIdSG As String
        Get
            Return gstrId
        End Get
        Set(value As String)
            gstrId = value
        End Set
    End Property

    Public Property GstrNombreSG As String
        Get
            Return gstrNombre
        End Get
        Set(value As String)
            gstrNombre = value
        End Set
    End Property

    Public Property GstrApellidoSG As String
        Get
            Return gstrApellido
        End Get
        Set(value As String)
            gstrApellido = value
        End Set
    End Property

    Public Property GstrCorreoSG As String
        Get
            Return gstrCorreo
        End Get
        Set(value As String)
            gstrCorreo = value
        End Set
    End Property

    Public Property GstrContrasennaSG As String
        Get
            Return gstrContrasenna
        End Get
        Set(value As String)
            gstrContrasenna = value
        End Set
    End Property

    Public Property GstrTipoIdSG As String
        Get
            Return gstrTipoId
        End Get
        Set(value As String)
            gstrTipoId = value
        End Set
    End Property

    Public Property GstrTipoUsuarioSG As String
        Get
            Return gstrTipoUsuario
        End Get
        Set(value As String)
            gstrTipoUsuario = value
        End Set
    End Property
End Class
