<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParamChart
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
        Me.components = New System.ComponentModel.Container()
        Me.RdVue2D = New System.Windows.Forms.RadioButton()
        Me.RdVue3D = New System.Windows.Forms.RadioButton()
        Me.GbVue = New System.Windows.Forms.GroupBox()
        Me.GbStyle = New System.Windows.Forms.GroupBox()
        Me.RdStyleBar = New System.Windows.Forms.RadioButton()
        Me.RdStyleCyl = New System.Windows.Forms.RadioButton()
        Me.GbCouleur = New System.Windows.Forms.GroupBox()
        Me.LblTransp = New System.Windows.Forms.Label()
        Me.TbTransparence = New System.Windows.Forms.TrackBar()
        Me.GbRotation = New System.Windows.Forms.GroupBox()
        Me.LblRotation = New System.Windows.Forms.Label()
        Me.TbRotation = New System.Windows.Forms.TrackBar()
        Me.GbInclinaison = New System.Windows.Forms.GroupBox()
        Me.TbInclinaison = New System.Windows.Forms.TrackBar()
        Me.LblInclinaison = New System.Windows.Forms.Label()
        Me.GbSeries = New System.Windows.Forms.GroupBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TbPerspective = New System.Windows.Forms.TrackBar()
        Me.TbProfColo = New System.Windows.Forms.TrackBar()
        Me.TbProfInterne = New System.Windows.Forms.TrackBar()
        Me.TbEpaisseMur = New System.Windows.Forms.TrackBar()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.GbPerspective = New System.Windows.Forms.GroupBox()
        Me.LblPerspective = New System.Windows.Forms.Label()
        Me.GbProfColo = New System.Windows.Forms.GroupBox()
        Me.LblProfColo = New System.Windows.Forms.Label()
        Me.GbProfInt = New System.Windows.Forms.GroupBox()
        Me.LblProfInterne = New System.Windows.Forms.Label()
        Me.GbEpaisMur = New System.Windows.Forms.GroupBox()
        Me.LblEpaisseMur = New System.Windows.Forms.Label()
        Me.GbEclairage = New System.Windows.Forms.GroupBox()
        Me.RdEclairage2 = New System.Windows.Forms.RadioButton()
        Me.RdEclairage1 = New System.Windows.Forms.RadioButton()
        Me.RdEclairageNon = New System.Windows.Forms.RadioButton()
        Me.BtActualiser = New System.Windows.Forms.Button()
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BtImprimerChart = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RdLegOui = New System.Windows.Forms.RadioButton()
        Me.RdLegNon = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ChkAxeY = New System.Windows.Forms.CheckBox()
        Me.ChkAxeX = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RdLibOui = New System.Windows.Forms.RadioButton()
        Me.RdLibNon = New System.Windows.Forms.RadioButton()
        Me.GbVue.SuspendLayout()
        Me.GbStyle.SuspendLayout()
        Me.GbCouleur.SuspendLayout()
        CType(Me.TbTransparence, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbRotation.SuspendLayout()
        CType(Me.TbRotation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbInclinaison.SuspendLayout()
        CType(Me.TbInclinaison, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TbPerspective, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TbProfColo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TbProfInterne, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TbEpaisseMur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbPerspective.SuspendLayout()
        Me.GbProfColo.SuspendLayout()
        Me.GbProfInt.SuspendLayout()
        Me.GbEpaisMur.SuspendLayout()
        Me.GbEclairage.SuspendLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'RdVue2D
        '
        Me.RdVue2D.AutoSize = True
        Me.RdVue2D.Location = New System.Drawing.Point(44, 12)
        Me.RdVue2D.Name = "RdVue2D"
        Me.RdVue2D.Size = New System.Drawing.Size(39, 17)
        Me.RdVue2D.TabIndex = 0
        Me.RdVue2D.TabStop = True
        Me.RdVue2D.Text = "2D"
        Me.RdVue2D.UseVisualStyleBackColor = True
        '
        'RdVue3D
        '
        Me.RdVue3D.AutoSize = True
        Me.RdVue3D.Location = New System.Drawing.Point(106, 12)
        Me.RdVue3D.Name = "RdVue3D"
        Me.RdVue3D.Size = New System.Drawing.Size(39, 17)
        Me.RdVue3D.TabIndex = 1
        Me.RdVue3D.TabStop = True
        Me.RdVue3D.Text = "3D"
        Me.RdVue3D.UseVisualStyleBackColor = True
        '
        'GbVue
        '
        Me.GbVue.Controls.Add(Me.RdVue2D)
        Me.GbVue.Controls.Add(Me.RdVue3D)
        Me.GbVue.Location = New System.Drawing.Point(1, 251)
        Me.GbVue.Name = "GbVue"
        Me.GbVue.Size = New System.Drawing.Size(190, 34)
        Me.GbVue.TabIndex = 2
        Me.GbVue.TabStop = False
        Me.GbVue.Text = "Vue"
        '
        'GbStyle
        '
        Me.GbStyle.Controls.Add(Me.RdStyleBar)
        Me.GbStyle.Controls.Add(Me.RdStyleCyl)
        Me.GbStyle.Location = New System.Drawing.Point(1, 175)
        Me.GbStyle.Name = "GbStyle"
        Me.GbStyle.Size = New System.Drawing.Size(190, 34)
        Me.GbStyle.TabIndex = 3
        Me.GbStyle.TabStop = False
        Me.GbStyle.Text = "Style"
        '
        'RdStyleBar
        '
        Me.RdStyleBar.AutoSize = True
        Me.RdStyleBar.Location = New System.Drawing.Point(44, 12)
        Me.RdStyleBar.Name = "RdStyleBar"
        Me.RdStyleBar.Size = New System.Drawing.Size(55, 17)
        Me.RdStyleBar.TabIndex = 0
        Me.RdStyleBar.TabStop = True
        Me.RdStyleBar.Text = "Barres"
        Me.RdStyleBar.UseVisualStyleBackColor = True
        '
        'RdStyleCyl
        '
        Me.RdStyleCyl.AutoSize = True
        Me.RdStyleCyl.Location = New System.Drawing.Point(106, 12)
        Me.RdStyleCyl.Name = "RdStyleCyl"
        Me.RdStyleCyl.Size = New System.Drawing.Size(67, 17)
        Me.RdStyleCyl.TabIndex = 1
        Me.RdStyleCyl.TabStop = True
        Me.RdStyleCyl.Text = "Cylindres"
        Me.RdStyleCyl.UseVisualStyleBackColor = True
        '
        'GbCouleur
        '
        Me.GbCouleur.Controls.Add(Me.LblTransp)
        Me.GbCouleur.Controls.Add(Me.TbTransparence)
        Me.GbCouleur.Enabled = False
        Me.GbCouleur.Location = New System.Drawing.Point(1, 284)
        Me.GbCouleur.Name = "GbCouleur"
        Me.GbCouleur.Size = New System.Drawing.Size(190, 34)
        Me.GbCouleur.TabIndex = 4
        Me.GbCouleur.TabStop = False
        Me.GbCouleur.Text = "Transparence"
        '
        'LblTransp
        '
        Me.LblTransp.AutoSize = True
        Me.LblTransp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTransp.Location = New System.Drawing.Point(162, 13)
        Me.LblTransp.Name = "LblTransp"
        Me.LblTransp.Size = New System.Drawing.Size(13, 13)
        Me.LblTransp.TabIndex = 29
        Me.LblTransp.Text = "0"
        '
        'TbTransparence
        '
        Me.TbTransparence.AutoSize = False
        Me.TbTransparence.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TbTransparence.Location = New System.Drawing.Point(10, 14)
        Me.TbTransparence.Maximum = 255
        Me.TbTransparence.Name = "TbTransparence"
        Me.TbTransparence.Size = New System.Drawing.Size(150, 16)
        Me.TbTransparence.TabIndex = 28
        Me.TbTransparence.TickStyle = System.Windows.Forms.TickStyle.None
        Me.ToolTip1.SetToolTip(Me.TbTransparence, "Glisser pour modifier")
        '
        'GbRotation
        '
        Me.GbRotation.Controls.Add(Me.LblRotation)
        Me.GbRotation.Controls.Add(Me.TbRotation)
        Me.GbRotation.Enabled = False
        Me.GbRotation.Location = New System.Drawing.Point(1, 317)
        Me.GbRotation.Name = "GbRotation"
        Me.GbRotation.Size = New System.Drawing.Size(190, 34)
        Me.GbRotation.TabIndex = 5
        Me.GbRotation.TabStop = False
        Me.GbRotation.Text = "Rotation"
        '
        'LblRotation
        '
        Me.LblRotation.AutoSize = True
        Me.LblRotation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRotation.Location = New System.Drawing.Point(160, 13)
        Me.LblRotation.Name = "LblRotation"
        Me.LblRotation.Size = New System.Drawing.Size(23, 13)
        Me.LblRotation.TabIndex = 28
        Me.LblRotation.Text = "30°"
        '
        'TbRotation
        '
        Me.TbRotation.AutoSize = False
        Me.TbRotation.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TbRotation.Location = New System.Drawing.Point(10, 14)
        Me.TbRotation.Maximum = 44
        Me.TbRotation.Name = "TbRotation"
        Me.TbRotation.Size = New System.Drawing.Size(150, 16)
        Me.TbRotation.TabIndex = 27
        Me.TbRotation.TickStyle = System.Windows.Forms.TickStyle.None
        Me.ToolTip1.SetToolTip(Me.TbRotation, "Glisser pour modifier")
        '
        'GbInclinaison
        '
        Me.GbInclinaison.Controls.Add(Me.TbInclinaison)
        Me.GbInclinaison.Controls.Add(Me.LblInclinaison)
        Me.GbInclinaison.Enabled = False
        Me.GbInclinaison.Location = New System.Drawing.Point(1, 350)
        Me.GbInclinaison.Name = "GbInclinaison"
        Me.GbInclinaison.Size = New System.Drawing.Size(190, 34)
        Me.GbInclinaison.TabIndex = 6
        Me.GbInclinaison.TabStop = False
        Me.GbInclinaison.Text = "Inclinaison"
        '
        'TbInclinaison
        '
        Me.TbInclinaison.AutoSize = False
        Me.TbInclinaison.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TbInclinaison.Location = New System.Drawing.Point(10, 14)
        Me.TbInclinaison.Maximum = 89
        Me.TbInclinaison.Name = "TbInclinaison"
        Me.TbInclinaison.Size = New System.Drawing.Size(150, 16)
        Me.TbInclinaison.TabIndex = 28
        Me.TbInclinaison.TickStyle = System.Windows.Forms.TickStyle.None
        Me.ToolTip1.SetToolTip(Me.TbInclinaison, "Glisser pour modifier")
        '
        'LblInclinaison
        '
        Me.LblInclinaison.AutoSize = True
        Me.LblInclinaison.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInclinaison.Location = New System.Drawing.Point(160, 13)
        Me.LblInclinaison.Name = "LblInclinaison"
        Me.LblInclinaison.Size = New System.Drawing.Size(23, 13)
        Me.LblInclinaison.TabIndex = 1
        Me.LblInclinaison.Text = "30°"
        '
        'GbSeries
        '
        Me.GbSeries.Location = New System.Drawing.Point(1, 208)
        Me.GbSeries.Name = "GbSeries"
        Me.GbSeries.Size = New System.Drawing.Size(190, 34)
        Me.GbSeries.TabIndex = 7
        Me.GbSeries.TabStop = False
        Me.GbSeries.Text = "Séries"
        '
        'TbPerspective
        '
        Me.TbPerspective.AutoSize = False
        Me.TbPerspective.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TbPerspective.Location = New System.Drawing.Point(10, 14)
        Me.TbPerspective.Maximum = 44
        Me.TbPerspective.Name = "TbPerspective"
        Me.TbPerspective.Size = New System.Drawing.Size(150, 16)
        Me.TbPerspective.TabIndex = 28
        Me.TbPerspective.TickStyle = System.Windows.Forms.TickStyle.None
        Me.ToolTip1.SetToolTip(Me.TbPerspective, "Glisser pour modifier")
        '
        'TbProfColo
        '
        Me.TbProfColo.AutoSize = False
        Me.TbProfColo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TbProfColo.Location = New System.Drawing.Point(10, 14)
        Me.TbProfColo.Maximum = 100
        Me.TbProfColo.Name = "TbProfColo"
        Me.TbProfColo.Size = New System.Drawing.Size(150, 16)
        Me.TbProfColo.TabIndex = 28
        Me.TbProfColo.TickStyle = System.Windows.Forms.TickStyle.None
        Me.ToolTip1.SetToolTip(Me.TbProfColo, "Glisser pour modifier")
        '
        'TbProfInterne
        '
        Me.TbProfInterne.AutoSize = False
        Me.TbProfInterne.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TbProfInterne.Location = New System.Drawing.Point(10, 14)
        Me.TbProfInterne.Maximum = 100
        Me.TbProfInterne.Name = "TbProfInterne"
        Me.TbProfInterne.Size = New System.Drawing.Size(150, 16)
        Me.TbProfInterne.TabIndex = 28
        Me.TbProfInterne.TickStyle = System.Windows.Forms.TickStyle.None
        Me.ToolTip1.SetToolTip(Me.TbProfInterne, "Glisser pour modifier")
        '
        'TbEpaisseMur
        '
        Me.TbEpaisseMur.AutoSize = False
        Me.TbEpaisseMur.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TbEpaisseMur.Location = New System.Drawing.Point(10, 14)
        Me.TbEpaisseMur.Maximum = 30
        Me.TbEpaisseMur.Name = "TbEpaisseMur"
        Me.TbEpaisseMur.Size = New System.Drawing.Size(150, 16)
        Me.TbEpaisseMur.TabIndex = 28
        Me.TbEpaisseMur.TickStyle = System.Windows.Forms.TickStyle.None
        Me.ToolTip1.SetToolTip(Me.TbEpaisseMur, "Glisser pour modifier")
        '
        'PictureBox4
        '
        Me.PictureBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PictureBox4.Location = New System.Drawing.Point(151, 0)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(48, 20)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 1
        Me.PictureBox4.TabStop = False
        '
        'GbPerspective
        '
        Me.GbPerspective.Controls.Add(Me.TbPerspective)
        Me.GbPerspective.Controls.Add(Me.LblPerspective)
        Me.GbPerspective.Enabled = False
        Me.GbPerspective.Location = New System.Drawing.Point(1, 383)
        Me.GbPerspective.Name = "GbPerspective"
        Me.GbPerspective.Size = New System.Drawing.Size(190, 34)
        Me.GbPerspective.TabIndex = 19
        Me.GbPerspective.TabStop = False
        Me.GbPerspective.Text = "Perspective"
        '
        'LblPerspective
        '
        Me.LblPerspective.AutoSize = True
        Me.LblPerspective.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPerspective.Location = New System.Drawing.Point(160, 13)
        Me.LblPerspective.Name = "LblPerspective"
        Me.LblPerspective.Size = New System.Drawing.Size(27, 13)
        Me.LblPerspective.TabIndex = 1
        Me.LblPerspective.Text = "30%"
        '
        'GbProfColo
        '
        Me.GbProfColo.Controls.Add(Me.LblProfColo)
        Me.GbProfColo.Controls.Add(Me.TbProfColo)
        Me.GbProfColo.Enabled = False
        Me.GbProfColo.Location = New System.Drawing.Point(1, 416)
        Me.GbProfColo.Name = "GbProfColo"
        Me.GbProfColo.Size = New System.Drawing.Size(190, 34)
        Me.GbProfColo.TabIndex = 20
        Me.GbProfColo.TabStop = False
        Me.GbProfColo.Text = "Profdr colonne"
        '
        'LblProfColo
        '
        Me.LblProfColo.AutoSize = True
        Me.LblProfColo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProfColo.Location = New System.Drawing.Point(160, 13)
        Me.LblProfColo.Name = "LblProfColo"
        Me.LblProfColo.Size = New System.Drawing.Size(25, 13)
        Me.LblProfColo.TabIndex = 29
        Me.LblProfColo.Text = "100"
        '
        'GbProfInt
        '
        Me.GbProfInt.Controls.Add(Me.LblProfInterne)
        Me.GbProfInt.Controls.Add(Me.TbProfInterne)
        Me.GbProfInt.Enabled = False
        Me.GbProfInt.Location = New System.Drawing.Point(1, 449)
        Me.GbProfInt.Name = "GbProfInt"
        Me.GbProfInt.Size = New System.Drawing.Size(190, 34)
        Me.GbProfInt.TabIndex = 21
        Me.GbProfInt.TabStop = False
        Me.GbProfInt.Text = "Profdr interne"
        '
        'LblProfInterne
        '
        Me.LblProfInterne.AutoSize = True
        Me.LblProfInterne.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProfInterne.Location = New System.Drawing.Point(160, 13)
        Me.LblProfInterne.Name = "LblProfInterne"
        Me.LblProfInterne.Size = New System.Drawing.Size(19, 13)
        Me.LblProfInterne.TabIndex = 29
        Me.LblProfInterne.Text = "50"
        '
        'GbEpaisMur
        '
        Me.GbEpaisMur.Controls.Add(Me.LblEpaisseMur)
        Me.GbEpaisMur.Controls.Add(Me.TbEpaisseMur)
        Me.GbEpaisMur.Enabled = False
        Me.GbEpaisMur.Location = New System.Drawing.Point(1, 482)
        Me.GbEpaisMur.Name = "GbEpaisMur"
        Me.GbEpaisMur.Size = New System.Drawing.Size(190, 34)
        Me.GbEpaisMur.TabIndex = 22
        Me.GbEpaisMur.TabStop = False
        Me.GbEpaisMur.Text = "Epaisseur mur"
        '
        'LblEpaisseMur
        '
        Me.LblEpaisseMur.AutoSize = True
        Me.LblEpaisseMur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEpaisseMur.Location = New System.Drawing.Point(160, 13)
        Me.LblEpaisseMur.Name = "LblEpaisseMur"
        Me.LblEpaisseMur.Size = New System.Drawing.Size(24, 13)
        Me.LblEpaisseMur.TabIndex = 29
        Me.LblEpaisseMur.Text = "7px"
        '
        'GbEclairage
        '
        Me.GbEclairage.Controls.Add(Me.RdEclairage2)
        Me.GbEclairage.Controls.Add(Me.RdEclairage1)
        Me.GbEclairage.Controls.Add(Me.RdEclairageNon)
        Me.GbEclairage.Enabled = False
        Me.GbEclairage.Location = New System.Drawing.Point(1, 515)
        Me.GbEclairage.Name = "GbEclairage"
        Me.GbEclairage.Size = New System.Drawing.Size(190, 34)
        Me.GbEclairage.TabIndex = 23
        Me.GbEclairage.TabStop = False
        Me.GbEclairage.Text = "Eclairage"
        '
        'RdEclairage2
        '
        Me.RdEclairage2.AutoSize = True
        Me.RdEclairage2.Location = New System.Drawing.Point(146, 12)
        Me.RdEclairage2.Name = "RdEclairage2"
        Me.RdEclairage2.Size = New System.Drawing.Size(38, 17)
        Me.RdEclairage2.TabIndex = 3
        Me.RdEclairage2.TabStop = True
        Me.RdEclairage2.Text = "E2"
        Me.RdEclairage2.UseVisualStyleBackColor = True
        '
        'RdEclairage1
        '
        Me.RdEclairage1.AutoSize = True
        Me.RdEclairage1.Location = New System.Drawing.Point(102, 12)
        Me.RdEclairage1.Name = "RdEclairage1"
        Me.RdEclairage1.Size = New System.Drawing.Size(38, 17)
        Me.RdEclairage1.TabIndex = 2
        Me.RdEclairage1.TabStop = True
        Me.RdEclairage1.Text = "E1"
        Me.RdEclairage1.UseVisualStyleBackColor = True
        '
        'RdEclairageNon
        '
        Me.RdEclairageNon.AutoSize = True
        Me.RdEclairageNon.Location = New System.Drawing.Point(53, 12)
        Me.RdEclairageNon.Name = "RdEclairageNon"
        Me.RdEclairageNon.Size = New System.Drawing.Size(38, 17)
        Me.RdEclairageNon.TabIndex = 1
        Me.RdEclairageNon.TabStop = True
        Me.RdEclairageNon.Text = "E0"
        Me.RdEclairageNon.UseVisualStyleBackColor = True
        '
        'BtActualiser
        '
        Me.BtActualiser.Location = New System.Drawing.Point(1, 562)
        Me.BtActualiser.Name = "BtActualiser"
        Me.BtActualiser.Size = New System.Drawing.Size(190, 21)
        Me.BtActualiser.TabIndex = 24
        Me.BtActualiser.Text = "Appliquer les modifications"
        Me.BtActualiser.UseVisualStyleBackColor = True
        Me.BtActualiser.Visible = False
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.CountItem = Nothing
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtImprimerChart, Me.ToolStripButton2, Me.ToolStripButton1})
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigator1.MoveFirstItem = Nothing
        Me.BindingNavigator1.MoveLastItem = Nothing
        Me.BindingNavigator1.MoveNextItem = Nothing
        Me.BindingNavigator1.MovePreviousItem = Nothing
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Nothing
        Me.BindingNavigator1.Size = New System.Drawing.Size(194, 25)
        Me.BindingNavigator1.TabIndex = 25
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'BtImprimerChart
        '
        Me.BtImprimerChart.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.BtImprimerChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtImprimerChart.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtImprimerChart.Name = "BtImprimerChart"
        Me.BtImprimerChart.Size = New System.Drawing.Size(23, 22)
        Me.BtImprimerChart.Text = "ToolStripButton1"
        Me.BtImprimerChart.ToolTipText = "Imprimer"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        Me.ToolStripButton2.ToolTipText = "Apperçu avant impression"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "Marges"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RdLegOui)
        Me.GroupBox1.Controls.Add(Me.RdLegNon)
        Me.GroupBox1.Location = New System.Drawing.Point(1, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(190, 34)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Légende"
        '
        'RdLegOui
        '
        Me.RdLegOui.AutoSize = True
        Me.RdLegOui.Location = New System.Drawing.Point(44, 12)
        Me.RdLegOui.Name = "RdLegOui"
        Me.RdLegOui.Size = New System.Drawing.Size(41, 17)
        Me.RdLegOui.TabIndex = 0
        Me.RdLegOui.TabStop = True
        Me.RdLegOui.Text = "Oui"
        Me.RdLegOui.UseVisualStyleBackColor = True
        '
        'RdLegNon
        '
        Me.RdLegNon.AutoSize = True
        Me.RdLegNon.Location = New System.Drawing.Point(106, 12)
        Me.RdLegNon.Name = "RdLegNon"
        Me.RdLegNon.Size = New System.Drawing.Size(45, 17)
        Me.RdLegNon.TabIndex = 1
        Me.RdLegNon.TabStop = True
        Me.RdLegNon.Text = "Non"
        Me.RdLegNon.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.ChkAxeY)
        Me.GroupBox2.Controls.Add(Me.ChkAxeX)
        Me.GroupBox2.Location = New System.Drawing.Point(1, 99)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(190, 34)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Axes"
        '
        'ChkAxeY
        '
        Me.ChkAxeY.AutoSize = True
        Me.ChkAxeY.Location = New System.Drawing.Point(106, 12)
        Me.ChkAxeY.Name = "ChkAxeY"
        Me.ChkAxeY.Size = New System.Drawing.Size(33, 17)
        Me.ChkAxeY.TabIndex = 1
        Me.ChkAxeY.Text = "Y"
        Me.ChkAxeY.UseVisualStyleBackColor = True
        '
        'ChkAxeX
        '
        Me.ChkAxeX.AutoSize = True
        Me.ChkAxeX.Location = New System.Drawing.Point(44, 12)
        Me.ChkAxeX.Name = "ChkAxeX"
        Me.ChkAxeX.Size = New System.Drawing.Size(33, 17)
        Me.ChkAxeX.TabIndex = 0
        Me.ChkAxeX.Text = "X"
        Me.ChkAxeX.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.RdLibOui)
        Me.GroupBox3.Controls.Add(Me.RdLibNon)
        Me.GroupBox3.Location = New System.Drawing.Point(1, 132)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(190, 34)
        Me.GroupBox3.TabIndex = 28
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Valeurs"
        '
        'RdLibOui
        '
        Me.RdLibOui.AutoSize = True
        Me.RdLibOui.Location = New System.Drawing.Point(44, 12)
        Me.RdLibOui.Name = "RdLibOui"
        Me.RdLibOui.Size = New System.Drawing.Size(41, 17)
        Me.RdLibOui.TabIndex = 0
        Me.RdLibOui.TabStop = True
        Me.RdLibOui.Text = "Oui"
        Me.RdLibOui.UseVisualStyleBackColor = True
        '
        'RdLibNon
        '
        Me.RdLibNon.AutoSize = True
        Me.RdLibNon.Location = New System.Drawing.Point(106, 12)
        Me.RdLibNon.Name = "RdLibNon"
        Me.RdLibNon.Size = New System.Drawing.Size(45, 17)
        Me.RdLibNon.TabIndex = 1
        Me.RdLibNon.TabStop = True
        Me.RdLibNon.Text = "Non"
        Me.RdLibNon.UseVisualStyleBackColor = True
        '
        'ParamChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(194, 596)
        Me.Controls.Add(Me.BindingNavigator1)
        Me.Controls.Add(Me.BtActualiser)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GbStyle)
        Me.Controls.Add(Me.GbSeries)
        Me.Controls.Add(Me.GbVue)
        Me.Controls.Add(Me.GbCouleur)
        Me.Controls.Add(Me.GbRotation)
        Me.Controls.Add(Me.GbInclinaison)
        Me.Controls.Add(Me.GbPerspective)
        Me.Controls.Add(Me.GbProfColo)
        Me.Controls.Add(Me.GbProfInt)
        Me.Controls.Add(Me.GbEpaisMur)
        Me.Controls.Add(Me.GbEclairage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ParamChart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Paramètres"
        Me.TopMost = True
        Me.GbVue.ResumeLayout(False)
        Me.GbVue.PerformLayout()
        Me.GbStyle.ResumeLayout(False)
        Me.GbStyle.PerformLayout()
        Me.GbCouleur.ResumeLayout(False)
        Me.GbCouleur.PerformLayout()
        CType(Me.TbTransparence, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbRotation.ResumeLayout(False)
        Me.GbRotation.PerformLayout()
        CType(Me.TbRotation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbInclinaison.ResumeLayout(False)
        Me.GbInclinaison.PerformLayout()
        CType(Me.TbInclinaison, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TbPerspective, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TbProfColo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TbProfInterne, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TbEpaisseMur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbPerspective.ResumeLayout(False)
        Me.GbPerspective.PerformLayout()
        Me.GbProfColo.ResumeLayout(False)
        Me.GbProfColo.PerformLayout()
        Me.GbProfInt.ResumeLayout(False)
        Me.GbProfInt.PerformLayout()
        Me.GbEpaisMur.ResumeLayout(False)
        Me.GbEpaisMur.PerformLayout()
        Me.GbEclairage.ResumeLayout(False)
        Me.GbEclairage.PerformLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RdVue2D As System.Windows.Forms.RadioButton
    Friend WithEvents RdVue3D As System.Windows.Forms.RadioButton
    Friend WithEvents GbVue As System.Windows.Forms.GroupBox
    Friend WithEvents GbStyle As System.Windows.Forms.GroupBox
    Friend WithEvents RdStyleBar As System.Windows.Forms.RadioButton
    Friend WithEvents RdStyleCyl As System.Windows.Forms.RadioButton
    Friend WithEvents GbCouleur As System.Windows.Forms.GroupBox
    Friend WithEvents GbRotation As System.Windows.Forms.GroupBox
    Friend WithEvents GbInclinaison As System.Windows.Forms.GroupBox
    Friend WithEvents LblInclinaison As System.Windows.Forms.Label
    Friend WithEvents GbSeries As System.Windows.Forms.GroupBox
    'Friend WithEvents CoulSerie1 As KS.Gantt.Dialogs.Controls.ColorComboBox
    'Friend WithEvents CoulSerie4 As KS.Gantt.Dialogs.Controls.ColorComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    'Friend WithEvents CoulSerie3 As KS.Gantt.Dialogs.Controls.ColorComboBox
    'Friend WithEvents CoulSerie2 As KS.Gantt.Dialogs.Controls.ColorComboBox
    'Friend WithEvents EnteteRapport As csprCollapsiblePanel.csprCollapisblePanel
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents GbPerspective As System.Windows.Forms.GroupBox
    Friend WithEvents LblPerspective As System.Windows.Forms.Label
    Friend WithEvents GbProfColo As System.Windows.Forms.GroupBox
    Friend WithEvents GbProfInt As System.Windows.Forms.GroupBox
    Friend WithEvents GbEpaisMur As System.Windows.Forms.GroupBox
    Friend WithEvents GbEclairage As System.Windows.Forms.GroupBox
    Friend WithEvents RdEclairage2 As System.Windows.Forms.RadioButton
    Friend WithEvents RdEclairage1 As System.Windows.Forms.RadioButton
    Friend WithEvents RdEclairageNon As System.Windows.Forms.RadioButton
    Friend WithEvents BtActualiser As System.Windows.Forms.Button
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BtImprimerChart As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RdLegOui As System.Windows.Forms.RadioButton
    Friend WithEvents RdLegNon As System.Windows.Forms.RadioButton
    Friend WithEvents TbRotation As System.Windows.Forms.TrackBar
    Friend WithEvents LblRotation As System.Windows.Forms.Label
    Friend WithEvents TbInclinaison As System.Windows.Forms.TrackBar
    Friend WithEvents TbPerspective As System.Windows.Forms.TrackBar
    Friend WithEvents TbProfColo As System.Windows.Forms.TrackBar
    Friend WithEvents TbProfInterne As System.Windows.Forms.TrackBar
    Friend WithEvents TbEpaisseMur As System.Windows.Forms.TrackBar
    Friend WithEvents LblProfColo As System.Windows.Forms.Label
    Friend WithEvents LblProfInterne As System.Windows.Forms.Label
    Friend WithEvents LblEpaisseMur As System.Windows.Forms.Label
    Friend WithEvents LblTransp As System.Windows.Forms.Label
    Friend WithEvents TbTransparence As System.Windows.Forms.TrackBar
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RdLibOui As System.Windows.Forms.RadioButton
    Friend WithEvents RdLibNon As System.Windows.Forms.RadioButton
    Friend WithEvents ChkAxeY As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAxeX As System.Windows.Forms.CheckBox
    'Friend WithEvents CoulTexte As KS.Gantt.Dialogs.Controls.ColorComboBox
End Class
