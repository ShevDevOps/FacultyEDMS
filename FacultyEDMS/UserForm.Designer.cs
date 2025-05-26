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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnLogs = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DocumetsView)).BeginInit();
            this.InfoBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentHistoryView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(706, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Особистий кабінет користувача <<USER>>\r\n";
            // 
            // DocumetsView
            // 
            this.DocumetsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DocumetsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DocumetsView.Location = new System.Drawing.Point(24, 92);
            this.DocumetsView.Margin = new System.Windows.Forms.Padding(4);
            this.DocumetsView.Name = "DocumetsView";
            this.DocumetsView.RowHeadersWidth = 51;
            this.DocumetsView.Size = new System.Drawing.Size(556, 447);
            this.DocumetsView.TabIndex = 1;
            // 
            // SearchBox
            // 
            this.SearchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SearchBox.Location = new System.Drawing.Point(24, 53);
            this.SearchBox.Margin = new System.Windows.Forms.Padding(4);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(449, 30);
            this.SearchBox.TabIndex = 2;
            // 
            // SearchButton
            // 
            this.SearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SearchButton.Location = new System.Drawing.Point(480, 47);
            this.SearchButton.Margin = new System.Windows.Forms.Padding(4);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(100, 42);
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
            this.radioButton1.Location = new System.Drawing.Point(588, 96);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(129, 29);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "За назвою";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.radioButton2.Location = new System.Drawing.Point(588, 133);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(143, 29);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.Text = "За автором";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // RedactButton
            // 
            this.RedactButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RedactButton.Location = new System.Drawing.Point(588, 170);
            this.RedactButton.Margin = new System.Windows.Forms.Padding(4);
            this.RedactButton.Name = "RedactButton";
            this.RedactButton.Size = new System.Drawing.Size(173, 42);
            this.RedactButton.TabIndex = 7;
            this.RedactButton.Text = "Редагувати документ";
            this.RedactButton.UseVisualStyleBackColor = true;
            this.RedactButton.Click += new System.EventHandler(this.RedactButton_Click);
            // 
            // LookButton
            // 
            this.LookButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LookButton.Location = new System.Drawing.Point(588, 219);
            this.LookButton.Margin = new System.Windows.Forms.Padding(4);
            this.LookButton.Name = "LookButton";
            this.LookButton.Size = new System.Drawing.Size(276, 42);
            this.LookButton.TabIndex = 8;
            this.LookButton.Text = "Переглянути документ";
            this.LookButton.UseVisualStyleBackColor = true;
            this.LookButton.Click += new System.EventHandler(this.LookButton_Click);
            // 
            // InfoButton
            // 
            this.InfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InfoButton.Location = new System.Drawing.Point(588, 268);
            this.InfoButton.Margin = new System.Windows.Forms.Padding(4);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.Size = new System.Drawing.Size(276, 63);
            this.InfoButton.TabIndex = 9;
            this.InfoButton.Text = "переглянути інформацію про документ";
            this.InfoButton.UseVisualStyleBackColor = true;
            this.InfoButton.Click += new System.EventHandler(this.InfoButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ExitButton.Location = new System.Drawing.Point(588, 47);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(4);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(100, 42);
            this.ExitButton.TabIndex = 10;
            this.ExitButton.Text = "Вихід";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // InfoBox
            // 
            this.InfoBox.Controls.Add(this.HideButton);
            this.InfoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InfoBox.Location = new System.Drawing.Point(871, 92);
            this.InfoBox.Margin = new System.Windows.Forms.Padding(4);
            this.InfoBox.Name = "InfoBox";
            this.InfoBox.Padding = new System.Windows.Forms.Padding(4);
            this.InfoBox.Size = new System.Drawing.Size(288, 447);
            this.InfoBox.TabIndex = 11;
            this.InfoBox.TabStop = false;
            this.InfoBox.Text = "Інформація про документ";
            // 
            // HideButton
            // 
            this.HideButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.HideButton.Location = new System.Drawing.Point(8, 398);
            this.HideButton.Margin = new System.Windows.Forms.Padding(4);
            this.HideButton.Name = "HideButton";
            this.HideButton.Size = new System.Drawing.Size(233, 42);
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
            this.DocumentHistoryView.Location = new System.Drawing.Point(1189, 92);
            this.DocumentHistoryView.Margin = new System.Windows.Forms.Padding(4);
            this.DocumentHistoryView.Name = "DocumentHistoryView";
            this.DocumentHistoryView.RowHeadersWidth = 51;
            this.DocumentHistoryView.Size = new System.Drawing.Size(320, 447);
            this.DocumentHistoryView.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(1184, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Історія змін";
            // 
            // HistoryButton
            // 
            this.HistoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.HistoryButton.Location = new System.Drawing.Point(588, 338);
            this.HistoryButton.Margin = new System.Windows.Forms.Padding(4);
            this.HistoryButton.Name = "HistoryButton";
            this.HistoryButton.Size = new System.Drawing.Size(276, 41);
            this.HistoryButton.TabIndex = 14;
            this.HistoryButton.Text = "переглянути історію змін";
            this.HistoryButton.UseVisualStyleBackColor = true;
            this.HistoryButton.Click += new System.EventHandler(this.HistoryButton_Click);
            // 
            // btnCreateNewDocument
            // 
            this.btnCreateNewDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnCreateNewDocument.Location = new System.Drawing.Point(588, 385);
            this.btnCreateNewDocument.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateNewDocument.Name = "btnCreateNewDocument";
            this.btnCreateNewDocument.Size = new System.Drawing.Size(276, 41);
            this.btnCreateNewDocument.TabIndex = 15;
            this.btnCreateNewDocument.Text = "Створити документ";
            this.btnCreateNewDocument.UseVisualStyleBackColor = true;
            this.btnCreateNewDocument.Click += new System.EventHandler(this.btnCreateNewDocument_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDelete.Location = new System.Drawing.Point(763, 170);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 42);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Видалити";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnLogs
            // 
            this.btnLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnLogs.Location = new System.Drawing.Point(1409, 13);
            this.btnLogs.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(100, 42);
            this.btnLogs.TabIndex = 17;
            this.btnLogs.Text = "Logs";
            this.btnLogs.UseVisualStyleBackColor = true;
            this.btnLogs.Click += new System.EventHandler(this.btnLogs_Click);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1519, 554);
            this.Controls.Add(this.btnLogs);
            this.Controls.Add(this.btnDelete);
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
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnLogs;
    }
}