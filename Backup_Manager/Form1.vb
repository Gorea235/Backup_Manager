Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports System.IO
Imports IWshRuntimeLibrary
Imports System.Environment
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
        ckbx_showWindow.Checked = showFormOnLaunch
        LoadBackups()
        CheckShortCut()
        mainLogger.Log("Setup complete")
    End Sub

    Private Sub Main_Shown() Handles Me.Shown
        If Not loadingComplete And Not showFormOnLaunch Then
            Me.Hide()
            loadingComplete = True
        Else
            Me.ShowInTaskbar = True
        End If
    End Sub

    Friend Sub CheckShortCut()
        Dim shortLocation As String = GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Microsoft\Windows\Start Menu\Programs\Startup\Backup_Manager.lnk"
        If startOnBoot Then
            If Not FileExists(shortLocation) Then
                Dim WshShort As WshShell = New WshShell
                Dim MainShort As IWshShortcut
                MainShort = CType(WshShort.CreateShortcut(shortLocation), IWshShortcut)
                MainShort.TargetPath = Application.ExecutablePath
                MainShort.IconLocation = Application.ExecutablePath & ", 0"
                MainShort.Save()
            End If
        Else
            If FileExists(shortLocation) Then
                DeleteFile(shortLocation)
            End If
        End If
    End Sub

    Friend Sub LoadBackups()
        backups.Clear()
        For Each f In GetFiles(backupFilesLoc)
            If Microsoft.VisualBasic.Right(f, "4") = ".xml" Then
                xDoc = XDocument.Load(f)
                Dim tmpName As String = xDoc...<Name>.Value
                Dim tmpOriginalLoc As String = xDoc...<OriginalLocation>.Value
                Dim tmpBackupLoc As String = xDoc...<BackupLocation>.Value
                Dim tmpBackupIntervalHour As String = xDoc...<BackupIntervalHour>.Value
                Dim tmpBackupIntervalMinute As String = xDoc...<BackupIntervalMinute>.Value
                Dim tmpCurrentIntervalHour As String = xDoc...<CurrentIntervalHour>.Value
                Dim tmpCurrentIntervalMinute As String = xDoc...<CurrentIntervalMinute>.Value
                Dim tmpPastVersions As String = xDoc...<PastVersionsToKeep>.Value
                If tmpName <> "" And tmpOriginalLoc <> "" And tmpBackupLoc <> "" And tmpBackupIntervalHour <> "" And tmpBackupIntervalMinute <> "" And tmpCurrentIntervalHour <> "" And tmpCurrentIntervalMinute <> "" And tmpPastVersions <> "" Then
                    backups.Add(tmpName, New BackupClass(tmpOriginalLoc, tmpBackupLoc, tmpBackupIntervalHour, tmpBackupIntervalMinute, tmpPastVersions, tmpCurrentIntervalHour, tmpCurrentIntervalMinute))
                    mainLogger.Log("Loaded " & f & " as a backup xml file with data: Name=" & tmpName _
                                   & ",OriginalLocation=" & tmpOriginalLoc & ",BackupLocation=" & tmpBackupLoc & _
                                   ",BackupInterval=" & tmpBackupIntervalHour & ",PastVersions=" & tmpPastVersions & _
                                   ",CurrentIntervalHour=" & tmpCurrentIntervalHour & _
                                   ",CurrentIntervalMinute=" & tmpCurrentIntervalMinute)
                Else
                    mainLogger.Log("|" & f & "| wasn't loaded due to having an incorrect element struture")
                End If
            Else
                mainLogger.Log("|" & f & "| wasn't loaded due to not being a .xml file")
            End If
        Next
        lbx_backups.Items.Clear()
        For Each k In backups.Keys
            lbx_backups.Items.Add(k)
        Next
        mainLogger.Log("Loaded backups into listbox")
    End Sub

    Friend Sub NewBackup(ByVal name As String, ByVal backupData As BackupClass)
        CreateBackup(name, backupData)
        mainLogger.Log("Created new backup '" & "backup_" & name & ".xml" & "' with data: Name=" & name & _
                       ",OriginalLocation=" & backupData.OriginalLoc & ",BackupLocation=" & backupData.BackupLoc & _
                       ",BackupIntervalHour=" & backupData.BackupInvervalHour & ",BackupIntervalMinute=" & backupData.BackupInvervalMinute & _
                       ",PastVersions=" & backupData.PastVersions & ",CurrentIntervalHour=" & backupData.CurrentIntervalHour & _
                       ",CurrentIntervalMinute=" & backupData.CurrentIntervalMinute)
        LoadBackups()
    End Sub

    Friend Sub SaveBackup(ByVal name As String, ByVal backupData As BackupClass, Optional ByVal log As Boolean = True)
        If FileExists(backupFilesLoc & "backup_" & name & ".xml") Then
            DeleteFile(backupFilesLoc & "backup_" & name & ".xml")
        End If
        CreateBackup(name, backupData)
        If log = True Then
            mainLogger.Log("Saved backup '" & "backup_" & name & ".xml" & "' with data: Name=" & name & _
                           ",OriginalLocation=" & backupData.OriginalLoc & ",BackupLocation=" & backupData.BackupLoc & _
                           ",BackupIntervalHour=" & backupData.BackupInvervalHour & ",BackupIntervalMinute=" & backupData.BackupInvervalMinute & _
                           ",PastVersions=" & backupData.PastVersions & ",CurrentIntervalHour=" & backupData.CurrentIntervalHour & _
                           ",CurrentIntervalMinute=" & backupData.CurrentIntervalMinute)
        End If
    End Sub

    Friend Sub CreateBackup(ByVal name As String, ByVal backupData As BackupClass)
        xDoc = New XDocument(New XElement("Backup", _
                                          New XElement("Name", name), _
                                          New XElement("OriginalLocation", backupData.OriginalLoc), _
                                          New XElement("BackupLocation", backupData.BackupLoc), _
                                          New XElement("BackupIntervalHour", backupData.BackupInvervalHour), _
                                          New XElement("BackupIntervalMinute", backupData.BackupInvervalMinute), _
                                          New XElement("CurrentIntervalHour", backupData.CurrentIntervalHour), _
                                          New XElement("CurrentIntervalMinute", backupData.CurrentIntervalMinute), _
                                          New XElement("PastVersionsToKeep", backupData.PastVersions) _
                                          ))
        xDoc.Save(backupFilesLoc & "backup_" & name & ".xml")
    End Sub

    Private Sub Main_Closing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = Not stopExecution
        Me.Hide()
        Me.ShowInTaskbar = False
        mainLogger.Log("Main form hidden by closing")
    End Sub

    Private Sub notifyIcon_main_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles notifyIcon_main.MouseDoubleClick
        Me.Show()
        Me.ShowInTaskbar = True
        mainLogger.Log("Main form shown by notifyIcon_main double click")
    End Sub

    Private Sub exit_tool_Click(sender As Object, e As EventArgs) Handles exit_tool.Click
        mainLogger.Log("notifyIcon_main exit clicked")
        Terminate()
    End Sub

    Private Sub startBackup_tool_Click(sender As Object, e As EventArgs) Handles startBackup_tool.Click
        BackupSelector.Show()
    End Sub

    Private Sub ckbx_startOnBoot_CheckedChanged(sender As Object, e As EventArgs) Handles ckbx_startOnBoot.CheckedChanged
        If startOnBoot <> ckbx_startOnBoot.Checked Then
            startOnBoot = ckbx_startOnBoot.Checked
            SaveOptions()
            CheckShortCut()
        End If
    End Sub

    Private Sub tmr_main_Tick(sender As Object, e As EventArgs) Handles tmr_main.Tick
        Dim keys As String() = backups.Keys.ToArray
        For Each k In keys
            Dim v As BackupClass = backups(k)
            v.CurrentIntervalMinute += 1
            If v.CurrentIntervalMinute = 60 Then
                v.CurrentIntervalMinute = 0
                v.CurrentIntervalHour += 1
            End If
            mainLogger.Log("Increased CurrentInvervalMinute on backup " & k & " (to " & v.CurrentIntervalMinute & "), CurrentIntervalHour = " & v.CurrentIntervalHour)
            If v.CurrentIntervalMinute = v.BackupInvervalMinute And v.CurrentIntervalHour = v.BackupInvervalHour Then
                QueueBackup(v.OriginalLoc, v.BackupLoc, v.PastVersions)
                v.CurrentIntervalHour = 0
                v.CurrentIntervalMinute = 0
                mainLogger.Log("Queued backup " & k)
            End If
            backups(k) = v
            SaveBackup(k, v, False)
        Next
    End Sub

    Private Sub btn_new_Click(sender As Object, e As EventArgs) Handles btn_new.Click
        BackupAdder.Show()
    End Sub

    Public Sub QueueBackup(ByVal originalLoc As String, ByVal backupLoc As String, ByVal pastVersions As Integer)
        queued.Add(queued.Count, New queueClass(originalLoc, backupLoc, pastVersions))
        mainLogger.Log("Backup added to queue list")
        If Not secondThread.IsBusy Then
            StartBackups()
        End If
    End Sub

    Public Sub StartBackups()
        If queued.Count > 0 Then
            ProgressForm.Show()
            secondThread.RunWorkerAsync(queued.First.Value)
            queued.Remove(queued.First.Key)
        End If
    End Sub

    Private Sub secondThread_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles secondThread.DoWork
        Dim args As queueClass
        Try
            args = e.Argument
        Catch ex As Exception
            Throw New ArgumentException("Argument couldn't be converted to queueClass")
            Exit Sub
        End Try
        secondThread.ReportProgress(0, {"proccess", "Getting amount of files in backup"})
        Dim amount As UInt32 = GetFileQuantity(args.originalLoc)
        secondaryLogger.Log("Loaded size of backup, " & amount & " to backup")
        Dim amountDone As New UInt32
        secondThread.ReportProgress(0, {"proccess", "Backing up files"})
        BackupFolder(args.originalLoc, args.backupLoc, args.pastVersions, amount, amountDone)
    End Sub

    Private Function BackupFolder(ByVal originalLoc As String, ByVal backupLoc As String, ByVal pastVersions As Integer, ByVal amount As UInt32, ByVal amountDone As UInt32) As Single
        Dim file1 As FileInfo
        Dim file2 As FileInfo
        Dim filebk As String = ""
        If Not DirectoryExists(backupLoc) Then
            CreateDirectory(backupLoc)
        End If
        For Each f In GetFiles(originalLoc)
            filebk = backupLoc & GetLocalPath(f, originalLoc)
            file1 = New FileInfo(f)
            file2 = New FileInfo(filebk)
            If FileExists(filebk) Then
                If file1.LastWriteTime <> file2.LastWriteTime Then
                    Try
                        If pastVersions = 0 Then
                            DeleteFile(filebk)
                            CopyFile(f, filebk)
                        Else
                            If FileExists(filebk & ".backup" & pastVersions) Then
                                DeleteFile(filebk & ".backup" & pastVersions)
                            End If
                            If pastVersions > 1 Then
                                For i = pastVersions - 1 To 1 Step -1
                                    If FileExists(filebk & ".backup" & i) Then
                                        DeleteFile(filebk & ".backup" & i)
                                    End If
                                    If FileExists(filebk & ".backup" & (i - 1)) Then
                                        CopyFile(filebk & ".backup" & (i - 1), filebk & ".backup" & i)
                                    End If
                                Next
                            End If
                            If FileExists(filebk) Then
                                CopyFile(filebk, filebk & ".backup" & 1)
                            End If
                        End If
                        secondaryLogger.Log("Backed up " & f & " to " & filebk)
                    Catch ex As Exception
                        secondaryLogger.Log("Didn't back up " & f & ", reason = " & ex.Message)
                    End Try
                Else
                    secondaryLogger.Log("Didn't back up " & f & ", reason = not changed")
                End If
            Else
                CopyFile(f, filebk)
                secondaryLogger.Log("Backed up " & f & " to " & filebk)
            End If
            amountDone += 1
            secondaryLogger.Log("amountDone=" & amountDone & ",amount=" & amount)
            UpdateQuantityLabel(Math.Round(amountDone / amount * 100, 2), amountDone, amount)
        Next
        For Each d In GetDirectories(originalLoc)
            amountDone = BackupFolder(d, backupLoc & GetLocalPath(d, originalLoc), pastVersions, amount, amountDone)
        Next
        Return amountDone
    End Function

    Public Function GetFileQuantity(ByVal startingDir As String, Optional ByVal startingValue As UInt32 = 0)
        Dim files As Single = startingValue
        For Each f In GetFiles(startingDir)
            files += 1
            UpdateQuantityLabel(0, 0, files)
        Next
        For Each d In GetDirectories(startingDir)
            files = GetFileQuantity(d, files)
        Next
        Return files
    End Function

    Private Sub UpdateQuantityLabel(ByVal percent As Byte, ByVal done As UInt32, ByVal amount As UInt32)
        secondThread.ReportProgress(percent, {"amount", done & "/" & amount})
    End Sub

