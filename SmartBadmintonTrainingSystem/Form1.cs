using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SmartBadmintonTrainingSystem
{
    public partial class Form1 : Form
    {
        singletonDB D_instance = singletonDB.getInstance();
        singletonUSER U_instance = singletonUSER.getInstance();
        MySqlCommand selectCommand = new MySqlCommand();
        string u_id, u_pw;
        bool flag = false;
        SelectForm frm;
        //TestMode frm;
        public Form1()
        {
            InitializeComponent();
            init();
        }
        public void init()
        {
            selectCommand.Connection = D_instance.conn;
            selectCommand.CommandText = "SELECT COUNT(id) from member where id=@id and pw=@pw";
            selectCommand.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            selectCommand.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
        }
        public void checkLogin(){


            singletonDB.IsOpen();

            selectCommand.Parameters[0].Value = u_id;
            selectCommand.Parameters[1].Value = u_pw;
            int myCount = Convert.ToInt32(selectCommand.ExecuteScalar());
            if (myCount == 1)
            {
                AutoClosingMessageBox.Show("로그인 성공!", "알림", 300);
                //MessageBox.Show("로그인 성공!");
                U_instance.setInstance(u_id,u_pw);
                
                frm = new SelectForm();
                //frm = new TestMode();
                this.Visible = false;
                frm.setForm(this);
                frm.Show();
                btn_login.Text = "로그아웃";
                flag = true;
            }
            else
            {
                AutoClosingMessageBox.Show("로그인 실패!", "알림", 300);
            }
        }
        public void checkLogout()
        {
            if (flag == true)
            {
                flag = false;
                btn_login.Text = "로그인";
                txt_id.Text = "";
                txt_pw.Text = "";
                U_instance.setInstance("", "");
            }
        }
        private void btn_join_Click(object sender, EventArgs e)
        {
            Join frm_join = new Join();
            frm_join.Show();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            u_id = txt_id.Text;
            u_pw = txt_pw.Text;


            if (u_id.Equals("") || u_pw.Equals(""))
            {
                MessageBox.Show("아이디 혹은 이름을 모두 입력해 주세요");
            }
            else
            {
                if (flag == false) checkLogin();
                else checkLogout();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            D_instance.conn.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void txt_pw_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Activate();
            txt_id.Select();
        }

        private void txt_id_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
