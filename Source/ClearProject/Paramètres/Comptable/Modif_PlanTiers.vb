Public Class Modif_PlanTiers 
    Dim dtcontact = New DataTable()
    Dim dtBanque = New DataTable()
    Dim idtcpt As String
    Private Class1 As New TiersClass

    Private Sub Modif_PlanTiers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        'remplir localité
        RemplirCombo2(Combville, "t_zonegeo", "libellezone")

        'remplir service
        RemplirCombo2(Combserv, "t_service", "nomservice")

        'remplir service
        RemplirCombo2(Combdev, "T_Devise", "AbregeDevise")

        'remplir les sous classe du plan comptable
        Combsc.Properties.Items.Clear()
        query = "select * from T_COMP_SOUS_CLASSE where code_sc like '4%' ORDER BY code_sc"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rwx As DataRow In dt.Rows
            Combsc.Properties.Items.Add(rwx(0).ToString + "   " + MettreApost(rwx(2).ToString))
        Next

        'remplir le type compte
        cmbType.Properties.Items.Clear()
        query = "select * from T_COMP_TYPE_COMPTE ORDER BY Code_CL"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cmbType.Properties.Items.Add(rw("Code_CL") & " | " & MettreApost(rw("libelle_tcpt")).ToString)
        Next
        txtCodeTiers.Enabled = False
        cmbType.Enabled = False
        txtIntitule.Focus()
        'txtCodeTiers.Enabled = False
    End Sub

    Private Sub btenregisterj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btenregisterj.Click
        Try

            Dim erreur As String = ""
            Dim sc() As String
            sc = Combsc.Text.Split("   ")

            If txtCodeTiers.Text = "" Then
                erreur += "- Numéro Compte" + ControlChars.CrLf
            End If
            'If Combsc.SelectedIndex = -1 Then
            '    erreur += "- Compte sélectif" + ControlChars.CrLf
            'End If
            If txtIntitule.Text = "" Then
                erreur += "- Nom Compte tier" + ControlChars.CrLf
            End If

            If cmbType.SelectedIndex = -1 Then
                erreur += "- Type Compte Tier" + ControlChars.CrLf
            End If

            If erreur = "" Then

                query = "Update t_comp_compte set abrege_cpt='" + EnleverApost(Txtabr.Text) + "', nom_cpt='" + EnleverApost(txtIntitule.Text) + "', adresse_cpt='" + EnleverApost(txtadresse.Text) + "', telephone='" + txtcontact.Text + "', email='" + EnleverApost(txtmail.Text) + "', rc='" + Txtrc.Text + "', cc='" + Txtcc.Text + "', telephone2='" + Txttel.Text + "', fax='" + Txtfax.Text + "', site='" + Txtsite.Text + "', qualite='" + Txtqualite.Text + "' where code_cpt='" + txtCodeTiers.Text + "' and code_projet='" + ProjetEnCours + "'"
                ExecuteNonQuery(query)

                'sql1 = "Update  t_comp_rattach_tiers set code_sc='" + sc(0).ToString + "', cpt_tier='" + EnleverApost(Txtabr.Text) + "'  where code_cpt='" + txtcpt.Text + "' and code_projet='" + ProjetEnCours + "'"
                'ExecuteNonQuery(query)

                'insertion de la banque et leur rattache
                For i = 0 To GridView1.RowCount - 1

                    DeleteRecords2("T_COMP_RATTACH_BANQ", "RIBBANQ", GridView1.GetRowCellValue(i, "RIB").ToString())
                    DeleteRecords2("T_COMP_BANQUE", "RIBBANQ", GridView1.GetRowCellValue(i, "RIB").ToString())

                    'insertion dans la table banque
                   query= "insert into T_COMP_BANQUE values (NULL,'" + EnleverApost(GridView1.GetRowCellValue(i, "Banque").ToString()) + "','" + EnleverApost(GridView1.GetRowCellValue(i, "RIB").ToString()) + "','" + EnleverApost(GridView1.GetRowCellValue(i, "Devise").ToString()) + "','" + ProjetEnCours + "')"
                    ExecuteNonQuery(query)

                    'insertion dans la table ratach_banq
                   query= "insert into T_COMP_RATTACH_BANQ values('" + EnleverApost(GridView1.GetRowCellValue(i, "RIB").ToString()) + "','" + EnleverApost(txtCodeTiers.Text) + "')"
                    ExecuteNonQuery(query)

                Next

                For i = 0 To ViewContact.RowCount - 1
                    'insertion dans la table banque
                   query= "insert into T_COMP_IDENTIFIANT values (NULL,'" + EnleverApost(txtCodeTiers.Text) + "','" + EnleverApost(dtcontact.Rows(i).item(0).ToString) + "','" + EnleverApost(dtcontact.Rows(i).item(2).ToString) + "','" + EnleverApost(dtcontact.Rows(i).item(4).ToString) + "','" + EnleverApost(dtcontact.Rows(i).item(5).ToString) + "','" + EnleverApost(dtcontact.Rows(i).item(6).ToString) + "','" + ProjetEnCours + "','" + EnleverApost(dtcontact.Rows(i).item(3).ToString) + "','" + EnleverApost(dtcontact.Rows(i).item(1).ToString) + "')"
                    ExecuteNonQuery(query)
                Next

                EffacerTexBox4(PanelControl5)
                'actualiser la table
               query= "select count(*) from t_comp_compte where CODE_PROJET='" + ProjetEnCours + "'"
                Dim nbre = ExecuteScallar(query)
                Plan_tiers.CmbPageSize.Text = 25
                With Class1

                    .PageSize = Plan_tiers.CmbPageSize.Text
                    .MaxRec = nbre \ .PageSize
                    .PageCount = .MaxRec \ .PageSize
                    If (.MaxRec Mod .PageSize) > 0 Then
                        .PageCount = .PageCount + 1
                    End If

                    .CurrentPage = 1
                    .Resqlconno = 0
                    .LoadPage(Plan_tiers.LgListCompteTier, .CurrentPage)
                End With

                Plan_tiers.ViewCptTiers.Columns(0).Visible = True
                Plan_tiers.ViewCptTiers.Columns(0).Width = 20
                Plan_tiers.ViewCptTiers.OptionsView.ColumnAutoWidth = True
                Plan_tiers.ViewCptTiers.OptionsBehavior.AutoExpandAllGroups = True
                Plan_tiers.ViewCptTiers.VertScrollVisibility = True
                Plan_tiers.ViewCptTiers.HorzScrollVisibility = True
                Plan_tiers.ViewCptTiers.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)

                SuccesMsg("Modification Effectuée avec Succès.")
                Me.Close()
            Else
                SuccesMsg("Veuillez remplir ces champs : " + ControlChars.CrLf + erreur)
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub txtnom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIntitule.TextChanged
        If (txtIntitule.Text.Replace(" ", "") <> "") Then

            Dim partS() As String = (txtIntitule.Text.Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "")).Split(" "c)
            Dim CodeS As String = ""
            For Each elt In partS
                CodeS = CodeS & Mid(elt, 1, 1).ToUpper
            Next
            Txtabr.Text = CodeS
        Else
            Txtabr.Text = ""
        End If
    End Sub

    Private Sub LgListCumul_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListCumul.Click
        Try
            If (ViewCumul.RowCount > 0) Then
                drx = ViewCumul.GetDataRow(ViewCumul.FocusedRowHandle)
                Dim ID = drx(0).ToString
                ColorRowGrid(ViewCumul, "[Période]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewCumul, "[Période]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub LgListCumul_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LgListCumul.MouseUp
        Try
            If (ViewCumul.RowCount > 0) Then
                drx = ViewCumul.GetDataRow(ViewCumul.FocusedRowHandle)
                Dim ID = drx(0).ToString
                ColorRowGrid(ViewCumul, "[Période]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewCumul, "[Période]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Combserv_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combserv.SelectedIndexChanged
        Try

            query = "select f.LibelleFonction from t_service s,t_fonction f where s.codeservice=f.codeservice and s.NomService='" + EnleverApost(Combserv.Text) + "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                Txtfonct.Text = MettreApost(rwx(0).ToString)
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Txtemail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txtemail.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                dtcontact.Columns.Add("Email", Type.GetType("System.String"))

                Dim drS = dtcontact.NewRow()
                drS(0) = Txtnomc.Text & " " & Txtprenom.Text
                drS(1) = Combserv.Text
                drS(2) = Txtfonct.Text
                drS(3) = Txttelc.Text
                drS(4) = TxtPort.Text
                drS(5) = Txtfaxc.Text
                drS(6) = Txtemail.Text
                dtcontact.Rows.Add(drS)
                LgListContact.DataSource = dtcontact

                ViewContact.OptionsView.ColumnAutoWidth = True
                ViewContact.OptionsBehavior.AutoExpandAllGroups = True
                ViewContact.VertScrollVisibility = True
                ViewContact.HorzScrollVisibility = True
                ViewContact.BestFitColumns()

                EffacerTexBox10(GroupBox4)
                EffacerTexBox4(PanelControl4)
            Case Else
        End Select
    End Sub

    Private Sub Txtfaxc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txtfaxc.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                dtcontact.Columns.Add("Email", Type.GetType("System.String"))

                Dim drS = dtcontact.NewRow()
                drS(0) = Txtnomc.Text + " " + Txtprenom.Text
                drS(1) = Combserv.Text
                drS(2) = Txtfonct.Text
                drS(3) = Txttelc.Text
                drS(4) = TxtPort.Text
                drS(5) = Txtfaxc.Text
                drS(6) = Txtemail.Text
                dtcontact.Rows.Add(drS)
                LgListContact.DataSource = dtcontact

                ViewContact.OptionsView.ColumnAutoWidth = True
                ViewContact.OptionsBehavior.AutoExpandAllGroups = True
                ViewContact.VertScrollVisibility = True
                ViewContact.HorzScrollVisibility = True
                ViewContact.BestFitColumns()

                EffacerTexBox10(GroupBox4)
                EffacerTexBox4(PanelControl4)
            Case Else
        End Select
    End Sub

    Private Sub TxtPort_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPort.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                dtcontact.Columns.Add("Email", Type.GetType("System.String"))

                Dim drS = dtcontact.NewRow()
                drS(0) = Txtnomc.Text + " " + Txtprenom.Text
                drS(1) = Combserv.Text
                drS(2) = Txtfonct.Text
                drS(3) = Txttelc.Text
                drS(4) = TxtPort.Text
                drS(5) = Txtfaxc.Text
                drS(6) = Txtemail.Text
                dtcontact.Rows.Add(drS)
                LgListContact.DataSource = dtcontact

                ViewContact.OptionsView.ColumnAutoWidth = True
                ViewContact.OptionsBehavior.AutoExpandAllGroups = True
                ViewContact.VertScrollVisibility = True
                ViewContact.HorzScrollVisibility = True
                ViewContact.BestFitColumns()

                EffacerTexBox10(GroupBox4)
                EffacerTexBox4(PanelControl4)
            Case Else
        End Select
    End Sub

    Private Sub Txttelc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txttelc.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
                dtcontact.Columns.Add("Service", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
                dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
                dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
                dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
                dtcontact.Columns.Add("Email", Type.GetType("System.String"))

                Dim drS = dtcontact.NewRow()
                drS(0) = Txtnomc.Text + " " + Txtprenom.Text
                drS(1) = Combserv.Text
                drS(2) = Txtfonct.Text
                drS(3) = Txttelc.Text
                drS(4) = TxtPort.Text
                drS(5) = Txtfaxc.Text
                drS(6) = Txtemail.Text
                dtcontact.Rows.Add(drS)
                LgListContact.DataSource = dtcontact

                ViewContact.OptionsView.ColumnAutoWidth = True
                ViewContact.OptionsBehavior.AutoExpandAllGroups = True
                ViewContact.VertScrollVisibility = True
                ViewContact.HorzScrollVisibility = True
                ViewContact.BestFitColumns()

                EffacerTexBox10(GroupBox4)
                EffacerTexBox4(PanelControl4)
            Case Else
        End Select
    End Sub

    Private Sub Combstr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combstr.SelectedIndexChanged
        If Combstr.Text = "LOCALE" Then
            Groupbban.Visible = False
            Groupiban.Visible = False
            Grouplocale.Visible = True
            Grouplocale.TabIndex = 3
            Grouplocale.Size = New Size(1060, 60)
            Grouplocale.Location = New Point(3, 97)
        ElseIf Combstr.Text = "AUTRE" Then
            Groupiban.Visible = False
            Groupbban.Visible = False
            Grouplocale.Visible = True
            Grouplocale.TabIndex = 3
            Grouplocale.Size = New Size(1060, 60)
            Grouplocale.Location = New Point(3, 97)
        ElseIf Combstr.Text = "BBAN" Then
            Groupbban.Visible = True
            Groupiban.Visible = False
            Grouplocale.Visible = False
            Groupbban.TabIndex = 3
            Groupbban.Size = New Size(1060, 60)
            Groupbban.Location = New Point(3, 97)
        ElseIf Combstr.Text = "IBAN" Then
            Groupbban.Visible = False
            Grouplocale.Visible = False
            Groupiban.TabIndex = 3
            Groupiban.Visible = True
            Groupiban.Size = New Size(1060, 60)
            Groupiban.Location = New Point(3, 97)
        End If
    End Sub

    Private Sub txtcle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcle.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf

                    dtBanque.Columns.Add("Banque", Type.GetType("System.String"))
                    dtBanque.Columns.Add("RIB", Type.GetType("System.String"))
                    dtBanque.Columns.Add("Devise", Type.GetType("System.String"))

                    Dim drS = dtBanque.NewRow()
                    drS(0) = txtin.Text
                    drS(1) = txtbanq.Text + " " + txtguich.Text + " " + txtnumcpt.Text + " " + txtcle.Text
                    drS(2) = Combdev.Text
                    dtBanque.Rows.Add(drS)
                    LgListBanque.DataSource = dtBanque

                    GridView1.OptionsView.ColumnAutoWidth = True
                    GridView1.OptionsBehavior.AutoExpandAllGroups = True
                    GridView1.VertScrollVisibility = True
                    GridView1.HorzScrollVisibility = True
                    GridView1.BestFitColumns()

                    EffacerTexBox10(Groupbban)
                    EffacerTexBox10(Grouplocale)
                    EffacerTexBox10(Groupiban)
                Case Else
            End Select
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub txtiban_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtiban.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf

                    dtBanque.Columns.Add("Banque", Type.GetType("System.String"))
                    dtBanque.Columns.Add("RIB", Type.GetType("System.String"))
                    dtBanque.Columns.Add("Devise", Type.GetType("System.String"))

                    Dim drS = dtBanque.NewRow()
                    drS(0) = txtin.Text
                    drS(1) = txtiban.Text
                    drS(2) = Combdev.Text
                    dtBanque.Rows.Add(drS)
                    LgListBanque.DataSource = dtBanque

                    GridView1.OptionsView.ColumnAutoWidth = True
                    GridView1.OptionsBehavior.AutoExpandAllGroups = True
                    GridView1.VertScrollVisibility = True
                    GridView1.HorzScrollVisibility = True
                    GridView1.BestFitColumns()

                    EffacerTexBox10(Groupbban)
                    EffacerTexBox10(Grouplocale)
                    EffacerTexBox10(Groupiban)
                Case Else
            End Select
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub txtbban_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbban.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf

                    dtBanque.Columns.Add("Banque", Type.GetType("System.String"))
                    dtBanque.Columns.Add("RIB", Type.GetType("System.String"))
                    dtBanque.Columns.Add("Devise", Type.GetType("System.String"))

                    Dim drS = dtBanque.NewRow()
                    drS(0) = txtin.Text
                    drS(1) = txtbban.Text
                    drS(2) = Combdev.Text
                    dtBanque.Rows.Add(drS)
                    LgListBanque.DataSource = dtBanque

                    GridView1.OptionsView.ColumnAutoWidth = True
                    GridView1.OptionsBehavior.AutoExpandAllGroups = True
                    GridView1.VertScrollVisibility = True
                    GridView1.HorzScrollVisibility = True
                    GridView1.BestFitColumns()

                    EffacerTexBox10(Groupbban)
                    EffacerTexBox10(Grouplocale)
                    EffacerTexBox10(Groupiban)
                Case Else
            End Select
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Combville_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Combville.SelectedIndexChanged
        Try

           query= "select codezone from t_zonegeo where libellezone='" + EnleverApost(Combville.Text) + "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                id_zone = rwx(0).ToString
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        Try
            If ViewContact.RowCount > 0 Then
                drx = ViewContact.GetDataRow(ViewContact.FocusedRowHandle)
                ViewContact.GetDataRow(ViewContact.FocusedRowHandle).Delete()
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub SupprimerToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerToolStripMenuItem1.Click
        Try
            If GridView1.RowCount > 0 Then
                drx = GridView1.GetDataRow(GridView1.FocusedRowHandle)
                GridView1.GetDataRow(GridView1.FocusedRowHandle).Delete()
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Combtct_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        'Try

        '   query= "select * from T_COMP_TYPE_COMPTE where LIBELLE_TCPT ='" + EnleverApost(cmbType.Text) + "'"
        '    Dim dt = ExcecuteSelectQuery(query)
        '    For Each rwx As DataRow In dt.Rows
        '        idtcpt = rwx(0).ToString
        '    Next

        'Catch ex As Exception
        '    FailMsg("Erreur : Information non disponible : " & ex.ToString())
        'End Try
    End Sub

    Private Sub Combville_TextChanged(sender As Object, e As System.EventArgs) Handles Combville.TextChanged
        Try

           query= "select codezone from t_zonegeo where libellezone='" + EnleverApost(Combville.Text) + "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                id_zone = rwx(0).ToString
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Combtct_TextChanged(sender As Object, e As System.EventArgs) Handles cmbType.TextChanged
        Try

           query= "select * from T_COMP_TYPE_COMPTE where LIBELLE_TCPT ='" + EnleverApost(cmbType.Text) + "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                idtcpt = rwx(0).ToString
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
End Class