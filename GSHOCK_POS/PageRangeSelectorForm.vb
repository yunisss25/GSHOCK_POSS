Public Class PageRangeSelectorForm
    Public Property FromPage As Integer
    Public Property ToPage As Integer

    Private Sub PageRangeSelectorForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nudFromPage.Minimum = 1
        nudToPage.Minimum = 1
        nudFromPage.Value = 1
        nudToPage.Value = 1
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If nudFromPage.Value > nudToPage.Value Then
            MessageBox.Show("From Page must be less than or equal to To Page.", "Invalid Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        FromPage = CInt(nudFromPage.Value)
        ToPage = CInt(nudToPage.Value)
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
