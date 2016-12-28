using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using MySql.Data.MySqlClient;
using System.IO;
using System.Media;

namespace SmartBadmintonTrainingSystem
{
    
    public partial class Test : Form
    {
        SoundPlayer sound;//시작,종료
        SoundPlayer sound2;//중앙감지

        int iTemp = 0;

        //Sensor HX Code
        byte[] index = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };//M1,M2,B3,B2,B1,F3,F2,F1
        byte[] color = { 0x01, 0x02, 0x03, 0x04, 0x00 };//Red,Green,Blue,Yellow
        byte start = 0x02;
        byte start_check = 0x01;
        byte end = 0x03;
        byte[] byteSendData = new byte[6];
        byte tmp2;

        int[] new_number = new int[8];

        bool threadFlag = false; // Thread Flag

        //float []T1 = new float[8];
        //float []T2 = new float[8];
        //float []T3 = new [8];

        //시리얼 포트 관련 변수 선언 시작
        SerialPort SP = new SerialPort();
        TestMode TM;
        string SP_name;
        string[] recv_buff = new string[3];//센서에서 들어오는 데이터 저장
        List<string> buffer = new List<string>();
        List<string> s_buffer = new List<string>();
        int buff_index = 0;
        bool port_set = false;
        //시리얼 포트 관련 변수 선언 종료

        //반복문
        Thread th1, th2, th3;
        ThreadStart ths;
        string status = "";

        //테스트 순서 관련 변수 선언 시작
        public string orderString = "";
        List<int> OrderList = new List<int>();
        int cnt_of_test = 0;
        int number;
        //테스트 순서 관련 변수 선언 종료

        //스탑워치
        Stopwatch sw = new Stopwatch();
        Stopwatch sw2 = new Stopwatch();
        Stopwatch sw3 = new Stopwatch();
        float Time,Time2;
        float[] T1 = new float[8];
        float[] T2 = new float[8];

        //중앙여부판단
        bool center_flag1 = true;
        bool center_flag2 = false;
        bool lrFlag = false; //좌우
        bool FbFlag = false; //상하
        //스윙여부판단
        bool swing_flag = true;
        bool is6byte = false;
        //현재 테스트 결과 저장 변수
        float[] Result = new float[16];

        //데이터베이스
        singletonDB instatnce = singletonDB.getInstance();
        MySqlCommand insertCommand = new MySqlCommand();
        MySqlCommand insertCommand2 = new MySqlCommand();       // count insert
        MySqlCommand selectCommand = new MySqlCommand();       // count insert

        //유저정보
        singletonUSER u_instance = singletonUSER.getInstance();

        public List<PictureBox> pList = new List<PictureBox>();
        string strRecData = "";
        int Size;
        
