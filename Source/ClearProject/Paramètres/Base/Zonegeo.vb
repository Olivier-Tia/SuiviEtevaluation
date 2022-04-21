Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Data.DataSet
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO.Ports

Public Class Zonegeo
    Dim dtzonegeo = New DataTable
    Dim DrX As DataRow

    Private Sub RemplirCmbTypZone()
        Try
            If CmbTypZone.SelectedIndex = -1 Then
                CmbTypZone.Properties.Items.Clear()
               query= "select LibelleStr from T_StructGeo ORDER BY NiveauStr"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    CmbTypZone.Properties.Items.Add(MettreApost(rw(0).ToString))
                Next
            End If
        Catch ex As Exception
            FailMsg("Code Erreur 0X0002 " & vbNewLine & ex.ToString())
        End Try
    End Sub
    Private Sub RemplirCmbDevise()
        Try
            If CmbDevise.SelectedIndex = -1 Then
                CmbDevise.Properties.Items.Clear()
               query= "select LibelleDevise from T_Devise ORDER BY LibelleDevise"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                For Each rw As DataRow In dt0.Rows
                    CmbDevise.Properties.Items.Add(MettreApost(rw(0).ToString))
                Next
            End If
        Catch ex As Exception
            FailMsg("Code Erreur 0X0003 " & vbNewLine & ex.ToString())
        End Try
    End Sub
    Private Sub CmbIssu_de_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbIssu_de.SelectedIndexChanged
        Try
            Dim LibelZ As String = CmbIssu_de.Text
            CorrectionChaine(LibelZ)
            query = "select CodeZone, IndicZone, CodeDevise, TVA from T_ZoneGeo where LibelleZone='" & EnleverApost(LibelZ) & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows

                TxtCodeZoneMereCache.Text = rw(0)
                TxtIndicatifZone.Text = rw(1)
                TxtCodeDeviseCache.Text = rw(2)
                TxtTva.Text = rw(3)
                Dim LibD As String = ""

                'Recherche de la devise***************
                query = "select LibelleDevise from T_Devise where CodeDevise='" & rw(2) & "'"
                Dim dt1 = ExcecuteSelectQuery(query)
                For Each rw1 As DataRow In dt1.Rows
                    LibD = rw1(0)
                    RestaurerChaine(LibD)
                Next
                CmbDevise.Text = LibD
                ActualiserDevise.Enabled = False

            Next
        Catch ex As Exception
            FailMsg("Code Erreur 0XIT_Z_MERE0001 " & vbNewLine & ex.ToString())
        End Try
    End Sub
    Private Sub CmbDevise_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbDevise.SelectedIndexChanged
        Try
            Dim LibelD As String = CmbDevise.Text
            CorrectionChaine(LibelD)
            query = "select CodeDevise from T_Devise where LibelleDevise='" & LibelD & "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt0.Rows
                TxtCodeDeviseCache.Text = rw(0)
            Next
        Catch ex As Exception
            FailMsg("Code Erreur 0XIT_DEV_CODE0001 " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub CmbTypZone_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbTypZone.SelectedIndexChanged
        Dim Temp0, Temp As Decimal
        Try
            If CmbTypZone.SelectedIndex > -1 Then
                query = "select NiveauStr, LibelleStr from T_StructGeo WHERE LibelleStr = '" & CmbTypZone.Text & "'"
                Dim dt0 As DataTable = ExcecuteSelectQuery(query)
                If dt0.Rows.Count = 0 Then
                    Initialiser()
                    Exit Sub
                End If
                Dim rwa As DataRow = dt0.Rows(0)
                Temp0 = rwa(0)
                TxtNiveauStrCache.Text = Temp0.ToString
                Temp = Temp0 - 1

                If Temp > 0 Then
                    query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr <= '" & Temp & "';"
                    If (Temp0 = 5) Then
                        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr >= '2' and NiveauStr <= '4'"
                    End If
                    If (Temp0 = 6) Then
                        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr >= '4' and NiveauStr <= '5'"
                    End If
                    If (Temp0 = 7) Then
                        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr >= '4' and NiveauStr <= '6'"
                    End If
                    If (Temp0 = 8) Then
                        query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr = '4' or NiveauStr = '6'"
                    End If
                Else
                    'Temp = 1
                    query = "select NiveauStr, LibelleZone from T_ZoneGeo WHERE NiveauStr = '" & Temp & "';"
                End If

                dt0 = ExcecuteSelectQuery(query)
                CmbIssu_de.Properties.Items.Clear()
                CmbIssu_de.Text = ""
                For Each rw As DataRow In dt0.Rows
                    CmbIssu_de.Properties.Items.Add(MettreApost(rw(1).ToString))
                Next
                If CmbTypZone.Text.ToLower = "pays" Then
                    TxtIndicatifZone.Enabled = True
                    CmbDevise.Enabled = True
                    TxtTva.Enabled = True
                    CmbIssu_de.Enabled = False
                    TxtIndicatifZone.Enabled = True
                    CmbDevise.Enabled = True
                    ActualiserDevise.Enabled = True
                Else
                    TxtIndicatifZone.Enabled = False
                    CmbDevise.Enabled = False
                    CmbIssu_de.Enabled = True
                    TxtIndicatifZone.Enabled = False
                    CmbDevise.Enabled = False
                    ActualiserDevise.Enabled = False
                End If
                TxtNomZone.Enabled = True
                TxtAbrege.Enabled = True
                TxtTva.Enabled = True
            Else
                Initialiser()
            End If

        Catch ex As Exception
            FailMsg("Code Erreur 0X0004 " & vbNewLine & ex.ToString())
        End Try
    End Sub

    Private Sub ActualiserDevise_Click(sender As System.Object, e As System.EventArgs) Handles ActualiserDevise.Click
        Dialog_form(Devise)
        RemplirCmbDevise()
    End Sub

    Private Sub BtEnregistrer_Click(sender As System.Object, e As System.EventArgs) Handles BtEnregistrer.Click
        If CmbTypZone.SelectedIndex = -1 Then
            SuccesMsg("Veuillez choisir le type de zone dans la liste.")
            Exit Sub
        End If

        If (CmbDevise.SelectedIndex = -1) And CmbDevise.Enabled Then
            SuccesMsg("Veuillez choisir la devise dans la liste.")
            Exit Sub
        End If

        If (CmbIssu_de.SelectedIndex = -1) And CmbIssu_de.Enabled Then
            SuccesMsg("Veuillez choisir zone supérieure dans la liste.")
            Exit Sub
        End If

        If (CmbTypZone.Text <> "" And TxtNomZone.Text.Trim() <> "" And TxtAbrege.Text.Trim() <> "" And TxtIndicatifZone.Text.Trim() <> "" And CmbDevise.Text <> "") Then

            Dim action As String = ""
            Dim madate = Now
            Dim dd = madate.ToString("H:mm:ss")
            madate = madate.ToString("yyyy-MM-dd")
            query = "SELECT * FROM T_ZoneGeo where CodeZone = '" + TxtCodeZone.Text + "'"
            Dim dt0 As DataTable = ExcecuteSelectQuery(query)
            If dt0.Rows.Count = 0 Then
                Dim DatSet = New DataSet
                query = "SELECT * FROM T_ZoneGeo"
                Dim sqlconn As New MySqlConnection
                BDOPEN(sqlconn)
                Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                Dim DatAdapt = New MySqlDataAdapter(Cmd)

                Dim codemere As Decimal = 0
                codemere = IIf(TxtCodeZoneMereCache.Text = "", 0, TxtCodeZoneMereCache.Text)

                DatAdapt.Fill(DatSet, "T_ZoneGeo")
                Dim DatTable = DatSet.Tables("T_ZoneGeo")
                Dim DatRow = DatSet.Tables("T_ZoneGeo").NewRow()

                DatRow("LibelleZone") = EnleverApost(TxtNomZone.Text)
                DatRow("AbregeZone") = TxtAbrege.Text
                DatRow("CodeZoneMere") = codemere.ToString
                DatRow("IndicZone") = TxtIndicatifZone.Text
                DatRow("NiveauStr") = TxtNiveauStrCache.Text
                DatRow("CodeDevise") = TxtCodeDeviseCache.Text
                DatRow("TVA") = Val(TxtTva.Text)

                DatSet.Tables("T_ZoneGeo").Rows.Add(DatRow)
                Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                DatAdapt.Update(DatSet, "T_ZoneGeo")
                DatSet.Clear()
                BDQUIT(sqlconn)

                action = "Ajout de la zone géogaphique : " + EnleverApost(TxtNomZone.Text) + ""

                query = "SELECT CodeZone FROM T_ZoneGeo where LibelleZone = '" + EnleverApost(TxtNomZone.Text) + "'"
                Dim codezone = ExecuteScallar(query)

                ''historique
                'sql = "insert into t_historique values (NULL,'" + ProjetEnCours + "','" + NomUtilisateur + "','" + EnleverApost(action) + "','" + madate + "','" + dd + "')"
                'ExecuteNonQuery(query)

                CmbTypZone.Focus()
            Else
                If (ViewZoneGeo.RowCount > 0) Then
                    DrX = ViewZoneGeo.GetDataRow(ViewZoneGeo.FocusedRowHandle)
                    Dim DatSet = New DataSet
                    query = "select * from T_ZoneGeo where CodeZone='" + EnleverApost(DrX(0).ToString) + "'"
                    Dim sqlconn As New MySqlConnection
                    BDOPEN(sqlconn)
                    Dim Cmd As MySqlCommand = New MySqlCommand(query, sqlconn)
                    Dim DatAdapt = New MySqlDataAdapter(Cmd)
                    Dim CmdBuilder = New MySqlCommandBuilder(DatAdapt)
                    DatAdapt.Fill(DatSet, "T_ZoneGeo")

                    CorrectionChaine(TxtNomZone.Text)
                    DatSet.Tables!T_ZoneGeo.Rows(0)!LibelleZone = EnleverApost(TxtNomZone.Text)
                    DatSet.Tables!T_ZoneGeo.Rows(0)!AbregeZone = TxtAbrege.Text
                    DatSet.Tables!T_ZoneGeo.Rows(0)!TVA = TxtTva.Text
                    DatSet.Tables!T_ZoneGeo.Rows(0)!CodeZoneMere = TxtCodeZoneMereCache.Text
                    DatSet.Tables!T_ZoneGeo.Rows(0)!CodeDevise = TxtCodeDeviseCache.Text
                    DatSet.Tables!T_ZoneGeo.Rows(0)!IndicZone = TxtIndicatifZone.Text

                    DatAdapt.Update(DatSet, "T_ZoneGeo")
                    DatSet.Clear()
                    BDQUIT(sqlconn)
                    'action = "Modification de la zone géogaphique : " + EnleverApost(TxtNomZone.Text) + ""
                    'sql = "insert into t_historique values (NULL,'" + ProjetEnCours + "','" + NomUtilisateur + "','" + EnleverApost(action) + "','" + madate + "','" + dd + "')"
                    'ExecuteNonQuery(query)
                End If
                CmbTypZone.Focus()
            End If
        Else
            SuccesMsg("Veuillez remplir tous les champs svp.")
            Exit Sub
        End If

        Initialiser()
        remplirdatagridzonegeo()
    End Sub

    Private Sub Zonegeo_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Initialiser()
    End Sub

    Private Sub Zonegeo_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        RemplirCmbDevise()
        RemplirCmbTypZone()
        remplirdatagridzonegeo()
    End Sub

    Private Sub BtRetour_Click(sender As System.Object, e As System.EventArgs) Handles BtRetour.Click
        Initialiser()
    End Sub

    Private Sub Initialiser()
        CmbTypZone.Text = "pays".ToUpper
        RemplirCmbTypZone()
        CmbIssu_de.Properties.Items.Clear()
        CmbIssu_de.Text = ""
        CmbDevise.Text = ""
        TxtNomZone.Text = ""
        TxtAbrege.Text = ""
        TxtIndicatifZone.Text = ""
        TxtTva.Text = ""
        TxtCodeZone.Text = ""
        RemplirCmbDevise()

        TxtNiveauStrCache.Text = "1"
        TxtCodeZoneMereCache.Text = ""
        TxtCodeDeviseCache.Text = ""
    End Sub

    Private Sub remplirdatagridzonegeo()
        Dim CodeDev As Decimal
        Dim LibDev As String
        Dim Codz As Decimal

        dtzonegeo.Columns.Clear()
        dtzonegeo.Columns.Add("CodeZone", Type.GetType("System.String"))
        dtzonegeo.Columns.Add("Code", Type.GetType("System.String"))
        dtzonegeo.Columns.Add("Indicatif", Type.GetType("System.String"))
        dtzonegeo.Columns.Add("Nom Zone", Type.GetType("System.String"))
        dtzonegeo.Columns.Add("Type Zone", Type.GetType("System.String"))
        dtzonegeo.Columns.Add("Issue de", Type.GetType("System.String"))
        dtzonegeo.Columns.Add("Devise", Type.GetType("System.String"))
        dtzonegeo.Columns.Add("TVA", Type.GetType("System.String"))
        dtzonegeo.Columns.Add("CodeMere", Type.GetType("System.String"))
        dtzonegeo.Rows.Clear()

        'remplir le datagrid

        query = "SELECT Z.CodeZone, Z.AbregeZone, Z.IndicZone, Z.LibelleZone, Z.CodeZoneMere, S.LibelleStr, Z.CodeDevise, Z.TVA FROM T_ZoneGeo As Z, T_StructGeo As S WHERE Z.NiveauStr=S.NiveauStr and Z.NiveauStr=1"
        Dim dt0 As DataTable = ExcecuteSelectQuery(query)
        For Each rw As DataRow In dt0.Rows
            Dim drS = dtzonegeo.NewRow()
            drS(0) = rw(0).ToString
            drS(1) = rw(1).ToString
            drS(2) = MettreApost(rw(2).ToString)
            drS(3) = MettreApost(rw(3).ToString)
            drS(4) = rw(5).ToString
            Codz = rw(4).ToString

            If IsDBNull(rw(6).ToString) Then
                LibDev = ""
            Else
                CodeDev = rw(6).ToString
                'Procedure pour afficher la devise dans le Combo Devise
                query = "SELECT LibelleDevise FROM T_Devise WHERE CodeDevise = '" & CodeDev & "'"
                Dim dtq As DataTable = ExcecuteSelectQuery(query)
                For Each rwq As DataRow In dtq.Rows
                    drS(6) = rwq(0).ToString
                Next
            End If

            'Procedure pour récupérer CodeZoneMere pour afficher le Combo Issu de
            Dim LibZ As String = ""
            query = ("SELECT LibelleZone FROM T_ZoneGeo WHERE CodeZone = '" & Codz & "'")
            Dim dt1 As DataTable = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                LibZ = MettreApost(rw1(0).ToString)
            Next
            If (LibZ = MettreApost(rw(3).ToString)) Then
                drS(5) = ""
            Else
                drS(5) = MettreApost(LibZ)
            End If

            drS(7) = MettreApost(rw(7).ToString)
            drS(8) = Codz

            dtzonegeo.Rows.Add(drS)
            LgListZoneGeo.DataSource = dtzonegeo

            query = "SELECT Z.CodeZone, Z.AbregeZone, Z.IndicZone, Z.LibelleZone, Z.CodeZoneMere, S.LibelleStr, Z.CodeDevise, Z.TVA FROM T_ZoneGeo As Z, T_StructGeo As S WHERE Z.NiveauStr=S.NiveauStr and Z.NiveauStr=2 and Z.CodeZoneMere='" & rw(0).ToString & "'"
            dt1 = ExcecuteSelectQuery(query)
            For Each rw1 As DataRow In dt1.Rows
                Dim drS1 = dtzonegeo.NewRow()
                drS1(0) = rw1(0).ToString
                drS1(1) = rw1(1).ToString
                drS1(2) = MettreApost(rw1(2).ToString)
                drS1(3) = MettreApost(rw1(3).ToString)
                drS1(4) = rw1(5).ToString
                Codz = rw1(4).ToString

                If IsDBNull(rw1(6).ToString) Then
                    LibDev = ""
                Else
                    CodeDev = rw1(6).ToString
                    'Procedure pour afficher la devise dans le Combo Devise
                   query= "SELECT LibelleDevise FROM T_Devise WHERE CodeDevise = '" & CodeDev & "'"
                    Dim dtq = ExcecuteSelectQuery(query)
                    For Each rwq As DataRow In dtq.Rows
                        drS1(6) = rwq(0).ToString
                    Next
                End If

                'Procedure pour récupérer CodeZoneMere pour afficher le Combo Issu de
                Dim LibZ1 As String = ""

                If Codz.ToString = "0" Then

                    drS1(5) = ""

                Else

                   query= "SELECT LibelleZone FROM T_ZoneGeo WHERE CodeZone = '" & Codz & "'"
                    Dim dt2 = ExcecuteSelectQuery(query)
                    For Each rw2 As DataRow In dt2.Rows
                        drS1(5) = MettreApost(rw2(0).ToString)
                    Next

                End If
                
                drS1(7) = MettreApost(rw1(7).ToString)
                drS1(8) = Codz
                dtzonegeo.Rows.Add(drS1)
                LgListZoneGeo.DataSource = dtzonegeo

                query = "SELECT Z.CodeZone, Z.AbregeZone, Z.IndicZone, Z.LibelleZone, Z.CodeZoneMere, S.LibelleStr, Z.CodeDevise, Z.TVA FROM T_ZoneGeo As Z, T_StructGeo As S WHERE Z.NiveauStr=S.NiveauStr and Z.NiveauStr=3 and Z.CodeZoneMere='" & rw1(0).ToString & "'"
                Dim dt3 = ExcecuteSelectQuery(query)
                For Each rw2 As DataRow In dt3.Rows

                    Dim drS2 = dtzonegeo.NewRow()
                    drS2(0) = rw2(0).ToString
                    drS2(1) = rw2(1).ToString
                    drS2(2) = MettreApost(rw2(2).ToString)
                    drS2(3) = MettreApost(rw2(3).ToString)
                    drS2(4) = rw2(5).ToString
                    Codz = rw2(4).ToString

                    If IsDBNull(rw2(6).ToString) Then
                        LibDev = ""
                    Else
                        CodeDev = rw2(6).ToString
                        'Procedure pour afficher la devise dans le Combo Devise

                       query= "SELECT LibelleDevise FROM T_Devise WHERE CodeDevise = '" & CodeDev & "'"
                        Dim dtq = ExcecuteSelectQuery(query)
                        For Each rwq As DataRow In dtq.Rows
                            drS2(6) = rwq(0).ToString
                        Next
                    End If

                    'Procedure pour récupérer CodeZoneMere pour afficher le Combo Issu de
                    Dim LibZ2 As String = ""

                   query= "SELECT LibelleZone FROM T_ZoneGeo WHERE CodeZone = '" & Codz & "'"
                    Dim dtz = ExcecuteSelectQuery(query)
                    For Each rw3 As DataRow In dtz.Rows
                        LibZ2 = MettreApost(rw3(0).ToString)
                    Next
                    If (LibZ2 = MettreApost(rw2(3).ToString)) Then
                        drS2(5) = ""
                    Else
                        drS2(5) = MettreApost(LibZ2)
                    End If

                    drS2(7) = MettreApost(rw2(7).ToString)
                    drS2(8) = Codz
                    dtzonegeo.Rows.Add(drS2)
                    LgListZoneGeo.DataSource = dtzonegeo

                    query = "SELECT Z.CodeZone, Z.AbregeZone, Z.IndicZone, Z.LibelleZone, Z.CodeZoneMere, S.LibelleStr, Z.CodeDevise, Z.TVA FROM T_ZoneGeo As Z, T_StructGeo As S WHERE Z.NiveauStr=S.NiveauStr and Z.NiveauStr=4 and Z.CodeZoneMere='" & rw2(0).ToString & "'"
                    dtz = ExcecuteSelectQuery(query)
                    For Each rw3 As DataRow In dtz.Rows
                        Dim drS3 = dtzonegeo.NewRow()
                        drS3(0) = rw3(0).ToString
                        drS3(1) = rw3(1).ToString
                        drS3(2) = MettreApost(rw3(2).ToString)
                        drS3(3) = MettreApost(rw3(3).ToString)
                        drS3(4) = rw3(5).ToString
                        Codz = rw3(4).ToString

                        If IsDBNull(rw3(6).ToString) Then
                            LibDev = ""
                        Else
                            CodeDev = rw3(6).ToString
                            'Procedure pour afficher la devise dans le Combo Devise

                           query= "SELECT LibelleDevise FROM T_Devise WHERE CodeDevise = '" & CodeDev & "'"
                            Dim dtq = ExcecuteSelectQuery(query)
                            For Each rwq As DataRow In dtq.Rows
                                drS3(6) = rwq(0).ToString
                            Next
                        End If

                        'Procedure pour récupérer CodeZoneMere pour afficher le Combo Issu de
                        Dim LibZ3 As String = ""

                       query= "SELECT LibelleZone FROM T_ZoneGeo WHERE CodeZone = '" & Codz & "'"
                        Dim dt4 = ExcecuteSelectQuery(query)
                        For Each rw4 As DataRow In dt4.Rows
                            LibZ3 = MettreApost(rw4(0).ToString)
                        Next
                        If (LibZ3 = MettreApost(rw3(3).ToString)) Then
                            drS3(5) = ""
                        Else
                            drS3(5) = MettreApost(LibZ3)
                        End If
                        drS3(8) = Codz
                        drS3(7) = MettreApost(rw3(7).ToString)
                        dtzonegeo.Rows.Add(drS3)
                        LgListZoneGeo.DataSource = dtzonegeo
                    Next
                Next
            Next
        Next

        LgListZoneGeo.DataSource = dtzonegeo
        ViewZoneGeo.Columns(0).Visible = False
        ViewZoneGeo.Columns("CodeMere").Visible = False
        ViewZoneGeo.OptionsView.ColumnAutoWidth = True
        ViewZoneGeo.OptionsBehavior.AutoExpandAllGroups = True
        ViewZoneGeo.VertScrollVisibility = True
        ViewZoneGeo.HorzScrollVisibility = True

    End Sub

    Private Sub LgListZoneGeo_Click(sender As System.Object, e As System.EventArgs) Handles LgListZoneGeo.Click
        If (ViewZoneGeo.RowCount > 0) Then
            DrX = ViewZoneGeo.GetDataRow(ViewZoneGeo.FocusedRowHandle)
            TxtAbrege.Text = DrX(1).ToString
            CmbTypZone.Text = DrX(4).ToString
            CmbIssu_de.Text = DrX(5).ToString
            TxtNomZone.Text = DrX(3).ToString
            TxtIndicatifZone.Text = DrX(2).ToString
            TxtTva.Text = DrX(7).ToString
            CmbDevise.Text = DrX(6).ToString
            TxtCodeZone.Text = DrX(0).ToString
            TxtCodeZoneMereCache.Text = IIf(DrX(8).ToString = "", 0, DrX(8).ToString)

            Dim IDL = DrX(1).ToString
            ColorRowGrid(ViewZoneGeo, "[Code]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
            ColorRowGridAnal(ViewZoneGeo, "[Code]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)
        End If
    End Sub

    Private Sub LgListZoneGeo_DoubleClick(sender As Object, e As System.EventArgs) Handles LgListZoneGeo.DoubleClick
        'If (ViewZoneGeo.RowCount > 0) Then
        '    DrX = ViewZoneGeo.GetDataRow(ViewZoneGeo.FocusedRowHandle)

        '    TxtAbrege.Text = DrX(1).ToString
        '    CmbTypZone.Text = DrX(4).ToString
        '    CmbIssu_de.Text = DrX(5).ToString
        '    TxtNomZone.Text = DrX(3).ToString
        '    TxtIndicatifZone.Text = DrX(2).ToString
        '    TxtTva.Text = DrX(7).ToString
        '    CmbDevise.Text = DrX(6).ToString
        '    TxtCodeZone.Text = DrX(0).ToString

        '    Dim IDL = DrX(1).ToString
        '    ColorRowGrid(ViewZoneGeo, "[Code]='x'", Color.White, "Times New Roman", 10, FontStyle.Regular, Color.Black)
        '    ColorRowGridAnal(ViewZoneGeo, "[Code]='" & IDL & "'", Color.Navy, "Times New Roman", 10, FontStyle.Bold, Color.White, True)

        '    Dim City As String = DrX(3).ToString
        '    Dim State As String = DrX(5).ToString
        '    Dim queryAddress As New StringBuilder()
        '    queryAddress.Append("http://maps.google.com/maps?q=")

        '    If DrX(2).ToString <> String.Empty Then
        '        queryAddress.Append(City + " , " & " + ")
        '    End If
        '    If DrX(4).ToString <> String.Empty Then
        '        queryAddress.Append(State + " , " & " + ")
        '    End If
        '    Maps.WebBrowser1.Navigate(queryAddress.ToString())

        '    Maps.ShowDialog()
        'End If
    End Sub

    Private Sub SupprimerLaLigneSelectionnerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupprimerLaLigneSelectionnerToolStripMenuItem.Click
        Dim action As String = ""
        Dim madate = Now
        Dim dd = madate.ToString("H:mm:ss")
        madate = madate.ToString("yyyy-MM-dd")


        If (ViewZoneGeo.RowCount > 0) Then
            DrX = ViewZoneGeo.GetDataRow(ViewZoneGeo.FocusedRowHandle)

            Dim Reponse As MsgBoxResult
            Reponse = MsgBox("Voulez-vous supprimer définitivement la zone " & MettreApost(DrX(3).ToString) & " du projet?", MsgBoxStyle.YesNo, "Suppression")
            If (Reponse = MsgBoxResult.Yes) Then

                Try

                   query= "SELECT CodeZone FROM T_ZoneGeo where CodeZone = '" + EnleverApost(DrX(0).ToString) + "'"
                    Dim codezone = ExecuteScallar(query)


                    query = "DELETE from T_ZoneGeo where CodeZone='" & EnleverApost(DrX(0).ToString) & "'"
                    ExecuteNonQuery(query)

                    ''historique
                    'action = "Suppression de la zone géogaphique : " + MettreApost(DrX(3).ToString) + ""
                    'sql = "insert into t_historique values (NULL,'" + ProjetEnCours + "','" + NomUtilisateur + "','" + EnleverApost(action) + "','" + madate + "','" + dd + "')"
                    'ExecuteNonQuery(query)


                    remplirdatagridzonegeo()
                    Initialiser()

                Catch ex As Exception
                    FailMsg("Code Erreur 0XSUP_LOCLT0001 " & vbNewLine & ex.ToString())
                End Try
            End If
        End If
    End Sub

    Private Sub TxtNomZone_TextChanged(sender As Object, e As System.EventArgs) Handles TxtNomZone.TextChanged
        If (ViewZoneGeo.RowCount > 0) Then
            
        Else
            If (TxtNomZone.Text.Replace(" ", "") <> "") Then

                Dim partS() As String = (TxtNomZone.Text.Replace("'", "").Replace("  ", " ").Replace(" le", "").Replace(" la", "").Replace(" les", "").Replace(" l'", "").Replace(" de", "").Replace(" du", "").Replace(" des", "").Replace(" d'", "")).Split(" "c)
                Dim CodeS As String = ""
                For Each elt In partS
                    CodeS = CodeS & Mid(elt, 1, 1).ToUpper
                Next
                TxtAbrege.Text = CodeS

            Else
                TxtAbrege.Text = ""
            End If
        End If
       
    End Sub

End Class