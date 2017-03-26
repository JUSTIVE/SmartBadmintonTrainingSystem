using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Media;
using System.Threading;


namespace SmartBadmintonTrainingSystem
{  
    public partial class Training : Form
    {
        int swing_pole = -1;
        int number;
        byte[] oldByte;
        //
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        //customprogParam
        public int customProgAmount;
        public List<CustomProgramType> customProgramTypeList;

        private int currentPole=1;

        //Sensor HX Code
        byte[] index = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };//M2,M1,B3,B2,B1,F3,F2,F1
        byte[] color = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };//off,Red,Green,Yellow,Blue,magenta,cyan,White;
        enum COLORENUM { OFF,RED, GREEN, YELLOW, BLUE,MAGENTA,CYAN,WHITE };
        byte start = 0x02;
        byte start_check = 0x01;
        byte end = 0x03;
        byte[] byteSendData = new byte[6];
        int[] mapper = {7,6,5,1,0,4,3,2};
        int[] unmapper = { 4, 3, 7, 6, 5, 2, 1, 0 };
        string strRecData = "";
        bool thread_flag = false;

        int iTemp;
        int Sizer;
        bool is_light=false;
        //테스트 순서 관련 변수 선언 시작
        public string orderString = "";
        List<int> OrderList = new List<int>();
        public bool r_flag, g_flag, b_flag, y_flag;
        public bool hasSaving = false;
        //순서 설정 모드
        public string status = "";

        //시리얼 포트 관련 변수 선언 시작
        SerialPort SP = new SerialPort();
        TrainingMode TM;
        string SP_name="";
        List<string> buffer = new List<string>();//센서의 중앙 감지 데이터 저장
        List<string> s_buffer = new List<string>();//센서의 모든 데이터를 저장
        bool port_set = false;
        //시리얼 포트 관련 변수 선언 종료
        //라벨String
        string openX = "컨트롤러 연결:X";
        string openO = "컨트롤러 연결:O";

        //센서 데이터 관련 플래그
        bool center_flag1 = true;
        
        bool lrFlag = false; //좌우
        bool FbFlag = false; //상하
        //스윙여부판단
        bool swing_flag = true;
        public string order_list = "";

        int target_pole;
        ThreadStart thread;
        Thread threader=null;
        Form2 f2;

        
        public List<PictureBox> pList = new List<PictureBox>();

        SoundPlayer sound, sound2;
        
        //Curtain booya
        bool isCurtain;
        //colorsetTest
        public TrainingColorSet TCS = null;
        public bool isColor = false;

        //생성자/constructor
        public Training()
        {
            InitializeComponent();
            pList.Clear();
            setpList();
            initial();
            customProgramTypeList = new List<CustomProgramType>();
            setUpProgramList();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.Size = new Size(1920,1048);
        }
        //for double buffering
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        public void initial()
        {
            r_flag = false;
            g_flag = false;
            b_flag = false;
            y_flag = false;

            label2.Text = openX;
            foreach (string comport in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(comport);
            }
            if (comboBox1.Items.Count != 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            customProgAmount=0;
            isCurtain = true;
        }
        public void flipCurtain()
        {
            isCurtain = !isCurtain;
            Visible = isCurtain;
        }
        
        public void setUpProgramList()
        {
            CustomProgramPanel.Visible = false;

            while (CustomProgramPanel.Controls.Count > 0)
            {
                CustomProgramPanel.Controls[0].Dispose();
            }
            CustomProgramPanel.Visible = true;
            
            for (int i = 0; i < customProgramTypeList.Count; ++i)
            {
                Button temp = new Button();
                temp.Location = new Point(8, 88*i+8);
                temp.Size = new Size(CustomProgramPanel.Size.Width - 184, 80);
                temp.Name = i+"CPT";
                temp.Visible = true;
                temp.FlatStyle = FlatStyle.Flat;
                temp.FlatAppearance.BorderSize = 0;
                temp.BackColor = Color.FromArgb(240, 240, 240);
                temp.MouseEnter += customButton_enter;
                temp.MouseHover += customButton_enter;
                temp.MouseLeave += customButton_leave;
                temp.MouseClick += customProgram_Clicked;

                Label templb = new Label();
                templb.Location = new Point(8, 8);
                templb.Name = i + "CPT_label";
                templb.Visible = true;
                templb.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                templb.Text = customProgramTypeList[i].name;
                templb.ForeColor = Color.FromArgb(255, 87, 34);
                templb.Size = new Size(temp.Size.Width-16, templb.Height);
                templb.BackColor = System.Drawing.Color.Transparent;
                templb.MouseClick += customProgram_Clicked;
                templb.MouseEnter += customButton_enter;
                templb.MouseLeave += customButton_leave;

                Label info = new Label();
                info.Location = new Point(10, 40);
                info.Name = i + "CPT_label_info";
                info.Visible = true;
                info.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                info.Text = customProgramTypeList[i].trainingSet;
                info.ForeColor = Color.FromArgb(66,66,66);
                info.Size = new Size(temp.Size.Width - 20, templb.Height);
                info.BackColor = System.Drawing.Color.Transparent;
                info.MouseClick += customProgram_Clicked;
                info.MouseEnter += customButton_enter;
                info.MouseLeave += customButton_leave;

                PictureBox tmpsetting = new PictureBox();
                tmpsetting.Location = new Point(CustomProgramPanel.Size.Width - 172, 88 * i + 8);
                tmpsetting.Size = new Size(80, 80);
                tmpsetting.Name = i + "CPT_setting";
                tmpsetting.Visible = true;
                tmpsetting.BackColor = Color.FromArgb(240, 240, 240);
                tmpsetting.Padding= new Padding(24);
                tmpsetting.SizeMode =PictureBoxSizeMode.StretchImage;
                tmpsetting.Image = Properties.Resources.setting;
                tmpsetting.MouseEnter += customButton_enter;
                tmpsetting.MouseLeave += customButton_leave;
                tmpsetting.Click += settingCustomButton;


                PictureBox tmpdelete = new PictureBox();
                tmpdelete.Location = new Point(CustomProgramPanel.Size.Width - 88, 88 * i + 8);
                tmpdelete.Size = new Size(80, 80);
                tmpdelete.Name = i + "CPT_delete";
                tmpdelete.Visible = true;
                tmpdelete.BackColor = Color.Tomato;
                tmpdelete.Padding = new Padding(24);
                tmpdelete.SizeMode = PictureBoxSizeMode.StretchImage;
                tmpdelete.Image = Properties.Resources.close_button;
                tmpdelete.MouseEnter += deleteButton_enter;
                tmpdelete.MouseLeave += deleteButton_leave;
                tmpdelete.Click += deleteButton_Clicked;

                CustomProgramPanel.Controls.Add(tmpdelete);
                CustomProgramPanel.Controls.Add(tmpsetting);
                temp.Controls.Add(info);
                temp.Controls.Add(templb);
                CustomProgramPanel.Controls.Add(temp);
                //buttonSize
            }
            Button addButton=new Button();
            addButton.Location=new Point(8,customProgramTypeList.Count*88+8);
            addButton.Size = new Size(CustomProgramPanel.Size.Width-16,80);
            addButton.Visible = true;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.FlatAppearance.BorderSize = 1;
            addButton.Text = "프로그램 추가";
            addButton.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            addButton.ForeColor = Color.FromArgb(255,87,34);
            addButton.Click += addProgramButtonHandler;
            addButton.Padding= new Padding(80,0,0,0);
            PictureBox icon = new PictureBox();
            icon.Location = new Point(0, 0);
            icon.Size = new Size(80,addButton.Size.Height);
            icon.Image = Properties.Resources.ic_plus_white_48dp;
            icon.SizeMode = PictureBoxSizeMode.StretchImage;
            icon.Padding = new Padding(24);
            icon.BackColor = Color.Tomato;
            icon.Click += addProgramButtonHandler;

            addButton.Controls.Add(icon);
            CustomProgramPanel.Controls.Add(addButton);
            CustomProgramPanel.Visible = true;
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
        /// <summary>
        /// 버튼 디자인 코드
        /// </summary>
        public void customButton_enter(object sender,EventArgs e)
        {
            Control t = sender as Control;
            if (!(sender.GetType().Equals(typeof(Button))|| sender.GetType().Equals(typeof(PictureBox))))
            {
                t.Parent.BackColor = Color.FromArgb(220, 220, 220);
            }
            else { 
                t.BackColor = Color.FromArgb(220, 220, 220);
            }
        }
        /// <summary>
        /// 버튼 디자인 코드
        /// </summary>
        public void customButton_leave(object sender, EventArgs e)
        {
            Control t = sender as Control;
            if (!(sender.GetType().Equals(typeof(Button)) || sender.GetType().Equals(typeof(PictureBox))))
            {
                t.Parent.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                t.BackColor = Color.FromArgb(240, 240, 240);
            }
        }
        public void deleteButton_leave(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            p.BackColor = Color.Tomato;
        }
        public void deleteButton_enter(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            p.BackColor = Color.Coral;
        }
        /// <summary>
        /// 버튼 이벤트 코드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void deleteButton_Clicked(object sender, EventArgs e)
        {

            Control t = sender as Control;
            kill(Int32.Parse(t.Name[0]+""));
            setUpProgramList();
        }
        public void customProgram_Clicked(object sender, MouseEventArgs e)
        {
            Control t = sender as Control;
            
            order_list = customProgramTypeList[Int32.Parse(t.Name[0]+"")].trainingSet;
            inputListbox(order_list);
            isColor = false;
        }
        

        public void setForm(TrainingMode t)
        {
            TM = t;
        }
        public void settingCustomButton(object sender, EventArgs e)
        {
            Control t = sender as Control;
            CustomProgramForm cpf = new CustomProgramForm(this, Int32.Parse(t.Name[0] + ""), customProgramTypeList[Int32.Parse(t.Name[0]+"")]);
            flipCurtain();
            
            cpf.Visible = true;
        }
        public void kill(int number)
        {
            customProgramTypeList.RemoveAt(number);
        }
        
        private void addProgramButtonHandler(object sender, EventArgs e)
        {
            CustomProgramForm cpf = new CustomProgramForm(this,-1);
            flipCurtain();
            cpf.Visible = true;
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (port_set) { 
                if (!thread_flag)
                {
                    inputListbox(target_pole+"");
                    if (!order_list.Equals("")||isColor)
                    {
                        thread = new ThreadStart(TrainingThreadStart);
                        threader = new Thread(thread);

                        threader.Start();

                        thread_flag = true;
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("기둥을 설정하십시요", "설정 오류", 500);
                        
                    }
                }
                else
                {
                    AutoClosingMessageBox.Show("켜져 있는 스윙을 해제하십시요","점등 해제",500);
                }
            }
            else
            {
                AutoClosingMessageBox.Show("컨트롤러가 연결되어 있지 않습니다!", "연결 상태", 500);
            }
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
                for (int i = 0; i < 8; i++)
                {
                    setImageOff(i + 1);
                    centerPic.Image = Properties.Resources.off_circle;
                }
            }
            catch
            {
                //MessageBox.Show("컨트롤러 USB를 연결해주세요!");
            }
        }
        private void Training_Load(object sender, EventArgs e)
        {
            SetSerialPort();
            //radioButton1.Checked = true;
            orderString = "1-2-3-4-5-6-7-8";
            //orderString = "4-5";
            for (int i = 0; i < 8; i++)
            {
                setImageOff(i + 1);
                centerPic.Image = Properties.Resources.off_circle;
            }
            sound = new SoundPlayer(SmartBadmintonTrainingSystem.Properties.Resources.beep_01a);
            sound2 = new SoundPlayer(SmartBadmintonTrainingSystem.Properties.Resources.beep_05);

        }

        private void btn_configuration_Click(object sender, EventArgs e)
        {
            //if (hasSaving)
            //{
            //    Configuration f = new Configuration();
            //    f.setSaving(r_flag, g_flag, b_flag, y_flag, orderString);
            //    f.setForm(this);
            //    f.Show();
            //}
            //else
            //{
            //    Configuration f = new Configuration();
            //    f.setForm(this);
            //    f.Show();
            //}
            flipCurtain();
            f2 = new Form2(this);
            f2.Show();
            order_list = "";
            isColor = true;
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
            while (true)
            {
                bool breaker = false;
                strRecData = "";
                if (oldByte != null)//붙이기만 할것
                {
                    byte[] temp = buff;
                    Sizer = oldByte.Length + buff.Length;
                    buff = new byte[Sizer];
                    oldByte.CopyTo(buff, 0);
                    temp.CopyTo(buff, oldByte.Length);
                    //inputListbox("fixed");
                    for (iTemp = 0; iTemp < Sizer; iTemp++)
                    {
                        strRecData += buff[iTemp].ToString("X2") + " ";
                    }
                    //inputListbox(strRecData);

                    oldByte = null;
                }
                if (Sizer < 6)
                {
                    oldByte = new byte[Sizer];
                    buff.CopyTo(oldByte, 0);
                    break;
                }
                if (Sizer == 6)
                {
                    strRecData = "";
                    for (iTemp = 0; iTemp < Sizer; iTemp++)
                    {
                        strRecData += buff[iTemp].ToString("X2") + " ";
                    }
                    //inputListbox(strRecData);
                    if (!center_flag1)
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
                            isSwing(number);
                        }
                    }
                    breaker = true;
                    break;
                }
                else if (Sizer > 6)//6보다 큰 경우 -center
                {
                    strRecData = "";
                    for (iTemp = 0; iTemp < 6; iTemp++)
                    {
                        strRecData += buff[iTemp].ToString("X2") + " ";
                    }
                    //inputListbox(strRecData + "overed");
                    if (!center_flag1)
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
                    for (int i = 6; i < Sizer + 6; i++)
                    {
                        buff[i - 6] = temp[i];
                    }

                }
                if (breaker)
                    break;
            }
        }
        //void EventDataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    Sizer = SP.BytesToRead;
        //    strRecData = "";

        //    byte[] buff = new byte[Sizer];
        //    SP.Read(buff, 0, Sizer);

        //    for (iTemp = 0; iTemp < Sizer; iTemp++)
        //    {
        //        strRecData += buff[iTemp].ToString("X2") + " ";
        //    }
        //    if (!center_flag1)
        //    {
        //        if (!buffer.Contains(strRecData))
        //        {
        //            buffer.Add(strRecData);
        //        }
        //    }
        //    if (!swing_flag)
        //    {
        //        /*if (Sizer == 6)*/ { s_buffer.Add(strRecData); }
        //        //else { send_packet(mapper[currentPole - 1], 0); }
        //        ///TODO: Substring 비교 체크
        //    }
        //}
        public void send_packet(int number, int number2) //number:기둥번호 number2:색상
        {
            try
            {
                byteSendData[0] = start;
                byteSendData[1] = start_check;
                byteSendData[2] = index[number];
                byteSendData[3] = color[number2];
                byte tmp2 = (byte)(byteSendData[1] + byteSendData[2] + byteSendData[3]);
                byteSendData[4] = tmp2;
                byteSendData[5] = end;

                SP.Write(byteSendData, 0, 6);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
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
                    //SP.ReadTimeout = (int)500;
                    //SP.WriteTimeout = (int)500;

                    SP.Open();
                    if (SP.IsOpen)
                    {
                        setOnPort();
                    }
                }
                else
                {
                    setOffPort();
                    port_set = false;
                    thread_flag = false;
                    is_light = false;
                    if (!(threader == null))
                    {
                        if (threader.IsAlive)
                        {
                            threader.Abort();
                            threader = null;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                AutoClosingMessageBox.Show("컨트롤러가 연결되어 있지 않습니다!", "연결 상태", 500);
                //                MessageBox.Show("컨트롤러가 연결되어있지 않습니다!");
            }
        }
        public void setOnPort()
        {
            try
            {
                buffer.Clear();
                port_set = true;
                Picture_Status.Image = SmartBadmintonTrainingSystem.Properties.Resources.signal_green;
                SP.DataReceived += new SerialDataReceivedEventHandler(EventDataReceivedV3);
                //AutoClosingMessageBox.Show("컨트롤러 연결 성공", "포트", 500);
                inputListbox("컨트롤러 연결 성공");
                label2.Text = openO;
                //초기 그림 설정
                button1.Text = "연결종료";
            }
            catch (System.Exception ex){}
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
                Picture_Status.Image = SmartBadmintonTrainingSystem.Properties.Resources.signal_red;
                
                for (int i = 0; i < 8; i++)
                {
                    setImageOff(i + 1);
                    centerPic.Image = Properties.Resources.off_circle;
                }
            }
            catch (System.Exception ex){}
        }

        
        ///returns 1-based pole number
        public int isSwing(int number)
        {
            //if(s_buffer.Count>0)
            //inputListbox("current Buff " + s_buffer[0]);
            //case 1
            if (s_buffer.Contains("02 01 08 01 0A 03 ") || s_buffer.Contains("02 01 08 03 0C 03 "))
            {
                swing_pole = 1;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 2
            if (s_buffer.Contains("02 01 07 01 09 03 ") || s_buffer.Contains("02 01 07 03 0B 03 "))
            {
                swing_pole = 2;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 3
            if (s_buffer.Contains("02 01 06 01 08 03 ") || s_buffer.Contains("02 01 06 03 0A 03 "))
            {
                swing_pole = 3;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 4
            if (s_buffer.Contains("02 01 02 01 04 03 ") || s_buffer.Contains("02 01 02 03 06 03 "))
            {
                swing_pole = 4;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 5
            if (s_buffer.Contains("02 01 01 01 03 03 ") || s_buffer.Contains("02 01 01 03 05 03 "))
            {
                swing_pole = 5;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 6
            if (s_buffer.Contains("02 01 05 01 07 03 ") || s_buffer.Contains("02 01 05 03 09 03 "))
            {
                swing_pole = 6;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 7
            if (s_buffer.Contains("02 01 04 01 06 03 ") || s_buffer.Contains("02 01 04 03 08 03 "))
            {
                swing_pole = 7;
                if (number == swing_pole)
                    swing_flag = true;
            }
            //case 8
            if (s_buffer.Contains("02 01 03 01 05 03 ") || s_buffer.Contains("02 01 03 03 07 03 "))
            {
                swing_pole = 8;
                if (number == swing_pole)
                    swing_flag = true;
            }
            return swing_pole;
        }

        public void setImageRed(int number){pList.ElementAt(number - 1).Image = SmartBadmintonTrainingSystem.Properties.Resources.red_circle;}
        public void setImageGreen(int number){pList.ElementAt(number - 1).Image = SmartBadmintonTrainingSystem.Properties.Resources.green_circle;}
        public void setImageBlue(int number){pList.ElementAt(number - 1).Image = SmartBadmintonTrainingSystem.Properties.Resources.blue_circle;}
        public void setImageYellow(int number){pList.ElementAt(number - 1).Image = SmartBadmintonTrainingSystem.Properties.Resources.yellow_circle;}
        public void setImageOff(int number){pList.ElementAt(number - 1).Image = SmartBadmintonTrainingSystem.Properties.Resources.off_circle;}

        private void Training_FormClosed(object sender, FormClosedEventArgs e)
        {
            TM.Visible = true;
            SP.Close();
            try
            {
                threader.Abort();
                thread_flag = false;
            }
            catch (Exception ex)
            {
            }
        }
        public void clearBuff()
        {
            buffer.Clear();
            s_buffer.Clear();
        }

        public void isCenter()
        {
            if (buffer.Contains("02 01 02 02 05 03 ") && !lrFlag)// 01 02 02 05
            {
                lrFlag = true;
            }
            else if (buffer.Contains("02 01 02 00 03 03") && lrFlag)// 01 02 00 03
            {
                lrFlag = false;
            }

            if (buffer.Contains("02 01 07 02 0A 03 ") && !FbFlag)
            {
                FbFlag = true;
            }
            else if (buffer.Contains("02 01 07 00 08 03 ") && FbFlag)
            {
                FbFlag = false;
            }

            if (lrFlag && FbFlag) center_flag1 = true;
        }

        void normalthreadStart()
        {
            clearBuff();
            centerPic.Image = Properties.Resources.red_circle;
            swing_flag = false;
            
            for (;;)
            {
                if (swing_flag)
                {
                    inputListbox("swing");
                    break;
                }
                else
                {
                    currentPole = unmapper[target_pole] + 1;
                    isSwing(currentPole);
                }
            }
            setImageOff(currentPole);//unmapped pole number
            send_packet(target_pole, 4);//mapped pole number

            clearBuff();
            lrFlag = false; FbFlag = false;
            if ((unmapper[target_pole] + 1) == 2 || (unmapper[target_pole] + 1) == 7) FbFlag = true;
            else if ((unmapper[target_pole] + 1) == 4 || (unmapper[target_pole] + 1) == 5) lrFlag = true;
            center_flag1 = false;
            for (;;)
            {
                if (center_flag1)
                {
                    inputListbox("center");
                    is_light = false;
                    centerPic.Image = Properties.Resources.off_circle;
                    break;
                }
                else isCenter();
            }
            thread_flag = false;
        }

        private void p1_Click(object sender, EventArgs e)
        {
            if (port_set) { 
                if (!is_light) {
                    send_packet(7, (int)COLORENUM.RED);
                    setImageRed(1);
                    is_light = true;
                    if (!thread_flag)
                    {
                        target_pole = 7;
                        if (threader != null)
                        {
                            if (threader.IsAlive)
                            {
                                threader.Abort();
                                threader = null;
                                inputListbox(threader.IsAlive + "");
                            }
                        }
                        thread = new ThreadStart(normalthreadStart);
                        threader = new Thread(thread);
                        threader.Start();
                        thread_flag = true;
                    }
                }
            }
        }

        private void p2_Click(object sender, EventArgs e)
        {
            if (port_set) {
                if (!is_light)
                {
                    send_packet(6, (int)COLORENUM.RED);
                    setImageRed(2);
                    is_light = true;
                    if (!thread_flag)
                    {
                        target_pole = 6;
                        if (threader != null)
                        {
                            if (threader.IsAlive)
                            {
                                threader.Abort();
                                threader = null;
                                inputListbox(threader.IsAlive + "");
                            }
                        }
                        thread = new ThreadStart(normalthreadStart);
                        threader = new Thread(thread);
                        threader.Start();
                        thread_flag = true;
                    }
                }
            }
        }
        
        private void p3_Click(object sender, EventArgs e)
        {
            if (port_set)
                if (!is_light)
            {
                    send_packet(5, (int)COLORENUM.RED);
                    setImageRed(3);
                    is_light = true;
                    if (!thread_flag)
                    {
                        target_pole = 5;
                        if (threader != null)
                        {
                            if (threader.IsAlive)
                            {

                                threader.Abort();
                                threader = null;
                                inputListbox(threader.IsAlive + "");
                            }

                        }
                        thread = new ThreadStart(normalthreadStart);
                        threader = new Thread(thread);
                        threader.Start();
                        thread_flag = true;
                    }

                }
        }

        private void p4_Click(object sender, EventArgs e)
        {
            if (port_set)
                if (!is_light)
            {
                    send_packet(1, (int)COLORENUM.RED);
                    setImageRed(4);
                    is_light = true;
                    if (!thread_flag)
                    {
                        target_pole = 1;
                        if (threader != null)
                        {
                            if (threader.IsAlive)
                            {

                                threader.Abort();
                                threader = null;
                                inputListbox(threader.IsAlive + "");
                            }

                        }
                        thread = new ThreadStart(normalthreadStart);
                        threader = new Thread(thread);
                        threader.Start();
                        thread_flag = true;
                    }
                }
        }

        private void p5_Click(object sender, EventArgs e)
        {
            if (port_set){ 
                if (!is_light){
                    send_packet(0, (int)COLORENUM.RED);
                    setImageRed(5);
                    is_light = true;
                    if (threader != null)
                    {
                        if (threader.IsAlive)
                        {

                            threader.Abort();
                            threader = null;
                            inputListbox(threader.IsAlive + "");
                        }

                    }
                    if (!thread_flag)
                    {
                        target_pole = 0;
                        thread = new ThreadStart(normalthreadStart);
                        threader = new Thread(thread);
                        threader.Start();
                        thread_flag = true;
                    }
                }
            }
        }

        private void p6_Click(object sender, EventArgs e)
        {
            if (port_set) { 
                if (!is_light)
                {
                    send_packet(4, (int)COLORENUM.RED);
                    setImageRed(6);
                    is_light = true;

                    if (!thread_flag)
                    {
                        target_pole = 4;
                        if (threader != null)
                        {
                            if (threader.IsAlive)
                            {

                                threader.Abort();
                                threader = null;
                                inputListbox(threader.IsAlive + "");
                            }

                        }
                        thread = new ThreadStart(normalthreadStart);
                        threader = new Thread(thread);
                        threader.Start();
                        thread_flag = true;
                    }
                }
            }
        }

        private void p7_Click(object sender, EventArgs e)
        {
            if (port_set) { 
                if (!is_light)
                {
                    send_packet(3, (int)COLORENUM.RED);
                    setImageRed(7);
                    is_light = true;
                    if (!thread_flag)
                    {
                        target_pole = 3;
                        if (threader != null)
                        {
                            if (threader.IsAlive)
                            {

                                threader.Abort();
                                threader = null;
                                inputListbox(threader.IsAlive + "");
                            }
                        }
                        thread = new ThreadStart(normalthreadStart);
                        threader = new Thread(thread);
                        threader.Start();
                        thread_flag = true;
                    }
                }
            }
        }

        private void p8_Click(object sender, EventArgs e)
        {
            if (port_set) { 
                if (!is_light)
                {
                    send_packet(2, (int)COLORENUM.RED);
                    setImageRed(8);
                    is_light = true;
                    if (!thread_flag)
                    {
                        target_pole = 2;
                        if (threader != null)
                        {
                            if (threader.IsAlive)
                            {
                                threader.Abort();
                                threader = null;
                                inputListbox(threader.IsAlive + "");
                            }
                        }
                        thread = new ThreadStart(normalthreadStart);
                        threader = new Thread(thread);
                        threader.Start();
                        thread_flag = true;
                    }
                }
            }
        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SP_name = comboBox1.SelectedItem.ToString();
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
                AutoClosingMessageBox.Show("컨트롤러 오류","Error",500);
            }
            button1.Text = "연결시도";
            label2.Text = openX;
            Picture_Status.Image = SmartBadmintonTrainingSystem.Properties.Resources.signal_red;
            for (int i = 0; i < 8; i++)
            {
                setImageOff(i + 1);
                centerPic.Image = Properties.Resources.off_circle;

            }
            SetSerialPort();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void inputListbox(string data)
        {
            listBox1.Items.Add(data);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        private void refreshButtonClick(object sender, EventArgs e)
        {
            inputListbox("컨트롤러 새로고침");
            setRefreshPort();
            port_set = false;
            thread_flag = false;
            is_light = false;
            if (!(threader == null)) {
                if (threader.IsAlive)
                {
                    threader.Abort();
                    threader = null;
                }
            }
            
        }
        public void actionBeam()
        {
            inputListbox(order_list);
        }
        void TrainingThreadStart()//실질적인 트레이닝 처리 스레드
        {
            //send_packet(0, (int)COLORENUM.OFF); send_packet(1, (int)COLORENUM.OFF); send_packet(2, (int)COLORENUM.OFF);
            //send_packet(3, (int)COLORENUM.OFF); send_packet(4, (int)COLORENUM.OFF); send_packet(5, (int)COLORENUM.OFF);
            //send_packet(6, (int)COLORENUM.OFF); send_packet(7, (int)COLORENUM.OFF);
            char[] delim = { ',' };
            string[] splitter = order_list.Split(delim);
            centerPic.Image = Properties.Resources._3_image;
            sound.Play();
            Thread.Sleep(1000);
            centerPic.Image = Properties.Resources._2_image;
            sound.Play();
            Thread.Sleep(1000);
            centerPic.Image = Properties.Resources._1_image;
            sound.Play();
            Thread.Sleep(1000);
            centerPic.Image = Properties.Resources.red_circle;
            stopwatch.Start();
            //fordebug -command multiple lights at once
            //if (true)
            //{
            //    while (true)
            //        for (int i = 0; ; i++)
            //        {
            //            for (int j = 0; j < 8; j++)
            //            {
            //                send_packet(j, color[i % 8]);
            //                inputListbox(j + "");
            //            }
            //            Thread.Sleep(500);

            //        }
            //}

            if (!isColor)//사용자 지정 프로그램
            { 
                for(int current_test_index = 0; current_test_index < splitter.Length;)//매 회차마다
                {
                    
                    inputListbox(current_test_index+"번째 기둥 시작 : "+splitter.ElementAt(current_test_index));
                    target_pole = mapper[Int32.Parse(splitter.ElementAt(current_test_index))-1];
                    send_packet(target_pole, (int)COLORENUM.RED);
                    setImageRed(unmapper[target_pole]+1);
                    clearBuff();
                    swing_flag = false;
                    swing_pole = -1;
                    inputListbox(target_pole + " , " + unmapper[target_pole]+" , "+swing_pole);
                    for (;;)
                    {
                        if (swing_flag)
                        {
                            current_test_index++;
                            inputListbox("swing");
                            break;
                        }
                        else {
                            isSwing(unmapper[target_pole]+1);
                            //currentPole = unmapper[target_pole] + 1;
                            //isSwing(currentPole);
                        } //1-base pole number
                    }
                    centerPic.Image = Properties.Resources.red_circle;
                    setImageOff(unmapper[target_pole]+1);//unmapped pole number
                    //send_packet(target_pole+1, 4);//mapped pole number

                    clearBuff();
                    lrFlag = false; FbFlag = false;
                    if ((unmapper[target_pole]+1) == 2 || (unmapper[target_pole]+1) == 7) FbFlag = true;
                    else if ((unmapper[target_pole]+1) == 4 || (unmapper[target_pole]+1) == 5) lrFlag = true;
                    center_flag1 = false;
                    for (;;)
                    {
                        if (center_flag1)
                        {
                            inputListbox("center");
                            is_light = false;
                            break;
                        }
                        else isCenter();
                    }
                    centerPic.Image = null;
                    centerPic.Image = Properties.Resources.green_circle;
               
                }
                stopwatch.Stop();
                float elapsed = float.Parse(stopwatch.ElapsedMilliseconds.ToString())*0.001f;
                AutoClosingMessageBox.Show("경과한 시간 : "+elapsed ,"ALERT",1000);
                inputListbox("elapsed" + elapsed);
                thread_flag = false;
            }
            else//색상 트레이닝
            {
                for (int i = 0; i < TCS.times; i++)//매 회차마다
                {
                    bool breaker = false;
                    int progress = 0;
                    ///ready to delete
                    string temp = "";
                    for(int a = 0; a < TCS.generatedData[i].Length; a++)
                    {
                        temp += TCS.generatedData[i][a];
                    }
                    inputListbox(i + "번째 케이스 시작 : " + temp);
                    temp = "";
                    for (int a = 0; a < 4; a ++) {
                        temp += TCS.dataset[a];
                    }
                    inputListbox(i + "색순서 : " + temp);
                    
                    for (int j = 0; j < 4; j++)
                    {
                        Thread.Sleep(150);
                        target_pole = mapper[TCS.generatedData[i][j]-1];
                        //1,2,3 순서 칠하기
                        if (j<3) { 
                            switch (TCS.dataset[j])
                            {
                                case (int)COLORENUM.RED:
                                    setImageRed(TCS.generatedData[i][j]);
                                    send_packet(target_pole, (int)COLORENUM.RED);
                                    break;
                                case (int)COLORENUM.GREEN:
                                    setImageGreen(TCS.generatedData[i][j]);
                                    send_packet(target_pole, (int)COLORENUM.GREEN);
                                    break;

                                case (int)COLORENUM.YELLOW:
                                    setImageYellow(TCS.generatedData[i][j]);
                                    send_packet(target_pole, (int)COLORENUM.YELLOW);
                                    break;
                                case (int)COLORENUM.BLUE:
                                    setImageBlue(TCS.generatedData[i][j]);
                                    send_packet(target_pole, (int)COLORENUM.BLUE);
                                    break;
                                    
                            }
                        }
                        else if(j==3)
                        {
                            switch (TCS.dataset[j])
                            {
                                case (int)COLORENUM.RED:
                                    setImageRed(TCS.generatedData[i][3]);
                                    setImageRed(TCS.generatedData[i][4]);
                                    send_packet(mapper[TCS.generatedData[i][3]-1], (int)COLORENUM.RED);
                                    send_packet(mapper[TCS.generatedData[i][4] - 1], (int)COLORENUM.RED);
                                    break;
                                case (int)COLORENUM.GREEN:
                                    setImageGreen(TCS.generatedData[i][3]);
                                    setImageGreen(TCS.generatedData[i][4]);
                                    send_packet(mapper[TCS.generatedData[i][3] - 1], (int)COLORENUM.GREEN);
                                    send_packet(mapper[TCS.generatedData[i][4] - 1], (int)COLORENUM.GREEN);
                                    break;
                                case (int)COLORENUM.BLUE:
                                    setImageBlue(TCS.generatedData[i][3]);
                                    setImageBlue(TCS.generatedData[i][4]);
                                    send_packet(mapper[TCS.generatedData[i][3] - 1], (int)COLORENUM.BLUE);
                                    send_packet(mapper[TCS.generatedData[i][4] - 1], (int)COLORENUM.BLUE);
                                    break;
                                case (int)COLORENUM.YELLOW:
                                    setImageYellow(TCS.generatedData[i][3]);
                                    setImageYellow(TCS.generatedData[i][4]);
                                    send_packet(mapper[TCS.generatedData[i][3] - 1], (int)COLORENUM.YELLOW);
                                    send_packet(mapper[TCS.generatedData[i][4] - 1], (int)COLORENUM.YELLOW);
                                    break;
                            }
                        }
                    }
                    //setImageRed(unmapper[target_pole] + 1);
                    clearBuff();
                    swing_flag = false;
                    for (;;)
                    {
                        if (swing_flag)
                        {
                            inputListbox("swing");
                            break;
                        }
                        else
                        {
                            currentPole = unmapper[target_pole] + 1;
                            breaker=(isSwing(currentPole)>0);
                        } //1-base pole number
                    }
                    centerPic.Image = Properties.Resources.red_circle;
                    setImageOff(unmapper[target_pole] + 1);//unmapped pole number
                    
                    if (breaker)//잘못된 기둥을 건드렸을 경우
                    {
                        AutoClosingMessageBox.Show("잘못된 기둥을 스윙하였습니다","Error",1500);
                        break;
                    }
                    clearBuff();
                    lrFlag = false; FbFlag = false;
                    if ((unmapper[target_pole] + 1) == 2 || (unmapper[target_pole] + 1) == 7) FbFlag = true;
                    else if ((unmapper[target_pole] + 1) == 4 || (unmapper[target_pole] + 1) == 5) lrFlag = true;
                    center_flag1 = false;
                    for (;;)
                    {
                        if (center_flag1)
                        {
                            inputListbox("center");
                            is_light = false;
                            break;
                        }
                        else isCenter();
                    }
                    centerPic.Image = null;
                    centerPic.Image = Properties.Resources.green_circle;
                }
                stopwatch.Stop();
                float elapsed = float.Parse(stopwatch.ElapsedMilliseconds.ToString()) * 0.001f;
                AutoClosingMessageBox.Show("경과한 시간 : " + elapsed, "ALERT", 1000);
                thread_flag = false;
            }
        } 
    }
}
