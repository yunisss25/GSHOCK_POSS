Imports System.Data.SqlClient

Public Class PAYMENT

    ' SQL Connection String
    Dim conn As New SqlConnection("Data Source=172.20.10.2;Initial Catalog=gshock;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Insert today's date into the database
        InsertTodayDate()
    End Sub

    Private Sub InsertTodayDate()
        Try
            conn.Open()

            Dim cmd As New SqlCommand("
                INSERT INTO ShockSystem (date) VALUES (@TodayDate)
            ", conn)

            ' Add today's date
            cmd.Parameters.AddWithValue("@TodayDate", Date.Today)

            cmd.ExecuteNonQuery()

            MessageBox.Show("Today's date has been saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error inserting date: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Text to be encoded in the QR code
        Dim textToEncode As String = "Hello, this is a QR code in VB.NET!"

        ' Create an instance of CASH_QR
        Dim cashQR As New CASH_QR()

        ' Call the GenerateQRCode method in CASH_QR and pass the text
        cashQR.GenerateQRCode(textToEncode)

        ' Show CASH_QR form
        cashQR.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CARD_CASHIER.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        PRODUCT_LOOK_UP.Show()
    End Sub

    Private Sub PAYMENT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Optional: actions when form loads
    End Sub

End Class
