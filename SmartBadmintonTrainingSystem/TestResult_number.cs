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
    public partial class TestResult_number : Form
    {
        //Query
        MySqlCommand selectCommand = new MySqlCommand();
        MySqlCommand selectCommand3 = new MySqlCommand();
        userData uData;
        List<userData> uDataList = new List<userData>();
        string selected_date = "";
        //싱글톤클래스 참조
        singletonUSER u_instance = singletonUSER.getInstance();
        singletonDB d_instance = singletonDB.getInstance();
        
        //날짜
        List<string> uDateList = new List<string>();

        //측정결과
        TextBox[] t = new TextBox[24];

        PlotModel pm = new PlotModel();

        bool flag = true;

        float[] data;

        string status = "";

        public TestResult_number()
        {
            InitializeComponent();
            uDateList.Clear();
            flag = false;
            setTextBox();
            init_SelectQuery();
        }
        public void setTextBox()
        {
            t[0] = f1_t1; t[1] = f2_t1; t[2] = f3_t1; t[3] = s1_t1; t[4] = s2_t1; t[5] = b1_t1; t[6] = b2_t1; t[7] = b3_t1;
            t[8] = f1_t2; t[9] = f2_t2; t[10] = f3_t2; t[11] = s1_t2; t[12] = s2_t2; t[13] = b1_t2; t[14] = b2_t2; t[15] = b3_t2;
            t[16] = f1_t3; t[17] = f2_t3; t[18] = f3_t3; t[19] = s1_t3; t[20] = s2_t3; t[21] = b1_t3; t[22] = b2_t3; t[23] = b3_t3;
        }
        public void set_today(string today)
        {
            combo_resultDatePick.SelectedIndex = 0;            
        }
        
        public void setData(float[] temp)
        {
            data = temp;
            flag = true;
            
            for (int i = 0; i < 16; i++)
            {
                t[i].Text = temp[i].ToString();
            }
            for (int i = 0; i < 8; i++)
            {
                t[i + 16].Text = SummaryOfNumber(i) + "";
            }
            TotalTimeTextBox.Text = "\n" + SummaryOfAllTime() + "";
        }
        public void add_combo_List(string date)
        {
            combo_resultDatePick.Items.Add(date);
        }
        public void clear_tbox()
        {
            for (int i = 0; i < 24; i++)
            {
                t[i].Text = "";
            }
        }
        public void init_SelectQuery()
        {
            singletonDB.IsOpen();

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
            selectCommand.Parameters[3].Value = Int16.Parse(combo_TimePick.SelectedItem.ToString());
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
        public void setResult()
        {
            //t[0].Text = SummaryOfTime(1, 0)+"";
            for (int i = 0; i < 8; i++)
            {
                t[i].Text = SummaryOfTime(i + 1, 0) + "";
            }
            for (int i = 0; i < 8; i++)
            {
                t[i+8].Text = SummaryOfTime(i + 1, 1) + "";
            }
            for (int i = 0; i < 8; i++)
            {
                t[i + 16].Text = SummaryOfNumber(i) + "";
            }
            
            TotalTimeTextBox.Text = "\n"+SummaryOfAllTime() + "";
        }
        public float SummaryOfNumber(int number)
        {
            float sum = 0.0f;

            sum = float.Parse(t[number].Text) + float.Parse(t[number + 8].Text);

            return sum;
        }
        public float SummaryOfTime(int number, int type)
        {
            float sum=0.0f;
            
            for (int i = 0; i < uDataList.Count; i++)
            {
                uData = uDataList.ElementAt(i);
                //MessageBox.Show(uData.number + "/" + uData.type + "/" + uData.time);
                if (uData.number == number && uData.type == type)
                {
                    //MessageBox.Show("들어옴");
                    sum += uData.time;
                }
            }
            return sum;
        }
        public float SummaryOfAllTime()
        {
            float sum = 0.0f;
            for (int i = 0; i < 16; i++)
            {
                sum += float.Parse(t[i].Text);
            }
            return sum;
        }
        public void addTimeCombo()
        {
            combo_TimePick.Items.Clear();
            try
            {
                selectCommand3.Parameters[0].Value = u_instance.uID;
                selectCommand3.Parameters[1].Value = u_instance.uPW;
                selectCommand3.Parameters[2].Value = combo_resultDatePick.SelectedItem.ToString();
            }
            catch (System.Exception ex)
            {
                //AutoClosingMessageBox.Show("저장된 데이터가 없습니다!", "Error", 250);
                AutoClosingMessageBox.Show(ex.ToString(), "Error", 5000);
            }

            int TestCount = Convert.ToInt32(selectCommand3.ExecuteScalar());

            try
            {
                if (TestCount != 0)
                {
                    for (int i = 0; i < TestCount; i++)
                    {
                        combo_TimePick.Items.Add(i + 1);
                    }
                    combo_TimePick.SelectedIndex = 0;
                }
            }
            catch (System.Exception Exception)
            {
                AutoClosingMessageBox.Show("저장된 데이터가 없습니다!", "Error", 250);
            }
            
            
        }

        private void TestResult_number_Load(object sender, EventArgs e)
        {
            pm.PlotType = PlotType.XY;
            var AxsX = new CategoryAxis();
            AxsX.Position = AxisPosition.Left;
            AxsX.Labels.Add("F1");
            AxsX.Labels.Add("F2");
            AxsX.Labels.Add("F3");
            AxsX.Labels.Add("M1");
            AxsX.Labels.Add("M2");
            AxsX.Labels.Add("B1");
            AxsX.Labels.Add("B2");
            AxsX.Labels.Add("B3");            

            var AxsY = new LinearAxis();
            AxsY.Position = AxisPosition.Bottom;
            AxsY.Maximum = 20;
            AxsY.Minimum = 0;
            AxsY.IntervalLength = 30;
            AxsY.MajorGridlineStyle = LineStyle.Dash;
            AxsY.MinorGridlineStyle = LineStyle.Dot;

            
            pm.Axes.Add(AxsX);
            pm.Axes.Add(AxsY);
            
            plotView1.Model = pm;

            this.tBox_uName.Text = u_instance.uID;
            if (flag == false)
            {
                
                addTimeCombo();
                exec_SelectQuery();
                setResult();
            }
            else
            {

            }
            status = "total";
            checkBox3.Checked = true;
        }

        private void TestResult_number_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            clear_chart();

            BarSeries bs = new BarSeries
            {
                FillColor = OxyColor.FromRgb(255, 87, 34),                                                
            };
            switch(status){
                case "forward":
                    bs.Items.Add(new BarItem(double.Parse(f1_t1.Text), 0));
                    bs.Items.Add(new BarItem(double.Parse(f2_t1.Text), 1));
                    bs.Items.Add(new BarItem(double.Parse(f3_t1.Text), 2));
                    bs.Items.Add(new BarItem(double.Parse(s1_t1.Text), 3));
                    bs.Items.Add(new BarItem(double.Parse(s2_t1.Text), 4));
                    bs.Items.Add(new BarItem(double.Parse(b1_t1.Text), 5));
                    bs.Items.Add(new BarItem(double.Parse(b2_t1.Text), 6));
                    bs.Items.Add(new BarItem(double.Parse(b3_t1.Text), 7));
                    break;
                case "backward":
                    bs.Items.Add(new BarItem(double.Parse(f1_t2.Text), 0));
                    bs.Items.Add(new BarItem(double.Parse(f2_t2.Text), 1));
                    bs.Items.Add(new BarItem(double.Parse(f3_t2.Text), 2));
                    bs.Items.Add(new BarItem(double.Parse(s1_t2.Text), 3));
                    bs.Items.Add(new BarItem(double.Parse(s2_t2.Text), 4));
                    bs.Items.Add(new BarItem(double.Parse(b1_t2.Text), 5));
                    bs.Items.Add(new BarItem(double.Parse(b2_t2.Text), 6));
                    bs.Items.Add(new BarItem(double.Parse(b3_t2.Text), 7));
                    break;
                case "total":
                    bs.Items.Add(new BarItem(double.Parse(f1_t3.Text), 0));
                    bs.Items.Add(new BarItem(double.Parse(f2_t3.Text), 1));
                    bs.Items.Add(new BarItem(double.Parse(f3_t3.Text), 2));
                    bs.Items.Add(new BarItem(double.Parse(s1_t3.Text), 3));
                    bs.Items.Add(new BarItem(double.Parse(s2_t3.Text), 4));
                    bs.Items.Add(new BarItem(double.Parse(b1_t3.Text), 5));
                    bs.Items.Add(new BarItem(double.Parse(b2_t3.Text), 6));
                    bs.Items.Add(new BarItem(double.Parse(b3_t3.Text), 7));
                    break;
            }
            
            
            
            pm.Series.Add(bs);

            pm.InvalidatePlot(true);
        }

        public void clear_chart()
        {
            pm.Series.Clear();                       

            pm.InvalidatePlot(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear_tbox();
            clear_chart();
            exec_SelectQuery();
            setResult();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            //checkBox1.Checked = true;
            status = "forward";
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox3.Checked = false;
            //checkBox2.Checked = true;
            status = "backward";
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            //checkBox3.Checked = true;
            status = "total";
        }

        private void combo_resultDatePick_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }

        private void combo_resultDatePick_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            selected_date = combo_resultDatePick.SelectedItem.ToString();
            addTimeCombo();
        }

    }
}
