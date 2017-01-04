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
        int[] colorarray = new int[4];

        bool[] colorchecker;

        Dictionary<int, string> mapper = new Dictionary<int, string>{ {0,"one" },{ 1,"two"},{2,"three" },{ 3,"four"} };
        Training from;
        Random randseed;

        enum ColorEnum {RED,GREEN,BLUE,YELLOW,NONE };

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Training t):this()
        {
            randseed = new Random();
            from = t;
            
            redCheck.Checked = false;
            greenCheck.Checked = false;
            blueCheck.Checked = false;
            colorarray = new int[4];
            colorchecker = new bool[4];
            for(int i = 0; i < 4; i++)
            {
                colorarray[i] = (int)ColorEnum.NONE;
                colorchecker[i] = false;
            }
            check_updater();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        private void check_updater()
        {
            for(int i = 0; i < 4; i++)
            {
                Panel p=(Panel)Controls.Find(mapper[i],true).FirstOrDefault();
                switch (colorarray[i])
                {
                    case (int)ColorEnum.NONE:
                        p.BackColor = Color.FromArgb(66,66,66);
                        break;
                    case (int)ColorEnum.RED:
                        p.BackColor = Color.FromArgb(244, 67, 54);
                        break;
                    case (int)ColorEnum.GREEN:
                        p.BackColor = Color.FromArgb(139, 195, 74);
                        break;
                    case (int)ColorEnum.BLUE:
                        p.BackColor = Color.FromArgb(33,150,243);
                        break;
                    case (int)ColorEnum.YELLOW:
                        p.BackColor = Color.FromArgb(254, 213, 93);
                        break;
                }
            }
        }

        private void redCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (redCheck.Checked)
            {
                if (!colorchecker[0])
                {
                    for(int i=0;i<4;i++)//빈칸이 있으면
                    {
                        if (colorarray[i] == (int)ColorEnum.NONE) { 
                            colorarray[i] = (int)ColorEnum.RED;//그 칸을 칠해
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
                    for (int i = 0; i < 4; i++)//빈칸이 있으면
                    {
                        if (colorarray[i] == (int)ColorEnum.RED)
                        {
                            colorarray[i] = (int)ColorEnum.NONE;//그 칸을 지워
                            break;
                        }
                    }
                }
                colorchecker[0] = false;
            }
            check_updater();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            from.flipCurtain();
            this.Close();
        }

        private void greenCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (greenCheck.Checked)
            {
                if (!colorchecker[1])
                {
                    for (int i = 0; i < 4; i++)//빈칸이 있으면
                    {
                        if (colorarray[i] == (int)ColorEnum.NONE)
                        {
                            colorarray[i] = (int)ColorEnum.GREEN;//그 칸을 칠해
                            break;
                        }
                    }
                }
                colorchecker[1] = true;

            }
            else
            {
                if (colorchecker[1])
                {
                    for (int i = 0; i < 4; i++)//빈칸이 있으면
                    {
                        if (colorarray[i] == (int)ColorEnum.GREEN)
                        {
                            colorarray[i] = (int)ColorEnum.NONE;//그 칸을 지워
                            break;
                        }
                    }

                }
                colorchecker[1] = false;
            }
            check_updater();
        }

        private void blueCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (blueCheck.Checked)
            {
                if (!colorchecker[2])
                {
                    for (int i = 0; i < 4; i++)//빈칸이 있으면
                    {
                        if (colorarray[i] == (int)ColorEnum.NONE)
                        {
                            colorarray[i] = (int)ColorEnum.BLUE;//그 칸을 칠해
                            break;
                        }
                    }
                }
                colorchecker[2] = true;

            }
            else
            {
                if (colorchecker[2])
                {
                    for (int i = 0; i < 4; i++)//빈칸이 있으면
                    {
                        if (colorarray[i] == (int)ColorEnum.BLUE)
                        {
                            colorarray[i] = (int)ColorEnum.NONE;//그 칸을 지워
                            break;
                        }
                    }

                }
                colorchecker[2] = false;
            }
            check_updater();
        }

        private void yellowCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (yellowCheck.Checked)
            {
                if (!colorchecker[3])
                {
                    for (int i = 0; i < 4; i++)//빈칸이 있으면
                    {
                        if (colorarray[i] == (int)ColorEnum.NONE)
                        {
                            colorarray[i] = (int)ColorEnum.YELLOW;//그 칸을 칠해
                            break;
                        }
                    }
                }
                colorchecker[3] = true;

            }
            else
            {
                if (colorchecker[3])
                {
                    for (int i = 0; i < 4; i++)//빈칸이 있으면
                    {
                        if (colorarray[i] == (int)ColorEnum.YELLOW)
                        {
                            colorarray[i] = (int)ColorEnum.NONE;//그 칸을 지워
                            break;
                        }
                    }

                }
                colorchecker[3] = false;
            }
            check_updater();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            
            for(int i = 0; i < 4; i++)
            {
                if (colorarray[i] == (int)ColorEnum.NONE)
                {
                    AutoClosingMessageBox.Show("기둥을 설정해주세요", "기둥 설정", 500);
                    return;
                }
                if (amount.Text.Equals(""))
                {
                    AutoClosingMessageBox.Show("기둥을 설정해주세요", "기둥 설정", 500);
                    return;
                }
            }
            from.TCS = new TrainingColorSet(colorarray,Int32.Parse(amount.Text));
            from.isColor = true;
            this.Close();
            from.flipCurtain();
        }
    }
}
