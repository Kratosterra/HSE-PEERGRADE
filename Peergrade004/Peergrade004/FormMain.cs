using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace Peergrade004
{
    public partial class FormMain : Form
    {
        private FontSettings fontSettings;
        private MiniFontSettingsForm miniFontSettings;
        public int fontSize = 14;
        public FontFamily fontFaсe = new Font("Times New Roman", 14).FontFamily;
        private List<string> fileList = new List<string>();
        private List<bool> fileChangeList = new List<bool>();
        PictureBox startPictureBox = new PictureBox();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ReadAllOldTabs();
            ExecuteSettings();
            DeleteAutoSaveDirectory();
            CreateStartMenu();
        }

        private void CreateStartMenu()
        {
            startPictureBox.Location = new Point(400, 54);
            startPictureBox.Size = new Size(600, 548);
            startPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(startPictureBox);

            PictureBox CreatePictureBox = new PictureBox();
            CreatePictureBox.Location = new Point(10, 350);
            CreatePictureBox.Size = new Size(300, 100);
            CreatePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            CreatePictureBox.Image = Properties.Resources.CREATE;
            CreatePictureBox.Click += NewTabToolStripMenuItem_Click;
            this.Controls.Add(CreatePictureBox);

            PictureBox OpenPictureBox = new PictureBox();
            OpenPictureBox.Location = new Point(10, 440);
            OpenPictureBox.Size = new Size(307, 70);
            OpenPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            OpenPictureBox.Image = Properties.Resources.OPEN;
            OpenPictureBox.Click += OpenToolStripMenuItem_Click;
            this.Controls.Add(OpenPictureBox);

            PictureBox ThemePictureBox = new PictureBox();
            ThemePictureBox.Location = new Point(0, 250);
            ThemePictureBox.Size = new Size(380, 100);
            ThemePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            ThemePictureBox.Image = Properties.Resources.NOTEPAD_;
            this.Controls.Add(ThemePictureBox);
        }

        private void NewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTab(out var tabPage, out var richTextBox);
        }

        private void CreateNewTab(out TabPage tabPage, out RichTextBox richTextBox)
        {
            try
            {
                tabPage = new TabPage("Безымянный");
                richTextBox = new RichTextBox();
                richTextBox.Dock = DockStyle.Fill;
                tabPage.Controls.Add(richTextBox);
                MainTabControl.TabPages.Add(tabPage);
                tabPage.Click += FormMain_Activated;
                fileList.Add("-");
                fileChangeList.Add(false);
                richTextBox.Font = new Font("Times New Roman", 12);
                richTextBox.BorderStyle = BorderStyle.None;
                richTextBox.TextChanged += IfTextChanged;
                richTextBox.MouseDown += RichTextBox_MouseDown;
                richTextBox.Click += FormMain_Activated;
                MainTabControl.Visible = true;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при создании новой вкладки!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabPage = null;
                richTextBox = null;
            }
        }

        private void IfTextChanged(object sender, EventArgs e)
        {
            TabPage tabPage = MainTabControl.SelectedTab;
            if (tabPage != null)
            {
                try
                {
                    if (fileChangeList[tabPage.TabIndex] == false)
                    {
                        fileChangeList[tabPage.TabIndex] = true;
                        tabPage.Text = $"*{tabPage.Text}";
                    }
                }
                catch
                {
                }
            }
        }

        private RichTextBox GetTextFromRichTextBox()
        {

            RichTextBox richTextBox = null;
            TabPage tabPage = MainTabControl.SelectedTab;
            if (tabPage != null)
            {
                richTextBox = tabPage.Controls[0] as RichTextBox;
                return richTextBox;
            }
            else
            {
                CreateNewTab(out TabPage newTabPage, out RichTextBox richTextBoxNew);
                return richTextBoxNew;
            }
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetTextFromRichTextBox().Cut();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при вырезании текста!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetTextFromRichTextBox().Copy();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при копировании текста!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetTextFromRichTextBox().Paste();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при вставке текста!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetTextFromRichTextBox().Clear();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при удалении всего текста!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialogOne.FileName = "";
                if (GetTextFromRichTextBox().Text != "")
                {
                    DialogResult userAnswer = MessageBox.Show("Хотите открыть файл в этой вкладке?\n" +
                                                              "Все несохранённые данные будут потеряны!", "Открытие файла",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (userAnswer == DialogResult.Yes)
                    {
                        OpenNewFile();
                    }
                }
                else
                {
                    OpenNewFile();
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при при открытии файла, вероятно она связана с вашей системой!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenNewFile()
        {
            if (OpenFileDialogOne.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader streamData = new StreamReader(OpenFileDialogOne.FileName))
                    {
                        if (CheckSizeOfFile(OpenFileDialogOne.FileName))
                        {
                            string data = streamData.ReadToEnd();
                            if (string.IsNullOrEmpty(data))
                            {
                                data = " ";
                            }

                            if (Path.GetExtension(OpenFileDialogOne.FileName) == ".rtf")
                            {
                                GetTextFromRichTextBox().Rtf = data;
                            }
                            else
                            {
                                GetTextFromRichTextBox().Text = data;
                            }

                            TabPage tabPage = MainTabControl.SelectedTab;
                            tabPage.Text = Path.GetFileName(OpenFileDialogOne.FileName);
                            fileList[tabPage.TabIndex] = OpenFileDialogOne.FileName;
                            fileChangeList[tabPage.TabIndex] = false;
                            DeleteAutoSaveFileInformation(tabPage);
                        }
                        else
                        {
                            MessageBox.Show($"Файл имеет размер больше 1ГБ, отказ в чтении!",
                                "Размер открываемого файла",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("У вас нет доступа к данному файлу!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException)
                {
                    MessageBox.Show("Невозможно открыть файл, он занят другим процессом!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Невозможно открыть файл!" +
                                    " Вероятно он занят другим процессом, или доступ к нему запрещён!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static bool CheckSizeOfFile(string path)
        {
            try
            {
                long length = new FileInfo(path).Length;
                if (length > 1073741824)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при попытке получить размер файла!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = MainTabControl.SelectedTab;
            SaveFile(tabPage);
        }

        private void SaveFile(TabPage tabPage)
        {
            if (tabPage != null)
            {
                try
                {
                    if (fileChangeList[tabPage.TabIndex] || fileList[tabPage.TabIndex] == "-")
                    {
                        if (fileList[tabPage.TabIndex] == "-")
                        {
                            SaveFileDialogOne.FileName = "";
                            if (SaveFileDialogOne.ShowDialog() == DialogResult.OK)
                            {
                                fileList[tabPage.TabIndex] = SaveFileDialogOne.FileName;
                                tabPage.Text = Path.GetFileName(fileList[tabPage.TabIndex]);
                            }
                            else
                            {
                                return;
                            }
                        }

                        using (StreamWriter streamWriter = new StreamWriter(fileList[tabPage.TabIndex]))
                        {
                            RichTextBox richTextBox = tabPage.Controls[0] as RichTextBox;
                            if (Path.GetExtension(fileList[tabPage.TabIndex]) == ".rtf")
                            {
                                if (richTextBox != null)
                                {
                                    streamWriter.Write(richTextBox.Rtf);
                                }
                            }
                            else
                            {
                                if (richTextBox != null)
                                {
                                    streamWriter.Write(richTextBox.Text);
                                }
                            }
                        }

                        tabPage.Text = Path.GetFileName(fileList[tabPage.TabIndex]);
                        fileChangeList[tabPage.TabIndex] = false;
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("У вас нет доступа к сохранению данного файла!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException)
                {
                    MessageBox.Show("Невозможно сохранить файл, он занят другим процессом!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при попытке сохранения файла!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Сохранять нечего! " +
                                "Не открыто и не создано ни одного файла!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = MainTabControl.SelectedTab;
            if (tabPage != null)
            {
                try
                {
                    SaveFileDialogOne.FileName = Path.GetFileName(fileList[tabPage.TabIndex]);
                    if (SaveFileDialogOne.ShowDialog() == DialogResult.OK)
                    {
                        fileList[tabPage.TabIndex] = SaveFileDialogOne.FileName;
                        tabPage.Text = Path.GetFileName(fileList[tabPage.TabIndex]);
                    }
                    else
                    {
                        return;
                    }

                    using (StreamWriter streamWriter = new StreamWriter(fileList[tabPage.TabIndex]))
                    {
                        if (Path.GetExtension(fileList[tabPage.TabIndex]) == ".rtf")
                        {
                            streamWriter.Write(GetTextFromRichTextBox().Rtf);
                        }
                        else
                        {
                            streamWriter.Write(GetTextFromRichTextBox().Text);
                        }
                    }

                    tabPage.Text = Path.GetFileName(fileList[tabPage.TabIndex]);
                    fileChangeList[tabPage.TabIndex] = false;

                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("У вас нет доступа к сохранению данного файла!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException)
                {
                    MessageBox.Show("Невозможно сохранить файл, он занят другим процессом!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при попытке сохранения файла!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Сохранять нечего! " +
                                "Не открыто и не создано ни одного файла!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAllTabs();
        }

        private void SaveAllTabs(bool isThisAutomaticSave = false)
        {
            try
            {
                if (MainTabControl.TabCount == 0 && !isThisAutomaticSave)
                {
                    MessageBox.Show("Сохранять нечего! " +
                                    "Не открыто и не создано ни одного файла!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    for (int i = 0; i < MainTabControl.TabCount; i++)
                    {
                        TabPage tabPage = MainTabControl.TabPages[i];
                        if (fileList[tabPage.TabIndex] == "-" && fileChangeList[tabPage.TabIndex]
                                                              && !isThisAutomaticSave)
                        {
                            MessageBox.Show($"Сохранение Безымянного файла\n" +
                                            $"Если вы закройте окно сохранения файла, он не будет сохранён!",
                                "Cохранение");
                            SaveFile(tabPage);
                        }
                        else if (fileList[tabPage.TabIndex] == "-")
                        {

                        }
                        else if (fileList[tabPage.TabIndex] != "-" && fileChangeList[tabPage.TabIndex] == true)
                        {
                            SaveFile(tabPage);
                        }
                    }

                    if (!isThisAutomaticSave)
                    {
                        MessageBox.Show("Изменения в файлах сохранены!", "Отчёт");
                    }
                }
            }
            catch
            {
                if (!isThisAutomaticSave)
                {
                    MessageBox.Show("Сохранять нечего! " +
                                    "Не открыто и не создано ни одного файла!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (fileChangeList.Contains(true))
                {
                    DialogResult userAnswer = MessageBox.Show("Хотите сохранить несохранёные файлы?", "Выход",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (userAnswer == DialogResult.Yes)
                    {
                        SaveAllTabs();
                        SaveAllOpenTabsInFile();
                    }
                    else if (userAnswer == DialogResult.No)
                    {
                        SaveAllOpenTabsInFile();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    DialogResult userAnswer = MessageBox.Show("Вы точно хотите выйти?\n" +
                                                              "(не обнаружено несохранёных файлов)", "Выход",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (userAnswer == DialogResult.Yes)
                    {
                        SaveAllOpenTabsInFile();
                    }
                    else if (userAnswer == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при завершении работы программы, сохранение данных не производится!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ItalicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Font oldFont;
                Font newFont;

                oldFont = GetTextFromRichTextBox().SelectionFont;
                if (oldFont != null && oldFont.Italic == true)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);
                    GetTextFromRichTextBox().SelectionFont = newFont;
                    GetTextFromRichTextBox().Focus();
                }
                else if (oldFont != null && oldFont.Italic == false)
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);
                    GetTextFromRichTextBox().SelectionFont = newFont;
                    GetTextFromRichTextBox().Focus();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно применить шрифт!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Font oldFont;
                Font newFont;

                oldFont = GetTextFromRichTextBox().SelectionFont;
                if (oldFont != null && oldFont.Bold == true)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);
                    GetTextFromRichTextBox().SelectionFont = newFont;
                    GetTextFromRichTextBox().Focus();
                }
                else if (oldFont != null && oldFont.Bold == false)
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);
                    GetTextFromRichTextBox().SelectionFont = newFont;
                    GetTextFromRichTextBox().Focus();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно применить шрифт!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void UnderlinedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Font oldFont;
                Font newFont;
                oldFont = GetTextFromRichTextBox().SelectionFont;
                if (oldFont != null && oldFont.Underline == true)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);
                    GetTextFromRichTextBox().SelectionFont = newFont;
                    GetTextFromRichTextBox().Focus();
                }
                else if (oldFont != null && oldFont.Underline == false)
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);
                    GetTextFromRichTextBox().SelectionFont = newFont;
                    GetTextFromRichTextBox().Focus();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно применить шрифт!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void StrikeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                Font oldFont;
                Font newFont;

                oldFont = GetTextFromRichTextBox().SelectionFont;
                if (oldFont != null && oldFont.Strikeout == true)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Strikeout);
                    GetTextFromRichTextBox().SelectionFont = newFont;
                    GetTextFromRichTextBox().Focus();
                }
                else if (oldFont != null && oldFont.Strikeout == false)
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Strikeout);
                    GetTextFromRichTextBox().SelectionFont = newFont;
                    GetTextFromRichTextBox().Focus();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно применить шрифт!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FormMain form = new FormMain();
                form.Show();
            }
            catch
            {
                MessageBox.Show("Невозможно открыть новое окно!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetTextFromRichTextBox().CanRedo)
                {
                    if (GetTextFromRichTextBox().RedoActionName != "Delete")
                        GetTextFromRichTextBox().Redo();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при отмене действия!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RichTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                switch (e.Button)
                {
                    case MouseButtons.Right:
                    {
                        ContextMainMenuStrip.Show(this, new Point(e.X, e.Y));
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        private void GetAllTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetTextFromRichTextBox().SelectAll();
            }
            catch
            {
                MessageBox.Show("Ошибка при выборе всего текста!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAllOpenTabsInFile()
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter("HideInformationTabs.txt"))
                {
                    for (int i = 0; i < fileList.Count; i++)
                    {
                        if (fileList[i] != "-")
                        {
                            streamWriter.WriteLine(fileList[i]);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show($"Ошибка сохранения вкладок!\n" +
                                $"Вкладки не будут автоматически открыты при следующем запуске", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadAllOldTabs()
        {
            try
            {
                if (File.Exists("HideInformationTabs.txt"))
                {
                    List<string> data = new List<string>();
                    using (StreamReader streamRider = new StreamReader("HideInformationTabs.txt"))
                    {
                        string line = streamRider.ReadLine();
                        while (line != null)
                        {
                            data.Add(line);
                            line = streamRider.ReadLine();
                        }

                    }

                    File.Create("HideInformationTabs.txt").Close();
                    AddAllOldTabs(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при автоматическом открытии старых вкаладок.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddAllOldTabs(List<string> dataPath)
        {
            try
            {
                for (int i = 0; i < dataPath.Count; i++)
                {
                    try
                    {
                        if (File.Exists(dataPath[i]))
                        {
                            using (StreamReader streamData = new StreamReader(dataPath[i]))
                            {
                                if (CheckSizeOfFile(dataPath[i]))
                                {
                                    string data = streamData.ReadToEnd();
                                    if (string.IsNullOrEmpty(data))
                                    {
                                        data = " ";
                                    }

                                    CreateNewTab(out TabPage tabPage, out RichTextBox richTextBox);
                                    if (Path.GetExtension(dataPath[i]) == ".rtf")
                                    {
                                        richTextBox.Rtf = data;
                                    }
                                    else
                                    {
                                        richTextBox.Text = data;
                                    }

                                    tabPage.Text = Path.GetFileName(dataPath[i]);
                                    fileList[tabPage.TabIndex] = dataPath[i];
                                    fileChangeList[tabPage.TabIndex] = false;
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при открытии старых вкладок!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FormSettings formSettings = new FormSettings();
                formSettings.Show();
            }
            catch
            {
                MessageBox.Show("Невозможно открыть новое окно!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                fontSettings = new FontSettings();
                fontSettings.Show();
            }
            catch
            {
                MessageBox.Show("Невозможно открыть новое окно!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            try
            {
                ExecuteSettings();
                if (miniFontSettings != null && miniFontSettings.fontSize != 0 && miniFontSettings.fontFaсe != null)
                {
                    GetTextFromRichTextBox().SelectionFont = new Font(miniFontSettings.fontFaсe, miniFontSettings.fontSize);
                    GetTextFromRichTextBox().Focus();
                    miniFontSettings.fontFaсe = null;
                    miniFontSettings.fontSize = 0;
                }
                if (fontSettings != null && fontSettings.fontFaсe != null && fontSettings.fontSize != 0)
                {
                    fontSize = fontSettings.fontSize;
                    fontFaсe = fontSettings.fontFaсe;
                    GetTextFromRichTextBox().Font = new Font(fontFaсe, fontSize);
                    fontSettings.fontSize = 0;
                    fontSettings.fontFaсe = null;
                }
            }
            catch
            {

            }
        }

        private void TimerSave_Tick(object sender, EventArgs e)
        {
            try
            {
                LableNotification.Visible = true;
                SaveAllTabs(true);
                LableNotification.Visible = false;
            }
            catch
            {

            }
        }

        public List<string> ReadSettingsFromFile()
        {
            try
            {
                if (File.Exists("Settings.txt"))
                {
                    List<string> data = new List<string>();
                    using (StreamReader streamRider = new StreamReader("Settings.txt"))
                    {
                        string line = streamRider.ReadLine();
                        while (line != null)
                        {
                            data.Add(line);
                            line = streamRider.ReadLine();
                        }

                    }

                    return data;
                }

                return null;
            }
            catch
            {
                MessageBox.Show($"Произошла ошибка при чтении настроек.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void ExecuteSettings()
        {
            try
            {
                List<string> data = ReadSettingsFromFile();
                if (data == null)
                {
                    this.BackColor = Color.WhiteSmoke;
                    startPictureBox.Image = Properties.Resources.White;
                }
                if (data != null && data.Count > 1)
                {
                    SetSettingsFormMain(data);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при исполнении настроек!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SetSettingsFormMain(List<string> data)
        {
            try
            {
                TimerSave.Interval = Int32.Parse(data[0]);
                SetThemeFormMain(data[1]);
                TimerAutoSave.Interval = Int32.Parse(data[2]);
            }
            catch
            {
                MessageBox.Show($"Произошла ошибка при применении настроек.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SetThemeFormMain(string name)
        {
            switch (name)
            {
                case "White Theme":
                    {
                        SetWhiteThemeFormMain();
                        break;
                    }
                case "Black Theme":
                    {
                        SetBlackThemeFormMain();
                        break;
                    }
                case "Hollow Purple Theme":
                    {
                        SetPurpleThemeFormMain();
                        break;
                    }
                case "Delicious Teal Theme":
                    {
                        SetTealThemeFormMain();
                        break;
                    }
            }
        }

        private void SetWhiteThemeFormMain()
        {
            MenuStrip.BackColor = Color.WhiteSmoke;
            ToolStrip.BackColor = Color.WhiteSmoke;
            startPictureBox.Image = Properties.Resources.White;
            this.BackColor = Color.WhiteSmoke;
        }

        private void SetBlackThemeFormMain()
        {
            MenuStrip.BackColor = Color.SlateGray;
            ToolStrip.BackColor = Color.SlateGray;
            startPictureBox.Image = Properties.Resources.Dark;
            this.BackColor = Color.SlateGray;
        }

        private void SetPurpleThemeFormMain()
        {
            MenuStrip.BackColor = Color.Purple;
            ToolStrip.BackColor = Color.Purple;
            startPictureBox.Image = Properties.Resources.Purple;
            this.BackColor = Color.Purple;
        }

        private void SetTealThemeFormMain()
        {
            MenuStrip.BackColor = Color.Aquamarine;
            ToolStrip.BackColor = Color.Aquamarine;
            startPictureBox.Image = Properties.Resources.Teal;
            this.BackColor = Color.Aquamarine;
        }

        private void CloseTabToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainTabControl.TabCount == 0)
                {
                    MessageBox.Show("Удалять нечего, не открыто ни одной вкладки!" +
                                    "", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult userAnswer = MessageBox.Show("Вы действительно хотите закрыть вкладку?\n" +
                                                              "Все несохранённые данные будут потеряны.", "Закрытие вкладки",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (userAnswer == DialogResult.Yes)
                    {
                        TabPage tabPage = MainTabControl.SelectedTab;
                        fileList[tabPage.TabIndex] = "-";
                        fileChangeList[tabPage.TabIndex] = false;
                        MainTabControl.TabPages.Remove(tabPage);
                        DeleteAutoSaveFileInformation(tabPage);
                        if (MainTabControl.TabPages.Count == 0)
                        {
                            MainTabControl.Visible = false;
                        }
                    }
                    else if (userAnswer == DialogResult.No)
                    {
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка во время удаления вкладки!" +
                                "", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DeleteAutoSaveFileInformation(TabPage tabPage)
        {
            try
            {
                string path = $@"./AutoSaveFolder/{tabPage.TabIndex}";
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении автоматических сохранений!");
            }
            finally
            {
                FileVersionToolStripComboBox.Items.Clear();
                FileVersionToolStripComboBox.Items.Add(Path.GetFileName("Текущая"));
                FileVersionToolStripComboBox.SelectedItem = FileVersionToolStripComboBox.Items[0];
            }
        }

        private void FontOfChoisenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                miniFontSettings = new MiniFontSettingsForm();
                miniFontSettings.Show();
            }
            catch
            {
                MessageBox.Show("Невозможно открыть новое окно!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoCopyOfAllFiels()
        {
            try
            {
                string path = @"./AutoSaveFolder";
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }

                if (MainTabControl.TabPages.Count != 0)
                {
                    for (int i = 0; i < MainTabControl.TabPages.Count; i++)
                    {
                        TabPage tabPage = MainTabControl.TabPages[i];
                        if (fileList[tabPage.TabIndex] != "-")
                        {
                            string destPath = $"{path}/{tabPage.TabIndex}";
                            if (!Directory.Exists(destPath))
                            {
                                DirectoryInfo di = Directory.CreateDirectory(destPath);
                                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                            }
                            string[] allFiles = Directory.GetFiles(destPath);
                            string filePathSave = $"{destPath}/" +
                                                  $"(Время {DateTime.Now.ToString("HH-mm-ss")})" +
                                                  $" {Path.GetFileName(fileList[tabPage.TabIndex])}";
                            if (allFiles.Length <= 10)
                            {
                                using (StreamWriter streamWriter = new StreamWriter(filePathSave))
                                {
                                    RichTextBox richTextBox = tabPage.Controls[0] as RichTextBox;
                                    if (Path.GetExtension(fileList[tabPage.TabIndex]) == ".rtf")
                                    {
                                        if (richTextBox != null)
                                        {
                                            streamWriter.Write(richTextBox.Rtf);
                                        }
                                    }
                                    else
                                    {
                                        if (richTextBox != null)
                                        {
                                            streamWriter.Write(richTextBox.Text);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DirectoryInfo dir = new DirectoryInfo(destPath);
                                FileInfo[] files = dir.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();
                                files[^1].Delete();
                                using (StreamWriter streamWriter = new StreamWriter(filePathSave))
                                {
                                    RichTextBox richTextBox = tabPage.Controls[0] as RichTextBox;
                                    if (Path.GetExtension(fileList[tabPage.TabIndex]) == ".rtf")
                                    {
                                        if (richTextBox != null)
                                        {
                                            streamWriter.Write(richTextBox.Rtf);
                                        }
                                    }
                                    else
                                    {
                                        if (richTextBox != null)
                                        {
                                            streamWriter.Write(richTextBox.Text);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при создании резервной копии файлов!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TimerAutoSave_Tick(object sender, EventArgs e)
        {
            DoCopyOfAllFiels();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeleteAutoSaveDirectory();
        }

        private void DeleteAutoSaveDirectory()
        {
            try
            {
                string path = @"./AutoSaveFolder";
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch
            {
                MessageBox.Show("Невозможно произвести удаление автосохранений!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void JournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateJournalComboBox();
        }

        private void FileVersionToolStripComboBox_Click(object sender, EventArgs e)
        {
            CreateJournalComboBox();
        }

        private void FileVersionToolStripComboBox_DropDown(object sender, EventArgs e)
        {
            CreateJournalComboBox();
        }

        private void CreateJournalComboBox()
        {
            try
            {
                if (MainTabControl.TabPages.Count == 0)
                {
                    FileVersionToolStripComboBox.Items.Clear();
                    FileVersionToolStripComboBox.Items.Add(Path.GetFileName("Нет вкладок"));
                    FileVersionToolStripComboBox.SelectedItem = FileVersionToolStripComboBox.Items[0];
                }
                else
                {
                    string path = @"./AutoSaveFolder";
                    TabPage tabPage = MainTabControl.SelectedTab;
                    if (tabPage != null)
                    {
                        FileVersionToolStripComboBox.Items.Clear();
                        FileVersionToolStripComboBox.Items.Add(Path.GetFileName("Текущая"));
                        FileVersionToolStripComboBox.SelectedItem = FileVersionToolStripComboBox.Items[0];
                        string destPath = $"{path}/{tabPage.TabIndex}";
                        if (Directory.Exists(destPath))
                        {
                            string[] allFiles = Directory.GetFiles(destPath);
                            for (int i = 0; i < allFiles.Length; i++)
                            {
                                FileVersionToolStripComboBox.Items.Add(Path.GetFileName(allFiles[i]));
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void FileVersionToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string path = @"./AutoSaveFolder";
                TabPage tabPage = MainTabControl.SelectedTab;
                object fileName = FileVersionToolStripComboBox.SelectedItem;
                if (tabPage != null && fileName != null && fileName.ToString() != "Текущая")
                {
                    string destPath = $"{path}/{tabPage.TabIndex}";
                    if (Directory.Exists(destPath))
                    {
                        destPath += $"/{fileName}";
                        using (StreamReader streamData = new StreamReader(destPath))
                        {
                            if (CheckSizeOfFile(destPath))
                            {
                                string data = streamData.ReadToEnd();
                                if (string.IsNullOrEmpty(data))
                                {
                                    data = " ";
                                }

                                if (Path.GetExtension(destPath) == ".rtf")
                                {
                                    GetTextFromRichTextBox().Rtf = data;
                                }
                                else
                                {
                                    GetTextFromRichTextBox().Text = data;
                                }
                                fileChangeList[tabPage.TabIndex] = true;
                                if (tabPage.Text[0] != '*')
                                {
                                    tabPage.Text = '*' + tabPage.Text;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Файл автосохранения имеет размер больше 1ГБ, отказ в чтении!",
                                    "Размер файла",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Невозможно открыть файл автосохранения, возможно он уже был удалён системой", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetTextFromRichTextBox().CanUndo)
                {
                    if (GetTextFromRichTextBox().RedoActionName != "Delete")
                        GetTextFromRichTextBox().Undo();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при откате действия!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void StartCompilingToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tabPage = MainTabControl.SelectedTab;
                if (tabPage != null)
                {
                    if (Path.GetExtension(fileList[tabPage.TabIndex]) == ".cs")
                    {
                        SaveFile(tabPage);
                        string source = fileList[tabPage.TabIndex];
                        List<string> data = ReadSettingsFromFile();
                        string compil = null;
                        try
                        {
                            if (data != null)
                            {
                                compil = data[7];
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Перед компиляцией укажите путь к вашему csc.exe в настройках!", "Настройка",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (compil == null) compil = "НЕТ";
                        if (compil == "НЕТ")
                        {
                            MessageBox.Show("Перед компиляцией укажите путь к вашему csc.exe в настройках", "Настройка",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (compil[^7..] != "csc.exe")
                        {
                            MessageBox.Show("Установите правильный компилятор в настройках!\n" +
                                            "Требуется csc.exe", "Отказ",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        string command = $@"{compil} /t:exe {source}";
                        Process cmd = new Process();
                        cmd.StartInfo = new ProcessStartInfo(@"cmd.exe");
                        cmd.StartInfo.CreateNoWindow = true;
                        cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.UseShellExecute = false;
                        cmd.StartInfo.RedirectStandardOutput = true;
                        cmd.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                        cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        cmd.Start();
                        cmd.StandardInput.WriteLine(command);
                        cmd.StandardInput.WriteLine("exit");
                        StreamReader srIncoming = cmd.StandardOutput;
                        string value = srIncoming.ReadToEnd();
                        if (value.Contains("error CS"))
                        {
                            string info = null;
                            string[] datas = value.Split("\n");
                            for (int i = 0; i < datas.Length; i++)
                            {
                                if (datas[i].Contains("error CS"))
                                {
                                    info += $"{datas[i]}\n";
                                }
                            }
                            MessageBox.Show($"Ошибки:\n{info}", "Ошибка при компиляции!",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show($"Программа успешно скомпилирована.\n" +
                                            $"Исполняемый файл с именем файла можно найти в корневой папке приложения!\n", "Успех!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Невозможно скомпилировать данный файл! Он не формата *.cs", "Отказ",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Отсутсвуют вкладки!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла неизвестная ошибка при компиляции!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
