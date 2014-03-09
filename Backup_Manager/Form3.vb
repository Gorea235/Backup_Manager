Public Class ProgressForm
    Friend Sub progress_bar_filled()
        AddHandler tmr_timer.Tick, AddressOf timer_finished
        tmr_timer.Interval = 3000
        tmr_timer.Enabled = True
    End Sub

    Public Sub timer_finished(sender As Object, e As EventArgs)
        Me.Close()
        RemoveHandler tmr_timer.Tick, AddressOf timer_finished
    End Sub
End Class