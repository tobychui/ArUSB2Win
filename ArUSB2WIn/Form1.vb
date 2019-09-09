Imports System.Net

Public Class Form1
    Dim USBpath As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dialog As New FolderBrowserDialog()
        dialog.RootFolder = Environment.SpecialFolder.Desktop
        dialog.SelectedPath = Application.StartupPath
        dialog.Description = "Select your USB Drive Mount Point"
        If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            USBpath = dialog.SelectedPath
        End If
        TextBox1.Text = USBpath
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Version: " & My.Application.Info.Version.ToString()
        If Not My.Computer.FileSystem.FileExists("fsconv.exe") Then
            MsgBox("Missing fsconv.exe. Downloading from network location.")
            Try
                My.Computer.Network.DownloadFile("https://hkwtc.org/aroz_online/hook/AOS-Mirror/src/SystemAOB/functions/file_system/fsconv.exe", "fsconv.exe")
            Catch ex As Exception
                MessageBox.Show("Unable to download fsconv! Please check your internet connection and try again later.", "Failed to download fsconv", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End Try

            MsgBox("fsconv.exe downloaded.")
        End If
    End Sub

    Private Sub downloadFile(url As String, saveFilename As String)
        Dim remoteUri As String = url
        Dim fileName As String = saveFilename

        Using client As New WebClient()
            client.DownloadFile(remoteUri, fileName)
        End Using
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim filepath As String = TextBox1.Text
        If Not My.Computer.FileSystem.DirectoryExists(filepath) Then
            MessageBox.Show("Given directory not found.", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim paramter As String = "-r"
        If RadioButton2.Checked = True Then
            paramter = "-rf"
        ElseIf RadioButton3.Checked = True Then
            paramter = "-rd"
        End If
        paramter = paramter & " -utf"
        Dim command As String = "-i """ & filepath & """ " & paramter
        startFsconv(command)
        MessageBox.Show("Decode Succeed", "Doned", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub startFsconv(para As String)
        Dim p As New ProcessStartInfo
        p.FileName = Application.StartupPath & "\" & "fsconv.exe"
        p.Arguments = " " & para
        Process.Start(p)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim filepath As String = TextBox1.Text
        If Not My.Computer.FileSystem.DirectoryExists(filepath) Then
            MessageBox.Show("Given directory not found.", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim paramter As String = "-r"
        If RadioButton2.Checked = True Then
            paramter = "-rf"
        ElseIf RadioButton3.Checked = True Then
            paramter = "-rd"
        End If
        paramter = paramter & " -um"
        Dim command As String = "-i """ & filepath & """ " & paramter
        startFsconv(command)
        MessageBox.Show("Encode Succeed", "Doned", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
