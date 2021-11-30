
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontSettings));
            this.LabelExample = new System.Windows.Forms.Label();
            this.LabelExampleShow = new System.Windows.Forms.Label();
            this.NumericUpDownFontSize = new System.Windows.Forms.NumericUpDown();
            this.ComboBoxFontChoice = new System.Windows.Forms.ComboBox();
            this.ButtonAccept = new System.Windows.Forms.Button();
            this.LabelInfoSize = new System.Windows.Forms.Label();
            this.LabelInfoFace = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelExample
            // 
            this.LabelExample.AutoSize = true;
            this.LabelExample.Location = new System.Drawing.Point(12, 73);
            this.LabelExample.Name = "LabelExample";
            this.LabelExample.Size = new System.Drawing.Size(54, 15);
            this.LabelExample.TabIndex = 0;
            this.LabelExample.Text = "Образец";
            // 
            // LabelExampleShow
            // 
            this.LabelExampleShow.AutoSize = true;
            this.LabelExampleShow.Font = new System.Drawing.Font("Arial Narrow", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelExampleShow.Location = new System.Drawing.Point(12, 97);
            this.LabelExampleShow.Name = "LabelExampleShow";
            this.LabelExampleShow.Size = new System.Drawing.Size(233, 63);
            this.LabelExampleShow.TabIndex = 1;
            this.LabelExampleShow.Text = "AaBbYyZz";
            // 
            // NumericUpDownFontSize
            // 
            this.NumericUpDownFontSize.Location = new System.Drawing.Point(12, 32);
            this.NumericUpDownFontSize.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.NumericUpDownFontSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.NumericUpDownFontSize.Name = "NumericUpDownFontSize";
            this.NumericUpDownFontSize.Size = new System.Drawing.Size(120, 23);
            this.NumericUpDownFontSize.TabIndex = 2;
            this.NumericUpDownFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.NumericUpDownFontSize.ValueChanged += new System.EventHandler(this.NumericUpDownFontSize_ValueChanged);
            // 
            // ComboBoxFontChoice
            // 
            this.ComboBoxFontChoice.FormattingEnabled = true;
            this.ComboBoxFontChoice.Items.AddRange(new object[] {
            "Arial",
            "Comic Sans Ms",
            "Times New Roman",
            "ALGERIAN",
            "Impact",
            "Lato"});
            this.ComboBoxFontChoice.Location = new System.Drawing.Point(277, 32);
            this.ComboBoxFontChoice.Name = "ComboBoxFontChoice";
            this.ComboBoxFontChoice.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxFontChoice.TabIndex = 3;
            this.ComboBoxFontChoice.SelectedIndexChanged += new System.EventHandler(this.ComboBoxFontChoice_SelectedIndexChanged);
            // 
            // ButtonAccept
            // 
            this.ButtonAccept.Location = new System.Drawing.Point(323, 156);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(75, 23);
            this.ButtonAccept.TabIndex = 4;
            this.ButtonAccept.Text = "Принять";
            this.ButtonAccept.UseVisualStyleBackColor = true;
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // LabelInfoSize
            // 
            this.LabelInfoSize.AutoSize = true;
            this.LabelInfoSize.Location = new System.Drawing.Point(12, 13);
            this.LabelInfoSize.Name = "LabelInfoSize";
            this.LabelInfoSize.Size = new System.Drawing.Size(95, 15);
            this.LabelInfoSize.TabIndex = 5;
            this.LabelInfoSize.Text = "Размер шрифта";
            // 
            // LabelInfoFace
            // 
            this.LabelInfoFace.AutoSize = true;
            this.LabelInfoFace.Location = new System.Drawing.Point(277, 13);
            this.LabelInfoFace.Name = "LabelInfoFace";
            this.LabelInfoFace.Size = new System.Drawing.Size(75, 15);
            this.LabelInfoFace.TabIndex = 6;
            this.LabelInfoFace.Text = "Тип шрифта";
            // 
            // FontSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 191);
            this.Controls.Add(this.LabelInfoFace);
            this.Controls.Add(this.LabelInfoSize);
            this.Controls.Add(this.ButtonAccept);
            this.Controls.Add(this.ComboBoxFontChoice);
            this.Controls.Add(this.NumericUpDownFontSize);
            this.Controls.Add(this.LabelExampleShow);
            this.Controls.Add(this.LabelExample);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(425, 230);
            this.MinimumSize = new System.Drawing.Size(425, 230);
            this.Name = "FontSettings";
            this.Text = "Шрифт";
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelExample;
        private System.Windows.Forms.Label LabelExampleShow;
        private System.Windows.Forms.NumericUpDown NumericUpDownFontSize;
        private System.Windows.Forms.ComboBox ComboBoxFontChoice;
        private System.Windows.Forms.Button ButtonAccept;
        private System.Windows.Forms.Label LabelInfoSize;
        private System.Windows.Forms.Label LabelInfoFace;
    }
}