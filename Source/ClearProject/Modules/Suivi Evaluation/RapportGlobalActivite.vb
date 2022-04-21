Imports MySql.Data.MySqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class RapportGlobalActivite
    Dim dtAutre = New DataTable()
    Dim dtPieces = New DataTable()
    Dim DrX As DataRow


    Private Sub RapportGlobalActivite_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        CmbActivite.Properties.Items.Clear()
        query = "select LibelleCourt,LibellePartition from T_Partition where CodeClassePartition='5' and CodeProjet='" & ProjetEnCours & "'  order by length(LibelleCourt) asc"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            CmbActivite.Properties.Items.Add(rw("LibelleCourt").ToString & "_" & MettreApost(rw("LibellePartition").ToString))
        Next

        query = "select numrapport from t_rapportactivites where EtatRapport=0 order by numrapport asc"
        Dim dt5 As DataTable = ExcecuteSelectQuery(query)
        For Each rw5 As DataRow In dt5.Rows
            CmbActivite.Properties.Items.Add(rw5("numrapport").ToString)
        Next

        LesAutresRapport()

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

    Private Sub CmbActivite_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbActivite.SelectedIndexChanged
        Try
            If ListBoxControl1.FindString(CmbActivite.Text) <> -1 Then
                SuccesMsg("L'activité existe déjà")
            Else
                ListBoxControl1.Items.Add(CmbActivite.Text)
            End If
        Catch ex As Exception
            Failmsg("Erreur : Information non disponible : " & ex.ToString())
        End Try
    End Sub

    Private Sub EnregistretePiece(ByVal NomDossiers As String)
        If ViewPieceJ.RowCount > 0 Then
            For i = 0 To ViewPieceJ.RowCount - 1
                Dim fichier = ViewPieceJ.GetRowCellValue(i, "Chemin").ToString
                Dim NomComp As String() = fichier.Split("\"c)
                Dim NomCourt As String = NomComp(NomComp.Length - 1)

                Dim NomDossier = line & "\Rapports\" & NomDossiers
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
                    DatRow("RefPiece") = "\Rapports\" & NomDossiers & "\" & NomComp(NomComp.Length - 1)
                    DatRow("NumRapport") = NomDossiers
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

    Private Sub BtEnrgRapport_Click(sender As System.Object, e As System.EventArgs) Handles BtEnrgRapport.Click

        Dim Erreur As String = ""
        If CmbActivite.SelectedIndex = -1 Then
            Erreur += "Activite" + ControlChars.CrLf
        End If
        If ListBoxControl1.Text = "" Then
            Erreur += "Liste activite" + ControlChars.CrLf
        End If
        If DtDebPeriode.Text = "" Then
            Erreur += "Date debut" + ControlChars.CrLf
        End If
        If DtFinPeriode.Text = "" Then
            Erreur += "Date fin" + ControlChars.CrLf
        End If

        If Erreur <> "" Then
            FailMsg("Veuillez remplir tous les champs")
            Exit Sub
        End If

        Dim activites = ""
        For i = 0 To ListBoxControl1.Items.Count - 1
            Dim activite() = ListBoxControl1.Items(i).ToString.Split("_")
            activites = activites + activite(0).ToString + ";"
        Next

        activites = activites.Replace(";", "_")
        activites = Mid(activites, 1, Len(activites) - 1)

        query = "insert into RapportGlobal values (NULL,'" & activites.ToString & "','" & EnleverApost(TxtDetail.Text) & "','" & CodeUtilisateur.ToString & "','" & DtDebPeriode.Text & "','" & DtFinPeriode.Text & "')"
        ExecuteNonQuery(query)

        Dim numrapport() = ListBoxControl1.Text.Split(";")
        FailMsg(numrapport(0))
        For i = 0 To numrapport.Length - 1
            SuccesMsg(numrapport(i))
            query = "UPDATE T_RapportActivites SET EtatRapport='1' where NumRapport='" & numrapport(i) & "' "
            ExecuteNonQuery(query)
        Next

        EnregistretePiece(activites.ToString)

        'Actualisation
        query = "select numrapport from t_rapportactivites where EtatRapport=0"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            CmbActivite.Properties.Items.Add(rw("numrapport").ToString)
        Next

        SuccesMsg("Rapport enregistré avec succès !")
        Initialiser()
        LesAutresRapport()
    End Sub

    Private Sub LesAutresRapport()

        dtAutre.Columns.Clear()
        dtAutre.Columns.Add("Code", Type.GetType("System.String"))
        dtAutre.Columns.Add("IdRapGbl", Type.GetType("System.String"))
        dtAutre.Columns.Add("Activités comprises", Type.GetType("System.String"))
        dtAutre.Columns.Add("Récapitulatifs", Type.GetType("System.String"))
        dtAutre.Columns.Add("Date début", Type.GetType("System.String"))
        dtAutre.Columns.Add("Date fin", Type.GetType("System.String"))

        query = "select * from RapportGlobal where CodeOperateur='" & CodeUtilisateur.ToString & "'"
        dtAutre.Rows.Clear()
        Dim nbres As Integer = 0
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt.Rows
            nbres += 1

            Dim drS = dtAutre.NewRow()
            drS("Code") = IIf((nbres Mod 2 = 0), "x", "").ToString
            drS("IdRapGbl") = rw("id").ToString
            drS("Activités comprises") = MettreApost(rw("LibelleCourt").ToString)
            drS("Récapitulatifs") = MettreApost(rw("commentaire").ToString)
            drS("Date début") = MettreApost(rw("datedebut").ToString)
            drS("Date fin") = MettreApost(rw("datefin").ToString)

            dtAutre.Rows.Add(drS)
        Next

        GridRapPrec.DataSource = dtAutre

        ViewRapPrec.Columns("IdRapGbl").Visible = False
        ViewRapPrec.Columns("Code").Visible = False
        ViewRapPrec.OptionsView.ColumnAutoWidth = True
        ViewRapPrec.OptionsBehavior.AutoExpandAllGroups = True
        ViewRapPrec.VertScrollVisibility = True
        ViewRapPrec.HorzScrollVisibility = True
        ViewRapPrec.BestFitColumns()

        ViewRapPrec.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
        ColorRowGrid(ViewRapPrec, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
    End Sub
    Private Sub Initialiser()
        ListBoxControl1.Items.Clear()
        DtDebPeriode.Text = ""
        DtFinPeriode.Text = ""
        CmbActivite.Text = ""
        TxtDetail.Text = ""
        GridPieceJ.DataSource = Nothing
        GridPieceJ.Refresh()
    End Sub
    Private Sub BtAnnulerRapp_Click(sender As System.Object, e As System.EventArgs) Handles BtAnnulerRapp.Click
        Initialiser()
        ListBoxControl1.Items.Clear()
    End Sub

    Private Sub BtSelectPJ_Click(sender As Object, e As EventArgs) Handles BtSelectPJ.Click
        Dim dlg As New OpenFileDialog
        'type du document a ouvrir'
        dlg.Filter = "Documents |*.pdf;*.png;*.jpeg;*.gif;*.jpg:*.docx;*.txt;*.xlsx;*.doc;*.rtf;*.xls;*.pptx;*.pptm;*.ppt;*.xps;*.pot;*.odp;*.docm;*.dot;*.dotx;*.dotm;*.xps;"
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
    Private Sub ChargerPJ(ByVal NumeroRapport As String)
        query = "select ID_Pieces, RefPiece, NomPiece from T_PieceJointe where NumRapport='" & NumeroRapport.ToString & "'"
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

    Private Sub OuvrirToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OuvrirToolStripMenuItem1.Click
        If (ViewPieceJ.RowCount > 0) Then
            DrX = ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle)
            Process.Start(DrX("Chemin").ToString)
        End If
    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem.Click

        If ViewPieceJ.RowCount > 0 Then

            DrX = ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle)
            Dim CheminFile As String = DrX("Chemin").ToString
            Dim NomDossier As String = CodeRaportCache.Text
            NomDossier = line & "\Rapports\" & NomDossier

            query = "select count(*) from T_PieceJointe where ID_Pieces='" & DrX("ID_Piece").ToString & "'"
            Dim nbres As Integer = Val(ExecuteScallar(query))

            If nbres > 0 Then
                If (Directory.Exists(NomDossier)) Then
                    query = "DELETE from T_PieceJointe where ID_Pieces='" & DrX("ID_Piece").ToString & "' and  NumRapport='" & CodeRaportCache.Text & "'"
                    ExecuteNonQuery(query)
                    'ChargerPJ(BtNumRap.Text)
                End If
            End If
            ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle).Delete()
        End If
    End Sub

    Private Sub ImprimerToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ImprimerToolStripMenuItem1.Click

        If (ViewPieceJ.RowCount > 0) Then

            DrX = ViewPieceJ.GetDataRow(ViewPieceJ.FocusedRowHandle)
            Dim monProcess As New Process()
            monProcess.StartInfo.FileName = DrX("Chemin").ToString
            monProcess.StartInfo.Verb = "Print"
            monProcess.StartInfo.CreateNoWindow = True
            Try
                If File.Exists(DrX("Chemin").ToString) Then
                    monProcess.Start()
                Else
                    FailMsg("Le fichier spécifié n'existe pas.")
                End If
            Catch ex As Exception
                monProcess = New Process()
                monProcess.StartInfo.FileName = DrX("Chemin").ToString
                If File.Exists(DrX("Chemin").ToString) Then
                    monProcess.Start()
                Else
                    FailMsg("Le fichier spécifié n'existe pas.")
                End If
            End Try
        End If
    End Sub

    Private Sub GridRapPrec_DoubleClick(sender As Object, e As EventArgs) Handles GridRapPrec.DoubleClick
        If ViewRapPrec.RowCount > 0 Then
            DrX = ViewRapPrec.GetDataRow(ViewRapPrec.FocusedRowHandle)
            CodeRaportCache.Text = DrX("Activités comprises").ToString
            ChargerPJ(CodeRaportCache.Text)
        End If
    End Sub
End Class