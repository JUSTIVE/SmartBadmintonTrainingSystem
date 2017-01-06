using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SmartBadmintonTrainingSystem
{
    class singletonDB
    {
        private static singletonDB instance = new singletonDB();
        public MySqlConnection conn;
        string strconn = "Server=220.69.209.170;Database=badminton;Uid=cglab;Pwd=clws";
        private singletonDB()
        {
            conn = new MySqlConnection(strconn);
            conn.Open();
        }

        public static void IsOpen()
        {
            if (!singletonDB.instance.conn.State.ToString().Equals("Open"))
            {
                try
                {
                    singletonDB.instance.conn.Open();
                }
                catch (System.Exception ex)
                {
                    //AutoClosingMessageBox.Show("서버 연결이 해제되었습니다", "ERROR", 2000);
                }
            }
            //if (singletonDB.instance.conn.State != System.Data.ConnectionState.Open)
            //{
            //try
            //{
            //    instance = new singletonDB();
            //}
            //catch (MySqlException ex)
            //{
            //    switch (ex.Number)
            //    {
            //        case 4060:
            //            //AutoClosingMessageBox.Show("invaliddatabase", "ERROR", 2000);
            //            break;
            //    }
            //}
            //AutoClosingMessageBox.Show(instance.conn.State.ToString(), "ERROR", 2000);
            //}      
        }

        public static singletonDB getInstance()
        {
            return instance;
        }
    }
}
