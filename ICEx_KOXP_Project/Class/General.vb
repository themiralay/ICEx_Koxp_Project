Option Explicit On
Imports System.IO
Imports System.Text
Imports System.Security.Principal

Module General
#Region "Get Handle Information"
    Public OsmanliExe As String = ""
    Public Function CheckAdiminstrator() As Boolean
        Return New WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)
    End Function
    Public Function getHandle(ByVal PID As Int32) As IntPtr
        getHandle = OpenProcess(PROCESS_ALL_ACCESS, False, PID)
    End Function
    Public Sub getWindTitle(ByVal fExefile As String, ByVal LstView As ListView)
        LstView.Items.Clear()
        For Each WndP As Process In Process.GetProcesses()
            If (WndP.ProcessName = fExefile & OsmanliExe) Then
                Dim lstItem As ListViewItem = New ListViewItem(New String() {WndP.MainWindowTitle,
                                                                             getCharNameTotHandle(getHandle(WndP.Id)),
                                                                             getServerToHandle(getHandle(WndP.Id)),
                                                                             getCharClassToHandle(getHandle(WndP.Id))})
                LstView.Items.Add(lstItem)
            End If
        Next
    End Sub
    Public Function GetBypassTitle() As String
        For Each WndP As Process In Process.GetProcesses()
            If (WndP.ProcessName = "KnightOnLine") Then
                Return WndP.MainWindowTitle
            End If
        Next
        Return ""
    End Function
    Public Function getStringHandle(ByVal Address As Integer, ByVal GameHandle As Long) As String
        Dim Count1 As Integer = 0, tempInt As Integer = 0
        Dim result As String = ""
        result = ""
        For n = 1 To 50
            ReadProcessMemory(GameHandle, Address + Count1, tempInt, 1, 0)
            If tempInt > 0 Then
                result = result & Chr(tempInt)
                Count1 += 1
            Else
                Return result
            End If
        Next
        Return result
    End Function
    Public Function getLongToHandle(ByVal pAddy As Long, ByVal GameHandle As Long) As Long
        Dim value As Long
        ReadProcessMemory(GameHandle, pAddy, value, 4, 0&)
        Return value
    End Function
    Public Function getServerToHandle(ByVal GameHandle As Long) As String
        If Len(getStringHandle(KO_CHAR_SERV, GameHandle)) > 3 Then
            Return getStringHandle(KO_CHAR_SERV, GameHandle)
        End If
        Return "Bulunamadı"
    End Function
    Public Function getCharNameTotHandle(ByVal GameHandle As String) As String
        Dim chrBase As Long, chrNameBase As Long, chrName As Long, result As String
        chrBase = getLongToHandle(KO_PTR_CHR, GameHandle)
        chrNameBase = getLongToHandle(chrBase + KO_OFF_NAMELEN, GameHandle)
        chrName = chrBase + KO_OFF_NAME
        If chrNameBase > 15 Then
            result = getStringHandle(getLongToHandle(chrName, GameHandle), GameHandle)
        Else
            result = getStringHandle(chrName, GameHandle)
        End If
        If result = "" Then Return "Bulunamadı"
        Return result
    End Function
    Public Function getCharClassToHandle(ByVal GameHandle As String) As String
        Select Case getLongToHandle(getLongToHandle(KO_PTR_CHR, GameHandle) + KO_OFF_CLASS, GameHandle)
            Case 101, 111, 112, 211, 212
                Return "Priest"
            Case 102, 107, 108, 207, 208
                Return "Rogue"
            Case 103, 105, 106, 205, 206
                Return "Warrior"
            Case 104, 109, 110, 209, 210
                Return "Mage"
            Case Else
                Return "Bulunamadı"
        End Select
    End Function
