Imports Microsoft.VisualBasic.FileIO.FileSystem
Public Class Main
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Setup()

    End Sub

    Private Sub Setup()
        If Not DirectoryExists(mainDir) Then
            CreateDirectory(mainDir)
        End If
        If Not FileExists(optionsLoc) Then
            SaveOptions()
        End If
        If Not DirectoryExists(backupFilesLoc) Then
            CreateDirectory(backupFilesLoc)
        End If
        LoadOptions()
        ckbx_startOnBoot.Checked = startOnBoot
        LoadBackups()
    End Sub

    Private Sub Main_Closing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = Not stopExecution
        Me.Hide()
        mainLogger.Log("Main form hidden by closing")
    End Sub

    Private Sub notifyIcon_main_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles notifyIcon_main.MouseDoubleClick
        Me.Show()
        mainLogger.Log("Main form shown by notifyIcon_main double click")
    End Sub

    Private Sub exit_tool_Click(sender As Object, e As EventArgs) Handles exit_tool.Click
        mainLogger.Log("notifyIcon_main exit clicked")
        Terminate()
    End Sub

    Private Sub startBackup_tool_Click(sender As Object, e As EventArgs) Handles startBackup_tool.Click

    End Sub

    Private Sub ckbx_startOnBoot_CheckedChanged(sender As Object, e As EventArgs) Handles ckbx_startOnBoot.CheckedChanged
        startOnBoot = ckbx_startOnBoot.Checked
        SaveOptions()
    End Sub
End Class
