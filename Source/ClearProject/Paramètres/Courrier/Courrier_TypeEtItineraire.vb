Imports MySql.Data.MySqlClient

Public Class Courrier_TypeEtItineraire

    Dim dtType = New DataTable()
    Dim dtTrait = New DataTable()
    Dim DrX As DataRow
    Dim DrX1 As DataRow
    Dim TypeEnCours As String = ""

    Private Sub Courrier_TypeEtItineraire_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        BtRetour_Click(Me, e)
    End Sub

    Private Sub Courrier_TypeEtItineraire_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
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
        query = "select CodeType, LibelleType from T_TypeCourrier order by LibelleType"
        dt = ExcecuteSelectQuery(query)
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

        'rechercher les informations de l'opérateur connecté
        CmbTraiteur.Properties.Items.Clear()
        query = "select CodeOperateur, NomOperateur, PrenOperateur from T_Operateur where EMP_ID<>'' and CodeProjet='" & ProjetEnCours & "' order by NomOperateur"
        Dim dt As DataTable = ExcecuteSelectQuery(query)
        For Each rw In dt.Rows
            Dim codep As String = rw(0).ToString
            While (Len(codep) < 4)
                codep = "0" & codep
            End While
            CmbTraiteur.Properties.Items.Add(MettreApost(rw(1).ToString & " " & rw(2).ToString) & " | " & codep)
        Next

    End Sub

    Private Sub PnlNewItiner_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PnlNewItiner.EnabledChanged
        PnlControl.Enabled = PnlNewItiner.Enabled
        BtRetour.Enabled = PnlNewItiner.Enabled
        PnlNewType.Enabled = Not (PnlNewItiner.Enabled)
    End Sub

    Private Sub TxtType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtType.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            If (TxtType.Text = "") Then
                FailMsg("Champ vide non autorise. veillez le remplir")
            Else
                If (TxtType.Text.Replace(" ", "") <> "") And TypeEnCours = "" Then

                    'Dim Reader As MySqlDataReader
                    query = "select * from T_TypeCourrier where LibelleType='" & EnleverApost(TxtType.Text) & "'"
                    dt = ExcecuteSelectQuery(query)
                    If dt.Rows.Count Then
                        MsgBox("Ce type existe déjà!", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If

                    Dim DatSet = New DataSet
                    query = "select * from T_TypeCourrier"
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    DatAdapt.Fill(DatSet, "T_TypeCourrier")
                    Dim DatTable = DatSet.Tables("T_TypeCourrier")
                    Dim DatRow = DatSet.Tables("T_TypeCourrier").NewRow()

                    DatRow("LibelleType") = EnleverApost(TxtType.Text)
                    DatRow("etat_corb") = "0"

                    DatSet.Tables("T_TypeCourrier").Rows.Add(DatRow)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Update(DatSet, "T_TypeCourrier")
                    DatSet.Clear()
                    BDQUIT(sqlconn)

                    ChargerType()
                    TxtType.Text = ""
                    TxtType.Focus()

                ElseIf (DrX(1).ToString <> "") Then

                    DrX = ViewType.GetDataRow(ViewType.FocusedRowHandle)
                    query = "Update t_typecourrier set LibelleType='" + EnleverApost(TxtType.Text) + "' where CodeType='" + DrX(1).ToString + "'"
                    ExecuteNonQuery(query)

                    ChargerType()
                    TxtType.Text = ""
                    DrX(1) = ""
                    TypeEnCours = ""

                Else
                    MsgBox("Chaine vide non autorisée!", MsgBoxStyle.Information)
                End If
            End If
        End If

    End Sub

    Private Sub GridType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridType.Click

        If (ViewType.RowCount > 0) Then

            DrX = ViewType.GetDataRow(ViewType.FocusedRowHandle)
            TxtType.Text = DrX(2).ToString
            TypeEnCours = DrX(1).ToString
            InfosType(DrX(1).ToString)

        End If

    End Sub

    Private Sub InfosType(ByVal CodeType As String)

        nbLigne = 0
        query = "select LibelleTraitement, CodeFonction, CodeOperateur, DelaiTraitement from T_TraitementCourrier where CodeType='" & CodeType & "' and CodeProjet='" & ProjetEnCours & "' order by OrdreTraitement"
        dt = ExcecuteSelectQuery(query)
        For Each rwx As DataRow In dt.Rows

            Traitement(nbLigne) = MettreApost(rwx(0).ToString)
            Dim traitX As String = ""
            query = "select CodeOperateur, NomOperateur, PrenOperateur from T_Operateur where EMP_ID<>'' and CodeOperateur='" + rwx(2).ToString + "' and CodeProjet='" & ProjetEnCours & "' order by NomOperateur"
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw In dt.Rows
                Dim codep As String = rw(0).ToString
                While (Len(codep) < 4)
                    codep = "0" & codep
                End While
                traitX = MettreApost(rw(1).ToString & " " & rw(2).ToString) & " | " & codep
            Next
            Traiteur(nbLigne) = traitX
            NbDelai(nbLigne) = CInt(Trim(rwx(3).ToString.Split(" "c)(0)))
            UniteDelai(nbLigne) = Trim(rwx(3).ToString.Split(" "c)(1))
            nbLigne += 1

        Next

        ChargerTraitement()

    End Sub

    Private Sub GridType_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridType.DoubleClick

        TxtType.Text = ""
        TxtNumDelai.EditValue = 1
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
        TxtTraitement.Text = ""
        CmbTraiteur.Text = ""
        PnlNewItiner.Enabled = False
        ColorRowGrid(ViewType, "[Code]='x'", Color.LightGray, "Times New Roman", 12, FontStyle.Regular, Color.Black)
        nbLigne = 0
        LgTraitCours = -1
        ChargerTraitement()

    End Sub

    Dim Traitement(100) As String
    Dim Traiteur(100) As String
    Dim NbDelai(100) As Decimal
    Dim UniteDelai(100) As String
    Dim nbLigNe As Decimal = 0

    Private Sub BtAjoutTraitement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAjoutTraitement.Click

        If (TxtTraitement.Text <> "" And CmbTraiteur.SelectedIndex <> -1 And TxtNumDelai.Text <> "" And CmbDelai.SelectedIndex <> -1) Then

            Traitement(nbLigNe) = EnleverApost(TxtTraitement.Text)
            Traiteur(nbLigNe) = EnleverApost(CmbTraiteur.Text)
            NbDelai(nbLigNe) = TxtNumDelai.EditValue
            UniteDelai(nbLigNe) = EnleverApost(CmbDelai.Text)

            TxtTraitement.Text = ""
            CmbTraiteur.Text = ""
            TxtNumDelai.EditValue = 1

            nbLigNe += 1
            LgTraitCours = -1
            ChargerTraitement()

            TxtTraitement.Focus()

        Else
            MsgBox("Information incomplète", MsgBoxStyle.Information, "ClearProjects")
        End If

    End Sub

    Private Sub ChargerTraitement()

        dtTrait.Columns.Clear()

        dtTrait.Columns.Add("Code", Type.GetType("System.String"))
        dtTrait.Columns.Add("N°", Type.GetType("System.String"))
        dtTrait.Columns.Add("Traitement", Type.GetType("System.String"))
        dtTrait.Columns.Add("Chargé du traitement", Type.GetType("System.String"))
        dtTrait.Columns.Add("Délai de traitement", Type.GetType("System.String"))

        dtTrait.Rows.Clear()

        For k As Integer = 0 To nbLigne - 1

            Dim drS = dtTrait.NewRow()

            drS(0) = IIf(CDec(k / 2) <> CDec(k \ 2), "x", "").ToString
            drS(1) = (k + 1).ToString
            drS(2) = Traitement(k)
            'drS(3) = Mid(Traiteur(k), 10)
            drS(3) = Traiteur(k)
            drS(4) = NbDelai(k).ToString & " " & UniteDelai(k)

            dtTrait.Rows.Add(drS)

        Next

        GridTraitement.DataSource = dtTrait

        ViewTraitement.Columns(0).Visible = False
        ViewTraitement.Columns(1).Width = 30
        ViewTraitement.Columns(2).Width = 204
        ViewTraitement.Columns(3).Width = 351
        ViewTraitement.Columns(4).Width = 122
        ViewTraitement.Appearance.Row.Font = New Font("Times New Roman", 10, FontStyle.Regular)
        ViewTraitement.Columns(4).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

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
            DrX1 = ViewTraitement.GetDataRow(ViewTraitement.FocusedRowHandle)

            ColorRowGrid(ViewTraitement, "[Code]='x'", Color.LightGray, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewTraitement, "[N°]='" & LgTraitCours.ToString & "'", Color.DarkGray, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

            TxtTraitement.Text = DrX1(2).ToString
            CmbTraiteur.Text = DrX1(3).ToString
            TxtNumDelai.Text = DrX1(4).ToString

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

                Traitement(k) = Traitement(k + 1)
                Traiteur(k) = Traiteur(k + 1)
                NbDelai(k) = NbDelai(k + 1)
                UniteDelai(k) = UniteDelai(k + 1)

                If (ViewTraitement.RowCount > 0) Then

                    DrX = ViewTraitement.GetDataRow(ViewTraitement.FocusedRowHandle)
                    query = "delete from T_TraitementCourrier where LibelleTraitement='" + EnleverApost(DrX(2).ToString) + "' and CodeType='" + TypeEnCours + "'"
                    ExecuteNonQuery(query)

                End If

            Next

            TypeEnCours = ""
            TxtTraitement.Text = ""
            CmbTraiteur.Text = ""
            TxtNumDelai.EditValue = 1
            CmbDelai.Text = ""
            nbLigne = nbLigne - 1
            LgTraitCours = -1
            ChargerTraitement()

        End If

    End Sub

    Private Sub BtEnreg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEnreg.Click

        If (ViewTraitement.RowCount > 0 And TypeEnCours <> "" And nbLigne > 0) Then

            query = "DELETE from T_TraitementCourrier where CodeType='" & TypeEnCours & "' and CodeProjet='" & ProjetEnCours & "'"
            ExecuteNonQuery(query)

            For k As Integer = 0 To nbLigne - 1

                Dim Operateur As String = ""
                Dim cr() As String
                cr = Traiteur(k).ToString.Split("|")
                If (Traiteur(k).ToString <> "") Then Operateur = Mid(cr(1), 3)
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim DatSet = New DataSet
                query = "select * from T_TraitementCourrier"
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)
                DatAdapt.Fill(DatSet, "T_TraitementCourrier")
                Dim DatTable = DatSet.Tables("T_TraitementCourrier")
                Dim DatRow = DatSet.Tables("T_TraitementCourrier").NewRow()

                DatRow("CodeType") = TypeEnCours
                DatRow("OrdreTraitement") = (k + 1).ToString
                DatRow("LibelleTraitement") = EnleverApost(Traitement(k))
                DatRow("CodeFonction") = "0"
                DatRow("CodeOperateur") = Operateur
                DatRow("DelaiTraitement") = NbDelai(k).ToString & " " & UniteDelai(k)
                DatRow("CodeProjet") = ProjetEnCours

                DatSet.Tables("T_TraitementCourrier").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_TraitementCourrier")
                DatSet.Clear()
                BDQUIT(sqlconn)

            Next

            MsgBox("Traitement enregistré avec succès!", MsgBoxStyle.Information)
            BtRetour_Click(Me, e)

        Else
            MsgBox("Enregistrement echoue")
        End If

    End Sub

    Private Sub SupprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        If (ViewType.RowCount > 0) Then
            DrX = ViewType.GetDataRow(ViewType.FocusedRowHandle)

            Dim result = MessageBox.Show("Voulez-vous Supprimer le Type de Courrier", "ClearProject", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then

            ElseIf result = DialogResult.Yes Then

                Dim codetype As String = ""

                query = "select LibelleTraitement from T_TraitementCourrier where CodeType='" & DrX(1).ToString & "'"
                dt = ExcecuteSelectQuery(query)
                If dt.Rows.Count = 0 Then
                    query = "delete from T_TypeCourrier where CodeType='" + DrX(1).ToString + "'"
                    ExecuteNonQuery(query)
                Else
                    MsgBox("ITENERAIRE EXISTE DEJA !!!")
                End If

                TypeEnCours = ""
                DrX(1) = ""
                TxtType.Text = ""
                ChargerType()

                End If
        End If
    End Sub

    Private Sub ModifierToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ModifierToolStripMenuItem.Click
        If (ViewType.RowCount > 0) Then
            DrX = ViewType.GetDataRow(ViewType.FocusedRowHandle)

            If DrX(1).ToString = "" Then
                TxtType.Text = "Non Défini"
            Else

                query = "Update t_typecourrier set LibelleType='" + EnleverApost(TxtType.Text) + "' where CodeType='" + DrX(1).ToString + "'"
                ExecuteNonQuery(query)

            End If
            TxtType.Text = DrX(2).ToString
            ChargerType()
        End If

    End Sub

End Class