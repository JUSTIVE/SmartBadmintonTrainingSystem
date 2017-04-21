using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace SmartBadmintonTrainingSystem
{
    class singletonDB
    {
        private static singletonDB instance = new singletonDB();
        public MySqlConnection conn;
        string strconn = "Server=220.69.209.170;Database=badminton;Uid=cglab;Pwd=clws";
        private singletonDB()
        {
            instance.conn = new MySqlConnection(strconn);
            instance.conn.Open();
        }

        public static void IsOpen()
        {
            if (!(instance.conn.State==ConnectionState.Open))
            {
                try
                { 
                    singletonDB.instance.conn.Open();
                }
                catch (System.Exception ex)
                {
                    AutoClosingMessageBox.Show("서버 연결이 해제되었습니다", "ERROR", 2000);
                }
            }
            instance.conn.Close();
            instance.conn.Open();
        }
        public static singletonDB getInstance()
        {
            singletonDB.IsOpen();
            instance.conn.Close();
            instance.conn.Open();
            return instance;
        }
        public MySqlConnection getConnection()
        {
            return this.conn;
        }
    }
}
