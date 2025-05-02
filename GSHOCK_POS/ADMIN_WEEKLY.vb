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

    ' SQL Connection String
    Dim conn As New SqlConnection("Data Source=172.20.10.2;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")

    Private Sub ADMIN_WEEKLY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadWeeklySales()
    End Sub

    ' ======== CHART: Weekly Sales ========
    Private Sub LoadWeeklySales()
        Dim weeklySales As Decimal = 0
        Dim cmdText As String = "SELECT ISNULL(SUM(total), 0) FROM gshock.dbo.lookup " &
                               "WHERE [date] >= DATEADD(day, -7, CAST(GETDATE() AS DATE)) " &
                               "AND [date] <= GETDATE()"

        Try
            conn.Open()
            Dim cmd As New SqlCommand(cmdText, conn)
            weeklySales = Convert.ToDecimal(cmd.ExecuteScalar())

            ' Update this line to use your actual chart name, assuming it's Chart1
            Chart1.Series.Clear()
            Dim series As New Series("WEEKLY SALES")
            series.ChartType = SeriesChartType.Column
            series.Points.AddXY("LAST 7 DAYS", weeklySales)
            Chart1.Series.Add(series)

            ' Formatting
            series.IsValueShownAsLabel = True
            series.LabelFormat = "₱#,##0.00"
            Chart1.Titles.Clear()
            Chart1.Titles.Add("Weekly Sales")
            Chart1.ChartAreas(0).AxisY.LabelStyle.Format = "₱#,##0"

        Catch ex As Exception
            MessageBox.Show("Error retrieving weekly sales: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    ' ======== Close Button ========
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    ' ======== Product Lookup Button ========
    Private Sub MP_Click(sender As Object, e As EventArgs) 
        PRODUCT_LOOK_UP.Show()
        Me.Hide()
    End Sub

    ' ======== Refresh Button ========
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadWeeklySales()
    End Sub

    Private Sub DAILY_Click(sender As Object, e As EventArgs) Handles DAILY.Click
        ADMIN_DAILY.Show()
        Me.Hide()
    End Sub

    Private Sub MONTHLY_Click(sender As Object, e As EventArgs) Handles MONTHLY.Click
        ADMIN_MONTHLY.Show()
        Me.Hide()
    End Sub

End Class
