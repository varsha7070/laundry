Public Class INVOICEFRM

    Private Sub INVOICEFRM_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
    End Sub

    Private Sub INVOICEFRM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If con.State = 1 Then con.Close()
        Dim cmd As New OleDb.OleDbDataAdapter("select I_name from ITEMmaster", con)
        con.Open()
        Dim ds As New DataSet
        cmd.Fill(ds, "table")
        CMBITEM.DataSource = ds.Tables("table")
        CMBITEM.DisplayMember = "I_id"
        CMBITEM.ValueMember = "I_Name"
        con.Close()
        BTSAVE.Text = "ADD"
        BTUPDATE.Text = "MODIFY"
        DISABLE()
        CLEAR()
    End Sub

    Private Sub BTEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTEXIT.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INVSEARCHFRM.MdiParent = Me
        INVSEARCHFRM.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If CMBITEM.Text <> "" And TXTRATE.Text <> "" And TXTTOTAL.Text <> "" Then
            If con.State = 1 Then con.Close()
            Dim cmd2 As New OleDb.OleDbCommand("Select max(IT_id)from INVITEM", con)
            Dim str1 As String
            con.Open()
            str1 = cmd2.ExecuteScalar
            If str1 > 0 Then
                TXTITEMID.Text = str1 + 1
            Else
                TXTITEMID.Text = "1"
            End If
            con.Close()
            If con.State = 1 Then con.Close()
            Dim cmd As New OleDb.OleDbCommand("Insert into INVITEM values(" & (TXTITEMID.Text) & ",'" & (CMBITEM.Text) & "','" & (TXTRATE.Text) & "','" & (TXTQTY.Text) & "'," & (TXTINVID.Text) & ",'" & (TXTTOTAL.Text) & "')", con)
            con.Open()
            cmd.ExecuteNonQuery()
            'MsgBox("ENTERED")
            INVITEMCLEAR()
            'DISABLE()
            INVITEMDatagrid()
            con.Close()
        Else
            MsgBox("ENTER ALL FIELDS")
            INVITEMCLEAR()
            'DISABLE()
        End If
        If con.State = 1 Then con.Close()
        Dim cmd1 As New OleDb.OleDbCommand("Select SUM(IT_TOTAL) from INVITEM where IT_INVOICEID = " & (TXTINVID.Text) & "", con)
        con.Open()
        Dim str As String
        str = cmd1.ExecuteScalar
        TXTAMOUNT.Text = str
        con.Close()
    End Sub
    Public Sub INVITEMCLEAR()
        TXTITEMID.Text = ""
        CMBITEM.Text = ""
        TXTQTY.Text = ""
        TXTRATE.Text = ""
        TXTTOTAL.Text = ""
    End Sub
    Public Sub INVITEMDatagrid()
        If con.State = 1 Then con.Close()
        Dim cmd As New OleDb.OleDbDataAdapter("Select * from INVITEM WHERE IT_INVOICEID = " & (TXTINVID.Text) & " ", con)
        con.Open()
        Dim ds As New DataSet
        cmd.Fill(ds, "table")
        DataGridView1.DataSource = ds.Tables("table")
        con.Close()
    End Sub

    Private Sub CMBITEM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBITEM.SelectedIndexChanged
        If con.State = 1 Then con.Close()
        Dim cmd As New OleDb.OleDbCommand("Select I_RATE from ITEMMaster where I_NAME = '" & (CMBITEM.Text) & "'", con)
        con.Open()
        Dim str As String
        str = cmd.ExecuteScalar
        TXTRATE.Text = str
        con.Close()
    End Sub

    Private Sub TXTQTY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTQTY.TextChanged
        TXTTOTAL.Text = Val(TXTRATE.Text) * Val(TXTQTY.Text)
    End Sub
    Public Sub ENABLE()
        TXTAMOUNT.Enabled = True
        TXTNAME.Enabled = True
        TXTQTY.Enabled = True
        TXTRATE.Enabled = True
        TXTTOTAL.Enabled = True
        CMBITEM.Enabled = True
        DTPINVOICE.Enabled = True
    End Sub
    Public Sub DISABLE()
        TXTAMOUNT.Enabled = False
        TXTNAME.Enabled = False
        TXTQTY.Enabled = False
        TXTRATE.Enabled = False
        TXTTOTAL.Enabled = False
        CMBITEM.Enabled = False
        DTPINVOICE.Enabled = False
    End Sub
    Public Sub CLEAR()
        TXTITEMID.Text = ""
        CMBITEM.Text = ""
        TXTQTY.Text = ""
        TXTRATE.Text = ""
        TXTTOTAL.Text = ""
        TXTINVID.Text = ""
        TXTAMOUNT.Text = ""
        TXTNAME.Text = ""
    End Sub

    Private Sub BTSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTSAVE.Click
        If BTSAVE.Text = "ADD" Then
            BTSAVE.Text = "SAVE"
            'CLEAR()
            ENABLE()
            If con.State = 1 Then con.Close()
            Dim cmd2 As New OleDb.OleDbCommand("Select max(I_INVID)from INVOICE", con)
            Dim str1 As String
            con.Open()
            str1 = cmd2.ExecuteScalar
            If str1 > 0 Then
                TXTINVID.Text = str1 + 1
            Else
                TXTINVID.Text = "1"
            End If
            con.Close()
        ElseIf BTSAVE.Text = "SAVE" Then
            BTSAVE.Text = "ADD"
            If TXTNAME.Text <> "" And TXTAMOUNT.Text <> "" Then
                If con.State = 1 Then con.Close()
                Dim cmd As New OleDb.OleDbCommand("Insert into INVOICE values (" & (TXTINVID.Text) & ",'" & (TXTNAME.Text) & "','" & (DTPINVOICE.Value) & "','" & (TXTAMOUNT.Text) & "'," & (CheckBox1.Checked) & ")", con)
                con.Open()
                cmd.ExecuteNonQuery()
                MsgBox("Saved Successfully")
                'DataGrid()
                DISABLE()
                CLEAR()
                con.Close()
            Else
                MsgBox("ENTER ALL FIELDS")
                DISABLE()
                CLEAR()
            End If
        End If
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        TXTITEMID.Text = DataGridView1.CurrentRow.Cells("IT_ID").Value
        CMBITEM.Text = DataGridView1.CurrentRow.Cells("IT_NAME").Value
        TXTRATE.Text = DataGridView1.CurrentRow.Cells("IT_RATE").Value
        TXTQTY.Text = DataGridView1.CurrentRow.Cells("IT_QTY").Value
        TXTINVID.Text = DataGridView1.CurrentRow.Cells("IT_INVOICEID").Value
        TXTTOTAL.Text = DataGridView1.CurrentRow.Cells("IT_TOTAL").Value
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If CMBITEM.Text <> "" And TXTRATE.Text <> "" And TXTTOTAL.Text <> "" Then
            If con.State = 1 Then con.Close()
            Dim cmd As New OleDb.OleDbCommand("DELETE FROM INVITEM WHERE IT_ID = " & (TXTITEMID.Text) & " AND IT_INVOICEID = " & (TXTINVID.Text) & "", con)
            con.Open()
            cmd.ExecuteNonQuery()
            'MsgBox("ENTERED")
            INVITEMCLEAR()
            'DISABLE()
            INVITEMDatagrid()
            con.Close()
        Else
            MsgBox("ENTER ALL FIELDS")
            INVITEMCLEAR()
        End If
        If con.State = 1 Then con.Close()
        Dim cmd1 As New OleDb.OleDbCommand("Select SUM(IT_TOTAL) from INVITEM where IT_INVOICEID = " & (TXTINVID.Text) & "", con)
        con.Open()
        Dim str As String
        str = cmd1.ExecuteScalar
        TXTAMOUNT.Text = str
        con.Close()
    End Sub

    Private Sub BTUPDATE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTUPDATE.Click
        MODIFYSEARCH.Show()
        If TXTNAME.Text <> "" And TXTAMOUNT.Text <> "" Then
            If con.State = 1 Then con.Close()
            Dim cmd As New OleDb.OleDbCommand("Update INVOICE set I_CNAME = '" & (TXTNAME.Text) & "',I_DATE = '" & (DTPINVOICE.Value) & "',I_AMOUNT = '" & (TXTAMOUNT.Text) & "' ,I_bit = '" & (CheckBox1.Checked) & "' Where I_INVID = " & (TXTINVID.Text) & "", con)
            con.Open()
            MsgBox("MODIFIED SUCCESFULLY")
            'DataGrid()
            DISABLE()
            CLEAR()
            con.Close()
        Else
            MsgBox("ENTER ALL FIELDS")
            DISABLE()
            CLEAR()
        End If
    End Sub
End Class
