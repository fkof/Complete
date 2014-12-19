Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1

    Private Sub btnprocess_Click(sender As System.Object, e As System.EventArgs) Handles btnprocess.Click
        RichTextBox1.Text = String.Empty
        DataGridView1.DataSource = Nothing

        Dim Cnn As New SqlClient.SqlConnection
        Cnn.ConnectionString = String.Format("Server={0};Database={1}; User Id={2}; Password={3};",
                                             txtServidor.Text, txtbd.Text, txtusuario.Text, txtpassword.Text)
        Try
            Cnn.Open()
            MsgBox(String.Format("{0:MM/dd/yyyy}", CDate(dtfecha.Text)))
            Dim consulta As New StringBuilder
            consulta.AppendLine("SELECT DISTINCT")
            consulta.AppendLine("V.ChildItem")
            consulta.AppendLine("FROM")
            consulta.AppendLine("tbl_Changeset CS")
            consulta.AppendLine("INNER JOIN tbl_Identity I ON I.IdentityID = CS.OwnerID")
            consulta.AppendLine("INNER JOIN tbl_Version V ON V.VersionFrom = CS.ChangesetID")
            consulta.AppendLine("INNER JOIN Constants C ON C.TeamFoundationId=I.TeamFoundationId")
            consulta.AppendLine("WHERE ISNULL(CS.Comment,'') <> ''")
            consulta.AppendLine("AND v.ParentPath like '$\SLAMNET\SLAMNet\SQL>MANAGER\SP>SlamNet>Impo\%'")
            consulta.AppendLine("AND CS.CreationDate BETWEEN '" + String.Format("{0:MM/dd/yyyy}", CDate(dtfecha.Text)) + "'")
            consulta.AppendLine("AND GETDATE()")



            Dim datos As New Data.DataTable
            Dim da As New SqlClient.SqlDataAdapter(consulta.ToString(), Cnn)
            da.Fill(datos)
            Cnn.Close()



            For Each dr As Data.DataRow In datos.Rows
                Dim archivo As String = dr(0)
                archivo = archivo.Replace(">", "_")
                archivo = archivo.Replace("\", "")
                If File.Exists("C:\Users\ahernandez\Documents\Visual Studio 2010\Projects\SLAMNet\SQL_MANAGER\SP_SlamNet_Impo\" + archivo) Then


                    Dim lector As New StreamReader("C:\Users\ahernandez\Documents\Visual Studio 2010\Projects\SLAMNet\SQL_MANAGER\SP_SlamNet_Impo\" + archivo)
                    'While lector.Peek() <> -1
                    '    Dim linea As String = lector.ReadLine()
                    '    ' Si no está vacía, añadirla al control
                    '    ' Si está vacía, continuar el bucle
                    '    If String.IsNullOrEmpty(linea) Then
                    '        Continue While
                    '    End If
                    RichTextBox1.Text = RichTextBox1.Text + lector.ReadToEnd.ToString() + vbCrLf
                    '    'Aqui ya haces segun lo que deseas con la variable "linea"
                    '    'tu modificas a tu necesidad 
                    'End While
                    ' Cerrar el fichero
                    Dim separador As String = "--**********************************************************************************"
                    RichTextBox1.Text = RichTextBox1.Text + separador + vbCrLf
                    lector.Close()
                Else
                    Label6.Text = Label6.Text + " " + archivo
                End If
            Next

            DataGridView1.DataSource = datos
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try


    End Sub

    Private Sub txtServidor_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtServidor.TextChanged

    End Sub

    Private Sub Buscar_Coincidencia( _
      ByVal pattern As String, _
      ByVal RichTextBox As RichTextBox, _
      ByVal cColor As Color, _
      ByVal BackColor As Color)


        Dim Resultados As MatchCollection
        Dim Palabra As Match

        Try
            ' PAsar el pattern e indicar que ignore las mayúsculas y minúsculas al mosmento de buscar  
            Dim obj_Expresion As New Regex(pattern.ToString, RegexOptions.IgnoreCase)

            ' Ejecutar el método Matches para buscar la cadena en el texto del control  
            ' y retornar un MatchCollection con los resultados  
            Resultados = obj_Expresion.Matches(RichTextBox.Text)

            ' quitar el coloreado anterior  
            With RichTextBox
                .SelectAll()
                .SelectionColor = Color.Black
            End With

            ' Si se encontraron coincidencias recorre las colección    
            For Each Palabra In Resultados
                With RichTextBox
                    .SelectionStart = Palabra.Index ' comienzo de la selección  
                    .SelectionLength = Palabra.Length ' longitud de la cadena a seleccionar  
                    .SelectionColor = cColor ' color de la selección  
                    .SelectionBackColor = BackColor
                    .Select(Palabra.Index, Palabra.Index)
                    Debug.Print(Palabra.Value)

                End With
            Next Palabra

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim texto As String
        texto = DataGridView1.Rows(e.RowIndex).Cells(0).Value()
        texto = texto.Replace(">", "_")
        texto = texto.Replace("\", "")
        texto = texto.Replace(".sql", "")

        '    Buscar_Coincidencia(texto, RichTextBox1, Color.Blue, Color.Yellow)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim saveFile1 As New SaveFileDialog()

        ' Initialize the SaveFileDialog to specify the RTF extension for the file.
        saveFile1.DefaultExt = "*.sql"
        saveFile1.Filter = "sql|*.sql"

        ' Determine if the user selected a file name from the saveFileDialog. 
        If (saveFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (saveFile1.FileName.Length) > 0 Then

            ' Save the contents of the RichTextBox into the file.
            RichTextBox1.SaveFile(saveFile1.FileName, _
                RichTextBoxStreamType.PlainText)
        End If
    End Sub
End Class
