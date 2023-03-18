<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_3Recv
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_3Recv))
        Me.gb_Recv = New System.Windows.Forms.GroupBox()
        Me.lbl_ConvertStatus = New System.Windows.Forms.Label()
        Me.txt_DecValue = New System.Windows.Forms.TextBox()
        Me.txt_HexValue = New System.Windows.Forms.TextBox()
        Me.txt_RecvPacket = New System.Windows.Forms.TextBox()
        Me.lbl_MobLID = New System.Windows.Forms.Label()
        Me.lbl_CharLID = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbl_MobZ = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbl_MobY = New System.Windows.Forms.Label()
        Me.lbl_CharZ = New System.Windows.Forms.Label()
        Me.lbl_MobX = New System.Windows.Forms.Label()
        Me.lbl_CharY = New System.Windows.Forms.Label()
        Me.lbl_MobID = New System.Windows.Forms.Label()
        Me.lbl_CharX = New System.Windows.Forms.Label()
        Me.lbl_CharID = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_ClearList = New System.Windows.Forms.Button()
        Me.chk_AllChecked = New System.Windows.Forms.CheckBox()
        Me.chk_Active = New System.Windows.Forms.CheckBox()
        Me.lst_Recv = New System.Windows.Forms.ListBox()
        Me.clst_Recv = New System.Windows.Forms.CheckedListBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.tmr_Info = New System.Windows.Forms.Timer(Me.components)
        Me.gb_Recv.SuspendLayout()
        Me.SuspendLayout()
        '
        'gb_Recv
        '
        Me.gb_Recv.Controls.Add(Me.lbl_ConvertStatus)
        Me.gb_Recv.Controls.Add(Me.txt_DecValue)
        Me.gb_Recv.Controls.Add(Me.txt_HexValue)
        Me.gb_Recv.Controls.Add(Me.txt_RecvPacket)
        Me.gb_Recv.Controls.Add(Me.lbl_MobLID)
        Me.gb_Recv.Controls.Add(Me.lbl_CharLID)
        Me.gb_Recv.Controls.Add(Me.Label11)
        Me.gb_Recv.Controls.Add(Me.Label9)
        Me.gb_Recv.Controls.Add(Me.Label8)
        Me.gb_Recv.Controls.Add(Me.Label7)
        Me.gb_Recv.Controls.Add(Me.Label6)
        Me.gb_Recv.Controls.Add(Me.lbl_MobZ)
        Me.gb_Recv.Controls.Add(Me.Label5)
        Me.gb_Recv.Controls.Add(Me.lbl_MobY)
        Me.gb_Recv.Controls.Add(Me.lbl_CharZ)
        Me.gb_Recv.Controls.Add(Me.lbl_MobX)
        Me.gb_Recv.Controls.Add(Me.lbl_CharY)
        Me.gb_Recv.Controls.Add(Me.lbl_MobID)
        Me.gb_Recv.Controls.Add(Me.lbl_CharX)
        Me.gb_Recv.Controls.Add(Me.lbl_CharID)
        Me.gb_Recv.Controls.Add(Me.Label4)
        Me.gb_Recv.Controls.Add(Me.Label3)
        Me.gb_Recv.Controls.Add(Me.Label2)
        Me.gb_Recv.Controls.Add(Me.Label1)
        Me.gb_Recv.Controls.Add(Me.btn_ClearList)
        Me.gb_Recv.Controls.Add(Me.chk_AllChecked)
        Me.gb_Recv.Controls.Add(Me.chk_Active)
        Me.gb_Recv.Controls.Add(Me.lst_Recv)
        Me.gb_Recv.Controls.Add(Me.clst_Recv)
        Me.gb_Recv.Controls.Add(Me.Label10)
        Me.gb_Recv.Location = New System.Drawing.Point(12, 12)
        Me.gb_Recv.Name = "gb_Recv"
        Me.gb_Recv.Size = New System.Drawing.Size(913, 485)
        Me.gb_Recv.TabIndex = 0
        Me.gb_Recv.TabStop = False
        Me.gb_Recv.Text = "Gelen Paketler"
        '
        'lbl_ConvertStatus
        '
        Me.lbl_ConvertStatus.AutoSize = True
        Me.lbl_ConvertStatus.Location = New System.Drawing.Point(756, 392)
        Me.lbl_ConvertStatus.Name = "lbl_ConvertStatus"
        Me.lbl_ConvertStatus.Size = New System.Drawing.Size(13, 13)
        Me.lbl_ConvertStatus.TabIndex = 23
        Me.lbl_ConvertStatus.Text = "+"
        '
        'txt_DecValue
        '
        Me.txt_DecValue.Location = New System.Drawing.Point(659, 389)
        Me.txt_DecValue.Name = "txt_DecValue"
        Me.txt_DecValue.Size = New System.Drawing.Size(91, 20)
        Me.txt_DecValue.TabIndex = 22
        '
        'txt_HexValue
        '
        Me.txt_HexValue.Location = New System.Drawing.Point(500, 389)
        Me.txt_HexValue.Name = "txt_HexValue"
        Me.txt_HexValue.Size = New System.Drawing.Size(91, 20)
        Me.txt_HexValue.TabIndex = 20
        '
        'txt_RecvPacket
        '
        Me.txt_RecvPacket.Location = New System.Drawing.Point(187, 363)
        Me.txt_RecvPacket.Name = "txt_RecvPacket"
        Me.txt_RecvPacket.Size = New System.Drawing.Size(720, 20)
        Me.txt_RecvPacket.TabIndex = 2
        '
        'lbl_MobLID
        '
        Me.lbl_MobLID.AutoSize = True
        Me.lbl_MobLID.Location = New System.Drawing.Point(362, 392)
        Me.lbl_MobLID.Name = "lbl_MobLID"
        Me.lbl_MobLID.Size = New System.Drawing.Size(37, 13)
        Me.lbl_MobLID.TabIndex = 19
        Me.lbl_MobLID.Text = "00000"
        '
        'lbl_CharLID
        '
        Me.lbl_CharLID.AutoSize = True
        Me.lbl_CharLID.Location = New System.Drawing.Point(242, 392)
        Me.lbl_CharLID.Name = "lbl_CharLID"
        Me.lbl_CharLID.Size = New System.Drawing.Size(37, 13)
        Me.lbl_CharLID.TabIndex = 19
        Me.lbl_CharLID.Text = "00000"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(187, 392)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Char LID :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(302, 392)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(54, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Mob LID :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(312, 446)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Mob Y :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(312, 464)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Mob Z :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(312, 428)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Mob X :"
        '
        'lbl_MobZ
        '
        Me.lbl_MobZ.AutoSize = True
        Me.lbl_MobZ.Location = New System.Drawing.Point(362, 464)
        Me.lbl_MobZ.Name = "lbl_MobZ"
        Me.lbl_MobZ.Size = New System.Drawing.Size(37, 13)
        Me.lbl_MobZ.TabIndex = 13
        Me.lbl_MobZ.Text = "00000"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(308, 410)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Mob ID :"
        '
        'lbl_MobY
        '
        Me.lbl_MobY.AutoSize = True
        Me.lbl_MobY.Location = New System.Drawing.Point(362, 446)
        Me.lbl_MobY.Name = "lbl_MobY"
        Me.lbl_MobY.Size = New System.Drawing.Size(37, 13)
        Me.lbl_MobY.TabIndex = 12
        Me.lbl_MobY.Text = "00000"
        '
        'lbl_CharZ
        '
        Me.lbl_CharZ.AutoSize = True
        Me.lbl_CharZ.Location = New System.Drawing.Point(242, 464)
        Me.lbl_CharZ.Name = "lbl_CharZ"
        Me.lbl_CharZ.Size = New System.Drawing.Size(37, 13)
        Me.lbl_CharZ.TabIndex = 13
        Me.lbl_CharZ.Text = "00000"
        '
        'lbl_MobX
        '
        Me.lbl_MobX.AutoSize = True
        Me.lbl_MobX.Location = New System.Drawing.Point(362, 428)
        Me.lbl_MobX.Name = "lbl_MobX"
        Me.lbl_MobX.Size = New System.Drawing.Size(37, 13)
        Me.lbl_MobX.TabIndex = 11
        Me.lbl_MobX.Text = "00000"
        '
        'lbl_CharY
        '
        Me.lbl_CharY.AutoSize = True
        Me.lbl_CharY.Location = New System.Drawing.Point(242, 446)
        Me.lbl_CharY.Name = "lbl_CharY"
        Me.lbl_CharY.Size = New System.Drawing.Size(37, 13)
        Me.lbl_CharY.TabIndex = 12
        Me.lbl_CharY.Text = "00000"
        '
        'lbl_MobID
        '
        Me.lbl_MobID.AutoSize = True
        Me.lbl_MobID.Location = New System.Drawing.Point(362, 410)
        Me.lbl_MobID.Name = "lbl_MobID"
        Me.lbl_MobID.Size = New System.Drawing.Size(37, 13)
        Me.lbl_MobID.TabIndex = 10
        Me.lbl_MobID.Text = "00000"
        '
        'lbl_CharX
        '
        Me.lbl_CharX.AutoSize = True
        Me.lbl_CharX.Location = New System.Drawing.Point(242, 428)
        Me.lbl_CharX.Name = "lbl_CharX"
        Me.lbl_CharX.Size = New System.Drawing.Size(37, 13)
        Me.lbl_CharX.TabIndex = 11
        Me.lbl_CharX.Text = "00000"
        '
        'lbl_CharID
        '
        Me.lbl_CharID.AutoSize = True
        Me.lbl_CharID.Location = New System.Drawing.Point(242, 410)
        Me.lbl_CharID.Name = "lbl_CharID"
        Me.lbl_CharID.Size = New System.Drawing.Size(37, 13)
        Me.lbl_CharID.TabIndex = 10
        Me.lbl_CharID.Text = "00000"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(197, 464)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Char Z :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(197, 446)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Char Y :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(197, 428)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Char X :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(193, 410)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Char ID :"
        '
        'btn_ClearList
        '
        Me.btn_ClearList.Location = New System.Drawing.Point(6, 419)
        Me.btn_ClearList.Name = "btn_ClearList"
        Me.btn_ClearList.Size = New System.Drawing.Size(175, 32)
        Me.btn_ClearList.TabIndex = 5
        Me.btn_ClearList.Text = "Listeyi Temizle"
        Me.btn_ClearList.UseVisualStyleBackColor = True
        '
        'chk_AllChecked
        '
        Me.chk_AllChecked.AutoSize = True
        Me.chk_AllChecked.Location = New System.Drawing.Point(149, 457)
        Me.chk_AllChecked.Name = "chk_AllChecked"
        Me.chk_AllChecked.Size = New System.Drawing.Size(32, 17)
        Me.chk_AllChecked.TabIndex = 4
        Me.chk_AllChecked.Text = "+"
        Me.chk_AllChecked.UseVisualStyleBackColor = True
        '
        'chk_Active
        '
        Me.chk_Active.AutoSize = True
        Me.chk_Active.Location = New System.Drawing.Point(6, 459)
        Me.chk_Active.Name = "chk_Active"
        Me.chk_Active.Size = New System.Drawing.Size(47, 17)
        Me.chk_Active.TabIndex = 3
        Me.chk_Active.Text = "Aktif"
        Me.chk_Active.UseVisualStyleBackColor = True
        '
        'lst_Recv
        '
        Me.lst_Recv.FormattingEnabled = True
        Me.lst_Recv.Location = New System.Drawing.Point(187, 19)
        Me.lst_Recv.Name = "lst_Recv"
        Me.lst_Recv.ScrollAlwaysVisible = True
        Me.lst_Recv.Size = New System.Drawing.Size(720, 342)
        Me.lst_Recv.TabIndex = 1
        '
        'clst_Recv
        '
        Me.clst_Recv.FormattingEnabled = True
        Me.clst_Recv.Items.AddRange(New Object() {"LOGIN", "NEW_CHAR", "DEL_CHAR", "SEL_CHAR", "SEL_NATION", "MOVE", "USER_INOUT", "ATTACK", "ROTATE", "NPC_INOUT", "NPC_MOVE", "ALLCHAR_INFO_REQ", "GAMESTART", "MYINFO", "LOGOUT", "CHAT", "DEAD", "REGENE", "TIME", "WEATHER", "REGIONCHANGE", "REQ_USERIN", "HP_CHANGE", "MSP_CHANGE", "ITEM_LOG", "EXP_CHANGE", "LEVEL_CHANGE", "NPC_REGION", "REQ_NPCIN", "WARP", "ITEM_MOVE", "NPC_EVENT", "ITEM_TRADE", "TARGET_HP", "ITEM_DROP", "BUNDLE_OPEN_REQ", "TRADE_NPC", "STATE_CHANGE", "USERLOOK_CHANGE", "NOTICE", "MAGIC_PROCESS", "OBJECT_EVENT", "REPAIR_NPC", "ITEM_REPAIR", "KNIGHTS_PROCESS", "ITEM_REMOVE", "COMPRESS_PACKET", "FRIEND_REPORT", "WEIGHT_CHANGE", "SELECT_MSG", "Unknow", "WIZ_PARTY"})
        Me.clst_Recv.Location = New System.Drawing.Point(6, 19)
        Me.clst_Recv.Name = "clst_Recv"
        Me.clst_Recv.Size = New System.Drawing.Size(175, 394)
        Me.clst_Recv.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(462, 392)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(191, 13)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "Hex :                                   to decimal :"
        '
        'tmr_Info
        '
        Me.tmr_Info.Enabled = True
        '
        'frm_3Recv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(936, 508)
        Me.Controls.Add(Me.gb_Recv)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "frm_3Recv"
        Me.Text = "[ICEx Project] - Enes"
        Me.gb_Recv.ResumeLayout(False)
        Me.gb_Recv.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gb_Recv As System.Windows.Forms.GroupBox
    Friend WithEvents btn_ClearList As System.Windows.Forms.Button
    Friend WithEvents chk_AllChecked As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Active As System.Windows.Forms.CheckBox
    Friend WithEvents txt_RecvPacket As System.Windows.Forms.TextBox
    Friend WithEvents lst_Recv As System.Windows.Forms.ListBox
    Friend WithEvents clst_Recv As System.Windows.Forms.CheckedListBox
    Friend WithEvents lbl_MobLID As System.Windows.Forms.Label
    Friend WithEvents lbl_CharLID As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbl_MobZ As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbl_MobY As System.Windows.Forms.Label
    Friend WithEvents lbl_CharZ As System.Windows.Forms.Label
    Friend WithEvents lbl_MobX As System.Windows.Forms.Label
    Friend WithEvents lbl_CharY As System.Windows.Forms.Label
    Friend WithEvents lbl_MobID As System.Windows.Forms.Label
    Friend WithEvents lbl_CharX As System.Windows.Forms.Label
    Friend WithEvents lbl_CharID As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmr_Info As System.Windows.Forms.Timer
    Friend WithEvents txt_DecValue As System.Windows.Forms.TextBox
    Friend WithEvents txt_HexValue As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lbl_ConvertStatus As System.Windows.Forms.Label
End Class
