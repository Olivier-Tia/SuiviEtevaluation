<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParametreAssurance
    Inherits DevExpress.XtraEditors.XtraForm

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnModifier = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSupprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.txtLibelle = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.LgListAssurance = New DevExpress.XtraGrid.GridControl()
        Me.ViewAssurance = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemPictureEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtLibelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.LgListAssurance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewAssurance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnModifier)
        Me.PanelControl1.Controls.Add(Me.btnAnnuler)
        Me.PanelControl1.Controls.Add(Me.btnSupprimer)
        Me.PanelControl1.Controls.Add(Me.btnEnregistrer)
        Me.PanelControl1.Controls.Add(Me.txtLibelle)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(394, 127)
        Me.PanelControl1.TabIndex = 33
        '
        'btnModifier
        '
        Me.btnModifier.Image = Global.ClearProject.My.Resources.Resources.Edit_16x16
        Me.btnModifier.Location = New System.Drawing.Point(107, 77)
        Me.btnModifier.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnModifier.Name = "btnModifier"
        Me.btnModifier.Size = New System.Drawing.Size(86, 29)
        Me.btnModifier.TabIndex = 9
        Me.btnModifier.Text = "Modifier"
        '
        'btnAnnuler
        '
        Me.btnAnnuler.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_161
        Me.btnAnnuler.Location = New System.Drawing.Point(290, 77)
        Me.btnAnnuler.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(86, 29)
        Me.btnAnnuler.TabIndex = 4
        Me.btnAnnuler.Text = "Annuler"
        '
        'btnSupprimer
        '
        Me.btnSupprimer.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.btnSupprimer.Location = New System.Drawing.Point(201, 77)
        Me.btnSupprimer.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSupprimer.Name = "btnSupprimer"
        Me.btnSupprimer.Size = New System.Drawing.Size(86, 29)
        Me.btnSupprimer.TabIndex = 3
        Me.btnSupprimer.Text = "Supprimer"
        '
        'btnEnregistrer
        '
        Me.btnEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_16x16
        Me.btnEnregistrer.Location = New System.Drawing.Point(11, 77)
        Me.btnEnregistrer.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnEnregistrer.Name = "btnEnregistrer"
        Me.btnEnregistrer.Size = New System.Drawing.Size(86, 29)
        Me.btnEnregistrer.TabIndex = 2
        Me.btnEnregistrer.Text = "Enregistrer"
        '
        'txtLibelle
        '
        Me.txtLibelle.Location = New System.Drawing.Point(13, 34)
        Me.txtLibelle.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtLibelle.Name = "txtLibelle"
        Me.txtLibelle.Size = New System.Drawing.Size(364, 20)
        Me.txtLibelle.TabIndex = 0
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
        Me.PanelControl2.Controls.Add(Me.LgListAssurance)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 127)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(394, 280)
        Me.PanelControl2.TabIndex = 36
        '
        'LgListAssurance
        '
        Me.LgListAssurance.Cursor = System.Windows.Forms.Cursors.Default
        Me.LgListAssurance.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LgListAssurance.Location = New System.Drawing.Point(3, 6)
        Me.LgListAssurance.MainView = Me.ViewAssurance
        Me.LgListAssurance.Name = "LgListAssurance"
        Me.LgListAssurance.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemPictureEdit1, Me.RepositoryItemPictureEdit2, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3})
        Me.LgListAssurance.Size = New System.Drawing.Size(389, 274)
        Me.LgListAssurance.TabIndex = 35
        Me.LgListAssurance.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewAssurance})
        '
        'ViewAssurance
        '
        Me.ViewAssurance.ActiveFilterEnabled = False
        Me.ViewAssurance.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewAssurance.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.ViewAssurance.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewAssurance.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewAssurance.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ViewAssurance.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewAssurance.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.ViewAssurance.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewAssurance.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewAssurance.Appearance.Empty.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.EvenRow.BackColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.ViewAssurance.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewAssurance.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewAssurance.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewAssurance.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.ViewAssurance.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewAssurance.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewAssurance.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewAssurance.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewAssurance.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewAssurance.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewAssurance.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewAssurance.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.ViewAssurance.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.FocusedCell.BackColor = System.Drawing.Color.Navy
        Me.ViewAssurance.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.ViewAssurance.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Navy
        Me.ViewAssurance.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewAssurance.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewAssurance.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.ViewAssurance.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewAssurance.Appearance.GroupButton.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewAssurance.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewAssurance.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewAssurance.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewAssurance.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewAssurance.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ViewAssurance.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.GroupPanel.Options.UseFont = True
        Me.ViewAssurance.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.ViewAssurance.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ViewAssurance.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ViewAssurance.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewAssurance.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewAssurance.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewAssurance.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.ViewAssurance.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewAssurance.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewAssurance.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.ViewAssurance.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ViewAssurance.Appearance.Preview.BackColor2 = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.Preview.ForeColor = System.Drawing.Color.Navy
        Me.ViewAssurance.Appearance.Preview.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.Preview.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewAssurance.Appearance.Row.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.Row.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewAssurance.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.ViewAssurance.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.ViewAssurance.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewAssurance.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewAssurance.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.ViewAssurance.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewAssurance.GridControl = Me.LgListAssurance
        Me.ViewAssurance.Name = "ViewAssurance"
        Me.ViewAssurance.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        Me.ViewAssurance.OptionsPrint.AutoWidth = False
        Me.ViewAssurance.OptionsPrint.PrintPreview = True
        Me.ViewAssurance.OptionsSelection.EnableAppearanceHideSelection = False
        Me.ViewAssurance.OptionsSelection.MultiSelect = True
        Me.ViewAssurance.OptionsView.ColumnAutoWidth = False
        Me.ViewAssurance.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewAssurance.OptionsView.EnableAppearanceOddRow = True
        Me.ViewAssurance.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewAssurance.OptionsView.ShowGroupPanel = False
        Me.ViewAssurance.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewAssurance.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
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
        'ParametreAssurance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 408)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ParametreAssurance"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Assurance"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.txtLibelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.LgListAssurance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewAssurance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnModifier As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSupprimer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtLibelle As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LgListAssurance As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewAssurance As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemPictureEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
