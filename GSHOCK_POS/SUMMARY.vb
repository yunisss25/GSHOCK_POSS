Public Class SUMMARY
    Private Sub SUMMARY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'GshockDataSet4.lookup' table. You can move, or remove it, as needed.
        Me.LookupTableAdapter.Fill(Me.GshockDataSet4.lookup)

    End Sub
End Class