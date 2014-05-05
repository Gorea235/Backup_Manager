<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class backupResultsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(backupResultsForm))
        Me.dgv_results = New System.Windows.Forms.DataGridView()
        Me.backup_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.backup_files_found = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.backup_file_find_time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.backup_files_time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.backup_skips = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.backup_copies = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.backup_errors = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgv_results, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_results
        '
        Me.dgv_results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_results.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.backup_name, Me.backup_files_found, Me.backup_file_find_time, Me.backup_files_time, Me.backup_skips, Me.backup_copies, Me.backup_errors})
        Me.dgv_results.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_results.Location = New System.Drawing.Point(0, 0)
        Me.dgv_results.Name = "dgv_results"
        Me.dgv_results.Size = New System.Drawing.Size(619, 282)
        Me.dgv_results.TabIndex = 0
        '
        'backup_name
        '
        Me.backup_name.HeaderText = "Backup Name"
        Me.backup_name.Name = "backup_name"
        Me.backup_name.ReadOnly = True
        '
        'backup_files_found
        '
        Me.backup_files_found.HeaderText = "Files Found"
        Me.backup_files_found.Name = "backup_files_found"
        Me.backup_files_found.ReadOnly = True
        '
        'backup_file_find_time
        '
        Me.backup_file_find_time.HeaderText = "File Find Time (s)"
        Me.backup_file_find_time.Name = "backup_file_find_time"
        Me.backup_file_find_time.ReadOnly = True
        '
        'backup_files_time
        '
        Me.backup_files_time.HeaderText = "File Backup Time (s)"
        Me.backup_files_time.Name = "backup_files_time"
        Me.backup_files_time.ReadOnly = True
        '
        'backup_skips
        '
        Me.backup_skips.HeaderText = "Skips"
        Me.backup_skips.Name = "backup_skips"
        Me.backup_skips.ReadOnly = True
        '
        'backup_copies
        '
        Me.backup_copies.HeaderText = "Copies"
        Me.backup_copies.Name = "backup_copies"
        Me.backup_copies.ReadOnly = True
        '
        'backup_errors
        '
        Me.backup_errors.HeaderText = "Errors (See log for details)"
        Me.backup_errors.Name = "backup_errors"
        Me.backup_errors.ReadOnly = True
        '
        'backupResultsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(619, 282)
        Me.Controls.Add(Me.dgv_results)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "backupResultsForm"
        Me.Text = "Form5"
        CType(Me.dgv_results, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgv_results As System.Windows.Forms.DataGridView
    Friend WithEvents backup_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents backup_files_found As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents backup_file_find_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents backup_files_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents backup_skips As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents backup_copies As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents backup_errors As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
