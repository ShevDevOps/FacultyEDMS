namespace FacultyEDMS
{
    partial class UserForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.DocumetsView = new System.Windows.Forms.DataGridView();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.RedactButton = new System.Windows.Forms.Button();
            this.LookButton = new System.Windows.Forms.Button();
            this.InfoButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.InfoBox = new System.Windows.Forms.GroupBox();
            this.HideButton = new System.Windows.Forms.Button();
            this.DocumentHistoryView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.HistoryButton = new System.Windows.Forms.Button();
            this.btnCreateNewDocument = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DocumetsView)).BeginInit();
            this.InfoBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentHistoryView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(560, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Особистий кабінет користувача <<USER>>\r\n";
            // 
            // DocumetsView
            // 
            this.DocumetsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DocumetsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DocumetsView.Location = new System.Drawing.Point(18, 75);
            this.DocumetsView.Name = "DocumetsView";
            this.DocumetsView.RowHeadersWidth = 51;
            this.DocumetsView.Size = new System.Drawing.Size(417, 363);
            this.DocumetsView.TabIndex = 1;
            // 
            // SearchBox
            // 
            this.SearchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SearchBox.Location = new System.Drawing.Point(18, 43);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(338, 26);
            this.SearchBox.TabIndex = 2;
            // 
            // SearchButton
            // 
            this.SearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SearchButton.Location = new System.Drawing.Point(360, 38);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 34);
            this.SearchButton.TabIndex = 3;
            this.SearchButton.Text = "Пошук";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.radioButton1.Location = new System.Drawing.Point(441, 78);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(107, 24);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "За назвою";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.radioButton2.Location = new System.Drawing.Point(441, 108);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(116, 24);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.Text = "За автором";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // RedactButton
            // 
            this.RedactButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.RedactButton.Location = new System.Drawing.Point(441, 138);
            this.RedactButton.Name = "RedactButton";
            this.RedactButton.Size = new System.Drawing.Size(207, 34);
            this.RedactButton.TabIndex = 7;
            this.RedactButton.Text = "Редагувати документ";
            this.RedactButton.UseVisualStyleBackColor = true;
            this.RedactButton.Click += new System.EventHandler(this.RedactButton_Click);
            // 
            // LookButton
            // 
            this.LookButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LookButton.Location = new System.Drawing.Point(441, 178);
            this.LookButton.Name = "LookButton";
            this.LookButton.Size = new System.Drawing.Size(207, 34);
            this.LookButton.TabIndex = 8;
            this.LookButton.Text = "Переглянути документ";
            this.LookButton.UseVisualStyleBackColor = true;
            this.LookButton.Click += new System.EventHandler(this.LookButton_Click);
            // 
            // InfoButton
            // 
            this.InfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InfoButton.Location = new System.Drawing.Point(441, 218);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.Size = new System.Drawing.Size(207, 51);
            this.InfoButton.TabIndex = 9;
            this.InfoButton.Text = "переглянути інформацію про документ";
            this.InfoButton.UseVisualStyleBackColor = true;
            this.InfoButton.Click += new System.EventHandler(this.InfoButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ExitButton.Location = new System.Drawing.Point(441, 38);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 34);
            this.ExitButton.TabIndex = 10;
            this.ExitButton.Text = "Вихід";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // InfoBox
            // 
            this.InfoBox.Controls.Add(this.HideButton);
            this.InfoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InfoBox.Location = new System.Drawing.Point(653, 75);
            this.InfoBox.Name = "InfoBox";
            this.InfoBox.Size = new System.Drawing.Size(216, 363);
            this.InfoBox.TabIndex = 11;
            this.InfoBox.TabStop = false;
            this.InfoBox.Text = "Інформація про документ";
            // 
            // HideButton
            // 
            this.HideButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.HideButton.Location = new System.Drawing.Point(6, 323);
            this.HideButton.Name = "HideButton";
            this.HideButton.Size = new System.Drawing.Size(175, 34);
            this.HideButton.TabIndex = 11;
            this.HideButton.Text = "Сховати інформацію";
            this.HideButton.UseVisualStyleBackColor = true;
            this.HideButton.Click += new System.EventHandler(this.HideButton_Click);
            // 
            // DocumentHistoryView
            // 
            this.DocumentHistoryView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DocumentHistoryView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DocumentHistoryView.Location = new System.Drawing.Point(892, 75);
            this.DocumentHistoryView.Name = "DocumentHistoryView";
            this.DocumentHistoryView.RowHeadersWidth = 51;
            this.DocumentHistoryView.Size = new System.Drawing.Size(240, 363);
            this.DocumentHistoryView.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(888, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Історія змін";
            // 
            // HistoryButton
            // 
            this.HistoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.HistoryButton.Location = new System.Drawing.Point(441, 276);
            this.HistoryButton.Name = "HistoryButton";
            this.HistoryButton.Size = new System.Drawing.Size(207, 33);
            this.HistoryButton.TabIndex = 14;
            this.HistoryButton.Text = "переглянути історію змін";
            this.HistoryButton.UseVisualStyleBackColor = true;
            this.HistoryButton.Click += new System.EventHandler(this.HistoryButton_Click);
            // 
            // btnCreateNewDocument
            // 
            this.btnCreateNewDocument.Location = new System.Drawing.Point(441, 314);
            this.btnCreateNewDocument.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCreateNewDocument.Name = "btnCreateNewDocument";
            this.btnCreateNewDocument.Size = new System.Drawing.Size(207, 33);
            this.btnCreateNewDocument.TabIndex = 15;
            this.btnCreateNewDocument.Text = "Create Document";
            this.btnCreateNewDocument.UseVisualStyleBackColor = true;
            this.btnCreateNewDocument.Click += new System.EventHandler(this.btnCreateNewDocument_Click);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 450);
            this.Controls.Add(this.btnCreateNewDocument);
            this.Controls.Add(this.HistoryButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DocumentHistoryView);
            this.Controls.Add(this.InfoBox);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.InfoButton);
            this.Controls.Add(this.LookButton);
            this.Controls.Add(this.RedactButton);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.DocumetsView);
            this.Controls.Add(this.label1);
            this.Name = "UserForm";
            this.Text = "Особистий кабінет користувача <<USER>>";
            ((System.ComponentModel.ISupportInitialize)(this.DocumetsView)).EndInit();
            this.InfoBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DocumentHistoryView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DocumetsView;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button RedactButton;
        private System.Windows.Forms.Button LookButton;
        private System.Windows.Forms.Button InfoButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.GroupBox InfoBox;
        private System.Windows.Forms.Button HideButton;
        private System.Windows.Forms.DataGridView DocumentHistoryView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button HistoryButton;
        private System.Windows.Forms.Button btnCreateNewDocument;
    }
}