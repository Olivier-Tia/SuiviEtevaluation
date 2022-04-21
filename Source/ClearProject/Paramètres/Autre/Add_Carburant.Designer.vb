<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Add_Carburant
    Inherits DevExpress.XtraEditors.XtraForm

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Add_Carburant))
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.PrixEnergie = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.btnModifier = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSupprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.NomEnergie = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.LgListSourceEnergie = New DevExpress.XtraGrid.GridControl()
        Me.ViewSourceEnergie = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemPictureEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PrixEnergie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NomEnergie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.LgListSourceEnergie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewSourceEnergie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.PrixEnergie)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.btnModifier)
        Me.PanelControl1.Controls.Add(Me.btnAnnuler)
        Me.PanelControl1.Controls.Add(Me.btnSupprimer)
        Me.PanelControl1.Controls.Add(Me.btnEnregistrer)
        Me.PanelControl1.Controls.Add(Me.NomEnergie)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(394, 155)
        Me.PanelControl1.TabIndex = 1
        '
        'PrixEnergie
        '
        Me.PrixEnergie.EditValue = "0"
        Me.PrixEnergie.Location = New System.Drawing.Point(13, 82)
        Me.PrixEnergie.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PrixEnergie.Name = "PrixEnergie"
        Me.PrixEnergie.Properties.Mask.EditMask = "n0"
        Me.PrixEnergie.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.PrixEnergie.Size = New System.Drawing.Size(364, 20)
        Me.PrixEnergie.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 10.25!)
        Me.LabelControl2.Location = New System.Drawing.Point(13, 60)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(70, 16)
        Me.LabelControl2.TabIndex = 10
        Me.LabelControl2.Text = "Prix Unitaire"
        '
        'btnModifier
        '
        Me.btnModifier.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.btnModifier.Location = New System.Drawing.Point(107, 112)
        Me.btnModifier.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnModifier.Name = "btnModifier"
        Me.btnModifier.Size = New System.Drawing.Size(86, 29)
        Me.btnModifier.TabIndex = 9
        Me.btnModifier.Text = "Modifier"
        '
        'btnAnnuler
        '
        Me.btnAnnuler.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_161
        Me.btnAnnuler.Location = New System.Drawing.Point(290, 112)
        Me.btnAnnuler.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(86, 29)
        Me.btnAnnuler.TabIndex = 4
        Me.btnAnnuler.Text = "Annuler"
        '
        'btnSupprimer
        '
        Me.btnSupprimer.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.btnSupprimer.Location = New System.Drawing.Point(201, 112)
        Me.btnSupprimer.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSupprimer.Name = "btnSupprimer"
        Me.btnSupprimer.Size = New System.Drawing.Size(86, 29)
        Me.btnSupprimer.TabIndex = 3
        Me.btnSupprimer.Text = "Supprimer"
        '
        'btnEnregistrer
        '
        Me.btnEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.btnEnregistrer.Location = New System.Drawing.Point(11, 112)
        Me.btnEnregistrer.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnEnregistrer.Name = "btnEnregistrer"
        Me.btnEnregistrer.Size = New System.Drawing.Size(86, 29)
        Me.btnEnregistrer.TabIndex = 2
        Me.btnEnregistrer.Text = "Enregistrer"
        '
        'NomEnergie
        '
        Me.NomEnergie.Location = New System.Drawing.Point(13, 34)
        Me.NomEnergie.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.NomEnergie.Name = "NomEnergie"
        Me.NomEnergie.Size = New System.Drawing.Size(364, 20)
        Me.NomEnergie.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 10.25!)
        Me.LabelControl1.Location = New System.Drawing.Point(13, 12)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(36, 16)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Libellé"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.LgListSourceEnergie)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 155)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(394, 251)
        Me.PanelControl2.TabIndex = 30
        '
        'LgListSourceEnergie
        '
        Me.LgListSourceEnergie.Cursor = System.Windows.Forms.Cursors.Default
        Me.LgListSourceEnergie.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LgListSourceEnergie.Location = New System.Drawing.Point(3, 6)
        Me.LgListSourceEnergie.MainView = Me.ViewSourceEnergie
        Me.LgListSourceEnergie.Name = "LgListSourceEnergie"
        Me.LgListSourceEnergie.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemPictureEdit1, Me.RepositoryItemPictureEdit2, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3})
        Me.LgListSourceEnergie.Size = New System.Drawing.Size(389, 245)
        Me.LgListSourceEnergie.TabIndex = 30
        Me.LgListSourceEnergie.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewSourceEnergie})
        '
        'ViewSourceEnergie
        '
        Me.ViewSourceEnergie.ActiveFilterEnabled = False
        Me.ViewSourceEnergie.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.ViewSourceEnergie.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewSourceEnergie.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.ViewSourceEnergie.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewSourceEnergie.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.Empty.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.EvenRow.BackColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.ViewSourceEnergie.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewSourceEnergie.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewSourceEnergie.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewSourceEnergie.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewSourceEnergie.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewSourceEnergie.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.ViewSourceEnergie.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewSourceEnergie.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.ViewSourceEnergie.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Navy
        Me.ViewSourceEnergie.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewSourceEnergie.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewSourceEnergie.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewSourceEnergie.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.ViewSourceEnergie.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewSourceEnergie.Appearance.GroupButton.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewSourceEnergie.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewSourceEnergie.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewSourceEnergie.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ViewSourceEnergie.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.ViewSourceEnergie.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.GroupPanel.Options.UseFont = True
        Me.ViewSourceEnergie.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.ViewSourceEnergie.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ViewSourceEnergie.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewSourceEnergie.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewSourceEnergie.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewSourceEnergie.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.ViewSourceEnergie.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.ViewSourceEnergie.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.ViewSourceEnergie.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewSourceEnergie.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.ViewSourceEnergie.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.Preview.BackColor2 = System.Drawing.Color.White
        Me.ViewSourceEnergie.Appearance.Preview.ForeColor = System.Drawing.Color.Navy
        Me.ViewSourceEnergie.Appearance.Preview.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.Preview.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.ViewSourceEnergie.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewSourceEnergie.Appearance.Row.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.Row.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.ViewSourceEnergie.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.ViewSourceEnergie.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.ViewSourceEnergie.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewSourceEnergie.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewSourceEnergie.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.ViewSourceEnergie.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewSourceEnergie.GridControl = Me.LgListSourceEnergie
        Me.ViewSourceEnergie.Name = "ViewSourceEnergie"
        Me.ViewSourceEnergie.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        Me.ViewSourceEnergie.OptionsPrint.AutoWidth = False
        Me.ViewSourceEnergie.OptionsPrint.PrintPreview = True
        Me.ViewSourceEnergie.OptionsSelection.EnableAppearanceHideSelection = False
        Me.ViewSourceEnergie.OptionsSelection.MultiSelect = True
        Me.ViewSourceEnergie.OptionsView.ColumnAutoWidth = False
        Me.ViewSourceEnergie.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewSourceEnergie.OptionsView.EnableAppearanceOddRow = True
        Me.ViewSourceEnergie.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewSourceEnergie.OptionsView.ShowGroupPanel = False
        Me.ViewSourceEnergie.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewSourceEnergie.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        '
        'RepositoryItemPictureEdit2
        '
        Me.RepositoryItemPictureEdit2.Name = "RepositoryItemPictureEdit2"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        '
        'Add_Carburant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 408)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Add_Carburant"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ajout de source d'énergie"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.PrixEnergie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NomEnergie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.LgListSourceEnergie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewSourceEnergie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PrixEnergie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnModifier As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSupprimer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents NomEnergie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LgListSourceEnergie As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewSourceEnergie As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemPictureEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
