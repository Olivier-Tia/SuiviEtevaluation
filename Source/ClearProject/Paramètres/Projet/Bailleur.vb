Imports MySql.Data.MySqlClient
Imports System.IO
Public Class Bailleur

    Inherits DevExpress.XtraEditors.XtraForm
    Dim CodePays As Decimal
    Dim IniBailleur As String
    Dim Sig As String
    Dim PourAjout As Boolean = False
    Dim PourModif As Boolean = False
    Dim PourSupp As Boolean = False
    Dim som As Decimal
    Dim NomVille As String
    Dim CodeZoneMere As String
    Dim IndicZone As String
    Dim Siege As String

    Private Sub TxtSigle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSigle.TextChanged

        If TxtSigle.Text <> "" And TxtSigle.Text <> "SIGLE" Then
            TxtNomBailleur.Enabled = True
        End If

        If TxtSigle.Text = "" Then
            TxtNomBailleur.Enabled = False
        End If

    End Sub

    Private Sub TxtSigle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSigle.Click

        If TxtSigle.Text = "SIGLE" Then
            TxtSigle.Text = ""
        End If

    End Sub

    Private Sub TxtNomBailleur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNomBailleur.TextChanged

        If TxtNomBailleur.Text <> "" Then
            ComboPays.Enabled = True
        End If

        If TxtNomBailleur.Text = "" Then
            ComboPays.Enabled = False
        End If


    End Sub

    Private Sub ComboPays_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboPays.SelectedIndexChanged

        Dim Pays As String = ComboPays.Text
        Dim Codevilleindic As String = ""
        CorrectionChaine(Pays)
        
        query = "select CodeZone,IndicZone from T_ZoneGeo where LibelleZone='" & Pays & "'"
        On Error Resume Next
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Indicatif.Text = rw(1)
            TxtIndic1.Text = rw(1)
            TxtIndic2.Text = rw(1)
            CodePays = rw(0)
            Codevilleindic = rw(1)
        Next
        RemplirChampVille(Codevilleindic)

        If ComboPays.Text <> "" Then
            TxtAdresse.Enabled = True

        End If
        If ComboPays.Text = "" Then
            TxtAdresse.Enabled = False

        End If

    End Sub

    Private Sub ComboVille_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboVille.SelectedIndexChanged
        If ComboVille.Text <> "" Then
            TxtAdresse.Enabled = True

        End If
        If ComboVille.Text = "" Then
            TxtAdresse.Enabled = False

        End If
    End Sub

    Private Sub TxtAdresse_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtAdresse.TextChanged
        If TxtAdresse.Text <> "" Then
            TxtLogo.Enabled = True
            BtLogo.Enabled = True
            ComboTitreTtl.Enabled = True
            TxtSiteWebBailleur.Enabled = True
        End If
        If TxtAdresse.Text = "" Then
            TxtLogo.Enabled = False
            BtLogo.Enabled = False
            ComboTitreTtl.Enabled = False
            TxtSiteWebBailleur.Enabled = False
        End If
    End Sub

    Private Sub ComboTitreTtl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboTitreTtl.SelectedIndexChanged

        If ComboTitreTtl.Text <> "" Then
            TxtNomTtl.Enabled = True

        End If
        If ComboTitreTtl.Text = "" Then
            TxtNomTtl.Enabled = False

        End If

    End Sub

    Private Sub TxtNomTtl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNomTtl.TextChanged
        If TxtNomTtl.Text <> "" Then
            TxtPrenomTtl.Enabled = True

        End If
        If TxtNomTtl.Text = "" Then
            TxtPrenomTtl.Enabled = False

        End If
    End Sub

    Private Sub TxtPrenomTtl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPrenomTtl.TextChanged
        If TxtPrenomTtl.Text <> "" Then
            TxtFonctionTtl.Enabled = True

        End If
        If TxtPrenomTtl.Text = "" Then
            TxtFonctionTtl.Enabled = False

        End If
    End Sub

    Private Sub TxtFonctionTtl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFonctionTtl.TextChanged
        If TxtFonctionTtl.Text <> "" Then
            TxtTelTtl.Enabled = True
            TxtFaxTtl.Enabled = True
            MailTTL.Enabled = True
        End If
        If TxtFonctionTtl.Text = "" Then
            TxtTelTtl.Enabled = False
            TxtFaxTtl.Enabled = False
            MailTTL.Enabled = False
        End If
    End Sub

    Private Sub TxtTelTtl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (TxtTelTtl.Text <> "" Or TxtFaxTtl.Text <> "") Then

        End If
        If (TxtTelTtl.Text = "" And TxtFaxTtl.Text = "") Then

        End If
    End Sub

    Private Sub BtLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtLogo.Click
        On Error Resume Next
        'affichage de l'image dans le picturebox
        Dim dlg As New OpenFileDialog
        dlg.Filter = "Documents Images (*.png; *.gif; *.jpg; *.bmp)|*.png;*.gif;*.jpg;*.bmp"
        If dlg.ShowDialog() = DialogResult.OK Then
            Dim fichier As FileStream = New FileStream(dlg.FileName, FileMode.Open)
            Dim fichier1 As String = dlg.FileName
            TxtExt.Text = ExtensionImage(fichier1)
            LogoBailleur.Image = Image.FromStream(fichier)
            TxtLogo.Text = fichier1

            Dim mon_fichier As FileInfo = New FileInfo(TxtLogo.Text)
            If mon_fichier.Length < 1000000 Then
            Else
                SuccesMsg("Image trop volumineuse.")
                TxtExt.Text = ""
                TxtLogo.Text = ""
                LogoBailleur.Image = Nothing
            End If
            fichier.Close()
        End If
    End Sub

    Private Sub TxtFaxTtl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (TxtTelTtl.Text <> "" Or TxtFaxTtl.Text <> "") Then
        End If
        If (TxtTelTtl.Text = "" And TxtFaxTtl.Text = "") Then
        End If
    End Sub

    Private Sub Bailleur_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        BtRetour1_Click(Me, e)
    End Sub

    Private Sub Bailleur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirChampPays()
        RemplirListViewBailleur()
        LogoBailleur.Image = Nothing
    End Sub
    Private Sub RemplirChampPays()
        query = "select LibelleZone from T_ZoneGeo where NiveauStr='1'"
        ComboPays.Items.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Dim Pays As String = rw(0)
            ComboPays.Items.Add(MettreApost(Pays))
        Next
    End Sub
    Private Sub RemplirChampVille(ByVal Indicteur As String)
        'on efface les lignes du comboville 
        query = "select LibelleZone from T_ZoneGeo where IndicZone='" & Indicteur & "'"
        ComboVille.Items.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            ComboVille.Items.Add(MettreApost(rw(0)))
        Next
    End Sub

    

    Private Sub EnregistrerNouveauBailleur()
        Try
            If TxtCodeBailleur.Text.Trim().Length = 0 Then
                Exit Sub
            End If
            If TxtSigle.Text.Trim().Length = 0 Then
                SuccesMsg("Entrer le sigle")
                TxtSigle.Focus()
                Exit Sub
            End If
            If TxtNomBailleur.Text.Trim().Length = 0 Then
                SuccesMsg("Entrer le nom")
                TxtNomBailleur.Focus()
                Exit Sub
            End If
            'on instancie l'objet DataSet avec de l'utiliser
            Dim logo As String = ""
            If (TxtLogo.Text <> "" And TxtExt.Text <> "") Then
                logo = TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text
            Else
                logo = "blanc.GIF"
            End If

            Dim codebail As Decimal
            Dim codezoNe As Integer
            codebail = TxtCodeBailleur.Text
            codezoNe = PrendreCodeVille(EnleverApost(ComboPays.Text))


            query = "insert into T_Bailleur values ('" + codebail.ToString + "','" + EnleverApost(TxtNomBailleur.Text) + "','" + EnleverApost(TxtSigle.Text) + "','" + EnleverApost(TxtAdresse.Text) + "','','','" + EnleverApost(TxtSiteWebBailleur.Text) + "','" + logo.ToString + "','" + EnleverApost(MailTTL.Text) + "','" + EnleverApost(ComboTitreTtl.Text) + "','" + EnleverApost(TxtNomTtl.Text) + "','" + EnleverApost(TxtPrenomTtl.Text) + "','" + EnleverApost(TxtFonctionTtl.Text) + "','" + TxtTelTtl.Text + "','" + TxtFaxTtl.Text + "','','" + codezoNe.ToString + "','" + ProjetEnCours + "')"
            ExecuteNonQuery(query)
            IniBailleur = TxtSigle.Text
            'query = "CALL `CreateTampColBailleur`();"
            'ExecuteNonQuery(query)

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try

    End Sub
    'Procédure permettant de créer une ligne après validation du boutton enregistrer
    Private Sub AjouterNvelLIgne()
        Dim MaLigne As ListViewItem
        MaLigne = New ListViewItem(New String() {TxtCodeBailleur.Text, TxtSigle.Text, TxtNomBailleur.Text, ComboPays.Text})
        ListViewBailleur.Items.Add(MaLigne)
    End Sub

    Private Sub CreationCodeBailleur(ByVal id As Decimal)
        id = id + 1
        If PourAjout Then
            TxtCodeBailleur.Text = "0" & id
        End If
    End Sub

    Private Function PrendreCodeVille(ByVal Vil As String)
        Dim ValRetour2 As Decimal = 0
        CorrectionChaine(Vil)
        query = "select CodeZone from T_ZoneGeo where LibelleZone='" & Vil & "'"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            ValRetour2 = rw(0)
        Next
        Return ValRetour2
    End Function
    Private Sub RemplirListViewBailleur()
        query = "select B.CodeBailleur,B.InitialeBailleur,B.NomBailleur,Z.LibelleZone from T_Bailleur B,T_ZoneGeo Z where B.CodeZone=Z.CodeZone and B.CodeProjet='" & ProjetEnCours & "'"
        ListViewBailleur.Items.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Dim NomBail As String = rw(2)
            NomBail = MettreApost(NomBail)
            Dim NomZone As String = rw(3)
            NomZone = MettreApost(NomZone)
            ListViewBailleur.Items.Add(New ListViewItem(New String() {rw(0), rw(1), NomBail, NomZone}))
        Next
    End Sub
    Private Sub Effacer()
        TxtNomBailleur.Text = ""
        TxtNomBailleur.Enabled = False
        TxtSigle.Text = ""
        TxtSigle.Enabled = False
        TxtAdresse.Text = ""
        TxtAdresse.Enabled = False
        MailTTL.Text = ""
        MailTTL.Enabled = False
        ComboTitreTtl.Text = ""
        ComboTitreTtl.Enabled = False
        TxtNomTtl.Text = ""
        TxtNomTtl.Enabled = False
        TxtPrenomTtl.Text = ""
        TxtPrenomTtl.Enabled = False
        TxtFonctionTtl.Text = ""
        TxtFonctionTtl.Enabled = False
        TxtTelTtl.Text = ""
        TxtTelTtl.Enabled = False
        TxtFaxTtl.Text = ""
        TxtFaxTtl.Enabled = False
        TxtCodeBailleur.Text = ""
        ComboPays.Text = ""
        ComboPays.Enabled = False
        ComboVille.Text = ""
        ComboVille.Enabled = False
        TxtLogo.Text = ""
        TxtLogo.Enabled = False
        BtLogo.Enabled = False
        Indicatif.Text = ""
        TxtIndic1.Text = ""
        TxtIndic2.Text = ""
        TxtSiteWebBailleur.Text = ""
        LogoBailleur.Image = Nothing
        If BtEnregistrer1.Enabled = False Then BtEnregistrer1.Enabled = True
    End Sub
    Private Sub BtAjouter1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter1.Click

        TxtSigle.Enabled = True
        PourAjout = True
        PourModif = False
        PourSupp = False

        BtModifier1.Enabled = True
        BtSupprimer1.Enabled = True
        BtRetour1.Enabled = True
        BtEnregistrer1.Enabled = True

        If PourAjout Then
            GenererCodeBailler()
        End If

    End Sub

    Private Sub BtModifier1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModifier1.Click
        If TxtCodeBailleur.Text.Trim().Length = 0 Then
            Exit Sub
        End If
        If TxtSigle.Text.Trim().Length = 0 Then
            SuccesMsg("Entrer le sigle")
            TxtSigle.Focus()
            Exit Sub
        End If
        If TxtNomBailleur.Text.Trim().Length = 0 Then
            SuccesMsg("Entrer le nom")
            TxtNomBailleur.Focus()
            Exit Sub
        End If
        If TxtCodeBailleur.Text <> "" Then

            Dim j As Integer
            Try
                j = ListViewBailleur.SelectedIndices(0).ToString
            Catch ex As Exception
                SuccesMsg("Veuillez selectionner une ligne dans le tableau.")
                Exit Sub
            End Try
            ModificationBailleur()
            'on met les nouvelles données dans les dif zones du listview
            ListViewBailleur.Items(j).SubItems(0).Text = TxtCodeBailleur.Text
            ListViewBailleur.Items(j).SubItems(1).Text = TxtSigle.Text
            ListViewBailleur.Items(j).SubItems(2).Text = TxtNomBailleur.Text
            ListViewBailleur.Items(j).SubItems(3).Text = ComboPays.Text
            '******************************************************
            SuccesMsg("Modification terminée avec succès.")
            Effacer() 'effacer les données des zones de saisies

        Else
        End If

    End Sub

    Private Sub BtSupprimer1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer1.Click
        Try
            Dim j As Integer
            Try
                j = ListViewBailleur.SelectedIndices(0).ToString
            Catch ex As Exception
                SuccesMsg("Veuillez selectionner une ligne dans le tableau.")
                Exit Sub
            End Try

            If TxtCodeBailleur.Text <> "" Then

                If ConfirmMsg("Voulez-vous vraiment supprimer?") = DialogResult.Yes Then

                    'on supprime le bailleur 

                    Dim DatSet = New DataSet
                    query = "DELETE FROM T_Bailleur WHERE CodeBailleur = '" & TxtCodeBailleur.Text & "'"
                    ExecuteNonQuery(query)

                    'query = "CALL `DeleteTampColBailleur`();"
                    'ExecuteNonQuery(query)
                    'query = "CALL `CreateTampColBailleur`();"
                    'ExecuteNonQuery(query)

                    Dim ChemImg As String = TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text
                    LogoBailleur.Image.Dispose()
                    LogoBailleur.Image = Nothing

                    If (File.Exists(line & "\LogoProjet\" & ChemImg) = True) Then
                        File.Delete(line & "\LogoProjet\" & ChemImg)
                    End If

                    RemplirListViewBailleur()
                    Effacer() 'pour effacer les zones de text,combo et autre
                    SuccesMsg("Suppression terminée avec succès.")
                End If

            Else
                MsgBox("Veuillez selectionner une ligne dans le tableau !", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub BtRetour1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetour1.Click

        Effacer()

        PourAjout = False
        PourModif = False
        PourSupp = False

        BtModifier1.Enabled = True
        BtAjouter1.Enabled = True
        BtSupprimer1.Enabled = True
        BtRetour1.Enabled = False
        BtEnregistrer1.Enabled = False

    End Sub
    Private Sub BtEnregistrer1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregistrer1.Click

        '**********************  Pour ajouter ******************************

        Sig = TxtSigle.Text
        EnregistrerNouveauBailleur()
        'ParaCon() 'procedure permettant de prendre le nom du serveur 

        'vérification de l'extension et copie du chemin de l'image dans la texbox txtchemin
        If (TxtExt.Text <> "") Then
            File.Copy(TxtLogo.Text, line & "\LogoProjet\" & TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text, True)
        End If

        AjouterNvelLIgne()
        CreationCodeBailleur(TxtCodeBailleur.Text)
        Effacer()


    End Sub

    Private Sub ListViewBailleur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListViewBailleur.Click

        Dim i As Integer = ListViewBailleur.SelectedIndices(0).ToString
        ToutChampActiver()

        With ListViewBailleur
            If .SelectedIndices.Count > 0 Then
                .Items(.SelectedIndices(0)).Selected = True
                TxtCodeBailleur.Text = .Items(.SelectedIndices(0)).SubItems(0).Text
                TxtSigle.Text = .Items(.SelectedIndices(0)).SubItems(1).Text
                TxtNomBailleur.Text = .Items(.SelectedIndices(0)).SubItems(2).Text
                AfficherInfoTableBailleur(ListViewBailleur.Items(i).SubItems(0).Text)
                GroupInstitution.Enabled = True
                GroupTTL.Enabled = True
                TxtExt.Text = ""
                BtEnregistrer1.Enabled = False
            End If
        End With
    End Sub

    Private Sub ToutChampActiver()

        TxtSigle.Enabled = True
        TxtNomBailleur.Enabled = True
        ComboPays.Enabled = True
        ComboVille.Enabled = True
        TxtAdresse.Enabled = True
        TxtLogo.Enabled = True
        BtLogo.Enabled = True
        ComboTitreTtl.Enabled = True
        TxtNomTtl.Enabled = True
        TxtPrenomTtl.Enabled = True
        TxtFonctionTtl.Enabled = True
        MailTTL.Enabled = True
        TxtTelTtl.Enabled = True
        TxtFaxTtl.Enabled = True
        ListViewBailleur.Enabled = True

    End Sub
    Private Sub ListViewBailleur_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewBailleur.DoubleClick
        Dim i As Integer = ListViewBailleur.SelectedIndices(0).ToString

        If PourSupp = True Then
            'ToutChampActiver()
            If PourSupp = True Then
                With ListViewBailleur
                    If .SelectedIndices.Count > 0 Then
                        .Enabled = True
                        .Items(.SelectedIndices(0)).Selected = True
                        TxtCodeBailleur.Text = .Items(.SelectedIndices(0)).SubItems(0).Text
                        TxtSigle.Text = .Items(.SelectedIndices(0)).SubItems(1).Text
                        TxtNomBailleur.Text = .Items(.SelectedIndices(0)).SubItems(2).Text
                        AfficherInfoTableBailleur(ListViewBailleur.Items(i).SubItems(0).Text)
                        GroupInstitution.Enabled = False
                        GroupTTL.Enabled = False
                    End If
                End With
            End If
        End If
    End Sub

    Private Sub AfficherInfoTableBailleur(ByVal CodeBail As String)

        LogoBailleur.Image = Nothing

        Dim IdZone As String = ""

        query = "select CodeBailleur,NomBailleur,InitialeBailleur,AdresseCompleteBailleur,TitreTTL,NomTTL,PrenomTTL,FonctionTTL,TelTTL,MailTTL,FaxTTL,CodeZone,LogoBailleur,SiteWeb from T_Bailleur where CodeBailleur='" & CodeBail & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            '*************  Institution *************************
            IdZone = rw(11).ToString
            TxtCodeBailleur.Text = rw(0).ToString
            TxtSigle.Text = rw(2).ToString
            TxtNomBailleur.Text = MettreApost(rw(1).ToString)
            TxtAdresse.Text = MettreApost(rw(3).ToString)
            '********* TTL *******************
            ComboTitreTtl.Text = MettreApost(rw(4).ToString)
            TxtNomTtl.Text = MettreApost(rw(5).ToString)
            TxtFonctionTtl.Text = MettreApost(rw(7).ToString)
            TxtTelTtl.Text = rw(8).ToString
            TxtPrenomTtl.Text = MettreApost(rw(6).ToString)
            MailTTL.Text = MettreApost(rw(9).ToString)
            TxtFaxTtl.Text = rw(10).ToString
            TxtSiteWebBailleur.Text = MettreApost(rw(13).ToString)
            'Dim img() As String
            'img = rw(12).ToString.Split(".")



            If File.Exists(line & "\LogoProjet\" & rw(12).ToString) Then
                LogoBailleur.Image = Image.FromFile(line & "\LogoProjet\" & rw(12).ToString)
                Dim fichier1 As String = line & "\LogoProjet\" & rw(12).ToString
                TxtExt.Text = ExtensionImage(fichier1)
                TxtLogo.Text = fichier1
            End If
            '*********************************************************
        Next

        PrendreLibVille(IdZone)
        ComboPays.Text = NomVille
        TxtIndic1.Text = IndicZone
        TxtIndic2.Text = IndicZone
        Indicatif.Text = IndicZone

    End Sub

    Private Sub PrendreLibVille(ByVal CodeVille As String)

        query = "select LibelleZone,CodeZoneMere,IndicZone from T_ZoneGeo where CodeZone='" & CodeVille & "'"
        Dim dt = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            CodeZoneMere = rw(1)
            NomVille = MettreApost(rw(0))
            IndicZone = rw(2)
        End If
    End Sub
    Private Sub PrendreLibDuPays(ByVal IdPays As String)

        query = "select LibelleZone,IndicZone from T_ZoneGeo where CodeZone='" & IdPays & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Dim rw As DataRow = dt.Rows(0)
            CodeZoneMere = MettreApost(rw(0))
            IndicZone = rw(1)
        End If
    End Sub
    Private Sub EffacerZoneDeSaises()
        TxtCodeBailleur.Text = ""
        TxtSigle.Text = ""
        TxtNomBailleur.Text = ""
        Indicatif.Text = ""
        ComboPays.Text = ""
        ComboVille.Text = ""
        TxtAdresse.Text = ""
        TxtLogo.Text = ""
        ComboTitreTtl.Text = ""
        TxtNomTtl.Text = ""
        TxtFonctionTtl.Text = ""
        TxtIndic1.Text = ""
        TxtIndic2.Text = ""
        TxtTelTtl.Text = ""
        TxtPrenomTtl.Text = ""
        MailTTL.Text = ""
        TxtFaxTtl.Text = ""
        LogoBailleur.Image = Image.FromFile(line & "\LogoProjet\blanc.GIF")
    End Sub

    Private Sub ModificationBailleur()

        Try
            'on instancie l'objet DataSet avec de l'utiliser

            If TxtExt.Text = "" Then
            Else
                Dim logo As String = ""


               query= "select LogoBailleur from T_Bailleur where CodeBailleur='" & TxtCodeBailleur.Text & "'"
                logo = ExecuteScallar(query)


                Dim ChemImg As String = logo.ToString
                LogoBailleur.Image.Dispose()
                LogoBailleur.Image = Nothing
                If Mid(logo, 1, 3) = "Tmp" Then
                    Dim cpt As Decimal = Val(Mid(logo, 4))
                    File.Copy(TxtLogo.Text, line & "\LogoProjet\Tmp" & cpt + 1 & TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text, True)
                   query= "UPDATE T_Bailleur SET LogoBailleur='Tmp" & cpt + 1 & TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text & "' where CodeBailleur='" & TxtCodeBailleur.Text & "'"
                    ExecuteNonQuery(query)
                Else
                    File.Copy(TxtLogo.Text, line & "\LogoProjet\Tmp" & TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text, True)
                   query= "UPDATE T_Bailleur SET LogoBailleur='Tmp" & TxtSigle.Text & ProjetEnCours & "." & TxtExt.Text & "' where CodeBailleur='" & TxtCodeBailleur.Text & "'"
                    ExecuteNonQuery(query)
                End If
            End If


           query= "update T_Bailleur set AdresseCompleteBailleur='" + EnleverApost(TxtAdresse.Text) + "',InitialeBailleur='" & EnleverApost(TxtSigle.Text) & "' , NomBailleur='" + EnleverApost(TxtNomBailleur.Text) + "', Siteweb='" + EnleverApost(TxtSiteWebBailleur.Text) + "', MailTTl='" + EnleverApost(MailTTL.Text) + "', NomTTl='" + EnleverApost(TxtNomTtl.Text) + "', PrenomTTL='" + EnleverApost(TxtPrenomTtl.Text) + "', FonctionTTL='" + EnleverApost(TxtFonctionTtl.Text) + "', TelTTL='" + TxtTelTtl.Text + "', FaxTTL='" + TxtFaxTtl.Text + "' where CodeBailleur='" & TxtCodeBailleur.Text & "'"
            ExecuteNonQuery(query)


        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try

    End Sub
    Private Sub GenererCodeBailler()
        'on  remplit le dataTable du  Dataset 
        Dim DatSet = New DataSet
        query = "select * from T_Bailleur"

        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)

        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_Bailleur")
        Dim DatTable = DatSet.Tables("T_Bailleur")
        'on teste ici pour voir s'il y a un enregistrement non
        If DatSet.HasErrors = False Then

            'requete pour compter le nb d'enregistrement

           query= "select COUNT(*) from T_Bailleur"
            som = ExecuteScallar(query)

            'appel de la procédure pour déterminer le Code de la Catégorie suivante à ajouter 
            CreationCodeBailleur(som)
        End If

    End Sub

    Private Sub Bailleur_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub ActualiserDevise_Click(sender As System.Object, e As System.EventArgs) Handles ActualiserDevise.Click
        Dialog_form(Zonegeo)
    End Sub

End Class