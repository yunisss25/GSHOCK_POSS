Imports System.Data.SqlClient

Public Class PRODUCT_LOOK_UP

    ' Connection String
    Private con As New SqlConnection("Data Source=MS-SOPHIE;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Private total As Decimal = 0

    ' Store matched products
    Private matchedProducts As New List(Of Product)()
    Private currentIndex As Integer = -1 ' To keep track of the current index in matched products

    ' Product structure to hold product data
    Public Class Product
        Public Property ID As String
        Public Property ProductName As String
        Public Property Series As String
        Public Property Price As Decimal
        Public Property Quantity As Integer
    End Class

    Private Sub PRODUCT_LOOK_UP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Using con As New SqlConnection("Data Source=MS-SOPHIE;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                con.Open()

                Dim inventoryAdapter As New SqlDataAdapter("SELECT * FROM gshock.dbo.products", con)
                Dim inventoryTable As New DataTable()
                inventoryAdapter.Fill(inventoryTable)
                DataGridView1.DataSource = inventoryTable

                Dim cartAdapter As New SqlDataAdapter("SELECT * FROM gshock.dbo.lookup", con)
                Dim cartTable As New DataTable()
                cartAdapter.Fill(cartTable)
                DataGridView2.DataSource = cartTable
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        If Not String.IsNullOrWhiteSpace(TextBox5.Text) AndAlso IsNumeric(TextBox1.Text) Then
            Dim quantity As Integer = Convert.ToInt32(TextBox1.Text)
            If quantity > 0 Then
                AddToCart(TextBox5.Text, TextBox6.Text, quantity)
            Else
                MessageBox.Show("Quantity must be greater than 0.")
            End If
        Else
            MessageBox.Show("Please select a product and enter a valid quantity.")
        End If
    End Sub

    Private Sub AddToCart(productId As String, productName As String, quantity As Integer)
        If quantity <= 0 Then Exit Sub

        Try
            Using con As New SqlConnection("Data Source=MS-SOPHIE;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                con.Open()

                Dim cmd As New SqlCommand("SELECT id, productname, series, price, quantity FROM gshock.dbo.products WHERE id = @id", con)
                cmd.Parameters.AddWithValue("@id", productId)

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim stock As Integer = Convert.ToInt32(reader("quantity"))
                        Dim price As Decimal = Convert.ToDecimal(reader("price"))
                        Dim series As String = reader("series").ToString()
                        Dim Name As String = reader("productname").ToString()
                        reader.Close()

                        If stock >= quantity Then
                            Dim totalPrice As Decimal = price * quantity
                            total += totalPrice

                            Dim updateTotalCmd As New SqlCommand("UPDATE gshock.dbo.products SET total = @total WHERE id = @id", con)
                            updateTotalCmd.Parameters.AddWithValue("@total", price * stock)
                            updateTotalCmd.Parameters.AddWithValue("@id", productId)
                            updateTotalCmd.ExecuteNonQuery()

                            UpdateInventory(con, productId, quantity)
                            UpdateCart(con, productId, Name, series, quantity, totalPrice)

                            TextBox2.Text = total.ToString("F2")
                            ClearProductFields()
                            LoadData()
                        Else
                            MessageBox.Show("Not enough stock for " & Name)
                            Me.Close()
                        End If
                    Else
                        MessageBox.Show("Product not found!")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Database error: " & ex.Message)
        End Try
    End Sub

    Private Sub UpdateInventory(con As SqlConnection, productId As String, quantity As Integer)
        Try
            Using cmd As New SqlCommand("UPDATE gshock.dbo.products SET quantity = quantity - @quantity WHERE id = @id", con)
                cmd.Parameters.AddWithValue("@quantity", quantity)
                cmd.Parameters.AddWithValue("@id", productId)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating inventory: " & ex.Message)
        End Try
    End Sub

    Private Sub ClearProductFields()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox1.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim payment As Decimal
        If Decimal.TryParse(TextBox3.Text, payment) Then
            If payment < 0 Then
                MessageBox.Show("Payment cannot be negative!")
            ElseIf payment >= total Then
                ' Set TextBox4.Text to show the payment amount formatted as currency
                TextBox4.Text = payment.ToString("F2")

                ' Insert payment details into the database
                Try
                    Using con As New SqlConnection("Data Source=MS-SOPHIE;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                        con.Open()
                        Dim cmd As New SqlCommand("INSERT INTO gshock.dbo.lookup (payment, [date]) VALUES (@payment, @date)", con)
                        cmd.Parameters.AddWithValue("@payment", payment)
                        cmd.Parameters.AddWithValue("@date", DateTime.Now)
                        cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Error saving change: " & ex.Message)
                End Try

                ' Notify user of success
                MessageBox.Show("Payment successful! Change: " & TextBox4.Text)
                LoadData() ' Assuming LoadData is a method you have defined elsewhere
            Else
                MessageBox.Show("Insufficient payment!")
            End If
        Else
            MessageBox.Show("Invalid payment amount!")
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        StartNewTransaction()
    End Sub

    Private Sub StartNewTransaction()
        total = 0
        TextBox1.Text = "1"
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()

        Try
            Using con As New SqlConnection("Data Source=MS-SOPHIE;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                con.Open()
                Dim cmd As New SqlCommand("DELETE FROM gshock.dbo.lookup", con)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error clearing cart: " & ex.Message)
        End Try

        MessageBox.Show("New transaction started.")
        LoadData()
    End Sub

    Private Sub PRODUCT_LOOK_UP_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Using con As New SqlConnection("Data Source=MS-SOPHIE;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                Dim cmd As New SqlCommand("DELETE FROM gshock.dbo.lookup", con)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error resetting cart: " & ex.Message)
        End Try
    End Sub

    Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBox5.Text = row.Cells("id").Value.ToString()
            TextBox6.Text = row.Cells("productname").Value.ToString()
            TextBox7.Text = row.Cells("series").Value.ToString()
            TextBox8.Text = row.Cells("price").Value.ToString()
            TextBox1.Text = row.Cells("quantity").Value.ToString()
        End If
    End Sub

    Private Sub MP_Click(sender As Object, e As EventArgs)
        Me.Show()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub UpdateCart(con As SqlConnection, productId As String, productName As String, series As String, quantity As Integer, totalPrice As Decimal)
        Try
            Using cmd As New SqlCommand("SELECT quantity, price FROM gshock.dbo.lookup WHERE id = @id", con)
                cmd.Parameters.AddWithValue("@id", productId)

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim existingQuantity As Integer = Convert.ToInt32(reader("quantity"))
                        Dim existingPrice As Decimal = Convert.ToDecimal(reader("price"))
                        reader.Close()

                        Dim newQuantity As Integer = existingQuantity + quantity
                        Dim newTotalPrice As Decimal = existingPrice + totalPrice

                        Using updateCmd As New SqlCommand("UPDATE gshock.dbo.lookup SET quantity = @newQuantity, price = @newPrice, total = @newTotal, [date] = @date WHERE id = @id", con)
                            updateCmd.Parameters.AddWithValue("@newQuantity", newQuantity)
                            updateCmd.Parameters.AddWithValue("@newPrice", newTotalPrice)
                            updateCmd.Parameters.AddWithValue("@newTotal", newTotalPrice) ' ✅ update total column
                            updateCmd.Parameters.AddWithValue("@date", DateTime.Now)
                            updateCmd.Parameters.AddWithValue("@id", productId)
                            updateCmd.ExecuteNonQuery()
                        End Using
                    Else
                        reader.Close()
                        Using insertCmd As New SqlCommand("INSERT INTO gshock.dbo.lookup (id, productname, quantity, price, total, series, [date]) VALUES (@id, @productname, @quantity, @price, @total, @series, @date)", con)
                            insertCmd.Parameters.AddWithValue("@id", productId)
                            insertCmd.Parameters.AddWithValue("@productname", productName)
                            insertCmd.Parameters.AddWithValue("@quantity", quantity)
                            insertCmd.Parameters.AddWithValue("@price", totalPrice)
                            insertCmd.Parameters.AddWithValue("@total", totalPrice) ' ✅ insert into total column
                            insertCmd.Parameters.AddWithValue("@series", series)
                            insertCmd.Parameters.AddWithValue("@date", DateTime.Now)
                            insertCmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating cart: " & ex.Message)
        End Try
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PAYMENT.Show()
    End Sub

    ' ✅ Search functionality using TextBox9 (product ID input)
    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        Dim productId As String = TextBox9.Text.Trim()

        If productId = "" Then
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox8.Clear()
            Return
        End If

        Try
            Using con As New SqlConnection("Data Source=MS-SOPHIE;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                con.Open()

                Dim cmd As New SqlCommand("SELECT id, productname, series, price FROM gshock.dbo.products WHERE id = @id", con)
                cmd.Parameters.AddWithValue("@id", productId)

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        TextBox5.Text = reader("id").ToString()
                        TextBox6.Text = reader("productname").ToString()
                        TextBox7.Text = reader("series").ToString()
                        TextBox8.Text = Convert.ToDecimal(reader("price")).ToString("F2")
                    Else
                        TextBox5.Clear()
                        TextBox6.Clear()
                        TextBox7.Clear()
                        TextBox8.Clear()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error searching product: " & ex.Message)
        End Try
    End Sub
End Class
