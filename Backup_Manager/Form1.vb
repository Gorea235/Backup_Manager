Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports System.IO
Imports IWshRuntimeLibrary
Imports System.Environment
Imports Microsoft.WindowsAPICodePack.Taskbar
Public Class Main
    Private Class queueClass
        Public name As String
        Public originalLoc As String
        Public backupLoc As String
        Public pastVersions As Integer

        Public Sub New(ByVal Name As String, ByVal OriginalLocation As String, ByVal BackupLocation As String, ByVal VersionsToKeep As Integer)
            Me.name = Name
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
        ckbx_debug.Checked = debugMode
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
            Else
                Dim shl = New Shell32.Shell()
                Dim dir = shl.[NameSpace](System.IO.Path.GetDirectoryName(shortLocation))
                Dim itm = dir.Items().Item(System.IO.Path.GetFileName(shortLocation))
                Dim lnk = DirectCast(itm.GetLink, Shell32.ShellLinkObject)
                If lnk.Path <> Application.ExecutablePath Then
                    DeleteFile(shortLocation)
                    CheckShortCut()
                End If
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
                Dim tmpManual As String = xDoc...<Manual>.Value
                If tmpName <> "" And tmpOriginalLoc <> "" And tmpBackupLoc <> "" And tmpBackupIntervalHour <> "" And tmpBackupIntervalMinute <> "" And tmpCurrentIntervalHour <> "" And tmpCurrentIntervalMinute <> "" And tmpPastVersions <> "" And tmpManual <> "" Then
                    backups.Add(tmpName, New BackupClass(tmpOriginalLoc, tmpBackupLoc, tmpBackupIntervalHour, tmpBackupIntervalMinute, tmpPastVersions, tmpCurrentIntervalHour, tmpCurrentIntervalMinute, CBool(tmpManual)))
                    mainLogger.Log("Loaded " & f & " as a backup xml file with data: Name=" & tmpName _
                                   & ",OriginalLocation=" & tmpOriginalLoc & ",BackupLocation=" & tmpBackupLoc & _
                                   ",BackupInterval=" & tmpBackupIntervalHour & ",PastVersions=" & tmpPastVersions & _
                                   ",CurrentIntervalHour=" & tmpCurrentIntervalHour & _
                                   ",CurrentIntervalMinute=" & tmpCurrentIntervalMinute & ",Manual=" & CBool(tmpManual))
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
                       ",CurrentIntervalMinute=" & backupData.CurrentIntervalMinute & ",Manual=" & backupData.Manual)
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
                           ",CurrentIntervalMinute=" & backupData.CurrentIntervalMinute & ",Manual=" & backupData.Manual)
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
                                          New XElement("PastVersionsToKeep", backupData.PastVersions), _
                                          New XElement("Manual", backupData.Manual) _
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
            If Not v.Manual Then
                v.CurrentIntervalMinute += 1
                If v.CurrentIntervalMinute = 60 Then
                    v.CurrentIntervalMinute = 0
                    v.CurrentIntervalHour += 1
                End If
                If debugMode Then
                    mainLogger.Log("Increased CurrentInvervalMinute on backup " & k & " (to " & v.CurrentIntervalMinute & "), CurrentIntervalHour = " & v.CurrentIntervalHour)
                End If
                If v.CurrentIntervalMinute = v.BackupInvervalMinute And v.CurrentIntervalHour = v.BackupInvervalHour Then
                    QueueBackup(k, v.OriginalLoc, v.BackupLoc, v.PastVersions)
                    v.CurrentIntervalHour = 0
                    v.CurrentIntervalMinute = 0
                    mainLogger.Log("Queued backup '" & k & "'")
                End If
                backups(k) = v
                SaveBackup(k, v, False)
                If lbx_backups.SelectedItem = k Then
                    tbx_currentHour.Text = v.CurrentIntervalHour
                    tbx_currentMinute.Text = v.CurrentIntervalMinute
                End If
            End If
        Next
    End Sub

    Private Sub btn_new_Click(sender As Object, e As EventArgs) Handles btn_new.Click
        BackupAdder.Show()
    End Sub

    Public Sub QueueBackup(ByVal name As String, ByVal originalLoc As String, ByVal backupLoc As String, ByVal pastVersions As Integer)
        queued.Add(queued.Count, New queueClass(name, originalLoc, backupLoc, pastVersions))
        UpdateStatusText()
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

