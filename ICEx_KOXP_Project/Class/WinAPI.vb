Option Explicit On
Imports System.Runtime.InteropServices
Module WinAPI
    Public BotForSteam As Boolean = False
#Region "Tanımlamalar"
    '==== Pointer ====
    Public KO_PTR_CHR As Long
    Public KO_PTR_DLG As Long
    Public KO_PTR_PKT As Long
    Public KO_SND_FNC As Long
    Public KO_FMBS As Long
    Public KO_FPBS As Long
    Public KO_FNC_ISEN As Long
    Public KO_CHAR_SERV As Long
    Public KO_OTO_LOGIN_PTR As Long
    Public KO_OTO_LOGIN_01 As Long
    Public KO_OTO_LOGIN_02 As Long
    Public KO_OTO_LOGIN_03 As Long
    Public KO_OTO_LOGIN_04 As Long
    Public KO_OTO_BTN_PTR As Long
    Public KO_BTN_LEFT As Long
    Public KO_BTN_RIGHT As Long
    Public KO_BTN_LOGIN As Long
    Public KO_FLDB As Long
    Public KO_FNCZ As Long
    Public KO_FNCB As Long
    Public KO_ITOB As Long
    Public KO_ITEB As Long
    Public KO_RECV_FNC As Long
    Public KO_RECV_PTR As Long
    '==== Offsetler ====
    Public KO_OFF_CLASS As Long
    Public KO_OFF_NT As Long
    Public KO_OFF_MOVE As Long
    Public KO_OFF_MOVEType As Long
    Public KO_OFF_GoX As Long
    Public KO_OFF_GoZ As Long
    Public KO_OFF_GoY As Long
    Public KO_OFF_X As Long
    Public KO_OFF_Z As Long
    Public KO_OFF_Y As Long
    Public KO_OFF_ID As Long
    Public KO_OFF_WH As Long
    Public KO_OFF_MCOR As Long

    Public KO_OFF_PtBase As Long
    Public KO_OFF_PtCount As Long
    Public KO_OFF_Pt As Long

    Public KO_OFF_MAXEXP As Long
    Public KO_OFF_EXP As Long
    Public KO_OFF_MOB As Long
    Public KO_OFF_ZONE As Long
    Public KO_OFF_NAMELEN As Long
    Public KO_OFF_NAME As Long
    Public KO_OFF_GOLD As Long
    Public KO_OFF_MAXMP As Long
    Public KO_OFF_MP As Long
    Public KO_OFF_MAXHP As Long
    Public KO_OFF_HP As Long
    Public KO_OFF_LEVEL As Long
    Public KO_OFF_POINTStat As Long
    Public KO_OFF_StatSTR As Long
    Public KO_OFF_StatHP As Long
    Public KO_OFF_StatDEX As Long
    Public KO_OFF_StatINT As Long
    Public KO_OFF_StatMP As Long
    Public KO_OFF_SBARBase As Long
    Public KO_OFF_BSkPoint As Long
    Public KO_OFF_SPoint1 As Long
    Public KO_OFF_SPoint2 As Long
    Public KO_OFF_SPoint3 As Long
    Public KO_OFF_SPoint4 As Long
    '==== Fonksiyon Offsetleri ====
    Public KO_OFF_ITEMB As Long
    Public KO_OFF_ITEMS As Long
    Public KO_OFF_BANKB As Long
    Public KO_OFF_BANKS As Long
    Public KO_OFF_BANKCONT As Long
    Public KO_OFF_SKILLBASE As Long
    Public KO_OFF_SKILLID As Long
    '==== Pet Offsetleri ====
    Public KO_OFF_PET_ID As Long
    Public KO_OFF_PET_BASE As Long
    Public KO_OFF_PET_MAXHP As Long
    Public KO_OFF_PET_HP As Long
    Public KO_OFF_PET_MAXMP As Long
    Public KO_OFF_PET_MP As Long
    Public KO_OFF_PET_LEVEL As Long
    Public KO_OFF_PET_HUNGRY As Long


    '==== İtem Silme ====
    Public KO_ITEMDESCALL As Long
    Public KO_ITEMDES As Long
    Public KO_ITEMDES2 As Long
    Public KO_FAKE_ITEM As Long

    Public KO_SH_HOOK As Long
    Public KO_SH_VALUE As Long
    Public KO_SPD_HOOK As Long
    Public KO_SUB_ADDR0 As Long
    Public KO_SUB_ADDR1 As Long
    Public KO_PTR_NRML As Long
    Public KO_SMMB As Long
    Public KO_SMMB_FNC As Long

    Public KO_BYPASS As Long
    Public KO_BYPASS_ADR As Long
    'Hook Define
    Public WIZ_CHAT
    Public WIZ_GETLOOT
    Public WIZ_LOOTOPEN

    'Game Define
    Public KO_OFF_ITEMROW As Integer
    Public KO_OFF_INVROW As Integer
    Public KO_OFF_INVCOUNT As Integer
    Public KO_OFF_SWIFT As Long
    Public KO_OFF_MCORX As Long
    Public KO_OFF_MCORY As Long
    Public KO_OFF_MCORZ As Long
