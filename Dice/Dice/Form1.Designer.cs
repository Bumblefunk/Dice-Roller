namespace Dice
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rollBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUpper = new System.Windows.Forms.TextBox();
            this.ListDisplay = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rollBtn
            // 
            this.rollBtn.Location = new System.Drawing.Point(25, 50);
            this.rollBtn.Name = "rollBtn";
            this.rollBtn.Size = new System.Drawing.Size(100, 50);
            this.rollBtn.TabIndex = 0;
            this.rollBtn.Text = "Roll";
            this.rollBtn.UseVisualStyleBackColor = true;
            this.rollBtn.Click += new System.EventHandler(this.rollBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 421);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // textBoxUpper
            // 
            this.textBoxUpper.Location = new System.Drawing.Point(150, 77);
            this.textBoxUpper.Name = "textBoxUpper";
            this.textBoxUpper.Size = new System.Drawing.Size(100, 23);
            this.textBoxUpper.TabIndex = 3;
            // 
            // ListDisplay
            // 
            this.ListDisplay.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ListDisplay.Enabled = false;
            this.ListDisplay.Location = new System.Drawing.Point(25, 189);
            this.ListDisplay.Multiline = true;
            this.ListDisplay.Name = "ListDisplay";
            this.ListDisplay.Size = new System.Drawing.Size(225, 214);
            this.ListDisplay.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(150, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 34);
            this.label2.TabIndex = 5;
            this.label2.Text = "how many side are on the die?";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 461);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListDisplay);
            this.Controls.Add(this.textBoxUpper);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rollBtn);
            this.Name = "Form";
            this.Text = "Dice Roller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button rollBtn;
        private Label label1;
        private TextBox textBoxUpper;
        private TextBox ListDisplay;
        private Label label2;
    }
}