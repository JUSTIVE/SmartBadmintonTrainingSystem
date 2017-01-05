namespace SmartBadmintonTrainingSystem
{
    partial class Form2
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
            this.one = new System.Windows.Forms.Panel();
            this.two = new System.Windows.Forms.Panel();
            this.four = new System.Windows.Forms.Panel();
            this.redCheck = new System.Windows.Forms.CheckBox();
            this.greenCheck = new System.Windows.Forms.CheckBox();
            this.blueCheck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.PictureBox();
            this.three = new System.Windows.Forms.Panel();
            this.confirmButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.amount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.yellowCheck = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // one
            // 
            this.one.Location = new System.Drawing.Point(269, 129);
            this.one.Name = "one";
            this.one.Size = new System.Drawing.Size(114, 120);
            this.one.TabIndex = 112;
            // 
            // two
            // 
            this.two.Location = new System.Drawing.Point(456, 129);
            this.two.Name = "two";
            this.two.Size = new System.Drawing.Size(114, 120);
            this.two.TabIndex = 113;
            // 
            // four
            // 
            this.four.Location = new System.Drawing.Point(836, 129);
            this.four.Name = "four";
            this.four.Size = new System.Drawing.Size(114, 120);
            this.four.TabIndex = 115;
            // 
            // redCheck
            // 
            this.redCheck.AutoSize = true;
            this.redCheck.FlatAppearance.BorderSize = 0;
            this.redCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.redCheck.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.redCheck.Location = new System.Drawing.Point(269, 298);
            this.redCheck.Name = "redCheck";
            this.redCheck.Size = new System.Drawing.Size(73, 34);
            this.redCheck.TabIndex = 117;
            this.redCheck.Text = "RED";
            this.redCheck.UseVisualStyleBackColor = true;
            this.redCheck.CheckedChanged += new System.EventHandler(this.redCheck_CheckedChanged);
            // 
            // greenCheck
            // 
            this.greenCheck.AutoSize = true;
            this.greenCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.greenCheck.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.greenCheck.Location = new System.Drawing.Point(269, 340);
            this.greenCheck.Name = "greenCheck";
            this.greenCheck.Size = new System.Drawing.Size(100, 34);
            this.greenCheck.TabIndex = 118;
            this.greenCheck.Text = "GREEN";
            this.greenCheck.UseVisualStyleBackColor = true;
            this.greenCheck.CheckedChanged += new System.EventHandler(this.greenCheck_CheckedChanged);
            // 
            // blueCheck
            // 
            this.blueCheck.AutoSize = true;
            this.blueCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blueCheck.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.blueCheck.Location = new System.Drawing.Point(269, 382);
            this.blueCheck.Name = "blueCheck";
            this.blueCheck.Size = new System.Drawing.Size(83, 34);
            this.blueCheck.TabIndex = 119;
            this.blueCheck.Text = "BLUE";
            this.blueCheck.UseVisualStyleBackColor = true;
            this.blueCheck.CheckedChanged += new System.EventHandler(this.blueCheck_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "색상 지정 프로그램 설정";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Tomato;
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 72);
            this.panel1.TabIndex = 120;
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Tomato;
            this.closeButton.Image = global::SmartBadmintonTrainingSystem.Properties.Resources.close_button;
            this.closeButton.Location = new System.Drawing.Point(904, 12);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(46, 48);
            this.closeButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.closeButton.TabIndex = 121;
            this.closeButton.TabStop = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // three
            // 
            this.three.Location = new System.Drawing.Point(647, 129);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(114, 120);
            this.three.TabIndex = 114;
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirmButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.confirmButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.confirmButton.Location = new System.Drawing.Point(850, 477);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(104, 54);
            this.confirmButton.TabIndex = 121;
            this.confirmButton.Text = "확인";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.amount);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Location = new System.Drawing.Point(0, 72);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(241, 480);
            this.panel3.TabIndex = 122;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Tomato;
            this.panel2.Location = new System.Drawing.Point(27, 171);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(187, 3);
            this.panel2.TabIndex = 3;
            // 
            // amount
            // 
            this.amount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.amount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.amount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.amount.ForeColor = System.Drawing.Color.White;
            this.amount.Location = new System.Drawing.Point(27, 138);
            this.amount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(187, 32);
            this.amount.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(23, 90);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 25);
            this.label6.TabIndex = 1;
            this.label6.Text = "횟수";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(241, 62);
            this.button1.TabIndex = 0;
            this.button1.Text = "랜덤";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Tomato;
            this.label2.Location = new System.Drawing.Point(263, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 32);
            this.label2.TabIndex = 123;
            this.label2.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Tomato;
            this.label3.Location = new System.Drawing.Point(450, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 32);
            this.label3.TabIndex = 124;
            this.label3.Text = "2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Tomato;
            this.label4.Location = new System.Drawing.Point(641, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 32);
            this.label4.TabIndex = 125;
            this.label4.Text = "3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(836, 110);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 24);
            this.label5.TabIndex = 126;
            // 
            // yellowCheck
            // 
            this.yellowCheck.AutoSize = true;
            this.yellowCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yellowCheck.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.yellowCheck.Location = new System.Drawing.Point(269, 424);
            this.yellowCheck.Name = "yellowCheck";
            this.yellowCheck.Size = new System.Drawing.Size(95, 34);
            this.yellowCheck.TabIndex = 128;
            this.yellowCheck.Text = "Yellow";
            this.yellowCheck.UseVisualStyleBackColor = true;
            this.yellowCheck.CheckedChanged += new System.EventHandler(this.yellowCheck_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SmartBadmintonTrainingSystem.Properties.Resources.close_tomato;
            this.pictureBox1.Location = new System.Drawing.Point(833, 98);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 127;
            this.pictureBox1.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(971, 549);
            this.Controls.Add(this.yellowCheck);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.three);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.blueCheck);
            this.Controls.Add(this.greenCheck);
            this.Controls.Add(this.redCheck);
            this.Controls.Add(this.four);
            this.Controls.Add(this.two);
            this.Controls.Add(this.one);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "환경 설정";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel one;
        private System.Windows.Forms.Panel two;
        private System.Windows.Forms.Panel four;
        private System.Windows.Forms.CheckBox redCheck;
        private System.Windows.Forms.CheckBox greenCheck;
        private System.Windows.Forms.CheckBox blueCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox closeButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel three;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox yellowCheck;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox amount;
    }
}