#End Region
#Region "Knight Online Version"
    Public Sub USKOActive()
        ' = = = = Pointer  = = = =
        KO_PTR_CHR = &HE08E00
        KO_PTR_DLG = &HDEFEB0
        KO_PTR_PKT = &HDEFE7C
        KO_SND_FNC = &H48CA10
        KO_FMBS = &H4EE2F0
        KO_FPBS = &H4EF260
        KO_FNC_ISEN = &H55B090
        KO_CHAR_SERV = &HC3F804
        KO_OTO_LOGIN_PTR = &HDEFE8C
        KO_OTO_LOGIN_01 = &H4D8150
        KO_OTO_LOGIN_02 = &H4D1620
        KO_OTO_LOGIN_03 = &H4D10E0
        KO_OTO_LOGIN_04 = &H4D5A50
        KO_OTO_BTN_PTR = &HDEFEAC
        KO_BTN_LEFT = &H4C1060
        KO_BTN_RIGHT = &H4C1300
        KO_BTN_LOGIN = &H4BD890
        KO_FLDB = &HE08DFC
        KO_FNCZ = &H51E5E0
        KO_FNCB = &H51E750
        KO_ITOB = &HE08C34
        KO_ITEB = &HE08C3C
        KO_ITEB = &HE0582C
        ' = = = = Offsetler  = = = =
        KO_OFF_CLASS = &H6B0
        KO_OFF_NT = &H6A8
        KO_OFF_MOVE = &HF90
        KO_OFF_MOVEType = &H3F0
        KO_OFF_GoX = &HF9C
        KO_OFF_GoZ = &HFA0
        KO_OFF_GoY = &HFA4
        KO_OFF_X = &HD8
        KO_OFF_Z = &HDC
        KO_OFF_Y = &HE0
        KO_OFF_ID = &H680
        KO_OFF_WH = &H6C0
        KO_OFF_MCOR = &H420

        KO_OFF_PtBase = &H1E8
        KO_OFF_PtCount = &H300
        KO_OFF_Pt = &H2FC

        KO_OFF_MAXEXP = &HB70
        KO_OFF_EXP = &HB78
        KO_OFF_MOB = &H644
        KO_OFF_ZONE = &HC00
        KO_OFF_NAMELEN = &H698
        KO_OFF_NAME = &H688
        KO_OFF_GOLD = &HB6C
        KO_OFF_MAXMP = &HB5C
        KO_OFF_MP = &HB60
        KO_OFF_MAXHP = &H6B8
        KO_OFF_HP = &H6BC
        KO_OFF_LEVEL = &H6B4
        KO_OFF_POINTStat = &H6B0
        KO_OFF_StatSTR = &HB94
        KO_OFF_StatHP = &HB9C
        KO_OFF_StatDEX = &HBA4
        KO_OFF_StatINT = &HBAC
        KO_OFF_StatMP = &HBB6
        KO_OFF_SBARBase = &H1EC
        KO_OFF_BSkPoint = &H16C
        KO_OFF_SPoint1 = &H180
        KO_OFF_SPoint2 = &H184
        KO_OFF_SPoint3 = &H188
        KO_OFF_SPoint4 = &H18C
        ' = = = = Fonksiyon Offsetleri  = = = =
        KO_OFF_ITEMB = &H1B8
        KO_OFF_ITEMS = &H20C
        KO_OFF_BANKB = &H208
        KO_OFF_BANKS = &H128
        KO_OFF_BANKCONT = &HFC
        KO_OFF_SKILLBASE = &H1D0
        KO_OFF_SKILLID = &H12C
        ' = = = = Pet Offsetleri  = = = =
        KO_OFF_PET_ID = &HFD8
        KO_OFF_PET_BASE = &H3A4
        KO_OFF_PET_HP = &H19E
        KO_OFF_PET_MAXHP = KO_OFF_PET_HP - 2
        KO_OFF_PET_MP = &H1A2
        KO_OFF_PET_MAXMP = KO_OFF_PET_MP - 2
        KO_OFF_PET_LEVEL = &H199
        KO_OFF_PET_HUNGRY = &H155
        ' = = = = İtem Silme  = = = =
        KO_ITEMDESCALL = &H5D5550
        KO_ITEMDES = &HDF3360
        KO_ITEMDES2 = &HDF3230
        KO_FAKE_ITEM = &H56A7D0

        KO_SH_HOOK = &H4E27CB
        KO_SH_VALUE = &HB4C390
        KO_SPD_HOOK = KO_SH_HOOK + &H9D
        KO_SUB_ADDR0 = &H889310
        KO_SUB_ADDR1 = &H576020
        KO_PTR_NRML = &HDEFE88
        KO_SMMB = &HE08CF8
        KO_SMMB_FNC = &H4F1450

        KO_BYPASS = &HC984F8
        KO_BYPASS_ADR = &HD6B66C
        'Hook Define
        WIZ_CHAT = &H10
        WIZ_GETLOOT = &H23
        WIZ_LOOTOPEN = &H24

        'Game Define
        KO_OFF_ITEMROW = 1
        KO_OFF_INVROW = 15
        KO_OFF_INVCOUNT = 42
        KO_OFF_SWIFT = &H7C6
        KO_OFF_MCORX = &H7C
        KO_OFF_MCORY = &H84
        KO_OFF_MCORZ = &H80
    End Sub
    Public Sub STEAMActive()
        '= = = =  Pointer = = = = 
        KO_PTR_CHR = &HE18340
        KO_PTR_DLG = &HDFF3E4
        KO_PTR_PKT = &HDFF3B0
        KO_SND_FNC = &H499500
        KO_FMBS = &H4FA3E0
        KO_FPBS = &H4FB350
        KO_FNC_ISEN = &H54CF60
        KO_CHAR_SERV = &HC4C8A4
        KO_OTO_LOGIN_PTR = &HDFF3C0
        KO_OTO_LOGIN_01 = &H4E55E0
        KO_OTO_LOGIN_02 = &H4DEB60
        KO_OTO_LOGIN_03 = &H4DE620
        KO_OTO_LOGIN_04 = &H4E2F90
        KO_OTO_BTN_PTR = &HDFF3E0
        KO_BTN_LEFT = &H4CE430
        KO_BTN_RIGHT = &H4CE6D0
        KO_BTN_LOGIN = &H4CAC60
        KO_FLDB = &HE1833C
        KO_FNCZ = &H529EA0
        KO_FNCB = &H52A010
        KO_ITOB = &HE18174
        KO_ITEB = &HE1817C

        '= = = =  Offsetler = = =
        KO_OFF_CLASS = &H6B0
        KO_OFF_NT = &H6A8
        KO_OFF_MOVE = &HF90
        KO_OFF_MOVEType = &H3F0
        KO_OFF_GoX = &HF9C
        KO_OFF_GoZ = &HFA0
        KO_OFF_GoY = &HFA4
        KO_OFF_X = &HD8
        KO_OFF_Z = &HDC
        KO_OFF_Y = &HE0
        KO_OFF_ID = &H680
        KO_OFF_WH = &H6C0
        KO_OFF_MCOR = &H40C

        KO_OFF_PtBase = &H1E8
        KO_OFF_PtCount = &H300
        KO_OFF_Pt = &H2FC

        KO_OFF_MAXEXP = &HB70
        KO_OFF_EXP = &HB78
        KO_OFF_MOB = &H644
        KO_OFF_ZONE = &HC00
        KO_OFF_NAMELEN = &H698
        KO_OFF_NAME = &H688
        KO_OFF_GOLD = &HB6C
        KO_OFF_MAXMP = &HB5C
        KO_OFF_MP = &HB60
        KO_OFF_MAXHP = &H6B8
        KO_OFF_HP = &H6BC
        KO_OFF_LEVEL = &H6B4
        KO_OFF_POINTStat = &H6B0
        KO_OFF_StatSTR = &HB94
        KO_OFF_StatHP = &HB9C
        KO_OFF_StatDEX = &HBA4
        KO_OFF_StatINT = &HBAC
        KO_OFF_StatMP = &HBB6
        KO_OFF_SBARBase = &H1EC
        KO_OFF_BSkPoint = &H16C
        KO_OFF_SPoint1 = &H180
        KO_OFF_SPoint2 = &H184
        KO_OFF_SPoint3 = &H188
        KO_OFF_SPoint4 = &H18C
        '= = = =  Fonksiyon Offsetleri = = = = 
        KO_OFF_ITEMB = &H1B8
        KO_OFF_ITEMS = &H20C
        KO_OFF_BANKB = &H208
        KO_OFF_BANKS = &H128
        KO_OFF_BANKCONT = &HFC
        KO_OFF_SKILLBASE = &H1D0
        KO_OFF_SKILLID = &H12C
        ' = = = = Pet Offsetleri  = = = =
        KO_OFF_PET_ID = &HFD8
        KO_OFF_PET_BASE = &H3A4
        KO_OFF_PET_MAXHP = &H19C
        KO_OFF_PET_HP = KO_OFF_PET_MAXHP - 2
        KO_OFF_PET_MAXMP = KO_OFF_PET_HP - 2
        KO_OFF_PET_MP = KO_OFF_PET_MAXMP - 2
        KO_OFF_PET_LEVEL = &H199
        KO_OFF_PET_HUNGRY = &H155

        '= = = =  İtem Silme = = = = 
        KO_ITEMDESCALL = &H5E1220
        KO_ITEMDES = &HE028A8
        KO_ITEMDES2 = &HE02778
        KO_FAKE_ITEM = &H575AD0
        KO_SH_HOOK = &H4EE8BB
        KO_SH_VALUE = &HB553E0
        KO_SPD_HOOK = &H4EE8BB + &H9D
        KO_SUB_ADDR0 = &H88CBC0
        KO_SUB_ADDR1 = &H581320
        KO_PTR_NRML = &HDFF3BC
        KO_SMMB = &HE18238
        KO_SMMB_FNC = &H4FD5B0


        'Hook Define
        WIZ_CHAT = &H10
        WIZ_GETLOOT = &H23
        WIZ_LOOTOPEN = &H24

        'Game Define
        KO_OFF_ITEMROW = 1
        KO_OFF_INVROW = 15
        KO_OFF_INVCOUNT = 42
        KO_OFF_SWIFT = &H7C6
        KO_OFF_MCORX = &H7C
        KO_OFF_MCORY = &H84
        KO_OFF_MCORZ = &H80
    End Sub
    Public Sub USKOActiveSRG()
        ' = = = = Pointer  = = = =
        KO_PTR_CHR = &HE02D40
        KO_PTR_DLG = &HDEA1C4
        KO_PTR_PKT = &HDEA190
        KO_SND_FNC = &H48C760
        KO_FMBS = &H4EBE00
        KO_FPBS = &H4ECD70
        KO_FNC_ISEN = &H558940
        KO_CHAR_SERV = &HC3A7EC
        KO_OTO_LOGIN_PTR = &HDEA1A0
        KO_OTO_LOGIN_01 = &H4D5C60
        KO_OTO_LOGIN_02 = &H4CF130
        KO_OTO_LOGIN_03 = &H4CEBF0
        KO_OTO_LOGIN_04 = &H4D3560
        KO_OTO_BTN_PTR = &HDEA1C0
        KO_BTN_LEFT = &H4BEB70
        KO_BTN_RIGHT = &H4BEE10
        KO_BTN_LOGIN = &H4BB3A0
        KO_FLDB = &HE02D3C
        KO_FNCZ = &H51BED0
        KO_FNCB = &H51C040
        KO_ITOB = &HE02B74
        KO_ITEB = &HE02B7C
        ' = = = = Offsetler  = = = =
        KO_OFF_CLASS = &H6B0
        KO_OFF_NT = &H6A8
        KO_OFF_MOVE = &HF90
        KO_OFF_MOVEType = &H3F0
        KO_OFF_GoX = &HF9C
        KO_OFF_GoZ = &HFA0
        KO_OFF_GoY = &HFA4
        KO_OFF_X = &HD8
        KO_OFF_Z = &HDC
        KO_OFF_Y = &HE0
        KO_OFF_ID = &H680
        KO_OFF_WH = &H6C0
        KO_OFF_MCOR = &H420

        KO_OFF_PtBase = &H1E8
        KO_OFF_PtCount = &H300
        KO_OFF_Pt = &H2FC

        KO_OFF_MAXEXP = &HB70
        KO_OFF_EXP = &HB78
        KO_OFF_MOB = &H644
        KO_OFF_ZONE = &HC00
        KO_OFF_NAMELEN = &H698
        KO_OFF_NAME = &H688
        KO_OFF_GOLD = &HB6C
        KO_OFF_MAXMP = &HB5C
        KO_OFF_MP = &HB60
        KO_OFF_MAXHP = &H6B8
        KO_OFF_HP = &H6BC
        KO_OFF_LEVEL = &H6B4
        KO_OFF_POINTStat = &H6B0
        KO_OFF_StatSTR = &HB94
        KO_OFF_StatHP = &HB9C
        KO_OFF_StatDEX = &HBA4
        KO_OFF_StatINT = &HBAC
        KO_OFF_StatMP = &HBB6
        KO_OFF_SBARBase = &H1EC
        KO_OFF_BSkPoint = &H16C
        KO_OFF_SPoint1 = &H180
        KO_OFF_SPoint2 = &H184
        KO_OFF_SPoint3 = &H188
        KO_OFF_SPoint4 = &H18C
        ' = = = = Fonksiyon Offsetleri  = = = =
        KO_OFF_ITEMB = &H1B8
        KO_OFF_ITEMS = &H20C
        KO_OFF_BANKB = &H208
        KO_OFF_BANKS = &H128
        KO_OFF_BANKCONT = &HFC
        KO_OFF_SKILLBASE = &H1D0
        KO_OFF_SKILLID = &H12C
        ' = = = = Pet Offsetleri  = = = =
        KO_OFF_PET_ID = &HFD8
        KO_OFF_PET_BASE = &H3A4
        KO_OFF_PET_HP = &H19E
        KO_OFF_PET_MAXHP = KO_OFF_PET_HP - 2
        KO_OFF_PET_MP = &H1A2
        KO_OFF_PET_MAXMP = KO_OFF_PET_MP - 2
        KO_OFF_PET_LEVEL = &H199
        KO_OFF_PET_HUNGRY = &H155
        ' = = = = İtem Silme  = = = =
        KO_ITEMDESCALL = &H5D2D00
        KO_ITEMDES = &HDED670
        KO_ITEMDES2 = &HDED540
        KO_FAKE_ITEM = &H568080
        KO_SH_HOOK = &H4E02DB
        KO_SH_VALUE = &HB48260
        KO_SPD_HOOK = &H4E02DB + &H9D
        KO_SUB_ADDR0 = &H8866C0
        KO_SUB_ADDR1 = &H5738D0
        KO_PTR_NRML = &HDEA19C
        KO_SMMB = &HE02C38
        KO_SMMB_FNC = &H4EEF60
        'Hook Define
        WIZ_CHAT = &H10
        WIZ_GETLOOT = &H23
        WIZ_LOOTOPEN = &H24

        'Game Define
        KO_OFF_ITEMROW = 0
        KO_OFF_INVROW = 15
        KO_OFF_INVCOUNT = 42
        KO_OFF_SWIFT = &H7C6
        KO_OFF_MCORX = &H7C
        KO_OFF_MCORY = &H84
        KO_OFF_MCORZ = &H80
    End Sub
    Public Sub STEAMActiveSRG()
        '= = = =  Pointer = = = = 
        KO_PTR_CHR = &HE14F30
        KO_PTR_DLG = &HDFBFE4
        KO_PTR_PKT = &HDFBFB0
        KO_SND_FNC = &H499580
        KO_FMBS = &H4F80A0
        KO_FPBS = &H4F9010
        KO_FNC_ISEN = &H54ABC0
        KO_CHAR_SERV = &HC497E4
        KO_OTO_LOGIN_PTR = &HDFBFC0
        KO_OTO_LOGIN_01 = &H4E32B0
        KO_OTO_LOGIN_02 = &H4DC830
        KO_OTO_LOGIN_03 = &H4DC2F0
        KO_OTO_LOGIN_04 = &H4E0C60
        KO_OTO_BTN_PTR = &HDFBFE0
        KO_BTN_LEFT = &H4CC210
        KO_BTN_RIGHT = &H4CC4B0
        KO_BTN_LOGIN = &H4C8A40
        KO_FLDB = &HE14F2C
        KO_FNCZ = &H527B20
        KO_FNCB = &H527C90
        KO_ITOB = &HE14D64
        KO_RECV_FNC = &H547260
        KO_RECV_PTR = &HB530D0

        '= = = =  Offsetler = = =
        KO_OFF_CLASS = &H6B0
        KO_OFF_NT = &H6A8
        KO_OFF_MOVE = &HF90
        KO_OFF_MOVEType = &H3F0
        KO_OFF_GoX = &HF9C
        KO_OFF_GoZ = &HFA0
        KO_OFF_GoY = &HFA4
        KO_OFF_X = &HD8
        KO_OFF_Z = &HDC
        KO_OFF_Y = &HE0
        KO_OFF_ID = &H680
        KO_OFF_WH = &H6C0
        KO_OFF_MCOR = &H40C

        KO_OFF_PtBase = &H1E8
        KO_OFF_PtCount = &H300
        KO_OFF_Pt = &H2FC

        KO_OFF_MAXEXP = &HB70
        KO_OFF_EXP = &HB78
        KO_OFF_MOB = &H644
        KO_OFF_ZONE = &HC00
        KO_OFF_NAMELEN = &H698
        KO_OFF_NAME = &H688
        KO_OFF_GOLD = &HB6C
        KO_OFF_MAXMP = &HB5C
        KO_OFF_MP = &HB60
        KO_OFF_MAXHP = &H6B8
        KO_OFF_HP = &H6BC
        KO_OFF_LEVEL = &H6B4
        KO_OFF_POINTStat = &H6B0
        KO_OFF_StatSTR = &HB94
        KO_OFF_StatHP = &HB9C
        KO_OFF_StatDEX = &HBA4
        KO_OFF_StatINT = &HBAC
        KO_OFF_StatMP = &HBB6
        KO_OFF_SBARBase = &H1EC
        KO_OFF_BSkPoint = &H16C
        KO_OFF_SPoint1 = &H180
        KO_OFF_SPoint2 = &H184
        KO_OFF_SPoint3 = &H188
        KO_OFF_SPoint4 = &H18C
        '= = = =  Fonksiyon Offsetleri = = = = 
        KO_OFF_ITEMB = &H1B8
        KO_OFF_ITEMS = &H20C
        KO_OFF_BANKB = &H208
        KO_OFF_BANKS = &H128
        KO_OFF_BANKCONT = &HFC
        KO_OFF_SKILLBASE = &H1D0
        KO_OFF_SKILLID = &H12C
        ' = = = = Pet Offsetleri  = = = =
        KO_OFF_PET_ID = &HFD8
        KO_OFF_PET_BASE = &H3A4
        KO_OFF_PET_MAXHP = &H19C
        KO_OFF_PET_HP = KO_OFF_PET_MAXHP - 2
        KO_OFF_PET_MAXMP = KO_OFF_PET_HP - 2
        KO_OFF_PET_MP = KO_OFF_PET_MAXMP - 2
        KO_OFF_PET_LEVEL = &H199
        KO_OFF_PET_HUNGRY = &H155

        '= = = =  İtem Silme = = = = 
        KO_ITEMDESCALL = &H5DEE80
        KO_ITEMDES = &HDFF498
        KO_ITEMDES2 = &HDFF368
        KO_FAKE_ITEM = &H573730

        KO_SH_HOOK = &H4EC58B
        KO_SH_VALUE = &HB52310
        KO_SPD_HOOK = KO_SH_HOOK + &H9D

        KO_SUB_ADDR0 = &H88A520
        KO_SUB_ADDR1 = &H57EF80
        KO_PTR_NRML = &HDFBFBC
        KO_SMMB = &HE14E28
        KO_SMMB_FNC = &H4FB270 '50B87C
        'KO_SMMB_FNC = &H4EF210

        KO_BYPASS = &HB4B1A0 'B4B1A0
        KO_BYPASS_ADR = &HD793A4


        'Hook Define
        WIZ_CHAT = &H10
        WIZ_GETLOOT = &H23
        WIZ_LOOTOPEN = &H24

        'Game Define
        KO_OFF_ITEMROW = 1
        KO_OFF_INVROW = 15
        KO_OFF_INVCOUNT = 42
        KO_OFF_SWIFT = &H7C6
        KO_OFF_MCORX = &H7C
        KO_OFF_MCORY = &H84
        KO_OFF_MCORZ = &H80
    End Sub
    Public Sub USKOActiveOsmanli()
        ' = = = = Pointer  = = = =
        KO_PTR_CHR = &HE046D0
        KO_PTR_DLG = &HDEBA98
        KO_PTR_PKT = &HDEBA64
        KO_SND_FNC = &H48C900
        KO_FMBS = &H4EBFA0
        KO_FPBS = &H4ECF10
        KO_FNC_ISEN = &H558B10
        KO_CHAR_SERV = &HC3B7EC
        KO_OTO_LOGIN_PTR = &HDEBA74
        KO_OTO_LOGIN_01 = &H4D5E00
        KO_OTO_LOGIN_02 = &H4CF2D0
        KO_OTO_LOGIN_03 = &H4CED90
        KO_OTO_LOGIN_04 = &H4D3700
        KO_OTO_BTN_PTR = &HDEBA94
        KO_BTN_LEFT = &H4BED10
        KO_BTN_RIGHT = &H4BEFB0
        KO_BTN_LOGIN = &H4BB540
        KO_FLDB = &HE046CC
        KO_FNCZ = &H51C090
        KO_FNCB = &H51C200
        KO_ITOB = &HE04504
        KO_ITEB = &HE0450C
        ' = = = = Offsetler  = = = =
        KO_OFF_CLASS = &H6B0
        KO_OFF_NT = &H6A8
        KO_OFF_MOVE = &HF90
        KO_OFF_MOVEType = &H3F0
        KO_OFF_GoX = &HF9C
        KO_OFF_GoZ = &HFA0
        KO_OFF_GoY = &HFA4
        KO_OFF_X = &HD8
        KO_OFF_Z = &HDC
        KO_OFF_Y = &HE0
        KO_OFF_ID = &H680
        KO_OFF_WH = &H6C0
        KO_OFF_MCOR = &H420

        KO_OFF_PtBase = &H1E8
        KO_OFF_PtCount = &H300
        KO_OFF_Pt = &H2FC

        KO_OFF_MAXEXP = &HB70
        KO_OFF_EXP = &HB78
        KO_OFF_MOB = &H644
        KO_OFF_ZONE = &HC00
        KO_OFF_NAMELEN = &H698
        KO_OFF_NAME = &H688
        KO_OFF_GOLD = &HB6C
        KO_OFF_MAXMP = &HB5C
        KO_OFF_MP = &HB60
        KO_OFF_MAXHP = &H6B8
        KO_OFF_HP = &H6BC
        KO_OFF_LEVEL = &H6B4
        KO_OFF_POINTStat = &H6B0
        KO_OFF_StatSTR = &HB94
        KO_OFF_StatHP = &HB9C
        KO_OFF_StatDEX = &HBA4
        KO_OFF_StatINT = &HBAC
        KO_OFF_StatMP = &HBB6
        KO_OFF_SBARBase = &H1EC
        KO_OFF_BSkPoint = &H16C
        KO_OFF_SPoint1 = &H180
        KO_OFF_SPoint2 = &H184
        KO_OFF_SPoint3 = &H188
        KO_OFF_SPoint4 = &H18C
        ' = = = = Fonksiyon Offsetleri  = = = =
        KO_OFF_ITEMB = &H1B8
        KO_OFF_ITEMS = &H20C
        KO_OFF_BANKB = &H208
        KO_OFF_BANKS = &H128
        KO_OFF_BANKCONT = &HFC
        KO_OFF_SKILLBASE = &H1D0
        KO_OFF_SKILLID = &H12C
        ' = = = = Pet Offsetleri  = = = =
        KO_OFF_PET_ID = &HFD8
        KO_OFF_PET_BASE = &H3A4
        KO_OFF_PET_HP = &H19E
        KO_OFF_PET_MAXHP = KO_OFF_PET_HP - 2
        KO_OFF_PET_MP = &H1A2
        KO_OFF_PET_MAXMP = KO_OFF_PET_MP - 2
        KO_OFF_PET_LEVEL = &H199
        KO_OFF_PET_HUNGRY = &H155
        ' = = = = İtem Silme  = = = =
        KO_ITEMDESCALL = &H5D2ED0
        KO_ITEMDES = &HDEEF40
        KO_ITEMDES2 = &HDEEE10
        KO_FAKE_ITEM = &H568250
        KO_SH_HOOK = &H4E047B
        KO_SH_VALUE = &HB492B0
        KO_SPD_HOOK = &H4E047B + &H9D
        KO_SUB_ADDR0 = &H886D30
        KO_SUB_ADDR1 = &H573AA0
        KO_PTR_NRML = &HDEBA70
        KO_SMMB = &HE045C8
        KO_SMMB_FNC = &H4EF100
        'Hook Define
        WIZ_CHAT = &H10
        WIZ_GETLOOT = &H23
        WIZ_LOOTOPEN = &H24

        'Game Define
        KO_OFF_ITEMROW = 0
        KO_OFF_INVROW = 15
        KO_OFF_INVCOUNT = 42
        KO_OFF_SWIFT = &H7C6
        KO_OFF_MCORX = &H7C
        KO_OFF_MCORY = &H84
        KO_OFF_MCORZ = &H80
    End Sub
    Public Sub STEAMActiveOsmanli()
        '= = = =  Pointer = = = = 
        KO_PTR_CHR = &HE12280
        KO_PTR_DLG = &HDF9704
        KO_PTR_PKT = &HDF96D0
        KO_SND_FNC = &H4992D0
        KO_FMBS = &H4F7DF0
        KO_FPBS = &H4F8D60
        KO_FNC_ISEN = &H54A860
        KO_CHAR_SERV = &HC487E4
        KO_OTO_LOGIN_PTR = &HDF96E0
        KO_OTO_LOGIN_01 = &H4E3000
        KO_OTO_LOGIN_02 = &H4DC580
        KO_OTO_LOGIN_03 = &H4DC040
        KO_OTO_LOGIN_04 = &H4E09B0
        KO_OTO_BTN_PTR = &HDF9700
        KO_BTN_LEFT = &H4CBF60
        KO_BTN_RIGHT = &H4CC200
        KO_BTN_LOGIN = &H4C8790
        KO_FLDB = &HE1227C
        KO_FNCZ = &H5277E0
        KO_FNCB = &H527950
        KO_ITOB = &HE120B4
        KO_ITEB = &HE120BC

        '= = = =  Offsetler = = =
        KO_OFF_CLASS = &H6B0
        KO_OFF_NT = &H6A8
        KO_OFF_MOVE = &HF90
        KO_OFF_MOVEType = &H3F0
        KO_OFF_GoX = &HF9C
        KO_OFF_GoZ = &HFA0
        KO_OFF_GoY = &HFA4
        KO_OFF_X = &HD8
        KO_OFF_Z = &HDC
        KO_OFF_Y = &HE0
        KO_OFF_ID = &H680
        KO_OFF_WH = &H6C0
        KO_OFF_MCOR = &H40C

        KO_OFF_PtBase = &H1E8
        KO_OFF_PtCount = &H300
        KO_OFF_Pt = &H2FC

        KO_OFF_MAXEXP = &HB70
        KO_OFF_EXP = &HB78
        KO_OFF_MOB = &H644
        KO_OFF_ZONE = &HC00
        KO_OFF_NAMELEN = &H698
        KO_OFF_NAME = &H688
        KO_OFF_GOLD = &HB6C
        KO_OFF_MAXMP = &HB5C
        KO_OFF_MP = &HB60
        KO_OFF_MAXHP = &H6B8
        KO_OFF_HP = &H6BC
        KO_OFF_LEVEL = &H6B4
        KO_OFF_POINTStat = &H6B0
        KO_OFF_StatSTR = &HB94
        KO_OFF_StatHP = &HB9C
        KO_OFF_StatDEX = &HBA4
        KO_OFF_StatINT = &HBAC
        KO_OFF_StatMP = &HBB6
        KO_OFF_SBARBase = &H1EC
        KO_OFF_BSkPoint = &H16C
        KO_OFF_SPoint1 = &H180
        KO_OFF_SPoint2 = &H184
        KO_OFF_SPoint3 = &H188
        KO_OFF_SPoint4 = &H18C
        '= = = =  Fonksiyon Offsetleri = = = = 
        KO_OFF_ITEMB = &H1B8
        KO_OFF_ITEMS = &H20C
        KO_OFF_BANKB = &H208
        KO_OFF_BANKS = &H128
        KO_OFF_BANKCONT = &HFC
        KO_OFF_SKILLBASE = &H1D0
        KO_OFF_SKILLID = &H12C
        ' = = = = Pet Offsetleri  = = = =
        KO_OFF_PET_ID = &HFD8
        KO_OFF_PET_BASE = &H3A4
        KO_OFF_PET_MAXHP = &H19C
        KO_OFF_PET_HP = KO_OFF_PET_MAXHP - 2
        KO_OFF_PET_MAXMP = KO_OFF_PET_HP - 2
        KO_OFF_PET_MP = KO_OFF_PET_MAXMP - 2
        KO_OFF_PET_LEVEL = &H199
        KO_OFF_PET_HUNGRY = &H155

        '= = = =  İtem Silme = = = = 
        KO_ITEMDESCALL=&H5DEB00
        KO_ITEMDES = &HDFCBB8
        KO_ITEMDES2 = &HDFCA88
        KO_FAKE_ITEM = &H5733C0
        KO_SH_HOOK = &H4EC2DB
        KO_SH_VALUE = &HB512A0
        KO_SPD_HOOK = &H4EC2DB + &H9D
        KO_SUB_ADDR0 = &H889D30
        KO_SUB_ADDR1 = &H57EC10
        KO_PTR_NRML = &HDF96DC
        KO_SMMB = &HE12178
        KO_SMMB_FNC = &H4FAFC0

        'Hook Define
        WIZ_CHAT = &H10
        WIZ_GETLOOT = &H23
        WIZ_LOOTOPEN = &H24

        'Game Define
        KO_OFF_ITEMROW = 1
        KO_OFF_INVROW = 15
        KO_OFF_INVCOUNT = 42
        KO_OFF_SWIFT = &H7C6
        KO_OFF_MCORX = &H7C
        KO_OFF_MCORY = &H84
        KO_OFF_MCORZ = &H80
    End Sub
