using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peergrade004
{
    public partial class FormMain : Form
    {
        private List<string> fileList = new List<string>();
        private List<bool> fileChangeList = new List<bool>();
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

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
            fileList.Add("-");
            fileChangeList.Add(false);
            richTextBox.BorderStyle = BorderStyle.None;
            richTextBox.TextChanged += IfTextChanged;
            richTextBox.MouseDown += RichTextBox_MouseDown;

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

            openFileDialogOne.FileName = "";
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
            if (openFileDialogOne.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader streamData = new StreamReader(openFileDialogOne.FileName))
                    {
                        if (CheckSizeOfFile(openFileDialogOne.FileName))
                        {
                            string data = streamData.ReadToEnd();
                            if (string.IsNullOrEmpty(data))
                            {
                                data = " ";
                            }
                            if (Path.GetExtension(openFileDialogOne.FileName) == ".rtf")
                            {
                                GetTextFromRichTextBox().Rtf = data;
                            }
                            else
                            {
                                GetTextFromRichTextBox().Text = data;
                            }
                            TabPage tabPage = MainTabControl.SelectedTab;
                            tabPage.Text = Path.GetFileName(openFileDialogOne.FileName);
                            fileList[tabPage.TabIndex] = openFileDialogOne.FileName;
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
                            saveFileDialogOne.FileName = "";
                            if (saveFileDialogOne.ShowDialog() == DialogResult.OK)
                            {
                                fileList[tabPage.TabIndex] = saveFileDialogOne.FileName;
                                tabPage.Text = Path.GetFileName(fileList[tabPage.TabIndex]);
                            }
                            else
                            {
                                return;
                            }
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
                    saveFileDialogOne.FileName = Path.GetFileName(fileList[tabPage.TabIndex]);
                    if (saveFileDialogOne.ShowDialog() == DialogResult.OK)
                    {
                        fileList[tabPage.TabIndex] = saveFileDialogOne.FileName;
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
                        if (fileList[tabPage.TabIndex] == "-" && !isThisAutomaticSave)
                        {
                            MessageBox.Show($"Сохранение Безымянного файла\n" +
                                            $"Если вы закройте окно сохранения файла, он не будет сохранён!", "Cохранение");
                            SaveFile(tabPage);
                        }
                        else if (fileList[tabPage.TabIndex] == "-" && isThisAutomaticSave)
                        {

                        }
                        else
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
                }
                else if (userAnswer == DialogResult.No)
                {

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
            Font oldFont;
            Font newFont;                                                           

            oldFont = GetTextFromRichTextBox().SelectionFont;
            if (oldFont.Italic == true)
            {
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);
                this.ItalicsToolStripMenuItem.Checked = false;
            }
            else
            {
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);
                this.ItalicsToolStripMenuItem.Checked = true;
            }

            GetTextFromRichTextBox().SelectionFont = newFont;    
            GetTextFromRichTextBox().Focus();
        }

        private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;

            oldFont = GetTextFromRichTextBox().SelectionFont;
            if (oldFont.Bold == true)
            {
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);
                this.BoldToolStripMenuItem.Checked = false;
            }
            else
            {
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);
                this.BoldToolStripMenuItem.Checked = true;
            }

            GetTextFromRichTextBox().SelectionFont = newFont;
            GetTextFromRichTextBox().Focus();
        }

        private void UnderlinedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;
            oldFont = GetTextFromRichTextBox().SelectionFont;
            if (oldFont.Underline == true)
            {
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);
                this.UnderlinedToolStripMenuItem.Checked = false;
            }
            else
            {
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);
                this.UnderlinedToolStripMenuItem.Checked = true;
            }

            GetTextFromRichTextBox().SelectionFont = newFont;
            GetTextFromRichTextBox().Focus();
        }

        private void StrikethroughToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;

            oldFont = GetTextFromRichTextBox().SelectionFont;
            if (oldFont.Strikeout == true)
            {
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Strikeout);
                this.StrikethroughToolStripMenuItem.Checked = false;
            }
            else
            {
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Strikeout);
                this.StrikethroughToolStripMenuItem.Checked = true;
            }

            GetTextFromRichTextBox().SelectionFont = newFont;
            GetTextFromRichTextBox().Focus();
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
                    contextMenuStrip1.Show(this, new Point(e.X, e.Y));
                }
                    break;
            }
        }

        private void GetAllTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextFromRichTextBox().SelectAll();
        }
    }
}
