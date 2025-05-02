Public Class CASH_CASHIER

    ' Event when the form is loaded
    Private Sub CASH_CASHIER_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Get the total amount from TextBox2 in PRODUCT_LOOK_UP form
        txtAmountDue.Text = PRODUCT_LOOK_UP.TextBox2.Text
        txtAmountTendered.Text = ""
        txtChange.Text = "0.00"
    End Sub

    ' When Submit is clicked
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim amountDue As Decimal
        Dim amountTendered As Decimal

        ' Validate input for amount due
        If Not Decimal.TryParse(txtAmountDue.Text, amountDue) OrElse amountDue <= 0 Then
            MessageBox.Show("Invalid amount due.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Validate input for amount tendered
        If Not Decimal.TryParse(txtAmountTendered.Text, amountTendered) OrElse amountTendered < amountDue Then
            MessageBox.Show("Insufficient amount tendered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Calculate and show change
        Dim change As Decimal = amountTendered - amountDue
        txtChange.Text = change.ToString("0.00")

        ' Show success message with change
        MessageBox.Show("Cash payment successful. Change: ₱" & txtChange.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Proceed to the next form (example: CRYSTAL form)
        CRYSTAL.Show()
        Me.Hide()
    End Sub

    ' Allow only numeric input with decimals
    Private Sub txtAmountTendered_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmountTendered.KeyPress
        If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "."c OrElse Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    ' Close the Cashier form
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Exit the application
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    ' Go back to the PRODUCT_LOOK_UP form
    Private Sub MP_Click(sender As Object, e As EventArgs) Handles MP.Click
        PRODUCT_LOOK_UP.Show()
        Me.Hide()
    End Sub

    ' Go to the PAYMENT form
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PAYMENT.Show()
        Me.Hide()
    End Sub
End Class