#End Region
#Region "Diğer Tanımlamalar"
    Public Cli As KOProject
    Public AutoLgn As KOProject
    Public BotStatus As Boolean
    Public AttackStatus As Boolean
    Public ServerStatus As Boolean = False
    Public uLoginEndDay As Date
    Public bIsActive As Boolean = False
    Public bMsgIsActive As Boolean = False
    Public userServerId As Integer = 0
    Public Const PROCESS_ALL_ACCESS As Long = &H1F0FFF
    Public Const INFINITE As Long = &HFFFF
    Public Const TH32CS_SNAPPROCESS As Long = &H2
    Public Const TH32CS_SNAPTHREAD As Long = &H4
    Public Const STANDARD_RIGHTS_REQUIRED As Long = &HF0000
    Public Const THREAD_SUSPEND_RESUME As Long = &H2
    Public Const MAX_PATH As Long = 260
    Public Const hNull As Long = 0
    Public Const PAGE_READWRITE As Long = &H4&
    Public Const MEM_COMMIT As Long = &H1000
    Public Const MEM_RELEASE As Long = &H8000&
    <StructLayout(LayoutKind.Sequential)>
    Public Structure STARTUPINFO
        Public cb As UInteger
        Public lpReserved As String
        Public lpDesktop As String
        Public lpTitle As String
        Public dwX As UInteger
        Public dwY As UInteger
        Public dwXSize As UInteger
        Public dwYSize As UInteger
        Public dwXCountChars As UInteger
        Public dwYCountChars As UInteger
        Public dwFillAttribute As UInteger
        Public dwFlags As UInteger
        Public wShowWindow As Short
        Public cbReserved2 As Short
        Public lpReserved2 As IntPtr
        Public hStdInput As IntPtr
        Public hStdOutput As IntPtr
        Public hStdError As IntPtr
    End Structure
    Public Enum ThreadAccess As Integer
        TERMINATE = (&H1)
        SUSPEND_RESUME = (&H2)
        GET_CONTEXT = (&H8)
        SET_CONTEXT = (&H10)
        SET_INFORMATION = (&H20)
        QUERY_INFORMATION = (&H40)
        SET_THREAD_TOKEN = (&H80)
        IMPERSONATE = (&H100)
        DIRECT_IMPERSONATION = (&H200)
    End Enum
    Public Structure PROCESS_INFORMATION
        Public hProcess As IntPtr
        Public hThread As IntPtr
        Public dwProcessID As UInteger
        Public dwThreadID As UInteger
    End Structure
    Public pInfo As PROCESS_INFORMATION = New PROCESS_INFORMATION(), sInfo As STARTUPINFO = New STARTUPINFO()
