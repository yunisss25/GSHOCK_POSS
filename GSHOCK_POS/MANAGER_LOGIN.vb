Public Class MANAGER_LOGIN
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MANAGER_SALES_OVERVIEW.Show()
    End Sub

    Private Sub MANAGER_LOGIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub
End Class