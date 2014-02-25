Public Class BackupSelector
    Private Sub Backup_Selector_Show() Handles Me.Shown
        clbx_backups.Items.Clear()
        For Each k In backups.Keys
            clbx_backups.Items.Add(k)
        Next
    End Sub

    Private Sub btn_startBackups_Click(sender As Object, e As EventArgs) Handles btn_startBackups.Click
        For Each c In clbx_backups.CheckedItems
            Dim checked As BackupClass = backups(c)
            Main.QueueBackup(checked.OriginalLoc, checked.BackupLoc, checked.PastVersions)
        Next
        Me.Close()
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Me.Close()
    End Sub
End Class