#End Region
#Region "Apiler"
    <DllImport("kernel32.dll")> _
    Public Function CreateProcess(ByVal lpApplicationName As String, ByVal lpCommandLine As String, ByVal lpProcessAttributes As IntPtr, ByVal lpThreadAttributes As IntPtr, ByVal bInheritHandles As Boolean, ByVal dwCreationFlags As UInteger, ByVal lpEnvironment As IntPtr, ByVal lpCurrentDirectory As String, ByRef lpStartupInfo As STARTUPINFO, ByRef lpProcessInformation As PROCESS_INFORMATION) As Boolean
    End Function
    <DllImport("kernel32.dll")> _
    Public Function GetTickCount() As UInteger
    End Function
    <DllImport("user32.dll")> _
    Public Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, ByRef lpdwProcessId As Int32) As Int32
    End Function
    <DllImport("kernel32.dll")> _
    Public Function OpenProcess(ByVal dwDesiredAccess As Int32, ByVal bInheritHandle As Boolean, ByVal dwProcessId As Int32) As IntPtr
    End Function
    <DllImport("psapi.dll")> _
    Public Function GetModuleFileNameExA(ByVal hProcess As IntPtr, ByVal hModule As IntPtr, ByRef moduleName As [Byte], ByVal nSize As UInt32) As IntPtr
    End Function
    <DllImport("User32")> _
    Public Function ShowWindow(ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Integer
    End Function
    <DllImport("kernel32.dll")> _
    Public Function OpenThread(ByVal dwDesiredAccess As ThreadAccess, ByVal bInheritHandle As Boolean, ByVal dwThreadId As UInteger) As IntPtr
    End Function
    <DllImport("kernel32.dll")> _
    Public Function SuspendThread(ByVal hThread As IntPtr) As UInteger
    End Function
    <DllImport("kernel32.dll")> _
    Public Function ResumeThread(ByVal hThread As IntPtr) As Integer
    End Function
    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Function WriteProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer As Byte(), ByVal nSize As System.UInt32, <Out()> ByRef lpNumberOfBytesWritten As IntPtr) As Boolean
    End Function
    Public Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Integer, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Public Declare Function CreateRemoteThread Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal lpThreadAttributes As Int32, ByVal dwStackSize As Int32, ByVal lpStartAddress As IntPtr, ByVal lpparameter As Int32, ByVal deCreationFlags As Int32, ByRef lpThreadID As IntPtr) As IntPtr
    Public Declare Function WaitForSingleObject Lib "kernel32" (ByVal hProcess As IntPtr, ByVal dwMilliseconds As UInteger) As IntPtr
    Public Declare Function CloseHandle Lib "kernel32.dll" (ByVal hObject As IntPtr) As IntPtr
    Public Declare Function VirtualFreeEx Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As Int32, ByVal dwFreeType As Int32) As IntPtr
    Public Declare Function VirtualAllocEx Lib "kernel32.dll" (ByVal hpProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As Int32, ByVal flAllocationType As Int32, ByVal flProtect As Int32) As IntPtr
    Public Declare Function ReadFloatMemory Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByRef buffer As Single, ByVal size As Int32, ByRef lpNumberOfBytesRead As Int32) As Boolean
    Public Declare Function WriteFloatMemory Lib "kernel32" Alias "WriteProcessMemory" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Single, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Public Declare Function ReadProcessMemory Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Integer, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Public Declare Function ReadProcessMem Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As Long, ByVal lpBaseAddress As IntPtr, ByRef lpBuffer As Long, ByVal nSize As Long, ByVal lpNumberOfBytesWritten As Long) As Long

    Public Declare Ansi Function WritePrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Public Declare Ansi Function GetPrivateProfileString Lib "kernel32.dll" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Integer
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    Public Declare Function TerminateProcess Lib "kernel32" (ByVal hProcess As IntPtr, ByVal uExitCode As IntPtr) As IntPtr
    Public Declare Function GetWindow Lib "user32.dll" (ByVal hWnd As Long, ByVal wCmd As Long) As Long
    Public Declare Function GetWindowTextLength Lib "user32.dll" Alias "GetWindowTextLengthA" (ByVal hWnd As Long) As Long
    Public Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hWnd As Long, ByVal lpString As String, ByVal cch As Long) As Long
    Public Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
    Public Declare Function GetModuleHandle Lib "kernel32" Alias "GetModuleHandleA" (ByVal lpModuleName As String) As Integer
    Public Declare Function GetLastError Lib "coredll.dll" () As Int32
    Public Declare Function SendMessageSTRING Lib "user32" Alias "SendMessageA" (ByVal Hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As String) As Long
    Public Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As IntPtr
    <DllImport("user32.dll", SetLastError:=True)> _
    Public Function SetActiveWindow(ByVal hWnd As IntPtr) As IntPtr
    End Function
    <DllImport("user32.dll")> _
    Public Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Public Declare Function SetFocus Lib "user32.dll" (ByVal hwnd As Int32) As Int32
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Function PostMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
    End Function
    <DllImport("kernel32")> _
    Public Function GetMailslotInfo(ByVal hMailSlot As IntPtr, ByRef lpMaxMessageSize As Int32, ByRef lpNextSize As Int32, ByRef lpMessageCount As Int32, ByRef lpReadTimeout As Int32) As Int32
    End Function
    <DllImport("kernel32.dll", EntryPoint:="CreateMailslotA")> _
    Public Function CreateMailslot(ByVal lpName As String, ByVal nMaxMessageSize As Int32, ByVal lReadTimeout As Int32, ByVal lpSecurityAttributes As Int32) As IntPtr
    End Function
    <DllImport("kernel32")> _
    Public Function ReadFile(ByVal hFile As IntPtr, ByRef lpBuffer As [Byte], ByVal nNumberOfBytesToRead As Int32, ByRef lpNumberOfBytesRead As Int32, ByVal lpOverlapped As Int32) As IntPtr
    End Function
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Ansi)> _
    Public Function LoadLibrary(ByVal lpFileName As String) As IntPtr
    End Function
    <DllImport("kernel32.dll")> _
    Public Function Beep(ByVal freq As Integer, ByVal duration As Integer) As Boolean
    End Function
    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Sub Sleep(MilliSeconds As UInteger)
    End Sub
    Public Declare Auto Function SetCursorPos Lib "User32.dll" (ByVal X As Integer, ByVal Y As Integer) As Long
    Public Declare Auto Function GetCursorPos Lib "User32.dll" (ByRef lpPoint As Point) As Long
    <DllImport("user32.dll")> _
    Public Sub mouse_event(ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)
    End Sub
    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Sub ZeroMemory(ByVal addr As IntPtr, ByVal size As IntPtr)
    End Sub
    Public Declare Function GetForegroundWindow Lib "user32" Alias "GetForegroundWindow" () As IntPtr
    Public Declare Auto Function GetWindowText Lib "user32" (ByVal hWnd As System.IntPtr, ByVal lpString As System.Text.StringBuilder, ByVal cch As Integer) As Integer

    Public Const MOUSEEVENTF_LEFTDOWN = &H2 ' left button down
    Public Const MOUSEEVENTF_LEFTUP = &H4 ' left button up
    Public Const MOUSEEVENTF_MIDDLEDOWN = &H20 ' middle button down
    Public Const MOUSEEVENTF_MIDDLEUP = &H40 ' middle button up
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8 ' right button down
    Public Const MOUSEEVENTF_RIGHTUP = &H10 ' right button up
