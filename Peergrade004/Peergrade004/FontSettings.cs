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
    public partial class FontSettings : Form
    {
        public int fontSize = 0;
        public FontFamily fontFaсe;


        public FontSettings()
        {
            InitializeComponent();
            ExecuteSetings();
        }

        private void numericUpDownFontSize_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                labelExampleShow.Font = new Font(labelExampleShow.Font.FontFamily, (int)numericUpDownFontSize.Value);
                fontSize = (int)numericUpDownFontSize.Value;
            }
            catch
            {
                MessageBox.Show("Ошибка при выборе размера шрифта!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxFontChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxFontChoice.SelectedItem.ToString() != null)
                {
                    labelExampleShow.Font = new Font(comboBoxFontChoice.SelectedItem.ToString() ?? string.Empty,
                        (int)numericUpDownFontSize.Value);
                    fontFaсe = labelExampleShow.Font.FontFamily;
                    fontSize = (int) numericUpDownFontSize.Value;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при выборе типа шрифта!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            this.Hide();
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
            if (data != null)
            {
                SetSettingsFormSettings(data);
            }
        }


        private void SetSettingsFormSettings(List<string> data)
        {
            try
            {

                SetThemeFormSettings(data[1]);
            }
            catch
            {
                MessageBox.Show($"Произошла ошибка при применении настроек.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

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

        private void SetWhiteThemeFormSettings()
        {
            this.BackColor = Color.WhiteSmoke;
        }


        private void SetBlackThemeFormSettings()
        {
            this.BackColor = Color.SlateGray;
        }

        private void SetPurpleThemeFormSettings()
        {
            this.BackColor = Color.Purple;
        }
        private void SetTealThemeFormSettings()
        {
            this.BackColor = Color.Aquamarine;
        }
    }
}
