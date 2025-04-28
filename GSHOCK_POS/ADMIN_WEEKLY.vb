Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Admin_WEEKLY
    Private Function GetConnectionString() As String
        Return "Server=YourServerName;Database=gshock;Trusted_Connection=True;"
    End Function

    Private Sub Admin_WEEKLY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup chart
        Chart1.Series("Series1").ChartType = SeriesChartType.Column
        Chart1.Series("Series1").Color = Color.RoyalBlue

        Dim area As ChartArea = Chart1.ChartAreas(0)
        area.AxisX.MajorGrid.Enabled = True
        area.AxisX.MajorGrid.LineColor = Color.LightGray
        area.AxisY.MajorGrid.Enabled = True
        area.AxisY.MajorGrid.LineColor = Color.LightGray

        ' Load data
        LoadWeeklySalesChart()
    End Sub

    Private Sub LoadWeeklySalesChart()
        Chart1.Series("Series1").Points.Clear()

        Dim connectionString As String = GetConnectionString()
        Dim query As String = "SELECT 
                               DATEPART(WEEK, SaleDate) AS WeekNumber, 
                               DATEPART(YEAR, SaleDate) AS YearNumber,
                               SUM(TotalAmount) AS WeeklyTotal
                               FROM Sales 
                               WHERE SaleDate >= DATEADD(WEEK, -8, GETDATE())
                               GROUP BY DATEPART(WEEK, SaleDate), DATEPART(YEAR, SaleDate)
                               ORDER BY YearNumber, WeekNumber"

        Using connection As New SqlClient.SqlConnection(connectionString)
            Using command As New SqlClient.SqlCommand(query, connection)
                Try
                    connection.Open()

                    Dim weekData As New List(Of Tuple(Of Integer, Integer, Decimal))() ' Week, Year, Amount

                    Using reader As SqlClient.SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim weekNumber As Integer = reader.GetInt32(0)
                            Dim yearNumber As Integer = reader.GetInt32(1)
                            Dim salesAmount As Decimal = reader.GetDecimal(2)

                            weekData.Add(New Tuple(Of Integer, Integer, Decimal)(weekNumber, yearNumber, salesAmount))
                        End While
                    End Using

                    ' Sort by date (year and week)
                    weekData.Sort(Function(a, b)
                                      If a.Item2 <> b.Item2 Then
                                          Return a.Item2.CompareTo(b.Item2)
                                      Else
                                          Return a.Item1.CompareTo(b.Item1)
                                      End If
                                  End Function)

                    ' Only take most recent 8 weeks
                    Dim recentWeeks = weekData.Skip(Math.Max(0, weekData.Count - 8)).ToList()

                    ' Find max value for scaling
                    Dim maxValue As Decimal = 0
                    For Each week In recentWeeks
                        If week.Item3 > maxValue Then
                            maxValue = week.Item3
                        End If
                    Next

                    ' Add data points
                    For i As Integer = 0 To recentWeeks.Count - 1
                        Chart1.Series("Series1").Points.AddXY(i + 1, recentWeeks(i).Item3)
                    Next

                    ' Set Y-axis maximum
                    Chart1.ChartAreas(0).AxisY.Maximum = Math.Ceiling(maxValue * 1.1 / 10) * 10

                Catch ex As Exception
                    MessageBox.Show("Error loading sales data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    ' Button click event handlers from your UI
    Private Sub btnDaily_Click(sender As Object, e As EventArgs) Handles btnDaily.Click
        Me.Hide()
        ADMIN_DAILY.Show()
    End Sub

    Private Sub btnWeekly_Click(sender As Object, e As EventArgs) Handles btnWeekly.Click
        ' Refresh current form
        LoadWeeklySalesChart()
    End Sub

    Private Sub btnMonthly_Click(sender As Object, e As EventArgs) Handles btnMonthly.Click
        Me.Hide()
        ADMIN_MONTHLY.Show()
    End Sub

    Private Sub btnMainPage_Click(sender As Object, e As EventArgs) Handles btnMainPage.Click
        Me.Hide()
        ADMIN_C1.Show()
    End Sub

    Private Sub btnCashier1_Click(sender As Object, e As EventArgs) Handles btnCashier1.Click
        Me.Hide()
        CASHIER.Show()
    End Sub

    Private Sub btnCashier2_Click(sender As Object, e As EventArgs) Handles btnCashier2.Click
        Me.Hide()
        CASHIER2.Show()
    End Sub

    Private Sub btnCashier3_Click(sender As Object, e As EventArgs) Handles btnCashier3.Click
        Me.Hide()
        CASHIER3.Show()
    End Sub
End Class