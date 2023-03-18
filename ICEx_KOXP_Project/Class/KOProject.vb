Option Explicit On
Imports System.Runtime.InteropServices
Imports System.Diagnostics.Debug
Imports System.Text
Imports System.IO
Imports System.Security.Permissions
Imports System.Reflection
Imports System.Threading
Imports System.StackOverflowException
Imports PVOID = System.IntPtr
Public Class KOProject
#Region "Tanımlamalar"
    Public Ko_Handle As Long
    Public Ko_Title As String
    Public Ko_Pid As Long
    Public RecvHandle As Long
    Public CodeBytes As Long
    Public PacketBytes As Long
    Public RecvMSSlot As Long
    Public RecvHKSlot As Long
    Public RecvBoxSlot As Long
    Public SpeedSlot As Long
    Public NormalAdress As Long
    Public ByteMob_Base As Long
    Public AutoLoginBytes As Long
    Public ItemIntID(45) As String
    Public ItemINNID(191) As Long
    Public ExITID(45) As String

    Public PartyUserID(8) As String
    Public PartyUserTargetID(8) As Integer
    Public PartyUserHP(8) As Integer
    Public PartyUserMaxHP(8) As Integer
    Public PartyUserHPFark(8) As Integer
    Public PartyUserCure(8) As Long
    Public PartyUserName(8) As String
    Public PartyUserDisease(8) As Boolean
    Public PartyUserX(8) As Integer
    Public PartyUserY(8) As Integer
    Public PartyUserZ(8) As Integer
    Public PartyUserUzaklık(8) As Integer
    Public PartyBuffHp(8) As Integer
    Public PartyBuffHpTime As Long
    Public PartyRestHp(8) As Integer
    Public PartyBuffSend(8) As Boolean
    Public PartyBuffString(8) As String

    Public HookPacket As Integer
    Public StatPaketTimer As Long
    Public zMobName As String
    Public zMobID As Long
    Public zMobX As Long
    Public zMobY As Long
    Public zMobZ As Long
    Public zMobDeadID As Long
    Public zMobDistance As Long
    Public zMobHP As Long
    Public zMobBase As Long
    Public zMobStatu As Integer
    Public zMobMove As Integer
    Public zLastMobID As Long
    Public WarSkill(0 To 46) As Integer
    Public RogueSkill(0 To 50) As Integer
    Public MageSkill(0 To 66) As Integer
    Public PriestSkill(0 To 32) As Integer
    Public PartySkill(0 To 100) As Integer
    Public ChaosSkill(0 To 10) As Integer
    Public PackSira(0 To 9999) As Long
    Public PackTick(0 To 9999) As Long
    Public MobCount As Integer, Mob(999) As String, MobDistance(999) As Integer, MobCor(999) As String, MobSwap, MobCoSwap As String, DisSwap As Integer, IlkArcheryRogue As Integer
    Public LastMobID As Integer = 0
    Public RepairActive As Boolean = False, rprText As String, useGateFunc As Boolean, goSlotActive As Boolean = False, goSlotText As String, useRespawnFunc As Boolean, rprAttackTime As Integer
    Public upBotActive As Boolean = False, upBotText As String, blueQuest As String, blueTime As Long, totalSC As Integer = 0, PartyTime As Long, RespawnTimer As Long, BacktoSlot As Boolean
    Public MobDissAttack As Integer = 55
    Public ArchDissAttack As Integer = 3
    Public SeriTimer As Single = 0.7
    Public SeriHiz As Long = 16384
    Public DefaultTimer As Single = 1.5
    Public DefaultHiz As Long = 16256
    Public NormalAttack As Boolean
    Public NormalSkill As Boolean
    Public RogueNormalSkillTimer As Long
    Public RogueArrowCombo As Integer = 0
    Public WaitAttack As Integer = 0
    Public SummonStart As Boolean
    Public SummonRow As Integer
#End Region
#Region "Bağlanma Fonksiyonları"
    Public Function AttachKO(ByVal hook As Boolean, Optional ByVal bypass As Boolean = False) As Boolean
        AttachKO = False
        If FindWindow(vbNullString, Ko_Title) Then
            GetWindowThreadProcessId(FindWindow(vbNullString, Ko_Title), Ko_Pid)
            Ko_Handle = OpenProcess(PROCESS_ALL_ACCESS, False, Ko_Pid)
            If Ko_Handle = 0 Or Ko_Pid = 0 Then
                goTxt("Handle Oluşturulamadı..", "Lütfen oyunu bypass edip tekrar deneyiniz..")
                AttachKO = False
                Exit Function
            End If
            If (bypass) Then
                'x3Disable()
                Return True
            End If
            For i As Integer = 0 To 5
                AutoLoginBytes = VirtualAllocEx(Ko_Handle, 0, 1024, MEM_COMMIT, PAGE_READWRITE)
                CodeBytes = VirtualAllocEx(Ko_Handle, 0, 1024, MEM_COMMIT, PAGE_READWRITE)
                PacketBytes = VirtualAllocEx(Ko_Handle, 0, 1024, MEM_COMMIT, PAGE_READWRITE)
                RecvHKSlot = VirtualAllocEx(Ko_Handle, 0, 1024, MEM_COMMIT, PAGE_READWRITE)
                RecvMSSlot = VirtualAllocEx(Ko_Handle, 0, 1024, MEM_COMMIT, PAGE_READWRITE)
                RecvBoxSlot = VirtualAllocEx(Ko_Handle, 0, 1024, MEM_COMMIT, PAGE_READWRITE)
                ByteMob_Base = VirtualAllocEx(Ko_Handle, 0, 1024, MEM_COMMIT, PAGE_READWRITE)
                SpeedSlot = VirtualAllocEx(Ko_Handle, 0, 1024, MEM_COMMIT, &H40&)
                NormalAdress = VirtualAllocEx(Ko_Handle, 0, 1024, MEM_COMMIT, PAGE_READWRITE)
            Next
            If (hook) Then getHook()
            GorevKod()
            Return True
        End If
    End Function
#End Region
#Region "XIGNCODE Disable"
    Public Sub x3Disable()
        Dim adr1, adr2, adr3, adr4, CreateFileADDR, ReadFileADDR, SetFilePointerADDR As PVOID
        adr1 = GetProcAddress(GetModuleHandle("kernel32.dll"), "CreateToolhelp32Snapshot")
        adr2 = GetProcAddress(GetModuleHandle("kernelbase.dll"), "CreateToolhelp32Snapshot")
        adr3 = GetProcAddress(GetModuleHandle("sechost.dll"), "StartServiceA")
        adr4 = GetProcAddress(GetModuleHandle("sechost.dll"), "StartServiceW")
        If (adr3 = 0) Then
            adr3 = GetProcAddress(GetModuleHandle("advapi32.dll"), "StartServiceA")
            adr4 = GetProcAddress(GetModuleHandle("advapi32.dll"), "StartServiceW")
        End If
        If (BotForSteam = False) Then
            InjectPatch(adr1, "33C0C20800")
            InjectPatch(adr2, "33C0C20800")
        End If
        InjectPatch(adr3, "B001C20C00")
        InjectPatch(adr4, "B001C20C00")

        CreateFileADDR = GetProcAddress(LoadLibrary("kernel32.dll"), "CreateFileA")
        ReadFileADDR = GetProcAddress(LoadLibrary("kernel32.dll"), "ReadFile")
        SetFilePointerADDR = GetProcAddress(LoadLibrary("kernel32.dll"), "SetFilePointer")
        SetFilePointerADDR = GetProcAddress(LoadLibrary("kernel32.dll"), "SetFilePointer")

        WriteProcessMemory(Ko_Handle, KO_BYPASS, CreateFileADDR, 4, 0&)
        WriteProcessMemory(Ko_Handle, KO_BYPASS + &H8, ReadFileADDR, 4, 0&)
        WriteProcessMemory(Ko_Handle, KO_BYPASS_ADR, CreateFileADDR, 4, 0&)
        WriteProcessMemory(Ko_Handle, KO_BYPASS_ADR + &H8, ReadFileADDR, 4, 0&)
        WriteProcessMemory(Ko_Handle, KO_BYPASS_ADR + &H16, SetFilePointerADDR, 4, 0&)

    End Sub
#End Region
#Region "HookRecv"
    Public Sub openRecv()
        Dim pHook As String, ph() As Byte
        pHook = "558BEC81C4D8FDFFFF53565760FF750CFF7508B8" + ADWORD(KO_RECV_FNC) + "FFD0618B45088B400833D28A1083FA2375348D8DF0FEFFFF8D95F1FEFFFF894DFC8B4003C685F0FEFFFF248902608B0D" + ADWORD(KO_PTR_PKT) + "6A05FF75FCB8" + ADWORD(KO_SND_FNC) + "FFD061E9AA00000033C98A0883F9240F859D0000008B50018955F8BEC8F040008DBDD8FEFFFFB906000000F3A533DB83C00633C98D95D8FEFFFF03C38B1883C006891A4183C20483F90672F066C745F6000033FF8DB5D8FEFFFF833E00744A8D85D8FDFFFF8D95D9FDFFFF8945F08D85DDFDFFFFC685D8FDFFFF268B4DF8890A8D8DE1FDFFFF8B168910668B5DF666891966FF45F6608B0D" + ADWORD(KO_PTR_PKT) + "6A0BFF75F0B8" + ADWORD(KO_SND_FNC) + "FFD0614783C60483FF0672A85F5E5B8BE55DC20800"
        ph = ConvHEX2ByteArray(pHook)
        WriteByteArray(RecvBoxSlot, ph, UBound(ph) - LBound(ph) + 1)
        WriteLong(KO_RECV_PTR, RecvBoxSlot)
    End Sub
    Public Sub closeRecv()
        WriteLong(KO_RECV_PTR, KO_RECV_FNC)
    End Sub
    Public Sub getHook()
        Dim RecvMailSlot As String
        RecvMailSlot = "\\.\mailslot\usknightonline" & ADWORD(GetTickCount, 4)
        RecvHandle = CreateMailslot(RecvMailSlot, 0, 50, 0)
        KO_RECV_PTR = ReadLong(ReadLong(KO_PTR_DLG - &H14)) + &H8
        KO_RECV_FNC = ReadLong(KO_RECV_PTR)
        Console.WriteLine("KO_RECV_PTR : " & Hex(KO_RECV_PTR))
        Console.WriteLine("KO_RECV_FNC : " & Hex(KO_RECV_FNC))
        recvHook(RecvMailSlot)
    End Sub
    Public Sub writeMailSlot(MailSlotName As String)
        Dim pHook As String, p() As Byte, ph() As Byte, CF As Long, WF As Long, CH As Long
        CF = GetProcAddress(GetModuleHandle("kernel32.dll"), "CreateFileA")
        WF = GetProcAddress(GetModuleHandle("kernel32.dll"), "WriteFile")
        CH = GetProcAddress(GetModuleHandle("kernel32.dll"), "CloseHandle")
        p = ConvHEX2ByteArray(StringToHex(MailSlotName))
        WriteByteArray(RecvMSSlot + &H400, p, UBound(p) - LBound(p) + 1)
        pHook = "558BEC83C4F433C08945FC33D28955F86A0068800000006A036A006A01680000004068" & ADWORD(RecvMSSlot + &H400) & "E8" & ADWORD(getCallDiff(RecvMSSlot + &H27, CF)) & "8945F86A008D4DFC51FF750CFF7508FF75F8E8" & ADWORD(getCallDiff(RecvMSSlot + &H3E, WF)) & "8945F4FF75F8E8" & ADWORD(getCallDiff(RecvMSSlot + &H49, CH)) & "8BE55DC3" '&H49
        ph = ConvHEX2ByteArray(pHook)
        WriteByteArray(RecvMSSlot, ph, UBound(ph) - LBound(ph) + 1)
    End Sub
    Public Sub recvHook(MailSlotName As String)
        Dim pHook As String, ph() As Byte, phR() As Byte
        writeMailSlot(MailSlotName)
        pHook = "558BEC83C4F8538B450883C0048B108955FC8B4D0883C1088B018945F8FF75FCFF75F8E8" & ADWORD(getCallDiff(RecvHKSlot + &H23, RecvMSSlot)) & "83C4088B0D" & ADWORD(KO_PTR_DLG - &H14) & "FF750CFF7508B8" & ADWORD(ReadLong(KO_RECV_PTR)) & "FFD05B59595DC20800"
        ph = ConvHEX2ByteArray(pHook)
        WriteByteArray(RecvHKSlot, ph, UBound(ph) - LBound(ph) + 1)
        phR = ConvHEX2ByteArray(ADWORD(RecvHKSlot))
        WriteByteArray(KO_RECV_PTR, phR, UBound(phR) - LBound(phR) + 1)
    End Sub
    Public Sub InjectPatch(ByVal addr As Long, ByVal pStr As String)
        Dim pBytes() As Byte
        pBytes = ConvHEX2ByteArray(pStr)
        WriteByteArray(addr, pBytes, UBound(pBytes) - LBound(pBytes) + 1)
    End Sub
    Public Sub ReadMessage(ByVal LootCheck As Boolean, ByVal LootStatu As Integer, ByRef LootListView As ListView, ByVal GoLoot As Boolean, ByRef LootIDList As ListBox, ByRef LootKorList As ListBox, ByVal LootItemExt As String, ByVal CheckGm As Boolean, ByVal Attack_Statu As Boolean, ByVal cmbChoose As ComboBox, ByVal chkGarbage As CheckBox, ByVal lstbGarbage As ListView)
        Dim rc As Integer
        Dim lnextMsgSize As Integer
        Dim tmplpBuffer As [Byte]()
        Dim lBytesRead As Int32
        Dim msg_buffer As String
        Dim ItemStatu, Box_X, Box_Y, ItemRow As Integer
        Dim BoxDownID, BoxLootDownID, BoxID, BoxTargetID, ItemID(5), ItemRec(5), RecAl, txtNotice, txtNoticeNick As String
        Static MessagesLeft As Int32
        Do While lnextMsgSize <> -1
            rc = GetMailslotInfo(RecvHandle, 0, lnextMsgSize, MessagesLeft, 0)
            If MessagesLeft > 0 Then
                tmplpBuffer = New Byte(lnextMsgSize) {}
                If rc > 0 Then
                    Dim abc As IntPtr = ReadFile(RecvHandle, tmplpBuffer(0), Marshal.SizeOf(GetType(System.Byte)) * lnextMsgSize, lBytesRead, IntPtr.Zero)
                    If lBytesRead <> 0 Then
                        msg_buffer = ByteToString(tmplpBuffer)
                        HookPacket += 1
                        Select Case tmplpBuffer(0)

                            Case &H1 'WIZ_LOGIN
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(0) = True) Then frm_3Recv.lst_Recv.Items.Add("LOGIN-->" & msg_buffer)

                            Case &H2 'WIZ_NEW_CHAR
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(1) = True) Then frm_3Recv.lst_Recv.Items.Add("NEW_CHAR-->" & msg_buffer)

                            Case &H3 'WIZ_DEL_CHAR
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(2) = True) Then frm_3Recv.lst_Recv.Items.Add("DEL_CHAR-->" & msg_buffer)

                            Case &H4 'WIZ_SEL_CHAR
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(3) = True) Then frm_3Recv.lst_Recv.Items.Add("SEL_CHAR-->" & msg_buffer)

                            Case &H5 'WIZ_SEL_NATION
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(4) = True) Then frm_3Recv.lst_Recv.Items.Add("SEL_NATION-->" & msg_buffer)

                            Case &H6 'WIZ_MOVE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(5) = True) Then frm_3Recv.lst_Recv.Items.Add("MOVE-->" & msg_buffer)

                            Case &H7 'WIZ_USER_INOUT
                                Dim playerId, playerNick As String
                                Dim playerNickLength, playerStatu, playerBase As Integer
                                playerStatu = Convert.ToInt32(FormatDec(msg_buffer.Substring(2, 2), 2))
                                If (playerStatu = 3) Then
                                    playerId = msg_buffer.Substring(6, 4)
                                    playerNickLength = Convert.ToInt32(FormatDec(msg_buffer.Substring(10, 2), 2)) * 2
                                    playerNick = HexToString(msg_buffer.Substring(12, playerNickLength))
                                    playerBase = GetBase(Convert.ToInt32(FormatDec(playerId, 4)))
                                    'If (searchList(playerNick, frm_2Genel.lstPlayerSlot) = False) Then frm_2Genel.lstPlayerSlot.Items.Add(playerNick)
                                Else
                                    playerId = msg_buffer.Substring(6, 4)
                                    playerNick = getBaseIDName(Convert.ToInt32(FormatDec(playerId, 4)))
                                    'If (searchListIndex(playerNick, frm_2Genel.lstPlayerSlot) > -1) Then frm_2Genel.lstPlayerSlot.Items.RemoveAt(searchListIndex(playerNick, frm_2Genel.lstPlayerSlot))
                                End If

                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(6) = True) Then frm_3Recv.lst_Recv.Items.Add("USER_INOUT-->" & msg_buffer)

                            Case &H8 'WIZ_ATTACK
                                Dim PLeaderID, PLeaderMobID As String
                                PLeaderID = msg_buffer.Substring(6, 4)
                                PLeaderMobID = msg_buffer.Substring(10, 4)
                                If (getPartyCount() > 0 And PLeaderID = PartyUserID(1) And PLeaderID <> getCharID() And Attack_Statu = True And cmbChoose.SelectedIndex = 4) Then
                                    selectMob(FormatDec(PLeaderMobID, 4), True)
                                End If

                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(7) = True) Then frm_3Recv.lst_Recv.Items.Add("ATTACK-->" & msg_buffer)

                            Case &H9 'WIZ_ROTATE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(8) = True) Then frm_3Recv.lst_Recv.Items.Add("ROTATE-->" & msg_buffer)

                            Case &HA  'WIZ_NPC_INOUT
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(9) = True) Then frm_3Recv.lst_Recv.Items.Add("NPC_INOUT-->" & msg_buffer)

                            Case &HB  'WIZ_NPC_MOVE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(10) = True) Then frm_3Recv.lst_Recv.Items.Add("NPC_MOVE-->" & msg_buffer)

                            Case &HC  'WIZ_ALLCHAR_INFO_REQ
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(11) = True) Then frm_3Recv.lst_Recv.Items.Add("ALLCHAR_INFO_REQ-->" & msg_buffer)

                            Case &HD  'WIZ_GAMESTART
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(12) = True) Then frm_3Recv.lst_Recv.Items.Add("GAMESTART-->" & msg_buffer)

                            Case &HE  'WIZ_MYINFO
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(13) = True) Then frm_3Recv.lst_Recv.Items.Add("MYINFO-->" & msg_buffer)

                            Case &HF  'WIZ_LOGOUT
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(14) = True) Then frm_3Recv.lst_Recv.Items.Add("LOGOUT-->" & msg_buffer)

                            Case &H10  'WIZ_CHAT
                                If msg_buffer.IndexOf("1007") >= 0 And msg_buffer.IndexOf("696C6C6567616C20736F6674776172652E2023232323") >= 0 And CheckGm = True Then 'Notice geçtiyse
                                    txtNotice = HexToString(msg_buffer.Substring(16, msg_buffer.Length - 16))
                                    txtNoticeNick = txtNotice.Substring(14, txtNotice.IndexOf(" is currently blocked") - 14)
                                    If frm_2Genel.rb_checkerListBan.Checked = True And searchList(txtNoticeNick, frm_2Genel.lst_checkerGM) = True Then
                                        frm_2Genel.lbl_GmStatu.Text = 1
                                    End If
                                    If frm_2Genel.rb_checkerChatBan.Checked = True Then
                                        frm_2Genel.lbl_GmStatu.Text = 1
                                    End If
                                End If
                                If (msg_buffer.Substring(0, 4) = "1003" Or msg_buffer.Substring(0, 4) = "1002") Then ' Party chat
                                    Dim charNameLen As Integer = FormatDec(msg_buffer.Substring(10, 2), 2)
                                    Dim charName As String = HexToString(msg_buffer.Substring(12, charNameLen * 2))
                                    Dim textLen As Integer = FormatDec(msg_buffer.Substring((12 + (charNameLen * 2)), 4), 4)
                                    Dim textStart As Integer = (12 + (charNameLen * 2) + 4)
                                    Dim textChat As String = HexToString(msg_buffer.Substring(textStart, msg_buffer.Length - textStart))

                                    If (frm_2Genel.chk_chatSummon.Checked And
                                        frm_2Genel.txt_chatSummon.Text <> "" And
                                        textChat.IndexOf(frm_2Genel.txt_chatSummon.Text) >= 0 And
                                        textChat.IndexOf(" ") >= frm_2Genel.txt_chatSummon.Text.Length And
                                        getPartyCount() > 1 And getCharClassName() = "Mage") Then

                                        If (textChat = frm_2Genel.txt_chatSummon.Text & " topla") Then
                                            SummonStart = True
                                            SummonRow = 1
                                            Exit Sub
                                        ElseIf (textChat.Length = frm_2Genel.txt_chatSummon.Text.Length + 2) Then
                                            Dim chatNickRow As String = textChat.Split(" ")(1)
                                            If (PartyUserID(chatNickRow) <> getCharID()) Then
                                                summonUser(PartyUserID(chatNickRow))
                                                Exit Sub
                                            End If
                                        End If

                                    End If
                                End If
                                '10020200000C5F535F706F646E696B5F535F02003434

                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(15) = True) Then frm_3Recv.lst_Recv.Items.Add("CHAT-->" & msg_buffer)

                            Case &H11  'WIZ_DEAD
                                Dim deadID As Integer
                                deadID = FormatDec(msg_buffer.Substring(2, 4), 4)
                                If (deadID = getMobID()) Then
                                    If (frm_2Genel.chk_DeathWait.Checked And frm_2Genel.btn_ProjectStart.Text = "Durdur") Then
                                        AttackStatus = False
                                        WaitAttack = Convert.ToInt32(frm_2Genel.txt_DeathWait.Text)
                                    End If
                                End If
                                WriteLong(GetBase(deadID) + &H3E4, 5)
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(16) = True) Then frm_3Recv.lst_Recv.Items.Add("DEAD-->" & msg_buffer)

                            Case &H12  'WIZ_REGENE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(17) = True) Then frm_3Recv.lst_Recv.Items.Add("REGENE-->" & msg_buffer)

                            Case &H13  'WIZ_TIME
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(18) = True) Then frm_3Recv.lst_Recv.Items.Add("TIME-->" & msg_buffer)

                            Case &H14 'WIZ_WEATHER
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(19) = True) Then frm_3Recv.lst_Recv.Items.Add("WEATHER-->" & msg_buffer)

                            Case &H15  'WIZ_REGIONCHANGE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(20) = True) Then frm_3Recv.lst_Recv.Items.Add("REGIONCHANGE-->" & msg_buffer)

                            Case &H16  'WIZ_REQ_USERIN
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(21) = True) Then frm_3Recv.lst_Recv.Items.Add("REQ_USERIN-->" & msg_buffer)

                            Case &H17  'WIZ_HP_CHANGE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(22) = True) Then frm_3Recv.lst_Recv.Items.Add("HP_CHANGE-->" & msg_buffer)

                            Case &H18  'WIZ_MSP_CHANGE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(23) = True) Then frm_3Recv.lst_Recv.Items.Add("MSP_CHANGE-->" & msg_buffer)

                            Case &H19  'WIZ_ITEM_LOG
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(24) = True) Then frm_3Recv.lst_Recv.Items.Add("ITEM_LOG-->" & msg_buffer)

                            Case &H1A  'WIZ_EXP_CHANGE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(25) = True) Then frm_3Recv.lst_Recv.Items.Add("EXP_CHANGE-->" & msg_buffer)

                            Case &H1B  'WIZ_LEVEL_CHANGE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(26) = True) Then frm_3Recv.lst_Recv.Items.Add("LEVEL_CHANGE-->" & msg_buffer)

                            Case &H1C  'WIZ_NPC_REGION
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(27) = True) Then frm_3Recv.lst_Recv.Items.Add("NPC_REGION-->" & msg_buffer)

                            Case &H1D  'WIZ_REQ_NPCIN
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(28) = True) Then frm_3Recv.lst_Recv.Items.Add("REQ_NPCIN-->" & msg_buffer)

                            Case &H1E  'WIZ_WARP
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(29) = True) Then frm_3Recv.lst_Recv.Items.Add("WARP-->" & msg_buffer)

                            Case &H1F  'WIZ_ITEM_MOVE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(30) = True) Then frm_3Recv.lst_Recv.Items.Add("ITEM_MOVE-->" & msg_buffer)

                            Case &H20  'WIZ_NPC_EVENT
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(31) = True) Then frm_3Recv.lst_Recv.Items.Add("NPC_EVENT-->" & msg_buffer)

                            Case &H21  'WIZ_ITEM_TRADE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(32) = True) Then frm_3Recv.lst_Recv.Items.Add("ITEM_TRADE-->" & msg_buffer)

                            Case &H22  'WIZ_TARGET_HP
                                Dim deadID As Integer, deahMaxHp As Integer, deahHp As Integer
                                deadID = FormatDec(msg_buffer.Substring(2, 4), 4)
                                deahMaxHp = FormatDec(msg_buffer.Substring(8, 8), 8)
                                deahHp = FormatDec(msg_buffer.Substring(16, 8), 8)

                                If (deadID = getMobID()) Then
                                    If (deahHp <= 0 And frm_2Genel.chk_DeathWait.Checked And frm_2Genel.btn_ProjectStart.Text = "Durdur") Then
                                        AttackStatus = False
                                        WaitAttack = Convert.ToInt32(frm_2Genel.txt_DeathWait.Text)
                                    End If
                                End If
                                If (deahHp <= 0) Then WriteLong(GetBase(deadID) + &H3E4, 5)

                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(33) = True) Then frm_3Recv.lst_Recv.Items.Add("TARGET_HP-->" & msg_buffer)

                            Case &H23  'WIZ_ITEM_DROP
                                If LootCheck Then
                                    BoxTargetID = FormatDec(msg_buffer.Substring(2, 4), 4)
                                    BoxDownID = msg_buffer.Substring(6, 8)
                                    ItemStatu = CInt(msg_buffer.Substring(msg_buffer.Length - 1, 1))
                                    If ItemStatu > 0 Then
                                        Box_X = CInt(getBaseIDX(BoxTargetID))
                                        Box_Y = CInt(getBaseIDY(BoxTargetID))
                                        If GoLoot = True Then
                                            LootIDList.Items.Add("24" & BoxDownID)
                                            LootKorList.Items.Add(Box_X & " - " & Box_Y)
                                        Else
                                            SendPack("24" & BoxDownID)
                                        End If
                                    End If
                                End If
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(34) = True) Then frm_3Recv.lst_Recv.Items.Add("ITEM_DROP-->" & msg_buffer)

                            Case &H24  'WIZ_BUNDLE_OPEN_REQ
                                If LootCheck = True Then
                                    ItemRow = 12
                                    If msg_buffer.Length < 80 Then Exit Sub 'Boş kutuyu açmaya çalışırsa
                                    BoxLootDownID = msg_buffer.Substring(2, 8)
                                    If GoLoot = True Then
                                        If (LootIDList.Text.IndexOf(BoxLootDownID) > -1) Then
                                            LootIDList.Items.RemoveAt(0)
                                            LootKorList.Items.RemoveAt(0)
                                        End If
                                    End If
                                    BoxID = msg_buffer.Substring(2, 4)
                                    RecAl = msg_buffer.Substring(6, 4)
                                    For i As Integer = 0 To 5
                                        ItemID(i) = msg_buffer.Substring(ItemRow, 8)
                                        ItemRow = ItemRow + 8
                                        ItemRec(i) = msg_buffer.Substring(ItemRow, 4)
                                        ItemRow = ItemRow + 4
                                    Next
                                    For i As Integer = 0 To 5
                                        If FormatDec(ItemID(i), 8) > 0 Then
                                            Select Case LootStatu
                                                Case 0
                                                    If ItemID(i) = "00E9A435" Then
                                                        LootBox(BoxID, RecAl, ItemID(i), ADWORD(i, 2))
                                                    End If
                                                Case 2
                                                    If searchListView(FormatDec(ItemID(i), 8).ToString().Substring(0, 6) & "000", 1, LootListView) = True Or ItemID(i) = "00E9A435" Then
                                                        LootBox(BoxID, RecAl, ItemID(i), ADWORD(i, 2))
                                                    End If
                                                Case 3
                                                    If ItemID(i) = "00E9A435" Or Convert.ToInt32(FormatDec(ItemID(i), 8).Substring(8, 1)) >= Convert.ToInt32(LootItemExt.Replace("+", "")) Then
                                                        LootBox(BoxID, RecAl, ItemID(i), ADWORD(i, 2))
                                                    End If
                                                Case Else
                                                    If (chkGarbage.Checked) Then
                                                        If (searchListView(FormatDec(ItemID(i), 8).ToString().Substring(0, 6) & "000", 1, lstbGarbage) = False Or ItemID(i) = "00E9A435") Then
                                                            LootBox(BoxID, RecAl, ItemID(i), ADWORD(i, 2))
                                                        End If
                                                    Else
                                                        LootBox(BoxID, RecAl, ItemID(i), ADWORD(i, 2))
                                                    End If
                                            End Select
                                        End If
                                    Next
                                End If
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(35) = True) Then frm_3Recv.lst_Recv.Items.Add("BUNDLE_OPEN_REQ-->" & msg_buffer)

                            Case &H25  'WIZ_TRADE_NPC
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(36) = True) Then frm_3Recv.lst_Recv.Items.Add("TRADE_NPC-->" & msg_buffer)

                            Case &H2F 'WIZ_PARTY
                                '12 
                                Dim pStatu As Integer = msg_buffer.Substring(3, 1)
                                If (pStatu = 2 And frm_2Genel.chk_AutoParty.Checked) Then
                                    Dim NickHex As String = msg_buffer.Substring(12, msg_buffer.Length - 12)
                                    Dim Nick As String = HexToString(NickHex)
                                    SendPack("2F0201")
                                End If

                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(51) = True) Then frm_3Recv.lst_Recv.Items.Add("PARTY-->" & msg_buffer)

                            Case &H29  'WIZ_STATE_CHANGE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(37) = True) Then frm_3Recv.lst_Recv.Items.Add("STATE_CHANGE-->" & msg_buffer)

                            Case &H2D  'WIZ_USERLOOK_CHANGE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(38) = True) Then frm_3Recv.lst_Recv.Items.Add("USERLOOK_CHANGE-->" & msg_buffer)

                            Case &H2E  'WIZ_NOTICE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(39) = True) Then frm_3Recv.lst_Recv.Items.Add("NOTICE-->" & msg_buffer)

                            Case &H31  'WIZ_MAGIC_PROCESS
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(40) = True) Then frm_3Recv.lst_Recv.Items.Add("MAGIC_PROCESS-->" & msg_buffer)

                            Case &H33  'WIZ_OBJECT_EVENT
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(41) = True) Then frm_3Recv.lst_Recv.Items.Add("OBJECT_EVENT-->" & msg_buffer)

                            Case &H3A  'WIZ_REPAIR_NPC
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(42) = True) Then frm_3Recv.lst_Recv.Items.Add("REPAIR_NPC-->" & msg_buffer)

                            Case &H3B  'WIZ_ITEM_REPAIR
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(43) = True) Then frm_3Recv.lst_Recv.Items.Add("ITEM_REPAIR-->" & msg_buffer)

                            Case &H3C  'WIZ_KNIGHTS_PROCESS
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(44) = True) Then frm_3Recv.lst_Recv.Items.Add("KNIGHTS_PROCESS-->" & msg_buffer)

                            Case &H3F  'WIZ_ITEM_REMOVE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(45) = True) Then frm_3Recv.lst_Recv.Items.Add("ITEM_REMOVE-->" & msg_buffer)

                            Case &H42  'WIZ_COMPRESS_PACKET
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(46) = True) Then frm_3Recv.lst_Recv.Items.Add("COMPRESS_PACKET-->" & msg_buffer)

                            Case &H49  'WIZ_FRIEND_REPORT
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(47) = True) Then frm_3Recv.lst_Recv.Items.Add("FRIEND_REPORT-->" & msg_buffer)

                            Case &H54  'WIZ_WEIGHT_CHANGE
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(48) = True) Then frm_3Recv.lst_Recv.Items.Add("WEIGHT_CHANGE-->" & msg_buffer)

                            Case &H55  'WIZ_SELECT_MSG
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(49) = True) Then frm_3Recv.lst_Recv.Items.Add("SELECT_MSG-->" & msg_buffer)

                            Case Else  'UNKNOW
                                If (frm_3Recv.chk_Active.Checked = True And frm_3Recv.clst_Recv.GetItemChecked(50) = True) Then frm_3Recv.lst_Recv.Items.Add("UNKNOW (" & msg_buffer.Substring(0, 2) & ")-->" & msg_buffer)

                        End Select
                    End If
                End If
            End If
        Loop
    End Sub
    Public Sub LootBox(ByVal BoxID As String, ByVal BoxRec As String, ByVal BoxItemID As String, ByVal BoxItemSlot As String)
        If FormatDec(BoxItemID, 8) > 0 Then SendPack("26" & BoxID & BoxRec & BoxItemID & BoxItemSlot & "00")
    End Sub
    Public Sub OpenBox(ByVal LootRecv As String, ByVal SuperBox As Boolean)
        If LootRecv = Nothing Then Exit Sub
        If SuperBox = True Then
            Dim BoxBaseID, BoxParam As String, BoxCount, For1, For2 As Integer
            BoxBaseID = LootRecv.Substring(6, 2)
            BoxParam = LootRecv.Substring(8, 6)
            BoxCount = FormatDec(BoxBaseID, 2)
            For1 = BoxCount - 16
            For2 = BoxCount + 1
            For i As Integer = For1 To For2
                SendPack("24" & ADWORD(i, 2) & BoxParam)
            Next
        Else
            Dim BoxID As String
            BoxID = LootRecv.Substring(6, 8)
            SendPack("24" & BoxID)
        End If
    End Sub
    Public Function getCallDiff(ByVal Source As Long, ByVal Destination As Long) As Long
        Dim result As Long = 0
        Dim Diff As Long = 0
        If (Source > Destination) Then
            Diff = Source - Destination
            If (Diff > 0) Then result = (4294967291) - Diff
        Else
            result = Destination - Source - 5
        End If
        Return result
    End Function
#End Region
#Region "Çeviri Fonksiyonları"
    Function ByteToString(ByVal Data() As Byte) As String
        Dim i As Short, s As String
        s = ""
        For i = 0 To UBound(Data) - 1
            s = s & Data(i).ToString("x2").ToUpper
        Next i
        Return s
    End Function
    Public Function Hex2Byte(ByVal sInput As String, ByRef bOutput() As Byte)
        Dim m_len As Long, tmp As String
        On Error Resume Next
        m_len = Len(sInput)
        If m_len > UBound(bOutput) Then
            m_len = UBound(bOutput)
        End If
        For i As Integer = 1 To Len(sInput) Step 2
            tmp = Mid(sInput, i, 2)
            bOutput((i + 1) / 2) = Val("&H" & tmp)
        Next
    End Function
    Public Function Hex2Val(ByVal pStrHex As String) As Long
        Dim TmpStr As String, TmpHex As String
        TmpStr = ""
        For i As Integer = Len(pStrHex) To 1 Step -1
            TmpHex = Hex(Asc(Mid(pStrHex, i, 1)))
            If Len(TmpHex) = 1 Then TmpHex = "0" & TmpHex
            TmpStr = TmpStr & TmpHex
        Next
        Return CLng("&H" & TmpStr)
    End Function
    Public Function HexToString(ByVal HexValue As String) As String
        Dim result As String = "", sHexVal As String = ""
        For x = 0 To HexValue.Length - 1 Step 2
            sHexVal = HexValue.Substring(x, 2)
            result &= System.Convert.ToChar(System.Convert.ToUInt32(sHexVal, 16)).ToString()
        Next
        Return result
    End Function
    Private Function ConvHEX2ByteArray(ByVal HexString As String) As Byte()
        Dim Bytes() As Byte
        Dim HexPos As Integer
        Dim HexDigit As Integer
        Dim BytePos As Integer
        Dim Digits As Integer

        ReDim Bytes(Len(HexString) \ 2)  'Initial estimate.
        For HexPos = 1 To Len(HexString)
            HexDigit = InStr("0123456789ABCDEF", _
                             UCase$(Mid$(HexString, HexPos, 1))) - 1
            If HexDigit >= 0 Then
                If BytePos > UBound(Bytes) Then
                    ReDim Preserve Bytes(UBound(Bytes) + 4)
                End If
                Bytes(BytePos) = Bytes(BytePos) * &H10 + HexDigit
                Digits = Digits + 1
            End If
            If Digits = 2 Or HexDigit < 0 Then
                If Digits > 0 Then BytePos = BytePos + 1
                Digits = 0
            End If
        Next
        If Digits = 0 Then BytePos = BytePos - 1
        If BytePos >= 0 Then
            ReDim Preserve Bytes(BytePos)
        End If
        Return Bytes
    End Function
    Public Function ConvHEX2ByteArrayS(ByVal pStr As String) As Byte()
        On Error Resume Next
        Dim i As Long
        Dim j As Long
        Dim pByte() As Byte

        ReDim pByte(0 To (Len(pStr) / 2) - 1)

        j = LBound(pByte) - 1
        For i = 1 To Len(pStr) Step 2
            j = j + 1
            pByte(j) = CByte("&H" & Mid(pStr, i, 2))
        Next
        Return pByte
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
    Function ADWORD(Dec As Long, Optional Length As Long = 8) As String
        Dim HiW As Integer
        Dim LoW As Integer

        Dim HiBHiW As Byte
        Dim HiBLoW As Byte

        Dim LoBHiW As Byte
        Dim LoBLoW As Byte
        Dim retStr As String
        HiW = HiWord(Dec)
        LoW = LoWord(Dec)

        HiBHiW = HiByte(HiW)
        HiBLoW = HiByte(LoW)

        LoBHiW = LoByte(HiW)
        LoBLoW = LoByte(LoW)

        retStr = IIf(Len(Hex(LoBLoW)) = 1, "0" & Hex(LoBLoW), Hex(LoBLoW)) & _
                 IIf(Len(Hex(HiBLoW)) = 1, "0" & Hex(HiBLoW), Hex(HiBLoW)) & _
                 IIf(Len(Hex(LoBHiW)) = 1, "0" & Hex(LoBHiW), Hex(LoBHiW)) & _
                 IIf(Len(Hex(HiBHiW)) = 1, "0" & Hex(HiBHiW), Hex(HiBHiW))
        Return retStr.Substring(0, Length)
    End Function
    Public Function HiByte(ByVal wParam As Integer) As Byte
        HiByte = (wParam And &HFF00&) \ (&H100)
    End Function
    Public Function LoByte(ByVal wParam As Integer) As Byte
        LoByte = wParam And &HFF&
    End Function
    Function LoWord(ByVal DWord As Long) As Integer
        If DWord And &H8000& Then
            LoWord = DWord Or &HFFFF0000
        Else
            LoWord = DWord And &HFFFF&
        End If
    End Function
    Function HiWord(ByVal DWord As Long) As Integer
        HiWord = (DWord And &HFFFF0000) \ &H10000
    End Function
    Public Function FormatHex(ByVal strHex As String, ByVal inLength As Integer) As String
        Dim newHex As String
        newHex = Strings.StrDup(inLength - Len(strHex), "0") + strHex
        Select Case Len(newHex)
            Case 2
                newHex = Left(newHex, 2)
            Case 4
                newHex = Right(newHex, 2) & Left(newHex, 2)
            Case 6
                newHex = Right(newHex, 2) & Mid(newHex, 3, 2) & Left(newHex, 2)
            Case 8
                newHex = Right(newHex, 2) & Mid(newHex, 5, 2) & Mid(newHex, 3, 2) & Left(newHex, 2)
            Case Else
        End Select
        Return newHex
    End Function
    Public Function FormatDec(ByVal strHex As String, ByVal inLength As Integer) As String
        Dim NewDec As String
        NewDec = Strings.StrDup(inLength - Len(strHex), "0") + strHex
        Select Case Len(NewDec)
            Case 2
                NewDec = CDec("&H" & Left(NewDec, 2))
            Case 4
                NewDec = CDec("&H" & Right(NewDec, 2) & Left(NewDec, 2))
            Case 6
                NewDec = CDec("&H" & Right(NewDec, 2) & Mid(NewDec, 3, 2) & Left(NewDec, 2))
            Case 8
                NewDec = CDec("&H" & Right(NewDec, 2) & Mid(NewDec, 5, 2) & Mid(NewDec, 3, 2) & Left(NewDec, 2))
        End Select
        Return NewDec
    End Function
#End Region
#Region "Bilgi Okutma Fonksiyonları"
    Public Function ReadLong(ByVal val As Long) As Long
        Dim value As Long
        ReadProcessMemory(Ko_Handle, val, value, 4, 0&)
        Return value
    End Function
    Public Function ReadFloat(ByVal Address As Long)
        On Error Resume Next
        Dim value As Single
        ReadFloatMemory(Ko_Handle, Address, value, 4, 0)
        Return Convert.ToInt32(value)
    End Function
    Public Function ReadByte(ByVal pAddy As Long) As Byte
        Dim Value As Byte
        ReadProcessMemory(Ko_Handle, pAddy, Value, 1, 0&)
        Return Value
    End Function
    Public Function ReadByteFloat(ByVal pAddy As Long) As Double
        Dim Value As Byte
        ReadFloatMemory(Ko_Handle, pAddy, Value, 2, 0&)
        Return Value
    End Function
    Public Function ReadString(ByVal Address As Integer) As String
        Dim tempInt As Integer = 0
        ReadString = ""
        For n As Integer = 0 To 150
            ReadProcessMemory(Ko_Handle, Address + n, tempInt, 1, 0)
            If tempInt > 0 Then
                ReadString = ReadString & Chr(tempInt)
            Else
                Exit Function
            End If
        Next
    End Function
    Public Function ReadStringChat(ByVal Address As Integer) As String
        Dim Count1 As Integer = 0
        Dim tempInt As Integer = 0
        ReadStringChat = ""
        For n = 1 To 50
            ReadProcessMemory(Ko_Handle, Address + Count1, tempInt, 1, 0)
            If tempInt > 0 Then
                ReadStringChat = ReadStringChat & Chr(tempInt)
                Count1 += 1
            Else
                Exit Function
            End If
        Next
    End Function
    Public Function ReadShort(ByVal val As Long) As Long
        Dim value As Long
        ReadProcessMemory(Ko_Handle, val, value, 2, 0&)
        Return value
    End Function
#End Region
#Region "Adrese Bilgi Yazdırma Fonksiyonları"
    Public Sub WriteByte(ByVal pAddy As IntPtr, ByVal pVal As IntPtr)
        WriteProcessMemory(Ko_Handle, pAddy, pVal, 1, 0&)
    End Sub
    Public Sub WriteByteArray(ByVal pAddy As Long, ByVal pmem() As Byte, ByVal pSize As Long)
        WriteProcessMemory(Ko_Handle, pAddy, pmem, pSize, 0&)
    End Sub
    Public Sub WriteFloat(ByVal addr As Long, ByVal Val As Single)
        WriteFloatMemory(Ko_Handle, addr, Val, 4, 0&)
    End Sub
    Public Sub WriteLong(ByVal pAddy As IntPtr, ByVal pVal As IntPtr)
        WriteProcessMemory(Ko_Handle, pAddy, pVal, 4, 0&)
    End Sub
    Public Function GetAdress(ByVal adress As Integer)
        Return ReadLong(ReadLong(KO_PTR_CHR) + adress)
    End Function
    Public Sub SetAdress(ByVal adress As Integer)
        WriteLong(ReadLong(KO_PTR_CHR) + adress, 0)
    End Sub
#End Region
#Region "Paket & Fonksiyon Gönderme Fonksiyonları"
    Public Function ExecuteRemoteCode(ByVal pCode As Byte()) As IntPtr
        On Error Resume Next
        Dim hThread As IntPtr
        WriteProcessMemory(Ko_Handle, CodeBytes, pCode, pCode.Length, 0)
        hThread = CreateRemoteThread(Ko_Handle, 0, 0, CodeBytes, 0, 0, New IntPtr(0))
        Call WaitForSingleObject(hThread, INFINITE)
        CloseHandle(hThread)
    End Function
    Public Function ExecuteRemoteCodeNormal(ByVal pCode As Byte(), ByVal pAdress As Long) As IntPtr
        On Error Resume Next
        Dim hThread As IntPtr
        WriteProcessMemory(Ko_Handle, pAdress, pCode, pCode.Length, 0)
        hThread = CreateRemoteThread(Ko_Handle, 0, 0, pAdress, 0, 0, New IntPtr(0))
        Call WaitForSingleObject(hThread, INFINITE)
        CloseHandle(hThread)
    End Function
    Public Sub SendPackets(ByVal pPacket() As Byte) '&H48CA10
        Dim pSize As Long, opCode As String
        pSize = UBound(pPacket) - LBound(pPacket) + 1
        WriteByteArray(PacketBytes, pPacket, pSize)
        opCode = "608B0D" & ADWORD((KO_PTR_PKT), 8) _
            & "68" & ADWORD((pSize), 8) _
            & "68" & ADWORD((PacketBytes), 8) _
            & "BF" & ADWORD((KO_SND_FNC), 8) _
            & "FFD761C3"
        ExecuteRemoteCode(ConvHEX2ByteArray(opCode))
    End Sub
    Public Sub SendPacketsS(ByVal pPacket() As Byte)
        Dim pSize As Long
        pSize = UBound(pPacket) - LBound(pPacket) + 1
        WriteByteArray(PacketBytes, pPacket, pSize)
        ExecuteRemoteCode(ConvHEX2ByteArray("608B0D" & ADWORD((KO_PTR_PKT), 8) _
                                            & "68" & ADWORD((pSize), 8) _
                                            & "68" & ADWORD((PacketBytes), 8) _
                                            & "BF" & ADWORD((KO_SND_FNC), 8) _
                                            & "FFD761C3"))
    End Sub
    Public Sub SendSkillNormal(ByVal lSkillID As String, ByVal wMobID As Long, Optional ByVal bTekSkl As Boolean = False)
        If (wMobID <= 0) Then Exit Sub
        Dim offset As String
        If (BotForSteam) Then
            offset = ADWORD(&H410)
        Else
            offset = ADWORD(&H424)
        End If
        Dim scode As String
        scode = "B8" & ADWORD(wMobID) & _
                "50" & _
                "B8" & lSkillID & _
                "50" & _
                "8B0D" & ADWORD(KO_SMMB) & _
                "B8" & ADWORD(KO_SMMB_FNC) & _
                "FFD0" & _
                "50" & _
                "8B0D" & ADWORD(KO_PTR_DLG) & _
                "8B89" & offset & _
                "B8" & ADWORD(KO_SUB_ADDR0) & _
                "FFD0"

        If bTekSkl = False Then
            scode = scode & "8B0D" & ADWORD(KO_PTR_NRML) & "B8" & ADWORD(KO_SUB_ADDR1) & "FFD0"
        End If
        scode = scode & "C3"
        ExecuteRemoteCodeNormal(ConvHEX2ByteArray(scode), NormalAdress)
    End Sub
    Public Sub SendKeys(ByVal _keys As String)
        If (GetCaption() = Ko_Title) Then
            'kSend.Send(_keys)
        End If
    End Sub
    Public Sub OpenGenie()
        Dim pHook As String, ph() As Byte
        pHook = "608B0D" & ADWORD(KO_PTR_CHR) & "6A02BF" & ADWORD(&H7DB790) & "FFD761C3"
        ph = ConvHEX2ByteArray(pHook)
        ExecuteRemoteCode(ph)
    End Sub
    Public Sub WriteGenie()
        WriteLong(ReadLong(ReadLong(KO_PTR_CHR) + &H748), 20)
    End Sub
    Public Sub useItem(ByVal WitchId As Integer, ByVal WitchSlot As Integer)
        Dim pHook As String, ph() As Byte
        ' Public Const KO_PTR_CHR As Long = &HDF87E4
        pHook = "608B0D" & ADWORD(KO_PTR_CHR) & "6A0" & WitchSlot & "68" & ADWORD(WitchId) & "BF" & ADWORD(KO_FAKE_ITEM) & "FFD761C3"
        ph = ConvHEX2ByteArray(pHook)
        ExecuteRemoteCode(ph)
    End Sub
    Public Sub SendPack(ByVal Pack3t As String)
        SendPackets(ConvHEX2ByteArray(Pack3t))
    End Sub
    Public Sub SendPSec(ByVal packetCode As String, ByVal TimeEx As Long, ByVal RowID As Integer)
        If PackSira(RowID) = 0 Then
            PackTick(RowID) = GetTickCount()
            SendPack(packetCode)
            PackSira(RowID) = 1
        End If
        If (GetTickCount() - PackTick(RowID)) < TimeEx Then
            Exit Sub
        Else
            PackTick(RowID) = GetTickCount()
            PackSira(RowID) = 0
        End If
    End Sub
    Public Sub SendPriestSec(ByVal packetCode As String, ByVal TimeEx As Long, ByVal RowID As Integer)
        If PackSira(RowID) = 0 Then
            PackTick(RowID) = GetTickCount()
            SendPack("3101" & packetCode & "00000000000000000F00")
            SendPack("3103" & packetCode & "000000000000")
            PackSira(RowID) = 1
        End If
        If (GetTickCount() - PackTick(RowID)) < TimeEx Then
            Exit Sub
        Else
            PackTick(RowID) = GetTickCount()
            PackSira(RowID) = 0
        End If
    End Sub
    Public Sub SendPotion(ByVal PotionID As String, ByVal RowID As Integer)
        If PackSira(RowID) = 0 Then
            PackTick(RowID) = GetTickCount()
            SendPack("3103" & PotionID & "00" & getCharID() & getCharID() & "00000000000000000000000000000000")
            PackSira(RowID) = 1
        End If
        If (GetTickCount() - PackTick(RowID)) < 2000 Then
            Exit Sub
        Else
            PackTick(RowID) = GetTickCount()
            PackSira(RowID) = 0
        End If
    End Sub
#End Region
#Region "İtem Fonksiyonları"
    Public Function GetItemName(ByVal itemId As Integer) As String
        Return ReadString(ReadLong(GetItemBase(itemId) + 28))
    End Function
    Function GetItemBase(ByVal ItemID As Long) As Long
        Dim CurrentPtr As Long
        Dim CurrentID As Long
        CurrentPtr = ReadLong(ReadLong(ReadLong(KO_ITOB) + 24) + &H4)
        While CurrentPtr <> 0
            CurrentID = ReadLong(CurrentPtr + &HC)
            If CurrentID >= ItemID Then
                If CurrentID = ItemID Then
                    Return CurrentPtr
                End If
                CurrentPtr = ReadLong(CurrentPtr + &H0)
            Else
                CurrentPtr = ReadLong(CurrentPtr + &H8)
            End If
        End While
        Return 0
    End Function
    Function GetItemExtBase(ItemExt As Long, ItemExtTbl As Long) As Long
        Dim CurrentPtr As Long, CurrentID As Long
        'ItemExt = Itemin Ext sayısı Item_Ext tblsindenden bakılır
        'ItemExtTbl = Hangi Item_Ext tblsinde olduğu
        CurrentPtr = ReadLong(ReadLong(ReadLong(KO_ITEB + 4 * ItemExtTbl) + 24) + &H4)
        While CurrentPtr <> 0
            CurrentID = ReadLong(CurrentPtr + &HC)
            If CurrentID >= ItemExt Then
                If CurrentID = ItemExt Then
                    Return CurrentPtr
                End If
                CurrentPtr = ReadLong(CurrentPtr + &H0)
            Else
                CurrentPtr = ReadLong(CurrentPtr + &H8)
            End If
        End While
        Return 0
        'Name = ReadString(ReadLong(ItemExtBase + &H18))
    End Function
    Public Function GetItemPrice(ByVal itemId As Integer) As Integer
        Dim ItemNo As Long, ItemExt As Long, ItemArtı As Long, ItemBase As Long, ItemExtGrup As Long, ItemExtBase As Long, ItemName As String, ItemExtName As String, ItemClass As Long, ItemSellFiyat As Integer
        ItemExt = Right(itemId, 3) 'Itemin Ext nosu
        ItemNo = Left(itemId, 6) & "000" 'Item base okumak için son 3 haneyi 000 yapıyoruz
        ItemBase = GetItemBase(ItemNo) 'Itemin Basesi
        ItemExtGrup = ReadLong(ItemBase + 20) 'Itemin Hangi Ext Tblsinde olduğu
        ItemExtBase = GetItemExtBase(ItemExt, ItemExtGrup) 'Itemin Ext Basesi
        ItemName = ReadString(ReadLong(ItemBase + 28)) 'Itemin Tbldeki adı
        ItemExtName = ReadString(ReadLong(ItemExtBase + 24)) 'Itemin Ext tablosundaki Adı
        'rs 104
        'rb 104
        'rl 104
        ItemArtı = ReadShort(ItemExtBase + 132) 'Itemin Ext tablosundaki + değeri
        ItemClass = ReadByte(ItemExtBase + 104) '1: magic, 2: rare, 3:craft, 4:unique, 5:upgrade
        ItemSellFiyat = ReadLong(ItemBase + 120) / 3
        If ItemClass = 2 Or ItemClass = 1 Then ItemSellFiyat = ItemSellFiyat * 2
        If ItemClass = 0 Then ItemSellFiyat = ItemSellFiyat / 2
        Return ItemSellFiyat
        'Dim new_info As New item_info
        'new_info.id = BASE / 1000
        'new_info.name = ItemBaseName(ESI)
        'new_info.item_type = Main.ReadByte(ESI + 104)

        'new_info.attack_speed = Main.ReadByte(ESI + 112)
        'new_info.attack_range = Main.ReadByte(ESI + 114)
        'new_info.weight = Main.ReadByte(ESI + 116) / 10
        'new_info.price = Main.ReadLong(ESI + 120)
        'new_info.countable = Main.ReadByte(ESI + 130)

        'new_info.ext_table = Main.ReadByte(ESI + 20)
        'new_info.item_grade = Main.ReadByte(ESI + 150)
        'item_org_tbl.Add(new_info)
        'ReadString(ReadLong(ItemBase + 56))	"A small bow made by| combining various odd| materials."
    End Function
    Function TamItemAdı(ItemNo2 As Long) As String
        'Test Fonksiyonudur tamamen bitmedi. Inventory/Pazar okurken kullanıyorum şimdilik.
        Dim ItemNo As Long, ItemExt As Long, ItemArtı As Long, ItemBase As Long, ItemExtGrup As Long, ItemExtBase As Long, ItemName As String, ItemExtName As String, ItemClass As Long, ItemSellFiyat As Integer, ItemArtısı As Integer
        Dim ParantezAra As Integer
        ItemExt = Right(ItemNo2, 3) 'Itemin Ext nosu
        ItemNo = Left(ItemNo2, 6) & "000" 'Item base okumak için son 3 haneyi 000 yapıyoruz
        ItemBase = GetItemBase(ItemNo) 'Itemin Basesi
        ItemExtGrup = ReadLong(ItemBase + 20) 'Itemin Hangi Ext Tblsinde olduğu
        ItemExtBase = GetItemExtBase(ItemExt, ItemExtGrup) 'Itemin Ext Basesi
        ItemName = ReadString(ReadLong(ItemBase + 28)) 'Itemin Tbldeki adı
        ItemExtName = ReadString(ReadLong(ItemExtBase + 24)) 'Itemin Ext tablosundaki Adı
        ItemArtı = ReadShort(ItemExtBase + 132) 'Itemin Ext tablosundaki + değeri
        ItemClass = ReadByte(ItemExtBase + 68) '1: magic, 2: rare, 3:craft, 4:unique, 5:upgrade
        ItemSellFiyat = ReadLong(ItemBase + 120) / 3
        If ItemClass = 2 Or ItemClass = 1 Then ItemSellFiyat = ItemSellFiyat * 2
        If ItemClass = 0 Then ItemSellFiyat = ItemSellFiyat / 2
        If ItemArtı = 0 Then
            Select Case Right(ItemNo2, 2) 'Bonuslarda sorun çıkartıyor :)
                Case 10 : ItemArtı = Right(ItemNo2, 2)
                Case 11 To 19 : ItemArtı = Right(ItemNo2, 2) - 10
                Case 21 To 29 : ItemArtı = Right(ItemNo2, 2) - 20
                Case 31 To 39 : ItemArtı = Right(ItemNo2, 2) - 30
                Case 41 To 49 : ItemArtı = Right(ItemNo2, 2) - 40
                Case 51 To 59 : ItemArtı = Right(ItemNo2, 2) - 50
                Case 61 To 69 : ItemArtı = Right(ItemNo2, 2) - 60
                Case 71 To 79 : ItemArtı = Right(ItemNo2, 2) - 70
                Case 81 To 89 : ItemArtı = Right(ItemNo2, 2) - 80
                Case 91 To 99 : ItemArtı = Right(ItemNo2, 2) - 90
                Case Else : ItemArtı = Right(ItemNo2, 2)
            End Select
            ItemArtı = Right(ItemArtı, 1)
        End If

        frm_2Genel.txt_Hex.Text = ItemExtName 'String değerler tanımlamadan kontrol edilmiyor bazen textten kontrol edelim.
        Select Case frm_2Genel.txt_Hex.Text
            'Bazı itemlerin Ext tablosunda adları yok onları belirleyelim.
            Case "", "Upgrade", "Strength", "Saint", "Light", "Revenge", "Unique", "Strong", "Wisdom", "Discharge", "Soul", "Blood", "Holy", "Blast", "Tempered", "Janet", "Shining", "Scarlet", "Stamina", "Cobalt", "Panic", "Agility", "Salamander", "Burning", "Frozen", "Lightning", "Viper", "Vampire", "Enchant", "Magical defense", "Blessed staff"
                frm_2Genel.txt_Hex.Text = ItemName 'Item adını text atalım. Stringler tanımlamalarda sorun çıkarıyor.
                If frm_2Genel.txt_Hex.Text = "Monster Stone" Then ItemArtı = 0
                If ItemArtı = 0 Then
                    TamItemAdı = frm_2Genel.txt_Hex.Text
                    ItemArtısı = 0
                Else
                    TamItemAdı = frm_2Genel.txt_Hex.Text
                    ItemArtısı = ItemArtı
                End If

                'Bazıların ise var direk Ext tablosundan alalım :)
            Case Else
                ParantezAra = InStr(1, ItemExtName, "(", vbTextCompare)
                If ParantezAra > 0 Then
                    TamItemAdı = Left(ItemExtName, ParantezAra - 1)
                    If ReadShort(ItemExtBase + 132) = 0 Then ItemArtısı = Mid(ItemExtName, ParantezAra + 2, 1)
                Else
                    TamItemAdı = ItemExtName
                    ItemArtısı = 0
                End If
        End Select
        Exit Function
    End Function
    Public Function GetItemBaseId(Optional ByVal Slot As Integer = 15) As Long
        On Error Resume Next
        Slot = Slot + KO_OFF_ITEMROW
        Return ReadLong(ReadLong(ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_ITEMB) _
        + (KO_OFF_ITEMS + (4 * Slot))) + &H68))
    End Function
    Public Function getItemID(Optional ByVal Slot As Integer = 15) As Long
        On Error Resume Next
        Slot = Slot + KO_OFF_ITEMROW
        Return ReadLong(ReadLong(ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_ITEMB) _
        + (KO_OFF_ITEMS + (4 * Slot))) + &H68)) _
        + ReadLong(ReadLong(ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_ITEMB) _
        + (KO_OFF_ITEMS + (4 * Slot))) + &H6C))
    End Function
    Public Function getItemUpExt(Optional ByVal Slot As Integer = 15) As Integer
        Slot = Slot + KO_OFF_ITEMROW
        Return ReadLong(ReadLong(ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_ITEMB) _
        + (KO_OFF_ITEMS + (4 * Slot))) + &H6C))
    End Function
    Public Function getItemIDInv(Optional ByVal Slot As Integer = 0) As Long
        On Error Resume Next
        Slot = Slot + KO_OFF_INVROW + KO_OFF_ITEMROW
        Return ReadLong(ReadLong(ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_ITEMB) _
        + (KO_OFF_ITEMS + (4 * Slot))) + &H68)) _
        + ReadLong(ReadLong(ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_ITEMB) _
        + (KO_OFF_ITEMS + (4 * Slot))) + &H6C))
    End Function
    Public Function getItemCountSlot(Optional ByVal Slot As Integer = 0) As Integer
        Slot = Slot + KO_OFF_ITEMROW
        Return ReadLong(ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_ITEMB) + (KO_OFF_ITEMS + (4 * Slot))) + &H70)
    End Function
    Public Function getItemDurab(Optional ByVal DurRow As Integer = 1) As Integer
        DurRow = DurRow + KO_OFF_ITEMROW
        Return ReadLong(ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_ITEMB) + (KO_OFF_ITEMS + (4 * DurRow))) + &H74)
    End Function
    Public Function getItemIDCountInv(ByVal ItemID As Long)
        Dim i As Integer, result As Integer = 0
        For i = 0 To 27
            If getItemIDInv(i) = ItemID Then
                result = getItemCountSlot(i + KO_OFF_INVROW)
                Exit For
            End If
        Next
        Return result
    End Function
    Public Function findItemSlot(ItemID As String) As Integer
        Dim i As Integer, result As Integer = -1
        For i = KO_OFF_INVROW To KO_OFF_INVCOUNT
            If getItemID(i) = ItemID Then
                result = ((i - KO_OFF_INVROW))
                Exit For
            End If
        Next
        Return result
    End Function
    Public Sub removeItem(CopItemID As String)
        If getItemSlotInv(CopItemID) > 0 Then
            Dim ItemBs As Long, ItemIDBs As Long
            ItemBs = ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_ITEMB)
            ItemIDBs = getItemSlotInv(CopItemID) - KO_OFF_INVROW
            WriteByte(KO_ITEMDES, 1)
            WriteLong(KO_ITEMDES + &H4, ItemIDBs)
            WriteLong(KO_ITEMDES + &H8, findItemBase(ItemIDBs + KO_OFF_INVROW))
            WriteByte(KO_ITEMDES2, 1)
            WriteLong(KO_ITEMDES2 + &H8, findItemBase(ItemIDBs + KO_OFF_INVROW))
            WriteByte(KO_ITEMDES2 + &HC, 1)
            WriteLong(KO_ITEMDES2 + &H10, ItemIDBs)
            WriteByte(KO_ITEMDES2 + &H14, 1)
            ExecuteRemoteCode(ConvHEX2ByteArray("60B9" & ADWORD((ItemBs), 8) & "BF" & ADWORD((KO_ITEMDESCALL), 8) & "FFD761C3"))
            SendPack("6A02")
        End If
    End Sub
    Public Function findItemBase(ByVal EAX As Long) As Long
        Dim ESI As Long
        Dim ECX As Long
        ESI = ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_ITEMB)
        ECX = ReadLong(ESI + KO_OFF_ITEMS + (4 * EAX))
        findItemBase = ECX
    End Function
    Public Sub readInventory()
        For i As Integer = 0 To KO_OFF_INVCOUNT
            ItemIntID(i) = getItemID(i)
        Next
    End Sub
    Public Function getItemSlotInv(ByVal ItemID As String) As Integer
        For i As Integer = KO_OFF_INVROW To KO_OFF_INVCOUNT
            If getItemID(i) = CLng(ItemID) Then
                Return i
            End If
        Next
        Return 0
    End Function
    Public Function getEmptySlotCountInv() As Integer
        Dim result As Integer = 0
        For i As Integer = KO_OFF_INVROW To KO_OFF_INVCOUNT
            If getItemID(i) = 0 Then result = result + 1
        Next
        Return result
    End Function
    Public Function getFirstEmptySlotInv() As Integer
        For i As Integer = KO_OFF_INVROW To KO_OFF_INVCOUNT
            If getItemID(i) = "0" Then Return ((i - KO_OFF_INVROW))
        Next
        Return -1
    End Function
#End Region
#Region "Banka İtem Fonksiyonları"
    Public Sub sendBankItem(ByVal ItemID As String, ByVal Page As Integer, ByVal InvSlot As Integer, ByVal BankSlot As Integer, ByVal ItemCount As Integer)
        Dim InnID, InnPageHex, InvSlotHex, InnSlotHex, InnItemCount As String
        InnID = findIDtoNPC("Neria")
        InnPageHex = FormatHex(Hex(Page), 2)
        InvSlotHex = FormatHex(Hex(InvSlot - KO_OFF_INVROW), 2)
        InnSlotHex = FormatHex(Hex(BankSlot), 2)
        InnItemCount = FormatHex(Hex(ItemCount), 4)
        SendPack("4502" & InnID & ItemID & InnPageHex & InvSlotHex & InnSlotHex & InnItemCount & "0000")
        SendPack("6A02")
    End Sub
    Public Sub sendItemsToBank(ByVal chkl_UpList As CheckedListBox, ByVal RowItem As Integer, ByRef rowList As ListBox)
        ReadInn()
        Sleep(1000)
        ReadInn()
        Sleep(1000)
        Dim InnPage As Integer
        Dim InnSlot As Integer
        For i As Integer = KO_OFF_INVROW To 41
            For j As Integer = 0 To 191
                If (getItemID(i) > 0 And getItemUpExt(i) >= RowItem And chkl_UpList.GetItemChecked(i - KO_OFF_INVROW) = True And ItemINNID(j) = 0) Then
                    Select Case j
                        Case 0 To 23
                            InnPage = 0
                            InnSlot = j
                        Case 24 To 47
                            InnPage = 1
                            InnSlot = j - 24
                        Case 48 To 71
                            InnPage = 2
                            InnSlot = j - 48
                        Case 72 To 95
                            InnPage = 3
                            InnSlot = j - 72
                        Case 96 To 119
                            InnPage = 4
                            InnSlot = j - 96
                        Case 120 To 143
                            InnPage = 5
                            InnSlot = j - 120
                        Case 144 To 167
                            InnPage = 6
                            InnSlot = j - 144
                        Case 168 To 191
                            InnPage = 7
                            InnSlot = j - 168
                    End Select
                    Sleep(200)
                    If (Left(getItemID(i), 9) <> 111131100) Then
                        sendBankItem(ADWORD(getItemID(i), 8), InnPage, i, InnSlot, getItemCountSlot(i))
                    Else
                        sendBankItem("B" & Right(getItemID(i), 1) & "3E3D42", InnPage, i, InnSlot, getItemCountSlot(i))
                    End If
                    ItemINNID(j) = getItemID(i)
                    Sleep(200)
                    Exit For
                End If
            Next
        Next
        ReadInn()
        Sleep(2000)
        rowList.SelectedIndex += 1
    End Sub
    Public Function getItemIDToBank(ByVal Slot As Integer) As Long
        Dim a, b, c As Long
        a = ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_BANKB)
        b = ReadLong(a + KO_OFF_BANKS + (4 * Slot))
        c = ReadLong(b + &H68)
        c = ReadLong(c)
        Return c
    End Function
    Public Sub ReadInn()
        SendPack("2001" & findIDtoNPC("Neria") & "FFFFFFFF")
        SendPack("4501" & findIDtoNPC("Neria"))
        SendPack("2001" & findIDtoNPC("Neria") & "FFFFFFFF")
        For i As Integer = 0 To 191
            ItemINNID(i) = getItemIDToBank(i)
        Next
    End Sub
    Public Function getReadInn(ByVal ItemID As String) As Integer
        Dim a As Long, result As Integer
        result = -1
        ReadInn()
        For i As Integer = 0 To 191
            a = InStr(1, ItemINNID(i), ItemID, CompareMethod.Text)
            If a <> 0 Then
                result = i
                Exit For
                Exit Function
            End If
            result = -1
        Next
        Return result
    End Function
    Public Function getFirstEmptySlotBank() As Integer
        For i As Integer = 0 To 191
            If getItemIDToBank(i) = 0 Then Return i
        Next
        Return -1
    End Function
    Public Function checkBank() As Boolean
        If ReadByte(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_BANKB) + KO_OFF_BANKCONT) = 1 Then Return True
        Return False
    End Function
#End Region
#Region "Skill Fonksiyonları"
    Public Function getSkills(ByVal SkillNo As Integer) As Integer
        Dim Ptr As Long, tmpBase As Long
        Ptr = ReadLong(KO_PTR_DLG)
        tmpBase = ReadLong(Ptr + KO_OFF_SKILLBASE)
        tmpBase = ReadLong(tmpBase + &H4)
        tmpBase = ReadLong(tmpBase + KO_OFF_SKILLID)
        For i As Integer = 1 To SkillNo
            tmpBase = ReadLong(tmpBase + &H0)
        Next
        tmpBase = ReadLong(tmpBase + &H8)
        If tmpBase > 0 Then
            tmpBase = ReadLong(tmpBase + &H0)
            Return tmpBase
        Else
            Return 0
        End If
    End Function
    Public Function getSkillCount() As Integer
        Return ReadLong(ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_SKILLBASE) + &H4) + KO_OFF_SKILLID + &H4)
    End Function
    Public Function checkSkillID(ByVal skillCode As Long) As Boolean
        Dim result As Boolean
        result = False
        For i As Integer = 1 To getSkillCount()
            If Right(getSkills(i), 3) = skillCode Then
                result = True
                Exit For
                Exit Function
            End If
            If i = 20 Then
                result = False
            End If
        Next
        Return result
    End Function
    Public Function checkTS(ByVal skillCode As Long) As Boolean
        Dim result As Boolean
        result = False
        For i As Integer = 1 To getSkillCount()
            If Left(getSkills(i), 3) = skillCode Then
                result = True
                Exit For
                Exit Function
            End If
            If i = 20 Then
                result = False
            End If
        Next
        Return result
    End Function
#End Region
#Region "Genel Fonksiyonlar"
    Public Function GetBase(ByVal BaseMobID As Long) As Long
        Dim xStr As String, TargetBase As Long, result As Long
        If BaseMobID > 0 Then
            If BaseMobID > 9999 Then
                TargetBase = KO_FMBS
            Else
                TargetBase = KO_FPBS
            End If
            xStr = "608B0D" & ADWORD(KO_FLDB) & "6A01" & "68" & ADWORD(BaseMobID) & "BF" & ADWORD(TargetBase) & "FFD7" & "A3" & ADWORD(ByteMob_Base) & "61C3"
            ExecuteRemoteCode(ConvHEX2ByteArray(xStr))
            result = ReadLong(ByteMob_Base)
        End If
        Return result
    End Function
    Public Function getDistanceToTarget(ByVal CharkorX As Integer, ByVal CharkorY As Integer, ByVal HedefX As Integer, ByVal HedefY As Integer) As Integer
        Return CInt(Math.Sqrt(Math.Pow(Math.Abs(CharkorX - HedefX), 2) + Math.Pow(Math.Abs(CharkorY - HedefY), 2)))
    End Function
    Public Function getDistance(ByVal HedefX As Integer, ByVal HedefY As Integer) As Double
        Return CDbl(Math.Sqrt(Math.Pow(Math.Abs(getCharX() - HedefX), 2) + Math.Pow(Math.Abs(getCharY() - HedefY), 2)))
    End Function
#End Region
#Region "Karakter Bilgi Fonksiyonları"
    Public Function getCharGold() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_GOLD)
    End Function
    Public Function getCharStat(ByVal Stat As Integer) As Integer
        Dim result As Integer = 0
        Select Case Stat
            Case 0 'Boş Stat Point
                result = ReadLong(ReadLong(KO_PTR_CHR) + &HB54)
            Case 1 'Str
                result = ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_StatSTR)
            Case 2 'Hp
                result = ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_StatHP)
            Case 3 'Dex
                result = ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_StatDEX)
            Case 4 'Int
                result = ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_StatINT)
            Case Else 'Mp
                result = ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_StatMP)
        End Select
        Return result
    End Function
    Public Function getCharName() As String
        If ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_NAMELEN) > 15 Then
            Return ReadString(ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_NAME))
        Else
            Return ReadString(ReadLong(KO_PTR_CHR) + KO_OFF_NAME)
        End If
    End Function
    Public Function getCharID() As String
        Return ADWORD((ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_ID)), 4)
    End Function
    Public Function getCharLongID() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_ID)
    End Function
    Public Function getLastMP() As Integer
        Return (Val(getCharMaxMP()) - Val(getCharMP()))
    End Function
    Public Function getLastHP() As Integer
        Return (Val(getCharMaxHP()) - Val(getCharHP()))
    End Function
    Public Function getCharMP() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_MP)
    End Function
    Public Function getCharMaxMP() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_MAXMP)
    End Function
    Public Function getCharEXP() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_EXP)
    End Function
    Public Function getCharMaxEXP() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_MAXEXP)
    End Function
    Public Function getCharHP() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_HP)
    End Function
    Public Function GetSpeedChar() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT)
    End Function
    Public Function GetCharSpeed() As Integer
        If (checkSkillID("010") Or checkSkillID("002")) Then
            Return 16320
        ElseIf (checkSkillID("725")) Then
            Return 16384
        Else
            Return 16256
        End If
    End Function
    Public Sub SetCharSpeed(ByVal normal As Boolean)
        If (normal) Then
            WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT, GetCharSpeed())
        Else
            WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT, 0)
        End If
    End Sub
    Public Function getCharSkillPoint(ByVal skillRow As Integer) As Integer
        Select Case skillRow
            Case 0
                Return ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_SBARBase) + KO_OFF_BSkPoint)
            Case 1
                Return ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_SBARBase) + KO_OFF_SPoint1)
            Case 2
                Return ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_SBARBase) + KO_OFF_SPoint2)
            Case 3
                Return ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_SBARBase) + KO_OFF_SPoint3)
            Case 4
                Return ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_SBARBase) + KO_OFF_SPoint4)
            Case Else
                Return 0
        End Select
    End Function
    Public Function getCharMaxHP() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_MAXHP)
    End Function
    Public Function getCharX() As Integer
        Return ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X)
    End Function
    Public Function getCharY() As Integer
        Return ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y)
    End Function
    Public Function getCharZ() As Integer
        Return ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z)
    End Function
    Public Function getCharLevel() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_LEVEL)
    End Function
    Public Function getCharClass() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_CLASS)
    End Function
    Public Function getCharUndyHp(CharHP As Long) As Integer
        Return CInt(CharHP * 60 / 100)
    End Function
    Public Function getMouseX() As Integer
        Return ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_GoX)
    End Function
    Public Function getMouseY() As Integer
        Return ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_GoY)
    End Function
    Public Function getCharClassName() As String
        Dim CLS As Long
        CLS = ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_CLASS)
        Select Case CLS
            Case 101, 105, 106, 205, 206
                Return "Warrior"
            Case 102, 107, 108, 207, 208
                Return "Rogue"
            Case 103, 109, 110, 209, 210
                Return "Mage"
            Case Else
                Return "Priest"
        End Select
    End Function
    Public Function getCharIrk() As String
        If (ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_NT) = 1) Then Return "Karus"
        Return "Human"
    End Function
    Public Function GetCharZoneId() As Integer
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_ZONE)
    End Function
    Public Function getCharZone()
        Dim Zone As Long, result As String
        Zone = ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_ZONE)
        Select Case Zone
            Case 1, 5, 6 : result = "Lüferson"
            Case 2, 7, 8 : result = "El Morad"
            Case 11, 13 : result = "Karus - Eslant"
            Case 12, 14 : result = "Human - Eslant"
            Case 21, 22, 23 : result = "Moradon"
            Case 30 : result = "Delos"
            Case 32 : result = "Abys"
            Case 34 : result = "Felankor’s Lair"
            Case 48 : result = "Arena"
            Case 71 : result = "Ronark Land"
            Case 72 : result = "Ardream"
            Case 73 : result = "Ronark Land Base"
            Case 82 : result = "Adonis"
            Case 85 : result = "Chaos"
            Case Else : result = Zone
        End Select
        Return result
    End Function
    Public Function CharRogueJob() As Boolean
        If (getItemID(7) > 0 And getItemID(9) > 0) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function CharIsTown() As Boolean
        '_______________Moradon__________________________Lüferson________________________Elmorad_________________________Karus Eslant____________________Karus CZ_________________________Humman CZ______________________Human Eslant
        If (getDistance(816, 530) < 40) Or (getDistance(450, 1628) < 40) Or (getDistance(1589, 417) < 40) Or (getDistance(532, 549) < 40) Or (getDistance(1383, 110) < 40) Or (getDistance(622, 931) < 40) Or (getDistance(536, 551) < 40) Then
            Return True
        End If
        Return False
    End Function
#End Region
#Region "Pet Bilgi Fonksiyonları"
    Public Function GetPetId() As String
        'ADWORD((ReadLong(ReadLong(KO_PTR_CHR) + &HFD8)), 4) 'PetID
        'ReadByte(ReadLong(ReadLong(KO_PTR_DLG) + &H3A4) + &H199)
        'MaxHP = &H19C
        'HP = &H19E
        'hunry = 1A4
        'hunry = 1A8
        '
        For j As Integer = 400 To 1000
            For i As Integer = 100 To 1000
                If (ReadByte(ReadLong(ReadLong(KO_PTR_DLG) + j) + i) = 71) Then
                    Console.WriteLine("j=" & Hex(j) & " i=" & Hex(i))
                End If
            Next
        Next


        '1A2
        '1A0
        'Console.WriteLine(Cli.GetPetId())
        'Console.WriteLine(Cli.GetPetHp())
        'Console.WriteLine(Cli.GetPetMaxHp())
        'Console.WriteLine(Cli.GetPetMp())
        'Console.WriteLine(Cli.GetPetMaxMp())
        'Console.WriteLine(Cli.GetPetLevel())
        'Console.WriteLine(Cli.GetPetHungry())
        Return ADWORD((ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_PET_ID)), 4)
    End Function
    Public Function GetPetHp() As Integer
        Return ReadByte(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_PET_BASE) + KO_OFF_PET_HP)
    End Function
    Public Function GetPetMaxHp() As Integer
        Return ReadByte(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_PET_BASE) + KO_OFF_PET_MAXHP)
    End Function
    Public Function GetPetMp() As Integer
        Return ReadByte(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_PET_BASE) + KO_OFF_PET_MP)
    End Function
    Public Function GetPetMaxMp() As Integer
        Return ReadByte(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_PET_BASE) + KO_OFF_PET_MAXMP)
    End Function
    Public Function GetPetLevel() As Integer
        Return ReadByte(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_PET_BASE) + KO_OFF_PET_LEVEL)
    End Function
    Public Function GetPetHungry() As Integer
        Return ReadByte(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_PET_BASE) + KO_OFF_PET_HUNGRY)
    End Function
#End Region
#Region "Karakter Client Priest Kontrol"
    Public Function CharStrStatu() As Boolean
        Dim result As Boolean
        result = False
        If (checkSkillID("004") = True) Then
            result = True
        End If
        Return result
    End Function
    Public Function CharResisStatu() As Boolean
        Dim result As Boolean
        result = False
        If (checkSkillID("609") = True Or checkSkillID("627") = True Or checkSkillID("636") = True Or checkSkillID("645") = True) Then
            result = True
        End If
        Return result
    End Function
    Public Function CharAccStatu() As Boolean
        Dim result As Boolean
        result = False
        If (checkSkillID("603") = True Or checkSkillID("612") = True Or checkSkillID("621") = True Or checkSkillID("630") = True Or checkSkillID("639") = True Or checkSkillID("651") = True Or checkSkillID("660") = True Or checkSkillID("674") = True Or checkSkillID("676") = True) Then
            result = True
        End If
        Return result
    End Function
    Public Function CharBuffStatu() As Boolean
        Dim result As Boolean
        result = False
        If (checkSkillID("606") = True Or checkSkillID("615") = True Or checkSkillID("624") = True Or checkSkillID("633") = True Or checkSkillID("642") = True Or checkSkillID("654") = True Or checkSkillID("655") = True Or checkSkillID("656") = True Or checkSkillID("657") = True Or checkSkillID("670") = True Or checkSkillID("672") = True Or checkSkillID("678") = True) Then
            result = True
        End If
        Return result
    End Function
    Public Function CharRestoreStatu() As Boolean
        Dim result As Boolean
        result = False
        If (checkSkillID("503") = True Or checkSkillID("512") = True Or checkSkillID("521") = True Or checkSkillID("530") = True Or checkSkillID("539") = True Or checkSkillID("548") = True Or checkSkillID("570") = True Or checkSkillID("575") = True Or checkSkillID("580") = True) Then
            result = True
        End If
        Return result
    End Function
    Public Function CharCureStatu() As Boolean
        Dim result As Boolean
        result = False
        If (checkSkillID("703") = True Or checkSkillID("715") = True Or checkSkillID("724") = True Or checkSkillID("745") = True Or checkSkillID("754") = True Or checkSkillID("760") = True) Then
            result = True
        End If
        Return result
    End Function
    Public Function CharDiseStatu() As Boolean
        Dim result As Boolean
        result = False
        If (checkSkillID("580") = True Or checkSkillID("680") = True Or checkSkillID("780") = True) Then
            result = True
        End If
        Return result
    End Function
#End Region
#Region "Düşman Bilgi Fonksiyonları"
    Public Function getMobStatu(ByVal MobBase As Long) As Integer
        Return ReadByte(MobBase + &H2A0)
    End Function
    Public Function getMobID() As Long
        Return ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_MOB)
    End Function
    Public Function getMobIDHex() As String
        Return ADWORD((ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_MOB)), 4)
    End Function
    Public Function GetXFromBase(ByVal baseId As Integer) As Integer
        Return ReadFloat(baseId + KO_OFF_X)
    End Function
    Public Function GetYFromBase(ByVal baseId As Integer) As Integer
        Return ReadFloat(baseId + KO_OFF_Y)
    End Function
    Public Function getBaseIDX(ByVal OkunanIDX As Long) As Integer
        Dim result As Integer
        result = 0
        If OkunanIDX > 0 Then
            result = ReadFloat(GetLootBase(OkunanIDX) + KO_OFF_X)
        End If
        Return result
    End Function
    Public Function getBaseIDY(ByVal OkunanIDY As Long) As Integer
        Dim result As Integer
        result = 0
        If OkunanIDY > 0 Then
            result = ReadFloat(GetLootBase(OkunanIDY) + KO_OFF_Y)
        End If
        Return result
    End Function
    Public Function getBaseIDZ(ByVal OkunanIDZ As Long) As Integer
        Dim result As Integer
        result = 0
        If OkunanIDZ > 0 Then
            result = ReadFloat(GetLootBase(OkunanIDZ) + KO_OFF_Z)
        End If
        Return result
    End Function
    Public Function getBaseIDHP(ByVal OkunanIDHP As Long) As Integer
        Dim result As Integer
        result = 0
        If OkunanIDHP > 0 Then
            result = ReadLong(GetBase(OkunanIDHP) + KO_OFF_HP)
        End If
        Return result
    End Function
    Public Function getBaseIDNat(ByVal OkunanIDNAT As Long) As Integer
        Dim result As Integer
        result = 0
        If OkunanIDNAT > 0 Then
            result = ReadLong(getBase(OkunanIDNAT) + KO_OFF_NT)
        End If
        Return result
    End Function
    Public Function getFunctionBaseName(ByVal BaseADDD As Long) As String
        Dim result As String
        If ReadLong(BaseADDD + KO_OFF_NAMELEN) > 15 Then
            result = ReadString(ReadLong(BaseADDD + KO_OFF_NAME))
        Else
            result = ReadString(BaseADDD + KO_OFF_NAME)
        End If
        Return result
    End Function
    Public Function getBaseIDName(ByVal targetID As Long) As String
        If targetID > 0 Then
            If ReadLong(getBase(targetID) + KO_OFF_NAMELEN) > 15 Then
                Return ReadString(ReadLong(getBase(targetID) + KO_OFF_NAME))
            Else
                Return ReadString(getBase(targetID) + KO_OFF_NAME)
            End If
        End If
        Return "NuLL"
    End Function
    Public Function getbMobName() As String
        If getMobID() > 0 Then
            If ReadLong(getBase(getMobID) + KO_OFF_NAMELEN) > 15 Then
                Return ReadString(ReadLong(getBase(getMobID) + KO_OFF_NAME))
            Else
                Return ReadString(getBase(getMobID) + KO_OFF_NAME)
            End If
        End If
        Return "NuLL"
    End Function
    Public Function getMobDistance() As Double
        On Error Resume Next
        If getMobID() <= 0 Then getMobDistance = 255.0 : Exit Function
        Return CDbl(getDistance(getMobX(), getMobY()))
    End Function
    Public Function getMobSkillCoor() As String
        Return ADWORD((getMobX()), 4) & ADWORD((getMobZ()), 4) & ADWORD((getMobY() - 1), 4)
        '0A03 0500 9F01'000305009F01
    End Function
    Public Function getCharSkillCoor() As String
        Return ADWORD((getCharX()), 4) & ADWORD((4), 4) & ADWORD((getCharY()), 4)
    End Function
    Function getMobX() As Long
        Return ReadFloat(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_MCOR) + KO_OFF_MCORX)
    End Function
    Function getMobY() As Long
        Return ReadFloat(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_MCOR) + KO_OFF_MCORY)
    End Function
    Function getMobZ() As Long
        Return ReadFloat(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_MCOR) + KO_OFF_MCORZ)
    End Function
#End Region
#Region "Karakter İşlem Fonksiyonları"
    Public Sub SpeedLe()
        Dim sHook As String = "E9" & ADWORD(getCallDiff(KO_SPD_HOOK, SpeedSlot)) & "9090"
        InjectPatch(KO_SPD_HOOK, sHook)
        SpeedEsLe()
    End Sub
    Public Sub SpeedEsLe()
        Dim sHook As String = "508BC183C0078038007413807801FF7505C600" & _
            "E2EB0880382D7403C6002D90807801FF7516" & _
            "C705" & ADWORD(KO_SH_VALUE) & ADWORD(SingleToHex(DefaultTimer)) & _
            "C705" & ADWORD(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT) & ADWORD(DefaultHiz) & _
            "EB14" & _
            "C705" & ADWORD(KO_SH_VALUE) & ADWORD(SingleToHex(SeriTimer)) & _
            "C705" & ADWORD(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT) & ADWORD(SeriHiz) & _
            "58518B0D" & ADWORD(KO_PTR_PKT) & "E9"
        sHook += ADWORD(getCallDiff(SpeedSlot + (sHook.Length / 2), KO_SPD_HOOK + 7))
        InjectPatch(SpeedSlot, sHook)
    End Sub
    Public Sub SpeedAl()
        Dim sHook As String = "518B0D" + ADWORD(KO_PTR_PKT)
        WriteFloat(KO_SH_VALUE, DefaultTimer)
        InjectPatch(KO_SPD_HOOK, sHook)
        WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT, DefaultHiz)
    End Sub
    Function SingleToHex(ByVal Tmp As Single) As Long
        Dim b As Byte()
        b = BitConverter.GetBytes(Tmp)
        Array.Reverse(b)
        Dim sb As StringBuilder = New StringBuilder
        For Each item As Byte In b
            sb.Append(item.ToString("X2"))
        Next
        Return Convert.ToInt32(sb.ToString(), 16)
    End Function
    Public Sub CharLoced()
        WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT, 0)
    End Sub
    Public Sub CharUnLoced()
        WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT, 16256)
    End Sub
    Public Sub WallHackOpen()
        WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_WH, 0)
    End Sub
    Public Sub WallHackClose()
        WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_WH, 1)
    End Sub
    Public Sub tpChar(crx As Single, cry As Single)
        On Error Resume Next
        Dim zipla, X, Y, uzak, A, b, D, e, i, isrtx, isrty As Double
        Dim tx As Single, ty As Single
        Dim x1 As Single, y1 As Single
        Dim bykx, byky, kckx, kcky
        WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT, 0)
        zipla = 1
        tx = ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X)
        ty = ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y)
        X = Math.Abs(crx - tx)
        Y = Math.Abs(cry - ty)
        If tx > crx Then isrtx = -1 : bykx = tx : kckx = crx Else isrtx = 1 : bykx = crx : kckx = tx
        If ty > cry Then isrty = -1 : byky = ty : kcky = cry Else isrty = 1 : byky = cry : kcky = ty
        uzak = Int(Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)))
        If uzak > 100 Or crx <= 0 Or cry <= 0 Then Exit Sub
        For i = zipla To uzak Step zipla
            A = Math.Pow(i, 2) * Math.Pow(X, 2)
            b = Math.Pow(X, 2) + Math.Pow(Y, 2)
            D = Math.Sqrt(A / b)
            e = Math.Sqrt(Math.Pow(i, 2) - Math.Pow(D, 2))
            x1 = Int(tx + isrtx * D)
            y1 = Int(ty + isrty * e)
            If (kckx <= x1 And x1 <= bykx) And (kcky <= y1 And y1 <= byky) Then
                WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X, x1)
                WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y, y1)
                WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z, ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z))
                SendPack("06" _
                         & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X)) * 10, 4) _
                         & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y)) * 10, 4) _
                         & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z)) * 10, 4) _
                         & "2DFFFF" _
                         & ADWORD(CInt(getCharX()) * 10, 4) _
                         & ADWORD(CInt(getCharY()) * 10, 4) _
                         & ADWORD(CInt(5) * 10, 4))
            End If
        Next
        WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT, 16256)
    End Sub
    Public Sub tpCharGo(crx As Single, cry As Single, Optional ByVal CharWalk As Boolean = False)
        On Error Resume Next
        Dim zipla, X, Y, uzak, A, b, D, e, i, isrtx, isrty As Double
        Dim tx As Single, ty As Single
        Dim x1 As Single, y1 As Single
        Dim bykx, byky, kckx, kcky
        WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT, 0)
        zipla = 5
        tx = ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X)
        ty = ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y)
        X = Math.Abs(crx - tx)
        Y = Math.Abs(cry - ty)
        If tx > crx Then isrtx = -1 : bykx = tx : kckx = crx Else isrtx = 1 : bykx = crx : kckx = tx
        If ty > cry Then isrty = -1 : byky = ty : kcky = cry Else isrty = 1 : byky = cry : kcky = ty
        uzak = CInt(Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)))
        'If uzak > 100 Or crx <= 0 Or cry <= 0 Then Exit Sub
        For i = zipla To uzak Step zipla
            A = Math.Pow(i, 2) * Math.Pow(X, 2)
            b = Math.Pow(X, 2) + Math.Pow(Y, 2)
            D = Math.Sqrt(A / b)
            e = Math.Sqrt(Math.Pow(i, 2) - Math.Pow(D, 2))
            x1 = CInt(tx + isrtx * D)
            y1 = CInt(ty + isrty * e)
            If (kckx <= x1 And x1 <= bykx) And (kcky <= y1 And y1 <= byky) Then
                WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X, x1)
                WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y, y1)
                WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z, ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z))
                SendPack("06" _
                         & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X)) * 10, 4) _
                         & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y)) * 10, 4) _
                         & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z)) * 10, 4) _
                         & "2D0000" _
                         & ADWORD(getCharX() * 10, 4) _
                         & ADWORD(getCharY() * 10, 4) _
                         & ADWORD(getCharZ() * 10, 4))
            End If
        Next
        If (uzak > 1 And CharWalk) Then
            walkChar(crx, cry)
        End If
        WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_SWIFT, 16256)
    End Sub
    Public Sub tpCharStep(crx As Single, cry As Single)
        If CSng(getCharX()) = crx And CSng(getCharY()) = cry Then Exit Sub
        Dim FarkX As Long, FarkY As Long
        Dim ZıplaX As Integer, ZıplaY As Integer, i As Integer
        FarkX = crx - getCharX()
        FarkY = cry - getCharY()
        ZıplaX = 5
        ZıplaY = 5
        For i = 1 To 5
            If FarkX = -1 * i Or FarkX = i Then
                ZıplaX = 1
            ElseIf FarkY = -1 * i Or FarkY = i Then
                ZıplaY = 1
            End If
        Next i
        Dim oAnkiX As Long, oAnkiY As Long
        oAnkiX = getCharX()
        oAnkiY = getCharY()
        If FarkX <> 0 Or FarkY <> 0 Then
            If FarkX < 0 Then
                WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X, getCharX() - ZıplaX)
            ElseIf FarkX > 0 Then
                WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X, getCharX() + ZıplaX)
            End If
            If FarkY < 0 Then
                WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y, getCharY() - ZıplaY)
            ElseIf FarkY > 0 Then
                WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y, getCharY() + ZıplaY)
            End If
            WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z, getCharZ())
            Dim RetX As Long, RetY As Long
            RetX = getCharX()
            RetY = getCharY()
            SendPack("06" _
         & ADWORD(CInt(oAnkiX * 10), 4) _
         & ADWORD(CInt(oAnkiY * 10), 4) _
         & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z)) * 10, 4) _
         & "2D0000" _
         & ADWORD(CInt(RetX) * 10, 4) _
         & ADWORD(CInt(RetY) * 10, 4) _
         & ADWORD(CInt(5) * 10, 4))
        End If
    End Sub
    Public Sub walkChar(ByVal GoX As Integer, ByVal GoY As Integer)
        If getCharX() <> GoX Or getCharY() <> GoY Then
            WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_MOVE, 1)
            WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_GoX, Val(GoX))
            WriteFloat(ReadLong(KO_PTR_CHR) + KO_OFF_GoY, Val(GoY))
            WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_MOVEType, 2)
        End If
    End Sub
    Public Sub AutoMobZ()
        Dim xStr As String
        xStr = "608B0D" & ADWORD((KO_PTR_DLG), 8) & "BF" & ADWORD((&H526B80), 8) & "FFD761C3"
        ExecuteRemoteCode(ConvHEX2ByteArray(xStr))
    End Sub
    Public Sub goKorText(ByVal goTextKor As String, ByVal goTP As Boolean, Optional ByVal tpCoor As Boolean = True)
        Dim xKor As Integer = getCoorText(goTextKor, 0)
        Dim yKor As Integer = getCoorText(goTextKor, 1)
        If (xKor <> getCharX() Or yKor <> getCharY()) Then
            If (goTP = True) Then
                If (tpCoor) Then
                    tpChar(xKor, yKor)
                Else
                    tpCharGo(xKor, yKor)
                End If
            Else
                walkChar(xKor, yKor)
            End If
        End If
    End Sub
#End Region
#Region "Party Fonksiyonları"
    Public Sub getPartyInformation()
        Dim tmpBase As Long, pt_count As Long
        pt_count = ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_PtBase) + KO_OFF_PtCount)
        If (pt_count <= 0) Then Exit Sub
        For i = 1 To pt_count
            PartyUserID(i) = 0
            PartyUserTargetID(i) = 0
            PartyUserHP(i) = 0
            PartyUserMaxHP(i) = 0
            PartyUserHPFark(i) = 0
            PartyUserCure(i) = 0
            PartyUserDisease(i) = False
            PartyUserX(i) = 0
            PartyUserY(i) = 0
            PartyUserZ(i) = 0
            PartyUserUzaklık(i) = 0
        Next
        If pt_count > 0 Then
            For i = 1 To pt_count
                For j = 1 To i
                    If tmpBase = 0 Then
                        tmpBase = ReadLong(ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_PtBase) + KO_OFF_Pt))
                    Else
                        tmpBase = ReadLong(tmpBase)
                    End If
                Next
                PartyUserID(i) = ADWORD((ReadLong(tmpBase + &H8)), 4)
                PartyUserTargetID(i) = ReadLong(tmpBase + &H8)
                PartyUserHP(i) = ReadLong(tmpBase + &H14)
                PartyUserMaxHP(i) = ReadLong(tmpBase + &H18)
                PartyUserHPFark(i) = CInt(PartyUserMaxHP(i) - PartyUserHP(i))
                PartyUserCure(i) = ReadLong(tmpBase + &H25)
                PartyUserDisease(i) = ReadLong(tmpBase + &H26)
                If (PartyUserID(i) <> getCharID()) Then
                    PartyUserX(i) = ReadFloat(GetBase(PartyUserTargetID(i)) + KO_OFF_X)
                    PartyUserY(i) = ReadFloat(GetBase(PartyUserTargetID(i)) + KO_OFF_Y)
                    PartyUserZ(i) = ReadFloat(GetBase(PartyUserTargetID(i)) + KO_OFF_Z)
                    PartyUserUzaklık(i) = getDistance(PartyUserX(i), PartyUserY(i))
                    Console.WriteLine(ReadStringChat(ReadLong(tmpBase + &H30)))
                Else
                    PartyUserX(i) = getCharX()
                    PartyUserY(i) = getCharY()
                    PartyUserZ(i) = getCharZ()
                    PartyUserUzaklık(i) = 0
                End If
                tmpBase = 0
            Next
        End If
    End Sub
    Public Function getPartyCount() As Integer
        Return ReadLong(ReadLong(ReadLong(KO_PTR_DLG) + KO_OFF_PtBase) + KO_OFF_PtCount)
    End Function
#End Region
#Region "Etraf Düşman Okutma"
    Public Function getStatuInMob(ByVal mobBase As Integer) As Integer
        Return ReadLong(mobBase + &H3E4)
    End Function
    Public Sub selectMob(ByVal selMob As Long, Optional ByVal ManuelMob As Boolean = False)
        If (ManuelMob = True) Then
            zMobHP = getBaseIDHP(selMob)
            zMobID = selMob
            zMobName = getBaseIDName(selMob)
            zMobX = getMobX()
            zMobY = getMobY()
            zMobZ = getMobZ()
            zMobDistance = getMobDistance()
        Else
            WriteLong(ReadLong(KO_PTR_CHR) + KO_OFF_MOB, selMob)
        End If
    End Sub
    Public Sub getZMob(ByVal MobTur As Integer, Optional ByVal MobKontrol As String = "Genel")
        Dim EBP As Long, ESI As Long, EAX As Long, FEnd As Long
        Dim LDist As Long, CrrDist As Long, LID As Long, LBase As Long, MobLong As Long, SelNT As Long
        Dim base_addr As Long, Exit_while As Long
        LDist = 999
        zMobName = ""
        zMobID = 0
        zMobX = 0
        zMobY = 0
        zMobZ = 0
        zMobDistance = 0
        zMobStatu = 0
        zMobMove = 0
        CrrDist = 0
        LBase = 0
        zMobBase = 0
        If MobKontrol = "Karşı" Then
            MobLong = &H40
        Else
            MobLong = &H34
        End If
        If MobTur = 1 Then
            SelNT = 3
        Else
            SelNT = 0
        End If
        EBP = ReadLong(ReadLong(KO_FLDB) + MobLong)
        FEnd = ReadLong(ReadLong(EBP + 4) + 4)
        ESI = ReadLong(EBP)
        Exit_while = GetTickCount()
        While ESI <> EBP
            base_addr = ReadLong(ESI + &H10)
            If Exit_while + 50 < GetTickCount() Then Exit While
            If base_addr = 0 Then Return : Exit Sub
            EAX = ReadLong(ESI + 8)
            If ReadLong(ESI + 8) <> FEnd Then
                While ReadLong(EAX) <> FEnd
                    EAX = ReadLong(EAX)
                    If Exit_while + 50 < GetTickCount() Then Exit While
                End While
                ESI = EAX
            Else
                EAX = ReadLong(ESI + 4)
                While ESI = ReadLong(EAX + 8)
                    ESI = EAX
                    EAX = ReadLong(EAX + 4)
                    If Exit_while + 50 < GetTickCount() Then Exit While
                End While
                If ReadLong(ESI + 8) <> EAX Then
                    ESI = EAX
                End If
            End If

            CrrDist = getDistance(ReadFloat(base_addr + KO_OFF_X), ReadFloat(base_addr + KO_OFF_Y))
            zMobStatu = ReadByte(base_addr + &H2A0)
            zMobMove = ReadLong(base_addr + &H3E4)
            If MobKontrol = "Genel" Then 'Genel Moblar Seçim
                If ReadLong(base_addr + KO_OFF_NT) <= SelNT And getFunctionBaseName(base_addr) <> "Dig Guard" Then
                    If CrrDist < LDist And zMobStatu <> 10 And zMobMove <> 4 Then
                        LID = ReadLong(base_addr + KO_OFF_ID)
                        LBase = (base_addr)
                        LDist = CrrDist
                    End If
                End If
            End If

            If MobKontrol = "List" Then 'Listeye Göre Seçim
                If searchList(getFunctionBaseName(base_addr), frm_2Genel.lst_AttackList) = True Then
                    If CrrDist < LDist And zMobStatu <> 10 And zMobMove <> 4 Then
                        LID = ReadLong(base_addr + KO_OFF_ID)
                        LBase = (base_addr)
                        LDist = CrrDist
                    End If
                End If
            End If

            If MobKontrol = "Karşı" Then 'Karşı Irk seçim
                If ReadLong(base_addr + KO_OFF_NT) <> ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_NT) And ReadLong(base_addr + KO_OFF_ID) <= 9999 Then
                    If CrrDist < LDist And zMobStatu <> 10 And zMobMove <> 4 Then
                        LID = ReadLong(base_addr + KO_OFF_ID)
                        LBase = (base_addr)
                        LDist = CrrDist
                    End If
                End If
            End If
        End While
        zMobID = LID
        zMobName = getFunctionBaseName(LBase)
        zMobX = ReadFloat(LBase + KO_OFF_X)
        zMobY = ReadFloat(LBase + KO_OFF_Y)
        zMobZ = ReadFloat(LBase + KO_OFF_Z)
        zMobDistance = LDist
        zMobBase = LBase
    End Sub
    Public Sub getAreaPeople(Optional ByRef Listem As ListBox = Nothing, Optional ByVal MobTips As String = "Mob", Optional ByVal PMBool As Boolean = True, Optional ByVal PMText As String = "", Optional ByVal PMLevel As Integer = 0, Optional ByVal PMJob As String = "", Optional ByVal clrList As Boolean = False)
        Dim EBP As Long, ESI As Long, EAX As Long, FEnd As Long
        Dim base_addr As Long, Exit_while As Long, MobLong As Long
        Dim plClassNumber As Integer, plClass As String, plLevel As Integer, plID As String, plName As String
        If (clrList) Then Listem.Items.Clear()
        If MobTips = "Mob" Then
            MobLong = &H34
        Else
            MobLong = &H40
        End If
        EBP = ReadLong(ReadLong(KO_FLDB) + MobLong)
        FEnd = ReadLong(ReadLong(EBP + 4) + 4)
        ESI = ReadLong(EBP)
        Exit_while = GetTickCount()
        While ESI <> EBP
            base_addr = ReadLong(ESI + &H10)
            If base_addr = 0 Then Return : Exit Sub
            EAX = ReadLong(ESI + 8)
            If Exit_while + 20 < GetTickCount() Then Return : Exit Sub
            If ReadLong(ESI + 8) <> FEnd Then
                While ReadLong(EAX) <> FEnd
                    EAX = ReadLong(EAX)
                    If Exit_while + 20 < GetTickCount() Then Return : Exit Sub
                End While
                ESI = EAX
            Else
                EAX = ReadLong(ESI + 4)
                While ESI = ReadLong(EAX + 8)
                    ESI = EAX
                    EAX = ReadLong(EAX + 4)
                    If Exit_while + 20 < GetTickCount() Then Return : Exit Sub
                End While
                If ReadLong(ESI + 8) <> EAX Then
                    ESI = EAX
                End If
            End If
            plClassNumber = ReadLong(base_addr + KO_OFF_CLASS)
            plLevel = ReadLong(base_addr + KO_OFF_LEVEL)
            plID = ADWORD(ReadLong(base_addr + KO_OFF_ID), 4)
            plName = getFunctionBaseName(base_addr)
            Select Case plClassNumber
                Case 101, 105, 106, 205, 206
                    plClass = "Warrior"
                Case 102, 107, 108, 207, 208
                    plClass = "Rogue"
                Case 103, 109, 110, 209, 210
                    plClass = "Mage"
                Case 104, 111, 112, 211, 212
                    plClass = "Priest"
                Case Else
                    plClass = "Bilinmiyor"
            End Select
            If MobTips = "PM" Then
                If PMBool = True And plLevel >= PMLevel And PMJob = plClass Then
                    pmUser(plName, PMText)
                Else
                    pmUser(plName, PMText)
                End If
            End If
            If MobTips = "Karşı" Then
                If ReadLong(base_addr + KO_OFF_NT) <> ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_NT) Then
                    If ReadLong(base_addr + KO_OFF_ID) <= 9999 Then
                        If searchList(plName, Listem) = False And plName.Length >= 3 Then
                            Listem.Items.Add(plName)
                        End If
                    End If
                End If
            End If
            If MobTips = "Player" Then
                If searchList((plName), Listem) = False And plName.Length >= 3 Then
                    Listem.Items.Add(plName)
                End If
            End If
            If MobTips = "Mob" Then
                If searchList((plName), Listem) = False And ReadLong(base_addr + KO_OFF_NT) <= 0 And plName <> "Dig Guard" And plName.Length >= 3 Then
                    Listem.Items.Add(plName)
                End If
            End If
        End While
    End Sub
    Public Sub rainbowArchery(ByVal mobStatu As Integer, Optional ByVal mobControl As String = "Mob")
        Dim EBP As Long, ESI As Long, EAX As Long, FEnd As Long, SelNT As Long
        Dim base_addr As Long, Exit_while As Long, MobLong As Long, CrrDist As Long
        If mobControl = "Karşı" Then
            MobLong = &H40
        Else
            MobLong = &H34
        End If
        If mobStatu = 1 Then
            SelNT = 3
        Else
            SelNT = 0
        End If
        MobCount = -1
        For mb As Integer = 0 To 50
            Mob(mb) = 0
            MobDistance(mb) = 0
            MobCor(mb) = ""
        Next
        If getCharClassName() <> "Rogue" Then Exit Sub
        EBP = ReadLong(ReadLong(KO_FLDB) + &H34)
        FEnd = ReadLong(ReadLong(EBP + 4) + 4)
        ESI = ReadLong(EBP)
        Exit_while = GetTickCount()
        While ESI <> EBP
            base_addr = ReadLong(ESI + &H10)
            If base_addr = 0 Then Return : Exit Sub
            EAX = ReadLong(ESI + 8)
            If Exit_while + 20 < GetTickCount() Then Return : Exit While
            If ReadLong(ESI + 8) <> FEnd Then
                While ReadLong(EAX) <> FEnd
                    EAX = ReadLong(EAX)
                    If Exit_while + 20 < GetTickCount() Then Return : Exit While
                End While
                ESI = EAX
            Else
                EAX = ReadLong(ESI + 4)
                While ESI = ReadLong(EAX + 8)
                    ESI = EAX
                    EAX = ReadLong(EAX + 4)
                    If Exit_while + 20 < GetTickCount() Then Return : Exit While
                End While
                If ReadLong(ESI + 8) <> EAX Then
                    ESI = EAX
                End If
            End If
            CrrDist = getDistance(ReadFloat(base_addr + KO_OFF_X), ReadFloat(base_addr + KO_OFF_Y))
            zMobStatu = ReadByte(base_addr + &H2A0)
            zMobMove = ReadLong(base_addr + &H3E4)
            If mobControl = "Mob" Then 'Genel Moblar Seçim
                If ReadLong(base_addr + KO_OFF_NT) <= SelNT And getFunctionBaseName(base_addr) <> "Dig Guard" Then
                    If zMobStatu <> 10 And zMobMove <> 4 Then
                        If CrrDist < 54 Then
                            MobCount = MobCount + 1
                            Mob(MobCount) = ReadLong(base_addr + KO_OFF_ID)
                            MobDistance(MobCount) = CrrDist
                            MobCor(MobCount) = ADWORD((ReadFloat(base_addr + KO_OFF_X)), 4) & ADWORD((ReadFloat(base_addr + KO_OFF_Z)), 4) & ADWORD((ReadFloat(base_addr + KO_OFF_Y)), 4)
                        End If
                    End If
                End If
            End If

            If mobControl = "List" Then 'Listeye Göre Seçim
                If searchList(getFunctionBaseName(base_addr), frm_2Genel.lst_AttackList) = True Then
                    If zMobStatu <> 10 And zMobMove <> 4 Then
                        If CrrDist < 54 Then
                            MobCount = MobCount + 1
                            Mob(MobCount) = ReadLong(base_addr + KO_OFF_ID)
                            MobDistance(MobCount) = CrrDist
                            MobCor(MobCount) = ADWORD((ReadFloat(base_addr + KO_OFF_X)), 4) & ADWORD((ReadFloat(base_addr + KO_OFF_Z)), 4) & ADWORD((ReadFloat(base_addr + KO_OFF_Y)), 4)
                        End If
                    End If
                End If
            End If

            If mobControl = "Karşı" Then 'Karşı Irk seçim
                If ReadLong(base_addr + KO_OFF_NT) <> ReadLong(ReadLong(KO_PTR_CHR) + KO_OFF_NT) And ReadLong(base_addr + KO_OFF_ID) <= 9999 Then
                    If zMobStatu <> 10 And zMobMove <> 4 Then
                        If CrrDist < 54 Then
                            MobCount = MobCount + 1
                            Mob(MobCount) = ReadLong(base_addr + KO_OFF_ID)
                            MobDistance(MobCount) = CrrDist
                            MobCor(MobCount) = ADWORD((ReadFloat(base_addr + KO_OFF_X)), 4) & ADWORD((ReadFloat(base_addr + KO_OFF_Z)), 4) & ADWORD((ReadFloat(base_addr + KO_OFF_Y)), 4)
                        End If
                    End If
                End If
            End If
        End While
        If MobCount <= 1 Then Exit Sub
        For i As Integer = 0 To MobCount - 1
            For j As Integer = i To MobCount
                If MobDistance(i) > MobDistance(j) Then
                    DisSwap = MobDistance(j)
                    MobSwap = Mob(j)
                    MobCoSwap = MobCor(i)

                    MobDistance(j) = MobDistance(i)
                    Mob(j) = Mob(i)
                    MobCor(j) = MobCor(i)

                    MobDistance(i) = DisSwap
                    Mob(i) = MobSwap
                    MobCor(i) = MobCoSwap
                End If
            Next
        Next
    End Sub
    Public Sub rainOne(ByVal mobId As String)
        If IlkArcheryRogue > 0 Then
            useAttackArchery(mobId, frm_2Genel.lstv_AttackRog.Items().Item(IlkArcheryRogue).SubItems(1).Text)
        End If
    End Sub
    Public Sub OkYagmuru(ByVal rainRow As Integer)
        IlkArcheryRogue = firstRogueSkill()
        If MobCount >= 0 Then
            rainOne(Mob(0))
        End If
        If rainRow = 0 Then '5 Mob

            If MobCount >= 5 Then
                rainFive(Mob(0), Mob(1), Mob(2), Mob(3), Mob(4), MobCor(0), MobCor(1), MobCor(2), MobCor(3), MobCor(4))
            End If
            If MobCount >= 3 Then
                rainThree(Mob(0), Mob(1), Mob(2), MobCor(0), MobCor(1), MobCor(2))
            End If

        Else '9 Mob
            If MobCount >= 8 Then
                rainFive(Mob(4), Mob(5), Mob(6), Mob(7), Mob(8), MobCor(4), MobCor(5), MobCor(6), MobCor(7), MobCor(8))
            End If
            If MobCount >= 3 Then
                rainThree(Mob(1), Mob(2), Mob(3), MobCor(1), MobCor(2), MobCor(3))
            End If
        End If

    End Sub
    Public Sub rainThree(ByVal ID1 As Integer, ByVal ID2 As Integer, ByVal ID3 As Integer, ByVal IDCor1 As String, ByVal IDCor2 As String, ByVal IDCor3 As String)
        If getCharSkillPoint(1) >= 15 And getCharMP() >= 40 Then
            SendPack("3101" & ADWORD((getCharClass() & "515"), 8) & getCharID() & ADWORD(ID1, 4) & "00000000000000000000000000000D00")
            SendPack("3102" & ADWORD((getCharClass() & "515"), 8) & getCharID() & ADWORD(ID1, 4) & "000000000000010000000000")
            SendPack("3103" & ADWORD((getCharClass() & "515"), 8) & getCharID() & ADWORD(ID1, 4) & "00000000000001000000000000000000")
            SendPack("3104" & ADWORD((getCharClass() & "515"), 8) & getCharID() & ADWORD(ID1, 4) & IDCor1 & "9BFF0100000000000000")
            SendPack("3103" & ADWORD((getCharClass() & "515"), 8) & getCharID() & ADWORD(ID2, 4) & "00000000000002000000000000000000")
            SendPack("3104" & ADWORD((getCharClass() & "515"), 8) & getCharID() & ADWORD(ID2, 4) & IDCor2 & "9BFF0200000000000000")
            SendPack("3103" & ADWORD((getCharClass() & "515"), 8) & getCharID() & ADWORD(ID3, 4) & "00000000000003000000000000000000")
            SendPack("3104" & ADWORD((getCharClass() & "515"), 8) & getCharID() & ADWORD(ID3, 4) & IDCor3 & "9BFF0300000000000000")
        End If
    End Sub
    Public Sub rainFive(ByVal ID1 As Integer, ByVal ID2 As Integer, ByVal ID3 As Integer, ByVal ID4 As Integer, ByVal ID5 As Integer, ByVal IDCor1 As String, ByVal IDCor2 As String, ByVal IDCor3 As String, ByVal IDCor4 As String, ByVal IDCor5 As String)
        If getCharSkillPoint(1) >= 55 And getCharMP() >= 150 Then
            SendPack("3101" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID1, 4) & "00000000000000000000000000000F00")
            SendPack("3102" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID1, 4) & "000000000000010000000000")
            SendPack("3103" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID1, 4) & "00000000000001000000000000000000")
            SendPack("3104" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID1, 4) & IDCor1 & "9BFF0100000000000000")
            SendPack("3103" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID2, 4) & "00000000000002000000000000000000")
            SendPack("3104" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID2, 4) & IDCor2 & "9BFF0200000000000000")
            SendPack("3103" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID3, 4) & "00000000000003000000000000000000")
            SendPack("3104" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID3, 4) & IDCor3 & "9BFF0300000000000000")
            SendPack("3103" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID4, 4) & "00000000000004000000000000000000")
            SendPack("3104" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID4, 4) & IDCor4 & "9BFF0400000000000000")
            SendPack("3103" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID5, 4) & "00000000000005000000000000000000")
            SendPack("3104" & ADWORD((getCharClass() & "555"), 8) & getCharID() & ADWORD(ID5, 4) & IDCor5 & "9BFF0500000000000000")
        End If
    End Sub
    Public Function firstRogueSkill() As Integer
        Dim resul As Integer = 0
        For i As Integer = 1 To 23
            If frm_2Genel.lstv_AttackRog.Items().Item(i).Checked = True Then
                resul = i
                Exit For
            End If
        Next
        Return resul
    End Function
    Public Function findIDtoNPC(ByVal FındName As String) As String
        Dim EBP As Long, ESI As Long, EAX As Long, FEnd As Long
        Dim base_addr As Long, Exit_while As Long, CrrDist As Integer
        EBP = ReadLong(ReadLong(KO_FLDB) + &H34)
        FEnd = ReadLong(ReadLong(EBP + 4) + 4)
        ESI = ReadLong(EBP)
        Exit_while = GetTickCount()
        findIDtoNPC = "NuLL"
        While ESI <> EBP
            base_addr = ReadLong(ESI + &H10)
            EAX = ReadLong(ESI + 8)
            If Exit_while + 20 < GetTickCount() Then Exit Function
            If ReadLong(ESI + 8) <> FEnd Then
                While ReadLong(EAX) <> FEnd
                    EAX = ReadLong(EAX)
                    If Exit_while + 20 < GetTickCount() Then Exit Function
                End While
                ESI = EAX
            Else
                EAX = ReadLong(ESI + 4)
                While ESI = ReadLong(EAX + 8)
                    ESI = EAX
                    EAX = ReadLong(EAX + 4)
                    If Exit_while + 20 < GetTickCount() Then Exit Function
                End While
                If ReadLong(ESI + 8) <> EAX Then
                    ESI = EAX
                End If
            End If
            CrrDist = getDistance(ReadFloat(base_addr + KO_OFF_X), ReadFloat(base_addr + KO_OFF_Y))
            If CrrDist < 50 And searchText(FındName, getFunctionBaseName(base_addr)) = True Then
                Return ADWORD((ReadLong(base_addr + KO_OFF_ID)), 4)
            End If
        End While
    End Function
    Public Function GetLootBase(ByVal BaseMobID As Integer) As Integer
        Dim EBP As Long, ESI As Long, EAX As Long, FEnd As Long
        Dim base_addr As Long, MobLong As Long, Exit_while As Long, result As Integer
        result = 0
        If BaseMobID < 10000 Then
            MobLong = &H40
        Else
            MobLong = &H34
        End If
        EBP = ReadLong(ReadLong(KO_FLDB) + MobLong)
        FEnd = ReadLong(ReadLong(EBP + 4) + 4)
        ESI = ReadLong(EBP)
        Exit_while = GetTickCount()
        While ESI <> EBP
            base_addr = ReadLong(ESI + &H10)
            EAX = ReadLong(ESI + 8)
            If Exit_while + 20 < GetTickCount() Then Exit While
            If ReadLong(ESI + 8) <> FEnd Then
                While ReadLong(EAX) <> FEnd
                    EAX = ReadLong(EAX)
                    If Exit_while + 20 < GetTickCount() Then Exit While
                End While
                ESI = EAX
            Else
                EAX = ReadLong(ESI + 4)
                While ESI = ReadLong(EAX + 8)
                    ESI = EAX
                    EAX = ReadLong(EAX + 4)
                    If Exit_while + 20 < GetTickCount() Then Exit While
                End While
                If ReadLong(ESI + 8) <> EAX Then
                    ESI = EAX
                End If
            End If
            If CInt(ReadLong(base_addr + KO_OFF_ID)) = CInt(BaseMobID) Then
                result = CInt(base_addr)
            End If
        End While
        Return result
    End Function
#End Region
#Region "######## OTO POİNTER"
    Public Function SearchHexArray(ByVal HexArray As String, ByVal StartOffset As Long, ByVal Length As Long) As Long
        Dim CharString As String
        Dim keyword As [Byte]()
        Dim tmpSearchHeap(&H1000) As Byte, i As Long, j As Long, k As Long
        keyword = UnicodeEncoding.ASCII.GetBytes(HexArray)
        CharString = 0
        For k = StartOffset To StartOffset + Length Step &H1000
            Return ReadProcessMemory(Ko_Handle, k, tmpSearchHeap(0), &H1000, 0&)
            For i = 0 To &HFFF
                If tmpSearchHeap(i) = keyword(1) Then
                    For j = 1 To UBound(keyword)
                        CharString = Mid(HexArray, (j * 2) - 1, 2)
                        If CharString = "XX" Then GoTo nocheck
                        If CharString = "MM" Then GoTo nocheck
                        If tmpSearchHeap(i + j - 1) <> keyword(j) Then GoTo fail
nocheck:
                    Next
                    SearchHexArray = k + i
                    If InStr(1, HexArray, "MMMMMMMM") > 0 Then
                        SearchHexArray = SearchHexArray + ((InStr(1, HexArray, "MMMMMMMM") - 1) / 2)
                    End If
                    GoTo Fin
fail:
                End If
            Next
        Next
        Return 0
Fin:
    End Function
#End Region
#Region "######## K2 BOT ########"
    Public Sub AttackMelee(ByVal skillCode As Integer)
        Dim SkillID As String
        SkillID = ADWORD((getCharClass() & skillCode), 8)
        SendPack("3103" & SkillID & getCharID() & ADWORD((getMobID()), 4) & "01000100000000000000000000000000")
    End Sub
    Public Sub AttackArea(ByVal skillCode As Integer)
        Dim SkillID As String
        SkillID = ADWORD((getCharClass() & skillCode), 8)
        SendPack("3103" & SkillID & getCharID() & "FFFF" & "00000000000000000000000000000000")
    End Sub
    Public Sub UseSkillMe(ByVal skillCode As Integer)
        SendPack("3103" + ADWORD(skillCode, 8) + getCharID() + getCharID() + "000000000000000000000000")
    End Sub
    Public Sub UseSkillMeArea(ByVal skillCode As Integer)
        SendPack("3103" + ADWORD(skillCode, 8) + getCharID() + "FFFF" + "00000000000000000000000000000000")
    End Sub
    Public Sub DeleteSkillMe(ByVal skillCode As Integer)
        SendPack("3106" + ADWORD(skillCode, 8) + getCharID() + getCharID() + "000000000000000000000000")
    End Sub
    Public Sub ClientRelog()
        SendPack("9F01")
        Sleep(1000)
        SendPack("0C01")
    End Sub
    Public Sub refreshMethod(ByRef cmbMethod As ComboBox, Optional ByVal goSlot As Boolean = False)
        cmbMethod.Items.Clear()
        cmbMethod.Items.Add("G - Town")
        Select Case getCharZone()
            Case "Moradon"
                cmbMethod.Items.Add("G - Folk")
                cmbMethod.Items.Add("G - Tale")
            Case "Lüferson"
                cmbMethod.Items.Add("G - Belluga")
                cmbMethod.Items.Add("G - Kalluga")
                cmbMethod.Items.Add("G - Roan")
                cmbMethod.Items.Add("G - Eslant")
                cmbMethod.Items.Add("G - Eslant -> Linart")
                If (goSlot) Then
                    cmbMethod.Items.Add("R - Belluga")
                    cmbMethod.Items.Add("R - Roan")
                    cmbMethod.Items.Add("R - Linart")
                End If
            Case "El Morad"
                cmbMethod.Items.Add("G - Asga")
                cmbMethod.Items.Add("G - Kalluga")
                cmbMethod.Items.Add("G - Doda")
                cmbMethod.Items.Add("G - Eslant")
                cmbMethod.Items.Add("G - Eslant -> Laiba")
        End Select
        cmbMethod.Items.Add("L - Pet Çıkart")
        If (goSlot = False) Then
            cmbMethod.Items.Add("S - Sundries Tedarik")
            cmbMethod.Items.Add("I - Item Sat")
            cmbMethod.Items.Add("P - Potion Tedarik")
            cmbMethod.Items.Add("D - DC Sundries Tedarik")
            cmbMethod.Items.Add("O - Repair OK")
        Else
            cmbMethod.Items.Add("O - Slot OK")
        End If
        cmbMethod.SelectedIndex = 0
    End Sub
    Public Sub addRprMethod(ByVal cmbMethod As ComboBox, ByRef lstRprCoor As ListBox)
        If (getCharIrk() = "Karus") Then
            Select Case cmbMethod.Text
                Case "G - Folk"
                    lstRprCoor.Items.Add("800 - 526")
                Case "G - Tale"
                    lstRprCoor.Items.Add("800 - 526")
                Case "G - Belluga"
                    lstRprCoor.Items.Add("446 - 1619")
                Case "G - Kalluga"
                    lstRprCoor.Items.Add("446 - 1619")
                Case "G - Roan"
                    lstRprCoor.Items.Add("446 - 1619")
                Case "G - Eslant"
                    lstRprCoor.Items.Add("446 - 1619")
                Case "G - Eslant -> Linart"
                    lstRprCoor.Items.Add("1364 - 1850")
            End Select
        Else
            Select Case cmbMethod.Text
                Case "G - Folk"
                    lstRprCoor.Items.Add("834 - 526")
                Case "G - Tale"
                    lstRprCoor.Items.Add("834 - 526")
                Case "G - Asga"
                    lstRprCoor.Items.Add("1590 - 416")
                Case "G - Kalluga"
                    lstRprCoor.Items.Add("1590 - 416")
                Case "G - Doda"
                    lstRprCoor.Items.Add("1590 - 416")
                Case "G - Eslant"
                    lstRprCoor.Items.Add("1590 - 416")
                Case "G - Eslant -> Laiba"
                    lstRprCoor.Items.Add("673 - 1850")
            End Select
        End If
        Select Case cmbMethod.Text.Substring(0, 1)
            Case "S", "P", "O", "I", "R", "L", "D"
                lstRprCoor.Items.Add(getCharX() & " - " & getCharY())
        End Select
        lstRprCoor.Items.Add(cmbMethod.Text)
    End Sub
    Public Sub useGate(ByVal GateTP As Integer)
        If (useGateFunc = True) Then Exit Sub
        Sleep(2000)
        If (getCharIrk() = "Karus") Then
            Select Case GateTP
                Case 0
                    SendPack("4BAE0F3F08")
                Case 1
                    SendPack("4BAE0F4008")
                Case 2
                    SendPack("4BA60F7000") 'Town > Belluge
                Case 3
                    SendPack("4BA60F7100") 'Town > Kalluga
                Case 4
                    SendPack("4BA60F7200") 'Town > Roan
                Case 5
                    SendPack("4BA60F7300") 'Town > Eslant
                Case 6
                    SendPack("4BA20F9800") 'Eslant > Linart
            End Select
        Else
            Select Case GateTP
                Case 0
                    SendPack("4BAD0F4908")
                Case 1
                    SendPack("4BAD0F4A08")
                Case 2
                    SendPack("4BA30FD400") 'Town > Asga
                Case 3
                    SendPack("4BA30FD600") 'Town > Kalluga
                Case 4
                    SendPack("4BA30FD500") 'Town > Doda
                Case 5
                    SendPack("4BA30FD700") 'Town > Eslant
                Case 6
                    SendPack("4BA10FFC00") 'Eslant > Laiba
            End Select
        End If
        useGateFunc = True
    End Sub
    Public Sub useRespawn(ByVal RespawnRow As Integer)
        If (useRespawnFunc = True) Then Exit Sub
        If (getCharIrk() = "Karus") Then
            Select Case RespawnRow
                Case 0
                    SendPack("3366000000") ' Belluga
                Case 1
                    SendPack("3368000000") 'Roan
                Case 2
                    SendPack("3367000000") 'Linart
            End Select
        Else
            Select Case RespawnRow
                Case 0
                    SendPack("4BAD0F4908")
                Case 1
                    SendPack("4BAD0F4A08")
                Case 2
                    SendPack("4BA30FD400")
            End Select
        End If
        useRespawnFunc = True
    End Sub
    Public Sub setRprMethod(ByVal txtMethod As String, ByRef lstCoor As ListBox)
        Select Case txtMethod
            Case "G - Town"
                useTown()
                If (CharIsTown() = True) Then lstCoor.SelectedIndex += 1
            Case "G - Folk"
                useGate(0)
                If (getDistance(411, 538) < 20) Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If

            Case "G - Tale"
                useGate(1)
                If (getDistance(85, 909) < 20) Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
            Case "G - Belluga"
                useGate(2)
                If (getDistance(386, 692) < 20) And (getCharIrk() = "Karus") Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
            Case "G - Kalluga"
                useGate(3)
                If (getDistance(921, 241) < 20) And (getCharIrk() = "Karus") Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
                If (getDistance(1133, 182) < 20) And (getCharIrk() = "Human") Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
            Case "G - Roan"
                useGate(4)
                If (getDistance(996, 920) < 20) And (getCharIrk() = "Karus") Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
            Case "G - Eslant"
                useGate(5)
                If (getDistance(1375, 1850) < 20) And (getCharIrk() = "Karus") Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
                If (getDistance(677, 202) < 20) And (getCharIrk() = "Human") Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
            Case "G - Eslant -> Linart"
                useGate(6)
                If (getDistance(1725, 880) < 20) And (getCharIrk() = "Karus") Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
            Case "G - Asga"
                useGate(2)
                If (getDistance(1653, 134) < 20) And (getCharIrk() = "Human") Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
            Case "G - Doda"
                useGate(4)
                If (getDistance(1055, 114) < 20) And (getCharIrk() = "Human") Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
            Case "G - Eslant -> Laiba"
                useGate(6)
                If (getDistance(317, 1251) < 20) And (getCharIrk() = "Human") Then
                    useGateFunc = False
                    lstCoor.SelectedIndex += 1
                End If
            Case "R - Belluga"
                useRespawn(0)
                useRespawnFunc = True
                lstCoor.SelectedIndex += 1
            Case "R - Roan"
                useRespawn(1)
                useRespawnFunc = True
                lstCoor.SelectedIndex += 1
            Case "R - Linart"
                useRespawn(2)
                useRespawnFunc = True
                lstCoor.SelectedIndex += 1
        End Select
    End Sub
    Public Sub useAutoPotionHP()
        If getItemSlotInv("389010000") > 0 And getLastHP() >= 45 Then SendPotion("1A7A07", 0) : Exit Sub
        If getItemSlotInv("389011000") > 0 And getLastHP() >= 90 Then SendPotion("1B7A07", 1) : Exit Sub
        If getItemSlotInv("900770000") > 0 And getLastHP() >= 90 Then SendPotion("E28107", 2) : Exit Sub
        If getItemSlotInv("389012000") > 0 And getLastHP() >= 180 Then SendPotion("1C7A07", 3) : Exit Sub
        If getItemSlotInv("900780000") > 0 And getLastHP() >= 180 Then SendPotion("E38107", 4) : Exit Sub
        If getItemSlotInv("389013000") > 0 And getLastHP() >= 360 Then SendPotion("1D7A07", 5) : Exit Sub
        If getItemSlotInv("900790000") > 0 And getLastHP() >= 360 Then SendPotion("E48107", 6) : Exit Sub
        If getItemSlotInv("389014000") > 0 And getLastHP() >= 720 Then SendPotion("1E7A07", 7) : Exit Sub
        If getItemSlotInv("399284000") > 0 And getLastHP() >= 720 Then SendPotion("5A7A07", 8) : Exit Sub
        If getItemSlotInv("900817000") > 0 And getLastHP() >= 720 Then SendPotion("F98107", 8) : Exit Sub
        If getItemSlotInv("389390000") > 0 And getLastHP() >= 720 Then SendPotion("B1A107", 8) : Exit Sub
        If getItemSlotInv("389310000") > 0 And getLastHP() >= 720 Then SendPotion("ABA107", 8) : Exit Sub
        If getItemSlotInv("810117000") > 0 And getLastHP() >= 720 Then SendPotion("5C7A07", 8) : Exit Sub
        If getItemSlotInv("389070000") > 0 And getLastHP() >= 1080 Then SendPotion("577A07", 8) : Exit Sub
        If getItemSlotInv("810189000") > 0 And getLastHP() >= 1080 Then SendPotion("577A07", 8) : Exit Sub
    End Sub
    Public Sub useAutoPotionMP()
        If getItemSlotInv("389016000") > 0 And getLastMP() >= 90 Then SendPotion("207A07", 9) : Exit Sub
        If getItemSlotInv("389017000") > 0 And getLastMP() >= 180 Then SendPotion("217A07", 10) : Exit Sub
        If getItemSlotInv("900800000") > 0 And getLastMP() >= 180 Then SendPotion("E58107", 11) : Exit Sub
        If getItemSlotInv("389018000") > 0 And getLastMP() >= 480 Then SendPotion("227A07", 12) : Exit Sub
        If getItemSlotInv("900810000") > 0 And getLastMP() >= 480 Then SendPotion("E68107", 13) : Exit Sub
        If getItemSlotInv("389019000") > 0 And getLastMP() >= 960 Then SendPotion("237A07", 14) : Exit Sub
        If getItemSlotInv("900820000") > 0 And getLastMP() >= 960 Then SendPotion("E78107", 15) : Exit Sub
        If getItemSlotInv("389020000") > 0 And getLastMP() >= 1920 Then SendPotion("247A07", 16) : Exit Sub
        If getItemSlotInv("399285000") > 0 And getLastMP() >= 1920 Then SendPotion("9C7A07", 17) : Exit Sub
        If getItemSlotInv("900818000") > 0 And getLastMP() >= 1920 Then SendPotion("FA8107", 17) : Exit Sub
        If getItemSlotInv("389400000") > 0 And getLastMP() >= 1920 Then SendPotion("B2A107", 17) : Exit Sub
        If getItemSlotInv("389340000") > 0 And getLastMP() >= 1920 Then SendPotion("AEA107", 17) : Exit Sub
        If getItemSlotInv("810118000") > 0 And getLastMP() >= 1920 Then SendPotion("647A07", 17) : Exit Sub
        If getItemSlotInv("389130000") > 0 And getLastMP() >= 2880 Then SendPotion("587A07", 17) : Exit Sub
        If getItemSlotInv("810192000") > 0 And getLastMP() >= 2880 Then SendPotion("587A07", 17) : Exit Sub

    End Sub
    Public Sub useHpPotionRow(ByVal hpRow As Integer)
        If getCharHP() <= ((getCharMaxHP() * hpRow) / 100) Then
            If getItemSlotInv("389010000") > 0 Then SendPotion("1A7A07", 0) : Exit Sub
            If getItemSlotInv("389011000") > 0 Then SendPotion("1B7A07", 1) : Exit Sub
            If getItemSlotInv("900770000") > 0 Then SendPotion("E28107", 2) : Exit Sub
            If getItemSlotInv("389012000") > 0 Then SendPotion("1C7A07", 3) : Exit Sub
            If getItemSlotInv("900780000") > 0 Then SendPotion("E38107", 4) : Exit Sub
            If getItemSlotInv("389013000") > 0 Then SendPotion("1D7A07", 5) : Exit Sub
            If getItemSlotInv("900790000") > 0 Then SendPotion("E48107", 6) : Exit Sub
            If getItemSlotInv("389014000") > 0 Then SendPotion("1E7A07", 7) : Exit Sub
            If getItemSlotInv("399284000") > 0 Then SendPotion("5A7A07", 8) : Exit Sub
            If getItemSlotInv("900817000") > 0 Then SendPotion("F98107", 8) : Exit Sub
            If getItemSlotInv("389390000") > 0 Then SendPotion("B1A107", 8) : Exit Sub
            If getItemSlotInv("389310000") > 0 Then SendPotion("ABA107", 8) : Exit Sub
            If getItemSlotInv("810117000") > 0 Then SendPotion("5C7A07", 8) : Exit Sub
            If getItemSlotInv("389070000") > 0 Then SendPotion("577A07", 8) : Exit Sub
            If getItemSlotInv("810189000") > 0 Then SendPotion("577A07", 8) : Exit Sub
        End If
    End Sub
    Public Sub useMpPotionRow(ByVal mpRow As Integer)
        If getCharMP() <= ((getCharMaxMP() * mpRow) / 100) Then
            If getItemSlotInv("389016000") > 0 Then SendPotion("207A07", 9) : Exit Sub
            If getItemSlotInv("389017000") > 0 Then SendPotion("217A07", 10) : Exit Sub
            If getItemSlotInv("900800000") > 0 Then SendPotion("E58107", 11) : Exit Sub
            If getItemSlotInv("389018000") > 0 Then SendPotion("227A07", 12) : Exit Sub
            If getItemSlotInv("900810000") > 0 Then SendPotion("E68107", 13) : Exit Sub
            If getItemSlotInv("389019000") > 0 Then SendPotion("237A07", 14) : Exit Sub
            If getItemSlotInv("900820000") > 0 Then SendPotion("E78107", 15) : Exit Sub
            If getItemSlotInv("389020000") > 0 Then SendPotion("247A07", 16) : Exit Sub
            If getItemSlotInv("399285000") > 0 Then SendPotion("9C7A07", 17) : Exit Sub
            If getItemSlotInv("900818000") > 0 Then SendPotion("FA8107", 17) : Exit Sub
            If getItemSlotInv("389400000") > 0 Then SendPotion("B2A107", 17) : Exit Sub
            If getItemSlotInv("389340000") > 0 Then SendPotion("AEA107", 17) : Exit Sub
            If getItemSlotInv("810118000") > 0 Then SendPotion("647A07", 17) : Exit Sub
            If getItemSlotInv("389130000") > 0 Then SendPotion("587A07", 17) : Exit Sub
            If getItemSlotInv("810192000") > 0 Then SendPotion("587A07", 17) : Exit Sub

        End If
    End Sub
    Public Sub useMinor(ByVal TargetID As String, ByVal minorRow As Integer)
        If getCharClassName() = "Rogue" And getCharSkillPoint(3) >= 5 Then
            If (minorRow >= 99) Then
                If Val(getCharMaxHP) - Val(getCharHP) > 60 Then
                    SendPSec("3103" + ADWORD((getCharClass() & "705"), 8) + getCharID() + TargetID + "00000000000000000000000000000000", 10, 0)
                End If
            Else
                If getCharHP() <= ((getCharMaxHP() * minorRow) / 100) Then
                    SendPSec("3103" + ADWORD((getCharClass() & "705"), 8) + getCharID() + TargetID + "00000000000000000000000000000000", 10, 0)
                End If
            End If
        End If
    End Sub
    Public Sub useTS(ByVal Sira As Integer)
        Dim ClassID As Long
        If getItemSlotInv("379091000") > 0 And checkTS("472") = False Then
            Select Case Sira
                Case 0 : ClassID = "472310"
                Case 1 : ClassID = "472020"
                Case 2 : ClassID = "472040"
                Case 3 : ClassID = "472050"
                Case 4 : ClassID = "472070"
                Case 5 : ClassID = "472080"
                Case 6 : ClassID = "472090"
                Case 7 : ClassID = "472130"
                Case 8 : ClassID = "472132"
                Case 9 : ClassID = "472150"
                Case 10 : ClassID = "472202"
                Case 11 : ClassID = "472250"
                Case 12 : ClassID = "472260"
                Case 13 : ClassID = "472276"
                Case 14 : ClassID = "472280"
            End Select
            SendPSec("3103" + ADWORD((ClassID), 8) & getCharID() & getCharID() & "0000000000000000000000000000", 1000, 1)
        End If
    End Sub
    Public Sub useMeatDump()
        If getItemSlotInv("508216000") > 0 And checkSkillID("141") = False Then
            SendPSec("3103" + ADWORD(("490141"), 8) + getCharID() + getCharID() + "000000000000000000000000", 1000, 2)
        End If
    End Sub
    Public Sub useGate()
        If getCharLevel() >= 10 And getCharClassName() = "Priest" Or getCharClassName() = "Mage" Then
            SendPSec("3101" + ADWORD((getCharClass() & "700"), 8) + getCharID() + getCharID() + "00000000000000000000000000000F00", 10000, 3)
            SendPSec("3103" + ADWORD((getCharClass() & "700"), 8) + getCharID() + getCharID() + "000000000000000000000000", 10000, 4)
        End If
    End Sub
    Public Sub useDropScBug()
        If checkSkillID("095") = False Then
            SendPSec("3103" + ADWORD((500095), 8) + getCharID() + getCharID() + "000000000000000000000000", 1000, 5)
        End If
    End Sub
    Public Sub useCharStat()
        If checkSkillID("059") = False Then
            SendPSec("3103" + ADWORD((492059), 8) + getCharID() + "FFFF" + "00000000000000000000000000000000", 1000, 56)
        End If
    End Sub
    Public Sub useCharFragManer()
        If checkSkillID("063") = False Then
            SendPSec("3103" + ADWORD((492063), 8) + getCharID() + "FFFF" + "00000000000000000000000000000000", 1000, 56)
        End If
    End Sub
    Public Sub useChar100Def()
        If checkSkillID("061") = False Then
            SendPSec("3103" + ADWORD((492061), 8) + getCharID() + "FFFF" + "00000000000000000000000000000000", 1000, 57)
        End If
    End Sub
    Public Sub useChar100Atak()
        If checkSkillID("062") = False And checkSkillID("271") = False Then
            SendPSec("3103" + ADWORD((492062), 8) + getCharID() + "FFFF" + "00000000000000000000000000000000", 1000, 58)
        End If
    End Sub
    Public Sub useHpFulled(ByVal hpRow As Integer)
        If checkSkillID("060") = False And getCharHP() <= ((getCharMaxHP() * hpRow) / 100) Then
            SendPSec("3103" + ADWORD((492060), 8) + getCharID() + "FFFF" + "00000000000000000000000000000000", 2000, 59)
        End If
    End Sub
    Public Sub useDefCake()
        If checkSkillID("343") = False Then
            SendPSec("3103" + ADWORD((500343), 8) + getCharID() + getCharID() + "000000000000000000000000", 1000, 6)
        End If
    End Sub
    Public Sub useImpCake()
        SendPack("3103" + ADWORD((500344), 8) + getCharID() + getCharID() + "000000000000000000000000")
        SendPack("3106" + ADWORD((500344), 8) + getCharID() + getCharID() + "000000000000000000000000")
    End Sub
    Public Sub useAttackBuffBug()
        If checkSkillID("271") = False Then
            SendPSec("3103" + ADWORD((500271), 8) + getCharID() + getCharID() + "000000000000000000000000", 1000, 7)
        End If
    End Sub
    Public Sub useAttackScroll()
        If checkSkillID("023") = False Then
            SendPSec("3103" + ADWORD((492023), 8) + getCharID() + getCharID() + "000000000000000000000000", 1000, 8)
        End If
    End Sub
    Public Sub useWeaponSc()
        If getItemSlotInv("389155000") > 0 And checkSkillID("049") = False Then
            SendPSec("3103" + ADWORD((500049), 8) + getCharID() + getCharID() + "000000000000000000000000", 1000, 9)
        End If
    End Sub
    Public Sub useArmorSc()
        If getItemSlotInv("389156000") > 0 And checkSkillID("050") = False Then
            SendPSec("3103" + ADWORD((500050), 8) + getCharID() + getCharID() + "000000000000000000000000", 1000, 10)
        End If
    End Sub
    Public Sub useRespawn()
        SendPack("290103")
        SendPack("1200")
    End Sub
    Public Sub useRespawnChar()
        If (frm_2Genel.chk_BackSlot.Checked And BacktoSlot = False) Then
            BacktoSlot = True
            RespawnTimer = 0
        ElseIf (frm_2Genel.chk_BackSlot.Checked = False) Then
            SendPack("1200")
        End If
    End Sub
    Public Sub useTown()
        If ((getCharHP() * 100) / getCharMaxHP()) > 50 Then SendPSec("4800", 1000, 11)
        If ((getCharHP() * 100) / getCharMaxHP()) < 50 Then useRespawn()
    End Sub
    Public Sub exchangeAbysGem()
        SendPack("2001" + findIDtoNPC("Moira") + "FFFFFFFF")
        SendPack("640725100000")
        SendPack("55000F31363034375F4D6F6972612E6C7561FF")
    End Sub
    Public Sub useAttackR(ByVal UserID As Long, ByVal skillCode As String)
        SendPSec(skillCode & ADWORD((UserID), 4) & "D100F9FF00", 1000, 12)
    End Sub
    Public Sub useMagicHammer()
        If (getItemDurab(2) < 500 And getItemID(2) > 0) _
            Or (getItemDurab(5) < 500 And getItemID(5) > 0) _
            Or (getItemDurab(7) < 500 And getItemID(7) > 0) _
            Or (getItemDurab(9) < 500 And getItemID(9) > 0) _
            Or (getItemDurab(11) < 500 And getItemID(11) > 0) _
            Or (getItemDurab(13) < 500 And getItemID(13) > 0) _
            Or (getItemDurab(14) < 500 And getItemID(14) > 0) Then
            SendPSec("3103" + ADWORD((490202), 8) + getCharID() + getCharID() + "00000000000000000000000000000000", 10000, 13)
        End If
    End Sub
    Public Sub useBezoar()
        If getItemSlotInv("389034000") > 0 And checkSkillID("034") = False Then
            SendPSec("3103" + ADWORD((490034), 8) + getCharID() + getCharID() + "00000000000000000000000000000000", 1000, 14)
        End If
    End Sub
    Public Sub useSmallCake()
        If getItemSlotInv("389035000") > 0 And checkSkillID("035") = False Then
            SendPSec("3103" + ADWORD((490035), 8) + getCharID() + getCharID() + "00000000000000000000000000000000", 1000, 15)
        End If
    End Sub
#End Region
#Region "######## Chaos Skill Sistemi ########"
    Public Sub ChaosSkillRow()
        For i As Integer = 0 To 9
            ChaosSkill(i) = ChaosSkill(i) - 1
        Next
    End Sub
    Public Sub useSkillChaos()
        If (getCharHP() <= 0) Then Exit Sub
        With frm_2Genel.lstv_AttackChaos.Items
            If .Item(0).Checked Then useAttackR(zMobID, .Item(0).SubItems(1).Text) 'R Atak
            If .Item(1).Checked And getCharHP() < getCharMaxHP() And ChaosSkill(1) <= 0 Then ChaosSkill(1) = 210 : useChaosSkill(490222) 'Hp Potion
            If .Item(2).Checked And getCharMP() >= 10 And checkSkillID("223") = False Then useChaosSkill(490223) 'Sprint
            If .Item(7).Checked And getCharMP() >= 10 And ChaosSkill(7) <= 0 And getMobDistance() < 55 Then ChaosSkill(7) = 6200 : useChaosAttack(zMobID, "227") : Exit Sub 'Stun Lightning
            If .Item(8).Checked And getCharMP() >= 10 And ChaosSkill(8) <= 0 And getMobDistance() < 14 Then ChaosSkill(8) = 6200 : useChaosAttack(zMobID, "226") : Exit Sub 'Killing Blade
            If .Item(4).Checked And getCharMP() >= 10 And ChaosSkill(4) <= 0 And getMobDistance() < 55 Then ChaosSkill(4) = 3000 : useChaosAttack(zMobID, "221") : Exit Sub 'Fire Sword
            If .Item(6).Checked And getCharMP() >= 10 And ChaosSkill(6) <= 0 And getMobDistance() < 55 Then ChaosSkill(6) = 1500 : useChaosAttack(zMobID, "224") : Exit Sub 'Ice Sword
            If .Item(5).Checked And getCharMP() >= 10 And ChaosSkill(5) <= 0 And getMobDistance() < 14 Then ChaosSkill(5) = 1000 : useChaosAttack(zMobID, "220") : Exit Sub 'Ice Counter
            If .Item(3).Checked And getCharMP() >= 10 And ChaosSkill(3) <= 0 And getMobDistance() < 14 Then ChaosSkill(3) = 210 : useChaosAttack(zMobID, "219") : Exit Sub 'Spirit Sword
            If .Item(9).Checked And getCharMP() >= 10 And ChaosSkill(9) <= 0 And getMobDistance() < 14 Then ChaosSkill(9) = 500 : useChaosAttack(zMobID, "228") : Exit Sub 'Poison Knife
        End With
    End Sub
    Public Sub useChaosAttack(ByVal TargetID As Integer, ByVal SkillCode As String)
        Select Case SkillCode
            Case "223"
                SendPack("3103" & ADWORD("490" & SkillCode, 8) & getCharID() & getCharID() & "00000000000000000000000000000000")
            Case "219", "220", "226"
                SendPack("3103" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & "01000100000000000000000000000000")
            Case "221"
                SendPack("3101" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & "00000000000000000000000000000F00")
                SendPack("3102" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & "660001008200000000000000")
                SendPack("3103" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & "00000000000000000000000000000000")
                SendPack("3104" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & getMobSkillCoor() & "9BFF0000000000000000")
            Case "224" 'İce Sword
                SendPack("3101" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & "00000000000000000000000000000500")
                SendPack("3104" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & getMobSkillCoor() & "9CFF0000000000000000")
            Case "227"
                SendPack("3101" & ADWORD("490" & SkillCode, 8) & getCharID() & "FFFF" & getMobSkillCoor() & "00000000000000000F00")
                SendPack("3103" & ADWORD("490" & SkillCode, 8) & getCharID() & "FFFF" & getMobSkillCoor() & "000000000000")
            Case "228"
                SendPack("3101" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & "00000000000000000000000000000500")
                SendPack("3102" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & getMobSkillCoor() & "000000000000")
                SendPack("3103" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & "00000000000000000000000000000000")
                SendPack("3104" & ADWORD("490" & SkillCode, 8) & getCharID() & ADWORD(TargetID, 4) & getMobSkillCoor() & "9BFF0000000000000000")
        End Select
    End Sub

    Public Sub useChaosSkill(ByVal SkillCode As String)
        SendPack("3103" & ADWORD(SkillCode, 8) & getCharID() & getCharID() & "00000000000000000000000000000000")
    End Sub

#End Region
#Region "######## Wariror Skill Sistemi ########"
    Public Sub warriorSkillRow()
        For i As Integer = 0 To 46
            WarSkill(i) = WarSkill(i) - 1
        Next
    End Sub
    Public Sub useSkillWarrior()
        With frm_2Genel.lstv_AttackWar.Items
            If (MobDissAttack > 14) Then
                MobDissAttack = 14
            End If
            If getMobDistance() <= MobDissAttack Then
                'Binding
                If .Item(25).Checked And getCharMP() >= 30 And WarSkill(25) <= 0 And getCharSkillPoint(2) >= 30 Then WarSkill(25) = 800 : useAttackWarrior(zMobID, .Item(25).SubItems(1).Text) : Exit Sub
                'Provoke
                If .Item(26).Checked And getCharMP() >= 60 And WarSkill(26) <= 0 And getCharSkillPoint(2) >= 45 Then WarSkill(26) = 1600 : useAttackWarrior(zMobID, .Item(26).SubItems(1).Text) : Exit Sub
                'R Atak
                If .Item(0).Checked Then useAttackR(zMobID, .Item(0).SubItems(1).Text)
                'Blooding
                If .Item(20).Checked And getCharMP() >= 350 And WarSkill(20) <= 0 And getCharSkillPoint(1) >= 75 Then WarSkill(20) = 2100 : useAttackWarrior(zMobID, .Item(20).SubItems(1).Text) : Exit Sub
                'Leg Cutting
                If .Item(9).Checked And getCharMP() >= 84 And WarSkill(9) <= 0 And getCharSkillPoint(1) >= 20 Then WarSkill(9) = 495 : useAttackWarrior(zMobID, .Item(9).SubItems(1).Text) : Exit Sub
                'Slash
                If .Item(2).Checked And getCharMP() >= 4 And WarSkill(2) <= 0 And getCharSkillPoint(1) >= 0 Then WarSkill(2) = 385 : useAttackWarrior(zMobID, .Item(2).SubItems(1).Text) : Exit Sub
                'Crash
                If .Item(3).Checked And getCharMP() >= 4 And WarSkill(3) <= 0 And getCharSkillPoint(1) >= 0 Then WarSkill(3) = 295 : useAttackWarrior(zMobID, .Item(3).SubItems(1).Text) : Exit Sub
                'Piercing
                If .Item(4).Checked And getCharMP() >= 8 And WarSkill(4) <= 0 And getCharSkillPoint(1) >= 0 Then WarSkill(4) = 295 : useAttackWarrior(zMobID, .Item(4).SubItems(1).Text) : Exit Sub
                'Hash
                If .Item(5).Checked And getCharMP() >= 10 And WarSkill(5) <= 0 And getCharSkillPoint(1) >= 0 Then WarSkill(5) = 295 : useAttackWarrior(zMobID, .Item(5).SubItems(1).Text) : Exit Sub
                'Shear
                If .Item(7).Checked And getCharMP() >= 20 And WarSkill(7) <= 0 And getCharSkillPoint(1) >= 10 Then WarSkill(7) = 295 : useAttackWarrior(zMobID, .Item(7).SubItems(1).Text) : Exit Sub
                'Sever
                If .Item(11).Checked And getCharMP() >= 40 And WarSkill(11) <= 0 And getCharSkillPoint(1) >= 30 Then WarSkill(11) = 295 : useAttackWarrior(zMobID, .Item(11).SubItems(1).Text) : Exit Sub
                'Multiple Shork
                If .Item(13).Checked And getCharMP() >= 60 And WarSkill(13) <= 0 And getCharSkillPoint(1) >= 40 Then WarSkill(13) = 295 : useAttackWarrior(zMobID, .Item(13).SubItems(1).Text) : Exit Sub
                'Mangling
                If .Item(15).Checked And getCharMP() >= 60 And WarSkill(15) <= 0 And getCharSkillPoint(1) >= 50 Then WarSkill(15) = 295 : useAttackWarrior(zMobID, .Item(15).SubItems(1).Text) : Exit Sub
                'Thrust
                If .Item(16).Checked And getCharMP() >= 200 And WarSkill(16) <= 0 And getCharSkillPoint(1) >= 55 Then WarSkill(16) = 295 : useAttackWarrior(zMobID, .Item(16).SubItems(1).Text) : Exit Sub
                '###################### BERSERKER ######################
                'Blade Of Hate - 3
                If .Item(46).Checked And getCharMP() >= 400 And WarSkill(46) <= 0 And getCharSkillPoint(3) >= 60 And getMobDistance() <= 3 Then WarSkill(46) = 1100 : useAttackWarrior(zMobID, .Item(46).SubItems(1).Text) : Exit Sub
                'Blade Of Hate - 2
                If .Item(37).Checked And getCharMP() >= 300 And WarSkill(37) <= 0 And getCharSkillPoint(3) >= 35 And getMobDistance() <= 3 Then WarSkill(37) = 1100 : useAttackWarrior(zMobID, .Item(37).SubItems(1).Text) : Exit Sub
                'Blade Of Hate - 1
                If .Item(32).Checked And getCharMP() >= 100 And WarSkill(32) <= 0 And getCharSkillPoint(3) >= 25 And getMobDistance() <= 3 Then WarSkill(32) = 1100 : useAttackWarrior(zMobID, .Item(32).SubItems(1).Text) : Exit Sub
                'Wind
                If .Item(36).Checked And getCharMP() >= 60 And WarSkill(36) <= 0 And getCharSkillPoint(3) >= 33 Then WarSkill(36) = 500 : useAttackWarrior(zMobID, .Item(36).SubItems(1).Text) : Exit Sub
                'Blaze
                If .Item(40).Checked And getCharMP() >= 90 And WarSkill(40) <= 0 And getCharSkillPoint(3) >= 43 Then WarSkill(40) = 400 : useAttackWarrior(zMobID, .Item(40).SubItems(1).Text) : Exit Sub
                'Hate
                If .Item(42).Checked And getCharMP() >= 90 And WarSkill(42) <= 0 And getCharSkillPoint(3) >= 53 Then WarSkill(42) = 400 : useAttackWarrior(zMobID, .Item(42).SubItems(1).Text) : Exit Sub
                'Wink
                If .Item(27).Checked And getCharMP() >= 45 And WarSkill(27) <= 0 And getCharSkillPoint(3) >= 8 Then WarSkill(27) = 0 : useAttackWarrior(zMobID, .Item(27).SubItems(1).Text) : Exit Sub
                'Pain Killer
                If .Item(29).Checked And getCharMP() >= 30 And WarSkill(29) <= 0 And getCharSkillPoint(3) >= 15 Then WarSkill(29) = 0 : useAttackWarrior(zMobID, .Item(29).SubItems(1).Text) : Exit Sub
                'Pain Killer
                If .Item(30).Checked And getCharMP() >= 90 And WarSkill(30) <= 0 And getCharSkillPoint(3) >= 18 Then WarSkill(30) = 0 : useAttackWarrior(zMobID, .Item(30).SubItems(1).Text) : Exit Sub
                'Shock
                If .Item(33).Checked And getCharMP() >= 135 And WarSkill(33) <= 0 And getCharSkillPoint(3) >= 28 Then WarSkill(33) = 0 : useAttackWarrior(zMobID, .Item(33).SubItems(1).Text) : Exit Sub
                'Rage
                If .Item(38).Checked And getCharMP() >= 180 And WarSkill(38) <= 0 And getCharSkillPoint(3) >= 38 Then WarSkill(38) = 0 : useAttackWarrior(zMobID, .Item(38).SubItems(1).Text) : Exit Sub
                'Killer
                If .Item(44).Checked And getCharMP() >= 300 And WarSkill(44) <= 0 And getCharSkillPoint(3) >= 58 Then WarSkill(44) = 0 : useAttackWarrior(zMobID, .Item(44).SubItems(1).Text) : Exit Sub
                'Echo
                If .Item(45).Checked And getCharMP() >= 375 And WarSkill(45) <= 0 And getCharSkillPoint(3) >= 60 Then WarSkill(45) = 0 : useAttackWarrior(zMobID, .Item(45).SubItems(1).Text) : Exit Sub
                
                'Pierce
                If .Item(8).Checked And getCharMP() >= 60 And WarSkill(8) <= 0 And getCharSkillPoint(1) >= 15 Then WarSkill(8) = 0 : useAttackWarrior(zMobID, .Item(8).SubItems(1).Text) : Exit Sub
                'Hell Blade
                If .Item(21).Checked And getCharMP() >= 400 And WarSkill(21) <= 0 And getCharSkillPoint(1) >= 80 Then WarSkill(21) = 0 : useAttackWarrior(zMobID, .Item(21).SubItems(1).Text) : Exit Sub
                'Howling Sword
                If .Item(19).Checked And getCharMP() >= 400 And WarSkill(19) <= 0 And getCharSkillPoint(1) >= 70 Then WarSkill(19) = 0 : useAttackWarrior(zMobID, .Item(19).SubItems(1).Text) : Exit Sub
                'Sword Dancing
                If .Item(18).Checked And getCharMP() >= 300 And WarSkill(18) <= 0 And getCharSkillPoint(1) >= 60 Then WarSkill(18) = 0 : useAttackWarrior(zMobID, .Item(18).SubItems(1).Text) : Exit Sub
                'Sword Aura
                If .Item(17).Checked And getCharMP() >= 250 And WarSkill(17) <= 0 And getCharSkillPoint(1) >= 57 Then WarSkill(17) = 0 : useAttackWarrior(zMobID, .Item(17).SubItems(1).Text) : Exit Sub
                'Cleave
                If .Item(14).Checked And getCharMP() >= 150 And WarSkill(14) <= 0 And getCharSkillPoint(1) >= 45 Then WarSkill(14) = 0 : useAttackWarrior(zMobID, .Item(14).SubItems(1).Text) : Exit Sub
                'Prick
                If .Item(12).Checked And getCharMP() >= 120 And WarSkill(12) <= 0 And getCharSkillPoint(1) >= 35 Then WarSkill(12) = 0 : useAttackWarrior(zMobID, .Item(12).SubItems(1).Text) : Exit Sub
                'Carving
                If .Item(10).Checked And getCharMP() >= 90 And WarSkill(10) <= 0 And getCharSkillPoint(1) >= 25 Then WarSkill(10) = 0 : useAttackWarrior(zMobID, .Item(10).SubItems(1).Text) : Exit Sub
                'Hoodwink
                If .Item(6).Checked And getCharMP() >= 30 And WarSkill(6) <= 0 And getCharSkillPoint(1) >= 5 Then WarSkill(6) = 0 : useAttackWarrior(zMobID, .Item(6).SubItems(1).Text) : Exit Sub
                'Stroke
                If .Item(1).Checked And getCharMP() >= 2 And WarSkill(1) <= 0 And getCharSkillPoint(1) >= 0 Then WarSkill(1) = 0 : useAttackWarrior(zMobID, .Item(1).SubItems(1).Text) : Exit Sub
            End If
        End With
    End Sub
    Public Sub useWarriorTimeSkill()
        Dim hpRow As Boolean, mpRow As Boolean
        hpRow = getCharHP() > ((getCharMaxHP() * 50) / 100)
        mpRow = getCharHP() < ((getCharMaxHP() * 20) / 100)
        With frm_2Genel.lstv_AttackWar.Items
            'Sprint
            If .Item(22).Checked And getCharMP() >= 2 And checkSkillID("002") = False And checkSkillID("010") = False Then useTimeSkillWarrior(.Item(22).SubItems(1).Text, 1000, 16)
            'Defense
            If .Item(23).Checked And RepairActive = False And getCharMP() >= 4 And checkSkillID(.Item(23).SubItems(1).Text) = False And getCharLevel() >= 7 Then useTimeSkillWarrior(.Item(23).SubItems(1).Text, 1000, 17)
            'Gain
            If .Item(24).Checked And RepairActive = False And getCharMP() >= 2 And checkSkillID(.Item(24).SubItems(1).Text) = False And getCharSkillPoint(3) >= 5 Then useTimeSkillWarrior(.Item(24).SubItems(1).Text, 1000, 18)
            'Rise
            If .Item(28).Checked And RepairActive = False And WarSkill(28) <= 0 And checkSkillID(.Item(28).SubItems(1).Text) = False And getCharSkillPoint(3) >= 10 Then useTimeSkillWarrior(.Item(28).SubItems(1).Text, 5000, 19)
            'Outrange
            If .Item(31).Checked And RepairActive = False And getCharMP() >= 60 And WarSkill(31) <= 0 And checkSkillID(.Item(31).SubItems(1).Text) = False And getCharSkillPoint(3) >= 20 Then useTimeSkillWarrior(.Item(31).SubItems(1).Text, 61000, 20)
            'Restoration
            If .Item(34).Checked And RepairActive = False And getCharMP() >= 105 And WarSkill(34) <= 0 And checkSkillID(.Item(34).SubItems(1).Text) = False And getCharSkillPoint(3) >= 30 Then useTimeSkillWarrior(.Item(34).SubItems(1).Text, 61000, 21)
            'Blaze Killer
            If .Item(35).Checked And RepairActive = False And WarSkill(35) <= 0 And getCharSkillPoint(3) >= 30 And getCharHP() > ((getCharMaxHP() * 50) / 100) And getCharHP() < ((getCharMaxHP() * 20) / 100) Then useTimeSkillWarrior(.Item(35).SubItems(1).Text, 5000, 22)
            'Return Of Life
            If .Item(39).Checked And RepairActive = False And WarSkill(39) <= 0 And getCharSkillPoint(3) >= 40 And getCharMP() >= 500 And getLastHP() > 250 Then useTimeSkillWarrior(.Item(39).SubItems(1).Text, 5000, 23)
            'Regeneration
            If .Item(41).Checked And RepairActive = False And getCharMP() >= 210 And WarSkill(41) <= 0 And checkSkillID(.Item(41).SubItems(1).Text) = False And getCharSkillPoint(3) >= 50 Then useTimeSkillWarrior(.Item(41).SubItems(1).Text, 61000, 24)
            'Hate
            If .Item(43).Checked And RepairActive = False And getCharMP() >= 150 And WarSkill(43) <= 0 And checkSkillID(.Item(43).SubItems(1).Text) = False And getCharSkillPoint(3) >= 55 Then useTimeSkillWarrior(.Item(43).SubItems(1).Text, 61000, 25)
        End With
    End Sub
    Public Sub useAttackWarrior(ByVal UserID As Long, ByVal SkillNo As String)
        If UserID > 0 Then
            With frm_2Genel.lstv_AttackWar.Items
                Select Case SkillNo
                    Case .Item(32).SubItems(1).Text, .Item(37).SubItems(1).Text, .Item(46).SubItems(1).Text
                        SendPack("3103" & ADWORD((getCharClass() & SkillNo), 8) & getCharID() & "FFFF" & getMobSkillCoor() & "0000000000000000000000000000")
                    Case .Item(27).SubItems(1).Text, .Item(29).SubItems(1).Text, .Item(30).SubItems(1).Text, .Item(33).SubItems(1).Text, .Item(36).SubItems(1).Text, .Item(38).SubItems(1).Text, .Item(40).SubItems(1).Text, .Item(42).SubItems(1).Text, .Item(44).SubItems(1).Text, .Item(45).SubItems(1).Text
                        SendPack("3103" & ADWORD((getCharClass() & SkillNo), 8) & getCharID() & ADWORD((UserID), 4) & "0100010000000000000000000000000000000000")
                    Case .Item(25).SubItems(1).Text
                        SendPack("3103" & ADWORD((getCharClass() & SkillNo), 8) & getCharID() & ADWORD((UserID), 4) & "0000000000000000000000000000000000000000")
                    Case .Item(26).SubItems(1).Text
                        SendPack("3101" & ADWORD((getCharClass() & SkillNo), 8) & getCharID() & "FFFF" & getCharSkillCoor() & "00000000000000000F0000000000")
                        SendPack("3103" & ADWORD((getCharClass() & SkillNo), 8) & getCharID() & "FFFF" & getCharSkillCoor() & "000000000000")
                    Case Else
                        SendPack("3103" & ADWORD((getCharClass() & SkillNo), 8) & getCharID() & ADWORD((UserID), 4) & "01000100000000000000000000000000")
                End Select
            End With
        End If
    End Sub
    Public Sub useTimeSkillWarrior(ByVal skillCode As String, ByVal skillTime As Long, ByVal skillRow As Long)
        With frm_2Genel.lstv_AttackWar.Items
            Select Case skillCode
                Case .Item(41).SubItems(1).Text
                    SendPSec("3103" & ADWORD((getCharClass() & skillCode), 8) & getCharID() & getCharID() & "0100010000000000000000000000000000000000", skillTime, skillRow)
                Case .Item(28).SubItems(1).Text, .Item(31).SubItems(1).Text, .Item(34).SubItems(1).Text, .Item(38).SubItems(1).Text, .Item(43).SubItems(1).Text
                    SendPSec("3103" & ADWORD((getCharClass() & skillCode), 8) & getCharID() & getCharID() & "0000000000000000000000000000000000000000", skillTime, skillRow)
                Case Else
                    SendPSec("3103" & ADWORD((getCharClass() & skillCode), 8) & getCharID() & getCharID() & "00000000000000000000000000000000", skillTime, skillRow)
            End Select
        End With
    End Sub
#End Region
#Region "######## Rogue Skill Sistemi ########"
    Public Sub rogueSkillRow()
        For i As Integer = 0 To 50
            RogueSkill(i) = RogueSkill(i) - 1
        Next
        RogueNormalSkillTimer = RogueNormalSkillTimer - 1
    End Sub
#Region "~~ Rogue Archery Sistemi ~~"
    Public Sub useAttackArchery()

        With frm_2Genel.lstv_AttackRog.Items()
            Dim firstSkill As Integer
            Dim MultipleWait As Integer

            If (frm_2Genel.chk_ComboLegal.Checked) Then
                MultipleWait = 100
            Else
                MultipleWait = 10
            End If
            firstSkill = firstRogueSkill()
            If (firstSkill = 6 Or firstSkill = 14 Or firstSkill = 15 Or firstSkill = 22 Or firstSkill = 23 Or firstSkill = 18 Or firstSkill = 19 Or firstSkill = 12 Or firstSkill = 13 Or firstSkill = 9 Or firstSkill = 10 Or firstSkill = 4 Or firstSkill = 5 Or firstSkill = 21) Then firstSkill = 1
            'Multiple Shot & Arrow Shower
            If .Item(6).Checked And .Item(14).Checked And getMobDistance() < ArchDissAttack Then

                If .Item(firstSkill).Checked And getCharLevel() >= 3 Then useAttackArchery(zMobID, .Item(firstSkill).SubItems(1).Text)

                If getCharMP() >= 40 And RogueSkill(6) <= 0 And getCharSkillPoint(1) >= 15 Then
                    RogueSkill(6) = MultipleWait : tripleArrow(zMobID)
                    If (frm_2Genel.chk_ComboLegal.Checked) Then Exit Sub
                End If
                If getCharMP() >= 150 And RogueSkill(14) <= 0 And getCharSkillPoint(1) >= 55 Then
                    RogueSkill(14) = MultipleWait : pentaArrow(zMobID)
                    If (frm_2Genel.chk_ComboLegal.Checked) Then Exit Sub
                End If

            ElseIf .Item(6).Checked And .Item(14).Selected = False And getMobDistance() < ArchDissAttack Then

                If .Item(firstSkill).Checked And getCharLevel() >= 3 Then useAttackArchery(zMobID, .Item(firstSkill).SubItems(1).Text)

                If getCharMP() >= 40 And RogueSkill(6) <= 0 And getCharSkillPoint(1) >= 15 Then
                    RogueSkill(6) = MultipleWait : tripleArrow(zMobID)
                    If (frm_2Genel.chk_ComboLegal.Checked) Then Exit Sub
                End If
            ElseIf .Item(6).Selected = False And .Item(14).Checked And getMobDistance() < ArchDissAttack Then

                If .Item(firstSkill).Checked And getCharLevel() >= 3 Then useAttackArchery(zMobID, .Item(11).SubItems(1).Text)

                If getCharMP() >= 150 And RogueSkill(14) <= 0 And getCharSkillPoint(1) >= 55 Then
                    RogueSkill(14) = MultipleWait : pentaArrow(zMobID)
                    If (frm_2Genel.chk_ComboLegal.Checked) Then Exit Sub
                End If
            ElseIf .Item(6).Checked And .Item(14).Selected = False And getMobDistance() < ArchDissAttack Then
                If getCharMP() >= 40 And RogueSkill(6) <= 0 And getCharSkillPoint(1) >= 15 Then
                    RogueSkill(6) = MultipleWait : tripleArrow(zMobID)
                    If (frm_2Genel.chk_ComboLegal.Checked) Then Exit Sub
                End If
            ElseIf .Item(6).Selected = False And .Item(14).Checked And getMobDistance() < ArchDissAttack Then
                If getCharMP() >= 150 And RogueSkill(14) <= 0 And getCharSkillPoint(1) >= 55 Then
                    RogueSkill(14) = MultipleWait : pentaArrow(zMobID)
                    If (frm_2Genel.chk_ComboLegal.Checked) Then Exit Sub
                End If
            End If

            If (MobDissAttack > 54) Then
                MobDissAttack = 54
            End If
            If getMobDistance() <= MobDissAttack Then
                'Counter Strike
                If .Item(15).Checked And getCharMP() >= 150 And RogueSkill(15) <= 0 And getCharSkillPoint(1) >= 52 Then RogueSkill(15) = 6100 : useAttackArchery(zMobID, .Item(15).SubItems(1).Text) : Exit Sub
                'Blinding Strafe
                If .Item(22).Checked And getCharMP() >= 300 And RogueSkill(22) <= 0 And getCharSkillPoint(1) >= 75 Then RogueSkill(22) = 6100 : useAttackArchery(zMobID, .Item(22).SubItems(1).Text) : Exit Sub
                'Power Shot
                If .Item(23).Checked And getCharMP() >= 400 And RogueSkill(23) <= 0 And getCharSkillPoint(1) >= 80 Then RogueSkill(23) = 6100 : useAttackArchery(zMobID, .Item(23).SubItems(1).Text) : Exit Sub
                'İce Shot
                If .Item(18).Checked And getCharMP() >= 200 And RogueSkill(18) <= 0 And getCharSkillPoint(1) >= 62 Then RogueSkill(18) = 600 : useAttackArchery(zMobID, .Item(18).SubItems(1).Text) : Exit Sub
                'Lighting Shot
                If .Item(19).Checked And getCharMP() >= 200 And RogueSkill(19) <= 0 And getCharSkillPoint(1) >= 66 Then RogueSkill(19) = 600 : useAttackArchery(zMobID, .Item(19).SubItems(1).Text) : Exit Sub
                'Explosive Shot
                If .Item(12).Checked And getCharMP() >= 50 And RogueSkill(12) <= 0 And getCharSkillPoint(1) >= 45 Then RogueSkill(12) = 490 : useAttackArchery(zMobID, .Item(12).SubItems(1).Text) : Exit Sub
                'Viper
                If .Item(13).Checked And getCharMP() >= 50 And RogueSkill(13) <= 0 And getCharSkillPoint(1) >= 50 Then RogueSkill(13) = 490 : useAttackArchery(zMobID, .Item(13).SubItems(1).Text) : Exit Sub
                'Fire Shot
                If .Item(9).Checked And getCharMP() >= 30 And RogueSkill(9) <= 0 And getCharSkillPoint(1) >= 30 Then RogueSkill(9) = 490 : useAttackArchery(zMobID, .Item(9).SubItems(1).Text) : Exit Sub
                'Poison Shot
                If .Item(10).Checked And getCharMP() >= 30 And RogueSkill(10) <= 0 And getCharSkillPoint(1) >= 35 Then RogueSkill(10) = 490 : useAttackArchery(zMobID, .Item(10).SubItems(1).Text) : Exit Sub
                'FireArrow
                If .Item(4).Checked And getCharMP() >= 10 And RogueSkill(4) <= 0 And getCharSkillPoint(1) >= 5 Then RogueSkill(4) = 400 : useAttackArchery(zMobID, .Item(4).SubItems(1).Text) : Exit Sub
                'Poison Arrow
                If .Item(5).Checked And getCharMP() >= 10 And RogueSkill(5) <= 0 And getCharSkillPoint(1) >= 10 Then RogueSkill(5) = 400 : useAttackArchery(zMobID, .Item(5).SubItems(1).Text) : Exit Sub
                'Blow Arrow
                If .Item(21).Checked And getCharMP() >= 200 And RogueSkill(21) <= 0 And getCharSkillPoint(1) >= 72 Then RogueSkill(21) = 0 : useAttackArchery(zMobID, .Item(21).SubItems(1).Text) : Exit Sub
                'Dark pursuer
                If .Item(20).Checked And getCharMP() >= 350 And RogueSkill(20) <= 0 And getCharSkillPoint(1) >= 70 Then RogueSkill(20) = 0 : useAttackArchery(zMobID, .Item(20).SubItems(1).Text) : Exit Sub
                'Shadow Hunter
                If .Item(17).Checked And getCharMP() >= 300 And RogueSkill(17) <= 0 And getCharSkillPoint(1) >= 60 Then RogueSkill(17) = 0 : useAttackArchery(zMobID, .Item(17).SubItems(1).Text) : Exit Sub
                'Shadow Shot
                If .Item(16).Checked And getCharMP() >= 200 And RogueSkill(16) <= 0 And getCharSkillPoint(1) >= 57 Then RogueSkill(16) = 0 : useAttackArchery(zMobID, .Item(16).SubItems(1).Text) : Exit Sub
                'Arc Shot
                If .Item(11).Checked And getCharMP() >= 100 And RogueSkill(11) <= 0 And getCharSkillPoint(1) >= 40 Then RogueSkill(11) = 0 : useAttackArchery(zMobID, .Item(11).SubItems(1).Text) : Exit Sub
                'Perfect Shot
                If .Item(8).Checked And getCharMP() >= 70 And RogueSkill(8) <= 0 And getCharSkillPoint(1) >= 25 Then RogueSkill(8) = 0 : useAttackArchery(zMobID, .Item(8).SubItems(1).Text) : Exit Sub
                'Guided Arrow
                If .Item(7).Checked And getCharMP() >= 40 And RogueSkill(7) <= 0 And getCharSkillPoint(1) >= 20 Then RogueSkill(7) = 0 : useAttackArchery(zMobID, .Item(7).SubItems(1).Text) : Exit Sub
                'Through Shot
                If .Item(3).Checked And getCharMP() >= 15 And RogueSkill(3) <= 0 And getCharLevel() >= 10 Then RogueSkill(3) = 0 : useAttackArchery(zMobID, .Item(3).SubItems(1).Text) : Exit Sub
                'Archery2
                If .Item(2).Checked And getCharMP() >= 7 And RogueSkill(2) <= 0 And getCharLevel() >= 7 Then RogueSkill(2) = 0 : useAttackArchery(zMobID, .Item(2).SubItems(1).Text) : Exit Sub
                'Archery
                If .Item(1).Checked And getCharMP() >= 0 And RogueSkill(1) <= 0 And getCharLevel() >= 3 Then RogueSkill(1) = 0 : useAttackArchery(zMobID, .Item(1).SubItems(1).Text) : Exit Sub
                'Keys 1
                If .Item(53).Checked And getCharMP() >= 0 And getCharLevel() >= 0 Then SendKeys("1") : Exit Sub

            End If
        End With

    End Sub
    Public Sub useAttackArchery(ByVal UserID As Long, ByVal SkillNo As String)
        If UserID > 0 And AttackStatus = True Then
            Dim SkillID As String, mobSkillCoor As String
            If (getMobDistance() <= 1) Then
                mobSkillCoor = "000000000000"
            Else
                mobSkillCoor = getMobSkillCoor()
            End If
            SkillID = ADWORD((getCharClass() & SkillNo), 8)
            If (NormalAttack) Then
                SendSkillNormal(SkillID, UserID)
            Else
                If SkillNo = "552" Or SkillNo = "585" Then
                    SendPack("3101" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000A00")
                    SendPack("3102" & SkillID & getCharID() & ADWORD((UserID), 4) & "000000000000000000000000")
                    SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000000")
                    SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & mobSkillCoor & "9BFF0000000000000000")
                Else
                    SendPack("3101" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000D00")
                    SendPack("3102" & SkillID & getCharID() & ADWORD((UserID), 4) & "000000000000000000000000")
                    SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000000")
                    SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & mobSkillCoor & "9BFF0000000000000000")
                End If
            End If
        End If
    End Sub
    Public Sub tripleArrow(ByVal UserID As Long)
        If UserID <= 0 Then Exit Sub
        Dim SkillID As String, mobSkillCoor As String
        If (getMobDistance() <= 1) Then
            mobSkillCoor = "000000000000"
        Else
            mobSkillCoor = getMobSkillCoor()
        End If
        SkillID = ADWORD((getCharClass() & "515"), 8)

        If (NormalAttack) Then
            SendSkillNormal(SkillID, UserID)
        Else
            If (frm_2Genel.chk_ArcheryCombo.Checked) Then
                If (RogueArrowCombo >= 2) Then RogueArrowCombo = 0
                SendPack("06" _
                             & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X) + RogueArrowCombo) * 10, 4) _
                             & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y) + RogueArrowCombo) * 10, 4) _
                             & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z)) * 10, 4) _
                             & "2D0000" _
                             & ADWORD((getCharX() + RogueArrowCombo) * 10, 4) _
                             & ADWORD((getCharY() + RogueArrowCombo) * 10, 4) _
                             & ADWORD(getCharZ() * 10, 4))
                RogueArrowCombo += 1
            End If
            SendPack("3101" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000D00")
            SendPack("3102" & SkillID & getCharID() & ADWORD((UserID), 4) & "000000000000010000000000")
            SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000001000000000000000000")
            SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & mobSkillCoor & "9BFF0100000000000000")
            SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000002000000000000000000")
            SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & mobSkillCoor & "9BFF0200000000000000")
            SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000003000000000000000000")
            SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & mobSkillCoor & "9BFF0300000000000000")
        End If
    End Sub
    Public Sub pentaArrow(ByVal UserID As Long)
        If UserID <= 0 Then Exit Sub
        Dim SkillID As String, mobSkillCoor As String
        If (getMobDistance() <= 1) Then
            mobSkillCoor = "000000000000"
        Else
            mobSkillCoor = getMobSkillCoor()
        End If
        SkillID = ADWORD((getCharClass() & "555"), 8)
        If (NormalAttack) Then
            SendSkillNormal(SkillID, UserID)
        Else
            If (frm_2Genel.chk_ArcheryCombo.Checked) Then
                If (RogueArrowCombo >= 2) Then RogueArrowCombo = 0
                SendPack("06" _
                             & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_X) + RogueArrowCombo) * 10, 4) _
                             & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Y) + RogueArrowCombo) * 10, 4) _
                             & ADWORD(CInt(ReadFloat(ReadLong(KO_PTR_CHR) + KO_OFF_Z)) * 10, 4) _
                             & "2D0000" _
                             & ADWORD((getCharX() + RogueArrowCombo) * 10, 4) _
                             & ADWORD((getCharY() + RogueArrowCombo) * 10, 4) _
                             & ADWORD(getCharZ() * 10, 4))
                RogueArrowCombo += 1
            End If
            SendPack("3101" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000F00")
            SendPack("3102" & SkillID & getCharID() & ADWORD((UserID), 4) & "000000000000010000000000")
            SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000001000000000000000000")
            SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & mobSkillCoor & "9BFF0100000000000000")
            SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000002000000000000000000")
            SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & mobSkillCoor & "9BFF0200000000000000")
            SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000003000000000000000000")
            SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & mobSkillCoor & "9BFF0300000000000000")
            SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000004000000000000000000")
            SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & mobSkillCoor & "9BFF0400000000000000")
            SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000005000000000000000000")
            SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & mobSkillCoor & "9BFF0500000000000000")
        End If
    End Sub
#End Region
#Region "~~ Rogue Asas Sistemi ~~"
    Public Sub useAssaianSkill()
        With frm_2Genel.lstv_AttackRog.Items
            If (MobDissAttack > 14) Then
                MobDissAttack = 14
            End If
            If getMobDistance() <= MobDissAttack Then
                'R Atak
                If .Item(0).Checked Then useAttackR(zMobID, .Item(0).SubItems(1).Text)
                'Critical Point
                If .Item(39).Checked And getCharMP() >= 200 And RogueSkill(39) <= 0 And getCharSkillPoint(2) >= 80 Then RogueSkill(39) = 6100 : useAttackAssaian(zMobID, .Item(39).SubItems(1).Text) : Exit Sub
                'Blinding 
                If .Item(37).Checked And getCharMP() >= 200 And RogueSkill(37) <= 0 And getCharSkillPoint(2) >= 72 Then RogueSkill(37) = 6100 : useAttackAssaian(zMobID, .Item(37).SubItems(1).Text) : Exit Sub
                'Vampiric touch
                If .Item(34).Checked And getCharMP() >= 50 And RogueSkill(34) <= 0 And getCharSkillPoint(2) >= 50 Then RogueSkill(34) = 6100 : useAttackAssaian(zMobID, .Item(34).SubItems(1).Text) : Exit Sub
                'Blood drain
                If .Item(28).Checked And getCharMP() >= 20 And RogueSkill(28) <= 0 And getCharSkillPoint(2) >= 10 Then RogueSkill(28) = 6100 : useAttackAssaian(zMobID, .Item(28).SubItems(1).Text) : Exit Sub
                'Beast Hiding
                If .Item(38).Checked And getCharMP() >= 250 And RogueSkill(38) <= 0 And getCharSkillPoint(2) >= 75 Then RogueSkill(38) = 4100 : useAttackAssaian(zMobID, .Item(38).SubItems(1).Text) : Exit Sub
                'Spike
                If .Item(35).Checked And getCharMP() >= 100 And RogueSkill(35) <= 0 And getCharSkillPoint(2) >= 55 Then RogueSkill(35) = 1090 : useAttackAssaian(zMobID, .Item(35).SubItems(1).Text) : Exit Sub
                'thrust 
                If .Item(32).Checked And getCharMP() >= 50 And RogueSkill(32) <= 0 And getCharSkillPoint(2) >= 35 Then RogueSkill(32) = 1090 : useAttackAssaian(zMobID, .Item(32).SubItems(1).Text) : Exit Sub
                'Pierce
                If .Item(29).Checked And getCharMP() >= 20 And RogueSkill(29) <= 0 And getCharSkillPoint(2) >= 15 Then RogueSkill(29) = 1090 : useAttackAssaian(zMobID, .Item(29).SubItems(1).Text) : Exit Sub
                'Illusion
                If .Item(31).Checked And getCharMP() >= 30 And RogueSkill(31) <= 0 And getCharSkillPoint(2) >= 30 Then RogueSkill(31) = 1090 : useAttackAssaian(zMobID, .Item(31).SubItems(1).Text) : Exit Sub
                'Cut 
                If .Item(33).Checked And getCharMP() >= 50 And RogueSkill(33) <= 0 And getCharSkillPoint(2) >= 40 Then RogueSkill(33) = 500 : useAttackAssaian(zMobID, .Item(33).SubItems(1).Text) : Exit Sub
                'Bloody Beast
                If .Item(36).Checked And getCharMP() >= 100 And RogueSkill(36) <= 0 And getCharSkillPoint(2) >= 70 Then RogueSkill(36) = 500 : useAttackAssaian(zMobID, .Item(36).SubItems(1).Text) : Exit Sub
                'shock
                If .Item(30).Checked And getCharMP() >= 20 And RogueSkill(30) <= 0 And getCharSkillPoint(2) >= 20 Then RogueSkill(30) = 490 : useAttackAssaian(zMobID, .Item(30).SubItems(1).Text) : Exit Sub
                'Jab
                If .Item(27).Checked And getCharMP() >= 10 And RogueSkill(27) <= 0 And getCharLevel() >= 10 Then RogueSkill(27) = 490 : useAttackAssaian(zMobID, .Item(27).SubItems(1).Text) : Exit Sub
                'Stab2
                If .Item(26).Checked And getCharMP() >= 5 And RogueSkill(26) <= 0 And getCharLevel() >= 7 Then RogueSkill(26) = 500 : useAttackAssaian(zMobID, .Item(26).SubItems(1).Text) : Exit Sub
                'Stab
                If .Item(25).Checked And getCharMP() >= 5 And RogueSkill(25) <= 0 And getCharLevel() >= 5 Then RogueSkill(25) = 500 : useAttackAssaian(zMobID, .Item(25).SubItems(1).Text) : Exit Sub
                'Stroke
                If .Item(24).Checked And getCharMP() >= 2 And RogueSkill(24) <= 0 And getCharLevel() >= 0 Then RogueSkill(24) = 0 : useAttackAssaian(zMobID, .Item(24).SubItems(1).Text) : Exit Sub
            End If
        End With
    End Sub
    Public Sub useAttackAssaian(ByVal UserID As Long, ByVal SkillNo As String)
        If UserID > 0 And CharRogueJob() = True Then
            If (NormalAttack) Then
                SendSkillNormal(ADWORD((getCharClass() & SkillNo), 8), UserID)
            Else
                If SkillNo = "610" Or SkillNo = "650" Or SkillNo = "630" Then
                    SendPack("3101" & ADWORD((getCharClass() & SkillNo), 8) & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000001000")
                    SendPack("3103" & ADWORD((getCharClass() & SkillNo), 8) & getCharID() & ADWORD((UserID), 4) & "000000000000000000000000")
                Else
                    SendPack("3103" & ADWORD((getCharClass() & SkillNo), 8) & getCharID() & ADWORD((UserID), 4) & "01000100000000000000000000000000")
                End If
            End If
        End If
    End Sub
#End Region
#Region "~~ RogueZaman Skilleri ~~"
    Public Sub useRogueTimeSkill()
        With frm_2Genel.lstv_AttackRog.Items
            'Sprint
            If .Item(40).Checked And getCharMP() >= 2 And getCharLevel() >= 1 Then useRogueSprint()
            'Swift
            If .Item(41).Checked And getCharMP() >= 15 And getCharLevel() >= 10 Then useSwift()
            'Wolf
            If .Item(42).Checked And RepairActive = False And getCharMP() >= 30 And getCharLevel() >= 30 Then useWolf()
            'Hide
            If .Item(43).Checked And RepairActive = False And getCharMP() >= 40 And getCharSkillPoint(3) >= 30 And RogueSkill(43) <= 0 Then RogueSkill(43) = 1100 : useHide()
            'CatEyes
            If .Item(45).Checked And RepairActive = False And getCharMP() >= 60 And getCharSkillPoint(3) >= 15 Then useCatsEye()
            'Light Feet
            If .Item(46).Checked And getCharMP() >= 40 And getCharSkillPoint(3) >= 25 Then useLightFeet()
            'Lupine Eyes
            If .Item(48).Checked And RepairActive = False And getCharMP() >= 200 And getCharSkillPoint(3) >= 35 Then useLupineEyes()
            'Stealth
            If .Item(50).Checked And RepairActive = False And getCharMP() >= 270 And getCharSkillPoint(2) >= 45 And RogueSkill(50) <= 0 Then RogueSkill(50) = 1100 : useStealth()

            If RepairActive = False And checkSkillID("710") = False And checkSkillID("730") = False And checkSkillID("760") = False And (frm_2Genel.chk_NormalSkill.Checked And (frm_2Genel.chk_NormalSkill.Checked And getMobDistance() < 3)) Then
                'ScaledSkin
                If .Item(49).Checked And getCharMP() >= 160 And getCharSkillPoint(3) >= 60 And RogueSkill(49) <= 0 Then
                    RogueSkill(47) = 50
                    RogueSkill(44) = 50
                    useScaledSkin()
                    Exit Sub
                End If

                If .Item(47).Checked And getCharMP() >= 80 And getCharSkillPoint(3) >= 30 And RogueSkill(47) <= 0 Then
                    RogueSkill(44) = 50
                    RogueSkill(49) = 50
                    useSafety()
                    Exit Sub
                End If
                'Evade
                If .Item(44).Checked And getCharMP() >= 40 And getCharSkillPoint(3) >= 10 And RogueSkill(44) <= 0 Then
                    RogueSkill(49) = 50
                    RogueSkill(47) = 50
                    useEvade()
                    Exit Sub
                End If
            End If
        End With
    End Sub
    Public Sub useRogueSprint()
        If checkSkillID("002") = 0 And checkSkillID("010") = 0 And checkSkillID("725") = 0 Then
            SendPSec("3103" & ADWORD((getCharClass() & "002"), 8) & getCharID() & getCharID() & "000000000000000000000000", 1000, 26)
        End If
    End Sub
    Public Sub useSwift()
        If (NormalSkill) Then
            If (RogueNormalSkillTimer <= 0 And checkSkillID("010") = False) Then
                If frm_2Genel.btn_ProjectStart.Text = "Durdur" Then AttackStatus = False
                If (AttackStatus = False) Then
                    Cli.SetCharSpeed(False)
                    SendSkillNormal(ADWORD(getCharClass() & "010", 8), getCharLongID(), True)
                    RogueNormalSkillTimer = 100
                    Exit Sub
                End If
            ElseIf (AttackStatus = False And frm_2Genel.btn_ProjectStart.Text = "Durdur" And checkSkillID("010") And frm_2Genel.lst_BoxCoor.Items.Count <= 0) Then
                Cli.SetCharSpeed(True)
                AttackStatus = True
            End If
        Else
            useSkillSwift()
        End If
    End Sub
    Public Sub useSkillSwift()
        If checkSkillID("002") = False And checkSkillID("010") = False And checkSkillID("725") = False Then
            SendPSec("3101" & ADWORD((getCharClass() & "010"), 8) & getCharID() & getCharID() & "00000000000000000000000000000F00", 1000, 27)
            SendPSec("3103" & ADWORD((getCharClass() & "010"), 8) & getCharID() & getCharID() & ADWORD((getCharX()), 4) & ADWORD((getCharZ()), 4) & ADWORD((getCharY()), 4) & "000000000000", 1000, 28)
        End If
    End Sub
    Public Sub useSwiftID(ByVal UserID As Long)
        If UserID > 0 Then
            SendPSec("3101" & ADWORD((getCharClass() & "010"), 8) & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000F00", 1000, 29)
            SendPSec("3103" & ADWORD((getCharClass() & "010"), 8) & getCharID() & ADWORD((UserID), 4) & getMobSkillCoor() & "000000000000", 1000, 30)
        End If
    End Sub
    Public Sub useLightFeet()
        If checkSkillID("002") = False And checkSkillID("010") = False And checkSkillID("725") = False Then
            SendPSec("3103" & ADWORD((getCharClass() & "725"), 8) & getCharID() & getCharID() & "0000000000000000000000000000", 1000, 31)
        End If
    End Sub
    Public Sub useWolf()
        If getItemSlotInv("370004000") <= 0 Then Exit Sub
        If (NormalSkill) Then
            If (RogueNormalSkillTimer <= 0 And checkSkillID("030") = False) Then
                If frm_2Genel.btn_ProjectStart.Text = "Durdur" Then AttackStatus = False
                If (AttackStatus = False) Then
                    Cli.SetCharSpeed(False)
                    SendSkillNormal(ADWORD(getCharClass() & "030", 8), getCharLongID(), True)
                    RogueNormalSkillTimer = 100
                    Exit Sub
                End If
            ElseIf (checkSkillID("030") = True And frm_2Genel.btn_ProjectStart.Text = "Durdur" And AttackStatus = False) Then
                Cli.SetCharSpeed(True)
                AttackStatus = True
            End If
        Else
            useSkillWolf()
        End If
    End Sub
    Public Sub useSkillWolf()
        If checkSkillID("030") = False And getItemSlotInv("370004000") > 0 Then
            SendPSec("3101" & ADWORD((getCharClass() & "030"), 8) & getCharID() & "FFFF" & ADWORD((getCharX()), 4) & ADWORD((getCharZ()), 4) & ADWORD((getCharY()), 4) & "00000000000000001100", 1000, 32)
            SendPSec("3103" & ADWORD((getCharClass() & "030"), 8) & getCharID() & "FFFF" & ADWORD((getCharX()), 4) & ADWORD((getCharZ()), 4) & ADWORD((getCharY()), 4) & "000000000000", 1000, 33)
        End If
    End Sub
    Public Sub useHide()
        If checkSkillID("645") = False And checkSkillID("700") = False Then
            SendPSec("3101" & ADWORD((getCharClass() & "700"), 8) & getCharID() & getCharID() & "00000000000000000000000000000F00", 1000, 34)
            SendPSec("3103" & ADWORD((getCharClass() & "700"), 8) & getCharID() & getCharID() & "0000000000000000000000000000", 1000, 35)
        End If
    End Sub
    Public Sub useScaledSkin()
        If checkSkillID("710") = False And checkSkillID("730") = False And checkSkillID("760") = False Then
            SendPSec("3103" & ADWORD((getCharClass() & "760"), 8) & getCharID() & getCharID() & "0000000000000000000000000000", 30000, 36)
            RogueSkill(49) = 300
        End If
    End Sub
    Public Sub useSafety()
        If checkSkillID("710") = False And checkSkillID("730") = False And checkSkillID("760") = False Then
            SendPSec("3103" & ADWORD((getCharClass() & "730"), 8) & getCharID() & getCharID() & "0000000000000000000000000000", 30000, 37)
            RogueSkill(47) = 300
        End If
    End Sub
    Public Sub useEvade()
        If checkSkillID("710") = False And checkSkillID("730") = False And checkSkillID("760") = False Then
            SendPSec("3103" & ADWORD((getCharClass() & "710"), 8) & getCharID() & getCharID() & "0000000000000000000000000000", 30000, 38)
            RogueSkill(44) = 300
        End If
    End Sub
    Public Sub useCatsEye()
        If checkSkillID("715") = False And checkSkillID("735") = False Then
            SendPSec("3101" & ADWORD((getCharClass() & "715"), 8) & getCharID() & getCharID() & "00000000000000000000000000000F00", 1000, 39)
            SendPSec("3103" & ADWORD((getCharClass() & "715"), 8) & getCharID() & getCharID() & "000000000000000000000000", 1000, 40)
        End If
    End Sub
    Public Sub useLupineEyes()
        If checkSkillID("715") = False And checkSkillID("735") = False Then
            SendPSec("3101" & ADWORD((getCharClass() & "735"), 8) & getCharID() & getCharID() & "00000000000000000000000000001400", 1000, 41)
            SendPSec("3103" & ADWORD((getCharClass() & "735"), 8) & getCharID() & getCharID() & "000000000000000000000000", 1000, 42)
        End If
    End Sub
    Public Sub useStealth()
        If checkSkillID("645") = False And checkSkillID("700") = False Then
            SendPSec("3101" & ADWORD((getCharClass() & "645"), 8) & getCharID() & getCharID() & "00000000000000000000000000001E00", 1000, 43)
            SendPSec("3103" & ADWORD((getCharClass() & "645"), 8) & getCharID() & getCharID() & "0000000000000000000000000000", 1000, 44)
        End If
    End Sub
#End Region
#End Region
#Region "######## Mage Skill Sistemi ########"
    Public Sub mageSkillRow()
        For i As Integer = 0 To 66
            MageSkill(i) = MageSkill(i) - 1
        Next
    End Sub
    Public Sub useMageSkill()
        If getMobDistance() <= MobDissAttack Then
            With frm_2Genel.lstv_AttackMage.Items
                'İdeal hız 970
                'Ignition 990
                'R Atak
                If .Item(0).Checked = True And getMobDistance() <= 15 Then useAttackR(zMobID, .Item(0).SubItems(1).Text)

                '###########################################################################################################################################################
                '################### Alan Skilleri ################### Alan Skilleri ################### Alan Skilleri ################### Alan Skilleri ###################
                '###########################################################################################################################################################

                If frm_2Genel.chk_attackArea.Checked = True Then ' Alan atak aktifse
                    'Meteor Fall
                    If .Item(58).Checked = True And getCharMP() >= 600 And MageSkill(58) <= 0 And getMobDistance() <= 23 And getCharSkillPoint(1) >= 70 Then MageSkill(58) = 1900 : useAreaSkillMage(zMobID, .Item(58).SubItems(1).Text) : Exit Sub
                    'Chain Lightning
                    If .Item(64).Checked = True And getCharMP() >= 600 And MageSkill(64) <= 0 And getMobDistance() <= 23 And getCharSkillPoint(3) >= 70 Then MageSkill(64) = 1900 : useAreaSkillMage(zMobID, .Item(64).SubItems(1).Text) : Exit Sub
                    'Ice Storm
                    If .Item(61).Checked = True And getCharMP() >= 600 And MageSkill(61) <= 0 And getMobDistance() <= 23 And getCharSkillPoint(2) >= 70 Then MageSkill(61) = 1900 : useAreaSkillMage(zMobID, .Item(61).SubItems(1).Text) : Exit Sub
                    'Super Nova
                    If .Item(59).Checked = True And getCharMP() >= 400 And MageSkill(59) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(1) >= 60 Then MageSkill(59) = 1700 : useAreaSkillMage(zMobID, .Item(59).SubItems(1).Text) : Exit Sub
                    'Static Nova
                    If .Item(65).Checked = True And getCharMP() >= 400 And MageSkill(65) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(3) >= 60 Then MageSkill(65) = 1700 : useAreaSkillMage(zMobID, .Item(65).SubItems(1).Text) : Exit Sub
                    'Frost Nova
                    If .Item(62).Checked = True And getCharMP() >= 400 And MageSkill(62) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(2) >= 60 Then MageSkill(62) = 1700 : useAreaSkillMage(zMobID, .Item(62).SubItems(1).Text) : Exit Sub
                    'Blizzard
                    If .Item(63).Checked = True And getCharMP() >= 200 And MageSkill(63) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(2) >= 45 Then MageSkill(63) = 1700 : useAreaSkillMage(zMobID, .Item(63).SubItems(1).Text) : Exit Sub
                    'Inferno
                    If .Item(60).Checked = True And getCharMP() >= 200 And MageSkill(60) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(1) >= 45 Then MageSkill(60) = 1600 : useAreaSkillMage(zMobID, .Item(60).SubItems(1).Text) : Exit Sub
                    'Thundercloud
                    If .Item(66).Checked = True And getCharMP() >= 200 And MageSkill(66) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(3) >= 45 Then MageSkill(66) = 1700 : useAreaSkillMage(zMobID, .Item(66).SubItems(1).Text) : Exit Sub
                    'Fire burst
                    If .Item(55).Checked = True And getCharMP() >= 0 And MageSkill(55) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(1) >= 33 Then MageSkill(55) = 200 : useAreaSkillMage(zMobID, .Item(55).SubItems(1).Text) : Exit Sub
                    'Ice burst
                    If .Item(56).Checked = True And getCharMP() >= 0 And MageSkill(56) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(2) >= 33 Then MageSkill(56) = 200 : useAreaSkillMage(zMobID, .Item(56).SubItems(1).Text) : Exit Sub
                    'Thunder burst
                    If .Item(57).Checked = True And getCharMP() >= 150 And MageSkill(57) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(3) >= 33 Then MageSkill(57) = 200 : useAreaSkillMage(zMobID, .Item(57).SubItems(1).Text) : Exit Sub
                End If

                '###########################################################################################################################################################
                '################### İce Skilleri ################### İce Skilleri ################### İce Skilleri ################### İce Skilleri #######################
                '###########################################################################################################################################################

                'Ice Blast
                If .Item(30).Checked = True And getCharMP() >= 150 And MageSkill(30) <= 0 And getMobDistance() <= 49 And getCharSkillPoint(2) >= 35 Then MageSkill(30) = 490 : useAttackMageSkill(zMobID, .Item(30).SubItems(1).Text) : Exit Sub
                'Frostbite
                If .Item(31).Checked = True And getCharMP() >= 150 And MageSkill(31) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(2) >= 39 Then MageSkill(31) = 490 : useAttackMageSkill(zMobID, .Item(31).SubItems(1).Text) : Exit Sub
                'Ice Orb
                If .Item(29).Checked = True And getCharMP() >= 80 And MageSkill(29) <= 0 And getMobDistance() <= 49 And getCharSkillPoint(2) >= 27 Then MageSkill(29) = 490 : useAttackMageSkill(zMobID, .Item(29).SubItems(1).Text) : Exit Sub
                'Freezing Distance
                If .Item(39).Checked = True And getCharMP() >= 350 And MageSkill(39) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(2) >= 80 Then MageSkill(39) = 6100 : useAttackMageSkill(zMobID, .Item(39).SubItems(1).Text) : Exit Sub
                'Prismatic
                If .Item(37).Checked = True And getCharMP() >= 390 And MageSkill(37) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(2) >= 70 Then MageSkill(37) = 2200 : useAttackMageSkill(zMobID, .Item(37).SubItems(1).Text) : Exit Sub
                'Ice Impact
                If .Item(36).Checked = True And getCharMP() >= 220 And MageSkill(36) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(2) >= 57 Then MageSkill(36) = 2100 : useAttackMageSkill(zMobID, .Item(36).SubItems(1).Text) : Exit Sub
                'Ice Comet
                If .Item(34).Checked = True And getCharMP() >= 160 And MageSkill(34) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(2) >= 51 Then MageSkill(34) = 600 : useAttackMageSkill(zMobID, .Item(34).SubItems(1).Text) : Exit Sub
                'Ice Arrow
                If .Item(27).Checked = True And getCharMP() >= 50 And MageSkill(27) <= 0 And getMobDistance() <= 39 And getCharSkillPoint(2) >= 15 Then MageSkill(27) = 590 : useAttackMageSkill(zMobID, .Item(27).SubItems(1).Text) : Exit Sub
                'Chill
                If .Item(26).Checked = True And getCharMP() >= 30 And MageSkill(26) <= 0 And getMobDistance() <= 39 And getCharSkillPoint(2) >= 9 Then MageSkill(26) = 600 : useAttackMageSkill(zMobID, .Item(26).SubItems(1).Text) : Exit Sub
                'Ice Staff
                If .Item(38).Checked = True And getCharMP() >= 300 And MageSkill(38) <= 0 And getMobDistance() <= 14 And getCharSkillPoint(2) >= 72 Then MageSkill(38) = 0 : useAttackMageSkill(zMobID, .Item(38).SubItems(1).Text) : Exit Sub
                'Manes of Ice
                If .Item(35).Checked = True And getCharMP() >= 95 And MageSkill(35) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(2) >= 56 Then MageSkill(35) = 0 : useAttackMageSkill(zMobID, .Item(35).SubItems(1).Text) : Exit Sub
                'Specter of Ice
                If .Item(33).Checked = True And getCharMP() >= 75 And MageSkill(33) <= 0 And getMobDistance() <= 19 And getCharSkillPoint(2) >= 43 Then MageSkill(33) = 0 : useAttackMageSkill(zMobID, .Item(33).SubItems(1).Text) : Exit Sub
                'Solid
                If .Item(28).Checked = True And getCharMP() >= 60 And MageSkill(28) <= 0 And getMobDistance() <= 42 And getCharSkillPoint(2) >= 18 Then MageSkill(28) = 0 : useAttackMageSkill(zMobID, .Item(28).SubItems(1).Text) : Exit Sub
                'Frozen Blade
                If .Item(32).Checked = True And getCharMP() >= 100 And MageSkill(32) <= 0 And getMobDistance() <= 14 And getCharSkillPoint(2) >= 42 Then MageSkill(32) = 0 : useAttackMageSkill(zMobID, .Item(32).SubItems(1).Text) : Exit Sub
                'Freeze
                If .Item(25).Checked = True And getCharMP() >= 20 And MageSkill(25) <= 0 And getMobDistance() <= 19 And getCharSkillPoint(2) >= 3 Then MageSkill(25) = 0 : useAttackMageSkill(zMobID, .Item(25).SubItems(1).Text) : Exit Sub


                '###########################################################################################################################################################
                '################# Flame Skilleri ################# Flame Skilleri ################# Flame Skilleri ################# Flame Skilleri #######################
                '###########################################################################################################################################################

                'Blaze
                If .Item(9).Checked = True And getCharMP() >= 30 And MageSkill(9) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(1) >= 9 Then MageSkill(9) = 600 : useAttackMageSkill(zMobID, .Item(9).SubItems(1).Text) : Exit Sub
                'Fire Ball
                If .Item(10).Checked = True And getCharMP() >= 50 And MageSkill(10) <= 0 And getMobDistance() <= 23 And getCharSkillPoint(1) >= 15 Then MageSkill(10) = 590 : useAttackMageSkill(zMobID, .Item(10).SubItems(1).Text) : Exit Sub
                'Fire Thorn
                If .Item(18).Checked = True And getCharMP() >= 220 And getItemSlotInv("379061000") > 0 And MageSkill(18) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(1) >= 54 Then MageSkill(18) = 700 : useAttackMageSkill(zMobID, .Item(18).SubItems(1).Text) : Exit Sub
                'Pillar of Fire
                If .Item(17).Checked = True And getCharMP() >= 160 And MageSkill(17) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(1) >= 51 Then MageSkill(17) = 600 : useAttackMageSkill(zMobID, .Item(17).SubItems(1).Text) : Exit Sub
                'Fire Spear
                If .Item(12).Checked = True And getCharMP() >= 80 And MageSkill(12) <= 0 And getMobDistance() <= 49 And getCharSkillPoint(1) >= 27 Then MageSkill(12) = 490 : useAttackMageSkill(zMobID, .Item(12).SubItems(1).Text) : Exit Sub
                'Fire Blast
                If .Item(13).Checked = True And getCharMP() >= 150 And MageSkill(13) <= 0 And getMobDistance() <= 49 And getCharSkillPoint(1) >= 35 Then MageSkill(13) = 490 : useAttackMageSkill(zMobID, .Item(13).SubItems(1).Text) : Exit Sub
                'Hell Fire
                If .Item(14).Checked = True And getCharMP() >= 150 And MageSkill(14) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(1) >= 39 Then MageSkill(14) = 490 : useAttackMageSkill(zMobID, .Item(14).SubItems(1).Text) : Exit Sub
                'Vampiric Fire
                If .Item(23).Checked = True And getCharMP() >= 350 And MageSkill(23) <= 0 And getMobDistance() <= 14 And getCharSkillPoint(1) >= 80 Then MageSkill(23) = 6100 : useAttackMageSkill(zMobID, .Item(23).SubItems(1).Text) : Exit Sub
                'Igzination
                If .Item(24).Checked = True And getCharMP() >= 390 And MageSkill(24) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(1) >= 80 Then MageSkill(24) = 2200 : useAttackMageSkill(zMobID, .Item(24).SubItems(1).Text) : Exit Sub
                'Fire Impact
                If .Item(20).Checked = True And getCharMP() >= 220 And MageSkill(20) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(1) >= 57 Then MageSkill(20) = 2100 : useAttackMageSkill(zMobID, .Item(20).SubItems(1).Text) : Exit Sub
                'Incineration
                If .Item(21).Checked = True And getCharMP() >= 390 And MageSkill(21) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(1) >= 70 Then MageSkill(21) = 2200 : useAttackMageSkill(zMobID, .Item(21).SubItems(1).Text) : Exit Sub
                'Fire Blade
                If .Item(15).Checked = True And getCharMP() >= 100 And MageSkill(15) <= 0 And getMobDistance() <= 14 And getCharSkillPoint(1) >= 42 Then MageSkill(15) = 0 : useAttackMageSkill(zMobID, .Item(15).SubItems(1).Text) : Exit Sub
                'Manes of Fire
                If .Item(19).Checked = True And getCharMP() >= 95 And MageSkill(19) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(1) >= 56 Then MageSkill(19) = 0 : useAttackMageSkill(zMobID, .Item(19).SubItems(1).Text) : Exit Sub
                'Specter of Fire
                If .Item(16).Checked = True And getCharMP() >= 75 And MageSkill(16) <= 0 And getMobDistance() <= 19 And getCharSkillPoint(1) >= 43 Then MageSkill(16) = 0 : useAttackMageSkill(zMobID, .Item(16).SubItems(1).Text) : Exit Sub
                'Fire Staff
                If .Item(22).Checked = True And getCharMP() >= 300 And MageSkill(22) <= 0 And getMobDistance() <= 14 And getCharSkillPoint(1) >= 72 Then MageSkill(22) = 0 : useAttackMageSkill(zMobID, .Item(22).SubItems(1).Text) : Exit Sub
                'Ignition
                If .Item(11).Checked = True And getCharMP() >= 60 And MageSkill(11) <= 0 And getMobDistance() <= 42 And getCharSkillPoint(1) >= 18 Then MageSkill(11) = 0 : useAttackMageSkill(zMobID, .Item(11).SubItems(1).Text) : Exit Sub
                'Burn
                If .Item(8).Checked = True And getCharMP() >= 20 And MageSkill(8) <= 0 And getMobDistance() <= 19 And getCharSkillPoint(1) >= 3 Then MageSkill(8) = 0 : useAttackMageSkill(zMobID, .Item(8).SubItems(1).Text) : Exit Sub

                '###########################################################################################################################################################
                '################### Lig Skilleri ################### Lig Skilleri ################### Lig Skilleri ################### Lig Skilleri #######################
                '###########################################################################################################################################################
                'Thunder Impact
                If .Item(52).Checked = True And getCharMP() >= 220 And MageSkill(52) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(3) >= 57 Then MageSkill(52) = 2100 : useAttackMageSkill(zMobID, .Item(52).SubItems(1).Text) : Exit Sub
                'Stun Cloud
                If .Item(53).Checked = True And getCharMP() >= 390 And MageSkill(53) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(3) >= 70 Then MageSkill(53) = 2200 : useAttackMageSkill(zMobID, .Item(53).SubItems(1).Text) : Exit Sub
                'Static Orb
                If .Item(49).Checked = True And getCharMP() >= 160 And MageSkill(49) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(3) >= 51 Then MageSkill(49) = 600 : useAttackMageSkill(zMobID, .Item(49).SubItems(1).Text) : Exit Sub
                'Static Thorn
                If .Item(50).Checked = True And getCharMP() >= 220 And getItemSlotInv("379061000") > 0 And MageSkill(50) <= 0 And getMobDistance() <= 40 And getCharSkillPoint(3) >= 54 Then MageSkill(50) = 700 : useAttackMageSkill(zMobID, .Item(50).SubItems(1).Text) : Exit Sub
                'Counter Spell
                If .Item(41).Checked = True And getCharMP() >= 30 And MageSkill(41) <= 0 And getMobDistance() <= 39 And getCharSkillPoint(3) >= 9 Then MageSkill(41) = 600 : useAttackMageSkill(zMobID, .Item(41).SubItems(1).Text) : Exit Sub
                'Lightning
                If .Item(42).Checked = True And getCharMP() >= 50 And MageSkill(42) <= 0 And getMobDistance() <= 49 And getCharSkillPoint(3) >= 15 Then MageSkill(42) = 590 : useAttackMageSkill(zMobID, .Item(42).SubItems(1).Text) : Exit Sub
                'Thunder
                If .Item(44).Checked = True And getCharMP() >= 80 And MageSkill(44) <= 0 And getMobDistance() <= 49 And getCharSkillPoint(3) >= 27 Then MageSkill(44) = 490 : useAttackMageSkill(zMobID, .Item(44).SubItems(1).Text) : Exit Sub
                'Thunder Blast
                If .Item(45).Checked = True And getCharMP() >= 150 And MageSkill(45) <= 0 And getMobDistance() <= 49 And getCharSkillPoint(3) >= 35 Then MageSkill(45) = 490 : useAttackMageSkill(zMobID, .Item(45).SubItems(1).Text) : Exit Sub
                'Discharge
                If .Item(46).Checked = True And getCharMP() >= 150 And MageSkill(46) <= 0 And getMobDistance() <= 49 And getCharSkillPoint(3) >= 39 Then MageSkill(46) = 490 : useAttackMageSkill(zMobID, .Item(46).SubItems(1).Text) : Exit Sub
                'Manes of Thunder
                If .Item(51).Checked = True And getCharMP() >= 95 And MageSkill(51) <= 0 And getMobDistance() <= 24 And getCharSkillPoint(3) >= 56 Then MageSkill(51) = 0 : useAttackMageSkill(zMobID, .Item(51).SubItems(1).Text) : Exit Sub
                'Specter of Thunder
                If .Item(48).Checked = True And getCharMP() >= 75 And MageSkill(48) <= 0 And getMobDistance() <= 19 And getCharSkillPoint(3) >= 43 Then MageSkill(48) = 0 : useAttackMageSkill(zMobID, .Item(48).SubItems(1).Text) : Exit Sub
                'Static Hemispher
                If .Item(43).Checked = True And getCharMP() >= 60 And MageSkill(43) <= 0 And getMobDistance() <= 42 And getCharSkillPoint(3) >= 18 Then MageSkill(43) = 0 : useAttackMageSkill(zMobID, .Item(43).SubItems(1).Text) : Exit Sub
                'Charge
                If .Item(40).Checked = True And getCharMP() >= 20 And MageSkill(40) <= 0 And getMobDistance() <= 19 And getCharSkillPoint(3) >= 3 Then MageSkill(40) = 0 : useAttackMageSkill(zMobID, .Item(40).SubItems(1).Text) : Exit Sub
                'Charged Blade
                If .Item(47).Checked = True And getCharMP() >= 100 And MageSkill(47) <= 0 And getMobDistance() <= 14 And getCharSkillPoint(3) >= 42 Then MageSkill(47) = 0 : useAttackMageSkill(zMobID, .Item(47).SubItems(1).Text) : Exit Sub
                'Light Staff
                If .Item(54).Checked = True And getCharMP() >= 300 And MageSkill(54) <= 0 And getMobDistance() <= 14 And getCharSkillPoint(3) >= 72 Then MageSkill(54) = 0 : useAttackMageSkill(zMobID, .Item(54).SubItems(1).Text) : Exit Sub

                '###########################################################################################################################################################
                '################## Basic Skilleri ################## Basic Skilleri ################## Basic Skilleri ################## Basic Skilleri ###################
                '###########################################################################################################################################################

                'Stroke
                If .Item(1).Checked = True And getCharMP() >= 2 And MageSkill(1) <= 0 And getMobDistance() <= 15 And getCharLevel() >= 0 Then MageSkill(1) = 0 : useAttackMageSkill(zMobID, .Item(1).SubItems(1).Text) : Exit Sub
                'Flash
                If .Item(2).Checked = True And getCharMP() >= 4 And MageSkill(2) <= 0 And getMobDistance() <= 39 And getCharLevel() >= 1 Then MageSkill(2) = 490 : useAttackMageSkill(zMobID, .Item(2).SubItems(1).Text) : Exit Sub
                'Shiver
                If .Item(3).Checked = True And getCharMP() >= 5 And MageSkill(3) <= 0 And getMobDistance() <= 39 And getCharLevel() >= 3 Then MageSkill(3) = 490 : useAttackMageSkill(zMobID, .Item(3).SubItems(1).Text) : Exit Sub
                'Flame
                If .Item(4).Checked = True And getCharMP() >= 5 And MageSkill(4) <= 0 And getMobDistance() <= 39 And getCharLevel() >= 5 Then MageSkill(4) = 490 : useAttackMageSkill(zMobID, .Item(4).SubItems(1).Text) : Exit Sub
                'Cold Wave
                If .Item(5).Checked = True And getCharMP() >= 7 And MageSkill(5) <= 0 And getMobDistance() <= 39 And getCharLevel() >= 7 Then MageSkill(5) = 490 : useAttackMageSkill(zMobID, .Item(5).SubItems(1).Text) : Exit Sub
                'Spark
                If .Item(6).Checked = True And getCharMP() >= 15 And MageSkill(6) <= 0 And getMobDistance() <= 39 And getCharLevel() >= 9 Then MageSkill(6) = 500 : useAttackMageSkill(zMobID, .Item(6).SubItems(1).Text) : Exit Sub
                'Magic Blade
                If .Item(7).Checked = True And getCharMP() >= 10 And MageSkill(7) <= 0 And getMobDistance() <= 14 And getCharLevel() >= 9 Then MageSkill(7) = 0 : useAttackMageSkill(zMobID, .Item(7).SubItems(1).Text) : Exit Sub
            End With
        End If
    End Sub
    Public Sub useAttackMageSkill(ByVal UserID As Long, ByVal skillCode As String)
        Dim SkillID As String
        If UserID <= 0 Then Exit Sub
        SkillID = ADWORD((getCharClass() & skillCode), 8)
        Select Case skillCode
            '____|Basic______________________________|Flame_____________________|İce_______________________________|Lightning | Mage_Skill_1
            Case "002", "003", "005", "007", "009", "509", "539", "551", "554", "603", "609", "618", "639", "651", "703", "709", "718", "739", "715", "727"
                SendPack("3101" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000F00")
                SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & getMobSkillCoor() & "000000000000")

                '|Basic_|Flame________|İce__________|Lightning | Mage Staff
            Case "010", "542", "572", "642", "672", "742", "772"
                SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "01000100000000000000000000000000")

                '|Flame______________________|İce_________|Lightning | Mage_Skill_2
            Case "503", "518", "543", "556", "643", "656", "743", "756", "703", "718", "756"
                SendPack("3101" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000A00")
                SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & getMobSkillCoor() & "000000000000")

                '|Flame______________________|İce________________________|Lightning | Mage_Skill_3
            Case "515", "527", "535", "557", "615", "627", "635", "657", "735", "757", "751", "754"
                SendPack("3101" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000F00")
                SendPack("3102" & SkillID & getCharID() & ADWORD((UserID), 4) & getMobSkillCoor() & "000000000000")
                SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000000")
                SendPack("3104" & SkillID & getCharID() & ADWORD((UserID), 4) & getMobSkillCoor() & "9BFF0000000000000000")

                '|Flame_|İce___|Lightning | Mage_Skill_4
            Case "570", "670", "770"
                SendPack("3101" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000B00")
                SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & getMobSkillCoor() & "000000000000")
                '|Stroke
            Case "001"
                SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "01000100000000000000000000000000")
        End Select
    End Sub
    Public Sub useAreaSkillMage(ByVal UserID As Long, ByVal SkNO As String)
        Dim SkillID As String
        If UserID <= 0 Then Exit Sub
        SkillID = ADWORD((getCharClass() & SkNO), 8)
        Select Case SkNO
            Case "533", "633", "733"
                SendPack("3101" & SkillID & getCharID() & "FFFF" & getMobSkillCoor() & "00000000000000000F00")
                SendPack("3102" & SkillID & getCharID() & "FFFF" & getMobSkillCoor() & "000000000000")
                SendPack("3103" & SkillID & getCharID() & "FFFF" & getMobSkillCoor() & "00000000000000000000")
                SendPack("3104" & SkillID & getCharID() & "FFFF" & getMobSkillCoor() & "9BFF0000000000000000")
            Case "545", "645", "745", "560", "660", "760"
                SendPack("3101" & SkillID & getCharID() & "FFFF" & getMobSkillCoor() & "00000000000000000F00")
                SendPack("3103" & SkillID & getCharID() & "FFFF" & getMobSkillCoor() & "000000000000")
            Case "571", "671", "771"
                SendPack("3101" & SkillID & getCharID() & "FFFF" & getMobSkillCoor() & "00000000000000000D00")
                SendPack("3103" & SkillID & getCharID() & "FFFF" & getMobSkillCoor() & "000000000000")
        End Select
    End Sub
    Public Sub useTimeSkillMage()
        With frm_2Genel.lstv_AttackMage.Items
            If RepairActive = False And checkSkillID("506") = False And checkSkillID("524") = False And checkSkillID("548") = False And checkSkillID("606") = False And checkSkillID("624") = False And checkSkillID("648") = False And checkSkillID("706") = False And checkSkillID("724") = False And checkSkillID("748") = False Then
                'Resist fire
                If .Item(67).Checked = True And getCharMP() >= 15 And getCharSkillPoint(1) >= 6 Then useMageTimeSkill(.Item(67).SubItems(1).Text)
                'Endure fire
                If .Item(68).Checked = True And getCharMP() >= 50 And getCharSkillPoint(1) >= 24 Then useMageTimeSkill(.Item(68).SubItems(1).Text)
                'Immunity fire
                If .Item(69).Checked = True And getCharMP() >= 80 And getCharSkillPoint(1) >= 48 Then useMageTimeSkill(.Item(69).SubItems(1).Text)

                'Resist cold
                If .Item(70).Checked = True And getCharMP() >= 15 And getCharSkillPoint(2) >= 6 Then useMageTimeSkill(.Item(70).SubItems(1).Text)
                'Endure cold
                If .Item(72).Checked = True And getCharMP() >= 50 And getCharSkillPoint(2) >= 24 Then useMageTimeSkill(.Item(72).SubItems(1).Text)
                'Immunity cold
                If .Item(74).Checked = True And getCharMP() >= 80 And getCharSkillPoint(2) >= 48 Then useMageTimeSkill(.Item(74).SubItems(1).Text)

                'Resist lightning
                If .Item(76).Checked = True And getCharMP() >= 15 And getCharSkillPoint(3) >= 6 Then useMageTimeSkill(.Item(76).SubItems(1).Text)
                'Endure lightning
                If .Item(77).Checked = True And getCharMP() >= 50 And getCharSkillPoint(3) >= 24 Then useMageTimeSkill(.Item(77).SubItems(1).Text)
                'Immunity lightning
                If .Item(78).Checked = True And getCharMP() >= 80 And getCharSkillPoint(3) >= 48 Then useMageTimeSkill(.Item(78).SubItems(1).Text)
            End If

            If RepairActive = False And checkSkillID("612") = False And checkSkillID("630") = False And checkSkillID("654") = False Then
                'Frozen Armor
                If .Item(71).Checked = True And getCharMP() >= 40 And getCharSkillPoint(2) >= 12 Then useMageTimeSkill(.Item(71).SubItems(1).Text)
                'Frozen shell
                If .Item(73).Checked = True And getCharMP() >= 80 And getCharSkillPoint(2) >= 30 Then useMageTimeSkill(.Item(73).SubItems(1).Text)
                'Ice barrier
                If .Item(75).Checked = True And getCharMP() >= 120 And getCharSkillPoint(2) >= 54 Then useMageTimeSkill(.Item(75).SubItems(1).Text)
            End If
        End With
    End Sub
    Public Sub useMageTimeSkill(ByVal SkNO As String)
        Dim SkillID As String
        SkillID = ADWORD((getCharClass() & SkNO), 8)
        SendPSec("3101" & SkillID & getCharID() & getCharID() & "00000000000000000000000000000F00", 1000, 45)
        SendPSec("3103" & SkillID & getCharID() & getCharID() & "000000000000000000000000", 1000, 46)
    End Sub
#End Region
#Region "######## Priest Skill Sistemi ########"
    Public Sub priestSkillRow()
        For i As Integer = 0 To 32
            PriestSkill(i) = PriestSkill(i) - 1
        Next
    End Sub
    Public Sub usePriestSkill()
        If (MobDissAttack > 14) Then
            MobDissAttack = 14
        End If
        If getMobDistance() <= MobDissAttack Then
            With frm_2Genel.lstv_AttackPriest.Items
                'R Atak
                If .Item(0).Checked = True And PriestSkill(0) <= 0 And getCharLevel() >= 0 Then PriestSkill(0) = 0 : useAttackR(zMobID, .Item(0).SubItems(1).Text)
                'Malice
                If .Item(22).Checked = True And getCharMP() >= 40 And PriestSkill(22) <= 0 And getCharSkillPoint(3) >= 3 Then PriestSkill(22) = 800 : useAttackPriest(zMobID, .Item(22).SubItems(1).Text)

                'Hellish
                If .Item(6).Checked = True And getCharMP() >= 120 And PriestSkill(6) <= 0 And getCharSkillPoint(1) >= 51 Then PriestSkill(6) = 300 : useAttackPriest(zMobID, .Item(6).SubItems(1).Text) : Exit Sub
                'Collapse
                If .Item(10).Checked = True And getCharMP() >= 120 And PriestSkill(10) <= 0 And getCharSkillPoint(2) >= 51 Then PriestSkill(10) = 300 : useAttackPriest(zMobID, .Item(10).SubItems(1).Text) : Exit Sub
                'Hades
                If .Item(14).Checked = True And getCharMP() >= 120 And PriestSkill(14) <= 0 And getCharSkillPoint(3) >= 51 Then PriestSkill(14) = 300 : useAttackPriest(zMobID, .Item(14).SubItems(1).Text) : Exit Sub
                'Ruin
                If .Item(5).Checked = True And getCharMP() >= 100 And PriestSkill(5) <= 0 And getCharSkillPoint(1) >= 42 Then PriestSkill(5) = 200 : useAttackPriest(zMobID, .Item(5).SubItems(1).Text) : Exit Sub
                'Raving Edge
                If .Item(13).Checked = True And getCharMP() >= 100 And PriestSkill(13) <= 0 And getCharSkillPoint(3) >= 39 Then PriestSkill(13) = 200 : useAttackPriest(zMobID, .Item(13).SubItems(1).Text) : Exit Sub
                'Harsh
                If .Item(9).Checked = True And getCharMP() >= 100 And PriestSkill(9) <= 0 And getCharSkillPoint(2) >= 42 Then PriestSkill(9) = 200 : useAttackPriest(zMobID, .Item(9).SubItems(1).Text) : Exit Sub

                'Helis
                If .Item(16).Checked = True And getCharMP() >= 350 And PriestSkill(16) <= 0 And getCharSkillPoint(4) >= 12 Then PriestSkill(16) = 0 : useAttackPriest(zMobID, .Item(16).SubItems(1).Text) : Exit Sub
                'Judgment
                If .Item(15).Checked = True And getCharMP() >= 200 And PriestSkill(15) <= 0 And getCharSkillPoint(4) >= 2 Then PriestSkill(15) = 0 : useAttackPriest(zMobID, .Item(15).SubItems(1).Text) : Exit Sub

                'Shuddering
                If .Item(4).Checked = True And getCharMP() >= 40 And PriestSkill(4) <= 0 And getCharSkillPoint(1) >= 21 Then PriestSkill(4) = 0 : useAttackPriest(zMobID, .Item(4).SubItems(1).Text) : Exit Sub
                'Wield
                If .Item(8).Checked = True And getCharMP() >= 40 And PriestSkill(8) <= 0 And getCharSkillPoint(2) >= 21 Then PriestSkill(8) = 0 : useAttackPriest(zMobID, .Item(8).SubItems(1).Text) : Exit Sub
                'Bloody
                If .Item(12).Checked = True And getCharMP() >= 40 And PriestSkill(12) <= 0 And getCharSkillPoint(3) >= 21 Then PriestSkill(12) = 0 : useAttackPriest(zMobID, .Item(12).SubItems(1).Text) : Exit Sub
                'Collision
                If .Item(3).Checked = True And getCharMP() >= 30 And PriestSkill(3) <= 0 And getCharSkillPoint(1) >= 12 Then PriestSkill(3) = 0 : useAttackPriest(zMobID, .Item(3).SubItems(1).Text) : Exit Sub
                'Wrath
                If .Item(7).Checked = True And getCharMP() >= 30 And PriestSkill(7) <= 0 And getCharSkillPoint(2) >= 12 Then PriestSkill(7) = 0 : useAttackPriest(zMobID, .Item(7).SubItems(1).Text) : Exit Sub
                'Tilt
                If .Item(11).Checked = True And getCharMP() >= 30 And PriestSkill(11) <= 0 And getCharSkillPoint(3) >= 12 Then PriestSkill(11) = 0 : useAttackPriest(zMobID, .Item(11).SubItems(1).Text) : Exit Sub
                'Holy Attack
                If .Item(2).Checked = True And getCharMP() >= 0 And PriestSkill(2) <= 0 And getCharLevel() >= 1 Then PriestSkill(2) = 0 : useAttackPriest(zMobID, .Item(2).SubItems(1).Text) : Exit Sub
                'Stroke
                If .Item(1).Checked = True And getCharMP() >= 0 And PriestSkill(1) <= 0 And getCharLevel() >= 1 Then PriestSkill(1) = 0 : useAttackPriest(zMobID, .Item(1).SubItems(1).Text) : Exit Sub



                
                'Clear mana
                If .Item(23).Checked = True And getCharMP() >= 80 And PriestSkill(23) <= 0 And getCharSkillPoint(3) >= 9 Then PriestSkill(23) = 800 : useAttackPriest(zMobID, .Item(23).SubItems(1).Text) : Exit Sub
                'Confusion
                If .Item(24).Checked = True And getCharMP() >= 80 And PriestSkill(24) <= 0 And getCharSkillPoint(3) >= 15 Then PriestSkill(24) = 800 : useAttackPriest(zMobID, .Item(24).SubItems(1).Text) : Exit Sub
                'Slow
                If .Item(25).Checked = True And getCharMP() >= 120 And PriestSkill(25) <= 0 And getCharSkillPoint(3) >= 24 Then PriestSkill(25) = 800 : useAttackPriest(zMobID, .Item(25).SubItems(1).Text) : Exit Sub
                'Reverse life
                If .Item(26).Checked = True And getCharMP() >= 50 And PriestSkill(26) <= 0 And getCharSkillPoint(3) >= 27 Then PriestSkill(26) = 1000 : useAttackPriest(zMobID, .Item(26).SubItems(1).Text) : Exit Sub
                'Sleep Wing
                If .Item(27).Checked = True And getCharMP() >= 120 And PriestSkill(27) <= 0 And getCharSkillPoint(3) >= 30 Then PriestSkill(27) = 800 : useAttackPriest(zMobID, .Item(27).SubItems(1).Text) : Exit Sub
                'Sweep mana
                If .Item(28).Checked = True And getCharMP() >= 160 And PriestSkill(28) <= 0 And getCharSkillPoint(3) >= 36 Then PriestSkill(28) = 800 : useAttackPriest(zMobID, .Item(28).SubItems(1).Text) : Exit Sub
                'Parasite
                If .Item(29).Checked = True And getCharMP() >= 100 And PriestSkill(29) <= 0 And getCharSkillPoint(3) >= 45 Then PriestSkill(29) = 800 : useAttackPriest(zMobID, .Item(29).SubItems(1).Text) : Exit Sub
                'Sleep Carpet
                If .Item(30).Checked = True And getCharMP() >= 240 And PriestSkill(30) <= 0 And getCharSkillPoint(3) >= 51 Then PriestSkill(30) = 1000 : useAttackPriest(zMobID, .Item(30).SubItems(1).Text) : Exit Sub
                'Torment
                If .Item(31).Checked = True And getCharMP() >= 150 And PriestSkill(31) <= 0 And getCharSkillPoint(3) >= 57 Then PriestSkill(31) = 1000 : useAttackPriest(zMobID, .Item(31).SubItems(1).Text) : Exit Sub
                'Massive
                If .Item(32).Checked = True And getCharMP() >= 180 And PriestSkill(32) <= 0 And getCharSkillPoint(3) >= 60 Then PriestSkill(32) = 1100 : useAttackPriest(zMobID, .Item(32).SubItems(1).Text) : Exit Sub
            End With
        End If
    End Sub
    Public Sub useTimeSkillPriest()
        With frm_2Genel.lstv_AttackPriest.Items
            'Prayer of god's power
            If RepairActive = False And .Item(18).Checked = True And getCharMP() >= 30 And getCharStat(1) >= 85 Then usePriestBook()
            ' Atak Arttırma
            If RepairActive = False And checkSkillID("004") = False And checkSkillID("529") = False And checkSkillID("629") = False And checkSkillID("729") = False Then
                'Blasting
                If .Item(19).Checked = True And getCharMP() >= 80 And getCharSkillPoint(1) >= 30 Then useBlasting()
                'Wildness
                If .Item(20).Checked = True And getCharMP() >= 80 And getCharSkillPoint(2) >= 30 Then useWildness()
                'Eruption
                If .Item(21).Checked = True And getCharMP() >= 80 And getCharSkillPoint(3) >= 30 Then useEruption()
                'Strength
                If .Item(17).Checked = True And getCharMP() >= 10 And getCharLevel() >= 4 Then usePriestStrength()
            End If
        End With
    End Sub
    Public Sub useAttackPriest(ByVal UserID As Long, ByVal skillCode As String)
        Dim SkillID As String
        SkillID = ADWORD((getCharClass() & skillCode), 8)
        Select Case skillCode
            Case "703", "709", "715", "724", "730", "736", "745", "760"
                SendPack("3101" & SkillID & getCharID() & ADWORD((UserID), 4) & "00000000000000000000000000000F00")
                SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & getMobSkillCoor() & "000000000000")
            Case "751", "757"
                SendPack("3101" & SkillID & getCharID() & "FFFF" & getMobSkillCoor() & "00000000000000000F00")
                SendPack("3103" & SkillID & getCharID() & "FFFF" & getMobSkillCoor() & "000000000000")
            Case Else
                SendPack("3103" & SkillID & getCharID() & ADWORD((UserID), 4) & "01000100000000000000000000000000")
        End Select
    End Sub
    Public Sub usePriestStrength()
        If checkSkillID("004") = False And checkSkillID("529") = False And checkSkillID("629") = False And checkSkillID("729") = False Then
            SendPSec("3101" & ADWORD((getCharClass() & "004"), 8) & getCharID() & getCharID() & "00000000000000000000000000000F00", 1000, 47)
            SendPSec("3103" & ADWORD((getCharClass() & "004"), 8) & getCharID() & getCharID() & "0000000000000000000000000000", 1000, 48)
        End If
    End Sub
    Public Sub useBlasting()
        If checkSkillID("004") = False And checkSkillID("529") = False And checkSkillID("629") = False And checkSkillID("729") = False Then
            SendPSec("3103" & ADWORD((getCharClass() & "529"), 8) & getCharID() & getCharID() & "000000000000000000000000", 1000, 49)
        End If
    End Sub
    Public Sub useWildness()
        If checkSkillID("004") = False And checkSkillID("529") = False And checkSkillID("629") = False And checkSkillID("729") = False Then
            SendPSec("3103" & ADWORD((getCharClass() & "629"), 8) & getCharID() & getCharID() & "000000000000000000000000", 1000, 50)
        End If
    End Sub
    Public Sub useEruption()
        If checkSkillID("004") = False And checkSkillID("529") = False And checkSkillID("629") = False And checkSkillID("729") = False Then
            SendPSec("3103" & ADWORD((getCharClass() & "729"), 8) & getCharID() & getCharID() & "000000000000000000000000", 1000, 51)
        End If
    End Sub
    Public Sub usePriestBook()
        If checkSkillID("026") = False And checkSkillID("030") = False Then
            SendPSec("3103" & ADWORD(("490026"), 8) & getCharID() & getCharID() & "00000000000000000000000000000000", 1000, 52)
        End If
    End Sub
#End Region
#Region "######## Party Skill Sistemi ########"
    Public Sub partySkillRow()
        For i As Integer = 0 To 100
            PartySkill(i) = PartySkill(i) - 1
        Next
    End Sub
    Public Sub PriestPartySkill(ByVal UserID As Long, ByVal SkNO As String)
        Dim PriestPacket, PriestPacketGroup As String
        PriestPacketGroup = ADWORD((getCharClass() & SkNO), 8) & getCharID() & "FFFF" & getCharSkillCoor()
        PriestPacket = ADWORD((getCharClass() & SkNO), 8) & getCharID() & ADWORD((UserID), 4) & "000000000000"
        Select Case SkNO
            '____|Heal Skilleri____________________________|Restore Skilleri_______________________________________|Buff Skilleri_________________________________________________|Deff Skilleri__________________________________________|Resist Skilleri___________|Curse__|Disease___________
            Case "500", "509", "518", "527", "536", "545", "503", "512", "521", "530", "539", "548", "575", "580", "606", "615", "624", "633", "642", "654", "655", "657", "678", "603", "612", "621", "630", "639", "651", "660", "676", "609", "627", "636", "645", "525", "535", "554", "004"
                SendPriestSec(PriestPacket, 200, CInt(SkNO))
            Case "656", "570", "670", "557", "560" 'Group Buff
                SendPriestSec(PriestPacketGroup, 200, CInt(SkNO))
        End Select
    End Sub
    Public Sub partyMinor(ByVal PtMinor As Boolean, ByVal mRow As Integer)
        Dim i As Integer
        If PtMinor = False Then Exit Sub
        For i = 1 To getPartyCount()
            If (PartyUserMaxHP(i) - PartyUserHP(i)) >= 60 Then
                useMinor(PartyUserID(i), mRow)
            End If
        Next
    End Sub
    Public Sub partyBuffControl(ByVal BuffRow As Integer, ByVal AccRow As Integer, ByVal ResisRow As Integer, ByVal StrBool As Boolean)
        Dim i As Integer, j As Integer, SkillCode As String, SkillMana As Integer
        Select Case BuffRow
            Case 0
                SkillCode = "606" : SkillMana = 15
            Case 1
                SkillCode = "615" : SkillMana = 30
            Case 2
                SkillCode = "624" : SkillMana = 60
            Case 3
                SkillCode = "633" : SkillMana = 120
            Case 4
                SkillCode = "642" : SkillMana = 240
            Case 5
                SkillCode = "654" : SkillMana = 240
            Case 6
                SkillCode = "655" : SkillMana = 300
            Case 7
                SkillCode = "656" : SkillMana = 570
            Case 8
                SkillCode = "657" : SkillMana = 360
            Case 9
                SkillCode = "670" : SkillMana = 460
            Case 10
                SkillCode = "678" : SkillMana = 690
            Case 11
                SkillCode = "oto" : SkillMana = 10
            Case Else
                SkillCode = "999" : SkillMana = 10
        End Select
        For i = 1 To getPartyCount()
            If PartyUserUzaklık(i) < 55 And (PartyBuffHp(i) <> PartyUserMaxHP(i)) And (PartyUserHP(i) > 0) And getCharMP() >= SkillMana And PartyBuffSend(i) = False Then
                If SkillCode = "999" Then
                    'partyUseBuff(PartyUserTargetID(i), SkillCode, AccRow, ResisRow, StrBool)
                    PartyBuffString(i) = SkillCode
                    PartyBuffSend(i) = True
                    Exit For
                End If
                If SkillCode <> "oto" Then
                    If getCharSkillPoint(2) >= CInt(SkillCode.Substring(1, 2)) Then
                        If (SkillCode = "656") Or (SkillCode = "670") Then 'Group Buff
                            'partyUseBuff(PartyUserTargetID(i), SkillCode, AccRow, ResisRow, StrBool)
                            PartyBuffString(i) = SkillCode
                            For j = 1 To 8
                                PartyBuffSend(j) = True
                            Next
                        Else
                            'partyUseBuff(PartyUserTargetID(i), SkillCode, AccRow, ResisRow, StrBool)
                            PartyBuffString(i) = SkillCode
                            PartyBuffSend(i) = True
                            Exit For
                        End If
                    End If
                Else 'Oto Buff
                    '//Superioris
                    If getCharSkillPoint(2) >= 78 And getCharMP() >= 690 Then
                        If (getCharUndyHp(PartyUserMaxHP(i)) >= 2500) Then
                            'partyUseBuff(PartyUserTargetID(i), "654", AccRow, ResisRow, StrBool)
                            PartyBuffString(i) = "654"
                        Else
                            'partyUseBuff(PartyUserTargetID(i), "678", AccRow, ResisRow, StrBool)
                            PartyBuffString(i) = "678"
                        End If
                        PartyBuffSend(i) = True
                        Exit For
                    End If

                    '//Massiveness
                    If getCharSkillPoint(2) >= 57 And getCharMP() >= 360 Then
                        If (getCharUndyHp(PartyUserMaxHP(i)) >= 1500) Then
                            'partyUseBuff(PartyUserTargetID(i), "654", AccRow, ResisRow, StrBool)
                            PartyBuffString(i) = "654"
                        Else
                            'partyUseBuff(PartyUserTargetID(i), "657", AccRow, ResisRow, StrBool)
                            PartyBuffString(i) = "657"
                        End If
                        PartyBuffSend(i) = True
                        Exit For
                    End If

                    '//Heapness
                    If getCharSkillPoint(2) >= 54 And getCharMP() >= 300 Then
                        If (getCharUndyHp(PartyUserMaxHP(i)) >= 1200) Then
                            'partyUseBuff(PartyUserTargetID(i), "654", AccRow, ResisRow, StrBool)
                            PartyBuffString(i) = "654"
                        Else
                            'partyUseBuff(PartyUserTargetID(i), "655", AccRow, ResisRow, StrBool)
                            PartyBuffString(i) = "655"
                        End If
                        PartyBuffSend(i) = True
                        Exit For
                    End If
                    '//Mightness
                    If getCharSkillPoint(2) >= 42 And getCharMP() >= 240 Then
                        'partyUseBuff(PartyUserTargetID(i), "642", AccRow, ResisRow, StrBool)
                        PartyBuffString(i) = "642"
                        PartyBuffSend(i) = True
                        Exit For
                    End If
                    '//Hardness
                    If getCharSkillPoint(2) >= 33 And getCharMP() >= 120 Then
                        'partyUseBuff(PartyUserTargetID(i), "633", AccRow, ResisRow, StrBool)
                        PartyBuffString(i) = "633"
                        PartyBuffSend(i) = True
                        Exit For
                    End If
                    '//Strong
                    If getCharSkillPoint(2) >= 24 And getCharMP() >= 60 Then
                        'partyUseBuff(PartyUserTargetID(i), "624", AccRow, ResisRow, StrBool)
                        PartyBuffString(i) = "624"
                        PartyBuffSend(i) = True
                        Exit For
                    End If
                    '//Brave
                    If getCharSkillPoint(2) >= 15 And getCharMP() >= 30 Then
                        'partyUseBuff(PartyUserTargetID(i), "615", AccRow, ResisRow, StrBool)
                        PartyBuffString(i) = "615"
                        PartyBuffSend(i) = True
                        Exit For
                    End If
                    '//Grace
                    If getCharSkillPoint(2) >= 6 And getCharMP() >= 15 Then
                        'partyUseBuff(PartyUserTargetID(i), "606", AccRow, ResisRow, StrBool)
                        PartyBuffString(i) = "606"
                        PartyBuffSend(i) = True
                        Exit For
                    End If
                End If
            End If
        Next
        For i = 1 To getPartyCount()
            If (PartyBuffSend(i) = True And GetTickCount() > PartyBuffHpTime) Then
                partyUseBuff(PartyUserTargetID(i), PartyBuffString(i), AccRow, ResisRow, StrBool)
                PartyBuffHp(i) = PartyUserMaxHP(i)
                PartyBuffSend(i) = False
                PartyBuffHpTime = GetTickCount() + 200
            End If
        Next
    End Sub
    Public Sub partyUseAcc(ByVal PartyAccID As String, ByVal AccRow As Integer)
        Dim SkillCode As String, SkillMana As Integer
        Select Case AccRow
            Case 0
                SkillCode = "603" : SkillMana = 10
            Case 1
                SkillCode = "612" : SkillMana = 20
            Case 2
                SkillCode = "621" : SkillMana = 40
            Case 3
                SkillCode = "630" : SkillMana = 80
            Case 4
                SkillCode = "639" : SkillMana = 80
            Case 5
                SkillCode = "651" : SkillMana = 100
            Case 6
                SkillCode = "660" : SkillMana = 150
            Case 7
                SkillCode = "676" : SkillMana = 300
            Case 8
                SkillCode = "oto" : SkillMana = 10
            Case Else
                Exit Sub
        End Select
        If SkillCode <> "oto" Then
            If getCharSkillPoint(2) >= CInt(SkillCode.Substring(1, 2)) And getCharMP() >= SkillMana Then
                PriestPartySkill(PartyAccID, SkillCode)
            End If
        Else
            '//Guard
            If getCharSkillPoint(2) >= 76 And getCharMP() >= 300 Then
                PriestPartySkill(PartyAccID, "676")
                Exit Sub
            End If
            '//Peel
            If getCharSkillPoint(2) >= 60 And getCharMP() >= 150 Then
                PriestPartySkill(PartyAccID, "660")
                Exit Sub
            End If
            '//Protector
            If getCharSkillPoint(2) >= 51 And getCharMP() >= 100 Then
                PriestPartySkill(PartyAccID, "651")
                Exit Sub
            End If
            '//Barrier
            If getCharSkillPoint(2) >= 39 And getCharMP() >= 80 Then
                PriestPartySkill(PartyAccID, "639")
                Exit Sub
            End If
            '//Shield
            If getCharSkillPoint(2) >= 30 And getCharMP() >= 80 Then
                PriestPartySkill(PartyAccID, "630")
                Exit Sub
            End If
            '//Armor
            If getCharSkillPoint(2) >= 21 And getCharMP() >= 40 Then
                PriestPartySkill(PartyAccID, "621")
                Exit Sub
            End If
            '//Shell
            If getCharSkillPoint(2) >= 12 And getCharMP() >= 20 Then
                PriestPartySkill(PartyAccID, "612")
                Exit Sub
            End If
            '//Skin
            If getCharSkillPoint(2) >= 3 And getCharMP() >= 10 Then
                PriestPartySkill(PartyAccID, "603")
                Exit Sub
            End If

        End If
    End Sub
    Public Sub partyUseResist(ByVal PartyResistID As String, ByVal ResistRow As Integer)
        Dim SkillCode As String, SkillMana As Integer
        Select Case ResistRow
            Case 0
                SkillCode = "609" : SkillMana = 15
            Case 1
                SkillCode = "627" : SkillMana = 30
            Case 2
                SkillCode = "636" : SkillMana = 45
            Case 3
                SkillCode = "645" : SkillMana = 60
            Case 4
                SkillCode = "oto" : SkillMana = 15
            Case Else
                Exit Sub
        End Select
        If SkillCode <> "oto" Then
            If getCharSkillPoint(2) >= CInt(SkillCode.Substring(1, 2)) And getCharMP() >= SkillMana Then
                PriestPartySkill(PartyResistID, SkillCode)
            End If
        Else
            '//Fresh
            If getCharSkillPoint(2) >= 45 And getCharMP() >= 60 Then
                PriestPartySkill(PartyResistID, "645")
                Exit Sub
            End If
            '//Calm
            If getCharSkillPoint(2) >= 36 And getCharMP() >= 40 Then
                PriestPartySkill(PartyResistID, "636")
                Exit Sub
            End If
            '//Bright
            If getCharSkillPoint(2) >= 27 And getCharMP() >= 30 Then
                PriestPartySkill(PartyResistID, "627")
                Exit Sub
            End If
            '//Resist
            If getCharSkillPoint(2) >= 9 And getCharMP() >= 15 Then
                PriestPartySkill(PartyResistID, "609")
                Exit Sub
            End If
        End If
    End Sub
    Public Sub partyUseBuff(ByVal PartyID As String, ByVal PartyBuffCode As String, ByVal AccRow As Integer, ByVal ResisRow As Integer, ByVal StrBool As Boolean)
        If PartyBuffCode <> "999" Then
            PriestPartySkill(PartyID, PartyBuffCode)
        End If
        partyUseAcc(PartyID, AccRow)
        partyUseResist(PartyID, ResisRow)
        partyStr(PartyID, StrBool)
    End Sub
    Public Sub partyHealControl(ByVal healRow As Integer, ByVal healPoint As Integer)
        Dim i As Integer, SkillCode As String, SkillMana As Integer
        Select Case healRow
            Case 0
                SkillCode = "500" : SkillMana = 10
            Case 1
                SkillCode = "509" : SkillMana = 20
            Case 2
                SkillCode = "518" : SkillMana = 40
            Case 3
                SkillCode = "527" : SkillMana = 80
            Case 4
                SkillCode = "536" : SkillMana = 160
            Case 5
                SkillCode = "545" : SkillMana = 320
            Case 6
                SkillCode = "oto" : SkillMana = 10
            Case Else
                Exit Sub
        End Select
        For i = 1 To getPartyCount()
            If SkillCode <> "oto" And SkillCode <> "otomax" Then
                If getCharSkillPoint(1) >= CInt(SkillCode.Substring(1, 2)) And getCharMP() >= SkillMana And (PartyUserHP(i) > 0) And (PartyUserHP(i) <= ((PartyUserMaxHP(i) * healPoint) / 100)) And PartyUserUzaklık(i) < 55 Then
                    PriestPartySkill(PartyUserTargetID(i), SkillCode)
                    Exit Sub
                End If
            ElseIf (SkillCode = "oto") Then
                If (PartyUserUzaklık(i) < 55) And (PartyUserHP(i) > 0) And getCharMP() >= 320 And (PartyUserHPFark(i) >= 1920) And getCharSkillPoint(1) >= 45 Then PriestPartySkill(PartyUserTargetID(i), "545") : Exit Sub
                If (PartyUserUzaklık(i) < 55) And (PartyUserHP(i) > 0) And getCharMP() >= 160 And (PartyUserHPFark(i) >= 960) And getCharSkillPoint(1) >= 36 Then PriestPartySkill(PartyUserTargetID(i), "536") : Exit Sub
                If (PartyUserUzaklık(i) < 55) And (PartyUserHP(i) > 0) And getCharMP() >= 80 And (PartyUserHPFark(i) >= 720) And getCharSkillPoint(1) >= 27 Then PriestPartySkill(PartyUserTargetID(i), "527") : Exit Sub
                If (PartyUserUzaklık(i) < 55) And (PartyUserHP(i) > 0) And getCharMP() >= 40 And (PartyUserHPFark(i) >= 360) And getCharSkillPoint(1) >= 18 Then PriestPartySkill(PartyUserTargetID(i), "518") : Exit Sub
                If (PartyUserUzaklık(i) < 55) And (PartyUserHP(i) > 0) And getCharMP() >= 20 And (PartyUserHPFark(i) >= 240) And getCharSkillPoint(1) >= 9 Then PriestPartySkill(PartyUserTargetID(i), "509") : Exit Sub
                If (PartyUserUzaklık(i) < 55) And (PartyUserHP(i) > 0) And getCharMP() >= 10 And (PartyUserHPFark(i) >= 60) And getCharSkillPoint(1) >= 0 And getCharLevel() >= 10 Then PriestPartySkill(PartyUserTargetID(i), "500") : Exit Sub
            End If
        Next
    End Sub
    Public Sub partyRestore(ByVal RestRow As Integer)
        Dim i As Integer
        Dim SkillCode As String, SkillMana As Integer
        Select Case RestRow
            Case 0
                SkillCode = "503" : SkillMana = 25
            Case 1
                SkillCode = "512" : SkillMana = 30
            Case 2
                SkillCode = "521" : SkillMana = 100
            Case 3
                SkillCode = "530" : SkillMana = 200
            Case 4
                SkillCode = "539" : SkillMana = 375
            Case 5
                SkillCode = "548" : SkillMana = 348
            Case 6
                SkillCode = "570" : SkillMana = 1000
            Case 7
                SkillCode = "575" : SkillMana = 1200
            Case 8
                SkillCode = "580" : SkillMana = 1400
            Case 9
                SkillCode = "oto" : SkillMana = 25
            Case Else
                Exit Sub
        End Select

        For i = 1 To getPartyCount()
            If SkillCode <> "oto" Then
                If PartyUserUzaklık(i) < 55 And getCharSkillPoint(1) >= CInt(SkillCode.Substring(1, 2)) And getCharMP() >= SkillMana And PartyUserMaxHP(i) > PartyRestHp(i) Then
                    PriestPartySkill(PartyUserTargetID(i), SkillCode)
                    PartyRestHp(i) = PartyUserHP(i)
                    Exit Sub
                End If

            ElseIf PartyUserUzaklık(i) < 55 And PartyUserHP(i) <> PartyRestHp(i) Then
                '//Past
                If getCharSkillPoint(1) >= 80 And getCharMP() >= 1400 Then
                    PriestPartySkill(PartyUserTargetID(i), "580")
                    PartyRestHp(i) = PartyUserHP(i)
                    Exit Sub
                End If
                '//Past Rec
                If getCharSkillPoint(1) >= 75 And getCharMP() >= 1200 Then
                    PriestPartySkill(PartyUserTargetID(i), "575")
                    PartyRestHp(i) = PartyUserHP(i)
                    Exit Sub
                End If
                '//Critical
                If getCharSkillPoint(1) >= 70 And getCharMP() >= 1000 Then
                    PriestPartySkill(PartyUserTargetID(i), "570")
                    PartyRestHp(i) = PartyUserHP(i)
                    Exit Sub
                End If
                '//Superior
                If getCharSkillPoint(1) >= 48 And getCharMP() >= 348 Then
                    PriestPartySkill(PartyUserTargetID(i), "548")
                    PartyRestHp(i) = PartyUserHP(i)
                    Exit Sub
                End If
                '//Massive
                If getCharSkillPoint(1) >= 39 And getCharMP() >= 375 Then
                    PriestPartySkill(PartyUserTargetID(i), "539")
                    PartyRestHp(i) = PartyUserHP(i)
                    Exit Sub
                End If
                '//Great
                If getCharSkillPoint(1) >= 31 And getCharMP() >= 200 Then
                    PriestPartySkill(PartyUserTargetID(i), "531")
                    PartyRestHp(i) = PartyUserHP(i)
                    Exit Sub
                End If
                '//Major
                If getCharSkillPoint(1) >= 22 And getCharMP() >= 100 Then
                    PriestPartySkill(PartyUserTargetID(i), "522")
                    PartyRestHp(i) = PartyUserHP(i)
                    Exit Sub
                End If
                '//Restore
                If getCharSkillPoint(1) >= 13 And getCharMP() >= 30 Then
                    PriestPartySkill(PartyUserTargetID(i), "513")
                    PartyRestHp(i) = PartyUserHP(i)
                    Exit Sub
                End If
                '//Light
                If getCharSkillPoint(1) >= 3 And getCharMP() >= 25 Then
                    PriestPartySkill(PartyUserTargetID(i), "503")
                    PartyRestHp(i) = PartyUserHP(i)
                    Exit Sub
                End If
            End If
        Next
    End Sub
    Public Sub partyStr(ByVal StrID As Long, ByVal StrStatu As Boolean)
        If StrStatu = False Then Exit Sub
        If getCharLevel() >= 4 And getCharMP() >= 10 Then
            PriestPartySkill(StrID, "004")
            Exit Sub
        End If
    End Sub
    Public Sub partyGroupHeal(ByVal pointGroup As Integer, ByVal GroupPerson As Integer, ByVal GroupBool As Boolean)
        If GroupBool = False Then Exit Sub
        If partyUserPoint(pointGroup) >= GroupPerson And getCharMP() >= 960 And PartySkill(2) <= 0 And getCharSkillPoint(1) >= 57 Then
            PriestPartySkill(0, "557")
            PartySkill(2) = 900
            Exit Sub
        End If

        If partyUserPoint(pointGroup) >= GroupPerson And getCharMP() >= 1920 And PartySkill(3) <= 0 And PartySkill(2) > 100 And getCharSkillPoint(1) >= 60 Then
            PriestPartySkill(0, "560")
            PartySkill(3) = 900
            Exit Sub
        End If
    End Sub
    Public Function partyUserPoint(ByVal pointGroup As Integer) As Integer
        Dim i As Integer, result As Integer
        result = 0
        For i = 1 To getPartyCount()
            If PartyUserHP(i) <= ((PartyUserMaxHP(i) * pointGroup) / 100) Then
                result = result + 1
            End If
        Next
        Return result
    End Function
#End Region
#Region "######## Ek İşlemler ########"
    Public Sub PetStatu(ByVal statu As Boolean)
        If (statu And checkSkillID(117) = False) Then
            SendPack("3101" & FormatHex(Hex(500117), 8) & getCharID() & getCharID() & "00000000000000000000000000006400")
            Sleep(7000)
            SendPack("3103" & FormatHex(Hex(500117), 8) & getCharID() & getCharID() & "000000000000000000000000")
            Sleep(2000)
        ElseIf (checkSkillID(117) And statu = False) Then
            SendPack("3106" & FormatHex(Hex(500117), 8) & getCharID() & getCharID() & "000000000000000000000000")
        End If
        If (checkSkillID(117)) Then
            SendPack("76010508")
        End If
    End Sub
    Public Sub exchangeOre()
        If getItemSlotInv("399210000") > 0 Then
            SendPack("2001" + findIDtoNPC("[Miner]") + "FFFFFFFF")
            SendPack("6407544E0000")
            SendPack("55001033313531315F5069746D616E2E6C7561")
            SendPack("6A02")
        End If
        If getItemSlotInv("399200000") > 0 Then
            SendPack("2001" + findIDtoNPC("[Miner]") + "FFFFFFFF")
            SendPack("6407544E0000")
            SendPack("55011033313531315F5069746D616E2E6C7561")
            SendPack("55001033313531315F5069746D616E2E6C7561")
            SendPack("6A02")
        End If
    End Sub
    Public Sub summonUser(ByVal SumChrID As String)
        If getCharLevel() < 10 Then Exit Sub
        Dim SkillID As String
        SkillID = FormatHex(Hex((getCharClass() & "004")), 6)
        SendPSec("3101" & SkillID & "00" & getCharID() & SumChrID & "00000000000000000000000000000F00", 500, 53)
        SendPSec("3103" & SkillID & "00" & getCharID() & SumChrID & getCharSkillCoor() & "000000000000", 500, 54)
    End Sub
    Public Sub writeChat(ByVal chatText As String, ByVal chatStatu As Integer)
        Dim ChatTur As Integer
        Select Case chatStatu
            Case 0
                ChatTur = 1
            Case 1
                ChatTur = 5
            Case 2
                ChatTur = 3
            Case Else
                ChatTur = 22
        End Select
        SendPSec("10" & ADWORD(ChatTur, 2) & ADWORD(chatText.Length, 4) & StringToHex(chatText), 500, 55)
    End Sub
    Public Sub itemRepair(ByVal Helmet As Boolean, ByVal Pauldron As Boolean, ByVal Weapon As Boolean, ByVal Pads As Boolean, ByVal Boots As Boolean, ByVal Gauntlet As Boolean)
        If Helmet = True Then
            setRepair(2)
        End If
        If Pauldron = True Then
            setRepair(5)
        End If
        If Weapon = True Then
            setRepair(7)
            setRepair(9)
        End If
        If Pads = True Then
            setRepair(11)
        End If
        If Boots = True Then
            setRepair(13)
        End If
        If Gauntlet = True Then
            setRepair(14)
        End If
    End Sub
    Public Sub setRepair(ByVal itemSlot As Integer)
        If getItemID(itemSlot) > 0 Then
            SendPack("3B01" & ADWORD(itemSlot - 1, 2) & findIDtoNPC("[Sundries]") & ADWORD(getItemID(itemSlot)))
            Sleep(250)
        End If
    End Sub
    Public Sub sundriesNPC(ByVal Arrow As Boolean, ByVal ArrowAdet As Integer,
                           ByVal Wolf As Boolean, ByVal WolfAdet As Integer,
                           ByVal Kitap As Boolean, ByVal KitapAdet As Integer,
                           ByVal TSGem As Boolean, ByVal TSGemAdet As Integer)
        If Arrow = True And getCharClassName() = "Rogue" Then
            takeSundries(391010000, ArrowAdet, 0, 0)
        End If
        If Wolf = True And getCharClassName() = "Rogue" Then
            takeSundries(370004000, WolfAdet, 0, 7)
        End If
        If Kitap = True And getCharClassName() = "Priest" Then
            takeSundries(389026000, KitapAdet, 0, 11)
        End If
        If TSGem = True Then
            takeSundries(379091000, TSGemAdet, 1, 2)
        End If
    End Sub
    Public Sub takeSundries(ByVal SundItemID As Integer, ByVal SundItemAdet As Integer, ByVal SundSayfa As Integer, ByVal SundSlot As Integer)
        Dim ItemSlot As Integer, ItemAdet As Integer
        If getItemIDCountInv(SundItemID) >= SundItemAdet Or getFirstEmptySlotInv() <= -1 Then Exit Sub
        If getItemIDCountInv(SundItemID) <= 0 Then
            ItemSlot = getFirstEmptySlotInv()
            ItemAdet = SundItemAdet
        Else
            ItemSlot = findItemSlot(SundItemID)
            ItemAdet = SundItemAdet - getItemCountSlot(ItemSlot + KO_OFF_INVROW)
        End If
        SendPack("2001" & findIDtoNPC("[Sundries]") & "FFFFFFFF")
        SendPack("210118E40300" & findIDtoNPC("[Sundries]") & "01" & ADWORD(SundItemID) & ADWORD(ItemSlot, 2) & ADWORD(ItemAdet, 4) & ADWORD(SundSayfa, 2) & ADWORD(SundSlot, 2))
        SendPack("6A02")
        Sleep(250)
    End Sub
    Public Sub potionNPC(ByVal HPPotionRow As Integer, ByVal HPPotionAdet As Integer, ByVal MPPotionRow As Integer, ByVal MPPotionAdet As Integer, ByVal HPPotionBool As Integer, ByVal MPPotionBool As Integer)
        Dim MPPotID As Integer, MPPotSundSlot As Integer, HPPotID As Integer, HPPotSundSlot As Integer
        If HPPotionBool = True Then
            Select Case HPPotionRow
                Case 0
                    HPPotID = 389010000 : HPPotSundSlot = 0
                Case 1
                    HPPotID = 389011000 : HPPotSundSlot = 1
                Case 2
                    HPPotID = 389012000 : HPPotSundSlot = 2
                Case 3
                    HPPotID = 389013000 : HPPotSundSlot = 3
                Case 4
                    HPPotID = 389014000 : HPPotSundSlot = 4
            End Select
            takePotion(HPPotID, HPPotionAdet, HPPotSundSlot)
        End If
        If MPPotionBool = True Then
            Select Case MPPotionRow
                Case 0
                    MPPotID = 389016000 : MPPotSundSlot = 6
                Case 1
                    MPPotID = 389017000 : MPPotSundSlot = 7
                Case 2
                    MPPotID = 389018000 : MPPotSundSlot = 8
                Case 3
                    MPPotID = 389019000 : MPPotSundSlot = 9
                Case 4
                    MPPotID = 389020000 : MPPotSundSlot = 10
            End Select
            takePotion(MPPotID, MPPotionAdet, MPPotSundSlot)
        End If
    End Sub

    Public Function checkHpPotion() As Boolean
        If (getItemIDCountInv(389010000) > 2) Or
                (getItemIDCountInv(389011000) > 2) Or
                (getItemIDCountInv(900770000) > 2) Or
                (getItemIDCountInv(389012000) > 2) Or
                (getItemIDCountInv(900780000) > 2) Or
                (getItemIDCountInv(389013000) > 2) Or
                (getItemIDCountInv(900790000) > 2) Or
                (getItemIDCountInv(389014000) > 2) Or
                (getItemIDCountInv(399284000) > 2) Or
                (getItemIDCountInv(900817000) > 2) Or
                (getItemIDCountInv(389390000) > 2) Then
            Return True
        End If
        Return False
    End Function
    Public Function checkMpPotion() As Boolean
        If (getItemIDCountInv(389016000) > 2) Or
            (getItemIDCountInv(389017000) > 2) Or
            (getItemIDCountInv(900800000) > 2) Or
            (getItemIDCountInv(389018000) > 2) Or
            (getItemIDCountInv(900810000) > 2) Or
            (getItemIDCountInv(389019000) > 2) Or
            (getItemIDCountInv(900820000) > 2) Or
            (getItemIDCountInv(389020000) > 2) Or
            (getItemIDCountInv(399285000) > 2) Or
            (getItemIDCountInv(900818000) > 2) Or
            (getItemIDCountInv(389400000) > 2) Then
            Return True
        End If
        Return False
    End Function
    Public Sub takePotion(ByVal PotItemID As Integer, ByVal PotItemAdet As Integer, ByVal PotSlot As Integer)
        Dim ItemSlot As Integer, ItemAdet As Integer
        If getItemIDCountInv(PotItemID) >= PotItemAdet Or getFirstEmptySlotInv() <= -1 Then Exit Sub
        If getItemIDCountInv(PotItemID) <= 0 Then
            ItemSlot = getFirstEmptySlotInv()
            ItemAdet = PotItemAdet
        Else
            ItemSlot = findItemSlot(PotItemID)
            ItemAdet = PotItemAdet - getItemCountSlot(ItemSlot + KO_OFF_INVROW)
        End If
        SendPack("2001" & findIDtoNPC("Potion") & "FFFFFFFF")
        SendPack("210148DC0300" & findIDtoNPC("Potion") & "01" & ADWORD(PotItemID) & ADWORD(ItemSlot, 2) & ADWORD(ItemAdet, 4) & "00" & ADWORD(PotSlot, 2))
        SendPack("6A02")
        Sleep(250)
    End Sub
    Public Sub potionDcNPC(ByVal HPPotionAdet As Integer, ByVal MPPotionAdet As Integer, ByVal HPPotionBool As Integer, ByVal MPPotionBool As Integer)
        If HPPotionBool = True Then
            takeDCPotion(399284000, HPPotionAdet, 0)
        End If
        If MPPotionBool = True Then
            takeDCPotion(399285000, MPPotionAdet, 1)
        End If
    End Sub
    Public Function checkDcHpPotion() As Boolean
        If (getItemIDCountInv(399284000) > 2) Then
            Return True
        End If
        Return False
    End Function
    Public Function checkDcMpPotion() As Boolean
        If (getItemIDCountInv(399285000) > 2) Then
            Return True
        End If
        Return False
    End Function
    Public Sub takeDCPotion(ByVal PotItemID As Integer, ByVal PotItemAdet As Integer, ByVal PotSlot As Integer)
        Dim ItemSlot As Integer, ItemAdet As Integer
        If getItemIDCountInv(PotItemID) >= PotItemAdet Or getFirstEmptySlotInv() <= -1 Then Exit Sub
        If getItemIDCountInv(PotItemID) <= 0 Then
            ItemSlot = getFirstEmptySlotInv()
            ItemAdet = PotItemAdet
        Else
            ItemSlot = findItemSlot(PotItemID)
            ItemAdet = PotItemAdet - getItemCountSlot(ItemSlot + KO_OFF_INVROW)
        End If
        SendPack("2001" & findIDtoNPC("DC Sundries") & "FFFFFFFF")
        SendPack("2101F8120400" & findIDtoNPC("DC Sundries") & "01" & ADWORD(PotItemID) & ADWORD(ItemSlot, 2) & ADWORD(ItemAdet, 4) & "00" & ADWORD(PotSlot, 2))
        SendPack("6A02")
        Sleep(250)
    End Sub
    Public Sub pmUser(ByVal PmNick As String, ByVal PmText As String)
        SendPack("3501" & FormatHex(Hex(Len(PmNick)), 2) & "00" & StringToHex(PmNick))
        SendPack("1002" & FormatHex(Hex(Len(PmText)), 2) & "00" & StringToHex(PmText))
    End Sub
    Public Sub TakeQuest(ByVal questCode As Integer, ByVal questDelete As Boolean)
        If (questDelete) Then
            SendPack("6405" & FormatHex(Hex(questCode), 8))
        Else
            SendPack("6406" & FormatHex(Hex(questCode), 8))
        End If
    End Sub
    Public Sub questTake(ByVal NPCName As String, ByVal GorevIslem As Integer)
        Dim GorevTuru As String, i As Integer
        If GorevIslem = 1 Then
            GorevTuru = "6406"
        Else
            GorevTuru = "6405"
        End If

        ' ### Potrang ###
        If NPCName = "Potrang" Then
            For i = 0 To 4
                Select Case Potrang(i)
                    Case 7368
                        SendPack(GorevTuru & FormatHex(Hex(Potrang(i)), 8))
                        SendPack("2001" & findIDtoNPC("Potrang") & "FFFFFFFF")
                        SendPack("55001232353030305F70686F7472616E672E6C7561")
                        SendPack("55001232353030305F70686F7472616E672E6C7561")
                        SendPack("6404" & FormatHex(Hex(Potrang(i)), 8))
                        SendPack("55001232353030305F70686F7472616E672E6C7561")
                    Case 7330
                        SendPack("6407" & FormatHex(Hex("7333"), 8))
                        SendPack("55001232353030305F70686F7472616E672E6C7561")
                        SendPack("55001232353030305F70686F7472616E672E6C7561FF")
                    Case Else
                        SendPack(GorevTuru & FormatHex(Hex(Potrang(i)), 8))
                End Select
            Next
        End If

        ' ### Sentinel ###
        If NPCName = "Sentinel Patrick" Then
            For i = 0 To 16
                SendPack(GorevTuru & FormatHex(Hex(Sentinel_Patrick(i)), 8))
            Next
            If getCharClassName() = "Mage" Then SendPack(GorevTuru & FormatHex(Hex(Sentinel_Patrick(17)), 8))
            If getCharClassName() = "Priest" Then SendPack(GorevTuru & FormatHex(Hex(Sentinel_Patrick(18)), 8))
            If getCharClassName() = "Warrior" Then SendPack(GorevTuru & FormatHex(Hex(Sentinel_Patrick(19)), 8))
        End If

        ' ### Daughter ###
        If NPCName = "GM Daughter" Then
            For i = 0 To 4
                SendPack(GorevTuru & FormatHex(Hex(GM_Daughter(i)), 8))
            Next
        End If

        ' ### Trust ###
        If NPCName = "Trust - 10 Lw" Then
            For i = 0 To 2
                SendPack(GorevTuru & FormatHex(Hex(TrustMiss(i)), 8))
            Next
        End If

        ' ### Heppa ###
        If NPCName = "Blacksmith Heppa" Then
            For i = 0 To 3
                SendPack(GorevTuru & FormatHex(Hex(BlackSmithHeppa(i)), 8))
            Next
        End If

        ' ### Jed ###
        If NPCName = "Jed" Then
            For i = 0 To 7
                SendPack(GorevTuru & FormatHex(Hex(Jed(i)), 8))
            Next
        End If

        ' ### Osmand ###
        If NPCName = "Osmand" Then
            For i = 0 To 4
                SendPack(GorevTuru & FormatHex(Hex(Osmand(i)), 8))
            Next
        End If

        ' ### Berret ###
        If NPCName = "Berret" Then
            For i = 0 To 4
                SendPack(GorevTuru & FormatHex(Hex(Berret(i)), 8))
            Next
        End If

        ' ### Lazer ###
        If NPCName = "Lazer" Then

            For i = 0 To 1
                SendPack(GorevTuru & FormatHex(Hex(Lazer(i)), 8))
            Next

            If getCharClassName() = "Rogue" Then
                For i = 2 To 11
                    SendPack(GorevTuru & FormatHex(Hex(Lazer(i)), 8))
                Next
            End If

            If getCharClassName() = "Mage" Then
                For i = 12 To 21
                    SendPack(GorevTuru & FormatHex(Hex(Lazer(i)), 8))
                Next
            End If

            If getCharClassName() = "Priest" Then
                For i = 22 To 31
                    SendPack(GorevTuru & FormatHex(Hex(Lazer(i)), 8))
                Next
            End If

            If getCharClassName() = "Warrior" Then
                For i = 32 To 41
                    SendPack(GorevTuru & FormatHex(Hex(Lazer(i)), 8))
                Next
            End If
        End If

        ' ### Nameless Warrior ###
        If NPCName = "Nameless Warrior" Then
            For i = 0 To 1
                SendPack(GorevTuru & FormatHex(Hex(NameLessWarrior(i)), 8))
            Next
        End If

        ' ### Lüferson İtem Görevleri ###
        If NPCName = "Lüferson İtem Görevleri" Then
            For i = 0 To 1
                SendPack(GorevTuru & FormatHex(Hex(Shymer(i)), 8))
            Next
        End If

        ' ### Lüferson Görevleri ###
        If NPCName = "Lüferson Görevleri" Then
            If getCharClassName() = "Rogue" Then
                For i = 0 To 5
                    SendPack(GorevTuru & FormatHex(Hex(Melverick(i)), 8))
                Next
            End If

            If getCharClassName() = "Mage" Then
                For i = 6 To 11
                    SendPack(GorevTuru & FormatHex(Hex(Melverick(i)), 8))
                Next
            End If

            If getCharClassName() = "Priest" Then
                For i = 12 To 17
                    SendPack(GorevTuru & FormatHex(Hex(Melverick(i)), 8))
                Next
            End If

            If getCharClassName() = "Warrior" Then
                For i = 18 To 23
                    SendPack(GorevTuru & FormatHex(Hex(Melverick(i)), 8))
                Next
            End If
        End If

        ' ### Chitin Görevleri ###
        If NPCName = "Chitin Görevleri" Then
            If getCharClassName() = "Rogue" Then
                For i = 0 To 4
                    SendPack(GorevTuru & FormatHex(Hex(ChitinQuest(i)), 8))
                Next
            End If

            If getCharClassName() = "Mage" Then
                For i = 5 To 9
                    SendPack(GorevTuru & FormatHex(Hex(ChitinQuest(i)), 8))
                Next
            End If

            If getCharClassName() = "Priest" Then
                For i = 10 To 14
                    SendPack(GorevTuru & FormatHex(Hex(ChitinQuest(i)), 8))
                Next
            End If

            If getCharClassName() = "Warrior" Then
                For i = 15 To 19
                    SendPack(GorevTuru & FormatHex(Hex(ChitinQuest(i)), 8))
                Next
            End If
        End If

        ' ### Lard ORC ###
        If NPCName = "Lard ORC" Then
            If getCharClassName() = "Rogue" Then
                SendPack(GorevTuru & FormatHex(Hex(LardOrc(0)), 8))
            End If

            If getCharClassName() = "Mage" Then
                SendPack(GorevTuru & FormatHex(Hex(LardOrc(1)), 8))
            End If

            If getCharClassName() = "Priest" Then
                SendPack(GorevTuru & FormatHex(Hex(LardOrc(2)), 8))
            End If

            If getCharClassName() = "Warrior" Then
                SendPack(GorevTuru & FormatHex(Hex(LardOrc(3)), 8))
            End If
        End If


        ' ### ATMIŞ Görevleri ###
        If NPCName = "Atmış Görevleri" Then
            If getCharClassName() = "Rogue" Then
                For i = 0 To 19
                    SendPack(GorevTuru & FormatHex(Hex(Quest60(i)), 8))
                Next
            End If

            If getCharClassName() = "Mage" Then
                For i = 20 To 39
                    SendPack(GorevTuru & FormatHex(Hex(Quest60(i)), 8))
                Next
            End If

            If getCharClassName() = "Priest" Then
                For i = 40 To 59
                    SendPack(GorevTuru & FormatHex(Hex(Quest60(i)), 8))
                Next
            End If

            If getCharClassName() = "Warrior" Then
                For i = 60 To 79
                    SendPack(GorevTuru & FormatHex(Hex(Quest60(i)), 8))
                Next
            End If
        End If

        ' ### Eslant Görevleri ###
        If NPCName = "Eslant Görevleri" Then
            If getCharClassName() = "Rogue" Then
                For i = 0 To 29
                    SendPack(GorevTuru & FormatHex(Hex(QuestEslant(i)), 8))
                Next
            End If

            If getCharClassName() = "Mage" Then

            End If

            If getCharClassName() = "Priest" Then

            End If

            If getCharClassName() = "Warrior" Then

            End If
        End If

    End Sub
    Public Sub questCompleted(ByVal GorevNPC As String)
        Dim ItemSira As String, TakiSira As String, LazerSira As String, MelverickSira As String, TrolArmor As String, i As Integer
        Select Case getCharClassName()
            Case "Warrior"
                ItemSira = "00" : TakiSira = "00" : LazerSira = "0" : MelverickSira = "0" : TrolArmor = "0"
            Case "Mage"
                ItemSira = "02" : TakiSira = "04" : LazerSira = "2" : MelverickSira = "2" : TrolArmor = "2"
            Case "Priest"
                ItemSira = "03" : TakiSira = "00" : LazerSira = "0" : MelverickSira = "0" : TrolArmor = "3"
            Case Else
                ItemSira = "01" : TakiSira = "02" : LazerSira = "1" : MelverickSira = "1" : TrolArmor = "1"
        End Select
        If GorevNPC = "Potrang" Then
            For i = 0 To 4
                SendPack("2001" & findIDtoNPC("Potrang") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(Potrang(i)), 8))
                SendPack("55001232353030305F70686F7472616E672E6C7561FF")
            Next
        End If

        If GorevNPC = "Sentinel Patrick" Then
            For i = 0 To 19
                SendPack("2001" & findIDtoNPC("Patrick") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(Sentinel_Patrick(i)), 8))
                Select Case Sentinel_Patrick(i)
                    Case 3355, 5275, 3373, 5310, 3323
                        SendPack("55001031333031335F5061747269632E6C7561" & TakiSira)
                    Case 3363, 3383, 3393, 5282, 5289, 3335, 3343
                        SendPack("55001031333031335F5061747269632E6C7561" & ItemSira)
                    Case Else
                        SendPack("55001031333031335F5061747269632E6C7561FF")
                End Select
            Next
        End If

        If GorevNPC = "GM Daughter" Then
            For i = 0 To 4
                SendPack("2001" & findIDtoNPC("Daughter of Grand") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(GM_Daughter(i)), 8))
                Select Case GM_Daughter(i)
                    Case 96
                        SendPack("55001131363037395F4D656E697369612E6C7561" & TakiSira)
                    Case 103
                        SendPack("55001131363037395F4D656E697369612E6C7561" & ItemSira)
                    Case Else
                        SendPack("55001131363037395F4D656E697369612E6C7561FF")
                End Select
            Next
        End If

        If GorevNPC = "Trust - 10 Lw" Then
            For i = 0 To 2
                If TrustMiss(i) = 7351 Then
                    SendPack("2001" & findIDtoNPC("Billbor") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(TrustMiss(i)), 8))
                    SendPack("55001031383030355F42696C626F722E6C7561")
                    SendPack("55001031383030355F42696C626F722E6C7561FF")
                ElseIf TrustMiss(i) = 7359 Then
                    SendPack("2001" & findIDtoNPC("Kaishan") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(TrustMiss(i)), 8))
                    SendPack("55001031383030345F4B616973616E2E6C7561FF")
                Else
                    SendPack("2001" & findIDtoNPC("Kaishan") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(TrustMiss(i)), 8))
                    SendPack("55001031383030345F4B616973616E2E6C7561")
                    SendPack("55001031383030345F4B616973616E2E6C7561FF")
                End If
            Next
        End If

        If GorevNPC = "Blacksmith Heppa" Then
            For i = 0 To 3
                SendPack("2001" & findIDtoNPC("Blacksmith") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(BlackSmithHeppa(i)), 8))
                Select Case BlackSmithHeppa(i)
                    Case 363
                        SendPack("55000E31343330315F486170612E6C7561" & TakiSira)
                    Case Else
                        SendPack("55000E31343330315F486170612E6C7561FF")
                End Select
            Next
        End If


        If GorevNPC = "Jed" Then
            For i = 0 To 7
                SendPack("2001" & findIDtoNPC("Jed") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(Jed(i)), 8))
                If Jed(i) = 7370 Then
                    SendPack("55000D32353030315F6A65642E6C7561")
                    SendPack("55000D32353030315F6A65642E6C7561FF")
                ElseIf Jed(i) = 7392 Then
                    SendPack("2001" & findIDtoNPC("Black Market") & "FFFFFFFF")
                    SendPack("55000D32353032325F425F4D2E6C7561")
                    SendPack("55000D32353032325F425F4D2E6C7561")
                    SendPack("55000D32353032325F425F4D2E6C7561")
                    SendPack("55000D32353032325F425F4D2E6C7561")
                    SendPack("2001" & findIDtoNPC("Black Market") & "FFFFFFFF")
                    SendPack("55010D32353032325F425F4D2E6C7561")
                    SendPack("55000D32353032325F425F4D2E6C7561")
                    SendPack("55000D32353032325F425F4D2E6C7561")
                    SendPack("55000D32353032325F425F4D2E6C7561")
                Else
                    SendPack("55000D32353030315F6A65642E6C7561FF")
                End If
            Next
        End If



        If GorevNPC = "Osmand" Then
            For i = 0 To 4
                SendPack("2001" & findIDtoNPC("Osmond") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(Osmand(i)), 8))
                Select Case Osmand(i)
                    Case 305, 513, 9945
                        SendPack("55001031393030345F4F736D756E642E6C7561" & TakiSira)
                    Case Else
                        SendPack("55001031393030345F4F736D756E642E6C7561FF")
                End Select
            Next
        End If

        If GorevNPC = "Berret" Then
            For i = 0 To 4
                SendPack("2001" & findIDtoNPC("Berret") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(Berret(i)), 8))
                Select Case Berret(i)
                    Case 534
                        SendPack("55001031393030325F4275726C65742E6C7561" & ItemSira)
                    Case Else
                        SendPack("55001031393030325F4275726C65742E6C7561FF")
                End Select
            Next
        End If


        If GorevNPC = "Lazer" Then
            For i = 0 To 1
                SendPack("2001" & findIDtoNPC("Lazi") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(Lazer(i)), 8))
                SendPack("550" & LazerSira & "0F31393030335F4C65697A792E6C7561FF")
            Next

            If getCharClassName() = "Rogue" Then
                For i = 2 To 11
                    SendPack("2001" & findIDtoNPC("Lazi") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Lazer(i)), 8))
                    SendPack("55000F31393030335F4C65697A792E6C7561FF")
                Next
            End If

            If getCharClassName() = "Mage" Then
                For i = 12 To 21
                    SendPack("2001" & findIDtoNPC("Lazi") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Lazer(i)), 8))
                    SendPack("55000F31393030335F4C65697A792E6C7561FF")
                Next
            End If

            If getCharClassName() = "Priest" Then
                For i = 22 To 31
                    SendPack("2001" & findIDtoNPC("Lazi") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Lazer(i)), 8))
                    SendPack("55000F31393030335F4C65697A792E6C7561FF")
                Next
            End If

            If getCharClassName() = "Warrior" Then
                For i = 32 To 41
                    SendPack("2001" & findIDtoNPC("Lazi") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Lazer(i)), 8))
                    SendPack("55000F31393030335F4C65697A792E6C7561FF")
                Next
            End If
        End If

        If GorevNPC = "Nameless Warrior" Then
            For i = 0 To 1
                SendPack("2001" & findIDtoNPC("Nameless Wa") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(NameLessWarrior(i)), 8))
                SendPack("55001232343431345F4E616D656C6573732E6C7561" & TakiSira)
            Next
        End If

        If GorevNPC = "Lüferson İtem Görevleri" Then
            For i = 0 To 1
                SendPack("2001" & findIDtoNPC("Shymer") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(Shymer(i)), 8))
                SendPack("55001132343433355F4D6F7261646F6E2E6C7561FF")
            Next
        End If


        If GorevNPC = "Lüferson Görevleri" Then
            'MelverickSira
            If getCharClassName() = "Rogue" Then
                For i = 0 To 5
                    SendPack("2001" & findIDtoNPC("Melverick") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Melverick(i)), 8))
                    If i = 0 Then
                        SendPack("550" & MelverickSira & "1232343432375F4D656C62757269632E6C7561FF")
                    ElseIf i = 5 Then
                        SendPack("2001" & findIDtoNPC("Bertem") & "FFFFFFFF")
                        SendPack("55000F32343434315F426F746F6D2E6C7561FF")
                    Else
                        SendPack("55001232343432375F4D656C62757269632E6C7561FF")
                    End If
                    '
                Next
            End If

            If getCharClassName() = "Mage" Then
                For i = 6 To 11
                    SendPack("2001" & findIDtoNPC("Melverick") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Melverick(i)), 8))
                    If i = 6 Then
                        SendPack("550" & MelverickSira & "1232343432375F4D656C62757269632E6C7561FF")
                    ElseIf i = 11 Then
                        SendPack("2001" & findIDtoNPC("Bertem") & "FFFFFFFF")
                        SendPack("55000F32343434315F426F746F6D2E6C7561FF")
                    Else
                        SendPack("55001232343432375F4D656C62757269632E6C7561FF")
                    End If
                Next
            End If

            If getCharClassName() = "Priest" Then
                For i = 12 To 17
                    SendPack("2001" & findIDtoNPC("Melverick") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Melverick(i)), 8))
                    If i = 12 Then
                        SendPack("550" & MelverickSira & "1232343432375F4D656C62757269632E6C7561FF")
                    ElseIf i = 17 Then
                        SendPack("2001" & findIDtoNPC("Bertem") & "FFFFFFFF")
                        SendPack("55000F32343434315F426F746F6D2E6C7561FF")
                    Else
                        SendPack("55001232343432375F4D656C62757269632E6C7561FF")
                    End If
                Next
            End If

            If getCharClassName() = "Warrior" Then
                For i = 18 To 23
                    SendPack("2001" & findIDtoNPC("Melverick") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Melverick(i)), 8))
                    If i = 18 Then
                        SendPack("550" & MelverickSira & "1232343432375F4D656C62757269632E6C7561FF")
                    ElseIf i = 23 Then
                        SendPack("2001" & findIDtoNPC("Bertem") & "FFFFFFFF")
                        SendPack("55000F32343434315F426F746F6D2E6C7561FF")
                    Else
                        SendPack("55001232343432375F4D656C62757269632E6C7561FF")
                    End If
                Next
            End If
        End If

        If GorevNPC = "Lard ORC" Then
            For i = 0 To 3
                SendPack("2001" & findIDtoNPC("Keife") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(LardOrc(i)), 8))
                SendPack("550" & MelverickSira & "0E32343433325F4B6170652E6C7561FF")
            Next
        End If


        If GorevNPC = "Chitin Görevleri" Then
            'Chitin Görevleri
            For i = 0 To 19
                SendPack("2001" & findIDtoNPC("Shymer") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(ChitinQuest(i)), 8))
                SendPack("55001132343433355F4D6F7261646F6E2E6C7561FF")

                SendPack("2001" & findIDtoNPC("Shymer") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(ChitinQuest(i)), 8))
                SendPack("55001132343433355F4D6F7261646F6E2E6C7561FF")

                SendPack("2001" & findIDtoNPC("Bertem") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(ChitinQuest(i)), 8))
                SendPack("55000F32343434315F426F746F6D2E6C7561FF")

                SendPack("2001" & findIDtoNPC("Keife") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(ChitinQuest(i)), 8))
                SendPack("55000E32343433325F4B6170652E6C7561FF")

                SendPack("2001" & findIDtoNPC("Russel") & "FFFFFFFF")
                SendPack("6407" & FormatHex(Hex(ChitinQuest(i)), 8))
                SendPack("55000F32343433305F527563656C2E6C7561FF")
            Next
        End If

        If GorevNPC = "Atmış Görevleri" Then
            'Chitin Görevleri
            If getCharClassName() = "Rogue" Then
                For i = 0 To 19
                    SendPack("2001" & findIDtoNPC("Keife") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55000E32343433325F4B6170652E6C7561FF")
                    SendPack("2001" & findIDtoNPC("Russel") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55000F32343433305F527563656C2E6C7561FF")
                    SendPack("2001" & findIDtoNPC("Verca") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55001332343430365F4775617264736D616E2E6C7561FF")
                    If i = 16 Then
                        SendPack("2001" & findIDtoNPC("Hashan") & "FFFFFFFF")
                        SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                        SendPack("55001032343433315F48617368616E2E6C75610" & TrolArmor)
                    End If
                Next
            End If
            If getCharClassName() = "Mage" Then
                For i = 20 To 39
                    SendPack("2001" & findIDtoNPC("Keife") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55000E32343433325F4B6170652E6C7561FF")
                    SendPack("2001" & findIDtoNPC("Russel") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55000F32343433305F527563656C2E6C7561FF")
                    SendPack("2001" & findIDtoNPC("Verca") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55001332343430365F4775617264736D616E2E6C7561FF")
                    If i = 26 Then
                        SendPack("2001" & findIDtoNPC("Hashan") & "FFFFFFFF")
                        SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                        SendPack("55001032343433315F48617368616E2E6C75610" & TrolArmor)
                    End If
                Next
            End If
            If getCharClassName() = "Priest" Then
                For i = 40 To 59
                    SendPack("2001" & findIDtoNPC("Keife") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55000E32343433325F4B6170652E6C7561FF")
                    SendPack("2001" & findIDtoNPC("Russel") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55000F32343433305F527563656C2E6C7561FF")
                    SendPack("2001" & findIDtoNPC("Verca") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55001332343430365F4775617264736D616E2E6C7561FF")
                    If i = 46 Then
                        SendPack("2001" & findIDtoNPC("Hashan") & "FFFFFFFF")
                        SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                        SendPack("55001032343433315F48617368616E2E6C75610" & TrolArmor)
                    End If
                Next
            End If
            If getCharClassName() = "Warrior" Then
                For i = 60 To 79
                    SendPack("2001" & findIDtoNPC("Keife") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55000E32343433325F4B6170652E6C7561FF")
                    SendPack("2001" & findIDtoNPC("Russel") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55000F32343433305F527563656C2E6C7561FF")
                    SendPack("2001" & findIDtoNPC("Verca") & "FFFFFFFF")
                    SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                    SendPack("55001332343430365F4775617264736D616E2E6C7561FF")
                    If i = 66 Then
                        SendPack("2001" & findIDtoNPC("Hashan") & "FFFFFFFF")
                        SendPack("6407" & FormatHex(Hex(Quest60(i)), 8))
                        SendPack("55001032343433315F48617368616E2E6C75610" & TrolArmor)
                    End If
                Next
            End If
        End If
    End Sub
    Public Sub getBuffNpc()
        SendPack("2001" & findIDtoNPC("National Enchanter") & "FFFFFFFF")
        SendPack("55031233313530385F4E456E6368616E742E6C7561")
        SendPack("55001233313530385F4E456E6368616E742E6C7561")
        SendPack("3106" & "F7810700" & getCharID() & getCharID() & "000000000000000000000000")
        SendPack("3106" & "2FA20700" & getCharID() & getCharID() & "000000000000000000000000")
        SendPack("3106" & "7FA10700" & getCharID() & getCharID() & "000000000000000000000000")
        SendPack("3103" & "F7810700" & getCharID() & getCharID() & "000000000000000000000000")
        SendPack("3103" & "7FA10700" & getCharID() & getCharID() & "000000000000000000000000")
        SendPack("3103" & "2FA20700" & getCharID() & getCharID() & "000000000000000000000000")
    End Sub
    Public Sub getMonsterStonr()
        SendPack("99028201") 'Reach Level 10
        SendPack("99028301") 'Reach Level 20
        SendPack("99028401") 'Reach Level 30
        SendPack("99028501") 'Reach Level 40
        SendPack("99025600") 'Merchant's Daughter
        SendPack("99025800") 'Chief Guard Patrick
        SendPack("99026500") 'Chief Hunting I
        SendPack("99027400") 'Chief Hunting II
        SendPack("99020200") '10 Kill Görevi 1
        SendPack("99021000") '10 Kill Görevi 2
        SendPack("99025F00") '1.000 Mob kesme
        SendPack("99026A00") 'Battle With Fog
    End Sub
    Public Sub buyDagger()
        SendPack("210129110300" + findIDtoNPC("Gargameth") + "0131259006" + ADWORD(getFirstEmptySlotInv, 2) + "01000000")
        SendPack("6A02")
    End Sub
    Public Sub upDagger()
        If getItemSlotInv("110110001") > 0 And getItemSlotInv("379229000") > 0 Then
            SendPack("5B02011C27" & "31259006" & ADWORD(findItemSlot("110110001"), 2) & "48939A16" & ADWORD(findItemSlot("379229000"), 2) & "00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF")
        End If
    End Sub
    Public Sub sellItem(ByRef LootListView As ListView)
        Dim i As Integer
        For i = (KO_OFF_INVROW) To KO_OFF_INVCOUNT
            If searchListView(getItemID(i), 1, LootListView) = False And getItemID(i) > 0 Then
                SendPack("210218E40300" & findIDtoNPC("Sundries") & "01" & ADWORD(getItemID(i)) & ADWORD(i - KO_OFF_INVROW, 2) & ADWORD(getItemCountSlot(i), 4))
                SendPack("6A02")
                Sleep(200)
            End If
        Next
    End Sub
    Public Sub StatPaketGonder(StatPaket As String, StatPaketEk As String)
        If (GetTickCount() - StatPaketTimer) > 1000 Then
            SendPack(StatPaketEk)
            SendPack(StatPaket)
            SendPack(StatPaketEk)
            StatPaketTimer = GetTickCount()
        End If
    End Sub
    Public Sub CharStatDoldur()
        If getCharStat(0) > 0 Then
            Select Case getCharClassName()
                Case "Rogue"
                    If getCharStat(2) <= 90 And getCharLevel() >= 20 Then
                        StatPaketGonder("28030100", "28020100") 'dex,hp
                    Else
                        StatPaketGonder("28030100", "28030100") 'dex,dex
                    End If

                Case "Warrior"
                    If getCharStat(2) <= 90 And getCharLevel() >= 20 Then
                        StatPaketGonder("28010100", "28020100") 'str,hp
                    Else
                        StatPaketGonder("28010100", "28010100") 'str,str
                    End If

                Case "Mage"
                    If getCharStat(4) <= 160 Then
                        StatPaketGonder("28050100", "28040100") 'mp,int
                    ElseIf getCharStat(5) <= 102 Then
                        StatPaketGonder("28050100", "28050100") 'mp, mp
                    ElseIf getCharStat(4) >= 160 Then
                        StatPaketGonder("28050100", "28050100") 'mp, mp
                    End If

                Case "Priest"
                    If getCharStat(4) <= 160 And getCharStat(1) <= 106 And getCharLevel() <= 20 Then
                        StatPaketGonder("28010100", "28040100") 'str, int
                    ElseIf getCharStat(4) <= 160 Then
                        StatPaketGonder("28040100", "28040100") ' int, int
                    Else
                        StatPaketGonder("28010100", "28010100") ' Str, Str
                    End If
            End Select
        End If
    End Sub
    Public Sub luckyYut()
        SendPack("55000F32393038335F446F6761652E6C7561")
        SendPack("8605")
    End Sub
#End Region
#Region "######## Upgrade Bot ########"
    Public Sub buyUpItem(ByVal buyItemID As String, ByVal buySlotPage As String, ByVal invSlot As Integer)
        If (getItemIDInv(invSlot) = 0) Then
            If (buyItemID = "B13E3D42") Then
                SendPack("210129110300" & findIDtoNPC("Weapon Merchant") & "01" & buyItemID & FormatHex(Hex(invSlot), 2) & "0100" & buySlotPage)
            Else
                SendPack("210111150300" & findIDtoNPC("Armor Merchant") & "01" & buyItemID & FormatHex(Hex(invSlot), 2) & "0100" & buySlotPage)
            End If
            SendPack("6A02")
        End If
    End Sub
    Public Sub buyUpScroll(ByVal scRow As ComboBox, ByVal scCount As Integer)
        Dim scSlot As Integer, scID As Integer, scTotal As Integer
        Select Case scRow.SelectedIndex
            Case 0 'Low
                scSlot = 7
                scID = 379221000
            Case 1 'Middle
                scSlot = 13
                scID = 379205000
            Case 2 'High
                scSlot = 1
                scID = 379016000
            Case 3 'Blessed
                scSlot = 19
                scID = 379021000
        End Select
        scTotal = scCount - getItemCountSlot(KO_OFF_INVCOUNT)
        If (scTotal > 0 And scTotal <= 27) Then
            SendPack("210130E00300" & findIDtoNPC("Upgrade Scroll Merchant") & "01" & ADWORD(scID) & "1B" & FormatHex(Hex(scTotal), 2) & "0000" & FormatHex(Hex(scSlot), 2))
            SendPack("6A02")
        End If
        Sleep(4000)
    End Sub
    Public Sub upgradeAll()
        Dim ItemID As Long, i As Integer
        ItemID = KO_OFF_INVROW
        For i = 0 To 26
            If getItemID(ItemID) <> 0 And getItemID(KO_OFF_INVCOUNT) <> 0 Then
                If (Left(getItemID(ItemID), 9) = 111131100) Then
                    SendPack("5B02011C27" + "B" + Right(getItemID(ItemID), 1) + "3E3D42" + ADWORD(i, 2) + ADWORD(getItemID(KO_OFF_INVCOUNT)) + "1B00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF")
                Else
                    SendPack("5B02011427" + ADWORD(getItemID(ItemID)) + ADWORD(i, 2) + ADWORD(getItemID(KO_OFF_INVCOUNT)) + "1B00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF")
                End If
            End If
            ItemID = ItemID + 1
        Next
    End Sub
    Public Sub upgradeSlot(ByVal chkl_Items As CheckedListBox, ByVal SixToSeven As Boolean)
        Dim ItemID As Long, i As Integer
        ItemID = KO_OFF_INVROW
        For i = 0 To 26
            If getItemID(ItemID) <> 0 And getItemID(42) <> 0 And chkl_Items.GetItemChecked(i) = True Then
                If (Left(getItemID(ItemID), 9) = 111131100) Then
                    SendPack("5B02011C27" + "B" + Right(getItemID(ItemID), 1) + "3E3D42" + ADWORD(i, 2) + ADWORD(getItemID(KO_OFF_INVCOUNT)) + "1B00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF")
                Else
                    SendPack("5B02011427" + ADWORD(getItemID(ItemID)) + ADWORD(i, 2) + ADWORD(getItemID(KO_OFF_INVCOUNT)) + "1B00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF")
                End If
                Sleep(50)
            End If
            ItemID = ItemID + 1
        Next
        If (SixToSeven) Then
            ItemID = KO_OFF_INVROW
            For i = 0 To 26
                If getItemID(ItemID) <> 0 And getItemID(42) <> 0 And chkl_Items.GetItemChecked(i) = True And getItemUpExt(ItemID) = 6 Then
                    If (Left(getItemID(ItemID), 9) = 111131100) Then
                        SendPack("5B02011C27" + "B" + Right(getItemID(ItemID), 1) + "3E3D42" + ADWORD(i, 2) + ADWORD(getItemID(KO_OFF_INVCOUNT)) + "1B00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF")
                    Else
                        SendPack("5B02011427" + ADWORD(getItemID(ItemID)) + ADWORD(i, 2) + ADWORD(getItemID(KO_OFF_INVCOUNT)) + "1B00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF00000000FF")
                    End If
                    Sleep(50)
                End If
                ItemID = ItemID + 1
            Next
        End If
    End Sub
    Public Sub compBuyUpItem(ByVal chkl_buyItems As CheckedListBox)
        For i As Integer = 0 To 26
            Dim txtChk As String, lstItemName As String
            txtChk = chkl_buyItems.Items.Item(i).ToString()
            If (txtChk.Length > 5) Then
                lstItemName = txtChk.Substring(txtChk.IndexOf(") ") + 2, txtChk.Length - (txtChk.IndexOf(") ") + 2))
                If (lstItemName = "Quilted Pauldron") Then buyUpItem("2908FB0B", "0000", i)
                If (lstItemName = "Leather Pads") Then buyUpItem("110CFB0B", "0001", i)
                If (lstItemName = "Leather Caps") Then buyUpItem("F90FFB0B", "0002", i)
                If (lstItemName = "Leather Gloves") Then buyUpItem("E113FB0B", "0003", i)
                If (lstItemName = "Leather Shoes") Then buyUpItem("C917FB0B", "0004", i)

                If (lstItemName = "Rogue Shirt") Then buyUpItem("29625D0E", "0006", i)
                If (lstItemName = "Rogue Pads") Then buyUpItem("11665D0E", "0007", i)
                If (lstItemName = "Rogue Cap") Then buyUpItem("F9695D0E", "0008", i)
                If (lstItemName = "Rogue Gloves") Then buyUpItem("E16D5D0E", "0009", i)
                If (lstItemName = "Rogue Shoes") Then buyUpItem("C9715D0E", "000A", i)

                If (lstItemName = "Mage Cotton Robe") Then buyUpItem("298F8E0F", "000C", i)
                If (lstItemName = "Mage Cloth Pants") Then buyUpItem("11938E0F", "000D", i)
                If (lstItemName = "Mage Hat") Then buyUpItem("F9968E0F", "000E", i)
                If (lstItemName = "Mage Gloves") Then buyUpItem("E19A8E0F", "000F", i)
                If (lstItemName = "Mage Shoes") Then buyUpItem("C99E8E0F", "0010", i)

                If (lstItemName = "Fabric Coat") Then buyUpItem("29BCBF10", "0012", i)
                If (lstItemName = "Fabric Pants") Then buyUpItem("11C0BF10", "0013", i)
                If (lstItemName = "Priest Cap") Then buyUpItem("F9C3BF10", "0014", i)
                If (lstItemName = "Priest Gloves") Then buyUpItem("E1C7BF10", "0015", i)
                If (lstItemName = "Priest Shoes") Then buyUpItem("C9CBBF10", "0016", i)

                If (lstItemName = "Half Plate Pauldron") Then buyUpItem("694A0A0C", "0100", i)
                If (lstItemName = "Half Plate Pads") Then buyUpItem("514E0A0C", "0101", i)
                If (lstItemName = "Half Plate Helmet") Then buyUpItem("39520A0C", "0102", i)
                If (lstItemName = "Half Plate Gauntlet") Then buyUpItem("21560A0C", "0103", i)
                If (lstItemName = "Half Plate Boots") Then buyUpItem("095A0A0C", "0104", i)

                If (lstItemName = "Rogue Half Plate Pauldron") Then buyUpItem("69A46C0E", "0106", i)
                If (lstItemName = "Rogue Half Plate Pads") Then buyUpItem("51A86C0E", "0107", i)
                If (lstItemName = "Rogue Helmet") Then buyUpItem("39AC6C0E", "0108", i)
                If (lstItemName = "Rogue Gauntlet") Then buyUpItem("21B06C0E", "0109", i)
                If (lstItemName = "Rogue Boots") Then buyUpItem("09B46C0E", "010A", i)

                If (lstItemName = "Mage Linen Robe") Then buyUpItem("69D19D0F", "010C", i)
                If (lstItemName = "Mage Linen Pants") Then buyUpItem("51D59D0F", "010D", i)
                If (lstItemName = "Mage Linen Cap") Then buyUpItem("39D99D0F", "010E", i)
                If (lstItemName = "Mage Leather Gloves") Then buyUpItem("21DD9D0F", "010F", i)
                If (lstItemName = "Mage Leather Boots") Then buyUpItem("09E19D0F", "0110", i)

                If (lstItemName = "Silk Coat") Then buyUpItem("69FECE10", "0112", i)
                If (lstItemName = "Silk Pants") Then buyUpItem("5102CF10", "0113", i)
                If (lstItemName = "Priest Helmet") Then buyUpItem("3906CF10", "0114", i)
                If (lstItemName = "Priest Gauntlet") Then buyUpItem("210ACF10", "0115", i)
                If (lstItemName = "Priest Boots") Then buyUpItem("090ECF10", "0116", i)

                If (lstItemName = "Plate Armor Pauldron") Then buyUpItem("A98C190C", "0200", i)
                If (lstItemName = "Plate Armor Pads") Then buyUpItem("9190190C", "0201", i)
                If (lstItemName = "Plate Armor Helmet") Then buyUpItem("7994190C", "0202", i)
                If (lstItemName = "Plate Armor Gauntlet") Then buyUpItem("6198190C", "0203", i)
                If (lstItemName = "Plate Armor Boots") Then buyUpItem("499C190C", "0204", i)

                If (lstItemName = "Rogue Plate Armor Pauldron") Then buyUpItem("A9E67B0E", "0206", i)
                If (lstItemName = "Rogue Plate Armor Pads") Then buyUpItem("91EA7B0E", "0207", i)
                If (lstItemName = "Rogue Plate Helmet") Then buyUpItem("79EE7B0E", "0208", i)
                If (lstItemName = "Rogue Plate Gauntlet") Then buyUpItem("61F27B0E", "0209", i)
                If (lstItemName = "Rogue Plate Boots") Then buyUpItem("49F67B0E", "020A", i)

                If (lstItemName = "Mage Silk Robe") Then buyUpItem("A913AD0F", "020C", i)
                If (lstItemName = "Mage Silk Pants") Then buyUpItem("9117AD0F", "020D", i)
                If (lstItemName = "Mage Helmet") Then buyUpItem("791BAD0F", "020E", i)
                If (lstItemName = "Mage Hard Leather Gloves") Then buyUpItem("611FAD0F", "020F", i)
                If (lstItemName = "Mage Hard Leather Boots") Then buyUpItem("4923AD0F", "0210", i)

                If (lstItemName = "Priest Plate Pauldron") Then buyUpItem("A940DE10", "0212", i)
                If (lstItemName = "Priest Plate Pads") Then buyUpItem("9144DE10", "0213", i)
                If (lstItemName = "Priest Plate Helmet") Then buyUpItem("7948DE10", "0214", i)
                If (lstItemName = "Priest Plate Gauntlet") Then buyUpItem("614CDE10", "0215", i)
                If (lstItemName = "Priest Plate Boots") Then buyUpItem("4950DE10", "0216", i)

                If (lstItemName = "Weapon Breaker") Then buyUpItem("B13E3D42", "000C", i)
            End If
        Next
        Sleep(2000)
    End Sub
    Public Function getTotalUpPrice(ByVal cmbScroll As ComboBox, ByVal chklItems As CheckedListBox) As Integer
        Dim coinScroll As Integer, coinTotal As Integer = 0
        Dim txtChk As String, lstItemName As String
        coinScroll = getCoinToItem(cmbScroll.Text)
        For i As Integer = 0 To 26
            If (chklItems.GetItemChecked(i) = True) Then
                txtChk = chklItems.Items(i).ToString()
                If (getItemIDInv(i) = 0 And txtChk.Length > 5) Then
                    lstItemName = txtChk.Substring(txtChk.IndexOf(") ") + 2, txtChk.Length - (txtChk.IndexOf(") ") + 2))
                    coinTotal += getCoinToItem(lstItemName) + coinScroll
                Else
                    coinTotal += coinScroll
                End If
            End If
        Next
        Return coinTotal
    End Function
    Public Function getExItemLimit(ByVal exLimit As Integer)
        For i = KO_OFF_INVROW To 42
            If getItemUpExt(i) >= exLimit Then
                Return True
            End If
        Next
        Return False
    End Function
#End Region
#Region "######## Oto Login ########"
    Public Sub ChannelList()
        Dim pHook As String, ph() As Byte
        pHook = "608B0D" & ADWORD(KO_OTO_LOGIN_PTR) & "8B89" & ADWORD(&H12C) & "68" & ADWORD(&HCD) & "BF" & ADWORD(KO_OTO_LOGIN_01) & "FFD761C3"
        ph = ConvHEX2ByteArray(pHook)
        ExecuteRemoteCode(ph)
    End Sub
    Public Sub ChooseChannel(ByVal ChannelRow As Integer)
        Dim pHook As String, ph() As Byte
        pHook = "608B0D" & ADWORD(KO_OTO_LOGIN_PTR) & "8B89" & ADWORD(&H12C) & "6A" & Strings.Left(ADWORD(ChannelRow), 2) & "BF" & ADWORD(KO_OTO_LOGIN_02) & "FFD761C3"
        ph = ConvHEX2ByteArray(pHook)
        ExecuteRemoteCode(ph)
    End Sub
    Public Sub ChooseServer(ByVal ServerRow As Integer)
        Dim pHook As String, ph() As Byte
        pHook = "608B0D" & ADWORD(KO_OTO_LOGIN_PTR) & "8B89" & ADWORD(&H12C) & "BF" & ADWORD(KO_OTO_LOGIN_03) & "FFD731C931FF" & "8B0D" & ADWORD(KO_OTO_LOGIN_PTR) & "8B89" & ADWORD(&H12C) & "6A" & Strings.Left(ADWORD(ServerRow + 1), 2) & "BF" & ADWORD(KO_OTO_LOGIN_04) & "FFD761C3"
        ph = ConvHEX2ByteArray(pHook)
        ExecuteRemoteCode(ph)
    End Sub
    Public Sub PressLoginLeft()
        Dim pHook As String, ph() As Byte
        pHook = "608B0D" & ADWORD(KO_OTO_BTN_PTR) & "BF" & ADWORD(KO_BTN_LEFT) & "FFD761C3"
        ph = ConvHEX2ByteArray(pHook)
        ExecuteRemoteCode(ph)
    End Sub
    Public Sub PressLoginRight()
        Dim pHook As String, ph() As Byte
        pHook = "608B0D" & ADWORD(KO_OTO_BTN_PTR) & "BF" & ADWORD(KO_BTN_RIGHT) & "FFD761C3"
        ph = ConvHEX2ByteArray(pHook)
        ExecuteRemoteCode(ph)
    End Sub
    Public Sub PressLoginOk()
        Dim pHook As String, ph() As Byte
        pHook = "608B0D" & ADWORD(KO_OTO_BTN_PTR) & "BF" & ADWORD(KO_BTN_LOGIN) & "FFD761C3"
        ph = ConvHEX2ByteArray(pHook)
        ExecuteRemoteCode(ph)
    End Sub
#End Region
#Region "######## Mavi Kutu ########"
    Public Sub addMethodBlue(ByRef cmbQuest As ComboBox)
        cmbQuest.Items.Clear()
        Dim Methods As String()
        If (getCharIrk() = "Human") Then
            Methods = {"Görevci Git", "Görev Al", "Zone Git", "Görev Ver", "Town", "Dönüş Gate", "Geri Dön", "Görevci Git", "2. Göreve Geç", "Town", "Gate Git", "CZ Git", "CZ Görevci", "CZ Görevi Al", "Gizlenme Bas", "Görev Ver", "Geri Dön", "Görevci Gel", "CZ Görev Teslim", "CZ Gate Git", "Maragon Git", "Moria Git", "Kutu Kırdır", "Görev Tamam"}
        Else
            Methods = {"Görevci Git", "Görev Al", "Zone Git", "Görev Ver", "Town", "Dönüş Gate", "Geri Dön", "Görevci Git", "2. Göreve Geç", "Town", "Gate Git", "CZ Git", "CZ Görevci", "CZ Görevi Al", "Gizlenme Bas", "Görev Ver", "Geri Dön", "Görevci Gel", "CZ Görev Teslim", "CZ Gate Git", "Maragon Git", "Moria Git", "Kutu Kırdır", "Görev Tamam"}
        End If
        For Each item As String In Methods
            cmbQuest.Items.Add(item)
        Next
    End Sub
    Public Sub blueQuestRow(ByRef cmbQuest As ComboBox)
        Dim qNpcX, qNpcY, CorX1, CorY1, CorX2, CorY2, CorX3, CorY3, CorX4, CorY4, CorX5, CorY5, CorX6, CorY6, CorBackX, CorBackY, GateCorX, GateCorY, QuestCzX, QuestCzY, CZGateX, CZGateY, CZBoxX1, CZBoxY1, CZBoxX2, CZBoxY2 As Integer
        If (getCharIrk() = "Human") Then
            'Görevci
            qNpcX = 1705
            qNpcY = 322
            'Lüfer kordinat
            CorX1 = 1672
            CorY1 = 612

            CorX2 = 1667
            CorY2 = 910

            CorX3 = 1436
            CorY3 = 1290

            CorX4 = 1104
            CorY4 = 1660

            CorX5 = 524
            CorY5 = 1631

            CorX6 = 325
            CorY6 = 1765
            'Dönüş Gate
            CorBackX = 1843
            CorBackY = 152
            'Loan Gate
            GateCorX = 1589
            GateCorY = 417
            'CZ Quest
            QuestCzX = 606
            QuestCzY = 891

            'Box
            CZBoxX1 = 827
            CZBoxY1 = 1170
            'Box
            CZBoxX2 = 1381
            CZBoxY2 = 1100

            CZGateX = 621
            CZGateY = 908
        Else
            'Görevci
            qNpcX = 332
            qNpcY = 1736

            'Elmorad kordinat
            CorX1 = 399
            CorY1 = 1372

            CorX2 = 407
            CorY2 = 1095

            CorX3 = 615
            CorY3 = 755

            CorX4 = 956
            CorY4 = 363

            CorX5 = 1550
            CorY5 = 431

            CorX6 = 1717
            CorY6 = 305

            'Dönüş Gate
            CorBackX = 224
            CorBackY = 1865

            'Town Gate
            GateCorX = 447
            GateCorY = 1619

            'CZ Quest
            QuestCzX = 1386
            QuestCzY = 1100

            'Box
            CZBoxX1 = 827
            CZBoxY1 = 1170

            CZBoxX2 = 612
            CZBoxY2 = 889

            CZGateX = 1378
            CZGateY = 1080
        End If
        If (getCharIrk() = "Human") Then
            Select Case cmbQuest.SelectedIndex
                Case 0 'Görevci Git
                    tpCharGo(qNpcX, qNpcY)
                Case 1 'Görev Al
                    SendPack("2001" & findIDtoNPC("Tactician") & "FFFFFFFF")
                    SendPack("55000F33313730315F47726163652E6C7561")
                    SendPack("55000F33313730315F47726163652E6C7561")
                    SendPack("55000F33313730315F47726163652E6C7561")
                    SendPack("55000F33313730315F47726163652E6C7561")
                    SendPack("55000F33313730315F47726163652E6C7561FF")
                    SendPack("55000F33313730315F47726163652E6C7561")
                    SendPack("55000F33313730315F47726163652E6C7561")

                Case 2
                    SendPack("31032B4F0900" & getCharID() & getCharID() & "00000000000000000000000000000000")
                Case 3
                    SendPack("2001" & findIDtoNPC(" Secret Archive") & "FFFFFFFF")
                    SendPack("55001333323630385F4B61727573436173652E6C7561")
                    SendPack("6404C0040000")
                Case 4
                    useTown()
                Case 5
                    walkChar(CorBackX, CorBackY)
                Case 6
                    SendPack("2001" & findIDtoNPC(" Camp Gate") & "FFFFFFFF")
                    SendPack("55000E32303330355F4D6F76652E6C7561")
                Case 7
                    tpCharGo(qNpcX, qNpcY)
                Case 8
                    SendPack("2001" & findIDtoNPC("Tactician") & "FFFFFFFF")
                    SendPack("55000F33313730315F47726163652E6C7561")
                    SendPack("55000F33313730315F47726163652E6C7561")
                    SendPack("55000F33313730315F47726163652E6C7561")
                    SendPack("55000F33313730315F47726163652E6C7561")
                    SendPack("55000F33313730315F47726163652E6C7561FF")
                    SendPack("55000F33313730315F47726163652E6C7561")
                Case 9
                    useTown()

                Case 10
                    walkChar(GateCorX, GateCorY)
                Case 11
                    SendPack("4BA30FDA00")
                Case 12
                    tpCharGo(QuestCzX, QuestCzY)
                Case 13
                    SendPack("2001" & findIDtoNPC("Analyst") & "FFFFFFFF")
                    SendPack("55031033323535385F4A756C6975732E6C7561")
                    SendPack("55001033323535385F4A756C6975732E6C7561")
                    SendPack("55001033323535385F4A756C6975732E6C7561FF")
                    SendPack("55001033323535385F4A756C6975732E6C7561")
                    SendPack("55001033323535385F4A756C6975732E6C7561")
                Case 14
                    SendPack("3103877A0700" & getCharID() & getCharID() & "00000000000000000000000000000000")
                Case 15
                    SendPack("2001" & findIDtoNPC("Ronarkland Information ") & "FFFFFFFF")
                    SendPack("55001233323631345F526F6E61436173652E6C7561")
                    SendPack("640410050000")
                Case 16
                    useRespawn()
                Case 17
                    tpCharGo(QuestCzX, QuestCzY)
                Case 18
                    SendPack("2001" & findIDtoNPC("Analyst") & "FFFFFFFF")
                    SendPack("55031033323535385F4A756C6975732E6C7561")
                    SendPack("55001033323535385F4A756C6975732E6C7561FF")
                    SendPack("640C12050000")

                Case 19
                    tpCharGo(CZGateX, CZGateY)

                Case 20 'Go Maradon
                    SendPack("4BB30FD41B")
                Case 21
                    tpCharGo(755, 771)

                Case 22 'Kutu kırdır
                    SendPack("2001" & findIDtoNPC("Moira") & "FFFFFFFF")
                    SendPack("640714050000")
                    SendPack("55000F31363034375F4D6F6972612E6C7561FF")
            End Select
        Else ' Karus
            Select Case cmbQuest.SelectedIndex
                Case 0 'Görevci Git
                    tpCharGo(qNpcX, qNpcY)

                Case 1 'Görev Al
                    SendPack("2001" & findIDtoNPC("Tactician") & "FFFFFFFF")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")
                    SendPack("55000F33313730305F4A6F7369612E6C7561FF")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")

                Case 2
                    SendPack("31032A4F0900" & getCharID() & getCharID() & "00000000000000000000000000000000")
                Case 3
                    SendPack("2001" & findIDtoNPC(" Secret Archive") & "FFFFFFFF")
                    SendPack("55001233323630395F456C6D6F436173652E6C7561")
                    SendPack("6404E3040000")

                Case 4
                    useTown()

                Case 5
                    walkChar(CorBackX, CorBackY)

                Case 6
                    SendPack("2001" & findIDtoNPC(" Camp Gate") & "FFFFFFFF")
                    SendPack("55000E31303330355F4D6F76652E6C7561")

                Case 7
                    tpCharGo(qNpcX, qNpcY)

                Case 8
                    SendPack("2001" & findIDtoNPC("Tactician") & "FFFFFFFF")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")
                    SendPack("55000F33313730305F4A6F7369612E6C7561FF")
                    SendPack("55000F33313730305F4A6F7369612E6C7561")

                Case 9
                    useTown()

                Case 10
                    walkChar(GateCorX, GateCorY)

                Case 11
                    SendPack("4BA60F7600")

                Case 12
                    tpCharGo(QuestCzX, QuestCzY)

                Case 13
                    SendPack("2001" & findIDtoNPC("Analyst") & "FFFFFFFF")
                    SendPack("55030F33323535375F4B657361722E6C7561")
                    SendPack("55000F33323535375F4B657361722E6C7561")
                    SendPack("55000F33323535375F4B657361722E6C7561FF")
                    SendPack("55000F33323535375F4B657361722E6C7561")
                    SendPack("55000F33323535375F4B657361722E6C7561")

                Case 14
                    SendPack("3103877A0700" & getCharID() & getCharID() & "00000000000000000000000000000000")

                Case 15
                    SendPack("2001" & findIDtoNPC("Ronarkland Information ") & "FFFFFFFF")
                    SendPack("55001233323631355F526F6E61436173652E6C7561")
                    SendPack("64040B050000")

                Case 16
                    useRespawn()

                Case 17
                    tpCharGo(QuestCzX, QuestCzY)

                Case 18
                    SendPack("2001" & findIDtoNPC("Analyst") & "FFFFFFFF")
                    SendPack("55030F33323535375F4B657361722E6C7561")
                    SendPack("55000F33323535375F4B657361722E6C7561FF")
                    SendPack("640C0D050000")

                Case 19
                    tpCharGo(CZGateX, CZGateY)

                Case 20 'Go Maradon
                    SendPack("4BB40FCA1B")

                Case 21
                    tpCharGo(755, 771)

                Case 22 'Kutu kırdır
                    SendPack("2001" & findIDtoNPC("Moira") & "FFFFFFFF")
                    SendPack("640714050000")
                    SendPack("55000F31363034375F4D6F6972612E6C7561FF")

            End Select

        End If


        If (cmbQuest.SelectedIndex < cmbQuest.Items.Count - 1) Then
            cmbQuest.SelectedIndex += 1
        Else
            cmbQuest.SelectedIndex = 0
        End If
    End Sub
#End Region
#Region "######## Ekstra ########"
    Public Sub UseMask()
        If (checkSkillID(159) = True) Then
            SendPack("3106AF7A0700" & getCharID() & getCharID() & "000000000000000000000000")
        Else
            If (getItemSlotInv(900434000) > 0) Then
                SendPack("3101AF7A0700" & getCharID() & getCharID() & "00000000000000000000000000000A00")
                SendPack("3103AF7A0700" & getCharID() & getCharID() & "740001009300000000000000")
            End If
        End If
    End Sub
    Public Sub UseHallowenTs()
        If (checkSkillID(157) = True) Then
            SendPack("3106AD7A0700" & getCharID() & getCharID() & "000000000000000000000000")
        Else
            If (getItemSlotInv(900433000) > 0) Then
                SendPack("3103AD7A0700" & getCharID() & getCharID() & "00000000000000000000000000000000")
            End If
        End If
    End Sub
    Public Sub TakeHallowenQuest()
        If (getItemSlotInv(900433000) <= 0 And getItemSlotInv(900434000) <= 0) Then
            SendPack("2001" & findIDtoNPC("Jack O Lantern") & "FFFFFFFF")
            SendPack("55000E32393138305F4A61636B2E6C7561")
            SendPack("55000E32393138305F4A61636B2E6C7561")
            SendPack("55000E32393138305F4A61636B2E6C7561")
            Wait(100)
            SendPack("2001" & findIDtoNPC("Jack O Lantern") & "FFFFFFFF")
            SendPack("55000E32393138305F4A61636B2E6C7561")
            SendPack("55000E32393138305F4A61636B2E6C7561")
        End If
    End Sub
    Public Sub TakeHallowenSpear()
        If (getItemIDCountInv(389125000) >= 10) Then
            SendPack("2001" & findIDtoNPC("Jack O Lantern") & "FFFFFFFF")
            SendPack("55020E32393138305F4A61636B2E6C7561")
            SendPack("55010E32393138305F4A61636B2E6C7561")
            SendPack("55000E32393138305F4A61636B2E6C7561")
        End If
    End Sub
    Public Sub TakeHallowenCane()
        If (getItemIDCountInv(389124000) >= 10) Then
            SendPack("2001" & findIDtoNPC("Jack O Lantern") & "FFFFFFFF")
            SendPack("55020E32393138305F4A61636B2E6C7561")
            SendPack("55000E32393138305F4A61636B2E6C7561")
            SendPack("55000E32393138305F4A61636B2E6C7561")
        End If
    End Sub
    Public Sub GenieStatu(ByVal statu As Boolean)
        If (statu) Then
            SendPack("970104")
        Else
            SendPack("970105")
        End If
    End Sub
#End Region
End Class
