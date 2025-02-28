Imports System.Data.SqlClient
Public Class LOGIN

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()
        cmd = New SqlCommand("gshock.dbo.login1", con)
        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@user", TextBox1.Text)
            .Parameters.AddWithValue("@pass", TextBox2.Text)
            .Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output
            .ExecuteScalar()

            If CInt(.Parameters("@result").Value) = 1 Then
                MsgBox("good", vbInformation)
                con.Close()
                PRODUCT_LOOK_UP.Show()
                Me.Hide()
            Else
                MsgBox("bad", vbCritical)
                con.Close()
            End If
        End With
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub LOGIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        opencon()
        con.Close()
    End Sub
End Class