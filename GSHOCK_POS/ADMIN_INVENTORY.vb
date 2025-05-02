Public Class ADMIN_INVENTORY
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub ADMIN_INVENTORY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'GshockDataSet6.products' table. You can move, or remove it, as needed.
        Me.ProductsTableAdapter.Fill(Me.GshockDataSet6.products)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ADMIN_SALES_OVERVIEW.ShowDialog()
        Me.Hide()
    End Sub
End Class