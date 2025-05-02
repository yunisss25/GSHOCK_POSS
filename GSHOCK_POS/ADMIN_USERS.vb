Public Class ADMIN_USERS
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub ADMIN_USERS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'GshockDataSet5.login' table. You can move, or remove it, as needed.
        Me.LoginTableAdapter.Fill(Me.GshockDataSet5.login)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ADMIN_SALES_OVERVIEW.Show()
        Me.Hide()
    End Sub
End Class