Public Class Creer_compte_tier

    Dim dtCompteTier = New DataTable()
    Dim dtcontact = New DataTable()
    Dim dtBanque = New DataTable
    Dim idtcpt As String
    Dim idcodezoNe As Integer = 0
    Private Class1 As New TiersClass

    Private Sub btnewj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnewj.Click
        ActiverChamps4(PanelControl5)
        ActiverChamps4(PanelControl4)
        ActiverChamps5(GroupControl2)
        ActiverChamps10(GroupBox1)
        ActiverChamps10(Groupbban)
        ActiverChamps10(Grouplocale)
        ActiverChamps10(Groupiban)
        ActiverChamps10(GroupBox3)
        ActiverChamps10(GroupBox4)
        txtCodeTiers.Enabled = False
        cmbType.Select()
    End Sub
    Private Sub Creer_compte_tier_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        EffacerTexBox10(GroupBox1)
        EffacerTexBox10(Groupbban)
        EffacerTexBox10(Grouplocale)
        EffacerTexBox10(Groupiban)
        EffacerTexBox10(GroupBox3)
        EffacerTexBox10(GroupBox4)
        EffacerTexBox2(GroupControl2)
        EffacerTexBox4(PanelControl5)
        EffacerTexBox4(PanelControl4)
        dtBanque.Rows.Clear()
        dtcontact.rows.clear()
        dtBanque.columns.clear()
        dtcontact.columns.clear()
        txtiban.Text = ""
    End Sub
    Private Sub Creer_compte_tier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        Try

            EffacerTexBox10(GroupBox1)
            EffacerTexBox10(Groupbban)
            EffacerTexBox10(Grouplocale)
            EffacerTexBox10(Groupiban)
            EffacerTexBox2(GroupControl2)
            EffacerTexBox4(PanelControl5)
            dtBanque.Rows.Clear()
            txtiban.Text = ""

            'remplir le type compte
            cmbType.Properties.Items.Clear()
            query = "select * from T_COMP_TYPE_COMPTE ORDER BY Code_CL"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                cmbType.Properties.Items.Add(rw("Code_CL") & " | " & MettreApost(rw("libelle_tcpt")).ToString)
            Next

            'remplir localité
            RemplirCombo2(Combville, "t_zonegeo", "libellezone")

            'remplir service
            RemplirCombo2(Combserv, "t_service", "nomservice")

            'remplir service
            RemplirCombo2(Combdev, "T_Devise", "AbregeDevise")

            'remplir les sous classe du plan comptable
            Combsc.Properties.Items.Clear()
            query = "select * from T_COMP_SOUS_CLASSE where code_sc like '4%' ORDER BY code_sc"
            Dim dt1 = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                Combsc.Properties.Items.Add(rw1(0).ToString + "   " + MettreApost(rw1(2).ToString))
            Next

            dtcontact.Columns.Clear()
            dtcontact.Columns.Add("Nom & Prénoms", Type.GetType("System.String"))
            dtcontact.Columns.Add("Service", Type.GetType("System.String"))
            dtcontact.Columns.Add("Fonction", Type.GetType("System.String"))
            dtcontact.Columns.Add("Téléphone", Type.GetType("System.String"))
            dtcontact.Columns.Add("Portable", Type.GetType("System.String"))
            dtcontact.Columns.Add("Fax", Type.GetType("System.String"))
            dtcontact.Columns.Add("Email", Type.GetType("System.String"))
            dtcontact.Rows.Clear()

            dtBanque.Columns.Clear()
            dtBanque.Columns.Add("Banque", Type.GetType("System.String"))
            dtBanque.Columns.Add("RIB", Type.GetType("System.String"))
            dtBanque.Columns.Add("Devise", Type.GetType("System.String"))
            dtBanque.Rows.Clear()
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub txtcle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcle.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
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

    Private Sub txtnumcpt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnumcpt.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
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

    Private Sub txtguich_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtguich.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
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

    Private Sub txtbanq_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbanq.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
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

    Private Sub txtbban_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbban.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
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

    Private Sub iban_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtiban.KeyPress
        Try
            Select Case e.KeyChar
                Case ControlChars.CrLf
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

    Private Sub btenregisterj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btenregisterj.Click
        Try
            'vérification des champ text
            Dim erreur As String = ""
            Dim sc() As String
            sc = Combsc.Text.Split("   ")

            If txtCodeTiers.Text.ToUpper() = "" Then
                erreur += "- Numéro Compte" + ControlChars.CrLf
            End If
            'If Combsc.SelectedIndex = -1 Then
            '    erreur += "- Compte sélectif" + ControlChars.CrLf
            'End If
            If txtIntitule.Text = "" Then
                erreur += "- Nom Compte tiers" + ControlChars.CrLf
            End If

            If cmbType.SelectedIndex = -1 Then
                erreur += "- Type Compte Tiers" + ControlChars.CrLf
            End If

            If erreur = "" Then

                'date
                Dim datedebut As String = ExerciceComptable.Rows(0).Item("datedebut")

                'query = "select datedebut, datefin from T_COMP_EXERCICE where encours='1'"
                'Dim dt = ExcecuteSelectQuery(query)
                'For Each rwx As DataRow In dt.Rows
                '    datedebut = CDate(rwx(0)).ToString("dd/MM/yyyy")
                'Next

                'Conversion date
                Dim str1(3) As String
                str1 = datedebut.ToString.Split("/")
                Dim tempdt1 As String = String.Empty
                For j As Integer = 2 To 0 Step -1
                    tempdt1 += str1(j) + "-"
                Next
                tempdt1 = tempdt1.Substring(0, 10)


                query = "select CODE_CPT from T_COMP_COMPTE where code_cpt='" + txtCodeTiers.Text.ToUpper() + "' and code_projet='" + ProjetEnCours + "'"
                Dim dt1 = ExcecuteSelectQuery(query)
                If dt1.Rows.Count > 0 Then
                    SuccesMsg("Le compte existe déjà.")
                Else

                    'insertion compte de tiers
                    query = "insert into T_COMP_COMPTE values('" + EnleverApost(txtCodeTiers.Text.ToUpper()) + "','" + EnleverApost(Txtabr.Text) + "','" + idtcpt.ToString + "','" + idcodezone.ToString + "','" + EnleverApost(txtIntitule.Text) + "','" + EnleverApost(txtadresse.Text) + "','" + txtcontact.Text + "','" + EnleverApost(txtmail.Text) + "','" + ProjetEnCours + "','" + EnleverApost(Txtrc.Text) + "','" + EnleverApost(Txtcc.Text) + "','" + Txttel.Text + "','" + Txtfax.Text + "','" + EnleverApost(Txtsite.Text) + "','" + EnleverApost(txtqualite.Text) + "')"
                    ExecuteNonQuery(query)

                    'On insert le nouveau compte de tiers dans la table de report de tous les exercices
                    query = "SELECT * FROM t_comp_exercice"
                    Dim dtExercice As DataTable = ExcecuteSelectQuery(query)
                    For Each rwExo In dtExercice.Rows
                        query = "SELECT * FROM report_cpt WHERE CODE_CPT='" & txtCodeTiers.Text.ToUpper() & "' AND DATE_LE='" & dateconvert(CDate(rwExo("datedebut"))) & "'"
                        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                        If dtVerif.Rows.Count = 0 Then
                            query = "insert into report_cpt values (NULL, '" + txtCodeTiers.Text.ToUpper() + "','0', '0','0','0','" & dateconvert(CDate(rwExo("datedebut"))) & "')"
                            ExecuteNonQuery(query)
                        End If
                    Next

                    'insertion rattaché compte tiers à compte selectif
                    'query = "insert into T_COMP_RATTACH_TIERS values('" + sc(0).ToString + "','" + Txtabr.Text + "','" + idtcpt.ToString + "','" + ProjetEnCours + "','" + txtcpt.Text + "')"
                    'ExecuteNonQuery(query)

                    'insertion de la banque et leur rattache
                    For i = 0 To GridView1.RowCount - 1
                        'insertion dans la table banque
                        query = "insert into T_COMP_BANQUE values (NULL,'" + dtBanque.Rows(i).item(0).ToString + "','" + dtBanque.Rows(i).item(1).ToString + "','" + dtBanque.Rows(i).item(2).ToString + "','" + ProjetEnCours + "')"
                        ExecuteNonQuery(query)

                        'insertion dans la table ratach_banq
                        query = "insert into T_COMP_RATTACH_BANQ values('" + dtBanque.Rows(i).item(1).ToString + "','" + txtCodeTiers.Text.ToUpper() + "')"
                        ExecuteNonQuery(query)
                    Next

                    For i = 0 To ViewContact.RowCount - 1
                        'insertion dans la table banque
                        query = "insert into T_COMP_IDENTIFIANT values (NULL,'" + EnleverApost(txtCodeTiers.Text.ToUpper()) + "','" + EnleverApost(dtcontact.Rows(i).item(0).ToString) + "','" + EnleverApost(dtcontact.Rows(i).item(2).ToString) + "','" + EnleverApost(dtcontact.Rows(i).item(4).ToString) + "','" + EnleverApost(dtcontact.Rows(i).item(5).ToString) + "','" + EnleverApost(dtcontact.Rows(i).item(6).ToString) + "','" + ProjetEnCours + "','" + EnleverApost(dtcontact.Rows(i).item(3).ToString) + "','" + EnleverApost(dtcontact.Rows(i).item(1).ToString) + "')"
                        ExecuteNonQuery(query)
                    Next

                    txtCodeTiers.Focus()
                    EffacerTexBox10(GroupBox1)
                    EffacerTexBox10(Groupbban)
                    EffacerTexBox10(Grouplocale)
                    EffacerTexBox10(Groupiban)
                    EffacerTexBox10(GroupBox3)
                    EffacerTexBox10(GroupBox4)
                    EffacerTexBox2(GroupControl2)
                    EffacerTexBox4(PanelControl5)
                    EffacerTexBox4(PanelControl4)
                    dtBanque.Rows.Clear()
                    dtcontact.rows.clear()
                    dtBanque.columns.clear()
                    dtcontact.columns.clear()
                    txtiban.Text = ""

                    Plan_tiers.CmbPageSize_SelectedIndexChanged(sender, e)

                    SuccesMsg("Enregistrement effectué avec succès.")
                End If
            Else
                AlertMsg("Veuillez remplir ces champs : " + ControlChars.CrLf + erreur)
            End If

        Catch ex As Exception
            SuccesMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    'Private Sub RemplirFRS()

    '    Saisie_engagement.TxtFournisMarche.Properties.Items.Clear()
    '    Modif_engagement.TxtFournisMarche.Properties.Items.Clear()
    '    query = "select * from T_COMP_COMPTE where Code_Projet='" & ProjetEnCours & "' order by code_cpt"
    '    Dim dt As DataTable = ExcecuteSelectQuery(query)
    '    For Each rw As DataRow In dt.Rows
    '        Saisie_engagement.TxtFournisMarche.Properties.Items.Add(rw(0).ToString + " | " + MettreApost(rw(4).ToString))
    '        Modif_engagement.TxtFournisMarche.Properties.Items.Add(rw(0).ToString + " | " + MettreApost(rw(4).ToString))
    '    Next

    'End Sub

    Private Sub btannulerj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btannulerj.Click
        EffacerTexBox10(GroupBox1)
        EffacerTexBox10(Groupbban)
        EffacerTexBox10(Grouplocale)
        EffacerTexBox10(Groupiban)
        EffacerTexBox10(GroupBox3)
        EffacerTexBox10(GroupBox4)
        EffacerTexBox2(GroupControl2)
        EffacerTexBox4(PanelControl5)
        EffacerTexBox4(PanelControl4)
        dtBanque.Rows.Clear()
        txtiban.Text = ""
        txtCodeTiers.Focus()
    End Sub

    Private Sub Txtemail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txtemail.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
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

    Private Sub txtnom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIntitule.TextChanged
        txtCodeTiers.Text = GenreateNumTiers()
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

    Private Sub txtcpt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodeTiers.TextChanged
        txtCodeTiers.Text = GenreateNumTiers()
        txtCodeTiers.Properties.CharacterCasing = CharacterCasing.Upper
        dtBanque.Rows.Clear()
        'If cmbType.Text = "Fournisseurs" Then

        '    query = "select t_fournisseur.NomFournis,t_fournisseur.AdresseCompleteFournis,t_fournisseur.MailFournis,t_fournisseur.TelFournis,t_fournisseur.PaysFournis from t_fournisseur,T_SoumissionFournisseur where T_SoumissionFournisseur.codefournis=t_fournisseur.codefournis and t_fournisseur.CodeFournis='" + txtCodeTiers.Text.ToUpper() + "' and t_fournisseur.codeprojet='" + ProjetEnCours + "' and T_SoumissionFournisseur.attribue='oui'"
        '    Dim dt = ExcecuteSelectQuery(query)
        '    For Each rwx As DataRow In dt.Rows
        '        txtIntitule.Text = rwx(0).ToString
        '        txtadresse.Text = rwx(1).ToString
        '        txtmail.Text = rwx(2).ToString
        '        txtcontact.Text = rwx(3).ToString
        '        Combville.Text = rwx(4).ToString
        '    Next

        'ElseIf cmbType.Text = "Consultants" Then

        '    query = "select T_Consultant.NomConsult,T_Consultant.AdressConsult,T_Consultant.EmailConsult,T_Consultant.TelConsult,T_Consultant.PaysConsult from T_Consultant,T_SoumissionConsultant where T_SoumissionConsultant.RefConsult=T_Consultant.RefConsult and T_SoumissionConsultant.attribue='oui' and T_Consultant.RefConsult='" + txtCodeTiers.Text.ToUpper() + "'"
        '    Dim dt = ExcecuteSelectQuery(query)
        '    For Each rwx As DataRow In dt.Rows
        '        txtIntitule.Text = rwx(0).ToString
        '        txtadresse.Text = rwx(1).ToString
        '        txtmail.Text = rwx(2).ToString
        '        txtcontact.Text = rwx(3).ToString
        '        Combville.Text = rwx(4).ToString
        '    Next

        'End If

    End Sub

    Private Sub Combtct_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        Try
            txtCodeTiers.Text = GenreateNumTiers()
            If cmbType.SelectedIndex <> -1 Then
                idtcpt = ExecuteScallar("SELECT CODE_TCPT FROM t_comp_type_compte WHERE Code_CL='" & cmbType.Text.Split(" | ")(0) & "'")
                'query = "select * from T_COMP_TYPE_COMPTE where LIBELLE_TCPT ='" + EnleverApost(cmbType.Text) + "'"
                'Dim dt = ExcecuteSelectQuery(query)
                'For Each rwx As DataRow In dt.Rows
                'Next
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
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

    Private Sub Combserv_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combserv.SelectedIndexChanged
        Try

            query = "select f.LibelleFonction from t_service s,t_fonction f where s.codeservice=f.codeservice and s.NomService='" + EnleverApost(Combserv.Text) + "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                Txtfonct.Properties.Items.Add(MettreApost(rwx(0).ToString))
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Combville_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combville.SelectedIndexChanged
        Try

            query = "select codezone from t_zonegeo where libellezone='" + EnleverApost(Combville.Text) + "'"
            Dim dt = ExcecuteSelectQuery(query)
            For Each rwx As DataRow In dt.Rows
                idcodezone = rwx(0).ToString
            Next

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub Txtfaxc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txtfaxc.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
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

    Private Sub Txtprenom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txtprenom.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
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

    Private Sub LgListContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListContact.Click
        Try
            If (ViewContact.RowCount > 0) Then
                drx = ViewContact.GetDataRow(ViewContact.FocusedRowHandle)
                Dim ID = drx(6).ToString
                ColorRowGrid(ViewContact, "[Email]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(ViewContact, "[Email]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub LgListBanque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LgListBanque.Click
        Try
            If (GridView1.RowCount > 0) Then
                drx = ViewContact.GetDataRow(GridView1.FocusedRowHandle)
                Dim ID = drx(1).ToString
                ColorRowGrid(GridView1, "[RIB]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
                ColorRowGridAnal(GridView1, "[RIB]='" & ID & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
            End If
        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        Try
            If ViewContact.RowCount > 0 Then
                drx = ViewContact.GetDataRow(ViewContact.FocusedRowHandle)
                ViewContact.GetDataRow(ViewContact.FocusedRowHandle).Delete()
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try

    End Sub

    Private Sub SupprimerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem1.Click
        Try
            If GridView1.RowCount > 0 Then
                drx = GridView1.GetDataRow(GridView1.FocusedRowHandle)
                GridView1.GetDataRow(GridView1.FocusedRowHandle).Delete()
            End If

        Catch ex As Exception
            FailMsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub
    Private Function GenreateNumTiers() As String
        If cmbType.SelectedIndex <> -1 Then
            If txtIntitule.Text.Trim().Length > 0 And txtIntitule.Text.Trim().Length >= 3 Then
                Dim type As String = cmbType.Text.Split(" | ")(0)
                Dim Code As String = Mid(txtIntitule.Text, 1, 3)
                Dim cpte As Decimal = 0
                For i = 1 To Code.Length
                    If Char.IsLetter(Mid(Code, 1, 1)) Then
                        cpte += 1
                    End If
                Next
                If cpte = 3 Then
                    Dim disponible As Boolean = False  'Permettra de verifier la disponibilite du code
                    Dim decal As String = "000"
                    Dim NewTiers As String = type & Code & decal
                    While Not disponible
                        query = "SELECT CODE_CPT FROM t_comp_compte WHERE CODE_CPT='" & NewTiers & "'"
                        Dim dtVerif As DataTable = ExcecuteSelectQuery(query)
                        If dtVerif.Rows.Count = 0 Then
                            disponible = True
                        Else
                            Dim newDecal As String = (Val(decal) + 1).ToString()
                            If newDecal.Length = 1 Then
                                decal = "00" & newDecal
                            ElseIf decal.Length = 2 Then
                                decal = "0" & newDecal
                            ElseIf decal.Length = 3 Then
                                decal = newDecal
                            Else
                                Exit While
                            End If
                            NewTiers = type & Code & decal
                        End If
                    End While
                    Return NewTiers
                End If
            End If
        End If
        Return String.Empty
    End Function

    Private Sub btNewType_Click(sender As Object, e As EventArgs) Handles btNewType.Click
        Dim NewType As New TypeTiers
        Dialog_form(NewType)
        Dim OldText As String = cmbType.Text
        'remplir le type compte
        cmbType.Properties.Items.Clear()
        query = "select * from T_COMP_TYPE_COMPTE ORDER BY Code_CL"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cmbType.Properties.Items.Add(rw("Code_CL") & " | " & MettreApost(rw("libelle_tcpt")).ToString)
        Next
        cmbType.Text = OldText
    End Sub
End Class