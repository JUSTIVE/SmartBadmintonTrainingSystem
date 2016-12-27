namespace SmartBadmintonTrainingSystem
{
    partial class InputForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_input = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(6, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(385, 14);
            this.textBox1.TabIndex = 0;
            // 
            // btn_input
            // 
            this.btn_input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btn_input.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_input.ForeColor = System.Drawing.Color.White;
            this.btn_input.Location = new System.Drawing.Point(6, 31);
            this.btn_input.Name = "btn_input";
            this.btn_input.Size = new System.Drawing.Size(186, 62);
            this.btn_input.TabIndex = 7;
            this.btn_input.Text = "입력 완료";
            this.btn_input.UseVisualStyleBackColor = false;
            this.btn_input.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_reset.ForeColor = System.Drawing.Color.White;
            this.btn_reset.Location = new System.Drawing.Point(205, 31);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(186, 62);
            this.btn_reset.TabIndex = 8;
            this.btn_reset.Text = "다시 작성";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.panel2.Location = new System.Drawing.Point(6, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(385, 2);
            this.panel2.TabIndex = 36;
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(398, 105);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_input);
            this.Controls.Add(this.textBox1);
            this.Name = "InputForm";
            this.Text = "InputForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InputForm_FormClosed);
            this.Load += new System.EventHandler(this.InputForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_input;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Panel panel2;
    }
}