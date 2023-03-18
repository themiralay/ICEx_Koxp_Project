Option Explicit On
Module Client
    Public Sub BuffKontrol(ByVal BuffRow As Integer)
        Dim SkillMana As Integer, SkillCode As String
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
                Exit Sub
        End Select
        If Cli.CharBuffStatu() = False And Cli.getCharHP > 0 Then
            If SkillCode <> "oto" Then
                If Cli.getCharMP >= SkillMana And Cli.getCharSkillPoint(2) >= CInt(SkillCode.Substring(1, 2)) Then
                    Cli.PriestPartySkill(Cli.getCharLongID, SkillCode)
                End If
            Else 'Oto Buff
                '//Superioris
                If Cli.getCharSkillPoint(2) >= 78 And Cli.getCharMP >= 690 Then
                    If (Cli.getCharUndyHp(Cli.getCharMaxHP) >= 2500) Then
                        Cli.PriestPartySkill(Cli.getCharLongID, "654")
                    Else
                        Cli.PriestPartySkill(Cli.getCharLongID, "678")
                    End If
                    Exit Sub
                End If
                '//Massiveness
                If Cli.getCharSkillPoint(2) >= 57 And Cli.getCharMP >= 360 Then
                    If (Cli.getCharUndyHp(Cli.getCharMaxHP) >= 1500) Then
                        Cli.PriestPartySkill(Cli.getCharLongID, "654")
                    Else
                        Cli.PriestPartySkill(Cli.getCharLongID, "657")
                    End If
                    Exit Sub
                End If
                '//Heapness
                If Cli.getCharSkillPoint(2) >= 54 And Cli.getCharMP >= 300 Then
                    If (Cli.getCharUndyHp(Cli.getCharMaxHP) >= 1200) Then
                        Cli.PriestPartySkill(Cli.getCharLongID, "654")
                    Else
                        Cli.PriestPartySkill(Cli.getCharLongID, "655")
                    End If
                    Exit Sub
                End If
                '//Mightness
                If Cli.getCharSkillPoint(2) >= 42 And Cli.getCharMP >= 240 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "642")
                    Exit Sub
                End If
                '//Hardness
                If Cli.getCharSkillPoint(2) >= 33 And Cli.getCharMP >= 120 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "633")
                    Exit Sub
                End If
                '//Strong
                If Cli.getCharSkillPoint(2) >= 24 And Cli.getCharMP >= 60 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "624")
                    Exit Sub
                End If
                '//Brave
                If Cli.getCharSkillPoint(2) >= 15 And Cli.getCharMP >= 30 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "615")
                    Exit Sub
                End If
                '//Grace
                If Cli.getCharSkillPoint(2) >= 6 And Cli.getCharMP >= 15 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "606")
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Public Sub AccKontrol(ByVal AccRow As Integer)
        Dim SkillMana As Integer, SkillCode As String
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
        If Cli.CharAccStatu() = False And Cli.getCharHP > 0 Then
            If SkillCode <> "oto" Then
                If Cli.getCharMP >= SkillMana And Cli.getCharSkillPoint(2) >= CInt(SkillCode.Substring(1, 2)) Then
                    Cli.PriestPartySkill(Cli.getCharLongID, SkillCode)
                End If
            Else 'Oto Acc
                '//Guard
                If Cli.getCharSkillPoint(2) >= 76 And Cli.getCharMP >= 300 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "676")
                    Exit Sub
                End If
                '//Peel
                If Cli.getCharSkillPoint(2) >= 60 And Cli.getCharMP >= 150 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "660")
                    Exit Sub
                End If
                '//Protector
                If Cli.getCharSkillPoint(2) >= 51 And Cli.getCharMP >= 100 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "651")
                    Exit Sub
                End If
                '//Barrier
                If Cli.getCharSkillPoint(2) >= 39 And Cli.getCharMP >= 80 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "639")
                    Exit Sub
                End If
                '//Shield
                If Cli.getCharSkillPoint(2) >= 30 And Cli.getCharMP >= 80 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "630")
                    Exit Sub
                End If
                '//Armor
                If Cli.getCharSkillPoint(2) >= 21 And Cli.getCharMP >= 40 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "621")
                    Exit Sub
                End If
                '//Shell
                If Cli.getCharSkillPoint(2) >= 12 And Cli.getCharMP >= 20 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "612")
                    Exit Sub
                End If
                '//Skin
                If Cli.getCharSkillPoint(2) >= 3 And Cli.getCharMP >= 10 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "603")
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Public Sub ResistKontrol(ByVal ResistRow As Integer)
        Dim SkillMana As Integer, SkillCode As String
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
        If Cli.CharResisStatu() = False And Cli.getCharHP > 0 Then
            If SkillCode <> "oto" Then
                If Cli.getCharMP >= SkillMana And Cli.getCharSkillPoint(2) >= CInt(SkillCode.Substring(1, 2)) Then
                    Cli.PriestPartySkill(Cli.getCharLongID, SkillCode)
                End If
            Else 'Oto Resist
                '//Fresh
                If Cli.getCharSkillPoint(2) >= 45 And Cli.getCharMP >= 60 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "645")
                    Exit Sub
                End If
                '//Calm
                If Cli.getCharSkillPoint(2) >= 36 And Cli.getCharMP >= 40 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "636")
                    Exit Sub
                End If
                '//Bright
                If Cli.getCharSkillPoint(2) >= 27 And Cli.getCharMP >= 30 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "627")
                    Exit Sub
                End If
                '//Resist
                If Cli.getCharSkillPoint(2) >= 9 And Cli.getCharMP >= 15 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "609")
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Public Sub RestoreKontrol(ByVal RestRow As Integer)
        Dim SkillMana As Integer, SkillCode As String
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
        If Cli.CharRestoreStatu() = False And Cli.getCharHP > 0 Then
            If SkillCode <> "oto" Then
                If Cli.getCharMP >= SkillMana And Cli.getCharSkillPoint(2) >= CInt(SkillCode.Substring(1, 2)) Then
                    Cli.PriestPartySkill(Cli.getCharLongID, SkillCode)
                End If
            Else 'Oto Resist
                '//Past
                If Cli.getCharSkillPoint(1) >= 80 And Cli.getCharMP >= 1400 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "580")
                    Exit Sub
                End If
                '//Past Rec
                If Cli.getCharSkillPoint(1) >= 75 And Cli.getCharMP >= 1200 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "575")
                    Exit Sub
                End If
                '//Critical
                If Cli.getCharSkillPoint(1) >= 70 And Cli.getCharMP >= 1000 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "570")
                    Exit Sub
                End If
                '//Superior
                If Cli.getCharSkillPoint(1) >= 48 And Cli.getCharMP >= 348 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "548")
                    Exit Sub
                End If
                '//Massive
                If Cli.getCharSkillPoint(1) >= 39 And Cli.getCharMP >= 375 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "539")
                    Exit Sub
                End If
                '//Great
                If Cli.getCharSkillPoint(1) >= 31 And Cli.getCharMP >= 200 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "531")
                    Exit Sub
                End If
                '//Major
                If Cli.getCharSkillPoint(1) >= 22 And Cli.getCharMP >= 100 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "522")
                    Exit Sub
                End If
                '//Restore
                If Cli.getCharSkillPoint(1) >= 13 And Cli.getCharMP >= 30 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "513")
                    Exit Sub
                End If
                '//Light
                If Cli.getCharSkillPoint(1) >= 3 And Cli.getCharMP >= 25 Then
                    Cli.PriestPartySkill(Cli.getCharLongID, "503")
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Public Sub PriestHealKontrol(ByVal HealRow As Integer, ByVal HealYuzde As Integer)
        Dim SkillCode As String, SkillMana As Integer
        Select Case HealRow
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
        If Cli.getLastHP < 60 And Cli.getCharHP <= 0 Then
            Exit Sub
        End If
        If SkillCode <> "oto" And SkillCode <> "otomax" Then
            If Cli.getCharSkillPoint(1) >= CInt(SkillCode.Substring(1, 2)) And Cli.getCharMP >= SkillMana And (Cli.getCharHP <= ((Cli.getCharMaxHP * HealYuzde) / 100)) Then
                Cli.PriestPartySkill(Cli.getCharLongID, SkillCode)
                Exit Sub
            End If
        ElseIf (SkillCode = "oto") Then
            If Cli.getCharMP >= 320 And (Cli.getLastHP >= 1920) And Cli.getCharSkillPoint(1) >= 45 Then Cli.PriestPartySkill(Cli.getCharLongID, "545") : Exit Sub
            If Cli.getCharMP >= 160 And (Cli.getLastHP >= 960) And Cli.getCharSkillPoint(1) >= 36 Then Cli.PriestPartySkill(Cli.getCharLongID, "536") : Exit Sub
            If Cli.getCharMP >= 80 And (Cli.getLastHP >= 720) And Cli.getCharSkillPoint(1) >= 27 Then Cli.PriestPartySkill(Cli.getCharLongID, "527") : Exit Sub
            If Cli.getCharMP >= 40 And (Cli.getLastHP >= 360) And Cli.getCharSkillPoint(1) >= 18 Then Cli.PriestPartySkill(Cli.getCharLongID, "518") : Exit Sub
            If Cli.getCharMP >= 20 And (Cli.getLastHP >= 240) And Cli.getCharSkillPoint(1) >= 9 Then Cli.PriestPartySkill(Cli.getCharLongID, "509") : Exit Sub
            If Cli.getCharMP >= 10 And (Cli.getLastHP >= 60) And Cli.getCharSkillPoint(1) >= 0 And Cli.getCharLevel() >= 10 Then Cli.PriestPartySkill(Cli.getCharLongID, "500") : Exit Sub
        End If
    End Sub
    Public Sub StrKontrol(ByVal StrStatu As Boolean)
        If StrStatu = False Then Exit Sub
        If Cli.CharStrStatu() = True And Cli.getCharHP > 0 Then
            If Cli.getCharMP >= 10 And Cli.getCharLevel >= 4 Then
                Cli.PriestPartySkill(Cli.getCharLongID, "004")
                Exit Sub
            End If
        End If
    End Sub
    Public Sub CureKontrol(ByVal CureStatu As Boolean)
        If CureStatu = False Then Exit Sub
        If Cli.CharCureStatu() = False And Cli.getCharHP > 0 Then
            If Cli.getCharMP >= 60 And Cli.PartySkill(6) <= 0 And Cli.getCharSkillPoint(1) >= 25 Then
                Cli.PriestPartySkill(Cli.getCharLongID, "525")
                Cli.PartySkill(6) = 300
                Exit Sub
            End If
        End If

        If Cli.CharDiseStatu() = False And Cli.getCharHP > 0 Then
            If Cli.getCharMP >= 60 And Cli.PartySkill(7) <= 0 And Cli.getCharSkillPoint(1) >= 35 Then
                Cli.PriestPartySkill(Cli.getCharLongID, "535")
                Cli.PartySkill(7) = 300
                Exit Sub
            End If
        End If
    End Sub

    Public Sub getItems(ByVal strJob As String, ByRef lstItem As ListBox)
        Dim Items() As String
        Select Case strJob
            Case "Warrior"
                Items = {"Quilted Pauldron", "Leather Pads", "Leather Caps", "Leather Gloves", "Leather Shoes", "Half Plate Pauldron", "Half Plate Pads", "Half Plate Helmet", "Half Plate Gauntlet", "Half Plate Boots", "Plate Armor Pauldron", "Plate Armor Pads", "Plate Armor Helmet", "Plate Armor Gauntlet", "Plate Armor Boots"}
            Case "Rogue"
                Items = {"Rogue Shirt", "Rogue Pads", "Rogue Cap", "Rogue Gloves", "Rogue Shoes", "Rogue Half Plate Pauldron", "Rogue Half Plate Pads", "Rogue Helmet", "Rogue Gauntlet", "Rogue Boots", "Rogue Plate Armor Pauldron", "Rogue Plate Armor Pads", "Rogue Plate Helmet", "Rogue Plate Gauntlet", "Rogue Plate Boots"}
            Case "Mage"
                Items = {"Mage Cotton Robe", "Mage Cloth Pants", "Mage Hat", "Mage Gloves", "Mage Shoes", "Mage Linen Robe", "Mage Linen Pants", "Mage Linen Cap", "Mage Leather Gloves", "Mage Leather Boots", "Mage Silk Robe", "Mage Silk Pants", "Mage Helmet", "Mage Hard Leather Gloves", "Mage Hard Leather Boots"}
            Case "Priest"
                Items = {"Fabric Coat", "Fabric Pants", "Priest Cap", "Priest Gloves", "Priest Shoes", "Silk Coat", "Silk Pants", "Priest Helmet", "Priest Gauntlet", "Priest Boots", "Priest Plate Pauldron", "Priest Plate Pads", "Priest Plate Helmet", "Priest Plate Gauntlet", "Priest Plate Boots"}
            Case Else
                Items = {"Weapon Breaker"}
        End Select
        lstItem.Items.Clear()
        For Each Item As String In Items
            lstItem.Items.Add(Item)
        Next
    End Sub
    Public Function getCoinToItem(ByVal SellItemID As String)
        Dim Items() As String, Coins() As Integer
        Items = {"Quilted Pauldron", "Leather Pads", "Leather Caps", "Leather Gloves", "Leather Shoes", "Half Plate Pauldron", "Half Plate Pads", "Half Plate Helmet", "Half Plate Gauntlet", "Half Plate Boots", "Plate Armor Pauldron", "Plate Armor Pads", "Plate Armor Helmet", "Plate Armor Gauntlet", "Plate Armor Boots", "Rogue Shirt", "Rogue Pads", "Rogue Cap", "Rogue Gloves", "Rogue Shoes", "Rogue Half Plate Pauldron", "Rogue Half Plate Pads", "Rogue Helmet", "Rogue Gauntlet", "Rogue Boots", "Rogue Plate Armor Pauldron", "Rogue Plate Armor Pads", "Rogue Plate Helmet", "Rogue Plate Gauntlet", "Rogue Plate Boots", "Mage Cotton Robe", "Mage Cloth Pants", "Mage Hat", "Mage Gloves", "Mage Shoes", "Mage Linen Robe", "Mage Linen Pants", "Mage Linen Cap", "Mage Leather Gloves", "Mage Leather Boots", "Mage Silk Robe", "Mage Silk Pants", "Mage Helmet", "Mage Hard Leather Gloves", "Mage Hard Leather Boots", "Fabric Coat", "Fabric Pants", "Priest Cap", "Priest Gloves", "Priest Shoes", "Silk Coat", "Silk Pants", "Priest Helmet", "Priest Gauntlet", "Priest Boots", "Priest Plate Pauldron", "Priest Plate Pads", "Priest Plate Helmet", "Priest Plate Gauntlet", "Priest Plate Boots", "Weapon Breaker", "Low Class Upgrade Scroll", "Middle Class Upgrade Scroll", "High Class Upgrade Scroll", "Blessed Upgrade Scroll"}
        Coins = {1232, 739, 492, 246, 246, 9900, 5940, 3960, 1980, 1980, 140800, 84480, 56320, 28160, 28160, 985, 589, 393, 195, 195, 7920, 4752, 3168, 1584, 1584, 112640, 67584, 45056, 22528, 22528, 1108, 664, 442, 220, 220, 8910, 5346, 3564, 1782, 1782, 126720, 76032, 50688, 25344, 25344, 862, 517, 343, 171, 171, 6930, 4158, 2772, 1386, 1386, 98560, 9136, 39424, 19712, 19712, 629706, 11000, 110000, 264000, 2640000}
        For i As Integer = 0 To 64
            If (Items(i) = SellItemID) Then
                Return Coins(i)
            End If
        Next
        Return 0
    End Function
    Public Sub setItems(ByVal ItemName As String, ByRef clstUpItem As CheckedListBox)
        For i As Integer = 0 To 26
            If (clstUpItem.Items.Item(i).ToString().Length < 5) Then
                clstUpItem.Items.Item(i) = "(" & (i + 1).ToString() & ") " & ItemName
                Exit For
            End If
        Next
    End Sub
End Module
