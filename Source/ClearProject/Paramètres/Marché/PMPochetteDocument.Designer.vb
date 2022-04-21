<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PMPochetteDocument
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btRetour = New DevExpress.XtraEditors.SimpleButton()
        Me.BtModif = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.CmbPochette = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BtEnrg = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtLibDoc = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.dtPochetteDocument = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ModifierService = New System.Windows.Forms.ToolStripMenuItem()
        Me.SupprimerServiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewPochetteDocument = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.CmbPochette.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLibDoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.dtPochetteDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.ViewPochetteDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btRetour)
        Me.PanelControl1.Controls.Add(Me.BtModif)
        Me.PanelControl1.Controls.Add(Me.LabelControl5)
        Me.PanelControl1.Controls.Add(Me.CmbPochette)
        Me.PanelControl1.Controls.Add(Me.BtEnrg)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.TxtLibDoc)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(683, 262)
        Me.PanelControl1.TabIndex = 0
        '
        'btRetour
        '
        Me.btRetour.Image = Global.ClearProject.My.Resources.Resources.Return_16x16
        Me.btRetour.Location = New System.Drawing.Point(468, 198)
        Me.btRetour.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btRetour.Name = "btRetour"
        Me.btRetour.Size = New System.Drawing.Size(143, 50)
        Me.btRetour.TabIndex = 139
        Me.btRetour.Text = "Annuler"
        '
        'BtModif
        '
        Me.BtModif.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtModif.Appearance.Options.UseFont = True
        Me.BtModif.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.BtModif.Location = New System.Drawing.Point(283, 200)
        Me.BtModif.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.BtModif.Name = "BtModif"
        Me.BtModif.Size = New System.Drawing.Size(165, 48)
        Me.BtModif.TabIndex = 8
        Me.BtModif.Text = "Modifier"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(17, 11)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(82, 26)
        Me.LabelControl5.TabIndex = 13
        Me.LabelControl5.Text = "Pochette"
        '
        'CmbPochette
        '
        Me.CmbPochette.Location = New System.Drawing.Point(13, 41)
        Me.CmbPochette.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.CmbPochette.Name = "CmbPochette"
        Me.CmbPochette.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbPochette.Properties.Appearance.Options.UseFont = True
        Me.CmbPochette.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbPochette.Size = New System.Drawing.Size(656, 32)
        Me.CmbPochette.TabIndex = 0
        '
        'BtEnrg
        '
        Me.BtEnrg.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnrg.Appearance.Options.UseFont = True
        Me.BtEnrg.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.BtEnrg.Location = New System.Drawing.Point(103, 200)
        Me.BtEnrg.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.BtEnrg.Name = "BtEnrg"
        Me.BtEnrg.Size = New System.Drawing.Size(160, 48)
        Me.BtEnrg.TabIndex = 7
        Me.BtEnrg.Text = "Enregistrer"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(17, 90)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(164, 26)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Libellé document"
        '
        'TxtLibDoc
        '
        Me.TxtLibDoc.Location = New System.Drawing.Point(17, 120)
        Me.TxtLibDoc.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.TxtLibDoc.Name = "TxtLibDoc"
        Me.TxtLibDoc.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLibDoc.Properties.Appearance.Options.UseFont = True
        Me.TxtLibDoc.Size = New System.Drawing.Size(655, 32)
        Me.TxtLibDoc.TabIndex = 1
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.dtPochetteDocument)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 262)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(683, 511)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Documents"
        '
        'dtPochetteDocument
        '
        Me.dtPochetteDocument.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dtPochetteDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtPochetteDocument.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dtPochetteDocument.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtPochetteDocument.Location = New System.Drawing.Point(2, 33)
        Me.dtPochetteDocument.MainView = Me.ViewPochetteDocument
        Me.dtPochetteDocument.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.dtPochetteDocument.Name = "dtPochetteDocument"
        Me.dtPochetteDocument.Size = New System.Drawing.Size(679, 476)
        Me.dtPochetteDocument.TabIndex = 10
        Me.dtPochetteDocument.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewPochetteDocument})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModifierService, Me.SupprimerServiceToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(182, 72)
        '
        'ModifierService
        '
        Me.ModifierService.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.ModifierService.Name = "ModifierService"
        Me.ModifierService.Size = New System.Drawing.Size(181, 34)
        Me.ModifierService.Text = "Modifier"
        Me.ModifierService.Visible = False
        '
        'SupprimerServiceToolStripMenuItem
        '
        Me.SupprimerServiceToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerServiceToolStripMenuItem.Name = "SupprimerServiceToolStripMenuItem"
        Me.SupprimerServiceToolStripMenuItem.Size = New System.Drawing.Size(181, 34)
        Me.SupprimerServiceToolStripMenuItem.Text = "Supprimer"
        '
        'ViewPochetteDocument
        '
        Me.ViewPochetteDocument.ActiveFilterEnabled = False
        Me.ViewPochetteDocument.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewPochetteDocument.Appearance.Row.Options.UseFont = True
        Me.ViewPochetteDocument.GridControl = Me.dtPochetteDocument
        Me.ViewPochetteDocument.Name = "ViewPochetteDocument"
        Me.ViewPochetteDocument.OptionsBehavior.Editable = False
        Me.ViewPochetteDocument.OptionsBehavior.ReadOnly = True
        Me.ViewPochetteDocument.OptionsCustomization.AllowColumnMoving = False
        Me.ViewPochetteDocument.OptionsCustomization.AllowFilter = False
        Me.ViewPochetteDocument.OptionsCustomization.AllowGroup = False
        Me.ViewPochetteDocument.OptionsCustomization.AllowSort = False
        Me.ViewPochetteDocument.OptionsFilter.AllowFilterEditor = False
        Me.ViewPochetteDocument.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewPochetteDocument.OptionsPrint.AutoWidth = False
        Me.ViewPochetteDocument.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewPochetteDocument.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewPochetteDocument.OptionsView.ColumnAutoWidth = False
        Me.ViewPochetteDocument.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewPochetteDocument.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewPochetteDocument.OptionsView.ShowGroupPanel = False
        Me.ViewPochetteDocument.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewPochetteDocument.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'GRHPochetteDocument
        '
        Me.AcceptButton = Me.BtEnrg
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(683, 773)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GRHPochetteDocument"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Document de pochette"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.CmbPochette.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLibDoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.dtPochetteDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.ViewPochetteDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtEnrg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtLibDoc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtPochetteDocument As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewPochetteDocument As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ModifierService As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CmbPochette As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents SupprimerServiceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BtModif As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btRetour As DevExpress.XtraEditors.SimpleButton
End Class
