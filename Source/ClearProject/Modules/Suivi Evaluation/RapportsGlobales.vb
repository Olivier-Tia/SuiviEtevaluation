Imports MySql.Data.MySqlClient
Imports System.Math
Imports System.IO
Imports DevExpress.XtraRichEdit
Imports System.Windows.Forms.DataVisualization.Charting
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions
Imports CrystalDecisions.CrystalReports

Public Class RapportsGlobales

    Dim DateDuJour As Date = Now.ToShortDateString
    Dim BailDuJour As String = ""
    Dim MonnaieDuJour As String = ""
    Dim montTravaux As Decimal = 0
    Dim montFournitures As Decimal = 0
    Dim montConsultants As Decimal = 0
    Dim imprEnCours As Boolean = False
    Dim NumImprim As Decimal = 1
    Dim FondDecais As Decimal = 0
    Dim FondBaillr As Decimal = 0

    'Nom Utiliser
    Dim NomDossier As String = ""

    Dim NomDossierEnCours As String = ""
    'Dim CodeClassePartition As Integer = 0
    Dim FermerDossierEnCours As Boolean = False
    Dim Bailleur As String = "Tous les bailleurs"
    Dim NepasExecuteleMemeCode As Boolean = False

    'Variable contexMenuScript Rapports
    Dim LectureDossier As Boolean = False

    'Tableau contenant les % de realisation  RSF des activites ****************
    'Dim TablePctPhysi(300, 2) As String
    'Dim NbrsLigneTable As Integer = 0

    Private Sub RapportsGlobales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RibbonControl1.Minimized = True
        LoadRGLES()
    End Sub

    Private Sub RapportSurMarches_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub


