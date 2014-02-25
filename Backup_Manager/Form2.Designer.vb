<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BackupAdder
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
        Me.nud_interval = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbx_bkupLoc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbx_originalLoc = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbx_bkupName = New System.Windows.Forms.TextBox()
        Me.btn_cancel = New System.Windows.Forms.Button()
        Me.btn_addBackup = New System.Windows.Forms.Button()
        Me.nud_minutesPast = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nud_previousQuantity = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btn_findOriginal = New System.Windows.Forms.Button()
        Me.btn_findBackup = New System.Windows.Forms.Button()
        Me.fbd_orignialLoc = New System.Windows.Forms.FolderBrowserDialog()
        Me.fbd_backupLoc = New System.Windows.Forms.FolderBrowserDialog()
        CType(Me.nud_interval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_minutesPast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_previousQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'nud_interval
        '
        Me.nud_interval.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_interval.Location = New System.Drawing.Point(15, 239)
        Me.nud_interval.Name = "nud_interval"
        Me.nud_interval.Size = New System.Drawing.Size(731, 23)
        Me.nud_interval.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 216)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(734, 20)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Backup Interval (hours):"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 138)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(734, 20)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Backup Location:"
        '
        'tbx_bkupLoc
        '
        Me.tbx_bkupLoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbx_bkupLoc.Location = New System.Drawing.Point(15, 161)
        Me.tbx_bkupLoc.Name = "tbx_bkupLoc"
        Me.tbx_bkupLoc.Size = New System.Drawing.Size(731, 23)
        Me.tbx_bkupLoc.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(734, 21)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Original Location:"
        '
        'tbx_originalLoc
        '
        Me.tbx_originalLoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbx_originalLoc.Location = New System.Drawing.Point(15, 83)
        Me.tbx_originalLoc.Name = "tbx_originalLoc"
        Me.tbx_originalLoc.Size = New System.Drawing.Size(731, 23)
        Me.tbx_originalLoc.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(734, 21)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Backup Name:"
        '
        'tbx_bkupName
        '
        Me.tbx_bkupName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbx_bkupName.Location = New System.Drawing.Point(15, 33)
        Me.tbx_bkupName.Name = "tbx_bkupName"
        Me.tbx_bkupName.Size = New System.Drawing.Size(731, 23)
        Me.tbx_bkupName.TabIndex = 12
        '
        'btn_cancel
        '
        Me.btn_cancel.Location = New System.Drawing.Point(383, 384)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(211, 50)
        Me.btn_cancel.TabIndex = 20
        Me.btn_cancel.Text = "Cancel"
        Me.btn_cancel.UseVisualStyleBackColor = True
        '
        'btn_addBackup
        '
        Me.btn_addBackup.Location = New System.Drawing.Point(166, 384)
        Me.btn_addBackup.Name = "btn_addBackup"
        Me.btn_addBackup.Size = New System.Drawing.Size(211, 50)
        Me.btn_addBackup.TabIndex = 21
        Me.btn_addBackup.Text = "Add Backup"
        Me.btn_addBackup.UseVisualStyleBackColor = True
        '
        'nud_minutesPast
        '
        Me.nud_minutesPast.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_minutesPast.Location = New System.Drawing.Point(15, 288)
        Me.nud_minutesPast.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.nud_minutesPast.Name = "nud_minutesPast"
        Me.nud_minutesPast.Size = New System.Drawing.Size(731, 23)
        Me.nud_minutesPast.TabIndex = 25
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 265)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(734, 20)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Minutes past the hour to run backup:"
        '
        'nud_previousQuantity
        '
        Me.nud_previousQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_previousQuantity.Location = New System.Drawing.Point(15, 337)
        Me.nud_previousQuantity.Name = "nud_previousQuantity"
        Me.nud_previousQuantity.Size = New System.Drawing.Size(731, 23)
        Me.nud_previousQuantity.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 314)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(734, 20)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Amount of previous versions to keep:"
        '
        'btn_findOriginal
        '
        Me.btn_findOriginal.Location = New System.Drawing.Point(622, 112)
        Me.btn_findOriginal.Name = "btn_findOriginal"
        Me.btn_findOriginal.Size = New System.Drawing.Size(124, 23)
        Me.btn_findOriginal.TabIndex = 26
        Me.btn_findOriginal.Text = "Browse"
        Me.btn_findOriginal.UseVisualStyleBackColor = True
        '
        'btn_findBackup
        '
        Me.btn_findBackup.Location = New System.Drawing.Point(622, 190)
        Me.btn_findBackup.Name = "btn_findBackup"
        Me.btn_findBackup.Size = New System.Drawing.Size(124, 23)
        Me.btn_findBackup.TabIndex = 27
        Me.btn_findBackup.Text = "Browse"
        Me.btn_findBackup.UseVisualStyleBackColor = True
        '
        'fbd_orignialLoc
        '
        Me.fbd_orignialLoc.Description = "Select the folder to backup"
        '
        'fbd_backupLoc
        '
        Me.fbd_backupLoc.Description = "Select the folder to place the back in"
        '
        'BackupAdder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(758, 446)
        Me.Controls.Add(Me.btn_findBackup)
        Me.Controls.Add(Me.btn_findOriginal)
        Me.Controls.Add(Me.nud_minutesPast)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.nud_previousQuantity)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btn_addBackup)
        Me.Controls.Add(Me.btn_cancel)
        Me.Controls.Add(Me.nud_interval)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbx_bkupLoc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbx_originalLoc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbx_bkupName)
        Me.Name = "BackupAdder"
        Me.Text = "Add New Backup"
        CType(Me.nud_interval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_minutesPast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_previousQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents nud_interval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbx_bkupLoc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbx_originalLoc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbx_bkupName As System.Windows.Forms.TextBox
    Friend WithEvents btn_cancel As System.Windows.Forms.Button
    Friend WithEvents btn_addBackup As System.Windows.Forms.Button
    Friend WithEvents nud_minutesPast As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents nud_previousQuantity As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btn_findOriginal As System.Windows.Forms.Button
    Friend WithEvents btn_findBackup As System.Windows.Forms.Button
    Friend WithEvents fbd_orignialLoc As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents fbd_backupLoc As System.Windows.Forms.FolderBrowserDialog
End Class
