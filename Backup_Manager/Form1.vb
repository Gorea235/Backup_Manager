Imports Microsoft.VisualBasic.FileIO.FileSystem
Public Class Main
    Private Class queueClass
        Public originalLoc As String
        Public backupLoc As String
        Public pastVersions As Integer

        Public Sub New(ByVal OriginalLocation As String, ByVal BackupLocation As String, ByVal VersionsToKeep As Integer)
            Me.originalLoc = OriginalLocation
            Me.backupLoc = BackupLocation
            Me.pastVersions = VersionsToKeep
        End Sub
    End Class
    Private queued As Dictionary(Of Integer, queueClass) = New Dictionary(Of Integer, queueClass)

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
        Dim seleced As BackupClass = backups(lbx_backups.SelectedValue)
    End Sub

    Private Sub ckbx_startOnBoot_CheckedChanged(sender As Object, e As EventArgs) Handles ckbx_startOnBoot.CheckedChanged
        startOnBoot = ckbx_startOnBoot.Checked
        SaveOptions()
    End Sub

    Private Sub tmr_main_Tick(sender As Object, e As EventArgs) Handles tmr_main.Tick
        For Each v In backups.Values
            If v.MinutePast = Minute(Now) Then
                v.CurrentInterval = v.CurrentInterval + 1
            End If
            If v.CurrentInterval = v.BackupInverval Then
                QueueBackup(v.OriginalLoc, v.BackupLoc, v.PastVersions)
                v.CurrentInterval = 0
            End If
        Next
    End Sub

    Private Sub btn_new_Click(sender As Object, e As EventArgs) Handles btn_new.Click
        BackupAdder.Show()
    End Sub

    Public Sub QueueBackup(ByVal originalLoc As String, ByVal backupLoc As String, ByVal pastVersions As Integer)
        queued.Add(queued.Count, New queueClass(originalLoc, backupLoc, pastVersions))
    End Sub

    Public Sub StartBackups()
        For Each k In queued.Keys
            Dim q As queueClass = queued(k)
            secondThread.RunWorkerAsync(q)
            queued.Remove(k)
        Next
        If queued.Count > 0 Then
            StartBackups()
        End If
    End Sub

    Private Sub secondThread_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles secondThread.DoWork
        Try
            Dim args As queueClass = e.Argument
        Catch ex As Exception
            Throw New ArgumentException("Argument couldn't be converted to queueClass")
        End Try
    End Sub
End Class
