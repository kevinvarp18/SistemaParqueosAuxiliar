Public Class Administrador
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

    'Declaracion de Variables.
    Public gintAdministrador As Integer

    'Declaracion de Constructor Default.
    Public Sub New()
        Me.gintAdministrador = 0
    End Sub

    'Declaracion de Constructor Sobrecargado.
    Public Sub New(gstrId As String, gstrNombre As String, gstrApellido As String, gstrCorreo As String,
                   gstrContrasenna As String, gstrTipoId As String, gstrTipoUsuario As String)
        MyBase.New(gstrId, gstrNombre, gstrApellido, gstrCorreo, gstrContrasenna, gstrTipoId, gstrTipoUsuario)
    End Sub

    'Declaracion set y get.
    Public Property gintAdministradorSG As Integer
        Get
            Return gintAdministrador
        End Get
        Set(value As Integer)
            gintAdministrador = value
        End Set
    End Property
End Class
