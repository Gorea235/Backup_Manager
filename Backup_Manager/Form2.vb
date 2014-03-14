Imports Microsoft.VisualBasic.FileIO.FileSystem
Public Class BackupAdder
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Me.Close()
    End Sub

    Private Sub btn_addBackup_Click(sender As Object, e As EventArgs) Handles btn_addBackup.Click
        If Not backups.ContainsKey(tbx_bkupName.Text) Then
            If DirectoryExists(tbx_originalLoc.Text) And DirectoryExists(tbx_bkupLoc.Text) Then
                Main.NewBackup(tbx_bkupName.Text, New BackupClass(tbx_originalLoc.Text, tbx_bkupLoc.Text, nud_hours.Value, nud_minutes.Value, nud_previousQuantity.Value, 0, 0, ckbx_manual.Checked))
                Me.Close()
            Else
                MessageBox.Show("1 or more of the specified folders don't exist!", "Invalid folder selection", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("A backup with the same name already exists!", "Backup already exists", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btn_findOriginal_Click(sender As Object, e As EventArgs) Handles btn_findOriginal.Click
        If fbd_orignialLoc.ShowDialog() = DialogResult.OK Then
            tbx_originalLoc.Text = fbd_orignialLoc.SelectedPath
        End If
    End Sub

    Private Sub btn_findBackup_Click(sender As Object, e As EventArgs) Handles btn_findBackup.Click
        If fbd_backupLoc.ShowDialog() = DialogResult.OK Then
            tbx_bkupLoc.Text = fbd_backupLoc.SelectedPath
        End If
    End Sub

    Private Sub BackupAdder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbx_bkupLoc.Text = ""
        tbx_bkupName.Text = ""
        tbx_originalLoc.Text = ""
        nud_hours.Value = 1
        nud_minutes.Value = 0
        nud_previousQuantity.Value = 0
        ckbx_manual.Checked = False
        mainLogger.Log("New backup form shown")
    End Sub
End Class