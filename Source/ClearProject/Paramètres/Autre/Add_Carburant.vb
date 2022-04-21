Public Class Add_Carburant
    Sub remplirTableauEnergie(ByVal requete As String)
        Dim dtAutre As New DataTable
        Try
            dtAutre.Columns.Clear()

            dtAutre.Columns.Add("ID", Type.GetType("System.String"))
            dtAutre.Columns.Add("Libellé", Type.GetType("System.String"))
            dtAutre.Columns.Add("Prix Unitaire", Type.GetType("System.String"))
            dtAutre.Rows.Clear()
            Dim cptr As Integer = 0
            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            For Each rw In dt.Rows
                cptr += 1
                Dim drS = dtAutre.NewRow()
                drS(0) = rw(0).ToString
                drS(1) = MettreApost(rw(1).ToString)
                drS(2) = AfficherMonnaie(rw(2).ToString)
                dtAutre.Rows.Add(drS)
            Next

            LgListSourceEnergie.DataSource = dtAutre
            If ViewSourceEnergie.Columns(1).Visible Then
                ViewSourceEnergie.OptionsBehavior.Editable = False
                ViewSourceEnergie.OptionsView.ColumnAutoWidth = True
                ViewSourceEnergie.OptionsBehavior.AutoExpandAllGroups = True
                ViewSourceEnergie.VertScrollVisibility = True
                ViewSourceEnergie.HorzScrollVisibility = True
                ViewSourceEnergie.BestFitColumns()
                ViewSourceEnergie.Columns(0).Width = 30
                ViewSourceEnergie.Columns(1).Width = 225
                ViewSourceEnergie.Columns(2).Width = 225
                ViewSourceEnergie.Columns(0).Visible = False
                ViewSourceEnergie.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
                ViewSourceEnergie.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try

    End Sub
    Private Sub Add_Carburant_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        'LesAutresRapport()
        Try
            query = "SELECT * FROM t_pa_energie"
            remplirTableauEnergie(query)
            btnAnnuler_Click(Me, e)
        Catch ex As Exception
            MsgBox("Erreur : Information non disponible : " & ex.ToString(), MsgBoxStyle.Exclamation, "ClearProject")
        End Try
    End Sub
    Shared Function form() As Object
        Throw New NotImplementedException
    End Function
    Public Sub MiseAneuf()
        NomEnergie.Text = ""
        PrixEnergie.Text = ""
    End Sub


    Private Sub btnEnregistrer_Click(sender As Object, e As EventArgs) Handles btnEnregistrer.Click
        Dim erreur As String = ""

        'si le nom de l'energie n'est pas renseigné
        If NomEnergie.Text = "" Then
            erreur += "- Libellé" + ControlChars.CrLf
        End If
        'si le prix n'est pas renseigné
        If PrixEnergie.Text = "" Then
            erreur += "- Prix Unitaire" + ControlChars.CrLf
        End If


        If erreur = "" Then
            query = "INSERT INTO t_pa_energie values (NULL,'" + EnleverApost(NomEnergie.Text) + "','" + PrixEnergie.Text + "') "
            ExecuteNonQuery(query)
            SuccesMsg("Energie enregistré avec succès")
            query = "SELECT * FROM t_pa_energie"
            remplirTableauEnergie(query)
            btnAnnuler_Click(Me, e)
        Else
            FailMsg("Veuillez remplir ces champs : " + ControlChars.CrLf + erreur)
        End If
    End Sub

    Private Sub btnModifier_Click(sender As Object, e As EventArgs) Handles btnModifier.Click
        If ConfirmMsg("Voulez-vous modifier cette source d'energie") = DialogResult.Yes Then
            Dim id = ViewSourceEnergie.GetFocusedRowCellValue("ID")
            query = "Update t_pa_energie set ENERGIE='" + EnleverApost(NomEnergie.Text) + "',PU='" + PrixEnergie.Text + "' where ID_ENERG='" + id + "'"
            ExecuteNonQuery(query)
            SuccesMsg("Source d'energie modifiée avec succès.")
            query = "SELECT * FROM t_pa_energie"
            remplirTableauEnergie(query)
            btnAnnuler_Click(Me, e)
        End If
        btnAnnuler_Click(Me, e)
    End Sub
    Private Sub btnSupprimer_Click(sender As Object, e As EventArgs) Handles btnSupprimer.Click
        query = "SELECT COUNT(*) FROM t_pa_vehicule v,t_pa_energie e WHERE v.ID_ENERG = e.ID_ENERG AND e.ENERGIE ='" & EnleverApost(NomEnergie.Text) & "'"
        Dim result As Integer = Val(ExecuteScallar(query))
        If result > 0 Then
            SuccesMsg("Impossible de supprimer Cette énergie.")
            btnAnnuler_Click(Me, e)
            Exit Sub
        Else
            If ConfirmMsg("Voulez-vous supprimer cette source d'energie ?") = DialogResult.Yes Then
                Dim id = ViewSourceEnergie.GetFocusedRowCellValue("ID")
                query = "DELETE FROM t_pa_energie where ID_ENERG='" + id + "'"
                ExecuteNonQuery(query)
                SuccesMsg("Source d'energie supprimée avec succès.")
                query = "SELECT * FROM t_pa_energie"
                remplirTableauEnergie(query)
                btnAnnuler_Click(Me, e)
            End If
            btnAnnuler_Click(Me, e)
        End If

    End Sub

    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click
        MiseAneuf()
        btnModifier.Enabled = False
        btnSupprimer.Enabled = False
        btnEnregistrer.Enabled = True
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub LgListSourceEnergie_DoubleClick(sender As Object, e As EventArgs) Handles LgListSourceEnergie.DoubleClick
        btnEnregistrer.Enabled = False
        btnModifier.Enabled = True
        btnSupprimer.Enabled = True
        NomEnergie.Text = ViewSourceEnergie.GetFocusedRowCellValue("Libellé")
        PrixEnergie.Text = CDbl(ViewSourceEnergie.GetFocusedRowCellValue("Prix Unitaire"))
    End Sub
    'Private Sub LgListSourceEnergie_Click(sender As Object, e As EventArgs) Handles LgListSourceEnergie.Click
    '    btnEnregistrer.Enabled = False
    '    btnModifier.Enabled = True
    '    btnSupprimer.Enabled = True
    '    NomEnergie.Text = ViewSourceEnergie.GetFocusedRowCellValue("Libellé")
    '    PrixEnergie.Text = CDbl(ViewSourceEnergie.GetFocusedRowCellValue("Prix Unitaire"))
    'End Sub

End Class