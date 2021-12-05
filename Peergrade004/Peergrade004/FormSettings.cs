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
using System.Xml.XPath;

namespace Peergrade004
{
    /// <summary>
    /// Класс, содержащий события и методы для реализации событий формы настроек Notepad+.
    /// </summary>
    public partial class FormSettings : Form
    {

        /// <summary>
        /// Конструктор формы настроек.
        /// </summary>
        public FormSettings()
        {
            // Иницализируем создание стартовых компонентов.
            InitializeComponent();
            // Исполняем настройки.
            ExecuteSettings();
        }

        /// <summary>
        /// Событие, возникающие при смене положения ползунка периода сохранения файла.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void TrackBarTiming_Scroll(object sender, EventArgs e)
        {
            LabelTimingShow.Text = $"Период сохранения файлов: {TrackBarTiming.Value + 1} мин";
        }

        /// <summary>
        /// Событие, возникающие принятии настроек.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void ButtonTimingAssept_Click(object sender, EventArgs e)
        {
            try
            {
                // Пытаемся записать текущие настройки в файл.
                using (StreamWriter streamWriter = new StreamWriter("Settings.txt", false))
                {
                    TryToSaveSelectedSettings(streamWriter);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении настроек", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
            }
        }

        /// <summary>
        /// Метод, сохраняющий текущие настройки в файл.
        /// </summary>
        /// <param name="streamWriter">Обьект записи в файл.</param>
        private void TryToSaveSelectedSettings(StreamWriter streamWriter)
        {
            // Получаем данные из элементов контроль и записываем их в файл настроек.
            int timing = (TrackBarTiming.Value + 1) * 1000 * 60;
            streamWriter.WriteLine(timing);
            string themeName;
            if (RadioButtonBlack.Checked == true) themeName = RadioButtonBlack.Text;
            else if (RadioButtonWhite.Checked == true) themeName = RadioButtonWhite.Text;
            else if (RadioButtonPurple.Checked == true) themeName = RadioButtonPurple.Text;
            else if (RadioButtonTeal.Checked == true) themeName = RadioButtonTeal.Text;
            else themeName = "None";
            streamWriter.WriteLine(themeName);
            timing = (TrackBarAutoSaveTime.Value + 1) * 1000 * 60;
            streamWriter.WriteLine(timing);
            string colour = null;
            if (ComboBoxKeyWords.SelectedItem != null) colour = ComboBoxKeyWords.SelectedItem.ToString();
            streamWriter.WriteLine(colour ?? "None");
            colour = null; if (ComboBoxClassNames.SelectedItem != null) colour = ComboBoxClassNames.SelectedItem.ToString();
            streamWriter.WriteLine(colour ?? "None");
            colour = null;
            if (ComboBoxVariables.SelectedItem != null) colour = ComboBoxVariables.SelectedItem.ToString();
            streamWriter.WriteLine(colour ?? "None");
            colour = null;
            if (ComboBoxComments.SelectedItem != null) colour = ComboBoxComments.SelectedItem.ToString();
            streamWriter.WriteLine(colour ?? "None");
            colour = null;
            streamWriter.WriteLine(LableWayCompil.Text);
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
            catch (Exception ex)
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
            List<string> data = ReadSettingsFromFile();
            if (data != null && data.Count > 1)
            {
                SetSettingsFormSettings(data);
            }
        }

        /// <summary>
        /// Метод, устанавливающий настройки для формы.
        /// </summary>
        /// <param name="data">Список настроек.</param>
        private void SetSettingsFormSettings(List<string> data)
        {
            try
            {
                // Устанавливаем положение всех элементов контроля.
                TrackBarTiming.Value = (Int32.Parse(data[0]) - 1) / (1000 * 60);
                LabelTimingShow.Text = $"Период сохранения файлов: {TrackBarTiming.Value + 1} мин";
                SetThemeFormSettings(data[1]);
                TrackBarAutoSaveTime.Value = (Int32.Parse(data[2]) - 1) / (1000 * 60);
                LableAutoSaveOneFile.Text = $"Частота резервного копирования: {TrackBarAutoSaveTime.Value + 1} мин";
                if (data[3] != "None") ComboBoxKeyWords.SelectedItem = (object) data[3];
                if (data[4] != "None") ComboBoxClassNames.SelectedItem = (object) data[4];
                if (data[5] != "None") ComboBoxVariables.SelectedItem = (object) data[5];
                if (data[6] != "None") ComboBoxComments.SelectedItem = (object) data[6];
                LableWayCompil.Text = data[7];
                LableWayToCompil.Text = $"Компилятор: {Path.GetFileName(data[7])}";

            }
            catch
            {
                MessageBox.Show($"Произошла ошибка при применении настроек.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Метод, устанавливающий тему окна, получая её название.
        /// </summary>
        /// <param name="name">Назавание темы.</param>
        private void SetThemeFormSettings(string name)
        {
            switch (name)
            {
                case "White Theme":
                {
                    RadioButtonWhite.Checked = true;
                    SetWhiteThemeFormSettings();
                    break;
                }
                case "Black Theme":
                {
                    RadioButtonBlack.Checked = true;
                    SetBlackThemeFormSettings();
                    break;
                }
                case "Hollow Purple Theme":
                {
                    RadioButtonPurple.Checked = true;
                    SetPurpleThemeFormSettings();
                    break;
                }
                case "Delicious Teal Theme":
                {
                    RadioButtonTeal.Checked = true;
                    SetTealThemeFormSettings();
                    break;
                }

            }
        }

        /// <summary>
        /// Метод, устанавливающий белую тему.
        /// </summary>
        private void SetWhiteThemeFormSettings()
        {
            this.BackColor = Color.WhiteSmoke;
        }

        /// <summary>
        /// Метод, устанавливающий темную тему.
        /// </summary>
        private void SetBlackThemeFormSettings()
        {
            this.BackColor = Color.SlateGray;
        }

        /// <summary>
        /// Метод, устанавливающий фиолетовую тему.
        /// </summary>
        private void SetPurpleThemeFormSettings()
        {
            this.BackColor = Color.Purple;
        }

        /// <summary>
        /// Метод, устанавливающий мятную/бирюзовую тему.
        /// </summary>
        private void SetTealThemeFormSettings()
        {
            this.BackColor = Color.Aquamarine;
        }

        /// <summary>
        /// Событие, возникающие при смене положения ползунка периода резервного копирования.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void TrackBarAutoSaveTime_Scroll(object sender, EventArgs e)
        {
            try
            {
                LableAutoSaveOneFile.Text = $"Частота резервного копирования: {TrackBarAutoSaveTime.Value + 1} мин";
            }
            catch
            {
                MessageBox.Show($"Произошла ошибка при применении настроек.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, возникающие при смене компилятора в настройках.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void ButtonSetCompil_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog.FileName = "";
                // Запрашиваем исполняемый файл.
                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LableWayCompil.Text = OpenFileDialog.FileName;
                    LableWayToCompil.Text = $"Компилятор: {Path.GetFileName(OpenFileDialog.FileName)}";
                }
            }
            catch
            {
                MessageBox.Show($"Произошла ошибка при выборе компилятора.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
