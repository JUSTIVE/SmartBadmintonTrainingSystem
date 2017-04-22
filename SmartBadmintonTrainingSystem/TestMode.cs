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
using System.IO.Ports;
namespace SmartBadmintonTrainingSystem
{
    public partial class TestMode : Form
    {
        singletonDB d_instance = singletonDB.getInstance();
        singletonUSER u_instance = singletonUSER.getInstance();
        MySqlCommand selectCommand = new MySqlCommand();
        MySqlCommand selectCommand2 = new MySqlCommand();
        MySqlCommand selectCommand3 = new MySqlCommand();
        MySqlCommand selectCommand4 = new MySqlCommand();
        PlotModel pm = new PlotModel();
        List<string> u_DateList = new List<string>();
        List<float> u_dataList = new List<float>();
        string selected_date = "";
        string dateTemp="";
        //그래프 적용
        LineSeries ser;
        ScatterSeries ser2;
        string year;
        string month;
        string day;
        SelectForm frm;
        TestResult_number tr_frm;
        TestResult_Section ts_frm;
        //날짜토탈데이터합산
        float sum_daily = 0.0f;

        public TestMode()
        {
            try
            {
                InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
            
                selectCommand.Connection = d_instance.conn;
                selectCommand.CommandText = "SELECT * from information where id=@id and pw=@pw order by date";
                selectCommand.Parameters.Add("@id", MySqlDbType.VarChar, 20);
                selectCommand.Parameters.Add("@pw", MySqlDbType.VarChar, 20);

                selectCommand2.Connection = d_instance.conn;
                selectCommand2.CommandText = "SELECT * from information where id=@id and pw=@pw and date=@date";
                selectCommand2.Parameters.Add("@id", MySqlDbType.VarChar, 20);
                selectCommand2.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
                selectCommand2.Parameters.Add("@date", MySqlDbType.VarChar, 20);

                selectCommand3.Connection = d_instance.conn;
                selectCommand3.CommandText = "SELECT COUNT(count) from testcount where id=@id and pw=@pw and date=@date";
                selectCommand3.Parameters.Add("@id", MySqlDbType.VarChar, 20);
                selectCommand3.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
                selectCommand3.Parameters.Add("@date", MySqlDbType.VarChar, 20);

                selectCommand4.Connection = d_instance.conn;
                selectCommand4.CommandText = "SELECT * from information where id=@id and pw=@pw and date=@date and count=@count";
                selectCommand4.Parameters.Add("@id", MySqlDbType.VarChar, 20);
                selectCommand4.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
                selectCommand4.Parameters.Add("@date", MySqlDbType.VarChar, 20);
                selectCommand4.Parameters.Add("@count", MySqlDbType.Int16);


                init_monthday();
                u_DateList.Clear();
            }
            catch(System.Exception ex)
            {
                
            }
        }

       
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {

        }

        public void setForm(SelectForm f)
        {
            this.frm = f;
        }
        public void init_monthday()
        {
            string[]temp=u_instance.LoginDate.Split('-');
            year = temp[0];
            month = temp[1];
            day = temp[2];
        }
        public void addTimeCombo(){
            combo_TimePick.Items.Clear();
            try
            {
                selectCommand3.Parameters[0].Value = u_instance.uID;
                selectCommand3.Parameters[1].Value = u_instance.uPW;
                selectCommand3.Parameters[2].Value = combo_datePick.SelectedItem.ToString();
            }
            catch (System.Exception ex)
            {
                AutoClosingMessageBox.Show("저장된 데이터가 없습니다!","Error",250);
            }

            int TestCount = Convert.ToInt32(selectCommand3.ExecuteScalar());

            try{
                if (TestCount != 0)
                {
                    for (int i = 0; i < TestCount; i++)
                    {
                        combo_TimePick.Items.Add(i + 1);
                    }
                    combo_TimePick.SelectedIndex = 0;
                }                
            }
            catch(System.Exception Exception){
                AutoClosingMessageBox.Show("저장된 데이터가 없습니다!", "Error", 250);
            }

        }

