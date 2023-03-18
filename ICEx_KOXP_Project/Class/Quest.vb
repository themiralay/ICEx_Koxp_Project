Option Explicit On
Module Quest
    Public Potrang(4) As Long, Sentinel_Patrick(19) As Long, GM_Daughter(4) As Long, TrustMiss(2) As Long, Jed(7) As Long
    Public Osmand(4) As Long, Berret(4) As Long, Lazer(41) As Long, Shymer(1) As Long, Melverick(23) As Long, ChitinQuest(19) As Long, NameLessWarrior(1) As Long
    Public BlackSmithHeppa(3) As Long, Quest60(79), LardOrc(4) As Long, QuestEslant(29) As Long
    Public Sub GorevKod()
        '######################################
        'Potrang
        Potrang(0) = 7310
        Potrang(1) = 7317
        Potrang(2) = 7324
        Potrang(3) = 7330
        Potrang(4) = 7368
        'Sentinel Patrick
        Sentinel_Patrick(0) = 49
        Sentinel_Patrick(1) = 64
        Sentinel_Patrick(2) = 3323
        Sentinel_Patrick(3) = 3335
        Sentinel_Patrick(4) = 3343
        Sentinel_Patrick(5) = 3355
        Sentinel_Patrick(6) = 3363
        Sentinel_Patrick(7) = 5275
        Sentinel_Patrick(8) = 3373
        Sentinel_Patrick(9) = 3383
        Sentinel_Patrick(10) = 3393
        Sentinel_Patrick(11) = 5282
        Sentinel_Patrick(12) = 5289
        Sentinel_Patrick(13) = 5303
        Sentinel_Patrick(14) = 5298
        Sentinel_Patrick(15) = 5310
        Sentinel_Patrick(16) = 8151
        Sentinel_Patrick(17) = Sentinel_Patrick(16) + 5 'Mage
        Sentinel_Patrick(18) = Sentinel_Patrick(17) + 5 'Priest
        Sentinel_Patrick(19) = Sentinel_Patrick(16) - 5 'Warrior
        'GM Daughter
        GM_Daughter(0) = 9
        GM_Daughter(1) = 57
        GM_Daughter(2) = 96
        GM_Daughter(3) = 103
        GM_Daughter(4) = 743
        'Trust Görevleri
        TrustMiss(0) = 7359
        TrustMiss(1) = 7351
        TrustMiss(2) = 4062
        'Blacksmith Heppa
        BlackSmithHeppa(0) = 363
        BlackSmithHeppa(1) = 657
        BlackSmithHeppa(2) = 407
        BlackSmithHeppa(3) = 10222
        'Jed
        Jed(0) = 7370
        Jed(1) = 7376
        Jed(2) = 7392
        Jed(3) = 7400
        Jed(4) = 7406
        Jed(5) = 7412
        Jed(6) = 7418
        Jed(7) = 7428

        'Osmand
        Osmand(0) = 305
        Osmand(1) = 513
        Osmand(2) = 333
        Osmand(3) = 823
        Osmand(4) = 9945

        'Berret
        Berret(0) = 534
        Berret(1) = 9961
        Berret(2) = 9973
        Berret(3) = 9985
        Berret(4) = 9997

        'Lazer
        Lazer(0) = 489 'All
        Lazer(1) = 5317 ' All
        'Rogue
        Lazer(2) = 7923 '7933
        Lazer(3) = 7965
        Lazer(4) = 8019
        Lazer(5) = 8061
        Lazer(6) = 8109
        Lazer(7) = 8193
        Lazer(8) = 8235
        Lazer(9) = 8277
        Lazer(10) = 8319
        Lazer(11) = 8361

        'Mage
        Lazer(12) = Lazer(2) + 5
        Lazer(13) = Lazer(3) + 5
        Lazer(14) = Lazer(4) + 5
        Lazer(15) = Lazer(5) + 5
        Lazer(16) = Lazer(6) + 5
        Lazer(17) = Lazer(7) + 5
        Lazer(18) = Lazer(8) + 5
        Lazer(19) = Lazer(9) + 5
        Lazer(20) = Lazer(10) + 5
        Lazer(21) = Lazer(11) + 5

        'Priest
        Lazer(22) = Lazer(12) + 5
        Lazer(23) = Lazer(13) + 5
        Lazer(24) = Lazer(14) + 5
        Lazer(25) = Lazer(15) + 5
        Lazer(26) = Lazer(16) + 5
        Lazer(27) = Lazer(17) + 5
        Lazer(28) = Lazer(18) + 5
        Lazer(29) = Lazer(19) + 5
        Lazer(30) = Lazer(20) + 5
        Lazer(31) = Lazer(21) + 5

        'Warrior
        Lazer(32) = Lazer(2) - 5
        Lazer(33) = Lazer(3) - 5
        Lazer(34) = Lazer(4) - 5
        Lazer(35) = Lazer(5) - 5
        Lazer(36) = Lazer(6) - 5
        Lazer(37) = Lazer(7) - 5
        Lazer(38) = Lazer(8) - 5
        Lazer(39) = Lazer(9) - 5
        Lazer(40) = Lazer(10) - 5
        Lazer(41) = Lazer(11) - 5

        'NamelessWarrior
        NameLessWarrior(0) = 3056
        NameLessWarrior(1) = 7013


        'Shymer
        Shymer(0) = 9875
        Shymer(1) = 9899
        '#############################################
        'Lüferson Görevleri
        'Rogue
        Melverick(0) = 8403
        Melverick(1) = 8517
        Melverick(2) = 8559
        Melverick(3) = 8601
        Melverick(4) = 8643
        Melverick(5) = 8475
        'Mage
        Melverick(6) = Melverick(0) + 5
        Melverick(7) = Melverick(1) + 5
        Melverick(8) = Melverick(2) + 5
        Melverick(9) = Melverick(3) + 5
        Melverick(10) = Melverick(4) + 5
        Melverick(11) = Melverick(5) + 5
        'Priest
        Melverick(12) = Melverick(6) + 5
        Melverick(13) = Melverick(7) + 5
        Melverick(14) = Melverick(8) + 5
        Melverick(15) = Melverick(9) + 5
        Melverick(16) = Melverick(10) + 5
        Melverick(17) = Melverick(11) + 5
        'Warrior
        Melverick(18) = Melverick(0) - 5
        Melverick(19) = Melverick(1) - 5
        Melverick(20) = Melverick(2) - 5
        Melverick(21) = Melverick(3) - 5
        Melverick(22) = Melverick(4) - 5
        Melverick(23) = Melverick(5) - 5

        '#############################################
        'Chitin Görevleri
        'Rogue
        ChitinQuest(0) = 8771
        ChitinQuest(1) = 8813
        ChitinQuest(2) = 8895
        ChitinQuest(3) = 8853
        ChitinQuest(4) = 8727
        'Mage
        ChitinQuest(5) = ChitinQuest(0) + 5
        ChitinQuest(6) = ChitinQuest(1) + 5
        ChitinQuest(7) = ChitinQuest(2) + 5
        ChitinQuest(8) = ChitinQuest(3) + 5
        ChitinQuest(9) = ChitinQuest(4) + 5
        'Priest
        ChitinQuest(10) = ChitinQuest(5) + 5
        ChitinQuest(11) = ChitinQuest(6) + 5
        ChitinQuest(12) = ChitinQuest(7) + 5
        ChitinQuest(13) = ChitinQuest(8) + 5
        ChitinQuest(14) = ChitinQuest(9) + 5
        'Warrior
        ChitinQuest(15) = ChitinQuest(0) - 5
        ChitinQuest(16) = ChitinQuest(1) - 5
        ChitinQuest(17) = ChitinQuest(2) - 5
        ChitinQuest(18) = ChitinQuest(3) - 5
        ChitinQuest(19) = ChitinQuest(4) - 5

        '#############################################
        'Lard ORC

        LardOrc(0) = 8685
        LardOrc(1) = LardOrc(0) + 5
        LardOrc(2) = LardOrc(1) + 5
        LardOrc(3) = LardOrc(0) - 5

        '#############################################
        '92230000
        'Rogue
        Quest60(0) = 9477
        Quest60(1) = 8937
        Quest60(2) = 8974
        Quest60(3) = 8986
        Quest60(4) = 9645 '+
        Quest60(5) = 8998
        Quest60(6) = 9010
        Quest60(7) = 9022
        Quest60(8) = 9561 '+
        Quest60(9) = 9034
        Quest60(10) = 9519 '+
        Quest60(11) = 9603 '+
        Quest60(12) = 9046
        Quest60(13) = 9058
        Quest60(14) = 9094
        Quest60(15) = 9082
        Quest60(16) = 9118 '+
        Quest60(17) = 9106 'NULL
        Quest60(18) = 9115 'NULL
        Quest60(19) = 9364 'NULL
        'Quest60(20) = 9070
        'Mage
        Quest60(20) = Quest60(0) + 5
        Quest60(21) = Quest60(1) + 5
        Quest60(22) = Quest60(2)
        Quest60(23) = Quest60(3)
        Quest60(24) = Quest60(4) + 5
        Quest60(25) = Quest60(5)
        Quest60(26) = Quest60(6)
        Quest60(27) = Quest60(7)
        Quest60(28) = Quest60(8) + 5
        Quest60(29) = Quest60(9)
        Quest60(30) = Quest60(10) + 5
        Quest60(31) = Quest60(11) + 5
        Quest60(32) = Quest60(12)
        Quest60(33) = Quest60(13)
        Quest60(34) = Quest60(14)
        Quest60(35) = Quest60(15)
        Quest60(36) = Quest60(16) + 5
        Quest60(37) = Quest60(17)
        Quest60(38) = Quest60(18) + 5
        Quest60(39) = Quest60(19) + 5
        'Priest
        Quest60(40) = Quest60(20) + 10
        Quest60(41) = Quest60(21) + 10
        Quest60(42) = Quest60(22)
        Quest60(43) = Quest60(23)
        Quest60(44) = Quest60(24) + 5
        Quest60(45) = Quest60(25)
        Quest60(46) = Quest60(26)
        Quest60(47) = Quest60(27)
        Quest60(48) = Quest60(28) + 5
        Quest60(49) = Quest60(29)
        Quest60(50) = Quest60(30) + 5
        Quest60(51) = Quest60(31) + 5
        Quest60(52) = Quest60(32)
        Quest60(53) = Quest60(33)
        Quest60(54) = Quest60(34)
        Quest60(55) = Quest60(35)
        Quest60(56) = Quest60(36) + 5
        Quest60(57) = Quest60(37)
        Quest60(58) = Quest60(38) + 5
        Quest60(59) = Quest60(39) + 5
        'Warrior
        Quest60(60) = Quest60(0)
        Quest60(61) = Quest60(1)
        Quest60(62) = Quest60(2)
        Quest60(63) = Quest60(3)
        Quest60(64) = Quest60(4) - 5
        Quest60(65) = Quest60(5)
        Quest60(66) = Quest60(6)
        Quest60(67) = Quest60(7)
        Quest60(68) = Quest60(8) - 5
        Quest60(69) = Quest60(9)
        Quest60(70) = Quest60(10) - 5
        Quest60(71) = Quest60(11) - 5
        Quest60(72) = Quest60(12)
        Quest60(73) = Quest60(13)
        Quest60(74) = Quest60(14)
        Quest60(75) = Quest60(15)
        Quest60(76) = Quest60(16) - 5
        Quest60(77) = Quest60(17)
        Quest60(78) = Quest60(18) - 5
        Quest60(79) = Quest60(19) - 5

        QuestEslant(0) = 4501
        QuestEslant(1) = 4521
        QuestEslant(2) = 4541
        QuestEslant(3) = 4561
        QuestEslant(4) = 5973
        QuestEslant(5) = 5953
        QuestEslant(6) = 5933
        QuestEslant(7) = 5913
        QuestEslant(8) = 5893
        QuestEslant(9) = 5873
        QuestEslant(10) = 5853
        QuestEslant(11) = 5833
        QuestEslant(12) = 4701
        QuestEslant(13) = 4691
        QuestEslant(14) = 4681
        QuestEslant(15) = 4671
        QuestEslant(16) = 4661
        QuestEslant(17) = 4651
        QuestEslant(18) = 4641
        QuestEslant(19) = 4631
        QuestEslant(20) = 4621
        QuestEslant(21) = 4601
        QuestEslant(22) = 4581
        QuestEslant(23) = 9724
        QuestEslant(24) = 9448
        QuestEslant(25) = 9424
        QuestEslant(26) = 9412
        QuestEslant(27) = 9400
        QuestEslant(28) = 12847
        QuestEslant(29) = 12979

    End Sub

End Module
