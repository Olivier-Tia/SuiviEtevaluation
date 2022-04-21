<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ArchivesRSM
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ArchivesRSM))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LstRapport = New System.Windows.Forms.ListView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GridPart = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtImprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.BtFermer = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridPart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LstRapport
        '
        Me.LstRapport.Dock = System.Windows.Forms.DockStyle.Left
        Me.LstRapport.LargeImageList = Me.ImageList1
        Me.LstRapport.Location = New System.Drawing.Point(0, 0)
        Me.LstRapport.Name = "LstRapport"
        Me.LstRapport.Size = New System.Drawing.Size(423, 290)
        Me.LstRapport.TabIndex = 1
        Me.LstRapport.UseCompatibleStateImageBehavior = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Ribbon_OPEN_32x32.png")
        '
        'GridPart
        '
        Me.GridPart.AllowUserToAddRows = False
        Me.GridPart.AllowUserToDeleteRows = False
        Me.GridPart.BackgroundColor = System.Drawing.Color.White
        Me.GridPart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridPart.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridPart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridPart.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.GridPart.Location = New System.Drawing.Point(423, 0)
        Me.GridPart.Name = "GridPart"
        Me.GridPart.ReadOnly = True
        Me.GridPart.RowHeadersVisible = False
        Me.GridPart.Size = New System.Drawing.Size(105, 226)
        Me.GridPart.TabIndex = 2
        '
        'Column1
        '
        Me.Column1.HeaderText = "*"
        Me.Column1.MinimumWidth = 2
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 2
        '
        'Column2
        '
        Me.Column2.HeaderText = "PART"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'BtImprimer
        '
        Me.BtImprimer.Location = New System.Drawing.Point(423, 228)
        Me.BtImprimer.Name = "BtImprimer"
        Me.BtImprimer.Size = New System.Drawing.Size(104, 30)
        Me.BtImprimer.TabIndex = 3
        Me.BtImprimer.Text = "Imprimer"
        '
        'BtFermer
        '
        Me.BtFermer.Location = New System.Drawing.Point(423, 259)
        Me.BtFermer.Name = "BtFermer"
        Me.BtFermer.Size = New System.Drawing.Size(104, 30)
        Me.BtFermer.TabIndex = 4
        Me.BtFermer.Text = "Quitter"
        '
        'ArchivesRSM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(528, 290)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtFermer)
        Me.Controls.Add(Me.BtImprimer)
        Me.Controls.Add(Me.GridPart)
        Me.Controls.Add(Me.LstRapport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "ArchivesRSM"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RSM EDITES"
        Me.TopMost = True
        CType(Me.GridPart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LstRapport As System.Windows.Forms.ListView
    Friend WithEvents GridPart As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtImprimer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtFermer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
End Class
