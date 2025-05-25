namespace FacultyEDMS
{
    partial class EnterForm
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
            this.EmailBox = new System.Windows.Forms.TextBox();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.EnterButton = new System.Windows.Forms.Button();
            this.DBstat = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(348, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Вхід";
            // 
            // EmailBox
            // 
            this.EmailBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.EmailBox.Location = new System.Drawing.Point(91, 53);
            this.EmailBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EmailBox.Name = "EmailBox";
            this.EmailBox.Size = new System.Drawing.Size(588, 30);
            this.EmailBox.TabIndex = 1;
            this.EmailBox.Text = "Пошта";
            // 
            // PasswordBox
            // 
            this.PasswordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PasswordBox.Location = new System.Drawing.Point(91, 92);
            this.PasswordBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(588, 30);
            this.PasswordBox.TabIndex = 2;
            this.PasswordBox.Text = "Пароль";
            // 
            // EnterButton
            // 
            this.EnterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.EnterButton.Location = new System.Drawing.Point(91, 132);
            this.EnterButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(100, 39);
            this.EnterButton.TabIndex = 3;
            this.EnterButton.Text = "Вхід";
            this.EnterButton.UseVisualStyleBackColor = true;
            this.EnterButton.Click += new System.EventHandler(this.EnterButton_Click);
            // 
            // DBstat
            // 
            this.DBstat.AutoSize = true;
            this.DBstat.Location = new System.Drawing.Point(576, 27);
            this.DBstat.Name = "DBstat";
            this.DBstat.Size = new System.Drawing.Size(103, 20);
            this.DBstat.TabIndex = 4;
            this.DBstat.Text = "radioButton1";
            this.DBstat.UseVisualStyleBackColor = true;
            // 
            // EnterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 513);
            this.Controls.Add(this.DBstat);
            this.Controls.Add(this.EnterButton);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.EmailBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "EnterForm";
            this.Text = "Вхід";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Button EnterButton;
        private System.Windows.Forms.RadioButton DBstat;
    }
}

