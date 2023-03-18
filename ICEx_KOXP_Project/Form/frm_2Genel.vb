Public Class frm_2Genel
#Region "Form Code"
    Private mobTimer As Integer = 0
    Private gmTimer As Integer = 0
    Private dcTimer As Integer = 0
    Private StartDate As DateTime
    Private Sub m_TopMost_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_TopMost.CheckedChanged
        TopMost = m_TopMost.Checked
    End Sub
    Private Sub m_TopMost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_TopMost.Click
        If m_TopMost.Checked = False Then
            m_TopMost.Checked = True
        Else
            m_TopMost.Checked = False
        End If
    End Sub
    Private Sub m_Hide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_Hide.Click
        N_icon.Visible = True
        N_icon.ShowBalloonTip(0, strProjectInfo, strHideBot, ToolTipIcon.Info)
        Me.Visible = False
    End Sub
    Private Sub N_icon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles N_icon.MouseDoubleClick
        N_icon.Visible = False
        Me.Visible = True
    End Sub
    Private Sub m_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_Close.Click
        End
    End Sub
    Private Sub frm_2Genel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        setOnlineUser(userServerId, 0)
        End
    End Sub
    Private Sub m_RecvHook_Click(sender As Object, e As EventArgs) Handles m_RecvHook.Click
        frm_3Recv.Show()
    End Sub
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Dim scr = Screen.FromPoint(Me.Location)
        Me.Location = New Point(scr.WorkingArea.Right - Me.Width, scr.WorkingArea.Top)
        MyBase.OnLoad(e)
    End Sub
    Private Sub frm_2Genel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StartDate = DateTime.Now
        Me.Text = RandomString()
        Cli.addMethodBlue(cmb_blueQuest)
        readBot()
        If (ServerStatus = False) Then End
        btn_TmrSET_Click(sender, e)
    End Sub
    Private Sub m_SaveSettings_Click(sender As Object, e As EventArgs) Handles m_SaveSettings.Click
        saveBot()
    End Sub
