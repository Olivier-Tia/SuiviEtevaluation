<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SourceFinancement
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtAjouter = New DevExpress.XtraEditors.SimpleButton()
        Me.BtModifier = New DevExpress.XtraEditors.SimpleButton()
        Me.BtSupprimer = New DevExpress.XtraEditors.SimpleButton()
        Me.BtRetour = New DevExpress.XtraEditors.SimpleButton()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TxtNumConvention = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtSource = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtRecherche = New System.Windows.Forms.Button()
        Me.CmbType = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtPieceJointe = New System.Windows.Forms.TextBox()
        Me.TxtCFA = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtLIVRE = New System.Windows.Forms.TextBox()
        Me.DTDateSignature = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TxtUS = New System.Windows.Forms.TextBox()
        Me.DTEntreeVigueur = New System.Windows.Forms.DateTimePicker()
        Me.TxtEuro = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TxtChemin = New System.Windows.Forms.TextBox()
        Me.DTCloture = New System.Windows.Forms.DateTimePicker()
        Me.TxtCodeSource = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.CmbSource = New System.Windows.Forms.ComboBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GridConvention = New System.Windows.Forms.DataGridView()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.Bailleur = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GridConvention, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtAjouter
        '
        Me.BtAjouter.Image = Global.ClearProject.My.Resources.Resources.ajouter
        Me.BtAjouter.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtAjouter.Location = New System.Drawing.Point(758, 5)
        Me.BtAjouter.Margin = New System.Windows.Forms.Padding(5)
        Me.BtAjouter.Name = "BtAjouter"
        Me.BtAjouter.Size = New System.Drawing.Size(65, 69)
        Me.BtAjouter.TabIndex = 9
        Me.BtAjouter.ToolTip = "Nouveau"
        '
        'BtModifier
        '
        Me.BtModifier.Image = Global.ClearProject.My.Resources.Resources.Edit_32x32
        Me.BtModifier.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtModifier.Location = New System.Drawing.Point(828, 5)
        Me.BtModifier.Margin = New System.Windows.Forms.Padding(5)
        Me.BtModifier.Name = "BtModifier"
        Me.BtModifier.Size = New System.Drawing.Size(65, 69)
        Me.BtModifier.TabIndex = 8
        Me.BtModifier.ToolTip = "Modifier"
        '
        'BtSupprimer
        '
        Me.BtSupprimer.Image = Global.ClearProject.My.Resources.Resources.supprimer
        Me.BtSupprimer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtSupprimer.Location = New System.Drawing.Point(898, 5)
        Me.BtSupprimer.Margin = New System.Windows.Forms.Padding(5)
        Me.BtSupprimer.Name = "BtSupprimer"
        Me.BtSupprimer.Size = New System.Drawing.Size(65, 69)
        Me.BtSupprimer.TabIndex = 7
        Me.BtSupprimer.ToolTip = "Supprimer"
        '
        'BtRetour
        '
        Me.BtRetour.Enabled = False
        Me.BtRetour.Image = Global.ClearProject.My.Resources.Resources.fleche_modifier_vieux_defaire_icone_5639_32
        Me.BtRetour.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtRetour.Location = New System.Drawing.Point(968, 5)
        Me.BtRetour.Margin = New System.Windows.Forms.Padding(5)
        Me.BtRetour.Name = "BtRetour"
        Me.BtRetour.Size = New System.Drawing.Size(65, 69)
        Me.BtRetour.TabIndex = 6
        Me.BtRetour.ToolTip = "Retour"
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Enabled = False
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.enregistrer1
        Me.BtEnregistrer.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtEnregistrer.Location = New System.Drawing.Point(1038, 5)
        Me.BtEnregistrer.Margin = New System.Windows.Forms.Padding(5)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(65, 69)
        Me.BtEnregistrer.TabIndex = 5
        Me.BtEnregistrer.ToolTip = "Enregistrer"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(278, 96)
        Me.Label16.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(32, 24)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "N°"
        '
        'TxtNumConvention
        '
        Me.TxtNumConvention.Enabled = False
        Me.TxtNumConvention.Location = New System.Drawing.Point(313, 92)
        Me.TxtNumConvention.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtNumConvention.MaxLength = 20
        Me.TxtNumConvention.Name = "TxtNumConvention"
        Me.TxtNumConvention.Size = New System.Drawing.Size(249, 30)
        Me.TxtNumConvention.TabIndex = 10
        Me.TxtNumConvention.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(2, 193)
        Me.Label7.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 24)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Signature"
        '
        'TxtSource
        '
        Me.TxtSource.Enabled = False
        Me.TxtSource.Location = New System.Drawing.Point(278, 44)
        Me.TxtSource.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtSource.Name = "TxtSource"
        Me.TxtSource.Size = New System.Drawing.Size(782, 30)
        Me.TxtSource.TabIndex = 8
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(578, 99)
        Me.Label17.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(114, 24)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "Pièce jointe"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 48)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Source"
        '
        'BtRecherche
        '
        Me.BtRecherche.Enabled = False
        Me.BtRecherche.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtRecherche.Location = New System.Drawing.Point(1015, 90)
        Me.BtRecherche.Margin = New System.Windows.Forms.Padding(5)
        Me.BtRecherche.Name = "BtRecherche"
        Me.BtRecherche.Size = New System.Drawing.Size(45, 37)
        Me.BtRecherche.TabIndex = 37
        Me.BtRecherche.Text = "..."
        Me.BtRecherche.UseVisualStyleBackColor = True
        '
        'CmbType
        '
        Me.CmbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbType.Enabled = False
        Me.CmbType.FormattingEnabled = True
        Me.CmbType.Items.AddRange(New Object() {"Don", "Prêt", "Contrepartie", "Fonds propre"})
        Me.CmbType.Location = New System.Drawing.Point(98, 92)
        Me.CmbType.Margin = New System.Windows.Forms.Padding(5)
        Me.CmbType.MaxLength = 10
        Me.CmbType.Name = "CmbType"
        Me.CmbType.Size = New System.Drawing.Size(167, 31)
        Me.CmbType.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(337, 196)
        Me.Label8.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(190, 24)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Entrée en vigueur le"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(42, 96)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 24)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Type"
        '
        'TxtPieceJointe
        '
        Me.TxtPieceJointe.Enabled = False
        Me.TxtPieceJointe.Location = New System.Drawing.Point(693, 94)
        Me.TxtPieceJointe.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtPieceJointe.Name = "TxtPieceJointe"
        Me.TxtPieceJointe.ReadOnly = True
        Me.TxtPieceJointe.Size = New System.Drawing.Size(317, 30)
        Me.TxtPieceJointe.TabIndex = 11
        '
        'TxtCFA
        '
        Me.TxtCFA.Enabled = False
        Me.TxtCFA.Location = New System.Drawing.Point(158, 142)
        Me.TxtCFA.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtCFA.MaxLength = 20
        Me.TxtCFA.Name = "TxtCFA"
        Me.TxtCFA.Size = New System.Drawing.Size(164, 30)
        Me.TxtCFA.TabIndex = 12
        Me.TxtCFA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(773, 195)
        Me.Label9.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(107, 24)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Clôturée le"
        '
        'TxtLIVRE
        '
        Me.TxtLIVRE.Enabled = False
        Me.TxtLIVRE.Location = New System.Drawing.Point(890, 142)
        Me.TxtLIVRE.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtLIVRE.MaxLength = 20
        Me.TxtLIVRE.Name = "TxtLIVRE"
        Me.TxtLIVRE.Size = New System.Drawing.Size(164, 30)
        Me.TxtLIVRE.TabIndex = 15
        Me.TxtLIVRE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DTDateSignature
        '
        Me.DTDateSignature.Enabled = False
        Me.DTDateSignature.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTDateSignature.Location = New System.Drawing.Point(97, 191)
        Me.DTDateSignature.Margin = New System.Windows.Forms.Padding(5)
        Me.DTDateSignature.Name = "DTDateSignature"
        Me.DTDateSignature.Size = New System.Drawing.Size(166, 30)
        Me.DTDateSignature.TabIndex = 16
        Me.DTDateSignature.Value = New Date(2012, 5, 28, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 145)
        Me.Label10.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 24)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Montant"
        '
        'TextBox4
        '
        Me.TextBox4.Enabled = False
        Me.TextBox4.Location = New System.Drawing.Point(98, 142)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(5)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(56, 30)
        Me.TextBox4.TabIndex = 38
        Me.TextBox4.Text = "CFA"
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtUS
        '
        Me.TxtUS.Enabled = False
        Me.TxtUS.Location = New System.Drawing.Point(643, 142)
        Me.TxtUS.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtUS.MaxLength = 20
        Me.TxtUS.Name = "TxtUS"
        Me.TxtUS.Size = New System.Drawing.Size(164, 30)
        Me.TxtUS.TabIndex = 14
        Me.TxtUS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DTEntreeVigueur
        '
        Me.DTEntreeVigueur.Enabled = False
        Me.DTEntreeVigueur.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTEntreeVigueur.Location = New System.Drawing.Point(537, 193)
        Me.DTEntreeVigueur.Margin = New System.Windows.Forms.Padding(5)
        Me.DTEntreeVigueur.Name = "DTEntreeVigueur"
        Me.DTEntreeVigueur.Size = New System.Drawing.Size(166, 30)
        Me.DTEntreeVigueur.TabIndex = 17
        Me.DTEntreeVigueur.Value = New Date(2012, 5, 28, 0, 0, 0, 0)
        '
        'TxtEuro
        '
        Me.TxtEuro.Enabled = False
        Me.TxtEuro.Location = New System.Drawing.Point(402, 142)
        Me.TxtEuro.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtEuro.MaxLength = 20
        Me.TxtEuro.Name = "TxtEuro"
        Me.TxtEuro.Size = New System.Drawing.Size(164, 30)
        Me.TxtEuro.TabIndex = 13
        Me.TxtEuro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox5
        '
        Me.TextBox5.Enabled = False
        Me.TextBox5.Location = New System.Drawing.Point(342, 142)
        Me.TextBox5.Margin = New System.Windows.Forms.Padding(5)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(56, 30)
        Me.TextBox5.TabIndex = 39
        Me.TextBox5.Text = "EUR"
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtChemin
        '
        Me.TxtChemin.Enabled = False
        Me.TxtChemin.Location = New System.Drawing.Point(955, 94)
        Me.TxtChemin.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtChemin.Name = "TxtChemin"
        Me.TxtChemin.ReadOnly = True
        Me.TxtChemin.Size = New System.Drawing.Size(42, 30)
        Me.TxtChemin.TabIndex = 42
        '
        'DTCloture
        '
        Me.DTCloture.Enabled = False
        Me.DTCloture.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTCloture.Location = New System.Drawing.Point(885, 191)
        Me.DTCloture.Margin = New System.Windows.Forms.Padding(5)
        Me.DTCloture.Name = "DTCloture"
        Me.DTCloture.Size = New System.Drawing.Size(166, 30)
        Me.DTCloture.TabIndex = 18
        Me.DTCloture.Value = New Date(2012, 5, 28, 0, 0, 0, 0)
        '
        'TxtCodeSource
        '
        Me.TxtCodeSource.Enabled = False
        Me.TxtCodeSource.Location = New System.Drawing.Point(198, 44)
        Me.TxtCodeSource.Margin = New System.Windows.Forms.Padding(5)
        Me.TxtCodeSource.Name = "TxtCodeSource"
        Me.TxtCodeSource.Size = New System.Drawing.Size(49, 30)
        Me.TxtCodeSource.TabIndex = 43
        '
        'TextBox6
        '
        Me.TextBox6.Enabled = False
        Me.TextBox6.Location = New System.Drawing.Point(583, 142)
        Me.TextBox6.Margin = New System.Windows.Forms.Padding(5)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(56, 30)
        Me.TextBox6.TabIndex = 40
        Me.TextBox6.Text = "$US"
        Me.TextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CmbSource
        '
        Me.CmbSource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbSource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbSource.Enabled = False
        Me.CmbSource.FormattingEnabled = True
        Me.CmbSource.Location = New System.Drawing.Point(98, 44)
        Me.CmbSource.Margin = New System.Windows.Forms.Padding(5)
        Me.CmbSource.Name = "CmbSource"
        Me.CmbSource.Size = New System.Drawing.Size(167, 31)
        Me.CmbSource.TabIndex = 7
        '
        'TextBox7
        '
        Me.TextBox7.Enabled = False
        Me.TextBox7.Location = New System.Drawing.Point(830, 142)
        Me.TextBox7.Margin = New System.Windows.Forms.Padding(5)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.ReadOnly = True
        Me.TextBox7.Size = New System.Drawing.Size(56, 30)
        Me.TextBox7.TabIndex = 41
        Me.TextBox7.Text = "£"
        Me.TextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.TextBox7)
        Me.GroupControl1.Controls.Add(Me.CmbSource)
        Me.GroupControl1.Controls.Add(Me.TextBox6)
        Me.GroupControl1.Controls.Add(Me.TxtCodeSource)
        Me.GroupControl1.Controls.Add(Me.DTCloture)
        Me.GroupControl1.Controls.Add(Me.TextBox5)
        Me.GroupControl1.Controls.Add(Me.TxtEuro)
        Me.GroupControl1.Controls.Add(Me.DTEntreeVigueur)
        Me.GroupControl1.Controls.Add(Me.TxtUS)
        Me.GroupControl1.Controls.Add(Me.TextBox4)
        Me.GroupControl1.Controls.Add(Me.Label10)
        Me.GroupControl1.Controls.Add(Me.DTDateSignature)
        Me.GroupControl1.Controls.Add(Me.TxtLIVRE)
        Me.GroupControl1.Controls.Add(Me.Label9)
        Me.GroupControl1.Controls.Add(Me.TxtCFA)
        Me.GroupControl1.Controls.Add(Me.TxtPieceJointe)
        Me.GroupControl1.Controls.Add(Me.Label3)
        Me.GroupControl1.Controls.Add(Me.Label8)
        Me.GroupControl1.Controls.Add(Me.CmbType)
        Me.GroupControl1.Controls.Add(Me.BtRecherche)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.Label17)
        Me.GroupControl1.Controls.Add(Me.TxtSource)
        Me.GroupControl1.Controls.Add(Me.Label7)
        Me.GroupControl1.Controls.Add(Me.TxtNumConvention)
        Me.GroupControl1.Controls.Add(Me.Label16)
        Me.GroupControl1.Controls.Add(Me.TxtChemin)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl1.Location = New System.Drawing.Point(0, 79)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1108, 244)
        Me.GroupControl1.TabIndex = 46
        Me.GroupControl1.Text = "Convention"
        '
        'GridConvention
        '
        Me.GridConvention.AllowUserToAddRows = False
        Me.GridConvention.AllowUserToDeleteRows = False
        Me.GridConvention.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GridConvention.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GridConvention.BackgroundColor = System.Drawing.Color.White
        Me.GridConvention.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.GridConvention.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.GridConvention.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridConvention.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Bailleur, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7})
        Me.GridConvention.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GridConvention.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridConvention.Location = New System.Drawing.Point(2, 31)
        Me.GridConvention.Margin = New System.Windows.Forms.Padding(5)
        Me.GridConvention.MultiSelect = False
        Me.GridConvention.Name = "GridConvention"
        Me.GridConvention.ReadOnly = True
        Me.GridConvention.RowHeadersVisible = False
        Me.GridConvention.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridConvention.Size = New System.Drawing.Size(1104, 277)
        Me.GridConvention.TabIndex = 47
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.GridConvention)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(0, 323)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(5)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1108, 310)
        Me.GroupControl2.TabIndex = 48
        Me.GroupControl2.Text = "Conventions enregistrées"
        '
        'Bailleur
        '
        Me.Bailleur.HeaderText = "Bailleur"
        Me.Bailleur.Name = "Bailleur"
        Me.Bailleur.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Type"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Numero"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column3.HeaderText = "Montant"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 120
        '
        'Column4
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column4.HeaderText = "Signature"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column5.HeaderText = "Ouverture"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column6.HeaderText = "Clôture"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "Etat actuel"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 120
        '
        'SourceFinancement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1108, 633)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.BtAjouter)
        Me.Controls.Add(Me.BtModifier)
        Me.Controls.Add(Me.BtSupprimer)
        Me.Controls.Add(Me.BtRetour)
        Me.Controls.Add(Me.BtEnregistrer)
        Me.Controls.Add(Me.GroupControl2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SourceFinancement"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Convention"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GridConvention, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtAjouter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtModifier As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtSupprimer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtRetour As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtNumConvention As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtSource As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtRecherche As System.Windows.Forms.Button
    Friend WithEvents CmbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtPieceJointe As System.Windows.Forms.TextBox
    Friend WithEvents TxtCFA As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtLIVRE As System.Windows.Forms.TextBox
    Friend WithEvents DTDateSignature As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TxtUS As System.Windows.Forms.TextBox
    Friend WithEvents DTEntreeVigueur As System.Windows.Forms.DateTimePicker
    Friend WithEvents TxtEuro As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TxtChemin As System.Windows.Forms.TextBox
    Friend WithEvents DTCloture As System.Windows.Forms.DateTimePicker
    Friend WithEvents TxtCodeSource As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents CmbSource As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridConvention As System.Windows.Forms.DataGridView
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Bailleur As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
