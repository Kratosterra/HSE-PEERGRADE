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
    /// <summary>
    /// Класс, содержащий события и методы для реализации событий основной формы Notepad+.
    /// </summary>
    public partial class FormMain : Form
    {
        // Создание списка для хранения путей до открытых и сохранёных файлов.
        private List<string> s_fileList = new List<string>();
        // Создание списка для хранения информации о статусе файла.
        private List<bool> s_fileChangeList = new List<bool>();

        // Создание обьекта формы настроек шрифта.
        private FontSettings s_fontSettings;
        // Создание обьекта уменьшенной формы настроек шрифта.
        private MiniFontSettingsForm s_miniFontSettings;

        // Создание пустого контейнера для начального меню.
        private PictureBox s_startPictureBox = new PictureBox();

        // Публичное поле, содержащее текущий размер шрифта.
        public int FontSize = 14;
        // Публичное поле, содержащее текущий вид шрифта.
        public FontFamily FontFaсe = new Font("Times New Roman", 14).FontFamily;
        // Публичное поле, содержащее данные о том, основная ли это форма.
        public bool IsFormMain = true;

        /// <summary>
        /// Конструктор основной формы.
        /// </summary>
        public FormMain()
        {
            // Инициализируем стартовые обьекты.
            InitializeComponent();
        }

        /// <summary>
        /// Событие, вызываемое при загрузке основной формы. Содержит вызовы необходимых методов.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Удаляем папку автосохранений, если она существует.
            DeleteAutoSaveDirectory(true);
            // Применяем текущие настройки приложения.
            ExecuteSettings();
            // Создаём стартовое меню.
            CreateStartMenu();
            // Блокируем и прячем ненужные элементы интерфейса.
            ControlTheMenuActivation(false);
            // Открываем все файлы, которые были о открыты в прошлой сессии.
            ReadAllOldTabs();
        }

        /// <summary>
        /// Метод, создающий главное меню приложения.
        /// </summary>
        private void CreateStartMenu()
        {
            // Настраиваем и размещаем контейнер для картинки на форме.
            s_startPictureBox.Location = new Point(400, 54);
            s_startPictureBox.Size = new Size(600, 548);
            s_startPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(s_startPictureBox);

            // Cоздаём импровизированную кнопку создания нового файла.
            PictureBox CreatePictureBox = new PictureBox();
            CreatePictureBox.Location = new Point(10, 350);
            CreatePictureBox.Size = new Size(300, 100);
            CreatePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            CreatePictureBox.Image = Properties.Resources.CREATE;
            CreatePictureBox.Click += NewTabToolStripMenuItem_Click;
            this.Controls.Add(CreatePictureBox);

            // Cоздаём импровизированную кнопку открытия нового файла.
            PictureBox OpenPictureBox = new PictureBox();
            OpenPictureBox.Location = new Point(10, 440);
            OpenPictureBox.Size = new Size(307, 70);
            OpenPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            OpenPictureBox.Image = Properties.Resources.OPEN;
            OpenPictureBox.Click += OpenToolStripMenuItem_Click;
            this.Controls.Add(OpenPictureBox);

            // Создаём контейнер для названия приложения.
            PictureBox ThemePictureBox = new PictureBox();
            ThemePictureBox.Location = new Point(0, 250);
            ThemePictureBox.Size = new Size(380, 100);
            ThemePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            ThemePictureBox.Image = Properties.Resources.NOTEPAD_;
            this.Controls.Add(ThemePictureBox);
        }

        /// <summary>
        /// Метод активирующий\деактивирующий некторые элементы интерфейса.
        /// </summary>
        /// <param name="isObjectEnable">Параметр, содежащий информацию о том, нужно ли деактивировать или
        /// ативировать элементы интерфейса.</param>
        private void ControlTheMenuActivation(bool isObjectEnable)
        {
            EditToolStripMenuItem.Enabled = isObjectEnable;
            EditToolStripMenuItem.Visible = isObjectEnable;
            FormatToolStripMenuItem.Enabled = isObjectEnable;
            FormatToolStripMenuItem.Visible = isObjectEnable;
            JournalToolStripMenuItem.Enabled = isObjectEnable;
            JournalToolStripMenuItem.Visible = isObjectEnable;
            CompilThisToolStripMenuItem.Enabled = isObjectEnable;
            CompilThisToolStripMenuItem.Visible = isObjectEnable;
            SaveToolStripMenuItem.Enabled = isObjectEnable;
            SaveToolStripMenuItem.Visible = isObjectEnable;
            SaveAsToolStripMenuItem.Enabled = isObjectEnable;
            SaveAsToolStripMenuItem.Visible = isObjectEnable;
            SaveAllToolStripMenuItem.Enabled = isObjectEnable;
            SaveAllToolStripMenuItem.Visible = isObjectEnable;
            SaveToolStripButton.Enabled = isObjectEnable;
            SaveToolStripButton.Visible = isObjectEnable;
            ItalicToolStripButton.Enabled = isObjectEnable;
            ItalicToolStripButton.Visible = isObjectEnable;
            BoldToolStripButton.Enabled = isObjectEnable;
            BoldToolStripButton.Visible = isObjectEnable;
            UnderlineToolStripButton.Enabled = isObjectEnable;
            UnderlineToolStripButton.Visible = isObjectEnable;
            StrikeToolStripButton.Enabled = isObjectEnable;
            StrikeToolStripButton.Visible = isObjectEnable;
            StartCompilingToolStripButton.Enabled = isObjectEnable;
            StartCompilingToolStripButton.Visible = isObjectEnable;
            CloseTabToolStripButton.Enabled = isObjectEnable;
            CloseTabToolStripButton.Visible = isObjectEnable;
            NewTabToolStripButton.Visible = isObjectEnable;
            SettingsToolStripButton.Visible = isObjectEnable;
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку создания новой вкладки.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void NewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создаём новую вкладку.
            CreateNewTab(out var tabPage, out var richTextBox);
        }

        /// <summary>
        /// Метод, содающий новую вкладку и размещающий на ней окно для ввода текста.
        /// </summary>
        /// <param name="tabPage">Содержит вкладку приложения.</param>
        /// <param name="richTextBox">Содержит RichTextBox, который размещён на вкладке.</param>
        private void CreateNewTab(out TabPage tabPage, out RichTextBox richTextBox)
        {
            try
            {
                if (MainTabControl.TabPages.Count == 10)
                {
                    MessageBox.Show("Максимум вкладок - 10 штук.", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabPage = null;
                    richTextBox = null;
                }
                else
                {
                    TryToCreateNewTab(out tabPage, out richTextBox);
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при создании новой вкладки!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabPage = null;
                richTextBox = null;
            }
        }

        /// <summary>
        /// Метод, производящий попытку содать новую вкладку и разместить на ней окно для ввода текста.
        /// </summary>
        /// <param name="tabPage">Содержит вкладку приложения.</param>
        /// <param name="richTextBox">Содержит RichTextBox, который размещён на вкладке.</param>
        private void TryToCreateNewTab(out TabPage tabPage, out RichTextBox richTextBox)
        {
            // Активируем все элементы интерфейса.
            ControlTheMenuActivation(true);
            tabPage = new TabPage("Безымянный");
            richTextBox = new RichTextBox();
            richTextBox.Dock = DockStyle.Fill;
            tabPage.Controls.Add(richTextBox);
            MainTabControl.TabPages.Add(tabPage);
            tabPage.Click += FormMain_Activated;
            // Добавляем пустую информацию для имени файла.
            s_fileList.Add("-");
            // Утверждаем, что файл не был изменён.
            s_fileChangeList.Add(false);
            // Устанавливаем шрифт и стиль края.
            richTextBox.Font = new Font("Times New Roman", 12);
            richTextBox.BorderStyle = BorderStyle.None;
            // Подписываемся на необходимые события
            richTextBox.TextChanged += IfTextChanged;
            richTextBox.MouseDown += RichTextBox_MouseDown;
            richTextBox.Click += FormMain_Activated;
            // Включаем видимость главного окна с вкладками.
            MainTabControl.Visible = true;
        }

        /// <summary>
        /// Метод, возвращающий обьект RichTextBox, открытый на экране, а если его нет - создаёт его.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает текущий открытый RichTectBox.</item>
        /// </list>
        /// </returns>
        private RichTextBox GetSelectedRichTextBox()
        {

            RichTextBox richTextBox = null;
            // Получаем информацию о текущей открытой вкладке.
            TabPage tabPage = MainTabControl.SelectedTab;
            // Если открытая вкладка существует - получаем из неё RichTextBox и возвращаем его.
            // Если её нет - создаём вкладку вместе с пустым RichTextBox.
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

        /// <summary>
        /// Событиие, вызываемое при изменении текста в RichTextBox, меняющее статус файла и название.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void IfTextChanged(object sender, EventArgs e)
        {
            // Получаем текущую открытую вкладку
            TabPage tabPage = MainTabControl.SelectedTab;
            if (tabPage != null)
            {
                try
                {
                    // Если файл находится в статусе "не изменён", переводим его в статус "изменён" и меняем название.
                    if (s_fileChangeList[tabPage.TabIndex] == false)
                    {
                        s_fileChangeList[tabPage.TabIndex] = true;
                        tabPage.Text = $"*{tabPage.Text}";
                    }
                }
                catch
                {
                    //Ничего не делаем.
                }
            }
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку "вырезать", вырезает выбранный текст.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetSelectedRichTextBox().Cut();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при вырезании текста!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку "копировать", копирует выбранный текст.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetSelectedRichTextBox().Copy();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при копировании текста!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку "вставить", вставляет текст и буфера обмена.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void InsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetSelectedRichTextBox().Paste();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при вставке текста!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку "удалить всё", удаляет весь текст.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetSelectedRichTextBox().Clear();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при удалении всего текста!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, вызываемое при попытке открытия нового файла.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialogOne.FileName = "";
                // Если в вкладке есть текст, то мы спрашиваем, следует ли открыть файл в данной вкладке,
                // иначе просто открываем новый файл.
                if (GetSelectedRichTextBox().Text != "")
                {
                    DialogResult userAnswer = MessageBox.Show("Хотите открыть файл в этой вкладке?\n" +
                                                              "Все несохранённые данные будут потеряны!",
                        "Открытие файла",
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
                MessageBox.Show("Возникла ошибка при при открытии файла, вероятно она связана с вашей" +
                                " системой!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод, производящий открытие нового файла.
        /// </summary>
        private void OpenNewFile()
        {
            // Если диалог с получением файла для открытия был успешен - пытаемся прочитать файл.
            if (OpenFileDialogOne.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Пытаемся окрыть файл.
                    TryToOpenFile();
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
                catch
                {
                    MessageBox.Show($"Невозможно открыть файл!" +
                                    " Вероятно он занят другим процессом, или доступ к нему запрещён!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Метод, производящий попытку чтения файла по данным, полученным от пользователя.
        /// </summary>
        private void TryToOpenFile()
        {
            using (StreamReader streamData = new StreamReader(OpenFileDialogOne.FileName))
            {
                // Производим проверку размера файла.
                if (CheckSizeOfFile(OpenFileDialogOne.FileName))
                {
                    // Считываем информацию из файла.
                    string data = streamData.ReadToEnd();
                    if (string.IsNullOrEmpty(data))
                    {
                        data = " ";
                    }
                    // В зависимости от формата файла загружаем разные данные в текущий RichTextBox.
                    if (Path.GetExtension(OpenFileDialogOne.FileName) == ".rtf")
                    {
                        GetSelectedRichTextBox().Rtf = data;
                    }
                    else
                    {
                        GetSelectedRichTextBox().Text = data;
                    }
                    TabPage tabPage = MainTabControl.SelectedTab;
                    tabPage.Text = Path.GetFileName(OpenFileDialogOne.FileName);
                    // Записываем данные о пути к файлу.
                    s_fileList[tabPage.TabIndex] = OpenFileDialogOne.FileName;
                    // Утверждаем, что файл не изменён.
                    s_fileChangeList[tabPage.TabIndex] = false;
                    // Удаляем текущие автоматические сохранения для вкладки, если они существуют.
                    DeleteAutoSaveFileInformation(tabPage);
                }
                else
                {
                    MessageBox.Show($"Файл имеет размер больше 10 мб, отказ в чтении!",
                        "Размер открываемого файла",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Метод, производящий проверку размера файла.
        /// </summary>
        /// <param name="path">Путь до файла.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее подходит ли файл по размеру или нет.</item>
        /// </list>
        /// </returns>
        private static bool CheckSizeOfFile(string path)
        {
            try
            {
                // Проверяем размер файла.
                long length = new FileInfo(path).Length;
                if (length > 10485760)
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

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку сохранения.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получаем текущую открытую вкладку.
            TabPage tabPage = MainTabControl.SelectedTab;
            // Сохраняяем информацию.
            SaveFile(tabPage);
        }

        /// <summary>
        /// Метод, производящий сохранение файла, находящегося во вкладке.
        /// </summary>
        /// <param name="tabPage">Обьект, содержащий вкладку программы.</param>
        private void SaveFile(TabPage tabPage)
        {
            // Производим сохранение только тогда, когда вкладка существует.
            if (tabPage != null)
            {
                try
                {
                    // Производим попытку сохранения файла.
                    if (TryToSaveFile(tabPage)) return;
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

        /// <summary>
        /// Метод, производящий попытку сохранения инофрмации из указаной вкладки.
        /// </summary>
        /// <param name="tabPage">Обьект, содержащий вкладку приложения.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение bool, указывающее на статус попытки сохранения.</item>
        /// </list>
        /// </returns>
        private bool TryToSaveFile(TabPage tabPage)
        {
            //Производим работу только тогда, когда файл изменён или он относится к категории Безымянных.
            if (s_fileChangeList[tabPage.TabIndex] || s_fileList[tabPage.TabIndex] == "-")
            {
                //Если файл относится к категории безымянных - просим выбрать имя и формат для сохранения.
                if (s_fileList[tabPage.TabIndex] == "-")
                {
                    SaveFileDialogOne.FileName = "";
                    if (SaveFileDialogOne.ShowDialog() == DialogResult.OK)
                    {
                        s_fileList[tabPage.TabIndex] = SaveFileDialogOne.FileName;
                        tabPage.Text = Path.GetFileName(s_fileList[tabPage.TabIndex]);
                    }
                    else
                    {
                        return true;
                    }
                }
                using (StreamWriter streamWriter = new StreamWriter(s_fileList[tabPage.TabIndex]))
                {
                    RichTextBox richTextBox = tabPage.Controls[0] as RichTextBox;
                    //Производим сохранение данных в зависимости от расширения, который задал пользователь.
                    if (Path.GetExtension(s_fileList[tabPage.TabIndex]) == ".rtf")
                    {
                        if (richTextBox != null) streamWriter.Write(richTextBox.Rtf);

                    }
                    else
                    {
                        if (richTextBox != null) streamWriter.Write(richTextBox.Text);

                    }
                }
                // Присваиваем название файла заголовку вкладки.
                tabPage.Text = Path.GetFileName(s_fileList[tabPage.TabIndex]);
                // Утверждаем, что файл относится к категории "не изменён".
                s_fileChangeList[tabPage.TabIndex] = false;
            }
            return false;
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку "сохранить как".
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получаем текущую открытую вкладку.
            TabPage tabPage = MainTabControl.SelectedTab;
            // Если вкладка существует - то производим сохранение.
            if (tabPage != null)
            {
                try
                {
                    // Производим попытку сохранения.
                    if (TryToSaveAsForFile(tabPage)) return;
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

        /// <summary>
        /// Метод, производящий попытку сохранения файла по профилю "сохранить как".
        /// </summary>
        /// <param name="tabPage">Обьект, который содержит вкладку.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение bool, содержащее информации о попытке сохранения.</item>
        /// </list>
        /// </returns>
        private bool TryToSaveAsForFile(TabPage tabPage)
        {
            SaveFileDialogOne.FileName = Path.GetFileName(s_fileList[tabPage.TabIndex]);
            // Запрашиваем информацию для сохранения файла у пользователя.
            if (SaveFileDialogOne.ShowDialog() == DialogResult.OK)
            {
                s_fileList[tabPage.TabIndex] = SaveFileDialogOne.FileName;
                tabPage.Text = Path.GetFileName(s_fileList[tabPage.TabIndex]);
            }
            else
            {
                return true;
            }

            // Производим сохранение информации в зависимости от указанного пользователем расширения.
            using (StreamWriter streamWriter = new StreamWriter(s_fileList[tabPage.TabIndex]))
            {
                if (Path.GetExtension(s_fileList[tabPage.TabIndex]) == ".rtf")
                {
                    streamWriter.Write(GetSelectedRichTextBox().Rtf);
                }
                else
                {
                    streamWriter.Write(GetSelectedRichTextBox().Text);
                }
            }
            // Меняем название вкладки на имя файла и утверждаем, что файл не был изменён.
            tabPage.Text = Path.GetFileName(s_fileList[tabPage.TabIndex]);
            s_fileChangeList[tabPage.TabIndex] = false;
            return false;
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку "сохранить всё".
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SaveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Cохраняем все вкладки.
            SaveAllTabs();
        }

        /// <summary>
        /// Метод, производящий сохранение всех вкладок в программе.
        /// </summary>
        /// <param name="isThisAutomaticSave">Параметр bool, указывающий на то, является ли сохранение
        /// автомтическим.</param>
        private void SaveAllTabs(bool isThisAutomaticSave = false)
        {
            try
            {
                // Производим попытку сохранения всех вкладок.
                TryToSaveAllTabs(isThisAutomaticSave);
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

        /// <summary>
        /// Метод, производящий попытку сохранения всех вкладок в программе.
        /// </summary>
        /// <param name="isThisAutomaticSave">Параметр bool, указывающий на то, является ли сохранение
        /// автоматическим.</param>
        private void TryToSaveAllTabs(bool isThisAutomaticSave)
        {
            //Производим сохранение только тогда, когда у нас существуют вкладки.
            if (MainTabControl.TabCount == 0 && !isThisAutomaticSave)
            {
                MessageBox.Show("Сохранять нечего! " +
                                "Не открыто и не создано ни одного файла!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Проходимся по всем вкладкам, сохраняя данные
                for (int i = 0; i < MainTabControl.TabCount; i++)
                {
                    TabPage tabPage = MainTabControl.TabPages[i];
                    // Если файл безымянен и это не автоматическое сохранение - запрашиваем данные для сохранения.
                    // Если же файл имеет данные о пути до него и файл является измененым - сохраняем его.
                    if (s_fileList[tabPage.TabIndex] == "-" && s_fileChangeList[tabPage.TabIndex]
                                                          && !isThisAutomaticSave)
                    {
                        MessageBox.Show($"Сохранение Безымянного файла\n" +
                                        $"Если вы закройте окно сохранения файла, он не будет сохранён!",
                            "Cохранение");
                        SaveFile(tabPage);
                    }
                    else if (s_fileList[tabPage.TabIndex] != "-" && s_fileChangeList[tabPage.TabIndex] == true)
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

        /// <summary>
        /// Событие, вызываемое при закрытии формы.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Производим попытку завершения работы.
                TryToExecuteFormClosing(e);
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при завершении работы программы, сохранение данных не производится!",
                    "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод производящий попытку завершения приложения.
        /// </summary>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void TryToExecuteFormClosing(FormClosingEventArgs e)
        {
            // Если в программе есть измененые файлы - спрашиваем пользователя, хочет ли он их сохранить.
            // Иначе спрашиваем, хочет ли пользователь выйти.
            if (s_fileChangeList.Contains(true))
            {
                DialogResult userAnswer = MessageBox.Show("Хотите сохранить несохранёные файлы?", "Выход",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (userAnswer == DialogResult.Yes)
                {
                    // Сохраняем все вкладки и сохраняем вкладки для будущего открытия.
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

        /// <summary>
        /// Событие, ответсвенное за закрытие приложения.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void CloseAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Инициализируем закрытие приложения.
            Application.Exit();
        }

        /// <summary>
        /// Событие, применяющее новый тип шрифта Italics.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void ItalicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Font oldFont;
                Font newFont;

                oldFont = GetSelectedRichTextBox().SelectionFont;
                if (oldFont != null && oldFont.Italic == true)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);
                    GetSelectedRichTextBox().SelectionFont = newFont;
                    GetSelectedRichTextBox().Focus();
                }
                else if (oldFont != null && oldFont.Italic == false)
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);
                    GetSelectedRichTextBox().SelectionFont = newFont;
                    GetSelectedRichTextBox().Focus();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно применить шрифт!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Событие, применяющее новый тип шрифта Bold.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Font oldFont;
                Font newFont;

                oldFont = GetSelectedRichTextBox().SelectionFont;
                if (oldFont != null && oldFont.Bold == true)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);
                    GetSelectedRichTextBox().SelectionFont = newFont;
                    GetSelectedRichTextBox().Focus();
                }
                else if (oldFont != null && oldFont.Bold == false)
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);
                    GetSelectedRichTextBox().SelectionFont = newFont;
                    GetSelectedRichTextBox().Focus();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно применить шрифт!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Событие, применяющее новый тип шрифта Underlined.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void UnderlinedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Font oldFont;
                Font newFont;
                oldFont = GetSelectedRichTextBox().SelectionFont;
                if (oldFont != null && oldFont.Underline == true)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);
                    GetSelectedRichTextBox().SelectionFont = newFont;
                    GetSelectedRichTextBox().Focus();
                }
                else if (oldFont != null && oldFont.Underline == false)
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);
                    GetSelectedRichTextBox().SelectionFont = newFont;
                    GetSelectedRichTextBox().Focus();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно применить шрифт!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Событие, применяющее новый тип шрифта Strikeout.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void StrikeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                Font oldFont;
                Font newFont;

                oldFont = GetSelectedRichTextBox().SelectionFont;
                if (oldFont != null && oldFont.Strikeout == true)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Strikeout);
                    GetSelectedRichTextBox().SelectionFont = newFont;
                    GetSelectedRichTextBox().Focus();
                }
                else if (oldFont != null && oldFont.Strikeout == false)
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Strikeout);
                    GetSelectedRichTextBox().SelectionFont = newFont;
                    GetSelectedRichTextBox().Focus();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно применить шрифт!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cобытие, вызываемое при нажатии на открытие нового окна..
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Cоздаём новую форму и показываем её.
                FormMain form = new FormMain();
                form.IsFormMain = false;
                form.Show();
            }
            catch
            {
                MessageBox.Show("Невозможно открыть новое окно!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, возникающее при необходимости повторить действие.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetSelectedRichTextBox().CanRedo)
                {
                    GetSelectedRichTextBox().Redo();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при отмене действия!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, исполняемое при нажатии правой кнопкой мыши на RichTextBox.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void RichTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                switch (e.Button)
                {
                    case MouseButtons.Right:
                        {
                            // Открываем контекстное меню рядом с указателем при нажатии правой кнопки мыши.
                            ContextMainMenuStrip.Show(this, new Point(e.X, e.Y));
                            break;
                        }
                }
            }
            catch
            {
                // Не делаем ничего.
            }
        }

        /// <summary>
        /// Событие, возникающее при необходимости выделить ввесь текст.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void GetAllTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetSelectedRichTextBox().SelectAll();
            }
            catch
            {
                MessageBox.Show("Ошибка при выборе всего текста!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод, производящий сохранение открытых вкладок в виде списка путей, для открытия в следющую
        /// сессию.
        /// </summary>
        private void SaveAllOpenTabsInFile()
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter("HideInformationTabs.txt"))
                {
                    // Проходимся по всем открытым файлам и если файл не безымянен, сохраним его.
                    for (int i = 0; i < s_fileList.Count; i++)
                    {
                        if (s_fileList[i] != "-")
                        {
                            streamWriter.WriteLine(s_fileList[i]);
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

        /// <summary>
        /// Метод, производящий чтение и открытие файлов из прошлой сессии.
        /// </summary>
        private void ReadAllOldTabs()
        {
            try
            {
                // Если файл существует - получаем из него список путей.
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
                    // Добавляем требуемые вкладки.
                    AddAllOldTabs(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при автоматическом открытии старых вкаладок.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод, производящий добавление файлов, открытых в прошлой сессии.
        /// </summary>
        /// <param name="dataPath">Список путей до файлов.</param>
        private void AddAllOldTabs(List<string> dataPath)
        {
            try
            {
                for (int i = 0; i < dataPath.Count; i++)
                {
                    //Производим попытку добавить вкладку со старым файлом.
                    TryToAddOldSavedTab(dataPath, i);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при открытии старых вкладок!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод, производящий попытку добавления файла в приложение.
        /// </summary>
        /// <param name="dataPath">Список старых путей из прошлой сессии.</param
        /// <param name="i">Текущая итерация цикла.</param>
        private void TryToAddOldSavedTab(List<string> dataPath, int i)
        {
            try
            {
                // Производим действия, только если файл существует на данный момент.
                if (File.Exists(dataPath[i]))
                {
                    using (StreamReader streamData = new StreamReader(dataPath[i]))
                    {
                        // Открываем файл только если он имеет подходящий размер.
                        if (CheckSizeOfFile(dataPath[i]))
                        {
                            // Cоздаём новую вкладку и записываем в неё все необходимые данные в зависимости
                            // от расширения.
                            string data = streamData.ReadToEnd();
                            if (string.IsNullOrEmpty(data)) data = " ";
                            CreateNewTab(out TabPage tabPage, out RichTextBox richTextBox);
                            if (Path.GetExtension(dataPath[i]) == ".rtf") richTextBox.Rtf = data;
                            else richTextBox.Text = data;
                            tabPage.Text = Path.GetFileName(dataPath[i]);
                            s_fileList[tabPage.TabIndex] = dataPath[i];
                            s_fileChangeList[tabPage.TabIndex] = false;
                        }
                    }
                }
            }
            catch
            {
                // Ничего не делаем.
            }
        }

        /// <summary>
        /// Cобытие, вызываемое при необходимости открыть настройки.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаём новую форму с настройками и показываем её.
                FormSettings formSettings = new FormSettings();
                formSettings.Show();
            }
            catch
            {
                MessageBox.Show("Невозможно открыть новое окно!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cобытие, вызываемое при необходимости открыть настройки шрифта.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаём новую форму с настройками шрифта и показываем её.
                s_fontSettings = new FontSettings();
                s_fontSettings.Show();
            }
            catch
            {
                MessageBox.Show("Невозможно открыть новое окно!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, вызываемое при переключении фокуса на основную форму.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FormMain_Activated(object sender, EventArgs e)
        {
            try
            {
                // Исполняем настройки.
                ExecuteSettings();
                // Если таковы настройки - применяем новый шрифт.
                if (s_miniFontSettings != null && s_miniFontSettings.FontSize != 0 && s_miniFontSettings.FontFaсe != null)
                {
                    GetSelectedRichTextBox().SelectionFont = new Font(s_miniFontSettings.FontFaсe, s_miniFontSettings.FontSize);
                    GetSelectedRichTextBox().Focus();
                    s_miniFontSettings.FontFaсe = null;
                    s_miniFontSettings.FontSize = 0;
                }
                if (s_fontSettings != null && s_fontSettings.FontFaсe != null && s_fontSettings.FontSize != 0)
                {
                    FontSize = s_fontSettings.FontSize;
                    FontFaсe = s_fontSettings.FontFaсe;
                    GetSelectedRichTextBox().Font = new Font(FontFaсe, FontSize);
                    s_fontSettings.FontSize = 0;
                    s_fontSettings.FontFaсe = null;
                }
            }
            catch
            {
                // Не делаем ничего.
            }
        }

        /// <summary>
        /// Событие вызываемое через равные, установленные  промежутки времени, чтобы производить автоматическое
        /// сохранение файлов.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void TimerSave_Tick(object sender, EventArgs e)
        {
            try
            {
                LableNotification.Visible = true;
                // Производим автоматическое сохранение всех открытых и созданных файлов. 
                SaveAllTabs(true);
                LableNotification.Visible = false;
            }
            catch
            {
                // Не делаем ничего.
            }
        }

        /// <summary>
        /// Метод, считывающий настройки из файла настроек.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает список со строками настроек.</item>
        /// </list>
        /// </returns>
        public List<string> ReadSettingsFromFile()
        {
            try
            {
                if (File.Exists("Settings.txt"))
                {
                    List<string> data = new List<string>();
                    // Считываем настройки из файла.
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

        /// <summary>
        /// Метод исполняющий настроки.
        /// </summary>
        private void ExecuteSettings()
        {
            try
            {
                List<string> data = ReadSettingsFromFile();
                if (data == null)
                {
                    // Если настроек нет, то устанавливаем стандартные настройки.
                    this.BackColor = Color.WhiteSmoke;
                    s_startPictureBox.Image = Properties.Resources.White;
                }
                if (data != null && data.Count > 1)
                {
                    // Иначе устанавливаем настройки.
                    SetSettingsFormMain(data);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при исполнении настроек!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Метод, устанавливающий настройки для формы.
        /// </summary>
        /// <param name="data">Список настроек.</param>
        private void SetSettingsFormMain(List<string> data)
        {
            try
            {
                // Устанавливаем значения таймера сохранений всех файлов.
                TimerSave.Interval = Int32.Parse(data[0]);
                // Устанавливаем тему приложения.
                SetThemeFormMain(data[1]);
                // Устанавливаем значение таймера автосохранений файла.
                TimerAutoSave.Interval = Int32.Parse(data[2]);
            }
            catch
            {
                MessageBox.Show($"Произошла ошибка при применении настроек.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Метод, устанавливающий тему приложения, получая её название.
        /// </summary>
        /// <param name="name">Назавание темы.</param>
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

        /// <summary>
        /// Метод, устанавливающий белую тему.
        /// </summary>
        private void SetWhiteThemeFormMain()
        {
            MenuStrip.BackColor = Color.WhiteSmoke;
            ToolStrip.BackColor = Color.WhiteSmoke;
            s_startPictureBox.Image = Properties.Resources.White;
            this.BackColor = Color.WhiteSmoke;
        }

        /// <summary>
        /// Метод, устанавливающий темную тему.
        /// </summary>
        private void SetBlackThemeFormMain()
        {
            MenuStrip.BackColor = Color.SlateGray;
            ToolStrip.BackColor = Color.SlateGray;
            s_startPictureBox.Image = Properties.Resources.Dark;
            this.BackColor = Color.SlateGray;
        }

        /// <summary>
        /// Метод, устанавливающий фиолетовую тему.
        /// </summary>
        private void SetPurpleThemeFormMain()
        {
            MenuStrip.BackColor = Color.Purple;
            ToolStrip.BackColor = Color.Purple;
            s_startPictureBox.Image = Properties.Resources.Purple;
            this.BackColor = Color.Purple;
        }

        /// <summary>
        /// Метод, устанавливающий мятную/бирюзовую тему.
        /// </summary>
        private void SetTealThemeFormMain()
        {
            MenuStrip.BackColor = Color.Aquamarine;
            ToolStrip.BackColor = Color.Aquamarine;
            s_startPictureBox.Image = Properties.Resources.Teal;
            this.BackColor = Color.Aquamarine;
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку закрытия вкладки.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void CloseTabToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Производим попытку закрыть текущую вкладку.
                TryToCloseTab();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка во время удаления вкладки!" +
                                "", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Метод, производящий попытку закрытия вкладки.
        /// </summary>
        private void TryToCloseTab()
        {
            if (MainTabControl.TabCount == 0)
            {
                MessageBox.Show("Удалять нечего, не открыто ни одной вкладки!" +
                                "", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Требуем от пользователя подтвердить действие.
                DialogResult userAnswer = MessageBox.Show("Вы действительно хотите закрыть вкладку?\n" +
                                                          "Все несохранённые данные будут потеряны.",
                    "Закрытие вкладки",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Если ответ положительный - производим процедуру удаления.
                if (userAnswer == DialogResult.Yes)
                {
                    TabPage tabPage = MainTabControl.SelectedTab;
                    // Задаём стандартные значения информационной системы.
                    s_fileList[tabPage.TabIndex] = "-";
                    s_fileChangeList[tabPage.TabIndex] = false;
                    // Удаляем автосохранения для данной вкладки
                    DeleteAutoSaveFileInformation(tabPage);
                    // Удаляем вкладку.
                    MainTabControl.TabPages.Remove(tabPage);
                    // Если вкладок больше нет, то показываем главное меню и деактивируем некторые элементы интерфеса.
                    if (MainTabControl.TabPages.Count == 0)
                    {
                        MainTabControl.Visible = false;
                        ControlTheMenuActivation(false);
                    }
                }
            }
        }

        /// <summary>
        /// Метод, производящий удаление папки с автосохранениями для текущей вкладки.
        /// </summary>
        /// <param name="tabPage">Обьект, содержащий текущую вкладку.</param>
        private void DeleteAutoSaveFileInformation(TabPage tabPage)
        {
            try
            {
                // Производим удаление папки.
                string path = $@"./AutoSaveFolder{this.GetHashCode()}/{tabPage.TabIndex}";
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch
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

        /// <summary>
        /// Событие, вызываемое при необходимости изменить шрифт выделенной области.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FontOfChoisenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаём новую форму уменьшенных настроек шрифта и показываем её.
                s_miniFontSettings = new MiniFontSettingsForm();
                s_miniFontSettings.Show();
            }
            catch
            {
                MessageBox.Show("Невозможно открыть новое окно!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод, создающий автосохранение всех файлов.
        /// </summary>
        private void DoCopyOfAllFiles()
        {
            try
            {
                string path = $@"./AutoSaveFolder{this.GetHashCode()}";
                // Если директории автосохранений нет, то создаём её.
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                // Если существуют вкладки - пытаемся произвести автосохранение каждого файла.
                if (MainTabControl.TabPages.Count != 0)
                {
                    for (int i = 0; i < MainTabControl.TabPages.Count; i++)
                    {
                        TryToDoCopyOfFileInTab(i, path);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при создании резервной копии файлов!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Метод, производящий попытку автосохранения файла.
        /// </summary>
        /// <param name="i">Номер итерации цикла.</param>
        /// <param name="path">Путь до папки автосохранения.</param>
        private void TryToDoCopyOfFileInTab(int i, string path)
        {
            TabPage tabPage = MainTabControl.TabPages[i];
            // Поизводим автосохранение только если файл не принадлежит категории Безымянных.
            if (s_fileList[tabPage.TabIndex] != "-")
            {
                string destPath = $"{path}/{tabPage.TabIndex}";
                // Проверяем, существует ли локальная папка автосохранений для текущей вкладки, если нет - создаём её.
                if (!Directory.Exists(destPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(destPath);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                // Получаем все файлы в директории.
                string[] allFiles = Directory.GetFiles(destPath);
                // Cоздаём оригинальное название для файла.
                string filePathSave = $"{destPath}/" +
                                      $"(Время {DateTime.Now.ToString("HH-mm-ss")})" +
                                      $" {Path.GetFileName(s_fileList[tabPage.TabIndex])}";
                if (allFiles.Length <= 10)
                {
                    // Производим сохранение файла автосохранения.
                    CreateAutoSaveInNotFullDirectory(filePathSave, tabPage);
                }
                else
                {
                    // Производим кроме сохранение еще и удаление самого старого файла автосохранения.
                    CreateAutoSaveInFullDirectory(destPath, filePathSave, tabPage);
                }
            }
        }

        /// <summary>
        /// Метод, производящий сохранение файла автосохранения и удаляющий устаревший файл автосохранения.
        /// </summary>
        /// <param name="destPath">Путь до папки автосохранения.</param>
        /// <param name="filePathSave">Путь до файла автоосохранения.</param>
        /// <param name="tabPage">Обьект, сожержащий информации о вкладке.</param>
        private void CreateAutoSaveInFullDirectory(string destPath, string filePathSave, TabPage tabPage)
        {
            // Удаляем самый старый по времени файл.
            DirectoryInfo dir = new DirectoryInfo(destPath);
            FileInfo[] files = dir.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();
            files[^1].Delete();
            // Cохраняем файл автосохранения в зависимости от текущего рассширения основного файла.
            using (StreamWriter streamWriter = new StreamWriter(filePathSave))
            {
                RichTextBox richTextBox = tabPage.Controls[0] as RichTextBox;
                if (Path.GetExtension(s_fileList[tabPage.TabIndex]) == ".rtf")
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

        /// <summary>
        /// Метод, сохраняющий файл автосохранения по указанному пути.
        /// </summary>
        /// <param name="filePathSave">Путь до файла автоосохранения.</param>
        /// <param name="tabPage">Обьект, сожержащий информации о вкладке.</param>
        private void CreateAutoSaveInNotFullDirectory(string filePathSave, TabPage tabPage)
        {
            // Cохраняем файл автосохранения в зависимости от текущего рассширения основного файла.
            using (StreamWriter streamWriter = new StreamWriter(filePathSave))
            {
                RichTextBox richTextBox = tabPage.Controls[0] as RichTextBox;
                if (Path.GetExtension(s_fileList[tabPage.TabIndex]) == ".rtf")
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

        /// <summary>
        /// Событие, возникающие через определенное время для автосохранения всех файлов.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void TimerAutoSave_Tick(object sender, EventArgs e)
        {
            // Создание автосохранения всех файлов.
            DoCopyOfAllFiles();
        }

        /// <summary>
        /// Событие, возникающие после закрытия формы.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Производим удаление папки автосохранении.
            DeleteAutoSaveDirectory();
        }

        /// <summary>
        /// Метод, удалаяющий всю папку автосохранения или все папки автосохранения.
        /// </summary>
        /// <param name="isStartDeleting">Является ли данное удаление стартовым.</param>
        private void DeleteAutoSaveDirectory(bool isStartDeleting = false)
        {
            try
            {
                // Если это основная форма и это начальное удаление, то удаляем все папки автосохранений. 
                if (isStartDeleting && IsFormMain)
                {
                    string[] data = Directory.GetDirectories("./", "AutoSaveFolder*");
                    for (int i = 0; i < data.Length; i++)
                    {
                        Directory.Delete(data[i], true);
                    }
                }
                else
                {
                    if (Directory.Exists($@"./AutoSaveFolder{this.GetHashCode()}"))
                    {
                        Directory.Delete($@"./AutoSaveFolder{this.GetHashCode()}", true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex} Невозможно произвести удаление автосохранений!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, вызываемое при клике на журнал автосохранений.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void JournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateJournalComboBox();
        }

        /// <summary>
        /// Событие, вызываемое при клике на элемент переключения версий.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileVersionToolStripComboBox_Click(object sender, EventArgs e)
        {
            CreateJournalComboBox();
        }

        /// <summary>
        /// Событие, вызываемое при открытии перключателя версий.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileVersionToolStripComboBox_DropDown(object sender, EventArgs e)
        {
            CreateJournalComboBox();
        }

        /// <summary>
        /// Метод, реализовывающий создание выпадающего меню версий.
        /// </summary>
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
                    // Добавляем в элементы выпадающего списка сохранённые в паке автосохранения файлы.
                    string path = $@"./AutoSaveFolder{this.GetHashCode()}";
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
                // Ничего не делаем.
            }
        }

        /// <summary>
        /// Событие, возникающеее при выборе определённой версии файла.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FileVersionToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Пытаемся установить другую версию файла.
                TryToLoadAndSetSelectedFileVersion();
            }
            catch
            {
                MessageBox.Show("Невозможно открыть файл автосохранения, возможно он уже был удалён системой",
                    "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод, производящий попытку установить и загрузить файл автосохранения.
        /// </summary>
        private void TryToLoadAndSetSelectedFileVersion()
        {
            string path = $@"./AutoSaveFolder{this.GetHashCode()}";
            TabPage tabPage = MainTabControl.SelectedTab;
            object fileName = FileVersionToolStripComboBox.SelectedItem;
            // Производим загрузку только тогда, когда действительно выбрана версия файла и вкладка существует.
            if (tabPage != null && fileName != null && fileName.ToString() != "Текущая")
            {
                string destPath = $"{path}/{tabPage.TabIndex}";
                if (Directory.Exists(destPath))
                {
                    destPath += $"/{fileName}";
                    // Загружаем файл в вкладку.
                    using (StreamReader streamData = new StreamReader(destPath))
                    {
                        if (CheckSizeOfFile(destPath))
                        {
                            string data = streamData.ReadToEnd();
                            if (string.IsNullOrEmpty(data)) data = " ";
                            if (Path.GetExtension(destPath) == ".rtf") GetSelectedRichTextBox().Rtf = data;
                            else GetSelectedRichTextBox().Text = data;
                            s_fileChangeList[tabPage.TabIndex] = true;
                            if (tabPage.Text[0] != '*') tabPage.Text = '*' + tabPage.Text;

                        }
                        else
                        {
                            MessageBox.Show($"Файл автосохранения имеет размер больше 10МБ, отказ в чтении!",
                                "Размер файла",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Событие, возникающие при необходимости отменить действие.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetSelectedRichTextBox().CanUndo)
                {
                    GetSelectedRichTextBox().Undo();
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
                if (TryToExecuteCompilationProcess()) return;
            }
            catch
            {
                MessageBox.Show($"Произошла неизвестная ошибка при компиляции!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TryToExecuteCompilationProcess()
        {
            TabPage tabPage = MainTabControl.SelectedTab;
            if (tabPage != null)
            {
                if (Path.GetExtension(s_fileList[tabPage.TabIndex]) == ".cs")
                {
                    if (StartCompilationWork(tabPage)) return true;
                }
                else
                {
                    MessageBox.Show("Невозможно скомпилировать данный файл! Он не формата *.cs", "Отказ",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Нечего компилировать, ведь отсутсвуют вкладки!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool StartCompilationWork(TabPage tabPage)
        {
            SaveFile(tabPage);
            string source = s_fileList[tabPage.TabIndex];
            List<string> data = ReadSettingsFromFile();
            string compil = null;
            if (CheckCompilationSettings(data, ref compil)) return true;
            var value = OpenCmdAndExecuteCommand(compil, source);
            PrintCommandExecutionData(value, source);
            return false;
        }

        private static bool CheckCompilationSettings(List<string> data, ref string compil)
        {
            try
            {
                if (data != null)
                {
                    compil = data[7];
                }
            }
            catch
            {
                MessageBox.Show("Перед компиляцией укажите путь к вашему csc.exe в настройках!",
                    "Настройка",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            if (compil == null) compil = "НЕТ";
            if (compil == "НЕТ")
            {
                MessageBox.Show("Перед компиляцией укажите путь к вашему csc.exe в настройках",
                    "Настройка",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            if (compil[^7..] != "csc.exe")
            {
                MessageBox.Show("Установите правильный компилятор в настройках!\n" +
                                "Требуется csc.exe", "Отказ",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }

            return false;
        }

        private static void PrintCommandExecutionData(string value, string source)
        {
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
            else if (!File.Exists($@"{Path.GetFileNameWithoutExtension(source)}.exe"))
            {
                MessageBox.Show($"После отправки на компиляцию не было создано выходного файла.\n" +
                                $"Вероятно, вы указали csc.exe не связанный с технологией Microsoft .NET!\n" +
                                $"Или произошла неизвестная ошибка!\n", "Провал!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show($"Программа успешно скомпилирована.\n" +
                                $"Исполняемый файл с именем файла можно найти в корневой папке приложения!\n" +
                                $"Путь: {Path.GetFullPath("./" + Path.GetFileNameWithoutExtension(source) + ".exe")}",
                    "Успех!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string OpenCmdAndExecuteCommand(string compil, string source)
        {
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
            return value;
        }
    }
}
