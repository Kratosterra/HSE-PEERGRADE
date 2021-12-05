﻿
namespace Peergrade004
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewTabOrWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItalicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BoldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnderlinedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StrikethroughToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JournalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoSaveInfoToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.FileVersionToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.CompilThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.NewTabToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CloseTabToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ItalicToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.BoldToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.UnderlineToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.StrikeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.StartCompilingToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SettingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.OpenFileDialogOne = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialogOne = new System.Windows.Forms.SaveFileDialog();
            this.TimerSave = new System.Windows.Forms.Timer(this.components);
            this.ContextMainMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GetAllTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CutExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FormatExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItalicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BoldContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnderlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StrikeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontOfChoisenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LableNotification = new System.Windows.Forms.Label();
            this.TimerAutoSave = new System.Windows.Forms.Timer(this.components);
            this.MenuStrip.SuspendLayout();
            this.ToolStrip.SuspendLayout();
            this.ContextMainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.Color.White;
            this.MenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.FormatToolStripMenuItem,
            this.SettingsToolStripMenuItem,
            this.JournalToolStripMenuItem,
            this.CompilThisToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(824, 24);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTabOrWindowToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.SaveAsToolStripMenuItem,
            this.SaveAllToolStripMenuItem,
            this.CloseAppToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // NewTabOrWindowToolStripMenuItem
            // 
            this.NewTabOrWindowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTabToolStripMenuItem,
            this.NewWindowToolStripMenuItem});
            this.NewTabOrWindowToolStripMenuItem.Name = "NewTabOrWindowToolStripMenuItem";
            this.NewTabOrWindowToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.NewTabOrWindowToolStripMenuItem.Text = "Создать";
            // 
            // NewTabToolStripMenuItem
            // 
            this.NewTabToolStripMenuItem.Name = "NewTabToolStripMenuItem";
            this.NewTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewTabToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.NewTabToolStripMenuItem.Text = "Новая вкладка";
            this.NewTabToolStripMenuItem.Click += new System.EventHandler(this.NewTabToolStripMenuItem_Click);
            // 
            // NewWindowToolStripMenuItem
            // 
            this.NewWindowToolStripMenuItem.Name = "NewWindowToolStripMenuItem";
            this.NewWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.NewWindowToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.NewWindowToolStripMenuItem.Text = "Новое окно";
            this.NewWindowToolStripMenuItem.Click += new System.EventHandler(this.NewWindowToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.OpenToolStripMenuItem.Text = "Открыть...";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.SaveAsToolStripMenuItem.Text = "Сохранить как...";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // SaveAllToolStripMenuItem
            // 
            this.SaveAllToolStripMenuItem.Name = "SaveAllToolStripMenuItem";
            this.SaveAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.SaveAllToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.SaveAllToolStripMenuItem.Text = "Сохранить всё";
            this.SaveAllToolStripMenuItem.Click += new System.EventHandler(this.SaveAllToolStripMenuItem_Click);
            // 
            // CloseAppToolStripMenuItem
            // 
            this.CloseAppToolStripMenuItem.Name = "CloseAppToolStripMenuItem";
            this.CloseAppToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.CloseAppToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.CloseAppToolStripMenuItem.Text = "Закрыть приложение";
            this.CloseAppToolStripMenuItem.Click += new System.EventHandler(this.CloseAppToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UndoToolStripMenuItem,
            this.RedoToolStripMenuItem,
            this.CutToolStripMenuItem,
            this.CopyToolStripMenuItem,
            this.InsertToolStripMenuItem,
            this.DeleteToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.EditToolStripMenuItem.Text = "Правка";
            // 
            // UndoToolStripMenuItem
            // 
            this.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            this.UndoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.UndoToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.UndoToolStripMenuItem.Text = "Отменить";
            this.UndoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItem_Click);
            // 
            // RedoToolStripMenuItem
            // 
            this.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem";
            this.RedoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.RedoToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.RedoToolStripMenuItem.Text = "Повтор действия";
            this.RedoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItem_Click);
            // 
            // CutToolStripMenuItem
            // 
            this.CutToolStripMenuItem.Name = "CutToolStripMenuItem";
            this.CutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.CutToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.CutToolStripMenuItem.Text = "Вырезать";
            this.CutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.CopyToolStripMenuItem.Text = "Копировать";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // InsertToolStripMenuItem
            // 
            this.InsertToolStripMenuItem.Name = "InsertToolStripMenuItem";
            this.InsertToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.InsertToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.InsertToolStripMenuItem.Text = "Вставить";
            this.InsertToolStripMenuItem.Click += new System.EventHandler(this.InsertToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Delete)));
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.DeleteToolStripMenuItem.Text = "Удалить всё";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // FormatToolStripMenuItem
            // 
            this.FormatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItalicsToolStripMenuItem,
            this.BoldToolStripMenuItem,
            this.UnderlinedToolStripMenuItem,
            this.StrikethroughToolStripMenuItem,
            this.FontToolStripMenuItem});
            this.FormatToolStripMenuItem.Name = "FormatToolStripMenuItem";
            this.FormatToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.FormatToolStripMenuItem.Text = "Формат";
            // 
            // ItalicsToolStripMenuItem
            // 
            this.ItalicsToolStripMenuItem.Name = "ItalicsToolStripMenuItem";
            this.ItalicsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.ItalicsToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.ItalicsToolStripMenuItem.Text = "Курсив";
            this.ItalicsToolStripMenuItem.Click += new System.EventHandler(this.ItalicsToolStripMenuItem_Click);
            // 
            // BoldToolStripMenuItem
            // 
            this.BoldToolStripMenuItem.Name = "BoldToolStripMenuItem";
            this.BoldToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.BoldToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.BoldToolStripMenuItem.Text = "Жирный";
            this.BoldToolStripMenuItem.Click += new System.EventHandler(this.BoldToolStripMenuItem_Click);
            // 
            // UnderlinedToolStripMenuItem
            // 
            this.UnderlinedToolStripMenuItem.Name = "UnderlinedToolStripMenuItem";
            this.UnderlinedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.UnderlinedToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.UnderlinedToolStripMenuItem.Text = "Подчёркнутый";
            this.UnderlinedToolStripMenuItem.Click += new System.EventHandler(this.UnderlinedToolStripMenuItem_Click);
            // 
            // StrikethroughToolStripMenuItem
            // 
            this.StrikethroughToolStripMenuItem.Name = "StrikethroughToolStripMenuItem";
            this.StrikethroughToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.StrikethroughToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.StrikethroughToolStripMenuItem.Text = "Зачёркнутый";
            this.StrikethroughToolStripMenuItem.Click += new System.EventHandler(this.StrikeoutToolStripMenuItem_Click);
            // 
            // FontToolStripMenuItem
            // 
            this.FontToolStripMenuItem.Name = "FontToolStripMenuItem";
            this.FontToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.FontToolStripMenuItem.Text = "Шрифт";
            this.FontToolStripMenuItem.Click += new System.EventHandler(this.FontToolStripMenuItem_Click);
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.SettingsToolStripMenuItem.Text = "Настройки";
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // JournalToolStripMenuItem
            // 
            this.JournalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AutoSaveInfoToolStripTextBox,
            this.FileVersionToolStripComboBox});
            this.JournalToolStripMenuItem.Name = "JournalToolStripMenuItem";
            this.JournalToolStripMenuItem.Size = new System.Drawing.Size(156, 20);
            this.JournalToolStripMenuItem.Text = "Журнал автосохранений";
            this.JournalToolStripMenuItem.DropDownClosed += new System.EventHandler(this.JournalToolStripMenuItem_Click);
            this.JournalToolStripMenuItem.Click += new System.EventHandler(this.JournalToolStripMenuItem_Click);
            // 
            // AutoSaveInfoToolStripTextBox
            // 
            this.AutoSaveInfoToolStripTextBox.BackColor = System.Drawing.Color.White;
            this.AutoSaveInfoToolStripTextBox.Name = "AutoSaveInfoToolStripTextBox";
            this.AutoSaveInfoToolStripTextBox.ReadOnly = true;
            this.AutoSaveInfoToolStripTextBox.Size = new System.Drawing.Size(100, 23);
            this.AutoSaveInfoToolStripTextBox.Text = "Версия файла";
            // 
            // FileVersionToolStripComboBox
            // 
            this.FileVersionToolStripComboBox.Items.AddRange(new object[] {
            "Текущая"});
            this.FileVersionToolStripComboBox.Name = "FileVersionToolStripComboBox";
            this.FileVersionToolStripComboBox.Size = new System.Drawing.Size(121, 23);
            this.FileVersionToolStripComboBox.DropDown += new System.EventHandler(this.FileVersionToolStripComboBox_DropDown);
            this.FileVersionToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.FileVersionToolStripComboBox_SelectedIndexChanged);
            this.FileVersionToolStripComboBox.Click += new System.EventHandler(this.FileVersionToolStripComboBox_Click);
            // 
            // CompilThisToolStripMenuItem
            // 
            this.CompilThisToolStripMenuItem.Name = "CompilThisToolStripMenuItem";
            this.CompilThisToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.CompilThisToolStripMenuItem.Text = "Скомпилировать";
            this.CompilThisToolStripMenuItem.Click += new System.EventHandler(this.StartCompilingToolStripButton_Click);
            // 
            // ToolStrip
            // 
            this.ToolStrip.BackColor = System.Drawing.Color.White;
            this.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTabToolStripButton,
            this.CloseTabToolStripButton,
            this.SaveToolStripButton,
            this.ItalicToolStripButton,
            this.BoldToolStripButton,
            this.UnderlineToolStripButton,
            this.StrikeToolStripButton,
            this.StartCompilingToolStripButton,
            this.SettingsToolStripButton});
            this.ToolStrip.Location = new System.Drawing.Point(0, 24);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(824, 25);
            this.ToolStrip.Stretch = true;
            this.ToolStrip.TabIndex = 3;
            this.ToolStrip.Text = "toolStrip1";
            // 
            // NewTabToolStripButton
            // 
            this.NewTabToolStripButton.BackColor = System.Drawing.Color.Transparent;
            this.NewTabToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewTabToolStripButton.Image = global::Peergrade004.Properties.Resources._17340;
            this.NewTabToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewTabToolStripButton.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.NewTabToolStripButton.Name = "NewTabToolStripButton";
            this.NewTabToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.NewTabToolStripButton.Text = "Новая вкладка";
            this.NewTabToolStripButton.Click += new System.EventHandler(this.NewTabToolStripMenuItem_Click);
            // 
            // CloseTabToolStripButton
            // 
            this.CloseTabToolStripButton.BackColor = System.Drawing.Color.Transparent;
            this.CloseTabToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CloseTabToolStripButton.Image = global::Peergrade004.Properties.Resources.kisspng_computer_icons_icon_design_close_icon_5b45abb757d510_7808943515312925993598;
            this.CloseTabToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseTabToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 15, 2);
            this.CloseTabToolStripButton.Name = "CloseTabToolStripButton";
            this.CloseTabToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.CloseTabToolStripButton.Text = "Закрыть данную вкладку";
            this.CloseTabToolStripButton.Click += new System.EventHandler(this.CloseTabToolStripButton_Click);
            // 
            // SaveToolStripButton
            // 
            this.SaveToolStripButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveToolStripButton.Image = global::Peergrade004.Properties.Resources._69539;
            this.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.SaveToolStripButton.Name = "SaveToolStripButton";
            this.SaveToolStripButton.Size = new System.Drawing.Size(23, 21);
            this.SaveToolStripButton.Text = "Сохранить";
            this.SaveToolStripButton.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // ItalicToolStripButton
            // 
            this.ItalicToolStripButton.BackColor = System.Drawing.Color.Transparent;
            this.ItalicToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ItalicToolStripButton.Image = global::Peergrade004.Properties.Resources.It;
            this.ItalicToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ItalicToolStripButton.Name = "ItalicToolStripButton";
            this.ItalicToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ItalicToolStripButton.Text = "Курсив";
            this.ItalicToolStripButton.Click += new System.EventHandler(this.ItalicsToolStripMenuItem_Click);
            // 
            // BoldToolStripButton
            // 
            this.BoldToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BoldToolStripButton.Image = global::Peergrade004.Properties.Resources.B;
            this.BoldToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BoldToolStripButton.Name = "BoldToolStripButton";
            this.BoldToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.BoldToolStripButton.Text = "Жирный ";
            this.BoldToolStripButton.Click += new System.EventHandler(this.BoldToolStripMenuItem_Click);
            // 
            // UnderlineToolStripButton
            // 
            this.UnderlineToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UnderlineToolStripButton.Image = global::Peergrade004.Properties.Resources.P;
            this.UnderlineToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UnderlineToolStripButton.Name = "UnderlineToolStripButton";
            this.UnderlineToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.UnderlineToolStripButton.Text = "Подчёркнутый";
            this.UnderlineToolStripButton.Click += new System.EventHandler(this.UnderlinedToolStripMenuItem_Click);
            // 
            // StrikeToolStripButton
            // 
            this.StrikeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StrikeToolStripButton.Image = global::Peergrade004.Properties.Resources._39569;
            this.StrikeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StrikeToolStripButton.Name = "StrikeToolStripButton";
            this.StrikeToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.StrikeToolStripButton.Text = "Перечёркнутый";
            this.StrikeToolStripButton.Click += new System.EventHandler(this.StrikeoutToolStripMenuItem_Click);
            // 
            // StartCompilingToolStripButton
            // 
            this.StartCompilingToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartCompilingToolStripButton.Image = global::Peergrade004.Properties.Resources.arrow_gc80cead99_1920;
            this.StartCompilingToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartCompilingToolStripButton.Name = "StartCompilingToolStripButton";
            this.StartCompilingToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.StartCompilingToolStripButton.Text = "Скомпилировать";
            this.StartCompilingToolStripButton.Click += new System.EventHandler(this.StartCompilingToolStripButton_Click);
            // 
            // SettingsToolStripButton
            // 
            this.SettingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SettingsToolStripButton.Image = global::Peergrade004.Properties.Resources.pngwing_com__4_;
            this.SettingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SettingsToolStripButton.Name = "SettingsToolStripButton";
            this.SettingsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.SettingsToolStripButton.Text = "Настройки";
            this.SettingsToolStripButton.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // MainTabControl
            // 
            this.MainTabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MainTabControl.Location = new System.Drawing.Point(0, 54);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.ShowToolTips = true;
            this.MainTabControl.Size = new System.Drawing.Size(824, 548);
            this.MainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.MainTabControl.TabIndex = 2;
            this.MainTabControl.TabStop = false;
            this.MainTabControl.Visible = false;
            // 
            // OpenFileDialogOne
            // 
            this.OpenFileDialogOne.FileName = "openFileDialog1";
            this.OpenFileDialogOne.Filter = "Text files(*.txt)|*.txt|Rich Text Format files(*.rtf)|*.rtf|C# files(*.cs)|*.cs|A" +
    "ll files(*.*)|*.*";
            // 
            // SaveFileDialogOne
            // 
            this.SaveFileDialogOne.DefaultExt = "txt";
            this.SaveFileDialogOne.Filter = "Text files(*.txt)|*.txt|Rich Text Format files(*.rtf)|*.rtf|C# files(*.cs)|*.cs|A" +
    "ll files(*.*)|*.*";
            // 
            // TimerSave
            // 
            this.TimerSave.Enabled = true;
            this.TimerSave.Interval = 10000;
            this.TimerSave.Tick += new System.EventHandler(this.TimerSave_Tick);
            // 
            // ContextMainMenuStrip
            // 
            this.ContextMainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GetAllTextToolStripMenuItem,
            this.CutExtendToolStripMenuItem,
            this.CopyExtendToolStripMenuItem,
            this.InsertExtendToolStripMenuItem,
            this.FormatExtendToolStripMenuItem,
            this.FontOfChoisenToolStripMenuItem});
            this.ContextMainMenuStrip.Name = "contextMenuStrip1";
            this.ContextMainMenuStrip.Size = new System.Drawing.Size(180, 136);
            // 
            // GetAllTextToolStripMenuItem
            // 
            this.GetAllTextToolStripMenuItem.Name = "GetAllTextToolStripMenuItem";
            this.GetAllTextToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.GetAllTextToolStripMenuItem.Text = "Выбрать весь текст";
            this.GetAllTextToolStripMenuItem.Click += new System.EventHandler(this.GetAllTextToolStripMenuItem_Click);
            // 
            // CutExtendToolStripMenuItem
            // 
            this.CutExtendToolStripMenuItem.Name = "CutExtendToolStripMenuItem";
            this.CutExtendToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.CutExtendToolStripMenuItem.Text = "Вырезать";
            this.CutExtendToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            // 
            // CopyExtendToolStripMenuItem
            // 
            this.CopyExtendToolStripMenuItem.Name = "CopyExtendToolStripMenuItem";
            this.CopyExtendToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.CopyExtendToolStripMenuItem.Text = "Копировать";
            this.CopyExtendToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // InsertExtendToolStripMenuItem
            // 
            this.InsertExtendToolStripMenuItem.Name = "InsertExtendToolStripMenuItem";
            this.InsertExtendToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.InsertExtendToolStripMenuItem.Text = "Вставить";
            this.InsertExtendToolStripMenuItem.Click += new System.EventHandler(this.InsertToolStripMenuItem_Click);
            // 
            // FormatExtendToolStripMenuItem
            // 
            this.FormatExtendToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItalicToolStripMenuItem,
            this.BoldContextToolStripMenuItem,
            this.UnderlineToolStripMenuItem,
            this.StrikeToolStripMenuItem});
            this.FormatExtendToolStripMenuItem.Name = "FormatExtendToolStripMenuItem";
            this.FormatExtendToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.FormatExtendToolStripMenuItem.Text = "Формат";
            // 
            // ItalicToolStripMenuItem
            // 
            this.ItalicToolStripMenuItem.Name = "ItalicToolStripMenuItem";
            this.ItalicToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.ItalicToolStripMenuItem.Text = "Курсив";
            this.ItalicToolStripMenuItem.Click += new System.EventHandler(this.ItalicsToolStripMenuItem_Click);
            // 
            // BoldContextToolStripMenuItem
            // 
            this.BoldContextToolStripMenuItem.Name = "BoldContextToolStripMenuItem";
            this.BoldContextToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.BoldContextToolStripMenuItem.Text = "Жирный";
            this.BoldContextToolStripMenuItem.Click += new System.EventHandler(this.BoldToolStripMenuItem_Click);
            // 
            // UnderlineToolStripMenuItem
            // 
            this.UnderlineToolStripMenuItem.Name = "UnderlineToolStripMenuItem";
            this.UnderlineToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.UnderlineToolStripMenuItem.Text = "Подчёркнутый";
            this.UnderlineToolStripMenuItem.Click += new System.EventHandler(this.UnderlinedToolStripMenuItem_Click);
            // 
            // StrikeToolStripMenuItem
            // 
            this.StrikeToolStripMenuItem.Name = "StrikeToolStripMenuItem";
            this.StrikeToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.StrikeToolStripMenuItem.Text = "Зачеркнутый";
            this.StrikeToolStripMenuItem.Click += new System.EventHandler(this.StrikeoutToolStripMenuItem_Click);
            // 
            // FontOfChoisenToolStripMenuItem
            // 
            this.FontOfChoisenToolStripMenuItem.Name = "FontOfChoisenToolStripMenuItem";
            this.FontOfChoisenToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.FontOfChoisenToolStripMenuItem.Text = "Шрифт";
            this.FontOfChoisenToolStripMenuItem.Click += new System.EventHandler(this.FontOfChoisenToolStripMenuItem_Click);
            // 
            // LableNotification
            // 
            this.LableNotification.AutoSize = true;
            this.LableNotification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.LableNotification.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LableNotification.Location = new System.Drawing.Point(588, 0);
            this.LableNotification.Name = "LableNotification";
            this.LableNotification.Size = new System.Drawing.Size(230, 15);
            this.LableNotification.TabIndex = 4;
            this.LableNotification.Text = "Автоматическое сохранение завершено";
            this.LableNotification.Visible = false;
            // 
            // TimerAutoSave
            // 
            this.TimerAutoSave.Enabled = true;
            this.TimerAutoSave.Interval = 10000;
            this.TimerAutoSave.Tick += new System.EventHandler(this.TimerAutoSave_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(824, 601);
            this.Controls.Add(this.LableNotification);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.MainTabControl);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(840, 640);
            this.MinimumSize = new System.Drawing.Size(840, 640);
            this.Name = "FormMain";
            this.Text = "Notepad+";
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Click += new System.EventHandler(this.FormMain_Activated);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.ContextMainMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewTabOrWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseAppToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UndoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RedoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ItalicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BoldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UnderlinedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StrikethroughToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem JournalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton NewTabToolStripButton;
        private System.Windows.Forms.ToolStripButton CloseTabToolStripButton;
        private System.Windows.Forms.ToolStripButton SaveToolStripButton;
        private System.Windows.Forms.ToolStripButton ItalicToolStripButton;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.OpenFileDialog OpenFileDialogOne;
        private System.Windows.Forms.SaveFileDialog SaveFileDialogOne;
        private System.Windows.Forms.Timer TimerSave;
        private System.Windows.Forms.ContextMenuStrip ContextMainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem GetAllTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CutExtendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyExtendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertExtendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FormatExtendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ItalicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BoldContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UnderlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StrikeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FontToolStripMenuItem;
        private System.Windows.Forms.Label LableNotification;
        private System.Windows.Forms.ToolStripButton BoldToolStripButton;
        private System.Windows.Forms.ToolStripButton UnderlineToolStripButton;
        private System.Windows.Forms.ToolStripButton StrikeToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem FontOfChoisenToolStripMenuItem;
        private System.Windows.Forms.Timer TimerAutoSave;
        private System.Windows.Forms.ToolStripTextBox AutoSaveInfoToolStripTextBox;
        private System.Windows.Forms.ToolStripComboBox FileVersionToolStripComboBox;
        private System.Windows.Forms.ToolStripButton StartCompilingToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem CompilThisToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton SettingsToolStripButton;
    }
}

