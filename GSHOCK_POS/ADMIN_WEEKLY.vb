Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ADMIN_WEEKLY
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
