Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports Logging
Module Globals
    Friend Class BackupClass
        Public OriginalLoc As String
        Public BackupLoc As String
        Public BackupInvervalHour As UInt32
        Public BackupInvervalMinute As UInt32
        Public CurrentIntervalHour As UInt32
        Public CurrentIntervalMinute As UInt32
        Public PastVersions As UInt32
        Public Manual As Boolean

        Public Sub New(ByVal sOriginalLoc As String, ByVal sBackupLoc As String, ByVal iBackupIntervalHour As UInt32, ByVal iBackupIntervalMinute As UInt32, ByVal iPastVersions As UInt32, ByVal iCurrentIntervalHour As UInt32, ByVal iCurrentIntervalMinute As UInt32, ByVal bManual As Boolean)
            Me.OriginalLoc = sOriginalLoc
            Me.BackupLoc = sBackupLoc
            Me.BackupInvervalHour = iBackupIntervalHour
            Me.BackupInvervalMinute = iBackupIntervalMinute
            Me.PastVersions = iPastVersions
            Me.CurrentIntervalHour = iCurrentIntervalHour
            Me.CurrentIntervalMinute = iCurrentIntervalMinute
            Me.Manual = bManual
        End Sub
    End Class
    Friend Const mainDir As String = "C:/Backup_Manager/"
    Friend Const optionsLoc As String = mainDir & "options.xml"
    Friend Const backupFilesLoc As String = mainDir & "Backup_Info/"
    Friend Const LogLoc As String = mainDir & "MainLog.log"
    Friend startOnBoot As Boolean = True
    Friend showFormOnLaunch As Boolean = True
    Friend loadingComplete As Boolean = False
    Friend xDoc As XDocument
    Friend stopExecution As Boolean = False
    Friend mainLogger As New Logger(LogLoc, "Main Thread:")
    Friend secondaryLogger As New Logger(LogLoc, "Secondary Thread: ")
    Friend backingup As Boolean = False
    Friend backups As Dictionary(Of String, BackupClass) = New Dictionary(Of String, BackupClass)
    Friend tmr_timer As New Timer

    Sub New()
        tmr_timer.Enabled = False
    End Sub

    Friend Sub SaveOptions()
        xDoc = New XDocument(New XElement("Options", _
                                          New XElement("StartOnBoot", startOnBoot), _
                                          New XElement("ShowFormOnLaunch", showFormOnLaunch) _
                                          ))
        xDoc.Save(optionsLoc)
        mainLogger.Log("Options saved")
    End Sub

    Friend Sub LoadOptions()
        xDoc = New XDocument
        xDoc = XDocument.Load(optionsLoc)
        Try
            startOnBoot = If(xDoc...<StartOnBoot>.Value = "", True, xDoc...<StartOnBoot>.Value)
            showFormOnLaunch = If(xDoc...<ShowFormOnLaunch>.Value = "", True, xDoc...<ShowFormOnLaunch>.Value)
        Catch ex As Exception
            startOnBoot = True
            showFormOnLaunch = True
            mainLogger.Log("Options failed to load, using defaults")
        End Try
        mainLogger.Log("Options loaded")
    End Sub

    Friend Sub Terminate()
        stopExecution = True
        Main.Close()
    End Sub

    Function GetLocalPath(ByVal location As String, ByVal actualDir As String)
        Return Right(location, location.Length - actualDir.Length)
    End Function

    Friend Sub UpdateApp()
        Main.Update()
        BackupAdder.Update()
        ProgressForm.Update()
        BackupSelector.Update()
        Application.DoEvents()
    End Sub

    Friend Sub wait(ByVal milliseconds As UInt32)
        Dim sw As New Stopwatch
        sw.Reset()
        sw.Start()
        While sw.ElapsedMilliseconds < milliseconds
            UpdateApp()
        End While
        sw.Stop()
    End Sub
End Module
