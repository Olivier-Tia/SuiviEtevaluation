Imports MySql.Data.MySqlClient

Public Class SaisieMethodes

    Dim PourAjout As Boolean = False
    Dim LigneAjout As Decimal = -1
    Dim LigneModif As Decimal = -1

    Private Sub SaisieMethodes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        BtAjouter.Enabled = True
        BtEnregistrer.Enabled = True

        RemplirListe()
    End Sub

    Private Sub init()
        If BtAjouter.Enabled = False Then
            BtAjouter.Enabled = True
        End If
        If PourAjout = True Then
            PourAjout = False
            LigneAjout = -1
            ListeMethode.Rows.Remove(ListeMethode.Rows(ListeMethode.Rows.Count - 1))
        ElseIf LigneModif <> -1 Then
            ListeMethode.Rows.Item(LigneModif).Cells(1).ReadOnly = True
            ListeMethode.Rows.Item(LigneModif).Cells(1).Style.BackColor = Color.Empty
            'Tim Dev ;;;; Obtenir dynamiquement le nombre de checkbox
            Dim colcount As Decimal = ListeMethode.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1
            For i As Integer = 2 To colcount
                ListeMethode.Rows.Item(LigneModif).Cells(i).ReadOnly = True
                ListeMethode.Rows.Item(LigneModif).Cells(i).Style.BackColor = Color.Empty
            Next
            Dim CurrentCell As Integer = Val(Mid(ListeMethode.CurrentCellAddress.ToString(), 4, 1))
            RemplirListe()
            ListeMethode.CurrentCell = ListeMethode.Rows(LigneModif).Cells(CurrentCell)
            LigneModif = -1
        End If
    End Sub

    Private Sub RemplirListe()
        ListeMethode.Columns.Clear()
        ListeMethode.Rows.Clear()
        query = "SELECT TypeMarche FROM t_typemarche"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        If dt.Rows.Count > 0 Then
            Dim col As New DataGridViewTextBoxColumn
            col.Name = "Code"
            col.HeaderText = "Code"
            ListeMethode.Columns.Add(col)

            col = New DataGridViewTextBoxColumn
            col.Name = "Lib"
            col.HeaderText = "Libellé"
            col.Width = 250
            ListeMethode.Columns.Add(col)

            Dim cpte As Decimal = 0
            For Each rwx As DataRow In dt.Rows
                cpte += 1
                Dim type As New DataGridViewCheckBoxColumn
                'type.Name = "Type" & cpte
                type.Name = rwx(0).ToString().Replace(" ", "_")
                type.HeaderText = rwx(0)
                ListeMethode.Columns.Add(type)
            Next


            query = "select distinct AbregeAO,LibelleAO from T_ProcAO where CodeProjet='" & ProjetEnCours & "' order by AbregeAO"
            dt = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                Dim TextLib As String = rw(1)
                RestaurerChaine(TextLib)
                Dim m As Decimal = ListeMethode.Rows.Add
                'Lig = m
                ListeMethode.Rows.Item(m).Cells(0).Value = rw(0)
                ListeMethode.Rows.Item(m).Cells(0).ReadOnly = True
                ListeMethode.Rows.Item(m).Cells(1).Value = TextLib
                ListeMethode.Rows.Item(m).Cells(1).ReadOnly = True

                For i As Integer = 2 To cpte + 1
                    ListeMethode.Rows.Item(m).Cells(i).ReadOnly = True
                Next
                query = "select TypeMarcheAO from T_ProcAO where AbregeAO='" & rw(0) & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    ListeMethode.Rows.Item(m).Cells(rw1(0).ToString().Replace(" ", "_")).Value = True
                Next

            Next
            If BtAjouter.Enabled = False Then BtAjouter.Enabled = True
        End If
    End Sub

    Private Sub BtAjouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjouter.Click

        PourAjout = True
        Dim n As Decimal = ListeMethode.Rows.Add
        LigneAjout = n
        For i As Integer = 0 To 4
            ListeMethode.Rows.Item(n).Cells(i).ReadOnly = False
            If (i = 0 Or i = 1) Then
                ListeMethode.Rows.Item(n).Cells(i).Value = ""
            Else
                ListeMethode.Rows.Item(n).Cells(i).Value = False
            End If
        Next
        ListeMethode.CurrentCell = ListeMethode.Rows(n).Cells(0)
        BtAjouter.Enabled = False
    End Sub

    Private Sub BtEnregistrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEnregistrer.Click
        'Tim---->Dev ::::::::: Validation des champs 
        Dim BienRenseigne As Boolean = True
        Dim BienRenseigne2 As Decimal = 0

        'Tim---->Dev ::::::::: Obtenir dynamiquement le nombre de checkbox
        Dim colcount As Decimal = ListeMethode.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1

        If (PourAjout And LigneAjout <> -1) Then
            For k As Integer = 0 To 1
                If ((ListeMethode.Rows(LigneAjout).Cells(k).Value).ToString = "") Then
                    BienRenseigne = False
                End If
            Next

            Dim CurrentCell As Integer = Val(Mid(ListeMethode.CurrentCellAddress.ToString(), 4, 1))
            If CurrentCell > 0 Then
                ListeMethode.CurrentCell = ListeMethode.Rows(LigneAjout).Cells(CurrentCell - 1)
            Else
                ListeMethode.CurrentCell = ListeMethode.Rows(LigneAjout).Cells(CurrentCell + 1)
            End If
            For p As Integer = 2 To colcount
                If (ListeMethode.Rows(LigneAjout).Cells(p).Value = False Or Len(ListeMethode.Rows(LigneAjout).Cells(p).Value) = 0) Then
                    BienRenseigne2 += 1
                End If
            Next
            If (BienRenseigne = False) Then
                MsgBox("Veuillez renseigner tous les champs svp.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If (BienRenseigne2 = (colcount - 1)) Then
                MsgBox("Veuillez selectionner au moins un type de marché.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            query = "select * from T_ProcAO where AbregeAO='" & ListeMethode.Rows.Item(LigneAjout).Cells(0).Value.ToString & "' and CodeProjet='" & ProjetEnCours & "'"
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                MsgBox("Cette méthode existe déjà!")
                Exit Sub
            Next

            'End If

            Dim Cmd As MySqlCommand

            'Tim Dev --- Obtenir dynamiquement le nombre de checkbox : c'est colcount
            For i As Integer = 2 To colcount
                If (ListeMethode.Rows.Item(LigneAjout).Cells(i).Value = True) Then
                    Dim TypeMarche As String = ListeMethode.Columns(ListeMethode.Rows.Item(LigneAjout).Cells(i).ColumnIndex).HeaderText
                    Dim DatSet = New DataSet

                    query = "select * from T_ProcAO"

                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Cmd = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_ProcAO")
                    Dim DatTable = DatSet.Tables("T_ProcAO")
                    Dim DatRow = DatSet.Tables("T_ProcAO").NewRow()

                    Dim LibAO As String = ListeMethode.Rows.Item(LigneAjout).Cells(1).Value.ToString
                    CorrectionChaine(LibAO)
                    DatRow("LibelleAO") = LibAO
                    DatRow("AbregeAO") = ListeMethode.Rows.Item(LigneAjout).Cells(0).Value.ToString
                    DatRow("TypeMarcheAO") = TypeMarche
                    DatRow("CodeProjet") = ProjetEnCours
                    DatRow("RechAuto") = "OUI"

                    DatSet.Tables("T_ProcAO").Rows.Add(DatRow)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Update(DatSet, "T_ProcAO")
                    DatSet.Clear()

                    Dim DernCode As Decimal

                    query = "select CodeProcAO from T_ProcAO where AbregeAO='" & ListeMethode.Rows.Item(LigneAjout).Cells(0).Value.ToString & "' and CodeProjet='" & ProjetEnCours & "' and TypeMarcheAO='" & TypeMarche & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt.Rows
                        DernCode = rw1(0)
                    Next


                    DatSet = New DataSet

                    query = "select * from T_NombreMarche"
                    Cmd = New MySqlCommand(query, sqlconn)
                    DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_NombreMarche")
                    DatTable = DatSet.Tables("T_NombreMarche")
                    DatRow = DatSet.Tables("T_NombreMarche").NewRow()

                    DatRow("CodeProjet") = ProjetEnCours
                    DatRow("TypeMarche") = TypeMarche
                    DatRow("CodeProcAO") = DernCode
                    DatRow("NbreMarche") = 0

                    DatSet.Tables("T_NombreMarche").Rows.Add(DatRow)
                    CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Update(DatSet, "T_NombreMarche")
                    DatSet.Clear()

                    BDQUIT(sqlconn)
                End If
            Next

            MsgBox("Méthode " & ListeMethode.Rows.Item(LigneAjout).Cells(0).Value.ToString & " enregistrée avec succès.", MsgBoxStyle.Information, "ClearProject")

            PourAjout = False
            LigneAjout = -1
            RemplirListe()

        ElseIf (LigneModif <> -1) Then 'Action de modification

            For k As Integer = 0 To 1
                If ((ListeMethode.Rows(LigneModif).Cells(k).Value).ToString = "") Then
                    BienRenseigne = False
                End If
            Next

            'Dim CurrentCell As Integer = Val(Mid(ListeMethode.CurrentCellAddress.ToString(), 4, 1))
            ListeMethode.CurrentCell = ListeMethode.Rows(LigneModif).Cells(0)

            For p As Integer = 2 To colcount
                If (ListeMethode.Rows(LigneModif).Cells(p).Value = False Or Len(ListeMethode.Rows(LigneModif).Cells(p).Value) = 0) Then
                    BienRenseigne2 += 1
                End If
            Next
            If (BienRenseigne = False) Then
                MsgBox("Veuillez renseigner tous les champs svp.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If (BienRenseigne2 = (colcount - 1)) Then
                MsgBox("Veuillez selectionner au moins un type de marché.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If


            ' Recherche du code de la methode *****
            Dim Abreg As String = ListeMethode.Rows.Item(LigneModif).Cells(0).Value.ToString

            'Tim Dev ;;;; Obtenir dynamiquement le nombre de checkbox : C'est colcount
            For i As Integer = 2 To colcount
                If (ListeMethode.Rows.Item(LigneModif).Cells(i).Value = True) Then
                    Dim TypeMarche As String = ListeMethode.Columns(ListeMethode.Rows.Item(LigneModif).Cells(i).ColumnIndex).HeaderText
                    Dim KodAO As Decimal = 0
                    query = "select CodeProcAO from T_ProcAO where AbregeAO='" & Abreg & "' and TypeMarcheAO='" & TypeMarche & "' and CodeProjet='" & ProjetEnCours & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt.Rows
                        query = "select * from T_DelaiEtape where CodeProcAO='" & CInt(rw1(0)) & "'"
                        Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                        For Each rw0 As DataRow In dt1.Rows
                            MsgBox("Cette méthode est en cours d'utilisation." & vbNewLine & "Vous ne pouvez pas la retirer du type de marché.", MsgBoxStyle.Exclamation)
                            Exit Sub
                        Next
                    Next
                End If
            Next

            Dim Cmd As MySqlCommand
            query = "DELETE FROM T_ProcAO WHERE AbregeAO='" & Abreg & "'"
            ExecuteNonQuery(query)
            For i As Integer = 2 To colcount
                If (ListeMethode.Rows.Item(LigneModif).Cells(i).Value = True) Then
                    Dim TypeMarche As String = ListeMethode.Columns(ListeMethode.Rows.Item(LigneModif).Cells(i).ColumnIndex).HeaderText
                    Dim DatSet = New DataSet

                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    query = "select * from T_ProcAO"
                    Cmd = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_ProcAO")
                    Dim DatTable = DatSet.Tables("T_ProcAO")
                    Dim DatRow = DatSet.Tables("T_ProcAO").NewRow()

                    Dim LibAO As String = ListeMethode.Rows.Item(LigneModif).Cells(1).Value.ToString
                    CorrectionChaine(LibAO)
                    DatRow("LibelleAO") = LibAO
                    DatRow("AbregeAO") = ListeMethode.Rows.Item(LigneModif).Cells(0).Value.ToString
                    DatRow("TypeMarcheAO") = TypeMarche
                    DatRow("CodeProjet") = ProjetEnCours
                    DatRow("RechAuto") = "OUI"

                    DatSet.Tables("T_ProcAO").Rows.Add(DatRow)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Update(DatSet, "T_ProcAO")
                    DatSet.Clear()


                    Dim DernCode As Decimal

                    query = "select CodeProcAO from T_ProcAO where AbregeAO='" & ListeMethode.Rows.Item(LigneModif).Cells(0).Value.ToString & "' and CodeProjet='" & ProjetEnCours & "' and TypeMarcheAO='" & TypeMarche & "'"
                    Dim dt As DataTable = ExcecuteSelectQuery(query)
                    For Each rw1 As DataRow In dt.Rows
                        DernCode = rw1(0)
                    Next


                    DatSet = New DataSet

                    query = "select * from T_NombreMarche"
                    Cmd = New MySqlCommand(query, sqlconn)
                    DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_NombreMarche")
                    DatTable = DatSet.Tables("T_NombreMarche")
                    DatRow = DatSet.Tables("T_NombreMarche").NewRow()

                    DatRow("CodeProjet") = ProjetEnCours
                    DatRow("TypeMarche") = TypeMarche
                    DatRow("CodeProcAO") = DernCode
                    DatRow("NbreMarche") = 0

                    DatSet.Tables("T_NombreMarche").Rows.Add(DatRow)
                    CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Update(DatSet, "T_NombreMarche")
                    DatSet.Clear()
                    BDQUIT(sqlconn)
                End If
            Next

            MsgBox("Méthode " & ListeMethode.Rows.Item(LigneModif).Cells(0).Value.ToString & " modifiée avec succès.", MsgBoxStyle.Information, "ClearProject")
            Dim CurrentCell As Integer = Val(Mid(ListeMethode.CurrentCellAddress.ToString(), 4, 1))
            RemplirListe()
            ListeMethode.CurrentCell = ListeMethode.Rows(LigneModif).Cells(CurrentCell)
            LigneModif = -1
        End If

    End Sub

    Private Sub ListeMethode_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ListeMethode.CellDoubleClick
        If (LigneModif = -1) Then
            Dim RepoAO As MsgBoxResult
            RepoAO = MsgBox("Voulez-vous modifier les marchés de la méthode " & ListeMethode.Rows.Item(ListeMethode.CurrentCell.RowIndex).Cells(0).Value.ToString, MsgBoxStyle.YesNo)
            If (RepoAO = MsgBoxResult.Yes) Then
                Dim IndLigNe As Integer = ListeMethode.CurrentCell.RowIndex
                LigneModif = IndLigne

                'Tim Dev ;;;; Obtenir dynamiquement le nombre de checkbox
                Dim colcount As Decimal = ListeMethode.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1
                ListeMethode.Rows.Item(IndLigne).Cells(1).ReadOnly = False
                ListeMethode.Rows.Item(IndLigne).Cells(1).Style.BackColor = Color.Yellow
                For i As Integer = 2 To colcount
                    ListeMethode.Rows.Item(IndLigne).Cells(i).ReadOnly = False
                    ListeMethode.Rows.Item(IndLigne).Cells(i).Style.BackColor = Color.Yellow
                Next
                BtAjouter.Enabled = False
            End If
        Else
            MsgBox("Veuillez d'abord enregistrer les modifications en cours!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub BtReload_Click(sender As Object, e As EventArgs) Handles BtReload.Click
        init()
    End Sub

    Private Sub BtSupprimer_Click(sender As Object, e As EventArgs) Handles BtSupprimer.Click
        ' Recherche du code de la methode *****
        Dim Abreg As String = ListeMethode.CurrentRow.Cells(0).Value.ToString

        'Tim---->Dev ::::::::: Obtenir dynamiquement le nombre de checkbox
        Dim colcount As Decimal = ListeMethode.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1
        For i As Integer = 2 To colcount
            If (ListeMethode.Rows.Item(ListeMethode.CurrentRow.Index).Cells(i).Value = True) Then
                Dim TypeMarche As String = ListeMethode.Columns(ListeMethode.CurrentRow.Cells(i).ColumnIndex).HeaderText
                Dim KodAO As Decimal = 0
                query = "select CodeProcAO from T_ProcAO where AbregeAO='" & Abreg & "' and TypeMarcheAO='" & TypeMarche & "' and CodeProjet='" & ProjetEnCours & "'"
                Dim dt As DataTable = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt.Rows
                    query = "select * from T_DelaiEtape where CodeProcAO='" & CInt(rw1(0)) & "'"
                    Dim dt1 As DataTable = ExcecuteSelectQuery(query)
                    For Each rw0 As DataRow In dt1.Rows
                        MsgBox("Cette méthode est en cours d'utilisation." & vbNewLine & "Vous ne pouvez pas la supprimer.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    Next
                Next
            End If
        Next

        If MsgBox("Voulez-vous supprimer " & Abreg & "?", MsgBoxStyle.YesNo, "ClearProject") = MsgBoxResult.Yes Then
            query = "DELETE FROM T_ProcAO WHERE AbregeAO='" & Abreg & "'"
            ExecuteNonQuery(query)
            Dim inde As Decimal = ListeMethode.CurrentRow.Index
            RemplirListe()
            If inde > 0 Then
                ListeMethode.CurrentCell = ListeMethode.Rows(inde - 1).Cells(0)
            End If
        End If
    End Sub

End Class