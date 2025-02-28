Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ADMIN_C1

    ' SQL Connection String
    Dim conn As New SqlConnection("Data Source=192.168.2.113;Initial Catalog=gshock;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")

    Private Sub ADMIN_C1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTotalPrice()
        LoadWeeklyTotal()
        LoadMonthlyTotal()
    End Sub

    ' ======== CHART 1: Total Price (Today) ========
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

        Chart1.Series.Clear()
        Chart1.Series.Add("DAILY")
        Chart1.Series("DAILY").ChartType = SeriesChartType.Column
        Chart1.Series("DAILY").Points.AddXY("TODAY", totalPrice)
    End Sub

    ' ======== CHART 2: Weekly Total (Today * 7) ========
    Private Sub LoadWeeklyTotal()
        Dim totalPriceToday As Decimal = 0
        Chart2.Series.Clear()
        Dim series As New Series("WEEKLY")
        series.ChartType = SeriesChartType.Column
        Chart2.Series.Add(series)

        Try
            conn.Open()
            Dim cmd As New SqlCommand("SELECT ISNULL(SUM([total]), 0) FROM products WHERE CAST([date] AS DATE) = CAST(GETDATE() AS DATE)", conn)
            Dim result = cmd.ExecuteScalar()

            If result IsNot DBNull.Value Then
                totalPriceToday = Convert.ToDecimal(result)
            End If

            ' Multiply today's total by 7 for weekly estimate
            Dim weeklyTotal As Decimal = totalPriceToday * 7
            series.Points.AddXY("WEEKLY (EST.)", weeklyTotal)

        Catch ex As Exception
            MessageBox.Show("Error calculating weekly total: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


    ' ======== CHART 3: Monthly Totals ========
    Private Sub LoadMonthlyTotal()
        Dim totalPriceToday As Decimal = 0
        Chart3.Series.Clear()
        Dim series As New Series("MONTHLY")
        series.ChartType = SeriesChartType.Column
        Chart3.Series.Add(series)

        Try
            conn.Open()
            Dim cmd As New SqlCommand("SELECT ISNULL(SUM([price]), 0) FROM products WHERE CAST([date] AS DATE) = CAST(GETDATE() AS DATE)", conn)
            Dim result = cmd.ExecuteScalar()

            If result IsNot DBNull.Value Then
                totalPriceToday = Convert.ToDecimal(result)
            End If

            ' Multiply today's total by 30 for monthly estimate
            Dim monthlyEstimate As Decimal = totalPriceToday * 30
            series.Points.AddXY("MONTHLY (EST.)", monthlyEstimate)

        Catch ex As Exception
            MessageBox.Show("Error calculating monthly total: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


    ' ======== Close Button ========
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' ======== Product Lookup Button ========
    Private Sub MP_Click(sender As Object, e As EventArgs) Handles MP.Click
        PRODUCT_LOOK_UP.Show()
        Me.Hide()
    End Sub

    ' ======== Optional Chart Click Handlers ========
    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click
        ' Optional: Implement chart click behavior
    End Sub

    Private Sub Chart2_Click(sender As Object, e As EventArgs) Handles Chart2.Click
        ' Optional: Implement chart click behavior
    End Sub

    Private Sub Chart3_Click(sender As Object, e As EventArgs) Handles Chart3.Click
        ' Optional: Implement chart click behavior
    End Sub

End Class
