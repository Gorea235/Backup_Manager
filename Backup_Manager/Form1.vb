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
                Dim tmpBackupInterval As String = xDoc...<BackupInterval>.Value
                Dim tmpMinutePast As String = xDoc...<MinutesPastHour>.Value
                Dim tmpPastVersions As String = xDoc...<PastVersionsToKeep>.Value
                If tmpName <> "" And tmpOriginalLoc <> "" And tmpBackupLoc <> "" And tmpBackupInterval <> "" And tmpMinutePast <> "" And tmpPastVersions <> "" Then
                    backups.Add(tmpName, New BackupClass(tmpOriginalLoc, tmpBackupLoc, tmpBackupInterval, tmpMinutePast, tmpPastVersions))
                    mainLogger.Log("Loaded " & f & " as a backup xml file with data: Name=" & tmpName _
                                   & ",OriginalLocation=" & tmpOriginalLoc & ",BackupLocation=" & tmpBackupLoc & _
                                   ",BackupInterval=" & tmpBackupInterval & ",MinutePast=" & tmpMinutePast & _
                                   ",PastVersions=" & tmpPastVersions)
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
                       ",BackupInterval=" & backupData.BackupInverval & ",MinutePast=" & backupData.MinutePast & _
                       ",PastVersions=" & backupData.PastVersions)
        LoadBackups()
    End Sub

    Friend Sub SaveBackup(ByVal name As String, ByVal backupData As BackupClass)
        If FileExists(backupFilesLoc & "backup_" & name & ".xml") Then
            DeleteFile(backupFilesLoc & "backup_" & name & ".xml")
        End If
        CreateBackup(name, backupData)
        mainLogger.Log("Saved backup '" & "backup_" & name & ".xml" & "' with data: Name=" & name & _
                       ",OriginalLocation=" & backupData.OriginalLoc & ",BackupLocation=" & backupData.BackupLoc & _
                       ",BackupInterval=" & backupData.BackupInverval & ",MinutePast=" & backupData.MinutePast & _
                       ",PastVersions=" & backupData.PastVersions)
    End Sub

    Friend Sub CreateBackup(ByVal name As String, ByVal backupData As BackupClass)
        xDoc = New XDocument(New XElement("Backup", _
                                          New XElement("Name", name), _
                                          New XElement("OriginalLocation", backupData.OriginalLoc), _
                                          New XElement("BackupLocation", backupData.BackupLoc), _
                                          New XElement("BackupInterval", backupData.BackupInverval), _
                                          New XElement("MinutesPastHour", backupData.MinutePast), _
                                          New XElement("PastVersionsToKeep", backupData.PastVersions) _
                                          ))
        xDoc.Save(backupFilesLoc & "backup_" & name & ".xml")
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
            If v.MinutePast = Minute(Now) Then
                v.CurrentInterval = v.CurrentInterval + 1
                mainLogger.Log("Increased CurrentInverval on backup " & k)
            End If
            If v.CurrentInterval = v.BackupInverval Then
                QueueBackup(v.OriginalLoc, v.BackupLoc, v.PastVersions)
                v.CurrentInterval = 0
                mainLogger.Log("Queued backup " & k)
            End If
            backups(k) = v
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
        secondThread.ReportProgress(0, {"proccess", "Getting size of files"})
        Dim size As UInt32 = GetFileQuantityBySize(args.originalLoc)
        secondaryLogger.Log("Loaded size of backup, " & size & " bytes to backup")
        Dim sizeDone As UInt32 = 0
        secondThread.ReportProgress(0, {"proccess", "Backing up files"})
        BackupFolder(args.originalLoc, args.backupLoc, args.pastVersions, size, sizeDone)
    End Sub

    Private Function BackupFolder(ByVal originalLoc As String, ByVal backupLoc As String, ByVal pastVersions As Integer, ByVal size As UInt32, ByVal sizeDone As UInt32)
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
            sizeDone = sizeDone + file1.Length
            secondThread.ReportProgress(Math.Round(sizeDone / size * 100))
        Next
        For Each d In GetDirectories(originalLoc)
            sizeDone = BackupFolder(d, backupLoc & GetLocalPath(d, originalLoc), pastVersions, size, sizeDone)
        Next
        Return sizeDone
    End Function

    Public Function GetFileQuantityBySize(ByVal startingDir As String)
        Dim fileSizes As UInt64 = 0
        Dim fileInfo As FileInfo
        For Each f In GetFiles(startingDir)
            fileInfo = GetFileInfo(f)
            fileSizes = fileSizes + fileInfo.Length
            UpdateQuantitySizeLabel(fileSizes)
        Next
        For Each d In GetDirectories(startingDir)
            fileSizes = fileSizes + GetFileQuantityBySize(d)
        Next
        Return fileSizes
    End Function

    Private Sub UpdateQuantitySizeLabel(ByVal bytes As UInt32)
        If bytes < 1024 Then
            secondThread.ReportProgress(0, {"size", bytes & " bytes"})
        ElseIf bytes < 1024 ^ 2 Then
            secondThread.ReportProgress(0, {"size", Math.Round((bytes ^ (1 / 2)), 1) & " KB"})
        ElseIf bytes < 1024 ^ 3 Then
            secondThread.ReportProgress(0, {"size", Math.Round((bytes / (1 / 3)), 1) & " MB"})
        Else
            secondThread.ReportProgress(0, {"size", Math.Round((bytes / (1 / 4)), 1) & " GB"})
        End If
    End Sub

    Private Sub secondThread_ReportProgress(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles secondThread.ProgressChanged
        If e.ProgressPercentage > 0 Then
            ProgressForm.prog_backupProg.Value = e.ProgressPercentage
        End If
        If Not IsNothing(e.UserState) Then
            Dim userState As String() = e.UserState
            If userState(0) = "proccess" Then
                ProgressForm.lbl_currentProccess.Text = userState(1)
            ElseIf userState(0) = "size" Then
                ProgressForm.lbl_size.Text = userState(1)
            End If
        End If
        ProgressForm.Update()
    End Sub

    Private Sub secondThread_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles secondThread.RunWorkerCompleted
        ProgressForm.lbl_currentProccess.Text = "Complete!"
        ProgressForm.Update()
        Threading.Thread.Sleep(1000)
        ProgressForm.Close()
        StartBackups()
        secondaryLogger.Log("Finished backup proccess")
    End Sub

    Private Sub lbx_backups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbx_backups.SelectedIndexChanged
        If lbx_backups.SelectedItem <> "" Then
            Dim selected As BackupClass = backups(lbx_backups.SelectedItem)
            tbx_bkupName.Text = lbx_backups.SelectedItem
            tbx_originalLoc.Text = selected.OriginalLoc
            tbx_bkupLoc.Text = selected.BackupLoc
            nud_interval.Value = selected.BackupInverval
            nud_minutesPast.Value = selected.MinutePast
            nud_previousQuantity.Value = selected.PastVersions
            btn_deleteBackup.Enabled = True
            btn_saveChanges.Enabled = True
            btn_startBackup.Enabled = True
        End If
    End Sub

    Private Sub btn_saveChanges_Click(sender As Object, e As EventArgs) Handles btn_saveChanges.Click
        If lbx_backups.SelectedItem <> "" Then
            Dim toUpdate As BackupClass = backups(lbx_backups.SelectedItem)
            toUpdate.BackupInverval = nud_interval.Value
            toUpdate.MinutePast = nud_minutesPast.Value
            toUpdate.PastVersions = nud_previousQuantity.Value
            backups(tbx_bkupName.Text) = toUpdate
            SaveBackup(tbx_bkupName.Text, toUpdate)
        End If
    End Sub

    Private Sub btn_startBackup_Click(sender As Object, e As EventArgs) Handles btn_startBackup.Click
        If lbx_backups.SelectedItem <> "" Then
            Dim selected As BackupClass = backups(lbx_backups.SelectedItem)
            QueueBackup(selected.OriginalLoc, selected.BackupLoc, selected.PastVersions)
            selected.CurrentInterval = 0
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
