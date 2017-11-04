Imports Datos
Imports Entidad

Public Class SP_Usuario_Negocios
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.Negocios.
    'DESCRIPCIÓN:
    ' Esta clase maneja la logica de negocios de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Dylan Zamora
    '
    'FECHA DE CREACIÓN                               05-Octubre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  21-Octubre-2017
    '******************************************************************
    'Declaracion de Varaiables.
    Public usuario_Acceso_a_Datos As SP_Usuario_Datos

    'Declaracion de constrcutor.
    Public Sub New(gstrconnString As String)
        Me.usuario_Acceso_a_Datos = New SP_Usuario_Datos(gstrconnString)
    End Sub

    Public Function insertarOficial(oficial As Oficial) As Boolean
        Return Me.usuario_Acceso_a_Datos.insertarOficial(oficial)
    End Function

    Public Function insertarVisitante(visitante As Visitante) As Boolean
        Return Me.usuario_Acceso_a_Datos.insertarVisitante(visitante)
    End Function

    Public Function insertarAdministrador(administrador As Administrador) As Boolean
        Return Me.usuario_Acceso_a_Datos.insertarAdministrador(administrador)
    End Function
    Public Function obtenerUsuarios(correo As String, contrasenna As String) As LinkedList(Of Usuario)
        Return Me.usuario_Acceso_a_Datos.obtenerUsuarios(correo, contrasenna)
    End Function

    Public Function obtenerCorreosUsuarios(strcorreo As String) As LinkedList(Of Usuario)
        Return Me.usuario_Acceso_a_Datos.obtenerCorreosUsuarios(strcorreo)
    End Function

    Public Function EnvioMail(strCorreo As String) As Boolean
        Return Me.usuario_Acceso_a_Datos.EnvioMail(strCorreo)
    End Function

    Public Function obtenerCorreoUsuariosVisitantes() As LinkedList(Of Usuario)
        Return Me.usuario_Acceso_a_Datos.obtenerCorreoUsuariosVisitantes()
    End Function

End Class