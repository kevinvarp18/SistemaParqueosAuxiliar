Imports System.Data.SqlClient
Imports Entidad
Imports System.Net.Mail
Imports System.Net
Imports System.Web.Configuration

Public Class SP_Usuario_Datos
    '**********************************************************************
    'NOMBRE DEL SISTEMA:  SistemaDeParqueos.
    'NOMBRE DEL PAQUETE:  SistemaDeParqueos.Acceso_a_Datos.
    'DESCRIPCIÓN:
    ' Esta clase maneja las coneciones de las entidades del sistema.

    'NOMBRE DEL DESARROLLADOR:                       Dylan Zamora
    '
    'FECHA DE CREACIÓN                               05-Octubre-2017
    'FECHA DE ULTIMA ACTUALIZACIÓN:                  13-Noviembre-2017
    '******************************************************************
    'Declaracion de Varaiables.
    Public gstrconnString As String

    'Declaracion de constrcutor.
    Public Sub New(gstrconnString As String)
        Me.gstrconnString = gstrconnString
    End Sub

    Public Function insertarOficial(oficial As Oficial) As Boolean
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_InsertaUsuarios"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        Dim result As Integer
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure
        cmdInsert.Parameters.Add(New SqlParameter("@Tipo", oficial.GstrTipoIdSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Identificacion", oficial.GstrIdSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Nombre", oficial.GstrNombreSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Apellidos", oficial.GstrApellidoSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Correo", oficial.GstrCorreoSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Contrasenia", oficial.GstrContrasennaSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Rol", oficial.GstrTipoUsuarioSG))
        cmdInsert.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output
        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        result = Convert.ToInt32(cmdInsert.Parameters("@resultado").Value)
        cmdInsert.Connection.Close()
        Return result
    End Function

    Public Function insertarAdministrador(administrador As Administrador) As Boolean
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_InsertaUsuarios"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        Dim result As Integer
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure
        cmdInsert.Parameters.Add(New SqlParameter("@Tipo", administrador.GstrTipoIdSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Identificacion", administrador.GstrIdSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Nombre", administrador.GstrNombreSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Apellidos", administrador.GstrApellidoSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Correo", administrador.GstrCorreoSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Contrasenia", administrador.GstrContrasennaSG))
        cmdInsert.Parameters.Add(New SqlParameter("@Rol", administrador.GstrTipoUsuarioSG))
        cmdInsert.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output
        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        result = Convert.ToInt32(cmdInsert.Parameters("@resultado").Value)
        cmdInsert.Connection.Close()
        Return result
    End Function
    Public Function insertarVisitante(visitante As Visitante) As Integer
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_RegistrarVisitante"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        Dim result As Integer
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure
        cmdInsert.Parameters.Add(New SqlParameter("@nombre", visitante.GstrNombreSG))
        cmdInsert.Parameters.Add(New SqlParameter("@apellidos", visitante.GstrApellidoSG))
        cmdInsert.Parameters.Add(New SqlParameter("@correo", visitante.GstrCorreoSG))
        cmdInsert.Parameters.Add(New SqlParameter("@contrasena", visitante.GstrContrasennaSG))
        cmdInsert.Parameters.Add(New SqlParameter("@tel", visitante.GintTelefonoSG))
        cmdInsert.Parameters.Add(New SqlParameter("@ubicacion", visitante.GstrUbicacionSG))
        cmdInsert.Parameters.Add(New SqlParameter("@tipoVisitante", visitante.GstrTipoVisitanteSG))
        cmdInsert.Parameters.Add(New SqlParameter("@tipoId", visitante.GstrTipoIdSG))
        cmdInsert.Parameters.Add(New SqlParameter("@id", visitante.gstrId))
        cmdInsert.Parameters.Add(New SqlParameter("@procedencia", visitante.gstrProcedencia))
        cmdInsert.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output
        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        result = Convert.ToInt32(cmdInsert.Parameters("@resultado").Value)
        cmdInsert.Connection.Close()
        Return result
    End Function
    Public Function insertarPermisoRol(id_permiso As Integer, rol As String) As Boolean
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_InsertarPermisoRol"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        Dim result As Integer
        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure
        cmdInsert.Parameters.Add(New SqlParameter("@id_permiso", id_permiso))
        cmdInsert.Parameters.Add(New SqlParameter("@rol", rol))
        cmdInsert.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output
        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()
        result = Convert.ToInt32(cmdInsert.Parameters("@resultado").Value)
        cmdInsert.Connection.Close()
        Return result
    End Function
    Public Function eliminarPermisoRol(id_permiso As Integer, rol As String) As Boolean
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlStoredProcedure As [String] = "PA_EliminarPermisoRol"
        Dim cmdInsert As New SqlCommand(sqlStoredProcedure, connection)
        Dim result As Boolean

        cmdInsert.CommandType = System.Data.CommandType.StoredProcedure

        cmdInsert.Parameters.Add(New SqlParameter("@id_permiso", id_permiso))
        cmdInsert.Parameters.Add(New SqlParameter("@rol", rol))
        cmdInsert.Parameters.Add(New SqlParameter("@resultado", 1))
        cmdInsert.Connection.Open()
        cmdInsert.ExecuteNonQuery()

        result = cmdInsert.Parameters("@resultado").Value
        cmdInsert.Connection.Close()

        Return result
    End Function
    Public Function obtenerPermisosPorRol(rol As String) As LinkedList(Of Permiso)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As String = "PA_ObtenerPermisosRol"
        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@rol", rol))
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "SP.TSP_Permiso")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("SP.TSP_Permiso").Rows
        Dim permisos As New LinkedList(Of Permiso)()

        For Each currentRow As DataRow In dataRowCollection
            Dim permisoActual As New Permiso()
            permisoActual.GintIdentificador1 = Int(currentRow("TN_Id_TSP_Permiso"))
            permisoActual.GstrTipo1 = currentRow("TC_Tipo_TSP_Permiso").ToString()
            permisos.AddLast(permisoActual)
        Next
        Return permisos
    End Function
    Public Function ObtenerPermisos() As LinkedList(Of Permiso)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As String = "PA_ObtenerPermisos"
        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "SP.TSP_Permiso")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("SP.TSP_Permiso").Rows
        Dim permisos As New LinkedList(Of Permiso)()

        For Each currentRow As DataRow In dataRowCollection
            Dim permiso As New Permiso()
            permiso.GintIdentificador1 = currentRow("TN_Id_TSP_Permiso")
            permiso.GstrTipo1 = currentRow("TC_Tipo_TSP_Permiso")
            permisos.AddLast(permiso)
        Next
        Return permisos
    End Function
    Public Function ObtenerRolesYPermisos() As LinkedList(Of RolYPermiso)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As String = "PA_ObtenerRolesYPermisos"
        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "SP.TSP_Permiso")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("SP.TSP_Permiso").Rows
        Dim permisos As New LinkedList(Of RolYPermiso)()

        For Each currentRow As DataRow In dataRowCollection
            Dim permiso As New RolYPermiso()
            permiso.GstrPermiso1 = currentRow("TC_Tipo_TSP_Permiso")
            permiso.GstrRol1 = currentRow("TC_Rol_TSP_Permiso_X_Rol")
            permisos.AddLast(permiso)
        Next
        Return permisos
    End Function
    Public Function obtenerUsuarios(correo As String) As LinkedList(Of Usuario)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As String = "PA_ObtenerUsuarios"
        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@correo", correo))
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "SP.TSP_Usuario")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("SP.TSP_Usuario").Rows
        Dim usuarios As New LinkedList(Of Usuario)()

        For Each currentRow As DataRow In dataRowCollection
            Dim usuarioActual As New Usuario()
            usuarioActual.gstrNombre = currentRow("TC_Nombre_TSP_Usuario").ToString()
            usuarioActual.gstrApellido = currentRow("TC_Apellido_TSP_Usuario").ToString()
            usuarioActual.gstrCorreo = currentRow("TC_Correo_TSP_Usuario").ToString()
            usuarioActual.gstrContrasenna = currentRow("TC_Contrasenna_TSP_Usuario").ToString()
            usuarioActual.gstrTipoUsuario = currentRow("TC_Tipo_TSP_Usuario").ToString()
            usuarios.AddLast(usuarioActual)
        Next
        Return usuarios
    End Function
    Public Function obtenerCorreosUsuarios(strcorreo As String) As LinkedList(Of Usuario)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As String = "PA_VerCorreosExistentes"
        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        sqlDataAdapterClient.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        sqlDataAdapterClient.SelectCommand.Parameters.Add(New SqlParameter("@Correo", strcorreo))
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "SP.TSP_Usuario")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("SP.TSP_Usuario").Rows
        Dim usuarios As New LinkedList(Of Usuario)()

        For Each currentRow As DataRow In dataRowCollection
            Dim usuarioActual As New Usuario()
            usuarioActual.gstrNombre = currentRow("TC_Nombre_TSP_Usuario").ToString()
            usuarioActual.gstrApellido = currentRow("TC_Apellido_TSP_Usuario").ToString()
            usuarioActual.gstrCorreo = currentRow("TC_Correo_TSP_Usuario").ToString()
            usuarioActual.gstrContrasenna = currentRow("TC_Contrasenna_TSP_Usuario").ToString()
            usuarioActual.gstrTipoUsuario = 0
            usuarios.AddLast(usuarioActual)
        Next
        Return usuarios
    End Function
    Public Function recuperacionContrasenaMail(strCorreo As String) As Boolean
        Dim correo As New MailMessage
        Dim smtp As New SmtpClient()

        Dim blnExite = False

        Dim usuarios As New LinkedList(Of Usuario)
        usuarios = obtenerCorreosUsuarios(strCorreo)

        If (usuarios.Count > 0) Then
            For Each usuarioActual As Usuario In usuarios
                correo.From = New MailAddress("sistemaparqueosoij@gmail.com", "Sistema Parqueos OIJ", System.Text.Encoding.UTF8)
                correo.[To].Add(usuarioActual.gstrCorreo)
                correo.SubjectEncoding = System.Text.Encoding.UTF8
                correo.Subject = "Recuperación de Contraseña"
                correo.Body = Convert.ToString("Hola " + usuarioActual.gstrNombre + " " + usuarioActual.gstrApellido + ", su contraseña para el Sistema de Parqueos del OIJ es:" + usuarioActual.gstrContrasenna)
                correo.BodyEncoding = System.Text.Encoding.UTF8
                correo.IsBodyHtml = (True)
                correo.Priority = MailPriority.High
                smtp.Credentials = New System.Net.NetworkCredential("sistemaparqueosoij@gmail.com", "OIJ.SistemaParqueos")
                smtp.Port = 587
                smtp.Host = "smtp.gmail.com"
                smtp.EnableSsl = True

                Try
                    smtp.Send(correo)
                    blnExite = True
                Catch
                    blnExite = False
                End Try
            Next
        End If

        Return blnExite
    End Function
    Public Function obtenerCorreoUsuariosVisitantes() As LinkedList(Of Usuario)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_Vercorreos;"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "TSP_Usuario")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("TSP_Usuario").Rows
        Dim usuarios As New LinkedList(Of Usuario)()

        For Each currentRow As DataRow In dataRowCollection
            Dim usuarioActual As New Usuario()
            usuarioActual.gstrCorreo = currentRow("TC_Correo_TSP_Usuario").ToString()
            usuarios.AddLast(usuarioActual)
        Next
        Return usuarios
    End Function
    Public Function obtenerPlacas() As LinkedList(Of String)
        Dim connection As New SqlConnection(Me.gstrconnString)
        Dim sqlSelect As [String] = "PA_VerPlacas;"

        Dim sqlDataAdapterClient As New SqlDataAdapter()
        sqlDataAdapterClient.SelectCommand = New SqlCommand()
        sqlDataAdapterClient.SelectCommand.CommandText = sqlSelect
        sqlDataAdapterClient.SelectCommand.Connection = connection
        Dim dataSetAttendant As New DataSet()
        sqlDataAdapterClient.Fill(dataSetAttendant, "[TSP_Solicitud]")
        sqlDataAdapterClient.SelectCommand.Connection.Close()
        Dim dataRowCollection As DataRowCollection = dataSetAttendant.Tables("[TSP_Solicitud]").Rows
        Dim placas As New LinkedList(Of String)()

        For Each currentRow As DataRow In dataRowCollection
            placas.AddLast(currentRow("TC_Placa_TSP_Solicitud").ToString())
        Next
        Return placas
    End Function
    Public Sub envioRespuestaSolicitud(strAsunto As String, strCorreo As String, strMensaje As String)
        Dim correo As New MailMessage
        Dim smtp As New SmtpClient()

        Dim usuarios As New LinkedList(Of Usuario)
        usuarios = obtenerCorreosUsuarios(strCorreo)

        If (usuarios.Count > 0) Then
            For Each usuarioActual As Usuario In usuarios
                correo.From = New MailAddress("sistemaparqueosoij@gmail.com", "Sistema Parqueos OIJ", System.Text.Encoding.UTF8)
                correo.[To].Add(usuarioActual.gstrCorreo)
                correo.SubjectEncoding = System.Text.Encoding.UTF8
                correo.Subject = strAsunto
                correo.Body = Convert.ToString("Hola " + usuarioActual.gstrNombre + " " + usuarioActual.gstrApellido + "." + strMensaje)
                correo.BodyEncoding = System.Text.Encoding.UTF8
                correo.IsBodyHtml = (True)
                correo.Priority = MailPriority.High
                smtp.Credentials = New System.Net.NetworkCredential("sistemaparqueosoij@gmail.com", "OIJ.SistemaParqueos")
                smtp.Port = 587
                smtp.Host = "smtp.gmail.com"
                smtp.EnableSsl = True

                Try
                    smtp.Send(correo)
                Catch
                End Try
            Next
        End If
    End Sub

End Class