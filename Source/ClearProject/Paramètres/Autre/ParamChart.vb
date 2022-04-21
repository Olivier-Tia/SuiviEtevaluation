Public Class ParamChart

    Private Sub ParamChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        Me.Size = New System.Drawing.Size(200, hauteur - 171)
        Me.Location = New System.Drawing.Point(largeur - 229, 0)

        RdLegOui.Checked = True
        RdStyleBar.Checked = True
        'CoulSerie1.SelectedColor = Color.Blue
        'CoulSerie2.SelectedColor = Color.Green
        'CoulSerie3.SelectedColor = Color.Red
        'CoulSerie4.SelectedColor = Color.Yellow
        RdVue2D.Checked = True
        RdEclairageNon.Checked = True
        ChkAxeX.Checked = True
        ChkAxeY.Checked = True
        RdLibNon.Checked = True
        'CoulTexte.SelectedColor = Color.Navy

        TbTransparence.Value = 0
        LblTransp.Text = "0"
        TbRotation.Value = 22
        LblRotation.Text = "22°"
        TbInclinaison.Value = 30
        LblInclinaison.Text = "30°"
        TbPerspective.Value = 0
        LblPerspective.Text = "0%"
        TbProfColo.Value = 50
        LblProfColo.Text = "50"
        TbProfInterne.Value = 20
        LblProfInterne.Text = "20"
        TbEpaisseMur.Value = 10
        LblEpaisseMur.Text = "10px"



    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtActualiser.Click
        If (ClearChart.Visible = False) Then
            Disposer_form(ClearChart)
        Else
            ClearChart.Dispose()
            Disposer_form(ClearChart)
        End If


    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        ClearChart.Close()
        Me.Close()
    End Sub
    Private Sub RdVue3D_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdVue3D.CheckedChanged
        If (RdVue3D.Checked = True) Then
            GbCouleur.Enabled = True
            GbRotation.Enabled = True
            GbInclinaison.Enabled = True
            GbPerspective.Enabled = True
            GbProfColo.Enabled = True
            GbProfInt.Enabled = True
            GbEpaisMur.Enabled = True
            GbEclairage.Enabled = True
        Else
            GbCouleur.Enabled = False
            GbRotation.Enabled = False
            GbInclinaison.Enabled = False
            GbPerspective.Enabled = False
            GbProfColo.Enabled = False
            GbProfInt.Enabled = False
            GbEpaisMur.Enabled = False
            GbEclairage.Enabled = False
        End If
        If (ClearChart.Visible = True) Then
            Button1_Click(Me, e)
        End If
    End Sub

    
    Private Sub BtImprimerChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImprimerChart.Click

        ClearChart.Chart2.Printing.Print(False)
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        ClearChart.Chart2.Printing.PageSetup()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        ClearChart.Chart2.Printing.PrintPreview()
    End Sub

    Private Sub TbRotation_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TbRotation.Scroll
        LblRotation.Text = TbRotation.Value.ToString & "°"
        Button1_Click(Me, e)
    End Sub

    Private Sub RdLegOui_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdLegOui.CheckedChanged
        If (ClearChart.Visible = True) Then
            Button1_Click(Me, e)
        End If

    End Sub

    Private Sub RdStyleCyl_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdStyleCyl.CheckedChanged
        If (ClearChart.Visible = True) Then
            Button1_Click(Me, e)
        End If
    End Sub

    'Private Sub CoulSerie1_ColorChanged(ByVal sender As Object, ByVal e As KS.Gantt.Dialogs.Controls.ColorChangeArgs) Handles CoulSerie1.ColorChanged
    '    If (ClearChart.Visible = True) Then
    '        Button1_Click(Me, e)
    '    End If
    'End Sub

    'Private Sub CoulSerie2_ColorChanged(ByVal sender As Object, ByVal e As KS.Gantt.Dialogs.Controls.ColorChangeArgs) Handles CoulSerie2.ColorChanged
    '    If (ClearChart.Visible = True) Then
    '        Button1_Click(Me, e)
    '    End If
    'End Sub

    'Private Sub CoulSerie3_ColorChanged(ByVal sender As Object, ByVal e As KS.Gantt.Dialogs.Controls.ColorChangeArgs) Handles CoulSerie3.ColorChanged
    '    If (ClearChart.Visible = True) Then
    '        Button1_Click(Me, e)
    '    End If
    'End Sub

    'Private Sub CoulSerie4_ColorChanged(ByVal sender As Object, ByVal e As KS.Gantt.Dialogs.Controls.ColorChangeArgs) Handles CoulSerie4.ColorChanged
    '    If (ClearChart.Visible = True) Then
    '        Button1_Click(Me, e)
    '    End If
    'End Sub

    'Private Sub CoulTexte_ColorChanged(ByVal sender As Object, ByVal e As KS.Gantt.Dialogs.Controls.ColorChangeArgs) Handles CoulTexte.ColorChanged
    '    If (ClearChart.Visible = True) Then
    '        Button1_Click(Me, e)
    '    End If
    'End Sub

    Private Sub RdCoulTrans_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (ClearChart.Visible = True) Then
            Button1_Click(Me, e)
        End If
    End Sub

    Private Sub RdEclairage1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdEclairage1.CheckedChanged
        If (ClearChart.Visible = True) Then
            Button1_Click(Me, e)
        End If
    End Sub

    Private Sub RdEclairage2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdEclairage2.CheckedChanged
        If (ClearChart.Visible = True) Then
            Button1_Click(Me, e)
        End If
    End Sub
    Private Sub ChkAxeX_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAxeX.CheckedChanged
        If (ClearChart.Visible = True) Then
            Button1_Click(Me, e)
        End If
    End Sub
    Private Sub ChkAxeY_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAxeY.CheckedChanged
        If (ClearChart.Visible = True) Then
            Button1_Click(Me, e)
        End If
    End Sub
    Private Sub RdLibOui_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdLibOui.CheckedChanged
        If (ClearChart.Visible = True) Then
            Button1_Click(Me, e)
        End If
    End Sub
    Private Sub RdLibNon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdLibNon.CheckedChanged
        If (ClearChart.Visible = True) Then
            Button1_Click(Me, e)
        End If
    End Sub
    Private Sub TbInclinaison_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TbInclinaison.Scroll
        If (ClearChart.Visible = True) Then
            LblInclinaison.Text = TbInclinaison.Value.ToString & "°"
            Button1_Click(Me, e)
        End If
    End Sub

    Private Sub TbPerspective_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TbPerspective.Scroll
        If (ClearChart.Visible = True) Then
            LblPerspective.Text = TbPerspective.Value.ToString & "%"
            Button1_Click(Me, e)
        End If
    End Sub

    Private Sub TbProfColo_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TbProfColo.Scroll
        If (ClearChart.Visible = True) Then
            LblProfColo.Text = TbProfColo.Value.ToString
            Button1_Click(Me, e)
        End If
    End Sub

    Private Sub TbProfInterne_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TbProfInterne.Scroll
        If (ClearChart.Visible = True) Then
            LblProfInterne.Text = TbProfInterne.Value.ToString
            Button1_Click(Me, e)
        End If
    End Sub

    Private Sub TbEpaisseMur_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TbEpaisseMur.Scroll
        If (ClearChart.Visible = True) Then
            LblEpaisseMur.Text = TbEpaisseMur.Value.ToString & "px"
            Button1_Click(Me, e)
        End If
    End Sub

    Private Sub TbTransparence_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TbTransparence.Scroll
        If (ClearChart.Visible = True) Then
            LblTransp.Text = TbTransparence.Value.ToString
            Button1_Click(Me, e)
        End If
    End Sub

    
    
   
   
    
End Class