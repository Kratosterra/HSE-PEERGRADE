
namespace Peergrade004
{
    partial class FontSettings
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
            this.labelExample = new System.Windows.Forms.Label();
            this.labelExampleShow = new System.Windows.Forms.Label();
            this.numericUpDownFontSize = new System.Windows.Forms.NumericUpDown();
            this.comboBoxFontChoice = new System.Windows.Forms.ComboBox();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.labelInfoSize = new System.Windows.Forms.Label();
            this.labelInfoFace = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // labelExample
            // 
            this.labelExample.AutoSize = true;
            this.labelExample.Location = new System.Drawing.Point(12, 73);
            this.labelExample.Name = "labelExample";
            this.labelExample.Size = new System.Drawing.Size(54, 15);
            this.labelExample.TabIndex = 0;
            this.labelExample.Text = "Образец";
            // 
            // labelExampleShow
            // 
            this.labelExampleShow.AutoSize = true;
            this.labelExampleShow.Font = new System.Drawing.Font("Arial Narrow", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelExampleShow.Location = new System.Drawing.Point(12, 97);
            this.labelExampleShow.Name = "labelExampleShow";
            this.labelExampleShow.Size = new System.Drawing.Size(233, 63);
            this.labelExampleShow.TabIndex = 1;
            this.labelExampleShow.Text = "AaBbYyZz";
            // 
            // numericUpDownFontSize
            // 
            this.numericUpDownFontSize.Location = new System.Drawing.Point(12, 32);
            this.numericUpDownFontSize.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numericUpDownFontSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownFontSize.Name = "numericUpDownFontSize";
            this.numericUpDownFontSize.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownFontSize.TabIndex = 2;
            this.numericUpDownFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownFontSize.ValueChanged += new System.EventHandler(this.numericUpDownFontSize_ValueChanged);
            // 
            // comboBoxFontChoice
            // 
            this.comboBoxFontChoice.FormattingEnabled = true;
            this.comboBoxFontChoice.Items.AddRange(new object[] {
            "Arial",
            "Comic Sans Ms",
            "Times New Roman",
            "ALGERIAN",
            "Impact",
            "Lato"});
            this.comboBoxFontChoice.Location = new System.Drawing.Point(277, 32);
            this.comboBoxFontChoice.Name = "comboBoxFontChoice";
            this.comboBoxFontChoice.Size = new System.Drawing.Size(121, 23);
            this.comboBoxFontChoice.TabIndex = 3;
            this.comboBoxFontChoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxFontChoice_SelectedIndexChanged);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(323, 156);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 4;
            this.buttonAccept.Text = "Принять";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // labelInfoSize
            // 
            this.labelInfoSize.AutoSize = true;
            this.labelInfoSize.Location = new System.Drawing.Point(12, 13);
            this.labelInfoSize.Name = "labelInfoSize";
            this.labelInfoSize.Size = new System.Drawing.Size(95, 15);
            this.labelInfoSize.TabIndex = 5;
            this.labelInfoSize.Text = "Размер шрифта";
            // 
            // labelInfoFace
            // 
            this.labelInfoFace.AutoSize = true;
            this.labelInfoFace.Location = new System.Drawing.Point(277, 13);
            this.labelInfoFace.Name = "labelInfoFace";
            this.labelInfoFace.Size = new System.Drawing.Size(75, 15);
            this.labelInfoFace.TabIndex = 6;
            this.labelInfoFace.Text = "Тип шрифта";
            // 
            // FontSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 191);
            this.Controls.Add(this.labelInfoFace);
            this.Controls.Add(this.labelInfoSize);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.comboBoxFontChoice);
            this.Controls.Add(this.numericUpDownFontSize);
            this.Controls.Add(this.labelExampleShow);
            this.Controls.Add(this.labelExample);
            this.Name = "FontSettings";
            this.Text = "Шрифт";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelExample;
        private System.Windows.Forms.Label labelExampleShow;
        private System.Windows.Forms.NumericUpDown numericUpDownFontSize;
        private System.Windows.Forms.ComboBox comboBoxFontChoice;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Label labelInfoSize;
        private System.Windows.Forms.Label labelInfoFace;
    }
}