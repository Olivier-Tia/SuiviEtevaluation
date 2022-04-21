Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Data
Imports Microsoft.Office.Interop.Word
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices


Public Class RapportSuiviFinancierClear

    Dim DossierFerme As Boolean = True
    Dim NomDossier As String = ""
    Dim dtConsult = New Data.DataTable()
    Dim dtFournit = New Data.DataTable()
    Dim dtTravaux = New Data.DataTable()
    Dim dtServAss = New Data.DataTable()
    Dim DrX As DataRow
    Dim EtapeConsult(100) As Boolean
    Dim nbEtpCons As Decimal = 0
    Dim EtapeFournit(100) As Boolean
    Dim nbEtpFour As Decimal = 0
    Dim EtapeTrvx(100) As Boolean
    Dim nbEtpTrvx As Decimal = 0
    Dim EtapeServAss(100) As Boolean
    Dim nbEtpServAss As Decimal = 0
    Dim Bailleur As String = String.Empty

    Private Sub RapportSuiviFinancierClear_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        MiseAneuf()
        LoadRSF()
    End Sub

    Private Sub LoadRSF()
        Dim dtArchives = New Data.DataTable()
        dtArchives.Columns.Clear()
        dtArchives.Columns.Add("Code", Type.GetType("System.String"))
        dtArchives.Columns.Add("Période", Type.GetType("System.String"))
        dtArchives.Columns.Add("Bailleur", Type.GetType("System.String"))
        dtArchives.Columns.Add("Edité le", Type.GetType("System.String"))
        dtArchives.Columns.Add("Edité par", Type.GetType("System.String"))
        dtArchives.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select R.PeriodeRSF,R.DateEdition,R.Bailleur,R.OperateurEdition,O.CiviliteOperateur,O.NomOperateur,O.PrenOperateur from T_RSF as R,T_Operateur as O where R.OperateurEdition=O.UtilOperateur and R.CodeProjet='" & ProjetEnCours & "' order by R.DateEdition desc"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtArchives.NewRow()
            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw("PeriodeRSF").ToString
            drS(2) = MettreApost(rw("Bailleur").ToString)
            drS(3) = Mid(rw("DateEdition").ToString, 1, 10)
            drS(4) = MettreApost(rw("CiviliteOperateur").ToString & " " & rw("NomOperateur").ToString & " " & rw("PrenOperateur").ToString)
            dtArchives.Rows.Add(drS)
        Next

        GridRapPrec.DataSource = dtArchives
        ViewRapPrec.Columns(0).Visible = False
        ViewRapPrec.Columns(1).Width = 150
        ViewRapPrec.Columns(2).Width = 100
        ViewRapPrec.Columns(3).Width = 100
        ViewRapPrec.Columns(4).Width = 200
        ViewRapPrec.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewRapPrec.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewRapPrec.Appearance.Row.Font = New Drawing.Font("Times New Roman", 10, FontStyle.Regular)
        ColorRowGrid(ViewRapPrec, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
    End Sub

    Private Sub RapportSuiviFinancierClear_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub DTDebPeriode_DateTimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTDebPeriode.DateTimeChanged
        ActualiserBtRSF()
    End Sub

    Private Sub ActualiserBtRSF()
        If IsDate(DTDebPeriode.Text) And IsDate(DTFinPeriode.Text) Then
            BtOuvrirRSF.Text = "Ouvrir le dossier du " & DTDebPeriode.DateTime.ToShortDateString & " au " & DTFinPeriode.DateTime.ToShortDateString & "."
            BtOuvrirRSF.Enabled = True
        Else
            BtOuvrirRSF.Enabled = False
        End If

    End Sub

    Private Sub DTFinPeriode_DateTimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTFinPeriode.DateTimeChanged
        ActualiserBtRSF()
    End Sub

    Private Sub BtOuvrirRSF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtOuvrirRSF.Click
        Dim LeTexte As String() = BtOuvrirRSF.Text.Split(" "c)
        Dim LeDebut As String = LeTexte(0)

        If (LeDebut = "Ouvrir") Then

            'Verification rapport sur cette période **********
            If cmbBailleur.SelectedIndex = -1 Then
                SuccesMsg("Veuillez choisir un bailleur.")
                Exit Sub
            End If

            Bailleur = cmbBailleur.Text

            query = "select count(*) from T_RSF where Bailleur='" & EnleverApost(cmbBailleur.Text) & "' AND PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Un rapport a déjà été établi sur cette période.")
                Exit Sub
            End If

            DebutChargement(True, "Récupération des informations de la période en cours...")

            '*************************************************
            DossierFerme = False

            Dim DossierRSF As String = (DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString) & " - " & Bailleur
            'Enlever les caractères bloqués dans la nomination du dossier
            DossierRSF = DossierRSF.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")

            NomDossier = line & "\RSF\" & DossierRSF


            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If

            NomDossier = NomDossier & "\PJ"
            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If

            EnregistrerTabMat()
            PnlPeriode.Enabled = False
            BtOuvrirRSF.Text = "Fermer le dossier du " & DTDebPeriode.DateTime.ToShortDateString & " au " & DTFinPeriode.DateTime.ToShortDateString & "."
            PnlSommaire.Enabled = True

            'Ouverture du dossier RSF **************************
            Dim DateDebutRSF As Date = DTDebPeriode.DateTime.Date
            Dim DateFinRSF As Date = DTFinPeriode.DateTime.Date
            Dim PeriodeRSF As String = DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString
            Dim DateEdition As String = Now.ToShortDateString & " " & Now.ToLongTimeString
            query = "INSERT INTO T_RSF(PeriodeRSF,DateEdition,OperateurEdition,DateModif,Operateur,CodeProjet,Bailleur) VALUES('" &
                PeriodeRSF & "','" & DateEdition & "','" & CodeUtilisateur & "','" & DateEdition & "','" & CodeUtilisateur & "','" & ProjetEnCours & "','" & EnleverApost(Bailleur) & "')"
            ExecuteNonQuery(query)

            LoadRSF()

            '**************************************************************************************************

        ElseIf (LeDebut = "Fermer") Then

            If Bailleur = String.Empty Then
                FailMsg("Nous n'avons pas pu récupérer le bailleur")
                Exit Sub
            End If

            Dim DateDebutRSF As Date = DTDebPeriode.DateTime.Date
            Dim DateFinRSF As Date = DTFinPeriode.DateTime.Date
            Dim PeriodeRSF As String = DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString

            BtSelectPJ.Enabled = False

            'Mise à jour des dates ********************************
            query = "Update T_RSF set DateModif='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "', Operateur='" & CodeUtilisateur & "' where PeriodeRSF='" & PeriodeRSF & "' and CodeProjet='" & ProjetEnCours & "' AND Bailleur='" & EnleverApost(Bailleur) & "'"
            ExecuteNonQuery(query)

            '******************************************************

            If (ChkAvanceProjet.Checked = True) Then
                Dim NomRepertoire As String = DTDebPeriode.DateTime.ToShortDateString.Replace("/", "") & "_" & DTFinPeriode.DateTime.ToShortDateString & " - " & Bailleur
                'Enlever les caractères bloqués dans la nomination du dossier
                NomRepertoire = NomRepertoire.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")
                NomRepertoire = line & "\RSF\" & NomRepertoire
                If Not Directory.Exists(NomRepertoire) Then
                    Directory.CreateDirectory(NomRepertoire)
                End If
                RTRapportProjet.SaveDocument(NomRepertoire & "\ExamenAvancementProjet.docx", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                'mise à jour dans la BD **************************************
                query = "Update T_RSF set ExamAvanceProjet='ExamenAvancementProjet.docx', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "' AND Bailleur='" & EnleverApost(Bailleur) & "'"
                ExecuteNonQuery(query)

                '**************************************************************
            End If

            DossierFerme = True
            BtOuvrirRSF.Text = "..."
            MiseAneuf()

        ElseIf (LeDebut = "Dossier") Then

            BtSelectPJ.Enabled = False
            DossierFerme = True
            BtOuvrirRSF.Text = "..."
            MiseAneuf()

        End If

        GridPieceJ.DataSource = Nothing
        GridPieceJ.Refresh()

        FinChargement()
    End Sub

    Private Sub MiseAneuf()
        DossierFerme = True
        LoadCompteBailleur()
        DTDebPeriode.Text = ""
        DTFinPeriode.Text = ""
        BtOuvrirRSF.Enabled = False
        ChkAvanceProjet.Checked = False
        ChkTableauFonds.Checked = False
        ChkEmploiFondsActivite.Checked = False
        ChkRealisationPhysq.Checked = False
        ChkRapportMarches.Checked = False
        ChkPiecesJointes.Checked = False

        GridPieceJ.DataSource = Nothing
        GridPieceJ.Refresh()
        BtImpRSF.Enabled = False
        PnlPeriode.Enabled = True
        PnlSommaire.Enabled = False
        GbEtapes.Enabled = True
        RTRapportProjet.ReadOnly = False
        SplitContainerControl2.Collapsed = False

        TabExamenAvancement.PageVisible = False
        TabRessourcesEmplois.PageVisible = False
        TabEmploisParActivite.PageVisible = False
        TabRealisationPhysique.PageVisible = False
        TabPassationMarche.PageVisible = False

        AppercuRSF.ReportSource = Nothing
        AppercuRSF.Refresh()

        If Not DTDebPeriode.Enabled Then
            DTDebPeriode.Enabled = True
        End If

        If Not DTFinPeriode.Enabled Then
            DTFinPeriode.Enabled = True
        End If

        If Not cmbBailleur.Enabled Then
            cmbBailleur.Enabled = True
        End If

        Bailleur = String.Empty
    End Sub

    Private Sub VerifDossier()

        If (ChkAvanceProjet.Checked = True Or ChkTableauFonds.Checked = True Or ChkEmploiFondsActivite.Checked = True _
           Or ChkRealisationPhysq.Checked = True Or ChkRapportMarches.Checked = True Or ChkPiecesJointes.Checked = True) Then
            BtImpRSF.Enabled = True
        Else
            BtImpRSF.Enabled = False
        End If

    End Sub

    Private Sub ChkAvanceProjet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAvanceProjet.CheckedChanged
        If (ChkAvanceProjet.Checked = True) Then

            Dim dossierrsf As String

            dossierrsf = DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString & " - " & Bailleur
            'Enlever les caractères bloqués dans la nomination du dossier
            dossierrsf = dossierrsf.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")

            Dim nomFichier As String = "ExamenAvancementProjet.docx"
            If (nomFichier <> "NON" And nomFichier <> "") Then
                Dim OldFile As String = line & "\RSF\" & dossierrsf & "\" & nomFichier
                Try
                    If File.Exists(OldFile) Then
                        RTRapportProjet.LoadDocument(OldFile, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                        RTRapportProjet.ReadOnly = False
                    Else
                        RTRapportProjet.ResetText()
                        RTRapportProjet.ReadOnly = False
                    End If
                Catch ex As Exception
                    'FailMsg("Impossible d'ouvrir le fichier de l'examen d'avancement." & vbNewLine & "Le fichier est introuvable")
                End Try
            End If

            TabExamenAvancement.PageVisible = True
            TabControl.SelectedTabPage = TabExamenAvancement
            query = "Update T_RSF set ExamAvanceProjet='ExamenAvancementProjet.docx', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' AND Bailleur='" & EnleverApost(Bailleur) & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)
        Else
            TabExamenAvancement.PageVisible = False

            If (DTDebPeriode.Text <> "" And DTFinPeriode.Text <> "") Then
                'Mise à jour dans la BD ******************************
                query = "Update T_RSF set ExamAvanceProjet='NON', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' AND Bailleur='" & EnleverApost(Bailleur) & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

        End If

        VerifDossier()
        EnregistrerTabMat()
    End Sub

    Private Sub ChkTableauFonds_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTableauFonds.CheckedChanged
        If (DossierFerme = False) Then
            If (ChkTableauFonds.Checked = True) Then
                RptViewerRessourcesEmplois.Enabled = True
                'Mise à jour dans la BD ******************************
                query = "Update T_RSF set TabEmploiRessources='OUI', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                '********************************************************
                Dim DateDebutRSF As Date = DTDebPeriode.DateTime.Date
                Dim DateFinRSF As Date = DTFinPeriode.DateTime.Date

                DebutChargement(True, "Analyse des données financières de la période en cours...")
                ChargerEtatEmploiRessources(DateDebutRSF, DateFinRSF)
                TabRessourcesEmplois.PageVisible = True
                TabControl.SelectedTabPage = TabRessourcesEmplois
            Else
                RptViewerRessourcesEmplois.Enabled = False
                'Mise à jour dans la BD ******************************
                query = "Update T_RSF set TabEmploiRessources='NON', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                '********************************************************
                TabRessourcesEmplois.PageVisible = False

                Dim periode As String = DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString

                'query = "SELECT TabEmploiRessources,EmploiFondsActivite,RealisationPhysq,PassationMarches WHERE PeriodeRSF='" & periode & "'"
                'Dim dtRSF As Data.DataTable = ExcecuteSelectQuery(query)

                query = "DELETE FROM t_etatcompoemploires WHERE Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & periode & "'"
                ExecuteNonQuery(query)
                query = "DELETE FROM t_etatfondsemploires WHERE Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & periode & "'"
                ExecuteNonQuery(query)
                query = "DELETE FROM t_rsf_disponible_treso WHERE Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & periode & "' AND CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If
            EnregistrerTabMat()

        End If
        VerifDossier()
        FinChargement()
    End Sub
    Private Sub ChargerEtatEmploiRessources(DateDebutRSf As Date, DateFinRSF As Date)
        Dim PeriodeRSF As String = DateDebutRSf.ToShortDateString & " - " & DateFinRSF.ToShortDateString
        Dim TotalFondsPercu As Decimal = 0
        Dim TotalFondsCumule As Decimal = 0
        Dim LimiteMontant As Decimal = 3
        Dim LesZeros As String = String.Empty
        Dim InitialeBailleur As String = String.Empty
        Dim OptBailleur As String = String.Empty

        If cmbBailleur.SelectedIndex > 0 Then
            InitialeBailleur = cmbBailleur.Text.Split(" - ")(0)
            OptBailleur = " AND InitialeBailleur='" & InitialeBailleur & "'"
        End If

        For i = 1 To LimiteMontant
            LesZeros += "0"
        Next

        'Recuperation des ressources sur la periode du RSF

        'Recuperation des comptes bailleurs
        query = "SELECT * FROM t_rsf_ressources where CodeProjet='" & ProjetEnCours & "'" & OptBailleur
        Dim dtRessources As Data.DataTable = ExcecuteSelectQuery(query)
        If dtRessources.Rows.Count > 0 Then
            'On va chercher si les comptes des bailleurs ont été approvionner sur la période
            For Each rwCompte As DataRow In dtRessources.Rows
                If rwCompte("Type") = "Débit" Then
                    query = "SELECT SUM(DEBIT_LE) As TotalApprovionnement FROM t_comp_ligne_ecriture WHERE CODE_SC='" & rwCompte("Code_SC") & "' AND CODE_PROJET='" & ProjetEnCours & "' AND DATE_LE<='" & dateconvert(DateFinRSF.ToShortDateString()) & "' AND DATE_LE>='" & dateconvert(DateDebutRSf.ToShortDateString()) & "'"
                Else
                    query = "SELECT SUM(CREDIT_LE) As TotalApprovionnement FROM t_comp_ligne_ecriture WHERE CODE_SC='" & rwCompte("Code_SC") & "' AND CODE_PROJET='" & ProjetEnCours & "' AND DATE_LE<='" & dateconvert(DateFinRSF.ToShortDateString()) & "' AND DATE_LE>='" & dateconvert(DateDebutRSf.ToShortDateString()) & "'"
                End If
                Dim rwRessource As DataRow = ExcecuteSelectQuery(query).Rows(0)
                Dim LibelleFonds As String = MettreApost(rwCompte("Libelle"))
                Dim MontantPercu As Decimal = 0
                If Not IsDBNull(rwRessource("TotalApprovionnement")) Then
                    MontantPercu = CDec(rwRessource("TotalApprovionnement"))
                End If
                MontantPercu = ReduireMontant(MontantPercu, LimiteMontant)
                TotalFondsPercu += MontantPercu

                If rwCompte("Type") = "Débit" Then
                    query = "SELECT SUM(DEBIT_LE) As CummulApprovionnement FROM t_comp_ligne_ecriture WHERE CODE_SC='" & rwCompte("Code_SC") & "' AND CODE_PROJET='" & ProjetEnCours & "' And DATE_LE<='" & dateconvert(DateFinRSF.ToShortDateString()) & "'" ' AND DATE_LE>='" & dateconvert(DateDebutRSf.ToShortDateString()) & "'
                Else
                    query = "SELECT SUM(CREDIT_LE) As CummulApprovionnement FROM t_comp_ligne_ecriture WHERE CODE_SC='" & rwCompte("Code_SC") & "' AND CODE_PROJET='" & ProjetEnCours & "' AND DATE_LE<='" & dateconvert(DateFinRSF.ToShortDateString()) & "'" ' AND DATE_LE>='" & dateconvert(DateDebutRSf.ToShortDateString()) & "'
                End If

                Dim MontantCummule As Decimal = 0
                rwRessource = ExcecuteSelectQuery(query).Rows(0)
                If Not IsDBNull(rwRessource("CummulApprovionnement")) Then
                    MontantCummule = CDec(rwRessource("CummulApprovionnement"))
                End If
                MontantCummule = ReduireMontant(MontantCummule, LimiteMontant)
                TotalFondsCumule += MontantCummule

                If Mid(BtOuvrirRSF.Text, 1, 7) <> "Dossier" Then 'On ajoute dans la base de donnees uniquement si c'est un nouveau RSF
                    query = "SELECT COUNT(*) FROM t_etatfondsemploires WHERE SourceFonds='" & EnleverApost(LibelleFonds) & "' AND MontantPeriode='" & MontantPercu & "' AND MontantCumule='" & MontantCummule & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & PeriodeRSF & "'"
                    If Val(ExecuteScallar(query)) = 0 Then
                        query = "INSERT INTO t_etatfondsemploires(RefEtat,SourceFonds,MontantPeriode,MontantCumule,PeriodeRSF,Bailleur) VALUES(NULL,'" & EnleverApost(LibelleFonds) & "','" & MontantPercu & "','" & MontantCummule & "','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "')"
                        ExecuteNonQuery(query)
                    End If
                End If
            Next
        End If

        'Les emplois par composante et activités *************************************************

        query = "select CodePartition,LibelleCourt,LibellePartition from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        Dim dtComposante As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rwComposante As DataRow In dtComposante.Rows
            Dim MontantComposante As Decimal = GetDepense("Composante", rwComposante("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur)

            Dim MontantCummuleComposante As Decimal = GetDepense("Composante", rwComposante("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur, True)

            If MontantComposante = 0 And MontantCummuleComposante = 0 Then
                Continue For
            End If

            MontantComposante = ReduireMontant(MontantComposante, LimiteMontant)
            MontantCummuleComposante = ReduireMontant(MontantCummuleComposante, LimiteMontant)

            If Mid(BtOuvrirRSF.Text, 1, 7) <> "Dossier" Then 'On ajoute dans la base de donnees uniquement si c'est un nouveau RSF
                query = "SELECT COUNT(*) FROM T_EtatCompoEmploiRes WHERE RefEtatCompo='" & rwComposante("CodePartition") & "' AND LibCourtCompo='" & rwComposante("LibelleCourt") & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND LibelleCompo='" & rwComposante("LibellePartition") & "' AND MontCompoPeriode='" & MontantComposante & "' AND MontCompoCumul='" & MontantCummuleComposante & "' AND PeriodeRSF='" & PeriodeRSF & "'"
                If Val(ExecuteScallar(query)) = 0 Then
                    query = "INSERT INTO T_EtatCompoEmploiRes(NumOrdre,RefEtatCompo,LibCourtCompo,LibelleCompo,MontCompoPeriode,MontCompoCumul,PeriodeRSF,Bailleur) VALUES(NULL,'" & rwComposante("CodePartition") & "','" & rwComposante("LibelleCourt") & "','" & rwComposante("LibellePartition") & "','" & MontantComposante & "','" & MontantCummuleComposante & "','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "')"
                    ExecuteNonQuery(query)
                End If
            End If

            query = "SELECT DISTINCT LibelleCourt from t_comp_activite where (('" & dateconvert(DateFinRSF) & "' BETWEEN Date_act AND DateSys) OR (DateSys<='" & dateconvert(DateFinRSF) & "' AND DateSys>='" & dateconvert(DateDebutRSf) & "')) AND LibelleCourt LIKE '" & rwComposante("LibelleCourt") & "%' AND CODE_PROJET='" & ProjetEnCours & "' GROUP BY LibelleCourt,codepartition"
            Dim dtActivites As Data.DataTable = ExcecuteSelectQuery(query)
            For Each rwActivites In dtActivites.Rows
                Dim CodePartition As String = ExecuteScallar("SELECT codepartition FROM t_partition where LibelleCourt='" & rwActivites("LibelleCourt") & "'")
                Dim LibelleActivite As String = ExecuteScallar("SELECT LibellePartition FROM t_partition where LibelleCourt='" & rwActivites("LibelleCourt") & "'")
                Dim MontantActivite As Decimal = GetDepense("Activité", rwActivites("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur)
                Dim MontantCummuleActivite As Decimal = GetDepense("Activité", rwActivites("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur, True)

                If MontantActivite = 0 And MontantCummuleActivite = 0 Then
                    Continue For
                End If

                MontantActivite = ReduireMontant(MontantActivite, LimiteMontant)
                MontantCummuleActivite = ReduireMontant(MontantCummuleActivite, LimiteMontant)

                'Dim MontantActivite As Decimal = CDec(ExecuteScallar("SELECT SUM(Montant_act) As TotalActivite from t_comp_activite where Date_act<='" & dateconvert(DateFinRSF) & "' and Date_act>='" & dateconvert(DateDebutRSf) & "' and LibelleCourt='" & rwActivites("LibelleCourt") & "' AND CODE_PROJET='" & ProjetEnCours & "'"))
                'Dim MontantCummuleActivite As Decimal = CDec(ExecuteScallar("SELECT SUM(Montant_act) As TotalActivite from t_comp_activite where LibelleCourt='" & rwActivites("LibelleCourt") & "' AND CODE_PROJET='" & ProjetEnCours & "'"))

                If Mid(BtOuvrirRSF.Text, 1, 7) <> "Dossier" Then 'On ajoute dans la base de donnees uniquement si c'est un nouveau RSF
                    query = "SELECT COUNT(*) FROM T_EtatCompoEmploiRes WHERE RefEtatCompo='" & CodePartition & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND LibCourtCompo='" & rwActivites("LibelleCourt") & "' AND LibelleCompo='" & LibelleActivite & "' AND MontCompoPeriode='" & MontantActivite & "' AND MontCompoCumul='" & MontantCummuleActivite & "' AND PeriodeRSF='" & PeriodeRSF & "'"
                    If Val(ExecuteScallar(query)) = 0 Then
                        query = "INSERT INTO T_EtatCompoEmploiRes(NumOrdre,RefEtatCompo,LibCourtCompo,LibelleCompo,MontCompoPeriode,MontCompoCumul,PeriodeRSF,Bailleur) VALUES(NULL,'" & CodePartition & "','" & rwActivites("LibelleCourt") & "','" & LibelleActivite & "','" & MontantActivite & "','" & MontantCummuleActivite & "','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "')"
                        ExecuteNonQuery(query)
                    End If
                End If
            Next
        Next

        'Chargement des soldes de la trésorerie
        Dim TSoldeOuvert As Decimal = 0
        Dim TSoldeEncaisse As Decimal = 0
        query = "SELECT COUNT(*) FROM t_rsf_disponible_treso WHERE PeriodeRSF='" & PeriodeRSF & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND Justificatif='Avant'"
        If Val(ExecuteScallar(query)) = 0 Then

            If InitialeBailleur = "" Then
                query = "SELECT CodeBailleur from t_bailleur WHERE CodeProjet='" & ProjetEnCours & "'"
            Else
                ' Dim CodeBailleur As Decimal = Val(ExecuteScallar("SELECT CodeBailleur from t_bailleur WHERE InitialeBailleur='" & InitialeBailleur & "'"))
                query = "SELECT CodeBailleur from t_bailleur WHERE InitialeBailleur='" & InitialeBailleur & "'"
            End If

            Dim rwCodbailleur As Data.DataTable = ExcecuteSelectQuery(query)

            For Each rw0 In rwCodbailleur.Rows

                ' query = "select DISTINCT LibelleCompte, CodeProjet, NumeroComptable from T_CompteBancaire where CodeBailleur='" & CodeBailleur & "' AND CodeProjet='" & ProjetEnCours & "'"

                query = "select DISTINCT LibelleCompte, CodeProjet, NumeroComptable from T_CompteBancaire where CodeBailleur='" & rw0("CodeBailleur") & "' AND CodeProjet='" & ProjetEnCours & "'"
                Dim dtComptes As Data.DataTable = ExcecuteSelectQuery(query)

                For Each rwCompte As DataRow In dtComptes.Rows
                    query = "select (SUM(DEBIT_LE)-SUM(CREDIT_LE)) as Solde from t_comp_ligne_ecriture where Date_le<'" & dateconvert(DateDebutRSf) & "' AND code_sc='" & rwCompte("NumeroComptable") & "'"
                    Dim dtSolde As Data.DataTable = ExcecuteSelectQuery(query)
                    Dim Solde As Decimal = 0
                    For Each rw As DataRow In dtSolde.Rows
                        If Not IsDBNull(rw("Solde")) Then
                            Solde = CDec(rw("Solde"))
                        End If
                    Next
                    Solde = ReduireMontant(Solde, LimiteMontant)
                    TSoldeOuvert += Solde
                    query = "INSERT INTO t_rsf_disponible_treso VALUES(NULL,'" & rwCompte("LibelleCompte") & "','" & Solde & "','Avant','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
                    ExecuteNonQuery(query)

                    query = "select (SUM(DEBIT_LE)-SUM(CREDIT_LE)) as Solde from t_comp_ligne_ecriture where Date_le<='" & dateconvert(DateFinRSF) & "' AND code_sc='" & rwCompte("NumeroComptable") & "'"
                    dtSolde = ExcecuteSelectQuery(query)
                    Solde = 0
                    For Each rw As DataRow In dtSolde.Rows
                        If Not IsDBNull(rw("Solde")) Then
                            Solde = CDec(rw("Solde"))
                        End If
                    Next
                    Solde = ReduireMontant(Solde, LimiteMontant)
                    TSoldeEncaisse += Solde
                    query = "INSERT INTO t_rsf_disponible_treso VALUES(NULL,'" & rwCompte("LibelleCompte") & "','" & Solde & "','Après','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
                    ExecuteNonQuery(query)
                Next
            Next

            'Insertion du solde de la caisse
            'query = "select (SUM(DEBIT_LE)-SUM(CREDIT_LE)) as Solde from t_comp_ligne_ecriture where Date_le<'" & dateconvert(DateDebutRSf) & "' AND code_sc='571100'"
            'Dim dtSoldecaisse As Data.DataTable = ExcecuteSelectQuery(query)
            'Dim SoldeCaisse As Decimal = 0
            'For Each rw As DataRow In dtSoldecaisse.Rows
            '    If Not IsDBNull(rw("Solde")) Then
            '        SoldeCaisse = CDec(rw("Solde"))
            '    End If
            'Next
            'SoldeCaisse = ReduireMontant(SoldeCaisse, LimiteMontant)
            'TSoldeOuvert += SoldeCaisse
            'query = "INSERT INTO t_rsf_disponible_treso VALUES(NULL,'Caisse " & ProjetEnCours & " (571100)','" & SoldeCaisse & "','Avant','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
            'ExecuteNonQuery(query)

            'query = "select (SUM(DEBIT_LE)-SUM(CREDIT_LE)) as Solde from t_comp_ligne_ecriture where Date_le<='" & dateconvert(DateFinRSF) & "' AND code_sc='571100'"
            'dtSoldecaisse = ExcecuteSelectQuery(query)
            'SoldeCaisse = 0
            'For Each rw As DataRow In dtSoldecaisse.Rows
            '    If Not IsDBNull(rw("Solde")) Then
            '        SoldeCaisse = CDec(rw("Solde"))
            '    End If
            'Next
            'SoldeCaisse = ReduireMontant(SoldeCaisse, LimiteMontant)
            'TSoldeEncaisse += SoldeCaisse
            'query = "INSERT INTO t_rsf_disponible_treso VALUES(NULL,'Caisse " & ProjetEnCours & " (571100)','" & SoldeCaisse & "','Après','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
            'ExecuteNonQuery(query)
        Else
            TSoldeOuvert = Val(ExecuteScallar("SELECT SUM(Solde) FROM t_rsf_disponible_treso WHERE PeriodeRSF='" & PeriodeRSF & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND CodeProjet='" & ProjetEnCours & "' AND Justificatif='Avant'"))
            TSoldeEncaisse = Val(ExecuteScallar("SELECT SUM(Solde) FROM t_rsf_disponible_treso WHERE PeriodeRSF='" & PeriodeRSF & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND CodeProjet='" & ProjetEnCours & "' AND Justificatif='Après'"))
        End If


        'Affichage de l'état ********************************************************************
        Dim LibelleProjet As String = ""
        query = "select NomProjet,PaysProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim LeGouv As String = rw(1).ToString
            LeGouv = "GOUVERNEMENT DE L'ETAT DE " & MettreApost(LeGouv).ToUpper
            LibelleProjet = MettreApost(rw(0).ToString.ToUpper)
            LibelleProjet = LeGouv & vbNewLine & LibelleProjet & vbNewLine & "TABLEAU DES RECETTES ET DES PAIEMENTS"
            LibelleProjet = LibelleProjet & vbNewLine & "Pour la période du " & DTDebPeriode.DateTime.ToShortDateString & " au " & DTFinPeriode.DateTime.ToShortDateString
            LibelleProjet = LibelleProjet & vbNewLine & " en francs CFA (FCFA)"
        Next
        LibelleProjet = "TABLEAU DES RESSOURCES ET EMPLOIS" & vbNewLine & "Pour la période du " & DTDebPeriode.DateTime.ToShortDateString & " au " & DTFinPeriode.DateTime.ToShortDateString
        LibelleProjet += vbNewLine & "Montant en milliers de Francs"

        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Engine.Tables
        Dim CrTable As Engine.Table
        Dim Chemin As String = lineEtat & "\RSF\"
        Dim report As New Engine.ReportDocument
        Dim DatSet = New DataSet
        report.Load(Chemin & "RapportEmplResFonds.rpt")
        report.SetDataSource(DatSet)

        Dim TEmploiPeriode As Decimal = 0
        Dim TEmploiCumule As Decimal = 0
        query = "select MontCompoPeriode,MontCompoCumul from T_EtatCompoEmploiRes where LENGTH(LibCourtCompo)='1' AND PeriodeRSF='" & PeriodeRSF & "' AND Bailleur='" & EnleverApost(Bailleur) & "'"
        dt = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Dim fonPer As Decimal = CDec(rw(0).ToString.Replace(" ", ""))
            Dim fonCum As Decimal = CDec(rw(1).ToString.Replace(" ", ""))
            TEmploiPeriode = TEmploiPeriode + fonPer
            TEmploiCumule = TEmploiCumule + fonCum
        Next

        Dim SignePer As String = ""
        Dim MontExcedPer As Decimal = TotalFondsPercu - TEmploiPeriode
        If (MontExcedPer < 0) Then
            MontExcedPer = Math.Abs(MontExcedPer)
            SignePer = "-"
        End If
        Dim SigneCum As String = ""
        Dim MontExcedCum As Decimal = TotalFondsCumule - TEmploiCumule
        If (MontExcedCum < 0) Then
            MontExcedCum = Math.Abs(MontExcedCum)
            SigneCum = "-"
        End If

        Dim TotGenPer As Decimal = 0
        Dim TotGenCum As Decimal = 0
        TotGenPer = (TotalFondsPercu + TSoldeOuvert) - TEmploiPeriode
        TotGenCum = (TotalFondsCumule - TEmploiCumule) '+ TSoldeOuvert

        report.SetParameterValue("EnteteEmploiRes", LibelleProjet)

        report.SetParameterValue("TotFondsPeriode", AfficherMonnaie(TotalFondsPercu.ToString))
        report.SetParameterValue("TotFondsCumule", AfficherMonnaie(TotalFondsCumule.ToString))

        report.SetParameterValue("ExceDeficPer", SignePer & AfficherMonnaie(MontExcedPer.ToString))
        report.SetParameterValue("ExceDeficCum", SigneCum & AfficherMonnaie(MontExcedCum.ToString))

        report.SetParameterValue("TotEmploiPer", AfficherMonnaie(TEmploiPeriode.ToString))
        report.SetParameterValue("TotEmploiCum", AfficherMonnaie(TEmploiCumule.ToString))

        report.SetParameterValue("TotSoldeOuverture", AfficherMonnaie(TSoldeOuvert.ToString))
        report.SetParameterValue("TotEncaisse", AfficherMonnaie(TSoldeEncaisse.ToString))

        report.SetParameterValue("TotalGeneral", AfficherMonnaie(TotGenPer.ToString))
        report.SetParameterValue("TotalGeneralCumule", AfficherMonnaie(TotGenCum.ToString))

        report.SetParameterValue("PeriodeRSF", PeriodeRSF, "SRapportEmplResFonds.rpt")
        report.SetParameterValue("Bailleur", EnleverApost(Bailleur), "SRapportEmplResFonds.rpt")

        report.SetParameterValue("PeriodeRSF", PeriodeRSF, "SRapportEmplResCompo.rpt")
        report.SetParameterValue("Bailleur", EnleverApost(Bailleur), "SRapportEmplResCompo.rpt")

        report.SetParameterValue("CodeProjet", ProjetEnCours, "SRapportEmplResCompteBancaire.rpt")
        report.SetParameterValue("PeriodeRSF", PeriodeRSF, "SRapportEmplResCompteBancaire.rpt")
        report.SetParameterValue("Bailleur", EnleverApost(Bailleur), "SRapportEmplResCompteBancaire.rpt")

        report.SetParameterValue("CodeProjet", ProjetEnCours, "SRapportEmplResCompteBancaireSolde.rpt")
        report.SetParameterValue("PeriodeRSF", PeriodeRSF, "SRapportEmplResCompteBancaireSolde.rpt")
        report.SetParameterValue("Bailleur", EnleverApost(Bailleur), "SRapportEmplResCompteBancaireSolde.rpt")

        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With

        CrTables = report.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        Dim NomRepertoire As String = DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString & " - " & Bailleur
        'Enlever les caractères bloqués dans la nomination du dossier
        NomRepertoire = NomRepertoire.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")
        NomRepertoire = line & "\RSF\" & NomRepertoire & "\"

        report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, NomRepertoire & "EmploiRessources.pdf")
        RptViewerRessourcesEmplois.ReportSource = report

    End Sub

    Private Sub EnregistrerTabMat()

        If (DTDebPeriode.Text <> "" And DTFinPeriode.Text <> "") Then

            Dim LeTitre As String = ""
            query = "DELETE from T_RSFTabMat"
            ExecuteNonQuery(query)

            Dim DatSet As New DataSet
            Dim DatAdapt As MySqlDataAdapter
            Dim DatTable As Data.DataTable
            Dim DatRow As DataRow
            Dim CmdBuilder As MySqlCommandBuilder

            Dim NumOrdre As Decimal = 0

            For i As Integer = 0 To 5
                If (i = 0) Then LeTitre = "Examen de l'état d'avancement du projet"
                If (i = 1) Then LeTitre = "Tableau sur l'origine et l'emploi des fonds"
                If (i = 2) Then LeTitre = "Emploi des fonds par activité du projet"
                If (i = 3) Then LeTitre = "Rapport de suivi des réalisations physiques"
                If (i = 4) Then LeTitre = "Rapport de suivi de la passation des marchés"
                If (i = 5) Then LeTitre = "Annexes : pièce(s) jointe(s)"

                If ((ChkAvanceProjet.Checked = True And i = 0) Or (ChkTableauFonds.Checked = True And i = 1) Or (ChkEmploiFondsActivite.Checked = True And i = 2) Or (ChkRealisationPhysq.Checked = True And i = 3) Or (ChkRapportMarches.Checked = True And i = 4) Or (ChkPiecesJointes.Checked = True And i = 5)) Then

                    NumOrdre = NumOrdre + 1

                    DatSet = New DataSet
                    query = "select * from T_RSFTabMat"
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_RSFTabMat")
                    DatTable = DatSet.Tables("T_RSFTabMat")
                    DatRow = DatSet.Tables("T_RSFTabMat").NewRow()

                    CorrectionChaine(LeTitre)
                    DatRow("NumTabMat") = NumOrdre.ToString
                    DatRow("LibTabMat") = EnleverApost(LeTitre)

                    DatSet.Tables("T_RSFTabMat").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
                    CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
                    DatAdapt.Update(DatSet, "T_RSFTabMat")
                    DatSet.Clear()
                    BDQUIT(sqlconn)
                End If
            Next


            Dim LibelleRSF As String = ""
            query = "select NomProjet,PaysProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                LibelleRSF = "RAPPORTS DE SUIVI FINANCIER : " & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & vbNewLine
                LibelleRSF = LibelleRSF & "----------------------------------------" & vbNewLine
                LibelleRSF = LibelleRSF & rw(1).ToString & " - " & rw(0).ToString
                LibelleRSF = MettreApost(LibelleRSF).ToUpper()
            Next

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Engine.Tables
            Dim CrTable As Engine.Table
            Dim Chemin As String = lineEtat & "\RSF\"
            Dim report As New Engine.ReportDocument
            DatSet = New DataSet

            report.Load(Chemin & "RapportCompletRSF.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = report.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            report.SetDataSource(DatSet)
            report.SetParameterValue("TitreRSF", LibelleRSF)

            Dim Bailleur As String = "Tous les bailleurs"
            If cmbBailleur.SelectedIndex >= 0 Then
                Bailleur = cmbBailleur.Text
            End If
            Dim NomRepertoire As String = DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString & " - " & Bailleur
            'Enlever les caractères bloqués dans la nomination du dossier
            NomRepertoire = NomRepertoire.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")
            NomRepertoire = line & "\RSF\" & NomRepertoire
            If Not Directory.Exists(NomRepertoire) Then
                Directory.CreateDirectory(NomRepertoire)
            End If

            report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, NomRepertoire & "\TableMatiere.pdf")
            AppercuRSF.ReportSource = report

        End If


    End Sub

    Private Sub ChkEmploiFondsActivite_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkEmploiFondsActivite.CheckedChanged

        If (DossierFerme = False) Then

            If (ChkEmploiFondsActivite.Checked = True) Then
                RptViewerEmploisActivites.Enabled = True
                'Mise à jour dans la BD ******************************
                query = "Update T_RSF set EmploiFondsActivite='OUI', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                '********************************************************
                DebutChargement(True, "Analyse des données financières des activités en cours...")
                Dim DateDebutRSF As Date = DTDebPeriode.DateTime.Date
                Dim DateFinRSF As Date = DTFinPeriode.DateTime.Date
                ChargerEtatFondsParActivite(DateDebutRSF, DateFinRSF)
                TabEmploisParActivite.PageVisible = True
                TabControl.SelectedTabPage = TabEmploisParActivite
            Else
                RptViewerEmploisActivites.Enabled = False
                'Mise à jour dans la BD ******************************
                query = "Update T_RSF set EmploiFondsActivite='NON', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                '********************************************************
                TabEmploisParActivite.PageVisible = False

                Dim periode As String = DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString
                query = "DELETE FROM t_etatfondsparactivite WHERE PeriodeRSF='" & periode & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            EnregistrerTabMat()
        End If
        VerifDossier()
        FinChargement()

    End Sub

    Private Sub ChargerEtatFondsParActivite(DateDebutRSf As Date, DateFinRSF As Date)
        Dim PeriodeRSF As String = DateDebutRSf.ToShortDateString & " - " & DateFinRSF.ToShortDateString
        Dim LimiteMontant As Decimal = 3
        Dim LesZeros As String = String.Empty
        For i = 1 To LimiteMontant
            LesZeros += "0"
        Next

        Dim InitialeBailleur As String = String.Empty
        Dim OptBailleur As String = String.Empty

        If cmbBailleur.SelectedIndex > 0 Then
            InitialeBailleur = cmbBailleur.Text.Split(" - ")(0)
            OptBailleur = " AND InitialeBailleur='" & InitialeBailleur & "'"
        End If

        Dim TDotationPeriode As Decimal = 0
        Dim TRealisationPeriode As Decimal = 0
        Dim TDotationCumule As Decimal = 0
        Dim TRealisationCumule As Decimal = 0
        Dim TDotationProjet As Decimal = 0
        Dim TRealisationProjet As Decimal = 0
        Dim TMontantsPrecedents As Decimal = 0

        'Les codes et libelles des composantes et activites *************************************************

        query = "select DISTINCT LibelleCourt,LibellePartition from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt ASC"
        Dim dtComposante As Data.DataTable = ExcecuteSelectQuery(query)

        For Each rwComposante As DataRow In dtComposante.Rows

            Dim RealisationComposantePrecedent As Decimal = GetMontantPrecedent(rwComposante("LibelleCourt"), DateDebutRSf, Bailleur)
            Dim RealisationComposantePeriode As Decimal = ReduireMontant(GetDepense("Composante", rwComposante("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur), LimiteMontant)
            Dim DateDebutAnnee As Date = CDate("01/01/" & DateDebutRSf.Year)
            Dim RealisationCumuleComposante As Decimal = ReduireMontant(GetDepense("Composante", rwComposante("LibelleCourt"), DateDebutAnnee, DateFinRSF, InitialeBailleur), LimiteMontant)
            Dim RealisationTotalComposante As Decimal = ReduireMontant(GetDepense("Composante", rwComposante("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur, True), LimiteMontant)

            Dim DotationComposantePeriode As Decimal = ReduireMontant(GetDotation("Composante", rwComposante("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur), LimiteMontant)
            Dim DotationCumuleComposante As Decimal = ReduireMontant(GetDotation("Composante", rwComposante("LibelleCourt"), DateDebutAnnee, DateFinRSF, InitialeBailleur), LimiteMontant)
            Dim DotationTotalComposante As Decimal = ReduireMontant(GetDotation("Composante", rwComposante("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur, True), LimiteMontant)

            TMontantsPrecedents += RealisationComposantePrecedent
            TDotationPeriode += DotationComposantePeriode
            TRealisationPeriode += RealisationComposantePeriode
            TDotationCumule += DotationCumuleComposante
            TRealisationCumule += RealisationCumuleComposante
            TDotationProjet += DotationTotalComposante
            TRealisationProjet += RealisationTotalComposante

            query = "select count(*) from t_etatfondsparactivite where PeriodeRSF='" & PeriodeRSF & "' and Bailleur='" & EnleverApost(Bailleur) & "' and CodeProjet='" & ProjetEnCours & "' and LibCourtActivite='" & rwComposante("LibelleCourt") & "' and LibelleFondsActivite='" & EnleverApost(rwComposante("LibellePartition")) & "' and MontantPrecedent='" & RealisationComposantePrecedent.ToString().Replace(",", ".") & "' and DotationPeriode='" & DotationComposantePeriode.ToString().Replace(",", ".") & "' and RealisationPeriode='" & RealisationComposantePeriode.ToString().Replace(",", ".") & "' and DotationCumule='" & DotationCumuleComposante.ToString().Replace(",", ".") & "' and RealisationCumule='" & RealisationCumuleComposante.ToString().Replace(",", ".") & "' and DotationTotalProjet='" & DotationTotalComposante.ToString().Replace(",", ".") & "' and RealisationTotalProjet='" & RealisationTotalComposante.ToString().Replace(",", ".") & "'"

            If Val(ExecuteScallar(query)) = 0 Then 'Nouvaeu rapports ligne composant RSF
                query = "INSERT INTO t_etatfondsparactivite VALUES(NULL,'" & rwComposante("LibelleCourt") & "','" & rwComposante("LibellePartition") & "','" & RealisationComposantePrecedent.ToString().Replace(",", ".") & "','" & DotationComposantePeriode.ToString().Replace(",", ".") & "','" & RealisationComposantePeriode.ToString().Replace(",", ".") & "','" & DotationCumuleComposante.ToString().Replace(",", ".") & "','" & RealisationCumuleComposante.ToString().Replace(",", ".") & "','" & DotationTotalComposante.ToString().Replace(",", ".") & "','" & RealisationTotalComposante.ToString().Replace(",", ".") & "','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
                ExecuteNonQuery(query)
            End If

            query = "select DISTINCT LibelleCourt,LibellePartition from T_Partition where CodeClassePartition='5' and LibelleCourt like '" & rwComposante("LibelleCourt").ToString & "%' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
            Dim dtActivite As Data.DataTable = ExcecuteSelectQuery(query)

            For Each rwActivite As DataRow In dtActivite.Rows
                Dim RealisationActivitePrecedent As Decimal = GetMontantPrecedent(rwActivite("LibelleCourt"), DateDebutRSf, Bailleur)
                Dim RealisationActivitePeriode As Decimal = ReduireMontant(GetDepense("Activité", rwActivite("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur), LimiteMontant)
                Dim RealisationCumuleActivite As Decimal = ReduireMontant(GetDepense("Activité", rwActivite("LibelleCourt"), DateDebutAnnee, DateFinRSF, InitialeBailleur), LimiteMontant)
                Dim RealisationTotalActivite As Decimal = ReduireMontant(GetDepense("Activité", rwActivite("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur, True), LimiteMontant)

                Dim DotationActivitePeriode As Decimal = ReduireMontant(GetDotation("Activité", rwActivite("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur), LimiteMontant)
                Dim DotationCumuleActivite As Decimal = ReduireMontant(GetDotation("Activité", rwActivite("LibelleCourt"), DateDebutAnnee, DateFinRSF, InitialeBailleur), LimiteMontant)
                Dim DotationTotalActivite As Decimal = ReduireMontant(GetDotation("Activité", rwActivite("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur, True), LimiteMontant)

                'Dim PourcentageRealisationActivite As Decimal = Math.Round(DotationTotalActivitePeriode / RealisationTotalActivite, 2)

                query = "select count(*) from t_etatfondsparactivite where PeriodeRSF='" & PeriodeRSF & "' and Bailleur='" & EnleverApost(Bailleur) & "' and CodeProjet='" & ProjetEnCours & "' and LibCourtActivite='" & rwActivite("LibelleCourt") & "' and LibelleFondsActivite='" & EnleverApost(rwActivite("LibellePartition")) & "' and MontantPrecedent='" & RealisationActivitePrecedent.ToString().Replace(",", ".") & "' and DotationPeriode='" & DotationActivitePeriode.ToString().Replace(",", ".") & "' and RealisationPeriode='" & RealisationActivitePeriode.ToString().Replace(",", ".") & "' and DotationCumule='" & DotationCumuleActivite.ToString().Replace(",", ".") & "' and RealisationCumule='" & RealisationCumuleActivite.ToString().Replace(",", ".") & "' and DotationTotalProjet='" & DotationTotalActivite.ToString().Replace(",", ".") & "' and RealisationTotalProjet='" & RealisationTotalActivite.ToString().Replace(",", ".") & "'"

                If Val(ExecuteScallar(query)) = 0 Then 'Nouvaeu rapports ligne activité RSF
                    query = "INSERT INTO t_etatfondsparactivite VALUES(NULL,'" & rwActivite("LibelleCourt") & "','" & rwActivite("LibellePartition") & "','" & RealisationActivitePrecedent.ToString().Replace(",", ".") & "','" & DotationActivitePeriode.ToString().Replace(",", ".") & "','" & RealisationActivitePeriode.ToString().Replace(",", ".") & "','" & DotationCumuleActivite.ToString().Replace(",", ".") & "','" & RealisationCumuleActivite.ToString().Replace(",", ".") & "','" & DotationTotalActivite.ToString().Replace(",", ".") & "','" & RealisationTotalActivite.ToString().Replace(",", ".") & "','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
                    ExecuteNonQuery(query)
                End If
            Next
        Next

        'Affichage de l'état ********************************************************************
        Dim LibelleProjet As String = ""
        query = "select NomProjet,PaysProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt6 As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw6 As DataRow In dt6.Rows

            Dim LeGouv As String = rw6(1).ToString
            RestaurerChaine(LeGouv)
            LeGouv = "GOUVERNEMENT DE L'ETAT DE " & MettreApost(LeGouv.ToUpper)

            LibelleProjet = rw6(0).ToString
            RestaurerChaine(LibelleProjet)
            LibelleProjet = LibelleProjet.ToUpper()

            LibelleProjet = LeGouv & vbNewLine & LibelleProjet & vbNewLine & "UTILISATION DES FONDS PAR ACTIVITE DU PROJET"
            LibelleProjet = LibelleProjet & vbNewLine & "Pour la période du " & DTDebPeriode.DateTime.ToShortDateString & " au " & DTFinPeriode.DateTime.ToShortDateString
            LibelleProjet = LibelleProjet & vbNewLine & " en francs CFA (FCFA)"

        Next

        LibelleProjet = "UTILISATION DES FONDS PAR COMPOSANTE ET ACTIVITE" & vbNewLine & "Pour la période du " & DTDebPeriode.DateTime.ToShortDateString & " au " & DTFinPeriode.DateTime.ToShortDateString
        LibelleProjet += vbNewLine & "Montant en milliers de Francs"


        Dim Chemin As String = lineEtat & "\RSF\"
        Dim report As New Engine.ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Engine.Tables
        Dim CrTable As Engine.Table

        Dim DatSet As New DataSet

        report.Load(Chemin & "RapportEmploiFondsParActivite.rpt")

        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With

        CrTables = report.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        report.SetDataSource(DatSet)
        report.SetParameterValue("EnteteRapportDepense", LibelleProjet)
        report.SetParameterValue("TotPrevuPeriode", TDotationPeriode)
        report.SetParameterValue("TotRealisationPeriode", TRealisationPeriode)
        'report.SetParameterValue("TotEcartPeriode", (TDotationPeriode - TRealisationPeriode))
        report.SetParameterValue("TotPrevuCumule", TDotationCumule)
        report.SetParameterValue("TotRealisationCumule", TRealisationCumule.ToString)
        'report.SetParameterValue("TotEcartCumule", (TDotationfCumule - TRealisationCumule))
        report.SetParameterValue("TotPrevuProjet", TDotationProjet)
        report.SetParameterValue("TotRealisationProjet", TRealisationProjet)
        'report.SetParameterValue("TotEcartProjet", (TDotationProjet - TRealisationProjet))
        report.SetParameterValue("TotMontPrecedent", TMontantsPrecedents)
        report.SetParameterValue("PeriodeRSF", PeriodeRSF)
        report.SetParameterValue("Bailleur", EnleverApost(Bailleur))

        Dim NomRepertoire As String = DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString & " - " & Bailleur
        'Enlever les caractères bloqués dans la nomination du dossier
        NomRepertoire = NomRepertoire.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")
        NomRepertoire = line & "\RSF\" & NomRepertoire & "\"

        report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, NomRepertoire & "EmploiFondsActivite.pdf")

        RptViewerEmploisActivites.ReportSource = report
    End Sub

    Private Sub ChkRealisationPhysq_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkRealisationPhysq.CheckedChanged

        'DebutChargement(True, "Analyse des rapports d'activités de la période en cours...")

        'If (DossierFerme = False) Then

        '    If (ChkRealisationPhysq.Checked = True) Then

        '        CrystalReportViewer1.Enabled = True
        '        'Mise à jour dans la BD ******************************
        '       query= "Update T_RSF set RealisationPhysq='OUI', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
        '        ExecuteNonQuery(query)

        '        '**********************************************************************
        '        'Traitement pour l'affichage de l'état ********************************
        '        '**********************************************************************

        '       query= "DELETE from T_TampEtatAvancementPhysique"
        '        ExecuteNonQuery(query)

        '        Dim cpt As Decimal = 5
        '        Dim Tampon As String = ""

        '        Dim DatSet As New DataSet
        '        Dim DatTable As Data.DataTable
        '        Dim DatRow As DataRow
        '        Dim CmdBuilder As MySqlCommandBuilder
        '        Dim sqlconn As New MySqlConnection
        '        BDOPEN(sqlconn)

        '        query = "select Code,Description,TravauxEffectif,TravauxPrevu,CoutEffectif,CoutPrevu,CoutPrctEffectif,CoutTotInitial,DateFinInitiale,CoutTotRevise,DateFinRevise from T_RappAvancemActivite where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
        '        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        '        For Each rw As DataRow In dt.Rows

        '            cpt = cpt + 1
        '            Dim CodeActu As String = rw(0).ToString
        '            Dim Cmd As MySqlCommand
        '            If (Len(Tampon) = 5 And Len(CodeActu) = 1) Then
        '                DatSet = New DataSet
        '                query = "select * from T_TampEtatAvancementPhysique"

        '                Cmd = New MySqlCommand(query, sqlconn)
        '                DatAdapt100 = New MySqlDataAdapter(Cmd)
        '                DatAdapt100.Fill(DatSet, "T_TampEtatAvancementPhysique")
        '                DatTable = DatSet.Tables("T_TampEtatAvancementPhysique")
        '                DatRow = DatSet.Tables("T_TampEtatAvancementPhysique").NewRow()

        '                DatRow("Code") = Mid(Tampon, 1, 1) & "X"
        '                DatRow("Desciption") = "-------"

        '                DatSet.Tables("T_TampEtatAvancementPhysique").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
        '                CmdBuilder = New MySqlCommandBuilder(DatAdapt100) 'execution de l'enregistrement
        '                DatAdapt100.Update(DatSet, "T_TampEtatAvancementPhysique")
        '                DatSet.Clear()

        '            End If
        '            Tampon = CodeActu

        '            DatSet = New DataSet
        '            query = "select * from T_TampEtatAvancementPhysique"

        '            Cmd = New MySqlCommand(query, sqlconn)
        '            DatAdapt100 = New MySqlDataAdapter(Cmd)
        '            DatAdapt100.Fill(DatSet, "T_TampEtatAvancementPhysique")
        '            DatTable = DatSet.Tables("T_TampEtatAvancementPhysique")
        '            DatRow = DatSet.Tables("T_TampEtatAvancementPhysique").NewRow()

        '            DatRow("Code") = rw(0).ToString
        '            DatRow("Desciption") = rw(1).ToString
        '            DatRow("TravauxEffectif") = rw(2).ToString
        '            DatRow("TravauxPrevu") = rw(3).ToString
        '            DatRow("CoutEffectif") = AfficherMonnaie(rw(4).ToString)
        '            DatRow("CoutPrevu") = AfficherMonnaie(rw(5).ToString)
        '            DatRow("CoutPrctEffectif") = rw(6).ToString
        '            DatRow("CoutTotInitial") = AfficherMonnaie(rw(7).ToString)
        '            DatRow("DateFinInitiale") = rw(8).ToString
        '            DatRow("CoutTotRevise") = AfficherMonnaie(rw(9).ToString)
        '            DatRow("DateFinRevise") = rw(10).ToString

        '            DatSet.Tables("T_TampEtatAvancementPhysique").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
        '            CmdBuilder = New MySqlCommandBuilder(DatAdapt100) 'execution de l'enregistrement
        '            DatAdapt100.Update(DatSet, "T_TampEtatAvancementPhysique")
        '            DatSet.Clear()
        '        Next

        '        Dim LibelleProjet As String = ""
        '        query = "select NomProjet,PaysProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        '        Dim dt1 As Data.DataTable = ExcecuteSelectQuery(query)
        '        For Each rw1 As DataRow In dt1.Rows

        '            Dim LeGouv As String = "GOUVERNEMENT DE L'ETAT DE " & MettreApost(rw1(1).ToString.ToUpper)
        '            LibelleProjet = MettreApost(rw1(0).ToString.ToUpper)
        '            LibelleProjet = LeGouv & vbNewLine & LibelleProjet & vbNewLine & "RAPPORT SUR L'AVANCEMENT DES ACTIVITES"
        '            LibelleProjet = LibelleProjet & vbNewLine & "En date du " & DTFinPeriode.DateTime.ToShortDateString

        '        Next


        '        Dim Chemin As String = lineEtat & "\RSF\"
        '        Dim report As New Engine.ReportDocument
        '        Dim crtableLogoninfos As New TableLogOnInfos
        '        Dim crtableLogoninfo As New TableLogOnInfo
        '        Dim crConnectionInfo As New ConnectionInfo
        '        Dim CrTables As Engine.Tables
        '        Dim CrTable As Engine.Table

        '        DatSet = New DataSet
        '        report.Load(Chemin & "RapportAvancementPhysique.rpt")

        '        With crConnectionInfo
        '            .ServerName = ODBCNAME
        '            .DatabaseName = DB
        '            .UserID = USERNAME
        '            .Password = PWD
        '        End With

        '        CrTables = report.Database.Tables
        '        For Each CrTable In CrTables
        '            crtableLogoninfo = CrTable.LogOnInfo
        '            crtableLogoninfo.ConnectionInfo = crConnectionInfo
        '            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        '        Next

        '        report.SetDataSource(DatSet)
        '        report.SetParameterValue("EnteteRapport", LibelleProjet)

        '        Dim NomRepertoire As String = (DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString).Replace("/", "")
        '        NomRepertoire = line & "\RSF\" & NomRepertoire & "\"
        '        report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, NomRepertoire & "RealisationPhysique.pdf")

        '        CrystalReportViewer1.ReportSource = report
        '        'Fin traitement etat ******************************************************

        '        TabRealisationPhysique.PageVisible = True
        '        TabControl.SelectedTabPage = TabRealisationPhysique

        '    Else
        '        CrystalReportViewer1.Enabled = False
        '        'Mise à jour dans la BD ******************************
        '       query= "Update T_RSF set RealisationPhysq='NON', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
        '        ExecuteNonQuery(query)
        '        '********************************************************
        '        TabRealisationPhysique.PageVisible = False
        '    End If

        '    EnregistrerTabMat()
        'End If
        'VerifDossier()
        'FinChargement()

    End Sub

    Private Sub ChkRapportMarches_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkRapportMarches.CheckedChanged

        DebutChargement(True, "Chargement des étapes par type de marché en cours...")

        For k As Integer = 0 To 99
            EtapeConsult(k) = False
            EtapeFournit(k) = False
            EtapeTrvx(k) = False
        Next

        If (DossierFerme = False) Then

            If (ChkRapportMarches.Checked = True) Then
                CrystalReportViewer2.Enabled = True
                ChargerEtapes()
                'Mise à jour dans la BD ******************************
                query = "Update T_RSF set PassationMarches='OUI', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
                '********************************************************
                TabPassationMarche.PageVisible = True
                TabControl.SelectedTabPage = TabPassationMarche
            Else

                'Mise à jour dans la BD ******************************
                query = "Update T_RSF set PassationMarches='NON', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
                '********************************************************
                TabPassationMarche.PageVisible = False

            End If

            EnregistrerTabMat()
        End If
        VerifDossier()
        FinChargement()

    End Sub

    Private Sub ChargerEtapes()
        ChargerEtapesConsult()
        ChargerEtapesFournit()
        ChargerEtapesTravaux()
        ChargerEtapesServAss()

        If (PnlSommaire.Enabled = False) Then
            LancerEtatMarche()
        End If
    End Sub

    Private Sub ChargerEtapesServAss()
        nbEtpServAss = 0
        dtServAss.Columns.Clear()
        dtServAss.Columns.Add("Code", Type.GetType("System.String"))
        dtServAss.Columns.Add("Code Etape", Type.GetType("System.String"))
        dtServAss.Columns.Add("*", Type.GetType("System.Boolean"))
        dtServAss.Columns.Add("Etapes", Type.GetType("System.String"))

        Dim cptr As Decimal = 0
        dtServAss.Rows.Clear()
        query = "select RefEtape, TitreEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='Services autres que les services de consultants' order by NumeroOrdre"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            cptr += 1
            query = "select * from T_RSF_EtapeServAss where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "' and CodeEtape='" & rw(0).ToString & "'"
            Dim dt1 As Data.DataTable = ExcecuteSelectQuery(query)
            If dt1.Rows.Count = 1 Then
                EtapeServAss(cptr - 1) = True
            End If

            Dim drS = dtServAss.NewRow()
            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = EtapeServAss(cptr - 1)
            drS(3) = MettreApost(rw(1).ToString)

            If (EtapeServAss(cptr - 1) = True) Then
                nbEtpServAss += 1
            End If

            dtServAss.Rows.Add(drS)

        Next
        GridServAss.DataSource = dtServAss
        ViewServAss.Columns(0).Visible = False
        ViewServAss.Columns(1).Visible = False
        ViewServAss.Columns(2).Width = 20
        ViewServAss.Columns(3).Width = 250
        ViewServAss.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewServAss.Appearance.Row.Font = New Drawing.Font("Times New Roman", 9, FontStyle.Regular)
        ViewServAss.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ColorRowGrid(ViewServAss, "[Code]='x'", Color.LightGray, "Times New Roman", 9, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewServAss, "[*]=true", Color.LightGray, "Times New Roman", 9, FontStyle.Bold, Color.Black, False)

        If (ViewServAss.RowCount > 0) Then
            GbServAss.Visible = True
        Else
            GbServAss.Visible = False
        End If

    End Sub

    Private Sub ChargerEtapesConsult()
        nbEtpCons = 0
        dtConsult.Columns.Clear()
        dtConsult.Columns.Add("Code", Type.GetType("System.String"))
        dtConsult.Columns.Add("Code Etape", Type.GetType("System.String"))
        dtConsult.Columns.Add("*", Type.GetType("System.Boolean"))
        dtConsult.Columns.Add("Etapes", Type.GetType("System.String"))
        dtConsult.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select RefEtape, TitreEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='Consultants' order by NumeroOrdre"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1

            query = "select * from T_RSF_EtapeConsultants where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "' and CodeEtape='" & rw(0).ToString & "'"
            Dim dt1 As Data.DataTable = ExcecuteSelectQuery(query)
            If dt1.Rows.Count = 1 Then
                EtapeConsult(cptr - 1) = True
            End If

            Dim drS = dtConsult.NewRow()
            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = EtapeConsult(cptr - 1)
            drS(3) = MettreApost(rw(1).ToString)

            If (EtapeConsult(cptr - 1) = True) Then
                nbEtpCons += 1
            End If

            dtConsult.Rows.Add(drS)

        Next


        GridConsult.DataSource = dtConsult
        ViewConsult.Columns(0).Visible = False
        ViewConsult.Columns(1).Visible = False
        ViewConsult.Columns(2).Width = 20
        ViewConsult.Columns(3).Width = 250
        ViewConsult.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewConsult.Appearance.Row.Font = New Drawing.Font("Times New Roman", 9, FontStyle.Regular)
        ViewConsult.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ColorRowGrid(ViewConsult, "[Code]='x'", Color.LightGray, "Times New Roman", 9, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewConsult, "[*]=true", Color.LightGray, "Times New Roman", 9, FontStyle.Bold, Color.Black, False)

        If (ViewConsult.RowCount > 0) Then
            GbConsult.Visible = True
        Else
            GbConsult.Visible = False
        End If

    End Sub

    Private Sub ChargerEtapesFournit()

        nbEtpFour = 0
        dtFournit.Columns.Clear()
        dtFournit.Columns.Add("Code", Type.GetType("System.String"))
        dtFournit.Columns.Add("Code Etape", Type.GetType("System.String"))
        dtFournit.Columns.Add("*", Type.GetType("System.Boolean"))
        dtFournit.Columns.Add("Etapes", Type.GetType("System.String"))
        dtFournit.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select RefEtape, TitreEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='Fournitures' order by NumeroOrdre"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            cptr += 1
            query = "select * from T_RSF_EtapeFournitures where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "' and CodeEtape='" & rw(0).ToString & "'"
            Dim dt1 As Data.DataTable = ExcecuteSelectQuery(query)
            If dt1.Rows.Count = 1 Then
                EtapeFournit(cptr - 1) = True
            End If

            Dim drS = dtFournit.NewRow()
            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = EtapeFournit(cptr - 1)
            drS(3) = MettreApost(rw(1).ToString)

            If (EtapeFournit(cptr - 1) = True) Then
                nbEtpFour += 1
            End If

            dtFournit.Rows.Add(drS)
        Next


        GridFournit.DataSource = dtFournit
        ViewFournit.Columns(0).Visible = False
        ViewFournit.Columns(1).Visible = False
        ViewFournit.Columns(2).Width = 20
        ViewFournit.Columns(3).Width = 250
        ViewFournit.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewFournit.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewFournit.Appearance.Row.Font = New Drawing.Font("Times New Roman", 9, FontStyle.Regular)
        ColorRowGrid(ViewFournit, "[Code]='x'", Color.LightGray, "Times New Roman", 9, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewFournit, "[*]=true", Color.LightGray, "Times New Roman", 9, FontStyle.Bold, Color.Black, False)

        If (ViewFournit.RowCount > 0) Then
            GbFournit.Visible = True
        Else
            GbFournit.Visible = False
        End If

    End Sub

    Private Sub ChargerEtapesTravaux()

        nbEtpTrvx = 0
        dtTravaux.Columns.Clear()
        dtTravaux.Columns.Add("Code", Type.GetType("System.String"))
        dtTravaux.Columns.Add("Code Etape", Type.GetType("System.String"))
        dtTravaux.Columns.Add("*", Type.GetType("System.Boolean"))
        dtTravaux.Columns.Add("Etapes", Type.GetType("System.String"))
        dtTravaux.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select RefEtape, TitreEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='Travaux' order by NumeroOrdre"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            cptr += 1
            query = "select * from T_RSF_EtapeTravaux where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "' and CodeEtape='" & rw(0).ToString & "'"
            Dim dt1 As Data.DataTable = ExcecuteSelectQuery(query)
            If dt1.Rows.Count > 0 Then
                EtapeTrvx(cptr - 1) = True
            End If

            Dim drS = dtTravaux.NewRow()
            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = rw(0).ToString
            drS(2) = EtapeTrvx(cptr - 1)
            drS(3) = MettreApost(rw(1).ToString)

            If (EtapeTrvx(cptr - 1) = True) Then
                nbEtpTrvx += 1
            End If

            dtTravaux.Rows.Add(drS)
        Next


        GridTravaux.DataSource = dtTravaux
        ViewTravaux.Columns(0).Visible = False
        ViewTravaux.Columns(1).Visible = False
        ViewTravaux.Columns(2).Width = 20
        ViewTravaux.Columns(3).Width = 250
        ViewTravaux.Columns(2).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewTravaux.Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ViewTravaux.Appearance.Row.Font = New Drawing.Font("Times New Roman", 9, FontStyle.Regular)
        ColorRowGrid(ViewTravaux, "[Code]='x'", Color.LightGray, "Times New Roman", 9, FontStyle.Regular, Color.Black)
        ColorRowGridAnal(ViewTravaux, "[*]=true", Color.LightGray, "Times New Roman", 9, FontStyle.Bold, Color.Black, False)

        If (ViewTravaux.RowCount > 0) Then
            GbTravaux.Visible = True
        Else
            GbTravaux.Visible = False
        End If

    End Sub

    Private Sub GridServAss_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridServAss.Click

        If (ViewServAss.RowCount > 0) Then
            DrX = ViewServAss.GetDataRow(ViewServAss.FocusedRowHandle)
            Dim posLg As Decimal = ViewServAss.FocusedRowHandle

            If (EtapeServAss(ViewServAss.FocusedRowHandle) = False And nbEtpServAss >= 10) Then
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                MsgBox("Vous avez atteint 10 étapes.", MsgBoxStyle.Information)
            Else
                EtapeServAss(ViewServAss.FocusedRowHandle) = Not (EtapeServAss(ViewServAss.FocusedRowHandle))
                ChargerEtapesServAss()
                ViewServAss.MakeRowVisible(posLg)
            End If

        End If

    End Sub

    Private Sub GridConsult_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridConsult.Click

        If (ViewConsult.RowCount > 0) Then
            DrX = ViewConsult.GetDataRow(ViewConsult.FocusedRowHandle)
            Dim posLg As Decimal = ViewConsult.FocusedRowHandle

            If (EtapeConsult(ViewConsult.FocusedRowHandle) = False And nbEtpCons >= 10) Then
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                MsgBox("Vous avez atteint 10 étapes.", MsgBoxStyle.Information)
            Else
                EtapeConsult(ViewConsult.FocusedRowHandle) = Not (EtapeConsult(ViewConsult.FocusedRowHandle))
                ChargerEtapesConsult()
                ViewConsult.MakeRowVisible(posLg)
            End If
        End If

    End Sub

    Private Sub GridFournit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridFournit.Click

        If (ViewFournit.RowCount > 0) Then
            DrX = ViewFournit.GetDataRow(ViewFournit.FocusedRowHandle)
            Dim posLg As Decimal = ViewFournit.FocusedRowHandle

            If (EtapeFournit(ViewFournit.FocusedRowHandle) = False And nbEtpFour >= 10) Then
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                MsgBox("Vous avez atteint 10 étapes.", MsgBoxStyle.Information)
            Else
                EtapeFournit(ViewFournit.FocusedRowHandle) = Not (EtapeFournit(ViewFournit.FocusedRowHandle))
                ChargerEtapesFournit()
                ViewFournit.MakeRowVisible(posLg)
            End If

        End If

    End Sub

    Private Sub GridTravaux_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridTravaux.Click

        If (ViewTravaux.RowCount > 0) Then
            DrX = ViewTravaux.GetDataRow(ViewTravaux.FocusedRowHandle)
            Dim posLg As Decimal = ViewTravaux.FocusedRowHandle

            If (EtapeTrvx(ViewTravaux.FocusedRowHandle) = False And nbEtpTrvx >= 10) Then
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                MsgBox("Vous avez atteint 10 étapes.", MsgBoxStyle.Information)
            Else
                EtapeTrvx(ViewTravaux.FocusedRowHandle) = Not (EtapeTrvx(ViewTravaux.FocusedRowHandle))
                ChargerEtapesTravaux()
                ViewTravaux.MakeRowVisible(posLg)
            End If

        End If

    End Sub

    Private Sub BtAfficheEtatMarche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAfficheEtatMarche.Click
        LancerEtatMarche()
    End Sub

    Private Sub LancerEtatMarche()
        DebutChargement(True, "Traitement du Plan de Passation de Marchés en cours...")
        TraitementEtatMarche()
        FinChargement()
    End Sub

    Private Sub TraitementEtatMarche()

        'Enregistrement des marchés de fournitures **********************************************************************
        query = "DELETE  from T_TampEtatMarche"    ' where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        query = "DELETE  from T_TampEtatEtape"
        ExecuteNonQuery(query)

        Dim tampMarche() As String = {"T_TampMarcheFournitures", "T_TampMarcheTravaux", "T_TampMarcheConsultants", "T_TampMarcheServAss"}
        Dim typeMarche() As String = {"Fournitures", "Travaux", "Consultants", "Services autres que les services de consultants"}
        Dim gridCible() As DevExpress.XtraGrid.Views.Grid.GridView = {ViewFournit, ViewTravaux, ViewConsult, ViewServAss}
        Dim tableEtape() As String = {"T_RSF_EtapeFournitures", "T_RSF_EtapeTravaux", "T_RSF_EtapeConsultants", "T_RSF_EtapeServAss"}

        For n As Decimal = 0 To 3

            'Dim Cmdsup As MySqlCommand = sqlconn.CreateCommand
            query = "DELETE  from " & tampMarche(n)
            ExecuteNonQuery(query)

            'Recherche des marchés ********************************************************************
            Dim LeMarche(5000) As String
            Dim nbMarche As Decimal = 0
            query = "select RefMarche from T_Marche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='" & typeMarche(n) & "'"
            Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                LeMarche(nbMarche) = rw(0).ToString
                nbMarche += 1
            Next

            For i As Integer = 0 To nbMarche - 1

                'Recherche du fournisseur et de son montant proposé *******************************
                Dim NumDoss As String = ""
                Dim DescMarche As String = ""
                Dim montEstim As String = ""
                Dim methodMarche As String = ""
                Dim CodLot As String = ""
                Dim RefDuLot As String = ""
                Dim CodFournis As Decimal = 0
                Dim PrixFournis As String = ""
                Dim NomCompFournis As String = ""

                query = "select NumeroDAO,CodeLot,DescriptionMarche,MontantEstimatif,MethodeMarche from T_Marche where RefMarche='" & LeMarche(i) & "'"
                Dim dt1 As Data.DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    NumDoss = rw1(0).ToString
                    CodLot = rw1(1).ToString
                    DescMarche = rw1(2).ToString
                    montEstim = rw1(3).ToString
                    methodMarche = rw1(4).ToString
                Next

                If (n <> 2) Then

                    query = "select F.NomFournis,S.PrixCorrigeOffre,S.CodeLot from T_SoumissionFournisseur as S, T_Fournisseur as F where S.CodeFournis=F.CodeFournis and F.NumeroDAO='" & NumDoss & "' and F.CodeProjet='" & ProjetEnCours & "' and S.Attribue='OUI' order by S.CodeLot"
                    Dim dt2 As Data.DataTable = ExcecuteSelectQuery(query)
                    For Each rw2 As DataRow In dt2.Rows
                        If (NomCompFournis <> "") Then
                            NomCompFournis = NomCompFournis & "; "
                            PrixFournis = PrixFournis & "; "
                        End If
                        NomCompFournis = NomCompFournis & "-(L" & rw2(2).ToString & ") " & rw2(0).ToString
                        PrixFournis = PrixFournis & "-(L" & rw2(2).ToString & ") " & rw2(1).ToString
                    Next

                Else

                    query = "select S.MontantPropose,C.NomConsult from T_SoumissionConsultant as S, T_Consultant as C, T_Marche as M where S.RefConsult=C.RefConsult and C.NumeroDp=M.NumeroDAO and S.Attribue='OUI' and M.RefMarche='" & LeMarche(i) & "'"
                    Dim dt3 As Data.DataTable = ExcecuteSelectQuery(query)
                    For Each rw3 As DataRow In dt3.Rows
                        NomCompFournis = rw3(1).ToString
                        PrixFournis = rw3(0)
                    Next

                End If

                '*****************************************************************************************************
                'Enregistrement dans la BD *******************************************************

                Dim DatSet = New DataSet
                query = "select * from T_TampEtatMarche"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_TampEtatMarche")
                Dim DatTable = DatSet.Tables("T_TampEtatMarche")
                Dim DatRow = DatSet.Tables("T_TampEtatMarche").NewRow()

                DatRow("RefMarche") = LeMarche(i)
                DatRow("PeriodeRSF") = DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString
                DatRow("DescriptionMarche") = DescMarche
                DatRow("MontantEstimatif") = AfficherMonnaie(montEstim)
                DatRow("MethodeMarche") = methodMarche
                DatRow("TypeMarche") = typeMarche(n)
                DatRow("Fournisseur") = NomCompFournis
                DatRow("MontantFournisseur") = AfficherMonnaie(PrixFournis)
                DatRow("CodeProjet") = ProjetEnCours

                DatSet.Tables("T_TampEtatMarche").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
                DatAdapt.Update(DatSet, "T_TampEtatMarche")
                DatSet.Clear()

                DatSet = New DataSet
                query = "select * from " & tampMarche(n)

                Cmd = New MySqlCommand(query, sqlconn)
                DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, tampMarche(n))
                DatTable = DatSet.Tables(tampMarche(n))
                DatRow = DatSet.Tables(tampMarche(n)).NewRow()

                DatRow("RefMarche") = LeMarche(i)
                DatRow("DescriptionMarche") = DescMarche
                DatRow("MontantEstimatif") = AfficherMonnaie(montEstim)
                DatRow("MethodeMarche") = methodMarche
                DatRow("Fournisseur") = NomCompFournis
                DatRow("MontantFournisseur") = AfficherMonnaie(PrixFournis)

                DatSet.Tables(tampMarche(n)).Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
                CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
                DatAdapt.Update(DatSet, tampMarche(n))
                DatSet.Clear()

                '***********************************************************************************

                'Rechercher et ajouter les étapes ici ***********************************************
                Dim nbEtp As Decimal = 0

                'If (PnlSommaire.Enabled = True) Then
                For k As Integer = 0 To gridCible(n).RowCount - 1

                    DrX = gridCible(n).GetDataRow(k)
                    If (DrX(2) = True) Then
                        nbEtp += 1

                        Dim LaRefEtape As String = ""
                        Dim LeTitreEtape As String = ""
                        Dim LaDatePrevue As String = ""
                        Dim LaDateEffective As String = ""

                        query = "select P.DebutPrevu,P.DebutEffectif,E.TitreEtape from T_PlanMarche as P,T_EtapeMarche as E where P.RefEtape=E.RefEtape and P.RefMarche='" & LeMarche(i) & "' and E.RefEtape='" & DrX(1).ToString & "' order by P.NumeroOrdre"
                        Dim dt4 As Data.DataTable = ExcecuteSelectQuery(query)
                        For Each rw4 As DataRow In dt4.Rows
                            LaRefEtape = DrX(1).ToString
                            LeTitreEtape = rw4(2).ToString
                            LaDatePrevue = rw4(0).ToString
                            LaDateEffective = rw4(1).ToString
                        Next

                        DatSet = New DataSet
                        query = "select * from T_TampEtatEtape"

                        Cmd = New MySqlCommand(query, sqlconn)
                        DatAdapt = New MySqlDataAdapter(Cmd)
                        DatAdapt.Fill(DatSet, "T_TampEtatEtape")
                        DatTable = DatSet.Tables("T_TampEtatEtape")
                        DatRow = DatSet.Tables("T_TampEtatEtape").NewRow()

                        DatRow("RefEtape") = DrX(1).ToString
                        DatRow("RefMarche") = LeMarche(i)
                        DatRow("NumeroOrdre") = (k + 1).ToString
                        DatRow("TitreEtape") = LeTitreEtape
                        DatRow("DatePrevue") = LaDatePrevue
                        DatRow("DateEffective") = LaDateEffective

                        DatSet.Tables("T_TampEtatEtape").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
                        CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
                        DatAdapt.Update(DatSet, "T_TampEtatEtape")
                        DatSet.Clear()


                        'Enregistrement des étapes du RSF ***********************************************************
                        If (PnlSommaire.Enabled = True And i = 0) Then
                            DatSet = New DataSet
                            query = "select * from " & tableEtape(n)

                            Cmd = New MySqlCommand(query, sqlconn)
                            DatAdapt = New MySqlDataAdapter(Cmd)
                            DatAdapt.Fill(DatSet, tableEtape(n))
                            DatTable = DatSet.Tables(tableEtape(n))
                            DatRow = DatSet.Tables(tableEtape(n)).NewRow()

                            DatRow("PeriodeRSF") = DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString
                            DatRow("CodeProjet") = ProjetEnCours
                            DatRow("CodeEtape") = DrX(1).ToString
                            DatRow("OrdreEtape") = (k + 1).ToString

                            DatSet.Tables(tableEtape(n)).Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
                            CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
                            DatAdapt.Update(DatSet, tableEtape(n))
                            DatSet.Clear()

                        End If
                        '********************************************************************************************

                        'dim DatSet = New DataSet
                        'query = "select * from " & tampMarche(n) & " where RefMarche='" & LeMarche(i) & "'"
                        'Cmd = New MySqlCommand(query, sqlconn)
                        'Dim DatAdapt = New MySqlDataAdapter(Cmd)
                        'Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                        'DatAdapt.Fill(DatSet, tampMarche(n))

                        'DatSet.Tables.Item(tampMarche(n)).Rows(0).Item("DatePrevue" & nbEtp.ToString) = LaDatePrevue
                        'DatSet.Tables.Item(tampMarche(n)).Rows(0).Item("DateEffective" & nbEtp.ToString) = LaDateEffective

                        'DatAdapt.Update(DatSet, tampMarche(n))
                        'DatSet.Clear()

                        'sql4 = "Update " & tampMarche(n) & " set DatePrevue" & nbEtp.ToString & " = '" + LaDatePrevue + "', DateEffective" & nbEtp.ToString & " = '" + LaDateEffective + "' where RefMarche='" & LeMarche(i) & "'"""
                        'ExecuteNonQuery(query)

                    End If

                Next
                BDQUIT(sqlconn)
            Next

        Next


        'Mise à jour date modif dans T_RSF ********************************
        query = "Update T_RSF set Operateur='" + CodeUtilisateur + "', DateModif='" + Now.ToShortDateString & " " & Now.ToLongTimeString + "' where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        '******************************************************************

        AfficherEtatMarches()


    End Sub

    Private Sub AfficherEtatMarches()

        Dim LesTitreEtape(10) As String
        Dim LesTitreEtapeT(10) As String
        Dim LesTitreEtapeC(10) As String
        Dim LesTitreEtapeS(10) As String
        For i As Integer = 0 To 9
            LesTitreEtape(i) = ""
            LesTitreEtapeT(i) = ""
            LesTitreEtapeC(i) = ""
            LesTitreEtapeS(i) = ""
        Next
        Dim LeNombre As Decimal = 0
        query = "select NumeroOrdre,TitreEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='Fournitures' order by NumeroOrdre"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            DrX = ViewFournit.GetDataRow(CInt(rw(0)) - 1)
            If (DrX(2) = True) Then
                LesTitreEtape(LeNombre) = MettreApost(rw(1).ToString)
                LeNombre = LeNombre + 1
            End If
        Next

        LeNombre = 0
        query = "select NumeroOrdre,TitreEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='Travaux' order by NumeroOrdre"
        Dim dt1 As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows
            DrX = ViewTravaux.GetDataRow(CInt(rw1(0)) - 1)
            If (DrX(2) = True) Then
                LesTitreEtapeT(LeNombre) = MettreApost(rw1(1).ToString)
                LeNombre = LeNombre + 1
            End If
        Next

        LeNombre = 0
        query = "select NumeroOrdre,TitreEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='Consultants' order by NumeroOrdre"
        Dim dt2 As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw2 As DataRow In dt2.Rows
            DrX = ViewConsult.GetDataRow(CInt(rw2(0)) - 1)
            If (DrX(2) = True) Then
                LesTitreEtapeC(LeNombre) = MettreApost(rw2(1).ToString)
                LeNombre = LeNombre + 1
            End If
        Next

        LeNombre = 0
        query = "select NumeroOrdre,TitreEtape from T_EtapeMarche where CodeProjet='" & ProjetEnCours & "' and TypeMarche='Services autres que les services de consultants' order by NumeroOrdre"
        Dim dt3 As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw3 As DataRow In dt3.Rows

            DrX = ViewServAss.GetDataRow(CInt(rw3(0)) - 1)
            If (DrX(2) = True) Then
                LesTitreEtapeS(LeNombre) = MettreApost(rw3(1).ToString)
                LeNombre = LeNombre + 1
            End If

        Next

        Dim LibelleProjet As String = ""
        query = "select NomProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt4 As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw4 As DataRow In dt4.Rows
            LibelleProjet = MettreApost(rw4(0).ToString.ToUpper)
        Next

        LibelleProjet = LibelleProjet & vbNewLine
        LibelleProjet = LibelleProjet & "Tableaux de suivi de la passation des marchés" & vbNewLine
        LibelleProjet = LibelleProjet & "en date du " & DTFinPeriode.DateTime.ToShortDateString

        Dim Chemin As String = lineEtat & "\RSF\"
        Dim report As New Engine.ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Engine.Tables
        Dim CrTable As Engine.Table

        Dim DatSet = New DataSet
        report.Load(Chemin & "RapportMarches9.rpt")

        With crConnectionInfo
            .ServerName = ODBCNAME
            .DatabaseName = DB
            .UserID = USERNAME
            .Password = PWD
        End With

        CrTables = report.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        report.SetParameterValue("TitreEtat", LibelleProjet)

        For t As Decimal = 0 To 9
            report.SetParameterValue("Etape" & (t + 1).ToString, LesTitreEtape(t))
            report.SetParameterValue("Etape" & (t + 1).ToString & "T", LesTitreEtapeT(t))
            report.SetParameterValue("Etape" & (t + 1).ToString & "C", LesTitreEtapeC(t))
            report.SetParameterValue("Etape" & (t + 1).ToString & "S", LesTitreEtapeS(t))
        Next
        Dim Bailleur As String = "Tous les bailleurs"
        If cmbBailleur.SelectedIndex >= 0 Then
            Bailleur = cmbBailleur.Text
        End If
        Dim NomRepertoire As String = DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString & " - " & Bailleur
        'Enlever les caractères bloqués dans la nomination du dossier
        NomRepertoire = NomRepertoire.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")
        NomRepertoire = line & "\RSF\" & NomRepertoire & "\"
        report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, NomRepertoire & "RapportMarches.pdf")
        CrystalReportViewer2.ReportSource = report
    End Sub

    Private Sub BtSelectPJ_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles BtSelectPJ.LinkClicked
        Dim dlg As New OpenFileDialog()
        dlg.Filter = "Tout|*.pdf;*.jpg;*.jpeg;*.png;*.gif;*.bmp|Documents|*.pdf|Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
        If (dlg.ShowDialog() = DialogResult.OK) Then

            Dim fichier As String = dlg.FileName
            Dim NomComp As String() = fichier.Split("\"c)

            Dim NomCourt As String = NomComp(NomComp.Length - 1)

            If File.Exists(NomDossier & "\" & NomComp(NomComp.Length - 1)) Then
                File.Delete(NomDossier & "\" & NomComp(NomComp.Length - 1))
            End If

            If (File.Exists(NomDossier & "\" & NomComp(NomComp.Length - 1)) = False) Then

                File.Copy(fichier, NomDossier & "\" & NomComp(NomComp.Length - 1), True)

                Dim NomRepertoire As String = DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString & " - " & Bailleur
                'Enlever les caractères bloqués dans la nomination du dossier
                Dim FileName As String = NomDossier.Replace("\", "/") & "/" & NomComp(NomComp.Length - 1)
                Dim PeriodeRSf As String = DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString
                query = "SELECT COUNT(*) FROM T_PieceJointeRSF WHERE RefPiece='" & FileName & "' AND PeriodeRSF='" & PeriodeRSf & "' AND Bailleur='" & Bailleur & "'"
                If Val(ExecuteScallar(query)) > 0 Then
                    SuccesMsg("Le fichier existe déjà")
                    Exit Sub
                End If

                query = "INSERT INTO T_PieceJointeRSF VALUES('" & EnleverApost(FileName) & "','" & PeriodeRSf & "','" & EnleverApost(Bailleur) & "','" & EnleverApost(NomComp(NomComp.Length - 1)) & "','" & Now.ToShortDateString & " " & Now.ToLongTimeString & "','" & ProjetEnCours & "')"
                ExecuteNonQuery(query)
                ChargerPJ(PeriodeRSf, Bailleur)
            Else
                SuccesMsg("Ce fichier existe déjà dans ce rapport.")
            End If

        End If

    End Sub

    Private Sub ChargerPJ(ByVal Periode As String, Bailleur As String)

        Dim dtPiece = New Data.DataTable()
        dtPiece.Columns.Clear()

        dtPiece.Columns.Add("Code", Type.GetType("System.String"))
        dtPiece.Columns.Add("Nom du fichier", Type.GetType("System.String"))
        dtPiece.Columns.Add("Chemin", Type.GetType("System.String"))
        dtPiece.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select RefPiece,NomPiece from T_PieceJointeRSF where PeriodeRSF='" & Periode & "' AND Bailleur='" & EnleverApost(Bailleur) & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            cptr += 1
            Dim drS = dtPiece.NewRow()

            drS(0) = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS(1) = MettreApost(rw(1).ToString)
            drS(2) = MettreApost(rw(0).ToString)

            dtPiece.Rows.Add(drS)

        Next

        GridPieceJ.DataSource = dtPiece
        ViewPieceJ.Columns(0).Visible = False
        ViewPieceJ.Columns(1).Width = GridPieceJ.Width - 18
        ViewPieceJ.Columns(2).Visible = False
        ViewPieceJ.Appearance.Row.Font = New Drawing.Font("Times New Roman", 10, FontStyle.Regular)
        ColorRowGrid(ViewPieceJ, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)

        'Mise à jour date modif dans T_RSF ********************************
        query = "update T_RSF set PieceJointe='" + IIf(ViewPieceJ.RowCount > 0, "OUI", "NON").ToString + "' where PeriodeRSF='" & DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)

        '******************************************************************

    End Sub

    Private Sub ApercuPJ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ApercuPJ.Click

        If (ViewPieceJ.RowCount > 0) Then
            DrX = ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle)
            If File.Exists(DrX(2).ToString.Replace("/", "\")) Then
                Process.Start(DrX(2).ToString.Replace("/", "\"))
            Else
                FailMsg("Le fichier spécifié n'existe pas.")
            End If
        End If

    End Sub

    Private Sub ImprimerPJ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImprimerPJ.Click

        If (ViewPieceJ.RowCount > 0) Then
            DrX = ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle)
            Dim monProcess As New Process()
            monProcess.StartInfo.FileName = DrX(2).ToString
            monProcess.StartInfo.Verb = "Print"
            monProcess.StartInfo.CreateNoWindow = True
            Try
                If File.Exists(DrX(2).ToString) Then
                    monProcess.Start()
                Else
                    FailMsg("Le fichier spécifié n'existe pas.")
                End If
            Catch ex As Exception
                monProcess = New Process()
                monProcess.StartInfo.FileName = DrX(2).ToString
                If File.Exists(DrX(2).ToString) Then
                    monProcess.Start()
                Else
                    FailMsg("Le fichier spécifié n'existe pas.")
                End If
            End Try
        End If

    End Sub

    Private Sub SupprimerPJ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SupprimerPJ.Click

        If (ViewPieceJ.RowCount > 0) Then
            Dim DrX = ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle)
            If ConfirmMsg("Confirmez-vous la suppression de " & DrX("Nom du fichier")) = DialogResult.Yes Then
                Dim PeriodeRSF As String = DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString
                Dim NomRepertoire As String = DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString & " - " & Bailleur
                'Enlever les caractères bloqués dans la nomination du dossier
                NomRepertoire = NomRepertoire.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")
                Try
                    If File.Exists(DrX(2).ToString.Replace("/", "\")) Then
                        File.Delete(DrX(2).ToString.Replace("/", "\"))
                    End If
                Catch ex As Exception
                    FailMsg("Impossible de supprimer la pièce jointe car elle est en cours d'utilisation.")
                    Exit Sub
                End Try
                query = "DELETE from T_PieceJointeRSF where RefPiece='" & EnleverApost(DrX(2).ToString) & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & PeriodeRSF & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
                ChargerPJ(PeriodeRSF, Bailleur)
            End If
        End If

    End Sub

    Private Sub OuvrirAncRapport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OuvrirAncRapport.Click

        Dim periode As String = ""
        If (ViewRapPrec.RowCount > 0) Then
            MiseAneuf()
            DrX = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)
            periode = DrX(1).ToString


            Dim Chk1 As String = "NON"
            Dim Chk2 As String = "NON"
            Dim Chk3 As String = "NON"
            Dim Chk4 As String = "NON"
            Dim Chk5 As String = "NON"
            Dim Chk6 As String = "NON"
            Dim nomFichier As String = ""

            query = "select ExamAvanceProjet,TabEmploiRessources,EmploiFondsActivite,RealisationPhysq,PassationMarches,PieceJointe,Bailleur from T_RSF where PeriodeRSF='" & DrX(1).ToString & "' AND Bailleur='" & EnleverApost(DrX("Bailleur")) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows

                DossierFerme = False
                Bailleur = DrX("Bailleur")
                cmbBailleur.Text = Bailleur
                DTDebPeriode.DateTime = CDate(Mid(DrX(1).ToString, 1, 10))
                DTDebPeriode.Enabled = False
                DTFinPeriode.DateTime = CDate(Mid(DrX(1).ToString, 14, 10))
                DTFinPeriode.Enabled = False
                cmbBailleur.Enabled = False
                BtOuvrirRSF.Text = "Dossier " & DrX(1).ToString
                Dim dossierrsf As String

                dossierrsf = DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString & " - " & MettreApost(rw("Bailleur"))
                'Enlever les caractères bloqués dans la nomination du dossier
                dossierrsf = dossierrsf.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")

                nomFichier = rw(0).ToString
                If (nomFichier <> "NON" And nomFichier <> "") Then
                    Try
                        RTRapportProjet.LoadDocument(line & "\RSF\" & dossierrsf & "\" & nomFichier, DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                        RTRapportProjet.ReadOnly = True
                        Chk1 = "OUI"
                    Catch ex As Exception
                        'FailMsg("Impossible d'ouvrir le fichier de l'examen d'avancement." & vbNewLine & "Le fichier est introuvable")
                    End Try
                End If

                Chk2 = IIf(rw(1).ToString = "OUI", "OUI", "NON").ToString
                Chk3 = IIf(rw(2).ToString = "OUI", "OUI", "NON").ToString
                Chk4 = IIf(rw(3).ToString = "OUI", "OUI", "NON").ToString
                Chk5 = IIf(rw(4).ToString = "OUI", "OUI", "NON").ToString
                Chk6 = IIf(rw(5).ToString = "OUI", "OUI", "NON").ToString
            Next

            If (Chk1 = "OUI") Then ChkAvanceProjet.Checked = True
            If (Chk2 = "OUI") Then ChkTableauFonds.Checked = True
            If (Chk3 = "OUI") Then ChkEmploiFondsActivite.Checked = True
            If (Chk4 = "OUI") Then ChkRealisationPhysq.Checked = True
            If (Chk5 = "OUI") Then
                ChkRapportMarches.Checked = True
                GbEtapes.Enabled = False
                SplitContainerControl2.Collapsed = True
            End If
            If (Chk6 = "OUI") Then
                ChkPiecesJointes.Checked = True
                ChargerPJ(periode, DrX("Bailleur"))
            End If

        End If

    End Sub

    Private Sub GridRapPrec_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridRapPrec.Click

        'If (ViewRapPrec.RowCount > 0) Then
        '    DrX = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)

        '    ChargerPJ(DrX(1).ToString, EnleverApost(DrX("Bailleur")))
        'End If

    End Sub

    Private Sub PnlSommaire_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PnlSommaire.EnabledChanged

        If (PnlSommaire.Enabled = False) Then
            GbEtapes.Enabled = False
        Else
            GbEtapes.Enabled = True
        End If

    End Sub

    Private Sub ChkPiecesJointes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPiecesJointes.CheckedChanged
        If (ChkPiecesJointes.Checked = True And PnlSommaire.Enabled = True) Then
            BtSelectPJ.Enabled = True
            Dim periode As String = DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString
            ChargerPJ(periode, Bailleur)
            query = "UPDATE t_rsf SET PieceJointe='OUI' WHERE PeriodeRSF='" & periode & "' AND Bailleur='" & EnleverApost(Bailleur) & "'"
            ExecuteNonQuery(query)
        Else
            BtSelectPJ.Enabled = False
            Dim periode As String = DTDebPeriode.DateTime.ToShortDateString & " - " & DTFinPeriode.DateTime.ToShortDateString
            query = "UPDATE t_rsf SET PieceJointe='NON' WHERE PeriodeRSF='" & periode & "' AND Bailleur='" & EnleverApost(Bailleur) & "'"
            ExecuteNonQuery(query)
        End If
        VerifDossier()
        EnregistrerTabMat()

    End Sub

    Private Sub BtImpRSF_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles BtImpRSF.LinkClicked
        Dim NomRepertoire As String = (DTDebPeriode.DateTime.ToShortDateString & "_" & DTFinPeriode.DateTime.ToShortDateString) & " - " & Bailleur
        'Enlever les caractères bloqués dans la nomination du dossier
        NomRepertoire = NomRepertoire.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")
        NomRepertoire = line & "\RSF\" & NomRepertoire & "\"

        If (ChkPiecesJointes.Checked = True) Then
            For k As Integer = 0 To ViewPieceJ.RowCount - 1
                DrX = ViewPieceJ.GetDataRow(k)
                Dim monProcess As New Process()
                monProcess.StartInfo.FileName = DrX(2).ToString
                monProcess.StartInfo.Verb = "Print"
                monProcess.StartInfo.CreateNoWindow = True
                Try
                    If File.Exists(DrX(2).ToString) Then
                        monProcess.Start()
                    End If
                Catch ex As Exception
                    monProcess = New Process()
                    monProcess.StartInfo.FileName = DrX(2).ToString
                    If File.Exists(DrX(2).ToString) Then
                        monProcess.Start()
                    End If
                End Try
            Next
        End If

        If (ChkRapportMarches.Checked = True) Then
            Dim monProcess As New Process()
            monProcess.StartInfo.FileName = NomRepertoire & "RapportMarches.pdf"
            monProcess.StartInfo.Verb = "Print"
            monProcess.StartInfo.CreateNoWindow = True
            Try
                monProcess.Start()
            Catch ex As Exception
                monProcess = New Process()
                monProcess.StartInfo.FileName = NomRepertoire & "RapportMarches.pdf"
                monProcess.Start()
            End Try
        End If

        If (ChkRealisationPhysq.Checked = True) Then
            Dim monProcess As New Process()
            monProcess.StartInfo.FileName = NomRepertoire & "RealisationPhysique.pdf"
            monProcess.StartInfo.Verb = "Print"
            monProcess.StartInfo.CreateNoWindow = True
            Try
                monProcess.Start()
            Catch ex As Exception
                monProcess = New Process()
                monProcess.StartInfo.FileName = NomRepertoire & "RealisationPhysique.pdf"
                monProcess.Start()
            End Try
        End If

        If (ChkEmploiFondsActivite.Checked = True) Then
            Dim monProcess As New Process()
            monProcess.StartInfo.FileName = NomRepertoire & "EmploiFondsActivite.pdf"
            monProcess.StartInfo.Verb = "Print"
            monProcess.StartInfo.CreateNoWindow = True
            Try
                monProcess.Start()
            Catch ex As Exception
                monProcess = New Process()
                monProcess.StartInfo.FileName = NomRepertoire & "EmploiFondsActivite.pdf"
                monProcess.Start()
            End Try
        End If

        If (ChkTableauFonds.Checked = True) Then
            Dim monProcess As New Process()
            monProcess.StartInfo.FileName = NomRepertoire & "EmploiRessources.pdf"
            monProcess.StartInfo.Verb = "Print"
            monProcess.StartInfo.CreateNoWindow = True
            Try
                monProcess.Start()
            Catch ex As Exception
                monProcess = New Process()
                monProcess.StartInfo.FileName = NomRepertoire & "EmploiRessources.pdf"
                monProcess.Start()
            End Try
        End If

        If (ChkAvanceProjet.Checked = True) Then
            If (File.Exists(NomRepertoire & "ExamenAvancementProjet.docx") = False) Then
                RTRapportProjet.SaveDocument(NomRepertoire & "ExamenAvancementProjet.docx", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
            End If

            Dim monProcess As New Process()
            monProcess.StartInfo.FileName = NomRepertoire & "ExamenAvancementProjet.docx"
            monProcess.StartInfo.Verb = "Print"
            monProcess.StartInfo.CreateNoWindow = True
            Try
                monProcess.Start()
            Catch ex As Exception
                monProcess = New Process()
                monProcess.StartInfo.FileName = NomRepertoire & "ExamenAvancementProjet.docx"
                monProcess.Start()
            End Try
        End If

        'Table de matières **************************
        Dim monProcess0 As New Process()
        monProcess0.StartInfo.FileName = NomRepertoire & "TableMatiere.pdf"
        monProcess0.StartInfo.Verb = "Print"
        monProcess0.StartInfo.CreateNoWindow = True
        Try
            monProcess0.Start()
        Catch ex As Exception
            monProcess0 = New Process()
            monProcess0.StartInfo.FileName = NomRepertoire & "TableMatiere.pdf"
            monProcess0.Start()
        End Try

    End Sub

    Private Sub ImprimerAncRapport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImprimerAncRapport.Click

        If (ViewRapPrec.RowCount > 0) Then
            DrX = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)
            Bailleur = DrX("Bailleur")
            Dim NomRepertoire As String = DrX(1).ToString.Replace(" - ", "_") & " - " & Bailleur
            'Enlever les caractères bloqués dans la nomination du dossier
            NomRepertoire = NomRepertoire.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")
            NomRepertoire = line & "\RSF\" & NomRepertoire & "\"

            query = "select RefPiece from T_PieceJointeRSF where PeriodeRSF='" & DrX(1).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Dim monProcess As New Process()
                monProcess.StartInfo.FileName = line & rw(0).ToString
                monProcess.StartInfo.Verb = "Print"
                monProcess.StartInfo.CreateNoWindow = True
                Try
                    If File.Exists(line & rw(0).ToString) Then
                        monProcess.Start()
                        While monProcess.HasExited = False
                            'on attend
                        End While
                    End If
                Catch ex As Exception
                    monProcess = New Process()
                    monProcess.StartInfo.FileName = line & rw(0).ToString
                    If File.Exists(line & rw(0).ToString) Then
                        monProcess.Start()
                        While monProcess.HasExited = False
                            'on attend
                        End While
                    End If
                End Try
            Next

            Dim Fichier() As String = {"RapportMarches.pdf", "RealisationPhysique.pdf", "EmploiFondsActivite.pdf", "EmploiRessources.pdf", "ExamenAvancementProjet.docx"}
            For k As Integer = 0 To 4

                If (File.Exists(NomRepertoire & Fichier(k)) = True) Then
                    Dim monProcess As New Process()
                    monProcess.StartInfo.FileName = NomRepertoire & Fichier(k)
                    monProcess.StartInfo.Verb = "Print"
                    monProcess.StartInfo.CreateNoWindow = True
                    Try
                        monProcess.Start()
                        While monProcess.HasExited = False
                            'on attend
                        End While
                    Catch ex As Exception
                        monProcess = New Process()
                        monProcess.StartInfo.FileName = NomRepertoire & Fichier(k)
                        monProcess.Start()
                        While monProcess.HasExited = False
                            'on attend
                        End While
                    End Try
                End If

            Next

            If (File.Exists(NomRepertoire & "TableMatiere.pdf") = True) Then
                Dim monProcess As New Process()
                monProcess.StartInfo.FileName = NomRepertoire & "TableMatiere.pdf"
                monProcess.StartInfo.Verb = "Print"
                monProcess.StartInfo.CreateNoWindow = True
                Try
                    monProcess.Start()
                    While monProcess.HasExited = False
                        'on attend
                    End While
                Catch ex As Exception
                    monProcess = New Process()
                    monProcess.StartInfo.FileName = NomRepertoire & "TableMatiere.pdf"
                    monProcess.Start()
                    While monProcess.HasExited = False
                        'on attend
                    End While
                End Try

            End If

        End If

    End Sub

    Private Sub MenuStripPiecesJointes_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MenuStripPiecesJointes.Opening
        If ViewPieceJ.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub MenuStripRSF_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MenuStripRSF.Opening
        If ViewRapPrec.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub Actualiser_Click(sender As Object, e As EventArgs) Handles ActualiserAncRapport.Click
        If (ViewRapPrec.RowCount > 0) Then
            If ConfirmMsg("Voulez-vous actualiser ce rapport?") = DialogResult.Yes Then
                Dim drx = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)
                MiseAneuf()

                Dim periode = drx(1).ToString
                Bailleur = drx("Bailleur")
                'query = "SELECT TabEmploiRessources,EmploiFondsActivite,RealisationPhysq,PassationMarches WHERE PeriodeRSF='" & periode & "'"
                'Dim dtRSF As Data.DataTable = ExcecuteSelectQuery(query)

                query = "DELETE FROM t_rsf WHERE PeriodeRSF='" & periode & "' AND Bailleur='" & EnleverApost(Bailleur) & "'"
                ExecuteNonQuery(query)
                query = "DELETE FROM t_etatcompoemploires WHERE PeriodeRSF='" & periode & "' AND Bailleur='" & EnleverApost(Bailleur) & "'"
                ExecuteNonQuery(query)
                query = "DELETE FROM t_etatfondsemploires WHERE PeriodeRSF='" & periode & "' AND Bailleur='" & EnleverApost(Bailleur) & "'"
                ExecuteNonQuery(query)
                query = "DELETE FROM t_rsf_disponible_treso WHERE PeriodeRSF='" & periode & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
                query = "DELETE FROM t_etatfondsparactivite WHERE PeriodeRSF='" & periode & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
                'query = "DELETE FROM T_PieceJointeRSF WHERE PeriodeRSF='" & periode & "' AND Bailleur='" & EnleverApost(OldBailleur) & "' AND CodeProjet='" & ProjetEnCours & "'"
                'ExecuteNonQuery(query)

                DTDebPeriode.Text = periode.Split(" - ")(0)
                DTFinPeriode.Text = periode.Split(" - ")(2)
                DTFinPeriode_DateTimeChanged(sender, e)
                cmbBailleur.Text = Bailleur
                'InputBox(0, 1, Bailleur)
                If cmbBailleur.SelectedIndex = -1 Then
                    cmbBailleur.SelectedIndex = 0
                    Bailleur = cmbBailleur.Text
                End If
                BtOuvrirRSF.PerformClick()
            End If
        End If
    End Sub

    Private Sub ConsoliderAncRapport_Click(sender As Object, e As EventArgs) Handles ConsoliderAncRapport.Click
        Try
            If (ViewRapPrec.RowCount > 0) Then
                Dim drx = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)
                Dim periode = drx(1).ToString
                'MiseAneuf()
                'DTDebPeriode.Text = periode.Split(" - ")(0)
                'DTFinPeriode.Text = periode.Split(" - ")(2)
                'DTFinPeriode_DateTimeChanged(sender, e)
                OuvrirAncRapport.PerformClick()

                Dim PeriodeDossierRSF As String = periode.Replace(" - ", "_").Replace("/", "")
                Dim DossierRSF As String = PeriodeDossierRSF & " - " & Bailleur
                'Enlever les caractères bloqués dans la nomination du dossier
                DossierRSF = DossierRSF.Replace("/", "").Replace("\", "").Replace("|", "").Replace(":", "").Replace("*", "").Replace("""", "").Replace("<", "").Replace(">", "")
                Dim NomRepertoire As String = line & "\RSF\" & DossierRSF & "\"

                If File.Exists(NomRepertoire & "Rapport_RSF_" & periode.Replace("/", "-") & ".docx") Then
                    Try
                        File.Delete(NomRepertoire & "Rapport_RSF_" & periode.Replace("/", "-") & ".docx")
                    Catch ex As Exception
                        FailMsg("L'ancien rapport est déjà ouvert dans une autre application." & vbNewLine & "Fermer le et réssayer.")
                        Exit Sub
                    End Try
                End If
                '

                'Table de matières **************************
                Dim Report As Engine.ReportDocument = CType(AppercuRSF.ReportSource, Engine.ReportDocument)
                'Report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.RichText, NomRepertoire & "TableDesMatieres.rtf")
                Report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, NomRepertoire & "TableDesMatieres.doc")

                If (ChkTableauFonds.Checked = True) Then
                    Report = CType(RptViewerRessourcesEmplois.ReportSource, Engine.ReportDocument)
                    Report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, NomRepertoire & "EmploiRessources.doc")
                End If

                If (ChkEmploiFondsActivite.Checked = True) Then
                    Report = CType(RptViewerEmploisActivites.ReportSource, Engine.ReportDocument)
                    Report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, NomRepertoire & "EmploiFondsActivite.doc")
                End If

                Dim LesFichiers As String() = {NomRepertoire & "TableDesMatieres.doc", NomRepertoire & "ExamenAvancementProjet.docx", NomRepertoire & "EmploiRessources.doc", NomRepertoire & "EmploiFondsActivite.doc"}
                For i = 0 To LesFichiers.Length - 1
                    If File.Exists(LesFichiers(i)) Then
                        Process.Start(LesFichiers(i))
                    End If
                Next


            End If
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
    Private Sub LoadCompteBailleur()
        cmbBailleur.Properties.Items.Clear()
        cmbBailleur.ResetText()
        query = "SELECT * FROM t_bailleur WHERE CodeProjet='" & ProjetEnCours & "'"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        cmbBailleur.Properties.Items.Add("Tous les bailleurs")
        For Each rw As DataRow In dt.Rows
            cmbBailleur.Properties.Items.Add(rw("InitialeBailleur") & " - " & MettreApost(rw("NomBailleur")))
        Next
    End Sub

    Private Sub GridPieceJ_DoubleClick(sender As Object, e As EventArgs) Handles GridPieceJ.DoubleClick
        ApercuPJ.PerformClick()
    End Sub
End Class