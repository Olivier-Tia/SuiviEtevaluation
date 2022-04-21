Public Class ParametreAssurance
    Private Sub ParametreAssurance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Try
            query = "SELECT * FROM t_pa_assurance"
            remplirTableauAssurance(query)
            btnAnnuler_Click(Me, e)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Public Sub MiseAneuf()
        txtLibelle.Text = ""
    End Sub
    Sub remplirTableauAssurance(ByVal requete As String)
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

            LgListAssurance.DataSource = dtAutre
            If ViewAssurance.Columns(1).Visible Then
                ViewAssurance.OptionsBehavior.Editable = False
                ViewAssurance.OptionsView.ColumnAutoWidth = True
                ViewAssurance.OptionsBehavior.AutoExpandAllGroups = True
                ViewAssurance.VertScrollVisibility = True
                ViewAssurance.HorzScrollVisibility = True
                ViewAssurance.BestFitColumns()
                ViewAssurance.Columns(0).Width = 30
                ViewAssurance.Columns(1).Width = 300
                ViewAssurance.Columns(0).Visible = False
                ViewAssurance.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
                ViewAssurance.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub btnEnregistrer_Click(sender As Object, e As EventArgs) Handles btnEnregistrer.Click
        If (txtLibelle.Text = "") Then
            FailMsg("Veuillez remplir le champ libellé !")
        Else
            query = "INSERT INTO t_pa_assurance values (NULL,'" + EnleverApost(txtLibelle.Text) + "') "
            ExecuteNonQuery(query)
            SuccesMsg("Assurance  enregistré avec succès")
            query = "SELECT * FROM t_pa_assurance"
            remplirTableauAssurance(query)
            MiseAneuf()
        End If
    End Sub

    Private Sub btnModifier_Click(sender As Object, e As EventArgs) Handles btnModifier.Click
        If ConfirmMsg("Voulez-vous modifier cette assurance ?") = DialogResult.Yes Then
            Dim id = ViewAssurance.GetFocusedRowCellValue("ID")
            query = "Update t_pa_assurance set ASSURANCE='" + EnleverApost(txtLibelle.Text) + "' where ID_ASS='" + id + "'"
            ExecuteNonQuery(query)
            SuccesMsg("Enregistrement modifié avec succès.")
            query = "SELECT * FROM t_pa_assurance"
            remplirTableauAssurance(query)
            btnAnnuler_Click(Me, e)
        End If
        btnAnnuler_Click(Me, e)
    End Sub

    Private Sub btnSupprimer_Click(sender As Object, e As EventArgs) Handles btnSupprimer.Click
        query = "SELECT COUNT(*) FROM t_pa_contrat_assurance v, t_pa_assurance c WHERE v.ID_ASS = c.ID_ASS And c.ASSURANCE ='" & EnleverApost(txtLibelle.Text) & "' "
        Dim result As Integer = Val(ExecuteScallar(query))
        If result > 0 Then
            SuccesMsg("Impossible de supprimer cette assurance.")
            btnAnnuler_Click(Me, e)
            Exit Sub
        Else
            If ConfirmMsg("Voulez-vous supprimer cette assurance ?") = DialogResult.Yes Then
                Dim id = ViewAssurance.GetFocusedRowCellValue("ID")
                query = "DELETE FROM t_pa_assurance where ID_ASS='" + id + "'"
                ExecuteNonQuery(query)
                SuccesMsg("Assurance supprimée avec succès.")
                query = "SELECT * FROM t_pa_assurance"
                remplirTableauAssurance(query)
                btnAnnuler_Click(Me, e)
            End If
            btnAnnuler_Click(Me, e)
        End If
    End Sub
    Private Sub ParametreAssurance_DoubleClick(sender As Object, e As EventArgs) Handles LgListAssurance.DoubleClick
        btnEnregistrer.Enabled = False
        btnModifier.Enabled = True
        btnSupprimer.Enabled = True
        txtLibelle.Text = ViewAssurance.GetFocusedRowCellValue("Libellé")
    End Sub
    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs)
        MiseAneuf()
        btnModifier.Enabled = False
        btnSupprimer.Enabled = False
        btnEnregistrer.Enabled = True
    End Sub


End Class