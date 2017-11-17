
Public Class Vinterno
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
    Public gintIdVinterno As Integer
    Public gstrDepartemento As String

    'Declaracion de constructor default.
    Public Sub New()
        gintIdVinterno = 0
        gstrDepartemento = ""
    End Sub

    'Declaracion de constructor sobrecragado.
    Public Sub New(gintIdVinterno As Integer, gstrDepartemento As String)
        Me.gintIdVinterno = gintIdVinterno
        Me.gstrDepartemento = gstrDepartemento
    End Sub

    'Declaracion set y get.
    Public Property gintIdVinternoSG As Integer
        Get
            Return gintIdVinterno
        End Get
        Set(value As Integer)
            gintIdVinterno = value
        End Set
    End Property

    Public Property gstrDepartementoSG As String
        Get
            Return gstrDepartemento
        End Get
        Set(value As String)
            gstrDepartemento = value
        End Set
    End Property
End Class
