Imports System
Imports System.Data
Imports System.Data.OleDb
Public Class ITEMMASTERFRM

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ITEMMASTERFRM_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
    End Sub

    Private Sub ITEMMASTERFRM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BTSAVE.Text = "ADD"
        BTUPDATE.Text = "MODIFY"
        DISABLE()
        CLEAR()
        Datagrid()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTEXIT.Click
        Me.Close()
    End Sub

    Private Sub BITEM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
       
    End Sub
    Public Sub CLEAR()
        txtid.Text = ""
        txtname.Text = ""
        txtrate.Text = ""
        txtremark.Text = ""
    End Sub
    Public Sub ENABLE()
        txtname.Enabled = True
        txtrate.Enabled = True
        txtremark.Enabled = True
    End Sub
    Public Sub DISABLE()
        txtname.Enabled = False
        txtrate.Enabled = False
        txtremark.Enabled = False
    End Sub
    Public Sub Datagrid()
        If con.State = 1 Then con.Close()
        Dim cmd As New OleDb.OleDbDataAdapter("Select * from ITEMMaster", con)
        con.Open()
        Dim ds As New DataSet
        cmd.Fill(ds, "table")
        DataGridView1.DataSource = ds.Tables("table")
        con.Close()
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        txtid.Text = DataGridView1.CurrentRow.Cells("I_ID").Value
        txtname.Text = DataGridView1.CurrentRow.Cells("I_NAME").Value
        txtrate.Text = DataGridView1.CurrentRow.Cells("I_RATE").Value
        txtremark.Text = DataGridView1.CurrentRow.Cells("I_REMARK").Value
    End Sub

    Private Sub BTUPDATE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTUPDATE.Click
        If BTUPDATE.Text = "MODIFY" Then
            BTUPDATE.Text = "UPDATE"
            ENABLE()
        ElseIf BTUPDATE.Text = "UPDATE" Then
            BTUPDATE.Text = "MODIFY"
            If txtname.Text <> "" And txtrate.Text <> "" And txtremark.Text <> "" Then
                Dim cmd As New OleDb.OleDbCommand("Update ITEMMaster set I_Name = '" & (txtname.Text) & "',I_RATE = '" & (txtrate.Text) & "',I_REMARK = '" & (txtremark.Text) & "' Where I_id = " & (txtid.Text) & "", con)
                con.Open()
                MsgBox("MODIFIED SUCCESSFULLY")
                Datagrid()
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

    Private Sub BTSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTSAVE.Click
        If BTSAVE.Text = "ADD" Then
            BTSAVE.Text = "SAVE"
            ENABLE()
            CLEAR()
            If con.State = 1 Then con.Close()
            Dim cmd As New OleDb.OleDbCommand("Select max(I_id)from ITEMmaster", con)
            Dim str As String
            con.Open()
            str = cmd.ExecuteScalar
            If str > 0 Then
                txtid.Text = str + 1
            Else
                txtid.Text = "1"
            End If
            con.Close()
        ElseIf BTSAVE.Text = "SAVE" Then
            BTSAVE.Text = "ADD"
            If txtname.Text <> "" And txtrate.Text <> "" And txtremark.Text <> "" Then
                If con.State = 1 Then con.Close()
                Dim cmd As New OleDb.OleDbCommand("Insert into ITEMmaster values('" & (txtid.Text) & "','" & (txtname.Text) & "','" & (txtrate.Text) & "','" & (txtremark.Text) & "')", con)
                con.Open()
                cmd.ExecuteNonQuery()
                MsgBox("SAVED SUCCESSFULLY")
                CLEAR()
                DISABLE()
                Datagrid()
                con.Close()
            Else
                MsgBox("ENTER ALL FIELDS")
                CLEAR()
                DISABLE()
            End If
        End If
    End Sub
End Class
