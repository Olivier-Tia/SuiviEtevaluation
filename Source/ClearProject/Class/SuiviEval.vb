Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System
Imports System.Drawing
Imports Microsoft
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Math
Imports System.Text.RegularExpressions
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen
Imports AxSms
Imports AxEmail
Imports DevExpress.XtraEditors.Repository
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Module SuiviEval

    Public Function GetDepense(Type As String, LibelleCourt As String, DateDebut As Date, DateFin As Date, InitialeBailleur As String, Optional Cumul As Boolean = False) As Decimal
        Dim OptionSupplLigne As String = String.Empty
        Dim OptionSupplActivite As String = String.Empty
        Dim OptionBailleur As String = String.Empty

        If InitialeBailleur <> "" Then
            OptionBailleur = " AND InitialeBailleur='" & InitialeBailleur & "'"
        End If

        If Type = "Composante" Then
            If Not Cumul Then
                OptionSupplActivite = " AND (('" & dateconvert(DateFin) & "' BETWEEN Date_act AND DateSys) OR (DateSys<='" & dateconvert(DateFin) & "' AND DateSys>='" & dateconvert(DateDebut) & "'))"
                OptionSupplLigne = " AND DATE_LE<='" & dateconvert(DateFin) & "' AND DATE_LE>='" & dateconvert(DateDebut) & "'"

                query = "SELECT DISTINCT CODE_E, LibelleCourt FROM t_comp_activite WHERE LibelleCourt LIKE '" & LibelleCourt & "%' AND CODE_PROJET='" & ProjetEnCours & "'" & OptionSupplActivite & OptionBailleur & " ORDER BY LibelleCourt ASC"
            Else
                OptionSupplActivite = " AND (Date_act<='" & dateconvert(DateFin) & "')"
                OptionSupplLigne = " AND DATE_LE<='" & dateconvert(DateFin) & "'"

                query = "SELECT DISTINCT CODE_E,LibelleCourt FROM t_comp_activite WHERE LibelleCourt LIKE '" & LibelleCourt & "%' AND CODE_PROJET='" & ProjetEnCours & "'" & OptionSupplActivite & OptionBailleur & " ORDER BY LibelleCourt ASC"
            End If
        Else
            If Not Cumul Then
                OptionSupplActivite = " AND (('" & dateconvert(DateFin) & "' BETWEEN Date_act AND DateSys) OR (DateSys<='" & dateconvert(DateFin) & "' AND DateSys>='" & dateconvert(DateDebut) & "'))"
                OptionSupplLigne = " AND DATE_LE<='" & dateconvert(DateFin) & "' AND DATE_LE>='" & dateconvert(DateDebut) & "'"

                query = "SELECT DISTINCT CODE_E,LibelleCourt FROM t_comp_activite WHERE LibelleCourt='" & LibelleCourt & "' AND CODE_PROJET='" & ProjetEnCours & "'" & OptionSupplActivite & OptionBailleur & " ORDER BY LibelleCourt ASC"
            Else
                OptionSupplActivite = " AND (Date_act<='" & dateconvert(DateFin) & "')"
                OptionSupplLigne = " AND DATE_LE<='" & dateconvert(DateFin) & "'"

                query = "SELECT DISTINCT CODE_E,LibelleCourt FROM t_comp_activite WHERE LibelleCourt='" & LibelleCourt & "' AND CODE_PROJET='" & ProjetEnCours & "'" & OptionSupplActivite & OptionBailleur & " ORDER BY LibelleCourt ASC"
            End If
        End If

        Dim dt As DataTable = ExcecuteSelectQuery(query)
        Dim cpte As Decimal = 1
        Dim FactureCpte As Decimal = 0
        Dim MonatantLibelleCourt As Decimal = 0
        Dim str As String = String.Empty

        For Each rw In dt.Rows
            'Declaration des varaibles
            Dim MontantPaiement As Decimal = 0
            Dim MontantPrisEncCharge As Decimal = 0

            query = "SELECT DISTINCT CODE_J FROM t_comp_activite WHERE CODE_PROJET='" & ProjetEnCours & "' AND LibelleCourt='" & rw("LibelleCourt") & "' AND CODE_E='" & rw("CODE_E") & "'" & OptionSupplActivite & OptionBailleur
            Dim dtPriseEnCharge As DataTable = ExcecuteSelectQuery(query)
            For Each rwPC In dtPriseEnCharge.Rows

                Dim CODE_J As String = rwPC("CODE_J")

                query = "SELECT SUM(CREDIT_LE) As MontantPrisEnCharge FROM t_comp_ligne_ecriture WHERE CODE_E='" & rw("CODE_E") & "' AND CODE_J='" & CODE_J & "'" '& OptionSupplLigne
                MontantPrisEncCharge = Val(ExecuteScallar(query))

                query = "SELECT * FROM t_comp_ligne_ecriture WHERE CODE_E='" & rw("CODE_E") & "' AND CODE_J='" & CODE_J & "' AND CODE_CPT<>''" '& OptionSupplLigne
                Dim dtTiers As DataTable = ExcecuteSelectQuery(query)

                For Each rwTiers As DataRow In dtTiers.Rows
                    query = "SELECT SUM(DEBIT_LE) As Solde FROM t_comp_ligne_ecriture WHERE CODE_E='" & rw("CODE_E") & "' AND CODE_J<>'" & CODE_J & "' AND CODE_CPT='" & rwTiers("CODE_CPT") & "'" & OptionSupplLigne
                    Dim dtSolde As DataTable = ExcecuteSelectQuery(query)
                    Dim TReglement As Decimal = 0
                    Dim rwSolde As DataRow = dtSolde.Rows(0)
                    If Not IsDBNull(rwSolde("Solde")) Then
                        TReglement = CDec(rwSolde("Solde"))
                    End If

                    query = "SELECT SUM(CREDIT_LE) As Solde FROM t_comp_ligne_ecriture WHERE CODE_E='" & rw("CODE_E") & "' AND CODE_J<>'" & CODE_J & "' AND CODE_CPT='" & rwTiers("CODE_CPT") & "'" & OptionSupplLigne
                    dtSolde = ExcecuteSelectQuery(query)
                    rwSolde = dtSolde.Rows(0)
                    If Not IsDBNull(rwSolde("Solde")) Then
                        TReglement -= CDec(rwSolde("Solde"))
                    End If

                    MontantPaiement += TReglement
                Next

                If MontantPrisEncCharge = MontantPaiement Then
                    query = "SELECT Montant_act FROM t_comp_activite WHERE LibelleCourt='" & rw("LibelleCourt") & "' AND CODE_J='" & CODE_J & "' AND CODE_PROJET='" & ProjetEnCours & "' AND CODE_E='" & rw("CODE_E") & "'" & OptionSupplActivite & OptionBailleur
                    Dim MontActivite As Decimal = Val(ExecuteScallar(query))
                    MonatantLibelleCourt += MontActivite
                Else
                    query = "SELECT Montant_act FROM t_comp_activite WHERE CODE_PROJET='" & ProjetEnCours & "' AND CODE_J='" & CODE_J & "' AND CODE_E='" & rw("CODE_E") & "'" & OptionSupplActivite & OptionBailleur & " ORDER BY Montant_act DESC"
                    Dim dtPrisCharge As DataTable = ExcecuteSelectQuery(query)
                    Dim TempMontant = MontantPaiement
                    If dtPrisCharge.Rows.Count = 1 Then
                        MonatantLibelleCourt += MontantPaiement
                    Else
                        For Each rwPrisCharge As DataRow In dtPrisCharge.Rows
                            If CDec(rwPrisCharge("Montant_act")) <= TempMontant Then
                                TempMontant -= CDec(rwPrisCharge("Montant_act"))
                                MonatantLibelleCourt += CDec(rwPrisCharge("Montant_act"))
                            Else
                                MonatantLibelleCourt += TempMontant
                                Exit For
                            End If
                        Next
                    End If
                End If

            Next

        Next

        Return MonatantLibelleCourt
    End Function

    Public Function GetMontantPrecedent(LibelleCourt As String, DateDebutRsfEnCours As Date, Bailleur As String) As Decimal
        Dim OptionSupplLigne As String = String.Empty
        Dim OptionSupplActivite As String = String.Empty
        Dim DateRecherche As Date = DateAdd(DateInterval.Day, -1, DateDebutRsfEnCours)
        query = "SELECT RealisationCumule from t_etatfondsparactivite WHERE LibCourtActivite='" & LibelleCourt & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND STR_TO_DATE(MID(PeriodeRSF,14),'%d/%m/%Y')='" & dateconvert(DateRecherche) & "' AND CodeProjet='" & ProjetEnCours & "'"
        Return Val(ExecuteScallar(query))
    End Function

    Public Function GetMontantPrecedent_Rapports_Global(LibelleCourt As String, DateDebutRsfEnCours As Date, Bailleur As String) As Decimal
        Dim OptionSupplLigne As String = String.Empty
        Dim OptionSupplActivite As String = String.Empty
        Dim DateRecherche As Date = DateAdd(DateInterval.Day, -1, DateDebutRsfEnCours)
        query = "SELECT RealisationCumule from t_rsf_etatfondsparactivite_rapports_globals WHERE LibCourtActivite='" & LibelleCourt & "' AND Bailleur='" & EnleverApost(Bailleur) & "' AND STR_TO_DATE(MID(PeriodeRSF,14),'%d/%m/%Y')='" & dateconvert(DateRecherche) & "' AND CodeProjet='" & ProjetEnCours & "'"
        Return Val(ExecuteScallar(query))
    End Function

    Public Function GetDotation(Type As String, LibelleCourt As String, DateDebut As Date, DateFin As Date, InitialeBailleur As String, Optional Cumul As Boolean = False) As Decimal
        Dim OptionSuppActivite As String = String.Empty
        Dim OptionSuppRepart As String = String.Empty

        If Not Cumul Then
            OptionSuppActivite = " And YEAR(DateDebutPartition)<='" & DateDebut.Year & "' AND YEAR(DateFinPartition)>='" & DateFin.Year & "'"
            OptionSuppRepart = " AND STR_TO_DATE(DateEcheance,'%d/%m/%Y')>='" & dateconvert(DateDebut) & "' AND STR_TO_DATE(DateEcheance,'%d/%m/%Y')<='" & dateconvert(DateFin) & "'"

            If Type = "Composante" Then
                query = "select DISTINCT LibelleCourt from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' AND LibelleCourt LIKE '" & LibelleCourt & "%' LIMIT 1"
            ElseIf Type = "Activité" Then
                query = "select CodePartition from T_Partition where LENGTH(LibelleCourt)='5' and CodeProjet='" & ProjetEnCours & "' AND LibelleCourt='" & LibelleCourt & "'" & OptionSuppActivite
            End If
        Else
            OptionSuppActivite = " AND DateDebutPartition<='" & dateconvert(DateFin) & "'"
            OptionSuppRepart = " AND STR_TO_DATE(DateEcheance,'%d/%m/%Y')<='" & dateconvert(DateFin) & "'"

            If Type = "Composante" Then
                query = "select DISTINCT LibelleCourt from T_Partition where LENGTH(LibelleCourt)='1' and CodeProjet='" & ProjetEnCours & "' AND LibelleCourt LIKE '" & LibelleCourt & "%' LIMIT 1"
            ElseIf Type = "Activité" Then
                query = "select CodePartition from T_Partition where LENGTH(LibelleCourt)='5' and CodeProjet='" & ProjetEnCours & "' AND LibelleCourt='" & LibelleCourt & "'" & OptionSuppActivite
            End If
        End If

        Dim TotalDotation As Decimal = 0
        If Type = "Composante" Then
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                query = "select CodePartition from T_Partition where LENGTH(LibelleCourt)='5' and CodeProjet='" & ProjetEnCours & "' AND LibelleCourt LIKE '" & rw("LibelleCourt") & "%'" & OptionSuppActivite
                Dim dtActivites As Data.DataTable = ExcecuteSelectQuery(query)

                For Each rwActivite As DataRow In dtActivites.Rows
                    ' If InitialeBailleur = "Tous les bailleurs" Then
                    If InitialeBailleur = "" Then
                        query = "select SUM(MontantEcheance) from t_echeanceactivite where CodePartition='" & rwActivite("CodePartition") & "'" & OptionSuppRepart
                    Else
                        Dim CodeBailleur As Decimal = Val(ExecuteScallar("SELECT CodeBailleur FROM t_bailleur WHERE InitialeBailleur='" & InitialeBailleur.Split(" - ")(0) & "'"))
                        query = "select SUM(MontantEcheance) from t_echeanceactivite where CodePartition='" & rwActivite("CodePartition") & "' AND CodeBailleur='" & CodeBailleur & "'" & OptionSuppRepart
                    End If
                    TotalDotation += Val(ExecuteScallar(query))
                Next
            Next
        ElseIf Type = "Activité" Then
            Dim dt As DataTable = ExcecuteSelectQuery(query)
            For Each rw As DataRow In dt.Rows
                If InitialeBailleur = "" Then
                    query = "select SUM(MontantEcheance) from t_echeanceactivite where CodePartition='" & rw("CodePartition") & "'" & OptionSuppRepart
                Else
                    Dim CodeBailleur As Decimal = Val(ExecuteScallar("SELECT CodeBailleur FROM t_bailleur WHERE InitialeBailleur='" & InitialeBailleur.Split(" - ")(0) & "'"))
                    query = "select SUM(MontantEcheance) from t_echeanceactivite where CodePartition='" & rw("CodePartition") & "' AND CodeBailleur='" & CodeBailleur & "'" & OptionSuppRepart
                End If
                TotalDotation += Val(ExecuteScallar(query))
            Next
        End If

        Return TotalDotation
    End Function
End Module
