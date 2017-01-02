namespace SmartBadmintonTrainingSystem
{
    partial class TestMode
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
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.combo_datePick = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_sectionresult = new System.Windows.Forms.Button();
            this.btn_sensorresult = new System.Windows.Forms.Button();
            this.btn_testresult = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.combo_TimePick = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_name
            // 
            this.txt_name.BackColor = System.Drawing.Color.White;
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_name.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_name.Location = new System.Drawing.Point(115, 49);
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            this.txt_name.Size = new System.Drawing.Size(133, 16);
            this.txt_name.TabIndex = 40;
            this.txt_name.TabStop = false;
            this.txt_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(42, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 23);
            this.label1.TabIndex = 39;
            this.label1.Text = "아이디";
            // 
            // combo_datePick
            // 
            this.combo_datePick.BackColor = System.Drawing.Color.White;
            this.combo_datePick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_datePick.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.combo_datePick.FormattingEnabled = true;
            this.combo_datePick.Location = new System.Drawing.Point(115, 88);
            this.combo_datePick.Name = "combo_datePick";
            this.combo_datePick.Size = new System.Drawing.Size(133, 23);
            this.combo_datePick.TabIndex = 42;
            this.combo_datePick.SelectedIndexChanged += new System.EventHandler(this.combo_datePick_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(23, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 23);
            this.label3.TabIndex = 41;
            this.label3.Text = "측정일자";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // plotView1
            // 
            this.plotView1.Location = new System.Drawing.Point(24, 130);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(528, 407);
            this.plotView1.TabIndex = 48;
            this.plotView1.Text = "plotView1";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.White;
            this.btn_start.FlatAppearance.BorderSize = 3;
            this.btn_start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_start.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btn_start.Location = new System.Drawing.Point(572, 457);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(199, 80);
            this.btn_start.TabIndex = 47;
            this.btn_start.Text = "테스트 시작";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.White;
            this.btn_exit.FlatAppearance.BorderSize = 3;
            this.btn_exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btn_exit.Location = new System.Drawing.Point(572, 371);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(199, 80);
            this.btn_exit.TabIndex = 46;
            this.btn_exit.Text = "테스트 모드 종료";
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_sectionresult
            // 
            this.btn_sectionresult.BackColor = System.Drawing.Color.White;
            this.btn_sectionresult.FlatAppearance.BorderSize = 3;
            this.btn_sectionresult.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_sectionresult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sectionresult.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_sectionresult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btn_sectionresult.Location = new System.Drawing.Point(572, 285);
            this.btn_sectionresult.Name = "btn_sectionresult";
            this.btn_sectionresult.Size = new System.Drawing.Size(199, 80);
            this.btn_sectionresult.TabIndex = 45;
            this.btn_sectionresult.Text = "구역별 결과확인";
            this.btn_sectionresult.UseVisualStyleBackColor = false;
            this.btn_sectionresult.Click += new System.EventHandler(this.btn_sectionresult_Click);
            // 
            // btn_sensorresult
            // 
            this.btn_sensorresult.BackColor = System.Drawing.Color.White;
            this.btn_sensorresult.FlatAppearance.BorderSize = 3;
            this.btn_sensorresult.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_sensorresult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sensorresult.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_sensorresult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btn_sensorresult.Location = new System.Drawing.Point(572, 199);
            this.btn_sensorresult.Name = "btn_sensorresult";
            this.btn_sensorresult.Size = new System.Drawing.Size(199, 80);
            this.btn_sensorresult.TabIndex = 44;
            this.btn_sensorresult.Text = "구간별 결과확인";
            this.btn_sensorresult.UseVisualStyleBackColor = false;
            this.btn_sensorresult.Click += new System.EventHandler(this.btn_sensorresult_Click);
            // 
            // btn_testresult
            // 
            this.btn_testresult.BackColor = System.Drawing.Color.White;
            this.btn_testresult.FlatAppearance.BorderSize = 3;
            this.btn_testresult.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.btn_testresult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_testresult.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_testresult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btn_testresult.Location = new System.Drawing.Point(572, 27);
            this.btn_testresult.Name = "btn_testresult";
            this.btn_testresult.Size = new System.Drawing.Size(199, 80);
            this.btn_testresult.TabIndex = 43;
            this.btn_testresult.Text = "월별 결과확인";
            this.btn_testresult.UseVisualStyleBackColor = false;
            this.btn_testresult.Click += new System.EventHandler(this.btn_testresult_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.panel2.Location = new System.Drawing.Point(115, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(133, 2);
            this.panel2.TabIndex = 60;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.panel1.Location = new System.Drawing.Point(115, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(133, 2);
            this.panel1.TabIndex = 61;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.button1.Location = new System.Drawing.Point(189, 509);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 25);
            this.button1.TabIndex = 62;
            this.button1.Text = "<";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.button2.Location = new System.Drawing.Point(400, 509);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 25);
            this.button2.TabIndex = 63;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 3;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(145)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.button3.Location = new System.Drawing.Point(572, 113);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(199, 80);
            this.button3.TabIndex = 64;
            this.button3.Text = "일별 결과확인";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.panel3.Location = new System.Drawing.Point(350, 105);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(133, 2);
            this.panel3.TabIndex = 67;
            // 
            // combo_TimePick
            // 
            this.combo_TimePick.BackColor = System.Drawing.Color.White;
            this.combo_TimePick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_TimePick.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.combo_TimePick.FormattingEnabled = true;
            this.combo_TimePick.Location = new System.Drawing.Point(350, 88);
            this.combo_TimePick.Name = "combo_TimePick";
            this.combo_TimePick.Size = new System.Drawing.Size(133, 23);
            this.combo_TimePick.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(258, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 23);
            this.label2.TabIndex = 65;
            this.label2.Text = "측정회차";
            // 
            // TestMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.combo_TimePick);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.plotView1);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_sectionresult);
            this.Controls.Add(this.btn_sensorresult);
            this.Controls.Add(this.btn_testresult);
            this.Controls.Add(this.combo_datePick);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.label1);
            this.Name = "TestMode";
            this.Text = "트레이닝 시스템 - 테스트모드";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestMode_FormClosed);
            this.Load += new System.EventHandler(this.TestMode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo_datePick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_sensorresult;
        private System.Windows.Forms.Button btn_sectionresult;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_start;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private System.Windows.Forms.Button btn_testresult;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox combo_TimePick;
        private System.Windows.Forms.Label label2;
    }
}