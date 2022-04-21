<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ParametreCouleur
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
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.LgListCouleur = New DevExpress.XtraGrid.GridControl()
        Me.ViewCouleur = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemPictureEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnModifier = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSupprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.txtlibelle = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.LgListCouleur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewCouleur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtlibelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.LgListCouleur)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 127)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(394, 280)
        Me.PanelControl2.TabIndex = 37
        '
        'LgListCouleur
        '
        Me.LgListCouleur.Cursor = System.Windows.Forms.Cursors.Default
        Me.LgListCouleur.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LgListCouleur.Location = New System.Drawing.Point(3, 16)
        Me.LgListCouleur.MainView = Me.ViewCouleur
        Me.LgListCouleur.Name = "LgListCouleur"
        Me.LgListCouleur.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemPictureEdit1, Me.RepositoryItemPictureEdit2, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3})
        Me.LgListCouleur.Size = New System.Drawing.Size(389, 264)
        Me.LgListCouleur.TabIndex = 35
        Me.LgListCouleur.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewCouleur})
        '
        'ViewCouleur
        '
        Me.ViewCouleur.ActiveFilterEnabled = False
        Me.ViewCouleur.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewCouleur.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.ViewCouleur.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewCouleur.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewCouleur.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ViewCouleur.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewCouleur.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.ViewCouleur.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewCouleur.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewCouleur.Appearance.Empty.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.EvenRow.BackColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.ViewCouleur.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewCouleur.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewCouleur.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewCouleur.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.ViewCouleur.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewCouleur.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewCouleur.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewCouleur.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewCouleur.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewCouleur.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewCouleur.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewCouleur.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.ViewCouleur.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.FocusedCell.BackColor = System.Drawing.Color.Navy
        Me.ViewCouleur.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.ViewCouleur.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Navy
        Me.ViewCouleur.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewCouleur.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewCouleur.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.ViewCouleur.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewCouleur.Appearance.GroupButton.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewCouleur.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewCouleur.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewCouleur.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewCouleur.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewCouleur.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ViewCouleur.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.GroupPanel.Options.UseFont = True
        Me.ViewCouleur.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.ViewCouleur.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ViewCouleur.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ViewCouleur.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewCouleur.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewCouleur.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewCouleur.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.ViewCouleur.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewCouleur.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewCouleur.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.ViewCouleur.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ViewCouleur.Appearance.Preview.BackColor2 = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.Preview.ForeColor = System.Drawing.Color.Navy
        Me.ViewCouleur.Appearance.Preview.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.Preview.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewCouleur.Appearance.Row.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.Row.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewCouleur.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.ViewCouleur.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.ViewCouleur.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewCouleur.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewCouleur.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.ViewCouleur.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewCouleur.GridControl = Me.LgListCouleur
        Me.ViewCouleur.Name = "ViewCouleur"
        Me.ViewCouleur.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        Me.ViewCouleur.OptionsPrint.AutoWidth = False
        Me.ViewCouleur.OptionsPrint.PrintPreview = True
        Me.ViewCouleur.OptionsSelection.EnableAppearanceHideSelection = False
        Me.ViewCouleur.OptionsSelection.MultiSelect = True
        Me.ViewCouleur.OptionsView.ColumnAutoWidth = False
        Me.ViewCouleur.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewCouleur.OptionsView.EnableAppearanceOddRow = True
        Me.ViewCouleur.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewCouleur.OptionsView.ShowGroupPanel = False
        Me.ViewCouleur.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewCouleur.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
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
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnModifier)
        Me.PanelControl1.Controls.Add(Me.btnAnnuler)
        Me.PanelControl1.Controls.Add(Me.btnSupprimer)
        Me.PanelControl1.Controls.Add(Me.btnEnregistrer)
        Me.PanelControl1.Controls.Add(Me.txtlibelle)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(394, 127)
        Me.PanelControl1.TabIndex = 36
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
        'txtlibelle
        '
        Me.txtlibelle.Location = New System.Drawing.Point(13, 34)
        Me.txtlibelle.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtlibelle.Name = "txtlibelle"
        Me.txtlibelle.Size = New System.Drawing.Size(364, 20)
        Me.txtlibelle.TabIndex = 0
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
        'ParametreCouleur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 408)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ParametreCouleur"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Couleur"
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.LgListCouleur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewCouleur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.txtlibelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LgListCouleur As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewCouleur As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemPictureEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnModifier As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSupprimer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtlibelle As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
