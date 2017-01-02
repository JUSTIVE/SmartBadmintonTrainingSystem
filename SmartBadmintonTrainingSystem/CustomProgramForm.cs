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

        Training from=null;
        Dictionary<string, int> mapper = new Dictionary<string, int> { { "one", 0 }, { "two", 1 }, { "three", 2 }, { "four", 3 }, { "five", 4 }, { "six", 5 }, { "seven", 6 }, { "eight", 7 } };
        Dictionary<int, string> unmapper = new Dictionary<int, string> { { 0, "one" }, { 1, "two" }, { 2, "three" }, { 3,"four"}, { 4,"five"}, {5,"six"}, { 6,"seven"}, {7, "eight"} };
        bool isRand;
        Random randomSeed;
        int[] poleCount;
        string programName;
        int programnumber;

        public CustomProgramForm(Training t) {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
            from = t;
            randomSeed = new Random();
            programnumber = -1;
            isRand = false;
        }
        public CustomProgramForm(Training t,int number):this(t)//추가할때 호출하는 생성자 number 는 zero-base
        {
            programnumber = number;
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
        public CustomProgramForm(Training t,int number,CustomProgramType CPT):this(t,number)//수정할때 호출하는 생성자
        {

            char[] delim = { ',' };
            string[] splitter = CPT.trainingSet.Split(delim);
            for(int i = 0; i <splitter.Length;i++)
            {
                TextBox tmp = (TextBox)Controls.Find(unmapper[Int32.Parse(splitter[i]) - 1] + "_count", true).FirstOrDefault();
                tmp.Text = (Int32.Parse(tmp.Text)+1)+"";
            }
            updateTrainingSet();
        }

            private void pictureBox1_Click(object sender, EventArgs e)
        {
            from.flipCurtain();
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

            if (progNameTextBox.Text.Equals(""))
            {
                programName = "사용자 지정 프로그램";
            }
            else
            {
                programName = progNameTextBox.Text;
            }
            if (programnumber == -1) {
                CustomProgramType CPT = new CustomProgramType(programName, TrainingSet.Text);
                from.customProgramTypeList.Add(CPT);
            }
            else
            {
                CustomProgramType CPT = new CustomProgramType(programName, TrainingSet.Text);
                from.kill(programnumber);
                from.customProgramTypeList.Insert(programnumber,CPT);
            }
            from.flipCurtain();
            from.setUpProgramList();
            this.Close();
        }

        private void up_Click(object sender, EventArgs e)
        {
            char[] delim = {'_'};
            PictureBox temp = (PictureBox)sender;
            string[] key = temp.Name.Split(delim);
            int index = mapper[key[0]];
            
            poleCount[index]++;
            TextBox tmp = (TextBox)Controls.Find(key[0] + "_count", true).FirstOrDefault();
            if (tmp != null) { 
                tmp.Text = poleCount[index] + "";
                updateTrainingSet();
            }
            else
            {
                
            }
        }
        private void down_click(object sender, EventArgs e)
        {
            char[] delim = { '_' };
            PictureBox temp = (PictureBox)sender;
            string[] key = temp.Name.Split(delim);
            int index = mapper[key[0]];

            if (poleCount[index] > 0) { 
                poleCount[index]--;
            
                TextBox tmp = (TextBox)Controls.Find(key[0] + "_count", true).FirstOrDefault();
            
                if (tmp != null)
                {
                    tmp.Text = poleCount[index] + "";
                    if (poleCount[index] == 0)
                    {
                        temp.BackColor = Color.FromArgb(66, 66, 66);
                    }
                    else
                    {
                        temp.BackColor = Color.Tomato;
                    }
                    updateTrainingSet();
                }
                else
                {

                }
            }
        }

        void shuffle_array(int[] array)
        {
            for (int t = 0; t < array.Length; t++)
            {
                int tmp = array[t];
                int r = randomSeed.Next(t, array.Length);
                array[t] = array[r];
                array[r] = tmp;
            }
        }
        private void updateTrainingSet()//죄송합니다 급해서 하드코딩으로 때웠습니다
        {
            TrainingSet.Text = "";
            int indexer;
            if (isRand)
            {
                int size = Int32.Parse(one_count.Text) + Int32.Parse(two_count.Text) + Int32.Parse(three_count.Text) + Int32.Parse(four_count.Text) + Int32.Parse(five_count.Text) + Int32.Parse(six_count.Text) + Int32.Parse(seven_count.Text) + Int32.Parse(eight_count.Text);
                int[] array = new int[size];
                int masterindex = 0;

                for (int i = 0; i < 8; i++)
                {
                    TextBox target = (TextBox)Controls.Find(unmapper[i] + "_count", true).FirstOrDefault();
                    if (!target.Text.Equals(""))
                    {
                        indexer = Int32.Parse(target.Text);
                        if (indexer > 0)
                        {
                            for (int j = 0; j < indexer; j++)
                            {
                                array[masterindex++] = (i+1);
                            }
                        }
                    }
                }
                shuffle_array(array);
                for(int i=0;i<array.Length;i++)
                {
                    TrainingSet.Text += array[i] + ",";
                }
                string tmp = TrainingSet.Text;
                if (tmp.Length > 0)
                {
                    if (tmp[tmp.Length - 1] == ',')
                    {
                        tmp = tmp.Substring(0, tmp.Length - 1);
                        TrainingSet.Clear();
                        TrainingSet.Text = tmp;
                    }
                }
            }
            else {
                for(int i = 0; i < 8; i++)
                {
                    TextBox target = (TextBox)Controls.Find(unmapper[i]+"_count",true).FirstOrDefault();
                    if (!target.Text.Equals("")) {
                        indexer = Int32.Parse(target.Text);
                        if (indexer > 0)
                        {
                            for (int j = 0; j < indexer; j++)
                            {
                                TrainingSet.Text += (i+1)+",";
                            }
                        }
                    }
                }
                string tmp = TrainingSet.Text;
                if (tmp.Length>0) { 
                    if (tmp[tmp.Length-1] == ',')
                    {
                        tmp = tmp.Substring(0, tmp.Length - 1);
                        TrainingSet.Clear();
                        TrainingSet.Text = tmp;
                    }
                }
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            temp.UseVisualStyleBackColor = false;
            isRand = !isRand;
            if (isRand)
            {
                temp.BackColor = Color.FromArgb(120,120,120);
            }
            else
            {
                temp.BackColor = Color.FromArgb(66,66,66);
            }
            updateTrainingSet();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            ActiveControl = confirmButton;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.programName = progNameTextBox.Text;
        }

        private void count_TextChanged(object sender, EventArgs e)
        {
            updateTrainingSet();
        }
    }
}
