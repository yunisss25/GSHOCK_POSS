Imports Microsoft.Data.SqlClient
Imports Dapper
Imports System.Globalization

Public Class ADMIN_WEEKLY

    Private Sub MONTHLY_Click(sender As Object, e As EventArgs) Handles MONTHLY.Click
        ADMIN_MONTHLY.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ADMIN_C1.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ADMIN_C2.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ADMIN_C3.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadData()
    End Sub

    Public Function GetSalesForWeek(connectionString As String, weekStart As Date, weekEnd As Date) As List(Of SaleRecord)
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim sql = "
            SELECT 
                CAST(SaleDate AS DATE) AS SaleDate, 
                SUM(Amount) AS TotalAmount
            FROM SalesReport
            WHERE SaleDate BETWEEN @StartDate AND @EndDate
            GROUP BY CAST(SaleDate AS DATE)
            ORDER BY SaleDate"

            Return conn.Query(Of SaleRecord)(sql, New With {.StartDate = weekStart, .EndDate = weekEnd}).ToList()
        End Using
    End Function

    Private Sub LoadData()

        Try


            Dim selectedDate As Date = DateTimePicker1.Value.Date

            Dim diff As Integer = selectedDate.DayOfWeek
            Dim weekStart As Date = selectedDate.AddDays(-diff)
            Dim weekEnd As Date = weekStart.AddDays(6)

            Dim reportData = GetSalesForWeek(My.Settings.ConStr, weekStart, weekEnd)

            With ChartSales
                .Series.Clear()
                .ChartAreas.Clear()
                .ChartAreas.Add("MainArea")

                .Series.Add("Weekly Sales")
                .Series("Weekly Sales").ChartType = DataVisualization.Charting.SeriesChartType.Column
                .Series("Weekly Sales").Points.Clear()


                For i As Integer = 0 To 6
                    Dim day As Date = weekStart.AddDays(i)

                    Dim sale = reportData.FirstOrDefault(Function(r) r.SaleDate = day)
                    Dim amount = If(sale IsNot Nothing, sale.TotalAmount, 0D)
                    .Series("Weekly Sales").Points.AddXY(day.ToString("ddd dd"), amount)
                Next
            End With

            Label3.Text = "Week of " & weekStart.ToString("MMM dd") & " - " & weekEnd.ToString("MMM dd")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ADMIN_WEEKLY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
End Class
