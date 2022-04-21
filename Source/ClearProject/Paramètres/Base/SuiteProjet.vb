﻿Public Class SuiteProjet 

    Private Sub BtEnregistrer_Click(sender As System.Object, e As System.EventArgs) Handles BtEnregistrer.Click
        Try
            If txtCompteContribuable.Text.Length = 0 Then
                SuccesMsg("Veuillez entrer le compte contribuable.")
                Exit Sub
            End If
            If txtRegistreCommerce.Text.Length = 0 Then
                SuccesMsg("Veuillez entrer le registre de commerce.")
                Exit Sub
            End If
            If txtCentreImpot.Text.Length = 0 Then
                SuccesMsg("Veuillez indiquer le centre des impôts.")
                Exit Sub
            End If
            If Not IsDate(txtdateRegistreCommerce.Text) Then
                SuccesMsg("Veuillez entrer la date du registre de commerce.")
                Exit Sub
            End If

            If cmbFormeJuridique.SelectedIndex = -1 Then
                SuccesMsg("Veuillez selectionner la forme juridique.")
                Exit Sub
            End If

            If Val(txtCapital.Text) = 0 Then
                SuccesMsg("Veuillez entrer le montant du capital.")
                Exit Sub
            End If

            If txtNumEmployeur.Text.Length = 0 Then
                SuccesMsg("Veuillez entrer le numéro employeur.")
                Exit Sub
            End If

            If txtCodeActivite.Text.Length = 0 Then
                SuccesMsg("Veuillez entrer le code de l'activité.")
                Exit Sub
            End If

            If txtCodeEtablissement.Text.Length = 0 Then
                SuccesMsg("Veuillez entrer le code de l'établissement.")
                Exit Sub
            End If

            If txtActivites.Text.Length = 0 Then
                SuccesMsg("Veuillez entrer les activités.")
                Exit Sub
            End If

            'convertion de la date en date anglaise
            Dim str(3) As String
            str = txtdateRegistreCommerce.Text.Split("/")
            Dim tempdt As String = String.Empty
            For j As Integer = 2 To 0 Step -1
                tempdt += str(j) + "-"
            Next
            tempdt = tempdt.Substring(0, 10)

            Dim madate = Now
            Dim dd = madate.ToString("H:mm:ss")

            Dim action As String = ""


            query = "select count(*) from t_suiteprojet"
            Dim nbre = Val(ExecuteScallar(query))
            If nbre = 0 Then

                action = "Ajout d'information supplémentaire du projet"

                query = "insert into t_suiteprojet values (NULL,'" + ProjetEnCours + "','" + txtCompteContribuable.Text + "','" + txtRegistreCommerce.Text + "','" + tempdt + "','" + EnleverApost(txtCentreImpot.Text) + "','" + EnleverApost(cmbFormeJuridique.Text) + "','" + txtCapital.Text + "','" + txtNumEmployeur.Text + "','" + txtCodeActivite.Text + "','" + txtCodeEtablissement.Text + "','" + EnleverApost(txtActivites.Text) + "')"
                ExecuteNonQuery(query)

                'query = "insert into t_historique values (NULL,'" + ProjetEnCours + "','" + NomUtilisateur + "','" + EnleverApost(action) + "','" + madate + "','" + dd + "')"
                'ExecuteNonQuery(query)

                SuccesMsg("Enregistrement effectué avec succès.")

            Else

                action = "Modification des données de l'information supplémentaire du projet"

                query = "update t_suiteprojet set cc='" + txtCompteContribuable.Text + "',rc='" + txtRegistreCommerce.Text + "',daterc='" + tempdt + "',CentreImpot='" + EnleverApost(txtCentreImpot.Text) + "',Formejuridiq='" + EnleverApost(cmbFormeJuridique.Text) + "',Capital='" + txtCapital.Text + "', NumEmployeur='" + txtNumEmployeur.Text + "',CodeActivite='" + txtCodeActivite.Text + "', CodeEtab='" + txtCodeEtablissement.Text + "', activitep ='" + EnleverApost(txtActivites.Text) + "'"
                ExecuteNonQuery(query)

                'sql = "insert into t_historique values (NULL,'" + ProjetEnCours + "','" + NomUtilisateur + "','" + EnleverApost(action) + "','" + madate + "','" + dd + "')"
                'ExecuteNonQuery(query)

                SuccesMsg("Modification effectué avec succès.")
                Me.Close()
            End If

        Catch ex As Exception
            FailMsg(ex.ToString())
        End Try
    End Sub

    Private Sub SuiteProjet_Load(sender As System.Object, e As System.EventArgs)  Handles MyBase.Load
        Me.Icon = My.Resources.Logo_ClearProject_Valide

        query = "select * from t_suiteprojet"
        dt = ExcecuteSelectQuery(query)
        For Each rwx As DataRow In dt.Rows
            txtCompteContribuable.Text = rwx("CC").ToString
            txtRegistreCommerce.Text = rwx("RC").ToString
            txtCentreImpot.Text = MettreApost(rwx("CentreImpot").ToString)
            txtdateRegistreCommerce.Text = CDate(rwx("DateRC")).ToString("dd/MM/yyyy")
            cmbFormeJuridique.Text = rwx("FormeJuridiq").ToString
            txtCapital.Text = CDec(rwx("Capital").ToString)
            txtNumEmployeur.Text = rwx("NumEmployeur").ToString
            txtCodeActivite.Text = rwx("CodeActivite").ToString
            txtCodeEtablissement.Text = rwx("CodeEtab").ToString
            txtActivites.Text = MettreApost(rwx("activitep").ToString)
        Next

    End Sub
End Class