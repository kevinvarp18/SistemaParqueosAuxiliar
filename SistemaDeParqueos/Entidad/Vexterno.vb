Public Class Vexterno
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
    Public gintIdVexterno As Integer
    Public gstrInstitucion As String

    'Declaracion de constructor default.
    Public Sub New()
        gintIdVexterno = 0
        gstrInstitucion = ""
    End Sub

    'Declaracion de constructor sobrecragado.
    Public Sub New(gintIdVexterno As Integer, gstrInstitucion As String)
        Me.gintIdVexterno = gintIdVexterno
        Me.gstrInstitucion = gstrInstitucion
    End Sub

    'Declaracion set y get.
    Public Property gintIdVexternoSG As Integer
        Get
            Return gintIdVexterno
        End Get
        Set(value As Integer)
            gintIdVexterno = value
        End Set
    End Property

    Public Property gstrInstitucionSG As String
        Get
            Return gstrInstitucion
        End Get
        Set(value As String)
            gstrInstitucion = value
        End Set
    End Property
End Class
