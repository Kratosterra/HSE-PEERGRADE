using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ReadAllOldTabs();
            ExecuteSetings();
        }

        private void NewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTab(out var tabPage, out var richTextBox);
        }

        private void CreateNewTab(out TabPage tabPage, out RichTextBox richTextBox)
        {
            tabPage = new TabPage("Безымянный");
            richTextBox = new RichTextBox();
            richTextBox.Dock = DockStyle.Fill;
            tabPage.Controls.Add(richTextBox);
            MainTabControl.TabPages.Add(tabPage);
            tabPage.Click += FormMain_Activated;
            fileList.Add("-");
            fileChangeList.Add(false);
            richTextBox.Font = new Font("Times New Roman",12);
            richTextBox.BorderStyle = BorderStyle.None;
            richTextBox.TextChanged += IfTextChanged;
            richTextBox.MouseDown += RichTextBox_MouseDown;
            richTextBox.Click += FormMain_Activated;

        }

        private void IfTextChanged(object sender, EventArgs e)
        {
            TabPage tabPage = MainTabControl.SelectedTab;
            if (fileChangeList[tabPage.TabIndex] == false)
            {
                fileChangeList[tabPage.TabIndex] = true;
                tabPage.Text = $"*{tabPage.Text}";
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
            GetTextFromRichTextBox().Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextFromRichTextBox().Copy();
        }

        private void InsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextFromRichTextBox().Paste();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextFromRichTextBox().Clear();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
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
                    MessageBox.Show($"{exception.Message} Невозможно открыть файл!" +
                                    " Вероятно он занят другим процессом, или доступ к нему запрещён!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static bool CheckSizeOfFile(string path)
        {
            long length = new FileInfo(path).Length;
            if (length > 1073741824)
            {
                return false;
            }

            return true;
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
                        else if(fileList[tabPage.TabIndex] != "-" && fileChangeList[tabPage.TabIndex] == true)
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
            FormMain form = new FormMain();
            form.Show();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextFromRichTextBox().Redo();
        }

        private void RichTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    ContextMenuStrip.Show(this, new Point(e.X, e.Y));
                    break;
                }
            }
        }

        private void GetAllTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextFromRichTextBox().SelectAll();
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
            catch (Exception ex)
            {
                MessageBox.Show($"Вкладки не будут автоматически открыты при следующем запуске", "Ошибка",
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
            FormSettings formSettings = new FormSettings();
            formSettings.Show();
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontSettings = new FontSettings();
            fontSettings.Show();
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            try
            {
                ExecuteSetings();
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
            LableNotification.Visible = true;
            SaveAllTabs(true);
            LableNotification.Visible = false;
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

        private void ExecuteSetings()
        {
            List<string> data = ReadSettingsFromFile();
            if (data != null && data.Count > 1)
            {
                SetSettingsFormMain(data);
            }
        }

        private void SetSettingsFormMain(List<string> data)
        {
            try
            {
                TimerSave.Interval = Int32.Parse(data[0]);
                SetThemeFormMain(data[1]);
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
        }

        private void SetBlackThemeFormMain()
        {
            MenuStrip.BackColor = Color.SlateGray;
            ToolStrip.BackColor = Color.SlateGray;
        }

        private void SetPurpleThemeFormMain()
        {
            MenuStrip.BackColor = Color.Purple; 
            ToolStrip.BackColor = Color.Purple; 
        }
       
        private void SetTealThemeFormMain()
        {
            MenuStrip.BackColor = Color.Aquamarine;
            ToolStrip.BackColor = Color.Aquamarine;
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

        private void FontOfChoisenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miniFontSettings = new MiniFontSettingsForm();
            miniFontSettings.Show();
        }
    }
}
