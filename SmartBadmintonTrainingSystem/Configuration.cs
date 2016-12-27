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
    public partial class Configuration : Form
    {
        public string orderString = "";
        List<int> OrderList = new List<int>();
        int cnt_of_test = 0;
        public string status = "";

        int colorNum = 0;

        public Training training;

        public Configuration()
        {
            InitializeComponent();
        }
        
        public void setSaving(bool r, bool g, bool b, bool y, string order)
        {
            if (r) checkBox1.Checked = true;
            if (b) checkBox2.Checked = true;
            if (g) checkBox3.Checked = true;
            if (y) checkBox4.Checked = true;

            orderString = order;
        }
        private void Configuration_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }
        public void setForm(Training t)
        {
            training = t;
        }
        
        public void setOrderList()
        {
            OrderList.Clear();
            string[] temp = orderString.Split('-');
            for (int i = 0; i < temp.Length; i++)
            {
                OrderList.Add(int.Parse(temp[i]));
            }
            cnt_of_test = OrderList.Count;
        }
        
        public void checkSelectColor()
        {
            colorNum = 0;
            if (checkBox1.Checked == true) colorNum++;
            if (checkBox2.Checked == true) colorNum++;
            if (checkBox3.Checked == true) colorNum++;
            if (checkBox4.Checked == true) colorNum++;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            training.orderString = this.orderString;
            training.r_flag = checkBox1.Checked;
            training.g_flag = checkBox3.Checked;
            training.b_flag = checkBox2.Checked;
            training.y_flag = checkBox4.Checked;
            training.hasSaving = true;
            AutoClosingMessageBox.Show("저장이 완료되었습니다!","환경설정",500);
            //MessageBox.Show("저장이 완료되었습니다!");
            training.inputListbox("환경 설정 완료");
            this.Close();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;

            orderString = "";
            status = "";

            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        public void setRandomList()
        {
            Random r = new Random();
            int number;
            List<int> rList = new List<int>();
            int[] checker = new int[8];
            int ccc = 0;
            rList.Clear();
            for (; ; )
            {
                if (rList.Count == 8) break;
                else
                {
                    number = r.Next(1, 9);
                    if (!rList.Contains(number))
                    {
                        rList.Add(number);
                    }
                }
            }
            orderString = "";
            for (int i = 0; i < 7; i++)
            {
                orderString += rList.ElementAt(i) + "-";
            }
            orderString += rList.ElementAt(7) + "";
            //AutoClosingMessageBox.Show("순서 설정이 완료되었습니다.", "순서 설정", 500);
        }

        private void btn_test_Click_1(object sender, EventArgs e)
        {
            if (status.Equals("random"))
            {
                training.inputListbox("랜덤 순서 설정 시작");
                //AutoClosingMessageBox.Show("랜덤 순서 설정 시작", "순서 설정", 500);
                //MessageBox.Show("랜덤 생성 시작");
                setRandomList();
                training.inputListbox("랜덤 순서 설정 완료");
            }
            else
            {
                training.inputListbox("선택 순서 설정 완료");
                MessageBox.Show("직접 생성 시작. 원하는 기둥의 번호를 입력해주세요!{(예)1-2-3-4-5}");
                if (orderString.Equals(""))
                {
                    InputForm ifrm = new InputForm(this);
                    ifrm.Show();
                }
                else
                {
                    InputForm ifrm = new InputForm(this, orderString);
                    ifrm.Show();
                }
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            checkSelectColor();
            if (colorNum > 3)
            {
                MessageBox.Show("3개의 색상까지 선택이 가능합니다!");
                checkBox1.Checked = false;
            }
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            checkSelectColor();
            if (colorNum > 3)
            {
                MessageBox.Show("3개의 색상까지 선택이 가능합니다!");
                checkBox3.Checked = false;
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkSelectColor();
            if (colorNum > 3)
            {
                MessageBox.Show("3개의 색상까지 선택이 가능합니다!");
                checkBox2.Checked = false;
            }
        }

        private void checkBox4_Click(object sender, EventArgs e)
        {
            checkSelectColor();
            if (colorNum > 3)
            {
                MessageBox.Show("3개의 색상까지 선택이 가능합니다!");
                checkBox4.Checked = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            status = "random";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            status = "select";
        }
    }
}
