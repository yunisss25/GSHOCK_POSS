Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ADMIN_DAILY

    ' Your connection string (adjust if needed)
    Dim connectionString As String = "Data Source=localhost;Initial Catalog=GSHOCK;Integrated Security=True"

    Private Sub ADMIN_DAILY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSalesData()
    End Sub

    Private Sub LoadSalesData()
        ' Clear old points
        Chart1.Series("Series1").Points.Clear()

        ' SQL query to get the prices
        Dim query As String = "SELECT price FROM gshock.dbo.product"

        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()

                Dim command As New SqlCommand(query, connection)
                Dim reader As SqlDataReader = command.ExecuteReader()

                Dim dayCounter As Integer = 1

                While reader.Read()
                    Dim price As Decimal = reader.GetDecimal(0)

                    ' Add each price to the chart, use dayCounter as X value
                    Chart1.Series("Series1").Points.AddXY(dayCounter, price)

                    dayCounter += 1
                End While

            Catch ex As Exception
                MessageBox.Show("Error loading data: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub DAILY_Click(sender As Object, e As EventArgs) Handles DAILY.Click
        Me.Show()
    End Sub

    Private Sub WEEKLY_Click(sender As Object, e As EventArgs) Handles WEEKLY.Click
        ADMIN_WEEKLY.Show()
    End Sub

    Private Sub MONTHLY_Click(sender As Object, e As EventArgs) Handles MONTHLY.Click
        ADMIN_MONTHLY.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ADMIN_C1.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ADMIN_C2.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ADMIN_C3.Show()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
