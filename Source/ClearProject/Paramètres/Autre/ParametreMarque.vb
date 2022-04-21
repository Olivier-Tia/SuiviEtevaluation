Public Class ParametreMarque
    Private Sub ParametreMarque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Try
            query = "SELECT * FROM t_pa_marque"
            remplirTableauMarque(query)
            btnAnnuler_Click(Me, e)
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Sub remplirTableauMarque(ByVal requete As String)
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

            LgListMarque.DataSource = dtAutre
            If ViewMarque.Columns(1).Visible Then
                ViewMarque.OptionsBehavior.Editable = False
                ViewMarque.OptionsView.ColumnAutoWidth = True
                ViewMarque.OptionsBehavior.AutoExpandAllGroups = True
                ViewMarque.VertScrollVisibility = True
                ViewMarque.HorzScrollVisibility = True
                ViewMarque.BestFitColumns()
                ViewMarque.Columns(0).Width = 30
                ViewMarque.Columns(1).Width = 300
                ViewMarque.Columns(0).Visible = False
                ViewMarque.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
                ViewMarque.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Public Sub MiseAneuf()
        txtlibelle.Text = ""
    End Sub
    Private Sub btnEnregistrer_Click(sender As Object, e As EventArgs) Handles btnEnregistrer.Click
        If (txtlibelle.Text = "") Then
            FailMsg("Veuillez remplir le champ libellé !")
        Else
            query = "INSERT INTO t_pa_marque values (NULL,'" + EnleverApost(txtlibelle.Text) + "') "
            ExecuteNonQuery(query)
            SuccesMsg("Marque enregistré avec succès")
            query = "SELECT * FROM t_pa_marque"
            remplirTableauMarque(query)
            MiseAneuf()
        End If
    End Sub
    Private Sub btnModifier_Click(sender As Object, e As EventArgs) Handles btnModifier.Click
        If ConfirmMsg("Voulez-vous modifier cette marque") = DialogResult.Yes Then
            Dim id = ViewMarque.GetFocusedRowCellValue("ID")
            query = "Update t_pa_marque set MARQUE='" + EnleverApost(txtlibelle.Text) + "' where ID_MARQ='" + id + "'"
            ExecuteNonQuery(query)
            SuccesMsg("Enregistrement modifié avec succès.")
            query = "SELECT * FROM t_pa_marque"
            remplirTableauMarque(query)
            btnAnnuler_Click(Me, e)
        End If
        btnAnnuler_Click(Me, e)
    End Sub
    Private Sub btnSupprimer_Click(sender As Object, e As EventArgs) Handles btnSupprimer.Click
        query = "SELECT COUNT(*) FROM t_pa_vehicule v,t_pa_marque m WHERE v.ID_MARQ = m.ID_MARQ AND m.MARQUE ='" & EnleverApost(txtlibelle.Text) & "' "
        Dim result As Integer = Val(ExecuteScallar(query))
        If result > 0 Then
            SuccesMsg("Impossible de supprimer Cette Marque.")
            btnAnnuler_Click(Me, e)
            Exit Sub
        Else
            If ConfirmMsg("Voulez-vous supprimer cette marque ?") = DialogResult.Yes Then
                Dim id = ViewMarque.GetFocusedRowCellValue("ID")
                query = "DELETE FROM t_pa_marque where ID_MARQ='" + id + "'"
                ExecuteNonQuery(query)
                SuccesMsg("Marque supprimée avec succès.")
                query = "SELECT * FROM t_pa_marque"
                remplirTableauMarque(query)
                btnAnnuler_Click(Me, e)
            End If
            btnAnnuler_Click(Me, e)
        End If
    End Sub
    Private Sub LgListMarque_DoubleClick(sender As Object, e As EventArgs) Handles LgListMarque.DoubleClick
        btnEnregistrer.Enabled = False
        btnModifier.Enabled = True
        btnSupprimer.Enabled = True
        txtlibelle.Text = ViewMarque.GetFocusedRowCellValue("Libellé")
    End Sub

    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click
        MiseAneuf()
        btnModifier.Enabled = False
        btnSupprimer.Enabled = False
        btnEnregistrer.Enabled = True
    End Sub
    'Private Sub LgListMarque_Click(sender As Object, e As EventArgs) Handles LgListMarque.Click
    '    btnEnregistrer.Enabled = False
    '    btnModifier.Enabled = True
    '    btnSupprimer.Enabled = True
    '    txtlibelle.Text = ViewMarque.GetFocusedRowCellValue("Libellé")
    'End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs)

    End Sub

End Class