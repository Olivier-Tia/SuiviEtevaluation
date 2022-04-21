<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PersonenChargeProjet
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.Nom = New DevExpress.XtraEditors.TextEdit()
        Me.Contact = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.Fonction = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.Ville = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.Adresse = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.Email = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GridPerson = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SupprimerCompteBailleurToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewPerson = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtEnregistrer = New DevExpress.XtraEditors.SimpleButton()
        Me.Fermer = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.Nom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Contact.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fonction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ville.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Adresse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Email.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GridPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.ViewPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.Nom)
        Me.GroupControl1.Controls.Add(Me.Contact)
        Me.GroupControl1.Controls.Add(Me.LabelControl8)
        Me.GroupControl1.Controls.Add(Me.Fonction)
        Me.GroupControl1.Controls.Add(Me.LabelControl5)
        Me.GroupControl1.Controls.Add(Me.Ville)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.Adresse)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.Email)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(516, 205)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Info personnel"
        '
        'Nom
        '
        Me.Nom.Location = New System.Drawing.Point(18, 46)
        Me.Nom.Name = "Nom"
        Me.Nom.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Nom.Properties.Appearance.Options.UseFont = True
        Me.Nom.Properties.MaxLength = 200
        Me.Nom.Size = New System.Drawing.Size(486, 22)
        Me.Nom.TabIndex = 1
        '
        'Contact
        '
        Me.Contact.Location = New System.Drawing.Point(20, 88)
        Me.Contact.Name = "Contact"
        Me.Contact.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Contact.Properties.Appearance.Options.UseFont = True
        Me.Contact.Size = New System.Drawing.Size(203, 22)
        Me.Contact.TabIndex = 26
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl8.Location = New System.Drawing.Point(20, 73)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(45, 15)
        Me.LabelControl8.TabIndex = 15
        Me.LabelControl8.Text = "Contact"
        '
        'Fonction
        '
        Me.Fonction.Location = New System.Drawing.Point(233, 137)
        Me.Fonction.Name = "Fonction"
        Me.Fonction.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fonction.Properties.Appearance.Options.UseFont = True
        Me.Fonction.Size = New System.Drawing.Size(271, 22)
        Me.Fonction.TabIndex = 9
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(20, 161)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(37, 15)
        Me.LabelControl5.TabIndex = 8
        Me.LabelControl5.Text = "E-mail"
        '
        'Ville
        '
        Me.Ville.Location = New System.Drawing.Point(20, 137)
        Me.Ville.Name = "Ville"
        Me.Ville.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ville.Properties.Appearance.Options.UseFont = True
        Me.Ville.Size = New System.Drawing.Size(203, 22)
        Me.Ville.TabIndex = 7
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(20, 116)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(26, 15)
        Me.LabelControl4.TabIndex = 6
        Me.LabelControl4.Text = "Ville"
        '
        'Adresse
        '
        Me.Adresse.Location = New System.Drawing.Point(233, 89)
        Me.Adresse.Name = "Adresse"
        Me.Adresse.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Adresse.Properties.Appearance.Options.UseFont = True
        Me.Adresse.Properties.Mask.EditMask = "n0"
        Me.Adresse.Size = New System.Drawing.Size(271, 22)
        Me.Adresse.TabIndex = 5
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(229, 73)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(42, 15)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Adresse"
        '
        'Email
        '
        Me.Email.Location = New System.Drawing.Point(18, 178)
        Me.Email.Name = "Email"
        Me.Email.Properties.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Email.Properties.Appearance.Options.UseFont = True
        Me.Email.Properties.Mask.EditMask = "(999)000-0000"
        Me.Email.Size = New System.Drawing.Size(484, 22)
        Me.Email.TabIndex = 3
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(233, 116)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(50, 15)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Fonction"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(20, 29)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(83, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Nom et Prénom"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.GridPerson)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(0, 246)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(516, 134)
        Me.GroupControl2.TabIndex = 2
        Me.GroupControl2.Text = "Liste des personnes enregistré"
        '
        'GridPerson
        '
        Me.GridPerson.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridPerson.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridPerson.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridPerson.Location = New System.Drawing.Point(2, 23)
        Me.GridPerson.MainView = Me.ViewPerson
        Me.GridPerson.Name = "GridPerson"
        Me.GridPerson.Size = New System.Drawing.Size(512, 109)
        Me.GridPerson.TabIndex = 7
        Me.GridPerson.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewPerson})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SupprimerCompteBailleurToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(133, 26)
        '
        'SupprimerCompteBailleurToolStripMenuItem
        '
        Me.SupprimerCompteBailleurToolStripMenuItem.Image = Global.ClearProject.My.Resources.Resources.Trash_16x16
        Me.SupprimerCompteBailleurToolStripMenuItem.Name = "SupprimerCompteBailleurToolStripMenuItem"
        Me.SupprimerCompteBailleurToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.SupprimerCompteBailleurToolStripMenuItem.Text = "Supprimer "
        '
        'ViewPerson
        '
        Me.ViewPerson.ActiveFilterEnabled = False
        Me.ViewPerson.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewPerson.Appearance.Row.Options.UseFont = True
        Me.ViewPerson.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.ViewPerson.GridControl = Me.GridPerson
        Me.ViewPerson.Name = "ViewPerson"
        Me.ViewPerson.OptionsBehavior.Editable = False
        Me.ViewPerson.OptionsBehavior.ReadOnly = True
        Me.ViewPerson.OptionsCustomization.AllowColumnMoving = False
        Me.ViewPerson.OptionsCustomization.AllowFilter = False
        Me.ViewPerson.OptionsCustomization.AllowGroup = False
        Me.ViewPerson.OptionsCustomization.AllowSort = False
        Me.ViewPerson.OptionsFilter.AllowFilterEditor = False
        Me.ViewPerson.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.ViewPerson.OptionsPrint.AutoWidth = False
        Me.ViewPerson.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewPerson.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.ViewPerson.OptionsView.ColumnAutoWidth = False
        Me.ViewPerson.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ViewPerson.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.ViewPerson.OptionsView.ShowGroupPanel = False
        Me.ViewPerson.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[True]
        Me.ViewPerson.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ViewPerson.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
        '
        'BtEnregistrer
        '
        Me.BtEnregistrer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtEnregistrer.Appearance.Options.UseFont = True
        Me.BtEnregistrer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Save_32x32
        Me.BtEnregistrer.Location = New System.Drawing.Point(136, 206)
        Me.BtEnregistrer.Name = "BtEnregistrer"
        Me.BtEnregistrer.Size = New System.Drawing.Size(109, 34)
        Me.BtEnregistrer.TabIndex = 3
        Me.BtEnregistrer.Text = "Enregistrer"
        '
        'Fermer
        '
        Me.Fermer.Appearance.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fermer.Appearance.Options.UseFont = True
        Me.Fermer.Image = Global.ClearProject.My.Resources.Resources.Ribbon_Exit_32x32
        Me.Fermer.Location = New System.Drawing.Point(251, 206)
        Me.Fermer.Name = "Fermer"
        Me.Fermer.Size = New System.Drawing.Size(115, 34)
        Me.Fermer.TabIndex = 4
        Me.Fermer.Text = "Fermer"
        '
        'PersonenChargeProjet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(516, 380)
        Me.ControlBox = False
        Me.Controls.Add(Me.Fermer)
        Me.Controls.Add(Me.BtEnregistrer)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PersonenChargeProjet"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Personne contact en charge du projet "
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.Nom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Contact.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fonction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ville.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Adresse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Email.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.GridPerson, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.ViewPerson, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Fonction As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Ville As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Adresse As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Email As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents BtEnregistrer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridPerson As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewPerson As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Contact As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SupprimerCompteBailleurToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Nom As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fermer As DevExpress.XtraEditors.SimpleButton
End Class
