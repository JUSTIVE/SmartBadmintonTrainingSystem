using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
namespace SmartBadmintonTrainingSystem
{
    public partial class Join : Form
    {
        singletonDB instatnce = singletonDB.getInstance();

        MySqlCommand insertCommand = new MySqlCommand();
        MySqlCommand selectCommand = new MySqlCommand();
        
        public Join()
        {
            InitializeComponent();            
        }

        private void Join_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            insertCommand.Connection = instatnce.conn;
            insertCommand.CommandText = "INSERT INTO member(id, pw, name, birth, height, weight, handtype) VALUES(@id,@pw,@name,@birth,@height,@weight,@handtype)";
            insertCommand.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            insertCommand.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
            insertCommand.Parameters.Add("@name", MySqlDbType.VarChar, 20);
            insertCommand.Parameters.Add("@birth", MySqlDbType.VarChar, 20);
            insertCommand.Parameters.Add("@height", MySqlDbType.Float);
            insertCommand.Parameters.Add("@weight", MySqlDbType.Float);
            insertCommand.Parameters.Add("@handtype", MySqlDbType.VarChar, 8);

            selectCommand.Connection = instatnce.conn;
            selectCommand.CommandText = "SELECT COUNT(id) from member where id=@id";
            selectCommand.Parameters.Add("@id", MySqlDbType.VarChar, 20);
        }
        public void clearText()
        {
            txt_height.Text = "";
            txt_weight.Text = "";
            txt_id.Text = "";
            txt_name.Text = "";
            txt_pw.Text = "";
            txt_pw2.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            clearText();
        }
        public bool check_pw()
        {
            bool flag = false;
            if (txt_pw.Text.Equals(txt_pw2.Text))
            {
                flag = true;
            }
            return flag;
        }
        private void btn_login_Click(object sender, EventArgs e)
        {
            singletonDB.IsOpen();
            string u_id = txt_id.Text; 

            selectCommand.Parameters[0].Value = u_id;
            //selectCommand.ExecuteScalar().ToString();
            int myCount = Convert.ToInt32(selectCommand.ExecuteScalar());

            singletonDB.IsOpen();

            if (myCount >= 1)
            {
                MessageBox.Show("아이디의 중복이 존재합니다. 다시 입력해주세요");
                txt_id.Text = "";
                this.Activate();
                txt_id.Select();
            }
            else if (!check_pw())
            {
                MessageBox.Show("패스워드가 일치하지 않습니다. 다시 입력해주세요");
            }
            else if (myCount < 1 && check_pw())
            {
                DateTime dt = dateTimePicker1.Value;
                string u_birth = string.Format("{0}-{1}-{2}", dt.Year, dt.Month, dt.Day);
                string u_handtype = comboBox1.SelectedItem.ToString();
                float u_height = float.Parse(txt_height.Text); float u_weight = float.Parse(txt_weight.Text); string u_name = txt_name.Text;
                string password = txt_pw.Text;

                insertCommand.Parameters[0].Value = u_id;
                insertCommand.Parameters[1].Value = password;
                insertCommand.Parameters[2].Value = u_name;
                insertCommand.Parameters[3].Value = u_birth;
                insertCommand.Parameters[4].Value = u_height;
                insertCommand.Parameters[5].Value = u_weight;
                insertCommand.Parameters[6].Value = u_handtype;

                insertCommand.ExecuteNonQuery();

                this.Close();
                MessageBox.Show("회원 가입이 완료되었습니다!");
            }
            
        }
    }
}
