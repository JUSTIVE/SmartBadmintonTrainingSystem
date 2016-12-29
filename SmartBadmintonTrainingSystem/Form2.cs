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

        
        Training tFrm;
        List<int> targetPoles;
        Random randseed;

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Training t)
        {
            InitializeComponent();
            randseed = new Random();
            tFrm = t;
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

    }
}
