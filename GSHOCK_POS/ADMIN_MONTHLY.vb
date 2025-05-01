Imports Microsoft.Data.SqlClient
Imports Dapper

Public Class ADMIN_MONTHLY

    Private Sub DAILY_Click(sender As Object, e As EventArgs) Handles DAILY.Click
        ADMIN_DAILY.Show()
    End Sub

    Private Sub WEEKLY_Click(sender As Object, e As EventArgs) Handles WEEKLY.Click
        ADMIN_WEEKLY.Show()
    End Sub

    Private Sub MONTHLY_Click(sender As Object, e As EventArgs) Handles MONTHLY.Click
        Me.Show()
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

    Public Function GetSalesForMonth(connectionString As String, monthStart As Date, monthEnd As Date) As List(Of SaleRecord)
        Using conn As New SqlConnection(My.Settings.ConStr)
            conn.Open()
            Dim sql = "
            SELECT 
                CAST(SaleDate AS DATE) AS SaleDate, 
                SUM(Amount) AS TotalAmount
            FROM SalesReport
            WHERE SaleDate BETWEEN @StartDate AND @EndDate
            GROUP BY CAST(SaleDate AS DATE)
            ORDER BY SaleDate"
            Return conn.Query(Of SaleRecord)(sql, New With {.StartDate = monthStart, .EndDate = monthEnd}).ToList()
        End Using
    End Function

End Class
