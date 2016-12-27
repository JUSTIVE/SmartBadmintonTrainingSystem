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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
namespace SmartBadmintonTrainingSystem
{
    public partial class TestResult_Section : Form
    {
        //Query
        MySqlCommand selectCommand = new MySqlCommand();
        MySqlCommand selectCommand3 = new MySqlCommand();
        userData uData;
        List<userData> uDataList = new List<userData>();

        //싱글톤클래스 참조
        singletonUSER u_instance = singletonUSER.getInstance();
        singletonDB d_instance = singletonDB.getInstance();

        //날짜
        List<string> uDateList = new List<string>();

        //측정결과
        TextBox[] t = new TextBox[4];

        List<PictureBox> pList = new List<PictureBox>();

        PlotModel pm = new PlotModel();

        bool flag = true;

        float[] data;

        string status = "";

        public TestResult_Section()
        {
            InitializeComponent();
            flag = false;
            t[0] = txt_front; t[1] = txt_left; t[2] = txt_right; t[3] = txt_back;
            pList.Clear();
            set_pList();
        }
        public void set_pList()
        {
            pList.Add(p1); pList.Add(p2);
            pList.Add(p3); pList.Add(p4);
            pList.Add(p5); pList.Add(p6);
            pList.Add(p7); pList.Add(p8);
            for (int i = 0; i < pList.Count; i++)
            {
                pList.ElementAt(i).Image = SmartBadmintonTrainingSystem.Properties.Resources.off_circle;
            }
        }
        public void add_combo_List(string date)
        {
            combo_resultDatePick.Items.Add(date);
        }
        public void init_SelectQuery()
        {
            selectCommand.Connection = d_instance.conn;
            selectCommand.CommandText = "SELECT * from information where id=@id and pw=@pw and date=@date and count=@count";
            selectCommand.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            selectCommand.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
            selectCommand.Parameters.Add("@date", MySqlDbType.VarChar, 25);
            selectCommand.Parameters.Add("@count", MySqlDbType.Int16);

            selectCommand3.Connection = d_instance.conn;
            selectCommand3.CommandText = "SELECT COUNT(count) from testcount where id=@id and pw=@pw and date=@date";
            selectCommand3.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            selectCommand3.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
            selectCommand3.Parameters.Add("@date", MySqlDbType.VarChar, 20);
        }
        public void exec_SelectQuery()
        {
            selectCommand.Parameters[0].Value = u_instance.uID;
            selectCommand.Parameters[1].Value = u_instance.uPW;
            selectCommand.Parameters[2].Value = combo_resultDatePick.SelectedItem;
            selectCommand.Parameters[3].Value = int.Parse(combo_TimePick.SelectedItem.ToString());
            MySqlDataReader rdr = selectCommand.ExecuteReader();

            uDataList.Clear();

            while (rdr.Read())
            {
                //MessageBox.Show("입력");
                uData = new userData();
                uData.setTime(float.Parse(rdr["time"].ToString()));
                uData.setNumber(int.Parse(rdr["number"].ToString()));
                uData.setType(int.Parse(rdr["type"].ToString()));
                uDataList.Add(uData);
            }
            //MessageBox.Show(uDataList.Count + "");
            rdr.Close();
        }
        public void set_today(string today)
        {
            combo_resultDatePick.SelectedItem = today;
        }
        public float SummaryOfTime(int number, int type)
        {
            float sum = 0.0f;

            for (int i = 0; i < uDataList.Count; i++)
            {
                uData = uDataList.ElementAt(i);                
                if (uData.number == number && uData.type == type)
                {                    
                    sum += uData.time;
                }
            }
            return sum;
        }
        public void setResult()
        {
            float front=0.0f;            float left=0.0f;            float right=0.0f;            float back=0.0f;

            for (int i = 0; i < 3; i++)
            {
                front += SummaryOfTime(i+1, 0);
                front += SummaryOfTime(i+1, 1);
            }
            for (int i = 5; i < 8; i++)
            {
                back += SummaryOfTime(i + 1, 0);
                back += SummaryOfTime(i + 1, 1);
            }
            left += SummaryOfTime(1, 0); left += SummaryOfTime(1, 1);
            left += SummaryOfTime(4, 0); left += SummaryOfTime(4, 1);
            left += SummaryOfTime(6, 0); left += SummaryOfTime(6, 1);

            right += SummaryOfTime(3, 0); right += SummaryOfTime(3, 1);
            right += SummaryOfTime(5, 0); right += SummaryOfTime(5, 1);
            right += SummaryOfTime(8, 0); right += SummaryOfTime(8, 1);
            
            t[0].Text = front.ToString();
            t[1].Text = left.ToString();
            t[2].Text = right.ToString();
            t[3].Text = back.ToString();
        }
        public void addTimeCombo()
        {
            selectCommand3.Parameters[0].Value = u_instance.uID;
            selectCommand3.Parameters[1].Value = u_instance.uPW;
            selectCommand3.Parameters[2].Value = combo_resultDatePick.SelectedItem.ToString();

            int TestCount = Convert.ToInt32(selectCommand3.ExecuteScalar());

            for (int i = 0; i < TestCount; i++)
            {
                combo_TimePick.Items.Add(i + 1);
            }
            combo_TimePick.SelectedIndex = 0;
        }
        private void TestResult_Section_Load(object sender, EventArgs e)
        {
            this.tBox_uName.Text = u_instance.uID;
            if (flag == false)
            {
                init_SelectQuery();
                addTimeCombo();
                exec_SelectQuery();
                setResult();
            }
            else
            {

            }                        

            pm.PlotType = PlotType.XY;
            var AxsX = new CategoryAxis();
            AxsX.Position = AxisPosition.Left;
            AxsX.Labels.Add("Forward");            
            AxsX.Labels.Add("Backward");
            AxsX.Labels.Add("Left Side");
            AxsX.Labels.Add("Right Side");
            
            var AxsY = new LinearAxis();
            AxsY.Position = AxisPosition.Bottom;
            AxsY.Maximum = 25;
            AxsY.Minimum = 0;
            AxsY.IntervalLength = 30;
            AxsY.MajorGridlineStyle = LineStyle.Dash;
            AxsY.MinorGridlineStyle = LineStyle.Dot;
            
            pm.Axes.Add(AxsX);
            pm.Axes.Add(AxsY);

            plotView1.Model = pm;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        public void clear_chart()
        {
            pm.Series.Clear();

            pm.InvalidatePlot(true);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            clear_chart();

            BarSeries bs = new BarSeries
            {
                FillColor = OxyColor.FromRgb(255, 87, 34),
            };
           
                
            bs.Items.Add(new BarItem(double.Parse(t[0].Text), 0));
            bs.Items.Add(new BarItem(double.Parse(t[3].Text), 1));
            bs.Items.Add(new BarItem(double.Parse(t[1].Text), 2));
            bs.Items.Add(new BarItem(double.Parse(t[2].Text), 3));                    

            pm.Series.Add(bs);

            pm.InvalidatePlot(true);
        }

        private void TestResult_Section_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

        }
        public void setImageOff()
        {
            for (int i = 0; i < pList.Count; i++)
            {
                pList.ElementAt(i).Image = SmartBadmintonTrainingSystem.Properties.Resources.off_circle;
            }
        }
        private void checkBox1_Click_1(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;

            if (checkBox1.Checked == false)
            {
                setImageOff();
            }
            else
            {
                setImageOff();
                pList.ElementAt(0).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                pList.ElementAt(1).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                pList.ElementAt(2).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            setImageOff();
            if (checkBox2.Checked)
            {
                pList.ElementAt(0).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                pList.ElementAt(3).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                pList.ElementAt(5).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
            }
            
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox4.Checked = false;
            setImageOff();
            if (checkBox3.Checked)
            {
                pList.ElementAt(2).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                pList.ElementAt(4).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                pList.ElementAt(7).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
            }
        }

        private void checkBox4_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox3.Checked = false;
            checkBox2.Checked = false;
            setImageOff();
            if (checkBox4.Checked)
            {
                pList.ElementAt(5).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                pList.ElementAt(6).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                pList.ElementAt(7).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            exec_SelectQuery();
            setResult();
            clear_chart();
        }

    }
}
