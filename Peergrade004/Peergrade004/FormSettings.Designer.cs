
namespace Peergrade004
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.LabelTimingShow = new System.Windows.Forms.Label();
            this.labelMSeconds = new System.Windows.Forms.Label();
            this.buttonTimingAssept = new System.Windows.Forms.Button();
            this.TrackBarTiming = new System.Windows.Forms.TrackBar();
            this.RadioButtonWhite = new System.Windows.Forms.RadioButton();
            this.RadioButtonBlack = new System.Windows.Forms.RadioButton();
            this.RadioButtonPurple = new System.Windows.Forms.RadioButton();
            this.RadioButtonTeal = new System.Windows.Forms.RadioButton();
            this.LabelTheme = new System.Windows.Forms.Label();
            this.TrackBarAutoSaveTime = new System.Windows.Forms.TrackBar();
            this.LableAutoSaveOneFile = new System.Windows.Forms.Label();
            this.LableColour = new System.Windows.Forms.Label();
            this.ComboBoxClassNames = new System.Windows.Forms.ComboBox();
            this.ComboBoxVariables = new System.Windows.Forms.ComboBox();
            this.ComboBoxComments = new System.Windows.Forms.ComboBox();
            this.LableKeyWords = new System.Windows.Forms.Label();
            this.LableClassNames = new System.Windows.Forms.Label();
            this.LableVariables = new System.Windows.Forms.Label();
            this.LableComments = new System.Windows.Forms.Label();
            this.LableWayToCompil = new System.Windows.Forms.Label();
            this.ButtonSetCompil = new System.Windows.Forms.Button();
            this.LableWayCompil = new System.Windows.Forms.Label();
            this.ComboBoxKeyWords = new System.Windows.Forms.ComboBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarTiming)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarAutoSaveTime)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelTimingShow
            // 
            this.LabelTimingShow.AutoSize = true;
            this.LabelTimingShow.Location = new System.Drawing.Point(9, 10);
            this.LabelTimingShow.Name = "LabelTimingShow";
            this.LabelTimingShow.Size = new System.Drawing.Size(200, 15);
            this.LabelTimingShow.TabIndex = 1;
            this.LabelTimingShow.Text = "Период сохранения файлов: 1 мин";
            // 
            // labelMSeconds
            // 
            this.labelMSeconds.AutoSize = true;
            this.labelMSeconds.Location = new System.Drawing.Point(77, 37);
            this.labelMSeconds.Name = "labelMSeconds";
            this.labelMSeconds.Size = new System.Drawing.Size(22, 15);
            this.labelMSeconds.TabIndex = 2;
            this.labelMSeconds.Text = "мс";
            // 
            // buttonTimingAssept
            // 
            this.buttonTimingAssept.Location = new System.Drawing.Point(441, 415);
            this.buttonTimingAssept.Name = "buttonTimingAssept";
            this.buttonTimingAssept.Size = new System.Drawing.Size(75, 23);
            this.buttonTimingAssept.TabIndex = 3;
            this.buttonTimingAssept.Text = "Принять";
            this.buttonTimingAssept.UseVisualStyleBackColor = true;
            this.buttonTimingAssept.Click += new System.EventHandler(this.ButtonTimingAssept_Click);
            // 
            // TrackBarTiming
            // 
            this.TrackBarTiming.Location = new System.Drawing.Point(9, 28);
            this.TrackBarTiming.Name = "TrackBarTiming";
            this.TrackBarTiming.Size = new System.Drawing.Size(162, 45);
            this.TrackBarTiming.TabIndex = 4;
            this.TrackBarTiming.Scroll += new System.EventHandler(this.TrackBarTiming_Scroll);
            // 
            // RadioButtonWhite
            // 
            this.RadioButtonWhite.AutoSize = true;
            this.RadioButtonWhite.Checked = true;
            this.RadioButtonWhite.Location = new System.Drawing.Point(12, 102);
            this.RadioButtonWhite.Name = "RadioButtonWhite";
            this.RadioButtonWhite.Size = new System.Drawing.Size(95, 19);
            this.RadioButtonWhite.TabIndex = 5;
            this.RadioButtonWhite.TabStop = true;
            this.RadioButtonWhite.Text = "White Theme";
            this.RadioButtonWhite.UseVisualStyleBackColor = true;
            // 
            // RadioButtonBlack
            // 
            this.RadioButtonBlack.AutoSize = true;
            this.RadioButtonBlack.Location = new System.Drawing.Point(12, 131);
            this.RadioButtonBlack.Name = "RadioButtonBlack";
            this.RadioButtonBlack.Size = new System.Drawing.Size(92, 19);
            this.RadioButtonBlack.TabIndex = 6;
            this.RadioButtonBlack.Text = "Black Theme";
            this.RadioButtonBlack.UseVisualStyleBackColor = true;
            // 
            // RadioButtonPurple
            // 
            this.RadioButtonPurple.AutoSize = true;
            this.RadioButtonPurple.Location = new System.Drawing.Point(12, 158);
            this.RadioButtonPurple.Name = "RadioButtonPurple";
            this.RadioButtonPurple.Size = new System.Drawing.Size(139, 19);
            this.RadioButtonPurple.TabIndex = 7;
            this.RadioButtonPurple.Text = "Hollow Purple Theme";
            this.RadioButtonPurple.UseVisualStyleBackColor = true;
            // 
            // RadioButtonTeal
            // 
            this.RadioButtonTeal.AutoSize = true;
            this.RadioButtonTeal.Location = new System.Drawing.Point(12, 187);
            this.RadioButtonTeal.Name = "RadioButtonTeal";
            this.RadioButtonTeal.Size = new System.Drawing.Size(135, 19);
            this.RadioButtonTeal.TabIndex = 8;
            this.RadioButtonTeal.Text = "Delicious Teal Theme";
            this.RadioButtonTeal.UseVisualStyleBackColor = true;
            // 
            // LabelTheme
            // 
            this.LabelTheme.AutoSize = true;
            this.LabelTheme.Location = new System.Drawing.Point(9, 71);
            this.LabelTheme.Name = "LabelTheme";
            this.LabelTheme.Size = new System.Drawing.Size(149, 15);
            this.LabelTheme.TabIndex = 9;
            this.LabelTheme.Text = "Выбор темы приложения";
            // 
            // TrackBarAutoSaveTime
            // 
            this.TrackBarAutoSaveTime.Location = new System.Drawing.Point(268, 28);
            this.TrackBarAutoSaveTime.Name = "TrackBarAutoSaveTime";
            this.TrackBarAutoSaveTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TrackBarAutoSaveTime.Size = new System.Drawing.Size(162, 45);
            this.TrackBarAutoSaveTime.TabIndex = 11;
            this.TrackBarAutoSaveTime.Scroll += new System.EventHandler(this.TrackBarAutoSaveTime_Scroll);
            // 
            // LableAutoSaveOneFile
            // 
            this.LableAutoSaveOneFile.AutoSize = true;
            this.LableAutoSaveOneFile.Location = new System.Drawing.Point(268, 10);
            this.LableAutoSaveOneFile.Name = "LableAutoSaveOneFile";
            this.LableAutoSaveOneFile.Size = new System.Drawing.Size(240, 15);
            this.LableAutoSaveOneFile.TabIndex = 10;
            this.LableAutoSaveOneFile.Text = "Частота создания резервных копий: 1 мин";
            // 
            // LableColour
            // 
            this.LableColour.AutoSize = true;
            this.LableColour.Location = new System.Drawing.Point(268, 71);
            this.LableColour.Name = "LableColour";
            this.LableColour.Size = new System.Drawing.Size(198, 15);
            this.LableColour.TabIndex = 12;
            this.LableColour.Text = "Цветовая подсветка синтаксиса .cs";
            // 
            // ComboBoxClassNames
            // 
            this.ComboBoxClassNames.FormattingEnabled = true;
            this.ComboBoxClassNames.Items.AddRange(new object[] {
            "Синий",
            "Зелёный",
            "Красный",
            "Фиолетовый",
            "Жёлтый"});
            this.ComboBoxClassNames.Location = new System.Drawing.Point(268, 125);
            this.ComboBoxClassNames.Name = "ComboBoxClassNames";
            this.ComboBoxClassNames.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxClassNames.TabIndex = 14;
            // 
            // ComboBoxVariables
            // 
            this.ComboBoxVariables.FormattingEnabled = true;
            this.ComboBoxVariables.Items.AddRange(new object[] {
            "Синий",
            "Зелёный",
            "Красный",
            "Фиолетовый",
            "Жёлтый"});
            this.ComboBoxVariables.Location = new System.Drawing.Point(268, 154);
            this.ComboBoxVariables.Name = "ComboBoxVariables";
            this.ComboBoxVariables.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxVariables.TabIndex = 15;
            // 
            // ComboBoxComments
            // 
            this.ComboBoxComments.FormattingEnabled = true;
            this.ComboBoxComments.Items.AddRange(new object[] {
            "Синий",
            "Зелёный",
            "Красный",
            "Фиолетовый",
            "Жёлтый"});
            this.ComboBoxComments.Location = new System.Drawing.Point(268, 183);
            this.ComboBoxComments.Name = "ComboBoxComments";
            this.ComboBoxComments.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxComments.TabIndex = 16;
            // 
            // LableKeyWords
            // 
            this.LableKeyWords.AutoSize = true;
            this.LableKeyWords.Location = new System.Drawing.Point(395, 104);
            this.LableKeyWords.Name = "LableKeyWords";
            this.LableKeyWords.Size = new System.Drawing.Size(100, 15);
            this.LableKeyWords.TabIndex = 17;
            this.LableKeyWords.Text = "Ключевые слова";
            // 
            // LableClassNames
            // 
            this.LableClassNames.AutoSize = true;
            this.LableClassNames.Location = new System.Drawing.Point(395, 133);
            this.LableClassNames.Name = "LableClassNames";
            this.LableClassNames.Size = new System.Drawing.Size(91, 15);
            this.LableClassNames.TabIndex = 18;
            this.LableClassNames.Text = "Имена классов";
            // 
            // LableVariables
            // 
            this.LableVariables.AutoSize = true;
            this.LableVariables.Location = new System.Drawing.Point(395, 162);
            this.LableVariables.Name = "LableVariables";
            this.LableVariables.Size = new System.Drawing.Size(73, 15);
            this.LableVariables.TabIndex = 19;
            this.LableVariables.Text = "Пременные";
            // 
            // LableComments
            // 
            this.LableComments.AutoSize = true;
            this.LableComments.Location = new System.Drawing.Point(395, 191);
            this.LableComments.Name = "LableComments";
            this.LableComments.Size = new System.Drawing.Size(84, 15);
            this.LableComments.TabIndex = 20;
            this.LableComments.Text = "Комментарии";
            // 
            // LableWayToCompil
            // 
            this.LableWayToCompil.AutoSize = true;
            this.LableWayToCompil.Location = new System.Drawing.Point(12, 244);
            this.LableWayToCompil.Name = "LableWayToCompil";
            this.LableWayToCompil.Size = new System.Drawing.Size(103, 15);
            this.LableWayToCompil.TabIndex = 21;
            this.LableWayToCompil.Text = "Компилятор: НЕТ";
            // 
            // ButtonSetCompil
            // 
            this.ButtonSetCompil.Location = new System.Drawing.Point(13, 263);
            this.ButtonSetCompil.Name = "ButtonSetCompil";
            this.ButtonSetCompil.Size = new System.Drawing.Size(75, 23);
            this.ButtonSetCompil.TabIndex = 22;
            this.ButtonSetCompil.Text = "Выбрать";
            this.ButtonSetCompil.UseVisualStyleBackColor = true;
            this.ButtonSetCompil.Click += new System.EventHandler(this.ButtonSetCompil_Click);
            // 
            // LableWayCompil
            // 
            this.LableWayCompil.AutoSize = true;
            this.LableWayCompil.Location = new System.Drawing.Point(94, 267);
            this.LableWayCompil.Name = "LableWayCompil";
            this.LableWayCompil.Size = new System.Drawing.Size(28, 15);
            this.LableWayCompil.TabIndex = 23;
            this.LableWayCompil.Text = "НЕТ";
            // 
            // ComboBoxKeyWords
            // 
            this.ComboBoxKeyWords.FormattingEnabled = true;
            this.ComboBoxKeyWords.Items.AddRange(new object[] {
            "Синий",
            "Зелёный",
            "Красный",
            "Фиолетовый",
            "Жёлтый"});
            this.ComboBoxKeyWords.Location = new System.Drawing.Point(268, 96);
            this.ComboBoxKeyWords.Name = "ComboBoxKeyWords";
            this.ComboBoxKeyWords.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxKeyWords.TabIndex = 24;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "Executable files .exe|*.exe";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 451);
            this.Controls.Add(this.ComboBoxKeyWords);
            this.Controls.Add(this.LableWayCompil);
            this.Controls.Add(this.ButtonSetCompil);
            this.Controls.Add(this.LableWayToCompil);
            this.Controls.Add(this.LableComments);
            this.Controls.Add(this.LableVariables);
            this.Controls.Add(this.LableClassNames);
            this.Controls.Add(this.LableKeyWords);
            this.Controls.Add(this.ComboBoxComments);
            this.Controls.Add(this.ComboBoxVariables);
            this.Controls.Add(this.ComboBoxClassNames);
            this.Controls.Add(this.LableColour);
            this.Controls.Add(this.TrackBarAutoSaveTime);
            this.Controls.Add(this.LableAutoSaveOneFile);
            this.Controls.Add(this.LabelTheme);
            this.Controls.Add(this.RadioButtonTeal);
            this.Controls.Add(this.RadioButtonPurple);
            this.Controls.Add(this.RadioButtonBlack);
            this.Controls.Add(this.RadioButtonWhite);
            this.Controls.Add(this.TrackBarTiming);
            this.Controls.Add(this.buttonTimingAssept);
            this.Controls.Add(this.labelMSeconds);
            this.Controls.Add(this.LabelTimingShow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(545, 490);
            this.MinimumSize = new System.Drawing.Size(545, 490);
            this.Name = "FormSettings";
            this.Text = "Настройки";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarTiming)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarAutoSaveTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LabelTimingShow;
        private System.Windows.Forms.Label labelMSeconds;
        private System.Windows.Forms.Button buttonTimingAssept;
        private System.Windows.Forms.TrackBar TrackBarTiming;
        private System.Windows.Forms.RadioButton RadioButtonWhite;
        private System.Windows.Forms.RadioButton RadioButtonBlack;
        private System.Windows.Forms.RadioButton RadioButtonPurple;
        private System.Windows.Forms.RadioButton RadioButtonTeal;
        private System.Windows.Forms.Label LabelTheme;
        private System.Windows.Forms.TrackBar TrackBarAutoSaveTime;
        private System.Windows.Forms.Label LableAutoSaveOneFile;
        private System.Windows.Forms.Label LableColour;
        private System.Windows.Forms.ComboBox СomboBoxKeyWords;
        private System.Windows.Forms.ComboBox ComboBoxClassNames;
        private System.Windows.Forms.ComboBox ComboBoxVariables;
        private System.Windows.Forms.ComboBox ComboBoxComments;
        private System.Windows.Forms.Label LableKeyWords;
        private System.Windows.Forms.Label LableClassNames;
        private System.Windows.Forms.Label LableVariables;
        private System.Windows.Forms.Label LableComments;
        private System.Windows.Forms.Label LableWayToCompil;
        private System.Windows.Forms.Button ButtonSetCompil;
        private System.Windows.Forms.Label LableWayCompil;
        private System.Windows.Forms.ComboBox СomboSuperBoxKeyWords;
        private System.Windows.Forms.ComboBox ComboBoxKeyWords;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
    }
}