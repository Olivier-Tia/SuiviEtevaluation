Public Class ParametreTypeMateriel
    Private Sub ParametreTypeMateriel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Try
            query = "SELECT * FROM t_pa_type_vehicule"
            remplirTableauTypeMateriel(query)
            btnAnnuler_Click(Me, e)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Sub remplirTableauTypeMateriel(ByVal requete As String)
        Dim dtAutre As New DataTable
        Try
            dtAutre.Columns.Clear()
            dtAutre.Columns.Add("ID", Type.GetType("System.String"))
            dtAutre.Columns.Add("Libellé", Type.GetType("System.String"))
            dtAutre.Rows.Clear()
            Dim cptr As Integer = 0
            Dim dt As DataTable = ExcecuteSelectQuery(requete)
            For Each rw In dt.Rows
                cptr += 1
                Dim drS = dtAutre.NewRow()
                drS(0) = rw(0).ToString
                drS(1) = MettreApost(rw(1).ToString)
                dtAutre.Rows.Add(drS)
            Next

            LgListTypeMateriel.DataSource = dtAutre
            If ViewTypeMateriel.Columns(1).Visible Then
                ViewTypeMateriel.OptionsBehavior.Editable = False
                ViewTypeMateriel.OptionsView.ColumnAutoWidth = True
                ViewTypeMateriel.OptionsBehavior.AutoExpandAllGroups = True
                ViewTypeMateriel.VertScrollVisibility = True
                ViewTypeMateriel.HorzScrollVisibility = True
                ViewTypeMateriel.BestFitColumns()
                ViewTypeMateriel.Columns(0).Width = 30
                ViewTypeMateriel.Columns(1).Width = 300
                ViewTypeMateriel.Columns(0).Visible = False
                ViewTypeMateriel.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
                ViewTypeMateriel.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try

    End Sub
    Public Sub MiseAneuf()
        txtLibelle.Text = ""
    End Sub
    Private Sub btnEnregistrer_Click(sender As Object, e As EventArgs) Handles btnEnregistrer.Click
        If (txtLibelle.Text = "") Then
            FailMsg("Veuillez remplir le champ libellé !")
        Else
            query = "INSERT INTO t_pa_type_vehicule values (NULL,'" + EnleverApost(txtLibelle.Text) + "') "
            ExecuteNonQuery(query)
            SuccesMsg("Type de matériel enregistré avec succès")
            query = "SELECT * FROM t_pa_type_vehicule"
            remplirTableauTypeMateriel(query)
            MiseAneuf()
        End If
    End Sub
    Private Sub btnModifier_Click(sender As Object, e As EventArgs) Handles btnModifier.Click
        If ConfirmMsg("Voulez-vous modifier ce type de matériel") = DialogResult.Yes Then
            Dim id = ViewTypeMateriel.GetFocusedRowCellValue("ID")
            query = "Update t_pa_energie set ENERGIE='" + EnleverApost(txtLibelle.Text) + "' where ID_TYP_VEH='" + id + "'"
            ExecuteNonQuery(query)
            SuccesMsg("Source d'energie modifiée avec succès.")
            query = "SELECT * FROM t_pa_type_vehicule"
            remplirTableauTypeMateriel(query)
            btnAnnuler_Click(Me, e)
        End If
        btnAnnuler_Click(Me, e)
    End Sub
    Private Sub btnSupprimer_Click(sender As Object, e As EventArgs) Handles btnSupprimer.Click
        query = "SELECT COUNT(*) FROM t_pa_vehicule v,t_pa_type_vehicule t WHERE v.ID_TYP_VEH = t.ID_TYP_VEH AND t.TYP_VEH = '" & EnleverApost(txtLibelle.Text) & "'"
        Dim result As Integer = Val(ExecuteScallar(query))
        If result > 0 Then
            SuccesMsg("Impossible de supprimer Ce type de matériel.")
            btnAnnuler_Click(Me, e)
            Exit Sub
        Else
            If ConfirmMsg("Voulez-vous supprimer ce type de matériel ?") = DialogResult.Yes Then
                Dim id = ViewTypeMateriel.GetFocusedRowCellValue("ID")
                query = "DELETE FROM t_pa_type_vehicule where ID_TYP_VEH='" + id + "'"
                ExecuteNonQuery(query)
                SuccesMsg("Type de matériel supprimée avec succès.")
                query = "SELECT * FROM t_pa_type_vehicule"
                remplirTableauTypeMateriel(query)
                btnAnnuler_Click(Me, e)
            End If
            btnAnnuler_Click(Me, e)
        End If

    End Sub
    Private Sub LgListTypeMateriel_DoubleClick(sender As Object, e As EventArgs) Handles LgListTypeMateriel.DoubleClick
        btnEnregistrer.Enabled = False
        btnModifier.Enabled = True
        btnSupprimer.Enabled = True
        txtLibelle.Text = ViewTypeMateriel.GetFocusedRowCellValue("Libellé")
    End Sub

    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click
        MiseAneuf()
        btnModifier.Enabled = False
        btnSupprimer.Enabled = False
        btnEnregistrer.Enabled = True
    End Sub



End Class