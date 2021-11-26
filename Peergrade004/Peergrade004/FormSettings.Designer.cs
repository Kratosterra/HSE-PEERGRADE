
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
            this.labelTimingShow = new System.Windows.Forms.Label();
            this.labelMSeconds = new System.Windows.Forms.Label();
            this.buttonTimingAssept = new System.Windows.Forms.Button();
            this.trackBarTiming = new System.Windows.Forms.TrackBar();
            this.radioButtonWhite = new System.Windows.Forms.RadioButton();
            this.radioButtonBlack = new System.Windows.Forms.RadioButton();
            this.radioButtonPurple = new System.Windows.Forms.RadioButton();
            this.radioButtonTeal = new System.Windows.Forms.RadioButton();
            this.labelTheme = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTiming)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTimingShow
            // 
            this.labelTimingShow.AutoSize = true;
            this.labelTimingShow.Location = new System.Drawing.Point(9, 10);
            this.labelTimingShow.Name = "labelTimingShow";
            this.labelTimingShow.Size = new System.Drawing.Size(200, 15);
            this.labelTimingShow.TabIndex = 1;
            this.labelTimingShow.Text = "Период сохранения файлов: 1 мин";
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
            this.buttonTimingAssept.Click += new System.EventHandler(this.buttonTimingAssept_Click);
            // 
            // trackBarTiming
            // 
            this.trackBarTiming.Location = new System.Drawing.Point(9, 28);
            this.trackBarTiming.Name = "trackBarTiming";
            this.trackBarTiming.Size = new System.Drawing.Size(162, 45);
            this.trackBarTiming.TabIndex = 4;
            this.trackBarTiming.Scroll += new System.EventHandler(this.trackBarTiming_Scroll);
            // 
            // radioButtonWhite
            // 
            this.radioButtonWhite.AutoSize = true;
            this.radioButtonWhite.Checked = true;
            this.radioButtonWhite.Location = new System.Drawing.Point(13, 89);
            this.radioButtonWhite.Name = "radioButtonWhite";
            this.radioButtonWhite.Size = new System.Drawing.Size(95, 19);
            this.radioButtonWhite.TabIndex = 5;
            this.radioButtonWhite.TabStop = true;
            this.radioButtonWhite.Text = "White Theme";
            this.radioButtonWhite.UseVisualStyleBackColor = true;
            // 
            // radioButtonBlack
            // 
            this.radioButtonBlack.AutoSize = true;
            this.radioButtonBlack.Location = new System.Drawing.Point(13, 114);
            this.radioButtonBlack.Name = "radioButtonBlack";
            this.radioButtonBlack.Size = new System.Drawing.Size(92, 19);
            this.radioButtonBlack.TabIndex = 6;
            this.radioButtonBlack.Text = "Black Theme";
            this.radioButtonBlack.UseVisualStyleBackColor = true;
            // 
            // radioButtonPurple
            // 
            this.radioButtonPurple.AutoSize = true;
            this.radioButtonPurple.Location = new System.Drawing.Point(13, 140);
            this.radioButtonPurple.Name = "radioButtonPurple";
            this.radioButtonPurple.Size = new System.Drawing.Size(139, 19);
            this.radioButtonPurple.TabIndex = 7;
            this.radioButtonPurple.Text = "Hollow Purple Theme";
            this.radioButtonPurple.UseVisualStyleBackColor = true;
            // 
            // radioButtonTeal
            // 
            this.radioButtonTeal.AutoSize = true;
            this.radioButtonTeal.Location = new System.Drawing.Point(13, 166);
            this.radioButtonTeal.Name = "radioButtonTeal";
            this.radioButtonTeal.Size = new System.Drawing.Size(135, 19);
            this.radioButtonTeal.TabIndex = 8;
            this.radioButtonTeal.Text = "Delicious Teal Theme";
            this.radioButtonTeal.UseVisualStyleBackColor = true;
            // 
            // labelTheme
            // 
            this.labelTheme.AutoSize = true;
            this.labelTheme.Location = new System.Drawing.Point(9, 71);
            this.labelTheme.Name = "labelTheme";
            this.labelTheme.Size = new System.Drawing.Size(149, 15);
            this.labelTheme.TabIndex = 9;
            this.labelTheme.Text = "Выбор темы приложения";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 450);
            this.Controls.Add(this.labelTheme);
            this.Controls.Add(this.radioButtonTeal);
            this.Controls.Add(this.radioButtonPurple);
            this.Controls.Add(this.radioButtonBlack);
            this.Controls.Add(this.radioButtonWhite);
            this.Controls.Add(this.trackBarTiming);
            this.Controls.Add(this.buttonTimingAssept);
            this.Controls.Add(this.labelMSeconds);
            this.Controls.Add(this.labelTimingShow);
            this.Name = "FormSettings";
            this.Text = "Настройки";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTiming)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTimingShow;
        private System.Windows.Forms.Label labelMSeconds;
        private System.Windows.Forms.Button buttonTimingAssept;
        private System.Windows.Forms.TrackBar trackBarTiming;
        private System.Windows.Forms.RadioButton radioButtonWhite;
        private System.Windows.Forms.RadioButton radioButtonBlack;
        private System.Windows.Forms.RadioButton radioButtonPurple;
        private System.Windows.Forms.RadioButton radioButtonTeal;
        private System.Windows.Forms.Label labelTheme;
    }
}