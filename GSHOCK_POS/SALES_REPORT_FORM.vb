Imports System.Runtime.InteropServices
Imports Microsoft.Data.SqlClient
Imports Dapper

Public Class SALES_REPORT_FORM

    Private currentPage As Integer = 1
    Private pageSize As Integer = 20
    Private totalPages As Integer = 0
    Private overallTotalSales As Decimal = 0

    Private currentPageGroup As Integer = 1
    Private Const pagesPerGroup As Integer = 5



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

    Public Function GetSalesReportByDateAndTimePaged(
    startDate As Date,
    endDate As Date,
    fromTime As TimeSpan,
    toTime As TimeSpan,
    pageNumber As Integer,
    pageSize As Integer) As List(Of SalesReportItem)

        Dim offset As Integer = (pageNumber - 1) * pageSize

        Dim sql As String = "
        SELECT 
            Id,
            SaleDate,
            Amount
        FROM SalesReport
        WHERE 
            SaleDate IS NOT NULL AND
            CAST(SaleDate AS DATE) BETWEEN @StartDate AND @EndDate AND
            (
                (@FromTime <= @ToTime AND CAST(SaleDate AS TIME) BETWEEN @FromTime AND @ToTime)
                OR
                (@FromTime > @ToTime AND (
                    CAST(SaleDate AS TIME) >= @FromTime OR
                    CAST(SaleDate AS TIME) <= @ToTime
                ))
            )
        ORDER BY SaleDate
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
    "

        Using con As New SqlConnection(My.Settings.ConStr)
            Return con.Query(Of SalesReportItem)(sql, New With {
            .StartDate = startDate,
            .EndDate = endDate,
            .FromTime = fromTime,
            .ToTime = toTime,
            .Offset = offset,
            .PageSize = pageSize
        }).ToList()
        End Using
    End Function


    Private Sub LoadSalesPage()
        Try

            DataGridView1.Columns.Clear()
            DataGridView1.AutoGenerateColumns = False

            Dim colId As New DataGridViewTextBoxColumn()
            colId.HeaderText = "ID"
            colId.DataPropertyName = "Id"
            colId.Visible = False
            DataGridView1.Columns.Add(colId)

            Dim colDate As New DataGridViewTextBoxColumn()
            colDate.HeaderText = "Sale Date"
            colDate.DataPropertyName = "SaleDate"
            DataGridView1.Columns.Add(colDate)

            Dim colAmount As New DataGridViewTextBoxColumn()
            colAmount.HeaderText = "Amount"
            colAmount.DataPropertyName = "Amount"
            DataGridView1.Columns.Add(colAmount)


            Dim startDate As Date = dtpDateFrom.Value.Date
            Dim endDate As Date = dtpDateTo.Value.Date
            Dim fromTime As TimeSpan = dtpTimeFrom.Value.TimeOfDay
            Dim toTime As TimeSpan = dtpTimeTo.Value.TimeOfDay

            Dim sales = GetSalesReportByDateAndTimePaged(startDate, endDate, fromTime, toTime, currentPage, pageSize)

            If sales.Count = 0 AndAlso currentPage > 1 Then
                currentPage -= 1
                LoadSalesPage()
                Return
            End If

            DataGridView1.DataSource = sales

            Dim pageSales = sales.Sum(Function(s) s.Amount)
            lblTotalSales.Text = $"Page {currentPage} Sales: ₱{pageSales:N2}    |    Overall: ₱{overallTotalSales:N2}"

            lblTotalSales.ForeColor = If(pageSales < 0, Color.Red, Color.Black)


        Catch ex As Exception
            MessageBox.Show("Error loading report: " & ex.Message, "Sales Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetSalesReportCount(
    startDate As Date,
    endDate As Date,
    fromTime As TimeSpan,
    toTime As TimeSpan) As Integer

        Dim sql As String = "
        SELECT COUNT(*) 
        FROM SalesReport
        WHERE 
            SaleDate IS NOT NULL AND
            CAST(SaleDate AS DATE) BETWEEN @StartDate AND @EndDate AND
            (
                (@FromTime <= @ToTime AND CAST(SaleDate AS TIME) BETWEEN @FromTime AND @ToTime)
                OR
                (@FromTime > @ToTime AND (
                    CAST(SaleDate AS TIME) >= @FromTime OR
                    CAST(SaleDate AS TIME) <= @ToTime
                ))
            );
    "

        Using con As New SqlConnection(My.Settings.ConStr)
            Return con.ExecuteScalar(Of Integer)(sql, New With {
            .StartDate = startDate,
            .EndDate = endDate,
            .FromTime = fromTime,
            .ToTime = toTime
        })
        End Using
    End Function


    Private Sub ShowReportButton_Click(sender As Object, e As EventArgs) Handles ShowReportButton.Click
        Try
            currentPage = 1
            flpPages.Controls.Clear()
            DataGridView1.DataSource = Nothing
            lblTotalSales.Text = ""
            overallTotalSales = 0
            currentPageGroup = 1


            flpPages.AutoScrollPosition = New Point(0, 0)

            Dim startDate As Date = dtpDateFrom.Value.Date
            Dim endDate As Date = dtpDateTo.Value.Date
            Dim fromTime As TimeSpan = dtpTimeFrom.Value.TimeOfDay
            Dim toTime As TimeSpan = dtpTimeTo.Value.TimeOfDay

            If startDate > endDate Then
                MessageBox.Show("'From' date must be earlier than or equal to 'To' date.", "Invalid Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If fromTime >= toTime Then
                MessageBox.Show("'From' time must be earlier than 'To' time.", "Invalid Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim totalRecords As Integer = GetSalesReportCount(startDate, endDate, fromTime, toTime)
            totalPages = If(totalRecords = 0, 1, CInt(Math.Ceiling(totalRecords / pageSize)))

            overallTotalSales = GetTotalSalesAmount(startDate, endDate, fromTime, toTime)

            GeneratePageButtons()
            LoadSalesPage()

        Catch ex As Exception
            MessageBox.Show("Error loading report: " & ex.Message, "Sales Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub GeneratePageButtons()
        flpPages.Controls.Clear()

        Dim startPage As Integer = (currentPageGroup - 1) * pagesPerGroup + 1
        Dim endPage As Integer = Math.Min(startPage + pagesPerGroup - 1, totalPages)

        If currentPageGroup > 1 Then
            Dim btnPrev As New Button()
            btnPrev.Text = "<<"
            btnPrev.Width = 40
            btnPrev.Height = 30
            btnPrev.Tag = "prev"
            AddHandler btnPrev.Click, AddressOf ArrowButton_Click
            flpPages.Controls.Add(btnPrev)
        End If

        For i As Integer = startPage To endPage
            Dim btn As New Button()
            btn.Text = i.ToString()
            btn.Width = 40
            btn.Height = 30
            btn.Margin = New Padding(2)
            btn.Tag = i
            If i = currentPage Then
                btn.BackColor = Color.LightBlue
            Else
                btn.BackColor = SystemColors.Control
            End If
            AddHandler btn.Click, AddressOf PageButton_Click
            flpPages.Controls.Add(btn)
        Next

        If endPage < totalPages Then
            Dim btnNext As New Button()
            btnNext.Text = ">>"
            btnNext.Width = 40
            btnNext.Height = 30
            btnNext.Tag = "next"
            AddHandler btnNext.Click, AddressOf ArrowButton_Click
            flpPages.Controls.Add(btnNext)
        End If
    End Sub

    Private Sub ArrowButton_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        If btn.Tag.ToString() = "prev" AndAlso currentPageGroup > 1 Then
            currentPageGroup -= 1
        ElseIf btn.Tag.ToString() = "next" AndAlso (currentPageGroup * pagesPerGroup) < totalPages Then
            currentPageGroup += 1
        End If

        currentPage = (currentPageGroup - 1) * pagesPerGroup + 1

        LoadSalesPage()
        GeneratePageButtons()
    End Sub

    Private Sub PageButton_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim selectedPage As Integer = CInt(btn.Tag)

        If selectedPage <> currentPage Then
            currentPage = selectedPage
            LoadSalesPage()
            GeneratePageButtons()
        End If
    End Sub

    Public Function GetTotalSalesAmount(
     startDate As Date,
     endDate As Date,
     fromTime As TimeSpan,
     toTime As TimeSpan) As Decimal

        Dim sql As String = "
        SELECT ISNULL(SUM(Amount), 0)
        FROM SalesReport
        WHERE 
            SaleDate IS NOT NULL AND
            CAST(SaleDate AS DATE) BETWEEN @StartDate AND @EndDate AND
            (
                (@FromTime <= @ToTime AND CAST(SaleDate AS TIME) BETWEEN @FromTime AND @ToTime)
                OR
                (@FromTime > @ToTime AND (
                    CAST(SaleDate AS TIME) >= @FromTime OR
                    CAST(SaleDate AS TIME) <= @ToTime
                ))
            );
    "

        Using con As New SqlConnection(My.Settings.ConStr)
            Return con.ExecuteScalar(Of Decimal)(sql, New With {
                .StartDate = startDate,
                .EndDate = endDate,
                .FromTime = fromTime,
                .ToTime = toTime
            })
        End Using
    End Function




End Class