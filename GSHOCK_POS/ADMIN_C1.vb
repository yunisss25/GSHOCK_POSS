﻿Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ADMIN_C1
    ' SQL Connection String
    Dim conn As New SqlConnection("Data Source=172.20.10.2;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")

    Private Sub ADMIN_C1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDailySales()
        LoadWeeklySales()
        LoadMonthlySales()
    End Sub

    ' ======== CHART 1: Daily Sales ========
    Private Sub LoadDailySales()
        Dim dailySales As Decimal = 0
        Dim cmdText As String = "SELECT ISNULL(SUM(total), 0) FROM gshock.dbo.lookup " &
                            "WHERE CAST([date] AS DATE) = CAST(GETDATE() AS DATE)"

        Try
            conn.Open()
            Dim cmd As New SqlCommand(cmdText, conn)
            dailySales = Convert.ToDecimal(cmd.ExecuteScalar())

            Chart1.Series.Clear()
            Dim series As New Series("DAILY SALES")
            series.ChartType = SeriesChartType.Column
            series.Points.AddXY("TODAY", dailySales)
            series.IsValueShownAsLabel = True
            series.LabelFormat = "₱#,##0.00"

            Chart1.Series.Add(series)
            Chart1.Titles.Clear()
            Chart1.Titles.Add("Today's Sales")
            Chart1.ChartAreas(0).AxisY.LabelStyle.Format = "₱#,##0"
            Chart1.Invalidate()

        Catch ex As Exception
            MessageBox.Show("Error retrieving daily sales: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' ======== CHART 2: Weekly Sales ========
    Private Sub LoadWeeklySales()
        Dim weeklySales As Decimal = 0
        Dim cmdText As String = "SELECT ISNULL(SUM(total), 0) FROM gshock.dbo.lookup " &
                            "WHERE [date] >= DATEADD(DAY, -6, CAST(GETDATE() AS DATE)) " &
                            "AND [date] <= GETDATE()"

        Try
            conn.Open()
            Dim cmd As New SqlCommand(cmdText, conn)
            weeklySales = Convert.ToDecimal(cmd.ExecuteScalar())

            Chart2.Series.Clear()
            Dim series As New Series("WEEKLY SALES")
            series.ChartType = SeriesChartType.Column
            series.Points.AddXY("LAST 7 DAYS", weeklySales)
            series.IsValueShownAsLabel = True
            series.LabelFormat = "₱#,##0.00"

            Chart2.Series.Add(series)
            Chart2.Titles.Clear()
            Chart2.Titles.Add("Weekly Sales")
            Chart2.ChartAreas(0).AxisY.LabelStyle.Format = "₱#,##0"
            Chart2.Invalidate()

        Catch ex As Exception
            MessageBox.Show("Error retrieving weekly sales: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' ======== CHART 3: Monthly Sales ========
    Private Sub LoadMonthlySales()
        Dim monthlySales As Decimal = 0
        Dim cmdText As String = "SELECT ISNULL(SUM(total), 0) FROM gshock.dbo.lookup " &
                            "WHERE [date] >= DATEADD(DAY, -29, CAST(GETDATE() AS DATE)) " &
                            "AND [date] <= GETDATE()"

        Try
            conn.Open()
            Dim cmd As New SqlCommand(cmdText, conn)
            monthlySales = Convert.ToDecimal(cmd.ExecuteScalar())

            Chart3.Series.Clear()
            Dim series As New Series("MONTHLY SALES")
            series.ChartType = SeriesChartType.Column
            series.Points.AddXY("LAST 30 DAYS", monthlySales)
            series.IsValueShownAsLabel = True
            series.LabelFormat = "₱#,##0.00"

            Chart3.Series.Add(series)
            Chart3.Titles.Clear()
            Chart3.Titles.Add("Monthly Sales")
            Chart3.ChartAreas(0).AxisY.LabelStyle.Format = "₱#,##0"
            Chart3.Invalidate()

        Catch ex As Exception
            MessageBox.Show("Error retrieving monthly sales: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' ======== Close Button ========
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    ' ======== Product Lookup Button ========
    Private Sub MP_Click(sender As Object, e As EventArgs) Handles MP.Click
        ADMIN_SALES_OVERVIEW.Show()
        Me.Hide()
    End Sub

    ' ======== Refresh Button ========
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadDailySales()
        LoadWeeklySales()
        LoadMonthlySales()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ADMIN_SALES_OVERVIEW.Show()
        Me.Hide()
    End Sub
End Class