Public Class ADMIN_LOGIN
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ADMIN_SALES_OVERVIEW.Show()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class