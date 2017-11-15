Public Class RolYPermiso
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.
    'DESCRIPCIÓN:
    ' Esta clase de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Adrian Serrano
    '
    'FECHA DE CREACIÓN                               14-Noviembre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  14-Noviembre-2017
    '******************************************************************
    'Declaracion Declare variables
    Private gstrPermiso As String
    Private gstrRol As String

    'Declaracion Declare constructor sobrecargado
    Public Sub New(gstrPermiso As String, gstrRol As String)
        Me.GstrPermiso1 = gstrPermiso
        Me.GstrRol1 = gstrRol
    End Sub

    Public Sub New()
        Me.GstrPermiso1 = ""
        Me.GstrRol1 = ""
    End Sub

    'Metodos accesores
    Public Property GstrPermiso1 As String
        Get
            Return gstrPermiso
        End Get
        Set(value As String)
            gstrPermiso = value
        End Set
    End Property

    Public Property GstrRol1 As String
        Get
            Return gstrRol
        End Get
        Set(value As String)
            gstrRol = value
        End Set
    End Property


End Class
