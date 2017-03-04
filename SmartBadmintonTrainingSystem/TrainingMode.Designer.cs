namespace SmartBadmintonTrainingSystem
{
    partial class TrainingMode
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
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.combo_datePick = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_sectionresult = new System.Windows.Forms.Button();
            this.btn_sensorresult = new System.Windows.Forms.Button();
            this.btn_testresult = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // plotView1
            // 
            this.plotView1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plotView1.Location = new System.Drawing.Point(262, 75);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(288, 196);
            this.plotView1.TabIndex = 58;
            this.plotView1.Text = "plotView1";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotView1.Click += new System.EventHandler(this.plotView1_Click);
            // 
            // combo_datePick
            // 
            this.combo_datePick.BackColor = System.Drawing.Color.Gainsboro;
            this.combo_datePick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_datePick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_datePick.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_datePick.FormattingEnabled = true;
            this.combo_datePick.Location = new System.Drawing.Point(234, 9);
            this.combo_datePick.Name = "combo_datePick";
            this.combo_datePick.Size = new System.Drawing.Size(133, 23);
            this.combo_datePick.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(181, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 51;
            this.label3.Text = "측정일자";
            // 
            // txt_name
            // 
            this.txt_name.BackColor = System.Drawing.Color.Gainsboro;
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_name.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_name.Location = new System.Drawing.Point(64, 10);
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            this.txt_name.Size = new System.Drawing.Size(100, 16);
            this.txt_name.TabIndex = 50;
            this.txt_name.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 49;
            this.label1.Text = "아이디";
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.Tomato;
            this.btn_start.FlatAppearance.BorderSize = 0;
            this.btn_start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.ForeColor = System.Drawing.Color.White;
            this.btn_start.Location = new System.Drawing.Point(0, 229);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(213, 58);
            this.btn_start.TabIndex = 6;
            this.btn_start.Text = "트레이닝 시작";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_exit.FlatAppearance.BorderSize = 0;
            this.btn_exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exit.ForeColor = System.Drawing.Color.White;
            this.btn_exit.Location = new System.Drawing.Point(0, 175);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(213, 48);
            this.btn_exit.TabIndex = 5;
            this.btn_exit.Text = "트레이닝 종료";
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_sectionresult
            // 
            this.btn_sectionresult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_sectionresult.FlatAppearance.BorderSize = 0;
            this.btn_sectionresult.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_sectionresult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sectionresult.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sectionresult.ForeColor = System.Drawing.Color.White;
            this.btn_sectionresult.Location = new System.Drawing.Point(0, 121);
            this.btn_sectionresult.Name = "btn_sectionresult";
            this.btn_sectionresult.Size = new System.Drawing.Size(213, 48);
            this.btn_sectionresult.TabIndex = 4;
            this.btn_sectionresult.Text = "구역별 결과확인";
            this.btn_sectionresult.UseVisualStyleBackColor = false;
            this.btn_sectionresult.Click += new System.EventHandler(this.btn_sectionresult_Click);
            // 
            // btn_sensorresult
            // 
            this.btn_sensorresult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_sensorresult.FlatAppearance.BorderSize = 0;
            this.btn_sensorresult.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_sensorresult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sensorresult.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sensorresult.ForeColor = System.Drawing.Color.White;
            this.btn_sensorresult.Location = new System.Drawing.Point(0, 67);
            this.btn_sensorresult.Name = "btn_sensorresult";
            this.btn_sensorresult.Size = new System.Drawing.Size(213, 48);
            this.btn_sensorresult.TabIndex = 3;
            this.btn_sensorresult.Text = "구간별 결과확인";
            this.btn_sensorresult.UseVisualStyleBackColor = false;
            this.btn_sensorresult.Click += new System.EventHandler(this.btn_sensorresult_Click);
            // 
            // btn_testresult
            // 
            this.btn_testresult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_testresult.FlatAppearance.BorderSize = 0;
            this.btn_testresult.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_testresult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_testresult.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_testresult.ForeColor = System.Drawing.Color.White;
            this.btn_testresult.Location = new System.Drawing.Point(0, 0);
            this.btn_testresult.Name = "btn_testresult";
            this.btn_testresult.Size = new System.Drawing.Size(213, 61);
            this.btn_testresult.TabIndex = 2;
            this.btn_testresult.Text = "월별 결과확인";
            this.btn_testresult.UseVisualStyleBackColor = false;
            this.btn_testresult.Click += new System.EventHandler(this.btn_testresult_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(64, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(100, 2);
            this.panel2.TabIndex = 59;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(234, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(133, 2);
            this.panel1.TabIndex = 36;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.button2.Location = new System.Drawing.Point(517, 244);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 25);
            this.button2.TabIndex = 65;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.button1.Location = new System.Drawing.Point(306, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 64;
            this.button1.Text = "<";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.panel3.Controls.Add(this.btn_testresult);
            this.panel3.Controls.Add(this.btn_sensorresult);
            this.panel3.Controls.Add(this.btn_sectionresult);
            this.panel3.Controls.Add(this.btn_exit);
            this.panel3.Controls.Add(this.btn_start);
            this.panel3.Location = new System.Drawing.Point(-1, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(213, 290);
            this.panel3.TabIndex = 66;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gainsboro;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.txt_name);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.combo_datePick);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(212, -1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(467, 48);
            this.panel4.TabIndex = 67;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(234, 27);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(133, 2);
            this.panel5.TabIndex = 60;
            // 
            // TrainingMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(593, 283);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.plotView1);
            this.Name = "TrainingMode";
            this.Text = "트레이닝 시스템 - 트레이닝 모드";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TrainingMode_FormClosed);
            this.Load += new System.EventHandler(this.TrainingMode_Load);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotView1;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_sectionresult;
        private System.Windows.Forms.Button btn_sensorresult;
        private System.Windows.Forms.Button btn_testresult;
        private System.Windows.Forms.ComboBox combo_datePick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
    }
}