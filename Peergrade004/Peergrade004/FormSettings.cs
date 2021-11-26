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
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            ExecuteSetings();
        }

        private void trackBarTiming_Scroll(object sender, EventArgs e)
        {
            labelTimingShow.Text = $"Период сохранения файлов: {trackBarTiming.Value + 1} мин";
        }

        private void buttonTimingAssept_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter("Settings.txt", false))
                {
                    int timing = (trackBarTiming.Value + 1) * 1000 * 60;
                    streamWriter.WriteLine(timing);
                    string themeName;
                    if (radioButtonBlack.Checked == true) themeName = radioButtonBlack.Text;
                    else if (radioButtonWhite.Checked == true) themeName = radioButtonWhite.Text;
                    else if (radioButtonPurple.Checked == true) themeName = radioButtonPurple.Text;
                    else if (radioButtonTeal.Checked == true) themeName = radioButtonTeal.Text;
                    else themeName = "None";
                    streamWriter.WriteLine(themeName);
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
                trackBarTiming.Value = (Int32.Parse(data[0])-1) / (1000*60);
                labelTimingShow.Text = $"Период сохранения файлов: {trackBarTiming.Value + 1} мин";
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
                    radioButtonWhite.Checked = true;
                    SetWhiteThemeFormSettings();
                    break;
                }
                case "Black Theme":
                    {
                        radioButtonBlack.Checked = true;
                        SetBlackThemeFormSettings();
                        break;
                    }
                case "Hollow Purple Theme":
                    {
                        radioButtonPurple.Checked = true;
                        SetPurpleThemeFormSettings();
                        break;
                    }
                case "Delicious Teal Theme":
                    {
                        radioButtonTeal.Checked = true;
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
