Imports System.Data.SqlClient
Module Module1
    Public con As New SqlConnection
    Public cmd As New SqlCommand

    Sub opencon()
        con.ConnectionString = "Data Source=192.168.8.40,1433;Initial Catalog=gshock;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
    End Sub
End Module
