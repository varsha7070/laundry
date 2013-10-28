Public Class PAYMENTFRM

    Private Sub PAYMENTFRM_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
    End Sub

    Private Sub PAYMENTFRM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BTSAVE.Text = "ADD"
        BTUPDATE.Text = "MODIFY"

    End Sub

    Private Sub BTSEARCH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTSEARCH.Click
        PAYMENTSEARCH.Show()
    End Sub

    Private Sub BTEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTEXIT.Click
        Me.Close()
    End Sub

    Private Sub BTSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTSAVE.Click
        If BTSAVE.Text = "ADD" Then
            BTSAVE.Text = "SAVE"
            ENABLE()
            If con.State = 1 Then con.Close()
            Dim cmd As New OleDb.OleDbCommand("Select max(R_ID)from PAYMENT", con)
            Dim str As String
            con.Open()
            str = cmd.ExecuteScalar
            If str > 0 Then
                TXTID.Text = str + 1
            Else
                TXTID.Text = "1"
            End If
            con.Close()
        ElseIf BTSAVE.Text = "SAVE" Then
            BTSAVE.Text = "ADD"
            If TXTAMOUNT.Text <> "" And TXTBAL.Text <> "" And TXTPAID.Text <> "" Then
                CheckBox1.Checked = True
                If con.State = 1 Then con.Close()
                Dim cmd As New OleDb.OleDbCommand("Insert into PAYMENT values(" & (TXTID.Text) & ",'" & (LBDATE.Text) & "','" & (LBNAME.Text) & "'," & (LBINVID.Text) & ",'" & (TXTAMOUNT.Text) & "','" & (TXTPAID.Text) & "','" & (TXTBAL.Text) & "','" & Format(DTPRECEIPT.Value, "dd/MM/yyyy") & "')", con)
                con.Open()
                cmd.ExecuteNonQuery()
                'MsgBox("SAVED SUCCESSFULLY")
                'CLEAR()
                'DISABLE()
                'DataGrid()
                con.Close()
                If con.State = 1 Then con.Close()
                Dim cmd1 As New OleDb.OleDbCommand("UPDATE INVOICE SET I_bit = " & (CheckBox1.Checked) & " WHERE I_INVID = " & (LBINVID.Text) & "", con)
                con.Open()
                cmd1.ExecuteNonQuery()
                MsgBox("SAVED SUCCESSFULLY")
                CLEAR()
                DISABLE()
                RECEIPTFRM.Show()
                'DataGrid()
            Else
                MsgBox("ENTER ALL FIELDS")
                CLEAR()
                DISABLE()
            End If
        End If
    End Sub
    Public Sub ENABLE()
        TXTAMOUNT.Enabled = True
        TXTBAL.Enabled = True
        TXTPAID.Enabled = True
        DTPRECEIPT.Enabled = True
    End Sub
    Public Sub DISABLE()
        TXTAMOUNT.Enabled = False
        TXTBAL.Enabled = False
        TXTPAID.Enabled = False
        DTPRECEIPT.Enabled = False
    End Sub
    Public Sub CLEAR()
        TXTAMOUNT.Text = ""
        TXTBAL.Text = ""
        TXTPAID.Text = ""
        TXTID.Text = ""
    End Sub

    Private Sub TXTPAID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTPAID.TextChanged
        TXTBAL.Text = Val(TXTAMOUNT.Text) - Val(TXTPAID.Text)
        If Val(TXTBAL.Text) < 0 Then
            MsgBox("NEGATIVE AMOUNT")
            TXTBAL.Text = ""
            TXTPAID.Text = ""
        End If

    End Sub
End Class
