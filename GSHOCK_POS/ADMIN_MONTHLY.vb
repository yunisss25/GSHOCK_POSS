Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ADMIN_MONTHLY

    ' SQL Connection String
    Dim conn As New SqlConnection("Data Source=172.20.10.2;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")

    Private Sub ADMIN_MONTHLY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadMonthlySales()
    End Sub

    ' ======== CHART: Monthly Sales ========
    Private Sub LoadMonthlySales()
        Dim monthlySales As Decimal = 0
        Dim cmdText As String = "SELECT ISNULL(SUM(total), 0) FROM gshock.dbo.lookup " &
                               "WHERE [date] >= DATEADD(month, -1, CAST(GETDATE() AS DATE)) " &
                               "AND [date] <= GETDATE()"

        Try
            conn.Open()
            Dim cmd As New SqlCommand(cmdText, conn)
            monthlySales = Convert.ToDecimal(cmd.ExecuteScalar())

            ' Use actual chart name (e.g., Chart1 if you only have one)
            Chart1.Series.Clear()
            Dim series As New Series("MONTHLY SALES")
            series.ChartType = SeriesChartType.Column
            series.Points.AddXY("LAST 30 DAYS", monthlySales)
            Chart1.Series.Add(series)

            ' Formatting
            series.IsValueShownAsLabel = True
            series.LabelFormat = "₱#,##0.00"
            Chart1.Titles.Clear()
            Chart1.Titles.Add("Monthly Sales")
            Chart1.ChartAreas(0).AxisY.LabelStyle.Format = "₱#,##0"

        Catch ex As Exception
            MessageBox.Show("Error retrieving monthly sales: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub DAILY_Click(sender As Object, e As EventArgs) Handles DAILY.Click
        ADMIN_DAILY.Show()
        Me.Hide()
    End Sub

    Private Sub WEEKLY_Click(sender As Object, e As EventArgs) Handles WEEKLY.Click
        ADMIN_WEEKLY.Show()
        Me.Hide()
    End Sub

    Private Sub MONTHLY_Click(sender As Object, e As EventArgs) Handles MONTHLY.Click
        ' Current page - do nothing or refresh
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ADMIN_C1.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ADMIN_C2.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ADMIN_C3.Show()
        Me.Hide()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub
End Class
