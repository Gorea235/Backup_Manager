<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.lbx_backups = New System.Windows.Forms.ListBox()
        Me.btn_new = New System.Windows.Forms.Button()
        Me.tbx_bkupName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbx_originalLoc = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbx_bkupLoc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_saveChanges = New System.Windows.Forms.Button()
        Me.nud_interval = New System.Windows.Forms.NumericUpDown()
        Me.secondThread = New System.ComponentModel.BackgroundWorker()
        Me.notifyIcon_main = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.notifyIcon_menustrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.startBackup_tool = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.exit_tool = New System.Windows.Forms.ToolStripMenuItem()
        Me.ckbx_startOnBoot = New System.Windows.Forms.CheckBox()
        Me.nud_previousQuantity = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tmr_main = New System.Windows.Forms.Timer(Me.components)
        Me.nud_minutesPast = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_startBackup = New System.Windows.Forms.Button()
        CType(Me.nud_interval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.notifyIcon_menustrip.SuspendLayout()
        CType(Me.nud_previousQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_minutesPast, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbx_backups
        '
        Me.lbx_backups.FormattingEnabled = True
        Me.lbx_backups.Location = New System.Drawing.Point(12, 12)
        Me.lbx_backups.Name = "lbx_backups"
        Me.lbx_backups.Size = New System.Drawing.Size(160, 368)
        Me.lbx_backups.TabIndex = 0
        '
        'btn_new
        '
        Me.btn_new.Location = New System.Drawing.Point(530, 12)
        Me.btn_new.Name = "btn_new"
        Me.btn_new.Size = New System.Drawing.Size(100, 70)
        Me.btn_new.TabIndex = 2
        Me.btn_new.Text = "New Backup"
        Me.btn_new.UseVisualStyleBackColor = True
        '
        'tbx_bkupName
        '
        Me.tbx_bkupName.Enabled = False
        Me.tbx_bkupName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbx_bkupName.Location = New System.Drawing.Point(178, 36)
        Me.tbx_bkupName.Name = "tbx_bkupName"
        Me.tbx_bkupName.Size = New System.Drawing.Size(346, 23)
        Me.tbx_bkupName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(175, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(349, 21)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Backup Name:"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(175, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(349, 21)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Original Location:"
        '
        'tbx_originalLoc
        '
        Me.tbx_originalLoc.Enabled = False
        Me.tbx_originalLoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbx_originalLoc.Location = New System.Drawing.Point(178, 86)
        Me.tbx_originalLoc.Name = "tbx_originalLoc"
        Me.tbx_originalLoc.Size = New System.Drawing.Size(452, 23)
        Me.tbx_originalLoc.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(175, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(455, 20)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Backup Location:"
        '
        'tbx_bkupLoc
        '
        Me.tbx_bkupLoc.Enabled = False
        Me.tbx_bkupLoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbx_bkupLoc.Location = New System.Drawing.Point(178, 135)
        Me.tbx_bkupLoc.Name = "tbx_bkupLoc"
        Me.tbx_bkupLoc.Size = New System.Drawing.Size(452, 23)
        Me.tbx_bkupLoc.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(175, 161)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(455, 20)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Backup Interval (hours):"
        '
        'btn_saveChanges
        '
        Me.btn_saveChanges.Enabled = False
        Me.btn_saveChanges.Location = New System.Drawing.Point(530, 337)
        Me.btn_saveChanges.Name = "btn_saveChanges"
        Me.btn_saveChanges.Size = New System.Drawing.Size(100, 70)
        Me.btn_saveChanges.TabIndex = 10
        Me.btn_saveChanges.Text = "Save Changes"
        Me.btn_saveChanges.UseVisualStyleBackColor = True
        '
        'nud_interval
        '
        Me.nud_interval.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_interval.Location = New System.Drawing.Point(178, 184)
        Me.nud_interval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_interval.Name = "nud_interval"
        Me.nud_interval.Size = New System.Drawing.Size(452, 23)
        Me.nud_interval.TabIndex = 11
        Me.nud_interval.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'secondThread
        '
        '
        'notifyIcon_main
        '
        Me.notifyIcon_main.ContextMenuStrip = Me.notifyIcon_menustrip
        Me.notifyIcon_main.Icon = CType(resources.GetObject("notifyIcon_main.Icon"), System.Drawing.Icon)
        Me.notifyIcon_main.Text = "Backups"
        Me.notifyIcon_main.Visible = True
        '
        'notifyIcon_menustrip
        '
        Me.notifyIcon_menustrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.startBackup_tool, Me.ToolStripSeparator1, Me.exit_tool})
        Me.notifyIcon_menustrip.Name = "ContextMenuStrip1"
        Me.notifyIcon_menustrip.Size = New System.Drawing.Size(141, 54)
        Me.notifyIcon_menustrip.Text = "Exit"
        '
        'startBackup_tool
        '
        Me.startBackup_tool.Name = "startBackup_tool"
        Me.startBackup_tool.Size = New System.Drawing.Size(140, 22)
        Me.startBackup_tool.Text = "Start Backup"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(137, 6)
        '
        'exit_tool
        '
        Me.exit_tool.Name = "exit_tool"
        Me.exit_tool.Size = New System.Drawing.Size(140, 22)
        Me.exit_tool.Text = "Exit"
        '
        'ckbx_startOnBoot
        '
        Me.ckbx_startOnBoot.AutoSize = True
        Me.ckbx_startOnBoot.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbx_startOnBoot.Location = New System.Drawing.Point(12, 386)
        Me.ckbx_startOnBoot.Name = "ckbx_startOnBoot"
        Me.ckbx_startOnBoot.Size = New System.Drawing.Size(220, 21)
        Me.ckbx_startOnBoot.TabIndex = 13
        Me.ckbx_startOnBoot.Text = "Start Backup Manager on boot"
        Me.ckbx_startOnBoot.UseVisualStyleBackColor = True
        '
        'nud_previousQuantity
        '
        Me.nud_previousQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_previousQuantity.Location = New System.Drawing.Point(178, 282)
        Me.nud_previousQuantity.Name = "nud_previousQuantity"
        Me.nud_previousQuantity.Size = New System.Drawing.Size(452, 23)
        Me.nud_previousQuantity.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(175, 259)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(455, 20)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Amount of previous versions to keep:"
        '
        'tmr_main
        '
        Me.tmr_main.Interval = 60000
        '
        'nud_minutesPast
        '
        Me.nud_minutesPast.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_minutesPast.Location = New System.Drawing.Point(178, 233)
        Me.nud_minutesPast.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.nud_minutesPast.Name = "nud_minutesPast"
        Me.nud_minutesPast.Size = New System.Drawing.Size(452, 23)
        Me.nud_minutesPast.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(175, 210)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(455, 20)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Minutes past the hour to run backup:"
        '
        'btn_startBackup
        '
        Me.btn_startBackup.Enabled = False
        Me.btn_startBackup.Location = New System.Drawing.Point(424, 337)
        Me.btn_startBackup.Name = "btn_startBackup"
        Me.btn_startBackup.Size = New System.Drawing.Size(100, 70)
        Me.btn_startBackup.TabIndex = 18
        Me.btn_startBackup.Text = "Start Backup Now"
        Me.btn_startBackup.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 419)
        Me.Controls.Add(Me.btn_startBackup)
        Me.Controls.Add(Me.nud_minutesPast)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.nud_previousQuantity)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ckbx_startOnBoot)
        Me.Controls.Add(Me.nud_interval)
        Me.Controls.Add(Me.btn_saveChanges)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbx_bkupLoc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbx_originalLoc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbx_bkupName)
        Me.Controls.Add(Me.btn_new)
        Me.Controls.Add(Me.lbx_backups)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.Text = "Backups"
        CType(Me.nud_interval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.notifyIcon_menustrip.ResumeLayout(False)
        CType(Me.nud_previousQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_minutesPast, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbx_backups As System.Windows.Forms.ListBox
    Friend WithEvents btn_new As System.Windows.Forms.Button
    Friend WithEvents tbx_bkupName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbx_originalLoc As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbx_bkupLoc As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btn_saveChanges As System.Windows.Forms.Button
    Friend WithEvents nud_interval As System.Windows.Forms.NumericUpDown
    Friend WithEvents secondThread As System.ComponentModel.BackgroundWorker
    Friend WithEvents notifyIcon_main As System.Windows.Forms.NotifyIcon
    Friend WithEvents notifyIcon_menustrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents exit_tool As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents startBackup_tool As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ckbx_startOnBoot As System.Windows.Forms.CheckBox
    Friend WithEvents nud_previousQuantity As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tmr_main As System.Windows.Forms.Timer
    Friend WithEvents nud_minutesPast As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_startBackup As System.Windows.Forms.Button

End Class