#End Region
#Region "Karışık Kodlar"
    Public Sub setDisableChar(ByRef txtBox As TextBox)
        If (txtBox.Text.IndexOf(";") > 0 Or txtBox.Text.IndexOf("'") > 0) Then
            txtBox.Text = txtBox.Text.Substring(0, txtBox.Text.Length - 1)
            txtBox.SelectionStart = txtBox.TextLength + 1
        End If
    End Sub
    Public Sub checkTextBox(ByRef txtCheck As TextBox, Optional ByVal txtNewVal As Integer = 60)
        If Not (IsNumeric(txtCheck.Text) Or String.IsNullOrEmpty(txtCheck.Text)) Then
            txtCheck.Text = txtNewVal
        End If
    End Sub
    Public Sub goTxt(ByVal BirinciCumle As String, ByVal IkinciCumle As String)
        MessageBox.Show(BirinciCumle & vbNewLine & IkinciCumle, strProjectInfo, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Public Sub ShutDown(ByVal Ko_pid As String)
        TerminateProcess(getHandle(Ko_pid), 0&)
    End Sub
    Public Sub Wait(ByVal xMilisecond As Long)
        Static LastTime As Long
        LastTime = GetTickCount() + xMilisecond
        Do While LastTime > GetTickCount()
            System.Windows.Forms.Application.DoEvents()
        Loop
    End Sub
    Public Sub windStatus(winTitle As String, winStatu As Long) '0 Gizle - 1 Yukarı - 2 Simge - 3 TamEkran - 4 PencereMode - 5 Göster
        Call ShowWindow(FindWindow(vbNullString, winTitle), winStatu)
    End Sub
    Public Function getCoorText(ByVal txtCoor As String, ByVal statuCoor As Integer) As Integer
        On Error Resume Next
        If (statuCoor = 0) Then
            Return Convert.ToInt32(txtCoor.Substring(0, txtCoor.IndexOf(" - ")))
        End If
        Return Convert.ToInt32(txtCoor.Substring(txtCoor.IndexOf(" - ") + 3, txtCoor.Length - txtCoor.IndexOf(" - ") - 3))
    End Function
    Public Function searchText(ByVal strFind As String, ByVal strText As String) As Boolean
        If strText.IndexOf(strFind) > -1 Then Return True
        Return False
    End Function
    Public Function searchListView(ByVal findText As String, ByVal ViewColumn As Integer, ByVal ViewList As ListView) As Boolean
        If ViewColumn > ViewList.Columns.Count Then Return False
        For i As Integer = 0 To ViewList.Items.Count - 1
            If ViewColumn <= 1 Then
                If ViewList.Items.Item(i).Text = findText Then
                    ViewList.Items.Item(i).EnsureVisible()
                    Return True
                End If
            End If
            If ViewColumn >= 1 Then
                If ViewList.Items.Item(i).SubItems(ViewColumn - 1).Text = findText Then
                    ViewList.Items.Item(i).EnsureVisible()
                    Return True
                End If
            End If
        Next
        Return False
    End Function
    Public Function searchList(ByVal strFind As String, ByRef lstSearch As ListBox) As Boolean
        For i As Integer = 0 To lstSearch.Items.Count - 1
            If lstSearch.Items(i).ToString() = strFind Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function searchListIndex(ByVal strFind As String, ByRef lstSearch As ListBox) As Integer
        For i As Integer = 0 To lstSearch.Items.Count - 1
            If lstSearch.Items(i).ToString() = strFind Then
                Return i
            End If
        Next
        Return -1
    End Function
    Public Sub PlayAlarm()
        Dim proc As Process = New Process()
        proc.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory & "/Settings/alarm.mp3"
        proc.Start()
    End Sub
    Public Function convertToMoney(ByVal mny As Integer)
        Dim strMny As String = mny.ToString()
        Dim rowCnt As Integer = strMny.Length Mod 3
        Dim txtMny As String = strMny.Substring(0, rowCnt)
        If (strMny.Length < 4) Then
            Return strMny
        ElseIf (rowCnt = 0) Then
            txtMny += strMny.Substring(0, 3)
            rowCnt = 3
        End If
        For i As Integer = rowCnt To strMny.Length - 1 Step 3
            txtMny += "." & strMny.Substring(i, 3)
        Next
        Return txtMny
    End Function
    Public Function StringToHex(ByVal StrToHex As String) As String
        Dim strTemp As String
        Dim strReturn As String
        strReturn = ""
        Dim i As Long
        For i = 1 To Len(StrToHex)
            strTemp = Hex$(Asc(Mid$(StrToHex, i, 1)))
            If Len(strTemp) = 1 Then strTemp = "0" & strTemp
            strReturn = strReturn & strTemp
        Next i
        StringToHex = strReturn
    End Function
    Public Function RandomString() As String
        Dim prng As New Random
        Const minCH As Integer = 15 'minimum chars in random string
        Const maxCH As Integer = 35 'maximum chars in random string
        Const randCH As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"

        Dim sb As New System.Text.StringBuilder
        For i As Integer = 1 To prng.Next(minCH, maxCH + 1)
            sb.Append(randCH.Substring(prng.Next(0, randCH.Length), 1))
        Next
        Return sb.ToString()
    End Function
#End Region
#Region "Mouse Click Event"
    Public Sub clickMouseLeft(ByVal CorX As Integer, ByVal CorY As Integer)
        SetCursorPos(CorX, CorY)
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
    End Sub
#End Region
End Module
