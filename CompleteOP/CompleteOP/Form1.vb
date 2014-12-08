Imports System.IO

Public Class Form1

    Private Sub btnprocess_Click(sender As System.Object, e As System.EventArgs) Handles btnprocess.Click
        Dim Cnn As New SqlClient.SqlConnection
        Cnn.ConnectionString = String.Format("Server={0};Database={1}; User Id={2}; Password={3};", txtServidor.Text, txtbd.Text, txtusuario.Text, txtpassword.Text)
        Try
            ' Cnn.Open()

            Dim lector As New StreamReader("C:\Users\ahernandez\Documents\Visual Studio 2010\Projects\SLAMNet\SQL_MANAGER\SP_SlamNet_Impo\SLAMNET_SPU_BORRARECIBO.sql")
            While lector.Peek() <> -1
                Dim linea As String = lector.ReadLine()
                ' Si no está vacía, añadirla al control
                ' Si está vacía, continuar el bucle
                If String.IsNullOrEmpty(linea) Then
                    Continue While
                End If
                RichTextBox1.Text = RichTextBox1.Text + linea + vbCrLf
                'Aqui ya haces segun lo que deseas con la variable "linea"
                'tu modificas a tu necesidad 
            End While
            ' Cerrar el fichero
            lector.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try


    End Sub
End Class
