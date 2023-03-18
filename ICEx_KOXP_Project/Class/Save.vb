Option Explicit On
Module Save
#Region "Genel Kayıtlar"
    Public Sub SaveIni(ByVal section As String, ByVal Key As String, ByVal Value As String, ByVal FileName As String)
        Call WritePrivateProfileString(section, Key, Value, Application.StartupPath & "\Settings\" & FileName)
    End Sub
    Public Sub SaveIniFile(ByVal section As String, ByVal Key As String, ByVal Value As String, ByVal FileName As String)
        Call WritePrivateProfileString(section, Key, Value, FileName)
    End Sub
    Public Function ReadIni(ByVal section As String, ByVal Key As String, ByVal FileName As String) As String
        Dim RetVal As String, LenKey As Long
        RetVal = Space(1024)
        LenKey = GetPrivateProfileString(section, Key, "", RetVal, 255, Application.StartupPath & "\Settings\" & FileName)
        Return Left(RetVal, LenKey)
    End Function
    Public Function ItemNameFind(ByVal ItemIDINI As String) As String
        Return ReadIni("Items", ItemIDINI, "Items.cs")
    End Function
#End Region
#Region "Giriş Bölümü"
    Public Sub PatchDataDisable(ByVal path As String)
        SaveIniFile("Version", "Count", "8", path & "Path.Ini")
        SaveIniFile("XignCode", "Error", "0", path & "Server.ini")
    End Sub
    Public Sub loginSave(ByVal saveColumn As String, ByVal saveValue As String)
        SaveIni("Login_Page", saveColumn, saveValue, "Login.cs")
    End Sub
    Public Function loginRead(ByVal saveColumn As String)
        Return ReadIni("Login_Page", saveColumn, "Login.cs")
    End Function
    Public Sub LoginPageSettings(ByRef lstV As ListView, ByRef cmbServer As ComboBox, ByRef cmbChannel As ComboBox, ByRef cmbSlot As ComboBox, ByRef txtSteamID As TextBox, ByRef txtSteamPass As TextBox, ByRef chkAutoLogin As CheckBox, ByRef chklgnedSteam As CheckBox, ByRef chkSlow As CheckBox, ByVal statu As Boolean)
        Try
            If (statu = True) Then
                loginSave("cmbServer", cmbServer.SelectedIndex)
                loginSave("cmbChannel", cmbChannel.SelectedIndex)
                loginSave("cmbSlot", cmbSlot.SelectedIndex)
                loginSave("txtSteamID", txtSteamID.Text)
                loginSave("txtSteamPass", txtSteamPass.Text)
                loginSave("chkAutoLogin", chkAutoLogin.Checked.ToString())
                loginSave("GameCount", lstV.Items.Count - 1)
                For i As Integer = 0 To lstV.Items.Count - 1
                    loginSave("GameNick(" & i & ")", lstV.Items(i).Text)
                    loginSave("GamePath(" & i & ")", lstV.Items(i).SubItems(1).Text)
                Next
                loginSave("chk_LoginedSteam", chklgnedSteam.Checked)
                loginSave("chk_SlowMotion", chkSlow.Checked)
            Else
                If Dir("Settings\Login.cs") = "" Then Exit Sub
                cmbServer.SelectedIndex = loginRead("cmbServer")
                cmbChannel.SelectedIndex = loginRead("cmbChannel")
                cmbSlot.SelectedIndex = loginRead("cmbSlot")
                txtSteamID.Text = loginRead("txtSteamID")
                txtSteamPass.Text = loginRead("txtSteamPass")
                chkAutoLogin.Checked = Convert.ToBoolean(loginRead("chkAutoLogin"))
                For i As Integer = 0 To Convert.ToInt32(loginRead("GameCount"))
                    If searchListView(loginRead("GameNick(" & i & ")"), 1, lstV) = False Then
                        Dim lstItem As ListViewItem = New ListViewItem(New String() {loginRead("GameNick(" & i & ")"), loginRead("GamePath(" & i & ")")})
                        lstV.Items.Add(lstItem)
                    End If
                Next
                chklgnedSteam.Checked = Convert.ToBoolean(loginRead("chk_LoginedSteam"))
                chkSlow.Checked = Convert.ToBoolean(loginRead("chk_SlowMotion"))
            End If
        Catch ex As Exception
            goTxt("Login bölümünde okunamayan bir kayıt mevcut !", "Tekrardan Ayar Kaydet özelliğini kullanmalısınız.")
        End Try
    End Sub
    Public Sub AutoLoginPathSave(ByVal loginPath As String, ByVal botForText As String)
        loginSave("BotForSteam", botForText)
        loginSave("AutoLoginPath", loginPath)
    End Sub
    Public Function IsSteam() As String
        Return loginRead("BotForSteam")
    End Function
#End Region
#Region "Bot Bölümü"
    Public Sub botSave(ByVal saveColumn As String, ByVal saveValue As String)
        SaveIni("Bot_Page", saveColumn, saveValue, "Bot.cs")
    End Sub
    Public Function botRead(ByVal saveColumn As String)
        Return ReadIni("Bot_Page", saveColumn, "Bot.cs")
    End Function
    Public Sub readBot()
        With frm_2Genel
            Try
                '################ Genel Bölümü ################
                '// Korunma
                .chk_HpPot.Checked = botRead("chk_HpPot")
                .txt_HpPot.Text = botRead("txt_HpPot")
                .chk_MpPot.Checked = botRead("chk_MpPot")
                .txt_MpPot.Text = botRead("txt_MpPot")
                .rb_AutoPot.Checked = botRead("rb_AutoPot")
                .rb_RowPot.Checked = botRead("rb_RowPot")
                .chk_Minor.Checked = botRead("chk_Minor")
                .chk_MagicHammer.Checked = botRead("chk_MagicHammer")
                .chk_respawnChr.Checked = botRead("chk_respawnChr")
                .txt_MinorRow.Text = botRead("txt_MinorRow")
                .chk_BackSlot.Checked = botRead("chk_BackSlot")
                .txt_BackSlot.Text = botRead("txt_BackSlot")

                '//Genel Fonksiyonlar
                .chk_WallHack.Checked = botRead("chk_WallHack")
                .chk_DropSc.Checked = botRead("chk_DropSc")
                .chk_AttackSc.Checked = botRead("chk_AttackSc")
                .chk_ArmorSc.Checked = botRead("chk_ArmorSc")
                .chk_WeaponSc.Checked = botRead("chk_WeaponSc")
                .chk_TownF3.Checked = botRead("chk_TownF3")
                .chk_RespawnF4.Checked = botRead("chk_RespawnF4")
                .chk_GateF5.Checked = botRead("chk_GateF5")
                '// Oto TS
                .cmb_AutoTs.SelectedIndex = botRead("cmb_AutoTs")
                .chk_AutoTs.Checked = botRead("chk_AutoTs")

                '// GM Kontrol Sistemi
                .chk_checkerGM.Checked = botRead("chk_checkerGM")
                .txt_checkerNick.Text = botRead("txt_checkerNick")
                .rb_checkerChatBan.Checked = botRead("rb_checkerChatBan")
                .rb_checkerListBan.Checked = botRead("rb_checkerListBan")
                .cmb_checkerFunc.SelectedIndex = botRead("cmb_checkerFunc")
                .lst_checkerGM.Items.Clear()
                If (botRead("lst_checkerGMCount") > -1) Then
                    For gm As Integer = 0 To botRead("lst_checkerGMCount")
                        .lst_checkerGM.Items.Add(botRead("lst_checkerGM(" & gm & ")"))
                    Next
                End If

                '################ Atak Bölümü ################
                '// Warrior Atak List
                For wr As Integer = 0 To .lstv_AttackWar.Items.Count - 1
                    .lstv_AttackWar.Items().Item(wr).Checked = botRead("lstv_AttackWar(" & wr & ")")
                Next
                '// Rogue Atak List
                For rg As Integer = 0 To .lstv_AttackRog.Items.Count - 1
                    .lstv_AttackRog.Items().Item(rg).Checked = botRead("lstv_AttackRog(" & rg & ")")
                Next
                '// Mage Atak List
                For mg As Integer = 0 To .lstv_AttackMage.Items.Count - 1
                    .lstv_AttackMage.Items().Item(mg).Checked = botRead("lstv_AttackMage(" & mg & ")")
                Next
                .chk_attackArea.Checked = botRead("chk_attackArea")
                '// Priest Atak List
                For pr As Integer = 0 To .lstv_AttackPriest.Items.Count - 1
                    .lstv_AttackPriest.Items().Item(pr).Checked = botRead("lstv_AttackPriest(" & pr & ")")
                Next
                '// Chaos Atak List
                For ch As Integer = 0 To .lstv_AttackChaos.Items.Count - 1
                    .lstv_AttackChaos.Items().Item(ch).Checked = botRead("lstv_AttackChaos(" & ch & ")")
                Next
                .chk_chaosAttack.Checked = botRead("chk_chaosAttack")

                '// Mob Seçimi
                .cmb_chooseMob.SelectedIndex = botRead("cmb_chooseMob")
                .cmb_selectMob.SelectedIndex = botRead("cmb_selectMob")
                .chk_GenieStatu.Checked = botRead("chk_GenieStatu")
                .chk_runMob.Checked = botRead("chk_runMob")
                .chk_backToCent.Checked = botRead("chk_backToCent")
                .chk_tpMob.Checked = botRead("chk_tpMob")
                .txt_rundisMob.Text = botRead("txt_rundisMob")
                .lbl_centX.Text = botRead("lbl_centX")
                .lbl_centY.Text = botRead("lbl_centY")

                '// Atak Hızları
                .txt_tmrWarrior.Text = botRead("txt_tmrWarrior")
                .txt_tmrAssassin.Text = botRead("txt_tmrAssassin")
                .txt_tmrArchery.Text = botRead("txt_tmrArchery")
                .txt_tmrMage.Text = botRead("txt_tmrMage")
                .txt_tmrPriest.Text = botRead("txt_tmrPriest")

                '################ Priest Bölümü ################
                .txt_PriestHeal.Text = botRead("txt_PriestHeal")
                .cmb_PriestHeal.SelectedIndex = botRead("cmb_PriestHeal")
                .cmb_PriestBuff.SelectedIndex = botRead("cmb_PriestBuff")
                .cmb_PriestAcc.SelectedIndex = botRead("cmb_PriestAcc")
                .cmb_PriestRestore.SelectedIndex = botRead("cmb_PriestRestore")
                .cmb_PriestResis.SelectedIndex = botRead("cmb_PriestResis")
                .chk_PriestCure.Checked = botRead("chk_PriestCure")
                .chk_PriestStr.Checked = botRead("chk_PriestStr")

                .txt_PartyHeal.Text = botRead("txt_PartyHeal")
                .cmb_PartyHeal.SelectedIndex = botRead("cmb_PartyHeal")
                .cmb_PartyBuff.SelectedIndex = botRead("cmb_PartyBuff")
                .cmb_PartyAcc.SelectedIndex = botRead("cmb_PartyAcc")
                .cmb_PartyRestore.SelectedIndex = botRead("cmb_PartyRestore")
                .cmb_PartyResistans.SelectedIndex = botRead("cmb_PartyResistans")
                .chk_PartySTR.Checked = botRead("chk_PartySTR")
                .chk_PartyGrupHeal.Checked = botRead("chk_PartyGrupHeal")
                .txt_PartyGHealRow.Text = botRead("txt_PartyGHealRow")
                .txt_PartyGroupHealPoint.Text = botRead("txt_PartyGroupHealPoint")
                .txt_MinorPtRow.Text = botRead("txt_MinorPtRow")
                '################ Diğer Bölümü ################
                '// Atak Listesi
                .lst_AttackList.Items.Clear()
                For at As Integer = 0 To botRead("lst_AttackListCount") - 1
                    .lst_AttackList.Items.Add(botRead("lst_AttackList(" & at & ")"))
                Next
                .txt_LstMobName.Text = botRead("txt_LstMobNick")


                For il As Integer = 0 To Convert.ToInt32(botRead("lstv_UniqueCount")) - 1
                    Dim lstItem As ListViewItem = New ListViewItem(New String() {botRead("lstv_UniqueID(" & il & ")"), botRead("lstv_UniqueName(" & il & ")")})
                    .lstv_UniqueItem.Items.Add(lstItem)
                Next

                For ils As Integer = 0 To Convert.ToInt32(botRead("lstv_GarbageItemCount")) - 1
                    Dim lstItem As ListViewItem = New ListViewItem(New String() {botRead("lstv_GarbageItemID(" & ils & ")"), botRead("lstv_GarbageItemName(" & ils & ")")})
                    .lstv_GarbageItem.Items.Add(lstItem)
                Next

                '// Repair
                .chk_RprActive.Checked = botRead("chk_RprActive")
                .chk_RprHP.Checked = botRead("chk_RprHP")
                .chk_RprMP.Checked = botRead("chk_RprMP")
                .chk_RprWOLF.Checked = botRead("chk_RprWOLF")
                .chk_RprARROW.Checked = botRead("chk_RprARROW")
                .chk_RprTS.Checked = botRead("chk_RprTS")
                .chk_RprPOG.Checked = botRead("chk_RprPOD")
                .chk_RprHelmet.Checked = botRead("chk_RprHelmet")
                .chk_RprPauldron.Checked = botRead("chk_RprPauldron")
                .chk_RprPads.Checked = botRead("chk_RprPads")
                .chk_RprGauntlet.Checked = botRead("chk_RprGauntlet")
                .chk_RprBoots.Checked = botRead("chk_RprBoots")
                .chk_RprWeapon.Checked = botRead("chk_RprWeapon")
                .txt_RprHPPot.Text = botRead("txt_RprHPPot")
                .txt_RprMPPot.Text = botRead("txt_RprMPPot")
                .txt_RprWOLF.Text = botRead("txt_RprWOLF")
                .txt_RprARROW.Text = botRead("txt_RprARROW")
                .txt_RprTS.Text = botRead("txt_RprTS")
                .txt_RprPOG.Text = botRead("txt_RprPOG")
                .cmb_rprHPPot.SelectedIndex = botRead("cmb_rprHPPot")
                .cmb_rprMPPot.SelectedIndex = botRead("cmb_rprMPPot")


                .chk_EmptyItem.Checked = botRead("chk_EmptyItem")
                .chk_sellItem.Checked = botRead("chk_sellItem")

                'Repair Kordinat
                .lst_rprCoor.Items.Clear()
                For at As Integer = 0 To botRead("lst_rprCoorCount") - 1
                    .lst_rprCoor.Items.Add(botRead("lst_rprCoor(" & at & ")"))
                Next

                .cmb_rprMethod.Items.Clear()
                For cr As Integer = 0 To botRead("cmb_rprMethodCount") - 1
                    .cmb_rprMethod.Items.Add(botRead("cmb_rprMethod(" & cr & ")"))
                Next
                .cmb_rprMethod.SelectedIndex = botRead("cmb_rprMethodSelected")

                '################ Extra Bölümü ################
                '// Chat
                .cmb_ChatJob.SelectedIndex = botRead("cmb_ChatJob")
                .cmb_ChatStatus.SelectedIndex = botRead("cmb_ChatStatus")
                .chk_ChatActive.Checked = botRead("chk_ChatActive")
                .chk_PMJobLevel.Checked = botRead("chk_PMJobLevel")
                .txt_ChatSec.Text = botRead("txt_ChatSec")
                .txt_ChatLevel.Text = botRead("txt_ChatLevel")
                .txt_ChatText.Text = botRead("txt_ChatText")
                .cmb_Quests.SelectedIndex = botRead("cmb_Quests")

                '################ Kutu Bölümü ################
                '// Loot
                .chk_AutoLoot.Checked = botRead("chk_AutoLoot")
                .chk_runBox.Checked = botRead("chk_runBox")
                .rb_justMoney.Checked = botRead("rb_justMoney")
                .rb_allLoot.Checked = botRead("rb_allLoot")
                .rb_ListItem.Checked = botRead("rb_ListItem")
                .lbl_LootStatus.Text = botRead("lbl_LootStatus")
                .lbl_xCoorLoot.Text = botRead("lbl_xCoorLoot")
                .lbl_yCoorLoot.Text = botRead("lbl_yCoorLoot")
                .txt_boxRunDis.Text = botRead("txt_boxRunDis")
                .chk_RunBoxStopAttack.Checked = botRead("chk_RunBoxStopAttack")

                

                '################ Upgrade Bölümü ################
                .chk_UpIsActive.Checked = botRead("chk_UpIsActive")

                .chk_upAllCheck.Checked = botRead("chk_upAllCheck")
                .cmb_JobItem.SelectedIndex = botRead("cmb_JobItem")
                .cmb_UpScrool.SelectedIndex = botRead("cmb_UpScrool")
                .chk_Up7.Checked = botRead("chk_Up7")

                For ib As Integer = 0 To Convert.ToInt32(botRead("lst_buyItemsCount"))
                    .lst_buyItems.Items(ib) = botRead("lst_buyItemsText(" & ib & ")")
                Next

                For ip As Integer = 0 To Convert.ToInt32(botRead("chkl_UpItemsCount"))
                    .chkl_UpItems.Items.Item(ip) = botRead("chkl_UpItemsText(" & ip & ")")
                    .chkl_UpItems.SetItemChecked(ip, Convert.ToBoolean(botRead("chkl_UpItemsCheck(" & ip & ")")))
                Next
                .chk_InvFull.Checked = botRead("chk_InvFull")
                .chk_rprIsActive.Checked = botRead("chk_rprIsActive")
                .txt_tpX.Text = botRead("txt_tpX")
                .txt_tpY.Text = botRead("txt_tpY")

                '################ CS Plus Bölümü ################
                For ip As Integer = 0 To Convert.ToInt32(botRead("lst_walkAreaCount")) - 1
                    .lst_walkArea.Items.Add(botRead("lst_walkArea(" & ip & ")"))
                Next
                .chk_walkArea.Checked = botRead("chk_walkArea")
                'Respawn Kordinat
                .lst_goSlot.Items.Clear()
                For ats As Integer = 0 To botRead("lst_goSlotCount") - 1
                    .lst_goSlot.Items.Add(botRead("lst_goSlot(" & ats & ")"))
                Next

                .cmb_slotMethod.Items.Clear()
                For cre As Integer = 0 To botRead("cmb_slotMethodCount") - 1
                    .cmb_slotMethod.Items.Add(botRead("cmb_slotMethod(" & cre & ")"))
                Next
                .cmb_slotMethod.SelectedIndex = botRead("cmb_slotMethodSelected")
                If (String.IsNullOrEmpty(botRead("txt_MobAttackDistance"))) Then .txt_MobAttackDistance.Text = 55 Else .txt_MobAttackDistance.Text = botRead("txt_MobAttackDistance")

                .chk_normalAttack.Checked = botRead("chk_normalAttack")
                .chk_SpeedHackGame.Checked = botRead("chk_SpeedHackGame")
                .chk_shShiftButton.Checked = botRead("chk_shShiftButton")
                .tb_shValue.Value = botRead("tb_shValue")
                .chk_RprDcHP.Checked = botRead("chk_RprDcHP")
                .chk_RprDcMP.Checked = botRead("chk_RprDcMP")
                .txt_RprDcHPPot.Text = botRead("txt_RprDcHPPot")
                .txt_RprDcMPPot.Text = botRead("txt_RprDcMPPot")

                .cmb_SlotCheck.SelectedIndex = botRead("cmb_SlotCheck")
                .cmb_DCMethod.SelectedIndex = botRead("cmb_DCMethod")
                .chk_DcMethod.Checked = botRead("chk_DcMethod")
                .chk_SlotCheck.Checked = botRead("chk_SlotCheck")
                If (String.IsNullOrEmpty(botRead("txt_MultipleDis"))) Then .txt_MultipleDis.Text = 3 Else .txt_MultipleDis.Text = botRead("txt_MultipleDis")
                If (String.IsNullOrEmpty(botRead("chk_ArcheryCombo"))) Then .chk_ArcheryCombo.Checked = False Else .chk_ArcheryCombo.Checked = botRead("chk_ArcheryCombo")
                If (String.IsNullOrEmpty(botRead("chk_GarbageItem"))) Then .chk_GarbageItem.Checked = False Else .chk_GarbageItem.Checked = botRead("chk_GarbageItem")
                If (String.IsNullOrEmpty(botRead("chk_HourMethod"))) Then .chk_HourMethod.Checked = False Else .chk_HourMethod.Checked = botRead("chk_HourMethod")
                If (String.IsNullOrEmpty(botRead("cmb_Hour"))) Then .cmb_Hour.SelectedIndex = 0 Else .cmb_Hour.SelectedIndex = botRead("cmb_Hour")
                If (String.IsNullOrEmpty(botRead("cmb_Minute"))) Then .cmb_Minute.SelectedIndex = 0 Else .cmb_Minute.SelectedIndex = botRead("cmb_Minute")
                If (String.IsNullOrEmpty(botRead("cmb_HourMethod"))) Then .cmb_HourMethod.SelectedIndex = 0 Else .cmb_HourMethod.SelectedIndex = botRead("cmb_HourMethod")
                If (String.IsNullOrEmpty(botRead("chk_DeathWait"))) Then .chk_DeathWait.Checked = False Else .chk_DeathWait.Checked = botRead("chk_DeathWait")
                If (String.IsNullOrEmpty(botRead("txt_DeathWait"))) Then .txt_DeathWait.Text = 3 Else .txt_DeathWait.Text = botRead("txt_DeathWait")
                If (String.IsNullOrEmpty(botRead("chk_ComboLegal"))) Then .chk_ComboLegal.Checked = False Else .chk_ComboLegal.Checked = botRead("chk_ComboLegal")
                If (String.IsNullOrEmpty(botRead("chk_SummonParty"))) Then .chk_SummonParty.Checked = False Else .chk_SummonParty.Checked = botRead("chk_SummonParty")
                If (String.IsNullOrEmpty(botRead("chk_chatSummon"))) Then .chk_chatSummon.Checked = False Else .chk_chatSummon.Checked = botRead("chk_chatSummon")
                If (String.IsNullOrEmpty(botRead("txt_chatSummon"))) Then .txt_chatSummon.Text = "44" Else .txt_chatSummon.Text = botRead("txt_chatSummon")
            Catch ex As Exception
                goTxt("Ayar okuma bölümünde hata.", "Dosya kayıtlarında eksiklik olabilir.")
            End Try
        End With
    End Sub
    Public Sub saveBot()
        With frm_2Genel
            Try
                '################ Genel Bölümü ################
                '// Korunma
                botSave("chk_HpPot", .chk_HpPot.Checked)
                botSave("txt_HpPot", .txt_HpPot.Text)
                botSave("chk_MpPot", .chk_MpPot.Checked)
                botSave("txt_MpPot", .txt_MpPot.Text)
                botSave("rb_AutoPot", .rb_AutoPot.Checked)
                botSave("rb_RowPot", .rb_RowPot.Checked)
                botSave("chk_Minor", .chk_Minor.Checked)
                botSave("chk_MagicHammer", .chk_MagicHammer.Checked)
                botSave("chk_respawnChr", .chk_respawnChr.Checked)
                botSave("chk_BackSlot", .chk_BackSlot.Checked)
                botSave("txt_MinorRow", .txt_MinorRow.Text)
                botSave("txt_BackSlot", .txt_BackSlot.Text)

                '//Genel Fonksiyonlar
                botSave("chk_WallHack", .chk_WallHack.Checked)
                botSave("chk_DropSc", .chk_DropSc.Checked)
                botSave("chk_AttackSc", .chk_AttackSc.Checked)
                botSave("chk_ArmorSc", .chk_ArmorSc.Checked)
                botSave("chk_WeaponSc", .chk_WeaponSc.Checked)
                botSave("chk_TownF3", .chk_TownF3.Checked)
                botSave("chk_RespawnF4", .chk_RespawnF4.Checked)
                botSave("chk_GateF5", .chk_GateF5.Checked)

                '// Oto TS
                botSave("cmb_AutoTs", .cmb_AutoTs.SelectedIndex)
                botSave("chk_AutoTs", .chk_AutoTs.Checked)


                '// GM Kontrol Sistemi
                botSave("chk_checkerGM", .chk_checkerGM.Checked)
                botSave("txt_checkerNick", .txt_checkerNick.Text)
                botSave("rb_checkerListBan", .rb_checkerListBan.Checked)
                botSave("rb_checkerChatBan", .rb_checkerChatBan.Checked)
                botSave("cmb_checkerFunc", .cmb_checkerFunc.SelectedIndex)
                botSave("lst_checkerGMCount", .lst_checkerGM.Items.Count - 1)
                For gm As Integer = 0 To .lst_checkerGM.Items.Count - 1
                    botSave("lst_checkerGM(" & gm & ")", .lst_checkerGM.Items(gm).ToString())
                Next

                '################ Atak Bölümü ################
                '// Warrior Atak List
                For w As Integer = 0 To .lstv_AttackWar.Items.Count - 1
                    botSave("lstv_AttackWar(" & w & ")", .lstv_AttackWar.Items().Item(w).Checked)
                Next
                '// Rogue Atak List
                For r As Integer = 0 To .lstv_AttackRog.Items.Count - 1
                    botSave("lstv_AttackRog(" & r & ")", .lstv_AttackRog.Items().Item(r).Checked)
                Next
                '// Mage Atak List
                For m As Integer = 0 To .lstv_AttackMage.Items.Count - 1
                    botSave("lstv_AttackMage(" & m & ")", .lstv_AttackMage.Items().Item(m).Checked)
                Next
                botSave("chk_attackArea", .chk_attackArea.Checked)
                '// Priest Atak List
                For m As Integer = 0 To .lstv_AttackPriest.Items.Count - 1
                    botSave("lstv_AttackPriest(" & m & ")", .lstv_AttackPriest.Items().Item(m).Checked)
                Next
                '// Chaos Atak List
                For chk As Integer = 0 To .lstv_AttackChaos.Items.Count - 1
                    botSave("lstv_AttackChaos(" & chk & ")", .lstv_AttackChaos.Items().Item(chk).Checked)
                Next
                botSave("chk_chaosAttack", .chk_chaosAttack.Checked)

                '// Mob Seçimi
                botSave("cmb_selectMob", .cmb_selectMob.SelectedIndex)
                botSave("cmb_chooseMob", .cmb_chooseMob.SelectedIndex)
                botSave("chk_GenieStatu", .chk_GenieStatu.Checked)

                '//Mob İşlem
                botSave("chk_runMob", .chk_runMob.Checked)
                botSave("chk_backToCent", .chk_backToCent.Checked)
                botSave("chk_tpMob", .chk_tpMob.Checked)
                botSave("txt_rundisMob", .txt_rundisMob.Text)
                botSave("lbl_centX", .lbl_centX.Text)
                botSave("lbl_centY", .lbl_centY.Text)

                '// Atak Hızları
                botSave("txt_tmrWarrior", .txt_tmrWarrior.Text)
                botSave("txt_tmrAssassin", .txt_tmrAssassin.Text)
                botSave("txt_tmrArchery", .txt_tmrArchery.Text)
                botSave("txt_tmrMage", .txt_tmrMage.Text)
                botSave("txt_tmrPriest", .txt_tmrPriest.Text)

                '################ Priest Bölümü ################
                botSave("txt_PriestHeal", .txt_PriestHeal.Text)
                botSave("cmb_PriestHeal", .cmb_PriestHeal.SelectedIndex)
                botSave("cmb_PriestBuff", .cmb_PriestBuff.SelectedIndex)
                botSave("cmb_PriestAcc", .cmb_PriestAcc.SelectedIndex)
                botSave("cmb_PriestRestore", .cmb_PriestRestore.SelectedIndex)
                botSave("cmb_PriestResis", .cmb_PriestResis.SelectedIndex)
                botSave("chk_PriestCure", .chk_PriestCure.Checked)
                botSave("chk_PriestStr", .chk_PriestStr.Checked)

                botSave("txt_PartyHeal", .txt_PartyHeal.Text)
                botSave("cmb_PartyHeal", .cmb_PartyHeal.SelectedIndex)
                botSave("cmb_PartyBuff", .cmb_PartyBuff.SelectedIndex)
                botSave("cmb_PartyAcc", .cmb_PartyAcc.SelectedIndex)
                botSave("cmb_PartyRestore", .cmb_PartyRestore.SelectedIndex)
                botSave("cmb_PartyResistans", .cmb_PartyResistans.SelectedIndex)
                botSave("chk_PartySTR", .chk_PartySTR.Checked)
                botSave("chk_PartyGrupHeal", .chk_PartyGrupHeal.Checked)
                botSave("txt_PartyGHealRow", .txt_PartyGHealRow.Text)
                botSave("txt_PartyGroupHealPoint", .txt_PartyGroupHealPoint.Text)
                botSave("txt_MinorPtRow", .txt_MinorPtRow.Text)

                '################ Diğer Bölümü ################
                '// Atak Listesi
                botSave("lst_AttackListCount", .lst_AttackList.Items.Count)
                For iat As Integer = 0 To .lst_AttackList.Items.Count - 1
                    botSave("lst_AttackList(" & iat & ")", .lst_AttackList.Items(iat).ToString())
                Next
                botSave("txt_LstMobName", .txt_LstMobName.Text)

                '// Repair
                botSave("cmb_rprHPPot", .cmb_rprHPPot.SelectedIndex)
                botSave("cmb_rprMPPot", .cmb_rprMPPot.SelectedIndex)
                botSave("chk_RprActive", .chk_RprActive.Checked)
                botSave("chk_RprHP", .chk_RprHP.Checked)
                botSave("chk_RprMP", .chk_RprMP.Checked)
                botSave("chk_RprWOLF", .chk_RprWOLF.Checked)
                botSave("chk_RprARROW", .chk_RprARROW.Checked)
                botSave("chk_RprTS", .chk_RprTS.Checked)
                botSave("chk_RprPOD", .chk_RprPOG.Checked)
                botSave("chk_RprHelmet", .chk_RprHelmet.Checked)
                botSave("chk_RprPauldron", .chk_RprPauldron.Checked)
                botSave("chk_RprPads", .chk_RprPads.Checked)
                botSave("chk_RprGauntlet", .chk_RprGauntlet.Checked)
                botSave("chk_RprBoots", .chk_RprBoots.Checked)
                botSave("chk_RprWeapon", .chk_RprWeapon.Checked)
                botSave("txt_RprHPPot", .txt_RprHPPot.Text)
                botSave("txt_RprMPPot", .txt_RprMPPot.Text)
                botSave("txt_RprWolf", .txt_RprWOLF.Text)
                botSave("txt_RprArrow", .txt_RprARROW.Text)
                botSave("txt_RprTS", .txt_RprTS.Text)
                botSave("txt_RprPOG", .txt_RprPOG.Text)
                botSave("chk_InvFull", .chk_InvFull.Checked)
                botSave("chk_EmptyItem", .chk_EmptyItem.Checked)
                botSave("chk_sellItem", .chk_sellItem.Checked)
                botSave("chk_rprIsActive", .chk_rprIsActive.Checked)


                'Repair Kordinat
                botSave("lst_rprCoorCount", .lst_rprCoor.Items.Count)
                For irp As Integer = 0 To .lst_rprCoor.Items.Count - 1
                    botSave("lst_rprCoor(" & irp & ")", .lst_rprCoor.Items(irp).ToString())
                Next

                botSave("cmb_rprMethodCount", .cmb_rprMethod.Items.Count)
                For icr As Integer = 0 To .cmb_rprMethod.Items.Count - 1
                    botSave("cmb_rprMethod(" & icr & ")", .cmb_rprMethod.Items(icr).ToString())
                Next
                botSave("cmb_rprMethodSelected", .cmb_rprMethod.SelectedIndex)


                '################ Extra Bölümü ################
                '// Chat
                botSave("cmb_ChatJob", .cmb_ChatJob.SelectedIndex)
                botSave("cmb_ChatStatus", .cmb_ChatStatus.SelectedIndex)
                botSave("chk_ChatActive", .chk_ChatActive.Checked)
                botSave("chk_PMJobLevel", .chk_PMJobLevel.Checked)
                botSave("txt_ChatSec", .txt_ChatSec.Text)
                botSave("txt_ChatLevel", .txt_ChatLevel.Text)
                botSave("txt_ChatText", .txt_ChatText.Text)
                '// Görevler
                botSave("cmb_Quests", .cmb_Quests.SelectedIndex)


                '################ Kutu Bölümü ################
                botSave("chk_AutoLoot", .chk_AutoLoot.Checked)
                botSave("chk_runBox", .chk_runBox.Checked)
                botSave("chk_TPLoot", .chk_TPLoot.Checked)

                botSave("rb_justMoney", .rb_justMoney.Checked)
                botSave("rb_allLoot", .rb_allLoot.Checked)
                botSave("rb_ListItem", .rb_ListItem.Checked)
                botSave("rb_Just8", .rb_Just8.Checked)
                botSave("chk_RunBoxStopAttack", .chk_RunBoxStopAttack.Checked)
                '// Değerli itemler
                botSave("lstv_UniqueCount", .lstv_UniqueItem.Items.Count)
                For iun As Integer = 0 To .lstv_UniqueItem.Items.Count - 1
                    botSave("lstv_UniqueID(" & iun & ")", .lstv_UniqueItem.Items(iun).Text)
                    botSave("lstv_UniqueName(" & iun & ")", .lstv_UniqueItem.Items(iun).SubItems(1).Text)
                Next

                '// Değerli itemler
                botSave("lstv_GarbageItemCount", .lstv_GarbageItem.Items.Count)
                For iun As Integer = 0 To .lstv_GarbageItem.Items.Count - 1
                    botSave("lstv_GarbageItemID(" & iun & ")", .lstv_GarbageItem.Items(iun).Text)
                    botSave("lstv_GarbageItemName(" & iun & ")", .lstv_GarbageItem.Items(iun).SubItems(1).Text)
                Next

                '// Upgrade
                botSave("chk_UpIsActive", .chk_UpIsActive.Checked)
                botSave("chk_upAllCheck", .chk_upAllCheck.Checked)
                botSave("cmb_JobItem", .cmb_JobItem.SelectedIndex)
                botSave("cmb_UpScrool", .cmb_UpScrool.SelectedIndex)

                botSave("lst_buyItemsCount", .lst_buyItems.Items.Count - 1)
                For ibi As Integer = 0 To .lst_buyItems.Items.Count - 1
                    botSave("lst_buyItemsText(" & ibi & ")", .lst_buyItems.Items(ibi).ToString())
                Next

                botSave("chkl_UpItemsCount", .chkl_UpItems.Items.Count - 1)
                For iupu As Integer = 0 To .chkl_UpItems.Items.Count - 1
                    botSave("chkl_UpItemsText(" & iupu & ")", .chkl_UpItems.Items(iupu).ToString())
                    botSave("chkl_UpItemsCheck(" & iupu & ")", .chkl_UpItems.GetItemChecked(iupu))
                Next

                botSave("lbl_LootStatus", .lbl_LootStatus.Text)
                botSave("lbl_xCoorLoot", .lbl_xCoorLoot.Text)
                botSave("lbl_yCoorLoot", .lbl_yCoorLoot.Text)
                botSave("txt_boxRunDis", .txt_boxRunDis.Text)
                botSave("chk_Up7", .chk_Up7.Checked)
                botSave("txt_tpX", .txt_tpX.Text)
                botSave("txt_tpY", .txt_tpY.Text)

                '################ CS Plus Bölümü ################
                '// Atak Listesi
                botSave("lst_walkAreaCount", .lst_walkArea.Items.Count)
                For iat As Integer = 0 To .lst_walkArea.Items.Count - 1
                    botSave("lst_walkArea(" & iat & ")", .lst_walkArea.Items(iat).ToString())
                Next
                botSave("chk_walkArea", .chk_walkArea.Checked)


                'Respawn Kordinat
                botSave("lst_goSlotCount", .lst_goSlot.Items.Count)
                For irp As Integer = 0 To .lst_goSlot.Items.Count - 1
                    botSave("lst_goSlot(" & irp & ")", .lst_goSlot.Items(irp).ToString())
                Next

                botSave("cmb_slotMethodCount", .cmb_slotMethod.Items.Count)
                For icr As Integer = 0 To .cmb_slotMethod.Items.Count - 1
                    botSave("cmb_slotMethod(" & icr & ")", .cmb_slotMethod.Items(icr).ToString())
                Next
                botSave("cmb_slotMethodSelected", .cmb_slotMethod.SelectedIndex)
                botSave("txt_MobAttackDistance", .txt_MobAttackDistance.Text)
                botSave("chk_normalAttack", .chk_normalAttack.Checked)
                botSave("chk_SpeedHackGame", .chk_SpeedHackGame.Checked)
                botSave("chk_shShiftButton", .chk_shShiftButton.Checked)
                botSave("tb_shValue", .tb_shValue.Value)

                botSave("chk_RprDcHP", .chk_RprDcHP.Checked)
                botSave("chk_RprDcMP", .chk_RprDcMP.Checked)
                botSave("txt_RprDcHPPot", .txt_RprDcHPPot.Text)
                botSave("txt_RprDcMPPot", .txt_RprDcMPPot.Text)

                botSave("cmb_SlotCheck", .cmb_SlotCheck.SelectedIndex)
                botSave("cmb_DCMethod", .cmb_DCMethod.SelectedIndex)
                botSave("chk_DcMethod", .chk_DcMethod.Checked)
                botSave("chk_SlotCheck", .chk_SlotCheck.Checked)
                botSave("txt_MultipleDis", .txt_MultipleDis.Text)
                botSave("chk_ArcheryCombo", .chk_ArcheryCombo.Checked)

                botSave("chk_GarbageItem", .chk_GarbageItem.Checked)
                botSave("chk_HourMethod", .chk_HourMethod.Checked)
                botSave("cmb_Hour", .cmb_Hour.SelectedIndex)
                botSave("cmb_Minute", .cmb_Minute.SelectedIndex)
                botSave("cmb_HourMethod", .cmb_HourMethod.SelectedIndex)
                botSave("chk_DeathWait", .chk_DeathWait.Checked)
                botSave("txt_DeathWait", .txt_DeathWait.Text)
                botSave("chk_ComboLegal", .chk_ComboLegal.Checked)
                botSave("chk_SummonParty", .chk_SummonParty.Checked)
                botSave("txt_chatSummon", .txt_chatSummon.Text)
                botSave("chk_chatSummon", .chk_chatSummon.Checked)
            Catch ex As Exception
                goTxt("Ayar kayıt bölümünde hata.", "Hata bilinmiyor.")
            End Try
        End With
    End Sub
#End Region
End Module
