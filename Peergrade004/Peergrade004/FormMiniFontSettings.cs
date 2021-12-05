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
    /// <summary>
    /// Класс, содержащий события и методы для реализации событий уменьшенной формы настроек шрифта Notepad+.
    /// </summary>
    public partial class MiniFontSettingsForm : Form
    {
        // Публичное поле, содержащее текущий размер шрифта.
        public int FontSize = 0;
        // Публичное поле, содержащее текущий вид шрифта.
        public FontFamily FontFaсe;

        /// <summary>
        /// Конструктор уменьшенной формы настроек шрифта.
        /// </summary>
        public MiniFontSettingsForm()
        {
            // Иницализируем создание стартовых компонентов.
            InitializeComponent();
            // Исполняем настройки.
            ExecuteSettings();
        }

        /// <summary>
        /// Событие, при нажатии на кнопку "принять" на форме.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            // Скрываем данную форму.
            this.Hide();
        }

        /// <summary>
        /// Событие, вызываемое смене значения размера шрифта.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void NumericUpDownFontSize_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Устанавиваем выбранное значение размера шрифта.
                FontSize = (int)NumericUpDownFontSize.Value;
            }
            catch
            {
                MessageBox.Show("Ошибка при выборе размера шрифта!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Событие, вызываемое смене типа шрифта.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void ComboBoxFontChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboBoxFontChoice.SelectedItem.ToString() != null)
                {
                    Font font = new Font(ComboBoxFontChoice.SelectedItem.ToString() ?? string.Empty,
                        (int)NumericUpDownFontSize.Value);
                    // Устанавливаем выбранное значение размера шрифта и его тип.
                    FontFaсe = font.FontFamily;
                    FontSize = (int)NumericUpDownFontSize.Value;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при выборе типа шрифта!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Устанавливаем тему окна.
                SetThemeFormSettings(data[1]);
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
                        SetWhiteThemeFormSettings();
                        break;
                    }
                case "Black Theme":
                    {
                        SetBlackThemeFormSettings();
                        break;
                    }
                case "Hollow Purple Theme":
                    {
                        SetPurpleThemeFormSettings();
                        break;
                    }
                case "Delicious Teal Theme":
                    {
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

    }
}