        public void refreshData()
        {
            pm.Axes.Clear();
            pm.PlotType = PlotType.XY;
            var InrAxsX = new DateTimeAxis();
            InrAxsX.Position = AxisPosition.Bottom;
            InrAxsX.Title = year + "/" + month;
            InrAxsX.StringFormat = "dd";
            InrAxsX.Minimum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), 1));
            //InrAxsX.Minimum = DateTimeAxis.ToDouble(DateTime.DaysInMonth(int.Parse(year),int.Parse(month)));
            InrAxsX.Maximum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), DateTime.DaysInMonth(int.Parse(year), int.Parse(month))));
            InrAxsX.IntervalLength = 10;
            InrAxsX.IntervalType = DateTimeIntervalType.Days;
            InrAxsX.MinorIntervalType = DateTimeIntervalType.Days;
            InrAxsX.MajorGridlineStyle = LineStyle.Solid;
            InrAxsX.MinorGridlineStyle = LineStyle.None;


            var InrAxsY = new LinearAxis();
            InrAxsY.Position = AxisPosition.Left;
            InrAxsY.Maximum = 100;
            InrAxsY.Minimum = 0;
            InrAxsX.IntervalLength = 10;
            InrAxsY.MajorGridlineStyle = LineStyle.Dash;
            InrAxsY.MinorGridlineStyle = LineStyle.Dot;

            pm.Axes.Add(InrAxsX);
            pm.Axes.Add(InrAxsY);

            selectCommand.Connection = singletonDB.getInstance().getConnection();
            txt_name.Text = u_instance.uID;
            selectCommand.Parameters[0].Value = u_instance.uID;
            selectCommand.Parameters[1].Value = u_instance.uPW;

            MySqlDataReader rdr = selectCommand.ExecuteReader();
            while (rdr.Read())
            {
                dateTemp = rdr["date"].ToString();
                if (combo_datePick.Items.Contains(dateTemp) == false)
                {
                    combo_datePick.Items.Add(dateTemp);
                }
            }
            rdr.Close();

            if (combo_datePick.Items.Count != 0)
            {
                combo_datePick.SelectedIndex = 0;
            }

            plotView1.Model = pm;
            addTimeCombo(); 
        }

        private void TestMode_Load(object sender, EventArgs e)
        {
            pm.Axes.Clear();
            pm.PlotType = PlotType.XY;
            var InrAxsX = new DateTimeAxis();
            InrAxsX.Position = AxisPosition.Bottom;
            InrAxsX.Title = year + "/" + month;
            InrAxsX.StringFormat = "dd";
            InrAxsX.Minimum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), 1));
            //InrAxsX.Minimum = DateTimeAxis.ToDouble(DateTime.DaysInMonth(int.Parse(year),int.Parse(month)));
            InrAxsX.Maximum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), DateTime.DaysInMonth(int.Parse(year), int.Parse(month))));
            InrAxsX.IntervalLength = 10;
            InrAxsX.IntervalType = DateTimeIntervalType.Days;
            InrAxsX.MinorIntervalType = DateTimeIntervalType.Days;
            InrAxsX.MajorGridlineStyle = LineStyle.Solid;
            InrAxsX.MinorGridlineStyle = LineStyle.None;


            var InrAxsY = new LinearAxis();
            InrAxsY.Position = AxisPosition.Left;
            InrAxsY.Maximum = 100;
            InrAxsY.Minimum = 0;
            InrAxsX.IntervalLength = 10;
            InrAxsY.MajorGridlineStyle = LineStyle.Dash;
            InrAxsY.MinorGridlineStyle = LineStyle.Dot;

            pm.Axes.Add(InrAxsX);
            pm.Axes.Add(InrAxsY);

            selectCommand.Connection = singletonDB.getInstance().getConnection();
            txt_name.Text = u_instance.uID;
            selectCommand.Parameters[0].Value = u_instance.uID;
            selectCommand.Parameters[1].Value = u_instance.uPW;

            MySqlDataReader rdr = selectCommand.ExecuteReader();
            while (rdr.Read())
            {
                dateTemp = rdr["date"].ToString();
                if (combo_datePick.Items.Contains(dateTemp) == false)
                {
                    combo_datePick.Items.Add(dateTemp);
                }
            }
            rdr.Close();

            if (combo_datePick.Items.Count != 0)
            {
                combo_datePick.SelectedIndex = 0;
            }

            plotView1.Model = pm;
            addTimeCombo(); 
        }

        public void clearParam()
        {
            selectCommand2.Parameters[0].Value = "";
            selectCommand2.Parameters[1].Value = "";
            selectCommand2.Parameters[2].Value = "";
        }
        public void SelectdateTime()
        {
            singletonDB.IsOpen();

            button1.Visible = false;
            button2.Visible = false;
            int TestCount;
            try{
                selectCommand3.Parameters[0].Value = u_instance.uID;
                selectCommand3.Parameters[1].Value = u_instance.uPW;
                selectCommand3.Parameters[2].Value = combo_datePick.SelectedItem.ToString();

                TestCount = Convert.ToInt32(selectCommand3.ExecuteScalar());
            }
            catch(System.Exception ex)
            {
                TestCount = 0;
            }
            

            if (TestCount == 0)
            {
                AutoClosingMessageBox.Show("데이터가 존재하지 않아 일별 결과를 확인할 수 없습니다!","Error",1000);                
            }
            else{
                pm.Axes.Clear();
            pm.PlotType = PlotType.XY;
            var AxsX = new CategoryAxis();
            AxsX.Position = AxisPosition.Left;

            for (int i = 0; i < TestCount; i++)
            {
                AxsX.Labels.Add((i+1) + " Time");
            }

            var AxsY = new LinearAxis();
            AxsY.Position = AxisPosition.Bottom;
            AxsY.Maximum = 50;
            AxsY.Minimum = 0;
            AxsY.IntervalLength = 30;
            AxsY.MajorGridlineStyle = LineStyle.Dash;
            AxsY.MinorGridlineStyle = LineStyle.Dot;


            pm.Axes.Add(AxsX);
            pm.Axes.Add(AxsY);

            plotView1.Model = pm;

            pm.Series.Clear();

            pm.InvalidatePlot(true);

            BarSeries bs = new BarSeries
            {
                FillColor = OxyColor.FromRgb(255, 87, 34),
            };

            float[] sum = new float[TestCount];
            for (int j = 0; j < TestCount; j++)
            {
                sum[j] = 0;
                selectCommand4.Parameters[0].Value = u_instance.uID;
                selectCommand4.Parameters[1].Value = u_instance.uPW;
                selectCommand4.Parameters[2].Value = combo_datePick.SelectedItem.ToString();
                selectCommand4.Parameters[3].Value = j+1;

                MySqlDataReader rdr = selectCommand4.ExecuteReader();
                while (rdr.Read())
                {
                    sum[j]+=float.Parse(rdr["time"].ToString());
                }
                rdr.Close();
            }

            for (int i = 0; i < TestCount; i++)
            {
                bs.Items.Add(new BarItem(double.Parse(sum[i].ToString()), i));
            }

            pm.Series.Add(bs);

            pm.InvalidatePlot(true);
            }
            
        }
        public void SelectMonthTime()
        {
            button1.Visible = true;
            button2.Visible = true;

            pm.Axes.Clear();
            pm.PlotType = PlotType.XY;
            var InrAxsX = new DateTimeAxis();
            InrAxsX.Position = AxisPosition.Bottom;
            InrAxsX.Title = year + "/" + month;
            InrAxsX.StringFormat = "dd";
            InrAxsX.Minimum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), 1));
            //InrAxsX.Minimum = DateTimeAxis.ToDouble(DateTime.DaysInMonth(int.Parse(year),int.Parse(month)));
            InrAxsX.Maximum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), DateTime.DaysInMonth(int.Parse(year), int.Parse(month))));
            InrAxsX.IntervalLength = 10;
            InrAxsX.IntervalType = DateTimeIntervalType.Days;
            InrAxsX.MinorIntervalType = DateTimeIntervalType.Days;
            InrAxsX.MajorGridlineStyle = LineStyle.Solid;
            InrAxsX.MinorGridlineStyle = LineStyle.None;


            var InrAxsY = new LinearAxis();
            InrAxsY.Position = AxisPosition.Left;
            InrAxsY.Maximum = 100;
            InrAxsY.Minimum = 0;
            InrAxsX.IntervalLength = 10;
            InrAxsY.MajorGridlineStyle = LineStyle.Dash;
            InrAxsY.MinorGridlineStyle = LineStyle.Dot;

            pm.Axes.Add(InrAxsX);
            pm.Axes.Add(InrAxsY);

            txt_name.Text = u_instance.uID;
            selectCommand.Parameters[0].Value = u_instance.uID;
            selectCommand.Parameters[1].Value = u_instance.uPW;

            MySqlDataReader rdr = selectCommand.ExecuteReader();
            while (rdr.Read())
            {
                dateTemp = rdr["date"].ToString();
                if (combo_datePick.Items.Contains(dateTemp) == false)
                {
                    combo_datePick.Items.Add(dateTemp);
                }
            }
            rdr.Close();

            if (combo_datePick.Items.Count != 0)
            {
                combo_datePick.SelectedIndex = 0;
            }

            plotView1.Model = pm;

            pm.Series.Clear();
            u_dataList.Clear();
            clearParam();
            if (combo_datePick.Items.Count == 0)
            {
                AutoClosingMessageBox.Show("데이터가 존재하지 않아 월별 결과를 확인할 수 없습니다!", "Error", 1000);
            }
            else
            {
                ser = new LineSeries();
                ser2 = new ScatterSeries();
                
                for (int i = 0; i < combo_datePick.Items.Count; i++)
                {
                    float min_daily = 0.0f;
                    string[] dateTemp = combo_datePick.Items[i].ToString().Split('-');
                    for (int j = 0; j < combo_TimePick.Items.Count; j++)
                    {
                        selectCommand4.Parameters[0].Value = u_instance.uID;
                        selectCommand4.Parameters[1].Value = u_instance.uPW;
                        selectCommand4.Parameters[2].Value = combo_datePick.Items[i].ToString();
                        selectCommand4.Parameters[3].Value = Int16.Parse(combo_TimePick.Items[j].ToString());                        

                        

                        rdr = selectCommand2.ExecuteReader();
                        while (rdr.Read())
                        {
                            //u_dataList.Add(float.Parse(rdr["time"].ToString()));
                            sum_daily += float.Parse(rdr["time"].ToString());
                        }
                        rdr.Close();
                        if (min_daily < sum_daily) min_daily = sum_daily;
                        
                        sum_daily = 0.0f;
                    }
                    ser.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(int.Parse(dateTemp[0]), int.Parse(dateTemp[1]), int.Parse(dateTemp[2]))), min_daily));
                    ser2.Points.Add(new ScatterPoint(DateTimeAxis.ToDouble(new DateTime(int.Parse(dateTemp[0]), int.Parse(dateTemp[1]), int.Parse(dateTemp[2]))), min_daily, 3));

                }
                pm.Series.Add(ser);
                pm.Series.Add(ser2);
                //ser.MarkerType = MarkerType.Circle;

                pm.InvalidatePlot(true);
            }
        }

        private void TestMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.Visible = true;
        }

        private void combo_datePick_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_date = combo_datePick.SelectedItem.ToString();
            addTimeCombo();
        }

        private void btn_testresult_Click(object sender, EventArgs e)
        {
            singletonDB.IsOpen();
            SelectMonthTime();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Test frm = new Test();
            frm.set_FormTestMode(this);
            this.Hide();
            frm.Select();
            frm.DesktopLocation = new Point(0,0);
            frm.Show();
        }

        private void btn_sensorresult_Click(object sender, EventArgs e)
        {
            singletonDB.IsOpen();
            if (combo_datePick.Items.Count == 0)
            {
                AutoClosingMessageBox.Show("데이터가 존재하지 않아 구간별 결과를 확인할 수 없습니다!", "Error", 800);                
            }
            else if (selected_date.Equals(""))
            {
                AutoClosingMessageBox.Show("측정일자를 선택해주세요!", "Error", 1000);
            }
            else
            {
                tr_frm = new TestResult_number();

                for (int i = 0; i < combo_datePick.Items.Count; i++)
                {
                    tr_frm.add_combo_List(combo_datePick.Items[i].ToString());
                }

                tr_frm.set_today(combo_datePick.SelectedItem.ToString());

                tr_frm.Show();
            }

        }

        private void btn_sectionresult_Click(object sender, EventArgs e)
        {
            singletonDB.IsOpen();
            if (combo_datePick.Items.Count == 0)
            {
                AutoClosingMessageBox.Show("데이터가 존재하지 않아 구역별 결과를 확인할 수 없습니다!", "Error", 1000);
            }
            else if (selected_date.Equals(""))
            {
                AutoClosingMessageBox.Show("측정일자를 선택해주세요!", "Error", 1000);                
            }
            else
            {
                ts_frm = new TestResult_Section();

                for (int i = 0; i < combo_datePick.Items.Count; i++)
                {
                    ts_frm.add_combo_List(combo_datePick.Items[i].ToString());
                }
                ts_frm.set_today(combo_datePick.SelectedItem.ToString());

                ts_frm.Show();
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (int.Parse(month)==1)
            {
                year = (int.Parse(year) - 1).ToString();
                month = "12";
            }
            else
            {
                month = (int.Parse(month) - 1).ToString();
            }

            pm.Axes.Clear();
            var InrAxsX = new DateTimeAxis();
            InrAxsX.Position = AxisPosition.Bottom;
            InrAxsX.Title = year+"/"+month;
            InrAxsX.StringFormat = "dd";
            InrAxsX.Minimum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), 1));
            //InrAxsX.Minimum = DateTimeAxis.ToDouble(DateTime.DaysInMonth(int.Parse(year),int.Parse(month)));
            InrAxsX.Maximum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), DateTime.DaysInMonth(int.Parse(year), int.Parse(month))));
            InrAxsX.IntervalLength = 10;
            InrAxsX.IntervalType = DateTimeIntervalType.Days;
            InrAxsX.MinorIntervalType = DateTimeIntervalType.Days;
            InrAxsX.MajorGridlineStyle = LineStyle.Solid;
            InrAxsX.MinorGridlineStyle = LineStyle.None;


            var InrAxsY = new LinearAxis();
            InrAxsY.Position = AxisPosition.Left;
            InrAxsY.Maximum = 100;
            InrAxsY.Minimum = 0;
            InrAxsX.IntervalLength = 10;
            InrAxsY.MajorGridlineStyle = LineStyle.Dash;
            InrAxsY.MinorGridlineStyle = LineStyle.Dot;

            pm.Axes.Add(InrAxsX);
            pm.Axes.Add(InrAxsY);
            pm.InvalidatePlot(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.Parse(month) == 12)
            {
                year = (int.Parse(year) + 1).ToString();
                month = "1";
            }
            else
            {
                month = (int.Parse(month) + 1).ToString();
            }

            pm.Axes.Clear();
            var InrAxsX = new DateTimeAxis();
            InrAxsX.Position = AxisPosition.Bottom;
            InrAxsX.Title = year + "/" + month;
            InrAxsX.StringFormat = "dd";
            InrAxsX.Minimum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), 1));
            //InrAxsX.Minimum = DateTimeAxis.ToDouble(DateTime.DaysInMonth(int.Parse(year),int.Parse(month)));
            InrAxsX.Maximum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), DateTime.DaysInMonth(int.Parse(year), int.Parse(month))));
            InrAxsX.IntervalLength = 10;
            InrAxsX.IntervalType = DateTimeIntervalType.Days;
            InrAxsX.MinorIntervalType = DateTimeIntervalType.Days;
            InrAxsX.MajorGridlineStyle = LineStyle.Solid;
            InrAxsX.MinorGridlineStyle = LineStyle.None;


            var InrAxsY = new LinearAxis();
            InrAxsY.Position = AxisPosition.Left;
            InrAxsY.Maximum = 100;
            InrAxsY.Minimum = 0;
            InrAxsX.IntervalLength = 10;
            InrAxsY.MajorGridlineStyle = LineStyle.Dash;
            InrAxsY.MinorGridlineStyle = LineStyle.Dot;

            pm.Axes.Add(InrAxsX);
            pm.Axes.Add(InrAxsY);
            pm.InvalidatePlot(true);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {            
            SelectdateTime();
        }
    }
}
