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
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  13-Octubre-2017
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

    Public Function insertarVisitante(visitante As Visitante) As Integer
        Return Me.usuario_Acceso_a_Datos.insertarVisitante(visitante)
    End Function

    Public Function insertarPermisoRol(id_permiso As Integer, rol As String) As Boolean
        Return Me.usuario_Acceso_a_Datos.insertarPermisoRol(id_permiso, rol)
    End Function

    Public Function eliminarPermisoRol(id_permiso As Integer, rol As String) As Boolean
        Return Me.usuario_Acceso_a_Datos.eliminarPermisoRol(id_permiso, rol)
    End Function

    Public Function obtenerPermisosPorRol(rol As String) As LinkedList(Of Permiso)
        Return Me.usuario_Acceso_a_Datos.obtenerPermisosPorRol(rol)
    End Function

    Public Function ObtenerPermisos() As LinkedList(Of Permiso)
        Return Me.usuario_Acceso_a_Datos.ObtenerPermisos()
    End Function

    Public Function ObtenerRolesYPermisos() As LinkedList(Of RolYPermiso)
        Return Me.usuario_Acceso_a_Datos.ObtenerRolesYPermisos()
    End Function

    Public Function insertarAdministrador(administrador As Administrador) As Boolean
        Return Me.usuario_Acceso_a_Datos.insertarAdministrador(administrador)
    End Function
    Public Function obtenerUsuarios(correo As String) As LinkedList(Of Usuario)
        Return Me.usuario_Acceso_a_Datos.obtenerUsuarios(correo)
    End Function
    Public Function obtenerCorreosUsuarios(strcorreo As String) As LinkedList(Of Usuario)
        Return Me.usuario_Acceso_a_Datos.obtenerCorreosUsuarios(strcorreo)
    End Function
    Public Function recuperacionContrasenaMail(strCorreo As String) As Boolean
        Return Me.usuario_Acceso_a_Datos.recuperacionContrasenaMail(strCorreo)
    End Function

    Public Function obtenerCorreoUsuariosVisitantes() As LinkedList(Of Usuario)
        Return Me.usuario_Acceso_a_Datos.obtenerCorreoUsuariosVisitantes()
    End Function

    Public Function obtenerPlacas() As LinkedList(Of String)
        Return Me.usuario_Acceso_a_Datos.obtenerPlacas()
    End Function

    Public Sub envioCorreoSolicitud(strAsunto As String, strCorreo As String, strMensaje As String)
        Me.usuario_Acceso_a_Datos.envioRespuestaSolicitud(strAsunto, strCorreo, strMensaje)
    End Sub
End Class