#Region "Depreciated size-get functions"

    <Obsolete("No longer needed")>
    Private backupSizeLevel As Integer
    <Obsolete("No longer needed")>
    Private backedupSizeLevel As Integer
    <Obsolete("No longer needed")>
    Private levels() As String = {"B", "KB", "MB", "GB", "TB"}

    <Obsolete("Use 'GetFileQuantity' instead")>
    Public Function GetFileQuantityBySize(ByVal startingDir As String, Optional ByVal startingValue As Single = 0)
        Dim fileSizes As Single = startingValue
        Dim fileInfo As FileInfo
        For Each f In GetFiles(startingDir)
            fileInfo = GetFileInfo(f)
            fileSizes += Conv(fileInfo.Length)
            UpdateQuantitySizeLabel(fileSizes)
            If fileSizes > 1024 Then
                backupSizeLevel += 1
                fileSizes = fileSizes / 1024
            End If
        Next
        For Each d In GetDirectories(startingDir)
            fileSizes = GetFileQuantityBySize(d, fileSizes)
        Next
        Return fileSizes
    End Function

    <Obsolete("Use 'UpdateQuantityLabel' instead")>
    Private Sub UpdateQuantitySizeLabel(ByVal bytes As Single)
        secondThread.ReportProgress(0, {"size", Math.Round(bytes, 2) & " " & levels(backupSizeLevel)})
    End Sub

    <Obsolete("No longer needed")>
    Private Function Conv(ByVal value As Single) As Single
        For i = 1 To backupSizeLevel
            value = value / 1024
        Next
        Return value
    End Function

