Public Class backupResultsForm
    Private Sub form_closed() Handles Me.FormClosed
        dgv_results.Rows.Clear()
    End Sub
End Class