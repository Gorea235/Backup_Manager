Imports Microsoft.VisualBasic.FileIO.FileSystem
Module Globals
    Friend Class BackupClass
        Public OriginalLoc As String
        Public BackupLoc As String
        Public BackupInverval As Integer
        Public CurrentInterval As Integer
        Public MinutePast As Byte
        Public PastVersions As Integer

        Public Sub New(ByVal sOriginalLoc As String, ByVal sBackupLoc As String, ByVal iBackupInterval As Integer, ByVal iMinutePast As Byte, ByVal iPastVersions As Integer)
            If iMinutePast > 59 Then
                Throw New InvalidConstraintException("Minute cannot be above 59")
            End If
            Me.OriginalLoc = sOriginalLoc
            Me.BackupLoc = sBackupLoc
            Me.BackupInverval = iBackupInterval
            Me.MinutePast = iMinutePast
            Me.PastVersions = iPastVersions
        End Sub
    End Class
    Friend Class FileSize
        Public size As UInt16
        Public Sub AddBytes(ByVal bytes As UInteger)

        End Sub

        Public Sub RemoveBytes(ByVal bytes As UInteger)
            If bytes > size Then
                Throw New ArithmeticException("Bytes cannot go below 0")
            End If
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
    Friend mainLogger As New Logger.Logger(LogLoc, "Main Thread:")
    Friend secondaryLogger As New Logger.Logger(LogLoc, "Secondary Thread: ")
    Friend backingup As Boolean = False
    Friend backups As Dictionary(Of String, BackupClass) = New Dictionary(Of String, BackupClass)

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

    Public Function GetLocalPath(ByVal location As String, ByVal actualDir As String)
        Return Right(location, location.Length - actualDir.Length)
    End Function
End Module
