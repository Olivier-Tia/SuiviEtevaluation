<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BonCommande
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
        Me.BtAnnuler = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.DateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Cmbct = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CmbService = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.TxtMarche = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CmbActivite = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TxtMontLettre = New System.Windows.Forms.TextBox()
        Me.TxtNewMont = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtPu = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtQte = New System.Windows.Forms.TextBox()
        Me.TxtDesignation = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.LgListBonCommande = New DevExpress.XtraGrid.GridControl()
        Me.ViewBonCommande = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.DateEdit1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmbct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbService.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.CmbActivite.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.LgListBonCommande, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewBonCommande, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtAnnuler
        '
        Me.BtAnnuler.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtAnnuler.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAnnuler.Location = New System.Drawing.Point(24, 653)
        Me.BtAnnuler.Margin = New System.Windows.Forms.Padding(5)
        Me.BtAnnuler.Name = "BtAnnuler"
        Me.BtAnnuler.Size = New System.Drawing.Size(208, 69)
        Me.BtAnnuler.TabIndex = 11
        Me.BtAnnuler.ToolTip = "Retour"
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnregistrer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtEnregistrer.Location = New System.Drawing.Point(266, 653)
        Me.BtEnregistrer.Margin = New System.Windows.Forms.Padding(5)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(225, 69)
        Me.BtEnregistrer.TabIndex = 10
        Me.BtEnregistrer.ToolTip = "Enregistrer"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.DateEdit1)
        Me.GroupControl1.Controls.Add(Me.Label12)
        Me.GroupControl1.Controls.Add(Me.Label11)
        Me.GroupControl1.Controls.Add(Me.Label10)
        Me.GroupControl1.Controls.Add(Me.Cmbct)
        Me.GroupControl1.Controls.Add(Me.CmbService)
        Me.GroupControl1.Controls.Add(Me.TextEdit1)
        Me.GroupControl1.Controls.Add(Me.TxtMarche)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Location = New System.Drawing.Point(13, 14)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(605, 739)
        Me.GroupControl1.TabIndex = 15
        Me.GroupControl1.Text = "Bon de commande"
        '
        'DateEdit1
        '
        Me.DateEdit1.EditValue = Nothing
        Me.DateEdit1.Location = New System.Drawing.Point(17, 88)
        Me.DateEdit1.Name = "DateEdit1"
        Me.DateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DateEdit1.Size = New System.Drawing.Size(580, 30)
        Me.DateEdit1.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(13, 50)
        Me.Label12.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 24)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Date"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 600)
        Me.Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(112, 24)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Fournisseur"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 513)
        Me.Label10.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(114, 24)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Demandeur"
        '
        'Cmbct
        '
        Me.Cmbct.Location = New System.Drawing.Point(11, 640)
        Me.Cmbct.Name = "Cmbct"
        Me.Cmbct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Cmbct.Size = New System.Drawing.Size(586, 30)
        Me.Cmbct.TabIndex = 8
        '
        'CmbService
        '
        Me.CmbService.Location = New System.Drawing.Point(11, 549)
        Me.CmbService.Name = "CmbService"
        Me.CmbService.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbService.Size = New System.Drawing.Size(586, 30)
        Me.CmbService.TabIndex = 7
        '
        'TextEdit1
        '
        Me.TextEdit1.Location = New System.Drawing.Point(216, 158)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Size = New System.Drawing.Size(381, 30)
        Me.TextEdit1.TabIndex = 6
        '
        'TxtMarche
        '
        Me.TxtMarche.Location = New System.Drawing.Point(11, 197)
        Me.TxtMarche.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtMarche.Multiline = True
        Me.TxtMarche.Name = "TxtMarche"
        Me.TxtMarche.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtMarche.Size = New System.Drawing.Size(586, 515)
        Me.TxtMarche.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 161)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(204, 24)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "N° Bon de commande"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.CmbActivite)
        Me.Panel1.Controls.Add(Me.BtAnnuler)
        Me.Panel1.Controls.Add(Me.BtEnregistrer)
        Me.Panel1.Controls.Add(Me.TxtMontLettre)
        Me.Panel1.Controls.Add(Me.TxtNewMont)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.TxtPu)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.TxtQte)
        Me.Panel1.Controls.Add(Me.TxtDesignation)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(628, 14)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(500, 739)
        Me.Panel1.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 8.142858!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 17)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 22)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Activité"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label9.Location = New System.Drawing.Point(16, 405)
        Me.Label9.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(159, 22)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Montant en lettres"
        '
        'CmbActivite
        '
        Me.CmbActivite.Location = New System.Drawing.Point(17, 42)
        Me.CmbActivite.Name = "CmbActivite"
        Me.CmbActivite.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CmbActivite.Size = New System.Drawing.Size(472, 30)
        Me.CmbActivite.TabIndex = 13
        '
        'TxtMontLettre
        '
        Me.TxtMontLettre.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.TxtMontLettre.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMontLettre.Location = New System.Drawing.Point(20, 433)
        Me.TxtMontLettre.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtMontLettre.Multiline = True
        Me.TxtMontLettre.Name = "TxtMontLettre"
        Me.TxtMontLettre.Size = New System.Drawing.Size(471, 204)
        Me.TxtMontLettre.TabIndex = 10
        '
        'TxtNewMont
        '
        Me.TxtNewMont.Font = New System.Drawing.Font("Tahoma", 8.142858!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNewMont.Location = New System.Drawing.Point(17, 325)
        Me.TxtNewMont.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtNewMont.Name = "TxtNewMont"
        Me.TxtNewMont.Size = New System.Drawing.Size(472, 30)
        Me.TxtNewMont.TabIndex = 9
        Me.TxtNewMont.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(160, 293)
        Me.Label6.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(217, 26)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "MONTANT A PAYER"
        '
        'TxtPu
        '
        Me.TxtPu.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.TxtPu.Location = New System.Drawing.Point(255, 221)
        Me.TxtPu.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtPu.Name = "TxtPu"
        Me.TxtPu.Size = New System.Drawing.Size(234, 30)
        Me.TxtPu.TabIndex = 7
        Me.TxtPu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label5.Location = New System.Drawing.Point(20, 193)
        Me.Label5.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 22)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Quantité"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label4.Location = New System.Drawing.Point(255, 193)
        Me.Label4.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(114, 22)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Prix unitaire"
        '
        'TxtQte
        '
        Me.TxtQte.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.TxtQte.Location = New System.Drawing.Point(20, 221)
        Me.TxtQte.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtQte.Name = "TxtQte"
        Me.TxtQte.Size = New System.Drawing.Size(212, 30)
        Me.TxtQte.TabIndex = 3
        Me.TxtQte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtDesignation
        '
        Me.TxtDesignation.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.TxtDesignation.Location = New System.Drawing.Point(20, 130)
        Me.TxtDesignation.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtDesignation.Name = "TxtDesignation"
        Me.TxtDesignation.Size = New System.Drawing.Size(472, 30)
        Me.TxtDesignation.TabIndex = 1
        Me.TxtDesignation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Italic)
        Me.Label2.Location = New System.Drawing.Point(20, 103)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 22)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Désignation"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.LgListBonCommande)
        Me.GroupControl3.Location = New System.Drawing.Point(1138, 14)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(512, 739)
        Me.GroupControl3.TabIndex = 23
        Me.GroupControl3.Text = "Historique des besoins"
        '
        'LgListBonCommande
        '
        Me.LgListBonCommande.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LgListBonCommande.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.LgListBonCommande.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LgListBonCommande.Location = New System.Drawing.Point(2, 31)
        Me.LgListBonCommande.MainView = Me.ViewBonCommande
        Me.LgListBonCommande.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.LgListBonCommande.Name = "LgListBonCommande"
        Me.LgListBonCommande.Size = New System.Drawing.Size(508, 706)
        Me.LgListBonCommande.TabIndex = 43
        Me.LgListBonCommande.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewBonCommande})
        '
        'ViewBonCommande
        '
        Me.ViewBonCommande.ActiveFilterEnabled = False
        Me.ViewBonCommande.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewBonCommande.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewBonCommande.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewBonCommande.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewBonCommande.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewBonCommande.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.ViewBonCommande.Appearance.Empty.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ViewBonCommande.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ViewBonCommande.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.EvenRow.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.EvenRow.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.EvenRow.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewBonCommande.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewBonCommande.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewBonCommande.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewBonCommande.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.FilterPanel.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.FilterPanel.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewBonCommande.Appearance.FixedLine.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.ViewBonCommande.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.FocusedCell.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.FocusedCell.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(177, Byte), Integer))
        Me.ViewBonCommande.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.ViewBonCommande.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewBonCommande.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ViewBonCommande.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.FooterPanel.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.FooterPanel.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.ViewBonCommande.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.ViewBonCommande.Appearance.GroupButton.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.GroupButton.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewBonCommande.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewBonCommande.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.GroupFooter.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.GroupFooter.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewBonCommande.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.ViewBonCommande.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.GroupPanel.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.GroupPanel.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewBonCommande.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.ViewBonCommande.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.GroupRow.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.GroupRow.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.GroupRow.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewBonCommande.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.ViewBonCommande.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.ViewBonCommande.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
        Me.ViewBonCommande.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ViewBonCommande.Appearance.HorzLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewBonCommande.Appearance.HorzLine.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.HorzLine.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewBonCommande.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewBonCommande.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.OddRow.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.OddRow.Options.UseBorderColor = True
        Me.ViewBonCommande.Appearance.OddRow.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ViewBonCommande.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.ViewBonCommande.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
        Me.ViewBonCommande.Appearance.Preview.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.Preview.Options.UseFont = True
        Me.ViewBonCommande.Appearance.Preview.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewBonCommande.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.Row.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.Row.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ViewBonCommande.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.ViewBonCommande.Appearance.RowSeparator.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ViewBonCommande.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
        Me.ViewBonCommande.Appearance.SelectedRow.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.SelectedRow.Options.UseForeColor = True
        Me.ViewBonCommande.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.ViewBonCommande.Appearance.TopNewRow.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ViewBonCommande.Appearance.VertLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ViewBonCommande.Appearance.VertLine.Options.UseBackColor = True
        Me.ViewBonCommande.Appearance.VertLine.Options.UseBorderColor = True
        Me.ViewBonCommande.GridControl = Me.LgListBonCommande
        Me.ViewBonCommande.Name = "ViewBonCommande"
        Me.ViewBonCommande.OptionsBehavior.Editable = False
        Me.ViewBonCommande.OptionsBehavior.ReadOnly = True
        Me.ViewBonCommande.OptionsCustomization.AllowColumnMoving = False
        Me.ViewBonCommande.OptionsCustomization.AllowFilter = False
        Me.ViewBonCommande.OptionsCustomization.AllowGroup = False
        Me.ViewBonCommande.OptionsCustomization.AllowSort = False
        Me.ViewBonCommande.OptionsFilter.AllowFilterEditor = False
        Me.ViewBonCommande.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewBonCommande.OptionsPrint.AutoWidth = False
        Me.ViewBonCommande.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewBonCommande.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewBonCommande.OptionsView.ColumnAutoWidth = False
        Me.ViewBonCommande.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewBonCommande.OptionsView.EnableAppearanceOddRow = True
        Me.ViewBonCommande.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewBonCommande.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewBonCommande.OptionsView.ShowGroupPanel = False
        Me.ViewBonCommande.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewBonCommande.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'BonCommande
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1655, 760)
        Me.Controls.Add(Me.GroupControl3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BonCommande"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bon de commande"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.DateEdit1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmbct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbService.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.CmbActivite.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.LgListBonCommande, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewBonCommande, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtAnnuler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TxtMarche As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtMontLettre As System.Windows.Forms.TextBox
    Friend WithEvents TxtNewMont As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtPu As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtQte As System.Windows.Forms.TextBox
    Friend WithEvents TxtDesignation As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents DateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Cmbct As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CmbService As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CmbActivite As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LgListBonCommande As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewBonCommande As DevExpress.XtraGrid.Views.Grid.GridView
End Class
