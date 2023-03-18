Public Class frm_3Recv

    Private Sub frm_3Recv_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        chk_Active.Checked = False
        Hide()
    End Sub
    Private Sub frm_3Recv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (ServerStatus = False) Then End
        TopMost = True
    End Sub

    Private Sub chk_AllChecked_CheckedChanged(sender As Object, e As EventArgs) Handles chk_AllChecked.CheckedChanged
        For i As Integer = 0 To clst_Recv.Items.Count - 1
            clst_Recv.SetItemChecked(i, chk_AllChecked.Checked)
        Next
    End Sub

    Private Sub btn_ClearList_Click(sender As Object, e As EventArgs) Handles btn_ClearList.Click
        lst_Recv.Items.Clear()
    End Sub

    Private Sub lst_Recv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lst_Recv.SelectedIndexChanged
        If (lst_Recv.SelectedIndex > -1) Then txt_RecvPacket.Text = lst_Recv.Text
    End Sub

    Private Sub tmr_Info_Tick(sender As Object, e As EventArgs) Handles tmr_Info.Tick
        If (chk_Active.Checked) Then
            lbl_CharLID.Text = Cli.getCharLongID()
            lbl_CharID.Text = Cli.getCharID()
            lbl_CharX.Text = Cli.getCharX()
            lbl_CharY.Text = Cli.getCharY()
            lbl_CharZ.Text = Cli.getCharZ()
            lbl_MobLID.Text = Cli.getMobID()
            lbl_MobID.Text = Cli.getMobIDHex()
            lbl_MobX.Text = Cli.getMobX()
            lbl_MobY.Text = Cli.getMobY()
            lbl_MobZ.Text = Cli.getMobZ()
            If (lst_Recv.Items.Count > 0) Then lst_Recv.SelectedIndex = lst_Recv.Items.Count - 1
        End If
    End Sub

    Private Sub txt_HexValue_TextChanged(sender As Object, e As EventArgs) Handles txt_HexValue.TextChanged
        Try
            txt_DecValue.Text = Cli.FormatDec(txt_HexValue.Text, txt_HexValue.Text.Length)
            lbl_ConvertStatus.Text = "+"
        Catch ex As Exception
            lbl_ConvertStatus.Text = "-"
        End Try
    End Sub

    Private Sub gb_Recv_Enter(sender As Object, e As EventArgs) Handles gb_Recv.Enter

    End Sub
End Class