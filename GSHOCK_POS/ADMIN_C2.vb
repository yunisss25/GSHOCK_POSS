Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class ADMIN_C2

    ' SQL Connection String
    Dim conn As New SqlConnection("Data Source=172.20.10.2;Initial Catalog=gshock;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")

    Private Sub ADMIN_C2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTotalPrice()
        LoadWeeklyTotal()
        LoadMonthlyTotal()
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

    ' ======== CHART 2: Weekly Total ========
    Private Sub LoadWeeklyTotal()
        Chart2.Series.Clear()
        Chart2.Series.Add("Series1")
        Chart2.Series("Series1").ChartType = SeriesChartType.Column

        Try
            conn.Open()

            Dim query As String = "
            SELECT 
                DATEPART(WEEK, [date]) AS WeekNumber, 
                ISNULL(SUM(price), 0) AS TotalPrice
            FROM Sales
            WHERE [date] >= DATEADD(YEAR, DATEDIFF(YEAR, 0, GETDATE()), 0) 
              AND [date] < DATEADD(YEAR, DATEDIFF(YEAR, 0, GETDATE()) + 1, 0)
            GROUP BY DATEPART(WEEK, [date])
            ORDER BY WeekNumber
        "

            Dim cmd As New SqlCommand(query, conn)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim weekNumber As Integer = If(reader("WeekNumber") IsNot DBNull.Value, Convert.ToInt32(reader("WeekNumber")), 0)
                Dim totalPrice As Decimal = If(reader("TotalPrice") IsNot DBNull.Value, Convert.ToDecimal(reader("TotalPrice")), 0D)

                ' Only add if weekNumber > 0
                If weekNumber > 0 Then
                    Chart2.Series("Series1").Points.AddXY("Week " & weekNumber, totalPrice)
                End If
            End While

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading weekly totals: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


    ' ======== CHART 3: Monthly Total ========
    Private Sub LoadMonthlyTotal()
        Chart3.Series.Clear()
        Chart3.Series.Add("Series1")
        Chart3.Series("Series1").ChartType = SeriesChartType.Column

        Try
            conn.Open()

            Dim query As String = "
                SELECT DATEPART(MONTH, [date]) AS MonthNumber, SUM(price) AS TotalPrice
                FROM Sales
                WHERE YEAR([date]) = YEAR(GETDATE())
                GROUP BY DATEPART(MONTH, [date])
                ORDER BY MonthNumber
            "

            Dim cmd As New SqlCommand(query, conn)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim monthNumber As Integer = Convert.ToInt32(reader("MonthNumber"))
                Dim totalPrice As Decimal = Convert.ToDecimal(reader("TotalPrice"))

                ' Display month as "Jan", "Feb", etc.
                Dim monthName As String = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(monthNumber)

                Chart3.Series("Series1").Points.AddXY(monthName, totalPrice)
            End While

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading monthly totals: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' ======== Close Button ========
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' ======== Open PRODUCT LOOK UP ========
    Private Sub MP_Click(sender As Object, e As EventArgs) Handles MP.Click
        PRODUCT_LOOK_UP.Show()
    End Sub

End Class
