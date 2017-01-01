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
    public partial class InputForm : Form
    {        
        Configuration tFrm2;
        Test tFrm1;
        public string Input = "";
        public string status = "";

        public InputForm()
        {
            InitializeComponent();
        }
     
        public InputForm(Configuration t)
        {
            InitializeComponent();
            tFrm2 = t;
            status = "Configuration";
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
        }
        public InputForm(Test t)
        {
            tFrm1 = t;
        }
     
        public InputForm(Configuration t, string order)
        {
            InitializeComponent();
            tFrm2 = t;
            Input = order;
            textBox1.Text = Input;
            status = "training";
        }
        public InputForm(Test t, string order)
        {
            InitializeComponent();
            tFrm1 = t;
            Input = order;
            textBox1.Text = Input;
            status = "test";
        }
        private void btn_login_Click(object sender, EventArgs e)
        {
            Input = textBox1.Text;
            
            this.Close();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void InputForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (status.Equals("training"))
            {
                tFrm2.orderString = Input;
                tFrm2.training.inputListbox("선택 순서 생성 완료");
            }
            else
            {
                tFrm1.orderString = Input;
                tFrm1.inputListbox("선택 순서 생성 완료");
            }
                
            AutoClosingMessageBox.Show("입력이 완료되었습니다","순서 설정",500);
            
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }
    }
}
