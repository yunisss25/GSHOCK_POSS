Imports System.Data.SqlClient
Imports System.IO

Public Class MANAGER
    Dim imageBytes As Byte()
    Dim imageFilePath As String

    Private Sub MANAGER_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbSeries.Items.AddRange(New String() {"women", "men", "kids"})
        cmbSeries.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub btnSelectImage_Click(sender As Object, e As EventArgs) Handles btnSelectImage.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
        openFileDialog.Title = "Select an Image"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim imagePath As String = openFileDialog.FileName
            imageBytes = File.ReadAllBytes(imagePath)

            Dim uniqueImageName As String = Guid.NewGuid().ToString() & Path.GetExtension(imagePath)
            imageFilePath = Path.Combine("C:\Users\EUNICE RA\Pictures", uniqueImageName)

            File.Copy(imagePath, imageFilePath, True)

            picProductImage.SizeMode = PictureBoxSizeMode.Zoom
            picProductImage.Image = Image.FromFile(imagePath)
        End If
    End Sub

    Private Sub btnInsertProduct_Click(sender As Object, e As EventArgs) Handles btnInsertProduct.Click
        ' Validate fields
        If String.IsNullOrWhiteSpace(txtId.Text) OrElse
           String.IsNullOrWhiteSpace(txtProductName.Text) OrElse
           cmbSeries.SelectedItem Is Nothing OrElse
           String.IsNullOrWhiteSpace(txtPrice.Text) OrElse
           String.IsNullOrWhiteSpace(txtQuantity.Text) OrElse
           imageBytes Is Nothing Then
            MessageBox.Show("Please fill in all fields and select an image.")
            Return
        End If

        ' Validate and parse price
        Dim priceValue As Decimal
        If Not Decimal.TryParse(txtPrice.Text, priceValue) Then
            MessageBox.Show("Invalid price format. Use numbers like 99.99.")
            Return
        End If

        ' Validate and parse quantity
        Dim quantityValue As Integer
        If Not Integer.TryParse(txtQuantity.Text, quantityValue) Then
            MessageBox.Show("Invalid quantity. Use whole numbers only.")
            Return
        End If

        Try
            Dim connectionString As String = "Data Source=172.20.10.2;Initial Catalog=gshock;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            Dim query As String = "
                IF EXISTS (SELECT 1 FROM products WHERE id = @id)
                    UPDATE products 
                    SET productname = @productname, 
                        series = @series, 
                        price = @price, 
                        quantity = @quantity, 
                        image = @image
                    WHERE id = @id
                ELSE
                    INSERT INTO products (id, productname, series, price, quantity, image)
                    VALUES (@id, @productname, @series, @price, @quantity, @image)
            "

            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@id", txtId.Text)
                    command.Parameters.AddWithValue("@productname", txtProductName.Text)
                    command.Parameters.AddWithValue("@series", cmbSeries.SelectedItem.ToString())
                    command.Parameters.AddWithValue("@price", priceValue)
                    command.Parameters.AddWithValue("@quantity", quantityValue)
                    command.Parameters.AddWithValue("@image", imageFilePath)

                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Product saved successfully (Inserted or Updated).")

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' Placeholder
    End Sub
End Class