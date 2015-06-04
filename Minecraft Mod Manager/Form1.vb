Imports System
Imports System.IO

Public Class Form1

    Public ModFolder As String

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.F5 Then

            ReloadMods()

        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Load the modlist for the standart version.
        ComboBox1.SelectedItem = "1.8"

        ReloadMods()

    End Sub

    Function ReloadMods()

        'Create Mod Folder Variable.

        If CheckBox1.Checked Then
            ModFolder = Environ$("appdata") + "/.minecraft/mods"
        Else
            ModFolder = Environ$("appdata") + "/.minecraft/mods/" + ComboBox1.Text
        End If

        'Clear List

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        'Check if the folder for mods exists and check that it doesn't generates a no version folder.

        If Not My.Computer.FileSystem.DirectoryExists(ModFolder) Then
            My.Computer.FileSystem.CreateDirectory(ModFolder)
        End If

        'Check if the folder for disabled mods exists.

        If Not My.Computer.FileSystem.DirectoryExists(ModFolder + "/_disabled/") Then
            My.Computer.FileSystem.CreateDirectory(ModFolder + "/_disabled/")
        End If

        'Enabled Mods

        For Each ModFile In Directory.GetFiles(ModFolder)
            ListBox1.Items.Add(ModFile.Replace(ModFolder + "\", ""))
        Next

        'Disabled Mods

        For Each ModFile In Directory.GetFiles(ModFolder + "/_disabled/")
            ListBox2.Items.Add(ModFile.Replace(ModFolder + "/_disabled/", ""))
        Next

        Return Nothing

    End Function

    Private Sub ComboBox1_Menu(sender As Object, e As EventArgs) Handles ComboBox1.SelectionChangeCommitted

        'Change mod folder on version change

        ReloadMods()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Toggle a mod.

        If ListBox1.SelectedItems.Count = 0 And ListBox2.SelectedItems.Count = 0 Then

            MessageBox.Show("Please select a mod to first.")

        ElseIf ListBox1.SelectedItems.Count = 1 And ListBox2.SelectedItems.Count = 0 Then

            If File.Exists(ModFolder + "/" + ListBox1.SelectedItem) Then
                My.Computer.FileSystem.MoveFile(ModFolder + "/" + ListBox1.SelectedItem, ModFolder + "/_disabled/" + ListBox1.SelectedItem)
            End If

        ElseIf ListBox1.SelectedItems.Count = 0 And ListBox2.SelectedItems.Count = 1 Then

            If File.Exists(ModFolder + "/_disabled/" + ListBox2.SelectedItem) Then
                My.Computer.FileSystem.MoveFile(ModFolder + "/_disabled/" + ListBox2.SelectedItem, ModFolder + "/" + ListBox2.SelectedItem)
            End If

        End If

        ReloadMods()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'Import a new mod

        If CheckBox1.Checked Then

            OpenFileDialog1.Filter = "Liteloader Mods|*.litemod"
            OpenFileDialog1.FileName = "LiteLoaderMod.litemod"

        Else

            OpenFileDialog1.Filter = "Minecraft Forge Jar Mods|*.jar"
            OpenFileDialog1.FileName = "MinecraftForgeMod.jar"

        End If

        OpenFileDialog1.ShowDialog()

        If File.Exists(OpenFileDialog1.FileName) Then
            My.Computer.FileSystem.MoveFile(OpenFileDialog1.FileName, ModFolder + "/_disabled/" + Path.GetFileName(OpenFileDialog1.FileName))
        End If

        OpenFileDialog1.Dispose()

        ReloadMods()

    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click

        If CheckBox1.Checked Then
            ComboBox1.Enabled = False
            ComboBox1.Items.Add("No Version Control")
            ComboBox1.Text = "No Version Control"
        Else
            ComboBox1.Text = "1.8"
            ComboBox1.Items.Remove("No Version Control")
            ComboBox1.Enabled = True
        End If

        ReloadMods()

    End Sub

    Private Sub ListBox1_Click(sender As Object, e As EventArgs) Handles ListBox1.Click

        ListBox2.ClearSelected()

    End Sub

    Private Sub ListBox2_Click(sender As Object, e As EventArgs) Handles ListBox2.Click

        ListBox1.ClearSelected()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If ListBox1.SelectedItems.Count = 1 And ListBox2.SelectedItems.Count = 0 Then

            If File.Exists(ModFolder + "/" + ListBox1.SelectedItem) Then
                My.Computer.FileSystem.DeleteFile(ModFolder + "/" + ListBox1.SelectedItem)
            End If

        ElseIf ListBox1.SelectedItems.Count = 0 And ListBox2.SelectedItems.Count = 1 Then

            If File.Exists(ModFolder + "/_disabled/" + ListBox2.SelectedItem) Then
                My.Computer.FileSystem.DeleteFile(ModFolder + "/_disabled/" + ListBox2.SelectedItem)
            End If

        End If

        ReloadMods()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Process.Start("http://mclucario.de/wordpress/programs")

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked

        Process.Start("http://files.minecraftforge.net/#Downloads")

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

        Process.Start("http://www.liteloader.com/")

    End Sub



End Class