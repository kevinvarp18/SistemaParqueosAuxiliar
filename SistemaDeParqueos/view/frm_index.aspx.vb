Public Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("Login") Is Nothing) Then
            Session("Login") = "N"
        ElseIf String.Equals(Session("Login"), "N") Then
        Else
            Session("Login") = "N"
        End If 'Fin del if.
    End Sub
End Class