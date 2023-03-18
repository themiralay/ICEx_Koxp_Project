Option Explicit On
Imports System.IO
Imports System.Text.RegularExpressions
'Imports AutoItX3Lib
Module AutoLogin
    Public autoGameStatu As Boolean = False
    Public autoLoginServer As Integer = -1
    Public autoLoginChannel As Integer = -1
    Public autoLoginSlot As Integer = -1
    Public autoLoginSlotRow As Integer = 0
    Public autoLoginPath As String = ""
    Public LauncherStatu As Boolean = False
    Public autoLoginRow As Integer = 0
    Public autoLoginTime As Long
    'Public kSend As New AutoItX3Lib.AutoItX3
    Public Sub runSteam(ByVal launcherPath As String, Optional ByVal uIDSteam As String = "", Optional ByVal uPassSteam As String = "", Optional ByVal pSteam As Integer = 2)
        Dim argSteam As String
        Dim proc As Process = New Process()
        proc.StartInfo.FileName = launcherPath
        Select Case pSteam
            Case 0
                argSteam = "-login " & uIDSteam & " " & uPassSteam
                LauncherStatu = False
                proc.StartInfo.Arguments = argSteam
                proc.StartInfo.CreateNoWindow = False
                proc.Start()
            Case 1
                argSteam = "-applaunch 389430"
                LauncherStatu = True
                proc.StartInfo.Arguments = argSteam
                proc.StartInfo.CreateNoWindow = False
                proc.Start()
            Case Else
                killProcess("Steam")
                LauncherStatu = False
        End Select
    End Sub
    Public Sub runKnightOnline(ByVal launcherPath As String, Optional ByVal IsOpen As Boolean = True)
        If (IsOpen) Then
            If (BotForSteam = True) Then
                runSteam(launcherPath, , , 1)
            Else
                Dim proc As Process = New Process()
                proc.StartInfo.FileName = launcherPath
                proc.Start()
            End If
        Else
            If (BotForSteam = True) Then
                runSteam(launcherPath)
            End If
            killProcess("KnightOnLine")
            killProcess("xm")
            killProcess("xxd-0")
            disableX3(launcherPath)
        End If
    End Sub
    Public Sub getChannel(ByVal cmbServer As ComboBox, ByRef cmbhannel As ComboBox, ByVal Steam As Boolean)
        cmbhannel.Items.Clear()
        If (Steam = False) Then
            Select Case cmbServer.SelectedIndex
                Case 5
                    cmbhannel.Items.Add("Destan 1")
                    cmbhannel.Items.Add("Destan 2")
                Case Else
                    cmbhannel.Items.Add(cmbServer.Text & " 1")
            End Select
        Else
            Select Case cmbServer.SelectedIndex
                Case 0
                    cmbhannel.Items.Add("Pathos 1")
                    cmbhannel.Items.Add("Pathos 2")
                Case Else
                    cmbhannel.Items.Add(cmbServer.Text & " 1")
            End Select
        End If
        cmbhannel.SelectedIndex = 0
    End Sub
    Public Function checkProcesstoName(ByVal prcName As String)
        For Each WndP As Process In Process.GetProcesses()
            If (WndP.ProcessName = prcName) Then Return True
        Next
        Return False
    End Function
    Public Function checkProcesstoTitle(ByVal prcTitle As String)
        For Each WndP As Process In Process.GetProcesses()
            If (WndP.MainWindowTitle = prcTitle) Then Return True
        Next
        Return False
    End Function
    Public Sub killProcess(ByVal pName As String)
        Try
            For Each WndP As Process In Process.GetProcesses()
                If (WndP.ProcessName = pName) Then WndP.Kill()
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub KillKnightAll()
        killProcess("Launcher")
        killProcess("KnightOnLine")
        killProcess("xm")
        killProcess("xxd-0")
    End Sub
    Public Sub disableX3(ByVal GamePath As String)
        KillKnightAll()
        Sleep(200)
        Dim logPath As String
        If (GamePath.IndexOf("Steam") > -1) Then
            logPath = GamePath.Replace("Steam.exe", "") & "steamapps\common\Knight Online\"
        Else
            logPath = GamePath.Replace("Launcher.exe", "")
        End If
        Dim xPath As String = "C:\Windows\xhunter1.sys"
        Dim x2Path As String = "C:\Windows\xspirit.sys"
        Dim xLogPath As String = logPath & "XIGNCODE\xigncode.log"
        Dim xemPath As String = logPath & "XIGNCODE\xxd-0.xem"
        Dim klgPath As String = logPath & "log.klg"
        Dim scPath As String = logPath & "Scheduler.ini"
        Dim infPath As String = logPath & "info"
        Dim koPatchData As String = logPath & "patch_data.zip"
        Dim deathLog As String = logPath & "DeathLog.klg"
        Try
            If (Directory.Exists(infPath)) Then Directory.Delete(infPath, True)
            If (File.Exists(xLogPath)) Then Kill(xLogPath)
            If (File.Exists(xemPath) And checkProcesstoName("xxd-0.xem") = False) Then Kill(xemPath)
            If (File.Exists(klgPath)) Then Kill(klgPath)
            If (File.Exists(scPath)) Then Kill(scPath)
            If (File.Exists(xLogPath)) Then Kill(xLogPath)
            If (File.Exists(koPatchData)) Then Kill(koPatchData)
            If (File.Exists(deathLog)) Then Kill(deathLog)

            If (File.Exists(xPath)) Then
                SetAttr(xPath, FileAttribute.Normal)
                Kill(xPath)
            End If
            If (File.Exists(x2Path)) Then
                SetAttr(x2Path, FileAttribute.Normal)
                Kill(x2Path)
            End If

            File.Create(xPath)
            SetAttr(xPath, FileAttribute.ReadOnly)
            File.Create(x2Path)
            SetAttr(x2Path, FileAttribute.ReadOnly)
            Sleep(200)
            PatchDataDisable(logPath)
            For Each foundFile As String In Directory.GetFiles(logPath, "*.dmp", SearchOption.TopDirectoryOnly)
                File.Delete(foundFile)
            Next
            Sleep(200)
        Catch ex As Exception
            Sleep(200)
        End Try
    End Sub

    Public Sub getGameWindow(Optional ByVal pTitle As String = "Knight OnLine Client")
        Dim hwndGame As Long = FindWindow(vbNullString, pTitle)
        SetActiveWindow(hwndGame)
        SetForegroundWindow(hwndGame)
        SetFocus(hwndGame)
    End Sub
    Public Sub runGameToLauncher()
        Dim hWndButton As Long
        hWndButton = FindWindowEx(FindWindow("#32770", " AUTO UPGRADE"), 0&, "Button", "start")
        Call SendMessage(hWndButton, &HF5, 0&, 0&)
    End Sub
    Public Function checkKO() As Boolean
        Return Convert.ToBoolean(FindWindow(vbNullString, "Knight OnLine Client"))
    End Function
    Public Function GetCaption() As String
        Dim Caption As New System.Text.StringBuilder(256)
        Dim hWnd As IntPtr = GetForegroundWindow()
        GetWindowText(hWnd, Caption, Caption.Capacity)
        Return Caption.ToString()
    End Function
End Module
