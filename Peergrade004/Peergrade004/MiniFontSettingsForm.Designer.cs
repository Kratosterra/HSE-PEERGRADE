
namespace Peergrade004
{
    partial class MiniFontSettingsForm
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
            this.LabelInfoSize = new System.Windows.Forms.Label();
            this.NumericUpDownFontSize = new System.Windows.Forms.NumericUpDown();
            this.LabelInfoFace = new System.Windows.Forms.Label();
            this.ComboBoxFontChoice = new System.Windows.Forms.ComboBox();
            this.ButtonAccept = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelInfoSize
            // 
            this.LabelInfoSize.AutoSize = true;
            this.LabelInfoSize.Location = new System.Drawing.Point(12, 39);
            this.LabelInfoSize.Name = "LabelInfoSize";
            this.LabelInfoSize.Size = new System.Drawing.Size(95, 15);
            this.LabelInfoSize.TabIndex = 7;
            this.LabelInfoSize.Text = "Размер шрифта";
            // 
            // NumericUpDownFontSize
            // 
            this.NumericUpDownFontSize.Location = new System.Drawing.Point(12, 57);
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
            this.NumericUpDownFontSize.TabIndex = 6;
            this.NumericUpDownFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.NumericUpDownFontSize.ValueChanged += new System.EventHandler(this.NumericUpDownFontSize_ValueChanged);
            // 
            // LabelInfoFace
            // 
            this.LabelInfoFace.AutoSize = true;
            this.LabelInfoFace.Location = new System.Drawing.Point(251, 39);
            this.LabelInfoFace.Name = "LabelInfoFace";
            this.LabelInfoFace.Size = new System.Drawing.Size(75, 15);
            this.LabelInfoFace.TabIndex = 9;
            this.LabelInfoFace.Text = "Тип шрифта";
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
            this.ComboBoxFontChoice.Location = new System.Drawing.Point(251, 57);
            this.ComboBoxFontChoice.Name = "ComboBoxFontChoice";
            this.ComboBoxFontChoice.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxFontChoice.TabIndex = 8;
            this.ComboBoxFontChoice.SelectedIndexChanged += new System.EventHandler(this.ComboBoxFontChoice_SelectedIndexChanged);
            // 
            // ButtonAccept
            // 
            this.ButtonAccept.Location = new System.Drawing.Point(297, 122);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(75, 23);
            this.ButtonAccept.TabIndex = 10;
            this.ButtonAccept.Text = "Принять";
            this.ButtonAccept.UseVisualStyleBackColor = true;
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // MiniFontSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 157);
            this.Controls.Add(this.ButtonAccept);
            this.Controls.Add(this.LabelInfoFace);
            this.Controls.Add(this.ComboBoxFontChoice);
            this.Controls.Add(this.LabelInfoSize);
            this.Controls.Add(this.NumericUpDownFontSize);
            this.Name = "MiniFontSettingsForm";
            this.Text = "Выбор шрифта для выделенной области";
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelInfoSize;
        private System.Windows.Forms.NumericUpDown NumericUpDownFontSize;
        private System.Windows.Forms.Label LabelInfoFace;
        private System.Windows.Forms.ComboBox ComboBoxFontChoice;
        private System.Windows.Forms.Button ButtonAccept;
    }
}