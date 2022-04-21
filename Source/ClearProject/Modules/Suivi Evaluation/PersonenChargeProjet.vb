Imports MySql.Data.MySqlClient

Public Class PersonenChargeProjet
    Dim dtPers = New DataTable()
    Dim DejaEnregistrer As Boolean = False
    Dim Index As Integer = 0

    Private Sub PersonenChargeProjet_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        '  BtAnnuler_Click(Me, e)
    End Sub

    Private Sub PersonenChargeProjet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        ChargerListePersonProjet()
    End Sub

    Private Sub ChargerListePersonProjet()
        dtPers.Columns.Clear()
        dtPers.Columns.Add("Code", Type.GetType("System.String"))
        dtPers.Columns.Add("RefPers", Type.GetType("System.String"))
        dtPers.Columns.Add("Nom", Type.GetType("System.String"))
        dtPers.Columns.Add("Contact", Type.GetType("System.String"))
        dtPers.Columns.Add("Adresse", Type.GetType("System.String"))
        dtPers.Columns.Add("Fonction", Type.GetType("System.String"))
        dtPers.Columns.Add("Ville", Type.GetType("System.String"))
        dtPers.Columns.Add("Email", Type.GetType("System.String"))
        dtPers.Columns.Add("LigneModif", Type.GetType("System.String"))
        dtPers.Rows.Clear()

        Dim NbTotal As Integer = 0
        query = "select * from t_personenchargeprojet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            NbTotal += 1
            Dim drS = dtPers.NewRow()

            drS("Code") = IIf(CDec(NbTotal / 2) <> CDec(NbTotal \ 2), "x", "").ToString
            drS("RefPers") = rw("RefPers")
            drS("Nom") = MettreApost(rw("Nom").ToString)
            drS("Contact") = MettreApost(rw("Contact").ToString)
            drS("Adresse") = MettreApost(rw("Adresse").ToString)
            drS("Fonction") = MettreApost(rw("Fonct").ToString)
            drS("Ville") = MettreApost(rw("VillePays").ToString)
            drS("Email") = MettreApost(rw("Email").ToString)
            drS("LigneModif") = "Enregistrer"

            dtPers.Rows.Add(drS)
        Next

        GridPerson.DataSource = dtPers

        ViewPerson.Columns("Code").Visible = False
        ViewPerson.Columns("RefPers").Visible = False
        ViewPerson.Columns("LigneModif").Visible = False

        ViewPerson.Columns("Nom").Width = 200
        ViewPerson.Columns("Contact").Width = 100
        ViewPerson.Columns("Adresse").Width = 100
        ViewPerson.Columns("Fonction").Width = 150
        ViewPerson.Columns("Ville").Width = 150
        ViewPerson.Columns("Email").Width = 150

        ViewPerson.Appearance.Row.Font = New Font("Times New Roman", 11, FontStyle.Regular)
        ColorRowGrid(ViewPerson, "[Code]='x'", Color.LightGray, "Times New Roman", 11, FontStyle.Regular, Color.Black)
    End Sub

    Private Sub GridPerson_Click(sender As System.Object, e As System.EventArgs) Handles GridPerson.Click
        If (ViewPerson.RowCount > 0) Then
            drx = ViewPerson.GetDataRow(ViewPerson.FocusedRowHandle)
            Dim IDL = drx("RefPers").ToString
            ColorRowGrid(ViewPerson, "[Code]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ' ColorRowGridAnal(ViewPerson, "[RefPers]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub GridPerson_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridPerson.DoubleClick
        If ViewPerson.RowCount > 0 Then
            drx = ViewPerson.GetDataRow(ViewPerson.FocusedRowHandle)
            Index = ViewPerson.FocusedRowHandle
            DejaEnregistrer = True

            Nom.Text = drx("Nom").ToString
            Email.Text = drx("Email").ToString
            Adresse.Text = drx("Adresse").ToString
            Ville.Text = drx("Ville").ToString
            Fonction.Text = drx("Fonction").ToString
            Contact.Text = drx("Contact").ToString
        End If
    End Sub


    Private Sub Email_KeyDown(sender As Object, e As KeyEventArgs) Handles Email.KeyDown, Nom.KeyDown, Fonction.KeyDown, Contact.KeyDown, Ville.KeyDown, Adresse.KeyDown
        If e.KeyCode = Keys.Enter Then

            If Nom.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir le nom")
                Nom.Focus()
                Exit Sub
            End If

            If Adresse.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir l'adresse")
                Adresse.Focus()
                Exit Sub
            End If
            If Fonction.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir la fonction")
                Fonction.Focus()
                Exit Sub
            End If
            If Ville.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir la ville")
                Ville.Focus()
                Exit Sub
            End If
            If Contact.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir le contact")
                Contact.Focus()
                Exit Sub
            End If
            If Email.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir l'adresse mail")
                Email.Focus()
                Exit Sub
            End If

            If DejaEnregistrer = False Then
                Dim dt As DataTable = GridPerson.DataSource
                Dim Nbres = ViewPerson.RowCount - 1

                Dim NewLigne = dt.NewRow()

                NewLigne("Code") = IIf(Nbres + 1 Mod 2 = 0, "x", "").ToString
                NewLigne("RefPers") = ""
                NewLigne("Nom") = Nom.Text
                NewLigne("Fonction") = Fonction.Text
                NewLigne("Ville") = Ville.Text
                NewLigne("Contact") = Contact.Text
                NewLigne("Email") = Email.Text
                NewLigne("Adresse") = Adresse.Text
                NewLigne("LigneModif") = ""

                dt.Rows.Add(NewLigne)
            Else
                ' ViewPerson.GetDataRow(Index).Item("RefPers") = ""
                ViewPerson.GetDataRow(Index).Item("Nom") = Nom.Text
                ViewPerson.GetDataRow(Index).Item("Fonction") = Fonction.Text
                ViewPerson.GetDataRow(Index).Item("Ville") = Ville.Text
                ViewPerson.GetDataRow(Index).Item("Contact") = Contact.Text
                ViewPerson.GetDataRow(Index).Item("Email") = Email.Text
                ViewPerson.GetDataRow(Index).Item("Adresse") = Adresse.Text
                ViewPerson.GetDataRow(Index).Item("LigneModif") = "Modifier"
            End If
            DejaEnregistrer = False
            Initialiser()
            ColorRowGrid(ViewPerson, "[Code]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
        End If
    End Sub

    Private Sub Initialiser()
        Fonction.Text = ""
        Ville.Text = ""
        Contact.Text = ""
        Email.Text = ""
        Adresse.Text = ""
        Nom.Text = ""
    End Sub
    Private Sub SupprimerCompteBailleurToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerCompteBailleurToolStripMenuItem.Click
        If ViewPerson.FocusedRowHandle <> -1 And ViewPerson.RowCount > 0 Then
            If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then
                drx = ViewPerson.GetDataRow(ViewPerson.FocusedRowHandle)

                If drx("RefPers").ToString <> "" Then
                    query = "DELETE FROM t_personenchargeprojet WHERE RefPers = '" & drx("RefPers").ToString & "' and CodeProjet='" & ProjetEnCours & "'"
                    ExecuteNonQuery(query)
                End If

                ViewPerson.GetDataRow(ViewPerson.FocusedRowHandle).Delete()

                SuccesMsg("Suppression effectuée avec succès")
                ChargerListePersonProjet()
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If ViewPerson.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub BtEnregistrer_Click(sender As Object, e As EventArgs) Handles BtEnregistrer.Click
        If ViewPerson.RowCount > 0 Then
            For i = 0 To ViewPerson.RowCount - 1
                If ViewPerson.GetRowCellValue(i, "RefPers").ToString = "" Then
                    query = "INSERT INTO t_personenchargeprojet values(NULL,'" & EnleverApost(ViewPerson.GetRowCellValue(i, "Nom").ToString) & "', '" & EnleverApost(ViewPerson.GetRowCellValue(i, "Fonction").ToString) & "', '" & ViewPerson.GetRowCellValue(i, "Contact").ToString & "', '" & EnleverApost(ViewPerson.GetRowCellValue(i, "Adresse").ToString) & "', '" & EnleverApost(ViewPerson.GetRowCellValue(i, "Ville").ToString) & "', '" & EnleverApost(ViewPerson.GetRowCellValue(i, "Email").ToString) & "','" & dateconvert(Now.ToString) & "', '" & dateconvert(Now.ToString) & "', '" & CodeOperateurEnCours & "', '" & ProjetEnCours & "')"
                    ExecuteNonQuery(query)
                ElseIf ViewPerson.GetRowCellValue(i, "RefPers").ToString <> "" And ViewPerson.GetRowCellValue(i, "LigneModif").ToString = "Modifier" Then
                    query = "Update t_personenchargeprojet set Nom='" & EnleverApost(ViewPerson.GetRowCellValue(i, "Nom").ToString) & "', Fonct='" & EnleverApost(ViewPerson.GetRowCellValue(i, "Fonction").ToString) & "', Contact='" & ViewPerson.GetRowCellValue(i, "Contact").ToString & "', Adresse='" & EnleverApost(ViewPerson.GetRowCellValue(i, "Adresse").ToString) & "', VillePays='" & EnleverApost(ViewPerson.GetRowCellValue(i, "Ville").ToString) & "', Email='" & EnleverApost(ViewPerson.GetRowCellValue(i, "Email").ToString) & "',DateModif='" & dateconvert(Now.ToString) & "', CodeUtilisateur='" & CodeOperateurEnCours & "' where CodeProjet='" & ProjetEnCours & "' and RefPers='" & ViewPerson.GetRowCellValue(i, "RefPers").ToString & "'"
                    ExecuteNonQuery(query)
                End If
            Next
            SuccesMsg("Enregistrement effectué avec succès")
            ChargerListePersonProjet()
        End If
    End Sub

    Private Sub Continuer_Click(sender As Object, e As EventArgs) Handles Fermer.Click
        Me.Close()
    End Sub
End Class