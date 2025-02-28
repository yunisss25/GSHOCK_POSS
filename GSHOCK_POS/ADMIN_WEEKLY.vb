Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ADMIN_WEEKLY

    ' SQL Connection String
    Dim conn As New SqlConnection("Data Source=192.168.2.113;Initial Catalog=gshock;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    Private Sub ADMIN_DAILY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTotalPrice()
    End Sub

    ' ======== CHART 1: Total Price (Overall) ========
    Private Sub LoadTotalPrice()
        Dim totalPrice As Decimal = 0

        Dim cmd As New SqlCommand("SELECT SUM(price) FROM products", conn)

        Try
            conn.Open()
            Dim result = cmd.ExecuteScalar()

            If result IsNot DBNull.Value Then
                totalPrice = Convert.ToDecimal(result)
            End If

        Catch ex As Exception
            MessageBox.Show("Error retrieving total price: " & ex.Message)
        Finally
            conn.Close()
        End Try

        ' Display in Chart1
        Chart1.Series.Clear()
        Chart1.Series.Add("Series1")
        Chart1.Series("Series1").ChartType = SeriesChartType.Column
        Chart1.Series("Series1").Points.AddXY("Total", totalPrice)
    End Sub

    Private Sub DAILY_Click(sender As Object, e As EventArgs) Handles DAILY.Click
        ADMIN_DAILY.Hide()
    End Sub

    Private Sub WEEKLY_Click(sender As Object, e As EventArgs) Handles WEEKLY.Click
        Me.Show()
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

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub


End Class
