using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using MySql.Data.MySqlClient;


namespace SmartBadmintonTrainingSystem
{
    public partial class TrainingMode : Form
    {
        //싱글톤 객체
        singletonDB d_instance = singletonDB.getInstance();
        singletonUSER u_instance = singletonUSER.getInstance();
        MySqlCommand selectCommand = new MySqlCommand();
        MySqlCommand selectCommand2 = new MySqlCommand();

        List<string> u_DateList = new List<string>();
        List<float> u_dataList = new List<float>();
        string selected_date = "";
        string dateTemp = "";

        //그래프 적용
        LineSeries ser;
        ScatterSeries ser2;
        string year;
        string month;
        string day;
        SelectForm frm;
        TestResult_number tr_frm;
        PlotModel pm = new PlotModel();
        //날짜토탈데이터합산
        float sum_daily = 0.0f;
        public TrainingMode()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
            selectCommand.Connection = d_instance.conn;
            selectCommand.CommandText = "SELECT * from training where id=@id and pw=@pw order by date";
            selectCommand.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            selectCommand.Parameters.Add("@pw", MySqlDbType.VarChar, 20);

            selectCommand2.Connection = d_instance.conn;
            selectCommand2.CommandText = "SELECT * from training where id=@id and pw=@pw and date=@date";
            selectCommand2.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            selectCommand2.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
            selectCommand2.Parameters.Add("@date", MySqlDbType.VarChar, 20);

        }
        public void init_monthday()
        {
            string[] temp = u_instance.LoginDate.Split('-');
            year = temp[0];
            month = temp[1];
            day = temp[2];
        }
        public void setForm(SelectForm f)
        {
            frm = f;
        }

        private void TrainingMode_Load(object sender, EventArgs e)
        {
            init_monthday(); 
            pm.PlotType = PlotType.XY;
            var InrAxsX = new DateTimeAxis();
            InrAxsX.Position = AxisPosition.Bottom;
            InrAxsX.Title = year + "/" + month;
            InrAxsX.StringFormat = "dd";
            InrAxsX.Minimum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), 1));
            //InrAxsX.Minimum = DateTimeAxis.ToDouble(DateTime.DaysInMonth(int.Parse(year),int.Parse(month)));
            InrAxsX.Maximum = DateTimeAxis.ToDouble(new DateTime(int.Parse(year), int.Parse(month), DateTime.DaysInMonth(int.Parse(year), int.Parse(month))));
            //InrAxsX.IntervalLength = 1;
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

            plotView1.Model = pm;
        }

        private void TrainingMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.Visible = true;
        }
        public void clearParam()
        {
            selectCommand2.Parameters[0].Value = "";
            selectCommand2.Parameters[1].Value = "";
            selectCommand2.Parameters[2].Value = "";
        }
        public void SelectMonthTime()
        {
            pm.Series.Clear();
            u_dataList.Clear();
            clearParam();
            if (combo_datePick.Items.Count == 0)
            {
                MessageBox.Show("데이터가 존재하지 않아 월별 결과를 확인할 수 없습니다!");
            }
            else
            {
                ser = new LineSeries();
                ser2 = new ScatterSeries();

                for (int i = 0; i < combo_datePick.Items.Count; i++)
                {
                    selectCommand2.Parameters[0].Value = u_instance.uID;
                    selectCommand2.Parameters[1].Value = u_instance.uPW;
                    selectCommand2.Parameters[2].Value = combo_datePick.Items[i].ToString();
                    string[] dateTemp = combo_datePick.Items[i].ToString().Split('-');

                    MySqlDataReader rdr = selectCommand2.ExecuteReader();
                    while (rdr.Read())
                    {
                        sum_daily += float.Parse(rdr["time"].ToString());
                    }
                    rdr.Close();

                    ser.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(int.Parse(dateTemp[0]), int.Parse(dateTemp[1]), int.Parse(dateTemp[2]))), sum_daily));
                    ser2.Points.Add(new ScatterPoint(DateTimeAxis.ToDouble(new DateTime(int.Parse(dateTemp[0]), int.Parse(dateTemp[1]), int.Parse(dateTemp[2]))), sum_daily, 3));

                    sum_daily = 0.0f;
                }
                pm.Series.Add(ser);
                pm.Series.Add(ser2);
                //ser.MarkerType = MarkerType.Circle;
                pm.InvalidatePlot(true);
            }
        }

        public void setChart()
        {

        }

        private void btn_testresult_Click(object sender, EventArgs e)
        {
            singletonDB.IsOpen();            
            SelectMonthTime();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Training t = new Training();
            t.setForm(this);
            this.Hide();
            t.Show();
        }

        private void btn_sensorresult_Click(object sender, EventArgs e)
        {
            singletonDB.IsOpen();
            if (combo_datePick.Items.Count == 0)
            {
                MessageBox.Show("데이터가 존재하지 않아 구간별 결과를 확인할 수 없습니다!");
            }
            else if (selected_date.Equals(""))
            {
                MessageBox.Show("측정일자를 선택해주세요!");
            }
            else
            {
                tr_frm = new TestResult_number();

                for (int i = 0; i < combo_datePick.Items.Count; i++)
                {
                    tr_frm.add_combo_List(combo_datePick.Items[i].ToString());
                }
                //            MessageBox.Show(combo_datePick.SelectedItem.ToString());
                tr_frm.set_today(combo_datePick.SelectedItem.ToString());
                //TestResult_number tr_frm = new TestResult_number(combo_datePick.SelectedItem.ToString(), u_DateList);

                tr_frm.Show();
            }          
        }

        private void btn_sectionresult_Click(object sender, EventArgs e)
        {

        }

        private void plotView1_Click(object sender, EventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(month) == 1)
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
    }
}
