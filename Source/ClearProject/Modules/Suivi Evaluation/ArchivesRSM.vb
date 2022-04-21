Imports System.IO

Public Class ArchivesRSM
    Dim RappSelect As String = ""

    Private Sub ArchivesRSM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        LstRapport.Items.Clear()
        For Each directory As String In My.Computer.FileSystem.GetDirectories(line & "\RSM\" & ProjetEnCours)
            Dim dirinfo As New DirectoryInfo(directory)
            Dim LVI As New ListViewItem
            LVI.Text = (dirinfo.Name).Replace("_", "/")
            LVI.ImageIndex = 0
            LstRapport.Items.Add(LVI)
            LstRapport.Activation = ItemActivation.OneClick
            LstRapport.LabelEdit = False
        Next

    End Sub

    Private Sub LstRapport_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles LstRapport.ItemSelectionChanged
        If LstRapport.SelectedIndices.Count = 0 Then Exit Sub
        Dim Dossier As String = LstRapport.Items(LstRapport.FocusedItem.Index).Text

        GridPart.Rows.Clear()
        For Each directory As String In My.Computer.FileSystem.GetDirectories(line & "\RSM\" & ProjetEnCours & "\" & Dossier.Replace("/", "_"))
            Dim dirinfo As New DirectoryInfo(directory)
            Dim n As Decimal = GridPart.Rows.Add()
            GridPart.Rows.Item(n).Cells(0).Value = Dossier.Replace("/", "_")
            GridPart.Rows.Item(n).Cells(1).Value = dirinfo.Name
        Next
    End Sub

    Private Sub GridPart_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridPart.CellClick
        Dim nLg As Decimal = GridPart.CurrentRow.Index
        RappSelect = GridPart.Rows.Item(nLg).Cells(0).Value.ToString & "\" & GridPart.Rows.Item(nLg).Cells(1).Value.ToString
    End Sub

    Private Sub BtImprimer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtImprimer.Click

        If (RappSelect <> "") Then
            Dim monDossier As String = line & "\RSM\" & ProjetEnCours & "\" & RappSelect & "\"

            Dim cpt As Decimal = 1

            While (File.Exists(monDossier & "R" & cpt & ".pdf") Or File.Exists(monDossier & "R" & cpt & ".jpg"))

                Dim nomFich As String = ""
                If (File.Exists(monDossier & "R" & cpt & ".pdf") = True) Then
                    nomFich = monDossier & "R" & cpt & ".pdf"
                ElseIf (File.Exists(monDossier & "R" & cpt & ".jpg") = True) Then
                    nomFich = monDossier & "R" & cpt & ".jpg"
                End If

                Dim monProcess As New ProcessStartInfo
                monProcess.FileName = nomFich

                ' Use these arguments for the process
                monProcess.Arguments = "a -tgzip """ & nomFich & """ -mx=9"

                ' Use a hidden window
                monProcess.WindowStyle = ProcessWindowStyle.Hidden

                ' Start the process
                Process.Start(monProcess)

                cpt = cpt + 1
            End While

        End If

    End Sub

    Private Sub BtFermer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtFermer.Click
        Me.Close()
    End Sub

    Private Sub BtImprimer_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtImprimer.MouseDown
        BtImprimer.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub BtImprimer_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtImprimer.MouseUp
        BtImprimer.BorderStyle = BorderStyle.None
    End Sub

    Private Sub BtFermer_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtFermer.MouseDown
        BtFermer.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub BtFermer_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtFermer.MouseUp
        BtFermer.BorderStyle = BorderStyle.None
    End Sub
End Class