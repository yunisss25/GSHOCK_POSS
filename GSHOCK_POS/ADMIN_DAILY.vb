Imports Microsoft.Data.SqlClient
Imports Dapper
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ADMIN_DAILY

    Private Sub WEEKLY_Click(sender As Object, e As EventArgs) Handles WEEKLY.Click
        ADMIN_WEEKLY.ShowDialog()
    End Sub

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

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        LoadData()
    End Sub

    Public Function GetHourlySalesForDay(targetDate As Date) As List(Of HourlySales)
        Dim sql As String = "
        WITH Hours AS (
            SELECT TOP 24 
                RIGHT('0' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1 AS VARCHAR), 2) + ':00' AS HourSlot
            FROM master.dbo.spt_values
        )
        SELECT 
            h.HourSlot,
            ISNULL(SUM(s.Amount), 0) AS TotalSales
        FROM Hours h
        LEFT JOIN SalesReport s 
            ON DATEPART(HOUR, s.SaleDate) = CAST(LEFT(h.HourSlot, 2) AS INT)
            AND CAST(s.SaleDate AS DATE) = @SaleDate
        GROUP BY h.HourSlot
        ORDER BY h.HourSlot;"

        Using con As New SqlConnection(My.Settings.ConStr)
            Return con.Query(Of HourlySales)(sql, New With {.SaleDate = targetDate}).ToList()
        End Using
    End Function


    Private Sub LoadHourlySalesChart(saleDate As Date)
        ChartSales.Series.Clear()
        ChartSales.Titles.Clear()
        ChartSales.Titles.Add("Hourly Sales for " & saleDate.ToShortDateString())

        Dim series = ChartSales.Series.Add("Hourly Sales")
        series.ChartType = DataVisualization.Charting.SeriesChartType.Column
        series.IsValueShownAsLabel = True
        series.XValueType = DataVisualization.Charting.ChartValueType.String

        Dim salesData = GetHourlySalesForDay(saleDate)

        For Each sale In salesData
            series.Points.AddXY(sale.HourSlot, sale.TotalSales)
        Next

        With ChartSales.ChartAreas(0)
            .AxisX.Title = "Hour"
            .AxisX.Interval = 1
            .AxisX.LabelStyle.Angle = -90
            .AxisX.MajorGrid.Enabled = False

            .AxisY.Title = "Total Sales"
            .AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot

            .AxisX.LabelStyle.Font = New Font("Segoe UI", 8)
            .AxisY.LabelStyle.Font = New Font("Segoe UI", 8)
            .InnerPlotPosition = New ElementPosition(10, 10, 80, 80)
            .Position = New ElementPosition(10, 10, 85, 80)
            .RecalculateAxesScale()
        End With
    End Sub


    Private Sub LoadData()
        Try
            Dim selectedDate As Date = dtpSaleDate.Value.Date
            LoadHourlySalesChart(selectedDate)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnLoadSales_Click(sender As Object, e As EventArgs) Handles btnLoadSales.Click
        LoadData()
    End Sub


    Private Sub ADMIN_DAILY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpSaleDate.Value = Date.Today
        LoadData()
    End Sub
End Class
