Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Data.DataSet
Imports System.IO
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math

Public Class SourceFinancement

    Public PourAjout, PourSupprim, PourModif As Boolean
    Public OkModifS As Boolean

    Private Sub SourceFinancement_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        BtRetour_Click(Me, e)
    End Sub

    Private Sub SourceFinancement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        OkModifS = False
        PourAjout = False
        PourModif = False
        PourSupprim = False
        ChargerSource()
        ChargerGridConv()
        DTDateSignature.Value = Now.ToShortDateString
        DTEntreeVigueur.Value = Now.ToShortDateString
        DTCloture.Value = Now.ToShortDateString
    End Sub

    Private Sub ChargerSource()
        query = "select InitialeBailleur from T_Bailleur where CodeProjet='" & ProjetEnCours & "'"
        Dim dt0 = ExcecuteSelectQuery(query)
        CmbSource.Items.Clear()
        For Each rw In dt0.rows
            CmbSource.Items.Add(rw(0))
        Next
    End Sub
    Private Sub BtAjouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter.Click
        CmbSource.Enabled = True
        BtRetour.Enabled = True
    End Sub
    Private Sub BtModifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtModifier.Click
        Try
            If TxtNumConvention.Text.Trim.Length = 0 Then
                Exit Sub
            End If
            Dim sqlconn As New MySqlConnection
            BDOPEN(sqlconn)
            Dim DatSet = New DataSet
            query = "select * from T_Convention where CodeConvention='" & TxtNumConvention.Text & "'"

            Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
            Dim DatAdapt = New MySqlDataAdapter(Cmd)
            Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
            DatAdapt.Fill(DatSet, "T_Convention")

            DatSet.Tables!T_Convention.Rows(0)!TypeConvention = CmbType.Text
            DatSet.Tables!T_Convention.Rows(0)!MontantConvention = TxtCFA.Text.Replace(" ", "")
            DatSet.Tables!T_Convention.Rows(0)!DateSignature = DTDateSignature.Value.ToShortDateString
            DatSet.Tables!T_Convention.Rows(0)!EntreeEnVigueur = DTEntreeVigueur.Value.ToShortDateString
            DatSet.Tables!T_Convention.Rows(0)!DateCloture = DTCloture.Value.ToShortDateString

            If (TxtPieceJointe.Text <> "") Then
                Dim NomFichier As String = line & "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&") & "\"
                If (Directory.Exists(NomFichier) = False) Then
                    Directory.CreateDirectory(NomFichier)
                    File.Copy(TxtChemin.Text, NomFichier, True)
                Else
                End If
                NomFichier = NomFichier & "\" & TxtPieceJointe.Text
                DatSet.Tables!T_Convention.Rows(0)!PieceConvention = EnleverApost(NomFichier)
                DatSet.Tables!T_Convention.Rows(0)!NomPiece = EnleverApost(TxtPieceJointe.Text)
            End If

            DatAdapt.Update(DatSet, "T_Convention")
            DatSet.Clear()
            ChargerGridConv()
            MsgBox("Modification terminée avec succès.", MsgBoxStyle.Information)
            BtRetour_Click(Me, e)
            BDQUIT(sqlconn)

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try

    End Sub
    Private Sub BtSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSupprimer.Click
        Try
            query = "DELETE from T_Convention where CodeConvention='" & TxtNumConvention.Text & "'"
            ExecuteNonQuery(query)

            Dim NomFichier As String = line & "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&")
            If (Directory.Exists(NomFichier) = True) Then
                Directory.Delete(NomFichier)
            End If

            ChargerGridConv()
            'query = "CALL `DeleteTampColConvention`();"
            'ExecuteNonQuery(query)
            'query = "CALL `CreateTampColConvention`();"
            'ExecuteNonQuery(query)
            BtRetour_Click(Me, e)
            MsgBox("Suppression terminée avec succès.", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub BtRetour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetour.Click
        BtAjouter.Enabled = True
        BtModifier.Enabled = True
        BtSupprimer.Enabled = True

        BtEnregistrer.Enabled = False
        BtRetour.Enabled = False

        PourAjout = False
        PourModif = False
        PourSupprim = False

        CmbSource.Text = ""
        CmbSource.Enabled = False
        TxtSource.Text = ""
        CmbType.Text = ""
        CmbType.Enabled = False
        TxtNumConvention.Text = ""
        TxtNumConvention.Enabled = False
        BtRecherche.Enabled = False
        TxtCFA.Text = ""
        TxtCFA.Enabled = False
        TxtEuro.Text = ""
        TxtEuro.Enabled = False
        TxtUS.Text = ""
        TxtUS.Enabled = False
        TxtLIVRE.Text = ""
        TxtLIVRE.Enabled = False
        DTDateSignature.Value = Now.ToShortDateString
        DTDateSignature.Enabled = False
        DTEntreeVigueur.Value = Now.ToShortDateString
        DTEntreeVigueur.Enabled = False
        DTCloture.Value = Now.ToShortDateString
        DTCloture.Enabled = False
        TxtPieceJointe.Text = ""

        GridConvention.Rows.Clear()
        ChargerSource()
        ChargerGridConv()

    End Sub
    Private Sub CmbSource_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbSource.SelectedIndexChanged
        Try
            If (CmbSource.Text <> "") Then
                query = "select NomBailleur,CodeBailleur from T_Bailleur where InitialeBailleur='" & CmbSource.Text & "' and CodeProjet='" + ProjetEnCours + "'"
                Dim dt0 = ExcecuteSelectQuery(query)
                For Each rw In dt0.Rows
                    TxtSource.Text = MettreApost(rw(0))
                    TxtCodeSource.Text = rw(1)
                Next
                CmbType.Enabled = True
                If Not TxtNumConvention.Enabled And CmbType.SelectedIndex >= 0 Then
                    TxtNumConvention.Enabled = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbType.SelectedIndexChanged
        If (CmbType.Text <> "") Then
            TxtNumConvention.Enabled = True
        End If
    End Sub
    Private Sub TxtNumConvention_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNumConvention.TextChanged
        If (TxtNumConvention.Text <> "") Then
            BtRecherche.Enabled = True
            TxtCFA.Enabled = True
            TxtEuro.Enabled = True
            TxtUS.Enabled = True
            TxtLIVRE.Enabled = True
        End If
    End Sub
    Private Sub TxtCFA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCFA.TextChanged
        TesterChampsMont()
        VerifSaisieMontant(TxtCFA)

        If (TxtCFA.Focused And TxtCFA.Text <> "") Then
            ConversionMont()
        ElseIf (TxtCFA.Focused And TxtCFA.Text = "") Then
            TxtEuro.Text = ""
            TxtUS.Text = ""
            TxtLIVRE.Text = ""
        End If
    End Sub
    Private Sub TesterChampsMont()
        If (TxtCFA.Text <> "" Or TxtEuro.Text <> "" Or TxtUS.Text <> "" Or TxtLIVRE.Text <> "") Then
            DTDateSignature.Enabled = True
        End If
    End Sub
    Private Sub ConversionMont()
        Dim TauxCfa As Double = 1
        Dim TauxEuro As Double = 1
        Dim TauxDoll As Double = 1
        Dim TauxLivre As Double = 1
        query = "select TauxDevise from T_Devise where AbregeDevise='FCFA'"
        Dim dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            TauxCfa = CDbl(rw(0))
        Next
        query = "select TauxDevise from T_Devise where AbregeDevise='€'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            TauxEuro = CDbl(rw(0))
        Next
        query = "select TauxDevise from T_Devise where AbregeDevise='US$'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            TauxDoll = CDbl(rw(0))
        Next
        query = "select TauxDevise from T_Devise where AbregeDevise='£'"
        dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            TauxLivre = CDbl(rw(0))
        Next

        If (TxtCFA.Focused) Then
            TxtEuro.Text = AfficherMonnaie(Math.Round((TauxCfa / TauxEuro) * CDbl(TxtCFA.Text.Replace(" ", "")), 2).ToString)
            TxtUS.Text = AfficherMonnaie(Math.Round((TauxCfa / TauxDoll) * CDbl(TxtCFA.Text.Replace(" ", "")), 2).ToString)
            TxtLIVRE.Text = AfficherMonnaie(Math.Round((TauxCfa / TauxLivre) * CDbl(TxtCFA.Text.Replace(" ", "")), 2).ToString)
        ElseIf (TxtEuro.Focused) Then
            TxtCFA.Text = AfficherMonnaie(Math.Round((TauxEuro / TauxCfa) * CDbl(TxtEuro.Text.Replace(" ", "")), 0).ToString)
            TxtUS.Text = AfficherMonnaie(Math.Round((TauxEuro / TauxDoll) * CDbl(TxtEuro.Text.Replace(" ", "")), 2).ToString)
            TxtLIVRE.Text = AfficherMonnaie(Math.Round((TauxEuro / TauxLivre) * CDbl(TxtEuro.Text.Replace(" ", "")), 2).ToString)
        ElseIf (TxtUS.Focused) Then
            TxtCFA.Text = AfficherMonnaie(Math.Round((TauxDoll / TauxCfa) * CDbl(TxtUS.Text.Replace(" ", "")), 0).ToString)
            TxtEuro.Text = AfficherMonnaie(Math.Round((TauxDoll / TauxEuro) * CDbl(TxtUS.Text.Replace(" ", "")), 2).ToString)
            TxtLIVRE.Text = AfficherMonnaie(Math.Round((TauxDoll / TauxLivre) * CDbl(TxtUS.Text.Replace(" ", "")), 2).ToString)
        ElseIf (TxtLIVRE.Focused) Then
            TxtCFA.Text = AfficherMonnaie(Math.Round((TauxLivre / TauxCfa) * CDbl(TxtLIVRE.Text.Replace(" ", "")), 0).ToString)
            TxtEuro.Text = AfficherMonnaie(Math.Round((TauxLivre / TauxEuro) * CDbl(TxtLIVRE.Text.Replace(" ", "")), 2).ToString)
            TxtUS.Text = AfficherMonnaie(Math.Round((TauxLivre / TauxDoll) * CDbl(TxtLIVRE.Text.Replace(" ", "")), 2).ToString)
        End If

    End Sub
    Private Sub TxtEuro_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtEuro.TextChanged
        TesterChampsMont()
        VerifSaisieMontant(TxtEuro)

        If (TxtEuro.Focused And TxtEuro.Text <> "") Then
            ConversionMont()
        ElseIf (TxtEuro.Focused And TxtEuro.Text = "") Then
            TxtCFA.Text = ""
            TxtUS.Text = ""
            TxtLIVRE.Text = ""
        End If
    End Sub
    Private Sub TxtUS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUS.TextChanged
        TesterChampsMont()
        VerifSaisieMontant(TxtUS)

        If (TxtUS.Focused And TxtUS.Text <> "") Then
            ConversionMont()
        ElseIf (TxtUS.Focused And TxtUS.Text = "") Then
            TxtCFA.Text = ""
            TxtEuro.Text = ""
            TxtLIVRE.Text = ""
        End If
    End Sub
    Private Sub TxtLIVRE_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtLIVRE.TextChanged
        TesterChampsMont()
        VerifSaisieMontant(TxtLIVRE)

        If (TxtLIVRE.Focused And TxtLIVRE.Text <> "") Then
            ConversionMont()
        ElseIf (TxtLIVRE.Focused And TxtLIVRE.Text = "") Then
            TxtCFA.Text = ""
            TxtEuro.Text = ""
            TxtUS.Text = ""
        End If
    End Sub
    Private Sub DTDateSignature_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTDateSignature.CloseUp
        DTEntreeVigueur.Enabled = True
    End Sub
    Private Sub DTEntreeVigueur_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTEntreeVigueur.CloseUp
        DTCloture.Enabled = True
    End Sub
    Private Sub DTCloture_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTCloture.CloseUp
        BtEnregistrer.Enabled = True
    End Sub
    Private Sub ChargerGridConv()
        Dim DateDeb, DateFin, DateJr As Date

        query = "select c.TypeConvention, c.CodeConvention, c.MontantConvention, c.DateSignature, c.EntreeEnVigueur, c.DateCloture, b.InitialeBailleur from T_Convention c, T_Bailleur b where c.CodeBailleur=b.CodeBailleur and b.CodeProjet='" & ProjetEnCours & "'"
        GridConvention.Rows.Clear()
        Dim dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            DateDeb = CDate(rw(4))
            DateFin = CDate(rw(5))
            DateJr = Now.ToShortDateString
            Dim EtatActu As String = "En exécution"

            Dim n As Decimal = GridConvention.Rows.Add()
            GridConvention.Rows.Item(n).Cells(0).Value = MettreApost(rw(6))
            GridConvention.Rows.Item(n).Cells(1).Value = MettreApost(rw(0))
            GridConvention.Rows.Item(n).Cells(2).Value = MettreApost(rw(1))
            GridConvention.Rows.Item(n).Cells(3).Value = AfficherMonnaie(rw(2).ToString)
            GridConvention.Rows.Item(n).Cells(4).Value = rw(3)
            GridConvention.Rows.Item(n).Cells(5).Value = rw(4)
            GridConvention.Rows.Item(n).Cells(6).Value = rw(5)

            If (DateTime.Compare(DateJr, DateDeb) < 0) Then
                EtatActu = "En attente"
            ElseIf (DateTime.Compare(DateFin, DateJr) < 0) Then
                EtatActu = "Délai passé"
            End If
            GridConvention.Rows.Item(n).Cells(7).Value = EtatActu
        Next
    End Sub

    Private Sub BtRecherche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRecherche.Click
        Dim dlg As New OpenFileDialog
        dlg.FileName = String.Empty
        If dlg.ShowDialog() = DialogResult.OK Then
            Dim fichier As String = dlg.FileName
            Dim NomComp As String() = fichier.Split("\"c)
            Dim Nbr As Decimal = 0
            For Each Elt In NomComp
                Nbr = Nbr + 1
            Next
            TxtPieceJointe.Text = NomComp(Nbr - 1)
            TxtChemin.Text = fichier

        End If

    End Sub

    Private Sub GridConvention_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridConvention.CellClick

        Dim IndexLg As Decimal = GridConvention.CurrentRow.Index
        CmbType.Text = GridConvention.Rows.Item(IndexLg).Cells(1).Value
        TxtNumConvention.Text = MettreApost(GridConvention.Rows.Item(IndexLg).Cells(2).Value)
        TxtCFA.Focus()
        TxtCFA.Text = GridConvention.Rows.Item(IndexLg).Cells(3).Value.ToString.Replace(" ", "")
        DTDateSignature.Value = CDate(GridConvention.Rows.Item(IndexLg).Cells(4).Value)
        DTEntreeVigueur.Value = CDate(GridConvention.Rows.Item(IndexLg).Cells(5).Value)
        DTCloture.Value = CDate(GridConvention.Rows.Item(IndexLg).Cells(6).Value)
        Dim EtatAct As String = GridConvention.Rows.Item(IndexLg).Cells(7).Value.ToString
        If (EtatAct = "En attente") Then
            OkModifS = True
        Else
            OkModifS = False
        End If

        Dim source As String = ""
        Dim textsource As String = ""

       query= "select C.PieceConvention, C.NomPiece, B.InitialeBailleur, B.NomBailleur from T_Convention C, T_Bailleur B where C.CodeConvention='" & TxtNumConvention.Text & "' and B.CodeBailleur=C.CodeBailleur"
        Dim dt0 = ExcecuteSelectQuery(query)
        For Each rw In dt0.Rows
            TxtPieceJointe.Text = MettreApost(rw(1))
            TxtChemin.Text = MettreApost(rw(0))
            source = MettreApost(rw(2))
            textsource = MettreApost(rw(3))
        Next

        CmbSource.Text = source.ToString
        TxtSource.Text = textsource.ToString
        DTEntreeVigueur.Enabled = True
        DTCloture.Enabled = True
        TxtNumConvention.Enabled = False
        CmbSource.Enabled = False
        BtEnregistrer.Enabled = False
    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        Try
            Dim erreur As String = ""

            If CmbSource.Text = "" Then
                erreur += "- Selectionner le Bailleur" + ControlChars.CrLf
            End If

            If CmbType.Text = "" Then
                erreur += "- Selectionner le type" + ControlChars.CrLf
            End If

            If TxtNumConvention.Text = "" Then
                erreur += "- Renseigner le Code de la convention" + ControlChars.CrLf
            End If

            If TxtCFA.Text = "" Then
                erreur += "- Renseigner le Montant de la convention" + ControlChars.CrLf
            End If

            If erreur = "" Then

                Dim Pays As String = ""
                query = "select PaysProjet from T_Projet where CodeProjet='" & ProjetEnCours & "'"
                Dim dt0 = ExcecuteSelectQuery(query)
                For Each rw In dt0.Rows
                    Pays = MettreApost(rw(0))
                Next

                query = "SELECT COUNT(*) FROM t_convention WHERE CodeConvention='" & TxtNumConvention.Text & "'"
                If Val(ExecuteScallar(query)) > 0 Then
                    SuccesMsg("La convention " & TxtNumConvention.Text & " existe déjà.")
                    Exit Sub
                End If

                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim DatSet = New DataSet
                query = "select * from T_Convention"
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_Convention")
                Dim DatTable = DatSet.Tables("T_Convention")
                Dim DatRow = DatSet.Tables("T_Convention").NewRow()

                DatRow("CodeConvention") = EnleverApost(TxtNumConvention.Text)
                DatRow("TypeConvention") = CmbType.Text
                DatRow("TitreConvention") = ""
                DatRow("Beneficiaire") = EnleverApost(Pays)
                DatRow("MontantConvention") = TxtCFA.Text.Replace(" ", "")
                DatRow("DateSignature") = DTDateSignature.Value.ToShortDateString
                DatRow("EntreeEnVigueur") = DTEntreeVigueur.Value.ToShortDateString
                DatRow("DateCloture") = DTCloture.Value.ToShortDateString

                If (TxtPieceJointe.Text <> "") Then
                    Dim NomFichier As String = line & "\Conventions\" & CmbSource.Text & "_" & CmbType.Text & TxtNumConvention.Text.Replace("/", "&")
                    If (Directory.Exists(NomFichier) = False) Then
                        Directory.CreateDirectory(NomFichier)
                    End If
                    NomFichier = NomFichier & "\" & TxtPieceJointe.Text
                    Try
                        If File.Exists(TxtChemin.Text) Then
                            File.Copy(TxtChemin.Text, NomFichier, True)
                        End If
                    Catch ex As Exception
                    End Try
                    DatRow("PieceConvention") = EnleverApost(NomFichier)
                    DatRow("NomPiece") = EnleverApost(TxtPieceJointe.Text)
                Else
                    DatRow("PieceConvention") = ""
                    DatRow("NomPiece") = ""
                End If

                DatRow("CodeBailleur") = TxtCodeSource.Text
                DatSet.Tables("T_Convention").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_Convention")
                DatSet.Clear()
                BDQUIT(sqlconn)

                'query = "CALL `CreateTampColConvention`();"
                'ExecuteNonQuery(query)
                ChargerGridConv()
                MsgBox("Enregistrement terminée avec succès.", MsgBoxStyle.Information)
                BtRetour_Click(Me, e)
            Else
                MsgBox("Veuillez : " + ControlChars.CrLf + erreur, MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try

    End Sub
    Private Sub SourceFinancement_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        FinChargement()
    End Sub

End Class