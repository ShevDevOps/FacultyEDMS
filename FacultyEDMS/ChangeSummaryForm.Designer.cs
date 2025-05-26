namespace FacultyEDMS
{
    partial class ChangeSummaryForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnNoChange = new System.Windows.Forms.Button();
            this.labelSummary = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(243, 100);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(98, 34);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnNoChange
            // 
            this.btnNoChange.Location = new System.Drawing.Point(98, 100);
            this.btnNoChange.Name = "btnNoChange";
            this.btnNoChange.Size = new System.Drawing.Size(98, 35);
            this.btnNoChange.TabIndex = 1;
            this.btnNoChange.Text = "Не змінено";
            this.btnNoChange.UseVisualStyleBackColor = true;
            this.btnNoChange.Click += new System.EventHandler(this.btnNoChange_Click);
            // 
            // labelSummary
            // 
            this.labelSummary.AutoSize = true;
            this.labelSummary.Location = new System.Drawing.Point(13, 34);
            this.labelSummary.Name = "labelSummary";
            this.labelSummary.Size = new System.Drawing.Size(98, 16);
            this.labelSummary.TabIndex = 2;
            this.labelSummary.Text = "Опишіть зміни";
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(132, 27);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(296, 22);
            this.txtSummary.TabIndex = 3;
            // 
            // ChangeSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 183);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.labelSummary);
            this.Controls.Add(this.btnNoChange);
            this.Controls.Add(this.btnOk);
            this.Name = "ChangeSummaryForm";
            this.Text = "ChangeSummaryForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnNoChange;
        private System.Windows.Forms.Label labelSummary;
        private System.Windows.Forms.TextBox txtSummary;
    }
}