#Region "Second thread work - backing up"

    Private Sub secondThread_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles secondThread.DoWork
        Dim args As queueClass
        Try
            args = e.Argument
        Catch ex As Exception
            Throw New ArgumentException("Argument couldn't be converted to queueClass")
            Exit Sub
        End Try
        secondThread.ReportProgress(0, {"proccess", "Getting amount of files in backup"})
        secondThread.ReportProgress(0, {"current", args.name})
        Dim seconds As UInt32 = 0
        Dim files As Stack(Of String) = GetFileQuantity(args.originalLoc, seconds)
        Dim amount As UInt32 = files.Count
        secondaryLogger.Log("Loaded size of backup, " & amount & " files to backup")
        Dim amountDone As New UInt32
        secondThread.ReportProgress(0, {"proccess", "Backing up files"})
        secondaryLogger.Log("Starting backup process with backup data '" & args.name & "'")
        e.Result = BackupFolder(args.originalLoc, args.backupLoc, args.pastVersions, amount, files) + seconds
    End Sub

    Private Function BackupFolder(ByVal originalLoc As String, ByVal backupLoc As String, ByVal pastVersions As Integer, ByVal amount As UInt32, ByVal files As Stack(Of String))
        Dim file1 As FileInfo
        Dim file2 As FileInfo
        Dim filebk As String = ""
        Dim amountDone As UInt32
        Dim origFile As String = ""
        Dim backDir As String = ""
        Dim skips As UInt32 = 0
        Dim copies As UInt32 = 0
        Dim errors As New Stack(Of String)
        Dim stopwatch As New Stopwatch
        stopwatch.Restart()
        While files.Count > 0
            origFile = files.Pop()
            file1 = New FileInfo(origFile)
            backDir = backupLoc & GetLocalPath(file1.DirectoryName, originalLoc)
            If Not DirectoryExists(backDir) Then
                CreateDirectory(backDir)
            End If
            filebk = backDir & GetLocalPath(origFile, file1.DirectoryName)
            file2 = New FileInfo(filebk)
            If FileExists(filebk) Then
                If file1.LastWriteTime <> file2.LastWriteTime Then
                    Try
                        If pastVersions = 0 Then
                            DeleteFile(filebk)
                            CopyFile(origFile, filebk)
                            copies += 1
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
                                copies += 1
                            End If
                        End If
                        'secondaryLogger.Log("Backed up " & origFile & " to " & filebk)
                    Catch ex As Exception
                        errors.Push("Didn't back up " & origFile & ", reason = " & ex.Message)
                    End Try
                Else
                    skips += 1
                    'secondaryLogger.Log("Didn't back up " & origFile & ", reason = not changed")
                End If
            Else
                CopyFile(origFile, filebk)
                copies += 1
                'secondaryLogger.Log("Backed up " & origFile & " to " & filebk)
            End If
            amountDone += 1
            'secondaryLogger.Log("amountDone=" & amountDone & ",amount=" & amount)
            UpdateQuantityLabel(Math.Round(amountDone / amount * 100, 2), amountDone, amount)
        End While
        stopwatch.Stop()
        secondaryLogger.Log("Backup process complete with " & copies & " copies, " & skips & " skips and " & errors.Count & " errors, with a total of " & amountDone & " / " & amount & " files checked in " & (stopwatch.ElapsedMilliseconds / 1000) & " seconds")
        If errors.Count > 0 Then
            For Each ex As String In errors
                secondaryLogger.Log(ex)
            Next
        End If
        Return (stopwatch.ElapsedMilliseconds / 1000)
    End Function

    Public Function GetFileQuantity(ByVal startingDir As String, ByRef seconds As UInt32)
        Dim files As New Stack(Of String)
        Dim dirs As New Stack(Of String)
        Dim dir As String
        Dim stopwatch As New Stopwatch
        dirs.Push(startingDir)
        secondaryLogger.Log("Getting all files in " & startingDir)
        stopwatch.Restart()
        While dirs.Count > 0
            dir = dirs.Pop()
            For Each f In GetFiles(dir)
                Try
                    files.Push(f)
                    UpdateQuantityLabel(0, 0, files.Count)
                Catch ex As Exception
                    secondaryLogger.Log("Error occured when adding file " & f & " to stack")
                End Try
            Next
            For Each d In GetDirectories(dir)
                Try
                    dirs.Push(d)
                Catch ex As Exception
                    secondaryLogger.Log("Error occured when adding directory " & d & " to stack")
                End Try
            Next
        End While
        stopwatch.Stop()
        seconds = (stopwatch.ElapsedMilliseconds / 1000)
        secondaryLogger.Log("Got all files in '" & startingDir & "' in " & seconds & " seconds")
        Return files
    End Function

    Private Sub UpdateQuantityLabel(ByVal percent As Byte, ByVal done As UInt32, ByVal amount As UInt32)
        secondThread.ReportProgress(percent, {"amount", done.ToString("n0") & "/" & amount.ToString("n0")})
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
        Try
            If e.ProgressPercentage > 0 Then
                ProgressForm.prog_backupProg.Value = e.ProgressPercentage
                TaskbarManager.Instance.SetProgressValue(e.ProgressPercentage, 100)
            End If
        Catch ex As Exception
            mainLogger.Log("Could not update percent, reason: " & ex.Message)
        End Try
        Dim userState As String() = e.UserState
        Try
            If Not IsNothing(e.UserState) Then
                If userState(0) = "proccess" Then
                    ProgressForm.lbl_currentProccess.Text = userState(1)
                ElseIf userState(0) = "amount" Then
                    ProgressForm.lbl_done.Text = userState(1)
                ElseIf userState(0) = "current" Then
                    ProgressForm.lbl_currentBackup.Text = userState(1)
                    currentBackup = userState(1)
                    ProgressForm.ControlBox = False
                    ProgressForm.prog_backupProg.Value = 0
                    UpdateStatusText()
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal)
                End If
            End If
        Catch ex As Exception
            mainLogger.Log("Could not update " & userState(0) & ", reason: " & ex.Message)
        End Try
    End Sub

    Private Sub secondThread_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles secondThread.RunWorkerCompleted
        ProgressForm.lbl_currentProccess.Text = "Completed in " & e.Result & " seconds"
        currentBackup = ""
        StartBackups()
        secondaryLogger.Log("Finished backup proccess")
        If queued.Count > 0 Then
            Threading.Thread.Sleep(3000)
        Else
            ProgressForm.ControlBox = True
        End If
        UpdateStatusText()
        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress)
    End Sub

