using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartBadmintonTrainingSystem
{
    public partial class Form2 : Form
    {
        public string input;
        bool isRand = false;
        int[] colorarray = new int[3];

        bool[] colorchecker;

        Training tFrm;
        List<int> targetPoles;
        Random randseed;

        enum ColorEnum {RED,GREEN,BLUE,NONE };

        public Form2()
        {
            InitializeComponent();

        }
        public Form2(Training t)
        {
            InitializeComponent();
            randseed = new Random();
            tFrm = t;
            First.BackColor = Color.FromArgb(150, 150, 150);
            Second.BackColor = Color.FromArgb(150, 150, 150);
            Banned.BackColor = Color.FromArgb(150, 150, 150);
            redCheck.Checked = false;
            greenCheck.Checked = false;
            blueCheck.Checked = false;
            colorarray = new int[3];
            colorchecker = new bool[3];
            for(int i = 0; i < 3; i++)
            {
                colorarray[i] = (int)ColorEnum.NONE;
                colorchecker[i] = false;
            }
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            ActiveControl = textBox4;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ActiveControl = textBox2;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isRand = !isRand;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void parser()
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox6.Text="";
            if (textBox2.Text.Equals(""))
            {
                AutoClosingMessageBox.Show("반복할 기둥을 입력해주세요", "반복 기둥", 500);
                ActiveControl = textBox2;
                return;
            }

            string temp = textBox2.Text;
            char[] delim = { ',' };
            string[] splitted=temp.Split(delim);

            
            if (textBox4.Text.Equals(""))
            {
                AutoClosingMessageBox.Show("반복 횟수를 입력해주세요","반복 횟수",500);
                ActiveControl = textBox4;
                return;
            }

            int amount = Int32.Parse(textBox4.Text);

            if (splitted.Count()>0)
            {
                for(int i = 0; i < amount; i++) { 
                    if (isRand)
                    {
                        for (int j = 0; j < splitted.Length; j++) { 
                            textBox6.Text += (splitted.ElementAt(randseed.Next(0,splitted.Length-1)))+",";
                        }
                    }
                    else
                    {
                        textBox6.Text += textBox2.Text + ",";
                    }
                }
            }
            //맨 마지막을 지우는 코드
            string tmp = textBox6.Text;
            tmp = tmp.Substring(0, tmp.Length - 1);
            textBox6.Clear();
            textBox6.Text = tmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tFrm.order_list = textBox6.Text;
            tFrm.actionBeam();
            this.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void redCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (redCheck.Checked)
            {
                if (!colorchecker[0])
                {
                    for(int i=0;i<3;i++)
                    {
                        if (colorarray[i] == (int)ColorEnum.NONE) { 
                            colorarray[i] = (int)ColorEnum.RED;
                            break;
                        }
                    }
                }
                colorchecker[0] = true;
            }
            else
            {
                if (colorchecker[0])
                {
                    

                }
                colorchecker[0] = false;
            }
        }
    }
}
