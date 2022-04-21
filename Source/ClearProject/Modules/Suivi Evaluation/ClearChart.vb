'Imports AxMicrosoft
Imports Microsoft
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Image
Imports System.Windows.Forms.DataVisualization.Charting
Public Class ClearChart


    Private Sub ClearChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        AfficherChart()

    End Sub
    Public Sub AfficherChart()

        ' Créer ChartArea (zone graphique)
        Dim ChartArea1 As New ChartArea()
        Dim ChartArea2 As New ChartArea()

        ' Ajouter le  Chart Area à la Collection ChartAreas du  Chart
        Chart2.ChartAreas.Add(ChartArea1)
        Chart2.ChartAreas.Add(ChartArea2)

        ' Créer deux  data series (qui contiendront les DataPoint)
        Dim series1 As New Series()
        Dim series2 As New Series()
        Dim series3 As New Series()
        Dim series4 As New Series()

        series1.Name = "SeriePrevuPer"
        series2.Name = "SerieEffectPer"

        series3.Name = "SeriePrevuCum"
        series4.Name = "SerieEffectCum"

        ' Ajouter des points à la collections Points de la première series
        query = "select LibCourtActivite,PrevuPeriode,EffectifPeriode,PrevuCumule,EffectifCumule from T_EtatFondsParActivite where LENGTH(LibCourtActivite)='3' order by LibCourtActivite"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            If (CDbl(rw(1)) <> 0 Or CDbl(rw(2)) <> 0 Or CDbl(rw(3)) <> 0 Or CDbl(rw(4)) <> 0) Then
                Dim LibAct As String = rw(0)
                Dim PtPrevu As Double = CDbl(rw(1))
                Dim PtEffectif As Double = CDbl(rw(2))
                series1.Points.AddXY(LibAct, PtPrevu)
                series2.Points.Add(PtEffectif)

                Dim PtPrevuCum As Double = CDbl(rw(3))
                Dim PtEffectCum As Double = CDbl(rw(4))
                series3.Points.AddXY(LibAct, PtPrevuCum)
                series4.Points.Add(PtEffectCum)
            End If

        Next



        'On indique d'afficher ces Series sur le ChartArea1
        series1.ChartArea = "ChartArea1"
        series2.ChartArea = "ChartArea1"
        series3.ChartArea = "ChartArea2"
        series4.ChartArea = "ChartArea2"

        ' Ajouter les series à la collection Series du chart
        Chart2.Series.Add(series1)
        Chart2.Series.Add(series2)
        Chart2.Series.Add(series3)
        Chart2.Series.Add(series4)

        ' Couleur des series
        Chart2.Series("SeriePrevuPer").Color = Color.Blue 'ParamChart.CoulSerie1.SelectedColor
        Chart2.Series("SerieEffectPer").Color = Color.Green 'ParamChart.CoulSerie2.SelectedColor
        Chart2.Series("SeriePrevuCum").Color = Color.LightGray 'ParamChart.CoulSerie3.SelectedColor
        Chart2.Series("SerieEffectCum").Color = Color.Yellow 'ParamChart.CoulSerie4.SelectedColor

        Chart2.Series("SeriePrevuPer").ChartType = SeriesChartType.Column
        Chart2.Series("SerieEffectPer").ChartType = SeriesChartType.Column
        Chart2.Series("SeriePrevuCum").ChartType = SeriesChartType.Column
        Chart2.Series("SerieEffectCum").ChartType = SeriesChartType.Column
        If (RdStyleCyl.Checked = True) Then
            Chart2.Series("SeriePrevuPer").CustomProperties = "DrawingStyle=cylinder"
            Chart2.Series("SerieEffectPer").CustomProperties = "DrawingStyle=cylinder"
            Chart2.Series("SeriePrevuCum").CustomProperties = "DrawingStyle=cylinder"
            Chart2.Series("SerieEffectCum").CustomProperties = "DrawingStyle=cylinder"
        End If

        If (ChkAxeX.Checked = False) Then
            Chart2.ChartAreas("ChartArea1").AxisX.Enabled = AxisEnabled.False
            Chart2.ChartAreas("ChartArea2").AxisX.Enabled = AxisEnabled.False
        End If

        If (ChkAxeY.Checked = False) Then
            Chart2.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.False
            Chart2.ChartAreas("ChartArea2").AxisY.Enabled = AxisEnabled.False
        End If

        If (RdLibOui.Checked = True) Then
            For i As Integer = 0 To 3
                For Each point In Chart2.Series(i).Points
                    Dim LeTexte As String() = point.ToString.Split("="c)
                    point.LabelForeColor = Color.Red 'ParamChart.CoulTexte.SelectedColor
                    Dim Code As String = point.AxisLabel.ToString
                    point.Label = AfficherMonnaie(Mid(LeTexte(2), 1, (LeTexte(2).Length) - 1))
                Next
            Next
        End If


        ' Affichage en 3D **********************************************************************************
        If (RdVue3D.Checked = True) Then
            Chart2.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
            Chart2.ChartAreas("ChartArea2").Area3DStyle.Enable3D = True

            ' Couleur transparente **************************

            Chart2.Series("SeriePrevuPer").Color = Color.FromArgb(255 - TbTransparence.Value, Color.Blue)
            Chart2.Series("SerieEffectPer").Color = Color.FromArgb(255 - TbTransparence.Value, Color.Green)
            Chart2.Series("SeriePrevuCum").Color = Color.FromArgb(255 - TbTransparence.Value, Color.LightGray)
            Chart2.Series("SerieEffectCum").Color = Color.FromArgb(255 - TbTransparence.Value, Color.Yellow)

            ' Rotation **************************************
            Chart2.ChartAreas("ChartArea1").Area3DStyle.Rotation = TbRotation.Value
            Chart2.ChartAreas("ChartArea2").Area3DStyle.Rotation = TbRotation.Value

            ' Inclinaison ***********************************
            Chart2.ChartAreas("ChartArea1").Area3DStyle.Inclination = TbInclinaison.Value
            Chart2.ChartAreas("ChartArea2").Area3DStyle.Inclination = TbInclinaison.Value

            ' Perspective ************************************
            Chart2.ChartAreas("ChartArea1").Area3DStyle.Perspective = TbPerspective.Value
            Chart2.ChartAreas("ChartArea2").Area3DStyle.Perspective = TbPerspective.Value

            ' Profondeur colonne *****************************
            Chart2.ChartAreas("ChartArea1").Area3DStyle.PointDepth = TbProfColo.Value
            Chart2.ChartAreas("ChartArea2").Area3DStyle.PointDepth = TbProfColo.Value

            ' Profondeur interne *****************************
            Chart2.ChartAreas("ChartArea1").Area3DStyle.PointGapDepth = TbProfInterne.Value
            Chart2.ChartAreas("ChartArea2").Area3DStyle.PointGapDepth = TbProfInterne.Value

            ' Epaisseur mur **********************************
            Chart2.ChartAreas("ChartArea1").Area3DStyle.WallWidth = TbEpaisseMur.Value
            Chart2.ChartAreas("ChartArea2").Area3DStyle.WallWidth = TbEpaisseMur.Value

            ' Eclairage **************************************
            If (RdEclairageNon.Checked = True) Then
                Chart2.ChartAreas("ChartArea1").Area3DStyle.LightStyle = LightStyle.None
                Chart2.ChartAreas("ChartArea2").Area3DStyle.LightStyle = LightStyle.None
            ElseIf (RdEclairage1.Checked = True) Then
                Chart2.ChartAreas("ChartArea1").Area3DStyle.LightStyle = LightStyle.Realistic
                Chart2.ChartAreas("ChartArea2").Area3DStyle.LightStyle = LightStyle.Realistic
            Else
                Chart2.ChartAreas("ChartArea1").Area3DStyle.LightStyle = LightStyle.Simplistic
                Chart2.ChartAreas("ChartArea2").Area3DStyle.LightStyle = LightStyle.Simplistic
            End If



        End If

        ' Arriere plan du ChartArea
        Chart2.ChartAreas(0).BackImage = line & "\ClearpNew4.png"
        Chart2.ChartAreas(1).BackImage = line & "\ClearpNew4.png"
        Chart2.BackImage = line & "\ClearpNew5.png"
        Chart2.BackImageWrapMode = ChartImageWrapMode.Unscaled
        'Mise à l'echelle pour que l'image remplisse le ChartArea
        'Chart2.ChartAreas(0).BackImageWrapMode = ChartImageWrapMode.Scaled
        ''Chart2.ChartAreas(1).BackImageWrapMode = ChartImageWrapMode.Scaled

        ' Titres des graphiques
        'Chart2.Titles.Add("Titre1")
        'Chart2.Titles.Add("Titre2")


        Chart2.Titles("Titre1").Text = "GRAPHIQUE DE L'EMPLOI DES FONDS PAR ACTIVITE" & vbNewLine & "POUR LA PERIODE DU " & RapportSuiviFinancierClear.DTDebPeriode.Text & " AU " & RapportSuiviFinancierClear.DTFinPeriode.Text & "." & vbNewLine
        Chart2.Titles("Titre1").Position.Auto = True
        Chart2.Titles("Titre1").DockedToChartArea = ChartArea1.Name
        Chart2.Titles("Titre1").Docking = Docking.Top
        Chart2.Titles("Titre1").IsDockedInsideChartArea = False
        Chart2.Titles("Titre1").Font = New Font("Arial", 8, FontStyle.Bold)
        'Chart2.Titles("Titre1").BorderColor = Color.Black


        Chart2.Titles("Titre2").Text = "GRAPHIQUE DU CUMUL DE L'EMPLOI DES FONDS PAR ACTIVITE" & vbNewLine & "DU DEBUT DU PROJET JUSQU'A LA DATE DU " & RapportSuiviFinancierClear.DTFinPeriode.Text & "." & vbNewLine
        Chart2.Titles("Titre2").Position.Auto = True
        Chart2.Titles("Titre2").DockedToChartArea = ChartArea2.Name
        Chart2.Titles("Titre2").Docking = Docking.Top
        Chart2.Titles("Titre2").IsDockedInsideChartArea = False
        Chart2.Titles("Titre2").Font = New Font("Arial", 8, FontStyle.Bold)
        'Chart2.Titles("Titre2").BorderColor = Color.Black

        ' Legende
        If (RdLegOui.Checked = True) Then
            Dim legend1 As New Legend
            legend1.Name = "Leg1"
            legend1.Title = "Légende"

            'On l'ajoute à la collection Legends n du Chart
            Chart2.Legends.Add(legend1)
            Chart2.Series(0).IsVisibleInLegend = False
            Chart2.Series(1).IsVisibleInLegend = False
            Chart2.Series(2).IsVisibleInLegend = False
            Chart2.Series(3).IsVisibleInLegend = False

            Chart2.Legends("Leg1").CustomItems.Clear()

            Chart2.Legends("Leg1").CustomItems.Add(New LegendItem("Prévisions" & vbNewLine & "période" & vbNewLine, Color.Blue, ""))
            Chart2.Legends("Leg1").CustomItems.Add(New LegendItem("Dépenses" & vbNewLine & "période" & vbNewLine, Color.Green, ""))
            Chart2.Legends("Leg1").CustomItems.Add(New LegendItem("Prévisions" & vbNewLine & "cumulatives" & vbNewLine, Color.LightGray, ""))
            Chart2.Legends("Leg1").CustomItems.Add(New LegendItem("Dépenses" & vbNewLine & "cumulatives" & vbNewLine, Color.Yellow, ""))

            legend1.LegendStyle = LegendStyle.Column

            Chart2.Legends("Leg1").BorderColor = Color.Black
            Chart2.Legends("Leg1").BorderWidth = 1
            Chart2.Legends("Leg1").BorderDashStyle = ChartDashStyle.Solid
            Chart2.Legends("Leg1").ShadowOffset = 2
        End If




        ' Positionner le controle Chart
        Chart2.Location = New System.Drawing.Point(0, 0)

        ' Dimensionner le Chart
        Chart2.Size = New System.Drawing.Size(360, 260)

        Chart2.Dock = DockStyle.Fill

        ' Ajouter le chart à la form
        Me.Controls.Add(Chart2)
    End Sub

    Private Sub Chart2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Chart2.MouseMove

        ' Appel HitTest qui retourne l'objet sous le curseur
        Dim result As HitTestResult = Chart2.HitTest(e.X, e.Y)
        Dim point As DataPoint
        For i As Integer = 0 To 3
            For Each point In Chart2.Series(i).Points
                point.BackSecondaryColor = Color.Black
                point.BackHatchStyle = ChartHatchStyle.None
                point.BorderWidth = 1
            Next
        Next


        ' Si la souris est sur un data point
        If result.ChartElementType = ChartElementType.DataPoint Then
            Dim LaSerie As String = result.Series.Name
            Dim point1 As DataPoint = Chart2.Series(LaSerie).Points(result.PointIndex)

            ' Changer l'apparence du data point survolé
            point1.BackSecondaryColor = Color.White
            point1.BackHatchStyle = ChartHatchStyle.Percent10
            point1.BorderWidth = 3
            Dim LeTexte As String() = point1.ToString.Split("="c)
            point1.ToolTip = AfficherMonnaie(Mid(LeTexte(2), 1, (LeTexte(2).Length) - 1))

        End If

    End Sub

    Private Sub ClearChart_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

End Class