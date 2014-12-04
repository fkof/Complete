Public Class Form1

    Private Sub btnprocess_Click(sender As System.Object, e As System.EventArgs) Handles btnprocess.Click
        Dim Cnn As New SqlClient.SqlConnection
        Cnn.ConnectionString = String.Format("Server={0};Database={1}; User Id={2}; Password={3};", txtServidor.Text, txtbd.Text, txtusuario.Text, txtpassword.Text)
        Try
            Cnn.Open()
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
        

    End Sub
End Class
