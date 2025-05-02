Public Class ADMIN_LOGIN
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ADMIN_SALES_OVERVIEW.Show()
        Me.Hide()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ADMIN_LOGIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class