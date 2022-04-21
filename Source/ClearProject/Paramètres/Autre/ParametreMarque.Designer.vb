<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParametreMarque
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
        Me.txtlibelle = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.LgListMarque = New DevExpress.XtraGrid.GridControl()
        Me.ViewMarque = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemPictureEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtlibelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.LgListMarque, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewMarque, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.PanelControl1.Controls.Add(Me.txtlibelle)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(394, 127)
        Me.PanelControl1.TabIndex = 31
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
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.LgListMarque)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 127)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(394, 280)
        Me.PanelControl2.TabIndex = 35
        '
        'LgListMarque
        '
        Me.LgListMarque.Cursor = System.Windows.Forms.Cursors.Default
        Me.LgListMarque.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LgListMarque.Location = New System.Drawing.Point(3, 16)
        Me.LgListMarque.MainView = Me.ViewMarque
        Me.LgListMarque.Name = "LgListMarque"
        Me.LgListMarque.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemPictureEdit1, Me.RepositoryItemPictureEdit2, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3})
        Me.LgListMarque.Size = New System.Drawing.Size(389, 264)
        Me.LgListMarque.TabIndex = 35
        Me.LgListMarque.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewMarque})
        '
        'ViewMarque
        '
        Me.ViewMarque.ActiveFilterEnabled = False
        Me.ViewMarque.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewMarque.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.ViewMarque.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewMarque.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewMarque.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewMarque.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewMarque.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ViewMarque.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewMarque.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.ViewMarque.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewMarque.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewMarque.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewMarque.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewMarque.Appearance.Empty.Options.UseBackColor = True
        Me.ViewMarque.Appearance.EvenRow.BackColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.ViewMarque.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewMarque.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewMarque.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewMarque.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewMarque.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewMarque.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(154, Byte), Integer))
        Me.ViewMarque.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewMarque.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewMarque.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewMarque.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewMarque.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewMarque.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewMarque.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewMarque.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewMarque.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.ViewMarque.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.ViewMarque.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewMarque.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewMarque.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.ViewMarque.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewMarque.Appearance.FocusedCell.BackColor = System.Drawing.Color.Navy
        Me.ViewMarque.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White
        Me.ViewMarque.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewMarque.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewMarque.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.ViewMarque.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Navy
        Me.ViewMarque.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewMarque.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewMarque.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewMarque.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewMarque.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewMarque.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewMarque.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewMarque.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.ViewMarque.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewMarque.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewMarque.Appearance.GroupButton.Options.UseForeColor = True
        Me.ViewMarque.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewMarque.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ViewMarque.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewMarque.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewMarque.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewMarque.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewMarque.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ViewMarque.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewMarque.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ViewMarque.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.ViewMarque.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewMarque.Appearance.GroupPanel.Options.UseFont = True
        Me.ViewMarque.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewMarque.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.ViewMarque.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ViewMarque.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewMarque.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewMarque.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.ViewMarque.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewMarque.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewMarque.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewMarque.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewMarque.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewMarque.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.ViewMarque.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ViewMarque.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewMarque.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewMarque.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewMarque.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.ViewMarque.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.ViewMarque.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewMarque.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.ViewMarque.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewMarque.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewMarque.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ViewMarque.Appearance.Preview.BackColor2 = System.Drawing.Color.White
        Me.ViewMarque.Appearance.Preview.ForeColor = System.Drawing.Color.Navy
        Me.ViewMarque.Appearance.Preview.Options.UseBackColor = True
        Me.ViewMarque.Appearance.Preview.Options.UseForeColor = True
        Me.ViewMarque.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.ViewMarque.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewMarque.Appearance.Row.Options.UseBackColor = True
        Me.ViewMarque.Appearance.Row.Options.UseForeColor = True
        Me.ViewMarque.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.ViewMarque.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.ViewMarque.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewMarque.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.ViewMarque.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.ViewMarque.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewMarque.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewMarque.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.ViewMarque.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewMarque.GridControl = Me.LgListMarque
        Me.ViewMarque.Name = "ViewMarque"
        Me.ViewMarque.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        Me.ViewMarque.OptionsPrint.AutoWidth = False
        Me.ViewMarque.OptionsPrint.PrintPreview = True
        Me.ViewMarque.OptionsSelection.EnableAppearanceHideSelection = False
        Me.ViewMarque.OptionsSelection.MultiSelect = True
        Me.ViewMarque.OptionsView.ColumnAutoWidth = False
        Me.ViewMarque.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewMarque.OptionsView.EnableAppearanceOddRow = True
        Me.ViewMarque.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewMarque.OptionsView.ShowGroupPanel = False
        Me.ViewMarque.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewMarque.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
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
        'ParametreMarque
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 408)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ParametreMarque"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Marque"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.txtlibelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.LgListMarque, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewMarque, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtlibelle As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LgListMarque As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewMarque As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemPictureEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
