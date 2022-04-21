<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Courrier_TypeEtItineraire
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GridType = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip()
        Me.ModifierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SupprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PnlNewType = New DevExpress.XtraEditors.PanelControl()
        Me.TxtType = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GridTraitement = New DevExpress.XtraGrid.GridControl()
        Me.ViewTraitement = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PnlControl = New DevExpress.XtraEditors.PanelControl()
        Me.BtSupp = New DevExpress.XtraEditors.SimpleButton()
        Me.BtBas = New DevExpress.XtraEditors.SimpleButton()
        Me.BtHaut = New DevExpress.XtraEditors.SimpleButton()
        Me.PnlNewItiner = New DevExpress.XtraEditors.PanelControl()
        Me.CmbDelai = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtNumDelai = New DevExpress.XtraEditors.TextEdit()
        Me.SimpleButton6 = New DevExpress.XtraEditors.SimpleButton()
        Me.BtAjoutTraitement = New DevExpress.XtraEditors.SimpleButton()
        Me.CmbTraiteur = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtTraitement = New DevExpress.XtraEditors.TextEdit()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.BtRetour = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnreg = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GridType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.ViewType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlNewType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlNewType.SuspendLayout()
        CType(Me.TxtType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GridTraitement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewTraitement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PnlControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlControl.SuspendLayout()
        CType(Me.PnlNewItiner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlNewItiner.SuspendLayout()
        CType(Me.CmbDelai.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtNumDelai.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbTraiteur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTraitement.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.GridType)
        Me.GroupControl1.Controls.Add(Me.PnlNewType)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(239, 421)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Type courrier"
        '
        'GridType
        '
        Me.GridType.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridType.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridType.Location = New System.Drawing.Point(2, 49)
        Me.GridType.MainView = Me.ViewType
        Me.GridType.Name = "GridType"
        Me.GridType.Size = New System.Drawing.Size(235, 370)
        Me.GridType.TabIndex = 6
        Me.GridType.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewType})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModifierToolStripMenuItem, Me.SupprimerToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(130, 48)
        '
        'ModifierToolStripMenuItem
        '
        Me.ModifierToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.ModifierToolStripMenuItem.Name = "ModifierToolStripMenuItem"
        Me.ModifierToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.ModifierToolStripMenuItem.Text = "Modifier"
        '
        'SupprimerToolStripMenuItem
        '
        Me.SupprimerToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerToolStripMenuItem.Name = "SupprimerToolStripMenuItem"
        Me.SupprimerToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.SupprimerToolStripMenuItem.Text = "Supprimer"
        '
        'ViewType
        '
        Me.ViewType.ActiveFilterEnabled = False
        Me.ViewType.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewType.Appearance.Row.Options.UseFont = True
        Me.ViewType.GridControl = Me.GridType
        Me.ViewType.Name = "ViewType"
        Me.ViewType.OptionsBehavior.Editable = False
        Me.ViewType.OptionsBehavior.ReadOnly = True
        Me.ViewType.OptionsCustomization.AllowColumnMoving = False
        Me.ViewType.OptionsCustomization.AllowFilter = False
        Me.ViewType.OptionsCustomization.AllowGroup = False
        Me.ViewType.OptionsCustomization.AllowSort = False
        Me.ViewType.OptionsFilter.AllowFilterEditor = False
        Me.ViewType.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewType.OptionsPrint.AutoWidth = False
        Me.ViewType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewType.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewType.OptionsView.ColumnAutoWidth = False
        Me.ViewType.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewType.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewType.OptionsView.ShowGroupPanel = False
        Me.ViewType.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewType.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'PnlNewType
        '
        Me.PnlNewType.Controls.Add(Me.TxtType)
        Me.PnlNewType.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlNewType.Location = New System.Drawing.Point(2, 23)
        Me.PnlNewType.Name = "PnlNewType"
        Me.PnlNewType.Size = New System.Drawing.Size(235, 26)
        Me.PnlNewType.TabIndex = 1
        '
        'TxtType
        '
        Me.TxtType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtType.Location = New System.Drawing.Point(2, 2)
        Me.TxtType.Name = "TxtType"
        Me.TxtType.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtType.Properties.Appearance.Options.UseFont = True
        Me.TxtType.Size = New System.Drawing.Size(231, 22)
        Me.TxtType.TabIndex = 0
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl2.Controls.Add(Me.GridTraitement)
        Me.GroupControl2.Controls.Add(Me.PnlControl)
        Me.GroupControl2.Controls.Add(Me.PnlNewItiner)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(239, 0)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(745, 421)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Itineraire courrier"
        '
        'GridTraitement
        '
        Me.GridTraitement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridTraitement.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridTraitement.Location = New System.Drawing.Point(2, 49)
        Me.GridTraitement.MainView = Me.ViewTraitement
        Me.GridTraitement.Name = "GridTraitement"
        Me.GridTraitement.Size = New System.Drawing.Size(713, 370)
        Me.GridTraitement.TabIndex = 6
        Me.GridTraitement.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewTraitement})
        '
        'ViewTraitement
        '
        Me.ViewTraitement.ActiveFilterEnabled = False
        Me.ViewTraitement.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewTraitement.Appearance.Row.Options.UseFont = True
        Me.ViewTraitement.GridControl = Me.GridTraitement
        Me.ViewTraitement.Name = "ViewTraitement"
        Me.ViewTraitement.OptionsBehavior.Editable = False
        Me.ViewTraitement.OptionsBehavior.ReadOnly = True
        Me.ViewTraitement.OptionsCustomization.AllowColumnMoving = False
        Me.ViewTraitement.OptionsCustomization.AllowFilter = False
        Me.ViewTraitement.OptionsCustomization.AllowGroup = False
        Me.ViewTraitement.OptionsCustomization.AllowSort = False
        Me.ViewTraitement.OptionsFilter.AllowFilterEditor = False
        Me.ViewTraitement.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewTraitement.OptionsPrint.AutoWidth = False
        Me.ViewTraitement.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewTraitement.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewTraitement.OptionsView.ColumnAutoWidth = False
        Me.ViewTraitement.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewTraitement.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewTraitement.OptionsView.ShowGroupPanel = False
        Me.ViewTraitement.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewTraitement.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'PnlControl
        '
        Me.PnlControl.Controls.Add(Me.BtSupp)
        Me.PnlControl.Controls.Add(Me.BtBas)
        Me.PnlControl.Controls.Add(Me.BtHaut)
        Me.PnlControl.Dock = System.Windows.Forms.DockStyle.Right
        Me.PnlControl.Location = New System.Drawing.Point(715, 49)
        Me.PnlControl.Name = "PnlControl"
        Me.PnlControl.Size = New System.Drawing.Size(28, 370)
        Me.PnlControl.TabIndex = 7
        '
        'BtSupp
        '
        Me.BtSupp.Image = Global.ClearProject.My.Resources.Resources.Delete_16x16
        Me.BtSupp.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtSupp.Location = New System.Drawing.Point(1, 120)
        Me.BtSupp.Name = "BtSupp"
        Me.BtSupp.Size = New System.Drawing.Size(26, 26)
        Me.BtSupp.TabIndex = 6
        '
        'BtBas
        '
        Me.BtBas.Image = Global.ClearProject.My.Resources.Resources.versLeBas
        Me.BtBas.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtBas.Location = New System.Drawing.Point(1, 170)
        Me.BtBas.Name = "BtBas"
        Me.BtBas.Size = New System.Drawing.Size(26, 26)
        Me.BtBas.TabIndex = 5
        '
        'BtHaut
        '
        Me.BtHaut.Image = Global.ClearProject.My.Resources.Resources.Previous_16x16
        Me.BtHaut.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtHaut.Location = New System.Drawing.Point(1, 69)
        Me.BtHaut.Name = "BtHaut"
        Me.BtHaut.Size = New System.Drawing.Size(26, 26)
        Me.BtHaut.TabIndex = 4
        '
        'PnlNewItiner
        '
        Me.PnlNewItiner.Controls.Add(Me.CmbDelai)
        Me.PnlNewItiner.Controls.Add(Me.TxtNumDelai)
        Me.PnlNewItiner.Controls.Add(Me.SimpleButton6)
        Me.PnlNewItiner.Controls.Add(Me.BtAjoutTraitement)
        Me.PnlNewItiner.Controls.Add(Me.CmbTraiteur)
        Me.PnlNewItiner.Controls.Add(Me.TxtTraitement)
        Me.PnlNewItiner.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlNewItiner.Enabled = False
        Me.PnlNewItiner.Location = New System.Drawing.Point(2, 23)
        Me.PnlNewItiner.Name = "PnlNewItiner"
        Me.PnlNewItiner.Size = New System.Drawing.Size(741, 26)
        Me.PnlNewItiner.TabIndex = 1
        '
        'CmbDelai
        '
        Me.CmbDelai.Location = New System.Drawing.Point(632, 2)
        Me.CmbDelai.Name = "CmbDelai"
        Me.CmbDelai.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbDelai.Properties.Appearance.Options.UseFont = True
        Me.CmbDelai.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbDelai.Properties.Items.AddRange(New Object() {"Heures", "Jours", "Semaines", "Mois"})
        Me.CmbDelai.Size = New System.Drawing.Size(81, 22)
        Me.CmbDelai.TabIndex = 4
        '
        'TxtNumDelai
        '
        Me.TxtNumDelai.Location = New System.Drawing.Point(584, 2)
        Me.TxtNumDelai.Name = "TxtNumDelai"
        Me.TxtNumDelai.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNumDelai.Properties.Appearance.Options.UseFont = True
        Me.TxtNumDelai.Properties.Appearance.Options.UseTextOptions = True
        Me.TxtNumDelai.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TxtNumDelai.Properties.Mask.BeepOnError = True
        Me.TxtNumDelai.Properties.Mask.EditMask = "n0"
        Me.TxtNumDelai.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TxtNumDelai.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.TxtNumDelai.Size = New System.Drawing.Size(42, 22)
        Me.TxtNumDelai.TabIndex = 3
        '
        'SimpleButton6
        '
        Me.SimpleButton6.Dock = System.Windows.Forms.DockStyle.Left
        Me.SimpleButton6.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.SimpleButton6.Location = New System.Drawing.Point(2, 2)
        Me.SimpleButton6.Name = "SimpleButton6"
        Me.SimpleButton6.Size = New System.Drawing.Size(14, 22)
        Me.SimpleButton6.TabIndex = 9
        '
        'BtAjoutTraitement
        '
        Me.BtAjoutTraitement.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtAjoutTraitement.Image = Global.ClearProject.My.Resources.Resources.Add_16x16
        Me.BtAjoutTraitement.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAjoutTraitement.Location = New System.Drawing.Point(713, 2)
        Me.BtAjoutTraitement.Name = "BtAjoutTraitement"
        Me.BtAjoutTraitement.Size = New System.Drawing.Size(26, 22)
        Me.BtAjoutTraitement.TabIndex = 5
        '
        'CmbTraiteur
        '
        Me.CmbTraiteur.Location = New System.Drawing.Point(251, 2)
        Me.CmbTraiteur.Name = "CmbTraiteur"
        Me.CmbTraiteur.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbTraiteur.Properties.Appearance.Options.UseFont = True
        Me.CmbTraiteur.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbTraiteur.Size = New System.Drawing.Size(327, 22)
        Me.CmbTraiteur.TabIndex = 2
        '
        'TxtTraitement
        '
        Me.TxtTraitement.Location = New System.Drawing.Point(15, 2)
        Me.TxtTraitement.Name = "TxtTraitement"
        Me.TxtTraitement.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTraitement.Properties.Appearance.Options.UseFont = True
        Me.TxtTraitement.Size = New System.Drawing.Size(236, 22)
        Me.TxtTraitement.TabIndex = 1
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.BtRetour)
        Me.PanelControl3.Controls.Add(Me.BtEnreg)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl3.Location = New System.Drawing.Point(0, 421)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(984, 40)
        Me.PanelControl3.TabIndex = 2
        '
        'BtRetour
        '
        Me.BtRetour.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtRetour.Appearance.Options.UseFont = True
        Me.BtRetour.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtRetour.Enabled = False
        Me.BtRetour.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtRetour.Location = New System.Drawing.Point(2, 2)
        Me.BtRetour.Name = "BtRetour"
        Me.BtRetour.Size = New System.Drawing.Size(153, 36)
        Me.BtRetour.TabIndex = 8
        Me.BtRetour.Text = "ANNULER"
        '
        'BtEnreg
        '
        Me.BtEnreg.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnreg.Appearance.Options.UseFont = True
        Me.BtEnreg.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtEnreg.Enabled = False
        Me.BtEnreg.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnreg.Location = New System.Drawing.Point(810, 2)
        Me.BtEnreg.Name = "BtEnreg"
        Me.BtEnreg.Size = New System.Drawing.Size(172, 36)
        Me.BtEnreg.TabIndex = 7
        Me.BtEnreg.Text = "ENREGISTRER"
        '
        'Courrier_TypeEtItineraire
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 461)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.PanelControl3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Courrier_TypeEtItineraire"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Type et Itineraire Courrier"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GridType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.ViewType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlNewType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlNewType.ResumeLayout(False)
        CType(Me.TxtType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.GridTraitement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewTraitement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PnlControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlControl.ResumeLayout(False)
        CType(Me.PnlNewItiner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlNewItiner.ResumeLayout(False)
        CType(Me.CmbDelai.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtNumDelai.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbTraiteur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTraitement.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PnlNewType As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PnlNewItiner As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridType As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TxtType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridTraitement As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewTraitement As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CmbTraiteur As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TxtTraitement As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PnlControl As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtBas As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtHaut As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtSupp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtAjoutTraitement As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnreg As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton6 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtRetour As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CmbDelai As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TxtNumDelai As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SupprimerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModifierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
