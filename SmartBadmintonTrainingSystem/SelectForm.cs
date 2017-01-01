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
    public partial class SelectForm : Form
    {
        singletonDB d_instance = singletonDB.getInstance();
        singletonUSER u_instance = singletonUSER.getInstance();
        Form1 frm;
        public SelectForm()
        {
            InitializeComponent();

        }
        private void SelectForm_Load(object sender, EventArgs e)
        {
            this.Activate();
            btn_test.Select();
            txt_id.Text = u_instance.uID;
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width-this.Size.Width)/2,(Screen.PrimaryScreen.Bounds.Height-this.Size.Height)/2);
        }
        public void setForm(Form1 f)
        {
            this.frm = f;
        }

        private void btn_training_Click(object sender, EventArgs e)
        {
            singletonDB.IsOpen();
            //MessageBox.Show("트레이닝 모드를 선택하셨습니다.");
            
            TrainingMode frm2 = new TrainingMode();
            frm2.setForm(this);
            this.Hide();
            frm2.Show();
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            singletonDB.IsOpen();
            //MessageBox.Show("테스트 모드를 선택하셨습니다.");
            
            TestMode frm2 = new TestMode();
            frm2.setForm(this);
            this.Hide();
            frm2.Show();
        }

        private void SelectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.Visible = true;
            frm.checkLogout();
        }

        private void txt_id_Enter(object sender, EventArgs e)
        {
            ActiveControl = btn_test;
        }
    }
}
