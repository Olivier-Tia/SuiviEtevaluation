Imports MySql.Data.MySqlClient
Imports System.Math
Imports System.IO
Imports DevExpress.XtraRichEdit
Imports System.Windows.Forms.DataVisualization.Charting
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions


Public Class RAPortGbolActivtes

    Dim DateDuJour As Date = Now.ToShortDateString
    Dim BailDuJour As String = ""
    Dim MonnaieDuJour As String = ""
    Dim montTravaux As Decimal = 0
    Dim montFournitures As Decimal = 0
    Dim montConsultants As Decimal = 0
    Dim imprEnCours As Boolean = False
    Dim NomDossier As String = ""
    Dim NumImprim As Decimal = 1
    Dim FondDecais As Decimal = 0
    Dim FondBaillr As Decimal = 0

    Private Sub RAPortGbolActivtes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RibbonControl1.Minimized = True
    End Sub

    Private Sub ActivImprim()

        'If (ChkDetails.Checked = True Or ChkStatistic.Checked = True Or ChkSituation.Checked = True Or ChkAttente.Checked = True Or ChkExecution.Checked = True Or ChkDAO.Checked = True Or ChkGraph.Checked = True) Then
        '    btAnnuler.Enabled = True
        'Else
        '    btAnnuler.Enabled = False
        'End If

    End Sub

    Private Sub BtNewRapp_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtNewRapp.Click

        imprEnCours = False
        montTravaux = 0
        montFournitures = 0
        montConsultants = 0

        RsmBailleurDevise.ShowDialog()
        DebutChargement()

        If (ReponseDialog = "") Then
            Exit Sub
        End If
        'BtImprimerRSM.Enabled = True
        PanelControl1.Enabled = True
        LblDate.Text = Now.ToShortDateString

        NomDossier = line & "\RSM\" & ProjetEnCours
        If (Directory.Exists(NomDossier) = False) Then
            Directory.CreateDirectory(NomDossier)
        End If
        NomDossier = NomDossier & "\" & Now.ToShortDateString.Replace("/", "_")
        If (Directory.Exists(NomDossier) = False) Then
            Directory.CreateDirectory(NomDossier)
        End If
        NomDossier = NomDossier & "\" & ReponseDialog
        If (Directory.Exists(NomDossier) = False) Then
            Directory.CreateDirectory(NomDossier)
        End If

        BailDuJour = ReponseDialog
        MonnaieDuJour = ExceptRevue

        InfosEtatProg(ReponseDialog)
        InfosExecutionRsm(ReponseDialog, ExceptRevue)
        InfosStatistic(ReponseDialog, ExceptRevue)
        InfosDAO(ReponseDialog)
        GraphMarcheApprouve(ExceptRevue)
        GraphMontantDecaisse(ExceptRevue)
        GraphPrioriPosteriori(ExceptRevue)
        GraphDecaissement(ExceptRevue)
        GraphDepense(ExceptRevue)
        GraphTypeMarche(ExceptRevue)
        GraphMethodeMarche(ReponseDialog, ExceptRevue)
        GraphDecaisCamembere()
        GraphMarcheAttribue(ExceptRevue)

        If (BailDuJour = "Tous") Then
            ReponseDialog = "DU PROJET"
        Else
            ReponseDialog = "DE " & BailDuJour
        End If


        Label1.Text = "PLAN DE RAPPORT " & ReponseDialog & " (" & MonnaieDuJour & ")"

        ReponseDialog = ""
        ExceptRevue = ""

        FinChargement()
    End Sub

    Private Sub AfficherEtatProgEtat()

        Dim EnteteR As String = ""
        query = "select Nomprojet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            EnteteR = MettreApost(rw(0).ToString).ToUpper & vbNewLine & "(" & ProjetEnCours.ToUpper & ")" &
            vbNewLine & "Situation des Marchés de Travaux et Fournitures de ETAT à la date du " & Now.ToShortDateString
        Next

        Dim Chemin As String = lineEtat & "\RSM\"
        Dim report As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        Dim DatSet = New DataSet
        report.Load(Chemin & "EtatProgressionRSMEtat.rpt")

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
        report.SetParameterValue("EnteteRapport", EnteteR)

    End Sub

    Private Sub GraphDecaisCamembere()

        ChartGraph7.ChartAreas.Clear()
        ChartGraph7.Series.Clear()
        ChartGraph7.Legends.Clear()

        Dim chartArea1 As New ChartArea()
        ChartGraph7.ChartAreas.Add(chartArea1)

        'Instanciation series sans Name
        Dim series1 As New Series("DecaisseC")
        ' Add data points to the first series
        Dim Baille1 As String
        If (BailDuJour = "Tous") Then
            Baille1 = "du projet"
        Else
            Baille1 = "de l'" & BailDuJour
        End If

        ChkGraph7.Text = "G7: Décaissements/Fonds " & Baille1

        Dim separDec As String = ","
        If (MonnaieDuJour = "US$") Then separDec = "."

        Dim prctDecC As Decimal = 0
        If (FondBaillr <> 0) Then prctDecC = Math.Round((FondDecais * 100) / (FondBaillr), 3)

        series1.Points.AddXY(AfficherMonnaie(FondBaillr.ToString).Replace(",", separDec) & " (" & MonnaieDuJour & ")" & vbNewLine & "(" & (100 - prctDecC).ToString.Replace(",", separDec) & "%)", (FondBaillr - FondDecais))
        series1.Points.AddXY(AfficherMonnaie(FondDecais.ToString).Replace(",", separDec) & " (" & MonnaieDuJour & ")" & vbNewLine & "(" & prctDecC.ToString.Replace(",", separDec) & "%)", FondDecais)

        series1.Points(0).Color = Color.LightBlue
        series1.Points(1).Color = Color.LightSalmon
        series1.Points(0).Font = New Font("Arial", 8, FontStyle.Bold)
        series1.Points(1).Font = New Font("Arial", 8, FontStyle.Bold)

        ' Add series to the chart
        series1.ChartArea = chartArea1.Name
        ChartGraph7.Series.Add(series1)

        ChartGraph7.BackImage = line & "\ClearpNew6.png"
        ChartGraph7.BackImageWrapMode = ChartImageWrapMode.Unscaled
        ChartGraph7.BackImageAlignment = ChartImageAlignmentStyle.BottomLeft

        ChartGraph7.Titles("Titre1").Text = "GRAPHIQUE DU MONTANT TOTAL DECAISSE " & Baille1.ToUpper & " POUR UN MONTANT TOTAL DE" & vbNewLine & AfficherMonnaie(FondBaillr).Replace(",", separDec) & " " & MonnaieDuJour & "   DE FINANCEMENT " & Baille1.ToUpper & " (à la date du " & Now.ToShortDateString & ")" & vbNewLine & " "
        ChartGraph7.Titles("Titre1").Position.Auto = True
        ChartGraph7.Titles("Titre1").DockedToChartArea = chartArea1.Name
        ChartGraph7.Titles("Titre1").Docking = Docking.Bottom
        ChartGraph7.Titles("Titre1").IsDockedInsideChartArea = False
        ChartGraph7.Titles("Titre1").Font = New Font("Arial", 10, FontStyle.Bold)

        Dim legend1 As New Legend
        legend1.Name = "Leg1"
        legend1.Title = "Légende"
        ChartGraph7.Legends.Add(legend1)
        ChartGraph7.Series(0).IsVisibleInLegend = False
        ChartGraph7.Legends("Leg1").CustomItems.Clear()
        ChartGraph7.Legends("Leg1").CustomItems.Add(New LegendItem("Montant non décaissé", Color.LightBlue, ""))
        ChartGraph7.Legends("Leg1").CustomItems.Add(New LegendItem("Montant décaissé", Color.LightSalmon, ""))
        ChartGraph7.Legends("Leg1").IsDockedInsideChartArea = True

        '*************************************************************************************
        ChartGraph7.ChartAreas(0).InnerPlotPosition.Auto = False
        ChartGraph7.ChartAreas(0).InnerPlotPosition.Height = 100
        ChartGraph7.ChartAreas(0).InnerPlotPosition.Width = 95

        ChartGraph7.ChartAreas(0).Position.Height = 85
        ChartGraph7.ChartAreas(0).Position.Width = 100

        ChartGraph7.ChartAreas("ChartArea1").BackColor = Color.Transparent
        ChartGraph7.ChartAreas("ChartArea1").BackSecondaryColor = Color.Transparent
        '******************************************************************************************
        ChartGraph7.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
        ChartGraph7.Series(0).ChartType = SeriesChartType.Pie
        ChartGraph7.ChartAreas("ChartArea1").Area3DStyle.LightStyle = LightStyle.Realistic
        ChartGraph7.ChartAreas(0).Area3DStyle.PointDepth = 40


    End Sub

    Private Sub GraphMethodeMarche(ByVal Baill0 As String, ByVal Devise As String)
        Dim separDecim As String = ","
        If (Devise = "US$") Then separDecim = "."
        Dim MontTotal As Decimal = 0

        ChartGraph6.ChartAreas.Clear()
        ChartGraph6.Series.Clear()
        ChartGraph6.Legends.Clear()

        Dim chartArea1 As New ChartArea()
        ChartGraph6.ChartAreas.Add(chartArea1)

        'Instanciation series sans Name
        Dim series1 As New Series("Methode")
        ' Add data points to the first series
        Dim Baille1 As String = ""
        If (Baill0 = "Tous") Then
            Baille1 = "DU PROJET"
        Else
            Baille1 = "DE L'" & BailDuJour
        End If


        Dim sqlBaille As String = "%"
        If (Baill0 <> "Tous") Then sqlBaille = Baill0

        Dim LesMethod(50) As String
        Dim LesMontants(50) As Decimal
        Dim NbMethod As Decimal = 0

        Dim NbVirg As Integer = 0
        If (Devise <> "FCFA") Then NbVirg = 2
        'Conversion du montant
        Dim tauxConv As Decimal = 1

        query = "select TauxDevise from T_Devise where AbregeDevise='" & Devise & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            tauxConv = CDec(rw(0))
        Next

        query = "select MethodeMarche from T_Marche where InitialeBailleur like '" & sqlBaille & "' and CodeProjet='" & ProjetEnCours & "' group by MethodeMarche"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows

            Dim MontMethod As Decimal = 0
            query = "select MontantEstimatif from T_Marche where InitialeBailleur like '" & sqlBaille & "' and MethodeMarche='" & rw1(0) & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
            For Each rw2 As DataRow In dt2.Rows
                MontMethod = MontMethod + CDec(rw2(0))
            Next

            LesMethod(NbMethod) = rw1(0).ToString
            LesMontants(NbMethod) = Math.Round(MontMethod / tauxConv, NbVirg)
            MontTotal = MontTotal + Math.Round(MontMethod / tauxConv, NbVirg)
            NbMethod = NbMethod + 1
        Next


        Dim Couleur() As Color = {Color.LightBlue, Color.LightSalmon, Color.LightGreen, Color.LightGray, Color.LightCoral, Color.Moccasin, Color.LightCyan, Color.LightGoldenrodYellow, Color.LightPink, Color.LightSeaGreen, Color.LightSlateGray}
        For k As Integer = 0 To NbMethod - 1
            series1.Points.AddXY("", LesMontants(k))
        Next

        For pt As Decimal = 0 To NbMethod - 1
            series1.Points(pt).Color = Couleur(pt)
            series1.Points(pt).Font = New Font("Arial", 8, FontStyle.Bold)
        Next

        ' Add series to the chart
        series1.ChartArea = chartArea1.Name
        ChartGraph6.Series.Add(series1)

        ChartGraph6.BackImage = line & "\ClearpNew6.png"
        ChartGraph6.BackImageWrapMode = ChartImageWrapMode.Unscaled
        ChartGraph6.BackImageAlignment = ChartImageAlignmentStyle.BottomLeft

        ChartGraph6.Titles("Titre1").Text = "GRAPHIQUE DES MONTANTS DES MARCHES PAR METHODE DE PASSATION " & Baille1 & " POUR UN MONTANT TOTAL DE" & vbNewLine & AfficherMonnaie(MontTotal.ToString).Replace(",", separDecim) & " " & Devise & "   DE MARCHES PREVUS" & " (à la date du " & Now.ToShortDateString & ")" & vbNewLine & " "
        ChartGraph6.Titles("Titre1").Position.Auto = True
        ChartGraph6.Titles("Titre1").DockedToChartArea = chartArea1.Name
        ChartGraph6.Titles("Titre1").Docking = Docking.Bottom
        ChartGraph6.Titles("Titre1").IsDockedInsideChartArea = False
        ChartGraph6.Titles("Titre1").Font = New Font("Arial", 10, FontStyle.Bold)

        Dim legend1 As New Legend
        legend1.Name = "Leg1"
        legend1.Title = "Légende (" & Devise & ")"
        ChartGraph6.Legends.Add(legend1)
        ChartGraph6.Series(0).IsVisibleInLegend = False
        ChartGraph6.Legends("Leg1").CustomItems.Clear()
        For z As Decimal = 0 To NbMethod - 1
            ChartGraph6.Legends("Leg1").CustomItems.Add(New LegendItem(LesMethod(z) & " = " & AfficherMonnaie(LesMontants(z).ToString).Replace(",", separDecim) & " (" & Math.Round((LesMontants(z) * 100) / MontTotal, 3).ToString & "%)", Couleur(z), ""))
        Next
        ChartGraph6.Legends("Leg1").IsDockedInsideChartArea = True

        '*************************************************************************************
        ChartGraph6.ChartAreas(0).InnerPlotPosition.Auto = False
        ChartGraph6.ChartAreas(0).InnerPlotPosition.Height = 100
        ChartGraph6.ChartAreas(0).InnerPlotPosition.Width = 95

        ChartGraph6.ChartAreas(0).Position.Height = 85
        ChartGraph6.ChartAreas(0).Position.Width = 100

        ChartGraph6.ChartAreas("ChartArea1").BackColor = Color.Transparent
        ChartGraph6.ChartAreas("ChartArea1").BackSecondaryColor = Color.Transparent
        '******************************************************************************************
        ChartGraph6.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
        ChartGraph6.Series(0).ChartType = SeriesChartType.Pie
        ChartGraph6.ChartAreas("ChartArea1").Area3DStyle.LightStyle = LightStyle.Realistic
        ChartGraph6.ChartAreas(0).Area3DStyle.PointDepth = 40


    End Sub

    Private Sub GraphTypeMarche(ByVal Devise As String)

        Dim MontTotal As Decimal = montTravaux + montFournitures + montConsultants
        ChartGraph5.ChartAreas.Clear()
        ChartGraph5.Series.Clear()
        ChartGraph5.Legends.Clear()

        Dim chartArea1 As New ChartArea()
        ChartGraph5.ChartAreas.Add(chartArea1)

        'Instanciation series sans Name
        Dim series1 As New Series("Decaisse")
        ' Add data points to the first series
        Dim Baille1 As String
        If (BailDuJour = "Tous") Then
            Baille1 = "DU PROJET"
        Else
            Baille1 = "DE L'" & BailDuJour
        End If

        Dim NbVirg As Integer = 0
        If (Devise <> "FCFA") Then NbVirg = 2
        Dim separDec As String = ","
        If (Devise = "US$") Then separDec = "."

        'Conversion du montant
        Dim tauxConv As Decimal = 1
        query = "select TauxDevise from T_Devise where AbregeDevise='" & Devise & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            tauxConv = CDec(rw(0))
        Next

        montTravaux = Math.Round(montTravaux / tauxConv, NbVirg)
        montFournitures = Math.Round(montFournitures / tauxConv, NbVirg)
        montConsultants = Math.Round(montConsultants / tauxConv, NbVirg)
        MontTotal = Math.Round(MontTotal / tauxConv, NbVirg)

        If (MontTotal = 0) Then Exit Sub

        Dim prctTrav As Decimal = Math.Round((montTravaux * 100) / (MontTotal), 3)
        Dim prctFour As Decimal = Math.Round((montFournitures * 100) / (MontTotal), 3)
        Dim prctCons As Decimal = Math.Round((montConsultants * 100) / (MontTotal), 3)

        series1.Points.AddXY(AfficherMonnaie(montTravaux.ToString).Replace(",", separDec) & " (" & Devise & ")" & vbNewLine & "(" & prctTrav.ToString.Replace(",", separDec) & "%)", montTravaux)
        series1.Points.AddXY(AfficherMonnaie(montFournitures.ToString).Replace(",", separDec) & " (" & Devise & ")" & vbNewLine & "(" & prctFour.ToString.Replace(",", separDec) & "%)", montFournitures)
        series1.Points.AddXY(AfficherMonnaie(montConsultants.ToString).Replace(",", separDec) & " (" & Devise & ")" & vbNewLine & "(" & prctCons.ToString.Replace(",", separDec) & "%)", montConsultants)

        Dim MonTT As String = ""
        MonTT = AfficherMonnaie(MontTotal.ToString).Replace(",", separDec) & " " & Devise

        series1.Points(0).Color = Color.LightBlue
        series1.Points(1).Color = Color.LightSalmon
        series1.Points(2).Color = Color.LightGreen
        series1.Points(0).Font = New Font("Arial", 8, FontStyle.Bold)
        series1.Points(1).Font = New Font("Arial", 8, FontStyle.Bold)
        series1.Points(2).Font = New Font("Arial", 8, FontStyle.Bold)

        ' Add series to the chart
        series1.ChartArea = chartArea1.Name
        ChartGraph5.Series.Add(series1)

        ChartGraph5.BackImage = line & "\ClearpNew6.png"
        ChartGraph5.BackImageWrapMode = ChartImageWrapMode.Unscaled
        ChartGraph5.BackImageAlignment = ChartImageAlignmentStyle.BottomLeft

        ChartGraph5.Titles("Titre1").Text = "GRAPHIQUE DES MONTANTS PAR TYPE DE MARCHE " & Baille1 & " POUR UN MONTANT TOTAL DE" & vbNewLine & MonTT & "   DE MARCHES PREVUS (à la date du " & Now.ToShortDateString & ")" & vbNewLine & " "
        ChartGraph5.Titles("Titre1").Position.Auto = True
        ChartGraph5.Titles("Titre1").DockedToChartArea = chartArea1.Name
        ChartGraph5.Titles("Titre1").Docking = Docking.Bottom
        ChartGraph5.Titles("Titre1").IsDockedInsideChartArea = False
        ChartGraph5.Titles("Titre1").Font = New Font("Arial", 10, FontStyle.Bold)

        Dim legend1 As New Legend
        legend1.Name = "Leg1"
        legend1.Title = "Légende"
        ChartGraph5.Legends.Add(legend1)
        ChartGraph5.Series(0).IsVisibleInLegend = False
        ChartGraph5.Legends("Leg1").CustomItems.Clear()
        ChartGraph5.Legends("Leg1").CustomItems.Add(New LegendItem("Marchés de Travaux", Color.LightBlue, ""))
        ChartGraph5.Legends("Leg1").CustomItems.Add(New LegendItem("Marchés de Fournitures", Color.LightSalmon, ""))
        ChartGraph5.Legends("Leg1").CustomItems.Add(New LegendItem("Marchés de Consultants", Color.LightGreen, ""))
        ChartGraph5.Legends("Leg1").IsDockedInsideChartArea = True

        '*************************************************************************************
        ChartGraph5.ChartAreas(0).InnerPlotPosition.Auto = False
        ChartGraph5.ChartAreas(0).InnerPlotPosition.Height = 100
        ChartGraph5.ChartAreas(0).InnerPlotPosition.Width = 95

        ChartGraph5.ChartAreas(0).Position.Height = 85
        ChartGraph5.ChartAreas(0).Position.Width = 100

        ChartGraph5.ChartAreas("ChartArea1").BackColor = Color.Transparent
        ChartGraph5.ChartAreas("ChartArea1").BackSecondaryColor = Color.Transparent
        '******************************************************************************************
        ChartGraph5.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
        ChartGraph5.Series(0).ChartType = SeriesChartType.Pie
        ChartGraph5.ChartAreas("ChartArea1").Area3DStyle.LightStyle = LightStyle.Realistic
        ChartGraph5.ChartAreas(0).Area3DStyle.PointDepth = 40


    End Sub

    Private Sub GraphDepense(ByVal Devise As String)

        Dim separDecim As String = ","
        If (Devise = "US$") Then separDecim = "."
        Dim MontTotal As String = ""

        ChartGraph4.ChartAreas.Clear()
        ChartGraph4.Series.Clear()
        ChartGraph4.Legends.Clear()

        Dim chartArea1 As New ChartArea()
        ChartGraph4.ChartAreas.Add(chartArea1)

        'Instanciation series sans Name
        Dim series1 As New Series("Depense")
        Dim series2 As New Series("Zero")
        Dim NbVirg As Integer = 0
        If (Devise <> "FCFA") Then NbVirg = 2
        'Conversion du montant
        Dim tauxConv As Decimal = 1
        query = "select TauxDevise from T_Devise where AbregeDevise='" & Devise & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            tauxConv = CDec(rw(0))
        Next

        Dim TotProjet As Decimal = 0
        query = "select C.MontantConvention from T_Convention as C,T_Bailleur as B where C.CodeBailleur=B.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows
            TotProjet = TotProjet + CDec(rw1(0))
        Next

        TotProjet = Math.Round(TotProjet / tauxConv, NbVirg)
        series1.Points.AddXY("Debut", 0)
        series2.Points.Add(0)

        Dim AnEnCours As String = Mid(Now.ToShortDateString, 7)
        Dim DebProjet As String = ""
        Dim FinProjet As String = ""
        query = "select DateDebutProjetMV,DateFinProjetMV from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        For Each rw2 As DataRow In dt2.Rows
            DebProjet = rw2(0).ToString
            FinProjet = rw2(1).ToString
        Next

        Dim FinAtteinte As Boolean = False
        Dim LeMois() As String = {"", "Jan", "Fev", "Mar", "Avr", "Mai", "Jui", "Jut", "Aou", "Sep", "Oct", "Nov", "Dec"}
        Dim moisDeb As Decimal = CInt(Mid(DebProjet, 4, 2))
        Dim anDeb As Decimal = CInt(Mid(DebProjet, 7))
        Dim nbrElt As Decimal = 0

        Dim DecTotal As Decimal = 0
        Dim moisSuiv As Decimal = moisDeb + 1
        Dim anSuiv As Decimal = anDeb

        While FinAtteinte = False

            Dim Decaismt As Decimal = 0
            query = "select Montant_act from T_COMP_ACTIVITE where Code_Projet='" & ProjetEnCours & "' and Date_act >= '" & anDeb & "-" & moisDeb & "-01' and Date_act < '" & anSuiv & "-" & moisSuiv & "-01'"
            Dim dt3 As DataTable = ExcecuteSelectQuery(query)
            For Each rw3 As DataRow In dt3.Rows
                Decaismt = Decaismt + CDec(rw3(0))
            Next

            DecTotal = DecTotal + Math.Round(Decaismt / tauxConv, NbVirg)
            series1.Points.AddXY(LeMois(moisDeb) & anDeb, DecTotal)
            series2.Points.Add(0)
            nbrElt = nbrElt + 1
            moisDeb = moisDeb + 1
            moisSuiv = moisDeb + 1
            If (moisSuiv > 12) Then
                moisSuiv = 1
                anSuiv = anDeb + 1
            End If
            If (moisDeb > 12) Then
                moisDeb = 1
                anDeb = anDeb + 1
            End If
            If (DateTime.Compare(CDate("01/" & moisDeb.ToString & "/" & anDeb), CDate(FinProjet)) > 0 Or DateTime.Compare(CDate("01/" & moisDeb.ToString & "/" & anDeb), CDate("31/12/" & Mid(Now.ToShortDateString, 7))) > 0) Then
                FinAtteinte = True
            End If
        End While

        series1.Color = Color.Blue
        series1.BorderWidth = 4
        series2.Color = Color.Green
        series2.BorderWidth = 1

        ChartGraph4.ChartAreas(0).AxisX.Minimum = 1
        ChartGraph4.ChartAreas(0).AxisX.Maximum = nbrElt + 1
        ChartGraph4.ChartAreas(0).AxisX.Interval = 1
        ChartGraph4.ChartAreas(0).AxisY.LabelStyle.Format = "### ### ### ### ###"
        ChartGraph4.ChartAreas(0).AxisY.Minimum = -Math.Round(50000000 / tauxConv, 0)
        ChartGraph4.ChartAreas(0).AxisY.Maximum = Math.Round(DecTotal + Math.Round(100000000 / tauxConv, 0), 0)
        ChartGraph4.ChartAreas(0).AxisY.Interval = Math.Round(100000000 / tauxConv, 0)
        ChartGraph4.ChartAreas(0).AxisY.LabelStyle.IsEndLabelVisible = False
        ChartGraph4.ChartAreas(0).AxisX.IsMarginVisible = False

        series1.ChartArea = chartArea1.Name
        series2.ChartArea = chartArea1.Name
        ChartGraph4.Series.Add(series1)
        ChartGraph4.Series.Add(series2)

        ChartGraph4.BackImage = line & "\ClearpNew6.png"
        ChartGraph4.BackImageWrapMode = ChartImageWrapMode.Unscaled
        ChartGraph4.BackImageAlignment = ChartImageAlignmentStyle.BottomLeft

        ChartGraph4.Titles("Titre1").Text = "GRAPHIQUE DE L'EVOLUTION DES DEPENSES DU " & ProjetEnCours & " DEPUIS LE DEBUT DU PROJET JUSQU'AU " & Now.ToShortDateString & vbNewLine & " "
        ChartGraph4.Titles("Titre1").Position.Auto = True
        ChartGraph4.Titles("Titre1").DockedToChartArea = chartArea1.Name
        ChartGraph4.Titles("Titre1").Docking = Docking.Bottom
        ChartGraph4.Titles("Titre1").IsDockedInsideChartArea = False
        ChartGraph4.Titles("Titre1").Font = New Font("Arial", 10, FontStyle.Bold)

        ChartGraph4.Series(0).MarkerStyle = MarkerStyle.Circle
        ChartGraph4.Series(0).MarkerColor = Color.Red

        ChartGraph4.Series(0).IsVisibleInLegend = False
        ChartGraph4.Series(1).IsVisibleInLegend = False

        ChartGraph4.Series(0).ChartType = SeriesChartType.Spline
        ChartGraph4.Series(1).ChartType = SeriesChartType.Spline

    End Sub

    Private Sub GraphDecaissement(ByVal Devise As String)

        Dim separDecim As String = ","
        If (Devise = "US$") Then separDecim = "."
        Dim MontTotal As String = ""

        ChartGraph3.ChartAreas.Clear()
        ChartGraph3.Series.Clear()
        ChartGraph3.Legends.Clear()

        Dim chartArea1 As New ChartArea()
        ChartGraph3.ChartAreas.Add(chartArea1)

        'Instanciation series sans Name
        Dim series1 As New Series("Decaisse")
        Dim series2 As New Series("Zero")
        Dim NbVirg As Integer = 0
        If (Devise <> "FCFA") Then NbVirg = 2
        'Conversion du montant
        Dim tauxConv As Decimal = 1
        query = "select TauxDevise from T_Devise where AbregeDevise='" & Devise & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            tauxConv = CDec(rw(0))
        Next

        Dim AnEnCours As String = Mid(Now.ToShortDateString, 7)
        Dim DernAnnee As String = (CInt(AnEnCours - 1)).ToString
        Dim DernDecais As Decimal = 0
        query = "select Montant_act from T_COMP_ACTIVITE where Code_Projet='" & ProjetEnCours & "' and Date_act between '" & DernAnnee & "-12-01' and '" & DernAnnee & "-12-31'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows
            DernDecais = DernDecais + CDec(rw1(0))
        Next

        DernDecais = Math.Round(DernDecais / tauxConv, NbVirg)
        series1.Points.AddXY("Dec" & DernAnnee, DernDecais)
        series2.Points.Add(0)

        Dim LeMois() As String = {"", "Jan", "Fev", "Mar", "Avr", "Mai", "Jui", "Jut", "Aou", "Sep", "Oct", "Nov", "Dec"}
        Dim MaxDec As Decimal = 0

        Dim MoisSuiv As Decimal = 0
        Dim anSuiv As Decimal = AnEnCours

        For Mois As Decimal = 1 To 12
            MoisSuiv = Mois + 1
            If (MoisSuiv > 12) Then
                MoisSuiv = MoisSuiv - 12
                anSuiv = AnEnCours + 1
            End If

            Dim Decaismt As Decimal = 0
            query = "select Montant_act from T_COMP_ACTIVITE where Code_Projet='" & ProjetEnCours & "' and Date_act >= '" & AnEnCours & "-" & Mois & "-01' and Date_act < '" & anSuiv & "-" & MoisSuiv & "-01'"
            Dim dt2 As DataTable = ExcecuteSelectQuery(query)
            For Each rw2 As DataRow In dt2.Rows
                If (CDec(rw2(0)) > MaxDec) Then MaxDec = CDec(rw2(0))
                Decaismt = Decaismt + CDec(rw2(0))
            Next

            Decaismt = Math.Round(Decaismt / tauxConv, NbVirg)
            series1.Points.AddXY(LeMois(Mois), Decaismt)
            series2.Points.Add(0)
        Next

        series1.Color = Color.Blue
        series1.BorderWidth = 4
        series2.Color = Color.Green
        series2.BorderWidth = 1

        ChartGraph3.ChartAreas(0).AxisX.Minimum = 1
        ChartGraph3.ChartAreas(0).AxisX.Maximum = 13
        ChartGraph3.ChartAreas(0).AxisX.Interval = 1
        ChartGraph3.ChartAreas(0).AxisY.Minimum = -100000
        ChartGraph3.ChartAreas(0).AxisY.LabelStyle.Format = "### ### ### ### ###"
        ChartGraph3.ChartAreas(0).AxisY.Minimum = -Math.Round(50000000 / tauxConv, 0)
        ChartGraph3.ChartAreas(0).AxisY.Maximum = Math.Round(MaxDec + Math.Round(100000000 / tauxConv, 0), 0)
        ChartGraph3.ChartAreas(0).AxisY.Interval = Math.Round(100000000 / tauxConv, 0)
        ChartGraph3.ChartAreas(0).AxisY.LabelStyle.IsEndLabelVisible = False
        ChartGraph3.ChartAreas(0).AxisX.IsMarginVisible = False

        ' Add series to the chart
        series1.ChartArea = chartArea1.Name
        ChartGraph3.Series.Add(series1)
        series2.ChartArea = chartArea1.Name
        ChartGraph3.Series.Add(series2)

        ChartGraph3.BackImage = line & "\ClearpNew6.png"
        ChartGraph3.BackImageWrapMode = ChartImageWrapMode.Unscaled
        ChartGraph3.BackImageAlignment = ChartImageAlignmentStyle.BottomLeft

        ChartGraph3.Titles("Titre1").Text = "GRAPHIQUE DU SUIVI DES DECAISSEMENTS MENSUELS DU " & ProjetEnCours & " POUR L'ANNEE " & AnEnCours & vbNewLine & " "
        ChartGraph3.Titles("Titre1").Position.Auto = True
        ChartGraph3.Titles("Titre1").DockedToChartArea = chartArea1.Name
        ChartGraph3.Titles("Titre1").Docking = Docking.Bottom
        ChartGraph3.Titles("Titre1").IsDockedInsideChartArea = False
        ChartGraph3.Titles("Titre1").Font = New Font("Arial", 10, FontStyle.Bold)

        ChartGraph3.Series(0).IsVisibleInLegend = False
        ChartGraph3.Series(1).IsVisibleInLegend = False

        ChartGraph3.Series(0).MarkerStyle = MarkerStyle.Circle
        ChartGraph3.Series(0).MarkerColor = Color.Red

        ChartGraph3.Series(0).ChartType = SeriesChartType.Spline
        ChartGraph3.Series(1).ChartType = SeriesChartType.Spline

    End Sub

    Private Sub GraphPrioriPosteriori(ByVal Devise As String)
        Dim separDecim As String = ","
        If (Devise = "US$") Then separDecim = "."
        Dim MontTotal As String = ""

        ChartGraph2.ChartAreas.Clear()
        ChartGraph2.Series.Clear()
        ChartGraph2.Legends.Clear()

        Dim chartArea1 As New ChartArea()
        ChartGraph2.ChartAreas.Add(chartArea1)

        'Instanciation series sans Name
        Dim series1 As New Series("Decaisse")
        ' Add data points to the first series
        Dim Baille1 As String = ""

        query = "select MontPriori,MontPosteriori,Bailleur,MontTTMarche from T_TampStatisticRSM"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Baille1 = MettreApost(rw(2).ToString).ToUpper
            Dim montPrio As Decimal = CDec(rw(0).ToString.Replace(" ", "").Replace(".", ","))
            Dim montPost As Decimal = CDec(rw(1).ToString.Replace(" ", "").Replace(".", ","))
            Dim montMarche As Decimal = CDec(rw(3).ToString.Replace(" ", "").Replace(".", ","))
            Dim prctAp As Decimal = 0
            If (montMarche <> 0) Then prctAp = Math.Round((montPrio * 100) / montMarche, 3)
            series1.Points.AddXY(rw(0).ToString & " (" & Devise & ")" & vbNewLine & "(" & prctAp.ToString & "%)", montPrio)
            series1.Points.AddXY(rw(1).ToString & " (" & Devise & ")" & vbNewLine & "(" & (100 - prctAp).ToString & "%)", montPost)
            MontTotal = rw(3).ToString
        Next

        series1.Points(0).Color = Color.LightBlue
        series1.Points(1).Color = Color.LightSalmon
        series1.Points(0).Font = New Font("Arial", 8, FontStyle.Bold)
        series1.Points(1).Font = New Font("Arial", 8, FontStyle.Bold)

        ' Add series to the chart
        series1.ChartArea = chartArea1.Name
        ChartGraph2.Series.Add(series1)

        ChartGraph2.BackImage = line & "\ClearpNew6.png"
        ChartGraph2.BackImageWrapMode = ChartImageWrapMode.Unscaled
        ChartGraph2.BackImageAlignment = ChartImageAlignmentStyle.BottomLeft

        ChartGraph2.Titles("Titre1").Text = "GRAPHIQUE COMPARATIF DES MARCHES A PRIORI ET A POSTERIORI " & Baille1 & " POUR UN MONTANT TOTAL DE" & vbNewLine & MontTotal & " " & Devise & "   DE MARCHES PREVUS" & "(à la date du " & Now.ToShortDateString & ")" & vbNewLine & " "
        ChartGraph2.Titles("Titre1").Position.Auto = True
        ChartGraph2.Titles("Titre1").DockedToChartArea = chartArea1.Name
        ChartGraph2.Titles("Titre1").Docking = Docking.Bottom
        ChartGraph2.Titles("Titre1").IsDockedInsideChartArea = False
        ChartGraph2.Titles("Titre1").Font = New Font("Arial", 10, FontStyle.Bold)

        Dim legend1 As New Legend
        legend1.Name = "Leg1"
        legend1.Title = "Légende"
        ChartGraph2.Legends.Add(legend1)
        ChartGraph2.Series(0).IsVisibleInLegend = False
        ChartGraph2.Legends("Leg1").CustomItems.Clear()
        ChartGraph2.Legends("Leg1").CustomItems.Add(New LegendItem("Montant marchés à priori", Color.LightBlue, ""))
        ChartGraph2.Legends("Leg1").CustomItems.Add(New LegendItem("Montant marchés à postériori", Color.LightSalmon, ""))
        ChartGraph2.Legends("Leg1").IsDockedInsideChartArea = True

        '*************************************************************************************
        ChartGraph2.ChartAreas(0).InnerPlotPosition.Auto = False
        ChartGraph2.ChartAreas(0).InnerPlotPosition.Height = 100
        ChartGraph2.ChartAreas(0).InnerPlotPosition.Width = 95

        ChartGraph2.ChartAreas(0).Position.Height = 85
        ChartGraph2.ChartAreas(0).Position.Width = 100

        ChartGraph2.ChartAreas("ChartArea1").BackColor = Color.Transparent
        ChartGraph2.ChartAreas("ChartArea1").BackSecondaryColor = Color.Transparent
        '******************************************************************************************
        ChartGraph2.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
        ChartGraph2.Series(0).ChartType = SeriesChartType.Pie
        ChartGraph2.ChartAreas("ChartArea1").Area3DStyle.LightStyle = LightStyle.Realistic
        ChartGraph2.ChartAreas(0).Area3DStyle.PointDepth = 40


    End Sub

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

    Private Sub GraphMarcheAttribue(ByVal Devise As String)
        Dim separDecim As String = ","
        If (Devise = "US$") Then separDecim = "."
        Dim MontTotal As String = ""

        ChartGraph8.ChartAreas.Clear()
        ChartGraph8.Series.Clear()
        ChartGraph8.Legends.Clear()

        Dim chartArea1 As New ChartArea()
        ChartGraph8.ChartAreas.Add(chartArea1)

        'Instanciation series sans Name
        Dim series1 As New Series("Attribue")
        ' Add data points to the first series
        Dim Baille1 As String = ""

        query = "select MontTTMarche,MontAttrib,Bailleur,MontApprouve from T_TampStatisticRSM"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Baille1 = MettreApost(rw(2).ToString).ToUpper
            Dim montMarche As Decimal = CDec(rw(0).ToString.Replace(" ", "").Replace(".", ","))
            Dim montAttribue As Decimal = CDec(rw(1).ToString.Replace(" ", "").Replace(".", ",")) + CDec(rw(3).ToString.Replace(" ", "").Replace(".", ","))
            Dim prctAtt As Decimal = 0
            If (montMarche <> 0) Then prctAtt = Math.Round((montAttribue * 100) / montMarche, 3)
            series1.Points.AddXY(AfficherMonnaie(montMarche - montAttribue).Replace(",", separDecim) & " (" & Devise & ")" & vbNewLine & "(" & (100 - prctAtt).ToString & "%)", montMarche - montAttribue)
            series1.Points.AddXY(AfficherMonnaie(montAttribue).Replace(",", separDecim) & " (" & Devise & ")" & vbNewLine & "(" & prctAtt.ToString & "%)", montAttribue)
            MontTotal = rw(0).ToString
        Next

        series1.Points(0).Color = Color.LightBlue
        series1.Points(1).Color = Color.LightSalmon
        series1.Points(0).Font = New Font("Arial", 8, FontStyle.Bold)
        series1.Points(1).Font = New Font("Arial", 8, FontStyle.Bold)

        ' Add series to the chart
        series1.ChartArea = chartArea1.Name
        ChartGraph8.Series.Add(series1)

        ChartGraph8.BackImage = line & "\ClearpNew6.png"
        ChartGraph8.BackImageWrapMode = ChartImageWrapMode.Unscaled
        ChartGraph8.BackImageAlignment = ChartImageAlignmentStyle.BottomLeft

        ChartGraph8.Titles("Titre1").Text = "GRAPHIQUE DES MARCHES ATTRIBUES (APPROUVES OU NON) " & Baille1 & " POUR UN MONTANT TOTAL DE" & vbNewLine & MontTotal & " " & Devise & "   DE MARCHES PREVUS (à la date du " & Now.ToShortDateString & ")" & vbNewLine & " "
        ChartGraph8.Titles("Titre1").Position.Auto = True
        ChartGraph8.Titles("Titre1").DockedToChartArea = chartArea1.Name
        ChartGraph8.Titles("Titre1").Docking = Docking.Bottom
        ChartGraph8.Titles("Titre1").IsDockedInsideChartArea = False
        ChartGraph8.Titles("Titre1").Font = New Font("Arial", 10, FontStyle.Bold)

        Dim legend1 As New Legend
        legend1.Name = "Leg1"
        legend1.Title = "Légende"
        ChartGraph8.Legends.Add(legend1)
        ChartGraph8.Series(0).IsVisibleInLegend = False
        ChartGraph8.Legends("Leg1").CustomItems.Clear()
        ChartGraph8.Legends("Leg1").CustomItems.Add(New LegendItem("Marchés non encore attribués", Color.LightBlue, ""))
        ChartGraph8.Legends("Leg1").CustomItems.Add(New LegendItem("Marchés attribués à ce jour", Color.LightSalmon, ""))

        '*************************************************************************************
        ChartGraph8.ChartAreas(0).InnerPlotPosition.Auto = False
        ChartGraph8.ChartAreas(0).InnerPlotPosition.Height = 100
        ChartGraph8.ChartAreas(0).InnerPlotPosition.Width = 95

        ChartGraph8.ChartAreas(0).Position.Height = 85
        ChartGraph8.ChartAreas(0).Position.Width = 100

        ChartGraph8.ChartAreas("ChartArea1").BackColor = Color.Transparent
        ChartGraph8.ChartAreas("ChartArea1").BackSecondaryColor = Color.Transparent
        '******************************************************************************************
        ChartGraph8.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
        ChartGraph8.Series(0).ChartType = SeriesChartType.Pie
        ChartGraph8.ChartAreas("ChartArea1").Area3DStyle.LightStyle = LightStyle.Realistic
        ChartGraph8.ChartAreas(0).Area3DStyle.PointDepth = 40


    End Sub

    Private Sub GraphMarcheApprouve(ByVal Devise As String)
        Dim separDecim As String = ","
        If (Devise = "US$") Then separDecim = "."
        Dim MontTotal As String = ""

        ChartGraph0.ChartAreas.Clear()
        ChartGraph0.Series.Clear()
        ChartGraph0.Legends.Clear()

        Dim chartArea1 As New ChartArea()
        ChartGraph0.ChartAreas.Add(chartArea1)

        'Instanciation series sans Name
        Dim series1 As New Series("Approuve")
        ' Add data points to the first series
        Dim Baille1 As String = ""

        query = "select MontTTMarche,MontApprouve,Bailleur from T_TampStatisticRSM"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            Baille1 = MettreApost(rw(2).ToString).ToUpper
            Dim montMarche As Decimal = CDec(rw(0).ToString.Replace(" ", "").Replace(".", ","))
            Dim montApprouve As Decimal = CDec(rw(1).ToString.Replace(" ", "").Replace(".", ","))
            Dim prctAp As Decimal = 0
            If (montMarche <> 0) Then prctAp = Math.Round((montApprouve * 100) / montMarche, 3)
            series1.Points.AddXY(AfficherMonnaie(montMarche - montApprouve).Replace(",", separDecim) & " (" & Devise & ")" & vbNewLine & "(" & (100 - prctAp).ToString & "%)", montMarche - montApprouve)
            series1.Points.AddXY(rw(1).ToString & " (" & Devise & ")" & vbNewLine & "(" & prctAp.ToString & "%)", montApprouve)
            MontTotal = rw(0).ToString
        Next

        series1.Points(0).Color = Color.LightBlue
        series1.Points(1).Color = Color.LightSalmon
        series1.Points(0).Font = New Font("Arial", 8, FontStyle.Bold)
        series1.Points(1).Font = New Font("Arial", 8, FontStyle.Bold)

        ' Add series to the chart
        series1.ChartArea = chartArea1.Name
        ChartGraph0.Series.Add(series1)

        ChartGraph0.BackImage = line & "\ClearpNew6.png"
        ChartGraph0.BackImageWrapMode = ChartImageWrapMode.Unscaled
        ChartGraph0.BackImageAlignment = ChartImageAlignmentStyle.BottomLeft

        ChartGraph0.Titles("Titre1").Text = "GRAPHIQUE DES MONTANTS DES MARCHES APPROUVES " & Baille1 & " POUR UN MONTANT TOTAL DE" & vbNewLine & MontTotal & " " & Devise & "   DE MARCHES PREVUS (à la date du " & Now.ToShortDateString & ")" & vbNewLine & " "
        ChartGraph0.Titles("Titre1").Position.Auto = True
        ChartGraph0.Titles("Titre1").DockedToChartArea = chartArea1.Name
        ChartGraph0.Titles("Titre1").Docking = Docking.Bottom
        ChartGraph0.Titles("Titre1").IsDockedInsideChartArea = False
        ChartGraph0.Titles("Titre1").Font = New Font("Arial", 10, FontStyle.Bold)

        Dim legend1 As New Legend
        legend1.Name = "Leg1"
        legend1.Title = "Légende"
        ChartGraph0.Legends.Add(legend1)
        ChartGraph0.Series(0).IsVisibleInLegend = False
        ChartGraph0.Legends("Leg1").CustomItems.Clear()
        ChartGraph0.Legends("Leg1").CustomItems.Add(New LegendItem("Marchés non encore approuvés", Color.LightBlue, ""))
        ChartGraph0.Legends("Leg1").CustomItems.Add(New LegendItem("Marchés approuvés à ce jour", Color.LightSalmon, ""))

        '*************************************************************************************
        ChartGraph0.ChartAreas(0).InnerPlotPosition.Auto = False
        ChartGraph0.ChartAreas(0).InnerPlotPosition.Height = 100
        ChartGraph0.ChartAreas(0).InnerPlotPosition.Width = 95

        ChartGraph0.ChartAreas(0).Position.Height = 85
        ChartGraph0.ChartAreas(0).Position.Width = 100

        ChartGraph0.ChartAreas("ChartArea1").BackColor = Color.Transparent
        ChartGraph0.ChartAreas("ChartArea1").BackSecondaryColor = Color.Transparent
        '******************************************************************************************
        ChartGraph0.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
        ChartGraph0.Series(0).ChartType = SeriesChartType.Pie
        ChartGraph0.ChartAreas("ChartArea1").Area3DStyle.LightStyle = LightStyle.Realistic
        ChartGraph0.ChartAreas(0).Area3DStyle.PointDepth = 40


    End Sub

    Private Sub InfosDAO(ByVal pourBaill As String)

        query = "DELETE from T_TampDaoRsm"
        ExecuteNonQuery(query)

        query = "DELETE from T_TampDpRsm"
        ExecuteNonQuery(query)

        Dim Baill01 As String = "*"
        If (pourBaill <> "Tous") Then Baill01 = pourBaill

        ' DAO
        query = "select D.NumeroDAO,D.TypeMarche,D.MethodePDM,D.DateEdition,D.NbreLotDAO,D.DateLimiteRemise,D.IntituleDAO from T_DAO as D,T_Marche as M where D.NumeroDAO=M.NumeroDAO and D.CodeProjet='" & ProjetEnCours & "' and M.InitialeBailleur like '" & Baill01 & "' order by D.NumeroDAO"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows

            Dim NbRetire As Decimal = 0
            Dim NbDepose As Decimal = 0
            query = "select CodeFournis,DateDepotDAO from T_Fournisseur where NumeroDAO='" & rw(0) & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                NbRetire = NbRetire + 1
                If (rw1(1).ToString <> "") Then NbDepose = NbDepose + 1
            Next

            Dim DelaiRest As String = "-"
            If (rw(5).ToString <> "") Then
                Dim PartLimite() As String = rw(5).ToString.Split(" "c)
                If (DateTime.Compare(CDate(PartLimite(0)), Now.ToShortDateString) > 0) Then
                    DelaiRest = NbreJourDansPeriode(CDate(PartLimite(0)), Now.ToShortDateString, False, False, False, False, False, False, False) & " Jours"
                ElseIf (DateTime.Compare(CDate(PartLimite(0)), Now.ToShortDateString) = 0) Then
                    DelaiRest = NbreJourDansPeriode(CDate(PartLimite(0)), Now.ToShortDateString, False, False, False, False, False, False, False) & " Jour"
                Else
                    DelaiRest = "Achevé"
                End If
            End If

            Dim DatSet = New DataSet
            query = "select * from T_TampDaoRsm"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_TampDaoRsm")
            Dim DatTable = DatSet.Tables("T_TampDaoRsm")
            Dim DatRow = DatSet.Tables("T_TampDaoRsm").NewRow()

            DatRow("NumeroDAO") = rw(0)
            DatRow("LibelleDAO") = EnleverApost(rw(6))
            DatRow("TypeMarche") = rw(1)
            DatRow("Methode") = rw(2)
            DatRow("Edition") = rw(3)
            DatRow("NbLot") = rw(4)
            DatRow("NbRetire") = NbRetire
            DatRow("NbOffres") = NbDepose
            DatRow("DateLimite") = rw(5)
            DatRow("DelaiRestant") = EnleverApost(DelaiRest)

            DatSet.Tables("T_TampDaoRsm").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_TampDaoRsm")
            DatSet.Clear()
            BDQUIT(sqlconn)
        Next


        ' DP
        query = "select D.NumeroDp,D.TypeRemune,D.MethodeSelection,D.DateEdition,D.ListeRestreinte,D.DateLimitePropo,D.LibelleMiss from T_DP as D,T_Marche as M where D.NumeroDP=M.NumeroDAO and D.CodeProjet='" & ProjetEnCours & "' and M.InitialeBailleur like '" & Baill01 & "' order by D.NumeroDp"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        For Each rw2 As DataRow In dt2.Rows

            Dim NbDepose As Decimal = 0
            query = "select Count(*) from T_Consultant where NumeroDp='" & rw2(0) & "' and DateDepot<>''"
            NbDepose = ExecuteScallar(query)


            Dim DelaiRest As String = "-"
            If (rw2(5).ToString <> "") Then
                Dim PartLimite() As String = rw2(5).ToString.Split(" "c)
                If (DateTime.Compare(CDate(PartLimite(0)), Now.ToShortDateString) > 0) Then
                    DelaiRest = NbreJourDansPeriode(CDate(PartLimite(0)), Now.ToShortDateString, False, False, False, False, False, False, False) & " Jours"
                ElseIf (DateTime.Compare(CDate(PartLimite(0)), Now.ToShortDateString) = 0) Then
                    DelaiRest = NbreJourDansPeriode(CDate(PartLimite(0)), Now.ToShortDateString, False, False, False, False, False, False, False) & " Jour"
                Else
                    DelaiRest = "Achevé"
                End If
            End If

            Dim DatSet = New DataSet
            query = "select * from T_TampDpRsm"
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            DatAdapt.Fill(DatSet, "T_TampDpRsm")
            Dim DatTable = DatSet.Tables("T_TampDpRsm")
            Dim DatRow = DatSet.Tables("T_TampDpRsm").NewRow()

            DatRow("NumeroDp") = rw2(0)
            DatRow("LibelleDp") = EnleverApost(rw2(6))
            DatRow("TypeRemune") = rw2(1)
            DatRow("MethodeSelect") = rw2(2)
            DatRow("Edition") = rw2(3)
            DatRow("NbListRest") = rw2(4)
            DatRow("NbOffres") = NbDepose
            DatRow("DateLimite") = rw2(5)
            DatRow("DelaiRestant") = EnleverApost(DelaiRest)

            DatSet.Tables("T_TampDpRsm").Rows.Add(DatRow)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Update(DatSet, "T_TampDpRsm")
            DatSet.Clear()
            BDQUIT(sqlconn)
        Next


    End Sub

    Private Sub InfosStatistic(ByVal Fonds As String, ByVal Devise As String)

        Dim DateDebProj As String = ""
        Dim DateFinProj As String = ""
        Dim montConv As Decimal = 0
        Dim montTTMarche As Decimal = 0
        Dim prctTTMarche As Decimal = 0
        Dim nbPrevu As Decimal = 0
        Dim nbPriori As Decimal = 0
        Dim montPriori As Decimal = 0
        Dim nbPoster As Decimal = 0
        Dim montPoster As Decimal = 0
        Dim montApprouve As Decimal = 0
        Dim montDecaisse As Decimal = 0
        Dim prctApprouve As Decimal = 0
        Dim prctDecaisConv As Decimal = 0
        Dim prctDecaisAppr As Decimal = 0
        Dim montApprNonDecais As Decimal = 0
        Dim prctApprNonDecais As Decimal = 0
        Dim montTTNonDecais As Decimal = 0
        Dim prctTTNonDecais As Decimal = 0
        Dim nbApprouve As Decimal = 0
        Dim nbRestant As Decimal = 0
        Dim nbMarcheMois As Decimal = 0
        Dim montMarcheMois As Decimal = 0
        Dim montDecaisMois As Decimal = 0
        Dim montRestant As Decimal = 0
        Dim montRestLettre As String = ""
        Dim nbAttrib As Decimal = 0
        Dim montAttrib As Decimal = 0
        Dim prctAttrib As Decimal = 0

        Dim Baille1 As String = "%"
        If (Fonds <> "Tous") Then Baille1 = Fonds

        query = "DELETE from T_TampStatisticRSM"
        ExecuteNonQuery(query)

        'Les dates du projet *********************
        query = "select DateDebutProjetMV,DateFinProjetMV from T_Projet where CodeProjet='" & ProjetEnCours & "'"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            DateDebProj = rw(0).ToString
            DateFinProj = rw(1).ToString
        Next

        'Montant de la convention du bailleur selectionné
        query = "select C.MontantConvention from T_Convention as C,T_Bailleur as B where B.InitialeBailleur like '" & Baille1 & "' and B.CodeBailleur=C.CodeBailleur and B.CodeProjet='" & ProjetEnCours & "'"
        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
        For Each rw1 As DataRow In dt1.Rows
            montConv = montConv + CDec(rw1(0))
        Next

        'Montant total des marchés, Nbre marches de chaque revue et les montants
        query = "select MontantEstimatif,RevuePrioPost from T_Marche where InitialeBailleur like '" & Baille1 & "' and CodeProjet='" & ProjetEnCours & "'"
        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
        For Each rw2 As DataRow In dt2.Rows
            nbPrevu = nbPrevu + 1
            montTTMarche = montTTMarche + CDec(rw2(0))
            If (rw2(1).ToString.Replace("*", "") = "Priori") Then
                nbPriori = nbPriori + 1
                montPriori = montPriori + CDec(rw2(0))
            ElseIf (rw2(1).ToString.Replace("*", "") = "Postériori") Then
                nbPoster = nbPoster + 1
                montPoster = montPoster + CDec(rw2(0))
            End If
        Next

        If (montConv <> 0) Then
            prctTTMarche = Math.Round((montTTMarche * 100) / montConv, 3)
        End If

        'Les marches approuvés, attribués, décaissé et du mois
        query = "select S.NumeroMarche, S.DateMarche, S.MontantHT, M.RefMarche, M.TypeMarche from T_Marche as M, T_MarcheSigne as S where M.InitialeBailleur like '" & Baille1 & "' and M.RefMarche=S.RefMarche and M.CodeProjet='" & ProjetEnCours & "'"
        Dim dt3 As DataTable = ExcecuteSelectQuery(query)
        For Each rw3 As DataRow In dt3.Rows

            Dim marcheInd As Decimal = CInt(rw3(3))
            Dim marcheKind As String = rw3(4).ToString
            nbAttrib = nbAttrib + 1
            montAttrib = montAttrib + CDec(rw3(2))

            query = "select * from T_PlanMarche as P, T_EtapeMarche as E where P.RefEtape=E.RefEtape and P.RefMarche='" & marcheInd & "' and P.DebutEffectif<>'' and P.FinEffective<>'' and E.ApproMarche='OUI'"
            Dim dt4 As DataTable = ExcecuteSelectQuery(query)
            If dt4.Rows.Count = 1 Then
                nbApprouve = nbApprouve + 1
                montApprouve = montApprouve + CDec(rw3(2))
                If (Mid(CDate(rw3(1)).ToShortDateString, 4) = Mid(Now.ToShortDateString, 4)) Then
                    nbMarcheMois = nbMarcheMois + 1
                    montMarcheMois = montMarcheMois + CDec(rw3(2))
                End If
            End If


            query = "select p.Date_act, p.Montant_act from T_COMP_ACTIVITE a, t_comp_activite_payer p where a.LibelleCourt=p.LibelleCourt and a.NumeroMarche='" & rw3(0) & "' and a.Code_Projet='" & ProjetEnCours & "'"
            Dim dt5 As DataTable = ExcecuteSelectQuery(query)
            For Each rw5 As DataRow In dt5.Rows
                montDecaisse = montDecaisse + CDec(rw5(1))
                If (Mid(CDate(rw5(0)).ToShortDateString, 4) = Mid(Now.ToShortDateString, 4)) Then
                    montDecaisMois = montDecaisMois + CDec(rw5(1))
                End If
            Next

        Next


        nbAttrib = nbAttrib - nbApprouve
        montAttrib = montAttrib - montApprouve

        montApprNonDecais = montApprouve - montDecaisse
        montTTNonDecais = montTTMarche - montDecaisse
        If (montApprouve <> 0) Then prctDecaisAppr = Math.Round((montDecaisse * 100) / montApprouve, 3)
        If (montConv <> 0) Then prctDecaisConv = Math.Round((montDecaisse * 100) / montConv, 3)
        If (montTTMarche <> 0) Then
            prctApprouve = Math.Round((montApprouve * 100) / montTTMarche, 3)
            prctAttrib = Math.Round((montAttrib * 100) / montTTMarche, 3)
        End If
        If (montApprouve <> 0) Then prctApprNonDecais = Math.Round((montApprNonDecais * 100) / montApprouve, 3)
        If (montTTMarche <> 0) Then prctTTNonDecais = Math.Round((montTTNonDecais * 100) / montTTMarche, 3)
        nbRestant = nbPrevu - nbApprouve
        montRestant = montTTMarche - montApprouve

        Dim NbVirg As Integer = 0
        If (Devise <> "FCFA") Then NbVirg = 2
        Dim separDecim As String = ","
        If (Devise = "US$") Then separDecim = "."
        'Conversion du montant
        Dim tauxConv As Decimal = 1
        Dim NomDev As String = ""

        query = "select TauxDevise,LibelleDevise from T_Devise where AbregeDevise='" & Devise & "'"
        Dim dt6 As DataTable = ExcecuteSelectQuery(query)
        For Each rw6 As DataRow In dt6.Rows
            tauxConv = CDec(rw6(0))
            NomDev = rw6(1).ToString
        Next

        montConv = Math.Round(montConv / tauxConv, NbVirg)
        FondBaillr = montConv    'pour le graph7
        montTTMarche = Math.Round(montTTMarche / tauxConv, NbVirg)
        montDecaisse = Math.Round(montDecaisse / tauxConv, NbVirg)
        FondDecais = montDecaisse    'pour le graph7
        montApprouve = Math.Round(montApprouve / tauxConv, NbVirg)
        montAttrib = Math.Round(montAttrib / tauxConv, NbVirg)
        montApprNonDecais = Math.Round(montApprNonDecais / tauxConv, NbVirg)
        montTTNonDecais = Math.Round(montTTNonDecais / tauxConv, NbVirg)
        montPriori = Math.Round(montPriori / tauxConv, NbVirg)
        montPoster = Math.Round(montPoster / tauxConv, NbVirg)
        montMarcheMois = Math.Round(montMarcheMois / tauxConv, NbVirg)
        montDecaisMois = Math.Round(montDecaisMois / tauxConv, NbVirg)
        montRestant = Math.Round(montRestant / tauxConv, NbVirg)

        If (Truncate(montDecaisMois) = montDecaisMois) Then
            montRestLettre = MontantLettre(montDecaisMois.ToString) & " " & NomDev & "s"
        Else
            Dim pEntiere As Decimal = Truncate(montDecaisMois)
            Dim pDecimale As Decimal = Truncate((montDecaisMois - pEntiere) * 100)
            montRestLettre = MontantLettre(pEntiere.ToString) & " " & NomDev & "s et " & MontantLettre(pDecimale.ToString) & " Centimes"
        End If

        If (Fonds = "Tous") Then
            Baille1 = "du projet"
        Else
            Baille1 = "de l'" & Fonds
        End If

        Dim DatSet = New DataSet
        query = "select * from T_TampStatisticRSM"
        Dim sqlconn As New MySqlConnection
        BDOPEN(sqlconn)
        Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
        Dim DatAdapt = New MySqlDataAdapter(Cmd)
        DatAdapt.Fill(DatSet, "T_TampStatisticRSM")
        Dim DatTable = DatSet.Tables("T_TampStatisticRSM")
        Dim DatRow = DatSet.Tables("T_TampStatisticRSM").NewRow()

        DatRow("Bailleur") = EnleverApost(Baille1)
        DatRow("DebutProjet") = DateDebProj
        DatRow("FinProjet") = DateFinProj
        DatRow("MontantConv") = AfficherMonnaie(montConv.ToString).Replace(",", separDecim)
        DatRow("MontTTMarche") = AfficherMonnaie(montTTMarche.ToString).Replace(",", separDecim)
        DatRow("PrctTTMarche") = prctTTMarche.ToString.Replace(",", separDecim)
        DatRow("MontApprouve") = AfficherMonnaie(montApprouve.ToString).Replace(",", separDecim)
        DatRow("PrctApprouve") = prctApprouve.ToString.Replace(",", separDecim)
        DatRow("MontDecaisse") = AfficherMonnaie(montDecaisse.ToString).Replace(",", separDecim)
        DatRow("PrctDecaisseAppr") = prctDecaisAppr.ToString.Replace(",", separDecim)
        DatRow("PrctDecaisseConv") = prctDecaisConv.ToString.Replace(",", separDecim)
        DatRow("MontAppNonDecais") = AfficherMonnaie(montApprNonDecais.ToString).Replace(",", separDecim)
        DatRow("PrctAppNonDecais") = prctApprNonDecais.ToString.Replace(",", separDecim)
        DatRow("MontTTNonDecais") = AfficherMonnaie(montTTNonDecais.ToString).Replace(",", separDecim)
        DatRow("PrctTTNonDecais") = prctTTNonDecais.ToString.Replace(",", separDecim)
        DatRow("NbPrevu") = nbPrevu
        DatRow("NbApprouve") = nbApprouve
        DatRow("NbRestant") = nbRestant
        DatRow("MontRestant") = AfficherMonnaie(montRestant.ToString).Replace(",", separDecim)
        DatRow("NbPriori") = nbPriori
        DatRow("MontPriori") = AfficherMonnaie(montPriori.ToString).Replace(",", separDecim)
        DatRow("NbPosteriori") = nbPoster
        DatRow("MontPosteriori") = AfficherMonnaie(montPoster.ToString).Replace(",", separDecim)
        DatRow("NbMarcheMois") = nbMarcheMois
        DatRow("MontMarcheMois") = AfficherMonnaie(montMarcheMois.ToString).Replace(",", separDecim)
        DatRow("MontDecaisseMois") = AfficherMonnaie(montDecaisMois.ToString).Replace(",", separDecim)
        DatRow("MontDecaisMoisLettre") = EnleverApost(montRestLettre.Replace(" zero", ""))
        DatRow("NbAttrib") = nbAttrib
        DatRow("MontAttrib") = AfficherMonnaie(montAttrib.ToString).Replace(",", separDecim)
        DatRow("PrctAttrib") = prctAttrib.ToString.Replace(",", separDecim)

        DatSet.Tables("T_TampStatisticRSM").Rows.Add(DatRow)
        Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
        DatAdapt.Update(DatSet, "T_TampStatisticRSM")
        DatSet.Clear()
        BDQUIT(sqlconn)
    End Sub

    Private Sub InfosEtatProg(ByVal Fonds As String)

        Dim tableBD() As String = {"T_TampProgRsmTravaux", "T_TampProgRsmFournitures", "T_TampProgRsmConsultants", "T_TampProgRsmServAss"}
        Dim typeMarche() As String = {"Travaux", "Fournitures", "Consultants", "Services autres que les services de consultants"}
        Dim leBaille As String = "%"
        If (Fonds <> "Tous") Then leBaille = Fonds

        For pt As Decimal = 0 To tableBD.Length - 1

            query = "DELETE from " & tableBD(pt)
            ExecuteNonQuery(query)

            Dim Nbre As Decimal = 0
            query = "select COUNT(*) from T_Partition where CodeProjet='" & ProjetEnCours & "'"
            Nbre = ExecuteScallar(query)


            If Nbre = 0 Then
            Else

                query = "select LibelleCourt,LibellePartition from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows

                    Dim DatSet = New DataSet
                    query = "select * from " & tableBD(pt)
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, tableBD(pt))
                    Dim DatTable = DatSet.Tables(tableBD(pt))
                    Dim DatRow = DatSet.Tables(tableBD(pt)).NewRow()

                    DatRow("Code") = "Composante " & rw(0).ToString
                    DatRow("Libelle") = rw(1).ToString
                    DatRow("EtapeEnCours") = "-"

                    DatRow("NumMarche") = ""
                    DatRow("Bailleur") = ""
                    DatRow("TypeMarche") = ""
                    DatRow("MethodeMarche") = ""
                    DatRow("NumDossier") = ""
                    DatRow("EtapeEnCours") = ""
                    DatRow("EtapeEnCours") = ""
                    DatRow("EtapeEnCours") = ""
                    DatRow("EtapeEnCours") = ""
                    DatRow("DebutEtape") = ""
                    DatRow("DelaiPrevu") = ""
                    DatRow("FinPrevue") = ""
                    DatRow("RetardEtape") = ""
                    DatRow("RetardMarche") = ""

                    DatSet.Tables(tableBD(pt)).Rows.Add(DatRow)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Update(DatSet, tableBD(pt))
                    DatSet.Clear()

                    Dim CptSouComp As Decimal = 0
                    query = "select LibelleCourt,LibellePartition from T_Partition where CodeClassePartition=2 and LibelleCourt like '" & rw(0).ToString & "%' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows

                        DatSet = New DataSet
                        query = "select * from " & tableBD(pt)
                        Cmd = New MySqlCommand(query, sqlconn)
                        DatAdapt = New MySqlDataAdapter(Cmd)
                        DatAdapt.Fill(DatSet, tableBD(pt))
                        DatTable = DatSet.Tables(tableBD(pt))
                        DatRow = DatSet.Tables(tableBD(pt)).NewRow()

                        DatRow("Code") = "S/C " & rw1(0).ToString
                        DatRow("Libelle") = rw1(1).ToString
                        DatRow("EtapeEnCours") = "-"

                        DatRow("NumMarche") = ""
                        DatRow("Bailleur") = ""
                        DatRow("TypeMarche") = ""
                        DatRow("MethodeMarche") = ""
                        DatRow("NumDossier") = ""
                        DatRow("EtapeEnCours") = ""
                        DatRow("EtapeEnCours") = ""
                        DatRow("EtapeEnCours") = ""
                        DatRow("EtapeEnCours") = ""
                        DatRow("DebutEtape") = ""
                        DatRow("DelaiPrevu") = ""
                        DatRow("FinPrevue") = ""
                        DatRow("RetardEtape") = ""
                        DatRow("RetardMarche") = ""

                        DatSet.Tables(tableBD(pt)).Rows.Add(DatRow)
                        CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                        DatAdapt.Update(DatSet, tableBD(pt))
                        DatSet.Clear()

                        CptSouComp = CptSouComp + 1    'On compte les sous compo 
                        Dim YaMarche As Boolean = False
                        query = "select distinct(R.RefMarche) from T_Partition as P,T_BesoinPartition as B,T_RepartitionParBailleur as R where LENGTH(P.LibelleCourt)='5' and P.LibelleCourt like '" & rw1(0).ToString & "%' and B.TypeBesoin='" & typeMarche(pt) & "' and P.CodePartition=B.CodePartition and P.CodeProjet='" & ProjetEnCours & "' and B.RefBesoinPartition=R.RefBesoinPartition and R.RefMarche<>'0'" ' order by P.LibelleCourt
                        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw2 As DataRow In dt2.Rows

                            query = "select DescriptionMarche,NumeroMarche,InitialeBailleur,TypeMarche,MethodeMarche,NumeroDAO,RevuePrioPost,MontantEstimatif from T_Marche where RefMarche='" & rw2(0) & "' and InitialeBailleur like '" & leBaille & "'"   'Recuperation des élements dans la table marché
                            Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw3 As DataRow In dt3.Rows

                                If (rw3(3).ToString = "Travaux") Then
                                    montTravaux = montTravaux + CDec(rw3(7))
                                ElseIf (rw3(3).ToString = "Fournitures") Then
                                    montFournitures = montFournitures + CDec(rw3(7))
                                ElseIf (rw3(3).ToString = "Consultants") Then
                                    montConsultants = montConsultants + CDec(rw3(7))
                                End If

                                YaMarche = True

                                Dim MarcheEnCours As Boolean = True
                                Dim DDeb As String = ""
                                Dim EDelai As Decimal = 0
                                Dim DFin As String = ""
                                Dim REtape As String = ""
                                Dim RMarche As String = ""
                                Dim DelaiAceJr As Decimal = 0
                                Dim EcartDate As Decimal = 0
                                Dim KodeEtap As Decimal = 0
                                Dim Revue As String = ""
                                Dim PlanNonEtabli As Boolean = True
                                Dim MarcheTermine As Boolean = True

                                query = "select RefEtape,NumeroOrdre,DebutEffectif,FinEffective,DebutPrevu,FinPrevue from T_PlanMarche where DebutPrevu<>'' and DebutEffectif<>'' and FinEffective='' and RefMarche='" & rw2(0) & "' order by NumeroOrdre"
                                Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw4 As DataRow In dt4.Rows

                                    PlanNonEtabli = False
                                    MarcheTermine = False
                                    If (CInt(rw4(1)) = 1 And DateTime.Compare(Now.ToShortDateString, CDate(rw4(4))) < 0) Then
                                        MarcheEnCours = False
                                        Exit For
                                    End If

                                    Dim AjoutRetardEtp As Decimal = 0
                                    DDeb = rw4(2).ToString
                                    EDelai = NbreJourDansPeriode(CDate(rw4(4)), CDate(rw4(5)), False, False, False, False, False, False, False)
                                    DFin = (CDate(DDeb).AddDays(EDelai)).ToShortDateString
                                    DelaiAceJr = NbreJourDansPeriode(CDate(rw4(2)), Now.ToShortDateString, False, False, False, False, False, False, False) 'Retard etape
                                    If (DateTime.Compare(DFin, Now.ToShortDateString) < 0) Then
                                        REtape = Abs(DelaiAceJr - EDelai).ToString & " Jours"
                                        AjoutRetardEtp = Abs(DelaiAceJr - EDelai)
                                    Else
                                        REtape = "R.A.S"
                                    End If
                                    EcartDate = NbreJourDansPeriode(CDate(rw4(2)), CDate(rw4(4)), False, False, False, False, False, False, False)
                                    If (EcartDate <> 0) Then
                                        If (DateTime.Compare(CDate(rw4(2)), CDate(rw4(4))) > 0) Then
                                            RMarche = (EcartDate + AjoutRetardEtp).ToString & " Jours de retard"
                                        Else
                                            If (AjoutRetardEtp > EcartDate) Then
                                                RMarche = Abs(AjoutRetardEtp - EcartDate).ToString & " Jours de retard"
                                            Else
                                                RMarche = Abs(AjoutRetardEtp - EcartDate).ToString & " Jours d'avance"
                                            End If

                                        End If
                                    ElseIf (EcartDate = 0 And AjoutRetardEtp <> 0) Then
                                        RMarche = AjoutRetardEtp.ToString & " Jours de retard"
                                    End If

                                    KodeEtap = rw4(0)

                                Next

                                Revue = rw3(6).ToString
                                If (PlanNonEtabli = True) Then
                                    query = "select * from T_PlanMarche where RefMarche='" & rw2(0) & "' and DebutPrevu<>'' and FinPrevue<>''"
                                    Dim dt5 As DataTable = ExcecuteSelectQuery(query)
                                    For Each rw5 As DataRow In dt5.Rows
                                        PlanNonEtabli = False
                                    Next

                                End If


                                'Marché achevé
                                Dim MarcheAcheve As Boolean = False
                                If (MarcheTermine = True) Then

                                    Dim Ecart2 As Decimal = 0
                                    query = "select FinEffective,FinPrevue from T_PlanMarche where DebutPrevu<>'' and DebutEffectif<>'' and RefMarche='" & rw2(0) & "' order by NumeroOrdre"
                                    Dim dt6 As DataTable = ExcecuteSelectQuery(query)
                                    For Each rw6 As DataRow In dt6.Rows

                                        If (rw6(0).ToString <> "") Then
                                            MarcheAcheve = True

                                            Ecart2 = NbreJourDansPeriode(CDate(rw6(0)), CDate(rw6(1)), False, False, False, False, False, False, False)
                                            If (DateTime.Compare(CDate(rw6(0)), CDate(rw6(1))) > 0) Then
                                                RMarche = Ecart2.ToString & " Jours de retard"
                                            ElseIf (DateTime.Compare(CDate(rw6(0)), CDate(rw6(1))) < 0) Then
                                                RMarche = Ecart2.ToString & " Jours d'avance"
                                            End If

                                        Else
                                            MarcheAcheve = False
                                        End If

                                    Next

                                End If

                                'Libelle de l'étape
                                Dim LibEtape As String = ""
                                query = "select TitreEtape,NumeroOrdre from T_EtapeMarche where RefEtape='" & KodeEtap & "'"
                                Dim dt7 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw7 As DataRow In dt7.Rows
                                    LibEtape = "(E" & rw7(1).ToString & ") " & rw7(0)
                                Next

                                DatSet = New DataSet
                                query = "select * from " & tableBD(pt)
                                Cmd = New MySqlCommand(query, sqlconn)
                                DatAdapt = New MySqlDataAdapter(Cmd)
                                DatAdapt.Fill(DatSet, tableBD(pt))
                                DatTable = DatSet.Tables(tableBD(pt))
                                DatRow = DatSet.Tables(tableBD(pt)).NewRow()

                                DatRow("Code") = rw2(0).ToString
                                DatRow("Libelle") = EnleverApost(rw3(0).ToString)
                                DatRow("NumMarche") = "A " & Revue.Replace("*", "")
                                DatRow("Bailleur") = rw3(2)
                                DatRow("TypeMarche") = rw3(3)
                                DatRow("MethodeMarche") = rw3(4)
                                DatRow("NumDossier") = rw3(5)
                                If (PlanNonEtabli = True) Then
                                    DatRow("EtapeEnCours") = "Non Echu"
                                ElseIf (MarcheAcheve = True) Then
                                    DatRow("EtapeEnCours") = "Marché Achevé"
                                ElseIf (MarcheEnCours = False) Then
                                    DatRow("EtapeEnCours") = "En Attente"
                                Else
                                    DatRow("EtapeEnCours") = EnleverApost(LibEtape)
                                End If
                                DatRow("DebutEtape") = DDeb
                                If (EDelai <> 0) Then
                                    DatRow("DelaiPrevu") = EDelai.ToString & " Jours"
                                End If
                                DatRow("FinPrevue") = DFin
                                DatRow("RetardEtape") = REtape
                                DatRow("RetardMarche") = EnleverApost(RMarche)

                                DatSet.Tables(tableBD(pt)).Rows.Add(DatRow)
                                CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                                DatAdapt.Update(DatSet, tableBD(pt))
                                DatSet.Clear()

                            Next

                        Next
                        BDQUIT(sqlconn)

                        If (YaMarche = False) Then
                            'Suppression des sous compo sans marché
                            query = "DELETE from " & tableBD(pt) & " where Code='S/C " & rw1(0).ToString & "'"
                            ExecuteNonQuery(query)
                            CptSouComp = CptSouComp - 1
                        End If

                    Next


                    If (CptSouComp < 1) Then
                        'Suppression des compo sans sous compo
                        query = "DELETE from " & tableBD(pt) & " where Code='Composante " & rw(0).ToString & "'"
                        ExecuteNonQuery(query)
                    End If

                Next

            End If

        Next

    End Sub

    Private Sub InfosExecutionRsm(ByVal Fonds As String, ByVal Devise As String)

        Dim tableBD() As String = {"T_TampExecRsmTravaux", "T_TampExecRsmFournitures", "T_TampExecRsmConsultants", "T_TampExecRsmServAss"}
        Dim typeMarche() As String = {"Travaux", "Fournitures", "Consultants", "Services autres que les services de consultants"}
        Dim leBaille As String = "%"
        If (Fonds <> "Tous") Then
            leBaille = Fonds
        End If
        Dim NbVirg As Integer = 0
        Dim separDecim As String = ","

        For pt As Decimal = 0 To tableBD.Length - 1

            query = "DELETE from " & tableBD(pt)
            ExecuteNonQuery(query)


            Dim LesCodeComp(20) As String
            Dim LesMontEstimComp(20) As String
            Dim LesMontMarcheComp(20) As String
            Dim LesMontDecaisComp(20) As String
            Dim LesMontExcedComp(20) As String
            Dim NbComp As Decimal = 0
            For i As Integer = 0 To 19
                LesCodeComp(i) = ""
                LesMontEstimComp(i) = "0"
                LesMontMarcheComp(i) = "0"
                LesMontDecaisComp(i) = "0"
                LesMontExcedComp(i) = "0"
            Next

            Dim LesCodeSouComp(50) As String
            Dim LesMontEstimSouComp(50) As String
            Dim LesMontMarcheSouComp(50) As String
            Dim LesMontDecaisSouComp(50) As String
            Dim LesMontExcedSouComp(50) As String
            Dim NbSouComp As Decimal = 0
            For j As Integer = 0 To 49
                LesCodeSouComp(j) = ""
                LesMontEstimSouComp(j) = "0"
                LesMontMarcheSouComp(j) = "0"
                LesMontDecaisSouComp(j) = "0"
                LesMontExcedSouComp(j) = "0"
            Next

            Dim Nbre As Decimal = 0
            query = "select COUNT(*) from T_Partition where CodeProjet='" & ProjetEnCours & "'"
            Nbre = ExecuteScallar(query)

            If Nbre = 0 Then
            Else

                query = "select LibelleCourt,LibellePartition from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt.Rows

                    Dim montEstimComp As Decimal = 0
                    Dim montMarcheComp As Decimal = 0
                    Dim montDecaisComp As Decimal = 0
                    Dim montExcedComp As Decimal = 0

                    Dim DatSet = New DataSet
                    query = "select * from " & tableBD(pt)
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, tableBD(pt))
                    Dim DatTable = DatSet.Tables(tableBD(pt))
                    Dim DatRow = DatSet.Tables(tableBD(pt)).NewRow()

                    DatRow("Code") = "Composante " & rw(0).ToString
                    DatRow("Libelle") = rw(1).ToString

                    DatSet.Tables(tableBD(pt)).Rows.Add(DatRow)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Update(DatSet, tableBD(pt))
                    DatSet.Clear()

                    Dim CptSouComp As Decimal = 0
                    query = "select LibelleCourt,LibellePartition from T_Partition where CodeClassePartition=2 and LibelleCourt like '" & rw(0).ToString & "%' and CodeProjet='" & ProjetEnCours & "' order by LibelleCourt"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt1.Rows

                        Dim montEstimSouComp As Decimal = 0
                        Dim montMarcheSouComp As Decimal = 0
                        Dim montDecaisSouComp As Decimal = 0
                        Dim montExcedSouComp As Decimal = 0

                        DatSet = New DataSet
                        query = "select * from " & tableBD(pt)
                        Cmd = New MySqlCommand(query, sqlconn)
                        DatAdapt = New MySqlDataAdapter(Cmd)
                        DatAdapt.Fill(DatSet, tableBD(pt))
                        DatTable = DatSet.Tables(tableBD(pt))
                        DatRow = DatSet.Tables(tableBD(pt)).NewRow()

                        DatRow("Code") = "S/C " & rw1(0).ToString
                        DatRow("Libelle") = rw1(1).ToString

                        DatSet.Tables(tableBD(pt)).Rows.Add(DatRow)
                        CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                        DatAdapt.Update(DatSet, tableBD(pt))
                        DatSet.Clear()

                        CptSouComp = CptSouComp + 1
                        Dim YaMarche As Boolean = False

                        query = "select R.RefMarche,P.LibelleCourt,P.LibellePartition from T_Partition as P,T_BesoinPartition as B,T_BesoinMarche as R where LENGTH(P.LibelleCourt)>='5' and P.LibelleCourt like '" & rw1(0).ToString & "%' and B.TypeBesoin='" & typeMarche(pt) & "' and P.CodePartition=B.CodePartition and P.CodeProjet='" & ProjetEnCours & "' and R.RefBesoinPartition=B.RefBesoinPartition and B.RefMarche<>'0' order by P.LibelleCourt"
                        Dim dt2 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw2 As DataRow In dt2.Rows

                            query = "select DescriptionMarche,NumeroMarche,InitialeBailleur,TypeMarche,MethodeMarche,NumeroDAO,RevuePrioPost,MontantEstimatif from T_Marche where RefMarche='" & rw2(0) & "' and InitialeBailleur like '" & leBaille & "'"   'Recuperation des élements dans la table marché
                            Dim dt3 As DataTable = ExcecuteSelectQuery(query)
                            For Each rw3 As DataRow In dt3.Rows

                                Dim MarcheSigne As Boolean = False
                                Dim DateMarcheSigne As String = ""
                                Dim MontEstim As Decimal = 0

                                query = "select P.FinEffective from T_PlanMarche as P, T_EtapeMarche as E where P.RefEtape=E.RefEtape and E.ApproMarche='OUI' and P.FinEffective<>'' and P.RefMarche='" & rw2(0).ToString & "'"
                                Dim dt4 As DataTable = ExcecuteSelectQuery(query)
                                For Each rw4 As DataRow In dt4.Rows
                                    DateMarcheSigne = rw4(0).ToString
                                    MontEstim = CDec(rw4(7))
                                Next

                                'Verification décaissement
                                Dim typeCours As String = ""
                                query = "select M.TypeMarche from T_MarcheSigne as M, T_COMP_ACTIVITE as R where M.NumeroMarche=R.NumeroMarche and M.RefMarche='" & rw2(0) & "' and M.CodeProjet='" & ProjetEnCours & "'"
                                Dim dt5 As DataTable = ExcecuteSelectQuery(query)
                                If dt5.Rows.Count = 1 Then
                                    typeCours = dt5.Rows(0).ToString
                                    MarcheSigne = True
                                End If

                                If (MarcheSigne = True) Then
                                    YaMarche = True

                                    If (Devise <> "FCFA") Then NbVirg = 2
                                    'Conversion du montant
                                    Dim tauxConv As Decimal = 1
                                    If (Devise = "US$") Then separDecim = "."
                                    query = "select TauxDevise from T_Devise where AbregeDevise='" & Devise & "'"
                                    Dim dt11 As DataTable = ExcecuteSelectQuery(query)
                                    For Each rw11 As DataRow In dt11.Rows
                                        tauxConv = CDec(rw11(0))
                                    Next
                                    MontEstim = Math.Round(MontEstim / tauxConv, NbVirg)

                                    'Attributaire et montant marché
                                    Dim LesRefLot(10) As String
                                    Dim LesFournis(10) As String
                                    Dim LesBons(10) As Decimal
                                    LesRefLot(0) = ""
                                    LesFournis(0) = ""
                                    Dim NbEnrg As Decimal = 0
                                    Dim montContrat As Decimal = 0

                                    Dim laReq As String = ""
                                    If (typeCours <> "Consultants") Then
                                        laReq = "select RefLot,CodeFournis,MontantHT,NumeroMarche from T_MarcheSigne where RefMarche='" & rw2(0) & "'"
                                    Else
                                        laReq = "select RefLot,RefConsult,MontantHT,NumeroMarche from T_MarcheSigne where RefMarche='" & rw2(0) & "'"
                                    End If

                                    If (laReq <> "") Then
                                        query = laReq
                                        Dim dt6 As DataTable = ExcecuteSelectQuery(query)
                                        For Each rw6 As DataRow In dt6.Rows
                                            LesRefLot(NbEnrg) = rw6(0)
                                            LesFournis(NbEnrg) = rw6(1)
                                            NbEnrg = NbEnrg + 1
                                            montContrat = montContrat + CDec(rw6(2))
                                        Next
                                    End If
                                    montContrat = Math.Round(montContrat / tauxConv, NbVirg)

                                    Dim Attribut As String = ""
                                    If (typeCours <> "Consultants") Then
                                        For k As Integer = 0 To NbEnrg - 1

                                            query = "select CodeLot from T_LotDAO where RefLot='" & LesRefLot(k) & "'"
                                            Dim dt7 As DataTable = ExcecuteSelectQuery(query)
                                            For Each rw7 As DataRow In dt7.Rows
                                                LesRefLot(k) = rw7(0)
                                            Next

                                            query = "select AbregeNomFournis,NomFournis from T_Fournisseur where CodeFournis='" & LesFournis(k) & "'"
                                            Dim dt8 As DataTable = ExcecuteSelectQuery(query)
                                            For Each rw8 As DataRow In dt8.Rows
                                                LesFournis(k) = "(" & MettreApost(rw8(0).ToString) & ") " & MettreApost(rw8(1).ToString)
                                            Next
                                        Next

                                        Dim Tampx As String = ""
                                        For i As Integer = 0 To NbEnrg - 2
                                            For j As Integer = i To NbEnrg - 1
                                                If (LesRefLot(i) > LesRefLot(j)) Then
                                                    Tampx = LesRefLot(i)
                                                    LesRefLot(i) = LesRefLot(j)
                                                    LesRefLot(j) = Tampx

                                                    Tampx = LesFournis(i)
                                                    LesFournis(i) = LesFournis(j)
                                                    LesFournis(j) = Tampx
                                                End If
                                            Next
                                        Next

                                        If (LesRefLot(0) <> "") Then
                                            Attribut = "-LotN°" & LesRefLot(0) & " : " & LesFournis(0)
                                            For k As Integer = 1 To NbEnrg - 1
                                                Attribut = Attribut & "   " & "-LotN°" & LesRefLot(k) & " : " & LesFournis(k)
                                            Next
                                        End If

                                    Else

                                        For k As Integer = 0 To NbEnrg - 1
                                            query = "select NomConsult from T_Consultant where RefConsult='" & LesFournis(k) & "'"
                                            Dim dt9 As DataTable = ExcecuteSelectQuery(query)
                                            For Each rw9 As DataRow In dt9.Rows
                                                Attribut = MettreApost(rw9(0).ToString)
                                            Next
                                        Next

                                    End If

                                    'Recherche des montants apurés
                                    Dim MontApure As Decimal = 0
                                    For i As Integer = 0 To NbEnrg - 1
                                        query = "select Montant_act from T_COMP_ACTIVITE where NumeroMarche='" & LesBons(i) & "' and Code_Projet='" & ProjetEnCours & "'"
                                        Dim dt10 As DataTable = ExcecuteSelectQuery(query)
                                        For Each rw10 As DataRow In dt10.Rows
                                            MontApure = MontApure + CDec(rw10(0))
                                        Next
                                    Next
                                    MontApure = Math.Round(MontApure / tauxConv, NbVirg)

                                    'Solde et pourcentage
                                    Dim SoldeMarche As Decimal = montContrat - MontApure
                                    Dim PrctApure As Decimal = 0
                                    If (montContrat <> 0) Then
                                        PrctApure = Math.Round((MontApure * 100) / montContrat, 2)
                                    End If

                                    'Montant excedent
                                    Dim MontExced As Decimal = MontEstim - montContrat
                                    Dim SigneExced As String = ""
                                    If (MontExced < 0) Then SigneExced = "-"

                                    DatSet = New DataSet
                                    query = "select * from " & tableBD(pt)
                                    Cmd = New MySqlCommand(query, sqlconn)
                                    DatAdapt = New MySqlDataAdapter(Cmd)
                                    DatAdapt.Fill(DatSet, tableBD(pt))
                                    DatTable = DatSet.Tables(tableBD(pt))
                                    DatRow = DatSet.Tables(tableBD(pt)).NewRow()

                                    DatRow("Code") = rw2(0).ToString
                                    DatRow("Libelle") = EnleverApost(rw3(0).ToString)
                                    DatRow("MethodeMarche") = rw3(4)
                                    DatRow("NumDossier") = rw3(5)
                                    DatRow("MontantEstim") = AfficherMonnaie(MontEstim.ToString).Replace(",", separDecim)
                                    DatRow("DateSignature") = DateMarcheSigne
                                    DatRow("Attributaire") = Attribut
                                    DatRow("MontantMarche") = AfficherMonnaie(montContrat.ToString).Replace(",", separDecim)
                                    DatRow("MontantDecaisse") = AfficherMonnaie(MontApure.ToString).Replace(",", separDecim)
                                    DatRow("SoldeMarche") = AfficherMonnaie(SoldeMarche.ToString).Replace(",", separDecim)
                                    DatRow("PrctDecaisse") = PrctApure.ToString.Replace(",", separDecim)
                                    DatRow("BeneficeEstim") = SigneExced & AfficherMonnaie(MontExced.ToString).Replace(",", separDecim)

                                    DatSet.Tables(tableBD(pt)).Rows.Add(DatRow)
                                    CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                                    DatAdapt.Update(DatSet, tableBD(pt))
                                    DatSet.Clear()

                                    montEstimSouComp = montEstimSouComp + MontEstim
                                    montMarcheSouComp = montMarcheSouComp + montContrat
                                    montDecaisSouComp = montDecaisSouComp + MontApure
                                    montExcedSouComp = montExcedSouComp + MontExced

                                End If

                            Next

                        Next
                        BDQUIT(sqlconn)

                        If (YaMarche = False) Then
                            'Suppression des sous compo sans marché
                            query = "DELETE from " & tableBD(pt) & " where Code='S/C " & rw1(0).ToString & "'"
                            ExecuteNonQuery(query)
                            CptSouComp = CptSouComp - 1
                        Else

                            montEstimComp = montEstimComp + montEstimSouComp
                            montMarcheComp = montMarcheComp + montMarcheSouComp
                            montDecaisComp = montDecaisComp + montDecaisSouComp
                            montExcedComp = montExcedComp + montExcedSouComp
                            Dim SigneExcedSouComp As String = ""
                            If (montExcedSouComp < 0) Then SigneExcedSouComp = "-"

                            LesCodeSouComp(NbSouComp) = "S/C " & rw1(0).ToString
                            LesMontEstimSouComp(NbSouComp) = AfficherMonnaie(montEstimSouComp.ToString).Replace(",", separDecim)
                            LesMontMarcheSouComp(NbSouComp) = AfficherMonnaie(montMarcheSouComp.ToString).Replace(",", separDecim)
                            LesMontDecaisSouComp(NbSouComp) = AfficherMonnaie(montDecaisSouComp.ToString).Replace(",", separDecim)
                            LesMontExcedSouComp(NbSouComp) = SigneExcedSouComp & AfficherMonnaie(montExcedSouComp.ToString).Replace(",", separDecim)

                            NbSouComp = NbSouComp + 1

                        End If

                    Next

                    If (CptSouComp < 1) Then
                        'Suppression des compo sans sous compo
                        query = "DELETE from " & tableBD(pt) & " where Code='Composante " & rw(0).ToString & "'"
                        ExecuteNonQuery(query)
                    Else
                        Dim SigneExcedComp As String = ""
                        If (montExcedComp < 0) Then SigneExcedComp = "-"
                        LesCodeComp(NbComp) = "Composante " & rw(0).ToString
                        LesMontEstimComp(NbComp) = AfficherMonnaie(montEstimComp.ToString).Replace(",", separDecim)
                        LesMontMarcheComp(NbComp) = AfficherMonnaie(montMarcheComp.ToString).Replace(",", separDecim)
                        LesMontDecaisComp(NbComp) = AfficherMonnaie(montDecaisComp.ToString).Replace(",", separDecim)
                        LesMontExcedComp(NbComp) = SigneExcedComp & AfficherMonnaie(montExcedComp.ToString).Replace(",", separDecim)
                        NbComp = NbComp + 1
                    End If
                Next

            End If

            For i As Integer = 0 To NbComp - 1

                query = "Update " & tableBD(pt) & " set BeneficeEstim='" + LesMontExcedComp(i) + "', MontantEstim='" + LesMontEstimComp(i) + "', MontantMarche='" + LesMontMarcheComp(i) + "', MontantDecaisse='" + LesMontDecaisComp(i) + "', MontantEstim='" + LesMontEstimComp(i) + "', SoldeMarche='" + AfficherMonnaie((CDec(LesMontMarcheComp(i).Replace(" ", "")) - CDec(LesMontDecaisComp(i).Replace(" ", ""))).ToString).Replace(",", separDecim) + "' where Code='" & LesCodeComp(i) & "'"
                ExecuteNonQuery(query)

                If (LesMontMarcheComp(i) <> "0") Then
                    query = "Update " & tableBD(pt) & " set PrctDecaisse='" + Math.Round((CDec(LesMontDecaisComp(i).Replace(" ", "")) * 100) / CDec(LesMontMarcheComp(i).Replace(" ", "")), 2).ToString.Replace(",", separDecim) + "', BeneficeEstim='" + LesMontExcedComp(i) + "', MontantEstim='" + LesMontEstimComp(i) + "', MontantMarche='" + LesMontMarcheComp(i) + "', MontantDecaisse='" + LesMontDecaisComp(i) + "', MontantEstim='" + LesMontEstimComp(i) + "', SoldeMarche='" + AfficherMonnaie((CDec(LesMontMarcheComp(i).Replace(" ", "")) - CDec(LesMontDecaisComp(i).Replace(" ", ""))).ToString).Replace(",", separDecim) + "' where Code='" & LesCodeComp(i) & "'"
                    ExecuteNonQuery(query)
                End If

            Next

            For j As Integer = 0 To NbSouComp - 1
                query = "Update " & tableBD(pt) & " set BeneficeEstim='" + LesMontExcedComp(j) + "', MontantEstim='" + LesMontEstimComp(j) + "', MontantMarche='" + LesMontMarcheComp(j) + "', MontantDecaisse='" + LesMontDecaisComp(j) + "', MontantEstim='" + LesMontEstimComp(j) + "', SoldeMarche='" + AfficherMonnaie((CDec(LesMontMarcheComp(j).Replace(" ", "")) - CDec(LesMontDecaisComp(j).Replace(" ", ""))).ToString).Replace(",", separDecim) + "' where Code='" & LesCodeComp(j) & "'"
                ExecuteNonQuery(query)

                If (LesMontMarcheComp(j) <> "0") Then
                    query = "Update " & tableBD(pt) & " set PrctDecaisse='" + Math.Round((CDec(LesMontDecaisComp(j).Replace(" ", "")) * 100) / CDec(LesMontMarcheComp(j).Replace(" ", "")), 2).ToString.Replace(",", separDecim) + "', BeneficeEstim='" + LesMontExcedComp(j) + "', MontantEstim='" + LesMontEstimComp(j) + "', MontantMarche='" + LesMontMarcheComp(j) + "', MontantDecaisse='" + LesMontDecaisComp(j) + "', MontantEstim='" + LesMontEstimComp(j) + "', SoldeMarche='" + AfficherMonnaie((CDec(LesMontMarcheComp(j).Replace(" ", "")) - CDec(LesMontDecaisComp(j).Replace(" ", ""))).ToString).Replace(",", separDecim) + "' where Code='" & LesCodeComp(j) & "'"
                    ExecuteNonQuery(query)
                End If

            Next

        Next

    End Sub

    Private Sub BtEnrgRapp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnrgRapp.Click

        Dim NomDossier As String = line & "\RSM\" & Now.ToShortDateString.Replace("/", "_")
        If (Directory.Exists(NomDossier) = False) Then
            Directory.CreateDirectory(NomDossier)
        End If
        'SaisieDetailsRapp.SaveDocument(NomDossier & "\DétailsRapport.rtf", DocumentFormat.Rtf)


    End Sub

    Private Sub BtImprimerRapp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAnnuler.Click

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

        If (ChkDetails.Checked = True) Then NbElt = NbElt + 1
        If (ChkStatistic.Checked = True) Then NbElt = NbElt + 1
        If (ChkSituation.Checked = True) Then NbElt = NbElt + 1
        ' If (ChkAttente.Checked = True) Then NbElt = NbElt + 1
        If (ChkExecution.Checked = True) Then NbElt = NbElt + 1
        If (ChkDAO.Checked = True) Then NbElt = NbElt + 1
        NbTot = NbElt

        'Impression DAO édités
        If (ChkDAO.Checked = True) Then
            NbElt = NbElt - 1
            SommaireRapport = "Chapitre " & Chap(NbElt) & "./" & vbTab & "Listes des DAO et DP" & vbNewLine & SommaireRapport

            ChkDAO_CheckedChanged(Me, e)

            printTitre(Chap(NbElt), "LISTES DES DAO ET DP " & BaillRapp)
        End If
        '***********************************

        'Impression Execution marchés
        If (ChkExecution.Checked = True) Then
            NbElt = NbElt - 1
            SommaireRapport = "Chapitre " & Chap(NbElt) & "./" & vbTab & "Situation financière de l'exécution des marchés attribués" & vbNewLine & SommaireRapport
            ChkExecution_CheckedChanged(Me, e)
            printTitre(Chap(NbElt), "SITUATION FINANCIERE DE L'EXECUTION DES MARCHES ATTRIBUES " & BaillRapp)
        End If
        '***********************************

        ''Impression marchés en attente
        'If (ChkAttente.Checked = True) Then
        '    NbElt = NbElt - 1
        '    SommaireRapport = "Chapitre " & Chap(NbElt) & "./" & vbTab & "Marchés non planifiés et non débutés" & vbNewLine & SommaireRapport
        '    ChkAttente_CheckedChanged(Me, e)
        '    printTitre(Chap(NbElt), "MARCHES NON PLANIFIES ET NON DEBUTES " & BaillRapp)
        'End If
        '***********************************

        'Impression Situation des marchés
        If (ChkSituation.Checked = True) Then
            NbElt = NbElt - 1
            SommaireRapport = "Chapitre " & Chap(NbElt) & "./" & vbTab & "Suivi du processus de passation de marchés" & vbNewLine & SommaireRapport
            ChkSituation_CheckedChanged(Me, e)
            printTitre(Chap(NbElt), "SUIVI DU PROCESSUS DE PASSATION DE MARCHES " & BaillRapp)
        End If
        '***********************************

        'Impression Statistiques des marchés
        If (ChkStatistic.Checked = True) Then
            NbElt = NbElt - 1
            SommaireRapport = "Chapitre " & Chap(NbElt) & "./" & vbTab & "Suivi des marchés et engagements" & vbNewLine & SommaireRapport

            If (ChkGraph.Checked = True) Then

                Dim PaysageDoc As New PageSetupDialog()
                If (ChkGraph8.Checked = True) Then
                    PaysageDoc.Document = ChartGraph8.Printing.PrintDocument
                    PaysageDoc.PageSettings.Landscape = True   'mettre en paysage
                    ChartGraph8.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
                    NumImprim = NumImprim + 1
                    ChartGraph8.Printing.Print(False)
                End If
                If (ChkGraph7.Checked = True) Then
                    PaysageDoc.Document = ChartGraph7.Printing.PrintDocument
                    PaysageDoc.PageSettings.Landscape = True   'mettre en paysage
                    ChartGraph7.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
                    NumImprim = NumImprim + 1
                    ChartGraph7.Printing.Print(False)
                End If
                If (ChkGraph6.Checked = True) Then
                    PaysageDoc.Document = ChartGraph6.Printing.PrintDocument
                    PaysageDoc.PageSettings.Landscape = True   'mettre en paysage
                    ChartGraph6.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
                    NumImprim = NumImprim + 1
                    ChartGraph6.Printing.Print(False)
                End If
                If (ChkGraph5.Checked = True) Then
                    PaysageDoc.Document = ChartGraph5.Printing.PrintDocument
                    PaysageDoc.PageSettings.Landscape = True
                    ChartGraph5.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
                    NumImprim = NumImprim + 1
                    ChartGraph5.Printing.Print(False)
                End If
                If (ChkGraph4.Checked = True) Then
                    PaysageDoc.Document = ChartGraph4.Printing.PrintDocument
                    PaysageDoc.PageSettings.Landscape = True
                    ChartGraph4.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
                    NumImprim = NumImprim + 1
                    ChartGraph4.Printing.Print(False)
                End If
                If (ChkGraph3.Checked = True) Then
                    PaysageDoc.Document = ChartGraph3.Printing.PrintDocument
                    PaysageDoc.PageSettings.Landscape = True
                    ChartGraph3.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
                    NumImprim = NumImprim + 1
                    ChartGraph3.Printing.Print(False)
                End If
                If (ChkGraph2.Checked = True) Then
                    PaysageDoc.Document = ChartGraph2.Printing.PrintDocument
                    PaysageDoc.PageSettings.Landscape = True
                    ChartGraph2.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
                    NumImprim = NumImprim + 1
                    ChartGraph2.Printing.Print(False)
                End If
                If (ChkGraph1.Checked = True) Then
                    PaysageDoc.Document = ChartGraph1.Printing.PrintDocument
                    PaysageDoc.PageSettings.Landscape = True
                    ChartGraph1.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
                    NumImprim = NumImprim + 1
                    ChartGraph1.Printing.Print(False)
                End If
                If (ChkGraph0.Checked = True) Then
                    PaysageDoc.Document = ChartGraph0.Printing.PrintDocument
                    PaysageDoc.PageSettings.Landscape = True
                    ChartGraph0.SaveImage(NomDossier & "\R" & NumImprim.ToString & ".jpg", ChartImageFormat.Jpeg)
                    NumImprim = NumImprim + 1
                    ChartGraph0.Printing.Print(False)
                End If

            End If


            ChkStatistic_CheckedChanged(Me, e)

            printTitre(Chap(NbElt), "SUIVI DES MARCHES ET ENGAGEMENTS " & BaillRapp)
        End If
        '***********************************

        'Impression Détails rapport
        If (ChkDetails.Checked = True) Then
            NbElt = NbElt - 1
            SommaireRapport = "Chapitre " & Chap(NbElt) & "./    " & "Informations sur le projet" & vbNewLine & SommaireRapport
            'SaisieDetailsRapp.SaveDocument(NomDossier & "\R" & NumImprim.ToString & ".pdf", DevExpress.XtraRichEdit.DocumentFormat.Doc)
            'NumImprim = NumImprim + 1
            'SaisieDetailsRapp.Print()
        End If
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
        If (XtraTabControl1.SelectedTabPage Is PageDetailsRapport) Then
            'RibbonControl1.Minimized = False
            'RibbonControl1.SelectedPage = HomeRibbonPage1
        Else
            'RibbonControl1.Minimized = True
        End If
    End Sub

    Private Sub ChkDetails_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkDetails.CheckedChanged
        ActivImprim()
        If (ChkDetails.Checked = True) Then
            XtraTabControl1.SelectedTabPage = PageDetailsRapport
            'SaisieDetailsRapp.Enabled = True
        Else
            '  SaisieDetailsRapp.Enabled = False
        End If

    End Sub

    Private Sub ChkStatistic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkStatistic.CheckedChanged
        ActivImprim()
        If (ChkStatistic.Checked = True) Then
            PageStatistic.PageVisible = True
            XtraTabControl1.SelectedTabPage = PageStatistic
            ViewStatistic.Enabled = True

            Dim EnteteR As String = ""
            query = "select Nomprojet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If (BailDuJour = "Tous") Then
                    ReponseDialog = "du Projet"
                Else
                    ReponseDialog = "de l'" & BailDuJour
                End If
                EnteteR = MettreApost(rw(0).ToString).ToUpper & vbNewLine & "(" & ProjetEnCours.ToUpper & ")" &
                vbNewLine & "Suivi des Marchés et Engagements " & ReponseDialog & " à la date du " & Now.ToShortDateString
                ReponseDialog = ""
            Next

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\RSM\"
            Dim report As New ReportDocument

            Dim DatSet = New DataSet
            report.Load(Chemin & "RapportStatisticMarche.rpt")

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
            report.SetParameterValue("EnteteStatistic", EnteteR)
            report.SetParameterValue("Devise", MonnaieDuJour)
            If (imprEnCours = True) Then
                report.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\R" & NumImprim.ToString & ".pdf")
                NumImprim = NumImprim + 1
                report.PrintToPrinter(1, True, 0, 0)
            Else
                ViewStatistic.ReportSource = report
            End If

        Else
            PageStatistic.PageVisible = False
            ViewStatistic.ReportSource = Nothing
            ViewStatistic.Enabled = False
        End If


    End Sub


    Private Sub ChkAttente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ActivImprim()
        ' If (ChkAttente.Checked = True) Then

        PageAttente.PageVisible = True
            XtraTabControl1.SelectedTabPage = PageAttente
            ViewAttente.Enabled = True

            Dim EnteteR As String = ""
            query = "select NomProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If (BailDuJour = "Tous") Then
                    ReponseDialog = "du Projet"
                Else
                    ReponseDialog = "de l'" & BailDuJour
                End If
                EnteteR = MettreApost(rw(0).ToString).ToUpper & vbNewLine & "(" & ProjetEnCours.ToUpper & ")" &
                vbNewLine & "Marchés Non Planifiés et Non Débutés " & ReponseDialog & " à la date du " & Now.ToShortDateString
                ReponseDialog = ""
            Next

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\RSM\"
            Dim report As New ReportDocument

            'dim DatSet = New DataSet
            report.Load(Chemin & "RapportMarchesEnAttente.rpt")

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
            'report.SetDataSource(DatSet)
            report.SetParameterValue("EnteteSituationAttente", EnteteR)
            If (imprEnCours = True) Then
                report.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\R" & NumImprim.ToString & ".pdf")
                NumImprim = NumImprim + 1
                report.PrintToPrinter(1, True, 0, 0)
            Else
                ViewAttente.ReportSource = report
            End If

            ' Else
            PageAttente.PageVisible = False
            ViewAttente.ReportSource = Nothing
            ViewAttente.Enabled = False
       ' End If


    End Sub

    Private Sub ChkSituation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSituation.CheckedChanged
        ActivImprim()
        If (ChkSituation.Checked = True) Then
            PageSituation.PageVisible = True
            XtraTabControl1.SelectedTabPage = PageSituation
            ViewSituation.Enabled = True

            Dim EnteteR As String = ""
            query = "select NomProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If (BailDuJour = "Tous") Then
                    ReponseDialog = "du Projet"
                Else
                    ReponseDialog = "de l'" & BailDuJour
                End If
                EnteteR = MettreApost(rw(0).ToString).ToUpper & vbNewLine & "(" & ProjetEnCours.ToUpper & ")" &
                vbNewLine & "Suivi du Processus de Passation des Marchés " & ReponseDialog & " à la date du " & Now.ToShortDateString
                ReponseDialog = ""
            Next

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\RSM\"
            Dim report As New ReportDocument

            'dim DatSet = New DataSet
            report.Load(Chemin & "RapportSituationMarches.rpt")

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

            report.SetParameterValue("EnteteSituation", EnteteR)
            If (imprEnCours = True) Then
                report.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\R" & NumImprim.ToString & ".pdf")
                NumImprim = NumImprim + 1
                report.PrintToPrinter(1, True, 0, 0)
            Else
                ViewSituation.ReportSource = report
            End If

        Else
            PageSituation.PageVisible = False
            ViewSituation.ReportSource = Nothing
            ViewSituation.Enabled = False
        End If


    End Sub

    Private Sub ChkExecution_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkExecution.CheckedChanged
        ActivImprim()
        If (ChkExecution.Checked = True) Then
            PageExecution.PageVisible = True
            XtraTabControl1.SelectedTabPage = PageExecution
            ViewExecution.Enabled = True

            Dim EnteteR As String = ""
            query = "select NomProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If (BailDuJour = "Tous") Then
                    ReponseDialog = "du Projet"
                Else
                    ReponseDialog = BailDuJour
                End If
                EnteteR = MettreApost(rw(0).ToString).ToUpper & vbNewLine & "(" & ProjetEnCours.ToUpper & ")" &
                vbNewLine & "Suivi du Financement des Marchés " & ReponseDialog & " en Cours d'Exécution à la date du " & Now.ToShortDateString
                ReponseDialog = ""
            Next

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\RSM\"
            Dim report As New ReportDocument

            report.Load(Chemin & "RapportExecutionMarches.rpt")

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

            report.SetParameterValue("EnteteExecution", EnteteR)
            report.SetParameterValue("Devise", MonnaieDuJour)
            report.SetParameterValue("Devise1", MonnaieDuJour)
            report.SetParameterValue("Devise2", MonnaieDuJour)
            report.SetParameterValue("Devise3", MonnaieDuJour)
            report.SetParameterValue("Devise4", MonnaieDuJour)
            If (imprEnCours = True) Then
                report.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\R" & NumImprim.ToString & ".pdf")
                NumImprim = NumImprim + 1
                report.PrintToPrinter(1, True, 0, 0)
            Else
                ViewExecution.ReportSource = report
            End If

        Else
            PageExecution.PageVisible = False
            ViewExecution.ReportSource = Nothing
            ViewExecution.Enabled = False
        End If


    End Sub

    Private Sub ChkDAO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkDAO.CheckedChanged
        ActivImprim()
        If (ChkDAO.Checked = True) Then
            'PageDAO.PageVisible = True
            'XtraTabControl1.SelectedTabPage = PageDAO
            'ViewDAO.Enabled = True
            'ViewDp.Enabled = True

            Dim EnteteR As String = ""
            Dim EnteteR2 As String = ""
            query = "select NomProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If (BailDuJour = "Tous") Then
                    ReponseDialog = "du Projet"
                Else
                    ReponseDialog = "de l'" & BailDuJour
                End If
                EnteteR = MettreApost(rw(0).ToString).ToUpper & vbNewLine & "(" & ProjetEnCours.ToUpper & ")" &
                vbNewLine & "Liste des Dossiers d'Appel d'Offres (DAO) des marchés " & ReponseDialog & " à la date du " & Now.ToShortDateString

                EnteteR2 = MettreApost(rw(0).ToString).ToUpper & vbNewLine & "(" & ProjetEnCours.ToUpper & ")" &
                vbNewLine & "Liste des Demandes de Propositions (DP) des marchés " & ReponseDialog & " à la date du " & Now.ToShortDateString

                ReponseDialog = ""
            Next

            Dim journaux As New ReportDocument
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim Chemin As String = lineEtat & "\RSM\"
            Dim report As New ReportDocument
            Dim report2 As New ReportDocument

            ' DP
            Dim DatSet = New DataSet
            report2.Load(Chemin & "RapportDaoDp2.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = report2.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            report2.SetDataSource(DatSet)
            report2.SetParameterValue("EnteteDP", EnteteR2)
            If (imprEnCours = True) Then
                report2.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\R" & NumImprim.ToString & ".pdf")
                NumImprim = NumImprim + 1
                report2.PrintToPrinter(1, True, 0, 0)
            Else
                'ViewDp.ReportSource = report2
            End If

            ' DAO
            DatSet = New DataSet
            report.Load(Chemin & "RapportDaoDp.rpt")

            With crConnectionInfo
                .ServerName = ODBCNAME
                .DatabaseName = DB
                .UserID = USERNAME
                .Password = PWD
            End With

            CrTables = report2.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            report.SetDataSource(DatSet)
            report.SetParameterValue("EnteteDAO", EnteteR)
            If (imprEnCours = True) Then
                report.ExportToDisk([Shared].ExportFormatType.WordForWindows, NomDossier & "\R" & NumImprim.ToString & ".pdf")
                NumImprim = NumImprim + 1
                report.PrintToPrinter(1, True, 0, 0)
            Else
                ' ViewDAO.ReportSource = report
            End If


            'Else
            '    PageDAO.PageVisible = False
            '    ViewDAO.ReportSource = Nothing
            '    ViewDAO.Enabled = False

            '    ViewDp.ReportSource = Nothing
            '    ViewDp.Enabled = False

        End If


    End Sub

    Private Sub ChkGraph_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGraph.CheckedChanged
        ActivImprim()
        If (ChkGraph.Checked = True) Then
            PageGraph.PageVisible = True
            SplitContainerControl2.Collapsed = False
            XtraTabControl1.SelectedTabPage = PageGraph
            ChkGraph0.Enabled = True
            ChkGraph1.Enabled = True
            ChkGraph2.Enabled = True
            ChkGraph3.Enabled = True
            ChkGraph4.Enabled = True
            ChkGraph5.Enabled = True
            ChkGraph6.Enabled = True
            ChkGraph7.Enabled = True
            ChkGraph8.Enabled = True

            ChkGraph0.Checked = True
        Else
            PageGraph.PageVisible = False
            SplitContainerControl2.Collapsed = True
            ChkGraph0.Enabled = False
            ChkGraph1.Enabled = False
            ChkGraph2.Enabled = False
            ChkGraph3.Enabled = False
            ChkGraph4.Enabled = False
            ChkGraph5.Enabled = False
            ChkGraph6.Enabled = False
            ChkGraph7.Enabled = False
            ChkGraph8.Enabled = False
        End If

    End Sub

    Private Sub ChkGraph0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGraph0.CheckedChanged
        If (ChkGraph0.Checked = True) Then
            SouTabGraph.SelectedTabPage = TabGraph0
        End If
    End Sub

    Private Sub ChkGraph1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGraph1.CheckedChanged
        If (ChkGraph1.Checked = True) Then
            SouTabGraph.SelectedTabPage = TabGraph1
        End If
    End Sub

    Private Sub ChkGraph2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGraph2.CheckedChanged
        If (ChkGraph2.Checked = True) Then
            SouTabGraph.SelectedTabPage = TabGraph2
        End If
    End Sub

    Private Sub ChkGraph3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGraph3.CheckedChanged
        If (ChkGraph3.Checked = True) Then
            SouTabGraph.SelectedTabPage = TabGraph3
        End If
    End Sub

    Private Sub ChkGraph4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGraph4.CheckedChanged
        If (ChkGraph4.Checked = True) Then
            SouTabGraph.SelectedTabPage = TabGraph4
        End If
    End Sub

    Private Sub ChkGraph5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGraph5.CheckedChanged
        If (ChkGraph5.Checked = True) Then
            SouTabGraph.SelectedTabPage = TabGraph5
        End If
    End Sub

    Private Sub ChkGraph6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGraph6.CheckedChanged
        If (ChkGraph6.Checked = True) Then
            SouTabGraph.SelectedTabPage = TabGraph6
        End If
    End Sub

    Private Sub ChkGraph7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGraph7.CheckedChanged
        If (ChkGraph7.Checked = True) Then
            SouTabGraph.SelectedTabPage = TabGraph7
        End If
    End Sub

    Private Sub ChkGraph8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkGraph8.CheckedChanged
        If (ChkGraph8.Checked = True) Then
            SouTabGraph.SelectedTabPage = TabGraph8
        End If
    End Sub

    Private Sub BtOuvrirRapp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ArchivesRSM.ShowDialog()
    End Sub

    Private Sub RapportSurMarches_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

End Class