#End Region

    Private Sub UpdateStatusText()
        Dim prefix As String = "Current status: "
        Dim text As String = Nothing
        If currentBackup = lbx_backups.SelectedItem Then
            text = "Backing up"
        Else
            For Each q In queued.Values
                If q.name = lbx_backups.SelectedItem Then
                    text = "Queued for backup"
                End If
            Next
        End If
        If text = Nothing Then
            text = "Idle"
        End If
        lbl_currentStatus.Text = prefix & text
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
            ckbx_manual.Checked = selected.Manual
            tbx_currentHour.Text = selected.CurrentIntervalHour
            tbx_currentMinute.Text = selected.CurrentIntervalMinute
            UpdateStatusText()
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
            toUpdate.Manual = ckbx_manual.Checked
            backups(tbx_bkupName.Text) = toUpdate
            SaveBackup(tbx_bkupName.Text, toUpdate)
        End If
    End Sub

    Private Sub btn_startBackup_Click(sender As Object, e As EventArgs) Handles btn_startBackup.Click
        If lbx_backups.SelectedItem <> "" Then
            Dim selected As BackupClass = backups(lbx_backups.SelectedItem)
            QueueBackup(lbx_backups.SelectedItem, selected.OriginalLoc, selected.BackupLoc, selected.PastVersions)
            selected.CurrentIntervalHour = 0
            selected.CurrentIntervalMinute = 0
            backups(lbx_backups.SelectedItem) = selected
            SaveBackup(lbx_backups.SelectedItem, selected)
            mainLogger.Log("Queued and updated backup '" & lbx_backups.SelectedItem & "'")
        End If
    End Sub

    Private Sub btn_deleteBackup_Click(sender As Object, e As EventArgs) Handles btn_deleteBackup.Click
        If lbx_backups.SelectedItem <> "" Then
            DeleteFile(backupFilesLoc & "backup_" & lbx_backups.SelectedItem & ".xml")
            LoadBackups()
            btn_deleteBackup.Enabled = False
            btn_saveChanges.Enabled = False
            btn_startBackup.Enabled = False
        End If
    End Sub

    Private Sub cbx_showWindow_CheckedChanged(sender As Object, e As EventArgs) Handles ckbx_showWindow.CheckedChanged
        If showFormOnLaunch <> ckbx_showWindow.Checked Then
            showFormOnLaunch = ckbx_showWindow.Checked
            SaveOptions()
        End If
    End Sub

    Private Sub ckbx_debug_CheckedChanged(sender As Object, e As EventArgs) Handles ckbx_debug.CheckedChanged
        If debugMode <> ckbx_debug.Checked Then
            debugMode = ckbx_debug.Checked
            SaveOptions()
        End If
    End Sub
End Class
