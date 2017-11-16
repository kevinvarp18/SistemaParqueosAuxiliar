Public Class Visitante
    Inherits Usuario
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.Dominio.
    'DESCRIPCIÓN:
    ' Esta clase de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Dylan Zamora
    '
    'FECHA DE CREACIÓN                               05-Octubre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  21-Octubre-2017
    '******************************************************************
    'Declaracion de variables.
    Public gintTelefono As Integer
    Public gstrUbicacion As String
    Public gstrTipoVisitante As String
    Public gstrProcedencia As String

    'Constructor Default.
    Public Sub New()
        Me.gintTelefono = ""
        Me.gstrUbicacion = ""
        Me.gstrTipoVisitante = ""
        Me.gstrProcedencia = ""
    End Sub

    'Constructor sobrecargado.
    Public Sub New(gstrId As String, gstrNombre As String, gstrApellido As String, gstrCorreo As String,
                   gstrContrasenna As String, gstrTipoId As String, gstrTipoUsuario As String, telefono As String,
                   ubicacion As String, tipoVisitante As String, procedencia As String)
        MyBase.New(gstrId, gstrNombre, gstrApellido, gstrCorreo, gstrContrasenna, gstrTipoId, gstrTipoUsuario)
        Me.gintTelefono = telefono
        Me.gstrUbicacion = ubicacion
        Me.gstrTipoVisitante = tipoVisitante
        Me.gstrProcedencia = procedencia
    End Sub

    'Set y get.
    Public Property GintTelefonoSG As String
        Get
            Return gintTelefono
        End Get
        Set(value As String)
            gintTelefono = value
        End Set
    End Property

    Public Property GstrUbicacionSG As String
        Get
            Return gstrUbicacion
        End Get
        Set(value As String)
            gstrUbicacion = value
        End Set
    End Property

    Public Property GstrTipoVisitanteSG As String
        Get
            Return gstrTipoVisitante
        End Get
        Set(value As String)
            gstrTipoVisitante = value
        End Set
    End Property
End Class