#End Region

    Private Sub secondThread_ReportProgress(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles secondThread.ProgressChanged
        If e.ProgressPercentage > 0 Then
            ProgressForm.prog_backupProg.Value = e.ProgressPercentage
        End If
        If Not IsNothing(e.UserState) Then
            Dim userState As String() = e.UserState
            If userState(0) = "proccess" Then
                ProgressForm.lbl_currentProccess.Text = userState(1)
            ElseIf userState(0) = "amount" Then
                ProgressForm.lbl_done.Text = userState(1)
            End If
        End If
        UpdateApp()
    End Sub

    Private Sub secondThread_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles secondThread.RunWorkerCompleted
        ProgressForm.lbl_currentProccess.Text = "Complete!"
        StartBackups()
        secondaryLogger.Log("Finished backup proccess")
        ProgressForm.progress_bar_filled()
    End Sub

    Private Sub lbx_backups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbx_backups.SelectedIndexChanged
        If lbx_backups.SelectedItem <> "" Then
            Dim selected As BackupClass = backups(lbx_backups.SelectedItem)
            tbx_bkupName.Text = lbx_backups.SelectedItem
            tbx_originalLoc.Text = selected.OriginalLoc
            tbx_bkupLoc.Text = selected.BackupLoc
            nud_hours.Value = selected.BackupInvervalHour
            nud_minutes.Value = selected.BackupInvervalMinute
            nud_previousQuantity.Value = selected.PastVersions
            btn_deleteBackup.Enabled = True
            btn_saveChanges.Enabled = True
            btn_startBackup.Enabled = True
        End If
    End Sub

    Private Sub btn_saveChanges_Click(sender As Object, e As EventArgs) Handles btn_saveChanges.Click
        If lbx_backups.SelectedItem <> "" Then
            Dim toUpdate As BackupClass = backups(lbx_backups.SelectedItem)
            toUpdate.BackupInvervalHour = nud_hours.Value
            toUpdate.BackupInvervalMinute = nud_minutes.Value
            toUpdate.PastVersions = nud_previousQuantity.Value
            backups(tbx_bkupName.Text) = toUpdate
            SaveBackup(tbx_bkupName.Text, toUpdate)
        End If
    End Sub

    Private Sub btn_startBackup_Click(sender As Object, e As EventArgs) Handles btn_startBackup.Click
        If lbx_backups.SelectedItem <> "" Then
            Dim selected As BackupClass = backups(lbx_backups.SelectedItem)
            QueueBackup(selected.OriginalLoc, selected.BackupLoc, selected.PastVersions)
            selected.CurrentIntervalHour = 0
            backups(lbx_backups.SelectedItem) = selected
            mainLogger.Log("Queued and updated backup " & lbx_backups.SelectedItem)
        End If
    End Sub

    Private Sub btn_deleteBackup_Click(sender As Object, e As EventArgs) Handles btn_deleteBackup.Click

    End Sub

    Private Sub cbx_showWindow_CheckedChanged(sender As Object, e As EventArgs) Handles ckbx_showWindow.CheckedChanged
        If showFormOnLaunch <> ckbx_showWindow.Checked Then
            showFormOnLaunch = ckbx_showWindow.Checked
            SaveOptions()
        End If
    End Sub
End Class
