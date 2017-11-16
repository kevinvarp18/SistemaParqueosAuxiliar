
Public Class Oficial
    Inherits Usuario
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.Dominio.
    'DESCRIPCIÓN:
    ' Esta clase de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Dylan Zamora
    '
    'FECHA DE CREACIÓN                              05-Octubre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                 21-Octubre-2017
    '******************************************************************

    'Declaracion de Variables.
    Public gintOficial As Integer


    'Declaracion de constructor sobrecargado.
    Public Sub New()
        Me.gintOficial = 0
    End Sub

    'Declaracion de constructor sobrecargado.
    Public Sub New(gstrId As String, gstrNombre As String, gstrApellido As String, gstrCorreo As String,
                   gstrContrasenna As String, gstrTipoId As String, gstrTipoUsuario As String)
        MyBase.New(gstrId, gstrNombre, gstrApellido, gstrCorreo, gstrContrasenna, gstrTipoId, gstrTipoUsuario)
    End Sub

    'Declaracion de Set y Get.
    Public Property gintOficialSG As Integer
        Get
            Return gintOficial
        End Get
        Set(value As Integer)
            gintOficial = value
        End Set
    End Property
End Class
