namespace SmartBadmintonTrainingSystem
{
    partial class SelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectForm));
            this.txt_id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_test = new System.Windows.Forms.Button();
            this.btn_training = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_id
            // 
            this.txt_id.BackColor = System.Drawing.Color.White;
            this.txt_id.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_id.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.txt_id.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.txt_id.Location = new System.Drawing.Point(325, 30);
            this.txt_id.Name = "txt_id";
            this.txt_id.ReadOnly = true;
            this.txt_id.Size = new System.Drawing.Size(154, 15);
            this.txt_id.TabIndex = 0;
            this.txt_id.TabStop = false;
            this.txt_id.Enter += new System.EventHandler(this.txt_id_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(276, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "아이디";
            // 
            // btn_test
            // 
            this.btn_test.BackColor = System.Drawing.Color.White;
            this.btn_test.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_test.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn_test.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btn_test.Location = new System.Drawing.Point(279, 69);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(200, 62);
            this.btn_test.TabIndex = 35;
            this.btn_test.Text = "민첩성 테스트";
            this.btn_test.UseVisualStyleBackColor = false;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // btn_training
            // 
            this.btn_training.BackColor = System.Drawing.Color.White;
            this.btn_training.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_training.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_training.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn_training.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btn_training.Location = new System.Drawing.Point(279, 137);
            this.btn_training.Name = "btn_training";
            this.btn_training.Size = new System.Drawing.Size(200, 62);
            this.btn_training.TabIndex = 34;
            this.btn_training.Text = "민첩성 트레이닝";
            this.btn_training.UseVisualStyleBackColor = false;
            this.btn_training.Click += new System.EventHandler(this.btn_training_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(23, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(192, 178);
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            // 
            // SelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(514, 231);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_test);
            this.Controls.Add(this.btn_training);
            this.Name = "SelectForm";
            this.Text = "스마트 헬스케어 트레이닝 시스템";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelectForm_FormClosed);
            this.Load += new System.EventHandler(this.SelectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.Button btn_training;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}