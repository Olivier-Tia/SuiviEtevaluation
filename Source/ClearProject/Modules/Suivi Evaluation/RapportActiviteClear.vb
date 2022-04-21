Imports MySql.Data.MySqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class RapportActiviteClear

    Dim dtPieces = New DataTable
    Dim DrX As DataRow
    Dim DrX1 As DataRow

    Private Sub RapportActiviteClear_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        query = "select COUNT(*) from T_Partition where CodeProjet='" & ProjetEnCours & "'"
        If Val(ExecuteScallar(query)) > 0 Then
            ChargerCompo()
            MiseAneuf()
        End If

        dtPieces.Columns.Clear()
        dtPieces.Columns.Add("Code", Type.GetType("System.String"))
        dtPieces.Columns.Add("Nom du fichier", Type.GetType("System.String"))
        dtPieces.Columns.Add("Chemin", Type.GetType("System.String"))
        dtPieces.Columns.Add("ID_Piece", Type.GetType("System.String"))
        GridPieceJ.DataSource = dtPieces
        ViewPieceJ.Columns("Code").Visible = False
        ViewPieceJ.Columns("Nom du fichier").Width = 300
        ViewPieceJ.Columns("Chemin").Visible = False
        ViewPieceJ.Columns("ID_Piece").Visible = False
    End Sub

    Private Sub ChargerCompo()
        query = "select LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)=1 and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
        CmbCompo.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbCompo.Properties.Items.Add(rw("LibelleCourt").ToString & " : " & MettreApost(rw("LibellePartition").ToString))
        Next
    End Sub

    Private Sub CmbCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompo.SelectedValueChanged, GroupControl2.ContextMenuStripChanged
        Initialiser()
        CmbSousCompo.Text = ""
        Dim CodeCompo As String = ""
        If CmbCompo.SelectedIndex <> -1 Then
            CodeCompo = Mid(CmbCompo.Text, 1, 1)
            ChargerSousCompo(CodeCompo)
        Else
            CmbSousCompo.Properties.Items.Clear()
        End If
    End Sub

    Private Sub ChargerSousCompo(ByVal Compo As String)
        query = "select LibelleCourt, LibellePartition from T_Partition where CodeClassePartition=2 and LibelleCourt like '" & Compo & "%' and CodeProjet='" & ProjetEnCours & "' order by length(libelleCourt),libelleCourt"
        CmbSousCompo.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbSousCompo.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub CmbSousCompo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSousCompo.SelectedValueChanged
        Initialiser()
        CmbActivite.Text = ""
        Dim CodeSousC As String = ""
        Dim codesouscomp() As String
        codesouscomp = CmbSousCompo.Text.Split(" : ")
        If CmbSousCompo.SelectedIndex <> -1 Then
            CodeSousC = codesouscomp(0).ToString
            Dim souscomp As String = ""
            query = "select codepartition from T_Partition where LibelleCourt='" & CodeSousC.ToString & "'"
            souscomp = ExecuteScallar(query)
            ChargerActivite(souscomp)
        Else
            CmbActivite.Properties.Items.Clear()
        End If
    End Sub

    Private Sub ChargerActivite(ByVal SousC As String)
        query = "select LibelleCourt, LibellePartition from T_Partition where CodeClassePartition=5 and CodePartitionMere  = '" & SousC & "' and CodeProjet='" & ProjetEnCours & "' order by length(libelleCourt),libelleCourt"
        CmbActivite.Properties.Items.Clear()
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbActivite.Properties.Items.Add(rw(0).ToString & " : " & MettreApost(rw(1).ToString))
        Next
    End Sub

    Private Sub CmbActivite_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbActivite.SelectedValueChanged
        Initialiser()

        If (CmbActivite.SelectedIndex <> -1) Then
            BtNouvRapport.Enabled = True

            Dim codeact() As String
            codeact = CmbActivite.Text.Split(" : ")

            query = "select CodePartition, DateDebutPartition, DateFinPartition from T_Partition where LibelleCourt='" & codeact(0).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                CodePartCache.Text = rw("CodePartition").ToString
                DtDebPeriode.DateTime = CDate(rw("DateDebutPartition")).ToShortDateString
                DtFinPeriode.DateTime = CDate(rw("DateFinPartition")).ToShortDateString
                LbPeriodeActiv.Text = "Du " & rw("DateDebutPartition").ToString & " Au " & rw("DateFinPartition").ToString

                DtDebPeriode.Properties.MinValue = CDate(rw("DateDebutPartition"))
                DtFinPeriode.Properties.MaxValue = CDate(rw("DateFinPartition"))
            Next
            LesAutresRapport()
            GridPieceJ.DataSource = dtPieces
            dtPieces.Rows.clear()
        Else
            Dim dtAutre As DataTable = GridRapPrec.DataSource
            dtAutre.Rows.Clear()
            GridPieceJ.DataSource = dtPieces
            dtPieces.Rows.clear()
        End If
    End Sub

    Private Sub LesAutresRapport()
        Dim dtAutre = New DataTable()
        dtAutre.Columns.Clear()
        dtAutre.Columns.Add("Code", Type.GetType("System.String"))
        dtAutre.Columns.Add("Numéro", Type.GetType("System.String"))
        dtAutre.Columns.Add("Titre", Type.GetType("System.String"))
        dtAutre.Columns.Add("Detail", Type.GetType("System.String"))
        dtAutre.Columns.Add("Probleme", Type.GetType("System.String"))
        dtAutre.Columns.Add("Solution", Type.GetType("System.String"))
        dtAutre.Columns.Add("Conclusion", Type.GetType("System.String"))
        dtAutre.Columns.Add("Valeur", Type.GetType("System.String"))
        dtAutre.Columns.Add("Pourcentage", Type.GetType("System.String"))
        dtAutre.Columns.Add("DateAjoutRapport", Type.GetType("System.String"))

        Dim cptr As Decimal = 0
        query = "select NumRapport,TitreRapport,DetailRapport,PbRapport,SolutRapport,ConcluRapport,ValeurRealise,PourcentRealise, DateAjout from T_RapportActivites where CodePartition='" & CodePartCache.Text & "'"
        dtAutre.Rows.Clear()

        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            cptr += 1
            Dim drS = dtAutre.NewRow()

            drS("Code") = IIf(CDec(cptr / 2) <> CDec(cptr \ 2), "x", "").ToString
            drS("Numéro") = rw("NumRapport").ToString
            drS("Titre") = MettreApost(rw("TitreRapport").ToString)
            drS("Detail") = MettreApost(rw("DetailRapport").ToString)
            drS("Probleme") = MettreApost(rw("PbRapport").ToString)
            drS("Solution") = MettreApost(rw("SolutRapport").ToString)
            drS("Conclusion") = MettreApost(rw("ConcluRapport").ToString)
            drS("Valeur") = rw("ValeurRealise").ToString
            drS("Pourcentage") = rw("PourcentRealise").ToString
            drS("DateAjoutRapport") = rw("DateAjout").ToString
            dtAutre.Rows.Add(drS)
        Next

        GridRapPrec.DataSource = dtAutre
        ViewRapPrec.Columns("Code").Visible = False
        ViewRapPrec.Columns("Numéro").Width = 100
        ViewRapPrec.Columns("Titre").Width = 200
        ViewRapPrec.Columns("Detail").Visible = False
        ViewRapPrec.Columns("Probleme").Visible = False
        ViewRapPrec.Columns("Solution").Visible = False
        ViewRapPrec.Columns("Conclusion").Visible = False
        ViewRapPrec.Columns("Valeur").Width = 100
        ViewRapPrec.Columns("Pourcentage").Width = 100
        ViewRapPrec.Columns("DateAjoutRapport").Visible = False

        ViewRapPrec.Columns("Pourcentage").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ViewRapPrec.Columns("Numéro").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ViewRapPrec.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)

        ColorRowGrid(ViewRapPrec, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)

        If (ViewRapPrec.RowCount > 0) Then
            BtAppRapport.Enabled = True
        Else
            BtAppRapport.Enabled = False
        End If
    End Sub

    Private Sub NumeroRapport()
        Dim NbreRappExist As Decimal = 0
        query = "select NumRapport from T_RapportActivites where CodePartition='" & CodePartCache.Text & "' ORDER by DateAjout DESC LIMIT 1"
        Dim NumRapport As String = MettreApost(ExecuteScallar(query))

        If NumRapport.ToString = "" Then
            NbreRappExist = 1
        Else
            Dim NbreRappor As String = IIf(Len(NumRapport.ToString) = 23, Mid(NumRapport.ToString, 6, 3), Mid(NumRapport.ToString, 7, 3))
            Dim NumRapp As Decimal = CInt(NbreRappor)
            NbreRappExist = NumRapp + 1
        End If

        Dim codeact() As String
        codeact = CmbActivite.Text.Split(" : ")

        Dim NbreEnChaine As String = ""
        If (NbreRappExist < 10) Then
            NbreEnChaine = codeact(0).ToString & "_00" & NbreRappExist.ToString & "/" & ProjetEnCours & "/" & Now.ToShortDateString.Replace("/", "")
        ElseIf (NbreRappExist < 100) Then
            NbreEnChaine = codeact(0).ToString & "_0" & NbreRappExist.ToString & "/" & ProjetEnCours & "/" & Now.ToShortDateString.Replace("/", "")
        Else
            NbreEnChaine = codeact(0).ToString & "_" & NbreRappExist.ToString & "/" & ProjetEnCours & "/" & Now.ToShortDateString.Replace("/", "")
        End If

        GbRapport.Text = "RAPPORT N° " & NbreEnChaine
        BtNumRap.Text = NbreEnChaine
        NumRappCache.Text = NbreEnChaine
    End Sub

    Private Sub BtNouvRapport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtNouvRapport.Click
        If (CmbActivite.SelectedIndex <> -1) Then

            NumeroRapport()
            'vider les champs
            ViderEtDesactivater()
            LesDonneesTechniquesNouveauRapport()
            BtEnrgRapport.Text = "Enregistrer"
            BtAnnulerRapp.Enabled = True

            Dim CodeActivite() As String
            CodeActivite = CmbActivite.Text.Split(" : ")

            query = "select DtFinRapport from T_RapportActivites where CodePartition='" & CodePartCache.Text & "' and NumRapport like '" & CodeActivite(0).ToString & "%' ORDER by DateAjout DESC LIMIT 1"
            Dim PeriodRaport As String = MettreApost(ExecuteScallar(query))

            If PeriodRaport.ToString = "" Then
                DtDebPeriode.DateTime = CDate(Mid(LbPeriodeActiv.Text, 4, 10)).ToShortDateString
                DtDebPeriode.Enabled = False
                DtFinPeriode.Properties.MinValue = CDate(Mid(LbPeriodeActiv.Text, 4, 10))
            Else
                Dim dtpls As String = Mid(PeriodRaport.ToString, 1, 2)
                Dim RestantDat As String = Mid(PeriodRaport.ToString, 3, 8)
                Dim plun As Integer = CInt(dtpls)

                If CDate(PeriodRaport) < CDate(Mid(LbPeriodeActiv.Text, 27, 10)) Then
                    plun += 1
                End If

                If plun < 10 Then
                    dtpls = "0" & plun.ToString
                Else
                    dtpls = plun.ToString
                End If

                Dim DatDeb As String = dtpls & RestantDat
                DtDebPeriode.DateTime = CDate(DatDeb).ToShortDateString
                DtDebPeriode.Enabled = False
                DtFinPeriode.Properties.MinValue = CDate(DatDeb)
            End If

            PnlNouvRapport.Enabled = True
            DtDebPeriode.Enabled = False
            DtFinPeriode.Enabled = True

            TxtValActu.Enabled = True
            PrctPeriode.Enabled = True

            BtSelectPJ.Enabled = True

            TxtTitre.Enabled = True
            TxtTitre.Focus()
        End If

    End Sub

    Private Sub ViderEtDesactivater()
        Paneltype.Enabled = True
        RapportInterm.Checked = False
        RapportAchev.Checked = False

        TxtTitre.Text = ""
        TxtDetail.Text = ""
        TxtProbleme.Text = ""
        TxtSolution.Text = ""
        TxtConclusion.Text = ""
        PrctPeriode.Text = 0
        TxtValActu.Text = ""
        TxtTitre.Enabled = False
        TxtDetail.Enabled = False
        TxtProbleme.Enabled = False
        TxtSolution.Enabled = False
        TxtConclusion.Enabled = False

        'vider les pièces jointe et desactiver
        BtSelectPJ.Enabled = False
        GridPieceJ.DataSource = Nothing
        GridPieceJ.Refresh()
    End Sub

    'les donnees technique nouvo rapport
    Private Sub LesDonneesTechniquesNouveauRapport()
        Dim ValRealise As String = ""
        Dim PrctRealise As Decimal = 0
        Dim LibIndic As String = ""

        Dim codeact() As String
        codeact = CmbActivite.Text.Split(" : ")
        query = "select PourcentRealise, pourcentagetotal from T_RapportActivites where CodePartition='" & CodePartCache.Text & "' and NumRapport like '" & codeact(0).ToString & "%' ORDER by DateAjout DESC LIMIT 1"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            TxtValPrecede.Text = rw("PourcentRealise").ToString
            PrctActu.Text = rw("pourcentagetotal").ToString
            PrctCache.Text = rw("pourcentagetotal").ToString
        Next
        query = "select B.LibelleIndicateur from T_IndicateurPartition as A,T_Indicateur as B where A.CodeIndicateur=B.CodeIndicateur and A.CodePartition='" & CodePartCache.Text & "'"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        For Each rw2 As DataRow In dt2.Rows
            LibIndic = MettreApost(rw2("LibelleIndicateur").ToString)
        Next
        TxtIndicateurPerform.Text = "Veuillez indiquer ci-dessous la valeur de l'avancement des travaux dépuis le début de l'activité en fonction de l'indicateur de performance :" & vbNewLine & "[ " & LibIndic & " ]"
    End Sub

    Private Sub MajPartition()
        Dim codeact() As String
        codeact = CmbActivite.Text.Split(" : ")
        Dim prct As Double = 0
        prct = PrctActu.Text
        query = "update T_Partition set ProgressionPartition='" & prct.ToString & "' where LibelleCourt='" & codeact(0).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
        ExecuteNonQuery(query)
    End Sub

    Private Sub EnregistretePiece()
        If ViewPieceJ.RowCount > 0 Then
            For i = 0 To ViewPieceJ.RowCount - 1
                Dim fichier = ViewPieceJ.GetRowCellValue(i, "Chemin").ToString
                Dim NomComp As String() = fichier.Split("\"c)
                Dim NomCourt As String = NomComp(NomComp.Length - 1)
                Dim NomDossier As String = NumRappCache.Text.Replace("/", "_")
                NomDossier = line & "\Rapports\" & NomDossier
                If (Directory.Exists(NomDossier) = False) Then
                    Directory.CreateDirectory(NomDossier)
                End If

                If (File.Exists(NomDossier & "\" & NomComp(NomComp.Length - 1)) = False) Then

                    File.Copy(fichier, NomDossier & "\" & NomComp(NomComp.Length - 1), True)

                    Dim DatSet = New DataSet
                    query = "select * from T_PieceJointe"
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_PieceJointe")
                    Dim DatTable = DatSet.Tables("T_PieceJointe")
                    Dim DatRow = DatSet.Tables("T_PieceJointe").NewRow()

                    ' DatRow("ID_Pieces") = ""
                    DatRow("RefPiece") = "\Rapports\" & NumRappCache.Text.Replace("/", "_") & "\" & NomComp(NomComp.Length - 1)
                    DatRow("NumRapport") = NumRappCache.Text
                    DatRow("NomPiece") = NomComp(NomComp.Length - 1)
                    DatRow("DatePiece") = Now.ToShortDateString & " " & Now.ToLongTimeString

                    DatSet.Tables("T_PieceJointe").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
                    DatAdapt.Update(DatSet, "T_PieceJointe")
                    DatSet.Clear()
                    BDQUIT(sqlconn)
                End If
            Next
        End If
    End Sub

    Private Sub BtEnrgRapport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgRapport.Click
        Try
            If BtEnrgRapport.Text = "Enregistrer" Then

                If (DateTime.Compare(CDate(DtFinPeriode.DateTime).ToShortDateString, CDate(DtDebPeriode.DateTime).ToShortDateString) < 0) Then
                    SuccesMsg("La période est incorrecte !")
                    Exit Sub
                End If

                query = "SELECT DtFinRapport from t_rapportactivites where CodePartition='" & CodePartCache.Text & "' order by DateAjout DESC LIMIT 1"
                Dim DtFinRaport As String = MettreApost(ExecuteScallar(query))

                If DtFinRaport.ToString <> "" Then
                    If CDate(Mid(DtFinRaport.ToString, 1, 10)) = CDate(Mid(LbPeriodeActiv.Text, 27, 10)) Then
                        FailMsg("Impossible d'édité un autre rapport sur la fiche d'activité")
                        Exit Sub
                    End If
                End If

                If RapportInterm.Checked = False And RapportAchev.Checked = False Then
                        SuccesMsg("Veuillez cocher le type du rapport")
                        Exit Sub
                    End If

                    If (TxtTitre.Text = "" And TxtDetail.Text = "" And TxtProbleme.Text = "" And TxtSolution.Text = "" And TxtConclusion.Text = "") Then
                        SuccesMsg("Aucune donnée à enregistrer !")
                        Exit Sub
                    End If

                    If PrctPeriode.Text = 0 Or PrctActu.Text > 100 Then
                        FailMsg("Impossible d'enregistré la réalisation actuelle")
                        Exit Sub
                    End If

                    Dim ExistAchevRaport As Boolean = False
                    query = "select TypeRapport from t_rapportactivites where CodePartition='" & CodePartCache.Text & "'"
                    Dim dts As DataTable = ExcecuteSelectQuery(query)
                    For Each rw As DataRow In dts.Rows
                        If rw("TypeRapport").ToString = "ACHEVEMENT" Then
                            ExistAchevRaport = True
                        End If
                    Next

                    If ExistAchevRaport = True Then
                        FailMsg("Impossible d'édité un autre rapport sur la fiche d'activité")
                        Exit Sub
                    End If

                    Dim DatSet = New DataSet
                    query = "select * from T_RapportActivites"
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_RapportActivites")
                    Dim DatTable = DatSet.Tables("T_RapportActivites")
                    Dim DatRow = DatSet.Tables("T_RapportActivites").NewRow()

                    DatRow("NumRapport") = NumRappCache.Text
                    DatRow("DateRapport") = Now.ToShortDateString

                    If RapportInterm.Checked = True Then
                        DatRow("TypeRapport") = "INTERMEDIAIRE"
                    Else
                        DatRow("TypeRapport") = "ACHEVEMENT"
                    End If

                    DatRow("DtDebRapport") = DtDebPeriode.Text
                    DatRow("DtFinRapport") = DtFinPeriode.Text

                    DatRow("CodePartition") = CodePartCache.Text
                    DatRow("DateAjout") = Now.ToShortDateString & " " & Now.ToLongTimeString
                DatRow("Operateur") = CodeUtilisateur

                DatRow("TitreRapport") = EnleverApost(TxtTitre.Text)
                    DatRow("DetailRapport") = EnleverApost(TxtDetail.Text)
                    DatRow("PbRapport") = EnleverApost(TxtProbleme.Text)
                    DatRow("SolutRapport") = EnleverApost(TxtSolution.Text)
                    DatRow("ConcluRapport") = EnleverApost(TxtConclusion.Text)
                    DatRow("ValeurRealise") = EnleverApost(TxtValActu.Text)
                    DatRow("PourcentRealise") = PrctPeriode.Text
                    DatRow("pourcentagetotal") = PrctActu.Text
                DatRow("EtatRapport") = "0"
                DatRow("CodeProjet") = ProjetEnCours
                DatSet.Tables("T_RapportActivites").Rows.Add(DatRow) 'ajout d'une nouvelle ligne 
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt) 'execution de l'enregistrement
                    DatAdapt.Update(DatSet, "T_RapportActivites")
                    DatSet.Clear()
                    BDQUIT(sqlconn)

                    'mise ajour progression partion
                    MajPartition()

                    'enregistrement pices
                    EnregistretePiece()
                    SuccesMsg("Rapport enregistré avec succès")
                    LesAutresRapport()
                    'actualisation des données
                    MiseAneuf()

                Else
                    If (DateTime.Compare(CDate(DtFinPeriode.DateTime).ToShortDateString, CDate(DtDebPeriode.DateTime).ToShortDateString) < 0) Then
                    SuccesMsg("La période est incorrecte !")
                    Exit Sub
                End If

                If (TxtTitre.Text = "" And TxtDetail.Text = "" And TxtProbleme.Text = "" And TxtSolution.Text = "" And TxtConclusion.Text = "") Then
                    SuccesMsg("Aucune donnée à modifier !")
                    Exit Sub
                End If

                If PrctPeriode.Text > 100 And PrctActu.Text > 100 Then
                    FailMsg("Saisissez une période valide")
                    Exit Sub
                End If

                Dim SomPctExit As Integer = 0
                Dim code As String()
                code = NumRappCache.Text.Split("_")

                query = "select pourcentagetotal from t_rapportactivites where NumRapport LIKE '" & code(0).ToString & "%' order by DateAjout DESC LIMIT 1"
                SomPctExit = Val(ExecuteScallar(query))
                SomPctExit -= PoutagActuel.Text
                SomPctExit += PrctPeriode.Text

                If SomPctExit > 100 Then
                    FailMsg("Le pourcentage de la réalisation de l'activité est atteint")
                    Exit Sub
                End If

                Dim ValPreceden As Integer
                query = "select NumRapport, pourcentagetotal from t_rapportactivites where NumRapport LIKE '" & code(0).ToString & "%'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                Dim j As Integer = -1
                For Each rw As DataRow In dt.Rows
                    j += 1
                    If rw("NumRapport").ToString = NumRappCache.Text Then
                        If dt.Rows(0)("NumRapport") = NumRappCache.Text Then
                            ValPreceden = 0
                        Else
                            ValPreceden = dt.Rows(j - 1)("pourcentagetotal")
                        End If
                    End If
                Next

                Dim PcrtTopredt As Integer = ValPreceden + CInt(PrctPeriode.Text)

                query = "select NumRapport, PourcentRealise from t_rapportactivites where NumRapport LIKE '" & code(0).ToString & "%' and DateAjout >'" & datemodifs.Text & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)

                query = "UPDATE T_RapportActivites SET TitreRapport='" & EnleverApost(TxtTitre.Text) & "', DetailRapport='" & EnleverApost(TxtDetail.Text) & "', PbRapport='" & EnleverApost(TxtProbleme.Text) & "', SolutRapport='" & EnleverApost(TxtSolution.Text) & "', ConcluRapport='" & EnleverApost(TxtConclusion.Text) & "', DtFinRapport = '" & dateconvert(DtFinPeriode.Text) & "', ValeurRealise='" & TxtValActu.Text & "',PourcentRealise='" & PrctPeriode.Text & "', pourcentagetotal='" & PcrtTopredt & "', Operateur = '" & CodeUtilisateur & "' where NumRapport='" & NumRappCache.Text & "'"
                ExecuteNonQuery(query)

                For Each rw As DataRow In dt1.Rows
                    query = "UPDATE T_RapportActivites set pourcentagetotal='" & CInt(rw("PourcentRealise").ToString) + PcrtTopredt & "' where NumRapport='" & rw("NumRapport").ToString & "'"
                    ExecuteNonQuery(query)
                    PcrtTopredt += CInt(rw("PourcentRealise").ToString)
                Next

                'mise a jour partition
                query = "update T_Partition set ProgressionPartition='" & PcrtTopredt.ToString & "' where LibelleCourt='" & code(0).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                'mise a jour pieces jointe
                EnregistretePiece()

                SuccesMsg("Modification effectuée avec succès")
                BtEnrgRapport.Text = "Enregistrer"

                LesAutresRapport()
                'actualisation des données
                MiseAneuf()
            End If
        Catch ex As Exception
            FailMsg("Erreur information non disponible " & ex.ToString)
        End Try

    End Sub
    Public Sub Initialiser()
        RapportInterm.Checked = False
        RapportAchev.Checked = False
        RapportInterm.Enabled = True
        RapportAchev.Enabled = True
        DtDebPeriode.Text = ""
        DtFinPeriode.Text = ""
        TxtTitre.Text = ""
        TxtTitre.Enabled = False
        TxtDetail.Text = ""
        TxtDetail.Enabled = False
        TxtProbleme.Text = ""
        TxtProbleme.Enabled = False
        TxtSolution.Text = ""
        TxtSolution.Enabled = False
        TxtConclusion.Text = ""
        TxtConclusion.Enabled = False

        BtEnrgRapport.Enabled = False
        BtAppRapport.Enabled = False
        BtSelectPJ.Enabled = False

        GridPieceJ.DataSource = Nothing
        GridPieceJ.Refresh()

        GbRapport.Text = "SAISIE RAPPORT"
        TxtIndicateurPerform.Text = "..."
        TxtValPrecede.Text = ""
        TxtValActu.Text = ""
        TxtValActu.Enabled = False
        PrctActu.Text = 0
        PrctPeriode.Text = 0
        'LbPeriodeActiv.Text = "..."
        BtNumRap.Text = "..."
        DtFinPeriode.Enabled = True
    End Sub

    Public Sub MiseAneuf()
        RapportInterm.Checked = False
        RapportAchev.Checked = False
        RapportInterm.Enabled = True
        RapportAchev.Enabled = True
        DtDebPeriode.Text = ""
        DtFinPeriode.Text = ""
        TxtTitre.Text = ""
        TxtTitre.Enabled = False
        TxtDetail.Text = ""
        TxtDetail.Enabled = False
        TxtProbleme.Text = ""
        TxtProbleme.Enabled = False
        TxtSolution.Text = ""
        TxtSolution.Enabled = False
        TxtConclusion.Text = ""
        TxtConclusion.Enabled = False

        ' BtNouvRapport.Enabled = False
        BtEnrgRapport.Enabled = False
        ' BtAppRapport.Enabled = False
        BtSelectPJ.Enabled = False

        GridPieceJ.DataSource = Nothing
        GridPieceJ.Refresh()

        GbRapport.Text = "SAISIE RAPPORT"
        TxtIndicateurPerform.Text = "..."
        TxtValPrecede.Text = ""
        TxtValActu.Text = ""
        TxtValActu.Enabled = False
        PrctActu.Text = 0
        PrctPeriode.Text = 0
        'LbPeriodeActiv.Text = "..."
        BtNumRap.Text = "..."
        DtFinPeriode.Enabled = True
    End Sub

    Private Sub RapportActiviteClear_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub


    Private Sub TxtTitre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtTitre.TextChanged
        If (TxtTitre.Text <> "") Then
            TxtDetail.Enabled = True
        Else
            TxtDetail.Enabled = False
            TxtDetail.Text = ""
        End If
    End Sub

    Private Sub TxtDetail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDetail.TextChanged
        If (TxtDetail.Text <> "") Then
            TxtProbleme.Enabled = True
        Else
            TxtProbleme.Text = ""
            TxtProbleme.Enabled = False
        End If
    End Sub

    Private Sub TxtProbleme_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtProbleme.TextChanged
        If (TxtProbleme.Text <> "") Then
            TxtSolution.Enabled = True
        Else
            TxtSolution.Text = ""
            TxtSolution.Enabled = False
        End If
    End Sub

    Private Sub TxtSolution_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSolution.TextChanged
        If (TxtSolution.Text <> "") Then
            TxtConclusion.Enabled = True
        Else
            TxtConclusion.Text = ""
            TxtConclusion.Enabled = False
        End If
    End Sub

    Private Sub TxtValActu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtValActu.TextChanged
        If (PrctPeriode.Text > 0 And TxtValActu.Text <> "") Then
            BtEnrgRapport.Enabled = True
        Else
            BtEnrgRapport.Enabled = False
        End If
    End Sub

    Private Sub BtSelectPJ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSelectPJ.Click
        Dim dlg As New OpenFileDialog
        'type du document a ouvrir'
        dlg.Filter = "Tout|*.pdf;*.jpg;*.jpeg;*.png;*.gif;*.bmp|Documents|*.pdf|Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
        If (dlg.ShowDialog() = DialogResult.OK) Then
            Dim k As Integer = 0
            Dim fichier As String = dlg.FileName
            Dim NomCourt As String = dlg.SafeFileName
            For i As Integer = 0 To ViewPieceJ.RowCount - 1
                k += 1
                If (ViewPieceJ.GetRowCellValue(i, "Chemin").ToString = fichier) Then
                    SuccesMsg("Ce fichier à déjà été ajouter !")
                    Exit Sub
                End If
            Next

            Dim drS = dtPieces.NewRow()
            drS("Code") = IIf(CDec(k / 2) <> CDec(k \ 2), "x", "").ToString
            drS("Nom du fichier") = NomCourt
            drS("Chemin") = fichier
            drS("ID_Piece") = ""
            dtPieces.Rows.Add(drS)

            GridPieceJ.DataSource = dtPieces

            ViewPieceJ.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            ColorRowGrid(ViewPieceJ, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
        End If
    End Sub

    Private Sub ChargerPJ(ByVal NumRap As String)
        query = "select ID_Pieces, RefPiece, NomPiece from T_PieceJointe where NumRapport='" & NumRap & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim dpiece As DataTable = GridPieceJ.DataSource
        dpiece.Rows.Clear()

        If dt.Rows.Count > 0 Then
            Dim nbreP As Decimal = 0
            For Each rw In dt.Rows
                nbreP += 1
                Dim drS = dpiece.NewRow()
                drS("Code") = IIf(CDec(nbreP / 2) <> CDec(nbreP \ 2), "x", "").ToString
                drS("Nom du fichier") = MettreApost(rw("NomPiece").ToString)
                drS("Chemin") = line & MettreApost(rw("RefPiece").ToString)
                drS("ID_Piece") = MettreApost(rw("ID_Pieces").ToString)
                dpiece.Rows.Add(drS)
            Next

            ViewPieceJ.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
            ColorRowGrid(ViewPieceJ, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
        End If
    End Sub

    Private Sub BtAnnulerRapp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAnnulerRapp.Click
        MiseAneuf()
        BtEnrgRapport.Text = "Enregistrer"
    End Sub

    Private Sub OuvrirToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OuvrirToolStripMenuItem.Click

        If (ViewPieceJ.RowCount > 0) Then
            DrX = ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle)
            Process.Start(DrX("Chemin").ToString)
        End If

    End Sub

    'Private Sub ImprimerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    'If (ViewPieceJ.RowCount > 0) Then
    '    '    DrX = ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle)
    '    '    Dim monProcess As New Process()
    '    '    monProcess.StartInfo.FileName = DrX("Chemin").ToString
    '    '    monProcess.StartInfo.Verb = "Print"
    '    '    monProcess.StartInfo.CreateNoWindow = True
    '    '    monProcess.Start()
    '    'End If

    'End Sub

    Private Sub BtAppRapport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAppRapport.Click
        If Not Access_Btn("BtnPrintRappActiv") Then
            Exit Sub
        End If

        If (ViewRapPrec.RowCount > 0) Then
            DrX = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)
            Dim Chemin As String = lineEtat & "\"

            Dim report As New ReportDocument
            Dim DatSet = New DataSet

            report.Load(Chemin & "\RapportActivites\RapportActivite.rpt")
            Progression(90)
            report.SetDataSource(DatSet)
            report.SetParameterValue("NumRapport", DrX("Numéro").ToString)
            report.SetParameterValue("Source", line, "PieceJointe.rpt")

            FullScreenReport.FullView.ReportSource = report
            FullScreenReport.ShowDialog()
        End If

    End Sub
    Private Sub GridRapPrec_DoubleClick(sender As Object, e As EventArgs) Handles GridRapPrec.DoubleClick
        If (ViewRapPrec.RowCount > 0) Then
            BtEnrgRapport.Text = "Modifier"
            BtEnrgRapport.Enabled = True

            DrX = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)
            NumRappCache.Text = DrX("Numéro").ToString
            ModifLesDonneesTechniques()

            'pourcentage precedent
            Dim code As String()
            code = NumRappCache.Text.Split("_")
            Dim ValPrecedens As Integer = 0

            query = "select NumRapport, PourcentRealise from t_rapportactivites where NumRapport LIKE '" & code(0).ToString & "%'"
            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
            Dim j As Integer = -1
            For Each rw As DataRow In dt2.Rows
                j += 1
                If rw("NumRapport").ToString = NumRappCache.Text Then
                    If dt2.Rows(0)("NumRapport") = NumRappCache.Text Then
                        ValPrecedens = dt2.Rows(0)("PourcentRealise")
                    Else
                        ValPrecedens = dt2.Rows(j - 1)("PourcentRealise")
                    End If
                End If
            Next

            query = "select DtDebRapport,DtFinRapport,TypeRapport from T_RapportActivites where NumRapport='" & DrX("Numéro").ToString & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt.Rows
                DtDebPeriode.DateTime = CDate(Mid(rw("DtDebRapport").ToString, 1, 10)).ToShortDateString
                DtFinPeriode.DateTime = CDate(Mid(rw("DtFinRapport").ToString, 1, 10)).ToShortDateString

                If rw("TypeRapport").ToString = "INTERMEDIAIRE" Then
                    RapportInterm.Checked = True
                Else
                    RapportAchev.Checked = True
                End If
            Next

            ' verification
            query = "select NumRapport from T_RapportActivites where NumRapport LIKE '" & code(0).ToString & "%' AND DateAjout > '" & DrX("DateAjoutRapport").ToString & "'"
            Dim VerifModif As DataTable = ExcecuteSelectQuery(query)

            If VerifModif.Rows.Count = 0 Then
                PnlNouvRapport.Enabled = True
                Paneltype.Enabled = False
                DtDebPeriode.Enabled = False
                DtFinPeriode.Enabled = True
            Else
                PnlNouvRapport.Enabled = False
            End If

            BtNumRap.Text = DrX("Numéro").ToString
            TxtTitre.Text = DrX("Titre").ToString
            TxtDetail.Text = DrX("Detail").ToString
            TxtProbleme.Text = DrX("Probleme").ToString
            TxtSolution.Text = DrX("Solution").ToString
            TxtConclusion.Text = DrX("Conclusion").ToString
            PrctPeriode.Text = DrX("Pourcentage").ToString
            TxtValActu.Text = DrX("Valeur").ToString
            TxtValPrecede.Text = ValPrecedens.ToString

            GbRapport.Text = "RAPPORT N° " & DrX("Numéro").ToString

            datemodifs.Text = DrX("DateAjoutRapport").ToString
            PoutagActuel.Text = DrX("Pourcentage").ToString

            TxtTitre.Enabled = True
            TxtDetail.Enabled = True
            TxtProbleme.Enabled = True
            TxtSolution.Enabled = True
            TxtConclusion.Enabled = True
            TxtSolution.Enabled = True
            TxtValActu.Enabled = True

            GridPieceJ.DataSource = dtPieces
            dtPieces.Rows.clear()

            ChargerPJ(DrX("Numéro").ToString)
            BtSelectPJ.Enabled = True
        End If
    End Sub

    'Private Sub SupprimerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
    '    If (ViewRapPrec.RowCount > 0) Then
    '        If ConfirmMsg("Voulez-vous vraiment supprimé ce rapport ?") = DialogResult.Yes Then
    '            DrX = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)

    '            query = "DELETE from T_PieceJointe where NumRapport='" & DrX("Numéro").ToString & "'"
    '            ExecuteNonQuery(query)

    '            query = "DELETE FROM T_RapportActivites WHERE NumRapport = '" & DrX("Numéro").ToString & "'"
    '            ExecuteNonQuery(query)

    '            Dim codeact() As String
    '            codeact = DrX("Numéro").ToString.Split("_")
    '            Dim prct As Double = 0
    '            query = "select ProgressionPartition from T_Partition where LibelleCourt='" & codeact(0).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '            prct = Val(ExecuteScallar(query))

    '            prct = prct - CDbl(DrX("Pourcentage").ToString)

    '            query = "update T_Partition set ProgressionPartition='" & prct.ToString & "' where LibelleCourt='" & codeact(0).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
    '            ExecuteNonQuery(query)

    '            query = "update t_rapportactivites set pourcentagetotal = pourcentagetotal - '" & CDbl(DrX("Pourcentage").ToString) & "' where NumRapport LIKE '" & codeact(0).ToString & "%' and DateAjout > '" & DrX("DateAjoutRapport").ToString & "'"
    '            ExecuteNonQuery(query)

    '            SuccesMsg("Suppression effectuée avec succès")
    '            ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle).Delete()

    '            Initialiser()
    '        End If
    '    End If
    'End Sub

    'donnees technique modif rapport
    Private Sub ModifLesDonneesTechniques()
        DrX = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)
        Dim LibIndic As String = ""
        query = "select CodePartition from T_RapportActivites where NumRapport= '" & DrX("Numéro").ToString & "'"
        Dim KodeParti As Decimal = Val(ExecuteScallar(query))

        query = "select B.LibelleIndicateur from T_IndicateurPartition as A,T_Indicateur as B where A.CodeIndicateur=B.CodeIndicateur and A.CodePartition='" & KodeParti.ToString & "'"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        For Each rw2 As DataRow In dt2.Rows
            LibIndic = MettreApost(rw2("LibelleIndicateur").ToString)
        Next
        TxtIndicateurPerform.Text = "Veuillez indiquer ci-dessous la valeur de l'avancement des travaux dépuis le début de l'activité en fonction de l'indicateur de performance :" & vbNewLine & "[ " & LibIndic & " ]"
    End Sub

    Private Sub OuvrirToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If (ViewRapPrec.RowCount > 0) Then
            DrX = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)

            NumRappCache.Text = DrX(1).ToString
            'LesDonneesTechniques()

            query = "select DtDebRapport, DtFinRapport, TypeRapport, NumRapport, TitreRapport, DetailRapport, PbRapport, SolutRapport, ConcluRapport, ValeurRealise, PourcentRealise from T_RapportActivites where NumRapport='" & DrX(1).ToString & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows

                DtDebPeriode.DateTime = CDate(rw("DtDebRapport").ToString).ToShortDateString
                DtFinPeriode.DateTime = CDate(rw("DtFinRapport").ToString).ToShortDateString

                RapportInterm.Checked = IIf(rw(1).ToString = "INTERMEDIAIRE", True, False)
                RapportAchev.Checked = Not (RapportInterm.Checked)
                BtNumRap.Text = DrX(1).ToString
                TxtTitre.Text = MettreApost(rw(3).ToString)
                TxtDetail.Text = MettreApost(rw(4).ToString)
                TxtProbleme.Text = MettreApost(rw(5).ToString)
                TxtSolution.Text = MettreApost(rw(6).ToString)
                TxtConclusion.Text = MettreApost(rw(7).ToString)
                PrctPeriode.Text = MettreApost(rw(9).ToString)
                TxtValActu.Text = MettreApost(rw(8).ToString)
                PrctActu.Text = CDbl(PrctActu.Text) - CDbl(PrctPeriode.Text)

            Next

            ChargerPJ(DrX(1).ToString)
            BtNouvRapport.Enabled = False
            BtAnnulerRapp.Enabled = True

        End If

    End Sub

    Private Sub PrctPeriode_TextChanged(sender As Object, e As System.EventArgs) Handles PrctPeriode.TextChanged
        If (PrctPeriode.Text > 0 And TxtValActu.Text <> "") Then
            BtEnrgRapport.Enabled = True
        Else
            BtEnrgRapport.Enabled = False
        End If

        Dim PrctRealise As Decimal = 0
        Dim codeact() As String
        codeact = CmbActivite.Text.Split(" : ")
        query = "select ProgressionPartition from T_Partition where LibelleCourt='" & codeact(0).ToString & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            PrctRealise = Val(rw("ProgressionPartition"))
        Next

        If BtEnrgRapport.Text = "Enregistrer" Then
            PrctActu.Text = CInt(PrctPeriode.Text) + CInt(PrctRealise)
        Else
            PrctActu.Text = CInt(PrctRealise)
        End If
    End Sub


    Private Sub ImprimerToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ImprimerToolStripMenuItem1.Click
        If Not Access_Btn("BtnPrintRappActiv") Then
            Exit Sub
        End If

        If (ViewRapPrec.RowCount > 0) Then
            DrX = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)
            Dim Chemin As String = lineEtat & "\"

            Dim report As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim DatSet = New DataSet

            report.Load(Chemin & "\RapportActivites\RapportActivite.rpt")

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

            Progression(90)
            report.SetDataSource(DatSet)
            report.SetParameterValue("NumRapport", DrX(1).ToString)
            report.SetParameterValue("Source", line, "PieceJointe.rpt")

            FullScreenReport.FullView.ReportSource = report
            FullScreenReport.ShowDialog()
        End If
    End Sub

    ' Private Sub ImprimerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImprimerToolStripMenuItem.Click

    '    If (ViewPieceJ.RowCount > 0) Then

    '        DrX = ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle)
    '        Dim monProcess As New Process()
    '        monProcess.StartInfo.FileName = DrX("Chemin").ToString
    '        monProcess.StartInfo.Verb = "Print"
    '        monProcess.StartInfo.CreateNoWindow = True
    '        Try
    '            If File.Exists(DrX("Chemin").ToString) Then
    '                monProcess.Start()
    '            Else
    '                FailMsg("Le fichier spécifié n'existe pas.")
    '            End If
    '        Catch ex As Exception
    '            monProcess = New Process()
    '            monProcess.StartInfo.FileName = DrX("Chemin").ToString
    '            If File.Exists(DrX("Chemin").ToString) Then
    '                monProcess.Start()
    '            Else
    '                FailMsg("Le fichier spécifié n'existe pas.")
    '            End If
    '        End Try
    '    End If
    'End Sub

    Private Sub SupprimerSToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SupprimerSToolStripMenuItem1.Click
        If ViewPieceJ.RowCount > 0 Then

            DrX = ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle)
            Dim CheminFile As String = DrX("Chemin").ToString
            Dim NomDossier As String = NumRappCache.Text.Replace("/", "_")
            NomDossier = line & "\Rapports\" & NomDossier

            query = "select count(*) from T_PieceJointe where ID_Pieces='" & DrX("ID_Piece").ToString & "'"
            Dim nbres As Integer = Val(ExecuteScallar(query))

            If nbres > 0 Then
                If (Directory.Exists(NomDossier)) Then
                    query = "DELETE from T_PieceJointe where ID_Pieces='" & DrX("ID_Piece").ToString & "' and  NumRapport='" & BtNumRap.Text & "'"
                    ExecuteNonQuery(query)
                    'ChargerPJ(BtNumRap.Text)
                End If
            End If
            ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle).Delete()
        End If
    End Sub
End Class