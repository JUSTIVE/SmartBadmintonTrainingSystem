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
