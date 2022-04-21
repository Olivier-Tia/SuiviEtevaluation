Imports MySql.Data.MySqlClient

Public Class Corbeil

    Dim dtType = New DataTable()
    Dim dtTrait = New DataTable()
    Dim DrX As DataRow

    Dim TypeEnCours As String = ""

    Dim Traitement(100) As String
    Dim Traiteur(100) As String
    Dim NbDelai(100) As Decimal
    Dim UniteDelai(100) As String
    Dim nbLigNe As Integer = 0

    Private Sub Corbeil_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        ChargerType()
        ChargerTraiteur()
        TypeEnCours = ""
        LgTraitCours = -1

    End Sub

    Private Sub ChargerType()

        dtType.Columns.Clear()

        dtType.Columns.Add("Code", Type.GetType("System.String"))
        dtType.Columns.Add("Ref", Type.GetType("System.String"))
        dtType.Columns.Add("Type", Type.GetType("System.String"))

        dtType.Rows.Clear()

        Dim cptTyp As Decimal = 0

        'Dim Reader As MySqlDataReader

        query = "select * from t_corbeille order by Libelle"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rwx As DataRow In dt.Rows
            cptTyp += 1
            Dim drS = dtType.NewRow()
            drS(0) = IIf(CDec(cptTyp / 2) <> CDec(cptTyp \ 2), "x", "").ToString
            drS(1) = rwx(0).ToString
            drS(2) = MettreApost(rwx(1).ToString)
            dtType.Rows.Add(drS)
        Next

        GridType.DataSource = dtType

        ViewType.Columns(0).Visible = False
        ViewType.Columns(1).Visible = False
        ViewType.Columns(2).Width = GridType.Width - 18

        ViewType.Appearance.Row.Font = New Font("Times New Roman", 12, FontStyle.Regular)

        ColorRowGrid(ViewType, "[Code]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)

    End Sub

    Private Sub ChargerTraiteur()


        'Dim Reader As MySqlDataReader

        query = "select * from t_typecourrier where etat_corb='0'"
        CmbTraiteur.Properties.Items.Clear()
        Dim dt = ExcecuteSelectQuery(query)
        For Each rwx As DataRow In dt.Rows
            CmbTraiteur.Properties.Items.Add(rwx(0).ToString & "  " & MettreApost(rwx(1).ToString))
        Next

    End Sub

    Private Sub PnlNewItiner_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PnlNewItiner.EnabledChanged
        PnlControl.Enabled = PnlNewItiner.Enabled
        BtRetour.Enabled = PnlNewItiner.Enabled
        PnlNewType.Enabled = Not (PnlNewItiner.Enabled)
    End Sub

    Private Sub TxtType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtType.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            If (TxtType.Text.Replace(" ", "") <> "") Then


                'Dim Reader As MySqlDataReader

                query = "select * from t_corbeille where libelle='" & EnleverApost(TxtType.Text) & "'"
                Dim dt = ExcecuteSelectQuery(query)
                If dt.rows.count > 0 Then

                    MsgBox("Ce type existe déjà!", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim DatSet = New DataSet
                query = "select * from t_corbeille"
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "t_corbeille")
                Dim DatTable = DatSet.Tables("t_corbeille")
                Dim DatRow = DatSet.Tables("t_corbeille").NewRow()

                DatRow("libelle") = EnleverApost(TxtType.Text)

                DatSet.Tables("t_corbeille").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "t_corbeille")
                DatSet.Clear()
                BDQUIT(sqlconn)

                ChargerType()
                TxtType.Text = ""
                TxtType.Focus()

            Else
                MsgBox("Chaine vide non autorisée!", MsgBoxStyle.Information)
            End If

        End If

    End Sub

    Private Sub GridType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridType.Click

        If (ViewType.RowCount > 0) Then

            DrX = ViewType.GetDataRow(ViewType.FocusedRowHandle)
            TxtType.Text = DrX(2).ToString

            InfosType(DrX(1).ToString)

        End If

    End Sub

    Private Sub InfosType(ByVal CodeType As String)

        nbLigne = 0

        'Dim Reader As MySqlDataReader

        query = "select libelle_type, CodeType, id_corb from t_liaisoncorbeille where id_corb='" & CodeType & "'"
        Dim dt = ExcecuteSelectQuery(query)
        For Each rwx As DataRow In dt.Rows

            Traiteur(nbLigne) = MettreApost(rwx(1).ToString + "  " + rwx(0).ToString)
            Dim traitX As String = ""
            For n As Decimal = 0 To CmbTraiteur.Properties.Items.Count - 1
                If (rwx(1).ToString <> "0") Then
                    If (InStr(CmbTraiteur.Properties.Items(n).ToString, rwx(1).ToString & "F|") <> 0) Then
                        traitX = CmbTraiteur.Properties.Items(n).ToString
                    End If
                End If
            Next
            nbLigne += 1

        Next

        ChargerTraitement()

    End Sub

    Private Sub GridType_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridType.DoubleClick

        TxtType.Text = ""
        If (ViewType.RowCount > 0) Then

            DrX = ViewType.GetDataRow(ViewType.FocusedRowHandle)
            TypeEnCours = DrX(1).ToString
            InfosType(TypeEnCours)

            ColorRowGrid(ViewType, "[Code]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewType, "[Ref]='" & TypeEnCours & "'", Color.Navy, "Times New Roman", 12, FontStyle.Bold, Color.White, True)

            PnlNewItiner.Enabled = True

        End If

    End Sub

    Private Sub BtRetour_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtRetour.Click

        TypeEnCours = ""
        CmbTraiteur.Text = ""
        PnlNewItiner.Enabled = False
        ColorRowGrid(ViewType, "[Code]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)
        nbLigne = 0
        LgTraitCours = -1
        ChargerTraitement()

    End Sub

    Private Sub ChargerTraitement()

        Dim cour() As String
        cour = CmbTraiteur.Text.Split("  ")

        dtTrait.Columns.Clear()

        dtTrait.Columns.Add("Code", Type.GetType("System.String"))
        dtTrait.Columns.Add("idcorbeille", Type.GetType("System.String"))
        dtTrait.Columns.Add("Code type", Type.GetType("System.String"))
        dtTrait.Columns.Add("Libellé Document", Type.GetType("System.String"))

        dtTrait.Rows.Clear()

        For k As Integer = 0 To nbLigne - 1

            Dim drS = dtTrait.NewRow()
            drS(0) = IIf(CDec(k / 2) <> CDec(k \ 2), "x", "").ToString
            drS(1) = TypeEnCours
            drS(2) = Mid(Traiteur(k), 1, 1)
            drS(3) = Mid(Traiteur(k), 3)
            dtTrait.Rows.Add(drS)

        Next

        GridTraitement.DataSource = dtTrait

        ViewTraitement.Columns(0).Visible = False
        ViewTraitement.Columns(1).Visible = False
        ViewTraitement.Columns(2).Visible = False
        ViewTraitement.Columns(3).Width = 700

        ViewTraitement.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
        ViewTraitement.Columns(3).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        ColorRowGrid(ViewTraitement, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)

        If (PnlNewItiner.Enabled = True And ViewTraitement.RowCount > 0) Then
            BtEnreg.Enabled = True
        Else
            BtEnreg.Enabled = False
        End If

    End Sub

    Dim LgTraitCours As Decimal = -1

    Private Sub GridTraitement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridTraitement.Click

        If (ViewTraitement.RowCount > 0) Then

            LgTraitCours = ViewTraitement.FocusedRowHandle + 1

            ColorRowGrid(ViewTraitement, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewTraitement, "[N°]='" & LgTraitCours.ToString & "'", Color.DarkGray, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

        End If

    End Sub

    Private Sub BtHaut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtHaut.Click

        If (LgTraitCours <> -1) Then

            If (LgTraitCours > 1) Then

                Dim Tamp As String = ""
                Tamp = Traitement(LgTraitCours - 2)
                Traitement(LgTraitCours - 2) = Traitement(LgTraitCours - 1)
                Traitement(LgTraitCours - 1) = Tamp

                Tamp = Traiteur(LgTraitCours - 2)
                Traiteur(LgTraitCours - 2) = Traiteur(LgTraitCours - 1)
                Traiteur(LgTraitCours - 1) = Tamp

                Tamp = NbDelai(LgTraitCours - 2).ToString
                NbDelai(LgTraitCours - 2) = NbDelai(LgTraitCours - 1)
                NbDelai(LgTraitCours - 1) = CInt(Tamp)

                Tamp = UniteDelai(LgTraitCours - 2)
                UniteDelai(LgTraitCours - 2) = UniteDelai(LgTraitCours - 1)
                UniteDelai(LgTraitCours - 1) = Tamp

                LgTraitCours = LgTraitCours - 1
                ChargerTraitement()
                ColorRowGridAnal(ViewTraitement, "[N°]='" & LgTraitCours.ToString & "'", Color.DarkGray, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

            End If

        End If

    End Sub

    Private Sub BtBas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtBas.Click

        If (LgTraitCours <> -1) Then

            If (LgTraitCours < ViewTraitement.RowCount) Then

                Dim Tamp As String = ""
                Tamp = Traitement(LgTraitCours - 1)
                Traitement(LgTraitCours - 1) = Traitement(LgTraitCours)
                Traitement(LgTraitCours) = Tamp

                Tamp = Traiteur(LgTraitCours - 1)
                Traiteur(LgTraitCours - 1) = Traiteur(LgTraitCours)
                Traiteur(LgTraitCours) = Tamp

                Tamp = NbDelai(LgTraitCours - 1).ToString
                NbDelai(LgTraitCours - 1) = NbDelai(LgTraitCours)
                NbDelai(LgTraitCours) = CInt(Tamp)

                Tamp = UniteDelai(LgTraitCours - 1)
                UniteDelai(LgTraitCours - 1) = UniteDelai(LgTraitCours)
                UniteDelai(LgTraitCours) = Tamp

                LgTraitCours = LgTraitCours + 1
                ChargerTraitement()
                ColorRowGridAnal(ViewTraitement, "[N°]='" & LgTraitCours.ToString & "'", Color.DarkGray, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

            End If

        End If

    End Sub

    Private Sub BtSupp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSupp.Click

        If (LgTraitCours <> -1) Then

            For k As Integer = LgTraitCours - 1 To nbLigne - 1

                Traiteur(k) = Traiteur(k + 1)

                If (ViewTraitement.RowCount > 0) Then

                    DrX = ViewTraitement.GetDataRow(ViewTraitement.FocusedRowHandle)
                    query = "delete from t_liaisoncorbeille where id_corb ='" + DrX(1).ToString + "'"
                    ExecuteNonQuery(query)

                End If

            Next

            nbLigne = nbLigne - 1
            LgTraitCours = -1
            ChargerTraitement()

        End If

    End Sub

    Private Sub BtEnreg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnreg.Click

        If (ViewTraitement.RowCount > 0 And TypeEnCours <> "" And nbLigne > 0) Then

            For k As Integer = 0 To nbLigne - 1

                Dim DatSet = New DataSet
                query = "select * from t_liaisoncorbeille"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "t_liaisoncorbeille")
                Dim DatTable = DatSet.Tables("t_liaisoncorbeille")
                Dim DatRow = DatSet.Tables("t_liaisoncorbeille").NewRow()

                DatRow("id_corb") = TypeEnCours
                DatRow("CodeType") = Mid(Traiteur(k), 1, 1)
                DatRow("libelle_type") = EnleverApost(Mid(Traiteur(k), 3))
                DatRow("CodeProjet") = ProjetEnCours

                DatSet.Tables("t_liaisoncorbeille").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "t_liaisoncorbeille")
                DatSet.Clear()
                BDQUIT(sqlconn)

                query = "update T_TypeCourrier set etat_corb=1 where CodeType='" + Mid(Traiteur(k), 1, 1) + "'"
                ExecuteNonQuery(query)

                ChargerTraiteur()

            Next

            MsgBox("Traitement enregistré avec succès!", MsgBoxStyle.Information)
            BtRetour_Click(Me, e)

        End If

    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        If (ViewType.RowCount > 0) Then
            DrX = ViewType.GetDataRow(ViewType.FocusedRowHandle)

            Dim result = MessageBox.Show("Voulez-vous Supprimer le Type de Courrier", "ClearProject", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then

            ElseIf result = DialogResult.Yes Then

                Dim codetype As String = ""

                query = "select libelle_type from t_liaisoncorbeille where id='" & DrX(1).ToString & "'"
                Dim dt = ExcecuteSelectQuery(query)
                If dt.Rows.Count = 0 Then

                    query = "delete from T_Corbeille where id='" + DrX(1).ToString + "'"
                    ExecuteNonQuery(query)

                Else
                    MsgBox("ITENERAIRE EXISTE DEJA !!!")
                End If
               
                ChargerType()

            End If
        End If
    End Sub

    Private Sub BtAjoutTraitement_Click(sender As System.Object, e As System.EventArgs) Handles BtAjoutTraitement.Click
        If (CmbTraiteur.Text <> "") Then

            Traiteur(nbLigne) = CmbTraiteur.Text
            CmbTraiteur.Text = ""
            nbLigne += 1
            LgTraitCours = -1
            ChargerTraitement()

        End If
    End Sub
End Class