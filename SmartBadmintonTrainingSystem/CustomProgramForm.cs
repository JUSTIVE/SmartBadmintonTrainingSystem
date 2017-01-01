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
    public partial class CustomProgramForm : Form
    {

        Dictionary<string, int> mapper = new Dictionary<string, int>{{"one",0},{"two",1},{"three",2},{"four",3},{"five",4},{"six",5},{"seven",6},{"eight",7}};
        bool isRand;
        Random randomSeed;
        int[] poleCount;
        public CustomProgramForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
            randomSeed = new Random();
            isRand = false;
            poleCount = Enumerable.Repeat(-1, 8).ToArray();
            {
                one_count.Text = 0+"";
                two_count.Text = 0 + "";
                three_count.Text = 0 + "";
                four_count.Text = 0 + "";
                five_count.Text = 0 + "";
                six_count.Text = 0 + "";
                seven_count.Text = 0 + "";
                eight_count.Text = 0 + "";
            }
        }
        public CustomProgramForm(int number,string Trainingset):this()
        {
            

        }

            private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            closeButton.BackColor = Color.Coral;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.BackColor = Color.Tomato;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            confirmButton.ForeColor = Color.Tomato;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            confirmButton.ForeColor = Color.FromArgb(66,66,66);
        }
        private void chevron_enter(object sender, EventArgs e)
        {
            PictureBox target = (PictureBox)sender;
            if(target!=null)
                target.BackColor = Color.Coral;
        }
        private void chevron_leave(object sender, EventArgs e) {
            PictureBox target = (PictureBox)sender;
            if (target != null)
                target.BackColor = Color.FromArgb(240, 240, 240);

        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void one_down_Click(object sender, EventArgs e)
        {
            
        }

        private void up_Click(object sender, EventArgs e)
        {
            char[] delim = { '_' };
            PictureBox temp = (PictureBox)sender;
            string[] key = temp.Name.Split(delim);
            int index = mapper[key[0]];
            
            poleCount[index]++;
            Label tmp = (Label)Controls.Find(key[0] + "_count", true).FirstOrDefault();
            if (tmp != null) { 
                tmp.Text = poleCount[index] + "";
            }
            else
            {
                one_count.Text = "값없음";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            temp.UseVisualStyleBackColor = false;
            isRand = !isRand;
            if (isRand)
            {
                temp.BackColor = Color.FromArgb(0x959595);
                temp.Text = "a";
            }
            else
            {
                temp.BackColor = Color.FromArgb(66,66,66);
                temp.Text = "b";
            }
        }
    }
}
