Imports System.Data.SqlClient

Public Class PRODUCT_LOOK_UP
    Dim total As Decimal = 0
    Dim con As New SqlConnection("Data Source=")

    Private Sub PRODUCT_LOOK_UP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load Data into Inventory & Cart
        LoadData()

    End Sub

    Private Sub LoadData()
        Try
            con.Open()

            ' Load Inventory Data (DataGridView1)
            Dim inventoryAdapter As New SqlDataAdapter("SELECT * FROM gshock.dbo.product", con)
            Dim inventoryTable As New DataTable()
            inventoryAdapter.Fill(inventoryTable)
            DataGridView1.DataSource = inventoryTable

            ' Load Cart Data (DataGridView2)
            Dim cartAdapter As New SqlDataAdapter("SELECT * FROM gshock.dbo.lookup", con)
            Dim cartTable As New DataTable()
            cartAdapter.Fill(cartTable)
            DataGridView2.DataSource = cartTable
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Dim products As New Dictionary(Of String, Integer)

        ' Read Product Names & Quantities from TextBoxes
        If Not String.IsNullOrWhiteSpace(TextBox5.Text) AndAlso Integer.TryParse(TextBox6.Text, Nothing) Then
            products(TextBox5.Text) = Convert.ToInt32(TextBox6.Text)
        End If
        If Not String.IsNullOrWhiteSpace(TextBox7.Text) AndAlso Integer.TryParse(TextBox8.Text, Nothing) Then
            products(TextBox7.Text) = Convert.ToInt32(TextBox8.Text)
        End If

        ' Process each product
        For Each item In products
            AddToCart(item.Key, item.Value)
        Next

        TextBox2.Text = total.ToString("F2") ' Update total price display

        ' Clear input fields after adding
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()

        LoadData() ' Refresh DataGridViews
    End Sub

    Private Sub AddToCart(productName As String, quantity As Integer)
        If quantity <= 0 Then Exit Sub

        Try
            con.Open()

            ' Fetch Item Details from Inventory
            Dim cmd As New SqlCommand("SELECT id, productname, series, price, quantity, image FROM gshock.dbo.product WHERE productname = @item", con)
            cmd.Parameters.AddWithValue("@item", productName)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim id As Integer = Convert.ToInt32(reader("id"))
                Dim stock As Integer = Convert.ToInt32(reader("quantity"))
                Dim price As Decimal = Convert.ToDecimal(reader("price"))
                reader.Close()

                If stock >= quantity Then
                    Dim totalPrice As Decimal = price * quantity
                    total += totalPrice ' Update total cost

                    ' Deduct from Inventory
                    cmd = New SqlCommand("UPDATE gshock.dbo.product SET quantity = quantity - @quantity WHERE productname = @item", con)
                    cmd.Parameters.AddWithValue("@quantity", quantity)
                    cmd.Parameters.AddWithValue("@item", productName)
                    cmd.ExecuteNonQuery()

                    ' Check if Item Already in Cart
                    cmd = New SqlCommand("SELECT quantity, price FROM gshock.dbo.lookup WHERE productname = @item", con)
                    cmd.Parameters.AddWithValue("@item", productName)
                    reader = cmd.ExecuteReader()

                    If reader.Read() Then
                        ' Update existing cart item
                        Dim existingQuantity As Integer = Convert.ToInt32(reader("quantity"))
                        Dim existingPrice As Decimal = Convert.ToDecimal(reader("price"))
                        reader.Close()

                        cmd = New SqlCommand("UPDATE gshock.dbo.lookup SET quantity = @newQuantity, price = @newPrice WHERE productname = @item", con)
                        cmd.Parameters.AddWithValue("@newQuantity", existingQuantity + quantity)
                        cmd.Parameters.AddWithValue("@newPrice", existingPrice + totalPrice)
                    Else
                        ' Insert new cart item
                        reader.Close()
                        cmd = New SqlCommand("INSERT INTO gshock.dbo.lookup (id, productname, quantity, price) VALUES (@id, @productname, @quantity, @price)", con)
                        cmd.Parameters.AddWithValue("@id", id)
                        cmd.Parameters.AddWithValue("@productname", productName)
                        cmd.Parameters.AddWithValue("@quantity", quantity)
                        cmd.Parameters.AddWithValue("@price", totalPrice)
                    End If

                    cmd.ExecuteNonQuery()
                Else
                    MessageBox.Show("Not enough stock for " & productName)
                End If
            Else
                MessageBox.Show("id '" & productName & "' not found in inventory!")
            End If
        Catch ex As Exception
            MessageBox.Show("Database Error: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim payment As Decimal
        If Decimal.TryParse(TextBox3.Text, payment) Then
            If payment < 0 Then
                MessageBox.Show("Payment cannot be negative!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox3.Text = "" ' Clear the invalid input
            ElseIf payment >= total Then
                TextBox4.Text = (payment - total).ToString("F2") ' Calculate change
                MessageBox.Show("Payment successful! Change: " & TextBox4.Text)
                LoadData()
            Else
                MessageBox.Show("Insufficient payment!")
            End If
        Else
            MessageBox.Show("Invalid payment amount!")
            TextBox3.Text = ""
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = "1"
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        total = 0

        Try
            con.Open()
            Dim cmd As New SqlCommand("DELETE FROM gshock.dbo.lookup", con)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error clearing cart: " & ex.Message)
        Finally
            con.Close()
        End Try

        MessageBox.Show("New transaction started.")
        LoadData()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            con.Open()
            Dim cmd As New SqlCommand("DELETE FROM gshock.dbo.lookup", con)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error resetting inventory and cart: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    ' Restrict Input to Numeric Values Only
    Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox6.KeyPress, TextBox8.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class