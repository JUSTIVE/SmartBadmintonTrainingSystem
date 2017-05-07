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
        List<userData> uDataList0 = new List<userData>();
        List<userData> uDataList1 = new List<userData>();
        string selected_date = "";
        //싱글톤클래스 참조
        singletonUSER u_instance = singletonUSER.getInstance();
        singletonDB d_instance = singletonDB.getInstance();
        
        //날짜
        List<string> uDateList = new List<string>();

        //측정결과
        TextBox[] t1 = new TextBox[36];
        TextBox[] t2 = new TextBox[36];
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
            t1[0] = f1_t1;  t1[1] = f2_t1;  t1[2] = f3_t1;  t1[3] = s1_t1;  t1[4] = s2_t1;  t1[5] = b1_t1;  t1[6] = b2_t1;  t1[7] = b3_t1;
            t1[8] = f1_t2;  t1[9] = f2_t2;  t1[10] = f3_t2; t1[11] = s1_t2; t1[12] = s2_t2; t1[13] = b1_t2; t1[14] = b2_t2; t1[15] = b3_t2;
            t1[16] = f1_t3; t1[17] = f2_t3; t1[18] = f3_t3; t1[19] = s1_t3; t1[20] = s2_t3; t1[21] = b1_t3; t1[22] = b2_t3; t1[23] = b3_t3;
            t1[24] = f1_t4;                 t1[25] = f3_t4;                                 t1[26] = b1_t4;                 t1[27] = b3_t4;
            t1[28] = f1_t5;                 t1[29] = f3_t5;                                 t1[30] = b1_t5;                 t1[31] = b3_t5;
            t1[32] = f1_t6;                 t1[33] = f3_t6;                                 t1[34] = b1_t6;                 t1[35] = b3_t6;

            t2[0] = f1_t7;  t2[1] = f2_t4;  t2[2] = f3_t7;  t2[3] = s1_t4;  t2[4] = s2_t4;  t2[5] = b1_t7;  t2[6] = b2_t4;  t2[7] = b3_t7;
            t2[8] = f1_t8;  t2[9] = f2_t5;  t2[10] = f3_t8; t2[11] = s1_t5; t2[12] = s2_t5; t2[13] = b1_t8; t2[14] = b2_t5; t2[15] = b3_t8;
            t2[16] = f1_t9; t2[17] = f2_t6; t2[18] = f3_t9; t2[19] = s1_t6; t2[20] = s2_t6; t2[21] = b1_t9; t2[22] = b2_t6; t2[23] = b3_t9;
            t2[24] = f1_t10;                 t2[25] = f3_t10;                                 t2[26] = b1_t10;              t2[27] = b3_t10;
            t2[28] = f1_t11;                 t2[29] = f3_t11;                                 t2[30] = b1_t11;              t2[31] = b3_t11;
            t2[32] = f1_t12;                 t2[33] = f3_t12;                                 t2[34] = b1_t12;              t2[35] = b3_t12;
        }
        public void set_today(string today)
        {
            combo_resultDatePick.SelectedIndex = 0;
        }
        public void add_combo_List(string date)
        {
            combo_resultDatePick.Items.Add(date);
        }
        public void clear_tbox()
        {
            for (int i = 0; i < 24; i++)
            {
                t1[i].Text = "";
            }
        }
        public void init_SelectQuery()
        {
            singletonDB.IsOpen();

            selectCommand.Connection = d_instance.conn;
            selectCommand.CommandText = "SELECT * from information where id=@id and pw=@pw and date=@date and count=@count and inning=@inning";
            selectCommand.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            selectCommand.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
            selectCommand.Parameters.Add("@date", MySqlDbType.VarChar, 25);
            selectCommand.Parameters.Add("@count", MySqlDbType.Int16);
            selectCommand.Parameters.Add("@inning", MySqlDbType.Int16);

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
            selectCommand.Parameters[4].Value = Int16.Parse(0+"");
            MySqlDataReader rdr = selectCommand.ExecuteReader();

            uDataList0.Clear();

            while (rdr.Read())
            {
                uDataList0.Add(uData = new userData((float)rdr["time"],
                    (int)rdr["number"],
                    (int)rdr["type"],
                    (int)rdr["inning"]));
            }
            rdr.Close();
            /////////////////////////////////////////////////////
            selectCommand.Parameters[0].Value = u_instance.uID;
            selectCommand.Parameters[1].Value = u_instance.uPW;
            selectCommand.Parameters[2].Value = combo_resultDatePick.SelectedItem;
            selectCommand.Parameters[3].Value = Int16.Parse(combo_TimePick.SelectedItem.ToString());
            selectCommand.Parameters[4].Value = Int16.Parse(1 + ""); 
            MySqlDataReader rdr1 = selectCommand.ExecuteReader();
            
            uDataList1.Clear();

            while (rdr1.Read())
            {
                uDataList1.Add(uData = new userData((float)rdr1["time"],
                    (int)rdr1["number"],
                    (int)rdr1["type"],
                    (int)rdr1["inning"]));
            }
            rdr1.Close();
        }
        public void setResult()
        {
            //inning - 0
            for (int i = 0; i < 8; i++)
                t1[i].Text = SummaryOfTime(i + 1, 0,0) + "";
            for(int i = 24; i < 28; i++)
                t1[i].Text = SummaryOfTime(i-15, 0,0) + "";

            for (int i = 0; i < 8; i++)
                t1[i+8].Text = SummaryOfTime(i + 1, 1,0) + "";
            for(int i = 28; i < 32; i++)
                t1[i].Text = SummaryOfTime(i-19, 1,0) + "";
            
            for (int i = 0; i < 8; i++)
                t1[i + 16].Text = SummaryOfNumber(i,0) + "";
            for (int i = 32; i < 36; i++)
                t1[i].Text = (float.Parse(t1[i-8].Text)+ float.Parse(t1[i-4].Text))+"";

            //inning - 1
            for (int i = 0; i < 8; i++)
                t2[i].Text = SummaryOfTime(i + 1, 0, 1) + "";
            for (int i = 24; i < 28; i++)
                t2[i].Text = SummaryOfTime(i - 15, 0, 1) + "";

            for (int i = 0; i < 8; i++)
                t2[i + 8].Text = SummaryOfTime(i + 1, 1, 1) + "";
            for (int i = 28; i < 32; i++)
                t2[i].Text = SummaryOfTime(i - 19, 1, 1) + "";

            for (int i = 0; i < 8; i++)
                t2[i + 16].Text = SummaryOfNumber(i,1) + "";
            for (int i = 32; i < 36; i++)
                t2[i].Text = (float.Parse(t2[i - 8].Text) + float.Parse(t2[i - 4].Text)) + "";


            TotalTimeTextBox.Text = "\n"+SummaryOfAllTime(0) + "";
            TotalTimeTextBox2.Text = "\n" + SummaryOfAllTime(1) + "";
        }
        public float SummaryOfNumber(int number, int inning)
        {
            float sum = 0.0f;
            if(inning==0)
                return sum = float.Parse(t1[number].Text) + float.Parse(t1[number + 8].Text);
            else
                return sum = float.Parse(t2[number].Text) + float.Parse(t2[number + 8].Text);

            
        }
        public float SummaryOfTime(int number, int type,int inning)
        {
            float sum=0.0f;

            if (inning == 0) { 
                for (int i = 0; i < uDataList0.Count; i++)
                {
                    uData = uDataList0.ElementAt(i);
                    if (uData.number == number && uData.type == type)
                    {
                        sum += uData.time;
                    }
                }
            }
            else
            {
               for (int i = 0; i < uDataList1.Count; i++)
                {
                    uData = uDataList1.ElementAt(i);
                    if (uData.number == number+12 && uData.type == type)
                    {
                        sum += uData.time;
                    }
                }
            }
            return sum;
        }
        public float SummaryOfAllTime(int inning)
        {
            float sum = 0.0f;
            for (int i = 16; i < 24; i++)
            {
                if(inning==0)
                    sum += float.Parse(t1[i].Text);
                else
                    sum += float.Parse(t2[i].Text);
            }
            for(int i = 32; i < 36; i++)
            {
                if (inning == 0)
                    sum += float.Parse(t1[i].Text);
                else
                    sum += float.Parse(t2[i].Text);
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
            AxsX.Labels.Add("F1_2");
            AxsX.Labels.Add("F1");
            
            AxsX.Labels.Add("F2");
            AxsX.Labels.Add("F3_2");
            AxsX.Labels.Add("F3");
            AxsX.Labels.Add("M1");
            AxsX.Labels.Add("M2");
            AxsX.Labels.Add("B1_2");
            AxsX.Labels.Add("B1");
            AxsX.Labels.Add("B2");
            AxsX.Labels.Add("B3_2");
            AxsX.Labels.Add("B3");
            AxsX.IsZoomEnabled = false;

            var AxsY = new LinearAxis();
            AxsY.Position = AxisPosition.Bottom;
            AxsY.Maximum = 10;
            AxsY.Minimum = 0;
            AxsY.IntervalLength = 30;
            AxsY.MajorGridlineStyle = LineStyle.Dash;
            AxsY.MinorGridlineStyle = LineStyle.Dot;
            AxsY.IsZoomEnabled = false;
            
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

            BarSeries bs0 = new BarSeries
            {
                FillColor = OxyColor.FromRgb(255, 87, 34),                                                
            };
            BarSeries bs1 = new BarSeries
            {
                FillColor = OxyColor.FromRgb(25, 189, 196),
            };
            switch(status){
                case "forward":
                    bs0.Items.Add(new BarItem(double.Parse(f1_t4.Text), 0));
                    bs0.Items.Add(new BarItem(double.Parse(f1_t1.Text), 1));
                    bs0.Items.Add(new BarItem(double.Parse(f2_t1.Text), 2));
                    bs0.Items.Add(new BarItem(double.Parse(f3_t4.Text), 3));
                    bs0.Items.Add(new BarItem(double.Parse(f3_t1.Text), 4));
                    bs0.Items.Add(new BarItem(double.Parse(s1_t1.Text), 5));
                    bs0.Items.Add(new BarItem(double.Parse(s2_t1.Text), 6));
                    bs0.Items.Add(new BarItem(double.Parse(b1_t4.Text), 7));
                    bs0.Items.Add(new BarItem(double.Parse(b1_t1.Text), 8));
                    bs0.Items.Add(new BarItem(double.Parse(b2_t1.Text), 9));
                    bs0.Items.Add(new BarItem(double.Parse(b3_t4.Text), 10));
                    bs0.Items.Add(new BarItem(double.Parse(b3_t1.Text), 11));

                    bs1.Items.Add(new BarItem(double.Parse(f1_t10.Text), 0));
                    bs1.Items.Add(new BarItem(double.Parse(f1_t7.Text), 1));
                    bs1.Items.Add(new BarItem(double.Parse(f2_t4.Text), 2));
                    bs1.Items.Add(new BarItem(double.Parse(f3_t10.Text), 3));
                    bs1.Items.Add(new BarItem(double.Parse(f3_t7.Text), 4));
                    bs1.Items.Add(new BarItem(double.Parse(s1_t4.Text), 5));
                    bs1.Items.Add(new BarItem(double.Parse(s2_t4.Text), 6));
                    bs1.Items.Add(new BarItem(double.Parse(b1_t10.Text), 7));
                    bs1.Items.Add(new BarItem(double.Parse(b1_t7.Text), 8));
                    bs1.Items.Add(new BarItem(double.Parse(b2_t4.Text), 9));
                    bs1.Items.Add(new BarItem(double.Parse(b3_t10.Text), 10));
                    bs1.Items.Add(new BarItem(double.Parse(b3_t7.Text), 11));
                    break;
                case "backward":
                    bs0.Items.Add(new BarItem(double.Parse(f1_t5.Text), 0));
                    bs0.Items.Add(new BarItem(double.Parse(f1_t2.Text), 1));
                    bs0.Items.Add(new BarItem(double.Parse(f2_t2.Text), 2));
                    bs0.Items.Add(new BarItem(double.Parse(f3_t5.Text), 3));
                    bs0.Items.Add(new BarItem(double.Parse(f3_t2.Text), 4));
                    bs0.Items.Add(new BarItem(double.Parse(s1_t2.Text), 5));
                    bs0.Items.Add(new BarItem(double.Parse(s2_t2.Text), 6));
                    bs0.Items.Add(new BarItem(double.Parse(b1_t2.Text), 7));
                    bs0.Items.Add(new BarItem(double.Parse(b1_t5.Text), 8));
                    bs0.Items.Add(new BarItem(double.Parse(b2_t2.Text), 9));
                    bs0.Items.Add(new BarItem(double.Parse(b3_t5.Text), 10));
                    bs0.Items.Add(new BarItem(double.Parse(b3_t2.Text), 11));

                    bs1.Items.Add(new BarItem(double.Parse(f1_t11.Text), 0));
                    bs1.Items.Add(new BarItem(double.Parse(f1_t8.Text), 1));
                    bs1.Items.Add(new BarItem(double.Parse(f2_t5.Text), 2));
                    bs1.Items.Add(new BarItem(double.Parse(f3_t11.Text), 3));
                    bs1.Items.Add(new BarItem(double.Parse(f3_t8.Text), 4));
                    bs1.Items.Add(new BarItem(double.Parse(s1_t5.Text), 5));
                    bs1.Items.Add(new BarItem(double.Parse(s2_t5.Text), 6));
                    bs1.Items.Add(new BarItem(double.Parse(b1_t11.Text), 7));
                    bs1.Items.Add(new BarItem(double.Parse(b1_t8.Text), 8));
                    bs1.Items.Add(new BarItem(double.Parse(b2_t5.Text), 9));
                    bs1.Items.Add(new BarItem(double.Parse(b3_t11.Text), 10));
                    bs1.Items.Add(new BarItem(double.Parse(b3_t8.Text), 11));
                    
                    break;
                case "total":
                    bs0.Items.Add(new BarItem(double.Parse(f1_t6.Text), 0));
                    bs0.Items.Add(new BarItem(double.Parse(f1_t3.Text), 1));
                    bs0.Items.Add(new BarItem(double.Parse(f2_t3.Text), 2));
                    bs0.Items.Add(new BarItem(double.Parse(f3_t6.Text), 3));
                    bs0.Items.Add(new BarItem(double.Parse(f3_t3.Text), 4));
                    bs0.Items.Add(new BarItem(double.Parse(s1_t3.Text), 5));
                    bs0.Items.Add(new BarItem(double.Parse(s2_t3.Text), 6));
                    bs0.Items.Add(new BarItem(double.Parse(b1_t6.Text), 7));
                    bs0.Items.Add(new BarItem(double.Parse(b1_t3.Text), 8));
                    bs0.Items.Add(new BarItem(double.Parse(b2_t3.Text), 9));
                    bs0.Items.Add(new BarItem(double.Parse(b3_t6.Text), 10));
                    bs0.Items.Add(new BarItem(double.Parse(b3_t3.Text), 11));

                    bs1.Items.Add(new BarItem(double.Parse(f1_t12.Text), 0));
                    bs1.Items.Add(new BarItem(double.Parse(f1_t9.Text), 1));
                    bs1.Items.Add(new BarItem(double.Parse(f2_t6.Text), 2));
                    bs1.Items.Add(new BarItem(double.Parse(f3_t12.Text), 3));
                    bs1.Items.Add(new BarItem(double.Parse(f3_t9.Text), 4));
                    bs1.Items.Add(new BarItem(double.Parse(s1_t6.Text), 5));
                    bs1.Items.Add(new BarItem(double.Parse(s2_t6.Text), 6));
                    bs1.Items.Add(new BarItem(double.Parse(b1_t12.Text), 7));
                    bs1.Items.Add(new BarItem(double.Parse(b1_t9.Text), 8));
                    bs1.Items.Add(new BarItem(double.Parse(b2_t6.Text), 9));
                    bs1.Items.Add(new BarItem(double.Parse(b3_t12.Text), 10));
                    bs1.Items.Add(new BarItem(double.Parse(b3_t9.Text), 11));
                    
                    break;
            }


            pm.Series.Add(bs1);
            pm.Series.Add(bs0);

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
            button2_Click(sender, e);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                status = "forward";
            }
            button2_Click(sender, e);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                status = "backward";
            }
            button2_Click(sender, e);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                status = "total";
            }
            button2_Click(sender, e);
        }
    }
}
