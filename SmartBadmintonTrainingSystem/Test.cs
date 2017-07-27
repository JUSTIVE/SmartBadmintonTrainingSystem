#undef DEBUG
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


        //V4 values
        private int length_v4=-1;
        private byte[] v4_data;
        private enum Step {START,STARTCHECK,POLE,LIGHT,CHECKSUM,END };
        //
        int targetTestAmount;
        int[] poleCoder = { 7, 6, 5, 1, 0, 4, 3, 2 };
        int[] numberExteneder = {0, 1, 2, 3, 4, 5, 6, 7, 0, 2, 5, 7 };
        const int SAFE_SLEEP_TIME =75;
        //Received V3
        byte[] oldByte;
        //
        int current_test_index;
        //Serial
        int bufflen=0;
        byte[] buff_temp;
        
        
        bool buff_full = false;
        List<byte> d_buffer = new List<byte>();
        //fileio
        StreamWriter streamWriterIn;
        StreamWriter streamWriterOut;
        Stopwatch loggerTime;


        SoundPlayer sound;//시작,종료
        SoundPlayer sound2;//중앙감지

        int iTemp = 0;

        //Sensor HX Code
        byte[] index = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };//M1,M2,B3,B2,B1,F3,F2,F1
        byte[] color = { 0x00, 0x01, 0x02, 0x04, 0x03, 0x05,0x06,0x07};//Red,Green,Blue,Yellow,magenta,cyan;
        enum Color{OFF=0,RED,GREEN,YELLOW,BLUE,MAGENTA,CYAN,WHITE};
        byte start = 0x02;
        byte start_check = 0x01;
        byte end = 0x03;
        byte[] byteSendData = new byte[6];
        byte tmp2;

        //int[] new_number = new int[8];

        bool threadFlag = false; // Thread Flag

        int[] array;
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
        bool port_set = false;
        
        //시리얼 포트 관련 변수 선언 종료

        //반복문
        Thread th1;
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
        float[] T1 = new float[24];
        float[] T2 = new float[24];

        //중앙여부판단
        bool center_flag = true;
        
        bool lrFlag = false; //좌우
        bool FbFlag = false; //상하
        //스윙여부판단
        bool swing_flag = true;
        int swing_pole=-1;
        
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
        int Sizer;
        
        //라벨String
        string openX = "컨트롤러 연결:X";
        string openO = "컨트롤러 연결:O";
        int TestCount = 1;
        //constructor
        public Test()
        {
            InitializeComponent();
            v4_data = null;
            length_v4 = -1;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
            initInsertQuery();
            pList.Clear();
            setpList();
            setupStream();
            this.DoubleBuffered = true;
            test_24.Checked = true;
            targetTestAmount = 1;
            goalSwingAmount.Text = 24 + "";
            
        }
        void setupStream()
        {
            streamWriterIn = new StreamWriter("in.txt");
            streamWriterOut = new StreamWriter("out.txt");
            loggerTime = new Stopwatch();
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
            streamWriterIn.Close();
            streamWriterOut.Close();
        }
        ~Test(){
            
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
            try { 
                selectCommand.Parameters[0].Value = u_instance.uID;
                selectCommand.Parameters[1].Value = u_instance.uPW;
                selectCommand.Parameters[2].Value = u_instance.LoginDate;
                TestCount = Convert.ToInt32(selectCommand.ExecuteScalar());
                TestCount++;
            }
            catch(MySqlException e )
            {

            }
        }
        public void insertDatabase2()
        {
            try { 
                singletonDB.IsOpen();
                insertCommand2.Connection = instatnce.conn;
                insertCommand2.Parameters[0].Value = u_instance.uID;
                insertCommand2.Parameters[1].Value = u_instance.uPW;
                insertCommand2.Parameters[2].Value = u_instance.LoginDate;
                insertCommand2.Parameters[3].Value = int.Parse(TestCount.ToString());
                insertCommand2.ExecuteNonQuery();
            }
            catch(MySqlException e)
            {

            }
        }
        public void initInsertQuery()
        {
            insertCommand.Connection = instatnce.conn;
            insertCommand.CommandText = "INSERT INTO information(id, pw, date, time, number,type,count,inning) VALUES(@id,@pw,@date,@time,@number,@type,@count,@inning)";
            insertCommand.Parameters.Add("@id", MySqlDbType.VarChar, 20);
            insertCommand.Parameters.Add("@pw", MySqlDbType.VarChar, 20);
            insertCommand.Parameters.Add("@date", MySqlDbType.VarChar, 25);
            insertCommand.Parameters.Add("@time", MySqlDbType.Float);
            insertCommand.Parameters.Add("@number", MySqlDbType.Int16);
            insertCommand.Parameters.Add("@type", MySqlDbType.Int16);
            insertCommand.Parameters.Add("@count", MySqlDbType.Int16);
            insertCommand.Parameters.Add("@inning", MySqlDbType.Int16);

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
        public void insertDatabase(string ID, string PW, float Time, int number, string DATE, int type, int count,int inning)
        {           
            try {
                singletonDB.IsOpen();
                insertCommand.Connection = instatnce.conn;
                insertCommand.Parameters[0].Value = ID;
                insertCommand.Parameters[1].Value = PW;
                insertCommand.Parameters[2].Value = DATE;
                insertCommand.Parameters[3].Value = Time;
                insertCommand.Parameters[4].Value = number;
                insertCommand.Parameters[5].Value = type;
                insertCommand.Parameters[6].Value = count;
                insertCommand.Parameters[7].Value = inning;
                insertCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                AutoClosingMessageBox.Show(e.ToString(),"ERROR!!",5000);
            }
        }
        public void set_FormTestMode(TestMode t)
        {
            this.TM = t;
        }
        private int[] reshuffle(int[] texts)
        {
            int[] re_texts= texts;
            Random rd = new Random();
            for (int t = 0; t < 12; t++)
            {

                int r = rd.Next(0, 11);
                int tmp = re_texts[t];
                re_texts[t] = re_texts[r];
                re_texts[r] = tmp;
            }
            return re_texts;
        }
        private void Test_Load(object sender, EventArgs e)
        {
            SetSerialPort();
            random_order.Checked = true;
            orderString = "1-2-3-4-5-6-7-8";
            array = new int[12];

            for (int i = 0; i < 12; i++)
            {
                array[i] = i+1;
            }
            //for(int i = 0; i < 4; i++)
            //{
            //    array[i + 8] = (2 * i + 1) + (i > 1 ?1:0)+8;//(1,3,6,8)+8
            //}
            orderString = poleArrayToString(reshuffle(array));
            
            
            for (int i = 0; i < 8; i++)
            {
                setImageOff(i);
            }
            sound = new SoundPlayer(SmartBadmintonTrainingSystem.Properties.Resources.Beep1);
            sound2 = new SoundPlayer(SmartBadmintonTrainingSystem.Properties.Resources.Beep2);
        }
        private String poleArrayToString(int[] array)
        {
            String temp="";
            for (int i = 0; i < 12; i++)
            {
                temp += array[i].ToString();
                if (i < 11)
                {
                    temp += "-";
                }
            }
            return temp;
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
        
        public void setOnPort()
        {
            try
            {
                buffer.Clear();
                port_set = true;
                Picture_Status.Image = SmartBadmintonTrainingSystem.Properties.Resources.signal_green;
                SP.DataReceived += new SerialDataReceivedEventHandler(EventDataReceivedV4);
#if (DEBUG)
                AutoClosingMessageBox.Show("컨트롤러 연결 성공","포트",500);
                inputListbox("컨트롤러 연결 성공");
#endif
                label2.Text = openO;
                setByteSendData();
                //초기 이미지 확인
                //send_packet(0, color[1]); send_packet(1, color[1]);
                //send_packet(2, color[3]); send_packet(3, color[3]); send_packet(4, color[3]);
                //send_packet(5, color[2]); send_packet(6, color[2]); send_packet(7, color[2]);
                //while (true)
                //{
                //    for (int i = 0; i < 8; i++)
                //    {
                //        for (int j = 0; j < 8; j++)
                //        {
                //            //AutoClosingMessageBox.Show("aa", "aa", 100);
                //            send_packet(j, color[i]);
                //        }
                //        AutoClosingMessageBox.Show("aa", "aa", 500);
                //    }
                //}
                setImageGreen(0); setImageGreen(1); setImageGreen(2);
                setImageRed(3); setImageRed(4);
                setImageBlue(5); setImageBlue(6); setImageBlue(7);

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
#if(DEBUG)
                    inputListbox("컨트롤러 연결 종료");
#endif
                }
            }
            catch
            {

            }
            button1.Text = "연결시도";
            label2.Text = openX;
            Picture_Status.Image = SmartBadmintonTrainingSystem.Properties.Resources.signal_red;
            for (int i = 0; i < 8; i++)
            {
                setImageOff(i);
            }
            SetSerialPort();
        }

        public void setOffPort()
        {
            try
            {
                try
                {
                    for (int i = 0; i < 8; i++) { 
                        send_packet((byte)poleCoder[i], (byte)Color.OFF);
                        Thread.Sleep(SAFE_SLEEP_TIME);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
                SP.Close();
#if(DEBUG)
                AutoClosingMessageBox.Show("컨트롤러 연결 종료", "포트", 500);
                inputListbox("컨트롤러 연결 종료");
#endif
                port_set = false;
                button1.Text = "연결시도";
                label2.Text = openX;
                Picture_Status.Image = SmartBadmintonTrainingSystem.Properties.Resources.signal_red;
                for (int i = 0; i < 8; i++)
                {
                    setImageOff(i);
                }
            }
            catch (System.Exception ex)
            {

            }
        }
        void EventDataReceivedV3(object sender, SerialDataReceivedEventArgs e)
        {
            Sizer = SP.BytesToRead;
            byte[] buff = new byte[Sizer];
            SP.Read(buff, 0, Sizer);
            string buffing = "";
            for (int i = 0; i < Sizer; i++)
            {
                buffing += buff[i].ToString("X2") + " ";
            }
            //streamWriterIn.WriteLine("rawinput - " + buffing);
            
            while (true) {
                bool breaker = false;
                strRecData = "";
                if (oldByte != null)//붙이기만 할것
                {
                    byte[] temp = buff;
                    Sizer = oldByte.Length + buff.Length;
                    buff = new byte[Sizer];
                    oldByte.CopyTo(buff, 0);
                    temp.CopyTo(buff, oldByte.Length);
#if(DEBUG)
                    streamWriterIn.WriteLine(" Fixed");
                    inputListbox("fixed");
#endif
                    for (iTemp = 0; iTemp < Sizer; iTemp++)
                    {
                        strRecData += buff[iTemp].ToString("X2") + " ";
                    }
#if(DEBUG)
                    streamWriterIn.WriteLine(strRecData + " = pasted");
                    inputListbox(strRecData);
#endif
                    oldByte = null;
                }
                if (Sizer < 6)
                {
                    oldByte = new byte[Sizer];
                    buff.CopyTo(oldByte, 0);
                    break;
                }
                if (Sizer == 6) {
                    strRecData = "";
                    for (iTemp = 0; iTemp < Sizer; iTemp++)
                    {
                        strRecData += buff[iTemp].ToString("X2") + " ";
                    }
#if(DEBUG)
                    streamWriterIn.WriteLine(strRecData + loggerTime.Elapsed.ToString(@"mm\:ss\:FFFFFF")+" = right");
                    inputListbox(strRecData);
#endif
                    if (!center_flag)
                    {
                        if (!buffer.Contains(strRecData))
                        {
                            buffer.Add(strRecData);
                        }
                    }
                    if (!swing_flag)
                    {
                        if (!s_buffer.Contains(strRecData))
                        {
                            s_buffer.Clear();
                            s_buffer.Add(strRecData);
                            
                        }
                    }
                    //isSwing(number);
                    //isCenter();
                    breaker = true;
                    break;
                }
                else if(Sizer>6)//6보다 큰 경우 -center
                {
                    strRecData = "";
                    for (iTemp = 0; iTemp < 6; iTemp++)
                    {
                        strRecData += buff[iTemp].ToString("X2") + " ";
                    }
#if(DEBUG)
                    streamWriterIn.WriteLine(strRecData + loggerTime.Elapsed.ToString(@"mm\:ss\:FFFFFF")+" Overed");
                    inputListbox(strRecData+ "overed");
#endif
                    if (!center_flag)
                    {
                        if (!buffer.Contains(strRecData))
                        {
                            buffer.Add(strRecData);
                            isCenter();
                        }
                    }
                    if (!swing_flag)
                    {
                        if (!s_buffer.Contains(strRecData))
                        {
                            s_buffer.Clear();
                            s_buffer.Add(strRecData);
                            isSwing(number);
                        }
                    }
                    Sizer -= 6;
                    //버퍼를 줄임
                    byte[] temp = buff;
                    buff = new byte[Sizer];
                    for(int i = 6; i < Sizer+6; i++)
                    {
                        buff[i-6] = temp[i];
                    }
                }
                if (breaker)
                    break;
            }
            if (s_buffer.Count > 10)
                s_buffer.Clear();
        }

        //ongoing
        void EventDataReceivedV4(object sender, SerialDataReceivedEventArgs e)
        {
            //남아있는게 있는지 확인
            if (length_v4 <=0) {
                length_v4 = SP.BytesToRead;
                v4_data = new byte[length_v4];
                SP.Read(v4_data, 0, length_v4);
            }
            else//남아있는경우
            {
                length_v4 = SP.BytesToRead;
                int remain_lentgh = v4_data.Length;
                length_v4 += v4_data.Length;
                byte[] tempBuff = new byte[length_v4];
                v4_data.CopyTo(tempBuff, 0);
                v4_data = tempBuff;
                SP.Read(v4_data, remain_lentgh, length_v4 - remain_lentgh);
            }
#if (DEBUG)
            string tempstring = "";
            for(int i = 0; i < length_v4; i++)
            {
                tempstring += v4_data[i].ToString("X2");
            }
            inputListbox(tempstring);
#endif            
            while (length_v4 >= 6)
            {
                if (length_v4 >= 6)//사용
                {
                    if (!swing_flag)
                        IsSwing_v2();
                    if (!center_flag)
                        IsCenter_v2();
                    length_v4 -= 6;
                    if (length_v4 > 0) {//쓰고남은경우 
                        byte[] temp = new byte[length_v4];
                        for (int i = 6; i < length_v4 + 6; i++)
                            temp[i - 6] = v4_data[i];
                        v4_data = temp;
                    }
                }
                if (length_v4 == 0)//나머지가 없는 경우
                {
                    v4_data = null;
                    return;
                }
            }
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
        byte poleMapper(int number)
        {
            return (byte)poleCoder[numberExteneder[number-1]];
        }

        //input : 0~7 number 
        public void setImageRed(int number)
        {
            pList.ElementAt(numberExteneder[number]).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
        }
        public void setImageGreen(int number)
        {
            pList.ElementAt(numberExteneder[number]).Image = SmartBadmintonTrainingSystem.Properties.Resources.green_circle;
        }
        public void setImageBlue(int number)
        {
            pList.ElementAt(numberExteneder[number]).Image = SmartBadmintonTrainingSystem.Properties.Resources.blue_circle;
        }
        public void setImageOff(int number)
        {
            pList.ElementAt(numberExteneder[number]).Image = SmartBadmintonTrainingSystem.Properties.Resources.off_circle;
        }
        public void clearBuff()
        {
            buffer.Clear();
            s_buffer.Clear();
        }
        public int isSwing(int number)
        {
            //case 1
            if (s_buffer.Contains("02 01 08 01 0A 03 ")|| s_buffer.Contains("02 01 08 03 0C 03 "))
            {
                swing_pole = 1;
                if (number ==swing_pole)
                    swing_flag = true;
            }
            //case 2
            if (s_buffer.Contains("02 01 07 01 09 03 ")|| s_buffer.Contains("02 01 07 03 0B 03 "))
            {
                swing_pole = 2;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 3
            if (s_buffer.Contains("02 01 06 01 08 03 ")||s_buffer.Contains("02 01 06 03 0A 03 "))
            {
                swing_pole = 3;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 4
            if (s_buffer.Contains("02 01 02 01 04 03 ")|| s_buffer.Contains("02 01 02 03 06 03 "))
            {
                swing_pole = 4;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 5
            if (s_buffer.Contains("02 01 01 01 03 03 ")|| s_buffer.Contains("02 01 01 03 05 03 "))
            {
                swing_pole = 5;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 6
            if (s_buffer.Contains("02 01 05 01 07 03 ")|| s_buffer.Contains("02 01 05 03 09 03 "))
            {
                swing_pole = 6;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 7
            if (s_buffer.Contains("02 01 04 01 06 03 ")|| s_buffer.Contains("02 01 04 03 08 03 "))
            {
                swing_pole = 7;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 8
            if (s_buffer.Contains("02 01 03 01 05 03 ")|| s_buffer.Contains("02 01 03 03 07 03 "))
            {
                swing_pole = 8;
                if (number == swing_pole)
                    swing_flag = true;
            }
#if (DEBUG)
            //inputListbox("SWINGGG"+swing_pole + " "+number);
#endif
            return swing_pole;
        }
        public int IsSwing_v2()//compatible with DataReceived_v4
        {
            if ((v4_data[1] + v4_data[2] + v4_data[3] == v4_data[4])&&v4_data[5]==3)//checksum
            {
                if (v4_data[1] == 1)
                {
                    if((v4_data[3] == 3) || (v4_data[3] == 1)) { 
                        byte[] polemapping = { 5, 4, 8, 7, 6, 3, 2, 1 };
                        swing_pole = polemapping[v4_data[2] - 1];
                    }
                }
            }
            return swing_pole;
        }
        public void isCenter()
        {
            if (buffer.Contains("02 01 02 02 05 03 ") && !lrFlag)// 01 02 02 05
            {
                lrFlag = true;
            }
            else if(buffer.Contains("02 01 02 00 03 03")&&lrFlag)// 01 02 00 03
            {
                lrFlag = false;
            }
            
            if (buffer.Contains("02 01 07 02 0A 03 ") && !FbFlag)
            {
                FbFlag = true;
            }
            else if(buffer.Contains("02 01 07 00 08 03 ") && FbFlag)
            {
                FbFlag = false;
            }
            if (lrFlag && FbFlag) center_flag = true;
        }
        //
        public void IsCenter_v2()
        {
            //if (v4_data[1] + v4_data[2] + v4_data[3] == v4_data[4]) { 
            //Thread.Sleep(50);
                if (v4_data[2] == (byte)2)
                {
                    if (v4_data[3] == (byte)2)
                        lrFlag = true;
                    else if (v4_data[3] == (byte)0)
                        lrFlag = false;
                }
                if(v4_data[2]== (byte)7)
                {
                    if (v4_data[3] == (byte)2 )
                        FbFlag = true;
                    else if (v4_data[3] == (byte)0 )
                        FbFlag = false;
                }
                if (lrFlag && FbFlag) center_flag = true;
          //  }
        }
        public void setByteSendData()
        {
            byteSendData[0] = start;
            byteSendData[1] = start_check;
            byteSendData[5] = end;
        }     
        public void send_packet(int number, byte color)
        {
            try
            {
                byteSendData[2] = index[number];
                byteSendData[3] = color;
                tmp2 = (byte)(byteSendData[1] + byteSendData[2] + byteSendData[3]);
                byteSendData[4] = tmp2;
                SP.Write(byteSendData, 0, 6);
                string circle = BitConverter.ToString(byteSendData);
                circle.Replace("-", "");
                //streamWriterOut.WriteLine(circle+ loggerTime.Elapsed.ToString(@"mm\:ss\:FFFFFF"));
            }
            catch (System.Exception e)
            {
                Debug.WriteLine("exception at send_packet");
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
            //if (streamWriterIn != null) {
            //    streamWriterIn.Close();
            //    streamWriterIn = null;   
            //}
            int RealPole_index_24 = 0;
            //streamWriterIn = new StreamWriter("in.txt");
            singletonDB.IsOpen();
            selectDatabase();
            insertDatabase2();
            listBox1.Items.Add("테스트 시작");
            setOrderList();
            center.Image = Properties.Resources._5_image;
            sound.Play();
            Thread.Sleep(1000);
            center.Image = Properties.Resources._4_image;
            sound.Play();
            Thread.Sleep(1000);
            center.Image = Properties.Resources._3_image;
            sound.Play();
            Thread.Sleep(1000);
            center.Image = Properties.Resources._2_image;
            sound.Play();
            Thread.Sleep(1000);
            center.Image = Properties.Resources._1_image;
            sound.Play();
            Thread.Sleep(1000);
            sound2.Play();
            for (int i = 0; i < 8; i++)
            {
                setImageOff(i);
            }
            Time = 0.0f;
            Time2 = 0.0f;
            for (int n=0;n<targetTestAmount+1;n++) {
                orderString = poleArrayToString(reshuffle(array));
                inputListbox(orderString);
                //setOrderList();
                for (current_test_index = 0; current_test_index < cnt_of_test;)
                {
                    
                    number = OrderList.ElementAt(current_test_index);
                    sw.Start();                sw2.Start();
                    clearBuff();
                    setImageRed(number - 1);
                    send_packet(poleMapper(number), (byte)Color.RED);
                    
#if (DEBUG)
                    streamWriterIn.WriteLine(number + " = number");
                    inputListbox(number + " = number");
#endif
                    swing_flag = false;
                    swing_pole = -1;
                    while(!swing_flag)
                    {
                        //input  == 1~8
                        //if (IsSwing_v2((numberExteneder[number-1]) + 1) == numberExteneder[number-1] + 1)
                        //if(swing_pole== numberExteneder[number - 1] + 1)
                        //Thread.Sleep(1);
                        if (swing_pole == numberExteneder[number - 1] + 1)
                        {
                            swing_flag = true;
                            current_test_index++;
                        }
                    }
                    
                    sw.Stop();
                    setImageOff(number-1);
                    center.Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;
                    //send_packet(poleMapper(number), (byte)Color.OFF);

                    clearBuff();
                    currentSwingAmount.Text = (current_test_index + n * 12) + "";
                    lrFlag = false; FbFlag = false;
                    if (number == 2 || number == 7) FbFlag = true;
                    else if (number == 4 || number == 5) lrFlag = true;
                    center_flag = false;
                    //v4_data = null;
                    //length_v4 = -1;
                    //Thread.Sleep(75);
                    while (true)
                    {
                        //if (center_flag)
                        //{                 
                        //    break;
                        //}
                        //else isCenter();
                        //isCenter();
                        //if (FbFlag && lrFlag) break;
                        //IsCenter_v2();
                        //inputListbox("LR = " +lrFlag +" fb = "+FbFlag);
                        //inputListbox(center_flag.ToString());
                        Thread.Sleep(5);
                        if (center_flag==true)
                            break;
                    }
                    sw2.Stop();
                    center.Image = SmartBadmintonTrainingSystem.Properties.Resources.green_circle;

                    T1[(number - 1) + 12 * n] = (float.Parse(sw.ElapsedMilliseconds.ToString()) * 0.001f) - 0.15f;
                    T2[(number - 1) + 12 * n] = (float.Parse(sw2.ElapsedMilliseconds.ToString()) * 0.001f) ;
                    T2[(number - 1) + 12 * n] = (T2[(number - 1) + 12 * n] - T1[(number - 1) + 12 * n]) - 0.15f;

                    sw.Reset();
                    sw2.Reset();
                    RealPole_index_24++;
                }
                
            }

            AutoClosingMessageBox.Show("테스트 종료", "종료 알림", 250);
            AutoClosingMessageBox.Show("데이터 전송", "상태 알림", 250);
            threadFlag = false;
            for (int i = 0; i < 12*(targetTestAmount+1); i++)
            {
                insertDatabase(u_instance.uID, u_instance.uPW, T1[i], i + 1, u_instance.LoginDate, 0, TestCount,i<12?0:1);
                insertDatabase(u_instance.uID, u_instance.uPW, T2[i], i + 1, u_instance.LoginDate, 1, TestCount, i< 12 ? 0 : 1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            if (orderString.Equals(""))
            {
                AutoClosingMessageBox.Show("순서를 설정해주세요", "Error", 300);
            }
            else if (!port_set)
            {
                AutoClosingMessageBox.Show("컨트롤러를 연결해주세요!", "Error", 300);
            }
            else
            {
                //setNewNumber();
                for(int i = 0; i < 8; i++)
                {
                    send_packet((byte)poleCoder[i], (byte)Color.OFF);
                    Thread.Sleep(SAFE_SLEEP_TIME);
                }
                setByteSendData();
                try
                {
                    if (!threadFlag)// 스레드 시작
                    {
                        ths = new ThreadStart(thread_test);
                        th1 = new Thread(ths);
                        th1.Start();
                        loggerTime.Start();
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

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            orderString = "8-7-6-5-4-3-2-1";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            orderString = "1-2-3-4-5-6-7-8";
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
            listBox1.Items.Insert(0,data);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void p3_Click(object sender, EventArgs e)
        {

        }

        private void test_12_CheckedChanged(object sender, EventArgs e)
        {
            targetTestAmount = 0;
            goalSwingAmount.Text = 12 + "";
        }

        private void test_24_CheckedChanged(object sender, EventArgs e)
        {
            targetTestAmount = 1;
            goalSwingAmount.Text = 24+ "";
        }

        private void random_order_CheckedChanged(object sender, EventArgs e)
        {
            status = "random";
        }

        private void choose_order_CheckedChanged(object sender, EventArgs e)
        {
            status = "select";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
#if (DEBUG)
            inputListbox("컨트롤러 새로고침");
#endif
            setRefreshPort();
            port_set = false;
        }
    }
}
