Imports System
Imports System.Data.OleDb
Public Class CUSTOMERFRM

    Private Sub CUSTOMERFRM_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
    End Sub

    Private Sub CUSTOMERFRM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BTSAVE.Text = "ADD"
        BTUPDATE.Text = "MODIFY"
        CLEAR()
        DISABLE()
        Datagrid()
    End Sub

    Private Sub BTEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTEXIT.Click
        Me.Close()
    End Sub

    Private Sub BTSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTSAVE.Click
        If BTSAVE.Text = "ADD" Then
            BTSAVE.Text = "SAVE"
            ENABLE()
            CLEAR()
            If con.State = 1 Then con.Close()
            Dim cmd As New OleDb.OleDbCommand("Select max(C_id)from COSTUMERmaster", con)
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
            If TXTADDRESS.Text <> "" And TXTMAIL.Text <> "" And TXTMOBILE.Text <> "" And txtname.Text <> "" And TXTPHONE.Text <> "" Then
                If con.State = 1 Then con.Close()
                Dim cmd As New OleDb.OleDbCommand("Insert into COSTUMERMaster values ('" & (txtid.Text) & "','" & (txtname.Text) & "','" & (TXTPHONE.Text) & "','" & (TXTMOBILE.Text) & "','" & (TXTADDRESS.Text) & "','" & (TXTMAIL.Text) & "')", con)
                con.Open()
                cmd.ExecuteNonQuery()
                MsgBox("Saved Successfully")
                Datagrid()
                DISABLE()
                CLEAR()
                con.Close()
            Else
                MsgBox("ENTER ALL FIELDS")
            End If
        End If
    End Sub
    Public Sub CLEAR()
        txtid.Text = ""
        TXTADDRESS.Text = ""
        TXTMOBILE.Text = ""
        txtname.Text = ""
        TXTPHONE.Text = ""
        TXTMAIL.Text = ""
    End Sub
    Public Sub ENABLE()
        TXTADDRESS.Enabled = True
        TXTMAIL.Enabled = True
        TXTMOBILE.Enabled = True
        txtname.Enabled = True
        TXTPHONE.Enabled = True
    End Sub
    Public Sub DISABLE()
        TXTADDRESS.Enabled = False
        TXTMAIL.Enabled = False
        TXTMOBILE.Enabled = False
        txtname.Enabled = False
        TXTPHONE.Enabled = False
    End Sub
    Public Sub Datagrid()
        If con.State = 1 Then con.Close()
        Dim cmd As New OleDb.OleDbDataAdapter("Select * from CostumerMaster", con)
        con.Open()
        Dim ds As New DataSet
        cmd.Fill(ds, "table")
        DataGridView1.DataSource = ds.Tables("table")
        con.Close()
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        txtid.Text = DataGridView1.CurrentRow.Cells("C_ID").Value
        txtname.Text = DataGridView1.CurrentRow.Cells("C_NAME").Value
        TXTPHONE.Text = DataGridView1.CurrentRow.Cells("C_PHONE").Value
        TXTMOBILE.Text = DataGridView1.CurrentRow.Cells("C_MOB").Value
        TXTADDRESS.Text = DataGridView1.CurrentRow.Cells("C_ADDRESS").Value
        TXTMAIL.Text = DataGridView1.CurrentRow.Cells("C_EMAIL").Value
    End Sub

    Private Sub BTUPDATE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTUPDATE.Click
        If BTUPDATE.Text = "MODIFY" Then
            BTUPDATE.Text = "UPDATE"
            ENABLE()
        ElseIf BTUPDATE.Text = "UPDATE" Then
            BTUPDATE.Text = "MODIFY"
            If TXTADDRESS.Text <> "" And TXTMAIL.Text <> "" And TXTMOBILE.Text <> "" And txtname.Text <> "" And TXTPHONE.Text <> "" Then
                If con.State = 1 Then con.Close()
                Dim cmd As New OleDb.OleDbCommand("Update COSTUMERMaster set C_Name = '" & (txtname.Text) & "',C_PHONE = '" & (TXTPHONE.Text) & "',C_MOB = '" & (TXTMOBILE.Text) & "' ,C_ADDRESS = '" & (TXTADDRESS.Text) & "',C_EMAIL = '" & (TXTMAIL.Text) & "' Where C_id = " & (txtid.Text) & "", con)
                con.Open()
                MsgBox("MODIFIED SUCCESFULLY")
                Datagrid()
                DISABLE()
                CLEAR()
                con.Close()
            Else
                MsgBox("ENTER ALL FIELDS")
            End If
        End If
    End Sub
End Class