        //라벨String
        string openX = "컨트롤러 연결:X";
        string openO = "컨트롤러 연결:O";
        int TestCount = 1;
        public Test()
        {
            InitializeComponent();
            
            initInsertQuery();
            pList.Clear();
            setpList();
        }
        public void setpList()
        {
            pList.Add(p1);
            pList.Add(p2);
            pList.Add(p3);
            pList.Add(p4);
            pList.Add(p5);
            pList.Add(p6);
            pList.Add(p7);
            pList.Add(p8);
        }
        void SetSerialPort()
        {
            comboBox1.Items.Clear();
            label2.Text = openX;
            try
            {
                foreach (string comport in SerialPort.GetPortNames())
                {
                    comboBox1.Items.Add(comport);
                }
                comboBox1.SelectedIndex = 0;
            }
            catch
            {
                //MessageBox.Show("컨트롤러 USB를 연결해주세요!");
            }
        }
        public void selectDatabase()
        {
            selectCommand.Parameters[0].Value = u_instance.uID;
            selectCommand.Parameters[1].Value = u_instance.uPW;
            selectCommand.Parameters[2].Value = u_instance.LoginDate;


            TestCount = Convert.ToInt32(selectCommand.ExecuteScalar());
            TestCount++;
        }
        public void insertDatabase2()
        {
            singletonDB.IsOpen();
            insertCommand2.Connection = instatnce.conn;
            insertCommand2.Parameters[0].Value = u_instance.uID;
            insertCommand2.Parameters[1].Value = u_instance.uPW;
            insertCommand2.Parameters[2].Value = u_instance.LoginDate;
            insertCommand2.Parameters[3].Value = int.Parse(TestCount.ToString());
            insertCommand2.ExecuteNonQuery();
        }
        public void initInsertQuery()
        {
            insertCommand.Connection = instatnce.conn;
            insertCommand.CommandText = "INSERT INTO information(id, pw, date, time, number,type,count) VALUES(@id,@pw,@date,@time,@number,@type,@count)";
            insertCommand.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            insertCommand.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
            insertCommand.Parameters.Add("@date", MySqlDbType.VarChar, 25);
            insertCommand.Parameters.Add("@time", MySqlDbType.Float);
            insertCommand.Parameters.Add("@number", MySqlDbType.Int16);
            insertCommand.Parameters.Add("@type", MySqlDbType.Int16);
            insertCommand.Parameters.Add("@count", MySqlDbType.Int16);
            
            insertCommand2.CommandText = "INSERT INTO testcount(id,pw,date,count) VALUES(@id,@pw,@date,@count)";
            insertCommand2.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            insertCommand2.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
            insertCommand2.Parameters.Add("@date", MySqlDbType.VarChar, 25);            
            insertCommand2.Parameters.Add("@count", MySqlDbType.Int16);

            selectCommand.Connection = instatnce.conn;
            selectCommand.CommandText = "SELECT COUNT(count) from testcount where id=@id and pw=@pw and date=@date";
            selectCommand.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            selectCommand.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
            selectCommand.Parameters.Add("@date", MySqlDbType.VarChar, 20);
        }
        public void insertDatabase(string ID, string PW, float Time, int number, string DATE, int type,int count)
        {
            insertCommand.Parameters[0].Value = ID;
            insertCommand.Parameters[1].Value = PW;
            insertCommand.Parameters[2].Value = DATE;
            insertCommand.Parameters[3].Value = Time;
            insertCommand.Parameters[4].Value = number;
            insertCommand.Parameters[5].Value = type;
            insertCommand.Parameters[6].Value = count;
            insertCommand.ExecuteNonQuery();
        }
        public void set_FormTestMode(TestMode t)
        {
            this.TM = t;
        }
        private void Test_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            ////this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            SetSerialPort();
            radioButton1.Checked = true;
            orderString = "1-2-3-4-5-6-7-8";
            //orderString = "4-5";
            for (int i = 0; i < 8; i++)
            {
                setImageOff(i + 1);
            }
            sound = new SoundPlayer(SmartBadmintonTrainingSystem.Properties.Resources.beep_01a);
            sound2 = new SoundPlayer(SmartBadmintonTrainingSystem.Properties.Resources.beep_05);
        }

        private void Test_FormClosed(object sender, FormClosedEventArgs e)
        {
            TM.Visible = true;
            TM.refreshData();
            if (SP.IsOpen) SP.Close();
            try
            {
                th1.Abort();
                threadFlag = false;
            }
            catch (System.Exception ex)
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SP_name = comboBox1.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (port_set == false)
                {
                    SP.PortName = SP_name;
                    SP.BaudRate = 115200;
                    SP.DataBits = (int)8;
                    SP.Parity = Parity.None;
                    SP.StopBits = StopBits.One;//스탑비트 비트수(1 or 2)

                    
                    SP.Open();
                    if (SP.IsOpen)
                    {
                        setOnPort();
                    }
                }
                else
                {
                    setOffPort();
                }
            }
            catch (System.Exception ex)
            {
                AutoClosingMessageBox.Show("컨트롤러가 연결되어있지 않습니다!","Error",300);
            }
            
        }
        public void setNewNumber()
        {   
            new_number[0] = 7;
            new_number[1] = 6;
            new_number[2] = 5;
            new_number[3] = 1;
            new_number[4] = 0;
            new_number[5] = 4;
            new_number[6] = 3;
            new_number[7] = 2;
        }
        public void setOnPort()
        {
            try
            {
                buffer.Clear();
                port_set = true;
                Picture_Status.Image = SmartBadmintonTrainingSystem.Properties.Resources.green_circle;
                SP.DataReceived += new SerialDataReceivedEventHandler(EventDataReceived);
                AutoClosingMessageBox.Show("컨트롤러 연결 성공","포트",500);
                inputListbox("컨트롤러 연결 성공");
                label2.Text = openO;
                setByteSendData();
                //초기 이미지 확인
                send_packet(0, 0); send_packet(1, 0); send_packet(2, 2);
                send_packet(3, 2); send_packet(4, 2); send_packet(5, 1);
                send_packet(6, 1); send_packet(7, 1);

                setImageGreen(1); setImageGreen(2); setImageGreen(3);
                setImageRed(4); setImageRed(5);
                setImageBlue(6); setImageBlue(7); setImageBlue(8);

                //초기 그림 설정
                button1.Text = "연결종료";
            }
            catch (System.Exception ex)
            {

            }            
        }
        public void setRefreshPort()
        {            
            port_set = false;
            try
            {
                if (SP.IsOpen)
                {
                    SP.Close();
                    inputListbox("컨트롤러 연결 종료");
                }
            }
            catch
            {

            }
            button1.Text = "연결시도";
            label2.Text = openX;
            Picture_Status.Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
            for (int i = 0; i < 8; i++)
            {
                setImageOff(i + 1);
            }
            SetSerialPort();
        }


        public void setOffPort()
        {
            try
            {
                try
                {
                    send_packet(0, 4); send_packet(1, 4); send_packet(2, 4);
                    send_packet(3, 4); send_packet(4, 4); send_packet(5, 4);
                    send_packet(6, 4); send_packet(7, 4);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
                SP.Close();
                AutoClosingMessageBox.Show("컨트롤러 연결 종료", "포트", 500);
                inputListbox("컨트롤러 연결 종료");
                port_set = false;
                button1.Text = "연결시도";
                label2.Text = openX;
                Picture_Status.Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                for (int i = 0; i < 8; i++)
                {
                    setImageOff(i + 1);
                }
            }
            catch (System.Exception ex)
            {

            }
            
        }
        void EventDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Size = SP.BytesToRead;
            strRecData = "";
            byte[] buff = new byte[Size];
            SP.Read(buff, 0, Size);

            for (iTemp=0; iTemp < Size; iTemp++)
            {
                strRecData += buff[iTemp].ToString("X2") + " ";
            }
            if (!center_flag1)
            {
                if (!buffer.Contains(strRecData))
                {
                    buffer.Add(strRecData);
                }
            }
            if (!swing_flag) s_buffer.Add(strRecData);
        }

        public void setOrderList()
        {
            OrderList.Clear();
            string[] temp = orderString.Split('-');
            int inputTemp;
            for (int i = 0; i < temp.Length; i++)
            {
                inputTemp = int.Parse(temp[i]);
                OrderList.Add(inputTemp);
            }
            cnt_of_test = OrderList.Count;
        }

        public void setImageRed(int number)
        {
            pList.ElementAt(number - 1).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
        }
        public void setImageGreen(int number)
        {
            pList.ElementAt(number - 1).Image = SmartBadmintonTrainingSystem.Properties.Resources.green_circle;
        }
        public void setImageBlue(int number)
        {
            pList.ElementAt(number - 1).Image = SmartBadmintonTrainingSystem.Properties.Resources.blue_circle;
        }
        public void setImageOff(int number)
        {
            pList.ElementAt(number - 1).Image = SmartBadmintonTrainingSystem.Properties.Resources.off_circle;
        }
        public void clearBuff()
        {
            buffer.Clear();
        }
        public void isSwing(int number)
        {
            switch (number)
            {
                case 1:
                    if (s_buffer.Contains("02 01 80 00 81 03 "))
                    {
                        swing_flag = true;
                    }
                    break;
                case 2:
                    if (s_buffer.Contains("02 01 40 00 41 03 "))
                    {
                        swing_flag = true;
                    }
                    break;
                case 3:
                    if (s_buffer.Contains("02 01 20 00 21 03 "))
                    {
                        swing_flag = true;
                    }                    
                    break;
                case 4:
                    if (s_buffer.Contains("02 01 02 00 03 03 "))
                    {
                        swing_flag = true;
                    }
                    break;
                case 5:
                    if (s_buffer.Contains("02 01 01 00 02 03 "))
                    {
                        swing_flag = true;
                    }
                    break;
                case 6:
                    if (s_buffer.Contains("02 01 10 00 11 03 "))
                    {
                        swing_flag = true;
                    }
                    break;
                case 7:
                    if (s_buffer.Contains("02 01 08 00 09 03 "))
                    {
                        swing_flag = true;
                    }
                    break;
                case 8:
                    if (s_buffer.Contains("02 01 04 00 05 03 "))
                    {
                        swing_flag = true;
                    }
                    break;
            }
        }
        public void isCenter()
        {               
            if (buffer.Contains("02 01 00 40 41 03 ") && !lrFlag)
            {
                lrFlag = true;
            }
            if (buffer.Contains("02 01 00 80 81 03 ") && !FbFlag)
            {
                FbFlag = true;
            }
            
            if (lrFlag && FbFlag) center_flag1 = true;
        }
        public void setByteSendData()
        {
            byteSendData[0] = start;
            byteSendData[1] = start_check;
            byteSendData[5] = end;
        }     
        public void send_packet(int number, int number2)
        {
            try
            {
                byteSendData[2] = index[number];
                byteSendData[3] = color[number2];
                tmp2 = (byte)(byteSendData[1] + byteSendData[2] + byteSendData[3]);
                byteSendData[4] = tmp2;
                SP.Write(byteSendData, 0, 6);
            }
            catch (System.Exception e)
            {
                
            }
        }

        public void setRandomList()
        {
            Random r = new Random();
            int number;
            List<int> rList = new List<int>();            
            
            rList.Clear();
            for (; ; )
            {
                if (rList.Count == 8) break;
                else
                {
                    number = r.Next(1,9);
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
            orderString += rList.ElementAt(7)+"";            
        }

        public void thread_test()
        {

            singletonDB.IsOpen();
            selectDatabase();
            insertDatabase2();
            listBox1.Items.Add("테스트 시작");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            setOrderList();
                        
            AutoClosingMessageBox.Show("2초뒤 테스트를 시작합니다!", "안내", 2000);
            sound.Play();
            for (int i = 0; i < 8; i++)
            {
                setImageOff(i + 1);
            }
            Time = 0.0f;
            Time2 = 0.0f;
            for (int i = 0; i < cnt_of_test; i++)
            {
                number = OrderList.ElementAt(i);
                sw.Start();                sw2.Start();
                clearBuff();
                center.Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                send_packet(new_number[number - 1], 0);
                setImageRed(number);

                swing_flag = false;
                for (; ; )
                {
                    if (swing_flag)
                    {
                        break;
                    }
                    else isSwing(number);
                }
                sw.Stop();
                setImageOff(number);
                send_packet(new_number[number - 1], 4);
                
                clearBuff();
                lrFlag = false; FbFlag = false;
                if (number == 2 || number == 7) FbFlag = true;
                else if (number == 4 || number == 5) lrFlag = true;
                center_flag1 = false;
                for (; ; )
                {
                    if (center_flag1)
                    {                        
                        break;
                    }
                    else isCenter();
                }
                sw2.Stop();
                center.Image = SmartBadmintonTrainingSystem.Properties.Resources.green_circle;

                T1[number - 1] = float.Parse(sw.ElapsedMilliseconds.ToString()) * 0.001f;
                T2[number - 1] = float.Parse(sw2.ElapsedMilliseconds.ToString()) * 0.001f;
                T2[number - 1] = T2[number - 1] - T1[number - 1];

                sw.Reset();
                sw2.Reset();
            }
            AutoClosingMessageBox.Show("테스트 종료", "종료 알림", 250);
            AutoClosingMessageBox.Show("데이터 전송", "상태 알림", 250);
            threadFlag = false;
            for (int i = 0; i < 8; i++)
            {
                insertDatabase(u_instance.uID, u_instance.uPW, T1[i], i + 1, u_instance.LoginDate, 0, TestCount);
                insertDatabase(u_instance.uID, u_instance.uPW, T2[i], i + 1, u_instance.LoginDate, 1, TestCount);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            if (orderString.Equals(""))
            {

            }
            else if (!port_set)
            {
                AutoClosingMessageBox.Show("컨트롤러를 연결해주세요!", "Error", 300);
            }
            else
            {
                setNewNumber();
                send_packet(0, 4); send_packet(1, 4); send_packet(2, 4);
                send_packet(3, 4); send_packet(4, 4); send_packet(5, 4);
                send_packet(6, 4); send_packet(7, 4);
                setByteSendData();
                try
                {
                    if (!threadFlag)
                    {
                        ths = new ThreadStart(thread_test);
                        th1 = new Thread(ths);
                        th1.Start();
                        threadFlag = true;
                    }                    
                }
                catch (System.Exception ex)
                {
                    AutoClosingMessageBox.Show("테스트 에러 발생", "errer", 200);
                }
                      
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                th1.Abort();
                AutoClosingMessageBox.Show("테스트를 중지합니다!", "안내", 500);
                threadFlag = false;
                sound.Play();
            }
            catch (System.Exception ex)
            {

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            orderString = "8-7-6-5-4-3-2-1";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            orderString = "1-2-3-4-5-6-7-8";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_test_Click_1(object sender, EventArgs e)
        {
            if (status.Equals("random"))
            {
                listBox1.Items.Add("랜덤 순서 생성 시작");
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                //AutoClosingMessageBox.Show("랜덤 순서 설정 시작", "순서 설정", 500);
                //MessageBox.Show("랜덤 생성 시작");
                setRandomList();
                listBox1.Items.Add("랜덤 순서 생성 완료");
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            else
            {
                listBox1.Items.Add("선택 순서 생성 시작");
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                AutoClosingMessageBox.Show("직접 생성 시작. 원하는 기둥의 번호를 입력해주세요!{(예)1-2-3-4-5}","안내",1000);
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
        public void inputListbox(string data)
        {
            listBox1.Items.Add(data);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            status = "random";
        }

        private void radioButton2_CheckedChanged_2(object sender, EventArgs e)
        {
            status = "select";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            inputListbox("컨트롤러 새로고침");
            setRefreshPort();
            port_set = false;
        }
    }
}
