﻿Public Class FadeTransition

    Shared FadeObj
    Shared FadeStartOp
    Shared FadeStopOp
    Shared FadeTickCount
    Shared FadeDirection
    Shared Modifier
    Shared FadeCloseOnFinish
    Shared Bounds As Decimal
    Shared FadeTickCumulation
    Shared FadeTimer As New Timer
    Shared SW As New Stopwatch


    ''' <summary>
    ''' Fades the target form into or out of view
    ''' </summary>
    ''' <param name="targetForm">Choose the target form</param>
    ''' <param name="startopacity">Set start opacity as a decimal between 0 and 1</param>
    ''' <param name="endopacity">Set end opacity as a decimal between 0 and 1</param>
    ''' <param name="duration">Set duration of transition in milliseconds</param>
    Public Shared Sub FadeForm(targetForm As Form, startopacity As Decimal, endopacity As Decimal, duration As Integer, Optional closeonFinish As Boolean = False)

        If duration <= 30 Then Modifier = 10
        If duration > 30 And duration < 1000 Then Modifier = 25
        If duration >= 1000 Then Modifier = 50

        FadeTimer.Interval = Modifier

        FadeObj = targetForm
        FadeObj.opacity = startopacity
        FadeStartOp = startopacity
        FadeStopOp = endopacity
        FadeCloseOnFinish = closeonFinish
        FadeTickCount = (endopacity - startopacity) / (duration / Modifier)

        If endopacity - startopacity < 0 Then
            Bounds = -FadeTickCount
            FadeTickCumulation = -FadeTickCount
        Else
            Bounds = FadeTickCount
            FadeTickCumulation = FadeTickCount
        End If
        FadeObj.Show()
        AddHandler FadeTimer.Tick, AddressOf FadeTimer_Tick
        FadeTimer.Start()

    End Sub

    Shared Sub FadeTimer_Tick(sender As Object, e As EventArgs)
        'If FadeObj.opacity > 0 Then FadeObj.show
        'If FadeObj.opacity = 0 Then FadeObj.hide
        If FadeObj.opacity < FadeStopOp + Bounds And FadeObj.opacity > FadeStopOp - Bounds Then
            FadeTimer.Stop()
            FadeObj.opacity = FadeStopOp
            RemoveHandler FadeTimer.Tick, AddressOf FadeTimer_Tick

            If FadeObj.opacity = 0 And FadeCloseOnFinish = True Then FadeObj.Close
            If FadeObj.opacity = 0 And FadeCloseOnFinish = False Then FadeObj.hide
        Else
            FadeObj.opacity += FadeTickCount
            FadeTickCumulation += FadeTickCount
        End If

    End Sub

End Class


Public Class UnfurlTransition

    Shared UnfurlObj As Control
    Shared UnfurlStartWidth As Integer
    Shared UnfurlEndWidth As Integer
    Shared UnfurlDuration As Integer
    Shared UnfurlTimer As New Timer
    Shared Modifier As Decimal = 16 + 2 / 3
    Shared UnfurlTickcount
    Shared Bounds As Decimal

    Public Shared Sub UnfurlControl(target As Control, startwidth As Integer, endwidth As Integer, duration As Integer)
        If endwidth - startwidth <> 0 Then

            UnfurlTimer.Interval = Modifier

            UnfurlObj = target
            UnfurlStartWidth = startwidth
            UnfurlEndWidth = endwidth
            UnfurlDuration = duration
            UnfurlTickcount = ((endwidth - startwidth)) / (duration / Modifier)
            If endwidth - startwidth < 0 Then
                Bounds = -UnfurlTickcount

            Else
                Bounds = UnfurlTickcount
            End If


            UnfurlObj.Width = startwidth
            UnfurlObj.Show()
            AddHandler UnfurlTimer.Tick, AddressOf UnfurlTimer_Tick
            UnfurlTimer.Start()

        End If


    End Sub


    Shared Sub UnfurlTimer_Tick(sender As Object, e As EventArgs)
        If UnfurlObj.Width < UnfurlEndWidth + Bounds And UnfurlObj.Width > UnfurlEndWidth - Bounds Then

            UnfurlTimer.Stop()

            UnfurlObj.Width = UnfurlEndWidth

            If UnfurlObj Is Compact.topbar_dirchooserContainer Then
                Compact.buttonQueryCompact.Visible = True
                Compact.panel_topBar.Height = 135
                Compact.topbar_dirchooserContainer.Location = New Point(44, 69)
            End If

            RemoveHandler UnfurlTimer.Tick, AddressOf UnfurlTimer_Tick
        Else


            UnfurlObj.Width += UnfurlTickcount


        End If
    End Sub


End Class

