Imports System.Runtime.InteropServices

Public Class ADMIN_C2

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function ReleaseCapture() As Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = &H2

    Private Sub MP_Click(sender As Object, e As EventArgs) Handles MP.Click
        ADMIN_SALES_OVERVIEW.Show()
    End Sub

    Private Sub ADMIN_C2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ADMIN_C2_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Me.Handle, WM_NCLBUTTONDOWN, CType(HTCAPTION, IntPtr), IntPtr.Zero)
        End If
    End Sub
End Class