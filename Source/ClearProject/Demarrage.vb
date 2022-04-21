Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient

Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Text
Imports System.Windows.Forms
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Threading

Public Class Demarrage

    Private Sub Demarrage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide
        SplitEvaluation.Visible = False
        SplitSuiviEval.Visible = False

        Dim taille As Decimal = Math.Round((hauteur - 340) / 3, 0) 'Pour 3 elements par ligne
        'Dim taille As Decimal = Math.Round((hauteur - 273) / 3, 0) 'Pour 2 elements par ligne
        TuileDemarrage.ItemSize = taille

        TuileDemarrage.Visible = True
        TuileDemarrage.Dock = DockStyle.Fill

    End Sub

    Private Sub OuvrirNavPane()
        If (ClearMdi.NavBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed) Then ClearMdi.NavBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Expanded
    End Sub

    Private Sub TileSuiviEval_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileSuiviEval.ItemClick

        ClearMdi.NavBarControl1.ActiveGroup = ClearMdi.GroupSuiviEval
        SplitSuiviEval.Visible = True
        SplitSuiviEval.Dock = DockStyle.Fill
        TuileDemarrage.Visible = False
        Me.Text = "SUIVI ET EVALUATION"

    End Sub

    Private Sub BtAccueilBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAccueilBudget.Click
        TuileDemarrage.Visible = True
        TuileDemarrage.Dock = DockStyle.Fill
        SplitEvaluation.Visible = False
        Me.Text = "ACCUEIL"
    End Sub


    Private Sub BtPrevisionActivite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPrevisionActivite.Click
        Disposer_form(FichesActivitesEtape1)
        FinChargement()
    End Sub

    Private Sub BtDiagrammeGantt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtDiagrammeGantt.Click
        Disposer_form(DiagrammeGantt)
        FinChargement()
    End Sub

    Private Sub BtRessourcesActivite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRessourcesActivite.Click
        DebutChargement()
        Disposer_form(FichesActivitesEtape2)
        FinChargement()
    End Sub

    Private Sub BtProprieteActivite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtProprieteActivite.Click
        Disposer_form(FichesActivitesEtape3)
        FinChargement()
    End Sub

    Private Sub BtRSPM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRSPM.Click
        DebutChargement()
        Disposer_form(RapportSurMarches)
        FinChargement()
    End Sub

    Private Sub BtAccueilSuiviEval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAccueilSuiviEval.Click
        TuileDemarrage.Visible = True
        TuileDemarrage.Dock = DockStyle.Fill
        SplitSuiviEval.Visible = False
        Me.Text = "ACCUEIL"
    End Sub

    Private Sub BtEditionBudgetaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtEditionBudgetaire.Click
        DebutChargement()
        Disposer_form(FicheActivite)
        FinChargement()
    End Sub

    Private Sub BtRSF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtRSF.Click
        DebutChargement()
        Disposer_form(RapportSuiviFinancierClear)
        FinChargement()
    End Sub

    Private Sub BtRAP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtRAP.Click
        DebutChargement()
        Disposer_form(RapportActiviteClear)
        FinChargement()
    End Sub
    Private Sub BtFicheBudgetaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtFicheBudgetaire.Click
        Disposer_form(EditionBudgetaire)
        FinChargement()
    End Sub

    Private Sub BtAllocationBudget_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtAllocationBudget.Click
        Dialog_form(RepartitionMontantConvention)
    End Sub

    Private Sub BtPlanDecaiss_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtPlanDecaiss.Click
        Disposer_form(PlanDecaissement)
        FinChargement()
    End Sub

    Private Sub TileEvaluation_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.TileItemEventArgs) Handles TileEvaluation.ItemClick

        ClearMdi.NavBarControl1.ActiveGroup = ClearMdi.GroupEvaluation
        SplitEvaluation.Visible = True
        SplitEvaluation.Dock = DockStyle.Fill
        TuileDemarrage.Visible = False
        Me.Text = "EVALUATION DU PROJET"
    End Sub

    Private Sub BtFB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtFB.Click
        Dialog_form(Etat_EngBail)
    End Sub

    Private Sub SimpleButton5_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton5.Click
        DebutChargement()
        'Disposer_form(RapportGlobalActivite)
        'Disposer_form(RAPortGbolActivtes)
        Disposer_form(RapportsGlobales)
        FinChargement()
    End Sub

    Private Sub TilePortefeuille_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs)
        Dim URL As String = "http://clearproject.online"
        If DB.Length > 2 Then
            If Mid(DB, 1, 2) = "bd" Then
                URL = "http://" & Mid(DB, 3) & ".clearproject.online"
            End If
        End If
        System.Diagnostics.Process.Start(URL)
    End Sub

    Private Sub TileClearWeb_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs)
        System.Diagnostics.Process.Start("http://pm-projects.net")
    End Sub

End Class