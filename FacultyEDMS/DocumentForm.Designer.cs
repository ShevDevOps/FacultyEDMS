namespace FacultyEDMS
{
    partial class DocumentForm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.BoldButton = new System.Windows.Forms.Button();
            this.ItalicButton = new System.Windows.Forms.Button();
            this.UnderLineButton = new System.Windows.Forms.Button();
            this.FontSizeNum = new System.Windows.Forms.NumericUpDown();
            this.LineupBox = new System.Windows.Forms.ListBox();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SignButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FontSizeNum)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 54);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(776, 384);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // BoldButton
            // 
            this.BoldButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.BoldButton.Location = new System.Drawing.Point(12, 10);
            this.BoldButton.Name = "BoldButton";
            this.BoldButton.Size = new System.Drawing.Size(34, 38);
            this.BoldButton.TabIndex = 1;
            this.BoldButton.Text = "Ж";
            this.BoldButton.UseVisualStyleBackColor = true;
            // 
            // ItalicButton
            // 
            this.ItalicButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic);
            this.ItalicButton.Location = new System.Drawing.Point(52, 10);
            this.ItalicButton.Name = "ItalicButton";
            this.ItalicButton.Size = new System.Drawing.Size(34, 38);
            this.ItalicButton.TabIndex = 2;
            this.ItalicButton.Text = "К";
            this.ItalicButton.UseVisualStyleBackColor = true;
            // 
            // UnderLineButton
            // 
            this.UnderLineButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline);
            this.UnderLineButton.Location = new System.Drawing.Point(92, 10);
            this.UnderLineButton.Name = "UnderLineButton";
            this.UnderLineButton.Size = new System.Drawing.Size(34, 38);
            this.UnderLineButton.TabIndex = 3;
            this.UnderLineButton.Text = "П";
            this.UnderLineButton.UseVisualStyleBackColor = true;
            // 
            // FontSizeNum
            // 
            this.FontSizeNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.FontSizeNum.Location = new System.Drawing.Point(132, 13);
            this.FontSizeNum.Name = "FontSizeNum";
            this.FontSizeNum.Size = new System.Drawing.Size(47, 30);
            this.FontSizeNum.TabIndex = 4;
            // 
            // LineupBox
            // 
            this.LineupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LineupBox.FormattingEnabled = true;
            this.LineupBox.ItemHeight = 20;
            this.LineupBox.Items.AddRange(new object[] {
            "По ліво",
            "По право",
            "Посередині",
            "По ширині"});
            this.LineupBox.Location = new System.Drawing.Point(185, 13);
            this.LineupBox.Name = "LineupBox";
            this.LineupBox.Size = new System.Drawing.Size(112, 24);
            this.LineupBox.TabIndex = 5;
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ExitButton.Location = new System.Drawing.Point(697, 10);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(91, 38);
            this.ExitButton.TabIndex = 6;
            this.ExitButton.Text = "Вихід";
            this.ExitButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SaveButton.Location = new System.Drawing.Point(600, 10);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(91, 38);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Зберегти";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // SignButton
            // 
            this.SignButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SignButton.Location = new System.Drawing.Point(492, 10);
            this.SignButton.Name = "SignButton";
            this.SignButton.Size = new System.Drawing.Size(102, 38);
            this.SignButton.TabIndex = 8;
            this.SignButton.Text = "Підписати";
            this.SignButton.UseVisualStyleBackColor = true;
            // 
            // DocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SignButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.LineupBox);
            this.Controls.Add(this.FontSizeNum);
            this.Controls.Add(this.UnderLineButton);
            this.Controls.Add(this.ItalicButton);
            this.Controls.Add(this.BoldButton);
            this.Controls.Add(this.richTextBox1);
            this.Name = "DocumentForm";
            this.Text = "Документ <<NAME>>";
            ((System.ComponentModel.ISupportInitialize)(this.FontSizeNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button BoldButton;
        private System.Windows.Forms.Button ItalicButton;
        private System.Windows.Forms.Button UnderLineButton;
        private System.Windows.Forms.NumericUpDown FontSizeNum;
        private System.Windows.Forms.ListBox LineupBox;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button SignButton;
    }
}