#End Region
#Region "Translate TR / EN"
    Public Const strProjectInfo As String = "[CS - Project Bilgilendirme]"
    Public Const strHideBot As String = "Bot buraya gizlendi."
    Public Const strSaveConfig As String = "Settings klasörü içerisinde eksik dosya var."
    Public Const strAgainDownload As String = "Lütfen tekrar indiriniz."
    Public Const strRenameGame As String = "Açmak istediğiniz oyunu isimlendirebilirsiniz."
    Public Const strSetNickGame As String = "Lütfen bir takma ad giriniz."
    Public Const strExNick As String = "Örneğin: Enes"
    Public Const strGameNick As String = "Oyun Nick"
    Public Const strEmptyEntry As String = "Eksik veri girişi yapıldı."
    Public Const strPleaseSetNick As String = "Lütfen oyun yolu ve takma adı giriniz."
    Public Const strPleaseEntryIDPass As String = "Lütfen oyun ID ve Pass giriniz"
    Public Const strNoChoseGame As String = "Yüklenecek oyun seçilmedi"
    Public Const strPleaseChoseGame As String = "Lütfen yüklemek istediğiniz oyunu seçiniz"
    Public Const strErrorHandle As String = "Handle alınamadı"
    Public Const strErrorBypassFunc As String = "Multi işlemi yada bypass gerçekleşmedi."
    Public Const strNotFoundGame As String = "Açılacak bir oyun bulunamadı."
    Public Const strPleaseChoseOpenGame As String = "Lütfen açmak istediğiniz oyunu seçiniz"
    Public Const strClearRprList As String = "Liste temizlenecek kabul ediyor musunuz?"
    Public Const strOnlyRepair As String = "Repair ile aynı anda upgrade bot özelliğini kullanamazsınız."
    Public Const strCancelUpgrade As String = "Upgrade iptal ediliyor."
    Public Const strPleaseAddCoorRpr As String = "Lütfen kordinatlarınızı ekleyiniz."
    Public Const strRepairOk As String = "Repair işleminin tamamlanamsı için listeye [O - Repair OK] eklemeniz gereklidir."
    Public Const strHowToRepairOk As String = "İşlem ekle butonu altından ekleyebilirsiniz."
    Public Const strBlueBoxLevel As String = "Görevi yapabilmek için 70 level üzeri olmanız gereklidir."
    Public Const strBlueBoxZone As String = "Mavi kutu görevi için geçmeniz gereken harita;"
    Public Const strFoundGm As String = "GM Tespit edildi koruma sistemi durduruldu."
    Public Const strGmCheckerActive As String = "Genel sekmesinden tekrar aktif edebilirsiniz."
    Public Const strLuckyJoke As String = "Bak bu yolda çok kaybeden oldu. :)"
    Public Const strLuckySure As String = "Hazır başlamışken kısa yoldan dön bence :)"
    Public Const strUniqueItem As String = "Değerli itemler eklenmemiştir."
    Public Const strHowToAddUnique As String = "Lütfen diğer bölümünden değerli itemleri ekleyiniz."
    Public Const strIdeaAttackSpeed As String = "İdeal Hız Değerleri;"
    Public Const strUpgradeCoin As String = "Upgrade işlemini tamamlamak için üzerinizde gerekli miktar bulunmamaktadır."
    Public Const strGetUpgradeCoin As String = "Lütfen gerekli miktarı temin ediniz."
    Public Const strOnlyUpgrade As String = "Repair ile aynı anda upgrade bot özelliğini kullanamazsınız."
    Public Const strCancelRepair As String = "Lütfen repair işlemini iptal ediniz."
    Public Const strInactiveAutoLogin As String = "Oto login aktif edilmemiş görünüyor."
    Public Const strHowToActiveAutoLogin As String = "Bota tekrar giriş yaptıktan sonra autologin bilgilerini kayıt edebilirsiniz."
    Public Const strPleaseUseCoor As String = "Lütfen kordinatları giriniz."
    Public Const strIfCoor As String = "Aksi takdirde oyundan düşeceksiniz."
    Public Const strWalkArea As String = "Moba koş eklentisi ile kordiat gez aynı alda çalışamaz."
    Public Const strCloseRunMob As String = "Moba koş eklentisi kaldırılıyor."
    Public Const strWelcome As String = "Hoş geldiniz."
    Public Const strServer As String = "Server kontrolleri için biraz bekleteceğim."
    Public Const strServerError As String = "Servera bağlanamdı."
    Public Const strTryAgainLater As String = "Lütfen daha sonra tekrar deneyiniz."
    Public Const strErrorUserInfo As String = "Kullanıcı adı ve şifrenizi giriniz."
    Public Const strEmptyError As String = "Lütfen alanları boş bırakmayınız."
    Public Const strWrongInfo As String = "Kullanıcı adı yada şifre yanlış!"
    Public Const strNoUser As String = "Üyeliğiniz yoksa kayıt olabilirsiniz."
    Public Const strFinishDay As String = "Hesabınızda gün kalmamıştır."
    Public Const strPleaseUpKey As String = "Lütfen key yükleyiniz."
    Public Const strServerNoRead As String = "Sunucu verileri okunamadı. Şifreniz yanlış olabilir."
    Public Const strPleaseCheckInfo As String = "Lütfen bilgilerinizi kontrol edin."
    Public Const strBotIsActive As String = "Hesabınız başka bir bilgisayarda açık bulunmamaktadır, eğer açık kalmadığını düşünüyorsanız."
    Public Const strCloseBotAgain As String = "Lütfen bota tekrar giriş yapıp sağ üstteki X butonu ile çıkış yapınız."
    Public Const strPleaseContact As String = "Lütfen yapımcı ile görüşünüz."
    Public Const strNotFoundSerial As String = "Hesabınıza bağlı bir bilgisayar görünmüyor."
    Public Const strPleaseEmptyInfo As String = "Lütfen alanları boş bırakmayınız."
    Public Const strPleaseEntryInfo As String = "Kullanıcı adı ve key kodunu giriniz."
    Public Const strPleaseLoginForKey As String = "Key yükleme için lütfen giriş yapınız."
    Public Const strEndDay As String = "Hesabınızda süre bitmiştir."
    Public Const strCloseBot As String = "Bot kapatılıyor."
    Public Const strBotIsnActive As String = "Bot şu anda aktif değildir."
    'Public Const strProdDatasource As String = "csqTwf1yWkH/jNU5f3f2ffZgvQ9V4Mkl8/KKRsvK+Iw_OKKIBBY"
    'Public Const strLeon As String = "y/oaQcV22WQezN+wkjt9h4IM8cRQx0GIplOowG14rFzmVbFfps62IbeyGoFEGiweATOT4bKN/BgZop4vkyJIePZ3wGmYZDMpv+xW9hU7nbVFGPZewLLNWB42yVmTwqFE02EgyTLa4z9KTa7QdJA/p+3r1MQMntkGO2sqhYzdUswCAGQmPOiz51Os5aCORFHi3Qsm/xxL2WlbHs13QHw7PJ0aWsMSKxDYJWrKjpJDu95OmGMQf0EcYEn3NWE/lXufcXRDOzxSy7xFu28WaLWcyyIT09jzDO/fAfLEXEgSyBk52ut9xn2vkXZEt+hdMZlwRXaVA0FH+rTa8D/rkbPTSpvvj/X2RDX8Hk3r/omsXSSRPPqxtQ0M9PiKzsmXidxe"
    'Public Const strProdDs As String = "EIxeS4S/RBPJ2WWprROJREwyrY9X0UUlpCF71qyQhgozwQZh0DpMuipUTT8ACMrjo0WT6Wq8NgE="
    'Public Const strProdCat As String = "EIxeS4S/RBPJ2WWprROJRJk9MuLoAtLM"
    'Public Const strProdId As String = "czCxXZQcR5CwYYZ6IP3PEbIugSmYQH9UC+OYmFWiNeUj65s01V0Sog=="
    'Public Const strProdPs As String = "U8AwZBE/8tYfT9vAXr4BkIlYxfQGWqKS"
    'Public Const strProdPro As String = "vEZ9dxRtNk92JSrHUAgtcIgm6qRXyiIhwt5fAs/j7GJ9nnTCxO8BCN0j0BleLA+W"
    'Public Const strProdMData As String = "dmGoQ8g5PPpXHtkid4MykXLIsZZUw8DxqcEb1FotVLNUwnr10k0oc2wQjtRql3IYKs3OOnfTzH7IDz67P5jsJPM5rln06O7s1vH5A9/gs71NjqgzWGNGfb7xDcDTPfka+RpOIl/duA1P19Lwn+RXwhNT0jcKB57XBtfzbfkkT+Q="
    'Public Const strProdFk As String = "+hdMZlwRXaVA0FH+rTa8D/rkbPTSpvvj/X2RDX8Hk3r/omsXSSRPPqxtQ0M9PiKzsmXidxe"

    Public Const strProdDatasource As String = "csqTwf1yWkH/jNU5f3f2ffZgvQ9V4Mkl8/KKRsvK+Iw_OKKIBBY"
    Public Const strLeon As String = "y/oaQcV22WQezN+wkjt9h4IM8cRQx0GIplOowG14rFzmVbFfps62IbeyGoFEGiweATOT4bKN/BgZop4vkyJIePZ3wGmYZDMpv+xW9hU7nbVFGPZewLLNWB42yVmTwqFE02EgyTLa4z9KTa7QdJA/p+3r1MQMntkGO2sqhYzdUswCAGQmPOiz51Os5aCORFHi3Qsm/xxL2WlbHs13QHw7PJ0aWsMSKxDYJWrKjpJDu95OmGMQf0EcYEn3NWE/lXufcXRDOzxSy7xFu28WaLWcyyIT09jzDO/fAfLEXEgSyBk52ut9xn2vkXZEt+hdMZlwRXaVA0FH+rTa8D/rkbPTSpvvj/X2RDX8Hk3r/omsXSSRPPqxtQ0M9PiKzsmXidxe"
    Public Const strProdDs As String = "qTyWkH/jNU5ZgvQ9V4Mkl8/KKRsvK+Iw"
    Public Const strProdCat As String = "GZ/SixxbpCoKmpYjojyIne2LdLNcxxwg"
    Public Const strProdId As String = "zUCq421hufaQuS8+jCrWGbVwT5Z2qSSDFHoGG/H8Rik="
    Public Const strProdPs As String = "ONdVD8a4S2IEbD79I/ig1cx5a3iu76Pk"
    Public Const strProdPro As String = "vEZ9dxRtNk92JSrHUAgtcIgm6qRXyiIhwt5fAs/j7GJ9nnTCxO8BCN0j0BleLA+W"
    Public Const strProdMData As String = "dmGoQ8g5PPpXHtkid4MykXLIsZZUw8DxqcEb1FotVLNUwnr10k0oc2wQjtRql3IYKs3OOnfTzH7IDz67P5jsJPM5rln06O7s1vH5A9/gs71NjqgzWGNGfb7xDcDTPfka+RpOIl/duA1P19Lwn+RXwhNT0jcKB57XBtfzbfkkT+Q="
    Public Const strProdFk As String = "+hdMZlwRXaVA0FH+rTa8D/rkbPTSpvvj/X2RDX8Hk3r/omsXSSRPPqxtQ0M9PiKzsmXidxe"
#End Region
End Module