#End Region
#Region "Atak Timers"
    Private Sub tmr_WarriorAttack_Tick(sender As Object, e As EventArgs) Handles tmr_WarriorAttack.Tick
        If (Cli.getCharClassName = "Warrior" And BotStatus = True And AttackStatus = True) Then Cli.useSkillWarrior()
    End Sub
    Private Sub tmr_ArcheryAttack_Tick(sender As Object, e As EventArgs) Handles tmr_ArcheryAttack.Tick
        With Cli
            If .getCharClassName = "Rogue" And .CharRogueJob = False And BotStatus = True And AttackStatus = True Then
                If (lstv_AttackRog.Items().Item(51).Checked = True Or lstv_AttackRog.Items().Item(52).Checked = True) Then
                    If lstv_AttackRog.Items().Item(51).Checked = True Then .OkYagmuru(0)
                    If lstv_AttackRog.Items().Item(52).Checked = True Then .OkYagmuru(1)
                Else
                    .useAttackArchery()
                End If
            End If
        End With
    End Sub
    Private Sub tmr_AssaianAttack_Tick(sender As Object, e As EventArgs) Handles tmr_AssaianAttack.Tick
        If (Cli.getCharClassName = "Rogue" And Cli.CharRogueJob = True And BotStatus = True And AttackStatus = True) Then Cli.useAssaianSkill()
    End Sub
    Private Sub tmr_MageAttack_Tick(sender As Object, e As EventArgs) Handles tmr_MageAttack.Tick
        If (Cli.getCharClassName = "Mage" And BotStatus = True And AttackStatus = True) Then Cli.useMageSkill()
    End Sub

    Private Sub tmr_PriestAttack_Tick(sender As Object, e As EventArgs) Handles tmr_PriestAttack.Tick
        If (Cli.getCharClassName = "Priest" And BotStatus = True And AttackStatus = True) Then Cli.usePriestSkill()
    End Sub
    Private Sub btn_AttackList_Click(sender As Object, e As EventArgs) Handles btn_AttackList.Click
        tb_Project.SelectedTab = Extra
    End Sub
    Private Sub btn_mobCenter_Click(sender As Object, e As EventArgs) Handles btn_mobCenter.Click
        lbl_centX.Text = Cli.getCharX()
        lbl_centY.Text = Cli.getCharY()
        lbl_MerZ.Text = Cli.getCharZ()
    End Sub
    Private Sub btn_TmrSET_Click(sender As Object, e As EventArgs) Handles btn_TmrSET.Click
        'Attack Timers
        tmr_WarriorAttack.Interval = CInt(txt_tmrWarrior.Text)
        tmr_AssaianAttack.Interval = CInt(txt_tmrAssassin.Text)
        tmr_MageAttack.Interval = CInt(txt_tmrMage.Text)
        tmr_ArcheryAttack.Interval = CInt(txt_tmrArchery.Text)
        tmr_PriestAttack.Interval = CInt(txt_tmrPriest.Text)
        Cli.MobDissAttack = CInt(txt_MobAttackDistance.Text)
        Cli.ArchDissAttack = CInt(txt_MultipleDis.Text)
    End Sub
    Private Sub btn_msgSpeed_Click(sender As Object, e As EventArgs) Handles btn_msgSpeed.Click
        Dim msgSpeed As String
        msgSpeed = strIdeaAttackSpeed & vbNewLine _
            & "Okçu : 1350" & vbNewLine _
            & "Warrior : 800" & vbNewLine _
            & "Asas : 950" & vbNewLine _
            & "Priest : 970" & vbNewLine _
            & "Mage İdeal : 970" & vbNewLine _
            & "Mage 18 : 990" & vbNewLine _
            & "Atak mesafesi : Karakterin atak yapacağı minimum mesafeyi belirler." & vbNewLine _
            & "Ok mesafe : 3'lü, 4'lü, 5'li, 6'lı, 8'li ve 9'lu atağın mesafesini belirler." & vbNewLine _
            & "Onayla işleminden sonra devreye girecektir."
        MessageBox.Show(msgSpeed, strProjectInfo, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub chk_normalAttack_CheckedChanged(sender As Object, e As EventArgs) Handles chk_normalAttack.CheckedChanged
        Cli.NormalAttack = chk_normalAttack.Checked
        If (Cli.NormalAttack) Then
            txt_tmrArchery.Text = 1600
            txt_tmrAssassin.Text = 1100
        Else
            txt_tmrArchery.Text = 1350
            txt_tmrAssassin.Text = 800
        End If
        btn_TmrSET_Click(sender, e)
    End Sub
#End Region
#Region "Bot Status"
    Private Sub btn_ProjectActive_Click(sender As Object, e As EventArgs) Handles btn_ProjectActive.Click
        If (btn_ProjectActive.Text = "Aktif") Then
            btn_ProjectActive.Text = "Pasif"
            BotStatus = True
        Else
            btn_ProjectActive.Text = "Aktif"
            BotStatus = False
            If (btn_ProjectStart.Text = "Durdur") Then
                AttackStatus = False
                btn_ProjectStart.Text = "Başlat"
            End If
        End If
    End Sub

    Private Sub btn_ProjectStart_Click(sender As Object, e As EventArgs) Handles btn_ProjectStart.Click
        Cli.WaitAttack = 0
        btn_mobCenter_Click(sender, e)
        lbl_xCoorLoot.Text = Cli.getCharX()
        lbl_yCoorLoot.Text = Cli.getCharY()
        If (btn_ProjectStart.Text = "Başlat") Then
            btn_ProjectStart.Text = "Durdur"
            AttackStatus = True
            If (btn_ProjectActive.Text = "Aktif") Then
                btn_ProjectActive.Text = "Pasif"
                BotStatus = True
            End If
        Else
            btn_ProjectStart.Text = "Başlat"
            AttackStatus = False
        End If
        Cli.SetCharSpeed(True)
        If (chk_GenieStatu.Checked) Then Cli.GenieStatu(AttackStatus)
    End Sub
    Private Sub lbl_charZone_TextChanged(sender As Object, e As EventArgs) Handles lbl_charZone.TextChanged
        If (btn_ProjectActive.Text = "Pasif") Then
            btn_ProjectActive.Text = "Aktif"
            BotStatus = False
            If (btn_ProjectStart.Text = "Durdur") Then
                AttackStatus = False
                btn_ProjectStart.Text = "Başlat"
            End If
        End If
    End Sub
#End Region
#Region "GM Checker"
    Private Sub btn_checkerPlayer_Click(sender As Object, e As EventArgs) Handles btn_checkerPlayer.Click
        Cli.getAreaPeople(lst_checkerGM, "Player")
    End Sub
    Private Sub btn_checkerTarget_Click(sender As Object, e As EventArgs) Handles btn_checkerTarget.Click
        If (searchList(Cli.getbMobName, lst_checkerGM) = False And Cli.getbMobName <> "NuLL") Then
            lst_checkerGM.Items.Add(Cli.getbMobName)
        End If
    End Sub
    Private Sub btn_checkerClear_Click(sender As Object, e As EventArgs) Handles btn_checkerClear.Click
        lst_checkerGM.Items.Clear()
    End Sub
    Private Sub btn_checkerAdd_Click(sender As Object, e As EventArgs) Handles btn_checkerAdd.Click
        If (searchList(txt_checkerNick.Text, lst_checkerGM) = False) Then
            lst_checkerGM.Items.Add(txt_checkerNick.Text)
        End If
    End Sub
    Private Sub lst_checkerGM_DoubleClick(sender As Object, e As EventArgs) Handles lst_checkerGM.DoubleClick
        If (lst_checkerGM.Items.Count > 0) Then lst_checkerGM.Items.RemoveAt(lst_checkerGM.SelectedIndex)
    End Sub
    Private Sub lbl_GmStatu_TextChanged(sender As Object, e As EventArgs) Handles lbl_GmStatu.TextChanged
        If lbl_GmStatu.Text = "0" Or lbl_GmStatu.Text = "2" Then Exit Sub
        If (btn_ProjectActive.Text = "Aktif") Then
            btn_ProjectActive_Click(sender, e)
        End If
        With Cli
            Select Case cmb_checkerFunc.SelectedIndex
                Case 0
                    .useTown()
                    lbl_GmStatu.Text = "0"
                    chk_checkerGM.Checked = False
                    goTxt(strFoundGm, strGmCheckerActive)
                Case 1
                    If (Cli.CharIsTown() = False) Then
                        .useTown()
                    End If

                Case 2
                    ShutDown(.Ko_Pid)
                    lbl_GmStatu.Text = "0"
                    chk_checkerGM.Checked = False
                    goTxt(strFoundGm, strGmCheckerActive)
            End Select
        End With
    End Sub
    Private Sub cmb_checkerFunc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_checkerFunc.SelectedIndexChanged
        If (chk_checkerGM.Checked And cmb_checkerFunc.SelectedIndex = 1) Then
            If (lst_rprCoor.Items.Count <= 0) Then
                goTxt(strPleaseAddCoorRpr, "")
                cmb_checkerFunc.SelectedIndex = 0
                chk_rprIsActive.Checked = False
            End If

            If lst_rprCoor.Items(lst_rprCoor.Items.Count - 1).ToString().IndexOf("O - Repair") < 0 Then
                goTxt(strRepairOk, strHowToRepairOk)
                cmb_checkerFunc.SelectedIndex = 0
                chk_rprIsActive.Checked = False
            Else
                Cli.RepairActive = False
                lbl_rprAttack.Text = "NULL"
                lbl_RprBuy.Text = "NULL"
            End If
        End If
    End Sub
#End Region
#Region "Atak Listesi"
    Private Sub btn_LstMobAdd_Click(sender As Object, e As EventArgs) Handles btn_LstMobAdd.Click
        Cli.getAreaPeople(lst_AttackList)
    End Sub
    Private Sub btn_LstMobSelected_Click(sender As Object, e As EventArgs) Handles btn_LstMobSelected.Click
        If (searchList(Cli.getbMobName, lst_AttackList) = False) Then
            lst_AttackList.Items.Add(Cli.getbMobName)
        End If
    End Sub
    Private Sub btn_LstMobPlayer_Click(sender As Object, e As EventArgs) Handles btn_LstMobPlayer.Click
        Cli.getAreaPeople(lst_AttackList, "Karşı")
    End Sub
    Private Sub btn_LstMobClear_Click(sender As Object, e As EventArgs) Handles btn_LstMobClear.Click
        lst_AttackList.Items.Clear()
    End Sub
    Private Sub lst_AttackList_DoubleClick(sender As Object, e As EventArgs) Handles lst_AttackList.DoubleClick
        If (CInt(lst_AttackList.Items.Count) > 0 And lst_AttackList.SelectedIndex > -1) Then
            lst_AttackList.Items.RemoveAt(lst_AttackList.SelectedIndex)
        End If
    End Sub
    Private Sub btn_LstMobNick_Click(sender As Object, e As EventArgs) Handles btn_LstMobNick.Click
        If (searchList(txt_LstMobName.Text, lst_AttackList) = False) Then
            lst_AttackList.Items.Add(txt_LstMobName.Text)
        End If
    End Sub
#End Region
#Region "Chat"
    Private Sub tmr_Chat_Tick(sender As Object, e As EventArgs) Handles tmr_Chat.Tick
        If (BotStatus = False) Then Exit Sub
        Cli.writeChat(txt_ChatText.Text, cmb_ChatStatus.SelectedIndex)
    End Sub

    Private Sub chk_ChatAktif_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ChatActive.CheckedChanged
        If chk_ChatActive.Checked Then
            tmr_Chat.Interval = CInt(txt_ChatSec.Text) * 1000
            tmr_Chat.Enabled = True
        Else
            tmr_Chat.Enabled = False
        End If
    End Sub

    Private Sub btn_ChatPlayer_Click(sender As Object, e As EventArgs) Handles btn_ChatPlayer.Click
        Cli.getAreaPeople(, "PM", CBool(chk_PMJobLevel.Checked), txt_ChatText.Text, CInt(txt_ChatLevel.Text), cmb_ChatJob.Text)
    End Sub

    Private Sub txt_ChatSec_TextChanged(sender As Object, e As EventArgs) Handles txt_ChatSec.TextChanged
        checkTextBox(txt_PartyGHealRow, 1)
        If chk_ChatActive.Checked = True Then chk_ChatActive.Checked = False
        If (CInt(txt_ChatSec.Text) < 1 Or CInt(txt_ChatSec.Text) > 60) Then txt_ChatSec.Text = 1
    End Sub
#End Region
#Region "Party Timers"
    Private Sub tmr_Party_Tick(sender As Object, e As EventArgs) Handles tmr_Party.Tick
        If (BotStatus = False) Then Exit Sub
        With Cli
            If .getCharClassName = "Priest" Then
                If (Cli.getPartyCount() > 0) Then
                    '// Party Priest
                    .partyBuffControl(cmb_PartyBuff.SelectedIndex, cmb_PartyAcc.SelectedIndex, cmb_PartyResistans.SelectedIndex, CBool(chk_PartySTR.Checked))
                    .partyGroupHeal(CInt(txt_PartyGroupHealPoint.Text), CInt(txt_PartyGHealRow.Text), CBool(chk_PartyGrupHeal.Checked))
                    .partyRestore(cmb_PartyRestore.SelectedIndex)
                Else
                    '// Client Priest
                    BuffKontrol(cmb_PriestBuff.SelectedIndex)
                    AccKontrol(cmb_PriestAcc.SelectedIndex)
                    ResistKontrol(cmb_PriestResis.SelectedIndex)
                    RestoreKontrol(cmb_PriestRestore.SelectedIndex)
                    StrKontrol(CBool(chk_PriestStr.Checked))
                    CureKontrol(CBool(chk_PriestCure.Checked))
                End If
            End If
        End With
    End Sub
    Private Sub tmr_HealControl_Tick(sender As Object, e As EventArgs) Handles tmr_HealControl.Tick
        If (BotStatus = False) Then Exit Sub
        With Cli
            If .getCharClassName = "Priest" Then
                If (Cli.getPartyCount() > 0) Then
                    Cli.partyHealControl(cmb_PartyHeal.SelectedIndex, Convert.ToInt32(txt_PartyHeal.Text))
                Else
                    PriestHealKontrol(cmb_PriestHeal.SelectedIndex, CInt(txt_PriestHeal.Text))
                End If
            End If
            If .getCharClassName = "Rogue" And (Cli.getPartyCount() > 0) Then
                .partyMinor(CBool(chk_PartyMinor.Checked), CInt(txt_MinorPtRow.Text))
            End If
        End With
    End Sub
#End Region
#Region "Repair"
    Private Sub btn_SundriesAction_Click(sender As Object, e As EventArgs) Handles btn_SundriesAction.Click
        With Cli
            If chk_RprActive.Checked Then
                .itemRepair(CBool(chk_RprHelmet.Checked), CBool(chk_RprPauldron.Checked), CBool(chk_RprWeapon.Checked), CBool(chk_RprPads.Checked), CBool(chk_RprBoots.Checked), CBool(chk_RprGauntlet.Checked))
            End If
            .sundriesNPC(CBool(chk_RprARROW.Checked), CInt(txt_RprARROW.Text), CBool(chk_RprWOLF.Checked), CInt(txt_RprWOLF.Text), CBool(chk_RprPOG.Checked), CInt(txt_RprPOG.Text), CBool(chk_RprTS.Checked), CInt(txt_RprTS.Text))
        End With
    End Sub
    Private Sub btn_PotionAction_Click(sender As Object, e As EventArgs) Handles btn_PotionAction.Click
        With Cli
            .potionNPC(cmb_rprHPPot.SelectedIndex, CInt(txt_RprHPPot.Text), cmb_rprMPPot.SelectedIndex, CInt(txt_RprMPPot.Text), CBool(chk_RprHP.Checked), CBool(chk_RprMP.Checked))
        End With
    End Sub
    Private Sub lst_rprCoor_DoubleClick(sender As Object, e As EventArgs) Handles lst_rprCoor.DoubleClick
        If (CInt(lst_rprCoor.Items.Count) > 0 And lst_rprCoor.SelectedIndex > -1) Then
            lst_rprCoor.Items.RemoveAt(lst_rprCoor.SelectedIndex)
        End If
    End Sub
    Private Sub btn_rprCoorAdd_Click(sender As Object, e As EventArgs) Handles btn_rprCoorAdd.Click
        lst_rprCoor.Items.Add(Cli.getCharX & " - " & Cli.getCharY)
    End Sub
    Private Sub btn_rprAddMethod_Click(sender As Object, e As EventArgs) Handles btn_rprAddMethod.Click
        Cli.addRprMethod(cmb_rprMethod, lst_rprCoor)
    End Sub
    Private Sub btn_rprRefMethod_Click(sender As Object, e As EventArgs) Handles btn_rprRefMethod.Click
        Cli.refreshMethod(cmb_rprMethod)
    End Sub
    Private Sub btn_ClearList_Click(sender As Object, e As EventArgs) Handles btn_ClearList.Click
        Dim result As Integer = MessageBox.Show(strClearRprList, "[CS - Project] | " & gb_coorRpr.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If (result = DialogResult.Yes) Then lst_rprCoor.Items.Clear()
    End Sub
    Private Sub lbl_RprBuy_TextChanged(sender As Object, e As EventArgs) Handles lbl_RprBuy.TextChanged
        Select Case lbl_RprBuy.Text
            Case "S"
                btn_SundriesAction_Click(sender, e)
            Case "I"
                If (chk_sellItem.Checked) Then Cli.sellItem(lstv_UniqueItem)
            Case "P"
                btn_PotionAction_Click(sender, e)
            Case "D"
                btn_DcSundriesAction_Click(sender, e)
            Case "L"
                Cli.PetStatu(True)
        End Select
        If (lbl_RprBuy.Text <> "NULL") Then lst_rprCoor.SelectedIndex += 1
    End Sub
    Private Sub lbl_rprAttack_TextChanged(sender As Object, e As EventArgs) Handles lbl_rprAttack.TextChanged
        If (lbl_rprAttack.Text = "OFF") Then
            btn_ProjectStart.Text = "Başlat"
            AttackStatus = False
        ElseIf (lbl_rprAttack.Text = "ON") Then
            btn_ProjectStart.Text = "Durdur"
            AttackStatus = True
            Cli.rprAttackTime = 6
        End If
        If (lbl_rprAttack.Text = "OFF" Or lbl_rprAttack.Text = "ON") Then
            If (chk_GenieStatu.Checked) Then Cli.GenieStatu(AttackStatus)
        End If
    End Sub
    Private Sub chk_rprIsActive_CheckedChanged(sender As Object, e As EventArgs) Handles chk_rprIsActive.CheckedChanged

        If (chk_UpIsActive.Checked) Then
            goTxt(strOnlyRepair, strCancelUpgrade)
            chk_UpIsActive.Checked = False
        End If

        If (lst_rprCoor.Items.Count <= 0) Then
            goTxt(strPleaseAddCoorRpr, "")
            chk_rprIsActive.Checked = False
            Exit Sub
        End If

        If lst_rprCoor.Items(lst_rprCoor.Items.Count - 1).ToString().IndexOf("O - Repair") < 0 Then
            goTxt(strRepairOk, strHowToRepairOk)
            chk_rprIsActive.Checked = False
        Else
            Cli.RepairActive = False
            lbl_rprAttack.Text = "NULL"
            lbl_RprBuy.Text = "NULL"
        End If
    End Sub
    Private Sub btn_UniqueItem_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub btn_SellItem_Click(sender As Object, e As EventArgs) Handles btn_SellItem.Click
        If (lstv_UniqueItem.Items.Count <= 0) Then
            goTxt(strUniqueItem, strHowToAddUnique)
            Return
        End If
        Cli.sellItem(lstv_UniqueItem)
    End Sub
    Private Sub chk_InvFull_CheckedChanged(sender As Object, e As EventArgs) Handles chk_InvFull.CheckedChanged
        If (chk_InvFull.Checked) And (lstv_UniqueItem.Items.Count <= 0) Then
            goTxt(strUniqueItem, strHowToAddUnique)
            chk_InvFull.Checked = False
            Return
        End If
    End Sub
#End Region
#Region "Upgrade Bot"
    Private Sub chkl_UpItems_DoubleClick(sender As Object, e As EventArgs) Handles chkl_UpItems.DoubleClick
        chkl_UpItems.Items.Item(chkl_UpItems.SelectedIndex) = "(" & chkl_UpItems.SelectedIndex + 1 & ")"
    End Sub

    Private Sub lst_buyItems_DoubleClick(sender As Object, e As EventArgs) Handles lst_buyItems.DoubleClick
        For i As Integer = 0 To 26
            setItems(lst_buyItems.Text, chkl_UpItems)
        Next
    End Sub

    Private Sub cmb_JobItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JobItem.SelectedIndexChanged
        getItems(cmb_JobItem.Text, lst_buyItems)
    End Sub

    Private Sub chk_upAllCheck_CheckedChanged(sender As Object, e As EventArgs) Handles chk_upAllCheck.CheckedChanged
        For i As Integer = 0 To 26
            chkl_UpItems.SetItemChecked(i, chk_upAllCheck.Checked)
        Next
    End Sub

    Private Sub chkl_UpItems_Click(sender As Object, e As EventArgs) Handles chkl_UpItems.Click
        If (chkl_UpItems.GetItemChecked(chkl_UpItems.SelectedIndex) = True) Then
            chkl_UpItems.SetItemChecked(chkl_UpItems.SelectedIndex, False)
        Else
            chkl_UpItems.SetItemChecked(chkl_UpItems.SelectedIndex, True)
        End If
    End Sub

    Private Sub lst_buyItems_Click(sender As Object, e As EventArgs) Handles lst_buyItems.Click
        setItems(lst_buyItems.Text, chkl_UpItems)
    End Sub

    Private Sub btn_upLstClear_Click(sender As Object, e As EventArgs) Handles btn_upLstClear.Click
        For i As Integer = 0 To 26
            chkl_UpItems.Items.Item(i) = "(" & (i + 1).ToString() & ")"
        Next
    End Sub

    Private Sub tmr_UpBot_Tick(sender As Object, e As EventArgs) Handles tmr_UpBot.Tick
        If (chk_UpIsActive.Checked = False Or BotStatus = False) Then Exit Sub

        If (Cli.upBotActive = True) Then
            If (Convert.ToInt32(lbl_TotalUpPrice.Text.Replace(".", "")) > Cli.getCharGold()) Then
                goTxt(strUpgradeCoin, strGetUpgradeCoin)
                chk_UpIsActive.Checked = False
            End If

            Cli.upBotText = lst_UpBotMethod.Text
            Select Case Cli.upBotText.Substring(0, 1)
                Case "G"
                    lbl_UpMethod.Text = "G"
                Case "S"
                    lbl_UpMethod.Text = "S"
                Case "W"
                    lbl_UpMethod.Text = "W"
                Case "U"
                    lbl_UpMethod.Text = "U"
                Case "I"
                    lbl_UpMethod.Text = "I"
                Case "O"
                    lbl_UpMethod.Text = "O"
                Case Else
                    Cli.goKorText(lst_UpBotMethod.Text, False)
                    If (Cli.getCharX() = getCoorText(Cli.upBotText, 0) And Cli.getCharY() = getCoorText(Cli.upBotText, 1)) Then
                        lst_UpBotMethod.SelectedIndex += 1
                    End If
            End Select
            '// SC Count check
            Cli.totalSC = 0
            For i As Integer = 0 To 26
                If (chkl_UpItems.GetItemChecked(i) = True) Then
                    Cli.totalSC += 1
                End If
            Next
        End If
    End Sub

    Private Sub lbl_UpMethod_TextChanged(sender As Object, e As EventArgs) Handles lbl_UpMethod.TextChanged
        Select Case lbl_UpMethod.Text
            Case "G"
                Cli.useTown()
            Case "S"
                Cli.buyUpScroll(cmb_UpScrool, Cli.totalSC)
            Case "W"
                Cli.compBuyUpItem(chkl_UpItems)
            Case "U"
                Cli.upgradeSlot(chkl_UpItems, chk_Up7.Checked)
                If (Cli.getExItemLimit(txt_ExtRow.Text) = False) Then
                    lbl_UpMethod.Text = "O"
                End If
            Case "I"
                Cli.sendItemsToBank(chkl_UpItems, Convert.ToInt32(txt_ExtRow.Text), lst_UpBotMethod)
            Case "O"
                ShutDown(Cli.Ko_Pid)
                lst_UpBotMethod.SelectedIndex = 0
                autoLoginTime = GetTickCount()

                'If (BotForSteam = True) Then
                '    frm_1Giris.tmr_SteamautoLogin.Enabled = True
                'Else
                '    frm_1Giris.tmr_UskoAutoLogin.Enabled = True
                'End If

                'If (frm_1Giris.chk_LoginedSteam.Checked) Then
                '    autoLoginRow = 2
                'Else
                '    autoLoginRow = 0
                'End If

                Cli.upBotActive = False
        End Select
        If (lbl_UpMethod.Text <> "NULL" And lbl_UpMethod.Text <> "I") Then lst_UpBotMethod.SelectedIndex += 1
    End Sub
    Private Sub chk_UpIsActive_CheckedChanged(sender As Object, e As EventArgs) Handles chk_UpIsActive.CheckedChanged
        If (chk_rprIsActive.Checked) Then
            goTxt(strOnlyUpgrade, strCancelRepair)
            chk_rprIsActive.Checked = False
            Exit Sub
        End If
        'If (autoLoginPath = "" And frm_1Giris.chk_AutoLogin.Checked = False) Then
        '    goTxt(strInactiveAutoLogin, strHowToActiveAutoLogin)
        '    chk_UpIsActive.Checked = False
        '    Exit Sub
        'End If
    End Sub
    Private Sub btn_AutoUpActive_Click(sender As Object, e As EventArgs) Handles btn_AutoUpActive.Click
        Cli.upBotActive = True
        lst_UpBotMethod.SelectedIndex = 0
        chk_UpIsActive.Checked = True

        'If (autoLoginPath = "" And frm_1Giris.chk_AutoLogin.Checked = False) And (chk_rprIsActive.Checked = False) Then
        '    goTxt(strInactiveAutoLogin, strHowToActiveAutoLogin)
        '    chk_UpIsActive.Checked = False
        'Else
        '    Cli.upBotActive = True
        '    lst_UpBotMethod.SelectedIndex = 0
        'End If
    End Sub
    Private Sub btn_setUpgrade_Click(sender As Object, e As EventArgs) Handles btn_setUpgrade.Click
        If (Cli.getItemID(KO_OFF_INVCOUNT) <= 0) Then
            goTxt("Upgrade işlemi gerçekleştirilemedi.", "Lütfen son slota scroll ekleyiniz.")
            Exit Sub
        End If
        Cli.upgradeAll()
    End Sub
#End Region
#Region "Auto Loot"
    Private Sub chk_runBox_CheckedChanged(sender As Object, e As EventArgs) Handles chk_runBox.CheckedChanged
        lst_BoxCoor.Items.Clear()
        lst_BoxPacket.Items.Clear()
        lbl_xCoorLoot.Text = Cli.getCharX()
        lbl_yCoorLoot.Text = Cli.getCharY()
        If (chk_runMob.Checked) Then chk_runMob.Checked = False
    End Sub
    Private Sub chk_runMob_CheckedChanged(sender As Object, e As EventArgs) Handles chk_runMob.CheckedChanged
        If (chk_runBox.Checked) Then chk_runBox.Checked = False
    End Sub
    Private Sub rb_justMoney_CheckedChanged(sender As Object, e As EventArgs) Handles rb_justMoney.CheckedChanged
        If (rb_justMoney.Checked) Then lbl_LootStatus.Text = 0
    End Sub
    Private Sub rb_allLoot_CheckedChanged(sender As Object, e As EventArgs) Handles rb_allLoot.CheckedChanged
        If (rb_allLoot.Checked) Then lbl_LootStatus.Text = 1
    End Sub
    Private Sub rb_ListItem_CheckedChanged(sender As Object, e As EventArgs) Handles rb_ListItem.CheckedChanged
        If (rb_ListItem.Checked) Then lbl_LootStatus.Text = 2
    End Sub
    Private Sub rb_Just8_CheckedChanged(sender As Object, e As EventArgs) Handles rb_Just8.CheckedChanged
        If (rb_Just8.Checked) Then lbl_LootStatus.Text = 3
    End Sub
    Private Sub lstv_LootItem_DoubleClick(sender As Object, e As EventArgs) Handles lstv_UniqueItem.DoubleClick
        If (lstv_UniqueItem.FocusedItem IsNot Nothing) Then
            lstv_UniqueItem.Items.RemoveAt(lstv_UniqueItem.FocusedItem.Index)
        End If
    End Sub
    Private Sub btn_getInven_Click(sender As Object, e As EventArgs) Handles btn_getInven.Click
        lstv_UniqueItem.Items.Clear()
        For i As Integer = 0 To 27
            If Cli.getItemIDInv(i) > 0 Then
                Dim lstItem As ListViewItem = New ListViewItem(New String() {Cli.getItemIDInv(i), ItemNameFind(Cli.getItemIDInv(i).ToString().Substring(0, 6) & "000")})
                lstv_UniqueItem.Items.Add(lstItem)
            End If
        Next
    End Sub
    Private Sub btn_getInvenGarbage_Click(sender As Object, e As EventArgs) Handles btn_getInvenGarbage.Click
        lstv_GarbageItem.Items.Clear()
        For i As Integer = 0 To 27
            If Cli.getItemIDInv(i) > 0 Then
                Dim lstItem As ListViewItem = New ListViewItem(New String() {Cli.getItemIDInv(i), ItemNameFind(Cli.getItemIDInv(i).ToString().Substring(0, 6) & "000")})
                lstv_GarbageItem.Items.Add(lstItem)
            End If
        Next
    End Sub
    Private Sub lstv_GarbageItem_DoubleClick(sender As Object, e As EventArgs) Handles lstv_GarbageItem.DoubleClick
        If (lstv_GarbageItem.FocusedItem IsNot Nothing) Then
            lstv_GarbageItem.Items.RemoveAt(lstv_GarbageItem.FocusedItem.Index)
        End If
    End Sub
#End Region
#Region "Extra"
    Private Sub btn_GorevAl_Click(sender As Object, e As EventArgs) Handles btn_GorevAl.Click
        Cli.questTake(cmb_Quests.Text, 1)
    End Sub

    Private Sub btn_GorevVer_Click(sender As Object, e As EventArgs) Handles btn_GorevVer.Click
        Cli.questCompleted(cmb_Quests.Text)
    End Sub

    Private Sub btn_GorevSil_Click(sender As Object, e As EventArgs) Handles btn_GorevSil.Click
        Cli.questTake(cmb_Quests.Text, 0)
    End Sub

    Private Sub btn_DaggerAl_Click(sender As Object, e As EventArgs) Handles btn_DaggerAl.Click
        Cli.buyDagger()
    End Sub

    Private Sub btn_DaggerBas_Click(sender As Object, e As EventArgs) Handles btn_DaggerBas.Click
        Cli.upDagger()
    End Sub

    Private Sub btn_MsTopla_Click(sender As Object, e As EventArgs) Handles btn_MsTopla.Click
        Cli.getMonsterStonr()
    End Sub
    Private Sub btn_tpCoor_Click(sender As Object, e As EventArgs) Handles btn_tpCoor.Click
        If (String.IsNullOrEmpty(txt_tpX.Text) Or String.IsNullOrEmpty(txt_tpY.Text)) Then
            goTxt(strPleaseUseCoor, strIfCoor)
            Exit Sub
        End If
        Cli.tpCharGo(Convert.ToSingle(txt_tpX.Text), Convert.ToSingle(txt_tpY.Text))
    End Sub
    Private Sub btn_useWitch_Click(sender As Object, e As EventArgs) Handles btn_useWitch.Click
        Cli.useItem(700039000, 1)
    End Sub
    Private Sub btn_AddCoorArea_Click(sender As Object, e As EventArgs) Handles btn_AddCoorArea.Click
        lst_walkArea.Items.Add(Cli.getCharX & " - " & Cli.getCharY)
    End Sub

    Private Sub chk_walkArea_CheckedChanged(sender As Object, e As EventArgs) Handles chk_walkArea.CheckedChanged
        If (chk_runMob.Checked) Then
            chk_runMob.Checked = False
            goTxt(strWalkArea, strCloseRunMob)
            Exit Sub
        End If
        If (chk_walkArea.Checked And lst_walkArea.Items.Count > 0) Then
            lst_walkArea.SelectedIndex = 0
        Else
            chk_walkArea.Checked = False
        End If
    End Sub

    Private Sub lst_walkArea_DoubleClick(sender As Object, e As EventArgs) Handles lst_walkArea.DoubleClick
        If (chk_walkArea.Checked = False) Then
            If (lst_walkArea.SelectedIndex > -1) Then
                lst_walkArea.Items.RemoveAt(lst_walkArea.SelectedIndex)
            End If
        End If
    End Sub
    Private Sub btn_WingsEmblem_Click(sender As Object, e As EventArgs) Handles btn_WingsEmblem.Click
        If (cmb_WingsEmblem.SelectedIndex < 0) Then cmb_WingsEmblem.SelectedIndex = 0
        Select Case cmb_WingsEmblem.SelectedIndex
            Case 0 : Cli.useItem(508220000, 3)
            Case 1 : Cli.useItem(810178000, 3)
            Case 2 : Cli.useItem(810179000, 3)
            Case 3 : Cli.useItem(810213000, 3)
            Case 4 : Cli.useItem(900028000, 3)
            Case 5 : Cli.useItem(900030000, 3)
            Case 6 : Cli.useItem(900177000, 3)
            Case 7 : Cli.useItem(900178000, 3)
            Case 8 : Cli.useItem(900179000, 3)
            Case 9 : Cli.useItem(900180000, 3)
            Case 10 : Cli.useItem(900181000, 3)
            Case 11 : Cli.useItem(900182000, 3)
            Case 12 : Cli.useItem(900183000, 3)
            Case 13 : Cli.useItem(900452000, 3)
            Case 14 : Cli.useItem(900453000, 3)
            Case 15 : Cli.useItem(910248000, 3)
            Case 16 : Cli.useItem(900701000, 3)
            Case 17 : Cli.useItem(900702000, 3)
            Case 18 : Cli.useItem(810263000, 3)
            Case 19 : Cli.useItem(900767000, 3)
            Case 20 : Cli.useItem(900768000, 3)
            Case 21 : Cli.useItem(810638000, 3)
            Case 22 : Cli.useItem(900595000, 1)
            Case 23 : Cli.useItem(900704000, 1)
            Case 24 : Cli.useItem(900562000, 1)
            Case 25 : Cli.useItem(810333000, 1)
            Case 26 : Cli.useItem(810351000, 1)
            Case 27 : Cli.useItem(900677000, 1)
            Case 28 : Cli.useItem(900709000, 1)
            Case 29 : Cli.useItem(810554000, 1)
            Case 30 : Cli.useItem(900758000, 1)
            Case 31 : Cli.useItem(998013000, 1)
            Case 32 : Cli.useItem(700038000, 1)
            Case 33 : Cli.useItem(700039000, 1)
            Case 34 : Cli.useItem(700042000, 1)
        End Select
    End Sub
#End Region
#Region "Blue Cheast"
    Private Sub btn_blueRow_Click(sender As Object, e As EventArgs) Handles btn_blueRow.Click
        If (Cli.getCharLevel() < 69) Then
            goTxt(strBlueBoxLevel, "")
            Exit Sub
        End If
        If (cmb_blueQuest.SelectedIndex < 2) Then
            Dim zone As String
            If (Cli.getCharIrk() = "Human") Then
                zone = "El Morad"
            Else
                zone = "Lüferson"
            End If

            If (Cli.getCharIrk() = "Human" And Cli.getCharZone() <> "El Morad") Then
                goTxt(strBlueBoxZone, zone)
                Exit Sub
            ElseIf (Cli.getCharIrk() = "Karus" And Cli.getCharZone() <> "Lüferson") Then
                goTxt(strBlueBoxZone, zone)
                Exit Sub
            End If
        End If
        Cli.blueQuestRow(cmb_blueQuest)
    End Sub
    Private Sub btn_setKecoon_Click(sender As Object, e As EventArgs) Handles btn_setKecoon.Click
        Cli.useTS(1)
    End Sub
    Private Sub btn_BlueBackRow_Click(sender As Object, e As EventArgs) Handles btn_BlueBackRow.Click
        If (cmb_blueQuest.SelectedIndex > 0) Then cmb_blueQuest.SelectedIndex -= 1
    End Sub
#End Region
#Region "All Timers"
    Private Sub tmr_100_Tick(sender As Object, e As EventArgs) Handles tmr_100.Tick
        lbl_dist.Text = Cli.getMobDistance()
        If (ServerStatus = False) Then End

        If (BotStatus = False) Then Exit Sub

        If (chk_SlotCheck.Checked) Then Cli.getAreaPeople(lst_PlayerSlot, "Player", , , , , True)

        If (chk_SlotCheck.Checked And lst_PlayerSlot.Items.Count > 0 And lbl_SlotCheck.Text = "NULL" And AttackStatus) Then
            lbl_SlotCheck.Text = cmb_SlotCheck.SelectedIndex.ToString()
        End If

        If (chk_walkArea.Checked And AttackStatus = True) Then
            Dim coorText As String = lst_walkArea.Text
            Cli.goKorText(coorText, False)
            If (getCoorText(coorText, 0) = Cli.getCharX() And getCoorText(coorText, 1) = Cli.getCharY()) Then
                If (lst_walkArea.SelectedIndex <> lst_walkArea.Items.Count - 1) Then
                    lst_walkArea.SelectedIndex += 1
                Else
                    lst_walkArea.SelectedIndex = 0
                End If
            End If
        End If

        With Cli

            '// Otomatik potion
            If rb_AutoPot.Checked = True Then
                If chk_HpPot.Checked = True Then
                    .useAutoPotionHP()
                End If
                If chk_MpPot.Checked = True Then
                    .useAutoPotionMP()
                End If
            End If

            '// Sınır potion
            If rb_RowPot.Checked = True Then
                If chk_HpPot.Checked = True Then
                    .useHpPotionRow(CInt(txt_HpPot.Text))
                End If
                If chk_MpPot.Checked = True Then
                    .useMpPotionRow(CInt(txt_MpPot.Text))
                End If
            End If

            '// Wall hack
            If chk_WallHack.Checked Then .WallHackOpen() Else .WallHackClose()
            '// Minor
            If chk_Minor.Checked Then .useMinor(.getCharID, CInt(txt_MinorRow.Text))
            '// Auto ts
            If chk_AutoTs.Checked Then .useTS(cmb_AutoTs.SelectedIndex)
            '//Atak sc Bug
            If chk_AttackSc.Checked Then .useAttackScroll()
            '// Drop sc Bug
            If chk_DropSc.Checked Then .useDropScBug()
            '// Weapon Sc
            If chk_WeaponSc.Checked Then .useWeaponSc()
            '// Armor Sc
            If chk_ArmorSc.Checked Then .useArmorSc()
            '// F3 Towm
            If chk_TownF3.Checked And GetAsyncKeyState(114) Then .useTown()
            '// F4 Respawn
            If chk_RespawnF4.Checked And GetAsyncKeyState(115) Then .useRespawn()
            '// F5 Gate
            If chk_GateF5.Checked And GetAsyncKeyState(116) Then .useGate()
            '// F6 Char Summon
            If chk_GateF5.Checked And GetAsyncKeyState(117) Then .SummonStart = True : .SummonRow = 1
            '// Magic Hammer
            If chk_MagicHammer.Checked Then .useMagicHammer()
            '// Respawn
            If (chk_respawnChr.Checked And .getCharHP <= 0) Then .useRespawnChar()

            '// Bot seçim
            If (BotStatus = True And AttackStatus = True And Cli.WaitAttack <= 0) Then
                '// Ok Yağmuru Seçim
                If .getCharClassName() = "Rogue" And .CharRogueJob = False And (lstv_AttackRog.Items().Item(51).Checked = True Or lstv_AttackRog.Items().Item(52).Checked = True) Then
                    If cmb_chooseMob.SelectedIndex = 3 Then
                        .rainbowArchery(cmb_selectMob.SelectedIndex, "List") '// Liste seç
                    ElseIf cmb_chooseMob.SelectedIndex = 2 Then
                        .rainbowArchery(cmb_selectMob.SelectedIndex, "Karşı") '// Karşı Irk
                    Else
                        .rainbowArchery(cmb_selectMob.SelectedIndex, "Mob") '// Mob
                    End If
                Else
                    '// Atak Ayarları
                    If cmb_chooseMob.SelectedIndex = 0 Then .getZMob(cmb_selectMob.SelectedIndex) : .selectMob(.zMobID) '// Otomtik Seç
                    If cmb_chooseMob.SelectedIndex = 1 Then .selectMob(Cli.getMobID, True) ': .selectMob(.zMobID) '// Manuel Seç
                    If cmb_chooseMob.SelectedIndex = 2 Then .getZMob(cmb_selectMob.SelectedIndex, "Karşı") : .selectMob(.zMobID) '// Karşı Irk
                    If cmb_chooseMob.SelectedIndex = 3 Then .getZMob(cmb_selectMob.SelectedIndex, "List") : .selectMob(.zMobID) '// Liste seç
                End If
            End If
        End With
    End Sub
    Private Sub tmr_1000_Tick(sender As Object, e As EventArgs) Handles tmr_1000.Tick
        Dim ts As TimeSpan
        ts = DateTime.Now - StartDate
        menu_Time.Text = "[" & Convert.ToInt32(ts.TotalMinutes) & "]dk."
        Cli.WaitAttack = Cli.WaitAttack - 1
        'If (chk_DeathWait.Checked And AttackStatus) Then
        '    AttackStatus = False
        '    Cli.DeathWaitAttack = Convert.ToInt32(txt_DeathWait.Text)
        'End If
        If (chk_DeathWait.Checked And Cli.WaitAttack < 0 And AttackStatus = False And btn_ProjectStart.Text = "Durdur") Then
            AttackStatus = True
        End If

        If (lbl_rprAttack.Text = "ON") Then
            Cli.rprAttackTime -= 1
            If (Cli.rprAttackTime <= 0) Then
                Cli.rprAttackTime = 0
                lbl_rprAttack.Text = "NULL"
            End If
        End If

        If (BotStatus = False) Then Exit Sub

        If (Cli.SummonStart And Cli.getPartyCount() > 1) Then
            If (Cli.SummonRow > Cli.getPartyCount()) Then
                Cli.SummonStart = False
            Else
                Cli.summonUser(Cli.PartyUserID(Cli.SummonRow))
                Cli.SummonRow = Cli.SummonRow + 1
            End If
        End If




        If (chk_DcMethod.Checked) Then
            dcTimer += 1
            If (dcTimer > 20) Then
                lbl_DcMethod.Text = cmb_DCMethod.SelectedIndex.ToString()
            End If
        End If

        If (chk_HourMethod.Checked And DateTime.Now.Hour = Convert.ToInt32(cmb_Hour.Text) And DateTime.Now.Minute = Convert.ToInt32(cmb_Minute.Text)) Then
            lbl_hourMethod.Text = cmb_HourMethod.SelectedIndex.ToString()
        End If

        If (chk_BackSlot.Checked And Cli.BacktoSlot) Then
            Cli.RespawnTimer = Cli.RespawnTimer + 1
            If (Cli.RespawnTimer > Convert.ToInt32(txt_BackSlot.Text.Trim())) Then
                Cli.useRespawn()
                If (btn_ProjectStart.Text = "Durdur") Then btn_ProjectStart_Click(sender, e)
                lst_goSlot.SelectedIndex = Convert.ToInt32(lbl_slotStart.Text)
                Cli.goSlotActive = True
                Cli.RespawnTimer = 0
                Cli.BacktoSlot = False
            End If
        End If

        If (chk_checkerGM.Checked And cmb_checkerFunc.SelectedIndex = 1) Then
            If (lbl_GmStatu.Text = "1") Then
                gmTimer = 0
                lbl_GmStatu.Text = "2"
            Else
                gmTimer += 1
                If (gmTimer >= 1800) Then
                    lst_rprCoor.SelectedIndex = 0
                    Cli.RepairActive = True
                    lbl_rprAttack.Text = "NULL"
                    If (btn_ProjectActive.Text = "Aktif") Then
                        btn_ProjectActive_Click(sender, e)
                    End If
                    gmTimer = 0
                End If
            End If
        End If

        'If (mobTimer <= 1800) Then
        '    mobTimer += 1
        'Else
        '    getProSettings()
        '    Dim restlgn As TimeSpan
        '    restlgn = uLoginEndDay - Date.Now
        '    If (bIsActive = False Or bMsgIsActive = False Or restlgn.TotalHours() < 0) Then
        '        goTxt(strEndDay, strCloseBot)
        '        End
        '    End If
        'End If

        With Cli
            '// Kutuya koş
            If (chk_runBox.Checked And btn_ProjectStart.Text = "Durdur") Then
                If (lst_BoxPacket.Items.Count > 0) Then
                    lst_BoxPacket.SelectedIndex = 0
                    lst_BoxCoor.SelectedIndex = 0
                    Dim KutuID As String = lst_BoxPacket.Text
                    Dim KutuCoor As String = lst_BoxCoor.Text
                    If (Cli.getDistanceToTarget(CInt(lbl_xCoorLoot.Text), CInt(lbl_yCoorLoot.Text), getCoorText(KutuCoor, 0), getCoorText(KutuCoor, 1)) < CInt(txt_boxRunDis.Text)) Then
                        If (Cli.getDistance(getCoorText(KutuCoor, 0), getCoorText(KutuCoor, 1)) <= 1) Then
                            Cli.SendPack(KutuID)
                        Else
                            If (chk_RunBoxStopAttack.Checked) Then
                                AttackStatus = False
                            End If
                            Cli.goKorText(KutuCoor, chk_TPLoot.Checked)
                        End If
                    Else
                        lst_BoxPacket.Items.RemoveAt(0)
                        lst_BoxCoor.Items.RemoveAt(0)
                    End If
                Else
                    If (chk_RunBoxStopAttack.Checked = False) Then
                        Dim coorCent As String = lbl_xCoorLoot.Text & " - " & lbl_yCoorLoot.Text
                        Cli.goKorText(coorCent, chk_TPLoot.Checked)
                    Else
                        AttackStatus = True
                    End If
                End If
            ElseIf chk_runMob.Checked And btn_ProjectStart.Text = "Durdur" And AttackStatus Then '// Moba koş
                If (.getDistanceToTarget(CInt(lbl_centX.Text), CInt(lbl_centY.Text), .getBaseIDX(.zMobID), .getBaseIDY(.zMobID)) < CInt(txt_rundisMob.Text)) And (.getDistanceToTarget(CInt(lbl_centX.Text), CInt(lbl_centY.Text), .getCharX, .getCharY) < 500) Then
                    If (chk_tpMob.Checked) Then
                        .tpChar(.getBaseIDX(.zMobID), .getBaseIDY(.zMobID))
                    Else
                        .walkChar(.getBaseIDX(.zMobID), .getBaseIDY(.zMobID))
                    End If
                ElseIf chk_backToCent.Checked Then
                    If (chk_tpMob.Checked) Then
                        .tpChar(CInt(lbl_centX.Text), CInt(lbl_centY.Text))
                    Else
                        .walkChar(CInt(lbl_centX.Text), CInt(lbl_centY.Text))
                    End If
                End If
            ElseIf chk_FollowUser.Checked And BotStatus Then '// Takip
                If (cmb_FollowUser.SelectedIndex = 0) Then '// Kayıtlı Takip
                    Dim userX, userY As Integer
                    userX = Cli.getBaseIDX(Convert.ToInt32(lbl_FollowBase.Text))
                    userY = Cli.getBaseIDY(Convert.ToInt32(lbl_FollowBase.Text))
                    If (userX > 0 And userY > 0) Then
                        If (Cli.getDistance(userX, userY) > 1 And Cli.getDistance(userX, userY) < 255) Then
                            Cli.walkChar(userX, userY)
                        End If
                    End If
                ElseIf (Cli.getPartyCount() > 0) Then '// Party başkanı takip
                    Dim userX, userY As Integer
                    userX = Cli.PartyUserX(1)
                    userY = Cli.PartyUserY(1)
                    If (userX > 0 And userY > 0) Then
                        If (Cli.getDistance(userX, userY) > 1 And Cli.getDistance(userX, userY) < 255) Then
                            Cli.walkChar(userX, userY)
                        End If
                    End If
                End If
            End If

        End With
    End Sub
    Private Sub tmr_10_Tick(sender As Object, e As EventArgs) Handles tmr_10.Tick
        With Cli

            lbl_DcChecker.Text = .HookPacket.ToString()
            'txt_Hex.Text = Cli.getMobDistance()
            lbl_charZone.Text = Cli.GetCharZoneId()
            '// Skill Süreleri Azalt
            If .getCharClassName = "Warrior" Then .warriorSkillRow()
            If .getCharClassName = "Rogue" Then .rogueSkillRow()
            If .getCharClassName = "Mage" Then .mageSkillRow()
            If .getCharClassName = "Priest" Then
                .priestSkillRow()
                .partySkillRow()
            End If

            If (BotStatus = False) Then Exit Sub

            '// Zamanlı Skiller
            If .getCharClassName = "Warrior" Then .useWarriorTimeSkill()
            If .getCharClassName = "Rogue" Then .useRogueTimeSkill()
            If .getCharClassName = "Mage" Then .useTimeSkillMage()
            If .getCharClassName = "Priest" Then .useTimeSkillPriest()

            If (chk_chaosAttack.Checked) Then
                .ChaosSkillRow()
            End If

            If (.getPartyCount() > 0) Then
                .getPartyInformation()
            End If
        End With
    End Sub
    Private Sub tmr_hookDis_Tick(sender As Object, e As EventArgs) Handles tmr_hookDis.Tick
        If (BotStatus = False) Then Exit Sub
        Cli.ReadMessage(CBool(chk_AutoLoot.Checked), CInt(lbl_LootStatus.Text), lstv_UniqueItem, CBool(chk_runBox.Checked), lst_BoxPacket, lst_BoxCoor, cmb_ItemExt.Text, CBool(chk_checkerGM.Checked), AttackStatus, cmb_chooseMob, chk_GarbageItem, lstv_GarbageItem)
    End Sub
    Private Sub tmr_200_Tick(sender As Object, e As EventArgs) Handles tmr_200.Tick
        'txt_Hex.Text = Cli.getStatuInMob()

        If (ServerStatus = False) Then End


        If (chk_chaosAttack.Checked) Then Cli.useSkillChaos()
        '// Repair Checking Finish

        If (Cli.goSlotActive = True And lst_goSlot.Items.Count > 0) Then
            Cli.goSlotText = lst_goSlot.Text
            Select Case Cli.goSlotText.Substring(0, 1)
                Case "G"
                    Cli.setRprMethod(Cli.goSlotText, lst_goSlot)
                Case "R"
                    lbl_goSlot.Text = "R"
                Case "L"
                    lbl_goSlot.Text = "L"
                Case "D"
                    lbl_goSlot.Text = "D"
                Case "O"
                    If (btn_ProjectStart.Text = "Başlat") Then btn_ProjectStart_Click(sender, e)
                    lbl_goSlot.Text = "NULL"
                    lst_goSlot.SelectedIndex = 0
                    Cli.goSlotActive = False
                    Exit Sub
                Case Else
                    Cli.goKorText(lst_goSlot.Text, False)
                    If (Cli.getCharX() = getCoorText(Cli.goSlotText, 0) And Cli.getCharY() = getCoorText(Cli.goSlotText, 1)) Then
                        lst_goSlot.SelectedIndex += 1
                    End If
            End Select
        End If

        If (BotStatus = False) Then Exit Sub

        '// Repair Checking Start
        If (chk_rprIsActive.Checked And Cli.RepairActive = False And lbl_rprAttack.Text <> "ON") And lst_rprCoor.Items.Count > 0 And Cli.goSlotActive = False And Cli.checkBank() = False Then

            If (Cli.getItemSlotInv("379099000") < 0 Or chk_MagicHammer.Checked = False) Then
                If (Cli.getItemDurab(7) < 150 And Cli.getItemID(7) > 0) Then
                    lst_rprCoor.SelectedIndex = 0
                    Cli.RepairActive = True
                End If

                If (Cli.getItemDurab(9) < 150 And Cli.getItemID(9) > 0) Then
                    lst_rprCoor.SelectedIndex = 0
                    Cli.RepairActive = True
                End If
            End If

            If (chk_InvFull.Checked And Cli.getEmptySlotCountInv() < 2) Then
                lst_rprCoor.SelectedIndex = 0
                Cli.RepairActive = True
            End If

            If (chk_EmptyItem.Checked) Then
                If (Cli.checkHpPotion() = False And chk_RprHP.Checked) Or (Cli.checkMpPotion() = False And chk_RprMP.Checked) Then
                    lst_rprCoor.SelectedIndex = 0
                    Cli.RepairActive = True
                End If
                If (chk_RprDcHP.Checked And Cli.checkDcHpPotion() = False) Then
                    lst_rprCoor.SelectedIndex = 0
                    Cli.RepairActive = True
                End If

                If (chk_RprDcMP.Checked And Cli.checkDcMpPotion() = False) Then
                    lst_rprCoor.SelectedIndex = 0
                    Cli.RepairActive = True
                End If

                If (chk_RprWOLF.Checked And Cli.getItemIDCountInv("370004000") <= 5) Then
                    lst_rprCoor.SelectedIndex = 0
                    Cli.RepairActive = True
                End If
                If (chk_RprARROW.Checked And Cli.getItemIDCountInv("391010000") <= 200) Then
                    lst_rprCoor.SelectedIndex = 0
                    Cli.RepairActive = True
                End If
                If (chk_RprPOG.Checked And Cli.getItemIDCountInv("389026000") <= 5) Then
                    lst_rprCoor.SelectedIndex = 0
                    Cli.RepairActive = True
                End If
                If (chk_RprTS.Checked And Cli.getItemIDCountInv("379091000") <= 5) Then
                    lst_rprCoor.SelectedIndex = 0
                    Cli.RepairActive = True
                End If
            End If
        End If
        '// Repair Checking Finish

        If (Cli.RepairActive = True And lst_rprCoor.Items.Count > 0 And lbl_rprAttack.Text <> "ON" And chk_rprIsActive.Checked) Then
            lbl_rprAttack.Text = "OFF"
            Cli.rprText = lst_rprCoor.Text
            Select Case Cli.rprText.Substring(0, 1)
                Case "G"
                    Cli.setRprMethod(Cli.rprText, lst_rprCoor)
                Case "S"
                    lbl_RprBuy.Text = "S"
                Case "I"
                    lbl_RprBuy.Text = "I"
                Case "P"
                    lbl_RprBuy.Text = "P"
                Case "L"
                    lbl_RprBuy.Text = "L"
                Case "D"
                    lbl_RprBuy.Text = "D"
                Case "O"
                    lbl_RprBuy.Text = "NULL"
                    Cli.RepairActive = False
                    lbl_rprAttack.Text = "ON"
                    lst_rprCoor.SelectedIndex = 0
                    Exit Sub
                Case Else
                    Cli.goKorText(lst_rprCoor.Text, False)
                    If (Cli.getCharX() = getCoorText(Cli.rprText, 0) And Cli.getCharY() = getCoorText(Cli.rprText, 1)) Then
                        lst_rprCoor.SelectedIndex += 1
                    End If
            End Select
        End If
    End Sub
    Private Sub tmr_2000_Tick(sender As Object, e As EventArgs) Handles tmr_2000.Tick
        If (chk_UpIsActive.Checked) Then
            lbl_TotalUpPrice.Text = convertToMoney(Cli.getTotalUpPrice(cmb_UpScrool, chkl_UpItems))
        End If
    End Sub
    Private Sub tmr_LoginCheck_Tick(sender As Object, e As EventArgs) Handles tmr_LoginCheck.Tick

        If (autoGameStatu = False) Then tmr_LoginCheck.Enabled = False

        If (autoGameStatu And chk_rprIsActive.Checked And autoLoginRow = 99 And lst_rprCoor.Items.Count > 0) Then
            lst_rprCoor.SelectedIndex = 0
            Cli.RepairActive = True
            lbl_rprAttack.Text = "NULL"
            If (btn_ProjectActive.Text = "Aktif") Then
                btn_ProjectActive_Click(sender, e)
            End If
            autoLoginRow = 2
            autoGameStatu = False
            tmr_LoginCheck.Enabled = False
        End If

        If (autoGameStatu And chk_UpIsActive.Checked And autoLoginRow = 99) Then
            lst_UpBotMethod.SelectedIndex = 0
            Cli.upBotActive = True
            If (btn_ProjectActive.Text = "Aktif") Then
                btn_ProjectActive_Click(sender, e)
            End If
            autoLoginRow = 2
            autoGameStatu = False
            tmr_LoginCheck.Enabled = False
        End If

    End Sub
#End Region
#Region "Text check"
    Private Sub txt_HpPot_TextChanged(sender As Object, e As EventArgs) Handles txt_HpPot.TextChanged
        checkTextBox(txt_HpPot)
    End Sub

    Private Sub txt_MpPot_TextChanged(sender As Object, e As EventArgs) Handles txt_MpPot.TextChanged
        checkTextBox(txt_MpPot)
    End Sub

    Private Sub txt_MinorRow_TextChanged(sender As Object, e As EventArgs) Handles txt_MinorRow.TextChanged
        checkTextBox(txt_MinorRow)
    End Sub

    Private Sub txt_rundisMob_TextChanged(sender As Object, e As EventArgs) Handles txt_rundisMob.TextChanged
        checkTextBox(txt_rundisMob)
    End Sub

    Private Sub txt_PriestHeal_TextChanged(sender As Object, e As EventArgs) Handles txt_PriestHeal.TextChanged
        checkTextBox(txt_PriestHeal)
    End Sub

    Private Sub txt_PartyHeal_TextChanged(sender As Object, e As EventArgs) Handles txt_PartyHeal.TextChanged
        checkTextBox(txt_PartyHeal)
    End Sub

    Private Sub txt_MinorPtRow_TextChanged(sender As Object, e As EventArgs) Handles txt_MinorPtRow.TextChanged
        checkTextBox(txt_MinorPtRow)
    End Sub

    Private Sub txt_PartyGHealRow_TextChanged(sender As Object, e As EventArgs) Handles txt_PartyGHealRow.TextChanged
        checkTextBox(txt_PartyGHealRow, 3)
    End Sub

    Private Sub txt_PartyGroupHealPoint_TextChanged(sender As Object, e As EventArgs) Handles txt_PartyGroupHealPoint.TextChanged
        checkTextBox(txt_PartyGroupHealPoint)
    End Sub

    Private Sub txt_boxRunDis_TextChanged(sender As Object, e As EventArgs) Handles txt_boxRunDis.TextChanged
        checkTextBox(txt_boxRunDis)
    End Sub
#End Region
#Region "Go back slot"

    Private Sub btn_addSlotCor_Click(sender As Object, e As EventArgs) Handles btn_addSlotCor.Click
        lst_goSlot.Items.Add(Cli.getCharX & " - " & Cli.getCharY)
    End Sub

    Private Sub btn_addSlotMethod_Click(sender As Object, e As EventArgs) Handles btn_addSlotMethod.Click
        Cli.addRprMethod(cmb_slotMethod, lst_goSlot)
        If (cmb_slotMethod.Text.Substring(0, 1) = "R") Then lbl_slotStart.Text = lst_goSlot.Items.Count - 1
    End Sub

    Private Sub btn_RefleshSlot_Click(sender As Object, e As EventArgs) Handles btn_RefleshSlot.Click
        Cli.refreshMethod(cmb_slotMethod, True)
    End Sub

    Private Sub lst_goSlot_DoubleClick(sender As Object, e As EventArgs) Handles lst_goSlot.DoubleClick
        If (CInt(lst_goSlot.Items.Count) > 0 And lst_goSlot.SelectedIndex > -1) Then
            lst_goSlot.Items.RemoveAt(lst_goSlot.SelectedIndex)
        End If
    End Sub

    Private Sub btn_goSlotChar_Click(sender As Object, e As EventArgs) Handles btn_goSlotChar.Click
        If (CInt(lst_goSlot.Items.Count) > 0) Then
            AttackStatus = False
            BotStatus = False
            lst_goSlot.SelectedIndex = 0
            Cli.goSlotActive = True
            btn_ProjectActive.Text = "Aktif"
            btn_ProjectStart.Text = "Başlat"
        Else
            goTxt("Lütfen karakter işlemlerini ekleyiniz.", "")
        End If
    End Sub
    Private Sub btn_stopSlotChar_Click(sender As Object, e As EventArgs) Handles btn_stopSlotChar.Click
        AttackStatus = False
        BotStatus = False
        btn_ProjectActive.Text = "Aktif"
        btn_ProjectStart.Text = "Başlat"
        lst_goSlot.SelectedIndex = 0
        Cli.goSlotActive = False
    End Sub

    Private Sub lbl_goSlot_TextChanged(sender As Object, e As EventArgs) Handles lbl_goSlot.TextChanged
        If (lbl_goSlot.Text.Substring(0, 1) = "R") Then
            Cli.setRprMethod(Cli.goSlotText, lst_goSlot)
            lbl_goSlot.Text = "NULL"
        End If
        If (lbl_goSlot.Text.Substring(0, 1) = "L") Then
            Cli.PetStatu(True)
            lst_goSlot.SelectedIndex += 1
            lbl_goSlot.Text = "NULL"
        End If
    End Sub

    Private Sub chk_BackSlot_CheckedChanged(sender As Object, e As EventArgs) Handles chk_BackSlot.CheckedChanged
        If (chk_BackSlot.Checked) Then
            If (lst_goSlot.Items.Count <= 0) Then
                goTxt("Lütfen Karakter Slot Gönder kordinatlarını ekleyiniz.", "Kordinatlar olmadan bu işlem çalışmaz.")
                chk_BackSlot.Checked = False
                Exit Sub
            End If
        End If
    End Sub
#End Region
    Private Sub btn_Test_Click(sender As Object, e As EventArgs) Handles btn_Test.Click
        Cli.openRecv()
    End Sub

    Private Sub chk_SpeedHACK_CheckedChanged(sender As Object, e As EventArgs) Handles chk_SpeedHACK.CheckedChanged
        If (chk_SpeedHACK.Checked) Then
            Cli.SpeedLe()
        Else
            Cli.SpeedAl()
        End If
    End Sub

    Private Sub btn_goElmorad_Click(sender As Object, e As EventArgs) Handles btn_goEnemyBase.Click
        Dim lstCoor As String()
       
        If (Cli.getCharIrk() = "Karus") Then
            lstCoor = {"340 - 1746", "396 - 1698", "478 - 1656", "681 - 1459", "687 - 1284", "646 - 1191", "614 - 748", "1040 - 396", "1194 - 477", "1480 - 623", "1612 - 497", "1629 - 468", "1738 - 367", "1717 - 304"}
        Else
            lstCoor = {"1680 - 634", "1679 - 873", "1664 - 965", "1550 - 1010", "1433 - 1290", "1103 - 1666", "701 - 1650", "535 - 1639", "376 - 1792", "327 - 1763"}
        End If

        lst_goBox.Items.Clear()
        For Each item As String In lstCoor
            lst_goBox.Items.Add(item)
        Next

        If (Cli.getCharIrk() = "Karus" And Cli.getCharZone() = "El Morad") Then
            lst_goBox.SelectedIndex = 0
            tmr_goBlueBox.Enabled = True
            Exit Sub
        ElseIf (Cli.getCharIrk() = "Karus") Then
            goTxt("İşlem gerçekleşmedi!", "Lütfen El Morad zonesine geçiş yapınız.")
            Exit Sub
        End If


        If (Cli.getCharIrk() = "Human" And Cli.getCharZone() = "Lüferson") Then
            lst_goBox.SelectedIndex = 0
            tmr_goBlueBox.Enabled = True
            Exit Sub
        ElseIf (Cli.getCharIrk() = "Human") Then
            goTxt("İşlem gerçekleşmedi!", "Lütfen Lüferson zonesine geçiş yapınız.")
            Exit Sub
        End If

    End Sub

    Private Sub tmr_goBlueBox_Tick(sender As Object, e As EventArgs) Handles tmr_goBlueBox.Tick
        Cli.goKorText(lst_goBox.Text, False)
        If (Cli.getCharHP() <= 0) Then tmr_goBlueBox.Enabled = False
        If (Cli.getCharX() = getCoorText(lst_goBox.Text, 0) And Cli.getCharY() = getCoorText(lst_goBox.Text, 1)) Then
            If (lst_goBox.SelectedIndex <> lst_goBox.Items.Count - 1) Then
                lst_goBox.SelectedIndex += 1
            Else
                tmr_goBlueBox.Enabled = False
            End If
        End If
    End Sub

    Private Sub btn_goEnemyBox_Click(sender As Object, e As EventArgs) Handles btn_goEnemyBox.Click
        Dim lstCzCoor As String()
        lstCzCoor = {"1380 - 1100", "1363 - 999", "1298 - 894", "1119 - 759", "978 - 729", "746 - 734", "653 - 805"}

        If (Cli.getCharIrk() = "Human") Then
            Array.Reverse(lstCzCoor)
        End If

        lst_goBox.Items.Clear()
        For Each item As String In lstCzCoor
            lst_goBox.Items.Add(item)
        Next

        If (Cli.getCharZone() = "Ronark Land") Then
            lst_goBox.SelectedIndex = 0
            tmr_goBlueBox.Enabled = True
            Exit Sub
        Else
            goTxt("İşlem gerçekleşmedi!", "Lütfen Ronark Land zonesine geçiş yapınız.")
            Exit Sub
        End If
    End Sub

    Private Sub btn_stopBule_Click(sender As Object, e As EventArgs) Handles btn_stopBule.Click
        lst_goBox.SelectedIndex = 0
        tmr_goBlueBox.Enabled = False
    End Sub

    Private Sub lbl_shStatu_TextChanged(sender As Object, e As EventArgs) Handles lbl_shStatu.TextChanged
        If (lbl_shStatu.Text = "ON") Then
            Cli.SpeedLe()
            tmrSh.Interval = 1
        Else
            tmrSh.Interval = 1
            Cli.SpeedAl()
        End If
    End Sub

    Private Sub chk_SpeedHackGame_CheckedChanged(sender As Object, e As EventArgs) Handles chk_SpeedHackGame.CheckedChanged
        If chk_SpeedHackGame.Checked Then
            Cli.SpeedLe()
        Else
            Cli.SpeedAl()
        End If
    End Sub
    Private Sub tb_shValue_ValueChanged(sender As Object, e As EventArgs) Handles tb_shValue.ValueChanged
        Cli.SeriHiz = 16256 + tb_shValue.Value
        Cli.SeriTimer = 1.5 - (FormatNumber(Val(tb_shValue.Value) / 185, 1) + 0.1)
        If (Cli.SeriHiz < 16320) Then
            lbl_shValue.Text = "Swift -"
        ElseIf (Cli.SeriHiz = 16320) Then
            lbl_shValue.Text = "Swift"
        ElseIf (Cli.SeriHiz > 16320 And Cli.SeriHiz < 16384) Then
            lbl_shValue.Text = "Swift +"
        ElseIf (Cli.SeriHiz = 16384) Then
            lbl_shValue.Text = "LF"
        ElseIf (Cli.SeriHiz > 16384) Then
            lbl_shValue.Text = "LF +"
        End If
        If chk_SpeedHackGame.Checked Then Cli.SpeedLe()
    End Sub
    Private Sub tmrSh_Tick(sender As Object, e As EventArgs) Handles tmrSh.Tick
        If (BotStatus = False) Then Exit Sub
        If chk_shShiftButton.Checked And GetAsyncKeyState(16) Then
            lbl_shStatu.Text = "ON"
            If (tb_shValue.Value <> tb_shValue.Maximum) Then tb_shValue.Value += 1
        ElseIf (chk_shShiftButton.Checked) Then
            lbl_shStatu.Text = "OFF"
            If (tb_shValue.Value <= tb_shValue.Maximum And tb_shValue.Value > 128) Then tb_shValue.Value -= 1
        End If
    End Sub

    Private Sub chk_NormalSkill_CheckedChanged(sender As Object, e As EventArgs) Handles chk_NormalSkill.CheckedChanged
        Cli.NormalSkill = chk_NormalSkill.Checked
    End Sub

    Private Sub gb_saveMe_Enter(sender As Object, e As EventArgs) Handles gb_saveMe.Enter

    End Sub

    Private Sub lbl_ChooseItem_Click(sender As Object, e As EventArgs) Handles lbl_ChooseItem.Click
        Dim msgSpeed As String
        msgSpeed = "Seçili itemlerin sınırları :" & vbNewLine _
            & "Arrow : 200" & vbNewLine _
            & "Wolf : 5" & vbNewLine _
            & "Transformation Gem : 5" & vbNewLine _
            & "Prayer of god's power : 5"
        MessageBox.Show(msgSpeed, strProjectInfo, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub chk_sellItem_CheckedChanged(sender As Object, e As EventArgs) Handles chk_sellItem.CheckedChanged
        If (chk_sellItem.Checked) Then
            If (lstv_UniqueItem.Items.Count <= 0) Then
                tb_Project.SelectedTab = Diger
                goTxt("Lütfen değerli itemleri ekleyiniz.", "Aksi takdirde bütün itemler satılacaktır.")
                chk_sellItem.Checked = False
            End If
        End If
    End Sub

    Private Sub btn_DcSundriesAction_Click(sender As Object, e As EventArgs) Handles btn_DcSundriesAction.Click
        With Cli
            .potionDcNPC(CInt(txt_RprDcHPPot.Text), CInt(txt_RprDcMPPot.Text), CBool(chk_RprDcHP.Checked), CBool(chk_RprDcMP.Checked))
        End With
    End Sub

    Private Sub chk_VIP_CheckedChanged(sender As Object, e As EventArgs) Handles chk_VIP.CheckedChanged
        gb_BlueCheast.Visible = chk_VIP.Checked
        gb_OtherFunction.Visible = Not chk_VIP.Checked
    End Sub

    Private Sub btn_CoorClear_Click(sender As Object, e As EventArgs) Handles btn_CoorClear.Click
        lst_walkArea.Items.Clear()
    End Sub

    Private Sub btn_UniqueAddFirst_Click(sender As Object, e As EventArgs) Handles btn_UniqueAddFirst.Click
        Dim lstItem As ListViewItem = New ListViewItem(New String() {Cli.getItemIDInv(0), ItemNameFind(Cli.getItemIDInv(0).ToString().Substring(0, 6) & "000")})
        lstv_UniqueItem.Items.Add(lstItem)
    End Sub

    Private Sub btn_GarbageAddFirst_Click(sender As Object, e As EventArgs) Handles btn_GarbageAddFirst.Click
        Dim lstItem As ListViewItem = New ListViewItem(New String() {Cli.getItemIDInv(0), ItemNameFind(Cli.getItemIDInv(0).ToString().Substring(0, 6) & "000")})
        lstv_GarbageItem.Items.Add(lstItem)
    End Sub

    Private Sub lbl_SlotCheck_TextChanged(sender As Object, e As EventArgs) Handles lbl_SlotCheck.TextChanged
        Select Case lbl_SlotCheck.Text
            Case "0"
                Cli.useTown()
                btn_ProjectActive_Click(sender, e)
                lbl_SlotCheck.Text = "NULL"
            Case "1"
                lst_rprCoor.SelectedIndex = 0
                Cli.RepairActive = True
                lbl_SlotCheck.Text = "NULL"
            Case "2"
                ShutDown(Cli.Ko_Pid)
                goTxt("Slota birisi geldi", "Oyun kapatıldı")
            Case Else
        End Select
    End Sub
    Private Sub lbl_DcChecker_TextChanged(sender As Object, e As EventArgs) Handles lbl_DcChecker.TextChanged
        dcTimer = 0
    End Sub

    Private Sub lbl_DcMethod_TextChanged(sender As Object, e As EventArgs) Handles lbl_DcMethod.TextChanged
        Select Case lbl_DcMethod.Text
            Case "0"
                PlayAlarm()
                lbl_DcMethod.Text = "NULL"
            Case "1"
                ShutDown(Cli.Ko_Pid)
                lbl_DcMethod.Text = "NULL"
            Case "2"
                System.Diagnostics.Process.Start("shutdown", "-s -t 00")
                lbl_DcMethod.Text = "NULL"
            Case Else
        End Select
    End Sub
    Private Sub lbl_RprBilgi_Click(sender As Object, e As EventArgs) Handles lbl_RprBilgi.Click
        Dim msgSpeed As String
        msgSpeed = "Repair işlemi sırasında yapılacak olan işlemleri bu bölümden seçebilirsiniz." & vbNewLine _
            & "Pot alımları için 'Potion' isimi NPC'leri" & vbNewLine _
            & "DC alımları için 'DC Sundries' isimli NPC'leri kullanmalısınız, diğer işlemler 'Sundries' adındaki NPC'lerde yapılmaktadır."
        MessageBox.Show(msgSpeed, strProjectInfo, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub lbl_txtSellItem_Click(sender As Object, e As EventArgs) Handles lbl_txtSellItem.Click
        Dim msgSpeed As String
        msgSpeed = "Diğer bölümünde Değerli İtemler kısmına eklediğiniz itemler dışındaki tüm itemleri satmanızı sağlar"
        MessageBox.Show(msgSpeed, strProjectInfo, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Sub lbl_hourMethod_TextChanged(sender As Object, e As EventArgs) Handles lbl_hourMethod.TextChanged
        Select Case lbl_hourMethod.Text
            Case "0"
                PlayAlarm()
                lbl_hourMethod.Text = "NULL"
            Case "1"
                Cli.useTown()
                btn_ProjectActive_Click(sender, e)
                lbl_hourMethod.Text = "NULL"
            Case "2"
                ShutDown(Cli.Ko_Pid)
                btn_ProjectActive_Click(sender, e)
                lbl_hourMethod.Text = "NULL"
            Case "3"
                System.Diagnostics.Process.Start("shutdown", "-s -t 00")
                End
            Case Else
        End Select
    End Sub

    Private Sub btn_SlotListClear_Click(sender As Object, e As EventArgs) Handles btn_SlotListClear.Click
        lst_goSlot.Items.Clear()
        lbl_slotStart.Text = "0"
    End Sub

    Private Sub btn_TakeQuest_Click(sender As Object, e As EventArgs) Handles btn_TakeQuest.Click
        Cli.TakeQuest(txt_QuestCode.Text, False)
    End Sub

    Private Sub btn_DeleteQuest_Click(sender As Object, e As EventArgs) Handles btn_DeleteQuest.Click
        Cli.TakeQuest(txt_QuestCode.Text, False)
    End Sub

    Private Sub OyunuKapatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OyunuKapatToolStripMenuItem.Click
        ShutDown(Cli.Ko_Pid)
        Sleep(300)
        disableX3(autoLoginPath)
    End Sub
    Private Sub UpgradeAktifToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpgradeAktifToolStripMenuItem.Click
        If (btn_ProjectActive.Text = "Aktif") Then
            btn_ProjectActive_Click(sender, e)
        End If
        btn_AutoUpActive_Click(sender, e)
    End Sub
    Private Sub lbl_queChatSummon_Click(sender As Object, e As EventArgs) Handles lbl_queChatSummon.Click
        Dim msgSpeed As String
        msgSpeed = "Chat'e göre party çekme sistemi nasıl çalışır ?" & vbNewLine _
            & "Party chat veya pm üzerinden şifre ile çekilecek üye belirtilmelidir." & vbNewLine _
            & txt_chatSummon.Text & " 2" & vbNewLine _
            & "yazıldığında 2. üye mage tarafından çekilecektir." & vbNewLine _
            & "party toplama özelliği için " & txt_chatSummon.Text & " topla yazabilirsiniz."
        MessageBox.Show(msgSpeed, strProjectInfo, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub txt_chatSummon_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_chatSummon.KeyDown
        If (e.KeyCode = Keys.Space) Then
            e.Handled = False
        End If
    End Sub

    Private Sub gb_GeneralFunc_Enter(sender As Object, e As EventArgs) Handles gb_GeneralFunc.Enter

    End Sub

    Private Sub chk_shShiftButton_CheckedChanged(sender As Object, e As EventArgs) Handles chk_shShiftButton.CheckedChanged

    End Sub

    Private Sub btn_SaveFollw_Click(sender As Object, e As EventArgs) Handles btn_SaveFollw.Click
        If (Cli.getMobID() > 0) Then
            lbl_FollowBase.Text = Cli.getMobID()
            gb_Takip.Text = "Takip (" & Cli.getbMobName() & ")"
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Cli.closeRecv()
    End Sub
End Class