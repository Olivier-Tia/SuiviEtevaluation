Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Math
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Module SuperModule
    Public Function DateSansJourWeekEnd(ladate As Date, nbreJour As Decimal) As String
        If nbreJour > 0 Then
            If ladate.DayOfWeek = DayOfWeek.Saturday Then
                ladate = ladate.AddDays(1)
                nbreJour += 1
            ElseIf ladate.DayOfWeek = DayOfWeek.Sunday Then
                nbreJour += 1
            End If
            While (nbreJour <> 0)
                ladate = ladate.AddDays(1)
                If ladate.DayOfWeek <> DayOfWeek.Saturday And ladate.DayOfWeek <> DayOfWeek.Sunday Then
                    nbreJour -= 1
                End If
            End While
            Return ladate.ToShortDateString
        Else
            Return ladate.ToShortDateString()
        End If
    End Function
    Public Function NbreJourSansJourWeekEnd(date1 As Date, date2 As Date) As Decimal
        If date1 > date2 Then 'Date1 est superieur a Date2

            Dim cpte = 0
            If date2.DayOfWeek = DayOfWeek.Saturday Then
                date2 = date2.AddDays(1)
                cpte -= 1
            ElseIf date2.DayOfWeek = DayOfWeek.Sunday Then
                cpte -= 1
            End If

            While (date1 > date2)
                date2 = date2.AddDays(1)
                If date2.DayOfWeek <> DayOfWeek.Saturday And date2.DayOfWeek <> DayOfWeek.Sunday Then
                    cpte += 1
                End If
            End While
            Return cpte

        ElseIf date1 = date2 Then 'Date1 est egal a Date2

            Return 0

        Else 'Date2 est superieur a Date1

            Dim cpte = 0
            If date1.DayOfWeek = DayOfWeek.Saturday Then
                date1 = date1.AddDays(1)
                cpte -= 1
            ElseIf date1.DayOfWeek = DayOfWeek.Sunday Then
                cpte -= 1
            End If

            While (date2 > date1)
                date1 = date1.AddDays(1)
                If date1.DayOfWeek <> DayOfWeek.Saturday And date1.DayOfWeek <> DayOfWeek.Sunday Then
                    cpte += 1
                End If
            End While
            Return cpte

        End If
    End Function
    <System.Runtime.CompilerServices.Extension()>
    Public Sub Invoke(ByVal control As Control, ByVal action As Action)
        If control.InvokeRequired Then
            control.Invoke(New MethodInvoker(Sub() action()), Nothing)
        Else
            action.Invoke()
        End If
    End Sub
    <System.Runtime.CompilerServices.Extension()>
    Public Sub SetEnabled(ByVal ctl As Control, ByVal enabled As Boolean)
        If ctl.InvokeRequired Then
            ctl.BeginInvoke(New Action(Of Control, Boolean)(AddressOf SetEnabled), ctl, enabled)
        Else
            ctl.Enabled = enabled
        End If
    End Sub
    <System.Runtime.CompilerServices.Extension()>
    Public Sub SetVisible(ByVal ctl As Control, ByVal enabled As Boolean)
        If ctl.InvokeRequired Then
            ctl.BeginInvoke(New Action(Of Control, Boolean)(AddressOf SetEnabled), ctl, enabled)
        Else
            ctl.Visible = enabled
        End If
    End Sub
    <System.Runtime.CompilerServices.Extension()>
    Public Sub SetText(ByVal ctl As Control, ByVal Text As String)
        If ctl.InvokeRequired Then
            ctl.BeginInvoke(New Action(Of Control, String)(AddressOf SetText), ctl, Text)
        Else
            ctl.Text = Text
        End If
    End Sub
End Module