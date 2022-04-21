Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class PMPochetteDocument

    Dim dtDocument = New DataTable()
    Dim DrX As DataRow

    Dim tabPochette As String()
    Private Sub Service_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        InitFormulaire()
        CmbPochette.ResetText()
        CmbPochette.SelectedIndex = -1
        ChargerPochette()
        CmbPochette_SelectedIndexChanged(sender, e)
        ViewPochetteDocument.OptionsView.ColumnAutoWidth = True
        ViewPochetteDocument.OptionsBehavior.AutoExpandAllGroups = True
        ViewPochetteDocument.VertScrollVisibility = True
        ViewPochetteDocument.HorzScrollVisibility = True
        ViewPochetteDocument.BestFitColumns()
    End Sub

    Private Sub ChargerPochette()
        query = "select * from `t_typemarche` ORDER BY `CodeTypeMarche` ASC"
        CmbPochette.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        ReDim tabPochette(dt.Rows.Count)
        Dim cpte As Decimal = 0
        For Each rw As DataRow In dt.Rows
            CmbPochette.Properties.Items.Add(MettreApost(rw(1).ToString))
            tabPochette(cpte) = rw(0)
            cpte += 1
        Next
    End Sub

    Private Sub RemplirDocument(ByVal PochetteID As String)

        dtDocument.Columns.Clear()

        dtDocument.Columns.Add("N°", Type.GetType("System.String"))
        dtDocument.Columns.Add("Ref", Type.GetType("System.String"))
        dtDocument.Columns.Add("Document", Type.GetType("System.String"))

        Dim cptr As Decimal = 0

        query = "SELECT * FROM `t_pm_pochette_document` where CodeTypeMarche='" & PochetteID & "' order by POCHDOC_LIB ASC"
        dtDocument.Rows.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtDocument.NewRow()
            drS(0) = cptr
            drS(1) = rw(0).ToString
            drS(2) = MettreApost(rw(1).ToString)
            dtDocument.Rows.Add(drS)
        Next
        dtPochetteDocument.DataSource = dtDocument

        ViewPochetteDocument.Columns(0).Visible = True
        ViewPochetteDocument.Columns(1).Visible = False
        ViewPochetteDocument.Columns(0).Width = 30
        ViewPochetteDocument.Columns(2).Width = 250

        ViewPochetteDocument.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewPochetteDocument, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)

    End Sub
    Private Sub InitFormulaire()
        TxtLibDoc.Text = ""
        TxtLibDoc.Enabled = True
        BtEnrg.Enabled = True
        BtModif.Enabled = False
        BtEnrg.Enabled = True
        CmbPochette.Enabled = True
        Me.AcceptButton = BtEnrg
    End Sub

    Private Sub CmbPochette_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbPochette.SelectedIndexChanged
        If CmbPochette.SelectedIndex > -1 Then
            RemplirDocument(tabPochette(CmbPochette.SelectedIndex))
        Else
            RemplirDocument(-1)
        End If
    End Sub
    Private Sub dtPochetteDocument_Click(sender As System.Object, e As System.EventArgs) Handles dtPochetteDocument.Click

        If (ViewPochetteDocument.RowCount > 0) Then
            DrX = ViewPochetteDocument.GetDataRow(ViewPochetteDocument.FocusedRowHandle)

            Dim IDL = DrX(1).ToString
            ColorRowGrid(ViewPochetteDocument, "[N°]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewPochetteDocument, "[Ref]='" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)

            BtModif.Enabled = True
            BtEnrg.Enabled = False
            CmbPochette.Enabled = False
            TxtLibDoc.Text = MettreApost(DrX(2))
        End If
    End Sub
    Private Sub BtEnrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnrg.Click
        If CmbPochette.SelectedIndex = -1 Then
            MessageBox.Show("Veuillez choisir une pochette svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Trim(TxtLibDoc.Text) = "" Then
            MessageBox.Show("Veuillez entrer le nom du document svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtLibDoc.Focus()
            Exit Sub
        End If

        Try
            query = "INSERT INTO t_pm_pochette_document VALUES(null,'" & EnleverApost(Trim(TxtLibDoc.Text)) & "','" & tabPochette(CmbPochette.SelectedIndex) & "')"
            ExecuteNonQuery(query)
            MessageBox.Show("Document enregistré avec succès.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Information)
            InitFormulaire()
            CmbPochette_SelectedIndexChanged(sender, e)
        Catch my As MySqlException
            MessageBox.Show("Erreur : Imformation non disponible." & vbNewLine & my.ToString(), "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("L'enregistrement à échoué!", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub BtModif_Click(sender As System.Object, e As System.EventArgs) Handles BtModif.Click, ModifierService.Click
        If Trim(TxtLibDoc.Text) = "" Then
            MessageBox.Show("Veuillez entrer le nom du document svp.", "Formulaire incomplet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Try
            DrX = ViewPochetteDocument.GetDataRow(ViewPochetteDocument.FocusedRowHandle)
            query = "Update t_pm_pochette_document set POCHDOC_LIB='" & EnleverApost(Trim(TxtLibDoc.Text)) & "' where POCHDOC_ID=" & DrX(1)
            ExecuteNonQuery(query)
            InitFormulaire()
            CmbPochette_SelectedIndexChanged(sender, e)

        Catch my As MySqlException
            Failmsg("Erreur : Information non disponible : " & my.Message)
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub
    Private Sub SupprimerServiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerServiceToolStripMenuItem.Click
        If ViewPochetteDocument.RowCount > 0 Then
            DrX = ViewPochetteDocument.GetDataRow(ViewPochetteDocument.FocusedRowHandle)
            Dim IDL = DrX(1).ToString
            ColorRowGrid(ViewPochetteDocument, "[N°]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewPochetteDocument, "[Ref]='" & IDL & "'", Color.Navy, "Times New Roman", 11, FontStyle.Bold, Color.White, True)
            Dim Reponse As DialogResult = MessageBox.Show("Voulez-vous continuer supprimer [" & DrX(2).ToString & "] ?", "ClearProject", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If Reponse = DialogResult.Yes Then
                query = "delete from t_pm_pochette_document where POCHDOC_ID=" & DrX(1)
                ExecuteNonQuery(query)
                InitFormulaire()
                RemplirDocument(tabPochette(CmbPochette.SelectedIndex))
                CmbPochette_SelectedIndexChanged(sender, e)
                MessageBox.Show("Document supprimé avec succès.", "ClearProject", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MsgBox("Suppression Impossible!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Service_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        InitFormulaire()
    End Sub
    Private Sub Service_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        CmbPochette.Focus()
    End Sub

    Private Sub btRetour_Click(sender As Object, e As EventArgs) Handles btRetour.Click
        InitFormulaire()
        CmbPochette.Text = ""
        CmbPochette.SelectedIndex = -1
        CmbPochette_SelectedIndexChanged(sender, e)
    End Sub
End Class