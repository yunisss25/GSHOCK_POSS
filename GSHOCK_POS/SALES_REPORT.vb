Imports System.Runtime.InteropServices
Imports Microsoft.Data.SqlClient
Imports Dapper

Public Class SALES_REPORT

    <DllImport("user32.dll")>
    Public Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As Integer, lParam As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = 2

    Private Sub SALES_REPORT_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
        End If
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Me.Dispose()
    End Sub

    Private Sub MaximizedButton_Click(sender As Object, e As EventArgs) Handles MaximizedButton.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Public Function GetSalesReportByDateAndTime(startDate As Date, endDate As Date, fromTime As TimeSpan, toTime As TimeSpan) As List(Of SalesReportItem)
        Dim sql As String = "
        SELECT 
            Id,
            SaleDate,
            Amount
        FROM SalesReport
        WHERE 
            CAST(SaleDate AS DATE) BETWEEN @StartDate AND @EndDate
            AND CAST(SaleDate AS TIME) >= @FromTime
            AND CAST(SaleDate AS TIME) <= @ToTime
        ORDER BY SaleDate;
    "

        Using con As New SqlConnection(My.Settings.ConStr)
            Return con.Query(Of SalesReportItem)(sql, New With {
            .StartDate = startDate.Date,
            .EndDate = endDate.Date,
            .FromTime = fromTime,
            .ToTime = toTime
        }).ToList()
        End Using
    End Function



    Private Sub ShowReportButton_Click(sender As Object, e As EventArgs) Handles ShowReportButton.Click
        Try

            Dim startDate As Date = dtpDateFrom.Value.Date
            Dim endDate As Date = dtpDateTo.Value.Date

            Dim fromTime As TimeSpan = dtpTimeFrom.Value.TimeOfDay
            Dim toTime As TimeSpan = dtpTimeTo.Value.TimeOfDay

            If startDate > endDate Then
                MessageBox.Show("'From' date must be earlier than or equal to 'To' date.")
                Return
            End If

            If fromTime >= toTime Then
                MessageBox.Show("'From' time must be earlier than 'To' time.")
                Return
            End If

            Dim sales = GetSalesReportByDateAndTime(startDate, endDate, fromTime, toTime)



            If Sales.Count = 0 Then
                MessageBox.Show("No sales found for the selected date and time range.",
                            "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DataGridView1.DataSource = Nothing
                lblTotalSales.Text = "Total Sales: ₱0.00"
                Return
            End If

            DataGridView1.DataSource = Sales

            Dim totalSales = Sales.Sum(Function(s) s.Amount)
            lblTotalSales.Text = "Total Sales: ₱" & totalSales.ToString("N2")

            If totalSales < 0 Then
                lblTotalSales.ForeColor = Color.Red
            Else
                lblTotalSales.ForeColor = Color.Black
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading report: " & ex.Message, "Sales Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class