#Region "Chargement des rapports precedent"

    Private Sub LoadRGLES()
        Dim dtArchives = New Data.DataTable()
        dtArchives.Columns.Clear()
        dtArchives.Columns.Add("Code", Type.GetType("System.String"))
        dtArchives.Columns.Add("Période", Type.GetType("System.String"))
        dtArchives.Columns.Add("Bailleur", Type.GetType("System.String"))
        dtArchives.Columns.Add("Edité le", Type.GetType("System.String"))
        dtArchives.Columns.Add("Edité par", Type.GetType("System.String"))
        dtArchives.Columns.Add("RapConsolider", Type.GetType("System.String"))
        dtArchives.Rows.Clear()

        Dim cptr As Decimal = 0
        query = "select R.PeriodeRGLS, R.DateEdition, R.Bailleur, R.OperateurEdition, R.RapConsolider, O.CiviliteOperateur, O.NomOperateur, O.PrenOperateur from t_rg_rapports_globals as R, T_Operateur as O where R.OperateurEdition=O.UtilOperateur and R.CodeProjet='" & ProjetEnCours & "' order by R.DateEdition desc"
        Dim dt As Data.DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            cptr += 1
            Dim drS = dtArchives.NewRow()
            drS("Code") = IIf((cptr Mod 2 = 0), "x", "").ToString
            drS("Période") = rw("PeriodeRGLS").ToString
            drS("Bailleur") = MettreApost(rw("Bailleur").ToString)
            drS("Edité le") = Mid(rw("DateEdition").ToString, 1, 10)
            drS("Edité par") = MettreApost(rw("CiviliteOperateur").ToString & " " & rw("NomOperateur").ToString & " " & rw("PrenOperateur").ToString)
            drS("RapConsolider") = rw("RapConsolider").ToString
            dtArchives.Rows.Add(drS)
        Next

        GridRapPrec.DataSource = dtArchives
        ViewRapPrec.Columns(0).Visible = False
        ViewRapPrec.Columns("RapConsolider").Visible = False
        ViewRapPrec.Columns(1).Width = 150
        ViewRapPrec.Columns(2).Width = 100
        ViewRapPrec.Columns(3).Width = 100
        ViewRapPrec.Columns(4).Width = 200
        ViewRapPrec.Columns(1).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewRapPrec.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        ViewRapPrec.Appearance.Row.Font = New System.Drawing.Font("Times New Roman", 10, FontStyle.Regular)
        ColorRowGrid(ViewRapPrec, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
    End Sub

#End Region

#Region "Code Non Utiliser"

    Private Sub GraphMontantDecaisse(ByVal Devise As String)
        Dim separDecim As String = ","
        If (Devise = "US$") Then separDecim = "."
        Dim MontTotal As String = ""

        ChartGraph1.ChartAreas.Clear()
        ChartGraph1.Series.Clear()
        ChartGraph1.Legends.Clear()

        Dim chartArea1 As New ChartArea()
        ChartGraph1.ChartAreas.Add(chartArea1)

        'Instanciation series sans Name
        Dim series1 As New Series("Decaisse")
        ' Add data points to the first series
        Dim Baille1 As String = ""

        query = "select MontApprouve,MontDecaisse,Bailleur from T_TampStatisticRSM"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Baille1 = MettreApost(rw(2).ToString).ToUpper
            Dim montApprouve As Decimal = CDec(rw(0).ToString.Replace(" ", "").Replace(".", ","))
            Dim montDecaisse As Decimal = CDec(rw(1).ToString.Replace(" ", "").Replace(".", ","))
            Dim prctAp As Decimal = 0
            If (montApprouve <> 0) Then Math.Round((montDecaisse * 100) / montApprouve, 3)
            series1.Points.AddXY(AfficherMonnaie(montApprouve - montDecaisse).Replace(",", separDecim) & " (" & Devise & ")" & vbNewLine & "(" & (100 - prctAp).ToString & "%)", montApprouve - montDecaisse)
            series1.Points.AddXY(rw(1).ToString & " (" & Devise & ")" & vbNewLine & "(" & prctAp.ToString & "%)", montDecaisse)
            MontTotal = rw(0).ToString
        Next

        series1.Points(0).Color = Color.LightBlue
        series1.Points(1).Color = Color.LightSalmon
        series1.Points(0).Font = New Font("Arial", 8, FontStyle.Bold)
        series1.Points(1).Font = New Font("Arial", 8, FontStyle.Bold)

        ' Add series to the chart
        series1.ChartArea = chartArea1.Name
        ChartGraph1.Series.Add(series1)

        ChartGraph1.BackImage = line & "\ClearpNew6.png"
        ChartGraph1.BackImageWrapMode = ChartImageWrapMode.Unscaled
        ChartGraph1.BackImageAlignment = ChartImageAlignmentStyle.BottomLeft

        ChartGraph1.Titles("Titre1").Text = "GRAPHIQUE DES MONTANTS APPROUVES DECAISSE ET NON DECAISSE " & Baille1 & " POUR UN MONTANT TOTAL DE" & vbNewLine & MontTotal & " " & Devise & "   DE MARCHES APPROUVES (à la date du " & Now.ToShortDateString & ")" & vbNewLine & " "
        ChartGraph1.Titles("Titre1").Position.Auto = True
        ChartGraph1.Titles("Titre1").DockedToChartArea = chartArea1.Name
        ChartGraph1.Titles("Titre1").Docking = Docking.Bottom
        ChartGraph1.Titles("Titre1").IsDockedInsideChartArea = False
        ChartGraph1.Titles("Titre1").Font = New Font("Arial", 10, FontStyle.Bold)

        Dim legend1 As New Legend
        legend1.Name = "Leg1"
        legend1.Title = "Légende"
        ChartGraph1.Legends.Add(legend1)
        ChartGraph1.Series(0).IsVisibleInLegend = False
        ChartGraph1.Legends("Leg1").CustomItems.Clear()
        ChartGraph1.Legends("Leg1").CustomItems.Add(New LegendItem("Montant approuvé non encore décaissé", Color.LightBlue, ""))
        ChartGraph1.Legends("Leg1").CustomItems.Add(New LegendItem("Montant approuvé décaissé à ce jour", Color.LightSalmon, ""))

        '*************************************************************************************
        ChartGraph1.ChartAreas(0).InnerPlotPosition.Auto = False
        ChartGraph1.ChartAreas(0).InnerPlotPosition.Height = 100
        ChartGraph1.ChartAreas(0).InnerPlotPosition.Width = 95

        ChartGraph1.ChartAreas(0).Position.Height = 85
        ChartGraph1.ChartAreas(0).Position.Width = 100

        ChartGraph1.ChartAreas("ChartArea1").BackColor = Color.Transparent
        ChartGraph1.ChartAreas("ChartArea1").BackSecondaryColor = Color.Transparent
        '******************************************************************************************
        ChartGraph1.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
        ChartGraph1.Series(0).ChartType = SeriesChartType.Pie
        ChartGraph1.ChartAreas("ChartArea1").Area3DStyle.LightStyle = LightStyle.Realistic
        ChartGraph1.ChartAreas(0).Area3DStyle.PointDepth = 40
    End Sub

    Private Sub BtImprimerRapp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImprimerRapp.Click

        DebutChargement()

        Dim BaillRapp As String = "DU PROJET"
        If (BailDuJour <> "Tous") Then
            BaillRapp = "DE L'" & BailDuJour
        End If


        imprEnCours = True
        Dim Chap() As String = {"", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X"}
        Dim SommaireRapport As String = ""
        Dim NbElt As Decimal = 1
        Dim NbTot As Decimal = 0

        'If (ChkDetails.Checked = True) Then NbElt = NbElt + 1
        'If (ChkStatistic.Checked = True) Then NbElt = NbElt + 1
        'If (ChkSituation.Checked = True) Then NbElt = NbElt + 1
        'If (ChkAttente.Checked = True) Then NbElt = NbElt + 1
        'If (ChkExecution.Checked = True) Then NbElt = NbElt + 1
        'If (ChkDAO.Checked = True) Then NbElt = NbElt + 1
        'NbTot = NbElt

        'Impression DAO édités
        ' If (ChkDAO.Checked = True) Then
        NbElt = NbElt - 1
        SommaireRapport = "Chapitre " & Chap(NbElt) & "./" & vbTab & "Listes des DAO et DP" & vbNewLine & SommaireRapport

        ' ChkDAO_CheckedChanged(Me, e)

        printTitre(Chap(NbElt), "LISTES DES DAO ET DP " & BaillRapp)
        ' End If
        '***********************************

        'Impression Execution marchés
        ' If (ChkExecution.Checked = True) Then
        NbElt = NbElt - 1
        SommaireRapport = "Chapitre " & Chap(NbElt) & "./" & vbTab & "Situation financière de l'exécution des marchés attribués" & vbNewLine & SommaireRapport
        'ChkExecution_CheckedChanged(Me, e)
        printTitre(Chap(NbElt), "SITUATION FINANCIERE DE L'EXECUTION DES MARCHES ATTRIBUES " & BaillRapp)
        ' End If
        '***********************************

        'Impression marchés en attente
        ' If (ChkAttente.Checked = True) Then
        NbElt = NbElt - 1
        SommaireRapport = "Chapitre " & Chap(NbElt) & "./" & vbTab & "Marchés non planifiés et non débutés" & vbNewLine & SommaireRapport

        printTitre(Chap(NbElt), "MARCHES NON PLANIFIES ET NON DEBUTES " & BaillRapp)
        ' End If
        '***********************************

        'Impression Situation des marchés
        '  If (ChkSituation.Checked = True) Then
        NbElt = NbElt - 1
        SommaireRapport = "Chapitre " & Chap(NbElt) & "./" & vbTab & "Suivi du processus de passation de marchés" & vbNewLine & SommaireRapport
        'ChkSituation_CheckedChanged(Me, e)
        printTitre(Chap(NbElt), "SUIVI DU PROCESSUS DE PASSATION DE MARCHES " & BaillRapp)
        ' End If
        '***********************************

        'Impression Statistiques des marchés
        ' If (ChkStatistic.Checked = True) Then
        NbElt = NbElt - 1
        SommaireRapport = "Chapitre " & Chap(NbElt) & "./" & vbTab & "Suivi des marchés et engagements" & vbNewLine & SommaireRapport

        If (ChkPassation.Checked = True) Then

            Dim PaysageDoc As New PageSetupDialog()
            '    If (ChkGraph8.Checked = True) Then
            PaysageDoc.Document = ChartGraph8.Printing.PrintDocument
            PaysageDoc.PageSettings.Landscape = True   'mettre en paysage
            ChartGraph8.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
            NumImprim = NumImprim + 1
            ChartGraph8.Printing.Print(False)
            '   End If
            '   If (ChkGraph7.Checked = True) Then
            PaysageDoc.Document = ChartGraph7.Printing.PrintDocument
            PaysageDoc.PageSettings.Landscape = True   'mettre en paysage
            ChartGraph7.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
            NumImprim = NumImprim + 1
            ChartGraph7.Printing.Print(False)
        End If
        ' If (ChkGraph6.Checked = True) Then
        'PaysageDoc.Document = ChartGraph6.Printing.PrintDocument
        ' PaysageDoc.PageSettings.Landscape = True   'mettre en paysage
        ChartGraph6.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
        NumImprim = NumImprim + 1
        ChartGraph6.Printing.Print(False)
        ' End If
        'If (ChkGraph5.Checked = True) Then
        'ageDoc.Document = ChartGraph5.Printing.PrintDocument
        'PaysageDoc.PageSettings.Landscape = True
        ChartGraph5.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
        NumImprim = NumImprim + 1
        ChartGraph5.Printing.Print(False)
        '  End If
        'If (ChkGraph4.Checked = True) Then
        'ageDoc.Document = ChartGraph4.Printing.PrintDocument
        ' PaysageDoc.PageSettings.Landscape = True
        ChartGraph4.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
        NumImprim = NumImprim + 1
        ChartGraph4.Printing.Print(False)
        '    End If
        'If (ChkGraph3.Checked = True) Then
        'PaysageDoc.Document = ChartGraph3.Printing.PrintDocument
        'PaysageDoc.PageSettings.Landscape = True
        ChartGraph3.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
        NumImprim = NumImprim + 1
        ChartGraph3.Printing.Print(False)
        '    End If
        'If (ChkGraph2.Checked = True) Then
        'aysageDoc.Document = ChartGraph2.Printing.PrintDocument
        'aysageDoc.PageSettings.Landscape = True
        ChartGraph2.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
        NumImprim = NumImprim + 1
        ChartGraph2.Printing.Print(False)
        '  End If
        '  If (ChkGraph1.Checked = True) Then
        '  PaysageDoc.Document = ChartGraph1.Printing.PrintDocument
        '  PaysageDoc.PageSettings.Landscape = True
        ChartGraph1.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
        NumImprim = NumImprim + 1
        ChartGraph1.Printing.Print(False)
        '      End If
        'If (ChkGraph0.Checked = True) Then
        ' PaysageDoc.Document = ChartGraph0.Printing.PrintDocument
        '  PaysageDoc.PageSettings.Landscape = True
        ChartGraph0.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
        NumImprim = NumImprim + 1
        ChartGraph0.Printing.Print(False)
        '    End If

        '    End If

        printTitre(Chap(NbElt), "SUIVI DES MARCHES ET ENGAGEMENTS " & BaillRapp)
        '  End If
        '***********************************

        'Impression Détails rapport
        '   If (ChkDetails.Checked = True) Then
        NbElt = NbElt - 1
        SommaireRapport = "Chapitre " & Chap(NbElt) & "./    " & "Informations sur le projet" & vbNewLine & SommaireRapport
        SaisieDetailsRapp.SaveDocument(NomDossier & "\R" & NumImprim.ToString & ".pdf", DevExpress.XtraRichEdit.DocumentFormat.Doc)
        NumImprim = NumImprim + 1
        SaisieDetailsRapp.Print()
        '  End If
        '***********************************

        'Impression Inforrmations projet

        printPageInfosProjet()
        'printTitre(Chap(NbElt), "INFORMATIONS SUR LE PROJET")
        '*******************************

        'Impression page de garde
        If (NbTot > 1) Then
            'ChargerDoc("PageGarde.rtf")

            Dim TypRapport As String = ""
            If (BailDuJour = "Tous") Then
                TypRapport = "RAPPORT COMPLET"
            Else
                TypRapport = "RAPPORT SUR LA PART " & BailDuJour
            End If

            'Impression sommaire
            Dim Chemin As String = lineEtat & "\RSM\"
            Dim report As New ReportDocument

            report.Load(Chemin & "PageSommaire.rpt")
            report.SetParameterValue("Sommaire", SommaireRapport)
            report.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\R" & NumImprim.ToString & ".pdf")
            NumImprim = NumImprim + 1
            report.PrintToPrinter(1, True, 0, 0)
            '**************************

            printGarde(TypRapport)
        End If

        '********************************************

        'MsgBox("Impression terminée", MsgBoxStyle.Information)
        imprEnCours = False
        NumImprim = 1

        FinChargement()
    End Sub

    Private Sub printPageInfosProjet()

        Dim nomProjet As String = ""
        Dim paysProjet As String = ""
        Dim benefProjet As String = ""
        Dim typeConv As String = ""
        Dim numConv As String = ""
        Dim numProjet As String = ""
        Dim dateDebut As String = ""
        Dim dateFin As String = ""
        Dim montChiffre As Decimal = 0

        query = "select NomProjet,PaysProjet,IdentifiantProjet,DateDebutProjetMV,DateFinProjetMV from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            nomProjet = MettreApost(rw(0).ToString) & " (" & ProjetEnCours & ")"
            paysProjet = MettreApost(rw(1).ToString)
            numProjet = rw(2).ToString
            dateDebut = rw(3).ToString
            dateFin = rw(4).ToString
        Next

        'Montant du projet
        query = "select C.MontantConvention from T_Bailleur as B,T_Convention as C where B.CodeBailleur=C.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows
            montChiffre = montChiffre + CDec(rw1(0))
        Next

        'Taux de la devise
        Dim separDecim As String = ","
        Dim NbVirg As Integer = 0
        If (MonnaieDuJour <> "FCFA") Then NbVirg = 2
        Dim tauxConv As Decimal = 1
        Dim libDevise As String = ""
        If (MonnaieDuJour = "US$") Then separDecim = "."

        query = "select TauxDevise,LibelleDevise from T_Devise where AbregeDevise='" & MonnaieDuJour & "'"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        For Each rw2 As DataRow In dt2.Rows
            tauxConv = CDec(rw2(0))
            libDevise = rw2(1).ToString
        Next

        montChiffre = Math.Round(montChiffre / tauxConv, NbVirg)

        'Infos convention
        query = "select C.CodeConvention,C.TypeConvention,C.Beneficiaire from T_Bailleur as B,T_Convention as C where B.CodeBailleur=C.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "' and B.InitialeBailleur<>'ETAT'"
        Dim dt3 As DataTable = ExcecuteSelectQuery(query)
        For Each rw3 As DataRow In dt3.Rows
            numConv = rw3(0).ToString
            typeConv = rw3(1).ToString
            benefProjet = "Etat de " & MettreApost(rw3(2).ToString)
        Next

        Dim monEnLettre As String = ""
        Dim partMont() As String = montChiffre.ToString.Split(","c)
        monEnLettre = MontantLettre(partMont(0)).Replace(" zero", "")

        If (Mid(monEnLettre, (monEnLettre.Length - 8)) = "milliards" Or Mid(monEnLettre, (monEnLettre.Length - 7)) = "milliard" Or Mid(monEnLettre, (monEnLettre.Length - 7)) = "millions" Or Mid(monEnLettre, (monEnLettre.Length - 6)) = "million") Then
            If (Mid(libDevise, 1, 1).ToLower = "a" Or Mid(libDevise, 1, 1).ToLower = "e" Or Mid(libDevise, 1, 1).ToLower = "i" Or Mid(libDevise, 1, 1).ToLower = "o" Or Mid(libDevise, 1, 1).ToLower = "u") Then
                monEnLettre = monEnLettre & " d'"
            Else
                monEnLettre = monEnLettre & " de"
            End If
        End If
        monEnLettre = monEnLettre & " " & libDevise & "s"

        If (partMont.Length > 1) Then
            monEnLettre = monEnLettre & " et " & MontantLettre(partMont(1)).Replace(" zero", "") & " Centimes"
        End If

        Dim Chemin As String = lineEtat & "\RSM\"
        Dim report As New ReportDocument

        report.Load(Chemin & "PageInfosProjet.rpt")
        report.SetParameterValue("Pays", paysProjet)
        report.SetParameterValue("Beneficiaire", benefProjet)
        report.SetParameterValue("NomProjet", nomProjet)
        report.SetParameterValue("TypeConvention", typeConv)
        report.SetParameterValue("NumConvention", numConv)
        report.SetParameterValue("NumProjet", numProjet)
        report.SetParameterValue("DateDebut", Mid(dateDebut, 1, 2) & " " & CDate(dateDebut).ToString("MMMM").ToUpper & " " & Mid(dateDebut, 7))
        report.SetParameterValue("DateFin", Mid(dateFin, 1, 2) & " " & CDate(dateFin).ToString("MMMM").ToUpper & " " & Mid(dateFin, 7))
        report.SetParameterValue("CodeProjet", ProjetEnCours)
        report.SetParameterValue("MontantChiffre", AfficherMonnaie(montChiffre.ToString).Replace(",", separDecim) & " " & MonnaieDuJour)
        report.SetParameterValue("MontantLettre", monEnLettre)
        report.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\R" & NumImprim.ToString & ".pdf")
        NumImprim = NumImprim + 1
        report.PrintToPrinter(1, True, 0, 0)

    End Sub

    Private Sub printTitre(ByVal Num As String, ByVal Titre As String)

        Dim report As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table
        Dim Chemin As String = lineEtat & "\RSM\"
        report.Load(Chemin & "PageChapitre.rpt")

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


        report.SetParameterValue("NumChapitre", Num)
        report.SetParameterValue("TitreChapitre", Titre)
        report.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\R" & NumImprim.ToString & ".pdf")
        NumImprim = NumImprim + 1
        report.PrintToPrinter(1, True, 0, 0)

    End Sub


    Private Sub printGarde(ByVal typeRapp As String)
        Dim EnteteR As String = ""
        query = "select NomProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            EnteteR = MettreApost(rw(0).ToString).ToUpper
        Next

        Dim Chemin As String = lineEtat & "\RSM\"
        Dim report As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table


        report.Load(Chemin & "PageGarde.rpt")

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

        report.SetParameterValue("NomProjet", EnteteR)
        report.SetParameterValue("TypeRapport", typeRapp)
        report.SetParameterValue("Monnaie", MonnaieDuJour)
        report.SetParameterValue("CodeProjet", ProjetEnCours)
        report.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\R" & NumImprim.ToString & ".pdf")
        NumImprim = NumImprim + 1
        report.PrintToPrinter(1, True, 0, 0)

    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        Me.Close()
    End Sub

    Private Sub XtraTabControl1_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        If (XtraTabControl1.SelectedTabPage Is PageIntroduction) Then
            'RibbonControl1.Minimized = False
            'RibbonControl1.SelectedPage = HomeRibbonPage1
        Else
            'RibbonControl1.Minimized = True
        End If
    End Sub

#End Region

#Region "Code Initialisation des données"

    Private Sub FermerBouton(ByVal value As Boolean)
        DtDebPeriode.Enabled = value
        DtFinPeriode.Enabled = value
        ' CheckCompo.Enabled = value
        ' CheckSousCompo.Enabled = value
        ' CheckActivite.Enabled = value
    End Sub

    Private Sub InitialiserCaseaCocher(ByVal value As Boolean)
        XtraTabControl1.SelectedTabPage = PageMatiere
        ViewMatiere.ReportSource = Nothing
        ViewMatiere.Refresh()

        CheckSectionA.Checked = value
        ChkDonneFinanc.Checked = value
        CheckSectionB.Checked = value
        CheckIntroduc.Checked = value
        ChkExamenResultat.Checked = value

        ChkSuiviFinanc.Checked = value
        ChkPassation.Checked = value
        ChkSaveMesure.Checked = value
        ChkProjet.Checked = value

        'ChkSuiviFinanc.Enabled = value
        ChkPassation.Enabled = value
        ChkSaveMesure.Enabled = value
        ChkProjet.Enabled = value

        CheckChrono.Checked = value
        CheckAnnexe.Checked = value
        CheckAnnexe1.Checked = value
        CheckAnnexe1.Checked = value

        CheckAnnexe1.Enabled = value
        CheckAnnexe1.Enabled = value
    End Sub

#End Region

#Region "Bt Enregistrement Commentaite et Introduction"

    Private Sub BtEnrgRapp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgRapp.Click
        Dim Enregistrer As Boolean = False
        DebutChargement()

        If PageCommentaire.PageVisible = True Then
            If ViewComm.RowCount > 0 Then
                For i = 0 To ViewComm.RowCount - 1
                    If ViewComm.GetRowCellValue(i, "TypeActivite").ToString = "A" And ViewComm.GetRowCellValue(i, "% Execution physique").ToString <> ViewComm.GetRowCellValue(i, "% Execution financier").ToString And ViewComm.GetRowCellValue(i, "Observation/Commentaires").ToString = "" Then
                        Enregistrer = True
                        Exit For
                    End If
                Next

                If Enregistrer = True Then
                    SuccesMsg("Vueillez saisir tous les commentaires")
                    FinChargement()
                    Exit Sub
                End If

                For i = 0 To ViewComm.RowCount - 1
                    If ViewComm.GetRowCellValue(i, "TypeActivite").ToString = "A" Then
                        query = "Update t_rg_Commentaire set Obervation='" & EnleverApost(ViewComm.GetRowCellValue(i, "Observation/Commentaires").ToString) & "' where RefTamp='" & ViewComm.GetRowCellValue(i, "RefTamp").ToString & "'"
                        ExecuteNonQuery(query)
                        Enregistrer = True
                    End If
                Next
            End If
        End If

        If PageIntroduction.PageVisible = True Then
            If SaisieDetailsRapp.Text.Trim = "" Then
                SuccesMsg("Veuillez saisir les detailes du rapports")
                SaisieDetailsRapp.Focus()
                FinChargement()
                Exit Sub
            End If

            query = "select CheminDetails from t_rg_details where CodeProjet='" & ProjetEnCours & "'"
            Dim CheminDetails As String = MettreApost(ExecuteScallar(query))

            Dim NomDossier As String = line & "\RapportsGlobals\" & ProjetEnCours & "\DétailsRapport.docx"

            If CheminDetails.ToString = "" Then
                query = "INSERT INTO t_rg_details VALUES(NULL,'" & NomDossier.ToString.Replace("\", "\\") & "','" & ProjetEnCours & "','" & EnleverApost(SaisieDetailsRapp.Text) & "')"
                ExecuteNonQuery(query)
            Else
                query = "UPDATE t_rg_details SET Details='" & EnleverApost(SaisieDetailsRapp.Text) & "' where CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            NomDossier = line & "\RapportsGlobals\" & ProjetEnCours
            If (Directory.Exists(NomDossier) = False) Then
                Directory.CreateDirectory(NomDossier)
            End If

            SaisieDetailsRapp.SaveDocument(NomDossier & "\DétailsRapport.docx", DocumentFormat.OpenXml)
            Enregistrer = True
        End If

        If Enregistrer = True Then
            SuccesMsg("Enregistrement effectié avec succès")
        End If
        FinChargement()
    End Sub
#End Region

#Region "Bouton Nouveau rapports"

    Private Sub DtDebPeriode_DateTimeChanged(sender As Object, e As EventArgs) Handles DtDebPeriode.DateTimeChanged, DtFinPeriode.DateTimeChanged
        GetNewRapports()
    End Sub

    'Private Sub CheckCompo_CheckedChanged(sender As Object, e As EventArgs) Handles CheckCompo.CheckedChanged, CheckSousCompo.CheckedChanged, CheckActivite.CheckedChanged
    '    GetNewRapports()
    'End Sub

    Private Sub GetNewRapports()
        If IsDate(DtDebPeriode.Text) And IsDate(DtFinPeriode.Text) Then
            BtNewRapports.Enabled = True
        Else
            BtNewRapports.Enabled = False
        End If

        'If ((CheckCompo.Checked = True) Or (CheckSousCompo.Checked = True) Or (CheckActivite.Checked = True)) And IsDate(DtDebPeriode.Text) And IsDate(DtFinPeriode.Text) Then
        '    BtNewRapports.Enabled = True
        'Else
        '    BtNewRapports.Enabled = False
        'End If
    End Sub

    Private Sub BtNewRapports_Click(sender As Object, e As EventArgs) Handles BtNewRapports.Click
        Dim LeTexte As String() = BtNewRapports.Text.Split(" "c)

        If LeTexte(0).ToString = "Nouveau" Then

            If DateTime.Compare(DtDebPeriode.DateTime.ToShortDateString, DtFinPeriode.DateTime.ToShortDateString) > 0 Then
                SuccesMsg("La période est incorrecte")
                Exit Sub
            End If

            query = "select count(*) from t_rg_rapports_globals where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
            If Val(ExecuteScallar(query)) > 0 Then
                SuccesMsg("Un rapport a déjà été établi sur cette période.")
                Exit Sub
            End If

            'Nouveau rapport
            FermerDossierEnCours = False
            DebutChargement(True, "Récupération des informations de la période en cours...")

            Dim NomRepertoire As String = DtDebPeriode.DateTime.ToShortDateString & "_" & DtFinPeriode.DateTime.ToShortDateString
            NomDossierEnCours = line & "\RapportsGlobals\" & FormatFileName(NomRepertoire.ToString.Trim, "") & " - " & Bailleur

            If (Directory.Exists(NomDossierEnCours) = False) Then
                Directory.CreateDirectory(NomDossierEnCours)
            End If

            If (Directory.Exists(NomDossierEnCours & "\PJ") = False) Then
                Directory.CreateDirectory(NomDossierEnCours & "\PJ")
            End If

            'If CheckCompo.Checked = True Then
            '    CodeClassePartition = 1
            'ElseIf CheckSousCompo.Checked = True Then
            '    CodeClassePartition = 2
            'ElseIf CheckActivite.Checked = True Then
            '    CodeClassePartition = 5
            'End If

            'Ouverture du dossier RGLS **************************
            Dim PeriodeRGLS As String = DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString
            Dim DateEdition As String = Now.ToShortDateString & " " & Now.ToLongTimeString

            query = "INSERT INTO t_rg_rapports_globals(PeriodeRGLS,DateEdition,OperateurEdition,RapportSuiviFinancier,DateModif,CodeProjet,Bailleur,RapConsolider) VALUES('" & PeriodeRGLS & "','" & DateEdition & "','" & CodeUtilisateur & "','OUI','" & DateEdition & "', '" & ProjetEnCours & "','Tous les bailleurs','N')"
            ExecuteNonQuery(query)

            LoadRGLES()

            BtNewRapports.Text = "Fermer le dossier du " & DtDebPeriode.DateTime.ToShortDateString & " au " & DtFinPeriode.DateTime.ToShortDateString & "."
            FermerBouton(False)

            Panel4PlanRapport.Enabled = True
            FinChargement()

            LectureDossier = False
            ChkSuiviFinanc.Checked = True
            SaisieDetailsRapp.ReadOnly = False

        ElseIf LeTexte(0).ToString = "Dossier" Or LeTexte(0).ToString = "Fermer" Then

            FermerDossierEnCours = True
            InitialiserCaseaCocher(False)

            FermerBouton(True)
            Panel4PlanRapport.Enabled = False

            DtDebPeriode.EditValue = Nothing
            DtFinPeriode.EditValue = Nothing

            BtNewRapports.Text = "Nouveau rapport"
            NomDossierEnCours = ""
            LectureDossier = False
        End If

    End Sub

#End Region

#Region "Table de matiere"
    Private Sub EnregistrerTableMatiere()

        If (DtDebPeriode.Text <> "" And DtFinPeriode.Text <> "") Then

            Dim LeTitre As String = ""
            query = "DELETE from t_rg_tabledesmatiere"
            ExecuteNonQuery(query)

            Dim Code() As String = {"I", "II", "III", "IV", "V"}

            Dim Index1 As Integer = 0
            Dim Index2 As Integer = 1
            Dim NumOrdre As String = ""

            For i As Integer = 0 To 12
                If (i = 0) Then LeTitre = "SECTION A : IDENTIFICATION"
                If (i = 1) Then LeTitre = "LES DONNEES FINANCIERES"
                If (i = 2) Then LeTitre = "SECTION B : ANALYSE DE L'ETAT D’AVANCEMENT DU PROJET"
                If (i = 3) Then LeTitre = "  INTRODUCTION"
                If (i = 4) Then LeTitre = "EXAMEN DE L'AVANCEMENT ET RESULTATS"
                If (i = 5) Then LeTitre = "Rapport sur le suivi financier"
                If (i = 6) Then LeTitre = "Rapport sur la passation des marchés et l'avancement des actives"
                If (i = 7) Then LeTitre = "Rapport sur les mesures de sauvegarde environnementale et sociale"
                If (i = 8) Then LeTitre = "Rapport sur le suivi et l'évaluation du projet"
                If (i = 9) Then LeTitre = "LE PLAN TRIMESTRIEL D'ACTIONS ET CHRONOGRAMME MIS A JOUR"
                If (i = 10) Then LeTitre = "ANNEXES"
                If (i = 11) Then LeTitre = "ANNEXE 1 : Tableau sur l'origine et l’emploi des fonds"
                If (i = 12) Then LeTitre = "ANNEXE 2 : Rapprochement bancaire"

                If ((CheckSectionA.Checked = True And i = 0) Or (ChkDonneFinanc.Checked = True And i = 1) Or (CheckSectionB.Checked = True And i = 2) Or (CheckIntroduc.Checked = True And i = 3) Or (ChkExamenResultat.Checked = True And i = 4) Or (ChkSuiviFinanc.Checked = True And i = 5) Or (ChkPassation.Checked = True And i = 6) Or (ChkSaveMesure.Checked = True And i = 7) Or (ChkProjet.Checked = True And i = 8) Or (CheckChrono.Checked = True And i = 9) Or (CheckAnnexe.Checked = True And i = 10) Or (CheckAnnexe1.Checked = True And i = 11) Or (CheckAnnexe2.Checked = True And i = 12)) Then

                    If i = 0 Or i = 2 Or i = 3 Or i = 10 Or i = 11 Or i = 12 Then
                        NumOrdre = ""
                    ElseIf i = 1 Or i = 4 Or i = 9 Then
                        NumOrdre = Code(Index1) & ".   "
                        Index1 += 1
                    ElseIf i = 5 Or i = 6 Or i = 7 Or i = 8 Then
                        NumOrdre = "   " & Index2 & ".   "
                        Index2 += 1
                    End If

                    query = "INSERT INTO t_rg_tabledesmatiere VALUES(NULL,'" & NumOrdre.ToString & "','" & NumOrdre.ToString & EnleverApost(LeTitre.ToString) & "')"
                    ExecuteNonQuery(query)
                End If
            Next

            Dim LibelleRSF As String = ""
            query = "select NomProjet,PaysProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt.Rows
                LibelleRSF = "RAPPORTS GLOBAL DU  " & DtDebPeriode.DateTime.ToShortDateString & "  au  " & DtFinPeriode.DateTime.ToShortDateString & vbNewLine
                LibelleRSF = LibelleRSF & "----------------------------------------" & vbNewLine
                LibelleRSF = LibelleRSF & rw("PaysProjet").ToString & " - " & rw("NomProjet").ToString
                LibelleRSF = MettreApost(LibelleRSF).ToUpper()
            Next

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Engine.Tables
            Dim CrTable As Engine.Table
            Dim Chemin As String = lineEtat & "\RapportsGlobals\"
            Dim report As New Engine.ReportDocument
            Dim DatSet As New DataSet

            report.Load(Chemin & "TableDesMatiereRapportGlobal.rpt")

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

            Dim NomRepertoire As String = DtDebPeriode.DateTime.ToShortDateString & "_" & DtFinPeriode.DateTime.ToShortDateString
            NomRepertoire = line & "\RapportsGlobals\" & FormatFileName(NomRepertoire.ToString.Trim, "") & " - " & Bailleur

            'NomRepertoire = line & "\RapportsGlobals\" & NomRepertoire

            If Not Directory.Exists(NomRepertoire) Then
                Directory.CreateDirectory(NomRepertoire)
            End If

            report.ExportToDisk(ExportFormatType.PortableDocFormat, NomRepertoire & "\TableMatiere.pdf")
            ViewMatiere.ReportSource = report
        End If
    End Sub

#End Region

#Region "Section A: Identification"

    Private Sub CheckSectionA_CheckedChanged(sender As Object, e As EventArgs) Handles CheckSectionA.CheckedChanged

        If FermerDossierEnCours = False Then
            Dim PeriodeRapports = DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString

            If CheckSectionA.Checked = True Then
                ' Dialog_form(PersonenChargeProjet)
                query = "Update t_rg_rapports_globals set Identification='OUI' where PeriodeRGLS='" & PeriodeRapports & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                DonneesIdentification()

                PageSectionA.PageVisible = True
                XtraTabControl1.SelectedTabPage = PageSectionA
                'FinChargement()
            Else
                PageSectionA.PageVisible = False
                query = "Update t_rg_rapports_globals set Identification='NON' where PeriodeRGLS='" & PeriodeRapports & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                query = "delete from T_rg_Identification where PeriodeRGLS='" & PeriodeRapports & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            EnregistrerTableMatiere()
        Else
            PageSectionA.PageVisible = False
            ' ViewIdentifications = Nothing
            ' ViewIdentification.Refresh()
        End If
    End Sub

    Private Sub DonneesIdentification()
        Try
            Dim periodeRapport As String = DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString

            ' si c'est pas pour ouvrir le rapport

            If NepasExecuteleMemeCode = False Then

                'Saisie des personnes en charge du projet
                Dialog_form(PersonenChargeProjet)

                DebutChargement(True, "Chargement des données lié à l'identification...")

                ' Dim IdentificationProjet As String = GetID_Projet()
                Dim MontantComposante As Decimal = 0
                Dim TauxComposante As Decimal = 0

                Dim MontantEuro As Decimal = 0
                Dim TauxEuro As Decimal = Val(ExecuteScallar("SELECT TauxDevise FROM t_devise WHERE LibelleDevise='Euro' and AbregeDevise='€'"))

                Dim MontantDollar As Decimal = 0
                Dim TauxDolar As Decimal = Val(ExecuteScallar("SELECT TauxDevise FROM t_devise WHERE LibelleDevise='Dollar' and AbregeDevise='US$'"))
                Dim Taux As Decimal = 0

                'Verifer si les donneés d'identification existe
                query = "SELECT COUNT(*) from T_rg_Identification where PeriodeRGLS='" & periodeRapport & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim ExisteIdentification As Decimal = Val(ExecuteScallar(query))

                ' query = "SELECT CodeConvention, TypeConvention, MontantConvention, DateSignature, EntreeEnVigueur, DateCloture, CodeBailleur FROM t_convention"

                query = "SELECT CodeConvention, MontantConvention, CodeBailleur FROM t_convention ORDER BY CodeBailleur ASC"
                Dim dt As DataTable = ExcecuteSelectQuery(query)

                For Each rwConvention In dt.Rows

                    query = "SELECT DISTINCT LibelleCourt, LibellePartition, codepartition FROM t_partition WHERE CodeClassePartition='1' and CodeProjet='" & ProjetEnCours & "' ORDER BY LibelleCourt ASC"
                    Dim dt0 As DataTable = ExcecuteSelectQuery(query)

                    For Each rwComposante As DataRow In dt0.Rows

                        query = "SELECT SUM(R.MontantBailleur) FROM t_partition as P, t_besoinpartition as B, t_repartitionparbailleur as R WHERE P.CodePartition=B.CodePartition and B.RefBesoinPartition=R.RefBesoinPartition and P.LibelleCourt LIKE '" & rwComposante("LibelleCourt") & "%' and P.CodeClassePartition='5' and P.CodeProjet='" & ProjetEnCours & "' and R.CodeConvention ='" & rwConvention("CodeConvention") & "' And R.CodeBailleur ='" & rwConvention("CodeBailleur") & "'"

                        MontantComposante = Val(ExecuteScallar(query))

                        If rwConvention("MontantConvention") > 0 Then
                            Taux = Round(CDec((MontantComposante * 100) / rwConvention("MontantConvention")), 2)
                            TauxComposante = IIf(Taux <= 0, Round(CDec((MontantComposante * 100) / rwConvention("MontantConvention")), 4), Taux).ToString
                        Else
                            TauxComposante = 0
                        End If

                        'TauxComposante = IIf(rwConvention("MontantConvention") > 0, Round((MontantComposante * 100) / rwConvention("MontantConvention"), 2), 0).ToString

                        MontantEuro = Round(CDec(rwConvention("MontantConvention")) / TauxEuro)
                        MontantDollar = Round(CDec(rwConvention("MontantConvention")) / TauxDolar)

                        If ExisteIdentification = 0 Then
                            query = "INSERT INTO T_rg_Identification values(NULL, '" & rwConvention("CodeConvention") & "', '" & rwComposante("Codepartition") & "', '" & rwComposante("LibelleCourt") & "','" & MontantComposante.ToString.Replace(",", ".") & "', '" & TauxComposante & "', '" & MontantEuro.ToString.Replace(",", ".") & "','" & MontantDollar.ToString.Replace(",", ".") & "','" & periodeRapport & "','" & ProjetEnCours & "')"
                            ExecuteNonQuery(query)
                        End If
                    Next
                Next

            End If


            ' Affichage de l'Etat *******************************************************

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\RapportsGlobals\"
            Dim report As New ReportDocument
            Dim DatSet As New DataSet

            report.Load(Chemin & "SectionA_Identification.rpt")

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
            report.SetParameterValue("Période", periodeRapport)
            report.SetParameterValue("CodeProjet", ProjetEnCours, "SagenceExecutionProjet.rpt")
            report.SetParameterValue("CodeProjet", ProjetEnCours, "SPersonenChargeProjet.rpt")

            Dim NomRepertoire As String = DtDebPeriode.DateTime.ToShortDateString & "_" & DtFinPeriode.DateTime.ToShortDateString
            NomRepertoire = line & "\RapportsGlobals\" & FormatFileName(NomRepertoire.ToString.Trim, "") & " - " & Bailleur

            If Not Directory.Exists(NomRepertoire) Then
                Directory.CreateDirectory(NomRepertoire)
            End If

            report.ExportToDisk(ExportFormatType.PortableDocFormat, NomRepertoire & "\SectionA_Identification.pdf")
            ViewIdentifications.ReportSource = report
            FinChargement()
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function GetID_Projet() As String
        Try
            query = "SELECT IdentifiantProjet FROM t_projet WHERE CodeProjet='" & ProjetEnCours & "'"
            Return MettreApost(ExecuteScallar(query))

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Function

#End Region

#Region "Donnee financiere"

    Private Sub ChkDonneFinanc_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDonneFinanc.CheckedChanged

        If FermerDossierEnCours = False Then
            Dim Periode = DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString

            If ChkDonneFinanc.Checked = True Then
                DebutChargement(True, "Chargement des données de la situation financière du projet...")

                query = "Update t_rg_rapports_globals set DonneFinanciere='OUI' where PeriodeRGLS='" & Periode & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                'DPD ET DRF ==== Mobilisation des ressources
                ChargerDonneFinanciere()

                PageDonneFinance.PageVisible = True
                XtraTabControl1.SelectedTabPage = PageDonneFinance

                FinChargement()
            Else
                PageDonneFinance.PageVisible = False
                query = "Update t_rg_rapports_globals set DonneFinanciere='NON' where PeriodeRGLS='" & Periode & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                query = "delete from t_rg_donnefinanciere where PeriodeRGLS='" & Periode & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            EnregistrerTableMatiere()
        Else
            PageDonneFinance.PageVisible = False
            ViewDonneFinance.ReportSource = Nothing
            ViewDonneFinance.Refresh()
        End If

    End Sub

    Private Sub ChargerLesRessources(ByVal DateDebut As Date, ByVal DateFin As Date)
        Try
            'Dim DateDebut As Date = DtDebPeriode.DateTime.ToShortDateString
            'Dim DateFin As Date = DtFinPeriode.DateTime.ToShortDateString
            Dim PerieodeRap = DateDebut & " - " & DateFin


            query = "select count(*) from T_rg_MobilisationRessource where PeriodeRGLS='" & PerieodeRap & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim DejaEnregistrer As Decimal = ExecuteScallar(query)

            query = "SELECT NumeroComptable, CodeBailleur from t_comptebancaire where CodeProjet='" & ProjetEnCours & "' and TypeCompte LIKE '%contrepartie%'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            Dim MontantConvention As Decimal = 0

            For Each rw In dt.Rows
                query = "SELECT MontantConvention from t_convention where CodeBailleur='" & rw("CodeBailleur") & "'"
                MontantConvention = Val(ExecuteScallar(query))

                'Approvisionnement depuis le debut du projet

                query = "SELECT SUM(DEBIT_LE) as MontantDebit from t_comp_ligne_ecriture where DATE_LE<'" & DateDebut & "' and CODE_SC='" & rw("NumeroComptable") & "' and CODE_PROJET='" & ProjetEnCours & "'"
                Dim MontantApproDepuiProjet = Val(ExecuteScallar(query))

                'Montant approvisionner dans la période
                query = "SELECT SUM(DEBIT_LE) as MontantDebit from t_comp_ligne_ecriture where DATE_LE>='" & DateDebut & "' and DATE_LE<='" & DateFin & "' and CODE_SC='" & rw("NumeroComptable") & "' and CODE_PROJET='" & ProjetEnCours & "'"
                Dim MontantApproPeriode = Val(ExecuteScallar(query))

                Dim TauxApproProjet As Decimal = 0
                Dim TauxApproPeriode As Decimal = 0

                If MontantConvention > 0 Then

                    TauxApproProjet = Round(CDec((MontantApproDepuiProjet * 100) / MontantConvention), 2)
                    TauxApproProjet = IIf(TauxApproProjet <= 0, Round(CDec((MontantApproDepuiProjet * 100) / MontantConvention), 4), TauxApproProjet).ToString

                    TauxApproPeriode = Round(CDec((MontantApproPeriode * 100) / MontantConvention), 2)
                    TauxApproPeriode = IIf(TauxApproPeriode <= 0, Round(CDec((MontantApproPeriode * 100) / MontantConvention), 4), TauxApproPeriode).ToString

                End If

                If DejaEnregistrer = 0 Then
                    query = "INSERT INTO T_rg_MobilisationRessource values(NULL, '" & PerieodeRap & "', '" & MontantApproDepuiProjet & "', '" & TauxApproProjet & "', '" & MontantApproPeriode & "', '" & TauxApproPeriode & "', '" & ProjetEnCours & "')"
                    ExecuteNonQuery(query)
                End If
            Next

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub ChargerDonneFinanciere()
        Try
            Dim DateDebut As Date = DtDebPeriode.DateTime.ToShortDateString
            Dim DateFin As Date = DtFinPeriode.DateTime.ToShortDateString
            Dim PerieodeRap = DateDebut & " - " & DateFin

            If NepasExecuteleMemeCode = False Then

                Dim MontantDRF_Elabore_Periode As Decimal = 0
                Dim MontantDRF_Paye_Periode As Decimal = 0

                Dim MontantDPD_Elabore_Periode As Decimal = 0
                Dim MontantDPD_Paye_Periode As Decimal = 0

                Dim MontantDRF_DPD_Elabore_pcd As Decimal = 0
                Dim MontantDRF_DPD_Paye_pcd As Decimal = 0

                Dim Montant_Elabore_Final As Decimal = 0
                Dim Montant_Paye_Final As Decimal = 0

                Dim TauxDRF_DPD_Elab As Decimal = 0
                Dim TauxDRF_DPD_Paye As Decimal = 0

                Dim TauxDRF_DPD_Elab_pcd As Decimal = 0
                Dim TauxDRF_DPD_Paye_pcd As Decimal = 0

                query = "select count(*) from t_rg_donnefinanciere where PeriodeRGLS='" & PerieodeRap & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim ExisteDonFinanciere As Decimal = ExecuteScallar(query)

                query = "SELECT B.CodeBailleur, B.InitialeBailleur, C.CodeConvention, C.MontantConvention FROM t_bailleur as B, t_convention as C where B.CodeBailleur=C.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "' ORDER BY B.CodeBailleur ASC"
                Dim dt As DataTable = ExcecuteSelectQuery(query)

                'query = "SELECT DateDebutProjetMO FROM T_projet WHERE CodeProjet='" & ProjetEnCours & "'"
                'Dim DateDebutProjet As String = ExecuteScallar(query)

                For Each rwConvention In dt.Rows

                    'Rechercher DRF elaborer et Payer dans la periode pour le bailleur et la convention en cours **********************************
                    '********* DRF ELABORER ************

                    query = "SELECT SUM(DRF_MONTANT) from t_gf_demanderf where ((DRF_ETAT='En cours') or (DRF_ETAT='Réglée')) and DRF_NUM_AID='" & rwConvention("InitialeBailleur") & "' and DRF_NUM_BIRD='" & rwConvention("CodeConvention") & "' and DRF_DATE>='" & DateDebut & "' and DRF_DATE<='" & DateFin & "' and CodeProjet='" & ProjetEnCours & "'"
                    MontantDRF_Elabore_Periode = Val(ExecuteScallar(query))

                    '********* DRF PAYER ************ 
                    query = "SELECT SUM(DRF_MONTANT_DEF) from t_gf_demanderf where DRF_ETAT='Réglée' and DRF_NUM_AID='" & rwConvention("InitialeBailleur") & "' and DRF_NUM_BIRD='" & rwConvention("CodeConvention") & "' and DRF_DATE>='" & DateDebut & "' and DRF_DATE<='" & DateFin & "' and CodeProjet='" & ProjetEnCours & "'"
                    MontantDRF_Paye_Periode = Val(ExecuteScallar(query))

                    'Rechercher les DPD ELABORER ET PAYER dans la periode pour le bailleur et la convention en cours *****************************
                    '*** DPD Elaborer *******************

                    query = "SELECT SUM(DPD_MONTANT) from t_gf_demandepd where ((DPD_ETAT='En cours') or (DPD_ETAT='Réglée')) and DPD_NUM_BIRD='" & rwConvention("InitialeBailleur") & "' and DPD_NUM_AID='" & rwConvention("CodeConvention") & "' and DPD_DATE>='" & DateDebut & "' and DPD_DATE<='" & DateFin & "' and CodeProjet='" & ProjetEnCours & "'"
                    MontantDPD_Elabore_Periode = Val(ExecuteScallar(query))

                    '*** DPD payer *******************
                    query = "SELECT SUM(DPD_MONTANT_DEF) from t_gf_demandepd where DPD_ETAT='Réglée' and DPD_NUM_BIRD='" & rwConvention("InitialeBailleur") & "' and DPD_NUM_AID='" & rwConvention("CodeConvention") & "' and DPD_DATE>='" & DateDebut & "' and DPD_DATE<='" & DateFin & "' and CodeProjet='" & ProjetEnCours & "'"
                    MontantDPD_Paye_Periode = Val(ExecuteScallar(query))

                    '*** Elaborer periode finanl *************
                    Montant_Elabore_Final = MontantDRF_Elabore_Periode + MontantDPD_Elabore_Periode

                    '*** Payer periode finanl *************
                    Montant_Paye_Final = MontantDRF_Paye_Periode + MontantDPD_Paye_Periode

                    '********** Cumule precedent elaborer et payer ***** ***************

                    MontantDRF_DPD_Elabore_pcd = GetMontantPrecedentDRF_DPD("Elaborer", DateDebut, rwConvention("InitialeBailleur"), rwConvention("CodeConvention"))
                    MontantDRF_DPD_Paye_pcd = GetMontantPrecedentDRF_DPD("Payer", DateDebut, rwConvention("InitialeBailleur"), rwConvention("CodeConvention"))

                    If rwConvention("MontantConvention") > 0 Then

                        TauxDRF_DPD_Elab = Round(CDec((Montant_Elabore_Final * 100) / rwConvention("MontantConvention")), 2)
                        TauxDRF_DPD_Elab = IIf(TauxDRF_DPD_Elab <= 0, Round(CDec((Montant_Elabore_Final * 100) / rwConvention("MontantConvention")), 4), TauxDRF_DPD_Elab).ToString

                        TauxDRF_DPD_Paye = Round(CDec((Montant_Paye_Final * 100) / rwConvention("MontantConvention")), 2)
                        TauxDRF_DPD_Paye = IIf(TauxDRF_DPD_Paye <= 0, Round(CDec((Montant_Paye_Final * 100) / rwConvention("MontantConvention")), 4), TauxDRF_DPD_Paye).ToString

                        TauxDRF_DPD_Elab_pcd = Round(CDec((MontantDRF_DPD_Elabore_pcd * 100) / rwConvention("MontantConvention")), 2)
                        TauxDRF_DPD_Elab_pcd = IIf(TauxDRF_DPD_Elab_pcd <= 0, Round(CDec((MontantDRF_DPD_Elabore_pcd * 100) / rwConvention("MontantConvention")), 4), TauxDRF_DPD_Elab_pcd).ToString()

                        TauxDRF_DPD_Paye_pcd = Round(CDec((MontantDRF_DPD_Paye_pcd * 100) / rwConvention("MontantConvention")), 2)
                        TauxDRF_DPD_Paye_pcd = IIf(TauxDRF_DPD_Paye_pcd <= 0, Round(CDec((MontantDRF_DPD_Paye_pcd * 100) / rwConvention("MontantConvention")), 4), TauxDRF_DPD_Paye_pcd).ToString()
                    Else
                        TauxDRF_DPD_Elab = 0
                        TauxDRF_DPD_Paye = 0

                        TauxDRF_DPD_Elab_pcd = 0
                        TauxDRF_DPD_Paye_pcd = 0
                    End If

                    If ExisteDonFinanciere = 0 Then
                        query = "INSERT INTO t_rg_donnefinanciere values(NULL, '" & PerieodeRap & "', '" & Montant_Elabore_Final & "', '" & TauxDRF_DPD_Elab & "', '" & Montant_Paye_Final & "', '" & TauxDRF_DPD_Paye & "', '" & MontantDRF_DPD_Elabore_pcd & "', '" & TauxDRF_DPD_Elab_pcd & "', '" & MontantDRF_DPD_Paye_pcd & "', '" & TauxDRF_DPD_Paye_pcd & "', '" & rwConvention("CodeBailleur") & "', '" & rwConvention("CodeConvention") & "', '" & ProjetEnCours & "')"
                        ExecuteNonQuery(query)
                    End If
                Next

            End If

            'Mobilisation des ressources
            ChargerLesRessources(DateDebut, DateFin)

            'Afficharge de l'Etat ******************************

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\RapportsGlobals\"
            Dim report As New ReportDocument
            Dim DatSet As New DataSet

            report.Load(Chemin & "DonneeFinanciere.rpt")

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
            report.SetParameterValue("Periode", PerieodeRap)
            report.SetParameterValue("Periode", PerieodeRap, "MobilisationDesRessources.rpt")

            Dim NomRepertoire As String = DtDebPeriode.DateTime.ToShortDateString & "_" & DtFinPeriode.DateTime.ToShortDateString
            NomRepertoire = line & "\RapportsGlobals\" & FormatFileName(NomRepertoire.ToString.Trim, "") & " - " & Bailleur

            If Not Directory.Exists(NomRepertoire) Then
                Directory.CreateDirectory(NomRepertoire)
            End If

            report.ExportToDisk(ExportFormatType.PortableDocFormat, NomRepertoire & "\DonneFinanceduProjet.pdf")
            ViewDonneFinance.ReportSource = report

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub


    Private Function GetMontantPrecedentDRF_DPD(ByVal TypeRechere As String, ByVal DateDebut As Date, ByVal InitialBailleur As String, ByVal Convention As String) As Decimal
        Try
            Dim MontantAnterieurDRF As Decimal = 0
            Dim MontantAnterieurDDP As Decimal = 0

            ' DateDebut = dateconvert(DateDebut)

            'Rechercher les Montants anterieur des DPD ET DRF elaborer

            If TypeRechere.ToString = "Elaborer" Then

                query = "Select SUM(DRF_MONTANT) from t_gf_demanderf where ((DRF_ETAT='En cours') or (DRF_ETAT='Réglée')) and DRF_NUM_AID='" & InitialBailleur & "' and DRF_NUM_BIRD='" & Convention & "' and DRF_DATE<'" & DateDebut & "' and CodeProjet='" & ProjetEnCours & "'"
                MontantAnterieurDRF = Val(ExecuteScallar(query))

            query = "SELECT SUM(DPD_MONTANT) from t_gf_demandepd where ((DPD_ETAT='En cours') or (DPD_ETAT='Réglée')) and DPD_NUM_BIRD='" & InitialBailleur & "' and DPD_NUM_AID='" & Convention & "' and DPD_DATE<'" & DateDebut & "' and CodeProjet='" & ProjetEnCours & "'"
                MontantAnterieurDDP = Val(ExecuteScallar(query))
            Else
                query = "SELECT SUM(DRF_MONTANT_DEF) from t_gf_demanderf where DRF_ETAT='Réglée' and DRF_NUM_AID='" & InitialBailleur & "' and DRF_NUM_BIRD='" & Convention & "' and DRF_DATE<'" & DateDebut & "' and CodeProjet='" & ProjetEnCours & "'"
                MontantAnterieurDRF = Val(ExecuteScallar(query))

                query = "SELECT SUM(DPD_MONTANT_DEF) from t_gf_demandepd where DPD_ETAT='Réglée' and DPD_NUM_BIRD='" & InitialBailleur & "' and DPD_NUM_AID='" & Convention & "' and DPD_DATE<'" & DateDebut & "' and CodeProjet='" & ProjetEnCours & "'"
                MontantAnterieurDDP = Val(ExecuteScallar(query))
            End If

            Return MontantAnterieurDDP + MontantAnterieurDRF
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Function

#End Region

#Region "Introduction"

    Private Sub CheckIntroduc_CheckedChanged(sender As Object, e As EventArgs) Handles CheckIntroduc.CheckedChanged
        If FermerDossierEnCours = False Then

            If CheckIntroduc.Checked = True Then
                PageIntroduction.PageVisible = True
                XtraTabControl1.SelectedTabPage = PageIntroduction

                If CheckSectionB.Checked = False Then CheckSectionB.Checked = True

                query = "Update t_rg_rapports_globals Set Introduction='OUI' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                Dim NomDossier As String = line & "\RapportsGlobals\" & ProjetEnCours
                If (Directory.Exists(NomDossier) = True) Then
                    SaisieDetailsRapp.LoadDocument(NomDossier & "\DétailsRapport.docx", DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
                Else
                    SaisieDetailsRapp.ResetText()
                End If

                BtEnrgRapp.Enabled = True
            Else
                PageIntroduction.PageVisible = False
                SaisieDetailsRapp.ResetText()
                'CheckSectionB.Checked = False

                query = "Update t_rg_rapports_globals set Introduction='NON' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

            End If

            EnregistrerTableMatiere()
        Else
            PageIntroduction.PageVisible = False
            SaisieDetailsRapp.ResetText()
            If CheckSectionB.Checked = True Then CheckSectionB.Checked = False
        End If
    End Sub

#End Region

#Region "Examen d'avancement et resultat"

    Private Sub ChkExamenResultat_CheckedChanged(sender As Object, e As EventArgs) Handles ChkExamenResultat.CheckedChanged
        If FermerDossierEnCours = False Then
            If ChkExamenResultat.Checked = True Then

                DebutChargement()

                PageExamen.PageVisible = True
                XtraTabControl1.SelectedTabPage = PageExamen
                XtraTabControl2.SelectedTabPage = PageExamenAvencemet

                ChargerDonneExamenAvencement()

                ChkPassation.Enabled = True
                ChkSaveMesure.Enabled = True
                ChkProjet.Enabled = True

                If CheckSectionB.Checked = False Then CheckSectionB.Checked = True
                FinChargement()
            Else
                PageExamen.PageVisible = False

                ChkPassation.Enabled = False
                ChkSaveMesure.Enabled = False
                ChkProjet.Enabled = False

                ChkPassation.Checked = False
                ChkSaveMesure.Checked = False
                ChkProjet.Checked = False
            End If

            EnregistrerTableMatiere()
        Else
            PageExamen.PageVisible = False
            ChkPassation.Enabled = False
            ChkSaveMesure.Enabled = False
            ChkProjet.Enabled = False

            ChkPassation.Checked = False
            ChkSaveMesure.Checked = False
            ChkProjet.Checked = False
            If CheckSectionB.Checked = True Then CheckSectionB.Checked = False
        End If
    End Sub

    Private Sub ChargerDonneExamenAvencement()
        Try
            'Dim DateDebut As Date = DtDebPeriode.DateTime.Date
            'Dim DateFin As Date = DtFinPeriode.DateTime.Date
            Dim periodeRapport As String = DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString

            'Afficharge de l'Etat **********************************************

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\RapportsGlobals\"
            Dim report As New ReportDocument
            Dim DatSet = New DataSet

            report.Load(Chemin & "RapportExamenAvencement.rpt")
            report.SetDataSource(DatSet)

            Dim TotalFondsPercu As Decimal = 0
            Dim fonPer As Decimal = 0

            query = "select MontantPeriode from t_rsf_etatfondsemploires_rapport_globals where Bailleur='" & EnleverApost(Bailleur) & "' and PeriodeRSF='" & periodeRapport & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt1.Rows
                fonPer = CDec(rw("MontantPeriode").ToString.Replace(" ", ""))
                TotalFondsPercu = TotalFondsPercu + fonPer
            Next

            Dim TEmploiPeriode As Decimal = 0
            Dim TEmploiCumule As Decimal = 0

            query = "select MontCompoPeriode from t_rsf_etatcompoemploires_rapport_global where LENGTH(LibCourtCompo)='1' AND PeriodeRSF='" & periodeRapport & "' AND Bailleur='" & EnleverApost(Bailleur) & "'"
            dt = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt.Rows
                fonPer = CDec(rw("MontCompoPeriode").ToString.Replace(" ", ""))
                TEmploiPeriode = TEmploiPeriode + fonPer
            Next

            Dim SignePer As String = ""
            Dim MontExcedPer As Decimal = TotalFondsPercu - TEmploiPeriode
            If (MontExcedPer < 0) Then
                MontExcedPer = Math.Abs(MontExcedPer)
                SignePer = "-"
            End If

            report.SetParameterValue("TotFondsPeriode", AfficherMonnaie(TotalFondsPercu.ToString))
            report.SetParameterValue("ExceDeficPer", SignePer & AfficherMonnaie(MontExcedPer.ToString))
            report.SetParameterValue("TotEmploiPer", AfficherMonnaie(TEmploiPeriode.ToString))
            report.SetParameterValue("Periode", periodeRapport.ToString.Replace("-", "au"))

            report.SetParameterValue("PeriodeRSF", periodeRapport, "SRapportEmplResFonds.rpt")

            report.SetParameterValue("PeriodeRSF", periodeRapport, "SRapportEmplResCompo.rpt")

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

            Dim NomRepertoire As String = DtDebPeriode.DateTime.ToShortDateString & "_" & DtFinPeriode.DateTime.ToShortDateString
            NomRepertoire = line & "\RapportsGlobals\" & FormatFileName(NomRepertoire.ToString.Trim, "") & " - " & Bailleur

            report.ExportToDisk(ExportFormatType.PortableDocFormat, NomRepertoire & "\ExamenAnvencement.pdf")
            ViewExamenAvencement.ReportSource = report

        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub
#End Region

#Region "Plan trimestriel d'action et chronogrmme"

    Private Sub CheckChrono_CheckedChanged(sender As Object, e As EventArgs) Handles CheckChrono.CheckedChanged
        If FermerDossierEnCours = False Then
            If CheckChrono.Checked = True Then
                PagePlanTrimestriele.PageVisible = True
                XtraTabControl1.SelectedTabPage = PagePlanTrimestriele

                query = "Update t_rg_rapports_globals set PlanActionChrono	='OUI' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            Else
                PagePlanTrimestriele.PageVisible = False

                query = "Update t_rg_rapports_globals set PlanActionChrono	='NON' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If
            EnregistrerTableMatiere()
        Else
            PagePlanTrimestriele.PageVisible = False
            ViewPlanChrono.ReportSource = Nothing
            ViewPlanChrono.Refresh()
        End If
    End Sub

#End Region

#Region "Rapport sur le suivi financier"

    Private Sub ChkSuiviFinanc_CheckedChanged(sender As Object, e As EventArgs) Handles ChkSuiviFinanc.CheckedChanged

        If FermerDossierEnCours = False Then

            Dim periodeRapport As String = DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString

            If ChkSuiviFinanc.Checked = True Then
                PageSuiviFinanc.PageVisible = True
                XtraTabControl1.SelectedTabPage = PageSuiviFinanc
                XtraTableFinanc.SelectedTabPage = TablRessource

                'Mise à jour dans la BD ******************************
                'query = "Update t_rg_rapports_globals set RapportSuiviFinancier='OUI', DateModif='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                'ExecuteNonQuery(query)

                '********************************************************
                Dim DateDebut As Date = DtDebPeriode.DateTime.Date
                Dim DateFin As Date = DtFinPeriode.DateTime.Date

                ChargerEmploiRessources(DateDebut, DateFin)

                ChargerFondsParActivite(DateDebut, DateFin)

                ChargerObservation_Commentaire(periodeRapport)

                BtEnrgRapp.Enabled = True
                'PageCommentaire.PageVisible = True
                XtraTabControl1.SelectedTabPage = PageCommentaire
            Else
                PageSuiviFinanc.PageVisible = False
                PageCommentaire.PageVisible = False

                'Mise à jour dans la BD ******************************
                query = "Update t_rg_rapports_globals set RapportSuiviFinancier='NON', DateModif='" & Now.ToShortDateString & " " & Now.ToLongTimeString & "' where PeriodeRGLS='" & periodeRapport & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                query = "DELETE FROM t_rsf_etatfondsparactivite_rapports_globals WHERE PeriodeRSF='" & periodeRapport & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

                query = "DELETE FROM t_rsf_etatfondsemploires_rapport_globals WHERE Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & periodeRapport & "'"
                ExecuteNonQuery(query)
                query = "DELETE FROM t_rsf_etatcompoemploires_rapport_global WHERE Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & periodeRapport & "'"
                ExecuteNonQuery(query)
                query = "DELETE FROM t_rsf_disponible_treso_rapport_global WHERE Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & periodeRapport & "' AND CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            EnregistrerTableMatiere()
        Else
            PageSuiviFinanc.PageVisible = False
            ViewEmpFonActivite.ReportSource = Nothing
            ViewEmpFonActivite.Refresh()

            ViewRessourEmploi.ReportSource = Nothing
            ViewRessourEmploi.Refresh()

            PageCommentaire.PageVisible = False
            GridComm.DataSource = Nothing

        End If

    End Sub

    Private Sub ChargerEmploiRessources(DateDebutRSf As Date, DateFinRSF As Date)
        Dim PeriodeRSF As String = DateDebutRSf.ToShortDateString & " - " & DateFinRSF.ToShortDateString
        Dim TotalFondsPercu As Decimal = 0
        Dim TotalFondsCumule As Decimal = 0
        Dim LimiteMontant As Decimal = 3
        Dim LesZeros As String = String.Empty
        Dim InitialeBailleur As String = String.Empty
        Dim OptBailleur As String = String.Empty

        Dim LeTexte As String() = BtNewRapports.Text.Split(" "c)
        For i = 1 To LimiteMontant
            LesZeros += "0"
        Next

        DebutChargement(True, "Analyse des données financières de la période en cours...")

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

                If LeTexte(0).ToString <> "Dossier" Then 'On ajoute dans la base de donnees uniquement si c'est un nouveau RSF
                    query = "SELECT COUNT(*) FROM t_rsf_etatfondsemploires_rapport_globals WHERE SourceFonds='" & EnleverApost(LibelleFonds) & "' AND MontantPeriode='" & MontantPercu & "' AND MontantCumule='" & MontantCummule & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND PeriodeRSF='" & PeriodeRSF & "'"
                    If Val(ExecuteScallar(query)) = 0 Then
                        query = "INSERT INTO t_rsf_etatfondsemploires_rapport_globals(RefEtat,SourceFonds,MontantPeriode,MontantCumule,PeriodeRSF,Bailleur) VALUES(NULL,'" & EnleverApost(LibelleFonds) & "','" & MontantPercu & "','" & MontantCummule & "','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "')"
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

            If LeTexte(0).ToString <> "Dossier" Then 'On ajoute dans la base de donnees uniquement si c'est un nouveau RSF
                query = "SELECT COUNT(*) FROM t_rsf_etatcompoemploires_rapport_global WHERE RefEtatCompo='" & rwComposante("CodePartition") & "' AND LibCourtCompo='" & rwComposante("LibelleCourt") & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND LibelleCompo='" & rwComposante("LibellePartition") & "' AND MontCompoPeriode='" & MontantComposante & "' AND MontCompoCumul='" & MontantCummuleComposante & "' AND PeriodeRSF='" & PeriodeRSF & "'"
                If Val(ExecuteScallar(query)) = 0 Then
                    query = "INSERT INTO t_rsf_etatcompoemploires_rapport_global(NumOrdre,RefEtatCompo,LibCourtCompo,LibelleCompo,MontCompoPeriode,MontCompoCumul,PeriodeRSF,Bailleur) VALUES(NULL,'" & rwComposante("CodePartition") & "','" & rwComposante("LibelleCourt") & "','" & rwComposante("LibellePartition") & "','" & MontantComposante & "','" & MontantCummuleComposante & "','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "')"
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

                If LeTexte(0).ToString <> "Dossier" Then 'On ajoute dans la base de donnees uniquement si c'est un nouveau RSF
                    query = "SELECT COUNT(*) FROM t_rsf_etatcompoemploires_rapport_global WHERE RefEtatCompo='" & CodePartition & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND LibCourtCompo='" & rwActivites("LibelleCourt") & "' AND LibelleCompo='" & LibelleActivite & "' AND MontCompoPeriode='" & MontantActivite & "' AND MontCompoCumul='" & MontantCummuleActivite & "' AND PeriodeRSF='" & PeriodeRSF & "'"
                    If Val(ExecuteScallar(query)) = 0 Then
                        query = "INSERT INTO t_rsf_etatcompoemploires_rapport_global(NumOrdre,RefEtatCompo,LibCourtCompo,LibelleCompo,MontCompoPeriode,MontCompoCumul,PeriodeRSF,Bailleur) VALUES(NULL,'" & CodePartition & "','" & rwActivites("LibelleCourt") & "','" & LibelleActivite & "','" & MontantActivite & "','" & MontantCummuleActivite & "','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "')"
                        ExecuteNonQuery(query)
                    End If
                End If
            Next
        Next

        'Chargement des soldes de la trésorerie
        Dim TSoldeOuvert As Decimal = 0
        Dim TSoldeEncaisse As Decimal = 0
        query = "SELECT COUNT(*) FROM t_rsf_disponible_treso_rapport_global WHERE PeriodeRSF='" & PeriodeRSF & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND Justificatif='Avant'"
        If Val(ExecuteScallar(query)) = 0 Then
            'Dim CodeBailleur As Decimal = Val(ExecuteScallar("SELECT CodeBailleur from t_bailleur WHERE InitialeBailleur='" & InitialeBailleur & "'"))
            'query = "select DISTINCT LibelleCompte, CodeProjet, NumeroComptable from T_CompteBancaire where CodeBailleur='" & CodeBailleur & "' AND CodeProjet='" & ProjetEnCours & "'"

            query = "SELECT CodeBailleur from t_bailleur WHERE CodeProjet='" & ProjetEnCours & "'"
            Dim rwCodbailleur As DataTable = ExcecuteSelectQuery(query)

            For Each rw0 In rwCodbailleur.Rows

                query = "select DISTINCT LibelleCompte, CodeProjet, NumeroComptable from T_CompteBancaire where CodeBailleur='" & rw0("CodeBailleur") & "' AND CodeProjet='" & ProjetEnCours & "'"
                Dim dtComptes As DataTable = ExcecuteSelectQuery(query)

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
                    query = "INSERT INTO t_rsf_disponible_treso_rapport_global VALUES(NULL,'" & rwCompte("LibelleCompte") & "','" & Solde & "','Avant','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
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
                    query = "INSERT INTO t_rsf_disponible_treso_rapport_global VALUES(NULL,'" & rwCompte("LibelleCompte") & "','" & Solde & "','Après','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
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
            'query = "INSERT INTO t_rsf_disponible_treso_rapport_global VALUES(NULL,'Caisse " & ProjetEnCours & " (571100)','" & SoldeCaisse & "','Avant','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
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
            'query = "INSERT INTO t_rsf_disponible_treso_rapport_global VALUES(NULL,'Caisse " & ProjetEnCours & " (571100)','" & SoldeCaisse & "','Après','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
            'ExecuteNonQuery(query)
        Else
            TSoldeOuvert = Val(ExecuteScallar("SELECT SUM(Solde) FROM t_rsf_disponible_treso_rapport_global WHERE PeriodeRSF='" & PeriodeRSF & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND CodeProjet='" & ProjetEnCours & "' AND Justificatif='Avant'"))
            TSoldeEncaisse = Val(ExecuteScallar("SELECT SUM(Solde) FROM t_rsf_disponible_treso_rapport_global WHERE PeriodeRSF='" & PeriodeRSF & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND CodeProjet='" & ProjetEnCours & "' AND Justificatif='Après'"))
        End If


        'Affichage de l'état ********************************************************************
        Dim LibelleProjet As String = ""
        query = "select NomProjet,PaysProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)

        For Each rw As DataRow In dt.Rows
            Dim LeGouv As String = "GOUVERNEMENT DE L'ETAT DE " & MettreApost(rw("PaysProjet").ToString).ToUpper
            LibelleProjet = MettreApost(rw("NomProjet").ToString.ToUpper)
            LibelleProjet = LeGouv & vbNewLine & LibelleProjet & vbNewLine & "TABLEAU DES RECETTES ET DES PAIEMENTS"
            LibelleProjet = LibelleProjet & vbNewLine & "Pour la période du " & DtDebPeriode.DateTime.ToShortDateString & " au " & DtFinPeriode.DateTime.ToShortDateString
            LibelleProjet = LibelleProjet & vbNewLine & " en francs CFA (FCFA)"
        Next

        LibelleProjet = "TABLEAU DES RESSOURCES ET EMPLOIS" & vbNewLine & "Pour la période du  " & DtDebPeriode.DateTime.ToShortDateString & "  au  " & DtFinPeriode.DateTime.ToShortDateString
        LibelleProjet += vbNewLine & "Montant en milliers de Francs"

        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Engine.Tables
        Dim CrTable As Engine.Table
        Dim Chemin As String = lineEtat & "\RapportsGlobals\"
        Dim report As New Engine.ReportDocument
        Dim DatSet = New DataSet
        report.Load(Chemin & "RapportEmplResFonds.rpt")
        report.SetDataSource(DatSet)

        Dim TEmploiPeriode As Decimal = 0
        Dim TEmploiCumule As Decimal = 0

        query = "select MontCompoPeriode, MontCompoCumul from t_rsf_etatcompoemploires_rapport_global where LENGTH(LibCourtCompo)='1' AND PeriodeRSF='" & PeriodeRSF & "' AND Bailleur='" & EnleverApost(Bailleur) & "'"
        dt = ExcecuteSelectQuery(query)

        For Each rw As DataRow In dt.Rows
            Dim fonPer As Decimal = CDec(rw("MontCompoPeriode").ToString.Replace(" ", ""))
            Dim fonCum As Decimal = CDec(rw("MontCompoCumul").ToString.Replace(" ", ""))
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
        'report.SetParameterValue("Bailleur", EnleverApost(Bailleur), "SRapportEmplResFonds.rpt")

        report.SetParameterValue("PeriodeRSF", PeriodeRSF, "SRapportEmplResCompo.rpt")
        ' report.SetParameterValue("Bailleur", EnleverApost(Bailleur), "SRapportEmplResCompo.rpt")

        report.SetParameterValue("CodeProjet", ProjetEnCours, "SRapportEmplResCompteBancaire.rpt")
        report.SetParameterValue("PeriodeRSF", PeriodeRSF, "SRapportEmplResCompteBancaire.rpt")
        ' report.SetParameterValue("Bailleur", EnleverApost(Bailleur), "SRapportEmplResCompteBancaire.rpt")

        report.SetParameterValue("CodeProjet", ProjetEnCours, "SRapportEmplResCompteBancaireSolde.rpt")
        report.SetParameterValue("PeriodeRSF", PeriodeRSF, "SRapportEmplResCompteBancaireSolde.rpt")
        'report.SetParameterValue("Bailleur", EnleverApost(Bailleur), "SRapportEmplResCompteBancaireSolde.rpt")

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

        Dim NomRepertoire As String = DtDebPeriode.DateTime.ToShortDateString & "_" & DtFinPeriode.DateTime.ToShortDateString
        NomRepertoire = line & "\RapportsGlobals\" & FormatFileName(NomRepertoire.ToString.Trim, "") & " - " & Bailleur

        report.ExportToDisk(ExportFormatType.PortableDocFormat, NomRepertoire & "\EmploiRessources.pdf")
        ViewRessourEmploi.ReportSource = report

        FinChargement()
    End Sub

    Private Sub ChargerFondsParActivite(ByVal DateDebutRSf As Date, ByVal DateFinRSF As Date)

        Try
            DebutChargement(True, "Analyse des données financières des activités en cours...")

            Dim PeriodeRSF As String = DateDebutRSf.ToShortDateString & " - " & DateFinRSF.ToShortDateString
            Dim LeTexte As String() = BtNewRapports.Text.Split(" "c)

            Dim LimiteMontant As Decimal = 3
            Dim LesZeros As String = String.Empty
            For i = 1 To LimiteMontant
                LesZeros += "0"
            Next

            Dim InitialeBailleur As String = String.Empty
            Dim OptBailleur As String = String.Empty

            'InitialeBailleur = Bailleur
            'OptBailleur = " AND InitialeBailleur='" & InitialeBailleur & "'"

            'If cmbBailleur.SelectedIndex > 0 Then
            '    InitialeBailleur = cmbBailleur.Text.Split(" - ")(0)
            '    OptBailleur = " AND InitialeBailleur='" & InitialeBailleur & "'"
            'End If

            Dim TDotationPeriode As Decimal = 0
            Dim TRealisationPeriode As Decimal = 0
            Dim TDotationCumule As Decimal = 0
            Dim TRealisationCumule As Decimal = 0
            Dim TDotationProjet As Decimal = 0
            Dim TRealisationProjet As Decimal = 0
            Dim TMontantsPrecedents As Decimal = 0

            Dim PoucentageComposante As Decimal = 0
            Dim PoucentageActivite As Decimal = 0
            Dim CodeComposante As Decimal = 0

            'Vide la table des commentaires  **************************
            'query = "delete from t_tampon_observation_rapports_global"
            'ExecuteNonQuery(query)

            'Les codes et libelles des composantes et activites *************************************************
            query = "Select COUNT(*) from t_rg_Commentaire WHERE PeriodeRGLS='" & PeriodeRSF & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim ExisteCommentaire As Decimal = Val(ExecuteScallar(query))

            query = "Select COUNT(*) from t_rsf_etatfondsparactivite_rapports_globals WHERE PeriodeRSF='" & PeriodeRSF & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim ExisteCompoActivite As Decimal = Val(ExecuteScallar(query))

            query = "select DISTINCT LibelleCourt, LibellePartition from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt ASC"
            Dim dtComposante As Data.DataTable = ExcecuteSelectQuery(query)

            For Each rwComposante As DataRow In dtComposante.Rows

                Dim RealisationComposantePrecedent As Decimal = GetMontantPrecedent_Rapports_Global(rwComposante("LibelleCourt"), DateDebutRSf, Bailleur)
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

                If DotationTotalComposante > 0 Then
                    PoucentageComposante = Round((RealisationTotalComposante * 100) / DotationTotalComposante, 2)
                Else
                    PoucentageComposante = 0
                End If

                If LeTexte(0).ToString <> "Dossier" Then 'Seulement pour un nouveau rapports

                    If ExisteCompoActivite = 0 Then
                        query = "INSERT INTO t_rsf_etatfondsparactivite_rapports_globals VALUES(NULL,'" & rwComposante("LibelleCourt") & "','" & rwComposante("LibellePartition") & "','" & RealisationComposantePrecedent.ToString().Replace(",", ".") & "','" & DotationComposantePeriode.ToString().Replace(",", ".") & "','" & RealisationComposantePeriode.ToString().Replace(",", ".") & "','" & DotationCumuleComposante.ToString().Replace(",", ".") & "','" & RealisationCumuleComposante.ToString().Replace(",", ".") & "','" & DotationTotalComposante.ToString().Replace(",", ".") & "','" & RealisationTotalComposante.ToString().Replace(",", ".") & "','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
                        ExecuteNonQuery(query)
                    End If

                    'Insertion Commentaire composante
                    If ExisteCommentaire = 0 Then
                        query = "insert into t_rg_Commentaire values(NULL, '" & PeriodeRSF & "', '" & rwComposante("LibelleCourt") & "', '" & EnleverApost(rwComposante("LibellePartition").ToString) & "', '" & RealisationTotalComposante * 1000 & "', '" & GetPourcentagePhysique(rwComposante("LibelleCourt"), "Composante") & "','" & PoucentageComposante & "', '','C', '" & ProjetEnCours & "','0')"
                        ExecuteNonQuery(query)
                    End If
                End If

                query = "select DISTINCT LibelleCourt,LibellePartition from T_Partition where CodeClassePartition='5' and LibelleCourt like '" & rwComposante("LibelleCourt").ToString & "%' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
                Dim dtActivite As Data.DataTable = ExcecuteSelectQuery(query)

                Dim PourcentageRealisationActivite As Decimal = 0

                'Code max de la composante après insertion
                query = "Select MAX(RefTamp) from t_rg_Commentaire"
                CodeComposante = Val(ExecuteScallar(query))

                For Each rwActivite As DataRow In dtActivite.Rows
                    Dim RealisationActivitePrecedent As Decimal = GetMontantPrecedent_Rapports_Global(rwActivite("LibelleCourt"), DateDebutRSf, Bailleur)
                    Dim RealisationActivitePeriode As Decimal = ReduireMontant(GetDepense("Activité", rwActivite("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur), LimiteMontant)
                    Dim RealisationCumuleActivite As Decimal = ReduireMontant(GetDepense("Activité", rwActivite("LibelleCourt"), DateDebutAnnee, DateFinRSF, InitialeBailleur), LimiteMontant)
                    Dim RealisationTotalActivite As Decimal = ReduireMontant(GetDepense("Activité", rwActivite("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur, True), LimiteMontant)

                    Dim DotationActivitePeriode As Decimal = ReduireMontant(GetDotation("Activité", rwActivite("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur), LimiteMontant)
                    Dim DotationCumuleActivite As Decimal = ReduireMontant(GetDotation("Activité", rwActivite("LibelleCourt"), DateDebutAnnee, DateFinRSF, InitialeBailleur), LimiteMontant)
                    Dim DotationTotalActivite As Decimal = ReduireMontant(GetDotation("Activité", rwActivite("LibelleCourt"), DateDebutRSf, DateFinRSF, InitialeBailleur, True), LimiteMontant)

                    If DotationTotalActivite > 0 Then
                        PoucentageActivite = Round((RealisationTotalActivite * 100) / DotationTotalActivite, 2)
                    Else
                        PoucentageActivite = 0
                    End If

                    'TablePctPhysi(NbrsLigneTable, 0) = rwComposante("LibelleCourt") 'Libelle court Composante
                    'TablePctPhysi(NbrsLigneTable, 1) = rwActivite("LibelleCourt") 'Libelle court activité
                    'TablePctPhysi(NbrsLigneTable, 2) = PourcentageRealisationActivite '% realisation physique activité

                    If LeTexte(0).ToString <> "Dossier" Then 'Seulement pour un nouveau rapports
                        If ExisteCompoActivite = 0 Then
                            query = "INSERT INTO t_rsf_etatfondsparactivite_rapports_globals VALUES(NULL,'" & rwActivite("LibelleCourt") & "','" & rwActivite("LibellePartition") & "','" & RealisationActivitePrecedent.ToString().Replace(",", ".") & "','" & DotationActivitePeriode.ToString().Replace(",", ".") & "','" & RealisationActivitePeriode.ToString().Replace(",", ".") & "','" & DotationCumuleActivite.ToString().Replace(",", ".") & "','" & RealisationCumuleActivite.ToString().Replace(",", ".") & "','" & DotationTotalActivite.ToString().Replace(",", ".") & "','" & RealisationTotalActivite.ToString().Replace(",", ".") & "','" & PeriodeRSF & "','" & EnleverApost(Bailleur) & "','" & ProjetEnCours & "')"
                            ExecuteNonQuery(query)
                        End If

                        'Insertion Commentaire activité
                        If ExisteCommentaire = 0 Then
                            query = "insert into t_rg_Commentaire values(NULL, '" & PeriodeRSF & "', '" & rwActivite("LibelleCourt") & "', '" & EnleverApost(rwActivite("LibellePartition").ToString) & "', '" & RealisationTotalActivite * 1000 & "', '" & GetPourcentagePhysique(rwActivite("LibelleCourt"), "Activité") & "','" & PoucentageActivite & "','" & GetRecherCherCommentairePhysiq(rwActivite("LibelleCourt")) & "','A', '" & ProjetEnCours & "','" & CodeComposante & "')"
                            ExecuteNonQuery(query)
                        End If
                    End If

                    ' NbrsLigneTable += 1
                Next
            Next


            ''Vide la table   **************************
            'query = "delete from t_tampon_observation_rapports_global"
            'ExecuteNonQuery(query)

            ''Enregistrement des observations  *******************************************************
            'query = "select DISTINCT codepartition, LibelleCourt, LibellePartition from T_Partition where CodeClassePartition='" & CodeClassePartition & "' and  CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
            'Dim dt0 As DataTable = ExcecuteSelectQuery(query)

            'For Each rw In dt0.Rows

            '    query = "select SUM(QteNature*PUNature) as MontantActivite from t_besoinpartition where codepartition='" & rw("codepartition") & "' And CodeProjet='" & ProjetEnCours & "'"
            '    Dim SommeMontant = Val(ExecuteScallar(query))

            '    'Recherche pourcentage d'execution physique
            '    Dim PctPhysique As Decimal = 0

            '    query = "select COUNT(NumRapport) as Nbres, SUM(PourcentRealise) as Somme from t_rapportactivites where NumRapport like '" & rw("LibelleCourt") & "%'"
            '    Dim dt1 As DataTable = ExcecuteSelectQuery(query)

            '    For Each rw1 In dt1.Rows

            '        'If CodeClassePartition = 1 Or CodeClassePartition = 2 Then
            '        '    If CInt(rw1("Nbres")) = 0 Then
            '        '            PctPhysique = 0
            '        '        Else
            '        '            PctPhysique = Math.Round(CDec(rw1("Somme")) / CInt(rw1("Nbres")), 2)
            '        '        '    End If
            '        '        'ElseIf CodeClassePartition = 5 Then
            '        '        PctPhysique = Val(rw1("Somme").ToString)
            '        '   ' End If
            '    Next

            '    query = "insert into t_tampon_observation_rapports_global values(NULL, '" & rw("LibelleCourt") & "', '" & EnleverApost(rw("LibellePartition").ToString) & "', '" & SommeMontant & "', '" & PctPhysique & "','" & RechercherPctRealisation(rw("LibelleCourt")) & "','','" & CodeClassePartition & "', '" & ProjetEnCours & "')"
            '    ExecuteNonQuery(query)
            'Next


            ' Affichage de l'état ********************************************************************
            Dim LibelleProjet As String = ""
            query = "select NomProjet,PaysProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt6 As Data.DataTable = ExcecuteSelectQuery(query)

            For Each rw6 As DataRow In dt6.Rows
                Dim LeGouv = "GOUVERNEMENT DE L'ETAT DE " & MettreApost(rw6("PaysProjet").ToString.ToUpper)
                LibelleProjet = MettreApost(rw6("NomProjet").ToString.ToUpper())

                LibelleProjet = LeGouv & vbNewLine & LibelleProjet & vbNewLine & "UTILISATION DES FONDS PAR ACTIVITE DU PROJET"
                LibelleProjet = LibelleProjet & vbNewLine & "Pour la période du  " & DtDebPeriode.DateTime.ToShortDateString & "  au  " & DtFinPeriode.DateTime.ToShortDateString
                LibelleProjet = LibelleProjet & vbNewLine & " en francs CFA (FCFA)"
            Next

            LibelleProjet = "UTILISATION DES FONDS PAR COMPOSANTE ET ACTIVITE" & vbNewLine & "Pour la période du  " & DtDebPeriode.DateTime.ToShortDateString & "  au  " & DtFinPeriode.DateTime.ToShortDateString
            LibelleProjet += vbNewLine & "Montant en milliers de Francs"

            Dim Chemin As String = lineEtat & "\RapportsGlobals\"
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

            Dim NomRepertoire As String = DtDebPeriode.DateTime.ToShortDateString & "_" & DtFinPeriode.DateTime.ToShortDateString
            NomRepertoire = FormatFileName(NomRepertoire.ToString.Trim, "") & " - " & Bailleur
            NomRepertoire = line & "\RapportsGlobals\" & NomRepertoire & "\"

            ' Chemin du dossier en cours =>>> NomRepertoire
            report.ExportToDisk(ExportFormatType.PortableDocFormat, NomRepertoire & "EmploiFondsparActivite.pdf")

            ViewEmpFonActivite.ReportSource = report

            FinChargement()
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Function GetRecherCherCommentairePhysiq(ByVal LibelleCourt As String) As String
        Try
            query = "SELECT ConcluRapport FROM t_rapportactivites WHERE NumRapport LIKE '" & LibelleCourt & "%' and CodeProjet='" & ProjetEnCours & "' ORDER BY NumRapport DESC LIMIT 1"
            Return MettreApost(ExecuteScallar(query))
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

    End Function

    Private Function GetPourcentagePhysique(ByVal LibelleCourts As String, ByVal TypeActivites As String) As Decimal
        Try
            Dim PourcentagePhysiq As Decimal = 0
            Dim NbreActivite As Integer = 0
            query = "select SUM(PourcentRealise) as Somme from t_rapportactivites where NumRapport like '" & LibelleCourts & "%'"
            PourcentagePhysiq = Val(ExecuteScallar(query))

            If TypeActivites.ToString = "Composante" Then
                query = "select COUNT(DISTINCT LibelleCourt) from T_Partition where LibelleCourt like '" & LibelleCourts & "%' and LENGTH(LibelleCourt)='5' and CodeProjet='" & ProjetEnCours & "'"
                NbreActivite = Val(ExecuteScallar(query))
                If NbreActivite > 0 Then
                    PourcentagePhysiq = Round(PourcentagePhysiq / NbreActivite, 2)
                End If
            End If

            'query = "select DISTINCT SUBSTR(NumRapport,1,5) as LibelleCours from t_rapportactivites where NumRapport like '" & LibelleCourts & "%'"
            'Dim dt1 As DataTable = ExcecuteSelectQuery(query)

            'For Each rw1 In dt1.Rows
            '    query = "select SUM(PourcentRealise) as Somme from t_rapportactivites where NumRapport like '" & rw1("LibelleCours") & "%'"
            '    PourcentagePhysiq += Val(ExecuteScallar(query))
            '    NbreTour += 1
            'Next
            ' End If

            Return PourcentagePhysiq
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Function

    Private Function MontantCompActivite(ByVal LibelleCourts As String, ByVal TypeActivites As String) As Decimal
        Try
            'Variable contenant le Montant de la composante, de l'activite
            Dim MontantComp_Activite As Decimal = 0

            Dim DateDebut As String = dateconvert(DtDebPeriode.Text)
            Dim DateFin As String = dateconvert(DtFinPeriode.Text)

            'Recherche Montant composante et activités

            If TypeActivites.ToString = "Activité" Then
                ' query = "SELECT CodePartition from t_partition WHERE LibelleCourt='" & LibelleCourts & "' and CodeProjet='" & ProjetEnCours & "' and SUBSTR(DateDebutPartition, 1, 10)<='" & DateDebut & "' and SUBSTR(DateFinPartition, 1, 10)>= '" & DateFin & "'"
                query = "SELECT CodePartition from t_partition WHERE LibelleCourt='" & LibelleCourts & "' and CodeProjet='" & ProjetEnCours & "' and SUBSTR(DateDebutPartition, 1, 10)<='" & DateDebut & "' and SUBSTR(DateFinPartition, 1, 10)>= '" & DateFin & "'"

                Dim CodePartion As String = ExecuteScallar(query)
                query = "Select SUM(QteNature*PUNature) As MontantActivite from t_besoinpartition where CodePartition='" & CodePartion & "' and CodeProjet='" & ProjetEnCours & "'"
                MontantComp_Activite = Val(ExecuteScallar(query))
            Else
                query = "SELECT CodePartition from t_partition WHERE LibelleCourt like '" & LibelleCourts & "%' and CodeProjet='" & ProjetEnCours & "' and SUBSTR(DateDebutPartition, 1, 10)<='" & DateDebut & "' And SUBSTR(DateFinPartition, 1, 10)>= '" & DateFin & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)

                For Each rw0 In dt0.Rows
                    query = "Select SUM(QteNature*PUNature) As MontantActivite from t_besoinpartition where CodePartition='" & rw0("CodePartition") & "' and CodeProjet='" & ProjetEnCours & "'"
                    MontantComp_Activite += Val(ExecuteScallar(query))
                Next
            End If

            Return MontantComp_Activite
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

    End Function

    'Private Function RechercherPctRealisationPhysique(ByVal LibelleCour As String, ByVal TypeRecherche As String) As Decimal

    'Recherche pourcentage d'execution physique
    'Dim PctPhysique As Decimal = 0


    'query = "select COUNT(NumRapport) as Nbres, SUM(PourcentRealise) as Somme from t_rapportactivites where NumRapport like '" & LibelleCour & "%'"
    'Dim dt1 As DataTable = ExcecuteSelectQuery(query)

    'For Each rw1 In dt1.Rows

    '    If TypeRecherche.ToString = "Composante" Then
    '        If CInt(rw1("Nbres")) > 0 Then
    '            PctPhysique = Round(CDec(rw1("Somme")) / CInt(rw1("Nbres")))
    '        End If
    '    Else
    '        If CInt(rw1("Nbres")) > 0 Then
    '            PctPhysique = CDec(rw1("Somme"))
    '        End If
    '    End If
    'Next

    'If CodeClassePartition = 1 Or CodeClassePartition = 2 Then
    '    If CInt(rw1("Nbres")) = 0 Then
    '            PctPhysique = 0
    '        Else
    '            PctPhysique = Math.Round(CDec(rw1("Somme")) / CInt(rw1("Nbres")), 2)
    '        '    End If
    '        'ElseIf CodeClassePartition = 5 Then
    '        PctPhysique = Val(rw1("Somme").ToString)
    '   ' End If

    'For i = 0 To NbrsLigneTable - 1

    '    ExtraitLMibCour1 = Mid(TablePctPhysi(i, 0), 1, LibelleCour.Length) 'LibelleCourt Composante
    '    ExtraitLMibCour2 = Mid(TablePctPhysi(i, 1), 1, LibelleCour.Length) 'LibelleCourt activite
    '    PctEnCours = CDec(TablePctPhysi(i, 2)) 'Pourcentage réalisation

    '    If CodeClassePartition = 1 Then

    '        If ExtraitLMibCour1 = LibelleCour And ExtraitLMibCour2 = LibelleCour Then
    '            PctRealisation += PctEnCours
    '        End If

    '    ElseIf CodeClassePartition = 2 Then
    '        If ExtraitLMibCour2 = LibelleCour Then
    '            PctRealisation += PctEnCours
    '        End If
    '    ElseIf CodeClassePartition = 5 Then
    '        If LibelleCour = TablePctPhysi(i, 2) Then
    '            PctRealisation += PctEnCours
    '        End If
    '    End If
    'Next

    'Return PctPhysique
    ' End Function

    Private Sub ChargerObservation_Commentaire(ByVal PeriodeRGLS As String)
        Try
            Dim dtObser = New DataTable()
            dtObser.Columns.Clear()
            dtObser.Columns.Add("CodeX", Type.GetType("System.String"))
            dtObser.Columns.Add("RefTamp", Type.GetType("System.String"))
            dtObser.Columns.Add("Code", Type.GetType("System.String"))
            dtObser.Columns.Add("Activités", Type.GetType("System.String"))
            dtObser.Columns.Add("Coût de l'activité", Type.GetType("System.String"))
            dtObser.Columns.Add("% Execution physique", Type.GetType("System.String"))
            dtObser.Columns.Add("% Execution financier", Type.GetType("System.String"))
            dtObser.Columns.Add("Observation/Commentaires", Type.GetType("System.String"))
            dtObser.Columns.Add("TypeActivite", Type.GetType("System.String"))
            dtObser.Rows.Clear()

            Dim cptr As Decimal = 0
            'Dim TypeActivite As String = ""
            'Dim Libelle1 As String = ""
            'Dim Libelle2 As String = ""
            'Dim CodeComposante As String = ""
            'Dim CodeSousComposante As String = ""
            'Dim CodeComposanteCours As String = ""
            'Dim CodeSousComposanteCours As String = ""
            'Dim LibelleCompo_Sous As String = ""

            query = "select * from t_rg_Commentaire WHERE PeriodeRGLS='" & PeriodeRGLS & "' and CodeProjet='" & ProjetEnCours & "' ORDER BY CodeActivite ASC"
            Dim dt As DataTable = ExcecuteSelectQuery(query)

            If dt.Rows.Count > 0 Then
                'TypeActivite = dt.Rows(0)("TypeActivite").ToString

                'If TypeActivite.ToString = "1" Then
                '    Libelle1 = "composantes"
                '    Libelle2 = "Coût de la composante"
                'ElseIf TypeActivite.ToString = "2" Then
                '    Libelle1 = "Sous-composantes"
                '    Libelle2 = "Coût de la sous-composantes"
                'ElseIf TypeActivite.ToString = "5" Then
                '    Libelle1 = "Activités"
                '    Libelle2 = "Coût de l'activités"
                'End If

                For Each rw As DataRow In dt.Rows

                    'If TypeActivite.ToString = "2" Then
                    '    CodeSousComposante = Mid(rw("CodeActivite").ToString, 1, 1)

                    '    'Ajout de la composante
                    '    If CodeComposante <> CodeSousComposante Then
                    '        CodeComposante = CodeSousComposante

                    '        cptr += 1
                    '        Dim drS0 = dtObser.NewRow()

                    '        LibelleCompo_Sous = GetRegrouper(CodeComposante)

                    '        drS0("CodeX") = IIf((cptr Mod 2 = 0), "x", "").ToString

                    '        drS0("RefTamp") = ""
                    '        drS0("Code") = CodeComposante.ToString
                    '        drS0(Libelle1) = LibelleCompo_Sous.ToString
                    '        drS0(Libelle2) = ""
                    '        drS0("% Execution physique") = ""
                    '        drS0("% Execution financier") = ""
                    '        drS0("Observation/Commentaires") = "-----------------"
                    '        drS0("TypeActivite") = "1"

                    '        dtObser.Rows.Add(drS0)
                    '    End If

                    'ElseIf TypeActivite.ToString = "5" Then

                    '    CodeComposanteCours = Mid(rw("CodeActivite").ToString, 1, 1) 'Libelle cout composante 
                    '    CodeSousComposanteCours = Mid(rw("CodeActivite").ToString, 1, 2) 'Libelle cout sous-composante 

                    '    'Ajout de la composante
                    '    If CodeComposante <> CodeComposanteCours Then
                    '        CodeComposante = CodeComposanteCours

                    '        cptr += 1
                    '        Dim drS0 = dtObser.NewRow()
                    '        LibelleCompo_Sous = GetRegrouper(CodeComposante)
                    '        drS0("CodeX") = IIf((cptr Mod 2 = 0), "x", "").ToString

                    '        drS0("RefTamp") = ""
                    '        drS0("Code") = CodeComposante.ToString
                    '        drS0(Libelle1) = LibelleCompo_Sous.ToString
                    '        drS0(Libelle2) = ""
                    '        drS0("% Execution physique") = ""
                    '        drS0("% Execution financier") = ""
                    '        drS0("Observation/Commentaires") = "-----------------"
                    '        drS0("TypeModif") = "1"

                    '        dtObser.Rows.Add(drS0)
                    '    End If

                    '    'Ajout de la sous-composante
                    '    If CodeSousComposante <> CodeSousComposanteCours Then
                    '        CodeSousComposante = CodeSousComposanteCours

                    '        cptr += 1
                    '        Dim drS1 = dtObser.NewRow()
                    '        LibelleCompo_Sous = GetRegrouper(CodeSousComposante)
                    '        drS1("CodeX") = IIf((cptr Mod 2 = 0), "x", "").ToString

                    '        drS1("RefTamp") = ""
                    '        drS1("Code") = CodeSousComposante.ToString
                    '        drS1(Libelle1) = LibelleCompo_Sous.ToString
                    '        drS1(Libelle2) = ""
                    '        drS1("% Execution physique") = ""
                    '        drS1("% Execution financier") = ""
                    '        drS1("Observation/Commentaires") = "-----------------"
                    '        drS1("TypeModif") = "1"

                    '        dtObser.Rows.Add(drS1)
                    '    End If
                    'End If

                    'Ajout de la sous-composante

                    cptr += 1
                    Dim drS = dtObser.NewRow()
                    drS("CodeX") = IIf((cptr Mod 2 = 0), "x", "").ToString

                    drS("RefTamp") = rw("RefTamp")
                    drS("Code") = rw("CodeActivite").ToString
                    drS("Activités") = MettreApost(rw("LibelleCompo_Sous-Compo_Activite").ToString)
                    drS("Coût de l'activité") = AfficherMonnaie(rw("CooutActivite").ToString)
                    drS("% Execution physique") = rw("PctPhysiq").ToString & "%"
                    drS("% Execution financier") = rw("PctRSF").ToString & "%"

                    If rw("TypeActivite").ToString = "C" Then
                        drS("Observation/Commentaires") = "------------"
                    Else
                        drS("Observation/Commentaires") = MettreApost(rw("Obervation").ToString)
                    End If

                    drS("TypeActivite") = rw("TypeActivite").ToString

                    dtObser.Rows.Add(drS)
                Next
            End If

            GridComm.DataSource = dtObser

            ViewComm.Columns("RefTamp").Visible = False
            ViewComm.Columns("TypeActivite").Visible = False
            ViewComm.Columns("CodeX").Visible = False
            ViewComm.OptionsView.ColumnAutoWidth = True
            ViewComm.OptionsBehavior.Editable = True

            ViewComm.Columns("Code").OptionsColumn.AllowEdit = False
            ViewComm.Columns("TypeActivite").OptionsColumn.AllowEdit = False
            ViewComm.Columns("RefTamp").OptionsColumn.AllowEdit = False
            ViewComm.Columns("CodeX").OptionsColumn.AllowEdit = False
            ViewComm.Columns("% Execution physique").OptionsColumn.AllowEdit = False
            ViewComm.Columns("% Execution financier").OptionsColumn.AllowEdit = False

            If LectureDossier = True Then
                ViewComm.Columns("Observation/Commentaires").OptionsColumn.AllowEdit = False
            Else
                ViewComm.Columns("Observation/Commentaires").OptionsColumn.AllowEdit = True
            End If

            ViewComm.Columns("Coût de l'activité").OptionsColumn.AllowEdit = False
            ViewComm.Columns("Activités").OptionsColumn.AllowEdit = False

            Dim txtNumero As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
            AddHandler txtNumero.EditValueChanging, AddressOf txtNumero_EditValueChanging

            ViewComm.Columns("Observation/Commentaires").ColumnEdit = txtNumero

            ViewComm.Columns("Code").Width = 30
            ViewComm.Columns("% Execution physique").Width = 30
            ViewComm.Columns("% Execution financier").Width = 30
            ViewComm.Columns("Coût de l'activité").Width = 50
            ViewComm.Columns("Activités").Width = 100

            ViewComm.Columns("Code").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewComm.Columns("Observation/Commentaires").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewComm.Columns("% Execution physique").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewComm.Columns("% Execution financier").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            ViewComm.Columns("Coût de l'activité").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

            ViewComm.Appearance.Row.Font = New System.Drawing.Font("Times New Roman", 10, FontStyle.Regular)
            ColorRowGrid(ViewComm, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewComm, "[TypeActivite]='C'", Color.LightGray, "Times New Roman", 11, FontStyle.Bold, Color.Black)

            PageCommentaire.PageVisible = True
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try

    End Sub

    Private Sub txtNumero_EditValueChanging(ByVal sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        If ViewComm.RowCount > 0 Then
            drx = ViewComm.GetDataRow(ViewComm.FocusedRowHandle)
            If drx("TypeActivite").ToString = "C" Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Rapport sur la passation des marches"

    Private Sub ChkPassation_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPassation.CheckedChanged
        If FermerDossierEnCours = False Then
            If ChkPassation.Checked = True Then
                PagePassation.PageVisible = True
                XtraTabControl2.SelectedTabPage = PagePassation

                If CheckSectionB.Checked = False Then CheckSectionB.Checked = True
                If ChkExamenResultat.Checked = False Then ChkExamenResultat.Checked = True
                query = "Update t_rg_rapports_globals set PassationMarches='OUI' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

            Else
                PagePassation.PageVisible = False

                'If CheckSectionB.Checked = False Then CheckSectionB.Checked = True
                'If ChkExamenResultat.Checked = False Then ChkExamenResultat.Checked = True
                query = "Update t_rg_rapports_globals set PassationMarches='NON' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)

            End If

            EnregistrerTableMatiere()
        Else
            PagePassation.PageVisible = False
            ViewPassation.ReportSource = Nothing
            ViewPassation.Refresh()
            If CheckSectionB.Checked = True Then CheckSectionB.Checked = False
            If ChkExamenResultat.Checked = True Then ChkExamenResultat.Checked = False
        End If
    End Sub

#End Region

#Region "Rapport sur les mesures de sauvegarde et environnemental"

    Private Sub ChkSaveMesure_CheckedChanged(sender As Object, e As EventArgs) Handles ChkSaveMesure.CheckedChanged
        If FermerDossierEnCours = False Then
            If ChkSaveMesure.Checked = True Then
                PageMesure.PageVisible = True
                XtraTabControl2.SelectedTabPage = PageMesure

                If CheckSectionB.Checked = False Then CheckSectionB.Checked = True
                If ChkExamenResultat.Checked = False Then ChkExamenResultat.Checked = True

                query = "Update t_rg_rapports_globals set MesureSauvegarde='OUI' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            Else
                PageMesure.PageVisible = False
                query = "Update t_rg_rapports_globals set MesureSauvegarde='NON' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            EnregistrerTableMatiere()
        Else
            PageMesure.PageVisible = False
            ViewMesure.ReportSource = Nothing
            ViewMesure.Refresh()
            If CheckSectionB.Checked = True Then CheckSectionB.Checked = False
            If ChkExamenResultat.Checked = True Then ChkExamenResultat.Checked = False
        End If
    End Sub

#End Region

#Region "Rapport sur le suivi et l'evaluation du prijet"

    Private Sub ChkProjet_CheckedChanged(sender As Object, e As EventArgs) Handles ChkProjet.CheckedChanged

        If FermerDossierEnCours = False Then
            If ChkProjet.Checked = True Then
                PageSuiviProjet.PageVisible = True
                XtraTabControl2.SelectedTabPage = PageSuiviProjet

                If CheckSectionB.Checked = False Then CheckSectionB.Checked = True
                If ChkExamenResultat.Checked = False Then ChkExamenResultat.Checked = True

                query = "Update t_rg_rapports_globals set SuiviEvaluationProjet='OUI' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            Else
                PageSuiviProjet.PageVisible = False
                query = "Update t_rg_rapports_globals set SuiviEvaluationProjet='NON' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            EnregistrerTableMatiere()
        Else
            PageSuiviProjet.PageVisible = False
            ViewSuiviEvaluatinProjet.ReportSource = Nothing
            ViewSuiviEvaluatinProjet.Refresh()
            If CheckSectionB.Checked = True Then CheckSectionB.Checked = False
            If ChkExamenResultat.Checked = True Then ChkExamenResultat.Checked = False
        End If
    End Sub

#End Region

#Region "ANNEXE"
    Private Sub CheckAnnexe_CheckedChanged(sender As Object, e As EventArgs) Handles CheckAnnexe.CheckedChanged
        If FermerDossierEnCours = False Then
            If CheckAnnexe.Checked = True Then
                'CheckAnnexe1.Checked = True
                CheckAnnexe1.Enabled = True
                CheckAnnexe2.Enabled = True
                ' BtSelectPJ.Enabled = True
                query = "Update t_rg_rapports_globals set Annexe='OUI' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            Else
                'CheckAnnexe1.Checked = False
                CheckAnnexe1.Enabled = False
                CheckAnnexe2.Enabled = False
                ' BtSelectPJ.Enabled = False

                query = "Update t_rg_rapports_globals set Annexe='NON' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If
            EnregistrerTableMatiere()
        End If
    End Sub

    Private Sub CheckAnnexe1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckAnnexe1.CheckedChanged
        If FermerDossierEnCours = False Then
            If CheckAnnexe1.Checked = True Then
                query = "Update t_rg_rapports_globals set Annexe1='OUI' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            Else
                query = "Update t_rg_rapports_globals set Annexe1='NON' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            EnregistrerTableMatiere()
        End If
    End Sub

    Private Sub CheckAnnexe2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckAnnexe2.CheckedChanged
        If FermerDossierEnCours = False Then
            If CheckAnnexe2.Checked = True Then
                query = "Update t_rg_rapports_globals set Annexe2='OUI' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            Else
                query = "Update t_rg_rapports_globals set Annexe2='NON' where PeriodeRGLS='" & DtDebPeriode.DateTime.ToShortDateString & " - " & DtFinPeriode.DateTime.ToShortDateString & "' and CodeProjet='" & ProjetEnCours & "'"
                ExecuteNonQuery(query)
            End If

            EnregistrerTableMatiere()
        End If
    End Sub

#End Region

#Region "Context Menuscript"

    Private Sub NewAffichargeRapports()

        Try
            drx = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)
            Dim periode As String = drx("Période").ToString

            DebutChargement(True, "Récuperation des données en cours...")

            FermerDossierEnCours = True
            InitialiserCaseaCocher(False)
            FermerBouton(False)

            Panel4PlanRapport.Enabled = Not LectureDossier
            NomDossierEnCours = ""
            Bailleur = drx("Bailleur").ToString

            DtDebPeriode.Text = CDate(periode.ToString.Split("-")(0))
            DtFinPeriode.Text = CDate(periode.ToString.Split("-")(1))

            BtNewRapports.Text = "Dossier du  " & drx("Période").ToString

            Dim Chk1 As String = "NON"
            Dim Chk2 As String = "NON"
            Dim Chk3 As String = "NON"
            Dim Chk4 As String = "NON"
            Dim Chk5 As String = "NON"
            Dim Chk6 As String = "NON"
            Dim Chk7 As String = "NON"
            Dim Chk8 As String = "NON"
            Dim Chk9 As String = "NON"
            Dim Chk10 As String = "NON"
            Dim Chk11 As String = "NON"
            Dim nomFichier As String = ""

            query = "select * from t_rg_rapports_globals where PeriodeRGLS='" & periode & "' AND Bailleur='" & EnleverApost(Bailleur.ToString) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)

            For Each rw As DataRow In dt.Rows
                Chk1 = IIf(rw("Identification").ToString = "OUI", "OUI", "NON").ToString
                Chk2 = IIf(rw("DonneFinanciere").ToString = "OUI", "OUI", "NON").ToString
                Chk3 = IIf(rw("Introduction").ToString = "OUI", "OUI", "NON").ToString
                Chk4 = IIf(rw("RapportSuiviFinancier").ToString = "OUI", "OUI", "NON").ToString
                Chk5 = IIf(rw("PassationMarches").ToString = "OUI", "OUI", "NON").ToString
                Chk6 = IIf(rw("MesureSauvegarde").ToString = "OUI", "OUI", "NON").ToString
                Chk7 = IIf(rw("SuiviEvaluationProjet").ToString = "OUI", "OUI", "NON").ToString
                Chk8 = IIf(rw("PlanActionChrono").ToString = "OUI", "OUI", "NON").ToString
                Chk9 = IIf(rw("Annexe").ToString = "OUI", "OUI", "NON").ToString
                Chk10 = IIf(rw("Annexe1").ToString = "OUI", "OUI", "NON").ToString
                Chk11 = IIf(rw("Annexe2").ToString = "OUI", "OUI", "NON").ToString
            Next

            FermerDossierEnCours = False

            If (Chk1 = "OUI") Then CheckSectionA.Checked = True
            If (Chk2 = "OUI") Then ChkDonneFinanc.Checked = True
            If Chk3 = "OUI" Or Chk4 = "OUI" Or Chk5 = "OUI" Or Chk6 = "OUI" Then CheckSectionB.Checked = True
            If Chk4 = "OUI" Or Chk5 = "OUI" Or Chk6 = "OUI" Then ChkExamenResultat.Checked = True
            If (Chk3 = "OUI") Then CheckIntroduc.Checked = True
            If (Chk4 = "OUI") Then ChkSuiviFinanc.Checked = True
            If (Chk5 = "OUI") Then ChkPassation.Checked = True
            If (Chk6 = "OUI") Then ChkSaveMesure.Checked = True
            If (Chk7 = "OUI") Then ChkProjet.Checked = True
            If (Chk8 = "OUI") Then CheckChrono.Checked = True
            If (Chk9 = "OUI") Then CheckAnnexe.Checked = True
            If (Chk10 = "OUI") Then CheckAnnexe1.Checked = True
            If (Chk11 = "OUI") Then CheckAnnexe2.Checked = True

            'If (Chk6 = "OUI") Then
            '    ChkPiecesJointes.Checked = True
            '    ChargerPJ(periode, drx("Bailleur"))
            'End If

            SaisieDetailsRapp.ReadOnly = LectureDossier
            FinChargement()
        Catch ex As Exception
            FailMsg(ex.ToString)
        End Try
    End Sub

    Private Sub GridRapPrec_Click(sender As Object, e As EventArgs) Handles GridRapPrec.Click
        If ViewRapPrec.RowCount > 0 Then
            drx = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)

            ColorRowGrid(ViewRapPrec, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewRapPrec, "[Période]='" & drx("Période").ToString & "'", Color.Navy, "Times New Roman", 10, FontStyle.Regular, Color.White)

        End If
    End Sub

    Private Sub OuvrirAncRapport_Click(sender As Object, e As EventArgs) Handles OuvrirAncRapport.Click
        If ViewRapPrec.RowCount > 0 Then
            LectureDossier = True
            NepasExecuteleMemeCode = True
            NewAffichargeRapports()
            NepasExecuteleMemeCode = False
        End If

    End Sub

    Private Sub MenuStripRapport_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MenuStripRapport.Opening
        If ViewRapPrec.RowCount = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ActualiserAncRapport_Click(sender As Object, e As EventArgs) Handles ActualiserAncRapport.Click
        If ViewRapPrec.RowCount > 0 Then
            LectureDossier = False
            NepasExecuteleMemeCode = True
            NewAffichargeRapports()
            NepasExecuteleMemeCode = False
        End If
    End Sub
#End Region

    'Private Sub CheckCommentaire_CheckedChanged(sender As Object, e As EventArgs) Handles CheckCommentaire.CheckedChanged
    '    If (CheckCommentaire.Checked = True) Then
    '        PageCommentaire.PageVisible = True
    '        XtraTabControl1.SelectedTabPage = PageCommentaire
    '        'ViewDAO.Enabled = True
    '        'ViewDp.Enabled = True
    '    Else
    '        PageCommentaire.PageVisible = False
    '        'ViewExecution.ReportSource = Nothing
    '        'ViewExecution.Enabled = False
    '    End If
    'End Sub

End Class