Public Class PAYMENT
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

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
End Class